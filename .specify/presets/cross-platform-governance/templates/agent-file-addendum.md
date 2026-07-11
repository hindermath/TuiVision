## Cross-Platform Governance Agent Guidance

- Treat any new script-shaped tool as a paired artefact: Bash (`*.sh`)
  + PowerShell (`*.ps1`). Never deliver only one variant for shared
  tooling.
- Detect the host OS at the start of work and call the matching
  variant: `bash scripts/xyz.sh` on macOS/Linux,
  `pwsh scripts/xyz.ps1` on Windows. Both variants must be functionally
  equivalent.
- Default to Bash 3.x compatibility (macOS ships Bash 3.2). Document
  any Bash 4+ requirement explicitly.
- For PowerShell, set `Set-StrictMode -Version Latest`, validate
  parameters, avoid `Invoke-Expression` on untrusted input, and use
  `-NoProfile` for subprocess calls.
- Provide a Unix man-page for every shared Bash script (default
  location `docs/man/<name>.1`) using the project man-page template.
- Provide a bilingual (DE first, EN second) PowerShell comment-based
  help block using the project PowerShell help template.
- Expose every shared PowerShell script as an Advanced Function with an
  approved `Verb-Noun` name (use `Get-Verb` to verify; common verbs:
  `New`, `Get`, `Set`, `Remove`, `Test`, `Invoke`, `Update`).
- Honour dry-run parity: `--dry-run` for Bash, `-WhatIf` for
  PowerShell.
- Handle Windows-specific surprises explicitly: `$env:HOME` is an
  empty string (not `$null`); use
  `if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }`.
- File a `script-parity-checklist` for any change that adds or alters a
  paired script.
- Document every `N/A` decision with rationale.

## Audit-Ready Spec-Kit Evidence

- When this preset applies, generated or updated Markdown evidence must include the Spec-Kit run, owner/reviewer, evidence path, applicability decision, N/A rationale where relevant, and open follow-up tracking.
- Do not treat an unfilled starter template as evidence. Evidence exists only after the current run has recorded concrete decisions, paths, and rationale.
