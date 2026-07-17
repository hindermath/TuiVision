# TP7 Demo: sichtbare Anwendungsshell / Visible Application Shell

## Zweck / Purpose

`Tp7Demo` bündelt die historische Absicht aus `TVDEMO.PAS`,
`DEMOCMDS.PAS`, `DEMOSTRS.PAS` und `GADGETS.PAS`: Menü, Commands, Fenster,
Status, Hilfe und begrenzte Idle-Gadgets.

`Tp7Demo` combines the historical intent of `TVDEMO.PAS`, `DEMOCMDS.PAS`,
`DEMOSTRS.PAS`, and `GADGETS.PAS`: menu, commands, windows, status, help, and
bounded idle gadgets.

## Start und Bedienung / Launch and Operation

```bash
dotnet run --project examples/Tp7Demo
```

Der erste Frame zeigt eine reale Familie schließbarer und verschiebbarer
`TWindow`-Instanzen. Das Demo-Menü öffnet weitere Fenster. Das Windows-Menü
bietet Tile, Cascade, Next und Close über die vorhandenen `TDesktop`-Verträge.
F1 oder `Help -> Description` öffnet die vollständige Beschreibung. Leere
Ereignisrunden führen nur einen begrenzten Gadget-Schritt aus; der
Heap-Hinweis ist keine Host-Speichermessung.

The first frame shows a real family of closeable and movable `TWindow`
instances. The Demo menu opens more windows. The Windows menu offers Tile,
Cascade, Next, and Close through the existing `TDesktop` contracts. F1 or
`Help -> Description` opens the complete description. Empty event rounds
perform one bounded gadget step only; the heap note is not a host-memory
measurement.

## A11Y und Proof / A11Y and Proof

Menü, Fensteroperationen, Status und Help sind tastaturerreichbar und
textorientiert. Der primäre Smoke verbindet Command-/Idle-Zustand, reale
Fensterzahl, Fokus, strukturierte Desktop-Ergebnisse, `TWindow`-Identität,
Description und gerenderte Zellen. Die enge `48x16`-Ansicht bewahrt Zweck,
Fenster und F1-Pfad.

Menu, window operations, status, and Help are keyboard reachable and
text-first. The primary smoke combines command/idle state, real window count,
focus, structured desktop results, `TWindow` identity, Description, and
rendered cells. The constrained `48x16` view preserves purpose, windows, and
the F1 path.
