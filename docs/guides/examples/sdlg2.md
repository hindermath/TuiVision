# Sdlg2 Beispiel / Sdlg2 Example

## Deutsch

`Sdlg2` portiert den historischen horizontal und vertikal scrollbaren
`ScrollDialog`/`ScrollGroup`-Zweck aus
`tv203s/contrib/tvision/examples/sdlg2/`. Das Beispiel konsumiert
`TScrollGroup` und zeigt beide Scrollachsen mit sichtbarer Zell-/Control-
Ausgabe.

Interaktiver Laufzeitpfad: `dotnet run --project examples/Sdlg2` zeigt
Zwecktext und ein Sdlg2-Menue. Scroll both, Focus far und Boundary bewegen
horizontale und vertikale Offsets zu Zellen ausserhalb des Start-Viewports und
melden Zellkoordinaten als Text.

Barrierefreiheit: Zellkoordinaten und Fokus werden als Text gemeldet.

Akzeptierte Einschraenkung und Cleanup: siehe
`docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Sdlg2"
dotnet run --project examples/Sdlg2
```

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: `Sdlg2` zeigt eine sichtbare zweiachsige `TScrollGroup`. Scroll both,
Focus far und Boundary pruefen horizontale und vertikale Bewegung sowie
Fokusverfolgung. Die Statuszeile meldet Ziel und Fokus, `Help -> Description`
erklaert beide Achsen. Der Smoke-Test beweist `app.Run()`, `TScrollGroup`,
Offset-Werte und eine gerenderte Region mit `Cell 29/19`. Historisch folgt
dies `scrldlg.cpp` und `scrlgrp.cpp` aus `sdlg2`. Abweichung: Die Bewegung ist
deterministisch statt modal-interaktiv.

English: `Sdlg2` shows a visible two-axis `TScrollGroup`. Scroll both, Focus
far, and Boundary prove horizontal and vertical movement plus focus tracking.
The status line reports target and focus, and `Help -> Description` explains
both axes. The smoke test proves `app.Run()`, `TScrollGroup`, offset values,
and a rendered region with `Cell 29/19`. This follows `scrldlg.cpp` and
`scrlgrp.cpp` from `sdlg2`. Deviation: movement is deterministic instead of
modal-interactive.

## English

`Sdlg2` ports the historical horizontally and vertically scrollable
`ScrollDialog`/`ScrollGroup` purpose from
`tv203s/contrib/tvision/examples/sdlg2/`. The example consumes `TScrollGroup`
and shows both scroll axes with visible cell/control output.

Interactive runtime path: `dotnet run --project examples/Sdlg2` shows purpose
text and an Sdlg2 menu. Scroll both, Focus far, and Boundary move horizontal
and vertical offsets to cells outside the initial viewport and report cell
coordinates as text.

Accessibility: Cell coordinates and focus are reported as text.

Accepted limitation and cleanup: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Sdlg2"
dotnet run --project examples/Sdlg2
```
