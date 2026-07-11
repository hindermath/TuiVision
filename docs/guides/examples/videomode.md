# Videomode — Anleitung / Guide

> **Quelle / Source**: `tv203s/contrib/tvision/examples/videomode/test.cc`
> **Wave**: 1 — Pflichtbeispiel aus dem Originalordner `tv203s/contrib/tvision/examples/`
> **Nicht** Bestandteil von `TVDEMOS/` oder `TVFM/`.

---

## Lernziel / Learning Goal

Dieses Beispiel zeigt, wie TuiVision-Anwendungen mit Terminal-Größenänderungen umgehen:
Wenn das Terminal die Größenänderung unterstützt, wird ein echter Übergang durchgeführt.
Andernfalls wird ein sichtbarer Fallback angezeigt, anstatt still zu scheitern.

This example shows how TuiVision applications handle terminal size changes:
If the terminal supports resizing, a real transition is performed.
Otherwise, a visible fallback is displayed instead of failing silently.

---

## Voraussetzungen / Prerequisites

- **[.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)** — Lade das SDK von
  [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) herunter und
  installiere es auf deinem System. /
  Download and install the SDK from
  [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0).

- **Kloniertes TuiVision-Repository** — Klone das Repository mit: /
  Cloned TuiVision repository — clone it with:
  ```
  git clone https://github.com/hindermath/TuiVision.git
  ```

- **Grundkenntnisse in C#** — Wenn du noch neu in C# bist, empfiehlt sich ein Blick auf
  [Microsoft Learn C#](https://learn.microsoft.com/de-de/dotnet/csharp/) als Einstieg. /
  Basic knowledge of C# — if you're new to C#, start with
  [Microsoft Learn C#](https://learn.microsoft.com/de-de/dotnet/csharp/).

- **TuiVision-Ereignissystem** — Das Beispiel nutzt das TuiVision-Ereignissystem.
  Den Quellcode findest du unter `src/TuiVision.Core/TEvent.cs` im Repository. /
  This example uses the TuiVision event system; see `src/TuiVision.Core/TEvent.cs` in
  the repository for reference.

- **Passendes Terminal** — `Console.SetWindowSize()` wird nur von bestimmten Terminals
  unterstützt; auf macOS und Linux greift meistens der Fallback (siehe unten). /
  A terminal that supports `Console.SetWindowSize()` (optional) — on macOS and Linux the
  fallback path is usually taken (see below).

---

## Starten / Startup

```bash
dotnet run --project examples/Videomode
```

Das Programm prüft beim Start die Terminal-Fähigkeiten und zeigt einen
kanonischen Ergebniszustand. `Videomode -> Probe` wiederholt die Prüfung,
`Help -> Description` erklärt die Plattformgrenze, und die Statuszeile hält den
aktuellen Zustand sichtbar.

The program probes terminal capabilities at startup and displays a canonical
result state. `Videomode -> Probe` repeats the probe, `Help -> Description`
explains the platform boundary, and the status line keeps the current state
visible.

**Laufzeit-Fähigkeitshinweise / Runtime capability notes**:
- **Windows**: `Console.SetWindowSize()` wird in der Regel unterstützt. /
  `Console.SetWindowSize()` is typically supported.
- **macOS/Linux**: `Console.SetWindowSize()` schlägt meistens fehl (hängt von der
  Terminal-Emulation ab). Ein sichtbarer Fallback wird angezeigt. /
  `Console.SetWindowSize()` usually fails (depends on terminal emulation). A visible fallback is shown.
- **CI/Headless**: Das Ergebnis hängt vom bereitgestellten Konsolentreiber ab;
  der Test akzeptiert nur einen ehrlichen kanonischen Zustand. /
  The result depends on the provided console driver; the test accepts only an
  honest canonical state.

**Verhalten bei zu kleinen Terminals / Behaviour on undersized terminals**:
Wenn das Terminal zu klein ist, wird der Übergangsversuch zwar unternommen, das Ergebnis
aber klar dokumentiert (Erfolg oder Fallback). Die Anwendung bleibt stabil.

If the terminal is too small, the transition attempt is still made but the result
is clearly documented (success or fallback). The application remains stable.

Der funktionale Nachweis für `014-wave1-functional-hardening` prüft den
gespeicherten Ergebniszustand. `DisplayModeCoordinator.LastOutcome` und
`VideomodeView.LastShownOutcome` muessen zusammenpassen; bei nicht
unterstützten Terminals muss `VisibleFallback` mit einer textorientierten
Meldung sichtbar werden. Ein weiterer Smoke-Test zeigt, dass die Anwendung nach
einem erneuten Übergangsversuch weiter lauffähig bleibt.

The functional proof for `014-wave1-functional-hardening` checks the stored
outcome state. `DisplayModeCoordinator.LastOutcome` and
`VideomodeView.LastShownOutcome` must match; unsupported terminals must expose
`VisibleFallback` with a text-first message. Another smoke test shows that the
application remains runnable after another transition attempt.

---

## Unterstützter Übergangsablauf / Supported Transition Flow

1. `DisplayModeCoordinator` prüft in `ProbeResizeSupport()`, ob `Console.SetWindowSize()`
   auf dem aktuellen Terminal ausführbar ist.
2. `TryTransition(width, height)` zählt den echten Versuch und erhält den
   bestehenden Enum-Vertrag `RealTransition` oder `VisibleFallback`.
3. Der zusätzliche Textzustand unterscheidet `supported`, `unchanged`,
   `rejected` und `fallback`.
4. `VideomodeApp` leitet Enum, Textzustand und Erklärung an
   `VideomodeView.ShowOutcome()` sowie die Statuszeile weiter.

1. `DisplayModeCoordinator` checks in `ProbeResizeSupport()` whether `Console.SetWindowSize()`
   is executable on the current terminal.
2. `TryTransition(width, height)` counts the real attempt and preserves the
   existing `RealTransition` or `VisibleFallback` enum contract.
3. The additional text state distinguishes `supported`, `unchanged`, `rejected`,
   and `fallback`.
4. `VideomodeApp` forwards enum, text state, and explanation to
   `VideomodeView.ShowOutcome()` and the status line.

---

## Expliziter Fallback / Explicit Fallback Explanation

Wenn `ProbeResizeSupport()` fehlschlägt, lautet der sichtbare Zustand `fallback`.
Wenn eine positive Sonde den späteren Wechsel nicht garantiert und
`Console.SetWindowSize()` eine Ausnahme wirft, lautet er `rejected`. In beiden
Fällen bleibt der bestehende Enum-Wert `VisibleFallback`; der Text vermeidet eine
falsche Erfolgsbehauptung. Beim bereits aktiven Ziel heißt der Zustand
`unchanged`, nach einem echten Wechsel `supported`.

```
Dieses Terminal unterstützt keine Größenänderung. / This terminal does not support resizing.
```

Dies ist ein **sichtbarer Fallback** — keine stille Degradierung.

If `ProbeResizeSupport()` fails, the visible state is `fallback`. If a positive
probe does not guarantee the later resize and `Console.SetWindowSize()` throws,
the state is `rejected`. Both retain the existing `VisibleFallback` enum value;
the text avoids a false success claim. An already active target is `unchanged`,
and a real resize is `supported`. This is visible, not silent degradation.

---

## Architekturhinweise / Architecture Hints

```
VideomodeApp (TApplication)
├── DisplayModeCoordinator   ← erkennt Terminal-Fähigkeiten / detects terminal capabilities
│   ├── IsResizeSupported: bool
│   ├── TryTransition(w, h): DisplayModeOutcome
│   ├── LastResultState: string
│   ├── TransitionAttemptCount: int
│   └── FallbackMessage: string
└── VideomodeView (TWindow)  ← zeigt das Ergebnis / displays the result
    ├── ShowOutcome(outcome, message, canonicalState)
    ├── LastShownOutcome: DisplayModeOutcome?
    └── LastShownMessage: string?
```

## Barrierearmer Bedien- und Nachweispfad / Accessible Operation and Proof Path

Probe, Wiederholung, Ergebnis, Status, Beschreibung und Beenden sind über
Tastaturbefehle erreichbar. Jeder Ergebniszustand steht als wörtlicher Text in
der View und Statuszeile. Die Aussage hängt nicht nur von Farbe, Fenstergröße
oder einem hostabhängigen Erfolg ab. Der App-Loop-Smoke prüft außerdem die
gerenderte Ergebnisregion und die Nutzbarkeit nach der Wiederholung.

Probe, retry, result, status, description, and quitting are reachable through
keyboard commands. Every result state appears as literal text in the view and
status line. Meaning does not depend only on colour, window size, or host-specific
success. The app-loop smoke also verifies the rendered result region and
usability after retry.

---

## Übungen / Exercises

1. Fügen Sie einen Menüeintrag hinzu, der verschiedene Terminalgrößen anbietet
   (z. B. 80×25, 132×50).
   Add a menu entry offering different terminal sizes (e.g., 80×25, 132×50).

2. Protokollieren Sie Übergangsversuche und -ergebnisse in einer Datei.
   Log transition attempts and results to a file.

3. Implementieren Sie einen Post-Übergangs-Usability-Test: Prüfen Sie, ob die Anwendung
   nach einem Übergang weiterhin korrekt zeichnet.
   Implement a post-transition usability test: verify that the application continues
   to draw correctly after a transition.

---

## Quellenrückverfolgung / Source Traceability

| Verwaltete Datei / Managed File | Historische Quelle / Historical Source |
|---|---|
| `examples/Videomode/DisplayModeCoordinator.cs` | `tv203s/contrib/tvision/examples/videomode/test.cc` — `TMyApp::testMode()` |
| `examples/Videomode/VideomodeApp.cs` | `tv203s/contrib/tvision/examples/videomode/test.cc` — `TMyApp` |
| `examples/Videomode/VideomodeView.cs` | Neue Klasse für die verwaltete Portierung — kein direktes historisches Gegenstück |
