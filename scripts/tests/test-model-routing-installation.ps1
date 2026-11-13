#Requires -Version 7.0
<#
.SYNOPSIS
    DE: Installiertes Model Routing und Root-Einstiege pruefen.
    EN: Check installed Model Routing and top-level entry points.
.DESCRIPTION
    DE: Prueft die vollstaendige Laufzeitkopie und identische Rollen-/Profil-
    Ergebnisse fuer alle fuenf Harnesses mit Fixtures. Keine echten Agenten,
    Konten, SDKs oder persoenlichen Profile; temporaere Dateien werden entfernt.
    EN: Verifies the complete runtime copy and equal role/profile results for
    all five harnesses using fixtures. No real agents, accounts, SDKs or personal
    profiles; temporary files are removed.
.EXAMPLE
    pwsh -NoProfile -File scripts/tests/test-model-routing-installation.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$root=Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
$package=Join-Path $root '.specify/presets/model-routing-governance'
$temporary=Join-Path ([IO.Path]::GetTempPath()) "model-routing-install-$([guid]::NewGuid())"
$script:checks=0
function Assert-Installation {
    param([bool]$Condition,[string]$Message)
    if(-not $Condition){throw $Message}
    $script:checks++
}
function global:codex { $global:LASTEXITCODE=0; 'fixture-cli 1.0' }
function global:agy { $global:LASTEXITCODE=0; 'fixture-cli 1.0' }
function global:claude { $global:LASTEXITCODE=0; 'fixture-cli 1.0' }
function global:copilot { $global:LASTEXITCODE=0; 'fixture-cli 1.0' }
function global:opencode { $global:LASTEXITCODE=0; 'fixture-cli 1.0' }
try {
    $null=New-Item -ItemType Directory $temporary
    foreach($path in @('scripts/resolve-model-routing.ps1','scripts/resolve-model-routing.sh','scripts/adapters/new-harnesses.ps1','scripts/adapters/invoke-harness-with-result.ps1','scripts/adapters/discover-sdk.mjs','scripts/adapters/selection-policy.json')){
        Assert-Installation ((Get-FileHash (Join-Path $root $path)).Hash -ceq (Get-FileHash (Join-Path $package $path)).Hash) "Runtime closure differs from installed release: ${path}"
    }
    foreach($family in @('Codex','Antigravity','Claude','Copilot','OpenCode')){
        if($family -in @('Codex','Antigravity')){
            $inventory=Get-Content (Join-Path $package 'tests/fixtures/codex-models.json') -Raw|ConvertFrom-Json -AsHashtable
            if($family -eq 'Antigravity'){
                $inventory.models[0].model='claude-opus-fixture-thinking'
                $inventory.models[1].model='gemini-fixture-flash-medium'
            }
        } else {
            $inventory=@{provider='fixture-provider';models=@(@{
                id='gpt-99';value='gpt-99';supportsEffort=$true;supportedEffortLevels=@('high','medium')
                capabilities=@{supports=@{reasoningEffort=$true};reasoning=$true}
                supportedReasoningEfforts=@('high','medium');policy=@{state='enabled'};variants=@{high=@{};medium=@{}}
            })}
        }
        $fixture=Join-Path $temporary 'inventory.json'
        $inventory|ConvertTo-Json -Depth 20|Set-Content $fixture
        $config=Join-Path $temporary "$family.json"
        $arguments=@{Action='Status';Harness=$family;ConfigPath=$config;DiscoveryFixture=$fixture;OutputFormat='Json'}
        $status=& (Join-Path $root 'scripts/resolve-model-routing.ps1') @arguments
        Assert-Installation ($LASTEXITCODE -eq 2 -and -not(Test-Path $config)) "$family root status wrote or failed."
        $arguments.Action='Refresh'
        $refresh=& (Join-Path $root 'scripts/resolve-model-routing.ps1') @arguments
        Assert-Installation ($LASTEXITCODE -eq 0) "$family root refresh failed: $refresh"
        if($family -in @('Claude','Copilot','OpenCode')){
            $profileData=Get-Content $config -Raw|ConvertFrom-Json -AsHashtable
            foreach($runnerProfileData in $profileData.profiles.Values){
                Assert-Installation ($runnerProfileData.executable -ceq 'pwsh' -and '{outputFile}' -cin $runnerProfileData.arguments) "$family lacks structured result transport."
            }
        }
        $before=(Get-FileHash $config).Hash
        $arguments.Action='Status'
        $rootStatus=& (Join-Path $root 'scripts/resolve-model-routing.ps1') @arguments
        Assert-Installation ($LASTEXITCODE -eq 0) "$family root status not aligned."
        $presetStatus=& (Join-Path $package 'scripts/resolve-model-routing.ps1') @arguments
        Assert-Installation ($LASTEXITCODE -eq 0 -and ($rootStatus -join "`n") -ceq ($presetStatus -join "`n")) "$family root/preset status differs."
        Assert-Installation ((Get-FileHash $config).Hash -ceq $before) "$family status changed profile."
        $data=$status -join "`n"|ConvertFrom-Json -AsHashtable
        Assert-Installation ($data.roles.Count -eq 4) "$family did not expose four roles."
    }
    Write-Output "PASS: $script:checks installed runtime-closure/root-preset parity assertions; synthetic and profile-local only."
} finally {
    Remove-Item $temporary -Recurse -Force -ErrorAction SilentlyContinue
    foreach($name in @('codex','agy','claude','copilot','opencode')){Remove-Item "Function:\global:$name" -ErrorAction SilentlyContinue}
}
