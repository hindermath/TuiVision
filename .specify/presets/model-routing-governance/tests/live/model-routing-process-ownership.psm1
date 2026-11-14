Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$script:ownedCompilers=@{}

function Get-MRSmokeCompilerPath {
    param([Parameter(Mandatory)] $TargetProcess)
    $path=[string]$TargetProcess.Path
    if([IO.Path]::GetFileNameWithoutExtension($path) -ceq 'VBCSCompiler'){return $path}
    if($IsLinux -and [IO.Path]::GetFileNameWithoutExtension($path) -ceq 'dotnet'){
        $arguments=([IO.File]::ReadAllText("/proc/$($TargetProcess.Id)/cmdline")).Split([char]0)
        $compilerArguments=@($arguments|Where-Object{[IO.Path]::GetFileName($_) -ceq 'VBCSCompiler.dll'})
        if($compilerArguments.Count -eq 1){return $compilerArguments[0]}
    }
    return ''
}

function Test-MRSmokeCompilerIdentity {
    [CmdletBinding()]
    param([string] $Path,[long] $ExpectedStartTicks,[long] $ActualStartTicks)
    return [IO.Path]::GetFileNameWithoutExtension($Path) -ceq 'VBCSCompiler' -and $ExpectedStartTicks -eq $ActualStartTicks
}

function Update-MRSmokeCompilerOwnership {
    [CmdletBinding()]
    param([Parameter(Mandatory)][hashtable] $Tree)
    # Parent IDs are observed while the invocation is alive. A global before/
    # after PID difference cannot establish ownership and is never used.
    $relations = if ($IsWindows) {
        @(Get-CimInstance Win32_Process -OperationTimeoutSec 2 | ForEach-Object { @{id=[int]$_.ProcessId;parent=[int]$_.ParentProcessId} })
    } else {
        $processList=(Get-Command ps -CommandType Application -ErrorAction Stop).Source
        @(& $processList -axo 'pid=,ppid=' | ForEach-Object {
            if ($_ -match '^\s*(\d+)\s+(\d+)\s*$') { @{id=[int]$Matches[1];parent=[int]$Matches[2]} }
        })
    }
    do {
        $added=$false
        foreach ($relation in $relations) {
            if ($Tree.ContainsKey($relation.id) -or -not $Tree.ContainsKey($relation.parent)) { continue }
            try {
                $parent=Get-Process -Id $relation.parent -ErrorAction Stop
                if ($parent.StartTime.ToUniversalTime().Ticks -ne $Tree[$relation.parent]) { continue }
                $candidate=Get-Process -Id $relation.id -ErrorAction Stop
                $ticks=$candidate.StartTime.ToUniversalTime().Ticks
                $Tree[$relation.id]=$ticks;$added=$true
                $compilerPath=Get-MRSmokeCompilerPath $candidate
                if (Test-MRSmokeCompilerIdentity $compilerPath $ticks $ticks) {
                    $script:ownedCompilers[$relation.id]=@{id=$relation.id;startTicks=$ticks;path=$candidate.Path;compilerPath=$compilerPath}
                }
            } catch { continue }
        }
    } while ($added)
}

function Stop-MRSmokeOwnedCompilers {
    [CmdletBinding(SupportsShouldProcess)]
    param()
    $stopped=0;$alreadyExited=0;$failed=0
    foreach ($entry in @($script:ownedCompilers.Values)) {
        try { $process=Get-Process -Id $entry.id -ErrorAction Stop }
        catch { $alreadyExited++;continue }
        try {
            $compilerPath=Get-MRSmokeCompilerPath $process
            if (-not (Test-MRSmokeCompilerIdentity $compilerPath $entry.startTicks $process.StartTime.ToUniversalTime().Ticks) -or $process.Path -cne $entry.path -or $compilerPath -cne $entry.compilerPath) {
                $failed++;continue
            }
        } catch {
            if($process.HasExited){$alreadyExited++}else{$failed++}
            continue
        }
        if ($PSCmdlet.ShouldProcess("owned compiler PID $($entry.id)",'Stop test compiler')) {
            try { $process.Kill();if(-not $process.WaitForExit(2000)){throw 'Compiler termination timeout.'};$stopped++ }
            catch { $failed++ }
        }
    }
    if ($failed -eq 0) { $script:ownedCompilers=@{} }
    return @{tracked=$stopped+$alreadyExited+$failed;stopped=$stopped;alreadyExited=$alreadyExited;failed=$failed}
}

Export-ModuleMember -Function Test-MRSmokeCompilerIdentity,Update-MRSmokeCompilerOwnership,Stop-MRSmokeOwnedCompilers
