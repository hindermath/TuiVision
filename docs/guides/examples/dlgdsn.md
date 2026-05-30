# DlgDsn Beispiel / DlgDsn Example

## Deutsch

`DlgDsn` portiert den dynamischen Dialog-Designer-Nachweis aus
`tv203s/contrib/tvision/examples/dlgdsn/`. Die verwaltete Version laedt oder
erzeugt eine strukturierte Dialogbeschreibung, fuehrt einen
Serialization-Roundtrip aus, rendert einen Runtime-Dialog und zeigt eine
einfache Aenderung.

Fehlerhafte Fixtures fuer malformed, incomplete, duplicate-control und
invalid-navigation werden sichtbar abgelehnt. Die Fixture-Dateien liegen unter
`examples/DlgDsn/Fixtures/`.

Interaktiver Laufzeitpfad: `dotnet run --project examples/DlgDsn` zeigt
Zwecktext und ein DlgDsn-Menue. Load/render rendert die gueltige Fixture,
Change zeigt eine geaenderte Eingabe, und die Reject-Befehle melden malformed
oder invalid-navigation sichtbar. Alle Fixture-Zugriffe bleiben auf
source-controlled Dateien begrenzt.

Barrierefreiheit: Rejection und Erfolgszustand sind text-first pruefbar.

Akzeptierte Einschraenkung: siehe `docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~DlgDsn"
dotnet run --project examples/DlgDsn
```

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: `DlgDsn` rendert eine gueltige Dialogbeschreibung als echte
`TDialog`-Baumstruktur und zeigt Ablehnungen ebenfalls sichtbar in einem
Dialog. Die Statuszeile meldet Render-, Aenderungs- und Rejection-Zustaende.
`Help -> Description` erklaert die kontrollierten Fixtures. Der Smoke-Test
nutzt `app.Run()`, prueft den View-Typ und liest eine gerenderte Region. Die
historischen Designer-Dateien bleiben die Referenz fuer Zweck und Objektmodell.
Abweichung: Nur source-controlled Fixtures unter `examples/DlgDsn/Fixtures/`
werden gelesen; kein beliebiges Datei-Oeffnen, kein Speichern von Nutzerdata.

English: `DlgDsn` renders a valid dialog description as a real `TDialog` tree
and shows rejections visibly in a dialog. The status line reports render,
change, and rejection states. `Help -> Description` explains the controlled
fixtures. The smoke test uses `app.Run()`, checks the view type, and reads a
rendered region. Historical designer files remain the reference for purpose
and object model. Deviation: only source-controlled fixtures under
`examples/DlgDsn/Fixtures/` are read; there is no arbitrary file open and no
user-data persistence.

## English

`DlgDsn` ports the dynamic dialog designer proof from
`tv203s/contrib/tvision/examples/dlgdsn/`. The managed version loads or creates
a structured dialog description, performs a serialization roundtrip, renders a
runtime dialog, and shows one simple change.

Invalid fixtures for malformed, incomplete, duplicate-control, and
invalid-navigation cases are visibly rejected. Fixture files live under
`examples/DlgDsn/Fixtures/`.

Interactive runtime path: `dotnet run --project examples/DlgDsn` shows purpose
text and a DlgDsn menu. Load/render renders the valid fixture, Change shows a
modified input value, and the Reject commands report malformed or
invalid-navigation visibly. All fixture access stays limited to source-controlled
files.

Accessibility: Rejection and success states are testable text-first.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~DlgDsn"
dotnet run --project examples/DlgDsn
```
