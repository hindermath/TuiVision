# Terminal Beispiel / Terminal Example

## Deutsch

`Terminal` zeigt die kontrollierte In-Process-Sitzung aus Feature 021 in einer
echten `TTerminalView`. Start:

```bash
dotnet run --project examples/Terminal
```

Die Hauptfläche zeigt Eingabe, Ausgabe und Cursor. Die Statuszeile nennt
Capability, Cursorposition und letztes Ergebnis. `Help -> Description` erklärt
den begrenzten Vertrag. Die Demo startet keinen Prozess, keine Shell und kein PTY.

Unterstützte Aktionen bleiben im dokumentierten C0-/CSI-Subset. Ungültige oder
nicht unterstützte Folgen werden atomar abgelehnt. Danach bleibt eine neue,
unabhängige Eingabe nutzbar. Bei fehlender Terminal-Capability erscheint ein
stabiler textorientierter `Unsupported`-Fallback.

Historisch bleibt der Zweck von `terminal.cc` und `terminal.h` erhalten: Eine
sichtbare Terminalansicht macht Puffer, Cursor und Ablauf prüfbar. Die moderne
Variante verwendet verwaltete Zellen statt Ringpuffer, Hostprozess oder Shell.

Barrierefreiheit: Alle Hauptpfade sind per Tastatur erreichbar. Zustand,
Fallback und nächster Bedienpfad stehen als Text und hängen nicht nur von Farbe ab.

Host-Nachweis: Automatische Tests klassifizieren den Prozess deterministisch.
Physische macOS-, Linux- und Windows-/WSL-Terminals bleiben getrennte manuelle
oder CI-Beobachtungen; die Demo behauptet dafür keinen lokalen Screenshot-Proof.

## English

`Terminal` shows the controlled in-process session from Feature 021 in a real
`TTerminalView`. Launch it with:

```bash
dotnet run --project examples/Terminal
```

The main area shows input, output, and the cursor. The status line names the
capability, cursor position, and latest result. `Help -> Description` explains
the bounded contract. The demo starts no process, shell, or PTY.

Supported actions stay within the documented C0/CSI subset. Invalid or
unsupported sequences are rejected atomically, and the next independent input
remains usable. An unavailable terminal capability produces a stable text-first
`Unsupported` fallback.

The historical purpose of `terminal.cc` and `terminal.h` remains: a visible
terminal view makes buffer, cursor, and flow reviewable. The modern version uses
managed cells instead of a ring buffer, host process, or shell.

Accessibility: All primary paths are keyboard reachable. State, fallback, and
the next operation are text and do not rely on color alone.

Host evidence: Automated tests classify the process deterministically. Physical
macOS, Linux, and Windows/WSL terminals remain separate manual or CI
observations; the demo does not claim local screenshot proof for them.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TerminalSmokeTests"
```
