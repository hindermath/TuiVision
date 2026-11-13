<#
.SYNOPSIS
    DE: Gemeinsame normale Profil-Ausfuehrung. EN: Shared production profile execution.
.DESCRIPTION
    DE: Physische Pfade, JSON-sicherer Argumenttransport und sitzungsgebundene Rollenrechte.
    EN: Physical paths, JSON-safe argument transport and session-scoped role permissions.
#>
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Resolve-MRExecutionPath {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string] $Path)
    $full = [IO.Path]::GetFullPath($Path)
    $current = [IO.Path]::GetPathRoot($full)
    foreach ($part in $full.Substring($current.Length).Split([char[]]@('/','\'),[StringSplitOptions]::RemoveEmptyEntries)) {
        $current = Join-Path $current $part
        $item = Get-Item -LiteralPath $current -Force
        if ($null -ne $item.LinkTarget) { $current = Resolve-MRExecutionPath $item.ResolveLinkTarget($true).FullName }
    }
    return $current
}

function Expand-MRExecutionArguments {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][Collections.IDictionary] $ExecutionProfile,
        [Parameter(Mandatory)][hashtable] $Values,
        [switch] $Preflight
    )
    $arguments = [Collections.Generic.List[string]]::new()
    $templates = @($ExecutionProfile.arguments)
    for ($index = 0; $index -lt $templates.Count; $index++) {
        $value = [string]$templates[$index]
        if ($index -gt 0 -and [string]$templates[$index - 1] -ceq '-HarnessArgumentsJson') {
            # Decode before substitution: a quoted or multiline prompt is data,
            # never raw JSON syntax. Re-encode only after all replacements.
            $inner = @($value | ConvertFrom-Json -NoEnumerate | ForEach-Object { $_ })
            $expanded = foreach ($item in $inner) {
                $text = [string]$item
                foreach ($entry in $Values.GetEnumerator()) { $text = $text.Replace("{$($entry.Key)}",[string]$entry.Value) }
                $text
            }
            $value = ConvertTo-Json -InputObject @($expanded) -Compress
        } else {
            foreach ($entry in $Values.GetEnumerator()) { $value = $value.Replace("{$($entry.Key)}",[string]$entry.Value) }
        }
        $arguments.Add($value)
    }
    if ($Preflight -or -not $ExecutionProfile.Contains('agentFamily')) { return $arguments.ToArray() }
    $family = [string]$ExecutionProfile.agentFamily
    $readOnly = [string]$ExecutionProfile.routingRole -ceq 'fast-mechanical'
    if ($family -ceq 'Antigravity') {
        $path = Resolve-MRExecutionPath ([string]$Values.worktree)
        $allow = @(if ($readOnly) { "read_file($path)" } else { 'command(*)';"write_file($path)" })
        $sessionSettings=@{permissions=@{allow=$allow}}
        if($readOnly){
            # plan forces strict tool permission in this CLI, which also denies
            # scoped reads. Explicit deny wins over accept-edits for every write.
            $sessionSettings.toolPermission='request-review'
            $sessionSettings.permissions.deny=@('write_file(*)','command(*)')
            $modeIndex=$arguments.IndexOf('--mode')
            if($modeIndex -ge 0){$arguments[$modeIndex+1]='accept-edits'}else{$arguments.Insert(0,'--mode');$arguments.Insert(1,'accept-edits')}
            $arguments.Insert(0,'--add-dir');$arguments.Insert(1,$path)
        }
        $settings = $sessionSettings | ConvertTo-Json -Depth 5 -Compress
        $index = $arguments.IndexOf('--print')
        if ($index -lt 0) { throw 'Antigravity print argument missing.' }
        $arguments.Insert($index,'--settings');$arguments.Insert($index + 1,$settings)
    } elseif ($family -cin @('Claude','Copilot','OpenCode')) {
        $index = $arguments.IndexOf('-HarnessArgumentsJson')
        if ($index -lt 0) { throw 'Structured harness argument transport missing.' }
        $inner = [Collections.Generic.List[string]]::new()
        foreach ($value in @($arguments[$index + 1] | ConvertFrom-Json)) { $inner.Add([string]$value) }
        if($readOnly){
            if($family -ceq 'Claude'){
                $position=$inner.Count - 1
                $schema=@{type='object';properties=@{
                    read=@{type='boolean'};writeDenied=@{type='boolean'};writeUnavailable=@{type='boolean'};writeAttempted=@{type='boolean'}
                    schemaVersion=@{type='string'};phaseId=@{type='string'};attemptId=@{type='string'};outcome=@{type='string'}
                    expectedTasks=@{type='integer'};completedTasks=@{type='integer'};blockedReason=@{type='string'};gatesSatisfied=@{type='boolean'}
                    payloadPath=@{type='string'};payloadSha256=@{type='string'}
                };additionalProperties=$true}|ConvertTo-Json -Depth 10 -Compress
                foreach($flag in @('--restricted','--tools','Read,Glob,Grep','--allowedTools','Read,Glob,Grep','--disallowedTools','Write,Edit,Bash,PowerShell,NotebookEdit','--strict-mcp-config','--mcp-config','{"mcpServers":{}}','--output-format','json','--json-schema',$schema,'--')){$inner.Insert($position,$flag);$position++}
                $protocolIndex=$arguments.IndexOf('-OutputProtocol')
                if($protocolIndex -lt 0){throw 'Claude structured output needs explicit result protocol.'}
                $arguments[$protocolIndex+1]='ClaudeStructuredJson'
            }
            $arguments[$index+1]=ConvertTo-Json -InputObject @($inner) -Compress
            return $arguments.ToArray()
        }
        switch ($family) {
            Claude {
                $position = $inner.Count - 1
                foreach ($flag in @('--allowedTools','Bash','Write','Edit','--')) { $inner.Insert($position,$flag);$position++ }
            }
            Copilot {
                $position = $inner.IndexOf('--prompt')
                if ($position -lt 0) { throw 'Copilot prompt argument missing.' }
                foreach ($flag in @('--trust-folder','--allow-tool=write','--allow-tool=shell')) { $inner.Insert($position,$flag);$position++ }
            }
            OpenCode {
                $position = $inner.IndexOf('--agent')
                if ($position -lt 0) { throw 'OpenCode agent argument missing.' }
                $inner.Insert($position,'--auto')
            }
        }
        $arguments[$index + 1] = ConvertTo-Json -InputObject @($inner) -Compress
    }
    return $arguments.ToArray()
}

function Get-MRReadOnlyToolBoundary {
    [CmdletBinding()]
    param([Parameter(Mandatory)][Collections.IDictionary]$ExecutionProfile,[Parameter(Mandatory)][string[]]$Arguments)
    if([string]$ExecutionProfile.routingRole -cne 'fast-mechanical'){return 'unverified'}
    if($ExecutionProfile.agentFamily -ceq 'Codex'){
        $sandbox=[Array]::IndexOf($Arguments,'--sandbox')
        if($sandbox -ge 0 -and @($Arguments|Where-Object{$_ -ceq '--sandbox'}).Count -eq 1 -and $Arguments[$sandbox+1] -ceq 'read-only'){
            return 'tools-withheld'
        }
        return 'unverified'
    }
    $index=[Array]::IndexOf($Arguments,'-HarnessArgumentsJson')
    if($index -lt 0){return 'unverified'}
    $inner=@($Arguments[$index+1]|ConvertFrom-Json)
    if($ExecutionProfile.agentFamily -ceq 'Claude'){
        $tools=[Array]::IndexOf($inner,'--tools');$mcp=[Array]::IndexOf($inner,'--mcp-config')
        if($tools -ge 0 -and @($inner|Where-Object{$_ -ceq '--tools'}).Count -eq 1 -and $inner[$tools+1] -ceq 'Read,Glob,Grep' -and '--restricted' -cin $inner -and '--strict-mcp-config' -cin $inner -and $mcp -ge 0 -and @($inner|Where-Object{$_ -ceq '--mcp-config'}).Count -eq 1 -and $inner[$mcp+1] -ceq '{"mcpServers":{}}'){return 'tools-withheld'}
    }elseif($ExecutionProfile.agentFamily -ceq 'Copilot'){
        $tools=[Array]::IndexOf($inner,'--available-tools');$deny=[Array]::IndexOf($inner,'--deny-tool')
        if($tools -ge 0 -and @($inner|Where-Object{$_ -ceq '--available-tools'}).Count -eq 1 -and $inner[$tools+1] -ceq 'view' -and $deny -ge 0 -and @($inner|Where-Object{$_ -ceq '--deny-tool'}).Count -eq 1 -and $inner[$deny+1] -ceq 'write,shell' -and '--disable-builtin-mcps' -cin $inner){return 'tools-withheld'}
    }
    return 'unverified'
}
Export-ModuleMember -Function Resolve-MRExecutionPath,Expand-MRExecutionArguments,Get-MRReadOnlyToolBoundary
