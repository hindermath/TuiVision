<#
.SYNOPSIS
Validiert ein Intake-Serienmanifest ohne Schreibzugriff.

.DESCRIPTION
Prueft UTF-8, Pfade, Hashes, Lifecycle, Roots und typisierte Kanten mit dem
portablen Validator. Validates UTF-8, paths, hashes, lifecycle, roots, and
typed edges without modifying files.

.PARAMETER File
Pfad zum Manifest. Path to the manifest.

.PARAMETER Repo
Repository-Wurzel. Repository root.

.PARAMETER Json
Gibt eine maschinenlesbare Zusammenfassung aus. Emits a JSON summary.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$File,
    [string]$Repo = '.',
    [switch]$Json
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$Arguments = @(
    (Join-Path $PSScriptRoot 'validate-intake-series.py'),
    '--kind', 'manifest',
    '--file', $File,
    '--repo', $Repo
)
if ($Json) { $Arguments += '--json' }
& python3 @Arguments
exit $LASTEXITCODE
