# Tasks: Controls Widgets and Collections

**Input**: Design documents from `/specs/009-controls-widgets-and-collections/`
**Prerequisites**: [plan.md](plan.md), [spec.md](spec.md), [research.md](research.md), [data-model.md](data-model.md), [contracts/widgets-collections-api.md](contracts/widgets-collections-api.md), [quickstart.md](quickstart.md)

**Tests**: Tests are mandatory for this feature. The constitution, plan, contract, and quickstart require visible Red-Green-Refactor sequencing with failing MSTest coverage first, followed by implementation, refactor, and repository-wide validation.

**Organization**: Tasks are grouped by user story so that list/input hardening, reusable combo/progress/parameter-text widgets, and the framework-first acceptance slice remain independently reviewable and incrementally deliverable.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete work)
- **[Story]**: Maps the task to one user story (`[US1]`, `[US2]`, `[US3]`)
- Every task includes an exact repository file path

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Prepare the shared Controls test scaffolding and helper surfaces used by all later stories.

- [ ] T001 Create the initial shared widget interaction scaffold in `tests/TuiVision.Controls.Tests/ControlsWidgetTestContext.cs`
- [ ] T002 [P] Extend reusable event and owner-buffer helpers for list/combo/history scenarios in `tests/TuiVision.Controls.Tests/ControlEventFactory.cs` and `tests/TuiVision.Controls.Tests/ControlTestContext.cs`

**Checkpoint**: The test suite has reusable scaffolding for the upcoming widget and collections work.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish the common acceptance-slice baseline and governance prerequisites that block all user stories.

**CRITICAL**: No user-story work should begin until this phase is complete.

- [ ] T003 [P] Add red acceptance-slice scaffolding for widgets and collections in `tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs`
- [ ] T004 [P] Align the numbered-branch version fields and next build/test counter handling in `Directory.Build.props`
- [ ] T005 Extend `tests/TuiVision.Controls.Tests/ControlsWidgetTestContext.cs` with test-side factories and reflection-based activation helpers for planned widget surfaces

**Checkpoint**: Shared acceptance/test scaffolding and numbered-branch governance are ready for story work.

---

## Phase 3: User Story 1 - Reuse Robust List and Input Building Blocks (Priority: P1) 🎯 MVP

**Goal**: Harden the reusable list, scroll, history, clipboard, and input contracts so later wave-2 examples can consume them without local workaround logic.

**Independent Test**: Run focused Controls tests for `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `THistory`, `TFileInputLine`, `TInputLine`, and `ManagedClipboard` without launching any example project and verify stable navigation, session recall, and clipboard behavior.

### Tests for User Story 1

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T006 [P] [US1] Add red list-navigation and undersized-bounds coverage in `tests/TuiVision.Controls.Tests/TListViewerTests.cs`, `tests/TuiVision.Controls.Tests/TListBoxTests.cs`, `tests/TuiVision.Controls.Tests/TScrollBarTests.cs`, and `tests/TuiVision.Controls.Tests/TScrollerTests.cs`
- [ ] T007 [P] [US1] Add red session-history and file-input recall coverage in `tests/TuiVision.Controls.Tests/THistoryTests.cs` and `tests/TuiVision.Controls.Tests/TFileInputLineTests.cs`
- [ ] T008 [P] [US1] Add red managed-clipboard and input-line interaction coverage in `tests/TuiVision.Controls.Tests/TManagedClipboardTests.cs` and `tests/TuiVision.Controls.Tests/TInputLineTests.cs`

### Implementation for User Story 1

- [ ] T009 [P] [US1] Harden list focus, visible-range, and scroll synchronization in `src/TuiVision.Controls/TListViewer.cs`, `src/TuiVision.Controls/TListBox.cs`, `src/TuiVision.Controls/TScrollBar.cs`, and `src/TuiVision.Controls/TScroller.cs`
- [ ] T010 [P] [US1] Implement session-scoped recall semantics and file-input integration with bilingual XML docs in `src/TuiVision.Controls/THistory.cs`, `src/TuiVision.Controls/TFileInputLine.cs`, and `src/TuiVision.Controls/TStringList.cs`
- [ ] T011 [US1] Implement application-internal clipboard semantics and input-line integration with bilingual XML docs in `src/TuiVision.Controls/ManagedClipboard.cs` and `src/TuiVision.Controls/TInputLine.cs`

**Checkpoint**: User Story 1 is independently functional and provides the MVP hardening baseline for later widget consumers.

---

## Phase 4: User Story 2 - Compose Wave-2-Specific Widgets from Shared Contracts (Priority: P2)

**Goal**: Deliver reusable combo-box, progress, and parameterized-text controls on top of the shared list/input/history contracts.

**Independent Test**: Instantiate and exercise `TComboBox`, `TProgressBar`, and `TParamText` in focused Controls tests and verify editable selection, determinate progress updates, and bounded text refresh/clipping without any example project.

### Tests for User Story 2

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T012 [P] [US2] Add red editable combo-box interaction and first-redraw drop-down visibility coverage in `tests/TuiVision.Controls.Tests/TComboBoxTests.cs`
- [ ] T013 [P] [US2] Add red determinate progress state, range, and first-redraw visibility coverage in `tests/TuiVision.Controls.Tests/TProgressBarTests.cs`
- [ ] T014 [P] [US2] Add red parameterized-text refresh, clipping, and first-redraw visibility coverage in `tests/TuiVision.Controls.Tests/TParamTextTests.cs`

### Implementation for User Story 2

- [ ] T015 [US2] Implement the editable combo-box surface with bilingual XML docs in `src/TuiVision.Controls/TComboBox.cs`
- [ ] T016 [P] [US2] Implement the determinate numeric progress surface with bilingual XML docs in `src/TuiVision.Controls/TProgressBar.cs`
- [ ] T017 [P] [US2] Implement the bounded parameterized-text view with bilingual XML docs in `src/TuiVision.Controls/TParamText.cs`
- [ ] T018 [US2] Integrate combo-box history and list-backed choice consumption in `src/TuiVision.Controls/TComboBox.cs`, `src/TuiVision.Controls/TStringList.cs`, and `src/TuiVision.Controls/THistory.cs`

**Checkpoint**: User Story 2 is independently functional and keeps wave-2 widget behavior inside the shared framework surface.

---

## Phase 5: User Story 3 - Keep Example Validation Framework-First (Priority: P3)

**Goal**: Prove the widget and collections feature through a dedicated Controls-level acceptance slice before any wave-2 example-smoke expansion.

**Independent Test**: Run a dedicated framework-only validation slice that covers list/history/clipboard, combo, progress, and parameterized-text behavior, and confirm the proof remains inside `tests/TuiVision.Controls.Tests` plus the repository validation workflow.

### Tests for User Story 3

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T019 [P] [US3] Add red framework-acceptance scenarios for event-loop dispatch, focus transitions, menu execution, dialog interaction, and first-redraw widget visibility in `tests/TuiVision.Controls.Tests/WidgetAcceptanceScenarioTests.cs`
- [ ] T020 [P] [US3] Add red traceability-matrix and thin-consumer guard assertions for `clipboard`, `dyntxt`, `inplis`, `listvi`, `progba`, `tcombo`, and `tprogb` in `tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs`

### Implementation for User Story 3

- [ ] T021 [US3] Implement the framework-first acceptance scenarios in `tests/TuiVision.Controls.Tests/WidgetAcceptanceScenarioTests.cs`
- [ ] T022 [US3] Refresh the cross-widget sweep, explicit later-consumer traceability guidance for `clipboard`, `dyntxt`, `inplis`, `listvi`, `progba`, `tcombo`, and `tprogb`, and the validation flow in `tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs` and `specs/009-controls-widgets-and-collections/quickstart.md`
- [ ] T023 [US3] Record the required Multi-Mac, Linux, and Windows/WSL acceptance-evidence workflow in `docs/guides/multi-mac-workflow.md`

**Checkpoint**: User Story 3 leaves a reviewable, framework-first acceptance package without pulling example-smoke delivery into this branch.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Run the required quality gates, finish governance follow-through, and refresh repository statistics.

- [ ] T024 [P] After aligning `Directory.Build.props` to the current numbered-branch version/build-counter state, run and record `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Controls.Tests/`, and `dotnet test` using `specs/009-controls-widgets-and-collections/quickstart.md`
- [ ] T025 [P] On the same `Directory.Build.props`-aligned state, run and record `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`, `dotnet format --verify-no-changes`, and conditional `docfx docfx.json` using `specs/009-controls-widgets-and-collections/quickstart.md`
- [ ] T026 Record repeated-run Multi-Mac, Linux, and Windows/WSL evidence in `docs/guides/multi-mac-workflow.md` and `specs/009-controls-widgets-and-collections/quickstart.md`
- [ ] T027 [P] Rename `Lastenheft_01_ControlsWidgetsAndCollections.md` to `Lastenheft_01_ControlsWidgetsAndCollections_009-controls-widgets-and-collections.md`
- [ ] T028 [P] Update `docs/project-statistics.md` with the branch/phase scope, code/test/doc line counts, observable work window, and manual-effort baseline for `009-controls-widgets-and-collections`
- [ ] T029 [P] Sync `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/agents/copilot-instructions.md` if the active `009` execution guidance changes during implementation

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all user-story work.
- **User Story 1 (Phase 3)**: Depends on Foundational; serves as the MVP hardening baseline.
- **User Story 2 (Phase 4)**: Depends on Foundational and reuses the list/input/history groundwork stabilized in US1.
- **User Story 3 (Phase 5)**: Depends on US1 and US2 because the acceptance slice must cover the full widget surface.
- **Polish (Phase 6)**: Depends on all desired user stories being complete.

### User Story Dependencies

- **US1 (P1)**: Can start after Foundational; no dependency on later stories.
- **US2 (P2)**: Builds on the shared list/input/history contracts completed by US1.
- **US3 (P3)**: Starts after US1 and US2 because it packages the full framework-first acceptance slice.

### Within Each User Story

- Tests MUST be written and fail before the corresponding implementation tasks begin.
- Shared helper and contract-seam work comes before behavior-heavy control implementation.
- Bilingual XML documentation is part of every production-code task that changes or adds public APIs.
- Do not treat example-porting work as part of this feature; later examples remain downstream consumers only.

### Parallel Opportunities

- In Setup, `T002` can run in parallel once the shared fixture direction from `T001` is clear.
- In Foundational, `T003` and `T004` can run in parallel before `T005` extends the shared test-side helpers.
- In US1, `T006`, `T007`, and `T008` can run in parallel; `T009` and `T010` can then proceed in parallel before `T011` closes the input/clipboard integration.
- In US2, `T012`, `T013`, and `T014` can run in parallel; `T016` and `T017` can proceed in parallel once `T015` fixes the combo-box direction.
- In US3, `T019` and `T020` can run in parallel; `T022` and `T023` can proceed in parallel after `T021` stabilizes the acceptance slice.
- In Polish, `T024`, `T025`, `T027`, `T028`, and `T029` can run in parallel after implementation is complete; `T026` depends on actual repeated-run evidence.

---

## Parallel Example: User Story 1

```bash
Task: "Add red list-navigation and undersized-bounds coverage in tests/TuiVision.Controls.Tests/TListViewerTests.cs, tests/TuiVision.Controls.Tests/TListBoxTests.cs, tests/TuiVision.Controls.Tests/TScrollBarTests.cs, and tests/TuiVision.Controls.Tests/TScrollerTests.cs"
Task: "Add red session-history and file-input recall coverage in tests/TuiVision.Controls.Tests/THistoryTests.cs and tests/TuiVision.Controls.Tests/TFileInputLineTests.cs"
Task: "Add red managed-clipboard and input-line interaction coverage in tests/TuiVision.Controls.Tests/TManagedClipboardTests.cs and tests/TuiVision.Controls.Tests/TInputLineTests.cs"
```

## Parallel Example: User Story 2

```bash
Task: "Add red editable combo-box interaction and first-redraw drop-down visibility coverage in tests/TuiVision.Controls.Tests/TComboBoxTests.cs"
Task: "Add red determinate progress state, range, and first-redraw visibility coverage in tests/TuiVision.Controls.Tests/TProgressBarTests.cs"
Task: "Add red parameterized-text refresh, clipping, and first-redraw visibility coverage in tests/TuiVision.Controls.Tests/TParamTextTests.cs"
```

## Parallel Example: User Story 3

```bash
Task: "Add red framework-acceptance scenarios for event-loop dispatch, focus transitions, menu execution, dialog interaction, and first-redraw widget visibility in tests/TuiVision.Controls.Tests/WidgetAcceptanceScenarioTests.cs"
Task: "Add red traceability-matrix and thin-consumer guard assertions for clipboard, dyntxt, inplis, listvi, progba, tcombo, and tprogb in tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational.
3. Complete Phase 3: User Story 1.
4. **STOP and VALIDATE**: Run the US1 Controls tests independently.
5. Review the hardened list/input/history/clipboard baseline before starting the new widget surfaces.

### Incremental Delivery

1. Finish Setup + Foundational once.
2. Add US1 and validate the reusable list/input baseline.
3. Add US2 and validate reusable combo/progress/parameter-text widgets without regressing US1.
4. Add US3 and validate the dedicated framework-first acceptance slice.
5. Finish with Phase 6 quality gates, traceability rename, and repository statistics.

### Parallel Team Strategy

1. One developer completes Setup and Foundational.
2. After Phase 2:
   - Developer A: User Story 1
   - Developer B: User Story 2 once the shared test-side helpers are stable
   - Developer C: User Story 3 after US1 and US2 stabilize
3. Finish with shared validation, statistics, and governance follow-through.

---

## Notes

- All tasks follow the required checklist format with IDs, optional `[P]` markers, story labels where required, and exact file paths.
- The primary acceptance surface for this feature remains `tests/TuiVision.Controls.Tests`; no example-smoke expansion belongs in this branch.
- The original Turbo Vision sources under `tv203s/` remain reference-only and must not be modified.
