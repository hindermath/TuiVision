# Tasks: Standard Dialogs and Designer Readiness

**Input**: Design documents from `/specs/010-standard-dialogs-designer/`
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, `contracts/standard-dialogs-designer-api.md`, `quickstart.md`, `checklists/plan-quality.md`

**Tests**: Test tasks are included because `spec.md` requires green framework-level Controls and Serialization acceptance evidence before wave-2 examples are treated as ported.

**Organization**: Tasks are grouped by user story so each story can be implemented and validated independently after the foundational tasks.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel because it touches different files and does not depend on incomplete tasks in the same phase.
- **[Story]**: User-story task label: `[US1]`, `[US2]`, `[US3]`.
- Every task includes an exact repository path.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Prepare the feature branch, shared test support, and validation evidence locations before behavior work starts.

- [ ] T001 Verify the numbered-branch `Major.Minor.Patch.Build` version fields for the planned implementation-start commit in `Directory.Build.props`
- [ ] T002 [P] Add shared standard-dialog test helpers for temporary directories, keyboard events, and text-first validation assertions in `tests/TuiVision.Controls.Tests/StandardDialogTestSupport.cs`
- [ ] T003 [P] Add dialog-description serialization test helpers for malformed payloads and roundtrip fixtures in `tests/TuiVision.Serialization.Tests/DialogDescriptionTestSupport.cs`
- [ ] T004 [P] Create the initial 010 implementation evidence notes section in `docs/guides/multi-mac-workflow.md`
- [ ] T005 [P] Add a 010 implementation tracking entry scaffold to the bottom of `docs/project-statistics.md`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Shared contracts and small data types required by all user-story phases.

**Critical**: No user story work should begin until this phase is complete.

- [ ] T006 [P] Add `StandardDialogKind`, `StandardDialogInteractionState`, and `StandardDialogFlowState` with bilingual XML docs in `src/TuiVision.Controls/TStandardDialogFlowState.cs`
- [ ] T007 [P] Add `StandardDialogValidationMessage` and text-first validation severity support with bilingual XML docs in `src/TuiVision.Controls/TStandardDialogValidationMessage.cs`
- [ ] T008 [P] Add `FileSelectionTargetKind`, `FileDecisionKind`, and `TFileDecisionResult` with caller-decision semantics in `src/TuiVision.Controls/TFileDecisionResult.cs`
- [ ] T009 [P] Add `ColorDisplaySelectionKind`, `ColorDisplaySelectionState`, and bounded fallback state in `src/TuiVision.Controls/TColorDisplaySelectionState.cs`
- [ ] T010 [P] Add `DialogDescription`, `DialogControlDescription`, and `DialogCommandBinding` skeletons with validation-oriented constructors in `src/TuiVision.Controls/TDialogDescription.cs`
- [ ] T011 [P] Add `PersistedDialogRepresentation` skeleton and format-version constants in `src/TuiVision.Serialization/TPersistedDialogRepresentation.cs`
- [ ] T012 Update public API XML documentation coverage for new foundational types in `src/TuiVision.Controls/TStandardDialogFlowState.cs` and `src/TuiVision.Serialization/TPersistedDialogRepresentation.cs`

**Checkpoint**: Shared state/result/description types exist with bilingual public docs; user-story implementation can proceed.

---

## Phase 3: User Story 1 - Complete Standard Dialog Flows (Priority: P1) MVP

**Goal**: Provide reusable file and directory standard-dialog flows with synchronized directory, filter, selection, manual path, metadata, session history, validation, and returned decisions without file content I/O.

**Independent Test**: A reviewer can drive file and directory dialog scenarios through framework tests and observe synchronized state, explicit open/select/save-target decisions, session-only history, keyboard operation, and non-destructive validation without a wave-2 example.

### Tests for User Story 1

- [ ] T013 [P] [US1] Add failing tests for file open/select/save-target decisions and no file content I/O in `tests/TuiVision.Controls.Tests/TFileDialogTests.cs`
- [ ] T014 [P] [US1] Add failing tests for directory navigation and directory select decisions in `tests/TuiVision.Controls.Tests/TDirListBoxTests.cs`
- [ ] T015 [P] [US1] Add failing tests for invalid manual paths, stale entries, and empty filtered lists in `tests/TuiVision.Controls.Tests/TFileInputLineTests.cs`
- [ ] T016 [P] [US1] Add failing tests for missing or unreadable metadata fallback in `tests/TuiVision.Controls.Tests/TFileListTests.cs`
- [ ] T017 [P] [US1] Add failing tests for session-only history partitioning by history ID in `tests/TuiVision.Controls.Tests/THistoryTests.cs`
- [ ] T018 [P] [US1] Add failing keyboard-only acceptance tests for file and directory flows in `tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs`

### Implementation for User Story 1

- [ ] T019 [US1] Extend `TFileDialogMode` with explicit open, select, and save-target semantics in `src/TuiVision.Controls/TFileDialog.cs`
- [ ] T020 [US1] Integrate `TFileDecisionResult` into confirmation and cancellation outcomes in `src/TuiVision.Controls/TFileDialog.cs`
- [ ] T021 [US1] Synchronize selected file, selected directory, manual path, metadata snapshot, validation state, and decision value in `src/TuiVision.Controls/TFileDialog.cs`
- [ ] T022 [US1] Harden visible-entry refresh, wildcard filtering, stale-entry detection, and missing metadata fallback in `src/TuiVision.Controls/TFileList.cs`
- [ ] T023 [US1] Harden directory navigation and directory selection state handoff in `src/TuiVision.Controls/TDirListBox.cs`
- [ ] T024 [US1] Harden manual path normalization and non-destructive validation handoff in `src/TuiVision.Controls/TFileInputLine.cs`
- [ ] T025 [US1] Ensure `TFileInfo` exposes text-first fallback state for missing or unreadable metadata in `src/TuiVision.Controls/TFileInfo.cs`
- [ ] T026 [US1] Ensure standard-dialog history remains session-scoped and partitioned by history ID in `src/TuiVision.Controls/THistory.cs`
- [ ] T027 [US1] Wire keyboard-only completion paths for file and directory dialog actions in `src/TuiVision.Controls/TFileDialog.cs`
- [ ] T028 [US1] Add US1 downstream consumer classification proof for `demo`, `sdlg`, `sdlg2`, and `dlgdsn` in `tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs`
- [ ] T029 [US1] Run focused Controls tests for US1 using `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj`

**Checkpoint**: User Story 1 is independently testable and provides the MVP.

---

## Phase 4: User Story 2 - Reusable Color, Charset, and Display Selection (Priority: P2)

**Goal**: Provide one reusable color/display/symbolic-charset selection flow with synchronized selected value, preview/display value, bounded fallback, cancellation, confirmation, and keyboard operation.

**Independent Test**: A reviewer can validate color, monochrome/display, and symbolic charset selection through framework tests, including confirm/cancel behavior and no terminal rendering/font/emulation effects.

### Tests for User Story 2

- [ ] T030 [P] [US2] Add failing tests for color group/value and preview synchronization in `tests/TuiVision.Controls.Tests/TColorDialogTests.cs`
- [ ] T031 [P] [US2] Add failing tests for monochrome/display selection and bounded fallback in `tests/TuiVision.Controls.Tests/TColorDialogTests.cs`
- [ ] T032 [P] [US2] Add failing tests for symbolic charset returned values without terminal rendering effects in `tests/TuiVision.Controls.Tests/TColorDialogTests.cs`
- [ ] T033 [P] [US2] Add failing keyboard-only and cancel/confirm acceptance tests for color/display/charset flows in `tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs`

### Implementation for User Story 2

- [ ] T034 [US2] Extend `TColorDialog` to own committed, selected, preview, cancellation, and confirmation state in `src/TuiVision.Controls/TColorDialog.cs`
- [ ] T035 [US2] Integrate `ColorDisplaySelectionState` into color and display decisions in `src/TuiVision.Controls/TColorDialog.cs`
- [ ] T036 [US2] Add symbolic charset choice support as returned dialog data without terminal rendering effects in `src/TuiVision.Controls/TColorDialog.cs`
- [ ] T037 [US2] Ensure `TColorSelector`, `TMonoSelector`, `TColorGroup`, and `TColorDisplay` stay synchronized through the composed flow in `src/TuiVision.Controls/TColorSelector.cs`
- [ ] T038 [US2] Add bounded no-supported-option fallback handling in `src/TuiVision.Controls/TColorDisplaySelectionState.cs`
- [ ] T039 [US2] Add US2 downstream consumer classification proof for `sdlg` and `sdlg2` in `tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs`
- [ ] T040 [US2] Run focused Controls tests for US2 using `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj`

**Checkpoint**: User Story 2 is independently testable and does not require a ported `sdlg` or `sdlg2`.

---

## Phase 5: User Story 3 - Dialog Designer Description Boundary (Priority: P3)

**Goal**: Provide a compact validated dialog-description model, runtime-dialog creation boundary, and minimal persisted-description roundtrip with malformed-input rejection before runtime creation.

**Independent Test**: A reviewer can validate one accepted and one rejected dialog description, duplicate ID and duplicate command-binding rejection, one minimal persisted roundtrip, and at least two rejected persisted inputs without relying on a ported `dlgdsn`.

### Tests for User Story 3

- [ ] T041 [P] [US3] Add failing tests for accepted and rejected dialog descriptions in `tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs`
- [ ] T042 [P] [US3] Add failing tests for duplicate control IDs and duplicate command bindings in `tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs`
- [ ] T043 [P] [US3] Add failing tests for unknown control roles, missing labels, invalid navigation order, and unsupported initial values in `tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs`
- [ ] T044 [P] [US3] Add failing tests for minimal persisted-description roundtrip in `tests/TuiVision.Serialization.Tests/DialogDescriptionPersistenceTests.cs`
- [ ] T045 [P] [US3] Add failing tests for malformed, truncated, unsupported-version, unsupported-value, and semantically invalid persisted input in `tests/TuiVision.Serialization.Tests/DialogDescriptionPersistenceTests.cs`
- [ ] T046 [P] [US3] Add failing keyboard-only designer-flow acceptance tests in `tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs`

### Implementation for User Story 3

- [ ] T047 [US3] Implement dialog-description validation for IDs, labels, roles, navigation, command targets, keyboard triggers, and initial values in `src/TuiVision.Controls/TDialogDescription.cs`
- [ ] T048 [US3] Add `DialogDescriptionValidationResult` with text-first validation messages in `src/TuiVision.Controls/TDialogDescriptionValidationResult.cs`
- [ ] T049 [US3] Add `DialogDescriptionValidator` to reject invalid descriptions before runtime dialog creation in `src/TuiVision.Controls/TDialogDescriptionValidator.cs`
- [ ] T050 [US3] Add `DialogDescriptionFactory` to create runtime `TDialog` instances only after validation succeeds in `src/TuiVision.Controls/TDialogDescriptionFactory.cs`
- [ ] T051 [US3] Implement a Controls-independent `ITStreamSerializable` persisted dialog-description DTO in `src/TuiVision.Serialization/TDialogDescriptionRecord.cs`
- [ ] T052 [US3] Add `DialogDescriptionPersistenceAdapter` to map between Controls `DialogDescription` objects and Serialization DTOs without adding a Serialization-to-Controls reference in `src/TuiVision.Controls/TDialogDescriptionPersistenceAdapter.cs`
- [ ] T053 [US3] Register, roundtrip, and reject malformed/truncated/unsupported-version/semantic persisted DTO input through `TRecordRegistry` and `TRecordSerializer` in `src/TuiVision.Serialization/TDialogDescriptionRecord.cs`
- [ ] T054 [US3] Add US3 downstream consumer classification proof for `dlgdsn` in `tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs`
- [ ] T055 [US3] Run focused Controls and Serialization tests for US3 using `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and `tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj`

**Checkpoint**: All three user stories are independently testable through framework-level evidence.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Validate the complete feature, update proof surfaces, and prepare the Lastenheft traceability handoff.

- [ ] T056 [P] Update bilingual public XML documentation for all changed public APIs in `src/TuiVision.Controls/TFileDialog.cs`
- [ ] T057 [P] Update bilingual public XML documentation for persisted dialog-description APIs in `src/TuiVision.Serialization/TDialogDescriptionRecord.cs` and `src/TuiVision.Controls/TDialogDescriptionPersistenceAdapter.cs`
- [ ] T058 [P] Record the full 010 security standards applicability matrix from Constitution Principles XIV-XIX in `docs/security/security-checklist.md`, with NIST SSDF and CWE Top 25 mandatory; OWASP ASVS, Zero Trust, CAPEC, OWASP SAMM, OWASP Cheat Sheet Series / Proactive Controls, OpenSSF Scorecard, and CRA awareness marked applicable or N/A with justification; and SBOM/VEX/SLSA tied to release evidence
- [ ] T059 [P] Apply secure-coding review for file-path validation and persisted-input parsing in `docs/security/security-checklist.md`
- [ ] T060 [P] Update dependency and supply-chain evidence for no new dependencies plus release-time SBOM/VEX obligations in `docs/security/dependency-audit.md` and `docs/security/supply-chain-evidence.md`
- [ ] T061 [P] Record text-first and keyboard-only A11Y proof notes for 010 in `docs/guides/multi-mac-workflow.md`
- [ ] T062 [P] Update final feature statistics and acceleration evidence in `docs/project-statistics.md`
- [ ] T063 Increment the manual build counter in `Directory.Build.props` before every `dotnet build` and every `dotnet test` validation command in T029, T040, T055, and T064-T068
- [ ] T064 Run release build validation with `dotnet build --configuration Release`
- [ ] T065 Run focused Controls validation with `dotnet test tests/TuiVision.Controls.Tests/`
- [ ] T066 Run focused Serialization validation with `dotnet test tests/TuiVision.Serialization.Tests/`
- [ ] T067 Run repository-wide test validation with `dotnet test`
- [ ] T068 Run coverage validation for the repo-wide 70 percent gate with `dotnet test --collect:"XPlat Code Coverage"`
- [ ] T069 Run formatting validation with `dotnet format --verify-no-changes`
- [ ] T070 [P] If public API XML comments changed, regenerate DocFX content with `docfx docfx.json`
- [ ] T071 [P] If DocFX was regenerated, run the DocFX web A11Y smoke checks with `cd tests/web-a11y && npm run test:docfx`
- [ ] T072 [P] Record Linux and Windows/WSL compatibility evidence when practical in `docs/guides/multi-mac-workflow.md`
- [ ] T073 [P] Review whether active feature context or shared agent guidance changed and update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` if affected
- [ ] T074 [P] Move the `>>> NAECHSTER SCHRITT <<<` marker if 010 completion changes the effective priority in `Pflichtenheft.md`
- [ ] T075 Rename `Lastenheft_02_StandardDialogsAndDesigner.md` to `Lastenheft_02_StandardDialogsAndDesigner.010-standard-dialogs-designer.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1 Setup**: No dependencies.
- **Phase 2 Foundational**: Depends on Phase 1 and blocks all user-story work.
- **Phase 3 US1**: Depends on Phase 2. This is the MVP and should be completed first for the safest incremental path.
- **Phase 4 US2**: Depends on Phase 2 and can run after or alongside US1 once shared state types exist.
- **Phase 5 US3**: Depends on Phase 2. It can run alongside US1/US2 after foundational model types exist, but persistence work should wait until `DialogDescription` validation shape is stable.
- **Phase 6 Polish**: Depends on the user stories selected for delivery.

### User Story Dependencies

- **US1 (P1)**: No dependency on US2 or US3 after Phase 2.
- **US2 (P2)**: No dependency on US1 behavior after Phase 2, but uses shared `StandardDialogFlowState` and `ColorDisplaySelectionState`.
- **US3 (P3)**: No dependency on US1/US2 behavior after Phase 2, but depends on shared validation-message conventions and the dialog-description model from Phase 2.

### Within Each User Story

- Write story-specific tests first and confirm they fail before implementation.
- Implement data/result types before dialog behavior that returns those types.
- Implement validation before runtime creation or persistence.
- Run focused project tests at the end of each story phase before moving to the next priority.

## Parallel Opportunities

- Setup tasks T002, T003, T004, and T005 touch different files and can run in parallel.
- Foundational tasks T006 through T011 create different type files and can run in parallel, followed by documentation pass T012.
- US1 tests T013 through T018 can be written in parallel before US1 implementation.
- US2 tests T030 through T033 can be written in parallel before US2 implementation.
- US3 tests T041 through T046 can be written in parallel before US3 implementation.
- US1, US2, and US3 implementation can run in parallel after Phase 2 if each worker owns the files named in that phase and coordinates shared public API changes.
- Polish documentation/evidence tasks T056 through T062 and conditional documentation/platform tasks T070 through T074 can run in parallel after the relevant code/test tasks are complete.

## Parallel Example: User Story 1

```text
Task: "T013 [P] [US1] Add failing tests for file open/select/save-target decisions and no file content I/O in tests/TuiVision.Controls.Tests/TFileDialogTests.cs"
Task: "T014 [P] [US1] Add failing tests for directory navigation and directory select decisions in tests/TuiVision.Controls.Tests/TDirListBoxTests.cs"
Task: "T015 [P] [US1] Add failing tests for invalid manual paths, stale entries, and empty filtered lists in tests/TuiVision.Controls.Tests/TFileInputLineTests.cs"
Task: "T016 [P] [US1] Add failing tests for missing or unreadable metadata fallback in tests/TuiVision.Controls.Tests/TFileListTests.cs"
Task: "T017 [P] [US1] Add failing tests for session-only history partitioning by history ID in tests/TuiVision.Controls.Tests/THistoryTests.cs"
Task: "T018 [P] [US1] Add failing keyboard-only acceptance tests for file and directory flows in tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs"
```

## Parallel Example: User Story 2

```text
Task: "T030 [P] [US2] Add failing tests for color group/value and preview synchronization in tests/TuiVision.Controls.Tests/TColorDialogTests.cs"
Task: "T031 [P] [US2] Add failing tests for monochrome/display selection and bounded fallback in tests/TuiVision.Controls.Tests/TColorDialogTests.cs"
Task: "T032 [P] [US2] Add failing tests for symbolic charset returned values without terminal rendering effects in tests/TuiVision.Controls.Tests/TColorDialogTests.cs"
Task: "T033 [P] [US2] Add failing keyboard-only and cancel/confirm acceptance tests for color/display/charset flows in tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs"
```

## Parallel Example: User Story 3

```text
Task: "T041 [P] [US3] Add failing tests for accepted and rejected dialog descriptions in tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs"
Task: "T042 [P] [US3] Add failing tests for duplicate control IDs and duplicate command bindings in tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs"
Task: "T043 [P] [US3] Add failing tests for unknown control roles, missing labels, invalid navigation order, and unsupported initial values in tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs"
Task: "T044 [P] [US3] Add failing tests for minimal persisted-description roundtrip in tests/TuiVision.Serialization.Tests/DialogDescriptionPersistenceTests.cs"
Task: "T045 [P] [US3] Add failing tests for malformed, truncated, unsupported-version, unsupported-value, and semantically invalid persisted input in tests/TuiVision.Serialization.Tests/DialogDescriptionPersistenceTests.cs"
Task: "T046 [P] [US3] Add failing keyboard-only designer-flow acceptance tests in tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs"
```

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 Setup.
2. Complete Phase 2 Foundational.
3. Complete Phase 3 User Story 1.
4. Stop and validate with `dotnet test tests/TuiVision.Controls.Tests/`.
5. Use the US1 proof to confirm reusable file/directory dialog readiness before taking US2/US3.

### Incremental Delivery

1. Deliver US1 for the standard file/directory dialog MVP.
2. Add US2 for color/display/symbolic-charset readiness.
3. Add US3 for `dlgdsn` dialog-description and persistence readiness.
4. Run Phase 6 validation and evidence tasks for the selected completed stories.

### Parallel Team Strategy

1. One worker owns US1 file/directory paths: `TFileDialog.cs`, `TFileList.cs`, `TDirListBox.cs`, `TFileInputLine.cs`, `TFileInfo.cs`, `THistory.cs`, and matching Controls tests.
2. One worker owns US2 color/display paths: `TColorDialog.cs`, `TColorSelector.cs`, `TMonoSelector.cs`, `TColorGroup.cs`, `TColorDisplay.cs`, `TColorDisplaySelectionState.cs`, and matching Controls tests.
3. One worker owns US3 dialog-description and persistence paths: `TDialogDescription*.cs`, `TDialogDescriptionRecord.cs`, `DialogDesignerFlowTests.cs`, and `DialogDescriptionPersistenceTests.cs`.
4. Workers must not revert or overwrite changes in shared foundational files; coordinate API naming in `TStandardDialogFlowState.cs` and `TStandardDialogValidationMessage.cs`.

## Notes

- Keep all new public APIs documented German-first and English-second at CEFR-B2 readability.
- Keep file dialogs decision-only; do not add file loading, writing, deletion, or overwrite operations to standard dialogs.
- Keep symbolic charset selection as dialog data only; do not change terminal rendering, fonts, buffers, or emulation behavior in this feature.
- Keep wave-2 examples as downstream consumers; do not port `demo`, `sdlg`, `sdlg2`, or `dlgdsn` in 010.
