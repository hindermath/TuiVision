# Extracted from home-baseline ade796af59bdd2248a264d0dc0d3f45728997838.
# MIT; copyright 2026 home-baseline contributors. See LICENSE and docs/provenance.md.
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
