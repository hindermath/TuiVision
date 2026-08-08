# Feature Specification: Wave-6 Combined Delta Closure

**Feature Branch**: `037-wave6-combined-delta-closure`
**Created**: 2026-08-08
**Status**: Clarified
**Binding Intake**: `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`

## Zielgruppe und Lernkontext / Audience and Learning Context

Das Audit richtet sich an Auszubildende, Maintainer und Reviewer, die den
zweistufigen Wave-6-Lauf nachvollziehen. Begriffe, Zustände, Abhängigkeiten und
Entscheidungen werden bei der ersten Verwendung erklärt. Deutsch ist die
Primärsprache; Englisch folgt bei erklärenden Blöcken. Vorwissen zu Spec Kit
wird nicht vorausgesetzt.

*The audit supports apprentices, maintainers, and reviewers who need to follow
the two-stage Wave-6 delivery. Terms, states, dependencies, and decisions are
explained on first use. German is primary and English follows in explanatory
blocks. Prior Spec Kit knowledge is not assumed.*

## User Scenarios & Testing

### User Story 1 - Verbindliches Wave-6-Delta rekonstruieren (Priority: P1)

Als Maintainer möchte ich ausschließlich die fachlichen Änderungen aus
Feature 035 und Feature 036 mit exakter Provenienz rekonstruieren, damit das
Audit weder fremde Metadatenänderungen einbezieht noch relevante Lieferpfade
übersieht.

**Independent Test**: Ein deterministischer Validator akzeptiert nur die
festgelegten Feature-/Closeout-Commits, Dateimengen, Eingabehashes und
historischen TVFM-Quellen; fehlende, doppelte oder veränderte Beziehungen
werden abgelehnt.

### User Story 2 - Funktion und Showcase gemeinsam bewerten (Priority: P1)

Als Reviewer möchte ich für jeden Wave-6-Consumer den funktionalen Nachweis aus
035 und den sichtbaren Showcase-Nachweis aus 036 gemeinsam prüfen, damit
Framework-Nutzung, Bedienbarkeit und Proof-Grenzen als ein Vertrag bewertet
werden.

**Independent Test**: Jede kombinierte Zeile besitzt eindeutige Consumer-,
Funktions-, Showcase-, Guide- und historische Zuordnungen sowie genau eine
Hauptentscheidung.

### User Story 3 - Findings fail-closed disponieren (Priority: P1)

Als Produktverantwortlicher möchte ich nur reproduzierbare Lücken als
`CandidateFinding` oder `ProductDecision` sehen, damit reine
Quelltextunterschiede keine unnötige Remediation auslösen.

**Independent Test**: Der Validator lehnt offene `Gap`-Dimensionen ohne
Finding, Findings ohne `W6D###`-ID/Evidence/Owner und unaufgelöste
Produktentscheidungen ab.

### User Story 4 - Wave-6-Abschluss wahrheitsgemäß entscheiden (Priority: P1)

Als Maintainer möchte ich Wave 6 nur bei vollständig grüner Closure-Evidence
schließen, damit der Post-Wave-6-Portfolioaudit erst nach belegtem Abschluss
startfähig wird.

**Independent Test**: `Closed` und die Freigabe des Portfolioaudits sind nur
bei null Candidate Findings, null Product Decisions und bestandenen Pflichtgates
zulässig; andernfalls bleibt der Zustand blockiert.

### Edge Cases

- Ein Feature-/Closeout-Commit oder eine gespeicherte Dateimenge driftet.
- Eine TVFM-Quelle, ein Consumer, ein Funktionsproof oder ein Showcase-Proof
  fehlt oder ist doppelt.
- Ein kombinierter Datensatz referenziert eine unbekannte Evidence-ID.
- Eine akzeptierte Zeile enthält eine Dimension `Gap`.
- LF-/CRLF-Unterschiede verfälschen textuelle Provenienz.
- Ein Finding besitzt keinen Owner, keine Evidence oder keinen
  Wiederbewertungsauslöser.
- Eine Plattform- oder A11Y-Grenze wird als Pass behauptet, obwohl nur ein
  Fallback nachgewiesen ist.
- Ein Produktproblem wird entdeckt, das außerhalb des read-only Audits liegt.

## Klärungen / Clarifications

### Pass 1 - 2026-08-08

- Die fachliche Delta-Menge ist die Vereinigung der Produktpfade aus PR #101
  und PR #104. PR #102 und #105 liefern Closeout-Evidence; PR #103 ist nur
  Intake- und Kontextmetadatenarbeit.
- Die kombinierte Matrix besitzt zehn Vertragsbereiche, die `W6-001` bis
  `W6-010` aus Feature 035 eindeutig mit `W6S-001` bis `W6S-010` aus Feature
  036 verbinden. Der einzige Einstiegspunkt ist `Tp7FileManager`.
- Die historische TVFM-Menge umfasst exakt 24 Dateien. Relevante
  `tv203s/`-Dateien erklären Framework-Absicht, erweitern aber nicht die
  normative Produktmenge.
- `LocalImplementation` darf den geprüften lokalen Kandidaten als
  `ReadyForDelivery` bewerten. Der tatsächliche Zustand `Closed` und die
  tatsächliche Portfolio-Freigabe bleiben bis zu einem später autorisierten
  Merge kausal ausstehend.

### Pass 2 - 2026-08-08

- Die normale PTY-Prüfung ist ein begrenzter Start- und Bedienpfad, kein
  visueller Pixelvergleich und keine interaktive Produktänderung.
- Fehlende aktuelle Ubuntu-/Windows-Providerläufe werden unter lokaler
  Autorität nicht als `Pass` erfunden. Sie bleiben nicht ausgelöste
  Delivery-Gates; die bereits gemergte Vorgänger-Evidence dient nur als
  ergänzende Provenienz.
- Zwei fokussierte Klärungspässe ergaben keine weitere Frage, deren Antwort
  Plan, Tasks, Validierungsstrategie oder Abnahme materiell verändern würde.

### Authority Update - 2026-08-08

- Nach dem fail-closed Stopp am veralteten Intake-Alignment-Guard hat der
  Auftraggeber die begrenzte Korrektur und die Fortsetzung im Modus
  `MergeAndSync` ausdrücklich autorisiert.
- Die neue Autorität ändert nur Delivery, Provider-Gates und den kausalen
  Closeout. Auditmenge, read-only Produktgrenze und alle fachlichen
  Entscheidungen bleiben unverändert.
- Ein Admin-Bypass ist nur zulässig, wenn alle technischen Gates grün sind,
  keine umsetzbaren Review-Threads offen sind und ausschließlich die
  Human-Approval-Regel verbleibt.

*Two focused clarification passes fixed the authoritative product-PR union,
the exact 24/10/10/10/1 cardinalities, the initial local causal boundary, and
provider-gate handling. The later authority update changes delivery only. No
material ambiguity remains for planning.*

## Requirements

### Functional Requirements

- **FR-001**: Das Feature MUSS das fachliche Produktdelta von Feature 035 und
  Feature 036 unabhängig von fremden Metadatenänderungen rekonstruieren.
- **FR-002**: Die Feature- und Closeout-Provenienz MUSS exakte Basis-, Head-,
  Merge-, Pfadlisten- und Set-Hash-Nachweise enthalten.
- **FR-003**: Alle bindenden Vorgängerartefakte MÜSSEN mit Pfad, Rolle und
  SHA-256 erfasst werden.
- **FR-004**: Historische Quellen unter `TVFM/` und relevante Referenzen unter
  `tv203s/` MÜSSEN read-only und hashgebunden geprüft werden.
- **FR-005**: Jeder erwartete Wave-6-Consumer MUSS genau einmal in der
  kombinierten Matrix erscheinen.
- **FR-006**: Jede kombinierte Zeile MUSS Funktionsproof, Showcase-Proof,
  Einstiegspunkt, ersten sichtbaren Zustand, primäre Bedienung, Fokus,
  StatusLine, F1-Description und Beendigungspfad abdecken.
- **FR-007**: Jede Zeile MUSS Framework-Nutzung und lokale Sonderlogik
  sichtbar voneinander trennen.
- **FR-008**: Die Dimensionen Verhalten, Interaktion, Layout, Proof,
  Dokumentation, A11Y, Plattform und Sicherheit DÜRFEN nur `Pass`,
  `IntentionalDeviation`, `Gap` oder `N/A` verwenden.
- **FR-009**: Jede Zeile MUSS genau eine Hauptentscheidung aus
  `AcceptedAsIs`, `AcceptedIntentionalDeviation`, `CandidateFinding` oder
  `ProductDecision` besitzen.
- **FR-010**: Ein `CandidateFinding` MUSS reproduzierbar sein und eine stabile
  `W6D###`-ID, Evidence, Owner, Restrisiko und Wiederbewertungsauslöser tragen.
- **FR-011**: Quelltextunterschiede allein DÜRFEN kein Finding begründen.
- **FR-012**: Findings und Produktentscheidungen DÜRFEN im Feature nicht durch
  Produktänderungen behoben werden.
- **FR-013**: Ein deterministischer test-only Validator MUSS positive
  Kardinalitäten und explizite negative Mutationen prüfen.
- **FR-014**: Der Validator MUSS fehlende, doppelte, unbekannte und
  widersprüchliche Beziehungen fail-closed ablehnen.
- **FR-015**: Textuelle Hash- und Provenienzprüfungen MÜSSEN die dokumentierte
  LF-/CRLF-Grenze plattformneutral behandeln.
- **FR-016**: Echte `app.Run()`-, View-, Fokus-, Dialog-, Dateiworkspace- und
  Buffer-/Cell-Pfade MÜSSEN aus den Vorgängern nachgewiesen werden.
- **FR-017**: Kontrollierte `--smoke`-Starts und normale PTY-Pfade der
  betroffenen Beispiele MÜSSEN nachweisbar bleiben.
- **FR-018**: Lokale Beispielsonderlogik MUSS auf verdeckte oder duplizierte
  Framework-Funktion geprüft werden.
- **FR-019**: Guides, Tastaturpfade und text-first A11Y-Evidence MÜSSEN mit dem
  kombinierten Verhalten übereinstimmen.
- **FR-020**: Wave 6 DARF nur bei null Candidate Findings, null Product
  Decisions und bestandenen Pflichtgates als `Closed` gelten.
- **FR-021**: Der Post-Wave-6-Portfolioaudit DARF erst nach wahrheitsgemäßem
  Wave-6-Abschluss `Eligible` werden.
- **FR-022**: Ein kausaler Closeout DARF nur für Fakten vorgesehen werden, die
  erst nach einem tatsächlichen Merge existieren können.
- **FR-023**: Feature 038 oder ein Remediation-Feature DARF nicht automatisch
  gestartet oder angelegt werden.
- **FR-024**: Das Feature DARF Runtime, öffentliche API, Abhängigkeiten,
  Projekte, Beispiele oder historische Quellen nicht verändern.
- **FR-025**: Evidence MUSS German-first/English-second, CEFR-B2 und text-first
  zugänglich sein.
- **FR-026**: Documentation Impact MUSS für jede geänderte Dokumentfamilie
  genau eine Entscheidung erhalten.

### Governance Requirements

- **GR-001**: NIST SSDF, CWE Top 25, Evidence-Integrität, Secrets,
  Supply-Chain-Prüfung und regulatorische Trigger MÜSSEN explizit bewertet
  werden.
- **GR-002**: STRIDE/CIA/CAPEC, S-ADR, arc42, Zero Trust, SAMM, BSI C3A und BSI
  C5 MÜSSEN mit anwendbarer Evidence oder begründetem `N/A` erscheinen.
- **GR-003**: WCAG 2.2 AA, text-first Evidence, bilinguale CEFR-B2-Erklärung
  und didaktischer Kommentarbedarf MÜSSEN geprüft werden.
- **GR-004**: Bash-/PowerShell-Parität MUSS bei Skriptänderungen gelten;
  andernfalls ist `N/A` mit Re-Evaluation-Trigger erforderlich.
- **GR-005**: Alle gepflegten Agent-Oberflächen MÜSSEN gemeinsam bewertet
  werden; Änderungen erfolgen nur bei einer tatsächlich neuen gemeinsamen
  Regel.
- **GR-006**: Autonomous-Run-State, Gate-Anforderungen, Tasks und Evidence
  MÜSSEN an jedem Phasenübergang konsistent bleiben.
- **GR-007**: `MergeAndSync` MUSS Commit, Push, PR, Exact-Head-Providerprüfung,
  Review-Konvergenz, Merge-Commit und lokale Main-Synchronisierung umfassen.
  Ein Admin-Bypass DARF ausschließlich die dokumentierte Human-Approval-Grenze
  nach grünen technischen Gates und null umsetzbaren Threads überbrücken.

## Success Criteria

- **SC-001**: 100 % der erwarteten Wave-6-Consumer und historischen Quellen
  besitzen genau eine vollständige Zuordnung.
- **SC-002**: Jede kombinierte Zeile besitzt genau eine Hauptentscheidung und
  neun gültige Dimensionswerte.
- **SC-003**: Alle positiven Closure-Tests und alle benannten negativen
  Mutationen bestehen.
- **SC-004**: Es bleiben keine unbekannten, fehlenden oder doppelten
  Beziehungen zurück.
- **SC-005**: Candidate Findings und Product Decisions sind entweder exakt
  null oder blockieren den Abschluss mit vollständiger Ownership-Evidence.
- **SC-006**: Keine ausführbare Produkt-, API-, Dependency-, Projekt-,
  Beispiel- oder historische Quellenänderung befindet sich im Diff.
- **SC-007**: Zielgerichtete Wave-6-Tests, vollständige Release-Tests und das
  kanonische Fünf-Assembly-Coverage-Gate bestehen.
- **SC-008**: Format-, DocFX-, Axe-, Secret-, Supply-Chain- und
  Agent-Paritätsgates besitzen aktuelle Ergebnisse oder begründete Trigger.
- **SC-009**: Ubuntu-, macOS- und Windows-Grenzen werden ehrlich als lokale
  Evidence oder nicht ausgelöste Provider-Gates dokumentiert.
- **SC-010**: Wave 6 und der Portfolioaudit besitzen jeweils genau einen
  wahrheitsgemäßen Abschlusszustand.
- **SC-011**: Alle Checklists sind vollständig; Analyze meldet keine Critical-,
  High- oder unbehandelte Medium-Findings.
- **SC-012**: Der autonome Lauf endet nach Feature-Merge und gegebenenfalls
  genau einem nicht rekursiven kausalen Closeout mit terminalem Run-State,
  vollständiger Retrospektive und sauberem synchronisiertem `main`.

## Assumptions

- Feature 035 und Feature 036 sind die vollständigen fachlichen Wave-6-Stufen.
- Vorgängerartefakte, historische Quellen und PR-Evidence bleiben read-only.
- Der Auditlauf kann Findings dokumentieren, aber keine Produktentscheidung
  stellvertretend treffen.
- Der Feature-Head darf Post-Merge-Zustände nicht als tatsächlich eingetreten
  behaupten; solche Fakten benötigen den benannten kausalen Closeout.

## Out of Scope

- Runtime-, API-, Paket-, Projekt- und Beispieländerungen
- Behebung entdeckter Findings
- Start oder Implementierung von Feature 038
- erneute vollständige Prüfung externer Vergleichsframeworks
- Änderungen unter `tv203s/`, `TVDEMOS/` oder `TVFM/`
- Remote-Aktionen außerhalb des benannten Feature- und kausalen
  Closeout-Pfads sowie jeder Bypass außerhalb der engen Human-Approval-Grenze
