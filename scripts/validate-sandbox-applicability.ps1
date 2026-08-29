#Requires -Version 7.0
<#
.SYNOPSIS
    Prüft TuiVision-Sandbox-Anwendbarkeit read-only. / Validates TuiVision sandbox applicability read-only.

.DESCRIPTION
    DE: Prüft ausschließlich Struktur, Kardinalität, erlaubte Werte und
    portable Pfade der Assessment-JSON. Der Befehl erteilt keine Freigabe.

    EN: Validates only structure, cardinality, allowed values, and portable
    paths in the assessment JSON. The command grants no approval.

.PARAMETER Evidence
    Repository-relativer Pfad zur Assessment-JSON. / Repository-relative path to the assessment JSON.

.PARAMETER RepositoryRoot
    Repository-Root für die sichere Pfadauflösung. / Repository root for safe path resolution.

.PARAMETER Json
    Gibt ein maschinenlesbares Ergebnis aus. / Emits a machine-readable result.

.EXAMPLE
    Test-SandboxApplicability -Evidence docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json -RepositoryRoot .
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string] $Evidence,

    [string] $RepositoryRoot = '.',

    [switch] $Json
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-SandboxPythonCommand {
    $python = Get-Command python3 -ErrorAction SilentlyContinue
    if ($null -eq $python) {
        $python = Get-Command python -ErrorAction SilentlyContinue
    }
    if ($null -eq $python) {
        throw 'Python 3 is required.'
    }
    return $python.Source
}

function Test-SandboxApplicability {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [string] $Evidence,

        [string] $RepositoryRoot = '.',

        [switch] $Json
    )

    $python = Get-SandboxPythonCommand
    $validator = Join-Path $PSScriptRoot 'validate-sandbox-applicability.py'
    $arguments = @($validator, '--evidence', $Evidence, '--repo-root', $RepositoryRoot)
    if ($Json) {
        $arguments += '--json'
    }
    & $python @arguments
    if ($LASTEXITCODE -ne 0) {
        throw "Sandbox applicability validation failed with exit code $LASTEXITCODE."
    }
}

if ($MyInvocation.InvocationName -ne '.') {
    $python = Get-SandboxPythonCommand
    $validator = Join-Path $PSScriptRoot 'validate-sandbox-applicability.py'
    $arguments = @($validator, '--evidence', $Evidence, '--repo-root', $RepositoryRoot)
    if ($Json) {
        $arguments += '--json'
    }
    & $python @arguments
    exit $LASTEXITCODE
}
