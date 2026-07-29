#Requires -Version 7
<#
.SYNOPSIS
    Orchestrates repository and agentic toolchain maintenance on Windows.

.DESCRIPTION
    Completes bounded fetch attempts for Level 0 and every active Git target
    before any domain mutation. Only clean canonical behind-only branches are
    fast-forwarded. It then synchronizes the local home baseline, checks the
    GSDB registry, maintenance package and data-driven preset profiles, and
    maintains the WinGet-based machine toolchain. Lease-owned temporary
    worktrees preserve ambiguous evidence instead of deleting user paths.
    The script never commits or pushes target repositories and never switches
    branches.

    Without parameters, an interactive terminal starts the enhanced TUI with
    Dry-run selected. Redirected or parameterized use preserves the existing
    headless maintenance workflow. Use -CheckOnly for a read-oriented status
    run or -WhatIf for a preview of mutating operations.

.PARAMETER Tui
    Start the enhanced terminal UI. Unsupported terminal or build capability
    falls back visibly to the line-oriented assistant before engine start.

.PARAMETER PlainUi
    Start the line-oriented, text-first maintenance assistant.

.PARAMETER NoTui
    Run the maintenance engine without interactive selection. This switch is
    also the recursion guard used by the TUI.

.PARAMETER CheckOnly
    Fetch and report only. Do not pull repositories, synchronize files, update
    the registry, propagate files, or update packages.

.PARAMETER ScriptsOnly
    Maintain repositories, home sync, registry, and propagation only. Skip
    WinGet and other machine-toolchain changes.

.PARAMETER RepairDrift
    Repair canonical maintenance-package drift in Level-1/Level-2 repositories.
    The affected repositories remain uncommitted and unpushed for review.

.PARAMETER IncludeOptional
    Install optional WinGet, VS Code, CLI, and npm registry entries too.

.PARAMETER AllowAdminPrompts
    Allow administrator prompts for the current run only. No credentials are
    stored or written to logs.

.PARAMETER ManifestPath
    Alternative desired-state fleet manifest, primarily for isolated tests.

.PARAMETER HomeDir
    Alternative home directory, primarily for isolated tests or a second profile.

.PARAMETER EventStream
    Interner, benutzerprivater JSONL-Ereignispfad für die TUI. Nur zusammen
    mit -NoTui und unterhalb von .home-baseline/events zulässig.

    Internal user-private JSONL event path for the TUI. Valid only with
    -NoTui and below .home-baseline/events.

.PARAMETER RunId
    Interne UUID zur Korrelation von Prozess, Ereignissen und Bericht.

    Internal UUID correlating process, events, and report.

.PARAMETER GitRetryAttempts
    Maximale begrenzte Versuche fuer transiente Fetch-/Fast-forward-Fehler.
    Authentifizierungs- und Repository-Zustandsfehler werden nicht wiederholt.

    Maximum bounded attempts for transient fetch and fast-forward pull
    failures. Authentication and repository-state failures are not retried.

.PARAMETER GitTimeoutSeconds
    Harte Zeitgrenze fuer einen Fetch- oder Pull-Versuch.

    Hard timeout for one fetch or pull attempt.

.PARAMETER WinGetTimeoutSeconds
    Harte Zeitgrenze fuer jeden weitergereichten WinGet-Unterprozess. Ein
    Upgrade oder Installer, der nicht sicher abgeschlossen werden kann, wird
    als DEFERRED_ADMIN_REQUIRED gemeldet.

    Hard timeout forwarded to every WinGet subprocess. An upgrade or installer
    that cannot complete safely is reported as DEFERRED_ADMIN_REQUIRED.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1

    Performs the complete maintenance workflow.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -CheckOnly

    Fetches and reports repository and toolchain state without applying updates.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -WhatIf

    Previews all mutating steps.

.EXAMPLE
    pwsh -NoProfile -File scripts/maintain-agentic-workspace.ps1 -ScriptsOnly -RepairDrift

    Updates repositories and repairs maintenance files locally without commits.

.NOTES
    Exit codes: 0 = current/success, 1 = drift found, 2 = operational error,
    3 = drift repaired and affected repositories need separate review/commit/push.
    Exitcode, sichtbarer Abschluss und JSON-Bericht werden aus derselben Run-ID
    abgeleitet. Eigene reparierte Dirty-Zwischenstaende werden nur mit exakt
    passender atomarer Resume-Evidence akzeptiert.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [switch] $Tui,
    [switch] $PlainUi,
    [switch] $NoTui,
    [switch] $CheckOnly,
    [switch] $ScriptsOnly,
    [switch] $RepairDrift,
    [switch] $IncludeOptional,
    [switch] $AllowAdminPrompts,
    [string] $ManifestPath,
    [string] $HomeDir = [Environment]::GetFolderPath('UserProfile'),
    [string] $EventStream,
    [string] $RunId,
    [ValidateRange(1, 10)][int] $GitRetryAttempts = 3,
    [ValidateRange(5, 3600)][int] $GitTimeoutSeconds = 300,
    [ValidateRange(5, 86400)][int] $WinGetTimeoutSeconds = 1800
)

$entryBoundParameters = @{} + $PSBoundParameters
$requestedRunId = $RunId
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$script:HBMaintenanceScriptPath = $PSCommandPath
$hardeningModule = Join-Path $PSScriptRoot 'lib/windows-maintenance-hardening.psm1'
if (-not (Test-Path -LiteralPath $hardeningModule -PathType Leaf)) {
    throw "Windows-Wartungsmodul fehlt / Windows maintenance module missing: ${hardeningModule}"
}
Import-Module $hardeningModule -Force

function Invoke-HBAgenticWorkspaceMaintenance {
    <#
    .SYNOPSIS
        Runs the cross-platform one-command workspace maintenance.
    .DESCRIPTION
        Invokes the repository-owned script with native PowerShell parameter
        binding. Use CheckOnly or WhatIf before an update run.
    #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [switch] $Tui,
        [switch] $PlainUi,
        [switch] $NoTui,
        [switch] $CheckOnly,
        [switch] $ScriptsOnly,
        [switch] $RepairDrift,
        [switch] $IncludeOptional,
        [switch] $AllowAdminPrompts,
        [string] $ManifestPath,
        [string] $HomeDir = [Environment]::GetFolderPath('UserProfile'),
        [string] $EventStream,
        [string] $RunId,
        [ValidateRange(1, 10)][int] $GitRetryAttempts = 3,
        [ValidateRange(5, 3600)][int] $GitTimeoutSeconds = 300,
        [ValidateRange(5, 86400)][int] $WinGetTimeoutSeconds = 1800
    )
    $parameters = @{
        Tui = $Tui
        PlainUi = $PlainUi
        NoTui = $NoTui
        CheckOnly = $CheckOnly
        ScriptsOnly = $ScriptsOnly
        RepairDrift = $RepairDrift
        IncludeOptional = $IncludeOptional
        AllowAdminPrompts = $AllowAdminPrompts
        HomeDir = $HomeDir
        GitRetryAttempts = $GitRetryAttempts
        GitTimeoutSeconds = $GitTimeoutSeconds
        WinGetTimeoutSeconds = $WinGetTimeoutSeconds
    }
    if ($EventStream) { $parameters.EventStream = $EventStream }
    if ($RunId) { $parameters.RunId = $RunId }
    if ($ManifestPath) { $parameters.ManifestPath = $ManifestPath }
    if ($WhatIfPreference) { $parameters.WhatIf = $true }
    & $script:HBMaintenanceScriptPath @parameters
}

if ($MyInvocation.InvocationName -eq '.') {
    return
}

$uiSelectors = @($Tui, $PlainUi, $NoTui).Where({ [bool]$_ }).Count
if ($uiSelectors -gt 1) {
    Write-Host 'Fehler / Error: -Tui, -PlainUi und -NoTui sind gegenseitig ausgeschlossen / are mutually exclusive.' -ForegroundColor Red
    exit 2
}
$maintenanceParameterNames = @(
    'CheckOnly',
    'ScriptsOnly',
    'RepairDrift',
    'IncludeOptional',
    'AllowAdminPrompts',
    'ManifestPath',
    'GitRetryAttempts',
    'GitTimeoutSeconds',
    'WinGetTimeoutSeconds',
    'WhatIf'
)
$hasExplicitMaintenanceParameter = @(
    $maintenanceParameterNames | Where-Object { $entryBoundParameters.ContainsKey($_) }
).Count -gt 0
if (($Tui -or $PlainUi) -and $hasExplicitMaintenanceParameter) {
    Write-Host 'Fehler / Error: Interaktive UI-Auswahl darf keine Wartungsoption vorwegnehmen / interactive UI selectors cannot include maintenance options.' -ForegroundColor Red
    exit 2
}
if (($EventStream -or $requestedRunId) -and -not $NoTui) {
    Write-Host 'Fehler / Error: Interne Ereignisparameter erfordern -NoTui / internal event parameters require -NoTui.' -ForegroundColor Red
    exit 2
}
if ($CheckOnly -and $WhatIfPreference) {
    Write-Host 'Fehler / Error: -CheckOnly und / and -WhatIf sind nicht kombinierbar / cannot be combined.' -ForegroundColor Red
    exit 2
}
if ($RepairDrift -and ($CheckOnly -or $WhatIfPreference)) {
    Write-Host 'Fehler / Error: -RepairDrift ist nur im echten Lauf erlaubt / is only allowed in an actual run.' -ForegroundColor Red
    exit 2
}
if ($IncludeOptional -and $ScriptsOnly) {
    Write-Host 'Fehler / Error: -IncludeOptional passt nicht zu / cannot be combined with -ScriptsOnly.' -ForegroundColor Red
    exit 2
}
$maintenanceMode = Get-HBMaintenanceMode -CheckOnly:$CheckOnly -Preview:$WhatIfPreference

$sourceRoot = (Resolve-Path -LiteralPath (Split-Path -Parent $PSScriptRoot)).Path
$presetProfileCatalog = Join-Path $sourceRoot 'scripts/config/spec-kit-preset-profiles.json'
$fleetPresetProfile = $null
$fleetEngine = Join-Path $sourceRoot 'scripts/lib/agentic_workspace_fleet.py'
if (-not $ManifestPath) {
    $ManifestPath = Join-Path $sourceRoot 'scripts/config/agentic-workspace-fleet.json'
}
$HomeDir = (Resolve-Path -LiteralPath $HomeDir).Path
$homeScriptsDir = Join-Path $HomeDir 'scripts'
if ((Test-Path -LiteralPath $homeScriptsDir -PathType Container) -and
    ((Resolve-Path -LiteralPath $PSScriptRoot).Path -eq (Resolve-Path -LiteralPath $homeScriptsDir).Path)) {
    . (Join-Path $PSScriptRoot 'lib/resolve-home-baseline-source.ps1')
    $sourceRoot = Resolve-HBSourceRepository -StartPath $PSScriptRoot -AllowLegacy
    $repoScript = Join-Path $sourceRoot 'scripts/maintain-agentic-workspace.ps1'
    if (-not (Test-Path -LiteralPath $repoScript -PathType Leaf)) {
        throw "Canonical maintenance script missing: $repoScript"
    }
    $forward = @{}
    if ($CheckOnly) { $forward.CheckOnly = $true }
    if ($ScriptsOnly) { $forward.ScriptsOnly = $true }
    if ($RepairDrift) { $forward.RepairDrift = $true }
    if ($IncludeOptional) { $forward.IncludeOptional = $true }
    if ($AllowAdminPrompts) { $forward.AllowAdminPrompts = $true }
    if ($Tui) { $forward.Tui = $true }
    if ($PlainUi) { $forward.PlainUi = $true }
    if ($NoTui) { $forward.NoTui = $true }
    if ($EventStream) { $forward.EventStream = $EventStream }
    if ($requestedRunId) { $forward.RunId = $requestedRunId }
    if ($ManifestPath) { $forward.ManifestPath = $ManifestPath }
    if ($WhatIfPreference) { $forward.WhatIf = $true }
    $forward.HomeDir = $HomeDir
    $forward.GitRetryAttempts = $GitRetryAttempts
    $forward.GitTimeoutSeconds = $GitTimeoutSeconds
    $forward.WinGetTimeoutSeconds = $WinGetTimeoutSeconds
    & $repoScript @forward
    exit $LASTEXITCODE
}

function Read-HBPlainYesNo {
    param([Parameter(Mandatory)][string] $Prompt)
    $answer = Read-Host $Prompt
    return $answer -in @('y', 'Y', 'j', 'J')
}

function Invoke-HBPlainMaintenanceUi {
    Write-Host 'Agentischer Workspace: Wartung / Agentic workspace maintenance'
    Write-Host 'Die Vorschau zeigt geplante Änderungen und nimmt keine Änderung vor.'
    Write-Host 'The dry-run shows planned changes and does not modify the workspace.'
    Write-Host '1) Vorschau / Dry-run [Standard / default]'
    Write-Host '2) Nur prüfen / Check-only'
    Write-Host '3) Aktualisieren / Update'
    $choice = Read-Host 'Auswahl / Selection [1]'
    $engineArguments = [Collections.Generic.List[object]]::new()
    $engineArguments.Add('-NoTui')
    switch ($choice) {
        '2' { $engineArguments.Add('-CheckOnly') }
        '3' { }
        default { $engineArguments.Add('-WhatIf') }
    }
    $scriptsOnlyChoice = Read-HBPlainYesNo -Prompt 'Nur Skripte? / Scripts only? [y/N]'
    if ($scriptsOnlyChoice) {
        $engineArguments.Add('-ScriptsOnly')
    } elseif (Read-HBPlainYesNo -Prompt 'Optionale Werkzeuge? / Optional tools? [y/N]') {
        $engineArguments.Add('-IncludeOptional')
    }
    if ($choice -eq '3') {
        if (Read-HBPlainYesNo -Prompt 'Wartungspaket-Drift lokal reparieren? / Repair maintenance-package drift locally? [y/N]') {
            $engineArguments.Add('-RepairDrift')
        }
        if (-not (Read-HBPlainYesNo -Prompt 'Schreibenden Lauf einmal starten? / Start one mutating run? [y/N]')) {
            Write-Host 'Vor dem Engine-Start abgebrochen. / Cancelled before engine start.'
            exit 130
        }
    }
    $engineArguments.Add('-HomeDir')
    $engineArguments.Add($HomeDir)
    $display = @('pwsh', '-NoProfile', '-File', $PSCommandPath) + @(
        $engineArguments | ForEach-Object { "'$($_.ToString().Replace("'", "''"))'" }
    )
    Write-Host "Befehl / command: $($display -join ' ')"
    & $PSCommandPath @engineArguments
    exit $LASTEXITCODE
}

function Get-HBTuiFingerprint {
    param([Parameter(Mandatory)][string] $Root)
    $incremental = [Security.Cryptography.IncrementalHash]::CreateHash(
        [Security.Cryptography.HashAlgorithmName]::SHA256
    )
    try {
        $utf8 = [Text.UTF8Encoding]::new($false)
        $incremental.AppendData($utf8.GetBytes("contract:1`n"))
        $paths = @(
            Get-ChildItem -LiteralPath (Join-Path $Root 'scripts/lib/maintenance-tui/src/HomeBaseline.MaintenanceTui') `
                -Recurse -File |
                Where-Object { $_.Extension -in @('.cs', '.csproj', '.json') }
            Get-Item -LiteralPath (Join-Path $Root 'scripts/lib/maintenance-tui/NuGet.config')
            Get-Item -LiteralPath (
                Join-Path $Root `
                    'scripts/lib/maintenance-tui/tests/HomeBaseline.MaintenanceTui.Tests/packages.lock.json'
            )
        ) | Sort-Object { [IO.Path]::GetRelativePath($Root, $_.FullName).Replace('\', '/') }
        foreach ($path in $paths) {
            $relative = [IO.Path]::GetRelativePath($Root, $path.FullName).Replace('\', '/')
            $bytes = [IO.File]::ReadAllBytes($path.FullName)
            $incremental.AppendData($utf8.GetBytes("path:${relative}`nlength:$($bytes.Length)`n"))
            $incremental.AppendData($bytes)
            $incremental.AppendData($utf8.GetBytes("`n"))
        }
        return [Convert]::ToHexStringLower($incremental.GetHashAndReset())
    } finally {
        $incremental.Dispose()
    }
}

function Test-HBTuiCacheComplete {
    param(
        [Parameter(Mandatory)][string] $CacheDirectory,
        [Parameter(Mandatory)][string] $Fingerprint,
        [Parameter(Mandatory)][string] $Platform
    )
    $entry = Join-Path $CacheDirectory 'HomeBaseline.MaintenanceTui.dll'
    $metadataPath = Join-Path $CacheDirectory 'cache.json'
    if (-not (Test-Path $entry -PathType Leaf) -or -not (Test-Path $metadataPath -PathType Leaf)) {
        return $false
    }
    try {
        $metadata = Get-Content -LiteralPath $metadataPath -Raw -Encoding UTF8 | ConvertFrom-Json
        return (
            $metadata.schemaVersion -eq 1 -and
            $metadata.fingerprint -eq $Fingerprint -and
            $metadata.platform -eq $Platform
        )
    } catch {
        return $false
    }
}

function Invoke-HBEnhancedMaintenanceUi {
    if ([Console]::IsInputRedirected -or [Console]::IsOutputRedirected -or $env:TERM -eq 'dumb') {
        Write-Warning 'Erweiterte TUI nicht verfügbar; lineare Ausgabe wird verwendet / enhanced TUI unavailable; using plain output.'
        Invoke-HBPlainMaintenanceUi
    }
    $dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
    $dotnetVersion = if ($dotnet) { (& dotnet --version 2>$null) } else { '' }
    if (-not $dotnet -or -not $dotnetVersion.StartsWith('10.', [StringComparison]::Ordinal)) {
        Write-Warning '.NET-10-SDK fehlt; lineare Ausgabe wird verwendet / .NET 10 SDK is missing; using plain output.'
        Invoke-HBPlainMaintenanceUi
    }
    $project = Join-Path $sourceRoot 'scripts/lib/maintenance-tui/src/HomeBaseline.MaintenanceTui/HomeBaseline.MaintenanceTui.csproj'
    $architecture = [Runtime.InteropServices.RuntimeInformation]::ProcessArchitecture.ToString().ToLowerInvariant()
    if ($architecture -notin @('arm64', 'x64')) {
        Write-Warning 'Plattform wird nicht unterstützt / platform is unsupported.'
        Invoke-HBPlainMaintenanceUi
    }
    $fingerprint = Get-HBTuiFingerprint -Root $sourceRoot
    $platform = "windows-${architecture}"
    $cacheParent = Join-Path $HomeDir ".home-baseline/cache/maintenance-tui/${platform}"
    $cacheDirectory = Join-Path $cacheParent $fingerprint
    $entry = Join-Path $cacheDirectory 'HomeBaseline.MaintenanceTui.dll'
    $metadata = Join-Path $cacheDirectory 'cache.json'
    $buildLock = Join-Path $cacheParent "${fingerprint}.lock"
    $lockAcquired = $false
    if (-not (Test-HBTuiCacheComplete -CacheDirectory $cacheDirectory -Fingerprint $fingerprint -Platform $platform)) {
        try {
            New-Item -ItemType Directory -Path $cacheParent -Force -ErrorAction Stop | Out-Null
        } catch {
            Write-Warning 'TUI-Cache ist nicht beschreibbar; lineare Ausgabe wird verwendet / TUI cache is not writable; using plain output.'
            Invoke-HBPlainMaintenanceUi
        }
        foreach ($attempt in 0..99) {
            try {
                New-Item -ItemType Directory -Path $buildLock -ErrorAction Stop | Out-Null
                $lockAcquired = $true
                break
            } catch {
                if (Test-HBTuiCacheComplete -CacheDirectory $cacheDirectory -Fingerprint $fingerprint -Platform $platform) {
                    break
                }
                Start-Sleep -Milliseconds 100
            }
        }
        if (-not $lockAcquired -and
            -not (Test-HBTuiCacheComplete -CacheDirectory $cacheDirectory -Fingerprint $fingerprint -Platform $platform)) {
            Write-Warning 'TUI-Build-Lock blieb belegt; lineare Ausgabe wird verwendet / TUI build lock remained busy; using plain output.'
            Invoke-HBPlainMaintenanceUi
        }
    }
    if (-not (Test-HBTuiCacheComplete -CacheDirectory $cacheDirectory -Fingerprint $fingerprint -Platform $platform)) {
        if (Test-Path $cacheDirectory) {
            Remove-Item -LiteralPath $cacheDirectory -Recurse -Force
        }
        $temporary = Join-Path $cacheParent ".build.$([Guid]::NewGuid())"
        try {
            New-Item -ItemType Directory -Path $temporary -ErrorAction Stop | Out-Null
        } catch {
            Write-Warning 'Temporärer TUI-Buildpfad ist nicht verfügbar; lineare Ausgabe wird verwendet / temporary TUI build path is unavailable; using plain output.'
            Invoke-HBPlainMaintenanceUi
        }
        & dotnet restore $project --locked-mode | Out-Null
        $restoreExit = $LASTEXITCODE
        if ($restoreExit -eq 0) {
            & dotnet publish $project --configuration Release --no-restore --output (Join-Path $temporary 'publish') | Out-Null
        }
        if ($restoreExit -ne 0 -or $LASTEXITCODE -ne 0) {
            Remove-Item -LiteralPath $temporary -Recurse -Force
            if ($lockAcquired -and (Test-Path $buildLock)) {
                Remove-Item -LiteralPath $buildLock -Recurse -Force
            }
            Write-Warning 'TUI-Build fehlgeschlagen; lineare Ausgabe wird verwendet / TUI build failed; using plain output.'
            Invoke-HBPlainMaintenanceUi
        }
        $publish = Join-Path $temporary 'publish'
        [IO.File]::WriteAllText(
            (Join-Path $publish 'cache.json'),
            (@{ schemaVersion = 1; fingerprint = $fingerprint; platform = $platform } |
                ConvertTo-Json -Compress) + "`n",
            [Text.UTF8Encoding]::new($false)
        )
        try {
            Move-Item -LiteralPath $publish -Destination $cacheDirectory -ErrorAction Stop
        } catch {
            if (-not (Test-Path $entry -PathType Leaf) -or -not (Test-Path $metadata -PathType Leaf)) {
                Write-Warning 'TUI-Cache konnte nicht atomar veröffentlicht werden; lineare Ausgabe wird verwendet / TUI cache could not be published atomically; using plain output.'
                Invoke-HBPlainMaintenanceUi
            }
        } finally {
            if (Test-Path $temporary) { Remove-Item -LiteralPath $temporary -Recurse -Force }
            if ($lockAcquired -and (Test-Path $buildLock)) {
                Remove-Item -LiteralPath $buildLock -Recurse -Force
            }
        }
    }
    if (-not (Test-HBTuiCacheComplete -CacheDirectory $cacheDirectory -Fingerprint $fingerprint -Platform $platform)) {
        Write-Warning 'TUI-Cache ist unvollständig; lineare Ausgabe wird verwendet / TUI cache is incomplete; using plain output.'
        Invoke-HBPlainMaintenanceUi
    }
    & dotnet $entry --wrapper $PSCommandPath --home-dir $HomeDir
    exit $LASTEXITCODE
}

$implicitInteractiveUi = (
    $entryBoundParameters.Count -eq 0 -and
    -not [Console]::IsInputRedirected -and
    -not [Console]::IsOutputRedirected
)
if ($Tui -or $implicitInteractiveUi) {
    Invoke-HBEnhancedMaintenanceUi
}
if ($PlainUi) {
    Invoke-HBPlainMaintenanceUi
}

$registry = Join-Path $HomeDir '.home-baseline/level2-repository-registry.json'
$stateDir = Join-Path $HomeDir '.home-baseline'
$lockDir = Join-Path $stateDir 'locks/agentic-workspace-maintenance.lock'
$logDir = Join-Path $stateDir 'logs'
$reportDir = Join-Path $stateDir 'reports'
$resumeEvidenceFile = Join-Path $stateDir 'agentic-workspace-resume.json'
$presetWorktreeLeaseDir = Join-Path $stateDir 'preset-validation-leases'
$presetWorktreeStateDir = Join-Path $stateDir 'preset-validation-worktrees'
$eventRoot = [IO.Path]::GetFullPath((Join-Path $stateDir 'events'))
if ($EventStream) {
    $EventStream = [IO.Path]::GetFullPath($EventStream)
    $eventPrefix = $eventRoot.TrimEnd(
        [IO.Path]::DirectorySeparatorChar,
        [IO.Path]::AltDirectorySeparatorChar
    ) + [IO.Path]::DirectorySeparatorChar
    if (-not $EventStream.StartsWith($eventPrefix, [StringComparison]::OrdinalIgnoreCase)) {
        Write-Host 'Fehler / Error: Event-Pfad liegt außerhalb des privaten Evidence-Verzeichnisses / event path is outside private evidence.' -ForegroundColor Red
        exit 2
    }
    New-Item -ItemType Directory -Path (Split-Path -Parent $EventStream) -Force | Out-Null
    [IO.File]::WriteAllText($EventStream, '', [Text.UTF8Encoding]::new($false))
    $identity = [Security.Principal.WindowsIdentity]::GetCurrent().Name
    & icacls $EventStream /inheritance:r /grant:r "${identity}:(F)" | Out-Null
    if ($LASTEXITCODE -ne 0) {
        Write-Host 'Fehler / Error: Event-Datei konnte nicht auf den aktuellen Benutzer begrenzt werden / event file could not be restricted to the current user.' -ForegroundColor Red
        exit 2
    }
}
$script:Findings = 0
$script:RepairApplied = $false
$script:PreviewDrift = $false
$script:ResumeAllowedPaths = @()
$exitCode = 0
$transcriptStarted = $false
$runId = if ($requestedRunId) {
    try { ([Guid]$requestedRunId).ToString() } catch {
        Write-Host 'Fehler / Error: Run-ID ist keine gültige UUID / run ID is not a valid UUID.' -ForegroundColor Red
        exit 2
    }
} else {
    [Guid]::NewGuid().ToString()
}
$reportFile = Join-Path $reportDir "agentic-workspace-${runId}.json"
$script:MaintenanceEventSequence = 0
$script:StartedMaintenanceEventPhases = [Collections.Generic.HashSet[string]]::new(
    [StringComparer]::Ordinal
)
if (-not (Test-Path -LiteralPath $fleetEngine -PathType Leaf)) {
    throw "Fleet-Vertragskern fehlt / fleet contract engine missing: ${fleetEngine}"
}

function Write-HBEarlyFailureReport {
    param(
        [Parameter(Mandatory)][string]$Status,
        [Parameter(Mandatory)][int]$ExitCode,
        [Parameter(Mandatory)][string]$Summary,
        [Parameter(Mandatory)][string]$NextAction
    )
    New-Item -ItemType Directory -Path $reportDir -Force | Out-Null
    $payload = [ordered]@{
        schemaVersion = '1.0'
        runId = $runId
        platform = 'win32'
        mode = $maintenanceMode.FleetMode
        startedAt = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ')
        completedAt = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ')
        overallStatus = $Status
        exitCode = $ExitCode
        stages = @([ordered]@{
            stageId = 'prerequisites'
            status = if ($ExitCode -eq 2) { 'Failed' } else { 'Blocked' }
            exitCode = $ExitCode
            durationMs = 0
            summary = $Summary
            nextAction = $NextAction
        })
        targets = @()
        toolchain = @()
        findings = @([ordered]@{
            code = 'PrerequisiteUnavailable'
            severity = if ($ExitCode -eq 2) { 'Fatal' } else { 'Blocking' }
            summary = $Summary
            nextAction = $NextAction
        })
        artifacts = [ordered]@{ logPath = 'N/A'; reportPath = $reportFile }
    }
    [IO.File]::WriteAllText(
        $reportFile,
        ($payload | ConvertTo-Json -Depth 8) + "`n",
        [Text.UTF8Encoding]::new($false)
    )
}

try {
    $script:HBPythonLauncher = Resolve-HBPythonLauncher
} catch {
    $message = 'Kein validierter Python-3-Launcher / no validated Python 3 launcher.'
    $next = 'Python 3 installieren oder den defekten Store-Alias deaktivieren, dann erneut ausfuehren / install Python 3 or disable the broken Store alias, then retry'
    Write-HBEarlyFailureReport -Status FAILED -ExitCode 2 -Summary $message -NextAction $next
    Write-Host "Fehler / Error: ${message}" -ForegroundColor Red
    Write-Host "Report / Bericht: ${reportFile}"
    exit 2
}

try {
    $profileCatalogData = Get-Content -LiteralPath $presetProfileCatalog -Raw -Encoding UTF8 | ConvertFrom-Json
    $fleetPresetProfile = if (
        (Test-Path -LiteralPath $registry -PathType Leaf) -and
        ((Get-Content -LiteralPath $registry -Raw -Encoding UTF8 | ConvertFrom-Json).defaultPresetProfile)
    ) {
        [string](Get-Content -LiteralPath $registry -Raw -Encoding UTF8 | ConvertFrom-Json).defaultPresetProfile
    } else {
        [string]$profileCatalogData.defaultProfile
    }
    $profileProperty = $profileCatalogData.profiles.PSObject.Properties[$fleetPresetProfile]
    if ($null -eq $profileProperty -or -not $profileProperty.Value.presetConfig) {
        throw "Unbekanntes Flottenprofil / unknown fleet profile: ${fleetPresetProfile}"
    }
    $fleetPresetConfig = Join-Path $sourceRoot ([string]$profileProperty.Value.presetConfig)
    $fleetPresetCount = @(
        (Get-Content -LiteralPath $fleetPresetConfig -Raw -Encoding UTF8 | ConvertFrom-Json).presets
    ).Count
    if ($fleetPresetCount -lt 1) {
        throw "Flottenprofil enthaelt keine Presets / fleet profile contains no presets."
    }
} catch {
    Write-HBEarlyFailureReport -Status FAILED -ExitCode 2 -Summary $_.Exception.Message `
        -NextAction 'Preset-Profilkatalog und Matrix pruefen / review profile catalog and matrix'
    Write-Host "Fehler / Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Report / Bericht: ${reportFile}"
    exit 2
}

function Invoke-HBPythonCommand {
    param([Parameter(Mandatory)][string[]]$Arguments)
    $commandArguments = @($script:HBPythonLauncher.PrefixArguments) + @($Arguments)
    & $script:HBPythonLauncher.FilePath @commandArguments
}

function ConvertTo-HBEventStatus {
    param([Parameter(Mandatory)][string] $Status)
    return $(switch ($Status) {
        'Passed' { 'PASSED' }
        'Warning' { 'WARNING' }
        'DeferredAdminRequired' { 'WARNING' }
        'Blocked' { 'BLOCKED' }
        'Skipped' { 'SKIPPED' }
        'Failed' { 'FAILED' }
        'Interrupted' { 'FAILED' }
        default { 'PARTIAL' }
    })
}

function Write-HBMaintenanceEvent {
    param(
        [Parameter(Mandatory)][string] $EventType,
        [Parameter(Mandatory)][string] $Status,
        [string] $PhaseId,
        [string] $TargetId,
        [Parameter(Mandatory)][string] $MessageDe,
        [Parameter(Mandatory)][string] $MessageEn,
        [string] $DetailsJson = '{}'
    )
    if (-not $EventStream) { return }
    $script:MaintenanceEventSequence++
    $arguments = @(
        $fleetEngine, 'event',
        '--event-stream', $EventStream,
        '--run-id', $runId,
        '--sequence', [string]$script:MaintenanceEventSequence,
        '--event-type', $EventType,
        '--status', $Status,
        '--message-de', $MessageDe,
        '--message-en', $MessageEn,
        '--details-json', $DetailsJson
    )
    if ($PhaseId) { $arguments += @('--phase-id', $PhaseId) }
    if ($TargetId) { $arguments += @('--target-id', $TargetId) }
    Invoke-HBPythonCommand -Arguments $arguments | Out-Null
    if ($LASTEXITCODE -ne 0) {
        Write-Warning 'Ereignis konnte nicht geschrieben werden / event could not be written.'
    }
}

function Start-HBMaintenanceEventPhase {
    param(
        [Parameter(Mandatory)][ValidateSet(
            'fleet',
            'level0',
            'home-sync',
            'registry',
            'propagation',
            'preset-profiles',
            'toolchain',
            'final'
        )][string] $PhaseId
    )
    if (-not $script:StartedMaintenanceEventPhases.Add($PhaseId)) { return }
    Write-HBMaintenanceEvent -EventType 'phase-started' -Status 'RUNNING' `
        -PhaseId $PhaseId `
        -MessageDe "Phase ${PhaseId} gestartet." `
        -MessageEn "Phase ${PhaseId} started."
}

$requiredAnalyzerVersion = [Version]'1.25.0'
$analyzerAvailable = $null -ne (
    Get-Module -ListAvailable PSScriptAnalyzer |
        Where-Object { $_.Version -eq $requiredAnalyzerVersion } |
        Select-Object -First 1
)
if (-not $analyzerAvailable -and $maintenanceMode.AllowsMutation) {
    $message = "PSScriptAnalyzer ${requiredAnalyzerVersion} fehlt vor der ersten Mutation / is missing before the first mutation."
    $next = 'pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 ausfuehren / run the module maintainer, then retry'
    Write-HBEarlyFailureReport -Status BLOCKED -ExitCode 1 -Summary $message -NextAction $next
    Write-Warning $message
    Write-Host "Report / Bericht: ${reportFile}"
    exit 1
}

function Add-HBReportStage {
    param(
        [Parameter(Mandatory)][string] $StageId,
        [Parameter(Mandatory)][ValidateSet('Passed', 'Warning', 'Blocked', 'Failed', 'Skipped')][string] $Status,
        [Parameter(Mandatory)][int] $ExitCode,
        [Parameter(Mandatory)][string] $Summary,
        [string] $NextAction = 'N/A'
    )
    if (-not (Test-Path -LiteralPath $reportFile -PathType Leaf)) { return }
    Invoke-HBPythonCommand -Arguments @(
        $fleetEngine, 'stage',
        '--report', $reportFile,
        '--stage-id', $StageId,
        '--status', $Status,
        '--exit-code', [string]$ExitCode,
        '--summary', $Summary,
        '--next-action', $NextAction
    )
    if ($LASTEXITCODE -ne 0) {
        throw "Run-Bericht konnte nicht aktualisiert werden / run report update failed: ${StageId}"
    }
    if ($StageId -in @(
        'fleet',
        'level0',
        'home-sync',
        'registry',
        'propagation',
        'preset-profiles',
        'toolchain',
        'final'
    )) {
        Start-HBMaintenanceEventPhase -PhaseId $StageId
        Write-HBMaintenanceEvent -EventType 'phase-completed' `
            -Status (ConvertTo-HBEventStatus -Status $Status) `
            -PhaseId $StageId `
            -MessageDe "Phase ${StageId} abgeschlossen." `
            -MessageEn "Phase ${StageId} completed." `
            -DetailsJson (@{ exitCode = $ExitCode } | ConvertTo-Json -Compress)
    }
}

function Invoke-HBFleetContract {
    $arguments = @(
        $fleetEngine, 'fleet',
        '--manifest', $ManifestPath,
        '--home-dir', $HomeDir,
        '--mode', $maintenanceMode.FleetMode,
        '--report', $reportFile,
        '--log', $logFile,
        '--run-id', $runId,
        '--level0-dir', $sourceRoot
    )
    foreach ($path in $script:ResumeAllowedPaths) {
        $arguments += @('--allowed-dirty-path', $path)
    }
    Invoke-HBPythonCommand -Arguments $arguments | ForEach-Object { Write-Host $_ }
    $status = $LASTEXITCODE
    return [int]$status
}

function Write-HBInfo {
    param([Parameter(Mandatory)][string] $Message)
    Write-Host "`n==> $Message"
}

function Write-HBWarning {
    param([Parameter(Mandatory)][string] $Message)
    Write-Warning $Message
}

function Test-HBHomeSync {
    $syncScript = Join-Path $sourceRoot 'scripts/sync-home.ps1'
    # The parent preview must not implicitly add WhatIf to the nested,
    # intentionally read-only CheckOnly contract.
    & $syncScript -NoPull -CheckOnly -WhatIf:$false
    $status = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }
    switch ($status) {
        0 { Write-Host 'OK: Lokale Home-Baseline ist manifestkonform / local home baseline matches manifest' }
        1 {
            Write-HBWarning 'Lokale Home-Baseline hat Drift oder Konflikte / local home baseline has drift or conflicts'
            $script:Findings++
        }
        default { throw 'sync-home Check fehlgeschlagen / check failed.' }
    }
}

function Invoke-HBGit {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string[]] $Arguments,
        [switch] $Capture
    )
    $isNetworkOperation = $Arguments.Count -gt 0 -and $Arguments[0] -in @('fetch', 'pull')
    if ($isNetworkOperation) {
        $gitCommand = Get-Command git -ErrorAction Stop
        $networkResult = Invoke-HBWithRetry -MaximumAttempts $GitRetryAttempts -Operation {
            Invoke-HBBoundedProcess -FilePath $gitCommand.Source `
                -Arguments (@('-C', $Repository) + $Arguments) `
                -TimeoutMilliseconds ($GitTimeoutSeconds * 1000) `
                -CommandLabel "git $($Arguments[0])"
        }
        if (-not $networkResult.Succeeded) {
            throw "git $($Arguments -join ' ') fehlgeschlagen / failed nach $($networkResult.Attempts) Versuch(en): $($networkResult.Summary)"
        }
        if ($Capture) {
            return @($networkResult.StandardOutput -split '\r?\n' | Where-Object { $_ })
        }
        if ($networkResult.StandardOutput) { Write-Host $networkResult.StandardOutput }
        if ($networkResult.StandardError) { Write-Verbose $networkResult.StandardError }
        return
    }
    if ($Capture) {
        $output = & git -C $Repository @Arguments 2>$null
        if ($LASTEXITCODE -ne 0) {
            throw "git $($Arguments -join ' ') fehlgeschlagen / failed: ${Repository}"
        }
        return @($output)
    }
    & git -C $Repository @Arguments
    if ($LASTEXITCODE -ne 0) {
        throw "git $($Arguments -join ' ') fehlgeschlagen / failed: ${Repository}"
    }
}

function Get-HBGitCounts {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string] $Upstream
    )
    $raw = (Invoke-HBGit -Repository $Repository -Arguments @('rev-list', '--left-right', '--count', "HEAD...${Upstream}") -Capture) -join ' '
    $parts = $raw.Trim() -split '\s+'
    if ($parts.Count -ne 2) {
        throw "Unerwartete Ahead/Behind-Ausgabe / unexpected output: ${raw}"
    }
    return [pscustomobject]@{ Ahead = [int]$parts[0]; Behind = [int]$parts[1] }
}

function Test-HBRepository {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string] $Label,
        [switch] $AllowRepairDirty
    )

    if (-not (Test-Path -LiteralPath (Join-Path $Repository '.git'))) {
        Write-HBWarning "${Label} ist kein Git-Repository / is not a Git repository: ${Repository}"
        $script:Findings++
        return $false
    }

    $branch = (& git -C $Repository symbolic-ref --quiet --short HEAD 2>$null) -join ''
    if ($LASTEXITCODE -ne 0 -or -not $branch) {
        Write-HBWarning "${Label} hat einen detached HEAD / has a detached HEAD: ${Repository}"
        $script:Findings++
        return $false
    }
    $upstream = (& git -C $Repository rev-parse --abbrev-ref --symbolic-full-name '@{upstream}' 2>$null) -join ''
    if ($LASTEXITCODE -ne 0 -or -not $upstream) {
        Write-HBWarning "${Label} hat keinen Upstream / has no upstream: ${Repository} (${branch})"
        $script:Findings++
        return $false
    }

    $status = (& git -C $Repository status --porcelain=v1 --untracked-files=all) -join "`n"
    if ($status -and -not $AllowRepairDirty) {
        Write-HBWarning "${Label} ist nicht sauber / is dirty: ${Repository}"
        $script:Findings++
        return $false
    }

    try {
        Invoke-HBGit -Repository $Repository -Arguments @('fetch', '--prune')
    } catch {
        Write-HBWarning "${Label} Netzwerkzugriff fehlgeschlagen / network operation failed: $($_.Exception.Message)"
        $script:Findings++
        return $false
    }
    $counts = Get-HBGitCounts -Repository $Repository -Upstream $upstream
    if ($counts.Ahead -gt 0) {
        Write-HBWarning "${Label} ist $($counts.Ahead) Commit(s) voraus; kein automatischer Push / is ahead; no automatic push: ${Repository}"
        $script:Findings++
        return $false
    }
    if ($counts.Behind -gt 0) {
        if ($CheckOnly) {
            Write-HBWarning "${Label} ist $($counts.Behind) Commit(s) zurueck / is behind: ${Repository}"
            $script:Findings++
            return $false
        }
        if ($WhatIfPreference) {
            Write-Host "[WhatIf] git -C `"${Repository}`" pull --ff-only"
            return $true
        }
        try {
            Invoke-HBGit -Repository $Repository -Arguments @('pull', '--ff-only')
        } catch {
            Write-HBWarning "${Label} Fast-forward fehlgeschlagen / failed: $($_.Exception.Message)"
            $script:Findings++
            return $false
        }
    }

    $counts = Get-HBGitCounts -Repository $Repository -Upstream $upstream
    if ($counts.Ahead -ne 0 -or $counts.Behind -ne 0) {
        Write-HBWarning "${Label} ist nach der Wartung nicht synchron / is not synchronized: ${Repository} ($($counts.Ahead)/$($counts.Behind))"
        $script:Findings++
        return $false
    }
    Write-Host "OK: ${Label}: ${Repository} (${branch}, 0/0)"
    return $true
}

function Get-HBManagedRepositories {
    $lines = @(
        Invoke-HBPythonCommand -Arguments @(
            $fleetEngine, 'canonical-repositories',
            '--manifest', $ManifestPath,
            '--home-dir', $HomeDir,
            '--existing-only'
        )
    )
    if ($LASTEXITCODE -ne 0) {
        throw 'Kanonische Fleet-Ziele konnten nicht ermittelt werden / canonical fleet targets could not be resolved.'
    }

    return @($lines | ForEach-Object {
        $fields = [string]$_ -split "`t", 2
        if ($fields.Count -ne 2 -or $fields[0] -notin @('1', '2')) {
            throw "Ungueltige Fleet-Zielausgabe / invalid fleet target output: $_"
        }
        [pscustomobject]@{ Path = $fields[1]; Level = [int]$fields[0] }
    })
}

function Get-HBManagedDirtyPaths {
    $paths = [Collections.Generic.List[string]]::new()
    foreach ($repository in Get-HBManagedRepositories) {
        $repositoryRelative = [IO.Path]::GetRelativePath($HomeDir, $repository.Path).Replace('\', '/')
        $statusLines = @(& git -C $repository.Path -c core.quotePath=false status --porcelain=v1 --untracked-files=all)
        if ($LASTEXITCODE -ne 0) {
            throw "Dirty-Pfade konnten nicht gelesen werden / could not read dirty paths: $($repository.Path)"
        }
        foreach ($line in $statusLines) {
            if ([string]::IsNullOrWhiteSpace([string]$line) -or ([string]$line).Length -lt 4) { continue }
            $relative = ([string]$line).Substring(3).Trim()
            if ($relative -match ' -> ') { $relative = ($relative -split ' -> ', 2)[1] }
            $paths.Add("${repositoryRelative}/$($relative.Replace('\', '/'))")
        }
    }
    return @($paths | Sort-Object -Unique)
}

function Initialize-HBResumeState {
    if (-not (Test-Path -LiteralPath $resumeEvidenceFile -PathType Leaf)) { return }
    $evidence = Get-Content -LiteralPath $resumeEvidenceFile -Raw -Encoding UTF8 | ConvertFrom-Json
    $dirtyPaths = @(Get-HBManagedDirtyPaths)
    if ($evidence.status -eq 'Applied') {
        if ($dirtyPaths.Count -eq 0) {
            $files = @($evidence.files | ForEach-Object {
                [pscustomobject]@{
                    Path = $_.path
                    BeforeSha256 = $_.beforeSha256
                    AfterSha256 = $_.afterSha256
                }
            })
            $null = Write-HBResumeEvidence -Path $resumeEvidenceFile -RunId ([string]$evidence.runId) `
                -Phase ([string]$evidence.phase) -Files $files -Status Archived `
                -NextAction 'N/A'
            return
        }
        $validation = Test-HBResumeEvidence -Path $resumeEvidenceFile -Root $HomeDir -DirtyPaths $dirtyPaths
        if (-not $validation.Valid) {
            throw "Resume-Evidence passt nicht exakt / does not match exactly: $($validation.Reason)"
        }
        $script:ResumeAllowedPaths = @($evidence.files | ForEach-Object { [string]$_.path })
        $script:RepairApplied = $true
        Write-Host "OK: Resume-Evidence exakt validiert / exact resume evidence validated: $($validation.RunId)"
        return
    }
    if ($evidence.status -eq 'Prepared' -and $dirtyPaths.Count -gt 0) {
        throw 'Unterbrochene Reparatur ist nur teilweise belegt; manueller Review erforderlich / interrupted repair is only partially evidenced; manual review required.'
    }
}

function Get-HBPropagationPlan {
    $manifestPath = Join-Path $sourceRoot 'scripts/config/agentic-toolchain-maintenance-files.json'
    $manifest = Get-Content -LiteralPath $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
    $changes = [Collections.Generic.List[object]]::new()
    foreach ($repository in Get-HBManagedRepositories) {
        foreach ($file in @($manifest.files)) {
            $source = Join-Path $sourceRoot ([string]$file.path)
            $target = Join-Path $repository.Path ([string]$file.path)
            $different = -not (Test-Path -LiteralPath $target -PathType Leaf)
            if (-not $different) {
                $sourceHash = Get-HBGitNormalizedHash -Repository $repository.Path -Path $source `
                    -RepositoryRelativePath ([string]$file.path)
                $targetHash = Get-HBGitNormalizedHash -Repository $repository.Path -Path $target `
                    -RepositoryRelativePath ([string]$file.path)
                $different = $sourceHash -ne $targetHash
            }
            if (-not $different) { continue }
            $changes.Add([pscustomobject]@{
                Path = [IO.Path]::GetRelativePath($HomeDir, $target).Replace('\', '/')
                BeforeSha256 = Get-HBFileSha256 -Path $target
                AfterSha256 = Get-HBFileSha256 -Path $source
            })
        }
    }
    return @($changes | Sort-Object Path)
}

function Test-HBRegistry {
    $registerScript = Join-Path $sourceRoot 'scripts/register-level2-repository.ps1'
    if (-not (Test-Path -LiteralPath $registerScript -PathType Leaf)) {
        throw "Registry-Skript fehlt / missing: ${registerScript}"
    }
    $repositories = @(Get-HBManagedRepositories)
    if ($repositories.Count -eq 0) {
        if (-not (Test-Path -LiteralPath $registry -PathType Leaf)) {
            Write-HBWarning 'Keine kanonischen Fleet-Ziele fuer Registry-Aufbau gefunden / no canonical fleet targets found for registry creation.'
            $script:Findings++
            return
        }
        return
    }

    foreach ($repository in $repositories) {
        $parameters = @{
            Repo     = @($repository.Path)
            Level    = [string]$repository.Level
            Registry = $registry
            Source   = 'maintenance-discovery'
            PresetProfile = $fleetPresetProfile
        }
        if ($CheckOnly -or $WhatIfPreference) {
            $parameters.WhatIf = $true
        }
        if ($CheckOnly -or $WhatIfPreference) {
            $output = @(& $registerScript @parameters 6>&1)
            $output | ForEach-Object { Write-Host "$_" }
            if ($output -match '^\[WhatIf\] (added|updated):') {
                Write-HBWarning 'Registry-Drift gefunden / registry drift found.'
                $script:Findings++
            }
        } else {
            & $registerScript @parameters
        }
    }
    if (-not (Test-Path -LiteralPath $registry -PathType Leaf)) {
        Write-HBWarning "Registry fehlt und wuerde erzeugt / registry is missing and would be created: ${registry}"
        $script:Findings++
    }
}

function Invoke-HBPropagationCheck {
    $propagation = Join-Path $sourceRoot 'scripts/propagate-agentic-toolchain-maintenance.ps1'
    & $propagation -HomeDir $HomeDir -Registry $registry -CheckOnly
    return $LASTEXITCODE
}

function Invoke-HBPropagation {
    $propagation = Join-Path $sourceRoot 'scripts/propagate-agentic-toolchain-maintenance.ps1'
    if (-not (Test-Path -LiteralPath $propagation -PathType Leaf)) {
        throw "Propagationsskript fehlt / missing: ${propagation}"
    }

    if ($WhatIfPreference) {
        $previewOutput = @(& $propagation -HomeDir $HomeDir -Registry $registry -DryRun 6>&1)
        $previewStatus = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }
        $previewOutput | ForEach-Object { Write-Host "$_" }
        if ($previewStatus -ne 0) {
            $script:Findings++
        } elseif ($previewOutput -match 'repositories\.drifted:\s+[1-9][0-9]*') {
            $script:PreviewDrift = $true
        }
        return
    }

    $status = Invoke-HBPropagationCheck
    switch ($status) {
        0 { Write-Host 'OK: Wartungspaket ist homogen / maintenance package is homogeneous' }
        1 {
            if ($CheckOnly -or -not $RepairDrift) {
                Write-HBWarning 'Wartungspaket-Drift gefunden; fuer Reparatur -RepairDrift verwenden / use -RepairDrift to repair.'
                $script:Findings++
                return
            }
            & $propagation -HomeDir $HomeDir -Registry $registry -DryRun
            if ($LASTEXITCODE -ne 0) { throw 'Propagation-Vorschau fehlgeschlagen / preview failed.' }
            $plannedChanges = @(Get-HBPropagationPlan)
            if ($plannedChanges.Count -eq 0) {
                throw 'Propagation meldet Drift ohne aktionsfaehige Dateien / reported drift without actionable files.'
            }
            $null = Write-HBResumeEvidence -Path $resumeEvidenceFile -RunId $runId `
                -Phase 'propagation' -Files $plannedChanges -Status Prepared `
                -NextAction 'Propagation ausfuehren und Hashes verifizieren / execute propagation and verify hashes'
            & $propagation -HomeDir $HomeDir -Registry $registry
            if ($LASTEXITCODE -ne 0) { throw 'Propagation fehlgeschlagen / failed.' }
            foreach ($change in $plannedChanges) {
                $current = Get-HBFileSha256 -Path (Join-Path $HomeDir $change.Path)
                if ($current -ne $change.AfterSha256) {
                    throw "Propagation-Nachher-Hash stimmt nicht / after-hash mismatch: $($change.Path)"
                }
            }
            $null = Write-HBResumeEvidence -Path $resumeEvidenceFile -RunId $runId `
                -Phase 'propagation' -Files $plannedChanges -Status Applied `
                -NextAction 'Geaenderte Ziel-Repositories separat pruefen und liefern / review and deliver changed target repositories separately'
            if ((Invoke-HBPropagationCheck) -ne 0) { throw 'Propagation-Abschlusspruefung fehlgeschlagen / final check failed.' }
            $script:RepairApplied = $true
        }
        default { throw 'Propagation konnte nicht sicher geprueft werden / could not be checked safely.' }
    }
}

function Get-HBPresetConfig {
    param([Parameter(Mandatory)][string] $ProfileName)
    if (-not (Test-Path -LiteralPath $presetProfileCatalog -PathType Leaf)) {
        throw "Preset-Profilkatalog fehlt / missing: ${presetProfileCatalog}"
    }
    $resolved = @(
        Invoke-HBPythonCommand -Arguments @(
            $fleetEngine, 'profile',
            '--catalog', $presetProfileCatalog,
            '--source-root', $sourceRoot,
            '--profile', $ProfileName,
            '--field', 'path'
        )
    )
    if ($LASTEXITCODE -ne 0 -or $resolved.Count -ne 1) {
        throw "Unbekanntes oder ungueltiges Preset-Profil / unknown or invalid preset profile: ${ProfileName}"
    }
    return [string]$resolved[0]
}

function Get-HBPresetTargets {
    if (-not (Test-Path -LiteralPath $registry -PathType Leaf)) { return @() }
    $data = Get-Content -LiteralPath $registry -Raw | ConvertFrom-Json
    $defaultProfile = if (($data.PSObject.Properties.Name -contains 'defaultPresetProfile') -and $data.defaultPresetProfile) {
        [string]$data.defaultPresetProfile
    } else {
        'standard-eight-governance-presets'
    }
    $targets = @([pscustomobject]@{ Level = 0; Path = $sourceRoot; Profile = $defaultProfile })
    foreach ($entry in @($data.repositories)) {
        if (-not $entry.path -or $entry.level -notin @(1, 2)) { throw 'Ungueltiger Registry-Eintrag / invalid registry entry.' }
        $path = [IO.Path]::GetFullPath((Join-Path $HomeDir ([string]$entry.path)))
        if (-not $path.StartsWith($HomeDir, [StringComparison]::OrdinalIgnoreCase) -or
            -not (Test-Path -LiteralPath (Join-Path $path '.git'))) {
            throw "Registriertes Git-Repository fehlt oder liegt ausserhalb HOME / missing or outside HOME: ${path}"
        }
        $presetProfile = if (($entry.PSObject.Properties.Name -contains 'presetProfile') -and $entry.presetProfile) {
            [string]$entry.presetProfile
        } else {
            $defaultProfile
        }
        $targets += [pscustomobject]@{ Level = [int]$entry.level; Path = $path; Profile = $presetProfile }
    }
    return @($targets)
}

function Get-HBDefaultRemoteRef {
    param([Parameter(Mandatory)][string] $Repository)

    $resolved = @(
        Invoke-HBPythonCommand -Arguments @(
            $fleetEngine,
            'default-ref',
            '--repository',
            $Repository
        )
    )
    if ($LASTEXITCODE -ne 0 -or $resolved.Count -ne 1) { return $null }
    return [string]$resolved[0]
}

function New-HBPresetValidationTarget {
    param([Parameter(Mandatory)][string] $Repository)

    if ($Repository -eq $sourceRoot) {
        if (-not (Test-Path -LiteralPath (Join-Path $Repository '.specify') -PathType Container)) {
            throw "Spec Kit ist nicht initialisiert / Spec Kit is not initialized: ${Repository}"
        }
        return [pscustomobject]@{ Path = $Repository; Isolated = $false; Root = $null; Repository = $Repository; Lease = $null }
    }

    $defaultRef = Get-HBDefaultRemoteRef -Repository $Repository
    if (-not $defaultRef) {
        throw "Kanonischer origin-Default-Branch ist nicht eindeutig / canonical origin default branch is ambiguous: ${Repository}"
    }
    $currentCommit = (& git -C $Repository rev-parse HEAD 2>$null | Out-String).Trim()
    $defaultCommit = (& git -C $Repository rev-parse $defaultRef 2>$null | Out-String).Trim()
    if (
        (Test-Path -LiteralPath (Join-Path $Repository '.specify') -PathType Container) -and
        $currentCommit -and
        $currentCommit -eq $defaultCommit
    ) {
        return [pscustomobject]@{ Path = $Repository; Isolated = $false; Root = $null; Repository = $Repository; Lease = $null }
    }

    & git -C $Repository cat-file -e "${defaultRef}:.specify/presets/.registry" 2>$null
    if ($LASTEXITCODE -ne 0) {
        throw "Spec Kit ist auch auf ${defaultRef} nicht initialisiert / is not initialized on the canonical default ref: ${Repository}"
    }

    $leaseId = [Guid]::NewGuid().ToString()
    $root = Join-Path $presetWorktreeStateDir $leaseId
    $worktree = Join-Path $root 'worktree'
    $lease = Join-Path $presetWorktreeLeaseDir "${leaseId}.json"
    Invoke-HBPythonCommand -Arguments @(
        $fleetEngine, 'lease-create',
        '--state-root', $stateDir,
        '--lease', $lease,
        '--run-id', $runId,
        '--owner-pid', [string]$PID,
        '--repository', $Repository,
        '--remote-ref', $defaultRef,
        '--commit', $defaultCommit,
        '--worktree', $worktree
    ) | ForEach-Object { Write-Host $_ }
    if ($LASTEXITCODE -ne 0) {
        throw "Preset-Worktree-Lease konnte nicht erstellt werden / could not create lease: ${Repository}"
    }
    & git -C $Repository worktree add --detach $worktree $defaultRef | Out-Null
    if ($LASTEXITCODE -ne 0) {
        Invoke-HBPythonCommand -Arguments @(
            $fleetEngine, 'lease-release',
            '--state-root', $stateDir,
            '--lease', $lease,
            '--run-id', $runId
        ) | Out-Null
        throw "Temporärer Preset-Prüf-Worktree konnte nicht erstellt werden / temporary preset validation worktree failed: ${Repository}"
    }
    Write-HBInfo "Preset-Profil wird isoliert auf ${defaultRef} geprüft / validating preset profile on canonical ref"
    return [pscustomobject]@{ Path = $worktree; Isolated = $true; Root = $root; Repository = $Repository; Lease = $lease }
}

function Remove-HBPresetValidationTarget {
    param([Parameter(Mandatory)][object] $Target)

    if (-not $Target.Isolated) { return }
    Invoke-HBPythonCommand -Arguments @(
        $fleetEngine, 'lease-release',
        '--state-root', $stateDir,
        '--lease', $Target.Lease,
        '--run-id', $runId
    ) | ForEach-Object { Write-Host $_ }
    if ($LASTEXITCODE -ne 0) {
        throw 'Preset-Worktree bleibt wegen mehrdeutiger Lease-Evidence erhalten / retained because lease evidence is ambiguous.'
    }
}

function Invoke-HBPresetProfiles {
    $installer = Join-Path $sourceRoot 'scripts/install-spec-kit-governance-presets.ps1'
    if (-not (Test-Path -LiteralPath $installer -PathType Leaf)) {
        throw "Preset-Installer fehlt / missing: ${installer}"
    }
    foreach ($target in Get-HBPresetTargets) {
        $config = Get-HBPresetConfig -ProfileName $target.Profile
        if (-not $config) { continue }
        if (-not (Test-Path -LiteralPath $config -PathType Leaf)) { throw "Preset-Matrix fehlt / missing: ${config}" }
        Write-HBInfo "Preset-Profil Level-$($target.Level): $($target.Path) -> $($target.Profile)"
        $validationTarget = $null
        try {
            $validationTarget = New-HBPresetValidationTarget -Repository $target.Path
            if ($WhatIfPreference) {
                & $installer -Repo @($validationTarget.Path) -PresetConfig $config -WhatIf
                continue
            }
            & $installer -Repo @($validationTarget.Path) -PresetConfig $config -CheckOnly
            if ($LASTEXITCODE -eq 0) { continue }
            if ($validationTarget.Isolated) {
                Write-HBWarning "Preset-Profil-Drift auf dem kanonischen Default-Branch erfordert einen eigenen Branch/PR / requires a dedicated branch/PR: $($target.Path)"
                $script:Findings++
                continue
            }
            if ($CheckOnly -or -not $RepairDrift) {
                Write-HBWarning "Preset-Profil-Drift gefunden / preset profile drift found: $($target.Path)"
                $script:Findings++
                continue
            }
            & $installer -Repo @($target.Path) -PresetConfig $config -Force
            if ($LASTEXITCODE -ne 0) { throw "Preset-Reparatur fehlgeschlagen / repair failed: $($target.Path)" }
            $script:RepairApplied = $true
        } catch {
            Write-HBWarning $_.Exception.Message
            $script:Findings++
        } finally {
            if ($null -ne $validationTarget) {
                Remove-HBPresetValidationTarget -Target $validationTarget
            }
        }
    }
}

New-Item -ItemType Directory -Path (Split-Path -Parent $lockDir), $logDir, $reportDir `
    -Force -WhatIf:$false | Out-Null
try {
    New-Item -ItemType Directory -Path $lockDir -ErrorAction Stop -WhatIf:$false | Out-Null
} catch {
    $holder = if (Test-Path (Join-Path $lockDir 'pid')) { Get-Content (Join-Path $lockDir 'pid') -Raw } else { 'unbekannt / unknown' }
    Write-Host "Fehler / Error: Wartung laeuft bereits (PID $($holder.Trim())) / maintenance already running." -ForegroundColor Red
    exit 2
}
Set-Content -LiteralPath (Join-Path $lockDir 'pid') -Value $PID -WhatIf:$false
$logFile = Join-Path $logDir "agentic-workspace-$(Get-Date -Format 'yyyyMMdd-HHmmss').log"

try {
    # Lock, log and report are mode-independent control evidence, not a
    # previewed workspace mutation.
    Start-Transcript -Path $logFile -Append -WhatIf:$false | Out-Null
    $transcriptStarted = $true
    $env:HOME = $HomeDir
    $env:USERPROFILE = $HomeDir
    $env:HB_GIT_RETRY_ATTEMPTS = [string]$GitRetryAttempts
    $env:HB_GIT_TIMEOUT_SECONDS = [string]$GitTimeoutSeconds

    $mode = $maintenanceMode.Name
    if ($ScriptsOnly) { $mode += ', scripts-only' }
    Write-Host 'Agentic workspace maintenance'
    Write-Host "Mode / Modus: ${mode}"
    Write-Host "Level-0: ${sourceRoot}"
    Write-Host "Home: ${HomeDir}"
    Write-Host "Run-ID: ${runId}"
    Write-HBMaintenanceEvent -EventType 'run-started' -Status 'RUNNING' `
        -MessageDe 'Wartung gestartet.' `
        -MessageEn 'Maintenance started.' `
        -DetailsJson (@{ mode = $maintenanceMode.FleetMode } | ConvertTo-Json -Compress)

    try {
        Initialize-HBResumeState
    } catch {
        Write-HBEarlyFailureReport -Status BLOCKED -ExitCode 1 `
            -Summary $_.Exception.Message `
            -NextAction 'Resume-Evidence und alle Dirty-Pfade manuell pruefen / manually review resume evidence and every dirty path'
        throw
    }

    Write-HBInfo 'Verwaiste eigene Preset-Worktrees prüfen / Check owned orphaned preset worktrees'
    Invoke-HBPythonCommand -Arguments @(
        $fleetEngine, 'lease-recover',
        '--state-root', $stateDir,
        '--lease-dir', $presetWorktreeLeaseDir
    ) | ForEach-Object { Write-Host $_ }
    $leaseRecoveryReady = $LASTEXITCODE -eq 0
    if (-not $leaseRecoveryReady) {
        $script:Findings++
        Write-HBWarning 'Mehrdeutige Lease-Evidence blockiert mutierende Folgephasen / ambiguous lease evidence blocks later mutations.'
    }

    Write-HBInfo 'Soll-Flotte pruefen und sicher warten / Check and safely maintain desired fleet'
    Start-HBMaintenanceEventPhase -PhaseId 'fleet'
    $fleetStatus = Invoke-HBFleetContract
    switch ($fleetStatus) {
        0 {
            Write-HBMaintenanceEvent -EventType 'phase-completed' -Status 'PASSED' `
                -PhaseId 'fleet' `
                -MessageDe 'Flottenprüfung abgeschlossen.' `
                -MessageEn 'Fleet check completed.' `
                -DetailsJson '{"exitCode":0}'
        }
        1 {
            Write-HBMaintenanceEvent -EventType 'phase-completed' -Status 'BLOCKED' `
                -PhaseId 'fleet' `
                -MessageDe 'Flottenprüfung mit Befund abgeschlossen.' `
                -MessageEn 'Fleet check completed with findings.' `
                -DetailsJson '{"exitCode":1}'
            $script:Findings++
        }
        default {
            Add-HBReportStage -StageId 'fleet' -Status Failed -ExitCode 2 `
                -Summary 'Fleet-Vertrag fehlgeschlagen / fleet contract failed' `
                -NextAction 'Manifest und Log pruefen / review manifest and log'
            throw 'Fleet-Vertrag fehlgeschlagen / fleet contract failed.'
        }
    }
    $fleetReport = Get-Content -LiteralPath $reportFile -Raw -Encoding UTF8 | ConvertFrom-Json
    $fleetReady = $fleetReport.mutationBarrier.fleetReady -eq $true
    $level0Row = @($fleetReport.targets | Where-Object { $_.targetId -eq 'level0' }) |
        Select-Object -First 1
    $level0Passed = $null -ne $level0Row -and $level0Row.result -eq 'Pass'
    Start-HBMaintenanceEventPhase -PhaseId 'level0'
    Add-HBReportStage -StageId 'level0' -Status $(if ($level0Passed) { 'Passed' } else { 'Blocked' }) `
        -ExitCode $(if ($level0Passed) { 0 } else { 1 }) `
        -Summary 'Level-0-Pruefung / Level-0 check' `
        -NextAction $(if ($level0Passed) { 'N/A' } else { 'Branch und Upstream pruefen / review branch and upstream' })

    $homeStatus = 'Skipped'
    if (($fleetReady -and $leaseRecoveryReady) -or $CheckOnly) {
        Start-HBMaintenanceEventPhase -PhaseId 'home-sync'
        Write-HBInfo 'Lokale Home-Baseline synchronisieren / Synchronize local home baseline'
        $findingsBefore = $script:Findings
        $syncScript = Join-Path $sourceRoot 'scripts/sync-home.ps1'
        $homeInvocationStatus = 0
        switch ($maintenanceMode.Name) {
            'CheckOnly' {
                Test-HBHomeSync
            }
            'Preview' {
                & $syncScript -NoPull -WhatIf
                $homeInvocationStatus = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }
            }
            'Update' {
                & $syncScript -NoPull
                $homeInvocationStatus = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }
            }
        }
        if ($homeInvocationStatus -ne 0) { throw 'sync-home fehlgeschlagen / failed.' }
        $homeStatus = if ($script:Findings -gt $findingsBefore) { 'Blocked' } else { 'Passed' }
    }
    Add-HBReportStage -StageId 'home-sync' -Status $homeStatus `
        -ExitCode $(if ($homeStatus -eq 'Blocked') { 1 } else { 0 }) `
        -Summary 'Home-Sync / home sync' `
        -NextAction $(if ($homeStatus -eq 'Skipped') { 'Nach Level-0-Freigabe erneut ausfuehren / rerun after Level-0 passes' } else { 'N/A' })
    if ($analyzerAvailable) {
        Add-HBReportStage -StageId 'prerequisites' -Status Passed -ExitCode 0 `
            -Summary "Python 3 und PSScriptAnalyzer ${requiredAnalyzerVersion} validiert / validated"
    } else {
        Add-HBReportStage -StageId 'prerequisites' -Status Blocked -ExitCode 1 `
            -Summary "PSScriptAnalyzer ${requiredAnalyzerVersion} fehlt / is missing" `
            -NextAction 'pwsh -NoProfile -File scripts/maintain-powershell-modules.ps1 ausfuehren / run the module maintainer'
        $script:Findings++
    }

    Start-HBMaintenanceEventPhase -PhaseId 'registry'
    Write-HBInfo 'Level-1/Level-2 Registry pruefen / Check Level-1/Level-2 registry'
    $findingsBefore = $script:Findings
    Test-HBRegistry
    $registrySafe = $false
    if (Test-Path -LiteralPath $registry -PathType Leaf) {
        Invoke-HBPythonCommand -Arguments @(
            $fleetEngine, 'registry', '--manifest', $ManifestPath, '--registry', $registry
        ) |
            ForEach-Object { Write-Host $_ }
        $registryStatus = $LASTEXITCODE
        $registrySafe = $registryStatus -eq 0
        if (-not $registrySafe) { $script:Findings++ }
    }
    if ($script:Findings -gt $findingsBefore -or -not $registrySafe) {
        Add-HBReportStage -StageId 'registry' -Status Blocked -ExitCode 1 `
            -Summary 'Registry-Pruefung mit Befund / registry check has findings' `
            -NextAction 'Registry-Befund beheben / resolve registry finding'
    } else {
        Add-HBReportStage -StageId 'registry' -Status Passed -ExitCode 0 `
            -Summary "Registry-Pruefung abgeschlossen / completed; source=scripts/config/spec-kit-preset-profiles.json; profile=${fleetPresetProfile}; presets=${fleetPresetCount}"
    }

    if ((($fleetReady -and $leaseRecoveryReady) -or $CheckOnly) -and ($script:Findings -eq 0 -or $CheckOnly) -and $registrySafe) {
        Start-HBMaintenanceEventPhase -PhaseId 'propagation'
        Write-HBInfo 'Kanonisches Wartungspaket pruefen / Check canonical maintenance package'
        if (Test-Path -LiteralPath $registry -PathType Leaf) {
            $findingsBefore = $script:Findings
            Invoke-HBPropagation
            if ($script:Findings -gt $findingsBefore) {
                Add-HBReportStage -StageId 'propagation' -Status Blocked -ExitCode 1 `
                    -Summary 'Wartungspaket-Drift / maintenance package drift' `
                    -NextAction 'Drift separat pruefen / review drift separately'
            } elseif ($WhatIfPreference -and $script:PreviewDrift) {
                Add-HBReportStage -StageId 'propagation' -Status Warning -ExitCode 1 `
                    -Summary 'Wartungspaket-Drift vorhergesagt / maintenance package drift predicted' `
                    -NextAction 'Mit -RepairDrift lokal reparieren / repair locally with -RepairDrift'
            } else {
                Add-HBReportStage -StageId 'propagation' -Status Passed -ExitCode 0 `
                    -Summary 'Wartungspaket geprueft / maintenance package checked'
            }
        }
    } else {
        Add-HBReportStage -StageId 'propagation' -Status Skipped -ExitCode 0 `
            -Summary 'Propagation wegen Vorbedingung uebersprungen / skipped by prerequisite' `
            -NextAction 'Blockierende Vorbedingung beheben / resolve blocking prerequisite'
    }

    if ((($fleetReady -and $leaseRecoveryReady) -or $CheckOnly) -and ($script:Findings -eq 0 -or $CheckOnly) -and $registrySafe -and $leaseRecoveryReady) {
        Start-HBMaintenanceEventPhase -PhaseId 'preset-profiles'
        Write-HBInfo 'Registry-gesteuerte Preset-Profile pruefen / Check registry-controlled preset profiles'
        $findingsBefore = $script:Findings
        Invoke-HBPresetProfiles
        if ($script:Findings -gt $findingsBefore) {
            Add-HBReportStage -StageId 'preset-profiles' -Status Blocked -ExitCode 1 `
                -Summary 'Preset-Profil-Befund / preset profile finding' `
                -NextAction 'Preset-Drift separat beheben / resolve preset drift separately'
        } else {
            Add-HBReportStage -StageId 'preset-profiles' -Status Passed -ExitCode 0 `
                -Summary 'Preset-Profile geprueft / preset profiles checked'
        }
    } else {
        Add-HBReportStage -StageId 'preset-profiles' -Status Skipped -ExitCode 0 `
            -Summary 'Preset-Pruefung wegen Vorbedingung uebersprungen / skipped by prerequisite' `
            -NextAction 'Blockierende Vorbedingung beheben / resolve blocking prerequisite'
    }

    if ((($fleetReady -and $leaseRecoveryReady) -or $CheckOnly) -and ($script:Findings -eq 0 -or $CheckOnly) -and -not $ScriptsOnly) {
        Start-HBMaintenanceEventPhase -PhaseId 'toolchain'
        Write-HBInfo 'Maschinen-Toolchain pflegen / Maintain machine toolchain'
        $maintenance = Join-Path $sourceRoot 'scripts/maintain-agentic-winget-apps.ps1'
        $parameters = @{}
        if ($CheckOnly) { $parameters.CompareOnly = $true }
        if ($WhatIfPreference) { $parameters.WhatIf = $true }
        $parameters.ProcessTimeoutSeconds = $WinGetTimeoutSeconds
        $optionalDeferred = $IncludeOptional -and -not $AllowAdminPrompts
        if ($IncludeOptional -and $AllowAdminPrompts) { $parameters.IncludeOptional = $true }
        if ($optionalDeferred) {
            Write-HBWarning 'DEFERRED_ADMIN_REQUIRED: optionale Pakete benoetigen aktuelle Admin-Prompt-Autoritaet / optional packages require current authority.'
        }
        $env:HB_ALLOW_ADMIN_PROMPTS = if ($AllowAdminPrompts) { '1' } else { '0' }
        & $maintenance @parameters
        $toolchainExit = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }
        if ($toolchainExit -eq 75 -or $optionalDeferred) {
            $script:Findings++
            Add-HBReportStage -StageId 'toolchain' -Status Blocked -ExitCode 1 `
                -Summary 'DEFERRED_ADMIN_REQUIRED' `
                -NextAction 'Mit aktueller Autoritaet erneut ausfuehren / rerun with current authority'
        } elseif ($toolchainExit -ne 0) {
            throw 'WinGet-Wartung fehlgeschlagen / maintenance failed.'
        } else {
            Add-HBReportStage -StageId 'toolchain' -Status Passed -ExitCode 0 `
                -Summary 'Toolchain-Wartung abgeschlossen / toolchain maintenance completed'
        }
    } else {
        Add-HBReportStage -StageId 'toolchain' -Status Skipped -ExitCode 0 `
            -Summary 'Toolchain durch Modus oder Vorbedingung uebersprungen / skipped by mode or prerequisite'
    }

    if ($script:Findings -eq 0) {
        Start-HBMaintenanceEventPhase -PhaseId 'final'
        Write-HBInfo 'Abschlusspruefung / Final verification'
        if ($WhatIfPreference) {
            $findingsBefore = $script:Findings
            Test-HBHomeSync
            if ($script:Findings -gt $findingsBefore) {
                Add-HBReportStage -StageId 'home-sync' -Status Blocked -ExitCode 1 `
                    -Summary 'Home-Sync-Drift vorhergesagt / home sync drift predicted' `
                    -NextAction 'Echten Home-Sync nach dem Merge ausfuehren / run actual home sync after merge'
            }
        } else {
            Test-HBHomeSync
            if ((Invoke-HBPropagationCheck) -ne 0) { throw 'Abschliessende Propagationspruefung fehlgeschlagen / final propagation check failed.' }
        }
        [void](Test-HBRepository -Repository $sourceRoot -Label 'Level-0')
        foreach ($repo in Get-HBManagedRepositories) {
            [void](Test-HBRepository -Repository $repo.Path -Label "Level-$($repo.Level)" -AllowRepairDirty:$script:RepairApplied)
        }
        if ($WhatIfPreference -and $script:PreviewDrift) {
            $script:Findings++
        }
    }

    if ($script:Findings -gt 0) {
        Add-HBReportStage -StageId 'final' -Status Blocked -ExitCode 1 `
            -Summary 'Wartung mit offenen Befunden / maintenance has open findings' `
            -NextAction 'Befunde im Bericht beheben / resolve report findings'
        Write-HBWarning "Wartung mit $($script:Findings) offenem Befund beendet / maintenance ended with open finding(s)."
        $exitCode = 1
    } elseif ($script:RepairApplied) {
        Add-HBReportStage -StageId 'final' -Status Warning -ExitCode 3 `
            -Summary 'Drift lokal repariert / drift repaired locally' `
            -NextAction 'Aenderungen separat pruefen / review changes separately'
        Write-HBWarning 'Drift wurde lokal repariert. Betroffene Repositories separat pruefen, committen und pushen.'
        Write-HBWarning 'Drift was repaired locally. Review, commit, and push affected repositories separately.'
        $exitCode = 3
    } else {
        Add-HBReportStage -StageId 'final' -Status Passed -ExitCode 0 `
            -Summary 'Wartung abgeschlossen / maintenance completed'
        Write-Host 'OK: Wartung abgeschlossen / maintenance completed'
    }
} catch {
    Write-Host "Fehler / Error: $($_.Exception.Message)" -ForegroundColor Red
    $exitCode = 2
    if (Test-Path -LiteralPath $reportFile -PathType Leaf) {
        try {
            $existingReport = Get-Content -LiteralPath $reportFile -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($existingReport.runId -eq $runId -and $existingReport.overallStatus -ne 'BLOCKED') {
                Add-HBReportStage -StageId 'final' -Status Failed -ExitCode 2 `
                    -Summary 'Wartung fehlgeschlagen / maintenance failed' `
                    -NextAction 'Log und Bericht pruefen / review log and report'
            }
        } catch {
            $exitCode = 2
        }
    }
} finally {
    if ($transcriptStarted) { Stop-Transcript -WhatIf:$false | Out-Null }
    if (Test-Path -LiteralPath $lockDir) {
        Remove-Item -LiteralPath $lockDir -Recurse -Force -WhatIf:$false
    }
    Write-Host "Log / log: ${logFile}"
    if (Test-Path -LiteralPath $reportFile -PathType Leaf) {
        try {
            $preFinalReport = Get-Content -LiteralPath $reportFile -Raw -Encoding UTF8 | ConvertFrom-Json
            $finalizedProperty = $preFinalReport.PSObject.Properties['finalized']
            if ($null -eq $finalizedProperty -or $finalizedProperty.Value -ne $true) {
                $finalStatus = switch ($exitCode) {
                    0 { 'Passed' }
                    1 { 'Blocked' }
                    2 { 'Failed' }
                    3 { 'Warning' }
                    default { 'Failed' }
                }
                Invoke-HBPythonCommand -Arguments @(
                    $fleetEngine, 'finalize',
                    '--report', $reportFile,
                    '--stage-id', 'final',
                    '--status', $finalStatus,
                    '--exit-code', [string]$exitCode,
                    '--summary', 'Wartung abgeschlossen / maintenance completed',
                    '--next-action', $(if ($exitCode -eq 0) {
                        'N/A'
                    } else {
                        'Bericht und Log prüfen / review report and log'
                    })
                ) | Out-Null
                if ($LASTEXITCODE -ne 0) {
                    throw 'Run-Bericht konnte nicht finalisiert werden / run report finalization failed.'
                }
            }
            $terminalReport = Get-Content -LiteralPath $reportFile -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($terminalReport.runId -ne $runId) {
                throw 'Report-Run-ID stimmt nicht mit dem aktuellen Lauf ueberein / does not match the current run.'
            }
            $exitCode = [int]$terminalReport.exitCode
            Start-HBMaintenanceEventPhase -PhaseId 'final'
            Write-HBMaintenanceEvent -EventType 'run-completed' `
                -Status (ConvertTo-HBEventStatus -Status $(switch ($exitCode) {
                    0 { 'Passed' }
                    1 { 'Blocked' }
                    2 { 'Failed' }
                    3 { 'Warning' }
                    default { 'Failed' }
                })) `
                -PhaseId 'final' `
                -MessageDe 'Wartung abgeschlossen.' `
                -MessageEn 'Maintenance completed.' `
                -DetailsJson (@{
                    reportPath = $reportFile
                    exitCode = $exitCode
                    overallStatus = [string]$terminalReport.overallStatus
                } | ConvertTo-Json -Compress)
            Write-Host "Status / status: $($terminalReport.overallStatus) (exit ${exitCode})"
        } catch {
            Write-Host "Fehler / Error: $($_.Exception.Message)" -ForegroundColor Red
            $exitCode = 2
        }
        Write-Host "Report / Bericht: ${reportFile}"
    }
}

exit (Get-HBCanonicalExitCode -Status $(switch ($exitCode) {
    0 { 'SUCCESS' }
    1 { 'PARTIAL' }
    2 { 'FAILED' }
    3 { 'REPAIRED' }
    default { 'FAILED' }
}))
