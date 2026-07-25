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
