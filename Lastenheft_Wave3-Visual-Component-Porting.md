# Lastenheft: Wave-3 Visual Component Porting

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-05-11
**Betrifft:** `examples/`, `tests/TuiVision.Examples.SmokeTests/`,
`docs/guides/examples/`, `examples/README.md`,
`src/TuiVision.Controls/`, `src/TuiVision.Serialization/`
**Empfohlene Prioritaet:** nach Wave-1- und Wave-2-Visual-Nachschaerfung,
vor Welle 4
**Empfohlener Spec-Kit-Branch:** naechste freie Nummer nach den Visual-
Nachschaerfungen, z. B. `015-wave3-visual-component-porting`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10, Abschnitt 12,
`Lastenheft_03_EditorHelpAndResourcesHardening.md`

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist die vorbereitete Eingabe fuer einen Spec-Kit-Feature-Lauf.
Sie beschreibt die Portierung der dritten verpflichtenden Beispielwelle:
Editor, Datei, Hilfe, Streams und Ressourcen. Anders als bei Wave 1 und Wave 2
soll die sichtbare Runtime-Ebene nicht spaeter repariert werden. Sie ist von
Beginn an Teil der Akzeptanz.

This file is the prepared input for a Spec-Kit feature run. It describes the
porting of the third mandatory example wave: editor, file, help, streams, and
resources. Unlike wave 1 and wave 2, the visible runtime layer shall not be
repaired later. It is part of acceptance from the start.

- Feature-Ziel: Die Wave-3-Beispiele als startbare, sichtbare und
  smoke-gepruefte TuiVision-Demos portieren.
- Voraussetzung: Die technischen Editor-/Help-/Resource-Vertraege aus
  `Lastenheft_03_EditorHelpAndResourcesHardening.md` sind abgeschlossen oder
  im Plan als vorgelagerte Tasks enthalten.
- Nichtziel: Keine Wave-4-Terminal-/Charset-Arbeit, keine Runtime-Mauspflicht,
  keine TP7-Anschlusswellen aus `TVDEMOS/` oder `TVFM/`.
- Abschlussgrenze: Jedes Wave-3-Beispiel zeigt beim normalen CLI-Start eine
  sichtbare Hauptkomposition oder einen stabilen Runtime-Zustand, besitzt eine
  Statuszeile oder einen gleichwertigen Statusbereich, bietet einen
  Beschreibungspfad und hat primaere Smokes fuer diese sichtbare Ebene.

- Feature goal: port the wave-3 examples as runnable, visible, and
  smoke-tested TuiVision demos.
- Precondition: The technical editor/help/resource contracts from
  `Lastenheft_03_EditorHelpAndResourcesHardening.md` are complete or included
  in the plan as prerequisite tasks.
- Non-goal: No wave-4 terminal/charset work, no mandatory runtime mouse
  support, no TP7 follow-on waves from `TVDEMOS/` or `TVFM/`.
- Completion boundary: Each wave-3 example shows a visible main composition or
  stable runtime state during normal CLI startup, has a status line or
  equivalent status area, offers a description path, and has primary smokes for
  that visible layer.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Welle 3 ist die erste Beispielwelle, die Editor-, Datei-, Hilfe- und
Ressourcenverhalten in echten Anwendungsablaeufen beweisen muss. Die
Framework-Bausteine koennen nicht nur als API existieren. Lernende und
Reviewer muessen sehen, wie ein Editorfenster, ein Hilfefenster, ein
Ressourcen-Lookup oder ein Help-Compiler-Zustand im normalen Programmstart
wirkt.

Wave 3 is the first example wave that must prove editor, file, help, and
resource behavior in real application flows. The framework pieces must not
only exist as APIs. Learners and reviewers must see how an editor window, a
help window, a resource lookup, or a help-compiler state appears during normal
program startup.

Aus den Erfahrungen mit Welle 1 und Welle 2 folgt ein fester Qualitaetsmassstab:
Startup, Stringstatus oder direkte Hilfsmethoden reichen nicht als primaerer
Paritaetsnachweis. Die historischen Beispiele aus `tv203s/contrib/tvision`
zeigen sichtbare Anwendungszustaende. Die C#-Ports duerfen moderner und
kleiner sein, muessen diese sichtbare Absicht aber reviewbar machen.

The wave-1 and wave-2 experience defines a fixed quality bar: startup, string
status, or direct helper methods are not enough as primary parity proof. The
historical examples from `tv203s/contrib/tvision` show visible application
states. The C# ports may be more modern and smaller, but they must make this
visible intent reviewable.

---

## 2. Ziel / Goal

Die Wave-3-Beispiele sollen direkt als lern- und reviewtaugliche Demos
portiert werden. Funktionale Portierung und sichtbare Runtime-Reife gehoeren
in denselben Spec-Kit-Lauf, sofern der Plan nicht ausdruecklich eine engere
Vorstufe begruendet.

The wave-3 examples shall be ported directly as learner-ready and
review-ready demos. Functional porting and visible runtime readiness belong in
the same Spec-Kit run unless the plan explicitly justifies a narrower
preparatory step.

Pflicht-Startpfade:

```bash
dotnet run --project examples/BHelp
dotnet run --project examples/HelpDemo
dotnet run --project examples/I18n
dotnet run --project examples/TvEdit
dotnet run --project examples/TvHc
```

Die spaetere Spezifikation darf die finalen C#-Projektordner festlegen. Wenn
andere Namen gewaehlt werden, muessen `examples/README.md`, Guides und Smokes
dies konsistent abbilden.

The later specification may define the final C# project folder names. If other
names are chosen, `examples/README.md`, guides, and smokes must reflect that
consistently.

### 2.1 Drei-Schichten-Modell / Three-Layer Model

Fuer jedes Beispiel gilt dasselbe Drei-Schichten-Modell wie bei der
Wave-1-/Wave-2-Visual-Nachschaerfung:

1. **Hauptflaeche:** sichtbare Komposition oder stabiler Runtime-Zustand, zum
   Beispiel Editorfenster, Help-Topic-Viewer, Ressourcenliste, Compiler-
   Ergebnisbereich oder i18n-Demo-Dialog. Diese Ebene ist der primaere
   Paritaetsnachweis.
2. **Statuszeile:** kurzer dynamischer Zustand, zum Beispiel Dateiname,
   Modified-State, Help-Kontext, Resource-Key, Compiler-Ergebnis oder
   Sprachvariante. Diese Ebene erklaert, ersetzt aber nicht die Hauptflaeche.
3. **Beschreibungspfad:** ein explizit erreichbarer Befehl wie Hilfe,
   Beschreibung oder About, der in kurzen text-first Saetzen erklaert, was
   visuell passiert und wie die Demo bedient wird.

Each example follows the same three-layer model as the wave-1/wave-2 visual
follow-up:

1. **Main area:** visible composition or stable runtime state, for example an
   editor window, help-topic viewer, resource list, compiler result area, or
   i18n demo dialog. This layer is the primary parity proof.
2. **Status line:** short dynamic state, for example file name, modified
   state, help context, resource key, compiler result, or language variant.
   This layer explains the main area but does not replace it.
3. **Description path:** an explicitly reachable command such as Help,
   Description, or About that explains in short text-first sentences what is
   happening visually and how the demo is operated.

---

## 3. Betroffene Beispiele / Affected Examples

- `bhelp`
- `helpdemo`
- `i18n`
- `tvedit`
- `tvhc`

Die bestehenden technischen Vorbereitungsdokumente bleiben gueltig. Dieses
Lastenheft definiert aber den eigentlichen Beispiel-Portierungsumfang fuer
Welle 3.

The existing technical preparation documents remain valid. This requirements
brief defines the actual wave-3 example-porting scope.

---

## 4. Beispielmatrix / Example Matrix

| Beispiel | Historische Quellen | Sichtbare Hauptidee | Zielzustand | Primaerer Smoke-Nachweis |
|---|---|---|---|---|
| `bhelp` | `examples/bhelp/bhelp.cc`, `bhelp.h`, `thelp.cc` | Grundlegendes Hilfesystem mit sichtbarem Thema, Kontext und Navigation | Normale CLI-App zeigt Help-Viewer oder Help-Fenster mit erstem Topic, Statuszeile nennt Kontext/Topic | App-Loop-Smoke oeffnet Hilfe, prueft sichtbaren Topic-Titel, Kontextwechsel und Fallback fuer unbekannten Kontext |
| `helpdemo` | `examples/helpdemo/helpdemo.cc` | Interaktive Hilfe-Demo mit Menue-/Command-Pfaden und Topics | Sichtbare Demo-Shell mit Help-Menue, Topic-Ansicht und Beschreibungspfad | Smoke nutzt Command/Key-Pfad, prueft sichtbares Topic, Cross-Reference oder Topic-Wechsel |
| `i18n` | `examples/i18n/test.cc`, `test.po`, `es.po`, `README`, `extract.sh` | Mehrsprachige Ressourcen und sichtbarer Sprachwechsel/Fallback | Demo zeigt mindestens zwei Sprachvarianten oder eine Sprachvariante plus Fallback in sichtbarer Komposition | Smoke prueft sichtbaren Text fuer Standardsprache, alternative Sprache und Fallback-/Missing-Key-Status |
| `tvedit` | `examples/tvedit/tvedit.cc` | Vollstaendiger Texteditor mit Datei oeffnen, bearbeiten, speichern und Modified-State | Sichtbares Editorfenster mit Fixture-Datei oder leerem Puffer, Cursor-/Modified-State, sicherem Quit-Pfad | App-Loop-Smoke oeffnet Fixture oder Testtemp-Datei, injiziert Editierereignis, prueft sichtbaren Text, Modified-State und Safe-Close-Entscheidung |
| `tvhc` | `examples/tvhc/tvhc.cc`, `tvhc.h`, `demohelp.txt` | Help-Compiler von Quelltext zu persistierter Hilfe-/Ressourcenstruktur | Sichtbarer Compiler- oder Ergebnisbereich zeigt Eingabe, Ergebnis, Fehler und Resource-Key-Zusammenhang | Smoke kompiliert source-controlled Fixture in Testtemp-Ziel, prueft sichtbaren Erfolg/Fehler und lesbare Help-Resource |

---

## 5. Funktionale Anforderungen / Functional Requirements

### W3-01: Historische Quellen sind Pflichtreferenz

Vor der Spezifikation und vor der Implementierung muessen die relevanten
historischen `.cc`-/`.h`-/Resource-/PO-Dateien unter
`tv203s/contrib/tvision/examples/` read-only geprueft werden. Der Spec-Kit-Lauf
muss dokumentieren, welche historische Absicht uebernommen wird und welche
Abweichungen bewusst sind.

Before specification and implementation, the relevant historical `.cc`, `.h`,
resource, and PO files under `tv203s/contrib/tvision/examples/` must be
reviewed as read-only reference. The Spec-Kit run must document which
historical intent is preserved and which deviations are intentional.

### W3-02: Sichtbare Hauptkomposition ist primaerer Nachweis

Jedes Beispiel muss eine sichtbare Hauptkomposition oder einen stabilen
sichtbaren Runtime-Zustand zeigen. Reiner Startup, reiner Textstatus oder
direkte Testhelfer zaehlen nicht als primaerer Paritaetsnachweis.

Each example must show a visible main composition or stable visible runtime
state. Startup only, text status only, or direct test helpers do not count as
primary parity proof.

### W3-03: Normale CLI-Starts muessen aussagekraeftig sein

`dotnet run --project examples/<Name>` muss ohne Test-Helfer einen ersten
Bildschirm zeigen, auf dem Zweck, sichtbare Hauptflaeche und naechster
Bedienpfad erkennbar sind. Das gilt auch fuer Fehler- oder Fallback-Zustaende.

`dotnet run --project examples/<Name>` must show an initial screen without
test helpers where purpose, visible main area, and next operation path are
recognizable. This also applies to error or fallback states.

### W3-04: Primaere Smokes laufen ueber App-Loop oder echte Dispatch-Pfade

Die primaeren Smoke-Tests muessen `app.Run()` oder den realen Event-/Command-/
Key-Dispatch nutzen. Direkte Hilfsmethoden duerfen Setup oder Zusatzbeweis
sein, aber nicht der Hauptbeweis.

Primary smoke tests must use `app.Run()` or the real event, command, or key
dispatch path. Direct helper methods may support setup or supplemental proof,
but they are not the main proof.

### W3-05: Datei- und Compilerfluesse bleiben kontrolliert

`tvedit` und `tvhc` duerfen keine beliebigen Nutzerdaten lesen oder
ueberschreiben. Akzeptanzpfade muessen source-controlled Fixtures, feste
Repository-Pfade oder Testtemp-Verzeichnisse verwenden. Schreibende Proofs
duerfen nur in Testtemp-Ziele gehen.

`tvedit` and `tvhc` must not read or overwrite arbitrary user data. Acceptance
paths must use source-controlled fixtures, fixed repository paths, or test
temporary directories. Write proofs may only target test temporary locations.

### W3-06: Hilfe- und Resource-Fehler bleiben sichtbar

Unbekannte Help-Kontexte, fehlende Cross-References, ungueltige Resource-Keys,
trunkierte Daten und fehlerhafte Compiler-Eingaben muessen sichtbar und
testbar bleiben. Beispiele duerfen solche Fehler erklaeren, aber nicht
verschlucken.

Unknown help contexts, missing cross-references, invalid resource keys,
truncated data, and invalid compiler input must remain visible and testable.
Examples may explain these failures, but must not hide them.

### W3-07: Statuszeile traegt den kurzen Zustand

Kurze dynamische Rueckmeldungen gehoeren bevorzugt in die Statuszeile oder
einen gleichwertigen Statusbereich: aktueller Dateiname, Modified-State,
Topic-Name, Sprachvariante, Resource-Key, Compiler-Ergebnis oder
Fallback-Grund.

Short dynamic feedback should preferably appear in the status line or an
equivalent status area: current file name, modified state, topic name,
language variant, resource key, compiler result, or fallback reason.

### W3-08: Beschreibungspfad ist barrierefrei und verstaendlich

Jedes Beispiel braucht einen erreichbaren Beschreibungspfad mit kurzen
Deutsch-zuerst-/Englisch-danach-Texten auf CEFR-B2-Niveau. Die Beschreibung
erklaert, was visuell passiert, wie die Demo bedient wird, welche historische
Quelle relevant ist und welche A11Y-Eigenschaften gelten.

Each example needs a reachable description path with short German-first and
English-second text at CEFR-B2 level. The description explains what happens
visually, how the demo is operated, which historical source is relevant, and
which accessibility properties apply.

### W3-09: Guides und README sind Abschlussartefakte

Fuer jedes portierte Beispiel muss ein Guide unter `docs/guides/examples/`
entstehen oder aktualisiert werden. `examples/README.md` muss die neuen
Startpfade, sichtbaren Nachweise und bekannten Fallbacks nennen.

Each ported example must create or update a guide under
`docs/guides/examples/`. `examples/README.md` must list the new startup paths,
visible proofs, and known fallbacks.

### W3-10: Completion-Evidence bleibt reviewbar

Der Feature-Lauf muss `pr-evidence.md` oder ein gleichwertiges Nachweisartefakt
pflegen. Dort muessen historische Quellen, sichtbare Hauptideen,
Testbefehle, bekannte Abweichungen, Sicherheits-/A11Y-Bewertung und
Beispielausgaben zusammenkommen.

The feature run must maintain `pr-evidence.md` or an equivalent proof
artifact. It must bring together historical sources, visible main ideas, test
commands, known deviations, security/accessibility assessment, and example
output snippets.

---

## 6. User Stories / User Stories

### US1: Editor-Vertical-Slice

Als Lernende moechte ich `tvedit` starten und ein sichtbares Editorfenster mit
Pufferinhalt, Cursor-/Modified-State und sicherem Quit-Pfad sehen, damit ich
den Editor nicht nur als API, sondern als Anwendung verstehe.

**Akzeptanz:** `tvedit` zeigt beim normalen Start eine Editor-Hauptflaeche,
Statuszustand und Beschreibungspfad; ein App-Loop-Smoke prueft Editieren,
Modified-State und Safe-Close.

As a learner, I want to start `tvedit` and see a visible editor window with
buffer content, cursor/modified state, and a safe quit path so I understand
the editor as an application, not only as an API.

**Acceptance:** `tvedit` shows an editor main area, status state, and
description path during normal startup; an app-loop smoke verifies editing,
modified state, and safe close.

### US2: Help-Demos

Als Reviewer moechte ich `bhelp` und `helpdemo` ueber normale Menue-,
Command- oder Tastaturpfade bedienen, damit ich Help-Kontext, Topic-Anzeige
und Fallbacks sichtbar pruefen kann.

**Akzeptanz:** Beide Help-Beispiele zeigen sichtbare Topics und pruefen
mindestens einen Kontext-/Topic-Wechsel ueber den realen Dispatch-Pfad.

As a reviewer, I want to operate `bhelp` and `helpdemo` through normal menu,
command, or keyboard paths so I can visibly check help context, topic display,
and fallbacks.

**Acceptance:** Both help examples show visible topics and verify at least one
context/topic transition through the real dispatch path.

### US3: Ressourcen, i18n und Compiler

Als Maintainer moechte ich `i18n` und `tvhc` mit kontrollierten Fixtures
pruefen, damit Resource-Lookup, Sprachfallback und Help-Compiler-Ergebnisse
reproduzierbar bleiben.

**Akzeptanz:** `i18n` zeigt mindestens eine sichtbare Sprach- oder Fallback-
Variante; `tvhc` zeigt einen sichtbaren Compile-Erfolg oder -Fehler und
schreibt nur in kontrollierte Testtemp-Ziele.

As a maintainer, I want to verify `i18n` and `tvhc` with controlled fixtures so
resource lookup, language fallback, and help-compiler results remain
reproducible.

**Acceptance:** `i18n` shows at least one visible language or fallback variant;
`tvhc` shows a visible compile success or failure and writes only to controlled
test temporary targets.

### US4: Lernbare Guides

Als textorientiert arbeitender Nutzer moechte ich jeden neuen Guide in Deutsch
und Englisch lesen koennen, damit ich Zweck, Bedienung, sichtbaren Zustand und
historische Abweichungen ohne rein visuelle Hinweise verstehe.

**Akzeptanz:** Jeder Guide erklaert Startpfad, Bedienpfad, sichtbare
Hauptflaeche, Statuszeile, Beschreibungspfad, historische Quelle und A11Y-
Eigenschaften.

As a text-oriented user, I want to read each new guide in German and English
so I understand purpose, operation, visible state, and historical deviations
without relying only on visual hints.

**Acceptance:** Each guide explains startup path, operation path, visible main
area, status line, description path, historical source, and accessibility
properties.

---

## 7. Akzeptanzkriterien / Success Criteria

- Alle fuenf Wave-3-Beispiele existieren als .NET-Beispielprojekte unter
  `examples/` oder mit konsistent dokumentierten Namen.
- Jeder normale CLI-Start zeigt Zweck, sichtbare Hauptkomposition oder
  stabilen Runtime-Zustand, Statuszeile und Beschreibungspfad.
- Primaere Smoke-Tests pruefen sichtbare Editor-, Help-, Resource-, i18n- oder
  Compiler-Zustaende ueber App-Loop oder reale Dispatch-Pfade.
- Datei- und Compiler-Smokes nutzen nur source-controlled Fixtures, feste
  Repository-Pfade oder Testtemp-Verzeichnisse.
- Guides, `examples/README.md`, `pr-evidence.md`, Pflichtenheft-Marker und
  `docs/project-statistics.md` werden im selben Feature-Lauf aktualisiert.
- Build, Example-Smokes, voller relevanter Testlauf, Format-Check und
  conditional DocFX/A11Y-Pfad sind als Evidence dokumentiert.

- All five wave-3 examples exist as .NET example projects under `examples/` or
  with consistently documented names.
- Each normal CLI startup shows purpose, visible main composition or stable
  runtime state, status line, and description path.
- Primary smoke tests verify visible editor, help, resource, i18n, or compiler
  states through app-loop or real dispatch paths.
- File and compiler smokes use only source-controlled fixtures, fixed
  repository paths, or test temporary directories.
- Guides, `examples/README.md`, `pr-evidence.md`, the Pflichtenheft marker,
  and `docs/project-statistics.md` are updated in the same feature run.
- Build, example smokes, full relevant test run, format check, and conditional
  DocFX/A11Y path are documented as evidence.

---

## 7.1 Framework-Usage- und Remediation-Gate / Framework Usage and Remediation Gate

Der spaetere Spec-Kit-Lauf muss pro Wave-3-Beispiel dokumentieren, welche
bestehende TuiVision-Framework-Komponente die sichtbare Hauptkomposition,
Statuszeile, Bedienpfade, Datei-/Help-/Resource-Flows und Smoke-Beweise
traegt. Lokale Sonderlogik in `examples/` ist nur als Beispiel-Komposition
erlaubt. Wenn sie Framework-Verhalten ersetzt oder in mehreren Beispielen
nuetzlich waere, muss sie als `SmallFrameworkFix` geschlossen oder als
`FollowUpHardening` dokumentiert werden.

The later Spec-Kit run must document for each wave-3 example which existing
TuiVision framework component carries the visible main composition, status
line, operation paths, file/help/resource flows, and smoke proof. Local special
logic in `examples/` is only allowed as example composition. If it replaces
framework behavior or would be useful for multiple examples, it must be closed
as `SmallFrameworkFix` or recorded as `FollowUpHardening`.

Zulaessige Entscheidungen / Allowed decisions:

- `UseExistingFramework`: vorhandene Framework-Komponente reicht.
- `SmallFrameworkFix`: kleine laufbezogene Framework-Korrektur mit Test.
- `IntentionalDeviation`: bewusste Abweichung mit Guide- oder Evidence-Bezug.
- `FollowUpHardening`: zu gross fuer diesen Lauf, eigenes Hardening-Follow-up.

---

## 8. Nichtziele / Non-Goals

- keine Wave-4-Terminal-/Charset- oder Emulationsarbeit
- keine Pflicht zur Mausbedienung
- keine TP7-Anschlusswellen aus `TVDEMOS/` oder `TVFM/`
- keine Bearbeitung historischer Dateien unter `tv203s/`
- kein Lesen oder Ueberschreiben beliebiger Nutzerdaten
- keine generierten DocFX-Artefakte im Commit
- keine breite Framework-Revision ausserhalb der fuer Welle 3 noetigen
  Editor-/Help-/Resource-Vertraege

- no wave-4 terminal/charset or emulation work
- no mandatory mouse operation
- no TP7 follow-on waves from `TVDEMOS/` or `TVFM/`
- no edits to historical files under `tv203s/`
- no reading or overwriting arbitrary user data
- no generated DocFX artifacts in the commit
- no broad framework revision outside the editor/help/resource contracts
  needed for wave 3

---

## 9. Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artefacts

Der spaetere Spec-Kit-Lauf soll mindestens erzeugen oder aktualisieren:

- `specs/<NNN-wave3-visual-component-porting>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/wave3-visual-component-acceptance.md`
- `tasks.md`
- `pr-evidence.md`
- Guides unter `docs/guides/examples/`
- `examples/README.md`
- `docs/project-statistics.md`
- `Pflichtenheft.md`

The later Spec-Kit run shall at least create or update:

- `specs/<NNN-wave3-visual-component-porting>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/wave3-visual-component-acceptance.md`
- `tasks.md`
- `pr-evidence.md`
- guides under `docs/guides/examples/`
- `examples/README.md`
- `docs/project-statistics.md`
- `Pflichtenheft.md`

---

## 10. Validierungspfad / Validation Path

Der Feature-Lauf soll mindestens folgenden Nachweis vorbereiten:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet format --verify-no-changes
```

Wenn XML-Dokumentation, DocFX-Navigation oder API-Dokumentation betroffen ist:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

The feature run shall prepare at least the validation path above. If XML
documentation, DocFX navigation, or API documentation is affected, the DocFX
and web A11Y smoke path is also required.

---

## 11. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_Wave3-Visual-Component-Porting.md als verbindliche Eingabe. Erstelle die Feature-Spezifikation fuer einen Wave-3-Visual-Component-Porting-Lauf.

Ziel: Die Wave-3-Beispiele bhelp, helpdemo, i18n, tvedit und tvhc muessen als normale .NET-Beispielprojekte portiert werden und beim normalen CLI-Start sichtbare historische Demo-Zustaende zeigen. Primaere Akzeptanz ist nicht Startup, Textstatus oder direkte Hilfsmethode, sondern das Drei-Schichten-Modell aus Hauptflaeche, Statuszeile/Statusbereich und Beschreibungspfad.

Pflicht:
- Historische Quellen unter tv203s/contrib/tvision/examples/ read-only pruefen und bewusste Abweichungen dokumentieren.
- Pro Beispiel eine sichtbare Hauptkomposition oder einen stabilen Runtime-Zustand liefern: Editorfenster, Help-Topic-Viewer, i18n-/Resource-Demo, Compiler-Ergebnisbereich oder begruendeter Fallback.
- Kurze dynamische Rueckmeldung in Statuszeile oder gleichwertigem Statusbereich erhalten.
- Beschreibungspfad mit Deutsch zuerst, Englisch danach, CEFR-B2 und text-first A11Y bereitstellen.
- Primaere Smokes ueber app.Run() oder reale Event-/Command-/Key-Dispatch-Pfade ausfuehren; direkte Helfer nur fuer Setup oder Zusatzbeweis nutzen.
- tvedit und tvhc duerfen keine beliebigen Nutzerdaten lesen oder ueberschreiben; Fixtures, feste Repo-Pfade oder Testtemp-Verzeichnisse verwenden.
- Framework-Usage-Gate aufnehmen: pro Beispiel bestehende Framework-Komponente, lokale Sonderlogik, Remediation-Entscheidung und Evidence-Pfad dokumentieren.
- Wiederverwendbare Logik nicht dauerhaft als lokale `examples/`-Sonderloesung belassen; bei Wiederholung als Framework-Fix oder Follow-up-Hardening behandeln.
- Guides, examples/README.md, pr-evidence.md, Pflichtenheft.md und docs/project-statistics.md im selben Feature-Lauf aktualisieren.
- Keine Wave-4-Funktionalitaet, keine Runtime-Mauspflicht, keine TP7-Anschlusswellen und keine breite Framework-Revision in diesen Lauf ziehen.
```
