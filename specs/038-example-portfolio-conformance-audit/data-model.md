# Datenmodell: Beispielportfolio-Audit / Data Model: Example Portfolio Audit

## 1. Kanonisches Aggregat / Canonical Aggregate

`ExamplePortfolioAudit` ist die einzige maschinenlesbare Wahrheitsquelle. Alle
Sammlungen werden ordinal nach ihrer stabilen ID gespeichert. Freitext ist
UTF-8 und Unicode-NFC; IDs, Enum-Werte, Pfade und Deduplication Keys sind
case-sensitive.

*`ExamplePortfolioAudit` is the only machine-readable source of truth. All
collections are stored in ordinal stable-ID order. Prose is UTF-8 and Unicode
NFC; IDs, enum values, paths, and deduplication keys are case-sensitive.*

| Feld / Field | Typ / Type | Kardinalität / Cardinality | Regel / Rule |
|---|---|---:|---|
| `SchemaVersion` | string | 1 | Start `1.0`; unbekannte Version wird abgelehnt. |
| `FeatureId` | string | 1 | Exakt `038-example-portfolio-conformance-audit`. |
| `DeliveryMode` | enum | 1 | Exakt `MergeAndSync`; die Lieferautorität ändert keinen Portfolio- oder Produktscope. |
| `Baseline` | `AuditBaseline` | 1 | Bindende Hashes, HEAD und Projektmengenhash. |
| `Sources` | `SourceManifestEntry[]` | 1..n | Historische, Vergleichs- und akzeptierte Evidence-Quellen. |
| `Evidence` | `EvidenceRecord[]` | 1..n | Lokale Entry-, Guide-, Smoke-, Feature- und Governance-Evidence. |
| `PortfolioEntries` | `PortfolioEntry[]` | exakt 37 | `EX001`–`EX037`, keine Lücke oder Dublette. |
| `Findings` | `ConformanceFinding[]` | 0..n | Bei nicht leerer Menge exakt `EF001+`, lückenlos. |
| `OwnerDag` | `OwnerDag` | 1 | Vier mögliche Owner-Knoten, nur nicht leere Knoten emittiert. |
| `RemediationHandoff` | `RemediationHandoff` | 1 | 0–4 Remediation-Intakes plus exakt ein Closure. |
| `GovernanceDecisions` | `GovernanceDecision[]` | 1..n | Zwölf Presets und alle benannten Standards ohne stille Auslassung. |
| `PortfolioGate` | `PortfolioGate` | 1 | Ehrlicher Auditstatus; keine vorweggenommene Closure-Aussage. |

Diese Kardinalitäten definieren den abnahmegültigen Datensatz. Das frühe
`NotAssessed`-Skelett darf parsefähig, aber unvollständig sein; es besteht
weder Portfolio- noch Relationsgate und darf nicht als gültig angenommen
werden, bevor exakt 37 vollständige Zeilen vorliegen.

*These cardinalities define the acceptance-valid dataset. The early
`NotAssessed` skeleton may be parseable but incomplete; it passes neither the
portfolio nor relations gate and cannot be accepted until all 37 rows are
complete.*

## 2. AuditBaseline

| Feld / Field | Regel / Rule |
|---|---|
| `PlanningHead` | Exakt der akzeptierte Ausgangs-HEAD; Drift braucht Revalidierung. |
| `AcceptedArtifactHashes` | Exakt die vier Hashes aus dem Autonomous-State. |
| `Feature037EvidenceHashes` | Hashes von `wave6-combined-delta.json` und `delivery-closeout.md`. |
| `ComparisonPins` | Free Vision, Terminal.GUI und magiblot Pins aus Features 024/029/030. |
| `DirectProjectCount` | Exakt 37. |
| `DirectProjectPathSetSha256` | `cb2f6568b70f2a62cd529250777e849dd2cd026c05732df81733b2fc3d177333`. |
| `ProtectedRoots` | `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/` plus API-/Dependency-Flächen. |

## 3. SourceManifestEntry

Eine Quellenzeile benennt eine historische Datei, eine gepinnte
Vergleichsquelle oder ein akzeptiertes unveränderliches Evidence-Artefakt.

*A source row identifies one historical file, pinned comparison source, or
accepted immutable evidence artifact.*

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `SourceId` | string | Eindeutig; `TV203-E###`, `TVDEMOS-E###`, `TVFM-E###`, bestehendes `FV###`, `TGSR###`, `MBSR###` oder `BASE-E###`; neue historische und `BASE-E`-IDs werden je Präfix ordinal nach normalisiertem relativem Pfad vergeben. |
| `Authority` | enum | `HistoricalTV203`, `HistoricalTVDEMOS`, `HistoricalTVFM`, `TuiVisionEvidence`, `FreeVision`, `TerminalGui`, `Magiblot`. |
| `RelativePath` | string | Repository-relativ oder bereits akzeptierter externer Pfad; kein `..`, kein absoluter neuer Checkout-Pfad. |
| `Sha256` | string | 64 lowercase Hex-Zeichen. Textnormalisierung nur, wenn Vorgängerevidence sie ausdrücklich bindet. |
| `Pin` | string | Commit/Tag/Tree oder `RepositoryHead` für lokale read-only Quellen. |
| `Responsibility` | string | Eigene, kurze Zusammenfassung; keine Kopie oder mechanische Übersetzung. |
| `NoCopyBoundary` | string | Pflicht für historische/externe Quellen. |
| `ExampleIds` | string[] | Eindeutig, ordinal sortiert, reziprok zu Portfoliozeilen. |
| `ReevaluationTrigger` | string | Pflicht, besonders bei `N/A`-Vergleichsgrenzen. |

## 4. EvidenceRecord

| Feld / Field | Regel / Rule |
|---|---|
| `EvidenceId` | `EVD001+`, eindeutig und lückenlos innerhalb der Evidence-Liste. |
| `EvidenceClass` | `EntryPoint`, `Guide`, `Smoke`, `FeatureEvidence`, `ClosureEvidence`, `FrameworkContract`, `Platform`, `Governance`, `Review`. |
| `RelativePath` | Existierender repository-relativer Pfad oder geplanter Feature-038-Pfad. |
| `AnchorOrTest` | Optionaler Markdown-Anker oder exakter Testname; niemals allein ein Dateiname bei mehrdeutiger Evidence. |
| `ExampleIds` | Eindeutige, ordinal sortierte Rückrelationen. |
| `Sha256` | Pflicht für bindende Vorgängerevidence; `Pending` nur vor ihrer späteren Erstellung. |
| `ProofBoundary` | Was die Evidence beweist und ausdrücklich nicht beweist. |

## 5. PortfolioEntry

Jede der 37 Zeilen besitzt alle FR-005-Felder. Dimensionswerte werden nicht als
lose Strings, sondern als `DimensionDecision` modelliert, damit `N/A` nie ohne
Begründung und Trigger vorkommen kann.

*Every one of the 37 rows contains all FR-005 fields. Dimension values use a
`DimensionDecision` object so `N/A` cannot appear without rationale and trigger.*

### Identität und Zweck / Identity and purpose

`ExampleId`, `Name`, `PortfolioRole`, `Wave`, `OriginalIntent`, `LearningGoal`,
`CurrentEntryPoint`, `VisibleFirstScreen`, `Owner`, `Reviewer`, `ReviewDate`,
`ResidualRisk`, `ReevaluationTrigger`.

### Relationen / Relations

`HistoricalSourceIds`, `ModernizationSourceIds`, `EvidenceIds`,
`PrimaryInteractionPaths`, `FrameworkComponents`, `LocalSpecialLogic`,
`FindingIds`, `EvidencePath`.

### Entscheidungen / Decisions

| Feld / Field | Erlaubte Werte / Allowed values |
|---|---|
| `FrameworkDecision` | `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, `FollowUpHardening` |
| `PrimaryDisposition` | `AcceptedAsIs`, `AcceptedIntentionalDeviation`, `CandidateFinding`, `ProductDecision` |
| zehn Dimensionen / ten dimensions | `BehaviorStatus`, `InteractionStatus`, `ProofStatus`, `DocumentationStatus`, `A11YStatus`, `PlatformStatus`, `HistoricalRelation`, `FreeVisionRelation`, `TerminalGuiRelation`, `MagiblotRelation` — jede als `DimensionDecision`; sechs Auditstatus und vier Source-Relationen bleiben getrennt. |

### Konsistenzregeln / Consistency rules

- `HistoricalExample` verlangt mindestens eine historische Quelle;
  `SupplementalControl` verlangt leere `HistoricalSourceIds` und
  `HistoricalRelation.Status=N/A`.
- `AcceptedAsIs` enthält kein `Gap`, keine `FindingIds` und keine offene
  Product Decision.
- `AcceptedIntentionalDeviation` enthält mindestens ein
  `IntentionalDeviation`, aber kein `Gap`.
- `CandidateFinding` enthält mindestens ein `Gap`; jedes Gap verweist über
  `FindingIds` auf genau ein dedupliziertes Finding.
- `ProductDecision` setzt das Portfolio-Gate auf `BlockedProductDecision` und
  stoppt den Lauf.
- `SmallFrameworkFix` und `FollowUpHardening` verlangen
  `CandidateFinding` oder `ProductDecision`; sie werden nicht implementiert.
- Jeder Pfad und jede ID hat eine reziproke Source-/Evidence-/Finding-Relation.

## 6. DimensionDecision

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `Status` | enum | `Pass`, `IntentionalDeviation`, `Gap`, `N/A`. |
| `Rationale` | string | Immer nicht leer; eigene Worte. |
| `EvidenceIds` | string[] | Bei `Pass`, `IntentionalDeviation` und `Gap` mindestens eine ID. |
| `FindingId` | string? | Exakt eine ID bei `Gap`, außer die Zeile ist blockierendes `ProductDecision`. |
| `ReevaluationTrigger` | string | Pflicht bei `N/A` und `IntentionalDeviation`; bei allen anderen empfohlen und nicht leer im finalen Audit. |

## 7. ConformanceFinding

| Feldgruppe / Field group | Pflichtfelder / Required fields |
|---|---|
| Identität | `FindingId`, `DeduplicationKey`, `Dimension`, `ExampleIds` |
| Beobachtung | `Observation`, `Reproduction`, `HistoricalIntent`, `CurrentBehavior`, `MissingBehaviorOrProof` |
| Relationen | `SourceRelations`, `EvidencePath`, reziproke `ExampleIds`/`FindingIds` |
| Risiko | `Risk`, `APIImpact`, `A11YImpact`, `PlatformImpact`, `ResidualRisk` |
| Ownership | `PrimaryOwner`, `SecondaryImpacts`, administratives `Owner`, `Reviewer`, `ReviewDate` |
| Folgearbeit | `Dependencies`, `RequiredRedProof`, `RequiredRealPathGreenProof`, `ReevaluationTrigger` |

### ID- und Deduplizierungsregel / ID and deduplication rule

Nach vollständiger Root-Cause-Gruppierung werden Findings nach fester
Primary-Owner-Reihenfolge und danach ordinal nach `DeduplicationKey` sortiert.
Erst dann entstehen `EF001`, `EF002`, … ohne Lücke. Ein Deduplication Key hat
exakt ein Finding; ein Finding hat mindestens eine `ExampleId`.

*After complete root-cause grouping, findings are sorted by fixed primary-owner
order and then ordinal deduplication key. Only then are contiguous `EF001+` IDs
assigned. One key maps to exactly one finding; one finding affects at least one
example.*

## 8. OwnerDag

| Feld / Field | Regel / Rule |
|---|---|
| `Nodes` | Teilmenge von `FrameworkReuse`, `BehaviorInteraction`, `ProofPlatform`, `LearningA11Y`; nur Owner mit Findings werden emittiert. |
| `FindingIds` | Jeder Finding-ID genau einem Knoten zugeordnet. |
| `Dependencies` | Finding A listet Voraussetzungen B. Bei verschiedenen Primary Ownern entsteht `Owner(B) -> Owner(A)`; gleiche Owner bleiben intern, Dubletten werden kollabiert, Selbstkante und Zyklus sind verboten. |
| `TopologicalOrder` | Enthält jeden emittierten Knoten genau einmal und respektiert alle Kanten; bei mehreren freien Knoten gilt die feste Owner-Reihenfolge als Tie-Breaker. |
| `SuppressedOwners` | Genau die leeren Owner; keine Intake-Datei. |

## 9. RemediationHandoff

| Feld / Field | Regel / Rule |
|---|---|
| `OwnerGroups` | Vier Statuszeilen mit `Emitted` oder `Suppressed`. |
| `RemediationIntakes` | Exakt ein unnummerierter Intake pro `Emitted` Owner; enthält alle zugehörigen Findings, Abhängigkeiten und Proof-Anforderungen. |
| `ClosureIntake` | Exakt ein `Lastenheft_Example-Portfolio-Closure.md`. |
| `OrderedIntakePaths` | Topologische Remediation-Pfade, danach Closure als letzter Pfad. |
| `StartedFeatureIds` | Exakt leere Liste. |

## 10. GovernanceDecision

| Feld / Field | Regel / Rule |
|---|---|
| `DecisionId` | Stabiler Feature-038-Identifier. |
| `Checkpoint` | Preset oder Standard; alle zwölf Presets werden geführt. |
| `Version` / `Priority` | Für Presets Pflicht; aus akzeptierter Spec. |
| `Applicability` | `Applicable`, `N/A`, `Open`. |
| `Implementation` | `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed`. `N/A` verlangt `Not Assessed`. |
| `Rationale`, `EvidencePath`, `Owner`, `Reviewer`, `ResidualRisk`, `ReevaluationTrigger`, `FollowUp` | Immer vorhanden; `Open` braucht konkrete Folgearbeit. |

## 11. PortfolioGate und Zustände / PortfolioGate and states

Erlaubte Zustände:

```text
NotAssessed
  -> VerticalSliceAccepted
  -> BroadReviewComplete
  -> FindingsFrozen
  -> HandoffComplete
  -> AuditCompleteNoFindings | AuditCompleteWithRemediation

Jeder Zustand / Any state
  -> BlockedPortfolioDrift
  -> BlockedProductDecision
  -> BlockedFindingIntegrity
  -> BlockedEvidenceIntegrity
  -> BlockedScopeViolation
  -> BlockedValidation
```

Die beiden `AuditComplete*`-Zustände bedeuten nur, dass Feature 038 seinen
Audit und Handoff beendet hat. Ausschließlich der spätere unabhängige Closure
darf `PortfolioConformantAndLearningReady` erklären; dieser Zustand ist im
Feature-038-Enum absichtlich nicht vorhanden.

*The `AuditComplete*` states mean only that Feature 038 completed its audit and
handoff. Only the later independent closure may declare the portfolio conformant
and learning-ready; that state is intentionally absent from the Feature-038 enum.*

## 12. Lösch- und Mutationsregeln / Deletion and mutation rules

- Keine Entity löscht oder verändert Produkt-, Beispiel- oder historische
  Quelldateien.
- Eine geänderte direkte Projektmenge mutiert nicht automatisch das Inventar;
  sie erzeugt `BlockedPortfolioDrift`.
- Finding-IDs werden nur beim finalen Audit-Freeze vergeben. Eine spätere
  Änderung erfordert atomare Neugenerierung aller reziproken Projektionen.
- Remediation- und Closure-Intakes sind Outputs, keine gestarteten Features.
- Malformed Input wird vollständig abgelehnt; ein Teilmodell wird nicht als
  gültige Evidence gespeichert.
