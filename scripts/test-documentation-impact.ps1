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
$fixtures = Join-Path $repo 'specs/012-documentation-impact-governance/tests/fixtures'

$cases = @(
    @{ Name = 'valid.json'; Expected = 0 },
    @{ Name = 'missing-decision.json'; Expected = 1 },
    @{ Name = 'duplicate-id.json'; Expected = 1 },
    @{ Name = 'invalid-followup.json'; Expected = 1 },
    @{ Name = 'unsafe-defer.json'; Expected = 1 }
)

foreach ($case in $cases) {
    & pwsh -NoProfile -File $validator -Evidence (Join-Path $fixtures $case.Name) *> $null
    $actual = $LASTEXITCODE
    if ($actual -ne $case.Expected) {
        throw "Fixture $($case.Name) expected $($case.Expected), got ${actual}."
    }
}

Write-Host "PASS: Documentation Impact fixtures ($($cases.Count) cases)."
