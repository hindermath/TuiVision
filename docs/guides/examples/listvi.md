# ListVi Beispiel / ListVi Example

## Deutsch

`ListVi` portiert Listenansichten aus
`tv203s/contrib/tvision/examples/listvi/`. Das Beispiel zeigt sichtbare
Auswahlbewegung, erste/letzte Grenze, leere Listen und Viewport-Zustand.

Interaktiver Laufzeitpfad: `dotnet run --project examples/ListVi` zeigt
Zwecktext und ein ListVi-Menue. Load, Last, First und Empty zeigen Auswahl,
erste/letzte Grenze und leere Liste als sichtbaren Textzustand.

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

Interactive runtime path: `dotnet run --project examples/ListVi` shows purpose
text and a ListVi menu. Load, Last, First, and Empty show selection, first/last
boundary, and empty list as visible text state.

Accessibility: The selection is reported as text state.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ListVi"
dotnet run --project examples/ListVi
```
