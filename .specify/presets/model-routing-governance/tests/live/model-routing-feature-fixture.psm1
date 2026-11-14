Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
function New-MRSmokeFeatureFixture {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Worktree,[Parameter(Mandatory)][string]$AutonomousRunnerSource)
    if(-not $PSCmdlet.ShouldProcess($Worktree,'Prepare synthetic accepted feature')){return}
    $source=Join-Path $PSScriptRoot '../fixtures/integration-feature'
    $feature=Join-Path $Worktree 'specs/999-routing-integration'
    [void](New-Item -ItemType Directory -Path $feature -Force)
    foreach($name in @('spec.md','plan.md','tasks.md','research.md','data-model.md','quickstart.md','checklists')){Copy-Item -LiteralPath (Join-Path $source $name) -Destination $feature -Recurse -Force}
    Copy-Item (Join-Path $source 'AGENTS.md') $Worktree
    $memory=Join-Path $Worktree '.specify/memory'
    [void](New-Item -ItemType Directory -Path $memory -Force)
    Copy-Item (Join-Path $source 'constitution.md') $memory
    foreach($shell in @('powershell','bash')){
        $destination=Join-Path $Worktree ".specify/scripts/$shell"
        [void](New-Item -ItemType Directory -Path $destination -Force)
        $extension=if($shell -eq 'bash'){'sh'}else{'ps1'}
        Copy-Item (Join-Path $source "check-prerequisites.$extension") $destination
    }
    Copy-Item (Join-Path $AutonomousRunnerSource 'templates/autonomous-phase-result-template.json') $Worktree
    $state=Get-Content (Join-Path $AutonomousRunnerSource 'templates/autonomous-run-state-template.json') -Raw | ConvertFrom-Json -AsHashtable
    $state.runId=[guid]::NewGuid().ToString();$state.featurePath='specs/999-routing-integration';$state.branch='999-routing-integration'
    $state.authorityRevalidationRequired=$false;$state.stage='Implement';$state.status='Active'
    $state.acceptedArtifacts=@(foreach($name in @('spec.md','plan.md')){@{path="specs/999-routing-integration/$name";sha256=(Get-FileHash (Join-Path $feature $name)).Hash.ToLowerInvariant()}})
    $state.tasks=@{path='specs/999-routing-integration/tasks.md';sha256=(Get-FileHash (Join-Path $feature 'tasks.md')).Hash.ToLowerInvariant();completed=0;total=1}
    $state.routing.phases=@(@{phaseId='implementation';command='speckit.implement';routingRole='long-running-implementation';runnerProfile='N/A';dependsOn=@();status='Pending'})
    $state.lastPassingGate='SyntheticFixtureReadiness';$state.nextExactAction='Implement accepted task T001 in this test repository.'
    $state.updatedAt=[DateTime]::UtcNow.ToString('o')
    $path=Join-Path $feature 'autonomous-run-state.json'
    $state | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $path
    return $path
}
Export-ModuleMember -Function New-MRSmokeFeatureFixture
