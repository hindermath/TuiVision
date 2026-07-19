#Requires -Version 7
<#
.SYNOPSIS
Renders the generated ASCII Statistics Profile 2 block.

.DESCRIPTION
Reads docs/project-statistics.config.json, derives repository statistics from
Git, and updates only the marked generated block in docs/project-statistics.md.
The renderer excludes its own ledger and STATS.md from volume calculations.

.PARAMETER Repo
Repository to inspect. Defaults to the current directory.

.PARAMETER CheckOnly
Checks whether the generated block is current without writing files.

.PARAMETER Json
Writes a machine-readable status object.

.EXAMPLE
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly

.EXAMPLE
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -WhatIf

.EXAMPLE
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo .
#>
[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
param(
    [string]$Repo = '.',
    [switch]$CheckOnly,
    [switch]$Json
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$script:BeginMarker = '<!-- project-statistics-v2:begin -->'
$script:EndMarker = '<!-- project-statistics-v2:end -->'
$script:Invariant = [Globalization.CultureInfo]::InvariantCulture

if (-not ('HomeBaseline.StatisticsTextFile' -as [type])) {
    Add-Type -TypeDefinition @'
using System.IO;

namespace HomeBaseline
{
    public static class StatisticsTextFile
    {
        public static long? CountLines(string path)
        {
            const int BufferSize = 65536;
            byte[] buffer = new byte[BufferSize];
            long lineFeeds = 0;
            long carriageReturns = 0;
            int lastByte = -1;
            bool hasBytes = false;

            using (FileStream stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite,
                BufferSize,
                FileOptions.SequentialScan))
            {
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    hasBytes = true;
                    for (int index = 0; index < bytesRead; index++)
                    {
                        byte value = buffer[index];
                        if (value == 0)
                        {
                            return null;
                        }
                        if (value == 10)
                        {
                            lineFeeds++;
                        }
                        else if (value == 13)
                        {
                            carriageReturns++;
                        }
                    }
                    lastByte = buffer[bytesRead - 1];
                }
            }

            if (!hasBytes)
            {
                return 0;
            }
            if (lineFeeds > 0)
            {
                return lineFeeds + (lastByte == 10 ? 0 : 1);
            }
            return carriageReturns + (lastByte == 13 ? 0 : 1);
        }
    }
}
'@
}

function Invoke-NativeCapture {
    param(
        [Parameter(Mandatory)][string]$Executable,
        [Parameter(Mandatory)][string[]]$Arguments,
        [Parameter(Mandatory)][string]$WorkingDirectory,
        [switch]$AllowFailure
    )

    $startInfo = [Diagnostics.ProcessStartInfo]::new()
    $startInfo.FileName = $Executable
    $startInfo.WorkingDirectory = $WorkingDirectory
    $startInfo.UseShellExecute = $false
    $startInfo.RedirectStandardOutput = $true
    $startInfo.RedirectStandardError = $true
    $startInfo.StandardOutputEncoding = [Text.Encoding]::UTF8
    $startInfo.StandardErrorEncoding = [Text.Encoding]::UTF8
    foreach ($argument in $Arguments) {
        $null = $startInfo.ArgumentList.Add($argument)
    }

    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $startInfo
    $null = $process.Start()
    $stdout = $process.StandardOutput.ReadToEnd()
    $stderr = $process.StandardError.ReadToEnd()
    $process.WaitForExit()

    if ($process.ExitCode -ne 0 -and -not $AllowFailure) {
        throw "$Executable failed with exit code $($process.ExitCode): $stderr"
    }

    [pscustomobject]@{
        ExitCode = $process.ExitCode
        StdOut = $stdout
        StdErr = $stderr
    }
}

function Invoke-GitCapture {
    param(
        [Parameter(Mandatory)][string]$Repository,
        [Parameter(Mandatory)][string[]]$Arguments,
        [switch]$AllowFailure
    )

    Invoke-NativeCapture -Executable 'git' -Arguments (@('-C', $Repository) + $Arguments) `
        -WorkingDirectory $Repository -AllowFailure:$AllowFailure
}

function ConvertTo-NormalizedText {
    param([Parameter(Mandatory)][string]$Text)
    ($Text -replace "`r`n", "`n" -replace "`r", "`n").TrimEnd() + "`n"
}

function Test-SafeRelativePattern {
    param([Parameter(Mandatory)][string]$Pattern)

    if ([IO.Path]::IsPathRooted($Pattern)) {
        return $false
    }
    $parts = $Pattern.Replace('\', '/').Split('/', [StringSplitOptions]::RemoveEmptyEntries)
    -not ($parts -contains '..')
}

function Get-Configuration {
    param(
        [Parameter(Mandatory)][string]$ConfigFile
    )

    if (-not (Test-Path -LiteralPath $ConfigFile -PathType Leaf)) {
        throw "Statistics configuration not found: $ConfigFile"
    }

    $configurationJson = Get-Content -LiteralPath $ConfigFile -Raw -Encoding UTF8
    $repositoryRoot = Split-Path (Split-Path $ConfigFile -Parent) -Parent
    $schemaFile = Join-Path $repositoryRoot 'scripts/config/project-statistics.schema.json'
    if (-not (Test-Path -LiteralPath $schemaFile -PathType Leaf)) {
        throw "Statistics schema not found: $schemaFile"
    }
    if (-not (Test-Json -Json $configurationJson -SchemaFile $schemaFile -ErrorAction Stop)) {
        throw "Statistics configuration does not match schema: $ConfigFile"
    }
    $configuration = $configurationJson | ConvertFrom-Json -Depth 20

    if ([int]$configuration.schemaVersion -ne 1) {
        throw 'Unsupported project statistics schemaVersion. Expected 1.'
    }
    if ([int]$configuration.methodologyVersion -ne 2) {
        throw 'Unsupported project statistics methodologyVersion. Expected 2.'
    }
    if ([string]::IsNullOrWhiteSpace([string]$configuration.repositoryName)) {
        throw 'repositoryName must not be empty.'
    }
    if ([int]$configuration.activityWindowWeeks -lt 1 -or
        [int]$configuration.activityWindowWeeks -gt 104) {
        throw 'activityWindowWeeks must be between 1 and 104.'
    }
    if ([double]$configuration.references.conservativeLinesPerDay -le 0 -or
        [double]$configuration.references.thorstenLinesPerDay -le 0) {
        throw 'Reference line rates must be greater than zero.'
    }

    try {
        $null = [TimeZoneInfo]::FindSystemTimeZoneById([string]$configuration.timeZone)
    } catch {
        throw "Unsupported timeZone '$($configuration.timeZone)'."
    }

    $phaseSlots = [Collections.Generic.HashSet[int]]::new()
    $phaseIds = [Collections.Generic.HashSet[string]]::new(
        [StringComparer]::OrdinalIgnoreCase
    )
    foreach ($phase in @($configuration.phases)) {
        if ([int]$phase.slot -lt 0) {
            throw 'Phase slots must be zero or greater.'
        }
        if (-not $phaseSlots.Add([int]$phase.slot)) {
            throw "Duplicate phase slot: $($phase.slot)"
        }
        if ([string]$phase.id -notmatch '^[a-z0-9][a-z0-9._-]*$') {
            throw "Unsafe phase id: $($phase.id)"
        }
        if (-not $phaseIds.Add([string]$phase.id)) {
            throw "Duplicate phase id: $($phase.id)"
        }
        if ([long]$phase.netLines -lt 0) {
            throw "Phase netLines must be zero or greater: $($phase.id)"
        }
        $activeDaysProperty = $phase.PSObject.Properties['activeDays']
        if ($null -ne $activeDaysProperty -and [int]$activeDaysProperty.Value -lt 1) {
            throw "Phase activeDays must be greater than zero: $($phase.id)"
        }
    }

    foreach ($pattern in @($configuration.excludedPaths)) {
        if (-not (Test-SafeRelativePattern -Pattern ([string]$pattern))) {
            throw "Unsafe excluded path pattern: $pattern"
        }
    }
    foreach ($override in @($configuration.categoryOverrides)) {
        if (-not (Test-SafeRelativePattern -Pattern ([string]$override.pattern))) {
            throw "Unsafe category override pattern: $($override.pattern)"
        }
        if ([string]$override.category -notin @(
                'Production', 'Tests', 'Documentation', 'Scripts',
                'Configuration', 'DataMedia', 'Other'
            )) {
            throw "Unsupported artifact category: $($override.category)"
        }
    }

    $configuration
}

function Test-MatchesPattern {
    param(
        [Parameter(Mandatory)][string]$RelativePath,
        [Parameter(Mandatory)][string]$Pattern
    )

    $wildcard = [Management.Automation.WildcardPattern]::new(
        $Pattern.Replace('\', '/'),
        [Management.Automation.WildcardOptions]::IgnoreCase
    )
    $wildcard.IsMatch($RelativePath.Replace('\', '/'))
}

function Test-ExcludedPath {
    param(
        [Parameter(Mandatory)][string]$RelativePath,
        [Parameter(Mandatory)]$Configuration
    )

    $normalized = $RelativePath.Replace('\', '/')
    if ($normalized -in @('docs/project-statistics.md', 'STATS.md')) {
        return $true
    }
    foreach ($pattern in @($Configuration.excludedPaths)) {
        if (Test-MatchesPattern -RelativePath $normalized -Pattern ([string]$pattern)) {
            return $true
        }
    }
    $false
}

function Get-TextLineCount {
    param([Parameter(Mandatory)][string]$File)

    [HomeBaseline.StatisticsTextFile]::CountLines($File)
}

function Get-ArtifactCategory {
    param(
        [Parameter(Mandatory)][string]$RelativePath,
        [Parameter(Mandatory)]$Configuration
    )

    $normalized = $RelativePath.Replace('\', '/')
    foreach ($override in @($Configuration.categoryOverrides)) {
        if (Test-MatchesPattern -RelativePath $normalized -Pattern ([string]$override.pattern)) {
            return [string]$override.category
        }
    }

    $extension = [IO.Path]::GetExtension($normalized).ToLowerInvariant()
    if ($normalized -match '(^|/)\.specify(/|$)') {
        return 'Documentation'
    }
    if ($normalized -match '(^|/)(tests?|testdata)(/|$)' -or
        $normalized -match '(^|/)[^/]*(tests?|spec)\.[^/]+$') {
        return 'Tests'
    }
    if ($normalized -match '(^|/)(docs?|documentation|guides?)(/|$)' -or
        $extension -in @('.md', '.mdx', '.rst', '.adoc')) {
        return 'Documentation'
    }
    if ($normalized -match '(^|/)scripts?(/|$)' -or
        $extension -in @('.sh', '.ps1', '.psm1', '.psd1', '.cmd', '.bat')) {
        return 'Scripts'
    }
    if ($extension -in @(
            '.json', '.jsonc', '.yaml', '.yml', '.toml', '.ini', '.conf',
            '.config', '.xml', '.props', '.targets', '.lock', '.editorconfig'
        )) {
        return 'Configuration'
    }
    if ($normalized -match '(^|/)(data|media|assets?)(/|$)' -or
        $extension -in @('.csv', '.tsv', '.jsonl', '.sql', '.graphql')) {
        return 'DataMedia'
    }
    if ($normalized -match '(^|/)(src|source|app|lib)(/|$)' -or
        $extension -in @(
            '.c', '.h', '.cc', '.cpp', '.cs', '.fs', '.go', '.java', '.kt',
            '.kts', '.py', '.rb', '.rs', '.scala', '.swift', '.ts', '.tsx',
            '.js', '.jsx', '.dart', '.ex', '.exs', '.erl', '.hrl', '.hs'
        )) {
        return 'Production'
    }
    'Other'
}

function Get-ArtifactSnapshot {
    param(
        [Parameter(Mandatory)][string]$Repository,
        [Parameter(Mandatory)]$Configuration
    )

    $tracked = (Invoke-GitCapture -Repository $Repository -Arguments @('ls-files')).StdOut
    $categories = [ordered]@{
        Production = 0L
        Tests = 0L
        Documentation = 0L
        Scripts = 0L
        Configuration = 0L
        DataMedia = 0L
        Other = 0L
    }
    $files = [ordered]@{}
    foreach ($key in $categories.Keys) {
        $files[$key] = 0
    }

    foreach ($relativePath in ($tracked -split "`r?`n")) {
        if ([string]::IsNullOrWhiteSpace($relativePath) -or
            (Test-ExcludedPath -RelativePath $relativePath -Configuration $Configuration)) {
            continue
        }
        $fullPath = Join-Path $Repository $relativePath
        if (-not (Test-Path -LiteralPath $fullPath -PathType Leaf)) {
            continue
        }
        $lineCount = Get-TextLineCount -File $fullPath
        if ($null -eq $lineCount) {
            continue
        }
        $category = Get-ArtifactCategory -RelativePath $relativePath `
            -Configuration $Configuration
        $categories[$category] += [long]$lineCount
        $files[$category]++
    }

    [pscustomobject]@{
        Categories = $categories
        Files = $files
        TotalLines = [long](($categories.Values | Measure-Object -Sum).Sum)
        TotalFiles = [int](($files.Values | Measure-Object -Sum).Sum)
    }
}

function Get-HistoryEntry {
    param(
        [Parameter(Mandatory)][string]$Repository,
        [Parameter(Mandatory)]$Configuration
    )

    $result = Invoke-GitCapture -Repository $Repository -Arguments @(
        'log', '--no-merges', '--format=@@%H%x09%cI', '--numstat', '--', '.'
    )
    $timeZone = [TimeZoneInfo]::FindSystemTimeZoneById([string]$Configuration.timeZone)
    $entries = [Collections.Generic.List[object]]::new()
    $currentHash = $null
    $currentDate = $null
    $currentLines = 0L

    $flush = {
        if ($null -ne $currentHash -and $currentLines -gt 0) {
            $entries.Add([pscustomobject]@{
                    Hash = $currentHash
                    Date = $currentDate
                    Lines = $currentLines
                })
        }
    }

    foreach ($line in ($result.StdOut -split "`r?`n")) {
        if ($line.StartsWith('@@', [StringComparison]::Ordinal)) {
            & $flush
            $parts = $line.Substring(2).Split("`t", 2)
            $currentHash = $parts[0]
            $timestamp = [DateTimeOffset]::Parse($parts[1], $script:Invariant)
            $currentDate = [TimeZoneInfo]::ConvertTime($timestamp, $timeZone).Date
            $currentLines = 0L
            continue
        }
        if ([string]::IsNullOrWhiteSpace($line) -or $null -eq $currentHash) {
            continue
        }
        $parts = $line.Split("`t", 3)
        if ($parts.Count -ne 3 -or $parts[0] -notmatch '^\d+$' -or
            $parts[1] -notmatch '^\d+$') {
            continue
        }
        if (Test-ExcludedPath -RelativePath $parts[2] -Configuration $Configuration) {
            continue
        }
        $currentLines += [long]$parts[0] + [long]$parts[1]
    }
    & $flush
    $entries.ToArray()
}

function Get-WeekStart {
    param([Parameter(Mandatory)][datetime]$Date)
    $Date.Date.AddDays(-[int]$Date.DayOfWeek)
}

function Get-AsOfDate {
    param([Parameter(Mandatory)][TimeZoneInfo]$TimeZone)

    if ($env:SOURCE_DATE_EPOCH) {
        if ($env:SOURCE_DATE_EPOCH -notmatch '^\d+$') {
            throw 'SOURCE_DATE_EPOCH must contain non-negative epoch seconds.'
        }
        $instant = [DateTimeOffset]::FromUnixTimeSeconds([long]$env:SOURCE_DATE_EPOCH)
        return [TimeZoneInfo]::ConvertTime($instant, $TimeZone).Date
    }
    [TimeZoneInfo]::ConvertTime([DateTimeOffset]::UtcNow, $TimeZone).Date
}

function Get-NiceMaximum {
    param([double]$Value)

    if ($Value -le 0) {
        return 1.0
    }
    $power = [Math]::Pow(10, [Math]::Floor([Math]::Log10($Value)))
    foreach ($factor in @(1.0, 2.0, 5.0, 10.0)) {
        $candidate = $factor * $power
        if ($candidate -ge $Value) {
            return $candidate
        }
    }
    10.0 * $power
}

function Format-Integer {
    param([double]$Value)
    [Math]::Round($Value).ToString('0', $script:Invariant)
}

function Format-Decimal {
    param([double]$Value)
    $Value.ToString('0.0', $script:Invariant)
}

function Format-Gauge {
    param(
        [double]$Value,
        [double]$Maximum,
        [int]$Width = 20
    )

    if ($Maximum -le 0) {
        return '[' + ('.' * $Width) + ']'
    }
    $filled = [int][Math]::Round(
        [Math]::Min(1.0, [Math]::Max(0.0, $Value / $Maximum)) * $Width,
        [MidpointRounding]::AwayFromZero
    )
    if ($Value -gt 0 -and $filled -eq 0) {
        $filled = 1
    }
    '[' + ('#' * $filled) + ('.' * ($Width - $filled)) + ']'
}

function Get-ActivityLevel {
    param([long]$Lines)
    if ($Lines -eq 0) { return '0' }
    if ($Lines -lt 80) { return '1' }
    if ($Lines -lt 400) { return '2' }
    if ($Lines -lt 1600) { return '3' }
    '4'
}

function Get-LongestStreak {
    param([datetime[]]$Dates)

    if ($Dates.Count -eq 0) {
        return [pscustomobject]@{ Days = 0; Start = $null; End = $null }
    }
    $ordered = @($Dates | Sort-Object -Unique)
    $bestDays = 1
    $bestStart = $ordered[0]
    $bestEnd = $ordered[0]
    $currentDays = 1
    $currentStart = $ordered[0]

    for ($index = 1; $index -lt $ordered.Count; $index++) {
        if (($ordered[$index] - $ordered[$index - 1]).Days -eq 1) {
            $currentDays++
        } else {
            $currentDays = 1
            $currentStart = $ordered[$index]
        }
        if ($currentDays -gt $bestDays) {
            $bestDays = $currentDays
            $bestStart = $currentStart
            $bestEnd = $ordered[$index]
        }
    }
    [pscustomobject]@{ Days = $bestDays; Start = $bestStart; End = $bestEnd }
}

function Format-HeatmapBlock {
    param(
        [Parameter(Mandatory)][object[]]$Weeks,
        [Parameter(Mandatory)][hashtable]$Daily,
        [Parameter(Mandatory)][datetime]$AsOfDate,
        [Parameter(Mandatory)][int]$FirstIndex
    )

    $lastIndex = $FirstIndex + $Weeks.Count - 1
    $lastDate = $Weeks[-1].Start.AddDays(6)
    $lines = [Collections.Generic.List[string]]::new()
    $lines.Add((
            'Wochen / Weeks {0:00}..{1:00} | {2:yyyy-MM-dd}..{3:yyyy-MM-dd}' -f
            ($FirstIndex + 1), ($lastIndex + 1), $Weeks[0].Start, $lastDate
        ))
    $dayLabels = @('So/Su', 'Mo/Mo', 'Di/Tu', 'Mi/We', 'Do/Th', 'Fr/Fr', 'Sa/Sa')
    for ($dayOffset = 0; $dayOffset -lt 7; $dayOffset++) {
        $cells = [Collections.Generic.List[string]]::new()
        foreach ($week in $Weeks) {
            $date = $week.Start.AddDays($dayOffset)
            if ($date -gt $AsOfDate) {
                $cells.Add('-')
            } else {
                $key = $date.ToString('yyyy-MM-dd', $script:Invariant)
                $value = $(if ($Daily.ContainsKey($key)) { [long]$Daily[$key] } else { 0L })
                $cells.Add((Get-ActivityLevel -Lines $value))
            }
        }
        $lines.Add(('{0}  {1}' -f $dayLabels[$dayOffset], ($cells -join ' ')))
    }
    $lines -join "`n"
}

function Format-VerticalBlock {
    param(
        [Parameter(Mandatory)][string]$Label,
        [Parameter(Mandatory)][double[]]$Values
    )

    $maximum = $(if ($Values.Count -gt 0) {
            [double](($Values | Measure-Object -Maximum).Maximum)
        } else { 0.0 })
    if ($maximum -le 0) {
        return $Label + "`nKeine Aktivitaet / No activity"
    }
    $cap = Get-NiceMaximum -Value $maximum
    $lines = [Collections.Generic.List[string]]::new()
    $lines.Add($Label)
    for ($level = 6; $level -ge 1; $level--) {
        $threshold = $cap * $level / 6.0
        $axis = $(if ($level -eq 6) {
                'cap ' + (Format-Integer -Value $cap)
            } else {
                Format-Integer -Value $threshold
            })
        $cells = foreach ($value in $Values) {
            if ($value -ge $threshold -and $value -gt 0) { '#' } else { '.' }
        }
        $lines.Add(('{0,12} | {1}' -f $axis, ($cells -join ' ')))
    }
    $lines.Add(('{0,12} +-{1}' -f '0', ('--' * $Values.Count)))
    $lines -join "`n"
}

function ConvertTo-GeneratedBlock {
    param(
        [Parameter(Mandatory)]$Configuration,
        [Parameter(Mandatory)]$Snapshot,
        [Parameter(Mandatory)][AllowEmptyCollection()][object[]]$History,
        [Parameter(Mandatory)][string]$SourceRevision
    )

    $timeZone = [TimeZoneInfo]::FindSystemTimeZoneById([string]$Configuration.timeZone)
    $asOf = Get-AsOfDate -TimeZone $timeZone
    $daily = @{}
    foreach ($entry in $History) {
        $key = $entry.Date.ToString('yyyy-MM-dd', $script:Invariant)
        if (-not $daily.ContainsKey($key)) {
            $daily[$key] = 0L
        }
        $daily[$key] += [long]$entry.Lines
    }

    $allActiveDates = @($daily.Keys | ForEach-Object {
            [datetime]::ParseExact($_, 'yyyy-MM-dd', $script:Invariant)
        } | Sort-Object)

    $currentWeekStart = Get-WeekStart -Date $asOf
    $windowWeeks = [int]$Configuration.activityWindowWeeks
    $windowStart = $currentWeekStart.AddDays(-7 * ($windowWeeks - 1))
    $windowActiveDates = @($allActiveDates | Where-Object {
            $_ -ge $windowStart -and $_ -le $asOf
        })
    $activeDays = $windowActiveDates.Count
    $deliveryRate = $(if ($activeDays -gt 0) {
            [double]$Snapshot.TotalLines / $activeDays
        } else { 0.0 })
    $conservative = [double]$Configuration.references.conservativeLinesPerDay
    $thorsten = [double]$Configuration.references.thorstenLinesPerDay
    $speedConservative = $(if ($activeDays -gt 0) { $deliveryRate / $conservative } else { 0.0 })
    $speedThorsten = $(if ($activeDays -gt 0) { $deliveryRate / $thorsten } else { 0.0 })
    $weeks = [Collections.Generic.List[object]]::new()
    for ($index = 0; $index -lt $windowWeeks; $index++) {
        $start = $windowStart.AddDays(7 * $index)
        $value = 0L
        for ($offset = 0; $offset -lt 7; $offset++) {
            $key = $start.AddDays($offset).ToString('yyyy-MM-dd', $script:Invariant)
            if ($daily.ContainsKey($key)) {
                $value += [long]$daily[$key]
            }
        }
        $weeks.Add([pscustomobject]@{ Start = $start; Value = $value })
    }

    $windowDaily = @{}
    foreach ($key in $daily.Keys) {
        $date = [datetime]::ParseExact($key, 'yyyy-MM-dd', $script:Invariant)
        if ($date -ge $windowStart -and $date -le $asOf) {
            $windowDaily[$key] = $daily[$key]
        }
    }
    $peakDayKey = $null
    $peakDayLines = 0L
    foreach ($key in $windowDaily.Keys) {
        if ([long]$windowDaily[$key] -gt $peakDayLines) {
            $peakDayKey = $key
            $peakDayLines = [long]$windowDaily[$key]
        }
    }
    $peakWeek = $weeks | Sort-Object Value -Descending | Select-Object -First 1
    $streak = Get-LongestStreak -Dates $windowActiveDates

    $historyCommitCount = @($History | Where-Object {
            $_.Date -ge $windowStart -and $_.Date -le $asOf
        }).Count
    $period = '{0:yyyy-MM-dd}..{1:yyyy-MM-dd}' -f $windowStart, $asOf
    $peakDay = $(if ($null -ne $peakDayKey) {
            "$peakDayKey / $(Format-Integer -Value $peakDayLines)"
        } else { 'N/A' })
    $peakWeekText = $(if ($null -ne $peakWeek -and $peakWeek.Value -gt 0) {
            '{0:yyyy-MM-dd} / {1}' -f $peakWeek.Start,
            (Format-Integer -Value $peakWeek.Value)
        } else { 'N/A' })

    $builder = [Text.StringBuilder]::new()
    $null = $builder.AppendLine($script:BeginMarker)
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        'Profil 2 verwendet Git-getrackte Textdateien und sichtbare Git-Aktivitaet. ' +
        'Die Werte beschreiben Lieferdichte, keine persoenliche Arbeitszeit.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*Profile 2 uses Git-tracked text files and visible Git activity. The values ' +
        'describe delivery density, not personal working time.*'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine('| Kennzahl / Metric | Wert / Value |')
    $null = $builder.AppendLine('|---|---:|')
    $null = $builder.AppendLine("| Textbasis / Text base | $(Format-Integer $Snapshot.TotalLines) lines |")
    $null = $builder.AppendLine("| Textdateien / Text files | $($Snapshot.TotalFiles) |")
    $null = $builder.AppendLine("| Beobachtbarer Zeitraum / Observable period | $period |")
    $null = $builder.AppendLine("| Aktivtage / Active days | $activeDays |")
    $null = $builder.AppendLine("| Relevante Commits / Relevant commits | $historyCommitCount |")
    $null = $builder.AppendLine("| Zeilen je Aktivtag / Lines per active day | $(Format-Decimal $deliveryRate) |")
    $null = $builder.AppendLine("| Peak-Tag im Fenster / Peak day in window | $peakDay |")
    $null = $builder.AppendLine("| Peak-Woche im Fenster / Peak week in window | $peakWeekText |")
    $null = $builder.AppendLine("| Laengste Serie / Longest streak | $($streak.Days) days |")
    $null = $builder.AppendLine("| Speedup vs. $(Format-Integer $conservative) lines/day | $(Format-Decimal $speedConservative)x |")
    $null = $builder.AppendLine("| Speedup vs. $(Format-Integer $thorsten) lines/day | $(Format-Decimal $speedThorsten)x |")
    $null = $builder.AppendLine("| Methodik / Methodology | v2; source ``$SourceRevision`` |")

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Artefaktmix / Artifact Mix')
    $null = $builder.AppendLine()
    $null = $builder.AppendLine('```text')
    $categoryLabels = [ordered]@{
        Production = 'Produktiv / Production'
        Tests = 'Tests'
        Documentation = 'Dokumentation / Documentation'
        Scripts = 'Skripte / Scripts'
        Configuration = 'Konfiguration / Configuration'
        DataMedia = 'Daten und Medien / Data and media'
        Other = 'Sonstiger Text / Other text'
    }
    foreach ($category in $categoryLabels.Keys) {
        $value = [long]$Snapshot.Categories[$category]
        $share = $(if ($Snapshot.TotalLines -gt 0) {
                100.0 * $value / $Snapshot.TotalLines
            } else { 0.0 })
        $gauge = Format-Gauge -Value $share -Maximum 100
        $null = $builder.AppendLine((
                '{0,-31} {1} {2,5}% | {3}' -f
                $categoryLabels[$category], $gauge, (Format-Decimal $share),
                (Format-Integer $value)
            ))
    }
    $null = $builder.AppendLine('```')
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        'Die Balken teilen die aktuelle getrackte Textbasis in stabile Kategorien. ' +
        'Prozent und Zeilenwert sind die genaue, textorientierte Aussage.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*The bars split the current tracked text base into stable categories. ' +
        'Percentages and line counts provide the exact text-first result.*'
    )

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Tagesaktivitaet / Daily Activity')
    $null = $builder.AppendLine()
    for ($offset = 0; $offset -lt $weeks.Count; $offset += 26) {
        $count = [Math]::Min(26, $weeks.Count - $offset)
        $blockWeeks = @($weeks.GetRange($offset, $count))
        $null = $builder.AppendLine('```text')
        $null = $builder.AppendLine((Format-HeatmapBlock -Weeks $blockWeeks -Daily $daily `
                    -AsOfDate $asOf -FirstIndex $offset))
        $null = $builder.AppendLine('```')
        $null = $builder.AppendLine()
    }
    $null = $builder.AppendLine(
        'DE: 0 = keine Aenderung; 1 = 1..79; 2 = 80..399; 3 = 400..1599; ' +
        '4 = 1600+ geaenderte Textzeilen; - = noch nicht abgelaufen.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*EN: 0 = no change; 1 = 1..79; 2 = 80..399; 3 = 400..1599; ' +
        '4 = 1600+ changed text lines; - = not elapsed.*'
    )

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Wochenvolumen / Weekly Volume')
    $null = $builder.AppendLine()
    for ($offset = 0; $offset -lt $weeks.Count; $offset += 26) {
        $count = [Math]::Min(26, $weeks.Count - $offset)
        $blockWeeks = @($weeks.GetRange($offset, $count))
        $values = [double[]]@($blockWeeks | ForEach-Object { [double]$_.Value })
        $label = 'Wochen / Weeks {0:00}..{1:00} | {2:yyyy-MM-dd}..{3:yyyy-MM-dd}' -f
            ($offset + 1), ($offset + $count), $blockWeeks[0].Start,
            $blockWeeks[-1].Start.AddDays(6)
        $null = $builder.AppendLine('```text')
        $null = $builder.AppendLine((Format-VerticalBlock -Label $label -Values $values))
        $null = $builder.AppendLine('```')
        $null = $builder.AppendLine()
    }
    $null = $builder.AppendLine(
        'Das Wochenvolumen zeigt Additionen plus Loeschungen. Es ist Aenderungsaktivitaet, ' +
        'nicht die aktuelle Groesse des Repositories.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*Weekly volume shows additions plus deletions. It represents change activity, ' +
        'not the current repository size.*'
    )

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Kumulative Entwicklung / Cumulative Development')
    $null = $builder.AppendLine()
    $running = 0.0
    $cumulative = [Collections.Generic.List[double]]::new()
    foreach ($week in $weeks) {
        $running += [double]$week.Value
        $cumulative.Add($running)
    }
    for ($offset = 0; $offset -lt $weeks.Count; $offset += 26) {
        $count = [Math]::Min(26, $weeks.Count - $offset)
        $values = [double[]]@($cumulative.GetRange($offset, $count))
        $blockWeeks = @($weeks.GetRange($offset, $count))
        $label = 'Wochen / Weeks {0:00}..{1:00} | {2:yyyy-MM-dd}..{3:yyyy-MM-dd}' -f
            ($offset + 1), ($offset + $count), $blockWeeks[0].Start,
            $blockWeeks[-1].Start.AddDays(6)
        $null = $builder.AppendLine('```text')
        $null = $builder.AppendLine((Format-VerticalBlock -Label $label -Values $values))
        $null = $builder.AppendLine('```')
        $null = $builder.AppendLine()
    }
    $null = $builder.AppendLine(
        'Die kumulative Kurve summiert nur das Brutto-Aenderungsvolumen im Fenster. ' +
        'Sie darf nicht als aktuelle Codebasis gelesen werden.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*The cumulative curve sums gross change volume within the window only. ' +
        'It must not be read as the current code base.*'
    )

    $null = $builder.AppendLine()
    $phases = @($Configuration.phases | Sort-Object slot)
    if ($phases.Count -gt 0) {
        $null = $builder.AppendLine('### Phasenvolumen / Phase Volume')
        $null = $builder.AppendLine()
        for ($offset = 0; $offset -lt $phases.Count; $offset += 16) {
            $count = [Math]::Min(16, $phases.Count - $offset)
            $block = @($phases[$offset..($offset + $count - 1)])
            $values = [double[]]@($block | ForEach-Object { [double]$_.netLines })
            $label = 'Slots {0}..{1}' -f $block[0].slot, $block[-1].slot
            $null = $builder.AppendLine('```text')
            $null = $builder.AppendLine((Format-VerticalBlock -Label $label -Values $values))
            $slotLabels = @($block | ForEach-Object { ([int]$_.slot).ToString('00') })
            $null = $builder.AppendLine(('             {0}' -f ($slotLabels -join ' ')))
            $null = $builder.AppendLine('```')
            $null = $builder.AppendLine()
        }
        $null = $builder.AppendLine('| Slot | Phase | Nettozeilen / Net lines |')
        $null = $builder.AppendLine('|---:|---|---:|')
        foreach ($phase in $phases) {
            $null = $builder.AppendLine(
                "| $($phase.slot) | $($phase.labelDe) / $($phase.labelEn) | " +
                "$(Format-Integer $phase.netLines) |"
            )
        }
        $null = $builder.AppendLine()
        $null = $builder.AppendLine(
            'Die festen Slots halten den Phasenvergleich auch bei fehlenden oder ' +
            'spaeter ergaenzten Werten stabil.'
        )
        $null = $builder.AppendLine()
        $null = $builder.AppendLine(
            '*Stable slots keep the phase comparison consistent when values are ' +
            'missing or added later.*'
        )
    } else {
        $null = $builder.AppendLine('### Monatsvolumen / Monthly Volume')
        $null = $builder.AppendLine()
        $monthStart = [datetime]::new($asOf.Year, $asOf.Month, 1).AddMonths(-11)
        $monthValues = [Collections.Generic.List[double]]::new()
        for ($index = 0; $index -lt 12; $index++) {
            $start = $monthStart.AddMonths($index)
            $end = $start.AddMonths(1)
            $sum = 0L
            foreach ($key in $daily.Keys) {
                $date = [datetime]::ParseExact($key, 'yyyy-MM-dd', $script:Invariant)
                if ($date -ge $start -and $date -lt $end) {
                    $sum += [long]$daily[$key]
                }
            }
            $monthValues.Add([double]$sum)
        }
        $null = $builder.AppendLine('```text')
        $null = $builder.AppendLine((Format-VerticalBlock -Label 'Last 12 calendar months' `
                    -Values $monthValues.ToArray()))
        $null = $builder.AppendLine('```')
        $null = $builder.AppendLine()
        $null = $builder.AppendLine(
            'Es liegen keine belastbaren Phasendaten vor. Deshalb zeigt dieses Diagramm ' +
            'Monate und erfindet keine Projektphasen.'
        )
        $null = $builder.AppendLine()
        $null = $builder.AppendLine(
            '*No reliable phase series is available. This chart therefore shows months ' +
            'and does not invent project phases.*'
        )
    }

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Beschleunigungsfaktoren / Acceleration Factors')
    $null = $builder.AppendLine()
    $maxSpeed = [Math]::Max($speedConservative, $speedThorsten)
    $speedCap = 10.0
    foreach ($candidate in @(10.0, 20.0, 50.0, 100.0, 200.0, 500.0)) {
        $speedCap = $candidate
        if ($candidate -ge $maxSpeed) {
            break
        }
    }
    $speedSuffixConservative = $(if ($speedConservative -gt 500) { ' >500x' } else {
            ' ' + (Format-Decimal $speedConservative) + 'x'
        })
    $speedSuffixThorsten = $(if ($speedThorsten -gt 500) { ' >500x' } else {
            ' ' + (Format-Decimal $speedThorsten) + 'x'
        })
    $null = $builder.AppendLine('```text')
    $null = $builder.AppendLine("Scale: 0..$(Format-Integer $speedCap)x")
    $null = $builder.AppendLine((
            '{0,-18} {1}{2}' -f
            "$(Format-Integer $conservative) lines/day",
            (Format-Gauge -Value $speedConservative -Maximum $speedCap),
            $speedSuffixConservative
        ))
    $null = $builder.AppendLine((
            '{0,-18} {1}{2}' -f
            "$(Format-Integer $thorsten) lines/day",
            (Format-Gauge -Value $speedThorsten -Maximum $speedCap),
            $speedSuffixThorsten
        ))
    $null = $builder.AppendLine('```')
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        'Die Faktoren vergleichen sichtbare Lieferdichte mit den dokumentierten ' +
        'manuellen Referenzen. Sie messen keine Arbeitszeit.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*The factors compare visible delivery density with documented manual ' +
        'references. They do not measure working time.*'
    )

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Durchsatzvergleich / Throughput Comparison')
    $null = $builder.AppendLine()
    $throughputCap = Get-NiceMaximum -Value ([Math]::Max($deliveryRate, $thorsten))
    $null = $builder.AppendLine('```text')
    $null = $builder.AppendLine("Scale: 0..$(Format-Integer $throughputCap) lines/day")
    $null = $builder.AppendLine((
            '{0,-18} {1} {2}' -f 'Experienced manual',
            (Format-Gauge -Value $conservative -Maximum $throughputCap),
            (Format-Integer $conservative)
        ))
    $null = $builder.AppendLine((
            '{0,-18} {1} {2}' -f 'Thorsten solo',
            (Format-Gauge -Value $thorsten -Maximum $throughputCap),
            (Format-Integer $thorsten)
        ))
    $null = $builder.AppendLine((
            '{0,-18} {1} {2}' -f 'Visible repository',
            (Format-Gauge -Value $deliveryRate -Maximum $throughputCap),
            (Format-Decimal $deliveryRate)
        ))
    $null = $builder.AppendLine('```')
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        'Die gemeinsame Skala vergleicht Referenzen und sichtbare Lieferdichte. ' +
        'Sie schreibt die Git-Aktivitaet keiner Person oder KI pauschal zu.'
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        '*The common scale compares references with visible delivery density. ' +
        'It does not attribute Git activity to a person or AI by default.*'
    )

    $elapsedWindowDays = ($asOf - $windowStart).Days + 1
    $inactiveDays = [Math]::Max(0, $elapsedWindowDays - $windowActiveDates.Count)
    $streakTextDe = $(if ($streak.Days -gt 0) {
            '{0} Tage ({1:yyyy-MM-dd}..{2:yyyy-MM-dd})' -f
            $streak.Days, $streak.Start, $streak.End
        } else { 'N/A' })
    $streakTextEn = $(if ($streak.Days -gt 0) {
            '{0} days ({1:yyyy-MM-dd}..{2:yyyy-MM-dd})' -f
            $streak.Days, $streak.Start, $streak.End
        } else { 'N/A' })

    $null = $builder.AppendLine()
    $null = $builder.AppendLine('### Textalternative / Text Alternative')
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        "DE: Das Fenster beginnt am $($windowStart.ToString('yyyy-MM-dd')) und endet am " +
        "$($asOf.ToString('yyyy-MM-dd')). Es enthaelt $($windowActiveDates.Count) aktive " +
        "und $inactiveDays inaktive vergangene Tage. Peak-Tag: $peakDay. " +
        "Peak-Woche: $peakWeekText. Laengste Serie: $streakTextDe."
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine(
        "*EN: The window starts on $($windowStart.ToString('yyyy-MM-dd')) and ends on " +
        "$($asOf.ToString('yyyy-MM-dd')). It contains $($windowActiveDates.Count) active " +
        "and $inactiveDays inactive elapsed days. Peak day: $peakDay. " +
        "Peak week: $peakWeekText. Longest streak: $streakTextEn.*"
    )
    $null = $builder.AppendLine()
    $null = $builder.AppendLine('| Monat / Month | Geaenderte Textzeilen / Changed text lines |')
    $null = $builder.AppendLine('|---|---:|')
    $monthStart = [datetime]::new($asOf.Year, $asOf.Month, 1).AddMonths(-11)
    for ($index = 0; $index -lt 12; $index++) {
        $start = $monthStart.AddMonths($index)
        $end = $start.AddMonths(1)
        $sum = 0L
        foreach ($key in $daily.Keys) {
            $date = [datetime]::ParseExact($key, 'yyyy-MM-dd', $script:Invariant)
            if ($date -ge $start -and $date -lt $end) {
                $sum += [long]$daily[$key]
            }
        }
        $null = $builder.AppendLine(
            "| $($start.ToString('yyyy-MM')) | $(Format-Integer $sum) |"
        )
    }
    $null = $builder.AppendLine()
    $null = $builder.AppendLine($script:EndMarker)
    ConvertTo-NormalizedText -Text $builder.ToString()
}

function Assert-GeneratedBlock {
    param([Parameter(Mandatory)][string]$Block)

    $codeBlockMatches = [regex]::Matches(
        $Block,
        '(?ms)^```text\s*\n(.*?)^```\s*$'
    )
    foreach ($match in $codeBlockMatches) {
        foreach ($line in ($match.Groups[1].Value -split "`n")) {
            if ($line.Length -gt 100) {
                throw "Generated ASCII chart exceeds 100 characters: $line"
            }
            foreach ($character in $line.ToCharArray()) {
                if ([int]$character -gt 127) {
                    throw 'Generated chart contains non-ASCII characters.'
                }
            }
        }
    }
    if ($Block -match '[\u2588\u2591\u25A0]') {
        throw 'Generated block contains forbidden Unicode block characters.'
    }
}

function Get-UpdatedLedger {
    param(
        [Parameter(Mandatory)][string]$Existing,
        [Parameter(Mandatory)][string]$Generated
    )

    $normalized = ConvertTo-NormalizedText -Text $Existing
    $beginIndex = $normalized.IndexOf($script:BeginMarker, [StringComparison]::Ordinal)
    $endIndex = $normalized.IndexOf($script:EndMarker, [StringComparison]::Ordinal)
    if ($beginIndex -ge 0 -or $endIndex -ge 0) {
        if ($beginIndex -lt 0 -or $endIndex -lt $beginIndex) {
            throw 'Incomplete or invalid project-statistics-v2 markers.'
        }
        $afterEnd = $endIndex + $script:EndMarker.Length
        $updated = $normalized.Substring(0, $beginIndex) +
            $Generated.TrimEnd() +
            $normalized.Substring($afterEnd)
        return ConvertTo-NormalizedText -Text $updated
    }

    $headingMatches = [regex]::Matches(
        $normalized,
        '(?m)^## Gesamtstatistik(?: / Overall Statistics)?\s*$'
    )
    if ($headingMatches.Count -eq 0) {
        return ConvertTo-NormalizedText -Text (
            $normalized.TrimEnd() +
            "`n`n## Gesamtstatistik / Overall Statistics`n`n" +
            $Generated
        )
    }

    $heading = $headingMatches[-1]
    $prefix = $normalized.Substring(0, $heading.Index)
    $oldSection = $normalized.Substring($heading.Index)
    $oldBody = $oldSection.Substring($heading.Length).Trim()
    if ($oldBody) {
        $archived = [regex]::Replace(
            $oldSection,
            '(?m)^## Gesamtstatistik(?: / Overall Statistics)?\s*$',
            '## Statistikprofil-1-Archiv / Statistics Profile 1 Archive',
            1
        )
        return ConvertTo-NormalizedText -Text (
            $prefix.TrimEnd() + "`n`n" + $archived.TrimEnd() +
            "`n`n## Gesamtstatistik / Overall Statistics`n`n" +
            $Generated
        )
    }

    ConvertTo-NormalizedText -Text (
        $prefix.TrimEnd() +
        "`n`n## Gesamtstatistik / Overall Statistics`n`n" +
        $Generated
    )
}

function Assert-LedgerContract {
    param([Parameter(Mandatory)][string]$LedgerText)

    $beginMatches = [regex]::Matches(
        $LedgerText,
        [regex]::Escape($script:BeginMarker)
    )
    $endMatches = [regex]::Matches(
        $LedgerText,
        [regex]::Escape($script:EndMarker)
    )
    if ($beginMatches.Count -ne 1 -or $endMatches.Count -ne 1 -or
        $endMatches[0].Index -le $beginMatches[0].Index) {
        throw 'Ledger must contain exactly one ordered Statistics Profile 2 marker pair.'
    }

    $headings = [regex]::Matches($LedgerText, '(?m)^## (.+?)\s*$')
    if ($headings.Count -eq 0 -or
        $headings[-1].Groups[1].Value -ne 'Gesamtstatistik / Overall Statistics') {
        throw 'Gesamtstatistik / Overall Statistics must be the final top-level section.'
    }
    if ($beginMatches[0].Index -lt $headings[-1].Index) {
        throw 'Statistics Profile 2 markers must be inside the final overall-statistics section.'
    }
}

function Write-Status {
    param(
        [Parameter(Mandatory)][string]$Status,
        [Parameter(Mandatory)][string]$Repository,
        [Parameter(Mandatory)][string]$Ledger,
        [Parameter(Mandatory)][string]$SourceRevision,
        [Parameter(Mandatory)][bool]$Changed,
        [Parameter(Mandatory)][long]$TotalLines,
        [Parameter(Mandatory)][int]$ActiveDays
    )

    $result = [ordered]@{
        status = $Status
        repository = $Repository
        ledger = $Ledger
        sourceRevision = $SourceRevision
        changed = $Changed
        methodologyVersion = 2
        totalTextLines = $TotalLines
        activeDays = $ActiveDays
    }
    if ($Json) {
        $result | ConvertTo-Json -Depth 5 -Compress
    } else {
        "[$Status] $Ledger"
        "  Source / Quelle: $SourceRevision"
        "  Text lines / Textzeilen: $TotalLines"
        "  Active days / Aktivtage: $ActiveDays"
    }
}

try {
    if ($CheckOnly -and $WhatIfPreference) {
        throw '-CheckOnly and -WhatIf cannot be combined.'
    }

    $repository = (Resolve-Path -LiteralPath $Repo).Path
    $inside = Invoke-GitCapture -Repository $repository -Arguments @(
        'rev-parse', '--is-inside-work-tree'
    )
    if ($inside.StdOut.Trim() -ne 'true') {
        throw "Not a Git repository: $repository"
    }
    $repository = (Invoke-GitCapture -Repository $repository -Arguments @(
            'rev-parse', '--show-toplevel'
        )).StdOut.Trim()
    $ledger = Join-Path $repository 'docs/project-statistics.md'
    $configFile = Join-Path $repository 'docs/project-statistics.config.json'
    if (-not (Test-Path -LiteralPath $ledger -PathType Leaf)) {
        throw "Statistics ledger not found: $ledger"
    }

    $configuration = Get-Configuration -ConfigFile $configFile
    $snapshot = Get-ArtifactSnapshot -Repository $repository -Configuration $configuration
    $history = @(Get-HistoryEntry -Repository $repository -Configuration $configuration)
    $sourceRevision = $(if ($history.Count -gt 0) {
            ([string]$history[0].Hash).Substring(0, 12)
        } else {
            (Invoke-GitCapture -Repository $repository -Arguments @(
                    'rev-parse', '--short=12', 'HEAD'
                )).StdOut.Trim()
        })
    $generated = ConvertTo-GeneratedBlock -Configuration $configuration -Snapshot $snapshot `
        -History $history -SourceRevision $sourceRevision
    Assert-GeneratedBlock -Block $generated
    $existing = Get-Content -LiteralPath $ledger -Raw -Encoding UTF8
    $updated = Get-UpdatedLedger -Existing $existing -Generated $generated
    Assert-LedgerContract -LedgerText $updated
    $changed = (ConvertTo-NormalizedText $existing) -cne $updated
    $timeZone = [TimeZoneInfo]::FindSystemTimeZoneById([string]$configuration.timeZone)
    $asOf = Get-AsOfDate -TimeZone $timeZone
    $windowStart = (Get-WeekStart -Date $asOf).AddDays(
        -7 * ([int]$configuration.activityWindowWeeks - 1)
    )
    $activeDays = @($history | Where-Object {
            $_.Date -ge $windowStart -and $_.Date -le $asOf
        } | ForEach-Object { $_.Date } | Sort-Object -Unique).Count

    if ($CheckOnly) {
        if ($changed) {
            Write-Status -Status 'DRIFT' -Repository $repository -Ledger $ledger `
                -SourceRevision $sourceRevision -Changed $true `
                -TotalLines $snapshot.TotalLines -ActiveDays $activeDays
            exit 1
        }
        Write-Status -Status 'CURRENT' -Repository $repository -Ledger $ledger `
            -SourceRevision $sourceRevision -Changed $false `
            -TotalLines $snapshot.TotalLines -ActiveDays $activeDays
        exit 0
    }

    if ($WhatIfPreference) {
        if (-not $Json) {
            $generated
        }
        Write-Status -Status 'DRY_RUN' -Repository $repository -Ledger $ledger `
            -SourceRevision $sourceRevision -Changed $changed `
            -TotalLines $snapshot.TotalLines -ActiveDays $activeDays
        exit 0
    }

    if (-not $changed) {
        Write-Status -Status 'CURRENT' -Repository $repository -Ledger $ledger `
            -SourceRevision $sourceRevision -Changed $false `
            -TotalLines $snapshot.TotalLines -ActiveDays $activeDays
        exit 0
    }

    $dirty = (Invoke-GitCapture -Repository $repository -Arguments @(
            'status', '--porcelain=v1', '--untracked-files=all'
        )).StdOut.Trim()
    if ($dirty) {
        throw 'Writing requires a clean working tree. Commit or stash existing changes first.'
    }

    if ($PSCmdlet.ShouldProcess($ledger, 'Render ASCII Statistics Profile 2')) {
        [IO.File]::WriteAllText($ledger, $updated, [Text.UTF8Encoding]::new($false))
    }
    Write-Status -Status 'UPDATED' -Repository $repository -Ledger $ledger `
        -SourceRevision $sourceRevision -Changed $true `
        -TotalLines $snapshot.TotalLines -ActiveDays $activeDays
    exit 0
} catch {
    if ($Json) {
        [ordered]@{
            status = 'ERROR'
            message = $_.Exception.Message
            methodologyVersion = 2
        } | ConvertTo-Json -Compress
    } else {
        Write-Error $_.Exception.Message
    }
    exit 2
}
