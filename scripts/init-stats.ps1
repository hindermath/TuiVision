#Requires -Version 7
# init-stats.ps1 — STATS.md Baseline-Generator v1.0 (PowerShell)
# FR-REV-B04; Contract: init-stats-cli.md
#
# Usage: pwsh scripts/init-stats.ps1 [-WorkspaceName <name-or-path>]
# Exit codes: 0=success, 1=error
[CmdletBinding()]
param(
    [string]$WorkspaceName = ''
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$CheckScript = Join-Path $ScriptDir 'check-homogeneity.ps1'
Import-Module (Join-Path $ScriptDir 'lib/managed-file-hardening.psm1') -Force

# ─── Prerequisites ───────────────────────────────────────────────────────────

if (-not (Test-Path $CheckScript)) {
    Write-Error "ERROR: check-homogeneity.ps1 nicht gefunden — zuerst FR-REV-B01 implementieren"
    exit 1
}

# ─── ASCII Bar Helper ─────────────────────────────────────────────────────────

function Get-AsciiBar {
    param([int]$Score)
    $rounded = [int]([Math]::Round($Score / 5.0) * 5)
    $filled  = [Math]::Min([int]($rounded / 5), 20)
    $empty   = 20 - $filled
    $bar     = ('#' * $filled) + ('.' * $empty)
    return "$bar $Score%"
}

# ─── STATS.md Header ─────────────────────────────────────────────────────────

$StatsHeader = @"
# Statistiken / Statistics

| Datum / Date | Compliance-Score | Fortschritt / Progress |
|---|---|---|
"@

# ─── Write Stats Entry ────────────────────────────────────────────────────────

function Write-StatsEntry {
    param([string]$StatsFile, [int]$Score)
    $timestamp = Get-Date -Format 'yyyy-MM-dd HH:mm'
    $bar       = Get-AsciiBar -Score $Score
    $entry     = "| $timestamp | $Score% | $bar |"

    $dir = Split-Path $StatsFile -Parent
    $relative = Split-Path $StatsFile -Leaf
    $existing = if (Test-Path -LiteralPath $StatsFile) {
        (Read-HBManagedText -Root $dir -RelativePath $relative).TrimEnd("`r", "`n") + "`n"
    } else {
        $StatsHeader.TrimEnd("`r", "`n") + "`n"
    }
    Write-HBManagedTextAtomic -Root $dir -RelativePath $relative -Content ($existing + $entry + "`n")
    Write-Host "✓ STATS.md updated at $StatsFile"
}

# ─── Get Score for a Directory ────────────────────────────────────────────────

function Get-ScoreFor {
    param([string]$TargetDir)
    try {
        $json = & pwsh $CheckScript -TargetDir $TargetDir -Json -DryRun 2>$null | ConvertFrom-Json -ErrorAction SilentlyContinue
        return [int]($json.score ?? 0)
    } catch {
        return 0
    }
}

function Assert-NoLinkComponents {
    param([Parameter(Mandatory)][string]$Path)

    $absolute = [IO.Path]::GetFullPath($Path)
    $root = [IO.Path]::GetPathRoot($absolute)
    $current = $root
    $relative = [IO.Path]::GetRelativePath($root, $absolute)
    foreach ($component in @($relative -split '[\\/]' | Where-Object { $_ -and $_ -ne '.' })) {
        if ($component -eq '..') { throw "Target path contains traversal: $Path" }
        $current = Join-Path $current $component
        $item = Get-Item -LiteralPath $current -Force -ErrorAction SilentlyContinue
        if ($item -and ($item.LinkType -or
            ($item.Attributes -band [IO.FileAttributes]::ReparsePoint))) {
            if ($IsWindows) { throw "Target path contains a link or reparse point: $Path" }
            $owner = @(& stat -f '%u' $current 2>$null)
            if ($LASTEXITCODE -ne 0) { $owner = @(& stat -c '%u' $current 2>$null) }
            if ($LASTEXITCODE -ne 0 -or @($owner).Count -ne 1 -or [string]$owner[0] -cne '0') {
                throw "Target path contains an untrusted symbolic link: $Path"
            }
        }
    }
}

function Resolve-TargetDir {
    param([string]$InputPath)

    if (Test-Path -LiteralPath $InputPath -PathType Container) {
        Assert-NoLinkComponents -Path $InputPath
        $item = Get-Item -LiteralPath $InputPath -Force
        if ($item.LinkType -or ($item.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
            throw "Target directory must not be a link or reparse point: $InputPath"
        }
        return $item.FullName
    }

    $homeCandidate = Join-Path $HomeDir $InputPath
    if (Test-Path -LiteralPath $homeCandidate -PathType Container) {
        Assert-NoLinkComponents -Path $homeCandidate
        $item = Get-Item -LiteralPath $homeCandidate -Force
        if ($item.LinkType -or ($item.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
            throw "Target directory must not be a link or reparse point: $homeCandidate"
        }
        return $item.FullName
    }

    return $null
}

# ─── Main ────────────────────────────────────────────────────────────────────

$HomeDir = $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE })

if ($WorkspaceName) {
    # Scoped mode: support either a workspace name or an absolute path
    $wsDir = Resolve-TargetDir -InputPath $WorkspaceName
    if (-not $wsDir) {
        Write-Error "ERROR: Workspace/Pfad nicht gefunden: $WorkspaceName"
        exit 1
    }

    $wsScore = Get-ScoreFor -TargetDir $wsDir
    Write-StatsEntry -StatsFile (Join-Path $wsDir 'STATS.md') -Score $wsScore

    # Level-2 projects within workspace
    Get-ChildItem -Path $wsDir -Directory -ErrorAction SilentlyContinue | Sort-Object Name | ForEach-Object {
        $projDir = $_.FullName
        if (Test-Path (Join-Path $projDir '.git')) {
            $projScore = Get-ScoreFor -TargetDir $projDir
            Write-StatsEntry -StatsFile (Join-Path $projDir 'STATS.md') -Score $projScore
        }
    }

} else {
    # Full mode: Level 0, all Level-1 workspaces, all Level-2 projects

    # Level 0 — root (L2-Fix: use Join-Path $HOME instead of ~/STATS.md literal)
    $l0Score = Get-ScoreFor -TargetDir $HomeDir
    Write-StatsEntry -StatsFile (Join-Path $HomeDir 'STATS.md') -Score $l0Score

    # Level 1 + Level 2
    Get-ChildItem -Path $HomeDir -Directory -ErrorAction SilentlyContinue | Sort-Object Name | ForEach-Object {
        $wsDir = $_.FullName
        if (-not (Test-Path (Join-Path $wsDir '.git'))) { return }

        $wsScore = Get-ScoreFor -TargetDir $wsDir
        Write-StatsEntry -StatsFile (Join-Path $wsDir 'STATS.md') -Score $wsScore

        # Level-2 projects
        Get-ChildItem -Path $wsDir -Directory -ErrorAction SilentlyContinue | Sort-Object Name | ForEach-Object {
            $projDir = $_.FullName
            if (Test-Path (Join-Path $projDir '.git')) {
                $projScore = Get-ScoreFor -TargetDir $projDir
                Write-StatsEntry -StatsFile (Join-Path $projDir 'STATS.md') -Score $projScore
            }
        }
    }
}

exit 0
