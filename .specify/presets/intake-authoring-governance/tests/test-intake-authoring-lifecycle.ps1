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
function Invoke-HBPair([string]$Kind, [string]$Path, [string]$Repo, [int]$Expected, [string]$Case, [string]$ExpectedError = '') {
    $Stem = if ($Kind -eq 'Receipt') { 'validate-intake-authoring-receipt' } else { 'validate-intake-authoring-artifact' }
    $PowerShellArgs = if ($Kind -eq 'Receipt') { @('-Receipt', $Path, '-Repo', $Repo) } else { @('-Artifact', $Path, '-Repo', $Repo) }
    $Output = & pwsh -NoProfile -File (Join-Path $PresetRoot "scripts/${Stem}.ps1") @PowerShellArgs 2>&1
    if ($LASTEXITCODE -ne $Expected) { throw "${Case}: PowerShell expected $Expected, got ${LASTEXITCODE}: $($Output -join ' | ')" }
    if ($ExpectedError -and -not ($Output -join ' | ').Contains($ExpectedError)) { throw "${Case}: PowerShell missing expected diagnostic ${ExpectedError}" }
    if (Get-Command bash -ErrorAction SilentlyContinue) {
        $BashArgs = if ($Kind -eq 'Receipt') { @('--receipt', $Path, '--repo', $Repo) } else { @('--artifact', $Path, '--repo', $Repo) }
        $Output = & bash (Join-Path $PresetRoot "scripts/${Stem}.sh") @BashArgs 2>&1
        if ($LASTEXITCODE -ne $Expected) { throw "${Case}: Bash expected $Expected, got ${LASTEXITCODE}: $($Output -join ' | ')" }
        if ($ExpectedError -and -not ($Output -join ' | ').Contains($ExpectedError)) { throw "${Case}: Bash missing expected diagnostic ${ExpectedError}" }
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
$OutsideRoot = "${Root}-outside"
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
        generator = [ordered]@{ preset = 'intake-authoring-governance'; version = '0.3.2' }
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
    $CurrentVersion = [regex]::Match((Get-Content (Join-Path $PresetRoot 'preset.yml') -Raw), '(?m)^\s+version:\s*"?([^"\s]+)').Groups[1].Value
    if (-not $CurrentVersion) { throw 'Missing current preset version' }
    $OriginalVersion = $Receipt.generator.version
    $Receipt.generator.version = $CurrentVersion
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 20)
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'current release generator'
    # DE: Die echte Paketvorlage verwenden; reine Versionsparitaet reicht nicht.
    # EN: Exercise the shipped template; version equality alone is insufficient.
    $ShippedReceipt = Get-Content (Join-Path $PresetRoot 'templates/intake-authoring-receipt-template.json') -Raw | ConvertFrom-Json -AsHashtable
    foreach ($Key in $Receipt.Keys) {
        if ($Key -notin @('generator', 'schemaVersion')) { $ShippedReceipt[$Key] = $Receipt[$Key] }
    }
    if ($ShippedReceipt.generator.version -ne $CurrentVersion -or $ShippedReceipt.schemaVersion -ne '2.0') {
        throw 'Shipped receipt template does not bind the current schema-2 release'
    }
    foreach ($EncodingCase in @('LF', 'CRLF', 'BOM')) {
        $TemplateJson = ($ShippedReceipt | ConvertTo-Json -Depth 30).Replace("`r`n", "`n").Replace("`r", "`n")
        if ($EncodingCase -eq 'CRLF') { $TemplateJson = $TemplateJson.Replace("`n", "`r`n") }
        [IO.File]::WriteAllText($ReceiptPath, $TemplateJson, [Text.UTF8Encoding]::new($EncodingCase -eq 'BOM'))
        $BeforeReceipt = (Get-FileHash -LiteralPath $ReceiptPath -Algorithm SHA256).Hash
        $BeforeTarget = (Get-FileHash -LiteralPath $Target -Algorithm SHA256).Hash
        Invoke-HBPair Receipt $ReceiptPath $Root 0 "shipped template ${EncodingCase}"
        if ((Get-FileHash -LiteralPath $ReceiptPath -Algorithm SHA256).Hash -ne $BeforeReceipt -or
            (Get-FileHash -LiteralPath $Target -Algorithm SHA256).Hash -ne $BeforeTarget) {
            throw 'Receipt validation changed input bytes'
        }
    }
    foreach ($LegacyGenerator in @('0.3.3', '0.3.4', '0.3.5', '0.3.6', '0.3.7', '0.3.8', '0.3.9', '0.3.10', '0.3.11', '0.3.12')) {
        $Receipt.generator.version = $LegacyGenerator
        Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 20)
        Invoke-HBPair Receipt $ReceiptPath $Root 0 "known schema-2 generator ${LegacyGenerator}"
    }
    $Receipt.generator.version = $CurrentVersion
    foreach ($LegacySchema in @('1.0', '1.1')) {
        $Receipt.schemaVersion = $LegacySchema
        Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 20)
        Invoke-HBPair Receipt $ReceiptPath $Root 2 "current generator with legacy schema ${LegacySchema}" 'generator.version'
    }
    $Receipt.schemaVersion = '2.0'
    $Receipt.generator.version = '99.0.0'
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 20)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'unknown generator version' 'generator.version'
    $Receipt.generator.version = $OriginalVersion
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 20)
    $CreateReceipt = ($Receipt | ConvertTo-Json -Depth 30) | ConvertFrom-Json -AsHashtable
    $PriorReceiptRelative = 'specs/intake-authoring-archive/prior-receipt.json'
    $PriorReceiptPath = Join-Path $Root $PriorReceiptRelative
    $CopiedReceiptRelative = 'specs/intake-authoring-archive/copied-receipt.json'
    $CopiedReceiptPath = Join-Path $Root $CopiedReceiptRelative
    $PriorTargetRelative = 'specs/intake-authoring-archive/prior-target.md'
    Write-HBText $PriorReceiptPath ($CreateReceipt | ConvertTo-Json -Depth 30)
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath
    Copy-Item -LiteralPath $Target -Destination (Join-Path $Root $PriorTargetRelative)
    $Receipt.receiptId = [Guid]::NewGuid().ToString()
    $Receipt.operation.type = 'Update'
    $Receipt.provenanceMode = 'Supersession'
    $Receipt.updateAuthorized = $true
    $Receipt.updateAuthorityEvidence = 'Explicit fixture update authority'
    $Receipt.supersedes.receiptPath = $PriorReceiptRelative
    $Receipt.supersedes.targetNormalizedSha256 = $CreateReceipt.target.normalizedSha256
    $Receipt.supersedes.archiveTargetPath = $PriorTargetRelative
    $Receipt.supersedes.archiveReceiptPath = $CopiedReceiptRelative
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'bound supersession predecessor'
    $Receipt.supersedes.archiveReceiptPath = $PriorReceiptRelative
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'archive receipt aliases predecessor' 'supersession predecessor is not bound'
    $Receipt.supersedes.archiveReceiptPath = $CopiedReceiptRelative
    $Receipt.supersedes.archiveTargetPath = $TargetRelative
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'archive target aliases current target' 'supersession predecessor is not bound'
    $Receipt.supersedes.archiveTargetPath = $PriorTargetRelative
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    $AlternateCasePrior = ($CreateReceipt | ConvertTo-Json -Depth 30) | ConvertFrom-Json -AsHashtable
    $AlternateCasePrior.intakeId = ([string]$CreateReceipt.intakeId).ToUpperInvariant()
    Write-HBText $PriorReceiptPath ($AlternateCasePrior | ConvertTo-Json -Depth 30)
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath -Force
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'bound predecessor with alternate UUID case'
    $ForeignPrior = ($CreateReceipt | ConvertTo-Json -Depth 30) | ConvertFrom-Json -AsHashtable
    $ForeignPrior.intakeId = [Guid]::NewGuid().ToString()
    Write-HBText $PriorReceiptPath ($ForeignPrior | ConvertTo-Json -Depth 30)
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath -Force
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'unrelated supersession predecessor' 'supersession predecessor is not bound'
    $InvalidPrior = ($CreateReceipt | ConvertTo-Json -Depth 30) | ConvertFrom-Json -AsHashtable
    [void]$InvalidPrior.Remove('receiptId')
    Write-HBText $PriorReceiptPath ($InvalidPrior | ConvertTo-Json -Depth 30)
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath -Force
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'predecessor without receipt ID' 'supersession predecessor is not bound'
    [IO.File]::WriteAllBytes($PriorReceiptPath, [byte[]](0x7b, 0xff, 0x7d))
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath -Force
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'predecessor with invalid UTF-8' 'supersession predecessor is not bound'
    $SelfPrior = ($CreateReceipt | ConvertTo-Json -Depth 30) | ConvertFrom-Json -AsHashtable
    $SelfPrior.receiptId = ([string]$Receipt.receiptId).ToUpperInvariant()
    Write-HBText $PriorReceiptPath ($SelfPrior | ConvertTo-Json -Depth 30)
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath -Force
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'self predecessor with alternate UUID case' 'supersession predecessor is not bound'
    Write-HBText $PriorReceiptPath ($CreateReceipt | ConvertTo-Json -Depth 30)
    Write-HBText $CopiedReceiptPath ("$([IO.File]::ReadAllText($PriorReceiptPath))`n")
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'archive receipt byte drift' 'supersession predecessor is not bound'
    Copy-Item -LiteralPath $PriorReceiptPath -Destination $CopiedReceiptPath -Force
    Write-HBText (Join-Path $Root $PriorTargetRelative) '# Drifted archived target'
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'archive target hash drift' 'supersession predecessor is not bound'
    $Receipt = $CreateReceipt
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    $ArchiveRelative = 'requirements/archive/url-source.001-completed.md'
    $ConfigPath = Join-Path $Root 'requirements/intake-governance-config.json'
    $ManifestPath = Join-Path $Root 'requirements/series.json'
    $Config = @{
        schemaVersion = '2.0'; documentationLanguage = 'en'; inventoryMode = 'SeriesManifest'
        artifactNaming = @{ profile = 'explicit'; canonicalIndex = 'RequirementsIndex.md'; intakePattern = '<slug>.md'; orderView = 'RequirementsIntakeOrder.md' }
        roles = @{ 'requirements-index' = 'RequirementsIndex.md'; 'requirements-intake' = 'intakes'; 'intake-order' = 'RequirementsIntakeOrder.md'; 'requirements-baseline' = 'requirements/baseline' }
        collections = @{ active = 'intakes'; archive = 'requirements/archive'; baseline = 'requirements/baseline'; backlog = 'requirements/backlog'; history = 'requirements/history'; seriesManifest = 'requirements/series.json' }
        legacyArtifactNames = @()
    }
    Write-HBText (Join-Path $Root 'RequirementsIndex.md') '# Index'
    Write-HBText (Join-Path $Root 'RequirementsIntakeOrder.md') '# Order'
    New-Item -ItemType Directory -Path (Join-Path $Root 'requirements/baseline') -Force | Out-Null
    Write-HBText $ConfigPath ($Config | ConvertTo-Json -Depth 20)
    # DE: Erst normalisieren, damit ein Windows-Checkout kein CRCRLF erzeugt.
    # EN: Normalize first so a Windows checkout cannot produce CRCRLF.
    Write-HBText (Join-Path $Root $ArchiveRelative) ([IO.File]::ReadAllText($Target).Replace("`r`n", "`n").Replace("`r", "`n").Replace("`n", "`r`n"))
    if ((Get-HBHash (Join-Path $Root $ArchiveRelative)) -ne $Receipt.target.normalizedSha256) { throw 'Archive fixture changed normalized content' }
    Remove-Item -LiteralPath $Target
    $Manifest = @{
        status = 'Completed'; seriesId = [guid]::NewGuid().ToString()
        orderedTargets = @(@{ path = $ArchiveRelative; normalizedSha256 = $Receipt.target.normalizedSha256; status = 'Completed' })
        roots = @($ArchiveRelative); dependencies = @()
    }
    Write-HBText $ManifestPath ($Manifest | ConvertTo-Json -Depth 20)
    $ReceiptBefore = Get-FileHash -LiteralPath $ReceiptPath
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'historical standalone receipt / CRLF archive'
    if ($ReceiptBefore.Hash -cne (Get-FileHash -LiteralPath $ReceiptPath).Hash) { throw 'Historical receipt was modified' }
    $OriginalSource = $Receipt.sources[0]
    $SourceArchiveRelative = 'requirements/archive/source.001-completed.md'
    Write-HBText (Join-Path $Root $SourceArchiveRelative) "# Source`n"
    $RepositorySource = [ordered]@{}
    foreach ($Key in $OriginalSource.Keys) { $RepositorySource[$Key] = $OriginalSource[$Key] }
    $RepositorySource.kind = 'File'; $RepositorySource.location = 'Repository'; $RepositorySource.path = 'intakes/source.md'
    foreach ($Key in @('requestedUrl','finalUrl','retrievedAt','httpStatus','contentType','contentLength','rawSha256')) { $RepositorySource[$Key] = 'N/A' }
    $RepositorySource.proofBoundary = 'SnapshotOnly'
    $RepositorySource.normalizedSha256 = Get-HBHash (Join-Path $Root $SourceArchiveRelative)
    $Receipt.sources = @($RepositorySource)
    $Manifest.orderedTargets += @{ path = $SourceArchiveRelative; status = 'Completed'; normalizedSha256 = $RepositorySource.normalizedSha256 }
    Write-HBText $ManifestPath ($Manifest | ConvertTo-Json -Depth 20)
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'archived repository source'
    $Receipt.sources[0].normalizedSha256 = '0' * 64
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'archived source hash mismatch'
    $Receipt.sources = @($OriginalSource)
    $Manifest.orderedTargets = @($Manifest.orderedTargets[0])
    Write-HBText $ManifestPath ($Manifest | ConvertTo-Json -Depth 20)
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    $Receipt.series.seriesId = [guid]::NewGuid().ToString()
    $Receipt.series.manifestPath = 'requirements/series.json'
    $Receipt.series.order = 1
    $Receipt.series.role = 'Primary'
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'foreign series binding'
    $Receipt.series.seriesId = $Manifest.seriesId
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 0 'matching series binding'
    $Receipt.series.seriesId = 'N/A'; $Receipt.series.manifestPath = 'N/A'; $Receipt.series.order = 'N/A'; $Receipt.series.role = 'N/A'
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    $DuplicateRelative = 'requirements/archive/url-source.002-completed.md'
    Copy-Item -LiteralPath (Join-Path $Root $ArchiveRelative) -Destination (Join-Path $Root $DuplicateRelative)
    $Manifest.orderedTargets += @{ path = $DuplicateRelative; normalizedSha256 = $Receipt.target.normalizedSha256; status = 'Completed' }
    Write-HBText $ManifestPath ($Manifest | ConvertTo-Json -Depth 20)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'ambiguous archive successors'
    $Manifest.orderedTargets = @($Manifest.orderedTargets[0])
    Write-HBText $ManifestPath ($Manifest | ConvertTo-Json -Depth 20)
    Write-HBText (Join-Path $Root $ArchiveRelative) '# Drift'
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'archive hash drift'
    Remove-Item -LiteralPath (Join-Path $Root $ArchiveRelative)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'missing archive successor'
    Move-Item -LiteralPath (Join-Path $Root $DuplicateRelative) -Destination (Join-Path $Root $ArchiveRelative)
    $Receipt.target.path = 'intakes/unrelated.md'
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Receipt $ReceiptPath $Root 2 'unrelated archive name'
    $Receipt.target.path = $TargetRelative
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    Move-Item -LiteralPath (Join-Path $Root $ArchiveRelative) -Destination $Target
    Remove-Item -LiteralPath $ConfigPath
    Remove-Item -LiteralPath $ManifestPath
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
    $SecondReceiptPath = Join-Path $Root $SecondReceiptRelative
    $ReviewRelative = 'specs/intake-authoring-series/example/intake-review-request.json'
    Write-HBText (Join-Path $Root $ReviewRelative) '{}'
    $ProposalRelative = 'specs/intake-authoring-operations/proposal.json'
    $Proposal = [ordered]@{ targets = @(
        [ordered]@{ path = $TargetRelative; normalizedSha256 = Get-HBHash $Target; receiptPath = $ReceiptRelative },
        [ordered]@{ path = $SecondTargetRelative; normalizedSha256 = Get-HBHash $SecondTarget; receiptPath = $SecondReceiptRelative }
    ) }
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $SeriesId = [Guid]::NewGuid().ToString()
    $SeriesRelative = 'specs/intake-authoring-series/example/series.json'
    $Receipt.series = [ordered]@{
        seriesId = $SeriesId; manifestPath = $SeriesRelative; order = 1; role = 'Foundation'; supersedesIntakeIds = @()
    }
    Write-HBText $ReceiptPath ($Receipt | ConvertTo-Json -Depth 30)
    $SecondReceipt = ($Receipt | ConvertTo-Json -Depth 30) | ConvertFrom-Json -AsHashtable
    $SecondReceipt.receiptId = [Guid]::NewGuid().ToString()
    $SecondReceipt.intakeId = $SecondId
    $SecondReceipt.target.path = $SecondTargetRelative
    $SecondReceipt.target.normalizedSha256 = Get-HBHash $SecondTarget
    $SecondReceipt.series.order = 2
    $SecondReceipt.series.role = 'Follow-up'
    $SecondReceipt.nextAction = "`$speckit-intake-review $SecondTargetRelative"
    Write-HBText $SecondReceiptPath ($SecondReceipt | ConvertTo-Json -Depth 30)
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
    $SeriesPath = Join-Path $Root $SeriesRelative
    Write-HBText $SeriesPath ($Series | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $SeriesPath $Root 0 'approved series'
    $SecondReceipt.intakeId = [Guid]::NewGuid().ToString()
    Write-HBText $SecondReceiptPath ($SecondReceipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $SeriesPath $Root 2 'unrelated member receipt' 'not bound to this member'
    $SecondReceipt.intakeId = $SecondId
    Write-HBText $SecondReceiptPath ($SecondReceipt | ConvertTo-Json -Depth 30)
    $MinimalReceipt = [ordered]@{
        schemaVersion = '2.0'; documentType = 'IntakeReceipt'; intakeId = $SecondId
        status = 'ReadyForReview'
        target = [ordered]@{ path = $SecondTargetRelative; normalizedSha256 = Get-HBHash $SecondTarget }
        series = $SecondReceipt.series
    }
    Write-HBText $SecondReceiptPath ($MinimalReceipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $SeriesPath $Root 2 'fabricated member receipt' 'failed full validation'
    Write-HBText $SecondReceiptPath ($SecondReceipt | ConvertTo-Json -Depth 30)
    $Series.members[1].order = '2'
    Write-HBText $SeriesPath ($Series | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $SeriesPath $Root 2 'string member order' 'order must be'
    $Series.members[1].order = 2
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
    $LegacyProposal = [ordered]@{ documentType = 'LearningPackageProposal'; orderedMembers = @($Proposal.targets) }
    Write-HBText (Join-Path $Root $ProposalRelative) ($LegacyProposal | ConvertTo-Json -Depth 20)
    $Operation.publicationScope = 'ReusableDraftPackageOnly'
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 0 'bound historical learning proposal'
    $LegacyProposal.orderedMembers[0].normalizedSha256 = ('0' * 64)
    Write-HBText (Join-Path $Root $ProposalRelative) ($LegacyProposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'historical learning proposal with stale target hash' 'hash'
    $Operation.Remove('publicationScope')
    $Proposal.targets[0].normalizedSha256 = Get-HBHash $Target
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    $Proposal.targets = @($Proposal.targets[0])
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    $Operation.type = 'Create'
    $Operation.intendedTargets = @($TargetRelative)
    $Operation.validatedTargets = @($TargetRelative)
    $Operation.publishedTargets = @($TargetRelative)
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 0 'single-target completed create'
    $Proposal.targets = @()
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    $Operation.type = 'Delete'
    $Operation.intendedTargets = @()
    $Operation.validatedTargets = @()
    $Operation.publishedTargets = @()
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 0 'empty-target completed delete'
    $Proposal.targets = @(
        [ordered]@{ path = $TargetRelative; normalizedSha256 = Get-HBHash $Target; receiptPath = $ReceiptRelative },
        [ordered]@{ path = $SecondTargetRelative; normalizedSha256 = Get-HBHash $SecondTarget; receiptPath = $SecondReceiptRelative }
    )
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    $Operation.type = 'CreateSeries'
    $Operation.intendedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.validatedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.publishedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.intendedTargets = @("$TargetRelative|intakes", 'second.md')
    $Operation.validatedTargets = @("$TargetRelative|intakes", 'second.md')
    $Operation.publishedTargets = @("$TargetRelative|intakes", 'second.md')
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'forged target list with delimiter' 'approved proposal'
    $Proposal.targets[0].Remove('normalizedSha256')
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    $Operation.intendedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.validatedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.publishedTargets = @($TargetRelative, $SecondTargetRelative)
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'proposal target without hash' 'normalizedSha256'
    $Proposal.targets[0]['normalizedSha256'] = Get-HBHash $Target
    Write-HBText (Join-Path $Root $ProposalRelative) ($Proposal | ConvertTo-Json -Depth 20)
    $Operation.proposalNormalizedSha256 = Get-HBHash (Join-Path $Root $ProposalRelative)
    $Operation.intendedTargets = @('intakes/unpublished.md')
    $Operation.validatedTargets = @('intakes/unpublished.md')
    $Operation.publishedTargets = @('intakes/unpublished.md')
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'unpublished target reported completed' 'approved proposal'
    $Operation.intendedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.validatedTargets = @($TargetRelative, $SecondTargetRelative)
    $Operation.publishedTargets = @($TargetRelative, $SecondTargetRelative)
    Remove-Item -LiteralPath $SecondTarget
    Write-HBText $OperationPath ($Operation | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'approved target absent after publication' 'is missing'
    Write-HBText $SecondTarget (New-HBIntake $SecondTargetRelative)
    $SecondReceipt.operation.operationId = [Guid]::NewGuid().ToString()
    Write-HBText $SecondReceiptPath ($SecondReceipt | ConvertTo-Json -Depth 30)
    Invoke-HBPair Artifact $OperationPath $Root 2 'unrelated published receipt' 'not bound'
    $SecondReceipt.operation.operationId = $OperationId
    Write-HBText $SecondReceiptPath ($SecondReceipt | ConvertTo-Json -Depth 30)
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
    New-Item -ItemType Directory -Path $OutsideRoot -Force | Out-Null
    $ArchivedTargetPath = Join-Path $Root $ArchivedTarget
    $OutsideArchivedTarget = Join-Path $OutsideRoot 'target.md'
    Copy-Item -LiteralPath $ArchivedTargetPath -Destination $OutsideArchivedTarget
    Remove-Item -LiteralPath $ArchivedTargetPath
    New-Item -ItemType SymbolicLink -Path $ArchivedTargetPath -Target $OutsideArchivedTarget | Out-Null
    Invoke-HBPair Artifact $TombstonePath $Root 2 'external archive symlink' 'outside repository'
    Remove-Item -LiteralPath $ArchivedTargetPath -Force
    Copy-Item -LiteralPath $OutsideArchivedTarget -Destination $ArchivedTargetPath
    Copy-Item (Join-Path $Root $ArchivedTarget) $Target
    Invoke-HBPair Artifact $TombstonePath $Root 2 'active target after delete rejection'

    Write-Host 'PASS: schema 2 URL, series graph, transaction, tombstone, and Bash/PowerShell parity'
} finally {
    Remove-Item -LiteralPath $Root -Recurse -Force -ErrorAction SilentlyContinue
    if ($OutsideRoot) { Remove-Item -LiteralPath $OutsideRoot -Recurse -Force -ErrorAction SilentlyContinue }
}
