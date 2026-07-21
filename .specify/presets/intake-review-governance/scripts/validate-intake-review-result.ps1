<#
.SYNOPSIS
Prueft ein Intake-Review-Ergebnis und aktuelle Ziel-Hashes read-only.

Validates an intake-review result and current target hashes read-only.
.PARAMETER Result
Pfad zur JSON-Ergebnisdatei. Path to the JSON result.
.PARAMETER Repo
Repository-Wurzel. Repository root.
.EXAMPLE
pwsh -NoProfile -File scripts/validate-intake-review-result.ps1 -Result specs/intake-review-result.json -Repo .
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Result,
    [string]$Repo = '.'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Test-IntakeReviewResult {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Result, [Parameter(Mandatory)][string]$Repo)

    $Errors = [System.Collections.Generic.List[string]]::new()
    function Get-Prop([object]$Object, [string]$Name) {
        if ($null -eq $Object) { return $null }
        $Property = $Object.PSObject.Properties[$Name]
        if ($null -eq $Property) { return $null }
        return $Property.Value
    }
    function Get-Text([object]$Object, [string]$Name, [string]$Label) {
        $Value = Get-Prop $Object $Name
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
    function Test-Relative([string]$Value) {
        if ([IO.Path]::IsPathRooted($Value)) { return $false }
        return -not ($Value -split '[\\/]' | Where-Object { $_ -eq '..' })
    }
    function Get-NormalizedHash([string]$Path) {
        $Bytes = [IO.File]::ReadAllBytes($Path)
        $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
        $Utf8 = [Text.UTF8Encoding]::new($false, $true)
        $Text = $Utf8.GetString($Bytes, $Offset, $Bytes.Length - $Offset)
        if ($Text.Contains([char]0)) { throw 'binary NUL detected' }
        $Normalized = $Text.Replace("`r`n", "`n").Replace("`r", "`n")
        $Hash = [Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Normalized))
        return [Convert]::ToHexString($Hash).ToLowerInvariant()
    }

    if (-not (Test-Path -LiteralPath $Result -PathType Leaf)) { throw "Result file not found: $Result" }
    $Data = Get-Content -LiteralPath $Result -Raw -Encoding UTF8 | ConvertFrom-Json
    if ((Get-Prop $Data 'schemaVersion') -ne '1.0') { $Errors.Add('schemaVersion must be 1.0') }
    $ReviewId = Get-Text $Data 'reviewId' 'result'
    $Guid = [Guid]::Empty
    if (-not [Guid]::TryParse($ReviewId, [ref]$Guid) -or $Guid -eq [Guid]::Empty) { $Errors.Add('reviewId must be a non-zero UUID') }
    $Mode = Get-Text $Data 'mode' 'result'
    if ($Mode -notin @('Single', 'Series', 'Campaign')) { $Errors.Add('mode is invalid') }
    $Status = Get-Text $Data 'status' 'result'
    $Accepted = @('Ready', 'ReadyWithAcceptedRisks')
    if ($Status -notin ($Accepted + @('NeedsClarification', 'NeedsRemediation', 'Rejected'))) { $Errors.Add('status is invalid') }
    [void](Get-Text $Data 'policy' 'result')
    $ReviewedAt = Get-Text $Data 'reviewedAt' 'result'
    $Timestamp = [DateTimeOffset]::MinValue
    if (-not $ReviewedAt.EndsWith('Z') -or -not [DateTimeOffset]::TryParse($ReviewedAt, [ref]$Timestamp)) { $Errors.Add('reviewedAt must be an ISO-8601 UTC timestamp') }

    $Targets = @(Get-Prop $Data 'targets')
    if ($Targets.Count -eq 0) { $Errors.Add('targets must be a non-empty array') }
    $TargetPaths = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    for ($Index = 0; $Index -lt $Targets.Count; $Index++) {
        $Target = $Targets[$Index]; $Label = "targets[$Index]"
        $Path = Get-Text $Target 'path' $Label
        $Digest = Get-Text $Target 'normalizedSha256' $Label
        [void](Get-Text $Target 'role' $Label); [void](Get-Text $Target 'gitBlob' $Label)
        if (-not (Test-Relative $Path) -or -not $TargetPaths.Add($Path)) { $Errors.Add("${Label}.path must be unique and repository-relative") }
        if ($Digest -notmatch '^[0-9a-f]{64}$') { $Errors.Add("${Label}.normalizedSha256 is invalid") }
        $TargetPath = Join-Path ([IO.Path]::GetFullPath($Repo)) $Path
        if (-not (Test-Path -LiteralPath $TargetPath -PathType Leaf)) { $Errors.Add("target missing: $Path") }
        else {
            try { $Actual = Get-NormalizedHash $TargetPath } catch { $Errors.Add("target ${Path}: $($_.Exception.Message)"); $Actual = '' }
            if ($Actual -and $Actual -ne $Digest) { $Errors.Add("target hash drift: $Path") }
        }
    }

    $Counts = @{ Critical = 0; High = 0; Medium = 0; Low = 0 }
    $FindingIds = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    $Findings = @(Get-Prop $Data 'findings')
    for ($Index = 0; $Index -lt $Findings.Count; $Index++) {
        $Finding = $Findings[$Index]; $Label = "findings[$Index]"
        $Id = Get-Text $Finding 'id' $Label
        if ($Id -notmatch '^IR[0-9]{3,}$' -or -not $FindingIds.Add($Id)) { $Errors.Add("${Label}.id must be a unique IR### identifier") }
        $Severity = Get-Text $Finding 'severity' $Label
        if (-not $Counts.ContainsKey($Severity)) { $Errors.Add("${Label}.severity is invalid") } else { $Counts[$Severity]++ }
        foreach ($Field in @('category', 'target', 'disposition', 'owner', 'evidence', 'reevaluationTrigger')) { [void](Get-Text $Finding $Field $Label) }
    }

    $Questions = @(Get-Prop $Data 'questions')
    $Risks = @(Get-Prop $Data 'acceptedRisks')
    $Exceptions = @(Get-Prop $Data 'operatorExceptions')
    if ($Status -in $Accepted -and @($Questions | Where-Object { (Get-Prop $_ 'status') -ne 'Answered' }).Count) { $Errors.Add('accepted status cannot have unanswered questions') }
    if ($Status -in $Accepted -and ($Counts.Critical -gt 0 -or $Counts.High -gt 0)) { $Errors.Add('Critical or High findings block an accepted status') }
    if ($Status -eq 'Ready' -and $Risks.Count) { $Errors.Add('Ready cannot contain accepted risks') }
    if ($Status -eq 'ReadyWithAcceptedRisks' -and $Risks.Count -eq 0) { $Errors.Add('ReadyWithAcceptedRisks needs acceptedRisks') }
    for ($Index = 0; $Index -lt $Risks.Count; $Index++) {
        $Risk = $Risks[$Index]; $Label = "acceptedRisks[$Index]"
        if ((Get-Text $Risk 'severity' $Label) -notin @('Medium', 'Low')) { $Errors.Add("${Label}.severity must be Medium or Low") }
        foreach ($Field in @('findingId', 'owner', 'rationale', 'acceptedAt', 'evidence', 'reevaluationTrigger')) { [void](Get-Text $Risk $Field $Label) }
        if ((Get-Prop $Risk 'acceptedByType') -ne 'Human') { $Errors.Add("${Label}.acceptedByType must be Human") }
    }
    for ($Index = 0; $Index -lt $Exceptions.Count; $Index++) {
        $Exception = $Exceptions[$Index]; $Label = "operatorExceptions[$Index]"
        foreach ($Field in @('exceptionId', 'author', 'reason', 'date', 'expiry')) { [void](Get-Text $Exception $Field $Label) }
        $WorkerIds = @(Get-Prop $Exception 'workerIds')
        if ($WorkerIds.Count -eq 0 -or @($WorkerIds | Where-Object { $_ -isnot [string] -or [string]::IsNullOrWhiteSpace($_) }).Count) {
            $Errors.Add("${Label}.workerIds must be a non-empty string array")
        }
        $ExpiryText = Get-Text $Exception 'expiry' $Label
        $ExpiryValue = [DateTimeOffset]::MinValue
        if (-not [DateTimeOffset]::TryParse($ExpiryText, [ref]$ExpiryValue) -or $ExpiryValue -le [DateTimeOffset]::UtcNow) {
            $Errors.Add("${Label}.expiry must be a future ISO-8601 timestamp")
        }
    }

    $Coverage = Get-Prop $Data 'coverage'
    $Individual = @(Get-Prop $Coverage 'individual')
    if ($Individual.Count -ne $TargetPaths.Count -or @($Individual | Where-Object { -not $TargetPaths.Contains([string]$_) }).Count) { $Errors.Add('coverage.individual must contain every target path exactly once') }
    if ($Mode -eq 'Series' -and @(Get-Prop $Coverage 'series').Count -eq 0) { $Errors.Add('Series mode requires series coverage') }
    if ($Mode -eq 'Campaign') {
        $Workers = @(Get-Prop $Coverage 'workers')
        if ($Workers.Count -eq 0) { $Errors.Add('Campaign mode requires worker coverage') }
        $WorkerIds = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
        for ($Index = 0; $Index -lt $Workers.Count; $Index++) {
            $Worker = $Workers[$Index]; $Label = "coverage.workers[$Index]"
            $WorkerId = Get-Text $Worker 'workerId' $Label
            $TargetPath = Get-Text $Worker 'targetPath' $Label
            if (-not $WorkerIds.Add($WorkerId)) { $Errors.Add("duplicate worker coverage: $WorkerId") }
            if (-not $TargetPaths.Contains($TargetPath)) { $Errors.Add("worker target is not reviewed: $TargetPath") }
            [void](Get-Text $Worker 'applicability' $Label)
        }
    }
    $Summary = Get-Prop $Data 'summary'
    foreach ($Severity in $Counts.Keys) {
        $Name = $Severity.ToLowerInvariant()
        if ((Get-Prop $Summary $Name) -ne $Counts[$Severity]) { $Errors.Add("summary.${Name} must equal $($Counts[$Severity])") }
    }
    [void](Get-Text $Data 'supersedes' 'result')

    if ($Errors.Count) { $Errors | ForEach-Object { [Console]::Error.WriteLine("ERROR: $_") }; return 2 }
    [Console]::Out.WriteLine("PASS: intake review $ReviewId is current ($Mode, $Status, $($Targets.Count) targets)")
    return 0
}

$ExitCode = Test-IntakeReviewResult -Result $Result -Repo $Repo
exit $ExitCode
