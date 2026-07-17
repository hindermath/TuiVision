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

## Determinismus und Tastatur / Determinism and Keyboard

Das sichtbare Monatsraster startet bewusst mit Dezember 2026. Pfeiltasten
wählen einen Tag; PageUp und PageDown wechseln den Monat. Dadurch ist der
Übergang nach Januar 2027 ohne Abhängigkeit von Systemdatum, Locale oder
Zeitzone nachvollziehbar. F1 öffnet die app-spezifische Description.

The visible month grid intentionally starts in December 2026. Arrow keys
select a day; PageUp and PageDown change the month. This makes the transition
to January 2027 understandable without depending on system date, locale, or
time zone. F1 opens the app-specific Description.

## Proof

Der primäre Smoke führt `app.Run()` aus und verbindet feste Fixture,
Tagesfokus, Monatswechsel, Statuszeile und gerenderte Zellen. Die
`42x16`-Fixture beweist Identität, Monat und Description in enger Darstellung.

The primary smoke runs `app.Run()` and combines the fixed fixture, day focus,
month transition, status line, and rendered cells. The `42x16` fixture proves
identity, month, and Description in a constrained layout.
