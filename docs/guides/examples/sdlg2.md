# Sdlg2 Beispiel / Sdlg2 Example

## Deutsch

`Sdlg2` portiert den historischen horizontal und vertikal scrollbaren
`ScrollDialog`/`ScrollGroup`-Zweck aus
`tv203s/contrib/tvision/examples/sdlg2/`. Das Beispiel konsumiert
`TScrollGroup` und zeigt beide Scrollachsen mit sichtbarer Zell-/Control-
Ausgabe.

Erwarteter Pfad: zu einer Zelle scrollen, eine andere Zelle fokussieren und
Bounds pruefen.

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

