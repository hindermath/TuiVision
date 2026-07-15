# Data Model: Pre-Wave-5 and Wave-6 Conformance Closure

## ClosureRun

| Field | Type | Rules |
|---|---|---|
| `runId` | string | exactly `028-pre-wave5-wave6-conformance-closure` |
| `schemaVersion` | string | exactly `1.0` |
| `auditFeature` | string | exactly `024-tv203-freevision-conformance-audit` |
| `ownerFeatures` | string array | exactly 025 and 026, in dependency order |
| `auditRevision` | integer | exactly `2` |
| `reviewDate` | date | ISO date |
| `deliveryMode` | enum | exactly `MergeAndSync` in run evidence, not product behavior |
| `existingGateDecision` | enum | `ReadyForTerminalGuiAudit` or `Blocked` |
| `wave5State` | enum | exactly `BlockedPendingTerminalGuiAudit` |
| `wave6State` | enum | exactly `BlockedPendingTerminalGuiAudit` |
| `nextIntake` | string | exactly `029-tv203-freevision-terminalgui-conformance-audit` |
| `owner` / `reviewer` / `reviewDate` | strings/date | non-empty roles and ISO date |
| `evidencePath` / `result` | strings | repository evidence path and explicit result |
| `residualRisk` / `followUp` | strings | non-empty; `None` only with rationale |
| `reevaluationTrigger` | string | concrete protected-contract or proof change |

## FindingClosure

Exactly thirteen rows exist, one for every `F001` through `F013`.

| Field | Rules |
|---|---|
| `findingId` | unique and complete `F001`-`F013` |
| `contractId` | existing canonical `C001`-`C048` and equal to the finding's contract |
| `ownerFeature` | 025 for F001-F009; 026 for F010-F013 |
| `originalObservation` | non-empty, unchanged from canonical finding |
| `mergedChangePaths` | non-empty existing repository paths from canonical resolution |
| `redProof` | non-empty canonical failure boundary |
| `realPathProofs` | non-empty `path::method` references that exist |
| `historicalIntent` | non-empty concise retained intent |
| `freeVisionRelation` | non-empty accepted relation summary |
| `consumerScope` | `Wave5`, `Wave6`, or `Both` |
| `apiImpact` | explicit impact or `None` |
| `a11yImpact` | explicit impact or `None` |
| `platformImpact` | explicit impact or `None` |
| `closureDecision` | `Closed`, `AlreadySatisfiedWithNewProof`, `Reopened025`, `Reopened026`, or `ProductDecision` |
| `evidencePath` | repository path to canonical and Feature-028 evidence |
| `owner` / `reviewer` | non-empty roles |
| `reviewDate` | ISO date |
| `residualRisk` | non-empty, including justified `None identified` |
| `followUp` | non-empty, including justified `None` |
| `reevaluationTrigger` | non-empty concrete trigger |

## IntegrationSlice

Exactly seven rows exist, `R-028-001` through `R-028-007`.

| Field | Rules |
|---|---|
| `sliceId` | unique and complete sequential identifier |
| `title` | concise accepted boundary |
| `contractIds` | one or more canonical contracts |
| `findingIds` | one or more canonical findings |
| `productionEntry` | named real entry point or public path |
| `proofReferences` | non-empty existing `path::method` values |
| `assertions` | concrete state, lifecycle, view, cell, or data claims |
| `negativeOrFallback` | required rejection, cancel, fallback, or lifecycle boundary |
| `helperRole` | `PrimaryProof` or `SupplementalProof` with rationale |
| `proofLimit` | non-empty statement of what the slice does not prove |
| `result` | `Pass`, `Fail`, or `Open` |
| `platforms` | explicit local and remote applicability |
| `a11yEvidence` | explicit keyboard, focus, text, or `N/A` rationale |
| `owner` / `reviewer` / `reviewDate` | non-empty roles and ISO date |
| `evidencePath` | repository path to proof source and feature evidence |
| `residualRisk` / `followUp` | non-empty, including justified `None` |
| `reevaluationTrigger` | concrete production-entry, contract, or proof change |

## ConsumerReadiness

The baseline contains exactly thirteen rows. A new row is allowed only for a
newly observed shared-framework responsibility and must not replace a baseline
row.

| Field | Rules |
|---|---|
| `consumerId` | stable `W5-001`-`W5-006` or `W6-001`-`W6-007`; additions continue per Wave |
| `sourcePaths` | one or more existing paths under `TVDEMOS/` or `TVFM/` |
| `flow` | concise consumer responsibility |
| `contractIds` | canonical shared contracts or explicit `ProductPolicy` |
| `findingIds` | canonical findings or empty only for product-policy row |
| `proofReferences` | current TuiVision proof or documented product-policy boundary |
| `decision` | `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, `FollowUpHardening`, or `ProductDecision` |
| `rationale` | explains shared reuse, deviation, or boundary |
| `wave` | `Wave5` or `Wave6` |
| `residualRisk` | non-empty |
| `followUpBoundary` | non-empty, including `None` when justified |
| `owner` / `reviewer` / `reviewDate` | non-empty roles and ISO date |
| `evidencePath` | repository path to baseline and current proof |
| `reevaluationTrigger` | concrete consumer, contract, or proof change |

## GovernanceDecision

| Field | Rules |
|---|---|
| `preset` / `version` | one installed preset and exact local version |
| `checkpoint` | stable feature-local checkpoint |
| `applicability` | `Applicable`, `N/A`, or `Open` |
| `rationale` | required |
| `evidencePath` | required repository path or command |
| `owner` / `reviewer` / `reviewDate` | required |
| `result` | explicit result |
| `residualRisk` | required |
| `followUp` | required for `Open`, otherwise explicit `None` |
| `reevaluationTrigger` | required for every row |

## AutonomousGateRequirement

The committed `autonomous-gate-requirements.json` follows the installed v0.2.0
schema. Each row has one stable gate ID, applicability, exact required scope,
command tokens, optional runner tokens, rationale, and re-evaluation trigger.
It is committed before implementation and is not rewritten to match later
provider output.

## ValidationEvidence

| Field | Rules |
|---|---|
| `validationId` | unique stable feature-local identifier |
| `command` / `scope` | exact invocation or review operation and bounded target |
| `candidateHead` | full staged or reviewed Git object ID where applicable |
| `runnerOrPlatform` | local platform or exact remote runner; `N/A` only with rationale |
| `result` / `metric` | `Pass`, `Fail`, `Open`, or justified `N/A`, plus count/rate when applicable |
| `failureBoundary` | explicit rejected condition or `None observed` |
| `evidencePath` | repository path or immutable external reference |
| `owner` / `reviewer` / `reviewDate` | non-empty roles and ISO date |
| `residualRisk` / `followUp` | non-empty, including justified `None` |
| `reevaluationTrigger` | concrete command, scope, dependency, workflow, or protected-path change |
| `buildCounter` | exact counter for `dotnet build`/`dotnet test`; otherwise `N/A` |

## TemporaryGateEvidence

The untracked provider-neutral evidence follows the installed v0.2.0 evidence
schema. It stores the SHA-256 of the committed requirements, exact reviewed
head, exactly one Primary row per gate, optional supplemental rows, provider,
immutable run ID, workflow, job, runner/platform, executed command, result, and
reference. Both Bash and PowerShell validators must pass. The file is deleted
or kept outside Git after merge.

## AutonomousRunState

The feature-local `autonomous-run-state.json` follows the installed v0.2.0
state schema. It binds the run ID, feature path, branch, current delivery
authority, accepted-artifact hashes, task hash and counts, stage, status,
checkpoint commit, last passing gate, last operation, and exact next action.
Both installed state validators must accept every persisted transition. The
state is an execution index; Git history, task checkboxes, and evidence remain
authoritative if they disagree with a stale index.

## State Transitions

```text
Open
  -> Blocked                     any finding/slice/shared consumer/gate fails
  -> LocalPass                   all fixed rows, additions, and local gates pass
LocalPass
  -> RemoteReview                reviewed PR head exists
RemoteReview
  -> ReadyForTerminalGuiAudit    exact-head gates and reviews converge
ReadyForTerminalGuiAudit
  -> Delivered                   merge and clean main sync complete
Delivered
  -> Open                        protected contract, proof, or consumer changes
```

Wave 5 and Wave 6 stay `BlockedPendingTerminalGuiAudit` through every state.
