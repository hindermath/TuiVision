#Requires -Version 7
<#
.SYNOPSIS
    Pflegt erforderliche PowerShell-Module. / Maintains required PowerShell modules.

.DESCRIPTION
    Liest die zentrale Modul-Registry, prueft exakte Modulversionen und installiert
    fehlende Module im CurrentUser-Bereich. Install-PSResource wird bevorzugt;
    Install-Module bleibt ein Kompatibilitaets-Fallback.

    Reads the central module registry, checks exact module versions, and installs
    missing modules in CurrentUser scope. Install-PSResource is preferred;
    Install-Module remains a compatibility fallback.

.PARAMETER Registry
    Alternative Registry-Datei. / Alternative registry file.

.PARAMETER CompareOnly
    Nur vergleichen, nicht installieren. / Compare only; do not install.

.PARAMETER IncludeOptional
    Auch optionale Module installieren. / Also install optional modules.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 -CompareOnly

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 -WhatIf
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [string] $Registry = '',
    [switch] $CompareOnly,
    [switch] $IncludeOptional
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
if (-not $Registry) {
    $Registry = Join-Path $repoRoot 'scripts/config/powershell-modules-registry.json'
}
if (-not (Test-Path -LiteralPath $Registry -PathType Leaf)) {
    throw "PowerShell-Modul-Registry nicht gefunden / not found: ${Registry}"
}

$platform = if ($IsWindows) {
    'Windows'
} elseif ($IsMacOS) {
    'Darwin'
} elseif ($IsLinux) {
    'Linux'
} else {
    throw 'Nicht unterstuetzte Plattform / Unsupported platform.'
}

$registryData = Get-Content -LiteralPath $Registry -Raw | ConvertFrom-Json
if ($registryData.schemaVersion -ne 1 -or -not $registryData.modules) {
    throw 'Nicht unterstuetztes Registry-Schema / Unsupported registry schema.'
}

$scopes = if ($IncludeOptional) { @('required', 'optional') } else { @('required') }
$selectedModules = @(
    $registryData.modules |
        Where-Object {
            ($scopes -contains [string]$_.scope) -and
            (@($_.platforms) -contains $platform)
        }
)

function Test-HBPowerShellModuleVersion {
    param(
        [Parameter(Mandatory)][string] $Name,
        [Parameter(Mandatory)][version] $Version
    )

    return [bool](
        Get-Module -ListAvailable -Name $Name |
            Where-Object { $_.Version -eq $Version } |
            Select-Object -First 1
    )
}

function Install-HBPowerShellModule {
    param(
        [Parameter(Mandatory)][string] $Name,
        [Parameter(Mandatory)][string] $Version,
        [Parameter(Mandatory)][string] $RepositoryName
    )

    $target = "${Name} ${Version} (CurrentUser)"
    if (-not $PSCmdlet.ShouldProcess($target, 'Install PowerShell module')) {
        Write-Host "WHATIF: Install-PSResource -Name $Name -Version $Version -Repository $RepositoryName -Scope CurrentUser"
        return
    }

    if (Get-Command Install-PSResource -ErrorAction SilentlyContinue) {
        Install-PSResource -Name $Name -Version $Version -Repository $RepositoryName `
            -Scope CurrentUser -TrustRepository -Quiet -AcceptLicense
        return
    }

    if (Get-Command Install-Module -ErrorAction SilentlyContinue) {
        Install-Module -Name $Name -RequiredVersion $Version -Repository $RepositoryName `
            -Scope CurrentUser -Force -AllowClobber -AcceptLicense
        return
    }

    throw 'Weder Install-PSResource noch Install-Module ist verfuegbar / Neither installer is available.'
}

Write-Host 'PowerShell module registry maintenance'
Write-Host "Registry: $Registry"
Write-Host "Platform: $platform"

foreach ($module in $selectedModules) {
    $name = [string]$module.name
    $version = [version][string]$module.version
    if (Test-HBPowerShellModuleVersion -Name $name -Version $version) {
        Write-Host "OK powershell module: ${name} ${version}"
        continue
    }

    if ($CompareOnly) {
        Write-Host "MISSING powershell module: ${name} ${version}"
        continue
    }

    Write-Host "INSTALL powershell module: ${name} ${version}"
    Install-HBPowerShellModule -Name $name -Version $version -RepositoryName ([string]$registryData.repository)
    if (-not $WhatIfPreference -and -not (Test-HBPowerShellModuleVersion -Name $name -Version $version)) {
        throw "PowerShell-Modul konnte nicht verifiziert werden / module verification failed: ${name} ${version}"
    }
}

foreach ($scope in @('required', 'optional')) {
    $missing = @(
        $registryData.modules |
            Where-Object {
                ([string]$_.scope -eq $scope) -and
                (@($_.platforms) -contains $platform) -and
                (-not (Test-HBPowerShellModuleVersion -Name ([string]$_.name) -Version ([version][string]$_.version)))
            } |
            ForEach-Object { "$($_.name) $($_.version)" }
    )
    $label = "missing_on_machine.${scope}.powershell_modules"
    if ($missing.Count -eq 0) {
        Write-Host "${label}: none"
    } else {
        Write-Host $label
        $missing | ForEach-Object { Write-Host "  - $_" }
    }
}
