#Requires -Version 7.0
<#
.SYNOPSIS
    Simuliert idempotente Post-Merge-Aktionen fuer lokale Tests.

.DESCRIPTION
    DE: Zaehlt lokale Aktionsversuche und kann deklarierte Fehler reproduzierbar
    ausloesen. Es werden keine Remotes veraendert.

    EN: Counts local action attempts and can reproduce declared failures. It
    does not modify remotes.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $StateFile,
    [Parameter(Mandatory)][string] $ActionId,
    [Parameter(Mandatory)][string] $Repository,
    [Parameter(Mandatory)][string] $ExpectedHead
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$state = Get-Content -LiteralPath $StateFile -Raw | ConvertFrom-Json -AsHashtable
if (-not $state.actions.ContainsKey($ActionId)) {
    throw "Unknown fixture post-merge action: $ActionId"
}
$action = $state.actions[$ActionId]
$action.attemptCount = [int] $action.attemptCount + 1
$actualHead = @(& git -C $Repository cat-file -e "$ExpectedHead^{commit}" 2>&1)
if ($LASTEXITCODE -ne 0) {
    throw "Cannot resolve expected fixture worker head: $(@($actualHead) -join [Environment]::NewLine)"
}
if ([int] $action.failuresRemaining -gt 0) {
    $action.failuresRemaining = [int] $action.failuresRemaining - 1
    $state | ConvertTo-Json -Depth 10 |
        Set-Content -LiteralPath $StateFile -Encoding utf8NoBOM
    exit 12
}
$action.succeeded = $true
$state | ConvertTo-Json -Depth 10 |
    Set-Content -LiteralPath $StateFile -Encoding utf8NoBOM
