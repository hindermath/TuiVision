#Requires -Version 7
<#
.SYNOPSIS
    Maintains WinGet packages for agentic development.

.DESCRIPTION
    Reads scripts/config/winget-apps-registry.json, updates WinGet metadata,
    upgrades installed packages, installs missing required packages, and reports
    drift between installed WinGet packages and the registry.

.PARAMETER Registry
    Alternative registry JSON path.

.PARAMETER VSCodeRegistry
    Alternative VS Code extensions registry JSON path.

.PARAMETER NpmAgentRegistry
    Alternative npm agent CLI registry JSON path.

.PARAMETER CompareOnly
    Only compare installed packages with the registry.

.PARAMETER SkipUpgrade
    Skip winget update/source update and winget upgrade --all.

.PARAMETER SkipVSCodeExtensions
    Skip VS Code extension install and comparison.

.PARAMETER IncludeOptional
    Also install optional registry entries.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -WhatIf
    pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -CompareOnly
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [string] $Registry = '',
    [string] $VSCodeRegistry = '',
    [string] $NpmAgentRegistry = '',
    [switch] $CompareOnly,
    [switch] $SkipUpgrade,
    [switch] $SkipVSCodeExtensions,
    [switch] $IncludeOptional
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
if (-not $Registry) {
    $Registry = Join-Path $repoRoot 'scripts/config/winget-apps-registry.json'
}
if (-not $VSCodeRegistry) {
    $VSCodeRegistry = Join-Path $repoRoot 'scripts/config/vscode-extensions-registry.json'
}
$cliRegistry = Join-Path $repoRoot 'scripts/config/required-cli-tools-registry.json'
if (-not $NpmAgentRegistry) {
    $NpmAgentRegistry = Join-Path $repoRoot 'scripts/config/npm-agent-cli-registry.json'
}

if (-not (Test-Path -Path $Registry -PathType Leaf)) {
    Write-Error "Registry nicht gefunden: $Registry"
}
if (-not $SkipVSCodeExtensions -and -not (Test-Path -Path $VSCodeRegistry -PathType Leaf)) {
    Write-Error "VS-Code-Extension-Registry nicht gefunden: $VSCodeRegistry"
}
if (-not (Test-Path -Path $cliRegistry -PathType Leaf)) {
    Write-Error "Required-CLI-Registry nicht gefunden: $cliRegistry"
}
if (-not (Test-Path -Path $NpmAgentRegistry -PathType Leaf)) {
    Write-Error "npm-Agent-CLI-Registry nicht gefunden: $NpmAgentRegistry"
}

if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
    Write-Error 'winget ist nicht installiert oder nicht im PATH.'
}

$registryData = Get-Content -Path $Registry -Raw | ConvertFrom-Json
$vscodeRegistryData = if ($SkipVSCodeExtensions) { $null } else { Get-Content -Path $VSCodeRegistry -Raw | ConvertFrom-Json }
$cliRegistryData = Get-Content -Path $cliRegistry -Raw | ConvertFrom-Json
$npmAgentRegistryData = Get-Content -Path $NpmAgentRegistry -Raw | ConvertFrom-Json
$installScope = if ($IncludeOptional) { @('required', 'optional') } else { @('required') }
$allRegistryIds = @($registryData.packages | ForEach-Object { $_.id } | Sort-Object -Unique)
$requiredRegistryIds = @(
    $registryData.packages |
        Where-Object { $_.scope -eq 'required' } |
        ForEach-Object { $_.id } |
        Sort-Object -Unique
)
$optionalRegistryIds = @(
    $registryData.packages |
        Where-Object { $_.scope -eq 'optional' } |
        ForEach-Object { $_.id } |
        Sort-Object -Unique
)
$installIds = @(
    $registryData.packages |
        Where-Object { $installScope -contains $_.scope } |
        ForEach-Object { $_.id } |
        Sort-Object -Unique
)
$allVSCodeExtensionIds = if ($SkipVSCodeExtensions) {
    @()
} else {
    @($vscodeRegistryData.extensions | ForEach-Object { $_.id } | Sort-Object -Unique)
}
$installVSCodeExtensionIds = if ($SkipVSCodeExtensions) {
    @()
} else {
    @(
        $vscodeRegistryData.extensions |
            Where-Object { $installScope -contains $_.scope } |
            ForEach-Object { $_.id } |
            Sort-Object -Unique
    )
}

function Invoke-HBWinget {
    param(
        [Parameter(Mandatory)][string[]] $Arguments,
        [Parameter(Mandatory)][string] $Action
    )

    $display = "winget $($Arguments -join ' ')"
    if ($PSCmdlet.ShouldProcess($display, $Action)) {
        & winget @Arguments
        return $LASTEXITCODE
    }

    Write-Host "WHATIF: $display"
    return 0
}

function Test-HBWingetSearchId {
    param([Parameter(Mandatory)][string] $Id)

    $output = & winget search --id $Id --exact --accept-source-agreements 2>$null
    if ($LASTEXITCODE -ne 0) { return $false }
    return (($output -join "`n") -match [regex]::Escape($Id))
}

function Test-HBWingetInstalledId {
    param([Parameter(Mandatory)][string] $Id)

    $output = & winget list --id $Id --exact --accept-source-agreements 2>$null
    if ($LASTEXITCODE -ne 0) { return $false }
    return (($output -join "`n") -match [regex]::Escape($Id))
}

function Get-HBWingetInstalledIds {
    $output = & winget list --accept-source-agreements 2>$null
    if ($LASTEXITCODE -ne 0) { return @() }

    $ids = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    foreach ($line in $output) {
        $matches = [regex]::Matches($line, '\s([A-Za-z0-9][A-Za-z0-9._-]+(?:\.[A-Za-z0-9][A-Za-z0-9._-]+)+)\s+([0-9][^\s]*)')
        foreach ($match in $matches) {
            [void]$ids.Add($match.Groups[1].Value)
        }
    }

    return @($ids | Sort-Object)
}

function Get-HBVSCodeCliCandidates {
    $candidates = [System.Collections.Generic.List[string]]::new()
    if ($env:VSCODE_CLI) {
        $candidates.Add($env:VSCODE_CLI)
    }

    foreach ($commandName in @('code.cmd', 'code')) {
        $command = Get-Command $commandName -ErrorAction SilentlyContinue
        if ($command) {
            $candidates.Add($command.Source)
        }
    }

    $knownPaths = @(
        (Join-Path $env:LOCALAPPDATA 'Programs/Microsoft VS Code/bin/code.cmd'),
        (Join-Path $env:ProgramFiles 'Microsoft VS Code/bin/code.cmd')
    )
    if (${env:ProgramFiles(x86)}) {
        $knownPaths += (Join-Path ${env:ProgramFiles(x86)} 'Microsoft VS Code/bin/code.cmd')
    }

    foreach ($path in $knownPaths) {
        $candidates.Add($path)
    }

    $seen = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    foreach ($candidate in $candidates) {
        if (-not $candidate) { continue }
        if (-not $seen.Add($candidate)) { continue }
        $candidate
    }
}

function Test-HBVSCodeCli {
    param([Parameter(Mandatory)][string] $Path)

    if (-not (Test-Path -Path $Path -PathType Leaf)) { return $false }

    & $Path --version *> $null
    if ($LASTEXITCODE -ne 0) { return $false }

    & $Path --list-extensions *> $null
    return ($LASTEXITCODE -eq 0)
}

function Get-HBVSCodeCli {
    foreach ($candidate in Get-HBVSCodeCliCandidates) {
        if (Test-HBVSCodeCli -Path $candidate) {
            return $candidate
        }
        Write-Warning "VS-Code-CLI nicht nutzbar: $candidate"
    }

    return $null
}

function Invoke-HBVSCode {
    param(
        [Parameter(Mandatory)][string] $Cli,
        [Parameter(Mandatory)][string[]] $Arguments,
        [Parameter(Mandatory)][string] $Action
    )

    $display = "$Cli $($Arguments -join ' ')"
    if ($PSCmdlet.ShouldProcess($display, $Action)) {
        & $Cli @Arguments
        return $LASTEXITCODE
    }

    Write-Host "WHATIF: $display"
    return 0
}

function Get-HBVSCodeInstalledExtensionIds {
    param([Parameter(Mandatory)][string] $Cli)

    $output = & $Cli --list-extensions 2>$null
    if ($LASTEXITCODE -ne 0) { return @() }
    return @($output | ForEach-Object { $_.ToLowerInvariant() } | Sort-Object -Unique)
}

function Install-HBVSCodeExtensions {
    if ($SkipVSCodeExtensions) { return }

    $codeCli = Get-HBVSCodeCli
    if (-not $codeCli) {
        if ($WhatIfPreference) {
            $codeCli = 'code'
            Write-Warning "Keine nutzbare VS-Code-CLI gefunden; WhatIf zeigt geplante Extension-Kommandos mit 'code'."
        } else {
            Write-Host 'SKIP vscode extensions: Keine nutzbare VS-Code-CLI gefunden. Vergleich meldet fehlende Extensions.'
            return
        }
    }

    $installed = if ($codeCli -eq 'code' -and -not (Get-Command code -ErrorAction SilentlyContinue)) {
        @()
    } else {
        @(Get-HBVSCodeInstalledExtensionIds -Cli $codeCli)
    }
    foreach ($id in $installVSCodeExtensionIds) {
        $idLower = $id.ToLowerInvariant()
        if ($installed -contains $idLower) {
            Write-Host "OK vscode extension: $id"
            continue
        }

        Write-Host "INSTALL vscode extension: $id"
        [void](Invoke-HBVSCode -Cli $codeCli -Arguments @('--install-extension', $id) -Action "Install VS Code extension $id")
    }
}

function Compare-HBVSCodeRegistry {
    if ($SkipVSCodeExtensions) { return }

    Write-Host "VS Code extension registry: $VSCodeRegistry"
    $codeCli = Get-HBVSCodeCli
    if (-not $codeCli) {
        Write-Host 'vscode_cli: unavailable'
        Write-Host 'missing_on_machine.vscode_extensions'
        $allVSCodeExtensionIds | ForEach-Object { Write-Host "  - $_" }
        return
    }

    $installed = @(Get-HBVSCodeInstalledExtensionIds -Cli $codeCli)
    $registry = @($allVSCodeExtensionIds | ForEach-Object { $_.ToLowerInvariant() } | Sort-Object -Unique)
    $missingOnMachine = @($registry | Where-Object { $installed -notcontains $_ })

    if ($missingOnMachine.Count -gt 0) {
        Write-Host 'missing_on_machine.vscode_extensions'
        $missingOnMachine | ForEach-Object { Write-Host "  - $_" }
    } else {
        Write-Host 'missing_on_machine.vscode_extensions: none'
    }

    $deprecatedInstalled = @(
        $vscodeRegistryData.deprecatedExtensions |
            Where-Object { $installed -contains $_.id.ToLowerInvariant() } |
            ForEach-Object {
                if ($_.replacement) { "$($_.id) -> $($_.replacement)" } else { $_.id }
            }
    )
    if ($deprecatedInstalled.Count -gt 0) {
        Write-Host 'deprecated_installed.vscode_extensions'
        $deprecatedInstalled | ForEach-Object { Write-Host "  - $_" }
    } else {
        Write-Host 'deprecated_installed.vscode_extensions: none'
    }
}

function Get-HBCLITools {
    param([Parameter(Mandatory)][string[]] $Scopes)

    return @(
        $cliRegistryData.tools |
            Where-Object {
                ($Scopes -contains $_.scope) -and
                (@($_.platforms) -contains 'Windows')
            }
    )
}

function Test-HBCommandWithTimeout {
    param(
        [Parameter(Mandatory)][string] $FilePath,
        [Parameter(Mandatory)][string[]] $Arguments,
        [int] $TimeoutMilliseconds = 5000
    )

    $startInfo = [System.Diagnostics.ProcessStartInfo]::new()
    $startInfo.FileName = $FilePath
    $startInfo.UseShellExecute = $false
    $startInfo.RedirectStandardOutput = $true
    $startInfo.RedirectStandardError = $true
    foreach ($argument in $Arguments) {
        [void]$startInfo.ArgumentList.Add($argument)
    }

    $process = [System.Diagnostics.Process]::new()
    $process.StartInfo = $startInfo
    try {
        [void]$process.Start()
        if (-not $process.WaitForExit($TimeoutMilliseconds)) {
            $process.Kill($true)
            return $false
        }
        return ($process.ExitCode -eq 0)
    } finally {
        $process.Dispose()
    }
}

function Test-HBCLITool {
    param([Parameter(Mandatory)] $Tool)

    $command = Get-Command $Tool.command -ErrorAction SilentlyContinue
    if (-not $command) { return $false }

    $arguments = @()
    if ($Tool.PSObject.Properties.Name -contains 'args') {
        $arguments = @($Tool.args | ForEach-Object { [string]$_ })
    }

    return Test-HBCommandWithTimeout -FilePath $command.Source -Arguments $arguments
}

function Invoke-HBExternal {
    param(
        [Parameter(Mandatory)][string] $Command,
        [Parameter(Mandatory)][string[]] $Arguments,
        [Parameter(Mandatory)][string] $Action
    )

    $display = "$Command $($Arguments -join ' ')"
    if ($PSCmdlet.ShouldProcess($display, $Action)) {
        & $Command @Arguments
        return $LASTEXITCODE
    }

    Write-Host "WHATIF: $display"
    return 0
}

function Get-HBNpmAgentTools {
    param([Parameter(Mandatory)][string[]] $Scopes)

    return @(
        $npmAgentRegistryData.tools |
            Where-Object {
                ($Scopes -contains $_.scope) -and
                (@($_.platforms) -contains 'Windows')
            }
    )
}

function Test-HBNpmAgentTool {
    param([Parameter(Mandatory)] $Tool)

    $command = Get-Command $Tool.command -ErrorAction SilentlyContinue
    if (-not $command) { return $false }

    $arguments = @()
    if ($Tool.PSObject.Properties.Name -contains 'args') {
        $arguments = @($Tool.args | ForEach-Object { [string]$_ })
    }

    return Test-HBCommandWithTimeout -FilePath $command.Source -Arguments $arguments
}

function Install-HBNpmAgentTools {
    foreach ($tool in Get-HBNpmAgentTools -Scopes $installScope) {
        if (Test-HBNpmAgentTool -Tool $tool) {
            Write-Host "OK npm agent cli: $($tool.id)"
            continue
        }

        if ($WhatIfPreference -or (Get-Command npm -ErrorAction SilentlyContinue)) {
            Write-Host "INSTALL npm agent cli: $($tool.id) ($($tool.package))"
            [void](Invoke-HBExternal -Command 'npm' -Arguments @('install', '-g', [string]$tool.package) -Action "Install npm agent CLI $($tool.id)")
        } else {
            Write-Host "MISSING npm agent cli: $($tool.id) (npm fehlt)"
        }
    }
}

function Compare-HBNpmAgentScope {
    param(
        [Parameter(Mandatory)][string] $Scope,
        [Parameter(Mandatory)][string] $Label
    )

    $missing = @(
        Get-HBNpmAgentTools -Scopes @($Scope) |
            Where-Object { -not (Test-HBNpmAgentTool -Tool $_) } |
            ForEach-Object { $_.id }
    )

    if ($missing.Count -gt 0) {
        Write-Host $Label
        $missing | ForEach-Object { Write-Host "  - $_" }
    } else {
        Write-Host "${Label}: none"
    }
}

function Compare-HBNpmAgentRegistry {
    Write-Host "npm agent CLI registry: $NpmAgentRegistry"
    Compare-HBNpmAgentScope -Scope 'required' -Label 'missing_on_machine.required.npm_agent_cli_tools'
    Compare-HBNpmAgentScope -Scope 'optional' -Label 'missing_on_machine.optional.npm_agent_cli_tools'
}

function Install-HBCLITool {
    param([Parameter(Mandatory)] $Tool)

    $install = if ($Tool.PSObject.Properties.Name -contains 'install') { $Tool.install } else { $null }
    if ($install -and $install.manager -eq 'uv') {
        $arguments = @($install.arguments | ForEach-Object { [string]$_ })
        Write-Host "INSTALL cli tool: $($Tool.id)"
        if ($WhatIfPreference -or (Get-Command uv -ErrorAction SilentlyContinue)) {
            [void](Invoke-HBExternal -Command 'uv' -Arguments $arguments -Action "Install CLI tool $($Tool.id)")
        } else {
            Write-Host "SKIP cli tool install: $($Tool.id) (uv fehlt)"
        }
        return
    }

    Write-Host "MISSING cli tool: $($Tool.id)"
}

function Install-HBCLITools {
    foreach ($tool in Get-HBCLITools -Scopes $installScope) {
        if (Test-HBCLITool -Tool $tool) {
            Write-Host "OK cli tool: $($tool.id)"
            continue
        }

        Install-HBCLITool -Tool $tool
    }
}

function Compare-HBCLIScope {
    param(
        [Parameter(Mandatory)][string] $Scope,
        [Parameter(Mandatory)][string] $Label
    )

    $missing = @(
        Get-HBCLITools -Scopes @($Scope) |
            Where-Object { -not (Test-HBCLITool -Tool $_) } |
            ForEach-Object { $_.id }
    )

    if ($missing.Count -gt 0) {
        Write-Host $Label
        $missing | ForEach-Object { Write-Host "  - $_" }
    } else {
        Write-Host "${Label}: none"
    }
}

function Compare-HBCLIRegistry {
    Write-Host "Required CLI tool registry: $cliRegistry"
    Compare-HBCLIScope -Scope 'required' -Label 'missing_on_machine.required.cli_tools'
    Compare-HBCLIScope -Scope 'optional' -Label 'missing_on_machine.optional.cli_tools'
}

function Compare-HBPackageScope {
    param(
        [Parameter(Mandatory)][string[]] $RegistryIds,
        [Parameter(Mandatory)][string[]] $InstalledIds,
        [Parameter(Mandatory)][string] $Label
    )

    $missing = @($RegistryIds | Where-Object { $InstalledIds -notcontains $_ })
    if ($missing.Count -gt 0) {
        Write-Host $Label
        $missing | ForEach-Object { Write-Host "  - $_" }
    } else {
        Write-Host "${Label}: none"
    }
}

Write-Host 'Agentic WinGet registry maintenance'
Write-Host "Registry: $Registry"

if (-not $CompareOnly -and -not $SkipUpgrade) {
    $updateStatus = Invoke-HBWinget -Arguments @('update') -Action 'WinGet package metadata update'
    if ($updateStatus -ne 0) {
        Write-Warning 'winget update ist nicht verfuegbar oder fehlgeschlagen; nutze winget source update als Fallback.'
        [void](Invoke-HBWinget -Arguments @('source', 'update') -Action 'WinGet source update')
    }
    [void](Invoke-HBWinget -Arguments @('upgrade', '--all', '--accept-package-agreements', '--accept-source-agreements') -Action 'WinGet package upgrade')
}

if (-not $CompareOnly) {
    foreach ($id in $installIds) {
        if (Test-HBWingetInstalledId -Id $id) {
            Write-Host "OK package: $id"
            continue
        }

        if (-not (Test-HBWingetSearchId -Id $id)) {
            Write-Error "WinGet-ID nicht gefunden: $id"
        }

        Write-Host "INSTALL package: $id"
        [void](Invoke-HBWinget -Arguments @('install', '--id', $id, '--exact', '--accept-package-agreements', '--accept-source-agreements') -Action "Install $id")
    }

    Install-HBVSCodeExtensions
    Install-HBCLITools
    Install-HBNpmAgentTools
}

$installedIds = @(Get-HBWingetInstalledIds)
$missingFromRegistry = @($installedIds | Where-Object { $allRegistryIds -notcontains $_ })

Compare-HBPackageScope -RegistryIds $requiredRegistryIds -InstalledIds $installedIds -Label 'missing_on_machine.required.packages'
Compare-HBPackageScope -RegistryIds $optionalRegistryIds -InstalledIds $installedIds -Label 'missing_on_machine.optional.packages'

if ($missingFromRegistry.Count -gt 0) {
    Write-Host 'missing_from_registry.packages'
    $missingFromRegistry | ForEach-Object { Write-Host "  - $_" }
} else {
    Write-Host 'missing_from_registry.packages: none'
}

Compare-HBVSCodeRegistry
Compare-HBCLIRegistry
Compare-HBNpmAgentRegistry
