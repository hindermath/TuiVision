# Feature Specification: Pre-Wave-5 and Wave-6 Conformance Closure

**Feature Branch**: `028-pre-wave5-wave6-conformance-closure`
**Created**: 2026-07-14
**Status**: Draft
**Binding Input**: `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md`

## Clarifications

### Session 2026-07-14

- No formal clarification is required. The binding Lastenheft fixes finding
  cardinality, decision vocabularies, seven integration slices, validation,
  stop boundaries, and next-intake ordering. Existing tests may satisfy a
  slice only when their combined evidence covers every named production-path
  boundary; otherwise the feature adds the smallest test-only proof without a
  product correction. The consumer baseline is the thirteen Revision-2 row
  groups, not a sampled subset; additions are allowed only when the read-only
  review discovers another shared-framework responsibility.

## User Scenarios & Testing

### User Story 1 - Revalidate all accepted findings (Priority: P1)

As a framework maintainer, I need every accepted finding from the combined
TV203 and Free Vision review independently revalidated on the merged baseline
so that a documentation-only closure cannot hide a regression.

**Why this priority**: Every readiness statement depends on the thirteen
finding contracts still passing through their real production paths.

**Independent Test**: Review exactly `F001` through `F013`, reproduce each
original boundary, and confirm one complete closure row with a named real-path
proof and an allowed decision.

**Acceptance Scenarios**:

1. **Given** the merged Feature-025 and Feature-026 results, **When** all
   thirteen findings are revalidated, **Then** each finding has exactly one
   complete closure row and a passing real-path proof.
2. **Given** a missing, duplicated, weakened, or contradictory closure row,
   **When** closure integrity is checked, **Then** the gate fails visibly.
3. **Given** a finding that still reproduces or regresses, **When** the result
   is classified, **Then** it is reopened for its owner feature or routed to a
   product decision without a runtime repair in Feature 028.

---

### User Story 2 - Prove the integrated framework paths (Priority: P2)

As a future Wave-5 or Wave-6 implementer, I need consumer-shaped integration
proof across input, focus, lifecycle, desktop, drag, dialog, file, and resource
boundaries so that isolated unit success is not mistaken for usable framework
composition.

**Why this priority**: The later applications consume several corrected
contracts together through the actual application loop.

**Independent Test**: Execute the seven required integration slices and verify
their concrete state, lifecycle, accessibility, view-tree, and rendered proof
where those signals apply.

**Acceptance Scenarios**:

1. **Given** raw keyboard input, **When** it traverses the real adapter and
   dispatch path, **Then** key kind, modifiers, command mapping, fallback, and
   consumption match the accepted contract.
2. **Given** focus, idle, desktop, modal, drag, dialog, file, and resource
   scenarios, **When** their integrated flows execute, **Then** all required
   state transitions and rejection boundaries remain deterministic.
3. **Given** any slice that can pass only through a direct helper or mocked
   normalized event, **When** proof quality is reviewed, **Then** that slice
   remains incomplete.

---

### User Story 3 - Reassess both consumer families (Priority: P3)

As a project owner, I need a complete read-only readiness matrix for
`TVDEMOS/` and `TVFM/` so that the shared framework boundary is distinguished
from later application-specific work.

**Why this priority**: Wave 5 and Wave 6 must not start from stale or implicit
consumer assumptions.

**Independent Test**: Give every relevant consumer file or named flow group
exactly one allowed readiness decision with contract, proof, rationale, and
follow-up boundary.

**Acceptance Scenarios**:

1. **Given** a shared-framework consumer flow, **When** the current framework
   and proof are reviewed, **Then** it is classified as direct reuse, accepted
   deviation, bounded follow-up, small framework gap, or product decision.
2. **Given** a shared framework gap, **When** it is detected, **Then** the
   closure remains blocked and the gap is not implemented locally in an
   example or inside Feature 028.
3. **Given** Wave-6-only application work, **When** the matrix is finalized,
   **Then** it may remain a named follow-up only when it does not conceal a
   shared framework defect.

---

### User Story 4 - Record an unambiguous gate and next intake (Priority: P4)

As a maintainer, I need one auditable gate result across project status,
governance, and evidence surfaces so that no document releases an example wave
prematurely.

**Why this priority**: A technically green closure is still unsafe when the
ordering marker or maintained agent guidance states a different next step.

**Independent Test**: Verify that the existing TV203 and Free Vision gate is
either blocked with a concrete owner or marked `ReadyForTerminalGuiAudit`,
while both example waves remain `BlockedPendingTerminalGuiAudit` and Feature
029 is the sole next intake.

**Acceptance Scenarios**:

1. **Given** all finding, integration, consumer, and validation gates pass,
   **When** closure is finalized, **Then** the existing gate becomes
   `ReadyForTerminalGuiAudit` without releasing Wave 5 or Wave 6.
2. **Given** any required gate fails, **When** project status is updated,
   **Then** closure remains `Blocked` with owner, reproduction, and
   re-evaluation boundary.
3. **Given** a successful closure, **When** ordering surfaces are reviewed,
   **Then** `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md`
   is the next binding intake and Feature 029 has not been started in this run.

### Edge Cases

- A finding test passes but no longer reaches the production path named by the
  original observation.
- One finding is represented by several tests but has no single primary
  closure decision or reviewer.
- A proof helper normalizes input before the real console adapter boundary.
- Focus rejection preserves text but loses selection, cursor, focus signal, or
  the last valid value.
- Idle work runs while an event is pending, displaces input, or cannot reach a
  stable shutdown state.
- Close or modal proof records an internal result without visible owner removal,
  event isolation, focus restoration, view-tree change, or rendered evidence.
- Pointer and keyboard drag paths finish at different positions or leave
  capture active after cancellation or owner loss.
- File proofs touch arbitrary user data instead of source fixtures or
  test-owned temporary directories.
- Invalid persisted UI resources partially mutate visible state before
  rejection.
- A `TVDEMOS/` or `TVFM/` row is omitted because no immediate product change is
  expected.
- Generated output or an external checkout changes an inventory count.
- Push and pull-request events run equivalent jobs; only evidence bound to the
  reviewed pull-request head may satisfy the delivery gate.
- A platform-shaped green job does not execute the command required by the
  acceptance contract.
- Copilot or another reviewer is unavailable; absence is recorded as a missing
  review, not as approval.

## Requirements

### Functional Requirements

- **FR-001**: The feature MUST treat the binding Lastenheft, Feature-024
  Revision-2 observations, and final Feature-025 and Feature-026 evidence as
  immutable decision input.
- **FR-002**: The closure model MUST contain exactly thirteen unique rows,
  covering `F001` through `F013` with no omission, duplicate, or unrecognized
  finding identifier.
- **FR-003**: Every finding row MUST preserve the original observation,
  contract, owner feature, historical intent, Free Vision relation, and
  consumer scope rather than rewriting them to fit the current outcome.
- **FR-004**: Every finding MUST receive exactly one closure decision:
  `Closed`, `AlreadySatisfiedWithNewProof`, `Reopened025`, `Reopened026`, or
  `ProductDecision`.
- **FR-005**: `Closed` and `AlreadySatisfiedWithNewProof` MUST name a real-path
  proof that exercises the accepted production boundary and records residual
  risk; comments, implementation statements, aggregate suite success, or
  pre-normalized input alone are insufficient.
- **FR-006**: `Reopened025`, `Reopened026`, and `ProductDecision` MUST block the
  gate and identify owner, reproduction, required follow-up, and
  re-evaluation trigger without implementing the issue in Feature 028.
- **FR-007**: Audit data MUST maintain bidirectional and machine-checkable
  relations among findings, contracts, sources, proofs, consumers, owner
  features, and final decisions.
- **FR-008**: The feature MUST complete integration slice `R-028-001` from the
  real keyboard adapter through event classification, modifiers, function
  keys, Ctrl+W, Alt shortcut, unknown input, dispatch, consumption, and visible
  target state.
- **FR-009**: The feature MUST complete `R-028-002` with exactly one focused
  current child, state-specific hierarchy propagation, ordered focus transfer,
  validation veto, preserved rejected state, and focus announcement.
- **FR-010**: The feature MUST complete `R-028-003` with pending-event-first
  ordering, bounded idle execution only when no event waits, visible command
  refresh, no input displacement, CPU release, and deterministic shutdown.
- **FR-011**: The feature MUST complete `R-028-004` with desktop insertion,
  top/next, tile, cascade, safe close-all, close veto, Ctrl+W and applicable
  Escape, modal result, event isolation, cleanup, focus restoration, view-tree,
  and rendered buffer/cell proof.
- **FR-012**: The feature MUST complete `R-028-005` with one bounded generic
  drag flow covering capture, threshold, bounds, target decision, drop,
  cancellation, owner or capability loss, and an equivalent keyboard path.
- **FR-013**: The feature MUST complete `R-028-006` with completion-command
  classification, real child validation, first rejection focus, preserved
  input and dialog state, accessible rejection evidence, and the documented
  cancel boundary.
- **FR-014**: The feature MUST complete `R-028-007` with all accepted file
  selection modes in test-owned paths plus safe UI-resource reconstruction,
  exact keys, versions, unknown types, truncation, trailing data, invalid
  references or graphs, bounds, and atomic no-partial-state rejection.
- **FR-015**: Every required integration slice MUST identify its production
  entry point, concrete assertions, negative or fallback boundary, helper role,
  and proof limitation.
- **FR-016**: The consumer matrix MUST include all thirteen Revision-2 row
  groups: six Wave-5 groups (`TVDEMO.PAS`, `TVEDIT.PAS`, `TVRDEMO.PAS`,
  `GENRDEMO.PAS`, `GADGETS.PAS` and related demos, `MOUSEDLG.PAS`) and seven
  Wave-6 groups (`TVFM.PAS`, `FILEVIEW.PAS`, `DRAGDROP.PAS`, `TREEWIN.PAS`,
  `GLOBALS.PAS`, `COLORS.PAS`, and the combined `FILECOPY.PAS`/`TRASH.PAS`
  product-policy boundary). Every row and any newly identified relevant shared
  flow MUST receive exactly one decision: `UseExistingFramework`,
  `SmallFrameworkFix`, `IntentionalDeviation`, `FollowUpHardening`, or
  `ProductDecision`.
- **FR-017**: Consumer readiness rows MUST contain contract references,
  current proof, rationale, Wave relevance, residual risk, and follow-up
  boundary; no consumer source may be edited.
- **FR-018**: A shared-framework `SmallFrameworkFix` or `ProductDecision` MUST
  block closure. `FollowUpHardening` is allowed only for non-gate-critical work
  that does not conceal a shared framework gap.
- **FR-019**: The final existing-gate decision MUST be exactly
  `ReadyForTerminalGuiAudit` or `Blocked`; a passing Feature 028 MUST NOT use
  `Eligible` for either example wave.
- **FR-020**: Wave 5 and Wave 6 MUST remain
  `BlockedPendingTerminalGuiAudit` regardless of the Feature-028 outcome.
- **FR-021**: A successful closure MUST name
  `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` and Feature
  029 as the next binding intake without creating its feature branch or
  directory.
- **FR-022**: Product runtime, public APIs, dependencies, packages, examples,
  external sources, historical sources, and Wave-5 or Wave-6 behavior MUST
  remain unchanged.
- **FR-023**: Test or audit-validator changes are allowed only when they
  strengthen measurement of an already accepted contract and MUST NOT hide a
  required product correction.
- **FR-024**: Relevant `tv203s/`, `TVDEMOS/`, `TVFM/`, and pinned Free Vision
  sources MUST be reviewed as read-only evidence; no mechanical C++, Pascal,
  or external-source translation is permitted.
- **FR-025**: The feature MUST run and record targeted finding and integration
  proof, full Release tests, canonical per-assembly coverage, formatting,
  DocFX, accessibility, text-first, security, dependency, generated-output,
  protected-source, and relevant platform gates on the final candidate.
- **FR-026**: Windows or WSL proof MUST execute the actual acceptance command
  for keyboard ingress, path handling, and any terminal-facing fallback used by
  the seven slices; a repository-tooling or OS-shaped green job without that
  command is insufficient.
- **FR-027**: Core, Controls, Serialization, Compatibility, and Drivers.Console
  MUST each retain at least 70 percent line coverage.
- **FR-028**: Every closure, governance, and validation record MUST include its
  applicable owner, reviewer, date, evidence path, result, residual risk,
  follow-up, and re-evaluation trigger.
- **FR-029**: All governance checkpoints MUST use exactly `Applicable`, `N/A`,
  or `Open`; every `N/A` requires rationale and a re-evaluation trigger, while
  every `Open` requires an owner and concrete follow-up.
- **FR-030**: Maintained agent guidance MUST be evaluated as one synchronized
  set and changed only when shared guidance changes; `.specify/templates/`
  MUST remain unchanged unless a separate reusable template need is accepted.
- **FR-031**: Lastenhefte 10, 11, and 12 MUST be archived through the repository
  rename workflow only after every accepted closure requirement passes.
- **FR-032**: Pflichtenheft, processing order, Feature-024 gate, maintained
  agent context, project statistics, and feature evidence MUST report one
  consistent final decision and next intake.
- **FR-033**: Generated DocFX output, caches, logs, credentials, test results,
  temporary exact-head evidence, and external checkouts MUST remain untracked.

### Constitution Requirements

- **CR-001**: TuiVision is a registered Level-2 C#/.NET project and MUST use
  the Constitution, repository guidance, binding Lastenheft, and local preset
  matrix as project context.
- **CR-002**: Learner-facing and shared explanations MUST be German-first,
  English-second at CEFR-B2 level and usable through text-first assistive
  environments.
- **CR-003**: Documentation, navigation, XML, and A11Y evidence MUST follow the
  DocFX, Playwright/Axe, and text-oriented review path required by this closure.
- **CR-004**: Statistics and all maintained agent-guidance files MUST be
  reviewed for synchronized updates; repository templates remain `N/A` unless
  intentionally changed.
- **CR-005**: C#/.NET remains the only implementation language and is on the
  memory-safe-language allow-list; C++, Pascal, and external C# remain
  read-only evidence.
- **CR-006**: NIST SSDF and CWE Top 25 MUST apply to evidence integrity,
  malformed audit input, test validity, and fail-closed gate decisions.
- **CR-007**: OWASP ASVS MUST be `N/A` because the feature creates no web,
  HTTP, authentication, session, or service boundary; re-evaluate if that
  scope changes.
- **CR-008**: SBOM, VEX, SLSA, OpenSSF Scorecard, and new release-provenance
  work MUST be `N/A` because this feature creates no dependency, package, or
  distributable runtime change; re-evaluate if the diff crosses that boundary.
- **CR-009**: AI remains development tooling only. AI-SBOM MUST be `N/A` while
  no model, dataset, AI service, inference infrastructure, or runtime AI
  component is released or operated.
- **CR-010**: STRIDE, CIA, and CAPEC MUST apply to event, focus, lifecycle,
  validation, file, resource, and evidence-integrity boundaries. Zero Trust,
  cloud, provider, and distributed-service models remain trigger-based `N/A`.
- **CR-011**: Feature evidence MUST reference existing repository security and
  architecture evidence or provide an explicit equivalent feature-local
  rationale; it MUST NOT claim a control from an aggregate green check alone.
- **CR-012**: The six baseline governance presets and optional
  `autonomous-run-governance` v0.2.0 MUST be evaluated separately at their
  installed local versions.
- **CR-013**: Product scope, stop boundaries, and decision vocabularies MUST
  remain separate from operational remote authority, which belongs only in
  the plan and run evidence.

### Governance Applicability

- **Security Governance v0.6.0**: NIST SSDF, CWE Top 25, evidence integrity,
  fail-closed malformed-input handling, scope scans, secrets, dependency
  review, and exact-head proof are `Applicable`. ASVS, supply-chain additions,
  AI-SBOM, NIS2, CRA, EU AI Act, and DORA remain `N/A` with scope-change
  triggers because no service, distribution, product AI, or regulated
  operational boundary changes.
- **Architecture Governance v0.5.0**: STRIDE, CIA, CAPEC, quality-boundary
  review, and consumer-to-framework traceability are `Applicable`. S-ADR and
  arc42 are triggered only by a material architecture decision. Zero Trust,
  SAMM, BSI C3A, and BSI C5 remain `N/A` without cloud, provider, deployment,
  or distributed-service changes.
- **iSAQB Architecture Governance v0.2.0**: Quality scenarios, architecture
  consistency, historical intent, deliberate modernization, technical debt,
  and risk traceability are `Applicable`; broad restructuring is excluded.
- **A11Y Governance v0.4.0**: Keyboard ingress, focus transfer and veto,
  keyboard-equivalent drag, accessible rejection, modal focus restoration,
  text-first evidence, bilingual documentation, and didactic-comment review
  for any changed non-trivial test logic are `Applicable`.
- **Cross-Platform Governance v0.2.0**: macOS, Linux, and relevant Windows or
  WSL acceptance proof plus path and terminal boundaries are `Applicable`.
  New script parity work remains `N/A` unless a script is actually changed.
- **Agent Parity Governance v0.3.0**: All five maintained agent guidance
  surfaces are reviewed together. They change only for shared status or
  guidance; `.specify/templates/` remain `N/A` unless deliberately affected.
- **Autonomous Run Governance v0.2.0**: Evidence-first execution, convergence,
  complete gate requirements, exact staged-candidate validation, exact-head
  remote proof, no-empty-PR behavior, permission boundaries, validated
  feature-local run state, protected stop/resume, closeout, and retrospective
  learning are `Applicable`.

### Key Entities

- **Finding Closure Row**: One `F001`-`F013` record with immutable observation,
  owner feature, accepted contract, red proof, merged change, real-path proof,
  source relations, impact, decision, evidence, and residual boundary.
- **Integration Slice**: One `R-028-001`-`R-028-007` end-to-end proof with
  production entry point, preconditions, event or data path, concrete state,
  negative boundary, visible or rendered result, helper role, and proof limit.
- **Consumer Readiness Row**: One read-only `TVDEMOS/` or `TVFM/` file or flow
  group with contract mapping, current proof, Wave relevance, primary decision,
  rationale, risk, and follow-up boundary.
- **Gate Decision**: Existing TV203/Free Vision closure result, Wave-5 and
  Wave-6 blocked states, evidence completeness, owner, and next intake.
- **Governance Decision**: One preset checkpoint classified as `Applicable`,
  `N/A`, or `Open` with complete review and re-evaluation fields.
- **Validation Evidence**: Exact command, scope, candidate head, runner or
  platform where applicable, result, count or metric, failure boundary, and
  evidence source.

## Success Criteria

### Measurable Outcomes

- **SC-001**: Exactly 13 unique finding rows cover `F001` through `F013`; 100
  percent have one allowed closure decision and complete required evidence.
- **SC-002**: All seven integration slices pass through their named real paths
  with concrete positive, negative or fallback, lifecycle, and proof-boundary
  evidence.
- **SC-003**: All 13 Revision-2 consumer groups and 100 percent of any newly
  identified in-scope shared flow receive one allowed readiness decision and
  complete contract traceability.
- **SC-004**: The final diff contains zero product runtime, public API,
  dependency, package, example, external-source, historical-source, or
  Wave-5/Wave-6 behavior changes.
- **SC-005**: Targeted and full Release validation pass, and every mandatory
  assembly retains at least 70 percent line coverage.
- **SC-006**: Formatting, DocFX, Playwright/Axe, UTF-8 text review, secret,
  dependency, generated-output, and protected-source checks all pass on the
  final candidate.
- **SC-007**: Required macOS, Linux, and relevant Windows or WSL evidence maps
  each acceptance gate to the actual executed command and platform; zero gate
  claims rely only on job names.
- **SC-008**: 100 percent of governance rows contain applicability, rationale,
  evidence, owner, reviewer, review date, result, residual risk, follow-up, and
  re-evaluation trigger.
- **SC-009**: The existing gate is exactly `ReadyForTerminalGuiAudit` or
  `Blocked`; Wave 5 and Wave 6 remain
  `BlockedPendingTerminalGuiAudit` in every maintained status surface.
- **SC-010**: A successful run archives exactly Lastenhefte 10, 11, and 12,
  names Feature 029 as the sole next intake, and does not create Feature 029.
- **SC-011**: No `[NEEDS CLARIFICATION]`, TODO, TBD, placeholder, incomplete
  checklist item, unowned blocker, or contradictory readiness marker remains.
- **SC-012**: The final delivery candidate has zero unintended untracked or
  unstaged files and zero actionable review threads before merge.

## Assumptions

- Features 025 and 026 are fully merged, and the thirteen accepted findings
  retain their Revision-2 identities and ownership.
- Feature 027 remains valid historical evidence but its former future-release
  decision is superseded by Revision 2 and the new Terminal.GUI gate sequence.
- Existing product behavior is expected to satisfy the closure. A reproduced
  product defect stops this feature instead of being repaired here.
- Test-only integration or audit-validator changes may be unnecessary when
  existing real-path proof already satisfies an acceptance boundary.
- The pinned Free Vision evidence remains available at the accepted commit;
  substitution with an unpinned revision is not allowed.
- Operational delivery for this run is authorized as `MergeAndSync`, but that
  authority is recorded in the plan and run evidence rather than changing the
  product requirements.

## Scope Boundaries

### In Scope

- Independent closure evidence for `F001` through `F013`
- Seven required real-path integration slices
- Read-only Wave-5 and Wave-6 consumer-readiness review
- Test-only or audit-integrity corrections that do not change product behavior
- Full local, platform, documentation, accessibility, security, and remote gate
  evidence
- Final gate, archive, ordering, agent-parity, statistics, and next-intake
  synchronization

### Out of Scope

- Runtime, public API, package, dependency, or product-behavior changes
- Fixing a reopened finding inside Feature 028
- Wave-5 or Wave-6 example implementation or visual remediation
- Terminal.GUI analysis or Feature-029 findings
- Edits to `tv203s/`, `TVDEMOS/`, `TVFM/`, Free Vision, or other external source
- Broad framework redesign, destructive product policy, or weakened acceptance
- Generated documentation, caches, logs, test output, or exact-head snapshots
  committed to Git

### Decision and Follow-up Model

- Finding closure decisions are exactly `Closed`,
  `AlreadySatisfiedWithNewProof`, `Reopened025`, `Reopened026`, or
  `ProductDecision`.
- Consumer readiness decisions are exactly `UseExistingFramework`,
  `SmallFrameworkFix`, `IntentionalDeviation`, `FollowUpHardening`, or
  `ProductDecision`.
- Existing-gate decisions are exactly `ReadyForTerminalGuiAudit` or `Blocked`.
- Wave states remain `BlockedPendingTerminalGuiAudit` in this feature.
- Governance decisions are exactly `Applicable`, `N/A`, or `Open` and never
  replace finding, consumer, or gate decisions.

## Dependencies and Ordering

- Feature 028 starts only after merged Features 025 and 026 and uses their
  complete evidence as immutable input.
- Feature 029 is the mandatory next intake after Feature 028, regardless of
  whether 028 passes or reopens an owner boundary.
- Findings from Feature 029 may create only non-empty hardening features,
  followed by one independent closure.
- Wave 5 remains blocked until that later closure passes. Wave 6 additionally
  requires the actual post-Wave-5 delta review.
