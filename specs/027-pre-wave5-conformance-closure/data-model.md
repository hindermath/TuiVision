# Data Model: Pre-Wave-5 Conformance Closure

## ClosureRun

| Field | Type | Rules |
|---|---|---|
| `RunId` | string | stable Feature-027 identifier |
| `AuditBaselineSha` | SHA | merged Feature-024 product baseline |
| `ClosureHeadSha` | SHA | reviewed closure head or `Pending` before commit |
| `DeliveryMode` | enum | exactly `MergeAndSync` |
| `GateState` | enum | `Open`, `Blocked`, `Passed`, `Released` |
| `NextIntake` | string | Wave 5 only after `Released` |

## BaselineSnapshot

| Field | Expected value |
|---|---:|
| Domains | 16 |
| Contracts | 48 |
| Historical items | 151 |
| Modern source files | 119 |
| Exported public types | 176 |
| External source records | 15 |
| Proof references | 94 |
| Aligned / Modernized / Omitted / Drift / Gap | 13 / 34 / 1 / 0 / 0 |
| Findings | 0 |

## RevalidationCheck

| Field | Rules |
|---|---|
| `CheckId` | unique `CL-027-NNN` |
| `Boundary` | one inventory, proof, validation, governance, scope, or delivery boundary |
| `Result` | exactly `Pass`, `Fail`, `N/A`, or `Open` |
| `CommandOrProof` | concrete command, query, path, or provider evidence |
| `Baseline` | expected count, hash, status, or scope |
| `Observed` | actual result |
| `Owner` | required |
| `Reviewer` | required |
| `ReviewDate` | required |
| `ResidualRisk` | required, including `None identified` when justified |
| `FollowUp` | required for `Open` or `Fail` |
| `ReevaluationTrigger` | required for every row |

## Wave5GateDecision

| Field | Rules |
|---|---|
| `Decision` | `Blocked` or `Released` |
| `FindingSets` | `Core025`, `ComponentData026`, follow-up, product decision counts |
| `RequiredChecks` | all `CL-027-*` identifiers |
| `Rationale` | explains why release is or is not allowed |
| `EvidencePath` | points to `closure-evidence.md` |
| `NextIntake` | Wave 5 only when released |

## PortableObservation

| Field | Rules |
|---|---|
| `ObservationId` | stable `PO-027-NNN` |
| `ArtifactKind` | skill, command, template, checklist, runbook, evidence, script, or project-specific |
| `ProjectExclusions` | required |
| `GenericRule` | provider-neutral or `N/A` |
| `OccurrenceCount` | positive integer |
| `Confidence` | Low, Medium, High |
| `Reproduction` | deterministic test |
| `Decision` | Promote, ObserveAgain, RejectProjectSpecific, Superseded, NoPromotion |

## State Transitions

```text
Open -> Blocked   when any required check fails or finding set opens
Open -> Passed    when all local checks pass and protected scope is unchanged
Passed -> Released after remote convergence and merged formal status
Released -> Open  when a later protected contract or proof changes
```
