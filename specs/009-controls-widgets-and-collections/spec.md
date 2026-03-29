# Feature Specification: Controls Widgets and Collections

**Feature Branch**: `009-controls-widgets-and-collections`  
**Created**: 2026-03-29  
**Status**: Draft  
**Input**: User description: "Erstelle eine Spezifikation aus dem Inhalt der Datei Lastenheft_01_ControlsWidgetsAndCollections.md"

## User Scenarios & Testing *(mandatory)*

This feature does not port one of the 25 mandatory original examples by itself.
Instead, it hardens reusable framework widgets and collections that the next
mandatory example wave depends on. The mandatory examples that consume this
feature later are `clipboard`, `dyntxt`, `inplis`, `listvi`, `progba`,
`tcombo`, and `tprogb` from `tv203s/contrib/tvision/examples`.

### User Story 1 - Reuse Robust List and Input Building Blocks (Priority: P1)

As a TuiVision framework consumer, I want list, scrolling, input, history, and
clipboard-aware widget contracts to behave consistently so that wave-2 examples
can compose them without local workaround logic.

**Why this priority**: List and input behaviour is the most shared foundation
across the affected wave-2 examples. If these contracts remain thin or
inconsistent, later example ports will fork the same behaviour in multiple
places and hide the real framework gaps.

**Independent Test**: Create focused control-level tests that exercise
`TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TInputLine`, `THistory`,
`TFileInputLine`, and `ManagedClipboard` without launching any example project,
and verify that the controls preserve visible state and predictable keyboard
behaviour under normal and edge conditions.

**Acceptance Scenarios**:

1. **Given** a scrollable list control with more items than visible rows,
   **When** a reviewer drives it with keyboard navigation alone, **Then** the
   focused item, visible range, and linked scroll state stay synchronized.
2. **Given** an input-oriented control flow that uses history or clipboard
   interaction, **When** the user triggers those interactions through the
   supported widget contract, **Then** the framework resolves the action without
   requiring example-local helper logic.
3. **Given** an empty list or very small bounds, **When** the control is drawn
   and navigated, **Then** it remains stable, keeps its bounds, and does not
   leak stale selection state.

---

### User Story 2 - Compose Wave-2-Specific Widgets from Shared Contracts (Priority: P2)

As a TuiVision framework consumer, I want combo-box, progress-display, and
dynamic-text capabilities to exist as reusable framework widgets so that the
affected wave-2 examples stay application-thin instead of re-implementing UI
primitives.

**Why this priority**: These widgets are less foundational than list and input
contracts, but they are the point where wave-2 examples would otherwise start
adding ad-hoc infrastructure that looks like framework code but lives in
example folders.

**Independent Test**: Prove that a reusable combo widget, a reusable progress
display contract, and a reusable parameterized text display can be instantiated,
updated, rendered, and verified in focused framework tests without any example
project.

**Acceptance Scenarios**:

1. **Given** a combo-style input flow, **When** the user opens the selection
   surface and chooses an item, **Then** the input value, list presentation, and
   history or drop-down state remain consistent.
2. **Given** a long-running operation represented by a progress widget,
   **When** the reported state changes from running to completed or canceled,
   **Then** the widget updates its visible state without requiring custom
   per-example rendering logic.
3. **Given** a parameterized text view with runtime values, **When** the values
   change or the available bounds shrink, **Then** the rendered output refreshes
   predictably and stays clipped to its declared area.

---

### User Story 3 - Keep Example Validation Framework-First (Priority: P3)

As a reviewer of later example waves, I want widget and collection acceptance to
be proven at framework level first so that example smoke tests only need to show
correct composition instead of compensating for missing core behaviour.

**Why this priority**: Without a framework-first acceptance surface, example
smoke tests turn into mixed infrastructure tests and stop being reliable proof
for the example applications themselves.

**Independent Test**: Run a dedicated widget-focused validation slice that stays
green before any of the affected wave-2 examples are ported, then demonstrate
that later example smoke tests can stay comparatively thin.

**Acceptance Scenarios**:

1. **Given** the widget and collections feature is implemented, **When** the
   framework test suite for lists, combo/history behaviour, dynamic text, and
   progress paths is executed, **Then** it passes without depending on example
   projects.
2. **Given** an affected wave-2 example is started later, **When** it needs
   combo, progress, clipboard, or list logic, **Then** it consumes an existing
   framework type instead of introducing a second competing implementation.

### Edge Cases

- What happens when a list-driven control has no items, only one item, or bounds
  too small to show its normal focus frame?
- How does the system handle clipboard or history actions when no current value
  exists or when the requested action produces no new content?
- What happens when a combo-style widget contains more choices than fit in its
  visible drop-down area?
- How does a progress widget behave when the reported value stalls, completes
  immediately, or is canceled before reaching a nominal end state?
- What happens when a parameterized text view receives values that exceed its
  render width or update more often than its host view is redrawn?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The Controls layer MUST provide list and scrolling behaviour that
  remains stable for visible focus, selection, range changes, and linked scroll
  position under both normal and edge conditions.
- **FR-002**: List-oriented controls MUST preserve stable behaviour for empty
  collections, single-item collections, and bounds smaller than a normal
  viewport.
- **FR-003**: Input-oriented controls MUST expose one coherent contract for
  typed input, history recall, and clipboard-oriented interactions so that
  affected wave-2 examples do not invent local variants of those flows.
- **FR-004**: The framework MUST define the expected interaction boundaries
  between `TInputLine`, `THistory`, `TFileInputLine`, and `ManagedClipboard`.
- **FR-005**: The Controls layer MUST introduce a reusable combo-box style
  widget model that combines editable or selectable input state with a list
  presentation contract.
- **FR-006**: Combo-style widget behaviour MUST support a visible selection
  surface, a consistent selected-value outcome, and predictable closure of the
  temporary selection state.
- **FR-007**: The Controls layer MUST provide a reusable progress-display
  contract that supports at least running, completed, and canceled states.
- **FR-008**: Progress-display behaviour MUST be testable without binding it to
  one specific example application's long-running task logic.
- **FR-009**: Dynamic parameterized text display MUST support runtime value
  refresh and clipping within declared bounds.
- **FR-010**: The widget and collections feature MUST establish a dedicated
  framework-level acceptance surface for list, combo/history, progress, and
  parameter-text behaviour before the affected wave-2 examples are ported.
- **FR-011**: Later example smoke tests for `clipboard`, `dyntxt`, `inplis`,
  `listvi`, `progba`, `tcombo`, and `tprogb` MUST be able to rely on the shared
  framework implementations delivered by this feature.
- **FR-012**: This feature MUST remain limited to reusable widget and collection
  behaviour and MUST NOT absorb menu, status-line, window, dialog-validation,
  editor, help, or terminal-emulation scope.
- **FR-013**: This feature MUST NOT deliver the mandatory example ports
  themselves; it only prepares the framework surface that those examples later
  consume.

### Key Entities *(include if feature involves data)*

- **List Navigation State**: The active item, visible range, and linked scroll
  position that together describe the current state of a list-driven control.
- **Input Interaction Contract**: The framework-level agreement for text entry,
  history recall, and clipboard-oriented actions across input-related controls.
- **Combo Presentation State**: The currently visible combo selection surface,
  its available choices, and the resulting selected or edited value.
- **Progress State**: The reported operational state of a progress display,
  including running, completed, and canceled outcomes.
- **Parameterized Text State**: A display contract that combines a text pattern,
  current runtime values, and the effective bounds available for rendering.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: A dedicated framework validation slice can verify list,
  combo/history, progress, and parameter-text behaviour without launching any
  of the affected example projects.
- **SC-002**: All affected wave-2 examples can be planned as consumers of shared
  framework widgets rather than as owners of their own combo, progress,
  clipboard, or list implementations.
- **SC-003**: Reviewers can trace every widget family in scope to at least one
  focused framework test and at least one later consuming example.
- **SC-004**: No affected wave-2 example needs to add a second competing
  implementation for combo-box behaviour, progress rendering, clipboard-aware
  input, or list-navigation logic.
