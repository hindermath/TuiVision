#Requires -Version 7.0
<#
.SYNOPSIS
    Bereitet den nativen Secure-CaseTracker-Feldtest vor und fuehrt ihn aus.

.DESCRIPTION
    DE: Erzeugt eine reproduzierbare Pipeline-Kampagne fuer Units 00 bis 03 in
    sechs MSL-Sprachrepositories. Die 24 autonomen Worker laufen mit maximal
    drei gleichzeitigen Prozessen nativ auf dem Entwicklungs-Mac. Das Skript
    dokumentiert die ausdruecklichen Entwicklungs-Overrides fuer native
    Ausfuehrung und GitHub-Billing-Startfehler.

    EN: Creates a reproducible pipeline campaign for Units 00 through 03 in six
    MSL language repositories. The 24 autonomous workers run with at most three
    concurrent processes natively on the development Mac. The script records
    the explicit development overrides for native execution and GitHub billing
    startup failures.

.PARAMETER Action
    Prepare, Validate, Start, Status, Stop, Resume oder Consolidate.

.PARAMETER RepositoryRoot
    Elternverzeichnis der sechs securecasetracker-* Repositories.

.PARAMETER OutputRoot
    Lokales, nicht versioniertes Verzeichnis fuer Manifest, Runner und Zustand.

.PARAMETER Merge
    Fuehrt bei Consolidate die geordnete PR-Zusammenfuehrung mit dem
    autorisierten Admin-Bypass aus und synchronisiert danach alle main-Branches.

.PARAMETER Force
    Ersetzt bei Prepare vorhandene Kampagnenartefakte. Laufzeitdaten und
    Repository-Branches werden nicht geloescht.

.EXAMPLE
    pwsh -NoProfile -File tests/run-native-secure-casetracker-field.ps1 -Action Prepare

.EXAMPLE
    pwsh -NoProfile -File tests/run-native-secure-casetracker-field.ps1 -Action Start

.EXAMPLE
    pwsh -NoProfile -File tests/run-native-secure-casetracker-field.ps1 -Action Consolidate -Merge
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Mandatory)]
    [ValidateSet('Prepare', 'Validate', 'Start', 'Status', 'Stop', 'Resume', 'Consolidate')]
    [string] $Action,

    [string] $RepositoryRoot = (Join-Path ([Environment]::GetFolderPath('UserProfile')) 'secure-casetracker-baseline'),
    [string] $OutputRoot = (Join-Path ([Environment]::GetFolderPath('UserProfile')) '.specify/parallel-runs/secure-casetracker-native-field-20260718'),
    [switch] $Merge,
    [switch] $Force
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$presetRoot = Split-Path -Parent $PSScriptRoot
$coordinator = Join-Path $presetRoot 'scripts/orchestrate-parallel-autonomous-runs.ps1'
$manifestPath = Join-Path $OutputRoot 'parallel-campaign.json'
$runnerPath = Join-Path $OutputRoot 'parallel-runner-profiles.json'
$statePath = Join-Path $OutputRoot 'parallel-campaign-state.json'
$runtimeRoot = Join-Path $OutputRoot 'runtime'

$languages = @(
    [ordered]@{ Id = 'csharp'; Repository = 'securecasetracker-csharp' },
    [ordered]@{ Id = 'go'; Repository = 'securecasetracker-go' },
    [ordered]@{ Id = 'java'; Repository = 'securecasetracker-java' },
    [ordered]@{ Id = 'python'; Repository = 'securecasetracker-python' },
    [ordered]@{ Id = 'rust'; Repository = 'securecasetracker-rust' },
    [ordered]@{ Id = 'swift'; Repository = 'securecasetracker-swift' }
)

$unitFiles = @(
    'Lastenheft_Secure-CaseTracker_00_Sprachrepo-Projekt-Scaffold.md',
    'Lastenheft_Secure-CaseTracker_01_Kundenauftrag-und-Scope.md',
    'Lastenheft_Secure-CaseTracker_02_Domaenenmodell-und-Zustaende.md',
    'Lastenheft_Secure-CaseTracker_03_Eingabevalidierung-und-Trust-Boundaries.md'
)

function Assert-FieldCondition {
    param(
        [Parameter(Mandatory)][bool] $Condition,
        [Parameter(Mandatory)][string] $Message
    )

    if (-not $Condition) {
        throw $Message
    }
}

function Invoke-FieldGit {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string[]] $Arguments
    )

    $output = @(& git -C $Repository @Arguments 2>&1)
    Assert-FieldCondition ($LASTEXITCODE -eq 0) "git $($Arguments -join ' ') fehlgeschlagen in ${Repository}: $($output -join [Environment]::NewLine)"
    return @($output)
}

function Write-FieldJson {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Writes only explicitly requested local field-campaign artifacts.')]
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)] $Value
    )

    $Value | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Test-FieldRepositories {
    foreach ($language in $languages) {
        $repository = Join-Path $RepositoryRoot $language.Repository
        Assert-FieldCondition (Test-Path -LiteralPath $repository -PathType Container) "Repository fehlt: $repository"
        $branch = @(Invoke-FieldGit $repository @('branch', '--show-current'))[0]
        Assert-FieldCondition ($branch -eq 'main') "Repository steht nicht auf main: $repository"
        $status = @(Invoke-FieldGit $repository @('status', '--porcelain'))
        Assert-FieldCondition ($status.Count -eq 0) "Repository ist nicht sauber: $repository"
        $head = @(Invoke-FieldGit $repository @('rev-parse', 'HEAD'))[0]
        $remoteHead = @(Invoke-FieldGit $repository @('rev-parse', 'origin/main'))[0]
        Assert-FieldCondition ($head -eq $remoteHead) "main ist nicht mit origin/main synchron: $repository"

        foreach ($unitFile in $unitFiles) {
            Assert-FieldCondition (Test-Path -LiteralPath (Join-Path $repository $unitFile) -PathType Leaf) "Unit-Datei fehlt in ${repository}: $unitFile"
        }

        $parallelPreset = Join-Path $repository '.specify/presets/parallel-autonomous-run-governance/preset.yml'
        $autonomousPreset = Join-Path $repository '.specify/presets/autonomous-run-governance/preset.yml'
        Assert-FieldCondition (Test-Path -LiteralPath $parallelPreset -PathType Leaf) "Parallel-Preset fehlt: $repository"
        Assert-FieldCondition (Test-Path -LiteralPath $autonomousPreset -PathType Leaf) "Autonomous-Preset fehlt: $repository"
        $parallelPresetContent = Get-Content -LiteralPath $parallelPreset -Raw
        $autonomousPresetContent = Get-Content -LiteralPath $autonomousPreset -Raw
        Assert-FieldCondition ($parallelPresetContent -match 'version:\s*"(?<Version>[^"]+)"') "Parallel-Preset-Version fehlt: $repository"
        Assert-FieldCondition ([version] $Matches.Version -ge [version] '0.1.1') "Parallel-Preset >= v0.1.1 fehlt: $repository"
        Assert-FieldCondition ($autonomousPresetContent -match 'version:\s*"(?<Version>[^"]+)"') "Autonomous-Preset-Version fehlt: $repository"
        Assert-FieldCondition ([version] $Matches.Version -ge [version] '0.2.2') "Autonomous-Preset >= v0.2.2 fehlt: $repository"
    }
}

function New-FieldCampaign {
    $workers = [Collections.Generic.List[object]]::new()
    foreach ($language in $languages) {
        $repository = Join-Path $RepositoryRoot $language.Repository
        for ($unit = 0; $unit -lt $unitFiles.Count; $unit++) {
            $workerId = '{0}-unit-{1:D2}' -f $language.Id, $unit
            $branch = '{0:D3}-securecasetracker-{1}-unit-{2:D2}' -f (100 + $unit), $language.Id, $unit
            $previousWorkerId = if ($unit -eq 0) { $null } else { '{0}-unit-{1:D2}' -f $language.Id, ($unit - 1) }
            $dependencies = @()
            if ($null -ne $previousWorkerId) {
                $dependencies = @($previousWorkerId)
            }
            $handoffs = @()
            if ($unit -lt ($unitFiles.Count - 1)) {
                $consumerId = '{0}-unit-{1:D2}' -f $language.Id, ($unit + 1)
                $handoffs = @(
                    [ordered]@{
                        consumerWorkerId = $consumerId
                        path = "specs/$branch/handoffs/$workerId-to-$consumerId.json"
                    }
                )
            }
            $workers.Add([ordered]@{
                workerId = $workerId
                runId = [guid]::NewGuid().ToString()
                repository = $repository
                baseRef = if ($unit -eq 0) { 'main' } else { $null }
                baseWorkerId = $previousWorkerId
                branch = $branch
                featureInput = $unitFiles[$unit]
                dependsOn = $dependencies
                handoffs = $handoffs
            })
        }
    }

    return [ordered]@{
        schemaVersion = '1.0'
        campaignId = [guid]::NewGuid().ToString()
        name = 'secure-casetracker-units-00-03-native-macos-field'
        topology = 'Pipeline'
        deliveryMode = 'MergeAndSync'
        maxConcurrency = 3
        runnerProfile = 'native-codex-field'
        requireAutonomousPreset = $true
        operatorInstructions = @'
Repository owner Thorsten Hindermann explicitly authorized this development field worker to run natively on the macOS development host, overriding the intake's Container-First gate only for this campaign. Record the native execution override in feature evidence.
The worker has PublishPR authority: perform the complete installed speckit.autonomous run for the assigned Unit, run every applicable local quality gate, commit, push, and create the pull request. Do not merge and do not use admin bypass.
For this campaign only, a GitHub Actions job that is rejected before its first step with the exact account-payment or spending-limit annotation may be recorded as an external provider N/A. It is not passing technical evidence. Local and agent-side build, test, security, accessibility, documentation, state, and evidence gates remain mandatory. Any technical CI failure, actionable review, missing evidence, or differently unavailable check remains blocking.
Do not include secrets or personal data. Do not start any later Unit; the campaign schedules it separately after the validated handoff.
'@
        workers = @($workers)
        consolidation = [ordered]@{
            allReadyBarrier = $true
            mergeOrder = @($workers | ForEach-Object { $_.workerId })
            humanSelectionRequired = $false
            mergeProfile = 'github-owner-development'
        }
    }
}

function New-FieldRunnerProfiles {
    return [ordered]@{
        schemaVersion = '1.0'
        profiles = [ordered]@{
            'native-codex-field' = [ordered]@{
                executable = 'codex'
                arguments = @(
                    'exec',
                    '--ephemeral',
                    '--cd', '{worktree}',
                    '--add-dir', '{resultDirectory}',
                    '--sandbox', 'danger-full-access',
                    '{prompt}'
                )
            }
        }
        mergeProfiles = [ordered]@{
            'github-owner-development' = [ordered]@{
                executable = 'gh'
                arguments = @('pr', 'merge', '{prUrl}', '--merge', '--delete-branch', '--admin')
            }
        }
    }
}

Assert-FieldCondition $IsMacOS 'Der ausdruecklich freigegebene native Feldtest darf nur auf macOS laufen.'
Assert-FieldCondition (Test-Path -LiteralPath $coordinator -PathType Leaf) "Coordinator fehlt: $coordinator"
Assert-FieldCondition ([bool](Get-Command git -ErrorAction SilentlyContinue)) 'git fehlt.'
Assert-FieldCondition ([bool](Get-Command gh -ErrorAction SilentlyContinue)) 'gh fehlt.'
Assert-FieldCondition ([bool](Get-Command codex -ErrorAction SilentlyContinue)) 'codex fehlt.'

if ($Action -eq 'Prepare') {
    Test-FieldRepositories
    if ((Test-Path -LiteralPath $manifestPath) -or (Test-Path -LiteralPath $runnerPath)) {
        Assert-FieldCondition $Force "Kampagnenartefakte existieren bereits: $OutputRoot. Fuer bewusstes Ersetzen -Force verwenden."
    }
    if ($PSCmdlet.ShouldProcess($OutputRoot, 'Create native Secure CaseTracker field campaign')) {
        [void](New-Item -ItemType Directory -Path $OutputRoot -Force)
        Write-FieldJson $manifestPath (New-FieldCampaign)
        Write-FieldJson $runnerPath (New-FieldRunnerProfiles)
    }
    Write-Output "Prepared: $manifestPath"
    exit 0
}

Assert-FieldCondition (Test-Path -LiteralPath $manifestPath -PathType Leaf) "Manifest fehlt. Zuerst -Action Prepare ausfuehren: $manifestPath"
Assert-FieldCondition (Test-Path -LiteralPath $runnerPath -PathType Leaf) "Runner-Konfiguration fehlt: $runnerPath"

if ($Action -in @('Validate', 'Start')) {
    Test-FieldRepositories
}

$coordinatorArguments = @(
    '-NoProfile', '-File', $coordinator,
    '-Action', $Action,
    '-Manifest', $manifestPath,
    '-RunnerConfig', $runnerPath,
    '-State', $statePath,
    '-RuntimeRoot', $runtimeRoot
)
if ($Action -eq 'Consolidate' -and $Merge) {
    $coordinatorArguments += '-Merge'
}

& pwsh @coordinatorArguments
Assert-FieldCondition ($LASTEXITCODE -eq 0) "Coordinator-Aktion fehlgeschlagen: $Action"

if ($Action -eq 'Consolidate' -and $Merge) {
    foreach ($language in $languages) {
        $repository = Join-Path $RepositoryRoot $language.Repository
        [void](Invoke-FieldGit $repository @('switch', 'main'))
        [void](Invoke-FieldGit $repository @('pull', '--ff-only'))
        $status = @(Invoke-FieldGit $repository @('status', '--porcelain'))
        Assert-FieldCondition ($status.Count -eq 0) "Repository ist nach Merge nicht sauber: $repository"
    }
}
