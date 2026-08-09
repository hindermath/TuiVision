#Requires -Version 7.0
[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $PSScriptRoot
$scriptPath = Join-Path $root 'scripts/resolve-model-routing.ps1'
$fixture = Join-Path $PSScriptRoot 'fixtures/codex-models.json'
$ambiguous = Join-Path $PSScriptRoot 'fixtures/ambiguous-models.json'
$temporary = Join-Path ([IO.Path]::GetTempPath()) "model-routing-test-$PID"
[void](New-Item -ItemType Directory -Path $temporary -Force)
try {
    $config = Join-Path $temporary 'profiles.json'
    & $scriptPath -Action Refresh -Harness Codex -ConfigPath $config -DiscoveryFixture $fixture -OutputFormat Json | Out-Null
    if ($LASTEXITCODE -ne 0) { throw 'Positive refresh failed.' }
    $data = Get-Content -LiteralPath $config -Raw -Encoding utf8 | ConvertFrom-Json -AsHashtable
    if ([string] $data.schemaVersion -ne '2.0') { throw 'Schema 2.0 missing.' }
    if (@($data.profiles.Keys).Count -ne 4) { throw 'Exactly four profiles expected.' }
    & $scriptPath -Action Status -Harness Codex -ConfigPath $config -DiscoveryFixture $fixture -OutputFormat Json | Out-Null
    if ($LASTEXITCODE -notin @(0, 2)) { throw 'Read-only status failed.' }
    & $scriptPath -Action Refresh -Harness Codex -ConfigPath (Join-Path $temporary 'blocked.json') -DiscoveryFixture $ambiguous -OutputFormat Json | Out-Null
    if ($LASTEXITCODE -ne 3) { throw 'Ambiguous fixture did not fail closed.' }
    Write-Output 'PASS: model-routing discovery, refresh, status, and ambiguity fixtures'
} finally {
    Remove-Item -LiteralPath $temporary -Recurse -Force -ErrorAction SilentlyContinue
}
