# TP7 Calculator: funktionaler Rechner / Functional Calculator

## Zweck / Purpose

`Tp7Calculator` übernimmt den historischen Lernzweck aus
`TVDEMOS/CALC.PAS`: Zifferneingabe, Dezimalpunkt, Vorzeichen,
Grundrechenarten, Löschen, Rückschritt und sichtbare Fehlerbehandlung.

`Tp7Calculator` retains the historical learning purpose from
`TVDEMOS/CALC.PAS`: digit entry, decimal point, sign, basic arithmetic, clear,
backspace, and visible error handling.

## Start / Launch

```bash
dotnet run --project examples/Tp7Calculator
```

Der kontrollierte Entry-Point-Smoke verwendet:

The controlled entry-point smoke uses:

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7Calculator -- --smoke
```

## Bedienung / Operation

Die funktionale erste Stufe führt Rechnerfolgen über typisierte
Anwendungsbefehle aus. Der echte App-Loop aktualisiert das sichtbare Fenster
und die Statuszeile. `C` löscht, `B` führt den Rückschritt aus und `S` wechselt
das Vorzeichen.

The functional first stage executes calculator sequences through typed
application commands. The real app loop updates the visible window and status
line. `C` clears, `B` performs backspace, and `S` toggles the sign.

## Moderne Abweichung / Modern Deviation

Die C#-Umsetzung verwendet invariant formatiertes `decimal`. Sie übernimmt
weder Pascal-Gleitkommadetails noch globale Rechnerzustände. Eine Division
durch null verwirft das ungültige Ergebnis atomar und lässt den letzten
gültigen linken Wert sichtbar.

The C# implementation uses invariantly formatted `decimal`. It does not retain
Pascal floating-point details or global calculator state. Division by zero
rejects the invalid result atomically and keeps the last valid left value
visible.

## Accessibility und Proof / Accessibility and Proof

Der Kernpfad ist vollständig per Tastatur erreichbar. Status und Ablehnung
sind textorientiert und nicht nur über Farbe erkennbar. Der primäre Smoke in
`Tp7CalculatorSmokeTests` führt `app.Run()` aus und verbindet Rechnerzustand,
`TWindow`-Identität und gerenderte Zellen.

The core path is fully keyboard reachable. Status and rejection are text-first
and do not rely on colour alone. The primary smoke in
`Tp7CalculatorSmokeTests` runs `app.Run()` and combines calculator state,
`TWindow` identity, and rendered cells.

## Grenze zur Showcase-Stufe / Showcase-Stage Boundary

Feature 032 liefert den funktionalen Command- und Proof-Pfad. Die spätere
Showcase-Stufe ergänzt direkte, sichtbare Rechner-Controls, vollständige
Shortcut-Hinweise und das einheitliche `Help -> Description`-Erlebnis.

Feature 032 delivers the functional command and proof path. The later showcase
stage adds direct visible calculator controls, complete shortcut hints, and
the shared `Help -> Description` experience.
