# Feature Specification: Gemeinsamer Konformitätsabschluss / Combined Conformance Closure

**Feature Branch**: `031-combined-conformance-closure`
**Created**: 2026-07-16
**Status**: Draft
**Binding Input**: `Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md`

## Klärungen / Clarifications

### Session 2026-07-16

- Keine formale Rückfrage ist erforderlich. Das bindende Lastenheft, der
  aktuelle Benutzerauftrag und die gemergten Vorgängerartefakte legen Scope,
  Mengen, Pins, Stop-Grenzen, Validierung, Delivery-Autorität und
  Wave-Reihenfolge vollständig fest.
- Der reviewte Feature-Head hält Wave 5 und Wave 6 gesperrt. Erst nachdem seine
  Exact-Head-Gates bestanden sind und der Feature-Merge extern nachgewiesen
  wurde, darf ein einzelner nicht rekursiver Evidence-Closeout Wave 5 auf
  `Eligible` und Wave 6 höchstens auf `ConditionallyReady` setzen.
- `Zero non-empty owner groups` erlaubt die drei bekannten leeren
  Owner-Schemazeilen, verlangt aber für jede eine leere Finding-Menge und
  verbietet daraus erzeugte Hardening-Intakes.
- Kann ein bindender externer Pin oder Source-Hash nicht reproduziert werden,
  wird nicht auf die ältere Manifestbehauptung vertraut. Der Lauf stoppt als
  blockiert, bis die Provenance wieder unabhängig nachweisbar ist.
- Feature 031 plant keinen weiteren absichtlichen Hard-Abort. Eine unerwartete
  Unterbrechung würde ausschließlich über read-only Status und expliziten
  Resume behandelt.

*No formal question is required. The reviewed feature head keeps both waves
blocked. Only a causal evidence closeout after exact-head validation and merge
may mark Wave 5 eligible and Wave 6 conditionally ready. Empty owner schema
rows remain valid only with empty finding sets, unavailable provenance blocks
closure, and no additional intentional interruption is scheduled.*

## Nutzungsszenarien und Prüfung / User Scenarios and Testing

### User Story 1 - Kombinierte Evidence unabhängig bestätigen (Priority: P1)

Als Framework-Maintainer möchte ich die zusammengeführte TV203-, Free-Vision-,
Terminal.GUI- und magiblot-Evidence auf dem gemergten Repository-Stand
unabhängig prüfen, damit Wave 5 nicht auf unvollständigen oder nur
fortgeschriebenen Audit-Aussagen beginnt.

*As a framework maintainer, I want the combined TV203, Free Vision,
Terminal.GUI, and magiblot evidence independently checked on the merged
repository baseline so Wave 5 does not start from incomplete or merely copied
audit claims.*

**Why this priority**: Jede spätere Wave-Freigabe hängt davon ab, dass die
akzeptierten Vertrags-, Consumer- und Beobachtungsmengen vollständig und
widerspruchsfrei bleiben.

**Independent Test**: Die Closure-Evidence enthält genau 48 Verträge, 13
Consumer-Gruppen, 48 TGO-Beobachtungen, 48 MB-Beobachtungen und 96 eindeutige
Dispositionen; alle Beziehungen sind vollständig und maschinell prüfbar.

**Acceptance Scenarios**:

1. **Given** die gemergten Artefakte der Features 024, 025, 026, 028, 029 und
   030, **When** die kombinierte Evidence geprüft wird, **Then** stimmen alle
   bindenden Mengen, IDs und Beziehungen exakt.
2. **Given** eine fehlende, doppelte oder unbekannte ID, **When** der
   Closure-Validator läuft, **Then** schlägt der Lauf sichtbar und fail-closed
   fehl.
3. **Given** eine nur zusammenfassende Aussage ohne zugehörigen Belegpfad,
   **When** die Evidence geprüft wird, **Then** erfüllt sie den
   Closure-Vertrag nicht.

---

### User Story 2 - Externe Quellenidentitäten reproduzierbar prüfen (Priority: P2)

Als Reviewer möchte ich die akzeptierten Free-Vision-, Terminal.GUI- und
magiblot-Identitäten und Dateihashes reproduzieren können, damit ein bewegter
Upstream-Stand die bisherige Konformitätsaussage nicht unbemerkt verändert.

*As a reviewer, I want to reproduce the accepted Free Vision, Terminal.GUI,
and magiblot identities and file hashes so an upstream change cannot silently
alter the existing conformance conclusion.*

**Why this priority**: Eine nicht reproduzierbare Quellenbasis entwertet alle
daraus abgeleiteten Beobachtungen.

**Independent Test**: Commit-, Tag-, Tree-, Lizenz- und Manifestwerte stimmen
mit den akzeptierten Pins überein; kein externer Quelltext oder Checkout wird
in TuiVision aufgenommen.

**Acceptance Scenarios**:

1. **Given** die akzeptierten externen Pins, **When** ihre Git-Objekte und
   Hashes geprüft werden, **Then** stimmen alle Identitäten exakt.
2. **Given** einen erreichbaren Commit mit abweichendem Tree, Lizenzhash oder
   Quelldateihash, **When** die Prüfung läuft, **Then** stoppt Feature 031.
3. **Given** externe Checkouts und temporäre Prüfdaten, **When** der
   Lieferkandidat inventarisiert wird, **Then** bleibt davon nichts getrackt.

---

### User Story 3 - Null-Finding-Ergebnis kritisch bestätigen (Priority: P3)

Als Projektverantwortlicher möchte ich bestätigen, dass kein kanonisches
Finding, keine Produktentscheidung und keine nicht leere Ownergruppe
unterdrückt wurde, damit die ausbleibende Remediation eine bewiesene
Entscheidung und keine Lücke in der Deduplizierung ist.

*As the project owner, I want confirmation that no canonical finding, product
decision, or non-empty owner group was suppressed so the absence of remediation
is a proven outcome rather than a deduplication gap.*

**Why this priority**: Ein fälschlich leeres Hardening-Portfolio würde Wave 5
auf einer ungeschlossenen Framework-Lücke aufbauen lassen.

**Independent Test**: Alle 96 Beobachtungen besitzen genau eine Disposition;
die Closure weist null `CF###`, null Produktentscheidungen, null nicht leere
Ownergruppen, null Abhängigkeitskanten und null Hardening-Intakes nach.

**Acceptance Scenarios**:

1. **Given** alle TGO- und MB-Dispositionen, **When** sie nach Contract,
   Reproduktion und Ownergrenze abgeglichen werden, **Then** bleibt jede
   Beobachtung genau einmal entschieden.
2. **Given** eine reproduzierbare TuiVision-Lücke, **When** sie erkannt wird,
   **Then** stoppt Feature 031 und erzeugt keine Produktkorrektur innerhalb
   dieses Laufs.
3. **Given** eine leere Ownergruppe, **When** die Intake-Logik geprüft wird,
   **Then** erzeugt sie weder Lastenheft noch Pull Request.

---

### User Story 4 - Wave-Freigabe kausal und widerspruchsfrei setzen (Priority: P4)

Als Maintainer möchte ich Wave 5 erst nach vollständig grüner lokaler,
plattformbezogener, remoter und reviewter Closure-Evidence auf `Eligible`
setzen, während Wave 6 höchstens `ConditionallyReady` wird, damit keine
Statusoberfläche die Lieferreihenfolge vorzeitig öffnet.

*As a maintainer, I want Wave 5 marked `Eligible` only after complete local,
platform, remote, and reviewed closure evidence, while Wave 6 remains at most
`ConditionallyReady`, so no maintained status surface opens the delivery order
prematurely.*

**Why this priority**: Ein grüner lokaler Audit allein beweist weder den
reviewten Head noch die plattformübergreifende Lieferfähigkeit.

**Independent Test**: Vor dem Feature-Merge bleiben beide Waves gesperrt. Nach
dem bewiesenen Merge nennen alle gepflegten Oberflächen Wave 5 `Eligible`, Wave
6 höchstens `ConditionallyReady` und keinen gestarteten Folge-Feature-Branch.

**Acceptance Scenarios**:

1. **Given** einen noch nicht gemergten oder nicht vollständig geprüften
   Kandidaten, **When** Wave-Marker geprüft werden, **Then** bleibt Wave 5
   blockiert.
2. **Given** alle bestandenen Gates und den gemergten Closure-Kandidaten,
   **When** der kausale Abschluss erfolgt, **Then** wird Wave 5 `Eligible`.
3. **Given** den erfolgreichen Feature-031-Abschluss, **When** Wave 6 bewertet
   wird, **Then** bleibt sie bis zum Abschluss von Wave 5 und dessen
   Delta-Review höchstens `ConditionallyReady`.

### Randfälle / Edge Cases

- Ein externer Commit ist erreichbar, aber Tag-Objekt, Tree oder Lizenzhash
  weicht ab.
- Eine Manifestdatei hat die richtige Zeilenzahl, aber eine doppelte oder
  unbekannte Source-ID.
- Ein Vertrag fehlt in einer Vergleichsmatrix, obwohl die Gesamtsumme durch
  eine doppelte Zeile weiterhin 48 beträgt.
- Eine TGO- oder MB-Beobachtung besitzt mehrere Dispositionen.
- Eine Ownergruppe existiert, enthält aber keine Finding-ID.
- Ein früher geschlossenes `F001`-`F013`-Finding reproduziert erneut.
- Ein Consumer-Proof verweist auf eine umbenannte oder nicht mehr vorhandene
  Testmethode.
- Ein grüner Workflowname enthält nicht den tatsächlich erforderlichen
  Acceptance-Befehl.
- Push- und Pull-Request-Ereignisse starten dieselben Jobs doppelt.
- Ein Reviewer ist wegen Quote oder Providergrenze nicht verfügbar.
- Ein Wave-Marker wird auf dem Feature-Head freigegeben, bevor dessen Merge
  kausal nachgewiesen ist.
- Ein externer Checkout, Cache, Log, TestResult oder generiertes DocFX-Artefakt
  erscheint im Git-Inventar.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Das Feature MUSS das bindende Lastenheft und die gemergten
  Evidence-Artefakte der Features 024, 025, 026, 028, 029 und 030 als
  unveränderliche Entscheidungseingabe behandeln.
- **FR-002**: Die Closure MUSS genau die 48 akzeptierten Verträge `C001` bis
  `C048` mit eindeutigen IDs, Domänen, Proofs und Vergleichsrelationen
  revalidieren.
- **FR-003**: Die Closure MUSS genau 13 geschützte Consumer-Gruppen enthalten:
  sechs für Wave 5 und sieben für Wave 6.
- **FR-004**: Genau 48 `TGO###`- und 48 `MB###`-Beobachtungen MÜSSEN vorhanden
  sein; jede Beobachtung MUSS genau eine kombinierte Disposition besitzen.
- **FR-005**: Die kombinierte Menge MUSS genau 96 eindeutige Dispositionen
  enthalten und bidirektional auf Beobachtung, Vertrag, Proof und Consumer
  verweisen.
- **FR-006**: Die Closure MUSS null kanonische `CF###`-Findings, null
  Produktentscheidungen, null nicht leere Ownergruppen, null
  Abhängigkeitskanten und null Hardening-Intakes bestätigen.
- **FR-007**: Leere Ownergruppen DÜRFEN als explizite Schemazeilen bestehen,
  MÜSSEN aber null Finding-IDs enthalten und DÜRFEN keinen Intake erzeugen.
- **FR-008**: Die abgeschlossenen Findings `F001` bis `F013` MÜSSEN weiterhin
  genau einer finalen Resolution und ihrem realen Proof zugeordnet sein.
- **FR-009**: Die Closure MUSS bestätigen, dass kein verpflichtendes
  Hardening-Lastenheft durch fehlende, doppelte oder falsch klassifizierte
  Evidence unterdrückt wurde.
- **FR-010**: Free Vision MUSS am Commit
  `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` und mit allen 15 akzeptierten
  Source-Hashes geprüft werden.
- **FR-011**: Terminal.GUI MUSS am Tag `v1.9.0`, Tag-Objekt
  `4b812e44798f2c7567afec50ba9a9293b6beb6de`, Commit
  `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`, MIT-Lizenzhash
  `2a7331c273b7c121f5e1f6f10e13d279a739ac310c49b56f2fb251d0490988d0`
  und mit allen 25 akzeptierten Source-Hashes geprüft werden.
- **FR-012**: magiblot/tvision MUSS am Commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693`, Tree
  `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, COPYRIGHT-Hash
  `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548`
  und mit allen 50 akzeptierten Source-Hashes geprüft werden.
- **FR-013**: Externe Quellen MÜSSEN außerhalb des Repositorys read-only
  geprüft werden; keine Quelle, Fixture, Übersetzung, Vendorisierung,
  Submodulreferenz oder Build-Ausgabe darf eingecheckt werden.
- **FR-014**: `tv203s/`, `TVDEMOS/` und `TVFM/` MÜSSEN read-only bleiben und
  dürfen ausschließlich als historische beziehungsweise Consumer-Evidence
  dienen.
- **FR-015**: Pin-Drift, fehlende oder doppelte Relation, ungelöste
  Beobachtung, unerwartetes Finding, Produktentscheidung, unklare
  Owner-Zuordnung oder ein fehlgeschlagenes Pflicht-Gate MUSS den Lauf
  fail-closed stoppen.
- **FR-016**: Feature 031 DARF keine entdeckte Produkt-, Runtime-, API-,
  Dependency-, Package-, Projekt-, Beispiel- oder Consumer-Lücke beheben.
- **FR-017**: Test-only Closure-Validatoren und negative Datenprüfungen DÜRFEN
  ergänzt werden, sofern sie ausschließlich bereits akzeptierte Evidence
  messen und keine Produktkorrektur verdecken.
- **FR-018**: Jede Closure-, Governance- und Validierungszeile MUSS Owner,
  Reviewer, Datum, Evidence-Pfad, Ergebnis, Restrisiko, Follow-up und
  Re-Evaluationsauslöser enthalten.
- **FR-019**: Governance-Anwendbarkeit MUSS ausschließlich `Applicable`, `N/A`
  oder `Open` verwenden; `N/A` benötigt Begründung und Trigger, `Open`
  zusätzlich Owner und konkretes Follow-up.
- **FR-020**: Der Lauf MUSS gezielte Audit-Validatoren, negative
  Schema-/Cardinality-Prüfungen, vollständige Release-Tests, das kanonische
  Coverage-Gate, Formatierung, DocFX/A11Y, Text-First-, Secret-, Scope-,
  Supply-Chain-, Agent-Paritäts- und Plattformnachweise ausführen.
- **FR-021**: Core, Controls, Serialization, Compatibility und
  Drivers.Console MÜSSEN jeweils mindestens 70 Prozent Line Coverage behalten.
- **FR-022**: Jeder Remote-Acceptance-Nachweis MUSS den exakten reviewten Head,
  Workflow, Job, Plattform und tatsächlich ausgeführten Command abbilden; ein
  grüner Name allein genügt nicht.
- **FR-023**: Fehlende Reviewer oder Quota-Ausfälle MÜSSEN als fehlender
  Review und nicht als erfolgreicher Review dokumentiert werden.
- **FR-024**: Wave 5 MUSS bis zu vollständig grünen lokalen, remoten,
  Review- und Exact-Head-Gates blockiert bleiben.
- **FR-025**: Wave 5 DARF erst nach dem wahrheitsgemäß nachgewiesenen Merge von
  Feature 031 auf `Eligible` gesetzt werden.
- **FR-026**: Wave 6 DARF nach Feature 031 höchstens `ConditionallyReady` sein
  und MUSS bis zum Abschluss von Wave 5 und dessen Delta-Review blockiert
  bleiben.
- **FR-027**: Wave-, Pflichtenheft-, Reihenfolge-, Agent-, Statistik-,
  Lastenheft- und Feature-Evidence-Oberflächen MÜSSEN denselben finalen Zustand
  nennen.
- **FR-028**: Falls Post-Merge-Fakten nicht wahrheitsgemäß auf dem reviewten
  Feature-Head stehen können, MUSS ein einzelner nicht rekursiver,
  evidence-only Closeout verwendet werden.
- **FR-029**: Maintained Agent Guidance MUSS als eine synchronisierte Gruppe
  geprüft und nur bei einer gemeinsamen Status- oder Guidance-Änderung
  angepasst werden.
- **FR-030**: `.specify/templates/` MUSS unverändert bleiben, sofern keine
  reproduzierbare portable Governance-Lücke nachgewiesen wird.
- **FR-031**: Das bindende Lastenheft MUSS nach erfolgreicher fachlicher
  Abnahme durch den Repository-Rename-Workflow archiviert werden.
- **FR-032**: Generierte `_site/`- und `api/*.yml`-Dateien, Caches, Logs,
  Credentials, Testausgaben, externe Checkouts und temporäre
  Exact-Head-Evidence MÜSSEN ungetrackt bleiben.
- **FR-033**: Der autonome Lauf MUSS seinen validierten State, akzeptierte
  Artefakthashes, Task-Fortschritt, Authority, letzte Operation und nächste
  exakte Aktion an jeder logischen Grenze pflegen.
- **FR-034**: Eine unerwartete Unterbrechung MUSS zuerst durch einen read-only
  Statuslauf rekonstruiert und anschließend ausschließlich durch expliziten
  Resume mit erneuter Authority-Prüfung fortgesetzt werden.
- **FR-035**: Der finale Lauf MUSS Commit, Push, Pull Request, Review-
  Konvergenz, Merge, Branch-Bereinigung und sauberen lokalen
  `HEAD == origin/main`-Sync nachweisen.
- **FR-036**: Feature 032, Wave 5 und Wave 6 DÜRFEN in diesem Lauf nicht
  gestartet werden.

### Governance-Anforderungen / Governance Requirements

- **GR-001**: `security-governance` v0.6.0 MUSS NIST SSDF, CWE Top 25,
  Evidence-Integrität, fail-closed Datenprüfung, Secrets und
  Supply-Chain-Baseline als `Applicable` bewerten. ASVS, neue SBOM-/VEX-/SLSA-/
  OpenSSF-Artefakte, AI-SBOM, NIS2, CRA, EU AI Act und DORA bleiben ohne
  Scope-Trigger `N/A`.
- **GR-002**: `architecture-governance` v0.5.0 MUSS STRIDE, CIA, CAPEC,
  Qualitätsgrenzen und Traceability als `Applicable` bewerten. S-ADR, arc42-
  Sicherheitskonzept, Zero Trust, SAMM, BSI C3A und BSI C5 bleiben ohne
  Architektur-, Cloud-, Provider-, Deployment- oder Serviceänderung `N/A`.
- **GR-003**: `isaqb-architecture-governance` v0.2.0 MUSS Qualitätsziele,
  Vertrags-/Consumer-Sichten, Risiken, bewusste Modernisierung und Technical
  Debt nachvollziehbar halten.
- **GR-004**: `a11y-governance` v0.4.0 MUSS bilinguale CEFR-B2-Evidence,
  semantische Struktur, Text-First-Nutzung und didaktische Kommentarprüfung
  für neue nicht triviale Testlogik anwenden.
- **GR-005**: `cross-platform-governance` v0.2.0 MUSS Linux-, macOS- und
  Windows-Evidence prüfen. Neue Skriptparität bleibt `N/A`, solange kein
  Skript geändert wird.
- **GR-006**: `agent-parity-governance` v0.3.0 MUSS alle fünf gepflegten
  Agentenoberflächen gemeinsam prüfen und Abweichungen verhindern.
- **GR-007**: `autonomous-run-governance` v0.2.2 MUSS Evidence-first,
  Konvergenz, State-Validierung, Authority, Exact-Head-Gates, Review,
  nicht rekursiven Closeout und Retrospektive anwenden.

### Schlüsselentitäten / Key Entities

- **Closure Run**: Eine unabhängige Prüfung mit Baseline-Commit, Reviewdatum,
  Quellenpins, Mengen, Gate-Status und finaler Wave-Disposition.
- **Contract Closure Row**: Ein `C001`-`C048`-Vertrag mit Domäne, historischen
  und modernen Relationen, Proof, Consumers, Risiko und Closure-Ergebnis.
- **Consumer Closure Row**: Eine der 13 Wave-Gruppen mit Verträgen, Proof,
  Wave-Relevanz, Entscheidung, Risiko und Folgegrenze.
- **Observation Disposition**: Eine `TGO###`- oder `MB###`-Beobachtung mit
  genau einer kombinierten Nicht-Finding- oder Finding-Entscheidung.
- **Owner Group**: Eine benannte Ownergrenze mit einer möglicherweise leeren
  Finding-ID-Menge; nur nicht leere Gruppen dürften einen Intake erzeugen.
- **Source Identity**: Repository, Git-Objekte, Lizenzhash und individuelle
  Source-Hashes einer akzeptierten externen Vergleichsquelle.
- **Acceptance Gate**: Stabiler Gate-Identifier mit Scope, Command-Tokens,
  Plattform, exaktem Head, Ergebnis und Evidence-Pfad.
- **Wave Disposition**: Kausaler Zustand für Wave 5 und Wave 6 mit Begründung,
  Voraussetzung und nächstem zulässigem Intake.

## Erfolgskriterien / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: Genau 48 eindeutige Vertragszeilen decken `C001` bis `C048`
  vollständig ab.
- **SC-002**: Genau 13 Consumer-Gruppen besitzen jeweils eine vollständige,
  eindeutige Closure-Zeile.
- **SC-003**: Genau 48 TGO- und 48 MB-Beobachtungen besitzen jeweils genau eine
  Disposition; die kombinierte Menge enthält genau 96 Zeilen.
- **SC-004**: Die Abschlussmenge enthält null kanonische Findings, null
  Produktentscheidungen, null nicht leere Ownergruppen, null
  Abhängigkeitskanten und null Hardening-Intakes.
- **SC-005**: Alle 15 Free-Vision-, 25 Terminal.GUI- und 50 magiblot-
  Source-Hashes sowie alle bindenden Git- und Lizenzidentitäten stimmen exakt.
- **SC-006**: `F001` bis `F013` bleiben vollständig geschlossen; kein
  erforderlicher Hardening-Intake ist unterdrückt.
- **SC-007**: Der finale Feature-Diff enthält null Runtime-, Public-API-,
  Dependency-, Package-, Projekt-, Beispiel-, Consumer-, historische oder
  externe Source-Änderungen.
- **SC-008**: Alle ausgelösten lokalen und remoten Gates bestehen; jede der
  fünf Coverage-Assemblies erreicht mindestens 70 Prozent Line Coverage.
- **SC-009**: 100 Prozent der Governance-Zeilen besitzen Applicability,
  Begründung, Evidence, Owner, Reviewer, Datum, Ergebnis, Restrisiko, Follow-up
  und Re-Evaluationsauslöser.
- **SC-010**: Vor dem Feature-Merge bleibt Wave 5 blockiert; nach dem kausal
  nachgewiesenen Abschluss ist Wave 5 überall `Eligible` und Wave 6 höchstens
  `ConditionallyReady`.
- **SC-011**: Kein `[NEEDS CLARIFICATION]`, TODO, TBD, Platzhalter, offener
  Startereintrag, undisponierter Medium-Fund oder umsetzbarer Review-Thread
  bleibt im finalen Kandidaten.
- **SC-012**: Der Lauf endet mit validiertem `Retrospective`, `Completed`,
  vollständigem Task-Zähler, `nextExactAction: N/A` und sauberem synchronem
  `main`.

## Annahmen / Assumptions

- Die Features 024, 025, 026, 028, 029 und 030 sind vollständig gemergt und
  ihre akzeptierten Evidence-Artefakte bleiben auf `main` verfügbar.
- Die bekannten Null-Finding- und Null-Intake-Ergebnisse sind die erwartete
  Baseline, aber keine vorweggenommene Acceptance.
- Ein externer Pin darf temporär außerhalb des Repositorys erneut ausgecheckt
  werden; er wird weder gebaut noch als Abhängigkeit verwendet.
- Test-only Evidence kann ergänzt werden, wenn eine bereits akzeptierte
  Invariante noch keinen deterministischen Closure-Validator besitzt.
- `MergeAndSync` ist für Commit, Push, PR, Review-Nacharbeit, eng begrenzten
  Human-Approval-only Bypass, Merge, Branch-Bereinigung und lokalen Sync
  autorisiert. Der Bypass ist nur zulässig, wenn alle technischen Gates grün
  sind, null umsetzbare Review-Threads verbleiben und Human Approval die
  einzige offene Regel ist.
- Es wird kein weiterer absichtlicher Unterbrechungstest geplant.

## Scope-Grenzen / Scope Boundaries

### Im Scope / In Scope

- Unabhängige Closure-Evidence für alle bindenden Vorgängerartefakte
- Reproduzierbare externe Pin- und Manifestprüfung
- Deterministische test-only Cardinality-, Schema-, Relation- und
  No-Suppression-Validierung
- Vollständige lokale, Plattform-, Security-, A11Y-, Agent- und Remote-Gates
- Kausale Wave- und Reihenfolge-Synchronisierung
- Lastenheft-Archivierung, Projektstatistik und Feature-Retrospektive

### Nicht im Scope / Out of Scope

- Produkt-, Runtime- oder Public-API-Korrekturen
- Neue Dependencies, Packages, Projekte oder Services
- Wave-5- oder Wave-6-Implementierung
- Änderungen an Beispielen oder Consumer-Quellen
- Änderungen unter `tv203s/`, `TVDEMOS/`, `TVFM/` oder externen Checkouts
- Breite Framework-Revision oder visuelle Remediation
- Neuer Feature-032-Branch oder ein automatisch gestarteter Folge-Lauf
- Preset-Änderung ohne reproduzierbare provider-neutrale Prozesslücke

### Entscheidungs- und Follow-up-Modell / Decision and Follow-up Model

- Contract-, Consumer- und Beobachtungsentscheidungen bleiben unverändert aus
  den akzeptierten Vorgängerartefakten.
- Governance verwendet ausschließlich `Applicable`, `N/A` und `Open`.
- Ein reproduziertes Produktproblem wird als blockierendes externes Follow-up
  dokumentiert und nicht in Feature 031 behoben.
- Wiederverwendbare autonome Prozessverbesserungen werden als
  `PresetFollowUp` bewertet; ohne reproduzierbare portable Lücke gilt
  `NoPromotion` ohne Branch, Leer-PR oder Preset-Release.
