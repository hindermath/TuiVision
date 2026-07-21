# hg-scan.ps1 — 3-Ebenen-Traversal Engine (FR-001) — PowerShell
# Gibt pro qualifizierendem Verzeichnis PSObject mit Level, Path, Type aus

function Invoke-HgScan {
    param(
        [string]$TargetDir = $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE })
    )

    $TargetDir = $TargetDir.TrimEnd([IO.Path]::DirectorySeparatorChar)
    $homeDir = if ($env:HOME) { $env:HOME.TrimEnd([IO.Path]::DirectorySeparatorChar) } else { $env:USERPROFILE.TrimEnd([IO.Path]::DirectorySeparatorChar) }
    $skipGitignored = $false
    git -C $TargetDir rev-parse --is-inside-work-tree 2>$null | Out-Null
    if (($LASTEXITCODE -eq 0) -and ($TargetDir -ne $homeDir)) {
        $skipGitignored = $true
    }

    function Test-HgIgnoredChild {
        param([string]$ChildPath)
        if (-not $skipGitignored) { return $false }
        $rel = [IO.Path]::GetRelativePath($TargetDir, $ChildPath)
        git -C $TargetDir check-ignore -q -- $rel 2>$null
        return ($LASTEXITCODE -eq 0)
    }

    # Level 0
    [PSCustomObject]@{ Level = 0; Path = $TargetDir; Type = 'home' }

    # Level 1 — Workspaces
    $workspaces = Get-ChildItem -Path $TargetDir -Directory -ErrorAction SilentlyContinue | Sort-Object Name
    foreach ($ws in $workspaces) {
        if (-not (Test-Path (Join-Path $ws.FullName '.git'))) { continue }
        if (Test-HgIgnoredChild $ws.FullName) { continue }
        [PSCustomObject]@{ Level = 1; Path = $ws.FullName; Type = 'workspace' }

        # Level 2 — Projects
        $projects = Get-ChildItem -Path $ws.FullName -Directory -ErrorAction SilentlyContinue | Sort-Object Name
        foreach ($proj in $projects) {
            if (-not (Test-Path (Join-Path $proj.FullName '.git'))) { continue }
            if (Test-HgIgnoredChild $proj.FullName) { continue }
            [PSCustomObject]@{ Level = 2; Path = $proj.FullName; Type = 'project' }
        }
    }
}
