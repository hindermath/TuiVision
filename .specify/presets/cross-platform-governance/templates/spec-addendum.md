## Cross-Platform Applicability

- Identify whether this feature adds, changes, or removes a script-shaped
  tool.
- Record the target platforms (macOS, Linux, Windows) and confirm that
  both `*.sh` and `*.ps1` variants are in scope.
- Record the planned man-page location for the Bash variant (default
  `docs/man/<name>.1`).
- Record that the PowerShell variant will provide bilingual
  (DE first, EN second) comment-based help.
- Record the Cmdlet `Verb-Noun` name for the PowerShell variant
  (approved PowerShell verbs only).
- Record dry-run support: `--dry-run` for Bash, `-WhatIf` for
  PowerShell.
- Record any justified `N/A` decisions with rationale (e.g. one of the
  variants is genuinely impossible on a given platform).

## Audit Evidence Applicability

- Record whether this Spec-Kit run requires an evidence document or checklist update.
- Use `Applicable`, `N/A`, or `Open` for each relevant standard or governance checkpoint.
- Document every `N/A` decision with a short rationale and re-evaluation trigger.
- Link the planned evidence path from the feature spec; silent omission is not allowed.
