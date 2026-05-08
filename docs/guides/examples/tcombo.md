# TCombo Beispiel / TCombo Example

## Deutsch

`TCombo` portiert Kombinationsfelder aus
`tv203s/contrib/tvision/examples/tcombo/`. Das Beispiel zeigt Auswahl,
Synchronisierung des Eingabewerts, leere Auswahl und Grenzlisten.

Erwarteter Pfad: mehrere Werte laden, einen Wert auswaehlen, Eingabetext
pruefen und leere Auswahl sichtbar machen.

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

