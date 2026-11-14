[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$PresetRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)
$TempRoot = Join-Path ([IO.Path]::GetTempPath()) "intake-sequencing-$([Guid]::NewGuid())"
$OutsideRoot = "${TempRoot}-outside"
New-Item -ItemType Directory -Path (Join-Path $TempRoot 'intakes') -Force | Out-Null
New-Item -ItemType Directory -Path $OutsideRoot -Force | Out-Null

function Get-NormalizedHash([string]$Path) {
    $Utf8 = [Text.UTF8Encoding]::new($false, $true)
    $Raw = [IO.File]::ReadAllBytes($Path)
    if ($Raw.Length -ge 3 -and $Raw[0] -eq 0xEF -and $Raw[1] -eq 0xBB -and $Raw[2] -eq 0xBF) {
        $Raw = $Raw[3..($Raw.Length - 1)]
    }
    $Text = $Utf8.GetString($Raw).Replace("`r`n", "`n").Replace("`r", "`n")
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Text))).ToLowerInvariant()
}

function Save-Json([object]$Value, [string]$Path) {
    $Value | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Invoke-Validator([string]$Shell, [string]$Kind, [int]$Expected, [string]$ExpectedClass = '') {
    $File = Join-Path $TempRoot "$Kind.json"
    if ($Shell -eq 'Bash') {
        $Output = & bash (Join-Path $PresetRoot "scripts/validate-intake-series-$Kind.sh") `
            --file $File --repo $TempRoot 2>&1
    } else {
        $Output = & pwsh -NoProfile -File (Join-Path $PresetRoot "scripts/validate-intake-series-$Kind.ps1") `
            -File $File -Repo $TempRoot 2>&1
    }
    if ($LASTEXITCODE -ne $Expected) {
        throw "$Shell/$Kind expected $Expected, got $LASTEXITCODE`: $Output"
    }
    $Joined = $Output -join "`n"
    if ($ExpectedClass -and $Joined -notmatch [regex]::Escape($ExpectedClass)) {
        throw "$Shell/$Kind did not report $ExpectedClass`: $Joined"
    }
}

function Reset-Fixture {
    foreach ($Name in @('a', 'b', 'c')) {
        [IO.File]::WriteAllText(
            (Join-Path $TempRoot "intakes/$Name.md"),
            "# $Name`n`nExisting intake $Name.`n",
            [Text.UTF8Encoding]::new($false)
        )
    }
    $script:Manifest = [ordered]@{
        schemaVersion = '1.0'
        documentType = 'IntakeSeriesManifest'
        seriesId = '11111111-1111-4111-8111-111111111111'
        title = 'Three target fixture'
        policy = 'fixture'
        status = 'Ready'
        orderedTargets = @(
            [ordered]@{ path = 'intakes/a.md'; role = 'Primary'; normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'intakes/a.md'); status = 'Completed' }
            [ordered]@{ path = 'intakes/b.md'; role = 'OrderedMember'; normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'intakes/b.md'); status = 'Eligible' }
            [ordered]@{ path = 'intakes/c.md'; role = 'OrderedMember'; normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'intakes/c.md'); status = 'Blocked' }
        )
        roots = @('intakes/a.md')
        dependencies = @(
            [ordered]@{ from = 'intakes/a.md'; to = 'intakes/b.md'; kind = 'RequirementsGovernanceGate'; binding = $true }
            [ordered]@{ from = 'intakes/b.md'; to = 'intakes/c.md'; kind = 'AssessmentBaseline'; binding = $true }
        )
        evidencePaths = @()
    }
    Save-Json $script:Manifest (Join-Path $TempRoot 'manifest.json')
    $script:Receipt = [ordered]@{
        schemaVersion = '1.0'
        documentType = 'IntakeSeriesReceipt'
        receiptId = '22222222-2222-4222-8222-222222222222'
        seriesId = $script:Manifest.seriesId
        generator = [ordered]@{ preset = 'intake-sequencing-governance'; version = '0.2.3' }
        createdAt = '2026-07-25T00:00:00Z'
        operation = [ordered]@{
            operationId = '33333333-3333-4333-8333-333333333333'
            type = 'Create'
            authorityEvidence = 'Explicit fixture authority'
        }
        status = 'Ready'
        manifest = [ordered]@{
            path = 'manifest.json'
            normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'manifest.json')
        }
        supersedes = [ordered]@{
            receiptPath = 'N/A'; receiptNormalizedSha256 = 'N/A'
            manifestArchivePath = 'N/A'; manifestArchiveSha256 = 'N/A'
        }
        tombstone = [ordered]@{ path = 'N/A'; normalizedSha256 = 'N/A' }
        nextAction = '$speckit-intake-series-status'
    }
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
}

function Initialize-DeleteFixture {
    Reset-Fixture
    Copy-Item (Join-Path $TempRoot 'manifest.json') (Join-Path $TempRoot 'prior-manifest.json') -Force
    Copy-Item (Join-Path $TempRoot 'receipt.json') (Join-Path $TempRoot 'prior-receipt.json') -Force
    $script:Manifest.status = 'Deleted'
    Save-Json $script:Manifest (Join-Path $TempRoot 'manifest.json')
    $script:Receipt.receiptId = '44444444-4444-4444-8444-444444444444'
    $script:Receipt.operation.type = 'Delete'
    $script:Receipt.status = 'Deleted'
    $script:Receipt.manifest.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'manifest.json')
    $script:Receipt.supersedes.receiptPath = 'prior-receipt.json'
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.manifestArchivePath = 'prior-manifest.json'
    $script:Receipt.supersedes.manifestArchiveSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-manifest.json')
    $script:Tombstone = [ordered]@{
        schemaVersion = '1.0'
        documentType = 'IntakeSeriesTombstone'
        seriesId = $script:Manifest.seriesId
        deletedAt = '2026-07-25T00:00:00Z'
        authorityEvidence = 'Explicit fixture delete authority'
        reason = 'Fixture deletion'
        archiveManifestPath = $script:Receipt.supersedes.manifestArchivePath
        archiveManifestSha256 = $script:Receipt.supersedes.manifestArchiveSha256
        intakeDocumentsDeleted = $false
    }
    Save-Json $script:Tombstone (Join-Path $TempRoot 'tombstone.json')
    $script:Receipt.tombstone.path = 'tombstone.json'
    $script:Receipt.tombstone.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'tombstone.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
}

function Initialize-UpdateFixture {
    Reset-Fixture
    Copy-Item (Join-Path $TempRoot 'manifest.json') (Join-Path $TempRoot 'prior-manifest.json') -Force
    Copy-Item (Join-Path $TempRoot 'receipt.json') (Join-Path $TempRoot 'prior-receipt.json') -Force
    $script:Receipt.receiptId = '44444444-4444-4444-8444-444444444444'
    $script:Receipt.operation.type = 'Update'
    $script:Receipt.supersedes.receiptPath = 'prior-receipt.json'
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.manifestArchivePath = 'prior-manifest.json'
    $script:Receipt.supersedes.manifestArchiveSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-manifest.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
}

function Invoke-NegativeCase([scriptblock]$Mutation, [string]$ExpectedClass) {
    Reset-Fixture
    & $Mutation $script:Manifest
    Save-Json $script:Manifest (Join-Path $TempRoot 'manifest.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell manifest 2 $ExpectedClass
    }
}

try {
    Reset-Fixture
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell manifest 0
        Invoke-Validator $Shell receipt 0
    }

    # DE: Metadatenpatches duerfen bestehende Receipts nicht ungueltig machen.
    # EN: Metadata patches must preserve valid receipts from earlier releases.
    foreach ($GeneratorVersion in @('0.2.10', '0.2.11')) {
        Reset-Fixture
        $script:Receipt.generator.version = $GeneratorVersion
        Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
        foreach ($Shell in @('Bash', 'PowerShell')) {
            $Before = (Get-FileHash (Join-Path $TempRoot 'receipt.json') -Algorithm SHA256).Hash
            Invoke-Validator $Shell receipt 0
            if ((Get-FileHash (Join-Path $TempRoot 'receipt.json') -Algorithm SHA256).Hash -ne $Before) {
                throw 'Receipt validation changed the metadata fixture'
            }
        }
    }

    Reset-Fixture
    $script:Manifest.status = 'Idle'
    $script:Manifest.orderedTargets = @()
    $script:Manifest.roots = @()
    $script:Manifest.dependencies = @()
    Save-Json $script:Manifest (Join-Path $TempRoot 'manifest.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell manifest 0
    }

    Invoke-NegativeCase {
        param($M)
        $M.status = 'Idle'
    } 'ISG009'
    Invoke-NegativeCase {
        param($M)
        $M.status = 'Ready'
        $M.orderedTargets = @()
        $M.roots = @()
        $M.dependencies = @()
    } 'ISG003'
    Invoke-NegativeCase { param($M) $M.status = 'Completed' } 'ISG009'
    Reset-Fixture
    $script:Manifest.status = 'Completed'
    foreach ($Target in $script:Manifest.orderedTargets) { $Target.status = 'Completed' }
    Save-Json $script:Manifest (Join-Path $TempRoot 'manifest.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell manifest 0 }

    Invoke-NegativeCase { param($M) $M.orderedTargets[1].path = '../escape.md' } 'ISG003'
    Invoke-NegativeCase { param($M) $M.orderedTargets[1].path = 'intakes/a.md' } 'ISG003'
    Invoke-NegativeCase { param($M) $M.orderedTargets[1].normalizedSha256 = '0' * 64 } 'ISG004'
    Reset-Fixture
    $outsideTarget = Join-Path $OutsideRoot 'a.md'
    Copy-Item (Join-Path $TempRoot 'intakes/a.md') $outsideTarget
    Remove-Item (Join-Path $TempRoot 'intakes/a.md')
    New-Item -ItemType SymbolicLink -Path (Join-Path $TempRoot 'intakes/a.md') -Target $outsideTarget | Out-Null
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell manifest 2 'ISG004' }
    Remove-Item (Join-Path $TempRoot 'intakes/a.md') -Force
    Reset-Fixture
    $outsideManifest = Join-Path $OutsideRoot 'manifest.json'
    Copy-Item (Join-Path $TempRoot 'manifest.json') $outsideManifest
    Remove-Item (Join-Path $TempRoot 'manifest.json')
    New-Item -ItemType SymbolicLink -Path (Join-Path $TempRoot 'manifest.json') -Target $outsideManifest | Out-Null
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR004' }
    Remove-Item (Join-Path $TempRoot 'manifest.json') -Force
    Reset-Fixture
    Invoke-NegativeCase { param($M) $M.dependencies[0].to = 'intakes/missing.md' } 'ISG005'
    Invoke-NegativeCase { param($M) $M.dependencies[0].to = 'intakes/a.md' } 'ISG005'
    Invoke-NegativeCase { param($M) $M.dependencies[0].kind = 'Unknown' } 'ISG006'
    Invoke-NegativeCase { param($M) $M.dependencies[0].binding = $false } 'ISG006'
    Invoke-NegativeCase {
        param($M)
        $M.dependencies += [ordered]@{
            from = 'intakes/a.md'; to = 'intakes/b.md'
            kind = 'SharedWriterSerialization'; binding = $false
        }
    } 'ISG006'
    Invoke-NegativeCase { param($M) $M.dependencies[0].from = 'intakes/b.md'; $M.dependencies[0].to = 'intakes/a.md'; $M.roots = @('intakes/b.md') } 'ISG007'
    Invoke-NegativeCase {
        param($M)
        $M.dependencies += [ordered]@{
            from = 'intakes/c.md'; to = 'intakes/a.md'
            kind = 'FinalAuditInput'; binding = $true
        }
        $M.roots = @()
    } 'ISG007'
    Invoke-NegativeCase { param($M) $M.roots = @('intakes/a.md', 'intakes/b.md') } 'ISG008'
    Invoke-NegativeCase { param($M) $M.roots = @('intakes/a.md', 'intakes/a.md') } 'ISG008'
    Invoke-NegativeCase { param($M) $M.orderedTargets[0].status = 'Unknown' } 'ISG009'
    Invoke-NegativeCase {
        param($M)
        $M.orderedTargets[0].status = 'Eligible'
        $M.orderedTargets[1].status = 'Eligible'
    } 'ISG009'

    Reset-Fixture
    $script:Receipt.manifest.normalizedSha256 = '0' * 64
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell receipt 2 'ISR004'
    }

    Reset-Fixture
    $script:Receipt.operation.authorityEvidence = ''
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell receipt 2 'ISR003'
    }

    Reset-Fixture
    $script:Receipt.operation.type = 'Update'
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell receipt 2 'ISR006'
    }

    Initialize-UpdateFixture
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 0 }

    Initialize-UpdateFixture
    $priorManifest = Get-Content (Join-Path $TempRoot 'prior-manifest.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorManifest.orderedTargets[0].path = 'intakes/archived-old.md'
    $priorManifest.roots = @('intakes/archived-old.md')
    $priorManifest.dependencies[0].from = 'intakes/archived-old.md'
    Save-Json $priorManifest (Join-Path $TempRoot 'prior-manifest.json')
    $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorReceipt.manifest.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-manifest.json')
    Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.manifestArchiveSha256 = $priorReceipt.manifest.normalizedSha256
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 0 }

    Initialize-UpdateFixture
    $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorReceipt.operation.Remove('operationId')
    Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }

    Initialize-UpdateFixture
    $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorReceipt.Remove('supersedes')
    Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }

    Initialize-UpdateFixture
    $priorManifest = Get-Content (Join-Path $TempRoot 'prior-manifest.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorManifest.orderedTargets = @()
    Save-Json $priorManifest (Join-Path $TempRoot 'prior-manifest.json')
    $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorReceipt.manifest.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-manifest.json')
    Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.manifestArchiveSha256 = $priorReceipt.manifest.normalizedSha256
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }

    foreach ($badArchive in @('windows-traversal', 'object-root')) {
        Initialize-UpdateFixture
        $priorManifest = Get-Content (Join-Path $TempRoot 'prior-manifest.json') -Raw | ConvertFrom-Json -AsHashtable
        if ($badArchive -eq 'windows-traversal') {
            $priorManifest.orderedTargets[0].path = '..\outside.md'
        } else {
            $priorManifest.roots = @(@{ unexpected = 'object' })
        }
        Save-Json $priorManifest (Join-Path $TempRoot 'prior-manifest.json')
        $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
        $priorReceipt.manifest.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-manifest.json')
        Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
        $script:Receipt.supersedes.manifestArchiveSha256 = $priorReceipt.manifest.normalizedSha256
        $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
        Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
        foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }
    }

    Initialize-UpdateFixture
    $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorReceipt.operation.type = 'Update'
    Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }

    Initialize-DeleteFixture
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 0 }
    $script:Receipt.tombstone.path = 'N/A'
    $script:Receipt.tombstone.normalizedSha256 = 'N/A'
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell receipt 2 'ISR007'
    }

    $outsidePrior = Join-Path $OutsideRoot 'prior-receipt.json'
    Copy-Item (Join-Path $TempRoot 'prior-receipt.json') $outsidePrior
    Remove-Item (Join-Path $TempRoot 'prior-receipt.json')
    New-Item -ItemType SymbolicLink -Path (Join-Path $TempRoot 'prior-receipt.json') -Target $outsidePrior | Out-Null
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }
    Remove-Item (Join-Path $TempRoot 'prior-receipt.json') -Force
    Copy-Item $outsidePrior (Join-Path $TempRoot 'prior-receipt.json')

    Initialize-DeleteFixture
    $script:Tombstone.seriesId = '55555555-5555-4555-8555-555555555555'
    Save-Json $script:Tombstone (Join-Path $TempRoot 'tombstone.json')
    $script:Receipt.tombstone.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'tombstone.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR007' }

    Initialize-DeleteFixture
    $script:Tombstone.archiveManifestPath = 'unrelated-manifest.json'
    Save-Json $script:Tombstone (Join-Path $TempRoot 'tombstone.json')
    $script:Receipt.tombstone.normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'tombstone.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR007' }

    Initialize-DeleteFixture
    $priorReceipt = Get-Content (Join-Path $TempRoot 'prior-receipt.json') -Raw | ConvertFrom-Json -AsHashtable
    $priorReceipt.seriesId = '55555555-5555-4555-8555-555555555555'
    Save-Json $priorReceipt (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR006' }

    Initialize-DeleteFixture

    $outsideTombstone = Join-Path $OutsideRoot 'tombstone.json'
    Copy-Item (Join-Path $TempRoot 'tombstone.json') $outsideTombstone
    $script:Receipt.tombstone.path = 'tombstone.json'
    $script:Receipt.tombstone.normalizedSha256 = Get-NormalizedHash $outsideTombstone
    Remove-Item (Join-Path $TempRoot 'tombstone.json') -Force
    New-Item -ItemType SymbolicLink -Path (Join-Path $TempRoot 'tombstone.json') -Target $outsideTombstone | Out-Null
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) { Invoke-Validator $Shell receipt 2 'ISR007' }

    Write-Output 'PASS: intake sequencing validator parity and negative cases'
} finally {
    Remove-Item -LiteralPath $TempRoot -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -LiteralPath $OutsideRoot -Recurse -Force -ErrorAction SilentlyContinue
}
