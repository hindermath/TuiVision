#Requires -Version 7
<#
.SYNOPSIS
Runs a harness and persists its structured standard output atomically.
.DESCRIPTION
The autonomous runner requires a result file, while Claude, Copilot and
OpenCode return their final answer on standard output. This adapter captures
successful JSON-only output, validates that it is a JSON object, writes it to
the exact runner-owned path, and also returns it for the ordinary phase log.
No result is written when the harness fails or emits non-JSON output.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string] $OutputFile,
    [Parameter(Mandatory)][string] $Executable,
    [string] $HarnessArgumentsJson = '',
    [ValidateSet('Plain','CopilotJsonl','ClaudeStructuredJson')][string] $OutputProtocol = 'Plain',
    [Parameter(ValueFromRemainingArguments)][string[]] $HarnessArguments
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$effectiveArguments = if (-not [string]::IsNullOrWhiteSpace($HarnessArgumentsJson)) {
    $decoded = $HarnessArgumentsJson | ConvertFrom-Json -Depth 20 -NoEnumerate
    if ($decoded -isnot [object[]]) { throw 'HarnessArgumentsJson must contain one JSON array.' }
    @($decoded | ForEach-Object { [string] $_ })
} else { @($HarnessArguments) }

$output = @(& $Executable @effectiveArguments)
$exitCode = $LASTEXITCODE
if ($exitCode -ne 0) { exit $exitCode }

$text = if ($OutputProtocol -eq 'CopilotJsonl') {
    $assistantMessages = [Collections.Generic.List[string]]::new()
    foreach ($line in $output) {
        if ([string]::IsNullOrWhiteSpace($line)) { continue }
        try { $jsonEvent = $line | ConvertFrom-Json -AsHashtable -Depth 100 }
        catch { throw 'Copilot JSONL contained a non-JSON event.' }
        if ($jsonEvent.type -ceq 'assistant.message' -and $jsonEvent.data.content -is [string] -and -not [string]::IsNullOrWhiteSpace($jsonEvent.data.content)) {
            $assistantMessages.Add([string] $jsonEvent.data.content)
        }
    }
    if ($assistantMessages.Count -lt 1) { throw 'Copilot JSONL did not contain a final assistant.message.' }
    $assistantMessages[$assistantMessages.Count - 1]
} elseif($OutputProtocol -ceq 'ClaudeStructuredJson') {
    $envelope=($output -join "`n")|ConvertFrom-Json -AsHashtable -Depth 100
    if($envelope -isnot [Collections.IDictionary] -or -not $envelope.Contains('structured_output') -or
        $envelope.structured_output -isnot [Collections.IDictionary] -or ($envelope.Contains('is_error') -and $envelope.is_error)){
        throw 'Claude did not return a successful structured_output object.'
    }
    $envelope.structured_output|ConvertTo-Json -Depth 100 -Compress
} else { $output -join "`n" }
if ([string]::IsNullOrWhiteSpace($text)) {
    throw 'Harness returned no structured standard output.'
}
$parsed = $text | ConvertFrom-Json -AsHashtable -Depth 100
if ($parsed -isnot [Collections.IDictionary]) {
    throw 'Harness standard output must be one JSON object.'
}

$target = [IO.Path]::GetFullPath($OutputFile)
$directory = Split-Path -Parent $target
[void](New-Item -ItemType Directory -Path $directory -Force)
$temporary = Join-Path $directory ('.' + [IO.Path]::GetFileName($target) + '.' + [guid]::NewGuid().ToString('N') + '.tmp')
try {
    [IO.File]::WriteAllText($temporary, $text + "`n", [Text.UTF8Encoding]::new($false))
    [IO.File]::Move($temporary, $target, $true)
} finally {
    if (Test-Path -LiteralPath $temporary) { Remove-Item -LiteralPath $temporary -Force }
}
$text
