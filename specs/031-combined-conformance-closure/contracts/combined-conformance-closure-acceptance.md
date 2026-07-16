# Acceptance Contract: Gemeinsamer Konformitätsabschluss

## Input Contract

1. Features 024, 025, 026, 028, 029, and 030 are merged.
2. Every structured input path and SHA-256 is declared before row-level
   closure is accepted.
3. Historical, consumer, and external source trees remain read-only.
4. A mismatched or unavailable binding pin blocks closure.

## Source Contract

- Free Vision uses commit
  `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` and 15 accepted source hashes.
- Terminal.GUI uses `v1.9.0`, tag object
  `4b812e44798f2c7567afec50ba9a9293b6beb6de`, commit
  `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`, license SHA-256
  `2a7331c273b7c121f5e1f6f10e13d279a739ac310c49b56f2fb251d0490988d0`,
  and 25 accepted source hashes.
- magiblot/tvision uses commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree
  `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, COPYRIGHT SHA-256
  `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548`,
  and 50 accepted source hashes.
- No external source text, fixture, build output, checkout, or vendored copy is
  tracked.

## Cardinality Contract

1. Exactly 48 contract rows cover `C001` through `C048`.
2. Exactly 13 consumer rows cover `W5-001` through `W5-006` and `W6-001`
   through `W6-007`.
3. Exactly 48 TGO and 48 MB observation rows exist.
4. Exactly 96 combined dispositions exist with one row per observation.
5. Exactly 13 prior finding rows cover `F001` through `F013`.
6. Exactly three known owner rows exist and all have empty finding sets.
7. Canonical findings, product decisions, dependency edges, and hardening
   intakes are empty.
8. Missing, duplicate, unknown, orphaned, contradictory, or out-of-order closed
   sets fail closed.

## Relationship Contract

- Every contract reconciles accepted Feature-024, Feature-029, and Feature-030
  relations, paired TGO/MB observations, final disposition, proof, and
  consumers.
- Every consumer reconciles Features 028, 029, and 030.
- Every observation reconciles its source audit and the Feature-030 combined
  disposition.
- Every prior finding reconciles Feature-024 finding/resolution and
  Feature-028 closure proof.
- Every source, contract, consumer, observation, finding, and owner reference
  is reciprocal where the source schema provides the reverse relation.

## No-Suppression Contract

- No empty owner row creates an intake.
- A non-empty owner row requires a real canonical finding and matching
  dependency-ordered hardening intake.
- Because the accepted finding set is empty, any non-empty owner row,
  hardening intake, product decision, or dependency edge blocks closure.
- A reproduced product defect is reported outside Feature 031 and is not fixed
  by the closure implementation.

## Validation Contract

- Targeted closure and predecessor audit validators pass.
- Full Release tests pass.
- Core, Controls, Serialization, Compatibility, and Drivers.Console each
  retain at least 70 percent line coverage.
- Format, DocFX, Playwright/Axe, UTF-8 text review, secret, scope,
  generated-output, supply-chain, and agent-parity checks pass.
- Ubuntu, macOS, and Windows run the actual repository Release body on the
  reviewed pull-request head.
- Exact-head evidence maps every applicable gate to command, workflow, job,
  platform, head, and result.
- Missing reviews remain missing; no actionable thread remains before merge.

## Wave Contract

1. The reviewed feature head keeps Wave 5 and Wave 6 blocked.
2. Full feature-head success yields `ReadyForMerge`, not Wave eligibility.
3. After the reviewed feature merge, one causal evidence-only closeout sets
   Wave 5 to `Eligible`.
4. The same closeout sets Wave 6 no further than `ConditionallyReady`.
5. Wave 6 remains blocked until Wave 5 completes and its real delta review
   passes.
6. Feature 031 never starts Wave 5, Wave 6, or Feature 032.
7. Marker-consuming tests are delivered on the feature branch and accept the
   final states only when `delivery-closeout.md` proves the exact reviewed
   feature head, passing gates, feature merge, and transition.
8. The closeout changes no executable or test logic.

## Delivery Contract

- Delivery mode is `MergeAndSync` under the current user authority.
- The narrow bypass may address only Human Approval after all technical gates
  pass and actionable review threads are zero.
- The feature PR and any causal closeout are non-empty and independently
  reviewable.
- The closeout does not require its own PR URL, reviewed head, or merge commit
  inside the same repository file.
- The run ends with completed tasks, `Retrospective`, `Completed`,
  `nextExactAction: N/A`, deleted obsolete branches, and clean synchronized
  `main`.
