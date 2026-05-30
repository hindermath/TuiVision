# InpLis Beispiel / InpLis Example

## Deutsch

`InpLis` portiert Eingabelisten aus
`tv203s/contrib/tvision/examples/inplis/`. Das Beispiel synchronisiert
Listenauswahl, Eingabetext und History-Zustand.

Interaktiver Laufzeitpfad: `dotnet run --project examples/InpLis` zeigt
Zwecktext und ein InpLis-Menue. Load, Next, Commit, Recall, Boundary und Empty
zeigen Auswahl, Eingabe, session-only History, Grenzwerte und Leerzustand als
Text. Es wird keine History auf Datentraeger geschrieben.

Barrierefreiheit: Der Nachweis ist keyboard-first und text-first.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~InpLis"
dotnet run --project examples/InpLis
```

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: `InpLis` nutzt eine sichtbare `TDialog`-Komposition mit `TListBox`,
`TInputLine` und text-first History-/Grenzzustand. Load, Next, Commit, Recall,
Boundary und Empty aktualisieren Dialog und Statuszeile. `Help -> Description`
erklaert die Liste, Eingabe und session-only History. Der Smoke-Test beweist
`app.Run()`, View-Typ und gerenderte Region. Historisch folgt dies
`inplist.cpp`. Abweichung: Die History bleibt nur im Speicher und wird nicht
persistiert.

English: `InpLis` uses a visible `TDialog` composition with `TListBox`,
`TInputLine`, and text-first history/boundary state. Load, Next, Commit,
Recall, Boundary, and Empty update the dialog and status line.
`Help -> Description` explains the list, input, and session-only history. The
smoke test proves `app.Run()`, view type, and rendered region. This follows
`inplist.cpp`. Deviation: history is memory-only and is not persisted.

## English

`InpLis` ports input-list behavior from
`tv203s/contrib/tvision/examples/inplis/`. The example synchronizes list
selection, input text, and history state.

Interactive runtime path: `dotnet run --project examples/InpLis` shows purpose
text and an InpLis menu. Load, Next, Commit, Recall, Boundary, and Empty show
selection, input, session-only history, boundary values, and empty state as
text. No history is written to disk.

Accessibility: The proof path is keyboard-first and text-first.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~InpLis"
dotnet run --project examples/InpLis
```
