# Datenmodell: Wave-6 Combined Delta Closure

## ClosureRun

| Field | Rule |
|---|---|
| `schemaVersion` | exactly `1.0` |
| `runId` | exactly `037-wave6-combined-delta-closure` |
| `deliveryMode` | exactly `MergeAndSync` after renewed authority |
| `candidateDecision` | `ActiveAudit`, `Blocked` or `ReadyForDelivery` |
| `wave6State` | `BlockedPendingDelivery` on the reviewed feature head; `Closed` only in causal post-merge evidence |
| `portfolioAuditState` | `BlockedPendingWave6Closure` on the reviewed feature head; `Eligible` only after causal closure |
| Review fields | owner, reviewer, date, result, risk and trigger are non-empty |

## ProductDelta

Exactly five records bind PR #101 through #105. Each record contains role,
base/head/merge commit, exact changed-file set, set hash and product-path set.
Only `FunctionalProduct` and `ShowcaseProduct` may contain product paths.

## AcceptedInput

Each binding predecessor artifact has a unique path, role, lowercase SHA-256,
result and re-evaluation trigger.

## HistoricalSource

Exactly 24 records cover every file directly under `TVFM/`. Each contains path,
Git blob, canonical SHA-256, hash mode, historical role and reciprocal combined
area IDs. Text normalization is allowed only for `.PAS` and `.BAT`.

## FunctionalProof

Exactly ten records bind `W6-001` through `W6-010` to concrete Feature-035
proof names, framework decisions and evidence paths.

## ShowcaseProof

Exactly ten records bind `W6S-001` through `W6S-010` to visible access,
normal/constrained layout, focus/status/Description/keyboard and cell proof.

## CombinedArea

Exactly ten records `W6C-001` through `W6C-010` contain:

- one unique functional area;
- one unique known showcase area;
- one or more historical source paths;
- the `Tp7FileManager` entry point;
- state, app-loop, view, focus, dialog, status, Description, keyboard and cell
  proof boundaries as applicable;
- framework contracts and local composition;
- safety, A11Y and platform boundaries;
- nine dimensions and one primary decision;
- evidence, residual risk and re-evaluation trigger.

Dimensions are `behavior`, `interaction`, `layout`, `proof`, `documentation`,
`a11y`, `platform`, `security` and `frameworkReuse`. Values are only `Pass`,
`IntentionalDeviation`, `Gap` or `N/A`.

Primary decisions are only `AcceptedAsIs`,
`AcceptedIntentionalDeviation`, `CandidateFinding` or `ProductDecision`.
Accepted rows cannot contain `Gap`.

## EntryPoint

Exactly one `Tp7FileManager` record binds project, guide, normal command,
`--smoke` command, first visible state, primary action, F1, `Ctrl+Q`, app-loop,
view/focus/status/cell proof and platform boundary.

## CandidateFinding

A finding uses a stable `W6D###` ID and contains category, reproduction,
affected areas, evidence, owner, follow-up boundary and trigger. A non-empty
finding set forces `candidateDecision = Blocked`.

## GovernanceDecision

Each applicable checkpoint records preset/version, applicability
(`Applicable`, `N/A` or `Open`), rationale, evidence, owner, reviewer, date,
result, residual risk, follow-up and re-evaluation trigger.

## ValidationEvidence

Each gate records ID, scope, command, platform, candidate identity, result,
metric, evidence, failure boundary and trigger. Exact-head Ubuntu, macOS,
Windows and remote delivery gates are mandatory under `MergeAndSync`; no
provider gate may be reported as `Pass` before its reviewed-head execution.

## State Transition

```text
ActiveAudit
  -> Blocked             on drift, finding, decision or gate failure
  -> ReadyForDelivery    after complete local validation
  -> feature merge       after exact-head provider and review convergence
  -> Closed              in one causal post-merge closeout
  -> Portfolio Eligible  without starting Feature 038
```

Feature 037 performs the full transition set under renewed `MergeAndSync`
authority while preserving the non-recursive causal boundary.
