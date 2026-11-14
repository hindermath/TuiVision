#Requires -Version 7.3
<#
.SYNOPSIS
    Stabilen Assurance-Routing-Vertrag pruefen / Test stable Assurance routing.
.DESCRIPTION
    DE: Statische Abdeckung und echte stabile Resolver mit isolierten lokalen
    Fixtures pruefen. Keine Modellaufrufe, Downloads oder persoenlichen Profile.
    EN: Check coverage and actual stable resolvers with isolated local fixtures.
    No model calls, downloads or personal profiles.
.PARAMETER ModelRoutingRoot
    Entpackte Quelle von Model Routing v0.1.5 / Extracted v0.1.5 source.
.PARAMETER AutonomousRoot
    Entpackte Quelle von Autonomous Run v0.4.5 / Extracted v0.4.5 source.
.EXAMPLE
    pwsh -NoProfile -File tests/test-stable-routing.ps1 -ModelRoutingRoot ../model-routing -AutonomousRoot ../autonomous
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$ModelRoutingRoot,
    [Parameter(Mandatory)][string]$AutonomousRoot
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$ModelRoutingRoot = (Resolve-Path -LiteralPath $ModelRoutingRoot -ErrorAction Stop).ProviderPath
$AutonomousRoot = (Resolve-Path -LiteralPath $AutonomousRoot -ErrorAction Stop).ProviderPath
$temporary = Join-Path ([IO.Path]::GetTempPath()) ('assurance-routing-' + [guid]::NewGuid().ToString('N'))
$assertions = 0

function Assert-SDARouting {
    param([bool]$Condition, [string]$Message)
    if (-not $Condition) { throw "FAIL: $Message" }
    $script:assertions++
}

function Write-SDARoutingJson {
    param([string]$Path, $Value)
    $null = [IO.Directory]::CreateDirectory((Split-Path -Parent $Path))
    [IO.File]::WriteAllText($Path, ($Value | ConvertTo-Json -Depth 30), [Text.UTF8Encoding]::new($false))
}

function Get-SDARoutingSnapshot {
    # DE: Rohbytes binden; Normalisierung wuerde unzulaessige Schreibzugriffe verdecken.
    # EN: Bind raw bytes; normalization could hide unauthorized writes.
    $rows = @(Get-ChildItem -LiteralPath $temporary -File -Recurse -Force | ForEach-Object {
        $relative = [IO.Path]::GetRelativePath($temporary, $_.FullName).Replace('\', '/')
        "$relative $((Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash)"
    })
    [Array]::Sort($rows, [StringComparer]::Ordinal)
    return $rows -join "`n"
}

function ConvertTo-SDARoutingDiagnostic {
    param([string]$Text)
    # DE/EN: Strip presentation codes before console margins; keep every message word.
    $plain = [regex]::Replace($Text, '\x1B\[[0-?]*[ -/]*[@-~]', '')
    $plain = [regex]::Replace($plain, '\s*\|\s*', ' ')
    return [regex]::Replace($plain, '\s+', ' ')
}

function Invoke-SDARoutingProcess {
    param([string]$Script, [string[]]$Arguments, [bool]$ReadOnly = $true, [string]$HarnessFixtureDirectory = '')
    $before = Get-SDARoutingSnapshot
    $info = [Diagnostics.ProcessStartInfo]::new()
    $info.FileName = (Get-Command pwsh -CommandType Application -ErrorAction Stop | Select-Object -First 1).Source
    foreach ($argument in @('-NoProfile', '-File', $Script) + $Arguments) { $info.ArgumentList.Add($argument) }
    $info.WorkingDirectory = $temporary
    $info.UseShellExecute = $false
    $info.RedirectStandardOutput = $true
    $info.RedirectStandardError = $true
    if ($HarnessFixtureDirectory) {
        $info.Environment['PATH'] = $HarnessFixtureDirectory + [IO.Path]::PathSeparator + $info.Environment['PATH']
    }
    $process = [Diagnostics.Process]::Start($info)
    $stdout = $process.StandardOutput.ReadToEndAsync()
    $stderr = $process.StandardError.ReadToEndAsync()
    if (-not $process.WaitForExit(30000)) {
        $process.Kill($true)
        $process.WaitForExit()
        throw 'Fixture timed out; no retry.'
    }
    # DE: PowerShell kann Fehlermeldungen fuer die Konsole mit Zeilen-/Randmarken umbrechen.
    # EN: Ignore console wrapping only; exit codes and semantic diagnostics stay mandatory.
    $diagnosticText = ConvertTo-SDARoutingDiagnostic $stderr.GetAwaiter().GetResult()
    $result = @{ Code = $process.ExitCode; Out = $stdout.GetAwaiter().GetResult(); Error = $diagnosticText }
    $process.Dispose()
    if ($ReadOnly) { Assert-SDARouting ($before -ceq (Get-SDARoutingSnapshot)) 'read-only fixture changed files' }
    return $result
}

try {
    $null = [IO.Directory]::CreateDirectory($temporary)
    $message = "Routing-Rolle fuer speckit.secure-development-review muss 'frontier-reasoning' sein."
    $wrapped = $message.Replace("muss '", "muss`n | '")
    $escape = [string][char]27
    $styled = "${escape}[31;1m" + $wrapped.Replace("`n", "${escape}[0m`n${escape}[31;1m") + "${escape}[0m"
    Assert-SDARouting ((ConvertTo-SDARoutingDiagnostic $wrapped) -ceq $message) 'plain wrapped diagnostic retains exact message'
    Assert-SDARouting ((ConvertTo-SDARoutingDiagnostic $styled) -ceq $message) 'ANSI wrapped diagnostic retains exact message'
    $manifest = [IO.File]::ReadAllText((Join-Path $root 'preset.yml'))
    $contractPath = Join-Path $root 'model-routing.json'
    $contractText = [IO.File]::ReadAllText($contractPath)
    $contract = $contractText | ConvertFrom-Json -AsHashtable
    Assert-SDARouting ($contract.schemaVersion -ceq '1.0') 'schema 1.0 required'
    Assert-SDARouting ($contract.presetId -ceq 'secure-development-assurance-governance') 'preset ID'
    Assert-SDARouting ($contract.defaultRole -ceq 'frontier-reasoning') 'safe default role'
    Assert-SDARouting ($contract.fallbackPolicy -ceq 'fail-closed') 'fail-closed required'
    Assert-SDARouting ($contractText -notmatch '(?i)gpt-|claude-|gemini-') 'no model IDs in public contract'
    Assert-SDARouting ([regex]::IsMatch($manifest, '(?s)name: "secure-development-assurance-governance-model-routing"\s+file: "model-routing.json"')) 'manifest registration'
    $expected = @{
        'speckit.secure-development-status' = 'fast-mechanical'
        'speckit.secure-development-review' = 'frontier-reasoning'
    }
    $commands = [regex]::Matches($manifest, '(?m)^\s+name: "(speckit\.[^"]+)"\s*$')
    Assert-SDARouting ($commands.Count -eq 2 -and $contract.commands.Count -eq 2) 'exact command coverage'
    foreach ($command in $commands) {
        $name = $command.Groups[1].Value
        Assert-SDARouting ($expected.ContainsKey($name) -and $contract.commands.ContainsKey($name)) "coverage: $name"
        Assert-SDARouting ($contract.commands[$name] -ceq $expected[$name]) "role: $name"
        $body = [IO.File]::ReadAllText((Join-Path $root "commands/$name.md"))
        Assert-SDARouting ($body.Contains($expected[$name]) -and $body.Contains('model-routing.json')) "command guidance: $name"
    }
    foreach ($dependency in @(@($ModelRoutingRoot, '0.1.5'), @($AutonomousRoot, '0.4.5'))) {
        $dependencyManifest = [IO.File]::ReadAllText((Join-Path $dependency[0] 'preset.yml'))
        Assert-SDARouting ($dependencyManifest -match ('(?m)^  version: "' + [regex]::Escape($dependency[1]) + '"')) 'stable dependency version'
    }

    $catalogs = Join-Path $temporary 'catalogs'
    foreach ($source in @(@($root, 'assurance'), @($ModelRoutingRoot, 'model-routing'), @($AutonomousRoot, 'autonomous'))) {
        $destination = Join-Path $catalogs $source[1]
        $null = [IO.Directory]::CreateDirectory($destination)
        Copy-Item -LiteralPath (Join-Path $source[0] 'model-routing.json') -Destination $destination
    }
    $installedContract = Join-Path $catalogs 'assurance/model-routing.json'
    $feature = Join-Path $temporary 'repository/specs/999-assurance-fixture'
    $statePath = Join-Path $feature 'state.json'
    $state = @{
        schemaVersion = '1.1'; runId = 'assurance-fixture'; status = 'Active'
        routing = @{
            policy = 'balanced-v1'; fallbackPolicy = 'fail-closed'
            phases = @(
                @{ phaseId = 'status'; command = 'speckit.secure-development-status'; routingRole = 'fast-mechanical'; runnerProfile = 'N/A'; status = 'Pending'; dependsOn = @() },
                @{ phaseId = 'review'; command = 'speckit.secure-development-review'; routingRole = 'frontier-reasoning'; runnerProfile = 'N/A'; status = 'Pending'; dependsOn = @('status') }
            )
        }
    }
    Write-SDARoutingJson $statePath $state
    $trap = Join-Path $temporary 'do-not-run.ps1'
    [IO.File]::WriteAllText($trap, "throw 'A validation-only fixture must never execute a runner or preflight.'")
    $profiles = @{ schemaVersion = '2.0'; profiles = @{} }
    foreach ($role in @('fast-mechanical', 'frontier-reasoning')) {
        $profiles.profiles[$role] = @{
            routingRole = $role; agentFamily = 'Fixture'; model = 'synthetic-only'; reasoningEffort = 'fixture'
            executable = 'pwsh'; arguments = @('-NoProfile', '-File', $trap)
            preflight = @{ executable = 'pwsh'; arguments = @('-NoProfile', '-File', $trap) }
        }
    }
    $profilesPath = Join-Path $temporary 'synthetic-profiles.json'
    Write-SDARoutingJson $profilesPath $profiles
    $runner = Join-Path $AutonomousRoot 'scripts/invoke-autonomous-model-phase.ps1'
    $baseArguments = @('-State', $statePath, '-RoutingRoot', $catalogs, '-RunnerConfig', $profilesPath)
    $valid = Invoke-SDARoutingProcess $runner (@('-Action', 'Validate') + $baseArguments)
    Assert-SDARouting ($valid.Code -eq 0 -and $valid.Out.Contains('VALID')) "stable runner validation: $($valid.Error)"
    $status = Invoke-SDARoutingProcess $runner (@('-Action', 'Status') + $baseArguments)
    Assert-SDARouting ($status.Code -eq 0) "stable read-only status: $($status.Error)"

    # DE: Mutationen betreffen ausschliesslich die Wegwerfkopie, nie installierte Presets.
    # EN: Mutate only the disposable copy, never installed presets.
    foreach ($case in @('missing-file', 'missing-command', 'invalid-role', 'invalid-schema', 'unsafe-fallback')) {
        Copy-Item -LiteralPath $contractPath -Destination $installedContract -Force
        $mutated = $contractText | ConvertFrom-Json -AsHashtable
        switch ($case) {
            'missing-file' { Remove-Item -LiteralPath $installedContract }
            'missing-command' { $mutated.commands.Remove('speckit.secure-development-review'); Write-SDARoutingJson $installedContract $mutated }
            'invalid-role' { $mutated.commands['speckit.secure-development-review'] = 'unknown'; Write-SDARoutingJson $installedContract $mutated }
            'invalid-schema' { $mutated.schemaVersion = '3.0'; Write-SDARoutingJson $installedContract $mutated }
            'unsafe-fallback' { $mutated.fallbackPolicy = 'automatic'; Write-SDARoutingJson $installedContract $mutated }
        }
        $blocked = Invoke-SDARoutingProcess $runner (@('-Action', 'Validate') + $baseArguments)
        $diagnostic = switch ($case) {
            'missing-file' { 'Kein Routing-Vertrag' }
            'missing-command' { 'Kein Routing-Vertrag' }
            'invalid-role' { 'Ungueltige Rolle' }
            'invalid-schema' { 'Routing-Schema muss 1.0 sein' }
            'unsafe-fallback' { 'fallbackPolicy muss fail-closed sein' }
        }
        Assert-SDARouting ($blocked.Code -ne 0 -and $blocked.Error.Contains($diagnostic)) "expected blocker: $case"
    }
    Copy-Item -LiteralPath $contractPath -Destination $installedContract -Force
    $overlap = @{
        schemaVersion = '1.0'; presetId = 'weaker-fixture'; defaultRole = 'frontier-reasoning'; fallbackPolicy = 'fail-closed'
        commands = @{ 'speckit.secure-development-review' = 'fast-mechanical' }
    }
    Write-SDARoutingJson (Join-Path $catalogs 'weaker/model-routing.json') $overlap
    $strong = Invoke-SDARoutingProcess $runner (@('-Action', 'Validate') + $baseArguments)
    Assert-SDARouting ($strong.Code -eq 0) 'strongest overlapping role preserved'
    $state.routing.phases[1].routingRole = 'fast-mechanical'
    Write-SDARoutingJson $statePath $state
    $wrong = Invoke-SDARoutingProcess $runner (@('-Action', 'Validate') + $baseArguments)
    Assert-SDARouting ($wrong.Code -ne 0 -and $wrong.Error.Contains("muss 'frontier-reasoning' sein")) "weaker phase role blocked (exit=$($wrong.Code)): $($wrong.Error)"
    $state.routing.phases[1].routingRole = 'frontier-reasoning'
    $state.routing.phases[1].runnerProfile = 'fast-mechanical'
    Write-SDARoutingJson $statePath $state
    $wrongProfile = Invoke-SDARoutingProcess $runner (@('-Action', 'Validate') + $baseArguments)
    Assert-SDARouting ($wrongProfile.Code -ne 0 -and $wrongProfile.Error.Contains('falsche routingRole')) 'wrong profile blocked'
    $state.routing.phases[1].runnerProfile = 'N/A'
    Write-SDARoutingJson $statePath $state
    $profiles.profiles.Remove('frontier-reasoning')
    Write-SDARoutingJson $profilesPath $profiles
    $missingProfile = Invoke-SDARoutingProcess $runner (@('-Action', 'Validate') + $baseArguments)
    Assert-SDARouting ($missingProfile.Code -ne 0 -and $missingProfile.Error.Contains('benoetigt genau ein lokales Runner-Profil')) 'missing profile blocked'

    $resolver = Join-Path $ModelRoutingRoot 'scripts/resolve-model-routing.ps1'
    # DE: Der stabile Resolver verlangt auch bei Discovery-Fixtures --version.
    # EN: Provide a version-only double; never depend on a signed-in native CLI.
    $fixtureBin = Join-Path $temporary 'bin'
    $null = [IO.Directory]::CreateDirectory($fixtureBin)
    if ($IsWindows) {
        $versionShim = @'
@echo off
if not "%~1"=="--version" exit /b 97
if not "%~2"=="" exit /b 97
echo codex-fixture 0.0.0
exit /b 0
'@
        [IO.File]::WriteAllText((Join-Path $fixtureBin 'codex.cmd'), ($versionShim.Replace("`r`n", "`n").Replace("`n", "`r`n") + "`r`n"), [Text.UTF8Encoding]::new($false))
    } else {
        $versionShim = @'
#!/usr/bin/env bash
set -euo pipefail
[[ "$#" == 1 && "$1" == --version ]] || exit 97
printf 'codex-fixture 0.0.0\n'
'@
        $shimPath = Join-Path $fixtureBin 'codex'
        [IO.File]::WriteAllText($shimPath, $versionShim.Replace("`r`n", "`n"), [Text.UTF8Encoding]::new($false))
        [IO.File]::SetUnixFileMode($shimPath, [IO.UnixFileMode]::UserRead -bor [IO.UnixFileMode]::UserWrite -bor [IO.UnixFileMode]::UserExecute)
    }
    $discoveryFixture = Join-Path $ModelRoutingRoot 'tests/fixtures/codex-models.json'
    $discoveredPath = Join-Path $temporary 'fixture-discovery.json'
    $discoveryArguments = @('-Harness', 'Codex', '-ConfigPath', $discoveredPath, '-DiscoveryFixture', $discoveryFixture, '-RoutingRoot', $catalogs, '-OutputFormat', 'Json')
    $refresh = Invoke-SDARoutingProcess $resolver (@('-Action', 'Refresh') + $discoveryArguments) -ReadOnly $false -HarnessFixtureDirectory $fixtureBin
    Assert-SDARouting ($refresh.Code -eq 0) "fixture-only stable discovery: $($refresh.Error)"
    $discovered = Get-Content -LiteralPath $discoveredPath -Raw | ConvertFrom-Json -AsHashtable
    Assert-SDARouting ($discovered.schemaVersion -eq '2.0' -and $discovered.catalogSha256 -match '^[0-9a-f]{64}$') 'schema 2 local fixture and catalog binding'
    Assert-SDARouting ($discovered.harness.version -ceq 'codex-fixture 0.0.0') 'only the owned version double was queried'
    $discoveryStatus = Invoke-SDARoutingProcess $resolver (@('-Action', 'Status') + $discoveryArguments) -HarnessFixtureDirectory $fixtureBin
    Assert-SDARouting ($discoveryStatus.Code -eq 0) "stable resolver status: $($discoveryStatus.Error)"
    "PASS: $assertions stable Assurance routing assertions; no model processes or personal profiles."
} finally {
    # DE/EN: Remove only this test's uniquely owned temporary fixture.
    if (Test-Path -LiteralPath $temporary) { Remove-Item -LiteralPath $temporary -Recurse -Force }
}
exit 0
