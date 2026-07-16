# Feature Specification: TV203-, Free-Vision- und Terminal.GUI-Konformitätsaudit

**Feature Branch**: `029-tv203-freevision-terminalgui-conformance-audit`
**Created**: 2026-07-16
**Status**: Implemented
**Binding Input**: `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md`

## Clarifications / Klärungen

### Session 2026-07-16

- Keine formale Rückfrage ist erforderlich. Das bindende Lastenheft legt die
  drei Quellenperspektiven und Pins, alle 48 bestehenden Verträge, die 16
  Prüfdomainen, Consumer-Grenzen, Relation- und Finding-Vokabulare,
  Feature-030-Handoff, Wave-Sperre, Validierung und Stop-Bedingungen
  vollständig fest. Verbleibende technische Entscheidungen gehören in Plan
  und Tasks und ändern den akzeptierten Scope nicht.

*No formal clarification is required. The binding intake fully fixes source
perspectives and pins, all 48 existing contracts, the 16 review domains,
consumer boundaries, relation and finding vocabularies, the Feature 030
handoff, Wave blocking, validation, and stop conditions. Remaining technical
choices belong in planning and tasks without changing accepted scope.*

## User Scenarios & Testing / Nutzungsszenarien und Prüfung

### User Story 1 - Bestehende Frameworkverträge erneut prüfen (Priority: P1)

Als Framework-Maintainer möchte ich alle akzeptierten Verträge `C001` bis
`C048` gegen Turbo Vision 2.0.3, Free Vision und Terminal.GUI v1.9.0 prüfen,
damit eine dritte unabhängige Implementierungsmeinung bislang übersehene
Vertrags- oder Proof-Lücken sichtbar machen kann.

*As a framework maintainer, I want every accepted contract from `C001` through
`C048` reviewed against Turbo Vision 2.0.3, Free Vision, and Terminal.GUI
v1.9.0 so that a third independent implementation opinion can expose any
previously missed contract or proof boundary.*

**Why this priority**: Alle späteren Findings und Wave-Entscheidungen hängen
von einer vollständigen und nicht selektiven Vertragsprüfung ab.

**Independent Test**: Die Vertragsmatrix enthält genau eine vollständige
Terminal.GUI-Relation für jeden bestehenden Vertrag und weist jede Relation
auf konkrete gepinnte Quellen sowie vorhandenen TuiVision-Proof zurück.

**Acceptance Scenarios**:

1. **Given** die 48 akzeptierten Verträge, **When** die Auditmatrix validiert
   wird, **Then** besitzt jeder Vertrag genau eine erlaubte Terminal.GUI-
   Relation ohne Auslassung oder Duplikat.
2. **Given** eine architektonische Abweichung ohne reproduzierbare
   TuiVision-Lücke, **When** sie bewertet wird, **Then** entsteht allein daraus
   kein Finding.
3. **Given** eine Relation ohne gepinnte Quelle, Begründung, TuiVision-Proof
   oder Consumer-Relevanz, **When** die Integrität geprüft wird, **Then**
   schlägt das Audit sichtbar fehl.

---

### User Story 2 - Consumer- und Proof-Grenzen kritisch prüfen (Priority: P2)

Als künftiger Wave-5- oder Wave-6-Implementierer möchte ich wissen, ob die
bestehenden Frameworkverträge die realen `TVDEMOS/`- und `TVFM/`-Verbraucher
weiterhin tragen, damit Portierungsarbeit nicht auf unbewiesenen Annahmen
aufbaut.

*As a future Wave-5 or Wave-6 implementer, I need to know whether the existing
framework contracts still support the real `TVDEMOS/` and `TVFM/` consumers so
that porting work does not rely on unproven assumptions.*

**Why this priority**: Eine vollständige Vertragsmatrix reicht nicht, wenn
reale Consumer-Flows oder ihre Proof-Grenzen fehlen.

**Independent Test**: Jeder relevante Consumer oder benannte Flow erhält eine
vollständige Vertragszuordnung, aktuelle Proof-Grenze, Risikobewertung und
eindeutige Auditentscheidung.

**Acceptance Scenarios**:

1. **Given** die vollständigen Consumer-Familien, **When** sie read-only
   geprüft werden, **Then** ist jede relevante Zeile mit Vertrag, Proof,
   Risiko und Entscheidung erfasst.
2. **Given** eine noch nicht abgedeckte materielle Frameworkverantwortung,
   **When** historische Absicht, realer Consumer und aktueller TuiVision-Proof
   sie bestätigen, **Then** darf ein neuer Vertrag `C049+` entstehen.
3. **Given** ein Produkt-, API- oder Proof-Problem außerhalb des Audit-Scope,
   **When** es reproduziert wird, **Then** wird es als Finding oder
   Produktentscheidung geroutet und nicht in Feature 029 behoben.

---

### User Story 3 - Reproduzierbare Terminal.GUI-Evidence sichern (Priority: P3)

Als Reviewer möchte ich jede Terminal.GUI-Beobachtung auf den exakten
v1.9.0-Stand zurückführen können, ohne fremden Quelltext in TuiVision zu
kopieren, damit Provenance, Lizenz und No-Copy-Grenze prüfbar bleiben.

*As a reviewer, I want every Terminal.GUI observation traceable to the exact
v1.9.0 revision without copying external source into TuiVision so that
provenance, licensing, and the no-copy boundary remain auditable.*

**Why this priority**: Nicht gepinnte oder kopierte Vergleichsquellen würden
die Audit-Aussage und spätere Wiederholbarkeit entwerten.

**Independent Test**: Das Quellenmanifest bestätigt URL, Release, annotiertes
Tag-Objekt, aufgelösten Commit, MIT-Lizenz, geprüfte relative Pfade,
Prüfsummen, kurze eigene Zusammenfassungen und Abrufdatum.

**Acceptance Scenarios**:

1. **Given** das externe Repository, **When** Provenance geprüft wird, **Then**
   entsprechen Tag-Objekt und aufgelöster Commit exakt den bindenden Pins.
2. **Given** eine relevante Quell- oder Teststelle, **When** sie in Evidence
   aufgenommen wird, **Then** enthält das Repository nur Pfad, Hash,
   Kurzbeschreibung und optional einen Commit-Permalink.
3. **Given** externen Quelltext, Testdaten oder generierte Artefakte, **When**
   der Lieferkandidat geprüft wird, **Then** ist davon nichts in Git enthalten.

---

### User Story 4 - Findings und Feature-030-Handoff eindeutig machen (Priority: P4)

Als Maintainer möchte ich jede neue Beobachtung entweder als begründetes
Nicht-Finding oder als vollständig beschriebenes `TG*`-Finding an Feature 030
übergeben, damit die spätere magiblot-Prüfung ohne Spekulation deduplizieren
und Eigentümergrenzen bestimmen kann.

*As a maintainer, I want every new observation handed to Feature 030 either as
a justified non-finding or a complete `TG*` finding so that the later magiblot
review can deduplicate evidence and determine ownership without speculation.*

**Why this priority**: Feature 029 darf weder Findings verlieren noch
vorzeitig Hardening- oder Closure-Features erzeugen.

**Independent Test**: Der maschinenlesbare Handoff enthält alle Relationen,
Findings, Nicht-Findings, Owner-Vorschläge, Abhängigkeiten, Proof-Anforderungen
und stabilen Deduplizierungsschlüssel.

**Acceptance Scenarios**:

1. **Given** eine reproduzierbare Vertrags- oder Proof-Lücke, **When** sie
   erfasst wird, **Then** besitzt sie eine eindeutige `TG*`-ID, genau einen
   Primary Owner und alle Pflichtfelder.
2. **Given** eine vertretbare Modernisierungsabweichung, **When** sie bewertet
   wird, **Then** ist sie als Nicht-Finding mit Nutzer-, Consumer- und
   Proof-Begründung dokumentiert.
3. **Given** den finalen Handoff, **When** dessen Integrität geprüft wird,
   **Then** existiert kein findings-basiertes Hardening- oder
   Closure-Lastenheft und Feature 030 bleibt der einzige nächste Intake.

---

### User Story 5 - Wave-Gate und Governance konsistent halten (Priority: P5)

Als Projektverantwortlicher möchte ich einen überprüfbaren Auditabschluss über
Evidence, Governance, Agent-Kontexte und Projektstatus, damit Wave 5 und Wave 6
nicht durch widersprüchliche Marker vorzeitig freigegeben werden.

*As the project owner, I want an auditable result across evidence, governance,
agent context, and project status so that contradictory markers cannot release
Wave 5 or Wave 6 prematurely.*

**Why this priority**: Ein fachlich korrektes Audit ist unvollständig, wenn
Status-, Reihenfolge- oder Governance-Oberflächen davon abweichen.

**Independent Test**: Alle gepflegten Statusoberflächen nennen Feature 030 als
nächsten Intake, beide Waves bleiben blockiert und jede Governance-Entscheidung
besitzt vollständige Review- und Re-Evaluationsdaten.

**Acceptance Scenarios**:

1. **Given** ein abgeschlossenes Feature 029, **When** Statusoberflächen
   verglichen werden, **Then** nennen sie Feature 030 als einzigen nächsten
   Intake und halten Wave 5 sowie Wave 6 blockiert.
2. **Given** ein offenes `ProductDecision`, einen unverifizierbaren Pin oder
   eine unvollständige Consumer-Zuordnung, **When** das Gate bewertet wird,
   **Then** stoppt der Lauf mit konkretem Owner und Re-Evaluationsgrenze.
3. **Given** einen nicht ausgelösten Governance-Checkpoint, **When** er als
   `N/A` erfasst wird, **Then** enthält er Begründung und
   Re-Evaluationsauslöser.

### Edge Cases / Randfälle

- Eine Relation verweist auf einen Terminal.GUI-Typnamen, aber nicht auf
  beobachtbares Verhalten oder einen konkreten gepinnten Quellpfad.
- Ein Vertrag erhält versehentlich mehrere primäre Relationen.
- `NotApplicable` wird ohne Begründung oder Re-Evaluationsauslöser verwendet.
- Terminal.GUI weicht architektonisch ab, während TuiVision den historischen
  und modernen Vertrag bereits vollständig erfüllt.
- Eine Beobachtung betrifft nur eine zusätzliche Terminal.GUI-Funktion ohne
  realen TuiVision-Consumer.
- Ein möglicher neuer Vertrag dupliziert eine vorhandene Vertragsgrenze.
- Ein `TG*`-Finding besitzt mehrere Primary Owner oder einen zyklischen
  Abhängigkeitsgraphen.
- Ein Consumer wird ausgelassen, weil aktuell keine Implementierungsänderung
  erwartet wird.
- Ein vorhandener Test beweist nur einen Helper, nicht den behaupteten realen
  Frameworkpfad.
- Das externe Tag existiert, löst aber nicht auf den bindenden Commit auf.
- Ein externes Checkout, Cache, Log, Testresultat oder generiertes Dokument
  erscheint im Git-Inventar.
- Terminal.GUI v2, ein späterer Branch-Stand oder `magiblot/tvision` wird
  versehentlich in die Analyse aufgenommen.
- Eine Statusoberfläche gibt Wave 5 oder Wave 6 frei, obwohl Feature 030 und
  der gemeinsame Closure-Lauf noch fehlen.
- Ein Reviewer oder Provider ist nicht verfügbar; die Abwesenheit wird als
  fehlender Review und nicht als erfolgreicher Review erfasst.

## Requirements / Anforderungen

### Functional Requirements / Funktionale Anforderungen

- **FR-001**: Das Feature MUSS das bindende Lastenheft sowie die finalen
  Artefakte der Features 024, 025, 026 und 028 als unveränderliche
  Entscheidungseingabe behandeln.
- **FR-002**: Turbo Vision 2.0.3 und relevante Quellen unter `tv203s/` MÜSSEN
  die normative historische Primärquelle bleiben.
- **FR-003**: Free Vision am Commit
  `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` MUSS als bereits akzeptierte
  unabhängige Implementierungsmeinung erhalten bleiben.
- **FR-004**: Terminal.GUI MUSS ausschließlich aus Release und Tag `v1.9.0`,
  dem annotierten Tag-Objekt
  `4b812e44798f2c7567afec50ba9a9293b6beb6de` und dem aufgelösten Commit
  `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` geprüft werden.
- **FR-005**: Terminal.GUI v2, spätere v1-Stände, nicht gepinnte Branches und
  `magiblot/tvision` DÜRFEN NICHT Teil von Feature 029 sein.
- **FR-006**: Das Feature MUSS ein reproduzierbares Quellenmanifest mit URL,
  Tag, Tag-Objekt, Commit, Lizenz, Abrufdatum, relativen Pfaden, SHA-256,
  eigener Verhaltenszusammenfassung und No-Copy-Grenze führen.
- **FR-007**: Externe Quellen MÜSSEN außerhalb des getrackten Repositorys
  read-only geprüft werden; fremde Dateien, längere Auszüge, Testdaten und
  generierte Artefakte DÜRFEN NICHT eingecheckt werden.
- **FR-008**: Die Auditmatrix MUSS genau die bestehenden Verträge `C001` bis
  `C048` ohne Auslassung, Duplikat oder unbekannte ID enthalten.
- **FR-009**: Jeder bestehende Vertrag MUSS genau eine Terminal.GUI-Relation
  erhalten: `CorroboratesOriginal`, `CorroboratesModernization`,
  `AlternativeModernization`, `DivergesFromTuiVision` oder `NotApplicable`.
- **FR-010**: Jede Vertragsrelation MUSS konkrete Terminal.GUI-Quellen,
  Begründung, TuiVision-Proof, Consumer-Relevanz, Risiko und optional eine
  Finding-ID enthalten.
- **FR-011**: `NotApplicable` MUSS eine konkrete Begründung und einen
  Re-Evaluationsauslöser besitzen.
- **FR-012**: `DivergesFromTuiVision` DARF nur mit einer zusätzlich
  reproduzierbaren TuiVision-Vertrags-, Consumer-, Sicherheits-, A11Y-,
  Plattform- oder Realpfad-Proof-Lücke ein Finding erzeugen.
- **FR-013**: Abweichende Typnamen, Vererbung, statische oder instanzbasierte
  APIs, Renderingmodelle oder Zusatzfunktionen allein DÜRFEN KEIN Finding
  begründen.
- **FR-014**: Neue Verträge `C049+` DÜRFEN nur für eine materielle, noch nicht
  abgedeckte Frameworkverantwortung mit realem `TVDEMOS/`- oder
  `TVFM/`-Consumer, Quellenbezug, TuiVision-Proof und Duplikatsprüfung
  entstehen.
- **FR-015**: Die Prüfung MUSS alle 16 im Lastenheft definierten
  Frameworkdomänen vollständig abdecken.
- **FR-016**: Die Prüfung MUSS mindestens Application/MainLoop, Event- und
  Command-Verarbeitung, Fokus, View-/Window-/Dialog-Lebenszyklus, Layout,
  Rendering, Menüs, Status, Validation, FileDialog, Clipboard, Keyboard,
  Mouse, Driver und Fake-/Proof-Helfer umfassen.
- **FR-017**: Die Consumer-Matrix MUSS alle relevanten `TVDEMOS/`- und
  `TVFM/`-Dateien oder benannten Flow-Gruppen vollständig und read-only
  erfassen.
- **FR-018**: Jede Consumer-Zeile MUSS Vertrag, aktuellen Proof,
  Wave-Relevanz, Risiko, Follow-up-Grenze und genau eine Entscheidung
  enthalten: `UseExistingFramework`, `IntentionalDeviation`,
  `FollowUpHardening`, `CandidateFinding` oder `ProductDecision`.
- **FR-019**: Jede neue Beobachtung MUSS genau eine Finding-Entscheidung
  erhalten: `CandidateFinding`, `IntentionalDeviation`,
  `AlreadySatisfiedWithNewEvidence`, `ProductDecision` oder
  `RejectedComparison`.
- **FR-020**: Jedes `TG*`-Finding MUSS alle im bindenden Lastenheft genannten
  fachlichen, technischen, Proof-, Impact-, Ownership-, Review-,
  Restrisiko- und Deduplizierungsfelder enthalten.
- **FR-021**: Jedes Finding MUSS genau einen Primary Owner besitzen;
  Abhängigkeiten MÜSSEN als azyklischer Graph erfasst werden.
- **FR-022**: `ProductDecision`, unverifizierbare Provenance, Lizenzkonflikte,
  unvollständige Consumer-Zuordnung oder nicht reparierbare
  Auditintegrität MÜSSEN den Lauf blockieren.
- **FR-023**: Das Feature MUSS bidirektional maschinenprüfbare Relationen
  zwischen Verträgen, Quellen, Findings, Nicht-Findings, Consumern, Proofs,
  Ownern und Abhängigkeiten bereitstellen.
- **FR-024**: Der Feature-030-Handoff MUSS alle `TG*`-Beobachtungen,
  Nicht-Findings, Entscheidungen, Owner-Vorschläge, Abhängigkeiten,
  Proof-Anforderungen und stabilen Deduplizierungsschlüssel vollständig
  enthalten.
- **FR-025**: Feature 029 DARF keine findings-basierten Hardening- oder
  Closure-Lastenhefte erzeugen.
- **FR-026**: `Lastenheft_14_TV203-Magiblot-Evolution-Audit.md` und Feature 030
  MÜSSEN der einzige nächste Intake bleiben.
- **FR-027**: Wave 5 und Wave 6 MÜSSEN nach Feature 029 in allen gepflegten
  Statusoberflächen blockiert bleiben.
- **FR-028**: Produkt-Runtime, öffentliche APIs, Abhängigkeiten, Pakete,
  Beispiele, externe Quellen, `tv203s/`, `TVDEMOS/` und `TVFM/` DÜRFEN NICHT
  verändert werden.
- **FR-029**: Teständerungen DÜRFEN ausschließlich die Integrität der
  Auditdaten und Relationen stärken und KEINE Produktkorrektur verdecken.
- **FR-030**: Die maschinenlesbaren Daten MÜSSEN unbekannte, fehlende,
  doppelte, widersprüchliche, verwaiste oder zyklische Relationen sichtbar
  ablehnen.
- **FR-031**: Feature-Evidence MUSS jeden Quellenabruf, Auditentscheid,
  Validierungsbefehl, Ergebnis, Skip-Trigger, Restrisiko und Follow-up
  nachvollziehbar dokumentieren.
- **FR-032**: Umfangreiche learner-facing Dokumentation MUSS den
  German-first/English-second-, CEFR-B2-, text-first-, DocFX-,
  Playwright/Axe- und UTF-8-Textbrowser-Nachweis erfüllen.
- **FR-033**: Maintained Agent Guidance MUSS als synchronisierte Gruppe geprüft
  und nur geändert werden, wenn sich gemeinsamer Status oder gemeinsame
  Guidance ändert.
- **FR-034**: Pflichtenheft, Feature-Status, Agent-Kontexte,
  Projektstatistik, Archivmarker und Feature-Evidence MÜSSEN denselben
  nächsten Intake und dieselbe Wave-Sperre nennen.
- **FR-035**: Das abgeschlossene Lastenheft MUSS nach bestandener fachlicher
  Abnahme über den Repository-Rename-Workflow mit dem Feature-Branch
  archiviert werden.
- **FR-036**: Formatierung, gezielte Auditvalidator-Tests, vollständige
  Release-Tests, bedingtes Coverage, Dokumentation, A11Y, Secret-,
  Dependency-, Agent-Paritäts-, Generated-Output- und
  Protected-Source-Prüfungen MÜSSEN entsprechend ihren Triggern ausgeführt
  und dokumentiert werden.
- **FR-037**: Kein offener Klärungs-, Aufgaben- oder Platzhaltertext,
  unvollständiger Checklist-Punkt oder widersprüchlicher Readiness-Marker DARF
  im finalen Lieferkandidaten verbleiben.

### Constitution Requirements / Verfassungsanforderungen

- **CR-001**: TuiVision MUSS als registriertes Level-2-C#/.NET-Projekt die
  lokale Constitution, Agent Guidance, das bindende Lastenheft und die
  installierte Preset-Matrix verwenden.
- **CR-002**: C#/.NET bleibt die primäre memory-safe Implementierungssprache;
  C++, Pascal und externes C# dienen ausschließlich als read-only Evidence.
- **CR-003**: NIST SSDF und CWE Top 25 gelten für Provenance,
  Auditdatenintegrität, Validatoren, fail-closed Entscheidungen und
  Scope-Schutz.
- **CR-004**: OWASP ASVS ist `N/A`, solange keine Web-, HTTP-, Authentifizierungs-,
  Session- oder Servicegrenze entsteht; eine Scope-Änderung löst
  Re-Evaluation aus.
- **CR-005**: SBOM, VEX, SLSA, OpenSSF Scorecard und neue
  Release-Provenance-Arbeit sind `N/A`, solange keine Dependency, kein Paket
  und kein distributables Runtime-Artefakt geändert wird.
- **CR-006**: AI bleibt Entwicklungshilfe. AI-SBOM ist `N/A`, solange kein
  Modell, Datensatz, AI-Service, Inferenzsystem oder Runtime-AI-Baustein
  ausgeliefert oder betrieben wird.
- **CR-007**: STRIDE, CIA und CAPEC gelten für Evidence-Integrität,
  Quellen-Provenance, Relationenkonsistenz und fail-closed Gate-Entscheidungen.
- **CR-008**: S-ADR und arc42-Sicherheitskonzepte werden nur bei einer
  materiellen Architekturentscheidung ausgelöst; reine Auditmodellierung
  erzeugt keine neue Produktarchitektur.
- **CR-009**: Zero Trust, SAMM, BSI C3A, BSI C5 und Cloud-/Provider-Assurance
  sind `N/A`, solange keine Cloud-, Provider-, Deployment-, Trust- oder
  verteilte Servicegrenze geändert wird.
- **CR-010**: NIS2, CRA, EU AI Act und DORA bleiben trigger-basiert `N/A`,
  solange das Audit keine regulierte Betriebs-, Produkt-, AI- oder
  Distributionsgrenze verändert.
- **CR-011**: WCAG 2.2 AA, text-first Zugänglichkeit, bilingualer CEFR-B2-Text
  und didaktische Kommentarprüfung gelten für alle geänderten learner-facing
  Dokumente oder nicht-trivialen Validatoren.
- **CR-012**: Cross-Platform Governance gilt für portable Auditdaten,
  Pfadgrenzen und ausgeführte Validatoren; neue Bash-/PowerShell-Skriptparität
  wird nur ausgelöst, wenn ein Repository-Skript geändert oder ergänzt wird.
- **CR-013**: Alle fünf gepflegten Agent-Oberflächen müssen gemeinsam geprüft
  werden. `.specify/templates/` bleiben `N/A`, sofern kein ausdrücklich
  wiederverwendbarer Template-Bedarf entsteht.
- **CR-014**: `security-governance` v0.6.0,
  `architecture-governance` v0.5.0,
  `isaqb-architecture-governance` v0.2.0, `a11y-governance` v0.4.0,
  `cross-platform-governance` v0.2.0,
  `agent-parity-governance` v0.3.0 und
  `autonomous-run-governance` v0.2.1 MÜSSEN getrennt bewertet werden.
- **CR-015**: Autonomous Run Governance gilt für Evidence-first-Arbeit,
  Konvergenz, validierten Laufzustand, vollständige Gate-Anforderungen,
  Kandidatenprüfung, Berechtigungsgrenzen, Stop/Resume und Retrospektive.
- **CR-016**: Fachlicher Scope, Stop-Grenzen und Auditentscheidungen MÜSSEN von
  operativer Remote-Autorität getrennt bleiben; diese wird ausschließlich in
  Plan, Run-State und Evidence festgehalten.

### Governance Applicability / Governance-Anwendbarkeit

- **Security Governance v0.6.0**: Provenance, NIST SSDF, CWE Top 25,
  Auditdatenintegrität, Secret- und Dependency-Review sowie fail-closed
  Validatoren sind `Applicable`. ASVS, Supply-Chain-Erweiterungen, AI-SBOM und
  regulatorische Nachweise bleiben mit Scope-Triggern `N/A`.
- **Architecture Governance v0.5.0**: STRIDE, CIA, CAPEC,
  Qualitätsgrenzen, Consumer-Traceability und Risiko-DAG sind `Applicable`.
  S-ADR, arc42, Zero Trust, SAMM, BSI C3A und BSI C5 bleiben ohne materiellen
  Architektur-, Cloud- oder Provider-Trigger `N/A`.
- **iSAQB Architecture Governance v0.2.0**: Qualitätsziele,
  Architekturkontext, historische Absicht, Modernisierungsabwägung, Risiken
  und technische Schuld sind `Applicable`; breite Restrukturierung ist
  ausgeschlossen.
- **A11Y Governance v0.4.0**: Text-first Evidence, bilingualer CEFR-B2-Text,
  Consumer-A11Y-Grenzen und didaktische Kommentarprüfung für geänderte
  nicht-triviale Validatorlogik sind `Applicable`.
- **Cross-Platform Governance v0.2.0**: Portable Daten- und Pfadverträge sind
  `Applicable`; neue Skriptparität bleibt ohne Skriptänderung `N/A`.
- **Agent Parity Governance v0.3.0**: Die fünf gepflegten Agent-Oberflächen
  werden gemeinsam geprüft; Templates bleiben ohne akzeptierten
  wiederverwendbaren Bedarf `N/A`.
- **Autonomous Run Governance v0.2.1**: Laufzustand, Evidence-first,
  Konvergenz, Gate-Anforderungen, exakter Kandidat, Stop/Resume,
  Berechtigungsgrenzen und Retrospektive sind `Applicable`.

### Key Entities / Zentrale Entitäten

- **Terminal.GUI Source Entry**: Gepinnte Quellenreferenz mit ID, Pfad, Hash,
  Lizenzbezug, Verhalten, Testnavigation, Abrufdatum und No-Copy-Grenze.
- **Contract Relation Row**: Ein Vertrag `C001+` mit Domain, TuiVision-Proof,
  Consumer-Relevanz, Terminal.GUI-Quellen, genau einer Relation, Risiko und
  optionaler Finding-ID.
- **Consumer Review Row**: Ein `TVDEMOS/`- oder `TVFM/`-Consumer mit Vertrag,
  Proof, Wave-Relevanz, Entscheidung, Risiko und Follow-up-Grenze.
- **TG Observation**: Finding oder Nicht-Finding mit Reproduktion,
  Quellenrelationen, Proof-Grenze, Impact, Owner, Abhängigkeiten,
  Deduplizierungsschlüssel und Reviewdaten.
- **Feature-030 Handoff**: Maschinenlesbarer vollständiger Übergabestand aus
  Relationen, Beobachtungen, Owner-Vorschlägen, Abhängigkeiten und
  Proof-Anforderungen.
- **Governance Decision**: Preset-Checkpoint als `Applicable`, `N/A` oder
  `Open` mit Begründung, Evidence, Owner, Reviewer, Ergebnis, Restrisiko,
  Follow-up und Re-Evaluationsauslöser.
- **Validation Evidence**: Exakter Befehl oder Review, Scope, Ergebnis,
  Metrik, Fehlergrenze und Evidence-Pfad.

## Success Criteria / Erfolgskriterien

### Measurable Outcomes / Messbare Ergebnisse

- **SC-001**: Genau 48 bestehende Vertragszeilen decken `C001` bis `C048`
  vollständig ab; 100 Prozent besitzen genau eine erlaubte Terminal.GUI-
  Relation und alle Pflichtfelder.
- **SC-002**: Alle 16 Prüfdomainen und alle im Lastenheft benannten
  Terminal.GUI-Flows sind durch konkrete Quellen- und TuiVision-Proof-Evidence
  abgedeckt.
- **SC-003**: 100 Prozent der relevanten `TVDEMOS/`- und `TVFM/`-Consumer
  besitzen eine vollständige Review-Zeile; kein Consumer wird wegen erwarteter
  Nicht-Änderung ausgelassen.
- **SC-004**: Jeder neue Vertrag `C049+` erfüllt alle fünf
  Aufnahmebedingungen; andernfalls entstehen null neue Verträge.
- **SC-005**: Jede neue Beobachtung ist entweder ein vollständig beschriebenes
  `TG*`-Finding oder ein begründetes Nicht-Finding; keine Beobachtung bleibt
  unklassifiziert.
- **SC-006**: Das Terminal.GUI-Manifest bestätigt exakt Tag-Objekt, Commit,
  Lizenz und alle verwendeten Quellenhashes; null externe Dateien werden
  getrackt.
- **SC-007**: Bidirektionale Validatoren melden null unbekannte, fehlende,
  doppelte, verwaiste, widersprüchliche oder zyklische Relationen.
- **SC-008**: Der Feature-030-Handoff enthält 100 Prozent der Findings,
  Nicht-Findings, Owner-, Abhängigkeits-, Proof- und
  Deduplizierungsinformationen und erzeugt null vorzeitige Folge-Lastenhefte.
- **SC-009**: Der finale Diff enthält null Runtime-, Public-API-, Dependency-,
  Paket-, Beispiel-, externen Source-, historischen Source-, Wave-5- oder
  Wave-6-Verhaltensänderungen.
- **SC-010**: Alle ausgelösten Format-, Test-, Coverage-, DocFX-, A11Y-,
  Textbrowser-, Secret-, Dependency-, Agent-Paritäts-, Generated-Output- und
  Protected-Source-Gates bestehen.
- **SC-011**: 100 Prozent der Governance-Zeilen enthalten Applicability,
  Begründung, Evidence, Owner, Reviewer, Datum, Ergebnis, Restrisiko,
  Follow-up und Re-Evaluationsauslöser.
- **SC-012**: Feature 030 ist in allen gepflegten Statusoberflächen der einzige
  nächste Intake; Wave 5 und Wave 6 bleiben überall blockiert.
- **SC-013**: Im finalen Kandidaten verbleiben null offene Klärungs-,
  Aufgaben-, Platzhalter-, unvollständige Checklist- oder widersprüchliche
  Readiness-Einträge.

## Assumptions / Annahmen

- Feature 028 ist vollständig gemergt und sein Status
  `ReadyForTerminalGuiAudit` bleibt bindende Ausgangslage.
- Die Artefakte aus Features 024 bis 028 sind vollständig und ihre
  akzeptierten IDs bleiben stabil.
- Der gepinnte Free-Vision-Stand und Terminal.GUI v1.9.0 sind abrufbar; eine
  unverifizierbare Quelle blockiert das Audit.
- Bestehendes TuiVision-Verhalten wird voraussichtlich nicht geändert. Ein
  reproduziertes Produktproblem wird an Feature 030 übergeben oder als
  `ProductDecision` blockiert.
- Maschinenlesbare Auditdaten und Validatoren dürfen ergänzt werden, solange
  sie ausschließlich Evidence-Integrität prüfen.
- Umfangreiche Auditdokumentation ist learner-facing und löst den vollständigen
  DocFX-, A11Y- und Textbrowser-Pfad aus.

## Scope Boundaries / Scope-Grenzen

### In Scope

- Audit-Spezifikation, Planung, Tasks, Checklists und Feature-Evidence
- Gepinntes Terminal.GUI-v1.9.0-Quellenmanifest
- Vertrags-, Quellen-, Consumer-, Finding- und Governance-Matrizen
- Maschinenlesbare Auditdaten und bidirektionale Integritätsvalidatoren
- Vollständiger maschinenlesbarer Handoff an Feature 030
- Status-, Reihenfolge-, Agent-, Archiv- und Statistik-Synchronisierung
- Scope-proportionale lokale Validierung

### Out of Scope

- Runtime-, Public-API-, Paket-, Dependency- oder Produktverhaltensänderungen
- Neue Beispielportierung oder Wave-5-/Wave-6-Arbeit
- Produkt- oder Proof-Fixes innerhalb Feature 029
- Terminal.GUI-Fork, Portierung, Integration oder Quellkopie
- Terminal.GUI v2, nicht gepinnte Revisionen oder `magiblot/tvision`
- Änderungen an `tv203s/`, `TVDEMOS/`, `TVFM/`, Free Vision oder Terminal.GUI
- Findings-basierte Hardening- oder Closure-Lastenhefte
- Breite Framework-Neustrukturierung oder visuelle Remediation
- Generierte Dokumentation, Caches, Logs, Credentials, Testresultate oder
  externe Checkouts in Git

### Decision and Follow-up Model / Entscheidungs- und Follow-up-Modell

- Terminal.GUI-Relationen sind genau `CorroboratesOriginal`,
  `CorroboratesModernization`, `AlternativeModernization`,
  `DivergesFromTuiVision` oder `NotApplicable`.
- Finding-Entscheidungen sind genau `CandidateFinding`,
  `IntentionalDeviation`, `AlreadySatisfiedWithNewEvidence`,
  `ProductDecision` oder `RejectedComparison`.
- Consumer-Entscheidungen sind genau `UseExistingFramework`,
  `IntentionalDeviation`, `FollowUpHardening`, `CandidateFinding` oder
  `ProductDecision`.
- Governance-Entscheidungen sind genau `Applicable`, `N/A` oder `Open`.
- Beobachtungen außerhalb des akzeptierten Audit-Scope werden an Feature 030
  oder eine benannte spätere Eigentümergrenze übergeben und nicht lokal
  implementiert.

## Dependencies and Ordering / Abhängigkeiten und Reihenfolge

- Feature 029 beginnt erst nach dem gemergten und synchronisierten Feature 028.
- Feature 030 ist der einzige nächste Intake und verwendet den vollständigen
  Feature-029-Handoff.
- Erst Feature 030 dedupliziert `TG*`- und spätere `MB*`-Beobachtungen und
  erzeugt ausschließlich nicht leere Hardening-Gruppen sowie danach genau
  einen Closure-Lauf.
- Wave 5 bleibt bis zum gemeinsamen findings-basierten Closure-Merge blockiert.
- Wave 6 bleibt zusätzlich bis nach Wave 5 und einer erneuten Delta-Prüfung
  blockiert.
