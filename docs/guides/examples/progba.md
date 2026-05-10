# ProgBa Beispiel / ProgBa Example

## Deutsch

`ProgBa` portiert den einfachen Fortschrittsbalken aus
`tv203s/contrib/tvision/examples/progba/`. Die verwaltete Version erreicht die
Fertigstellung deterministisch und ohne Wall-Clock-Assertions.

Interaktiver Laufzeitpfad: `dotnet run --project examples/ProgBa` zeigt
Zwecktext und ein ProgBa-Menue. Complete setzt den Fortschritt deterministisch
auf `10/10` und meldet den Completed-Zustand sichtbar.

Barrierefreiheit: Der Status wird als Text und numerischer Wert nachgewiesen.

Akzeptierte Einschraenkung: siehe `docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ProgBa"
dotnet run --project examples/ProgBa
```

## English

`ProgBa` ports the simple progress bar from
`tv203s/contrib/tvision/examples/progba/`. The managed version reaches
completion deterministically and without wall-clock assertions.

Interactive runtime path: `dotnet run --project examples/ProgBa` shows purpose
text and a ProgBa menu. Complete deterministically sets progress to `10/10` and
reports the completed state visibly.

Accessibility: The status is proven as text and numeric value.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ProgBa"
dotnet run --project examples/ProgBa
```
