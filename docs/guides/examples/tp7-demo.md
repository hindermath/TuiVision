# TP7 Demo: funktionale Anwendungsshell / Functional Application Shell

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

Der App-Loop verarbeitet typisierte Demo- und Help-Commands genau einmal.
Leere Ereignisrunden führen nur einen begrenzten Gadget-Schritt aus. Der
sichtbare Heap-Hinweis ist bewusst keine Host-Speichermessung.

The app loop processes typed demo and help commands exactly once. Empty event
rounds perform one bounded gadget step only. The visible heap note is
intentionally not a host-memory measurement.

## A11Y und Proof / A11Y and Proof

Menü, Status und Help sind tastaturerreichbar und textorientiert. Der primäre
Smoke verbindet Command-/Idle-Zustand, `TWindow`-Identität und gerenderte
Zellen. Stage 2 ergänzt vollständige Fensteranordnung, Shortcut-Hinweise und
`Help -> Description`.

Menu, status, and help are keyboard reachable and text-first. The primary
smoke combines command/idle state, `TWindow` identity, and rendered cells.
Stage 2 adds complete window arrangement, shortcut hints, and
`Help -> Description`.
