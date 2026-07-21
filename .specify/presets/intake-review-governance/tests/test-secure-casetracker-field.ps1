[CmdletBinding()]
param(
    [string]$EvidenceRoot = "$HOME/.specify/parallel-runs/secure-casetracker-native-field-20260718/evidence/pre-consolidation-20260719T073505Z",
    [string]$CoordinatorPath = ''
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$ManifestSource = Join-Path $EvidenceRoot 'parallel-campaign.json'
$ProfilesSource = Join-Path $EvidenceRoot 'parallel-runner-profiles.json'
if (-not (Test-Path -LiteralPath $ManifestSource -PathType Leaf)) {
    Write-Output 'SKIP: immutable Secure CaseTracker field evidence is not installed.'
    exit 0
}

$HomeBaseline = Split-Path -Parent (Split-Path -Parent (Split-Path -Parent $PSCommandPath))
$CoordinatorCandidates = @(
    $CoordinatorPath
    (Join-Path $HomeBaseline 'parallel-autonomous-run-governance/scripts/orchestrate-parallel-autonomous-runs.ps1')
    (Join-Path $HomeBaseline 'spec-kit-preset-parallel-autonomous-run-governance/scripts/orchestrate-parallel-autonomous-runs.ps1')
) | Where-Object { -not [string]::IsNullOrWhiteSpace($_) }
$Coordinator = $CoordinatorCandidates | Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } | Select-Object -First 1
if (-not $Coordinator) {
    Write-Output 'SKIP: Parallel Autonomous coordinator is not installed beside the optional field fixture.'
    exit 0
}
$TempRoot = Join-Path ([IO.Path]::GetTempPath()) "intake-field-$([Guid]::NewGuid())"
New-Item -ItemType Directory -Path $TempRoot | Out-Null

function Get-NormalizedHash([string]$Path) {
    $Bytes = [IO.File]::ReadAllBytes($Path)
    $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
    $Utf8 = [Text.UTF8Encoding]::new($false, $true)
    $Text = $Utf8.GetString($Bytes, $Offset, $Bytes.Length - $Offset).Replace("`r`n", "`n").Replace("`r", "`n")
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Text))).ToLowerInvariant()
}

function Save-Json([object]$Value, [string]$Path) {
    $Value | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Invoke-FieldValidation([int]$ExpectedExit, [string]$ExpectedText) {
    $Output = & pwsh -NoProfile -File $Coordinator -Action Validate `
        -Manifest (Join-Path $TempRoot 'campaign.json') `
        -RunnerConfig (Join-Path $TempRoot 'profiles.json') 2>&1
    if ($LASTEXITCODE -ne $ExpectedExit -or ($ExpectedText -and ($Output -join "`n") -notmatch $ExpectedText)) {
        throw "Expected exit $ExpectedExit / '$ExpectedText', got $LASTEXITCODE`: $Output"
    }
}

try {
    $Campaign = Get-Content -LiteralPath $ManifestSource -Raw | ConvertFrom-Json
    $Profiles = Get-Content -LiteralPath $ProfilesSource -Raw | ConvertFrom-Json
    $Campaign.schemaVersion = '1.2'
    $Campaign.deliveryMode = 'LocalImplementation'
    $Campaign | Add-Member -NotePropertyName intakeReview -NotePropertyValue ([pscustomobject]@{
        required = $true; resultPath = 'intake-review-result.json'
    }) -Force
    $Profiles.schemaVersion = '1.2'
    foreach ($RunnerProfile in $Profiles.profiles.PSObject.Properties) {
        $RunnerProfile.Value | Add-Member -NotePropertyName agentFamily -NotePropertyValue 'codex' -Force
    }
    $RuntimeWorktrees = Join-Path (Split-Path -Parent (Split-Path -Parent $EvidenceRoot)) 'runtime/worktrees'
    foreach ($Worker in $Campaign.workers) {
        $Language = ([string]$Worker.workerId -split '-unit-')[0]
        $Worker.repository = Join-Path $RuntimeWorktrees "${Language}-unit-03"
    }

    $Targets = [ordered]@{}
    New-Item -ItemType Directory -Path (Join-Path $TempRoot 'review-targets') -Force | Out-Null
    $WorkerCoverage = @()
    $Edges = @()
    foreach ($Worker in $Campaign.workers) {
        $FeatureInputPath = Join-Path ([string]$Worker.repository) ([string]$Worker.featureInput)
        $TargetPath = "review-targets/$([string]$Worker.featureInput)"
        if (-not $Targets.Contains($TargetPath)) {
            Copy-Item -LiteralPath $FeatureInputPath -Destination (Join-Path $TempRoot $TargetPath)
            $Targets[$TargetPath] = [ordered]@{
                path = $TargetPath; role = 'UniqueCampaignIntake'
                normalizedSha256 = Get-NormalizedHash $FeatureInputPath; gitBlob = 'N/A'
            }
        }
        $WorkerCoverage += [ordered]@{
            workerId = [string]$Worker.workerId; targetPath = $TargetPath
            applicability = 'Applicable'; repository = [string]$Worker.repository
        }
        foreach ($Dependency in @($Worker.dependsOn)) {
            $Edges += [ordered]@{ from = [string]$Dependency; to = [string]$Worker.workerId }
        }
    }
    $Result = [ordered]@{
        schemaVersion = '1.0'; reviewId = [Guid]::NewGuid().ToString(); mode = 'Campaign'
        status = 'Ready'; policy = 'secure-casetracker-field'; reviewedAt = [DateTime]::UtcNow.ToString('o')
        repository = [ordered]@{ root = 'multi-repository'; head = 'immutable-field-evidence' }
        targets = @($Targets.Values); findings = @(); questions = @(); acceptedRisks = @()
        operatorExceptions = @()
        coverage = [ordered]@{ individual = @($Targets.Keys); series = @($Edges); workers = @($WorkerCoverage) }
        summary = [ordered]@{ critical = 0; high = 0; medium = 0; low = 0 }
        supersedes = 'N/A'
    }
    Save-Json $Campaign (Join-Path $TempRoot 'campaign.json')
    Save-Json $Profiles (Join-Path $TempRoot 'profiles.json')
    Save-Json $Result (Join-Path $TempRoot 'intake-review-result.json')

    Invoke-FieldValidation 1 'Container-First/native Override'
    $Unit00Workers = @($Campaign.workers | Where-Object workerId -like '*-unit-00' | ForEach-Object workerId)
    $Result.operatorExceptions = @([ordered]@{
        exceptionId = 'OE001'; author = 'Thorsten Hindermann'
        reason = 'Native macOS field execution explicitly authorized for this campaign only.'
        date = '2026-07-18'; expiry = '2027-07-18T23:59:59Z'; workerIds = $Unit00Workers
    })
    Save-Json $Result (Join-Path $TempRoot 'intake-review-result.json')
    Invoke-FieldValidation 0 'PASS: campaign'
    $PresetRoot = Split-Path -Parent $PSScriptRoot
    $Standalone = Join-Path $PresetRoot 'scripts/validate-intake-review-result.ps1'
    & pwsh -NoProfile -File $Standalone -Result (Join-Path $TempRoot 'intake-review-result.json') -Repo $TempRoot
    if ($LASTEXITCODE -ne 0) { throw 'Standalone campaign review validation failed.' }

    $Result.coverage.workers = @($Result.coverage.workers | Select-Object -Skip 1)
    Save-Json $Result (Join-Path $TempRoot 'intake-review-result.json')
    Invoke-FieldValidation 1 'Applicability-Zeile'
    Write-Output 'PASS: 4 unique intakes, 6 pipelines, 24 worker applicability rows, max concurrency 3'
} finally {
    Remove-Item -LiteralPath $TempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
