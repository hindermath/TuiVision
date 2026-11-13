#Requires -Version 7.3
<#
.SYNOPSIS
    DE: Fuehrt Smoke und normale Runner-Integration gemeinsam aus.
    EN: Runs smoke and production runner integration together.
.DESCRIPTION
    DE: Manuelle Live-Pruefung mit interaktiven Anmeldungen. Fuehrt beide Stufen
    sequenziell aus, auch nach einem Fehler; schreibt einen lokalen Gesamtbericht.
    EN: Manual live check using interactive sign-ins. Runs both stages sequentially,
    including after a failure, and writes a local combined report.
.PARAMETER ConfirmLiveRun
    DE: Bestaetigt Modellaufrufe und Kontingentverbrauch. EN: Confirms model calls and quota use.
.PARAMETER AutonomousRunnerSource
    DE: Sauberer lokaler Autonomous-Runner-Quellkandidat. EN: Clean local autonomous-runner source candidate.
.PARAMETER MaxRetries
    DE: Zusaetzliche Versuche je Harness und Stufe, 0 bis 3 (Standard 0).
    EN: Additional attempts per harness and stage, 0 through 3 (default 0).
.PARAMETER IncludeReadOnlyProbe
    DE: Ergaenzt eine dritte Stufe mit negativem Schreibtest.
    EN: Adds a third stage with a negative write test.
.EXAMPLE
    pwsh -NoProfile -File tests/live/invoke-model-routing-validation.ps1 -ConfirmLiveRun -MaxRetries 3 -AutonomousRunnerSource /path/to/autonomous-run-governance
#>
[CmdletBinding()]
param(
    [switch] $ConfirmLiveRun,
    [Parameter(Mandatory)][string] $AutonomousRunnerSource,
    [ValidateRange(0,3)][int] $MaxRetries = 0,
    [switch] $IncludeReadOnlyProbe
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
if (-not $ConfirmLiveRun) { throw 'Live validation requires -ConfirmLiveRun.' }
$repository = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
Import-Module (Join-Path $PSScriptRoot 'model-routing-smoke-core.psm1') -Force
Import-Module (Join-Path $PSScriptRoot 'model-routing-validation-core.psm1') -Force
$AutonomousRunnerSource = Resolve-MRSmokePhysicalPath $AutonomousRunnerSource
if (-not (Test-Path (Join-Path $AutonomousRunnerSource 'scripts/invoke-autonomous-model-phase.ps1') -PathType Leaf)) { throw 'Production phase runner source is missing.' }
$consumerHead = (& git -C $AutonomousRunnerSource rev-parse HEAD).Trim()
if ($LASTEXITCODE -ne 0) { throw 'Production source HEAD unavailable.' }
$consumerStatus = @(& git -C $AutonomousRunnerSource -c core.fsmonitor=false status --porcelain --untracked-files=all)
if ($LASTEXITCODE -ne 0 -or $consumerStatus.Count) { throw 'Production source must be clean.' }
$head = (& git -C $repository rev-parse HEAD).Trim()
$started = [DateTime]::UtcNow
$runner = Join-Path $PSScriptRoot 'invoke-model-routing-smoke.ps1'
$reportRoot = Join-Path $repository '.test-results/model-routing-smoke'
$summaryPath = Join-Path $reportRoot ("validation-pair-{0}-{1}.json" -f $started.ToString('yyyyMMddTHHmmssZ'),$head.Substring(0,12))
$runStage = {
    param($mode)
    Write-Host "Starting paired stage: $mode (MaxRetries=$MaxRetries)"
    $arguments = @('-NoProfile','-File',$runner,'-ConfirmLiveRun','-MaxRetries',[string]$MaxRetries)
    if ($mode -ceq 'production-phase-runner') { $arguments += @('-AutonomousRunnerSource',$AutonomousRunnerSource) }
    if ($mode -ceq 'read-only-probe') { $arguments += '-ReadOnlyProbe' }
    $reportLines = [Collections.Generic.List[string]]::new()
    & pwsh @arguments | ForEach-Object {
        $line = [string]$_
        if ($line.StartsWith('{"harness":')) { Write-Host $line }
        if ($line.StartsWith('Report: ')) { $reportLines.Add($line.Substring(8));Write-Host $line }
    }
    # GetNewClosure captures a stale local LASTEXITCODE. Native commands update
    # the global automatic variable, which must be read before any other command.
    $exitCode = $global:LASTEXITCODE
    if ($reportLines.Count -ne 1) { throw "Stage $mode did not produce exactly one report; exit $exitCode." }
    $path = $reportLines[0]
    $report = Get-Content -LiteralPath $path -Raw | ConvertFrom-Json -AsHashtable -Depth 50
    if ([string]$report.testId -cne 'model-routing-live-smoke' -or [string]$report.schemaVersion -notin @('1.0','1.1')) { throw 'Stage report schema unsupported.' }
    $status = [string]$report.finalStatus
    if ($exitCode -ne 0 -and $status -in @('PASS','PASS_WITH_RETRY')) { $status='INFRA_ERROR' }
    return [ordered]@{mode=$mode;finalStatus=$status;exitCode=$exitCode;reportPath=$path
        reportSha256=(Get-FileHash $path -Algorithm SHA256).Hash.ToLowerInvariant()
        harnesses=@($report.harnesses | ForEach-Object { @{harness=$_.harness;finalStatus=$_.finalStatus;attempts=@($_.attempts).Count} })}
}.GetNewClosure()
$pair = Invoke-MRValidationPair -RunStage $runStage -IncludeReadOnlyProbe:$IncludeReadOnlyProbe
$consumerAfter = @(& git -C $AutonomousRunnerSource -c core.fsmonitor=false status --porcelain --untracked-files=all)
$consumerUnchanged = $LASTEXITCODE -eq 0 -and ($consumerAfter -join "`n") -ceq ($consumerStatus -join "`n") -and
    ((& git -C $AutonomousRunnerSource rev-parse HEAD).Trim() -ceq $consumerHead)
if (-not $consumerUnchanged) { $pair.finalStatus='FAILED' }
$summary = [ordered]@{schemaVersion='1.0';testId='model-routing-paired-validation';startedAt=$started.ToString('o')
    finishedAt=[DateTime]::UtcNow.ToString('o');repositoryCommit=$head;autonomousRunnerCommit=$consumerHead
    maxRetries=$MaxRetries;includeReadOnlyProbe=[bool]$IncludeReadOnlyProbe;consumerUnchanged=$consumerUnchanged;finalStatus=$pair.finalStatus;stages=$pair.stages}
Write-MRSmokeJsonAtomic -Path $summaryPath -Value $summary
$summary | ConvertTo-Json -Depth 20
Write-Host "Combined report: $summaryPath"
if ($summary.finalStatus -notin @('PASS','PASS_WITH_RETRY')) { exit 1 }
