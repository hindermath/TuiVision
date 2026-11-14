#Requires -Version 7.0
<#
.SYNOPSIS
    DE: Breite Modellauswahl pruefen. EN: Verify broad model selection.
.DESCRIPTION
    DE: Synthetische Inventare fuer Claude, Copilot und OpenCode; keine echten
    Modelle oder Konten. Prueft Rollen, Sperren, explizite IDs und Policy-Drift.
    EN: Synthetic inventories for Claude, Copilot and OpenCode; no real models
    or accounts. Checks roles, blockers, exact IDs and policy drift.
.EXAMPLE
    pwsh -NoProfile -File tests/test-broad-model-selection.ps1
#>
[CmdletBinding()]
param()
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$temporary = Join-Path ([IO.Path]::GetTempPath()) "broad-routing-$([guid]::NewGuid())"
$script:checks = 0
function Assert-Selection {
    param([bool] $Condition, [string] $Message)
    if (-not $Condition) { throw $Message }
    $script:checks++
}
function global:claude { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function global:copilot { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function global:opencode { $global:LASTEXITCODE = 0; 'fixture-cli 1.0' }
function New-TestModel {
    param([string] $Id, [string[]] $Efforts = @('high','medium','low'))
    $supports = $Efforts.Count -gt 0
    $variants = @{}; foreach ($effort in $Efforts) { $variants[$effort] = @{} }
    return @{
        id=$Id; value=$Id; supportsEffort=$supports; supportedEffortLevels=$Efforts
        policy=@{state='enabled'}; capabilities=@{supports=@{reasoningEffort=$supports}; reasoning=$supports}
        supportedReasoningEfforts=$Efforts; variants=$variants
    }
}
function Invoke-SelectionTest {
    param(
        [string] $Family, [object[]] $Models, [string] $Frontier = '', [string] $Fast = '',
        [string] $Action = 'Refresh', [bool] $UseExisting = $false
    )
    $fixture = Join-Path $temporary 'inventory.json'
    @{models=$Models;provider='fixture-provider'} | ConvertTo-Json -Depth 30 | Set-Content $fixture
    $config = Join-Path $temporary "$Family.json"
    if (-not $UseExisting -and (Test-Path $config)) { Remove-Item -LiteralPath $config -Force }
    $arguments = @{
        Action=$Action; Harness=$Family; DiscoveryFixture=$fixture; ConfigPath=$config
        OutputFormat='Json'; FrontierModel=$Frontier; FastModel=$Fast
    }
    $text = & (Join-Path $root 'scripts/resolve-model-routing.ps1') @arguments
    return @{code=$LASTEXITCODE; result=($text -join "`n" | ConvertFrom-Json -AsHashtable)}
}
function Get-TestId {
    param([string] $Family, [string] $Id)
    if ($Family -eq 'OpenCode') { return "fixture-provider/$Id" }
    return $Id
}
try {
    $null = New-Item -ItemType Directory $temporary
    foreach ($family in @('Claude','Copilot','OpenCode')) {
        foreach ($pair in @(
            @('gpt-99','gpt-99-mini'), @('gemini-99-pro','gemini-99-flash'),
            @('o99','gpt-99-nano'), @('claude-sonnet-99','claude-sonnet-99'),
            @('custom-reasoner','custom-reasoner')
        )) {
            $models = @((New-TestModel $pair[0]))
            if ($pair[0] -cne $pair[1]) { $models += New-TestModel $pair[1] @() }
            $reply = Invoke-SelectionTest $family $models
            Assert-Selection ($reply.code -eq 0) "$family $($pair -join '/') failed."
            $roles = $reply.result.roles
            $demanding = @($roles | Where-Object { $_.role -cne 'fast-mechanical' })
            $fast = @($roles | Where-Object { $_.role -ceq 'fast-mechanical' })
            Assert-Selection ($demanding.Count -eq 3 -and @($demanding | Where-Object { $_.model -cne (Get-TestId $family $pair[0]) -or $_.reasoningEffort -cne 'high' }).Count -eq 0) "$family wrong demanding roles."
            Assert-Selection ($fast[0].model -ceq (Get-TestId $family $pair[1])) "$family wrong fast model."
            # Discovery order must not decide model choice.
            [array]::Reverse($models)
            $reordered = Invoke-SelectionTest $family $models
            Assert-Selection (($reply.result.roles | ConvertTo-Json -Compress) -ceq ($reordered.result.roles | ConvertTo-Json -Compress)) "$family order-dependent selection."
        }
        $highOnly = @((New-TestModel 'gpt-99' @('high')))
        $reply = Invoke-SelectionTest $family $highOnly
        Assert-Selection ($reply.code -eq 0 -and @($reply.result.roles | Where-Object { $_.reasoningEffort -cne 'high' }).Count -eq 0) "$family invented effort on reused high-only model."
        $reply = Invoke-SelectionTest $family @((New-TestModel 'claude-opus-99'), (New-TestModel 'gpt-99'))
        Assert-Selection ($reply.code -eq 0 -and @($reply.result.roles | Where-Object { $_.model -cne (Get-TestId $family 'claude-opus-99') }).Count -eq 0) "$family no-Haiku fallback did not reuse frontier."
        if ($family -ne 'OpenCode') {
            foreach ($switchingAlias in @('default','auto','opusplan')) {
                $reply = Invoke-SelectionTest $family @((New-TestModel $switchingAlias)) $switchingAlias
                Assert-Selection ($reply.code -eq 3) "$family bound switching policy $switchingAlias as an exact model."
            }
        }
        $contextId = 'custom[1m]'
        $reply = Invoke-SelectionTest $family @((New-TestModel $contextId))
        Assert-Selection ($reply.code -eq 3) "$family selected larger context implicitly."
        $reply = Invoke-SelectionTest $family @((New-TestModel $contextId)) (Get-TestId $family $contextId)
        Assert-Selection ($reply.code -eq 0) "$family explicit larger-context choice failed."
        $unknownReasoning = New-TestModel 'claude-opus-99'
        $unknownReasoning.supportsEffort=$null; $unknownReasoning.capabilities=@{}
        $reply = Invoke-SelectionTest $family @($unknownReasoning, (New-TestModel 'gpt-99'), (New-TestModel 'gpt-99-mini' @()))
        Assert-Selection ($reply.code -eq 0 -and $reply.result.roles[0].model -ceq (Get-TestId $family 'gpt-99')) "$family preferred ineligible Opus over capable GPT: $($reply | ConvertTo-Json -Depth 10 -Compress)"
        $twoVersions = @((New-TestModel 'gpt-98'), (New-TestModel 'gpt-99'), (New-TestModel 'gpt-99-mini' @()))
        $config = Join-Path $temporary "$family.json"
        @{schemaVersion='1.0';marker='preserve-on-ambiguity'} | ConvertTo-Json | Set-Content $config
        $digest = (Get-FileHash $config).Hash
        $blocked = Invoke-SelectionTest $family $twoVersions '' '' 'Refresh' $true
        Assert-Selection ($blocked.code -eq 3 -and $blocked.result.reason.Contains('-FrontierModel')) "$family silently guessed newest GPT."
        Assert-Selection ((Get-FileHash $config).Hash -ceq $digest) "$family ambiguous refresh changed profile."
        $reply = Invoke-SelectionTest $family $twoVersions (Get-TestId $family 'gpt-98')
        Assert-Selection ($reply.code -eq 0 -and $reply.result.roles[0].model -ceq (Get-TestId $family 'gpt-98')) "$family exact model choice ignored."
        $fastVersions = @((New-TestModel 'claude-opus-99'), (New-TestModel 'gpt-98-mini' @()), (New-TestModel 'gpt-99-mini' @()))
        $reply = Invoke-SelectionTest $family $fastVersions
        Assert-Selection ($reply.code -eq 3 -and $reply.result.reason.Contains('-FastModel')) "$family ambiguous fast tier silently reused frontier."
        $reply = Invoke-SelectionTest $family $fastVersions '' (Get-TestId $family 'gpt-98-mini')
        Assert-Selection ($reply.code -eq 0 -and @($reply.result.roles | Where-Object { $_.role -ceq 'fast-mechanical' })[0].model -ceq (Get-TestId $family 'gpt-98-mini')) "$family exact fast choice failed."
        $unknownPair = @((New-TestModel 'custom-a'), (New-TestModel 'custom-b'))
        $reply = Invoke-SelectionTest $family $unknownPair (Get-TestId $family 'custom-b') (Get-TestId $family 'custom-a')
        Assert-Selection ($reply.code -eq 0 -and $reply.result.roles[0].model -ceq (Get-TestId $family 'custom-b')) "$family exact custom models blocked by family allowlist."
        foreach ($bad in @('not-discovered','--unsafe')) {
            $digest = (Get-FileHash $config).Hash
            $reply = Invoke-SelectionTest $family $unknownPair $bad '' 'Refresh' $true
            Assert-Selection ($reply.code -eq 3 -and (Get-FileHash $config).Hash -ceq $digest) "$family invalid override wrote profile."
        }
        $reply = Invoke-SelectionTest $family @((New-TestModel 'gpt-99-mini' @())) (Get-TestId $family 'gpt-99-mini')
        Assert-Selection ($reply.code -eq 3) "$family exact choice bypassed high requirement."
        if ($family -eq 'Copilot') {
            foreach ($state in @('disabled','unconfigured')) {
                $disabled = New-TestModel 'gpt-99'; $disabled.policy.state=$state
                $reply = Invoke-SelectionTest $family @($disabled,(New-TestModel 'gpt-99-mini' @())) 'gpt-99'
                Assert-Selection ($reply.code -eq 3) "Copilot exact choice bypassed $state policy."
            }
        }
        $reply = Invoke-SelectionTest $family @((New-TestModel 'gpt-99'))
        $profileData = Get-Content $config -Raw | ConvertFrom-Json -AsHashtable
        Assert-Selection ($profileData.selectionPolicySha256 -cmatch '^[a-f0-9]{64}$') "$family missing selection policy binding."
        $profileData.selectionPolicySha256 = 'stale'
        $profileData | ConvertTo-Json -Depth 30 | Set-Content $config
        $digest = (Get-FileHash $config).Hash
        $reply = Invoke-SelectionTest $family @((New-TestModel 'gpt-99')) '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 2 -and (Get-FileHash $config).Hash -ceq $digest) "$family policy drift not detected read-only."
        $profileData.Remove('selectionPolicySha256')
        $profileData | ConvertTo-Json -Depth 30 | Set-Content $config
        $digest = (Get-FileHash $config).Hash
        $reply = Invoke-SelectionTest $family @((New-TestModel 'gpt-99')) '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 2 -and (Get-FileHash $config).Hash -ceq $digest) "$family older profile needs explicit policy refresh."

        # A deliberate first selection is reusable only while the exact catalog
        # evidence and the stored reasoning levels remain valid.
        $reuseModels = @(
            (New-TestModel 'claude-opus-98'), (New-TestModel 'claude-opus-99'),
            (New-TestModel 'claude-haiku-98' @('medium','low')), (New-TestModel 'claude-haiku-99' @('medium','low'))
        )
        $savedFrontier = Get-TestId $family 'claude-opus-99'
        $savedFast = Get-TestId $family 'claude-haiku-99'
        $alternateFrontier = Get-TestId $family 'claude-opus-98'
        $alternateFast = Get-TestId $family 'claude-haiku-98'
        $reply = Invoke-SelectionTest $family $reuseModels $savedFrontier $savedFast
        Assert-Selection ($reply.code -eq 0) "$family could not seed a deliberate selection."
        $savedBytes = [Convert]::ToBase64String([IO.File]::ReadAllBytes($config))

        $reply = Invoke-SelectionTest $family $reuseModels '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 0 -and $reply.result.status -ceq 'Aligned') "$family did not reuse the stored selection for Status."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $savedBytes) "$family Status changed the stored profile."
        $reply = Invoke-SelectionTest $family $reuseModels '' '' 'Refresh' $true
        Assert-Selection ($reply.code -eq 0 -and $reply.result.status -ceq 'Aligned') "$family parameterless Refresh was not aligned."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $savedBytes) "$family parameterless Refresh was not byte-identical."

        $reply = Invoke-SelectionTest $family $reuseModels $alternateFrontier '' 'Status' $true
        $fastRole = @($reply.result.roles | Where-Object { $_.role -ceq 'fast-mechanical' })[0]
        Assert-Selection ($reply.code -eq 2 -and $reply.result.roles[0].model -ceq $alternateFrontier -and $fastRole.model -ceq $savedFast) "$family did not combine an explicit frontier with the stored fast model."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $savedBytes) "$family partial Status changed the profile."
        $reply = Invoke-SelectionTest $family $reuseModels $alternateFrontier '' 'Refresh' $true
        Assert-Selection ($reply.code -eq 0) "$family could not refresh an explicit frontier with the stored fast model."
        $partialBytes = [Convert]::ToBase64String([IO.File]::ReadAllBytes($config))
        $reply = Invoke-SelectionTest $family $reuseModels '' $alternateFast 'Status' $true
        Assert-Selection ($reply.code -eq 2 -and $reply.result.roles[0].model -ceq $alternateFrontier -and @($reply.result.roles | Where-Object { $_.role -ceq 'fast-mechanical' })[0].model -ceq $alternateFast) "$family explicit fast model did not override the stored value."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $partialBytes) "$family explicit-precedence Status changed the profile."

        # An explicit replacement may repair one stale role while retaining the
        # other stored role. Without that replacement the resolver must block.
        $driftModels = @((New-TestModel 'claude-opus-99'), (New-TestModel 'claude-haiku-99' @('medium','low')))
        $reply = Invoke-SelectionTest $family $driftModels '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 3) "$family silently replaced a missing stored frontier model."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $partialBytes) "$family catalog-drift blocker changed the profile."
        $reply = Invoke-SelectionTest $family $driftModels $savedFrontier '' 'Status' $true
        Assert-Selection ($reply.code -eq 2 -and $reply.result.roles[0].model -ceq $savedFrontier) "$family explicit frontier could not replace a stale stored model."

        $reply = Invoke-SelectionTest $family $reuseModels $savedFrontier $savedFast
        $savedBytes = [Convert]::ToBase64String([IO.File]::ReadAllBytes($config))
        $reducedReasoning = @(
            (New-TestModel 'claude-opus-99' @('medium','low')),
            (New-TestModel 'claude-haiku-99' @('medium','low'))
        )
        $reply = Invoke-SelectionTest $family $reducedReasoning '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 3) "$family accepted a stored frontier reasoning level after capability drift."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $savedBytes) "$family reasoning-drift blocker changed the profile."

        $duplicateCatalog = @(
            (New-TestModel 'claude-opus-99'), (New-TestModel 'claude-opus-99'),
            (New-TestModel 'claude-haiku-99' @('medium','low'))
        )
        $reply = Invoke-SelectionTest $family $duplicateCatalog '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 3) "$family accepted duplicate discovered model IDs."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $savedBytes) "$family duplicate-ID blocker changed the profile."

        if ($family -eq 'Copilot') {
            $disabledSaved = New-TestModel 'claude-opus-99'; $disabledSaved.policy.state='disabled'
            $reply = Invoke-SelectionTest $family @($disabledSaved,(New-TestModel 'claude-haiku-99' @('medium','low'))) '' '' 'Status' $true
            Assert-Selection ($reply.code -eq 3) 'Copilot reused a policy-disabled stored model.'
            Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $savedBytes) 'Copilot policy blocker changed the profile.'
        }

        $profileData = Get-Content $config -Raw | ConvertFrom-Json -AsHashtable
        $profileData.profiles["$($family.ToLowerInvariant())-implementation-auto"].model = Get-TestId $family 'claude-opus-98'
        $profileData | ConvertTo-Json -Depth 30 | Set-Content $config
        $inconsistentBytes = [Convert]::ToBase64String([IO.File]::ReadAllBytes($config))
        $reply = Invoke-SelectionTest $family $reuseModels '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 3) "$family accepted inconsistent stored frontier profiles."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $inconsistentBytes) "$family inconsistent-profile blocker changed the file."

        [IO.File]::WriteAllText($config, '{broken-profile')
        $brokenBytes = [Convert]::ToBase64String([IO.File]::ReadAllBytes($config))
        $reply = Invoke-SelectionTest $family $reuseModels '' '' 'Status' $true
        Assert-Selection ($reply.code -eq 3) "$family accepted a damaged stored profile."
        Assert-Selection ([Convert]::ToBase64String([IO.File]::ReadAllBytes($config)) -ceq $brokenBytes) "$family damaged-profile blocker changed the file."
    }
    Write-Output "PASS: $script:checks broad-selection assertions; offline."
} finally {
    Remove-Item $temporary -Recurse -Force -ErrorAction SilentlyContinue
    foreach ($name in @('claude','copilot','opencode')) { Remove-Item "Function:\global:$name" -ErrorAction SilentlyContinue }
}
