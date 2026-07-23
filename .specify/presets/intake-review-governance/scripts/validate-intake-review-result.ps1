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
    function Add-GraphError([string]$Code, [string]$Message) {
        $Errors.Add("${Code}: $Message")
    }
    function Test-Relative([string]$Value) {
        if ([IO.Path]::IsPathRooted($Value)) { return $false }
        return -not ($Value -split '[\\/]' | Where-Object { $_ -eq '..' })
    }
    function Get-NormalizedBytes([string]$Path) {
        $Bytes = [IO.File]::ReadAllBytes($Path)
        $Offset = if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) { 3 } else { 0 }
        $Utf8 = [Text.UTF8Encoding]::new($false, $true)
        $Text = $Utf8.GetString($Bytes, $Offset, $Bytes.Length - $Offset)
        if ($Text.Contains([char]0)) { throw 'binary NUL detected' }
        $Normalized = $Text.Replace("`r`n", "`n").Replace("`r", "`n")
        return $Utf8.GetBytes($Normalized)
    }
    function Get-NormalizedHash([string]$Path) {
        $Hash = [Security.Cryptography.SHA256]::HashData((Get-NormalizedBytes $Path))
        return [Convert]::ToHexString($Hash).ToLowerInvariant()
    }

    if (-not (Test-Path -LiteralPath $Result -PathType Leaf)) { throw "Result file not found: $Result" }
    $Data = Get-Content -LiteralPath $Result -Raw -Encoding UTF8 | ConvertFrom-Json
    $SchemaVersion = Get-Prop $Data 'schemaVersion'
    if ($SchemaVersion -notin @('1.0', '1.1')) { $Errors.Add('schemaVersion must be 1.0 or 1.1') }
    $ReviewId = Get-Text $Data 'reviewId' 'result'
    $Guid = [Guid]::Empty
    if (-not [Guid]::TryParse($ReviewId, [ref]$Guid) -or $Guid -eq [Guid]::Empty) { $Errors.Add('reviewId must be a non-zero UUID') }
    $Mode = Get-Text $Data 'mode' 'result'
    if ($Mode -notin @('Single', 'Series', 'Campaign')) { $Errors.Add('mode is invalid') }
    if ($Mode -eq 'Series' -and $SchemaVersion -ne '1.1') {
        Add-GraphError 'IRG001' 'Series result schemaVersion must be 1.1'
    }
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
    $TargetRoles = @{}
    for ($Index = 0; $Index -lt $Targets.Count; $Index++) {
        $Target = $Targets[$Index]; $Label = "targets[$Index]"
        $Path = Get-Text $Target 'path' $Label
        $Digest = Get-Text $Target 'normalizedSha256' $Label
        $Role = Get-Text $Target 'role' $Label
        [void](Get-Text $Target 'gitBlob' $Label)
        if (-not (Test-Relative $Path) -or -not $TargetPaths.Add($Path)) { $Errors.Add("${Label}.path must be unique and repository-relative") }
        $TargetRoles[$Path] = $Role
        if ($Digest -notmatch '^[0-9a-f]{64}$') { $Errors.Add("${Label}.normalizedSha256 is invalid") }
        $TargetPath = Join-Path ([IO.Path]::GetFullPath($Repo)) $Path
        if (-not (Test-Path -LiteralPath $TargetPath -PathType Leaf)) { $Errors.Add("target missing: $Path") }
        else {
            try { $Actual = Get-NormalizedHash $TargetPath } catch { $Errors.Add("target ${Path}: $($_.Exception.Message)"); $Actual = '' }
            if ($Actual -and $Actual -ne $Digest) { $Errors.Add("target hash drift: $Path") }
        }
    }

    $RequestEvidence = Get-Prop $Data 'requestEvidence'
    $RequestBindingRequired = $Mode -eq 'Series' -or $null -ne $RequestEvidence
    if ($RequestBindingRequired) {
        if ($null -eq $RequestEvidence) {
            Add-GraphError 'IRG001' 'requestEvidence must be an object'
        }
        else {
            $RequestPathText = Get-Text $RequestEvidence 'path' 'requestEvidence'
            $RequestDigest = Get-Text $RequestEvidence 'normalizedSha256' 'requestEvidence'
            if ($RequestPathText -and -not (Test-Relative $RequestPathText)) {
                Add-GraphError 'IRG001' 'requestEvidence.path must be repository-relative'
            }
            if ($RequestDigest -notmatch '^[0-9a-f]{64}$') {
                Add-GraphError 'IRG001' 'requestEvidence.normalizedSha256 is invalid'
            }
            $RequestPath = Join-Path ([IO.Path]::GetFullPath($Repo)) $RequestPathText
            $RequestData = $null
            $ActualRequestDigest = ''
            if (-not (Test-Path -LiteralPath $RequestPath -PathType Leaf)) {
                Add-GraphError 'IRG001' "request missing: $RequestPathText"
            }
            else {
                try {
                    $RequestBytes = Get-NormalizedBytes $RequestPath
                    $ActualRequestDigest = [Convert]::ToHexString(
                        [Security.Cryptography.SHA256]::HashData($RequestBytes)
                    ).ToLowerInvariant()
                    $RequestText = [Text.UTF8Encoding]::new($false, $true).GetString($RequestBytes)
                    $RequestData = $RequestText | ConvertFrom-Json
                }
                catch {
                    Add-GraphError 'IRG001' "invalid request: $($_.Exception.Message)"
                }
                if ($ActualRequestDigest -and $ActualRequestDigest -ne $RequestDigest) {
                    Add-GraphError 'IRG001' 'request hash drift'
                }
            }

            if ($null -ne $RequestData) {
                $RequestSchema = Get-Prop $RequestData 'schemaVersion'
                $ExpectedRequestSchemas = if ($Mode -eq 'Series') { @('1.1') } else { @('1.0', '1.1') }
                if ($RequestSchema -notin $ExpectedRequestSchemas) {
                    Add-GraphError 'IRG002' "$Mode request schemaVersion is invalid"
                }
                foreach ($Field in @('reviewId', 'mode', 'policy')) {
                    if ((Get-Prop $RequestData $Field) -ne (Get-Prop $Data $Field)) {
                        Add-GraphError 'IRG002' "request.$Field must match result.$Field"
                    }
                }

                $RequestTargets = @(Get-Prop $RequestData 'targets')
                if ($RequestTargets.Count -eq 0) {
                    Add-GraphError 'IRG003' 'request.targets must be a non-empty array'
                }
                $RequestPaths = [Collections.Generic.List[string]]::new()
                $RequestRoles = @{}
                for ($Index = 0; $Index -lt $RequestTargets.Count; $Index++) {
                    $RequestTarget = $RequestTargets[$Index]
                    $Label = "request.targets[$Index]"
                    $RequestTargetPath = Get-Text $RequestTarget 'path' $Label
                    $RequestRole = Get-Text $RequestTarget 'role' $Label
                    if (-not $RequestTargetPath -or -not (Test-Relative $RequestTargetPath)) {
                        Add-GraphError 'IRG003' "${Label}.path must be repository-relative"
                    }
                    if ($RequestRoles.ContainsKey($RequestTargetPath)) {
                        Add-GraphError 'IRG003' "duplicate request target: $RequestTargetPath"
                    }
                    $RequestPaths.Add($RequestTargetPath)
                    $RequestRoles[$RequestTargetPath] = $RequestRole
                }
                $RequestPathSet = [Collections.Generic.HashSet[string]]::new(
                    [string[]]$RequestPaths,
                    [StringComparer]::Ordinal
                )
                if ($RequestPaths.Count -ne $TargetPaths.Count -or
                    $RequestPathSet.Count -ne $TargetPaths.Count -or
                    @($TargetPaths | Where-Object { -not $RequestPathSet.Contains($_) }).Count) {
                    Add-GraphError 'IRG003' 'request and result target sets must match exactly'
                }
                foreach ($TargetPathValue in $TargetPaths) {
                    if ($RequestRoles.ContainsKey($TargetPathValue) -and
                        $RequestRoles[$TargetPathValue] -ne $TargetRoles[$TargetPathValue]) {
                        Add-GraphError 'IRG003' "request and result roles differ: $TargetPathValue"
                    }
                }

                if ($Mode -eq 'Series') {
                    $SeriesData = Get-Prop $RequestData 'series'
                    if ($null -eq $SeriesData) {
                        Add-GraphError 'IRG004' 'request.series must be an object'
                    }
                    $Ordered = @(Get-Prop $SeriesData 'orderedTargetPaths')
                    $OrderedSet = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
                    $OrderIndex = @{}
                    for ($Index = 0; $Index -lt $Ordered.Count; $Index++) {
                        $OrderedPath = $Ordered[$Index]
                        if ($OrderedPath -isnot [string] -or [string]::IsNullOrWhiteSpace($OrderedPath)) {
                            Add-GraphError 'IRG004' 'series.orderedTargetPaths must contain non-empty strings'
                            continue
                        }
                        if (-not $OrderedSet.Add($OrderedPath)) {
                            Add-GraphError 'IRG004' 'series.orderedTargetPaths contains duplicates'
                        }
                        $OrderIndex[$OrderedPath] = $Index
                    }
                    if ($Ordered.Count -ne $TargetPaths.Count -or
                        $OrderedSet.Count -ne $TargetPaths.Count -or
                        @($TargetPaths | Where-Object { -not $OrderedSet.Contains($_) }).Count) {
                        Add-GraphError 'IRG004' 'series.orderedTargetPaths must contain every target exactly once'
                    }

                    $Roots = @(Get-Prop $SeriesData 'roots')
                    $RootSet = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
                    foreach ($Root in $Roots) {
                        if ($Root -isnot [string] -or [string]::IsNullOrWhiteSpace($Root)) {
                            Add-GraphError 'IRG008' 'series.roots must contain non-empty strings'
                            continue
                        }
                        if (-not $RootSet.Add($Root)) {
                            Add-GraphError 'IRG008' 'series.roots contains duplicates'
                        }
                        if (-not $TargetPaths.Contains($Root)) {
                            Add-GraphError 'IRG008' 'series.roots references an unknown target'
                        }
                    }

                    $Dependencies = @(Get-Prop $SeriesData 'dependencies')
                    $EdgePairs = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
                    $Adjacency = @{}
                    $Indegree = @{}
                    foreach ($TargetPathValue in $TargetPaths) {
                        $Adjacency[$TargetPathValue] = [Collections.Generic.List[string]]::new()
                        $Indegree[$TargetPathValue] = 0
                    }
                    for ($Index = 0; $Index -lt $Dependencies.Count; $Index++) {
                        $Dependency = $Dependencies[$Index]
                        $Label = "series.dependencies[$Index]"
                        $Source = Get-Text $Dependency 'from' $Label
                        $Destination = Get-Text $Dependency 'to' $Label
                        [void](Get-Text $Dependency 'kind' $Label)
                        if (-not $TargetPaths.Contains($Source) -or -not $TargetPaths.Contains($Destination)) {
                            Add-GraphError 'IRG005' "$Label references an unknown target"
                            continue
                        }
                        if ($Source -eq $Destination) {
                            Add-GraphError 'IRG005' "$Label must not be a self-edge"
                            continue
                        }
                        $Pair = "$Source`0$Destination"
                        if (-not $EdgePairs.Add($Pair)) {
                            Add-GraphError 'IRG006' "duplicate dependency edge: $Source -> $Destination"
                            continue
                        }
                        $Adjacency[$Source].Add($Destination)
                        $Indegree[$Destination] = [int]$Indegree[$Destination] + 1
                        if ($OrderIndex.ContainsKey($Source) -and $OrderIndex.ContainsKey($Destination) -and
                            [int]$OrderIndex[$Source] -ge [int]$OrderIndex[$Destination]) {
                            Add-GraphError 'IRG004' "dependency contradicts declared order: $Source -> $Destination"
                        }
                    }

                    $ActualRoots = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
                    foreach ($TargetPathValue in $TargetPaths) {
                        if ([int]$Indegree[$TargetPathValue] -eq 0) {
                            [void]$ActualRoots.Add($TargetPathValue)
                        }
                    }
                    if ($RootSet.Count -ne $ActualRoots.Count -or
                        @($ActualRoots | Where-Object { -not $RootSet.Contains($_) }).Count) {
                        Add-GraphError 'IRG008' 'series.roots must equal the zero-indegree target set'
                    }
                    if ($TargetPaths.Count -gt 0 -and $RootSet.Count -eq 0) {
                        Add-GraphError 'IRG008' 'Series requires at least one root'
                    }

                    $Remaining = @{}
                    $Queue = [Collections.Generic.Queue[string]]::new()
                    foreach ($TargetPathValue in $TargetPaths) {
                        $Remaining[$TargetPathValue] = [int]$Indegree[$TargetPathValue]
                        if ([int]$Remaining[$TargetPathValue] -eq 0) {
                            $Queue.Enqueue($TargetPathValue)
                        }
                    }
                    $Visited = 0
                    while ($Queue.Count -gt 0) {
                        $Node = $Queue.Dequeue()
                        $Visited++
                        foreach ($Successor in $Adjacency[$Node]) {
                            $Remaining[$Successor] = [int]$Remaining[$Successor] - 1
                            if ([int]$Remaining[$Successor] -eq 0) {
                                $Queue.Enqueue($Successor)
                            }
                        }
                    }
                    if ($Visited -ne $TargetPaths.Count) {
                        Add-GraphError 'IRG007' 'series.dependencies must be acyclic'
                    }
                }
            }
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
