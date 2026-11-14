#Requires -Version 7.0
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if (-not $IsMacOS) {
    Write-Output 'SKIP: the historical field wrapper is macOS-only.'
    exit 0
}

$wrapper = Join-Path $PSScriptRoot 'run-native-secure-casetracker-field.ps1'
$root = Join-Path ([IO.Path]::GetTempPath()) ('parallel-field-preview-' + [guid]::NewGuid().ToString('N'))
try {
    [void](New-Item -ItemType Directory -Path $root)
    '{}' | Set-Content -LiteralPath (Join-Path $root 'parallel-campaign.json') -Encoding utf8NoBOM
    '{}' | Set-Content -LiteralPath (Join-Path $root 'parallel-runner-profiles.json') -Encoding utf8NoBOM

    foreach ($action in @('Start', 'Stop', 'Resume', 'Consolidate')) {
        $arguments = @('-NoProfile', '-File', $wrapper, '-Action', $action, '-OutputRoot', $root, '-WhatIf')
        if ($action -eq 'Consolidate') { $arguments += '-Merge' }
        $output = @(& pwsh @arguments 2>&1)
        $expected = if ($action -eq 'Stop') { "Preview: $action would invoke" } else { "Preview: $action is blocked" }
        if ($LASTEXITCODE -ne 0 -or -not ($output -match $expected)) {
            throw "-WhatIf did not stop $action before child dispatch: $($output -join ' ')"
        }
        if ((Test-Path -LiteralPath (Join-Path $root 'parallel-campaign-state.json')) -or
            (Test-Path -LiteralPath (Join-Path $root 'runtime'))) {
            throw "-WhatIf wrote campaign state for $action."
        }
    }
    foreach ($action in @('Prepare', 'Start', 'Resume', 'Consolidate')) {
        $arguments = @('-NoProfile', '-File', $wrapper, '-Action', $action, '-OutputRoot', $root)
        if ($action -eq 'Consolidate') { $arguments += '-Merge' }
        $output = @(& pwsh @arguments 2>&1)
        if ($LASTEXITCODE -eq 0 -or -not ($output -match 'is retired')) {
            throw "Historical native action was not blocked: $action $($output -join ' ')"
        }
    }
    $output = @(& pwsh -NoProfile -File $wrapper -Action Consolidate -OutputRoot $root 2>&1)
    if ($LASTEXITCODE -eq 0 -or -not ($output -match 'is retired')) {
        throw 'Historical non-merge consolidation was not blocked.'
    }
    Write-Output 'PASS: preview stops before dispatch and retired native actions are blocked.'
} finally {
    if (Test-Path -LiteralPath $root) { Remove-Item -LiteralPath $root -Recurse -Force }
}
