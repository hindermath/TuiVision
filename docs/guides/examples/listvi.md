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

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: `ListVi` zeigt die Liste jetzt in einem sichtbaren `TDialog` mit
`TListBox` und text-first Auswahl-/Leerzustand. Load, First, Last und Empty
aktualisieren die Liste und die echte Statuszeile. `Help -> Description`
beschreibt Auswahl, Grenze und Leerliste. Der Smoke-Test beweist `app.Run()`,
View-Typ und Buffer-Region. Historisch folgt dies den List-Viewer-Quellen und
`tlistvie.cc`. Abweichung: Ein fokussierter Listenbeweis ersetzt den
historischen Mehrlisten-Dialog.

English: `ListVi` now shows the list in a visible `TDialog` with `TListBox`
and text-first selection/empty state. Load, First, Last, and Empty update the
list and the real status line. `Help -> Description` describes selection,
boundary, and empty list. The smoke test proves `app.Run()`, view type, and
buffer region. This follows the list viewer sources and `tlistvie.cc`.
Deviation: one focused list proof replaces the historical multi-list dialog.

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
