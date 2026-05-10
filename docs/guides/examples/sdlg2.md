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
