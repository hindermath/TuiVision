#Requires -Version 7
Set-StrictMode -Version Latest

function Test-HBSourceRepository {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Path)

    if (-not (Test-Path -LiteralPath (Join-Path $Path '.git')) -or
        -not (Test-Path -LiteralPath (Join-Path $Path 'scripts/sync-home.ps1'))) {
        return $false
    }
    $remote = & git -C $Path remote get-url origin 2>$null
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace([string]$remote)) {
        return $false
    }
    $prefix = & git -C $Path rev-parse --show-prefix 2>$null
    $LASTEXITCODE -eq 0 -and
        [string]::IsNullOrEmpty([string]$prefix)
}

function Resolve-HBSourceRepository {
    [CmdletBinding()]
    param(
        [string]$StartPath = $PSScriptRoot,
        [switch]$AllowLegacy
    )

    $candidate = (Resolve-Path -LiteralPath $StartPath -ErrorAction Stop).Path
    if (Test-Path -LiteralPath $candidate -PathType Leaf) {
        $candidate = Split-Path -Parent $candidate
    }
    $homeRoot = (Resolve-Path -LiteralPath $HOME).Path
    while ($candidate) {
        if ([IO.Path]::GetFullPath($candidate) -ne $homeRoot -and
            (Test-HBSourceRepository -Path $candidate)) {
            return $candidate
        }
        $parent = Split-Path -Parent $candidate
        if ($parent -eq $candidate) { break }
        $candidate = $parent
    }

    $candidates = [Collections.Generic.List[object]]::new()
    if (-not [string]::IsNullOrWhiteSpace($env:HOME_BASELINE_SOURCE)) {
        $candidates.Add([pscustomobject]@{ Path = $env:HOME_BASELINE_SOURCE; Legacy = $false })
    }
    $stateFile = Join-Path $HOME '.home-baseline/source-repository.json'
    if (Test-Path -LiteralPath $stateFile -PathType Leaf) {
        try {
            $state = Get-Content -LiteralPath $stateFile -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($state.sourcePath) {
                $candidates.Add([pscustomobject]@{ Path = [string]$state.sourcePath; Legacy = $false })
            }
        } catch {
            throw "Invalid source repository state: $stateFile"
        }
    }
    $candidates.Add([pscustomobject]@{ Path = (Join-Path $HOME 'home-baseline-source'); Legacy = $false })
    if ($AllowLegacy) {
        $candidates.Add([pscustomobject]@{ Path = (Join-Path $HOME 'home-baseline-tmp'); Legacy = $true })
    }

    foreach ($item in $candidates) {
        $path = [IO.Path]::GetFullPath([Environment]::ExpandEnvironmentVariables($item.Path))
        if (Test-HBSourceRepository -Path $path) {
            if ($item.Legacy) {
                Write-Warning 'Legacy checkout ~/home-baseline-tmp is deprecated; migrate to ~/home-baseline-source.'
            }
            return $path
        }
    }
    throw 'Level-0 source checkout not found. Set HOME_BASELINE_SOURCE or run migrate-level0-source-checkout.'
}
