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

    $registry = [ordered]@{
        schema_version = '1.0'
        presets = [ordered]@{
            'autonomous-run-governance' = [ordered]@{
                version = $Version
                enabled = $Enabled
                priority = 70
            }
        }
    }
    $registryPath = Join-Path $Repository '.specify/presets/.registry'
    Write-DependencyJson $registryPath $registry
    & git -C $Repository add .specify/presets/.registry
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

    Set-AutonomousRegistry $repository '0.3.2' $true
    $current = Invoke-DependencyValidation $manifest $runnerConfig
    Assert-DependencyTest ($current.ExitCode -eq 0) `
        "Current autonomous preset failed validation: $($current.Output)"

    Write-Output 'PASS: autonomous preset dependency preflight fixtures passed.'
} finally {
    Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
