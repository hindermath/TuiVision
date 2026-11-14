---
description: Update one intake series with explicit authority and preserved lineage.
---

## User Input

```text
$ARGUMENTS
```

Update exactly one active series. Current explicit update authority is required.

1. Validate the accepted manifest and receipt before proposing changes.
2. Present exact target, order, root, edge, lifecycle, and evidence differences.
3. Refuse ambiguous facts and unrelated intake-content changes.
4. Archive the prior manifest and receipt byte-identically.
5. Prepare successor manifest, receipt, operation journal, and order document.
6. Bind the successor through `supersedes`, prior hashes, archive paths, and
   current authority evidence.
7. Run both validators before atomic publication.
8. Do not perform Intake Review or start downstream work.

Finish with changed cardinalities, archive evidence, validation results, and
exactly `$speckit-intake-series-status` as the next action.

## Nutzungshilfe / Reader help

DE: Abhängigkeiten und Reihenfolge vorhandener Arbeitsaufträge erklären, ohne ihre Arbeit zu starten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.md).

EN: Explain dependencies and order of existing work requests without starting their work. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
