# Intake-Abarbeitungsreihenfolge / Intake Processing Order

## Begriffe / Terms

Explain every project-specific or Spec Kit term at first use for the declared
audience. Do not assume prior Spec Kit experience unless project policy says so.

- **Position:** bevorzugte sichtbare Lieferreihenfolge.
- **Root:** Ziel ohne eingehende Kante.
- **Bindende Kante:** Vorgaenger muss abgeschlossen sein.
- **Serialisierung:** gemeinsame Schreibflaechen, aber keine fachliche
  Abhaengigkeit.

## Reihenfolge / Order

| Position | Intake | Status | Zweck |
|---:|---|---|---|
| 1 | `intakes/example.md` | Pending | Beispiel |

## Abhaengigkeiten / Dependencies

```text
Root --> dependent intake
```

The text table and the written dependency list are normative. A diagram may
support them, but must not be the only representation of order, blockers,
status, decisions, or the next action.

## Naechste Kandidaten / Next Candidates

Diese Liste ist eine Auskunft und startet keine Arbeit.

## Nutzungshilfe / Reader help

DE: Abhängigkeiten und Reihenfolge vorhandener Arbeitsaufträge erklären, ohne ihre Arbeit zu starten. Diese Vorlage ist ein auszufüllender Starter, kein bestandener Nachweis. Vorhandene passende Evidence weiterverwenden.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.md).

EN: Explain dependencies and order of existing work requests without starting their work. This template is a starter to complete, not passed evidence. Reuse suitable existing evidence.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
