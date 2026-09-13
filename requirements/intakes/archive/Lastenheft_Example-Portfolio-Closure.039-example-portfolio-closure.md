<!-- intake-authoring:begin -->
# Lastenheft: Abschluss des Beispielportfolio-Konformitätsaudits / Requirements Intake: Example Portfolio Conformance Closure

**Status:** ReadyForReview
**Zielgruppe / Audience:** Maintainer, Reviewer und Lernbegleitende des TuiVision-Beispielportfolios / maintainers, reviewers, and learning facilitators of the TuiVision example portfolio
**Vorausgesetztes Wissen / Assumed prior knowledge:** Grundverständnis von Repository-Tests und Markdown; Spec Kit wird bei der ersten Verwendung erklärt. / Basic repository-test and Markdown knowledge; Spec Kit is explained on first use.
**Profil / Profile:** `level2-lastenheft`

## Zweck / Purpose

Dieser unabhängige spätere Abschluss bestätigt erst nach dem gelieferten
Feature 038, dass das exakt 37 Einträge umfassende Beispielportfolio als Ganzes
konform und lernreif ist. Feature 038 darf diese abschließende Produktbehauptung
nicht selbst abgeben.

*This later independent closure confirms, only after Feature 038 has been
delivered, that the exact 37-entry example portfolio is conformant and ready
for learning. Feature 038 must not make that final product claim itself.*

## Aktueller Stand / Current State

Feature 038 prüft 37 kanonische Zeilen, 138 hashgebundene Source-Knoten, 128
Evidence-Knoten, zehn Dimensionen je Zeile und genau 46 malformed Fixtures. Der
eingefrorene Finding-Satz ist leer: keine Gap-Dimension, kein CandidateFinding,
kein ProductDecision und deshalb kein Remediation-Intake. Die vier möglichen
Owner-Gruppen sind unterdrückt. Der aktuelle Implementierungslauf startet
diesen Closure nicht.

*Feature 038 audits 37 canonical rows, 138 hash-bound source nodes, 128 evidence
nodes, ten dimensions per row, and exactly 46 malformed fixtures. Its frozen
finding set is empty, so no remediation intake exists. All four owner groups
are suppressed, and the current implementation run does not start this
closure.*

## Zielzustand / Target State

Ein späterer, eigenständiger Spec-Kit-Lauf revalidiert den gelieferten exakten
Feature-038-Stand, bestätigt die unveränderte Portfolio-Grundmenge und
Relationen und zeichnet erst dann die vollständige Portfolio-Konformität und
Lernreife nachvollziehbar auf.

*A later independent Spec Kit run revalidates the delivered exact Feature-038
state, confirms the unchanged population and relations, and only then records
full portfolio conformance and learning readiness.*

## Umfang / Scope

- Feature-038-Datensatz, neun fachliche Markdown-Projektionen und PR-Evidence
  gegen den gelieferten exakten Stand revalidieren.
- Exakt `EX001`–`EX037`, die Aufteilung 25/10/1/1, Source-/Evidence-
  Reziprozität und den leeren Finding-Satz erneut prüfen.
- Die vollständige Validierungsleiter und den Read-only-Produktscope erneut
  belegen.
- Eine eindeutige text-first Closure-Entscheidung und abschließende Evidence
  erstellen.

*Revalidate the Feature-038 dataset and projections, the exact population,
reciprocity, empty finding set, full validation ladder, and read-only product
scope; then create one text-first closure decision and its evidence.*

## Nicht-Ziele / Non-Goals

- Keine Runtime-, Public-API-, Dependency-, Paket-, Projekt-, Beispiel- oder
  historische Quelländerung.
- Keine neue Portierung, Härtung oder Behebung eines nicht vorhandenen Findings.
- Kein Start eines weiteren Features aus dem Closure-Lauf.
- Keine Übernahme von Commit-, Push-, PR-, Merge-, Bypass- oder Provider-
  Autorität ohne neue ausdrückliche Freigabe.

*No product, API, dependency, project, example, or historical-source change;
no new port or remediation; no next-feature start; and no inferred remote or
bypass authority.*

## Anforderungen / Requirements

- **FR-001:** Der Lauf MUSS den gelieferten Feature-038-Datensatz und seine
  bindenden Baseline-Hashes vor jeder Abschlussbehauptung revalidieren.
- **FR-002:** Der Lauf MUSS exakt 37 Zeilen in der Reihenfolge `EX001`–`EX037`
  und die Aufteilung 25/10/1/1 verlangen.
- **FR-003:** Jede Source-, Evidence-, Dimension-, Dispositions- und
  Finding-Relation MUSS vollständig und reziprok bleiben.
- **FR-004:** Der eingefrorene Satz MUSS weiterhin null Gap, null
  CandidateFinding und null ProductDecision enthalten; jede Drift stoppt
  fail-closed und hebt die Abschlussfähigkeit auf.
- **FR-005:** Alle 46 kanonischen malformed Fixtures MÜSSEN weiterhin jeweils
  genau einen stabilen EPA-Code erzeugen.
- **FR-006:** Die vollständige lokale Validierungsleiter einschließlich
  Coverage, Format, DocFX, Axe, Lynx, Security, Supply Chain, Agentenparität,
  Routing, Generated-Output und Scope MUSS bestehen.
- **FR-007:** `src/`, `examples/`, `tv203s/`, `TVDEMOS/` und `TVFM/` MÜSSEN
  gegenüber dem gebundenen Scope unverändert bleiben.
- **FR-008:** Erst nach FR-001 bis FR-007 DARF der Closure den Status
  `PortfolioConformantAndLearningReady` setzen und ihn von Audit- und
  Remediationstatus textuell unterscheiden.
- **FR-009:** Der Lauf DARF kein weiteres Feature starten und keine Remote-
  Autorität aus diesem Intake ableiten.

*The later run must revalidate the exact Feature-038 baseline, population,
relations, empty finding set, 46 diagnostics, full validation ladder, and
protected roots. Only then may it record `PortfolioConformantAndLearningReady`,
without starting another feature or inferring remote authority.*

## Qualität und Governance / Quality And Governance

Eingaben bleiben begrenzt, UTF-8-validiert und fail-closed. Der Closure erhält
keine neue Runtime-, Netzwerk-, Authentifizierungs-, Dependency- oder
Supply-Chain-Fläche. Deutsche Abschnitte stehen zuerst, englische direkt
danach, ungefähr CEFR-B2. Status, Abhängigkeiten, Entscheidungen, Fehler und
nächste Aktionen bleiben als geordneter Text für Screenreader, Braillezeilen
und Textbrowser verfügbar; Bedeutung hängt nicht von Farbe oder Position ab.

*Inputs remain bounded, UTF-8 validated, and fail-closed. The closure adds no
runtime, network, authentication, dependency, or supply-chain surface. German
comes first, followed by English at about CEFR B2. Status, dependencies,
decisions, failures, and next actions remain available as ordered text.*

## Abhängigkeiten und Risiken / Dependencies And Risks

Einzige fachliche Abhängigkeit ist der formal gelieferte exakte Feature-038-
Kandidat. Es gibt keine Remediation-Vorgänger. Provenienzdrift, ein neues
Finding, ein ProductDecision, eine unklare Ownership, eine geschützte
Pfadänderung oder ein Pflicht-Gate-Fehler stoppt. Die größte Fehlannahme wäre,
akzeptierte Vorgänger- oder lokale Evidence als neue Remote-Exact-Head-
Evidence auszugeben.

*The only functional dependency is the formally delivered exact Feature-038
candidate; there are no remediation predecessors. Provenance drift, a finding,
a product decision, ambiguous ownership, protected-root change, or mandatory
gate failure stops the run. Prior or local evidence must never be relabelled as
new exact-head remote evidence.*

## Erwartete Artefakte und Evidence / Expected Artifacts And Evidence

- aktualisierte, exakt synchronisierte Closure-, Gate- und PR-Evidence;
- maschinenlesbarer Abschlussstatus mit gebundenen Hashes;
- vollständiges Validierungsprotokoll mit ehrlichen lokalen und Remote-Grenzen;
- Scope-Diff, der null Produkt- oder historische Quelländerung belegt.

*Expected outputs are synchronized closure/gate/PR evidence, a hash-bound
machine-readable closure status, the full validation ledger, and a zero-product
scope diff.*

## Abnahmekriterien / Acceptance Criteria

- **AC-001:** Exakt 37 Zeilen, 25/10/1/1 und alle reziproken Relationen bestehen.
- **AC-002:** Exakt 46/46 malformed Fixtures bestehen mit eindeutigen EPA-Codes.
- **AC-003:** Finding-, ProductDecision- und Remediationanzahl sind weiterhin null.
- **AC-004:** Alle Pflicht-Gates der festen Validierungsleiter bestehen.
- **AC-005:** Geschützte Produkt- und historische Roots zeigen null Delta.
- **AC-006:** Genau eine unabhängige Closure-Evidence setzt erst danach
  `PortfolioConformantAndLearningReady`.
- **AC-007:** Kein Folgefeature ist gestartet; nicht ausgeführte Remote-Aktionen
  werden nicht als bestanden behauptet.

*Acceptance requires the exact population and relations, 46/46 diagnostics,
zero findings and remediation, every mandatory gate, zero protected-root
delta, exactly one final closure claim, and no started follow-up.*

## Annahmen und offene Fragen / Assumptions And Open Questions

Die leere Finding-Menge und die fehlenden Remediation-Abhängigkeiten sind
bindende Feature-038-Ergebnisse. Es gibt keine offene materielle Entscheidung.
Dieser Intake ist `ReadyForReview`, nicht bereits reviewed oder gestartet.

*The empty finding set and absence of remediation dependencies are binding
Feature-038 results. No material decision remains open. This intake is ready
for review, not reviewed or started.*

<!-- intake-authoring:prompts -->
## Copy-Ready Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
### Specify

```text
$speckit-specify requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md. Binde exakt diesen Intake und den formal gelieferten Feature-038-Kandidaten. Erzeuge nur die Spezifikation; implementiere nichts und führe keine Remote-Schreibaktion aus.
```

<!-- spec-kit-command-id: speckit.autonomous -->
### Autonomous

```text
$speckit-autonomous requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md mit Delivery-Authority LocalImplementation. Binde den dann akzeptierten Intake und starte kein Folgefeature. Commit, Push, PR, Review, Merge, Bypass, Provider-Administration und Secret-Zugriff sind nicht autorisiert.
```

<!-- intake-authoring:end -->
