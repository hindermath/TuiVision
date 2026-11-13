# DE/EN: JSON templates parse; documentation-only 0.2.13/0.2.14 preserve generator 0.2.12.
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$Root = Split-Path $PSScriptRoot -Parent
$Preset = Get-Content (Join-Path $Root 'preset.yml') -Raw
$Version = [regex]::Match($Preset, '(?m)^\s+version:\s*"?([^"\s]+)').Groups[1].Value
if (-not $Version) { throw 'Missing preset version' }
# DE/EN: The package metadata advances without rewriting receipt provenance.
# All other versions retain the original package/generator equality contract.
$GeneratorVersion = if ($Version -cin @('0.2.13', '0.2.14')) { '0.2.12' } else { $Version }
$Count = 0
foreach ($File in Get-ChildItem (Join-Path $Root 'templates') -Filter '*.json' -Recurse) {
    $Data = Get-Content $File.FullName -Raw | ConvertFrom-Json -AsHashtable
    if ($Data.ContainsKey('generator') -and $Data.generator.version -ne $GeneratorVersion) {
        throw "Generator version mismatch: $($File.Name)"
    }
    $Count++
}
if ($Count -eq 0) { throw 'No JSON templates verified' }
# DE: Zeilenfortsetzungen sind fuer die Community-Pruefung nicht auswertbar.
# EN: The community verifier cannot parse shell line continuations.
$Readme = Get-Content (Join-Path $Root 'README.md') -Raw
$Archive = "https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/archive/refs/tags/v${Version}.zip"
$Command = "specify preset add --from ${Archive} --priority 66"
$InstallPattern = '(?m)^' + [regex]::Escape($Command) + '\r?$'
if (-not [regex]::IsMatch($Readme, $InstallPattern)) {
    throw 'README must contain the exact release installation command on one line'
}
foreach ($Ending in @("`n", "`r`n")) {
    if (-not [regex]::IsMatch("${Command}${Ending}", $InstallPattern)) {
        throw 'Installation-line regression: LF/CRLF command rejected'
    }
}
foreach ($InvalidCommand in @(
    "specify preset add \`n  --from ${Archive} --priority 66",
    $Command.Replace("v${Version}.zip", 'v0.0.0.zip'),
    $Command.Replace('--priority 66', '--priority 65')
)) {
    if ([regex]::IsMatch($InvalidCommand, $InstallPattern)) {
        throw 'Installation-line regression: invalid command accepted'
    }
}
Write-Output "PASS: ${Count} shipped JSON templates parse; package ${Version}, receipt generator ${GeneratorVersion}"
