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
'intake-authoring-governance' {
        Assert-Documentation ($readme.Contains("$version; bekannte Schema-2-Receipts")) 'German generator claim binds current release'
        Assert-Documentation ($readme.Contains("generator template binds $version")) 'English generator claim binds same release'
        $receipt=Get-Content (Join-Path $root 'templates/intake-authoring-receipt-template.json') -Raw|ConvertFrom-Json
        Assert-Documentation ($receipt.generator.version -ceq $version -and $receipt.schemaVersion -ceq '2.0') 'shipped generator metadata unchanged in schema and bound to current release'
        foreach($path in @('scripts/validate-intake-authoring-receipt.sh','scripts/validate-intake-authoring-receipt.ps1','tests/test-intake-authoring-lifecycle.ps1')){
            Assert-Documentation ([IO.File]::ReadAllText((Join-Path $root $path)).Contains("'0.3.8'") -or [IO.File]::ReadAllText((Join-Path $root $path)).Contains('"0.3.8"')) 'previous stable generator retained explicitly'
        }
    }
}
"PASS: $assertions release-documentation assertions; no model calls or network downloads."
exit 0
