#Requires -Version 7.0
<#
.SYNOPSIS
DE: CI-Testfehler duerfen nicht verdeckt werden. EN: CI test failures must not be masked.
.DESCRIPTION
DE: Prueft die veroeffentlichte Aufrufsequenz mit isolierten Exitcode-Fixtures;
keine Modelle, Netzwerke, Provider oder Verbraucher-Aenderungen.
EN: Replays the actual workflow sequence with isolated exit-code fixtures;
no models, network, providers or consumer changes.
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$workflow = [IO.File]::ReadAllText((Join-Path $root '.github/workflows/adapter-discovery.yml'))
$match = [regex]::Match($workflow, '(?s)- name: Adapter and unchanged legacy contracts \(offline\)\r?\n\s+shell: pwsh\r?\n\s+run: \|\r?\n(?<body>.*)\z')
if (-not $match.Success) { throw 'Actual adapter test step not found' }
$body = [regex]::Replace($match.Groups['body'].Value, '(?m)^          ', '')
$calls = @([regex]::Matches($body, '(?m)^pwsh -NoProfile -File (?<path>tests/[^\r\n]+)\r?$'))
if ($calls.Count -lt 3) { throw 'Native test calls not found' }
foreach ($call in $calls) {
    $remaining = $body.Substring($call.Index + $call.Length).TrimStart("`r", "`n")
    if (-not $remaining.StartsWith('if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }')) {
        throw "Missing immediate exit check after $($call.Groups['path'].Value)"
    }
}
$temporary = Join-Path ([IO.Path]::GetTempPath()) "ci-exit-codes-$([guid]::NewGuid())"
$fixture = Join-Path $temporary 'repo'
$replay = Join-Path $temporary 'replay.ps1'
$targets = @('tests/test-live-smoke-contract.ps1', 'tests/test-execution-policy.ps1')
try {
    [void](New-Item -ItemType Directory -Path (Join-Path $fixture 'tests') -Force)
    & git -C $fixture init --quiet
    if ($LASTEXITCODE -ne 0) { throw 'Fixture Git initialization failed' }
    foreach ($call in $calls) { [IO.File]::WriteAllText((Join-Path $fixture $call.Groups['path'].Value), "exit 0`n") }
    foreach ($name in @('test-sdk-bridge.mjs','test-adapter-transports.mjs')) {
        [IO.File]::WriteAllText((Join-Path $fixture "tests/$name"), '// Offline workflow stub; no providers.');
    }
    # Keep replay outside the fixture so the workflow's real cleanliness gate runs.
    $prefix = "`$ErrorActionPreference = 'Stop'`n`$PSNativeCommandUseErrorActionPreference = `$false`nSet-Location -LiteralPath '" + $fixture.Replace("'", "''") + "'`n"
    foreach ($target in @('') + $targets) {
        foreach ($path in $targets) { [IO.File]::WriteAllText((Join-Path $fixture $path), $(if ($path -ceq $target) { "exit 23`n" } else { "exit 0`n" })) }
        & git -C $fixture add --all
        if ($LASTEXITCODE -ne 0) { throw 'Fixture staging failed' }
        & git -C $fixture -c user.name=Fixture -c user.email=fixture@example.invalid commit --quiet --allow-empty -m fixture
        if ($LASTEXITCODE -ne 0) { throw 'Fixture commit failed' }
        [IO.File]::WriteAllText($replay, $prefix + $body + "`nexit `$LASTEXITCODE`n")
        $output = @(& pwsh -NoProfile -File $replay 2>&1)
        $actual = $LASTEXITCODE
        $expected = if ($target) { 23 } else { 0 }
        if ($actual -ne $expected) { throw "Actual CI step target '${target}': expected ${expected}, got ${actual}: $($output -join ' ')" }
        if ($target) {
            # Mutation proof: removing exactly the former missing guard reproduces
            # the original false success, including all later workflow gates.
            $broken = $body.Replace("pwsh -NoProfile -File $target`nif (`$LASTEXITCODE -ne 0) { exit `$LASTEXITCODE }", "pwsh -NoProfile -File $target")
            $broken = $broken.Replace("pwsh -NoProfile -File $target`r`nif (`$LASTEXITCODE -ne 0) { exit `$LASTEXITCODE }", "pwsh -NoProfile -File $target")
            if ($broken -ceq $body) { throw 'Guard mutation did not apply' }
            [IO.File]::WriteAllText($replay, $prefix + $broken + "`nexit `$LASTEXITCODE`n")
            $null = & pwsh -NoProfile -File $replay 2>&1
            if ($LASTEXITCODE -ne 0) { throw 'Mutation must reproduce the original masked failure' }
        }
    }
    Write-Output "PASS: $($calls.Count) immediate native-test guards; real workflow replay, two injected failures and two masking mutations"
} finally {
    if (Test-Path -LiteralPath $temporary) { Remove-Item -LiteralPath $temporary -Recurse -Force }
}
