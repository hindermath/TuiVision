# Feature Specification: Wave-5 TP7 Functional Porting and Proof

**Feature Branch**: `032-wave5-tp7-functional-porting`
**Created**: 2026-07-16
**Status**: Draft
**Binding Input**: `Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md`

## Klärungen / Clarifications

### Session 2026-07-16

- Keine formale Rückfrage ist erforderlich. Lastenheft 17, Feature-031-
  Closure, die sechs akzeptierten Wave-5-Consumer-Gruppen und der aktuelle
  Benutzerauftrag legen Scope, Liefermodus, Quellenmenge, Stop-Grenzen und
  zweistufige Reihenfolge vollständig fest.
- Feature 032 ist ausschließlich die funktionale erste Wave-5-Stufe. Die
  spätere Showcase-Stufe wird erst aus der tatsächlich gelieferten
  Delta-Matrix erstellt. Feature 033, Wave 6 und der Post-Wave-6-Audit werden
  nicht gestartet.
- Die zehn verwalteten Beispielziele sind `Tp7Demo`, `Tp7Edit`, `Tp7Help`,
  `Tp7ResourceDemo`, `Tp7ResourceGenerator`, `Tp7AsciiTable`,
  `Tp7Calculator`, `Tp7Calendar`, `Tp7Puzzle` und `Tp7MouseDialog`.
- Die 15 Pascal-Dateien bleiben read-only und erhalten genau eine primäre
  Quellenrolle. Zusammengehörige historische Units dürfen in einem modernen
  Beispiel gebündelt werden.
- Ein gemeinsamer Frameworkdefekt darf nicht durch lokale Beispielsonderlogik
  verdeckt werden. Ein reproduzierbarer kleiner Fix braucht einen roten
  Vertrag und eine Scope-Prüfung; ein breiter oder closure-widersprechender
  Fund wird `FollowUpHardening`.
- Die wiederholte Klärungsprüfung findet keine weitere planungswirksame Frage.
  Kalender-Proofs verwenden eine feste Datum-Fixture, Puzzle-Proofs einen
  festen Startzustand und eine feste Zugfolge. Beide dürfen weder Systemzeit
  noch Zufall als Testoracle verwenden.
- Der Mausdialog bildet historische Einstellungen nur als lokalen,
  beobachtbaren Beispielzustand ab. Er verändert weder Host-Konfiguration noch
  globale Mausparameter und bleibt vollständig per Tastatur bedienbar.
- Wiederverwendbare Wave-5-Beispielkomposition darf in genau einer bewusst
  gemeinsam kompilierten Beispielassembly liegen. Die zehn eigenständigen
  Startprojekte bleiben erhalten; mehrfach gelinkte Quelldateien mit
  widersprüchlicher CLR-Typidentität sind zu vermeiden.

*No formal question is required. Feature 032 is only the functional first
Wave-5 stage. It has ten managed example targets, exactly 15 read-only Pascal
source roles, and six accepted consumer groups. A later showcase stage is
derived from the delivered delta matrix. Shared framework defects must not be
hidden in local example code.*

## Nutzungsszenarien und Prüfung / User Scenarios and Testing

### User Story 1 - Kleinen TP7-Fachslice vollständig erleben (Priority: P1)

Als Lernender möchte ich einen überschaubaren TP7-Fachslice als moderne
TuiVision-Anwendung starten und per Tastatur bedienen können, damit ich den
historischen Zweck, den aktuellen Zustand und den realen Frameworkpfad
nachvollziehen kann.

*As a learner, I want to run and operate a small TP7 domain slice as a modern
TuiVision application so I can understand the historical purpose, current
state, and real framework path.*

**Why this priority**: Der erste kleine Slice definiert das wiederverwendbare
Projekt-, Test-, Guide- und Evidence-Muster, bevor die große Demo und die
weiteren Anwendungen folgen.

**Independent Test**: `Tp7Calculator` startet normal, verarbeitet eine
Tastaturaktion über den echten App-Loop, zeigt ein deterministisches Ergebnis
und beweist Zustand, View-Identität sowie gerenderte Zellen.

**Acceptance Scenarios**:

1. **Given** den normalen Start von `Tp7Calculator`, **When** eine gültige
   Rechenaktion per Tastatur ausgelöst wird, **Then** erscheint das richtige
   Ergebnis sichtbar und im Anwendungszustand.
2. **Given** eine ungültige oder nicht ausführbare Rechenaktion, **When** sie
   ausgelöst wird, **Then** bleibt der vorherige gültige Zustand erhalten und
   die Ablehnung ist textlich erkennbar.
3. **Given** den primären Smoke-Test, **When** er den realen Anwendungsloop
   ausführt, **Then** weist er konkrete Zustands-, View- und Cell-Evidence
   nach.

---

### User Story 2 - Große Demo, Editor und Hilfe funktional nutzen (Priority: P2)

Als Anwendungsentwickler möchte ich die große TP7-Demo, den Editor und den
Help-Compiler-/Viewer-Pfad als getrennte moderne Beispiele ausführen können,
damit die zentralen App-, Fenster-, Datei- und Hilfeverträge gemeinsam
nachvollziehbar werden.

*As an application developer, I want to run the large TP7 demo, editor, and
help compiler/viewer path as separate modern examples so the central
application, window, file, and help contracts are understandable together.*

**Why this priority**: Diese Slices verwenden die meisten geschlossenen
Frameworkverträge und sind die wichtigste Vorbereitung für spätere
Wave-5-Showcase- und Wave-6-Arbeit.

**Independent Test**: `Tp7Demo`, `Tp7Edit` und `Tp7Help` starten getrennt und
beweisen je mindestens einen echten Command-, Datei- oder Help-Pfad mit
sichtbarem Zustand.

**Acceptance Scenarios**:

1. **Given** `Tp7Demo`, **When** ein Fenster- oder Demo-Command ausgeführt
   wird, **Then** wird es genau einmal verarbeitet und der sichtbare Zustand
   aktualisiert.
2. **Given** `Tp7Edit` mit einer kontrollierten Fixture, **When** Text geändert
   und ein Close- oder Save-Pfad ausgelöst wird, **Then** bleiben Modified-,
   Safe-Close- und Dateigrenzen explizit.
3. **Given** `Tp7Help`, **When** gültige, unbekannte und fehlerhafte
   Help-Inhalte verarbeitet werden, **Then** sind Erfolg, Fallback und
   Ablehnung sichtbar und reproduzierbar.

---

### User Story 3 - Ressourcen sicher erzeugen und anzeigen (Priority: P3)

Als Maintainer möchte ich die historische Resource-Demo und Generatorabsicht
über das geschlossene moderne Schema nachvollziehen können, damit benannte
Menüs, Statuszeilen und Dialoge ohne unsichere Binär- oder Typaktivierung
entstehen.

*As a maintainer, I want to understand the historical resource demo and
generator intent through the closed modern schema so named menus, status
lines, and dialogs are created without unsafe binary or type activation.*

**Why this priority**: Resource- und Generatorpfade sind sicherheits- und
wartungsrelevant und dürfen nicht durch unkontrollierte historische
Formatparität ersetzt werden.

**Independent Test**: `Tp7ResourceGenerator` erzeugt nur erlaubte Records in
einem kontrollierten Ziel; `Tp7ResourceDemo` liest diese über exakte Keys und
zeigt einen realen rekonstruierten UI-Zustand.

**Acceptance Scenarios**:

1. **Given** eine gültige beschriebene UI-Ressource, **When** sie erzeugt und
   geladen wird, **Then** bleiben Schlüssel, Reihenfolge und sichtbare
   Identität erhalten.
2. **Given** einen unbekannten Typ, doppelten Schlüssel, ungültige Länge oder
   nicht erlaubten Record, **When** er verarbeitet wird, **Then** wird die
   gesamte Eingabe atomar abgelehnt.
3. **Given** ein schreibendes Generatorziel, **When** der Proof läuft, **Then**
   befindet es sich ausschließlich in einem Test-Temp-Verzeichnis.

---

### User Story 4 - Fach-, Gadget- und Mausbeispiele bedienen (Priority: P4)

Als Lernender möchte ich ASCII-Tabelle, Kalender, Puzzle und Mausdialog mit
vollständigen Tastaturpfaden ausprobieren können, damit Fachlogik,
Idle-Verhalten, Fokus und Capability-Grenzen sichtbar werden.

*As a learner, I want to operate the ASCII table, calendar, puzzle, and mouse
dialog with complete keyboard paths so domain logic, idle behavior, focus, and
capability boundaries become visible.*

**Why this priority**: Diese Beispiele erweitern die funktionale Breite,
dürfen aber keine Host-Zufälle, ungebundene Idle-Arbeit oder
maus-exklusive Bedienung einführen.

**Independent Test**: Die vier Fachbeispiele und `Tp7MouseDialog` besitzen je
einen realen App-Loop-Smoke; Mausunterstützung und Unsupported-Fallback werden
zusätzlich per Tastatur vollständig bedient.

**Acceptance Scenarios**:

1. **Given** `Tp7AsciiTable`, `Tp7Calendar` oder `Tp7Puzzle`, **When** eine
   Navigation oder Auswahl ausgelöst wird, **Then** ändern sich Zustand und
   sichtbare Zellen deterministisch.
2. **Given** einen Idle-basierten Gadget-Zustand, **When** ein Idle-Zyklus
   läuft, **Then** bleibt die Arbeit begrenzt und der Status nachvollziehbar.
3. **Given** eine unterstützte oder nicht unterstützte Maus-Capability,
   **When** derselbe Dialog bedient wird, **Then** bleibt der vollständige
   Tastaturpfad verfügbar und der Capability-Status sichtbar.

---

### User Story 5 - Restarbeit für die Showcase-Stufe ehrlich bestimmen (Priority: P5)

Als Projektverantwortlicher möchte ich pro geliefertem Beispiel wissen, welche
Visual-, Interaktions-, Layout- oder A11Y-Arbeit noch fehlt, damit die zweite
Wave-5-Stufe ausschließlich reale Deltas und keine Vermutungen enthält.

*As the project owner, I want to know the remaining visual, interaction,
layout, and accessibility work for each delivered example so the second
Wave-5 stage contains only real deltas rather than assumptions.*

**Why this priority**: Ohne vollständige Delta-Matrix würde die zweite Stufe
entweder wichtige Arbeit übersehen oder bereits erfüllte Arbeit wiederholen.

**Independent Test**: Die Showcase-Delta-Matrix besitzt genau zehn Zeilen,
eine je verwaltetem Beispiel, und jede Dimension ist entweder abgeschlossen
oder mit konkreter Restarbeit und Priorität beschrieben.

**Acceptance Scenarios**:

1. **Given** die zehn gelieferten Beispiele, **When** ihre Stage-1-Evidence
   geprüft wird, **Then** besitzt jedes genau eine Delta-Zeile.
2. **Given** einen bereits vollständigen Bereich, **When** er bewertet wird,
   **Then** wird er als abgeschlossen und nicht als leeres Follow-up markiert.
3. **Given** verbleibende Showcase-Arbeit, **When** sie dokumentiert wird,
   **Then** nennt sie konkrete sichtbare oder bedienbare Akzeptanzgrenzen.

### Randfälle / Edge Cases

- Eine Pascal-Datei ist nur Support-Unit oder Fixture-Inhalt und benötigt kein
  eigenes modernes Projekt.
- Zwei historische Units liefern gemeinsam genau einen modernen App-Slice.
- Eine Resource besitzt einen bekannten Schlüssel, aber einen nicht
  registrierten Typ.
- Eine Help-Quelle enthält eine Vorwärtsreferenz, unbekannten Kontext oder
  ungültigen Bereich.
- Ein Editorpfad versucht, ein Ziel außerhalb des Test-Temp-Verzeichnisses zu
  verwenden.
- Eine Division durch null oder unvollständige Rechenoperation darf keinen
  ungültigen Ergebniszustand übernehmen.
- Kalenderlogik darf nicht von Host-Locale, aktueller Zeitzone oder
  Systemdatum als Testoracle abhängen.
- Ein Puzzlezug ist außerhalb des Rasters oder nicht mit der Leerstelle
  benachbart.
- Ein Idle-Zyklus wird mehrfach ohne neues Ereignis ausgeführt.
- Maus-Capability fällt während einer Interaktion aus.
- Ein Beispiel passt nicht in eine begrenzte Terminalgröße.
- Ein gemeinsamer Frameworkdefekt reproduziert trotz Feature-031-Closure.
- Ein Remote-Job trägt einen Plattformnamen, führt aber keinen geforderten
  Beispiel- oder Runtime-Test aus.
- Copilot oder ein anderer Reviewer ist wegen Quote nicht verfügbar.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Feature 032 MUSS Lastenheft 17 und die gemergte
  Feature-031-Closure als bindende Eingabe behandeln.
- **FR-002**: Der Lauf MUSS genau die 15 Pascal-Dateien aus der
  Quelleninventur prüfen; jede MUSS genau eine primäre Rolle aus
  `EntryPoint`, `SupportUnit`, `FixtureOrContent`, `GeneratorIntent` oder
  `IntentionalOmission` erhalten.
- **FR-003**: `IntentionalOmission` MUSS historische Funktion, moderne
  Alternative, Begründung, sichtbare Auswirkung und Follow-up-Grenze nennen.
- **FR-004**: `TVDEMOS/`, `TVFM/`, `tv203s/` und externe
  Vergleichscheckouts MÜSSEN read-only bleiben.
- **FR-005**: Das Feature MUSS genau die sechs Consumer-Gruppen `W5-001` bis
  `W5-006` abdecken.
- **FR-006**: Jeder Consumer MUSS genau eine Frameworkentscheidung aus
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening` erhalten.
- **FR-007**: Die Ausgangsentscheidung aller sechs Consumer MUSS
  `UseExistingFramework` sein; jede Abweichung benötigt reproduzierbare
  Evidence.
- **FR-008**: Ein gemeinsamer Defekt DARF NICHT durch lokale
  Beispielsonderlogik verdeckt werden.
- **FR-009**: `SmallFrameworkFix` MUSS einen vorangehenden beobachtbaren
  Red-Test, eine begrenzte Änderung und vollständige Regressionsevidence
  besitzen.
- **FR-010**: Ein breiter, API-brechender oder Feature-031-widersprechender
  Fund MUSS als `FollowUpHardening` den betroffenen Slice stoppen.
- **FR-011**: Das Feature MUSS genau die zehn verwalteten Beispielziele
  `Tp7Demo`, `Tp7Edit`, `Tp7Help`, `Tp7ResourceDemo`,
  `Tp7ResourceGenerator`, `Tp7AsciiTable`, `Tp7Calculator`, `Tp7Calendar`,
  `Tp7Puzzle` und `Tp7MouseDialog` liefern.
- **FR-012**: Jedes Beispiel MUSS als eigenständiges .NET-Projekt bauen und
  über einen dokumentierten normalen Startpfad ausführbar sein.
- **FR-013**: Jedes Beispiel MUSS beim normalen Start Zweck, aktuellen Zustand
  und mindestens einen tastaturerreichbaren Kernpfad sichtbar machen.
- **FR-014**: Der primäre Proof jedes Beispiels MUSS `app.Run()` oder einen
  gleichwertigen echten App-Loop-/Dispatch-Pfad verwenden.
- **FR-015**: Der primäre Proof MUSS konkreten Fach- oder Anwendungszustand,
  relevante View-Identität und gerenderte Buffer-/Cell-Evidence verbinden.
- **FR-016**: Direkte Helfer DÜRFEN nur Setup oder ergänzenden Proof liefern.
- **FR-017**: `Tp7Calculator` MUSS als erster test-first Referenz-Slice das
  vollständige Projekt-, Test-, Guide- und Evidence-Muster liefern.
- **FR-018**: `Tp7Demo` MUSS vorhandene Application-, Desktop-, Menu-,
  StatusLine-, Dialog-, Help-, Idle- und Command-Verträge verwenden.
- **FR-019**: Commands MÜSSEN genau einmal verarbeitet werden; Fokus,
  Aktivierung, Next, Close und Fensteranordnung MÜSSEN bestehende
  Frameworkpfade verwenden.
- **FR-020**: `Tp7Edit` MUSS vorhandene Editor- und typed File-Outcome-Verträge
  verwenden und Modified-, Safe-Close-, Konflikt- und Ablehnungspfade sichtbar
  halten.
- **FR-021**: Datei-Proofs DÜRFEN nur source-controlled Fixtures oder
  Test-Temp-Verzeichnisse lesen beziehungsweise beschreiben.
- **FR-022**: `Tp7Help` MUSS gültige Kompilierung, Navigation, unbekannten
  Kontext, ungültige Quelle und Fallback über den vorhandenen Help-Vertrag
  beweisen.
- **FR-023**: Proprietäre oder ungeprüfte historische Help-Binärformate
  DÜRFEN NICHT dekodiert werden.
- **FR-024**: `Tp7ResourceDemo` und `Tp7ResourceGenerator` MÜSSEN exakte Keys,
  registrierte Typen, geschlossene primitive Records, Bounds und atomare
  Ablehnung verwenden.
- **FR-025**: Der Generator DARF ausschließlich das akzeptierte
  allowlist-basierte Schema und kontrollierte Testziele erzeugen.
- **FR-026**: ASCII-, Rechner-, Kalender- und Puzzle-Logik MUSS
  deterministisch und unabhängig von Host-Locale, Zeitzone oder zufälligem
  Systemzustand prüfbar sein.
- **FR-026a**: Kalender-Proofs MÜSSEN eine feste Datum-Fixture verwenden;
  Puzzle-Proofs MÜSSEN einen festen Startzustand und eine feste Zugfolge
  verwenden. Systemzeit und Zufall DÜRFEN NICHT als Testoracle dienen.
- **FR-027**: Idle- und Gadget-Logik MUSS pro Zyklus begrenzte Arbeit
  ausführen; Host-Speicherwerte DÜRFEN NICHT als stabile Produktsemantik
  behauptet werden.
- **FR-028**: `Tp7MouseDialog` MUSS Capability-Status, unterstützten Mauspfad,
  Unsupported-Fallback und vollständige Tastaturparität beweisen.
- **FR-028a**: Historische Maus-Einstellungen DÜRFEN nur lokalen
  Beispielzustand ändern; Host-Konfiguration und globale Mausparameter
  MÜSSEN unverändert bleiben.
- **FR-029**: Kein Beispiel DARF Maus als einzigen Bedienpfad verlangen.
- **FR-030**: Neue sichtbare Widgets MÜSSEN den bestehenden
  Accessibility-Vertrag verwenden, wenn dieser anwendbar ist.
- **FR-031**: Fokus, Shortcuts, Status, Validierungsablehnung und
  High-Contrast-Verhalten MÜSSEN text-first nachvollziehbar sein.
- **FR-032**: Neue oder geänderte nicht triviale Logik MUSS auf didaktischen
  Inline-Kommentarwert geprüft werden; Kommentare erklären Warum,
  Trade-off, historische Abweichung oder Proof-Grenze.
- **FR-033**: Neue oder geänderte öffentliche APIs MÜSSEN vollständige
  XML-Dokumentation besitzen.
- **FR-034**: Jedes Beispiel MUSS eine erste DE-first/EN-second
  CEFR-B2-Anleitung mit Zweck, Start, Bedienung, historischer Quelle,
  Abweichung, A11Y und Proof-Grenze erhalten.
- **FR-035**: Die Feature-Evidence MUSS eine 15-zeilige Source-Matrix, eine
  sechszeilige Consumer-Matrix und eine zehnzeilige Showcase-Delta-Matrix
  enthalten.
- **FR-036**: Jede Showcase-Delta-Zeile MUSS gelieferte Funktion sowie
  verbleibende Visual-, Interaktions-, Layout- und A11Y-Arbeit oder deren
  vollständigen Abschluss benennen.
- **FR-037**: Eine leere oder pauschale Delta-Zeile ist unzulässig.
- **FR-038**: Free Vision, Terminal.GUI und `magiblot/tvision` DÜRFEN nur über
  akzeptierte Vorgängerevidence als nicht normative zweite Meinung dienen.
- **FR-039**: Das Feature DARF keine neuen Runtime-Pakete, externen Dienste,
  Datenbanken, Shells, Prozesse, PTYs oder nativen Bridges einführen.
- **FR-040**: Das Feature MUSS alle neuen Beispielprojekte und ihre Smokes in
  die bestehenden Solution-, Example-README-, Guide-, DocFX- und
  Coverage-/Projektinventarflächen integrieren, soweit ausgelöst.
- **FR-040a**: Gemeinsame Wave-5-Beispielkomposition DARF in genau einer
  gemeinsam kompilierten Beispielassembly liegen. Die zehn startbaren
  Beispielprojekte MÜSSEN eigenständig bleiben; dieselbe Quelle DARF NICHT so
  in mehrere Assemblies gelinkt werden, dass Tests gemeinsame
  CLR-Typidentität voraussetzen.
- **FR-041**: Das Feature MUSS gezielte Beispiel-Smokes, vollständige
  Release-Tests, das kanonische Fünf-Assembly-Coverage-Gate, Formatierung,
  DocFX/A11Y, Text-First-, Secret-, Supply-Chain-, Agent-Paritäts- und
  Plattformnachweise ausführen.
- **FR-042**: Jeder Remote-Acceptance-Nachweis MUSS den exakten reviewten Head,
  Workflow, Job, Plattform und tatsächlich ausgeführten Command abbilden.
- **FR-043**: Fehlende Reviewer oder Quota-Ausfälle MÜSSEN als fehlender
  Review und nicht als Pass dokumentiert werden.
- **FR-044**: Der finale Diff MUSS null Änderungen unter `TVDEMOS/`, `TVFM/`,
  `tv203s/` und externen Quellen enthalten.
- **FR-045**: Das Lastenheft MUSS nach erfolgreicher Abnahme über den
  Repository-Rename-Workflow archiviert werden.
- **FR-046**: Pflichtenheft, Reihenfolge, Agentenflächen und
  Projektstatistik MÜSSEN denselben finalen Feature-032- und Wave-Zustand
  nennen.
- **FR-047**: Feature 032 DARF Feature 033, die Showcase-Stufe, Wave 6 oder den
  Post-Wave-6-Audit nicht starten.
- **FR-048**: Wave 6 MUSS bis zu beiden abgeschlossenen Wave-5-Stufen und dem
  tatsächlichen Delta-Review blockiert bleiben.
- **FR-049**: Der autonome Lauf MUSS State, Artefakthashes, Tasks, Authority,
  letzte Operation und nächste exakte Aktion an logischen Grenzen pflegen.
- **FR-050**: Commit, Push, PR, Review-Konvergenz, Merge, Branch-Bereinigung
  und sauberer lokaler `HEAD == origin/main`-Sync MÜSSEN nachgewiesen werden.

### Governance-Anforderungen / Governance Requirements

- **GR-001**: `security-governance` v0.6.0 MUSS NIST SSDF, CWE Top 25,
  sichere Datei-/Resource-/Parsergrenzen, Secrets und Supply Chain als
  `Applicable` behandeln. ASVS, AI-SBOM, NIS2, CRA, EU AI Act und DORA
  bleiben ohne Trigger begründet `N/A`.
- **GR-002**: `architecture-governance` v0.5.0 MUSS STRIDE/CIA/CAPEC für
  Datei-, Resource-, Input- und Capability-Grenzen sowie Separation of
  Concerns anwenden. Zero Trust, BSI C3A und BSI C5 bleiben ohne Cloud-,
  Service- oder Provideränderung `N/A`.
- **GR-003**: `isaqb-architecture-governance` v0.2.0 MUSS Qualitätsziele,
  Consumer-Sichten, bewusste Abweichungen, Risiken und Technical Debt
  nachvollziehbar halten.
- **GR-004**: `a11y-governance` v0.4.0 MUSS Tastaturvollständigkeit,
  Fokus-/Statussignale, High Contrast, text-first Proof, bilinguale
  CEFR-B2-Guides und didaktische Kommentarprüfung anwenden.
- **GR-005**: `cross-platform-governance` v0.2.0 MUSS Linux-, macOS- und
  Windows-Laufzeitproof prüfen. Neue Skriptparität ist `N/A`, solange kein
  Skript entsteht.
- **GR-006**: `agent-parity-governance` v0.3.0 MUSS `AGENTS.md`, `CLAUDE.md`,
  `GEMINI.md` und `.github/copilot-instructions.md` gemeinsam prüfen.
- **GR-007**: `autonomous-run-governance` v0.2.2 MUSS Evidence-first,
  Konvergenz, State-/Authority-Revalidierung, Exact-Head-Gates, Review,
  nicht rekursiven Closeout und Retrospektive anwenden.

### Schlüsselentitäten / Key Entities

- **Historical Source Role**: Eine der 15 Pascal-Dateien mit historischem
  Zweck, primärer Rolle, modernem Ziel und Abweichungsgrenze.
- **Managed Example**: Eines der zehn startbaren C#-Beispiele mit Kernfluss,
  sichtbarem Zustand, Bedienpfad, Guide und Proof.
- **Consumer Decision**: Eine Entscheidung für `W5-001` bis `W5-006` mit
  Vertragsbezug, Framework-Nutzung, Proof und Restrisiko.
- **Real-Path Proof**: Ein App-Loop- oder Dispatch-Nachweis, der Zustand,
  View-Identität und gerenderte Buffer-/Cell-Evidence verbindet.
- **Controlled Data Boundary**: Source-controlled Fixture oder
  Test-Temp-Ziel für Datei-, Help-, Resource- und Generatorpfade.
- **Showcase Delta**: Pro Beispiel der konkrete nach Feature 032 verbleibende
  Visual-, Interaktions-, Layout- und A11Y-Umfang.

## Erfolgsmaße / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: Genau 15 von 15 Pascal-Quellen besitzen eine eindeutige primäre
  Rolle und einen vorhandenen Evidence-Pfad.
- **SC-002**: Genau sechs von sechs Consumer-Gruppen besitzen eine eindeutige
  Frameworkentscheidung und vollständigen Real-Path-Proof.
- **SC-003**: Zehn von zehn verwalteten Beispielen bauen und starten über den
  dokumentierten normalen CLI-Pfad.
- **SC-004**: Zehn von zehn Beispielen besitzen mindestens einen
  tastaturerreichbaren Kernpfad; kein Beispiel ist maus-exklusiv.
- **SC-005**: Zehn von zehn primären Smoke-Slices führen einen realen App-Loop-
  oder Dispatch-Pfad aus und kombinieren Zustand, View sowie Buffer-/Cell-
  Evidence.
- **SC-006**: Datei-, Help-, Resource- und Generator-Negativfälle erzeugen
  keine Teilmodelle oder Schreibwirkung außerhalb kontrollierter Testziele.
- **SC-007**: Die Showcase-Delta-Matrix enthält genau zehn vollständige,
  nicht leere Zeilen.
- **SC-008**: Alle gezielten und vollständigen Release-Tests bestehen; Core,
  Controls, Serialization, Compatibility und Drivers.Console behalten jeweils
  mindestens 70 Prozent Line Coverage.
- **SC-009**: DocFX baut mit null Warnungen und null Fehlern; der
  Playwright-/Axe-Pfad besteht vollständig.
- **SC-010**: Linux, macOS und Windows führen die anwendbaren Beispiel- und
  Runtime-Gates auf dem finalen reviewten Head erfolgreich aus.
- **SC-011**: Der finale Diff enthält null Änderungen an historischen oder
  externen Quellen, null neue Runtime-Abhängigkeiten und null Wave-6-Code.
- **SC-012**: Alle gepflegten Statusflächen nennen nach Abschluss Feature 032
  als erste gelieferte Wave-5-Stufe, die Showcase-Stufe als nächsten Intake
  und Wave 6 weiterhin blockiert.
- **SC-013**: Der Lauf endet mit vollständigen Tasks, null umsetzbaren
  Review-Threads, dokumentierten fehlenden Reviews, gemergtem PR und sauberem
  lokalem `main == origin/main`.

## Annahmen / Assumptions

- Die Feature-031-Closure ist die verbindliche gemeinsame Framework-Baseline.
- Die zehn empfohlenen Projektnamen vermeiden Kollisionen mit bestehenden
  Beispielen und sind für Lernende eindeutig.
- Zusammengehörige Pascal-Units werden nach Zweck statt mechanisch nach Datei
  auf moderne Projekte verteilt.
- `Tp7Calculator` ist der kleinste geeignete Referenz-Slice.
- Bestehende Controls, Serialization-, Help-, Editor-, Mouse- und A11Y-
  Verträge tragen die Consumer ohne breites Framework-Redesign.
- Der Community-Katalog-Issue `github/spec-kit#3569` ist extern und
  nicht blockierend; TuiVision verwendet bereits das validierte v0.2.2-Preset.
- AI ist ausschließlich Entwicklungswerkzeug und keine Runtime- oder
  Produktkomponente.

## Scope-Grenzen / Scope Boundaries

### In Scope

- zehn moderne Wave-5-Stage-1-Beispiele
- 15-zeilige historische Source-Traceability
- sechs Consumer-Entscheidungen und Real-Path-Proofs
- kontrollierte Fixtures, Guides, Smokes und Showcase-Delta-Evidence
- kleine reproduzierbare Frameworkfixes nur unter dem definierten Gate
- notwendige Solution-, README-, DocFX-, Agent-, Statistik- und
  Statussynchronisierung

### Out of Scope

- vollständige Wave-5-Showcase-Politur
- Feature 033
- Wave 6 und `TVFM/`
- Post-Wave-6-Portfolio-Audit
- breites Framework- oder API-Redesign
- mechanische Pascal-Übersetzung
- neue Runtime-Abhängigkeiten, Dienste, Datenbanken, Shells, Prozesse, PTYs
  oder native Bridges
- beliebige Nutzerdateien oder Host-Konfigurationsänderungen
- normative Übernahme von Free Vision, Terminal.GUI oder magiblot/tvision

### Decision and Follow-up Model

- Quellenrollen: `EntryPoint`, `SupportUnit`, `FixtureOrContent`,
  `GeneratorIntent`, `IntentionalOmission`
- Frameworkentscheidungen: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, `FollowUpHardening`
- Governance: `Applicable`, `N/A`, `Open`
- Showcase-Delta: `CompleteIn032`, `Stage2Required`
