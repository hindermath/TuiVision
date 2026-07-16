# Data Model: Gemeinsamer Konformitätsabschluss

## 1. ClosureRun

One root object describing the independent run.

| Field | Rule |
|---|---|
| `schemaVersion` | exactly `1.0` |
| `runId` | exactly `031-combined-conformance-closure` |
| `baselineCommit` | full Git object ID from the starting synchronized `main` |
| `sourceFeatures` | exactly 024, 025, 026, 028, 029, 030 in dependency order |
| `deliveryMode` | exactly `MergeAndSync` |
| `featureHeadGateDecision` | `ReadyForMerge` or `Blocked` |
| `featureHeadWave5State` | blocked until causal closeout |
| `featureHeadWave6State` | blocked until causal closeout |
| `postMergeWave5Target` | exactly `Eligible` |
| `postMergeWave6Target` | exactly `ConditionallyReady` |
| review fields | owner, reviewer, review date, evidence path, result, risk, follow-up, trigger |

State transition:

```text
ActiveClosure
  -> Blocked                     when any mandatory invariant fails
  -> ReadyForMerge               when all feature-head gates pass
  -> MergedPendingCausalCloseout after reviewed feature merge
  -> Completed                   after evidence-only Wave/state closeout
```

The validator derives the allowed state from causal evidence:

- no `delivery-closeout.md`: only the blocked feature-head state is valid;
- complete closeout with exact reviewed head and feature merge: only
  `Eligible` / `ConditionallyReady` final markers are valid;
- partial closeout, final markers without merge evidence, or mixed marker
  states are invalid.

## 2. AcceptedInput

One immutable predecessor artifact used by the closure.

| Field | Rule |
|---|---|
| `featureId` | one of 024, 025, 026, 028, 029, 030 |
| `path` | repository-relative, unique, existing |
| `sha256` | lowercase 64-character SHA-256 |
| `role` | contract, finding, closure, source audit, combined disposition, or delivery evidence |
| `result` | `Pass` only when the current file hash matches |
| `reevaluationTrigger` | any content or path change |

## 3. ExternalSourceBaseline

One row for Free Vision, Terminal.GUI, or magiblot/tvision.

| Field | Rule |
|---|---|
| `sourceName` | `FreeVision`, `TerminalGUI`, or `MagiblotTvision` |
| `repository` | exact accepted repository URL |
| Git identity | accepted commit plus tag object or tree where applicable |
| license identity | exact accepted license/COPYRIGHT hash |
| `manifestPath` | accepted source manifest |
| `expectedSourceCount` | 15, 25, or 50 |
| `sourceIds` | exact unique closed set |
| `result` | `Pass` only after manifest and current provenance checks |
| review fields | complete owner/reviewer/date/evidence/risk/follow-up/trigger |

## 4. ContractClosure

Exactly 48 rows, ordered `C001` through `C048`.

| Field | Rule |
|---|---|
| `contractId` | exact unique `C001`-`C048` |
| `domainId` | existing `D01`-`D16` |
| `feature024Decision` | accepted Revision-2 decision |
| `freeVisionRelation` | accepted Feature-024 relation |
| `terminalGuiRelation` | accepted Feature-029 relation |
| `tgoObservationId` | exact `TGO001`-`TGO048` relation |
| `magiblotRelation` | accepted Feature-030 relation |
| `mbObservationId` | exact `MB001`-`MB048` relation |
| `combinedDisposition` | exactly the accepted Feature-030 disposition |
| `proof` | existing repository `path::method` reference |
| `consumerIds` | reciprocal subset of the 13 consumer IDs |
| `result` | `Pass` only with all predecessor links equal |
| review fields | complete metadata and residual boundary |

## 5. ConsumerClosure

Exactly 13 rows: `W5-001`-`W5-006`, then `W6-001`-`W6-007`.

Fields preserve the Feature-028 source paths, flow, contract IDs, finding IDs,
proof references, decision, Wave, risk, and follow-up. Feature-029 and
Feature-030 consumer rows must agree on identity, Wave, source paths, and
contract set. Any difference blocks closure.

## 6. ObservationClosure

Exactly 96 rows:

- `TGO001`-`TGO048`
- `MB001`-`MB048`

| Field | Rule |
|---|---|
| `observationId` | exact unique closed set |
| `sourceFeature` | 029 for TGO, 030 for MB |
| `contractId` | existing `C001`-`C048` |
| `sourceDecision` | accepted audit decision |
| `combinedDisposition` | exactly `NonFinding` for the accepted baseline |
| `canonicalFindingId` | exactly `None` for the accepted baseline |
| `rationale` | non-empty accepted explanation |
| `residualRisk` | non-empty |
| `reevaluationTrigger` | non-empty |
| `result` | `Pass` after source and combined rows reconcile |

## 7. PriorFindingClosure

Exactly 13 rows ordered `F001` through `F013`.

Each row links:

- Feature-024 finding and contract;
- Feature-025 or Feature-026 owner;
- Feature-024 final resolution;
- Feature-028 closure decision and real-path proofs;
- current proof existence;
- result and complete review metadata.

Allowed result is `Closed`. A missing proof, changed owner, reopened decision,
or absent source row blocks Feature 031.

## 8. OwnerGroup

Exactly three rows:

- `CoreRuntimeDriver`
- `ComponentDataInteraction`
- `CrossCuttingA11YProof`

For the accepted baseline, every `findingIds` array is empty. A non-empty row
requires a matching canonical finding and hardening intake; because none is
accepted, any non-empty row blocks closure.

## 9. GovernanceDecision

One or more rows per installed preset checkpoint.

Required fields:

- `runId`
- `presetName`
- `presetVersion`
- `checkpoint`
- `applicability`: `Applicable`, `N/A`, or `Open`
- `rationale`
- `evidencePath`
- `owner`
- `reviewer`
- `reviewDate`
- `result`
- `residualRisk`
- `followUp`
- `reevaluationTrigger`

`N/A` requires rationale and trigger. `Open` requires an owner and concrete
follow-up. No empty starter row is valid.

## 10. ValidationEvidence

One row per declared acceptance gate.

| Field | Rule |
|---|---|
| `gateId` | stable and unique |
| `scope` | actual proof boundary |
| `command` | exact command or provider-fact description |
| `platform` | local, Ubuntu, macOS, Windows, or provider |
| `candidateHead` | exact head where applicable |
| `result` | `Pass`, `Fail`, `N/A`, `Open`, or `TrackedExternally` |
| `countOrMetric` | test count, coverage, warning count, thread count, etc. |
| `evidencePath` | repository or provider evidence |
| `failureBoundary` | explicit when not Pass |
| review fields | complete metadata |

## 11. WaveTransition

The causal contract has two distinct observations:

```text
Feature head:
  Wave 5 = BlockedPendingCausalClosure
  Wave 6 = BlockedPendingCausalClosure

After reviewed feature merge and causal closeout:
  Wave 5 = Eligible
  Wave 6 = ConditionallyReady
```

The transition records prerequisite checks, reviewed head, feature merge,
evidence path, owner, reviewer, result, residual risk, and the Wave-5
completion/delta-review trigger for Wave 6.

All marker-consuming tests and maintained status files use this same
transition contract. The closeout may change evidence and status files but
must not change executable test logic.
