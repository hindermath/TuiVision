<#
.SYNOPSIS
    Benennt ein verfolgtes Lastenheft sicher um. / Safely renames a tracked Lastenheft.
.SYNTAX
    rename-lastenheft.ps1 -File <path> -BranchName <name> [-NoCommit] [-WhatIf]
.DESCRIPTION
    Verwendet git mv und erzeugt standardmäßig einen isolierten Rename-Commit.
    Uses git mv and creates an isolated rename commit by default.
.PARAMETER File
    Verfolgte Lastenheft*.md-Quelldatei. / Tracked Lastenheft*.md source file.
.PARAMETER BranchName
    Branchname; Schrägstriche werden zu Bindestrichen. / Branch name; slashes become hyphens.
.PARAMETER NoCommit
    Benennt um, ohne einen Commit zu erzeugen. / Renames without creating a commit.
.EXAMPLE
    ./scripts/rename-lastenheft.ps1 -File Lastenheft_Test.md -BranchName 016-test -NoCommit
#>
#Requires -Version 7

[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
param(
    [Parameter(Mandatory)][string]$File,
    [Parameter(Mandatory)][string]$BranchName,
    [switch]$NoCommit
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

function Stop-WithBilingualError {
    param(
        [Parameter(Mandatory)][string]$German,
        [Parameter(Mandatory)][string]$English
    )

    throw "$German / $English"
}

git rev-parse --is-inside-work-tree *> $null
if ($LASTEXITCODE -ne 0) {
    Stop-WithBilingualError 'Kein Git-Repository.' 'Not inside a Git repository.'
}

if (-not (Test-Path -LiteralPath $File -PathType Leaf)) {
    Stop-WithBilingualError "Datei nicht gefunden: $File" "File not found: $File"
}

$fileName = Split-Path $File -Leaf
if (-not ($fileName.StartsWith('Lastenheft', [StringComparison]::Ordinal) -and
        [IO.Path]::GetExtension($fileName) -eq '.md')) {
    Stop-WithBilingualError 'Quelle muss Lastenheft*.md sein.' 'Source must match Lastenheft*.md.'
}

git ls-files --error-unmatch -- $File *> $null
if ($LASTEXITCODE -ne 0) {
    Stop-WithBilingualError "Quelle ist nicht in Git verfolgt: $File" "Source is not tracked by Git: $File"
}

$safeBranch = $BranchName.Replace('/', '-')
if ($safeBranch.Contains('\', [StringComparison]::Ordinal) -or
    $safeBranch.Contains('..', [StringComparison]::Ordinal) -or
    $safeBranch -notmatch '^[A-Za-z0-9][A-Za-z0-9._-]*$') {
    Stop-WithBilingualError "Unsicherer Branchname: $BranchName" "Unsafe branch name: $BranchName"
}

if ($fileName.EndsWith(".$safeBranch.md", [StringComparison]::Ordinal)) {
    Write-Host "INFO: Datei bereits korrekt benannt / File already named correctly: $fileName"
    exit 0
}

$stem = [IO.Path]::GetFileNameWithoutExtension($fileName)
$newName = "$stem.$safeBranch.md"
$targetDir = Split-Path $File -Parent
if ([string]::IsNullOrEmpty($targetDir)) {
    $targetDir = '.'
}
$newPath = Join-Path $targetDir $newName

if (Test-Path -LiteralPath $newPath) {
    Stop-WithBilingualError "Zieldatei existiert bereits: $newPath" "Target file already exists: $newPath"
}

if (-not $PSCmdlet.ShouldProcess("$File -> $newPath", 'git mv')) {
    Write-Host "DRY-RUN: $File -> $newPath"
    exit 0
}

git mv -- $File $newPath
if ($LASTEXITCODE -ne 0) {
    Stop-WithBilingualError 'git mv fehlgeschlagen.' 'git mv failed.'
}

if ($NoCommit) {
    Write-Host "OK: Umbenannt ohne Commit / Renamed without commit: $fileName -> $newName"
    exit 0
}

# --only hält bereits vorgemerkte fremde Änderungen aus dem Rename-Commit fern.
# --only keeps unrelated staged changes out of the rename commit.
$commitMessage = "chore: rename Lastenheft to $newName`n`nCo-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"
git commit --only -m $commitMessage -- $File $newPath
if ($LASTEXITCODE -ne 0) {
    Stop-WithBilingualError 'git commit fehlgeschlagen.' 'git commit failed.'
}

Write-Host "OK: Umbenannt und committed / Renamed and committed: $fileName -> $newName"
