#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Prueft den Live-Smoke-Test offline. EN: Verifies the live smoke test offline.
.DESCRIPTION
    DE: Verwendet nur temporaere PowerShell-Kindprozesse und synthetische
    Profile. Keine Harnesses, Modelle, Konten oder Netzwerkzugriffe.
    EN: Uses only temporary PowerShell child processes and synthetic profiles.
    No harnesses, models, accounts or network access.
.EXAMPLE
    pwsh -NoProfile -File tests/test-live-smoke-contract.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$liveRoot = Join-Path $PSScriptRoot 'live'
$temporary = Join-Path ([IO.Path]::GetTempPath()) "model-routing-smoke-contract-$([guid]::NewGuid().ToString('N'))"
$script:checks = 0
function Assert-SmokeContract {
    param([bool] $Condition, [string] $Message)
    if (-not $Condition) { throw "Live smoke contract: $Message" }
    $script:checks++
}
try {
    [void](New-Item -ItemType Directory -Path $temporary)
    Import-Module (Join-Path $liveRoot 'model-routing-smoke-core.psm1') -Force
    $defaultsPath = Join-Path $liveRoot 'model-routing-smoke.defaults.json'
    $childScript = Join-Path $liveRoot 'invoke-smoke-child.ps1'
    $configuration = Read-MRSmokeConfiguration $defaultsPath
    Assert-SmokeContract ($configuration.harnesses.Count -eq 5) 'five ordered harnesses'
    Assert-SmokeContract ($configuration.maxRetriesUpperBound -eq 3) 'retry upper bound'
    Assert-SmokeContract ($configuration.sdk.claude.version -ceq '0.3.318' -and $configuration.sdk.copilot.version -ceq '1.0.28') 'pinned SDK versions'
    Assert-SmokeContract ($configuration.task.expectedOutput -ceq 'MODEL_ROUTING_OK') 'exact task output'
    Assert-SmokeContract (Test-MRSmokeRequired @('scripts/adapters/new-harnesses.ps1') $configuration.triggerPaths) 'adapter change requires smoke test'
    Assert-SmokeContract (Test-MRSmokeRequired @('tests/live/model-routing-smoke.defaults.json') $configuration.triggerPaths) 'matrix change requires smoke test'
    Assert-SmokeContract (-not (Test-MRSmokeRequired @('docs/user-guide.md') $configuration.triggerPaths)) 'documentation-only change does not require smoke test'
    $ignored = @(& git -C $root check-ignore '.test-results/model-routing-smoke/fixture.json' 'tests/live/model-routing-smoke.local.json')
    Assert-SmokeContract ($LASTEXITCODE -eq 0 -and $ignored.Count -eq 2) 'local report and selection paths are git-ignored'

    $overridePath = Join-Path $temporary 'selection.json'
    @{schemaVersion='1.0';selections=@{Copilot=@{frontierModel='fixture-frontier';fastModel='fixture-fast'}}} | ConvertTo-Json -Depth 10 | Set-Content $overridePath
    $overridden = Merge-MRSmokeSelectionOverride $configuration $overridePath
    $copilot = @($overridden.harnesses | Where-Object family -ceq 'Copilot')[0]
    Assert-SmokeContract ($copilot.frontierModel -ceq 'fixture-frontier' -and $copilot.fastModel -ceq 'fixture-fast') 'local exact-selection override'

    $argumentFixture = Join-Path $temporary 'argument-fixture.ps1'
    @'
[CmdletBinding()]
param([Parameter(Mandatory)][AllowEmptyString()][string] $Value)
"$($PWD.Path)|$env:PWD|$($Value.Length)"
'@ | Set-Content $argumentFixture
    $working = Join-Path $temporary 'working directory'
    [void](New-Item -ItemType Directory -Path $working)
    $child = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' `
        -Arguments @('-NoProfile','-File',$argumentFixture,'') -WorkingDirectory $working -TimeoutSeconds 10
    $parts = $child.stdout.Trim() -split '\|'
    Assert-SmokeContract ($child.exitCode -eq 0 -and $parts.Count -eq 3) 'JSON child argument transport'
    $workingSuffix = $working.Substring($temporary.Length)
    Assert-SmokeContract ($parts[0].EndsWith($workingSuffix,[StringComparison]::Ordinal) -and
        $parts[1] -ceq (Resolve-MRSmokePhysicalPath $working) -and $parts[2] -ceq '0') 'PWD and empty argument preservation'
    $env:MODEL_ROUTING_SMOKE_TEST_SECRET = 'must-not-reach-child'
    try {
        $startInfo = New-MRSmokeProcessStartInfo -ChildScript $childScript -Executable 'pwsh' -Arguments @('-NoProfile') `
            -WorkingDirectory $working -RemovedEnvironment @('MODEL_ROUTING_SMOKE_TEST_SECRET')
        Assert-SmokeContract (-not $startInfo.Environment.ContainsKey('MODEL_ROUTING_SMOKE_TEST_SECRET')) 'sensitive child environment removal'
    } finally { Remove-Item Env:MODEL_ROUTING_SMOKE_TEST_SECRET -ErrorAction SilentlyContinue }

    $sentinel = Join-Path $temporary 'forbidden-sentinel.txt'
    $grandchild = Join-Path $temporary 'grandchild.ps1'
    "Start-Sleep -Seconds 2; Set-Content -LiteralPath '$($sentinel.Replace("'","''"))' -Value forbidden" | Set-Content $grandchild
    $parent = Join-Path $temporary 'parent.ps1'
    "`$process = Start-Process pwsh -ArgumentList @('-NoProfile','-File','$($grandchild.Replace("'","''"))') -PassThru; Start-Sleep -Seconds 10" | Set-Content $parent
    $timedOut = $false
    try {
        $null = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' -Arguments @('-NoProfile','-File',$parent) `
            -WorkingDirectory $working -TimeoutSeconds 0.5
    } catch { $timedOut = $_.Exception.Message -like 'TIMEOUT:*' }
    Start-Sleep -Seconds 3
    Assert-SmokeContract $timedOut 'bounded process timeout'
    Assert-SmokeContract (-not (Test-Path $sentinel)) 'timeout kills the descendant process tree'

    $pipeFixture = Join-Path $temporary 'inherited-pipe.ps1'
    @'
$start = [Diagnostics.ProcessStartInfo]::new('pwsh')
$start.UseShellExecute = $false
foreach ($argument in @('-NoProfile','-Command','Start-Sleep -Seconds 3')) { [void]$start.ArgumentList.Add($argument) }
$child = [Diagnostics.Process]::Start($start)
$child.Dispose()
'@ | Set-Content $pipeFixture
    $pipeWatch = [Diagnostics.Stopwatch]::StartNew()
    $pipeTimeout = $false
    try {
        $null = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' -Arguments @('-NoProfile','-File',$pipeFixture) `
            -WorkingDirectory $working -TimeoutSeconds 1
    } catch { $pipeTimeout = $_.Exception.Message -like '*TIMEOUT:*' }
    Assert-SmokeContract ($pipeTimeout -and $pipeWatch.Elapsed.TotalSeconds -lt 3) 'inherited pipe drain obeys deadline'
    Start-Sleep -Seconds 3

    $baseProfile = @{model='fixture-model';reasoningEffort='high'}
    $agy = $baseProfile.Clone();$agy.arguments=@('--model','{model}','--effort','{reasoningEffort}','--mode','accept-edits','--print','{prompt}')
    $agyArguments = @(Expand-MRSmokeProfileArguments $agy $working (Join-Path $temporary 'agy.json') 'Antigravity' 'fixture prompt')
    Assert-SmokeContract ('--settings' -cin $agyArguments -and $agyArguments[-1] -ceq 'fixture prompt') 'agy session permission and prompt'
    $settingsIndex = [Array]::IndexOf([object[]]$agyArguments,'--settings')
    $agyPermissions = ($agyArguments[$settingsIndex + 1] | ConvertFrom-Json).permissions.allow
    Assert-SmokeContract ($agyPermissions.Count -eq 2 -and 'command(*)' -cin $agyPermissions -and
        "write_file($(Resolve-MRSmokePhysicalPath $working))" -cin $agyPermissions -and 'write_file(*)' -cnotin $agyPermissions) 'agy write permission is scoped to physical worktree'
    if (-not $IsWindows) {
        $alias = Join-Path $temporary 'directory alias'
        [void](New-Item -ItemType SymbolicLink -Path $alias -Target $working)
        Assert-SmokeContract ((Resolve-MRSmokePhysicalPath $alias) -ceq (Resolve-MRSmokePhysicalPath $working)) 'directory alias resolves to physical path'
        $aliasedStart = New-MRSmokeProcessStartInfo -ChildScript $childScript -Executable pwsh -Arguments @('-NoProfile') -WorkingDirectory $alias -Environment @{PWD='wrong'}
        Assert-SmokeContract ($aliasedStart.WorkingDirectory -ceq $aliasedStart.Environment['PWD'] -and
            $aliasedStart.WorkingDirectory -ceq (Resolve-MRSmokePhysicalPath $working)) 'physical process directory and PWD cannot diverge'
    }
    foreach ($family in @('Claude','Copilot','OpenCode')) {
        $runnerProfile = $baseProfile.Clone()
        $harnessArguments = switch ($family) {
            Claude { @('--print','--model','{model}','--permission-mode','acceptEdits','{prompt}') }
            Copilot { @('--model','{model}','--output-format','json','--prompt','{prompt}') }
            OpenCode { @('run','--model','{model}#high','--agent','build','{prompt}') }
        }
        $runnerProfile.arguments = @('-NoProfile','-File','adapter.ps1','-OutputFile','{outputFile}','-HarnessArgumentsJson',(ConvertTo-Json -InputObject $harnessArguments -Compress))
        $expanded = @(Expand-MRSmokeProfileArguments $runnerProfile $working (Join-Path $temporary "$family.json") $family 'fixture prompt')
        $jsonIndex = [Array]::IndexOf([object[]]$expanded,'-HarnessArgumentsJson')
        $decoded = @($expanded[$jsonIndex + 1] | ConvertFrom-Json)
        Assert-SmokeContract ($decoded -ccontains 'fixture prompt') "$family prompt survives JSON transport"
        if ($family -eq 'Claude') { Assert-SmokeContract ('--allowedTools' -cin $decoded -and '--' -cin $decoded) 'Claude session permissions' }
        if ($family -eq 'Copilot') { Assert-SmokeContract ('--trust-folder' -cin $decoded -and '--allow-tool=shell' -cin $decoded) 'Copilot session permissions' }
        if ($family -eq 'OpenCode') { Assert-SmokeContract ('--auto' -cin $decoded) 'OpenCode session permission' }
    }

    Assert-SmokeContract ((Get-MRSmokeFinalStatus @(@{attempt=1;status='PASS'})) -ceq 'PASS') 'first-pass result'
    Assert-SmokeContract ((Get-MRSmokeFinalStatus @(@{attempt=1;status='FAILED'},@{attempt=2;status='PASS'})) -ceq 'PASS_WITH_RETRY') 'retry result'
    Assert-SmokeContract ((Get-MRSmokeFinalStatus @(@{attempt=1;status='BLOCKED'})) -ceq 'BLOCKED') 'blocked result'
    Assert-SmokeContract ((Get-MRSmokeFinalStatus @(@{attempt=1;status='TIMEOUT'})) -ceq 'TIMEOUT') 'timeout result'
    Assert-SmokeContract ((Get-MRSmokeFinalStatus @(@{attempt=1;status='INFRA_ERROR'})) -ceq 'INFRA_ERROR') 'infrastructure result'

    $exitFixture = Join-Path $temporary 'exit-131.ps1'
    @'
[Console]::Error.WriteLine("password=super-secret Bearer abcdefghijk " + $PWD.Path)
exit 131
'@ | Set-Content $exitFixture
    $exitResult = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' `
        -Arguments @('-NoProfile','-File',$exitFixture) -WorkingDirectory $working -TimeoutSeconds 10
    $processDiagnostic = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $exitResult -RedactValues @('super-secret') `
        -PathReplacements @{$working='<WORKTREE>'}
    Assert-SmokeContract ($processDiagnostic.exitCode -eq 131) 'synthetic exit 131 is retained'
    Assert-SmokeContract (-not $processDiagnostic.stderr.Contains('super-secret') -and
        -not $processDiagnostic.stderr.Contains('abcdefghijk') -and $processDiagnostic.stderr.Contains('<WORKTREE>')) 'diagnostic stderr redaction'
    $truncatedText = ConvertTo-MRSmokeDiagnosticText -Text ('x' * 20000) -MaxBytes 1024
    Assert-SmokeContract ($truncatedText.Contains('<truncated:originalBytes=20000>')) 'diagnostic text size limit'

    $projectRoot = Join-Path $temporary 'diagnostic-project'
    [void](New-Item -ItemType Directory -Path (Join-Path $projectRoot '.git') -Force)
    [void](New-Item -ItemType Directory -Path (Join-Path $projectRoot 'obj') -Force)
    $binRoot = Join-Path $projectRoot 'bin/Debug/net10.0'
    [void](New-Item -ItemType Directory -Path $binRoot -Force)
    Set-Content (Join-Path $projectRoot 'App.csproj') '<Project Sdk="Microsoft.NET.Sdk"></Project>'
    Set-Content (Join-Path $projectRoot 'Program.cs') 'Console.WriteLine("MODEL_ROUTING_OK");'
    Set-Content (Join-Path $projectRoot '.git/config') 'secret-token=forbidden'
    [void](New-Item -ItemType Directory -Path (Join-Path $projectRoot '.dotnet/lockfiles/global') -Force)
    $sdkLockPath = Join-Path $projectRoot '.dotnet/lockfiles/global/active.server'
    Set-Content $sdkLockPath 'excluded-sdk-cache'
    Set-Content (Join-Path $projectRoot 'obj/assets.json') 'excluded'
    Set-Content (Join-Path $binRoot 'App.runtimeconfig.json') '{"runtimeOptions":{"tfm":"net10.0"}}'
    Set-Content (Join-Path $binRoot 'App.deps.json') '{"runtimeTarget":{"name":"fixture"}}'
    [IO.File]::WriteAllBytes((Join-Path $binRoot 'App'),[byte[]](0..255))
    $sdkLock = [IO.File]::Open($sdkLockPath,[IO.FileMode]::Open,[IO.FileAccess]::ReadWrite,[IO.FileShare]::None)
    try {
        $projectDiagnostics = Get-MRSmokeProjectDiagnostics -Worktree $projectRoot -RedactValues @('forbidden') -PathReplacements @{$projectRoot='<WORKTREE>'}
    } finally { $sdkLock.Dispose() }
    $projectPaths = @($projectDiagnostics.files | ForEach-Object { [string]$_.path })
    Assert-SmokeContract ('App.csproj' -cin $projectPaths -and 'Program.cs' -cin $projectPaths) 'project source metadata'
    Assert-SmokeContract ('bin/Debug/net10.0/App.runtimeconfig.json' -cin $projectPaths -and
        'bin/Debug/net10.0/App.deps.json' -cin $projectPaths) 'selected runtime metadata'
    Assert-SmokeContract (@($projectPaths | Where-Object { $_ -like '.git/*' -or $_ -like '.dotnet/*' -or $_ -like 'obj/*' -or $_ -ceq 'bin/Debug/net10.0/App' }).Count -eq 0) 'excluded project artifacts and SDK cache'
    Assert-SmokeContract (@($projectDiagnostics.files | Where-Object { $_.path -ceq 'App.csproj' -and $_.content.Contains('Microsoft.NET.Sdk') }).Count -eq 1) 'bounded project content'

    $emptyProject = Join-Path $temporary 'no-project'
    [void](New-Item -ItemType Directory -Path $emptyProject)
    Set-Content (Join-Path $emptyProject 'NuGet.Config') '<configuration />'
    $emptyDiagnostics = Get-MRSmokeProjectDiagnostics -Worktree $emptyProject
    Assert-SmokeContract ($emptyDiagnostics.files.Count -eq 1 -and @($emptyDiagnostics.files | Where-Object { $_.path -like '*.csproj' }).Count -eq 0) 'no-project inventory is retained'

    $largeFiles = 1..10 | ForEach-Object { [ordered]@{path="Program$_.cs";size=40000;sha256=('a' * 64);content=('y' * 32768)} }
    $largeDiagnostics = [ordered]@{processes=[ordered]@{};project=[ordered]@{files=@($largeFiles);truncated=$false}}
    $limitedDiagnostics = Limit-MRSmokeAttemptDiagnostics -Diagnostics $largeDiagnostics
    Assert-SmokeContract ([Text.Encoding]::UTF8.GetByteCount(($limitedDiagnostics | ConvertTo-Json -Depth 50 -Compress)) -le 131072) 'per-attempt diagnostic limit'
    Remove-Item -LiteralPath $projectRoot -Recurse -Force
    Assert-SmokeContract (($projectDiagnostics | ConvertTo-Json -Depth 20).Contains('App.csproj')) 'diagnostics survive worktree cleanup'

    if (-not $IsWindows) {
        $fifoRoot = Join-Path $temporary 'fifo-project'
        [void](New-Item -ItemType Directory -Path $fifoRoot)
        & mkfifo (Join-Path $fifoRoot 'clr-debug-pipe-fixture')
        if ($LASTEXITCODE -ne 0) { throw 'FIFO fixture preparation failed.' }
        $fifoScript = Join-Path $temporary 'fifo-inventory.ps1'
        @'
param($ModulePath,$Worktree)
Import-Module $ModulePath
$diagnostics = Get-MRSmokeProjectDiagnostics -Worktree $Worktree
if ($diagnostics.files.Count -ne 0) { exit 1 }
'@ | Set-Content $fifoScript
        $fifoResult = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' `
            -Arguments @('-NoProfile','-File',$fifoScript,(Join-Path $liveRoot 'model-routing-smoke-core.psm1'),$fifoRoot) `
            -WorkingDirectory $working -TimeoutSeconds 5
        Assert-SmokeContract ($fifoResult.exitCode -eq 0) 'real Unix FIFO is excluded without blocking'
    }

    $reportPath = Join-Path $temporary 'report/report.json'
    Write-MRSmokeJsonAtomic -Path $reportPath -Value @{schemaVersion='1.1';testId='model-routing-live-smoke';finalStatus='PASS'} -Confirm:$false
    $report = Get-Content $reportPath -Raw | ConvertFrom-Json -AsHashtable
    Assert-SmokeContract ($report.schemaVersion -ceq '1.1' -and $report.finalStatus -ceq 'PASS') 'atomic report JSON'
    Assert-SmokeContract ((Read-MRSmokeReport $reportPath).schemaVersion -ceq '1.1') 'current report schema readability'
    $historicalReport = Join-Path $temporary 'report/historical-1.0.json'
    @{schemaVersion='1.0';testId='model-routing-live-smoke';finalStatus='FAILED'} | ConvertTo-Json | Set-Content $historicalReport
    Assert-SmokeContract ((Read-MRSmokeReport $historicalReport).schemaVersion -ceq '1.0') 'historical report schema readability'

    $partialHarnesses = [Collections.Generic.List[object]]::new()
    $partialReport = [ordered]@{harnesses=@()}
    $partialHarnesses.Add([ordered]@{harness='agy';finalStatus='FAILED';attempts=@(@{attempt=1;status='FAILED'})})
    $partialReport.harnesses = @($partialHarnesses)
    Write-MRSmokeJsonAtomic -Path $reportPath -Value $partialReport -Confirm:$false
    try { throw 'synthetic later harness failure' } catch { $null = $_ }
    $savedPartialReport = Get-Content -LiteralPath $reportPath -Raw | ConvertFrom-Json -AsHashtable
    Assert-SmokeContract ($savedPartialReport.harnesses.Count -eq 1 -and $savedPartialReport.harnesses[0].harness -ceq 'agy') 'atomic completed harness evidence survives later failure on disk'

    $gate = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' `
        -Arguments @('-NoProfile','-File',(Join-Path $liveRoot 'invoke-model-routing-smoke.ps1')) `
        -WorkingDirectory $root -TimeoutSeconds 15
    Assert-SmokeContract ($gate.exitCode -ne 0 -and $gate.stderr.Contains('-ConfirmLiveRun')) 'live authorization gate'
    "PASS: $script:checks live-smoke contract assertions; offline."
} finally {
    Remove-Item -LiteralPath $temporary -Recurse -Force -ErrorAction SilentlyContinue
}
