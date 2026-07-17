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

## Verhalten und Tastatur / Behavior and Keyboard

Das fokussierbare 4x4-Board beginnt mit Stein `15` neben dem Leerfeld.
Pfeiltasten wählen einen Stein; Enter versucht den Zug. Nur ein benachbarter
Stein darf sich bewegen, eine Ablehnung bewahrt das vollständige Board. F1
öffnet die app-spezifische Description.

The focusable 4x4 board starts with tile `15` next to the blank. Arrow keys
select a tile; Enter attempts the move. Only an adjacent tile may move, and a
rejection preserves the complete board. F1 opens the app-specific Description.

## Proof

Der primäre Smoke führt `app.Run()` aus und verbindet Fokus, gültigen Zug,
atomare Ablehnung, Move-Zähler, Statuszeile und gerenderte Zellen. Die
`38x15`-Fixture zeigt Identität, vollständiges Board und Description.

The primary smoke runs `app.Run()` and combines focus, a valid move, atomic
rejection, move count, status line, and rendered cells. The `38x15` fixture
shows identity, the complete board, and Description.
