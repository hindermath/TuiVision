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

Das Programm erkennt beim Start die Terminal-Fähigkeiten und zeigt das Ergebnis an.
The program detects terminal capabilities at startup and displays the result.

**Laufzeit-Fähigkeitshinweise / Runtime capability notes**:
- **Windows**: `Console.SetWindowSize()` wird in der Regel unterstützt. /
  `Console.SetWindowSize()` is typically supported.
- **macOS/Linux**: `Console.SetWindowSize()` schlägt meistens fehl (hängt von der
  Terminal-Emulation ab). Ein sichtbarer Fallback wird angezeigt. /
  `Console.SetWindowSize()` usually fails (depends on terminal emulation). A visible fallback is shown.
- **CI/Headless**: Immer Fallback. / Always fallback.

**Verhalten bei zu kleinen Terminals / Behaviour on undersized terminals**:
Wenn das Terminal zu klein ist, wird der Übergangsversuch zwar unternommen, das Ergebnis
aber klar dokumentiert (Erfolg oder Fallback). Die Anwendung bleibt stabil.

If the terminal is too small, the transition attempt is still made but the result
is clearly documented (success or fallback). The application remains stable.

Der funktionale Nachweis fuer `014-wave1-functional-hardening` prueft den
gespeicherten Ergebniszustand. `DisplayModeCoordinator.LastOutcome` und
`VideomodeView.LastShownOutcome` muessen zusammenpassen; bei nicht
unterstuetzten Terminals muss `VisibleFallback` mit einer textorientierten
Meldung sichtbar werden. Ein weiterer Smoke-Test zeigt, dass die Anwendung nach
einem erneuten Uebergangsversuch weiter lauffaehig bleibt.

The functional proof for `014-wave1-functional-hardening` checks the stored
outcome state. `DisplayModeCoordinator.LastOutcome` and
`VideomodeView.LastShownOutcome` must match; unsupported terminals must expose
`VisibleFallback` with a text-first message. Another smoke test shows that the
application remains runnable after another transition attempt.

---

## Unterstützter Übergangsablauf / Supported Transition Flow

1. `DisplayModeCoordinator` prüft in `ProbeResizeSupport()`, ob `Console.SetWindowSize()`
   auf dem aktuellen Terminal ausführbar ist.
2. Bei Unterstützung: `TryTransition(width, height)` setzt die Konsolengröße und
   gibt `DisplayModeOutcome.RealTransition` zurück.
3. `VideomodeApp` leitet das Ergebnis an `VideomodeView.ShowOutcome()` weiter.

1. `DisplayModeCoordinator` checks in `ProbeResizeSupport()` whether `Console.SetWindowSize()`
   is executable on the current terminal.
2. If supported: `TryTransition(width, height)` sets the console size and returns
   `DisplayModeOutcome.RealTransition`.
3. `VideomodeApp` forwards the result to `VideomodeView.ShowOutcome()`.

---

## Expliziter Fallback / Explicit Fallback Explanation

Wenn `ProbeResizeSupport()` fehlschlägt oder `Console.SetWindowSize()` eine Ausnahme wirft,
wird `DisplayModeOutcome.VisibleFallback` gesetzt. Der Benutzer sieht die Meldung:

```
Dieses Terminal unterstützt keine Größenänderung. / This terminal does not support resizing.
```

Dies ist ein **sichtbarer Fallback** — keine stille Degradierung.

If `ProbeResizeSupport()` fails or `Console.SetWindowSize()` throws, `DisplayModeOutcome.VisibleFallback`
is set. The user sees the message above. This is a **visible fallback** — not a silent degradation.

---

## Architekturhinweise / Architecture Hints

```
VideomodeApp (TApplication)
├── DisplayModeCoordinator   ← erkennt Terminal-Fähigkeiten / detects terminal capabilities
│   ├── IsResizeSupported: bool
│   ├── TryTransition(w, h): DisplayModeOutcome
│   └── FallbackMessage: string
└── VideomodeView (TWindow)  ← zeigt das Ergebnis / displays the result
    ├── ShowOutcome(outcome, message)
    ├── LastShownOutcome: DisplayModeOutcome?
    └── LastShownMessage: string?
```

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
