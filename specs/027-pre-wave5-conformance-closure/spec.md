# Feature Specification: Pre-Wave-5 Conformance Closure

**Feature Branch**: `027-pre-wave5-conformance-closure`  
**Created**: 2026-07-12  
**Status**: Draft  
**Binding Input**: `Lastenheft_09_Pre-Wave5-Conformance-Closure.md`

## Clarifications

### Session 2026-07-12

- No formal clarification is required. The merged Feature-024 gate fixes all
  cardinalities, stop conditions, owner sets, validation gates, and the rule
  that any new drift requires a reviewed audit revision rather than runtime
  work inside Feature 027.

## User Scenarios & Testing

### User Story 1 - Revalidate the merged audit baseline (Priority: P1)

As a maintainer, I need the complete Feature-024 contract model revalidated
against the current merged repository so that no inventory, decision, source,
or proof drift can be hidden before Wave 5.

**Why this priority**: Every later closure decision depends on the audit still
describing the repository exactly.

**Independent Test**: Run the conformance evidence validator and confirm the
fixed 16-domain, 48-contract, 151/119/176 inventory, 15-source, 94-proof, and
13/34/1/0/0 decision cardinalities with zero findings.

**Acceptance Scenarios**:

1. **Given** the merged Feature-024 dataset, **When** live inventories and
   contract references are re-evaluated, **Then** all exact counts and unique
   identities match.
2. **Given** malformed, unknown, duplicated, missing, or hash-drifted evidence,
   **When** the validator runs, **Then** it rejects the closure input visibly.
3. **Given** any new drift or evidence gap, **When** closure is evaluated,
   **Then** Feature 027 stops and requests a reviewed audit revision without
   changing runtime behavior.

---

### User Story 2 - Prove complete integration and release gates (Priority: P2)

As a release maintainer, I need the complete repository validation path on the
closure head so that an audit-only success is not mistaken for integrated
release readiness.

**Why this priority**: Wave 5 may only rely on a baseline that passes all
framework, coverage, documentation, accessibility, security, and scope gates.

**Independent Test**: Execute the focused audit suite, full Release tests,
canonical per-assembly coverage, format, DocFX, Axe, Lynx, secret, generated-
output, dependency, API, runtime, example, and historical-source checks.

**Acceptance Scenarios**:

1. **Given** the unchanged product baseline, **When** all local gates run,
   **Then** every required gate passes and every proof boundary is recorded.
2. **Given** a failed required gate, **When** the closure decision is made,
   **Then** Wave 5 remains blocked with owner and reproduction evidence.
3. **Given** conditional governance checks, **When** no trigger entered scope,
   **Then** `N/A` includes rationale and a re-evaluation trigger.

---

### User Story 3 - Make the formal Wave-5 decision (Priority: P3)

As a project owner, I need one explicit pre-Wave-5 gate result so that the
ordering, Pflichtenheft, agent guidance, and later intake cannot disagree.

**Why this priority**: A technically green run is insufficient if project
governance still reports Wave 5 as blocked or creates empty remediation work.

**Independent Test**: Verify that all accepted finding owner sets remain empty,
Features 025 and 026 do not exist, Feature 027 is marked complete, and every
maintained status surface names Wave 5 as the next eligible intake only after
the closure gates pass.

**Acceptance Scenarios**:

1. **Given** zero accepted findings and all gates passing, **When** closure is
   finalized, **Then** Wave 5 is formally released as the next intake.
2. **Given** empty `Core025` and `ComponentData026` sets, **When** downstream
   work is evaluated, **Then** no branch, feature directory, task list, or PR
   is created for 025 or 026.
3. **Given** an unresolved blocker, **When** status surfaces are updated,
   **Then** Wave 5 remains visibly blocked and no completion claim is made.

---

### User Story 4 - Deliver and learn under explicit authority (Priority: P4)

As a maintainer, I need remote checks, reviews, merge, synchronization, and
portable learning completed under explicit authority so that closure evidence
is trustworthy and reusable without widening permissions.

**Why this priority**: Remote facts and reusable learning complete the delegated
`MergeAndSync` contract but must not weaken technical or human controls.

**Independent Test**: Required PR-context checks pass, actionable GraphQL
threads are zero, unavailable reviewers are recorded honestly, any bypass is
limited to the sole Human Approval rule, local `main` equals `origin/main`, and
the retrospective either creates a non-empty portable handoff or records no
promotion.

**Acceptance Scenarios**:

1. **Given** green technical checks and zero actionable threads, **When** only
   Human Approval blocks merge, **Then** the authorized narrow bypass may be
   used and is recorded.
2. **Given** a reviewer quota failure, **When** review state is summarized,
   **Then** it is recorded as missing review, never approval.
3. **Given** no new portable preset gap, **When** retrospective runs, **Then**
   no empty preset PR or upstream update is created.

### Edge Cases

- A source file count changes only because generated output appears.
- A public type moves between files while the contract remains identical.
- A proof method is renamed but an obsolete string remains in the dataset.
- The external Free Vision worktree is unavailable while recorded hashes stay
  unchanged; local provenance can pass only within the documented proof limit.
- A new Low or Medium finding appears after 024; closure still stops because
  the accepted owner sets are no longer empty.
- Push and pull-request events start equivalent checks; PR-context checks remain
  authoritative and duplicate runs are not cancelled without a safe contract.
- Documentation passes visually but loses semantic text in Lynx.
- The Home-Baseline PowerShell scanner exits zero while writing an ErrorRecord;
  this is a failed helper result.

## Requirements

### Functional Requirements

- **FR-001**: The feature MUST treat the merged Feature-024 artifacts and the
  binding Lastenheft as immutable decision input unless a reviewed audit
  revision is explicitly created.
- **FR-002**: The feature MUST verify exactly 16 domains, 48 contracts, 151
  historical items, 119 maintained production files, 176 exported public
  types, 15 external source records, and 94 concrete proof references.
- **FR-003**: The feature MUST verify primary decision counts of 13 `Aligned`,
  34 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 0 `BehavioralDrift`,
  and 0 `EvidenceGap`.
- **FR-004**: The feature MUST verify exactly zero findings and empty
  `Core025`, `ComponentData026`, `AcceptedFollowUp`, and `ProductDecision`
  owner sets.
- **FR-005**: Every reviewed closure boundary MUST receive exactly one result:
  `Pass`, `Fail`, `N/A`, or `Open`; `N/A` and `Open` MUST carry the required
  rationale, owner/follow-up, and re-evaluation trigger.
- **FR-006**: Any new drift, evidence gap, finding, or non-empty remediation
  owner set MUST block closure and MUST NOT be repaired as runtime work in 027.
- **FR-007**: The feature MUST execute the focused Feature-024 conformance
  evidence suite on the closure head.
- **FR-008**: The feature MUST execute the full Release test suite and record
  project-level pass, failure, and skip counts.
- **FR-009**: The feature MUST execute the canonical coverage gate and record
  assembly-specific line coverage of at least 70 percent for Core, Controls,
  Serialization, Compatibility, and Drivers.Console.
- **FR-010**: The feature MUST execute diff, format, secret, generated-output,
  dependency, API, runtime, example, and historical-source scope checks.
- **FR-011**: The feature MUST execute DocFX, Playwright/Axe, and UTF-8 Lynx
  because the formal project-status documentation changes.
- **FR-012**: The feature MUST preserve `tv203s/`, `TVDEMOS/`, `TVFM/`, and the
  external Free Vision checkout as read-only evidence.
- **FR-013**: The feature MUST NOT change product runtime behavior, public API
  signatures, packages, dependencies, example behavior, or historical source.
- **FR-014**: The feature MUST NOT create Feature 025 or 026 artifacts while
  their accepted finding sets are empty.
- **FR-015**: The feature MUST create `closure-evidence.md` before any closure
  implementation edit and keep command, result, proof, governance, scope,
  review, and resume state current.
- **FR-016**: The feature MUST record all seven installed governance layers
  with exact local versions and trigger-based applicability.
- **FR-017**: The feature MUST revalidate the corrected Home-Baseline
  PowerShell homogeneity path using explicit repository root, exit status,
  parseable required output, and a clean error channel.
- **FR-018**: Shared evidence, version, status, statistics, and agent-guidance
  files MUST be edited serially.
- **FR-019**: Before every explicit `dotnet build` or `dotnet test`, the manual
  build counter MUST be incremented exactly once; branch version fields MUST
  follow `1.27.<patch>.<build>` before commit or push.
- **FR-020**: After all local gates pass, Pflichtenheft and ordering MUST mark
  Feature 027 complete and Wave 5 as the next eligible intake.
- **FR-021**: All five maintained agent surfaces MUST carry the same completed
  Feature-027 context or an explicit documented agent-specific divergence.
- **FR-022**: The Lastenheft MUST be archived with the numbered branch suffix
  only after accepted closure requirements are satisfied.
- **FR-023**: Remote delivery MUST use the explicit `MergeAndSync` authority,
  required PR-context checks, thread-level review, and the narrow bypass only
  within its accepted boundary.
- **FR-024**: Post-merge facts MUST use one causal evidence-only closeout when
  writing them earlier would be false or self-invalidating.
- **FR-025**: The retrospective MUST classify each observation as project-
  specific, local correction, preset follow-up, or no promotion and MUST NOT
  create an empty PR or upstream update.
- **FR-026**: German-first/English-second explanatory text MUST target CEFR-B2
  and remain usable in text-first assistive environments.

### Governance Applicability

- **Security Governance 0.6.0**: NIST SSDF and evidence integrity are
  applicable. ASVS, SBOM/VEX/SLSA/OpenSSF/AI-SBOM and NIS2/CRA/EU-AI-Act/DORA
  remain trigger-based `N/A` unless product, package, release, AI, web/auth, or
  regulated-service scope changes.
- **Architecture Governance 0.5.0**: contract boundaries are reviewed;
  STRIDE/CIA/CAPEC, S-ADR, arc42, Zero Trust, SAMM, BSI C3A, and BSI C5 remain
  `N/A` while runtime boundaries, cloud, topology, and provider dependencies do
  not change.
- **iSAQB Architecture Governance 0.2.0**: quality scenarios, risks, and the
  formal gate are applicable.
- **A11Y Governance 0.4.0**: bilingual text-first closure evidence and the
  DocFX/Axe/Lynx path are applicable.
- **Cross-Platform Governance 0.2.0**: no repository script changes are
  planned; external PowerShell/Bash helper revalidation is applicable evidence,
  while new script governance remains `N/A` unless scope changes.
- **Agent Parity Governance 0.3.0**: all five maintained agent surfaces are
  applicable; `.specify/templates/` remain `N/A` unless a reusable local rule
  actually changes.
- **Autonomous Run Governance 0.1.0**: authority, convergence, no-empty-work,
  review, closeout, retrospective, and synchronization are applicable.

## Key Entities

- **ClosureRun**: identifies baseline SHA, closure SHA, authority, gate state,
  and final Wave-5 decision.
- **RevalidationCheck**: one named boundary with result, command/proof, owner,
  residual risk, and re-evaluation trigger.
- **BaselineSnapshot**: the fixed 024 counts, hashes, decisions, findings, and
  proof references compared with live repository state.
- **Wave5GateDecision**: `Blocked` or `Released`, rationale, evidence path, and
  next intake.
- **PortableObservation**: reusable workflow finding with project exclusions,
  confidence, reproduction, decision, and handoff boundary.

## Assumptions

- Feature 024, its closeout, and its retrospective are merged on `main` before
  Feature 027 starts.
- No product or public API change occurred after the audited 024 baseline.
- The 024 evidence validator remains the canonical machine-checkable audit
  contract and may be extended only for closure-specific test evidence.
- The official Free Vision pin and recorded hashes remain the accepted
  secondary-source identity; Borland/`tv203s/` remain primary.
- Wave 5 will be prepared as Feature 028 after 027; it is not part of this run.

## Success Criteria

- **SC-001**: 100 percent of the 16 domains, 48 contracts, 151/119/176
  inventories, 15 source records, and 94 proof references pass exact automated
  revalidation.
- **SC-002**: Decision counts remain 13/34/1/0/0 and the finding count remains
  exactly zero.
- **SC-003**: Features 025 and 026 have no branch, directory, task list, or PR.
- **SC-004**: Focused audit tests, full Release tests, five coverage gates,
  format, DocFX, Axe, Lynx, secret, scope, and required remote checks all pass.
- **SC-005**: The final diff contains zero product-runtime, public-API,
  dependency, package, example-behavior, generated-output, or historical-source
  changes.
- **SC-006**: Every governance checkpoint has one final result and complete
  ownership/evidence fields; no unowned `Open` row remains at local closure.
- **SC-007**: All maintained status and agent surfaces agree that Feature 027
  is complete and Wave 5 is the next eligible intake.
- **SC-008**: Local `main` is clean and equals `origin/main` after authorized
  merge, branch cleanup, retrospective, and required handoff.
- **SC-009**: No clarification marker, placeholder, TODO, or TBD remains in
  accepted Feature-027 artifacts.
