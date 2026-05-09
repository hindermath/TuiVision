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

Accessibility: Rejection and success states are testable text-first.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~DlgDsn"
dotnet run --project examples/DlgDsn
```
