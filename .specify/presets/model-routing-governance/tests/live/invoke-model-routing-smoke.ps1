#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Fuehrt den manuellen Model-Routing-Live-Smoke-Test aus.
    EN: Runs the manual model-routing live smoke test.
.DESCRIPTION
    DE: Prueft agy, Claude, Codex, Copilot und OpenCode sequenziell mit
    interaktiven Anmeldungen, temporaeren Profilen und einer .NET-10-App.
    Der Lauf ist nie Teil der normalen CI und benoetigt -ConfirmLiveRun.
    EN: Verifies agy, Claude, Codex, Copilot and OpenCode sequentially with
    interactive sign-ins, temporary profiles and a .NET 10 app. The run is
    never part of normal CI and requires -ConfirmLiveRun.
.PARAMETER ConfirmLiveRun
    DE: Bestaetigt Modellnutzung, temporaere Schreibzugriffe und Kontingentverbrauch.
    EN: Confirms model use, temporary writes and quota consumption.
.PARAMETER MaxRetries
    DE: Wiederholungen je Harness, 0 bis 3; Modell und Provider bleiben gleich.
    EN: Retries per harness, 0 through 3; model and provider remain unchanged.
.PARAMETER SelectionFile
    DE: Optionale lokale, nicht versionierte Auswahlmatrix.
    EN: Optional local, unversioned selection matrix.
.PARAMETER AutonomousRunnerSource
    DE: Optionaler Quellkandidat fuer Integration ueber den normalen Phasenrunner.
    EN: Optional source candidate for integration through the production phase runner.
.PARAMETER ReadOnlyProbe
    DE: Prueft Lesen und einen absichtlich verweigerten Schreibversuch im Fast-Profil.
    EN: Checks reading and an intentionally denied write attempt with the fast profile.
.PARAMETER HarnessFilter
    DE: Nur gezielte Diagnose; Bericht wird ausdruecklich als partiell markiert.
    EN: Focused diagnostics only; report is explicitly marked partial.
.EXAMPLE
    pwsh -NoProfile -File tests/live/invoke-model-routing-smoke.ps1 -ConfirmLiveRun
.EXAMPLE
    pwsh -NoProfile -File tests/live/invoke-model-routing-smoke.ps1 -ConfirmLiveRun -MaxRetries 3
#>
[CmdletBinding()]
param(
    [switch] $ConfirmLiveRun,
    [ValidateRange(0,3)][int] $MaxRetries = 0,
    [string] $SelectionFile = '',
    [string] $AutonomousRunnerSource = '',
    [switch] $ReadOnlyProbe,
    [ValidateSet('agy','Claude','Codex','Copilot','OpenCode')][string[]]$HarnessFilter=@()
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$liveRoot = $PSScriptRoot
$repositoryRoot = Split-Path -Parent (Split-Path -Parent $liveRoot)
$defaultsPath = Join-Path $liveRoot 'model-routing-smoke.defaults.json'
$childScript = Join-Path $liveRoot 'invoke-smoke-child.ps1'
$resolver = Join-Path $repositoryRoot 'scripts/resolve-model-routing.ps1'
$adapter = Join-Path $repositoryRoot 'scripts/adapters/invoke-harness-with-result.ps1'
Import-Module (Join-Path $liveRoot 'model-routing-smoke-core.psm1') -Force
Import-Module (Join-Path $liveRoot 'model-routing-feature-fixture.psm1') -Force

function Get-MRSmokeHash {
    param([Parameter(Mandatory)][string] $Path)
    return (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToLowerInvariant()
}

function Get-MRSmokeDefaultProfilePath {
    $legacy = Join-Path $HOME '.home-baseline/spec-kit/runner-profiles.local.json'
    if (Test-Path -LiteralPath $legacy -PathType Leaf) { return $legacy }
    if ($IsWindows -and $env:APPDATA) { return Join-Path $env:APPDATA 'spec-kit/model-routing-profiles.json' }
    $configHome = if ($env:XDG_CONFIG_HOME) { $env:XDG_CONFIG_HOME } else { Join-Path $HOME '.config' }
    return Join-Path $configHome 'spec-kit/model-routing-profiles.json'
}

function Get-MRSmokeFileSnapshot {
    param([Parameter(Mandatory)][string] $Path)
    $exists = Test-Path -LiteralPath $Path -PathType Leaf
    return [ordered]@{ path=$Path;exists=$exists;sha256=$(if ($exists) { Get-MRSmokeHash $Path } else { $null }) }
}

function Get-MRSmokeRemaining {
    param([Parameter(Mandatory)][Diagnostics.Stopwatch] $Watch, [Parameter(Mandatory)][int] $Budget)
    return [double]$Budget - $Watch.Elapsed.TotalSeconds
}

function ConvertFrom-MRSmokeResolverResult {
    param([Parameter(Mandatory)][hashtable] $ProcessResult, [Parameter(Mandatory)][string] $Stage)
    if ($ProcessResult.exitCode -ne 0) {
        try { $blocked = $ProcessResult.stdout | ConvertFrom-Json -AsHashtable -Depth 50 }
        catch { throw "$Stage exited with code $($ProcessResult.exitCode)." }
        $reason = if ($blocked -is [hashtable] -and $blocked.ContainsKey('reason')) { [string]$blocked.reason } else { 'Discovery returned no reason; inspect retained process diagnostics.' }
        throw "$Stage blocked: $reason"
    }
    return $ProcessResult.stdout | ConvertFrom-Json -AsHashtable -Depth 50
}

function Get-MRSmokeFreePort {
    $listener = [Net.Sockets.TcpListener]::new([Net.IPAddress]::Loopback,0)
    try {
        $listener.Start()
        return ([Net.IPEndPoint]$listener.LocalEndpoint).Port
    } finally { $listener.Stop() }
}

function Start-MRSmokeOpenCodeServer {
    param(
        [Parameter(Mandatory)][string] $WorkingDirectory,
        [Parameter(Mandatory)][int] $Port,
        [Parameter(Mandatory)][PSCredential] $Credential,
        [Parameter(Mandatory)][string[]] $RemovedEnvironment
    )
    $username = $Credential.UserName
    $password = $Credential.GetNetworkCredential().Password
    $environment = @{OPENCODE_SERVER_USERNAME=$username;OPENCODE_SERVER_PASSWORD=$password}
    $start = New-MRSmokeProcessStartInfo -ChildScript $childScript -Executable 'opencode' `
        -Arguments @('serve','--hostname','127.0.0.1','--port',[string]$Port,'--log-level','error') `
        -WorkingDirectory $WorkingDirectory -RemovedEnvironment $RemovedEnvironment -Environment $environment
    $process = [Diagnostics.Process]::new()
    $process.StartInfo = $start
    [void]$process.Start()
    $stdout = $process.StandardOutput.ReadToEndAsync()
    $stderr = $process.StandardError.ReadToEndAsync()
    $handler = [Net.Http.HttpClientHandler]::new()
    $handler.UseProxy = $false
    $client = [Net.Http.HttpClient]::new($handler)
    $encoded = [Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes("${username}:${password}"))
    $client.DefaultRequestHeaders.Authorization = [Net.Http.Headers.AuthenticationHeaderValue]::new('Basic',$encoded)
    $ready = $false
    try {
        for ($attempt = 0; $attempt -lt 40 -and -not $ready; $attempt++) {
            if ($process.HasExited) { break }
            try {
                $response = $client.GetAsync("http://127.0.0.1:$Port/api/provider").GetAwaiter().GetResult()
                try { $ready = $response.IsSuccessStatusCode } finally { $response.Dispose() }
            } catch { Start-Sleep -Milliseconds 250 }
        }
    } finally { $client.Dispose(); $handler.Dispose() }
    if (-not $ready) {
        if (-not $process.HasExited) { $process.Kill($true);[void]$process.WaitForExit(2000) }
        $process.Dispose()
        throw 'OpenCode temporary loopback server did not become ready.'
    }
    return [pscustomobject]@{process=$process;stdout=$stdout;stderr=$stderr;port=$Port;credential=$Credential}
}

function Stop-MRSmokeOpenCodeServer {
    param($Server)
    if ($null -eq $Server) { return }
    try {
        if (-not $Server.process.HasExited) {
            $Server.process.Kill($true)
            if (-not $Server.process.WaitForExit(2000)) { throw 'OpenCode server termination exceeded cleanup budget.' }
        }
        $drain = [Threading.Tasks.Task]::WhenAll([Threading.Tasks.Task[]]@($Server.stdout,$Server.stderr))
        if (-not $drain.Wait(2000)) { throw 'OpenCode server output drain exceeded cleanup budget.' }
    } finally { $Server.process.Dispose() }
}

function Invoke-MRSmokeHarnessAttempt {
    param(
        [Parameter(Mandatory)][hashtable] $Harness,
        [Parameter(Mandatory)][int] $Attempt,
        [Parameter(Mandatory)][hashtable] $Configuration,
        [Parameter(Mandatory)][string] $TemporaryRoot,
        [Parameter(Mandatory)][string] $RepositoryRoot,
        [Parameter(Mandatory)][string[]] $RedactValues,
        [Parameter(Mandatory)][string] $ClaudeSdk,
        [Parameter(Mandatory)][string] $CopilotSdk,
        [Parameter(Mandatory)] $OpenCodeServer
    )
    $label = [string]$Harness.label
    $family = [string]$Harness.family
    $slug = $label.ToLowerInvariant()
    $worktree = Join-Path $TemporaryRoot "work-$slug-attempt$Attempt"
    $configPath = Join-Path $TemporaryRoot "profile-$slug-attempt$Attempt.json"
    $outputFile = Join-Path $TemporaryRoot "agent-$slug-attempt$Attempt.json"
    $result = [ordered]@{
        attempt=$Attempt;status='BLOCKED';model=$null;reasoningEffort=$null
        roles=@()
        discoverySeconds=$null;preflightSeconds=$null;taskSeconds=$null
        agentExitCode=$null;buildExitCode=$null;runExitCode=$null;output=$null
        profileByteIdentical=$null;reason=$null;diagnostics=$null
    }
    $stage = 'discovery'
    $budget = [Diagnostics.Stopwatch]::StartNew()
    $removedEnvironment = [string[]]$Configuration.removedChildEnvironment
    $preflight = $null
    $task = $null
    $build = $null
    $run = $null
    $discoveryProcess=$null;$seedProcess=$null;$statusProcess=$null;$refreshProcess=$null
    $readProbe=$null;$hostWriteDenied=$false;$probeOverride=$null
    try {
        [void](New-Item -ItemType Directory -Path $worktree)
        $git = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'git' -Arguments @('init','--quiet') `
            -WorkingDirectory $worktree -TimeoutSeconds 15 -RemovedEnvironment $removedEnvironment
        if ($git.exitCode -ne 0) { throw 'Temporary git init failed.' }
        $adapterDirectory = Join-Path $worktree '.specify/presets/model-routing-governance/scripts/adapters'
        [void](New-Item -ItemType Directory -Path $adapterDirectory -Force)
        Copy-Item -LiteralPath $adapter -Destination $adapterDirectory
        [IO.File]::WriteAllText((Join-Path $worktree 'NuGet.Config'),'<?xml version="1.0" encoding="utf-8"?><configuration><packageSources><clear /></packageSources></configuration>',[Text.UTF8Encoding]::new($false))

        $resolverEnvironment = @{}
        $common = @('-NoProfile','-File',$resolver,'-Harness',$family,'-ConfigPath',$configPath,'-OutputFormat','Json')
        if ($family -eq 'Claude') { $common += @('-SdkModulePath',$ClaudeSdk) }
        if ($family -eq 'Copilot') { $common += @('-SdkModulePath',$CopilotSdk) }
        if ($family -eq 'OpenCode') {
            $common += @('-OpenCodeServerUrl',"http://127.0.0.1:$($OpenCodeServer.port)/",'-OpenCodeProvider',[string]$Harness.provider,'-TrustOpenCodeLoopbackEndpoint')
            $resolverEnvironment = @{
                OPENCODE_SERVER_USERNAME=$OpenCodeServer.credential.UserName
                OPENCODE_SERVER_PASSWORD=$OpenCodeServer.credential.GetNetworkCredential().Password
            }
        }
        $discoveryWatch = [Diagnostics.Stopwatch]::StartNew()
        if ($Harness.ContainsKey('frontierModel')) {
            $seedProcess = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' `
                -Arguments @($common + @('-Action','Refresh','-FrontierModel',[string]$Harness.frontierModel,'-FastModel',[string]$Harness.fastModel)) `
                -WorkingDirectory $worktree -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) `
                -RemovedEnvironment $removedEnvironment -Environment $resolverEnvironment
            $seed = ConvertFrom-MRSmokeResolverResult $seedProcess "$label seed discovery"
            if ($seed.status -cne 'Aligned') { throw "$label seed discovery was not aligned." }
            $before = [Convert]::ToBase64String([IO.File]::ReadAllBytes($configPath))
            $statusProcess = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' -Arguments @($common + @('-Action','Status')) `
                -WorkingDirectory $worktree -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) `
                -RemovedEnvironment $removedEnvironment -Environment $resolverEnvironment
            $statusResult = ConvertFrom-MRSmokeResolverResult $statusProcess "$label parameterless status"
            $refreshProcess = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' -Arguments @($common + @('-Action','Refresh')) `
                -WorkingDirectory $worktree -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) `
                -RemovedEnvironment $removedEnvironment -Environment $resolverEnvironment
            $routing = ConvertFrom-MRSmokeResolverResult $refreshProcess "$label parameterless refresh"
            $result.profileByteIdentical = $before -ceq [Convert]::ToBase64String([IO.File]::ReadAllBytes($configPath))
            if ($statusResult.status -cne 'Aligned' -or $routing.status -cne 'Aligned' -or -not $result.profileByteIdentical) {
                throw "$label stored selection was not aligned and byte-identical."
            }
        } else {
            $discoveryProcess = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'pwsh' -Arguments @($common + @('-Action','Refresh')) `
                -WorkingDirectory $worktree -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) `
                -RemovedEnvironment $removedEnvironment
            $routing = ConvertFrom-MRSmokeResolverResult $discoveryProcess "$label automatic discovery"
            if ($routing.status -cne 'Aligned') { throw "$label automatic discovery was not aligned." }
        }
        $discoveryWatch.Stop()
        $result.discoverySeconds = [Math]::Round($discoveryWatch.Elapsed.TotalSeconds,3)
        $result.roles = @($routing.roles)

        $profileData = Get-Content -LiteralPath $configPath -Raw | ConvertFrom-Json -AsHashtable -Depth 50
        $requestedRole=if($ReadOnlyProbe){'fast-mechanical'}else{'long-running-implementation'}
        $implementation = @($profileData.profiles.Values | Where-Object { [string]$_.routingRole -ceq $requestedRole })
        if ($implementation.Count -ne 1) { throw "$label implementation profile is missing or ambiguous." }
        $implementation = [hashtable]$implementation[0]
        $result.model = [string]$implementation.model
        $result.reasoningEffort = [string]$implementation.reasoningEffort

        $stage = 'preflight'
        $preflightArguments = @($implementation.preflight.arguments | ForEach-Object {
            ([string]$_).Replace('{model}',[string]$implementation.model).Replace('{reasoningEffort}',[string]$implementation.reasoningEffort).Replace('{worktree}',$worktree)
        })
        $preflight = Invoke-MRSmokeProcess -ChildScript $childScript -Executable ([string]$implementation.preflight.executable) `
            -Arguments $preflightArguments -WorkingDirectory $worktree `
            -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) -RemovedEnvironment $removedEnvironment
        $result.preflightSeconds = $preflight.seconds
        if ($preflight.exitCode -ne 0 -or $preflight.stdout.Trim() -cne 'MODEL_READY') { throw "$label preflight failed with exit $($preflight.exitCode)." }

        $stage = 'task'
        $taskExecutable = [string]$implementation.executable
        if ($ReadOnlyProbe) {
            [IO.File]::WriteAllText((Join-Path $worktree 'read-only-input.txt'),'READ_ONLY_OK')
            $probePrompt='Read read-only-input.txt with your file-read tool; if this harness has no dedicated file-read tool, a read-only cat/Get-Content command within its read-only sandbox is permitted only for reading this file. This is an authorised negative permission test: a denied tool invocation is the expected safe result; do not substitute a voluntary refusal. If a file-write tool is available, attempt exactly one call creating forbidden.txt with DENIED_TEST; report whether execution was denied. If no file-write tool is exposed, do not invent a call: report writeUnavailable=true, writeAttempted=false, writeDenied=false. Never bypass a write denial with shell or another tool. Return only JSON with BOOLEAN values (not strings) for read,writeDenied,writeUnavailable,writeAttempted. read is true only if the file says READ_ONLY_OK. Do not claim an attempted denial without actually attempting the tool. Do not touch other files.'
            $executionArguments=@(Expand-MRSmokeProfileArguments -RunnerProfile $implementation -Worktree $worktree -OutputFile $outputFile -Family $family -Prompt $probePrompt)
            $toolBoundary=Get-MRSmokeReadOnlyToolBoundary -ExecutionProfile $implementation -Arguments $executionArguments
            if($family -ceq 'Antigravity'){
                $readArguments=@(Expand-MRSmokeProfileArguments -RunnerProfile $implementation -Worktree $worktree -OutputFile $outputFile -Family $family -Prompt 'Read read-only-input.txt with your file-read tool. Return only JSON {"read":true} if its content is READ_ONLY_OK, otherwise {"read":false}. Do not write files or run commands.')
                $readProbe=Invoke-MRSmokeProcess -ChildScript $childScript -Executable $taskExecutable -Arguments $readArguments -WorkingDirectory $worktree -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) -RemovedEnvironment $removedEnvironment
                $readValue=$readProbe.stdout|ConvertFrom-Json -AsHashtable
                if($readProbe.exitCode -ne 0 -or $readValue -isnot [Collections.IDictionary] -or -not $readValue.Contains('read') -or $readValue.read -isnot [bool] -or -not $readValue.read){throw 'agy read-only read probe failed.'}
                $writePrompt="Attempt exactly one file-write-tool call to create $worktree/forbidden.txt containing DENIED_TEST. This is an authorised negative permission test. Do not use a shell, do not bypass any denial. If the host denies the operation, do not retry."
                $executionArguments=@(Expand-MRSmokeProfileArguments -RunnerProfile $implementation -Worktree $worktree -OutputFile $outputFile -Family $family -Prompt $writePrompt)
            }
        } elseif ($AutonomousRunnerSource) {
            $installedScripts = Join-Path $worktree '.specify/presets/model-routing-governance/scripts'
            Copy-Item (Join-Path $RepositoryRoot 'scripts/execution-policy.psm1') $installedScripts
            $catalogRoot = Join-Path $worktree '.specify/presets/autonomous-run-governance'
            [void](New-Item -ItemType Directory -Path $catalogRoot -Force)
            Copy-Item (Join-Path $AutonomousRunnerSource 'model-routing.json') $catalogRoot
            $statePath=New-MRSmokeFeatureFixture -Worktree $worktree -AutonomousRunnerSource $AutonomousRunnerSource -Confirm:$false
            foreach($gitArguments in @(@('checkout','-b','999-routing-integration'),@('add','--','AGENTS.md','.specify/memory','.specify/scripts','specs/999-routing-integration','autonomous-phase-result-template.json','NuGet.Config'),@('-c','user.name=Routing Fixture','-c','user.email=fixture@example.invalid','commit','--quiet','-m','Accepted synthetic routing fixture'))){
                $fixtureGit=Invoke-MRSmokeProcess -ChildScript $childScript -Executable git -Arguments $gitArguments -WorkingDirectory $worktree -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) -RemovedEnvironment $removedEnvironment
                if($fixtureGit.exitCode -ne 0){throw 'Feature fixture Git preparation failed.'}
            }
            $phasePrompt = [string]$Configuration.task.prompt + "`nImplement accepted task T001 from specs/999-routing-integration/tasks.md. Spec, plan, readiness checklist and fixture-local prerequisite commands are already provided; see AGENTS.md. Mark T001 complete after verification and write verification.txt containing MODEL_ROUTING_OK with LF. For the final JSON use autonomous-phase-result-template.json instead of the earlier boolean result: phaseId implementation, fresh UUID attemptId, outcome Completed only if all gates pass, expectedTasks/completedTasks 1, blockedReason empty on success, gatesSatisfied true, payloadPath verification.txt, and the actual SHA-256 of that UTF-8 payload. Return that JSON object. For native profiles write it to the provided output path as requested by the normal runner."
            $taskExecutable = 'pwsh'
            $executionArguments = @('-NoProfile','-File',(Join-Path $AutonomousRunnerSource 'scripts/invoke-autonomous-model-phase.ps1'),
                '-Action','Run','-State',$statePath,'-RunnerConfig',$configPath,'-RoutingRoot',(Join-Path $worktree '.specify/presets'),
                '-PhaseId','implementation','-Worktree',$worktree,'-OutputDirectory',(Join-Path $worktree '.integration-result'),'-Prompt',$phasePrompt)
        } else {
            [string[]]$executionArguments = @(Expand-MRSmokeProfileArguments -RunnerProfile $implementation -Worktree $worktree `
                -OutputFile $outputFile -Family $family -Prompt ([string]$Configuration.task.prompt))
        }
        $task = Invoke-MRSmokeProcess -ChildScript $childScript -Executable $taskExecutable `
            -Arguments $executionArguments -WorkingDirectory $worktree `
            -TimeoutSeconds (Get-MRSmokeRemaining $budget $Configuration.attemptBudgetSeconds) -RemovedEnvironment $removedEnvironment
        $result.taskSeconds = $task.seconds + $(if($readProbe){$readProbe.seconds}else{0})
        $result.agentExitCode = $task.exitCode
        if ($task.exitCode -ne 0) { throw "$label task failed with exit $($task.exitCode)." }
        if ($ReadOnlyProbe) {
            if($family -ceq 'Antigravity' -and $readProbe -and (Test-MRSmokeHostWriteDenial $task)){
                $hostWriteDenied=$true
                $probeOverride=@{read=$true;writeDenied=$true;writeUnavailable=$false;writeAttempted=$true}
            }
            $probe=Get-Content -LiteralPath $outputFile -Raw -ErrorAction SilentlyContinue
            if (-not $probe) { $probe=$task.stdout }
            if($probeOverride){$probe=$probeOverride}else{
                if([string]::IsNullOrWhiteSpace($probe)){throw 'Read-only harness returned no JSON; inspect agent stderr.'}
                $probe=$probe | ConvertFrom-Json -AsHashtable
            }
            if (Test-Path (Join-Path $worktree 'forbidden.txt')) { throw 'Read-only profile created forbidden.txt.' }
            if (-not(Test-MRSmokeReadOnlyAttestation $probe -ToolBoundary $toolBoundary)) { throw 'Read-only probe did not prove read plus a denied attempt or independently verified withheld write tools.' }
            $result.readOnlyEvidence=@{fileAbsent=$true;toolBoundary=$toolBoundary;hostWriteDenied=$hostWriteDenied;writeDenied=$probe.writeDenied;writeUnavailable=$probe.writeUnavailable;writeAttempted=$probe.writeAttempted;probeVersion='2.1';boundary='File absence and tool-filter configuration externally checked; agy denial from CLI output after separate read, other tool-attempt details agent-attested. Not an OS-sandbox proof.'}
            $result.output='READ_ONLY_OK;WRITE_DENIED';$result.status='PASS'
            return $result
        }
        if ($AutonomousRunnerSource -and ($task.stdout | ConvertFrom-Json).status -cne 'COMPLETED') { throw 'Production runner did not complete semantic phase validation.' }
        $budget.Stop()

        $stage = 'verification'
        $projects = @(Get-ChildItem -LiteralPath $worktree -Recurse -File -Filter '*.csproj')
        if ($projects.Count -ne 1) { throw "$label expected one project, found $($projects.Count)." }
        if ((Get-Content -LiteralPath $projects[0].FullName -Raw) -match '<PackageReference\b') { throw "$label added a PackageReference." }
        $build = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'dotnet' -Arguments @('build','--no-restore',$projects[0].FullName) `
            -WorkingDirectory $worktree -TimeoutSeconds $Configuration.verificationTimeoutSeconds -RemovedEnvironment $removedEnvironment
        $result.buildExitCode = $build.exitCode
        if ($build.exitCode -ne 0) { throw "$label external build failed with exit $($build.exitCode)." }
        $run = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'dotnet' `
            -Arguments @('run','--project',$projects[0].FullName,'--no-build','--no-restore') -WorkingDirectory $worktree `
            -TimeoutSeconds $Configuration.verificationTimeoutSeconds -RemovedEnvironment $removedEnvironment
        $result.runExitCode = $run.exitCode
        $result.output = $run.stdout.Replace("`r`n","`n").TrimEnd("`n")
        if ($run.exitCode -ne 0 -or $result.output -cne [string]$Configuration.task.expectedOutput) { throw "$label external output verification failed." }
        $result.status = 'PASS'
    } catch {
        $failedProcess=if($_.Exception.Data.Contains('MRSmokeProcess')){$_.Exception.Data['MRSmokeProcess']}else{$null}
        if($ReadOnlyProbe){$result.readOnlyEvidence=@{fileAbsent=(-not(Test-Path (Join-Path $worktree 'forbidden.txt')));agentAttestsDeniedAttempt=$false;boundary='No successful attestation; see diagnostics.'}}
        if ($budget.IsRunning) { $budget.Stop() }
        $pathReplacements = @{
            $HOME='<HOME>'; $RepositoryRoot='<REPOSITORY>'
            $TemporaryRoot='<TEMP>'; $worktree='<WORKTREE>'
        }
        $result.reason = ConvertTo-MRSmokeDiagnosticText -Text $_.Exception.Message -RedactValues $RedactValues -PathReplacements $pathReplacements
        if ($result.reason -like 'TIMEOUT:*') { $result.status = 'TIMEOUT' }
        elseif ($stage -in @('discovery','preflight')) { $result.status = 'BLOCKED' }
        elseif ($stage -in @('task','verification')) { $result.status = 'FAILED' }
        else { $result.status = 'INFRA_ERROR' }
        $diagnostics = [ordered]@{
            processes = [ordered]@{
                failed = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $failedProcess -RedactValues $RedactValues -PathReplacements $pathReplacements
                readProbe = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $readProbe -RedactValues $RedactValues -PathReplacements $pathReplacements
                discovery = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $discoveryProcess -RedactValues $RedactValues -PathReplacements $pathReplacements
                seed = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $seedProcess -RedactValues $RedactValues -PathReplacements $pathReplacements
                status = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $statusProcess -RedactValues $RedactValues -PathReplacements $pathReplacements
                refresh = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $refreshProcess -RedactValues $RedactValues -PathReplacements $pathReplacements
                preflight = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $preflight -RedactValues $RedactValues -PathReplacements $pathReplacements
                agent = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $task -RedactValues $RedactValues -PathReplacements $pathReplacements
                build = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $build -RedactValues $RedactValues -PathReplacements $pathReplacements
                run = ConvertTo-MRSmokeProcessDiagnostic -ProcessResult $run -RedactValues $RedactValues -PathReplacements $pathReplacements
            }
            agentResult = Get-MRSmokeAgentResultDiagnostic -OutputFile $(if ($AutonomousRunnerSource) {Join-Path $worktree '.integration-result/implementation.result.json'} else {$outputFile}) -StandardOutput $(if ($null -ne $task) { [string]$task.stdout } else { '' }) -RedactValues $RedactValues -PathReplacements $pathReplacements
            project = Get-MRSmokeProjectDiagnostics -Worktree $worktree -RedactValues $RedactValues -PathReplacements $pathReplacements
        }
        $result.diagnostics = Limit-MRSmokeAttemptDiagnostics -Diagnostics $diagnostics
    }
    return $result
}

if (-not $ConfirmLiveRun) { throw 'Live model use is blocked. Repeat with -ConfirmLiveRun after explicit authorization.' }
if ($AutonomousRunnerSource) { $AutonomousRunnerSource = Resolve-MRSmokePhysicalPath $AutonomousRunnerSource }
$configuration = Read-MRSmokeConfiguration $defaultsPath
if ($MaxRetries -gt [int]$configuration.maxRetriesUpperBound) { throw 'MaxRetries exceeds the versioned upper bound.' }
if ($SelectionFile) {
    $SelectionFile = [IO.Path]::GetFullPath($SelectionFile)
    $configuration = Merge-MRSmokeSelectionOverride $configuration $SelectionFile
}
$reportRoot = Join-Path $repositoryRoot '.test-results/model-routing-smoke'

$startedAt = [DateTime]::UtcNow
$commit = (& git -C $repositoryRoot rev-parse HEAD).Trim()
if ($LASTEXITCODE -ne 0) { throw 'Repository HEAD is unavailable.' }
$statusBefore = @(& git -C $repositoryRoot status --porcelain --untracked-files=all)
$profilePath = Get-MRSmokeDefaultProfilePath
$profileBefore = Get-MRSmokeFileSnapshot $profilePath
$defaultHash = Get-MRSmokeHash $defaultsPath
$effectiveJson = $configuration | ConvertTo-Json -Depth 50 -Compress
$effectiveHash = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($effectiveJson))).ToLowerInvariant()
$report = [ordered]@{
    schemaVersion='1.1';testId='model-routing-live-smoke';startedAt=$startedAt.ToString('o')
    finishedAt=$null;platform=[Runtime.InteropServices.RuntimeInformation]::OSDescription
    powershellVersion=$PSVersionTable.PSVersion.ToString();repositoryCommit=$commit
    defaultsSha256=$defaultHash;effectiveConfigurationSha256=$effectiveHash
    selectionSource=$(if ($SelectionFile) { 'local-override' } else { 'versioned-defaults' })
    maxRetries=$MaxRetries;harnessScope=$(if($HarnessFilter.Count){'partial'}else{'all-five'});readOnlyProbeVersion=$(if($ReadOnlyProbe){'2.1'}else{$null});executionMode=$(if ($ReadOnlyProbe){'read-only-probe'}elseif ($AutonomousRunnerSource) {'production-phase-runner'} else {'shared-profile-execution'});environment=$null;harnesses=@();finalStatus='INFRA_ERROR';cleanup=[ordered]@{}
}
$reportName = '{0}-{1}.json' -f $startedAt.ToString('yyyyMMddTHHmmssZ'),$commit.Substring(0,12)
$reportPath = Join-Path $reportRoot $reportName
$temporaryRoot = $null
$server = $null
$globalFailure = $null
$serverStopped = $true
$compilerCleanup=@{tracked=0;stopped=0;alreadyExited=0;failed=0}
Enable-MRSmokeCompilerTracking
$harnessReports = [Collections.Generic.List[object]]::new()
try {
    if ($statusBefore.Count -ne 0) { throw 'Candidate repository must be clean before the live smoke test.' }
    foreach ($command in @('git','node','npm','pwsh','dotnet','agy','claude','codex','copilot','opencode')) {
        if ($null -eq (Get-Command $command -ErrorAction SilentlyContinue)) { throw "Required command is missing: $command" }
    }
    if (-not ((& dotnet --version).Trim().StartsWith('10.'))) { throw 'The live smoke test requires .NET SDK 10.' }
    $temporaryRoot = Join-Path ([IO.Path]::GetTempPath()) "model-routing-smoke-$([guid]::NewGuid().ToString('N'))"
    [void](New-Item -ItemType Directory -Path $temporaryRoot)
    $temporaryRoot = Resolve-MRSmokePhysicalPath $temporaryRoot
    $sdkRoot = Join-Path $temporaryRoot 'sdk'
    $claudePackage = "$($configuration.sdk.claude.package)@$($configuration.sdk.claude.version)"
    $copilotPackage = "$($configuration.sdk.copilot.package)@$($configuration.sdk.copilot.version)"
    $sdkInstall = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'npm' `
        -Arguments @('install','--prefix',$sdkRoot,$claudePackage,$copilotPackage,'--ignore-scripts','--no-audit','--no-fund') `
        -WorkingDirectory $temporaryRoot -TimeoutSeconds 120 -RemovedEnvironment ([string[]]$configuration.removedChildEnvironment)
    if ($sdkInstall.exitCode -ne 0) { throw 'Pinned SDK preparation failed.' }
    $claudeSdk = Join-Path $sdkRoot ([string]$configuration.sdk.claude.entry)
    $copilotSdk = Join-Path $sdkRoot ([string]$configuration.sdk.copilot.entry)
    if (-not (Test-Path -LiteralPath $claudeSdk -PathType Leaf) -or -not (Test-Path -LiteralPath $copilotSdk -PathType Leaf)) { throw 'Pinned SDK entry is missing.' }

    $serverPort = Get-MRSmokeFreePort
    $serverPassword = [Convert]::ToBase64String([Security.Cryptography.RandomNumberGenerator]::GetBytes(32))
    $serverSecret = [Security.SecureString]::new()
    foreach ($character in $serverPassword.ToCharArray()) { $serverSecret.AppendChar($character) }
    $serverSecret.MakeReadOnly()
    $serverCredential = [PSCredential]::new('opencode',$serverSecret)
    $serverPassword = $null
    $server = Start-MRSmokeOpenCodeServer -WorkingDirectory $temporaryRoot -Port $serverPort -Credential $serverCredential `
        -RemovedEnvironment ([string[]]$configuration.removedChildEnvironment)

    $redactValues = [Collections.Generic.List[string]]::new()
    foreach ($name in [string[]]$configuration.removedChildEnvironment) {
        $environmentValue = [Environment]::GetEnvironmentVariable($name)
        if (-not [string]::IsNullOrWhiteSpace($environmentValue)) { $redactValues.Add($environmentValue) }
    }
    $redactValues.Add($server.credential.GetNetworkCredential().Password)
    $environmentPaths = @{$HOME='<HOME>';$repositoryRoot='<REPOSITORY>';$temporaryRoot='<TEMP>'}
    $dotnetVersion = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'dotnet' -Arguments @('--version') `
        -WorkingDirectory $temporaryRoot -TimeoutSeconds 15 -RemovedEnvironment ([string[]]$configuration.removedChildEnvironment)
    $dotnetRuntimes = Invoke-MRSmokeProcess -ChildScript $childScript -Executable 'dotnet' -Arguments @('--list-runtimes') `
        -WorkingDirectory $temporaryRoot -TimeoutSeconds 15 -RemovedEnvironment ([string[]]$configuration.removedChildEnvironment)
    $report.environment = [ordered]@{
        processArchitecture=[Runtime.InteropServices.RuntimeInformation]::ProcessArchitecture.ToString()
        osArchitecture=[Runtime.InteropServices.RuntimeInformation]::OSArchitecture.ToString()
        dotnetPath=ConvertTo-MRSmokeDiagnosticText -Text ([string](Get-Command dotnet).Source) -RedactValues $redactValues.ToArray() -PathReplacements $environmentPaths
        dotnetVersion=ConvertTo-MRSmokeDiagnosticText -Text $dotnetVersion.stdout -RedactValues $redactValues.ToArray() -PathReplacements $environmentPaths
        dotnetRuntimes=ConvertTo-MRSmokeDiagnosticText -Text $dotnetRuntimes.stdout -RedactValues $redactValues.ToArray() -PathReplacements $environmentPaths
    }

    foreach ($harnessValue in $configuration.harnesses) {
        if($HarnessFilter.Count -and [string]$harnessValue.label -notin $HarnessFilter){continue}
        $harness = [hashtable]$harnessValue
        $attempts = [Collections.Generic.List[object]]::new()
        $harnessReport = [ordered]@{harness=[string]$harness.label;finalStatus='INFRA_ERROR';attempts=@()}
        $harnessReports.Add($harnessReport)
        $report.harnesses = @($harnessReports)
        for ($attempt = 1; $attempt -le (1 + $MaxRetries); $attempt++) {
            try {
                $attemptResult = Invoke-MRSmokeHarnessAttempt -Harness $harness -Attempt $attempt -Configuration $configuration `
                    -TemporaryRoot $temporaryRoot -RepositoryRoot $repositoryRoot -RedactValues $redactValues.ToArray() `
                    -ClaudeSdk $claudeSdk -CopilotSdk $copilotSdk -OpenCodeServer $server
            } catch {
                $attemptResult = [ordered]@{
                    attempt=$attempt;status='INFRA_ERROR';reason=ConvertTo-MRSmokeDiagnosticText -Text $_.Exception.Message `
                        -RedactValues $redactValues.ToArray() -PathReplacements $environmentPaths
                    model=$null;reasoningEffort=$null;diagnostics=$null
                }
            }
            $attempts.Add($attemptResult)
            $harnessReport.attempts = @($attempts)
            $harnessReport.finalStatus = Get-MRSmokeFinalStatus -Attempts @($attempts)
            Write-MRSmokeJsonAtomic -Path $reportPath -Value $report
            [ordered]@{harness=$harness.label;attempt=$attempt;status=$attemptResult.status;model=$attemptResult.model;reasoningEffort=$attemptResult.reasoningEffort} | ConvertTo-Json -Compress
            if ($attemptResult.status -ceq 'PASS') { break }
        }
        $report.harnesses = @($harnessReports)
    }
    if (@($report.harnesses | Where-Object { $_.finalStatus -notin @('PASS','PASS_WITH_RETRY') }).Count -eq 0) {
        $report.finalStatus = $(if (@($report.harnesses | Where-Object { [string]$_.finalStatus -ceq 'PASS_WITH_RETRY' }).Count) { 'PASS_WITH_RETRY' } else { 'PASS' })
    } else {
        $failedStatuses = @($report.harnesses | ForEach-Object { [string]$_.finalStatus })
        $report.finalStatus = if ($failedStatuses -ccontains 'INFRA_ERROR') { 'INFRA_ERROR' }
            elseif ($failedStatuses -ccontains 'TIMEOUT') { 'TIMEOUT' }
            elseif ($failedStatuses -ccontains 'FAILED') { 'FAILED' }
            else { 'BLOCKED' }
    }
} catch {
    $globalFailure = $_.Exception.Message
    $report.finalStatus = 'INFRA_ERROR'
} finally {
    try{$compilerCleanup=Stop-MRSmokeOwnedCompilers -Confirm:$false}
    catch{$compilerCleanup=@{tracked=0;stopped=0;alreadyExited=0;failed=1};if(-not $globalFailure){$globalFailure='Owned compiler cleanup failed.'}}
    try { Stop-MRSmokeOpenCodeServer $server }
    catch { $serverStopped = $false;if (-not $globalFailure) { $globalFailure = 'Temporary OpenCode server cleanup failed.' } }
    try {
        if ($temporaryRoot -and (Test-Path -LiteralPath $temporaryRoot)) { Remove-MRSmokeTemporaryRoot -Path $temporaryRoot -Confirm:$false }
    } catch {
        if (-not $globalFailure) { $globalFailure = 'Temporary smoke-test directory cleanup failed.' }
    }
}

$statusAfter = @(& git -C $repositoryRoot status --porcelain --untracked-files=all)
$headAfter = (& git -C $repositoryRoot rev-parse HEAD).Trim()
$profileAfter = Get-MRSmokeFileSnapshot $profilePath
$report.cleanup = [ordered]@{
    compilerCleanup=$compilerCleanup
    openCodeServerStopped = $serverStopped
    temporaryRootRemoved = -not ($temporaryRoot -and (Test-Path -LiteralPath $temporaryRoot))
    repositoryHeadUnchanged = $headAfter -ceq $commit
    repositoryStatusUnchanged = ($statusBefore -join "`n") -ceq ($statusAfter -join "`n")
    permanentProfileUnchanged = ($profileBefore | ConvertTo-Json -Compress) -ceq ($profileAfter | ConvertTo-Json -Compress)
}
if ($globalFailure) {
    $failurePaths = @{$HOME='<HOME>';$repositoryRoot='<REPOSITORY>'}
    if ($temporaryRoot) { $failurePaths[$temporaryRoot] = '<TEMP>' }
    $report.failureReason = ConvertTo-MRSmokeDiagnosticText -Text $globalFailure -PathReplacements $failurePaths
}
if ($compilerCleanup.failed -gt 0 -or -not $report.cleanup.openCodeServerStopped -or -not $report.cleanup.temporaryRootRemoved -or -not $report.cleanup.repositoryHeadUnchanged -or
    -not $report.cleanup.repositoryStatusUnchanged -or -not $report.cleanup.permanentProfileUnchanged) {
    $report.finalStatus = 'FAILED'
}
$report.finishedAt = [DateTime]::UtcNow.ToString('o')
$reportName = '{0}-{1}.json' -f $startedAt.ToString('yyyyMMddTHHmmssZ'),$commit.Substring(0,12)
$reportPath = Join-Path $reportRoot $reportName
Write-MRSmokeJsonAtomic -Path $reportPath -Value $report
$report | ConvertTo-Json -Depth 50
Write-Output "Report: $reportPath"
if ($report.finalStatus -notin @('PASS','PASS_WITH_RETRY')) { exit 1 }
