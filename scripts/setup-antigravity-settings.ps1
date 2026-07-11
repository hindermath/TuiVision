#Requires -Version 7
<#
.SYNOPSIS
    Richtet die gehaertete Antigravity-CLI-Konfiguration ein.
    Applies the hardened Antigravity CLI configuration.
.DESCRIPTION
    Aktualisiert settings.json strukturiert, erhaelt unbekannte Einstellungen
    und installiert eine ASCII-only-Statuszeile ohne Identitaetsdaten.

    Updates settings.json structurally, preserves unknown settings, and
    installs an ASCII-only status line that does not expose identity data.
.PARAMETER CheckOnly
    Prueft nur auf Abweichungen und liefert bei Drift Exit-Code 2.
    Checks for drift only and exits with code 2 when drift is found.
.EXAMPLE
    pwsh -NoProfile -File scripts/setup-antigravity-settings.ps1 -WhatIf
.EXAMPLE
    pwsh -NoProfile -File scripts/setup-antigravity-settings.ps1 -CheckOnly
#>
[CmdletBinding(SupportsShouldProcess)]
param([switch] $CheckOnly)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-HBHomeDirectory {
    if ($env:HOME) { return $env:HOME }
    return $env:USERPROFILE
}

function Set-HBJsonProperty {
    param(
        [Parameter(Mandatory)][psobject] $Object,
        [Parameter(Mandatory)][string] $Name,
        [Parameter(Mandatory)][AllowNull()] $Value
    )
    $Object | Add-Member -NotePropertyName $Name -NotePropertyValue $Value -Force
}

$settingsDir = if ($env:ANTIGRAVITY_HOME) { $env:ANTIGRAVITY_HOME } else { Join-Path (Get-HBHomeDirectory) '.gemini/antigravity-cli' }
$settingsFile = Join-Path $settingsDir 'settings.json'
$statuslineFile = Join-Path $settingsDir 'statusline.ps1'
$templateFile = Join-Path $PSScriptRoot 'templates/antigravity-statusline.ps1'

if (-not (Test-Path $templateFile -PathType Leaf)) {
    throw "Fehler / Error: template not found: $templateFile"
}

$settings = if (Test-Path $settingsFile -PathType Leaf) {
    try { Get-Content $settingsFile -Raw | ConvertFrom-Json }
    catch { throw ('Fehler / Error: invalid settings JSON in {0}: {1}' -f $settingsFile, $_.Exception.Message) }
} else {
    [pscustomobject]@{}
}

$command = 'pwsh -NoProfile -File "{0}"' -f $statuslineFile
$expected = [ordered]@{
    toolPermission = 'strict'
    artifactReviewPolicy = 'asks-for-review'
    enableTerminalSandbox = $true
    allowNonWorkspaceAccess = $false
    enableTelemetry = $false
}

if ($CheckOnly) {
    $drift = [System.Collections.Generic.List[string]]::new()
    foreach ($entry in $expected.GetEnumerator()) {
        $property = $settings.PSObject.Properties[$entry.Key]
        if (-not $property -or $property.Value -ne $entry.Value) { $drift.Add($entry.Key) }
    }
    $status = $settings.PSObject.Properties['statusLine']
    if (-not $status -or $status.Value.type -ne 'command' -or $status.Value.command -ne $command) { $drift.Add('statusLine') }
    if (-not (Test-Path $statuslineFile -PathType Leaf)) { $drift.Add('statusline helper') }
    if ($drift.Count -gt 0) {
        Write-Host "DRIFT Antigravity settings: $($drift -join ', ')"
        exit 2
    }
    Write-Host 'OK Antigravity settings hardened'
    return
}

foreach ($entry in $expected.GetEnumerator()) {
    Set-HBJsonProperty -Object $settings -Name $entry.Key -Value $entry.Value
}
Set-HBJsonProperty -Object $settings -Name 'statusLine' -Value ([pscustomobject]@{ type = 'command'; command = $command })

if ($PSCmdlet.ShouldProcess($settingsFile, 'Antigravity settings haerten / Harden Antigravity settings')) {
    New-Item -ItemType Directory -Path $settingsDir -Force | Out-Null
    Copy-Item -Path $templateFile -Destination $statuslineFile -Force
    $settings | ConvertTo-Json -Depth 20 | Set-Content -Path $settingsFile -Encoding UTF8
    Write-Host "OK Antigravity settings hardened: $settingsFile"
}
