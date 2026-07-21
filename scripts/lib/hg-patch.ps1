# hg-patch.ps1 — memory-patch.md Generator (FR-020/021) — PowerShell

function Invoke-HgGeneratePatch {
    param(
        [string]$StatsFile,
        [string[]]$Issues = @()
    )

    $currentScore = 0
    $prevScore    = 0

    if (Test-Path $StatsFile) {
        $scoreLines = Select-String -Path $StatsFile -Pattern 'Overall Score: \*\*(\d+)'
        if ($scoreLines.Count -ge 1) {
            $currentScore = [int]$scoreLines[-1].Matches.Groups[1].Value
        }
        if ($scoreLines.Count -ge 2) {
            $prevScore = [int]$scoreLines[-2].Matches.Groups[1].Value
        }
    }

    $triggered = $false
    $triggerReason = ''

    # Trigger 1: new repo
    if ($prevScore -eq 0 -and $currentScore -gt 0) {
        $triggered = $true; $triggerReason = 'new-repo-detected'
    }

    # Trigger 2: score delta >= 10
    $delta = [Math]::Abs($currentScore - $prevScore)
    if ($prevScore -gt 0 -and $delta -ge 10) {
        $triggered = $true
        $triggerReason = ($triggerReason + ",score-delta-$delta").TrimStart(',')
    }

    if (-not $triggered) { return }

    $specsDir = Join-Path $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }) 'specs'
    $activeFeature = Get-ChildItem -Path $specsDir -Directory -Filter '001-*' -ErrorAction SilentlyContinue | Select-Object -First 1
    $patchFile = if ($activeFeature) { Join-Path $activeFeature.FullName 'memory-patch.md' } else { Join-Path $specsDir 'memory-patch.md' }

    $lines = @(
        "# memory-patch.md",
        "",
        "**Generated**: $(Get-Date -Format 'yyyy-MM-dd HH:mm')",
        "**Trigger**: $triggerReason",
        "**Score**: $currentScore % (prev: $prevScore %)",
        "",
        "---",
        ""
    )

    $entryCount = 0
    foreach ($issue in $Issues) {
        $parts     = $issue -split ':', 2
        $filepath  = $parts[0]
        $msg       = $parts[1]

        $routing = switch -Regex ($filepath) {
            'CLAUDE\.md|AGENTS\.md|GEMINI\.md|copilot' { 'agent_file'; break }
            'README\.md'                                { 'readme';     break }
            'constitution'                              { 'constitution'; break }
            default                                     { 'agent_file' }
        }

        $entryCount++
        $lines += @(
            "## MemoryPatchEntry $entryCount",
            "",
            "- **File**: ``$filepath``",
            "- **Issue**: $msg",
            "- **Routing**: $routing",
            "- **Action**: Review and fix the reported issue",
            "",
            "### Target: $filepath",
            "",
            "+++ [Review needed: $msg]",
            "",
            "---",
            ""
        )
    }

    $lines += "**Total entries**: $entryCount"
    $lines | Set-Content $patchFile -Encoding UTF8

    if ($entryCount -gt 0) {
        Write-Host "memory-patch.md: $patchFile ($entryCount entries)"
    }
}
