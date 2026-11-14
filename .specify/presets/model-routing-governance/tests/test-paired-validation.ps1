#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Prueft die Testkopplung offline. EN: Checks paired validation offline.
.DESCRIPTION
    DE: Keine Modelle oder Netzwerkzugriffe. Prueft Reihenfolge, Fehlerfortsetzung und Gesamtstatus.
    EN: No models or network. Checks ordering, continuation after failures and aggregate status.
.EXAMPLE
    pwsh -NoProfile -File tests/test-paired-validation.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
Import-Module (Join-Path $PSScriptRoot 'live/model-routing-validation-core.psm1') -Force
$seen=[Collections.Generic.List[string]]::new()
$callback={param($mode);$seen.Add($mode);@{mode=$mode;finalStatus='PASS'}}.GetNewClosure()
$result=Invoke-MRValidationPair $callback
if ($result.finalStatus -cne 'PASS' -or ($seen -join ',') -cne 'smoke,production-phase-runner') { throw 'Pair order or pass aggregation failed.' }
$callback={param($mode);if($mode -ceq 'smoke'){throw 'synthetic infrastructure failure'};@{mode=$mode;finalStatus='PASS'}}
$result=Invoke-MRValidationPair $callback
if ($result.finalStatus -cne 'FAILED' -or $result.stages.Count -ne 2 -or $result.stages[1].finalStatus -cne 'PASS') { throw 'Second stage was lost after first-stage error.' }
$callback={param($mode);@{mode=$mode;finalStatus='PASS_WITH_RETRY'}}
if ((Invoke-MRValidationPair $callback).finalStatus -cne 'PASS_WITH_RETRY') { throw 'Retry evidence was lost.' }
$callback={param($mode);@{mode=$mode;finalStatus='BLOCKED'}}
if ((Invoke-MRValidationPair $callback).finalStatus -cne 'FAILED') { throw 'Blocked pair accepted.' }
$callback={param($mode);@{mode=$mode;finalStatus='PASS'}}
$extended=Invoke-MRValidationPair $callback -IncludeReadOnlyProbe
if(($extended.stages.mode -join ',') -cne 'smoke,production-phase-runner,read-only-probe'){throw 'Read-only extension order failed.'}
$LASTEXITCODE=0
$nativeCallback={
    param($mode)
    & pwsh -NoProfile -Command 'exit 7' | ForEach-Object { $_ }
    $exitCode=$global:LASTEXITCODE
    @{mode=$mode;finalStatus='FAILED';exitCode=$exitCode}
}.GetNewClosure()
$nativeResult=Invoke-MRValidationPair $nativeCallback
if (@($nativeResult.stages | Where-Object { $_.exitCode -ne 7 }).Count) { throw 'Native exit code was shadowed by closure.' }
'PASS: paired ordering, continuation, failure and retry aggregation; offline.'
