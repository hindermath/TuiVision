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

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: `TCombo` zeigt das Kombinationsfeld jetzt in einem sichtbaren Dialog
mit verwalteter `TComboBox`, Eingabewert und Leer-/Grenzzustand. Load, Select,
Boundary und Empty aktualisieren Dialog und Statuszeile. `Help -> Description`
erklaert Auswahl und Eingabesynchronisierung. Der Smoke-Test beweist
`app.Run()`, View-Typ und gerenderte Region. Historisch folgt dies
`tcombobx.cpp`, `tcmbovwr.cpp` und den statischen Input-Line-Quellen.
Abweichung: Die aktuelle managed `TComboBox` ersetzt das historische
Popup-Fenster.

English: `TCombo` now shows the combo box in a visible dialog with managed
`TComboBox`, input value, and empty/boundary state. Load, Select, Boundary,
and Empty update the dialog and status line. `Help -> Description` explains
selection and input synchronisation. The smoke test proves `app.Run()`, view
type, and rendered region. This follows `tcombobx.cpp`, `tcmbovwr.cpp`, and
the static input-line sources. Deviation: the current managed `TComboBox`
replaces the historical popup window.

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
