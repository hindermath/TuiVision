#Requires -Version 7
<#
.SYNOPSIS
Validates Documentation Impact evidence.

.DESCRIPTION
Prueft die vier verbindlichen Dokumentationsentscheidungen und ihre
deterministischen Evidence-Grenzen. Validates the four binding documentation
decisions and their deterministic evidence boundaries.

.PARAMETER Evidence
Repository-relative or absolute path to the JSON evidence file.

.EXAMPLE
pwsh -NoProfile -File scripts/validate-documentation-impact.ps1 -Evidence specs/012-documentation-impact-governance/documentation-impact-evidence.json
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$Evidence
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Test-TextValue {
    param([object]$Value)
    return $null -ne $Value -and -not [string]::IsNullOrWhiteSpace([string]$Value)
}

function Test-RepoPath {
    param([object]$Value)
    if (-not (Test-TextValue $Value)) { return $false }
    $text = [string]$Value
    return -not [System.IO.Path]::IsPathRooted($text) -and
        $text -notmatch '(^|[\\/])\.\.([\\/]|$)'
}

function Get-PropertyValue {
    param([object]$Object, [string]$Name)
    $property = $Object.PSObject.Properties[$Name]
    if ($null -eq $property) {
        return [PSCustomObject]@{ Value = $null }
    }
    return $property
}

function Test-NonEmptyStringArray {
    param([object]$Value)
    if ($Value -isnot [System.Array] -or @($Value).Count -eq 0) { return $false }
    return @($Value | Where-Object { -not (Test-TextValue $_) }).Count -eq 0
}

try {
    $resolved = (Resolve-Path -LiteralPath $Evidence -ErrorAction Stop).Path
    $document = Get-Content -LiteralPath $resolved -Raw -Encoding utf8 |
        ConvertFrom-Json -Depth 32
    $errors = [System.Collections.Generic.List[string]]::new()
    $decisions = @('UpdateRequired', 'NoUpdateRequired', 'GeneratedUpdate', 'FollowUp')
    $criticalities = @('Normal', 'Security', 'Usage', 'BreakingChange')

    if ($document.schemaVersion -notin @('1.0', '1.1')) {
        $errors.Add('SCHEMA: schemaVersion must equal 1.0 or 1.1.')
    }
    if (-not (Test-TextValue $document.feature)) {
        $errors.Add('IDENTITY: feature is required.')
    }
    if ($null -eq $document.entries -or @($document.entries).Count -eq 0) {
        $errors.Add('ENTRIES: at least one entry is required.')
    }

    $ids = @{}
    foreach ($entry in @($document.entries)) {
        $id = [string]$entry.changeId
        $decision = (Get-PropertyValue $entry 'decision').Value
        $criticality = (Get-PropertyValue $entry 'criticality').Value
        $documents = (Get-PropertyValue $entry 'documents').Value
        $riskValue = (Get-PropertyValue $entry 'risk').Value
        $reevaluationValue = (Get-PropertyValue $entry 'reevaluationTrigger').Value
        if ($id -notmatch '^CHG[0-9]{3,}$') {
            $errors.Add("IDENTITY: invalid changeId '${id}'.")
        } elseif ($ids.ContainsKey($id)) {
            $errors.Add("DUPLICATE: duplicate changeId '${id}'.")
        } else {
            $ids[$id] = $true
        }

        if (-not (Test-TextValue $entry.scope) -or
            -not (Test-TextValue $entry.rationale) -or
            -not (Test-TextValue $entry.owner) -or
            -not (Test-RepoPath $entry.evidence)) {
            $errors.Add("REQUIRED: '${id}' lacks scope, rationale, owner, or repository-relative evidence.")
        }
        if ($decision -notin $decisions) {
            $errors.Add("DECISION: '${id}' has an unknown or missing decision.")
            continue
        }
        if ($criticality -notin $criticalities) {
            $errors.Add("CRITICALITY: '${id}' has an unknown or missing criticality.")
        }
        foreach ($path in @($documents)) {
            if (-not (Test-RepoPath $path)) {
                $errors.Add("PATH: '${id}' contains a non-repository-relative document path.")
            }
        }

        if ($document.schemaVersion -eq '1.1') {
            $audiences = (Get-PropertyValue $entry 'audiences').Value
            $readerPaths = (Get-PropertyValue $entry 'readerPaths').Value
            $canonicalSource = (Get-PropertyValue $entry 'canonicalSource').Value
            $navigationImpact = (Get-PropertyValue $entry 'navigationImpact').Value
            $documentClass = (Get-PropertyValue $entry 'documentClass').Value
            $languageStrategy = (Get-PropertyValue $entry 'languageStrategy').Value
            $languagePartners = (Get-PropertyValue $entry 'languagePartners').Value
            $platformProof = (Get-PropertyValue $entry 'platformAndExampleProof').Value
            $distributionClass = (Get-PropertyValue $entry 'distributionClass').Value
            $homeSyncRequired = (Get-PropertyValue $entry 'homeSyncRequired').Value
            $reevaluationTrigger = (Get-PropertyValue $entry 'reevaluationTrigger').Value
            $risk = (Get-PropertyValue $entry 'risk').Value

            if (-not (Test-NonEmptyStringArray $audiences) -or
                -not (Test-NonEmptyStringArray $readerPaths)) {
                $errors.Add("ARCHITECTURE: '${id}' needs non-empty audiences and readerPaths arrays.")
            }
            if (-not (Test-RepoPath $canonicalSource) -or
                -not (Test-TextValue $navigationImpact) -or
                -not (Test-TextValue $documentClass) -or
                -not (Test-TextValue $languageStrategy) -or
                -not (Test-TextValue $platformProof) -or
                -not (Test-TextValue $reevaluationTrigger) -or
                -not (Test-TextValue $risk)) {
                $errors.Add("ARCHITECTURE: '${id}' lacks source, navigation, class, language, proof, risk, or re-evaluation evidence.")
            }
            if ($distributionClass -notin @('homeRuntime', 'sourceOnly', 'machineLocal')) {
                $errors.Add("DISTRIBUTION: '${id}' has an invalid distributionClass.")
            }
            if ($homeSyncRequired -isnot [bool]) {
                $errors.Add("DISTRIBUTION: '${id}' homeSyncRequired must be boolean.")
            }
            if ($languagePartners -isnot [System.Array]) {
                $errors.Add("LANGUAGE: '${id}' languagePartners must be an array.")
            } else {
                foreach ($partnerPath in @($languagePartners)) {
                    if (-not (Test-RepoPath $partnerPath)) {
                        $errors.Add("LANGUAGE: '${id}' contains a non-repository-relative language partner.")
                    }
                }
            }
        }

        if ($decision -eq 'UpdateRequired' -and @($documents).Count -eq 0) {
            $errors.Add("DECISION: '${id}' UpdateRequired needs at least one document.")
        }
        if ($decision -eq 'GeneratedUpdate') {
            $generatedSource = (Get-PropertyValue $entry 'generatedSource').Value
            $generatedPath = $(if ($null -eq $generatedSource) { $null } else { (Get-PropertyValue $generatedSource 'path').Value })
            $generatedRenderer = $(if ($null -eq $generatedSource) { $null } else { (Get-PropertyValue $generatedSource 'renderer').Value })
            if (@($documents).Count -eq 0 -or
                $null -eq $generatedSource -or
                -not (Test-RepoPath $generatedPath) -or
                -not (Test-TextValue $generatedRenderer)) {
                $errors.Add("GENERATED: '${id}' needs documents, source, and renderer.")
            }
        }
        if ($decision -eq 'FollowUp') {
            $validDate = [datetime]::MinValue
            $dueDate = (Get-PropertyValue $entry 'dueDate').Value
            $scopeReason = (Get-PropertyValue $entry 'scopeReason').Value
            $acceptedRiskEvidence = (Get-PropertyValue $entry 'acceptedRiskEvidence').Value
            $dateOk = [datetime]::TryParse([string]$dueDate, [ref]$validDate)
            if (-not $dateOk -or -not (Test-TextValue $reevaluationValue) -or
                -not (Test-TextValue $scopeReason) -or
                -not (Test-TextValue $riskValue) -or $riskValue -eq 'N/A') {
                $errors.Add("FOLLOWUP: '${id}' lacks risk, due date, trigger, or scope reason.")
            }
            if ($criticality -in @('Security', 'Usage', 'BreakingChange') -and
                -not (Test-RepoPath $acceptedRiskEvidence)) {
                $errors.Add("RISK: '${id}' cannot defer critical documentation without accepted-risk evidence.")
            }
        }
    }

    if ($errors.Count -gt 0) {
        $errors | ForEach-Object { Write-Error $_ -ErrorAction Continue }
        exit 1
    }
    Write-Host "PASS: Documentation Impact evidence is current ($(@($document.entries).Count) entries)."
    exit 0
} catch {
    Write-Error $_.Exception.Message
    exit 2
}
