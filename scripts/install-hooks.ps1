#Requires -Version 7
<#
.SYNOPSIS
    Installiert die Git-Hooks aus scripts/hooks/ in .git/hooks/.
.DESCRIPTION
    Auf jedem neuen Gerät nach dem Clonen einmalig ausführen:
        pwsh scripts/install-hooks.ps1
    Mit -Verbose für detaillierte Ausgabe:
        pwsh scripts/install-hooks.ps1 -Verbose
.PARAMETER HookSourcePath
    Optionaler alternativer Pfad zum Hooks-Verzeichnis. Standard: scripts/hooks/
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [string] $HookSourcePath = ''
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$hooksSrc = if ($HookSourcePath) { $HookSourcePath } else { Join-Path $PSScriptRoot 'hooks' }
$hooksDst = Join-Path $repoRoot '.git' 'hooks'

if (-not (Test-Path -Path $hooksDst -PathType Container)) {
    Write-Error "Verzeichnis '$hooksDst' nicht gefunden. Ist dies ein Git-Repository?"
}

$hooks = Get-ChildItem -Path $hooksSrc -File
if (-not $hooks) {
    Write-Warning "Keine Hook-Dateien in '$hooksSrc' gefunden."
    return
}

$installed = 0
foreach ($hook in $hooks) {
    $target = Join-Path $hooksDst $hook.Name
    if ($PSCmdlet.ShouldProcess($target, 'Hook installieren')) {
        Copy-Item -Path $hook.FullName -Destination $target -Force
        if ($IsLinux -or $IsMacOS) {
            & chmod +x $target
        }
        Write-Verbose "Kopiert: $($hook.FullName) -> $target"
        Write-Host "OK  $($hook.Name)" -ForegroundColor Green
        $installed++
    }
}

Write-Host ""
Write-Host "$installed Hook(s) erfolgreich installiert." -ForegroundColor Cyan
