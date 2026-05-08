# ADR 0001: TScrollGroup Foundation

## Status

Accepted for `011-port-wave2-examples`.

## Kontext / Context

Die historischen Beispiele `sdlg` und `sdlg2` implementieren eigene
`ScrollGroup`/`ScrollDialog`-Klassen. Das verwaltete Framework hatte bereits
`TScrollBar` und `TScroller`, aber keinen gruppenbasierten scrollbaren
Container fuer Dialoginhalte.

The historical `sdlg` and `sdlg2` examples implement their own
`ScrollGroup`/`ScrollDialog` classes. The managed framework already had
`TScrollBar` and `TScroller`, but no group-based scrollable container for
dialog content.

## Entscheidung / Decision

`src/TuiVision.Controls/TScrollGroup.cs` wird als minimale managed
Framework-Oberflaeche eingefuehrt. Sie verwaltet `Delta`, `Limit`, optionale
horizontale und vertikale Scrollbars, Fokus-in-Sicht-Bewegung und einen
textorientierten sichtbaren Zustand fuer Smoke-Tests.

`src/TuiVision.Controls/TScrollGroup.cs` is introduced as a minimal managed
framework surface. It manages `Delta`, `Limit`, optional horizontal and vertical
scroll bars, focus-into-view movement, and a text-first visible state for smoke
tests.

## Alternativen / Alternatives

- T042/T043 kontingent lassen: verworfen, weil die historischen Quellen den
  fehlenden Container strukturell belegen.
- Scrollverhalten in `examples/Sdlg/` und `examples/Sdlg2/` duplizieren:
  verworfen, weil das wiederverwendbare Framework-Verhalten waere.
- Vollstaendige Controls/Dialog-Neugestaltung: verworfen, weil Welle 2 nur die
  blockierende Oberflaeche benoetigt.

## Konsequenzen / Consequences

- `sdlg` und `sdlg2` konsumieren dieselbe Framework-Oberflaeche.
- Spaetere Wellen koennen `TScrollGroup` wiederverwenden.
- Zusaetzliche historische Dialogoptik bleibt als Historical Example Parity
  Cleanup dokumentiert.

## Referenz / Reference

Research Decision 11 in `specs/011-port-wave2-examples/research.md`.

