# Feature Specification: TV203- und magiblot/tvision-Evolutionsaudit

**Feature Branch**: `030-tv203-magiblot-evolution-audit`
**Created**: 2026-07-16
**Status**: Draft
**Binding Input**: `Lastenheft_14_TV203-Magiblot-Evolution-Audit.030-tv203-magiblot-evolution-audit.md`

## Clarifications / Klärungen

### Session 2026-07-16

- Keine formale Rückfrage ist erforderlich. Das bindende Lastenheft und die
  gemergten Feature-029-Artefakte legen Quellenhierarchie, Pin und Lizenzgrenze,
  Vertrags- und Consumer-Baseline, Relations- und Beobachtungsvokabular,
  TG-/MB-Deduplizierung, Primary-Owner-Grenzen, deterministische
  Folge-Nummerierung, Wave-Sperre, Validierung und Stop-Bedingungen vollständig
  fest.
- Die absichtliche Unterbrechung ist ein Prozessnachweis mit genau einem
  zufällig gewählten, wiederanlauffähigen Phasenpunkt. Sie ändert weder
  Feature-Scope noch fachliche Akzeptanz und darf keine Operation absichtlich
  in einen nicht rekonstruierbaren Zustand bringen.
- Ein `ProductDecision`, nicht reproduzierbare Provenance, unklare
  Deduplizierung oder unklare Primary-Owner-Zuordnung bleibt ein echter
  Stop-Grund und wird nicht autonom geraten.

*No formal clarification is required. The binding intake and merged Feature
029 artifacts fully define source hierarchy, pin and license boundary,
contract and consumer baselines, relation and observation vocabularies, TG/MB
deduplication, primary-owner boundaries, deterministic follow-up numbering,
wave blocking, validation, and stop conditions. The intentional interruption
is one process proof at a randomly selected recoverable phase and does not
change feature scope or acceptance.*

## User Scenarios & Testing / Nutzungsszenarien und Prüfung

### User Story 1 - Akzeptierte Verträge gegen die direkte Evolution prüfen (Priority: P1)

Als Framework-Maintainer möchte ich alle nach Feature 029 akzeptierten
TuiVision-Verträge gegen den gepinnten Stand von `magiblot/tvision` prüfen,
damit Modernisierungsentscheidungen innerhalb der ursprünglichen C++-Linie
sichtbar werden, ohne diese Linie zur neuen Produktnorm zu erklären.

*As a framework maintainer, I want every contract accepted after Feature 029
reviewed against the pinned magiblot/tvision revision so modernization choices
within the original C++ lineage become visible without turning that lineage
into a new product norm.*

**Why this priority**: Die spätere TG-/MB-Deduplizierung ist nur belastbar,
wenn jeder bestehende Vertrag vollständig und nach derselben Quellenhierarchie
geprüft wurde.

**Independent Test**: Jeder akzeptierte Vertrag besitzt genau eine erlaubte
`magiblotRelation`, konkrete gepinnte Source-Evidence, TuiVision-Proof,
historischen Bezug, Consumer-Relevanz und eine Shared-Bias-Bewertung.

**Acceptance Scenarios**:

1. **Given** die akzeptierten Verträge aus Feature 029, **When** die Matrix
   geprüft wird, **Then** besitzt jeder Vertrag genau eine vollständige
   magiblot-Relation ohne unbekannte, doppelte oder fehlende ID.
2. **Given** eine gleiche C++-Form oder ein gleicher Klassenname, **When** keine
   reproduzierbare TuiVision-Lücke vorliegt, **Then** entsteht daraus kein
   Finding.
3. **Given** eine materielle Verhaltensabweichung, **When** sie bewertet wird,
   **Then** benötigt ein Candidate Finding zusätzliche TuiVision-, Consumer-
   oder Proof-Evidence.

---

### User Story 2 - Provenance und No-Copy-Grenze reproduzierbar sichern (Priority: P2)

Als Reviewer möchte ich jede Beobachtung auf den exakten gepinnten Commit,
Tree und mehrteiligen Lizenzkontext zurückführen können, damit der Audit
wiederholbar bleibt und keine externe C++-Quelle in TuiVision übernommen wird.

*As a reviewer, I want every observation traceable to the exact pinned commit,
tree, and multipart license context so the audit remains reproducible and no
external C++ source is copied into TuiVision.*

**Why this priority**: Ein beweglicher Branchstand oder eine vereinfachte
Lizenzaussage würde die Aussagekraft des Audits entwerten.

**Independent Test**: Das Quellenmanifest bestätigt Repository, Commit, Tree,
Zeitpunkt, Betreff, `COPYRIGHT`-Hash, geprüfte relative Pfade, eigene kurze
Verhaltenszusammenfassungen und den externen read-only Checkout.

**Acceptance Scenarios**:

1. **Given** das externe Repository, **When** Provenance geprüft wird, **Then**
   entsprechen Commit und Tree exakt den bindenden Werten.
2. **Given** das upstream `COPYRIGHT`, **When** der Lizenzkontext dokumentiert
   wird, **Then** bleiben Borland-Disclaimer, MIT-Anteile der Änderungen und
   Drittkomponentenhinweise unterscheidbar.
3. **Given** externe Quellen, Fixtures oder Build-Ausgaben, **When** der
   Lieferkandidat inventarisiert wird, **Then** ist davon nichts getrackt.

---

### User Story 3 - Consumer- und Proof-Grenzen für Wave 5 und 6 prüfen (Priority: P3)

Als künftiger Wave-Implementierer möchte ich wissen, ob die vorhandenen
Verträge und Proofs die realen `TVDEMOS/`- und `TVFM/`-Verbraucher weiterhin
tragen, damit die gesperrten Waves nicht auf unbewiesenen Annahmen aufbauen.

*As a future wave implementer, I need to know whether current contracts and
proofs still support the real TVDEMOS and TVFM consumers so the blocked waves
do not build on unproven assumptions.*

**Why this priority**: Architekturvergleich ohne Consumer-Bezug darf keine
Remediation oder Freigabe auslösen.

**Independent Test**: Jede akzeptierte Consumer-Zeile besitzt Verträge,
aktuelle Proof-Grenze, Wave-Relevanz, magiblot-Bezug, Entscheidung, Risiko und
Follow-up-Grenze.

**Acceptance Scenarios**:

1. **Given** die gemergte Consumer-Baseline, **When** sie read-only erneut
   geprüft wird, **Then** bleibt jede relevante Zeile vollständig zugeordnet.
2. **Given** eine bislang ungedeckte materielle Consumer-Verantwortung,
   **When** historische Absicht, TuiVision-Source, Proof und magiblot-Bezug sie
   bestätigen, **Then** darf ein neuer Vertrag nach dem aktuellen Höchstwert
   entstehen.
3. **Given** eine zusätzliche magiblot-Funktion ohne TuiVision-Consumer,
   **When** sie bewertet wird, **Then** wird sie nicht zum Finding.

---

### User Story 4 - TG- und MB-Beobachtungen deterministisch deduplizieren (Priority: P4)

Als Maintainer möchte ich jede Terminal.GUI- und magiblot-Beobachtung genau
einem kanonischen Finding oder einer begründeten Nicht-Finding-Entscheidung
zuordnen, damit dieselbe TuiVision-Lücke nur einmal umgesetzt wird.

*As a maintainer, I want every Terminal.GUI and magiblot observation mapped to
one canonical finding or justified non-finding decision so the same TuiVision
gap is implemented only once.*

**Why this priority**: Feature 030 ist die gemeinsame Deduplizierungsgrenze und
darf weder Beobachtungen verlieren noch spekulative Folgefeatures erzeugen.

**Independent Test**: Jede `TG*`- und `MB*`-Beobachtung besitzt genau einen
Deduplizierungsausgang; jedes `CF*`-Finding besitzt genau einen Primary Owner,
eine gemeinsame Reproduktion, Red-Proof, Real-Path-Green-Proof und einen
azyklischen Abhängigkeitsgraphen.

**Acceptance Scenarios**:

1. **Given** alle Feature-029-Handoff-Beobachtungen und neuen `MB001+`-Zeilen,
   **When** dedupliziert wird, **Then** ist jede Beobachtung genau einmal
   entschieden.
2. **Given** zwei Beobachtungen derselben TuiVision-Grenze, **When** sie
   zusammengeführt werden, **Then** entsteht höchstens ein `CF*`-Finding.
3. **Given** eine destruktive oder Breaking-Entscheidung, **When** sie als
   `ProductDecision` erkannt wird, **Then** stoppt der autonome Lauf.

---

### User Story 5 - Folgeintakes und Wave-Gate korrekt ableiten (Priority: P5)

Als Projektverantwortlicher möchte ich ausschließlich aus realen, nicht leeren
Finding-Ownergruppen Hardening-Lastenhefte und danach genau einen unabhängigen
Closure-Intake erhalten, damit Wave 5 und 6 erst nach nachgewiesener Schließung
weitergehen.

*As the project owner, I want hardening intakes generated only from real,
non-empty finding owner groups and exactly one independent closure intake
afterward so Wave 5 and Wave 6 proceed only after proven closure.*

**Why this priority**: Reihenfolge- und Gate-Fehler würden spekulative Arbeit
oder eine verfrühte Wave-Freigabe erzeugen.

**Independent Test**: Die Owner-DAG erzeugt deterministisch null oder mehrere
nicht leere Hardening-Lastenhefte ab Feature 031 und anschließend genau ein
Closure-Lastenheft; alle Statusoberflächen bleiben bis zu dessen Merge
blockiert.

**Acceptance Scenarios**:

1. **Given** null kanonische Findings, **When** Folgeintakes erzeugt werden,
   **Then** ist Feature 031 ausschließlich der Closure-Lauf.
2. **Given** nicht leere Ownergruppen, **When** ihre DAG sortiert wird, **Then**
   erhält jede Gruppe genau ein dependency-geordnetes Hardening-Lastenheft und
   zuletzt folgt genau ein Closure-Lastenheft.
3. **Given** den Abschluss von Feature 030, **When** Statusoberflächen geprüft
   werden, **Then** bleiben Wave 5 und Wave 6
   `BlockedPendingCombinedConformanceClosure`.

### Edge Cases / Randfälle

- Der gepinnte Commit ist erreichbar, aber der Tree oder `COPYRIGHT`-Hash
  stimmt nicht.
- Eine Quelle wird nur über einen Header verständlich, der nicht im Manifest
  steht.
- Ein Vertrag erhält mehrere magiblot-Relationen oder nur einen Typnamen ohne
  beobachtbare Verantwortung.
- `NotApplicable` besitzt keine Begründung oder keinen Re-Evaluationsauslöser.
- Direkte Abstammung wird fälschlich als unabhängige Bestätigung bewertet.
- Eine `MB*`-Beobachtung dupliziert eine `TG*`-Beobachtung mit anderer Sprache.
- Ein `CF*`-Finding besitzt mehrere Primary Owner oder einen Zyklus.
- Eine leere Ownergruppe erzeugt ein Lastenheft oder einen Leer-PR.
- Ein neuer Vertrag dupliziert eine bereits akzeptierte Grenze.
- Ein Consumer-Proof beweist nur einen Helper statt des realen Pfads.
- Ein externer Checkout, Build, Cache, Log oder generiertes Artefakt erscheint
  im Git-Inventar.
- Ein Reviewer ist nicht verfügbar; dies wird als fehlender Review und nicht
  als erfolgreicher Review erfasst.
- Eine Unterbrechung hinterlässt einen veralteten aktiven Zustand; ein normaler
  autonomer Start darf ihn nicht überschreiben.

## Requirements / Anforderungen

### Functional Requirements / Funktionale Anforderungen

- **FR-001**: Das Feature MUSS das bindende Lastenheft und die gemergten
  Artefakte der Features 024, 025, 026, 028 und 029 als Entscheidungseingabe
  behandeln.
- **FR-002**: Turbo Vision 2.0.3, Borland-Dokumentation und `tv203s/` MÜSSEN
  normative historische Primärquellen bleiben.
- **FR-003**: Akzeptierte TuiVision-Verträge, Public API und Nutzerverhalten
  MÜSSEN die aktuelle Produktsemantik bestimmen.
- **FR-004**: Free Vision und Terminal.GUI v1.9.0 MÜSSEN ausschließlich über
  ihre gemergte Evidence verwendet werden.
- **FR-005**: `magiblot/tvision` MUSS ausschließlich am Commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693` und Tree
  `96dd03873955689ff0a79f6c8107a8148fe1ebd6` geprüft werden.
- **FR-006**: Der mehrteilige Lizenzkontext und der `COPYRIGHT`-SHA-256
  `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548`
  MÜSSEN reproduzierbar nachgewiesen werden.
- **FR-007**: Externe Quellen MÜSSEN außerhalb des Repositorys read-only
  geprüft werden; kein Fork, Vendor, Submodul, Quelltextauszug oder Build-
  Artefakt darf eingecheckt werden.
- **FR-008**: Jeder akzeptierte Vertrag MUSS genau eine Relation
  `CorroboratesOriginal`, `CorroboratesModernization`,
  `AlternativeModernization`, `DivergesFromTuiVision` oder `NotApplicable`
  erhalten.
- **FR-009**: Jede Relation MUSS Source-IDs, Begründung, TuiVision-Proof,
  historischen Bezug, Consumer-Relevanz, Shared-Bias-Risiko und
  Re-Evaluationsgrenze enthalten.
- **FR-010**: `NotApplicable` MUSS eine konkrete Begründung und einen
  Re-Evaluationsauslöser besitzen.
- **FR-011**: C++-Form, Name, Vererbung, Speicherlayout oder
  Quelltextkompatibilität allein DÜRFEN KEIN Finding erzeugen.
- **FR-012**: Neue Verträge DÜRFEN nur für reale, bislang ungedeckte
  `TVDEMOS/`- oder `TVFM/`-Consumer-Verantwortungen entstehen.
- **FR-013**: Die Auswertung MUSS alle 14 bindenden Vergleichskapitel und alle
  akzeptierten Verträge vollständig abdecken.
- **FR-014**: Die Consumer-Matrix MUSS alle gemergten Wave-5- und Wave-6-
  Gruppen vollständig read-only prüfen.
- **FR-015**: Neue magiblot-Beobachtungen MÜSSEN bei `MB001` beginnen und alle
  Pflichtfelder aus dem Lastenheft besitzen.
- **FR-016**: Jede `MB*`-Beobachtung MUSS genau eine Entscheidung
  `CandidateFinding`, `IntentionalDeviation`,
  `AlreadySatisfiedWithNewEvidence`, `ProductDecision` oder
  `RejectedComparison` erhalten.
- **FR-017**: Jede offene `TG*`- und `MB*`-Beobachtung MUSS genau einem
  `CF001+`-Finding oder einer begründeten Nicht-Finding-Entscheidung zugeordnet
  werden.
- **FR-018**: Jedes `CF*`-Finding MUSS genau einen Primary Owner, eine
  gemeinsame Reproduktion, Required Red Proof, Required Real-Path Green Proof,
  Impact, Risiko und Abhängigkeiten besitzen.
- **FR-019**: Finding-Abhängigkeiten MÜSSEN azyklisch und topologisch
  sortierbar sein.
- **FR-020**: `ProductDecision`, unverifizierbare Provenance, Lizenzkonflikt,
  nicht deterministische Deduplizierung oder unklare Owner-Zuordnung MÜSSEN den
  Lauf blockieren.
- **FR-021**: Nur nicht leere Primary-Owner-Gruppen DÜRFEN genau ein
  Hardening-Lastenheft ab Feature 031 erzeugen.
- **FR-022**: Nach allen Hardening-Lastenheften MUSS genau ein unabhängiges
  Closure-Lastenheft erzeugt werden.
- **FR-023**: Wave 5 und Wave 6 MÜSSEN bis zum Merge des gemeinsamen Closure-
  Laufs blockiert bleiben.
- **FR-024**: Produkt-Runtime, Public API, Dependencies, Packages, Beispiele,
  `tv203s/`, `TVDEMOS/`, `TVFM/` und externe Quellen DÜRFEN NICHT verändert
  werden.
- **FR-025**: Teständerungen DÜRFEN ausschließlich Auditdaten-, Manifest-,
  Relations-, Deduplizierungs- und Reihenfolgeintegrität prüfen.
- **FR-026**: Feature-Evidence MUSS Quellen, Entscheidungen, Befehle,
  Ergebnisse, Skip-Trigger, Restrisiken, Reviews und Follow-ups dokumentieren.
- **FR-027**: Learner-facing Dokumentation MUSS German-first/English-second,
  CEFR-B2, text-first und WCAG-2.2-AA-orientiert sein.
- **FR-028**: Maintained Agent Guidance MUSS als synchronisierte Gruppe geprüft
  und nur bei gemeinsamer Status- oder Guidance-Änderung aktualisiert werden.
- **FR-029**: Pflichtenheft, Reihenfolge, Agent-Kontexte, Projektstatistik,
  Archivmarker und Feature-Evidence MÜSSEN denselben Gate- und Folgeintake-
  Zustand nennen.
- **FR-030**: Das Lastenheft MUSS nach fachlicher Abnahme über den Repository-
  Rename-Workflow archiviert werden.
- **FR-031**: Alle triggerbasierten lokalen, Plattform-, Security-, A11Y-,
  Agent-Paritäts-, Generated-Output- und Remote-Gates MÜSSEN nachgewiesen
  werden.
- **FR-032**: Der MergeAndSync-Abschluss MUSS einen reviewten exakten Head,
  null umsetzbare Threads, dokumentierte fehlende Reviews, Merge, Branch-
  Bereinigung und sauberen `main`-Sync beweisen.
- **FR-033**: Ein unterbrochener Lauf MUSS Status, implizite
  Fortsetzungsverweigerung, Authority-Revalidierung und expliziten Resume
  fail-closed unterstützen.
- **FR-034**: Kein Clarification-, TODO-, TBD-, Platzhalter-, offener Starter-
  oder widersprüchlicher Readiness-Eintrag DARF im finalen Kandidaten bleiben.

### Governance Requirements / Governance-Anforderungen

- **GR-001**: Die sechs Basis-Presets und `autonomous-run-governance` v0.2.2
  MÜSSEN mit Version, Applicability, Evidence, Owner, Reviewer, Ergebnis,
  Restrisiko und Re-Evaluationsauslöser erfasst werden.
- **GR-002**: NIST SSDF, CWE Top 25, STRIDE, CIA und CAPEC sind für Provenance,
  Datenintegrität, Deduplizierung, Scope- und Resume-Grenzen anwendbar.
- **GR-003**: ASVS, SBOM, VEX, SLSA, OpenSSF Scorecard und AI-SBOM sind `N/A`,
  solange keine Web-, Auth-, Dependency-, Paket-, Release- oder Runtime-AI-
  Grenze entsteht.
- **GR-004**: NIS2, CRA, EU AI Act und DORA sind `N/A`, solange keine neue
  regulierte Produkt-, Betreiber-, AI- oder Finanz-ICT-Rolle entsteht.
- **GR-005**: S-ADR, arc42-Sicherheitskonzept, Zero Trust, SAMM, BSI C3A und
  BSI C5 sind `N/A`, solange keine Produktarchitektur, Cloud-, Provider-,
  Deployment- oder verteilte Servicegrenze geändert wird.
- **GR-006**: iSAQB-Qualitäts-, Risiko-, Owner- und Technical-Debt-
  Nachverfolgbarkeit ist für Audit und Folgeintakes anwendbar.
- **GR-007**: A11Y-Governance ist für bilinguale, text-first Evidence,
  Consumer-A11Y und didaktische Kommentare in nicht trivialer Testlogik
  anwendbar.
- **GR-008**: Cross-Platform-Daten- und Pfadportabilität ist anwendbar;
  Script-Parität ist `N/A`, sofern keine Skripte geändert werden.
- **GR-009**: Agent-Parität ist für den gemeinsamen Gate- und Folgeintake-
  Status anwendbar; `.specify/templates/` ist `N/A`, sofern keine portable
  Regeländerung entsteht.
- **GR-010**: Autonome Zustands-, Authority-, Resume-, Kandidaten-, exakte
  Gate-, Review-, Merge- und Retrospektivgrenzen sind anwendbar.

## Success Criteria / Erfolgskriterien

- **SC-001**: 100 Prozent der akzeptierten Verträge besitzen genau eine
  vollständige magiblot-Relation.
- **SC-002**: Commit, Tree, Zeitpunkt, Betreff und `COPYRIGHT`-Hash stimmen
  exakt mit dem bindenden Pin überein.
- **SC-003**: 100 Prozent der akzeptierten Consumer-Zeilen besitzen vollständige
  Vertrags-, Proof-, Risiko- und Entscheidungsdaten.
- **SC-004**: 100 Prozent aller `TG*`- und `MB*`-Beobachtungen besitzen genau
  einen Deduplizierungsausgang.
- **SC-005**: Jedes `CF*`-Finding besitzt genau einen Primary Owner und einen
  azyklischen Proof- und Abhängigkeitsvertrag.
- **SC-006**: Es entstehen ausschließlich nicht leere, dependency-geordnete
  Hardening-Lastenhefte und genau ein letzter Closure-Intake.
- **SC-007**: Der finale Diff enthält null Runtime-, Public-API-, Dependency-,
  Package-, Beispiel-, Consumer-, historische oder externe Source-Änderungen.
- **SC-008**: Alle ausgelösten lokalen und remoten Gates bestehen; kein
  Critical-, High- oder undisponierter Medium-Fund bleibt.
- **SC-009**: Wave 5 und Wave 6 bleiben in allen gepflegten Oberflächen bis zum
  gemeinsamen Closure-Merge blockiert.
- **SC-010**: Der autonome Lauf endet nach genau einer absichtlichen
  Hard-Abort-/Resume-Probe mit validiertem `Completed`, `Retrospective`,
  `nextExactAction: N/A` und sauber synchronem `main`.

## Assumptions / Annahmen

- Feature 029 und sein vollständiger Handoff sind unverändert auf `main`
  verfügbar.
- Der gepinnte magiblot-Commit bleibt öffentlich abrufbar; ein lokaler externer
  Checkout darf für die Dauer des Audits bestehen.
- Der Audit baut magiblot/tvision nicht und installiert dafür keine
  Systempakete.
- Die aktuelle Vertragszahl bleibt 48, sofern kein realer ungedeckter Consumer
  einen neuen Vertrag nach den bindenden Regeln verlangt.
- Remote-Authority umfasst Commit, Push, PR, Review-Nacharbeit, den eng
  begrenzten Human-Approval-only Admin-Bypass, Merge, Branch-Bereinigung und
  lokalen `main`-Sync.

## Dependencies / Abhängigkeiten

- Gemergte Feature-Artefakte 024, 025, 026, 028 und 029.
- Read-only historische Quellen unter `tv203s/` und Consumer unter
  `TVDEMOS/` sowie `TVFM/`.
- Gepinnter externer `magiblot/tvision`-Commit.
- Installierte Sieben-Preset-Matrix und Spec Kit 0.12.11.

## Out of Scope / Nicht im Scope

- Sofortige Produktkorrekturen oder Public-API-Entscheidungen
- Wave-5- oder Wave-6-Portierung
- Breite Framework-Revision oder visuelle Remediation
- Kopie, Übersetzung, Vendorisierung oder Build von `magiblot/tvision`
- Neue Abhängigkeiten, Pakete, Dienste, Datenbanken oder Runtime-AI
- Neuer oder aktualisierter `github/spec-kit`-Issue während Feature 030
