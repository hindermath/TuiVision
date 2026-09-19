#Requires -Version 7
<#
.SYNOPSIS
Prueft den gelieferten Statistik-Kontext / Verifies the delivered statistics context.
.DESCRIPTION
Prueft Paketbindung, Profil, Fixtures, Status, Idempotenz und Encoding-Paritaet.
Der Projektcheckout bleibt unveraendert; Schreibtests laufen in einer temporaeren
lokalen Kopie. Keine Downloads, Git-Lieferung oder menschliche Freigabe.
Verifies package binding, profile, fixtures, status, idempotency and encodings.
The source stays unchanged; scratch clones contain intentional test writes.
.PARAMETER Repo
Vollstaendiger Git-Checkout / complete Git checkout.
.PARAMETER EvidenceDirectory
Neues Verzeichnis ausserhalb des Repos / new output directory outside the repo.
.EXAMPLE
pwsh -NoProfile -File scripts/prove-project-statistics-context.ps1 -Repo . -EvidenceDirectory /tmp/tuivision-proof
#>
[CmdletBinding()]
param([string]$Repo = '.', [Parameter(Mandatory)][string]$EvidenceDirectory)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = (Resolve-Path -LiteralPath $Repo).Path
$evidence = [IO.Path]::GetFullPath($EvidenceDirectory)
if ($evidence.StartsWith($root + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase) -or
    $evidence -eq $root -or (Test-Path -LiteralPath $evidence)) { throw 'Evidence must be new and outside the repository.' }
$initial = Get-Location
$fixture = Join-Path ([IO.Path]::GetTempPath()) ('statistics-context-' + [guid]::NewGuid().ToString('N'))
$config = 'docs/project-statistics/config.json'
$mutable = @($config, 'docs/project-statistics/report.md', 'docs/project-statistics/snapshot.json')
$protected = $mutable + @('docs/project-statistics.config.json', 'docs/project-statistics.md')
$packageRelative = '.specify/presets/project-statistics-governance'
function Invoke-ContextProofCommand {
    param([string]$Executable, [string[]]$Arguments)
    $output = @(& $Executable @Arguments 2>&1)
    if ($LASTEXITCODE -ne 0) { throw "Command failed: $Executable $Arguments : $output" }
    $output -join [Environment]::NewLine
}
function Get-ContextProofHashSet {
    param([string]$Directory)
    $hashes = [ordered]@{}
    foreach ($path in $protected) { $hashes[$path] = (Get-FileHash -LiteralPath (Join-Path $Directory $path)).Hash.ToLowerInvariant() }
    ConvertTo-Json -InputObject $hashes -Compress
}
function Get-ContextProofStatus {
    param([string]$Directory, [string]$Surface)
    if ($Surface -eq 'Bash') {
        $command = 'bash'
        $arguments = @((Join-Path $Directory "$packageRelative/scripts/project-statistics.sh"), 'status', '--repo', $Directory, '--config', $config, '--json')
    } else {
        $command = 'pwsh'
        $arguments = @('-NoProfile', '-File', (Join-Path $Directory "$packageRelative/scripts/project-statistics.ps1"), '-Action', 'Status', '-Repo', $Directory, '-Config', $config, '-Json')
    }
    $before = Get-ContextProofHashSet $Directory
    $gitBefore = Invoke-ContextProofCommand git @('-C', $Directory, 'status', '--porcelain=v1', '--untracked-files=all')
    $decision = Invoke-ContextProofCommand $command $arguments | ConvertFrom-Json
    if ($decision.status -ne 'CURRENT' -or -not $decision.reproducible -or -not $decision.current -or $decision.changed) { throw 'Statistics are not reproducible/current.' }
    if ((Get-ContextProofHashSet $Directory) -cne $before -or
        (Invoke-ContextProofCommand git @('-C', $Directory, 'status', '--porcelain=v1', '--untracked-files=all')) -cne $gitBefore) { throw 'Status modified checked files or Git state.' }
    [ordered]@{surface=$Surface;command=$command;arguments=$arguments;exitCode=0;decision=$decision;checkedContentChanges=0}
}
try {
    Set-Location $root
    if (Invoke-ContextProofCommand git @('status', '--porcelain=v1', '--untracked-files=all')) { throw 'Clean source checkout required.' }
    $headCommit = Invoke-ContextProofCommand git @('rev-parse', 'HEAD')
    $before = Get-ContextProofHashSet $root
    $null = New-Item -ItemType Directory -Path $evidence
    $receipt = Get-Content docs/maintenance/project-statistics-installation-v010.json -Raw | ConvertFrom-Json
    foreach ($file in $receipt.installedPayloadFiles) {
        if ((Get-FileHash -LiteralPath (Join-Path $root "$packageRelative/$($file.path)")).Hash.ToLowerInvariant() -cne $file.sha256) { throw "Package drift: $($file.path)" }
    }
    $matrixPath = 'scripts/config/spec-kit-project-statistics-governance-presets.json'
    if ((Get-FileHash $matrixPath).Hash.ToLowerInvariant() -cne $receipt.matrixSha256) { throw 'Matrix source drift.' }
    $matrix = Get-Content $matrixPath -Raw | ConvertFrom-Json
    $registry = Get-Content .specify/presets/.registry -Raw | ConvertFrom-Json -AsHashtable
    if ($registry.presets.Count -ne 14) { throw 'Expected fourteen presets.' }
    foreach ($item in $matrix.presets) {
        $actual = $registry.presets[$item.id]
        if ($actual.version -cne $item.version.TrimStart('v') -or $actual.priority -ne $item.priority -or -not $actual.enabled) { throw "Profile drift: $($item.id)" }
    }
    $null = Invoke-ContextProofCommand specify @('preset', 'list')
    $null = Invoke-ContextProofCommand specify @('preset', 'info', 'project-statistics-governance')
    foreach ($contract in @('project-statistics-contract','secure-development-evidence-contract','constitution-template','plan-template','tasks-template')) {
        $null = Invoke-ContextProofCommand specify @('preset','resolve',$contract)
    }
    $null = Invoke-ContextProofCommand specify @('check')
    $null = Invoke-ContextProofCommand pwsh @('-NoProfile','-File','scripts/install-spec-kit-governance-presets.ps1','-Repo',$root,'-PresetConfig',$matrixPath,'-CheckOnly')
    if (-not $IsWindows) { $null = Invoke-ContextProofCommand bash @('scripts/install-spec-kit-governance-presets.sh','--repo',$root,'--preset-config',$matrixPath,'--check-only') }
    $suitePath = "$packageRelative/tests/test-project-statistics.ps1"
    $suite = Invoke-ContextProofCommand pwsh @('-NoProfile','-File',$suitePath)
    $suite | Set-Content (Join-Path $evidence 'fixtures.log') -Encoding utf8NoBOM
    $assertions = if ($IsWindows) { 61 } else { 67 }
    if ($suite -notmatch "PASS: $assertions assertions;") { throw 'Missing fixture assertion evidence.' }
    $lifecycle = Invoke-ContextProofCommand pwsh @('-NoProfile','-File',"$packageRelative/tests/test-installed-preset.ps1",'-Package',(Join-Path $root $packageRelative))
    $lifecycle | Set-Content (Join-Path $evidence 'lifecycle.log') -Encoding utf8NoBOM
    $surfaces = if ($IsWindows) { @('PowerShell') } else { @('Bash','PowerShell') }
    $decisions = @($surfaces | ForEach-Object { Get-ContextProofStatus $root $_ })
    # Reale Quellen bleiben unveraendert; Encoding- und Schreibtests nur in der Kopie.
    # Keep real sources unchanged; only the isolated copy receives encoding/write tests.
    $null = Invoke-ContextProofCommand git @('clone','--quiet','--no-hardlinks',$root,$fixture)
    $copyBefore = Get-ContextProofHashSet $fixture
    $null = Invoke-ContextProofCommand pwsh @('-NoProfile','-File',(Join-Path $fixture "$packageRelative/scripts/project-statistics.ps1"),'-Action','Update','-Repo',$fixture,'-Config',$config)
    if ((Get-ContextProofHashSet $fixture) -cne $copyBefore -or (Invoke-ContextProofCommand git @('-C',$fixture,'status','--porcelain=v1'))) { throw 'Repeated update is not idempotent.' }
    $encodingDecisions = @()
    foreach ($variant in @('LF','CRLF','LF-BOM','CRLF-BOM')) {
        foreach ($path in $mutable) {
            $content = [IO.File]::ReadAllText((Join-Path $root $path)).TrimStart([char]0xfeff).Replace(([string][char]13 + [char]10),[string][char]10)
            if ($variant.StartsWith('CRLF')) { $content = $content.Replace([string][char]10,([string][char]13 + [char]10)) }
            [IO.File]::WriteAllText((Join-Path $fixture $path), $content, [Text.UTF8Encoding]::new($variant.EndsWith('BOM')))
        }
        foreach ($surface in $surfaces) { $encodingDecisions += [ordered]@{variant=$variant;proof=(Get-ContextProofStatus $fixture $surface)} }
    }
    if ((Get-ContextProofHashSet $root) -cne $before -or (Invoke-ContextProofCommand git @('status','--porcelain=v1','--untracked-files=all'))) { throw 'Source checkout changed.' }
    $snapshot = Get-Content $mutable[2] -Raw | ConvertFrom-Json
    $record = [ordered]@{schemaVersion=1;head=$headCommit;sourceRevision=$snapshot.measurement.sourceRevision;asOf=$snapshot.measurement.asOf;
        platform=[Runtime.InteropServices.RuntimeInformation]::OSDescription;powerShell=$PSVersionTable.PSVersion.ToString();
        proofCommand='pwsh -NoProfile -File scripts/prove-project-statistics-context.ps1 -Repo . -EvidenceDirectory EXTERNAL_DIRECTORY';
        proofScriptSha256=(Get-FileHash $PSCommandPath).Hash.ToLowerInvariant();suiteCommand="pwsh -NoProfile -File $suitePath";
        suiteSha256=(Get-FileHash $suitePath).Hash.ToLowerInvariant();suiteLogSha256=(Get-FileHash (Join-Path $evidence 'fixtures.log')).Hash.ToLowerInvariant();
        assertions=$assertions;exitCode=0;payloadHashes=($before | ConvertFrom-Json);status=$decisions;encoding=$encodingDecisions;
        idempotent=$true;checkedSourceChanges=0;fixtureWritesExpected=$true;fullIoTrace=$false;humanAcceptance='Pending'}
    $record | ConvertTo-Json -Depth 30 | Set-Content (Join-Path $evidence 'native-evidence.json') -Encoding utf8NoBOM
    Get-FileHash (Join-Path $evidence 'native-evidence.json') | Format-List
    Write-Output "PASS: exact head $headCommit, $assertions assertions, profile, lifecycle, current status, idempotency, encoding and read-only checks."
} finally {
    Set-Location $initial
    if (Test-Path -LiteralPath $fixture) { Remove-Item -LiteralPath $fixture -Recurse -Force }
}
