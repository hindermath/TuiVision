# XTerm Beispiel / XTerm Example

## Deutsch

`XTerm` zeigt ausgewählte historische Tastenfolgen, Farbwerte und Capabilities
als unveränderliches Manifest. Start:

```bash
dotnet run --project examples/XTerm
```

Die Hauptfläche nennt Schlüssel, Wert, Kategorie und `Xterm.res` als Quelle.
`Nächster Eintrag`, Statuszeile und `Help -> Description` bilden den vollständigen
Tastaturpfad.

Die Demo startet kein XTerm, liest keine X-Resource-Datenbank und wertet weder
terminfo noch externe Kommandos aus. Laufzeitparsing bleibt bei der bestehenden
Compatibility-Eingabe und dem Feature-021-Session-Parser. Native oder nicht
belegte Ressourcen erscheinen sichtbar als `Unsupported`.

Der historische Zweck bleibt als prüfbare Ressourcen- und Sequenzübersicht
erhalten. Zustand und Fallback sind Text und benötigen keine Farbwahrnehmung.

Host-Nachweis: Der automatische Pfad beweist die kontrollierte Ansicht, nicht
ein physisches XTerm. Auswahl, Status und Hilfe bleiben per Tastatur erreichbar.

## English

`XTerm` shows selected historical key sequences, colors, and capabilities as an
immutable manifest. Launch it with:

```bash
dotnet run --project examples/XTerm
```

The main area names each key, value, category, and `Xterm.res` as its source.
`Next entry`, the status line, and `Help -> Description` form the complete
keyboard path.

The demo starts no XTerm process, reads no X resource database, and evaluates
neither terminfo nor external commands. Runtime parsing remains with existing
Compatibility input and the Feature-021 session parser. Native or unproven
resources appear visibly as `Unsupported`.

The historical purpose remains as a reviewable resource and sequence overview.
State and fallback are textual and require no color perception.

Host evidence: The automated path proves the controlled view, not a physical
XTerm. Selection, status, and help remain keyboard reachable.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~XTermSmokeTests"
```
