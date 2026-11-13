#Requires -Version 7.0
<#
.SYNOPSIS
    Validiert und koordiniert parallele autonome Spec-Kit-Laeufe.

.DESCRIPTION
    DE: Verwendet ein versioniertes Campaign-Manifest und lokale Runner-Profile,
    erstellt getrennte Git-Worktrees, begrenzt Parallelitaet auf hoechstens
    drei Worker und fuehrt Stop, Resume und Konsolidierung evidenzbasiert aus.

    EN: Uses a versioned campaign manifest and local runner profiles, creates
    isolated Git worktrees, limits concurrency to at most three workers, and
    performs stop, resume, and consolidation from evidence.

.PARAMETER Action
    Validate, Start, Status, Stop, Resume oder Consolidate.

.PARAMETER Manifest
    Pfad zum parallel-campaign.json.

.PARAMETER RunnerConfig
    Lokale JSON-Datei mit Executable/Argument-Array-Profilen.

.PARAMETER State
    Optionaler Pfad zur Campaign-State-Datei direkt im RuntimeRoot.

.PARAMETER RuntimeRoot
    Nicht versioniertes, von Worker-Repositories getrenntes Verzeichnis fuer
    Campaign-State, Worktrees, Logs, Locks und Ergebnisse.

.PARAMETER SelectedWorker
    Menschlich ausgewaehlter Worker fuer AlternativeSolutions.

.PARAMETER Merge
    Autorisiert bei Consolidate oder Resume die konfigurierte Merge-Command.

.PARAMETER OutputFormat
    Ausgabeformat fuer Status: Json oder barrierearmer Text.

.EXAMPLE
    pwsh -NoProfile -File scripts/orchestrate-parallel-autonomous-runs.ps1 -Action Validate -Manifest campaign.json -RunnerConfig runners.json

.EXAMPLE
    pwsh -NoProfile -File scripts/orchestrate-parallel-autonomous-runs.ps1 -Action Start -Manifest campaign.json -RunnerConfig runners.json
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateSet('Validate', 'Start', 'Status', 'Stop', 'Resume', 'Consolidate')]
    [string] $Action,

    [Parameter(Mandatory)]
    [string] $Manifest,

    [string] $RunnerConfig = '',
    [string] $State = '',
    [string] $RuntimeRoot = '',
    [string] $SelectedWorker = '',
    [switch] $Merge,
    [ValidateSet('Json', 'Text')]
    [string] $OutputFormat = 'Json'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$script:SupportedSchemaVersions = @('1.0', '1.1', '1.2')
$script:UndeclaredRunnerMetadata = 'Agent-Standard/nicht deklariert'

function Assert-PARCondition {
    param(
        [Parameter(Mandatory)][bool] $Condition,
        [Parameter(Mandatory)][string] $Message
    )

    if (-not $Condition) {
        throw $Message
    }
}

function Read-PARJson {
    param([Parameter(Mandatory)][string] $Path)

    Assert-PARCondition (Test-Path -LiteralPath $Path -PathType Leaf) "JSON-Datei fehlt: $Path"
    return Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -AsHashtable
}

function Assert-PARSchemaVersion {
    param(
        [Parameter(Mandatory)][hashtable] $Data,
        [Parameter(Mandatory)][string] $ContractName
    )

    Assert-PARCondition $Data.ContainsKey('schemaVersion') "$ContractName schemaVersion fehlt."
    Assert-PARCondition ($script:SupportedSchemaVersions -contains [string] $Data.schemaVersion) `
        "$ContractName schemaVersion muss 1.0, 1.1 oder 1.2 sein."
}

function Write-PARJsonAtomic {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)] $Value
    )

    $parent = Split-Path -Parent $Path
    if ($parent) {
        [void](New-Item -ItemType Directory -Path $parent -Force)
    }
    $temp = "${Path}.tmp.$PID"
    $Value | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $temp -Encoding utf8NoBOM
    Move-Item -LiteralPath $temp -Destination $Path -Force
}

function Enter-PARCampaignLock {
    param([Parameter(Mandatory)][string] $RuntimePath)

    [void](New-Item -ItemType Directory -Path $RuntimePath -Force)
    $lockPath = Join-Path $RuntimePath 'campaign.lock'
    $ownerPath = Join-Path $lockPath 'owner.json'
    for ($attempt = 1; $attempt -le 3; $attempt++) {
        $created = $false
        try {
            [void](New-Item -ItemType Directory -Path $lockPath -ErrorAction Stop)
            $created = $true
        } catch {
            $created = $false
        }

        if ($created) {
            $lockId = [guid]::NewGuid().ToString()
            $processStart = (Get-Process -Id $PID).StartTime.ToUniversalTime()
            try {
                Write-PARJsonAtomic $ownerPath ([ordered]@{
                    lockId = $lockId
                    processId = $PID
                    processStartTimeUtc = $processStart.ToString('o')
                    processStartTimeUtcTicks = $processStart.Ticks
                    acquiredAt = [DateTime]::UtcNow.ToString('o')
                })
            } catch {
                Remove-Item -LiteralPath $lockPath -Recurse -Force -ErrorAction SilentlyContinue
                throw
            }
            return @{
                Path = $lockPath
                LockId = $lockId
            }
        }

        if (-not (Test-Path -LiteralPath $lockPath -PathType Container)) {
            Start-Sleep -Milliseconds 25
            continue
        }

        $ownerIsActive = $false
        $ownerIsReadable = $false
        if (Test-Path -LiteralPath $ownerPath -PathType Leaf) {
            try {
                $owner = Read-PARJson $ownerPath
                $ownerIsReadable = $true
                $ownerProcess = Get-Process -Id ([int] $owner.processId) -ErrorAction SilentlyContinue
                if ($null -ne $ownerProcess) {
                    $actualStartTicks = $ownerProcess.StartTime.ToUniversalTime().Ticks
                    if ($owner.ContainsKey('processStartTimeUtcTicks')) {
                        $recordedStartTicks = [long] $owner.processStartTimeUtcTicks
                        $ownerIsActive = [Math]::Abs($actualStartTicks - $recordedStartTicks) -lt
                            [TimeSpan]::TicksPerSecond
                    } else {
                        $ownerIsActive = $true
                    }
                }
            } catch {
                $ownerIsReadable = $false
            }
        }
        if (-not $ownerIsReadable) {
            try {
                $lockItem = Get-Item -LiteralPath $lockPath -ErrorAction Stop
            } catch {
                Start-Sleep -Milliseconds 25
                continue
            }
            $lockAge = [DateTime]::UtcNow - $lockItem.LastWriteTimeUtc
            $ownerIsActive = $lockAge.TotalSeconds -lt 30
        }
        if ($ownerIsActive) {
            throw "Campaign-Lock ist bereits aktiv: $lockPath"
        }

        # Rename first so only one contender can reclaim a stale lock.
        $stalePath = "$lockPath.stale.$([guid]::NewGuid().ToString('N'))"
        try {
            Move-Item -LiteralPath $lockPath -Destination $stalePath -ErrorAction Stop
            Remove-Item -LiteralPath $stalePath -Recurse -Force
        } catch {
            Start-Sleep -Milliseconds 50
        }
    }
    throw "Campaign-Lock konnte nicht sicher uebernommen werden: $lockPath"
}

function Exit-PARCampaignLock {
    param([Parameter(Mandatory)][hashtable] $Lock)

    $ownerPath = Join-Path ([string] $Lock.Path) 'owner.json'
    if (-not (Test-Path -LiteralPath $ownerPath -PathType Leaf)) {
        return
    }
    try {
        $owner = Read-PARJson $ownerPath
        if ([string] $owner.lockId -eq [string] $Lock.LockId) {
            Remove-Item -LiteralPath ([string] $Lock.Path) -Recurse -Force
        }
    } catch {
        return
    }
}

function Get-PARSha256 {
    param([Parameter(Mandatory)][string] $Path)
    return (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToLowerInvariant()
}

function Get-PARNormalizedTextSha256 {
    param([Parameter(Mandatory)][string] $Path)
    $bytes = [IO.File]::ReadAllBytes($Path)
    $offset = if ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and
        $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) { 3 } else { 0 }
    $utf8 = [Text.UTF8Encoding]::new($false, $true)
    $text = $utf8.GetString($bytes, $offset, $bytes.Length - $offset)
    Assert-PARCondition (-not $text.Contains([char] 0)) "Intake ist binaer: $Path"
    $normalized = $text.Replace("`r`n", "`n").Replace("`r", "`n")
    $hash = [Security.Cryptography.SHA256]::HashData($utf8.GetBytes($normalized))
    return [Convert]::ToHexString($hash).ToLowerInvariant()
}

function Test-PARUuid {
    param([Parameter(Mandatory)][string] $Value)
    $parsed = [guid]::Empty
    return [guid]::TryParse($Value, [ref] $parsed) -and $parsed -ne [guid]::Empty
}

function Invoke-PARGit {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string[]] $Arguments,
        [switch] $AllowFailure
    )

    $output = @(& git -C $Repository @Arguments 2>&1)
    $code = $LASTEXITCODE
    if ($code -ne 0 -and -not $AllowFailure) {
        throw "git -C $Repository $($Arguments -join ' ') fehlgeschlagen: $(@($output) -join [Environment]::NewLine)"
    }
    return @{
        ExitCode = $code
        Output = @($output)
    }
}

function Invoke-PARProfileCommand {
    param(
        [Parameter(Mandatory)][hashtable] $CommandProfile,
        [Parameter(Mandatory)][hashtable] $Values,
        [Parameter(Mandatory)][string] $WorkingDirectory
    )

    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $CommandProfile.executable)) 'Profile executable fehlt.'
    Assert-PARCondition ($CommandProfile.arguments -is [object[]]) 'Profile arguments muss ein Array sein.'
    $arguments = ConvertTo-PARArgumentList @($CommandProfile.arguments) $Values
    $outputRoot = Join-Path ([IO.Path]::GetTempPath()) "parallel-profile-$([guid]::NewGuid().ToString('N'))"
    [void](New-Item -ItemType Directory -Path $outputRoot)
    $processInfo = $null
    try {
        Push-Location -LiteralPath $WorkingDirectory
        try {
            $processInfo = Invoke-PARProcessAsync ([string] $CommandProfile.executable) $arguments `
                $WorkingDirectory (Join-Path $outputRoot 'stdout.log') (Join-Path $outputRoot 'stderr.log')
        } finally {
            Pop-Location
        }
        $deadline = [DateTime]::UtcNow.AddMinutes(5)
        while (-not $processInfo.Process.HasExited -or
            -not $processInfo.StdoutTask.IsCompleted -or -not $processInfo.StderrTask.IsCompleted) {
            if ($processInfo.StdoutStream.Length -gt 4MB -or $processInfo.StderrStream.Length -gt 4MB -or
                [DateTime]::UtcNow -gt $deadline) {
                if (-not $processInfo.Process.HasExited) { $processInfo.Process.Kill($true) }
                throw 'Profilbefehl ueberschritt das Ausgabe- oder Zeitlimit (4 MiB je Stream, 5 Minuten).'
            }
            Start-Sleep -Milliseconds 100
        }
        [void] $processInfo.StdoutTask.GetAwaiter().GetResult()
        [void] $processInfo.StderrTask.GetAwaiter().GetResult()
        return @{ ExitCode = $processInfo.Process.ExitCode; Output = @() }
    } finally {
        if ($null -ne $processInfo) {
            if (-not $processInfo.Process.HasExited) {
                try { $processInfo.Process.Kill($true) } catch [InvalidOperationException] { Write-Verbose 'Prozess beendete sich bereits waehrend der Bereinigung.' }
                [void] $processInfo.Process.WaitForExit(5000)
            }
            $processInfo.StdoutStream.Dispose()
            $processInfo.StderrStream.Dispose()
            $processInfo.Process.Dispose()
        }
        Remove-Item -LiteralPath $outputRoot -Recurse -Force -ErrorAction SilentlyContinue
    }
}

function Resolve-PARRepository {
    param(
        [Parameter(Mandatory)][string] $Value,
        [Parameter(Mandatory)][string] $ManifestDirectory
    )

    if ([IO.Path]::IsPathRooted($Value)) {
        return [IO.Path]::GetFullPath($Value)
    }
    return [IO.Path]::GetFullPath((Join-Path $ManifestDirectory $Value))
}

function Get-PARRemoteRepositoryIdentity {
    param([Parameter(Mandatory)][string] $Repository)

    $remote = Invoke-PARGit $Repository @('remote', 'get-url', 'origin') -AllowFailure
    Assert-PARCondition ($remote.ExitCode -eq 0 -and $remote.Output.Count -eq 1) `
        "origin-Remote fuer PR-Identitaet fehlt: $Repository"
    $url = [string] $remote.Output[0]
    $hostName = ''
    $repositoryPath = ''
    if ($url -match '^[A-Za-z][A-Za-z0-9+.-]*://') {
        $uri = $null
        Assert-PARCondition ([Uri]::TryCreate($url, [UriKind]::Absolute, [ref] $uri) -and
            $uri.Scheme -in @('https', 'ssh')) "origin-Remote hat keine sichere PR-Adresse: $url"
        $hostName = $uri.Host
        $repositoryPath = $uri.AbsolutePath.Trim('/')
    } else {
        Assert-PARCondition ($url -match '^(?:[^@/]+@)?(?<host>[^:/]+):(?<path>[^\s]+)$') `
            "origin-Remote hat keine sichere PR-Adresse: $url"
        $hostName = $Matches.host
        $repositoryPath = $Matches.path
    }
    $repositoryPath = $repositoryPath.TrimEnd('/') -replace '\.git$', ''
    $parts = @($repositoryPath -split '/')
    Assert-PARCondition ($hostName -match '^[A-Za-z0-9.-]+$' -and $parts.Count -eq 2 -and
        @($parts | Where-Object { $_ -notmatch '^[A-Za-z0-9_.-]+$' }).Count -eq 0) `
        "origin-Remote ist kein kanonisches Repository: $url"
    return "$($hostName.ToLowerInvariant())/$($parts[0].ToLowerInvariant())/$($parts[1].ToLowerInvariant())"
}

function Get-PARExpectedPrIdentity {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $Provider
    )

    $repository = Resolve-PARRepository ([string] $Worker.repository) $ManifestDirectory
    $headRepository = if ($Provider -eq 'Local Fixture') {
        [IO.Path]::GetFullPath($repository)
    } else {
        Get-PARRemoteRepositoryIdentity $repository
    }
    if ($Worker.ContainsKey('prHeadRepository')) {
        Assert-PARCondition ([string] $Worker.prHeadRepository -eq $headRepository) `
            "PR-Head-Repository stimmt nicht mit origin ueberein: $($Worker.workerId)"
    }
    $baseRepository = if ($Worker.ContainsKey('prBaseRepository')) {
        [string] $Worker.prBaseRepository
    } else { $headRepository }
    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($baseRepository)) `
        "PR-Zielrepository fehlt: $($Worker.workerId)"
    if ($Provider -ne 'Local Fixture') {
        Assert-PARCondition ($baseRepository -match '^[A-Za-z0-9.-]+/[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+$') `
            "PR-Zielrepository muss host/owner/repo sein: $($Worker.workerId)"
        $baseRepository = $baseRepository.ToLowerInvariant()
    }
    $baseWorkerId = if ($Worker.ContainsKey('baseWorkerId')) { [string] $Worker.baseWorkerId } else { '' }
    $baseRef = if (-not [string]::IsNullOrWhiteSpace($baseWorkerId)) {
        [string] (Get-PARCampaignWorker $Campaign $baseWorkerId).branch
    } elseif ($Worker.ContainsKey('prBaseRef')) {
        [string] $Worker.prBaseRef
    } else {
        $sourceRef = [string] $Worker.baseRef
        if ($sourceRef -match '^origin/(.+)$') { $Matches[1] }
        elseif ($sourceRef -match '^refs/heads/(.+)$') { $Matches[1] }
        elseif ($sourceRef -match '^[0-9a-fA-F]{40}$' -or $sourceRef -match '^refs/') {
            throw "PR-Zielbranch muss fuer Base-Commit/Tag explizit als prBaseRef deklariert sein: $($Worker.workerId)"
        } else { $sourceRef }
    }
    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($baseRef)) `
        "PR-Zielbranch fehlt: $($Worker.workerId)"
    return @{ headRepository = $headRepository; headRefName = [string] $Worker.branch;
        baseRepository = $baseRepository; baseRefName = $baseRef }
}

function Resolve-PARPhysicalPath {
    param([Parameter(Mandatory)][string] $Path, [int] $Depth = 0)

    Assert-PARCondition ($Depth -lt 32) 'Symlink-Pfad enthaelt zu viele Weiterleitungen.'
    $full = [IO.Path]::GetFullPath($Path)
    $root = [IO.Path]::GetPathRoot($full)
    $current = $root
    foreach ($segment in $full.Substring($root.Length).Split([char[]]@('/', '\'), [StringSplitOptions]::RemoveEmptyEntries)) {
        $current = [IO.Path]::Combine($current, $segment)
        $entry = Get-Item -LiteralPath $current -Force -ErrorAction Stop
        if (($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) {
            $resolved = $entry.ResolveLinkTarget($true)
            Assert-PARCondition ($null -ne $resolved) "Symlink kann nicht aufgeloest werden: $current"
            # A final link target can still contain links in its parent path.
            # Rewalk that entire target before appending later path segments.
            $current = Resolve-PARPhysicalPath $resolved.FullName ($Depth + 1)
        }
    }
    return [IO.Path]::GetFullPath($current)
}

function Assert-PARCheckpointBoundary {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $StatePath,
        [Parameter(Mandatory)][string] $RuntimePath,
        [Parameter(Mandatory)][string] $Action
    )

    # Isolated legacy fixtures keep their test-local state layout.
    if (-not [bool] $Campaign.requireAutonomousPreset -and
        -not [string]::IsNullOrWhiteSpace([string] $env:PAR_TEST_FIXTURE_ROOT)) { return }
    if (-not (Test-Path -LiteralPath $RuntimePath -PathType Container)) {
        Assert-PARCondition ($Action -eq 'Start') 'RuntimeRoot fuer bestehenden Campaign-State fehlt.'
        [void](New-Item -ItemType Directory -Path $RuntimePath -Force)
    }
    $root = Resolve-PARPhysicalPath $RuntimePath
    $stateParent = Resolve-PARPhysicalPath (Split-Path -Parent $StatePath)
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    Assert-PARCondition ([string]::Equals($root, $stateParent, $comparison)) `
        'Operativer Campaign-State muss direkt im geschuetzten RuntimeRoot liegen.'
    if (Test-Path -LiteralPath $StatePath) {
        $entry = Get-Item -LiteralPath $StatePath -Force
        Assert-PARCondition (($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -eq 0) `
            'Campaign-State darf kein Symlink oder Reparse Point sein.'
    }
    $prefix = $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    foreach ($worker in @($Campaign.workers)) {
        $repository = Resolve-PARPhysicalPath (Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory)
        Assert-PARCondition (-not [string]::Equals($root, $repository, $comparison) -and
            -not $root.StartsWith($repository.TrimEnd([IO.Path]::DirectorySeparatorChar) +
                [IO.Path]::DirectorySeparatorChar, $comparison) -and
            -not $repository.StartsWith($prefix, $comparison)) `
            'RuntimeRoot muss ausserhalb aller Worker-Repositories liegen.'
    }
}

function Resolve-PARFeatureInput {
    param(
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string] $Relative
    )

    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($Relative) -and
        -not [IO.Path]::IsPathRooted($Relative) -and $Relative -notmatch '\\' -and
        $Relative -notmatch '(^|/)\.\.?(/|$)') 'featureInput muss ein sicherer repository-relativer Pfad sein.'
    $root = Resolve-PARPhysicalPath $Repository
    $path = Resolve-PARPhysicalPath (Join-Path $Repository $Relative)
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    $prefix = $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    Assert-PARCondition ($path.StartsWith($prefix, $comparison) -and
        (Test-Path -LiteralPath $path -PathType Leaf)) "featureInput verlaesst das Repository oder fehlt: $Relative"
    return $path
}

function Resolve-PARHandoffPath {
    param(
        [Parameter(Mandatory)][string] $Worktree,
        [Parameter(Mandatory)][string] $Relative
    )

    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($Relative) -and
        -not [IO.Path]::IsPathRooted($Relative) -and $Relative -notmatch '\\' -and
        $Relative -notmatch '(^|/)\.\.?(/|$)') 'Handoff-Pfad muss worktree-relativ sein.'
    $root = Resolve-PARPhysicalPath $Worktree
    $candidate = [IO.Path]::GetFullPath((Join-Path $root $Relative))
    $prefix = $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    Assert-PARCondition ($candidate.StartsWith($prefix, $comparison)) "Handoff verlaesst den Worker-Worktree: $Relative"
    $current = $root
    foreach ($segment in $Relative.Split('/')) {
        $current = Join-Path $current $segment
        $entry = Get-Item -LiteralPath $current -Force -ErrorAction Stop
        Assert-PARCondition (($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -eq 0) `
            "Handoff enthaelt Symlink oder Reparse Point: $Relative"
    }
    $file = Get-Item -LiteralPath $candidate -Force -ErrorAction Stop
    Assert-PARCondition (-not $file.PSIsContainer -and $file.Length -le 8MB) `
        "Handoff fehlt oder ueberschreitet 8 MiB: $Relative"
    return $candidate
}

function Get-PARRunnerProfileName {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Worker
    )

    if ($Worker.ContainsKey('runnerProfile') -and
        -not [string]::IsNullOrWhiteSpace([string] $Worker.runnerProfile)) {
        return [string] $Worker.runnerProfile
    }
    return [string] $Campaign.runnerProfile
}

function Get-PARWorkerRoutingRole {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Worker
    )

    if ($Worker.ContainsKey('routingRole') -and
        -not [string]::IsNullOrWhiteSpace([string] $Worker.routingRole)) {
        return [string] $Worker.routingRole
    }
    if ($Campaign.ContainsKey('routingRole') -and
        -not [string]::IsNullOrWhiteSpace([string] $Campaign.routingRole)) {
        return [string] $Campaign.routingRole
    }
    return $script:UndeclaredRunnerMetadata
}

function Get-PARRunnerMetadata {
    param(
        [Parameter(Mandatory)][string] $ProfileName,
        [Parameter(Mandatory)][hashtable] $RunnerProfileData
    )

    $agentFamily = if ($RunnerProfileData.ContainsKey('agentFamily') -and
        -not [string]::IsNullOrWhiteSpace([string] $RunnerProfileData.agentFamily)) {
        [string] $RunnerProfileData.agentFamily
    } else {
        $script:UndeclaredRunnerMetadata
    }
    $model = if ($RunnerProfileData.ContainsKey('model') -and
        -not [string]::IsNullOrWhiteSpace([string] $RunnerProfileData.model)) {
        [string] $RunnerProfileData.model
    } else {
        $script:UndeclaredRunnerMetadata
    }
    $reasoningEffort = if ($RunnerProfileData.ContainsKey('reasoningEffort') -and
        -not [string]::IsNullOrWhiteSpace([string] $RunnerProfileData.reasoningEffort)) {
        [string] $RunnerProfileData.reasoningEffort
    } else {
        $script:UndeclaredRunnerMetadata
    }

    return [ordered]@{
        runnerProfile = $ProfileName
        agentFamily = $agentFamily
        model = $model
        reasoningEffort = $reasoningEffort
    }
}

function Get-PARWorkerState {
    param(
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][string] $WorkerId
    )
    return @($CampaignState.workers | Where-Object { $_.workerId -eq $WorkerId })[0]
}

function Add-PARStateEvent {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Mutates only the in-memory campaign state passed by the caller.')]
    param(
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][string] $Type,
        [Parameter(Mandatory)][string] $Summary,
        [string] $WorkerId = '',
        [int] $Attempt = 0
    )

    if (-not $CampaignState.ContainsKey('eventSequence')) {
        $CampaignState.eventSequence = 0
    }
    if (-not $CampaignState.ContainsKey('events')) {
        $CampaignState.events = @()
    }
    $CampaignState.eventSequence = [int] $CampaignState.eventSequence + 1
    $CampaignState.events = @($CampaignState.events) + [ordered]@{
        sequence = [int] $CampaignState.eventSequence
        timestamp = [DateTime]::UtcNow.ToString('o')
        type = $Type
        phase = [string] $CampaignState.phase
        status = [string] $CampaignState.status
        workerId = if ([string]::IsNullOrWhiteSpace($WorkerId)) { $null } else { $WorkerId }
        attempt = $Attempt
        summary = $Summary
    }
}

function Sync-PARStopRequest {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Synchronizes only the in-memory campaign state passed by the caller.')]
    param(
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][string] $StatePath
    )

    $stopWasRequested = [bool] $CampaignState.stopRequested
    $markerPath = "$([IO.Path]::GetFullPath($StatePath)).stop-requested"
    if (Test-Path -LiteralPath $markerPath -PathType Leaf) {
        $CampaignState.stopRequested = $true
    }
    if (-not (Test-Path -LiteralPath $StatePath -PathType Leaf)) {
        return
    }
    $diskState = Read-PARJson $StatePath
    if ([bool] $diskState.stopRequested) {
        $CampaignState.stopRequested = $true
    }
    if ($diskState.ContainsKey('eventSequence') -and
        [int] $diskState.eventSequence -gt [int] $CampaignState.eventSequence) {
        $CampaignState.eventSequence = [int] $diskState.eventSequence
        $CampaignState.events = @($diskState.events)
    }
    if (-not $stopWasRequested -and [bool] $CampaignState.stopRequested) {
        Add-PARStateEvent $CampaignState 'StopRequestObserved' `
            'Durable cooperative stop request observed by the active coordinator.'
    }
}

function Test-PARAutonomousPreset {
    param([Parameter(Mandatory)][string] $Repository)

    try {
        $root = Resolve-PARPhysicalPath $Repository
        $prefix = $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
        $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
        $registryPath = Resolve-PARPhysicalPath (Join-Path $root '.specify/presets/.registry')
        if (-not $registryPath.StartsWith($prefix, $comparison) -or
            -not (Test-Path -LiteralPath $registryPath -PathType Leaf)) { return $false }
        $registry = Read-PARJson $registryPath
        if (-not $registry.presets.ContainsKey('autonomous-run-governance')) { return $false }
        $entry = $registry.presets['autonomous-run-governance']
        if (-not [bool] $entry.enabled -or [version] $entry.version -lt [version] '0.2.2') { return $false }
        $presetRoot = Join-Path $root '.specify/presets/autonomous-run-governance'
        $physicalPresetRoot = Resolve-PARPhysicalPath $presetRoot
        if (-not $physicalPresetRoot.StartsWith($prefix, $comparison) -or
            -not (Test-Path -LiteralPath $physicalPresetRoot -PathType Container)) { return $false }
        $presetPrefix = $physicalPresetRoot.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
        $manifest = Resolve-PARPhysicalPath (Join-Path $presetRoot 'preset.yml')
        if (-not $manifest.StartsWith($presetPrefix, $comparison) -or
            -not (Test-Path -LiteralPath $manifest -PathType Leaf) -or
            (Get-Item -LiteralPath $manifest).Length -gt 1MB -or
            [string] $entry.manifest_hash -ne "sha256:$(Get-PARSha256 $manifest)") { return $false }
        $manifestText = [IO.File]::ReadAllText($manifest, [Text.UTF8Encoding]::new($false, $true))
        $versionMatch = [regex]::Match($manifestText, '(?m)^  version:\s*"([0-9]+(?:\.[0-9]+){2})"\s*$')
        if (-not $versionMatch.Success -or [version] $versionMatch.Groups[1].Value -ne [version] $entry.version) { return $false }
        $files = [regex]::Matches($manifestText, '(?m)^\s+file:\s*"([^"\r\n]+)"\s*$')
        if ($files.Count -eq 0) { return $false }
        foreach ($match in $files) {
            $relative = [string] $match.Groups[1].Value
            if ([IO.Path]::IsPathRooted($relative) -or $relative -match '\\|(^|/)\.\.?(/|$)') { return $false }
            $payload = Resolve-PARPhysicalPath (Join-Path $presetRoot $relative)
            if (-not $payload.StartsWith($presetPrefix, $comparison) -or
                -not (Test-Path -LiteralPath $payload -PathType Leaf)) { return $false }
        }
        $requiredFiles = @('commands/speckit.autonomous.md',
            'scripts/validate-autonomous-run-state.ps1',
            'scripts/validate-autonomous-run-state.sh')
        if ([version] $entry.version -ge [version] '0.4.0') {
            $requiredFiles += 'scripts/autonomous-evidence-core.py'
        }
        foreach ($required in $requiredFiles) {
            $requiredPath = Resolve-PARPhysicalPath (Join-Path $presetRoot $required)
            if (-not $requiredPath.StartsWith($presetPrefix, $comparison) -or
                -not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) { return $false }
        }
        return $true
    } catch {
        return $false
    }
}

function Test-PARFixtureExemption {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestDirectory
    )

    $marker = [string] $env:PAR_TEST_FIXTURE_ROOT
    if ([string]::IsNullOrWhiteSpace($marker)) { return $false }
    try {
        $root = Resolve-PARPhysicalPath $marker
        $prefix = $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
        $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
        $fixtureRunner = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../tests/fixture-worker.ps1'))
        foreach ($worker in @($Campaign.workers)) {
            $repository = Resolve-PARPhysicalPath (Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory)
            if (-not $repository.StartsWith($prefix, $comparison)) { return $false }
            $profileName = Get-PARRunnerProfileName $Campaign $worker
            if (-not $Profiles.profiles.ContainsKey($profileName)) { return $false }
            $runnerProfile = $Profiles.profiles[$profileName]
            $arguments = @($runnerProfile.arguments)
            $expected = @('-NoProfile', '-File', $fixtureRunner,
                '-CampaignId', '{campaignId}', '-WorkerId', '{workerId}',
                '-RunId', '{runId}', '-AttemptId', '{attemptId}',
                '-Worktree', '{worktree}', '-ResultFile', '{resultFile}',
                '-Mode', 'Completed', '-HandoffsJson', '{handoffsJson}',
                '-DelayMilliseconds', '0')
            if ([string] $runnerProfile.executable -cne 'pwsh' -or $runnerProfile.ContainsKey('preflight') -or
                $arguments.Count -notin @(21, 23) -or
                -not [IO.Path]::IsPathFullyQualified([string] $arguments[2]) -or
                -not [string]::Equals([IO.Path]::GetFullPath([string] $arguments[2]), $fixtureRunner, $comparison)) {
                return $false
            }
            for ($index = 0; $index -lt $expected.Count; $index++) {
                if ($index -in @(2, 16, 20)) { continue }
                if ([string] $arguments[$index] -cne [string] $expected[$index]) { return $false }
            }
            $delay = 0
            if ([string] $arguments[16] -notin @('Completed', 'ReadyForMerge') -or
                -not [int]::TryParse([string] $arguments[20], [ref] $delay) -or
                $delay -lt 0 -or $delay -gt 5000) { return $false }
            if ($arguments.Count -eq 23) {
                $extraName = [string] $arguments[21]
                $extraValue = [string] $arguments[22]
                if ($extraName -eq '-ReportedAttemptId') {
                    if (-not (Test-PARUuid $extraValue)) { return $false }
                } elseif ($extraName -eq '-HandoffSymlinkOutside') {
                    if (-not [IO.Path]::IsPathFullyQualified($extraValue)) { return $false }
                    $extraPath = Resolve-PARPhysicalPath $extraValue
                    if (-not $extraPath.StartsWith($prefix, $comparison)) { return $false }
                } elseif ($extraName -eq '-OutputBytes') {
                    $outputBytes = 0
                    if (-not [int]::TryParse($extraValue, [ref] $outputBytes) -or
                        $outputBytes -lt 0 -or $outputBytes -gt 20MB) { return $false }
                } else { return $false }
            }
        }
        if ($Campaign.deliveryMode -eq 'PublishPR') { return $false }
        if ($Campaign.deliveryMode -eq 'MergeAndSync') {
            $mergeName = [string] $Campaign.consolidation.mergeProfile
            if (-not $Profiles.mergeProfiles.ContainsKey($mergeName)) { return $false }
            $mergeProfile = $Profiles.mergeProfiles[$mergeName]
            if ([string] $Campaign.schemaVersion -eq '1.0') {
                if ($mergeName -cne 'fixture-merge' -or [string] $mergeProfile.executable -cne 'pwsh' -or
                    (@($mergeProfile.arguments) -join "`0") -cne (@('-NoProfile', '-Command', 'exit 0') -join "`0")) {
                    return $false
                }
            } else {
                if ($mergeName -cne 'fixture-provider' -or [string] $mergeProfile.provider -cne 'Local Fixture') {
                    return $false
                }
                $providerScript = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../tests/fixture-provider.ps1'))
                $preflightArgs = @('-NoProfile', '-File', $providerScript, '-Mode', 'Preflight',
                    '-StateFile', '<fixture-state>', '-WorkerId', '{workerId}', '-PrUrl', '{prUrl}',
                    '-ExpectedHead', '{headSha}', '-OutputFile', '{preflightResultFile}')
                $mergeArgs = @('-NoProfile', '-File', $providerScript, '-Mode', 'Merge',
                    '-StateFile', '<fixture-state>', '-WorkerId', '{workerId}', '-PrUrl', '{prUrl}',
                    '-ExpectedHead', '{headSha}')
                if (-not (Test-PARFixtureProfileCommand $mergeProfile.preflight $preflightArgs 6 $prefix $comparison) -or
                    -not (Test-PARFixtureProfileCommand $mergeProfile.merge $mergeArgs 6 $prefix $comparison)) {
                    return $false
                }
                $postScript = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../tests/fixture-post-merge.ps1'))
                $postArgs = @('-NoProfile', '-File', $postScript, '-StateFile', '<fixture-state>',
                    '-ActionId', '{actionId}', '-Repository', '{repository}', '-ExpectedHead', '{headSha}')
                foreach ($action in @($Campaign.postMergeActions)) {
                    if ([string] $action.profile -cne 'fixture-post-merge' -or
                        -not $Profiles.postMergeProfiles.ContainsKey('fixture-post-merge') -or
                        -not (Test-PARFixtureProfileCommand $Profiles.postMergeProfiles['fixture-post-merge'] `
                            $postArgs 4 $prefix $comparison)) { return $false }
                }
            }
        }
        return $true
    } catch {
        return $false
    }
}

function Test-PARFixtureProfileCommand {
    param(
        [Parameter(Mandatory)][hashtable] $RunnerProfile,
        [Parameter(Mandatory)][object[]] $Expected,
        [Parameter(Mandatory)][int] $StateFileIndex,
        [Parameter(Mandatory)][string] $Prefix,
        [Parameter(Mandatory)][StringComparison] $Comparison
    )

    $arguments = @($RunnerProfile.arguments)
    if ([string] $RunnerProfile.executable -cne 'pwsh' -or $arguments.Count -ne $Expected.Count) { return $false }
    for ($index = 0; $index -lt $Expected.Count; $index++) {
        $actual = [string] $arguments[$index]
        if ($index -eq 2 -or $index -eq $StateFileIndex) {
            if (-not [IO.Path]::IsPathFullyQualified($actual)) { return $false }
            $physical = Resolve-PARPhysicalPath $actual
            if ($index -eq 2) {
                if (-not [string]::Equals($physical, [string] $Expected[$index], $Comparison)) { return $false }
            } elseif (-not $physical.StartsWith($Prefix, $Comparison) -or
                -not (Test-Path -LiteralPath $physical -PathType Leaf)) { return $false }
        } elseif ($actual -cne [string] $Expected[$index]) { return $false }
    }
    return $true
}

function Test-PARDag {
    param([Parameter(Mandatory)][object[]] $Workers)

    $known = @{}
    $inDegree = @{}
    $children = @{}
    foreach ($worker in $Workers) {
        $known[$worker.workerId] = $true
        $inDegree[$worker.workerId] = 0
        $children[$worker.workerId] = [Collections.Generic.List[string]]::new()
    }
    foreach ($worker in $Workers) {
        foreach ($dependency in @($worker.dependsOn)) {
            Assert-PARCondition $known.ContainsKey($dependency) "Unbekannte Abhaengigkeit '$dependency' bei '$($worker.workerId)'."
            Assert-PARCondition ($dependency -ne $worker.workerId) "Worker '$($worker.workerId)' darf nicht von sich selbst abhaengen."
            $inDegree[$worker.workerId]++
            $children[$dependency].Add($worker.workerId)
        }
    }
    foreach ($worker in $Workers) {
        foreach ($dependency in @($worker.dependsOn)) {
            $producer = @($Workers | Where-Object { $_.workerId -eq $dependency })[0]
            $matchingHandoffs = @($producer.handoffs | Where-Object { $_.consumerWorkerId -eq $worker.workerId })
            Assert-PARCondition ($matchingHandoffs.Count -eq 1) "Pipeline-Abhaengigkeit '$dependency' -> '$($worker.workerId)' benoetigt genau einen deklarierten Handoff."
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $matchingHandoffs[0].path)) "Handoff '$dependency' -> '$($worker.workerId)' benoetigt einen Pfad."
        }
    }
    $queue = [Collections.Generic.Queue[string]]::new()
    foreach ($id in $inDegree.Keys) {
        if ($inDegree[$id] -eq 0) {
            $queue.Enqueue($id)
        }
    }
    $visited = 0
    while ($queue.Count -gt 0) {
        $id = $queue.Dequeue()
        $visited++
        foreach ($child in $children[$id]) {
            $inDegree[$child]--
            if ($inDegree[$child] -eq 0) {
                $queue.Enqueue($child)
            }
        }
    }
    Assert-PARCondition ($visited -eq $Workers.Count) 'Die Worker-Abhaengigkeiten enthalten einen Zyklus.'
}

function Test-PARIntakeReview {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][string] $ManifestDirectory
    )

    $script:CurrentIntakeWorkerHashes = @{}
    $notApplicable = [ordered]@{ required = $false; path = 'N/A'; sha256 = 'N/A' }
    if ([string] $Campaign.schemaVersion -ne '1.2') { return $notApplicable }
    Assert-PARCondition $Campaign.ContainsKey('intakeReview') `
        'Schema 1.2 benoetigt intakeReview.'
    $gate = $Campaign.intakeReview
    Assert-PARCondition ($gate -is [hashtable] -and $gate.ContainsKey('required') -and
        $gate.required -is [bool]) 'intakeReview.required muss boolesch sein.'
    if (-not [bool] $gate.required) { return $notApplicable }
    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $gate.resultPath)) `
        'Erforderliches intakeReview benoetigt resultPath.'

    $resultPath = [IO.Path]::GetFullPath((Join-Path $ManifestDirectory ([string] $gate.resultPath)))
    $manifestPrefix = [IO.Path]::GetFullPath($ManifestDirectory).TrimEnd(
        [IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    Assert-PARCondition $resultPath.StartsWith($manifestPrefix, [StringComparison]::Ordinal) `
        'intakeReview.resultPath muss unterhalb des Manifestverzeichnisses liegen.'
    Assert-PARCondition (Test-Path -LiteralPath $resultPath -PathType Leaf) `
        "Intake-Review-Ergebnis fehlt: $resultPath"
    $physicalManifest = Resolve-PARPhysicalPath $ManifestDirectory
    $physicalResult = Resolve-PARPhysicalPath $resultPath
    $physicalPrefix = $physicalManifest.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    Assert-PARCondition $physicalResult.StartsWith($physicalPrefix, $comparison) `
        'Intake-Review-Ergebnis verlaesst das Manifestverzeichnis.'
    $result = Read-PARJson $resultPath
    Assert-PARCondition ($result.requestEvidence -is [hashtable] -and
        -not [string]::IsNullOrWhiteSpace([string] $result.requestEvidence.path)) `
        'Campaign-Review benoetigt eine gebundene Review-Anfrage.'
    $validatorCandidates = @(
        (Join-Path $PSScriptRoot '../../intake-review-governance/scripts/validate-intake-review-result.ps1'),
        (Join-Path $PSScriptRoot '../../spec-kit-preset-intake-review-governance/scripts/validate-intake-review-result.ps1')
    )
    $validator = @($validatorCandidates | Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } | Select-Object -First 1)
    Assert-PARCondition ($validator.Count -eq 1) 'Installierter Intake-Review-Validator fehlt.'
    $validationOutput = @(& pwsh -NoProfile -File $validator[0] -Result $resultPath -Repo $ManifestDirectory 2>&1)
    Assert-PARCondition ($LASTEXITCODE -eq 0) `
        "Intake-Review-Validator blockiert Kampagne: $($validationOutput -join ' ')"
    Assert-PARCondition ([string] $result.schemaVersion -eq '1.0') `
        'Intake-Review-Ergebnis benoetigt Schema 1.0.'
    Assert-PARCondition ([string] $result.mode -eq 'Campaign') `
        'Kampagnen-Gate benoetigt ein Campaign-Review.'
    Assert-PARCondition (@('Ready', 'ReadyWithAcceptedRisks') -contains [string] $result.status) `
        "Intake-Review blockiert Worker: $($result.status)"
    Assert-PARCondition (@($result.questions | Where-Object status -ne 'Answered').Count -eq 0) `
        'Akzeptiertes Intake-Review enthaelt offene Fragen.'
    Assert-PARCondition (@($result.findings | Where-Object severity -in @('Critical', 'High')).Count -eq 0) `
        'Critical/High Intake-Findings blockieren die Kampagne.'
    if ([string] $result.status -eq 'ReadyWithAcceptedRisks') {
        Assert-PARCondition (@($result.acceptedRisks).Count -gt 0) `
            'ReadyWithAcceptedRisks benoetigt akzeptierte Risiken.'
        foreach ($risk in @($result.acceptedRisks)) {
            Assert-PARCondition ([string] $risk.acceptedByType -eq 'Human') `
                'Nur Menschen duerfen Intake-Risiken akzeptieren.'
        }
    }

    $targetByPath = @{}
    foreach ($target in @($result.targets)) {
        $path = [string] $target.path
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($path) -and
            -not $targetByPath.ContainsKey($path)) 'Review-Zielpfade muessen eindeutig sein.'
        Assert-PARCondition ([string] $target.normalizedSha256 -match '^[0-9a-f]{64}$') `
            "Ungueltiger normalisierter Intake-Hash: $path"
        $targetByPath[$path] = [string] $target.normalizedSha256
    }
    $coverage = @($result.coverage.workers)
    Assert-PARCondition ($coverage.Count -eq @($Campaign.workers).Count) `
        'Intake-Review benoetigt genau eine Applicability-Zeile je Worker.'
    Assert-PARCondition (@($coverage.workerId | Sort-Object -Unique).Count -eq $coverage.Count) `
        'Worker-Applicability darf keine Duplikate enthalten.'
    $containerOverrideWorkers = [Collections.Generic.List[string]]::new()
    foreach ($worker in @($Campaign.workers)) {
        $rows = @($coverage | Where-Object workerId -eq $worker.workerId)
        Assert-PARCondition ($rows.Count -eq 1) `
            "Worker '$($worker.workerId)' benoetigt genau eine Intake-Applicability-Zeile."
        $targetPath = [string] $rows[0].targetPath
        Assert-PARCondition $targetByPath.ContainsKey($targetPath) `
            "Worker '$($worker.workerId)' verweist auf ein ungeprueftes Intake."
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $rows[0].applicability)) `
            "Worker '$($worker.workerId)' benoetigt eine Applicability-Entscheidung."
        $repository = Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory
        $featureInput = Resolve-PARFeatureInput $repository ([string] $worker.featureInput)
        Assert-PARCondition ((Get-PARNormalizedTextSha256 $featureInput) -eq $targetByPath[$targetPath]) `
            "featureInput-Hash stimmt nicht mit Review ueberein: $($worker.workerId)"
        $script:CurrentIntakeWorkerHashes[[string] $worker.workerId] = $targetByPath[$targetPath]
        $inputText = [IO.File]::ReadAllText($featureInput, [Text.UTF8Encoding]::new($false, $true))
        if ($inputText -match '(?i)container-first' -and
            [string] $Campaign.operatorInstructions -match '(?is)\bnativ(?:e|ely)?\b.*\b(?:override|overriding|ausnahme)\b') {
            $containerOverrideWorkers.Add([string] $worker.workerId)
        }
    }

    $expectedEdges = @($Campaign.workers | ForEach-Object {
        $consumer = [string] $_.workerId
        @($_.dependsOn) | ForEach-Object { "${_}->${consumer}" }
    } | Sort-Object -Unique)
    $reviewEdges = @($result.coverage.series | ForEach-Object {
        "$([string] $_.from)->$([string] $_.to)"
    } | Sort-Object -Unique)
    Assert-PARCondition (($expectedEdges -join '|') -eq ($reviewEdges -join '|')) `
        'Review-Series-Coverage stimmt nicht mit dem Manifest-DAG ueberein.'

    $exceptionWorkers = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
    foreach ($exception in @($result.operatorExceptions)) {
        foreach ($field in @('author', 'reason', 'date', 'expiry')) {
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $exception[$field])) `
                "Operator-Ausnahme benoetigt $field."
        }
        $expiry = if ($exception.expiry -is [DateTime]) {
            [DateTimeOffset]::new(([DateTime] $exception.expiry).ToUniversalTime())
        } else {
            $parsedExpiry = [DateTimeOffset]::MinValue
            Assert-PARCondition ([DateTimeOffset]::TryParse(
                [string] $exception.expiry,
                [Globalization.CultureInfo]::InvariantCulture,
                [Globalization.DateTimeStyles]::AssumeUniversal,
                [ref] $parsedExpiry
            )) 'Operator-Ausnahme enthaelt kein gueltiges Ablaufdatum.'
            $parsedExpiry
        }
        Assert-PARCondition ($expiry -gt [DateTimeOffset]::UtcNow) `
            'Operator-Ausnahme ist abgelaufen.'
        foreach ($workerId in @($exception.workerIds)) { [void] $exceptionWorkers.Add([string] $workerId) }
    }
    foreach ($workerId in $containerOverrideWorkers) {
        Assert-PARCondition $exceptionWorkers.Contains($workerId) `
            "Container-First/native Override benoetigt eine gueltige Operator-Ausnahme: $workerId"
    }
    return [ordered]@{ required = $true; path = [string] $gate.resultPath; sha256 = Get-PARSha256 $resultPath }
}

function Test-PARCampaign {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [switch] $SkipCleanCheck
    )

    Assert-PARSchemaVersion $Campaign 'Campaign'
    Assert-PARCondition (Test-PARUuid ([string] $Campaign.campaignId)) 'campaignId muss eine nichtleere UUID sein.'
    Assert-PARCondition (@('ReplicatedTargets', 'IndependentFeatures', 'AlternativeSolutions', 'Pipeline') -contains $Campaign.topology) 'Unbekannte Campaign-Topologie.'
    Assert-PARCondition (@('LocalImplementation', 'PublishPR', 'MergeAndSync') -contains $Campaign.deliveryMode) 'Unbekannter deliveryMode.'
    Assert-PARCondition ([int] $Campaign.maxConcurrency -ge 1 -and [int] $Campaign.maxConcurrency -le 3) 'maxConcurrency muss zwischen 1 und 3 liegen.'
    Assert-PARCondition (@($Campaign.workers).Count -gt 0) 'Mindestens ein Worker ist erforderlich.'
    if (-not [bool] $Campaign.requireAutonomousPreset -and $Action -notin @('Status', 'Stop')) {
        Assert-PARCondition (Test-PARFixtureExemption $Campaign $Profiles $ManifestDirectory) `
            'Autonomous-Preset-Gate ist fuer operative Kampagnen verpflichtend; nur isolierte Test-Fixtures duerfen es deaktivieren.'
    }
    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $Campaign.runnerProfile)) 'Campaign runnerProfile fehlt.'
    Assert-PARCondition ($Profiles.ContainsKey('profiles') -and $Profiles.profiles -is [hashtable]) `
        'RunnerConfig profiles fehlt.'
    $strictRouting = $Campaign.ContainsKey('fallbackPolicy') -and
        [string] $Campaign.fallbackPolicy -eq 'fail-closed'
    if ($Campaign.ContainsKey('fallbackPolicy')) {
        Assert-PARCondition ([string] $Campaign.fallbackPolicy -eq 'fail-closed') `
            'fallbackPolicy darf nur fail-closed sein.'
    }
    $allowedRoutingRoles = @('fast-mechanical', 'long-running-implementation', 'coding-review', 'frontier-reasoning', 'script-only')
    if ($strictRouting) {
        Assert-PARCondition ($allowedRoutingRoles -contains [string] $Campaign.routingRole) `
            'Campaign routingRole fehlt oder ist ungueltig.'
    }
    $campaignRunnerProfile = [string] $Campaign.runnerProfile
    Assert-PARCondition $Profiles.profiles.ContainsKey($campaignRunnerProfile) `
        "Campaign-Runner-Fallback '$campaignRunnerProfile' fehlt."
    if ($Campaign.ContainsKey('operatorInstructions')) {
        Assert-PARCondition ($Campaign.operatorInstructions -is [string]) 'operatorInstructions muss eine Zeichenkette sein.'
        Assert-PARCondition (([string] $Campaign.operatorInstructions).Length -le 8000) 'operatorInstructions ist laenger als 8000 Zeichen.'
    }

    if ($null -eq $script:CurrentIntakeReview) {
        $script:CurrentIntakeReview = Test-PARIntakeReview $Campaign $ManifestDirectory
    }

    $workerIds = @($Campaign.workers | ForEach-Object { [string] $_.workerId })
    $runIds = @($Campaign.workers | ForEach-Object { [string] $_.runId })
    $branches = @($Campaign.workers | ForEach-Object { [string] $_.branch })
    Assert-PARCondition (@($workerIds | Sort-Object -Unique).Count -eq $workerIds.Count) 'workerId muss eindeutig sein.'
    Assert-PARCondition (@($runIds | Sort-Object -Unique).Count -eq $runIds.Count) 'runId muss eindeutig sein.'
    Assert-PARCondition (@($branches | Sort-Object -Unique).Count -eq $branches.Count) 'branch muss campaignweit eindeutig sein.'

    foreach ($worker in $Campaign.workers) {
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $worker.workerId)) 'workerId darf nicht leer sein.'
        Assert-PARCondition (Test-PARUuid ([string] $worker.runId)) "runId von '$($worker.workerId)' ist keine UUID."
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $worker.branch)) "branch von '$($worker.workerId)' fehlt."
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $worker.featureInput) -and
            -not [IO.Path]::IsPathRooted([string] $worker.featureInput) -and
            [string] $worker.featureInput -notmatch '\\' -and
            [string] $worker.featureInput -notmatch '(^|/)\.\.?(/|$)') `
            "featureInput von '$($worker.workerId)' muss repository-relativ sein."
        $runnerProfileName = Get-PARRunnerProfileName $Campaign $worker
        Assert-PARCondition $Profiles.profiles.ContainsKey($runnerProfileName) "Runner-Profil '$runnerProfileName' fuer Worker '$($worker.workerId)' fehlt."
        $runnerProfileData = $Profiles.profiles[$runnerProfileName]
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.executable)) "Runner executable fehlt: $runnerProfileName"
        Assert-PARCondition ($runnerProfileData.arguments -is [object[]]) "Runner arguments muss ein Array sein: $runnerProfileName"
        if ([string] $Campaign.schemaVersion -in @('1.1', '1.2')) {
            Assert-PARCondition ($runnerProfileData.ContainsKey('agentFamily') -and
                -not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.agentFamily)) `
                "Runner-Profil '$runnerProfileName' benoetigt agentFamily."
        }
        if ($strictRouting) {
            $workerRoutingRole = Get-PARWorkerRoutingRole $Campaign $worker
            Assert-PARCondition ($allowedRoutingRoles -contains $workerRoutingRole) `
                "Worker '$($worker.workerId)' hat keine gueltige routingRole."
            Assert-PARCondition ([string] $runnerProfileData.routingRole -eq $workerRoutingRole) `
                "Runner-Profil '$runnerProfileName' passt nicht zur routingRole '$workerRoutingRole'."
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.model)) `
                "Runner-Profil '$runnerProfileName' benoetigt model fuer fail-closed Routing."
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.reasoningEffort)) `
                "Runner-Profil '$runnerProfileName' benoetigt reasoningEffort fuer fail-closed Routing."
            Assert-PARCondition ($runnerProfileData.ContainsKey('preflight') -and
                $runnerProfileData.preflight -is [hashtable]) `
                "Runner-Profil '$runnerProfileName' benoetigt preflight fuer fail-closed Routing."
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.preflight.executable) -and
                $runnerProfileData.preflight.arguments -is [object[]]) `
                "Runner-Profil '$runnerProfileName' hat keinen gueltigen Preflight-Vertrag."
        }
        $repository = Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory
        Assert-PARCondition (Test-Path -LiteralPath $repository -PathType Container) "Repository fehlt: $repository"
        $inside = Invoke-PARGit $repository @('rev-parse', '--is-inside-work-tree') -AllowFailure
        Assert-PARCondition ($inside.ExitCode -eq 0) "Kein Git-Worktree: $repository"
        $baseWorkerId = if ($worker.ContainsKey('baseWorkerId')) { [string] $worker.baseWorkerId } else { '' }
        if ([string]::IsNullOrWhiteSpace($baseWorkerId)) {
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $worker.baseRef)) "baseRef oder baseWorkerId von '$($worker.workerId)' fehlt."
            $base = Invoke-PARGit $repository @('rev-parse', '--verify', "$($worker.baseRef)^{commit}") -AllowFailure
            Assert-PARCondition ($base.ExitCode -eq 0) "baseRef '$($worker.baseRef)' fehlt in $repository."
        } else {
            Assert-PARCondition ($workerIds -contains $baseWorkerId) "baseWorkerId '$baseWorkerId' von '$($worker.workerId)' ist unbekannt."
            Assert-PARCondition (@($worker.dependsOn) -contains $baseWorkerId) "baseWorkerId '$baseWorkerId' muss direkte Abhaengigkeit von '$($worker.workerId)' sein."
            $baseWorker = @($Campaign.workers | Where-Object { $_.workerId -eq $baseWorkerId })[0]
            $baseRepository = Resolve-PARRepository ([string] $baseWorker.repository) $ManifestDirectory
            Assert-PARCondition ($baseRepository -eq $repository) "baseWorkerId '$baseWorkerId' muss dasselbe Repository wie '$($worker.workerId)' verwenden."
        }
        if (-not $SkipCleanCheck) {
            $dirty = Invoke-PARGit $repository @('status', '--porcelain')
            Assert-PARCondition ($dirty.Output.Count -eq 0) "Repository ist nicht sauber: $repository"
        }
        if ([bool] $Campaign.requireAutonomousPreset) {
            Assert-PARCondition (Test-PARAutonomousPreset $repository) "autonomous-run-governance >= 0.2.2 fehlt oder ist deaktiviert: $repository"
        }
    }

    Test-PARDag @($Campaign.workers)

    $mergeOrder = @($Campaign.consolidation.mergeOrder)
    Assert-PARCondition ($mergeOrder.Count -eq $workerIds.Count) 'mergeOrder muss jeden Worker genau einmal enthalten.'
    Assert-PARCondition (@($mergeOrder | Sort-Object -Unique).Count -eq $workerIds.Count) 'mergeOrder enthaelt Duplikate.'
    foreach ($id in $mergeOrder) {
        Assert-PARCondition ($workerIds -contains $id) "mergeOrder enthaelt unbekannten Worker '$id'."
    }
    if ($Campaign.topology -eq 'AlternativeSolutions') {
        Assert-PARCondition ([bool] $Campaign.consolidation.humanSelectionRequired) 'AlternativeSolutions erfordert humanSelectionRequired=true.'
    }

    if ($Campaign.deliveryMode -eq 'MergeAndSync') {
        if ([string] $Campaign.schemaVersion -in @('1.1', '1.2')) {
            Assert-PARCondition $Campaign.consolidation.ContainsKey('mergeProfile') 'consolidation.mergeProfile fehlt.'
        }
        if ($Campaign.consolidation.ContainsKey('mergeProfile')) {
            $mergeProfileName = [string] $Campaign.consolidation.mergeProfile
            Assert-PARCondition ($Profiles.ContainsKey('mergeProfiles') -and
                $Profiles.mergeProfiles.ContainsKey($mergeProfileName)) "Merge-Profil '$mergeProfileName' fehlt."
        }
        if ([string] $Campaign.schemaVersion -in @('1.1', '1.2')) {
            $mergeProfile = $Profiles.mergeProfiles[$mergeProfileName]
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $mergeProfile.provider)) `
                "Merge-Profil '$mergeProfileName' benoetigt provider."
            Assert-PARCondition ($mergeProfile.ContainsKey('preflight') -and
                $mergeProfile.preflight -is [hashtable]) "Merge-Profil '$mergeProfileName' benoetigt preflight."
            Assert-PARCondition ($mergeProfile.ContainsKey('merge') -and
                $mergeProfile.merge -is [hashtable]) "Merge-Profil '$mergeProfileName' benoetigt merge."
            if ([string] $mergeProfile.provider -eq 'GitHub' -and
                [string] $mergeProfile.merge.executable -eq 'gh') {
                $mergeArguments = @($mergeProfile.merge.arguments)
                Assert-PARCondition ($mergeArguments.Count -eq 8 -and
                    [string] $mergeArguments[0] -ceq 'pr' -and
                    [string] $mergeArguments[1] -ceq 'merge' -and
                    [string] $mergeArguments[2] -ceq '{prNumber}' -and
                    [string] $mergeArguments[3] -ceq '--repo' -and
                    [string] $mergeArguments[4] -ceq '{prBaseRepository}' -and
                    [string] $mergeArguments[5] -ceq '--match-head-commit' -and
                    [string] $mergeArguments[6] -ceq '{headSha}' -and
                    [string] $mergeArguments[7] -in @('--merge', '--squash', '--rebase')) `
                    "GitHub-Merge-Profil '$mergeProfileName' muss die geprüfte PR-Nummer im Zielrepository und --match-head-commit verwenden."
            }
            Assert-PARCondition ($Campaign.ContainsKey('postMergeActions') -and
                $Campaign.postMergeActions -is [object[]]) 'Schema 1.1 MergeAndSync benoetigt postMergeActions.'
            Assert-PARCondition ($Profiles.ContainsKey('postMergeProfiles') -and
                $Profiles.postMergeProfiles -is [hashtable]) 'RunnerConfig benoetigt postMergeProfiles.'

            $actionIds = @($Campaign.postMergeActions | ForEach-Object { [string] $_.actionId })
            Assert-PARCondition (@($actionIds | Sort-Object -Unique).Count -eq $actionIds.Count) 'postMergeActions.actionId muss eindeutig sein.'
            $lastPhaseIndex = -1
            $phaseOrder = @('Synchronize', 'PostMerge', 'Validate')
            foreach ($postMergeAction in @($Campaign.postMergeActions)) {
                Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $postMergeAction.actionId)) 'postMergeActions.actionId fehlt.'
                Assert-PARCondition ($workerIds -contains [string] $postMergeAction.workerId) `
                    "postMergeAction '$($postMergeAction.actionId)' enthaelt einen unbekannten workerId."
                Assert-PARCondition ($phaseOrder -contains [string] $postMergeAction.phase) `
                    "postMergeAction '$($postMergeAction.actionId)' enthaelt eine ungueltige phase."
                $phaseIndex = [Array]::IndexOf($phaseOrder, [string] $postMergeAction.phase)
                Assert-PARCondition ($phaseIndex -ge $lastPhaseIndex) 'postMergeActions muessen nach Synchronize, PostMerge, Validate geordnet sein.'
                $lastPhaseIndex = $phaseIndex
                $postMergeProfileName = [string] $postMergeAction.profile
                Assert-PARCondition $Profiles.postMergeProfiles.ContainsKey($postMergeProfileName) `
                    "Post-Merge-Profil '$postMergeProfileName' fehlt."
            }
            Assert-PARCondition (@($Campaign.postMergeActions | Where-Object phase -eq 'Synchronize').Count -gt 0) `
                'Schema 1.1 MergeAndSync benoetigt mindestens eine Synchronize-Aktion.'
            Assert-PARCondition (@($Campaign.postMergeActions | Where-Object phase -eq 'Validate').Count -gt 0) `
                'Schema 1.1 MergeAndSync benoetigt mindestens eine Validate-Aktion.'
        }
    }
    if ($Campaign.deliveryMode -eq 'PublishPR') {
        $publicationName = [string] $Campaign.publicationProfile
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($publicationName) -and
            $Profiles.ContainsKey('publicationProfiles') -and
            $Profiles.publicationProfiles -is [hashtable] -and
            $Profiles.publicationProfiles.ContainsKey($publicationName)) `
            'PublishPR benoetigt ein lesendes publicationProfile.'
        $publicationProfile = $Profiles.publicationProfiles[$publicationName]
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $publicationProfile.provider) -and
            $publicationProfile.ContainsKey('preflight') -and
            $publicationProfile.preflight -is [hashtable] -and
            -not $publicationProfile.ContainsKey('merge')) `
            'PublishPR-Profil muss einen lesenden Provider-Preflight ohne Merge enthalten.'
    }
}

function Get-PARInitialCampaignState {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestPath
    )

    $workerStates = foreach ($worker in $Campaign.workers) {
        $profileName = Get-PARRunnerProfileName $Campaign $worker
        $runnerMetadata = Get-PARRunnerMetadata $profileName $Profiles.profiles[$profileName]
        $routingRole = Get-PARWorkerRoutingRole $Campaign $worker
        $fallbackPolicy = if ($Campaign.ContainsKey('fallbackPolicy')) {
            [string] $Campaign.fallbackPolicy
        } else {
            $script:UndeclaredRunnerMetadata
        }
        [ordered]@{
            workerId = [string] $worker.workerId
            runId = [string] $worker.runId
            status = 'Pending'
            worktree = 'N/A'
            processId = $null
            exitCode = $null
            headSha = 'N/A'
            autonomousStatePath = 'N/A'
            autonomousStateSha256 = 'N/A'
            resultPath = 'N/A'
            resultSha256 = 'N/A'
            prUrl = $null
            prNumber = $null
            prBaseRepository = $null
            mergeCommitSha = $null
            providerState = 'NotChecked'
            baseRefName = $null
            handoffs = @()
            routingRole = $routingRole
            fallbackPolicy = $fallbackPolicy
            runnerProfile = $runnerMetadata.runnerProfile
            agentFamily = $runnerMetadata.agentFamily
            model = $runnerMetadata.model
            reasoningEffort = $runnerMetadata.reasoningEffort
            executionAttempt = 0
            attemptId = 'N/A'
            mergeAttempt = 0
            modelPreflight = if ($fallbackPolicy -eq 'fail-closed') { 'Pending' } else { $script:UndeclaredRunnerMetadata }
            lastPreflightAt = $null
            summary = 'Not started.'
        }
    }
    $postMergeActionStates = foreach ($postMergeAction in @(
        if ($Campaign.ContainsKey('postMergeActions')) { $Campaign.postMergeActions } else { @() }
    )) {
        [ordered]@{
            actionId = [string] $postMergeAction.actionId
            workerId = [string] $postMergeAction.workerId
            phase = [string] $postMergeAction.phase
            profile = [string] $postMergeAction.profile
            status = 'Pending'
            attemptCount = 0
            exitCode = $null
            receiptSha256 = 'N/A'
            startedAt = $null
            completedAt = $null
            summary = 'Not started.'
        }
    }
    $state = [ordered]@{
        schemaVersion = '1.1'
        sourceSchemaVersion = [string] $Campaign.schemaVersion
        campaignId = [string] $Campaign.campaignId
        manifestSha256 = Get-PARSha256 $ManifestPath
        intakeReview = $script:CurrentIntakeReview
        status = 'Prepared'
        phase = 'Preflight'
        deliveryMode = [string] $Campaign.deliveryMode
        stopRequested = $false
        selectedWorkerId = $null
        maximumObservedConcurrency = 0
        attempts = [ordered]@{
            execute = 0
            consolidation = 0
            postMerge = 0
        }
        completion = [ordered]@{
            mergeComplete = $false
            synchronizationComplete = $false
            postMergeComplete = $false
            validationComplete = $false
        }
        workers = @($workerStates)
        postMergeActions = @($postMergeActionStates)
        eventSequence = 0
        events = @()
        updatedAt = [DateTime]::UtcNow.ToString('o')
    }
    Add-PARStateEvent $state 'CampaignPrepared' 'Campaign state initialized.'
    return $state
}

function Initialize-PARStateShape {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Upgrades only the in-memory campaign state passed by the caller.')]
    param(
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles
    )

    if ($Campaign.topology -ne 'AlternativeSolutions') {
        Assert-PARCondition ([string]::IsNullOrWhiteSpace([string] $CampaignState.selectedWorkerId)) `
            'selectedWorkerId ist nur fuer AlternativeSolutions zulaessig.'
    }

    $CampaignState.schemaVersion = '1.1'
    if (-not $CampaignState.ContainsKey('sourceSchemaVersion')) {
        $CampaignState.sourceSchemaVersion = '1.0'
    }
    if (-not $CampaignState.ContainsKey('intakeReview')) {
        $CampaignState.intakeReview = [ordered]@{ required = $false; path = 'N/A'; sha256 = 'N/A' }
    }
    Assert-PARCondition ([bool] $CampaignState.intakeReview.required -eq
        [bool] $script:CurrentIntakeReview.required) 'Intake-Review-Pflicht hat sich seit dem Checkpoint geaendert.'
    Assert-PARCondition ([string] $CampaignState.intakeReview.path -eq
        [string] $script:CurrentIntakeReview.path) 'Intake-Review-Pfad hat sich seit dem Checkpoint geaendert.'
    Assert-PARCondition ([string] $CampaignState.intakeReview.sha256 -eq
        [string] $script:CurrentIntakeReview.sha256) 'Intake-Review-Ergebnis ist seit dem Checkpoint gedriftet.'
    if (-not $CampaignState.ContainsKey('attempts')) {
        $CampaignState.attempts = [ordered]@{ execute = 0; consolidation = 0; postMerge = 0 }
    }
    if (-not $CampaignState.ContainsKey('completion')) {
        $CampaignState.completion = [ordered]@{
            mergeComplete = $false
            synchronizationComplete = $false
            postMergeComplete = $false
            validationComplete = $false
        }
    }
    if (-not $CampaignState.ContainsKey('postMergeActions')) {
        $CampaignState.postMergeActions = @()
    }
    if (-not $CampaignState.ContainsKey('eventSequence')) {
        $CampaignState.eventSequence = 0
    }
    if (-not $CampaignState.ContainsKey('events')) {
        $CampaignState.events = @()
    }

    foreach ($worker in $Campaign.workers) {
        $workerState = Get-PARWorkerState $CampaignState ([string] $worker.workerId)
        $profileName = Get-PARRunnerProfileName $Campaign $worker
        $metadata = Get-PARRunnerMetadata $profileName $Profiles.profiles[$profileName]
        $routingRole = Get-PARWorkerRoutingRole $Campaign $worker
        $fallbackPolicy = if ($Campaign.ContainsKey('fallbackPolicy')) {
            [string] $Campaign.fallbackPolicy
        } else {
            $script:UndeclaredRunnerMetadata
        }
        foreach ($entry in @{
            routingRole = $routingRole
            fallbackPolicy = $fallbackPolicy
            runnerProfile = $metadata.runnerProfile
            agentFamily = $metadata.agentFamily
            model = $metadata.model
            reasoningEffort = $metadata.reasoningEffort
            executionAttempt = 0
            attemptId = 'N/A'
            mergeAttempt = 0
            mergeCommitSha = $null
            providerState = 'NotChecked'
            baseRefName = $null
            modelPreflight = if ($fallbackPolicy -eq 'fail-closed') { 'Pending' } else { $script:UndeclaredRunnerMetadata }
            lastPreflightAt = $null
        }.GetEnumerator()) {
            if (-not $workerState.ContainsKey($entry.Key)) {
                $workerState[$entry.Key] = $entry.Value
            }
        }
    }

    if (@($CampaignState.postMergeActions).Count -eq 0 -and $Campaign.ContainsKey('postMergeActions')) {
        $CampaignState.postMergeActions = @($Campaign.postMergeActions | ForEach-Object {
            [ordered]@{
                actionId = [string] $_.actionId
                workerId = [string] $_.workerId
                phase = [string] $_.phase
                profile = [string] $_.profile
                status = 'Pending'
                attemptCount = 0
                exitCode = $null
                startedAt = $null
                completedAt = $null
                summary = 'Not started.'
            }
        })
    }
}

function Set-PARStateTimestamp {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Mutates only the in-memory campaign state passed by the caller.')]
    param([Parameter(Mandatory)][hashtable] $CampaignState)
    $CampaignState.updatedAt = [DateTime]::UtcNow.ToString('o')
}

function ConvertTo-PARArgumentList {
    param(
        [Parameter(Mandatory)][object[]] $Arguments,
        [Parameter(Mandatory)][hashtable] $Values
    )

    $expanded = foreach ($argument in $Arguments) {
        $value = [string] $argument
        foreach ($key in $Values.Keys) {
            $value = $value.Replace("{$key}", [string] $Values[$key])
        }
        $value
    }
    return @($expanded)
}

function Invoke-PARProcessAsync {
    param(
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][string[]] $Arguments,
        [Parameter(Mandatory)][string] $WorkingDirectory,
        [Parameter(Mandatory)][string] $StdoutPath,
        [Parameter(Mandatory)][string] $StderrPath
    )

    $psi = [Diagnostics.ProcessStartInfo]::new()
    $psi.FileName = $Executable
    $psi.WorkingDirectory = $WorkingDirectory
    $psi.UseShellExecute = $false
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $true
    foreach ($argument in $Arguments) {
        [void] $psi.ArgumentList.Add($argument)
    }
    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $psi
    $stdoutStream = $null
    $stderrStream = $null
    try {
        $stdoutStream = [IO.FileStream]::new($StdoutPath, [IO.FileMode]::CreateNew,
            [IO.FileAccess]::Write, [IO.FileShare]::Read)
        $stderrStream = [IO.FileStream]::new($StderrPath, [IO.FileMode]::CreateNew,
            [IO.FileAccess]::Write, [IO.FileShare]::Read)
        Assert-PARCondition $process.Start() "Runner konnte nicht gestartet werden: $Executable"
    } catch {
        if ($null -ne $stdoutStream) { $stdoutStream.Dispose() }
        if ($null -ne $stderrStream) { $stderrStream.Dispose() }
        $process.Dispose()
        throw
    }
    return @{
        Process = $process
        StdoutTask = $process.StandardOutput.BaseStream.CopyToAsync($stdoutStream)
        StderrTask = $process.StandardError.BaseStream.CopyToAsync($stderrStream)
        StdoutStream = $stdoutStream
        StderrStream = $stderrStream
        StartedAt = [DateTime]::UtcNow
        StdoutPath = $StdoutPath
        StderrPath = $StderrPath
    }
}

function Confirm-PARAutonomousState {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][string] $Worktree,
        [Parameter(Mandatory)][hashtable] $Result
    )

    if (-not [bool] $Campaign.requireAutonomousPreset -or [string] $Result.status -eq 'Failed') { return }
    $relative = [string] $Result.autonomousStatePath
    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($relative) -and $relative -ne 'N/A' -and
        -not [IO.Path]::IsPathRooted($relative) -and $relative -notmatch '\\|(^|/)\.\.?(/|$)' -and
        $relative -match '^specs/.+/autonomous-run-state\.json$') `
        "Worker '$($Worker.workerId)' benoetigt einen worktree-relativen Autonomous-State-Pfad."
    $root = Resolve-PARPhysicalPath $Worktree
    $path = Resolve-PARPhysicalPath (Join-Path $root $relative)
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    $prefix = $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
    Assert-PARCondition ($path.StartsWith($prefix, $comparison) -and
        (Test-Path -LiteralPath $path -PathType Leaf)) "Autonomous-State verlaesst den Worker-Checkout: $relative"
    Assert-PARCondition ([string] $Result.autonomousStateSha256 -match '^[0-9a-f]{64}$' -and
        (Get-PARSha256 $path) -ceq [string] $Result.autonomousStateSha256) `
        "Autonomous-State-Hash stimmt nicht mit dem Worker-Result ueberein: $relative"

    # The validator beside this coordinator is trusted; never execute a worker-writable validator.
    $presetParent = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
    $validatorCandidates = @(
        (Join-Path $presetParent 'autonomous-run-governance/scripts/validate-autonomous-run-state.ps1'),
        (Join-Path $presetParent 'spec-kit-preset-autonomous-run-governance/scripts/validate-autonomous-run-state.ps1')
    )
    $validator = @($validatorCandidates | Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } | Select-Object -First 1)
    Assert-PARCondition ($validator.Count -eq 1) 'Vertrauenswuerdiger Autonomous-State-Validator fehlt neben dem Coordinator.'
    $validatorOutput = @(& pwsh -NoProfile -File $validator[0] -State $path 2>&1)
    Assert-PARCondition ($LASTEXITCODE -eq 0) `
        "Autonomous-State-Validator lehnt Worker '$($Worker.workerId)' ab: $($validatorOutput -join [Environment]::NewLine)"
    $state = Read-PARJson $path
    $expectedDelivery = if ($Campaign.deliveryMode -eq 'MergeAndSync') { 'PublishPR' } else { [string] $Campaign.deliveryMode }
    $featureDirectory = [IO.Path]::GetRelativePath($root, (Split-Path -Parent $path)).Replace('\', '/')
    Assert-PARCondition ([string] $state.runId -eq [string] $Worker.runId -and
        [string] $state.branch -ceq [string] $Worker.branch -and
        [string] $state.featurePath -ceq $featureDirectory -and
        [string] $state.deliveryMode -ceq $expectedDelivery -and
        [string] $state.status -ceq 'Completed' -and
        $state.authorityRevalidationRequired -eq $false) `
        "Autonomous-State ist nicht als abgeschlossener Lauf dieses Workers gebunden: $relative"
}

function Complete-PARWorker {
    param(
        [Parameter(Mandatory)][hashtable] $RunningEntry,
        [Parameter(Mandatory)][hashtable] $WorkerState,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath,
        [Parameter(Mandatory)][string] $ResultPath
    )

    $process = $RunningEntry.Process.Process
    try {
        [void] $RunningEntry.Process.StdoutTask.GetAwaiter().GetResult()
        [void] $RunningEntry.Process.StderrTask.GetAwaiter().GetResult()
        $outputExceeded = $RunningEntry.Process.StdoutStream.Length -gt 16MB -or
            $RunningEntry.Process.StderrStream.Length -gt 16MB
    } finally {
        $RunningEntry.Process.StdoutStream.Dispose()
        $RunningEntry.Process.StderrStream.Dispose()
    }
    $WorkerState.exitCode = $process.ExitCode
    $WorkerState.processId = $null

    if ($outputExceeded -or $RunningEntry.Process.ContainsKey('FailureReason')) {
        $WorkerState.status = 'Failed'
        $WorkerState.summary = if ($RunningEntry.Process.ContainsKey('FailureReason')) {
            [string] $RunningEntry.Process.FailureReason
        } else { 'Runner output limit exceeded 16 MiB per stream.' }
        return
    }

    if ($process.ExitCode -ne 0) {
        $WorkerState.status = 'Failed'
        $WorkerState.summary = "Runner exit code $($process.ExitCode)."
        return
    }

    if (-not (Test-Path -LiteralPath $ResultPath -PathType Leaf)) {
        Assert-PARCondition ($Campaign.deliveryMode -ne 'PublishPR') `
            "PublishPR worker '$($Worker.workerId)' muss ein explizites PR-Ergebnis liefern."
        $head = (Invoke-PARGit $RunningEntry.Worktree @('rev-parse', 'HEAD')).Output[0]
        $autonomousStates = @(Get-ChildItem -LiteralPath (Join-Path $RunningEntry.Worktree 'specs') -Filter 'autonomous-run-state.json' -File -Recurse -ErrorAction SilentlyContinue | Sort-Object LastWriteTimeUtc -Descending)
        if ([bool] $Campaign.requireAutonomousPreset -and $autonomousStates.Count -eq 0) {
            $WorkerState.status = 'Failed'
            $WorkerState.summary = 'Runner succeeded but produced no autonomous-run-state.json.'
            return
        }
        $statePath = if ($autonomousStates.Count -gt 0) { $autonomousStates[0].FullName } else { 'N/A' }
        $stateHash = if ($statePath -ne 'N/A') { Get-PARSha256 $statePath } else { 'N/A' }
        $relativeState = if ($statePath -ne 'N/A') { [IO.Path]::GetRelativePath($RunningEntry.Worktree, $statePath) } else { 'N/A' }
        $derivedStatus = if ($Campaign.deliveryMode -eq 'LocalImplementation') { 'Completed' } else { 'ReadyForMerge' }
        $derivedHandoffs = @()
        foreach ($declaredHandoff in @($Worker.handoffs)) {
            $handoffPath = Resolve-PARHandoffPath $RunningEntry.Worktree ([string] $declaredHandoff.path)
            $derivedHandoffs += [ordered]@{
                consumerWorkerId = [string] $declaredHandoff.consumerWorkerId
                path = [string] $declaredHandoff.path
                sha256 = Get-PARSha256 $handoffPath
            }
        }
        $result = [ordered]@{
            schemaVersion = '1.1'
            campaignId = [string] $Campaign.campaignId
            workerId = [string] $Worker.workerId
            runId = [string] $Worker.runId
            attemptId = [string] $RunningEntry.AttemptId
            status = $derivedStatus
            headSha = [string] $head
            autonomousStatePath = $relativeState
            autonomousStateSha256 = $stateHash
            evidencePath = 'N/A'
            prUrl = $null
            handoffs = @($derivedHandoffs)
            summary = 'Derived from successful runner exit.'
        }
        Write-PARJsonAtomic $ResultPath $result
    }

    $resultData = Read-PARJson $ResultPath
    Assert-PARSchemaVersion $resultData "Worker result '$ResultPath'"
    Assert-PARCondition ($resultData.campaignId -eq $Campaign.campaignId) "Worker result campaignId stimmt nicht: $ResultPath"
    Assert-PARCondition ($resultData.workerId -eq $Worker.workerId) "Worker result workerId stimmt nicht: $ResultPath"
    Assert-PARCondition ($resultData.runId -eq $Worker.runId) "Worker result runId stimmt nicht: $ResultPath"
    Assert-PARCondition ([string] $resultData.attemptId -eq [string] $RunningEntry.AttemptId) `
        "Worker result stammt nicht aus dem aktuellen Ausfuehrungsversuch: $ResultPath"
    Assert-PARCondition (@('Completed', 'ReadyForMerge', 'Failed') -contains $resultData.status) "Worker result status ist ungueltig: $ResultPath"
    if ($Campaign.deliveryMode -eq 'MergeAndSync') {
        Assert-PARCondition ($resultData.status -eq 'ReadyForMerge') "MergeAndSync worker must stop at ReadyForMerge: $ResultPath"
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $resultData.prUrl)) "MergeAndSync worker result requires prUrl: $ResultPath"
    }
    if ($Campaign.deliveryMode -eq 'PublishPR') {
        Assert-PARCondition ($resultData.status -eq 'ReadyForMerge' -and
            -not [string]::IsNullOrWhiteSpace([string] $resultData.prUrl)) `
            "PublishPR worker '$($Worker.workerId)' benoetigt ReadyForMerge und eine PR-URL."
    }
    $actualHead = (Invoke-PARGit $RunningEntry.Worktree @('rev-parse', 'HEAD')).Output[0]
    Assert-PARCondition ($resultData.headSha -eq $actualHead) "Worker result headSha ist nicht der aktuelle Worktree-Head: $ResultPath"
    Confirm-PARAutonomousState $Campaign $Worker $RunningEntry.Worktree $resultData

    $validatedHandoffs = @()
    foreach ($declaredHandoff in @($Worker.handoffs)) {
        $resultHandoffs = @($resultData.handoffs | Where-Object {
            $_.consumerWorkerId -eq $declaredHandoff.consumerWorkerId -and $_.path -eq $declaredHandoff.path
        })
        Assert-PARCondition ($resultHandoffs.Count -eq 1) "Worker '$($Worker.workerId)' lieferte den deklarierten Handoff zu '$($declaredHandoff.consumerWorkerId)' nicht eindeutig."
        $handoffPath = Resolve-PARHandoffPath $RunningEntry.Worktree ([string] $declaredHandoff.path)
        $actualHash = Get-PARSha256 $handoffPath
        Assert-PARCondition ($resultHandoffs[0].sha256 -eq $actualHash) "Handoff-Hash stimmt nicht: $handoffPath"
        $validatedHandoffs += [ordered]@{
            consumerWorkerId = [string] $declaredHandoff.consumerWorkerId
            path = [string] $declaredHandoff.path
            sourcePath = $handoffPath
            sha256 = $actualHash
        }
    }

    $WorkerState.status = [string] $resultData.status
    $WorkerState.headSha = [string] $resultData.headSha
    $WorkerState.autonomousStatePath = [string] $resultData.autonomousStatePath
    $WorkerState.autonomousStateSha256 = [string] $resultData.autonomousStateSha256
    $WorkerState.resultPath = $ResultPath
    $WorkerState.resultSha256 = Get-PARSha256 $ResultPath
    $WorkerState.attemptId = [string] $RunningEntry.AttemptId
    $WorkerState.prUrl = $resultData.prUrl
    $WorkerState.handoffs = @($validatedHandoffs)
    $WorkerState.summary = [string] $resultData.summary
    if ($Campaign.deliveryMode -eq 'PublishPR') {
        try {
            Confirm-PARPublicationWorkerState $Campaign $Profiles $Worker $WorkerState `
                $ManifestDirectory $RuntimePath
        } catch {
            $WorkerState.status = 'NeedsRevalidation'
            $WorkerState.summary = $_.Exception.Message
        }
    }
}

function Confirm-PARPublicationWorkerState {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][hashtable] $WorkerState,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath
    )

    Confirm-PARWorkerExecutionEvidence $Campaign $Worker $WorkerState $ManifestDirectory $RuntimePath
    $publicationProfile = $Profiles.publicationProfiles[[string] $Campaign.publicationProfile]
    $preflight = Invoke-PARProviderPreflight $Campaign $publicationProfile $Worker $WorkerState `
        $ManifestDirectory $RuntimePath -PublicationOnly
    $WorkerState.providerState = [string] $preflight.state
    $WorkerState.prNumber = [int] $preflight.prNumber
    $WorkerState.prBaseRepository = [string] $preflight.baseRepository
    $WorkerState.status = 'ReadyForMerge'
    $WorkerState.summary = 'Published PR identity and exact head verified by provider.'
}

function Confirm-PARWorkerExecutionEvidence {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][hashtable] $WorkerState,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath
    )

    $expectedWorktree = Resolve-PARPhysicalPath (Join-Path $RuntimePath "worktrees/$($Worker.workerId)")
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    Assert-PARCondition ([string]::Equals((Resolve-PARPhysicalPath ([string] $WorkerState.worktree)),
        $expectedWorktree, $comparison)) "Worker-Worktree passt nicht zur Kampagne: $($Worker.workerId)"
    $repository = Resolve-PARRepository ([string] $Worker.repository) $ManifestDirectory
    $registeredOutput = (Invoke-PARGit $repository @('worktree', 'list', '--porcelain')).Output
    $registered = @($registeredOutput | Where-Object { $_ -like 'worktree *' } |
        ForEach-Object { $_.Substring(9) })
    Assert-PARCondition (@($registered | Where-Object {
        (Test-Path -LiteralPath $_ -PathType Container) -and
        [string]::Equals((Resolve-PARPhysicalPath $_), $expectedWorktree, $comparison)
    }).Count -eq 1) "Worker-Worktree ist nicht beim deklarierten Repository registriert: $($Worker.workerId)"
    $attemptId = [string] $WorkerState.attemptId
    Assert-PARCondition (Test-PARUuid $attemptId) "Worker-Versuchs-ID fehlt: $($Worker.workerId)"
    $expectedResult = Join-Path $RuntimePath "results/$($Worker.workerId)-$attemptId/result.json"
    Assert-PARCondition ([string]::Equals([IO.Path]::GetFullPath([string] $WorkerState.resultPath),
        [IO.Path]::GetFullPath($expectedResult), $comparison) -and
        (Test-Path -LiteralPath $expectedResult -PathType Leaf)) `
        "Worker-Ergebnis fehlt im geschuetzten Versuchspfad: $($Worker.workerId)"
    Assert-PARCondition ([string] $WorkerState.resultSha256 -match '^[0-9a-f]{64}$' -and
        (Get-PARSha256 $expectedResult) -ceq [string] $WorkerState.resultSha256) `
        "Worker-Ergebnis wurde nach dem Lauf veraendert: $($Worker.workerId)"
    $result = Read-PARJson $expectedResult
    $expectedStatus = if ($Campaign.deliveryMode -eq 'LocalImplementation') { 'Completed' } else { 'ReadyForMerge' }
    Assert-PARCondition ([string] $result.campaignId -ceq [string] $Campaign.campaignId -and
        [string] $result.workerId -ceq [string] $Worker.workerId -and
        [string] $result.runId -ceq [string] $Worker.runId -and
        [string] $result.attemptId -ceq $attemptId -and
        [string] $result.status -ceq $expectedStatus -and
        [string] $result.headSha -ceq [string] $WorkerState.headSha -and
        [string] $result.prUrl -ceq [string] $WorkerState.prUrl) `
        "Worker-Ergebnis passt nicht zur Kampagne: $($Worker.workerId)"
    $head = (Invoke-PARGit $expectedWorktree @('rev-parse', 'HEAD')).Output[0]
    $branch = (Invoke-PARGit $expectedWorktree @('branch', '--show-current')).Output[0]
    Assert-PARCondition ([string] $head -ceq [string] $WorkerState.headSha -and
        [string] $branch -ceq [string] $Worker.branch) `
        "Worker-Worktree-Head/Branch ist gedriftet: $($Worker.workerId)"
    Confirm-PARAutonomousState $Campaign $Worker $expectedWorktree $result
}

function Invoke-PARStart {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestPath,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $StatePath,
        [Parameter(Mandatory)][string] $RuntimePath,
        [switch] $IsResume
    )

    $campaignLock = Enter-PARCampaignLock $RuntimePath

    try {
        if (Test-Path -LiteralPath $StatePath -PathType Leaf) {
            $campaignState = Read-PARJson $StatePath
            Initialize-PARStateShape $campaignState $Campaign $Profiles
            Assert-PARCondition ($campaignState.campaignId -eq $Campaign.campaignId) 'State campaignId stimmt nicht mit Manifest ueberein.'
            Assert-PARCondition ($campaignState.manifestSha256 -eq (Get-PARSha256 $ManifestPath)) 'Manifest hat sich seit dem State-Checkpoint geaendert.'
            foreach ($workerState in @($campaignState.workers | Where-Object {
                $_.status -in @('Completed', 'ReadyForMerge', 'Merged', 'NeedsRevalidation')
            })) {
                $worker = Get-PARCampaignWorker $Campaign ([string] $workerState.workerId)
                Confirm-PARWorkerExecutionEvidence $Campaign $worker $workerState $ManifestDirectory $RuntimePath
            }
            if (-not $IsResume) {
                Assert-PARCondition (@('Prepared', 'PausedByUser', 'Interrupted') -contains $campaignState.status) 'Bestehende Campaign ist nicht startbar; Resume oder Status verwenden.'
            }
        } else {
            $campaignState = Get-PARInitialCampaignState $Campaign $Profiles $ManifestPath
        }

        if ($IsResume) {
            Assert-PARCondition (@('PausedByUser', 'Interrupted', 'Failed', 'NeedsRevalidation') -contains $campaignState.status) 'Campaign ist nicht in einem Resume-faehigen Zustand.'
            foreach ($workerState in $campaignState.workers) {
                if (@('Interrupted', 'Running') -contains $workerState.status) {
                    $workerState.status = 'Pending'
                    $workerState.processId = $null
                    $workerState.summary = 'Revalidated for resume.'
                }
                if ($Campaign.deliveryMode -eq 'PublishPR' -and
                    $workerState.status -eq 'NeedsRevalidation') {
                    $worker = Get-PARCampaignWorker $Campaign ([string] $workerState.workerId)
                    try {
                        Confirm-PARPublicationWorkerState $Campaign $Profiles $worker $workerState `
                            $ManifestDirectory $RuntimePath
                    } catch {
                        $workerState.summary = $_.Exception.Message
                    }
                }
            }
        }
        Remove-Item -LiteralPath "$([IO.Path]::GetFullPath($StatePath)).stop-requested" `
            -Force -ErrorAction SilentlyContinue
        $campaignState.stopRequested = $false

        $campaignState.attempts.execute = [int] $campaignState.attempts.execute + 1
        $campaignState.status = 'Active'
        $campaignState.phase = 'Execute'
        Add-PARStateEvent $campaignState $(if ($IsResume) { 'ExecutionResumed' } else { 'ExecutionStarted' }) `
            'Worker scheduling started.' -Attempt ([int] $campaignState.attempts.execute)
        Set-PARStateTimestamp $campaignState
        Write-PARJsonAtomic $StatePath $campaignState

        $running = @{}
        $worktreesRoot = Join-Path $RuntimePath 'worktrees'
        $logsRoot = Join-Path $RuntimePath 'logs'
        $resultsRoot = Join-Path $RuntimePath 'results'
        $promptsRoot = Join-Path $RuntimePath 'prompts'
        foreach ($path in @($worktreesRoot, $logsRoot, $resultsRoot, $promptsRoot)) {
            [void](New-Item -ItemType Directory -Path $path -Force)
        }

        while ($true) {
            $campaignState = Read-PARJson $StatePath
            Sync-PARStopRequest $campaignState $StatePath
            $madeProgress = $false

            foreach ($worker in $Campaign.workers) {
                $workerState = Get-PARWorkerState $campaignState ([string] $worker.workerId)
                if ($workerState.status -ne 'Pending') {
                    continue
                }
                $dependencyStates = @($worker.dependsOn | ForEach-Object { (Get-PARWorkerState $campaignState ([string] $_)).status })
                if (@($dependencyStates | Where-Object { $_ -in @('Failed', 'Blocked', 'Interrupted') }).Count -gt 0) {
                    $workerState.status = 'Blocked'
                    $workerState.summary = 'A required dependency did not complete.'
                    $madeProgress = $true
                    continue
                }
                if (@($dependencyStates | Where-Object { $_ -notin @('Completed', 'ReadyForMerge', 'Merged') }).Count -gt 0) {
                    continue
                }
                if ([bool] $campaignState.stopRequested -or $running.Count -ge [int] $Campaign.maxConcurrency) {
                    continue
                }
                $incomingHandoffs = @()
                foreach ($dependency in @($worker.dependsOn)) {
                    $dependencyState = Get-PARWorkerState $campaignState ([string] $dependency)
                    $matchingHandoffs = @($dependencyState.handoffs | Where-Object { $_.consumerWorkerId -eq $worker.workerId })
                    Assert-PARCondition ($matchingHandoffs.Count -eq 1) "Validierter Handoff '$dependency' -> '$($worker.workerId)' fehlt."
                    $handoffSource = Resolve-PARHandoffPath ([string] $dependencyState.worktree) ([string] $matchingHandoffs[0].path)
                    Assert-PARCondition ($handoffSource -eq [string] $matchingHandoffs[0].sourcePath) `
                        "Handoff-Quelle wurde nach Validierung umgeleitet: $handoffSource"
                    Assert-PARCondition ((Get-PARSha256 $handoffSource) -eq $matchingHandoffs[0].sha256) "Handoff-Quelle wurde nach Validierung veraendert: $handoffSource"
                    $runtimePhysical = Resolve-PARPhysicalPath $RuntimePath
                    $verifiedRoot = Join-Path $runtimePhysical "verified-handoffs-$([guid]::NewGuid().ToString('N'))"
                    [void](New-Item -ItemType Directory -Path $verifiedRoot -ErrorAction Stop)
                    Assert-PARCondition (((Get-Item -LiteralPath $verifiedRoot -Force).Attributes -band
                        [IO.FileAttributes]::ReparsePoint) -eq 0) 'Verifizierter Handoff-Ordner ist umgeleitet.'
                    $verifiedPath = Join-Path $verifiedRoot 'handoff.bin'
                    [IO.File]::Copy($handoffSource, $verifiedPath, $false)
                    Assert-PARCondition ((Get-PARSha256 $verifiedPath) -eq $matchingHandoffs[0].sha256) `
                        "Handoff-Kopie stimmt nicht mit dem validierten Hash ueberein: $handoffSource"
                    $incomingHandoffs += [ordered]@{
                        producerWorkerId = [string] $dependency
                        sourcePath = $verifiedPath
                        sha256 = [string] $matchingHandoffs[0].sha256
                    }
                }
                $runnerProfileName = Get-PARRunnerProfileName $Campaign $worker
                $runnerProfileData = $Profiles.profiles[$runnerProfileName]
                $repository = Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory
                $worktree = Join-Path $worktreesRoot ([string] $worker.workerId)
                if (-not (Test-Path -LiteralPath $worktree -PathType Container)) {
                    $branchExists = Invoke-PARGit $repository @('show-ref', '--verify', '--quiet', "refs/heads/$($worker.branch)") -AllowFailure
                    Assert-PARCondition ($branchExists.ExitCode -ne 0) "Branch existiert bereits: $($worker.branch)"
                    $baseWorkerId = if ($worker.ContainsKey('baseWorkerId')) { [string] $worker.baseWorkerId } else { '' }
                    if ([string]::IsNullOrWhiteSpace($baseWorkerId)) {
                        $startPoint = [string] $worker.baseRef
                    } else {
                        $baseWorkerState = Get-PARWorkerState $campaignState $baseWorkerId
                        Assert-PARCondition (@('Completed', 'ReadyForMerge', 'Merged') -contains $baseWorkerState.status) "baseWorkerId '$baseWorkerId' ist nicht bereit."
                        $startPoint = [string] $baseWorkerState.headSha
                        $baseCommit = Invoke-PARGit $repository @('rev-parse', '--verify', "$startPoint^{commit}") -AllowFailure
                        Assert-PARCondition ($baseCommit.ExitCode -eq 0) "Head von baseWorkerId '$baseWorkerId' fehlt in $repository."
                    }
                    $add = Invoke-PARGit $repository @('worktree', 'add', '-b', [string] $worker.branch, $worktree, $startPoint)
                    Assert-PARCondition ($add.ExitCode -eq 0) "Worktree konnte nicht erstellt werden: $worktree"
                }

                $promptPath = Join-Path $promptsRoot "$($worker.workerId).txt"
                $attemptId = [guid]::NewGuid().ToString('N')
                # The sandbox grants only this worker's result directory.
                $workerResultRoot = Join-Path $resultsRoot "$($worker.workerId)-$attemptId"
                [void](New-Item -ItemType Directory -Path $workerResultRoot)
                $resultPath = Join-Path $workerResultRoot 'result.json'
                $featureInput = Resolve-PARFeatureInput $worktree ([string] $worker.featureInput)
                if ([bool] $Campaign.requireAutonomousPreset) {
                    Assert-PARCondition (Test-PARAutonomousPreset $worktree) `
                        "autonomous-run-governance >= 0.2.2 fehlt oder ist im Worker-Checkout deaktiviert: $worktree"
                }
                if ([bool] $script:CurrentIntakeReview.required) {
                    $expectedInputHash = $script:CurrentIntakeWorkerHashes[[string] $worker.workerId]
                    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $expectedInputHash) -and
                        (Get-PARNormalizedTextSha256 $featureInput) -eq $expectedInputHash) `
                        "featureInput im Worker-Checkout stimmt nicht mit dem Intake-Review ueberein: $($worker.workerId)"
                }
                $handoffInstructions = if (@($worker.handoffs).Count -eq 0) {
                    'No outgoing handoff file is required.'
                } else {
                    "Create every outgoing handoff file declared in this JSON before finishing: $($worker.handoffs | ConvertTo-Json -Depth 10 -Compress)"
                }
                $incomingHandoffInstructions = if ($incomingHandoffs.Count -eq 0) {
                    'No incoming handoff is required.'
                } else {
                    "Treat these validated, immutable incoming handoffs as required input: $($incomingHandoffs | ConvertTo-Json -Depth 10 -Compress)"
                }
                $workerDeliveryMode = if ($Campaign.deliveryMode -eq 'MergeAndSync') { 'PublishPR' } else { [string] $Campaign.deliveryMode }
                $resultInstructions = if ($Campaign.deliveryMode -eq 'MergeAndSync') {
                    @"
The campaign retains all merge authority. Your worker delivery boundary is PublishPR: commit, push, create the PR, but do not merge it.
Before finishing, write worker-result schema 1.1 to: $resultPath
Set attemptId to $attemptId, status ReadyForMerge, headSha to the exact pushed head, prUrl to the created PR URL, autonomousStatePath and autonomousStateSha256 to the final state, and include every declared handoff with its SHA-256.
"@
                } else {
                    'The coordinator derives a local worker result when no explicit result file is written.'
                }
                $operatorInstructions = if ($Campaign.ContainsKey('operatorInstructions') -and
                    -not [string]::IsNullOrWhiteSpace([string] $Campaign.operatorInstructions)) {
                    [string] $Campaign.operatorInstructions
                } else {
                    'No campaign-specific operator instructions were declared.'
                }
                $prompt = @"
Execute the installed speckit.autonomous workflow for this worker.
Campaign ID: $($Campaign.campaignId)
Worker ID: $($worker.workerId)
Run ID: $($worker.runId)
Execution attempt ID: $attemptId
Campaign delivery mode: $($Campaign.deliveryMode)
Worker delivery mode: $workerDeliveryMode
Feature input: $featureInput
Remain on the assigned branch '$($worker.branch)'. Do not create or switch branches.
Campaign-specific operator instructions:
$operatorInstructions
$incomingHandoffInstructions
$handoffInstructions
$resultInstructions
Do not infer remote, merge, bypass, cancellation, secret, or provider authority.
"@
                $prompt | Set-Content -LiteralPath $promptPath -Encoding utf8NoBOM
                $handoffDeclarations = @($worker.handoffs)
                $handoffsJson = if ($handoffDeclarations.Count -eq 0) {
                    '[]'
                } else {
                    $handoffDeclarations | ConvertTo-Json -Depth 10 -Compress
                }
                $values = @{
                    campaignId = [string] $Campaign.campaignId
                    workerId = [string] $worker.workerId
                    runId = [string] $worker.runId
                    attemptId = $attemptId
                    worktree = $worktree
                    repository = $repository
                    branch = [string] $worker.branch
                    prompt = $prompt
                    promptFile = $promptPath
                    resultFile = $resultPath
                    resultDirectory = $workerResultRoot
                    handoffsJson = $handoffsJson
                    model = if ($runnerProfileData.ContainsKey('model')) { [string] $runnerProfileData.model } else { '' }
                    reasoningEffort = if ($runnerProfileData.ContainsKey('reasoningEffort')) { [string] $runnerProfileData.reasoningEffort } else { '' }
                    routingRole = Get-PARWorkerRoutingRole $Campaign $worker
                }
                $strictRouting = $Campaign.ContainsKey('fallbackPolicy') -and
                    [string] $Campaign.fallbackPolicy -eq 'fail-closed'
                if ($strictRouting) {
                    $modelPreflight = Invoke-PARProfileCommand $runnerProfileData.preflight $values $worktree
                    $workerState.lastPreflightAt = [DateTime]::UtcNow.ToString('o')
                    if ([int] $modelPreflight.ExitCode -ne 0) {
                        $workerState.status = 'Blocked'
                        $workerState.modelPreflight = 'Failed'
                        $workerState.summary = "Model preflight failed for runner profile '$runnerProfileName' with exit code $($modelPreflight.ExitCode)."
                        Add-PARStateEvent $campaignState 'ModelPreflightFailed' $workerState.summary `
                            -WorkerId ([string] $worker.workerId) -Attempt ([int] $workerState.executionAttempt)
                        $madeProgress = $true
                        continue
                    }
                    $workerState.modelPreflight = 'Completed'
                    Add-PARStateEvent $campaignState 'ModelPreflightCompleted' `
                        "Model preflight completed for runner profile '$runnerProfileName'." `
                        -WorkerId ([string] $worker.workerId) -Attempt ([int] $workerState.executionAttempt)
                }
                $arguments = ConvertTo-PARArgumentList @($runnerProfileData.arguments) $values
                $processInfo = Invoke-PARProcessAsync ([string] $runnerProfileData.executable) $arguments $worktree (Join-Path $logsRoot "$($worker.workerId)-$attemptId.out.log") (Join-Path $logsRoot "$($worker.workerId)-$attemptId.err.log")
                $workerState.status = 'Running'
                $workerState.worktree = $worktree
                $workerState.processId = $processInfo.Process.Id
                $workerState.executionAttempt = [int] $workerState.executionAttempt + 1
                $workerState.attemptId = $attemptId
                $workerState.summary = 'Runner active.'
                Add-PARStateEvent $campaignState 'WorkerStarted' "Worker started with runner profile '$runnerProfileName'." `
                    -WorkerId ([string] $worker.workerId) -Attempt ([int] $workerState.executionAttempt)
                $running[[string] $worker.workerId] = @{
                    Worker = $worker
                    Worktree = $worktree
                    ResultPath = $resultPath
                    AttemptId = $attemptId
                    Process = $processInfo
                }
                if ($running.Count -gt [int] $campaignState.maximumObservedConcurrency) {
                    $campaignState.maximumObservedConcurrency = $running.Count
                }
                $madeProgress = $true
            }

            foreach ($workerId in @($running.Keys)) {
                $entry = $running[$workerId]
                if (-not $entry.Process.Process.HasExited) {
                    if ($entry.Process.StdoutStream.Length -gt 16MB -or
                        $entry.Process.StderrStream.Length -gt 16MB -or
                        [DateTime]::UtcNow -gt $entry.Process.StartedAt.AddHours(4)) {
                        $entry.Process.FailureReason = 'Runner exceeded the output limit (16 MiB per stream) or time limit (4 hours).'
                        $entry.Process.Process.Kill($true)
                    }
                    continue
                }
                $workerState = Get-PARWorkerState $campaignState $workerId
                try {
                    Complete-PARWorker $entry $workerState $entry.Worker $Campaign $Profiles `
                        $ManifestDirectory $RuntimePath $entry.ResultPath
                } catch {
                    $workerState.status = 'Failed'
                    $workerState.summary = $_.Exception.Message
                }
                Add-PARStateEvent $campaignState 'WorkerFinished' $workerState.summary `
                    -WorkerId $workerId -Attempt ([int] $workerState.executionAttempt)
                $entry.Process.Process.Dispose()
                $running.Remove($workerId)
                $madeProgress = $true
            }

            Sync-PARStopRequest $campaignState $StatePath
            Set-PARStateTimestamp $campaignState
            Write-PARJsonAtomic $StatePath $campaignState

            $terminal = @('Completed', 'ReadyForMerge', 'Merged', 'Failed', 'Blocked')
            if ($Campaign.deliveryMode -eq 'PublishPR') { $terminal += 'NeedsRevalidation' }
            $nonTerminal = @($campaignState.workers | Where-Object { $_.status -notin $terminal })
            if ($nonTerminal.Count -eq 0 -and $running.Count -eq 0) {
                break
            }
            if ($Campaign.deliveryMode -eq 'PublishPR' -and $running.Count -eq 0 -and
                -not $madeProgress -and
                @($campaignState.workers | Where-Object status -eq 'NeedsRevalidation').Count -gt 0) {
                break
            }
            if ([bool] $campaignState.stopRequested -and $running.Count -eq 0) {
                $campaignState.status = 'PausedByUser'
                $campaignState.phase = 'Paused'
                Add-PARStateEvent $campaignState 'ExecutionPaused' 'Cooperative stop reached between worker starts.'
                Set-PARStateTimestamp $campaignState
                Write-PARJsonAtomic $StatePath $campaignState
                return
            }
            if (-not $madeProgress) {
                Start-Sleep -Milliseconds 200
            }
        }

        $campaignState = Read-PARJson $StatePath
        if ($Campaign.deliveryMode -eq 'PublishPR') {
            foreach ($workerState in @($campaignState.workers | Where-Object status -eq 'ReadyForMerge')) {
                $worker = Get-PARCampaignWorker $Campaign ([string] $workerState.workerId)
                try {
                    Confirm-PARPublicationWorkerState $Campaign $Profiles $worker $workerState `
                        $ManifestDirectory $RuntimePath
                } catch {
                    $workerState.status = 'NeedsRevalidation'
                    $workerState.summary = $_.Exception.Message
                }
            }
        }
        if (@($campaignState.workers | Where-Object { $_.status -eq 'Failed' }).Count -gt 0) {
            $campaignState.status = 'Failed'
        } elseif (@($campaignState.workers | Where-Object { $_.status -eq 'Blocked' }).Count -gt 0) {
            $campaignState.status = 'Blocked'
        } elseif (@($campaignState.workers | Where-Object { $_.status -eq 'NeedsRevalidation' }).Count -gt 0) {
            $campaignState.status = 'NeedsRevalidation'
        } elseif ($Campaign.topology -eq 'AlternativeSolutions') {
            $campaignState.status = 'AwaitingSelection'
        } elseif ($Campaign.deliveryMode -eq 'MergeAndSync') {
            Assert-PARCondition (@($campaignState.workers | Where-Object { $_.status -ne 'ReadyForMerge' }).Count -eq 0) 'MergeAndSync worker result must be ReadyForMerge.'
            $campaignState.status = 'ReadyForConsolidation'
        } else {
            $campaignState.status = 'Completed'
        }
        $campaignState.phase = if ($campaignState.status -eq 'Completed') { 'Completed' } else { 'Consolidate' }
        Add-PARStateEvent $campaignState 'ExecutionFinished' "Execution phase ended with status '$($campaignState.status)'." `
            -Attempt ([int] $campaignState.attempts.execute)
        Set-PARStateTimestamp $campaignState
        Write-PARJsonAtomic $StatePath $campaignState
    } finally {
        Exit-PARCampaignLock $campaignLock
    }
}

function Get-PARCampaignWorker {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][string] $WorkerId
    )

    return @($Campaign.workers | Where-Object { $_.workerId -eq $WorkerId })[0]
}

function Test-PARConsolidationPause {
    param(
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][string] $StatePath
    )

    Sync-PARStopRequest $CampaignState $StatePath
    if (-not [bool] $CampaignState.stopRequested) {
        return $false
    }
    $CampaignState.status = 'PausedByUser'
    $CampaignState.phase = 'ConsolidationPaused'
    Add-PARStateEvent $CampaignState 'ConsolidationPaused' 'Cooperative stop reached between consolidation operations.'
    Set-PARStateTimestamp $CampaignState
    Write-PARJsonAtomic $StatePath $CampaignState
    return $true
}

function Invoke-PARProviderPreflight {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $MergeProfile,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][hashtable] $WorkerState,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath,
        [switch] $PublicationOnly
    )

    Assert-PARCondition ($MergeProfile.ContainsKey('preflight') -and
        $MergeProfile.preflight -is [hashtable]) 'Merge-Profil benoetigt einen providergebundenen Preflight-Vertrag.'
    $preflightRoot = Join-Path $RuntimePath 'provider-preflight'
    [void](New-Item -ItemType Directory -Path $preflightRoot -Force)
    $resultPath = Join-Path $preflightRoot "$($Worker.workerId).json"
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
    $repository = Resolve-PARRepository ([string] $Worker.repository) $ManifestDirectory
    $expectedIdentity = Get-PARExpectedPrIdentity $Campaign $Worker $ManifestDirectory ([string] $MergeProfile.provider)
    $values = @{
        campaignId = [string] $Campaign.campaignId
        workerId = [string] $Worker.workerId
        repository = $repository
        branch = [string] $Worker.branch
        prUrl = [string] $WorkerState.prUrl
        headSha = [string] $WorkerState.headSha
        preflightResultFile = $resultPath
    }
    $commandResult = Invoke-PARProfileCommand $MergeProfile.preflight $values $repository
    Assert-PARCondition ($commandResult.ExitCode -eq 0) `
        "Provider-Preflight fuer Worker '$($Worker.workerId)' endete mit Exitcode $($commandResult.ExitCode)."
    Assert-PARCondition (Test-Path -LiteralPath $resultPath -PathType Leaf) `
        "Provider-Preflight fuer Worker '$($Worker.workerId)' erzeugte keinen Vertrag."
    $preflight = Read-PARJson $resultPath
    Assert-PARSchemaVersion $preflight "Provider preflight '$($Worker.workerId)'"
    $requiredFields = @('prUrl', 'state', 'headSha', 'headRepository', 'headRefName',
        'baseRepository', 'baseRefName', 'prNumber')
    if (-not $PublicationOnly) {
        $requiredFields += @('isDraft', 'mergeable', 'reviewDecision',
            'unresolvedCurrentThreads', 'checkPolicySatisfied', 'technicalFailures', 'mergeCommitSha')
    }
    foreach ($requiredField in $requiredFields) {
        Assert-PARCondition $preflight.ContainsKey($requiredField) `
            "Provider-Preflight '$($Worker.workerId)' enthaelt '$requiredField' nicht."
    }
    Assert-PARCondition ([string] $preflight.prUrl -eq [string] $WorkerState.prUrl) `
        "Provider-Preflight verweist auf eine andere PR fuer Worker '$($Worker.workerId)'."
    Assert-PARCondition ([string] $preflight.headSha -eq [string] $WorkerState.headSha) `
        "PR-Head-Drift bei Worker '$($Worker.workerId)'."
    foreach ($field in @('headRefName', 'baseRefName')) {
        Assert-PARCondition ([string] $preflight[$field] -ceq [string] $expectedIdentity[$field]) `
            "Provider-PR-Identitaet '$field' passt nicht zur Kampagne fuer Worker '$($Worker.workerId)'."
    }
    foreach ($field in @('headRepository', 'baseRepository')) {
        Assert-PARCondition ([string] $preflight[$field] -eq [string] $expectedIdentity[$field]) `
            "Provider-PR-Identitaet '$field' passt nicht zur Kampagne fuer Worker '$($Worker.workerId)'."
    }
    $prNumber = 0
    Assert-PARCondition ([int]::TryParse([string] $preflight.prNumber, [ref] $prNumber) -and
        $prNumber -gt 0) "Provider-PR hat keine gueltige Nummer fuer Worker '$($Worker.workerId)'."
    if ([string] $MergeProfile.provider -eq 'GitHub') {
        $canonicalUrl = "https://$($expectedIdentity.baseRepository)/pull/$prNumber"
        $prUri = $null
        Assert-PARCondition ([Uri]::TryCreate([string] $preflight.prUrl, [UriKind]::Absolute, [ref] $prUri) -and
            $prUri.Scheme -eq 'https' -and $prUri.Query -eq '' -and $prUri.Fragment -eq '' -and
            $prUri.AbsoluteUri.TrimEnd('/') -eq $canonicalUrl) `
            "Provider-PR-URL liegt nicht im deklarierten Zielrepository fuer Worker '$($Worker.workerId)'."
    }
    Assert-PARCondition (@('Open', 'Merged', 'Closed') -contains [string] $preflight.state) `
        "Provider-Preflight meldet einen ungueltigen PR-Zustand fuer Worker '$($Worker.workerId)'."

    if ($PublicationOnly) {
        Assert-PARCondition ([string] $preflight.state -eq 'Open') `
            "PublishPR worker '$($Worker.workerId)' benoetigt eine offene Provider-PR."
    } elseif ([string] $preflight.state -eq 'Open') {
        Assert-PARCondition (-not [bool] $preflight.isDraft) "PR von Worker '$($Worker.workerId)' ist ein Draft."
        Assert-PARCondition ([bool] $preflight.mergeable) "PR von Worker '$($Worker.workerId)' ist nicht mergebar."
        Assert-PARCondition ([string] $preflight.reviewDecision -ne 'ChangesRequested') `
            "PR von Worker '$($Worker.workerId)' hat eine aktuelle Change Request."
        Assert-PARCondition ([int] $preflight.unresolvedCurrentThreads -eq 0) `
            "PR von Worker '$($Worker.workerId)' hat aktuelle ungeloeste Review-Threads."
        Assert-PARCondition ([bool] $preflight.checkPolicySatisfied) `
            "PR von Worker '$($Worker.workerId)' erfuellt die Check-Policy nicht."
        Assert-PARCondition (@($preflight.technicalFailures).Count -eq 0) `
            "PR von Worker '$($Worker.workerId)' hat technische Checkfehler."
    } elseif ([string] $preflight.state -eq 'Merged') {
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $preflight.mergeCommitSha)) `
            "Gemergte PR von Worker '$($Worker.workerId)' hat keinen Merge-Commit."
    } else {
        throw "PR von Worker '$($Worker.workerId)' ist geschlossen, aber nicht gemergt."
    }

    return $preflight
}

function Confirm-PARWorkerProviderState {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $MergeProfile,
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][string] $WorkerId,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath,
        [Parameter(Mandatory)][string] $StatePath
    )

    $worker = Get-PARCampaignWorker $Campaign $WorkerId
    $workerState = Get-PARWorkerState $CampaignState $WorkerId
    try {
        Confirm-PARWorkerExecutionEvidence $Campaign $worker $workerState $ManifestDirectory $RuntimePath
        $preflight = Invoke-PARProviderPreflight $Campaign $MergeProfile $worker $workerState $ManifestDirectory $RuntimePath
        Sync-PARStopRequest $CampaignState $StatePath
        $workerState.providerState = [string] $preflight.state
        $workerState.baseRefName = [string] $preflight.baseRefName
        $workerState.prNumber = [int] $preflight.prNumber
        $workerState.prBaseRepository = [string] $preflight.baseRepository
        $workerState.lastPreflightAt = [DateTime]::UtcNow.ToString('o')
        if ([string] $preflight.state -eq 'Merged') {
            $workerState.status = 'Merged'
            $workerState.mergeCommitSha = [string] $preflight.mergeCommitSha
            $workerState.summary = 'Provider reports the exact expected head as merged.'
        } elseif ($workerState.status -eq 'Merged') {
            throw "Worker '$WorkerId' war lokal Merged, die Provider-PR ist jedoch offen."
        } elseif ($workerState.status -eq 'NeedsRevalidation') {
            $workerState.status = 'ReadyForMerge'
            $workerState.summary = 'Provider state revalidated.'
        }
        Add-PARStateEvent $CampaignState 'ProviderPreflightPassed' `
            "Provider preflight passed with state '$($preflight.state)'." -WorkerId $WorkerId `
            -Attempt ([int] $CampaignState.attempts.consolidation)
        Set-PARStateTimestamp $CampaignState
        Write-PARJsonAtomic $StatePath $CampaignState
        return $preflight
    } catch {
        Sync-PARStopRequest $CampaignState $StatePath
        $workerState.status = 'NeedsRevalidation'
        $workerState.summary = $_.Exception.Message
        $CampaignState.status = 'NeedsRevalidation'
        $CampaignState.phase = 'ProviderPreflight'
        Add-PARStateEvent $CampaignState 'ProviderPreflightFailed' $_.Exception.Message -WorkerId $WorkerId `
            -Attempt ([int] $CampaignState.attempts.consolidation)
        Set-PARStateTimestamp $CampaignState
        Write-PARJsonAtomic $StatePath $CampaignState
        throw
    }
}

function Set-PARCompletionFlags {
    [Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUseShouldProcessForStateChangingFunctions', '', Justification = 'Mutates only the in-memory campaign state passed by the caller.')]
    param([Parameter(Mandatory)][hashtable] $CampaignState)

    $mergeWorkers = if (-not [string]::IsNullOrWhiteSpace([string] $CampaignState.selectedWorkerId)) {
        @($CampaignState.workers | Where-Object workerId -eq $CampaignState.selectedWorkerId)
    } else {
        @($CampaignState.workers)
    }
    $CampaignState.completion.mergeComplete =
        @($mergeWorkers | Where-Object status -ne 'Merged').Count -eq 0
    $eligibleActions = if (-not [string]::IsNullOrWhiteSpace([string] $CampaignState.selectedWorkerId)) {
        @($CampaignState.postMergeActions | Where-Object workerId -eq $CampaignState.selectedWorkerId)
    } else { @($CampaignState.postMergeActions) }
    $syncActions = @($eligibleActions | Where-Object phase -eq 'Synchronize')
    $postActions = @($eligibleActions | Where-Object phase -eq 'PostMerge')
    $validationActions = @($eligibleActions | Where-Object phase -eq 'Validate')
    $CampaignState.completion.synchronizationComplete =
        $syncActions.Count -gt 0 -and @($syncActions | Where-Object status -ne 'Succeeded').Count -eq 0
    $CampaignState.completion.postMergeComplete =
        $postActions.Count -eq 0 -or @($postActions | Where-Object status -ne 'Succeeded').Count -eq 0
    $CampaignState.completion.validationComplete =
        $validationActions.Count -gt 0 -and @($validationActions | Where-Object status -ne 'Succeeded').Count -eq 0
}

function Assert-PARPostMergeStateContract {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][hashtable] $CampaignState
    )

    Assert-PARCondition ($Campaign.ContainsKey('postMergeActions') -and
        $Campaign.postMergeActions -is [object[]]) 'Geprueftes Manifest enthaelt keinen Post-Merge-Vertrag.'
    Assert-PARCondition ($Profiles.ContainsKey('postMergeProfiles') -and
        $Profiles.postMergeProfiles -is [hashtable]) 'RunnerConfig enthaelt keine Post-Merge-Profile.'
    $manifestActions = @($Campaign.postMergeActions)
    $stateActions = @($CampaignState.postMergeActions)
    Assert-PARCondition ($stateActions.Count -eq $manifestActions.Count) `
        'State-Post-Merge-Aktionen stimmen nicht mit dem geprueften Manifest ueberein.'
    $stateActionIds = @($stateActions | ForEach-Object { [string] $_.actionId })
    Assert-PARCondition (@($stateActionIds | Sort-Object -Unique).Count -eq $stateActionIds.Count) `
        'State-Post-Merge-Aktions-IDs muessen eindeutig sein.'

    foreach ($actionState in $stateActions) {
        $actionId = [string] $actionState.actionId
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($actionId)) `
            'State-Post-Merge-Aktion enthaelt keine actionId.'
        $matchingActions = @($manifestActions | Where-Object actionId -eq $actionId)
        Assert-PARCondition ($matchingActions.Count -eq 1) `
            "State-Post-Merge-Aktion '$actionId' ist nicht im geprueften Manifest deklariert."
        $action = $matchingActions[0]
        foreach ($property in @('workerId', 'phase', 'profile')) {
            Assert-PARCondition ([string] $actionState[$property] -eq [string] $action[$property]) `
                "State-Post-Merge-Aktion '$actionId' weicht bei '$property' vom geprueften Manifest ab."
        }
        $profileName = [string] $action.profile
        Assert-PARCondition ($Profiles.postMergeProfiles.ContainsKey($profileName) -and
            $Profiles.postMergeProfiles[$profileName] -is [hashtable]) `
            "Post-Merge-Profil '$profileName' fehlt in der geprueften RunnerConfig."
        Assert-PARCondition ([bool] $Profiles.postMergeProfiles[$profileName].idempotent) `
            "Post-Merge-Profil '$profileName' muss idempotent sein."
    }
}

function Get-PARActionReceiptPath {
    param([Parameter(Mandatory)][string] $RuntimePath,
        [Parameter(Mandatory)][string] $ActionId)

    $bytes = [Text.UTF8Encoding]::new($false).GetBytes($ActionId)
    $digest = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($bytes)).ToLowerInvariant()
    return Join-Path $RuntimePath "post-merge-receipts/$digest.json"
}

function Assert-PARActionReceipt {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Action,
        [Parameter(Mandatory)][hashtable] $ActionState,
        [Parameter(Mandatory)][hashtable] $WorkerState,
        [Parameter(Mandatory)][string] $Repository,
        [Parameter(Mandatory)][string] $RuntimePath
    )

    $path = Get-PARActionReceiptPath $RuntimePath ([string] $Action.actionId)
    Assert-PARCondition (Test-Path -LiteralPath $path -PathType Leaf) `
        "Post-Merge-Aktion '$($Action.actionId)' hat keinen Abschlussbeleg."
    $root = Resolve-PARPhysicalPath $RuntimePath
    $physicalPath = Resolve-PARPhysicalPath $path
    $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
    Assert-PARCondition $physicalPath.StartsWith(
        $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar,
        $comparison) "Post-Merge-Abschlussbeleg verlaesst RuntimeRoot: $($Action.actionId)"
    Assert-PARCondition ((Test-Path -LiteralPath $path -PathType Leaf) -and
        [string] $ActionState.receiptSha256 -match '^[0-9a-f]{64}$' -and
        (Get-PARSha256 $path) -ceq [string] $ActionState.receiptSha256) `
        "Post-Merge-Aktion '$($Action.actionId)' hat keinen unveraenderten Abschlussbeleg."
    $receipt = Read-PARJson $path
    Assert-PARCondition ([string] $receipt.campaignId -ceq [string] $Campaign.campaignId -and
        [string] $receipt.actionId -ceq [string] $Action.actionId -and
        [string] $receipt.workerId -ceq [string] $Action.workerId -and
        [string] $receipt.profile -ceq [string] $Action.profile -and
        [string] $receipt.repository -ceq (Resolve-PARPhysicalPath $Repository) -and
        [string] $receipt.mergeCommitSha -ceq [string] $WorkerState.mergeCommitSha -and
        [int] $receipt.attemptCount -eq [int] $ActionState.attemptCount -and
        [int] $receipt.exitCode -eq 0 -and [int] $ActionState.exitCode -eq 0) `
        "Post-Merge-Aktion '$($Action.actionId)' hat keinen passenden Erfolgbeleg."
}

function Invoke-PARPostMergeActions {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath,
        [Parameter(Mandatory)][string] $StatePath
    )

    Assert-PARCondition (@($CampaignState.postMergeActions).Count -gt 0) `
        'Post-Merge-Vertrag fehlt; Completed darf ohne Synchronisation und Abschlussvalidierung nicht gesetzt werden.'
    Assert-PARPostMergeStateContract $Campaign $Profiles $CampaignState
    $CampaignState.attempts.postMerge = [int] $CampaignState.attempts.postMerge + 1
    foreach ($phase in @('Synchronize', 'PostMerge', 'Validate')) {
        foreach ($actionState in @($CampaignState.postMergeActions | Where-Object phase -eq $phase)) {
            if ($Campaign.topology -eq 'AlternativeSolutions' -and
                [string] $actionState.workerId -ne [string] $CampaignState.selectedWorkerId) {
                $actionState.status = 'Skipped'
                $actionState.summary = 'Unselected alternative was not merged.'
                continue
            }
            if ($actionState.status -eq 'Succeeded') {
                $action = @($Campaign.postMergeActions | Where-Object actionId -eq $actionState.actionId)[0]
                $worker = Get-PARCampaignWorker $Campaign ([string] $action.workerId)
                $workerState = Get-PARWorkerState $CampaignState ([string] $action.workerId)
                $repository = Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory
                Assert-PARActionReceipt $Campaign $action $actionState $workerState $repository $RuntimePath
                continue
            }
            if (Test-PARConsolidationPause $CampaignState $StatePath) {
                return $false
            }
            $action = @($Campaign.postMergeActions | Where-Object actionId -eq $actionState.actionId)[0]
            $worker = Get-PARCampaignWorker $Campaign ([string] $action.workerId)
            $workerState = Get-PARWorkerState $CampaignState ([string] $action.workerId)
            Assert-PARCondition ([string] $workerState.status -eq 'Merged') `
                "Post-Merge-Aktion '$($action.actionId)' erfordert einen gemergten Worker."
            $postMergeProfile = $Profiles.postMergeProfiles[[string] $action.profile]
            $repository = Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory
            $values = @{
                campaignId = [string] $Campaign.campaignId
                workerId = [string] $worker.workerId
                repository = $repository
                branch = [string] $worker.branch
                headSha = [string] $workerState.headSha
                mergeCommitSha = [string] $workerState.mergeCommitSha
                prUrl = [string] $workerState.prUrl
                runtimeRoot = $RuntimePath
                actionId = [string] $action.actionId
            }
            $actionState.status = 'Running'
            $actionState.attemptCount = [int] $actionState.attemptCount + 1
            $actionState.startedAt = [DateTime]::UtcNow.ToString('o')
            $CampaignState.phase = $phase
            $CampaignState.status = switch ($phase) {
                'Synchronize' { 'Synchronizing' }
                'PostMerge' { 'RunningPostMergeActions' }
                'Validate' { 'FinalValidation' }
            }
            Add-PARStateEvent $CampaignState 'PostMergeActionStarted' `
                "Action '$($action.actionId)' started in phase '$phase'." -WorkerId ([string] $worker.workerId) `
                -Attempt ([int] $actionState.attemptCount)
            Set-PARStateTimestamp $CampaignState
            Write-PARJsonAtomic $StatePath $CampaignState

            $commandResult = Invoke-PARProfileCommand $postMergeProfile $values $repository
            $actionState.exitCode = [int] $commandResult.ExitCode
            if ($commandResult.ExitCode -ne 0) {
                $actionState.status = 'Failed'
                $actionState.summary = "Action failed with exit code $($commandResult.ExitCode)."
                $CampaignState.status = switch ($phase) {
                    'Synchronize' { 'SynchronizationFailed' }
                    'PostMerge' { 'PostMergeFailed' }
                    'Validate' { 'ValidationFailed' }
                }
                Add-PARStateEvent $CampaignState 'PostMergeActionFailed' $actionState.summary `
                    -WorkerId ([string] $worker.workerId) -Attempt ([int] $actionState.attemptCount)
                Set-PARCompletionFlags $CampaignState
                Set-PARStateTimestamp $CampaignState
                Write-PARJsonAtomic $StatePath $CampaignState
                throw "Post-Merge-Aktion '$($action.actionId)' ist fehlgeschlagen."
            }

            $actionState.completedAt = [DateTime]::UtcNow.ToString('o')
            $receiptPath = Get-PARActionReceiptPath $RuntimePath ([string] $action.actionId)
            [void](New-Item -ItemType Directory -Path (Split-Path -Parent $receiptPath) -Force)
            $root = Resolve-PARPhysicalPath $RuntimePath
            $receiptDirectory = Resolve-PARPhysicalPath (Split-Path -Parent $receiptPath)
            $comparison = if ($IsWindows) { [StringComparison]::OrdinalIgnoreCase } else { [StringComparison]::Ordinal }
            Assert-PARCondition $receiptDirectory.StartsWith(
                $root.TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar,
                $comparison) 'Post-Merge-Belegordner verlaesst RuntimeRoot.'
            if (Test-Path -LiteralPath $receiptPath) {
                $receiptEntry = Get-Item -LiteralPath $receiptPath -Force
                Assert-PARCondition (($receiptEntry.Attributes -band [IO.FileAttributes]::ReparsePoint) -eq 0) `
                    'Post-Merge-Belegdatei ist umgeleitet.'
            }
            Write-PARJsonAtomic $receiptPath ([ordered]@{
                schemaVersion = '1.0'
                campaignId = [string] $Campaign.campaignId
                actionId = [string] $action.actionId
                workerId = [string] $action.workerId
                profile = [string] $action.profile
                repository = Resolve-PARPhysicalPath $repository
                mergeCommitSha = [string] $workerState.mergeCommitSha
                attemptCount = [int] $actionState.attemptCount
                exitCode = 0
                completedAt = [string] $actionState.completedAt
            })
            $actionState.receiptSha256 = Get-PARSha256 $receiptPath
            $actionState.status = 'Succeeded'
            $actionState.summary = 'Action completed.'
            Add-PARStateEvent $CampaignState 'PostMergeActionSucceeded' $actionState.summary `
                -WorkerId ([string] $worker.workerId) -Attempt ([int] $actionState.attemptCount)
            Set-PARCompletionFlags $CampaignState
            Set-PARStateTimestamp $CampaignState
            Write-PARJsonAtomic $StatePath $CampaignState
        }
    }
    return $true
}

function Invoke-PARConsolidateCore {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestPath,
        [Parameter(Mandatory)][string] $StatePath,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath,
        [string] $Selection = '',
        [switch] $DoMerge,
        [switch] $IsResume
    )

    $campaignState = Read-PARJson $StatePath
    Assert-PARCondition ($campaignState.campaignId -eq $Campaign.campaignId) `
        'State campaignId stimmt nicht mit Manifest ueberein.'
    Assert-PARCondition ($campaignState.manifestSha256 -eq (Get-PARSha256 $ManifestPath)) `
        'Manifest hat sich seit dem State-Checkpoint geaendert.'
    Initialize-PARStateShape $campaignState $Campaign $Profiles
    if ($DoMerge) {
        Assert-PARPostMergeStateContract $Campaign $Profiles $campaignState
    }
    $eligibleIds = @($Campaign.consolidation.mergeOrder)
    if ($Campaign.topology -eq 'AlternativeSolutions') {
        if (-not [string]::IsNullOrWhiteSpace([string] $campaignState.selectedWorkerId) -and
            -not [string]::IsNullOrWhiteSpace($Selection) -and
            $Selection -ne [string] $campaignState.selectedWorkerId) {
            throw 'SelectedWorker darf nach der Auswahl nicht gewechselt werden.'
        }
        if ([string]::IsNullOrWhiteSpace($Selection) -and
            -not [string]::IsNullOrWhiteSpace([string] $campaignState.selectedWorkerId)) {
            $Selection = [string] $campaignState.selectedWorkerId
        }
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($Selection)) 'AlternativeSolutions erfordert -SelectedWorker.'
        Assert-PARCondition ($eligibleIds -contains $Selection) "SelectedWorker '$Selection' ist unbekannt."
        $eligibleIds = @($Selection)
        $campaignState.selectedWorkerId = $Selection
        if ($DoMerge) {
            foreach ($requiredPhase in @('Synchronize', 'Validate')) {
                Assert-PARCondition (@($Campaign.postMergeActions | Where-Object {
                    $_.workerId -eq $Selection -and $_.phase -eq $requiredPhase
                }).Count -gt 0) `
                    "SelectedWorker '$Selection' benoetigt eine $requiredPhase-Aktion."
            }
        }
    }

    $eligibleStatuses = if ($DoMerge) {
        @('Completed', 'ReadyForMerge', 'Merged', 'NeedsRevalidation')
    } else {
        @('Completed', 'ReadyForMerge', 'Merged')
    }
    foreach ($id in $eligibleIds) {
        $workerState = Get-PARWorkerState $campaignState $id
        Assert-PARCondition ($workerState.status -in $eligibleStatuses) `
            "Worker '$id' ist nicht konsolidierungsbereit."
        $worker = Get-PARCampaignWorker $Campaign $id
        Confirm-PARWorkerExecutionEvidence $Campaign $worker $workerState $ManifestDirectory $RuntimePath
    }

    if (-not $DoMerge) {
        if ($Campaign.deliveryMode -eq 'PublishPR') {
            foreach ($id in $eligibleIds) {
                $worker = Get-PARCampaignWorker $Campaign $id
                $workerState = Get-PARWorkerState $campaignState $id
                try {
                    Confirm-PARPublicationWorkerState $Campaign $Profiles $worker $workerState `
                        $ManifestDirectory $RuntimePath
                } catch {
                    $workerState.status = 'NeedsRevalidation'
                    $workerState.summary = $_.Exception.Message
                    $campaignState.status = 'NeedsRevalidation'
                    Set-PARStateTimestamp $campaignState
                    Write-PARJsonAtomic $StatePath $campaignState
                    throw
                }
            }
        }
        $campaignState.status = 'Consolidated'
        $campaignState.phase = 'Completed'
        Add-PARStateEvent $campaignState 'ConsolidationRecorded' 'Consolidation recorded without remote merge.'
        Set-PARStateTimestamp $campaignState
        Write-PARJsonAtomic $StatePath $campaignState
        return
    }

    Assert-PARCondition ($Campaign.deliveryMode -eq 'MergeAndSync') '-Merge erfordert deliveryMode MergeAndSync.'
    Assert-PARCondition $Campaign.consolidation.ContainsKey('mergeProfile') 'consolidation.mergeProfile fehlt.'
    $mergeProfileName = [string] $Campaign.consolidation.mergeProfile
    Assert-PARCondition $Profiles.mergeProfiles.ContainsKey($mergeProfileName) "Merge-Profil '$mergeProfileName' fehlt."
    $mergeProfile = $Profiles.mergeProfiles[$mergeProfileName]
    Assert-PARCondition ($mergeProfile.ContainsKey('preflight') -and $mergeProfile.preflight -is [hashtable]) `
        'MergeAndSync erfordert ab v0.2.0 einen providergebundenen Preflight im lokalen Merge-Profil.'
    Assert-PARCondition ($mergeProfile.ContainsKey('merge') -and $mergeProfile.merge -is [hashtable]) `
        'MergeAndSync erfordert ab v0.2.0 ein separates merge-Profil.'
    $campaignState.attempts.consolidation = [int] $campaignState.attempts.consolidation + 1
    if ($IsResume) {
        Remove-Item -LiteralPath "$([IO.Path]::GetFullPath($StatePath)).stop-requested" `
            -Force -ErrorAction SilentlyContinue
        $campaignState.stopRequested = $false
    }
    $campaignState.status = 'Merging'
    $campaignState.phase = 'MergeAndSync'
    Add-PARStateEvent $campaignState 'ConsolidationStarted' 'Provider-gated consolidation started.' `
        -Attempt ([int] $campaignState.attempts.consolidation)
    Set-PARStateTimestamp $campaignState
    Write-PARJsonAtomic $StatePath $campaignState

    foreach ($id in $eligibleIds) {
        if (Test-PARConsolidationPause $campaignState $StatePath) {
            return
        }
        [void](Confirm-PARWorkerProviderState $Campaign $mergeProfile $campaignState $id $ManifestDirectory $RuntimePath $StatePath)
    }
    if (Test-PARConsolidationPause $campaignState $StatePath) {
        return
    }

    for ($index = 0; $index -lt $eligibleIds.Count; $index++) {
        $id = [string] $eligibleIds[$index]
        $workerState = Get-PARWorkerState $campaignState $id
        if ($workerState.status -eq 'Merged') {
            continue
        }
        foreach ($remainingId in @($eligibleIds[$index..($eligibleIds.Count - 1)])) {
            if (Test-PARConsolidationPause $campaignState $StatePath) {
                return
            }
            $remainingState = Get-PARWorkerState $campaignState ([string] $remainingId)
            if ($remainingState.status -ne 'Merged') {
                [void](Confirm-PARWorkerProviderState $Campaign $mergeProfile $campaignState ([string] $remainingId) $ManifestDirectory $RuntimePath $StatePath)
            }
        }
        if (Test-PARConsolidationPause $campaignState $StatePath) {
            return
        }
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $workerState.prUrl)) "Worker '$id' hat keine prUrl."
        $worker = Get-PARCampaignWorker $Campaign $id
        $repository = Resolve-PARRepository ([string] $worker.repository) $ManifestDirectory
        $values = @{
            campaignId = [string] $Campaign.campaignId
            workerId = $id
            prUrl = [string] $workerState.prUrl
            prNumber = [string] $workerState.prNumber
            prBaseRepository = [string] $workerState.prBaseRepository
            headSha = [string] $workerState.headSha
            repository = $repository
            branch = [string] $worker.branch
        }
        $workerState.mergeAttempt = [int] $workerState.mergeAttempt + 1
        Add-PARStateEvent $campaignState 'MergeStarted' 'Merge command started.' -WorkerId $id `
            -Attempt ([int] $workerState.mergeAttempt)
        Set-PARStateTimestamp $campaignState
        Write-PARJsonAtomic $StatePath $campaignState
        $mergeResult = Invoke-PARProfileCommand $mergeProfile.merge $values $repository
        if ($mergeResult.ExitCode -ne 0) {
            $campaignState.status = 'MergeFailed'
            $workerState.summary = "Merge failed with exit code $($mergeResult.ExitCode)."
            Add-PARStateEvent $campaignState 'MergeFailed' $workerState.summary -WorkerId $id `
                -Attempt ([int] $workerState.mergeAttempt)
            Set-PARStateTimestamp $campaignState
            Write-PARJsonAtomic $StatePath $campaignState
            throw "Merge fehlgeschlagen; Campaign nach Worker '$id' angehalten."
        }
        $postMergePreflight = Confirm-PARWorkerProviderState $Campaign $mergeProfile $campaignState $id $ManifestDirectory $RuntimePath $StatePath
        Assert-PARCondition ([string] $postMergePreflight.state -eq 'Merged') `
            "Provider bestaetigte den Merge fuer Worker '$id' nicht."
        $workerState.status = 'Merged'
        $workerState.mergeCommitSha = [string] $postMergePreflight.mergeCommitSha
        $workerState.summary = 'Merged in declared order and verified by provider.'
        Add-PARStateEvent $campaignState 'MergeVerified' $workerState.summary -WorkerId $id `
            -Attempt ([int] $workerState.mergeAttempt)
        Set-PARStateTimestamp $campaignState
        Write-PARJsonAtomic $StatePath $campaignState
        if (Test-PARConsolidationPause $campaignState $StatePath) {
            return
        }
    }

    Set-PARCompletionFlags $campaignState
    if (@($campaignState.postMergeActions).Count -eq 0) {
        $campaignState.status = 'AwaitingPostMergeActions'
        $campaignState.phase = 'PostMerge'
        Add-PARStateEvent $campaignState 'PostMergeContractRequired' `
            'Merges are complete, but synchronization and final validation are not declared.'
        Set-PARStateTimestamp $campaignState
        Write-PARJsonAtomic $StatePath $campaignState
        return
    }

    if (-not (Invoke-PARPostMergeActions $Campaign $Profiles $campaignState $ManifestDirectory $RuntimePath $StatePath)) {
        return
    }
    Set-PARCompletionFlags $campaignState
    Assert-PARCondition ([bool] $campaignState.completion.mergeComplete) 'Merge-Abschlussvalidierung fehlt.'
    Assert-PARCondition ([bool] $campaignState.completion.synchronizationComplete) 'Synchronisationsabschluss fehlt.'
    Assert-PARCondition ([bool] $campaignState.completion.postMergeComplete) 'Post-Merge-Abschluss fehlt.'
    Assert-PARCondition ([bool] $campaignState.completion.validationComplete) 'Abschlussvalidierung fehlt.'
    $campaignState.status = 'Completed'
    $campaignState.phase = 'Completed'
    Add-PARStateEvent $campaignState 'CampaignCompleted' `
        'Merge, synchronization, post-merge actions, and final validation completed.'
    Set-PARStateTimestamp $campaignState
    Write-PARJsonAtomic $StatePath $campaignState
}

function Invoke-PARConsolidate {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $Profiles,
        [Parameter(Mandatory)][string] $ManifestPath,
        [Parameter(Mandatory)][string] $StatePath,
        [Parameter(Mandatory)][string] $ManifestDirectory,
        [Parameter(Mandatory)][string] $RuntimePath,
        [string] $Selection = '',
        [switch] $DoMerge,
        [switch] $IsResume
    )

    $campaignLock = Enter-PARCampaignLock $RuntimePath

    try {
        Invoke-PARConsolidateCore $Campaign $Profiles $ManifestPath $StatePath `
            $ManifestDirectory $RuntimePath $Selection -DoMerge:$DoMerge -IsResume:$IsResume
    } finally {
        Exit-PARCampaignLock $campaignLock
    }
}

function Write-PARStatus {
    param(
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][hashtable] $CampaignState,
        [Parameter(Mandatory)][ValidateSet('Json', 'Text')][string] $Format
    )

    if ($Format -eq 'Json') {
        $CampaignState | ConvertTo-Json -Depth 30
        return
    }

    Write-Output "Kampagne / Campaign: $($CampaignState.campaignId)"
    Write-Output "Status / Status: $($CampaignState.status)"
    Write-Output "Phase / Phase: $($CampaignState.phase)"
    Write-Output "Parallelitaet konfiguriert / Configured concurrency: $($Campaign.maxConcurrency)"
    $observed = if ($CampaignState.ContainsKey('maximumObservedConcurrency')) {
        [int] $CampaignState.maximumObservedConcurrency
    } else {
        0
    }
    Write-Output "Parallelitaet beobachtet / Observed concurrency: $observed"
    Write-Output "Stop angefordert / Stop requested: $([bool] $CampaignState.stopRequested)"
    if ($CampaignState.ContainsKey('attempts')) {
        Write-Output "Versuche / Attempts: execute=$($CampaignState.attempts.execute), consolidation=$($CampaignState.attempts.consolidation), postMerge=$($CampaignState.attempts.postMerge)"
    }
    Write-Output 'Worker / Workers:'
    foreach ($workerState in @($CampaignState.workers)) {
        $runnerProfileName = if ($workerState.ContainsKey('runnerProfile')) {
            [string] $workerState.runnerProfile
        } else {
            $script:UndeclaredRunnerMetadata
        }
        $agent = if ($workerState.ContainsKey('agentFamily')) {
            [string] $workerState.agentFamily
        } else {
            $script:UndeclaredRunnerMetadata
        }
        $model = if ($workerState.ContainsKey('model')) {
            [string] $workerState.model
        } else {
            $script:UndeclaredRunnerMetadata
        }
        $reasoning = if ($workerState.ContainsKey('reasoningEffort')) {
            [string] $workerState.reasoningEffort
        } else {
            $script:UndeclaredRunnerMetadata
        }
        Write-Output "- $($workerState.workerId): $($workerState.status)"
        Write-Output "  Runner-Profil / Runner profile: $runnerProfileName"
        Write-Output "  Agentenfamilie / Agent family: $agent"
        $routingRole = if ($workerState.ContainsKey('routingRole')) {
            [string] $workerState.routingRole
        } else {
            $script:UndeclaredRunnerMetadata
        }
        $modelPreflight = if ($workerState.ContainsKey('modelPreflight')) {
            [string] $workerState.modelPreflight
        } else {
            $script:UndeclaredRunnerMetadata
        }
        Write-Output "  Routing-Rolle / Routing role: $routingRole"
        Write-Output "  Modell / Model: $model"
        Write-Output "  Reasoning / Effort: $reasoning"
        Write-Output "  Modell-Preflight / Model preflight: $modelPreflight"
    }
    if ($CampaignState.ContainsKey('postMergeActions') -and
        @($CampaignState.postMergeActions).Count -gt 0) {
        Write-Output 'Post-Merge-Aktionen / Post-merge actions:'
        foreach ($actionState in @($CampaignState.postMergeActions)) {
            Write-Output "- $($actionState.actionId): $($actionState.phase), $($actionState.status), attempts=$($actionState.attemptCount)"
        }
    }
}

$manifestPath = [IO.Path]::GetFullPath($Manifest)
$manifestDirectory = Split-Path -Parent $manifestPath
$campaign = Read-PARJson $manifestPath
Assert-PARSchemaVersion $campaign 'Campaign'
$script:CurrentIntakeReview = Test-PARIntakeReview $campaign $manifestDirectory

if (-not $RuntimeRoot) {
    $RuntimeRoot = Join-Path ([Environment]::GetFolderPath('UserProfile')) ".specify/parallel-runs/$($campaign.campaignId)"
}
$runtimePath = [IO.Path]::GetFullPath($RuntimeRoot)
if (-not $State) {
    $State = Join-Path $runtimePath 'parallel-campaign-state.json'
}
$statePath = [IO.Path]::GetFullPath($State)
if ($Action -ne 'Validate') {
    Assert-PARCheckpointBoundary $campaign $manifestDirectory $statePath $runtimePath $Action
}

if ($Action -eq 'Status') {
    $statusData = Read-PARJson $statePath
    Assert-PARCondition ($statusData.campaignId -eq $campaign.campaignId) 'State campaignId stimmt nicht.'
    Write-PARStatus $campaign $statusData $OutputFormat
    exit 0
}

if ($Action -eq 'Stop') {
    $statusData = Read-PARJson $statePath
    Assert-PARCondition ($statusData.campaignId -eq $campaign.campaignId) 'State campaignId stimmt nicht.'
    [IO.File]::WriteAllText(
        "$statePath.stop-requested",
        [DateTime]::UtcNow.ToString('o'),
        [Text.UTF8Encoding]::new($false)
    )
    $statusData.stopRequested = $true
    Add-PARStateEvent $statusData 'StopRequested' 'Cooperative stop requested; no process was killed.'
    Set-PARStateTimestamp $statusData
    Write-PARJsonAtomic $statePath $statusData
    Write-Output 'PASS: cooperative stop requested; no process was killed.'
    exit 0
}

Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($RunnerConfig)) "-RunnerConfig ist fuer Action '$Action' erforderlich."
$runnerConfigPath = [IO.Path]::GetFullPath($RunnerConfig)
$profiles = Read-PARJson $runnerConfigPath
Assert-PARSchemaVersion $profiles 'RunnerConfig'

# Historical field profile names carried an expired native-agent/admin exception.
# Reject their saved artifacts at the shared entry point, not only in the wrapper.
if ($Action -ne 'Validate') {
    $retiredRunner = 'native-codex-field'
    $retiredMerge = 'github-owner-development'
    function Assert-PARRetiredProfileCommand {
        param($Value)
        if ($Value -is [string]) {
            Assert-PARCondition ($Value -cnotmatch '(?<![a-z0-9-])(danger-full-access|--dangerously-bypass-approvals-and-sandbox|--admin)(?![a-z0-9-])') `
                'Historischer nativer Sandbox-Bypass oder Admin-Merge-Befehl ist gesperrt.'
        } elseif ($Value -is [System.Collections.IDictionary]) {
            foreach ($item in $Value.Values) { Assert-PARRetiredProfileCommand $item }
        } elseif ($Value -is [System.Collections.IEnumerable]) {
            foreach ($item in $Value) { Assert-PARRetiredProfileCommand $item }
        }
    }
    Assert-PARRetiredProfileCommand $profiles
    Assert-PARCondition ([string] $campaign.runnerProfile -cne $retiredRunner) 'Historisches natives Runner-Profil ist gesperrt.'
    foreach ($worker in @($campaign.workers)) {
        if ($worker.ContainsKey('runnerProfile')) {
            Assert-PARCondition ([string] $worker.runnerProfile -cne $retiredRunner) 'Historisches natives Runner-Profil ist gesperrt.'
        }
    }
    if ($campaign.ContainsKey('consolidation') -and $campaign.consolidation -is [hashtable] -and
        $campaign.consolidation.ContainsKey('mergeProfile')) {
        Assert-PARCondition ([string] $campaign.consolidation.mergeProfile -cne $retiredMerge) 'Historisches Admin-Merge-Profil ist gesperrt.'
    }
}

$skipClean = $Action -in @('Resume', 'Consolidate')
Test-PARCampaign $campaign $profiles $manifestDirectory -SkipCleanCheck:$skipClean

switch ($Action) {
    'Validate' {
        Write-Output "PASS: campaign $($campaign.campaignId), $(@($campaign.workers).Count) worker, max concurrency $($campaign.maxConcurrency)."
    }
    'Start' {
        Invoke-PARStart $campaign $profiles $manifestPath $manifestDirectory $statePath $runtimePath
        Write-Output "PASS: campaign state written to $statePath"
    }
    'Resume' {
        $resumeState = Read-PARJson $statePath
        $consolidationStatuses = @(
            'ReadyForConsolidation', 'Merging', 'MergeFailed', 'NeedsRevalidation',
            'AwaitingPostMergeActions', 'Synchronizing', 'SynchronizationFailed',
            'RunningPostMergeActions', 'PostMergeFailed', 'FinalValidation',
            'ValidationFailed'
        )
        $resumeConsolidation = $campaign.deliveryMode -eq 'MergeAndSync' -and (
            $consolidationStatuses -contains [string] $resumeState.status -or
            ([string] $resumeState.status -eq 'PausedByUser' -and
                [string] $resumeState.phase -eq 'ConsolidationPaused')
        )
        if ($resumeConsolidation) {
            Assert-PARCondition $Merge 'Resume einer MergeAndSync-Konsolidierung erfordert ein ausdrueckliches -Merge.'
            Invoke-PARConsolidate $campaign $profiles $manifestPath $statePath $manifestDirectory $runtimePath `
                $SelectedWorker -DoMerge -IsResume
        } else {
            Invoke-PARStart $campaign $profiles $manifestPath $manifestDirectory $statePath $runtimePath -IsResume
        }
        Write-Output "PASS: campaign resumed; state written to $statePath"
    }
    'Consolidate' {
        Invoke-PARConsolidate $campaign $profiles $manifestPath $statePath $manifestDirectory $runtimePath `
            $SelectedWorker -DoMerge:$Merge
        Write-Output "PASS: campaign consolidation recorded in $statePath"
    }
}
