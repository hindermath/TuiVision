# Feature Specification: Wave 2 Visual Component Remediation

**Feature Branch**: `013-wave2-visual-component-remediation`  
**Created**: 2026-05-15  
**Status**: Draft  
**Input**: User description: "Use `Lastenheft_Wave2-Visual-Component-Remediation.md` as input. Create a feature specification for `013-wave2-visual-component-remediation`. The eleven Wave-2 examples must show real visible TuiVision controls, dialogs, windows, or view groups. The 012 implementation with app-loop menus and text-first feedback is the baseline but is no longer sufficient as primary parity proof."

## Clarifications

### Session 2026-05-22

- Q: How shall `013-wave2-visual-component-remediation` formally handle the new AI-SBOM governance? -> A: `AI-SBOM: N/A` for this feature because only development and agent tooling are used; no runtime or product AI is delivered. If planning or implementation introduces runtime AI, models, datasets, AI infrastructure, or delivered AI components, the AI-SBOM decision MUST be re-evaluated.
- Q: How strictly shall primary smoke tests prove the visible UI composition? -> A: Each primary smoke test MUST run through the real app loop, verify concrete control/dialog/focus/selection/scroll/progress state, and include at least one stable rendered visibility proof.
- Q: How binding shall the description path be in each example app? -> A: Each app MUST provide a consistently named, keyboard-reachable runtime description path; a primary or supplemental smoke test MUST verify reachability and text content. The later clarification in this session fixes that canonical path as `Help -> Description`.
- Q: How shall the 013 run handle missing or weak TuiVision control capabilities? -> A: Only the smallest necessary shared control/status/test seams are allowed; larger framework gaps MUST be documented as intentional deviations and are not solved in 013.
- Q: Which validation shall count as formal completion evidence for 013? -> A: Completion evidence MUST include Release build, fast Example-Smoke suite, full Release test run, Coverlet coverage gate, and `dotnet format --verify-no-changes`; DocFX plus web-a11y are additionally required when guides, DocFX content, navigation, or API documentation are affected.
- Q: Which runtime description path shall be canonical for all eleven examples? -> A: All examples MUST use `Help -> Description` as the canonical runtime description path; `About` may provide supplemental context but is not the primary description path.
- Q: How binding shall the status line be for short dynamic status? -> A: A real `TStatusLine` is the primary status-feedback surface in each app; an equivalent status area is allowed only as a documented deviation.
- Q: Which three visible minimum flows must `Demo` prove in 013? -> A: `Demo` MUST prove three distinct flow families: `Dialog/Control`, `File/Path metadata`, and `Display/Color/Gadget`; additional demo flows are optional.
- Q: Which data sources may be used for file/path, dialog-designer, and clipboard-adjacent proof? -> A: Only source-controlled fixtures and test temporary directories may be used; arbitrary user files, persistent user history, and external proof paths are not allowed.
- Q: What counts as a stable rendered visibility proof for primary smokes? -> A: A primary smoke proof MUST combine a view-tree proof with a buffer/cell snapshot that shows control-specific content at the expected position or region.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Visible component parity per example (Priority: P1)

As a learner or manual reviewer, I want each Wave-2 example to show its
historical visual main idea during normal startup, so that I can understand the
example from the running application and not only from source code, status
text, or test helpers.

**Why this priority**: This is the central remediation goal. The already
delivered Wave-2 examples are operable, but too many still prove behavior
through text summaries rather than visible controls, dialogs, windows, or view
groups.

**Independent Test**: Each example can be started on its own and inspected for
a visible main component or stable visual runtime state that matches its
documented historical purpose.

**Acceptance Scenarios**:

1. **Given** any of the eleven Wave-2 examples starts normally, **When** the
   first usable screen is shown, **Then** the screen contains a visible main
   component or stable runtime state that represents the example's historical
   visual idea.
2. **Given** an example previously displayed only a summary sentence after a
   command, **When** the same behavior is reviewed after this feature, **Then**
   the visual component itself is present and the summary sentence appears only
   as supporting status feedback.
3. **Given** the historical source shows a concrete visual concept, **When**
   the C# example intentionally simplifies that concept, **Then** the visible
   target state and the intentional deviation are recorded in review evidence
   or learner documentation.

---

### User Story 2 - Three-layer runtime experience (Priority: P1)

As a reviewer, I want every example to follow the same three-layer model of
main component, `TStatusLine` feedback, and description path, so that Wave 2
uses one consistent quality standard instead of a different rule per example.

**Why this priority**: The user explicitly wants the earlier text feedback to
remain useful without counting as primary parity proof. The three-layer model
makes that rule clear and testable.

**Independent Test**: For each example, a reviewer can identify the main
component, the short dynamic status feedback, and a reachable description path
without reading the source.

**Acceptance Scenarios**:

1. **Given** a Wave-2 example is running, **When** a reviewer inspects the
   screen, **Then** the main area contains the visual component or stable
   visual runtime state.
2. **Given** the example changes selection, focus, scroll position, progress,
   dialog state, or validation state, **When** the change occurs, **Then** the
   `TStatusLine` reports a short text-first state unless a documented
   deviation uses an equivalent status area.
3. **Given** a learner needs explanation, **When** the learner uses
   `Help -> Description`, **Then** the text explains the visible
   component, operation path, historical intent, and accessible review path in
   clear German-first and English-second wording.

---

### User Story 3 - Smoke tests prove visible controls and dialogs (Priority: P2)

As a maintainer, I want the primary example smoke tests to verify the visible
composition itself, so that regressions cannot pass by updating only
`VisibleText`, `VisibleHistory`, or another text-only proof surface.

**Why this priority**: The existing app-loop and text-first proof remains
valuable, but it must no longer be the only acceptance proof when the
historical example demonstrates a visible control or dialog.

**Independent Test**: Each affected smoke test exercises the real application
path and verifies at least one concrete visible state such as a dialog,
window, view role, selected item, focus target, scroll offset, input value, or
progress state.

**Acceptance Scenarios**:

1. **Given** a primary smoke scenario runs for a Wave-2 example, **When** it
   triggers the example's visible operation path, **Then** it verifies a
   visible control, dialog, focus, selection, scroll, input, progress, or
   dialog-description state.
2. **Given** a direct helper remains useful, **When** it is used in tests,
   **Then** it is classified as setup or supplemental evidence and not counted
   as the primary parity proof.
3. **Given** all eleven examples are validated, **When** the smoke suite
   completes, **Then** every example has a traceable visible-composition proof.

---

### User Story 4 - Learner-ready documentation and evidence (Priority: P2)

As an apprentice, screen-reader user, or text-oriented reviewer, I want the
guides and evidence to explain the visible behavior in German and English, so
that I can understand what happens visually even when I rely on text-first
review tools.

**Why this priority**: The feature changes the meaning of acceptance for Wave
2. Documentation and evidence must make that shift visible for future
reviewers and learners.

**Independent Test**: A reviewer can open the guide or evidence for each
example and trace the historical source, visible main component, status
feedback, description path, smoke proof, accessibility notes, and intentional
deviations.

**Acceptance Scenarios**:

1. **Given** a Wave-2 guide is updated, **When** a learner reads it, **Then**
   it explains startup, visible main component, operation path, expected status
   feedback, description path, and accessibility considerations in German
   first and English second.
2. **Given** a historical deviation remains, **When** the feature evidence is
   reviewed, **Then** the deviation is explicitly tied to the historical source
   and to the visible C# target state.
3. **Given** project proof surfaces are reviewed, **When** the feature is
   complete, **Then** the guide set, example README, project statistics, and
   feature evidence all describe the visual-remediation status consistently.

### Edge Cases

- Clipboard access is unavailable, isolated, or intentionally simulated as
  unavailable; the visible component must still show current content and a
  clear unavailable state.
- A dialog-description fixture is malformed, incomplete, duplicate, or invalid
  for navigation; the example must show a visible rejection path without
  loading unsafe user data.
- Lists, input lines, combo choices, and history flows are empty, at their
  first/last item, or too narrow for the available view.
- Progress examples are at zero, partial, complete, aborted, or cancelled
  states.
- Scroll-dialog examples target content outside the first visible viewport,
  including vertical-only and horizontal-plus-vertical movement.
- A historical visual behavior depends on capability that is not yet present
  in the framework; the feature may add only the smallest necessary shared
  control, status, or test seam, while larger framework gaps must be
  documented as intentional deviations and remain out of 013 scope.
- A learner uses a text-first setup and cannot rely on layout or color alone;
  status and description paths must still explain the result.
- A future reviewer tries to count `VisibleText` or `VisibleHistory` alone as
  parity proof; the specification must make that unacceptable.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST cover exactly these eleven Wave-2 examples:
  `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`,
  `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- **FR-002**: Each covered example MUST show a real visible main component or
  stable visual runtime state during normal startup that corresponds to the
  example's historical visual intent.
- **FR-003**: Each covered example MUST follow the three-layer model: visible
  main component, short `TStatusLine` feedback, and a consistently named,
  keyboard-reachable runtime description path named `Help -> Description`.
  An equivalent status area is allowed only as a documented deviation.
- **FR-004**: A text-only status, visible text history, or direct helper proof
  MUST NOT count as the primary parity proof for any example whose historical
  purpose demonstrates a visual component.
- **FR-005**: Short status sentences already introduced by the 012 work MUST
  remain available as `TStatusLine` feedback where they help text-first
  review. An equivalent status area is allowed only as a documented deviation.
- **FR-006**: Each primary smoke proof MUST verify a concrete visible state,
  such as a view, dialog, window, focus target, selection, scroll position,
  input value, history state, progress state, dialog-description result, or
  rejection state, and MUST include at least one stable rendered visibility
  proof that combines a view-tree proof with a buffer/cell snapshot showing
  control-specific content at the expected position or region.
- **FR-007**: Primary smoke proofs MUST exercise the real application path
  through the normal application loop, command, key, or event path; direct
  helpers MAY support setup or supplemental assertions only.
- **FR-008**: `Clipboard` MUST show a visible text or input component before
  and after copy, cut, paste, and unavailable-clipboard paths.
- **FR-009**: `Demo` MUST show at least three distinct visible demo flow
  families: `Dialog/Control`, `File/Path metadata`, and
  `Display/Color/Gadget`. Additional demo flows are optional.
- **FR-010**: `DlgDsn` MUST show a visible dialog or control tree for valid
  dialog descriptions and visible rejection feedback for invalid controlled
  fixtures.
- **FR-011**: `DynTxt` MUST show a visible dynamic text view whose rendered
  content changes or demonstrates clipping, alignment, or narrow-width
  behavior.
- **FR-012**: `InpLis` MUST show a visible dialog composition with list,
  input, history or boundary behavior, and status feedback for selection or
  recall.
- **FR-013**: `ListVi` MUST show a visible list viewer or list box with
  selected item, empty-state or boundary feedback, and optional scrollbar or
  focus indication when appropriate.
- **FR-014**: `ProgBa` MUST show a visible progress-bar state through
  completion.
- **FR-015**: `Sdlg` MUST show a visible scroll-dialog or scroll-group state
  with content outside the initial visible area.
- **FR-016**: `Sdlg2` MUST show visible two-axis scroll-dialog or scroll-group
  behavior, including horizontal and vertical movement.
- **FR-017**: `TCombo` MUST show a visible input-plus-combo or selection
  composition whose displayed value and boundary or empty-state behavior can
  be verified.
- **FR-018**: `TProgB` MUST show a visible progress dialog or window with
  partial progress, abort, and cancelled states.
- **FR-019**: Each example MUST be checked against the relevant read-only
  historical C/C++ sources under `tv203s/`, plus headers when declarations are
  needed, and MUST record the historical visual intent and intentional
  user-visible deviations.
- **FR-020**: The feature MAY add a small shared control, status, or test seam
  only when it is necessary to expose the visible Wave-2 composition; broad
  framework redesign is out of scope. Larger framework gaps MUST be documented
  as intentional deviations instead of being solved in 013.
- **FR-021**: File, path, clipboard, and dialog-designer proof paths MUST use
  source-controlled fixtures or test temporary directories and MUST NOT read
  arbitrary user file contents, rely on external proof paths, or write
  persistent user history as proof.
- **FR-022**: Guides and `examples/README.md` MUST identify each example's
  visible main component, operation path, status feedback, description path,
  accessibility notes, and historical-source relationship.
- **FR-023**: Feature evidence MUST trace each example from historical source
  to visible target state to smoke proof and MUST record validation commands,
  known deviations, security rationale, architecture rationale, and A11Y
  rationale.
- **FR-024**: Each covered example MUST provide a consistently named,
  keyboard-reachable runtime description path named `Help -> Description` that
  explains the visible component and operation path. `About` MAY provide
  supplemental context but MUST NOT replace `Help -> Description` as the
  primary description path. A primary or supplemental smoke test MUST verify
  the path's reachability and text content.
- **FR-025**: The feature MUST NOT add Wave-3 or Wave-4 behavior, mandatory
  runtime mouse support, databases, external services, network dependencies,
  or unrelated documentation-platform changes.
- **FR-026**: Project proof surfaces, including project statistics and any
  relevant requirement or evidence markers, MUST be updated when the feature
  is implemented.

### Constitution Requirements *(mandatory)*

- **CR-001**: This feature targets the TuiVision Level-2 project and MUST use
  the matching Level-2 Project Environment Registry entry from
  `constitution.md` as binding project context.
- **CR-002**: User-facing terminal examples, guide changes, example README
  updates, smoke-output descriptions, and generated documentation changes MUST
  identify their A11Y review path. Terminal UI uses keyboard-first and
  text-first review; generated HTML uses WCAG 2.2 Level AA where changed.
- **CR-003**: Learner-facing guide and README changes MUST be DE-first,
  EN-second and readable at roughly CEFR-B2. A synchronized `.EN.md` sidecar
  is not selected for this feature.
- **CR-004**: `docs/project-statistics.md` MUST be updated at implementation
  completion. AI-agent guidance files require synchronized review only if
  planning changes active feature context, technologies, project structure, or
  shared workflow rules.
- **CR-005**: The primary implementation language is C#, which is memory-safe
  and on the project's MSL allow-list. No non-memory-safe language is planned.
  Under `security-governance` v0.4.0, the additional Rust, Go, Swift,
  Java/Kotlin, Python, and TypeScript/JavaScript secure-coding profiles do not
  create implementation obligations for this C#/.NET feature; the C#/.NET
  secure-coding baseline and existing TuiVision project rules continue to
  apply.
- **CR-006**: `NIST SSDF` and `CWE Top 25` apply as Level-2 secure-development
  baselines and must be considered in feature evidence.
- **CR-007**: `OWASP ASVS` is `N/A` because this feature does not introduce a
  web, API, HTTP, authentication, or authorization service.
- **CR-008**: `SBOM`, `VEX`, and `SLSA` evidence use the repository's normal
  build, release, and supply-chain evidence path unless planning introduces a
  new releasable artefact or dependency.
- **CR-009**: `AI-SBOM` is `N/A` for this feature because the feature uses AI
  only as development or agent tooling and does not deliver runtime AI,
  product AI, models, datasets, AI infrastructure, or AI components. If
  planning or implementation introduces any delivered AI element, this
  decision MUST be re-evaluated.
- **CR-010**: `CAPEC` and `Zero Trust` are `N/A` unless planning introduces a
  changed trust boundary, externally reachable flow, or service architecture.
- **CR-011**: Security evidence defaults to `docs/security/`:
  `security-checklist.md`, `asvs-verification.md`,
  `supply-chain-evidence.md`, `threat-model.md`, and
  `zero-trust-applicability.md` are updated only when plan or implementation
  changes make existing evidence incomplete; otherwise feature evidence records
  the unchanged-risk rationale.
- **CR-012**: General architecture evidence is required because the feature
  affects runtime behavior, visible UI composition, testable interaction
  contracts, accessibility quality attributes, and technical-debt boundaries.
  Expected evidence belongs under `docs/architecture/` or the feature's
  planning/evidence artefacts with explicit links.

### Governance Applicability

- **Security Governance**: The installed and applicable baseline is
  `security-governance` v0.4.0. C# is memory-safe. NIST SSDF and CWE Top 25
  are relevant as repository baselines. The v0.4.0 secure-coding profiles for
  Rust, Go, Swift, Java/Kotlin, Python, and TypeScript/JavaScript do not create
  implementation obligations for this C#/.NET feature. OWASP ASVS is N/A for
  this feature because no web/API/auth-bearing surface is added. SBOM, VEX,
  and SLSA stay on the existing repository evidence path unless planning adds a
  new dependency, packaged artefact, or release change. AI-SBOM is N/A because
  no runtime or product AI is delivered; this decision must be re-evaluated if
  delivered AI elements enter scope.
- **Architecture Governance**: Runtime behavior, visible UI composition,
  interaction testing, and accessibility quality attributes are affected.
  Trust boundaries are not expected to change because no external services,
  network flows, arbitrary user-file reads, or persistent user data are in
  scope.
- **iSAQB Architecture Governance**: Runtime view, quality scenarios, and
  architecture-risk evidence may need targeted updates or explicit unchanged
  rationale. New ADRs are expected only if planning introduces shared UI or
  status abstractions with lasting architectural impact.
- **A11Y Governance**: User-facing terminal examples, guides, README, and
  possible generated documentation are affected. Text-first accessibility and
  keyboard-first operation are mandatory; WCAG 2.2 AA applies to generated HTML
  documentation when refreshed.
- **Cross-Platform Governance**: The feature does not add, change, or remove a
  script-shaped tool. Bash/PowerShell parity, man pages, PowerShell help, and
  `-WhatIf` parity are N/A unless planning adds a script.
- **Agent Parity Governance**: Shared agent guidance and Spec-Kit templates
  are not expected to change. If planning changes active feature context,
  technologies, project structure, or shared workflow rules, the maintained
  agent surfaces must be reviewed together:
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md`.

### Key Entities

- **Wave-2 Example**: One of the eleven scoped example applications, with a
  historical source reference, visible main component, status feedback,
  description path, guide, and smoke proof.
- **Visible Main Component**: The primary visible control, dialog, window,
  view group, scroll group, progress display, dynamic text view, combo/input
  composition, or stable visual runtime state used as parity proof.
- **Status Feedback**: A short text-first status-line or equivalent status
  area message that reports current selection, scroll position, progress,
  error, validation state, or next operation without replacing the main
  component. The primary runtime surface is a real `TStatusLine`; equivalent
  status areas require documented deviation evidence.
- **Description Path**: The canonical `Help -> Description` runtime path that
  explains the visible component, operation path, historical intent, and A11Y
  expectations. It is a consistently named, keyboard-reachable runtime path,
  not only a guide or evidence entry. `About` may provide supplemental context
  only.
- **Primary Smoke Proof**: A deterministic validation scenario that exercises
  the visible application path through the real app loop, verifies concrete
  control/dialog/focus/selection/scroll/progress state, and includes a stable
  rendered visibility proof built from both view-tree proof and a buffer/cell
  snapshot with control-specific content at the expected position or region.
- **Historical Source Review**: A per-example record of the relevant read-only
  C/C++ source and header files, the historical visual purpose, the C# target
  state, and any intentional user-visible deviation.
- **Evidence Surface**: The guide, README, project statistics, feature or PR
  evidence under the feature directory, security rationale, architecture
  rationale, supply-chain/AI-SBOM rationale, and A11Y rationale that make
  completion reviewable.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All eleven scoped examples show a visible main component or
  stable visual runtime state from normal startup.
- **SC-002**: All eleven examples provide the three-layer model: main
  component, `TStatusLine` feedback, and reachable description path. Any
  equivalent status-area fallback has documented deviation evidence.
- **SC-003**: All eleven primary smoke proofs verify a visible component or
  visible runtime state through both concrete state assertions and stable
  rendered visibility assertions that combine view-tree proof with buffer/cell
  snapshots at expected positions or regions. Zero primary smoke proofs rely
  only on `VisibleText`, `VisibleHistory`, or direct helper output.
- **SC-004**: The smoke coverage includes every required visual family:
  clipboard text/input state, the three required `Demo` flow families,
  dialog-description rendering or rejection, dynamic text, input/list/history,
  list selection or boundary, progress completion, scroll-dialog focus or
  offset, two-axis scroll state, combo/input selection, and progress
  abort/cancel state.
- **SC-005**: Every scoped example has historical-source review evidence that
  names the relevant source files, historical visual intent, target visible
  state, and intentional deviations.
- **SC-006**: All affected guides and `examples/README.md` describe startup,
  visible main component, operation path, expected status feedback, description
  path, and accessibility notes in German first and English second.
- **SC-007**: All eleven apps provide a consistently named,
  keyboard-reachable `Help -> Description` runtime description path, and a
  primary or supplemental smoke test verifies reachability and text content.
- **SC-008**: A reviewer can trace every example from historical source to
  visible runtime state to smoke proof to guide/evidence entry.
- **SC-009**: No Wave-3 or Wave-4 behavior is required, implemented, or counted
  toward completion.
- **SC-010**: Completion evidence records successful Release build, fast
  Example-Smoke suite, full Release test run, Coverlet coverage gate, and
  `dotnet format --verify-no-changes`.
- **SC-011**: Formatting, coverage, security, architecture, supply-chain,
  AI-SBOM, and A11Y evidence are either updated or explicitly recorded as
  unchanged or not applicable with rationale.
- **SC-012**: DocFX generation and the web-a11y smoke path are successful when
  guides, DocFX content, documentation navigation, or API documentation are
  affected.

## Assumptions

- The starting point is the merged 011 Wave-2 port plus the 012 interactive
  showcase baseline on `main`.
- The existing 012 app-loop command paths and text-first feedback remain
  valuable and can be reused, but they are supporting proof rather than final
  visual parity proof.
- Keyboard operation is the mandatory user path. Mouse-only operation is out of
  scope.
- The feature may add narrowly scoped shared support only when required to
  expose the visible Wave-2 composition and keep tests stable. Larger
  framework gaps are documented as intentional deviations and stay out of
  scope for 013.
- Existing generated DocFX output remains untracked; DocFX and web A11Y checks
  are required only when documentation output or navigation changes.
- Controlled fixtures and test temporary directories are acceptable proof data
  boundaries. Arbitrary user-file content, persistent user history, and
  external proof paths are not acceptable proof sources.
- No new external service, database, network dependency, or persistent user
  history is required.
