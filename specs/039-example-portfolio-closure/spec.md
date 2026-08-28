# Feature Specification: Example Portfolio Closure

**Feature Branch**: `039-example-portfolio-closure`

**Created**: 2026-08-28

**Status**: Accepted for local implementation
**Input**: `requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md`

## Zweck / Purpose

Dieses Feature revalidiert den unveränderten, gelieferten Feature-038-Stand
und gibt genau eine unabhängige Abschlussentscheidung für das vollständige
Beispielportfolio ab. Es verändert weder Produktcode noch Beispiele,
Abhängigkeiten oder historische Quellen.

*This feature revalidates the unchanged delivered Feature 038 baseline and
records exactly one independent closure decision for the complete example
portfolio. It changes no product code, examples, dependencies, or historical
sources.*

## User Stories / Nutzungsszenarien

### US1 – Exakte Auditbasis revalidieren (P1)

Als Maintainer möchte ich den gelieferten Auditdatensatz erneut gegen seine
Integritätsverträge prüfen, damit keine Abschlussbehauptung auf Drift beruht.

**Akzeptanz**: Exakt `EX001` bis `EX037`, die Rollenverteilung 25/10/1/1,
138 Source-Knoten, 128 Evidence-Knoten und alle reziproken Relationen bestehen.

### US2 – Leeren Finding-Satz bestätigen (P1)

Als Reviewer möchte ich bestätigen, dass weiterhin keine Gap, kein
CandidateFinding, kein ProductDecision und kein Remediation-Intake existiert.

**Akzeptanz**: Jede Abweichung stoppt fail-closed; nur der unverändert leere
Satz ist abschlussfähig.

### US3 – Portfolioabschluss nachvollziehbar aufzeichnen (P1)

Als Lernbegleitender möchte ich eine text-first und maschinenlesbare
Abschlussentscheidung sehen, die Audit-, Remediation- und Closure-Status klar
trennt.

**Akzeptanz**: Erst nach allen lokalen Pflicht-Gates lautet der Status
`PortfolioConformantAndLearningReady`. Nicht autorisierte Remote-Gates werden
nicht als bestanden behauptet.

## Functional Requirements / Funktionale Anforderungen

- **FR-001**: Der Feature-038-Datensatz und seine gebundenen Artefakte werden
  über SHA-256 und die bestehende test-only Integritätsprüfung revalidiert.
- **FR-002**: Population, Reihenfolge, Rollenverteilung und Reziprozität müssen
  exakt erhalten bleiben.
- **FR-003**: Alle 46 malformed Fixtures müssen weiterhin jeweils mit ihrem
  stabilen `EPA`-Diagnosecode abgelehnt werden.
- **FR-004**: Finding-, ProductDecision- und Remediationmenge müssen null sein.
- **FR-005**: `src/`, `examples/`, `tv203s/`, `TVDEMOS/` und `TVFM/` bleiben
  gegenüber dem Ausgangs-HEAD unverändert.
- **FR-006**: Die lokale Release-, Test-, Coverage-, Format-, DocFX-, Axe-,
  Lynx- und Governance-Leiter muss bestehen.
- **FR-007**: Genau ein hashgebundener Closure-Datensatz dokumentiert Status,
  Metriken, Gates, Scope und Autoritätsgrenzen.
- **FR-008**: Commit, Push, PR, Merge, Bypass und Remote-Administration bleiben
  ohne zusätzliche Freigabe `NotAuthorized`.

## Nicht-Ziele / Non-Goals

Keine Runtime-, API-, Projekt-, Paket-, Beispiel-, Testvalidator- oder
historische Quellenänderung; keine Remediation und keine neue Produktsemantik.

## Success Criteria / Erfolgskriterien

- **SC-001**: 37/37 Einträge und 25/10/1/1 Rollen bestehen.
- **SC-002**: 138/138 Source- und 128/128 Evidence-Knoten bleiben reziprok.
- **SC-003**: 46/46 Negativ-Fixtures bestehen.
- **SC-004**: Findings, Product Decisions und Remediation-Intakes bleiben 0.
- **SC-005**: Alle anwendbaren lokalen Gates bestehen; Remote-Grenzen sind
  ehrlich ausgewiesen.
- **SC-006**: Geschützte Produkt- und historische Roots haben kein Delta.

## Clarifications / Klärungen

- Der ausdrücklich genehmigte Gesamtplan autorisiert nach dem unabhängigen
  Closure die getrennten Folgefeatures. Der Closure selbst startet sie nicht
  und leitet daraus keine Remote-Autorität ab.
- Die neue Quellenpolicy gilt prospektiv und ist deshalb nicht Bestandteil
  dieser Revalidierung.
