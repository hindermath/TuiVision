# TProgB Beispiel / TProgB Example

## Deutsch

`TProgB` portiert den erweiterten Fortschrittsbalken mit Abbruch aus
`tv203s/contrib/tvision/examples/tprogb/`. Die verwaltete Version zeigt einen
deterministischen Fortschrittswert und einen sichtbaren Canceled-Zustand.

Interaktiver Laufzeitpfad: `dotnet run --project examples/TProgB` zeigt
Zwecktext und ein TProgB-Menue. Partial, Abort und Cancelled melden
Teilfortschritt, Abbruchanforderung und den separaten Cancelled-Zustand als
Text.

Barrierefreiheit: Fortschritt und Abbruch sind als Text sichtbar.

Akzeptierte Einschraenkung: siehe `docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TProgB"
dotnet run --project examples/TProgB
```

## English

`TProgB` ports the extended progress bar with abort from
`tv203s/contrib/tvision/examples/tprogb/`. The managed version shows a
deterministic progress value and a visible canceled state.

Interactive runtime path: `dotnet run --project examples/TProgB` shows purpose
text and a TProgB menu. Partial, Abort, and Cancelled report partial progress,
abort request, and the separate cancelled state as text.

Accessibility: Progress and abort are visible as text.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TProgB"
dotnet run --project examples/TProgB
```
