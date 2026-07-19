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
    Optionaler Pfad zur Campaign-State-Datei.

.PARAMETER RuntimeRoot
    Nicht versioniertes Verzeichnis fuer Worktrees, Logs, Locks und Ergebnisse.

.PARAMETER SelectedWorker
    Menschlich ausgewaehlter Worker fuer AlternativeSolutions.

.PARAMETER Merge
    Fuehrt bei Consolidate die konfigurierte Merge-Command aus.

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
$script:SupportedSchemaVersions = @('1.0', '1.1')
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
        "$ContractName schemaVersion muss 1.0 oder 1.1 sein."
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
    Push-Location -LiteralPath $WorkingDirectory
    try {
        $output = @(& ([string] $CommandProfile.executable) @arguments 2>&1)
        $exitCode = $LASTEXITCODE
    } finally {
        Pop-Location
    }
    return @{
        ExitCode = $exitCode
        Output = @($output)
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

    $registryPath = Join-Path $Repository '.specify/presets/.registry'
    if (-not (Test-Path -LiteralPath $registryPath -PathType Leaf)) {
        return $false
    }
    $registry = Read-PARJson $registryPath
    if (-not $registry.presets.ContainsKey('autonomous-run-governance')) {
        return $false
    }
    $entry = $registry.presets['autonomous-run-governance']
    if (-not [bool] $entry.enabled) {
        return $false
    }
    return [version] $entry.version -ge [version] '0.2.2'
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
    Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $Campaign.runnerProfile)) 'Campaign runnerProfile fehlt.'
    Assert-PARCondition ($Profiles.ContainsKey('profiles') -and $Profiles.profiles -is [hashtable]) `
        'RunnerConfig profiles fehlt.'
    $campaignRunnerProfile = [string] $Campaign.runnerProfile
    Assert-PARCondition $Profiles.profiles.ContainsKey($campaignRunnerProfile) `
        "Campaign-Runner-Fallback '$campaignRunnerProfile' fehlt."
    if ($Campaign.ContainsKey('operatorInstructions')) {
        Assert-PARCondition ($Campaign.operatorInstructions -is [string]) 'operatorInstructions muss eine Zeichenkette sein.'
        Assert-PARCondition (([string] $Campaign.operatorInstructions).Length -le 8000) 'operatorInstructions ist laenger als 8000 Zeichen.'
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
        $runnerProfileName = Get-PARRunnerProfileName $Campaign $worker
        Assert-PARCondition $Profiles.profiles.ContainsKey($runnerProfileName) "Runner-Profil '$runnerProfileName' fuer Worker '$($worker.workerId)' fehlt."
        $runnerProfileData = $Profiles.profiles[$runnerProfileName]
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.executable)) "Runner executable fehlt: $runnerProfileName"
        Assert-PARCondition ($runnerProfileData.arguments -is [object[]]) "Runner arguments muss ein Array sein: $runnerProfileName"
        if ([string] $Campaign.schemaVersion -eq '1.1') {
            Assert-PARCondition ($runnerProfileData.ContainsKey('agentFamily') -and
                -not [string]::IsNullOrWhiteSpace([string] $runnerProfileData.agentFamily)) `
                "Runner-Profil '$runnerProfileName' benoetigt agentFamily."
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
        if ([string] $Campaign.schemaVersion -eq '1.1') {
            Assert-PARCondition $Campaign.consolidation.ContainsKey('mergeProfile') 'consolidation.mergeProfile fehlt.'
        }
        if ($Campaign.consolidation.ContainsKey('mergeProfile')) {
            $mergeProfileName = [string] $Campaign.consolidation.mergeProfile
            Assert-PARCondition ($Profiles.ContainsKey('mergeProfiles') -and
                $Profiles.mergeProfiles.ContainsKey($mergeProfileName)) "Merge-Profil '$mergeProfileName' fehlt."
        }
        if ([string] $Campaign.schemaVersion -eq '1.1') {
            $mergeProfile = $Profiles.mergeProfiles[$mergeProfileName]
            Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $mergeProfile.provider)) `
                "Merge-Profil '$mergeProfileName' benoetigt provider."
            Assert-PARCondition ($mergeProfile.ContainsKey('preflight') -and
                $mergeProfile.preflight -is [hashtable]) "Merge-Profil '$mergeProfileName' benoetigt preflight."
            Assert-PARCondition ($mergeProfile.ContainsKey('merge') -and
                $mergeProfile.merge -is [hashtable]) "Merge-Profil '$mergeProfileName' benoetigt merge."
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
            prUrl = $null
            mergeCommitSha = $null
            providerState = 'NotChecked'
            baseRefName = $null
            handoffs = @()
            runnerProfile = $runnerMetadata.runnerProfile
            agentFamily = $runnerMetadata.agentFamily
            model = $runnerMetadata.model
            reasoningEffort = $runnerMetadata.reasoningEffort
            executionAttempt = 0
            mergeAttempt = 0
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

    $CampaignState.schemaVersion = '1.1'
    if (-not $CampaignState.ContainsKey('sourceSchemaVersion')) {
        $CampaignState.sourceSchemaVersion = '1.0'
    }
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
        foreach ($entry in @{
            runnerProfile = $metadata.runnerProfile
            agentFamily = $metadata.agentFamily
            model = $metadata.model
            reasoningEffort = $metadata.reasoningEffort
            executionAttempt = 0
            mergeAttempt = 0
            mergeCommitSha = $null
            providerState = 'NotChecked'
            baseRefName = $null
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
    Assert-PARCondition $process.Start() "Runner konnte nicht gestartet werden: $Executable"
    return @{
        Process = $process
        StdoutTask = $process.StandardOutput.ReadToEndAsync()
        StderrTask = $process.StandardError.ReadToEndAsync()
        StdoutPath = $StdoutPath
        StderrPath = $StderrPath
    }
}

function Complete-PARWorker {
    param(
        [Parameter(Mandatory)][hashtable] $RunningEntry,
        [Parameter(Mandatory)][hashtable] $WorkerState,
        [Parameter(Mandatory)][hashtable] $Worker,
        [Parameter(Mandatory)][hashtable] $Campaign,
        [Parameter(Mandatory)][string] $ResultPath
    )

    $process = $RunningEntry.Process.Process
    $stdout = $RunningEntry.Process.StdoutTask.GetAwaiter().GetResult()
    $stderr = $RunningEntry.Process.StderrTask.GetAwaiter().GetResult()
    $stdout | Set-Content -LiteralPath $RunningEntry.Process.StdoutPath -Encoding utf8NoBOM
    $stderr | Set-Content -LiteralPath $RunningEntry.Process.StderrPath -Encoding utf8NoBOM
    $WorkerState.exitCode = $process.ExitCode
    $WorkerState.processId = $null

    if ($process.ExitCode -ne 0) {
        $WorkerState.status = 'Failed'
        $WorkerState.summary = "Runner exit code $($process.ExitCode)."
        return
    }

    if (-not (Test-Path -LiteralPath $ResultPath -PathType Leaf)) {
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
            $handoffPath = [IO.Path]::GetFullPath((Join-Path $RunningEntry.Worktree ([string] $declaredHandoff.path)))
            $worktreePrefix = [IO.Path]::GetFullPath($RunningEntry.Worktree).TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
            if (-not $handoffPath.StartsWith($worktreePrefix, [StringComparison]::Ordinal) -or
                -not (Test-Path -LiteralPath $handoffPath -PathType Leaf)) {
                $WorkerState.status = 'Failed'
                $WorkerState.summary = "Runner succeeded but did not produce declared handoff '$($declaredHandoff.path)'."
                return
            }
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
    Assert-PARCondition (@('Completed', 'ReadyForMerge', 'Failed') -contains $resultData.status) "Worker result status ist ungueltig: $ResultPath"
    if ($Campaign.deliveryMode -eq 'MergeAndSync') {
        Assert-PARCondition ($resultData.status -eq 'ReadyForMerge') "MergeAndSync worker must stop at ReadyForMerge: $ResultPath"
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace([string] $resultData.prUrl)) "MergeAndSync worker result requires prUrl: $ResultPath"
    }
    $actualHead = (Invoke-PARGit $RunningEntry.Worktree @('rev-parse', 'HEAD')).Output[0]
    Assert-PARCondition ($resultData.headSha -eq $actualHead) "Worker result headSha ist nicht der aktuelle Worktree-Head: $ResultPath"

    $validatedHandoffs = @()
    foreach ($declaredHandoff in @($Worker.handoffs)) {
        $resultHandoffs = @($resultData.handoffs | Where-Object {
            $_.consumerWorkerId -eq $declaredHandoff.consumerWorkerId -and $_.path -eq $declaredHandoff.path
        })
        Assert-PARCondition ($resultHandoffs.Count -eq 1) "Worker '$($Worker.workerId)' lieferte den deklarierten Handoff zu '$($declaredHandoff.consumerWorkerId)' nicht eindeutig."
        $handoffPath = [IO.Path]::GetFullPath((Join-Path $RunningEntry.Worktree ([string] $declaredHandoff.path)))
        $worktreePrefix = [IO.Path]::GetFullPath($RunningEntry.Worktree).TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
        Assert-PARCondition ($handoffPath.StartsWith($worktreePrefix, [StringComparison]::Ordinal)) "Handoff verlaesst den Worker-Worktree: $handoffPath"
        Assert-PARCondition (Test-Path -LiteralPath $handoffPath -PathType Leaf) "Handoff-Datei fehlt: $handoffPath"
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
    $WorkerState.prUrl = $resultData.prUrl
    $WorkerState.handoffs = @($validatedHandoffs)
    $WorkerState.summary = [string] $resultData.summary
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
            if (-not $IsResume) {
                Assert-PARCondition (@('Prepared', 'PausedByUser', 'Interrupted') -contains $campaignState.status) 'Bestehende Campaign ist nicht startbar; Resume oder Status verwenden.'
            }
        } else {
            $campaignState = Get-PARInitialCampaignState $Campaign $Profiles $ManifestPath
        }

        if ($IsResume) {
            Assert-PARCondition (@('PausedByUser', 'Interrupted', 'Failed') -contains $campaignState.status) 'Campaign ist nicht in einem Resume-faehigen Zustand.'
            foreach ($workerState in $campaignState.workers) {
                if (@('Interrupted', 'Running') -contains $workerState.status) {
                    $workerState.status = 'Pending'
                    $workerState.processId = $null
                    $workerState.summary = 'Revalidated for resume.'
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
                $incomingHandoffs = @()
                foreach ($dependency in @($worker.dependsOn)) {
                    $dependencyState = Get-PARWorkerState $campaignState ([string] $dependency)
                    $matchingHandoffs = @($dependencyState.handoffs | Where-Object { $_.consumerWorkerId -eq $worker.workerId })
                    Assert-PARCondition ($matchingHandoffs.Count -eq 1) "Validierter Handoff '$dependency' -> '$($worker.workerId)' fehlt."
                    $handoffSource = [string] $matchingHandoffs[0].sourcePath
                    Assert-PARCondition (Test-Path -LiteralPath $handoffSource -PathType Leaf) "Handoff-Quelle fehlt: $handoffSource"
                    Assert-PARCondition ((Get-PARSha256 $handoffSource) -eq $matchingHandoffs[0].sha256) "Handoff-Quelle wurde nach Validierung veraendert: $handoffSource"
                    $incomingHandoffs += [ordered]@{
                        producerWorkerId = [string] $dependency
                        sourcePath = $handoffSource
                        sha256 = [string] $matchingHandoffs[0].sha256
                    }
                }
                if ([bool] $campaignState.stopRequested -or $running.Count -ge [int] $Campaign.maxConcurrency) {
                    continue
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
                $resultPath = Join-Path $resultsRoot "$($worker.workerId).json"
                $featureInput = [string] $worker.featureInput
                if (-not [IO.Path]::IsPathRooted($featureInput)) {
                    $featureInput = Join-Path $worktree $featureInput
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
Set status ReadyForMerge, headSha to the exact pushed head, prUrl to the created PR URL, autonomousStatePath and autonomousStateSha256 to the final state, and include every declared handoff with its SHA-256.
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
                    worktree = $worktree
                    repository = $repository
                    branch = [string] $worker.branch
                    prompt = $prompt
                    promptFile = $promptPath
                    resultFile = $resultPath
                    resultDirectory = $resultsRoot
                    handoffsJson = $handoffsJson
                }
                $arguments = ConvertTo-PARArgumentList @($runnerProfileData.arguments) $values
                $processInfo = Invoke-PARProcessAsync ([string] $runnerProfileData.executable) $arguments $worktree (Join-Path $logsRoot "$($worker.workerId).out.log") (Join-Path $logsRoot "$($worker.workerId).err.log")
                $workerState.status = 'Running'
                $workerState.worktree = $worktree
                $workerState.processId = $processInfo.Process.Id
                $workerState.executionAttempt = [int] $workerState.executionAttempt + 1
                $workerState.summary = 'Runner active.'
                Add-PARStateEvent $campaignState 'WorkerStarted' "Worker started with runner profile '$runnerProfileName'." `
                    -WorkerId ([string] $worker.workerId) -Attempt ([int] $workerState.executionAttempt)
                $running[[string] $worker.workerId] = @{
                    Worker = $worker
                    Worktree = $worktree
                    ResultPath = $resultPath
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
                    continue
                }
                $workerState = Get-PARWorkerState $campaignState $workerId
                try {
                    Complete-PARWorker $entry $workerState $entry.Worker $Campaign $entry.ResultPath
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
            $nonTerminal = @($campaignState.workers | Where-Object { $_.status -notin $terminal })
            if ($nonTerminal.Count -eq 0 -and $running.Count -eq 0) {
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
        if (@($campaignState.workers | Where-Object { $_.status -eq 'Failed' }).Count -gt 0) {
            $campaignState.status = 'Failed'
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
        [Parameter(Mandatory)][string] $RuntimePath
    )

    Assert-PARCondition ($MergeProfile.ContainsKey('preflight') -and
        $MergeProfile.preflight -is [hashtable]) 'Merge-Profil benoetigt einen providergebundenen Preflight-Vertrag.'
    $preflightRoot = Join-Path $RuntimePath 'provider-preflight'
    [void](New-Item -ItemType Directory -Path $preflightRoot -Force)
    $resultPath = Join-Path $preflightRoot "$($Worker.workerId).json"
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
    $repository = Resolve-PARRepository ([string] $Worker.repository) $ManifestDirectory
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
    foreach ($requiredField in @(
        'prUrl', 'state', 'isDraft', 'headSha', 'mergeable', 'reviewDecision',
        'unresolvedCurrentThreads', 'checkPolicySatisfied', 'technicalFailures',
        'baseRefName', 'mergeCommitSha'
    )) {
        Assert-PARCondition $preflight.ContainsKey($requiredField) `
            "Provider-Preflight '$($Worker.workerId)' enthaelt '$requiredField' nicht."
    }
    Assert-PARCondition ([string] $preflight.prUrl -eq [string] $WorkerState.prUrl) `
        "Provider-Preflight verweist auf eine andere PR fuer Worker '$($Worker.workerId)'."
    Assert-PARCondition ([string] $preflight.headSha -eq [string] $WorkerState.headSha) `
        "PR-Head-Drift bei Worker '$($Worker.workerId)'."
    Assert-PARCondition (@('Open', 'Merged', 'Closed') -contains [string] $preflight.state) `
        "Provider-Preflight meldet einen ungueltigen PR-Zustand fuer Worker '$($Worker.workerId)'."

    if ([string] $preflight.state -eq 'Open') {
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
        $preflight = Invoke-PARProviderPreflight $Campaign $MergeProfile $worker $workerState $ManifestDirectory $RuntimePath
        Sync-PARStopRequest $CampaignState $StatePath
        $workerState.providerState = [string] $preflight.state
        $workerState.baseRefName = [string] $preflight.baseRefName
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
    $syncActions = @($CampaignState.postMergeActions | Where-Object phase -eq 'Synchronize')
    $postActions = @($CampaignState.postMergeActions | Where-Object phase -eq 'PostMerge')
    $validationActions = @($CampaignState.postMergeActions | Where-Object phase -eq 'Validate')
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
            if ($actionState.status -eq 'Succeeded') {
                continue
            }
            if (Test-PARConsolidationPause $CampaignState $StatePath) {
                return $false
            }
            $action = @($Campaign.postMergeActions | Where-Object actionId -eq $actionState.actionId)[0]
            $worker = Get-PARCampaignWorker $Campaign ([string] $action.workerId)
            $workerState = Get-PARWorkerState $CampaignState ([string] $action.workerId)
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

            $actionState.status = 'Succeeded'
            $actionState.completedAt = [DateTime]::UtcNow.ToString('o')
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
        if ([string]::IsNullOrWhiteSpace($Selection) -and
            -not [string]::IsNullOrWhiteSpace([string] $campaignState.selectedWorkerId)) {
            $Selection = [string] $campaignState.selectedWorkerId
        }
        Assert-PARCondition (-not [string]::IsNullOrWhiteSpace($Selection)) 'AlternativeSolutions erfordert -SelectedWorker.'
        Assert-PARCondition ($eligibleIds -contains $Selection) "SelectedWorker '$Selection' ist unbekannt."
        $eligibleIds = @($Selection)
        $campaignState.selectedWorkerId = $Selection
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
    }

    if (-not $DoMerge) {
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
        Write-Output "  Modell / Model: $model"
        Write-Output "  Reasoning / Effort: $reasoning"
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

if (-not $State) {
    $State = Join-Path $manifestDirectory 'parallel-campaign-state.json'
}
$statePath = [IO.Path]::GetFullPath($State)

if (-not $RuntimeRoot) {
    $RuntimeRoot = Join-Path ([Environment]::GetFolderPath('UserProfile')) ".specify/parallel-runs/$($campaign.campaignId)"
}
$runtimePath = [IO.Path]::GetFullPath($RuntimeRoot)

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
