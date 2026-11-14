#Requires -Version 7.0
<#
.SYNOPSIS
    DE: Startet genau einen Smoke-Test-Kindprozess. EN: Starts one smoke-test child process.
.DESCRIPTION
    DE: Dekodiert ein JSON-Argumentarray und verwendet den PowerShell-Aufrufoperator,
    damit native Programme sowie Windows-CMD-Starter gleich behandelt werden.
    EN: Decodes one JSON argument array and uses PowerShell's call operator so
    native programs and Windows CMD launchers follow the same path.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $Executable,
    [Parameter(Mandatory)][string] $ArgumentsJson
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$arguments = $ArgumentsJson | ConvertFrom-Json -Depth 30 -NoEnumerate
if ($arguments -isnot [object[]]) { throw 'ArgumentsJson must contain one JSON array.' }
& $Executable @($arguments | ForEach-Object { [string] $_ })
if ($null -ne $LASTEXITCODE) { exit $LASTEXITCODE }
