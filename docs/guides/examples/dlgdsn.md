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
