---
description: Validate current intake-series state without changing files.
---

## User Input

```text
$ARGUMENTS
```

Inspect one named series read-only.

1. Hash run state and tracked evidence before inspection.
2. Run Bash and PowerShell validators.
3. Report identity, status, targets, roots, dependencies, eligible targets,
   blockers, receipt lineage, archive/tombstone state, and drift.
   `Idle` is valid only for a repository with zero active targets, roots, and
   dependencies; report that state explicitly instead of inventing a target.
4. When schema-2.0 requirements governance is configured, validate it first and
   resolve target paths from its portable roles and collection paths. Confirm
   that the human-readable order and manifest use the same resolved paths.
5. Classify ambiguity or drift fail-closed. Do not repair it.
6. Prove before/after hashes and Git status are unchanged.
7. Do not stage, commit, push, review, or execute a target.

Finish with one exact next action.

## Nutzungshilfe / Reader help

DE: Abhängigkeiten und Reihenfolge vorhandener Arbeitsaufträge erklären, ohne ihre Arbeit zu starten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.md).

EN: Explain dependencies and order of existing work requests without starting their work. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
