# Feature Specification: Wave-5 Combined Delta Closure

**Feature Branch**: `034-wave5-combined-delta-closure`
**Created**: 2026-07-17
**Status**: Draft
**Binding Input**: `Lastenheft_19_Wave5-Combined-Delta-Closure.md`

## Klärungen / Clarifications

### Session 2026-07-17

- Keine formale Rückfrage ist erforderlich. Lastenheft 19 legt Feature,
  Liefermodus, Provenienz, Cardinalities, Entscheidungsmodell, Grenzen und
  Wave-Ergebnis vollständig fest.
- Maßgeblich ist die Vereinigung der geprüften Produktdateien aus PR #93 und
  PR #96. Die PRs #94 und #97 sind Abschluss-Evidence; PR #95 ist
  Prompt-Metadatenarbeit.
- Feature 034 ist ein read-only Audit. Es darf keine Produktlücke innerhalb
  des Audits beheben.
- Ein sauberer Abschluss darf einen Wave-6-Intake für Feature 035 ableiten,
  aber weder Feature 035 anlegen noch Wave 6 starten.
- Ein fokussierter zweiter Pass über Provenienz, Finding-Schwelle,
  Framework-Ownership, Normalstart-Proof und Ergebnisverzweigung fand keine
  verbleibende planwirksame Mehrdeutigkeit.

*No formal clarification is required. The binding intake completely fixes the
feature identity, provenance, decision vocabulary, cardinalities, boundaries,
and outcome rules. Feature 034 audits product delivery without remediating it.*

## Nutzungsszenarien und Prüfung / User Scenarios and Testing

### User Story 1 - Tatsächlichen Wave-5-Delta nachvollziehen (Priority: P1)

Als Maintainer möchte ich den tatsächlich geprüften Produktdelta der
funktionalen und sichtbaren Wave-5-Stufe getrennt von Abschluss- und
Metadatenänderungen nachvollziehen, damit die Freigabe nicht auf einem
zu breiten oder mehrdeutigen Vergleich beruht.

*As a maintainer, I want to reconstruct the reviewed product delta separately
from closeout and metadata changes so the release decision is based on the
right evidence.*

**Why this priority**: Ohne eindeutige Provenienz können spätere
Cardinality-, Proof- und Regressionsergebnisse nicht zuverlässig den
gelieferten Beispielen zugeordnet werden.

**Independent Test**: Die beiden Feature-PRs werden über exakte Basis-, Head-,
Merge- und Dateimengen rekonstruiert. Abschluss- und Prompt-PRs werden
separat klassifiziert; jede unerwartete oder fehlende Beziehung blockiert.

**Acceptance Scenarios**:

1. **Given** PR #93 und PR #96, **When** der Wave-5-Produktdelta
   rekonstruiert wird, **Then** stammen alle Produktpfade genau aus ihren
   geprüften Heads.
2. **Given** die PRs #94, #95 und #97, **When** ihre Rolle geprüft wird,
   **Then** werden Abschluss-Evidence und Prompt-Metadaten nicht als
   Produktdelta gezählt.
3. **Given** eine Commit-, Datei- oder Hash-Abweichung, **When** sie erkannt
   wird, **Then** wird keine Wave-5-Abschlussentscheidung veröffentlicht.

---

### User Story 2 - Zehn Beispiele gemeinsam abnehmen (Priority: P1)

Als Lernender oder Anwendungsentwickler möchte ich erkennen können, dass jedes
TP7-Beispiel seinen historischen Zweck, seine moderne C#-Funktion und seine
sichtbare Bedienung gemeinsam erfüllt.

*As a learner or application developer, I want every TP7 example to connect
historical intent, modern C# behavior, and visible operation in one reviewable
record.*

**Why this priority**: Feature 032 und 033 lieferten unterschiedliche
Qualitätsschichten. Erst ihre kombinierte Prüfung zeigt, ob die Anwendung
vollständig und verständlich ist.

**Independent Test**: Jedes der zehn Beispiele erhält eine vollständige Zeile
mit Quelle, Consumer, Funktionsproof, Showcase-Proof, Start- und Bedienpfad,
Framework-Nutzung, Dimensionsstatus und genau einer Hauptentscheidung.

**Acceptance Scenarios**:

1. **Given** ein `Tp7*`-Beispiel, **When** seine kombinierte Zeile geprüft
   wird, **Then** sind historische Absicht, Kernfunktion, sichtbarer erster
   Zustand, primäre Bedienung, Status, F1-Beschreibung und Beendigung belegt.
2. **Given** alle zehn Beispiele, **When** die Matrix ausgewertet wird,
   **Then** existieren genau zehn eindeutige Hauptentscheidungen.
3. **Given** einen offenen Dimensionsstatus `Gap`, **When** die Zeile
   akzeptiert werden soll, **Then** verhindern fehlende Finding-, Evidence-
   oder Owner-Angaben den Abschluss.

---

### User Story 3 - Framework-Nutzung und Proof-Qualität bewerten (Priority: P2)

Als Framework-Maintainer möchte ich unterscheiden, ob gemeinsame
Wave-5-Helfer reine Beispielkomposition sind oder Framework-Verhalten
duplizieren, damit wiederverwendbare Logik nicht dauerhaft in Beispielen
verborgen bleibt.

*As a framework maintainer, I want to distinguish pedagogical composition from
duplicated framework behavior so reusable logic does not remain hidden in
examples.*

**Why this priority**: Die Beispiele sollen moderne idiomatische
C#-Anwendungen bleiben, dürfen aber kein zweites examples-lokales Framework
bilden.

**Independent Test**: Die gemeinsame Wave-5-Komposition, ihre Zustandsmodelle
und alle Primary-Proofs werden gegen Framework-Verantwortung,
App-Loop-Ausführung, View-Identität und sichtbare Buffer-/Cell-Evidence
geprüft.

**Acceptance Scenarios**:

1. **Given** gemeinsame Beispielkomposition, **When** sie nur didaktische
   Anordnung und lokalen Zustand bündelt, **Then** darf sie als
   beispielspezifisch akzeptiert werden.
2. **Given** lokale Logik, **When** sie ein Framework-Verhalten ersetzt oder
   mehreren unabhängigen Beispielwellen dienen würde, **Then** wird ein
   reproduzierbares Candidate Finding erzeugt.
3. **Given** einen direkten Testhelfer, **When** er den realen
   Anwendungsloop ersetzt, **Then** darf er nicht als Primary-Proof gelten.

---

### User Story 4 - Wave-5-Abschluss und Wave-6-Folge festlegen (Priority: P3)

Als Projektverantwortlicher möchte ich eine eindeutige, evidence-basierte
Entscheidung über Wave 5 und den nächsten zulässigen Intake erhalten.

*As a project owner, I want an evidence-based Wave-5 closure decision and one
unambiguous next intake boundary.*

**Why this priority**: Wave 6 darf weder auf unvollständiger Grundlage
beginnen noch durch ein reines Prozessdetail dauerhaft blockiert bleiben.

**Independent Test**: Die Abschlusslogik akzeptiert nur vollständige,
widerspruchsfreie Matrizen und ausgelöste Gates. Sie erzeugt genau einen
sauberen Abschluss, Finding-basierte Folgearbeit oder einen
Produktentscheidungsstopp.

**Acceptance Scenarios**:

1. **Given** null Candidate Findings und null Product Decisions, **When** alle
   Gates bestehen, **Then** wird Wave 5 `Closed` und Wave 6
   `EligibleForIntake`.
2. **Given** mindestens ein Candidate Finding, **When** die Befunde
   dedupliziert werden, **Then** entstehen nur nicht leere ownerbezogene
   Hardening-Intakes und Wave 6 bleibt blockiert.
3. **Given** eine Product Decision, **When** sie erkannt wird, **Then** stoppt
   der Lauf ohne Remediation oder Wave-6-Intake.

### Randfälle / Edge Cases

- Ein PR-Pin existiert, aber seine erwartete Dateimenge weicht ab.
- Ein Pfad erscheint in beiden Feature-PRs mit unterschiedlicher Rolle.
- Eine der 15 Pascal-Quellen fehlt, ist doppelt oder seit Feature 032 geändert.
- Eine Consumer-Gruppe verweist auf kein Beispiel oder auf mehrere
  widersprüchliche Primärproofs.
- Ein Beispiel besitzt Funktionsproof, aber keinen Showcase-Abschluss oder
  umgekehrt.
- Ein Guide oder Startprojekt fehlt, obwohl die Beispielzeile akzeptiert ist.
- Eine Dimension verwendet einen unbekannten Status.
- Eine akzeptierte Zeile enthält ein `Gap`.
- Ein Candidate Finding besitzt keinen Owner, keine Evidence oder keine
  stabile ID.
- Eine `ProductDecision` wird fälschlich als normaler Abschluss behandelt.
- LF- und CRLF-Auswertung liefern unterschiedliche Cardinalities.
- Ein `--smoke`-Pfad besteht, während der normale Start oder ein
  Tastaturpfad nicht nachvollziehbar ist.
- Ein grüner Remote-Job hat den geforderten Scope nicht ausgeführt.
- Ein Reviewer ist wegen Quote oder Providergrenze nicht verfügbar.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Feature 034 MUSS Lastenheft 19 als bindenden Intake verwenden.
- **FR-002**: Der autoritative Produktdelta MUSS aus den exakten geprüften
  Dateimengen von PR #93 am Head
  `cf274c61968fdc5422d3c1cf16ed5488ad5d37ad` und PR #96 am Head
  `8921bd3f9e354b38835528442f950f53c9d925f0` bestehen.
- **FR-003**: Die Basis-, Head- und Merge-Pins beider Feature-PRs MÜSSEN vor
  jeder Abschlussaussage verifiziert werden.
- **FR-004**: PR #94 und PR #97 MÜSSEN als kausale Abschluss-Evidence und
  PR #95 MUSS als Nicht-Produkt-Metadatenänderung klassifiziert werden.
- **FR-005**: Ein pauschaler Basis-bis-`main`-Diff DARF NICHT als
  autoritative Produktmenge gelten.
- **FR-006**: Der Audit MUSS genau 15 historische Quellenrollen, sechs
  Consumer-Gruppen, zehn Beispiele, zehn funktionale Proofs, zehn
  Showcase-Abschlüsse und zehn Guide-/Launch-Pfade prüfen.
- **FR-007**: Die 15 historischen `TVDEMOS/*.PAS`-Blobs MÜSSEN zwischen dem
  Feature-032-Merge und dem finalen Audit-Head unverändert bleiben.
- **FR-008**: Jedes Beispiel MUSS genau eine Hauptentscheidung aus
  `AcceptedAsIs`, `AcceptedIntentionalDeviation`, `CandidateFinding` oder
  `ProductDecision` erhalten.
- **FR-009**: Jede Prüfdimension MUSS genau einen Status aus `Pass`,
  `IntentionalDeviation`, `Gap` oder `N/A` erhalten.
- **FR-010**: Eine akzeptierte Beispielzeile DARF KEINEN offenen `Gap`
  enthalten.
- **FR-011**: `AcceptedIntentionalDeviation` MUSS historische Absicht,
  moderne C#-Begründung, sichtbare Auswirkung, Restrisiko und
  Wiederbewertungsauslöser dokumentieren.
- **FR-012**: `CandidateFinding` MUSS eine stabile `W5D###`-ID,
  reproduzierbare Beobachtung, Evidence, Owner und Folgegrenze besitzen.
- **FR-013**: `ProductDecision` MUSS den Lauf ohne automatische Remediation
  oder Wave-6-Ableitung stoppen.
- **FR-014**: Ein Finding DARF NICHT allein auf einem stilistischen
  Unterschied zwischen Pascal und modernem C# beruhen.
- **FR-015**: Pro Beispiel MÜSSEN historische Quelle, Consumer,
  Feature-032-Proof, Feature-033-Proof, Einstiegspunkt, sichtbarer erster
  Zustand, primäre Bedienung, Framework-Nutzung und Safety-Grenze verbunden
  sein.
- **FR-016**: Jeder kombinierte Proof MUSS den normalen Start, mindestens
  einen primären Bedienpfad, Fokus oder Status, `F1` beziehungsweise
  `Help -> Description`, `Ctrl+Q` und den vorhandenen constrained-layout
  Proof berücksichtigen.
- **FR-017**: Primary-Proof MUSS `app.Run()` oder einen gleichwertigen realen
  Anwendungsloop sowie konkreten Zustand, View-Identität und sichtbare
  Buffer-/Cell-Evidence verbinden.
- **FR-018**: Direkte Helfer DÜRFEN nur `SetupOnly` oder
  `SupplementalProof` sein, wenn sie den realen Loop nicht ausführen.
- **FR-019**: `Wave5Application`, `Wave5ConsoleHost`, `Wave5StatusLine`,
  `Wave5GridView` und alle beispielspezifischen Zustandsmodelle MÜSSEN auf
  Framework-Duplikation geprüft werden.
- **FR-020**: Reine gemeinsame Beispielkomposition DARF unter
  `examples/Shared/` verbleiben.
- **FR-021**: Lokale Logik, die Framework-Verhalten ersetzt oder mehreren
  unabhängigen Beispielwellen dienen würde, MUSS als Finding disponiert
  werden und DARF im Audit nicht verschoben werden.
- **FR-022**: Free Vision, Terminal.GUI v1.9.0 und `magiblot/tvision` DÜRFEN
  nur bei einer neuen konkreten reproduzierbaren Wave-5-Frage erneut
  konsultiert werden.
- **FR-023**: Der Lauf DARF Runtime-Verhalten, öffentliche APIs,
  Dependencies, Pakete, Projekte, Solution, Beispielcode, Frameworkcode,
  `TVDEMOS/`, `TVFM/`, `tv203s/` oder externe Checkouts NICHT ändern.
- **FR-024**: Der Lauf DARF Feature-Artefakte, Evidence, Status- und
  Reihenfolgedokumente sowie deterministische test-only Closure-Validierung
  ergänzen.
- **FR-025**: Der Closure-Validator MUSS exakte Cardinalities, eindeutige
  Beziehungen, erlaubte Werte, vollständige Findings, Pins, Quellblobs,
  Pfade und LF-/CRLF-Parität fail-closed prüfen.
- **FR-026**: Negative Fälle MÜSSEN fehlende, doppelte, unbekannte,
  widersprüchliche, driftende und verfrühte Abschlussdaten einzeln ablehnen.
- **FR-027**: Learner-facing Dokumentation MUSS German-first/English-second,
  CEFR-B2, semantisch und text-first sein.
- **FR-028**: Alle gepflegten Agent-Kontexte MÜSSEN denselben aktiven
  Feature-, Scope- und Folgezustand nennen.
- **FR-029**: Die Feature-Evidence MUSS Entscheidungen, Commands, Ergebnisse,
  übersprungene Trigger, Restrisiken, Reviewergrenzen und Follow-ups
  vollständig dokumentieren.
- **FR-030**: Der Lauf MUSS gezielte positive und negative Closure-Tests,
  relevante Wave-5-Smokes, zehn kontrollierte Starts, zehn normale
  Interaktionspfade, vollständige Release-Tests, Coverage, Formatierung,
  DocFX/Axe, UTF-8, Plattform-, Paritäts-, Secret-, Supply-Chain-, Review-
  und Exact-Head-Gates nachweisen.
- **FR-031**: Vor jedem einzelnen `dotnet build` oder `dotnet test` MUSS der
  manuelle Build-Zähler genau einmal erhöht werden.
- **FR-032**: Ein Remote-Gate DARF nur als Pass gelten, wenn Workflow, Job,
  Plattform und tatsächlich ausgeführter Command den geforderten Scope
  abdecken.
- **FR-033**: Fehlende Reviewer oder Quota-Ausfälle MÜSSEN als fehlender
  Review und nicht als Pass dokumentiert werden.
- **FR-034**: Bei null `CandidateFinding` und null `ProductDecision` MUSS
  Wave 5 `Closed` und Wave 6 `EligibleForIntake` werden.
- **FR-035**: Ein sauberer Abschluss MUSS
  `Lastenheft_20_Wave6-TVFM-Functional-Porting.md` für ein späteres Feature
  035 ableiten, ohne Feature 035 anzulegen oder zu starten.
- **FR-036**: Bei Candidate Findings DÜRFEN nur deduplizierte, nicht leere,
  ownerbezogene Hardening-Intakes entstehen; Wave 6 MUSS blockiert bleiben.
- **FR-037**: Feature 034 DARF Wave 6, Feature 035 oder das Post-Wave-6-
  Portfolio-Audit NICHT starten.
- **FR-038**: Der Lauf MUSS Commit, Push, nicht leeren PR,
  Review-Konvergenz, Exact-Head-Evidence, Merge, Branch-Bereinigung und
  sauberen lokalen `HEAD == origin/main`-Sync nachweisen.
- **FR-039**: Ein kausaler Closeout DARF nur entstehen, wenn Post-Merge-Fakten
  nicht wahrheitsgemäß im reviewten Feature-Head stehen konnten.
- **FR-040**: Preset-Promotion DARF nur bei einem reproduzierbaren
  providerneutralen Defekt erfolgen; sonst MUSS die Retrospektive
  `NoPromotion` dokumentieren.

### Governance-Anforderungen / Governance Requirements

- **GR-001**: `security-governance` v0.6.0 MUSS NIST SSDF, CWE Top 25,
  Secrets, Supply Chain und Evidence-Integrität als `Applicable` behandeln.
  ASVS, SBOM/VEX/SLSA-Neuerzeugung, AI-SBOM und regulatorische Trigger bleiben
  ohne Scope-Änderung begründet `N/A`.
- **GR-002**: `architecture-governance` v0.5.0 MUSS
  Komponentenverantwortung, lokale Framework-Duplikation und
  Qualitätsgrenzen prüfen. Neue STRIDE/CIA/CAPEC-, Zero-Trust-, BSI-C3A-,
  BSI-C5-, S-ADR- oder arc42-Arbeit bleibt ohne Architektur- oder
  Cloudänderung begründet `N/A`.
- **GR-003**: `isaqb-architecture-governance` v0.2.0 MUSS Qualitätsziele,
  Komponentenverantwortung, Risiken und Technical Debt nachvollziehbar
  disponieren.
- **GR-004**: `a11y-governance` v0.4.0 MUSS Tastaturpfade, Fokus,
  Status/Description, constrained Layout, text-first Evidence,
  German-first/English-second CEFR-B2 und didaktischen Kommentarbedarf
  prüfen.
- **GR-005**: `cross-platform-governance` v0.2.0 MUSS Linux-, macOS- und
  Windows-Evidence prüfen. Skriptparität bleibt `N/A`, solange kein Skript
  geändert wird.
- **GR-006**: `agent-parity-governance` v0.3.0 MUSS alle fünf gepflegten
  Agent-Kontexte gemeinsam prüfen.
- **GR-007**: `autonomous-run-governance` v0.2.2 MUSS Evidence-first,
  Konvergenz, Run-State, Authority, Exact-Head-Gates, Review, Closeout und
  Retrospektive anwenden.

### Verfassungsanforderungen / Constitution Requirements

- **CR-001**: TuiVision MUSS als bestehendes Level-2-C#/.NET-Projekt behandelt
  werden.
- **CR-002**: A11Y-Evidence MUSS WCAG 2.2 AA anwenden, soweit Dokumentation
  oder UI-Proof betroffen sind, und sonst text-first bleiben.
- **CR-003**: Learner-facing oder gemeinsame Guidance MUSS DE-first,
  EN-second sein.
- **CR-004**: Statistik und Agent-Guidance MÜSSEN bei Statusänderungen
  gemeinsam geprüft und gegebenenfalls synchronisiert werden.
- **CR-005**: C# bleibt die primäre, auf der MSL-Allowlist akzeptierte
  Implementierungssprache; Feature 034 ergänzt nur test-only C#.
- **CR-006**: NIST SSDF und CWE Top 25 bleiben anwendbar; weitere
  Sicherheitsstandards erhalten triggerbasierte Entscheidungen.
- **CR-007**: ASVS ist `N/A`, weil kein Web-, API-, HTTP-, Auth- oder
  Session-Flow geändert wird.
- **CR-008**: Neue SBOM-, VEX- oder SLSA-Artefakte sind `N/A`, weil kein
  distributables Paket oder keine Dependency geändert wird.
- **CR-009**: AI bleibt ausschließlich Entwicklungswerkzeug; AI-SBOM ist
  `N/A`, solange keine Produkt- oder Runtime-KI entsteht.
- **CR-010**: Neue CAPEC- oder Zero-Trust-Arbeit ist `N/A`, weil keine
  Trust Boundary, kein externer Fluss und keine Servicearchitektur geändert
  wird.
- **CR-011**: Feature-lokale Evidence ist der begründete Governance-Ort;
  bestehende projektweite Security-Ledger bleiben unverändert.
- **CR-012**: Alle sechs Baseline-Presets und das optionale
  Autonomous-Run-Preset gelten in ihren triggerbasierten Grenzen.
- **CR-013**: Scope, Entscheidungsmodell und Follow-up-Grenzen sind
  ausdrücklich festgelegt; Delivery-Autorität bleibt in Run-Evidence und Plan.

### Schlüsselentitäten / Key Entities

- **Product Delta Record**: Exakter Basis-, Head-, Merge- und Dateimengenbezug
  einer Wave-5-Lieferstufe.
- **Historical Source Role**: Eine der 15 read-only Pascal-Quellen mit genau
  einer Feature-032-Rolle und einem stabilen Blob.
- **Consumer Group**: Eine der sechs `W5-00#`-Gruppen, die Quellen,
  Beispiele und Framework-Verträge verbindet.
- **Combined Example Row**: Genau eine vollständige Abnahmezeile je
  `Tp7*`-Beispiel.
- **Dimension Assessment**: Status einer einzelnen Qualitätsdimension.
- **Primary Disposition**: Genau eine abschließende Entscheidung je Beispiel.
- **Candidate Finding**: Reproduzierbare Lücke mit ID, Evidence, Owner und
  Folgegrenze.
- **Wave Outcome**: `Closed`, finding-blockiert oder
  produktentscheidungs-blockiert.

## Erfolgsmaße / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: Beide autoritativen Feature-PRs stimmen in Basis, Head, Merge
  und erwarteter Produktdateimenge vollständig.
- **SC-002**: Genau 15 Quellenrollen, sechs Consumer, zehn Beispiele, zehn
  Funktionsproofs, zehn Showcase-Abschlüsse und zehn Guide-/Launch-Pfade sind
  eindeutig verbunden.
- **SC-003**: Genau zehn kombinierte Beispielzeilen besitzen jeweils genau
  eine zulässige Hauptentscheidung.
- **SC-004**: Null akzeptierte Zeilen enthalten einen offenen `Gap`.
- **SC-005**: Jeder Candidate Finding besitzt zu 100 Prozent ID,
  reproduzierbare Evidence, Owner und Folgegrenze.
- **SC-006**: Alle zehn normalen Starts und kontrollierten Smokes belegen
  Kernfunktion, sichtbaren Zustand, Tastaturpfad, Beschreibung und
  kontrollierte Beendigung.
- **SC-007**: Alle Primary-Proofs führen den realen App-Loop aus und verbinden
  Zustand, View-Identität und sichtbare Zellen.
- **SC-008**: Alle gezielten und vollständigen Release-Tests bestehen; die
  fünf Framework-Assemblies behalten mindestens 70 Prozent Line Coverage.
- **SC-009**: DocFX baut mit null Warnungen und Fehlern; Playwright/Axe
  besteht.
- **SC-010**: Linux, macOS und Windows führen die anwendbaren Gates auf dem
  final reviewten Head erfolgreich aus.
- **SC-011**: Der finale Diff enthält null Produkt-, API-, Dependency-,
  Projekt-, Beispiel-, Framework-, historische oder externe Source-Änderung.
- **SC-012**: Der Lauf endet mit vollständigen Tasks, null umsetzbaren
  Review-Threads, gemergtem PR und sauberem lokalem `main == origin/main`.
- **SC-013**: Wave 5 besitzt genau einen belegten Abschlusszustand; Wave 6
  wird nicht automatisch gestartet.

## Annahmen / Assumptions

- PR #93 und PR #96 sind die einzigen autoritativen Produktlieferungen für
  Wave 5.
- Die 15 historischen Quellen, sechs Consumer und zehn Beispiele sind
  vollständig und stabil.
- Vorhandene Feature-032-/033-Tests und Evidence bleiben verwendbar, müssen
  aber unabhängig zusammengeführt und dürfen nicht nur zitiert werden.
- Gemeinsame Wave-5-Komposition ist zunächst beispielspezifisch; der Audit
  entscheidet anhand beobachtbarer Verantwortung statt anhand des Dateipfads.
- AI ist ausschließlich Entwicklungswerkzeug und keine Runtime-Komponente.
- Der aktuelle Auftrag erteilt `MergeAndSync`, aber keine Berechtigung zum
  automatischen Start des Folgefeatures.

## Scope-Grenzen / Scope Boundaries

### In Scope

- read-only Rekonstruktion des exakten Feature-032-/033-Produktdeltas
- kombinierte 15/6/10/10/10-Evidence und genau eine Entscheidung je Beispiel
- Framework-Usage-, Proof-, Guide-, A11Y-, Plattform- und Safety-Prüfung
- deterministische positive und negative test-only Closure-Validierung
- erforderliche Status-, Reihenfolge-, Statistik- und Agent-Paritätspflege
- abgeleiteter Wave-6-Intake nur bei sauberem Abschluss

### Out of Scope

- Produkt-, Runtime-, API-, Dependency-, Projekt-, Beispiel- oder
  Frameworkänderung
- Änderung historischer oder externer Vergleichsquellen
- Remediation eines Candidate Findings
- vollständige Wiederholung externer Vergleichsaudits
- Start von Wave 6, Feature 035 oder Post-Wave-6-Portfolio-Audit

### Decision and Follow-up Model

- Primary disposition: `AcceptedAsIs`, `AcceptedIntentionalDeviation`,
  `CandidateFinding`, `ProductDecision`
- Dimension: `Pass`, `IntentionalDeviation`, `Gap`, `N/A`
- Existing framework decision: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, `FollowUpHardening`
- Governance: `Applicable`, `N/A`, `Open`
- Proof role: `PrimaryProof`, `SupplementalProof`, `SetupOnly`
