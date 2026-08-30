# Datenmodell: RL-SE-Selbstprüfung / Data Model: RL-SE Self-Review

**Feature**: `045-rl-se-checklist-self-review`
**Datum / Date**: 2026-08-30

Dieses Feature führt kein Laufzeit-, Datenbank- oder Benutzerdatenschema ein.
Das Modell beschreibt ausschließlich repository-lokale Audit-Evidence in
`rl-se-self-review.json` und deren lesbare Markdown-Projektionen.

*This feature introduces no runtime, database, or user-data schema. The model
describes repository-local audit evidence only.*

## 1. RlSeSelfReview

Wurzelobjekt des kanonischen JSON-Datensatzes.

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `schemaVersion` | string | Exakt `1.0`. |
| `featureId` | string | Exakt `045-rl-se-checklist-self-review`. |
| `reviewSnapshot` | `ReviewSnapshot` | Hashgebundener Reviewstand. |
| `allowedStatuses` | string[] | Exakt und in dieser Reihenfolge: `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, `FollowUp`. |
| `allowedPriorities` | string[] | Exakt `Critical`, `High`, `Medium`, `Low`, `None`. |
| `controls` | `ControlAssessment[157]` | Exakt 157, ordinal nach `ControlId`, keine Lücke oder Dublette. |
| `evidence` | `EvidenceReference[]` | Eindeutige IDs und Pfade; jede Referenz trägt Freshness. |
| `presets` | `PresetAssessment[12]` | Exakt die zwölf aktivierten Registry-Presets. |
| `governanceObservations` | `GovernanceObservation[]` | Bestätigte oder verworfene Driftkandidaten, keine Reparatur. |
| `humanBoundaries` | `HumanBoundary[]` | Recht, Organisation, Provider, Secrets, Plattform und Freigabe. |
| `summary` | `ReviewSummary` | Ausschließlich aus validierten Detaildaten abgeleitet. |

**Validierungsregeln / Validation rules**:

- Unbekannte Top-Level-Properties werden abgelehnt.
- JSON muss UTF-8 ohne private absolute Pfade, Credentials oder Sitzungsdaten
  sein.
- Der gesamte Datensatz wird atomar akzeptiert oder abgelehnt.
- Arrays, deren Reihenfolge fachlich relevant ist, werden ordinal sortiert.

## 2. ReviewSnapshot

| Feld / Field | Regel / Rule |
|---|---|
| `reviewedHead` | Vollständiger 40-stelliger Git-Commit des geprüften Repository-Snapshots. |
| `capturedAtUtc` | ISO-8601-UTC-Zeitpunkt mit `Z`. |
| `bindingInputHashes` | Pfad-zu-SHA-256-Menge für Intake, Spec, Clarify, beide Checklisten und Ready-Review. |
| `baselineSourceHashes` | Pfad-zu-SHA-256-Menge für Manifest, Richtlinie, Sammelband, zwölf Einzelchecklisten, Mapping, beide Constitutions, Registry und zwölf `preset.yml`. |
| `feature016EvidenceHashes` | Hashes von `docs/security/control-assessment.md` und `specs/016-secure-development-hardening/pr-evidence.md`. |
| `feature044EvidenceHashes` | Hashes von `docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json` und `specs/044-sandbox-secure-development-hardening/pr-evidence.md`; die Empfehlung `ConditionallyUsable` bleibt eine begrenzte historische Evidence, keine formale Freigabe. |
| `chapterCounts` | Exakt `12,13,15,10,13,11,12,13,17,17,12,12`. |
| `sourceControlCount` | Exakt `157`. |
| `sourceDuplicateCount` | Exakt `0`. |
| `sourceUnknownCount` | Exakt `0`. |

**Freshness-Regel / Freshness rule**: Eine Änderung eines gespeicherten Hashes
macht den Snapshot nicht still aktuell. Der Audit muss den betroffenen Bereich
erneut prüfen und einen neuen Snapshot erzeugen.

## 3. ControlAssessment

Eine Entität repräsentiert genau eine kanonische `CL-XX-NN`-Kontrolle.

| Feld / Field | Typ / Type | Pflichtregel / Required rule |
|---|---|---|
| `controlId` | string | Muster `CL-(01..12)-NN`; eindeutig und in der Quellmenge vorhanden. |
| `sourcePath` | string | Repository-relativer Pfad zur passenden Einzelcheckliste. |
| `sourceHeading` | string | Exakte Quellüberschrift ohne Heading-Marker. |
| `title` | bilingual string | Deutsch zuerst, Englisch danach. |
| `status` | enum | Genau ein Wert aus dem Fünfermodell. |
| `rationale` | bilingual string | Konkrete TuiVision-Begründung, nicht nur Wiederholung des Status. |
| `evidenceIds` | string[] | Direkte Referenzen oder leer nur zusammen mit `evidenceGap`. |
| `evidenceGap` | bilingual string or null | Explizite Beweislücke; niemals leerer String. |
| `owner` | string | Verantwortliche Rolle; Person nur bei vorhandener veröffentlichbarer Evidence. |
| `reviewer` | string | Prüfende Rolle. |
| `reviewDate` | ISO date | Datum des aktuellen Zeilenreviews. |
| `followUp` | bilingual string | Konkrete Aktion/Arbeitsgrenze oder begründetes `None`. |
| `priority` | enum | `Critical`, `High`, `Medium`, `Low` oder begründetes `None`. |
| `residualRisk` | bilingual string | Verbleibendes Risiko oder begründet niedrig/kein zusätzliches Risiko. |
| `reevaluationTrigger` | bilingual string | Konkretes Ereignis, das die Entscheidung erneut öffnet. |
| `humanOnly` | boolean | `true`, wenn die offene Entscheidung befugte menschliche Autorität braucht. |
| `externalOnly` | boolean | `true`, wenn aktuelle externe Plattform-/Provider-Evidence nötig ist. |
| `feature016Status` | enum | Historischer Vergleich aus 016; nicht der neue Status. |
| `feature016EvidenceId` | string | Referenz auf die alte Zeile oder deren Proof-Grenze. |
| `changeExplanation` | bilingual string | Warum Status/Evidence gleich blieb oder sich änderte. |

### Statusinvarianten / Status Invariants

| Status | Zusätzliche Regeln / Additional rules |
|---|---|
| `Applicable` | Nennt geltende Pflicht, aktuelle Evidence oder Lücke und sichere nächste Aktion. |
| `AlreadySatisfied` | Mindestens eine `EvidenceReference` mit `directness=Direct`, `freshness=Current`, existierendem Pfad und direkt stützendem Ergebnis; `evidenceGap=null`. |
| `N/A` | Faktische Nichtanwendbarkeit, begründetes `None` oder passende Aktion, Restrisiko und konkreter Trigger; fehlende Zeit, Autorität oder Evidence reichen nicht. |
| `Open` | Owner, Aktion, Priorität außer unbegründetem `None`, Restrisiko und Trigger; Human-/External-only-Grenze ist sichtbar. |
| `FollowUp` | Relevanter, klar begrenzter späterer Arbeitsumfang; keine automatisch erzeugte Intake-, Issue-, Branch- oder Feature-ID. |

## 4. EvidenceReference

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `evidenceId` | string | Lückenlos `EVD-001+` nach ordinal sortiertem Pfad und Beweisrolle. |
| `relativePath` | string | Repository-relativ, kein `..`, kein Root-/Home-/Temp-/privater absoluter Pfad. |
| `sha256` | string | 64 lowercase Hex-Zeichen für den Review-Snapshot. |
| `observedAtUtc` | timestamp | Aktueller Beobachtungszeitpunkt. |
| `evidenceType` | enum | `Policy`, `Configuration`, `Implementation`, `Test`, `CI`, `Audit`, `Architecture`, `Security`, `A11Y`, `Delivery`, `ExternalBoundary`. |
| `directness` | enum | `Direct`, `Supporting`, `ContextOnly`, `Gap`. |
| `freshness` | enum | `Current`, `Stale`, `Unverifiable`, `NotApplicable`. |
| `supportsClaims` | string[] | Kontroll-, Preset-, Observation- oder Boundary-IDs. |
| `result` | bilingual string | Beobachtetes Ergebnis ohne pauschalen Compliance-Claim. |
| `proofBoundary` | bilingual string | Was die Evidence ausdrücklich nicht beweist. |
| `reevaluationTrigger` | bilingual string | Hash-, Scope-, Plattform-, Provider- oder Policy-Änderung. |

**Relationsregeln / Relationship rules**:

- Jede `evidenceId` wird mindestens einmal referenziert.
- Jede Rückreferenz in `supportsClaims` besitzt die entsprechende
  Vorwärtsreferenz.
- `AlreadySatisfied` darf keine `Stale`, `Unverifiable`, `ContextOnly` oder
  `Gap`-Referenz als alleinigen Beweis verwenden.
- Ein fehlender Pfad wird als Gap modelliert, nicht als existierende Evidence.

## 5. PresetAssessment

| Feld / Field | Regel / Rule |
|---|---|
| `presetId` | Exakt eine der zwölf Registry-IDs. |
| `version` | Exakte Registry-Version. |
| `registryManifestHash` | Hash aus `.specify/presets/.registry`. |
| `presetArtifactHash` | SHA-256 der passenden `preset.yml`. |
| `checkpoints` | Nicht leere, featurebezogene Prüfpunktliste. |
| `status` | Einer der fünf Feature-Statuswerte. |
| `rationale` | Bilingual und konkret. |
| `evidenceIds` | Mindestens eine Evidence oder explizite Lücke. |
| `owner`, `reviewer`, `reviewDate` | Vollständig. |
| `followUp`, `priority`, `residualRisk`, `reevaluationTrigger` | Vollständig und statusgerecht. |
| `humanOnly` | Boolean. |

**Mengenkontrakt / Set contract**:

```text
security-governance@0.6.2
architecture-governance@0.5.2
isaqb-architecture-governance@0.2.2
a11y-governance@0.4.3
cross-platform-governance@0.2.2
agent-parity-governance@0.4.2
model-routing-governance@0.1.4
intake-authoring-governance@0.3.1
intake-review-governance@0.2.1
intake-sequencing-governance@0.2.3
autonomous-run-governance@0.4.1
parallel-autonomous-run-governance@0.2.6
```

Kein nicht ausgelöster Script-, Parallel- oder Remote-Aspekt wird ausgelassen;
er erhält ein begründetes `N/A` und einen Trigger.

## 6. GovernanceObservation

| Feld / Field | Regel / Rule |
|---|---|
| `observationId` | Lückenlos `GOV-001+`, ordinal nach Gegenstandsbereich. |
| `topic` | Baseline, Constitution, Preset-Mapping, Feature-016-Freshness, Feature-044-Sandbox-Grenzen oder später bestätigter Governance-Bereich. |
| `sourceEvidenceIds` | Mindestens zwei vergleichbare Quellen bei einem Driftbefund. |
| `observation` | Faktischer Unterschied, keine Reparaturanweisung als vollzogenes Ergebnis. |
| `disposition` | `Confirmed`, `Rejected`, `Refined`, `OpenHumanDecision`. |
| `impact` | Auswirkung auf Audit oder spätere Governance. |
| `owner`, `reviewer`, `reviewDate` | Vollständig. |
| `followUp`, `priority`, `residualRisk`, `reevaluationTrigger` | Vollständig. |
| `repairPerformed` | Exakt `false`. |

Mindestens diese Startbeobachtungen werden geprüft: Manifest/Richtlinie/
Sammelband/Checklisten-Versionen, Constitution 1.17.0 gegen 1.18.1,
sechs/sieben/acht gegen zwölf Presets, Feature-016-Freshness und die noch
offenen Human-/External-only-Grenzen der Feature-044-Sandbox-Evidence.

## 7. HumanBoundary

| Feld / Field | Regel / Rule |
|---|---|
| `boundaryId` | Lückenlos `HUM-001+`. |
| `domain` | `Legal`, `Organisation`, `Provider`, `Secrets`, `Platform`, `FormalApproval`, `ExternalEvidence`. |
| `relatedControlIds` | Mindestens eine Kontroll-ID. |
| `status` | `Open`, `FollowUp` oder faktisch begründet `N/A`; nie unbefugt `AlreadySatisfied`. |
| `responsibleHumanRole` | Konkrete Rolle. |
| `evidenceIds` / `evidenceGap` | Vorhandene technische Evidence und klar getrennte Autoritätslücke. |
| `action`, `priority`, `residualRisk`, `reevaluationTrigger` | Vollständig. |
| `agentMayClose` | Exakt `false`, solange keine befugte veröffentlichbare Evidence vorliegt. |

## 8. ReviewSummary

| Feld / Field | Regel / Rule |
|---|---|
| `totalControls` | Exakt 157. |
| `chapterCounts` | Exakte zwölf Zahlen. |
| `statusCounts` | Summe exakt 157; nur fünf Schlüssel. |
| `presetCount` | Exakt 12. |
| `governanceObservationCounts` | Nach Disposition abgeleitet. |
| `humanBoundaryCount` | Aus Detailmenge abgeleitet. |
| `positiveClaimCount` | Anzahl `AlreadySatisfied`; jede Zeile muss direkte aktuelle Evidence besitzen. |
| `staleEvidenceCount` | Aus Evidence abgeleitet; darf positive Claims nicht allein stützen. |
| `scopeViolationCount` | Exakt 0 für Akzeptanz. |
| `unauthorisedHumanClaimCount` | Exakt 0 für Akzeptanz. |
| `validationState` | `NotRun`, `Failed`, `Passed`; `Passed` nur nach erfolgreichem atomarem Auditvalidator für Komplettdatensatz, Projektionen und alle Negativ-Fixtures. Delivery- oder PostMerge-Gates gehören nicht in diesen getrackten Datensatz. |
| `auditStatement` | Nicht zertifizierende Zusammenfassung; kein QISMS-, Compliance- oder Freigabeclaim. |

Die historische 016-Verteilung wird als Vergleichsobjekt gespeichert, aber
nicht in neue Summen eingerechnet.

## 9. MarkdownProjection

Jede Projektion enthält:

- Titel, Feature-ID, Snapshot-HEAD und Datenhash;
- Deutsch zuerst, Englisch danach;
- semantische Überschriften und Tabellenheader;
- alle fachlich relevanten IDs als Text;
- Status, Priorität und Risiko ausgeschrieben;
- Erklärungen zentraler Fachbegriffe beim ersten Gebrauch;
- vollständigen Text für jede zusätzliche visuelle Darstellung;
- keine divergierende Entscheidung gegenüber JSON.

`control-assessment.md` muss exakt 157 Kontrollzeilen und die gleichen
Statussummen wie JSON enthalten. `preset-assessment.md` muss exakt zwölf
Preset-Zeilen enthalten. Der Validator prüft ID-, Status- und Hashparität.

## 10. ValidationFixture

| Feld / Field | Regel / Rule |
|---|---|
| `fixtureId` | Stabiler Name. |
| `baseCase` | `VerticalSlice` oder `CompleteAudit`. |
| `mutatedInvariant` | Genau eine Primärinvariante. |
| `expectedErrorCode` | Stabiler Wert `RLSE001+`. |
| `expectedMessageFragment` | Bilinguale verständliche Fehlergrenze. |
| `mustNotWrite` | Exakt `true`. |

### Minimale Fehlercodes / Minimum Error Codes

| Code | Bedeutung / Meaning |
|---|---|
| `RLSE001` | Ungültiges JSON oder unbekannte Schema-Version. |
| `RLSE002` | Falsche Gesamt- oder Kapitelkardinalität. |
| `RLSE003` | Fehlende, doppelte oder unbekannte Kontroll-ID. |
| `RLSE004` | Ungültiger Status oder ungültige Priorität. |
| `RLSE005` | Leeres Pflichtfeld oder ungültiger statusbezogener Vertrag. |
| `RLSE006` | `AlreadySatisfied` ohne direkte aktuelle Evidence. |
| `RLSE007` | Evidence-Pfad/Hash/Freshness/Relation ungültig. |
| `RLSE008` | Nicht exakt zwölf Presets oder Version/Hash falsch. |
| `RLSE009` | Unbefugter Human-/External-only-Claim. |
| `RLSE010` | Governance-Beobachtung ohne Quellen oder mit Reparaturclaim. |
| `RLSE011` | JSON-/Markdown-Projektionsdrift. |
| `RLSE012` | Scope-Verletzung oder verbotener privater/absoluter Pfad. |

## 11. Zustandsmodell / State Model

```text
NotRun
  -> EvidenceSkeletonCreated
  -> VerticalSliceRedProven
  -> VerticalSliceAccepted
  -> ChapterReviewInProgress
  -> ControlReviewComplete
  -> PresetAndDriftReviewComplete
  -> LocalValidationComplete
  -> ExactHeadEvidenceAccepted
  -> AuditEvidenceComplete

Jeder Zustand / Any state
  -> BlockedInputDrift
  -> BlockedEvidenceIntegrity
  -> BlockedScopeViolation
  -> BlockedHumanAuthority
  -> BlockedValidation
```

`AuditEvidenceComplete` bedeutet nur, dass die Selbstprüfung und ihre Evidence
vollständig sind. Es bedeutet keine Zertifizierung, formale Freigabe,
Governance-Reparatur oder gestartete Folgearbeit.

## 12. Mutations- und Löschregeln / Mutation and Deletion Rules

- Keine Entität verändert Produkt-, Beispiel-, Package-, Projekt-, Workflow-,
  Constitution-, Preset-, Baseline- oder historische Quelldateien.
- Feature-016-Evidence bleibt unverändert und wird nur referenziert.
- Ein veränderter Input erzeugt einen neuen Snapshot, keine stille Hash-
  Aktualisierung.
- Findings erzeugen keine Intakes, Issues, Branches oder Features.
- Ein ungültiger Datensatz erzeugt keine teilweise akzeptierte Projektion.
- Temporäre Test-, Coverage-, DocFX- und Gate-Ausgaben bleiben untracked und
  werden nach ihrer Auswertung entfernt.
