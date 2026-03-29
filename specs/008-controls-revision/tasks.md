# Tasks: Controls Revision

**Input**: Design documents from `/Users/thorstenhindermann/RiderProjects/TuiVision/specs/008-controls-revision/`
**Prerequisites**: `plan.md` (required), `spec.md` (required for user stories), `research.md`, `data-model.md`, `contracts/`

**Tests**: Test tasks are mandatory for this behavior-changing Controls revision. Follow Red-Green-Refactor: add failing MSTest coverage first, keep the first red state reviewable, implement the minimum managed behavior to make those tests green in a later commit, and only refactor after the green state is already visible.

**Organization**: Tasks are grouped by user story so that menu behavior, status-line routing, and window/dialog control can each be implemented and validated as independent increments.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel after dependencies are satisfied
- **[Story]**: Which user story this task belongs to (`[US1]`, `[US2]`, `[US3]`)
- Every task names the concrete file path that must be touched or reviewed

## Path Conventions

- Controls production code lives in `src/TuiVision.Controls/`
- Controls MSTest coverage lives in `tests/TuiVision.Controls.Tests/`
- Proof and tracking artifacts live in `docs/`, `Pflichtenheft.md`, and `specs/`

## Phase 1: Setup (Branch Governance and Delivery Surface)

**Purpose**: Lock the numbered-branch governance and delivery boundaries before behavior work starts.

- [ ] T001 Align and maintain the numbered-branch version fields in `Directory.Build.props` for feature `008-controls-revision`, including incrementing the `Build` component before every `dotnet build` or `dotnet test` invocation during implementation and realigning `Patch` before commit or push.

---

## Phase 2: Foundational (Blocking Compile-Time Seams)

**Purpose**: Install shared test scaffolding and reserve only the narrow compile-only declarations that a story's first red commit may need, without widening scope beyond the approved plan.

**⚠️ CRITICAL**: No user-story runtime behavior under `src/TuiVision.Controls/` should be implemented before the corresponding red tests exist. If a statically typed red test needs a new type or member to compile, add only the inert declaration in the same red commit as that test, never in a prior standalone implementation commit.

- [ ] T002 When a story's first red tests require them, add inert compile-only seams in `src/TuiVision.Controls/TSubMenu.cs`, `src/TuiVision.Controls/TStatusDef.cs`, `src/TuiVision.Controls/WindowFlags.cs`, and `src/TuiVision.Controls/TView.cs` only in the same red commit as those tests, and only as far as required for the tests to compile without implementing delivered runtime behavior.
- [ ] T003 Create the new test home `tests/TuiVision.Controls.Tests/TWindowTests.cs` with bilingual XML documentation so the User-Story-3 red suite has a dedicated file before production window changes begin.
- [ ] T004 [P] Extend shared Controls test helpers in `tests/TuiVision.Controls.Tests/ControlEventFactory.cs`, `tests/TuiVision.Controls.Tests/ControlTestContext.cs`, and `tests/TuiVision.Controls.Tests/ShellTestSupport.cs` only where needed so menu, status-line, window, and dialog red tests can drive repeatable keyboard- and shell-behavior scenarios.
- [ ] T005 [P] If existing test homes need structural cleanup before red coverage is added, refactor only `tests/TuiVision.Controls.Tests/TMenuBarTests.cs`, `tests/TuiVision.Controls.Tests/TStatusLineTests.cs`, `tests/TuiVision.Controls.Tests/TDialogTests.cs`, `tests/TuiVision.Controls.Tests/TDialogCompositeTests.cs`, `tests/TuiVision.Controls.Tests/TProgramTests.cs`, `tests/TuiVision.Controls.Tests/TEditorCommandTests.cs`, and `tests/TuiVision.Controls.Tests/TEditWindowTests.cs` so later red tests can be added without touching production files first.

**Checkpoint**: Shared red-test homes and helper scaffolding exist, and any compile-only production seams still wait for the specific red commit that needs them.

---

## Phase 3: User Story 1 - Operate Menus Like a Real Turbo Vision Shell (Priority: P1) 🎯 MVP

**Goal**: Restore one-level menu declaration, active-menu navigation, submenu focus visibility, and resize-safe layout behavior.

**Independent Test**: Activate the menu shell, navigate across top-level entries and within an open submenu using directional keys alone, confirm the focused submenu entry remains visually obvious, and verify resize-driven relayout without relying on mnemonic-only activation.

### Tests for User Story 1 (MANDATORY) ⚠️

> **NOTE**: Write these tests first, verify they fail, and only then implement the menu behavior.

- [ ] T006 [P] [US1] Add failing menu-behavior coverage in `tests/TuiVision.Controls.Tests/TMenuBarTests.cs` for top-level wrap-around, submenu wrap-around, skip-over of disabled/separator entries, focused-entry highlighting, `Enter` confirmation, mnemonic confirmation, and clean dismissal.
- [ ] T007 [P] [US1] Add failing shell-integration coverage in `tests/TuiVision.Controls.Tests/TProgramTests.cs` for resize-driven menu relayout, active-menu command routing, and first-redraw-visible menu-state updates after relevant shell events.

### Implementation for User Story 1

- [ ] T008 [P] [US1] Expand the declaration model in `src/TuiVision.Controls/TMenuItem.cs` and `src/TuiVision.Controls/TSubMenu.cs` so one-level submenu composition, historical `tvguid02`-style builder syntax, mnemonic metadata, separator rows, and disabled entries are represented explicitly.
- [ ] T009 [US1] Implement active menu interaction in `src/TuiVision.Controls/TMenuBar.cs`, covering activation, top-level navigation, submenu opening, skip/wrap behavior, focused-entry highlight state, command dispatch, and clean dismissal.
- [ ] T010 [US1] Update `src/TuiVision.Controls/TProgram.cs` and `src/TuiVision.Controls/TApplication.cs` only as needed so menu relayout and active-menu execution remain aligned with shell resize and command-routing behavior.
- [ ] T011 [US1] Re-run the focused menu slice in `tests/TuiVision.Controls.Tests/TMenuBarTests.cs` and `tests/TuiVision.Controls.Tests/TProgramTests.cs`, and complete the mandatory bilingual XML documentation/comments for the touched menu-shell members in the same User-Story-1 slice until that slice is independently green and documented.

**Checkpoint**: Menu declaration and runtime navigation now behave like the approved one-level Turbo Vision shell baseline and can be reviewed independently.

---

## Phase 4: User Story 2 - See Context-Sensitive Actions in the Status Line (Priority: P2)

**Goal**: Add explicit help-context-driven status routing with first-match-wins ordering, neutral fallback, and a temporary compatibility bridge for existing hint producers.

**Independent Test**: Switch focus between views with different help contexts, verify that the status line selects the correct action set on every change, shows a neutral empty state on no match, and still falls back to direct focused-view hints only when no explicit definitions were configured.

### Tests for User Story 2 (MANDATORY) ⚠️

- [ ] T012 [P] [US2] Add failing status-routing coverage in `tests/TuiVision.Controls.Tests/TStatusLineTests.cs` for inclusive-range matching, first-match-wins overlap handling, neutral no-match fallback, context-change refresh, and execution of the visible status action through the normal command path.
- [ ] T013 [P] [US2] Add failing compatibility-bridge coverage in `tests/TuiVision.Controls.Tests/TEditorCommandTests.cs` and `tests/TuiVision.Controls.Tests/TEditWindowTests.cs` for existing `GetStatusHints()` producers when no explicit `TStatusDef` configuration is present.

### Implementation for User Story 2

- [ ] T014 [P] [US2] Implement the explicit status-definition surface in `src/TuiVision.Controls/TStatusDef.cs` and update `src/TuiVision.Controls/TStatusItem.cs` only as needed so ordered definition chains and status action sets can be represented without inventing a second routing model.
- [ ] T015 [US2] Implement explicit help-context routing and compatibility-bridge behavior in `src/TuiVision.Controls/TView.cs`, `src/TuiVision.Controls/TEditor.cs`, and `src/TuiVision.Controls/TEditWindow.cs`.
- [ ] T016 [US2] Implement first-match-wins resolution, neutral fallback, and focus-driven refresh in `src/TuiVision.Controls/TStatusLine.cs`, and narrow `src/TuiVision.Controls/TProgram.cs` only if shell focus-change propagation needs adjustment for the new status model.
- [ ] T017 [US2] Re-run the focused status slice in `tests/TuiVision.Controls.Tests/TStatusLineTests.cs`, `tests/TuiVision.Controls.Tests/TEditorCommandTests.cs`, and `tests/TuiVision.Controls.Tests/TEditWindowTests.cs`, and complete the mandatory bilingual XML documentation/comments for the touched status-routing members in the same User-Story-2 slice until that slice is independently green and documented.

**Checkpoint**: Status-line behavior is explicitly context-driven, deterministic on overlap and no-match cases, and still compatible with the currently ported legacy hint producers.

---

## Phase 5: User Story 3 - Control Windows and Dialog Closure Safely (Priority: P3)

**Goal**: Add visible window close affordances, guarded keyboard close behavior, reversible move mode, and dialog close validation that preserves modal semantics.

**Independent Test**: Open a closable/movable window and a validating dialog, confirm visible close affordance plus `Ctrl+W` and guarded `Escape`, exercise `Ctrl+F5` move mode with `Enter` commit and `Escape` restore, and verify that rejected dialog closes preserve state while accepted closes return the expected modal result.

### Tests for User Story 3 (MANDATORY) ⚠️

- [ ] T018 [P] [US3] Add failing window-behavior coverage in `tests/TuiVision.Controls.Tests/TWindowTests.cs` for close-affordance visibility, `Ctrl+W`, guarded `Escape`, child-consumed `Escape`, `Ctrl+F5` move entry, preview movement, `Enter` commit, and `Escape` restore.
- [ ] T019 [P] [US3] Add failing dialog-validation coverage in `tests/TuiVision.Controls.Tests/TDialogTests.cs` and `tests/TuiVision.Controls.Tests/TDialogCompositeTests.cs` for `Valid(ushort command)`, rejected close requests that preserve dialog state, and accepted close requests that return an explicit modal result without regressing existing modal behavior.

### Implementation for User Story 3

- [ ] T020 [P] [US3] Implement the focused window-capability surface in `src/TuiVision.Controls/WindowFlags.cs` and `src/TuiVision.Controls/TWindow.cs`, covering visible close affordance, guarded close handling, move-session snapshots, and reversible move mode without reopening zoom/grow scope.
- [ ] T021 [US3] Implement dialog close validation in `src/TuiVision.Controls/TDialog.cs` and update `src/TuiVision.Controls/TButton.cs` only if command dispatch needs narrowing so validated closes stay on the existing modal-result path.
- [ ] T022 [US3] Update `src/TuiVision.Controls/TProgram.cs` and `src/TuiVision.Controls/TApplication.cs` only as needed so window close commands and dialog validation stay on the normal shell command pipeline.
- [ ] T023 [US3] Re-run the focused window/dialog slice in `tests/TuiVision.Controls.Tests/TWindowTests.cs`, `tests/TuiVision.Controls.Tests/TDialogTests.cs`, and `tests/TuiVision.Controls.Tests/TDialogCompositeTests.cs`, and complete the mandatory bilingual XML documentation/comments for the touched window/dialog members in the same User-Story-3 slice until that slice is independently green and documented.

**Checkpoint**: Closable/movable windows and validating dialogs now follow the approved shell contract without broadening into zoom/grow or a new modal-loop design.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Finish proof, governance, documentation, portability evidence, and final validation after the three user stories are green.

- [ ] T024 [P] Update `tests/TuiVision.Controls.Tests/ControlsProofTests.cs` and `tests/TuiVision.Controls.Tests/ControlCoverageSweepTests.cs` where needed so the affected historical Controls lineage remains explicitly proved, the fresh `TuiVision.Controls` coverage gate stays reviewable, and any cross-assembly coverage references needed for untouched gate assemblies remain current.
- [ ] T025 [P] Update `docs/porting-status.md` for the affected historical rows `tmenubar.cc`, `tmenubox.cc`, `tmenuvie.cc`, `tsubmenu.cc`, `tstatusd.cc`, `tstatusl.cc`, `tdialog.cc`, and `twindow.cc` so the ledger reflects the delivered Controls behavior and evidence paths.
- [ ] T026 [P] Record the delivered Controls revision in `Pflichtenheft.md`, and move the prominent `>>> NAECHSTER SCHRITT <<<` marker if this implementation changes the highest-priority remaining work item.
- [ ] T027 [P] Refresh `docs/project-statistics.md` with the `008-controls-revision` implementation window, production/test/documentation deltas, main work packages, and both manual-effort baselines after the feature lands.
- [ ] T028 [P] Capture runtime validation evidence for `MacBook Air M2`, `Mac mini M4 Pro`, Linux, and Windows/WSL in `docs/guides/multi-mac-workflow.md`, covering menu behavior, status-context refresh, window move/close behavior, dialog validation, and a repeated-run result matrix for the `SC-003` close/move scenarios with 20 attempts per environment plus the first-attempt pass rate.
- [ ] T029 [P] Perform the final cross-story documentation audit for all touched project-owned source files under `src/TuiVision.Controls/`, run `docfx docfx.json` if API/XML surfaces changed, and fix any remaining documentation-generator issues in `src/` and `docs/` without deferring member-level documentation debt past the story slices.
- [ ] T030 [P] Audit touched files under `src/TuiVision.Controls/`, `tests/TuiVision.Controls.Tests/`, `docs/`, and `specs/008-controls-revision/` against `FR-016`, `FR-017`, and `FR-018`, then record that no streaming, terminal-mouse, or palette-customization scope leaked into this increment.
- [ ] T031 [P] Rename `Lastenheft_ControlsRevision.md` to `Lastenheft_ControlsRevision.008-controls-revision.md` once this branch fully implements the described scope so the delivered requirement set stays branch-traceable in the repository.
- [ ] T032 [P] Synchronize `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` in the same work item if implementation changes shared guidance, active technologies, or project structure.
- [ ] T033 [P] Before each final validation run, increment the `Build` component in `Directory.Build.props`, then run `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Controls.Tests/`, `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`, `dotnet test`, `dotnet format --verify-no-changes`, and conditional `docfx docfx.json` on the primary macOS path, plus record equivalent `dotnet build --configuration Release`, targeted Controls-test, and full `dotnet test` outcomes on Linux and Windows/WSL; produce the final assembly-specific coverage package by refreshing `TuiVision.Controls`, citing the accepted Phase-8 evidence for untouched `TuiVision.Core`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`, and regenerating fresh evidence for any of those assemblies if implementation touches them; then fix remaining issues in touched `src/`, `tests/`, `docs/`, `Pflichtenheft.md`, and `specs/008-controls-revision/quickstart.md`.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; starts immediately.
- **Foundational (Phase 2)**: Depends on Setup and prepares shared test infrastructure; any production compile-only seams are introduced only inside the corresponding story's first red commit.
- **User Story 1 (Phase 3)**: Depends on Foundational and is the MVP.
- **User Story 2 (Phase 4)**: Depends on Foundational; safest after User Story 1 establishes the menu/shell interaction pattern.
- **User Story 3 (Phase 5)**: Depends on Foundational; can start after User Story 1 if shared shell command-path work needs coordination.
- **Polish (Phase 6)**: Depends on the desired user stories being complete.

### User Story Dependencies

- **User Story 1 (P1)**: First deliverable and MVP; it restores the menu shell baseline needed by the next example wave.
- **User Story 2 (P2)**: Depends on the shared red-test infrastructure from Phase 2 and may introduce its explicit help-context seam inside its own red/green story slice.
- **User Story 3 (P3)**: Depends on the shared red-test infrastructure from Phase 2 and may introduce any required compile-only seams inside its own red/green story slice while sharing command-path plumbing with User Stories 1 and 2.

### Within Each User Story

- Red tests must be written and observed failing before production behavior is added.
- If a red test needs a new production type or member to compile, introduce only the inert declaration in that same red commit.
- Keep a reviewable Red commit -> Green commit -> optional Refactor commit sequence for each story.
- Shared declaration or seam changes should remain minimal and must not reopen mouse, streaming, zoom/grow, or broader editor/help scope.
- Re-run the focused story test slice before advancing to the next story or to polish.

### Parallel Opportunities

- `T004` and `T005` can run in parallel after the new test-home task `T003`; any compile-only seam from `T002` is introduced only when a specific story's red tests actually need it.
- In User Story 1, `T006` and `T007` can run in parallel before the production work starts.
- In User Story 2, `T012` and `T013` can run in parallel before the production work starts.
- In User Story 3, `T018` and `T019` can run in parallel before the production work starts.
- In Polish, `T024` through `T032` can run in parallel after the story slices are green; `T033` remains the final consolidation step.

---

## Parallel Example: User Story 1

```bash
# Launch the mandatory red tests together:
Task: "T006 Add failing menu-behavior coverage in tests/TuiVision.Controls.Tests/TMenuBarTests.cs"
Task: "T007 Add failing shell-integration coverage in tests/TuiVision.Controls.Tests/TProgramTests.cs"

# Then split the production work by declaration/runtime responsibility:
Task: "T008 Expand the declaration model in src/TuiVision.Controls/TMenuItem.cs and src/TuiVision.Controls/TSubMenu.cs"
Task: "T009 Implement active menu interaction in src/TuiVision.Controls/TMenuBar.cs"
```

## Parallel Example: User Story 2

```bash
# Launch the mandatory red tests together:
Task: "T012 Add failing status-routing coverage in tests/TuiVision.Controls.Tests/TStatusLineTests.cs"
Task: "T013 Add failing compatibility-bridge coverage in tests/TuiVision.Controls.Tests/TEditorCommandTests.cs and tests/TuiVision.Controls.Tests/TEditWindowTests.cs"

# Then split definition and bridge work:
Task: "T014 Implement the explicit status-definition surface in src/TuiVision.Controls/TStatusDef.cs and src/TuiVision.Controls/TStatusItem.cs"
Task: "T015 Implement explicit help-context routing in src/TuiVision.Controls/TView.cs, src/TuiVision.Controls/TEditor.cs, and src/TuiVision.Controls/TEditWindow.cs"
```

## Parallel Example: User Story 3

```bash
# Launch the mandatory red tests together:
Task: "T018 Add failing window-behavior coverage in tests/TuiVision.Controls.Tests/TWindowTests.cs"
Task: "T019 Add failing dialog-validation coverage in tests/TuiVision.Controls.Tests/TDialogTests.cs and tests/TuiVision.Controls.Tests/TDialogCompositeTests.cs"

# Then split window and dialog production work:
Task: "T020 Implement window capability behavior in src/TuiVision.Controls/WindowFlags.cs and src/TuiVision.Controls/TWindow.cs"
Task: "T021 Implement dialog close validation in src/TuiVision.Controls/TDialog.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational compile-time seams.
3. Complete Phase 3: User Story 1.
4. Stop and validate that `src/TuiVision.Controls/TMenuBar.cs`, `src/TuiVision.Controls/TMenuItem.cs`, `src/TuiVision.Controls/TSubMenu.cs`, `tests/TuiVision.Controls.Tests/TMenuBarTests.cs`, and `tests/TuiVision.Controls.Tests/TProgramTests.cs` together satisfy the menu-shell MVP.

### Incremental Delivery

1. Establish the branch-governance and compile-time seams.
2. Deliver User Story 1 as the menu-shell baseline.
3. Add User Story 2 for explicit status-line routing and legacy bridge compatibility.
4. Add User Story 3 for window control and dialog validation behavior.
5. Finish with proof-surface updates, portability evidence, documentation, and full validation.

### Parallel Team Strategy

With multiple developers:

1. Complete Setup and Foundational together.
2. Split the story work after Phase 2:
   - Developer A: menu tests and `src/TuiVision.Controls/TMenuBar.cs` / `src/TuiVision.Controls/TMenuItem.cs` / `src/TuiVision.Controls/TSubMenu.cs`
   - Developer B: status-line tests and `src/TuiVision.Controls/TStatusLine.cs` / `src/TuiVision.Controls/TStatusDef.cs` / `src/TuiVision.Controls/TView.cs`
   - Developer C: window/dialog tests and `src/TuiVision.Controls/TWindow.cs` / `src/TuiVision.Controls/WindowFlags.cs` / `src/TuiVision.Controls/TDialog.cs`
3. Rejoin for proof surfaces, portability evidence, and full validation.

---

## Notes

- `[P]` means the task can proceed independently after its stated dependencies are satisfied.
- The task order preserves the one-level submenu boundary and avoids hidden expansion into terminal mouse, streaming, zoom/grow, or new example-wave work.
- `TEditor` and `TEditWindow` are treated as the current concrete legacy-hint producers for the temporary `GetStatusHints()` compatibility bridge.
- `docs/porting-status.md`, `Pflichtenheft.md`, `docs/project-statistics.md`, and the Lastenheft rename are mandatory delivery surfaces for this feature; do not treat them as optional afterthoughts.
- The final validation step is not complete until the Controls-specific tests, coverage run, full repository test suite, formatting pass, and conditional docfx pass are all green.
