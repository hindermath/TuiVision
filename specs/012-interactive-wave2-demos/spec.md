# Feature Specification: Interactive Wave 2 Demos

**Feature Branch**: `012-interactive-wave2-demos`
**Created**: 2026-05-09
**Status**: Draft
**Input**: User description: "Use `Lastenheft_Interactive-Wave2-Demos.md` as input. Create a feature for interactive Wave-2 demos."

## Clarifications

### Session 2026-05-09

- Q: How strict must primary smoke tests be about exercising the real app event loop? -> A: Primary smoke proof per example must use `app.Run()` or the real app loop with injected `TEvent`, command, or key events; direct methods may only be setup or supplemental evidence.
- Q: What side-effect boundary applies to file, path, and dialog-designer demo paths? -> A: File/path and dialog-designer paths remain read-only toward existing user data; they use source-controlled fixtures or test temp directories, do not read file contents as demo proof, and do not write persistent user data.
- Q: Which repository-visible proof artifacts must be explicit completion criteria? -> A: `specs/012-interactive-wave2-demos/pr-evidence.md` must be maintained, and `examples/README.md` must be updated when the visible operation model changes.
- Q: Which test commands are formal completion evidence for this feature? -> A: Completion requires both `dotnet test tests/TuiVision.Examples.SmokeTests/` as the fast Wave-2 smoke proof and a green full `dotnet test` run.

### Session 2026-05-10

- Q: Which historical-source review obligation applies before task generation and implementation? -> A: Each Wave-2 example's planned interaction must be compared with the relevant read-only `.c`/`.cc` source under `tv203s/`, plus important matching headers when declarations are needed; intentional user-visible deviations must be recorded in guide or PR evidence.
- Q: Which validation evidence supplements the fast smoke and full test completion proof? -> A: Repository merge evidence must also record the configured coverage gate, formatting gate, and DocFX/A11Y proof when documentation is refreshed; unavailable commands or unchanged evidence surfaces require a written rationale in `pr-evidence.md` or the matching governance artifact.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Demo as the visible vertical slice (Priority: P1)

As a learner, I want to start the broad Wave-2 demo and immediately see visible commands, status hints, and result states, so that I understand which controls and dialog concepts the demo presents.

**Why this priority**: The broad demo is the fastest way to prove the intended learner experience before the same interaction model is repeated across the smaller examples.

**Independent Test**: A reviewer can start the demo, trigger at least three visible commands, and verify that the screen state changes after each command without relying on a direct proof method.

**Acceptance Scenarios**:

1. **Given** the demo starts normally, **When** a learner opens the visible command surface, **Then** at least three existing Wave-2 behaviours are discoverable from menus, keyboard paths, or equivalent command labels.
2. **Given** a visible demo command is activated, **When** the command completes, **Then** the application shows a text-first result state that names the completed action and exposes the important outcome.
3. **Given** the same demo command path is exercised by the smoke suite, **When** the test injects the user-facing command path, **Then** the same visible result state is verified.

---

### User Story 2 - Every Wave-2 example has a real operation path (Priority: P1)

As a manual reviewer, I want to start each Wave-2 example and trigger at least one visible main path, so that each example functions as a demonstration and not only as a test fixture.

**Why this priority**: The central gap after Wave 2 porting is the difference between provable functions and usable interactive examples.

**Independent Test**: Each of the eleven Wave-2 examples can be started on its own, operated through one visible menu, key, or command path, and checked for a text-first feedback state.

**Acceptance Scenarios**:

1. **Given** any Wave-2 example starts normally, **When** the first screen is shown, **Then** it contains a recognizable example purpose, at least one reachable operation path, and a visible feedback area or status result.
2. **Given** a reviewer triggers the main operation path of any Wave-2 example, **When** the operation finishes or is rejected, **Then** the result is visible through text, selection state, progress state, dialog state, or an explicit error message.
3. **Given** an example previously exposed its proof only through direct methods, **When** this feature is complete, **Then** the same proof-worthy behaviour is reachable through the visible application flow.

---

### User Story 3 - Smoke tests verify visible application paths (Priority: P2)

As a maintainer, I want smoke tests to drive the same user-facing paths that reviewers see, so that interactive demo behaviour is verified instead of merely claimed.

**Why this priority**: The examples must remain deterministic in continuous validation while moving from direct proof methods to visible interaction paths.

**Independent Test**: For each Wave-2 example, one smoke scenario activates a command, key, or event path through the real application loop and verifies the resulting visible state.

**Acceptance Scenarios**:

1. **Given** a Wave-2 example has a documented main operation path, **When** the smoke scenario runs, **Then** it exercises that operation through the same visible command model instead of calling the final proof method directly.
2. **Given** direct proof helpers still exist, **When** they are used by a smoke scenario, **Then** they are limited to setup or supporting evidence and not counted as the primary interaction proof.
3. **Given** all Wave-2 examples are validated, **When** the example smoke suite completes, **Then** it proves all eleven interactive main paths.

---

### User Story 4 - Guides describe the real operation paths (Priority: P2)

As an apprentice or visually impaired learner, I want each updated guide to explain the normal startup path, commands, feedback, and accessibility notes in German and English, so that the examples remain usable in text-oriented setups.

**Why this priority**: The project treats examples as learning material; visible behaviour is incomplete if the operation path is only present in tests or source code.

**Independent Test**: A reviewer can read each affected guide and follow the documented operation path without needing internal test method names.

**Acceptance Scenarios**:

1. **Given** a Wave-2 example guide is updated, **When** a learner reads it, **Then** the guide names the normal startup path, the primary operation path, expected feedback, and accessibility considerations.
2. **Given** headless seams or direct methods remain documented, **When** the guide presents user operation, **Then** those details appear only as proof or developer notes and are not described as the main user workflow.
3. **Given** generated documentation is refreshed for the changed content, **When** accessibility validation is required, **Then** the matching text-first or WCAG 2.2 AA proof path is recorded.

### Edge Cases

- The clipboard is unavailable, isolated, or intentionally simulated as unavailable.
- A file or dialog operation is cancelled before a valid selection is made.
- A manually entered path or dialog-description fixture is invalid and must be rejected visibly.
- A file, path, or dialog-designer operation points at existing user data; the demo must keep the operation read-only, avoid file-content reads as proof, and avoid persistent writes.
- Lists, combo boxes, and dynamic text examples receive empty, boundary-sized, or narrow-viewport content.
- A progress example is completed, partially advanced, or cancelled.
- Scrollable dialog examples target a control or cell outside the first visible viewport.
- The terminal environment cannot support optional mouse behaviour; keyboard and command paths still remain sufficient.
- A historical behaviour belongs to a later wave or broad parity cleanup and must be recorded as a deliberate omission.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST make all eleven Wave-2 examples visibly operable from normal startup: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- **FR-002**: Each Wave-2 example MUST show more than an empty application shell on first screen: it MUST expose an example purpose, one reachable operation path, and one visible feedback mechanism.
- **FR-003**: Each primary example behaviour MUST be reachable through a menu, keyboard path, command surface, or equivalent visible application command.
- **FR-004**: Each executed example command MUST update visible state such as status text, desktop text, selection state, progress state, dialog result, or a user-readable error message.
- **FR-005**: A behaviour MUST NOT count as interactive when it is only a direct method call, only a computed string, or only a smoke-test-only helper with no visible application path.
- **FR-006**: The broad demo MUST serve as the P1 vertical slice and expose at least three existing Wave-2 behaviours through visible commands and result feedback.
- **FR-007**: Every Wave-2 example MUST have at least one deterministic smoke scenario that verifies a command, key, or event path through `app.Run()` or the real application loop with injected `TEvent`, command, or key events.
- **FR-008**: Existing direct proof helpers MAY remain as supporting evidence, but the primary acceptance proof for each example MUST be the visible application path.
- **FR-009**: Shared command names, interaction labels, or testing utilities SHOULD be used when multiple examples need the same operation pattern; example-specific special cases MUST be justified by the example's purpose.
- **FR-010**: The feature MUST NOT add Wave-3 or Wave-4 functionality, mandatory runtime mouse support, broad framework redesign, or unrelated documentation-platform changes.
- **FR-011**: The `Clipboard` example MUST expose copy, cut, paste, and unavailable-clipboard feedback through visible operation paths.
- **FR-012**: The `Demo` example MUST expose broad controls/dialogs flow, file or path metadata feedback, cancellation or invalid-state feedback, and color/display-style feedback through visible commands.
- **FR-013**: The `DlgDsn` example MUST visibly load, render, change, and reject dialog descriptions, including clear feedback for invalid descriptions.
- **FR-014**: The `DynTxt` example MUST visibly demonstrate short text, long text, and constrained-width or narrow-viewport feedback.
- **FR-015**: The `InpLis`, `ListVi`, and `TCombo` examples MUST visibly demonstrate list, selection, input, history, boundary, or empty-state behaviour appropriate to each example.
- **FR-016**: The `ProgBa` example MUST visibly demonstrate progress through completion, and the `TProgB` example MUST visibly demonstrate partial progress, abort, and cancelled state.
- **FR-017**: The `Sdlg` and `Sdlg2` examples MUST visibly demonstrate scroll and focus behaviour for content outside the initial viewport, including the combined horizontal/vertical case for `Sdlg2`.
- **FR-018**: The eleven Wave-2 guides MUST be updated so normal startup, primary operation path, expected feedback, and accessibility notes are described in German first and English second.
- **FR-019**: Proof artifacts MUST make clear that Wave 2 is fully learner- and review-ready only after this interactive showcase stage is complete.
- **FR-020**: Project statistics and formal requirement/proof surfaces MUST be updated when the feature is implemented and validated.
- **FR-021**: File, path, and dialog-designer demo paths MUST remain read-only toward existing user data, MUST use source-controlled fixtures or test temp directories for proof data, MUST NOT read file contents as the demo proof, and MUST NOT write persistent user data.
- **FR-022**: `specs/012-interactive-wave2-demos/pr-evidence.md` MUST record local and CI evidence for this feature, and `examples/README.md` MUST be updated when the visible Wave-2 operation model changes.
- **FR-023**: Completion evidence MUST include a successful `dotnet test tests/TuiVision.Examples.SmokeTests/` run and a successful full `dotnet test` run.
- **FR-024**: Each Wave-2 example MUST include a historical source review against the relevant read-only `.c`/`.cc` files under `tv203s/`, and important matching headers when declarations are needed; the review MUST record the original demo intent and any intentional user-visible deviation.
- **FR-025**: Repository merge evidence MUST record the configured coverage gate, formatting gate, and DocFX/A11Y validation when documentation is refreshed; unavailable commands or intentionally unchanged evidence surfaces MUST be justified in `pr-evidence.md` or the matching governance artifact.

### Constitution Requirements *(mandatory)*

- **CR-001**: This feature targets the TuiVision Level-2 project and MUST use the matching Level-2 Project Environment Registry entry from `constitution.md` as binding project context.
- **CR-002**: User-facing terminal examples, guides, smoke-output descriptions, and generated documentation changes MUST identify their A11Y review path. Terminal examples use keyboard-first and text-first review; generated HTML uses WCAG 2.2 Level AA where changed.
- **CR-003**: Learner-facing guide updates MUST be DE-first, EN-second at CEFR-B2 readability. A synchronized `.EN.md` sidecar is not selected for this feature.
- **CR-004**: `docs/project-statistics.md` MUST be updated at implementation completion. AI-agent guidance files require synchronized review only if planning changes active feature context, technologies, or shared workflow rules.
- **CR-005**: The primary implementation language is C#, which is on the MSL allow-list in `constitution.md`, Principle XI.
- **CR-006**: `NIST SSDF` and `CWE Top 25` apply as Level-2 secure-development baselines. The feature MUST record whether existing security evidence remains sufficient or needs a targeted update.
- **CR-007**: `OWASP ASVS` is `N/A` because this feature does not introduce a web, API, HTTP, or authentication-bearing service.
- **CR-008**: `SBOM`, `VEX`, and provenance evidence are limited to the repository's normal build/release evidence unless planning introduces new distributable artifacts.
- **CR-009**: `CAPEC` and `Zero Trust` are `N/A` unless planning introduces a changed trust boundary, externally reachable flow, or service architecture.
- **CR-010**: Default governance evidence locations under `docs/security/` remain the expected security evidence path when updates are needed.
- **CR-011**: General architecture evidence is required because the feature affects runtime behaviour, interactive example readiness, testable UI paths, and accepted historical-behaviour boundaries. Expected evidence belongs under `docs/architecture/` or the feature's planning evidence with explicit links.

### Key Entities

- **Interactive Example**: One Wave-2 example application with a name, purpose, visible operation path, feedback state, guide, smoke scenario, and accepted limitations.
- **Operation Path**: A user-facing route that starts at normal example startup and reaches a command, menu, keyboard action, or equivalent visible application command.
- **Visible Feedback State**: The observable result after an operation path, such as status text, selected item, dialog result, progress value, cancellation state, or readable error message.
- **Smoke Scenario**: A deterministic validation path that exercises the same application operation path a learner or reviewer can use through `app.Run()` or the real application loop with injected events, and verifies the visible feedback state.
- **Guide Update**: A learner-facing documentation update that explains startup, operation path, expected feedback, accessibility notes, and proof notes in German and English.
- **Historical Source Review**: A per-example review that records the relevant `.c`/`.cc` source files, important matching headers when needed, original historical demo intent, planned interactive C# path, and intentional user-visible deviations.
- **Omission Record**: A traceable note for historical behaviour that is intentionally out of scope, belongs to a later wave, or should be handled as separate parity cleanup.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All eleven Wave-2 examples show at least one visible operation path from normal startup.
- **SC-002**: The broad demo exposes at least three visible commands and three corresponding visible result states.
- **SC-003**: The example smoke suite contains at least one visible application-path smoke scenario for each of the eleven Wave-2 examples, and each primary scenario runs through `app.Run()` or the real application loop with injected events.
- **SC-004**: The interactive smoke scenarios cover all required interaction families: clipboard, broad demo/dialog flow, dynamic dialog description, dynamic text, input/list/history, list bounds, combo selection, progress completion, progress abort, vertical scroll/focus, and horizontal/vertical scroll/focus.
- **SC-005**: All eleven affected Wave-2 guides describe normal startup, operation path, expected feedback, and accessibility notes in German and English.
- **SC-006**: A reviewer can trace every Wave-2 example from guide to visible operation path to smoke evidence.
- **SC-007**: No Wave-3 or Wave-4 behaviour is required or counted toward completion.
- **SC-008**: Existing Wave-2 proof coverage remains present or is replaced by stronger visible-path proof for the same behaviour.
- **SC-009**: `Pflichtenheft.md` and project statistics reflect that Wave 2 is interactively review-ready only after this showcase stage is complete.
- **SC-010**: When generated documentation is refreshed, the matching accessibility smoke path completes without serious accessibility violations.
- **SC-011**: `pr-evidence.md` traces the completed interactive paths, validation evidence, and any CI proof, and `examples/README.md` is either updated for the changed operation model or explicitly recorded as unchanged.
- **SC-012**: The fast Wave-2 smoke proof and the full repository test proof are both recorded as passing completion evidence.
- **SC-013**: Every Wave-2 example has traceable historical-source review evidence, and merge evidence records coverage, formatting, and DocFX/A11Y outcomes or justified non-applicability.

## Assumptions

- The starting branch includes the merged Wave-2 port and review-cleanup baseline from 011, including the corrected dialog-designer fixture handling and scroll/focus proofs.
- The eleven Wave-2 examples already have functional proof methods that can be reused or routed through visible commands.
- Task generation should include a short prerequisite verification or reference for reused 011 proof methods before those methods are routed into visible interactive paths.
- Keyboard-first operation is the acceptance baseline; mouse operation can remain optional or omitted.
- The implementation may add minimal shared helpers for command routing, visible status, or smoke-event injection when that reduces duplication across examples.
- Broad framework redesign is out of scope unless planning proves that a small reusable control or helper is necessary to expose the required interaction paths.
- Existing direct proof methods may remain available for developer evidence, but they no longer satisfy primary user-facing acceptance by themselves.
- The root Lastenheft remains the intake artifact for this feature until implementation completion; branch-specific renaming is handled when the feature is delivered.
