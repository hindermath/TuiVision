<#
.SYNOPSIS
Validiert ein Intake-Serien-Receipt und dessen Manifest ohne Schreibzugriff.

.DESCRIPTION
Prueft Receipt-Identitaet, Authority, Hashbindung, Archive und Tombstone sowie
das gebundene Manifest. Validates receipt identity, authority, hash binding,
archives, tombstones, and the bound manifest without modifying files.

.PARAMETER File
Pfad zum Receipt. Path to the receipt.

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
    '--kind', 'receipt',
    '--file', $File,
    '--repo', $Repo
)
if ($Json) { $Arguments += '--json' }
& python3 @Arguments
exit $LASTEXITCODE
