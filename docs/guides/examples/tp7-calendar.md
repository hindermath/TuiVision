# TP7 Calendar: deterministische Monate / Deterministic Months

## Zweck / Purpose

`Tp7Calendar` übernimmt den Lernzweck aus `TVDEMOS/CALENDAR.PAS`: Monate
vorwärts und rückwärts durchlaufen und Jahreswechsel nachvollziehen.

`Tp7Calendar` retains the learning purpose from `TVDEMOS/CALENDAR.PAS`: move
forward and backward through months and observe year transitions.

## Start / Launch

```bash
dotnet run --project examples/Tp7Calendar
```

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7Calendar -- --smoke
```

## Determinismus und Proof / Determinism and Proof

Die funktionale Stufe startet bewusst mit Dezember 2026. Dadurch belegt der
Smoke den Übergang nach Januar 2027 ohne Abhängigkeit von Systemdatum, Locale
oder Zeitzone. `app.Run()` verbindet Monatszustand, Fenster und gerenderte
`yyyy-MM`-Zellen.

The functional stage intentionally starts in December 2026. This lets the
smoke prove the transition to January 2027 without depending on system date,
locale, or time zone. `app.Run()` combines month state, window, and rendered
`yyyy-MM` cells.

## Showcase-Grenze / Showcase Boundary

Die spätere Stufe ergänzt ein sichtbares Monatsraster, Tagesfokus,
Navigations-Shortcuts und `Help -> Description`.

The later stage adds a visible month grid, day focus, navigation shortcuts,
and `Help -> Description`.
