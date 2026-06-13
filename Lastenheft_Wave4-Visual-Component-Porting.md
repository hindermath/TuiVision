# Lastenheft: Wave-4 Visual Component Porting

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-05-11
**Betrifft:** `examples/`, `tests/TuiVision.Examples.SmokeTests/`,
`docs/guides/examples/`, `examples/README.md`,
`src/TuiVision.Drivers.Console/`, `src/TuiVision.Compatibility/`,
`src/TuiVision.Controls/`
**Empfohlene Prioritaet:** nach Welle 3 und nach der technischen Terminal-/
Charset-Haertung, vor TP7-Anschlusswellen
**Empfohlener Spec-Kit-Branch:** naechste freie Nummer nach Welle 3, z. B.
`016-wave4-visual-component-porting`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10, Abschnitt 12,
`Lastenheft_04_MouseSupportAndInteraction.md`,
`Lastenheft_05_TerminalCharsetAndEmulation.md`

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist die vorbereitete Eingabe fuer einen Spec-Kit-Feature-Lauf.
Sie beschreibt die Portierung der vierten verpflichtenden Beispielwelle:
Terminal-Emulation, Zeichensaetze und terminalnahe Runtime-Zustaende. Wie bei
Welle 3 soll der visuelle Nachweis nicht spaeter nachgezogen werden, sondern
direkt Teil der Portierung sein.

This file is the prepared input for a Spec-Kit feature run. It describes the
porting of the fourth mandatory example wave: terminal emulation, charsets,
and terminal-oriented runtime states. As with wave 3, the visible proof shall
not be added later; it is part of the port from the start.

- Feature-Ziel: Die Wave-4-Beispiele als startbare, sichtbare und
  plattformbewusste TuiVision-Demos portieren.
- Voraussetzung: Welle 3 ist abgeschlossen; Terminal-/Charset-Vertraege aus
  `Lastenheft_05_TerminalCharsetAndEmulation.md` und relevante Maus-/
  Interaktionsentscheidungen aus `Lastenheft_04_MouseSupportAndInteraction.md`
  sind abgeschlossen oder im Plan als vorgelagerte Tasks enthalten.
- Nichtziel: Keine Editor-/Help-/Stream-Arbeit aus Welle 3, keine TP7-
  Anschlusswellen, keine native Komplett-Emulator-Neuschreibung.
- Abschlussgrenze: Jedes Wave-4-Beispiel zeigt beim normalen CLI-Start einen
  sichtbaren Terminal-/Charset-/Emulationszustand oder einen ehrlichen,
  stabilen Fallback, besitzt Statuszeile/Statusbereich, Beschreibungspfad und
  primaere Smokes fuer den sichtbaren Zustand.

- Feature goal: port the wave-4 examples as runnable, visible, and
  platform-aware TuiVision demos.
- Precondition: wave 3 is complete; terminal/charset contracts from
  `Lastenheft_05_TerminalCharsetAndEmulation.md` and relevant mouse/interaction
  decisions from `Lastenheft_04_MouseSupportAndInteraction.md` are complete or
  included in the plan as prerequisite tasks.
- Non-goal: no editor/help/stream work from wave 3, no TP7 follow-on waves, no
  full native emulator rewrite.
- Completion boundary: Each wave-4 example shows a visible terminal, charset,
  or emulation state during normal CLI startup, or an honest stable fallback;
  each has a status line/status area, a description path, and primary smokes
  for the visible state.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Welle 4 ist technisch riskanter als Welle 1 bis 3, weil sie stark von Host-
Terminal, Zeichensatzabbildung, Escape-Sequenzen und Plattformfaehigkeiten
abhaengt. Genau deshalb darf die Akzeptanz nicht auf einem unscharfen "auf
meinem Terminal sah es gut aus" beruhen.

Wave 4 is technically riskier than waves 1 to 3 because it depends heavily on
the host terminal, charset mapping, escape sequences, and platform
capabilities. That is exactly why acceptance must not rely on a vague "it
looked fine on my terminal" result.

Die Beispiele muessen sichtbare und testbare Zustaende liefern, aber diese
Zustaende sollen moeglichst ueber strukturierte TuiVision-/Driver-Puffer oder
stabile View-Kompositionen nachweisbar sein. Reine Screenshots oder
host-spezifische Zufallsausgaben sind kein belastbarer Hauptbeweis.

The examples must provide visible and testable states, but these states should
be proven through structured TuiVision/driver buffers or stable view
compositions where possible. Screenshots alone or host-specific accidental
output are not reliable primary proof.

---

## 2. Ziel / Goal

Die Wave-4-Beispiele sollen direkt als sichtbare, plattformbewusste und
reproduzierbar getestete Demos portiert werden. Jede Demo muss klar zeigen,
welche Terminal- oder Charset-Faehigkeit unterstuetzt wird, wie ein
Nicht-Unterstuetzt-Fall aussieht und wie der Zustand barrierefrei beschrieben
wird.

The wave-4 examples shall be ported directly as visible, platform-aware, and
reproducibly tested demos. Each demo must clearly show which terminal or
charset capability is supported, what an unsupported case looks like, and how
the state is described accessibly.

Pflicht-Startpfade:

```bash
dotnet run --project examples/Cyrillic
dotnet run --project examples/ETerm
dotnet run --project examples/Fonts
dotnet run --project examples/Terminal
dotnet run --project examples/XTerm
```

Die spaetere Spezifikation darf die finalen C#-Projektordner festlegen. Wenn
andere Namen gewaehlt werden, muessen `examples/README.md`, Guides und Smokes
dies konsistent abbilden.

The later specification may define the final C# project folder names. If other
names are chosen, `examples/README.md`, guides, and smokes must reflect that
consistently.

### 2.1 Drei-Schichten-Modell / Three-Layer Model

Fuer jedes Beispiel gilt dasselbe Drei-Schichten-Modell wie bei Welle 1 bis 3:

1. **Hauptflaeche:** sichtbare Terminal-, Charset- oder Emulationskomposition,
   zum Beispiel Zeichenraster, Font-/Codepage-Vorschau, Terminal-Session-
   Fenster, Escape-Sequenz-Demo oder kontrollierter Fallback. Diese Ebene ist
   der primaere Nachweis.
2. **Statuszeile:** kurzer dynamischer Zustand, zum Beispiel Host-Faehigkeit,
   aktive Codepage, Escape-Sequenz, Cursorposition, Mapping-Status oder
   Fallback-Grund.
3. **Beschreibungspfad:** ein explizit erreichbarer Befehl wie Hilfe,
   Beschreibung oder About, der in kurzen text-first Saetzen erklaert, was
   visuell passiert, welche Host-Grenzen gelten und wie die Demo bedient wird.

Each example follows the same three-layer model as waves 1 to 3:

1. **Main area:** visible terminal, charset, or emulation composition, for
   example a character grid, font/codepage preview, terminal-session window,
   escape-sequence demo, or controlled fallback. This layer is the primary
   proof.
2. **Status line:** short dynamic state, for example host capability, active
   codepage, escape sequence, cursor position, mapping status, or fallback
   reason.
3. **Description path:** an explicitly reachable command such as Help,
   Description, or About that explains in short text-first sentences what is
   happening visually, which host limits apply, and how the demo is operated.

---

## 3. Betroffene Beispiele / Affected Examples

- `cyrillic`
- `eterm`
- `fonts`
- `terminal`
- `xterm`

Die Beispiele koennen je nach historischer Quelle unterschiedlich stark
portiert werden. Konfigurations- oder Resource-only-Quellen muessen trotzdem
als historische Absicht dokumentiert werden.

The examples may require different porting depth depending on the historical
source. Configuration-only or resource-only sources must still be documented
as historical intent.

---

## 4. Beispielmatrix / Example Matrix

| Beispiel | Historische Quellen | Sichtbare Hauptidee | Zielzustand | Primaerer Smoke-Nachweis |
|---|---|---|---|---|
| `cyrillic` | `examples/cyrillic/linuxkoi8/test.cc`, `README`, `trivial.acm`, `setkoi8.sh`, `setlat1.sh`; `examples/cyrillic/x11koi8/test.cc` | Kyrillische Zeichen und KOI8-/Unicode-Abbildung mit Host-Grenzen | Sichtbares Zeichenraster oder Textfenster zeigt kyrillische Beispiele, Mapping-Status und ehrlichen Fallback | Smoke prueft strukturierte Buffer-/View-Zeichen, Mapping-Status und Fallback-Text ohne Host-Screenshot-Abhaengigkeit |
| `eterm` | `examples/eterm/menus.cfg`, `theme.cfg` | Konfigurierbare Terminal-/Menu-/Theme-Praesentation | Demo laedt source-controlled Konfiguration und zeigt sichtbare Menue-/Theme-/Terminal-Session-Ansicht oder dokumentierten Resource-only-Fallback | Smoke prueft geladene Konfiguration, sichtbare Menue-/Theme-Werte und Statuszeile |
| `fonts` | `examples/fonts/test.cc`, `genraw.cc`, `font.016`, `ocr.sft` | Font-/Zeichensatzdarstellung und Raw-Font-Umwandlung | Sichtbares Font- oder Codepage-Raster mit Beispielzeichen; Generatorpfad nur kontrolliert oder als dokumentierter Fallback | Smoke prueft sichtbare Rasterdaten, bekannte Beispielzeichen und kontrollierten Generator-/Fallback-Status |
| `terminal` | `examples/terminal/terminal.cc` | Einfache Terminal-Integration mit Session-/Pufferzustand | Sichtbares Terminal-Session-Fenster oder Terminal-View zeigt Eingabe, Ausgabe, Cursor und Status | App-Loop-Smoke injiziert kontrollierte Eingabe, prueft sichtbare Ausgabe, Cursor-/Statuszustand und Quit-Pfad |
| `xterm` | `examples/xterm/Xterm.res` | XTerm-Ressourcen oder Protokoll-/Resource-Konfiguration | Demo zeigt geladene XTerm-Resource-Werte, unterstuetzte Sequenzen oder klaren Resource-only-Fallback | Smoke prueft Resource-Laden, sichtbare Parameter/Sequenzliste und nicht-unterstuetzte Faehigkeiten |

---

## 5. Funktionale Anforderungen / Functional Requirements

### W4-01: Historische Quellen sind Pflichtreferenz

Vor Spezifikation und Implementierung muessen die relevanten historischen
Dateien unter `tv203s/contrib/tvision/examples/` read-only geprueft werden.
Wenn ein Beispiel nur Konfigurations- oder Resource-Dateien besitzt, muss die
Spezifikation dies ausdruecklich nennen und daraus einen sinnvollen
C#-Demo-Zuschnitt ableiten.

Before specification and implementation, the relevant historical files under
`tv203s/contrib/tvision/examples/` must be reviewed as read-only reference. If
an example only has configuration or resource files, the specification must
state this explicitly and derive a useful C# demo scope from it.

### W4-02: Sichtbarer Zustand ist primaerer Nachweis

Jedes Beispiel muss einen sichtbaren Terminal-, Charset- oder
Emulationszustand zeigen. Reiner Startup, reiner Textstatus, direkte
Hilfsmethoden oder Host-Screenshot allein zaehlen nicht als primaerer
Paritaetsnachweis.

Each example must show a visible terminal, charset, or emulation state. Startup
only, text status only, direct helper methods, or a host screenshot alone do
not count as primary parity proof.

### W4-03: Strukturierte Puffer sind bevorzugter Smoke-Beweis

Primaere Smokes sollen strukturierte TuiVision-/Driver-Puffer, View-Zustaende,
Rollen, Zeichenraster, Cursorpositionen oder geladene Resource-Werte pruefen.
Host-Terminal-Rendering darf als Zusatzbeweis dokumentiert werden, aber nicht
als einziger Hauptbeweis.

Primary smokes should verify structured TuiVision/driver buffers, view states,
roles, character grids, cursor positions, or loaded resource values. Host
terminal rendering may be documented as supplemental proof, but not as the
only main proof.

### W4-04: Plattformgrenzen werden sichtbar gemacht

Multi-Mac, Linux und Windows/WSL muessen bei relevanten Terminal- und Charset-
Faehigkeiten als Review-Kontext beruecksichtigt werden. Wenn eine Faehigkeit
nicht auf jedem Host gleich funktioniert, muss die Demo den Fallback oder die
Nicht-Unterstuetzung klar anzeigen.

Multi-Mac, Linux, and Windows/WSL must be considered as review context for
relevant terminal and charset capabilities. If a capability does not work the
same way on every host, the demo must clearly show the fallback or unsupported
state.

### W4-05: Escape- und Charset-Umfang wird bewusst begrenzt

Die Spezifikation muss definieren, welche Escape-Sequenzen, Cursoraktionen,
Attribute, Codepages, Fontdaten und Resource-Werte unterstuetzt werden. Alles
ausserhalb dieses Umfangs muss als bewusst ausgelassen, Fallback oder
Follow-up dokumentiert werden.

The specification must define which escape sequences, cursor actions,
attributes, codepages, font data, and resource values are supported. Anything
outside that scope must be documented as intentionally omitted, fallback, or
follow-up.

### W4-06: Normale CLI-Starts bleiben aussagekraeftig

`dotnet run --project examples/<Name>` muss ohne Test-Helfer einen ersten
Bildschirm zeigen, der Zweck, sichtbare Hauptflaeche, Status und naechsten
Bedienpfad erkennen laesst. Auch ein Fallback muss sichtbar und verstaendlich
sein.

`dotnet run --project examples/<Name>` must show an initial screen without
test helpers where purpose, visible main area, status, and next operation path
are recognizable. A fallback must also be visible and understandable.

### W4-07: Statuszeile traegt Host- und Mapping-Zustand

Die Statuszeile oder ein gleichwertiger Statusbereich soll kurze dynamische
Rueckmeldungen tragen: Host-Faehigkeit, aktive Codepage, Font-Quelle,
Resource-Key, Escape-Sequenz, Cursorposition, Mapping-Ergebnis oder
Fallback-Grund.

The status line or equivalent status area should carry short dynamic feedback:
host capability, active codepage, font source, resource key, escape sequence,
cursor position, mapping result, or fallback reason.

### W4-08: Beschreibungspfad ist barrierefrei und plattformbewusst

Jedes Beispiel braucht einen erreichbaren Beschreibungspfad mit kurzen
Deutsch-zuerst-/Englisch-danach-Texten auf CEFR-B2-Niveau. Die Beschreibung
erklaert den sichtbaren Zustand, die historische Quelle, Host-Grenzen,
Fallbacks und die A11Y-Eigenschaften der Demo.

Each example needs a reachable description path with short German-first and
English-second text at CEFR-B2 level. The description explains the visible
state, historical source, host limits, fallbacks, and accessibility properties
of the demo.

### W4-09: Keine beliebigen Host-Manipulationen

Die Beispiele duerfen keine dauerhaften Host-Terminal-, Font-, Keyboard- oder
Codepage-Einstellungen veraendern. Historische Shell-Skripte oder Fontdaten
werden als Referenz oder Fixture genutzt, nicht als ungefragte Host-Aktion.

The examples must not permanently change host terminal, font, keyboard, or
codepage settings. Historical shell scripts or font data are used as reference
or fixture, not as unsolicited host actions.

### W4-10: Completion-Evidence bleibt reviewbar

Der Feature-Lauf muss `pr-evidence.md` oder ein gleichwertiges Nachweisartefakt
pflegen. Dort muessen historische Quellen, unterstuetzter Umfang, sichtbare
Zustaende, Plattformannahmen, Fallbacks, Sicherheits-/A11Y-Bewertung und
Validierungsbefehle zusammenkommen.

The feature run must maintain `pr-evidence.md` or an equivalent proof
artifact. It must bring together historical sources, supported scope, visible
states, platform assumptions, fallbacks, security/accessibility assessment,
and validation commands.

---

## 6. User Stories / User Stories

### US1: Terminal-Vertical-Slice

Als Reviewer moechte ich `terminal` starten und eine sichtbare Session mit
Eingabe, Ausgabe, Cursor und Status sehen, damit ich Terminalintegration
praktisch beurteilen kann.

**Akzeptanz:** `terminal` zeigt eine sichtbare Terminal-Session oder
gleichwertige Terminal-View; ein App-Loop-Smoke prueft Eingabe, Ausgabe,
Cursor-/Statuszustand und Quit-Pfad.

As a reviewer, I want to start `terminal` and see a visible session with input,
output, cursor, and status so I can judge terminal integration practically.

**Acceptance:** `terminal` shows a visible terminal session or equivalent
terminal view; an app-loop smoke verifies input, output, cursor/status state,
and quit path.

### US2: Charset- und Font-Demos

Als Lernende moechte ich `cyrillic` und `fonts` starten und ein sichtbares
Zeichen- oder Font-Raster sehen, damit ich erkenne, welche Zeichen direkt,
ersetzt oder nicht unterstuetzt werden.

**Akzeptanz:** Beide Beispiele zeigen sichtbare Raster- oder Textzustaende,
Status zu Mapping/Fallback und Smokes auf strukturierte Zeichen- oder
Bufferdaten.

As a learner, I want to start `cyrillic` and `fonts` and see a visible
character or font grid so I understand which characters are direct, replaced,
or unsupported.

**Acceptance:** Both examples show visible grid or text states, mapping/fallback
status, and smokes on structured character or buffer data.

### US3: Konfigurations- und Resource-only-Beispiele

Als Maintainer moechte ich `eterm` und `xterm` auch dann sinnvoll portieren,
wenn die historischen Quellen hauptsaechlich Konfigurations- oder Resource-
Dateien sind, damit die Beispiele nicht leer oder scheinbar vollstaendig
wirken.

**Akzeptanz:** Die Spezifikation dokumentiert den Resource-only-Befund und
liefert sichtbare Konfigurations-, Sequenz- oder Fallback-Demos mit Smokes.

As a maintainer, I want to port `eterm` and `xterm` meaningfully even when the
historical sources are mainly configuration or resource files so the examples
do not appear empty or falsely complete.

**Acceptance:** The specification documents the resource-only finding and
delivers visible configuration, sequence, or fallback demos with smokes.

### US4: Plattformbewusste Guides

Als Nutzer auf macOS, Linux oder Windows/WSL moechte ich im Guide erkennen,
welche Terminal- oder Charset-Faehigkeiten erwartet werden und welche
Fallbacks normal sind.

**Akzeptanz:** Jeder Guide nennt Startpfad, sichtbare Hauptflaeche,
Statuszeile, Beschreibungspfad, Plattformannahmen, Fallbacks und historische
Quelle.

As a user on macOS, Linux, or Windows/WSL, I want the guide to explain which
terminal or charset capabilities are expected and which fallbacks are normal.

**Acceptance:** Each guide states startup path, visible main area, status line,
description path, platform assumptions, fallbacks, and historical source.

---

## 7. Akzeptanzkriterien / Success Criteria

- Alle fuenf Wave-4-Beispiele existieren als .NET-Beispielprojekte unter
  `examples/` oder mit konsistent dokumentierten Namen.
- Jeder normale CLI-Start zeigt Zweck, sichtbaren Terminal-/Charset-/
  Emulationszustand oder stabilen Fallback, Statuszeile und Beschreibungspfad.
- Primaere Smoke-Tests pruefen strukturierte Puffer, Views, Resource-Werte,
  Zeichenraster, Cursor-/Statuszustaende oder Fallbacks statt nur Startup oder
  Textstatus.
- Host- und Plattformgrenzen sind dokumentiert und in Tests oder Evidence
  sichtbar.
- Kein Beispiel veraendert dauerhaft Host-Terminal-, Font-, Keyboard- oder
  Codepage-Einstellungen.
- Guides, `examples/README.md`, `pr-evidence.md`, Pflichtenheft-Marker und
  `docs/project-statistics.md` werden im selben Feature-Lauf aktualisiert.
- Build, Example-Smokes, voller relevanter Testlauf, Format-Check und
  conditional DocFX/A11Y-Pfad sind als Evidence dokumentiert.

- All five wave-4 examples exist as .NET example projects under `examples/` or
  with consistently documented names.
- Each normal CLI startup shows purpose, visible terminal/charset/emulation
  state or stable fallback, status line, and description path.
- Primary smoke tests verify structured buffers, views, resource values,
  character grids, cursor/status states, or fallbacks instead of only startup
  or text status.
- Host and platform limits are documented and visible in tests or evidence.
- No example permanently changes host terminal, font, keyboard, or codepage
  settings.
- Guides, `examples/README.md`, `pr-evidence.md`, the Pflichtenheft marker,
  and `docs/project-statistics.md` are updated in the same feature run.
- Build, example smokes, full relevant test run, format check, and conditional
  DocFX/A11Y path are documented as evidence.

---

## 7.1 Framework-Usage- und Remediation-Gate / Framework Usage and Remediation Gate

Der spaetere Spec-Kit-Lauf muss pro Wave-4-Beispiel dokumentieren, welche
bestehende TuiVision-Framework-Komponente die sichtbare Terminal-, Charset-,
Font-, Resource- oder Emulationskomposition traegt. Lokale Sonderlogik in
`examples/` ist nur als Beispiel-Komposition erlaubt. Wenn sie Framework-,
Driver-, Buffer-, Mapping- oder Resource-Verhalten ersetzt oder in mehreren
Beispielen nuetzlich waere, muss sie als `SmallFrameworkFix` geschlossen oder
als `FollowUpHardening` dokumentiert werden.

The later Spec-Kit run must document for each wave-4 example which existing
TuiVision framework component carries the visible terminal, charset, font,
resource, or emulation composition. Local special logic in `examples/` is only
allowed as example composition. If it replaces framework, driver, buffer,
mapping, or resource behavior or would be useful for multiple examples, it
must be closed as `SmallFrameworkFix` or recorded as `FollowUpHardening`.

Zulaessige Entscheidungen / Allowed decisions:

- `UseExistingFramework`: vorhandene Framework-Komponente reicht.
- `SmallFrameworkFix`: kleine laufbezogene Framework-Korrektur mit Test.
- `IntentionalDeviation`: bewusste Abweichung mit Guide- oder Evidence-Bezug.
- `FollowUpHardening`: zu gross fuer diesen Lauf, eigenes Hardening-Follow-up.

---

## 8. Nichtziele / Non-Goals

- keine Welle-3-Editor-/Help-/Stream-Arbeit
- keine TP7-Anschlusswellen aus `TVDEMOS/` oder `TVFM/`
- keine native Komplett-Neuentwicklung eines Terminalemulators
- keine Pflicht zur Mausbedienung als einzigem Pfad
- keine dauerhafte Manipulation von Host-Terminal-, Font-, Keyboard- oder
  Codepage-Einstellungen
- keine Bearbeitung historischer Dateien unter `tv203s/`
- keine generierten DocFX-Artefakte im Commit

- no wave-3 editor/help/stream work
- no TP7 follow-on waves from `TVDEMOS/` or `TVFM/`
- no complete native rewrite of a terminal emulator
- no mandatory mouse operation as the only path
- no permanent manipulation of host terminal, font, keyboard, or codepage
  settings
- no edits to historical files under `tv203s/`
- no generated DocFX artifacts in the commit

---

## 9. Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artefacts

Der spaetere Spec-Kit-Lauf soll mindestens erzeugen oder aktualisieren:

- `specs/<NNN-wave4-visual-component-porting>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/wave4-visual-component-acceptance.md`
- `tasks.md`
- `pr-evidence.md`
- Guides unter `docs/guides/examples/`
- `examples/README.md`
- `docs/project-statistics.md`
- `Pflichtenheft.md`

The later Spec-Kit run shall at least create or update:

- `specs/<NNN-wave4-visual-component-porting>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/wave4-visual-component-acceptance.md`
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

Wenn Terminal-/Charset-Verhalten plattformrelevant ist, soll die Evidence
zusaetzlich festhalten, welche Teile lokal auf Multi-Mac geprueft wurden und
welche Linux-/Windows- oder WSL-Nachweise in CI, manueller Evidence oder
Follow-up-Tickets liegen.

If terminal/charset behavior is platform-relevant, the evidence shall also
state which parts were checked locally on the Multi-Mac setup and which Linux,
Windows, or WSL proofs are covered by CI, manual evidence, or follow-up
tickets.

---

## 11. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_Wave4-Visual-Component-Porting.md als verbindliche Eingabe. Erstelle die Feature-Spezifikation fuer einen Wave-4-Visual-Component-Porting-Lauf.

Ziel: Die Wave-4-Beispiele cyrillic, eterm, fonts, terminal und xterm muessen als normale .NET-Beispielprojekte portiert werden und beim normalen CLI-Start sichtbare Terminal-, Charset- oder Emulationszustaende zeigen. Primaere Akzeptanz ist nicht Startup, Textstatus, direkte Hilfsmethode oder Host-Screenshot allein, sondern das Drei-Schichten-Modell aus Hauptflaeche, Statuszeile/Statusbereich und Beschreibungspfad.

Pflicht:
- Historische Quellen unter tv203s/contrib/tvision/examples/ read-only pruefen; bei Resource-only- oder Config-only-Quellen den Befund ausdruecklich dokumentieren.
- Pro Beispiel einen sichtbaren Terminal-/Charset-/Emulationszustand oder einen ehrlichen stabilen Fallback liefern.
- Kurze dynamische Rueckmeldung in Statuszeile oder gleichwertigem Statusbereich erhalten: Host-Faehigkeit, Codepage, Font-Quelle, Resource-Key, Escape-Sequenz, Cursorposition, Mapping-Ergebnis oder Fallback-Grund.
- Beschreibungspfad mit Deutsch zuerst, Englisch danach, CEFR-B2 und text-first A11Y bereitstellen.
- Primaere Smokes auf strukturierte TuiVision-/Driver-Puffer, View-Zustaende, Resource-Werte, Zeichenraster, Cursor-/Statuszustaende oder Fallbacks stuetzen; Host-Screenshots nur als Zusatzbeweis nutzen.
- Keine dauerhaften Host-Terminal-, Font-, Keyboard- oder Codepage-Einstellungen veraendern.
- Plattformgrenzen fuer Multi-Mac, Linux und Windows/WSL in Spec, Plan, Tasks oder Evidence sichtbar machen.
- Framework-Usage-Gate aufnehmen: pro Beispiel bestehende Framework-Komponente, lokale Sonderlogik, Remediation-Entscheidung und Evidence-Pfad dokumentieren.
- Wiederverwendbare Logik nicht dauerhaft als lokale `examples/`-Sonderloesung belassen; bei Wiederholung als Framework-Fix oder Follow-up-Hardening behandeln.
- Guides, examples/README.md, pr-evidence.md, Pflichtenheft.md und docs/project-statistics.md im selben Feature-Lauf aktualisieren.
- Keine Wave-3-Arbeit, keine TP7-Anschlusswellen, keine native Komplett-Emulator-Neuschreibung und keine breite Framework-Revision in diesen Lauf ziehen.
```
