# Intake-Abarbeitungsreihenfolge / Intake Processing Order

## Begriffe / Terms

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

## Naechste Kandidaten / Next Candidates

Diese Liste ist eine Auskunft und startet keine Arbeit.
