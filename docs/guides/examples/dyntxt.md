# DynTxt Beispiel / DynTxt Example

## Deutsch

`DynTxt` portiert den dynamischen Textzweck aus
`tv203s/contrib/tvision/examples/dyntxt/`. Das Beispiel aktualisiert
Textausgaben vorhersagbar und klemmt lange Werte an die sichtbare Breite.

Interaktiver Laufzeitpfad: `dotnet run --project examples/DynTxt` zeigt
Zwecktext und ein DynTxt-Menue. Short, Long und Constrained setzen die
dynamische Textausgabe ueber Befehle; der Smoke-Test prueft dieselben
sichtbaren Werte ueber die App-Schleife.

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

Interactive runtime path: `dotnet run --project examples/DynTxt` shows purpose
text and a DynTxt menu. Short, Long, and Constrained set dynamic text output
through commands; the smoke test verifies the same visible values through the
app loop.

Accessibility: The visible value is text and does not require color.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~DynTxt"
dotnet run --project examples/DynTxt
```
