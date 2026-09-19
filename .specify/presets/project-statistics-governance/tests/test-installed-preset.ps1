#Requires -Version 7
<#
.SYNOPSIS
Prueft die installierten Preset-Oberflaechen / Tests installed preset surfaces.
.DESCRIPTION
Installiert ausschliesslich in einem temporaeren Git-Projekt.
Installs only in a temporary Git project and preserves authored report data.
.PARAMETER Package
Geprueftes Paketverzeichnis / verified package directory.
.EXAMPLE
pwsh -NoProfile -File tests/test-installed-preset.ps1
#>
[CmdletBinding()]
param([string]$Package=(Split-Path $PSScriptRoot))
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$root=Join-Path ([IO.Path]::GetTempPath()) ('statistics-install-test-'+[guid]::NewGuid().ToString('N'))
$initial=Get-Location
function Invoke-InstallTest {
    param([string]$Executable,[string[]]$Arguments)
    $output=& $Executable @Arguments 2>&1
    if($LASTEXITCODE){throw "Installation test failed: $Executable $Arguments : $output"}
    return $output -join "`n"
}
try {
    $null=New-Item -ItemType Directory -Path $root,(Join-Path $root '.specify/templates'),(Join-Path $root '.agents/skills'),(Join-Path $root '.claude/commands') -Force
    # Test the deliverable, not a developer checkout containing .git object files.
    # In particular, Windows Git objects may be read-only and are not package data.
    $archive=Join-Path $root 'candidate.zip'
    $candidate=Join-Path $root 'package'
    if(Test-Path -LiteralPath (Join-Path $Package '.git')){
        $null=Invoke-InstallTest git @('-C',$Package,'archive','--format=zip','--output',$archive,'HEAD')
        Expand-Archive -LiteralPath $archive -DestinationPath $candidate
        $Package=$candidate
    }
    Set-Location $root
    $null=Invoke-InstallTest git @('init','-q')
    foreach($name in @('constitution','plan','tasks')){
        [IO.File]::WriteAllText((Join-Path $root ".specify/templates/${name}-template.md"),'BASE CONTRACT')
    }
    $null=Invoke-InstallTest specify @('preset','add','--dev',$Package,'--priority','90')
    $null=Invoke-InstallTest specify @('preset','list')
    $null=Invoke-InstallTest specify @('preset','info','project-statistics-governance')
    $contract=Invoke-InstallTest specify @('preset','resolve','project-statistics-contract')
    if(-not $contract.Contains('project-statistics-contract')){throw 'Missing contract resolution.'}
    foreach($name in @('constitution','plan','tasks')){
        $resolved=Invoke-InstallTest specify @('preset','resolve',"${name}-template")
        if(-not $resolved){throw 'Missing composed template.'}
    }
    $commands=@(Get-ChildItem '.agents/skills' -Recurse -File | Where-Object FullName -match 'statistics')
    if($commands.Count -lt 3){throw 'Missing generated agent commands.'}
    foreach($file in $commands){
        if(-not ([IO.File]::ReadAllText($file.FullName)).Contains('.specify/presets/project-statistics-governance/scripts/')){throw 'Installed script path was rewritten.'}
    }
    $null=New-Item -ItemType Directory -Path 'docs/project-statistics'
    [IO.File]::WriteAllText((Join-Path $root 'docs/project-statistics/report.md'),'AUTHORED REPORT')
    $hash=(Get-FileHash 'docs/project-statistics/report.md').Hash
    $null=Invoke-InstallTest specify @('preset','disable','project-statistics-governance')
    $registry=Get-Content '.specify/presets/.registry' -Raw | ConvertFrom-Json -AsHashtable
    if($registry.presets['project-statistics-governance'].enabled){throw 'Disable did not persist.'}
    $null=Invoke-InstallTest specify @('preset','enable','project-statistics-governance')
    $null=Invoke-InstallTest specify @('preset','remove','project-statistics-governance')
    if((Get-FileHash 'docs/project-statistics/report.md').Hash -cne $hash){throw 'Removal altered authored data.'}
    $null=Invoke-InstallTest specify @('preset','add','--dev',$Package,'--priority','90')
    if((Get-FileHash 'docs/project-statistics/report.md').Hash -cne $hash){throw 'Reinstall altered authored data.'}
    Write-Output 'PASS: isolated install, commands, resolve, disable/enable, remove and reinstall.'
}finally{
    Set-Location $initial
    if(Test-Path -LiteralPath $root){Remove-Item -LiteralPath $root -Recurse -Force}
}
