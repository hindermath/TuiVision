# hg-bilingual.ps1 — Bilingualitätsprüfung Markdown (FR-004) — PowerShell

$HG_DE_PATTERNS = 'Überblick|Verwendung|Einrichtung|Voraussetzungen|Azubi|Hinweise|Zweck|Beschreibung|Schnellstart|Für Azubis|Zusammenfassung|Anleitung'
$HG_EN_PATTERNS = 'Overview|Usage|Setup|Prerequisites|Apprentice|Notes|Purpose|Description|Quickstart|For Apprentices|Summary|Instructions'

function Invoke-HgCheckBilingual {
    param([string]$FilePath)

    if (-not (Test-Path $FilePath)) { return }
    if ($FilePath -notmatch '\.md$') { return }

    $content = Get-Content -Path $FilePath -ErrorAction SilentlyContinue

    # Array normalization keeps zero and one match strict-mode safe.
    $hasDE = @($content | Select-String -Pattern "^#{1,3} .*($HG_DE_PATTERNS)" -CaseSensitive:$false).Count
    $hasEN = @($content | Select-String -Pattern "^#{1,3} .*($HG_EN_PATTERNS)" -CaseSensitive:$false).Count

    if ($hasDE -gt 0 -and $hasEN -gt 0) {
        [PSCustomObject]@{ Status = 'PASS'; File = $FilePath; Message = 'bilingual-ok' }
    } else {
        $partner = if ($FilePath -match '(?i)\.en\.md$') {
            $FilePath -replace '(?i)\.en\.md$', '.md'
        } else {
            $FilePath -replace '(?i)\.md$', '.en.md'
        }
        $baseName = [IO.Path]::GetFileName($FilePath)
        $partnerName = [IO.Path]::GetFileName($partner)
        $paired = (Test-Path -LiteralPath $partner -PathType Leaf) -and
            ((Get-Content -LiteralPath $FilePath -Raw) -match [regex]::Escape("(${partnerName})")) -and
            ((Get-Content -LiteralPath $partner -Raw) -match [regex]::Escape("(${baseName})"))
        if ($paired) {
            [PSCustomObject]@{ Status = 'PASS'; File = $FilePath; Message = 'bilingual-language-pair' }
        } else {
            [PSCustomObject]@{ Status = 'WARN'; File = $FilePath; Message = 'bilingual-section-missing' }
        }
    }
}
