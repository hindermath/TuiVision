#Requires -Version 7.0
<#
.SYNOPSIS
    DE: Additive Modelladapter. EN: Additive model adapters.
.DESCRIPTION
    DE: Vom Resolver geladen; keine eigenstaendigen Aktionen. Discovery liest
    Metadaten, Auswahl blockiert unbekannte/mehrdeutige Modelle. Keine Installation.
    EN: Loaded by the resolver; no standalone actions. Discovery reads metadata,
    selection blocks unknown/ambiguous models. Never installs dependencies.
#>
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$script:MREffortFlags = @{
    Claude = '--effort'
    Copilot = '--reasoning-effort'
    OpenCode = '--variant'
}
$script:MROpenCodeVariantInModel = $false

function Get-MRNewCliHelp {
    param([string] $Executable, [string[]] $Arguments)
    $commands = @(Get-Command $Executable -CommandType Application -ErrorAction SilentlyContinue)
    Assert-MRCondition ($commands.Count -gt 0) "CLI executable missing: $Executable"
    $command = $commands[0]
    $start = [Diagnostics.ProcessStartInfo]::new($command.Source)
    foreach ($argument in $Arguments) { $start.ArgumentList.Add($argument) }
    $start.UseShellExecute = $false
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $start
    Assert-MRCondition $process.Start() 'CLI capability check could not start.'
    try {
        $stdout = $process.StandardOutput.ReadToEndAsync()
        $stderr = $process.StandardError.ReadToEndAsync()
        if (-not $process.WaitForExit(10000)) {
            $process.Kill($true)
            throw 'CLI capability check timeout.'
        }
        $text = $stdout.GetAwaiter().GetResult() + "`n" + $stderr.GetAwaiter().GetResult()
        Assert-MRCondition ($process.ExitCode -eq 0 -and $text.Length -le 1MB) 'CLI capability check failed.'
        # Some native CLIs colorize or otherwise decorate help even when redirected.
        $withoutAnsi = [regex]::Replace($text, "`e\[[0-?]*[ -/]*[@-~]", '')
        return [regex]::Replace($withoutAnsi, '[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]', '')
    } finally { $process.Dispose() }
}

function Test-MRHelpFlag {
    param([string] $Text, [string] $Flag)
    return [regex]::IsMatch($Text, "(?<![A-Za-z0-9-])$([regex]::Escape($Flag))(?![A-Za-z0-9-])")
}

function Test-MRNewCli {
    param([string] $Family)
    $executable = [string] $script:Harnesses[$Family].executable
    $arguments = if ($Family -eq 'OpenCode') { @('run', '--help') } else { @('--help') }
    $text = Get-MRNewCliHelp $executable $arguments
    $required = switch ($Family) {
        Claude { @('--model', '--effort', '--permission-mode', '--tools', '--strict-mcp-config', '--mcp-config', '--no-session-persistence', '--print') }
        Copilot { @('--model', '--available-tools', '--deny-tool', '--disable-builtin-mcps', '--no-auto-update', '--no-ask-user', '--prompt', '--silent') }
        OpenCode { @('--model', '--agent') }
    }
    foreach ($flag in $required) {
        Assert-MRCondition (Test-MRHelpFlag $text $flag) "${Family}: CLI interface unsupported; required flag $flag missing."
    }
    if ($Family -eq 'Copilot') {
        $effortFlag = if (Test-MRHelpFlag $text '--reasoning-effort') { '--reasoning-effort' }
            elseif (Test-MRHelpFlag $text '--effort') { '--effort' }
            else { '' }
        Assert-MRCondition (-not [string]::IsNullOrWhiteSpace($effortFlag)) 'Copilot: CLI interface unsupported; required reasoning-effort flag missing.'
        $script:MREffortFlags.Copilot = $effortFlag
    } elseif ($Family -eq 'OpenCode') {
        if (Test-MRHelpFlag $text '--variant') {
            $script:MROpenCodeVariantInModel = $false
        } else {
            Assert-MRCondition ($text -match 'provider/model#variant') 'OpenCode: CLI interface unsupported; neither --variant nor provider/model#variant is documented.'
            $script:MROpenCodeVariantInModel = $true
        }
    }
}

function Invoke-MRSdkDiscovery {
    param([string] $Family)
    Assert-MRCondition ([IO.Path]::IsPathFullyQualified($SdkModulePath) -and
        (Test-Path -LiteralPath $SdkModulePath -PathType Leaf)) 'SDK-Einstieg fehlt / SDK entry missing: -SdkModulePath (absolute local file).'
    $node = Get-Command node -CommandType Application -ErrorAction SilentlyContinue
    Assert-MRCondition ($null -ne $node) 'Node.js fehlt / Node.js missing.'
    $start = [Diagnostics.ProcessStartInfo]::new($node.Source)
    foreach ($argument in @((Join-Path $PSScriptRoot 'discover-sdk.mjs'), $Family,
        $SdkModulePath, (Get-Command $script:Harnesses[$Family].executable -CommandType Application).Source)) {
        $start.ArgumentList.Add($argument)
    }
    $start.UseShellExecute = $false
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $process = [Diagnostics.Process]::Start($start)
    try {
        $stdout = $process.StandardOutput.ReadToEndAsync()
        $stderr = $process.StandardError.ReadToEndAsync()
        if (-not $process.WaitForExit(25000)) {
            $process.Kill($true)
            throw 'SDK discovery timeout; blocked.'
        }
        # Never include vendor stderr: it can contain credentials or private paths.
        $text = $stdout.GetAwaiter().GetResult()
        $null = $stderr.GetAwaiter().GetResult()
        Assert-MRCondition ($process.ExitCode -eq 0 -and $text.Length -le 4MB) 'SDK discovery failed; check SDK/CLI compatibility and authentication.'
        try { return $text | ConvertFrom-Json -AsHashtable -Depth 50 }
        catch { throw 'SDK metadata JSON invalid; vendor output suppressed.' }
    } finally { $process.Dispose() }
}

function Invoke-MROpenCodeRequest {
    param([Net.Http.HttpClient] $Client, [uri] $BaseUri, [string] $RelativePath)
    $response = $Client.GetAsync([uri]::new($BaseUri, $RelativePath), [Net.Http.HttpCompletionOption]::ResponseHeadersRead).GetAwaiter().GetResult()
    try {
        if ([int] $response.StatusCode -ge 300 -and [int] $response.StatusCode -lt 400) {
            throw 'OpenCode metadata redirects are blocked.'
        }
        $stream = $response.Content.ReadAsStreamAsync().GetAwaiter().GetResult()
        $memory = [IO.MemoryStream]::new()
        try {
            $buffer = [byte[]]::new(8192)
            $cancel = [Threading.CancellationTokenSource]::new(15000)
            try {
                while (($count = $stream.ReadAsync($buffer, 0, $buffer.Length, $cancel.Token).GetAwaiter().GetResult()) -gt 0) {
                    Assert-MRCondition (($memory.Length + $count) -le 4MB) 'OpenCode metadata exceeds limit.'
                    $memory.Write($buffer, 0, $count)
                }
            } finally { $cancel.Dispose() }
            $text = [Text.Encoding]::UTF8.GetString($memory.ToArray())
        } finally { $memory.Dispose(); $stream.Dispose() }
        return [ordered]@{
            success = $response.IsSuccessStatusCode
            statusCode = [int] $response.StatusCode
            contentType = [string] $response.Content.Headers.ContentType.MediaType
            text = $text
        }
    } finally { $response.Dispose() }
}

function ConvertFrom-MROpenCodeV2 {
    param([hashtable] $Providers, [hashtable] $Models)
    Assert-MRCondition ($Providers.data -is [object[]] -and $Models.data -is [object[]]) 'OpenCode v2 metadata schema unsupported.'
    $providerMatches = @($Providers.data | Where-Object {
        $_ -is [hashtable] -and [string] $_.id -ceq $OpenCodeProvider -and [string] $_.activation -cne 'disabled'
    })
    Assert-MRCondition ($providerMatches.Count -eq 1) 'Selected OpenCode provider is unavailable or ambiguous.'
    $normalized = [Collections.Generic.List[object]]::new()
    foreach ($item in @($Models.data | Where-Object {
        $_ -is [hashtable] -and [string] $_.providerID -ceq $OpenCodeProvider -and
            $_.enabled -is [bool] -and $_.enabled -and [string] $_.status -ceq 'active'
    })) {
        $variants = @{}
        if ($item.variants -is [object[]]) {
            foreach ($variant in $item.variants) {
                if ($variant -is [hashtable] -and $variant.id -is [string]) { $variants[[string] $variant.id] = @{} }
            }
        }
        $normalized.Add(@{
            id = [string] $item.id
            capabilities = @{ reasoning = $variants.Count -gt 0 }
            variants = $variants
        })
    }
    Assert-MRCondition ($normalized.Count -gt 0) 'Selected OpenCode provider has no active enabled models.'
    return @{ models = $normalized.ToArray(); provider = $OpenCodeProvider }
}

function Invoke-MROpenCodeDiscovery {
    Assert-MRCondition (-not [string]::IsNullOrWhiteSpace($OpenCodeProvider)) '-OpenCodeProvider is required; no implicit provider selection.'
    $uri = $null
    Assert-MRCondition ([uri]::TryCreate($OpenCodeServerUrl, [UriKind]::Absolute, [ref]$uri)) 'OpenCode server URL missing or invalid.'
    Assert-MRCondition ($uri.Scheme -ceq 'http' -and $uri.Host -in @('127.0.0.1', '[::1]') -and
        $uri.AbsolutePath -ceq '/' -and -not $uri.UserInfo -and -not $uri.Query -and -not $uri.Fragment) 'OpenCode requires an explicit HTTP loopback origin without credentials/path/query.'
    if ($env:OPENCODE_SERVER_PASSWORD) {
        Assert-MRCondition $TrustOpenCodeLoopbackEndpoint 'OpenCode HTTP loopback password requires -TrustOpenCodeLoopbackEndpoint for this invocation.'
    }
    $handler = [Net.Http.HttpClientHandler]::new()
    $handler.AllowAutoRedirect = $false
    $handler.UseProxy = $false
    $client = [Net.Http.HttpClient]::new($handler)
    $client.Timeout = [TimeSpan]::FromSeconds(15)
    try {
        if ($env:OPENCODE_SERVER_PASSWORD) {
            $username = if ($env:OPENCODE_SERVER_USERNAME) { $env:OPENCODE_SERVER_USERNAME } else { 'opencode' }
            $encoded = [Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes("${username}:$($env:OPENCODE_SERVER_PASSWORD)"))
            $client.DefaultRequestHeaders.Authorization = [Net.Http.Headers.AuthenticationHeaderValue]::new('Basic', $encoded)
        }
        $providerReply = Invoke-MROpenCodeRequest $client $uri 'api/provider'
        $useLegacy = -not $providerReply.success -or $providerReply.contentType -ceq 'text/html'
        if (-not $useLegacy) {
            try { $providerData = $providerReply.text | ConvertFrom-Json -AsHashtable -Depth 50 }
            catch { throw 'OpenCode v2 provider metadata JSON invalid.' }
            if ($providerData.data -is [object[]]) {
                $modelReply = Invoke-MROpenCodeRequest $client $uri 'api/model'
                Assert-MRCondition $modelReply.success 'OpenCode v2 model metadata unavailable.'
                try { $modelData = $modelReply.text | ConvertFrom-Json -AsHashtable -Depth 50 }
                catch { throw 'OpenCode v2 model metadata JSON invalid.' }
                return ConvertFrom-MROpenCodeV2 $providerData $modelData
            }
            throw 'OpenCode v2 provider metadata schema unsupported.'
        }
        $legacyReply = Invoke-MROpenCodeRequest $client $uri 'provider'
        Assert-MRCondition $legacyReply.success 'OpenCode provider metadata unavailable; check local server/authentication.'
        try { $data = $legacyReply.text | ConvertFrom-Json -AsHashtable -Depth 50 }
        catch { throw 'OpenCode legacy provider metadata JSON invalid.' }
        Assert-MRCondition ($data.connected -is [object[]] -and $OpenCodeProvider -cin $data.connected) 'Selected OpenCode provider is not connected.'
        $providers = @($data.all | Where-Object { $_.id -ceq $OpenCodeProvider })
        Assert-MRCondition ($providers.Count -eq 1 -and $providers[0].models -is [hashtable]) 'OpenCode provider metadata is ambiguous or unsupported.'
        return @{ models = @($providers[0].models.Values); provider = $OpenCodeProvider }
    } catch {
        throw 'OpenCode metadata discovery blocked; check loopback server, selected provider, authentication and API shape.'
    } finally { $client.Dispose() }
}

function Invoke-MRNewDiscovery {
    param([string] $Family)
    $data = if ($Family -eq 'OpenCode') { Invoke-MROpenCodeDiscovery } else { Invoke-MRSdkDiscovery $Family }
    return @(ConvertTo-MRDiscoveredModel $Family $data)
}

function ConvertTo-MRDiscoveredModel {
    param([string] $Family, [hashtable] $Data)
    Assert-MRCondition ($Data.models -is [object[]] -and $Data.models.Count -gt 0 -and $Data.models.Count -le 4096) 'Empty, oversized or unsupported model inventory.'
    $seen = @{}
    foreach ($item in $Data.models) {
        Assert-MRCondition ($item -is [hashtable]) 'Malformed model metadata.'
        $efforts = @()
        $defaultOnly = $false
        if ($Family -eq 'Claude') {
            $id = [string] $item.value
            if ($item.ContainsKey('supportsEffort') -and $item.supportsEffort -is [bool]) {
                if ($item.supportsEffort -and $item.ContainsKey('supportedEffortLevels')) { $efforts = @($item.supportedEffortLevels) }
                elseif (-not $item.supportsEffort) { $defaultOnly = $true }
            }
        } elseif ($Family -eq 'Copilot') {
            $id = [string] $item.id
            # Unknown policy is not permission. Do not infer enablement from list membership.
            if (-not $item.ContainsKey('policy') -or $item.policy -isnot [hashtable] -or
                -not $item.policy.ContainsKey('state') -or $item.policy.state -cne 'enabled') { continue }
            if ($item.ContainsKey('capabilities') -and $item.capabilities -is [hashtable] -and
                $item.capabilities.ContainsKey('supports') -and $item.capabilities.supports -is [hashtable] -and
                $item.capabilities.supports.ContainsKey('reasoningEffort')) {
                $supports = $item.capabilities.supports.reasoningEffort
                if ($supports -is [bool] -and $supports -and $item.ContainsKey('supportedReasoningEfforts')) { $efforts = @($item.supportedReasoningEfforts) }
                elseif ($supports -is [bool] -and -not $supports) { $defaultOnly = $true }
            }
        } else {
            Assert-MRCondition ($Data.provider -is [string] -and $Data.provider -cmatch '^[a-zA-Z0-9][a-zA-Z0-9._-]*$') 'OpenCode provider missing/invalid.'
            Assert-MRCondition ([string] $item.id -cmatch '^[a-zA-Z0-9][a-zA-Z0-9._:/-]{0,199}(?:\[1m\])?$') 'Invalid OpenCode model identifier.'
            $id = "$($Data.provider)/$($item.id)"
            if ($item.ContainsKey('capabilities') -and $item.capabilities -is [hashtable] -and
                $item.capabilities.ContainsKey('reasoning') -and $item.capabilities.reasoning -is [bool]) {
                if (-not $item.capabilities.reasoning) { $defaultOnly = $true }
                elseif ($item.ContainsKey('variants') -and $item.variants -is [hashtable]) { $efforts = @($item.variants.Keys) }
            }
        }
        # Documented extended-context entries may coexist with the base aliases.
        # Inventory them without making them an implicit selection or shell syntax.
        Assert-MRCondition ($id -cmatch '^[a-zA-Z0-9][a-zA-Z0-9._:/-]{0,199}(?:\[1m\])?$') 'Invalid model identifier.'
        # These are switching/default policies, not an exact model binding.
        if ($Family -ne 'OpenCode' -and $id -cin @('default', 'auto', 'opusplan')) { continue }
        Assert-MRCondition (-not $seen.ContainsKey($id)) 'Duplicate model identifier; discovery is ambiguous.'
        $seen[$id] = $true
        $accepted = @($efforts | Where-Object { $_ -is [string] -and $_ -cin @('low','medium','high','xhigh','max') } | Sort-Object -Unique)
        [ordered]@{ model = $id; efforts = $accepted; defaultOnly = $defaultOnly }
    }
}

function Get-MRAdapterEffort {
    param([hashtable] $Model, [string] $Role)
    if ($Role -eq 'frontier') {
        if ('high' -cin $Model.efforts) { return 'high' }
        return ''
    }
    foreach ($effort in @('medium', 'low')) {
        if ($effort -cin $Model.efforts) { return $effort }
    }
    if ($Model.defaultOnly) { return 'default' }
    # Fast tasks may reuse an expensive model; never invent a lower effort.
    foreach ($effort in @('high', 'xhigh', 'max')) {
        if ($effort -cin $Model.efforts) { return $effort }
    }
    return ''
}

function Select-MRAdapterCandidate {
    param([string] $Family, [string] $Role, [object[]] $Models, [object[]] $Rules, [string] $ExplicitModel, [hashtable] $FallbackModel)
    $eligible = @($Models | Where-Object { (Get-MRAdapterEffort $_ $Role) -ne '' })
    Assert-MRCondition ($eligible.Count -gt 0) "${Family}: $Role has no model with confirmed required reasoning metadata."
    if ($ExplicitModel) {
        $candidates = @($eligible | Where-Object { $_.model -ceq $ExplicitModel })
        Assert-MRCondition ($candidates.Count -eq 1) "${Family}: explicit $Role model is not an eligible discovered model."
        return $candidates[0]
    }
    foreach ($rule in $Rules) {
        if ($rule.name -ceq 'reuse-frontier') {
            Assert-MRCondition ($null -ne $FallbackModel -and (Get-MRAdapterEffort $FallbackModel $Role) -ne '') "${Family}: no eligible frontier model to reuse."
            return $FallbackModel
        }
        $candidates = @($eligible | Where-Object {
            $name = if ($Family -eq 'OpenCode') { ($_.model -split '/', 2)[1] } else { $_.model }
            # A larger context can change cost; require an explicit exact choice.
            $name -cnotmatch '\[1m\]$' -and $name -cmatch $rule.pattern
        })
        if ($candidates.Count -eq 0) { continue }
        $flag = if ($Role -eq 'frontier') { '-FrontierModel' } else { '-FastModel' }
        $ids = @($candidates | ForEach-Object { $_.model } | Sort-Object -CaseSensitive) -join ', '
        Assert-MRCondition ($candidates.Count -eq 1) "${Family}: $Role rule '$($rule.name)' is ambiguous: $ids. Specify $flag with an exact discovered ID."
        return $candidates[0]
    }
    throw "${Family}: no $Role candidate matched selection policy."
}

function Select-MRDiscoveredModel {
    param(
        [string] $Family,
        [AllowEmptyCollection()][object[]] $Models,
        [string] $EffectiveFrontierModel = $FrontierModel,
        [string] $EffectiveFastModel = $FastModel
    )
    $policy = Read-MRJson (Join-Path $PSScriptRoot 'selection-policy.json')
    $frontier = Select-MRAdapterCandidate $Family 'frontier' $Models $policy.roles.frontier $EffectiveFrontierModel
    $fast = Select-MRAdapterCandidate $Family 'fast' $Models $policy.roles.fast $EffectiveFastModel $frontier
    return @{
        frontier = $frontier; fast = $fast
        frontierEffort = Get-MRAdapterEffort $frontier 'frontier'
        fastEffort = Get-MRAdapterEffort $fast 'fast'
    }
}

function New-MRAdapterProfile {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Returns an in-memory profile only; the resolver owns ShouldProcess and persistence.')]
    [CmdletBinding()]
    param([string] $Family, [string] $Role, [hashtable] $Model, [string] $Effort)
    $readOnly = $Role -eq 'fast-mechanical'
    $effortArgs = if ($Effort -eq 'default' -or ($Family -eq 'OpenCode' -and $script:MROpenCodeVariantInModel)) { @() } else { @([string] $script:MREffortFlags[$Family], '{reasoningEffort}') }
    $resultInstruction = 'Return the machine-readable autonomous phase result as the only standard-output content. Use autonomous-phase-result-template.json. Do not write the result file yourself; project-file writes remain governed by the task and this profile.'
    switch ($Family) {
        Claude {
            $common = @('--print', '--no-session-persistence', '--model', '{model}') + $effortArgs
            # --mcp-config accepts multiple values, so -- is required before the positional prompt.
            $preflight = $common + @('--permission-mode', 'plan', '--tools', '', '--strict-mcp-config', '--mcp-config', '{"mcpServers":{}}', '--', 'Reply exactly MODEL_READY without using tools.')
            $mode = if ($readOnly) { 'plan' } else { 'acceptEdits' }
            $harnessArguments = $common + @('--permission-mode', $mode, ('{prompt}' + "`n`n" + $resultInstruction))
        }
        Copilot {
            $common = @('--model', '{model}', '--no-auto-update', '--no-ask-user', '--silent') + $effortArgs
            $preflight = $common + @('--available-tools', 'view', '--deny-tool', 'write,shell', '--disable-builtin-mcps', '--prompt', 'Reply exactly MODEL_READY without using tools.')
            $harnessArguments = $common + $(if ($readOnly) { @('--available-tools', 'view', '--deny-tool', 'write,shell', '--disable-builtin-mcps') } else { @() }) + @('--output-format', 'json', '--prompt', ('{prompt}' + "`n`n" + $resultInstruction))
        }
        OpenCode {
            $modelArgument = if ($Effort -ne 'default' -and $script:MROpenCodeVariantInModel) { '{model}#{reasoningEffort}' } else { '{model}' }
            $common = @('run', '--model', $modelArgument) + $effortArgs
            $preflight = $common + @('--agent', 'plan', 'Reply exactly MODEL_READY without using tools.')
            $mode = if ($readOnly) { 'plan' } else { 'build' }
            $harnessArguments = $common + @('--agent', $mode, ('{prompt}' + "`n`n" + $resultInstruction))
        }
    }
    $executable = [string] $script:Harnesses[$Family].executable
    # Repository-relative canonical path keeps root/preset resolution byte-identical.
    $resultAdapter = '.specify/presets/model-routing-governance/scripts/adapters/invoke-harness-with-result.ps1'
    $protocol = if ($Family -eq 'Copilot') { 'CopilotJsonl' } else { 'Plain' }
    $arguments = @('-NoProfile', '-File', $resultAdapter, '-OutputFile', '{outputFile}', '-Executable', $executable,
        '-OutputProtocol', $protocol, '-HarnessArgumentsJson', ($harnessArguments | ConvertTo-Json -Compress))
    return [ordered]@{
        routingRole = $Role; agentFamily = $Family; model = $Model.model
        reasoningEffort = $Effort; executable = 'pwsh'
        preflight = [ordered]@{ executable = $executable; arguments = $preflight }
        arguments = $arguments
    }
}
