#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Prueft Diagnose, Feature-Fixture und Prozessbesitz offline.
    EN: Tests diagnostics, feature fixture and process ownership offline.
.DESCRIPTION
    DE: Keine Modelle, Anbieter oder Netzwerkzugriffe. Verwendet synthetische Artefakte.
    EN: No models, providers or network access. Uses synthetic artifacts.
.EXAMPLE
    pwsh -NoProfile -File tests/test-validation-hardening.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$live=Join-Path $PSScriptRoot 'live'
Import-Module (Join-Path $live 'model-routing-smoke-core.psm1') -Force
Import-Module (Join-Path $live 'model-routing-process-ownership.psm1') -Force
Import-Module (Join-Path $live 'model-routing-feature-fixture.psm1') -Force
$temporary=Join-Path ([IO.Path]::GetTempPath()) "validation-hardening-$([guid]::NewGuid().ToString('N'))"
[void](New-Item -ItemType Directory -Path $temporary)
try{
    $refused=$false
    try{Remove-MRSmokeTemporaryRoot -Path $temporary -Confirm:$false}catch{$refused=$true}
    if(-not $refused -or -not(Test-Path $temporary)){throw 'Unsafe cleanup target was accepted.'}
    foreach($invalid in @($null,@{},@{outcome='Blocked'},@{read=$true;writeDenied=$false},@{read='true';writeDenied='true'})){
        if(Test-MRSmokeReadOnlyAttestation $invalid){throw 'Invalid read-only attestation accepted.'}
    }
    if(-not(Test-MRSmokeReadOnlyAttestation @{read=$true;writeDenied=$true;writeAttempted=$true;writeUnavailable=$false})){throw 'Valid denied-attempt attestation rejected.'}
    $withheld=@{read=$true;writeDenied=$false;writeAttempted=$false;writeUnavailable=$true}
    if(Test-MRSmokeReadOnlyAttestation $withheld){throw 'Unverified tool-unavailability accepted.'}
    if(-not(Test-MRSmokeReadOnlyAttestation $withheld -ToolBoundary 'tools-withheld')){throw 'Verified withholding rejected.'}
    if(-not(Test-MRSmokeHostWriteDenial @{exitCode=0;stderr='tool required the "write_file" permission so it was auto-denied'})){throw 'Host denial not recognised.'}
    if(-not(Test-MRSmokeHostWriteDenial @{exitCode=0;stdout='Permission denied for write_file(/tmp/worktree/forbidden.txt). Matches user-configured deny rule.';stderr=''})){throw 'Current agy stdout host denial not recognised.'}
    if(Test-MRSmokeHostWriteDenial @{exitCode=0;stdout='Permission denied for read_file(/tmp/worktree/input.txt). Matches user-configured deny rule.';stderr=''}){throw 'Read denial mistaken for current agy write protection.'}
    if(Test-MRSmokeHostWriteDenial @{exitCode=0;stderr='tool required the "read_file" permission so it was auto-denied'}){throw 'Read denial mistaken for write protection.'}
    if(Test-MRSmokeHostWriteDenial @{exitCode=7;stderr='tool required the "write_file" permission so it was auto-denied'}){throw 'Unrelated process failure accepted.'}
    $result=Get-MRSmokeAgentResultDiagnostic -StandardOutput '{"outcome":"Blocked","blockedReason":"password=secret-value at /test/root","phaseId":"implementation","transcript":"must not retain"}' -RedactValues @('secret-value') -PathReplacements @{'/test/root'='<WORKTREE>'}
    if($result.outcome -cne 'Blocked' -or $result.blockedReason -match 'secret-value|/test/root' -or $result.Contains('transcript')){throw 'Phase-result redaction/allowlist failed.'}
    $long=Get-MRSmokeAgentResultDiagnostic -StandardOutput ('{"outcome":"Blocked","blockedReason":"'+('x'*10000)+'"}')
    if($long.blockedReason.Length -gt 4200 -or $long.blockedReason -notmatch '<truncated:'){throw 'Blocked reason is unbounded.'}
    $multibyte=ConvertTo-MRSmokeDiagnosticText -Text (([char]0x20ac).ToString()*20000) -MaxBytes 1024
    if([Text.Encoding]::UTF8.GetByteCount($multibyte) -gt 1024 -or $multibyte.Contains([char]0xfffd)){throw 'UTF-8 limit or truncation boundary failed.'}
    if(Test-MRSmokeCompilerIdentity '/usr/bin/dotnet' 10 10){throw 'Unrelated dotnet accepted.'}
    if(Test-MRSmokeCompilerIdentity '/tmp/VBCSCompiler' 10 11){throw 'Recycled PID identity accepted.'}
    if(-not(Test-MRSmokeCompilerIdentity '/tmp/VBCSCompiler' 10 10)){throw 'Compiler identity rejected.'}
    $emptyCleanup=Stop-MRSmokeOwnedCompilers -Confirm:$false
    if($emptyCleanup.tracked -ne 0 -or $emptyCleanup.failed -ne 0){throw 'Unowned compiler selected.'}
    $templateRoot=Join-Path $temporary 'consumer/templates'
    [void](New-Item -ItemType Directory -Path $templateRoot -Force)
    @{schemaVersion='1.1';routing=@{phases=@()}}|ConvertTo-Json -Depth 10|Set-Content (Join-Path $templateRoot 'autonomous-run-state-template.json')
    '{}'|Set-Content (Join-Path $templateRoot 'autonomous-phase-result-template.json')
    $worktree=Join-Path $temporary 'worktree'
    [void](New-Item -ItemType Directory -Path $worktree)
    $statePath=New-MRSmokeFeatureFixture -Worktree $worktree -AutonomousRunnerSource (Split-Path -Parent $templateRoot) -Confirm:$false
    $state=Get-Content $statePath -Raw|ConvertFrom-Json -AsHashtable
    if($state.tasks.total -ne 1 -or $state.routing.phases[0].routingRole -cne 'long-running-implementation' -or $state.acceptedArtifacts.Count -ne 2){throw 'Feature readiness state incomplete.'}
    $checker=Join-Path $worktree '.specify/scripts/powershell/check-prerequisites.ps1'
    $reply=& pwsh -NoProfile -File $checker -Json -RequireTasks -IncludeTasks
    if($LASTEXITCODE -ne 0){throw 'Complete fixture rejected.'}
    $paths=$reply|ConvertFrom-Json
    if('tasks.md' -cnotin $paths.AVAILABLE_DOCS -or -not(Test-Path $paths.FEATURE_DIR)){throw 'Prerequisite paths incorrect.'}
    Remove-Item -LiteralPath (Join-Path $paths.FEATURE_DIR 'plan.md')
    $null=& pwsh -NoProfile -File $checker -Json 2>&1
    if($LASTEXITCODE -eq 0){throw 'Incomplete fixture accepted.'}
    $child=Join-Path $live 'invoke-smoke-child.ps1'
    $fixture=Join-Path $temporary 'timeout.ps1'
    'Start-Sleep -Seconds 3'|Set-Content $fixture
    try{
        $null=Invoke-MRSmokeProcess -ChildScript $child -Executable pwsh -Arguments @('-NoProfile','-File',$fixture) -WorkingDirectory $worktree -TimeoutSeconds 0.3
        throw 'Timeout not enforced.'
    }catch{
        if(-not $_.Exception.Data.Contains('MRSmokeProcess')){throw}
        if($_.Exception.Data['MRSmokeProcess'].seconds -lt 0.2){throw 'Timeout metadata missing.'}
    }
    'PASS: blocked-reason redaction, timeout evidence, identity guards, complete and incomplete fixtures; offline.'
}finally{Remove-Item -LiteralPath $temporary -Recurse -Force}
