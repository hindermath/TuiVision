#Requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$presetRoot = Split-Path -Parent $PSScriptRoot
$coordinator = Join-Path $presetRoot 'scripts/orchestrate-parallel-autonomous-runs.ps1'
$fixtureWorker = Join-Path $PSScriptRoot 'fixture-worker.ps1'
$fixtureProvider = Join-Path $PSScriptRoot 'fixture-provider.ps1'
$fixturePostMerge = Join-Path $PSScriptRoot 'fixture-post-merge.ps1'
$tempRoot = Join-Path ([IO.Path]::GetTempPath()) "parallel-autonomous-v2-tests-$([guid]::NewGuid())"
[void](New-Item -ItemType Directory -Path $tempRoot)
$env:PAR_TEST_FIXTURE_ROOT = $tempRoot

function Assert-Test {
    param([bool] $Condition, [string] $Message)

    if (-not $Condition) {
        throw $Message
    }
}

function Write-TestJson {
    param([Parameter(Mandatory)][string] $Path, [Parameter(Mandatory)] $Value)

    $Value | ConvertTo-Json -Depth 30 |
        Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Invoke-TestGit {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string[]] $Arguments
    )

    $output = @(& git -C $Repository @Arguments 2>&1)
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Arguments -join ' ') failed: $(@($output) -join [Environment]::NewLine)"
    }
    return @($output)
}

function Initialize-TestRepository {
    param([Parameter(Mandatory)][string] $Name)

    $path = Join-Path $tempRoot $Name
    [void](New-Item -ItemType Directory -Path $path)
    [void](Invoke-TestGit $path @('init', '-b', 'main'))
    [void](Invoke-TestGit $path @('config', 'user.name', 'Parallel V2 Fixture'))
    [void](Invoke-TestGit $path @('config', 'user.email', 'parallel-v2@example.invalid'))
    "# $Name" | Set-Content -LiteralPath (Join-Path $path 'README.md') -Encoding utf8NoBOM
    [void](Invoke-TestGit $path @('add', '--', 'README.md'))
    [void](Invoke-TestGit $path @('commit', '-m', 'test: baseline'))
    return $path
}

function New-TestWorker {
    param(
        [Parameter(Mandatory)][string] $WorkerId,
        [Parameter(Mandatory)][string] $Repository,
        [AllowNull()][string] $RunnerProfile
    )

    $worker = [ordered]@{
        workerId = $WorkerId
        runId = [guid]::NewGuid().ToString()
        repository = $Repository
        baseRef = 'main'
        baseWorkerId = $null
        branch = "parallel/v2/$([guid]::NewGuid().ToString('N'))/$WorkerId"
        featureInput = 'README.md'
        dependsOn = @()
        handoffs = @()
    }
    if (-not [string]::IsNullOrWhiteSpace($RunnerProfile)) {
        $worker.runnerProfile = $RunnerProfile
    }
    return $worker
}

function New-TestCase {
    param(
        [Parameter(Mandatory)][string] $Name,
        [int] $WorkerCount = 1,
        [switch] $SharedRepository,
        [switch] $Stacked,
        [switch] $Alternative,
        [string[]] $RunnerProfiles = @(),
        [int] $SyncFailures = 0
    )

    $caseRoot = Join-Path $tempRoot $Name
    [void](New-Item -ItemType Directory -Path $caseRoot)
    $repositories = [Collections.Generic.List[string]]::new()
    $shared = if ($SharedRepository) { Initialize-TestRepository "$Name-shared" } else { $null }
    $workers = [Collections.Generic.List[object]]::new()
    for ($index = 0; $index -lt $WorkerCount; $index++) {
        $repository = if ($SharedRepository) {
            $shared
        } else {
            Initialize-TestRepository "$Name-repo-$index"
        }
        if (-not $repositories.Contains($repository)) {
            $repositories.Add($repository)
        }
        $runnerProfile = if ($index -lt $RunnerProfiles.Count) { $RunnerProfiles[$index] } else { $null }
        $workers.Add((New-TestWorker "worker-$('{0:D2}' -f $index)" $repository $runnerProfile))
    }
    if ($Stacked) {
        Assert-Test $SharedRepository 'Stacked fixture requires one shared repository'
        for ($index = 1; $index -lt $workers.Count; $index++) {
            $workers[$index].baseWorkerId = [string] $workers[$index - 1].workerId
            $workers[$index].dependsOn = @([string] $workers[$index - 1].workerId)
            $workers[$index - 1].handoffs = @([ordered]@{
                consumerWorkerId = [string] $workers[$index].workerId
                path = "handoff-$($workers[$index - 1].workerId)-$($workers[$index].workerId).json"
            })
        }
    }

    $providerStatePath = Join-Path $caseRoot 'provider-state.json'
    $postMergeStatePath = Join-Path $caseRoot 'post-merge-state.json'
    $manifestPath = Join-Path $caseRoot 'campaign.json'
    $runnerPath = Join-Path $caseRoot 'runners.json'
    $statePath = Join-Path $caseRoot 'state.json'
    $runtimePath = Join-Path $caseRoot 'runtime'
    $firstWorkerId = [string] $workers[0].workerId
    $campaign = [ordered]@{
        schemaVersion = '1.1'
        campaignId = [guid]::NewGuid().ToString()
        name = $Name
        topology = if ($Alternative) { 'AlternativeSolutions' } else { 'ReplicatedTargets' }
        deliveryMode = 'MergeAndSync'
        maxConcurrency = 3
        runnerProfile = 'fixture-default'
        requireAutonomousPreset = $false
        operatorInstructions = 'V2 deterministic fixture.'
        workers = @($workers)
        consolidation = [ordered]@{
            allReadyBarrier = $true
            mergeOrder = @($workers | ForEach-Object workerId)
            humanSelectionRequired = [bool]$Alternative
            mergeProfile = 'fixture-provider'
        }
        postMergeActions = @(
            [ordered]@{
                actionId = 'sync-main'
                workerId = $firstWorkerId
                phase = 'Synchronize'
                profile = 'fixture-post-merge'
            },
            [ordered]@{
                actionId = 'closeout'
                workerId = $firstWorkerId
                phase = 'PostMerge'
                profile = 'fixture-post-merge'
            },
            [ordered]@{
                actionId = 'validate-main'
                workerId = $firstWorkerId
                phase = 'Validate'
                profile = 'fixture-post-merge'
            }
        )
    }
    $runnerArguments = @(
        '-NoProfile', '-File', $fixtureWorker,
        '-CampaignId', '{campaignId}',
        '-WorkerId', '{workerId}',
        '-RunId', '{runId}',
        '-AttemptId', '{attemptId}',
        '-Worktree', '{worktree}',
        '-ResultFile', '{resultFile}',
        '-Mode', 'ReadyForMerge',
        '-HandoffsJson', '{handoffsJson}',
        '-DelayMilliseconds', '30'
    )
    $runnerConfig = [ordered]@{
        schemaVersion = '1.1'
        profiles = [ordered]@{
            'fixture-default' = [ordered]@{
                agentFamily = 'Fixture Agent'
                executable = 'pwsh'
                arguments = $runnerArguments
                privateNote = 'SENSITIVE-RUNNER-DATA-MUST-NOT-LEAK'
            }
            'fixture-codex' = [ordered]@{
                agentFamily = 'Codex'
                model = 'explicit-fixture-model'
                reasoningEffort = 'high'
                executable = 'pwsh'
                arguments = $runnerArguments
            }
            'fixture-claude' = [ordered]@{
                agentFamily = 'Claude Code'
                executable = 'pwsh'
                arguments = $runnerArguments
            }
        }
        mergeProfiles = [ordered]@{
            'fixture-provider' = [ordered]@{
                provider = 'Local Fixture'
                preflight = [ordered]@{
                    executable = 'pwsh'
                    arguments = @(
                        '-NoProfile', '-File', $fixtureProvider,
                        '-Mode', 'Preflight',
                        '-StateFile', $providerStatePath,
                        '-WorkerId', '{workerId}',
                        '-PrUrl', '{prUrl}',
                        '-ExpectedHead', '{headSha}',
                        '-OutputFile', '{preflightResultFile}'
                    )
                }
                merge = [ordered]@{
                    executable = 'pwsh'
                    arguments = @(
                        '-NoProfile', '-File', $fixtureProvider,
                        '-Mode', 'Merge',
                        '-StateFile', $providerStatePath,
                        '-WorkerId', '{workerId}',
                        '-PrUrl', '{prUrl}',
                        '-ExpectedHead', '{headSha}'
                    )
                }
            }
        }
        postMergeProfiles = [ordered]@{
            'fixture-post-merge' = [ordered]@{
                idempotent = $true
                executable = 'pwsh'
                arguments = @(
                    '-NoProfile', '-File', $fixturePostMerge,
                    '-StateFile', $postMergeStatePath,
                    '-ActionId', '{actionId}',
                    '-Repository', '{repository}',
                    '-ExpectedHead', '{headSha}'
                )
            }
        }
    }
    if ($Alternative) {
        $campaign.postMergeActions += [ordered]@{
            actionId = 'validate-unselected'; workerId = 'worker-01'; phase = 'Validate'; profile = 'fixture-post-merge'
        }
    }
    Write-TestJson $manifestPath $campaign
    Write-TestJson $runnerPath $runnerConfig
    Write-TestJson $providerStatePath ([ordered]@{ schemaVersion = '1.1'; workers = [ordered]@{} })
    $postMergeFixture = [ordered]@{
        actions = [ordered]@{
            'sync-main' = [ordered]@{ attemptCount = 0; failuresRemaining = $SyncFailures; succeeded = $false }
            closeout = [ordered]@{ attemptCount = 0; failuresRemaining = 0; succeeded = $false }
            'validate-main' = [ordered]@{ attemptCount = 0; failuresRemaining = 0; succeeded = $false }
        }
    }
    if ($Alternative) {
        $postMergeFixture.actions['validate-unselected'] = [ordered]@{
            attemptCount = 0; failuresRemaining = 0; succeeded = $false
        }
    }
    Write-TestJson $postMergeStatePath $postMergeFixture

    & pwsh -NoProfile -File $coordinator -Action Validate -Manifest $manifestPath -RunnerConfig $runnerPath | Out-Null
    Assert-Test ($LASTEXITCODE -eq 0) "$Name validation failed"
    & pwsh -NoProfile -File $coordinator -Action Start -Manifest $manifestPath -RunnerConfig $runnerPath `
        -State $statePath -RuntimeRoot $runtimePath | Out-Null
    Assert-Test ($LASTEXITCODE -eq 0) "$Name start failed"
    $campaignState = Get-Content -LiteralPath $statePath -Raw | ConvertFrom-Json -AsHashtable
    $expectedStatus = if ($Alternative) { 'AwaitingSelection' } else { 'ReadyForConsolidation' }
    Assert-Test ([string] $campaignState.status -eq $expectedStatus) "$Name did not reach $expectedStatus"
    $providerState = Get-Content -LiteralPath $providerStatePath -Raw | ConvertFrom-Json -AsHashtable
    foreach ($workerState in $campaignState.workers) {
        $worker = @($campaign.workers | Where-Object workerId -eq $workerState.workerId)[0]
        $providerState.workers[[string] $workerState.workerId] = [ordered]@{
            prUrl = [string] $workerState.prUrl
            state = 'Open'
            isDraft = $false
            headSha = [string] $workerState.headSha
            headRepository = [string] $worker.repository
            headRefName = [string] $worker.branch
            baseRepository = [string] $worker.repository
            prNumber = [int] ([regex]::Match([string] $worker.workerId, '\d+$').Value) + 1
            mergeable = $true
            reviewDecision = $null
            unresolvedCurrentThreads = 0
            checkPolicySatisfied = $true
            technicalFailures = @()
            baseRefName = if ($worker.baseWorkerId) {
                [string] (@($campaign.workers | Where-Object workerId -eq $worker.baseWorkerId)[0]).branch
            } else { 'main' }
            mergeCommitSha = $null
            mergeFailuresRemaining = 0
            mergeDelayMilliseconds = 0
            postMergeDelayMilliseconds = 0
            preflightDelayMilliseconds = 0
            preflightCount = 0
        }
    }
    Write-TestJson $providerStatePath $providerState

    return @{
        Name = $Name
        Campaign = $campaign
        Manifest = $manifestPath
        Runner = $runnerPath
        State = $statePath
        Runtime = $runtimePath
        ProviderState = $providerStatePath
        PostMergeState = $postMergeStatePath
        Repositories = @($repositories)
    }
}

function Invoke-TestConsolidate {
    param([Parameter(Mandatory)][hashtable] $Case, [switch] $ExpectFailure, [string] $SelectedWorker = '')

    $selectionArgs = if ($SelectedWorker) { @('-SelectedWorker', $SelectedWorker) } else { @() }
    $consolidationOutput = @(& pwsh -NoProfile -File $coordinator -Action Consolidate -Manifest $Case.Manifest `
        -RunnerConfig $Case.Runner -State $Case.State -RuntimeRoot $Case.Runtime -Merge @selectionArgs 2>&1)
    if ($ExpectFailure) {
        Assert-Test ($LASTEXITCODE -ne 0) "$($Case.Name) unexpectedly consolidated"
    } else {
        Assert-Test ($LASTEXITCODE -eq 0) "$($Case.Name) consolidation failed: $($consolidationOutput -join ' ')"
    }
}

function Invoke-TestResume {
    param([Parameter(Mandatory)][hashtable] $Case)

    & pwsh -NoProfile -File $coordinator -Action Resume -Manifest $Case.Manifest `
        -RunnerConfig $Case.Runner -State $Case.State -RuntimeRoot $Case.Runtime -Merge 2>$null | Out-Null
    Assert-Test ($LASTEXITCODE -eq 0) "$($Case.Name) resume failed"
}

function Read-TestState {
    param([Parameter(Mandatory)][string] $Path)

    return Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -AsHashtable
}

function Update-ProviderWorker {
    param(
        [Parameter(Mandatory)][hashtable] $Case,
        [Parameter(Mandatory)][string] $WorkerId,
        [Parameter(Mandatory)][scriptblock] $Update
    )

    $providerState = Read-TestState $Case.ProviderState
    & $Update $providerState.workers[$WorkerId]
    Write-TestJson $Case.ProviderState $providerState
}

try {
    $coordinatorAst = [System.Management.Automation.Language.Parser]::ParseFile(
        $coordinator, [ref] $null, [ref] $null)
    foreach ($name in @('Assert-PARCondition', 'Assert-PARSchemaVersion', 'Read-PARJson',
            'Invoke-PARGit', 'Get-PARSha256', 'Test-PARUuid', 'Resolve-PARPhysicalPath',
            'Resolve-PARRepository', 'Get-PARRemoteRepositoryIdentity',
            'Get-PARExpectedPrIdentity', 'ConvertTo-PARArgumentList', 'Invoke-PARProcessAsync',
            'Invoke-PARProfileCommand', 'Invoke-PARProviderPreflight',
            'Confirm-PARAutonomousState', 'Confirm-PARWorkerExecutionEvidence',
            'Confirm-PARPublicationWorkerState')) {
        $definition = @($coordinatorAst.FindAll({ param($node)
            $node -is [System.Management.Automation.Language.FunctionDefinitionAst]
        }, $true) | Where-Object Name -eq $name)[0]
        . ([scriptblock]::Create($definition.Extent.Text))
    }
    $remoteRepo = Initialize-TestRepository 'remote-identity'
    [void](Invoke-TestGit $remoteRepo @('remote', 'add', 'origin', 'https://GitHub.com/Owner/Repo.git'))
    Assert-Test ((Get-PARRemoteRepositoryIdentity $remoteRepo) -ceq 'github.com/owner/repo') `
        'HTTPS origin was not canonicalized to its repository identity'
    $identityWorker = [ordered]@{ workerId = 'identity'; repository = $remoteRepo;
        branch = 'parallel/identity'; baseRef = 'origin/main'; baseWorkerId = $null }
    $identity = Get-PARExpectedPrIdentity @{ workers = @($identityWorker) } $identityWorker $tempRoot 'GitHub'
    Assert-Test ($identity.baseRefName -ceq 'main' -and
        $identity.headRepository -ceq 'github.com/owner/repo') `
        'origin/main was not bound to GitHub base branch main'
    [void](Invoke-TestGit $remoteRepo @('remote', 'set-url', 'origin', 'git@GitHub.com:Owner/Repo.git'))
    Assert-Test ((Get-PARRemoteRepositoryIdentity $remoteRepo) -ceq 'github.com/owner/repo') `
        'SSH origin was not canonicalized to its repository identity'

    $mixed = New-TestCase 'mixed-runner-fallback' -WorkerCount 3 `
        -RunnerProfiles @('fixture-codex', 'fixture-claude')
    $script:SupportedSchemaVersions = @('1.0', '1.1', '1.2')
    $publicationProfile = [ordered]@{
        provider = 'Local Fixture'
        preflight = (Read-TestState $mixed.Runner).mergeProfiles['fixture-provider'].preflight
    }
    $publicationWorker = $mixed.Campaign.workers[0]
    $publicationWorkerState = (Read-TestState $mixed.State).workers[0]
    $publication = Invoke-PARProviderPreflight $mixed.Campaign $publicationProfile `
        $publicationWorker $publicationWorkerState (Split-Path -Parent $mixed.Manifest) $mixed.Runtime `
        -PublicationOnly
    Assert-Test ($publication.state -eq 'Open') 'Read-only publication preflight rejected a valid PR'
    $publishCampaign = Read-TestState $mixed.Manifest
    $publishCampaign.deliveryMode = 'PublishPR'
    $publishCampaign.publicationProfile = 'fixture-publication'
    $publishProfiles = Read-TestState $mixed.Runner
    $publishProfiles.publicationProfiles = @{ 'fixture-publication' = $publicationProfile }
    Confirm-PARPublicationWorkerState $publishCampaign $publishProfiles $publicationWorker `
        $publicationWorkerState (Split-Path -Parent $mixed.Manifest) $mixed.Runtime
    Assert-Test ($publicationWorkerState.status -eq 'ReadyForMerge' -and
        $publicationWorkerState.providerState -eq 'Open') `
        'Published worker result was not bound to an open provider PR'
    Update-ProviderWorker $mixed 'worker-00' { param($worker) $worker.headRepository = '/foreign/repository' }
    $foreignPublicationRejected = $false
    try {
        Confirm-PARPublicationWorkerState $publishCampaign $publishProfiles $publicationWorker `
            $publicationWorkerState (Split-Path -Parent $mixed.Manifest) $mixed.Runtime
    } catch { $foreignPublicationRejected = $true }
    Assert-Test $foreignPublicationRejected 'Read-only publication preflight accepted a foreign PR'
    Update-ProviderWorker $mixed 'worker-00' {
        param($worker)
        $worker.headRepository = [string] $publicationWorker.repository
    }
    Invoke-TestConsolidate $mixed
    $mixedState = Read-TestState $mixed.State
    Assert-Test ($mixedState.status -eq 'Completed') 'Mixed runner campaign did not complete'
    Assert-Test ($mixedState.workers[0].runnerProfile -eq 'fixture-codex') 'Worker runner profile override was ignored'
    Assert-Test ($mixedState.workers[1].agentFamily -eq 'Claude Code') 'Second worker agent family was not recorded'
    Assert-Test ($mixedState.workers[2].runnerProfile -eq 'fixture-default') 'Campaign runner fallback was not applied'
    Assert-Test ($mixedState.workers[2].model -eq 'Agent-Standard/nicht deklariert') 'Undeclared model was guessed'
    Assert-Test (@($mixedState.events | Where-Object { [string]::IsNullOrWhiteSpace([string] $_.timestamp) }).Count -eq 0) `
        'State events are missing timestamps'
    $statusJson = @(& pwsh -NoProfile -File $coordinator -Action Status -Manifest $mixed.Manifest `
        -State $mixed.State -OutputFormat Json) -join [Environment]::NewLine
    $statusText = @(& pwsh -NoProfile -File $coordinator -Action Status -Manifest $mixed.Manifest `
        -State $mixed.State -OutputFormat Text) -join [Environment]::NewLine
    Assert-Test (-not $statusJson.Contains('SENSITIVE-RUNNER-DATA-MUST-NOT-LEAK')) 'JSON status leaked runner configuration'
    Assert-Test ($statusText.Contains('Agent-Standard/nicht deklariert')) 'Text status omitted undeclared model marker'
    Assert-Test (-not $statusText.Contains('SENSITIVE-RUNNER-DATA-MUST-NOT-LEAK')) 'Text status leaked runner configuration'
    $receiptTamper = Read-TestState $mixed.State
    $receiptTamper.postMergeActions[0].receiptSha256 = '0' * 64
    Write-TestJson $mixed.State $receiptTamper
    Invoke-TestConsolidate $mixed -ExpectFailure
    Assert-Test ((Read-TestState $mixed.State).status -ne 'Completed') `
        'Forged successful post-merge action completed the campaign'

    $alternative = New-TestCase 'selected-alternative-actions' -WorkerCount 2 -Alternative
    Invoke-TestConsolidate $alternative -SelectedWorker 'worker-00'
    $alternativeState = Read-TestState $alternative.State
    $alternativeActions = Read-TestState $alternative.PostMergeState
    Assert-Test ($alternativeState.status -eq 'Completed') 'Selected alternative did not complete'
    Assert-Test ((@($alternativeState.postMergeActions | Where-Object actionId -eq 'validate-unselected')[0]).status -eq 'Skipped') `
        'Unselected alternative action was not skipped'
    Assert-Test ([int] $alternativeActions.actions['validate-unselected'].attemptCount -eq 0) `
        'Unselected alternative post-merge action executed'

    $headDrift = New-TestCase 'head-drift'
    Update-ProviderWorker $headDrift 'worker-00' { param($worker) $worker.headSha = '0000000000000000000000000000000000000000' }
    Invoke-TestConsolidate $headDrift -ExpectFailure
    Assert-Test ((Read-TestState $headDrift.State).status -eq 'NeedsRevalidation') 'Head drift did not require revalidation'
    & pwsh -NoProfile -File $coordinator -Action Consolidate -Manifest $headDrift.Manifest `
        -RunnerConfig $headDrift.Runner -State $headDrift.State -RuntimeRoot $headDrift.Runtime 2>$null | Out-Null
    Assert-Test ($LASTEXITCODE -ne 0) 'Consolidation without merge hid NeedsRevalidation'
    Assert-Test ((Read-TestState $headDrift.State).status -eq 'NeedsRevalidation') `
        'Consolidation without merge overwrote NeedsRevalidation'

    $foreignPr = New-TestCase 'foreign-pr-identity'
    Update-ProviderWorker $foreignPr 'worker-00' {
        param($worker)
        $worker.headRepository = '/foreign/repository'
    }
    Invoke-TestConsolidate $foreignPr -ExpectFailure
    Assert-Test ((Read-TestState $foreignPr.State).status -eq 'NeedsRevalidation') `
        'Foreign PR head repository did not require revalidation'

    $forgedResult = New-TestCase 'forged-worker-result'
    $forgedCheckpoint = Read-TestState $forgedResult.State
    $forgedCheckpoint.workers[0].resultSha256 = '0' * 64
    Write-TestJson $forgedResult.State $forgedCheckpoint
    Invoke-TestConsolidate $forgedResult -ExpectFailure
    Assert-Test ((Read-TestState $forgedResult.State).status -eq 'ReadyForConsolidation') `
        'Forged worker result reached provider merge preflight'
    $forgedCheckpoint.status = 'PausedByUser'
    Write-TestJson $forgedResult.State $forgedCheckpoint
    & pwsh -NoProfile -File $coordinator -Action Start -Manifest $forgedResult.Manifest `
        -RunnerConfig $forgedResult.Runner -State $forgedResult.State `
        -RuntimeRoot $forgedResult.Runtime 2>$null | Out-Null
    Assert-Test ($LASTEXITCODE -ne 0 -and
        (Read-TestState $forgedResult.State).status -eq 'PausedByUser') `
        'Start skipped a forged completed worker checkpoint'

    $wrongBase = New-TestCase 'wrong-pr-base'
    Update-ProviderWorker $wrongBase 'worker-00' {
        param($worker)
        $worker.baseRefName = 'unexpected-base'
    }
    Invoke-TestConsolidate $wrongBase -ExpectFailure
    Assert-Test ((Read-TestState $wrongBase.State).status -eq 'NeedsRevalidation') `
        'PR targeting another base branch did not require revalidation'

    $openReview = New-TestCase 'open-review'
    Update-ProviderWorker $openReview 'worker-00' { param($worker) $worker.unresolvedCurrentThreads = 1 }
    Invoke-TestConsolidate $openReview -ExpectFailure
    Assert-Test ((Read-TestState $openReview.State).status -eq 'NeedsRevalidation') 'Open review did not require revalidation'

    $technicalFailure = New-TestCase 'technical-check-failure'
    Update-ProviderWorker $technicalFailure 'worker-00' {
        param($worker)
        $worker.checkPolicySatisfied = $false
        $worker.technicalFailures = @('unit-tests')
    }
    Invoke-TestConsolidate $technicalFailure -ExpectFailure
    Assert-Test ((Read-TestState $technicalFailure.State).status -eq 'NeedsRevalidation') 'Technical failure did not require revalidation'

    $manifestDrift = New-TestCase 'manifest-drift'
    $changedManifest = Read-TestState $manifestDrift.Manifest
    $changedManifest.name = 'manifest-drift-after-review'
    Write-TestJson $manifestDrift.Manifest $changedManifest
    Invoke-TestConsolidate $manifestDrift -ExpectFailure
    Assert-Test ((Read-TestState $manifestDrift.State).status -eq 'ReadyForConsolidation') `
        'Manifest drift modified the campaign state before rejection'

    $campaignMismatch = New-TestCase 'campaign-id-mismatch'
    $mismatchedState = Read-TestState $campaignMismatch.State
    $mismatchedState.campaignId = [guid]::NewGuid().ToString()
    Write-TestJson $campaignMismatch.State $mismatchedState
    & pwsh -NoProfile -File $coordinator -Action Status -Manifest $campaignMismatch.Manifest `
        -State $campaignMismatch.State -OutputFormat Text 2>$null | Out-Null
    Assert-Test ($LASTEXITCODE -ne 0) 'Status accepted a state from another campaign'
    Invoke-TestConsolidate $campaignMismatch -ExpectFailure
    Assert-Test ((Read-TestState $campaignMismatch.State).status -eq 'ReadyForConsolidation') `
        'Campaign ID mismatch modified the campaign state before rejection'

    $postMergeStateDrift = New-TestCase 'post-merge-state-contract-drift'
    $driftedPostMergeState = Read-TestState $postMergeStateDrift.State
    $driftedPostMergeState.postMergeActions[0].actionId = 'undeclared-action'
    Write-TestJson $postMergeStateDrift.State $driftedPostMergeState
    Invoke-TestConsolidate $postMergeStateDrift -ExpectFailure
    $rejectedPostMergeState = Read-TestState $postMergeStateDrift.State
    $rejectedProviderState = Read-TestState $postMergeStateDrift.ProviderState
    $rejectedActionFixture = Read-TestState $postMergeStateDrift.PostMergeState
    Assert-Test ($rejectedPostMergeState.status -eq 'ReadyForConsolidation') `
        'Post-merge state drift modified the campaign before rejection'
    Assert-Test ($rejectedProviderState.workers['worker-00'].state -eq 'Open') `
        'Post-merge state drift reached a remote merge'
    Assert-Test (@($rejectedActionFixture.actions.Values | Where-Object attemptCount -ne 0).Count -eq 0) `
        'Post-merge state drift started an undeclared action'

    $forgedSelection = New-TestCase 'forged-selection'
    $forgedSelectionState = Read-TestState $forgedSelection.State
    $forgedSelectionState.selectedWorkerId = 'worker-00'
    Write-TestJson $forgedSelection.State $forgedSelectionState
    Invoke-TestConsolidate $forgedSelection -ExpectFailure
    Assert-Test ((Read-TestState $forgedSelection.ProviderState).workers['worker-00'].state -eq 'Open') `
        'Non-alternative selection reached provider merge'

    $partial = New-TestCase 'partial-merge-resume' -WorkerCount 2
    Update-ProviderWorker $partial 'worker-01' { param($worker) $worker.mergeFailuresRemaining = 1 }
    Invoke-TestConsolidate $partial -ExpectFailure
    $partialFailedState = Read-TestState $partial.State
    Assert-Test ($partialFailedState.workers[0].status -eq 'Merged') 'First partial merge was not checkpointed'
    Assert-Test ($partialFailedState.workers[1].status -ne 'Merged') 'Failed second merge was marked merged'
    Invoke-TestResume $partial
    $partialCompletedState = Read-TestState $partial.State
    Assert-Test ($partialCompletedState.status -eq 'Completed') 'Partial merge resume did not complete'
    Assert-Test ([int] $partialCompletedState.workers[0].mergeAttempt -eq 1) 'Resume repeated an already verified merge'

    $concurrentConsolidate = New-TestCase 'concurrent-consolidate-lock'
    Update-ProviderWorker $concurrentConsolidate 'worker-00' { param($worker) $worker.mergeDelayMilliseconds = 5000 }
    $firstConsolidate = Start-Process -FilePath 'pwsh' -ArgumentList @(
        '-NoProfile', '-File', $coordinator,
        '-Action', 'Consolidate',
        '-Manifest', $concurrentConsolidate.Manifest,
        '-RunnerConfig', $concurrentConsolidate.Runner,
        '-State', $concurrentConsolidate.State,
        '-RuntimeRoot', $concurrentConsolidate.Runtime,
        '-Merge'
    ) -PassThru
    $consolidateLock = Join-Path $concurrentConsolidate.Runtime 'campaign.lock'
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    do {
        Start-Sleep -Milliseconds 100
        $consolidateLockObserved = Test-Path -LiteralPath $consolidateLock -PathType Container
    } until ($consolidateLockObserved -or [DateTime]::UtcNow -gt $deadline)
    Assert-Test $consolidateLockObserved 'Consolidate fixture did not acquire the campaign lock'
    Invoke-TestConsolidate $concurrentConsolidate -ExpectFailure
    $firstConsolidate.WaitForExit()
    Assert-Test ($firstConsolidate.ExitCode -eq 0) 'First locked consolidation failed'
    Assert-Test ((Read-TestState $concurrentConsolidate.State).status -eq 'Completed') `
        'Concurrent consolidate lock changed the completed state'

    $concurrentResume = New-TestCase 'concurrent-consolidation-resume-lock' -WorkerCount 2
    Update-ProviderWorker $concurrentResume 'worker-01' { param($worker) $worker.mergeFailuresRemaining = 1 }
    Invoke-TestConsolidate $concurrentResume -ExpectFailure
    Update-ProviderWorker $concurrentResume 'worker-01' { param($worker) $worker.mergeDelayMilliseconds = 5000 }
    $firstResume = Start-Process -FilePath 'pwsh' -ArgumentList @(
        '-NoProfile', '-File', $coordinator,
        '-Action', 'Resume',
        '-Manifest', $concurrentResume.Manifest,
        '-RunnerConfig', $concurrentResume.Runner,
        '-State', $concurrentResume.State,
        '-RuntimeRoot', $concurrentResume.Runtime,
        '-Merge'
    ) -PassThru
    $resumeLock = Join-Path $concurrentResume.Runtime 'campaign.lock'
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    do {
        Start-Sleep -Milliseconds 100
        $resumeLockObserved = Test-Path -LiteralPath $resumeLock -PathType Container
    } until ($resumeLockObserved -or [DateTime]::UtcNow -gt $deadline)
    Assert-Test $resumeLockObserved 'Consolidation resume fixture did not acquire the campaign lock'
    & pwsh -NoProfile -File $coordinator -Action Resume -Manifest $concurrentResume.Manifest `
        -RunnerConfig $concurrentResume.Runner -State $concurrentResume.State `
        -RuntimeRoot $concurrentResume.Runtime -Merge 2>$null | Out-Null
    Assert-Test ($LASTEXITCODE -ne 0) 'Concurrent consolidation resume bypassed the campaign lock'
    $firstResume.WaitForExit()
    Assert-Test ($firstResume.ExitCode -eq 0) 'First locked consolidation resume failed'
    Assert-Test ((Read-TestState $concurrentResume.State).status -eq 'Completed') `
        'Concurrent consolidation resume lock changed the completed state'

    $stacked = New-TestCase 'stacked-base-revalidation' -WorkerCount 3 -SharedRepository -Stacked
    Invoke-TestConsolidate $stacked
    $stackedProviderState = Read-TestState $stacked.ProviderState
    Assert-Test ([int] $stackedProviderState.workers['worker-02'].preflightCount -ge 4) `
        'Remaining stacked PR bases were not revalidated after each merge'

    $stopDuringMerge = New-TestCase 'stop-during-merge'
    Update-ProviderWorker $stopDuringMerge 'worker-00' { param($worker) $worker.mergeDelayMilliseconds = 1200 }
    $consolidateProcess = Start-Process -FilePath 'pwsh' -ArgumentList @(
        '-NoProfile', '-File', $coordinator,
        '-Action', 'Consolidate',
        '-Manifest', $stopDuringMerge.Manifest,
        '-RunnerConfig', $stopDuringMerge.Runner,
        '-State', $stopDuringMerge.State,
        '-RuntimeRoot', $stopDuringMerge.Runtime,
        '-Merge'
    ) -PassThru
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    do {
        Start-Sleep -Milliseconds 100
        $mergeStarted = @((Read-TestState $stopDuringMerge.State).events |
            Where-Object type -eq 'MergeStarted').Count -gt 0
    } until ($mergeStarted -or [DateTime]::UtcNow -gt $deadline)
    Assert-Test $mergeStarted 'Stop-during-merge fixture did not enter merge'
    & pwsh -NoProfile -File $coordinator -Action Stop -Manifest $stopDuringMerge.Manifest `
        -State $stopDuringMerge.State | Out-Null
    Assert-Test ($LASTEXITCODE -eq 0) 'Stop command failed during merge'
    $consolidateProcess.WaitForExit()
    Assert-Test ($consolidateProcess.ExitCode -eq 0) 'Cooperatively stopped consolidation process failed'
    $pausedMergeState = Read-TestState $stopDuringMerge.State
    Assert-Test ($pausedMergeState.status -eq 'PausedByUser') 'Stop during merge did not reach PausedByUser'
    Assert-Test ($pausedMergeState.workers[0].status -eq 'Merged') 'Completed merge was lost during cooperative stop'
    Assert-Test ($pausedMergeState.postMergeActions[0].status -eq 'Pending') 'Post-merge action started after stop'
    Invoke-TestResume $stopDuringMerge
    Assert-Test ((Read-TestState $stopDuringMerge.State).status -eq 'Completed') 'Stop-during-merge resume did not complete'

    $stopDuringPreflight = New-TestCase 'stop-between-provider-preflights' -WorkerCount 2
    Update-ProviderWorker $stopDuringPreflight 'worker-00' { param($worker) $worker.preflightDelayMilliseconds = 1200 }
    $preflightProcess = Start-Process -FilePath 'pwsh' -ArgumentList @(
        '-NoProfile', '-File', $coordinator,
        '-Action', 'Consolidate',
        '-Manifest', $stopDuringPreflight.Manifest,
        '-RunnerConfig', $stopDuringPreflight.Runner,
        '-State', $stopDuringPreflight.State,
        '-RuntimeRoot', $stopDuringPreflight.Runtime,
        '-Merge'
    ) -PassThru
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    do {
        Start-Sleep -Milliseconds 100
        $firstPreflightStarted =
            [int](Read-TestState $stopDuringPreflight.ProviderState).workers['worker-00'].preflightCount -gt 0
    } until ($firstPreflightStarted -or [DateTime]::UtcNow -gt $deadline)
    Assert-Test $firstPreflightStarted 'Stop-between-preflights fixture did not enter the first preflight'
    & pwsh -NoProfile -File $coordinator -Action Stop -Manifest $stopDuringPreflight.Manifest `
        -State $stopDuringPreflight.State | Out-Null
    Assert-Test ($LASTEXITCODE -eq 0) 'Stop command failed during provider preflight'
    $preflightProcess.WaitForExit()
    Assert-Test ($preflightProcess.ExitCode -eq 0) 'Cooperatively stopped preflight process failed'
    $preflightPausedState = Read-TestState $stopDuringPreflight.State
    $preflightProviderState = Read-TestState $stopDuringPreflight.ProviderState
    Assert-Test ($preflightPausedState.status -eq 'PausedByUser') `
        'Stop between provider preflights did not reach PausedByUser'
    Assert-Test ([int] $preflightProviderState.workers['worker-01'].preflightCount -eq 0) `
        'A second provider preflight started after the stop request'
    Invoke-TestResume $stopDuringPreflight
    Assert-Test ((Read-TestState $stopDuringPreflight.State).status -eq 'Completed') `
        'Stop-between-preflights resume did not complete'

    $processAbort = New-TestCase 'process-abort-adoption'
    Update-ProviderWorker $processAbort 'worker-00' { param($worker) $worker.postMergeDelayMilliseconds = 3000 }
    $abortProcess = Start-Process -FilePath 'pwsh' -ArgumentList @(
        '-NoProfile', '-File', $coordinator,
        '-Action', 'Consolidate',
        '-Manifest', $processAbort.Manifest,
        '-RunnerConfig', $processAbort.Runner,
        '-State', $processAbort.State,
        '-RuntimeRoot', $processAbort.Runtime,
        '-Merge'
    ) -PassThru
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    do {
        Start-Sleep -Milliseconds 100
        $providerMerged = (Read-TestState $processAbort.ProviderState).workers['worker-00'].state -eq 'Merged'
    } until ($providerMerged -or [DateTime]::UtcNow -gt $deadline)
    Assert-Test $providerMerged 'Process-abort fixture did not reach external merge'
    Stop-Process -Id $abortProcess.Id -Force
    $abortProcess.WaitForExit()
    Update-ProviderWorker $processAbort 'worker-00' { param($worker) $worker.postMergeDelayMilliseconds = 0 }
    $abortedState = Read-TestState $processAbort.State
    $abortedState.status = 'MergeFailed'
    $abortedState.phase = 'MergeAndSync'
    Write-TestJson $processAbort.State $abortedState
    Invoke-TestResume $processAbort
    $adoptedState = Read-TestState $processAbort.State
    Assert-Test ($adoptedState.status -eq 'Completed') 'Externally completed exact merge was not adopted'
    Assert-Test ([int] $adoptedState.workers[0].mergeAttempt -eq 1) 'Adopted external merge was executed again'

    $postMergeFailure = New-TestCase 'post-merge-failure-resume' -SyncFailures 1
    Invoke-TestConsolidate $postMergeFailure -ExpectFailure
    $postMergeFailedState = Read-TestState $postMergeFailure.State
    Assert-Test ($postMergeFailedState.status -eq 'SynchronizationFailed') 'Post-merge failure state is incorrect'
    Assert-Test ($postMergeFailedState.workers[0].status -eq 'Merged') 'Post-merge failure lost verified merge'
    $postMergeFailedPhase = [string] $postMergeFailedState.phase
    & pwsh -NoProfile -File $coordinator -Action Stop -Manifest $postMergeFailure.Manifest `
        -State $postMergeFailure.State | Out-Null
    Assert-Test ($LASTEXITCODE -eq 0) 'Stop request failed after post-merge failure'
    $stoppedPostMergeState = Read-TestState $postMergeFailure.State
    Assert-Test ($stoppedPostMergeState.status -eq 'SynchronizationFailed') `
        'Stop request overwrote the resumable consolidation status'
    Assert-Test ([string] $stoppedPostMergeState.phase -eq $postMergeFailedPhase) `
        'Stop request overwrote the resumable consolidation phase'
    Assert-Test ([bool] $stoppedPostMergeState.stopRequested) 'Stop request flag was not persisted'
    Invoke-TestResume $postMergeFailure
    $postMergeResumedState = Read-TestState $postMergeFailure.State
    Assert-Test ($postMergeResumedState.status -eq 'Completed') 'Post-merge failure resume did not complete'
    Assert-Test ([int] $postMergeResumedState.postMergeActions[0].attemptCount -eq 2) 'Failed idempotent action was not retried once'

    Write-Output 'PASS: all schema 1.1 consolidation fixtures passed.'
} finally {
    Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
