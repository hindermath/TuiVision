[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$PresetRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)
$TempRoot = Join-Path ([IO.Path]::GetTempPath()) "intake-review-$([Guid]::NewGuid())"
$OutsideRoot = "${TempRoot}-outside"
New-Item -ItemType Directory -Path (Join-Path $TempRoot 'intakes') -Force | Out-Null
New-Item -ItemType Directory -Path $OutsideRoot -Force | Out-Null

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
        throw "$Kind at test line $($MyInvocation.ScriptLineNumber) expected exit $ExpectedExit, got $LASTEXITCODE`: $Output"
    }
    return ($Output -join "`n")
}

function Save-Json([object]$Value, [string]$Path) {
    $Value | ConvertTo-Json -Depth 30 | Set-Content -LiteralPath $Path -Encoding utf8NoBOM
}

function Reset-SeriesFixture {
    foreach ($Name in @('a', 'b', 'c')) {
        [IO.File]::WriteAllText(
            (Join-Path $TempRoot "intakes/$Name.md"),
            "# $Name`n`nDeliver $Name proof.`n",
            [Text.UTF8Encoding]::new($false)
        )
    }
    $script:SeriesRequest = [ordered]@{
        schemaVersion = '1.1'
        reviewId = [Guid]::NewGuid().ToString()
        mode = 'Series'
        policy = 'series-graph-fixture'
        targets = @(
            [ordered]@{ path = 'intakes/a.md'; role = 'Primary' }
            [ordered]@{ path = 'intakes/b.md'; role = 'OrderedMember' }
            [ordered]@{ path = 'intakes/c.md'; role = 'OrderedMember' }
        )
        series = [ordered]@{
            orderedTargetPaths = @('intakes/a.md', 'intakes/b.md', 'intakes/c.md')
            roots = @('intakes/a.md')
            dependencies = @(
                [ordered]@{ from = 'intakes/a.md'; to = 'intakes/b.md'; kind = 'SeriesPredecessor' }
                [ordered]@{ from = 'intakes/b.md'; to = 'intakes/c.md'; kind = 'SeriesPredecessor' }
            )
        }
        campaign = [ordered]@{ manifestPath = 'N/A'; workers = @(); operatorExceptions = @() }
    }
    Save-Json $script:SeriesRequest (Join-Path $TempRoot 'request.json')
    $script:SeriesResult = [ordered]@{
        schemaVersion = '1.1'
        reviewId = $script:SeriesRequest.reviewId
        mode = 'Series'
        status = 'Ready'
        policy = $script:SeriesRequest.policy
        reviewedAt = [DateTime]::UtcNow.ToString('o')
        repository = [ordered]@{ root = '.'; head = 'N/A' }
        targets = @(
            [ordered]@{ path = 'intakes/a.md'; role = 'Primary'; normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'intakes/a.md'); gitBlob = 'N/A' }
            [ordered]@{ path = 'intakes/b.md'; role = 'OrderedMember'; normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'intakes/b.md'); gitBlob = 'N/A' }
            [ordered]@{ path = 'intakes/c.md'; role = 'OrderedMember'; normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'intakes/c.md'); gitBlob = 'N/A' }
        )
        requestEvidence = [ordered]@{
            path = 'request.json'
            normalizedSha256 = Get-NormalizedHash (Join-Path $TempRoot 'request.json')
        }
        findings = @()
        questions = @()
        acceptedRisks = @()
        operatorExceptions = @()
        coverage = [ordered]@{
            individual = @('intakes/a.md', 'intakes/b.md', 'intakes/c.md')
            series = @('Three targets, one root, two predecessor edges')
            workers = @()
        }
        summary = [ordered]@{ critical = 0; high = 0; medium = 0; low = 0 }
        supersedes = 'N/A'
    }
    Save-Json $script:SeriesResult (Join-Path $TempRoot 'result.json')
}

function Invoke-SeriesCase {
    param(
        [Parameter(Mandatory)][scriptblock]$Mutation,
        [Parameter(Mandatory)][string]$ExpectedClass,
        [switch]$RebindRequest
    )

    Reset-SeriesFixture
    & $Mutation $script:SeriesRequest $script:SeriesResult
    Save-Json $script:SeriesRequest (Join-Path $TempRoot 'request.json')
    if ($RebindRequest) {
        $script:SeriesResult.requestEvidence.normalizedSha256 =
            Get-NormalizedHash (Join-Path $TempRoot 'request.json')
    }
    Save-Json $script:SeriesResult (Join-Path $TempRoot 'result.json')
    foreach ($Kind in @('Bash', 'PowerShell')) {
        $Output = Invoke-Validator $Kind 2
        if ($Output -notmatch [regex]::Escape($ExpectedClass)) {
            throw "$Kind did not report $ExpectedClass`: $Output"
        }
    }
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

    $Result.findings = @([ordered]@{
        id = 'IR001'; severity = 'Medium'; category = 'Security'; target = 'intakes/a.md'
        disposition = 'AcceptedRisk'; owner = 'Human reviewer'; evidence = 'review evidence'
        reevaluationTrigger = 'next review'
    })
    $Result.summary.medium = 1
    $Result.acceptedRisks[0].acceptedByType = 'Human'
    $Result.acceptedRisks[0].owner = 'Human reviewer'
    $Result.acceptedRisks[0].severity = 'Medium'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 0); [void](Invoke-Validator PowerShell 0)

    $ValidFindings = $Result.findings
    $Result.findings = $ValidFindings[0]
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.findings = $ValidFindings

    $ValidRisks = $Result.acceptedRisks
    $Result.acceptedRisks = $ValidRisks[0]
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.acceptedRisks = $ValidRisks

    $Result.findings[0].severity = 'medium'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.findings[0].severity = 'Medium'

    $Result.acceptedRisks[0].acceptedByType = 'human'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.acceptedRisks[0].acceptedByType = 'Human'

    $Result.acceptedRisks[0].findingId = 'IR999'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.acceptedRisks[0].findingId = 'IR001'

    $Result.acceptedRisks[0].severity = 'Low'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.acceptedRisks[0].severity = 'Medium'

    $Result.findings[0].id = @('IR001')
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.findings[0].id = 'IR001'

    $Result.findings[0].id = 'ir001'
    $Result.acceptedRisks[0].findingId = 'ir001'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.findings[0].id = 'IR001'
    $Result.acceptedRisks[0].findingId = 'IR001'

    $Result.acceptedRisks[0].findingId = @('IR001')
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.acceptedRisks[0].findingId = 'IR001'

    $Result.status = 'Ready'
    $Result.acceptedRisks = @()
    $Result.findings[0].disposition = 'Open'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)

    $Result.status = 'ready'
    $Result | ConvertTo-Json -Depth 20 | Set-Content -LiteralPath (Join-Path $TempRoot 'result.json') -Encoding utf8NoBOM
    [void](Invoke-Validator Bash 2); [void](Invoke-Validator PowerShell 2)
    $Result.status = 'Ready'

    Reset-SeriesFixture
    $BashSeriesPass = Invoke-Validator Bash 0
    $PowerShellSeriesPass = Invoke-Validator PowerShell 0
    if ($BashSeriesPass -notmatch 'PASS:' -or $PowerShellSeriesPass -notmatch 'PASS:') {
        throw 'Series positive output parity failed'
    }

    $RequestPath = Join-Path $TempRoot 'request.json'
    $LfRequest = [IO.File]::ReadAllText($RequestPath, [Text.UTF8Encoding]::new($false, $true))
    [IO.File]::WriteAllText(
        $RequestPath,
        ([char]0xFEFF) + $LfRequest.Replace("`r`n", "`n").Replace("`r", "`n").Replace("`n", "`r`n"),
        [Text.UTF8Encoding]::new($false)
    )
    [void](Invoke-Validator Bash 0)
    [void](Invoke-Validator PowerShell 0)

    Invoke-SeriesCase -ExpectedClass IRG001 -Mutation {
        param($Request, $Result)
        $Result.Remove('requestEvidence')
    }
    Invoke-SeriesCase -ExpectedClass IRG001 -Mutation {
        param($Request, $Result)
        $Request.series.roots = @('intakes/a.md', 'intakes/c.md')
    }
    Invoke-SeriesCase -ExpectedClass IRG002 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.policy = 'wrong-policy'
    }
    Invoke-SeriesCase -ExpectedClass IRG003 -RebindRequest -Mutation {
        param($Request, $Result)
        $Result.targets[1].role = 'WrongRole'
    }
    Invoke-SeriesCase -ExpectedClass IRG003 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.targets = @($Request.targets | Select-Object -First 2)
    }
    Invoke-SeriesCase -ExpectedClass IRG004 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.orderedTargetPaths = @('intakes/a.md', 'intakes/b.md', 'intakes/b.md')
    }
    Invoke-SeriesCase -ExpectedClass IRG005 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.dependencies[1].to = 'intakes/missing.md'
    }
    Invoke-SeriesCase -ExpectedClass IRG005 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.dependencies[1].to = 'intakes/b.md'
    }
    Invoke-SeriesCase -ExpectedClass IRG006 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.dependencies += [ordered]@{
            from = 'intakes/a.md'; to = 'intakes/b.md'; kind = 'DuplicateKind'
        }
    }
    Invoke-SeriesCase -ExpectedClass IRG007 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.dependencies += [ordered]@{
            from = 'intakes/c.md'; to = 'intakes/a.md'; kind = 'Cycle'
        }
        $Request.series.roots = @()
    }
    Invoke-SeriesCase -ExpectedClass IRG004 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.dependencies[0] = [ordered]@{
            from = 'intakes/b.md'; to = 'intakes/a.md'; kind = 'WrongOrder'
        }
        $Request.series.roots = @('intakes/b.md')
    }
    Invoke-SeriesCase -ExpectedClass IRG008 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.dependencies = @($Request.series.dependencies | Select-Object -First 1)
    }
    Invoke-SeriesCase -ExpectedClass IRG008 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.roots = @('intakes/a.md', 'intakes/a.md')
    }
    Invoke-SeriesCase -ExpectedClass IRG008 -RebindRequest -Mutation {
        param($Request, $Result)
        $Request.series.roots = @('intakes/a.md', 'intakes/b.md')
    }
    Invoke-SeriesCase -ExpectedClass 'coverage.individual' -Mutation {
        param($Request, $Result)
        $Result.coverage.individual = @('intakes/a.md', 'intakes/a.md', 'intakes/c.md')
    }
    Invoke-SeriesCase -ExpectedClass 'questions[0]' -Mutation {
        param($Request, $Result)
        $Result.questions = @('not an object')
    }
    Invoke-SeriesCase -ExpectedClass 'questions[0].answer' -Mutation {
        param($Request, $Result)
        $Result.questions = @([ordered]@{
            id = 'IRA001'; status = 'Answered'; owner = 'Maintainer'; question = 'Proceed?'; evidence = 'review'
        })
    }
    Invoke-SeriesCase -ExpectedClass 'questions must be an array' -Mutation {
        param($Request, $Result)
        $Result.questions = [ordered]@{
            id = 'IRA001'; status = 'Answered'; owner = 'Maintainer'; question = 'Proceed?'; answer = 'Yes'; evidence = 'review'
        }
    }
    Invoke-SeriesCase -ExpectedClass 'accepted status cannot have unanswered questions' -Mutation {
        param($Request, $Result)
        $Result.questions = @([ordered]@{
            id = 'IRA001'; status = 'answered'; owner = 'Maintainer'; question = 'Proceed?'; answer = 'Yes'; evidence = 'review'
        })
    }

    Reset-SeriesFixture
    $insideTarget = Join-Path $TempRoot 'intakes/a.md'
    $outsideTarget = Join-Path $OutsideRoot 'a.md'
    Copy-Item -LiteralPath $insideTarget -Destination $outsideTarget
    Remove-Item -LiteralPath $insideTarget
    [void](New-Item -ItemType SymbolicLink -Path $insideTarget -Target $outsideTarget)
    foreach ($shell in @('Bash', 'PowerShell')) {
        $output = Invoke-Validator $shell 2
        if ($output -notmatch 'outside repository') { throw "$shell did not identify external target symlink: $output" }
    }
    Remove-Item -LiteralPath $insideTarget -Force

    Reset-SeriesFixture
    $insideRequest = Join-Path $TempRoot 'request.json'
    $outsideRequest = Join-Path $OutsideRoot 'request.json'
    Copy-Item -LiteralPath $insideRequest -Destination $outsideRequest
    Remove-Item -LiteralPath $insideRequest
    [void](New-Item -ItemType SymbolicLink -Path $insideRequest -Target $outsideRequest)
    foreach ($shell in @('Bash', 'PowerShell')) {
        $output = Invoke-Validator $shell 2
        if ($output -notmatch 'outside repository') { throw "$shell did not identify external request symlink: $output" }
    }

    $fieldRoot = Join-Path $TempRoot 'field/evidence/immutable'
    $fieldRepo = Join-Path $TempRoot 'field/runtime/worktrees/example-unit-03'
    [void](New-Item -ItemType Directory -Path $fieldRoot -Force)
    [void](New-Item -ItemType Directory -Path $fieldRepo -Force)
    $fieldCampaign = [ordered]@{
        schemaVersion = '1.0'; deliveryMode = 'LocalImplementation'
        workers = @([ordered]@{
            workerId = 'example-unit-00'; repository = 'N/A'; featureInput = '../../../outside.md'; dependsOn = @()
        })
    }
    Save-Json $fieldCampaign (Join-Path $fieldRoot 'parallel-campaign.json')
    Save-Json ([ordered]@{ schemaVersion = '1.0'; profiles = [ordered]@{} }) `
        (Join-Path $fieldRoot 'parallel-runner-profiles.json')
    $fieldScript = Join-Path $PresetRoot 'tests/test-secure-casetracker-field.ps1'
    $fieldOutput = & pwsh -NoProfile -File $fieldScript -EvidenceRoot $fieldRoot `
        -CoordinatorPath $fieldScript 2>&1
    if ($LASTEXITCODE -eq 0 -or ($fieldOutput -join "`n") -notmatch 'Unsicherer featureInput') {
        throw "Field fixture accepted traversal or failed elsewhere: $fieldOutput"
    }
    $fieldCampaign.workers[0].workerId = '../escape-unit-00'
    $fieldCampaign.workers[0].featureInput = 'safe.md'
    Save-Json $fieldCampaign (Join-Path $fieldRoot 'parallel-campaign.json')
    $fieldOutput = & pwsh -NoProfile -File $fieldScript -EvidenceRoot $fieldRoot `
        -CoordinatorPath $fieldScript 2>&1
    if ($LASTEXITCODE -eq 0 -or ($fieldOutput -join "`n") -notmatch 'Unsicherer workerId') {
        throw "Field fixture accepted workerId traversal or failed elsewhere: $fieldOutput"
    }
    $fieldCampaign.workers[0].workerId = 'example-unit-00'
    $fieldCampaign.workers[0].featureInput = 'linked.md'
    Save-Json $fieldCampaign (Join-Path $fieldRoot 'parallel-campaign.json')
    $outsideFieldInput = Join-Path $TempRoot 'field/outside.md'
    'external fixture input' | Set-Content -LiteralPath $outsideFieldInput -Encoding utf8NoBOM
    [void](New-Item -ItemType SymbolicLink -Path (Join-Path $fieldRepo 'linked.md') -Target $outsideFieldInput)
    $fieldOutput = & pwsh -NoProfile -File $fieldScript -EvidenceRoot $fieldRoot `
        -CoordinatorPath $fieldScript 2>&1
    if ($LASTEXITCODE -eq 0 -or ($fieldOutput -join "`n") -notmatch 'featureInput verlaesst') {
        throw "Field fixture accepted external featureInput symlink or failed elsewhere: $fieldOutput"
    }

    Write-Output 'PASS: intake-review validator parity, Series 1.1 graph contract, and negative cases'
} finally {
    Remove-Item -LiteralPath $TempRoot -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -LiteralPath $OutsideRoot -Recurse -Force -ErrorAction SilentlyContinue
}
