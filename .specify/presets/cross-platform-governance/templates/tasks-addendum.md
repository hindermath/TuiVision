## Cross-Platform Tasks

- Add explicit tasks to create or update both `*.sh` and `*.ps1`
  variants of every script-shaped tool in this change.
- Add a manual-verification task per variant on at least one target OS.
- Add a task to write or update the Unix man-page for the Bash variant
  (default location `docs/man/<name>.1`) using
  `man-page-template`.
- Add a task to write or update the bilingual (DE first, EN second)
  PowerShell comment-based help block using
  `powershell-help-template`.
- Add a task to expose the PowerShell variant as a Cmdlet (Advanced
  Function) with an approved `Verb-Noun` name.
- Add a task to confirm dry-run / `-WhatIf` parity.
- Add a task to confirm implementation discipline (Bash quoting,
  `set -euo pipefail`, no `eval`; PowerShell `Set-StrictMode
  -Version Latest`, `-NoProfile`, validated parameters, no
  `Invoke-Expression` on untrusted input).
- Add a task to confirm platform guards (empty-string `$env:HOME` on
  Windows; Bash 3.x compatibility on default macOS).
- Add a task to file a `script-parity-checklist` artefact for the
  change.

## Audit Evidence Tasks

- Add tasks to create or update the Markdown evidence/checklist documents for this Spec-Kit run.
- Each task must name the target evidence file, the standard or governance checkpoint, and the expected decision: `Applicable`, `N/A`, or `Open`.
- Add tasks to fill evidence rows with reviewer, date, evidence path, residual risk, and follow-up where relevant.
- Add tasks to verify that no relevant checkpoint was silently omitted.
