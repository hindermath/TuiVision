#Requires -Version 7.0
<#
.SYNOPSIS
    Fuehrt 13 reale autonome Spec-Kit-Smoke-Worker nativ auf macOS aus.

.DESCRIPTION
    Initialisiert ein temporaeres Git- und Spec-Kit-Projekt mit Codex-Skills,
    installiert Autonomous Run Governance und dieses Preset im Entwicklungsmodus
    und fuehrt vier lokale Kampagnen aus: ReplicatedTargets,
    IndependentFeatures, AlternativeSolutions und Pipeline. Es werden keine
    Remotes angelegt oder beschrieben. Der Lauf dokumentiert den ausdruecklichen
    Entwicklungs-Override fuer native macOS-Ausfuehrung.

    Initializes a temporary Git and Spec Kit project with Codex skills, installs
    Autonomous Run Governance and this preset in development mode, and executes
    four local campaigns: ReplicatedTargets, IndependentFeatures,
    AlternativeSolutions, and Pipeline. No remotes are created or modified. The
    run records the explicit development override for native macOS execution.

.PARAMETER AutonomousPresetPath
    Lokaler Pfad zum Autonomous-Run-Governance-Preset v0.2.2 oder neuer.

    Local path to Autonomous Run Governance preset v0.2.2 or newer.

.PARAMETER OutputRoot
    Optionales persistentes Ausgabeverzeichnis. Ohne Angabe wird ein
    temporaeres Verzeichnis verwendet und am Ende ausgegeben.

    Optional persistent output directory. When omitted, a temporary directory
    is used and printed at completion.

.PARAMETER KeepRuntime
    Behaelt Laufzeit-Worktrees und Logs. Ohne diesen Schalter bleiben die
    Artefakte ebenfalls bis zur normalen Betriebssystembereinigung erhalten;
    der Schalter dokumentiert die bewusste Evidenzaufbewahrung.

    Retains runtime worktrees and logs. Without this switch artifacts also
    remain until normal operating-system cleanup; the switch records deliberate
    evidence retention.

.EXAMPLE
    pwsh -NoProfile -File tests/test-native-agent-campaigns.ps1 `
      -AutonomousPresetPath ~/spec-kit-preset-autonomous-run-governance-tmp

.NOTES
    Nur fuer den am 2026-07-18 vom Repository-Eigentuemer autorisierten
    Entwicklungs-Smoke auf macOS. Dies ist keine allgemeine Ausnahme von
    Container-First.

    Only for the macOS development smoke explicitly authorized by the repository
    owner on 2026-07-18. This is not a general Container-First exception.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateScript({ Test-Path -LiteralPath $_ -PathType Container })]
    [string] $AutonomousPresetPath,

    [string] $OutputRoot,

    [switch] $KeepRuntime
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Assert-NativeSmokeCondition {
    param(
        [Parameter(Mandatory)]
        [bool] $Condition,

        [Parameter(Mandatory)]
        [string] $Message
    )

    if (-not $Condition) {
        throw $Message
    }
}

function Invoke-NativeSmokeCommand {
    param(
        [Parameter(Mandatory)]
        [string] $Executable,

        [Parameter(Mandatory)]
        [string[]] $Arguments,

        [Parameter(Mandatory)]
        [string] $WorkingDirectory
    )

    Push-Location $WorkingDirectory
    try {
        & $Executable @Arguments
        if ($LASTEXITCODE -ne 0) {
            throw "'$Executable' wurde mit Exit-Code $LASTEXITCODE beendet."
        }
    } finally {
        Pop-Location
    }
}

function Get-NativeSmokeWorker {
    param(
        [Parameter(Mandatory)]
        [string] $WorkerId,

        [Parameter(Mandatory)]
        [string] $Repository,

        [Parameter(Mandatory)]
        [int] $FeatureNumber,

        [string[]] $DependsOn = @(),

        [object[]] $Handoffs = @()
    )

    return [ordered]@{
        workerId = $WorkerId
        runId = [guid]::NewGuid().ToString()
        repository = $Repository
        baseRef = 'main'
        branch = ('{0:D3}-native-parallel-smoke-{1}' -f $FeatureNumber, $WorkerId)
        featureInput = 'intakes/native-parallel-smoke.md'
        dependsOn = @($DependsOn)
        handoffs = @($Handoffs)
    }
}

function Get-NativeSmokeCampaign {
    param(
        [Parameter(Mandatory)]
        [string] $Name,

        [Parameter(Mandatory)]
        [string] $Topology,

        [Parameter(Mandatory)]
        [object[]] $Workers
    )

    return [ordered]@{
        schemaVersion = '1.0'
        campaignId = [guid]::NewGuid().ToString()
        name = $Name
        topology = $Topology
        deliveryMode = 'LocalImplementation'
        maxConcurrency = 3
        runnerProfile = 'native-codex'
        requireAutonomousPreset = $true
        workers = @($Workers)
        consolidation = [ordered]@{
            allReadyBarrier = $true
            mergeOrder = @($Workers.workerId)
            humanSelectionRequired = $Topology -eq 'AlternativeSolutions'
        }
    }
}

Assert-NativeSmokeCondition $IsMacOS 'Dieser explizite native Smoke-Test ist nur fuer macOS freigegeben.'
foreach ($command in @('git', 'specify', 'codex', 'pwsh')) {
    Assert-NativeSmokeCondition ($null -ne (Get-Command $command -ErrorAction SilentlyContinue)) "Erforderliches CLI fehlt: $command"
}

$presetRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$autonomousRoot = [IO.Path]::GetFullPath($AutonomousPresetPath)
if ([string]::IsNullOrWhiteSpace($OutputRoot)) {
    $OutputRoot = Join-Path ([IO.Path]::GetTempPath()) "parallel-native-agent-smoke-$([guid]::NewGuid())"
}
$output = [IO.Path]::GetFullPath($OutputRoot)
$repository = Join-Path $output 'repository'
$campaignRoot = Join-Path $output 'campaigns'
$runtimeRoot = Join-Path $output 'runtime'
[void](New-Item -ItemType Directory -Path $repository, $campaignRoot, $runtimeRoot -Force)

Invoke-NativeSmokeCommand git @('init', '-b', 'main') $repository
Invoke-NativeSmokeCommand specify @(
    'init', '--here', '--force', '--integration', 'codex',
    '--integration-options=--skills', '--ignore-agent-tools'
) $repository
Invoke-NativeSmokeCommand specify @('preset', 'add', '--dev', $autonomousRoot, '--priority', '70') $repository
Invoke-NativeSmokeCommand specify @('preset', 'add', '--dev', $presetRoot, '--priority', '80') $repository

$intakeDirectory = Join-Path $repository 'intakes'
[void](New-Item -ItemType Directory -Path $intakeDirectory -Force)
@'
# Native Parallel Autonomous Smoke Feature

Create a minimal documentation-only feature that proves a complete autonomous
Spec Kit lifecycle. Stay on the already assigned feature branch. Do not create
or switch branches. Do not perform remote operations.

The implementation must create `native-parallel-smoke-result.md` containing:

- the campaign, worker, and run identifiers from the orchestration prompt;
- a one-sentence statement that this was a native macOS development smoke;
- no secret, credential, personal path, or machine-specific value.

Keep every artifact concise. Run applicable local validation and complete the
autonomous state contract. If an outgoing handoff is declared by the
orchestration prompt, create that exact JSON file with a short portable summary.
'@ | Set-Content -LiteralPath (Join-Path $intakeDirectory 'native-parallel-smoke.md') -Encoding utf8NoBOM

Invoke-NativeSmokeCommand git @('add', '--all') $repository
Invoke-NativeSmokeCommand git @('commit', '-m', 'test: initialize native autonomous smoke fixture') $repository

$profiles = [ordered]@{
    schemaVersion = '1.0'
    profiles = [ordered]@{
        'native-codex' = [ordered]@{
            executable = 'codex'
            arguments = @(
                'exec', '--ephemeral', '-c', 'model_reasoning_effort="medium"',
                '--cd', '{worktree}',
                '--sandbox', 'workspace-write', '{prompt}'
            )
        }
    }
    mergeProfiles = [ordered]@{}
}
$profilePath = Join-Path $output 'parallel-runner-profiles.json'
$profiles | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath $profilePath -Encoding utf8NoBOM

$replicated = 1..3 | ForEach-Object {
    Get-NativeSmokeWorker "replicated-0$_" $repository $_
}
$independent = 1..3 | ForEach-Object {
    Get-NativeSmokeWorker "independent-0$_" $repository ($_ + 3)
}
$alternatives = 1..3 | ForEach-Object {
    Get-NativeSmokeWorker "alternative-0$_" $repository ($_ + 6)
}
$pipelineA = Get-NativeSmokeWorker 'pipeline-a' $repository 10 -Handoffs @(
    [ordered]@{ consumerWorkerId = 'pipeline-b'; path = 'handoffs/a-to-b.json' },
    [ordered]@{ consumerWorkerId = 'pipeline-c'; path = 'handoffs/a-to-c.json' }
)
$pipelineB = Get-NativeSmokeWorker 'pipeline-b' $repository 11 -DependsOn @('pipeline-a') -Handoffs @(
    [ordered]@{ consumerWorkerId = 'pipeline-d'; path = 'handoffs/b-to-d.json' }
)
$pipelineC = Get-NativeSmokeWorker 'pipeline-c' $repository 12 -DependsOn @('pipeline-a') -Handoffs @(
    [ordered]@{ consumerWorkerId = 'pipeline-d'; path = 'handoffs/c-to-d.json' }
)
$pipelineD = Get-NativeSmokeWorker 'pipeline-d' $repository 13 -DependsOn @('pipeline-b', 'pipeline-c')

$campaigns = @(
    Get-NativeSmokeCampaign 'native-replicated-smoke' 'ReplicatedTargets' $replicated
    Get-NativeSmokeCampaign 'native-independent-smoke' 'IndependentFeatures' $independent
    Get-NativeSmokeCampaign 'native-alternative-smoke' 'AlternativeSolutions' $alternatives
    Get-NativeSmokeCampaign 'native-pipeline-smoke' 'Pipeline' @($pipelineA, $pipelineB, $pipelineC, $pipelineD)
)

$coordinator = Join-Path $presetRoot 'scripts/orchestrate-parallel-autonomous-runs.ps1'
$summaries = @()
foreach ($campaign in $campaigns) {
    $campaignDirectory = Join-Path $campaignRoot ([string] $campaign.name)
    [void](New-Item -ItemType Directory -Path $campaignDirectory -Force)
    $manifestPath = Join-Path $campaignDirectory 'parallel-campaign.json'
    $statePath = Join-Path $campaignDirectory 'parallel-campaign-state.json'
    $campaignRuntime = Join-Path $runtimeRoot ([string] $campaign.name)
    $campaign | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath $manifestPath -Encoding utf8NoBOM

    & pwsh -NoProfile -File $coordinator -Action Start -Manifest $manifestPath `
        -RunnerConfig $profilePath -State $statePath -RuntimeRoot $campaignRuntime
    Assert-NativeSmokeCondition ($LASTEXITCODE -eq 0) "Campaign fehlgeschlagen: $($campaign.name)"

    if ($campaign.topology -eq 'AlternativeSolutions') {
        & pwsh -NoProfile -File $coordinator -Action Consolidate -Manifest $manifestPath `
            -RunnerConfig $profilePath -State $statePath -RuntimeRoot $campaignRuntime `
            -SelectedWorker 'alternative-01'
        Assert-NativeSmokeCondition ($LASTEXITCODE -eq 0) 'Alternative-Auswahl konnte nicht konsolidiert werden.'
    }

    $state = Get-Content -LiteralPath $statePath -Raw | ConvertFrom-Json -AsHashtable
    $summaries += [ordered]@{
        campaignId = [string] $campaign.campaignId
        name = [string] $campaign.name
        topology = [string] $campaign.topology
        status = [string] $state.status
        configuredConcurrency = [int] $campaign.maxConcurrency
        maximumObservedConcurrency = [int] $state.maximumObservedConcurrency
        workers = @($state.workers | ForEach-Object {
            [ordered]@{
                workerId = [string] $_.workerId
                runId = [string] $_.runId
                status = [string] $_.status
                headSha = [string] $_.headSha
            }
        })
    }
}

$summary = [ordered]@{
    schemaVersion = '1.0'
    executionEnvironment = 'native-macOS'
    policyOverride = [ordered]@{
        authorizer = 'Thorsten Hindermann'
        authorizationDate = '2026-07-18'
        scope = '13-worker parallel autonomous preset development smoke'
        expires = 'When smoke findings are captured'
    }
    agentFamily = 'Codex'
    remoteWrites = $false
    workerCount = 13
    keepRuntime = [bool] $KeepRuntime
    outputRoot = $output
    campaigns = @($summaries)
}
$summaryPath = Join-Path $output 'native-agent-smoke-summary.json'
$summary | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath $summaryPath -Encoding utf8NoBOM

Write-Output "PASS: 13 native real-agent workers completed."
Write-Output "Evidence: $summaryPath"
