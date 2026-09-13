# DE: Jede ausgelieferte JSON-Vorlage muss parsebar sein; Generatoren binden das Release.
# EN: Every shipped JSON template must parse and generators must bind this release.
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$Root = Split-Path $PSScriptRoot -Parent
$Preset = Get-Content (Join-Path $Root 'preset.yml') -Raw
$Version = [regex]::Match($Preset, '(?m)^\s+version:\s*"?([^"\s]+)').Groups[1].Value
if (-not $Version) { throw 'Missing preset version' }
$Count = 0
foreach ($File in Get-ChildItem (Join-Path $Root 'templates') -Filter '*.json' -Recurse) {
    $Data = Get-Content $File.FullName -Raw | ConvertFrom-Json -AsHashtable
    if ($Data.ContainsKey('generator') -and $Data.generator.version -ne $Version) {
        throw "Generator version mismatch: $($File.Name)"
    }
    $Count++
}
if ($Count -eq 0) { throw 'No JSON templates verified' }
Write-Output "PASS: ${Count} shipped JSON templates parse and bind release ${Version}"
