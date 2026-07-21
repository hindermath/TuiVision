# hg-hook.ps1 — SHA-256-Vergleich pre-push Hook (FR-002) — PowerShell

function Invoke-HgCheckHook {
    param([string]$Dir)

    $canonicalHook = Join-Path $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }) 'scripts/hooks/pre-push'
    $installedHook = Join-Path $Dir '.git/hooks/pre-push'

    if (-not (Test-Path $canonicalHook)) {
        return [PSCustomObject]@{ Status = 'WARN'; Dir = $Dir; Message = 'hook-canonical-missing' }
    }

    if (-not (Test-Path $installedHook)) {
        return [PSCustomObject]@{ Status = 'WARN'; Dir = $Dir; Message = 'hook-missing' }
    }

    $canonicalHash = (Get-FileHash -Algorithm SHA256 -Path $canonicalHook).Hash.ToLower()
    $installedHash = (Get-FileHash -Algorithm SHA256 -Path $installedHook).Hash.ToLower()

    if ($canonicalHash -eq $installedHash) {
        [PSCustomObject]@{ Status = 'PASS'; Dir = $Dir; Message = 'hook-sha256-match' }
    } else {
        [PSCustomObject]@{ Status = 'WARN'; Dir = $Dir; Message = 'hook-outdated' }
    }
}
