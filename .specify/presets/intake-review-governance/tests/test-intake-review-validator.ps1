[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$PresetRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)
$TempRoot = Join-Path ([IO.Path]::GetTempPath()) "intake-review-$([Guid]::NewGuid())"
New-Item -ItemType Directory -Path (Join-Path $TempRoot 'intakes') -Force | Out-Null

function Get-NormalizedHash([string]$Path) {
    $Utf8 = [Text.UTF8Encoding]::new($false, $true)
    $Text = $Utf8.GetString([IO.File]::ReadAllBytes($Path)).Replace("`r`n", "`n").Replace("`r", "`n")
    return [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($Utf8.GetBytes($Text))).ToLowerInvariant()
}

function Invoke-Validator([string]$Kind, [int]$ExpectedExit) {
    if ($Kind -eq 'PowerShell') {
        $Output = & pwsh -NoProfile -File (Join-Path $PresetRoot 'scripts/validate-intake-review-result.ps1') `
            -Result (Join-Path $TempRoot 'result.json') -Repo $TempRoot 2>&1
    } else {
        $Output = & bash (Join-Path $PresetRoot 'scripts/validate-intake-review-result.sh') `
            --result (Join-Path $TempRoot 'result.json') --repo $TempRoot 2>&1
    }
    if ($LASTEXITCODE -ne $ExpectedExit) {
        throw "$Kind expected exit $ExpectedExit, got $LASTEXITCODE`: $Output"
    }
    return ($Output -join "`n")
}

try {
    [IO.File]::WriteAllText((Join-Path $TempRoot 'intakes/a.md'), "# Goal`r`n`r`nDeliver proof.`r`n", [Text.UTF8Encoding]::new($false))
    $Hash = Get-NormalizedHash (Join-Path $TempRoot 'intakes/a.md')
    $Result = [ordered]@{
        schemaVersion = '1.0'; reviewId = [Guid]::NewGuid().ToString(); mode = 'Single'
        status = 'Ready'; policy = 'generic-markdown'; reviewedAt = [DateTime]::UtcNow.ToString('o')
        repository = [ordered]@{ root = '.'; head = 'N/A' }
        targets = @([ordered]@{ path = 'intakes/a.md'; role = 'Primary'; normalizedSha256 = $Hash; gitBlob = 'N/A' })
        findings = @(); questions = @(); acceptedRisks = @(); operatorExceptions = @()
        coverage = [ordered]@{ individual = @('intakes/a.md'); series = @(); workers = @() }
        summary = [ordered]@{ critical = 0; high = 0; medium = 0; low = 0 }
        supersedes = 'N/A'
    }
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    $BashPass = Invoke-Validator Bash 0
    $PowerShellPass = Invoke-Validator PowerShell 0
    if ($BashPass -notmatch 'PASS:' -or $PowerShellPass -notmatch 'PASS:') { throw 'positive output parity failed' }

    [IO.File]::WriteAllText((Join-Path $TempRoot 'intakes/a.md'), "# Goal`n`nDeliver proof.`n", [Text.UTF8Encoding]::new($false))
    [void](Invoke-Validator Bash 0); [void](Invoke-Validator PowerShell 0)

    Add-Content -LiteralPath (Join-Path $TempRoot 'intakes/a.md') -Value 'Changed.' -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)

    [IO.File]::WriteAllText((Join-Path $TempRoot 'intakes/a.md'), "# Goal`n`nDeliver proof.`n", [Text.UTF8Encoding]::new($false))
    $Result.status = 'ReadyWithAcceptedRisks'
    $Result.acceptedRisks = @([ordered]@{
        findingId = 'IR001'; severity = 'Low'; owner = 'Agent'; rationale = 'fixture'
        acceptedAt = [DateTime]::UtcNow.ToString('o'); acceptedByType = 'Agent'
        evidence = 'fixture'; reevaluationTrigger = 'next review'
    })
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    Write-Output 'PASS: intake-review validator parity and negative cases'
} finally {
    Remove-Item -LiteralPath $TempRoot -Recurse -Force -ErrorAction SilentlyContinue
}
