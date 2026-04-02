#Requires -Version 7
# init-stats.ps1 — STATS.md Baseline-Generator v1.0 (PowerShell)
# FR-REV-B04; Contract: init-stats-cli.md
#
# Usage: pwsh scripts/init-stats.ps1 [-WorkspaceName <string>]
# Exit codes: 0=success, 1=error
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

[CmdletBinding()]
param(
    [string]$WorkspaceName = ''
)

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$CheckScript = Join-Path $ScriptDir 'check-homogeneity.ps1'

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
    $bar     = ('█' * $filled) + ('░' * $empty)
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
    if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir -Force | Out-Null }

    if (Test-Path $StatsFile) {
        Add-Content -Path $StatsFile -Value $entry
    } else {
        $StatsHeader | Set-Content -Path $StatsFile
        Add-Content -Path $StatsFile -Value $entry
    }
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

# ─── Main ────────────────────────────────────────────────────────────────────

$HomeDir = $env:HOME

if ($WorkspaceName) {
    # Scoped mode: only the specified Level-1 workspace and its Level-2 projects
    $wsDir = Join-Path $HomeDir $WorkspaceName
    if (-not (Test-Path $wsDir)) {
        Write-Error "ERROR: Workspace nicht gefunden: $wsDir"
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
