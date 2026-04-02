# rename-lastenheft.ps1 — Lastenheft umbenennen / Rename Lastenheft
# FR-REV-B03; Contract: rename-lastenheft-cli.md
#Requires -Version 7

[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$File,
    [Parameter(Mandatory)][string]$BranchName
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

if (-not (Test-Path $File)) {
    Write-Error "Fehler: Datei nicht gefunden: $File / Error: File not found: $File"
    exit 1
}

$fileName  = Split-Path $File -Leaf
$stem      = [IO.Path]::GetFileNameWithoutExtension($fileName)
$newName   = "$stem.$BranchName.md"
$targetDir = Split-Path $File -Parent
$newPath   = Join-Path $targetDir $newName

if ($File -eq $newPath) {
    Write-Host "INFO: Datei bereits korrekt benannt: $fileName"
    exit 0
}

git mv $File $newPath
if ($LASTEXITCODE -ne 0) {
    Write-Error "git mv fehlgeschlagen / git mv failed"
    exit 1
}

$commitMsg = @"
chore: rename Lastenheft to $newName

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>
"@
git commit -m $commitMsg
if ($LASTEXITCODE -ne 0) {
    Write-Error "git commit fehlgeschlagen / git commit failed"
    exit 1
}

Write-Host "✓ Umbenannt: $fileName -> $newName"
