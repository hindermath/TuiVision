# Lastenheft 17: Wave-5 TP7 Functional Porting and Proof

**Dokumentstatus:** Verbindliche Spec-Kit-Eingabedatei fuer Feature 032
**Liefermodus:** `MergeAndSync`
**Verbindlicher Branch:** `032-wave5-tp7-functional-porting`
**Verbindliche Reihenfolge:** nach dem gemergten Feature
`031-combined-conformance-closure`, vor der Wave-5-Showcase-Stufe und vor
Wave 6
**Historische Primaerquelle:** `TVDEMOS/` aus Turbo Pascal 7, read-only
**Framework-Referenz:** moderne TuiVision-C#-Basis nach Features 025, 026 und
031
**Zusaetzliche Meinungen:** gepinnte Free-Vision-, Terminal.GUI-v1.9.0- und
`magiblot/tvision`-Evidence aus Features 024, 029 und 030, nicht normativ

*This is the binding Spec Kit intake for Feature 032. It runs after the merged
Feature 031 closure, before the Wave-5 showcase stage, and before Wave 6.
`TVDEMOS/` is the read-only historical authority. The existing modern
TuiVision C# framework remains the implementation base. Free Vision,
Terminal.GUI v1.9.0, and `magiblot/tvision` are additional implementation
opinions through already accepted pinned evidence, not new normative sources.*

---

## 0. Intake-Zusammenfassung / Intake Summary

Feature 032 liefert die **erste von zwei Wave-5-Stufen**. Es portiert die
fachlichen TP7-Demoabsichten aus `TVDEMOS/` in moderne, idiomatische
C#-Beispiele und beweist ihre Kernablaeufe ueber reale TuiVision-Anwendungs-,
Event-, Command-, View- und Renderpfade.

Der Lauf ist keine mechanische Pascal-Uebersetzung. Er ordnet alle 15
Pascal-Quellen genau einer nachvollziehbaren Rolle zu, verwendet die
bestehenden Framework-Vertraege und vermeidet lokale Ersatzframeworks.

Die spaetere zweite Stufe macht jede gelieferte Anwendung vollstaendig
showcase- und reviewreif. Ihr Lastenheft entsteht erst aus der tatsaechlichen
Feature-032-Evidence. Die erwartete naechste freie Nummer ist Feature 033; in
diesem Vorbereitungsschritt wird jedoch weder ein Feature-033-Verzeichnis
noch eine Spezifikation angelegt.

*Feature 032 is the first of two Wave-5 stages. It ports the functional intent
of the TP7 demos into modern idiomatic C# examples and proves their core flows
through real TuiVision application, event, command, view, and rendering paths.
It is not a mechanical Pascal translation. Every Pascal source receives one
traceable role and the implementation uses the existing framework instead of
creating local substitutes. A later showcase stage will be derived from the
actual Feature-032 evidence; no Feature-033 directory or specification is
created now.*

### 0.1 Abschlussgrenze von Feature 032 / Feature 032 Completion Boundary

Feature 032 ist abgeschlossen, wenn:

1. alle 15 Pascal-Quellen inventarisiert und genau einer primaeren Rolle
   zugeordnet sind;
2. alle sechs akzeptierten Wave-5-Consumer-Gruppen `W5-001` bis `W5-006`
   funktional geliefert oder mit einer zulaessigen, vollstaendigen
   Abweichungsentscheidung dokumentiert sind;
3. jedes gelieferte Beispiel baut, normal startet und mindestens einen
   historischen Kernzweck ueber einen echten App-Loop-/Dispatch-Pfad zeigt;
4. fuer jeden Consumer-Slice konkreter Zustand, relevante View-Identitaet und
   sichtbarer Buffer-/Cell-Proof vorliegen;
5. alle Datei-, Resource-, Generator- und Mausgrenzen kontrolliert,
   tastaturvollstaendig und reproduzierbar bleiben;
6. die tatsaechlichen verbleibenden Visual-, Interaktions-, Layout- und
   Lernpfad-Deltas fuer die zweite Wave-5-Stufe in Evidence feststehen.

*Feature 032 completes when all 15 Pascal sources have one primary role, all
six accepted Wave-5 consumer groups have a delivered or explicitly justified
outcome, every delivered example builds and starts normally, real app-loop
proof combines state, view identity, and visible buffer or cell evidence, all
file/resource/generator/mouse boundaries remain controlled and
keyboard-complete, and the real remaining showcase delta is recorded for the
second stage.*

---

## 1. Ausgangslage / Background

Feature 031 hat 48 Framework-Vertraege, 13 Consumer-Gruppen und 96
Terminal.GUI-/magiblot-Beobachtungen unabhaengig geschlossen. Fuer Wave 5
gelten sechs Consumer-Gruppen mit der Ausgangsentscheidung
`UseExistingFramework`. Es existiert kein offenes kanonisches Finding und kein
vorgezogenes Hardening-Lastenheft.

Diese Freigabe beweist die gemeinsame Framework-Basis. Sie beweist noch nicht
die anwendungsspezifische C#-Komposition fuer Rechner, Kalender, Puzzle,
ASCII-Tabelle, Hilfe-/Resource-Flows, Editor, Mausdialog oder die grosse
TP7-Demo. Genau diese Verbraucherarbeit ist der Scope von Feature 032.

*Feature 031 independently closed the shared framework basis. It did not
implement the application-specific C# composition for the calculator,
calendar, puzzle, ASCII table, help and resource flows, editor, mouse dialog,
or the large TP7 demo. That consumer work is the scope of Feature 032.*

---

## 2. Zweistufiges Liefermodell / Two-Stage Delivery Model

### Stufe 1: Feature 032 - funktionale Portierung und Proof

- Portiert die fachlichen Kernablaeufe.
- Liefert startbare Beispielprojekte und kontrollierte Fixtures.
- Beweist mindestens einen realen interaktiven Kernpfad je Anwendung.
- Nutzt App-Loop, Event-/Command-Dispatch, konkreten Zustand, View-Baum und
  Buffer-/Cell-Evidence.
- Liefert pro Anwendung eine erste didaktische Anleitung.
- Dokumentiert ehrlich, welche sichtbaren Zustaende und Komfortpfade noch
  nicht Showcase-Reife besitzen.

### Stufe 2: spaeteres Wave-5-Showcase-Feature

- Wird erst nach dem gemergten Feature 032 aus dessen Delta-Matrix erstellt.
- Bringt jedes Beispiel auf das vollstaendige Drei-Schichten-Modell:
  sichtbare Hauptkomponente, echte `TStatusLine` und tastaturerreichbares
  `Help -> Description`.
- Schliesst alle akzeptierten Bedienpfade, constrained layouts, text-first
  A11Y, relevante Mauspfade und die vollstaendige sichtbare Smoke-Matrix.
- Muss vor Wave 6 abgeschlossen sein.

*Stage 1 delivers functional ports and real-path proof. Stage 2 is created
from the delivered delta matrix and completes the full three-layer showcase,
interaction, constrained-layout, accessibility, and visible smoke standard.
Wave 6 remains blocked until both stages and the Wave-5 delta review are
complete.*

---

## 3. Historische Quelleninventur / Historical Source Inventory

Die folgenden 15 Pascal-Dateien bilden den verbindlichen Wave-5-Quellumfang:

| Quelle | Historische Rolle | Erwartete moderne Rolle |
|---|---|---|
| `TVDEMOS/TVDEMO.PAS` | grosse Demo-Anwendung | eigenstaendiger Haupt-Slice |
| `TVDEMOS/DEMOCMDS.PAS` | Command-IDs | typisierte lokale Command-Konstanten |
| `TVDEMOS/DEMOSTRS.PAS` | Texte und Labels | source-controlled UTF-8-Ressourcen |
| `TVDEMOS/GADGETS.PAS` | Clock-/Heap-/Idle-Gadgets | begrenzte sichtbare Demo-Gadgets |
| `TVDEMOS/TVEDIT.PAS` | Editor-Anwendung | eigenstaendiger Editor-Slice |
| `TVDEMOS/TVHC.PAS` | Hilfe-Compiler | Help-Compiler-/Viewer-Slice |
| `TVDEMOS/HELPFILE.PAS` | Help-Quell-/Dateifluss | kontrollierte Help-Fixture |
| `TVDEMOS/DEMOHELP.PAS` | Help-Kontexte | typisierte Help-Kontexte und Inhalte |
| `TVDEMOS/TVRDEMO.PAS` | Runtime-Ressourcen | allowlist-basierte Resource-Demo |
| `TVDEMOS/GENRDEMO.PAS` | Resource-Generator | geschlossener moderner Generatorpfad |
| `TVDEMOS/ASCIITAB.PAS` | ASCII-Tabellen-Demo | eigenstaendiger Widget-Slice |
| `TVDEMOS/CALC.PAS` | Rechner-Demo | eigenstaendiger Fach-Slice |
| `TVDEMOS/CALENDAR.PAS` | Kalender-Demo | eigenstaendiger Fach-Slice |
| `TVDEMOS/PUZZLE.PAS` | Puzzle-Demo | eigenstaendiger Fach-Slice |
| `TVDEMOS/MOUSEDLG.PAS` | Maus-/Settings-Dialog | Maus-Slice mit Tastaturfallback |

Jede Quelle erhaelt genau eine primaere Entscheidung:

- `EntryPoint`
- `SupportUnit`
- `FixtureOrContent`
- `GeneratorIntent`
- `IntentionalOmission`

`IntentionalOmission` benoetigt historische Funktion, moderne Alternative,
Grund, sichtbare Auswirkung und Follow-up-Grenze. Keine Datei wird kopiert,
veraendert, kompiliert oder als ausgeliefertes Pascal-Artefakt vendort.

*Each source receives exactly one primary role. Intentional omission requires
the historical purpose, modern alternative, rationale, visible impact, and
follow-up boundary. No Pascal source is modified, compiled, copied, or
vendored as a delivered artifact.*

---

## 4. Verbindliche Consumer-Gruppen / Binding Consumer Groups

| ID | Quellen | Kernfluss | Ausgangsentscheidung |
|---|---|---|---|
| `W5-001` | `TVDEMO.PAS` plus `DEMOCMDS.PAS`, `DEMOSTRS.PAS`, `GADGETS.PAS` | App-Loop, Menues, Status, Dialoge, Hilfe, Idle, Commands und Fenster | `UseExistingFramework` |
| `W5-002` | `TVEDIT.PAS` | Editorfenster, Dialogausfuehrung, Close/Resize/Next und typisierte Dateientscheidungen | `UseExistingFramework` |
| `W5-003` | `TVRDEMO.PAS` | benannte Runtime-Ressourcen fuer Menue, Status, Dialog und sichtbare Komposition | `UseExistingFramework` |
| `W5-004` | `GENRDEMO.PAS` | geschlossene beschriebene oder generierte UI-Ressourcen | `UseExistingFramework` |
| `W5-005` | `ASCIITAB.PAS`, `CALC.PAS`, `CALENDAR.PAS`, `PUZZLE.PAS` sowie Gadget-Anteile | begrenzte Fachlogik, Idle und Command-State | `UseExistingFramework` |
| `W5-006` | `MOUSEDLG.PAS` | Mouse-Events, Capability und vollstaendiger Tastaturpfad | `UseExistingFramework` |

Die Help-Dateien `TVHC.PAS`, `HELPFILE.PAS` und `DEMOHELP.PAS` bilden einen
verbindlichen Querschnitt zwischen `W5-001`, `W5-002` und dem vorhandenen
Feature-018-/019-Help-Stack. Sie muessen gemeinsam inventarisiert und in
mindestens einem startbaren Wave-5-Beispiel sichtbar bewiesen werden.

*The help files form a binding cross-cutting slice across the main demo,
editor, and existing help stack. They must be inventoried together and proven
visibly in at least one runnable Wave-5 example.*

---

## 5. Moderne Beispielstruktur / Modern Example Shape

Die Spezifikation legt die finalen Projektordner fest. Namenskollisionen mit
den bestehenden Beispielen `Demo`, `TvEdit`, `HelpDemo` und `TvHc` sind zu
vermeiden. Empfohlene sprechende Namen sind:

- `Tp7Demo`
- `Tp7Edit`
- `Tp7Help`
- `Tp7ResourceDemo`
- `Tp7ResourceGenerator`
- `Tp7AsciiTable`
- `Tp7Calculator`
- `Tp7Calendar`
- `Tp7Puzzle`
- `Tp7MouseDialog`

Zusammengehoerige historische Units duerfen in einem modernen Projekt
gebuendelt werden. Jede Buendelung muss in der Source-Matrix nachvollziehbar
sein. Gemeinsame wiederverwendbare Framework-Logik darf nicht als lokale
`examples/`-Sonderloesung dupliziert werden.

*The specification chooses final project names and avoids collisions with
existing examples. Related Pascal units may be grouped into one modern
project when the source matrix remains traceable. Reusable framework behavior
must not become a local examples-only substitute.*

---

## 6. Funktionale Anforderungen / Functional Requirements

### W5F-001: Historische Absicht, moderne C#-Umsetzung

Die Pascal-Quellen sind vor Spezifikation und Implementierung read-only zu
lesen. Uebernommen werden Zweck, Nutzerfluss und beobachtbares Verhalten,
nicht Pascal-Objektmodell, DOS-API, Speicherlayout oder Zeilenstruktur.

### W5F-002: Reale TuiVision-Pfade

Primaerer Proof muss `app.Run()` oder einen gleichwertigen echten
Anwendungsloop verwenden. Eingaben laufen ueber reale Key-, Command-, Mouse-
oder Event-Pfade. Direkte Helfer sind nur fuer Setup oder Zusatzbeweis
zulaessig.

### W5F-003: Vertikaler Referenz-Slice zuerst

`Tp7Calculator` oder ein gleichwertiger kleiner, klarer Fach-Slice ist zuerst
test-first zu liefern. Er muss Projektaufbau, historische Traceability,
Keyboard-Command, sichtbaren Zustand, View-Identitaet, Cell-Proof, Guide und
Evidence-Muster vor der breiten Wiederholung zeigen.

### W5F-004: Mindest-Sichtbarkeit in Stufe 1

Jedes Beispiel zeigt beim normalen Start Zweck, aktuellen Zustand und
mindestens einen tastaturerreichbaren Kernpfad. Reiner Startup, nur ein
Statusstring oder nur ein direkter Testhelfer reicht nicht.

### W5F-005: App- und Command-Shell

Die grosse Demo verwendet vorhandene `TApplication`-, `TDesktop`-,
`TMenuBar`-, `TStatusLine`-, Dialog-, Help-, Idle- und Command-Kontrakte.
Commands werden genau einmal verarbeitet; Aktivierung, Deaktivierung,
Tile/Cascade/Next/Close und Fokus folgen den geschlossenen Framework-Pfaden.

### W5F-006: Editor und sichere Dateigrenze

Der Editor-Slice verwendet `TEditor`, `TFileEditor` oder die vorhandene
Editor-Shell. Tests und Guides verwenden nur source-controlled Fixtures oder
Test-Temp-Verzeichnisse. Beliebige Nutzerdaten duerfen weder gelesen noch
ueberschrieben werden. Modified-, Safe-Close-, Conflict- und
FileDialog-Entscheidungen bleiben explizit.

### W5F-007: Help-Compiler und Help-Anzeige

Der Help-Slice verwendet den vorhandenen geschlossenen Help-Source-Compiler,
Runtime-Help und Fallback-Vertrag. Proprietäre oder ungepruefte historische
Binärformate werden nicht dekodiert. Gueltige, fehlerhafte, unbekannte und
Cross-Reference-Pfade bleiben sichtbar und testbar.

### W5F-008: Ressourcen und Generator

Runtime-Resource- und Generator-Slices verwenden exakte Keys, registrierte
Typen, geschlossene primitive Records, Bounds und atomare Ablehnung. Der
Generator darf nur das akzeptierte allowlist-basierte Schema erzeugen. Keine
beliebige Runtime-Typaktivierung und kein ungeprueftes historisches
Resource-Format sind zulaessig.

### W5F-009: Fach-/Widget-Demos

ASCII-Tabelle, Rechner, Kalender und Puzzle verwenden lokale, klar begrenzte
Fachlogik auf vorhandenen Controls. Rechen-, Datums-, Auswahl- und
Puzzlegrenzen muessen deterministisch und ohne Host-Locale- oder
Zeitzonen-Zufall testbar sein.

### W5F-010: Idle- und Gadget-Grenze

Clock-, Heap- oder andere Gadget-Analoga duerfen pro Idle-Zyklus nur
begrenzte Arbeit ausfuehren. Host-Speicherwerte werden nicht als stabile
Produktsemantik behauptet. Deterministische Fixtures oder klar markierte
Statusklassen sind zu bevorzugen.

### W5F-011: Maus mit Tastaturparitaet

Der Mouse-Dialog nutzt den vorhandenen SGR-/Capability-Vertrag. Jeder
Mauspfad besitzt einen vollstaendigen Tastaturpfad. Historische
Button-Reversal-, Timing- oder DOS-Treiberoptionen sind keine behauptete
native Paritaet und werden als bewusste Abweichung dokumentiert.

### W5F-012: Framework-Usage-Gate

Jeder Consumer erhaelt genau eine Entscheidung:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

Die Ausgangsentscheidung ist fuer alle sechs Consumer
`UseExistingFramework`. Ein neu entdeckter gemeinsamer Runtime- oder
API-Defekt darf nicht durch lokale Beispielsonderlogik verdeckt werden.
`SmallFrameworkFix` benoetigt einen reproduzierbaren Red-Test und eine
ausdrueckliche Scope-Pruefung. Ein breiter oder closure-widersprechender Fund
stoppt den betroffenen Slice als `FollowUpHardening`.

### W5F-013: Didaktische Kommentare und XML-Dokumentation

Neue oder geaenderte nicht-triviale Logik wird auf didaktischen
Inline-Kommentarwert geprueft. Kommentare erklaeren Warum, Trade-off,
historische Abweichung oder Proof-Grenze. Neue oeffentliche APIs benoetigen
vollstaendige XML-Dokumentation und loesen DocFX/A11Y aus.

### W5F-014: A11Y-Basis

Alle startbaren Beispiele sind ohne Maus bedienbar. Fokus, Shortcuts,
Validierungsablehnung, Status und High-Contrast-Verhalten werden text-first
geprueft. Neue sichtbare Widgets implementieren den bestehenden
Accessibility-Vertrag, wenn er anwendbar ist.

### W5F-015: Keine mechanische Vergleichsparitaet

Free Vision, Terminal.GUI und `magiblot/tvision` dienen nur als zweite
Meinung. Feature 032 darf keine API oder Architektur allein deshalb
uebernehmen, weil eine Vergleichsimplementierung sie besitzt.

*The functional requirements preserve historical purpose through real
TuiVision paths, controlled files and resources, bounded consumer logic,
keyboard-complete interaction, framework reuse, selective didactic comments,
and text-first accessibility. Comparison projects remain non-normative.*

---

## 7. Evidence-Modell / Evidence Model

`specs/032-wave5-tp7-functional-porting/pr-evidence.md` wird vor der ersten
Implementierungsaenderung angelegt und enthaelt mindestens:

### 7.1 Source-Matrix

`SourceId`, `PascalPath`, `HistoricalPurpose`, `PrimaryRole`,
`ManagedTarget`, `IntentPreserved`, `IntentionalDeviation`,
`ProofBoundary`, `FollowUp`

### 7.2 Consumer-Matrix

`ConsumerId`, `ManagedExample`, `ContractIds`, `FrameworkDecision`,
`RealPath`, `StateProof`, `ViewProof`, `BufferCellProof`, `KeyboardProof`,
`MouseProof`, `A11YProof`, `Guide`, `ResidualDelta`

### 7.3 Showcase-Delta-Matrix

`ManagedExample`, `DeliveredIn032`, `MissingVisualState`,
`MissingInteraction`, `MissingLayoutProof`, `MissingA11YProof`,
`Stage2Priority`, `Rationale`

Eine leere Delta-Zeile ist nicht zulaessig. Entweder ist ein Bereich
vollstaendig, oder die konkrete verbleibende Arbeit wird benannt. Aus dieser
Matrix entsteht das spaetere Showcase-Lastenheft.

---

## 8. Sicherheit, Daten und Plattformen / Security, Data, and Platforms

- Keine neuen Pakete, externen Dienste, Datenbanken oder Runtime-AI.
- `TVDEMOS/`, `TVFM/`, `tv203s/` und externe Vergleichscheckouts bleiben
  read-only.
- Keine Shell-, Prozess-, PTY-, Host-Font-, Host-Codepage- oder
  Host-Terminal-Konfigurationsaenderung.
- Schreibende Tests verwenden nur Test-Temp-Verzeichnisse.
- Resource- und Help-Parser bleiben geschlossen, gebunden und fail-closed.
- Linux, macOS und Windows muessen die relevanten Beispiel- und
  Frameworkpfade ueber echte Remote-Jobs ausfuehren.
- SGR-Mausunterstuetzung bleibt capability-basiert; Unsupported-Faelle sind
  ehrlich und tastaturvollstaendig.
- `AI-SBOM`, ASVS, NIS2, DORA, EU AI Act, BSI C3A/C5 und Zero Trust werden
  triggerbasiert bewertet und voraussichtlich mit begruendetem `N/A`
  dokumentiert. NIST SSDF, CWE Top 25, Supply-Chain-, A11Y- und
  Agent-Parity-Evidence bleiben anwendbar.

---

## 9. Nichtziele / Out of Scope

- Wave-5-Showcase-Stufe vollstaendig vorwegnehmen
- Wave 6 oder `TVFM/` portieren
- Post-Wave-6-Portfolio-Audit starten
- breites Framework-Redesign
- mechanische Pascal-zu-C#-Uebersetzung
- historische Binärressourcen ungeprueft dekodieren
- beliebige Nutzerdateien als Proof lesen oder schreiben
- DOS-Speichermodelle, Overlay, CRT, direkte Video- oder Treiber-APIs
  nachbilden
- neue Runtime-Abhaengigkeiten oder native Bridges
- Maus als einzigen Bedienpfad verwenden
- Free Vision, Terminal.GUI oder magiblot/tvision als neue normative Quelle
  behandeln

---

## 10. Validierung / Validation

Feature 032 muss mindestens ausfuehren und in Evidence binden:

1. `specify check` und Repository-Prerequisites
2. vollstaendige Clarify-, Checklist-, Plan-, Task- und Analyze-Konvergenz
3. Source-/Consumer-/Delta-Matrix-Validatoren mit negativen Fixtures
4. `git diff --check` und `git diff --cached --check`
5. `dotnet format --verify-no-changes`
6. gezielte Release-Smokes fuer jeden neuen Beispiel-Slice
7. vollstaendige Release-Tests
8. kanonisches Coverage-Gate fuer die fuenf Framework-Assemblies
9. `docfx docfx.json`
10. Playwright/Axe fuer die erzeugte Dokumentation
11. UTF-8-/Lynx-/text-first-Review
12. Secret-, Dependency-, Supply-Chain- und Generated-Output-Scans
13. Agent-Paritaet und Preset-Aufloesung
14. Linux-, macOS- und Windows-Remote-Proof
15. exakte Reviewed-HEAD-Gate-Evidence vor Merge

Vor jedem einzelnen `dotnet build`- oder `dotnet test`-Aufruf wird der manuelle
Build-Zaehler genau einmal erhoeht. Vor Commit und Push wird die nummerierte
Branch-Version ausgerichtet.

---

## 11. Akzeptanzkriterien / Acceptance Criteria

1. Feature 032 verwendet exakt den Branch
   `032-wave5-tp7-functional-porting`.
2. Alle 15 Pascal-Quellen besitzen genau eine primaere Rolle.
3. Alle sechs Consumer-Gruppen `W5-001` bis `W5-006` besitzen genau eine
   Framework-Entscheidung und konkrete Proof-Evidence.
4. Jedes gelieferte Beispiel baut und zeigt beim normalen Start einen
   erkennbaren Zweck sowie mindestens einen tastaturerreichbaren Kernpfad.
5. Primaere Smokes verwenden reale App-Loop-/Dispatch-Pfade und kombinieren
   Zustand, View und Buffer-/Cell-Proof.
6. Datei-, Help-, Resource-, Generator-, Idle- und Mouse-Grenzen sind
   deterministisch, fail-closed und tastaturvollstaendig.
7. Es existiert keine wiederverwendbare lokale Ersatzframeworklogik.
8. `TVDEMOS/`, `TVFM/`, `tv203s/` und externe Quellen haben null Diff.
9. Guides, XML-Dokumentation, A11Y und didaktische Kommentare erfuellen die
   Repository-Regeln.
10. Alle lokalen, Remote-, Review- und Exact-Head-Gates sind gruen oder tragen
    eine ausdruecklich zulaessige Nichtanwendbarkeit.
11. Eine vollstaendige Showcase-Delta-Matrix beschreibt die tatsaechlich
    verbleibende zweite Stufe.
12. Wave 6 bleibt blockiert. Feature 032 startet weder Feature 033 noch Wave 6.
13. Nach Merge sind lokales `main` und `origin/main` sauber und identisch.

---

## 12. Stop-Grenzen / Stop Boundaries

Der autonome Lauf stoppt bei:

- einem neuen gemeinsamen Frameworkdefekt, der die Feature-031-Closure
  materiell widerlegt;
- notwendiger breiter API- oder Architekturentscheidung;
- unkontrollierbarer Nutzerdatei-, Prozess-, Shell- oder Host-Konfiguration;
- historischem oder externem Source-Diff;
- fehlender Tastaturparitaet fuer einen verpflichtenden Mauspfad;
- nicht aufloesbarer Quell-/Consumer-Zuordnung;
- fehlgeschlagenem Pflichtgate;
- notwendiger destruktiver Produktentscheidung;
- fehlender aktueller Remote-/Merge-Autoritaet.

Ein solcher Fund wird nicht als lokale Beispielsonderlogik umgangen.

---

## 13. Spec-Kit-Auftrag / Spec Kit Command

```text
$speckit-autonomous Execute the complete autonomous Spec Kit run for Feature
032 using `Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md` as the binding
intake.

Delivery mode: MergeAndSync.

Start from clean synchronized main after Feature 031. Create exactly branch
`032-wave5-tp7-functional-porting` and feature directory
`specs/032-wave5-tp7-functional-porting`.

Run Specify, repeated Clarify, all useful domain and governance checklists,
Plan, plan review, Tasks, repeated Analyze, Implement, full validation,
delivery, and retrospective to their documented convergence criteria.

Deliver only Wave-5 stage 1: functional TP7 ports and real-path proof for all
15 source files and all six accepted W5 consumer groups. Keep historical and
external sources read-only. Do not start the showcase stage, Feature 033,
Wave 6, or the post-Wave-6 portfolio audit.

Create evidence and autonomous state before implementation. Prove one small
vertical example slice test-first before broad rollout. Require real app-loop
or dispatch proof, state, view identity, rendered buffer/cells, keyboard
operation, controlled file/resource boundaries, A11Y, guides, and an explicit
showcase-delta row for every delivered example.

Use existing framework contracts. Do not hide a shared defect in local example
code. Stop and route a material framework contradiction to FollowUpHardening.

Run all local, cross-platform, documentation, accessibility, security,
supply-chain, exact-head, review, and merge gates. Merge and synchronize main
when complete. Retrospectively promote only reproducible provider-neutral
autonomous-preset defects. Do not create an empty follow-up branch or PR.
```
