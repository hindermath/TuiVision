# Tasks: Dialog-/Control-Schicht (Dialog and Control Layer)

**Input**: Design documents from `/specs/003-dialog-control-layer/`
**Prerequisites**: [plan.md](plan.md) (required), [spec.md](spec.md) (required), [research.md](research.md), [data-model.md](data-model.md), [contracts/public-api.md](contracts/public-api.md), [quickstart.md](quickstart.md)

**Tests**: Tests are mandatory for this feature. Every control class needs MSTest coverage with at least one positive and one negative/boundary case, and the implementation must follow visible Red-Green-Refactor sequencing.

**Organization**: Tasks are grouped by user story so each increment stays independently testable. Shared setup and blocking prerequisites are separated into Phase 1 and Phase 2.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete tasks)
- **[Story]**: Maps the task to one user story (`[US1]` ... `[US6]`)
- Every task includes an exact repository file path

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Prepare the existing controls test project for the new control-layer work.

- [ ] T001 Remove the placeholder MSTest file in `tests/TuiVision.Controls.Tests/Test1.cs`
- [ ] T002 Create shared control test context helpers for buffer inspection and owner wiring in `tests/TuiVision.Controls.Tests/ControlTestContext.cs`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish shared command IDs and reusable test utilities before user-story work starts.

**⚠️ CRITICAL**: No user story work should begin until this phase is complete.

- [ ] T003 Extend shared dialog command identifiers (`cmOK`, `cmCancel`, `cmYes`, `cmNo`) in `src/TuiVision.Controls/ShellCommandIds.cs`
- [ ] T004 [P] Add reusable keyboard/mouse/command event builders in `tests/TuiVision.Controls.Tests/ControlEventFactory.cs`
- [ ] T005 [P] Add reusable console-buffer assertion helpers in `tests/TuiVision.Controls.Tests/ControlBufferAssert.cs`

**Checkpoint**: Shared command IDs and common control-test helpers are ready.

---

## Phase 3: User Story 1 - Interaktiver Dialog mit Steuerelementen (Priority: P1) 🎯 MVP

**Goal**: Deliver a modal `TDialog` that can host focusable controls, render frame/title, cycle focus with wrap-around, and close via button command or Escape.

**Independent Test**: Instantiate a dialog with two buttons, verify initial focus on the first button, wrap-around with Tab/Shift-Tab, Enter on the focused cancel button returns its command, and Escape closes with `cmCancel`.

### Tests for User Story 1

> **NOTE**: Write these tests first and verify that they fail before implementing the production code.

- [ ] T006 [P] [US1] Add red tests for baseline button activation and command dispatch in `tests/TuiVision.Controls.Tests/TButtonTests.cs`
- [ ] T007 [P] [US1] Add red tests for modal run loop, frame/title rendering, focus wrap-around, and Escape → `cmCancel` in `tests/TuiVision.Controls.Tests/TDialogTests.cs`

### Implementation for User Story 1

- [ ] T008 [US1] Implement baseline `TButton`/`TButtonFlags` activation, rendering, and bilingual XML docs in `src/TuiVision.Controls/TButton.cs`
- [ ] T009 [US1] Implement `TDialog` modal run loop, frame/title drawing, focus delegation to `TGroup`, and default Escape → `cmCancel` semantics with bilingual XML docs in `src/TuiVision.Controls/TDialog.cs`

**Checkpoint**: User Story 1 is functional and independently testable as the MVP dialog workflow.

---

## Phase 4: User Story 2 - Einzeilige Texteingabe (Priority: P2)

**Goal**: Deliver `TInputLine` with single-line editing, cursor movement, insert/overwrite mode, and bounded input length.

**Independent Test**: Place one `TInputLine` in a dialog, type and delete text, move the cursor with Home/End/arrow keys, and verify `MaxLen` and `MaxLen = 0` boundary handling.

### Tests for User Story 2

- [ ] T010 [US2] Add red tests for text entry, cursor movement, insert/overwrite mode, delete/backspace, horizontal scrolling, and `MaxLen = 0` in `tests/TuiVision.Controls.Tests/TInputLineTests.cs`

### Implementation for User Story 2

- [ ] T011 [US2] Implement `TInputLine` editing behavior, viewport scrolling, boundary handling, and bilingual XML docs in `src/TuiVision.Controls/TInputLine.cs`

**Checkpoint**: User Story 2 is functional and independently testable with one dialog-hosted text field.

---

## Phase 5: User Story 3 - Scrollbare Listenauswahl (Priority: P3)

**Goal**: Deliver the list-selection stack with `TStringList`, `TScrollBar`, `TScroller`, `TListViewer`, and `TListBox`, including empty-list handling and double-click confirmation without an extra command event.

**Independent Test**: Show a `TListBox` with 20 items and a vertical `TScrollBar`, move through the list with keyboard input, observe synchronized scrollbar updates, verify empty-list rendering, and confirm that a double-click only confirms selection.

### Tests for User Story 3

- [ ] T012 [P] [US3] Add red tests for indexed collection behavior and out-of-range access in `tests/TuiVision.Controls.Tests/TStringListTests.cs`
- [ ] T013 [P] [US3] Add red tests for clamp behavior, page/arrow steps, and bounds handling in `tests/TuiVision.Controls.Tests/TScrollBarTests.cs`
- [ ] T014 [P] [US3] Add red tests for coordinated delta updates and scrollbar synchronization in `tests/TuiVision.Controls.Tests/TScrollerTests.cs`
- [ ] T015 [P] [US3] Add red tests for `TopItem`/`FocusedItem` coordination and optional scrollbar usage in `tests/TuiVision.Controls.Tests/TListViewerTests.cs`
- [ ] T016 [P] [US3] Add red tests for empty-list rendering, click selection, keyboard navigation, and double-click confirmation without a separate command in `tests/TuiVision.Controls.Tests/TListBoxTests.cs`

### Implementation for User Story 3

- [ ] T017 [P] [US3] Implement `TStringList` with index validation and bilingual XML docs in `src/TuiVision.Controls/TStringList.cs`
- [ ] T018 [P] [US3] Implement `TScrollBar` parameter handling, clamping, owner event forwarding, and bilingual XML docs in `src/TuiVision.Controls/TScrollBar.cs`
- [ ] T019 [US3] Implement abstract `TScroller` coordinated scroll logic and bilingual XML docs in `src/TuiVision.Controls/TScroller.cs`
- [ ] T020 [US3] Implement abstract `TListViewer` viewport/focus synchronization, optional scrollbar integration, and bilingual XML docs in `src/TuiVision.Controls/TListViewer.cs`
- [ ] T021 [US3] Implement `TListBox` data binding, empty rendering, click selection, and double-click confirmation-without-command semantics with bilingual XML docs in `src/TuiVision.Controls/TListBox.cs`

**Checkpoint**: User Story 3 is functional and independently testable as a complete list-selection workflow.

---

## Phase 6: User Story 4 - Schaltflächen (Priority: P4)

**Goal**: Complete advanced button behavior with hotkeys, disabled-state focus handling, and default-button activation through dialog Enter routing.

**Independent Test**: Use a dialog with a default OK button, a cancel button, and an input field; verify Alt-hotkey activation, disabled button skip in focus traversal, and Enter activating the default button when the focused control does not consume Enter.

### Tests for User Story 4

- [ ] T022 [US4] Add red tests for Alt-hotkeys, disabled-state focus skipping, and default-button behavior in `tests/TuiVision.Controls.Tests/TButtonTests.cs`
- [ ] T023 [US4] Add red dialog-integration tests for default-button activation when a focused control does not consume Enter in `tests/TuiVision.Controls.Tests/TDialogTests.cs`

### Implementation for User Story 4

- [ ] T024 [US4] Extend `TButton` hotkey parsing, default-state rendering, disabled behavior, and XML docs in `src/TuiVision.Controls/TButton.cs`
- [ ] T025 [US4] Extend `TDialog` Enter routing for `bfDefault` while preserving child-control event consumption rules in `src/TuiVision.Controls/TDialog.cs`

**Checkpoint**: User Story 4 is functional and independently testable as the complete button interaction model.

---

## Phase 7: User Story 5 - Auswahlgruppen: Checkboxen und Radiobuttons (Priority: P5)

**Goal**: Deliver grouped selection controls based on `TCluster` for multi-select and mutually exclusive single-select interaction.

**Independent Test**: Place one `TCheckBoxes` or `TRadioButtons` control in a dialog, navigate with arrow keys, toggle/select items with space, and verify disabled-state behavior.

### Tests for User Story 5

- [ ] T026 [P] [US5] Add red tests for abstract cluster navigation, selection tracking, and disabled behavior in `tests/TuiVision.Controls.Tests/TClusterTests.cs`
- [ ] T027 [P] [US5] Add red tests for checkbox bitmask toggling in `tests/TuiVision.Controls.Tests/TCheckBoxesTests.cs`
- [ ] T028 [P] [US5] Add red tests for radio-button exclusivity and reselection rules in `tests/TuiVision.Controls.Tests/TRadioButtonsTests.cs`

### Implementation for User Story 5

- [ ] T029 [US5] Implement abstract `TCluster` drawing, navigation, state handling, and bilingual XML docs in `src/TuiVision.Controls/TCluster.cs`
- [ ] T030 [US5] Implement `TCheckBoxes` bitmask semantics and XML docs in `src/TuiVision.Controls/TCheckBoxes.cs`
- [ ] T031 [US5] Implement `TRadioButtons` single-selection semantics and XML docs in `src/TuiVision.Controls/TRadioButtons.cs`

**Checkpoint**: User Story 5 is functional and independently testable for grouped option selection.

---

## Phase 8: User Story 6 - Statische Anzeige: Beschriftungen und Texte (Priority: P6)

**Goal**: Deliver non-interactive text display and label-to-peer focus transfer for structured dialogs.

**Independent Test**: Render static text in a dialog without focus participation and verify that a `TLabel` hotkey moves focus to its linked peer control.

### Tests for User Story 6

- [ ] T032 [P] [US6] Add red tests for non-focusable multi-line text rendering in `tests/TuiVision.Controls.Tests/TStaticTextTests.cs`
- [ ] T033 [P] [US6] Add red tests for label hotkey parsing, light-state rendering, and linked-focus transfer in `tests/TuiVision.Controls.Tests/TLabelTests.cs`

### Implementation for User Story 6

- [ ] T034 [US6] Implement `TStaticText` rendering, non-focusable behavior, and bilingual XML docs in `src/TuiVision.Controls/TStaticText.cs`
- [ ] T035 [US6] Implement `TLabel` hotkey handling, peer-focus transfer, visual highlighting, and bilingual XML docs in `src/TuiVision.Controls/TLabel.cs`

**Checkpoint**: User Story 6 is functional and independently testable for static dialog text and labels.

---

## Phase 9: Polish & Cross-Cutting Concerns

**Purpose**: Finish coverage, integration proof, and documentation/verification across all stories.

- [ ] T036 [P] Add a composite dialog integration scenario covering `TInputLine`, `TListBox`, `TButton`, `TCheckBoxes`, and `TRadioButtons` in `tests/TuiVision.Controls.Tests/TDialogCompositeTests.cs`
- [ ] T037 [P] Add cross-cutting negative/render coverage to close the ≥70% gate in `tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs`
- [ ] T038 Resolve any remaining XML documentation and docfx-facing API gaps across `src/TuiVision.Controls/*.cs`
- [ ] T039 Update final verification commands and feature-specific test filters in `specs/003-dialog-control-layer/quickstart.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all user stories.
- **User Stories (Phase 3+)**: Depend on Foundational completion.
- **Polish (Phase 9)**: Depends on the completion of all desired user stories.

### User Story Dependencies

- **US1 (P1)**: Starts after Foundational; no dependency on other user stories.
- **US2 (P2)**: Starts after Foundational; no dependency on other user stories.
- **US3 (P3)**: Starts after Foundational; no dependency on other user stories.
- **US4 (P4)**: Depends on US1 because it extends the baseline `TButton`/`TDialog` behavior delivered for the MVP dialog workflow.
- **US5 (P5)**: Starts after Foundational; no dependency on other user stories.
- **US6 (P6)**: Starts after Foundational; no dependency on other user stories.

### Within Each User Story

- Test tasks must be written and fail before the corresponding implementation tasks start.
- Base abstractions and data models come before dependent concrete controls.
- Rendering, event handling, and bilingual XML documentation are part of the production-code task, not deferred clean-up.
- Boundary cases from the plan must be covered explicitly: empty `TListBox`, `TInputLine` with `MaxLen = 0`, `TScrollBar` clamp/bounds, `TDialog` without a focusable child control, and mouse input outside a modal dialog.

### Parallel Opportunities

- `T004` and `T005` can run in parallel after `T003`.
- In US1, `T006` and `T007` can run in parallel.
- In US3, `T012`–`T016` can run in parallel, followed by `T017` and `T018` in parallel.
- In US5, `T026`–`T028` can run in parallel.
- In US6, `T032` and `T033` can run in parallel.
- In Phase 9, `T036` and `T037` can run in parallel.

---

## Parallel Example: User Story 1

```bash
Task: "Add red tests for baseline button activation and command dispatch in tests/TuiVision.Controls.Tests/TButtonTests.cs"
Task: "Add red tests for modal run loop, frame/title rendering, focus wrap-around, and Escape → cmCancel in tests/TuiVision.Controls.Tests/TDialogTests.cs"
```

## Parallel Example: User Story 3

```bash
Task: "Add red tests for indexed collection behavior and out-of-range access in tests/TuiVision.Controls.Tests/TStringListTests.cs"
Task: "Add red tests for clamp behavior, page/arrow steps, and bounds handling in tests/TuiVision.Controls.Tests/TScrollBarTests.cs"
Task: "Add red tests for coordinated delta updates and scrollbar synchronization in tests/TuiVision.Controls.Tests/TScrollerTests.cs"
Task: "Add red tests for TopItem/FocusedItem coordination and optional scrollbar usage in tests/TuiVision.Controls.Tests/TListViewerTests.cs"
Task: "Add red tests for empty-list rendering, click selection, keyboard navigation, and double-click confirmation without a separate command in tests/TuiVision.Controls.Tests/TListBoxTests.cs"
```

## Parallel Example: User Story 5

```bash
Task: "Add red tests for abstract cluster navigation, selection tracking, and disabled behavior in tests/TuiVision.Controls.Tests/TClusterTests.cs"
Task: "Add red tests for checkbox bitmask toggling in tests/TuiVision.Controls.Tests/TCheckBoxesTests.cs"
Task: "Add red tests for radio-button exclusivity and reselection rules in tests/TuiVision.Controls.Tests/TRadioButtonsTests.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational.
3. Complete Phase 3: User Story 1.
4. Validate the dialog MVP independently before continuing.

### Incremental Delivery

1. Finish Setup + Foundational once.
2. Deliver US1 as the first runnable dialog increment.
3. Add US2 and US3 for text and list interaction.
4. Add US4, US5, and US6 as focused capability increments.
5. Finish with Phase 9 coverage/documentation validation.

### Parallel Team Strategy

1. One developer closes Phase 1 and Phase 2.
2. After Foundation:
   - Developer A: US1 / later US4
   - Developer B: US2 / US6
   - Developer C: US3
   - Developer D: US5
3. Integrate all stories, then run Phase 9 together.

---

## Notes

- All tasks follow the required checklist format.
- Tests are intentionally first-class because the spec and constitution require MSTest and visible TDD sequencing.
- `Newtonsoft.Json` must not be introduced while implementing this feature; if any helper/test JSON becomes necessary, it must use `System.Text.Json`.
- `TDirListBox`, `TFileDialog`, and other file/editor-layer controls remain out of scope for this feature.
