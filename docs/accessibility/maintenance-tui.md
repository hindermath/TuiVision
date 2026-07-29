# Barrierefreiheit der Wartungs-TUI / Maintenance TUI Accessibility

## Ziel / Goal

Die Wartung muss ab dem ersten Ausbildungsjahr mit Tastatur,
Screenreader, Braille-Zeile, Textbrowser und `NO_COLOR` verständlich bleiben.
WCAG 2.2 Level AA wird auf die anwendbaren Terminalkriterien übertragen.

*Maintenance must remain understandable from the first apprenticeship year
with keyboard, screen reader, Braille display, text browser, and `NO_COLOR`.
WCAG 2.2 Level AA is applied where its criteria fit a terminal interface.*

## Anwendbare Anforderungen / Applicable Requirements

| Thema | Umsetzung und Evidence |
|---|---|
| Tastaturbedienung | Jeder Prompt ist sequenziell erreichbar; keine Maus erforderlich |
| Fokus und Reihenfolge | Eingabe folgt der sichtbaren linearen Reihenfolge; der aktuelle Prompt enthält eine Textfrage |
| Farbe | `NO_COLOR` wird respektiert; Status besitzt immer ein sichtbares Wort |
| Vergrößerung und Breite | Enhanced ab 100, Compact ab 40, darunter lineare Darstellung |
| Bewegung | Live-Aktualisierung höchstens 10 Hz; keine Information nur durch Animation |
| Fehler | Fehlercode, deutsche Erklärung, englische Entsprechung und nächste Aktion |
| Zeit | Keine zeitbegrenzte Auswahl; Prozessabbruch bleibt kontrolliert |
| Sprache | Deutsch zuerst, Englisch danach, CEFR B2; TUI und Dry-run werden beim ersten Auftreten erklärt |
| Kopierbarkeit | Status, Exitcode, Berichtspfad, Logpfad und nächste Aktion bleiben Text |

## Textmodell / Text Model

Jeder Abschluss enthält mindestens:

```text
Status: <STATUS>
Exitcode / exit code: <NUMBER>
Bericht / report: <PATH>
Log / log: <PATH>
Nächste Aktion / next action: <TEXT>
```

Farbe, Tabellenrahmen, Position oder Fortschrittsanzeige dürfen diese Angaben
nicht ersetzen. Fremde Meldungen werden maskiert, damit Zeichen wie
`[red]` weder Inhalt verbergen noch Terminal-Markup einschleusen.

*Color, table borders, position, or progress display cannot replace the
textual fields. Foreign messages are escaped so strings such as `[red]`
cannot hide content or inject terminal markup.*

## Fallback

`TERM=dumb`, umgeleitete Streams, fehlende Terminalfähigkeit oder ein nicht
verfügbarer TUI-Build führen sichtbar zur linearen ASCII-Auswahl. Die
Sicherheitsregeln, auswählbaren Modi, Bestätigung und Exitcodes bleiben
identisch. Ein Eventfehler degradiert die laufende Anzeige dauerhaft zu
linearem Text und zeigt `EVENT_STREAM_DEGRADED`, beendet oder wiederholt aber
nicht die Wartung. Das erste `Ctrl+C` wird genau einmal weitergegeben; weitere
Signale starten keinen zweiten Prozess. Die Schlussansicht bleibt vollständig
kopierbar und nennt Mutationsbarriere, Repository-Zählung, Preset-Phase,
Bericht, Log und nächste Aktion.

*Unsupported terminal capability selects the linear ASCII path. Event failure
permanently shows `EVENT_STREAM_DEGRADED` without ending or repeating
maintenance. The first `Ctrl+C` is forwarded exactly once, later signals cannot
start a second process, and the complete final summary remains copyable.*

## Prüfnachweise / Verification

- Tastatur- und Auswahltests ohne Maus
- `NO_COLOR`- und ASCII-Status-Snapshots
- Breiten `39`, `79` und `120`
- deutsche und englische Textprüfung
- Spectre-Testkonsole mit sichtbarer Textalternative
- Bash-Fallback unter `TERM=dumb`
- macOS-, Ubuntu- und Windows-Ausführung im CI

Nicht anwendbar sind Zeigerzielgröße, Dragging und grafische Reflow-Kriterien,
weil die Oberfläche keine Maussteuerung oder zweidimensionale
Informationsabhängigkeit besitzt. Diese Entscheidung ist neu zu prüfen, sobald
Mausbedienung oder eine grafische Oberfläche hinzukommt.

*Pointer target size, dragging, and graphical reflow are not applicable
because the interface has no mouse control or two-dimensional information
dependency. Re-evaluate this decision if mouse input or a graphical surface is
added.*

<!-- EN: docs/accessibility/maintenance-tui.md
[DE-Zusammenfassung: WCAG-2.2-AA-Anwendung, Textmodell und Fallback der Wartungs-TUI.]
-->
