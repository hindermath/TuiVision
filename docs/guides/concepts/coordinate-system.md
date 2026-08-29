# Koordinatensystem und Rechtecke / Coordinate System and Rectangles

## Deutsch

### Lernziel und Voraussetzungen

Du kennst die [View-Hierarchie](view-hierarchy.md). Jetzt kannst du lokale
Positionen, globale Bildschirmpositionen und rechteckige Bereiche sicher
unterscheiden.

### Vertrag

`TPoint` speichert ganzzahlige X-/Y-Koordinaten. `TRect.A` ist die inklusive
linke obere Ecke; `TRect.B` ist die exklusive rechte untere Ecke. Deshalb gilt:

```text
Breite = B.X - A.X
Höhe   = B.Y - A.Y
enthalten: A.X <= X < B.X und A.Y <= Y < B.Y
```

`TView.Origin` ist relativ zum Owner. `MakeGlobal()` addiert Origins entlang
der Owner-Kette. `MakeLocal()` zieht den globalen Ursprung der View ab. Diese
Regel ist wichtig für Maus-Hit-Tests, Pufferkomposition und verschachtelte
Dialoge.

### Randbedingungen

- Negative Breite oder Höhe ist ungültig.
- Die exklusive B-Ecke vermeidet doppelte Randzellen bei benachbarten Views.
- Clipping begrenzt Zeichnen; es ändert nicht still die fachlichen Bounds.
- Ein enger Terminal-Fallback muss Inhalte neu anordnen oder ehrlich begrenzen,
  nicht außerhalb des Puffers schreiben.

### Übung

Eine View liegt bei `(3,2)` in einer Gruppe, die bei `(10,4)` liegt. Berechne
die globale Position des lokalen Punkts `(1,1)`: `(14,7)`. Prüfe das Ergebnis
mit `MakeGlobal()`. Nächster Schritt: [Serialisierung](serialization.md).

## English

### Learning goal and contract

You already know the view hierarchy. `TPoint` stores integer coordinates.
`TRect.A` is inclusive and `TRect.B` is exclusive, so width and height are the
differences shown above. A view origin is relative to its owner. `MakeGlobal()`
adds origins through the owner chain; `MakeLocal()` removes the view's global
origin.

This convention prevents duplicate border cells and supports predictable hit
testing, clipping, and buffer composition. Negative dimensions are invalid.

### Exercise

For a view at `(3,2)` inside a group at `(10,4)`, local `(1,1)` becomes global
`(14,7)`. Verify it with `MakeGlobal()` and continue with serialization.
