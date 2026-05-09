# ProgBa Beispiel / ProgBa Example

## Deutsch

`ProgBa` portiert den einfachen Fortschrittsbalken aus
`tv203s/contrib/tvision/examples/progba/`. Die verwaltete Version erreicht die
Fertigstellung deterministisch und ohne Wall-Clock-Assertions.

Erwarteter Pfad: Fortschrittsmaximum setzen und sichtbaren Completed-Zustand
pruefen.

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

Expected path: set the progress maximum and verify the visible completed state.

Accessibility: The status is proven as text and numeric value.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ProgBa"
dotnet run --project examples/ProgBa
```
