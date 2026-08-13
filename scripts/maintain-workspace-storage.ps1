#Requires -Version 7
<#
.SYNOPSIS
    Inventarisiert und bereinigt sicheren Workspace-Speicher.

    Inventories and safely reclaims workspace storage.

.DESCRIPTION
    Verwendet den gemeinsamen Storage-Vertrag für registrierte Level-2-
    Repositories. Safe schützt Git-Dateien, Symlinks, laufende Builds,
    Container-Volumes sowie cc65-Sample- und Targettest-Nachweise. Non-MSL-
    Repositories benötigen einen kuratierten Adapter und eine dokumentierte
    Begründung.

    Uses the shared storage contract for registered Level-2 repositories.
    Safe protects Git files, symlinks, running builds, container volumes, and
    cc65 sample/target-test evidence. Non-MSL repositories require a curated
    adapter and a documented justification.

.PARAMETER CheckOnly
    Nur inventarisieren. Es werden keine Build-Ausgaben oder Caches geändert.

    Inventory only. No build output or cache is changed.

.PARAMETER CleanupProfile
    Safe ist der Standard. Deep leert zusätzlich wiederherstellbare
    Dependency-Caches und benötigt bei einem echten Lauf ConfirmDeepCleanup.
    None deaktiviert die Inventur und Bereinigung.

    Safe is the default. Deep also clears recoverable dependency caches and
    requires ConfirmDeepCleanup for an update run. None disables inventory and
    cleanup.

.PARAMETER ConfirmDeepCleanup
    Bestätigt die zusätzliche Deep-Löschung in einem echten Lauf. Dieser
    Schalter ist für CheckOnly oder WhatIf nicht erforderlich.

    Confirms additional deep deletion in an update run. The switch is not
    required for CheckOnly or WhatIf.

.PARAMETER HomeDir
    Alternatives Home-Verzeichnis für Tests oder ein zweites Profil.

    Alternative home directory for tests or a second profile.

.PARAMETER RegistryPath
    Alternatives Level-2-Repository-Register.

    Alternative Level-2 repository registry.

.PARAMETER PolicyPath
    Alternative deklarative Storage-Policy.

    Alternative declarative storage policy.

.PARAMETER ResultFile
    Pfad des privaten atomaren JSON-Berichts.

    Path of the private atomic JSON report.

.PARAMETER RunId
    UUID zur Korrelation mit dem Ein-Kommando-Orchestrator.

    UUID correlating the run with the one-command orchestrator.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 -CheckOnly

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 -WhatIf

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-workspace-storage.ps1 -CleanupProfile Deep -ConfirmDeepCleanup

.NOTES
    Exitcode 0 umfasst Provider-Warnungen, die vollständig im JSON-Bericht
    stehen. Exitcode 2 bedeutet Aufruf-, Policy-, Pfad- oder Betriebsfehler.

    Exit code 0 includes provider warnings captured in the JSON report. Exit
    code 2 means a usage, policy, path, or operational failure.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [switch] $CheckOnly,
    [ValidateSet('Safe', 'Deep', 'None')][string] $CleanupProfile = 'Safe',
    [switch] $ConfirmDeepCleanup,
    [string] $HomeDir = [Environment]::GetFolderPath('UserProfile'),
    [string] $RegistryPath,
    [string] $PolicyPath,
    [string] $ResultFile,
    [string] $RunId
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Resolve-HBStoragePythonLauncher {
    [CmdletBinding()]
    param()

    foreach ($candidate in @('python3', 'python')) {
        $command = Get-Command $candidate -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($null -eq $command) { continue }
        & $command.Source -c 'import sys; raise SystemExit(0 if sys.version_info >= (3, 9) else 2)'
        if ($LASTEXITCODE -eq 0) {
            return @($command.Source)
        }
    }
    $py = Get-Command py -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($null -ne $py) {
        & $py.Source -3 -c 'import sys; raise SystemExit(0 if sys.version_info >= (3, 9) else 2)'
        if ($LASTEXITCODE -eq 0) {
            return @($py.Source, '-3')
        }
    }
    throw 'Python 3.9 oder neuer fehlt / Python 3.9 or later is missing.'
}

$engine = Join-Path $PSScriptRoot 'lib/workspace_storage_maintenance.py'
if (-not $PolicyPath) {
    $PolicyPath = Join-Path $PSScriptRoot 'config/workspace-storage-maintenance.json'
}
if (-not (Test-Path -LiteralPath $HomeDir -PathType Container)) {
    throw "Home-Verzeichnis fehlt / home directory is missing: ${HomeDir}"
}
$HomeDir = (Resolve-Path -LiteralPath $HomeDir).Path
if (-not $RegistryPath) {
    $RegistryPath = Join-Path $HomeDir '.home-baseline/level2-repository-registry.json'
}
if (-not (Test-Path -LiteralPath $RegistryPath -PathType Leaf)) {
    throw "Level-2-Register fehlt / registry is missing: ${RegistryPath}"
}
if (-not (Test-Path -LiteralPath $PolicyPath -PathType Leaf)) {
    throw "Storage-Policy fehlt / policy is missing: ${PolicyPath}"
}
if (-not (Test-Path -LiteralPath $engine -PathType Leaf)) {
    throw "Storage-Engine fehlt / engine is missing: ${engine}"
}

if (-not $RunId) { $RunId = [Guid]::NewGuid().ToString() }
try { $RunId = ([Guid]$RunId).ToString() } catch {
    throw 'Run-ID ist keine gültige UUID / run ID is not a valid UUID.'
}
if (-not $ResultFile) {
    $ResultFile = Join-Path $HomeDir ".home-baseline/reports/workspace-storage-${RunId}.json"
}

$mode = if ($CheckOnly) { 'check-only' } elseif ($WhatIfPreference) { 'dry-run' } else { 'update' }
$python = @(Resolve-HBStoragePythonLauncher)
$arguments = @(
    $engine,
    '--home-dir', $HomeDir,
    '--registry', $RegistryPath,
    '--policy', $PolicyPath,
    '--report', $ResultFile,
    '--mode', $mode,
    '--profile', $CleanupProfile.ToLowerInvariant(),
    '--run-id', $RunId
)
if ($ConfirmDeepCleanup) { $arguments += '--confirm-deep-cleanup' }

$launcher = $python[0]
$launcherArguments = @()
if ($python.Count -gt 1) {
    $launcherArguments = @($python[1..($python.Count - 1)])
}
& $launcher @launcherArguments @arguments
$status = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }
if (Test-Path -LiteralPath $ResultFile -PathType Leaf) {
    if ($IsWindows) {
        $identity = [Security.Principal.WindowsIdentity]::GetCurrent().Name
        & icacls $ResultFile /inheritance:r /grant:r "${identity}:(F)" | Out-Null
        if ($LASTEXITCODE -ne 0) {
            throw 'Storage-Bericht konnte nicht auf den aktuellen Benutzer begrenzt werden / report ACL could not be restricted.'
        }
    } else {
        & chmod 600 $ResultFile
    }
}
Write-Host "Storage-Bericht / storage report: ${ResultFile}"
exit $status
