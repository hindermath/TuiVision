# Lastenheft: Interaktive Wave-2-Demos

**Dokument-Status:** Entwurf
**Erstellt:** 2026-05-09
**Betrifft:** `examples/`, `tests/TuiVision.Examples.SmokeTests/`, `src/TuiVision.Controls/`
**Empfohlene Prioritaet:** direkt nach Abschluss von `011-port-wave2-examples`
**Empfohlener Spec-Kit-Branch:** `012-interactive-wave2-demos`

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Mit `011-port-wave2-examples` wurden elf Wave-2-Beispiele portiert und durch
deterministische Smoke-Tests abgesichert. Die Beispiele sind als
Nachweisobjekte brauchbar: ihre fachlichen Kernzustaende werden ueber
Headless-Seams, direkte Methoden und Guide-Evidence pruefbar.

With `011-port-wave2-examples`, eleven wave-2 examples were ported and covered
by deterministic smoke tests. They are useful as proof artifacts: their core
behaviour can be verified through headless seams, direct methods, and guide
evidence.

Beim normalen Start mit `dotnet run --project examples/<Name>` zeigen mehrere
Wave-2-Beispiele jedoch nur die Standard-Anwendungsschale aus `TApplication`:
leere Menueleiste, Desktop und Statuszeile. Die eigentlichen Beispielablaeufe
sind nicht oder nur unzureichend ueber sichtbare Menues, Tastaturbefehle,
Dialoge oder Statusausgaben erreichbar.

When started normally with `dotnet run --project examples/<Name>`, several
wave-2 examples show only the default `TApplication` shell: an empty menu bar,
desktop, and status line. Their actual example flows are not, or not
sufficiently, reachable through visible menus, keyboard commands, dialogs, or
status output.

Damit entsteht eine Luecke zwischen Testbarkeit und Nutzbarkeit. Die Beispiele
belegen Verhalten fuer das Repository, wirken fuer Lernende und manuelle
Reviewer aber noch nicht wie echte interaktive Turbo-Vision-Demos.

This creates a gap between testability and usability. The examples prove
behaviour for the repository, but for learners and manual reviewers they do not
yet feel like real interactive Turbo Vision demos.

---

## 2. Ziel / Goal

Alle elf Wave-2-Beispiele sollen beim normalen CLI-Start sichtbare,
bedienbare Demonstrationen zeigen. Die bestehenden smoke-testbaren
Nachweismethoden bleiben erlaubt, muessen aber ueber reale UI-Kommandos,
Menueeintraege oder Tastaturpfade erreichbar werden.

All eleven wave-2 examples shall show visible, usable demonstrations when
started from the CLI. Existing smoke-testable proof methods may remain, but
they must become reachable through real UI commands, menu entries, or keyboard
paths.

Das Feature soll keine neue grosse Framework-Revision erzwingen. Es soll die
vorhandenen Controls und Shell-Bausteine so nutzen, dass die Beispiele als
Anwendungen erlebbar werden und dieselben Pfade deterministisch getestet werden
koennen.

The feature must not force a new broad framework revision. It shall use the
existing controls and shell building blocks so the examples become usable
applications and the same paths can be tested deterministically.

---

## 3. Betroffene Beispiele / Affected Examples

- `Clipboard`
- `Demo`
- `DlgDsn`
- `DynTxt`
- `InpLis`
- `ListVi`
- `ProgBa`
- `Sdlg`
- `Sdlg2`
- `TCombo`
- `TProgB`

`Demo` soll als Vertical Slice dienen. Nach `Demo` werden die uebrigen
Beispiele in Gruppen nachgezogen: einfache Statusbeispiele, Listen/Input,
Dialoge und dynamische Dialogbeschreibung.

`Demo` shall act as the vertical slice. After `Demo`, the remaining examples
are updated in groups: simple status examples, list/input examples, dialogs,
and dynamic dialog descriptions.

---

## 4. Anforderungen / Requirements

### R-01: Normaler CLI-Start zeigt echte Beispielinhalte

Jedes Wave-2-Beispiel muss bei `dotnet run --project examples/<Name>` mehr als
die leere `TApplication`-Standardschale zeigen. Der erste Bildschirm muss einen
erkennbaren Beispielzweck, mindestens einen erreichbaren Bedienpfad und eine
sichtbare Rueckmeldung enthalten.

Each wave-2 example must show more than the empty `TApplication` default shell
when run via `dotnet run --project examples/<Name>`. The first screen must show
a recognizable example purpose, at least one reachable action path, and visible
feedback.

### R-02: Beispielablaeufe sind ueber Menues oder Tastatur erreichbar

Die fachlichen Beispielablaeufe duerfen nicht nur als direkte Methoden fuer
Tests existieren. Sie muessen ueber Menuebefehle, Tastaturbefehle oder
vergleichbare UI-Kommandos erreichbar sein.

The functional example flows must not exist only as direct methods for tests.
They must be reachable through menu commands, keyboard commands, or comparable
UI commands.

### R-03: Sichtbare Ergebniszustaende statt stiller Methodenrueckgaben

Jeder ausgefuehrte Beispielbefehl muss den sichtbaren App-Zustand aktualisieren:
z. B. Desktop-Text, Statuszeile, Auswahlzustand, Fortschritt, Dialogantwort
oder Fehlermeldung. Reine String-Rueckgaben ohne UI-Anbindung reichen nicht.

Each executed example command must update the visible application state: for
example desktop text, status line, selection state, progress, dialog result, or
error message. Pure string return values without UI integration are not enough.

### R-04: Smoke-Tests pruefen skriptbare UI-Pfade

Die Smoke-Tests muessen mindestens einen echten UI-Pfad pro Beispiel
deterministisch ausloesen. Direkte Methodenaufrufe duerfen als Hilfsbeweis
bleiben, aber sie duerfen nicht mehr der einzige Beweis fuer die Hauptfunktion
sein.

The smoke tests must trigger at least one real UI path per example
deterministically. Direct method calls may remain as supporting evidence, but
they must no longer be the only proof for the main function.

### R-05: Gemeinsamer Headless-Eventpfad bleibt stabil

Der bestehende Headless-Seam mit `bool headless` und `GetEvent()`-Override
bleibt die Grundlage. Er soll erweitert werden, damit Tests Command-Events oder
Key-Events in die echte App-Schleife einspeisen koennen.

The existing headless seam with `bool headless` and a `GetEvent()` override
remains the foundation. It shall be extended so tests can inject command events
or key events into the real application loop.

### R-06: Gemeinsame Beispiel-Kommandos statt lokaler Sonderlogik

Falls mehrere Beispiele gleichartige Aktionen brauchen, sollen gemeinsame
Command-IDs, kleine Hilfsbasen oder Test-Utilities entstehen. Lokale
Sonderlogik ist nur zulaessig, wenn sie den historischen Zweck eines einzelnen
Beispiels ausdrueckt.

If several examples need similar actions, shared command IDs, small helper
bases, or test utilities should be introduced. Local special logic is allowed
only when it represents the historical purpose of one specific example.

### R-07: Keine Vorwegnahme spaeterer Wellen

Dieses Feature darf keine Editor-, Hilfe-, Stream-, Terminal-, Charset- oder
Runtime-Maus-Themen aus spaeteren Wellen erzwingen. Solche Themen duerfen als
bewusste Omission sichtbar bleiben.

This feature must not force editor, help, stream, terminal, charset, or runtime
mouse topics from later waves. Those topics may remain visible, deliberate
omissions.

---

## 5. Empfohlener Umsetzungszuschnitt / Recommended Implementation Slice

### Schritt 1: `Demo` als Vertical Slice

- `DemoApp` ueberschreibt `InitMenuBar()`, `InitStatusLine()` und bei Bedarf
  `InitDesktop()`.
- Die heutigen Nachweismethoden werden ueber konkrete Commands erreichbar.
- Ein sichtbarer Ergebnisbereich zeigt den zuletzt ausgefuehrten Demo-Schritt.
- Ein Smoke-Test speist Commands ein und prueft den sichtbaren Zustand.

- `DemoApp` overrides `InitMenuBar()`, `InitStatusLine()`, and, where needed,
  `InitDesktop()`.
- The current proof methods become reachable through concrete commands.
- A visible result area shows the last executed demo step.
- A smoke test injects commands and verifies the visible state.

### Schritt 2: Kleine gemeinsame Runtime-/Test-Helfer

- Gemeinsame Beispiel-Command-IDs definieren.
- Eine minimale Hilfsflaeche fuer sichtbaren Status pruefen.
- Event-Queue oder Command-Queue fuer Headless-Tests einfuehren, wenn sie die
  Beispiele einfacher und konsistenter macht.

- Define shared example command IDs.
- Validate a minimal helper surface for visible status.
- Introduce an event queue or command queue for headless tests if it makes the
  examples simpler and more consistent.

### Schritt 3: Beispiele gruppenweise nachziehen

- Einfache Status-/Textbeispiele: `DynTxt`, `ProgBa`, `TProgB`, `Clipboard`
- Listen und Eingaben: `InpLis`, `ListVi`, `TCombo`
- Scrollbare Dialoge: `Sdlg`, `Sdlg2`
- Dynamische Dialogbeschreibung: `DlgDsn`

- Simple status/text examples: `DynTxt`, `ProgBa`, `TProgB`, `Clipboard`
- Lists and input: `InpLis`, `ListVi`, `TCombo`
- Scrollable dialogs: `Sdlg`, `Sdlg2`
- Dynamic dialog description: `DlgDsn`

---

## 6. Nicht im Scope / Out of Scope

- Vollstaendige historische Pixel-/Zeichenparitaet der alten C++-Demos
- Runtime-Maussupport als Pflichtbedienweg
- Neue Editor-, Help-, Stream-, Terminal- oder Charset-Funktionen
- Entfernen der vorhandenen Smoke-Seams
- Allgemeine DocFX- oder GitHub-Pages-Umstellung

- Full historical pixel/character parity with the old C++ demos
- Runtime mouse support as a required interaction path
- New editor, help, stream, terminal, or charset features
- Removal of the existing smoke seams
- General DocFX or GitHub Pages restructuring

---

## 7. Akzeptanzkriterien / Acceptance Criteria

- `dotnet run --project examples/Demo` zeigt eine sichtbare Demo mit Menue oder
  Tastaturpfad und aktualisiertem Ergebniszustand.
- Jedes der elf Wave-2-Beispiele besitzt mindestens einen interaktiven
  Hauptpfad, der beim normalen Start erreichbar ist.
- Fuer jedes Beispiel prueft mindestens ein Smoke-Test denselben Pfad ueber
  eingespeiste Events oder Commands.
- Die vorhandenen Wave-2-Smoke-Tests bleiben gruen oder werden durch
  staerkere UI-Pfad-Smokes ersetzt.
- `dotnet test tests/TuiVision.Examples.SmokeTests/` bleibt der schnelle
  Nachweis fuer alle gelieferten Beispiele.
- `dotnet test` bleibt gruen.
- Guides nennen die interaktiven Bedienpfade, ohne die Headless-Testdetails als
  Benutzeranleitung zu missbrauchen.

- `dotnet run --project examples/Demo` shows a visible demo with a menu or
  keyboard path and an updated result state.
- Each of the eleven wave-2 examples has at least one interactive main path
  reachable from a normal start.
- For each example, at least one smoke test verifies the same path through
  injected events or commands.
- Existing wave-2 smoke tests remain green or are replaced by stronger UI-path
  smokes.
- `dotnet test tests/TuiVision.Examples.SmokeTests/` remains the fast proof for
  all delivered examples.
- `dotnet test` remains green.
- Guides describe the interactive operation paths without turning headless test
  details into user instructions.

---

## 8. Eingabehinweis fuer Spec-Kit Specify / Input Hint For Spec-Kit Specify

Empfohlener `/speckit-specify`-Input:

```text
Nutze Lastenheft_Interactive-Wave2-Demos.md als Eingabe. Erstelle ein Feature
fuer interaktive Wave-2-Demos: Die elf bereits portierten Wave-2-Beispiele
sollen beim normalen CLI-Start sichtbare, bedienbare Menue-/Tastaturpfade
zeigen. Bestehende smoke-testbare Nachweismethoden muessen ueber echte
UI-Kommandos erreichbar werden, und die Smoke-Tests sollen diese UI-Pfade
deterministisch ueber Events oder Commands pruefen. Scope: examples/,
tests/TuiVision.Examples.SmokeTests/ und nur minimale gemeinsame Controls- oder
Test-Helfer, falls noetig. Out of scope: neue Wave-3-/Wave-4-Funktionalitaet,
Runtime-Mauspflicht, DocFX-/Pages-Umstellung.
```

Recommended `/speckit-specify` input:

```text
Use Lastenheft_Interactive-Wave2-Demos.md as input. Create a feature for
interactive wave-2 demos: the eleven already ported wave-2 examples shall show
visible, usable menu/keyboard paths during normal CLI startup. Existing
smoke-testable proof methods must become reachable through real UI commands,
and the smoke tests shall verify those UI paths deterministically through events
or commands. Scope: examples/, tests/TuiVision.Examples.SmokeTests/, and only
minimal shared Controls or test helpers where needed. Out of scope: new wave-3
or wave-4 functionality, mandatory runtime mouse support, DocFX/Pages
restructuring.
```
