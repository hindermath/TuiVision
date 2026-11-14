#Requires -Version 7.0
<#
.SYNOPSIS
    DE: Neue Adapter und Legacy-Paritaet pruefen. EN: Test new adapters and legacy parity.
.DESCRIPTION
    DE: Offline-Fixtures, keine echten Agenten oder Profile. Vergleicht gegen
    den unveraenderten Ausgangscommit. Temporaere Dateien werden entfernt.
    EN: Offline fixtures, no real agents or profiles. Compares against the
    unchanged base commit. Temporary files are removed.
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$resolver = Join-Path $root 'scripts/resolve-model-routing.ps1'
$temporary = Join-Path ([IO.Path]::GetTempPath()) "additive-routing-$([guid]::NewGuid())"
$script:checks = 0
function Assert-Adapter {
    param([bool] $Condition, [string] $Message)
    if (-not $Condition) { throw $Message }
    $script:checks++
}
# Functions shadow real CLIs: fixtures cannot accidentally start an installed agent.
function global:codex { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function global:agy { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function global:claude { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function global:copilot { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function global:opencode { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function Invoke-TestResolver {
    param(
        [string] $Path, [string] $Family, [string] $Action, [string] $Fixture, [string] $Config,
        [string] $FrontierModel = '', [string] $FastModel = ''
    )
    $arguments = @{Action=$Action;Harness=$Family;DiscoveryFixture=$Fixture;ConfigPath=$Config;OutputFormat='Json'}
    if ($Path -ceq $resolver) {
        $arguments.FrontierModel = $FrontierModel
        $arguments.FastModel = $FastModel
    }
    $text = & $Path @arguments
    $code = $LASTEXITCODE
    return @{ code = $code; result = ($text -join "`n" | ConvertFrom-Json -AsHashtable) }
}
function Write-TestFixture {
    param($Data)
    $path = Join-Path $temporary 'inventory.json'
    $Data | ConvertTo-Json -Depth 30 | Set-Content $path
    return $path
}
try {
    $null = New-Item -ItemType Directory $temporary
    $baseline = Join-Path $temporary 'baseline.ps1'
    & git -C $root show 'b35ee208959ec133e51ad557820a393b9bdddbf4:scripts/resolve-model-routing.ps1' | Set-Content $baseline
    Assert-Adapter ($LASTEXITCODE -eq 0) 'Baseline commit unavailable.'
    foreach ($family in @('Codex', 'Antigravity')) {
        $data = Get-Content (Join-Path $PSScriptRoot 'fixtures/codex-models.json') -Raw | ConvertFrom-Json -AsHashtable
        if ($family -eq 'Antigravity') {
            $data.models[0].model = 'claude-opus-fixture-thinking'
            $data.models[1].model = 'gemini-fixture-flash-medium'
        }
        $fixture = Write-TestFixture $data
        $config = Join-Path $temporary "$family.json"
        $before = Invoke-TestResolver $baseline $family Refresh $fixture $config
        $beforeProfile = Get-Content $config -Raw | ConvertFrom-Json -AsHashtable
        Remove-Item $config
        $after = Invoke-TestResolver $resolver $family Refresh $fixture $config
        $afterProfile = Get-Content $config -Raw | ConvertFrom-Json -AsHashtable
        $beforeProfile.Remove('generatedAt'); $afterProfile.Remove('generatedAt')
        Assert-Adapter (($beforeProfile | ConvertTo-Json -Depth 30 -Compress) -ceq ($afterProfile | ConvertTo-Json -Depth 30 -Compress)) "$family profile changed."
        Assert-Adapter ($before.code -eq $after.code -and ($before.result | ConvertTo-Json -Depth 30 -Compress) -ceq ($after.result | ConvertTo-Json -Depth 30 -Compress)) "$family refresh result changed."
        $before = Invoke-TestResolver $baseline $family Status $fixture $config
        $after = Invoke-TestResolver $resolver $family Status $fixture $config
        Assert-Adapter ($before.code -eq $after.code -and ($before.result | ConvertTo-Json -Depth 30 -Compress) -ceq ($after.result | ConvertTo-Json -Depth 30 -Compress)) "$family status result changed."
        $data.models += $data.models[0].Clone()
        $fixture = Write-TestFixture $data
        $digest = (Get-FileHash $config).Hash
        $before = Invoke-TestResolver $baseline $family Refresh $fixture $config
        $after = Invoke-TestResolver $resolver $family Refresh $fixture $config
        Assert-Adapter ($before.code -eq 3 -and $after.code -eq 3 -and $before.result.reason -ceq $after.result.reason) "$family ambiguity behavior changed."
        Assert-Adapter ((Get-FileHash $config).Hash -ceq $digest) "$family blocked refresh wrote profile."
    }
    $currentAgyModels = @(
        'gemini-4-argon-medium','gemini-4-argon-high','gemini-4-argon-low',
        'gemini-3.8-flash-high','gemini-3.8-flash-medium','gemini-3.8-flash-low',
        'claude-opus-5-5-low','claude-opus-5-5-medium','claude-opus-5-5-high',
        'claude-sonnet-5-5-low','claude-sonnet-5-5-medium','claude-sonnet-5-5-high',
        'gpt-oss-120b-medium'
    ) | ForEach-Object {
        @{
            id=$_; model=$_; displayName=$_; description='Current agy fixture'; hidden=$false; isDefault=$false
            defaultReasoningEffort='medium'; supportedReasoningEfforts=@(
                @{reasoningEffort='low';description='Low'}, @{reasoningEffort='medium';description='Medium'},
                @{reasoningEffort='high';description='High'}
            )
        }
    }
    $currentAgyFixture = Write-TestFixture @{models=@($currentAgyModels)}
    $currentAgy = Invoke-TestResolver $resolver 'Antigravity' 'Refresh' $currentAgyFixture (Join-Path $temporary 'Antigravity-current.json')
    $currentAgyFrontier = @($currentAgy.result.roles | Where-Object { $_.role -eq 'frontier-reasoning' })
    $currentAgyFast = @($currentAgy.result.roles | Where-Object { $_.role -eq 'fast-mechanical' })
    Assert-Adapter ($currentAgy.code -eq 0 -and $currentAgyFrontier[0].model -ceq 'claude-opus-5-5-high') 'Current agy Opus high frontier was not selected.'
    Assert-Adapter ($currentAgyFast[0].model -ceq 'gemini-3.8-flash-medium') 'Current agy Flash medium fast model was not selected.'
    foreach ($family in @('Claude','Copilot','OpenCode')) {
        $frontier = @{ value='opus'; id='claude-opus-99'; supportsEffort=$true; supportedEffortLevels=@('high','medium'); policy=@{state='enabled'}; capabilities=@{supports=@{reasoningEffort=$true}; reasoning=$true}; supportedReasoningEfforts=@('high','medium'); variants=@{high=@{}; medium=@{}} }
        $fast = @{ value='haiku'; id='claude-haiku-99'; supportsEffort=$false; policy=@{state='enabled'}; capabilities=@{supports=@{reasoningEffort=$false}; reasoning=$false} }
        $inventory = @{ models=@($frontier,$fast); provider='anthropic' }
        $fixture = Write-TestFixture $inventory
        $config = Join-Path $temporary "$family.json"
        $status = Invoke-TestResolver $resolver $family Status $fixture $config
        Assert-Adapter ($status.code -eq 2 -and -not (Test-Path $config)) "$family status wrote profile or failed."
        & $resolver -Action Refresh -Harness $family -DiscoveryFixture $fixture -ConfigPath $config -WhatIf | Out-Null
        Assert-Adapter (-not (Test-Path $config)) "$family WhatIf wrote profile."
        $refresh = Invoke-TestResolver $resolver $family Refresh $fixture $config
        Assert-Adapter ($refresh.code -eq 0 -and $refresh.result.roles.Count -eq 4) "$family refresh failed."
        $profileData = Get-Content $config -Raw | ConvertFrom-Json -AsHashtable
        foreach ($runnerProfileData in $profileData.profiles.Values) {
            Assert-Adapter ($runnerProfileData.executable -ceq 'pwsh') "$family profile bypasses the result adapter."
            Assert-Adapter ('{outputFile}' -cin $runnerProfileData.arguments) "$family profile lacks the runner result path."
            Assert-Adapter ('-HarnessArgumentsJson' -cin $runnerProfileData.arguments) "$family profile lacks an unambiguous JSON argument transport."
            $jsonIndex = [Array]::IndexOf([object[]]$runnerProfileData.arguments, '-HarnessArgumentsJson')
            $decodedHarnessArguments = @($runnerProfileData.arguments[$jsonIndex + 1] | ConvertFrom-Json)
            Assert-Adapter ($decodedHarnessArguments.Count -gt 0) "$family JSON argument transport is empty."
            Assert-Adapter (($decodedHarnessArguments -join "`n") -match 'Do not write the result file yourself; project-file writes remain governed') "$family result instruction must not prohibit authorized project writes."
            Assert-Adapter (@($runnerProfileData.arguments | Where-Object { $_ -like '*invoke-harness-with-result.ps1' }).Count -eq 1) "$family profile lacks the structured-output transport."
            Assert-Adapter ('.specify/presets/model-routing-governance/scripts/adapters/invoke-harness-with-result.ps1' -cin $runnerProfileData.arguments) "$family profile stores a machine-specific adapter path."
            Assert-Adapter ($runnerProfileData.preflight.executable -ceq $family.ToLowerInvariant()) "$family preflight no longer invokes the harness directly."
            if ($family -ceq 'Claude') {
                $mcpIndex = [Array]::IndexOf([object[]]$runnerProfileData.preflight.arguments, '--mcp-config')
                Assert-Adapter ($mcpIndex -ge 0 -and $runnerProfileData.preflight.arguments[$mcpIndex + 2] -ceq '--') 'Claude preflight must terminate variadic MCP arguments before the prompt.'
            }
            if ($family -ceq 'Copilot') {
                Assert-Adapter ('--silent' -cin $runnerProfileData.preflight.arguments) 'Copilot preflight must keep diagnostic banners off standard output.'
                Assert-Adapter ('--silent' -cin $decodedHarnessArguments) 'Copilot execution must return only the agent response on standard output.'
                Assert-Adapter ('CopilotJsonl' -cin $runnerProfileData.arguments) 'Copilot profile must select the JSONL result protocol.'
                Assert-Adapter ('--output-format' -cin $decodedHarnessArguments -and 'json' -cin $decodedHarnessArguments) 'Copilot execution must request JSONL output.'
            }
        }
        $fastProfile = $profileData.profiles["$($family.ToLowerInvariant())-fast-auto"]
        Assert-Adapter ($fastProfile.reasoningEffort -ceq 'default' -and '{reasoningEffort}' -cnotin $fastProfile.arguments) "$family invented effort."
        $digest = (Get-FileHash $config).Hash
        $status = Invoke-TestResolver $resolver $family Status $fixture $config
        Assert-Adapter ($status.code -eq 0 -and (Get-FileHash $config).Hash -ceq $digest) "$family aligned status mutated profile."
        if ($family -eq 'Claude') {
            $extended = $inventory | ConvertTo-Json -Depth 30 | ConvertFrom-Json -AsHashtable
            $variant = $extended.models[0].Clone(); $variant.value = 'opus[1m]'
            $extended.models += $variant
            $variantFixture = Write-TestFixture $extended
            $coexist = Invoke-TestResolver $resolver $family Status $variantFixture $config
            Assert-Adapter ($coexist.code -eq 0 -and (Get-FileHash $config).Hash -ceq $digest) 'Claude extended-context inventory blocked base aliases or changed profile.'
        }
        foreach ($negative in @('duplicate','ambiguousVersions','empty','unknownAmbiguous','missingEffort','invalidId','policyDisabled','policyUnknown','policyUnconfigured')) {
            $copy = $inventory | ConvertTo-Json -Depth 30 | ConvertFrom-Json -AsHashtable
            switch ($negative) {
                duplicate { $copy.models += $copy.models[0].Clone() }
                ambiguousVersions {
                    $copy.models[0].value = 'claude-opus-98'
                    $copy.models[0].id = 'claude-opus-98'
                    $other = $copy.models[0].Clone()
                    $other.value='claude-opus-97'; $other.id='claude-opus-97'
                    $copy.models += $other
                }
                empty { $copy.models = @() }
                unknownAmbiguous {
                    $copy.models[0].value='unknown'; $copy.models[0].id='unknown'
                    $other = $copy.models[0].Clone(); $other.value='other'; $other.id='other'
                    $copy.models += $other
                }
                missingEffort { $copy.models[0].supportsEffort=$null; $copy.models[0].capabilities=@{} }
                invalidId { $copy.models[0].value='--unsafe'; $copy.models[0].id='--unsafe' }
                policyDisabled { if ($family -ne 'Copilot') { continue }; $copy.models[0].policy.state='disabled' }
                policyUnknown { if ($family -ne 'Copilot') { continue }; $copy.models[0].Remove('policy') }
                policyUnconfigured { if ($family -ne 'Copilot') { continue }; $copy.models[0].policy.state='unconfigured' }
            }
            if ($negative -like 'policy*' -and $family -ne 'Copilot') { continue }
            $fixture = Write-TestFixture $copy
            $blocked = Invoke-TestResolver $resolver $family Refresh $fixture $config
            Assert-Adapter ($blocked.code -eq 3 -and $blocked.result.status -ceq 'Blocked') "$family $negative did not block."
            Assert-Adapter ((Get-FileHash $config).Hash -ceq $digest) "$family $negative overwrote profile."
        }
        foreach ($effort in @('medium','low')) {
            $copy = $inventory | ConvertTo-Json -Depth 30 | ConvertFrom-Json -AsHashtable
            $copy.models[1].supportsEffort=$true
            $copy.models[1].supportedEffortLevels=@($effort)
            $copy.models[1].capabilities.supports.reasoningEffort=$true
            $copy.models[1].supportedReasoningEfforts=@($effort)
            $copy.models[1].capabilities.reasoning=$true
            $copy.models[1].variants=@{}; $copy.models[1].variants[$effort]=@{}
            $fixture = Write-TestFixture $copy
            $frontierChoice = if ($family -eq 'Claude') { 'opus' } elseif ($family -eq 'OpenCode') { 'anthropic/claude-opus-99' } else { 'claude-opus-99' }
            $fastChoice = if ($family -eq 'Claude') { 'haiku' } elseif ($family -eq 'OpenCode') { 'anthropic/claude-haiku-99' } else { 'claude-haiku-99' }
            $positive = Invoke-TestResolver $resolver $family Refresh $fixture $config $frontierChoice $fastChoice
            $role = @($positive.result.roles | Where-Object { $_.role -ceq 'fast-mechanical' })
            Assert-Adapter ($positive.code -eq 0 -and $role[0].reasoningEffort -ceq $effort) "$family $effort was not preserved."
        }
    }
    Write-Output "PASS: $script:checks additive-adapter and exact legacy parity assertions; offline."
} finally {
    Remove-Item $temporary -Recurse -Force -ErrorAction SilentlyContinue
    foreach ($name in @('codex','agy','claude','copilot','opencode')) { Remove-Item "Function:\global:$name" -ErrorAction SilentlyContinue }
}
