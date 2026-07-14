# Acceptance Contract: Pre-Wave-5 and Wave-6 Conformance Closure

## Input Contract

- Feature-024 Revision 2 exists with 48 contracts, thirteen findings, and
  exactly thirteen final Feature-025/026 resolutions.
- Finding identities, observations, ownership, source relations, and accepted
  boundaries are immutable during Feature 028.
- `tv203s/`, `TVDEMOS/`, `TVFM/`, and pinned Free Vision remain read-only.
- The six baseline presets and `autonomous-run-governance` v0.1.4 are active.

## Finding Closure Contract

1. Exactly one row exists for every `F001` through `F013`.
2. Every successful row names a real-path proof and complete review metadata.
3. Canonical finding, resolution, contract, path, and proof relations reconcile
   bidirectionally.
4. Unknown decisions, duplicate IDs, missing links, malformed JSON, and wrong
   cardinalities fail closed.
5. `Reopened025`, `Reopened026`, or `ProductDecision` blocks the run and does
   not trigger a product fix inside Feature 028.

## Integration Contract

- Exactly seven slices cover real keyboard ingress; focus/state/validation;
  idle/command/shutdown; desktop/close/modal; generic drag/keyboard parity;
  dialog/validator/rejection; and file/resource boundaries.
- Each slice records entry point, proof references, assertions, negative or
  fallback behavior, helper role, proof limit, A11Y relevance, and platforms.
- Each slice also records owner, reviewer, review date, evidence path, result,
  residual risk, follow-up, and re-evaluation trigger.
- Existing tests may satisfy a slice only when their combined evidence reaches
  every named production boundary.
- Aggregate suite success, direct helpers, comments, and pre-normalized input
  cannot replace the named proof.

## Consumer Contract

- All six Wave-5 and seven Wave-6 Revision-2 groups remain present. Uniquely
  identified additions are allowed only for a genuinely newly observed shared
  framework responsibility.
- Every row has one readiness decision and complete contract/proof/risk plus
  owner/reviewer/date/evidence/re-evaluation traceability.
- `SmallFrameworkFix` and `ProductDecision` block closure.
- `FollowUpHardening` is allowed only for non-gate-critical application work.
- No consumer or historical source file changes.

## Validation Contract

- Targeted closure and slice tests pass.
- The complete Release suite passes.
- Core, Controls, Serialization, Compatibility, and Drivers.Console each reach
  at least 70 percent line coverage through the canonical collector.
- Format, DocFX, Playwright/Axe, Lynx, secret, dependency, generated-output,
  and protected-source checks pass.
- Ubuntu, macOS, and Windows execute the real restore/build/test/DocFX CI body
  on the reviewed pull-request head.
- Every committed remote gate requirement has exactly one valid Primary
  evidence row for the exact head; WSL remains an explicit `N/A` unless an
  actual WSL command runs.

## Gate Contract

- Full success sets only the existing TV203/Free Vision gate to
  `ReadyForTerminalGuiAudit`.
- Any required failure leaves it `Blocked` with owner and reproduction.
- Wave 5 and Wave 6 remain `BlockedPendingTerminalGuiAudit` in either outcome.
- Feature 029 is the sole next intake; Terminal.GUI analysis does not start in
  Feature 028.

## Delivery Contract

- Delivery mode is `MergeAndSync` under the current user authority.
- Technical checks must be green and actionable threads zero.
- Missing reviewers remain missing, not approved.
- The narrow bypass may address only the Human Approval rule after all
  technical proof is present.
- Post-merge facts use one non-recursive closeout.
- Final local `main` is clean and exactly equals `origin/main`.
