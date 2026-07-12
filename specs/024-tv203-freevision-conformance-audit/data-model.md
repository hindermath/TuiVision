# Data Model: TV203 and Free Vision Conformance Audit

## AuditRun

| Field | Rule |
|---|---|
| `runId` | Stable feature-scoped identifier, exactly one for 024 |
| `auditDate` | ISO date |
| `historicalLedgerPath` | Repository-relative canonical ledger path |
| `historicalRowCount` | Must equal current filesystem and ledger count; planning baseline 151 |
| `modernSourceCount` | Must equal maintained source inventory at validation time; planning baseline 119 |
| `freeVisionRepository` | Exact official upstream URL |
| `freeVisionCommit` | Exact 40-character approved commit |
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
| `contractIds` | At least one valid contract |
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
| `contractIds` | At least one valid contract |

## PublicContractItem

| Field | Rule |
|---|---|
| `itemId` | Stable `P###` identifier, unique |
| `assembly` | One of the five framework assemblies |
| `fullTypeName` | Exact exported type name discovered by reflection |
| `domainId` | Exactly one valid domain |
| `contractIds` | At least one valid contract |

## FrameworkContract

| Field | Rule |
|---|---|
| `contractId` | Stable `C###` identifier, unique |
| `domainId` | Exactly one owning domain |
| `nameDe` / `nameEn` | Non-empty bilingual contract name |
| `historicalItemIds` | One or more unless historical direct-equivalent absence is explicit |
| `modernSourceItemIds` | One or more unless consciously omitted |
| `publicContractItemIds` | Zero or more |
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
| `observation` | Reproducible behavior or proof gap |
| `impact` | User/API/data/security/A11Y/platform impact |
| `owner` | Named role or maintainer |
| `acceptanceBoundary` | Concrete later proof |
| `nonGoals` | Explicit excluded work |
| `disposition` | Exactly one allowed downstream term |

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

No contract may reach `Validated` with an unknown decision, missing relation,
invalid inventory link, or unresolved finding requirement.
