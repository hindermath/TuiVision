#Requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$presetRoot = Split-Path -Parent $PSScriptRoot
$coordinator = Join-Path $presetRoot 'scripts/orchestrate-parallel-autonomous-runs.ps1'
$tempRoot = Join-Path ([IO.Path]::GetTempPath()) "parallel-autonomous-dependency-$([guid]::NewGuid())"
[void](New-Item -ItemType Directory -Path $tempRoot)

function Assert-DependencyTest {
    param(
        [Parameter(Mandatory)][bool] $Condition,
        [Parameter(Mandatory)][string] $Message
    )

    if (-not $Condition) {
        throw $Message
    }
}

function Write-DependencyJson {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)] $Value
    )

    $parent = Split-Path -Parent $Path
    if ($parent) {
        [void](New-Item -ItemType Directory -Path $parent -Force)
    }
    $Value | ConvertTo-Json -Depth 20 |
        Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Set-AutonomousRegistry {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string] $Version,
        [Parameter(Mandatory)][bool] $Enabled
    )

    $presetPath = Join-Path $Repository '.specify/presets/autonomous-run-governance'
    $manifestPath = Join-Path $presetPath 'preset.yml'
    [void](New-Item -ItemType Directory -Path $presetPath -Force)
    $manifestText = "preset:`n  version: `"$Version`"`nprovides:`n  templates:`n    - type: `"command`"`n      file: `"commands/speckit.autonomous.md`"`n"
    $manifestText | Set-Content -LiteralPath $manifestPath -Encoding utf8NoBOM
    foreach ($relative in @('commands/speckit.autonomous.md', 'scripts/validate-autonomous-run-state.ps1',
            'scripts/validate-autonomous-run-state.sh', 'scripts/autonomous-evidence-core.py')) {
        $file = Join-Path $presetPath $relative
        [void](New-Item -ItemType Directory -Path (Split-Path -Parent $file) -Force)
        'fixture payload' | Set-Content -LiteralPath $file -Encoding utf8NoBOM
    }
    $manifestHash = (Get-FileHash -LiteralPath $manifestPath -Algorithm SHA256).Hash.ToLowerInvariant()
    $registry = [ordered]@{
        schema_version = '1.0'
        presets = [ordered]@{
            'autonomous-run-governance' = [ordered]@{
                version = $Version
                enabled = $Enabled
                manifest_hash = "sha256:$manifestHash"
                priority = 70
            }
        }
    }
    $registryPath = Join-Path $Repository '.specify/presets/.registry'
    Write-DependencyJson $registryPath $registry
    & git -C $Repository add .specify/presets
    & git -C $Repository commit -m "test: autonomous preset $Version enabled=$Enabled" |
        Out-Null
}

function Invoke-DependencyValidation {
    param(
        [Parameter(Mandatory)][string] $Manifest,
        [Parameter(Mandatory)][string] $RunnerConfig
    )

    $output = @(& pwsh -NoProfile -File $coordinator `
        -Action Validate `
        -Manifest $Manifest `
        -RunnerConfig $RunnerConfig 2>&1)
    return @{
        ExitCode = $LASTEXITCODE
        Output = ($output -join [Environment]::NewLine)
    }
}

try {
    $repository = Join-Path $tempRoot 'worker-repository'
    [void](New-Item -ItemType Directory -Path $repository)
    & git -C $repository init -b main | Out-Null
    & git -C $repository config user.name 'Dependency Fixture'
    & git -C $repository config user.email 'dependency-fixture@example.invalid'
    '# Dependency fixture' |
        Set-Content -LiteralPath (Join-Path $repository 'README.md') -Encoding utf8NoBOM
    & git -C $repository add README.md
    & git -C $repository commit -m 'test: baseline' | Out-Null
    $baselineWithoutPreset = (& git -C $repository rev-parse HEAD).Trim()

    $runnerConfig = Join-Path $tempRoot 'runners.json'
    Write-DependencyJson $runnerConfig ([ordered]@{
        schemaVersion = '1.1'
        profiles = [ordered]@{
            fixture = [ordered]@{
                agentFamily = 'Fixture'
                model = $null
                reasoningEffort = $null
                executable = 'pwsh'
                arguments = @('-NoProfile', '-Command', 'exit 0')
            }
        }
        mergeProfiles = [ordered]@{}
        postMergeProfiles = [ordered]@{}
    })

    $manifest = Join-Path $tempRoot 'campaign.json'
    $workerId = 'dependency-worker'
    Write-DependencyJson $manifest ([ordered]@{
        schemaVersion = '1.1'
        campaignId = [guid]::NewGuid().ToString()
        name = 'autonomous-preset-dependency'
        topology = 'IndependentFeatures'
        deliveryMode = 'LocalImplementation'
        maxConcurrency = 1
        runnerProfile = 'fixture'
        requireAutonomousPreset = $true
        operatorInstructions = 'Dependency validation fixture.'
        workers = @(
            [ordered]@{
                workerId = $workerId
                runId = [guid]::NewGuid().ToString()
                repository = $repository
                baseRef = 'main'
                baseWorkerId = $null
                branch = "parallel/dependency/$workerId"
                featureInput = 'README.md'
                runnerProfile = $null
                dependsOn = @()
                handoffs = @()
            }
        )
        consolidation = [ordered]@{
            allReadyBarrier = $true
            mergeOrder = @($workerId)
            humanSelectionRequired = $false
            mergeProfile = ''
        }
        postMergeActions = @()
    })

    $missing = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($missing.ExitCode -ne 0) `
        'Missing autonomous-run-governance preset was accepted.'
    Assert-DependencyTest ($missing.Output -match 'autonomous-run-governance >= 0\.2\.2') `
        'Missing dependency did not produce the expected preflight finding.'

    Set-AutonomousRegistry $repository '0.3.1' $false
    $disabled = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($disabled.ExitCode -ne 0) `
        'Disabled autonomous-run-governance preset was accepted.'

    Set-AutonomousRegistry $repository '0.2.1' $true
    $outdated = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($outdated.ExitCode -ne 0) `
        'Outdated autonomous-run-governance preset was accepted.'

    Set-AutonomousRegistry $repository '0.2.2' $true
    $minimum = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($minimum.ExitCode -eq 0) `
        "Minimum supported autonomous preset failed validation: $($minimum.Output)"

    Set-AutonomousRegistry $repository '0.4.10' $true
    $current = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($current.ExitCode -eq 0) `
        "Current autonomous preset failed validation: $($current.Output)"
    $outsideState = Join-Path $tempRoot 'outside-runtime-state.json'
    $outsideCheckpoint = @(& pwsh -NoProfile -File $coordinator -Action Start `
        -Manifest $manifest -RunnerConfig $runnerConfig -State $outsideState `
        -RuntimeRoot (Join-Path $tempRoot 'outside-state-runtime') 2>&1)
    Assert-DependencyTest ($LASTEXITCODE -ne 0 -and
        ($outsideCheckpoint -join ' ') -match 'Campaign-State muss direkt') `
        'Operational campaign accepted a checkpoint outside RuntimeRoot.'
    if (-not $IsWindows) {
        $nestedRuntime = Join-Path $repository 'nested-runtime'
        [void](New-Item -ItemType Directory -Path $nestedRuntime -Force)
        $parentAlias = Join-Path $tempRoot 'repository-parent-alias'
        $runtimeAlias = Join-Path $tempRoot 'nested-runtime-alias'
        [void](New-Item -ItemType SymbolicLink -Path $parentAlias -Target $repository)
        [void](New-Item -ItemType SymbolicLink -Path $runtimeAlias -Target (Join-Path $parentAlias 'nested-runtime'))
        $nestedCheckpoint = @(& pwsh -NoProfile -File $coordinator -Action Start `
            -Manifest $manifest -RunnerConfig $runnerConfig -State (Join-Path $runtimeAlias 'state.json') `
            -RuntimeRoot $runtimeAlias 2>&1)
        Assert-DependencyTest ($LASTEXITCODE -ne 0 -and
            ($nestedCheckpoint -join ' ') -match 'RuntimeRoot muss ausserhalb') `
            'Nested symlink let an operational RuntimeRoot enter a worker repository.'
    }
    $publishCampaign = Get-Content -LiteralPath $manifest -Raw | ConvertFrom-Json -AsHashtable
    $publishCampaign.campaignId = [Guid]::NewGuid().ToString()
    $publishCampaign.deliveryMode = 'PublishPR'
    $publishCampaign.workers[0].branch = 'parallel/dependency/publish-pr'
    $publishManifest = Join-Path $tempRoot 'publish-campaign.json'
    Write-DependencyJson $publishManifest $publishCampaign
    $missingPublication = Invoke-DependencyValidation $publishManifest $runnerConfig
    Assert-DependencyTest ($missingPublication.ExitCode -ne 0 -and
        $missingPublication.Output -match 'publicationProfile') `
        'PublishPR accepted a campaign without a read-only provider profile.'
    $publishCampaign.publicationProfile = 'fixture-publication'
    Write-DependencyJson $publishManifest $publishCampaign
    $publishProfiles = Get-Content -LiteralPath $runnerConfig -Raw | ConvertFrom-Json -AsHashtable
    $publishProfiles.publicationProfiles = [ordered]@{
        'fixture-publication' = [ordered]@{
            provider = 'Local Fixture'
            preflight = [ordered]@{ executable = 'pwsh'; arguments = @('-NoProfile', '-Command', 'exit 0') }
        }
    }
    $publishRunnerConfig = Join-Path $tempRoot 'publish-runners.json'
    Write-DependencyJson $publishRunnerConfig $publishProfiles
    $validPublication = Invoke-DependencyValidation $publishManifest $publishRunnerConfig
    Assert-DependencyTest ($validPublication.ExitCode -eq 0) `
        "PublishPR read-only profile failed validation: $($validPublication.Output)"
    $publishState = Join-Path $tempRoot 'publish-runtime/state.json'
    & pwsh -NoProfile -File $coordinator -Action Start -Manifest $publishManifest `
        -RunnerConfig $publishRunnerConfig -State $publishState `
        -RuntimeRoot (Join-Path $tempRoot 'publish-runtime') | Out-Null
    $publishStateData = Get-Content -LiteralPath $publishState -Raw | ConvertFrom-Json
    Assert-DependencyTest ($publishStateData.status -eq 'Failed' -and
        $publishStateData.workers[0].summary -match 'explizites PR-Ergebnis') `
        'PublishPR completed from a zero-exit runner without a PR result.'
    $withoutGate = Get-Content -LiteralPath $manifest -Raw | ConvertFrom-Json -AsHashtable
    $withoutGate.requireAutonomousPreset = $false
    $withoutGateManifest = Join-Path $tempRoot 'without-gate-campaign.json'
    Write-DependencyJson $withoutGateManifest $withoutGate
    $disabledGate = Invoke-DependencyValidation $withoutGateManifest $runnerConfig
    Assert-DependencyTest ($disabledGate.ExitCode -ne 0 -and
        $disabledGate.Output -match 'Autonomous-Preset-Gate.*verpflichtend') `
        'An operational campaign disabled its mandatory Autonomous preset gate.'
    $fixtureWorker = Join-Path $PSScriptRoot 'fixture-worker.ps1'
    $unsafeProfiles = Get-Content -LiteralPath $runnerConfig -Raw | ConvertFrom-Json -AsHashtable
    $unsafeProfiles.profiles['spoofed-fixture'] = [ordered]@{
        agentFamily = 'Fixture'; executable = 'pwsh'
        arguments = @('-NoProfile', '-Command', 'Write-Output BYPASS', '-File', $fixtureWorker)
    }
    $unsafeProfiles.profiles['foreign-worktree'] = [ordered]@{
        agentFamily = 'Fixture'; executable = 'pwsh'
        arguments = @(
            '-NoProfile', '-File', $fixtureWorker,
            '-CampaignId', '{campaignId}', '-WorkerId', '{workerId}',
            '-RunId', '{runId}', '-AttemptId', '{attemptId}',
            '-Worktree', (Split-Path -Parent $tempRoot), '-ResultFile', '{resultFile}',
            '-Mode', 'Completed', '-HandoffsJson', '{handoffsJson}',
            '-DelayMilliseconds', '20'
        )
    }
    $relativeArguments = @($unsafeProfiles.profiles['foreign-worktree'].arguments)
    $relativeArguments[2] = [IO.Path]::GetRelativePath((Get-Location).Path, $fixtureWorker)
    $relativeArguments[12] = '{worktree}'
    $unsafeProfiles.profiles['relative-file'] = [ordered]@{
        agentFamily = 'Fixture'; executable = 'pwsh'; arguments = $relativeArguments
    }
    Write-DependencyJson $runnerConfig $unsafeProfiles
    $previousFixtureRoot = $env:PAR_TEST_FIXTURE_ROOT
    try {
        $env:PAR_TEST_FIXTURE_ROOT = $tempRoot
        foreach ($name in @('spoofed-fixture', 'foreign-worktree', 'relative-file')) {
            $withoutGate.runnerProfile = $name
            Write-DependencyJson $withoutGateManifest $withoutGate
            $unsafeResult = Invoke-DependencyValidation $withoutGateManifest $runnerConfig
            Assert-DependencyTest ($unsafeResult.ExitCode -ne 0 -and
                $unsafeResult.Output -match 'Autonomous-Preset-Gate.*verpflichtend') `
                "Unsafe fixture profile '$name' bypassed the mandatory gate."
        }
    } finally {
        $env:PAR_TEST_FIXTURE_ROOT = $previousFixtureRoot
    }

    $runnerData = Get-Content -LiteralPath $runnerConfig -Raw | ConvertFrom-Json -AsHashtable
    $runnerData.profiles['explicit'] = [ordered]@{
        agentFamily = 'Fixture'
        model = $null
        reasoningEffort = $null
        executable = 'pwsh'
        arguments = @(
            '-NoProfile', '-File', $fixtureWorker,
            '-CampaignId', '{campaignId}', '-WorkerId', '{workerId}',
            '-RunId', '{runId}', '-AttemptId', '{attemptId}',
            '-Worktree', '{worktree}', '-ResultFile', '{resultFile}',
            '-Mode', 'Completed', '-HandoffsJson', '{handoffsJson}',
            '-DelayMilliseconds', '20'
        )
    }
    Write-DependencyJson $runnerConfig $runnerData
    $explicitCampaign = Get-Content -LiteralPath $manifest -Raw | ConvertFrom-Json -AsHashtable
    $explicitCampaign.campaignId = [Guid]::NewGuid().ToString()
    $explicitCampaign.runnerProfile = 'explicit'
    $explicitCampaign.workers[0].runId = [Guid]::NewGuid().ToString()
    $explicitCampaign.workers[0].branch = 'parallel/dependency/explicit-without-state'
    $explicitManifest = Join-Path $tempRoot 'explicit-campaign.json'
    Write-DependencyJson $explicitManifest $explicitCampaign
    $explicitState = Join-Path $tempRoot 'explicit-runtime/state.json'
    $explicitOutput = @(& pwsh -NoProfile -File $coordinator -Action Start -Manifest $explicitManifest `
        -RunnerConfig $runnerConfig -State $explicitState -RuntimeRoot (Join-Path $tempRoot 'explicit-runtime') 2>&1)
    Assert-DependencyTest (Test-Path -LiteralPath $explicitState -PathType Leaf) `
        "Explicit-result fixture did not create state: $($explicitOutput -join [Environment]::NewLine)"
    $explicitStateData = Get-Content -LiteralPath $explicitState -Raw | ConvertFrom-Json
    Assert-DependencyTest ($explicitStateData.status -eq 'Failed' -and
        $explicitStateData.workers[0].summary -match 'Autonomous-State-Pfad') `
        'An explicit successful worker result without Autonomous state was accepted.'
    $runnerData.profiles['explicit-valid'] = [ordered]@{
        agentFamily = 'Fixture'
        model = $null
        reasoningEffort = $null
        executable = 'pwsh'
        arguments = @($runnerData.profiles['explicit'].arguments) + @('-WriteAutonomousState')
    }
    Write-DependencyJson $runnerConfig $runnerData
    $validCampaign = Get-Content -LiteralPath $explicitManifest -Raw | ConvertFrom-Json -AsHashtable
    $validCampaign.campaignId = [Guid]::NewGuid().ToString()
    $validCampaign.runnerProfile = 'explicit-valid'
    $validCampaign.workers[0].runId = [Guid]::NewGuid().ToString()
    $validCampaign.workers[0].branch = 'parallel/dependency/explicit-valid-state'
    $validManifest = Join-Path $tempRoot 'valid-state-campaign.json'
    Write-DependencyJson $validManifest $validCampaign
    $validState = Join-Path $tempRoot 'valid-runtime/state.json'
    $validOutput = @(& pwsh -NoProfile -File $coordinator -Action Start -Manifest $validManifest `
        -RunnerConfig $runnerConfig -State $validState -RuntimeRoot (Join-Path $tempRoot 'valid-runtime') 2>&1)
    $validStateData = Get-Content -LiteralPath $validState -Raw | ConvertFrom-Json
    Assert-DependencyTest ($validStateData.status -eq 'Completed' -and
        $validStateData.workers[0].status -eq 'Completed') `
        "A valid explicit Autonomous state was rejected: $($validStateData.workers[0].summary)"

    $campaign = Get-Content -LiteralPath $manifest -Raw | ConvertFrom-Json -AsHashtable
    $campaign.workers[0].baseRef = $baselineWithoutPreset
    Write-DependencyJson $manifest $campaign
    $checkoutRuntime = Join-Path $tempRoot 'checkout-runtime'
    $checkoutState = Join-Path $checkoutRuntime 'state.json'
    $checkoutOutput = @(& pwsh -NoProfile -File $coordinator -Action Start -Manifest $manifest `
        -RunnerConfig $runnerConfig -State $checkoutState -RuntimeRoot $checkoutRuntime 2>&1)
    Assert-DependencyTest ($LASTEXITCODE -ne 0 -and
        ($checkoutOutput -join [Environment]::NewLine) -match 'Worker-Checkout') `
        'A source-valid but preset-free worker checkout was accepted.'

    $corePath = Join-Path $repository '.specify/presets/autonomous-run-governance/scripts/autonomous-evidence-core.py'
    Remove-Item -LiteralPath $corePath
    & git -C $repository add -u
    & git -C $repository commit -m 'test: missing autonomous core payload' | Out-Null
    $baseWithoutPayload = (& git -C $repository rev-parse HEAD).Trim()
    Set-AutonomousRegistry $repository '0.4.10' $true
    $campaign.workers[0].baseRef = $baseWithoutPayload
    $campaign.workers[0].branch = 'parallel/dependency/missing-payload'
    Write-DependencyJson $manifest $campaign
    $payloadOutput = @(& pwsh -NoProfile -File $coordinator -Action Start -Manifest $manifest `
        -RunnerConfig $runnerConfig -State (Join-Path $tempRoot 'payload-runtime/state.json') `
        -RuntimeRoot (Join-Path $tempRoot 'payload-runtime') 2>&1)
    Assert-DependencyTest ($LASTEXITCODE -ne 0 -and
        ($payloadOutput -join [Environment]::NewLine) -match 'Worker-Checkout') `
        'A worker checkout with registry metadata but missing payload was accepted.'

    $presetDirectory = Join-Path $repository '.specify/presets/autonomous-run-governance'
    $externalPreset = Join-Path $tempRoot 'external-autonomous-preset'
    Move-Item -LiteralPath $presetDirectory -Destination $externalPreset
    [void](New-Item -ItemType SymbolicLink -Path $presetDirectory -Target $externalPreset)
    & git -C $repository add -A .specify/presets
    & git -C $repository commit -m 'test: linked autonomous preset outside checkout' | Out-Null
    $baseWithExternalPreset = (& git -C $repository rev-parse HEAD).Trim()
    Remove-Item -LiteralPath $presetDirectory -Force
    Copy-Item -LiteralPath $externalPreset -Destination $presetDirectory -Recurse
    & git -C $repository add -A .specify/presets
    & git -C $repository commit -m 'test: restore local autonomous preset' | Out-Null
    $campaign.workers[0].baseRef = $baseWithExternalPreset
    $campaign.workers[0].branch = 'parallel/dependency/linked-preset'
    Write-DependencyJson $manifest $campaign
    $linkedOutput = @(& pwsh -NoProfile -File $coordinator -Action Start -Manifest $manifest `
        -RunnerConfig $runnerConfig -State (Join-Path $tempRoot 'linked-runtime/state.json') `
        -RuntimeRoot (Join-Path $tempRoot 'linked-runtime') 2>&1)
    Assert-DependencyTest ($LASTEXITCODE -ne 0 -and
        ($linkedOutput -join [Environment]::NewLine) -match 'Worker-Checkout') `
        'An externally linked autonomous preset passed the checkout gate.'

    $baseWithOldInput = (& git -C $repository rev-parse HEAD).Trim()
    '# Updated reviewed input' | Set-Content -LiteralPath (Join-Path $repository 'README.md') -Encoding utf8NoBOM
    & git -C $repository add README.md
    & git -C $repository commit -m 'test: change reviewed input' | Out-Null
    $currentInputHash = (Get-FileHash -LiteralPath (Join-Path $repository 'README.md') -Algorithm SHA256).Hash.ToLowerInvariant()
    Copy-Item -LiteralPath (Join-Path $repository 'README.md') -Destination (Join-Path $tempRoot 'reviewed-input.md')
    $reviewId = [Guid]::NewGuid().ToString()
    $reviewRequest = Join-Path $tempRoot 'review-request.json'
    Write-DependencyJson $reviewRequest ([ordered]@{
        schemaVersion = '1.0'; reviewId = $reviewId; mode = 'Campaign'; policy = 'fixture-policy'
        targets = @([ordered]@{ path = 'reviewed-input.md'; role = 'UniqueCampaignIntake' })
    })
    $reviewPath = Join-Path $tempRoot 'review-result.json'
    Write-DependencyJson $reviewPath ([ordered]@{
        schemaVersion = '1.0'; reviewId = $reviewId; mode = 'Campaign'; status = 'Ready'
        policy = 'fixture-policy'; reviewedAt = [DateTime]::UtcNow.ToString('o')
        repository = [ordered]@{ root = '.'; head = 'N/A' }
        requestEvidence = [ordered]@{
            path = 'review-request.json'
            normalizedSha256 = (Get-FileHash -LiteralPath $reviewRequest -Algorithm SHA256).Hash.ToLowerInvariant()
        }
        questions = @(); findings = @(); acceptedRisks = @()
        targets = @([ordered]@{ path = 'reviewed-input.md'; role = 'UniqueCampaignIntake'; normalizedSha256 = $currentInputHash; gitBlob = 'N/A' })
        coverage = [ordered]@{
            individual = @('reviewed-input.md')
            workers = @([ordered]@{ workerId = $workerId; targetPath = 'reviewed-input.md'; applicability = 'Applicable' })
            series = @()
        }
        operatorExceptions = @(); summary = [ordered]@{ critical = 0; high = 0; medium = 0; low = 0 }
        supersedes = 'N/A'
    })
    $campaign.schemaVersion = '1.2'
    $campaign.campaignId = [Guid]::NewGuid().ToString()
    $campaign.intakeReview = [ordered]@{ required = $true; resultPath = 'review-result.json' }
    $campaign.workers[0].runId = [Guid]::NewGuid().ToString()
    $campaign.workers[0].baseRef = $baseWithOldInput
    $campaign.workers[0].branch = 'parallel/dependency/reviewed-input'
    Write-DependencyJson $manifest $campaign
    $reviewFixture = Get-Content -LiteralPath $reviewPath -Raw | ConvertFrom-Json -AsHashtable
    $reviewFixture.coverage.individual = @()
    Write-DependencyJson $reviewPath $reviewFixture
    $incompleteReview = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($incompleteReview.ExitCode -ne 0 -and
        ($incompleteReview.Output -join ' ') -match 'coverage.individual') `
        'Incomplete individual intake coverage passed campaign validation.'
    $reviewFixture.coverage.individual = @('reviewed-input.md')
    Write-DependencyJson $reviewPath $reviewFixture
    $reviewSource = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($reviewSource.ExitCode -eq 0) `
        "Source review fixture failed validation: $($reviewSource.Output)"
    $reviewRuntime = Join-Path $tempRoot 'review-checkout-runtime'
    $reviewState = Join-Path $reviewRuntime 'state.json'
    $reviewOutput = @(& pwsh -NoProfile -File $coordinator -Action Start -Manifest $manifest `
        -RunnerConfig $runnerConfig -State $reviewState -RuntimeRoot $reviewRuntime 2>&1)
    Assert-DependencyTest ($LASTEXITCODE -ne 0 -and
        ($reviewOutput -join [Environment]::NewLine) -match 'featureInput im Worker-Checkout') `
        'A checkout with different effective intake bytes was accepted.'

    Write-Output 'PASS: autonomous preset dependency preflight fixtures passed.'
} finally {
    Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
