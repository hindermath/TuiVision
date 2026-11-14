#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Prueft nur die synthetische Testfixture. EN: Checks only the synthetic fixture.
.DESCRIPTION
    DE: Keine produktiven Gates; validiert vorhandene Dateien und liefert reale Pfade.
    EN: Not a production gate; validates actual files and reports their real paths.
.PARAMETER Json
    DE: Liefert JSON. EN: Returns JSON.
.PARAMETER RequireTasks
    DE: Verlangt die Aufgabenliste. EN: Requires the task list.
.PARAMETER IncludeTasks
    DE: Fuehrt tasks.md im Ergebnis auf. EN: Lists tasks.md in the result.
.EXAMPLE
    pwsh -NoProfile -File .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
#>
[CmdletBinding()]
param([switch]$Json,[switch]$RequireTasks,[switch]$IncludeTasks)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$root=Split-Path -Parent (Split-Path -Parent (Split-Path -Parent $PSScriptRoot))
$feature=Join-Path $root 'specs/999-routing-integration'
foreach($name in @('spec.md','plan.md','tasks.md','research.md','data-model.md','quickstart.md','checklists/requirements.md')) {
    if(-not(Test-Path -LiteralPath (Join-Path $feature $name) -PathType Leaf)){throw "Missing fixture artifact: $name"}
}
$docs=@('spec.md','research.md','data-model.md','quickstart.md','checklists/requirements.md')
if($RequireTasks -or $IncludeTasks){$docs+='tasks.md'}
if($Json){@{REPO_ROOT=$root;FEATURE_DIR=$feature;AVAILABLE_DOCS=$docs}|ConvertTo-Json -Compress}else{$feature;$docs}
