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

try {
    $resolved = (Resolve-Path -LiteralPath $Evidence -ErrorAction Stop).Path
    $document = Get-Content -LiteralPath $resolved -Raw -Encoding utf8 |
        ConvertFrom-Json -Depth 32
    $errors = [System.Collections.Generic.List[string]]::new()
    $decisions = @('UpdateRequired', 'NoUpdateRequired', 'GeneratedUpdate', 'FollowUp')
    $criticalities = @('Normal', 'Security', 'Usage', 'BreakingChange')

    if ($document.schemaVersion -ne '1.0') {
        $errors.Add('SCHEMA: schemaVersion must equal 1.0.')
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
        if ($entry.decision -notin $decisions) {
            $errors.Add("DECISION: '${id}' has an unknown or missing decision.")
            continue
        }
        if ($entry.criticality -notin $criticalities) {
            $errors.Add("CRITICALITY: '${id}' has an unknown or missing criticality.")
        }
        foreach ($path in @($entry.documents)) {
            if (-not (Test-RepoPath $path)) {
                $errors.Add("PATH: '${id}' contains a non-repository-relative document path.")
            }
        }

        if ($entry.decision -eq 'UpdateRequired' -and @($entry.documents).Count -eq 0) {
            $errors.Add("DECISION: '${id}' UpdateRequired needs at least one document.")
        }
        if ($entry.decision -eq 'GeneratedUpdate') {
            if (@($entry.documents).Count -eq 0 -or
                $null -eq $entry.generatedSource -or
                -not (Test-RepoPath $entry.generatedSource.path) -or
                -not (Test-TextValue $entry.generatedSource.renderer)) {
                $errors.Add("GENERATED: '${id}' needs documents, source, and renderer.")
            }
        }
        if ($entry.decision -eq 'FollowUp') {
            $validDate = [datetime]::MinValue
            $dateOk = [datetime]::TryParse([string]$entry.dueDate, [ref]$validDate)
            if (-not $dateOk -or -not (Test-TextValue $entry.reevaluationTrigger) -or
                -not (Test-TextValue $entry.scopeReason) -or
                -not (Test-TextValue $entry.risk) -or $entry.risk -eq 'N/A') {
                $errors.Add("FOLLOWUP: '${id}' lacks risk, due date, trigger, or scope reason.")
            }
            if ($entry.criticality -in @('Security', 'Usage', 'BreakingChange') -and
                -not (Test-RepoPath $entry.acceptedRiskEvidence)) {
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
