---
description: Logically delete one intake series through archive and tombstone evidence.
---

## User Input

```text
$ARGUMENTS
```

Logically delete exactly one named series with explicit current authority.

1. Validate current manifest and receipt.
2. State that intake documents remain unchanged.
3. Archive manifest and receipt byte-identically.
4. Create a tombstone with identity, authority, reason, UTC time, archive paths,
   hashes, and `intakeDocumentsDeleted: false`.
5. Publish deletion receipt and tombstone only after both validators pass.
6. Do not physically purge evidence or start another command.

Finish with archive and tombstone proof plus one exact safe next action.

## Nutzungshilfe / Reader help

DE: Abhängigkeiten und Reihenfolge vorhandener Arbeitsaufträge erklären, ohne ihre Arbeit zu starten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.md).

EN: Explain dependencies and order of existing work requests without starting their work. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
