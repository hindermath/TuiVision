#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Prueft die normale Ausfuehrungspolicy offline. EN: Tests the production execution policy offline.
.DESCRIPTION
    DE: Prueft Rollenrechte, Preflight, JSON-Transport und physische Pfade ohne Modelle.
    EN: Checks role permissions, preflight, JSON transport and physical paths without models.
.EXAMPLE
    pwsh -NoProfile -File tests/test-execution-policy.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Import-Module (Join-Path $PSScriptRoot '../scripts/execution-policy.psm1') -Force
$temporary = Join-Path ([IO.Path]::GetTempPath()) "execution-policy-$([guid]::NewGuid().ToString('N'))"
[void](New-Item -ItemType Directory -Path $temporary)
try {
    $values = @{worktree=$temporary;model='test-model';reasoningEffort='high';prompt="quoted `"text`"`nnext line";outputFile=(Join-Path $temporary 'result.json')}
    foreach ($family in @('Claude','Copilot','OpenCode')) {
        $inner = switch ($family) {
            Claude { @('--permission-mode','acceptEdits','{prompt}') }
            Copilot { @('--prompt','{prompt}') }
            OpenCode { @('run','--agent','build','{prompt}') }
        }
        $executionProfile = @{agentFamily=$family;routingRole='long-running-implementation';arguments=@('-OutputProtocol','Plain','-HarnessArgumentsJson',(ConvertTo-Json -InputObject $inner -Compress))}
        $before = $executionProfile | ConvertTo-Json -Depth 10 -Compress
        $expanded = @(Expand-MRExecutionArguments $executionProfile $values)
        $decoded = @($expanded[3] | ConvertFrom-Json)
        if ($values.prompt -cnotin $decoded) { throw "$family lost multiline quoted prompt." }
        $permission = switch ($family) { Claude {'--allowedTools'} Copilot {'--allow-tool=write'} OpenCode {'--auto'} }
        if ($permission -cnotin $decoded) { throw "$family missing implementation permissions." }
        if (($executionProfile | ConvertTo-Json -Depth 10 -Compress) -cne $before) { throw 'Persistent profile was mutated.' }
        $executionProfile.routingRole = 'fast-mechanical'
        if($family -ceq 'Claude'){$inner[1]='plan'}
        if($family -ceq 'Copilot'){$inner=@('--available-tools','view','--deny-tool','write,shell','--disable-builtin-mcps','--prompt','{prompt}')}
        $executionProfile.arguments[3]=ConvertTo-Json -InputObject $inner -Compress
        $readonly = @(Expand-MRExecutionArguments $executionProfile $values)
        $readArguments=@($readonly[3]|ConvertFrom-Json)
        if($family -ceq 'Claude'){
            $schemaIndex=[Array]::IndexOf([object[]]$readArguments,'--json-schema')
            $schema=$readArguments[$schemaIndex+1]|ConvertFrom-Json -AsHashtable
            foreach($name in @('read','writeDenied','writeUnavailable','writeAttempted','gatesSatisfied')){
                if($schema.properties[$name].type -cne 'boolean'){throw "Claude schema does not require boolean ${name}."}
            }
            if($readonly[1] -cne 'ClaudeStructuredJson' -or 'Read,Glob,Grep' -cnotin $readArguments -or '--restricted' -cnotin $readArguments){throw 'Claude read-only filter/transport missing.'}
        }elseif ($permission -cin $readArguments) { throw "$family escalated read-only permissions." }
        if($family -cin @('Claude','Copilot')){
            if((Get-MRReadOnlyToolBoundary $executionProfile $readonly) -cne 'tools-withheld'){throw "$family tool boundary unverified."}
            $tampered=$readonly.Clone();$changed=@($tampered[3]|ConvertFrom-Json)+@('--tools','Write','--available-tools','view,create')
            $tampered[3]=ConvertTo-Json -InputObject $changed -Compress
            if((Get-MRReadOnlyToolBoundary $executionProfile $tampered) -cne 'unverified'){throw 'Conflicting tool filters accepted.'}
        }
        $preflight = @(Expand-MRExecutionArguments $executionProfile $values -Preflight)
        if ($permission -cin @($preflight[3] | ConvertFrom-Json)) { throw "$family escalated preflight." }
    }
    foreach ($role in @('fast-mechanical','long-running-implementation')) {
        $executionProfile = @{agentFamily='Antigravity';routingRole=$role;arguments=@('--print','{prompt}')}
        $expanded = @(Expand-MRExecutionArguments $executionProfile $values)
        $settingsIndex=[Array]::IndexOf([object[]]$expanded,'--settings')
        $settingsObject=$expanded[$settingsIndex+1]|ConvertFrom-Json
        $settings = $settingsObject.permissions.allow
        $path = Resolve-MRExecutionPath $temporary
        if ($role -ceq 'fast-mechanical') {
            if('--json-schema' -cin $expanded){throw 'agy plain JSON transport received incompatible schema flag.'}
            if ($settings.Count -ne 1 -or $settings[0] -cne "read_file($path)") { throw 'Read-only agy grants too much.' }
            if($settingsObject.toolPermission -cne 'request-review' -or 'write_file(*)' -cnotin $settingsObject.permissions.deny -or 'command(*)' -cnotin $settingsObject.permissions.deny){throw 'agy effective write/code denial missing.'}
        } elseif ($settings.Count -ne 2 -or "write_file($path)" -cnotin $settings) { throw 'agy write scope mismatch.' }
        $preflight = @(Expand-MRExecutionArguments $executionProfile $values -Preflight)
        if ('--settings' -cin $preflight) { throw 'agy preflight escalated.' }
    }
    $codex = @{agentFamily='Codex';routingRole='long-running-implementation';arguments=@('exec','--cd','{worktree}','{prompt}')}
    $expanded = @(Expand-MRExecutionArguments $codex $values)
    if ($expanded.Count -ne 4 -or $expanded[-1] -cne $values.prompt) { throw 'Codex argument semantics changed.' }
    $codexFast=@{agentFamily='Codex';routingRole='fast-mechanical';arguments=@('exec','--sandbox','read-only','--cd','{worktree}','{prompt}')}
    $codexReadOnly=@(Expand-MRExecutionArguments $codexFast $values)
    if((Get-MRReadOnlyToolBoundary $codexFast $codexReadOnly) -cne 'tools-withheld'){throw 'Codex read-only sandbox boundary unverified.'}
    $codexFast.arguments=@('exec','--sandbox','workspace-write','--cd','{worktree}','{prompt}')
    if((Get-MRReadOnlyToolBoundary $codexFast @(Expand-MRExecutionArguments $codexFast $values)) -cne 'unverified'){throw 'Writable Codex sandbox accepted as read-only.'}
    'PASS: production execution policy, role boundaries, preflight and JSON transport; offline.'
} finally { Remove-Item -LiteralPath $temporary -Recurse -Force }
