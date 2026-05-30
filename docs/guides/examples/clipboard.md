# Clipboard Beispiel / Clipboard Example

## Deutsch

`Clipboard` portiert das historische Zwischenablage-Beispiel aus
`tv203s/contrib/tvision/examples/clipboard/`. Der verwaltete Pfad zeigt Copy,
Cut und Paste ueber `ManagedClipboard`. Ein isolierter oder nicht verfuegbarer
Clipboard-Zustand wird sichtbar gemeldet und nicht still uebersprungen.

Interaktiver Laufzeitpfad: `dotnet run --project examples/Clipboard` startet
mit Zwecktext und einem Clipboard-Menue. Die Befehle Copy, Cut, Paste und
Unavailable aktualisieren den sichtbaren Textzustand; der Smoke-Test injiziert
dieselben Befehle ueber die App-Schleife.

Barrierefreiheit: Alle Nachweise sind text-first und keyboard-first. Farbe oder
Maus ist nicht erforderlich.

Akzeptierte Einschraenkung: siehe `docs/architecture/architecture-risks.md`.

Validierung:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Clipboard"
dotnet run --project examples/Clipboard
```

## English

`Clipboard` ports the historical clipboard example from
`tv203s/contrib/tvision/examples/clipboard/`. The managed path shows copy, cut,
and paste through `ManagedClipboard`. An isolated or unavailable clipboard state
is reported visibly instead of being skipped silently.

Interactive runtime path: `dotnet run --project examples/Clipboard` starts
with purpose text and a Clipboard menu. The Copy, Cut, Paste, and Unavailable
commands update the visible text state; the smoke test injects the same
commands through the app loop.

Accessibility: all proof is text-first and keyboard-first. Color or mouse input
is not required.

Accepted limitation: see `docs/architecture/architecture-risks.md`.

## 013 Sichtbarer Nachweis / 013 Visible Proof

Deutsch: In 013 ist `Clipboard` nicht mehr nur ein Textprotokoll. Die
Hauptkomponente ist eine sichtbare `TInputLine`. Copy, Cut, Paste und
Unavailable aktualisieren diese Eingabezeile und die Statuszeile. Der Pfad
`Help -> Description` erklaert den sichtbaren Zweck direkt in der Anwendung.
Der Smoke-Test prueft `app.Run()`, den View-Typ und eine gerenderte
Buffer-Region. Die historische Idee aus `test.cc` und `osclipboard.h` bleibt:
Clipboard-Zustaende sollen sichtbar sein. Abweichung: Der Beweis nutzt die
verwaltete Clipboard-Schicht und einen deterministischen Fallback, nicht die
live OS-Zwischenablage.

English: In 013, `Clipboard` is no longer only a text log. The main component
is a visible `TInputLine`. Copy, Cut, Paste, and Unavailable update that input
line and the status line. `Help -> Description` explains the visible purpose
inside the app. The smoke test checks `app.Run()`, the view type, and a rendered
buffer region. The historical idea from `test.cc` and `osclipboard.h` remains:
clipboard states must be visible. Deviation: proof uses the managed clipboard
layer and a deterministic fallback, not the live OS clipboard.
