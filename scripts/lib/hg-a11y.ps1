# hg-a11y.ps1 — Accessibility-Prüfung Markdown (FR-005/006) — PowerShell

function Invoke-HgCheckA11y {
    param([string]$FilePath)

    if (-not (Test-Path $FilePath)) { return }
    if ($FilePath -notmatch '\.md$') { return }

    $lines = Get-Content -Path $FilePath -ErrorAction SilentlyContinue
    $prevLevel = 0
    $inFencedBlock = $false

    # 1. Heading hierarchy — skip fenced code blocks and indented code blocks
    foreach ($line in $lines) {
        if ($line -match '^```') { $inFencedBlock = -not $inFencedBlock; continue }
        if ($inFencedBlock) { continue }
        if ($line -match '^(    |\t)') { continue }

        if ($line -match '^(#+)') {
            $level = $Matches[1].Length
            if ($prevLevel -gt 0 -and $level -gt ($prevLevel + 1)) {
                [PSCustomObject]@{ Status = 'WARN'; File = $FilePath; Message = "heading-gap-h${prevLevel}-to-h${level}" }
            }
            $prevLevel = $level
        }
    }

    # 2. Empty alt texts
    # Array normalization keeps zero and one match strict-mode safe.
    $emptyAlt = @($lines | Select-String -Pattern '!\[\]\(' -SimpleMatch).Count
    if ($emptyAlt -gt 0) {
        [PSCustomObject]@{ Status = 'WARN'; File = $FilePath; Message = 'empty-alt-text' }
    }

    # 3. Non-descriptive link texts — skip inline code spans to avoid false positives
    $badLinks = 0
    foreach ($line in $lines) {
        $stripped = $line -replace '`[^`]+`', ''
        if ($stripped -match '\[(hier|here|click here|link|mehr|more|this)\]\(' ) { $badLinks++ }
    }
    if ($badLinks -gt 0) {
        [PSCustomObject]@{ Status = 'WARN'; File = $FilePath; Message = 'non-descriptive-link-text' }
    }

    # 4. Color-only styling
    $colorOnly = @($lines | Select-String -Pattern 'style="[^"]*color:' -CaseSensitive:$false).Count
    if ($colorOnly -gt 0) {
        [PSCustomObject]@{ Status = 'WARN'; File = $FilePath; Message = 'colour-only-styling' }
    }
}
