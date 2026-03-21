# Tasks: Editor, File, Help, and Stream Components

**Input**: Design documents from `/specs/004-editor-file-help-streams/`
**Prerequisites**: [plan.md](plan.md), [spec.md](spec.md), [research.md](research.md), [data-model.md](data-model.md), [contracts/public-api.md](contracts/public-api.md), [quickstart.md](quickstart.md)

**Tests**: Tests are mandatory for this feature. The constitution, plan, and quickstart require visible Red-Green-Refactor sequencing with MSTest coverage for `TuiVision.Controls` and `TuiVision.Serialization`.

**Organization**: Tasks are grouped by user story so each story remains independently testable, while Setup and Foundational phases prepare the shared infrastructure required by multiple stories.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete tasks)
- **[Story]**: Maps the task to one user story (`[US1]` ... `[US4]`)
- Every task includes an exact repository file path

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create the missing test project and shared fixtures required by the feature plan.

- [ ] T001 Create the serialization test project in `tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj`
- [ ] T002 Register `tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj` in `TuiVision.sln`
- [ ] T003 [P] Add shared serialization fixtures, streamable test doubles, and registry helpers in `tests/TuiVision.Serialization.Tests/SerializationTestSupport.cs`
- [ ] T004 [P] Add shared shell-hosting, temp-file, and dialog test utilities in `tests/TuiVision.Controls.Tests/EditorShellTestContext.cs`

**Checkpoint**: Both test projects and their shared test-support fixtures are ready.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish shared commands, host infrastructure, and serialization foundations before any story work begins.

**⚠️ CRITICAL**: No user story work should begin until this phase is complete.

- [ ] T005 [P] Add red regression tests for archive/registry foundation behavior in `tests/TuiVision.Serialization.Tests/TRecordCompatibilityTests.cs`
- [ ] T006 [P] Add red shell-host lifecycle tests for non-modal framed views in `tests/TuiVision.Controls.Tests/FramedHostViewTests.cs`
- [ ] T007 Refactor the serialization scaffold into `src/TuiVision.Serialization/ITStreamSerializable.cs`, `src/TuiVision.Serialization/TBinaryArchiveReader.cs`, `src/TuiVision.Serialization/TBinaryArchiveWriter.cs`, `src/TuiVision.Serialization/TRecordRegistry.cs`, `src/TuiVision.Serialization/TRecordSerializer.cs`, and trim `src/TuiVision.Serialization/Class1.cs`
- [ ] T008 Implement the shared non-modal framed host base in `src/TuiVision.Controls/Internal/FramedHostView.cs`
- [ ] T009 Extend editor/file/help command identifiers in `src/TuiVision.Controls/ShellCommandIds.cs`

**Checkpoint**: Shared serialization and shell-host infrastructure is in place for all stories.

---

## Phase 3: User Story 1 - Edit and manage document content (Priority: P1) 🎯 MVP

**Goal**: Deliver a reusable in-shell editor surface with document mutation, search/replace, indicators, and safe-close behavior.

**Independent Test**: Open an editor host with an in-memory document, type and delete text, move across lines, run find/replace, and confirm that closing a modified document requires an explicit decision without destabilizing the shell.

### Tests for User Story 1 ⚠️

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T010 [P] [US1] Add red buffer, cursor, selection, and scrolling tests in `tests/TuiVision.Controls.Tests/TEditorTests.cs`
- [ ] T011 [P] [US1] Add red search/replace, undo, and safe-close command tests in `tests/TuiVision.Controls.Tests/TEditorCommandTests.cs`
- [ ] T012 [P] [US1] Add red host-window and indicator integration tests in `tests/TuiVision.Controls.Tests/TEditWindowTests.cs`

### Implementation for User Story 1

- [ ] T013 [P] [US1] Implement the reusable multi-line editor core with bilingual XML docs in `src/TuiVision.Controls/TEditor.cs`
- [ ] T014 [P] [US1] Implement the memory-backed editor wrapper with bilingual XML docs in `src/TuiVision.Controls/TMemo.cs`
- [ ] T015 [P] [US1] Implement the editor indicator view with bilingual XML docs in `src/TuiVision.Controls/TIndicator.cs`
- [ ] T016 [US1] Implement the editor host window and safe-close coordination with bilingual XML docs in `src/TuiVision.Controls/TEditWindow.cs`

**Checkpoint**: User Story 1 is independently functional and serves as the MVP for the increment.

---

## Phase 4: User Story 2 - Open, choose, and save files through reusable dialogs (Priority: P2)

**Goal**: Add file-backed editing, reusable file dialogs, and history-scoped file selection on the real local file system.

**Independent Test**: Open a file dialog, browse directories with a wildcard filter, recall a prior path from the same history bucket, load a file into the editor, save it without changing line endings, and confirm that overwrite conflicts require an explicit decision.

### Tests for User Story 2 ⚠️

- [ ] T017 [P] [US2] Add red file-backed editor tests for load/save, newline preservation, and external-change conflicts in `tests/TuiVision.Controls.Tests/TFileEditorTests.cs`
- [ ] T018 [P] [US2] Add red file-dialog synchronization and history-scoping tests in `tests/TuiVision.Controls.Tests/TFileDialogTests.cs`
- [ ] T019 [P] [US2] Add red directory navigation and empty-filter-result tests in `tests/TuiVision.Controls.Tests/TDirListBoxTests.cs`
- [ ] T020 [P] [US2] Add red filtered file-list and manual-path-entry tests in `tests/TuiVision.Controls.Tests/TFileListTests.cs`

### Implementation for User Story 2

- [ ] T021 [P] [US2] Implement history bucket recall with bilingual XML docs in `src/TuiVision.Controls/THistory.cs`
- [ ] T022 [P] [US2] Implement file-path input behavior with bilingual XML docs in `src/TuiVision.Controls/TFileInputLine.cs`
- [ ] T023 [P] [US2] Implement filtered file-list behavior with bilingual XML docs in `src/TuiVision.Controls/TFileList.cs`
- [ ] T024 [P] [US2] Implement directory-list navigation with bilingual XML docs in `src/TuiVision.Controls/TDirListBox.cs`
- [ ] T025 [US2] Implement real-file load/save, line-ending preservation, and snapshot-based overwrite conflict handling with bilingual XML docs in `src/TuiVision.Controls/TFileEditor.cs`
- [ ] T026 [US2] Implement reusable file-dialog orchestration with bilingual XML docs in `src/TuiVision.Controls/TFileDialog.cs`

**Checkpoint**: User Story 2 is independently functional and supports real file-system workflows without breaking US1.

---

## Phase 5: User Story 3 - Read context-sensitive help and follow linked topics (Priority: P3)

**Goal**: Deliver dedicated help-file persistence plus runtime help viewing, fallback handling, and linked-topic navigation inside the shell.

**Independent Test**: Load a dedicated help file, open help for a valid context, navigate cross-references in the same help workflow, and verify that a missing context yields fallback content instead of a broken screen.

### Tests for User Story 3 ⚠️

- [ ] T027 [P] [US3] Add red help-file model tests for context lookup, cross-reference resolution, and fallback behavior in `tests/TuiVision.Serialization.Tests/THelpFileTests.cs`
- [ ] T028 [P] [US3] Add red help-view navigation and cross-reference activation tests in `tests/TuiVision.Controls.Tests/THelpViewerTests.cs`
- [ ] T029 [P] [US3] Add red help-window host integration tests in `tests/TuiVision.Controls.Tests/THelpWindowTests.cs`

### Implementation for User Story 3

- [ ] T030 [P] [US3] Implement the dedicated help persistence model with bilingual XML docs in `src/TuiVision.Serialization/THelpTopic.cs`, `src/TuiVision.Serialization/THelpIndex.cs`, and `src/TuiVision.Serialization/THelpFile.cs`
- [ ] T031 [US3] Implement the scrollable help viewer with cross-reference navigation and bilingual XML docs in `src/TuiVision.Controls/THelpViewer.cs`
- [ ] T032 [US3] Implement the help host window with context-open flow and bilingual XML docs in `src/TuiVision.Controls/THelpWindow.cs`

**Checkpoint**: User Story 3 is independently functional and reuses the shell and persistence infrastructure without breaking US1 or US2.

---

## Phase 6: User Story 4 - Persist and reload named resources and streamable objects (Priority: P4)

**Goal**: Deliver compatibility stream primitives and named resource persistence with explicit malformed-input rejection and shared-reference preservation.

**Independent Test**: Persist a registered object graph with shared references into a named resource container, reload it in a fresh read step, and verify exact-key lookup, replacement, removal, and explicit failure for truncated, trailing, unknown-type, and cyclic payloads.

### Tests for User Story 4 ⚠️

- [ ] T033 [P] [US4] Add red compatibility-stream tests for shared references, seek/tell, and malformed input rejection in `tests/TuiVision.Serialization.Tests/PStreamTests.cs`
- [ ] T034 [P] [US4] Add red resource-container tests for exact-key lookup, replacement, removal, and enumeration in `tests/TuiVision.Serialization.Tests/TResourceFileTests.cs`
- [ ] T035 [P] [US4] Add red end-to-end registration and reload regression tests in `tests/TuiVision.Serialization.Tests/TRecordCompatibilityTests.cs`

### Implementation for User Story 4

- [ ] T036 [US4] Implement `pstream`, `ipstream`, `opstream`, and `fpstream` with bilingual XML docs in `src/TuiVision.Serialization/pstream.cs`, `src/TuiVision.Serialization/ipstream.cs`, `src/TuiVision.Serialization/opstream.cs`, and `src/TuiVision.Serialization/fpstream.cs`
- [ ] T037 [US4] Implement named resource persistence with bilingual XML docs in `src/TuiVision.Serialization/TResourceFile.cs` and `src/TuiVision.Serialization/TResourceCollection.cs`
- [ ] T038 [US4] Integrate help-file and resource persistence through the compatibility stream registrations in `src/TuiVision.Serialization/THelpFile.cs` and `src/TuiVision.Serialization/TResourceFile.cs`

**Checkpoint**: User Story 4 is independently functional and completes the persistence backbone for the increment.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Close coverage gaps, run the mandatory quality gates, and refresh repository statistics.

- [ ] T039 [P] Add cross-cutting editor/file/help edge-case coverage in `tests/TuiVision.Controls.Tests/EditorCoverageSweepTests.cs`
- [ ] T040 [P] Add cross-cutting serialization negative-case coverage in `tests/TuiVision.Serialization.Tests/SerializationCoverageSweepTests.cs`
- [ ] T041 Run and record `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Controls.Tests/`, `dotnet test tests/TuiVision.Serialization.Tests/`, and `dotnet test` using `specs/004-editor-file-help-streams/quickstart.md`
- [ ] T042 Run and record `dotnet test --collect:"XPlat Code Coverage"`, `dotnet format --verify-no-changes`, and `docfx docfx.json` using `specs/004-editor-file-help-streams/quickstart.md`
- [ ] T043 [P] Update `docs/project-statistics.md` with the final branch/phase scope, code/test/doc line counts, observable work window, and manual-effort baseline

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all user-story work.
- **User Story 1 (Phase 3)**: Depends on Foundational; serves as the MVP.
- **User Story 2 (Phase 4)**: Depends on Foundational and reuses the editor baseline from US1.
- **User Story 3 (Phase 5)**: Depends on Foundational and the shared host/persistence groundwork from Phase 2.
- **User Story 4 (Phase 6)**: Depends on Foundational and completes the stream/resource backbone used by later validation.
- **Polish (Phase 7)**: Depends on all desired user stories being complete.

### User Story Dependencies

- **US1 (P1)**: Can start immediately after Foundational; no dependency on other stories.
- **US2 (P2)**: Builds on the editor baseline from US1 for the file-backed workflow.
- **US3 (P3)**: Builds on the shared non-modal host and serialization groundwork from Phase 2, but remains independently testable from US4.
- **US4 (P4)**: Builds on the shared serialization groundwork from Phase 2 and remains independently testable from the UI stories.

### Within Each User Story

- Tests MUST be written and fail before the corresponding implementation tasks start.
- Shared helpers and model files come before orchestration files in the same story.
- Bilingual XML documentation is part of every production-code implementation task.
- Do not merge a story phase until its independent test criteria from `spec.md` can be exercised without relying on unfinished later stories.

### Parallel Opportunities

- In Setup, `T003` and `T004` can run in parallel after `T001` and `T002`.
- In Foundational, `T005` and `T006` can run in parallel before `T007` and `T008`; `T009` is independent once command requirements are clear.
- In US1, `T010`, `T011`, and `T012` can run in parallel; `T013`, `T014`, and `T015` can then run in parallel.
- In US2, `T017`-`T020` can run in parallel; `T021`-`T024` can then run in parallel.
- In US3, `T027`, `T028`, and `T029` can run in parallel; `T030` can proceed in parallel with the UI implementation once the tests are in place.
- In US4, `T033`, `T034`, and `T035` can run in parallel before implementation starts.
- In Polish, `T039`, `T040`, and `T043` can run in parallel after the user stories are complete.

---

## Parallel Example: User Story 1

```bash
Task: "Add red buffer, cursor, selection, and scrolling tests in tests/TuiVision.Controls.Tests/TEditorTests.cs"
Task: "Add red search/replace, undo, and safe-close command tests in tests/TuiVision.Controls.Tests/TEditorCommandTests.cs"
Task: "Add red host-window and indicator integration tests in tests/TuiVision.Controls.Tests/TEditWindowTests.cs"
```

## Parallel Example: User Story 2

```bash
Task: "Implement history bucket recall with bilingual XML docs in src/TuiVision.Controls/THistory.cs"
Task: "Implement file-path input behavior with bilingual XML docs in src/TuiVision.Controls/TFileInputLine.cs"
Task: "Implement filtered file-list behavior with bilingual XML docs in src/TuiVision.Controls/TFileList.cs"
Task: "Implement directory-list navigation with bilingual XML docs in src/TuiVision.Controls/TDirListBox.cs"
```

## Parallel Example: User Story 3

```bash
Task: "Add red help-file model tests for context lookup, cross-reference resolution, and fallback behavior in tests/TuiVision.Serialization.Tests/THelpFileTests.cs"
Task: "Add red help-view navigation and cross-reference activation tests in tests/TuiVision.Controls.Tests/THelpViewerTests.cs"
Task: "Add red help-window host integration tests in tests/TuiVision.Controls.Tests/THelpWindowTests.cs"
```

## Parallel Example: User Story 4

```bash
Task: "Add red compatibility-stream tests for shared references, seek/tell, and malformed input rejection in tests/TuiVision.Serialization.Tests/PStreamTests.cs"
Task: "Add red resource-container tests for exact-key lookup, replacement, removal, and enumeration in tests/TuiVision.Serialization.Tests/TResourceFileTests.cs"
Task: "Add red end-to-end registration and reload regression tests in tests/TuiVision.Serialization.Tests/TRecordCompatibilityTests.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational.
3. Complete Phase 3: User Story 1.
4. **STOP and VALIDATE**: Exercise the US1 independent test from `spec.md`.
5. Demo or review the reusable editor baseline before adding file, help, or persistence work.

### Incremental Delivery

1. Finish Setup + Foundational once.
2. Add US1 and validate the in-memory editor workflow.
3. Add US2 and validate real file-system workflows without regressing US1.
4. Add US3 and validate dedicated help-file runtime navigation.
5. Add US4 and validate stream/resource persistence plus malformed-input rejection.
6. Finish with Phase 7 quality gates and repository statistics.

### Parallel Team Strategy

1. One developer completes Setup and Foundational.
2. After Phase 2:
   - Developer A: User Story 1
   - Developer B: User Story 2
   - Developer C: User Story 3
3. User Story 4 starts once serialization capacity is available and merges after the shared groundwork is stable.

---

## Notes

- All tasks follow the required checklist format with IDs, optional `[P]` markers, story labels where required, and exact file paths.
- The original Turbo Vision sources under `tv203s/` remain reference-only and must not be modified.
- TDD is not optional for this feature: tests must fail before implementation tasks start.
- `tasks.md` is intended to be directly executable by an LLM or contributor without additional planning context.
