<#
.SYNOPSIS
Prüft den Secure-Development-Assurance-Vertrag und die Shell-Parität.

.DESCRIPTION
Erzeugt eine isolierte Fixture mit zwölf Checklisten und prüft positive,
negative, Read-only-, Gate-Review-, Runbook-, UTF-8-BOM-, CRLF- und
Bash-/PowerShell-Paritätsfälle.
#>
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$presetRoot = Split-Path -Parent $PSScriptRoot
$bashValidator = Join-Path $presetRoot 'scripts/validate-secure-development-assurance.sh'
$powerShellValidator = Join-Path $presetRoot 'scripts/validate-secure-development-assurance.ps1'
$temporaryRoot = Join-Path ([IO.Path]::GetTempPath()) ('sda-' + [guid]::NewGuid().ToString('N'))
$contextRelative = 'docs/security/secure-development/2099-01-01-contract-test'
$boundary = 'HOSK/GWDG: ExternalComparison only; never local evidence'
$utf8 = [Text.UTF8Encoding]::new($false)
$validRiskReviewedAt = [DateTimeOffset]::UtcNow.AddDays(-1).ToString('yyyy-MM-ddTHH:mm:ssZ')

function Write-SDATestText {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$Text,
        [switch]$Bom,
        [switch]$CrLf
    )
    $directory = Split-Path -Parent $Path
    if ($directory) { [IO.Directory]::CreateDirectory($directory) | Out-Null }
    $content = $Text
    if ($CrLf) {
        $content = $content.Replace(([string][char]13 + [char]10), [string][char]10)
        $content = $content.Replace([string][char]10, ([string][char]13 + [char]10))
    }
    $encoding = [Text.UTF8Encoding]::new([bool]$Bom)
    [IO.File]::WriteAllText($Path, $content, $encoding)
}

function Write-SDATestJson {
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][object]$Value
    )
    Write-SDATestText $Path (($Value | ConvertTo-Json -Depth 30) + [Environment]::NewLine)
}

function Get-SDATestHash {
    param([Parameter(Mandatory)][string]$Path)
    $strictUtf8 = [Text.UTF8Encoding]::new($false, $true)
    $text = $strictUtf8.GetString([IO.File]::ReadAllBytes($Path))
    if ($text.Length -gt 0 -and $text[0] -eq [char]0xFEFF) { $text = $text.Substring(1) }
    $text = $text.Replace(([string][char]13 + [char]10), [string][char]10)
    $text = $text.Replace([string][char]13, [string][char]10)
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($strictUtf8.GetBytes($text))).ToLowerInvariant()
}

function Get-SDATestSnapshot {
    param(
        [Parameter(Mandatory)][string]$Directory,
        [IO.FileInfo[]]$Files
    )
    if (-not $PSBoundParameters.ContainsKey('Files')) {
        $Files = @(Get-ChildItem -LiteralPath $Directory -File -Recurse -Force)
    }

    # Byte-Treue und ordinale Pfade beweisen Read-only, nicht die fachliche Textnormalisierung.
    # Raw bytes and ordinal paths prove read-only behavior, separate from semantic text normalization.
    $entries = [Collections.Generic.SortedDictionary[string, string]]::new([StringComparer]::Ordinal)
    foreach ($file in $Files) {
        $relative = [IO.Path]::GetRelativePath($Directory, $file.FullName).Replace([IO.Path]::DirectorySeparatorChar, '/')
        $entries.Add($relative, (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash)
    }
    $records = @(
        foreach ($entry in $entries.GetEnumerator()) {
            [ordered]@{ path = $entry.Key; sha256 = $entry.Value }
        }
    )
    return ConvertTo-Json -InputObject $records -Depth 3 -Compress
}

function Read-SDATestJson {
    param([Parameter(Mandatory)][string]$Path)
    return Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -Depth 100 -DateKind String
}

function Get-SDATestReview {
    return [ordered]@{
        owner = 'Fixture owner'
        reviewer = 'Fixture reviewer'
        reviewedAt = '2099-01-01T00:00:00Z'
        reviewDue = '2099-12-31'
        residualRisk = 'None identified in the fixture.'
        reevaluationTrigger = 'Any fixture change.'
    }
}

function Get-SDATestAssessment {
    param([string]$Id = 'TEST-001')
    return [ordered]@{
        id = $Id
        applicability = 'Applicable'
        implementation = 'Fulfilled'
        evidence = 'tests/evidence.md'
    }
}

function Get-SDATestGate {
    param(
        [Parameter(Mandatory)][string]$Gate,
        [string]$Outcome = 'Ready'
    )
    return [ordered]@{
        schemaVersion = '1.0'
        documentType = 'SecureDevelopmentGateEvidence'
        contextId = 'contract-test'
        gate = $Gate
        mode = 'mixed'
        createdAt = '2099-01-01T00:00:00Z'
        review = Get-SDATestReview
        assessments = @(Get-SDATestAssessment)
        outcome = $Outcome
        externalComparisonBoundary = $boundary
    }
}

function Update-SDATestBindings {
    $manifestPath = Join-Path $temporaryRoot 'docs/secure-development/baseline-manifest.json'
    $manifest = Read-SDATestJson $manifestPath
    $baselinePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'baseline.json')
    $baseline = Read-SDATestJson $baselinePath
    $documents = @(
        $manifest.guideline
        $manifest.compendium
        $manifest.checklists
        $manifest.relatedDocuments
        $manifest.learningDocuments
    )
    $baseline.baselineBinding = [ordered]@{
        manifestPath = 'docs/secure-development/baseline-manifest.json'
        baselineVersion = $manifest.baselineVersion
        manifestNormalizedSha256 = Get-SDATestHash $manifestPath
        documentBindings = @(
            $documents | ForEach-Object {
                [ordered]@{
                    path = $_.path
                    version = $_.version
                    normalizedSha256 = Get-SDATestHash (Join-Path (Split-Path $manifestPath) $_.path)
                }
            }
        )
    }
    Write-SDATestJson $baselinePath $baseline
}

function New-SDATestFixture {
    if (Test-Path -LiteralPath $temporaryRoot) {
        Remove-Item -LiteralPath $temporaryRoot -Recurse -Force
    }
    [IO.Directory]::CreateDirectory($temporaryRoot) | Out-Null
    $docRoot = Join-Path $temporaryRoot 'docs/secure-development'
    $context = Join-Path $temporaryRoot $contextRelative

    Write-SDATestText (Join-Path $temporaryRoot '.specify/presets/security-governance/preset.yml') @'
schema_version: "1.0"
preset:
  id: "security-governance"
  version: "0.6.2"
'@
    Write-SDATestText (Join-Path $docRoot 'Richtlinie.md') @'
# Richtlinie

## Dokumenten-Metadaten

| Versionsnummer | 1.0.0 |

## Document Metadata

| Version | 1.0.0 |
'@

    $checklists = [Collections.Generic.List[object]]::new()
    $compendiumItems = [Collections.Generic.List[string]]::new()
    foreach ($number in 1..12) {
        $checklistId = 'CL-{0:D2}' -f $number
        $itemId = if ($number -eq 2) { 'CL-02-13' } else { 'CL-{0:D2}-01' -f $number }
        $relative = 'checklisten/CL_{0:D2}.md' -f $number
        $text = @(
            "## $checklistId"
            ''
            "**Dokument-ID / Document ID:** $checklistId"
            '**Version / Version:** 1.0.0'
            ''
            "#### $($itemId): Fixture item"
            ''
        ) -join [Environment]::NewLine
        Write-SDATestText (Join-Path $docRoot $relative) $text
        $checklists.Add([ordered]@{ id = $checklistId; path = $relative; version = '1.0.0' })
        $compendiumItems.Add((@(
            "## $checklistId"
            ''
            "**Dokument-ID / Document ID:** $checklistId"
            '**Version / Version:** 1.0.0'
            '**Baseline-Version / Baseline version:** 0.9.0'
            ''
            "#### $($itemId): Fixture item"
        ) -join [Environment]::NewLine))
    }

    $compendium = @(
        '# Checklistensammelband'
        ''
        '**Baseline-Version / Baseline version:** 1.0.0'
        '**Dokumentversion / Document version:** 1.0.0'
        ''
        ($compendiumItems -join ([Environment]::NewLine + [Environment]::NewLine))
        ''
    ) -join [Environment]::NewLine
    Write-SDATestText (Join-Path $docRoot 'Sammelband.md') $compendium
    Write-SDATestText (Join-Path $docRoot 'related.md') @'
# Related

**Version / Version:** 1.0.0
'@
    Write-SDATestText (Join-Path $docRoot 'learning.md') @'
# Learning

**Version / Version:** 1.0.0
'@
    Write-SDATestText (Join-Path $docRoot 'README.md') '# Managed reference'
    Write-SDATestText (Join-Path $docRoot 'binary.bin') 'fixture'

    $manifest = [ordered]@{
        schemaVersion = 1
        baselineVersion = '1.0.0'
        releaseDate = '2099-01-01'
        checklistItemCount = 12
        guideline = [ordered]@{ path = 'Richtlinie.md'; version = '1.0.0' }
        compendium = [ordered]@{ path = 'Sammelband.md'; version = '1.0.0'; generated = $true }
        statusModel = [ordered]@{
            applicability = @('Applicable', 'N/A', 'Open')
            implementation = @('Fulfilled', 'Partly Fulfilled', 'Not Fulfilled', 'Not Assessed')
        }
        checklists = @($checklists)
        relatedDocuments = @([ordered]@{ path = 'related.md'; version = '1.0.0' })
        learningDocuments = @([ordered]@{ path = 'learning.md'; version = '1.0.0' })
        managedBinaryFiles = @('binary.bin')
        managedReferenceFiles = @('README.md')
    }
    Write-SDATestJson (Join-Path $docRoot 'baseline-manifest.json') $manifest

    $baseline = Get-SDATestGate baseline
    $baseline.baselineBinding = [ordered]@{
        manifestPath = 'docs/secure-development/baseline-manifest.json'
        baselineVersion = '1.0.0'
        manifestNormalizedSha256 = '0' * 64
        documentBindings = @()
    }
    Write-SDATestJson (Join-Path $context 'baseline.json') $baseline
    Write-SDATestJson (Join-Path $context 'deltas/change.json') (Get-SDATestGate delta)

    $closure = Get-SDATestGate closure
    $closure.humanDecisions = [ordered]@{
        technicalValidation = [ordered]@{ status = 'Fulfilled'; authority = 'Technical reviewer'; evidence = 'tests/evidence.md' }
        pilotAuthorization = [ordered]@{ status = 'Open'; authority = 'Pilot owner'; evidence = 'Not authorized by this test' }
        projectAcceptance = [ordered]@{ status = 'Open'; authority = 'Project owner'; evidence = 'Not authorized by this test' }
        generalRelease = [ordered]@{ status = 'Open'; authority = 'Release owner'; evidence = 'Not authorized by this test' }
    }
    $closure.outcome = 'NeedsRemediation'
    $closure.assessments = @([ordered]@{
        id = 'CLOSE-001'
        applicability = 'Open'
        implementation = 'Not Assessed'
        evidence = 'Human decisions remain open'
        nextAction = 'Obtain explicit human decisions.'
        owner = 'Project owner'
        dueAt = '2099-12-31'
    })
    $closure.nextAction = 'Obtain explicit human decisions.'
    Write-SDATestJson (Join-Path $context 'closure.json') $closure

    $image = Get-SDATestGate image-impact
    $image.imageChecks = [ordered]@{
        build = 'Fulfilled'; compose = 'Fulfilled'; toolchain = 'Fulfilled'
        ociDigest = 'Fulfilled'; sbom = 'Fulfilled'; secrets = 'Fulfilled'
        mounts = 'Fulfilled'; network = 'Fulfilled'; ci = 'Fulfilled'
    }
    Write-SDATestJson (Join-Path $context 'image-impact.json') $image
    Write-SDATestText (Join-Path $context 'evidence-matrix.md') '# Fixture evidence matrix'
    foreach ($gate in @('baseline', 'delta', 'closure', 'image-impact')) {
        Write-SDATestText (Join-Path $temporaryRoot "docs/runbooks/secure-development/$gate-contract-test.md") "# $gate runbook"
    }
    Update-SDATestBindings
}

function Invoke-SDATestProcess {
    param(
        [Parameter(Mandatory)][string]$FilePath,
        [Parameter(Mandatory)][string[]]$Arguments
    )
    $info = [Diagnostics.ProcessStartInfo]::new()
    # Windows kann vor PATH den WSL-Stub finden; der Test bindet das zuvor erkannte Programm.
    # Windows may find the WSL stub before PATH; bind the executable selected by command discovery.
    $info.FileName = (Get-Command $FilePath -CommandType Application -ErrorAction Stop | Select-Object -First 1).Source
    $info.WorkingDirectory = $temporaryRoot
    $info.UseShellExecute = $false
    $info.RedirectStandardOutput = $true
    $info.RedirectStandardError = $true
    foreach ($argument in $Arguments) { $null = $info.ArgumentList.Add($argument) }
    $process = [Diagnostics.Process]::Start($info)
    # Nur Konsolen-CRLF wird vereinheitlicht; keine globale CR-Entfernung oder Änderung roher Evidence-Bytes.
    # Normalize console CRLF only; do not globally strip CR or change raw evidence bytes.
    $stdout = $process.StandardOutput.ReadToEnd().Replace("`r`n", "`n").Trim()
    $stderr = $process.StandardError.ReadToEnd().Replace("`r`n", "`n").Trim()
    $process.WaitForExit()
    return [pscustomobject]@{ ExitCode = $process.ExitCode; StdOut = $stdout; StdErr = $stderr }
}

function Invoke-SDATestPair {
    $bash = Invoke-SDATestProcess bash @($bashValidator, 'status', $contextRelative)
    $pwsh = Invoke-SDATestProcess pwsh @(
        '-NoProfile', '-File', $powerShellValidator, '-Action', 'Status',
        '-EvidenceDirectory', $contextRelative
    )
    return [pscustomobject]@{ Bash = $bash; PowerShell = $pwsh }
}

function Invoke-SDAReviewPair {
    param(
        [Parameter(Mandatory)]
        [ValidateSet('baseline', 'delta', 'closure', 'image-impact')]
        [string]$Gate,
        [string]$ContextId = 'contract-test',
        [string]$Mode = 'mixed'
    )
    $before = Get-SDATestSnapshot $temporaryRoot
    $bash = Invoke-SDATestProcess bash @($bashValidator, 'review', $Gate, $ContextId, $Mode)
    Assert-SDATest ([string]::Equals($before, (Get-SDATestSnapshot $temporaryRoot), [StringComparison]::Ordinal)) "Bash $Gate review changed fixture files."
    $pwsh = Invoke-SDATestProcess pwsh @(
        '-NoProfile', '-File', $powerShellValidator, '-Action', 'Review',
        '-Gate', $Gate, '-ContextId', $ContextId, '-Mode', $Mode
    )
    Assert-SDATest ([string]::Equals($before, (Get-SDATestSnapshot $temporaryRoot), [StringComparison]::Ordinal)) "PowerShell $Gate review changed fixture files."
    return [pscustomobject]@{ Bash = $bash; PowerShell = $pwsh }
}

function Assert-SDATest {
    param(
        [Parameter(Mandatory)][bool]$Condition,
        [Parameter(Mandatory)][string]$Message
    )
    if (-not $Condition) { throw "FAIL: $Message" }
}

function Invoke-SDANegativeCase {
    param(
        [Parameter(Mandatory)][scriptblock]$Mutation,
        [Parameter(Mandatory)][string]$ExpectedText
    )
    New-SDATestFixture
    & $Mutation
    $pair = Invoke-SDATestPair
    Assert-SDATest ($pair.Bash.ExitCode -eq 2) "Bash did not block: $ExpectedText"
    Assert-SDATest ($pair.PowerShell.ExitCode -eq 2) "PowerShell did not block: $ExpectedText"
    Assert-SDATest ($pair.Bash.StdErr -match [regex]::Escape($ExpectedText)) "Bash message missing: $ExpectedText"
    Assert-SDATest (-not [string]::IsNullOrWhiteSpace($pair.PowerShell.StdErr)) "PowerShell cause missing: $ExpectedText"
}

function Test-SDAActiveMetadata {
    $targets = @(
        @('checklisten/CL_03.md', '**Version / Version:** 1.0.0', '**Version / Version:**'),
        @('related.md', '**Version / Version:** 1.0.0', '**Version / Version:**'),
        @('learning.md', '**Version / Version:** 1.0.0', '**Version / Version:**'),
        @('Sammelband.md', '**Dokumentversion / Document version:** 1.0.0', '**Dokumentversion / Document version:**'),
        @('Sammelband.md', '**Baseline-Version / Baseline version:** 1.0.0', '**Baseline-Version / Baseline version:**'),
        @('checklisten/CL_03.md', '**Dokument-ID / Document ID:** CL-03', '**Dokument-ID / Document ID:**'),
        @('Richtlinie.md', '| Versionsnummer | 1.0.0 |', 'Versionsnummer'),
        @('Richtlinie.md', '| Version | 1.0.0 |', 'Version')
    )
    $count = 0
    $nl = [Environment]::NewLine
    foreach ($target in $targets) {
        foreach ($mutation in @('stale-body', 'body-only', 'prefix', 'duplicate', 'stale-inline')) {
            New-SDATestFixture
            $path = Join-Path $temporaryRoot ('docs/secure-development/' + $target[0])
            $text = Get-Content $path -Raw
            $marker = $target[1]
            $wrong = $marker.Replace('1.0.0', '9.0.0').Replace('CL-03', 'CL-99')
            $replacement = switch ($mutation) {
                'stale-body' { $wrong }
                'body-only' { '' }
                'prefix' { if ($marker.StartsWith('|')) { $marker.Replace('1.0.0', '1.0.00') } else { $marker + '0' } }
                'duplicate' { $marker }
                'stale-inline' { $wrong }
            }
            $text = $text.Replace($marker, $replacement)
            if ($mutation -in @('stale-body', 'body-only', 'duplicate')) {
                $body = if ($mutation -eq 'body-only') { 'Historical example in ordinary body text.' } else { '## Historical example' }
                $text += $nl + $body + $nl + $marker + $nl
            }
            if ($mutation -eq 'stale-inline') { $text += $nl + 'Example: ' + $marker }
            Write-SDATestText $path $text
            # Rebind mutated bytes: hash drift must not hide metadata false positives.
            # Manipulierte Bytes neu binden: Hashdrift darf Metadatenfehler nicht verdecken.
            Update-SDATestBindings
            $before = Get-SDATestSnapshot $temporaryRoot
            $pair = Invoke-SDATestPair
            foreach ($shell in @('Bash', 'PowerShell')) {
                $result = $pair.$shell
                Assert-SDATest ($result.ExitCode -eq 2) "$shell accepted metadata $($mutation): $($target[0]) / $($target[2])"
                Assert-SDATest ($result.StdErr.Contains($target[0]) -and $result.StdErr.Contains($target[2])) "$shell metadata failure omitted path/field: $($result.StdErr)"
            }
            Assert-SDATest ($before -ceq (Get-SDATestSnapshot $temporaryRoot)) 'Metadata status mutated evidence.'
            $count++
        }
    }
    foreach ($heading in @('## Dokumenten-Metadaten', '## Document Metadata')) {
        foreach ($mutation in @('missing-section', 'duplicate-section', 'second-table')) {
            New-SDATestFixture
            $path = Join-Path $temporaryRoot 'docs/secure-development/Richtlinie.md'
            $text = Get-Content $path -Raw
            $field = if ($heading -ceq '## Document Metadata') { 'Version' } else { 'Versionsnummer' }
            $text = switch ($mutation) {
                'missing-section' { $text.Replace($heading, '## Other section') }
                'duplicate-section' { $text + $nl + $heading + $nl }
                'second-table' { $text.Replace("| $field | 1.0.0 |", ('| Other | Value |' + $nl + $nl + "| $field | 1.0.0 |")) }
            }
            Write-SDATestText $path $text
            Update-SDATestBindings
            $before = Get-SDATestSnapshot $temporaryRoot
            $pair = Invoke-SDATestPair
            foreach ($shell in @('Bash', 'PowerShell')) {
                Assert-SDATest ($pair.$shell.ExitCode -eq 2) "$shell accepted guideline boundary $mutation / $field"
                Assert-SDATest ($pair.$shell.StdErr.Contains('Richtlinie.md') -and $pair.$shell.StdErr.Contains($field)) "$shell guideline error omitted path/field."
            }
            Assert-SDATest ($before -ceq (Get-SDATestSnapshot $temporaryRoot)) 'Guideline status mutated evidence.'
            $count++
        }
    }
    New-SDATestFixture
    $path = Join-Path $temporaryRoot 'docs/secure-development/Sammelband.md'
    $text = Get-Content $path -Raw
    $text = $text.Replace('**Baseline-Version / Baseline version:** 0.9.0', ('**Baseline-Version / Baseline version:** 0.9.0' + $nl + '**Baseline-Version / Baseline version:** 0.9.0'))
    Write-SDATestText $path $text
    Update-SDATestBindings
    $before = Get-SDATestSnapshot $temporaryRoot
    $pair = Invoke-SDATestPair
    foreach ($shell in @('Bash', 'PowerShell')) {
        Assert-SDATest ($pair.$shell.ExitCode -eq 2 -and $pair.$shell.StdErr.Contains('Sammelband.md') -and $pair.$shell.StdErr.Contains('Baseline-Version')) "$shell accepted duplicate embedded baseline."
    }
    Assert-SDATest ($before -ceq (Get-SDATestSnapshot $temporaryRoot)) 'Embedded metadata status mutated evidence.'
    $count++
    # Historical tables and narrative mentions remain valid; only metadata fields are unique.
    # Historientabellen und Textnennungen bleiben erlaubt; eindeutig sind Metadatenfelder.
    New-SDATestFixture
    $path = Join-Path $temporaryRoot 'docs/secure-development/Richtlinie.md'
    Add-Content $path ($nl + '## History' + $nl + '| 0.9.0 | Historical version |')
    $path = Join-Path $temporaryRoot 'docs/secure-development/related.md'
    Add-Content $path ($nl + 'Example: **Version / Version:** 1.0.0')
    Update-SDATestBindings
    $pair = Invoke-SDATestPair
    Assert-SDATest ($pair.Bash.ExitCode -eq 0 -and $pair.PowerShell.ExitCode -eq 0) 'Historical/narrative metadata mentions must stay valid.'
    "PASS: $count rebound active-metadata negative cases per shell; historical/narrative positive parity"
}

function Test-SDAAcceptedRiskIds {
    New-SDATestFixture
    $gatePaths = [ordered]@{
        baseline = 'baseline.json'; delta = 'deltas/change.json'
        closure = 'closure.json'; 'image-impact' = 'image-impact.json'
    }
    foreach ($gate in $gatePaths.Keys) {
        $path = Join-Path $temporaryRoot "$contextRelative/$($gatePaths[$gate])"
        $document = Read-SDATestJson $path
        $document | Add-Member acceptedRisks @([ordered]@{
            id = " RISK-$gate-ä "
            owner = 'Fixture owner'; reviewer = 'Fixture reviewer'
            reviewedAt = $validRiskReviewedAt; reviewDue = '2099-12-31'
            scope = 'Fixture assessment scope'; rationale = 'Explicit fixture acceptance decision.'
            residualRisk = 'Fixture risk'; reevaluationTrigger = 'Any fixture change.'
        })
        if ($gate -eq 'delta') { $document.outcome = 'ReadyWithAcceptedRisks' }
        Write-SDATestJson $path $document
    }
    $before = Get-SDATestSnapshot $temporaryRoot
    $controls = @(Invoke-SDATestPair)
    foreach ($gate in $gatePaths.Keys) { $controls += Invoke-SDAReviewPair $gate }
    foreach ($pair in $controls) {
        foreach ($result in @($pair.Bash, $pair.PowerShell)) {
            Assert-SDATest ($result.ExitCode -eq 0 -and $result.StdErr -eq '') "Valid risk ID failed: $($result | ConvertTo-Json -Compress)"
        }
        Assert-SDATest ($pair.Bash.StdOut -ceq $pair.PowerShell.StdOut) 'Valid risk ID output differs.'
    }
    Assert-SDATest ($before -ceq (Get-SDATestSnapshot $temporaryRoot)) 'Valid risk ID calls changed evidence.'

    # Jede Mutation betrifft nur die ID; beide Eintrittspunkte muessen ohne Erfolgsausgabe blockieren.
    # Mutate the ID only; both entry points must block without printing success output.
    $whitespace = -join (@(9..13) + @(32, 133, 160, 5760) + @(8192..8202) + @(8232, 8233, 8239, 8287, 12288) | ForEach-Object { [char]$_ })
    $cases = @(
        @{ name = 'missing'; key = ''; value = $null }
        @{ name = 'null'; key = 'id'; value = $null }
        @{ name = 'empty'; key = 'id'; value = '' }
        @{ name = 'whitespace'; key = 'id'; value = $whitespace }
        @{ name = 'number'; key = 'id'; value = 123 }
        @{ name = 'boolean'; key = 'id'; value = $true }
        @{ name = 'object'; key = 'id'; value = @{ text = 'RISK-001' } }
        @{ name = 'singleton-array'; key = 'id'; value = @('RISK-001') }
        @{ name = 'case-alias'; key = 'ID'; value = 'RISK-001' }
    )
    foreach ($gate in $gatePaths.Keys) {
        $path = Join-Path $temporaryRoot "$contextRelative/$($gatePaths[$gate])"
        $validText = [IO.File]::ReadAllText($path)
        $gateCases = if ($gate -eq 'delta') { $cases } else { @($cases[0]) }
        foreach ($case in $gateCases) {
            $document = $validText | ConvertFrom-Json -Depth 100 -DateKind String
            $document.acceptedRisks[0].PSObject.Properties.Remove('id')
            if ($case.key) { $document.acceptedRisks[0] | Add-Member -NotePropertyName $case.key -NotePropertyValue $case.value }
            Write-SDATestJson $path $document
            $before = Get-SDATestSnapshot $temporaryRoot
            $pairs = @((Invoke-SDATestPair), (Invoke-SDAReviewPair $gate))
            foreach ($pair in $pairs) {
                foreach ($result in @($pair.Bash, $pair.PowerShell)) {
                    Assert-SDATest ($result.ExitCode -eq 2 -and $result.StdOut -eq '' -and $result.StdErr.Contains('acceptedRisks.id', [StringComparison]::Ordinal)) "Risk ID $($case.name) ($gate) was not rejected: $($result | ConvertTo-Json -Compress)"
                }
            }
            Assert-SDATest ($before -ceq (Get-SDATestSnapshot $temporaryRoot)) "Risk ID $($case.name) calls changed evidence."
            Write-SDATestText $path $validText
        }
    }

    # jq verarbeitet sonst mehrere JSON-Wurzeln als Datenstrom und verwendet fuer -e das letzte Ergebnis.
    # jq otherwise parses multiple JSON roots as a stream and uses the last result for -e.
    $deltaPath = Join-Path $temporaryRoot "$contextRelative/deltas/change.json"
    $validDeltaText = [IO.File]::ReadAllText($deltaPath)
    $delta = $validDeltaText | ConvertFrom-Json -Depth 100 -DateKind String
    $delta.acceptedRisks[0].PSObject.Properties.Remove('id')
    $tail = [ordered]@{
        schemaVersion = '1.0'
        documentType = 'SecureDevelopmentGateEvidence'
        assessments = @([ordered]@{
            id = 'TAIL-001'; applicability = 'Applicable'; implementation = 'Fulfilled'; evidence = 'Tail fixture'
        })
        acceptedRisks = @([ordered]@{
            id = 'RISK-TAIL'; owner = 'Fixture owner'; reviewer = 'Fixture reviewer'
            reviewedAt = $validRiskReviewedAt; reviewDue = '2099-12-31'
            residualRisk = 'Fixture risk'; reevaluationTrigger = 'Any fixture change.'
        })
        externalComparisonBoundary = 'HOSK/GWDG: ExternalComparison only; never local evidence'
    }
    $streamText = ($delta | ConvertTo-Json -Depth 100) + [Environment]::NewLine + ($tail | ConvertTo-Json -Depth 100)
    Write-SDATestText $deltaPath $streamText
    $streamBefore = Get-SDATestSnapshot $temporaryRoot
    foreach ($pair in @((Invoke-SDATestPair), (Invoke-SDAReviewPair delta))) {
        foreach ($result in @($pair.Bash, $pair.PowerShell)) {
            Assert-SDATest ($result.ExitCode -eq 2 -and $result.StdOut -eq '' -and -not [string]::IsNullOrWhiteSpace($result.StdErr)) "Multiple JSON roots were not rejected: $($result | ConvertTo-Json -Compress)"
        }
    }
    Assert-SDATest ($streamBefore -ceq (Get-SDATestSnapshot $temporaryRoot)) 'Multiple-root calls changed evidence.'
    Write-SDATestText $deltaPath $validDeltaText

    'PASS: scalar nonblank accepted-risk IDs, single JSON roots, status/all gate reviews and unchanged evidence'
}

function Test-SDAAcceptedRiskBinding {
    New-SDATestFixture
    $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
    $delta = Read-SDATestJson $deltaPath
    $delta.assessments[0].implementation = 'Not Fulfilled'
    $delta.assessments[0] | Add-Member nextAction 'Resolve fixture finding'
    $delta.assessments[0] | Add-Member owner 'Fixture owner'
    $delta.assessments[0] | Add-Member dueAt '2099-12-31'
    $delta.outcome = 'ReadyWithAcceptedRisks'
    $delta | Add-Member acceptedRisks @([ordered]@{
        id = 'RISK-001'; owner = 'Fixture owner'; reviewer = 'Fixture reviewer'
        reviewedAt = '2026-11-01T00:00:00Z'; reviewDue = '2099-12-31'
        scope = 'TEST-001 in delta'; rationale = 'Explicit fixture risk decision.'
        residualRisk = 'Bounded fixture risk'; reevaluationTrigger = 'Any fixture change.'
        assessmentIds = @('TEST-001')
    })
    Write-SDATestJson $deltaPath $delta
    Update-SDATestBindings
    $pair = Invoke-SDATestPair
    Assert-SDATest ($pair.Bash.ExitCode -eq 0 -and $pair.PowerShell.ExitCode -eq 0) 'Bound accepted risk was rejected.'

    $delta.acceptedRisks[0].reviewedAt = [DateTimeOffset]::UtcNow.ToString('yyyy-MM-ddT00:00:00Z')
    Write-SDATestJson $deltaPath $delta
    Update-SDATestBindings
    $pair = Invoke-SDATestPair
    Assert-SDATest ($pair.Bash.ExitCode -eq 0 -and $pair.PowerShell.ExitCode -eq 0) `
        'Past risk review from the current UTC day was rejected.'
    $delta.acceptedRisks[0].reviewedAt = $validRiskReviewedAt

    $delta.acceptedRisks[0].assessmentIds = @('UNRELATED-001')
    Write-SDATestJson $deltaPath $delta
    Update-SDATestBindings
    $pair = Invoke-SDATestPair
    Assert-SDATest ($pair.Bash.ExitCode -eq 2 -and $pair.PowerShell.ExitCode -eq 2) 'Unrelated risk covered an unresolved assessment.'

    $delta.acceptedRisks[0].assessmentIds = @('TEST-001')
    $futureUtc = [DateTimeOffset]::UtcNow.AddDays(1)
    foreach ($mutation in @(
        @{ Field = 'scope'; Value = '' },
        @{ Field = 'rationale'; Value = '' },
        @{ Field = 'reviewDue'; Value = '2020-01-01' },
        @{ Field = 'reviewedAt'; Value = 'not-a-date' },
        @{ Field = 'reviewedAt'; Value = '2026-11-01T00:00:00+14:01' },
        @{ Field = 'reviewedAt'; Value = $futureUtc.ToString('yyyy-MM-ddTHH:mm:ssZ') },
        @{ Field = 'reviewedAt'; Value = $futureUtc.ToOffset([TimeSpan]::FromHours(14)).ToString('yyyy-MM-ddTHH:mm:sszzz') },
        @{ Field = 'reviewedAt'; Value = $futureUtc.ToOffset([TimeSpan]::FromHours(-12)).ToString('yyyy-MM-ddTHH:mm:sszzz') }
    )) {
        $original = $delta.acceptedRisks[0].($mutation.Field)
        $delta.acceptedRisks[0].($mutation.Field) = $mutation.Value
        Write-SDATestJson $deltaPath $delta
        Update-SDATestBindings
        $pair = Invoke-SDATestPair
        Assert-SDATest ($pair.Bash.ExitCode -eq 2 -and $pair.PowerShell.ExitCode -eq 2) `
            "Invalid accepted risk $($mutation.Field) was accepted."
        $delta.acceptedRisks[0].($mutation.Field) = $original
    }
    $delta.acceptedRisks[0].reviewedAt = [DateTimeOffset]::UtcNow.AddDays(-1).ToOffset(
        [TimeSpan]::FromHours(14)).ToString('yyyy-MM-ddTHH:mm:sszzz')
    Write-SDATestJson $deltaPath $delta
    Update-SDATestBindings
    $pair = Invoke-SDATestPair
    Assert-SDATest ($pair.Bash.ExitCode -eq 0 -and $pair.PowerShell.ExitCode -eq 0) `
        'Past accepted risk with a positive UTC offset was rejected.'
}

function Test-SDAContextAndRiskTypes {
    New-SDATestFixture
    $failures = [Collections.Generic.List[string]]::new()
    function Assert-BlockedPair($Pair, [string]$Case) {
        foreach ($shell in @('Bash', 'PowerShell')) {
            $result = $Pair.$shell
            if ($result.ExitCode -ne 2 -or $result.StdOut -ne '' -or $result.StdErr -eq '') {
                $failures.Add("${Case}/${shell}: $($result | ConvertTo-Json -Compress)")
            }
        }
    }
    $paths = [ordered]@{ baseline='baseline.json'; delta='deltas/change.json'; closure='closure.json'; 'image-impact'='image-impact.json' }
    $contextPath = Join-Path $temporaryRoot $contextRelative
    # Ein Suffix ist keine Identitaet; die falsche Auswahl muss trotz gueltigem Runbook blockieren.
    # A suffix is not identity; reject the wrong context even with a valid requested runbook.
    foreach ($gate in $paths.Keys) {
        Write-SDATestText (Join-Path $temporaryRoot "docs/runbooks/secure-development/$gate-test.md") '# Fixture runbook'
        Assert-BlockedPair (Invoke-SDAReviewPair $gate -ContextId test) "suffix/$gate"
        $path = Join-Path $contextPath $paths[$gate]
        $original = [IO.File]::ReadAllText($path)
        $doc = Read-SDATestJson $path
        $doc.contextId = 'wrong-context'
        Write-SDATestJson $path $doc
        Assert-BlockedPair (Invoke-SDAReviewPair $gate) "evidence-id/$gate"
        Write-SDATestText $path $original
        Assert-BlockedPair (Invoke-SDAReviewPair $gate -Mode development) "mode/$gate"

        foreach ($riskShape in @('missing', 'empty')) {
            $doc = Read-SDATestJson $path
            $doc.outcome = 'ReadyWithAcceptedRisks'
            if ($riskShape -eq 'empty') {
                $doc | Add-Member -NotePropertyName acceptedRisks -NotePropertyValue @()
            }
            Write-SDATestJson $path $doc
            Assert-BlockedPair (Invoke-SDAReviewPair $gate) "risk-$riskShape/$gate"
            Assert-BlockedPair (Invoke-SDATestPair) "risk-$riskShape-status/$gate"
            Write-SDATestText $path $original
        }

        $doc = Read-SDATestJson $path
        $doc.outcome = 'ReadyWithAcceptedRisks'
        $doc | Add-Member acceptedRisks ([ordered]@{
            id='R001'; owner='Fixture owner'; reviewer='Fixture reviewer'
            reviewedAt='2099-01-01T00:00:00Z'; reviewDue='2099-12-31'
            residualRisk='Fixture'; reevaluationTrigger='Fixture change'
        })
        Write-SDATestJson $path $doc
        Assert-BlockedPair (Invoke-SDAReviewPair $gate) "risk-object/$gate"
        Assert-BlockedPair (Invoke-SDATestPair) "risk-object-status/$gate"
        Write-SDATestText $path $original
    }
    $duplicate = Join-Path $temporaryRoot 'docs/security/secure-development/2099-02-01-contract-test'
    Copy-Item -LiteralPath $contextPath -Destination $duplicate -Recurse
    Assert-BlockedPair (Invoke-SDAReviewPair delta) 'duplicate-context'
    Remove-Item -LiteralPath $duplicate -Recurse -Force
    $deltaPath = Join-Path $contextPath 'deltas/change.json'
    $original = [IO.File]::ReadAllText($deltaPath)
    foreach ($invalid in @('null', 'false', '42', '"risk"')) {
        $doc = Read-SDATestJson $deltaPath
        $doc | Add-Member acceptedRisks ($invalid | ConvertFrom-Json)
        Write-SDATestJson $deltaPath $doc
        Assert-BlockedPair (Invoke-SDAReviewPair delta) "risk-type/$invalid"
        Assert-BlockedPair (Invoke-SDATestPair) "risk-type-status/$invalid"
        Write-SDATestText $deltaPath $original
    }
    Assert-SDATest ($failures.Count -eq 0) ("Context/risk regression failures: " + ($failures -join "`n"))
    'PASS: exact unique context, evidence ID/mode binding and array-only risks'
}

function Test-SDATestSnapshot {
    $snapshotRoot = Join-Path $temporaryRoot 'snapshot-regression'
    foreach ($relative in @('Z.txt', 'a.txt', '.hidden', '.metadata/inside.txt')) {
        Write-SDATestText (Join-Path $snapshotRoot $relative) "original`n"
    }
    if ($IsWindows) {
        foreach ($relative in @('.hidden', '.metadata')) {
            $hiddenPath = Join-Path $snapshotRoot $relative
            [IO.File]::SetAttributes($hiddenPath, ([IO.File]::GetAttributes($hiddenPath) -bor [IO.FileAttributes]::Hidden))
        }
    }
    $files = @(Get-ChildItem -LiteralPath $snapshotRoot -File -Recurse -Force)
    $before = Get-SDATestSnapshot $snapshotRoot -Files $files
    [array]::Reverse($files)
    $reordered = Get-SDATestSnapshot $snapshotRoot -Files $files
    Assert-SDATest ([string]::Equals($before, $reordered, [StringComparison]::Ordinal)) 'Snapshot depends on enumeration order.'
    Assert-SDATest ([string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Snapshot omits hidden files.'

    $records = @($before | ConvertFrom-Json)
    Assert-SDATest (($records.path -join '|') -ceq '.hidden|.metadata/inside.txt|Z.txt|a.txt') 'Snapshot paths are not relative, normalized and ordinally sorted.'
    foreach ($record in $records) {
        $expectedHash = (Get-FileHash -LiteralPath (Join-Path $snapshotRoot $record.path) -Algorithm SHA256).Hash
        Assert-SDATest ($record.sha256 -ceq $expectedHash) 'Snapshot does not contain the actual SHA-256 value.'
    }

    # Windows verweigert WriteAllText beim Überschreiben versteckter Dateien; deren Aufzählung bleibt separat geprüft.
    # Windows denies WriteAllText when overwriting hidden files; hidden-file enumeration stays separately tested.
    $mutablePath = Join-Path $snapshotRoot 'a.txt'
    $normalizedHash = Get-SDATestHash $mutablePath
    Write-SDATestText $mutablePath "original`n" -Bom -CrLf
    Assert-SDATest ($normalizedHash -ceq (Get-SDATestHash $mutablePath)) 'Semantic hash no longer normalizes BOM/CRLF.'
    Assert-SDATest (-not [string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Raw snapshot missed a BOM/CRLF change.'
    Write-SDATestText $mutablePath "changed`n"
    Assert-SDATest (-not [string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Snapshot missed changed content.'
    Write-SDATestText $mutablePath "original`n"

    $addedPath = Join-Path $snapshotRoot 'added.txt'
    Write-SDATestText $addedPath 'added'
    Assert-SDATest (-not [string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Snapshot missed an added file.'
    Remove-Item -LiteralPath $addedPath
    Remove-Item -LiteralPath $mutablePath -Force
    Assert-SDATest (-not [string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Snapshot missed a deleted file.'
    Write-SDATestText $mutablePath "original`n"
    Rename-Item -LiteralPath $mutablePath -NewName 'renamed.txt' -Force
    Assert-SDATest (-not [string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Snapshot missed a renamed file.'
    Rename-Item -LiteralPath (Join-Path $snapshotRoot 'renamed.txt') -NewName 'a.txt' -Force
    Assert-SDATest ([string]::Equals($before, (Get-SDATestSnapshot $snapshotRoot), [StringComparison]::Ordinal)) 'Restored files did not restore the snapshot.'
    Assert-SDATest ((Get-SDATestSnapshot $snapshotRoot -Files @()) -ceq '[]') 'Empty snapshot is not deterministic.'
    'PASS: deterministic raw-byte snapshots, hidden files and mutation detection'
}

function Test-SDAGateReviews {
    foreach ($gate in @('baseline', 'delta', 'closure', 'image-impact')) {
        # Ein bestandener Validatorlauf ersetzt keine noch offene menschliche Freigabe.
        # A successful validator run does not replace a human decision that remains open.
        $outcome = if ($gate -eq 'closure') { 'NeedsRemediation' } else { 'Ready' }
        $expectedOutput = "Reviewed: gate=$gate context=contract-test mode=mixed outcome=$outcome"
        $pair = Invoke-SDAReviewPair $gate
        Assert-SDATest ($pair.Bash.ExitCode -eq 0) "Bash $gate review failed: $($pair.Bash.StdErr)"
        Assert-SDATest ($pair.PowerShell.ExitCode -eq 0) "PowerShell $gate review failed: $($pair.PowerShell.StdErr)"
        Assert-SDATest ($pair.Bash.StdOut -ceq $expectedOutput) "Bash $gate review output is not the expected gate result."
        Assert-SDATest ($pair.PowerShell.StdOut -ceq $expectedOutput) "PowerShell $gate review output differs from the expected Bash result."
        Assert-SDATest ([string]::IsNullOrWhiteSpace($pair.Bash.StdErr)) "Bash $gate review wrote unexpected errors."
        Assert-SDATest ([string]::IsNullOrWhiteSpace($pair.PowerShell.StdErr)) "PowerShell $gate review wrote unexpected errors."

        $runbookPath = Join-Path $temporaryRoot "docs/runbooks/secure-development/$gate-contract-test.md"
        $runbookText = Get-Content -LiteralPath $runbookPath -Raw
        Remove-Item -LiteralPath $runbookPath
        $missingRunbook = Invoke-SDAReviewPair $gate
        foreach ($result in @($missingRunbook.Bash, $missingRunbook.PowerShell)) {
            Assert-SDATest ($result.ExitCode -eq 2) "Missing $gate runbook did not block review."
            Assert-SDATest ($result.StdErr -match [regex]::Escape('Runbook fehlt für mixed:')) "Missing $gate runbook cause is absent."
            Assert-SDATest ($result.StdErr -match [regex]::Escape("$gate-contract-test.md")) "Missing $gate runbook path is absent."
            Assert-SDATest ([string]::IsNullOrWhiteSpace($result.StdOut)) "Blocked $gate review reported successful output."
        }
        Write-SDATestText $runbookPath $runbookText
    }

    $closure = Read-SDATestJson (Join-Path $temporaryRoot "$contextRelative/closure.json")
    Assert-SDATest ($closure.outcome -ceq 'NeedsRemediation') 'Gate reviews changed the closure outcome.'
    Assert-SDATest ($closure.humanDecisions.technicalValidation.status -ceq 'Fulfilled') 'Gate reviews changed fixture technical validation.'
    foreach ($decision in @('pilotAuthorization', 'projectAcceptance', 'generalRelease')) {
        Assert-SDATest ($closure.humanDecisions.$decision.status -ceq 'Open') "Gate reviews granted an open human decision: $decision"
    }
    'PASS: four mixed-mode gate reviews, four missing-runbook blockers and unchanged human decisions'
}

function Test-SDAJqOutputMode {
    param([Parameter(Mandatory)][string]$ExpectedStatus)

    $shimRoot = Join-Path $temporaryRoot 'jq-output-mode'
    # Der Adapter simuliert nur jq-Fähigkeiten; echte JSON-Abfragen bleiben beim installierten jq.
    # The adapter simulates jq capabilities only; real JSON queries still use the installed jq.
    $shimContent = @'
#!/usr/bin/env bash
set -euo pipefail
if [[ "$SDA_JQ_TEST_MODE" == binary ]]; then
  [[ "${1:-}" == --binary ]] || { printf 'Missing --binary argument\n' >&2; exit 97; }
  shift
  if [[ "$SDA_JQ_TEST_REAL_BINARY" == 1 ]]; then
    exec "$SDA_JQ_TEST_REAL" --binary "$@"
  fi
  exec "$SDA_JQ_TEST_REAL" "$@"
fi
[[ "${1:-}" != --binary ]] || exit 2
case "$SDA_JQ_TEST_MODE" in
  legacy-lf) printf 'a\nb\n' ;;
  legacy-crlf) printf 'a\r\nb\r\n' ;;
  legacy-error) printf 'a\nb\n'; printf 'Simulated jq diagnostic\n' >&2 ;;
esac
'@
    Write-SDATestText (Join-Path $shimRoot 'jq') ($shimContent.Replace("`r`n", "`n"))
    $launch = @'
set -euo pipefail
shim_root=$1
validator=$2
real_jq=$3
if command -v cygpath >/dev/null 2>&1; then
  shim_root=$(cygpath -u "$shim_root")
  validator=$(cygpath -u "$validator")
  real_jq=$(cygpath -u "$real_jq")
fi
chmod +x "$shim_root/jq"
export SDA_JQ_TEST_MODE="$4" SDA_JQ_TEST_REAL="$real_jq" SDA_JQ_TEST_REAL_BINARY=0
if ( "$real_jq" --binary -n null >/dev/null 2>&1 ); then export SDA_JQ_TEST_REAL_BINARY=1; fi
export PATH="$shim_root:$PATH"
exec "$BASH" "$validator" status "$5"
'@
    $launch = $launch.Replace("`r`n", "`n")
    $jqPath = (Get-Command jq -CommandType Application -ErrorAction Stop | Select-Object -First 1).Source
    $before = Get-SDATestSnapshot $temporaryRoot
    foreach ($mode in @('binary', 'legacy-lf', 'legacy-crlf', 'legacy-error')) {
        $context = if ($mode -eq 'binary') { $contextRelative } else { 'missing-context' }
        $result = Invoke-SDATestProcess bash @('-c', $launch, 'sda-jq-mode', $shimRoot, $bashValidator, $jqPath, $mode, $context)
        if ($mode -eq 'binary') {
            Assert-SDATest ($result.ExitCode -eq 0) "Binary jq routing failed (exit=$($result.ExitCode)): $($result.StdErr)"
            Assert-SDATest ($result.StdOut -ceq $ExpectedStatus) 'Binary jq routing changed the status result.'
        } else {
            $cause = switch ($mode) {
                'legacy-lf' { 'Evidence-Verzeichnis fehlt' }
                'legacy-crlf' { 'requires --binary support' }
                'legacy-error' { 'jq output probe failed' }
            }
            if ($result.ExitCode -ne 2 -or $result.StdErr -notmatch [regex]::Escape($cause)) {
                # Ein Trace betrifft nur die synthetische Fehlfixture und ersetzt keine Gate-Bewertung.
                # Trace the synthetic missing-context fixture only; it never replaces a gate result.
                $traceLaunch = $launch.Replace('exec "$BASH" "$validator"', 'exec "$BASH" -x "$validator"')
                $diagnostic = Invoke-SDATestProcess bash @('-c', $traceLaunch, 'sda-jq-trace', $shimRoot, $bashValidator, $jqPath, $mode, 'missing-context')
                $trace = $diagnostic.StdErr.Substring(0, [Math]::Min(8192, $diagnostic.StdErr.Length))
                Assert-SDATest $false "jq $mode unexpected result (exit=$($result.ExitCode)): stdout=[$($result.StdOut)] stderr=[$($result.StdErr)] diagnostic-only-trace=[$trace]"
            }
        }
        Assert-SDATest ([string]::Equals($before, (Get-SDATestSnapshot $temporaryRoot), [StringComparison]::Ordinal)) "jq $mode probe changed fixture files."
    }
    'PASS: binary jq routing, legacy LF compatibility and fail-closed CRLF/error probes'
}

function Test-SDAConfinedBaselineEvidence {
    $outsideRoot = "${temporaryRoot}-outside"
    [void](New-Item -ItemType Directory -Path $outsideRoot -Force)
    try {
        foreach ($relative in @('docs/secure-development/baseline-manifest.json', 'docs/secure-development/related.md')) {
            New-SDATestFixture
            $inside = Join-Path $temporaryRoot $relative
            $outside = Join-Path $outsideRoot ([IO.Path]::GetFileName($relative))
            Copy-Item -LiteralPath $inside -Destination $outside -Force
            Remove-Item -LiteralPath $inside -Force
            [void](New-Item -ItemType SymbolicLink -Path $inside -Target $outside)
            $pair = Invoke-SDATestPair
            Assert-SDATest ($pair.Bash.ExitCode -eq 2 -and $pair.PowerShell.ExitCode -eq 2) `
                "External baseline evidence symlink was accepted: $relative"
            Assert-SDATest ($pair.Bash.StdErr -match 'verlaesst das Repository') `
                "Bash rejected external baseline evidence for an unrelated reason: $relative"
        }
    } finally {
        Remove-Item -LiteralPath $outsideRoot -Recurse -Force -ErrorAction SilentlyContinue
    }
}

try {
    foreach ($command in @('bash', 'pwsh', 'jq')) {
        if (-not (Get-Command $command -ErrorAction SilentlyContinue)) {
            throw "Required test command missing: $command"
        }
    }

    Test-SDAActiveMetadata
    Test-SDAContextAndRiskTypes
    Test-SDAAcceptedRiskIds
    Test-SDAAcceptedRiskBinding
    Test-SDAConfinedBaselineEvidence
    New-SDATestFixture
    Test-SDATestSnapshot
    $before = Get-SDATestSnapshot $temporaryRoot
    $positive = Invoke-SDATestPair
    Assert-SDATest ($positive.Bash.ExitCode -eq 0) "Positive Bash status failed (exit=$($positive.Bash.ExitCode)): stdout=[$($positive.Bash.StdOut)] stderr=[$($positive.Bash.StdErr)]"
    Assert-SDATest ($positive.PowerShell.ExitCode -eq 0) "Positive PowerShell status failed (exit=$($positive.PowerShell.ExitCode)): stdout=[$($positive.PowerShell.StdOut)] stderr=[$($positive.PowerShell.StdErr)]"
    Assert-SDATest ($positive.Bash.StdOut -eq $positive.PowerShell.StdOut) 'Status output differs between Bash and PowerShell.'
    $after = Get-SDATestSnapshot $temporaryRoot
    Assert-SDATest ([string]::Equals($before, $after, [StringComparison]::Ordinal)) 'Status changed fixture files.'
    Test-SDAJqOutputMode -ExpectedStatus $positive.Bash.StdOut
    Test-SDAGateReviews

    New-SDATestFixture
    $readyClosurePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'closure.json')
    $readyClosure = Read-SDATestJson $readyClosurePath
    $readyClosure.outcome = 'Ready'
    $readyClosure.assessments[0].applicability = 'Applicable'
    $readyClosure.assessments[0].implementation = 'Fulfilled'
    Write-SDATestJson $readyClosurePath $readyClosure
    $readyBash = Invoke-SDATestProcess bash @($bashValidator, 'ready', $contextRelative)
    $readyPowerShell = Invoke-SDATestProcess pwsh @(
        '-NoProfile', '-File', $powerShellValidator, '-Action', 'Status',
        '-RequireReady', '-EvidenceDirectory', $contextRelative
    )
    Assert-SDATest ($readyBash.ExitCode -eq 0 -and $readyPowerShell.ExitCode -eq 0) 'Ready CI gates rejected valid evidence.'
    New-SDATestFixture
    $blockedPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
    $blockedDelta = Read-SDATestJson $blockedPath
    $blockedDelta.outcome = 'Blocked'
    Write-SDATestJson $blockedPath $blockedDelta
    $statusPair = Invoke-SDATestPair
    Assert-SDATest ($statusPair.Bash.ExitCode -eq 0 -and $statusPair.PowerShell.ExitCode -eq 0) 'Diagnostic Status rejected valid Blocked evidence.'
    $blockedBash = Invoke-SDATestProcess bash @($bashValidator, 'ready', $contextRelative)
    $blockedPowerShell = Invoke-SDATestProcess pwsh @(
        '-NoProfile', '-File', $powerShellValidator, '-Action', 'Status',
        '-RequireReady', '-EvidenceDirectory', $contextRelative
    )
    Assert-SDATest ($blockedBash.ExitCode -ne 0 -and $blockedPowerShell.ExitCode -ne 0) 'Blocked evidence passed a CI readiness gate.'

    Invoke-SDANegativeCase {
        Add-Content -LiteralPath (Join-Path $temporaryRoot 'docs/secure-development/baseline-manifest.json') -Value ' '
    } 'Manifest-Hashdrift'

    Invoke-SDANegativeCase {
        Remove-Item -LiteralPath (Join-Path $temporaryRoot 'docs/secure-development/checklisten/CL_12.md')
    } 'Kontrolliertes Dokument fehlt'

    Invoke-SDANegativeCase {
        $manifestPath = Join-Path $temporaryRoot 'docs/secure-development/baseline-manifest.json'
        $manifest = Read-SDATestJson $manifestPath
        $manifest.checklists[11].id = 'CL-11'
        Write-SDATestJson $manifestPath $manifest
        Update-SDATestBindings
    } 'Checklisten-IDs müssen eindeutig'

    Invoke-SDANegativeCase {
        $checklistPath = Join-Path $temporaryRoot 'docs/secure-development/checklisten/CL_03.md'
        $text = (Get-Content -LiteralPath $checklistPath -Raw).Replace(
            '**Version / Version:** 1.0.0',
            '**Version / Version:** 2.0.0'
        )
        Write-SDATestText $checklistPath $text
        Update-SDATestBindings
    } 'Aktive Metadaten fehlen, weichen ab oder sind doppelt: checklisten/CL_03.md (**Version / Version:**)'

    Invoke-SDANegativeCase {
        $compendiumPath = Join-Path $temporaryRoot 'docs/secure-development/Sammelband.md'
        $text = (Get-Content -LiteralPath $compendiumPath -Raw).Replace('#### CL-12-01: Fixture item', '')
        Write-SDATestText $compendiumPath $text
        Update-SDATestBindings
    } 'Sammelbanddrift'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.assessments[0].applicability = 'Open'
        $delta.assessments[0].implementation = 'Not Assessed'
        $delta.outcome = 'Ready'
        $delta.assessments[0] | Add-Member nextAction 'Resolve the item.'
        $delta.assessments[0] | Add-Member owner 'Fixture owner'
        $delta.assessments[0] | Add-Member dueAt '2099-12-31'
        Write-SDATestJson $deltaPath $delta
    } 'Ready ist bei offenen oder unerfüllten Pflichtpunkten unzulässig'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.assessments[0].applicability = 'N/A'
        $delta.assessments[0].implementation = 'Not Assessed'
        Write-SDATestJson $deltaPath $delta
    } 'N/A benötigt Begründung'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.review.reviewDue = '2000-01-01'
        Write-SDATestJson $deltaPath $delta
    } 'Review ist abgelaufen'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.review.reviewDue = '2099-02-30'
        Write-SDATestJson $deltaPath $delta
    } 'review.reviewDue ist kein ISO-Datum'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.contextId = 'other-context'
        Write-SDATestJson $deltaPath $delta
    } 'Kontext-ID-Drift'

    Invoke-SDANegativeCase {
        $imagePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'image-impact.json')
        $image = Read-SDATestJson $imagePath
        $image.mode = 'training'
        Write-SDATestJson $imagePath $image
    } 'Betriebsart-Drift'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.assessments[0].implementation = @{ malformed = $true }
        Write-SDATestJson $deltaPath $delta
    } 'Assessment-Felder sind ungueltig'

    Invoke-SDANegativeCase {
        $imagePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'image-impact.json')
        $image = Read-SDATestJson $imagePath
        $image.imageChecks.build = 42
        Write-SDATestJson $imagePath $image
    } 'Image-Impact-Nachweise sind unvollständig'

    Invoke-SDANegativeCase {
        $closurePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'closure.json')
        $closure = Read-SDATestJson $closurePath
        $closure.humanDecisions.technicalValidation = $null
        Write-SDATestJson $closurePath $closure
    } 'Getrennte Entscheidungsgrenzen fehlen'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.review.residualRisk = 'C5 certified'
        Write-SDATestJson $deltaPath $delta
        $text = [IO.File]::ReadAllText($deltaPath).Replace('C5 certified', 'C5\u0020certified')
        Write-SDATestText $deltaPath $text
    } 'Unzulässige Zertifizierungs-, Konformitäts- oder Testatbehauptung'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.review.residualRisk = 'C5 certified ' + ('x' * 100000)
        Write-SDATestJson $deltaPath $delta
    } 'Unzulässige Zertifizierungs-, Konformitäts- oder Testatbehauptung'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta.outcome = 'ReadyWithAcceptedRisks'
        $delta | Add-Member acceptedRisks @([ordered]@{
            id = 'RISK-001'
            owner = 'Fixture owner'
            reviewedAt = '2099-01-01T00:00:00Z'
            reviewDue = '2099-12-31'
            residualRisk = 'Fixture risk'
            reevaluationTrigger = 'Any change'
        })
        Write-SDATestJson $deltaPath $delta
    } 'Akzeptierte Risiken benötigen Owner, Reviewer'

    Invoke-SDANegativeCase {
        $closurePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'closure.json')
        $closure = Read-SDATestJson $closurePath
        $closure.humanDecisions.technicalValidation.status = 'Open'
        $closure.humanDecisions.pilotAuthorization.status = 'Fulfilled'
        Write-SDATestJson $closurePath $closure
    } 'Menschliche Freigabe darf technische Validierung nicht überspringen'

    Invoke-SDANegativeCase {
        $closurePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'closure.json')
        $closure = Read-SDATestJson $closurePath
        $closure.nextAction = "Review evidence.`nOverall: Ready"
        Write-SDATestJson $closurePath $closure
    } 'nextAction enthält Steuerzeichen'
    Invoke-SDANegativeCase {
        $closurePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'closure.json')
        $closure = Read-SDATestJson $closurePath
        $closure.nextAction = 'Review evidence.' + [char]0x2028 + 'Overall: Ready'
        Write-SDATestJson $closurePath $closure
    } 'nextAction enthält Steuerzeichen'
    Invoke-SDANegativeCase {
        $closurePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'closure.json')
        $closure = Read-SDATestJson $closurePath
        $closure.nextAction = $false
        Write-SDATestJson $closurePath $closure
    } 'nextAction enthält Steuerzeichen'

    Invoke-SDANegativeCase {
        $deltaPath = Join-Path $temporaryRoot (Join-Path $contextRelative 'deltas/change.json')
        $delta = Read-SDATestJson $deltaPath
        $delta | Add-Member certificationClaim 'C5 compliant'
        Write-SDATestJson $deltaPath $delta
    } 'Unzulässige Zertifizierungs'

    Invoke-SDANegativeCase {
        Remove-Item -LiteralPath (Join-Path $temporaryRoot '.specify/presets/security-governance/preset.yml')
    } 'Fachliche Voraussetzung fehlt'

    Invoke-SDANegativeCase {
        $imagePath = Join-Path $temporaryRoot (Join-Path $contextRelative 'image-impact.json')
        $image = Read-SDATestJson $imagePath
        $image.imageChecks.PSObject.Properties.Remove('sbom')
        Write-SDATestJson $imagePath $image
    } 'Image-Impact-Nachweise sind unvollständig'

    New-SDATestFixture
    $controlledFiles = Get-ChildItem -LiteralPath (Join-Path $temporaryRoot 'docs/secure-development') -File -Recurse |
        Where-Object { $_.Extension -in @('.md', '.json', '.yml') }
    foreach ($file in $controlledFiles) {
        $text = [Text.UTF8Encoding]::new($false, $true).GetString([IO.File]::ReadAllBytes($file.FullName))
        Write-SDATestText $file.FullName $text -Bom -CrLf
    }
    $lineEndingPair = Invoke-SDATestPair
    Assert-SDATest ($lineEndingPair.Bash.ExitCode -eq 0) "BOM/CRLF Bash status failed: $($lineEndingPair.Bash.StdErr)"
    Assert-SDATest ($lineEndingPair.PowerShell.ExitCode -eq 0) "BOM/CRLF PowerShell status failed: $($lineEndingPair.PowerShell.StdErr)"

    'PASS: secure-development-assurance validator contract and Bash/PowerShell parity'
} finally {
    if (Test-Path -LiteralPath $temporaryRoot) {
        Remove-Item -LiteralPath $temporaryRoot -Recurse -Force
    }
}
