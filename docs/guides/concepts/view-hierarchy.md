# View-Hierarchie, Fokus und Zeichnen / View Hierarchy, Focus, and Drawing

## Deutsch

### Lernziel und Voraussetzungen

Nach dem [Event-Loop-Guide](event-loop.md) lernst du, wie Owner-Beziehungen,
Fokus und Z-Reihenfolge zusammenwirken.

### Hierarchie

- `TView` ist das sichtbare Basiselement mit Bounds, Zustand und Event-Hook.
- `TGroup` besitzt direkte Kind-Views, eine aktuelle fokussierte View und einen
  zusammengesetzten Puffer.
- `TProgram` ist die Wurzel. `TApplication` fügt Menüleiste, Desktop und
  Statuszeile hinzu.
- `TDesktop`, Fenster und Dialoge bilden weitere Gruppen im Baum.

`Insert()` setzt den Owner genau einmal und überträgt nur die vorgesehenen
Gruppenzustände. Fokuswechsel können abgelehnt werden, wenn Ziel oder aktueller
Zustand nicht freigabefähig sind. Maus-Dispatch und Fokus verwenden dasselbe
oberste Ziel, damit verdeckte Views keine Commands auslösen.

### Zeichnen und Proof

Kinder zeichnen in lokale Puffer; Gruppen setzen sie anhand ihrer Origins in
den Elternpuffer ein. Ein belastbarer UI-Proof kombiniert deshalb konkrete
Zustände, View-Typen und sichtbare Buffer-/Cell-Positionen. Ein Screenshot
allein beweist weder Identität noch Fokus.

### Übung

Öffne in `examples/Tp7Demo` zwei Fenster und verwende `Next`. Benenne Wurzel,
Desktop, Fenster und fokussierte View. Nächster Schritt:
[Koordinatensystem](coordinate-system.md).

## English

### Learning goal and hierarchy

After the event-loop guide, learn how ownership, focus, and Z order interact.
`TView` is the visible base element. `TGroup` owns direct children, one current
focus target, and a composed buffer. `TProgram` is the root; `TApplication`
adds menu bar, desktop, and status line.

Insertion establishes one owner. Focus can be rejected when the current view
cannot release it or the target is not eligible. Mouse dispatch and focus use
the same topmost target, so a covered view cannot invoke commands.

### Drawing, proof, and exercise

Children draw locally and groups compose them by origin. Strong UI proof
combines state, view identity, and rendered cells; a screenshot alone does not
prove identity or focus. Open two `Tp7Demo` windows, use `Next`, identify the
tree nodes, and continue with the coordinate-system guide.
