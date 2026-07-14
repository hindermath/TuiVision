# Detailed Plan Review: Pre-Wave-5 and Wave-6 Conformance Closure

**Purpose**: Challenge proof feasibility, data integrity, platform evidence, and
delivery causality before task generation.
**Created**: 2026-07-14

## Canonical Finding Reconciliation

- [x] PR001 Can the validator join exactly thirteen Feature-024 findings to exactly thirteen final resolutions without changing either source? [Data Model - FindingClosure]
  - Durchfuehrungshinweis: Inspect the canonical JSON shape and test how duplicate, missing, unknown, or cross-owned IDs will fail.
- [x] PR002 Are owner rules `F001`-`F009 -> 025` and `F010`-`F013 -> 026` explicit and machine-checkable? [Data Model - FindingClosure]
  - Durchfuehrungshinweis: Compare owner fields in canonical resolutions and reject inferred ownership based only on path names.
- [x] PR003 Do proof references use existing `path::method` values and verify both path and method existence? [Contract - Finding Closure Contract]
  - Durchfuehrungshinweis: Enumerate current canonical proof references and match them against test source methods.
- [x] PR004 Do reopening decisions fail closed without turning Feature 028 into a product-fix run? [Contract - Finding Closure Contract]
  - Durchfuehrungshinweis: Simulate each non-closed decision and confirm the planned state becomes `Blocked` with an external owner.

## Seven Integration Slices

- [x] PR005 Does R-028-001 prove raw keyboard translation, concrete event kinds, modifier separation, and command routing? [Plan - Phase 3]
  - Durchfuehrungshinweis: Read the named event, program, window, and command tests and identify the production entry for each claim.
- [x] PR006 Does R-028-002 prove atomic focus transfer, state propagation, and validation rejection? [Plan - Phase 3]
  - Durchfuehrungshinweis: Trace `TrySetFocus`, `CanReleaseFocus`, state propagation, and InputLine acceptance tests through real owners.
- [x] PR007 Does R-028-003 prove pending-event ordering, idle behavior, command-state refresh, and shutdown? [Plan - Phase 3]
  - Durchfuehrungshinweis: Trace the event loop and assert that direct queue helpers do not replace `TProgram` behavior.
- [x] PR008 Does R-028-004 prove desktop stack, close, modal, and application lifecycle boundaries? [Plan - Phase 3]
  - Durchfuehrungshinweis: Map F005/F006 tests to desktop, window, dialog, and application production methods.
- [x] PR009 Does R-028-005 prove bounded generic drag plus keyboard parity and cancellation/fallback? [Plan - Phase 3]
  - Durchfuehrungshinweis: Map F009 and program mouse integration tests to title-row drag, bounds, release, Escape, capability loss, and Ctrl+F5.
- [x] PR010 Does R-028-006 prove dialog completion, validator phases, focus retention, and rejection observability? [Plan - Phase 3]
  - Durchfuehrungshinweis: Inspect F010/F011 tests and ensure both child-consumed command and cancel bypass boundaries remain covered.
- [x] PR011 Does R-028-007 prove file outcomes, resource composition, malformed persistence, and deterministic menu/status descriptions? [Plan - Phase 3]
  - Durchfuehrungshinweis: Inspect F012/F013 and description tests for explicit typed outcomes, limits, unknown types, cycles, and first-match order.

## Consumer Readiness

- [x] PR012 Are the six Wave-5 consumer groups preserved exactly from Revision 2? [Data Model - ConsumerReadiness]
  - Durchfuehrungshinweis: Extract `W5-*` rows from the current review and compare source paths, flows, contracts, and findings.
- [x] PR013 Are the seven Wave-6 consumer groups preserved exactly from Revision 2? [Data Model - ConsumerReadiness]
  - Durchfuehrungshinweis: Extract `W6-*` rows and compare the same fields without starting consumer implementation.
- [x] PR014 Do `SmallFrameworkFix` and `ProductDecision` block while non-critical `FollowUpHardening` remains explicit? [Contract - Consumer Contract]
  - Durchfuehrungshinweis: Evaluate every allowed decision against gate state, residual risk, and follow-up owner.

## Platform and Gate Evidence

- [x] PR015 Can the current CI workflow accept `windows-latest` without shell-specific command breakage? [Research - Decision 5]
  - Durchfuehrungshinweis: Read the whole CI job, action inputs, path syntax, and shell defaults; compare with the prior successful Windows run.
- [x] PR016 Do runtime gate command tokens match commands actually present in the workflow? [Gate requirements]
  - Durchfuehrungshinweis: Normalize multiline YAML commands and compare every required token for Ubuntu, macOS, and Windows.
- [x] PR017 Can the DocFX/A11Y, homogeneity, supply-chain, and secret gates each produce immutable exact-head evidence? [Gate requirements]
  - Durchfuehrungshinweis: Map each gate to workflow, job, runner, command, run ID, and immutable URL before accepting it as Primary.
- [x] PR018 Do both Bash and PowerShell evidence validators accept the planned requirements schema and reject stale heads? [Quickstart - Section 6]
  - Durchfuehrungshinweis: Inspect both validator implementations and later run positive and tampered-head cases.

## Closeout and Repository State

- [x] PR019 Are all three completed Lastenheft paths and all next-intake markers updated atomically? [Plan - Phase 5]
  - Durchfuehrungshinweis: Inventory Lastenheft 10, 11, 12 and every maintained agent/Pflichtenheft/statistics marker before task generation.
- [x] PR020 Does `delivery-closeout.md` record only post-merge facts and avoid a recursive PR? [Research - Decision 8]
  - Durchfuehrungshinweis: Separate pre-merge evidence from merge SHA, branch deletion, clean-main sync, retrospective, and preset outcome.
- [x] PR021 Does the representative F001/R-028-001/TVDEMO slice expose the evidence pattern before bulk closure? [Plan - Autonomous Execution Contract]
  - Durchfuehrungshinweis: Verify the first slice contains canonical relation, real path, negative boundary, consumer link, and review metadata.
- [x] PR022 Are no placeholders, hidden delivery assumptions, stale feature markers, or unbounded proof claims left? [Readiness]
  - Durchfuehrungshinweis: Search every Feature-028 artifact and current agent marker, then reconcile with delivery authority and gate states.

## Review Result

- [x] PR023 Every execution hint was applied. The combined secret gate was split into independently provable runs, consumer cardinality no longer hides new shared flows, complete review metadata is modeled, and all five agent surfaces are synchronized. [Readiness]
