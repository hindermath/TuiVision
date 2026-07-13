# Data Model: TV203 and Free Vision Conformance Audit

## AuditRun

| Field | Rule |
|---|---|
| `runId` | Stable feature-scoped identifier, exactly one for 024 |
| `auditRevision` | Positive revision number; Revision 2 adds Wave-5 and Wave-6 consumer validation |
| `auditDate` | ISO date |
| `historicalLedgerPath` | Repository-relative canonical ledger path |
| `historicalRowCount` | Must equal current filesystem and ledger count; planning baseline 151 |
| `modernSourceCount` | Must equal maintained source inventory at validation time; planning baseline 119 |
| `freeVisionRepository` | Exact official upstream URL |
| `freeVisionCommit` | Exact 40-character approved commit |
| `consumerScope` | Exact reviewed consumer roots; Revision 2 requires `TVDEMOS` and `TVFM` |
| `deliveryMode` | `MergeAndSync` |

## AuditDomain

| Field | Rule |
|---|---|
| `domainId` | `D01` through `D16`, unique |
| `nameDe` / `nameEn` | Non-empty bilingual names |
| `historicalItemIds` | At least one unless a justified domain boundary says otherwise |
| `modernSourceItemIds` | Zero or more, all uniquely owned globally |
| `publicContractItemIds` | Zero or more, all uniquely owned globally |
| `contractIds` | At least one |
| `freeVisionCoverage` | Concrete source comparison or justified `NotApplicable` |

## HistoricalItem

| Field | Rule |
|---|---|
| `itemId` | Stable `H###` identifier, unique |
| `ledgerPath` | Exact repository-relative `.cc` path |
| `domainId` | Exactly one valid domain |
| `contractIds` | At least one valid contract; every link is reciprocal in the matching contract array |
| `ledgerStatus` | Current canonical M-07 status |
| `omissionRationale` | Required for consciously omitted rows |
| `reevaluationTrigger` | Required for consciously omitted rows |

## ModernSourceItem

| Field | Rule |
|---|---|
| `itemId` | Stable `M###` identifier, unique |
| `path` | Exact repository-relative maintained `.cs` path |
| `assembly` | One of the five framework assemblies |
| `domainId` | Exactly one valid domain |
| `contractIds` | At least one valid contract; every link is reciprocal in the matching contract array |

## PublicContractItem

| Field | Rule |
|---|---|
| `itemId` | Stable `P###` identifier, unique |
| `assembly` | One of the five framework assemblies |
| `fullTypeName` | Exact exported type name discovered by reflection |
| `domainId` | Exactly one valid domain |
| `contractIds` | At least one valid contract; every link is reciprocal in the matching contract array |

## FrameworkContract

| Field | Rule |
|---|---|
| `contractId` | Stable `C###` identifier, unique |
| `domainId` | Exactly one owning domain |
| `nameDe` / `nameEn` | Non-empty bilingual contract name |
| `historicalItemIds` | One or more unless historical direct-equivalent absence is explicit; every listed link is reciprocal |
| `modernSourceItemIds` | One or more unless consciously omitted; every listed link is reciprocal |
| `publicContractItemIds` | Zero or more; every listed link is reciprocal |
| `borlandSources` | Concrete docs and/or `tv203s` paths |
| `freeVisionSourceIds` | Concrete external source records or justified no-match record |
| `historicalIntent` | Own-word behavioral summary |
| `observedBehavior` | Current TuiVision behavior summary |
| `primaryDecision` | Exactly one allowed primary term |
| `freeVisionRelation` | Exactly one allowed relation |
| `modernCSharpRationale` | Required for `IntentionalModernization` |
| `proof` | Concrete test/evidence or explicit gap |
| `risk` | Non-empty risk or `None` |
| `findingId` | Required only for drift/gap; forbidden otherwise |
| `downstreamDisposition` | Finding disposition or `None` |

## ExternalSourceRecord

| Field | Rule |
|---|---|
| `sourceId` | Stable `FV###` identifier |
| `repository` | Official FPC URL |
| `commit` | Approved immutable revision |
| `path` | Repository-relative Free Vision source path |
| `sha256` | Hash of reviewed file at the pinned revision |
| `retrievedAt` | ISO timestamp or date |
| `behavioralScope` | Own-word summary, no substantial source excerpt |
| `provenanceNote` | License/no-copy boundary |

## AuditFinding

| Field | Rule |
|---|---|
| `findingId` | Stable `F###` identifier, unique |
| `contractId` | Exactly one drift/gap contract |
| `severity` | `Critical`, `High`, `Medium`, or `Low` |
| `consumerScope` | `Wave5`, `Wave6`, or `Both` |
| `observation` | Reproducible behavior or proof gap |
| `reproduction` | Concrete path that demonstrates the behavior or missing proof |
| `impact` | User/API/data/security/A11Y/platform impact |
| `owner` | Named role or maintainer |
| `sourceEvidence` | One or more existing TuiVision, historical, or consumer-source paths |
| `acceptanceBoundary` | Concrete later proof |
| `nonGoals` | Explicit excluded work |
| `disposition` | Exactly one of `Core025`, `ComponentData026`, `Closure028`, `AcceptedFollowUp`, or `ProductDecision` |

## PresetObservation

| Field | Rule |
|---|---|
| `observationId` | Stable run-local identifier |
| `evidencePath` | Immutable feature or command evidence |
| `artifactKind` | Skill, command, runbook, template, checklist, evidence, or script requirement |
| `projectSpecificExclusions` | TuiVision-only details that must not be promoted |
| `providerNeutralRule` | Candidate portable behavior |
| `occurrenceCount` | Positive integer |
| `confidence` | Low, Medium, or High |
| `decision` | Retrospective vocabulary from the installed preset |

## Relationships

```text
AuditRun 1 --- 16 AuditDomain
AuditDomain 1 --- * HistoricalItem
AuditDomain 1 --- * ModernSourceItem
AuditDomain 1 --- * PublicContractItem
AuditDomain 1 --- * FrameworkContract
FrameworkContract * --- * inventory items
FrameworkContract * --- * ExternalSourceRecord
FrameworkContract 1 --- 0..1 AuditFinding
AuditRun 1 --- * PresetObservation
```

## State Transitions

```text
InventoryItem: Discovered -> Assigned -> Linked -> Validated
FrameworkContract: Draft -> SourceReviewed -> BehaviorReviewed -> Decided -> Validated
AuditFinding: Proposed -> Classified -> Owned -> Routed
PreWave5Gate: Open -> Blocked | Conditional -> Pass
PresetObservation: Observed -> Promote | ObserveAgain | RejectProjectSpecific | Superseded | NoPromotion
```

No contract may reach `Validated` with an unknown decision, a one-sided
inventory relation, a missing source-evidence path, or an unresolved finding
requirement. Contract inventory arrays are supporting evidence links rather
than an exhaustive dependency graph; consumer-specific semantic evidence lives
on the finding when a reviewed Wave-5 or Wave-6 flow exposes the gap.
