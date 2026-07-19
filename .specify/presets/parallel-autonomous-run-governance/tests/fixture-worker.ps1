#Requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $CampaignId,
    [Parameter(Mandatory)][string] $WorkerId,
    [Parameter(Mandatory)][string] $RunId,
    [Parameter(Mandatory)][string] $Worktree,
    [Parameter(Mandatory)][string] $ResultFile,
    [ValidateSet('Completed', 'ReadyForMerge', 'Failed')]
    [string] $Mode = 'Completed',
    [string] $HandoffsJson = '[]',
    [int] $DelayMilliseconds = 100
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Start-Sleep -Milliseconds $DelayMilliseconds
if ($Mode -eq 'Failed') {
    Write-Error "Intentional fixture failure for $WorkerId"
}

$artifact = Join-Path $Worktree "result-$WorkerId.md"
"# Result $WorkerId`n`nCampaign: $CampaignId`n" |
    Set-Content -LiteralPath $artifact -Encoding utf8NoBOM
& git -C $Worktree add -- "result-$WorkerId.md"
if ($LASTEXITCODE -ne 0) {
    throw 'git add failed'
}
& git -C $Worktree commit -m "test: complete $WorkerId" | Out-Null
if ($LASTEXITCODE -ne 0) {
    throw 'git commit failed'
}
$head = (& git -C $Worktree rev-parse HEAD).Trim()
$handoffResults = @()
$handoffDeclarations = @($HandoffsJson | ConvertFrom-Json)
foreach ($handoff in $handoffDeclarations) {
    $handoffPath = Join-Path $Worktree ([string] $handoff.path)
    [void](New-Item -ItemType Directory -Path (Split-Path -Parent $handoffPath) -Force)
    [ordered]@{
        producer = $WorkerId
        consumer = [string] $handoff.consumerWorkerId
        campaignId = $CampaignId
    } | ConvertTo-Json | Set-Content -LiteralPath $handoffPath -Encoding utf8NoBOM
    & git -C $Worktree add -- ([string] $handoff.path)
    if ($LASTEXITCODE -ne 0) {
        throw "git add failed for handoff '$($handoff.path)'"
    }
    & git -C $Worktree commit -m "test: handoff $WorkerId to $($handoff.consumerWorkerId)" | Out-Null
    if ($LASTEXITCODE -ne 0) {
        throw "git commit failed for handoff '$($handoff.path)'"
    }
    $head = (& git -C $Worktree rev-parse HEAD).Trim()
    $handoffResults += [ordered]@{
        consumerWorkerId = [string] $handoff.consumerWorkerId
        path = [string] $handoff.path
        sha256 = (Get-FileHash -LiteralPath $handoffPath -Algorithm SHA256).Hash.ToLowerInvariant()
    }
}

$parent = Split-Path -Parent $ResultFile
[void](New-Item -ItemType Directory -Path $parent -Force)
[ordered]@{
    schemaVersion = '1.1'
    campaignId = $CampaignId
    workerId = $WorkerId
    runId = $RunId
    status = $Mode
    headSha = $head
    autonomousStatePath = 'N/A'
    autonomousStateSha256 = 'N/A'
    evidencePath = "result-$WorkerId.md"
    prUrl = if ($Mode -eq 'ReadyForMerge') { "https://example.invalid/pr/$WorkerId" } else { $null }
    handoffs = @($handoffResults)
    summary = 'Deterministic fixture completed.'
} | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath $ResultFile -Encoding utf8NoBOM
