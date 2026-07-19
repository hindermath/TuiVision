# hg-stats.ps1 — STATS.md Append-Only Schreiber (FR-007/008) — PowerShell
# Verwendet New-Item Directory als atomares Lock

function Get-HgBar {
    param([int]$Score, [int]$Width = 20)
    $filled = [int]($Score * $Width / 100)
    $empty  = $Width - $filled
    ('#' * $filled) + ('.' * $empty)
}

function Invoke-HgWriteStats {
    param(
        [string]$StatsFile,
        [int]$Score,
        [string[]]$Dirs = @()
    )

    $lockDir = "${StatsFile}.lock"
    $elapsed = 0
    while (-not (New-Item -ItemType Directory -Path $lockDir -ErrorAction SilentlyContinue)) {
        Start-Sleep -Seconds 1; $elapsed++
        if ($elapsed -ge 5) {
            Write-Warning "stats file locked — try again later"
            return
        }
    }

    try {
        if (-not (Test-Path $StatsFile)) {
            @("# STATS.md", "", "## Überblick / Overview", "", "Compliance-Historie -- Compliance History", "", "## Verwendung / Usage", "", "Jeder ``check-homogeneity.sh``-Aufruf fuegt hier einen Eintrag hinzu.", "", "Each ``check-homogeneity.sh`` run appends an entry here.", "") |
                Set-Content $StatsFile -Encoding UTF8
        }

        $ts = Get-Date -Format 'yyyy-MM-dd HH:mm'
        $suffix = ''
        $collision = 2
        while (Select-String -Path $StatsFile -Pattern "^## Run ${ts}${suffix}" -Quiet) {
            $suffix = ":${collision}"; $collision++
        }

        $lines = @(
            "",
            "## Run $ts$suffix",
            "",
            "Overall Score: **$Score %**",
            "",
            "| Level | Directory | Score % |",
            "|-------|-----------|---------|"
        )
        $i = 0
        foreach ($dir in $Dirs) {
            $shortDir = $dir -replace [regex]::Escape($(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE })), '~'
            $lines += "| $i | ``$shortDir`` | -- |"
            $i++
        }
        $bar = Get-HgBar -Score $Score
        $lines += @("", "ASCII Bar: [$bar] $Score %", "")

        $lines | Add-Content $StatsFile -Encoding UTF8

        # Archive if >= 500 entries
        $entryCount = (Select-String -Path $StatsFile -Pattern '^## Run ').Count
        if ($entryCount -ge 500) {
            $year = (Get-Date).Year
            $archiveFile = $StatsFile -replace 'STATS\.md', "STATS-archive-$year.md"
            Copy-Item $StatsFile $archiveFile
            $content = @(Get-Content -LiteralPath $StatsFile -Encoding UTF8)
            $entries = @(Select-String -LiteralPath $StatsFile -Pattern '^## Run ')
            $firstEntryIndex = $entries[0].LineNumber - 1
            $keepEntryIndex = $entries[$entries.Count - 50].LineNumber - 1
            $kept = [Collections.Generic.List[string]]::new()
            if ($firstEntryIndex -gt 0) {
                $kept.AddRange([string[]]$content[0..($firstEntryIndex - 1)])
            }
            $kept.AddRange([string[]]$content[$keepEntryIndex..($content.Count - 1)])
            Set-Content -LiteralPath $StatsFile -Value $kept -Encoding UTF8
            Write-Warning "STATS.md archived to: $archiveFile"
        }
    } finally {
        Remove-Item -Path $lockDir -Force -ErrorAction SilentlyContinue
    }
}
