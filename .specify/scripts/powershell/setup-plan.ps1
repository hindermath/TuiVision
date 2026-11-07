#!/usr/bin/env pwsh
# Setup implementation plan for a feature

[CmdletBinding()]
param(
    [switch]$Json,
    [switch]$Help
)

$ErrorActionPreference = 'Stop'

# Show help if requested
if ($Help) {
    Write-Output "Usage: ./setup-plan.ps1 [-Json] [-Help]"
    Write-Output "  -Json     Output results in JSON format"
    Write-Output "  -Help     Show this help message"
    exit 0
}

# Load common functions
. "$PSScriptRoot/common.ps1"

# Get all paths and variables from common functions
$paths = Get-FeaturePathsEnv

# Check if we're on a proper feature branch (only for git repos)
if (-not (Test-FeatureBranch -Branch $paths.CURRENT_BRANCH -HasGit $paths.HAS_GIT)) {
    exit 1
}

# Ensure the feature directory exists
New-Item -ItemType Directory -Path $paths.FEATURE_DIR -Force | Out-Null
$featureItem = Get-Item -LiteralPath $paths.FEATURE_DIR -Force
$specsItem = Get-Item -LiteralPath (Join-Path $paths.REPO_ROOT 'specs') -Force
if (($featureItem.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0 -or
    ($specsItem.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) {
    throw 'Feature directory must stay in a link-free specs tree.'
}
if (Test-Path -LiteralPath $paths.IMPL_PLAN) {
    $planItem = Get-Item -LiteralPath $paths.IMPL_PLAN -Force
    if (($planItem.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0 -or $planItem.PSIsContainer) {
        throw 'plan.md must be a regular non-link target.'
    }
}
$planTemp = Join-Path $paths.FEATURE_DIR ('.plan.md.tmp.{0}' -f [guid]::NewGuid().ToString('N'))

# Copy plan template if it exists, otherwise note it or create empty file
$template = Join-Path $paths.REPO_ROOT '.specify/templates/plan-template.md'
if (Test-Path $template) {
    Copy-Item -LiteralPath $template -Destination $planTemp
    Move-Item -LiteralPath $planTemp -Destination $paths.IMPL_PLAN -Force
    Write-Output "Copied plan template to $($paths.IMPL_PLAN)"
} else {
    Write-Warning "Plan template not found at $template"
    # Create a basic plan file if template doesn't exist
    New-Item -ItemType File -Path $planTemp | Out-Null
    Move-Item -LiteralPath $planTemp -Destination $paths.IMPL_PLAN -Force
}

# Output results
if ($Json) {
    $result = [PSCustomObject]@{
        FEATURE_SPEC = $paths.FEATURE_SPEC
        IMPL_PLAN = $paths.IMPL_PLAN
        SPECS_DIR = $paths.FEATURE_DIR
        BRANCH = $paths.CURRENT_BRANCH
        HAS_GIT = $paths.HAS_GIT
    }
    $result | ConvertTo-Json -Compress
} else {
    Write-Output "FEATURE_SPEC: $($paths.FEATURE_SPEC)"
    Write-Output "IMPL_PLAN: $($paths.IMPL_PLAN)"
    Write-Output "SPECS_DIR: $($paths.FEATURE_DIR)"
    Write-Output "BRANCH: $($paths.CURRENT_BRANCH)"
    Write-Output "HAS_GIT: $($paths.HAS_GIT)"
}
