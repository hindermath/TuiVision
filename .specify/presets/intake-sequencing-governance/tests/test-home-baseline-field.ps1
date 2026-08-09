[CmdletBinding()]
param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../../..')).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RequestPath = Join-Path $RepositoryRoot 'specs/active-lastenheft-normalization/intake-review-request.json'
$ResultPath = Join-Path $RepositoryRoot 'specs/active-lastenheft-normalization/intake-review-result.json'
$LiveManifestPath = Join-Path $RepositoryRoot 'requirements/intakes/series/home-baseline-delivery/manifest.json'
$Request = Get-Content -LiteralPath $RequestPath -Raw | ConvertFrom-Json -Depth 50
$Result = Get-Content -LiteralPath $ResultPath -Raw | ConvertFrom-Json -Depth 50
$LiveManifest = Get-Content -LiteralPath $LiveManifestPath -Raw | ConvertFrom-Json -Depth 50

if ($Request.mode -ne 'Series' -or $Result.status -ne 'Ready') {
    throw 'The accepted Home Baseline Series evidence is not Ready.'
}
if ($Request.targets.Count -ne $Result.targets.Count -or
    $Request.targets.Count -ne $Request.series.orderedTargetPaths.Count) {
    throw 'The accepted request, result, and ordered target set have different cardinalities.'
}

$RequestPaths = @($Request.series.orderedTargetPaths)
$LivePaths = @($LiveManifest.orderedTargets | ForEach-Object { $_.path })
$RequestRoots = @($Request.series.roots)
$LiveRoots = @($LiveManifest.roots)
$RequestEdges = @(
    $Request.series.dependencies |
        ForEach-Object { '{0}|{1}|{2}' -f $_.from, $_.to, $_.kind }
)
$LiveEdges = @(
    $LiveManifest.dependencies |
        ForEach-Object { '{0}|{1}|{2}' -f $_.from, $_.to, $_.kind }
)
if (($RequestPaths -join "`n") -ne ($LivePaths -join "`n") -or
    ($RequestRoots -join "`n") -ne ($LiveRoots -join "`n") -or
    ($RequestEdges -join "`n") -ne ($LiveEdges -join "`n")) {
    throw 'The accepted review topology does not match the active Home Baseline Series manifest.'
}

$ResultByPath = @{}
foreach ($Target in $Result.targets) { $ResultByPath[$Target.path] = $Target }
$Targets = foreach ($Path in $Request.series.orderedTargetPaths) {
    $RequestTarget = $Request.targets | Where-Object path -EQ $Path
    [ordered]@{
        path = $Path
        role = $RequestTarget.role
        normalizedSha256 = $ResultByPath[$Path].normalizedSha256
        status = if ($Path -eq $Request.series.orderedTargetPaths[0]) { 'Eligible' } else { 'Blocked' }
    }
}
$Dependencies = foreach ($Dependency in $Request.series.dependencies) {
    [ordered]@{
        from = $Dependency.from
        to = $Dependency.to
        kind = $Dependency.kind
        binding = $true
    }
}
$Manifest = [ordered]@{
    schemaVersion = '1.0'
    documentType = 'IntakeSeriesManifest'
    seriesId = $Request.reviewId
    title = 'Home Baseline active requirements-governance series'
    policy = $Request.policy
    status = 'Ready'
    orderedTargets = @($Targets)
    roots = @($Request.series.roots)
    dependencies = @($Dependencies)
    evidencePaths = @(
        'specs/active-lastenheft-normalization/intake-review-request.json',
        'specs/active-lastenheft-normalization/intake-review-result.json'
    )
}

$TempRoot = Join-Path ([IO.Path]::GetTempPath()) "intake-series-field-$([Guid]::NewGuid())"
New-Item -ItemType Directory -Path $TempRoot -Force | Out-Null
try {
    $ManifestPath = Join-Path $TempRoot 'manifest.json'
    $Manifest | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $ManifestPath -Encoding utf8NoBOM
    $PresetRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)
    & bash (Join-Path $PresetRoot 'scripts/validate-intake-series-manifest.sh') `
        --file $ManifestPath --repo $RepositoryRoot
    if ($LASTEXITCODE -ne 0) { throw 'Bash LegacyAdoption validation failed.' }
    & pwsh -NoProfile -File (Join-Path $PresetRoot 'scripts/validate-intake-series-manifest.ps1') `
        -File $ManifestPath -Repo $RepositoryRoot
    if ($LASTEXITCODE -ne 0) { throw 'PowerShell LegacyAdoption validation failed.' }
    Write-Output (
        'PASS: Home Baseline Series ({0} targets, {1} roots, {2} dependencies)' -f
        $Request.targets.Count,
        $Request.series.roots.Count,
        $Request.series.dependencies.Count
    )
} finally {
    Remove-Item -LiteralPath $TempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
