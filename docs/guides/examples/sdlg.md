# Sdlg Beispiel / Sdlg Example

## Deutsch

`Sdlg` portiert den historischen vertikalen `ScrollDialog`/`ScrollGroup`-Zweck
aus `tv203s/contrib/tvision/examples/sdlg/`. Das Beispiel konsumiert das neue
managed `TScrollGroup` aus `TuiVision.Controls` und zeigt vertikales Scrollen,
Fokuszustand, Grenzen und sichtbare Controls.

Interaktiver Laufzeitpfad: `dotnet run --project examples/Sdlg` zeigt
Zwecktext und ein Sdlg-Menue. Scroll, Focus und Boundary bewegen den
vertikalen Scrollzustand zu Controls ausserhalb des Start-Viewports und melden
Control-Name sowie Offset als Text.

Barrierefreiheit: Der sichtbare Zustand nennt die Controls textuell.

Akzeptierte Einschraenkung und Cleanup: siehe
`docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Sdlg"
dotnet run --project examples/Sdlg
```

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: `Sdlg` zeigt eine sichtbare vertikale `TScrollGroup`. Scroll, Focus
und Boundary bewegen den Viewport zu Controls ausserhalb des Startbereichs und
melden den Zustand in der Statuszeile. `Help -> Description` erklaert Scrollen
und Fokus. Der Smoke-Test beweist `app.Run()`, `TScrollGroup`, Offset-Werte
und eine gerenderte Region mit `Control 40`. Historisch folgt dies
`scrldlg.cpp` und `scrlgrp.cpp`. Abweichung: Der Beweis nutzt deterministische
Commands statt langer modaler Tastaturtraversal.

English: `Sdlg` shows a visible vertical `TScrollGroup`. Scroll, Focus, and
Boundary move the viewport to controls outside the start area and report state
in the status line. `Help -> Description` explains scrolling and focus. The
smoke test proves `app.Run()`, `TScrollGroup`, offset values, and a rendered
region with `Control 40`. This follows `scrldlg.cpp` and `scrlgrp.cpp`.
Deviation: proof uses deterministic commands instead of long modal keyboard
traversal.

## English

`Sdlg` ports the historical vertical `ScrollDialog`/`ScrollGroup` purpose from
`tv203s/contrib/tvision/examples/sdlg/`. The example consumes the new managed
`TScrollGroup` from `TuiVision.Controls` and shows vertical scrolling, focus
state, bounds, and visible controls.

Interactive runtime path: `dotnet run --project examples/Sdlg` shows purpose
text and an Sdlg menu. Scroll, Focus, and Boundary move the vertical scroll
state to controls outside the initial viewport and report the control name plus
offset as text.

Accessibility: The visible state names the controls as text.

Accepted limitation and cleanup: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Sdlg"
dotnet run --project examples/Sdlg
```
