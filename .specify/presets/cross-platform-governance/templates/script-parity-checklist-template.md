# Script Parity Checklist

## Scope

- Script (logical name):
- Change description:
- Reviewer:
- Date:

## Variant Coverage

| Variant | File | Created/updated? | Verified on | Notes |
|---------|------|------------------|-------------|-------|
| Bash | `scripts/<name>.sh` | | macOS / Linux | |
| PowerShell | `scripts/<name>.ps1` | | Windows / pwsh on macOS | |

## Behavioural Equivalence

- Same arguments accepted and same defaults applied:
- Same exit codes and error messages:
- Same dry-run behaviour (`--dry-run` ↔ `-WhatIf`):
- Same output format (where applicable):
- Same side-effects (files written, network calls, registry/keychain
  access):

## Documentation

- Unix man-page present at `docs/man/<name>.1`:
- Man-page sections complete (NAME, SYNOPSIS, DESCRIPTION, OPTIONS,
  ENVIRONMENT, EXIT STATUS, EXAMPLES, SEE ALSO):
- PowerShell comment-based help present and bilingual (DE first,
  EN second):
- PowerShell help renders correctly via `Get-Help <name>`:
- Help switches `-h` / `--help` and `-Help` documented:

## Cmdlet Discipline

- Cmdlet exposed as Advanced Function with `Verb-Noun` name:
- Verb appears in `Get-Verb` output (approved):
- Cmdlet supports `-WhatIf` and `-Confirm` where state-changing:
- Parameters validated (`[ValidateSet]`, `[ValidateRange]`,
  `[ValidatePattern]`):

## Implementation Discipline

- Bash: `set -euo pipefail` (or documented justification):
- Bash: all variables quoted; no `eval` on untrusted input:
- Bash: Bash 3.x compatibility verified (or 4+ requirement documented):
- PowerShell: `Set-StrictMode -Version Latest`:
- PowerShell: no `Invoke-Expression` on untrusted input:
- PowerShell subprocess calls use `-NoProfile`:
- Windows: `$env:HOME` empty-string handling explicit:

## Cross-References

- Spec section that introduced the change:
- Tasks list reference:

## Follow-Up

- Open gaps:
- Required follow-up commits:
- Re-verification trigger:
