#Requires -Version 7
<#
.SYNOPSIS
Prueft feste Git-Fixtures / Tests deterministic Git fixtures.
.DESCRIPTION
Erstellt isolierte temporaere Repositories und entfernt nur diese Testdaten.
Creates isolated temporary repositories and removes only those test fixtures.
.PARAMETER Keep
Behaelt Testdaten zur Fehleranalyse / Keeps fixtures for diagnosis.
.EXAMPLE
pwsh -NoProfile -File tests/test-project-statistics.ps1
#>
[CmdletBinding()]
param([switch]$Keep)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$package=Split-Path $PSScriptRoot
$engine=Join-Path $package 'scripts/project-statistics.ps1'
$wrapper=Join-Path $package 'scripts/project-statistics.sh'
$root=Join-Path ([IO.Path]::GetTempPath()) ('project-statistics-test-'+[guid]::NewGuid().ToString('N'))
$assertions=0
$initial=Get-Location
$savedAuthor=$env:GIT_AUTHOR_DATE; $savedCommitter=$env:GIT_COMMITTER_DATE

function Assert-Test {
    param([bool]$Condition,[string]$Message)
    if (-not $Condition) { throw "FAIL: $Message" }
    $script:assertions++
}
function Invoke-TestGit {
    param([string[]]$Arguments)
    $result=& git @Arguments 2>&1
    if ($LASTEXITCODE) { throw "Git fixture failed: $result" }
    return $result
}
function Write-TestFile {
    param([string]$Path,[string]$Text)
    $absolute=Join-Path (Get-Location) $Path
    $null=New-Item -ItemType Directory -Path (Split-Path $absolute) -Force
    [IO.File]::WriteAllText($absolute,$Text,[Text.UTF8Encoding]::new($false))
}
function Invoke-TestEngine {
    param([string]$Action='Status',[int]$Expected=0,[string[]]$Extra=@())
    $output=& pwsh -NoProfile -File $engine -Action $Action -Repo (Get-Location).Path -Json @Extra 2>&1
    $code=$LASTEXITCODE
    if($code -ne $Expected){throw "Expected $Expected, got ${code}: $output"}
    $script:assertions++
    # WhatIf emits an informational line before the single JSON object.
    $json=@($output | Where-Object { $_.ToString().StartsWith('{') }) -join "`n"
    return ($json | ConvertFrom-Json -AsHashtable)
}
function Save-TestCommit {
    param([string]$Message)
    $null=Invoke-TestGit @('add','--all')
    $null=Invoke-TestGit @('-c','core.hooksPath=','commit','-qm',$Message)
}
function Get-TestDigest {
    $data=Get-ChildItem -LiteralPath (Get-Location) -Recurse -Force -File | Where-Object { $_.FullName -notmatch '[/\\]\.git[/\\]' } |
        Sort-Object FullName | ForEach-Object { $_.FullName+':'+(Get-FileHash -LiteralPath $_.FullName).Hash }
    $data -join "`n"
}
try {
    $null=New-Item -ItemType Directory -Path $root
    Set-Location $root
    $null=Invoke-TestGit @('init','-q','-b','main')
    $null=Invoke-TestGit @('config','user.name','Fixture')
    $null=Invoke-TestGit @('config','user.email','fixture@example.invalid')
    $null=Invoke-TestGit @('config','core.autocrlf','false')
    $env:GIT_AUTHOR_DATE='2026-01-04T12:00:00Z'; $env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
    Write-TestFile 'src/app.cs' "one`ntwo`n"
    Write-TestFile 'tests/check.cs' "test`n"
    Write-TestFile 'docs/manual.md' "manual`n"
    Write-TestFile 'ignored.txt' "excluded`n"
    [IO.File]::WriteAllBytes((Join-Path $root 'binary.dat'),[byte[]](0,1,2))
    Save-TestCommit 'initial source'
    $before=Get-TestDigest
    if(-not $IsWindows){
        $preview=& bash $wrapper init --repo $root --dry-run --json 2>&1
        Assert-Test ($LASTEXITCODE -eq 0) "Bash initial safe mode: $preview"
    }
    $null=Invoke-TestEngine Init 0 @('-WhatIf')
    Assert-Test ((Get-TestDigest) -ceq $before) 'Init preview writes nothing'
    $null=Invoke-TestEngine Init
    $null=Invoke-TestEngine Init 2
    $configPath=Join-Path $root 'docs/project-statistics/config.json'
    $config=Get-Content -LiteralPath $configPath -Raw | ConvertFrom-Json -AsHashtable
    $config.excludedPaths=@('ignored.txt')
    Write-TestFile 'docs/project-statistics/config.json' (($config|ConvertTo-Json -Depth 20)+"`n")
    $null=Invoke-TestEngine Update 2
    Save-TestCommit 'initialize statistics'
    $null=Invoke-TestEngine Status 1
    $before=Get-TestDigest
    $null=Invoke-TestEngine Update 0 @('-WhatIf')
    Assert-Test ((Get-TestDigest) -ceq $before) 'Update preview writes nothing'
    $null=Invoke-TestEngine Update
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    Assert-Test ($snapshot.measurement.totalTextLines -eq 4) 'Exact text lines'
    Assert-Test ($snapshot.measurement.totalTextFiles -eq 3) 'Exact text files'
    Assert-Test ($snapshot.measurement.categories.Production.lines -eq 2) 'Production category'
    Assert-Test ($snapshot.measurement.categories.Tests.lines -eq 1) 'Tests category'
    Assert-Test ($snapshot.measurement.omitted.binary -eq 1) 'Binary omitted'
    Assert-Test ($snapshot.measurement.activeDays -eq 1) 'Exact active days'
    Assert-Test ($snapshot.measurement.daily['2026-01-04'].added -eq 4) 'Exact additions'
    $report=Get-Content 'docs/project-statistics/report.md' -Raw
    Assert-Test (-not $report.Contains('Optional reference estimate')) 'References off by default'
    Assert-Test (@($report -split "`n" | Where-Object Length -gt 100).Count -eq 0) 'Accessible width'
    $before=Get-TestDigest
    $status=Invoke-TestEngine Status
    Assert-Test ($status.reproducible -and $status.current) 'Snapshot reproducible and current'
    Assert-Test ((Get-TestDigest) -ceq $before) 'Status writes nothing'
    Save-TestCommit 'statistics outputs only'
    $before=Get-TestDigest
    $null=Invoke-TestEngine Update
    Assert-Test ((Get-TestDigest) -ceq $before) 'No output-only commit feedback'
    if(-not $IsWindows){
        $bash=& bash $wrapper status --repo $root --json
        Assert-Test ($LASTEXITCODE -eq 0) 'Bash status passes'
        $bash=ConvertFrom-Json -InputObject ($bash -join "`n") -AsHashtable
        Assert-Test ($bash.sourceRevision -ceq $status.sourceRevision) 'Bash/PowerShell source parity'
    }
    # Inventory comes from Git, even if the working copy changes.
    Write-TestFile 'src/app.cs' "uncommitted`n"
    $null=Invoke-TestEngine Status
    $null=Invoke-TestEngine Update 2
    Write-TestFile 'src/app.cs' "one`ntwo`n"
    # CRLF and BOM variations in output/config are semantically identical.
    foreach($path in @('docs/project-statistics/config.json','docs/project-statistics/report.md','docs/project-statistics/snapshot.json')){
        $text=[IO.File]::ReadAllText((Join-Path $root $path)) -replace "`r`n","`n"
        [IO.File]::WriteAllText((Join-Path $root $path),$text.Replace("`n","`r`n"),[Text.UTF8Encoding]::new($true))
    }
    $null=Invoke-TestEngine Status
    foreach($path in @('docs/project-statistics/config.json','docs/project-statistics/report.md','docs/project-statistics/snapshot.json')){
        $text=[IO.File]::ReadAllText((Join-Path $root $path)) -replace "`r`n","`n"
        Write-TestFile $path $text
    }
    $env:GIT_AUTHOR_DATE='2026-01-05T12:00:00Z'; $env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
    Write-TestFile 'src/app.cs' "one`nchanged`nthree`n"
    Save-TestCommit 'change source'
    $status=Invoke-TestEngine Status 1
    Assert-Test ($status.reproducible -and -not $status.current) 'Freshness differs from reproducibility'
    $null=Invoke-TestEngine Update
    Save-TestCommit 'refresh outputs'
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    Assert-Test ($snapshot.measurement.totalTextLines -eq 5) 'Changed inventory'
    Assert-Test ($snapshot.measurement.daily['2026-01-05'].added -eq 2 -and $snapshot.measurement.daily['2026-01-05'].removed -eq 1) 'Exact changed lines'
    $null=Invoke-TestGit @('mv','src/app.cs','src/renamed.cs')
    Save-TestCommit 'rename only'
    $null=Invoke-TestEngine Update
    Save-TestCommit 'rename source binding'
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    Assert-Test ($snapshot.measurement.totalTextLines -eq 5 -and $snapshot.measurement.daily['2026-01-05'].added -eq 2) 'Rename preserves volume'
    # Config mutation is explicit and committed; optional comparisons remain estimates.
    $config.references.enabled=$true
    $config.references.scenarios=@(@{name='Example';linesPerDay=100})
    Write-TestFile 'docs/project-statistics/config.json' (($config|ConvertTo-Json -Depth 20)+"`n")
    Save-TestCommit 'enable reference estimate'
    $null=Invoke-TestEngine Update 0 @('-AsOf','2000-01-01')
    $report=Get-Content 'docs/project-statistics/report.md' -Raw
    Assert-Test ($report.Contains('not calculable')) 'Zero denominator is not calculable'
    Assert-Test ($report.Contains('not measured time savings')) 'Estimate caveat'
    Save-TestCommit 'empty window outputs'
    $null=Invoke-TestEngine Status
    $before=Get-TestDigest
    $null=Invoke-TestEngine Update 2 @('-Revision','missing-revision')
    $null=Invoke-TestEngine Update 2 @('-AsOf','2026-99-99')
    $null=Invoke-TestEngine Status 2 @('-Config','../outside.json')
    Assert-Test ((Get-TestDigest) -ceq $before) 'Negative cases write nothing'
    Write-TestFile 'docs/project-statistics/report.md' ($report.Replace('## Projekttransparenz','## Tampered'))
    $status=Invoke-TestEngine Status 1
    Assert-Test (-not $status.reproducible) 'Report tampering detected'
    Write-TestFile 'docs/project-statistics/report.md' $report
    Write-TestFile 'docs/project-statistics/config.json' '{"schemaVersion":999}'
    $null=Invoke-TestEngine Status 2
    Write-TestFile 'docs/project-statistics/config.json' (($config|ConvertTo-Json -Depth 20)+"`n")
    # Separate shallow clone must never claim complete history.
    $null=Invoke-TestEngine Status 2 @('-Config','docs/project-statistics/report.md')
    if(-not $IsWindows){
        $outside=Join-Path $root 'outside'
        $null=New-Item -ItemType Directory -Path $outside
        $null=New-Item -ItemType SymbolicLink -Path (Join-Path $root 'unsafe') -Target $outside
        $null=Invoke-TestEngine Init 2 @('-Config','unsafe/config.json')
        Remove-Item -LiteralPath (Join-Path $root 'unsafe')
        Remove-Item -LiteralPath $outside
    }
    $null=Invoke-TestGit @('branch','fixture-side')
    Write-TestFile 'src/main.cs' "main`n"
    Save-TestCommit 'main addition'
    $null=Invoke-TestGit @('switch','fixture-side')
    Write-TestFile 'src/side.cs' "side`n"
    Save-TestCommit 'side addition'
    $null=Invoke-TestGit @('switch','main')
    $null=Invoke-TestGit @('-c','core.hooksPath=','merge','--no-ff','-m','fixture merge','fixture-side')
    $null=Invoke-TestEngine Update
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    Assert-Test ($snapshot.measurement.totalTextLines -eq 7) 'Merge inventory includes both parents'
    Assert-Test ($snapshot.measurement.daily['2026-01-05'].added -eq 4) 'Merge changes counted once, not twice'
    Save-TestCommit 'merge report'
    $null=Invoke-TestGit @('rm','docs/manual.md')
    Save-TestCommit 'delete fixture document'
    $null=Invoke-TestEngine Update
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    Assert-Test ($snapshot.measurement.totalTextLines -eq 6) 'Deleted file removed from inventory'
    Assert-Test ($snapshot.measurement.daily['2026-01-05'].removed -eq 2) 'Deletion included in gross changes'
    Save-TestCommit 'deletion report'
    $config.timeZone='America/Los_Angeles'
    Write-TestFile 'docs/project-statistics/config.json' (($config|ConvertTo-Json -Depth 20)+"`n")
    Save-TestCommit 'timezone configuration'
    $env:GIT_AUTHOR_DATE='2026-01-06T00:30:00Z'; $env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
    Write-TestFile 'src/time.cs' "timezone`n"
    Save-TestCommit 'timezone source'
    $null=Invoke-TestEngine Update
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    Assert-Test ($snapshot.measurement.asOf -ceq '2026-01-05') 'Committer date uses configured timezone'
    Save-TestCommit 'timezone report'
    Write-TestFile 'STATS.md' "excluded ledger`n"
    Save-TestCommit 'coverage-only change'
    $status=Invoke-TestEngine Status 1
    Assert-Test ($status.reproducible -and -not $status.current) 'Coverage-only change invalidates freshness'
    $null=Invoke-TestEngine Update
    Save-TestCommit 'coverage report'
    $null=Invoke-TestEngine Status
    $snapshot=Get-Content 'docs/project-statistics/snapshot.json' -Raw | ConvertFrom-Json -AsHashtable
    $snapshot.measurement.totalTextLines=999
    Write-TestFile 'docs/project-statistics/snapshot.json' (($snapshot|ConvertTo-Json -Depth 30)+"`n")
    $status=Invoke-TestEngine Status 1
    Assert-Test (-not $status.reproducible) 'Snapshot metrics tampering detected'
    $null=Invoke-TestGit @('config','remote.fixture.promisor','true')
    $null=Invoke-TestEngine Status 2
    $null=Invoke-TestGit @('config','--unset','remote.fixture.promisor')
    if(-not $IsWindows){
        $bashPath=(Get-Command bash).Source
        $dirnamePath=(Get-Command dirname).Source
        $fakeBin=Join-Path $root 'fake-tools'
        $null=New-Item -ItemType Directory -Path $fakeBin
        $null=New-Item -ItemType SymbolicLink -Path (Join-Path $fakeBin 'dirname') -Target $dirnamePath
        $savedPath=$env:PATH
        try {
            $env:PATH=$fakeBin
            $failure=& $bashPath $wrapper status --repo $root 2>&1
            Assert-Test ($LASTEXITCODE -eq 2 -and ($failure -join ' ').Contains('PowerShell 7')) 'Missing PowerShell blocks before measurement'
        } finally { $env:PATH=$savedPath }
        Write-TestFile 'fake-tools/pwsh' "#!/bin/sh`necho 6`n"
        & chmod +x (Join-Path $fakeBin 'pwsh')
        try {
            $env:PATH=$fakeBin
            $failure=& $bashPath $wrapper status --repo $root 2>&1
            Assert-Test ($LASTEXITCODE -eq 2 -and ($failure -join ' ').Contains('PowerShell 7')) 'Old PowerShell blocks before measurement'
        } finally { $env:PATH=$savedPath }
    }
    $shallow=Join-Path $root 'shallow'
    $null=Invoke-TestGit @('clone','-q','--no-local','--depth=1',$root,$shallow)
    Set-Location $shallow
    $null=Invoke-TestEngine Status 2
    Set-Location $root
    Write-Output "PASS: $assertions assertions; platform=$([Environment]::OSVersion.Platform); PowerShell=$($PSVersionTable.PSVersion)"
} finally {
    Set-Location $initial
    $env:GIT_AUTHOR_DATE=$savedAuthor; $env:GIT_COMMITTER_DATE=$savedCommitter
    if($Keep){Write-Output "Fixture retained: $root"}elseif(Test-Path -LiteralPath $root){Remove-Item -LiteralPath $root -Recurse -Force}
}
