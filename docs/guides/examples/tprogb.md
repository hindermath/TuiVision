# TProgB Beispiel / TProgB Example

## Deutsch

`TProgB` portiert den erweiterten Fortschrittsbalken mit Abbruch aus
`tv203s/contrib/tvision/examples/tprogb/`. Die verwaltete Version zeigt einen
deterministischen Fortschrittswert und einen sichtbaren Canceled-Zustand.

Erwarteter Pfad: Teilfortschritt setzen, Abbruch ausloesen und Canceled-State
pruefen.

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

