# Demo Beispiel / Demo Example

## Deutsch

`Demo` portiert den Welle-2-relevanten Kern der historischen Demo aus
`tv203s/contrib/tvision/examples/demo/`. Es zeigt Controls, Standarddialoge,
Farb-/Displayauswahl und einfache Gadget-Zustaende. Datei- und
Verzeichnisdialoge nutzen reale lokale Metadaten, Wildcards, manuelle Pfade,
Abbruch und ungueltige Pfade, aber kein Dateiinhalt-I/O.

Ausgeschlossen fuer Welle 2 sind Editor, Hilfe, Streams, Terminalemulation,
Runtime-Maus und echte Charset-Effekte. Diese Punkte bleiben dokumentierte
Omissionen und sind in den Architekturrisiken verlinkt.

Interaktiver Laufzeitpfad: `dotnet run --project examples/Demo` zeigt
Zwecktext und das Demo-Menue. Die Befehle Broad controls/dialogs, Metadata,
Manual path, Cancel, Invalid path, Color/display und Omissions aktualisieren
jeweils sichtbaren Text. Der primaere Smoke-Test fuehrt diese Befehle ueber
die App-Schleife aus.

Barrierefreiheit: Alle Ergebnisse sind als Textzustand sichtbar.

Akzeptierte Einschraenkung: siehe `docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Demo"
dotnet run --project examples/Demo
```

## English

`Demo` ports the wave-2-relevant core of the historical demo from
`tv203s/contrib/tvision/examples/demo/`. It shows controls, standard dialogs,
color/display selection, and simple gadget state. File and directory dialogs use
real local metadata, wildcards, manual paths, cancel, and invalid paths, but no
file-content I/O.

Editor, help, streams, terminal emulation, runtime mouse, and real charset
effects are excluded from wave 2. They remain documented omissions linked from
the architecture risks.

Interactive runtime path: `dotnet run --project examples/Demo` shows purpose
text and the Demo menu. The Broad controls/dialogs, Metadata, Manual path,
Cancel, Invalid path, Color/display, and Omissions commands each update visible
text. The primary smoke test runs these commands through the app loop.

Accessibility: All results are visible as text state.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

Validation:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Demo"
dotnet run --project examples/Demo
```
