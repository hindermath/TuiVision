# InpLis Beispiel / InpLis Example

## Deutsch

`InpLis` portiert Eingabelisten aus
`tv203s/contrib/tvision/examples/inplis/`. Das Beispiel synchronisiert
Listenauswahl, Eingabetext und History-Zustand.

Erwarteter Pfad: Liste laden, per Tastatur/Index weitergehen, Eingabe in die
History uebernehmen und leere Listen sichtbar behandeln.

Barrierefreiheit: Der Nachweis ist keyboard-first und text-first.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~InpLis"
dotnet run --project examples/InpLis
```

## English

`InpLis` ports input-list behavior from
`tv203s/contrib/tvision/examples/inplis/`. The example synchronizes list
selection, input text, and history state.

Expected path: load a list, move forward, commit input to history, and show
empty lists visibly.

