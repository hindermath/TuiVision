#Requires -Version 7
<#
.SYNOPSIS
    Maintains WinGet packages for agentic development.

.DESCRIPTION
    Reads scripts/config/winget-apps-registry.json, updates WinGet metadata,
    upgrades installed packages, installs missing required packages, and reports
    drift between installed WinGet packages and the registry. Every WinGet
    subprocess has a hard timeout and complete process-tree cleanup. Upgrade or
    install work without current administrator-prompt authority is not started
    and exits as DEFERRED_ADMIN_REQUIRED. UAC is never bypassed.

.PARAMETER Registry
    Alternative registry JSON path.

.PARAMETER VSCodeRegistry
    Alternative VS Code extensions registry JSON path.

.PARAMETER NpmAgentRegistry
    Alternative npm agent CLI registry JSON path.

.PARAMETER PowerShellModuleRegistry
    Alternative PowerShell module registry JSON path.

.PARAMETER CompareOnly
    Only compare installed packages with the registry.

.PARAMETER SkipUpgrade
    Skip winget update/source update and winget upgrade --all.

.PARAMETER SkipVSCodeExtensions
    Skip VS Code extension install and comparison.

.PARAMETER ProcessTimeoutSeconds
    Harte Zeitgrenze fuer jeden WinGet-Unterprozess. Leseproben verwenden
    hoechstens 30 Sekunden.

    Hard timeout for each WinGet subprocess. Read probes use at most 30 seconds.

.PARAMETER ProcessCleanupSeconds
    Begrenzte Frist zum Beenden und Abwarten des gesamten Prozessbaums.

    Bounded interval for terminating and awaiting a timed-out process tree.

.PARAMETER IncludeOptional
    Also install optional registry entries.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -WhatIf
    pwsh -NoProfile -File scripts/maintain-agentic-winget-apps.ps1 -CompareOnly

.NOTES
    Exit codes: 0 = current/success, 2 = operational or contradictory package
    status, 75 = DEFERRED_ADMIN_REQUIRED. Package detection and summary reduce
    observations to exactly one final status per canonical package ID.
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [string] $Registry = '',
    [string] $VSCodeRegistry = '',
    [string] $NpmAgentRegistry = '',
    [string] $PowerShellModuleRegistry = '',
    [switch] $CompareOnly,
    [switch] $SkipUpgrade,
    [switch] $SkipVSCodeExtensions,
    [switch] $IncludeOptional,
    [ValidateRange(5, 86400)][int] $ProcessTimeoutSeconds = 1800,
    [ValidateRange(1, 60)][int] $ProcessCleanupSeconds = 10
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$hardeningModule = Join-Path $repoRoot 'scripts/lib/windows-maintenance-hardening.psm1'
if (-not (Test-Path -LiteralPath $hardeningModule -PathType Leaf)) {
    Write-Error "Windows-Wartungsmodul fehlt / Windows maintenance module missing: ${hardeningModule}"
}
Import-Module $hardeningModule -Force
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
if (-not $PowerShellModuleRegistry) {
    $PowerShellModuleRegistry = Join-Path $repoRoot 'scripts/config/powershell-modules-registry.json'
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
if (-not (Test-Path -Path $PowerShellModuleRegistry -PathType Leaf)) {
    Write-Error "PowerShell-Modul-Registry nicht gefunden: $PowerShellModuleRegistry"
}

$wingetCommand = Get-Command winget -ErrorAction SilentlyContinue
if (-not $wingetCommand) {
    Write-Error 'winget ist nicht installiert oder nicht im PATH.'
}
$script:WingetDeferred = $false
$script:WingetFailed = $false
$script:PackageObservations = [Collections.Generic.List[object]]::new()

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
        $result = Invoke-HBBoundedProcess -FilePath $wingetCommand.Source `
            -Arguments $Arguments `
            -TimeoutMilliseconds ($ProcessTimeoutSeconds * 1000) `
            -CleanupMilliseconds ($ProcessCleanupSeconds * 1000) `
            -CommandLabel $Action
        if ($result.StandardOutput) { Write-Host $result.StandardOutput }
        if ($result.StandardError) { Write-Warning $result.StandardError }
        if ($result.Status -eq 'TimedOut') {
            if (-not $result.ProcessTreeCleaned) {
                $script:WingetFailed = $true
                Write-Warning "WinGet-Prozessbaum konnte nicht vollstaendig beendet werden / process tree cleanup failed: ${Action}"
                return 124
            }
            if ($Arguments[0] -in @('upgrade', 'install')) {
                $script:WingetDeferred = $true
                Write-Warning "DEFERRED_ADMIN_REQUIRED: Zeitgrenze erreicht / timeout reached: ${Action}"
                return 75
            }
            $script:WingetFailed = $true
            return 124
        }
        return [int]$result.ExitCode
    }

    Write-Host "WHATIF: $display"
    return 0
}

function Invoke-HBWingetRead {
    param([Parameter(Mandatory)][string[]]$Arguments)

    return Invoke-HBBoundedProcess -FilePath $wingetCommand.Source `
        -Arguments $Arguments `
        -TimeoutMilliseconds ([Math]::Min($ProcessTimeoutSeconds, 30) * 1000) `
        -CleanupMilliseconds ($ProcessCleanupSeconds * 1000) `
        -CommandLabel "winget $($Arguments[0])"
}

function Test-HBWingetSearchId {
    param([Parameter(Mandatory)][string] $Id)

    $result = Invoke-HBWingetRead -Arguments @('search', '--id', $Id, '--exact', '--accept-source-agreements')
    if (-not $result.Succeeded) { return $false }
    return ($result.StandardOutput -match [regex]::Escape($Id))
}

function Test-HBWingetInstalledId {
    param([Parameter(Mandatory)][string] $Id)

    $result = Invoke-HBWingetRead -Arguments @('list', '--id', $Id, '--exact', '--accept-source-agreements')
    if (-not $result.Succeeded) { return $false }
    return ($result.StandardOutput -match [regex]::Escape($Id))
}

function Get-HBWingetInstalledIds {
    $result = Invoke-HBWingetRead -Arguments @('list', '--accept-source-agreements')
    if (-not $result.Succeeded) { return @() }
    $output = $result.StandardOutput -split '\r?\n'

    $ids = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
    foreach ($line in $output) {
        $packageMatches = [regex]::Matches($line, '\s([A-Za-z0-9][A-Za-z0-9._-]+(?:\.[A-Za-z0-9][A-Za-z0-9._-]+)+)\s+([0-9][^\s]*)')
        foreach ($match in $packageMatches) {
            [void]$ids.Add($match.Groups[1].Value)
        }
    }
    # Exact registry probes use the same canonical IDs as install decisions and
    # close locale/column-width gaps in the bulk WinGet table parser.
    foreach ($id in $allRegistryIds) {
        if (Test-HBWingetInstalledId -Id $id) {
            [void]$ids.Add($id)
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
        $sourceStatus = Invoke-HBWinget -Arguments @('source', 'update') -Action 'WinGet source update'
        if ($sourceStatus -ne 0) { $script:WingetFailed = $true }
    }
    if ($WhatIfPreference) {
        $upgradeStatus = Invoke-HBWinget -Arguments @('upgrade', '--all', '--accept-package-agreements', '--accept-source-agreements') -Action 'WinGet package upgrade'
    } elseif ($env:HB_ALLOW_ADMIN_PROMPTS -eq '1') {
        $upgradeStatus = Invoke-HBWinget -Arguments @('upgrade', '--all', '--accept-package-agreements', '--accept-source-agreements') -Action 'WinGet package upgrade'
    } else {
        $script:WingetDeferred = $true
        Write-Warning 'DEFERRED_ADMIN_REQUIRED: winget upgrade --all wurde ohne aktuelle Admin-Prompt-Autoritaet nicht gestartet / was not started without current authority.'
        $upgradeStatus = 75
    }
    if ($upgradeStatus -notin @(0, 75)) { $script:WingetFailed = $true }
}

if (-not $CompareOnly) {
    foreach ($id in $installIds) {
        if (Test-HBWingetInstalledId -Id $id) {
            Write-Host "OK package: $id"
            $script:PackageObservations.Add([pscustomobject]@{ Id = $id; Status = 'OK'; Evidence = 'exact-list-before-install' })
            continue
        }

        if (-not (Test-HBWingetSearchId -Id $id)) {
            Write-Error "WinGet-ID nicht gefunden: $id"
        }

        if (-not $WhatIfPreference -and $env:HB_ALLOW_ADMIN_PROMPTS -ne '1') {
            $script:WingetDeferred = $true
            $script:PackageObservations.Add([pscustomobject]@{ Id = $id; Status = 'DEFERRED_ADMIN_REQUIRED'; Evidence = 'admin-authority-not-granted' })
            Write-Warning "DEFERRED_ADMIN_REQUIRED package: ${id}"
            continue
        }

        Write-Host "INSTALL package: $id"
        $installStatus = Invoke-HBWinget -Arguments @('install', '--id', $id, '--exact', '--accept-package-agreements', '--accept-source-agreements') -Action "Install $id"
        if ($installStatus -eq 75) {
            $script:PackageObservations.Add([pscustomobject]@{ Id = $id; Status = 'DEFERRED_ADMIN_REQUIRED'; Evidence = 'bounded-install' })
        } elseif ($installStatus -ne 0) {
            $script:PackageObservations.Add([pscustomobject]@{ Id = $id; Status = 'FAILED'; Evidence = 'bounded-install' })
        }
    }

    Install-HBVSCodeExtensions
    Install-HBCLITools
    Install-HBNpmAgentTools
}

$installedIds = @(Get-HBWingetInstalledIds)
$missingFromRegistry = @($installedIds | Where-Object { $allRegistryIds -notcontains $_ })

foreach ($id in $allRegistryIds) {
    $status = if ($installedIds -contains $id) { 'OK' } else { 'MISSING' }
    $script:PackageObservations.Add([pscustomobject]@{ Id = $id; Status = $status; Evidence = 'canonical-final-installed-set' })
}
$packageResults = @(Get-HBPackageResults -Observations $script:PackageObservations)
$requiredMissing = @(
    $packageResults |
        Where-Object { $_.CanonicalId -in @($requiredRegistryIds | ForEach-Object { $_.ToLowerInvariant() }) -and $_.FinalStatus -ne 'OK' } |
        ForEach-Object { $_.CanonicalId }
)
$optionalMissing = @(
    $packageResults |
        Where-Object { $_.CanonicalId -in @($optionalRegistryIds | ForEach-Object { $_.ToLowerInvariant() }) -and $_.FinalStatus -ne 'OK' } |
        ForEach-Object { $_.CanonicalId }
)
if ($requiredMissing.Count -gt 0) {
    Write-Host 'missing_on_machine.required.packages'
    $requiredMissing | ForEach-Object { Write-Host "  - $_" }
} else {
    Write-Host 'missing_on_machine.required.packages: none'
}
if ($optionalMissing.Count -gt 0) {
    Write-Host 'missing_on_machine.optional.packages'
    $optionalMissing | ForEach-Object { Write-Host "  - $_" }
} else {
    Write-Host 'missing_on_machine.optional.packages: none'
}
foreach ($result in $packageResults | Where-Object { $_.FinalStatus -eq 'CONFLICT' }) {
    $script:WingetFailed = $true
    Write-Warning "CONFLICT package status: $($result.CanonicalId)"
}

if ($missingFromRegistry.Count -gt 0) {
    Write-Host 'missing_from_registry.packages'
    $missingFromRegistry | ForEach-Object { Write-Host "  - $_" }
} else {
    Write-Host 'missing_from_registry.packages: none'
}

Compare-HBVSCodeRegistry
Compare-HBCLIRegistry
Compare-HBNpmAgentRegistry

$moduleMaintainer = Join-Path $repoRoot 'scripts/maintain-powershell-modules.ps1'
if (-not (Test-Path -LiteralPath $moduleMaintainer -PathType Leaf)) {
    Write-Error "PowerShell-Modulpfleger nicht gefunden: $moduleMaintainer"
}
$moduleParameters = @{ Registry = $PowerShellModuleRegistry }
if ($CompareOnly) { $moduleParameters.CompareOnly = $true }
if ($IncludeOptional) { $moduleParameters.IncludeOptional = $true }
if ($WhatIfPreference) { $moduleParameters.WhatIf = $true }
& $moduleMaintainer @moduleParameters
if ($LASTEXITCODE -notin @(0, $null)) { $script:WingetFailed = $true }

if ($script:WingetFailed) { exit 2 }
if ($script:WingetDeferred) { exit 75 }
exit 0
