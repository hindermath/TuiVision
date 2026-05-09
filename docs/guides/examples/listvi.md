# ListVi Beispiel / ListVi Example

## Deutsch

`ListVi` portiert Listenansichten aus
`tv203s/contrib/tvision/examples/listvi/`. Das Beispiel zeigt sichtbare
Auswahlbewegung, erste/letzte Grenze, leere Listen und Viewport-Zustand.

Erwarteter Pfad: drei Eintraege laden, zur letzten Position springen, zur
ersten Grenze zurueckspringen und leere Liste pruefen.

Barrierefreiheit: Die Auswahl wird als Textzustand gemeldet.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ListVi"
dotnet run --project examples/ListVi
```

## English

`ListVi` ports list viewers from `tv203s/contrib/tvision/examples/listvi/`.
The example shows visible selection movement, first/last bounds, empty lists,
and viewport state.

Expected path: load three entries, jump to the last position, jump back to the
first bound, and verify an empty list.

Accessibility: The selection is reported as text state.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ListVi"
dotnet run --project examples/ListVi
```
