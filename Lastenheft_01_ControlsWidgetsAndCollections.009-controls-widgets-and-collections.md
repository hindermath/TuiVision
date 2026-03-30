# Lastenheft: Controls-Widgets und Collections fuer Beispielwelle 2

**Dokument-Status:** Entwurf
**Erstellt:** 2026-03-29
**Betrifft:** `src/TuiVision.Controls/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Examples.SmokeTests/`
**Empfohlene Prioritaet:** vor Start der Wave-2-Portierung abarbeiten

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Die bisherige Portierung enthaelt die grobe Dialog-/Control-Schicht, aber mehrere
Wave-2-Beispiele haengen an Widgets und Datenmodellen, die derzeit entweder
fehlen oder nur als schmale API-Huelle vorhanden sind. Das betrifft besonders
Kombinationsfelder, Fortschrittsanzeigen, dynamische Parametertexte,
clipboard-nahe Eingabefluesse sowie die feinere Listen- und History-Integration.

The current port includes the rough dialog/control layer, but several wave-2
examples depend on widgets and data models that are either still missing or
exist only as thin API shells. This especially affects combo boxes, progress
indicators, dynamic parameter text, clipboard-oriented input flows, and the
finer integration between lists and history-aware input.

Ohne diese Nacharbeit droht Welle 2 in mehrere beispielspezifische
Sonderloesungen zu zerfallen. Das wuerde den bereits beobachteten Effekt
verstaerken, dass historische `.cc`-Dateien formal portiert wirken, praktisch
aber noch keine tragfaehige Framework-Flaeche liefern.

Without this follow-up, wave 2 is likely to fragment into example-specific
special cases. That would reinforce the already observed pattern where
historical `.cc` files look formally ported but still do not provide a durable
framework surface.

---

## 2. Betroffene Beispiele / Affected Examples

- `clipboard`
- `dyntxt`
- `inplis`
- `listvi`
- `progba`
- `tcombo`
- `tprogb`

Diese Beispiele teilen mehr Infrastruktur, als die Ordnernamen vermuten
lassen. Ein gemeinsames Vorab-Lastenheft ist deshalb sinnvoller als sieben
isolierte Mini-Portierungen.

These examples share more infrastructure than their folder names suggest. A
shared prerequisite document is therefore more useful than seven isolated
mini-ports.

---

## 3. Ziele / Goals

- Beispiel-geeignete Widgets statt einzelner API-Platzhalter bereitstellen.
- Gemeinsame Datenmodelle und Interaktionsmuster nur einmal im Framework
  implementieren.
- Smoke- und Unit-Tests so schneiden, dass spaetere Beispielports auf stabile
  Basiskomponenten aufsetzen koennen.

- Provide example-ready widgets instead of isolated API placeholders.
- Implement shared data models and interaction patterns once in the framework.
- Shape smoke and unit tests so later example ports can rely on stable base
  components.

---

## 4. Anforderungen / Requirements

### R-01: Listen- und Scroller-Nutzbarkeit

`TListViewer`, `TListBox`, `TScrollBar` und `TScroller` muessen fuer reale
Beispielablaufe belastbar sein. Dazu gehoeren sichtbare Fokus-/Selektion,
stabile Bereichswechsel, gekoppelte Scrollposition, leere Listen, kleine
Bounds und reproduzierbares Verhalten bei Tastatursteuerung.

`TListViewer`, `TListBox`, `TScrollBar`, and `TScroller` must be robust enough
for real example flows, including visible focus/selection, stable range
changes, coupled scroll position, empty lists, small bounds, and reproducible
keyboard behaviour.

### R-02: History- und Clipboard-vertraegliche Eingabeflaechen

`TInputLine`, `THistory`, `TFileInputLine` und `ManagedClipboard` muessen als
zusammenhaengender Interaktionsvertrag beschrieben und umgesetzt werden. Wave-2-
Beispiele duerfen Clipboard- oder History-Flows nicht lokal neu erfinden.

`TInputLine`, `THistory`, `TFileInputLine`, and `ManagedClipboard` must form one
coherent interaction contract. Wave-2 examples must not re-invent clipboard or
history flows locally.

### R-03: Kombinationsfelder als Framework-Baustein

Ein fehlender `TComboBox`-Baustein muss als echter Framework-Typ mit klarer
Kopplung aus Eingabefeld, History-/Drop-down-Mechanik und Listenpraesentation
eingefuehrt werden. `tcombo` darf nicht durch beispielspezifische Hilfsklassen
ersetzt werden.

A missing `TComboBox` building block must be introduced as a real framework
type with a clear contract that combines input, history/drop-down mechanics,
and list presentation. `tcombo` must not be replaced by example-local helper
classes.

### R-04: Fortschritts- und Zustandsanzeigen

Fuer `progba` und `tprogb` muessen wiederverwendbare Fortschritts- und
Abbruchmuster bereitstehen. Dazu gehoeren mindestens ein renderbares
Progress-Control, ein updatefaehiger Wertevertrag und testbare Zustandswechsel
zwischen laufend, abgeschlossen und abgebrochen.

Reusable progress and cancellation patterns must exist for `progba` and
`tprogb`, including at minimum a renderable progress control, an updateable
value contract, and testable state transitions between running, completed, and
canceled.

### R-05: Dynamische Text- und Parameterausgabe

`TParamText` und verwandte statische Anzeigeelemente muessen fuer `dyntxt` und
die spaetere Demo-Nutzung so ausgebaut werden, dass formatierte Laufzeitwerte
kontrolliert dargestellt, aktualisiert und in Bounds abgeschnitten werden
koennen.

`TParamText` and related display elements must be strengthened for `dyntxt` and
later demo usage so formatted runtime values can be displayed, refreshed, and
clipped inside bounds in a controlled way.

### R-06: Beispieluebergreifende Akzeptanzflaeche

Vor dem ersten Wave-2-Beispiel muessen fokussierte Framework-Tests die neuen
Widgets direkt pruefen. Beispiel-Smoke-Tests sollen danach nur noch belegen,
dass die Bausteine korrekt zusammengesetzt werden.

Focused framework tests must validate the new widgets before the first wave-2
example starts. Example smoke tests should then prove only that the building
blocks are composed correctly.

### R-07: Klare Abgrenzung zu anderen Lastenheften

Menue-/Status-/Fenster-Grundverhalten bleibt in
`Lastenheft_ControlsRevision.md`. Standarddialoge und Designer-Flows gehoeren in
ein separates Lastenheft. Editor-, Hilfe- und Terminal-Themen sind ausdruecklich
nicht Teil dieses Dokuments.

Menu/status/window baseline behaviour remains in
`Lastenheft_ControlsRevision.md`. Standard dialogs and designer flows belong in
a separate requirements document. Editor, help, and terminal topics are
explicitly out of scope here.

---

## 5. Nicht im Scope / Out of Scope

- Menueleiste, Statuszeile, Fensterbewegung, Dialog-Validierung
- Editor-, Datei-, Hilfe- und Stream-Subsysteme
- Laufzeit-Maussupport und terminalseitige Mausereignis-Erfassung
- XTerm-, Terminal- oder Zeichensatzemulation
- Beispielspezifische One-off-Widgets im jeweiligen `examples/`-Ordner

- Menu bar, status line, window movement, dialog validation
- Editor, file, help, and stream subsystems
- Runtime mouse support and terminal-side mouse event capture
- XTerm, terminal, or charset emulation
- Example-specific one-off widgets inside individual `examples/` folders

---

## 6. Akzeptanzkriterien / Acceptance Criteria

- Ein dediziertes Widget- oder Collections-Feature kann ohne Beispielcode
  gruene Tests fuer Listen, Combo-/History-Flows, ParamText und Progress-Faelle
  ausfuehren.
- `clipboard`, `inplis`, `listvi`, `tcombo`, `progba` und `tprogb` lassen sich
  danach als vergleichsweise duenne Anwendungsports formulieren.
- Kein Wave-2-Beispiel fuehrt eine zweite, konkurrierende Implementierung fuer
  Combo-, Progress-, Clipboard- oder Listenlogik ein.

- A dedicated widget or collections feature can run green tests for lists,
  combo/history flows, parameter text, and progress scenarios without example
  code.
- `clipboard`, `inplis`, `listvi`, `tcombo`, `progba`, and `tprogb` can then be
  written as comparatively thin application ports.
- No wave-2 example introduces a second competing implementation for combo,
  progress, clipboard, or list logic.
