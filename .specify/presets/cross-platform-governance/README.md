# Cross-Platform Governance Preset

Version: `0.1.0`
Requires: `spec-kit >= 0.8.0` (uses the `wrap` and `append` composition
strategies introduced in 0.8.x).

Purpose:

- enforce that every script-shaped tool ships in both Bash and
  PowerShell variants with full parity
- enforce that each variant ships with proper documentation: a Unix
  man-page for Bash, bilingual comment-based help for PowerShell, and a
  `Verb-Noun` Cmdlet alias

Primary source chapter from `home-baseline` constitution:

- `II. Cross-Platform Parity & Documentation`

What it covers:

- mandatory pairing rules (`*.sh` for macOS/Linux, `*.ps1` for Windows
  and as a portable alternative)
- definition-of-done: paired script + man-page + bilingual help +
  approved `Verb-Noun` Cmdlet
- implementation discipline (`set -euo pipefail`, quoted variables,
  `Set-StrictMode -Version Latest`, `-NoProfile`, no
  `Invoke-Expression` on untrusted input)
- platform guards (Bash 3.x default on macOS; empty-string `$env:HOME`
  on Windows)
- dry-run / `-WhatIf` parity
- parity-verification artefact

Preset strategy:

- append cross-platform governance to `constitution-template`,
  `spec-template`, `plan-template`, and `tasks-template`
- provide a standalone agent-guidance addendum template for projects that
  maintain agent instruction files
- wrap `speckit.specify`, `speckit.plan`, and `speckit.tasks` with a
  shared cross-platform workflow
- provide three starter templates: a script-parity checklist, a Unix
  man-page skeleton, and a bilingual PowerShell help skeleton

Evidence templates included:

- `script-parity-checklist-template`
- `man-page-template` (groff section 1)
- `powershell-help-template` (bilingual `<# ... #>` block + Cmdlet
  guidance)

Default evidence locations:

- man-pages: `docs/man/<name>.1`
- parity checklists: `docs/cross-platform/` or alongside the script

When to use:

- any project that ships shell-shaped tooling intended to run on more
  than one OS
- teams that want script-shaped tooling treated as a first-class,
  documented product surface
- projects mixing macOS, Linux, and Windows contributors

When not to use:

- projects targeting only a single OS with no plan to support others
- repositories with no script-shaped tooling

Recommended standalone install priority:

- `25`
