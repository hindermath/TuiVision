Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Import-Module (Join-Path $PSScriptRoot '../../scripts/execution-policy.psm1') -Force
Import-Module (Join-Path $PSScriptRoot 'model-routing-process-ownership.psm1') -Force
$script:trackCompilers=$false

function Enable-MRSmokeCompilerTracking { $script:trackCompilers=$true }
Export-ModuleMember -Function Enable-MRSmokeCompilerTracking,Stop-MRSmokeOwnedCompilers

function Read-MRSmokeConfiguration {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string] $Path)
    $configuration = Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -AsHashtable -Depth 50
    if ([string] $configuration.schemaVersion -cne '1.0' -or
        [string] $configuration.testId -cne 'model-routing-live-smoke') {
        throw 'Smoke configuration schema or test ID is unsupported.'
    }
    if ($configuration.attemptBudgetSeconds -isnot [long] -or $configuration.attemptBudgetSeconds -lt 1 -or
        $configuration.verificationTimeoutSeconds -isnot [long] -or $configuration.verificationTimeoutSeconds -lt 1 -or
        $configuration.maxRetriesUpperBound -ne 3) {
        throw 'Smoke configuration time or retry limits are invalid.'
    }
    $families = @($configuration.harnesses | ForEach-Object { [string] $_.family })
    if (($families -join ',') -cne 'Antigravity,Claude,Codex,Copilot,OpenCode') {
        throw 'Smoke configuration harness order is invalid.'
    }
    foreach ($name in @('Claude','Copilot','OpenCode')) {
        $entry = @($configuration.harnesses | Where-Object { [string] $_.family -ceq $name })
        if ($entry.Count -ne 1 -or [string]::IsNullOrWhiteSpace([string]$entry[0].frontierModel) -or
            [string]::IsNullOrWhiteSpace([string]$entry[0].fastModel)) {
            throw "Smoke configuration exact selection is missing for $name."
        }
    }
    if ([string] $configuration.task.framework -cne 'net10.0' -or
        [string] $configuration.task.expectedOutput -cne 'MODEL_ROUTING_OK' -or
        [string]::IsNullOrWhiteSpace([string]$configuration.task.prompt)) {
        throw 'Smoke configuration task contract is invalid.'
    }
    return $configuration
}

function Merge-MRSmokeSelectionOverride {
    [CmdletBinding()]
    param([Parameter(Mandatory)][hashtable] $Configuration, [string] $Path = '')
    if ([string]::IsNullOrWhiteSpace($Path)) { return $Configuration }
    $override = Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -AsHashtable -Depth 20
    if ([string] $override.schemaVersion -cne '1.0' -or $override.selections -isnot [hashtable]) {
        throw 'Local smoke selection override schema is unsupported.'
    }
    $allowed = @('Claude','Copilot','OpenCode')
    foreach ($key in $override.selections.Keys) {
        if ($key -cnotin $allowed) { throw "Unknown local smoke selection family: $key" }
        $selection = [hashtable] $override.selections[$key]
        foreach ($field in @('frontierModel','fastModel')) {
            if ([string]::IsNullOrWhiteSpace([string]$selection[$field])) { throw "$key local override is missing $field." }
        }
        $target = @($Configuration.harnesses | Where-Object { [string] $_.family -ceq $key })[0]
        $target.frontierModel = [string] $selection.frontierModel
        $target.fastModel = [string] $selection.fastModel
        if ($key -eq 'OpenCode' -and $selection.ContainsKey('provider')) {
            if ([string]::IsNullOrWhiteSpace([string]$selection.provider)) { throw 'OpenCode local override provider is empty.' }
            $target.provider = [string] $selection.provider
        }
    }
    return $Configuration
}

function Resolve-MRSmokePhysicalPath {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string] $Path)
    $full = [IO.Path]::GetFullPath($Path)
    $current = [IO.Path]::GetPathRoot($full)
    foreach ($part in $full.Substring($current.Length).Split([char[]]@('/','\'),[StringSplitOptions]::RemoveEmptyEntries)) {
        $current = Join-Path $current $part
        $item = Get-Item -LiteralPath $current -Force
        if ($null -ne $item.LinkTarget) { $current = Resolve-MRSmokePhysicalPath $item.ResolveLinkTarget($true).FullName }
    }
    return $current
}

Export-ModuleMember -Function Resolve-MRSmokePhysicalPath

function Remove-MRSmokeTemporaryRoot {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Path,[ValidateRange(1,10)][int]$TimeoutSeconds=5)
    $full=[IO.Path]::GetFullPath($Path).TrimEnd([char[]]@('/','\'))
    $name=[IO.Path]::GetFileName($full)
    $comparison=if($IsWindows){[StringComparison]::OrdinalIgnoreCase}else{[StringComparison]::Ordinal}
    if($name -notmatch '^(model-routing-smoke|routing-dotnet-contract)-[0-9a-f]{32}$' -or
        -not (Resolve-MRSmokePhysicalPath (Split-Path -Parent $full)).Equals((Resolve-MRSmokePhysicalPath ([IO.Path]::GetTempPath())),$comparison)){
        throw 'Cleanup target is not a recognised direct temporary test root.'
    }
    if(-not $PSCmdlet.ShouldProcess($full,'Remove temporary test root with bounded lock retry')){return}
    $watch=[Diagnostics.Stopwatch]::StartNew()
    while(Test-Path -LiteralPath $full){
        if((Get-Item -LiteralPath $full -Force).LinkTarget){throw 'Refusing a linked temporary root.'}
        try{Remove-Item -LiteralPath $full -Recurse -Force -ErrorAction Stop}
        catch{if($watch.Elapsed.TotalSeconds -ge $TimeoutSeconds){throw};Start-Sleep -Milliseconds 200}
    }
}
Export-ModuleMember -Function Remove-MRSmokeTemporaryRoot

function New-MRSmokeProcessStartInfo {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string] $ChildScript,
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][AllowEmptyString()][string[]] $Arguments,
        [Parameter(Mandatory)][string] $WorkingDirectory,
        [string[]] $RemovedEnvironment = @(),
        [hashtable] $Environment = @{}
    )
    $WorkingDirectory = Resolve-MRSmokePhysicalPath $WorkingDirectory
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = (Get-Process -Id $PID).Path
    $start.WorkingDirectory = $WorkingDirectory
    $start.UseShellExecute = $false
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    foreach ($argument in @('-NoProfile','-File',$ChildScript,'-Executable',$Executable,'-ArgumentsJson',(ConvertTo-Json -InputObject @($Arguments) -Compress))) {
        [void] $start.ArgumentList.Add($argument)
    }
    foreach ($name in $RemovedEnvironment) { [void] $start.Environment.Remove($name) }
    foreach ($entry in $Environment.GetEnumerator()) { $start.Environment[[string]$entry.Key] = [string]$entry.Value }
    $start.Environment['UseSharedCompilation']='false'
    $start.Environment['DOTNET_CLI_USE_MSBUILD_SERVER']='0'
    $start.Environment['MSBUILDDISABLENODEREUSE']='1'
    $start.Environment['PWD'] = $WorkingDirectory
    return $start
}

function Invoke-MRSmokeProcess {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string] $ChildScript,
        [Parameter(Mandatory)][string] $Executable,
        [Parameter(Mandatory)][AllowEmptyString()][string[]] $Arguments,
        [Parameter(Mandatory)][string] $WorkingDirectory,
        [Parameter(Mandatory)][double] $TimeoutSeconds,
        [string[]] $RemovedEnvironment = @(),
        [hashtable] $Environment = @{}
    )
    if ($TimeoutSeconds -le 0) { throw 'TIMEOUT: harness budget exhausted.' }
    $startArguments = @{
        ChildScript = $ChildScript; Executable = $Executable; Arguments = $Arguments
        WorkingDirectory = $WorkingDirectory; RemovedEnvironment = $RemovedEnvironment
        Environment = $Environment
    }
    $process = [Diagnostics.Process]::new()
    $process.StartInfo = New-MRSmokeProcessStartInfo @startArguments
    $watch = [Diagnostics.Stopwatch]::StartNew()
    $cancellation = [Threading.CancellationTokenSource]::new()
    $stdout=$null;$stderr=$null;$started=$false
    try {
        [void] $process.Start()
        $started=$true
        $tree=@{};$tree[$process.Id]=$process.StartTime.ToUniversalTime().Ticks
        $stdout = $process.StandardOutput.ReadToEndAsync($cancellation.Token)
        $stderr = $process.StandardError.ReadToEndAsync($cancellation.Token)
        $exited=$false
        do {
            if ($script:trackCompilers) { Update-MRSmokeCompilerOwnership -Tree $tree }
            $remaining=[Math]::Max(1,[int](($TimeoutSeconds-$watch.Elapsed.TotalSeconds)*1000))
            $exited=$process.WaitForExit([Math]::Min(250,$remaining))
        } while (-not $exited -and $watch.Elapsed.TotalSeconds -lt $TimeoutSeconds)
        if (-not $exited) {
            $process.Kill($true)
            [void]$process.WaitForExit(2000)
            throw "TIMEOUT: process tree exceeded the remaining $([Math]::Round($TimeoutSeconds,3))-second budget."
        }
        # Descendants can retain inherited pipes after their parent exits. Drain
        # only within the original deadline, never via an unbounded GetResult.
        $remainingMilliseconds = [Math]::Max(1,[int](($TimeoutSeconds - $watch.Elapsed.TotalSeconds) * 1000))
        $drain = [Threading.Tasks.Task]::WhenAll([Threading.Tasks.Task[]]@($stdout,$stderr))
        if (-not $drain.Wait($remainingMilliseconds)) {
            throw 'TIMEOUT: inherited output pipes remained open after child exit.'
        }
        return [ordered]@{
            exitCode = $process.ExitCode
            stdout = $stdout.GetAwaiter().GetResult()
            stderr = $stderr.GetAwaiter().GetResult()
            seconds = [Math]::Round($watch.Elapsed.TotalSeconds,3)
        }
    } catch {
        $_.Exception.Data['MRSmokeProcess']=@{
            exitCode=$(if($started -and $process.HasExited){$process.ExitCode}else{-1})
            seconds=[Math]::Round($watch.Elapsed.TotalSeconds,3)
            stdout=$(if($null -ne $stdout -and $stdout.IsCompletedSuccessfully){$stdout.GetAwaiter().GetResult()}else{'<output unavailable before process completion>'})
            stderr=$(if($null -ne $stderr -and $stderr.IsCompletedSuccessfully){$stderr.GetAwaiter().GetResult()}else{'<output unavailable before process completion>'})
        }
        throw
    } finally {
        $watch.Stop()
        $cancellation.Cancel()
        $process.Dispose()
        $cancellation.Dispose()
    }
}

function Expand-MRSmokeProfileArguments {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][hashtable] $RunnerProfile,
        [Parameter(Mandatory)][string] $Worktree,
        [Parameter(Mandatory)][string] $OutputFile,
        [Parameter(Mandatory)][string] $Family,
        [Parameter(Mandatory)][string] $Prompt
    )
    $executionProfile = $RunnerProfile.Clone()
    if (-not $executionProfile.ContainsKey('agentFamily')) { $executionProfile.agentFamily = $Family }
    if (-not $executionProfile.ContainsKey('routingRole')) { $executionProfile.routingRole = 'long-running-implementation' }
    return Expand-MRExecutionArguments -ExecutionProfile $executionProfile -Values @{
        model=[string]$executionProfile.model;reasoningEffort=[string]$executionProfile.reasoningEffort
        worktree=(Resolve-MRExecutionPath $Worktree);outputFile=$OutputFile;prompt=$Prompt
    }
}

function ConvertTo-MRSmokeDiagnosticText {
    [CmdletBinding()]
    param(
        [AllowEmptyString()][string] $Text = '',
        [int] $MaxBytes = 16384,
        [string[]] $RedactValues = @(),
        [hashtable] $PathReplacements = @{}
    )
    if ([string]::IsNullOrEmpty($Text)) { return '' }
    $safe = $Text.Replace("`r`n","`n").Replace("`r","`n")
    foreach ($entry in @($PathReplacements.GetEnumerator() | Sort-Object { ([string]$_.Key).Length } -Descending)) {
        if (-not [string]::IsNullOrWhiteSpace([string]$entry.Key)) { $safe = $safe.Replace([string]$entry.Key,[string]$entry.Value) }
    }
    foreach ($value in @($RedactValues | Where-Object { -not [string]::IsNullOrWhiteSpace($_) -and $_.Length -ge 4 } | Sort-Object Length -Descending -Unique)) {
        $safe = $safe.Replace($value,'<redacted>')
    }
    $safe = [regex]::Replace($safe,'(?i)\b(Bearer|Basic)\s+[A-Za-z0-9+/=._~-]+','$1 <redacted>')
    $safe = [regex]::Replace($safe,'(?i)\b(api[_-]?key|token|password|authorization)\b(\s*[:=]\s*)([^\s,;]+)','$1$2<redacted>')
    $safe = [regex]::Replace($safe,'\bsk-[A-Za-z0-9_-]{8,}\b','<redacted>')
    $safe = [regex]::Replace($safe,'\bgh[pousr]_[A-Za-z0-9]{8,}\b','<redacted>')
    $bytes = [Text.Encoding]::UTF8.GetBytes($safe)
    if ($bytes.Length -le $MaxBytes) { return $safe }
    $marker="`n<truncated:originalBytes=$($bytes.Length)>"
    $prefixLength=[Math]::Max(0,$MaxBytes-[Text.Encoding]::UTF8.GetByteCount($marker))
    $decoder=[Text.UTF8Encoding]::new($false,$true)
    $prefix=''
    while($prefixLength -gt 0){
        try{$prefix=$decoder.GetString($bytes,0,$prefixLength);break}catch{$prefixLength--}
    }
    return $prefix + $marker
}

function ConvertTo-MRSmokeProcessDiagnostic {
    [CmdletBinding()]
    param(
        $ProcessResult,
        [string[]] $RedactValues = @(),
        [hashtable] $PathReplacements = @{}
    )
    if ($null -eq $ProcessResult) { return $null }
    return [ordered]@{
        exitCode = $ProcessResult.exitCode
        seconds = $ProcessResult.seconds
        stdout = ConvertTo-MRSmokeDiagnosticText -Text ([string]$ProcessResult.stdout) -RedactValues $RedactValues -PathReplacements $PathReplacements
        stderr = ConvertTo-MRSmokeDiagnosticText -Text ([string]$ProcessResult.stderr) -RedactValues $RedactValues -PathReplacements $PathReplacements
    }
}

function Get-MRSmokeAgentResultDiagnostic {
    [CmdletBinding()]
    param([string] $OutputFile = '', [AllowEmptyString()][string] $StandardOutput = '',[string[]] $RedactValues=@(),[hashtable] $PathReplacements=@{})
    $candidate = if ($OutputFile -and (Test-Path -LiteralPath $OutputFile -PathType Leaf)) {
        Get-Content -LiteralPath $OutputFile -Raw
    } else { $StandardOutput }
    if ([string]::IsNullOrWhiteSpace($candidate)) { return $null }
    try { $value = $candidate | ConvertFrom-Json -AsHashtable -Depth 20 }
    catch { return $null }
    if ($value -isnot [Collections.IDictionary]) { return $null }
    $result = [ordered]@{}
    foreach ($key in @('created','executed','verified','read','writeDenied','writeUnavailable','writeAttempted')) {
        if ($value.Contains($key) -and $value[$key] -is [bool]) { $result[$key] = [bool]$value[$key] }
    }
    foreach ($key in @('outcome','blockedReason','phaseId')) {
        if ($value.Contains($key) -and $value[$key] -is [string]) {
            $result[$key]=ConvertTo-MRSmokeDiagnosticText -Text $value[$key] -MaxBytes 4096 -RedactValues $RedactValues -PathReplacements $PathReplacements
        }
    }
    if ($result.Count -eq 0) { return $null }
    return $result
}

function Test-MRSmokeReadOnlyAttestation {
    [CmdletBinding()]
    param([AllowNull()]$Value,[string]$ToolBoundary='unverified')
    if($Value -isnot [Collections.IDictionary]){return $false}
    foreach($key in @('read','writeDenied','writeUnavailable','writeAttempted')){
        if(-not $Value.Contains($key) -or $Value[$key] -isnot [bool]){return $false}
    }
    return $Value.read -and (
        ($Value.writeDenied -and $Value.writeAttempted -and -not $Value.writeUnavailable) -or
        ($ToolBoundary -ceq 'tools-withheld' -and $Value.writeUnavailable -and -not $Value.writeAttempted -and -not $Value.writeDenied)
    )
}
Export-ModuleMember -Function Test-MRSmokeReadOnlyAttestation

function Get-MRSmokeReadOnlyToolBoundary {
    [CmdletBinding()]
    param([Collections.IDictionary]$ExecutionProfile,[string[]]$Arguments)
    return Get-MRReadOnlyToolBoundary -ExecutionProfile $ExecutionProfile -Arguments $Arguments
}
Export-ModuleMember -Function Get-MRSmokeReadOnlyToolBoundary

function Test-MRSmokeHostWriteDenial {
    [CmdletBinding()]
    param([AllowNull()]$ProcessResult)
    if($null -eq $ProcessResult -or $ProcessResult.exitCode -ne 0){return $false}
    $standardOutput=if($ProcessResult.Contains('stdout')){[string]$ProcessResult.stdout}else{''}
    $standardError=if($ProcessResult.Contains('stderr')){[string]$ProcessResult.stderr}else{''}
    $combined=$standardOutput+"`n"+$standardError
    return ($combined -match 'tool required the "write_file" permission' -and $combined -match 'auto-denied') -or
        ($combined -match 'Permission denied for write_file\([^\r\n]+\)\. Matches user-configured deny rule\.')
}
Export-ModuleMember -Function Test-MRSmokeHostWriteDenial

function Get-MRSmokeProjectDiagnostics {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string] $Worktree,
        [string[]] $RedactValues = @(),
        [hashtable] $PathReplacements = @{}
    )
    if (-not (Test-Path -LiteralPath $Worktree -PathType Container)) { return [ordered]@{files=@();truncated=$false} }
    $entries = [Collections.Generic.List[object]]::new()
    $allFiles = @(Get-ChildItem -LiteralPath $Worktree -Recurse -File -Force -ErrorAction SilentlyContinue | Sort-Object FullName)
    $truncated = $false
    foreach ($file in $allFiles) {
        # PowerShell's -File also returns Unix FIFOs and sockets. Opening one
        # for hashing can block forever; only regular, non-link files qualify.
        if (($file.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) { continue }
        if (-not $IsWindows -and [string]$file.UnixStat.ItemType -cne 'File') { continue }
        $relative = [IO.Path]::GetRelativePath($Worktree,$file.FullName).Replace('\','/')
        if ($relative -match '(^|/)\.git(/|$)' -or $relative -match '(^|/)\.specify(/|$)' -or
            $relative -match '(^|/)\.dotnet(/|$)' -or
            $relative -match '(^|/)obj(/|$)') { continue }
        $inBin = $relative -match '(^|/)bin(/|$)'
        $selectedRuntimeMetadata = $relative -match '\.(runtimeconfig|deps)\.json$'
        if ($inBin -and -not $selectedRuntimeMetadata) { continue }
        if ($entries.Count -ge 200) { $truncated = $true;break }
        $entry = [ordered]@{path=$relative;size=[long]$file.Length;sha256=$null}
        try { $entry.sha256 = (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant() }
        catch { $entry.hashUnavailable = $true }
        if ($relative -match '\.csproj$' -or [IO.Path]::GetFileName($relative) -ceq 'Program.cs' -or $selectedRuntimeMetadata) {
            try {
                $raw = [IO.File]::ReadAllText($file.FullName)
                $entry.content = ConvertTo-MRSmokeDiagnosticText -Text $raw -MaxBytes 32768 -RedactValues $RedactValues -PathReplacements $PathReplacements
            } catch { $entry.contentUnavailable = $true }
        }
        $entries.Add($entry)
    }
    return [ordered]@{files=$entries.ToArray();truncated=$truncated}
}

function Limit-MRSmokeAttemptDiagnostics {
    [CmdletBinding()]
    param([Parameter(Mandatory)][Collections.IDictionary] $Diagnostics, [int] $MaxBytes = 131072)
    $serialized = $Diagnostics | ConvertTo-Json -Depth 50 -Compress
    if ([Text.Encoding]::UTF8.GetByteCount($serialized) -le $MaxBytes) { return $Diagnostics }
    if ($Diagnostics.project -is [Collections.IDictionary]) {
        foreach ($file in @($Diagnostics.project.files | Sort-Object size -Descending)) {
            if ($file -is [Collections.IDictionary] -and $file.Contains('content')) {
                $file.Remove('content');$file.contentOmittedByAttemptLimit=$true
                $serialized = $Diagnostics | ConvertTo-Json -Depth 50 -Compress
                if ([Text.Encoding]::UTF8.GetByteCount($serialized) -le $MaxBytes) { return $Diagnostics }
            }
        }
        if ($Diagnostics.project.files.Count -gt 50) {
            $Diagnostics.project.files = @($Diagnostics.project.files | Select-Object -First 50)
            $Diagnostics.project.truncated = $true
        }
    }
    $serialized = $Diagnostics | ConvertTo-Json -Depth 50 -Compress
    if ([Text.Encoding]::UTF8.GetByteCount($serialized) -gt $MaxBytes) {
        foreach ($processName in @($Diagnostics.processes.Keys)) {
            $process = $Diagnostics.processes[$processName]
            if ($process -is [Collections.IDictionary]) {
                $process.stdout = ConvertTo-MRSmokeDiagnosticText -Text ([string]$process.stdout) -MaxBytes 4096
                $process.stderr = ConvertTo-MRSmokeDiagnosticText -Text ([string]$process.stderr) -MaxBytes 4096
            }
        }
        $Diagnostics.attemptLimitApplied = $true
    }
    return $Diagnostics
}

function Read-MRSmokeReport {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string] $Path)
    $report = Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -AsHashtable -Depth 50
    if ([string]$report.schemaVersion -notin @('1.0','1.1') -or [string]$report.testId -cne 'model-routing-live-smoke') {
        throw 'Smoke report schema or test ID is unsupported.'
    }
    return $report
}

function Test-MRSmokeRequired {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string[]] $ChangedPaths, [Parameter(Mandatory)][string[]] $TriggerPaths)
    foreach ($path in $ChangedPaths) {
        foreach ($pattern in $TriggerPaths) {
            if ($path -like $pattern) { return $true }
        }
    }
    return $false
}

function Get-MRSmokeFinalStatus {
    [CmdletBinding()]
    param([Parameter(Mandatory)][object[]] $Attempts)
    $passed = @($Attempts | Where-Object { [string]$_.status -ceq 'PASS' })
    if ($passed.Count -gt 0) { return $(if ([int]$passed[-1].attempt -eq 1) { 'PASS' } else { 'PASS_WITH_RETRY' }) }
    if (@($Attempts | Where-Object { [string]$_.status -ceq 'INFRA_ERROR' }).Count) { return 'INFRA_ERROR' }
    if (@($Attempts | Where-Object { [string]$_.status -ceq 'TIMEOUT' }).Count) { return 'TIMEOUT' }
    if (@($Attempts | Where-Object { [string]$_.status -ceq 'FAILED' }).Count) { return 'FAILED' }
    return 'BLOCKED'
}

function Write-MRSmokeJsonAtomic {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string] $Path, [Parameter(Mandatory)] $Value)
    $directory = Split-Path -Parent $Path
    if ($PSCmdlet.ShouldProcess($Path,'Write smoke-test report')) {
        [void](New-Item -ItemType Directory -Path $directory -Force)
        $temporary = "$Path.tmp.$PID"
        try {
            [IO.File]::WriteAllText($temporary,($Value | ConvertTo-Json -Depth 50) + [Environment]::NewLine,[Text.UTF8Encoding]::new($false))
            [IO.File]::Move($temporary,$Path,$true)
        } finally {
            Remove-Item -LiteralPath $temporary -Force -ErrorAction SilentlyContinue
        }
    }
}

Export-ModuleMember -Function Read-MRSmokeConfiguration,Merge-MRSmokeSelectionOverride,New-MRSmokeProcessStartInfo,Invoke-MRSmokeProcess,Expand-MRSmokeProfileArguments,ConvertTo-MRSmokeDiagnosticText,ConvertTo-MRSmokeProcessDiagnostic,Get-MRSmokeAgentResultDiagnostic,Get-MRSmokeProjectDiagnostics,Limit-MRSmokeAttemptDiagnostics,Read-MRSmokeReport,Test-MRSmokeRequired,Get-MRSmokeFinalStatus,Write-MRSmokeJsonAtomic
