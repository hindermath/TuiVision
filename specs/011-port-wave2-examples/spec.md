# Feature Specification: Port Wave 2 Examples

**Feature Branch**: `011-port-wave2-examples`  
**Created**: 2026-05-05  
**Status**: Draft  
**Input**: User description: "Erstelle eine Spezifikation fuer `>>> NAECHSTER SCHRITT <<< Welle 2 - Controls und Dialoge` aus Pflichtenheft.md"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Run the core controls and dialogs demo set (Priority: P1)

As a maintainer, I want the wave-2 example set to run as reviewable applications so that the project can prove the controls and dialogs layer with real example flows instead of isolated component checks.

**Why this priority**: `Pflichtenheft.md` marks wave 2 as the next required work item. The `demo`, `sdlg`, `sdlg2`, and `dlgdsn` examples are the broadest integration proof for the control and dialog layer.

**Independent Test**: A reviewer can run the delivered wave-2 examples in headless or normal mode, observe deterministic startup and exit behavior, and verify that each example demonstrates its promised user flow without relying on private test-only behavior.

**Acceptance Scenarios**:

1. **Given** the wave-1 examples are already delivered, **When** a reviewer starts the wave-2 port set, **Then** all eleven wave-2 examples are present in the repository with consistent project metadata, executable entry points, and a short guide.
2. **Given** `demo` is the broad integration example, **When** it is run, **Then** it presents a coherent controls-and-dialogs experience that exercises the wave-2 surface without pulling editor, help, terminal, or charset behavior into scope.
3. **Given** `sdlg`, `sdlg2`, and `dlgdsn` are standard-dialog consumers, **When** they are run, **Then** file, directory, color, display, validation, and dynamic-dialog flows are visible as reusable user workflows.

---

### User Story 2 - Validate focused widget examples (Priority: P2)

As a learner, I want each smaller wave-2 example to demonstrate one clear control or interaction family so that I can understand lists, input/history, clipboard, combo boxes, progress, and dynamic text without reading the large demo first.

**Why this priority**: The wave-2 checklist contains several narrow examples that should remain didactic and not become hidden infrastructure work.

**Independent Test**: Each focused example can be run or smoke-tested on its own and proves one named user-visible behavior with clear output or observable state.

**Acceptance Scenarios**:

1. **Given** the `clipboard` example is selected, **When** the clipboard-oriented flow is exercised, **Then** the user can observe copy, cut, paste, and input-state behavior through the example surface.
2. **Given** the `inplis`, `listvi`, and `tcombo` examples are selected, **When** keyboard navigation changes input, selection, history, or list state, **Then** the visible state stays synchronized and deterministic.
3. **Given** the `progba` and `tprogb` examples are selected, **When** progress advances or cancellation is requested, **Then** the examples show running, completed, and canceled states without inconsistent labels or stale state.
4. **Given** the `dyntxt` example is selected, **When** dynamic values change, **Then** text output updates predictably and remains readable within the available view area.

---

### User Story 3 - Preserve documentation and proof for the example wave (Priority: P3)

As a project reviewer, I want wave-2 documentation, smoke evidence, and project statistics updated in the same feature so that the repository remains traceable from the Pflichtenheft marker to the delivered examples.

**Why this priority**: The repository treats guides, statistics, and proof artifacts as formal completion criteria, not follow-up cleanup.

**Independent Test**: A reviewer can inspect the feature artifacts and confirm that each delivered wave-2 example has a guide, smoke-test coverage, and updated proof status.

**Acceptance Scenarios**:

1. **Given** a wave-2 example is delivered, **When** the documentation is reviewed, **Then** a DE-first, EN-second guide explains the example's purpose, controls, and expected interaction path at CEFR-B2 readability.
2. **Given** the full wave-2 feature is complete, **When** the proof artifacts are reviewed, **Then** `Pflichtenheft.md`, example guides, and the project statistics ledger reflect the delivered scope and the next open wave.
3. **Given** generated or user-facing documentation changes, **When** accessibility review is required, **Then** the feature records the applicable text-first or WCAG 2.2 AA review path.

### Edge Cases

- A historical wave-2 example contains optional or host-sensitive behavior that cannot be reproduced identically on every supported terminal.
- A dialog can be cancelled, closed, or validated without a successful selection.
- Lists, combo boxes, and progress displays receive empty, very small, or boundary-sized content.
- Clipboard access is unavailable or intentionally isolated in a headless test path.
- A standard dialog points at a missing, unreadable, or manually entered path.
- A dynamic dialog description is malformed or incomplete.
- Documentation generation or accessibility review is not affected by a specific example and therefore only needs recorded `N/A` reasoning.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST deliver the eleven wave-2 examples named in `Pflichtenheft.md`: `clipboard`, `demo`, `dlgdsn`, `dyntxt`, `inplis`, `listvi`, `progba`, `sdlg`, `sdlg2`, `tcombo`, and `tprogb`.
- **FR-002**: Each delivered example MUST have a runnable application surface, a deterministic smoke-test path, and a didactic guide.
- **FR-003**: The wave-2 examples MUST prove controls and dialogs behavior only; editor, help, stream, terminal-emulation, charset, and runtime-mouse features MUST remain out of scope except where an example needs an explicit placeholder or documented limitation.
- **FR-004**: `demo` MUST act as the broad integration example for wave-2 controls and dialogs and MUST not be accepted as complete if it only starts and exits without demonstrating meaningful user-visible flows.
- **FR-005**: `sdlg` and `sdlg2` MUST demonstrate standard dialog flows for file or directory selection, color or display selection, validation, cancellation, and result reporting.
- **FR-006**: `dlgdsn` MUST demonstrate dynamic or described dialog composition through a documented user workflow and MUST handle invalid or incomplete dialog descriptions as visible failures.
- **FR-007**: `clipboard` MUST demonstrate clipboard-oriented control interactions, including unavailable or isolated clipboard conditions.
- **FR-008**: `inplis`, `listvi`, and `tcombo` MUST demonstrate input, list, history, selection, and combo-box interaction with synchronized visible state.
- **FR-009**: `progba` and `tprogb` MUST demonstrate progress changes, completion, and cancellation or abort behavior.
- **FR-010**: `dyntxt` MUST demonstrate dynamic text or parameter output that updates predictably and stays readable within constrained view bounds.
- **FR-011**: Every example guide MUST be German first and English second, use CEFR-B2-readable language, and remain useful in text-first assistive setups.
- **FR-012**: The feature MUST update repository proof surfaces when scope changes, including the wave-2 checklist, the next-step marker, example documentation index points, and project statistics.
- **FR-013**: The feature MUST record any skipped, deferred, or intentionally reduced historical behavior with a clear rationale and a traceable follow-up reference when it affects acceptance.
- **FR-014**: The feature MUST preserve the wave ordering from `Pflichtenheft.md`: wave 3 cannot start until wave 2 is delivered and recorded.

### Constitution Requirements *(mandatory)*

- **CR-001**: If this feature targets a listed Level-2 project, the feature MUST use the matching Level-2 Project Environment Registry entry from `constitution.md` as binding project context.
- **CR-002**: User-facing artefacts MUST identify their A11Y review path (WCAG 2.2 Level AA where applicable, text-first fallback otherwise).
- **CR-003**: Learner-facing or shared guidance content MUST be DE-first, EN-second unless a synchronized `.EN.md` companion is explicitly chosen.
- **CR-004**: The feature MUST state whether statistics and AI-agent guidance files require synchronized updates.
- **CR-005**: The feature MUST name its primary implementation language and either confirm it is on the MSL allow-list (`constitution.md`, Principle XI) or cite the documented non-MSL justification from the Level-2 `constitution.md`.
- **CR-006**: The feature MUST determine the applicable security standards from `constitution.md`, Principles XIV-XVIII, and mark non-applicable standards as `N/A` with justification. `NIST SSDF` and `CWE Top 25` are mandatory for all Level-2 work.
- **CR-007**: If the feature includes web/API/HTTP/auth-bearing services, it MUST declare the selected `OWASP ASVS` level and verification scope.
- **CR-008**: If the feature creates releasable or distributable artefacts, it MUST declare the intended `SBOM` / `VEX` evidence path and any required provenance / `SLSA` considerations.
- **CR-009**: If the feature changes trust boundaries, externally reachable flows, or distributed/service architecture, it MUST state how `CAPEC` and `Zero Trust` applicability will be handled.
- **CR-010**: The feature MUST state whether it uses the default evidence files in `docs/security/` (`asvs-verification.md`, `supply-chain-evidence.md`, `zero-trust-applicability.md`, `samm-assessment.md`) or an explicitly justified equivalent governance location.
- **CR-011**: The feature MUST state whether general architecture evidence is required under `constitution.md`, Principle XX. If the feature affects structure, interfaces, quality attributes, runtime behavior, deployment, or technical debt, it MUST identify the expected evidence under `docs/architecture/` or record a justified `N/A` decision.

### Governance Applicability

- **GA-001 Security**: Primary implementation language is C#, which is memory-safe and listed in the MSL allow-list. `NIST SSDF` and `CWE Top 25` apply as Level-2 secure-development baselines. `OWASP ASVS` is `N/A` because this feature does not introduce a web, API, HTTP, or authentication service. `SBOM`, `VEX`, and `SLSA` are relevant only through normal build/release provenance; no new distributable external service is introduced. Expected evidence path: update or explicitly mark `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and any project-local security checklist if the plan changes dependencies, release outputs, or trust boundaries.
- **GA-002 Architecture**: Runtime and hardware constraints do not require a non-MSL language. The feature affects runtime behavior of example applications and user-facing controls, but it does not create a new service boundary. General architecture evidence under `docs/architecture/` is required because the wave creates a reviewable example-readiness boundary for controls, dialogs, runtime smoke flows, and accepted historical-behavior reductions. ADRs are needed only if wave-2 parity requires a new cross-cutting decision.
- **GA-003 iSAQB**: The feature affects architecture goals, interfaces, runtime behavior, quality attributes, and technical debt for example readiness. Expected architecture evidence includes a lightweight context or architecture-vision note, runtime-view notes for smokeable example flows, quality-scenario notes for example readiness, and architecture-risk records for any historical example behavior that cannot be reproduced directly.
- **GA-004 A11Y**: User-facing artifacts include terminal examples, guides, smoke-test output, and possibly generated documentation. WCAG 2.2 AA applies to generated HTML documentation when changed; terminal examples require text-first and keyboard-first review paths. Bilingual DE-first, EN-second delivery is required for learner-facing guides. Expected evidence path: update `docs/accessibility/` only when a concrete accessibility proof artifact is introduced or changed; otherwise record `N/A` in the plan.
- **GA-005 Cross-platform scripts**: This feature is not expected to add, change, or remove script-shaped tools. Bash and PowerShell parity, Unix man pages, PowerShell help blocks, `Verb-Noun` naming, dry-run, and `-WhatIf` parity are `N/A` unless planning introduces a script or command-line helper.
- **GA-006 Agent parity**: Active feature context and project statistics are likely affected. Maintained agent surfaces are `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`. They must be reviewed together if this feature changes active guidance, technologies, next-step markers, or shared workflow rules.

### Key Entities *(include if feature involves data)*

- **Wave 2 Example**: One of the eleven required examples in the controls-and-dialogs wave; key attributes are name, user-visible purpose, status, guide path, smoke-test evidence, and any accepted limitation.
- **Example Guide**: A learner-facing documentation page for one example; key attributes are DE-first explanation, EN-second explanation, expected interaction path, accessibility considerations, and relationship to the example.
- **Smoke Scenario**: A deterministic proof path for an example; key attributes are startup condition, exercised interaction, observable result, and cross-platform notes.
- **Dialog Flow**: A user-visible sequence for selecting, validating, cancelling, or reporting dialog state; key attributes are initial state, user action, result state, and failure behavior.
- **Progress Flow**: A user-visible sequence for running, completing, or cancelling progress; key attributes are current value, final state, cancellation state, and visible text.
- **Wave Proof Record**: The repository-visible evidence that the wave is complete; key attributes are checked Pflichtenheft item, updated next-step marker, statistics entry, and validation evidence.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All eleven wave-2 examples can be started through their documented entry points and complete their smoke-test path without unhandled exceptions.
- **SC-002**: Each wave-2 example has one dedicated guide, so the number of example guides increases from 4 to 15.
- **SC-003**: The example smoke suite includes coverage for all 15 delivered examples after this feature, including the 4 existing wave-1 examples and the 11 new wave-2 examples.
- **SC-004**: At least one smoke scenario covers each required wave-2 interaction family: clipboard, list/input/history, combo box, progress, dynamic text, standard dialogs, dynamic dialog design, and broad demo integration.
- **SC-005**: A reviewer can trace every wave-2 checklist item in `Pflichtenheft.md` to an example project, a guide, and a smoke scenario.
- **SC-006**: The feature records the next open work item after wave 2 by moving the next-step marker to wave 3 only after wave-2 proof is complete.
- **SC-007**: No wave-3 or wave-4 example is counted toward wave-2 completion.
- **SC-008**: User-facing documentation added by the feature passes a text-first review path and, when generated HTML is changed, the applicable accessibility smoke path is recorded.

## Assumptions

- The feature starts after framework readiness work from `008-controls-revision`, `009-controls-widgets-and-collections`, and `010-standard-dialogs-designer` is available.
- Wave 2 is a porting and proof feature, not a new framework-design feature; missing framework behavior discovered during planning should be scoped only when it blocks a required wave-2 example.
- The historical examples under `tv203s/contrib/tvision/examples` are the source-of-truth input for wave-2 behavior, while the C# examples may use project-appropriate idioms and documented limitations.
- Existing wave-1 smoke-test patterns remain the baseline for deterministic example validation.
- Examples remain keyboard-first and text-first; terminal-mouse support is not required for wave-2 acceptance.
- The feature may update documentation and statistics in the same work item because repository governance treats them as completion criteria.
