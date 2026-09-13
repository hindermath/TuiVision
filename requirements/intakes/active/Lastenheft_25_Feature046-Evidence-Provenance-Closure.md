<!-- intake-authoring:begin -->
# Lastenheft 25: Feature-046-Evidence-Provenienzabschluss

**Dokumenttyp:** Eigenständiger Spec-Kit-Intake
**Status:** ReadyForReview
**Projekt:** TuiVision
**Vorgesehenes Feature:** `048-feature046-evidence-provenance-closure`
**Zielgruppe:** Maintainer, Security- und Evidence-Reviewer, Auszubildende und KI-Agenten

## Zweck / Purpose

Feature 048 schließt die in Feature 047 bestätigten Evidence-Lücken
`EQA002`, `EQA004` und `EQA010`, ohne historische Artefakte umzuschreiben. Der
Lauf erzeugt einen nachvollziehbaren Nachfolger für die Feature-046-Evidence
und legt das gemeinsame Follow-up-Ledger für alle zehn `EQA###`-Findings an.

*Feature 048 closes the evidence gaps `EQA002`, `EQA004`, and `EQA010`
confirmed by Feature 047 without rewriting historical artifacts. It produces a
traceable successor to the Feature 046 evidence and initializes the shared
follow-up ledger for all ten `EQA###` findings.*

## Ausgangslage / Current State

- Feature 046 inventarisierte 988 Evidence-Referenzen, meldete aber keine
  kanonischen Observations trotz konkreter Hinweise in der PR-Evidence.
- Feature 047 bestätigte 916 Referenzen, 66 fehlende Referenzen und sechs
  Hashabweichungen am jeweils behaupteten Commit.
- Der manuelle Feature-046-Inhaltsreview weist eine Dauer von null Sekunden aus
  und beweist deshalb keinen belastbaren semantischen Review.
- Das Feature-047-Finding-Register mit SHA-256
  `c820e596fe11ad83572c1ea1f08ee8faafd75aa84a5b38a7b93653bcacea43cb`
  bleibt die unveränderliche Eingangsquelle.

*Feature 046 inventoried 988 evidence references but omitted known technical
observations from its canonical register. Feature 047 verified 916 references
and identified 66 missing objects, six hash mismatches, and a zero-duration
manual review. The Feature 047 finding register remains immutable input.*

## Verbindliche Eingaben / Binding Inputs

- `docs/security/secure-development/2026-09-13-evidence-quality-audit/finding-register.json`
- `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/gsdb-spec-kit-intensive-review.json`
- `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/validation-evidence.md`
- `specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md`
- `specs/047-evidence-quality-audit/spec.md` und `pr-evidence.md`
- die in den 988 Referenzen genannten unveränderlichen Git-Objekte und
  Repository-Dateien

## Scope

1. Alle 988 Referenzen am jeweils behaupteten unveränderlichen Commit erneut
   auf Existenz, Pfad, normalisierten Hash und Claim-Bezug prüfen.
2. Die 66 fehlenden und sechs hashabweichenden Referenzen einzeln untersuchen.
3. Jede Referenz genau als `Verified`, `ReboundToImmutableEvidence`,
   `AcceptedHistoricalGap` oder `StillOpen` klassifizieren.
4. Jede Neubindung mit altem Claim, altem Pfad/Commit, neuem unveränderlichem
   Ziel, Begründung und Hash dokumentieren.
5. Technische Hinweise aus Feature-046-PR-Evidence und kanonischem
   Observation-Register vollständig und dedupliziert abgleichen.
6. Reale Quelle-Claim-Evidence-Entscheidungs-Traces mit Start, Ende, geprüften
   Hops, Ergebnis und Proof-Grenze erzeugen.
7. Ein gemeinsames Follow-up-Ledger mit genau zehn eindeutigen Zeilen
   `EQA001` bis `EQA010` anlegen.

## Nicht-Ziele / Non-Goals

- Keine Änderung am Feature-047-Finding-Register oder an historischen
  Artefakten der Features 044 bis 047.
- Keine erfundenen Git-Objekte, Hashes, Zeitstempel, Reviews oder Evidence.
- Keine Runtime-, API-, Paket-, Projekt-, Beispiel- oder Produktverhaltensaenderung.
- Keine Control-Level-Reassessment aus Lastenheft 26.
- Keine Toolchain-, Paket- oder Workflow-Abstellung aus Lastenheft 27.
- Keine Änderung oder Wiedereröffnung der abgeschlossenen Serie
  `tui-vision-delivery`.
- Kein automatischer Start von Feature 049 oder 050.

*The run does not rewrite historical evidence, invent proof, change product
behavior, perform the later control reassessment, or implement later toolchain
decisions. The completed delivery series remains unchanged.*

## Evidence-Modell / Evidence Model

Jede der 988 Referenzen besitzt genau eine Entscheidung:

- `Verified`: Das behauptete Objekt existiert am behaupteten Commit und der
  normalisierte Hash stimmt.
- `ReboundToImmutableEvidence`: Der alte Verweis ist nachweisbar falsch oder
  unvollständig und wird durch ein fachlich gleichwertiges unveränderliches
  Objekt ersetzt.
- `AcceptedHistoricalGap`: Der historische Nachweis ist nicht rekonstruierbar;
  Owner, Restrisiko und Wiederbewertungsauslöser sind vollständig.
- `StillOpen`: Die Entscheidung ist noch nicht tragfähig und blockiert eine
  positive Gesamtschließung.

Das Follow-up-Ledger verwendet für jedes `EQA###` genau eine Entscheidung aus
`Resolved`, `Contradicted`, `Superseded`, `AcceptedOpenBoundary` oder
`StillOpen`. Feature 048 darf nur seine primären Findings `EQA002`, `EQA004`
und `EQA010` abschliessend verändern; die anderen sieben Zeilen bleiben
sichtbar als spätere Zuständigkeit markiert.

## Erwartete Artefakte / Expected Artifacts

- kanonischer 988-Zeilen-Provenienzdatensatz und German-first/English-second
  Leserprojektion;
- dedupliziertes Observation-Register für die Feature-046-Hinweise;
- semantische Review-Traces mit realen Zeitgrenzen;
- `docs/security/secure-development/evidence-quality-follow-up/eqa-follow-up-ledger.json`
  mit genau zehn Finding-Zeilen;
- deterministischer test-only Validator mit positiven und negativen Fixtures;
- Feature-Evidence, Retrospektive und gegebenenfalls klar begrenzte Follow-ups.

## Serielle Folgeplanung / Serial Follow-up Plan

| Reihenfolge | Intake | Feature | Primaere Findings | Voraussetzung |
|---:|---|---|---|---|
| 1 | `Lastenheft_25_Feature046-Evidence-Provenance-Closure.md` | `048-feature046-evidence-provenance-closure` | `EQA002`, `EQA004`, `EQA010` | Feature 047 abgeschlossen |
| 2 | `Lastenheft_26_Feature045-046-Control-Level-Semantic-Reassessment.md` | `049-feature045-046-control-level-reassessment` | `EQA001`, `EQA003`, `EQA009` | Feature 048 abgeschlossen |
| 3 | `Lastenheft_27_Toolchain-CI-Finding-Closure.md` | `050-toolchain-ci-finding-closure` | `EQA005`, `EQA006`, `EQA007`, `EQA008` | Feature 049 abgeschlossen |

Lastenheft 26 und 27 werden just-in-time erstellt. Diese Tabelle erteilt keine
Authoring-, Specify-, Implementierungs- oder Remote-Autoritaet.

*Intakes 26 and 27 are authored just in time. This table grants no authoring,
specification, implementation, or remote-delivery authority.*

## Qualität und Governance / Quality And Governance

- Nutzernahe Dokumentation ist German-first/English-second, CEFR-B2 und
  text-first gemäß WCAG-2.2-AA-Grundsatz.
- Security- und Evidence-Aussagen sind keine Zertifizierungs- oder
  Compliance-Claims.
- Git-Historie wird nur gelesen; historische Commits und Feature-Artefakte
  bleiben unverändert.
- Neue Skripte benötigen Bash-/PowerShell-Parität oder eine begründete,
  plattformneutrale gemeinsame Kernimplementierung mit beiden Entry-Points.
- Ein unabhängiger semantischer Review ist bei späterer Remote-Lieferung ein
  nicht durch Admin-Bypass ersetzbares Gate.

## Risiken und Stop-Grenzen / Risks And Stop Boundaries

- Mehrdeutige Neubindung, nicht eindeutige Hashzuordnung, fehlender Owner oder
  ein `StillOpen` ohne konkrete Grenze stoppt eine positive Schliessung.
- Nicht rekonstruierbare historische Evidence darf als
  `AcceptedHistoricalGap` erhalten bleiben, aber nicht als `Verified` zählen.
- Eine notwendige Produkt-, Paket-, Workflow- oder API-Änderung wird nicht in
  Feature 048 umgesetzt, sondern an Lastenheft 27 oder einen neu zu
  autorisierenden Intake übergeben.
- Drift am Feature-047-Finding-Register stoppt den Lauf vor Implementierung.

## Akzeptanzkriterien / Acceptance Criteria

1. Der Provenienzdatensatz enthält genau 988 eindeutige Referenzen und jede
   besitzt genau eine erlaubte Entscheidung.
2. Die Ausgangsverteilung 916 bestätigt, 66 fehlend und sechs hashabweichend
   ist als reproduzierbare Baseline nachgewiesen.
3. Jede der 72 Abweichungen besitzt eine individuelle Entscheidung und
   Evidence; keine Sammelbegründung ersetzt die Einzelprüfung.
4. Alle konkreten Feature-046-Hinweise erscheinen genau einmal im neuen
   Observation-Register oder besitzen eine nachgewiesene Deduplizierung.
5. Semantische Traces besitzen reale, geordnete Zeitpunkte und konkrete
   Quelle-Claim-Evidence-Entscheidungs-Hops; eine reine Dauerangabe reicht nicht.
6. Das Follow-up-Ledger enthält `EQA001` bis `EQA010` genau einmal und Feature
   048 ändert nur die drei ihm zugeordneten primären Zeilen.
7. `StillOpen` besitzt Owner, Restrisiko, Trigger und konkrete nächste Grenze
   und verhindert eine falsche positive Gesamtschließung.
8. Positive und negative Validator-Fixtures lehnen fehlende, doppelte,
   mehrdeutige, hashabweichende und unvollständige Zeilen ab.
9. Der Diff enthält keine Änderung an Runtime, API, Paketen, Projekten,
   Beispielen, Workflows, historischen Features oder der abgeschlossenen Serie.
10. Intake-, Evidence-, Agent-Paritäts-, Secret-, Whitespace- und proportionale
    Dokumentationsgates sind erfolgreich dokumentiert.

## Annahmen und offene Fragen / Assumptions And Open Questions

- Feature 047 und sein Finding-Register sind abgeschlossen und unverändert.
- Lastenheftnummer 25 und Feature-Nummer 048 sind frei.
- Es gibt keine offene materielle Frage für Specify oder Plan.
- Die Ausführungsautorität bleibt `LocalImplementation`, bis sie ausdrücklich
  neu erteilt wird.

<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

### Specify

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use requirements/intakes/active/Lastenheft_25_Feature046-Evidence-Provenance-Closure.md as the binding intake. Create Feature 048 as a successor evidence-provenance closure for EQA002, EQA004, and EQA010. Preserve the Feature 047 finding register, all historical Feature 044-047 artifacts, the completed delivery series, and product behavior unchanged. Reconcile all 988 Feature 046 evidence references and initialize the ten-row EQA follow-up ledger. Do not implement, commit, push, create or merge a pull request, start another feature, or infer remote authority.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run for Feature 048 using requirements/intakes/active/Lastenheft_25_Feature046-Evidence-Provenance-Closure.md as the binding intake. Delivery mode: LocalImplementation. Revalidate all 988 Feature 046 evidence references at their claimed immutable commits, resolve the 66 missing and six hash-mismatched references individually, reconcile canonical observations, record real semantic review traces, and initialize the ten-row EQA follow-up ledger. Preserve historical Features 044-047, runtime, APIs, packages, projects, examples, workflows, and the completed delivery series. Stop on provenance ambiguity, finding-register drift, an unowned open boundary, or any mandatory gate failure. Do not push, create or merge a pull request, use bypass authority, create Intake 26 or 27, or start another feature.
```
<!-- intake-authoring:end -->
