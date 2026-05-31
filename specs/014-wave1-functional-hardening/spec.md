# Feature Specification: Wave 1 Functional Hardening

**Feature Branch**: `014-wave1-functional-hardening`
**Created**: 2026-05-31
**Status**: Draft
**Input**: User description: "Use `Lastenheft_Wave1-Functional-Hardening.md` as the binding input. Create the specification for `014-wave1-functional-hardening`. Harden the delivered Wave-1 examples against their historical sources, document ported, replaced, and intentionally omitted core behavior, strengthen smoke proof where startup, string, or headless-helper paths prove too much, and keep Wave-1 visual remediation and later waves out of scope."

## Clarifications

### Session 2026-05-31

- Q: Which proof rule applies when a historical Wave-1 core point is didactic or has no directly executable managed runtime behavior? → A: Executable smoke proof is required whenever managed runtime behavior exists; evidence-only proof is allowed only for explicitly documented no-runtime-target deviations.
- Q: Where is the binding historical proof matrix for Wave 1 maintained? → A: The feature MUST maintain `specs/014-wave1-functional-hardening/pr-evidence.md` as the primary proof matrix; guides and README updates may summarize or link to it but do not replace it.
- Q: When may a helper or headless path count as `PrimaryProof`? → A: `PrimaryProof` is allowed only when the path executes real example or application logic through public commands, events, application methods, or stable public state and contains concrete assertions; paths that merely prepare state, inspect private details, or bypass behavior must be classified as `SetupOnly`, `SupplementalProof`, or `LegacyOrTemporary`.
- Q: How must relevant negative or fallback paths be proven? → A: Each relevant negative or fallback path MUST either be reproducible by smoke proof or, when the environment cannot trigger it deterministically, be documented in `specs/014-wave1-functional-hardening/pr-evidence.md` with trigger, expected deviation, observed fallback, and proof boundary.
- Q: What happens when historical review finds a missing core function in the current managed example? → A: Missing historical core function MUST be implemented and smoke-proven when necessary for the existing Wave-1 functional purpose and feasible without broad framework work, visual remediation, or new dependencies; otherwise it MUST be recorded in `specs/014-wave1-functional-hardening/pr-evidence.md` as an intentional deviation or follow-up.
- Q: When must learner-facing guides or `examples/README.md` be updated instead of keeping details only in `pr-evidence.md`? → A: Guides or `examples/README.md` MUST be updated when runtime behavior, usage path, visible output, historical deviation, or learner-facing proof explanation changes; review-only classification details MAY remain only in `specs/014-wave1-functional-hardening/pr-evidence.md`.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Historical proof matrix for Wave 1 (Priority: P1)

As a maintainer, I want each Wave-1 example area to have a clear historical
proof matrix, so that later remediation work can rely on documented intent
instead of re-reading the original sources from scratch.

**Why this priority**: This is the feature's foundation. Without the matrix,
reviewers cannot tell whether current behavior is historically grounded,
modernized on purpose, or accidentally thin.

**Independent Test**: A reviewer can inspect
`specs/014-wave1-functional-hardening/pr-evidence.md` and find one complete
row or section for `Desklogo`, `MsgCls`, `Videomode`, and each of the 16
`Tutorial` steps.

**Acceptance Scenarios**:

1. **Given** a reviewer inspects
   `specs/014-wave1-functional-hardening/pr-evidence.md`, **When** they choose
   any Wave-1 example area, **Then** they can identify the historical source,
   historical core function, current managed behavior, proof method, helper
   classification, and intentional deviations.
2. **Given** a reviewer inspects `Tutorial`, **When** they check the 16
   historical steps, **Then** each step remains individually traceable instead
   of being collapsed into one generic tutorial statement.
3. **Given** a historical helper or asset generator is reviewed, **When** it is
   not required for the current managed example, **Then** the evidence explains
   whether it is omitted, replaced, or used only as context.

---

### User Story 2 - Hardened functional smoke proof (Priority: P1)

As a reviewer, I want the Wave-1 smoke proof to verify meaningful example
behavior, so that the examples are not accepted only because they start or
display static text.

**Why this priority**: The existing Wave-1 delivery is accepted, but this
quality pass must close proof gaps before visible Wave-1 remediation builds on
top of it.

**Independent Test**: Each Wave-1 example area has at least one smoke scenario
or evidence-backed proof that checks a historical core function rather than
only launch success.

**Acceptance Scenarios**:

1. **Given** `Desklogo` is reviewed, **When** its proof is inspected, **Then**
   it verifies logo or desktop intent, asset-source rationale, or a documented
   fallback rather than only startup.
2. **Given** `MsgCls` is reviewed, **When** its proof is inspected, **Then** it
   verifies custom message triggering, routing, and observable result,
   including repeated-trigger stability.
3. **Given** `Tutorial` is reviewed, **When** its proof is inspected, **Then**
   all 16 step tokens remain individually discoverable and each step has a
   step-specific learning or behavior proof.
4. **Given** `Videomode` is reviewed, **When** its proof is inspected, **Then**
   it verifies a real capability outcome or an explicit fallback with
   post-transition usability.

---

### User Story 3 - Helper and headless path classification (Priority: P2)

As the later visual-remediation implementer, I want every helper and headless
proof path used by Wave-1 smokes to be classified, so that I know which proof
can stay and which proof must later move behind visible runtime paths.

**Why this priority**: The feature intentionally does not deliver interactive
visual remediation, but it must prepare that follow-up by making current proof
surfaces explicit.

**Independent Test**: A reviewer can list all Wave-1 smoke helper and proof
paths and see one classification for each: `SetupOnly`, `PrimaryProof`,
`SupplementalProof`, or `LegacyOrTemporary`.

**Acceptance Scenarios**:

1. **Given** a helper prepares controlled state, **When** it is reviewed,
   **Then** it is marked `SetupOnly` and not counted as proof of historical
   behavior.
2. **Given** a helper currently verifies the main behavior, **When** it is
   reviewed, **Then** it is marked `PrimaryProof` only if it exercises real
   example or application logic through public commands, events, application
   methods, or stable public state with concrete assertions, or
   `LegacyOrTemporary` if later visual remediation must replace it.
3. **Given** a helper adds extra assertions around a real smoke path, **When**
   it is reviewed, **Then** it is marked `SupplementalProof`.

---

### User Story 4 - Learner-facing traceability (Priority: P2)

As an apprentice or text-first reviewer, I want the Wave-1 guides and evidence
to explain historical intent and modern deviations in German first and English
second, so that I can understand the port without confusing it with the
original C/C++ examples.

**Why this priority**: This feature changes the trust level of the already
delivered examples. The proof must be readable by learners and accessible
reviewers, not only by maintainers.

**Independent Test**: A learner can open the relevant guide, README summary, or
the primary feature evidence in
`specs/014-wave1-functional-hardening/pr-evidence.md` and find
German-first/English-second traceability, deviation, and proof notes for each
Wave-1 area.

**Acceptance Scenarios**:

1. **Given** a guide discusses a historical deviation, **When** a learner reads
   it, **Then** the German explanation appears first and the English
   explanation follows at CEFR-B2 readability.
2. **Given** a proof path is helper-based or headless, **When** a learner or
   reviewer reads the evidence, **Then** the text explains why that proof is
   acceptable for functional hardening or why it is deferred to visual
   remediation.
3. **Given** runtime behavior, usage path, visible output, historical
   deviation, or learner-facing proof explanation changes, **When** the
   feature is documented, **Then** the affected guide or `examples/README.md`
   is updated in German first and English second.
4. **Given** the current project next-step marker still points to Wave 3,
   **When** this feature is documented, **Then** the documentation states that
   this is a Wave-1 quality follow-up and does not complete or replace Wave 3.

### Edge Cases

- A historical source uses platform-specific support files, asset generators,
  or build scripts that are not part of the managed example runtime.
- A `Tutorial` step is mostly didactic in the current port and has no direct
  one-to-one runtime behavior.
- A current smoke proves useful behavior only through a helper or headless
  inspection path.
- `Videomode` cannot perform a real terminal mode change in the current
  environment and must present an honest fallback.
- `Desklogo` cannot or should not reproduce the historical asset-generation
  path exactly.
- `MsgCls` uses a modern event-routing shape that differs from the historical
  source layout.
- A future reviewer tries to treat this feature as the Wave-1 visual
  remediation or as permission to start Wave-3 implementation.
- A relevant negative or fallback path cannot be triggered deterministically in
  local or CI validation, but still affects whether the historical behavior is
  accepted, replaced, or intentionally omitted.
- Historical review finds a missing core function in the current managed
  example, but implementing it would require broad framework work, visual
  remediation, a new dependency, or behavior outside this feature's Wave-1
  functional scope.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST cover only the delivered Wave-1 examples:
  `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and
  `Videomode`.
- **FR-002**: The feature MUST review the relevant historical sources as
  read-only material before accepting any hardened proof claim.
- **FR-003**: The historical review MUST include `desklogo/desklogo.cc`, with
  `set-logo.cc` and `tv_logo.cc` used only for asset and generator
  boundary decisions.
- **FR-004**: The historical review MUST include `msgcls/testdyn.cpp`,
  `msgcls/tlnmsg.cpp`, and `msgcls/tlnmsg.h`.
- **FR-005**: The historical review MUST include `tutorial/tvguid01.cc`
  through `tutorial/tvguid16.cc`.
- **FR-006**: The historical review MUST include `videomode/test.cc`.
- **FR-007**: The feature MUST create and maintain
  `specs/014-wave1-functional-hardening/pr-evidence.md` as the primary
  evidence surface that records, for every covered example area, the
  historical source, historical core function, current managed behavior, proof
  method, helper classification, and intentional deviation or omission.
- **FR-008**: The feature MUST keep all 16 `Tutorial` steps individually
  traceable, selectable, and reviewable.
- **FR-009**: The feature MUST require at least one meaningful functional
  smoke proof per Wave-1 example area.
- **FR-010**: Smoke proof MUST verify behavior that is relevant to the
  historical example intent, not only launch success, static text presence, or
  project existence.
- **FR-011**: `Desklogo` proof MUST address logo or desktop intent, asset
  source or replacement rationale, and undersized or unsupported display
  fallback when applicable.
- **FR-012**: `MsgCls` proof MUST address custom message triggering, routing,
  observable result, repeated-trigger stability, and intentional differences
  from the historical message-class structure.
- **FR-013**: `Tutorial` proof MUST address each step's learning target or
  defining behavior, token identity, and sequence relationship.
- **FR-014**: `Videomode` proof MUST address real capability outcome or clear
  fallback, post-transition usability, and any modern platform limitation.
- **FR-015**: The feature MUST classify every Wave-1 helper, headless, or
  direct proof path used by the relevant smokes as `SetupOnly`,
  `PrimaryProof`, `SupplementalProof`, or `LegacyOrTemporary`.
- **FR-016**: Any helper classified as `PrimaryProof` MUST execute real
  example or application logic through public commands, events, application
  methods, or stable public state, MUST include concrete behavior assertions,
  and MUST have a documented reason why that is acceptable for functional
  hardening.
- **FR-017**: Any helper classified as `LegacyOrTemporary` MUST identify the
  later visual-remediation responsibility it prepares.
- **FR-018**: Guide and evidence updates MUST document intentional historical
  deviations in German first and English second at CEFR-B2 readability.
- **FR-019**: User-facing proof and guide text MUST remain text-first and
  accessible for screen readers, Braille displays, and text browsers.
- **FR-020**: The feature MUST NOT add Wave-2, Wave-3, Wave-4, mouse-only,
  broad framework-redesign, or visual-remediation scope.
- **FR-021**: The feature MUST state that `Lastenheft_Wave1-Visual-Component-Remediation.md`
  is follow-up context and not part of this feature's acceptance boundary.
- **FR-022**: The feature MUST state that the current Wave-3 next-step marker
  remains open and is not completed by this Wave-1 quality follow-up.
- **FR-023**: When managed runtime behavior exists for a covered historical
  core point, the feature MUST provide executable smoke proof. Evidence-only
  proof MAY satisfy didactic or historical-only cases only when the evidence
  explicitly records that no direct managed runtime target exists and documents
  the intentional deviation or omission.
- **FR-024**: Guide and README updates MAY summarize or link to the Wave-1
  hardening matrix, but acceptance of historical proof, helper classification,
  and deviation decisions MUST be based on
  `specs/014-wave1-functional-hardening/pr-evidence.md`.
- **FR-025**: Helper or headless paths that only prepare controlled state,
  inspect private implementation details, or bypass the behavior under review
  MUST NOT be accepted as `PrimaryProof`; they MUST be classified as
  `SetupOnly`, `SupplementalProof`, or `LegacyOrTemporary` according to their
  role.
- **FR-026**: Every relevant negative or fallback path that affects acceptance
  MUST either be reproducible by smoke proof or, when local or CI validation
  cannot trigger it deterministically, be documented in
  `specs/014-wave1-functional-hardening/pr-evidence.md` with trigger, expected
  deviation, observed fallback, and proof boundary.
- **FR-027**: When historical review finds a missing core function, the
  feature MUST implement and smoke-prove it if it is necessary for the
  existing Wave-1 functional purpose and feasible without broad framework
  work, visual remediation, new runtime dependencies, or behavior outside this
  feature's scope; otherwise the feature MUST record it in
  `specs/014-wave1-functional-hardening/pr-evidence.md` as an intentional
  deviation or follow-up.
- **FR-028**: Affected learner-facing guides or `examples/README.md` MUST be
  updated when the feature changes runtime behavior, usage path, visible
  output, historical deviation, or learner-facing proof explanation; review-only
  helper classifications, proof boundaries, and traceability details MAY remain
  only in `specs/014-wave1-functional-hardening/pr-evidence.md`.

### Constitution Requirements *(mandatory)*

- **CR-001**: This feature targets existing Level-2 project artefacts and MUST
  use the matching Level-2 Project Environment Registry entry from
  `constitution.md` as binding project context.
- **CR-002**: User-facing artefacts MUST identify their A11Y review path:
  WCAG 2.2 Level AA where generated or rendered documentation is affected, and
  text-first review otherwise.
- **CR-003**: Learner-facing or shared guidance content MUST be German-first
  and English-second unless a synchronized `.EN.md` companion is explicitly
  selected.
- **CR-004**: The feature MUST decide during planning whether project
  statistics and synchronized AI-agent guidance files require updates. If no
  active feature context or shared workflow rule changes, the rationale for
  leaving them unchanged MUST be recorded.
- **CR-005**: The primary implementation language remains C#/.NET for managed
  example and smoke-test work and is on the project's MSL allow-list.
- **CR-006**: NIST SSDF and CWE Top 25 remain mandatory governance context.
  Other security standards from `constitution.md` MUST be marked applicable or
  `N/A` with rationale during planning.
- **CR-007**: OWASP ASVS is expected to be `N/A` unless planning introduces a
  web, API, HTTP, authentication-bearing, or externally reachable service.
- **CR-008**: SBOM, VEX, and SLSA evidence MUST be evaluated for dependency or
  releasable-artifact impact; no new runtime dependency is expected from this
  specification.
- **CR-009**: AI-SBOM is `N/A` for this feature as specified because AI is used
  only as a development aid and no runtime or product AI is delivered. If
  planning adds runtime AI, models, datasets, AI infrastructure, or delivered
  AI components, this decision MUST be re-evaluated.
- **CR-010**: CAPEC and Zero Trust are expected to be `N/A` unless planning
  changes trust boundaries, externally reachable flows, or distributed/service
  architecture.
- **CR-011**: The default governance evidence locations under `docs/security/`
  SHOULD be used unless planning records an explicitly justified equivalent.
- **CR-012**: The installed Spec-Kit governance presets apply by default:
  `security-governance`, `a11y-governance`, `agent-parity-governance`,
  `architecture-governance`, `isaqb-architecture-governance`, and
  `cross-platform-governance`.

### Key Entities *(include if feature involves data)*

- **Wave1FunctionalReview**: One primary evidence record in
  `specs/014-wave1-functional-hardening/pr-evidence.md` for each covered
  example area; identifies historical intent, current behavior, proof status,
  helper classification, negative or fallback proof status, and deviation
  status.
- **TutorialStepReview**: One review record for each token from `tvguid01` to
  `tvguid16`; preserves individual learning-target traceability.
- **HistoricalSourceReference**: A read-only source reference used to justify a
  proof or deviation decision.
- **SmokeProofClassification**: A proof record that distinguishes functional
  behavior proof from startup, static text, setup, or supplemental proof.
- **HelperClassification**: A classification of helper or headless proof paths
  as `SetupOnly`, `PrimaryProof`, `SupplementalProof`, or
  `LegacyOrTemporary`, with `PrimaryProof` reserved for paths that execute real
  example or application logic through public commands, events, application
  methods, or stable public state.
- **IntentionalDeviationRecord**: A documented difference between historical
  behavior and the managed example, including the reason and learner-facing
  explanation requirement and whether a guide or README update is required.
- **NegativeFallbackProof**: A smoke-backed or evidence-backed record for a
  relevant unsupported, invalid, undersized, or platform-limited path,
  including trigger, expected deviation, observed fallback, and proof boundary.
- **MissingCoreFunctionDecision**: A primary evidence record for a historical
  core function that is not fully present in the current managed example;
  identifies whether the function is implemented in this feature, intentionally
  omitted, or deferred as follow-up, with the reason and proof status.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of the four Wave-1 example areas have a documented primary
  evidence record in `specs/014-wave1-functional-hardening/pr-evidence.md`
  with historical source, current behavior, proof method, helper
  classification, and deviation or omission decision.
- **SC-002**: 16 of 16 `Tutorial` steps remain individually traceable,
  selectable, and covered by step-specific proof.
- **SC-003**: Each Wave-1 example area has at least one proof that verifies a
  historically relevant function rather than only launch or static text.
- **SC-004**: 100% of Wave-1 helper or headless proof paths used by the
  relevant smokes are classified with one of the four accepted labels, and no
  path that only prepares state, inspects private implementation details, or
  bypasses behavior is counted as `PrimaryProof`.
- **SC-005**: 100% of intentional historical deviations found during this
  feature are documented in evidence or learner-facing guidance.
- **SC-006**: A reviewer can determine from
  `specs/014-wave1-functional-hardening/pr-evidence.md` whether each Wave-1
  behavior is ready for later visual remediation, already adequately proven for
  functional hardening, or intentionally out of scope.
- **SC-007**: 100% of relevant negative or fallback paths that affect
  acceptance have either deterministic smoke proof or a primary evidence record
  in `specs/014-wave1-functional-hardening/pr-evidence.md` with trigger,
  expected deviation, observed fallback, and proof boundary.
- **SC-008**: 100% of missing historical core functions found during review
  have a `MissingCoreFunctionDecision` in
  `specs/014-wave1-functional-hardening/pr-evidence.md`; all decisions marked
  implemented in this feature include matching smoke proof.
- **SC-009**: 100% of runtime behavior, usage path, visible output, historical
  deviation, and learner-facing proof explanation changes are reflected in the
  affected guide or `examples/README.md`; review-only classification details
  are present in the primary evidence matrix when no guide or README update is
  required.

## Assumptions

- The existing Wave-1 delivery remains accepted; this feature strengthens
  proof quality and does not revoke prior acceptance.
- Historical sources under `tv203s/` are read-only and are not modified.
- `Lastenheft_Wave1-Visual-Component-Remediation.md` is follow-up context only.
- The current project next-step marker for Wave 3 remains open and is not
  changed by this feature unless a later planning decision explicitly updates
  project governance.
- No new runtime dependency, database, network service, runtime AI component,
  mouse-only requirement, or broad framework redesign is expected.
- Existing guides and `examples/README.md` remain learner-facing summaries;
  they supplement but do not replace the primary feature proof matrix.
