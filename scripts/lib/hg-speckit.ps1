# hg-speckit.ps1 — Spec-kit Template-Versionserkennung (FR-018) — PowerShell

function Invoke-HgCheckSpeckit {
    param([string]$SpecFile)

    $specifyDir = Join-Path $(if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }) '.specify'
    if (-not (Test-Path $specifyDir)) { return }
    if (-not (Test-Path $SpecFile)) { return }

    $optionsFile = Join-Path $specifyDir 'init-options.json'
    $speckitVer = 'unknown'
    if (Test-Path $optionsFile) {
        try {
            $opts = Get-Content $optionsFile | ConvertFrom-Json
            $speckitVer = $opts.speckit_version ?? 'unknown'
        } catch {
            Write-Verbose "Spec Kit options could not be read: $($_.Exception.Message)"
        }
    }

    $content = Get-Content -Path $SpecFile -ErrorAction SilentlyContinue
    $specVer = ''
    $versionMatch = $content | Select-String -Pattern '^\*\*Spec-kit Template Version\*\*:\s*(.+)$' | Select-Object -First 1
    if ($versionMatch -and $versionMatch.Matches.Count -gt 0) {
        $specVer = $versionMatch.Matches[0].Groups[1].Value
    }

    if ($specVer) {
        $specVer = $specVer.Trim()
        if ($specVer -eq $speckitVer) {
            [PSCustomObject]@{ Status = 'PASS'; File = $SpecFile; Message = 'speckit-version-current' }
        } else {
            [PSCustomObject]@{ Status = 'WARN'; File = $SpecFile; Message = "spec-template-version-outdated (spec: ${specVer}, installed: ${speckitVer})" }
        }
        return
    }

    # Fallback: check created date
    $createdMatch = $content | Select-String -Pattern '^\*\*Created\*\*:' | Select-Object -First 1
    $createdLine = if ($null -eq $createdMatch) { '' } else { $createdMatch.Line }
    if ($createdLine -and $createdLine -match '(\d{4}-\d{2}-\d{2})') {
        $createdDate = [datetime]::ParseExact($Matches[1], 'yyyy-MM-dd', $null)
        $daysOld = ([datetime]::Today - $createdDate).Days
        if ($daysOld -gt 90) {
            [PSCustomObject]@{ Status = 'WARN'; File = $SpecFile; Message = "spec-may-be-outdated (created $($Matches[1]), ${daysOld} days ago)" }
            return
        }
    }

    [PSCustomObject]@{ Status = 'PASS'; File = $SpecFile; Message = 'speckit-version-ok' }
}
