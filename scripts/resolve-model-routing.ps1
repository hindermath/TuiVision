#Requires -Version 7.0
<#
.SYNOPSIS
    Prueft oder aktualisiert lokale Spec-Kit-Modellbindungen.

.DESCRIPTION
    DE: Erkennt das ausgewaehlte Agenten-Harness, liest dessen verfuegbare
    Modelle soweit die lokale Schnittstelle dies belastbar erlaubt und ordnet
    vier modellgestuetzte Spec-Kit-Rollen fail-closed zu. Konkrete Modelle
    bleiben in einer lokalen, nicht versionierten Konfiguration.

    EN: Detects the selected agent harness, reads available models where the
    local interface supports reliable discovery, and maps four model-backed
    Spec Kit roles with fail-closed behavior. Concrete models remain local.

.PARAMETER Action
    Status ist read-only. Refresh schreibt die lokale Konfiguration atomar.

.PARAMETER Harness
    Auto, Codex, Antigravity, Claude, Copilot oder OpenCode.

.PARAMETER ConfigPath
    Lokaler Zielpfad. Ohne Angabe wird der Plattform-Konfigurationspfad oder
    ein vorhandener Home-Baseline-Legacypfad verwendet.

.PARAMETER RoutingRoot
    Wurzel der installierten Presets mit model-routing.json-Dateien.

.PARAMETER DiscoveryFixture
    Nur fuer deterministische Tests: JSON-Modellinventar statt Provideraufruf.

.EXAMPLE
    pwsh -NoProfile -File scripts/resolve-model-routing.ps1 -Action Status -Harness Codex

.EXAMPLE
    pwsh -NoProfile -File scripts/resolve-model-routing.ps1 -Action Refresh -Harness Antigravity
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory)]
    [ValidateSet('Status', 'Refresh')]
    [string] $Action,

    [ValidateSet('Auto', 'Codex', 'Antigravity', 'Claude', 'Copilot', 'OpenCode')]
    [string] $Harness = 'Auto',

    [string] $ConfigPath = '',
    [string] $RoutingRoot = '',
    [string] $DiscoveryFixture = '',

    [ValidateSet('Text', 'Json')]
    [string] $OutputFormat = 'Text'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$script:Harnesses = [ordered]@{
    Codex = [ordered]@{ executable = 'codex'; discoveryMode = 'Enumerate' }
    Antigravity = [ordered]@{ executable = 'agy'; discoveryMode = 'EnumerateNames' }
    Claude = [ordered]@{ executable = 'claude'; discoveryMode = 'ValidateCandidate' }
    Copilot = [ordered]@{ executable = 'copilot'; discoveryMode = 'ValidateCandidate' }
    OpenCode = [ordered]@{ executable = 'opencode'; discoveryMode = 'ConfiguredOnly' }
}

function Assert-MRCondition {
    param([Parameter(Mandatory)][bool] $Condition, [Parameter(Mandatory)][string] $Message)
    if (-not $Condition) { throw $Message }
}

function Read-MRJson {
    param([Parameter(Mandatory)][string] $Path)
    Assert-MRCondition (Test-Path -LiteralPath $Path -PathType Leaf) "JSON-Datei fehlt: $Path"
    return Get-Content -LiteralPath $Path -Raw -Encoding utf8 | ConvertFrom-Json -AsHashtable
}

function Write-MRJsonAtomic {
    param([Parameter(Mandatory)][string] $Path, [Parameter(Mandatory)] $Value)
    $parent = Split-Path -Parent $Path
    if ($parent) { [void](New-Item -ItemType Directory -Path $parent -Force) }
    $temporary = "${Path}.tmp.$PID"
    try {
        $Value | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $temporary -Encoding utf8NoBOM
        Move-Item -LiteralPath $temporary -Destination $Path -Force
    } finally {
        Remove-Item -LiteralPath $temporary -Force -ErrorAction SilentlyContinue
    }
}

function Get-MRDefaultConfigPath {
    $legacy = Join-Path $HOME '.home-baseline/spec-kit/runner-profiles.local.json'
    if (Test-Path -LiteralPath $legacy -PathType Leaf) { return $legacy }
    if ($IsWindows -and -not [string]::IsNullOrWhiteSpace($env:APPDATA)) {
        return Join-Path $env:APPDATA 'spec-kit/model-routing-profiles.json'
    }
    $configHome = if (-not [string]::IsNullOrWhiteSpace($env:XDG_CONFIG_HOME)) {
        $env:XDG_CONFIG_HOME
    } else {
        Join-Path $HOME '.config'
    }
    return Join-Path $configHome 'spec-kit/model-routing-profiles.json'
}

function Get-MRCommandVersion {
    param([Parameter(Mandatory)][string] $Executable)
    $lines = @(& $Executable --version 2>&1)
    Assert-MRCondition ($LASTEXITCODE -eq 0 -and $lines.Count -gt 0) "Version fuer '$Executable' nicht lesbar."
    return [string] $lines[0]
}

function Get-MRInstalledHarnesses {
    $found = [Collections.Generic.List[string]]::new()
    foreach ($entry in $script:Harnesses.GetEnumerator()) {
        if ($null -ne (Get-Command ([string] $entry.Value.executable) -ErrorAction SilentlyContinue)) {
            $found.Add([string] $entry.Key)
        }
    }
    return $found.ToArray()
}

function Resolve-MRHarness {
    param([Parameter(Mandatory)][string] $Requested, [string] $ExistingConfig)
    if ($Requested -ne 'Auto') { return $Requested }
    if (-not [string]::IsNullOrWhiteSpace($env:SPECKIT_AGENT_FAMILY) -and
        $script:Harnesses.Contains($env:SPECKIT_AGENT_FAMILY)) {
        return [string] $env:SPECKIT_AGENT_FAMILY
    }
    if (Test-Path -LiteralPath $ExistingConfig -PathType Leaf) {
        $existing = Read-MRJson $ExistingConfig
        if ($existing.harness -is [hashtable] -and
            $script:Harnesses.Contains([string] $existing.harness.family)) {
            return [string] $existing.harness.family
        }
    }
    $installed = @(Get-MRInstalledHarnesses)
    Assert-MRCondition ($installed.Count -eq 1) "Harness Auto ist mehrdeutig. Verfuegbar: $($installed -join ', ')."
    return [string] $installed[0]
}

function Get-MRCatalogHash {
    param([string] $Root)
    if ([string]::IsNullOrWhiteSpace($Root) -or -not (Test-Path -LiteralPath $Root -PathType Container)) {
        return 'N/A'
    }
    $builder = [Text.StringBuilder]::new()
    $files = @(Get-ChildItem -LiteralPath $Root -Filter 'model-routing.json' -File -Recurse | Sort-Object FullName)
    foreach ($file in $files) {
        $text = (Get-Content -LiteralPath $file.FullName -Raw -Encoding utf8).Replace("`r`n", "`n").Replace("`r", "`n")
        [void]$builder.Append($file.Name).Append("`n").Append($text).Append("`n")
    }
    if ($files.Count -eq 0) { return 'N/A' }
    $bytes = [Text.Encoding]::UTF8.GetBytes($builder.ToString())
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($bytes)).ToLowerInvariant()
}

function Invoke-MRCodexDiscovery {
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = 'codex'
    $start.ArgumentList.Add('app-server')
    $start.ArgumentList.Add('--listen')
    $start.ArgumentList.Add('stdio://')
    $start.UseShellExecute = $false
    $start.RedirectStandardInput = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $start
    Assert-MRCondition $process.Start() 'Codex app-server konnte nicht gestartet werden.'
    try {
        $initialize = [ordered]@{
            jsonrpc = '2.0'; id = 1; method = 'initialize'
            params = [ordered]@{
                clientInfo = [ordered]@{ name = 'model-routing-governance'; version = '0.1.2' }
                capabilities = [ordered]@{ experimentalApi = $true }
            }
        } | ConvertTo-Json -Compress -Depth 10
        $list = [ordered]@{
            jsonrpc = '2.0'; id = 2; method = 'model/list'
            params = [ordered]@{ limit = 100; includeHidden = $false }
        } | ConvertTo-Json -Compress -Depth 10
        $process.StandardInput.WriteLine($initialize)
        $process.StandardInput.Flush()
        # Codex completes initialize asynchronously. The model/list request is
        # sent only after a bounded settling interval so it is not discarded
        # while the app-server is still negotiating client capabilities.
        Start-Sleep -Milliseconds 1000
        $process.StandardInput.WriteLine($list)
        $process.StandardInput.Flush()
        Start-Sleep -Milliseconds 2000
        $process.StandardInput.Close()
        $stdoutTask = $process.StandardOutput.ReadToEndAsync()
        $stderrTask = $process.StandardError.ReadToEndAsync()
        if (-not $process.WaitForExit(15000)) {
            $process.Kill($true)
            throw 'Codex model/list Timeout.'
        }
        $stdout = $stdoutTask.GetAwaiter().GetResult()
        $stderr = $stderrTask.GetAwaiter().GetResult()
        Assert-MRCondition ($process.ExitCode -eq 0) "Codex model/list fehlgeschlagen: $stderr"
        foreach ($line in @($stdout -split "`n")) {
            if ([string]::IsNullOrWhiteSpace($line)) { continue }
            try { $message = $line | ConvertFrom-Json -AsHashtable } catch { continue }
            if ($message.ContainsKey('id') -and [string] $message.id -eq '2' -and
                $message.ContainsKey('result') -and $message.result -is [hashtable]) {
                return @($message.result.data | Where-Object { -not [bool] $_.hidden })
            }
        }
        throw 'Codex model/list lieferte keine Modellantwort.'
    } finally {
        $process.Dispose()
    }
}

function Invoke-MRAntigravityDiscovery {
    $lines = @(& agy models 2>&1)
    Assert-MRCondition ($LASTEXITCODE -eq 0) 'agy models ist fehlgeschlagen.'
    return @($lines | ForEach-Object { (([string] $_) -split "`t", 2)[0].Trim() } | Where-Object { $_ -match '^[A-Za-z0-9][A-Za-z0-9._-]+$' } | ForEach-Object {
        [ordered]@{
            id = $_; model = $_; displayName = $_; description = 'Name-only Antigravity catalog entry'
            hidden = $false; isDefault = $false; defaultReasoningEffort = 'medium'
            supportedReasoningEfforts = @(
                [ordered]@{ reasoningEffort = 'low'; description = 'Low' },
                [ordered]@{ reasoningEffort = 'medium'; description = 'Medium' },
                [ordered]@{ reasoningEffort = 'high'; description = 'High' }
            )
        }
    })
}

function Get-MRModels {
    param([Parameter(Mandatory)][string] $Family, [string] $Fixture)
    if (-not [string]::IsNullOrWhiteSpace($Fixture)) {
        $fixtureData = Read-MRJson ([IO.Path]::GetFullPath($Fixture))
        Assert-MRCondition ($fixtureData.models -is [object[]]) 'DiscoveryFixture.models muss ein Array sein.'
        return @($fixtureData.models)
    }
    switch ($Family) {
        'Codex' { return @(Invoke-MRCodexDiscovery) }
        'Antigravity' { return @(Invoke-MRAntigravityDiscovery) }
        default { return @() }
    }
}

function Test-MRReasoning {
    param([Parameter(Mandatory)][hashtable] $Model, [Parameter(Mandatory)][string] $Effort)
    return @($Model.supportedReasoningEfforts | ForEach-Object { [string] $_.reasoningEffort }) -contains $Effort
}

function Select-MRModels {
    param([Parameter(Mandatory)][string] $Family, [Parameter(Mandatory)][object[]] $Models)
    Assert-MRCondition ($Models.Count -gt 0) "Keine Modelle fuer $Family erkannt."
    if ($Family -eq 'Codex') {
        $frontier = @($Models | Where-Object { [bool] $_.isDefault })
        if ($frontier.Count -ne 1) {
            $frontier = @($Models | Where-Object { [string] $_.description -match '(?i)frontier' })
        }
        Assert-MRCondition ($frontier.Count -eq 1) 'Codex Frontier-Zuordnung ist nicht eindeutig.'
        $fast = @($Models | Where-Object { [string] $_.description -match '(?i)fast and affordable' })
        if ($fast.Count -ne 1) {
            $fast = @($Models | Where-Object { [string] $_.description -match '(?i)small, fast|ultra-fast' })
        }
        Assert-MRCondition ($fast.Count -eq 1) 'Codex Fast-Zuordnung ist nicht eindeutig.'
    } elseif ($Family -eq 'Antigravity') {
        $frontier = @($Models | Where-Object { [string] $_.model -match '^claude-opus-.+-thinking$' })
        if ($frontier.Count -ne 1) {
            $frontier = @($Models | Where-Object { [string] $_.model -match '^gemini-.+-pro-high$' })
        }
        $fast = @($Models | Where-Object { [string] $_.model -match '^gemini-.+-flash-medium$' } | Select-Object -First 1)
        Assert-MRCondition ($frontier.Count -eq 1) 'Antigravity Frontier-Zuordnung ist nicht eindeutig.'
        Assert-MRCondition ($fast.Count -eq 1) 'Antigravity Fast-Zuordnung ist nicht eindeutig.'
    } else {
        throw "$Family unterstuetzt in diesem Preset-Release keine sichere automatische Zuordnung."
    }
    $frontierModel = [hashtable] $frontier[0]
    $fastModel = [hashtable] $fast[0]
    Assert-MRCondition (Test-MRReasoning $frontierModel 'high') 'Frontier-Modell unterstuetzt Reasoning high nicht.'
    $fastEffort = if (Test-MRReasoning $fastModel 'medium') { 'medium' } elseif (Test-MRReasoning $fastModel 'low') { 'low' } else { '' }
    Assert-MRCondition (-not [string]::IsNullOrWhiteSpace($fastEffort)) 'Fast-Modell besitzt keine sichere Reasoning-Stufe.'
    return [ordered]@{ frontier = $frontierModel; fast = $fastModel; frontierEffort = 'high'; fastEffort = $fastEffort }
}

function New-MRProfile {
    param(
        [Parameter(Mandatory)][string] $Family,
        [Parameter(Mandatory)][string] $Role,
        [Parameter(Mandatory)][hashtable] $Model,
        [Parameter(Mandatory)][string] $Effort
    )
    $executable = [string] $script:Harnesses[$Family].executable
    if ($Family -eq 'Codex') {
        $preflightArguments = @('exec', '--ephemeral', '--model', '{model}', '--config', 'model_reasoning_effort="{reasoningEffort}"', '--sandbox', 'read-only', '--cd', '{worktree}', 'Reply exactly MODEL_READY without using tools.')
        $sandbox = if ($Role -eq 'fast-mechanical') { 'read-only' } else { 'workspace-write' }
        $arguments = @('exec', '--ephemeral', '--model', '{model}', '--config', 'model_reasoning_effort="{reasoningEffort}"', '--sandbox', $sandbox, '--cd', '{worktree}', '--output-last-message', '{outputFile}', '{prompt}')
    } elseif ($Family -eq 'Antigravity') {
        $preflightArguments = @('--model', '{model}', '--effort', '{reasoningEffort}', '--mode', 'plan', '--print', 'Reply exactly MODEL_READY without using tools.')
        $mode = if ($Role -eq 'fast-mechanical') { 'plan' } else { 'accept-edits' }
        $arguments = @('--model', '{model}', '--effort', '{reasoningEffort}', '--mode', $mode, '--print', '{prompt}')
    } else {
        throw "Kein Profilrenderer fuer $Family."
    }
    return [ordered]@{
        routingRole = $Role
        agentFamily = $Family
        model = [string] $Model.model
        reasoningEffort = $Effort
        executable = $executable
        preflight = [ordered]@{ executable = $executable; arguments = $preflightArguments }
        arguments = $arguments
    }
}

function New-MRConfig {
    param(
        [Parameter(Mandatory)][string] $Family,
        [Parameter(Mandatory)][string] $Version,
        [Parameter(Mandatory)][string] $Mode,
        [Parameter(Mandatory)][hashtable] $Selection,
        [Parameter(Mandatory)][string] $CatalogHash
    )
    $prefix = $Family.ToLowerInvariant()
    return [ordered]@{
        schemaVersion = '2.0'
        generatedBy = 'model-routing-governance/0.1.2'
        generatedAt = [DateTime]::UtcNow.ToString('o')
        harness = [ordered]@{ family = $Family; version = $Version; discoveryMode = $Mode }
        catalogSha256 = $CatalogHash
        profiles = [ordered]@{
            "${prefix}-frontier-auto" = New-MRProfile $Family 'frontier-reasoning' $Selection.frontier $Selection.frontierEffort
            "${prefix}-implementation-auto" = New-MRProfile $Family 'long-running-implementation' $Selection.frontier $Selection.frontierEffort
            "${prefix}-review-auto" = New-MRProfile $Family 'coding-review' $Selection.frontier $Selection.frontierEffort
            "${prefix}-fast-auto" = New-MRProfile $Family 'fast-mechanical' $Selection.fast $Selection.fastEffort
        }
    }
}

function Write-MRResult {
    param([Parameter(Mandatory)][hashtable] $Result, [Parameter(Mandatory)][string] $Format)
    if ($Format -eq 'Json') { $Result | ConvertTo-Json -Depth 20; return }
    Write-Output "Model routing: $($Result.status)"
    Write-Output "Harness: $($Result.harness)"
    Write-Output "Discovery: $($Result.discoveryMode)"
    Write-Output "Models: $($Result.modelCount)"
    Write-Output "Config: $($Result.configPath)"
    Write-Output "Next action: $($Result.nextAction)"
}

if ([string]::IsNullOrWhiteSpace($ConfigPath)) { $ConfigPath = Get-MRDefaultConfigPath }
$ConfigPath = [IO.Path]::GetFullPath($ConfigPath)
$resolvedHarness = Resolve-MRHarness $Harness $ConfigPath
$adapter = $script:Harnesses[$resolvedHarness]
$executable = [string] $adapter.executable
Assert-MRCondition ($null -ne (Get-Command $executable -ErrorAction SilentlyContinue)) "Harness executable fehlt: $executable"
$version = Get-MRCommandVersion $executable
$models = @(Get-MRModels $resolvedHarness $DiscoveryFixture)
$mode = [string] $adapter.discoveryMode
$catalogHash = Get-MRCatalogHash $RoutingRoot

try {
    $selection = Select-MRModels $resolvedHarness $models
    $candidate = New-MRConfig $resolvedHarness $version $mode $selection $catalogHash
    $status = if (Test-Path -LiteralPath $ConfigPath -PathType Leaf) {
        $existing = Read-MRJson $ConfigPath
        if ([string] $existing.schemaVersion -ne '2.0') {
            'RefreshRequired'
        } else {
            $existingComparable = [ordered]@{
                schemaVersion = [string] $existing.schemaVersion
                generatedBy = [string] $existing.generatedBy
                harness = $existing.harness
                catalogSha256 = [string] $existing.catalogSha256
                profiles = $existing.profiles
            } | ConvertTo-Json -Depth 30 -Compress
            $candidateComparable = [ordered]@{
                schemaVersion = [string] $candidate.schemaVersion
                generatedBy = [string] $candidate.generatedBy
                harness = $candidate.harness
                catalogSha256 = [string] $candidate.catalogSha256
                profiles = $candidate.profiles
            } | ConvertTo-Json -Depth 30 -Compress
            if ($existingComparable -ceq $candidateComparable) { 'Aligned' } else { 'RefreshRequired' }
        }
    } else { 'RefreshRequired' }
    if ($Action -eq 'Refresh') {
        if ($PSCmdlet.ShouldProcess($ConfigPath, "Write local $resolvedHarness model routing profile")) {
            Write-MRJsonAtomic $ConfigPath $candidate
            $status = 'Aligned'
        }
    }
    Write-MRResult ([ordered]@{
        status = $status; harness = $resolvedHarness; harnessVersion = $version
        discoveryMode = $mode; modelCount = $models.Count; configPath = $ConfigPath
        catalogSha256 = $catalogHash
        roles = @($candidate.profiles.GetEnumerator() | ForEach-Object {
            [ordered]@{ role = [string] $_.Value.routingRole; model = [string] $_.Value.model; reasoningEffort = [string] $_.Value.reasoningEffort }
        })
        nextAction = if ($status -eq 'Aligned') { 'N/A' } else { 'Run speckit.model-routing-refresh with explicit local authority.' }
    }) $OutputFormat
    if ($status -eq 'RefreshRequired') { exit 2 }
} catch {
    Write-MRResult ([ordered]@{
        status = 'Blocked'; harness = $resolvedHarness; harnessVersion = $version
        discoveryMode = $mode; modelCount = $models.Count; configPath = $ConfigPath
        reason = $_.Exception.Message; nextAction = 'Resolve the reported harness or mapping blocker and repeat status.'
    }) $OutputFormat
    exit 3
}
