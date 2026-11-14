#Requires -Version 7.3
<#
.SYNOPSIS
    Release-Dokumentation pruefen / Test release documentation.
.DESCRIPTION
    DE: Statische Vertragspruefung und bei Statistics eigene Offline-Doubles.
    Keine Modellaufrufe, echte Downloads, Installation oder Provider-Aktionen.
    EN: Static contracts and owned offline doubles for Statistics. No model
    calls, real downloads, installations or provider operations.
.EXAMPLE
    pwsh -NoProfile -File tests/test-release-documentation.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$metadata=[IO.File]::ReadAllText((Join-Path $root 'preset.yml'))
$id=[regex]::Match($metadata,'(?m)^  id: "([^"]+)"').Groups[1].Value
$version=[regex]::Match($metadata,'(?m)^  version: "([^"]+)"').Groups[1].Value
$readme=[IO.File]::ReadAllText((Join-Path $root 'README.md'))
$priorities=@{'secure-development-assurance-governance'=15;'intake-authoring-governance'=64;'autonomous-run-governance'=70;'parallel-autonomous-run-governance'=80;'project-statistics-governance'=90}
$assertions=0
function Assert-Documentation {
    param([bool]$Condition,[string]$Message)
    if(-not $Condition){throw "FAIL: $Message"}
    $script:assertions++
}
$url="https://github.com/hindermath/spec-kit-preset-$id/archive/refs/tags/v$version.zip"
$line="specify preset add --from $url --priority $($priorities[$id])"
Assert-Documentation ([regex]::IsMatch($readme,'(?m)^\s*'+[regex]::Escape($line)+'\r?$')) 'exact release install command on one line'
Assert-Documentation (Test-Path -LiteralPath (Join-Path $root "docs/release-$version-validation.md")) 'current release preparation record exists'
switch($id){
'secure-development-assurance-governance' {
        $flow=[regex]::Match($readme,'(?s)Der Lebenszyklus ist:(.*?)<a id="assurance-section-7">').Groups[1].Value
        foreach($term in @('baseline.json','deltas/','image-impact.json','closure.json','evidence-matrix.md','Ready','Lifecycle')){
            Assert-Documentation ($flow.Contains($term)) "lifecycle explains $term"
        }
        Assert-Documentation (-not $flow.Contains('[Zweck und Einsatzbereich')) 'lifecycle is not a duplicate contents list'
        Assert-Documentation ($flow.Contains('keine Pilot-, Projekt- oder allgemeine Freigabe')) 'technical evidence grants no human approval'
        Assert-Documentation ($readme.Contains('[Routing-Vertrag](model-routing.json)')) 'routing contract linked'
        foreach($term in @('fast-mechanical','frontier-reasoning',('modellfreie Skriptpr'+[char]0xfc+'fungen'),'model-free script checks','kein Schema 3')){
            Assert-Documentation ($readme.Contains($term)) "stable routing explains $term"
        }
    }
}
"PASS: $assertions release-documentation assertions; no model calls or network downloads."
exit 0
