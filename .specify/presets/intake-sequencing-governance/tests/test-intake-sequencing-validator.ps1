[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$PresetRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)
$TempRoot = Join-Path ([IO.Path]::GetTempPath()) "intake-sequencing-$([Guid]::NewGuid())"
New-Item -ItemType Directory -Path (Join-Path $TempRoot 'intakes') -Force | Out-Null

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
            [ordered]@{ from = 'intakes/a.md'; to = 'intakes/b.md'; kind = 'HardCompletionGate'; binding = $true }
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
        generator = [ordered]@{ preset = 'intake-sequencing-governance'; version = '0.1.0' }
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

    Invoke-NegativeCase { param($M) $M.orderedTargets[1].path = '../escape.md' } 'ISG003'
    Invoke-NegativeCase { param($M) $M.orderedTargets[1].path = 'intakes/a.md' } 'ISG003'
    Invoke-NegativeCase { param($M) $M.orderedTargets[1].normalizedSha256 = '0' * 64 } 'ISG004'
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

    Reset-Fixture
    $script:Receipt.operation.type = 'Delete'
    $script:Receipt.status = 'Deleted'
    [IO.File]::WriteAllText((Join-Path $TempRoot 'prior-receipt.json'), "{}`n", [Text.UTF8Encoding]::new($false))
    [IO.File]::WriteAllText((Join-Path $TempRoot 'prior-manifest.json'), "{}`n", [Text.UTF8Encoding]::new($false))
    $script:Receipt.supersedes.receiptPath = 'prior-receipt.json'
    $script:Receipt.supersedes.receiptNormalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-receipt.json')
    $script:Receipt.supersedes.manifestArchivePath = 'prior-manifest.json'
    $script:Receipt.supersedes.manifestArchiveSha256 = Get-NormalizedHash (Join-Path $TempRoot 'prior-manifest.json')
    Save-Json $script:Receipt (Join-Path $TempRoot 'receipt.json')
    foreach ($Shell in @('Bash', 'PowerShell')) {
        Invoke-Validator $Shell receipt 2 'ISR007'
    }

    Write-Output 'PASS: intake sequencing validator parity and negative cases'
} finally {
    Remove-Item -LiteralPath $TempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
