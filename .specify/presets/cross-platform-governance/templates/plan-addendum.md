## Cross-Platform Planning Checks

- Confirm that both `*.sh` and `*.ps1` variants will be created or
  updated in this change.
- Plan the manual verification step on at least one target OS per
  variant (macOS or Linux for Bash; Windows for PowerShell).
- Plan the Unix man-page (default location `docs/man/<name>.1`).
- Plan the bilingual (DE first, EN second) PowerShell comment-based
  help block.
- Plan the `Verb-Noun` Cmdlet name and confirm the verb is on the
  approved PowerShell verb list.
- Plan dry-run / `-WhatIf` parity for both variants.
- Plan adherence to the implementation discipline rules: `set -euo
  pipefail`, quoted variables, no `eval`/`Invoke-Expression` on
  untrusted input, `Set-StrictMode -Version Latest`, `-NoProfile` for
  subprocess calls.
- Plan platform-specific guards: empty-string `$env:HOME` on Windows;
  Bash 3.x compatibility on default macOS unless Bash 4+ is documented
  as required.
- Plan a parity-checklist artefact for the change.
