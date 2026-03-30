# Lastenheft: Maussupport und Interaktions-Haertung zwischen Beispielwelle 3 und 4

**Dokument-Status:** Entwurf
**Erstellt:** 2026-03-30
**Betrifft:** `src/TuiVision.Core/`, `src/TuiVision.Controls/`, `src/TuiVision.Drivers.Console/`, `src/TuiVision.Compatibility/`, `tests/TuiVision.Core.Tests/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Drivers.Tests/`, `tests/TuiVision.Examples.SmokeTests/`
**Empfohlene Prioritaet:** nach stabiler Wave-3-Basis und vor Wave-4-Terminal-/Emulationsarbeiten abarbeiten

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Die Portierung besitzt bereits ein UI-seitiges Mausmodell ueber `TEvent`,
`TMouseEvent`, `TView.MouseInView()` und mousefaehige Controls. Was bisher
fehlt, ist der belastbare Laufzeitpfad vom realen Host-Terminal oder
Konsolen-Backend in diese Framework-Abstraktion hinein.

The port already contains a UI-facing mouse model through `TEvent`,
`TMouseEvent`, `TView.MouseInView()`, and mouse-aware controls. What is still
missing is a durable runtime path that carries input from the real host
terminal or console backend into that framework abstraction.

Die aktuelle Projektlage spricht gegen ein sofortiges Vorziehen in Wave 2:
Die Controls-Revision hat terminalseitigen Maussupport explizit aus dem Scope
gehalten, bis ein unterstuetzter Eingabepfad existiert. Gleichzeitig waere es
zu spaet, das Thema erst mitten in Wave 4 oder erst bei TP7-Mausdemos zu
beginnen. Deshalb braucht die Reihe einen eigenen Vorbereitungsblock zwischen
Wave 3 und Wave 4.

The current project state argues against pulling the topic into wave 2 right
now: the controls revision explicitly kept terminal-side mouse support out of
scope until a supported input path exists. At the same time, it would be too
late to start this work only in the middle of wave 4 or when the TP7 mouse
demos arrive. The series therefore needs a dedicated preparation block between
wave 3 and wave 4.

---

## 2. Betroffene Beispiele / Affected Examples

- `demo`
- `dlgdsn`
- `sdlg`
- `sdlg2`
- `bhelp`
- `helpdemo`
- `tvedit`
- `terminal`
- `eterm`
- `xterm`
- spaeter auch `MOUSEDLG.PAS` aus `TVDEMOS/`

Diese Beispiele beruehren unterschiedliche Ebenen, aber dieselbe Luecke:
Clicks, Doppelklicks, Fokuswechsel, Selektion, Scrollen und Fallback bei
nicht verfuegbarem Host-Support muessen frameworkweit einheitlich behandelt
werden.

These examples touch different layers but expose the same gap: clicks,
double-clicks, focus changes, selection, scrolling, and fallback when host
support is unavailable must be handled consistently across the framework.

---

## 3. Ziele / Goals

- Den bereits vorhandenen UI-Mausvertrag an einen realen, kontrollierten
  Laufzeit-Eingabepfad anbinden.
- Plattform- und Terminalunterschiede explizit machen, statt still zu hoffen,
  dass dieselben Sequenzen ueberall funktionieren.
- Mausinteraktionen als Framework-Faehigkeit pruefbar machen, bevor Beispiele
  oder spaetere TP7-Demos lokale Sonderloesungen einfuehren.

- Connect the already existing UI mouse contract to a real, controlled runtime
  input path.
- Make platform and terminal differences explicit instead of silently hoping
  that the same sequences work everywhere.
- Make mouse interaction a testable framework capability before examples or
  later TP7 demos introduce local special cases.

---

## 4. Anforderungen / Requirements

### R-01: Der Runtime-Maussupport braucht einen expliziten Ingress-Vertrag

Das Projekt muss definieren, ueber welche Schicht reale Mausereignisse in
`TuiVision` eintreten. Diese Schicht darf kein verstreutes Parsing von
Escape-Sequenzen in Beispielcode sein, sondern muss als benannter Treiber-,
Adapter- oder Capability-Vertrag im Framework sichtbar werden.

The project must define through which layer real mouse input enters
`TuiVision`. This layer must not be scattered escape-sequence parsing inside
example code; it has to become a named driver, adapter, or capability contract
in the framework.

### R-02: Das bestehende UI-Mausmodell bleibt der kanonische Zielvertrag

Neue Laufzeitlogik darf keine zweite konkurrierende Mausabstraktion erfinden.
Reale Eingaben muessen auf das vorhandene Ereignismodell mit Position,
Buttons, Doppelklickstatus und View-bezogener Weiterleitung abgebildet werden.

New runtime logic must not invent a second competing mouse abstraction. Real
input has to map onto the existing event model with position, button state,
double-click status, and view-oriented dispatch.

### R-03: Unterstuetzte Hosts und Aktivierungsbedingungen muessen benannt sein

Es muss reviewbar feststehen, auf welchen Hosts und unter welchen Bedingungen
Maussupport offiziell als "unterstuetzt" gilt. Dazu gehoeren mindestens
Multi-Mac, Linux und Windows/WSL sowie die Frage, ob ein kompatibler
Terminal-Emulator, bestimmte Escape-Protokolle oder aktive Opt-in-Flags
erforderlich sind.

The project must define reviewably on which hosts and under which conditions
mouse support officially counts as "supported". This includes at minimum
Multi-Mac, Linux, and Windows/WSL as well as whether a compatible terminal
emulator, specific escape protocols, or active opt-in flags are required.

### R-04: Interaktionssemantik fuer Klick, Doppelklick und Drag muss begrenzt werden

Der erste Maussupport-Inkrement darf nicht unendlich offen bleiben. Mindestens
ein bewusst geschnittener Umfang aus Click-to-focus, Click-to-activate,
Doppelklick-Bestaetigung und gegebenenfalls einfachem Drag fuer Fenster oder
Scrollbereiche ist festzulegen. Hover-, Wheel- oder Mehrfachprotokoll-Support
duerfen nur mit ausdruecklicher Begruendung in denselben Umfang gezogen werden.

The first mouse-support increment must not remain infinitely open-ended. At
minimum, it needs a consciously scoped subset such as click-to-focus,
click-to-activate, double-click confirmation, and, where justified, simple
drag support for windows or scrollable areas. Hover, wheel, or multi-protocol
support may only enter the same increment with explicit justification.

### R-05: Nicht-Unterstuetzung und Deaktivierung muessen sichtbare Pfade bleiben

Wenn ein Host keine tragfaehige Mausereignisquelle bietet oder wenn Maussupport
bewusst deaktiviert ist, muss das Framework reproduzierbar auf reine
Tastatursteuerung zurueckfallen. Dieser Zustand darf nicht als stiller
Teildefekt erscheinen.

If a host does not provide a reliable mouse-event source or if mouse support is
deliberately disabled, the framework must reproducibly fall back to pure
keyboard control. That state must not appear as a silent partial defect.

### R-06: Validierung muss Framework-, Integrations- und Host-Ebene umfassen

Die Akzeptanz darf weder nur aus Unit-Tests noch nur aus manuellen Vorfuehrungen
bestehen. Benoetigt werden fokussierte Core-/Controls-/Driver-Tests, mindestens
ein Integrationspfad mit Event-Loop und Fokuswechsel sowie reviewbare
Host-Nachweise fuer Linux und Windows/WSL neben den beiden Macs.

Acceptance must rely on neither unit tests alone nor manual demos alone. The
project needs focused core, controls, and driver tests, at least one
integration path with event loop and focus changes, and reviewable host
evidence for Linux and Windows/WSL in addition to the two Macs.

### R-07: Abgrenzung zu Terminalemulation und TP7-Mausdemos bleibt erhalten

Dieses Lastenheft fuehrt keinen Vollausbau von XTerm- oder sonstigen
Emulationsprotokollen ein und ersetzt keine spaeteren TP7-Mausbeispiele. Es
schafft nur die stabile Framework-Vorstufe, auf der `terminal`, `xterm` und
spaeter `MOUSEDLG.PAS` sauber aufsetzen koennen.

This requirements document does not introduce full XTerm or other emulation
protocol coverage and does not replace the later TP7 mouse demos. It only
creates the stable framework precursor on which `terminal`, `xterm`, and later
`MOUSEDLG.PAS` can build cleanly.

---

## 5. Nicht im Scope / Out of Scope

- Vollstaendige XTerm- oder plattformspezifische Raw-Mausprotokoll-Paritaet
- Beliebige Hover-, Wheel-, Touch- oder Mehrfinger-Erweiterungen ohne
  unmittelbaren Beispielbedarf
- Editor-, Hilfe-, Ressourcen- oder Zeichensatz-Haertung, soweit sie nicht
  direkt von der Maus-Eingabeschicht abhaengt
- Beispielspezifische Mouse-Helper in einzelnen `examples/`-Ordnern als Ersatz
  fuer Framework-Logik

- Full XTerm or platform-specific raw mouse protocol parity
- Arbitrary hover, wheel, touch, or multi-pointer extensions without immediate
  example demand
- Editor, help, resource, or charset hardening unless it directly depends on
  the mouse-input layer
- Example-specific mouse helpers inside individual `examples/` folders as a
  substitute for framework logic

---

## 6. Akzeptanzkriterien / Acceptance Criteria

- Vor dem ersten Wave-4-Beispiel existiert ein dokumentierter, getesteter
  Maus-Eingabepfad mit expliziter Aussage zu unterstuetzten Hosts und
  Fallback-Verhalten.
- `demo`, `sdlg`, `helpdemo`, `tvedit`, `terminal` oder `xterm` muessen
  Mausklicks nicht lokal parsen oder lokal in Commands uebersetzen.
- Nicht unterstuetzte Umgebungen bleiben benutzbar und reviewbar
  tastaturzentriert, statt halbaktivierte Mauszustande zu zeigen.

- Before the first wave-4 example starts, a documented and tested mouse input
  path exists with an explicit statement about supported hosts and fallback
  behaviour.
- `demo`, `sdlg`, `helpdemo`, `tvedit`, `terminal`, or `xterm` must not parse
  mouse clicks locally or translate them into commands through local glue code.
- Unsupported environments remain usable and reviewable in keyboard-centric
  mode instead of exposing half-activated mouse states.
