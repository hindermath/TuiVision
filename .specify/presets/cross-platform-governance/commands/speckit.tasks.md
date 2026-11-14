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

## Nutzungshilfe / Reader help

DE: Dieselbe fachliche Funktion auf benannten Plattformen prüfen, ohne nicht ausgeführte Tests zu behaupten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-cross-platform-governance/blob/main/docs/user-guide.md).

EN: Check the same functional behaviour on named platforms without claiming unperformed tests. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-cross-platform-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
