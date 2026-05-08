# Sdlg Beispiel / Sdlg Example

## Deutsch

`Sdlg` portiert den historischen vertikalen `ScrollDialog`/`ScrollGroup`-Zweck
aus `tv203s/contrib/tvision/examples/sdlg/`. Das Beispiel konsumiert das neue
managed `TScrollGroup` aus `TuiVision.Controls` und zeigt vertikales Scrollen,
Fokuszustand, Grenzen und sichtbare Controls.

Erwarteter Pfad: zu einem spaeteren Control scrollen und einen weiteren
Control-Fokus sichtbar machen.

Barrierefreiheit: Der sichtbare Zustand nennt die Controls textuell.

Akzeptierte Einschraenkung und Cleanup: siehe
`docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Sdlg"
dotnet run --project examples/Sdlg
```

## English

`Sdlg` ports the historical vertical `ScrollDialog`/`ScrollGroup` purpose from
`tv203s/contrib/tvision/examples/sdlg/`. The example consumes the new managed
`TScrollGroup` from `TuiVision.Controls` and shows vertical scrolling, focus
state, bounds, and visible controls.

