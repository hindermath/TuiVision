# Tasks: Dialog-/Control-Schicht (Dialog and Control Layer)

**Input**: Design documents from `/specs/003-dialog-control-layer/`
**Prerequisites**: [plan.md](plan.md), [spec.md](spec.md), [research.md](research.md), [data-model.md](data-model.md), [contracts/public-api.md](contracts/public-api.md), [quickstart.md](quickstart.md)

**Tests**: Tests are mandatory for this feature. Every control class needs MSTest coverage with at least one positive and one negative/boundary case, and all implementation must follow visible Red-Green-Refactor sequencing.

**Organization**: Story labels preserve traceability to the spec, but the execution order of phases follows the technical class dependency sequence from [plan.md](plan.md) so that `TDialog` remains the final coordinator step.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete tasks)
- **[Story]**: Maps the task to one user story (`[US1]` ... `[US6]`)
- Every task includes an exact repository file path

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Prepare the existing controls test project for the new control-layer work.

- [X] T001 Remove the placeholder MSTest file in `tests/TuiVision.Controls.Tests/Test1.cs`
- [X] T002 Create shared control test context helpers for buffer inspection and owner wiring in `tests/TuiVision.Controls.Tests/ControlTestContext.cs`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish shared command IDs and reusable test utilities before any control porting starts.

**⚠️ CRITICAL**: No implementation work should begin until this phase is complete.

- [X] T003 Extend shared dialog command identifiers (`cmOK`, `cmCancel`, `cmYes`, `cmNo`) in `src/TuiVision.Controls/ShellCommandIds.cs`
- [X] T004 [P] Add reusable keyboard, mouse, and command event builders in `tests/TuiVision.Controls.Tests/ControlEventFactory.cs`
- [X] T005 [P] Add reusable console-buffer assertion helpers in `tests/TuiVision.Controls.Tests/ControlBufferAssert.cs`

**Checkpoint**: Shared command IDs and control-test helpers are ready.

---

## Phase 3: Technical Base Controls

**Purpose**: Implement the plan's earliest technical base classes before dependent controls.

- [X] T006 [P] [US3] Add red tests for indexed collection behavior and out-of-range access in `tests/TuiVision.Controls.Tests/TStringListTests.cs`
- [X] T007 [P] [US3] Add red tests for clamp behavior, page/arrow steps, and scrollbar bounds in `tests/TuiVision.Controls.Tests/TScrollBarTests.cs`
- [X] T008 [P] [US6] Add red tests for non-focusable multi-line text rendering in `tests/TuiVision.Controls.Tests/TStaticTextTests.cs`
- [X] T009 [P] [US3] Implement `TStringList` with index validation and bilingual XML docs in `src/TuiVision.Controls/TStringList.cs`
- [X] T010 [P] [US3] Implement `TScrollBar` parameter handling, clamping, owner event forwarding, and bilingual XML docs in `src/TuiVision.Controls/TScrollBar.cs`
- [X] T011 [US3] Add red tests for coordinated delta updates and scrollbar synchronization in `tests/TuiVision.Controls.Tests/TScrollerTests.cs`
- [X] T012 [US3] Implement abstract `TScroller` coordinated scroll logic and bilingual XML docs in `src/TuiVision.Controls/TScroller.cs`
- [X] T013 [US6] Implement `TStaticText` rendering, non-focusable behavior, and bilingual XML docs in `src/TuiVision.Controls/TStaticText.cs`

**Checkpoint**: Base classes from plan steps 1-4 are available for dependent controls.

---

## Phase 4: Selection and Label Controls

**Purpose**: Implement grouped selection controls and linked labels from the next dependency layer.

- [X] T014 [P] [US5] Add red tests for abstract cluster navigation, selection tracking, and disabled behavior in `tests/TuiVision.Controls.Tests/TClusterTests.cs`
- [X] T015 [P] [US5] Add red tests for checkbox bitmask toggling in `tests/TuiVision.Controls.Tests/TCheckBoxesTests.cs`
- [X] T016 [P] [US5] Add red tests for radio-button exclusivity, single-option rendering, and reselection rules in `tests/TuiVision.Controls.Tests/TRadioButtonsTests.cs`
- [X] T017 [US5] Implement abstract `TCluster` drawing, navigation, state handling, and bilingual XML docs in `src/TuiVision.Controls/TCluster.cs`
- [X] T018 [US5] Implement `TCheckBoxes` bitmask semantics and XML docs in `src/TuiVision.Controls/TCheckBoxes.cs`
- [X] T019 [US5] Implement `TRadioButtons` single-selection semantics and XML docs in `src/TuiVision.Controls/TRadioButtons.cs`
- [X] T020 [US6] Add red tests for label hotkey parsing, light-state rendering, and linked-focus transfer in `tests/TuiVision.Controls.Tests/TLabelTests.cs`
- [X] T021 [US6] Implement `TLabel` hotkey handling, peer-focus transfer, visual highlighting, and bilingual XML docs in `src/TuiVision.Controls/TLabel.cs`

**Checkpoint**: Selection groups and labels are ready for later dialog composition.

---

## Phase 5: List Controls

**Purpose**: Complete the list stack on top of the earlier base controls.

- [X] T022 [P] [US3] Add red tests for `TopItem`/`FocusedItem` coordination and optional scrollbar usage in `tests/TuiVision.Controls.Tests/TListViewerTests.cs`
- [X] T023 [P] [US3] Add red tests for empty-list rendering, click selection, keyboard navigation, and double-click confirmation without a separate command in `tests/TuiVision.Controls.Tests/TListBoxTests.cs`
- [X] T024 [US3] Implement abstract `TListViewer` viewport/focus synchronization, optional scrollbar integration, and bilingual XML docs in `src/TuiVision.Controls/TListViewer.cs`
- [X] T025 [US3] Implement `TListBox` data binding, empty rendering, click selection, and double-click confirmation-without-command semantics with bilingual XML docs in `src/TuiVision.Controls/TListBox.cs`

**Checkpoint**: The list-selection workflow is independently testable and aligned with the clarified double-click semantics.

---

## Phase 6: Input Controls

**Purpose**: Implement the direct user-input controls that the dialog coordinator depends on.

- [X] T026 [US4] Add red tests for baseline button activation and command dispatch in `tests/TuiVision.Controls.Tests/TButtonTests.cs`
- [X] T027 [US4] Add red tests for Alt-hotkeys, disabled-state focus skipping, and default-button behavior in `tests/TuiVision.Controls.Tests/TButtonTests.cs`
- [X] T028 [US4] Implement `TButton`/`TButtonFlags` activation, hotkey parsing, default-state rendering, disabled behavior, and bilingual XML docs in `src/TuiVision.Controls/TButton.cs`
- [X] T029 [US2] Add red tests for text entry, cursor movement, insert/overwrite mode, delete/backspace, horizontal scrolling, and `MaxLen = 0` in `tests/TuiVision.Controls.Tests/TInputLineTests.cs`
- [X] T030 [US2] Implement `TInputLine` editing behavior, viewport scrolling, boundary handling, and bilingual XML docs in `src/TuiVision.Controls/TInputLine.cs`

**Checkpoint**: Buttons and input lines are ready for the final dialog coordinator step.

---

## Phase 7: Dialog Coordinator

**Purpose**: Implement `TDialog` last, as required by the plan, after all dependent controls exist.

- [X] T031 [US1] Add red tests for modal run loop, frame/title rendering, focus wrap-around, Escape → `cmCancel`, behavior without a focusable child control, and ignoring mouse events outside an open modal dialog in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T032 [US1] Add red integration tests for cancel-button return values, default-button activation, and child-control event consumption rules in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T033 [P] [US1] Add a red composite dialog integration scenario covering `TInputLine`, `TListBox`, `TButton`, `TCheckBoxes`, and `TRadioButtons` in `tests/TuiVision.Controls.Tests/TDialogCompositeTests.cs`
- [X] T034 [US1] Implement `TDialog` modal coordination, frame/title drawing, command routing, focus delegation to `TGroup`, and boundary handling with bilingual XML docs in `src/TuiVision.Controls/TDialog.cs`

**Checkpoint**: The dialog layer is complete and the feature's highest-priority user workflow is testable end-to-end.

---

## Phase 8: Polish & Mandatory Quality Gates

**Purpose**: Close cross-cutting coverage gaps and satisfy the Constitution's merge gates.

- [X] T035 [P] Add cross-cutting negative/render coverage for visual states and remaining edge cases in `tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs`
- [X] T036 Update the final control-layer verification checklist and command filters in `specs/003-dialog-control-layer/quickstart.md`
- [X] T037 Run and record `dotnet build --configuration Release` plus `dotnet test` using the checklist in `specs/003-dialog-control-layer/quickstart.md`
- [X] T038 Run and record the `TuiVision.Controls` coverage gate validation using the checklist in `specs/003-dialog-control-layer/quickstart.md`
- [X] T039 Run and record `dotnet format --verify-no-changes` plus `docfx docfx.json` validation against `docfx.json`
- [X] T040 Run and record the example smoke-test project validation against `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all later work.
- **Technical Base Controls (Phase 3)**: Depends on Foundational.
- **Selection and Label Controls (Phase 4)**: Depends on Phase 3.
- **List Controls (Phase 5)**: Depends on Phase 3 and the shared helpers from Phase 2.
- **Input Controls (Phase 6)**: Depends on Phase 5 for plan-aligned sequencing.
- **Dialog Coordinator (Phase 7)**: Depends on all prior technical phases.
- **Polish & Mandatory Quality Gates (Phase 8)**: Depends on the completion of all implementation phases.

### Story Dependencies

- **US3 (P3)**: Starts earliest because its technical base classes (`TStringList`, `TScrollBar`, `TScroller`) are plan prerequisites for later list behavior.
- **US5 (P5)**: Follows the plan's cluster hierarchy before dialog composition.
- **US6 (P6)**: `TStaticText` starts early; `TLabel` follows after the selection controls layer.
- **US4 (P4)**: Starts only after the list stack is complete so `TButton` stays in plan step 11.
- **US2 (P2)**: Starts after `TButton` so `TInputLine` remains in plan step 12.
- **US1 (P1)**: Finishes last because `TDialog` is the final coordinator in plan step 13, even though it remains the highest-priority end-user workflow.

### Within Each Phase

- Tests must be written and fail before the corresponding implementation tasks start.
- Every implementation task includes bilingual XML documentation updates for the touched production file.
- Boundary cases from the spec and plan must be covered explicitly: empty `TListBox`, `TInputLine` with `MaxLen = 0`, `TDialog` without a focusable child control, `TScrollBar` at bounds, `TRadioButtons` with a single option, and mouse input outside a modal dialog.
- No task may introduce `Newtonsoft.Json`; if feature-owned JSON becomes necessary, it must use `System.Text.Json`.

### Parallel Opportunities

- `T004` and `T005` can run in parallel after `T003`.
- In Phase 3, `T006`, `T007`, and `T008` can run in parallel; `T009` and `T010` can then run in parallel.
- In Phase 4, `T014`, `T015`, and `T016` can run in parallel.
- In Phase 5, `T022` and `T023` can run in parallel.
- In Phase 7, `T033` can run in parallel with `T031`/`T032` because it uses a separate test file.
- In Phase 8, `T035`, `T039`, and `T040` can run in parallel after `T036`.

---

## Parallel Example: Phase 3

```bash
Task: "Add red tests for indexed collection behavior and out-of-range access in tests/TuiVision.Controls.Tests/TStringListTests.cs"
Task: "Add red tests for clamp behavior, page/arrow steps, and scrollbar bounds in tests/TuiVision.Controls.Tests/TScrollBarTests.cs"
Task: "Add red tests for non-focusable multi-line text rendering in tests/TuiVision.Controls.Tests/TStaticTextTests.cs"
```

## Parallel Example: Phase 4

```bash
Task: "Add red tests for abstract cluster navigation, selection tracking, and disabled behavior in tests/TuiVision.Controls.Tests/TClusterTests.cs"
Task: "Add red tests for checkbox bitmask toggling in tests/TuiVision.Controls.Tests/TCheckBoxesTests.cs"
Task: "Add red tests for radio-button exclusivity, single-option rendering, and reselection rules in tests/TuiVision.Controls.Tests/TRadioButtonsTests.cs"
```

## Parallel Example: Phase 8

```bash
Task: "Add cross-cutting negative/render coverage for visual states and remaining edge cases in tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs"
Task: "Run and record dotnet format --verify-no-changes plus docfx docfx.json validation against docfx.json"
Task: "Run and record the example smoke-test project validation against tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj"
```

---

## Implementation Strategy

### Plan-Aligned Delivery

1. Complete Setup and Foundational once.
2. Follow the technical class sequence from [plan.md](plan.md): base classes → selection/labels → lists → input controls → dialog coordinator.
3. Use the story labels to verify that each spec story remains covered even though execution order follows class dependencies.

### End-User Validation Order

1. Validate the list workflow after Phase 5.
2. Validate button and input behavior after Phase 6.
3. Validate the full dialog workflow after Phase 7.
4. Close mandatory build, coverage, formatting, docfx, and smoke-test gates in Phase 8.

### Team Strategy

1. One developer completes Phase 1 and Phase 2.
2. After that:
   - Developer A: Phase 3 base classes
   - Developer B: Phase 4 selection controls
   - Developer C: Phase 5 list controls
3. Merge into Phase 6 and Phase 7 sequentially to preserve the plan's dependency order.

---

## Notes

- All tasks follow the required checklist format.
- The original Turbo Vision files under `tv203s/` remain reference-only and must not be modified.
- The Constitution's mandatory quality gates are represented explicitly in Phase 8.
