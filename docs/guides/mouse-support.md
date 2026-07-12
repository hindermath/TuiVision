# Maussupport und Interaktion / Mouse Support and Interaction

## Deutsch

### Zweck und Grenze

TuiVision führt reale Terminal-Mausmeldungen zentral über
`TConsoleDriver` und `ConsoleMouseIngress` in das vorhandene `TEvent`-Modell.
Controls und Beispiele lesen keine eigenen Escape-Sequenzen. Feature 020
unterstützt nur vollständige SGR-1006-Meldungen für linkes Drücken, Bewegung
bei gedrückter Taste und Loslassen.

Nicht unterstützt sind native Windows-Console-Mausrecords, X10-Parität,
Mausrad, Hover, Touch, weitere Buttons und beliebige Gesten. Diese Grenzen
werden nicht durch stille Fallbacks verdeckt.

### Host- und Capability-Matrix

| Umgebung | Zustand beim Start | Bedingung |
|---|---|---|
| Interaktives macOS-Terminal | SGR 1006 kann aktiviert werden | Ein- und Ausgabe sind TTY; `TERM` ist gesetzt und nicht `dumb` |
| Interaktives Linux-Terminal | SGR 1006 kann aktiviert werden | dieselbe Terminalbedingung |
| WSL in einem passenden Terminal | SGR 1006 kann aktiviert werden | WSL und passende Terminalweiterleitung |
| Native Windows Console | `Unsupported` | Ein eigener Backend-Vertrag fehlt |
| Umgeleitete oder headless Ein-/Ausgabe | `Unsupported` | Tastatur- und Testpfade bleiben verfügbar |

Die drei Zustände sind `Enabled`, `Disabled` und `Unsupported`. Beim Beenden
wird SGR-Reporting abgeschaltet und jeder Press-, Klick- oder Drag-Zustand
verworfen. Ein Aktivierungsfehler lässt die Anwendung im Tastaturmodus.

### Interaktionen

- Ein MouseDown fokussiert genau die oberste sichtbare, nicht deaktivierte und
  selektierbare View am Zielpunkt.
- Ein Button verwendet danach seinen vorhandenen Command-Pfad genau einmal.
- Ein Doppelklick ist nur der zweite linke Press auf derselben Zelle und
  demselben Ziel innerhalb von einschließlich 500 Millisekunden monotonic time.
- Genau ein Drag-Pfad ist vorhanden: Ein Fenster mit `WindowFlags.Move` kann
  an seiner oberen Titelzeile verschoben werden. Das vollständige Fenster
  bleibt innerhalb seines Owners.
- Release übernimmt die begrenzte Position. Escape, Capability-Verlust,
  Deaktivierung, Entfernung und Shutdown brechen den Drag ab.

### Tastatur und Barrierefreiheit

Die Maus ergänzt die Tastatur, ersetzt sie aber nicht. Fokus und Aktivierung
bleiben über die vorhandenen Tab-, Shortcut-, Enter- und Command-Pfade
erreichbar. Fenster lassen sich weiterhin mit `Ctrl+F5`, Pfeiltasten und Enter
verschieben; Escape stellt die Startposition wieder her.

Capability, Fokus, Aktivierung, Doppelklick, Drag und Fallback müssen als Text
verständlich bleiben. Farbe, Zeigerposition oder ein Screenshot sind kein
alleiniger Nachweis.

### Sicherheit und Nachweis

Terminaleingaben gelten als nicht vertrauenswürdig. Erst eine vollständige,
begrenzte und syntaktisch sowie zustandsbezogen gültige Sequenz erzeugt genau
ein Framework-Ereignis. Fehlerhafte Beobachtungen erzeugen kein Teilereignis
und beschädigen die nächste eigenständige Beobachtung nicht.

Deterministische Tests beweisen Parser, Zustandsfolgen und App-Loop-Verhalten.
Sie beweisen keinen nicht ausgeführten physischen Host. Die aktuelle lokale
Codex-Session ist auf macOS headless mit `TERM=dumb`; deshalb ist der physische
macOS-Spot-Check `NotRun`, nicht `Pass`.

## English

### Purpose and Boundary

TuiVision routes real terminal mouse reports centrally through
`TConsoleDriver` and `ConsoleMouseIngress` into the existing `TEvent` model.
Controls and examples do not parse their own escape sequences. Feature 020
supports only complete SGR 1006 reports for left press, movement while pressed,
and release.

Native Windows Console mouse records, X10 parity, wheel, hover, touch, extra
buttons, and arbitrary gestures are unsupported. These boundaries are not
hidden behind silent fallbacks.

### Host and Capability Matrix

| Environment | Startup state | Condition |
|---|---|---|
| Interactive macOS terminal | SGR 1006 can be enabled | Input and output are TTY; `TERM` is set and not `dumb` |
| Interactive Linux terminal | SGR 1006 can be enabled | Same terminal condition |
| WSL in a suitable terminal | SGR 1006 can be enabled | WSL and suitable terminal forwarding |
| Native Windows Console | `Unsupported` | A dedicated backend contract is absent |
| Redirected or headless I/O | `Unsupported` | Keyboard and test paths remain available |

The three states are `Enabled`, `Disabled`, and `Unsupported`. Shutdown disables
SGR reporting and clears every press, click, or drag state. Activation failure
keeps the application in keyboard mode.

### Interactions

- MouseDown focuses exactly one topmost visible, enabled, selectable view at the target.
- A button then uses its existing command path exactly once.
- Double click means only the second left press on the same cell and target
  within an inclusive 500 milliseconds of monotonic time.
- Exactly one drag path exists: a window with `WindowFlags.Move` can move from
  its top title row while the complete window remains inside its owner.
- Release commits the clamped position. Escape, capability loss, disable,
  removal, and shutdown cancel the drag.

### Keyboard and Accessibility

Mouse input augments the keyboard; it does not replace it. Existing Tab,
shortcut, Enter, and command paths retain focus and activation. Windows still
move with `Ctrl+F5`, arrow keys, and Enter; Escape restores the start position.

Capability, focus, activation, double click, drag, and fallback remain
understandable as text. Colour, pointer position, or a screenshot is not
sufficient proof on its own.

### Security and Proof

Terminal input is untrusted. Only a complete, bounded, syntactically valid, and
state-valid sequence creates exactly one framework event. Rejected observations
create no partial event and do not damage the next independent observation.

Deterministic tests prove parser, state, and app-loop contracts. They do not
prove a physical host that was not exercised. The current local Codex session
runs headless on macOS with `TERM=dumb`; therefore the physical macOS spot-check
is `NotRun`, not `Pass`.
