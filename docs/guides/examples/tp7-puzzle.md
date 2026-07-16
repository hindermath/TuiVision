# TP7 Puzzle: feste Schiebepuzzle-Fixture / Fixed Sliding-Puzzle Fixture

## Zweck / Purpose

`Tp7Puzzle` übernimmt den Lernzweck aus `TVDEMOS/PUZZLE.PAS`: Nur ein zum
Leerfeld benachbarter Stein darf bewegt werden.

`Tp7Puzzle` retains the learning purpose from `TVDEMOS/PUZZLE.PAS`: only a
tile adjacent to the blank may move.

## Start / Launch

```bash
dotnet run --project examples/Tp7Puzzle
```

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7Puzzle -- --smoke
```

## Verhalten und Proof / Behavior and Proof

Das feste 4x4-Board beginnt mit Stein `15` neben dem Leerfeld. Der primäre
Smoke akzeptiert diesen Zug, lehnt danach einen nicht benachbarten Stein ab und
beweist, dass das gültige Board erhalten bleibt. `app.Run()` verbindet
Move-Zähler, Boardzustand, Fenster und gerenderte Zellen.

The fixed 4x4 board starts with tile `15` next to the blank. The primary smoke
accepts this move, then rejects a non-adjacent tile and proves that the valid
board remains intact. `app.Run()` combines move count, board state, window,
and rendered cells.

## Showcase-Grenze / Showcase Boundary

Die spätere Stufe ergänzt auswählbare Kacheln, Fokusreihenfolge,
Tastaturhinweise, Layout-Proof und `Help -> Description`.

The later stage adds selectable tiles, focus order, keyboard hints, layout
proof, and `Help -> Description`.
