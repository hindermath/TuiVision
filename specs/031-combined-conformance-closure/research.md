# Research: Gemeinsamer Konformitätsabschluss

## Decision 1: Independent composition instead of another product audit

**Decision**: Feature 031 reads the accepted structured artifacts from Features
024, 028, 029, and 030 and reconciles them with the final 025/026 evidence.

**Rationale**: The prior validators already prove each source dataset. The new
value is the cross-feature closure: exact hashes, cardinalities, reciprocal
relations, no suppressed finding, and causal Wave release.

**Alternatives considered**:

- Re-run the full domain audits from raw source: rejected because it would
  duplicate Features 024, 029, and 030 and expand the closure into a new audit.
- Trust only the human-readable summaries: rejected because aggregate prose
  cannot prove IDs, duplicates, hashes, or reciprocal relations.

## Decision 2: Bind accepted predecessor files by SHA-256

**Decision**: Every structured input used by 031 is listed by relative path and
SHA-256 in the new closure dataset. The validator recalculates the hashes.

**Rationale**: A merged predecessor artifact can drift later while retaining
the same filename. Hash binding makes the exact accepted input explicit and
forces a reviewed closure revision when it changes.

**Alternatives considered**:

- Bind only feature merge commits: rejected because a later commit on `main`
  can legitimately modify one predecessor file.
- Store only counts: rejected because duplicate replacement can preserve a
  total count.

## Decision 3: Keep full closure rows for reviewable identities

**Decision**: Store 48 contract rows, 13 consumer rows, 96 observation rows,
13 prior finding rows, and three empty owner rows in `closure-evidence.json`.

**Rationale**: Reviewers need row-level traceability, while tests need
deterministic IDs and reciprocal links. Arrays of IDs alone would not show why
one row remains acceptable.

**Alternatives considered**:

- Store only a summary and read every detail dynamically: rejected because the
  Feature-031 decision would have no durable row-level evidence.
- Copy complete predecessor objects verbatim: rejected because it creates
  unnecessary duplication and makes later comparisons harder.

## Decision 4: One test-only validator in the existing Drivers test project

**Decision**: Add `CombinedConformanceClosureEvidenceTests.cs` under
`tests/TuiVision.Drivers.Tests`.

**Rationale**: All audit and closure validators already live there and have
access to all five framework assemblies plus the repository-root helper. No
new project or package is needed.

**Alternatives considered**:

- Add a standalone script: rejected because it would require Bash/PowerShell
  parity, man page, help, and another execution surface.
- Add production validation code: rejected because closure logic is not a
  runtime feature.

## Decision 5: Compose existing validators instead of copying their internals

**Decision**: The new test validates 031 relationships and runs in the same
targeted test invocation as the existing 024/028/029/030 validator classes.

**Rationale**: Private helper duplication would increase maintenance and could
create inconsistent schemas. Running the predecessor validators preserves
their full proof while the new class focuses on cross-file closure.

**Alternatives considered**:

- Refactor all validators into a shared production library: rejected as a
  broad framework and test-infrastructure revision.
- Link source files across test assemblies: rejected because one existing test
  project already owns all validators.

## Decision 6: External provenance has local and CI boundaries

**Decision**: Local implementation re-fetches or reuses detached external
checkouts outside the repository and verifies Git objects plus file hashes.
CI deterministically verifies the accepted structured manifests, pins, hash
syntax, and source counts without requiring network cloning during tests.

**Rationale**: Network availability is not a stable unit-test dependency, but
the current run must still prove that the recorded upstream objects are real.

**Alternatives considered**:

- Clone upstream repositories inside every CI job: rejected as slow,
  availability-dependent, and outside the existing workflow contract.
- Skip current external verification: rejected by the binding intake.

## Decision 7: Feature-head Wave states stay blocked

**Decision**: The feature candidate records Wave 5 and Wave 6 as
`BlockedPendingCausalClosure`. Its explicit transition target is Wave 5
`Eligible` and Wave 6 `ConditionallyReady` only after exact-head gates and the
feature merge.

**Rationale**: A feature commit cannot truthfully contain its own future merge
fact. Updating the marker earlier would violate the Lastenheft.

**Alternatives considered**:

- Set `Eligible` in the feature PR: rejected as temporally false.
- Never persist the final Wave state: rejected because maintained status
  surfaces must converge.

## Decision 8: Use one non-recursive evidence closeout

**Decision**: After the feature merge, create one single-commit-capable
evidence-only closeout. Its repository file records feature PR, reviewed head,
checks, merge, Wave transition, task/state completion, and retrospective, but
does not require its own PR URL, head, or merge inside itself.

**Rationale**: This preserves truthful post-merge evidence without an infinite
series of self-invalidating commits.

**Alternatives considered**:

- Record remote facts on the feature head: rejected because a new commit
  changes the reviewed head.
- Keep all terminal facts outside Git: rejected because Wave state and run
  completion are durable project evidence.

## Decision 9: Ship dual-state marker validation before the merge

**Decision**: The Feature-031 validator and marker checks support two exact
states:

1. without a complete `delivery-closeout.md`, every current Wave marker remains
   blocked;
2. with a complete closeout that names the reviewed feature head, passing
   gates, feature merge, and causal transition, maintained final markers are
   exactly Wave 5 `Eligible` and Wave 6 `ConditionallyReady`.

**Rationale**: Closeout CI must not require a test-code change after the
feature merge. The executable rule is reviewed on the feature branch, while
the closeout supplies only the causal facts.

**Alternatives considered**:

- Update tests in the closeout: rejected because the closeout must be
  evidence-only.
- Leave predecessor Wave assertions unchanged: rejected because they would
  block the truthful final transition.
- Accept either state without causal evidence: rejected because that permits
  premature Wave release.

## Decision 10: Full validation is mandatory despite no runtime change

**Decision**: Run targeted validators, full Release tests, canonical coverage,
format, DocFX/A11Y, security, parity, platforms, and exact-head gates.

**Rationale**: The Lastenheft explicitly makes this an independent release
gate. A test-only closure can still expose stale tests, missing proof paths, or
platform workflow gaps.

**Alternatives considered**:

- Run only targeted tests: rejected because Wave eligibility is repository
  wide.
- Skip DocFX/A11Y because no XML changes are planned: rejected because
  learner-facing status and evidence are updated and the intake explicitly
  requires the gate.

## Decision 11: No preset promotion without a portable defect

**Decision**: Retrospective classification defaults to `NoPromotion` unless
the run reproduces a deterministic provider-neutral flaw in state, authority,
gate evidence, closeout, or convergence.

**Rationale**: Feature-specific data and provider quota behavior do not justify
general preset complexity.

**Alternatives considered**:

- Publish every workflow preference: rejected because it creates churn and
  overfits TuiVision.
- Skip retrospective: rejected by the autonomous-run contract.
