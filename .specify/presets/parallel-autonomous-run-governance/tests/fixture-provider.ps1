#Requires -Version 7.0
<#
.SYNOPSIS
    Simuliert einen providergebundenen PR-Preflight und Merge fuer Tests.

.DESCRIPTION
    DE: Liest und aktualisiert ausschließlich eine lokale JSON-Fixture. Es
    entstehen keine Netzwerk- oder Remote-Schreibzugriffe.

    EN: Reads and updates only a local JSON fixture. It performs no network or
    remote write operation.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateSet('Preflight', 'Merge')]
    [string] $Mode,
    [Parameter(Mandatory)][string] $StateFile,
    [Parameter(Mandatory)][string] $WorkerId,
    [Parameter(Mandatory)][string] $PrUrl,
    [Parameter(Mandatory)][string] $ExpectedHead,
    [string] $OutputFile = ''
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-FixtureJson {
    param([Parameter(Mandatory)] $Value)

    $Value | ConvertTo-Json -Depth 20 |
        Set-Content -LiteralPath $StateFile -Encoding utf8NoBOM
}

$state = Get-Content -LiteralPath $StateFile -Raw | ConvertFrom-Json -AsHashtable
if (-not $state.workers.ContainsKey($WorkerId)) {
    throw "Unknown fixture worker: $WorkerId"
}
$worker = $state.workers[$WorkerId]
if ([string] $worker.prUrl -ne $PrUrl) {
    throw "Unexpected PR URL for fixture worker: $WorkerId"
}

if ($Mode -eq 'Preflight') {
    if ([string]::IsNullOrWhiteSpace($OutputFile)) {
        throw '-OutputFile is required in Preflight mode.'
    }
    if (-not $worker.ContainsKey('preflightCount')) {
        $worker.preflightCount = 0
    }
    $worker.preflightCount = [int] $worker.preflightCount + 1
    Write-FixtureJson $state
    if ($worker.ContainsKey('preflightDelayMilliseconds') -and
        [int] $worker.preflightDelayMilliseconds -gt 0) {
        Start-Sleep -Milliseconds ([int] $worker.preflightDelayMilliseconds)
    }
    [ordered]@{
        schemaVersion = '1.1'
        prUrl = [string] $worker.prUrl
        state = [string] $worker.state
        isDraft = [bool] $worker.isDraft
        headSha = [string] $worker.headSha
        mergeable = [bool] $worker.mergeable
        reviewDecision = $worker.reviewDecision
        unresolvedCurrentThreads = [int] $worker.unresolvedCurrentThreads
        checkPolicySatisfied = [bool] $worker.checkPolicySatisfied
        technicalFailures = @($worker.technicalFailures)
        baseRefName = [string] $worker.baseRefName
        mergeCommitSha = $worker.mergeCommitSha
    } | ConvertTo-Json -Depth 10 |
        Set-Content -LiteralPath $OutputFile -Encoding utf8NoBOM
    exit 0
}

if ([string] $worker.headSha -ne $ExpectedHead) {
    throw "Unexpected expected head for fixture worker: $WorkerId"
}
if ($worker.ContainsKey('mergeDelayMilliseconds') -and
    [int] $worker.mergeDelayMilliseconds -gt 0) {
    Start-Sleep -Milliseconds ([int] $worker.mergeDelayMilliseconds)
}
if ($worker.ContainsKey('mergeFailuresRemaining') -and
    [int] $worker.mergeFailuresRemaining -gt 0) {
    $worker.mergeFailuresRemaining = [int] $worker.mergeFailuresRemaining - 1
    Write-FixtureJson $state
    exit 11
}
$worker.state = 'Merged'
$workerIdHash = [Security.Cryptography.SHA256]::HashData(
    [Text.Encoding]::UTF8.GetBytes($WorkerId)
)
$worker.mergeCommitSha = [Convert]::ToHexString($workerIdHash).ToLowerInvariant().Substring(0, 40)
Write-FixtureJson $state
if ($worker.ContainsKey('postMergeDelayMilliseconds') -and
    [int] $worker.postMergeDelayMilliseconds -gt 0) {
    Start-Sleep -Milliseconds ([int] $worker.postMergeDelayMilliseconds)
}
