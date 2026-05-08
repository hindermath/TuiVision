# DynTxt Beispiel / DynTxt Example

## Deutsch

`DynTxt` portiert den dynamischen Textzweck aus
`tv203s/contrib/tvision/examples/dyntxt/`. Das Beispiel aktualisiert
Textausgaben vorhersagbar und klemmt lange Werte an die sichtbare Breite.

Erwarteter Pfad: kurzen Wert anzeigen, langen Wert clippen und einen engen
Viewport pruefen.

Barrierefreiheit: Der sichtbare Wert ist Text und benoetigt keine Farbe.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~DynTxt"
dotnet run --project examples/DynTxt
```

## English

`DynTxt` ports the dynamic text purpose from
`tv203s/contrib/tvision/examples/dyntxt/`. The example updates text output
predictably and clips long values to the visible width.

Expected path: show a short value, clip a long value, and check a narrow
viewport.

