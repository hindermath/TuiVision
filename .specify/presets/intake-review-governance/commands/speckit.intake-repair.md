---
description: Repair explicitly authorized intake findings and perform a complete re-review.
---

## User Input

```text
$ARGUMENTS
```

Require a current intake-review result and explicit authority to modify the
named targets. Preserve unrelated work.

1. Revalidate target hashes before editing. Stop on drift.
2. Automatically repair only mechanical defects such as broken local links,
   duplicate numbering, malformed fenced blocks, inconsistent copied prompt
   paths, or deterministic ordering errors when the intended meaning is
   unambiguous.
3. Require an explicit user answer before changing goals, scope, non-goals,
   requirements, acceptance thresholds, delivery authority, security/privacy/
   A11Y decisions, risk acceptance, dependency ownership, or campaign
   exceptions.
4. Record each changed finding and authorization. Do not broaden the intake or
   create implementation artifacts.
5. Run a complete intake review again, produce a new result with `supersedes`,
   and validate all current hashes. A repaired file is not accepted merely
   because the edit succeeded.

Finish with changed targets, resolved and remaining findings, new outcome, and
the next exact action.

## Nutzungshilfe / Reader help

DE: Einen bestehenden Arbeitsauftrag fachlich prüfen, ohne ihn still zu reparieren. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/main/docs/user-guide.md).

EN: Assess an existing work request without silently repairing it. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
