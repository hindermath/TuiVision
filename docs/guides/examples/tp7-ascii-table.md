# TP7 ASCII Table: Bytewerte untersuchen / Inspect Byte Values

## Zweck / Purpose

`Tp7AsciiTable` übernimmt den Lernzweck aus `TVDEMOS/ASCIITAB.PAS`: einen
Bytewert auswählen und seine dezimale, hexadezimale und druckbare Darstellung
gemeinsam sehen.

`Tp7AsciiTable` retains the learning purpose from `TVDEMOS/ASCIITAB.PAS`:
select a byte value and view its decimal, hexadecimal, and printable
representations together.

## Start / Launch

```bash
dotnet run --project examples/Tp7AsciiTable
```

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7AsciiTable -- --smoke
```

## Verhalten und Proof / Behavior and Proof

Typisierte Commands bewegen die Auswahl oder wählen direkt einen Wert von
`0` bis `255`. Eine ungültige direkte Auswahl wird sichtbar abgelehnt und
verändert den letzten gültigen Wert nicht. Der primäre Smoke führt `app.Run()`
aus und prüft Zustand, Fenster und gerenderte Dezimal-/Hex-Zellen.

Typed commands move the selection or directly select a value from `0` through
`255`. An invalid direct selection is visibly rejected and does not change the
last valid value. The primary smoke runs `app.Run()` and verifies state,
window, and rendered decimal/hex cells.

## Moderne Abweichung / Modern Deviation

Nicht druckbare Werte erhalten ein textorientiertes `CTRL-nnn`-Label. Die
Umsetzung übernimmt keine historische Codepage oder Host-Zeichensatztabelle.

Non-printable values receive a text-first `CTRL-nnn` label. The implementation
does not retain a historical code page or host character table.

## Showcase-Grenze / Showcase Boundary

Die spätere Stufe ergänzt ein sichtbares Tabellenraster, direkte Tastaturnavigation,
Fokusnachweis und `Help -> Description`.

The later stage adds a visible table grid, direct keyboard navigation, focus
proof, and `Help -> Description`.
