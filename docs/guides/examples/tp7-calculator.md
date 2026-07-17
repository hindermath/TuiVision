# TP7 Calculator: sichtbarer Rechner / Visible Calculator

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

Der Rechner zeigt ein echtes `TDialog` mit Display und 20 fokussierbaren
Tasten. Ziffern, Dezimalpunkt, `+`, `-`, `*`, `/` und `=` wirken direkt.
Enter führt `=` aus, `C` löscht, Backspace führt den Rückschritt aus, `S`
wechselt das Vorzeichen und Tab bewegt den Fokus durch das Tastenraster.
F1 oder `Help -> Description` öffnet die vollständige Beschreibung.

The calculator shows a real `TDialog` with a display and 20 focusable buttons.
Digits, decimal point, `+`, `-`, `*`, `/`, and `=` act directly. Enter
executes `=`, `C` clears, Backspace removes the last digit, `S` toggles the
sign, and Tab moves focus through the button grid. F1 or
`Help -> Description` opens the complete description.

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
`TDialog`-/`TButton`-Identität, Fokus, echte Statuszeile, Description und
gerenderte Zellen. Die enge `40x12`-Ansicht hält Display, Pflichtbuttons,
F1-Hinweis und Status getrennt sichtbar.

The core path is fully keyboard reachable. Status and rejection are text-first
and do not rely on colour alone. The primary smoke in
`Tp7CalculatorSmokeTests` runs `app.Run()` and combines calculator state,
`TDialog`/`TButton` identity, focus, a real status line, Description, and
rendered cells. The constrained `40x12` view keeps the display, required
buttons, F1 hint, and status visibly separate.

## Proof-Grenze / Proof Boundary

Feature 032 liefert die unveränderte Fachlogik. Feature 033 ergänzt nur die
sichtbare Komposition und deren App-Loop-, Fokus-, Status- und Cell-Proofs.
Der Headless-Smoke beweist nicht die Darstellung eines konkreten
Host-Terminals.

Feature 032 supplies the unchanged domain logic. Feature 033 adds only the
visible composition and its app-loop, focus, status, and cell proofs. The
headless smoke does not prove rendering by a specific host terminal.
