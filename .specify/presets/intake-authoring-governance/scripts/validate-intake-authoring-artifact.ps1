<#
.SYNOPSIS
Prueft Series-, Operations- und Tombstone-Artefakte des Intake Authoring.

Validates Intake Authoring series, operation, and tombstone artifacts.
.PARAMETER Artifact
Pfad zum JSON-Artefakt. Path to the JSON artifact.
.PARAMETER Repo
Repository-Wurzel fuer relative Pfade. Repository root for relative paths.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Artifact,
    [string]$Repo = '.'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$Errors = [Collections.Generic.List[string]]::new()
$RepoRoot = [IO.Path]::GetFullPath($Repo)

function Get-HBProperty([object]$Object, [string]$Name) {
    if ($null -eq $Object) { return $null }
    $Property = $Object.PSObject.Properties[$Name]
    if ($null -eq $Property) { return $null }
    return $Property.Value
}
function Get-HBText([object]$Object, [string]$Name, [string]$Label) {
    $Value = Get-HBProperty $Object $Name
    if ($Value -is [DateTime]) {
        return $Value.ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ss.FFFFFFFZ', [Globalization.CultureInfo]::InvariantCulture)
    }
    if ($Value -is [DateTimeOffset]) {
        return $Value.UtcDateTime.ToString('yyyy-MM-ddTHH:mm:ss.FFFFFFFZ', [Globalization.CultureInfo]::InvariantCulture)
    }
    if ($Value -isnot [string] -or [string]::IsNullOrWhiteSpace($Value)) {
        $Errors.Add("${Label}.${Name} must be a non-empty string")
        return ''
    }
    return $Value.Trim()
}
function Test-HBRelative([string]$Value) {
    return -not [IO.Path]::IsPathRooted($Value) -and -not ($Value -split '[\\/]' | Where-Object { $_ -eq '..' })
}
function Get-HBHash([string]$Path) {
    $Bytes = [IO.File]::ReadAllBytes($Path)
    $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
    $Utf8 = [Text.UTF8Encoding]::new($false, $true)
    $Text = $Utf8.GetString($Bytes, $Offset, $Bytes.Length - $Offset)
    if ($Text.Contains([char]0)) { throw 'binary NUL detected' }
    $Normalized = $Text.Replace("`r`n", "`n").Replace("`r", "`n")
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Normalized))).ToLowerInvariant()
}
function Test-HBHash([string]$Value, [string]$Label) {
    if ($Value -notmatch '^[0-9a-f]{64}$') { $Errors.Add("${Label} must be a lowercase SHA-256") }
}
function Test-HBUuid([string]$Value, [string]$Label) {
    $Parsed = [Guid]::Empty
    if (-not [Guid]::TryParse($Value, [ref]$Parsed) -or $Parsed -eq [Guid]::Empty) {
        $Errors.Add("${Label} must be a non-zero UUID")
    }
}
function Test-HBTime([string]$Value, [string]$Label) {
    $Parsed = [DateTimeOffset]::MinValue
    if (-not $Value.EndsWith('Z') -or -not [DateTimeOffset]::TryParse($Value, [ref]$Parsed)) {
        $Errors.Add("${Label} must be an ISO-8601 UTC timestamp")
    }
}
function Test-HBFile([string]$Value, [string]$Label, [string]$Hash = '', [switch]$Absent) {
    if (-not (Test-HBRelative $Value)) { $Errors.Add("${Label} must be repository-relative"); return }
    $Path = Join-Path $RepoRoot $Value
    if ($Absent) {
        if (Test-Path -LiteralPath $Path) { $Errors.Add("${Label} must no longer exist") }
        return
    }
    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) { $Errors.Add("${Label} is missing: $Value"); return }
    if ($Hash) {
        Test-HBHash $Hash "${Label} hash"
        try { $Actual = Get-HBHash $Path } catch { $Errors.Add("${Label} is not strict UTF-8 text: $($_.Exception.Message)"); return }
        if ($Actual -ne $Hash) { $Errors.Add("${Label} hash drift") }
    }
}

try {
    $Data = (Get-Content -LiteralPath $Artifact -Raw -Encoding UTF8) | ConvertFrom-Json
} catch {
    [Console]::Error.WriteLine("ERROR: invalid artifact: $($_.Exception.Message)")
    exit 2
}
if ((Get-HBProperty $Data 'schemaVersion') -ne '1.0') { $Errors.Add('schemaVersion must be 1.0') }
$DocumentType = Get-HBText $Data 'documentType' 'artifact'

switch ($DocumentType) {
    'IntakeSeries' {
        Test-HBUuid (Get-HBText $Data 'seriesId' 'series') 'seriesId'
        $Version = Get-HBProperty $Data 'version'
        if ($Version -is [bool] -or $Version -isnot [long] -or $Version -lt 1) { $Errors.Add('version must be a positive integer') }
        $Status = Get-HBText $Data 'status' 'series'
        if ($Status -notin @('Proposed', 'Approved', 'ReadyForReview', 'NeedsClarification', 'Superseded')) { $Errors.Add('series.status is invalid') }
        Test-HBUuid (Get-HBText $Data 'operationId' 'series') 'operationId'
        Test-HBHash (Get-HBText $Data 'proposalNormalizedSha256' 'series') 'proposalNormalizedSha256'
        $Approval = Get-HBProperty $Data 'approval'
        $Approved = Get-HBProperty $Approval 'approved'
        if ($Approved -isnot [bool]) { $Errors.Add('approval.approved must be boolean') }
        if ($Approved) {
            if ((Get-HBText $Approval 'approvedBy' 'approval') -eq 'N/A') { $Errors.Add('approved series requires approvedBy') }
            $ApprovedAt = Get-HBText $Approval 'approvedAt' 'approval'
            if ($ApprovedAt -eq 'N/A') { $Errors.Add('approved series requires approvedAt') } else { Test-HBTime $ApprovedAt 'approval.approvedAt' }
            if ((Get-HBText $Approval 'evidence' 'approval') -eq 'N/A') { $Errors.Add('approved series requires approval evidence') }
        }
        $Ordered = @(Get-HBProperty $Data 'orderedIntakeIds')
        $Members = @(Get-HBProperty $Data 'members')
        $Roots = @(Get-HBProperty $Data 'roots')
        $Edges = @(Get-HBProperty $Data 'edges')
        if (-not $Ordered.Count) { $Errors.Add('orderedIntakeIds must be a non-empty array') }
        if (-not $Members.Count) { $Errors.Add('members must be a non-empty array') }
        if (-not $Roots.Count) { $Errors.Add('roots must be a non-empty array') }
        $MemberIds = [Collections.Generic.List[string]]::new()
        $Positions = @{}
        for ($Index = 0; $Index -lt $Members.Count; $Index++) {
            $Member = $Members[$Index]
            $Label = "members[$Index]"
            $IntakeId = Get-HBText $Member 'intakeId' $Label
            Test-HBUuid $IntakeId "${Label}.intakeId"
            if ($Positions.ContainsKey($IntakeId)) { $Errors.Add("${Label}.intakeId must be unique") }
            $Positions[$IntakeId] = $Index
            $MemberIds.Add($IntakeId)
            Test-HBFile (Get-HBText $Member 'path' $Label) "${Label}.path" (Get-HBText $Member 'normalizedSha256' $Label)
            Test-HBFile (Get-HBText $Member 'receiptPath' $Label) "${Label}.receiptPath"
            [void](Get-HBText $Member 'title' $Label)
            [void](Get-HBText $Member 'role' $Label)
            if ((Get-HBProperty $Member 'order') -ne ($Index + 1)) { $Errors.Add("${Label}.order must be $($Index + 1)") }
            foreach ($Previous in @(Get-HBProperty $Member 'supersedesIntakeIds')) { Test-HBUuid ([string]$Previous) "${Label}.supersedesIntakeIds[]" }
        }
        if (($Ordered -join '|') -ne ($MemberIds -join '|') -or @($Ordered | Select-Object -Unique).Count -ne $Ordered.Count) {
            $Errors.Add('orderedIntakeIds must contain every member exactly once in member order')
        }
        $Incoming = @{}; $Adjacency = @{}; $Pairs = [Collections.Generic.HashSet[string]]::new()
        foreach ($Id in $MemberIds) { $Incoming[$Id] = 0; $Adjacency[$Id] = [Collections.Generic.List[string]]::new() }
        for ($Index = 0; $Index -lt $Edges.Count; $Index++) {
            $Edge = $Edges[$Index]; $Label = "edges[$Index]"
            $From = Get-HBText $Edge 'from' $Label; $To = Get-HBText $Edge 'to' $Label; [void](Get-HBText $Edge 'kind' $Label)
            if (-not $Positions.ContainsKey($From) -or -not $Positions.ContainsKey($To)) { $Errors.Add("${Label} references an unknown member"); continue }
            if ($From -eq $To) { $Errors.Add("${Label} must not be a self-edge") }
            if (-not $Pairs.Add("${From}|${To}")) { $Errors.Add("${Label} duplicates an edge") }
            if ($Positions[$From] -ge $Positions[$To]) { $Errors.Add("${Label} contradicts member order") }
            $Incoming[$To]++; $Adjacency[$From].Add($To)
        }
        $ExpectedRoots = @($MemberIds | Where-Object { $Incoming[$_] -eq 0 })
        if (($Roots | Sort-Object) -join '|' -ne (($ExpectedRoots | Sort-Object) -join '|') -or @($Roots | Select-Object -Unique).Count -ne $Roots.Count) {
            $Errors.Add('roots must exactly match unique members without incoming edges')
        }
        $State = @{}
        function Test-HBCycle([string]$Node) {
            $State[$Node] = 1
            foreach ($Child in $Adjacency[$Node]) {
                if ($State[$Child] -eq 1) { return $true }
                if (-not $State.ContainsKey($Child) -and (Test-HBCycle $Child)) { return $true }
            }
            $State[$Node] = 2
            return $false
        }
        foreach ($Id in $MemberIds) {
            if (-not $State.ContainsKey($Id) -and (Test-HBCycle $Id)) { $Errors.Add('series graph must be acyclic'); break }
        }
        $Coverage = @(Get-HBProperty $Data 'coverage')
        if (-not $Coverage.Count) { $Errors.Add('coverage must be a non-empty array') }
        $CoverageIds = [Collections.Generic.HashSet[string]]::new()
        for ($Index = 0; $Index -lt $Coverage.Count; $Index++) {
            $Row = $Coverage[$Index]; $Label = "coverage[$Index]"
            if (-not $CoverageIds.Add((Get-HBText $Row 'sourceId' $Label))) { $Errors.Add("${Label}.sourceId must be unique") }
            [void](Get-HBText $Row 'topic' $Label)
            $References = @(Get-HBProperty $Row 'intakeIds')
            if (-not $References.Count -or @($References | Where-Object { -not $Positions.ContainsKey($_) }).Count) { $Errors.Add("${Label}.intakeIds must reference known members") }
            if ((Get-HBText $Row 'disposition' $Label) -notin @('Covered', 'ExcludedWithRationale', 'NeedsClarification')) { $Errors.Add("${Label}.disposition is invalid") }
        }
        if ($Status -in @('Approved', 'ReadyForReview') -and $Approved -ne $true) { $Errors.Add('approved or ready series requires explicit approval') }
        if ($Status -eq 'ReadyForReview' -and @($Coverage | Where-Object { (Get-HBProperty $_ 'disposition') -eq 'NeedsClarification' }).Count) {
            $Errors.Add('ReadyForReview cannot contain unresolved coverage')
        }
        $ReviewPath = Get-HBText $Data 'reviewRequestPath' 'series'
        if ($Status -eq 'ReadyForReview') { Test-HBFile $ReviewPath 'reviewRequestPath' }
        [void](Get-HBText $Data 'nextAction' 'series')
    }
    'IntakeOperation' {
        Test-HBUuid (Get-HBText $Data 'operationId' 'operation') 'operationId'
        if ((Get-HBText $Data 'type' 'operation') -notin @('Create', 'Update', 'Delete', 'CreateSeries', 'UpdateSeries', 'DeleteSeries')) { $Errors.Add('operation.type is invalid') }
        $Status = Get-HBText $Data 'status' 'operation'
        if ($Status -notin @('Proposed', 'Approved', 'Applying', 'Completed', 'Failed')) { $Errors.Add('operation.status is invalid') }
        Test-HBTime (Get-HBText $Data 'createdAt' 'operation') 'createdAt'
        [void](Get-HBText $Data 'authorityEvidence' 'operation')
        Test-HBFile (Get-HBText $Data 'proposalPath' 'operation') 'proposalPath' (Get-HBText $Data 'proposalNormalizedSha256' 'operation')
        $Approval = Get-HBProperty $Data 'approval'; $Approved = Get-HBProperty $Approval 'approved'
        if ($Approved -isnot [bool]) { $Errors.Add('approval.approved must be boolean') }
        $Intended = @(Get-HBProperty $Data 'intendedTargets'); $Validated = @(Get-HBProperty $Data 'validatedTargets'); $Published = @(Get-HBProperty $Data 'publishedTargets')
        foreach ($Entry in @(@($Intended, 'intendedTargets'), @($Validated, 'validatedTargets'), @($Published, 'publishedTargets'))) {
            $Values = @($Entry[0]); $Label = [string]$Entry[1]
            if (@($Values | Select-Object -Unique).Count -ne $Values.Count -or @($Values | Where-Object { $_ -isnot [string] -or -not (Test-HBRelative $_) }).Count) {
                $Errors.Add("${Label} must be a unique repository-relative path array")
            }
        }
        if ($Status -eq 'Completed') {
            if ($Approved -ne $true) { $Errors.Add('Completed operation requires explicit approval') }
            if (($Intended -join '|') -ne ($Validated -join '|') -or ($Intended -join '|') -ne ($Published -join '|')) {
                $Errors.Add('Completed operation requires intended, validated, and published targets to match')
            }
        }
        [void](Get-HBText $Data 'rollbackBoundary' 'operation')
        $Failure = Get-HBProperty $Data 'failure'
        if ($Status -eq 'Failed' -and ((Get-HBText $Failure 'class' 'failure') -eq 'N/A' -or (Get-HBText $Failure 'message' 'failure') -eq 'N/A')) {
            $Errors.Add('Failed operation requires failure class and message')
        }
        [void](Get-HBText $Data 'nextAction' 'operation')
    }
    'IntakeTombstone' {
        Test-HBUuid (Get-HBText $Data 'tombstoneId' 'tombstone') 'tombstoneId'
        Test-HBUuid (Get-HBText $Data 'intakeId' 'tombstone') 'intakeId'
        Test-HBUuid (Get-HBText $Data 'operationId' 'tombstone') 'operationId'
        Test-HBTime (Get-HBText $Data 'deletedAt' 'tombstone') 'deletedAt'
        [void](Get-HBText $Data 'reason' 'tombstone'); [void](Get-HBText $Data 'deleteAuthorityEvidence' 'tombstone')
        $Original = Get-HBProperty $Data 'original'; $Archive = Get-HBProperty $Data 'archive'
        $OriginalTargetHash = Get-HBText $Original 'targetNormalizedSha256' 'original'
        $OriginalReceiptHash = Get-HBText $Original 'receiptNormalizedSha256' 'original'
        Test-HBHash $OriginalTargetHash 'original.targetNormalizedSha256'; Test-HBHash $OriginalReceiptHash 'original.receiptNormalizedSha256'
        Test-HBFile (Get-HBText $Original 'targetPath' 'original') 'original.targetPath' -Absent
        Test-HBFile (Get-HBText $Original 'receiptPath' 'original') 'original.receiptPath' -Absent
        $ArchiveTargetHash = Get-HBText $Archive 'targetNormalizedSha256' 'archive'
        $ArchiveReceiptHash = Get-HBText $Archive 'receiptNormalizedSha256' 'archive'
        Test-HBFile (Get-HBText $Archive 'targetPath' 'archive') 'archive.targetPath' $ArchiveTargetHash
        Test-HBFile (Get-HBText $Archive 'receiptPath' 'archive') 'archive.receiptPath' $ArchiveReceiptHash
        if ($ArchiveTargetHash -ne $OriginalTargetHash -or $ArchiveReceiptHash -ne $OriginalReceiptHash) { $Errors.Add('archive hashes must preserve the original target and receipt hashes') }
        $Series = Get-HBProperty $Data 'seriesImpact'
        $SeriesId = Get-HBText $Series 'seriesId' 'seriesImpact'; $MigrationId = Get-HBText $Series 'migrationOperationId' 'seriesImpact'
        if (($SeriesId -eq 'N/A') -ne ($MigrationId -eq 'N/A')) { $Errors.Add('seriesImpact fields must both be N/A or UUIDs') }
        if ($SeriesId -ne 'N/A') { Test-HBUuid $SeriesId 'seriesImpact.seriesId'; Test-HBUuid $MigrationId 'seriesImpact.migrationOperationId' }
        [void](Get-HBText $Data 'reactivationBoundary' 'tombstone'); [void](Get-HBText $Data 'nextAction' 'tombstone')
    }
    default { $Errors.Add('documentType must be IntakeSeries, IntakeOperation, or IntakeTombstone') }
}

if ($Errors.Count) {
    foreach ($Message in $Errors) { [Console]::Error.WriteLine("ERROR: $Message") }
    exit 2
}
Write-Host "PASS: $DocumentType artifact is valid ($Artifact)"
