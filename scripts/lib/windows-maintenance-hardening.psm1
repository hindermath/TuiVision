#Requires -Version 7
<#
.SYNOPSIS
Interne Sicherheitsverträge für die Windows-Wartung.

.DESCRIPTION
Stellt begrenzte Prozessausführung, validierte Python-Auflösung, Retry-
Klassifikation, Git-normalisierte Hashes, atomare Resume-Evidence und
kanonische Abschlussstatus bereit. Die Funktionen umgehen keine UAC-,
Repository- oder Sicherheitsgrenze.

Internal contracts for bounded processes, validated Python discovery, retry
classification, Git-normalized hashes, atomic resume evidence, and canonical
terminal states. The functions do not bypass UAC, repository, or security
boundaries.
#>

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-HBMaintenanceMode {
    <#
    .SYNOPSIS
    Leitet genau einen Wartungsmodus ab. / Derives exactly one maintenance mode.
    #>
    [CmdletBinding()]
    param(
        [switch]$CheckOnly,
        [switch]$Preview
    )

    if ($CheckOnly -and $Preview) {
        throw 'CheckOnly und Preview sind gegenseitig ausschliessend / are mutually exclusive.'
    }
    if ($CheckOnly) {
        return [pscustomobject]@{
            Name = 'CheckOnly'
            AllowsMutation = $false
            FleetMode = 'check-only'
            HomeSyncSwitch = 'CheckOnly'
        }
    }
    if ($Preview) {
        return [pscustomobject]@{
            Name = 'Preview'
            AllowsMutation = $false
            FleetMode = 'dry-run'
            HomeSyncSwitch = 'WhatIf'
        }
    }
    return [pscustomobject]@{
        Name = 'Update'
        AllowsMutation = $true
        FleetMode = 'update'
        HomeSyncSwitch = 'N/A'
    }
}

function Invoke-HBBoundedProcess {
    <#
    .SYNOPSIS
    Führt einen Prozess mit harter Grenze und Prozessbaum-Bereinigung aus.

    .DESCRIPTION
    Runs one process with a hard timeout. On timeout, the complete child tree is
    terminated and awaited for the bounded cleanup interval.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$FilePath,
        [string[]]$Arguments = @(),
        [ValidateRange(1, 86400000)][int]$TimeoutMilliseconds = 300000,
        [ValidateRange(1, 60000)][int]$CleanupMilliseconds = 5000,
        [string]$CommandLabel = ''
    )

    $stopwatch = [Diagnostics.Stopwatch]::StartNew()
    $startInfo = [Diagnostics.ProcessStartInfo]::new()
    $startInfo.FileName = $FilePath
    $startInfo.UseShellExecute = $false
    $startInfo.CreateNoWindow = $true
    $startInfo.RedirectStandardOutput = $true
    $startInfo.RedirectStandardError = $true
    foreach ($argument in $Arguments) {
        [void]$startInfo.ArgumentList.Add([string]$argument)
    }

    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $startInfo
    $stdout = ''
    $stderr = ''
    $status = 'Failed'
    $exitCode = 2
    $treeCleaned = $true
    try {
        [void]$process.Start()
        $stdoutTask = $process.StandardOutput.ReadToEndAsync()
        $stderrTask = $process.StandardError.ReadToEndAsync()
        if (-not $process.WaitForExit($TimeoutMilliseconds)) {
            $status = 'TimedOut'
            $exitCode = 124
            $treeCleaned = $false
            try {
                $process.Kill($true)
                $treeCleaned = $process.WaitForExit($CleanupMilliseconds)
            } catch {
                $treeCleaned = $process.HasExited
            }
        } else {
            $exitCode = $process.ExitCode
            $status = if ($exitCode -eq 0) { 'Succeeded' } else { 'Failed' }
        }
        if ($process.HasExited) {
            $stdout = $stdoutTask.GetAwaiter().GetResult()
            $stderr = $stderrTask.GetAwaiter().GetResult()
        }
    } catch {
        $stderr = $_.Exception.Message
        $status = 'Failed'
        $exitCode = 2
        $treeCleaned = $process.HasExited
    } finally {
        $stopwatch.Stop()
        $process.Dispose()
    }

    $summary = (($stderr, $stdout | Where-Object { $_ }) -join "`n").Trim()
    return [pscustomobject]@{
        CommandLabel = if ($CommandLabel) { $CommandLabel } else { [IO.Path]::GetFileName($FilePath) }
        Status = $status
        Succeeded = $status -eq 'Succeeded'
        ExitCode = $exitCode
        Attempts = 1
        DurationMs = [int64]$stopwatch.ElapsedMilliseconds
        ProcessTreeCleaned = [bool]$treeCleaned
        StandardOutput = $stdout.Trim()
        StandardError = $stderr.Trim()
        Summary = $summary
    }
}

function Resolve-HBPythonLauncher {
    <#
    .SYNOPSIS
    Ermittelt einen ausführbaren Python-3-Launcher durch begrenzte Probes.

    .DESCRIPTION
    Probes python3, python, and py -3 in that order unless explicit candidates
    are supplied. Returned display data contains only the public launcher label.
    #>
    [CmdletBinding()]
    param(
        [object[]]$Candidates = @(),
        [ValidateRange(100, 30000)][int]$TimeoutMilliseconds = 5000
    )

    if ($Candidates.Count -eq 0) {
        $resolved = [Collections.Generic.List[object]]::new()
        foreach ($name in @('python3', 'python', 'py')) {
            $command = Get-Command $name -ErrorAction SilentlyContinue | Select-Object -First 1
            if ($null -eq $command) { continue }
            $prefix = if ($name -eq 'py') { @('-3') } else { @() }
            $resolved.Add([pscustomobject]@{
                Label = if ($name -eq 'py') { 'py -3' } else { $name }
                FilePath = $command.Source
                Arguments = $prefix
            })
        }
        $Candidates = @($resolved)
    }

    foreach ($candidate in $Candidates) {
        $label = [string]$candidate.Label
        $arguments = @($candidate.Arguments | ForEach-Object { [string]$_ })
        $arguments += @('-c', 'import sys; print(sys.version_info[0])')
        $probe = Invoke-HBBoundedProcess -FilePath ([string]$candidate.FilePath) `
            -Arguments $arguments -TimeoutMilliseconds $TimeoutMilliseconds `
            -CommandLabel $label
        $majorText = ($probe.StandardOutput -split '\r?\n' | Select-Object -Last 1).Trim()
        $major = 0
        if ($probe.Succeeded -and [int]::TryParse($majorText, [ref]$major) -and $major -eq 3) {
            return [pscustomobject]@{
                Label = $label
                FilePath = [string]$candidate.FilePath
                PrefixArguments = @($candidate.Arguments | ForEach-Object { [string]$_ })
                MajorVersion = $major
                Display = $label
                ProbeDurationMs = $probe.DurationMs
            }
        }
    }

    throw 'Kein nutzbarer Python-3-Launcher gefunden / no usable Python 3 launcher found.'
}

function Test-HBTransientFailure {
    <#
    .SYNOPSIS
    Klassifiziert retryfähige Netzwerkfehler. / Classifies retryable network failures.
    #>
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Summary)

    if ($Summary -match '(?i)auth(?:entication|orization)?|permission|forbidden|not found|dirty|ahead|diverged') {
        return $false
    }
    return $Summary -match '(?i)timed?\s*out|timeout|connection\s+(?:was\s+)?(?:reset|closed|aborted)|temporary failure|could not resolve host|name resolution|http\s+50[234]'
}

function Invoke-HBWithRetry {
    <#
    .SYNOPSIS
    Wiederholt ausschließlich begrenzte transiente Fehler.

    .DESCRIPTION
    Invokes an operation up to MaximumAttempts. Backoff and jitter are capped;
    terminal authentication and repository-state failures return immediately.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][scriptblock]$Operation,
        [ValidateRange(1, 10)][int]$MaximumAttempts = 3,
        [ValidateRange(0, 60000)][int]$BaseDelayMilliseconds = 250,
        [ValidateRange(0, 60000)][int]$MaximumDelayMilliseconds = 3000
    )

    for ($attempt = 1; $attempt -le $MaximumAttempts; $attempt++) {
        $result = & $Operation
        $succeeded = $result -is [bool] -and $result
        if ($result -isnot [bool] -and $null -ne $result.PSObject.Properties['Succeeded']) {
            $succeeded = [bool]$result.Succeeded
        }
        $summary = if ($result -is [bool]) {
            if ($result) { 'ok' } else { 'operation failed' }
        } elseif ($null -ne $result.PSObject.Properties['Summary']) {
            [string]$result.Summary
        } else {
            [string]$result
        }
        if ($succeeded -or $attempt -eq $MaximumAttempts -or
            -not (Test-HBTransientFailure -Summary $summary)) {
            $properties = [ordered]@{}
            if ($result -isnot [bool]) {
                foreach ($property in $result.PSObject.Properties) {
                    if ($property.Name -ne 'Attempts') {
                        $properties[$property.Name] = $property.Value
                    }
                }
            } else {
                $properties.Succeeded = $succeeded
                $properties.Summary = $summary
            }
            $properties.Attempts = $attempt
            return [pscustomobject]$properties
        }
        $exponent = [Math]::Pow(2, $attempt - 1)
        $delay = [int][Math]::Min($MaximumDelayMilliseconds, $BaseDelayMilliseconds * $exponent)
        $jitterMaximum = [Math]::Max(1, [int]($delay / 4) + 1)
        $jitter = if ($delay -gt 0) { Get-Random -Minimum 0 -Maximum $jitterMaximum } else { 0 }
        Start-Sleep -Milliseconds ($delay + $jitter)
    }
}

function Get-HBFileSha256 {
    <#
    .SYNOPSIS
    Liefert einen kleingeschriebenen SHA-256-Dateihash.
    #>
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Path)

    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) { return 'MISSING' }
    return (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToLowerInvariant()
}

function Write-HBResumeEvidence {
    <#
    .SYNOPSIS
    Schreibt Resume-Evidence atomar. / Atomically writes resume evidence.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$RunId,
        [Parameter(Mandatory)][string]$Phase,
        [Parameter(Mandatory)][object[]]$Files,
        [Parameter(Mandatory)][ValidateSet('Prepared', 'Applied', 'Completed', 'Archived')][string]$Status,
        [Parameter(Mandatory)][string]$NextAction
    )

    $guid = [Guid]::Empty
    if (-not [Guid]::TryParse($RunId, [ref]$guid) -or $guid -eq [Guid]::Empty) {
        throw 'RunId muss eine UUID sein / must be a UUID.'
    }
    $normalizedFiles = foreach ($file in $Files) {
        $relative = ([string]$file.Path).Replace('\', '/')
        if (-not $relative -or [IO.Path]::IsPathRooted($relative) -or
            ($relative -split '/' | Where-Object { $_ -eq '..' })) {
            throw "Unsicherer Resume-Pfad / unsafe resume path: ${relative}"
        }
        [ordered]@{
            path = $relative
            beforeSha256 = ([string]$file.BeforeSha256).ToLowerInvariant()
            afterSha256 = ([string]$file.AfterSha256).ToLowerInvariant()
        }
    }
    $payload = [ordered]@{
        schemaVersion = '1.0'
        runId = $RunId
        phase = $Phase
        status = $Status
        files = @($normalizedFiles | Sort-Object path)
        nextAction = $NextAction
        updatedAt = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ')
    }
    $parent = Split-Path -Parent ([IO.Path]::GetFullPath($Path))
    [IO.Directory]::CreateDirectory($parent) | Out-Null
    $temporary = Join-Path $parent ('.' + [IO.Path]::GetFileName($Path) + '.' + [Guid]::NewGuid().ToString('N') + '.tmp')
    $json = $payload | ConvertTo-Json -Depth 8
    [IO.File]::WriteAllText($temporary, $json + "`n", [Text.UTF8Encoding]::new($false))
    try {
        [IO.File]::Move($temporary, [IO.Path]::GetFullPath($Path), $true)
    } finally {
        if (Test-Path -LiteralPath $temporary) {
            Remove-Item -LiteralPath $temporary -Force
        }
    }
    return [pscustomobject]$payload
}

function Test-HBResumeEvidence {
    <#
    .SYNOPSIS
    Prüft Resume-Evidence gegen aktuelle Hashes und Dirty-Pfade.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$Root,
        [string[]]$DirtyPaths = @()
    )

    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
        return [pscustomobject]@{ Valid = $false; Reason = 'EvidenceMissing'; RunId = 'N/A' }
    }
    try {
        $data = Get-Content -LiteralPath $Path -Raw -Encoding UTF8 | ConvertFrom-Json
    } catch {
        return [pscustomobject]@{ Valid = $false; Reason = 'EvidenceInvalidJson'; RunId = 'N/A' }
    }
    if ($data.schemaVersion -ne '1.0' -or $data.status -notin @('Applied', 'Completed')) {
        return [pscustomobject]@{ Valid = $false; Reason = 'EvidenceStateInvalid'; RunId = [string]$data.runId }
    }
    $rootFull = [IO.Path]::GetFullPath($Root).TrimEnd([IO.Path]::DirectorySeparatorChar)
    $expected = [Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($file in @($data.files)) {
        $relative = ([string]$file.path).Replace('\', '/')
        if (-not $relative -or [IO.Path]::IsPathRooted($relative) -or
            ($relative -split '/' | Where-Object { $_ -eq '..' }) -or
            -not $expected.Add($relative)) {
            return [pscustomobject]@{ Valid = $false; Reason = 'EvidencePathInvalid'; RunId = [string]$data.runId }
        }
        $currentPath = [IO.Path]::GetFullPath((Join-Path $rootFull $relative))
        if (-not $currentPath.StartsWith($rootFull + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) {
            return [pscustomobject]@{ Valid = $false; Reason = 'EvidenceTraversal'; RunId = [string]$data.runId }
        }
        if ((Get-HBFileSha256 -Path $currentPath) -ne ([string]$file.afterSha256).ToLowerInvariant()) {
            return [pscustomobject]@{ Valid = $false; Reason = "HashMismatch:${relative}"; RunId = [string]$data.runId }
        }
    }
    $dirty = [Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($item in $DirtyPaths) {
        [void]$dirty.Add(([string]$item).Replace('\', '/'))
    }
    if ($dirty.Count -ne $expected.Count) {
        return [pscustomobject]@{ Valid = $false; Reason = 'DirtyPathCountMismatch'; RunId = [string]$data.runId }
    }
    foreach ($item in $dirty) {
        if (-not $expected.Contains($item)) {
            return [pscustomobject]@{ Valid = $false; Reason = "UnknownDirtyPath:${item}"; RunId = [string]$data.runId }
        }
    }
    return [pscustomobject]@{ Valid = $true; Reason = 'ExactMatch'; RunId = [string]$data.runId }
}

function Get-HBGitNormalizedHash {
    <#
    .SYNOPSIS
    Hasht eine Datei unter dem Git-Attributvertrag des Zielpfads.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$Repository,
        [Parameter(Mandatory)][string]$Path,
        [Parameter(Mandatory)][string]$RepositoryRelativePath
    )

    $result = (& git -C $Repository hash-object "--path=${RepositoryRelativePath}" -- $Path 2>$null | Out-String).Trim()
    if ($LASTEXITCODE -ne 0 -or $result -notmatch '^[0-9a-fA-F]{40,64}$') {
        throw "Git-normalisierter Hash fehlgeschlagen / failed: ${RepositoryRelativePath}"
    }
    return $result.ToLowerInvariant()
}

function Get-HBPackageResults {
    <#
    .SYNOPSIS
    Reduziert Paketbeobachtungen auf genau einen finalen Status je ID.
    #>
    [CmdletBinding()]
    param([Parameter(Mandatory)][object[]]$Observations)

    $groups = $Observations | Group-Object { ([string]$_.Id).ToLowerInvariant() }
    foreach ($group in $groups | Sort-Object Name) {
        $statuses = @($group.Group | ForEach-Object { ([string]$_.Status).ToUpperInvariant() } | Sort-Object -Unique)
        $final = if ($statuses.Count -eq 0 -or 'CONFLICT' -in $statuses) {
            'CONFLICT'
        } elseif ('OK' -in $statuses -and $statuses.Count -gt 1) {
            'CONFLICT'
        } elseif ('FAILED' -in $statuses) {
            'FAILED'
        } elseif ('DEFERRED_ADMIN_REQUIRED' -in $statuses) {
            'DEFERRED_ADMIN_REQUIRED'
        } elseif ('MISSING' -in $statuses) {
            'MISSING'
        } else {
            'OK'
        }
        [pscustomobject]@{
            CanonicalId = $group.Name
            FinalStatus = $final
            Observations = @($group.Group | ForEach-Object {
                [pscustomobject]@{
                    Status = ([string]$_.Status).ToUpperInvariant()
                    Evidence = [string]$_.Evidence
                }
            })
        }
    }
}

function Get-HBCanonicalExitCode {
    <#
    .SYNOPSIS
    Ordnet fachliche Abschlussklassen einem Prozess-Exitcode zu.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [ValidateSet('SUCCESS', 'SUCCESS_WITH_WARNINGS', 'DRIFT', 'PARTIAL', 'BLOCKED', 'FAILED', 'DEFERRED_ADMIN_REQUIRED', 'REPAIRED')]
        [string]$Status
    )

    switch ($Status) {
        { $_ -in @('SUCCESS', 'SUCCESS_WITH_WARNINGS') } { return 0 }
        { $_ -in @('DRIFT', 'PARTIAL', 'BLOCKED', 'DEFERRED_ADMIN_REQUIRED') } { return 1 }
        'FAILED' { return 2 }
        'REPAIRED' { return 3 }
    }
}

Export-ModuleMember -Function @(
    'Get-HBMaintenanceMode',
    'Invoke-HBBoundedProcess',
    'Resolve-HBPythonLauncher',
    'Test-HBTransientFailure',
    'Invoke-HBWithRetry',
    'Get-HBFileSha256',
    'Write-HBResumeEvidence',
    'Test-HBResumeEvidence',
    'Get-HBGitNormalizedHash',
    'Get-HBPackageResults',
    'Get-HBCanonicalExitCode'
)
