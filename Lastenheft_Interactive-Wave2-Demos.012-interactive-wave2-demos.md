# Lastenheft: Interaktive Wave-2-Demos

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-05-09
**Aktualisiert:** 2026-05-09 nach Merge von PR #24 (`011`-Review-Cleanup)
**Betrifft:** `examples/`, `tests/TuiVision.Examples.SmokeTests/`, `src/TuiVision.Controls/`
**Empfohlene Prioritaet:** naechster Feature-Lauf vor Welle 3
**Empfohlener Spec-Kit-Branch:** `012-interactive-wave2-demos`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, Abschnitt 11.1 und
Abschnitt 12

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist die bewusst vorbereitete Eingabe fuer den naechsten
Spec-Kit-Feature-Lauf. Sie soll zuerst mit `/speckit-specify` in eine
Feature-Spezifikation ueberfuehrt werden; danach folgen `/speckit-plan`,
`/speckit-tasks` und erst dann `/speckit-implement`.

This file is the prepared input for the next Spec-Kit feature run. It shall be
used first with `/speckit-specify` to create a feature specification; then
`/speckit-plan`, `/speckit-tasks`, and only then `/speckit-implement` follow.

- Feature-Name: `012-interactive-wave2-demos`
- Hauptziel: Aus den in `011-port-wave2-examples` bewiesenen Wave-2-Beispielen
  echte interaktive CLI-Demos machen.
- Voraussetzung: PR #24 ist gemergt; die 011-Review-Cleanup-Punkte sind auf
  `main` enthalten.
- Nichtziel: Keine neue Welle-3-/Welle-4-Funktionalitaet, keine Runtime-Maus-
  Pflicht und keine breite Framework-Revision.
- Abschlussgrenze: Jedes der elf Wave-2-Beispiele hat mindestens einen
  sichtbaren Bedienpfad im normalen Start und mindestens einen Smoke-Test ueber
  denselben UI-/Eventpfad.

- Feature name: `012-interactive-wave2-demos`
- Main goal: Turn the wave-2 examples proven in `011-port-wave2-examples` into
  real interactive CLI demos.
- Precondition: PR #24 is merged; the 011 review-cleanup fixes are present on
  `main`.
- Non-goal: No new wave-3/wave-4 functionality, no mandatory runtime mouse
  support, and no broad framework revision.
- Completion boundary: Each of the eleven wave-2 examples has at least one
  visible operation path in normal startup and at least one smoke test through
  the same UI/event path.

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

Nach PR #24 sind die wichtigsten Review-Folgen aus dem 011-PR bereinigt:
`DlgDsn` hat diagnostische Fixture-Ablehnung, `Sdlg`/`Sdlg2` pruefen echten
`TScrollGroup`-Fokus, und die betroffenen Wave-2-Guides besitzen vollstaendige
englische Proof-Bloecke. Damit ist die Basis fuer die interaktive zweite Stufe
hinreichend stabil.

After PR #24, the most important follow-up findings from the 011 PR are fixed:
`DlgDsn` has diagnostic fixture rejection, `Sdlg`/`Sdlg2` verify real
`TScrollGroup` focus, and the affected wave-2 guides contain complete English
proof blocks. This makes the base stable enough for the interactive second
stage.

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

### 2.1 Liefermuster fuer Beispielwellen / Delivery Pattern for Example Waves

Dieses Lastenheft bildet bewusst die zweite Stufe eines zweistufigen
Spec-Kit-Liefermusters. `011-port-wave2-examples` hat die fachlichen
Beispielfunktionen, Nachweismethoden und deterministic smoke paths geliefert.
`012-interactive-wave2-demos` soll darauf aufbauen und diese vorhandenen
Funktionen ueber echte Menues, Statuszeilen, Desktop-Controls, Dialoge,
Tastaturpfade und UI-Event-Smoke-Tests sichtbar machen.

This requirements brief deliberately forms the second stage of a two-stage
Spec-Kit delivery pattern. `011-port-wave2-examples` delivered the functional
example behavior, proof methods, and deterministic smoke paths.
`012-interactive-wave2-demos` shall build on that work and expose those
existing functions through real menus, status lines, desktop controls, dialogs,
keyboard paths, and UI-event smoke tests.

Fuer kuenftige groessere Beispielwellen gilt dieses Muster als bevorzugte
Planungsform, wenn Portierung/Nachweis und interaktive Runtime-Politur sonst
zu gross oder zu riskant fuer einen einzelnen Feature-Lauf wuerden. Eine
Beispielwelle ist erst dann vollstaendig lern- und reviewtauglich, wenn beide
Stufen geliefert sind, sofern der Scope nicht ausdruecklich nur einen minimalen
nicht-interaktiven Nachweis verlangt.

For future larger example waves, use this pattern as the preferred planning
model when porting/proof work and interactive runtime polish would otherwise
be too large or risky for one feature run. An example wave is fully ready for
learners and reviewers only after both stages are delivered, unless the scope
explicitly asks for a minimal non-interactive proof.

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

### 3.1 Beispiel-Matrix fuer 012 / Example Matrix for 012

| Beispiel | Interaktiver Hauptpfad in 012 | Bestehende 011-Basis |
|---|---|---|
| `Clipboard` | Copy, Cut, Paste und Fallback ueber Menue/Tasten ausloesen | `SetInput`, `CopyInput`, `CutInput`, `PasteIntoInput`, `SimulateUnavailableClipboard` |
| `Demo` | Breiten Demo-Fluss, Dateimetadaten, manuellen Pfad, Abbruch/Invalid-State und Farb-/Displayauswahl ueber sichtbare Befehle starten | `RunBroadControlsDialogsGadgetsFlow`, `InspectStandardFileDialog`, `EnterManualPath`, `CancelFileDialog`, `RejectInvalidPath`, `SelectColorAndDisplay` |
| `DlgDsn` | Gueltige Beschreibung laden/rendern/aendern und fehlerhafte Fixtures sichtbar ablehnen | `CreateValidDescription`, `LoadFixture`, `RenderDescription`, `ApplySimpleChange`, `TryLoadFixture` |
| `DynTxt` | Kurzen Text, langen Text und engen Viewport ueber UI-Kommandos demonstrieren | `UpdateText` |
| `InpLis` | Liste laden, Auswahl bewegen, Eingabe in History uebernehmen, leere Liste anzeigen | `LoadItems`, `SelectNext`, `CommitInput` |
| `ListVi` | Eintraege laden, Grenzen ansteuern, leere Liste zeigen | `LoadItems`, `MoveSelection` |
| `ProgBa` | Fortschrittsmaximum setzen und Completed-Zustand sichtbar machen | `RunToCompletion` |
| `Sdlg` | Zu spaeterem Control scrollen und Fokus sichtbar setzen | `ScrollToControl`, `FocusControl` |
| `Sdlg2` | Horizontal/vertikal zu Zelle scrollen und andere Zelle fokussieren | `ScrollToCell`, `FocusControl` |
| `TCombo` | Auswahl laden, Eintrag waehlen, Eingabewert und leere Auswahl zeigen | `LoadChoices`, `SelectIndex` |
| `TProgB` | Teilfortschritt setzen, Abbruch ausloesen und Canceled-State zeigen | `RunTo`, `Abort` |

Die Spalte "Bestehende 011-Basis" ist kein Aufruf zur direkten Testmethode als
Endbeweis. Sie nennt die vorhandenen Funktionen, die in 012 ueber sichtbare
UI-Kommandos und skriptbare Eventpfade erreichbar gemacht werden sollen.

The "existing 011 base" column is not an instruction to keep direct method
calls as the final proof. It names the existing functions that 012 shall expose
through visible UI commands and scriptable event paths.

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

### R-08: Guides beschreiben echte Bedienpfade

Die elf Wave-2-Guides muessen nach der Umsetzung den normalen Start und den
interaktiven Hauptpfad beschreiben. Headless-Seams und direkte Testmethoden
duerfen in Nachweis- oder Entwicklerabschnitten genannt werden, aber nicht als
primaere Benutzerbedienung verkauft werden.

The eleven wave-2 guides must describe normal startup and the interactive main
path after implementation. Headless seams and direct test methods may be named
in proof or developer sections, but must not be presented as the primary user
operation.

### R-09: Kein Rueckfall auf Schein-Interaktion

Ein Beispiel gilt nicht als interaktiv, wenn nur ein Textstring berechnet oder
ein Smoke-Test direkt eine Methode aufruft. Der sichtbare Zustand muss durch
einen App-Befehl, ein Menue, eine Taste oder einen injizierten `TEvent`-Pfad
ausgeloest werden.

An example is not interactive if it only computes a string or if a smoke test
directly calls a method. The visible state must be triggered through an
application command, a menu, a key, or an injected `TEvent` path.

---

## 4.1 Priorisierte User Stories fuer Spec-Kit / Prioritized User Stories for Spec-Kit

### US1 - Demo als sichtbarer Vertical Slice (P1)

Als lernende Person moechte ich `dotnet run --project examples/Demo` starten
und sofort sichtbare Menues, Statushinweise und Ergebniszustaende sehen, damit
ich verstehe, welche Wave-2-Konzepte die Demo zeigt.

As a learner, I want to run `dotnet run --project examples/Demo` and
immediately see visible menus, status hints, and result states, so I understand
which wave-2 concepts the demo shows.

Akzeptanz: `Demo` besitzt mindestens drei sichtbare Befehle fuer vorhandene
011-Funktionen, einen Ergebnisbereich und einen Smoke-Test, der diese Befehle
ueber den Eventpfad ausloest.

Acceptance: `Demo` has at least three visible commands for existing 011
functions, a result area, and one smoke test that triggers these commands
through the event path.

### US2 - Alle Wave-2-Beispiele haben mindestens einen echten Bedienpfad (P1)

Als manuelle reviewende Person moechte ich jedes Wave-2-Beispiel normal
starten und einen sichtbaren Hauptpfad ausloesen koennen, damit das Beispiel
nicht nur als Testfixture, sondern als Demo funktioniert.

As a manual reviewer, I want to start each wave-2 example normally and trigger
one visible main path, so the example works as a demo and not only as a test
fixture.

Akzeptanz: Jedes der elf Beispiele besitzt mindestens einen sichtbaren
Menue-/Tastatur-/Command-Pfad und zeigt danach einen textorientierten
Ergebniszustand.

Acceptance: Each of the eleven examples has at least one visible
menu/key/command path and shows a text-first result state afterwards.

### US3 - Smoke-Tests pruefen dieselben Pfade wie die sichtbare App (P2)

Als Maintainer moechte ich dieselben UI-Pfade deterministisch in Smoke-Tests
einspeisen koennen, damit interaktive Demo-Funktionalitaet nicht nur manuell
behauptet wird.

As a maintainer, I want to inject the same UI paths deterministically into
smoke tests, so interactive demo behavior is not only claimed manually.

Akzeptanz: Pro Beispiel prueft mindestens ein Smoke-Test einen Command- oder
Key/Event-Pfad durch die echte App-Schleife. Direkte Methodenaufrufe bleiben
nur Hilfs- oder Arrange-Schritte.

Acceptance: Per example, at least one smoke test verifies a command or
key/event path through the real application loop. Direct method calls remain
only helper or arrange steps.

### US4 - Guides und Nachweise bleiben bilingual und barrierearm (P2)

Als Auszubildende oder sehbehinderte Person moechte ich die interaktiven Pfade
in Deutsch und Englisch nachvollziehen koennen, damit die Beispiele in
textorientierten Setups verwendbar bleiben.

As an apprentice or visually impaired person, I want to understand the
interactive paths in German and English, so the examples remain usable in
text-oriented setups.

Akzeptanz: Die betroffenen Guides enthalten normale Startbefehle,
Bedienpfade, erwartete Rueckmeldungen und Accessibility-Hinweise in Deutsch
und Englisch.

Acceptance: The affected guides contain normal startup commands, operation
paths, expected feedback, and accessibility notes in German and English.

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

### Schritt 4: Guides und Evidence abschliessen

- Wave-2-Guides auf normale Bedienpfade aktualisieren.
- `examples/README.md` bei geaendertem Bedienmodell nachziehen.
- `specs/012-interactive-wave2-demos/pr-evidence.md` fuer lokale und CI-
  Nachweise anlegen.
- `docs/project-statistics.md` am Ende der Umsetzung fortschreiben.
- Bei DocFX-relevanten Doku-Aenderungen `docfx docfx.json` und
  `npm run test:docfx` unter `tests/web-a11y/` ausfuehren.

- Update wave-2 guides to normal operation paths.
- Update `examples/README.md` if the operation model changes.
- Add `specs/012-interactive-wave2-demos/pr-evidence.md` for local and CI
  proof.
- Update `docs/project-statistics.md` at the end of implementation.
- If documentation changes affect DocFX content, run `docfx docfx.json` and
  `npm run test:docfx` under `tests/web-a11y/`.

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
- `Pflichtenheft.md` bleibt konsistent: Welle 2 darf erst dann als
  interaktiv reviewtauglich gelten, wenn diese Showcase-Stufe abgeschlossen ist.
- Kein Beispiel verliert den bisherigen 011-Smoke-Nachweis; schwache direkte
  Nachweise werden durch staerkere UI-Pfad-Smokes ersetzt.

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
- `Pflichtenheft.md` remains consistent: wave 2 may be considered
  interactively review-ready only after this showcase stage is complete.
- No example loses its previous 011 smoke proof; weak direct proofs are
  replaced by stronger UI-path smokes.

---

## 7.1 Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artifacts

Der naechste Feature-Lauf soll mindestens diese Artefakte erzeugen oder
aktualisieren:

The next feature run shall create or update at least these artifacts:

- `specs/012-interactive-wave2-demos/spec.md`
- `specs/012-interactive-wave2-demos/plan.md`
- `specs/012-interactive-wave2-demos/research.md`
- `specs/012-interactive-wave2-demos/data-model.md`
- `specs/012-interactive-wave2-demos/quickstart.md`
- `specs/012-interactive-wave2-demos/contracts/interactive-wave2-demo-acceptance.md`
- `specs/012-interactive-wave2-demos/tasks.md`
- `specs/012-interactive-wave2-demos/pr-evidence.md`

Der Plan soll ausdruecklich klaeren, ob gemeinsame Runtime-/Test-Helfer in
`examples/` oder `tests/TuiVision.Examples.SmokeTests/` ausreichen oder ob eine
kleine, wiederverwendbare Controls-Erweiterung noetig ist.

The plan shall explicitly decide whether shared runtime/test helpers in
`examples/` or `tests/TuiVision.Examples.SmokeTests/` are enough, or whether a
small reusable Controls extension is needed.

---

## 8. Eingabehinweis fuer Spec-Kit Specify / Input Hint For Spec-Kit Specify

Empfohlener `/speckit-specify`-Input:

```text
Nutze Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md als Eingabe. Erstelle ein Feature
`012-interactive-wave2-demos` fuer interaktive Wave-2-Demos. Ausgangspunkt ist
`main` nach PR #24: Die elf bereits portierten Wave-2-Beispiele besitzen
funktionale 011-Nachweise und sollen jetzt beim normalen CLI-Start sichtbare,
bedienbare Menue-/Tastatur-/Command-Pfade zeigen. Bestehende
smoke-testbare Nachweismethoden muessen ueber echte UI-Kommandos erreichbar
werden, und die Smoke-Tests sollen diese UI-Pfade deterministisch ueber
Events oder Commands durch die echte App-Schleife pruefen. `Demo` ist der
P1-Vertical-Slice; danach folgen einfache Status-/Textbeispiele,
Listen/Input, scrollbare Dialoge und `DlgDsn`. Scope: `examples/`,
`tests/TuiVision.Examples.SmokeTests/`, betroffene Guides und nur minimale
gemeinsame Controls- oder Test-Helfer, falls noetig. Out of scope: neue
Wave-3-/Wave-4-Funktionalitaet, Runtime-Mauspflicht, breite Framework-Revision,
DocFX-/Pages-Umstellung. Die Spezifikation muss die User Stories, die
Beispiel-Matrix, Akzeptanzkriterien und den A11Y-/bilingualen Guide-Nachweis
aus dieser Datei uebernehmen.
```

Recommended `/speckit-specify` input:

```text
Use Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md as input. Create a feature for
`012-interactive-wave2-demos` for interactive wave-2 demos. The starting point
is `main` after PR #24: the eleven already ported wave-2 examples have
functional 011 proof and shall now show visible, usable menu/key/command paths
during normal CLI startup. Existing smoke-testable proof methods must become
reachable through real UI commands, and smoke tests shall verify these UI paths
deterministically through events or commands through the real application loop.
`Demo` is the P1 vertical slice; after that, update simple status/text
examples, list/input examples, scrollable dialogs, and `DlgDsn`. Scope:
`examples/`, `tests/TuiVision.Examples.SmokeTests/`, affected guides, and only
minimal shared Controls or test helpers where needed. Out of scope: new wave-3
or wave-4 functionality, mandatory runtime mouse support, broad framework
revision, DocFX/Pages restructuring. The specification must carry over the
user stories, example matrix, acceptance criteria, and A11Y/bilingual guide
proof from this file.
```
