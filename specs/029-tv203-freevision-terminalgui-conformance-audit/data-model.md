# Data Model: Terminal.GUI Conformance Audit

## TerminalGuiAuditRun

| Field | Rule |
|---|---|
| `schemaVersion` | exactly `1.0` |
| `runId` | exactly `029-tv203-freevision-terminalgui-conformance-audit` |
| `canonicalAuditFeature` | exactly `024-tv203-freevision-conformance-audit` |
| `closureFeature` | exactly `028-pre-wave5-wave6-conformance-closure` |
| `reviewDate` | ISO date |
| `terminalGuiRepository` | official GitHub URL |
| `terminalGuiTag` | exactly `v1.9.0` |
| `terminalGuiTagObject` | exact 40-character binding object |
| `terminalGuiCommit` | exact 40-character peeled commit |
| `terminalGuiLicense` | exactly `MIT` |
| `licenseSha256` | lowercase SHA-256 |
| `existingContractCount` | exactly `48` |
| `newContractCount` | zero unless every C049+ admission condition passes |
| `wave5State` / `wave6State` | blocked values defined by the gate |
| `nextIntake` | exactly `030-tv203-magiblot-evolution-audit` |

## TerminalGuiSourceRecord

| Field | Rule |
|---|---|
| `sourceId` | stable unique `TGSR###` |
| `path` | relative path inside pinned Terminal.GUI checkout |
| `sourceKind` | `Production`, `UnitTest`, or `License` |
| `sha256` | lowercase SHA-256 at the pinned commit |
| `behavioralScopeDe` / `behavioralScopeEn` | short own-word summaries |
| `contractIds` | one or more valid contract IDs |
| `retrievedAt` | ISO date |
| `permalink` | optional URL containing the pinned commit |
| `noCopyBoundary` | non-empty provenance statement |

Every source-to-contract relation is reciprocal.

## TerminalGuiContractRelation

Exactly 48 existing rows cover `C001` through `C048`.

| Field | Rule |
|---|---|
| `contractId` | unique, complete existing contract ID |
| `domainId` | matches the canonical Feature-024 contract |
| `nameDe` / `nameEn` | matches canonical names |
| `terminalGuiSourceIds` | valid source records or empty only for justified `NotApplicable` |
| `terminalGuiRelation` | `CorroboratesOriginal`, `CorroboratesModernization`, `AlternativeModernization`, `DivergesFromTuiVision`, or `NotApplicable` |
| `rationaleDe` / `rationaleEn` | observable responsibility comparison |
| `tuiVisionProof` | existing repository `path::method` or evidence path |
| `consumerIds` | valid Wave-5/Wave-6 consumer IDs or explicit empty rationale |
| `risk` | non-empty |
| `findingId` | `None` or one valid `TG###` |
| `reevaluationTrigger` | required for every `NotApplicable`, otherwise concrete source/contract/proof trigger |

## TerminalGuiConsumerReview

The baseline retains all six Wave-5 and seven Wave-6 rows accepted by Feature
028. Additions are permitted only for a new shared-framework responsibility.

| Field | Rule |
|---|---|
| `consumerId` | stable existing `W5-001`-`W5-006` or `W6-001`-`W6-007`; additions continue per Wave |
| `sourcePaths` | one or more existing read-only `TVDEMOS/` or `TVFM/` paths |
| `flow` | concise consumer responsibility |
| `contractIds` | valid canonical contracts or explicit product-policy boundary |
| `terminalGuiSourceIds` | relevant advisory evidence or justified empty set |
| `currentProof` | existing TuiVision proof or product-policy evidence |
| `decision` | `UseExistingFramework`, `IntentionalDeviation`, `FollowUpHardening`, `CandidateFinding`, or `ProductDecision` |
| `rationale` | explains reuse, deviation, observation, or boundary |
| `wave` | `Wave5` or `Wave6` |
| `risk` / `residualRisk` | non-empty |
| `followUpBoundary` | non-empty, including justified `None` |
| `owner` / `reviewer` / `reviewDate` | non-empty roles and ISO date |
| `reevaluationTrigger` | concrete consumer, contract, source, or proof change |

## TerminalGuiObservation

| Field | Rule |
|---|---|
| `findingId` | stable unique `TG###` or a non-finding observation ID |
| `contractId` / `domainId` | valid reciprocal IDs |
| `observation` / `reproduction` | concrete and reproducible |
| `historicalIntent` | retained TV203 responsibility |
| `freeVisionRelation` / `terminalGuiRelation` | explicit comparisons |
| `consumerScope` | `Wave5`, `Wave6`, or `Both` |
| `tuiVisionSource` / `currentProof` | existing paths |
| `missingProofOrBehavior` | explicit gap or `None` |
| `risk` | non-empty |
| `primaryOwner` | exactly one owner group |
| `dependencies` | zero or more observation IDs, acyclic |
| `requiredRedProof` / `requiredRealPathGreenProof` | complete for candidate findings |
| `apiImpact` / `a11yImpact` / `platformImpact` | explicit impact or `None` |
| `suggestedBoundary` | bounded later work |
| `decision` | `CandidateFinding`, `IntentionalDeviation`, `AlreadySatisfiedWithNewEvidence`, `ProductDecision`, or `RejectedComparison` |
| `evidencePath` / `owner` / `reviewer` / `reviewDate` | required |
| `residualRisk` / `reevaluationTrigger` | required |
| `deduplicationKey` | stable value consumed by Feature 030 |

## Feature030Handoff

| Field | Rule |
|---|---|
| `schemaVersion` | exactly `1.0` |
| `sourceFeature` | exactly Feature 029 |
| `targetFeature` | exactly Feature 030 |
| `terminalGuiTagObject` / `terminalGuiCommit` | exact pinned values |
| `contractIds` | exact Feature-029 contract set |
| `observationIds` | all finding and non-finding observations |
| `candidateFindingIds` | complete subset with `CandidateFinding` decision |
| `ownerGroups` | provisional owner groups represented by observations |
| `dependencyEdges` | acyclic observation dependencies |
| `deduplicationKeys` | unique and complete |
| `hardeningLastenhefteCreated` | exactly `false` |
| `closureLastenheftCreated` | exactly `false` |
| `nextIntake` | exactly Feature 030 |
| `wave5State` / `wave6State` | blocked |

## GovernanceDecision

| Field | Rule |
|---|---|
| `preset` / `version` | one installed preset and exact version |
| `checkpoint` | stable feature-local checkpoint |
| `applicability` | `Applicable`, `N/A`, or `Open` |
| `rationale` / `evidencePath` | required |
| `owner` / `reviewer` / `reviewDate` | required |
| `result` / `residualRisk` | required |
| `followUp` | required for `Open`, otherwise explicit `None` |
| `reevaluationTrigger` | required for every row |

## ValidationEvidence

| Field | Rule |
|---|---|
| `validationId` | stable unique ID |
| `command` / `scope` | exact operation and bounded target |
| `candidateHead` | full Git object where applicable |
| `runnerOrPlatform` | local or exact remote runner |
| `result` / `metric` | explicit result and count/rate |
| `failureBoundary` | rejected condition or `None observed` |
| `evidencePath` | repository or immutable external reference |
| `owner` / `reviewer` / `reviewDate` | required |
| `residualRisk` / `followUp` / `reevaluationTrigger` | required |
| `buildCounter` | exact value for build/test commands; otherwise `N/A` |

## Relationships

```text
TerminalGuiAuditRun 1 --- * TerminalGuiSourceRecord
TerminalGuiAuditRun 1 --- 48 TerminalGuiContractRelation
TerminalGuiAuditRun 1 --- * TerminalGuiConsumerReview
TerminalGuiContractRelation * --- * TerminalGuiSourceRecord
TerminalGuiContractRelation 1 --- 0..1 TerminalGuiObservation
TerminalGuiObservation * --- * TerminalGuiObservation dependency DAG
TerminalGuiAuditRun 1 --- 1 Feature030Handoff
TerminalGuiAuditRun 1 --- * GovernanceDecision
TerminalGuiAuditRun 1 --- * ValidationEvidence
```text

## State Transitions

```text
SourceRecord: Discovered -> Pinned -> Hashed -> Linked -> Validated
ContractRelation: Draft -> SourceReviewed -> ProofReviewed -> Decided -> Validated
Observation: Proposed -> Reproduced -> Classified -> Owned -> HandedOff
Feature029Gate: Open -> Blocked | LocalPass -> RemoteReview -> Delivered
Feature030Handoff: Draft -> Reconciled -> Validated -> Delivered
```text

Wave 5 and Wave 6 remain blocked in every Feature-029 state.
