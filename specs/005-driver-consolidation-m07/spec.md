# Feature Specification: Driver Consolidation and M-07 Porting Proof

**Feature Branch**: `005-driver-consolidation-m07`  
**Created**: 2026-03-23  
**Status**: Ready for Planning  
**Input**: User description: "Bitte >>> NAECHSTER SCHRITT <<< 1. Phase 7 Treiberkonsolidierung abschliessen. als Naechstes gezielt an Phase 7 und schaue zuerst auf M-07-relevante Treiberreste und das fehlende docs/porting-status.md."

## Clarifications

### Session 2026-03-23

- Q: Wie soll `docs/porting-status.md` historische Dateien behandeln, deren Verantwortung heute auf mehrere Zielbereiche verteilt ist? → A: Ein Primaerziel ist Pflicht, zusaetzliche Zielbereiche oder Dateien duerfen ergaenzend genannt werden.
- Q: Wie verbindlich muessen Linux- und Windows/WSL-Kompatibilitaetspruefungen in Phase 7 bereits nachgewiesen sein? → A: Sie muessen fuer Phase 7 nachweisbar getestet werden, duerfen aber noch manuell oder halbautomatisch belegt sein.

## User Scenarios & Testing *(mandatory)*

This feature does not port any of the 25 mandatory original examples from
`tv203s/contrib/tvision/examples` yet. It prepares the framework and the
porting proof that must be complete before mandatory example waves 1 to 4 may
begin. `TVDEMOS/` and `TVFM/` remain outside this feature scope.

### User Story 1 - Close the driver-consolidation gap (Priority: P1)

As a framework maintainer, I want the remaining historical driver variants to be
consolidated into one managed console-driver baseline so that the framework no
longer depends on unresolved platform-specific driver behavior before the final
example wave.

**Why this priority**: Phase 7 is the current top-priority work item in the
Pflichtenheft. Without a credible consolidated driver baseline, the project
cannot claim framework completeness and the later terminal-heavy examples remain
blocked.

**Independent Test**: Review the consolidated driver scope against the known
historical driver families, exercise the managed driver on the primary macOS
workflow and the defined compatibility environments, and verify that every
remaining platform-specific case has either a supported managed behavior or a
documented replacement decision.

**Acceptance Scenarios**:

1. **Given** the historical codebase contains multiple platform-specific driver
   variants, **When** this feature is complete, **Then** each relevant driver
   responsibility is either covered by the managed console-driver baseline or
   explicitly documented as a conscious replacement decision.
2. **Given** Phase 7 is the prerequisite for the last mandatory example wave,
   **When** a reviewer inspects the resulting framework state, **Then** no
   unresolved driver gap remains that still blocks terminal-emulation or
   extended-character work on principle.

---

### User Story 2 - Prove M-07 completeness with a mapping ledger (Priority: P2)

As a reviewer, I want a complete, reviewable mapping of the historical
implementation files to their current framework targets so that M-07 can be
checked objectively instead of by memory or ad-hoc repository searches.

**Why this priority**: The Pflichtenheft explicitly names `docs/porting-status.md`
as the required proof artifact before Phase 8 may start. Without that ledger,
M-07 remains formally open even if most framework code already exists.

**Independent Test**: Open the mapping ledger, sample entries from both shared
framework code and platform-specific driver folders, and confirm that each file
has a target area, a current status, and a justification whenever it was not
ported one-to-one.

**Acceptance Scenarios**:

1. **Given** a reviewer selects any implementation file from
   `tv203s/contrib/tvision/classes`, **When** the reviewer consults the mapping
   ledger, **Then** the file appears with its source path, current target,
   current status, and the related proof or explanation.
2. **Given** a historical file was not carried over directly, **When** the file
   is listed in the ledger, **Then** the entry explains why the behavior is
   replaced, merged, or intentionally omitted instead of leaving an open gap.

---

### User Story 3 - Prepare the Phase-8 entrance gate (Priority: P3)

As a project lead, I want Phase 7 and the M-07 proof packaged as a clear
precondition set for the Phase-8 entrance gate so that the team can start the
mandatory example waves only after framework completeness is demonstrable.

**Why this priority**: The project must avoid starting example ports on an
incomplete or weakly evidenced framework baseline. A clear entrance-gate view
reduces rework and is easier to communicate to trainees.

**Independent Test**: Compare the completed feature artifacts against the
Phase-8 gate in the Pflichtenheft and confirm that the remaining gate work is
reduced to explicit, reviewable follow-up items rather than unclear framework
unknowns.

**Acceptance Scenarios**:

1. **Given** the team reviews the prioritized rest-work section in the
   Pflichtenheft, **When** this feature is finished, **Then** Phase 7 and the
   M-07 evidence package are concrete, traceable prerequisites for Phase 8
   instead of loosely described intentions.
2. **Given** mandatory example work remains out of scope for this feature,
   **When** the feature is reviewed, **Then** the resulting artifacts clearly
   separate framework-completion proof from later example-port execution.

### Edge Cases

- How is a historical driver file handled when its original platform-specific
  behavior is no longer reproduced directly but its responsibility is covered by
  a managed cross-platform replacement?
- What happens when one historical source file contributes behavior to more than
  one current target area instead of mapping neatly to a single destination?
  The proof ledger must still assign one review-leading primary target and may
  list additional secondary targets where needed.
- How is the proof ledger handled when a historical source file has no current
  runtime equivalent because the modern managed architecture deliberately
  removes that dependency?
- What happens when compatibility validation on macOS, Linux, and Windows/WSL
  exposes different observable driver limits for the same historical behavior?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST define the remaining scope of Phase 7 as the
  consolidation of historical platform-specific driver responsibilities into the
  managed console-driver baseline used by the project.
- **FR-002**: The system MUST provide a reviewable inventory of all historical
  implementation files under `tv203s/contrib/tvision/classes`, including the
  platform-specific driver subdirectories that influence the managed driver
  baseline.
- **FR-003**: The feature MUST provide a maintained mapping ledger in
  `docs/porting-status.md` that lists each historical implementation file with
  its source path, one mandatory primary target area, any additional secondary
  target areas where applicable, current completion status, and the related
  evidence or rationale.
- **FR-004**: The mapping ledger MUST distinguish at least between behavior that
  is ported and verified, ported but still awaiting explicit verification, and
  consciously replaced or omitted with a documented reason.
- **FR-005**: The mapping ledger MUST NOT leave any historical implementation
  file in an undefined state such as an unexplained gap, placeholder, or
  undocumented omission.
- **FR-006**: For platform-specific historical driver files, the feature MUST
  state whether each responsibility is covered by the managed driver baseline,
  merged into another target area, or intentionally not reproduced as a direct
  one-to-one port.
- **FR-007**: The feature MUST document the driver-consolidation decisions in a
  form that can be reviewed by maintainers and trainees without requiring them
  to reconstruct the project state from commit history alone.
- **FR-008**: The feature MUST preserve the project rule that mandatory example
  waves 1 to 4 cannot begin until the framework-completeness gate is satisfied;
  this feature therefore prepares the gate but does not count as example-port
  scope itself.
- **FR-009**: The feature MUST name the primary validation environments for the
  consolidated driver baseline as the two macOS development machines plus Linux
  and Windows compatibility validation, with Windows preferably exercised
  through current Ubuntu on WSL. For this feature, Linux and Windows/WSL
  validation must be demonstrably executed, but the evidence may still be
  manual or semi-automated rather than already enforced as a mandatory CI gate.
- **FR-010**: The feature MUST identify the remaining explicit follow-up proof
  items required for the full Phase-8 entrance gate, including build, test,
  coverage, and API-documentation evidence where those gates are not closed by
  this feature alone.
- **FR-011**: The feature MUST keep the resulting specification and proof
  artifacts aligned with the Pflichtenheft terminology for Phase 7,
  `M-07`, and the Phase-8 entrance gate so that later planning and review use
  consistent language.

### Key Entities *(include if feature involves data)*

- **Historical Implementation File**: One source file from
  `tv203s/contrib/tvision/classes` or its platform-specific subdirectories that
  must be accounted for in the M-07 proof.
- **Driver Responsibility**: A historically distinct capability such as screen
  output, keyboard handling, mouse handling, display adaptation, or related
  console behavior that must be covered or consciously replaced in the managed
  baseline.
- **Managed Driver Baseline**: The current cross-platform driver behavior that
  the project treats as the maintained runtime target for terminal interaction.
- **Porting Status Entry**: One ledger row in `docs/porting-status.md` that ties
  a historical implementation file to one primary current target, optional
  secondary targets, proof state, and rationale.
- **Replacement Decision**: A documented explanation for a historical behavior
  that is merged, superseded, or intentionally not reproduced one-to-one in the
  current architecture.
- **Phase-8 Entrance Evidence**: The set of proof artifacts that demonstrate the
  framework is complete enough for mandatory example porting to begin.

## Assumptions

- The framework work from phases 1 to 6 already provides most of the non-driver
  baseline needed for M-07; this feature focuses on the remaining driver-related
  proof and consolidation gap.
- The project continues to prefer one managed console-driver architecture over
  one-to-one preservation of every historical OS-specific driver variant.
- The authoritative historical source inventory remains
  `tv203s/contrib/tvision/classes`, including the platform-specific
  subdirectories named in the Pflichtenheft.
- `docs/porting-status.md` is the primary human-readable proof artifact for
  M-07, even if supporting validation can also appear in tests, CI output, or
  later planning artifacts.
- Mandatory original examples from `tv203s/contrib/tvision/examples` remain out
  of scope for this feature and start only after the separate entrance-gate
  decision.
- Compatibility validation on Linux and Windows/WSL supplements, but does not
  replace, the primary day-to-day Multi-Mac development workflow.
- For the Phase-7 acceptance scope, Linux and Windows/WSL compatibility checks
  need reviewable evidence, but they do not yet have to exist as fully
  automated mandatory CI jobs.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: In 100% of reviewed historical implementation files under
  `tv203s/contrib/tvision/classes`, maintainers can find a corresponding entry
  in `docs/porting-status.md` without resorting to ad-hoc repository search.
- **SC-002**: In 100% of reviewed platform-specific driver entries, the ledger
  states whether the historical behavior is covered, merged, or consciously not
  reproduced one-to-one.
- **SC-003**: A reviewer can explain the remaining gap between Phase 7 and the
  full Phase-8 entrance gate in one pass through the feature artifacts without
  discovering undocumented framework unknowns.
- **SC-004**: The feature leaves zero undocumented historical implementation
  files in the M-07 scope and zero unexplained driver omissions in the proof
  ledger.
