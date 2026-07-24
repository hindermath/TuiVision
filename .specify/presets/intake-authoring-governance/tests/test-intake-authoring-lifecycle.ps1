<#
.SYNOPSIS
Prueft Schema 2.0, URL-Evidence und CRUD-/Series-Lifecycle-Paritaet.

Validates schema 2.0, URL evidence, and CRUD/series lifecycle parity.
#>
[CmdletBinding()]
param([string]$PresetRoot = (Split-Path -Parent $PSScriptRoot))

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-HBText([string]$Path, [string]$Text) {
    $Parent = Split-Path -Parent $Path
    if ($Parent) { New-Item -ItemType Directory -Path $Parent -Force | Out-Null }
    [IO.File]::WriteAllText($Path, $Text, [Text.UTF8Encoding]::new($false))
}
function Get-HBHash([string]$Path) {
    $Bytes = [IO.File]::ReadAllBytes($Path)
    $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
    $Utf8 = [Text.UTF8Encoding]::new($false, $true)
    $Text = $Utf8.GetString($Bytes, $Offset, $Bytes.Length - $Offset).Replace("`r`n", "`n").Replace("`r", "`n")
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Text))).ToLowerInvariant()
}
function Invoke-HBPair([string]$Kind, [string]$Path, [string]$Repo, [int]$Expected, [string]$Case) {
    $Stem = if ($Kind -eq 'Receipt') { 'validate-intake-authoring-receipt' } else { 'validate-intake-authoring-artifact' }
    $PowerShellArgs = if ($Kind -eq 'Receipt') { @('-Receipt', $Path, '-Repo', $Repo) } else { @('-Artifact', $Path, '-Repo', $Repo) }
    $Output = & pwsh -NoProfile -File (Join-Path $PresetRoot "scripts/${Stem}.ps1") @PowerShellArgs 2>&1
    if ($LASTEXITCODE -ne $Expected) { throw "${Case}: PowerShell expected $Expected, got ${LASTEXITCODE}: $($Output -join ' | ')" }
    if (Get-Command bash -ErrorAction SilentlyContinue) {
        $BashArgs = if ($Kind -eq 'Receipt') { @('--receipt', $Path, '--repo', $Repo) } else { @('--artifact', $Path, '--repo', $Repo) }
        $Output = & bash (Join-Path $PresetRoot "scripts/${Stem}.sh") @BashArgs 2>&1
        if ($LASTEXITCODE -ne $Expected) { throw "${Case}: Bash expected $Expected, got ${LASTEXITCODE}: $($Output -join ' | ')" }
    }
}
function New-HBIntake([string]$Path) {
    return @"
<!-- intake-authoring:begin -->
# Lifecycle Fixture

## Purpose
Validate lifecycle evidence.

## Current State
No reviewed intake exists.

## Target State
The intake is ready for review.

## Scope
One deterministic fixture.

## Non-Goals
No product implementation.

## Requirements
- FR-001: Validate lifecycle metadata.

## Quality And Governance
Use strict UTF-8 and text-first evidence.

## Dependencies And Risks
No runtime dependency.

## Expected Artifacts And Evidence
One receipt.

## Acceptance Criteria
- AC-001: Both validators pass.

## Assumptions And Open Questions
No open question.

<!-- intake-authoring:prompts -->
## Copy-Ready Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
### Specify
``````text
`$speckit-specify Use $Path as binding input.
``````

<!-- spec-kit-command-id: speckit.autonomous -->
### Autonomous
``````text
`$speckit-autonomous Use $Path as binding input. Delivery authority: LocalImplementation.
``````
<!-- intake-authoring:end -->
"@
}

$Root = Join-Path ([IO.Path]::GetTempPath()) ("intake-authoring-lifecycle-" + [Guid]::NewGuid())
try {
    New-Item -ItemType Directory -Path $Root | Out-Null
    $IntakeId = [Guid]::NewGuid().ToString()
    $OperationId = [Guid]::NewGuid().ToString()
    $ReceiptId = [Guid]::NewGuid().ToString()
    $TargetRelative = 'intakes/url-source.md'
    $ReceiptRelative = 'specs/intake-authoring-receipts/url-source.json'
    $Target = Join-Path $Root $TargetRelative
    $ReceiptPath = Join-Path $Root $ReceiptRelative
    Write-HBText $Target (New-HBIntake $TargetRelative)
    $Receipt = [ordered]@{
        schemaVersion = '2.0'; documentType = 'IntakeReceipt'; receiptId = $ReceiptId; intakeId = $IntakeId
        generator = [ordered]@{ preset = 'intake-authoring-governance'; version = '0.2.0' }
        createdAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ')
        operation = [ordered]@{ operationId = $OperationId; type = 'Create'; authorityEvidence = 'Explicit fixture create' }
        status = 'ReadyForReview'
        target = [ordered]@{ path = $TargetRelative; normalizedSha256 = Get-HBHash $Target }
        sources = @([ordered]@{
            sourceId = 'SRC001'; order = 1; kind = 'Url'; label = 'SQLite documentation'; location = 'RemoteSnapshot'; path = 'N/A'
            requestedUrl = 'https://www.sqlite.org/docs.html'; finalUrl = 'https://www.sqlite.org/docs.html'
            retrievedAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ'); httpStatus = 200; contentType = 'text/html'
            contentLength = 1024; etag = 'N/A'; lastModified = 'N/A'; redirectChain = @()
            rawSha256 = ('1' * 64); normalizedSha256 = ('2' * 64); gitBlob = 'N/A'; proofBoundary = 'RemoteSnapshot'
        })
        profile = 'generic-markdown'; languagePolicy = 'RepositoryPolicyOrDominantSource'
        decisions = @(); openDecisionIds = @(); questionCount = 0
        agentSurface = [ordered]@{
            specifyCanonicalId = 'speckit.specify'; specifyInvocation = '$speckit-specify'
            autonomousCanonicalId = 'speckit.autonomous'; autonomousInvocation = '$speckit-autonomous'
        }
        deliveryAuthority = 'LocalImplementation'; authorityEvidence = 'Default: no explicit remote authority'; promptState = 'Enabled'
        provenanceMode = 'New'
        supersedes = [ordered]@{ receiptPath = 'N/A'; targetNormalizedSha256 = 'N/A'; archiveTargetPath = 'N/A'; archiveReceiptPath = 'N/A' }
        legacyAdoption = [ordered]@{ evidenceType = 'N/A'; priorTargetNormalizedSha256 = 'N/A'; priorGitBlob = 'N/A' }
        updateAuthorized = $false; updateAuthorityEvidence = 'N/A'
        series = [ordered]@{ seriesId = 'N/A'; manifestPath = 'N/A'; order = 'N/A'; role = 'N/A'; supersedesIntakeIds = @() }
        nextAction = '$speckit-intake-review intakes/url-source.md'
    }
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'schema 2 URL receipt'
    $Receipt.sources[0].requestedUrl = 'https://127.0.0.1/private'
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'private URL rejection'
    $Receipt.sources[0].requestedUrl = 'https://www.sqlite.org/docs.html'
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)

    $SecondId = [Guid]::NewGuid().ToString()
    $SecondTargetRelative = 'intakes/second.md'
    $SecondReceiptRelative = 'specs/intake-authoring-receipts/second.json'
    $SecondTarget = Join-Path $Root $SecondTargetRelative
    Write-HBText $SecondTarget (New-HBIntake $SecondTargetRelative)
    Write-HBText (Join-Path $Root $SecondReceiptRelative) '{}'
    $ReviewRelative = 'specs/intake-authoring-series/example/intake-review-request.json'
    Write-HBText (Join-Path $Root $ReviewRelative) '{}'
    $ProposalRelative = 'specs/intake-authoring-operations/proposal.json'
    Write-HBText (Join-Path $Root $ProposalRelative) '{"approved":true}'
    $SeriesId = [Guid]::NewGuid().ToString()
    $Series = [ordered]@{
        schemaVersion = '1.0'; documentType = 'IntakeSeries'; seriesId = $SeriesId; version = 1; status = 'ReadyForReview'
        operationId = $OperationId; proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
        approval = [ordered]@{ approved = $true; approvedBy = 'Thorsten'; approvedAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ'); evidence = 'Explicit fixture approval' }
        orderedIntakeIds = @($IntakeId, $SecondId); roots = @($IntakeId)
        members = @(
            [ordered]@{ intakeId = $IntakeId; path = $TargetRelative; receiptPath = $ReceiptRelative; title = 'Root'; role = 'Foundation'; order = 1; normalizedSha256 = Get-HBHash $Target; supersedesIntakeIds = @() },
            [ordered]@{ intakeId = $SecondId; path = $SecondTargetRelative; receiptPath = $SecondReceiptRelative; title = 'Second'; role = 'Follow-up'; order = 2; normalizedSha256 = Get-HBHash $SecondTarget; supersedesIntakeIds = @() }
        )
        edges = @([ordered]@{ from = $IntakeId; to = $SecondId; kind = 'Precedes' })
        coverage = @([ordered]@{ sourceId = 'SRC001'; topic = 'SQLite docs'; intakeIds = @($IntakeId, $SecondId); disposition = 'Covered' })
        overlapDecisions = @(); reviewRequestPath = $ReviewRelative; nextAction = '$speckit-intake-review the approved series'
    }
    $SeriesPath = Join-Path $Root 'specs/intake-authoring-series/example/series.json'
    Write-HBText $SeriesPath ($Series | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $SeriesPath $Root 0 'approved series'
    $Series.roots = @($SecondId)
    Write-HBText $SeriesPath ($Series | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $SeriesPath $Root 2 'invalid root'
    $Series.roots = @($IntakeId)
    Write-HBText $SeriesPath ($Series | ConvertTo-Json -Depth 30)

    $Operation = [ordered]@{
        schemaVersion = '1.0'; documentType = 'IntakeOperation'; operationId = $OperationId; type = 'CreateSeries'; status = 'Completed'
        createdAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ'); authorityEvidence = 'Explicit fixture approval'
        proposalPath = $ProposalRelative; proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
        approval = [ordered]@{ approved = $true; approvedBy = 'Thorsten'; approvedAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ'); evidence = 'Explicit fixture approval' }
        stagingDirectory = 'specs/intake-authoring-operations/staging'
        intendedTargets = @($TargetRelative, $SecondTargetRelative); validatedTargets = @($TargetRelative, $SecondTargetRelative); publishedTargets = @($TargetRelative, $SecondTargetRelative)
        rollbackBoundary = 'No active target changes before validation'; failure = [ordered]@{ class = 'N/A'; message = 'N/A' }; nextAction = '$speckit-intake-review'
    }
    $OperationPath = Join-Path $Root 'specs/intake-authoring-operations/operation.json'
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 0 'completed transaction'
    $Operation.publishedTargets = @($TargetRelative)
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'partial publication rejection'

    $ArchivedTarget = 'specs/intake-authoring-archive/example/target.md'
    $ArchivedReceipt = 'specs/intake-authoring-archive/example/receipt.json'
    New-Item -ItemType Directory -Path (Split-Path -Parent (Join-Path $Root $ArchivedTarget)) -Force | Out-Null
    Move-Item $Target (Join-Path $Root $ArchivedTarget)
    Move-Item $ReceiptPath (Join-Path $Root $ArchivedReceipt)
    $Tombstone = [ordered]@{
        schemaVersion = '1.0'; documentType = 'IntakeTombstone'; tombstoneId = [Guid]::NewGuid().ToString(); intakeId = $IntakeId; operationId = [Guid]::NewGuid().ToString()
        deletedAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddTHH:mm:ssZ'); reason = 'Fixture deletion'; deleteAuthorityEvidence = 'Explicit fixture delete'
        original = [ordered]@{ targetPath = $TargetRelative; targetNormalizedSha256 = Get-HBHash (Join-Path $Root $ArchivedTarget); receiptPath = $ReceiptRelative; receiptNormalizedSha256 = Get-HBHash (Join-Path $Root $ArchivedReceipt) }
        archive = [ordered]@{ targetPath = $ArchivedTarget; targetNormalizedSha256 = Get-HBHash (Join-Path $Root $ArchivedTarget); receiptPath = $ArchivedReceipt; receiptNormalizedSha256 = Get-HBHash (Join-Path $Root $ArchivedReceipt) }
        seriesImpact = [ordered]@{ seriesId = 'N/A'; migrationOperationId = 'N/A' }
        reactivationBoundary = 'Explicit Update from archived evidence'; nextAction = 'N/A'
    }
    $TombstonePath = Join-Path $Root 'specs/intake-authoring-tombstones/example.json'
    Write-HBText $TombstonePath ($Tombstone | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $TombstonePath $Root 0 'logical delete'
    Copy-Item (Join-Path $Root $ArchivedTarget) $Target
    Invoke-HBPair Artifact $TombstonePath $Root 2 'active target after delete rejection'

    Write-Host 'PASS: schema 2 URL, series graph, transaction, tombstone, and Bash/PowerShell parity'
} finally {
    Remove-Item -LiteralPath $Root -Recurse -Force -ErrorAction SilentlyContinue
}
