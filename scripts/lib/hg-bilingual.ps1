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
        $partnerCandidates = if ($FilePath -match '(?i)\.en\.md$') {
            $pairRoot = $FilePath -replace '(?i)\.en\.md$', ''
            @("${pairRoot}.md", "${pairRoot}.MD")
        } else {
            $pairRoot = $FilePath -replace '(?i)\.md$', ''
            @("${pairRoot}.en.md", "${pairRoot}.EN.md", "${pairRoot}.en.MD", "${pairRoot}.EN.MD")
        }
        $baseName = [IO.Path]::GetFileName($FilePath)
        $paired = $false
        foreach ($candidate in $partnerCandidates) {
            $partnerName = [IO.Path]::GetFileName($candidate)
            if ((Test-Path -LiteralPath $candidate -PathType Leaf) -and
                ((Get-Content -LiteralPath $FilePath -Raw) -cmatch [regex]::Escape("(${partnerName})")) -and
                ((Get-Content -LiteralPath $candidate -Raw) -cmatch [regex]::Escape("(${baseName})"))) {
                $paired = $true
                break
            }
        }
        if ($paired) {
            [PSCustomObject]@{ Status = 'PASS'; File = $FilePath; Message = 'bilingual-language-pair' }
        } else {
            [PSCustomObject]@{ Status = 'WARN'; File = $FilePath; Message = 'bilingual-section-missing' }
        }
    }
}
