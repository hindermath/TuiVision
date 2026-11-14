<#
.SYNOPSIS
Validiert gebundene Secure-Development-Evidence.

.DESCRIPTION
Prüft die vier Gates, das projektgeführte Baseline-Manifest, zwölf eindeutige
Checklisten, Dokumentversionen, normalisierte SHA-256-Bindungen, Reviewdaten,
Restrisiken und getrennte menschliche Entscheidungsgrenzen. Status ist strikt
read-only. Review validiert genau ein benanntes Gate und schreibt keine
Richtlinien, Baseline-Quellen oder Freigaben.

.PARAMETER Action
Status oder Review.

.PARAMETER EvidenceDirectory
Explizites Evidence-Verzeichnis für Status.

.PARAMETER Gate
Baseline, Delta, Closure oder Image-Impact.

.PARAMETER ContextId
Kleine Buchstaben, Zahlen und Bindestriche.

.PARAMETER Mode
Training, Mixed oder Development.

.EXAMPLE
pwsh -NoProfile -File validate-secure-development-assurance.ps1 -Action Status -EvidenceDirectory docs/security/secure-development/2026-08-29-example

.PARAMETER RequireReady
Status beendet den CI-Gate-Lauf mit Fehler, wenn das Gesamtergebnis Blocked oder NeedsRemediation ist.

.EXAMPLE
pwsh -NoProfile -File validate-secure-development-assurance.ps1 -Action Review -Gate baseline -ContextId example -Mode training
#>
[CmdletBinding()]
param(
    [ValidateSet('Status', 'Review')]
    [string]$Action = 'Status',
    [string]$EvidenceDirectory,
    [ValidateSet('baseline', 'delta', 'closure', 'image-impact')]
    [string]$Gate,
    [string]$ContextId,
    [ValidateSet('training', 'mixed', 'development')]
    [string]$Mode,
    [switch]$RequireReady
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$script:ExternalComparisonBoundary = 'HOSK/GWDG: ExternalComparison only; never local evidence'
$script:RequiredSecurityVersion = [version]'0.6.1'
$script:RepositoryRoot = (Get-Location).Path

function Stop-SDAValidation {
    param([Parameter(Mandatory)][string]$Message)
    throw "Blocked: ${Message}"
}

function Get-SDAProperty {
    param(
        [Parameter(Mandatory)][object]$Object,
        [Parameter(Mandatory)][string]$Name,
        [Parameter(Mandatory)][string]$Label,
        [switch]$Optional
    )
    $property = $Object.PSObject.Properties[$Name]
    if (-not $property) {
        if ($Optional) { return $null }
        Stop-SDAValidation "${Label} fehlt."
    }
    return $property.Value
}

function Get-SDAText {
    param(
        [Parameter(Mandatory)][object]$Object,
        [Parameter(Mandatory)][string]$Name,
        [Parameter(Mandatory)][string]$Label
    )
    $value = Get-SDAProperty -Object $Object -Name $Name -Label $Label
    if ($value -is [DateTime]) {
        if ($Label.EndsWith('reviewDue', [StringComparison]::Ordinal) -or
            $Label.EndsWith('dueAt', [StringComparison]::Ordinal)) {
            $value = $value.ToString('yyyy-MM-dd', [Globalization.CultureInfo]::InvariantCulture)
        } else {
            $value = $value.ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ', [Globalization.CultureInfo]::InvariantCulture)
        }
    }
    if ($value -isnot [string] -or [string]::IsNullOrWhiteSpace($value)) {
        Stop-SDAValidation "${Label} fehlt oder ist leer."
    }
    return $value
}

function Test-SDAAllowedValue {
    param(
        [Parameter(Mandatory)][string]$Value,
        [Parameter(Mandatory)][string[]]$Allowed,
        [Parameter(Mandatory)][string]$Label
    )
    if ($Value -notin $Allowed) {
        Stop-SDAValidation "Unzulässiger Wert '${Value}' für ${Label}."
    }
}

function Read-SDAJson {
    param([Parameter(Mandatory)][string]$Path)
    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
        Stop-SDAValidation "Evidence fehlt: ${Path}"
    }
    try {
        $json = Get-Content -LiteralPath $Path -Raw
        if ((Get-Command ConvertFrom-Json).Parameters.ContainsKey('DateKind')) {
            return $json | ConvertFrom-Json -Depth 100 -DateKind String
        }
        return $json | ConvertFrom-Json -Depth 100
    } catch {
        Stop-SDAValidation "Ungültiges JSON: ${Path}"
    }
}

function Test-SDARelativePath {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$Label
    )
    if ([IO.Path]::IsPathRooted($Path) -or $Path.Contains('\')) {
        Stop-SDAValidation "${Label} muss repository-relativ sein: ${Path}"
    }
    if (('/' + $Path + '/') -match '/\.\.?/') {
        Stop-SDAValidation "${Label} enthält ein unzulässiges Segment: ${Path}"
    }
}

function Resolve-SDAPhysicalPath {
    param([Parameter(Mandatory)][string]$Path)
    $full = [IO.Path]::GetFullPath($Path)
    $root = [IO.Path]::GetPathRoot($full)
    $current = $root
    foreach ($segment in $full.Substring($root.Length).Split([char[]]@('/', '\'), [StringSplitOptions]::RemoveEmptyEntries)) {
        $current = [IO.Path]::Combine($current, $segment)
        $entry = Get-Item -LiteralPath $current -Force -ErrorAction Stop
        if (($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) {
            $resolved = $entry.ResolveLinkTarget($true)
            if ($null -eq $resolved) { throw "Symlink kann nicht aufgeloest werden: $current" }
            $current = $resolved.FullName
        }
    }
    return [IO.Path]::GetFullPath($current)
}

function Resolve-SDAConfinedFile {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$Root,
        [Parameter(Mandatory)][string]$Label
    )
    try {
        $physicalRoot = Resolve-SDAPhysicalPath $Root
        $physicalPath = Resolve-SDAPhysicalPath $Path
        $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
        $prefix = $physicalRoot.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
        if (-not $physicalPath.StartsWith($prefix, $comparison) -or -not [IO.File]::Exists($physicalPath)) {
            Stop-SDAValidation "${Label} verlaesst das Repository oder fehlt: ${Path}"
        }
        return $physicalPath
    } catch {
        Stop-SDAValidation "${Label} kann nicht sicher gelesen werden: ${Path}: $($_.Exception.Message)"
    }
}

function Get-SDANormalizedSha256 {
    param([Parameter(Mandatory)][string]$Path)
    $utf8 = [Text.UTF8Encoding]::new($false, $true)
    try {
        $text = $utf8.GetString([IO.File]::ReadAllBytes($Path))
    } catch {
        Stop-SDAValidation "Textdatei ist kein striktes UTF-8: ${Path}"
    }
    if ($text.Length -gt 0 -and $text[0] -eq [char]0xFEFF) {
        $text = $text.Substring(1)
    }
    $crlf = [string][char]13 + [char]10
    $lf = [string][char]10
    $cr = [string][char]13
    $normalized = $text.Replace($crlf, $lf).Replace($cr, $lf)
    $hash = [Security.Cryptography.SHA256]::HashData($utf8.GetBytes($normalized))
    return [Convert]::ToHexString($hash).ToLowerInvariant()
}

function Test-SDASecurityDependency {
    $manifest = Join-Path $script:RepositoryRoot '.specify/presets/security-governance/preset.yml'
    if (-not (Test-Path -LiteralPath $manifest -PathType Leaf)) {
        Stop-SDAValidation "Fachliche Voraussetzung fehlt: security-governance >=$($script:RequiredSecurityVersion)"
    }
    $text = Get-Content -LiteralPath $manifest -Raw
    if ($text -notmatch '(?ms)^preset:\s*\r?\n.*?^\s{2}version:\s*["'']?([0-9]+\.[0-9]+\.[0-9]+)') {
        Stop-SDAValidation 'security-governance-Version ist ungültig.'
    }
    $version = [version]$Matches[1]
    if ($version -lt $script:RequiredSecurityVersion) {
        Stop-SDAValidation "security-governance ${version} ist älter als $($script:RequiredSecurityVersion)."
    }
}

function Test-SDAReviewMetadata {
    param(
        [Parameter(Mandatory)][object]$Document,
        [Parameter(Mandatory)][string]$Path
    )
    $review = Get-SDAProperty -Object $Document -Name 'review' -Label 'review'
    $null = Get-SDAText $review 'owner' 'review.owner'
    $null = Get-SDAText $review 'reviewer' 'review.reviewer'
    $reviewedAt = Get-SDAText $review 'reviewedAt' 'review.reviewedAt'
    $reviewDue = Get-SDAText $review 'reviewDue' 'review.reviewDue'
    $null = Get-SDAText $review 'residualRisk' 'review.residualRisk'
    $null = Get-SDAText $review 'reevaluationTrigger' 'review.reevaluationTrigger'
    $parsedDateTime = [DateTimeOffset]::MinValue
    if (-not [DateTimeOffset]::TryParse(
        $reviewedAt,
        [Globalization.CultureInfo]::InvariantCulture,
        [Globalization.DateTimeStyles]::RoundtripKind,
        [ref]$parsedDateTime
    )) {
        Stop-SDAValidation "review.reviewedAt ist kein ISO-8601-Zeitpunkt: ${Path}"
    }
    $parsedDue = [DateTime]::MinValue
    if (-not [DateTime]::TryParseExact(
        $reviewDue,
        'yyyy-MM-dd',
        [Globalization.CultureInfo]::InvariantCulture,
        [Globalization.DateTimeStyles]::None,
        [ref]$parsedDue
    )) {
        Stop-SDAValidation "review.reviewDue ist kein ISO-Datum: ${Path}"
    }
    if ($parsedDue.Date -lt [DateTime]::UtcNow.Date) {
        Stop-SDAValidation "Review ist abgelaufen (${reviewDue}): ${Path}"
    }
}

function Test-SDAAssessments {
    param(
        [Parameter(Mandatory)][object]$Document,
        [Parameter(Mandatory)][string]$Path
    )
    $assessments = @(Get-SDAProperty -Object $Document -Name 'assessments' -Label 'assessments')
    if ($assessments.Count -eq 0) {
        Stop-SDAValidation "Assessments fehlen: ${Path}"
    }
    $ids = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    foreach ($assessment in $assessments) {
        $id = Get-SDAText $assessment 'id' 'assessment.id'
        if (-not $ids.Add($id)) {
            Stop-SDAValidation "Doppelte Assessment-ID in ${Path}: ${id}"
        }
        $applicability = Get-SDAText $assessment 'applicability' "assessments[${id}].applicability"
        $implementation = Get-SDAText $assessment 'implementation' "assessments[${id}].implementation"
        $null = Get-SDAText $assessment 'evidence' "assessments[${id}].evidence"
        Test-SDAAllowedValue $applicability @('Applicable', 'N/A', 'Open') "assessments[${id}].applicability"
        Test-SDAAllowedValue $implementation @('Fulfilled', 'Partly Fulfilled', 'Not Fulfilled', 'Not Assessed') "assessments[${id}].implementation"
        if ($applicability -eq 'N/A') {
            $null = Get-SDAText $assessment 'reason' "assessments[${id}].reason"
            $null = Get-SDAText $assessment 'reevaluationTrigger' "assessments[${id}].reevaluationTrigger"
        }
        if ($applicability -eq 'Open' -or ($applicability -eq 'Applicable' -and $implementation -ne 'Fulfilled')) {
            $null = Get-SDAText $assessment 'nextAction' "assessments[${id}].nextAction"
            $null = Get-SDAText $assessment 'owner' "assessments[${id}].owner"
            $null = Get-SDAText $assessment 'dueAt' "assessments[${id}].dueAt"
        }
    }
}

function Test-SDAAcceptedRisks {
    param(
        [Parameter(Mandatory)][object]$Document,
        [Parameter(Mandatory)][string]$Path
    )
    $risksProperty = $Document.PSObject.Properties['acceptedRisks']
    if (-not $risksProperty) { return }
    if ($risksProperty.Name -cne 'acceptedRisks' -or $risksProperty.Value -isnot [Array]) {
        Stop-SDAValidation 'acceptedRisks muss ein JSON-Array sein.'
    }
    foreach ($risk in @($risksProperty.Value)) {
        # Die Pipeline entpackt einteilige Arrays; den kanonischen Rohwert zuerst pruefen.
        # The pipeline unwraps singleton arrays; check the canonical raw value first.
        $idProperty = $risk.PSObject.Properties['id']
        if (-not $idProperty -or $idProperty.Name -cne 'id') {
            Stop-SDAValidation 'acceptedRisks.id fehlt.'
        }
        if ($idProperty.Value -is [Array]) {
            Stop-SDAValidation 'acceptedRisks.id muss einzelner Text sein.'
        }
        $id = Get-SDAText $risk 'id' 'acceptedRisks.id'
        foreach ($field in @('owner', 'reviewer', 'reviewedAt', 'reviewDue', 'scope', 'rationale', 'residualRisk', 'reevaluationTrigger')) {
            $null = Get-SDAText $risk $field "acceptedRisks[${id}].${field}"
        }
        $reviewedAt = Get-SDAText $risk 'reviewedAt' "acceptedRisks[${id}].reviewedAt"
        $reviewDue = Get-SDAText $risk 'reviewDue' "acceptedRisks[${id}].reviewDue"
        if ($reviewedAt -cnotmatch '^[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}([.][0-9]+)?(Z|[+-][0-9]{2}:[0-9]{2})$') {
            Stop-SDAValidation "acceptedRisks[${id}].reviewedAt ist kein ISO-8601-Zeitpunkt: ${Path}"
        }
        $parsedDateTime = [DateTimeOffset]::MinValue
        if (-not [DateTimeOffset]::TryParse($reviewedAt, [Globalization.CultureInfo]::InvariantCulture,
                [Globalization.DateTimeStyles]::RoundtripKind, [ref]$parsedDateTime)) {
            Stop-SDAValidation "acceptedRisks[${id}].reviewedAt ist kein ISO-8601-Zeitpunkt: ${Path}"
        }
        if ($parsedDateTime -gt [DateTimeOffset]::UtcNow) {
            Stop-SDAValidation "acceptedRisks[${id}].reviewedAt liegt in der Zukunft: ${Path}"
        }
        $parsedDue = [DateTime]::MinValue
        if (-not [DateTime]::TryParseExact($reviewDue, 'yyyy-MM-dd', [Globalization.CultureInfo]::InvariantCulture,
                [Globalization.DateTimeStyles]::None, [ref]$parsedDue)) {
            Stop-SDAValidation "acceptedRisks[${id}].reviewDue ist kein ISO-Datum: ${Path}"
        }
        if ($parsedDue.Date -lt [DateTime]::UtcNow.Date) {
            Stop-SDAValidation "Akzeptiertes Risiko ist abgelaufen (${reviewDue}): ${Path}"
        }
    }
}

function Test-SDAClaimBoundary {
    param([Parameter(Mandatory)][string]$Path)
    $pattern = '(?i)(^|[^a-z0-9])(c5[ -]?(compliant|certified|conformant|ready)|c5[- ]?(konform|zertifiziert|testatbereit)|iso[ /-]?[0-9]+[ -]?(compliant|certified|konform|zertifiziert)|testatbereit|attestation[- ]ready)([^a-z0-9]|$)'
    $document = Read-SDAJson $Path
    function Assert-SDAClaimText {
        param($Value)
        if ($Value -is [string]) {
            if ([regex]::IsMatch($Value, $pattern, [Text.RegularExpressions.RegexOptions]::CultureInvariant)) {
                Stop-SDAValidation "Unzulässige Zertifizierungs-, Konformitäts- oder Testatbehauptung: ${Path}"
            }
        } elseif ($Value -is [System.Collections.IDictionary]) {
            foreach ($key in $Value.Keys) {
                Assert-SDAClaimText ([string]$key)
                Assert-SDAClaimText $Value[$key]
            }
        } elseif ($Value -is [pscustomobject]) {
            foreach ($property in $Value.PSObject.Properties) {
                Assert-SDAClaimText $property.Name
                Assert-SDAClaimText $property.Value
            }
        } elseif ($Value -is [System.Collections.IEnumerable]) {
            foreach ($entry in $Value) { Assert-SDAClaimText $entry }
        }
    }
    Assert-SDAClaimText $document
}

function Test-SDAGateFile {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$ExpectedGate
    )
    $document = Read-SDAJson $Path
    if ((Get-SDAText $document 'schemaVersion' 'schemaVersion') -ne '1.0' -or
        (Get-SDAText $document 'documentType' 'documentType') -ne 'SecureDevelopmentGateEvidence') {
        Stop-SDAValidation "Evidence-Schema ist ungültig: ${Path}"
    }
    $gate = Get-SDAText $document 'gate' 'gate'
    $outcome = Get-SDAText $document 'outcome' 'outcome'
    $contextId = Get-SDAText $document 'contextId' 'contextId'
    $mode = Get-SDAText $document 'mode' 'mode'
    if ($gate -ne $ExpectedGate) {
        Stop-SDAValidation "Gate-Drift in ${Path}: $gate"
    }
    if ($contextId -notmatch '^[a-z0-9][a-z0-9-]*$') {
        Stop-SDAValidation "contextId ist ungültig: ${Path}"
    }
    Test-SDAAllowedValue $mode @('training', 'mixed', 'development') 'mode'
    Test-SDAAllowedValue $outcome @('Ready', 'ReadyWithAcceptedRisks', 'NeedsRemediation', 'Blocked') 'outcome'
    Test-SDAReviewMetadata $document $Path
    Test-SDAAssessments $document $Path
    Test-SDAAcceptedRisks $document $Path
    Test-SDAClaimBoundary $Path
    if ($outcome -eq 'Ready') {
        $blocked = @($document.assessments | Where-Object {
            $_.applicability -eq 'Open' -or
            ($_.applicability -eq 'Applicable' -and $_.implementation -ne 'Fulfilled')
        })
        if ($blocked.Count -gt 0) {
            Stop-SDAValidation "Ready ist bei offenen oder unerfüllten Pflichtpunkten unzulässig: ${Path}"
        }
    }
    if ($outcome -eq 'ReadyWithAcceptedRisks') {
        $risks = @($document.acceptedRisks)
        if ($risks.Count -eq 0) {
            Stop-SDAValidation "ReadyWithAcceptedRisks benötigt mindestens ein akzeptiertes Risiko: ${Path}"
        }
        $unresolved = @($document.assessments | Where-Object {
            $_.applicability -eq 'Open' -or
            ($_.applicability -eq 'Applicable' -and $_.implementation -ne 'Fulfilled')
        })
        foreach ($assessment in $unresolved) {
            $covered = $false
            foreach ($risk in $risks) {
                $idsProperty = $risk.PSObject.Properties['assessmentIds']
                if ($idsProperty -and $idsProperty.Value -is [Array] -and
                    @($idsProperty.Value | Where-Object { $_ -is [string] -and $_ -ceq $assessment.id }).Count -gt 0) {
                    $covered = $true
                    break
                }
            }
            if (-not $covered) {
                Stop-SDAValidation "ReadyWithAcceptedRisks benötigt ein zugeordnetes Risiko für Assessment '$($assessment.id)': ${Path}"
            }
        }
    }
    if ((Get-SDAText $document 'externalComparisonBoundary' 'externalComparisonBoundary') -ne $script:ExternalComparisonBoundary) {
        Stop-SDAValidation "Externe Vergleichsgrenze fehlt: ${Path}"
    }
    return $document
}

function Test-SDAActiveMetadataField {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$RelativePath,
        [Parameter(Mandatory)][string]$Prefix,
        [Parameter(Mandatory)][string]$Expected,
        [string]$Heading = '',
        [switch]$AllowEmbeddedBaseline
    )
    # Only the first metadata block/table may supply the value; body matches cannot mask drift.
    # Nur der erste Metadatenblock bzw. die erste Tabelle liefert den aktiven Wert.
    $text = (Get-Content -LiteralPath $Path -Raw).Replace("`r`n", "`n").Replace("`r", "`n")
    $count = 0; $matched = 0; $sections = 0
    $started = $false; $active = $false; $table = $false; $ended = $false
    $introClosed = $false; $titleSeen = $false
    $embedded = $false; $embeddedCount = 0; $invalid = $false
    foreach ($line in ($text -split "`n")) {
        if ($Heading) {
            if ($line.StartsWith('## ', [StringComparison]::Ordinal)) {
                $active = $line -ceq $Heading
                if ($active) { $sections++ }
                $table = $false; $ended = $false
            }
            if ($active -and $line.StartsWith('|', [StringComparison]::Ordinal) -and -not $ended) { $table = $true }
            if ($active -and $line -cne $Heading -and -not $line.StartsWith('|', [StringComparison]::Ordinal) -and
                ($table -or -not [string]::IsNullOrWhiteSpace($line))) { $ended = $true }
            if ($line.StartsWith($Prefix, [StringComparison]::Ordinal) -and ($line -split '\|').Count -eq 4) {
                $count++
                if ($active -and $table -and -not $ended -and $line -ceq $Expected) { $matched++ }
            }
        } else {
            if (-not $started -and $line -cmatch '^#+ ') {
                if ($titleSeen) { $introClosed = $true }
                $titleSeen = $true
            }
            if (-not $started -and $titleSeen -and $line -cnotmatch '^#+ ' -and
                -not [string]::IsNullOrWhiteSpace($line) -and -not $line.StartsWith('>', [StringComparison]::Ordinal) -and
                $line -cnotmatch '^\*\*[^*]+:\*\*') { $introClosed = $true }
            if (-not $started -and -not $introClosed -and $line -cmatch '^\*\*[^*]+:\*\*') { $started = $true; $active = $true }
            if ($started -and $line -cnotmatch '^\*\*[^*]+:\*\*') { $active = $false }
            # Embedded checklists own their baseline; duplicate fields within that block still fail.
            # Eingebettete Checklisten haben eigene Baselines; doppelte Felder im Block bleiben Fehler.
            if ($AllowEmbeddedBaseline -and -not $active -and $line -cmatch '^\*\*Dokument-ID / Document ID:\*\* CL-(0[1-9]|1[0-2])$') {
                $embedded = $true; $embeddedCount = 0
            }
            if ($line -cnotmatch '^\*\*[^*]+:\*\*') { $embedded = $false }
            if ($embedded -and $line.StartsWith($Prefix, [StringComparison]::Ordinal)) {
                $embeddedCount++
                if ($embeddedCount -gt 1) { $invalid = $true }
                continue
            }
            if ($line.StartsWith($Prefix, [StringComparison]::Ordinal)) {
                $count++
                if ($active -and $line -ceq $Expected) { $matched++ }
            }
        }
    }
    if ($count -ne 1 -or $matched -ne 1 -or $invalid -or ($Heading -and $sections -ne 1)) {
        Stop-SDAValidation "Aktive Metadaten fehlen, weichen ab oder sind doppelt: ${RelativePath} (${Prefix})"
    }
}

function Test-SDADocumentVersion {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$RelativePath,
        [Parameter(Mandatory)][string]$Version,
        [Parameter(Mandatory)][string]$Kind
    )
    if ($Kind -eq 'guideline') {
        Test-SDAActiveMetadataField $Path $RelativePath '| Versionsnummer |' "| Versionsnummer | ${Version} |" '## Dokumenten-Metadaten'
        Test-SDAActiveMetadataField $Path $RelativePath '| Version |' "| Version | ${Version} |" '## Document Metadata'
    } else {
        $prefix = if ($Kind -eq 'compendium') { '**Dokumentversion / Document version:**' } else { '**Version / Version:**' }
        Test-SDAActiveMetadataField $Path $RelativePath $prefix "${prefix} ${Version}"
    }
}

function Test-SDABaselineContract {
    param(
        [Parameter(Mandatory)][object]$Baseline,
        [Parameter(Mandatory)][string]$BaselinePath
    )
    Test-SDASecurityDependency
    $binding = Get-SDAProperty $Baseline 'baselineBinding' 'baselineBinding'
    $manifestPath = Get-SDAText $binding 'manifestPath' 'baselineBinding.manifestPath'
    $baselineVersion = Get-SDAText $binding 'baselineVersion' 'baselineBinding.baselineVersion'
    $manifestHash = Get-SDAText $binding 'manifestNormalizedSha256' 'baselineBinding.manifestNormalizedSha256'
    Test-SDARelativePath $manifestPath 'baselineBinding.manifestPath'
    if ($manifestHash -notmatch '^[0-9a-f]{64}$') {
        Stop-SDAValidation 'baselineBinding.manifestNormalizedSha256 ist ungültig.'
    }
    $fullManifestPath = Resolve-SDAConfinedFile (Join-Path $script:RepositoryRoot $manifestPath) $script:RepositoryRoot 'Baseline-Manifest'
    $manifest = Read-SDAJson $fullManifestPath
    if ([int](Get-SDAProperty $manifest 'schemaVersion' 'manifest.schemaVersion') -ne 1 -or
        @($manifest.checklists).Count -ne 12) {
        Stop-SDAValidation "Baseline-Manifest benötigt Schema 1 und genau zwölf Checklisten: ${manifestPath}"
    }
    if ((Get-SDANormalizedSha256 $fullManifestPath) -ne $manifestHash) {
        Stop-SDAValidation "Manifest-Hashdrift: ${manifestPath}"
    }
    if ((Get-SDAText $manifest 'baselineVersion' 'manifest.baselineVersion') -ne $baselineVersion) {
        Stop-SDAValidation 'Baseline-Version und Evidence-Bindung stimmen nicht überein.'
    }
    if ((@($manifest.statusModel.applicability) -join '|') -ne 'Applicable|N/A|Open' -or
        (@($manifest.statusModel.implementation) -join '|') -ne 'Fulfilled|Partly Fulfilled|Not Fulfilled|Not Assessed') {
        Stop-SDAValidation 'Statusmodell im Baseline-Manifest ist ungültig.'
    }
    $expectedIds = 1..12 | ForEach-Object { 'CL-{0:D2}' -f $_ }
    $actualIds = @($manifest.checklists.id | Sort-Object)
    if (($actualIds -join '|') -ne ($expectedIds -join '|')) {
        Stop-SDAValidation 'Checklisten-IDs müssen eindeutig CL-01 bis CL-12 entsprechen.'
    }

    $bindings = @(Get-SDAProperty $binding 'documentBindings' 'baselineBinding.documentBindings')
    $documents = [Collections.Generic.List[object]]::new()
    $documents.Add([pscustomobject]@{ path = $manifest.guideline.path; version = $manifest.guideline.version; kind = 'guideline' })
    $documents.Add([pscustomobject]@{ path = $manifest.compendium.path; version = $manifest.compendium.version; kind = 'compendium' })
    foreach ($item in @($manifest.checklists)) {
        $documents.Add([pscustomobject]@{ path = $item.path; version = $item.version; kind = 'checklist' })
    }
    foreach ($item in @($manifest.relatedDocuments)) {
        $documents.Add([pscustomobject]@{ path = $item.path; version = $item.version; kind = 'related' })
    }
    foreach ($item in @($manifest.learningDocuments)) {
        $documents.Add([pscustomobject]@{ path = $item.path; version = $item.version; kind = 'learning' })
    }
    if ($bindings.Count -ne $documents.Count) {
        Stop-SDAValidation "Dokumentbindungen unvollständig: erwartet $($documents.Count), gefunden $($bindings.Count)."
    }
    $duplicateBindings = $bindings | Group-Object path | Where-Object Count -gt 1
    if ($duplicateBindings) {
        Stop-SDAValidation "Doppelte Dokumentbindung: $($duplicateBindings.Name -join ', ')"
    }
    $docRoot = Split-Path -Parent $fullManifestPath
    foreach ($document in $documents) {
        Test-SDARelativePath $document.path 'Manifest-Dokumentpfad'
        $bindingMatches = @($bindings | Where-Object path -eq $document.path)
        if ($bindingMatches.Count -ne 1) {
            Stop-SDAValidation "Dokumentbindung fehlt oder ist doppelt: $($document.path)"
        }
        $boundVersion = Get-SDAText $bindingMatches[0] 'version' "documentBindings[$($document.path)].version"
        $boundHash = Get-SDAText $bindingMatches[0] 'normalizedSha256' "documentBindings[$($document.path)].normalizedSha256"
        if ($boundVersion -ne $document.version) {
            Stop-SDAValidation "Versionsbindung stimmt nicht: $($document.path)"
        }
        if ($boundHash -notmatch '^[0-9a-f]{64}$') {
            Stop-SDAValidation "Hashbindung ist ungültig: $($document.path)"
        }
        $fullPath = Resolve-SDAConfinedFile (Join-Path $docRoot $document.path) $docRoot 'Kontrolliertes Dokument'
        if ((Get-SDANormalizedSha256 $fullPath) -ne $boundHash) {
            Stop-SDAValidation "Dokument-Hashdrift: $($document.path)"
        }
        Test-SDADocumentVersion $fullPath $document.path $document.version $document.kind
    }

    $itemIds = [Collections.Generic.List[string]]::new()
    foreach ($checklist in @($manifest.checklists)) {
        $path = Resolve-SDAConfinedFile (Join-Path $docRoot $checklist.path) $docRoot 'Checkliste'
        $text = Get-Content -LiteralPath $path -Raw
        Test-SDAActiveMetadataField $path $checklist.path '**Dokument-ID / Document ID:**' "**Dokument-ID / Document ID:** $($checklist.id)"
        foreach ($match in [regex]::Matches($text, '(?m)^#### (CL-[0-9]{2}-[0-9]{2}):')) {
            $itemIds.Add($match.Groups[1].Value)
        }
    }
    if ($itemIds.Count -ne [int]$manifest.checklistItemCount) {
        Stop-SDAValidation "Checklistenmenge stimmt nicht: erwartet $($manifest.checklistItemCount), gefunden $($itemIds.Count)."
    }
    if ($itemIds | Group-Object | Where-Object Count -gt 1) {
        Stop-SDAValidation 'Doppelte Prüfpunkte in den zwölf Checklisten.'
    }
    if ('CL-02-13' -notin $itemIds) {
        Stop-SDAValidation 'Der projektgeführte Prüfpunkt CL-02-13 Cloud-Compliance-Assurance fehlt.'
    }
    $compendiumPath = Resolve-SDAConfinedFile (Join-Path $docRoot $manifest.compendium.path) $docRoot 'Sammelband'
    $compendiumText = Get-Content -LiteralPath $compendiumPath -Raw
    $compendiumIds = @([regex]::Matches($compendiumText, '(?m)^#### (CL-[0-9]{2}-[0-9]{2}):') | ForEach-Object { $_.Groups[1].Value })
    if ((($itemIds | Sort-Object) -join '|') -ne (($compendiumIds | Sort-Object) -join '|')) {
        Stop-SDAValidation 'Sammelbanddrift gegenüber den zwölf Einzelchecklisten.'
    }
    Test-SDAActiveMetadataField $compendiumPath $manifest.compendium.path '**Baseline-Version / Baseline version:**' "**Baseline-Version / Baseline version:** ${baselineVersion}" -AllowEmbeddedBaseline
    foreach ($relative in @($manifest.managedBinaryFiles) + @($manifest.managedReferenceFiles)) {
        Test-SDARelativePath $relative 'Verwalteter Referenzpfad'
        $null = Resolve-SDAConfinedFile (Join-Path $docRoot $relative) $docRoot 'Verwaltete Referenz'
    }
}

function Test-SDAImageImpact {
    param([Parameter(Mandatory)][object]$Document)
    $checks = Get-SDAProperty $Document 'imageChecks' 'imageChecks'
    foreach ($key in @('build', 'compose', 'toolchain', 'ociDigest', 'sbom', 'secrets', 'mounts', 'network', 'ci')) {
        $status = Get-SDAText $checks $key "imageChecks.${key}"
        Test-SDAAllowedValue $status @('Fulfilled', 'Partly Fulfilled', 'Not Fulfilled', 'Not Assessed', 'N/A') "imageChecks.${key}"
        if ($Document.outcome -eq 'Ready' -and $status -notin @('Fulfilled', 'N/A')) {
            Stop-SDAValidation 'Ready ist bei offenen Image-Impact-Prüfungen unzulässig.'
        }
    }
}

function Test-SDAClosure {
    param([Parameter(Mandatory)][object]$Document)
    $nextActionProperty = $Document.PSObject.Properties['nextAction']
    if ($nextActionProperty -and $null -ne $nextActionProperty.Value -and
        ($nextActionProperty.Value -isnot [string] -or
         $nextActionProperty.Value -match '[\x00-\x1F\x7F-\x9F\u2028\u2029]')) {
        Stop-SDAValidation 'nextAction enthält Steuerzeichen oder ist kein Text.'
    }
    $decisions = Get-SDAProperty $Document 'humanDecisions' 'humanDecisions'
    foreach ($key in @('technicalValidation', 'pilotAuthorization', 'projectAcceptance', 'generalRelease')) {
        $decision = Get-SDAProperty $decisions $key "humanDecisions.${key}"
        $status = Get-SDAText $decision 'status' "humanDecisions.${key}.status"
        Test-SDAAllowedValue $status @('Fulfilled', 'Open', 'Blocked', 'Not Assessed') "humanDecisions.${key}.status"
        $null = Get-SDAText $decision 'authority' "humanDecisions.${key}.authority"
        $null = Get-SDAText $decision 'evidence' "humanDecisions.${key}.evidence"
    }
    $technical = $decisions.technicalValidation.status
    foreach ($key in @('pilotAuthorization', 'projectAcceptance', 'generalRelease')) {
        if ($technical -ne 'Fulfilled' -and $decisions.$key.status -eq 'Fulfilled') {
            Stop-SDAValidation "Menschliche Freigabe darf technische Validierung nicht überspringen: ${key}"
        }
    }
}

function Test-SDAContext {
    param([Parameter(Mandatory)][string]$Path)
    if (-not (Test-Path -LiteralPath $Path -PathType Container)) {
        Stop-SDAValidation "Evidence-Verzeichnis fehlt: ${Path}"
    }
    if (-not (Test-Path -LiteralPath (Join-Path $Path 'evidence-matrix.md') -PathType Leaf)) {
        Stop-SDAValidation 'evidence-matrix.md fehlt.'
    }
    $baselinePath = Join-Path $Path 'baseline.json'
    $baseline = Test-SDAGateFile $baselinePath baseline
    Test-SDABaselineContract $baseline $baselinePath
    $expectedId = Get-SDAText $baseline 'contextId' 'contextId'
    $expectedMode = Get-SDAText $baseline 'mode' 'mode'
    $deltaDirectory = Join-Path $Path 'deltas'
    $deltaFiles = @(if (Test-Path -LiteralPath $deltaDirectory -PathType Container) {
        Get-ChildItem -LiteralPath $deltaDirectory -Filter '*.json' -File | Sort-Object Name
    })
    if ($deltaFiles.Count -eq 0) {
        Stop-SDAValidation 'Mindestens eine Delta-Evidence fehlt.'
    }
    foreach ($deltaFile in $deltaFiles) {
        $delta = Test-SDAGateFile $deltaFile.FullName delta
        if ((Get-SDAText $delta 'contextId' 'contextId') -cne $expectedId -or
            (Get-SDAText $delta 'mode' 'mode') -cne $expectedMode) {
            Stop-SDAValidation "Kontext- oder Betriebsart-Drift: $($deltaFile.FullName)"
        }
    }
    $closure = Test-SDAGateFile (Join-Path $Path 'closure.json') closure
    if ((Get-SDAText $closure 'contextId' 'contextId') -cne $expectedId -or
        (Get-SDAText $closure 'mode' 'mode') -cne $expectedMode) {
        Stop-SDAValidation 'Kontext- oder Betriebsart-Drift: closure.json'
    }
    Test-SDAClosure $closure
    $image = Test-SDAGateFile (Join-Path $Path 'image-impact.json') image-impact
    if ((Get-SDAText $image 'contextId' 'contextId') -cne $expectedId -or
        (Get-SDAText $image 'mode' 'mode') -cne $expectedMode) {
        Stop-SDAValidation 'Kontext- oder Betriebsart-Drift: image-impact.json'
    }
    Test-SDAImageImpact $image
}

function Get-SDAWorstOutcome {
    param([Parameter(Mandatory)][string[]]$Outcomes)
    foreach ($value in @('Blocked', 'NeedsRemediation', 'ReadyWithAcceptedRisks', 'Ready')) {
        if ($value -in $Outcomes) { return $value }
    }
    return 'Blocked'
}

function Write-SDAStatus {
    param([Parameter(Mandatory)][string]$Path)
    $baseline = Read-SDAJson (Join-Path $Path 'baseline.json')
    $deltaDocuments = @(Get-ChildItem -LiteralPath (Join-Path $Path 'deltas') -Filter '*.json' -File |
        Sort-Object Name | ForEach-Object { Read-SDAJson $_.FullName })
    $closure = Read-SDAJson (Join-Path $Path 'closure.json')
    $image = Read-SDAJson (Join-Path $Path 'image-impact.json')
    $deltaOutcome = Get-SDAWorstOutcome @($deltaDocuments.outcome)
    $overall = Get-SDAWorstOutcome @($baseline.outcome, $deltaOutcome, $closure.outcome, $image.outcome)
    $script:OverallOutcome = $overall
    $nextActionProperty = $closure.PSObject.Properties['nextAction']
    $nextAction = if ($nextActionProperty -and -not [string]::IsNullOrWhiteSpace([string]$nextActionProperty.Value)) {
        [string]$nextActionProperty.Value
    } else {
        'No further technical action recorded.'
    }
    "Context: ${Path}"
    "Gate baseline: $($baseline.outcome)"
    "Gate delta: ${deltaOutcome}"
    "Gate closure: $($closure.outcome)"
    "Gate image-impact: $($image.outcome)"
    "Overall: ${overall}"
    foreach ($key in @('technicalValidation', 'pilotAuthorization', 'projectAcceptance', 'generalRelease')) {
        "Decision ${key}: $($closure.humanDecisions.$key.status)"
    }
    "Next action: ${nextAction}"
}

try {
    if ($Action -eq 'Status') {
        if (-not $EvidenceDirectory) {
            $root = 'docs/security/secure-development'
            if (Test-Path -LiteralPath $root -PathType Container) {
                $EvidenceDirectory = Get-ChildItem -LiteralPath $root -Directory |
                    Sort-Object Name |
                    Select-Object -Last 1 -ExpandProperty FullName
            }
        }
        if (-not $EvidenceDirectory) {
            Stop-SDAValidation 'Kein Evidence-Verzeichnis gefunden.'
        }
        Test-SDAContext $EvidenceDirectory
        Write-SDAStatus $EvidenceDirectory
        if ($RequireReady -and $script:OverallOutcome -notin @('Ready', 'ReadyWithAcceptedRisks')) {
            Stop-SDAValidation "Assurance-Evidence ist nicht bereit: $script:OverallOutcome"
        }
        exit 0
    }

    if (-not $Gate -or -not $ContextId -or -not $Mode) {
        Stop-SDAValidation 'Review benötigt Gate, ContextId und Mode.'
    }
    if ($ContextId -cnotmatch '^[a-z0-9][a-z0-9-]*$') {
        Stop-SDAValidation 'ContextId ist ungültig.'
    }
    # Vollstaendige IDs und eindeutige Auswahl binden den Auftrag, nie ein passendes Suffix.
    # Full IDs and unique selection bind the request, never a matching suffix.
    $contextMatches = @(Get-ChildItem -LiteralPath 'docs/security/secure-development' -Directory |
        Where-Object { $_.Name -cmatch ('^[0-9]{4}-[0-9]{2}-[0-9]{2}-' + [regex]::Escape($ContextId) + '$') })
    if ($contextMatches.Count -eq 0) {
        Stop-SDAValidation "Kontext nicht gefunden: ${ContextId}"
    }
    if ($contextMatches.Count -ne 1) {
        Stop-SDAValidation "Kontext muss genau einmal vorhanden sein: ${ContextId}"
    }
    $contextDirectory = $contextMatches[0].FullName
    switch ($Gate) {
        'baseline' {
            $gateFile = Join-Path $contextDirectory 'baseline.json'
            $document = Test-SDAGateFile $gateFile baseline
            Test-SDABaselineContract $document $gateFile
        }
        'closure' {
            $gateFile = Join-Path $contextDirectory 'closure.json'
            $document = Test-SDAGateFile $gateFile closure
            Test-SDAClosure $document
        }
        'image-impact' {
            $gateFile = Join-Path $contextDirectory 'image-impact.json'
            $document = Test-SDAGateFile $gateFile image-impact
            Test-SDAImageImpact $document
        }
        'delta' {
            $gateFile = Get-ChildItem -LiteralPath (Join-Path $contextDirectory 'deltas') -Filter '*.json' -File |
                Sort-Object Name |
                Select-Object -Last 1 -ExpandProperty FullName
            if (-not $gateFile) { Stop-SDAValidation 'Delta-Evidence fehlt.' }
            $document = Test-SDAGateFile $gateFile delta
        }
    }
    if ((Get-SDAText $document 'contextId' 'contextId') -cne $ContextId) {
        Stop-SDAValidation "Kontext-ID-Drift: ${gateFile}"
    }
    if ((Get-SDAText $document 'mode' 'mode') -cne $Mode) {
        Stop-SDAValidation "Betriebsart-Drift: ${gateFile}"
    }
    $runbook = "docs/runbooks/secure-development/${Gate}-${ContextId}.md"
    if ($Mode -ne 'development') {
        if (-not (Test-Path -LiteralPath $runbook -PathType Leaf)) {
            Stop-SDAValidation "Runbook fehlt für ${Mode}: ${runbook}"
        }
    } elseif (-not (Test-Path -LiteralPath $runbook -PathType Leaf)) {
        $applicabilityProperty = $document.PSObject.Properties['runbookApplicability']
        $rationaleProperty = $document.PSObject.Properties['runbookRationale']
        if (-not $applicabilityProperty -or $applicabilityProperty.Value -ne 'N/A' -or
            -not $rationaleProperty -or [string]::IsNullOrWhiteSpace([string]$rationaleProperty.Value)) {
            Stop-SDAValidation "Development benötigt Runbook oder begründetes N/A: ${runbook}"
        }
    }
    "Reviewed: gate=${Gate} context=${ContextId} mode=${Mode} outcome=$($document.outcome)"
    exit 0
} catch {
    [Console]::Error.WriteLine($_.Exception.Message)
    if ($env:SDA_DEBUG) {
        [Console]::Error.WriteLine($_.ScriptStackTrace)
    }
    exit 2
}
