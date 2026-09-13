# Feature Specification: Evidence-Qualitaetsaudit 044-046

**Feature Branch**: `047-evidence-quality-audit`  
**Created**: 2026-09-13  
**Status**: Accepted  
**Binding Input**: `requirements/intakes/active/Lastenheft_24_Evidence-Quality-Audit-Features-044-046.md`  
**Accepted Review**: Single review `9d9adb39-05a9-405e-953f-ad3e70b690c0` (`Ready`)  
**Delivery Authority**: `MergeAndSync`; granted after the completed local audit.

## Zweck / Purpose

Feature 047 prueft unabhaengig, ob die Abschlussaussagen der Features 044, 045
und 046 durch konkrete, aktuelle und angemessen tiefe Evidence getragen werden.
Es veraendert weder die geprueften Artefakte noch Produktcode und erzeugt keine
Folge-Intakes.

Feature 047 independently checks whether the closure claims of Features 044,
045, and 046 are supported by concrete, current, and sufficiently deep
evidence. It changes neither audited artifacts nor product code and creates no
follow-up intakes.

## User Stories

### US1 - Vollstaendige Evidence-Abdeckung (P1)

Reviewer koennen alle 12 Feature-044-Kontrollen sowie alle 157 kanonischen
Kontrollen aus 045/046 auf genau eine Qualitaetsentscheidung zurueckverfolgen.

**Acceptance**: Keine ID fehlt oder ist doppelt; jede Zeile verweist auf Claim,
Evidence, Begruendung und gegebenenfalls genau ein dedupliziertes Finding.

### US2 - Konkrete Hinweise ehrlich klassifizieren (P1)

Reviewer erkennen, ob Paket-, Workflow-, Laufzeit- und Reviewhinweise offen,
widerlegt, doppelt, ueberholt oder durch Evidence getragen sind.

**Acceptance**: Coverlet, MSTest, mutable Workflow-Referenz, Node-Version und
Nullsekunden-Review besitzen jeweils eine nachvollziehbare Entscheidung.

### US3 - Finding-Register ohne vorbestimmtes Ergebnis (P1)

Maintainer erhalten ein kanonisches Register, dessen Summen aus den tatsaechlich
erfassten Findings berechnet werden. Findings werden nicht behoben und erzeugen
keine automatische Folgearbeit.

**Acceptance**: Stabile `EQA###`-IDs, Kategorien, Severity, Owner,
Re-Evaluation-Trigger und Evidence-Pfade sind vollstaendig; null Findings ist
nur bei wirklich leerem Register moeglich.

### US4 - Lern- und barrierearme Darstellung (P2)

Auszubildende und Maintainer koennen Auditgrenzen, Findings und Restrisiken in
semantischem, text-first Markdown nachvollziehen.

**Acceptance**: Deutsch steht vor Englisch, CEFR-B2 wird angestrebt, und keine
Bedeutung haengt nur von Farbe, Symbolen oder Layout ab.

## Functional Requirements

- **FR-001**: Der Audit MUSS die 12 Feature-044-Kontrollen vollstaendig pruefen.
- **FR-002**: Der Crosswalk MUSS exakt 157 eindeutige `CL-XX-NN`-Zeilen enthalten.
- **FR-003**: Jede Auditzeile MUSS genau eine Entscheidung aus `Supported`,
  `OpenBoundary`, `EvidenceGap`, `Contradicted`, `Duplicate`, `Superseded` besitzen.
- **FR-004**: Findings MUESSEN genau eine Kategorie aus `EvidenceDefect`,
  `GovernanceFinding`, `TechnicalFinding`, `HumanBoundary`, `Informational` besitzen.
- **FR-005**: Findings MUESSEN stabile `EQA###`-IDs und vollstaendige Evidence-,
  Owner-, Risiko-, Trigger- und Scope-Felder besitzen.
- **FR-006**: Die Inventare fuer 10 Sprachprofile, 12 Presets, 123 Agentenflaechen,
  46 Governance-Checkpoints, 12 Evidence-Familien und 988 Referenzen MUESSEN
  mechanisch bestaetigt werden.
- **FR-007**: Mindestens 50 Evidence-Referenzen MUESSEN semantisch auf
  Existenz, Claim-Bezug und Aussagegrenze geprueft werden.
- **FR-008**: Der Audit MUSS die fuenf benannten Pflichtkandidaten klassifizieren.
- **FR-009**: Summen MUESSEN aus kanonischen Arrays berechnet werden; ein
  fest codierter Null-Finding-Ausgang ist verboten.
- **FR-010**: Bestehende Artefakte von 044/045/046 bleiben unveraendert.
- **FR-011**: Es duerfen keine Produkt-, API-, Dependency-, Projekt-, Beispiel-,
  Workflow- oder Serienmutationen entstehen.
- **FR-012**: Die Remote-Lieferung benoetigt mindestens einen unabhaengigen
  semantischen Review. Ein eng begrenzter Admin-Bypass darf nur bei gruenen
  technischen Gates, null umsetzbaren Threads und allein offener Human-Approval-
  Regel eingesetzt werden.

## Success Criteria

- **SC-001**: 12/12 Feature-044-Kontrollen und 157/157 Crosswalk-Zeilen sind bewertet.
- **SC-002**: 10/10 Sprachprofile, 12/12 Presets, 123/123 Agentenflaechen,
  46/46 Governance-Checkpoints, 12/12 Evidence-Familien und 988/988 Referenzen
  sind mechanisch inventarisiert.
- **SC-003**: Mindestens 50 Referenzen besitzen dokumentierte semantische Traces.
- **SC-004**: Positive und negative Validator-Faelle sind deterministisch gruen.
- **SC-005**: Der finale Diff enthaelt keine verbotene Aenderung und keine
  automatische Abstellung oder Folge-Intake-Datei.

## Assumptions And Boundaries

- Die frueheren Abschlusszustaende bleiben historische Tatsachen; Feature 047
  bewertet ihre Evidence-Qualitaet und schreibt sie nicht rueckwirkend um.
- Ein offener Human- oder Provider-Punkt ist nicht automatisch ein Defekt.
- `tv203s/` und externe Vergleichsquellen sind fuer diesen Governance-Audit `N/A`.
- DocFX/A11Y werden nur ausgeloest, wenn eine DocFX-Eingabe oder Navigation
  geaendert wird; feature-lokale Markdown-Evidence allein loest sie nicht aus.
