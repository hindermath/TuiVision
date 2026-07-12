# Laufzeitsicht Welle 2 / Runtime View Wave 2

## Normaler Start

```text
Reviewer
  |
  | dotnet run --project examples/Sdlg2
  v
Program.cs
  |
  | creates bounds from console size
  v
Sdlg2App : TApplication
  |
  | initializes menu/status/desktop through TApplication
  v
Managed console driver
  |
  | normal event loop until quit
  v
Clean shutdown
```

## Headless-Smoke-Start

```text
MSTest smoke method
  |
  | new Sdlg2App(DefaultBounds(), headless: true)
  | QueueEvents(command sequence)
  v
Sdlg2App.GetEvent()
  |
  | returns queued command events, then cmQuit
  v
TApplication.Run()
  |
  | dispatches the same command route as normal runtime
  | exits in process, no shell process and no timing dependency
  v
Assertions verify visible state
```

012 note: Every Wave-2 primary smoke follows this queued command model. Startup
plus quit remains useful as a liveness check, but primary assertions verify
state produced by command dispatch inside `Run()`.

## Scrollbarer Dialog: `sdlg2`

```text
Sdlg2 smoke
  |
  | creates Sdlg2App
  | creates horizontal + vertical scroll flow
  v
TScrollGroup
  |
  | SetLimit(30, 20)
  | ScrollTo(7, 6)
  | SetFocus(control outside viewport)
  v
Visible state
  |
  | Delta, scrollbar values, focused label and bounds are asserted
```

## Dynamischer Dialog: `dlgdsn`

```text
DlgDsn smoke
  |
  | creates or loads structured description fixture
  v
DialogDescriptionPersistenceAdapter
  |
  | maps persisted record to Controls model
  v
DialogDescriptionValidator
  |
  | accepts valid description
  | rejects malformed, incomplete, duplicate-control, invalid-navigation cases
  v
DialogDescriptionFactory
  |
  | creates runtime TDialog for the valid description
  v
Visible state and rejection messages
```

## 013 Runtime View Addendum

Deutsch: 013 ergaenzt in jedem Welle-2-Beispiel dasselbe Laufzeitmodell:
sichtbare Hauptkomponente, echte `TStatusLine`-Rueckmeldung und
`Help -> Description`. Die primaeren Smokes injizieren Commands in `app.Run()`
und pruefen konkrete Zustandswerte, View-Baum-Typen sowie BackBuffer-Regionen.
Die neue Hilfsdatei unter `examples/Shared/` bleibt beispielintern und erzeugt
keine neue Runtime-Abhaengigkeit.

English: 013 adds the same runtime model to each Wave 2 example: visible main
component, real `TStatusLine` feedback, and `Help -> Description`. Primary
smokes inject commands into `app.Run()` and verify concrete state values,
view-tree types, and back-buffer regions. The new helper file under
`examples/Shared/` remains example-internal and creates no new runtime
dependency.

## 020 Mouse Ingress Addendum

```text
Interactive SGR terminal or controlled observation
  |
  | complete bounded SGR 1006 report
  v
ConsoleMouseIngress in TuiVision.Drivers.Console
  |
  | syntax, range, capability, phase and monotonic click validation
  | zero or one existing TEvent
  v
TProgram.GetEvent
  |
  | point-to-target-key delegate, no Driver-to-Controls reference
  v
TGroup topmost hit and focus -> existing control command or TWindow title drag
  |
  | visible status, view identity and buffer/cell proof
  v
Keyboard-complete application; shutdown disables SGR and clears transient state
```

Deutsch: Der Driver besitzt Protokoll und Hostzustand, Core bleibt der
kanonische Eventvertrag, und Controls besitzen Fokus sowie den einzigen
Titelzeilen-Drag. Ungültige Rohdaten erreichen den View-Baum nicht. Native
Windows Console, Wheel, Hover, Touch und weitere Drag-Ziele bleiben außerhalb
von 020.

English: Driver owns protocol and host state, Core remains the canonical event
contract, and Controls own focus plus the sole title-row drag. Invalid raw data
never reaches the view tree. Native Windows Console, wheel, hover, touch, and
additional drag targets remain outside 020.

## 021 Terminal and Charset Addendum

```text
Controlled text, C0 action, CSI sequence, profile JSON, or font fixture
  |
  | complete validation within fixed limits
  v
TuiVision.Drivers.Console
  | TerminalSession -> TConsoleBuffer/TConsoleCell
  | TerminalCharsetMapper -> Unicode or KOI8-R
  | BitmapFontFixture -> raw 8x16 metadata
  | TerminalProfile -> requested/effective values and fallback
  v
TTerminalView in TuiVision.Controls
  |
  | existing app loop, keyboard dispatch, view identity, status and cells
  v
Deterministic quit; no host process, PTY, font, codepage, or profile mutation
```

Deutsch: Drivers.Console besitzt Session, Parser, Mapping, Fixture und Profil.
Core bleibt der Cell-/Buffer-Vertrag. Controls projiziert nur den validierten
Zustand und fügt keinen zweiten Terminal- oder Key-Parser hinzu. Physische
Hostbeobachtung bleibt von deterministischem In-Process-Proof getrennt.

English: Drivers.Console owns session, parser, mapping, fixture, and profile.
Core remains the cell/buffer contract. Controls only projects validated state
and adds no second terminal or key parser. Physical host observation remains
separate from deterministic in-process proof.

## 022 Wave-4 Visual Component Addendum

```text
Feature-021 contracts or immutable source-controlled manifest
  |
  | controlled command through app.Run()
  v
Terminal | Cyrillic | Fonts | ETerm | XTerm
  |
  | visible main component + TStatusLine + Help -> Description
  v
State + exact view identity + buffer/cell proof + text-first fallback
  |
  v
Deterministic host classification; physical host evidence remains separate
```

Deutsch: Terminal, Cyrillic und Fonts projizieren bestehende 021-Verträge.
ETerm und XTerm zeigen nur exakte unveränderliche Ressourcenmanifeste und führen
keinen historischen Parser aus. Die verlinkte Präsentationsquelle wird pro
Beispiel-Assembly kompiliert; Querschnittstests teilen deshalb neutrale DTOs und
Delegates statt eine gemeinsame CLR-Typidentität anzunehmen.

English: Terminal, Cyrillic, and Fonts project existing 021 contracts. ETerm
and XTerm show only exact immutable resource manifests and execute no historical
parser. Linked presentation source is compiled into each example assembly, so
cross-project tests share neutral DTOs and delegates instead of assuming one CLR
type identity.
