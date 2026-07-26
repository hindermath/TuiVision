# Lastenheft: Wave-2 Visual Component Remediation

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-05-11
**Betrifft:** `examples/`, `tests/TuiVision.Examples.SmokeTests/`,
`docs/guides/examples/`, `examples/README.md`, `src/TuiVision.Controls/`
**Empfohlene Prioritaet:** vor Welle 3, weil Welle 2 fachlich gemergt ist,
aber fuer Lernende und manuelle Reviews visuell nachgeschaerft werden muss
**Empfohlener Spec-Kit-Branch:** `013-wave2-visual-component-remediation`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10, Abschnitt 12
und `specs/012-interactive-wave2-demos/pr-evidence.md`

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist die vorbereitete Eingabe fuer den naechsten
Spec-Kit-Feature-Lauf. Sie soll direkt mit `/speckit-specify` verwendet
werden. Danach folgen `/speckit-plan`, `/speckit-tasks`, eine erneute
Analyse gegen die Artefakte und erst danach die Implementierung.

This file is the prepared input for the next Spec-Kit feature run. It shall be
used directly with `/speckit-specify`. After that, `/speckit-plan`,
`/speckit-tasks`, another artifact analysis, and only then implementation
follow.

- Feature-Name: `013-wave2-visual-component-remediation`
- Hauptziel: Die elf Wave-2-Beispiele so nacharbeiten, dass sie echte
  sichtbare TuiVision-Controls, Dialoge, Fenster oder View-Gruppen zeigen.
- Ausgangsbefund: `012-interactive-wave2-demos` hat App-Loop-Bedienpfade,
  Menues und text-first Rueckmeldungen geliefert, beweist aber zu oft nur
  Status-Text statt der historischen visuellen Komponenten.
- Nichtziel: Keine Wave-3-/Wave-4-Funktionalitaet, keine breite
  Framework-Revision, keine Runtime-Mauspflicht und keine Ruecknahme der
  bestehenden 011-/012-Merges.
- Abschlussgrenze: Jedes betroffene Beispiel zeigt beim normalen CLI-Start
  mindestens eine historische Hauptidee als reale sichtbare TuiVision-
  Komposition, und der primaere Smoke-Test prueft diese Komposition.

- Feature name: `013-wave2-visual-component-remediation`
- Main goal: Rework the eleven wave-2 examples so they show real visible
  TuiVision controls, dialogs, windows, or view groups.
- Starting finding: `012-interactive-wave2-demos` delivered app-loop operation
  paths, menus, and text-first feedback, but too often proves only status text
  instead of the historical visual components.
- Non-goal: No wave-3/wave-4 functionality, no broad framework revision, no
  mandatory runtime mouse support, and no rollback of the existing 011/012
  merges.
- Completion boundary: Each affected example shows at least one historical
  main idea as a real visible TuiVision composition during normal CLI startup,
  and the primary smoke test verifies that composition.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Wave 2 wurde in zwei Stufen geliefert:

1. `011-port-wave2-examples` portierte die elf Beispiele fachlich und machte
   sie durch deterministische Smoke-Tests reviewbar.
2. `012-interactive-wave2-demos` machte die Beispiele ueber normale
   App-Loop-Kommandos, Menues und sichtbare text-first Rueckmeldungen
   bedienbar.

Wave 2 was delivered in two stages:

1. `011-port-wave2-examples` ported the eleven examples functionally and made
   them reviewable through deterministic smoke tests.
2. `012-interactive-wave2-demos` made the examples operable through normal
   app-loop commands, menus, and visible text-first feedback.

Nach der manuellen Sichtpruefung ist aber klar: Die zweite Stufe ist nicht
visuell stark genug. Viele Beispiele zeigen zwar eine Menueleiste und nach
einem Befehl einen sichtbaren Status-Text, aber nicht die historischen
TuiVision-Controls selbst. Aus Sicht von Lernenden und manuellen Reviewern ist
das zu duenn. Ein Beispiel wie `ListVi` soll eine Liste oder einen
listenartigen Viewer zeigen, nicht nur einen Satz wie `listvi: selected last`.
Ein Beispiel wie `TCombo` soll ein Eingabefeld mit Combo-Auswahl zeigen, nicht
nur `tcombo: selected Gamma`.

After manual review, however, it is clear that the second stage is not visually
strong enough. Many examples show a menu bar and then visible status text after
a command, but not the historical TuiVision controls themselves. For learners
and manual reviewers this is too thin. An example such as `ListVi` should show
a list or list-like viewer, not only a sentence like `listvi: selected last`.
An example such as `TCombo` should show an input field with a combo selection,
not only `tcombo: selected Gamma`.

Die historischen C/C++-Quellen unter `tv203s/contrib/tvision/examples/` zeigen
sichtbare View-Kompositionen: Dialoge mit Buttons, Listen mit Scrollbars,
Combo-Fenster, Progress-Balken, ScrollDialog-/ScrollGroup-Inhalte,
InputLine-/History-Verhalten und dynamische Text-Views. Die C#-Ports duerfen
projektgerecht vereinfacht sein, muessen diese sichtbare Hauptidee aber als
echte UI-Komposition zeigen.

The historical C/C++ sources under `tv203s/contrib/tvision/examples/` show
visible view compositions: dialogs with buttons, lists with scrollbars, combo
windows, progress bars, ScrollDialog/ScrollGroup content, input-line/history
behavior, and dynamic text views. The C# ports may simplify this in a
project-appropriate way, but they must show the visible main idea as a real UI
composition.

---

## 2. Ziel / Goal

Die Wave-2-Beispiele sollen von "bedienbarem Textstatus" zu "sichtbarer
Controls-/Dialog-Demo" gehoben werden. Text-first Rueckmeldung bleibt wichtig
fuer Barrierefreiheit, darf aber nicht mehr der primaere Nachweis sein, wenn
das historische Beispiel eine konkrete visuelle Komponente demonstriert.

The wave-2 examples shall move from "operable text status" to "visible
controls/dialog demo". Text-first feedback remains important for accessibility,
but it must no longer be the primary proof when the historical example
demonstrates a concrete visual component.

Der spaetere Spec-Kit-Lauf soll pro Beispiel beantworten:

- Welche historische visuelle Hauptidee zeigt der C/C++-Code?
- Welche reale TuiVision-Komposition zeigt die C#-App beim normalen Start?
- Welche Smoke-Tests pruefen diese sichtbare Komposition direkt?
- Welche Abweichungen bleiben bewusst und sind dokumentiert?

The later Spec-Kit run shall answer for each example:

- Which historical visual main idea does the C/C++ code show?
- Which real TuiVision composition does the C# app show during normal startup?
- Which smoke tests verify this visible composition directly?
- Which deviations remain intentional and documented?

### 2.1 Drei-Schichten-Modell / Three-Layer Model

Die Remediation soll die bisherige Text-Rueckmeldung nicht entfernen, sondern
neu platzieren. Fuer jedes Beispiel gilt ein klares Drei-Schichten-Modell:

1. **Hauptflaeche:** die echte sichtbare Komponente, zum Beispiel Liste,
   Combo, Progress-Bar, Dialog, Fenster oder ScrollGroup. Diese Flaeche ist der
   primaere Paritaetsnachweis.
2. **Statuszeile:** ein kurzer Zustands- oder Bedienhinweis, der den frueheren
   `TStaticText`-Status aufnimmt, aber die sichtbare Komponente nicht ersetzt.
3. **Beschreibungspfad:** ein explizit erreichbarer Befehl wie Hilfe,
   Beschreibung oder About, der in kurzen text-first Saetzen erklaert, was
   visuell passiert und wie die Demo bedient wird.

The remediation must not remove the existing text feedback. It must place it
more deliberately. Each example follows a clear three-layer model:

1. **Main area:** the real visible component, for example a list, combo,
   progress bar, dialog, window, or ScrollGroup. This area is the primary
   parity proof.
2. **Status line:** a short state or operation hint that carries the former
   `TStaticText` status, but does not replace the visible component.
3. **Description path:** an explicitly reachable command such as Help,
   Description, or About that explains in short text-first sentences what is
   happening visually and how the demo is operated.

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

Die bestehenden Guides, App-Loop-Smokes und direkten Hilfsmethoden aus 011/012
bleiben wertvoll. Sie sind aber nur Ausgangsmaterial. Primaere Akzeptanz in
diesem Remediation-Lauf ist die sichtbare UI-Komposition.

The existing guides, app-loop smokes, and direct helper methods from 011/012
remain valuable. They are only starting material, however. Primary acceptance
in this remediation run is the visible UI composition.

---

## 4. Beispielmatrix / Example Matrix

| Beispiel | Historische Quellen | Sichtbare Hauptidee | Aktuelle Luecke | Zielzustand |
|---|---|---|---|---|
| `Clipboard` | `examples/clipboard/test.cc`, `include/tv/osclipboard.h` | Eingabe-/Textbereich mit Copy, Cut, Paste und sichtbarem unavailable-Fallback | Rueckmeldung ist primaer Textstatus | Sichtbare Eingabe- oder Text-Control zeigt Inhalt vor/nach Copy/Cut/Paste; kurzer Clipboard-Zustand erscheint in der Statuszeile |
| `Demo` | `examples/demo/tvdemo1.cc`, `tvdemo2.cc`, `tvdemo3.cc`, `tvdemo.h`, `tvcmds.h`, `gadgets.cc`, `fileview.cc`, `ascii.cc`, `calendar.cc` | Breite Controls-/Dialog-/Gadget-Demo mit oeffnenden Dialogen oder sichtbaren Fenstern | Commands melden zusammenfassenden Status | Mindestens drei reale sichtbare Demo-Flows: Dialog/Control, Datei-Metadaten oder Pfad, Color/Display oder Gadget; Statuszeile benennt aktuellen Flow |
| `DlgDsn` | `examples/dlgdsn/freedsgn.cc`, `dsgobjs.cc`, `propdlgs.cc`, `propedit.cc`, `strmoper.cc`, `dsgdata.h`, `dsgobjs.h` | Geladene Dialogbeschreibung wird als Dialog/Control-Baum sichtbar | Render-/Reject-Pfade sind zu stark textuell zusammengefasst | Gueltige Beschreibung erzeugt sichtbaren `TDialog` mit echten Controls; fehlerhafte Fixtures zeigen sichtbare Ablehnung plus kurzen Status |
| `DynTxt` | `examples/dyntxt/dyntext.cpp`, `testdyn.cpp`, `dyntext.h` | Dynamische Text-View aktualisiert Inhalt und zeigt Clipping/Justierung | Textstatus ersetzt View-Verhalten | Sichtbare dynamische Text-View aktualisiert nach Eingabe/Befehl; enge Breite wird als gerenderter Zustand geprueft; Statuszeile meldet Variante |
| `InpLis` | `examples/inplis/inplist.cpp`, `test.cpp`, `inplist.h` | ListBox mit InputLine-Editierung und History-/Boundary-Verhalten | Auswahl und History erscheinen primaer als Statuszeilen | Sichtbarer Dialog mit Liste, Eingabezeile und Scrollbar; Statuszeile zeigt Auswahl/History-Kurzstatus; Smoke prueft Auswahl, Commit, Recall und leere Liste |
| `ListVi` | `examples/listvi/lst_view.cpp`, `listbox2.cpp`, `lst_view.h`, `classes/tlistvie.cc` | `TListViewer`-/`TListBox`-Navigation, Auswahl, Bounds und About/Dialog | Interne Liste plus `TStaticText`-Zusammenfassung | Sichtbarer Listenbereich mit Auswahl und optionaler Scrollbar; Statuszeile uebernimmt den kurzen Auswahlstatus; Smoke prueft selektierten sichtbaren Eintrag und Empty-State |
| `ProgBa` | `examples/progba/example.cpp`, `tprogbar.cpp`, `tprogbar.h`, `makerez.cpp`, `readrez.cpp` | Dialog/Fenster mit Fortschrittsbalken bis Completion | Completion wird als Text gemeldet | Sichtbarer `TProgressBar` in Dialog/Fenster; Statuszeile zeigt Prozent/Abschluss; Smoke prueft Fortschrittswert und Abschlussanzeige |
| `Sdlg` | `examples/sdlg/main.cpp`, `scrldlg.cpp`, `scrlgrp.cpp`, `dlg.h` | Vertikaler ScrollDialog/ScrollGroup mit Controls ausserhalb des ersten Viewports | `TScrollGroup` existiert intern, wird aber nicht als sichtbarer Primaerbereich eingesetzt | Sichtbarer ScrollGroup-/Dialogbereich mit Controls; Smoke prueft gerenderten/fokussierten unteren Inhalt |
| `Sdlg2` | `examples/sdlg2/main.cpp`, `scrldlg.cpp`, `scrlgrp.cpp`, `dlg.h` | Zwei-Achsen-ScrollDialog/ScrollGroup mit horizontalen und vertikalen Grenzen | Zwei-Achsen-Zustand ist primaer Textfeedback | Sichtbarer zweiachsiger Scrollbereich; Statuszeile zeigt Offset/Fokus kurz; Smoke prueft horizontale und vertikale Verschiebung sowie fokussierte entfernte Zelle |
| `TCombo` | `examples/tcombo/test.cpp`, `tcombobx.cpp`, `tcombobx.h`, `tcmbovwr.cpp`, `tcmbowin.cpp`, `tsinputl.cpp`, `tsinputl.h` | Eingabefeld plus Combo-Ausloeser, Auswahlfenster und synchronisierte Eingabe | `TComboBox` wird intern erzeugt, aber nicht sichtbar eingefuegt | Sichtbares Eingabefeld mit Combo-/Listenkomponente; Statuszeile meldet Auswahl oder Boundary; Smoke prueft sichtbaren Wert und Boundary-/Empty-State |
| `TProgB` | `examples/tprogb/calc.cpp`, `tprogbar.cpp`, `tprogbar.h` | Fortschrittsdialog mit Start, Abort und Cancelled-State | Partial/Abort/Cancelled erscheinen als Textstatus | Sichtbarer Progress-Dialog oder Fenster mit Balken; Statuszeile zeigt Partial/Abort/Cancelled; Smoke prueft Teilfortschritt, Abort und Cancelled-Anzeige |

---

## 5. Funktionale Anforderungen / Functional Requirements

### VR-01: Sichtbare Komponenten sind der Primaerbeweis

Jedes Beispiel muss mindestens eine reale sichtbare TuiVision-Komponente im
Desktop, in einem Dialog oder in einem Fenster einsetzen, die zur historischen
Hauptidee passt. Ein `TStaticText`-Status darf diese Komponente erklaeren,
aber nicht ersetzen. Kurze bisherige Statussaetze sollen bevorzugt in die
Statuszeile oder einen gleichwertigen Statusbereich wandern.

Each example must use at least one real visible TuiVision component in the
desktop, in a dialog, or in a window that matches the historical main idea. A
`TStaticText` status may explain this component, but it must not replace it.
Short existing status sentences should preferably move into the status line or
an equivalent status area.

### VR-02: Normale CLI-Starts zeigen die Komposition

`dotnet run --project examples/<Name>` muss ohne Test-Helfer einen ersten
Bildschirm zeigen, auf dem der Zweck und die sichtbare Komponente erkennbar
sind. Ein rein leerer Shell-Desktop oder nur ein Ergebnistext reicht nicht.

`dotnet run --project examples/<Name>` must show a first screen without test
helpers where the purpose and the visible component are recognizable. A purely
empty shell desktop or only result text is not enough.

### VR-03: Primaere Smoke-Tests pruefen Controls/Dialoge direkt

Die primaeren Smoke-Tests muessen die sichtbare Komposition pruefen. Erlaubte
Nachweise sind zum Beispiel:

- vorhandene View-/Dialog-/Window-Instanzen mit stabiler Rolle oder Typ
- sichtbarer Listeninhalt und selektierter Eintrag
- Fokusziel oder Scrollposition nach UI-Ereignis
- Progress-Wert und sichtbarer Balken-/Statuszustand
- Eingabewert und Combo-/History-Synchronisierung
- gerenderter Dialogbaum aus einer Dialogbeschreibung

Primary smoke tests must verify the visible composition. Valid proof includes:

- existing view/dialog/window instances with stable role or type
- visible list content and selected item
- focus target or scroll position after a UI event
- progress value and visible bar/status state
- input value and combo/history synchronization
- rendered dialog tree from a dialog description

### VR-04: Kurzstatus gehoert in die Statuszeile

Text-first Feedback bleibt Pflicht fuer Screenreader, Braille-Zeile und
textorientierte Reviews. Der kurze dynamische Zustand soll bevorzugt in der
Statuszeile angezeigt werden, zum Beispiel aktuelle Auswahl, Scroll-Offset,
Progress-Wert, Fehlerkurztext oder naechste Bedienaktion. Er bleibt wichtig,
ist aber nur Zusatznachweis, wenn eine historische visuelle Komponente im
Scope steht.

Text-first feedback remains mandatory for screen readers, Braille displays,
and text-oriented reviews. The short dynamic state should preferably appear in
the status line, for example current selection, scroll offset, progress value,
short error text, or next operation. It remains important, but it is only
supporting evidence when a historical visual component is in scope.

### VR-05: Historische Quellenpruefung bleibt verpflichtend

Jedes Beispiel muss erneut gegen die relevanten read-only C/C++-Quellen unter
`tv203s/` geprueft werden. Die Pruefung muss die historische visuelle
Hauptidee, den C#-Zielzustand und bewusste Abweichungen dokumentieren.

Each example must again be checked against the relevant read-only C/C++
sources under `tv203s/`. The review must document the historical visual main
idea, the C# target state, and intentional deviations.

### VR-06: Kleine Framework-Luecken duerfen geschlossen werden

Wenn ein Beispiel eine kleine fehlende Control-Faehigkeit braucht, darf diese
im engen Scope ergaenzt werden. Das gilt nur, wenn die Faehigkeit direkt fuer
die sichtbare Wave-2-Komposition erforderlich ist. Eine breite
Framework-Revision gehoert nicht in dieses Feature.

If an example needs a small missing control capability, it may be added within
a narrow scope. This applies only when the capability is directly required for
the visible wave-2 composition. A broad framework revision does not belong in
this feature.

Wenn die vorhandene `TStatusLine` keinen dynamischen Beispielstatus anzeigen
kann, darf sie eng fuer diesen Zweck erweitert werden. Diese Erweiterung muss
allgemein genug fuer die elf Beispiele sein, darf aber keine breite
Statusline-Neugestaltung erzwingen.

If the existing `TStatusLine` cannot display dynamic example status, it may be
narrowly extended for that purpose. This extension must be general enough for
the eleven examples, but it must not force a broad status-line redesign.

### VR-07: Guides und Evidence muessen die neue Wahrheit zeigen

Betroffene Guides, `examples/README.md` und das Feature-Evidence-Artefakt
muessen klar sagen, welche sichtbare Komponente der normale Start zeigt und
welcher Smoke-Test sie prueft.

Affected guides, `examples/README.md`, and the feature evidence artifact must
clearly state which visible component normal startup shows and which smoke
test verifies it.

### VR-08: Beschreibungspfad ergaenzt die sichtbare Komponente

Jedes Beispiel muss einen leicht auffindbaren Beschreibungspfad besitzen, zum
Beispiel `Hilfe`, `Beschreibung` oder `About`. Dieser Pfad erklaert kurz:
welche Komponente sichtbar ist, welche historische Idee sie zeigt, welche
Tasten oder Menuepunkte wichtig sind und welche Rueckmeldung in der
Statuszeile erscheint.

Each example must provide an easy-to-find description path, for example
`Help`, `Description`, or `About`. This path briefly explains which component
is visible, which historical idea it demonstrates, which keys or menu items
matter, and which feedback appears in the status line.

---

## 6. User Stories

### US1 - Sichtbare Demo-Komposition je Beispiel

Als Lernende oder manueller Reviewer moechte ich jedes Wave-2-Beispiel starten
und sofort eine echte Controls-/Dialog-Komposition sehen, damit ich das
historische Beispielverhalten ohne Quellcodelekture erkenne.

As a learner or manual reviewer, I want to start each wave-2 example and
immediately see a real controls/dialog composition so I can recognize the
historical example behavior without reading the source code.

**Independent Test:** Jedes Beispiel startet im normalen CLI-Pfad und zeigt
eine sichtbare Hauptkomponente, die im Test als View/Dialog/Window oder stabile
Rolle nachweisbar ist.

### US2 - Bedienpfade veraendern sichtbare Controls

Als Nutzer moechte ich Menue- oder Tastaturbefehle ausloesen und sehen, dass
die sichtbare Komponente selbst ihren Zustand aendert.

As a user, I want to trigger menu or keyboard commands and see that the visible
component itself changes state.

**Independent Test:** Der Smoke-Test injiziert App-Loop-Events und prueft den
veraenderten Control-, Dialog-, Fokus-, Scroll-, Auswahl- oder Progress-Zustand.

### US3 - Nachweise ersetzen text-only Akzeptanz

Als Maintainer moechte ich verhindern, dass ein reiner Statussatz wieder als
Beispielparitaet zaehlt.

As a maintainer, I want to prevent a plain status sentence from counting as
example parity again.

**Independent Test:** Primaere Tests duerfen nicht nur `VisibleText` oder
`VisibleHistory` pruefen. Sie muessen die sichtbare Komposition selbst
validieren.

### US4 - Textzugang ergaenzt die visuelle Komponente

Als Auszubildende oder Screenreader-Nutzer moechte ich trotzdem eine
verstaendliche Textbeschreibung bekommen, die erklaert, was visuell passiert,
ohne die echte Komponente auf der Hauptflaeche zu verdecken.

As an apprentice or screen-reader user, I still want a clear text description
that explains what happens visually without hiding the real component in the
main area.

**Independent Test:** Die App bietet eine Hauptkomponente, eine kurze
Statuszeilen-Rueckmeldung und einen Beschreibungspfad. Guides und Evidence
beschreiben Bedienpfad, sichtbare Komponente, erwartete Rueckmeldung und
A11Y-Pfad in Deutsch zuerst und Englisch danach.

---

## 7. Akzeptanzkriterien / Acceptance Criteria

- Alle elf Wave-2-Beispiele besitzen eine reale sichtbare UI-Komposition, die
  zur historischen Hauptidee passt.
- `TStaticText`-Status wird nicht mehr als primaerer Paritaetsnachweis
  gewertet; kurze bisherige Statussaetze bleiben als Statuszeilen- oder
  Statusbereichs-Rueckmeldung erhalten.
- Jede primaere Example-Smoke-Klasse prueft mindestens einen Control-,
  Dialog-, Fenster-, Fokus-, Scroll-, Auswahl-, Eingabe- oder Progress-Zustand.
- Jede Beispiel-App folgt dem Drei-Schichten-Modell aus Hauptkomponente,
  Statuszeile und Beschreibungspfad.
- App-Loop-Smokes laufen weiterhin ueber `app.Run()` oder die echte
  App-Schleife mit injizierten `TEvent`-, Command- oder Key-Ereignissen.
- Jede bewusste visuelle Abweichung vom historischen C/C++-Beispiel ist in
  Evidence oder Guide dokumentiert.
- Guides bleiben DE-first/EN-second, CEFR-B2-lesbar und text-first
  barrierefrei.
- Der spaetere Feature-Lauf aktualisiert die formalen Proof-Oberflaechen:
  Feature-Evidence, Example-Guides, `examples/README.md`, Projektstatistik und
  bei Bedarf `Pflichtenheft.md`.

- All eleven wave-2 examples have a real visible UI composition that matches
  the historical main idea.
- `TStaticText` status is no longer counted as primary parity proof.
- Short former status sentences remain available as status-line or status-area
  feedback.
- Each primary example smoke class verifies at least one control, dialog,
  window, focus, scroll, selection, input, or progress state.
- Each example app follows the three-layer model of main component, status
  line, and description path.
- App-loop smokes still run through `app.Run()` or the real app loop with
  injected `TEvent`, command, or key events.
- Each intentional visual deviation from the historical C/C++ example is
  documented in evidence or guide material.
- Guides remain German-first/English-second, CEFR-B2-readable, and accessible
  in text-first setups.
- The later feature run updates the formal proof surfaces: feature evidence,
  example guides, `examples/README.md`, project statistics, and `Pflichtenheft.md`
  when needed.

---

## 8. Nicht im Scope / Out of Scope

- Wave-3-Beispiele wie `tvedit`, `bhelp`, `helpdemo`, `tvhc` oder `i18n`
- Wave-4-Terminalemulation, erweiterte Zeichensaetze oder Runtime-Maussupport
- Vollstaendige historische Byte-, Stream- oder Resource-Paritaet fuer
  Wave-2-Beispiele
- Ruecknahme der vorhandenen 011-/012-Merges
- Breite Redesigns von `TApplication`, `TProgram`, Driver-Schicht oder
  Serialization
- Persistente Nutzerdaten, neue externe Services, Datenbanken oder
  Netzwerkabhaengigkeiten

- Wave-3 examples such as `tvedit`, `bhelp`, `helpdemo`, `tvhc`, or `i18n`
- Wave-4 terminal emulation, extended charsets, or runtime mouse support
- Full historical byte, stream, or resource parity for wave-2 examples
- Rollback of the existing 011/012 merges
- Broad redesigns of `TApplication`, `TProgram`, the driver layer, or
  Serialization
- Persistent user data, new external services, databases, or network
  dependencies

---

## 9. Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artifacts

Der spaetere Spec-Kit-Lauf soll mindestens diese Artefakte erzeugen oder
pflegen:

- `specs/013-wave2-visual-component-remediation/spec.md`
- `specs/013-wave2-visual-component-remediation/plan.md`
- `specs/013-wave2-visual-component-remediation/research.md`
- `specs/013-wave2-visual-component-remediation/data-model.md`
- `specs/013-wave2-visual-component-remediation/quickstart.md`
- `specs/013-wave2-visual-component-remediation/contracts/visual-wave2-acceptance.md`
- `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- eine Anforderungen- oder Plan-Qualitaetscheckliste unter
  `specs/013-wave2-visual-component-remediation/checklists/`

The later Spec-Kit run shall create or maintain at least these artifacts:

- `specs/013-wave2-visual-component-remediation/spec.md`
- `specs/013-wave2-visual-component-remediation/plan.md`
- `specs/013-wave2-visual-component-remediation/research.md`
- `specs/013-wave2-visual-component-remediation/data-model.md`
- `specs/013-wave2-visual-component-remediation/quickstart.md`
- `specs/013-wave2-visual-component-remediation/contracts/visual-wave2-acceptance.md`
- `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- a requirements or plan-quality checklist under
  `specs/013-wave2-visual-component-remediation/checklists/`

---

## 10. Validierung / Validation

Fuer die spaetere Umsetzung sind diese Validierungen Pflicht:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet format --verify-no-changes
```

Wenn Guides, `examples/README.md`, `Pflichtenheft.md`, DocFX-Navigation oder
API-Dokumentation betroffen sind:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Wenn das Coverage-Gate im spaeteren Plan als Merge-Evidence benoetigt wird:

```bash
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

The later implementation must run the validation commands above. DocFX and the
web A11Y smoke are required when documentation output or navigation changes.
Coverage evidence is required when the later plan uses it as merge evidence.

---

## 11. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_Wave2-Visual-Component-Remediation.md als Eingabedatei. Erstelle die Feature-Spezifikation fuer `013-wave2-visual-component-remediation`.

Ziel: Die elf Wave-2-Beispiele muessen echte sichtbare TuiVision-Controls, Dialoge, Fenster oder View-Gruppen zeigen. Die bisherige 012-Umsetzung mit App-Loop-Menues und text-first Rueckmeldungen ist Ausgangsbasis, reicht aber nicht mehr als primaerer Paritaetsnachweis.

Wichtig:
- Alle elf Beispiele sind Scope: Clipboard, Demo, DlgDsn, DynTxt, InpLis, ListVi, ProgBa, Sdlg, Sdlg2, TCombo, TProgB.
- Primaere Akzeptanz ist die sichtbare UI-Komposition, nicht `VisibleText` oder `VisibleHistory`.
- Kurze bisherige Statussaetze sollen in die Statuszeile oder einen gleichwertigen Statusbereich wandern.
- Jede App soll ein Drei-Schichten-Modell nutzen: Hauptkomponente, Statuszeile und Beschreibungspfad.
- Smoke-Tests muessen konkrete Controls/Dialoge, Fokus, Auswahl, Scrollposition, Progress oder Dialogzustand pruefen.
- Historische C/C++-Quellen unter `tv203s/` muessen read-only als Intent-Quelle geprueft werden.
- Bewusste Abweichungen muessen in Spec, Plan, Guide oder PR-Evidence dokumentiert werden.
- Keine Wave-3-/Wave-4-Funktionalitaet und keine breite Framework-Revision in diesen Feature-Lauf ziehen.
```

---

## 12. Offene Planungsnotizen / Open Planning Notes

- Die Spezifikation soll ausdruecklich unterscheiden zwischen
  `Text-first accessibility feedback` und `primary visual parity proof`.
- Die Aufgaben spaeter duerfen Beispiele gruppieren, aber die Acceptance muss
  pro Beispiel eindeutig bleiben.
- Wenn fuer stabile Tests neue kleine Test-Seams noetig sind, sollen sie
  benannt und eng begrenzt werden.
- Ein Beispiel darf weiterhin vereinfachen, wenn die historische
  Komponentenidee klar sichtbar und die Abweichung dokumentiert ist.

- The specification shall explicitly distinguish between text-first
  accessibility feedback and primary visual parity proof.
- Later tasks may group examples, but acceptance must remain clear per
  example.
- If stable tests require small new test seams, they shall be named and kept
  narrow.
- An example may still simplify if the historical component idea remains
  clearly visible and the deviation is documented.
