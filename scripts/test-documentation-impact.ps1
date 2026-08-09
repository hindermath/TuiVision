#Requires -Version 7
<#
.SYNOPSIS
Runs deterministic Documentation Impact contract fixtures.

.EXAMPLE
pwsh -NoProfile -File scripts/test-documentation-impact.ps1
#>
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
$validator = Join-Path $PSScriptRoot 'validate-documentation-impact.ps1'
$fixtures = Join-Path $repo 'scripts/tests/documentation-impact/fixtures'

$cases = @(
    @{ Name = 'valid.json'; Expected = 0; Marker = '' },
    @{ Name = 'legacy-valid.json'; Expected = 0; Marker = '' },
    @{ Name = 'missing-architecture-fields.json'; Expected = 1; Marker = 'ARCHITECTURE' },
    @{ Name = 'invalid-distribution-class.json'; Expected = 1; Marker = 'DISTRIBUTION' },
    @{ Name = 'invalid-home-sync-type.json'; Expected = 1; Marker = 'DISTRIBUTION' },
    @{ Name = 'invalid-language-partners-type.json'; Expected = 1; Marker = 'LANGUAGE' },
    @{ Name = 'missing-decision.json'; Expected = 1; Marker = 'DECISION' },
    @{ Name = 'duplicate-id.json'; Expected = 1; Marker = 'DUPLICATE' },
    @{ Name = 'invalid-followup.json'; Expected = 1; Marker = 'FOLLOWUP' },
    @{ Name = 'unsafe-defer.json'; Expected = 1; Marker = 'RISK' }
)

foreach ($case in $cases) {
    $output = & pwsh -NoProfile -File $validator -Evidence (Join-Path $fixtures $case.Name) 2>&1
    $actual = $LASTEXITCODE
    if ($actual -ne $case.Expected) {
        throw "Fixture $($case.Name) expected $($case.Expected), got ${actual}."
    }
    if ($case.Marker -and (($output | Out-String) -notmatch [regex]::Escape($case.Marker))) {
        throw "Fixture $($case.Name) did not report expected class $($case.Marker)."
    }
}

Write-Host "PASS: Documentation Impact fixtures ($($cases.Count) cases)."
