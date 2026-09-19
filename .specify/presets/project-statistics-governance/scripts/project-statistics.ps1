#Requires -Version 7
<#
.SYNOPSIS
Prueft oder aktualisiert reproduzierbare Projekttransparenz.
Checks or updates reproducible project transparency.
.DESCRIPTION
Liest feste Git-Objekte ohne Netzwerk. Status und Vorschau schreiben nichts.
Reads immutable Git objects offline. Status and preview never write.
.PARAMETER Action
Init legt neue Dateien an; Status prueft; Update rendert autorisierte Ausgaben.
Init creates new files; Status checks; Update renders authorized outputs.
.PARAMETER Repo
Git-Repository / Git repository. Default: current directory.
.PARAMETER Config
Repositoryrelativer Konfigurationspfad / repository-relative configuration path.
.PARAMETER Revision
Quellrevision fuer Update / source revision for Update. Default: HEAD.
.PARAMETER AsOf
Optionaler Stichtag YYYY-MM-DD / optional reporting cutoff YYYY-MM-DD.
.PARAMETER Json
Maschinenlesbare Ausgabe / machine-readable output.
.EXAMPLE
bash scripts/project-statistics.sh status --repo . --json
.EXAMPLE
pwsh -NoProfile -File scripts/project-statistics.ps1 -Action Init -WhatIf
.EXAMPLE
pwsh -NoProfile -File scripts/project-statistics.ps1 -Action Update -WhatIf
#>
[CmdletBinding(SupportsShouldProcess, ConfirmImpact='Low')]
param(
    [ValidateSet('Init','Status','Update')][string]$Action='Status',
    [string]$Repo='.',
    [string]$Config='docs/project-statistics/config.json',
    [string]$Revision='HEAD',
    [string]$AsOf='',
    [switch]$Json
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$script:Invariant=[Globalization.CultureInfo]::InvariantCulture
$script:Begin='<!-- project-transparency:begin -->'
$script:End='<!-- project-transparency:end -->'
. (Join-Path $PSScriptRoot 'lib/profile-helpers.ps1')

function Invoke-StatisticsGit {
    param([string[]]$Arguments,[switch]$AllowMissing)
    $info=[Diagnostics.ProcessStartInfo]::new('git')
    $info.WorkingDirectory=$script:Root
    $info.UseShellExecute=$false
    $info.RedirectStandardOutput=$true
    $info.RedirectStandardError=$true
    $info.StandardOutputEncoding=[Text.Encoding]::UTF8
    $info.Environment['GIT_NO_LAZY_FETCH']='1'
    $info.Environment['GIT_OPTIONAL_LOCKS']='0'
    $info.Environment['GIT_TERMINAL_PROMPT']='0'
    foreach ($arg in @('--no-replace-objects')+$Arguments) { $info.ArgumentList.Add($arg) }
    $process=[Diagnostics.Process]::Start($info)
    try {
        $stderr=$process.StandardError.ReadToEndAsync()
        $stdout=$process.StandardOutput.ReadToEnd()
        $process.WaitForExit()
        $null=$stderr.GetAwaiter().GetResult()
        if ($AllowMissing -and $process.ExitCode -eq 1) { return '' }
        if ($process.ExitCode -ne 0) { throw 'Git-Abfrage fehlgeschlagen / Git query failed; verify repository and revision.' }
        return $stdout
    } finally { $process.Dispose() }
}

function Get-StatisticsHash {
    param([AllowEmptyString()][string]$Text)
    $algorithm=[Security.Cryptography.SHA256]::Create()
    try { ([BitConverter]::ToString($algorithm.ComputeHash([Text.Encoding]::UTF8.GetBytes($Text)))).Replace('-','').ToLowerInvariant() }
    finally { $algorithm.Dispose() }
}

function ConvertTo-StatisticsJson {
    param($Value)
    ($Value | ConvertTo-Json -Depth 40 -Compress) + "`n"
}

function ConvertTo-StatisticsText {
    param([AllowEmptyString()][string]$Text)
    ($Text.TrimStart([char]0xfeff) -replace "`r`n","`n" -replace "`r","`n")
}

function Assert-StatisticsPath {
    param([string]$Path)
    if ([string]::IsNullOrWhiteSpace($Path) -or [IO.Path]::IsPathRooted($Path) -or
        $Path -match '[\x00-\x1f\x7f\\:*?\[\]]' -or $Path.Split('/') -contains '..' -or
        $Path.Split('/') -contains '.git') { throw 'Unsicherer Pfad / Unsafe path.' }
    $cursor=$script:Root
    foreach ($part in $Path.Split('/')) {
        $cursor=Join-Path $cursor $part
        $item=Get-Item -LiteralPath $cursor -Force -ErrorAction SilentlyContinue
        if ($null -ne $item) {
            if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
                throw 'Symlink-Ausgabepfad gesperrt / Symlink output path blocked.'
            }
        }
    }
}

function Read-StatisticsConfig {
    param([string]$Text)
    $schema=Join-Path $PSScriptRoot 'config/project-statistics.schema.json'
    $normalized=ConvertTo-StatisticsText $Text
    if (-not (Test-Json -Json $normalized -SchemaFile $schema -ErrorAction Stop)) { throw 'Invalid configuration.' }
    $value=$normalized | ConvertFrom-Json -AsHashtable
    $null=[TimeZoneInfo]::FindSystemTimeZoneById($value.timeZone)
    foreach ($pattern in @($value.excludedPaths)+@($value.categoryOverrides | ForEach-Object { $_.pattern })) {
        if ([IO.Path]::IsPathRooted($pattern) -or $pattern.Replace('\','/').Split('/') -contains '..') {
            throw 'Unsicheres Ausschlussmuster / Unsafe exclusion pattern.'
        }
    }
    if (@($value.phases | Group-Object slot | Where-Object Count -gt 1).Count -or
        @($value.phases | Group-Object id | Where-Object Count -gt 1).Count) { throw 'Duplicate phase slot or ID.' }
    return $value
}

function Test-StatisticsExcluded {
    param([string]$Path, $Configuration)
    if ($Path -eq $Config -or $Path.StartsWith($script:OutputDirectory+'/',[StringComparison]::Ordinal)) { return $true }
    Test-ExcludedPath -RelativePath $Path -Configuration $Configuration
}

function Get-StatisticsMeasurement {
    param([string]$Commit, $Configuration, [string]$Cutoff)
    $tree=Invoke-StatisticsGit @('ls-tree','-r','-z','--full-tree',$Commit)
    $objects=[Collections.Generic.List[object]]::new()
    $omitted=[ordered]@{binary=0; symlinks=0; submodules=0; excluded=0}
    foreach ($entry in $tree.Split([char]0,[StringSplitOptions]::RemoveEmptyEntries)) {
        if ($entry -notmatch '^([0-9]+) (blob|commit) ([a-f0-9]+)\t(.+)$') { throw 'Unsupported tree entry.' }
        $mode=$Matches[1]; $id=$Matches[3]; $path=$Matches[4]
        if ($path -match '[\x00-\x1f\x7f]') { throw 'Control characters in tracked paths are unsupported.' }
        # Own outputs are outside the measurement, including coverage counts.
        # Otherwise the first snapshot would create a second measurement change.
        if ($path -eq $Config -or $path.StartsWith($script:OutputDirectory+'/',[StringComparison]::Ordinal)) { continue }
        if (Test-StatisticsExcluded $path $Configuration) { $omitted.excluded++; continue }
        if ($mode -eq '120000') { $omitted.symlinks++; continue }
        if ($mode -eq '160000') { $omitted.submodules++; continue }
        $objects.Add([ordered]@{path=$path; id=$id})
    }
    $ids=[string[]]@($objects | ForEach-Object { $_.id })
    $counts=[ProjectTransparency.GitBlobCounter]::Count($script:Root,$ids)
    $categories=[ordered]@{}
    foreach ($category in @('Production','Tests','Documentation','Scripts','Configuration','DataMedia','Other')) {
        $categories[$category]=[ordered]@{files=0L; lines=0L}
    }
    for ($i=0; $i -lt $objects.Count; $i++) {
        if ($null -eq $counts[$i]) { $omitted.binary++; continue }
        $category=Get-ArtifactCategory $objects[$i].path $Configuration
        $categories[$category].files++
        $categories[$category].lines += $counts[$i]
    }
    $log=Invoke-StatisticsGit @('log',$Commit,'--no-merges','--format=%x1e%H%x09%cI','--numstat','-z','--find-renames=50%','--no-ext-diff','--no-textconv','--','.')
    $history=[Collections.Generic.List[object]]::new()
    $zone=[TimeZoneInfo]::FindSystemTimeZoneById($Configuration.timeZone)
    foreach ($record in $log.Split([char]30,[StringSplitOptions]::RemoveEmptyEntries)) {
        $parts=$record.Split([char]0)
        $header=$parts[0].Trim().Split("`t")
        if ($header.Count -ne 2) { throw 'Invalid history header.' }
        $timestamp=[DateTimeOffset]::Parse($header[1],$script:Invariant)
        $added=0L; $removed=0L
        for ($i=1; $i -lt $parts.Count; $i++) {
            $line=$parts[$i].TrimStart([char]10)
            if (-not $line) { continue }
            if ($line -notmatch '^(\d+|-)\t(\d+|-)\t(.*)$') { throw 'Unsupported history path.' }
            $a=$Matches[1]; $r=$Matches[2]; $path=$Matches[3]; $old=$path
            if (-not $path) { $i++; $old=$parts[$i]; $i++; $path=$parts[$i] }
            if ($path -match '[\x00-\x1f\x7f]' -or $old -match '[\x00-\x1f\x7f]') { throw 'Unsupported history path.' }
            if ($a -eq '-' -or $r -eq '-' -or (Test-StatisticsExcluded $path $Configuration) -or (Test-StatisticsExcluded $old $Configuration)) { continue }
            $added += [long]$a; $removed += [long]$r
        }
        if (($added+$removed) -gt 0) {
            $history.Add([ordered]@{commit=$header[0]; date=[TimeZoneInfo]::ConvertTime($timestamp,$zone).ToString('yyyy-MM-dd'); added=$added; removed=$removed})
        }
    }
    if (-not $Cutoff) {
        $Cutoff=if ($history.Count) { $history[0].date } else {
            $stamp=(Invoke-StatisticsGit @('show','-s','--format=%cI',$Commit)).Trim()
            [TimeZoneInfo]::ConvertTime([DateTimeOffset]::Parse($stamp),$zone).ToString('yyyy-MM-dd')
        }
    }
    $end=[datetime]::ParseExact($Cutoff,'yyyy-MM-dd',$script:Invariant)
    $start=$end.AddDays(-[int]$end.DayOfWeek-7*($Configuration.activityWindowWeeks-1))
    $daily=[ordered]@{}
    foreach ($entry in $history) {
        $date=[datetime]::ParseExact($entry.date,'yyyy-MM-dd',$script:Invariant)
        if ($date -lt $start -or $date -gt $end) { continue }
        if (-not $daily.Contains($entry.date)) { $daily[$entry.date]=[ordered]@{added=0L; removed=0L} }
        $daily[$entry.date].added += $entry.added; $daily[$entry.date].removed += $entry.removed
    }
    $orderedDaily=[ordered]@{}
    foreach ($key in @($daily.Keys | Sort-Object)) { $orderedDaily[$key]=$daily[$key] }
    # Coverage is observable output too: an added excluded path or submodule
    # must invalidate freshness even when counted text remains unchanged.
    $identity=[ordered]@{objects=@($objects.ToArray()); history=@($history.ToArray()); omitted=$omitted}
    [ordered]@{
        sourceRevision=$Commit; asOf=$Cutoff; timeZone=$Configuration.timeZone
        windowStart=$start.ToString('yyyy-MM-dd'); windowWeeks=$Configuration.activityWindowWeeks
        inputsSha256=Get-StatisticsHash (ConvertTo-StatisticsJson $identity)
        totalTextFiles=[long](($categories.Values | ForEach-Object { $_.files } | Measure-Object -Sum).Sum)
        totalTextLines=[long](($categories.Values | ForEach-Object { $_.lines } | Measure-Object -Sum).Sum)
        activeDays=$orderedDaily.Count; categories=$categories; daily=$orderedDaily; omitted=$omitted
    }
}

function Get-StatisticsReport {
    param($Measurement, $Configuration)
    $lines=[Collections.Generic.List[string]]::new()
    $lines.Add($script:Begin)
    $lines.Add('## Projekttransparenz / Project transparency')
    $lines.Add('')
    $lines.Add('Git-gebundener Bestand und Aktivitaet.')
    $lines.Add('Keine Messung von Qualitaet, Lernleistung oder KI-Zeitersparnis.')
    $lines.Add('Git-bound inventory and activity; not a measure of quality, learning performance or AI time savings.')
    $lines.Add('')
    $lines.Add('| Kennzahl / Metric | Wert / Value |')
    $lines.Add('| --- | --- |')
    $lines.Add("| Textdateien / Text files | $($Measurement.totalTextFiles) |")
    $lines.Add("| Textzeilen / Text lines | $($Measurement.totalTextLines) |")
    $lines.Add("| Aktivtage / Active days | $($Measurement.activeDays) |")
    $lines.Add("| Stichtag / As of | $($Measurement.asOf) |")
    $lines.Add("| Fensterbeginn / Window start | $($Measurement.windowStart) |")
    $lines.Add("| Zeitzone / Time zone | $($Measurement.timeZone) |")
    $lines.Add('')
    $lines.Add('Quellrevision / Source revision:')
    $lines.Add($Measurement.sourceRevision)
    $lines.Add('')
    $lines.Add('### Artefakte / Artifacts')
    $lines.Add('')
    $lines.Add('| Kategorie / Category | Dateien / Files | Zeilen / Lines |')
    $lines.Add('| --- | ---: | ---: |')
    foreach ($name in $Measurement.categories.Keys) {
        $item=$Measurement.categories[$name]
        $lines.Add("| $name | $($item.files) | $($item.lines) |")
    }
    $lines.Add('')
    $lines.Add('### Aktivitaet / Activity')
    $lines.Add('')
    $lines.Add('DE: Zellen zeigen Bruttoaenderungen je Tag, keine Stunden. Exakte Tageswerte folgen.')
    $lines.Add('EN: Cells show gross changes per day, not hours. Exact daily values follow.')
    $lines.Add('0=0; 1=1..79; 2=80..399; 3=400..1599; 4=1600+; -=zukuenftig/future')
    $daily=@{}
    foreach ($key in $Measurement.daily.Keys) { $daily[$key]=$Measurement.daily[$key].added+$Measurement.daily[$key].removed }
    $weeks=@(for ($i=0;$i -lt $Measurement.windowWeeks;$i++) { @{Start=([datetime]$Measurement.windowStart).AddDays(7*$i)} })
    for ($i=0;$i -lt $weeks.Count;$i+=26) {
        $last=[Math]::Min($i+25,$weeks.Count-1)
        $lines.Add(''); $lines.Add('```text')
        $lines.Add((Format-HeatmapBlock -Weeks $weeks[$i..$last] -Daily $daily -AsOfDate ([datetime]$Measurement.asOf) -FirstIndex $i))
        $lines.Add('```')
    }
    $lines.Add(''); $lines.Add('| Datum / Date | Hinzu / Added | Entfernt / Removed |')
    $lines.Add('| --- | ---: | ---: |')
    foreach ($date in $Measurement.daily.Keys) { $lines.Add("| $date | $($Measurement.daily[$date].added) | $($Measurement.daily[$date].removed) |") }
    if (-not $Measurement.daily.Count) { $lines.Add('| Keine Aktivitaet / No activity | 0 | 0 |') }
    $lines.Add(''); $lines.Add('### Abdeckung / Coverage'); $lines.Add('')
    foreach ($kind in $Measurement.omitted.Keys) { $lines.Add("- $kind : $($Measurement.omitted[$kind])") }
    $lines.Add(''); $lines.Add('DE: Binaerdateien, Symlinks und Submodule werden nicht inhaltlich ausgewertet.')
    $lines.Add('EN: Binary files, symlinks and submodules are not analyzed as text inventory.')
    if ($Configuration.phases.Count) {
        $lines.Add(''); $lines.Add('### Manuell gepflegte Phasen / Manually maintained phases'); $lines.Add('')
        $lines.Add('Keine automatische Abschluss-Evidence / Not automatic completion evidence.')
        foreach ($phase in @($Configuration.phases | Sort-Object slot)) {
            $lines.Add(''); $lines.Add("Slot $($phase.slot): $($phase.id)")
            $lines.Add("DE: $(ConvertTo-StatisticsLabel $phase.labelDe)"); $lines.Add("EN: $(ConvertTo-StatisticsLabel $phase.labelEn)")
            $lines.Add("Textzeilen / Text lines: $($phase.netLines)")
        }
    }
    if ($Configuration.references.enabled) {
        $lines.Add(''); $lines.Add('### Optionale Modellrechnung / Optional reference estimate'); $lines.Add('')
        $lines.Add('DE: Textbestand / (Git-Aktivtage * Referenzzeilen pro Tag); keine gemessene Zeitersparnis.')
        $lines.Add('EN: Text inventory / (Git-active days * reference lines per day); not measured time savings.')
        foreach ($scenario in $Configuration.references.scenarios) {
            $factor=if ($Measurement.activeDays -gt 0) { Format-Decimal ($Measurement.totalTextLines/($Measurement.activeDays*$scenario.linesPerDay)) } else { 'nicht berechenbar / not calculable' }
            $lines.Add("$(ConvertTo-StatisticsLabel $scenario.name): $factor; reference=$($scenario.linesPerDay) lines/day")
        }
    }
    $lines.Add(''); $lines.Add($script:End)
    $text=($lines -join "`n")+"`n"
    foreach ($line in $text.Split("`n")) { if ($line.Length -gt 100) { throw 'Report exceeds 100 columns; shorten configuration labels.' } }
    return $text
}

function Merge-StatisticsReport {
    param([string]$Existing,[string]$Generated)
    $existing=ConvertTo-StatisticsText $Existing
    foreach ($marker in @($script:Begin,$script:End)) {
        if ([regex]::Matches($existing,[regex]::Escape($marker)).Count -ne 1) { throw 'Missing or duplicate report marker.' }
    }
    $first=$existing.IndexOf($script:Begin,[StringComparison]::Ordinal)
    $last=$existing.IndexOf($script:End,[StringComparison]::Ordinal)
    if ($last -le $first) { throw 'Invalid report marker order.' }
    $existing.Substring(0,$first)+$Generated.TrimEnd("`n")+$existing.Substring($last+$script:End.Length)
}

function ConvertTo-StatisticsLabel {
    param([string]$Label)
    # Project-authored labels are data, not executable HTML or Markdown images.
    [Net.WebUtility]::HtmlEncode($Label).Replace('\','\\').Replace('[','\[').Replace(']','\]').Replace('`','\`')
}

try {
    $script:Root=(Resolve-Path -LiteralPath $Repo).Path
    $script:Root=(Invoke-StatisticsGit @('rev-parse','--show-toplevel')).Trim()
    if ((Invoke-StatisticsGit @('rev-parse','--is-shallow-repository')).Trim() -ne 'false') { throw 'Unvollstaendige Historie / Shallow history blocked.' }
    $partial=Invoke-StatisticsGit -Arguments @('config','--get-regexp','^(extensions\.partialclone|remote\..*\.promisor)$') -AllowMissing
    if ($partial) { throw 'Partial/promisor clones blocked; no implicit network fetch.' }
    Assert-StatisticsPath $Config
    if ((Split-Path $Config -Leaf) -in @('report.md','snapshot.json')) { throw 'Configuration must not alias an output file.' }
    $script:OutputDirectory=($Config -split '/')[0..(($Config -split '/').Count-2)] -join '/'
    if (-not $Config.Contains('/') -or $script:OutputDirectory -in @('','.')) { throw 'Configuration needs a dedicated subdirectory.' }
    $configPath=Join-Path $script:Root $Config
    $reportPath=Join-Path (Split-Path $configPath) 'report.md'
    $snapshotPath=Join-Path (Split-Path $configPath) 'snapshot.json'
    foreach ($leaf in @('report.md','snapshot.json')) { Assert-StatisticsPath "$script:OutputDirectory/$leaf" }
    if ($Action -eq 'Init') {
        foreach ($path in @($configPath,$reportPath,$snapshotPath)) { if (Test-Path -LiteralPath $path) { throw 'Init refuses existing files.' } }
        if ((Invoke-StatisticsGit @('status','--porcelain=v1','--untracked-files=all')).Trim()) { throw 'Clean worktree required for Init.' }
        if ($PSCmdlet.ShouldProcess($Config,'Initialize project statistics')) {
            $null=New-Item -ItemType Directory -Path (Split-Path $configPath) -Force
            $template=[IO.File]::ReadAllText((Join-Path $PSScriptRoot '../templates/config.json'))
            [IO.File]::WriteAllText($configPath,$template,[Text.UTF8Encoding]::new($false))
            [IO.File]::WriteAllText($reportPath,"# Projektstatistik / Project statistics`n`n$script:Begin`n$script:End`n",[Text.UTF8Encoding]::new($false))
        }
        $result=[ordered]@{status=if($WhatIfPreference){'DRY_RUN'}else{'INITIALIZED'}; changed=(-not $WhatIfPreference)}
        if ($Json) { ConvertTo-StatisticsJson $result } else { $result.status }; exit 0
    }
    if (-not ('ProjectTransparency.GitBlobCounter' -as [type])) { Add-Type -Path (Join-Path $PSScriptRoot 'lib/GitBlobCounter.cs') }
    $configText=ConvertTo-StatisticsText ([IO.File]::ReadAllText($configPath))
    $configuration=Read-StatisticsConfig $configText
    $configHash=Get-StatisticsHash $configText
    $toolMaterial=foreach ($file in @('project-statistics.ps1','lib/profile-helpers.ps1','lib/GitBlobCounter.cs','config/project-statistics.schema.json')) {
        $file+':'+(Get-StatisticsHash (ConvertTo-StatisticsText ([IO.File]::ReadAllText((Join-Path $PSScriptRoot $file)))))
    }
    $toolHash=Get-StatisticsHash ($toolMaterial -join "`n")
    $stored=if(Test-Path -LiteralPath $snapshotPath){[IO.File]::ReadAllText($snapshotPath) | ConvertFrom-Json -AsHashtable}else{$null}
    if ($Action -eq 'Status' -and -not $stored) {
        $result=@{status='DRIFT'; reproducible=$false; current=$false; reason='No snapshot; commit initialization, then Update.'}
        if($Json){ConvertTo-StatisticsJson $result}else{$result.reason}; exit 1
    }
    $commit=(Invoke-StatisticsGit @('rev-parse','--verify','--end-of-options',"$Revision^{commit}")).Trim()
    $head=(Invoke-StatisticsGit @('rev-parse','HEAD')).Trim()
    if ($Action -eq 'Status') {
        if ($stored.schemaVersion -ne 1 -or $stored.presetVersion -ne '0.1.0' -or $stored.methodology -ne 'project-transparency/1' -or
            $stored.measurement.sourceRevision -notmatch '^[a-f0-9]{40}([a-f0-9]{24})?$') { throw 'Invalid snapshot contract.' }
        $originalText=ConvertTo-StatisticsText (Invoke-StatisticsGit @('show',"$($stored.measurement.sourceRevision):$Config"))
        $originalConfig=Read-StatisticsConfig $originalText
        $replayed=Get-StatisticsMeasurement $stored.measurement.sourceRevision $originalConfig $stored.measurement.asOf
        $generated=Get-StatisticsReport $replayed $originalConfig
        $reproducible=(ConvertTo-StatisticsJson $replayed) -ceq (ConvertTo-StatisticsJson $stored.measurement)
        $reproducible=$reproducible -and $stored.configSha256 -ceq (Get-StatisticsHash $originalText) -and $stored.rendererSha256 -ceq $toolHash -and $stored.generatedSha256 -ceq (Get-StatisticsHash $generated)
        $existing=ConvertTo-StatisticsText ([IO.File]::ReadAllText($reportPath))
        $reproducible=$reproducible -and $existing -ceq (Merge-StatisticsReport $existing $generated)
        $latest=Get-StatisticsMeasurement $head $configuration $stored.measurement.asOf
        $current=$latest.inputsSha256 -ceq $stored.measurement.inputsSha256 -and $configHash -ceq $stored.configSha256
        $result=[ordered]@{status=if($reproducible -and $current){'CURRENT'}else{'DRIFT'};reproducible=[bool]$reproducible;current=[bool]$current;sourceRevision=$stored.measurement.sourceRevision;asOf=$stored.measurement.asOf;changed=$false}
        if($Json){ConvertTo-StatisticsJson $result}else{$result | Format-List}
        if($reproducible -and $current){exit 0}else{exit 1}
    }
    if ((Invoke-StatisticsGit @('status','--porcelain=v1','--untracked-files=all')).Trim()) { throw 'Clean worktree required for Update; no automatic stash or commit.' }
    if ((ConvertTo-StatisticsText (Invoke-StatisticsGit @('show',"${commit}:$Config"))) -cne $configText) { throw 'Configuration must match the selected committed source.' }
    $measurement=Get-StatisticsMeasurement $commit $configuration $AsOf
    # Reuse the original source anchor for output-only commits, avoiding self-reference churn.
    if ($stored -and $stored.configSha256 -ceq $configHash -and $stored.rendererSha256 -ceq $toolHash -and
        $stored.measurement.inputsSha256 -ceq $measurement.inputsSha256 -and $stored.measurement.asOf -ceq $measurement.asOf) { $measurement.sourceRevision=$stored.measurement.sourceRevision }
    $generated=Get-StatisticsReport $measurement $configuration
    $existing=ConvertTo-StatisticsText ([IO.File]::ReadAllText($reportPath))
    $updated=Merge-StatisticsReport $existing $generated
    $payload=[ordered]@{schemaVersion=1;presetVersion='0.1.0';methodology='project-transparency/1';configSha256=$configHash;rendererSha256=$toolHash;generatedSha256=Get-StatisticsHash $generated;measurement=$measurement}
    $snapshot=ConvertTo-StatisticsJson $payload
    $changed=$existing -cne $updated -or -not (Test-Path $snapshotPath) -or (ConvertTo-StatisticsText ([IO.File]::ReadAllText($snapshotPath))) -cne $snapshot
    if ($changed -and $PSCmdlet.ShouldProcess($script:OutputDirectory,'Update report and snapshot')) {
        [IO.File]::WriteAllText($reportPath,$updated,[Text.UTF8Encoding]::new($false))
        [IO.File]::WriteAllText($snapshotPath,$snapshot,[Text.UTF8Encoding]::new($false))
    }
    $result=[ordered]@{status=if($WhatIfPreference){'DRY_RUN'}elseif($changed){'UPDATED'}else{'CURRENT'};changed=($changed -and -not $WhatIfPreference);sourceRevision=$measurement.sourceRevision;asOf=$measurement.asOf;totalTextLines=$measurement.totalTextLines;activeDays=$measurement.activeDays}
    if($Json){ConvertTo-StatisticsJson $result}else{$result | Format-List}
    exit 0
} catch {
    $result=[ordered]@{status='ERROR';changed=$false;message=$_.Exception.Message}
    if($Json){ConvertTo-StatisticsJson $result}else{Write-Output "ERROR: $($result.message)"}
    exit 2
}
