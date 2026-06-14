Before continuing, apply the Cross-Platform Governance preset:

- add explicit tasks for both `*.sh` and `*.ps1` variants in the same
  change
- add tasks for the Unix man-page and the bilingual PowerShell help
  block
- add a task to expose the PowerShell variant as a Cmdlet with an
  approved `Verb-Noun` name
- add a parity-verification task using the script-parity checklist

{CORE_TEMPLATE}

Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.
