<#
.SYNOPSIS
Prueft die TuiVision-Quellenreferenz-Policy read-only.

Validates the TuiVision source-reference policy read-only.
#>
[CmdletBinding()]
param(
    [string]$Policy = 'requirements/source-reference-policy.json',
    [string]$Repo = '.',
    [switch]$SkipSurfaceChecks,
    [string]$SelfTest = '',
    [switch]$Json
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$arguments = @(
    (Join-Path $PSScriptRoot 'validate-source-reference-policy.py'),
    '--policy', $Policy,
    '--repo', $Repo
)
if ($SkipSurfaceChecks) { $arguments += '--skip-surface-checks' }
if ($SelfTest) { $arguments += @('--self-test', $SelfTest) }
if ($Json) { $arguments += '--json' }

& python3 @arguments
exit $LASTEXITCODE
