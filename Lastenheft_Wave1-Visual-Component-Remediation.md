# Lastenheft: Wave-1 Visual Component Remediation

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-05-10
**Umbenannt und nachgeschaerft:** 2026-05-11
**Betrifft:** `examples/Desklogo/`, `examples/MsgCls/`,
`examples/Tutorial/`, `examples/Videomode/`,
`tests/TuiVision.Examples.SmokeTests/`, `docs/guides/examples/`,
`examples/README.md`, `src/TuiVision.Controls/`
**Empfohlene Prioritaet:** nach `Lastenheft_Wave1-Functional-Hardening.md`
und vor einer weiteren Beispielwelle
**Empfohlener Spec-Kit-Branch:** naechste freie Nummer nach dem Functional-
Hardening-Lauf, z. B. `014-wave1-visual-component-remediation`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10, Abschnitt 12
und das zweistufige Beispielwellen-Liefermuster

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist die vorbereitete Eingabe fuer einen Spec-Kit-Feature-Lauf. Sie
ersetzt den frueheren Interactive-Wave1-Zuschnitt.
Der neue Name und Inhalt sind bewusst schaerfer: Es geht nicht nur um
Interaktion, sondern um sichtbare historische Beispielkomponenten und stabile
Runtime-Zustaende als primaeren Nachweis.

This file is the prepared input for a Spec-Kit feature run. It replaces the
older Interactive-Wave1 scope. The new name and content are deliberately
stricter: the goal is not only interaction, but visible historical example
components and stable runtime states as the primary proof.

- Feature-Ziel: Wave-1-Beispiele beim normalen CLI-Start als sichtbare
  historische Demo-Kompositionen nachschaerfen.
- Voraussetzung: `Lastenheft_Wave1-Functional-Hardening.md` ist abgeschlossen
  oder liefert eine belastbare historische Funktionsbasis.
- Nichtziel: Keine erneute fachliche Komplettportierung, keine Wave-2-/Wave-3-/
  Wave-4-Arbeit, keine breite Framework-Revision.
- Abschlussgrenze: Jedes Wave-1-Beispiel zeigt eine reale sichtbare
  Hauptkomponente oder einen stabilen sichtbaren Runtime-Zustand, besitzt eine
  Statuszeilen-/Statusbereichs-Rueckmeldung und einen Beschreibungspfad, und
  die primaeren Smokes pruefen diese sichtbare Ebene.

- Feature goal: sharpen the wave-1 examples during normal CLI startup as
  visible historical demo compositions.
- Precondition: `Lastenheft_Wave1-Functional-Hardening.md` is complete or
  provides a reliable historical function baseline.
- Non-goal: No complete functional re-port, no wave-2/wave-3/wave-4 work, no
  broad framework revision.
- Completion boundary: Each wave-1 example shows a real visible main component
  or stable visible runtime state, has status-line/status-area feedback and a
  description path, and the primary smokes verify this visible layer.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Wave 1 wurde in `007-port-wave1-examples` als erste verpflichtende
Beispielwelle geliefert:

- `Desklogo`
- `MsgCls`
- `Tutorial` mit `tvguid01` bis `tvguid16`
- `Videomode`

Wave 1 was delivered in `007-port-wave1-examples` as the first mandatory
example wave:

- `Desklogo`
- `MsgCls`
- `Tutorial` from `tvguid01` through `tvguid16`
- `Videomode`

Die Beispiele sind startbar, smoke-getestet und dokumentiert. Nach den
Erkenntnissen aus Welle 2 reicht das fuer Lernende und manuelle Reviews aber
nicht immer aus. Ein Beispiel soll nicht nur eine Funktion intern beweisen
oder einen Textzustand melden. Es soll seine historische Hauptidee sichtbar
zeigen und bedienbar machen.

The examples are runnable, smoke-tested, and documented. The findings from
wave 2 show that this is not always enough for learners and manual reviews. An
example must not only prove a function internally or report a text state. It
must visibly show and operate its historical main idea.

Diese Datei beschreibt die zweite Wave-1-Stufe nach dem funktionalen
Hardening: sichtbare Komposition, Statuszeile und Beschreibungspfad. Sie nutzt
dieselben Pruefprinzipien wie `Lastenheft_Wave2-Visual-Component-Remediation.md`.

This file describes the second wave-1 stage after functional hardening:
visible composition, status line, and description path. It uses the same proof
principles as `Lastenheft_Wave2-Visual-Component-Remediation.md`.

---

## 2. Ziel / Goal

Alle Wave-1-Beispiele sollen beim normalen Start ueber `dotnet run --project`
sichtbar, tastaturbedienbar und fuer Lernende nachvollziehbar sein. Primaere
Akzeptanz ist nicht ein statischer Text allein, sondern die sichtbare
historische Demo-Idee:

- Logo-/Desktop-Zustand bei `Desklogo`
- Message-Routing-Trigger und sichtbares Ergebnis bei `MsgCls`
- 16 unterscheidbare Tutorial-Schritte mit sichtbarem Lernziel und Ergebnis
- Terminalgroessen-/Video-Mode-Probe oder ehrlicher Fallback bei `Videomode`

All wave-1 examples shall be visible, keyboard-operable, and understandable for
learners when started through `dotnet run --project`. Primary acceptance is
not static text alone, but the visible historical demo idea:

- logo/desktop state in `Desklogo`
- message-routing trigger and visible result in `MsgCls`
- 16 distinguishable tutorial steps with visible learning goal and result
- terminal-size/video-mode probe or honest fallback in `Videomode`

Pflicht-Startpfade:

```bash
dotnet run --project examples/Desklogo
dotnet run --project examples/MsgCls
dotnet run --project examples/Tutorial
dotnet run --project examples/Videomode
```

Fuer `Tutorial` muss zusaetzlich die bestehende Token-Auswahl erhalten bleiben:

```bash
dotnet run --project examples/Tutorial -- tvguid01
dotnet run --project examples/Tutorial -- tvguid16
```

### 2.1 Drei-Schichten-Modell / Three-Layer Model

Wie bei der Wave-2-Remediation gilt fuer jedes Beispiel ein klares
Drei-Schichten-Modell:

1. **Hauptflaeche:** die echte sichtbare Komponente oder der stabile sichtbare
   Runtime-Zustand. Das ist der primaere Paritaetsnachweis.
2. **Statuszeile:** ein kurzer Zustands- oder Bedienhinweis, zum Beispiel
   aktueller Schritt, Routing-Ergebnis, Logo-/Groessenstatus, Fallback oder
   naechste Bedienaktion.
3. **Beschreibungspfad:** ein explizit erreichbarer Befehl wie Hilfe,
   Beschreibung oder About, der in kurzen text-first Saetzen erklaert, was
   visuell passiert und welche historische Idee gezeigt wird.

As in the wave-2 remediation, each example follows a clear three-layer model:

1. **Main area:** the real visible component or stable visible runtime state.
   This is the primary parity proof.
2. **Status line:** a short state or operation hint, for example current step,
   routing result, logo/size state, fallback, or next operation.
3. **Description path:** an explicitly reachable command such as Help,
   Description, or About that explains in short text-first sentences what is
   happening visually and which historical idea is demonstrated.

---

## 3. Scope und historische Quellen / Scope and Historical Sources

| Beispiel | Historische Quellen | Sichtbare Hauptidee | Aktuelle Prueffrage | Zielzustand |
|---|---|---|---|---|
| `Desklogo` | `tv203s/contrib/tvision/examples/desklogo/desklogo.cc`; `set-logo.cc`, `tv_logo.cc` als Asset-/Generator-Kontext | Minimale Desktop-App mit sichtbarem Logo oder begruendetem Logo-Fallback | Wird wirklich ein Logo-/Desktop-Zustand sichtbar und nicht nur Startup bestaetigt? | Sichtbarer Desktop-/Logo-Zustand; Statuszeile nennt Logoquelle/Fallback; Beschreibungspfad erklaert Asset-/Generator-Entscheidung |
| `MsgCls` | `tv203s/contrib/tvision/examples/msgcls/testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h` | Benutzerdefinierte Nachricht, Broadcast/Routing und sichtbares Ergebnis | Wird Message-Routing ueber Runtime-Pfad ausgeloest und sichtbar beobachtet? | Sichtbarer Menue-/Tastaturtrigger; Ergebnis im Fenster, Desktop oder Statusbereich; wiederholter Trigger bleibt stabil |
| `Tutorial` | `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc` bis `tvguid16.cc` | 16 einzelne Lernschritte mit unterscheidbarem Ziel | Zeigt jeder Schritt ein eigenes sichtbares Lernziel statt nur generischen Text? | Jeder Tokenpfad zeigt Schritt, Lernziel, Hauptzustand und Bedienhinweis; Statuszeile nennt Schritt/Navigation; Smoke-Matrix bleibt 16/16 |
| `Videomode` | `tv203s/contrib/tvision/examples/videomode/test.cc` | Terminalgroessen-/Video-Mode-Reaktion mit ehrlichem Fallback | Wird reale Faehigkeit oder Fallback sichtbar und nicht ueberbehauptet? | Sichtbarer Probe-/Moduszustand: supported, fallback, rejected oder unchanged; Statuszeile nennt Ergebnis; Tests bleiben deterministisch |

Build-Hilfsdateien wie `.bmk`, `.mkf`, `.imk`, `.umk`, `.gpr` und `rhide.env`
sind Kontext. Sie muessen nur dann einbezogen werden, wenn sie fuer
Asset-Herkunft, Startparameter oder historische Intent-Abgrenzung wichtig sind.

Build helper files such as `.bmk`, `.mkf`, `.imk`, `.umk`, `.gpr`, and
`rhide.env` are context. They should be considered only when they matter for
asset origin, launch arguments, or historical intent boundaries.

---

## 4. Zu pruefende C#-Artefakte / C# Artifacts to Review

- `examples/Desklogo/`
- `examples/MsgCls/`
- `examples/Tutorial/`
- `examples/Videomode/`
- `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`
- `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`
- `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`
- `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`
- `docs/guides/examples/desklogo.md`
- `docs/guides/examples/msgcls.md`
- `docs/guides/examples/tutorial.md`
- `docs/guides/examples/videomode.md`
- `examples/README.md`
- enge Framework-/Driver-Pfade nur dann, wenn ein Wave-1-Beispiel sie direkt
  fuer sichtbare Komposition, Statuszeile oder deterministische Tests braucht

---

## 5. Funktionale Anforderungen / Functional Requirements

### VR1-01: Sichtbare Runtime-Ebene ist der Primaerbeweis

Jedes Wave-1-Beispiel muss eine sichtbare Hauptflaeche haben, die zur
historischen Hauptidee passt. Ein reiner Statussatz oder ein Startup-Erfolg
reicht nicht als Primaerbeweis.

Each wave-1 example must have a visible main area that matches the historical
main idea. A plain status sentence or startup success is not enough as primary
proof.

### VR1-02: Kurzstatus gehoert in Statuszeile oder Statusbereich

Kurze Text-Rueckmeldungen bleiben erhalten. Sie sollen bevorzugt in die
Statuszeile oder einen gleichwertigen Statusbereich wandern. Dort nennen sie
aktuellen Schritt, Routing-Zustand, Logo-/Fallback-Kontext, Probe-Ergebnis
oder naechste Bedienaktion.

Short text feedback remains available. It should preferably move into the
status line or an equivalent status area. There it names the current step,
routing state, logo/fallback context, probe result, or next operation.

### VR1-03: Beschreibungspfad ergaenzt die visuelle Ebene

Jedes Beispiel muss einen leicht auffindbaren Beschreibungspfad besitzen, zum
Beispiel `Hilfe`, `Beschreibung` oder `About`. Dieser Pfad erklaert, was
sichtbar ist, welche historische Idee gezeigt wird, wie die Demo bedient wird
und welcher Statuszeilen-Text wichtig ist.

Each example must provide an easy-to-find description path, for example
`Help`, `Description`, or `About`. This path explains what is visible, which
historical idea is shown, how the demo is operated, and which status-line text
matters.

### VR1-04: Bedienpfade loesen die sichtbare Ebene aus

Jedes Beispiel muss mindestens einen echten Bedienpfad besitzen:

- Menuebefehl
- Statuszeilenbefehl
- Tastaturbefehl
- sichtbare Command-Oberflaeche

Der Pfad muss dieselbe Funktion ausloesen, die im Functional-Hardening-Lauf
als fachlich relevant bestaetigt wurde.

### VR1-05: Primaere Smokes pruefen sichtbare Zustaende

Primaere Smoke-Tests muessen die sichtbare Ebene pruefen. Erlaubte Nachweise
sind zum Beispiel:

- vorhandener Desktop-/Logo-Zustand
- sichtbares Message-Routing-Ergebnis nach App-Loop-Event
- Tutorial-Schritt, Lernziel, Schrittzustand oder sichtbarer Tokenpfad
- Videomode-Probe mit Ergebniszustand `supported`, `fallback`, `rejected` oder
  `unchanged`
- Statuszeile oder Statusbereich als Zusatznachweis
- Beschreibungspfad als text-first A11Y-Nachweis

Primary smoke tests must verify the visible layer. Valid proof includes:

- existing desktop/logo state
- visible message-routing result after an app-loop event
- tutorial step, learning goal, step state, or visible token path
- video-mode probe with result state `supported`, `fallback`, `rejected`, or
  `unchanged`
- status line or status area as supporting evidence
- description path as text-first accessibility evidence

### VR1-06: Historische Quellenpruefung bleibt verbindlich

Die historische Quellenpruefung aus `Lastenheft_Wave1-Functional-Hardening.md`
bleibt Grundlage. Wenn die visuelle Remediation eine neue sichtbare Abweichung
erzeugt, muss sie gegen die passende historische Quelle dokumentiert werden.

The historical source review from `Lastenheft_Wave1-Functional-Hardening.md`
remains the baseline. If visual remediation creates a new visible deviation,
it must be documented against the matching historical source.

### VR1-07: Kleine Framework-Luecken duerfen geschlossen werden

Wenn ein Beispiel eine kleine fehlende Faehigkeit braucht, darf diese eng
ergaenzt werden. Das gilt nur fuer direkt benoetigte Demo-Komposition,
Statuszeile, Beschreibungspfad oder stabile Smoke-Pruefung. Eine breite
Framework-Revision gehoert nicht in diesen Feature-Lauf.

If an example needs a small missing capability, it may be added narrowly. This
applies only to directly required demo composition, status line, description
path, or stable smoke proof. A broad framework revision does not belong in this
feature run.

### VR1-08: Guides bleiben DE-first/EN-second und text-first

Alle betroffenen Guides muessen Startbefehl, sichtbare Hauptflaeche,
Bedienhandlung, Statuszeilen-Rueckmeldung, Beschreibungspfad, erwartete
Ausgabe, A11Y-Hinweis, historischen Bezug und bekannte Abweichungen
beschreiben. Deutsch kommt zuerst, Englisch danach; die Sprache bleibt
ungefaehr CEFR-B2.

All affected guides must describe startup command, visible main area,
operation, status-line feedback, description path, expected output,
accessibility note, historical reference, and known deviations. German comes
first, then English; language remains roughly CEFR-B2.

---

## 6. Beispielbezogene Ziele / Per-Example Goals

### Desklogo

`Desklogo` bleibt die minimale Desktop-/Logo-Demo, muss aber mehr beweisen als
einen sauberen Start:

- sichtbarer Logo- oder begruendeter Fallback-Zustand
- Statuszeile nennt Logoquelle, Fallback oder Quit-Hinweis
- Beschreibungspfad erklaert `desklogo.cc` und die Asset-/Generator-
  Abgrenzung zu `set-logo.cc` und `tv_logo.cc`
- Smoke-Test prueft Start, sichtbaren Logo-/Desktop-Zustand, Status und Quit

### MsgCls

`MsgCls` muss Message-Routing ueber sichtbare Runtime-Pfade pruefbar machen:

- Menue- oder Tastaturpfad fuer Nachricht ausloesen
- sichtbares Ergebnis im Fenster, Desktop oder Statusbereich
- wiederholter Trigger bleibt stabil und nachvollziehbar
- Beschreibungspfad erklaert `TLineMessage`-/Broadcast-Idee
- Smoke-Test nutzt denselben Eventpfad und prueft sichtbares Ergebnis

### Tutorial

`Tutorial` muss alle 16 Schritte als sichtbare Lernpfade erhalten:

- erster Bildschirm nennt aktuellen Token, Schritt und Lernziel
- jeder `tvguidNN`-Pfad zeigt ein schritt-spezifisches sichtbares Ergebnis
- Statuszeile nennt Schritt, Navigation oder naechste Bedienaktion
- Beschreibungspfad erklaert Zweck des aktuellen Schritts text-first
- Smoke-Matrix prueft alle 16 Schritte weiterhin einzeln

### Videomode

`Videomode` muss reale Terminalfaehigkeit und Fallback ehrlich unterscheiden:

- sichtbarer Bedienpfad fuer Modus-/Groessenprobe
- sichtbarer Zustand `supported`, `fallback`, `rejected` oder `unchanged`
- Statuszeile nennt Ergebnis und ob echte Transition oder Fallback gezeigt wird
- post-transition usability bleibt erhalten
- Smoke-Test ist deterministisch und behauptet keine nicht belegte
  Terminalfaehigkeit

---

## 7. User Stories fuer Spec-Kit / User Stories for Spec-Kit

### US1: Sichtbare Wave-1-Demo-Kompositionen

Als Lernender moechte ich jedes Wave-1-Beispiel normal starten und sofort den
sichtbaren Hauptzustand erkennen, damit ich nicht nur eine leere App-Schale
oder einen generischen Text sehe.

**Akzeptanz:** Alle vier Beispielbereiche zeigen eine sichtbare Hauptflaeche
oder einen stabilen Runtime-Zustand, der zur historischen Hauptidee passt.

### US2: Bedienpfade veraendern sichtbare Zustaende

Als manueller Reviewer moechte ich die Kernfunktion jedes Beispiels ueber
Tastatur, Menue, Statuszeile oder Command-Pfad ausloesen, damit ich nicht auf
interne Testmethoden angewiesen bin.

**Akzeptanz:** Jeder Beispielbereich besitzt mindestens einen sichtbaren
Bedienpfad zur fachlichen Kernfunktion und veraendert danach einen sichtbaren
Zustand.

### US3: App-loop smoke proof

Als Maintainer moechte ich dieselben Bedienpfade per Smoke-Test ueber den
App-Loop pruefen, damit visuelle Runtime-Reife regressionssicher bleibt.

**Akzeptanz:** Primaere Smokes nutzen Event-/Command-/Key-Pfade und pruefen
sichtbare Hauptflaeche, Runtime-Zustand, Statuszeile oder Beschreibungspfad.

### US4: Textzugang ergaenzt die visuelle Ebene

Als sehbehinderter oder textorientiert arbeitender Lernender moechte ich eine
verstaendliche Textbeschreibung bekommen, die erklaert, was visuell passiert,
ohne die Hauptflaeche zu ersetzen.

**Akzeptanz:** Jede App bietet Hauptflaeche, Statuszeile oder Statusbereich und
Beschreibungspfad. Guides und Evidence beschreiben diese drei Ebenen in
Deutsch zuerst und Englisch danach.

---

## 8. Akzeptanzkriterien / Acceptance Criteria

- `dotnet run --project examples/Desklogo` zeigt Logo-/Desktop-Zustand,
  Statuszeilen-/Statusbereichsfeedback, Beschreibungspfad und stabilen Quit.
- `dotnet run --project examples/MsgCls` besitzt einen sichtbaren
  Nachrichtentrigger und sichtbares Routing-Ergebnis.
- `dotnet run --project examples/Tutorial` und die 16 Tokenpfade bleiben
  startbar, unterscheidbar, sichtbar beschrieben und smoke-geprueft.
- `dotnet run --project examples/Videomode` zeigt reale Transition oder
  sichtbaren Fallback ohne falsche Faehigkeitsbehauptung.
- Jede Beispiel-App folgt dem Drei-Schichten-Modell aus Hauptflaeche,
  Statuszeile/Statusbereich und Beschreibungspfad.
- Primaere Wave-1-Smokes laufen ueber sichtbare Runtime-/Eventpfade.
- Primaere Smokes pruefen nicht nur Startup, `VisibleText` oder direkte
  Hilfsmethoden, sondern sichtbare Zustaende.
- Guides und `examples/README.md` sind auf den tatsaechlichen Bedienpfad
  aktualisiert.
- A11Y-/Text-first-Anforderungen bleiben erfuellt.
- Historische Abweichungen, die durch die visuelle Remediation neu sichtbar
  werden, sind dokumentiert.

---

## 9. Nichtziele / Non-Goals

- keine erneute funktionale Komplettportierung der Wave-1-Beispiele
- keine Wave-2-/Wave-3-/Wave-4-Arbeit
- keine Pflicht zur Mausbedienung
- keine breite Framework-Revision
- keine Bearbeitung historischer Dateien unter `tv203s/`
- keine generierten DocFX-Artefakte im Commit

---

## 10. Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artifacts

Der spaetere Spec-Kit-Lauf soll mindestens erzeugen oder aktualisieren:

- `specs/<NNN-wave1-visual-component-remediation>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/wave1-visual-component-acceptance.md`
- `tasks.md`
- `pr-evidence.md`

`tasks.md` soll die Reihenfolge ausdruecklich so schneiden:

1. Functional-Hardening-Evidence pruefen.
2. Einen kleinen Vertical Slice waehlen, vorzugsweise `MsgCls` oder
   `Desklogo`.
3. Hauptflaeche, Statuszeile und Beschreibungspfad fuer den Slice liefern.
4. App-loop-Smoke fuer den Slice liefern.
5. `Tutorial` und `Videomode` mit demselben Muster nachziehen.
6. Guides, README, A11Y, Statistik, Pflichtenheft und PR-Evidence
   aktualisieren.

---

## 11. Validierung / Validation

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

The later implementation must run the validation commands above. DocFX and the
web A11Y smoke are required when documentation output or navigation changes.

---

## 12. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_Wave1-Visual-Component-Remediation.md als verbindliche Eingabe. Erstelle die Feature-Spezifikation fuer einen Wave-1-Visual-Component-Remediation-Lauf.

Ziel: Die Wave-1-Beispiele Desklogo, MsgCls, Tutorial und Videomode muessen beim normalen CLI-Start sichtbare historische Demo-Zustaende zeigen. Das fruehere Interactive-Wave1-Ziel wird dadurch ersetzt und geschaerft: Primaere Akzeptanz ist nicht generischer Text oder Startup allein, sondern das Drei-Schichten-Modell aus Hauptflaeche, Statuszeile/Statusbereich und Beschreibungspfad.

Scope:
- `dotnet run --project examples/Desklogo`
- `dotnet run --project examples/MsgCls`
- `dotnet run --project examples/Tutorial`
- `dotnet run --project examples/Videomode`
- `dotnet run --project examples/Tutorial -- tvguid01` bis `tvguid16`

Wichtig:
- Functional-Hardening aus `Lastenheft_Wave1-Functional-Hardening.md` bleibt Voraussetzung oder Eingangspruefung.
- Historische C/C++-Quellen unter `tv203s/contrib/tvision/examples/` bleiben read-only Intent-Quelle.
- Primaere Smokes muessen sichtbare Runtime-Zustaende pruefen, nicht nur direkte Hilfsmethoden oder Startup.
- Kurze bisherige Statussaetze sollen in Statuszeile oder Statusbereich wandern.
- Jede App braucht einen Beschreibungspfad wie Hilfe, Beschreibung oder About.
- Bewusste historische Abweichungen muessen in Spec, Plan, Guide oder PR-Evidence dokumentiert werden.
- Keine Wave-2-/Wave-3-/Wave-4-Funktionalitaet und keine breite Framework-Revision in diesen Lauf ziehen.
```
