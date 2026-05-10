# TCombo Beispiel / TCombo Example

## Deutsch

`TCombo` portiert Kombinationsfelder aus
`tv203s/contrib/tvision/examples/tcombo/`. Das Beispiel zeigt Auswahl,
Synchronisierung des Eingabewerts, leere Auswahl und Grenzlisten.

Interaktiver Laufzeitpfad: `dotnet run --project examples/TCombo` zeigt
Zwecktext und ein TCombo-Menue. Load, Select, Boundary und Empty zeigen die
geladene Auswahl, den sichtbaren Eingabewert, einen ignorierten Grenzindex und
leere Auswahlwerte.

Barrierefreiheit: Auswahl und Eingabetext sind textuell sichtbar.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TCombo"
dotnet run --project examples/TCombo
```

## English

`TCombo` ports combo boxes from `tv203s/contrib/tvision/examples/tcombo/`.
The example shows selection, input-value synchronization, empty choices, and
boundary choice lists.

Interactive runtime path: `dotnet run --project examples/TCombo` shows purpose
text and a TCombo menu. Load, Select, Boundary, and Empty show the loaded
choices, visible input value, ignored boundary index, and empty choices.

Accessibility: Selection and input text are visible as text.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TCombo"
dotnet run --project examples/TCombo
```
