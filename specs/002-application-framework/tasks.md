# Tasks: Application Framework Shell

**Input**: Design documents from `/specs/002-application-framework/`
**Prerequisites**: `plan.md` (required), `spec.md` (required), `research.md`, `data-model.md`, `contracts/application-shell-api.md`, `quickstart.md`

**Tests**: Included. The feature specification, constitution, and plan require visible TDD with failing MSTest coverage before implementation.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story after the foundational phases are complete.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies on incomplete tasks)
- **[Story]**: Which user story this task belongs to (e.g. `US1`, `US2`, `US3`)
- Every task includes exact file path(s)

## Phase 1: Setup (Shared Test Infrastructure)

**Purpose**: Prepare reusable test scaffolding for shell implementation without committing production behavior before Red tests exist.

- [x] T001 Create shell test support helpers in `tests/TuiVision.Controls.Tests/ShellTestSupport.cs`
- [x] T002 [P] Create shell presenter and resize test doubles in `tests/TuiVision.Controls.Tests/ShellPresenterSpy.cs`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Shared shell primitives and extension hooks that block all user stories.

**⚠️ CRITICAL**: No user story work should begin until these tasks are complete.

- [x] T003 Write failing shared command-ID and shell action model tests in `tests/TuiVision.Controls.Tests/TProgramTests.cs`
- [x] T004 [P] Write failing status-hint declaration and propagation tests in `tests/TuiVision.Controls.Tests/TStatusLineTests.cs`
- [x] T005 Implement shared integer command constants in `src/TuiVision.Controls/ShellCommandIds.cs`
- [x] T006 [P] Implement nested menu action model in `src/TuiVision.Controls/TMenuItem.cs`
- [x] T007 [P] Implement status action and status-hint model in `src/TuiVision.Controls/TStatusItem.cs`
- [x] T008 Implement virtual `GetStatusHints()` in `src/TuiVision.Controls/TView.cs` for status-hint declarations
- [x] T009 Add shell extension hooks for focus/change propagation in `src/TuiVision.Controls/TGroup.cs`

**Checkpoint**: Shared shell primitives, command IDs, and test scaffolding are ready. User story implementation can proceed.

---

## Phase 3: User Story 1 - Launch a complete application shell (Priority: P1) 🎯 MVP

**Goal**: Deliver a usable default shell that auto-creates menu bar, desktop workspace, and status line with the documented fixed layout and shell lifecycle.

**Independent Test**: A representative `TApplication` starts with a 1-row menu bar, a fill desktop region, and a 1-row status line, remains interactive with an empty desktop, and shuts down cleanly on exit request.

### Tests for User Story 1 ⚠️

> **NOTE: Write these tests FIRST and ensure they FAIL before implementation.**

- [x] T010 [P] [US1] Write failing default shell composition and fixed-layout tests in `tests/TuiVision.Controls.Tests/TApplicationTests.cs`
- [x] T011 [P] [US1] Write failing shell lifecycle and controlled-shutdown tests in `tests/TuiVision.Controls.Tests/TProgramTests.cs`

### Implementation for User Story 1

- [x] T012 [P] [US1] Implement empty-workspace shell container in `src/TuiVision.Controls/TDesktop.cs`
- [x] T013 [P] [US1] Implement fixed-height shell region view for default menu presentation in `src/TuiVision.Controls/TMenuBar.cs`
- [x] T014 [P] [US1] Implement fixed-height shell region view for default status presentation in `src/TuiVision.Controls/TStatusLine.cs`
- [x] T015 [US1] Implement shell lifecycle, region ownership, startup, and controlled shutdown orchestration in `src/TuiVision.Controls/TProgram.cs`
- [x] T016 [US1] Implement default shell assembly and customization seams in `src/TuiVision.Controls/TApplication.cs`

**Checkpoint**: User Story 1 is independently functional as the MVP shell.

---

## Phase 4: User Story 2 - Trigger global actions from menus and status line (Priority: P2)

**Goal**: Deliver shared command routing across menu bar, status line, and keyboard interactions with disabled-but-visible action handling.

**Independent Test**: A shared command bound to menu bar and status line executes exactly once regardless of entry point, menu activation works with F10/Alt and arrow keys, nested submenus open correctly, and disabled actions remain visible but cannot execute.

### Tests for User Story 2 ⚠️

- [x] T017 [P] [US2] Write failing menu activation, arrow-key navigation, and nested submenu tests in `tests/TuiVision.Controls.Tests/TMenuBarTests.cs`
- [x] T018 [P] [US2] Write failing shared command-routing and single-execution tests in `tests/TuiVision.Controls.Tests/TProgramTests.cs`
- [x] T019 [P] [US2] Write failing status-line hint refresh and disabled-visibility tests in `tests/TuiVision.Controls.Tests/TStatusLineTests.cs`

### Implementation for User Story 2

- [x] T020 [US2] Implement shared integer Command ID routing and disabled-command guards in `src/TuiVision.Controls/TProgram.cs`
- [x] T021 [US2] Implement F10/Alt activation, arrow-key navigation, and nested submenu traversal in `src/TuiVision.Controls/TMenuBar.cs`
- [x] T022 [US2] Implement focus-driven status hint refresh and disabled-action rendering in `src/TuiVision.Controls/TStatusLine.cs`
- [x] T023 [P] [US2] Wire command binding and visibility rules in `src/TuiVision.Controls/TMenuItem.cs` and `src/TuiVision.Controls/TStatusItem.cs`

**Checkpoint**: User Story 2 is independently functional on top of the MVP shell.

---

## Phase 5: User Story 3 - Work within the desktop area (Priority: P3)

**Goal**: Deliver desktop child activation, removal, and deterministic focus fallback while preserving a valid shell focus target.

**Independent Test**: Child views can be inserted, activated, and removed within `TDesktop`; closing the active child selects the next eligible child when present or falls back to the desktop itself when none remain.

### Tests for User Story 3 ⚠️

- [x] T024 [P] [US3] Write failing desktop child activation and focus fallback tests in `tests/TuiVision.Controls.Tests/TDesktopTests.cs`
- [x] T025 [P] [US3] Write failing zero-child startup and active-child removal integration tests in `tests/TuiVision.Controls.Tests/TApplicationTests.cs`

### Implementation for User Story 3

- [x] T026 [US3] Implement desktop child activation, removal handling, and next-eligible fallback in `src/TuiVision.Controls/TDesktop.cs`
- [x] T027 [US3] Integrate desktop focus notifications and empty-workspace fallback into `src/TuiVision.Controls/TProgram.cs` and `src/TuiVision.Controls/TApplication.cs`

**Checkpoint**: User Story 3 is independently functional on top of the MVP shell.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Complete functional edge cases, documentation, and repository-wide validation.

- [x] T028 Write failing terminal resize re-layout tests in `tests/TuiVision.Controls.Tests/TProgramTests.cs` and `tests/TuiVision.Controls.Tests/TApplicationTests.cs`
- [x] T029 Implement runtime terminal resize detection and shell re-layout in `src/TuiVision.Controls/TProgram.cs` and `src/TuiVision.Controls/TApplication.cs`
- [x] T030 [P] Update bilingual XML documentation in `src/TuiVision.Controls/TProgram.cs`, `src/TuiVision.Controls/TApplication.cs`, `src/TuiVision.Controls/TDesktop.cs`, `src/TuiVision.Controls/TMenuBar.cs`, `src/TuiVision.Controls/TStatusLine.cs`, `src/TuiVision.Controls/TMenuItem.cs`, `src/TuiVision.Controls/TStatusItem.cs`, `src/TuiVision.Controls/ShellCommandIds.cs`, `src/TuiVision.Controls/TView.cs`, and `src/TuiVision.Controls/TGroup.cs`
- [x] T031 [P] Align feature-facing documentation with the implemented API in `specs/002-application-framework/quickstart.md` and `specs/002-application-framework/contracts/application-shell-api.md`
- [x] T032 Run full validation and coverage gate from repository root with `dotnet build --configuration Release`, `dotnet test`, `dotnet format --verify-no-changes`, `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`, and `docfx docfx.json`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1: Setup** — can start immediately
- **Phase 2: Foundational** — depends on Phase 1 and blocks all user stories
- **Phase 3: User Story 1** — depends on Phase 2 and delivers the MVP shell
- **Phase 4: User Story 2** — depends on User Story 1 because shared routing and menu/status behavior require the MVP shell classes
- **Phase 5: User Story 3** — depends on User Story 1 because desktop child management builds on the MVP shell
- **Phase 6: Polish** — depends on all desired user stories being complete

### User Story Dependencies

- **US1 (P1)**: First deliverable and MVP; no user-story dependencies after Foundational
- **US2 (P2)**: Depends on US1 shell types being present; remains independently testable once implemented
- **US3 (P3)**: Depends on US1 shell types being present; remains independently testable once implemented

### Within Each User Story

- Tests MUST be written first and fail before implementation begins
- Shared models/constants before story-specific orchestration
- Container/view behavior before shell integration
- Story checkpoint validation before starting the next dependent phase

### Dependency Graph

```text
Setup
  ↓
Foundational
  ↓
US1 (MVP shell)
  ├── US2 (menus/status commands)
  └── US3 (desktop child management)
        ↓
Polish (resize, docs, validation)
```

---

## Parallel Opportunities

- **Phase 1**: `T001` and `T002` can be split across helper files
- **Phase 2**: `T004`, `T006`, and `T007` are parallel after `T003`; `T008` and `T009` can proceed once shared models are understood
- **US1**: `T010` and `T011` can run in parallel; `T012`, `T013`, and `T014` can run in parallel after failing tests exist
- **US2**: `T017`, `T018`, and `T019` can run in parallel; `T023` can run in parallel with `T021`/`T022` after `T020` defines routing expectations
- **US3**: `T024` and `T025` can run in parallel
- **Polish**: `T030` and `T031` can run in parallel after implementation stabilizes

---

## Parallel Example: User Story 1

```bash
# Write failing US1 tests in parallel:
Task: "T010 [US1] Write failing default shell composition and fixed-layout tests in tests/TuiVision.Controls.Tests/TApplicationTests.cs"
Task: "T011 [US1] Write failing shell lifecycle and controlled-shutdown tests in tests/TuiVision.Controls.Tests/TProgramTests.cs"

# Implement basic shell regions in parallel after tests are red:
Task: "T012 [US1] Implement empty-workspace shell container in src/TuiVision.Controls/TDesktop.cs"
Task: "T013 [US1] Implement fixed-height shell region view for default menu presentation in src/TuiVision.Controls/TMenuBar.cs"
Task: "T014 [US1] Implement fixed-height shell region view for default status presentation in src/TuiVision.Controls/TStatusLine.cs"
```

## Parallel Example: User Story 2

```bash
# Write failing US2 tests in parallel:
Task: "T017 [US2] Write failing menu activation, arrow-key navigation, and nested submenu tests in tests/TuiVision.Controls.Tests/TMenuBarTests.cs"
Task: "T018 [US2] Write failing shared command-routing and single-execution tests in tests/TuiVision.Controls.Tests/TProgramTests.cs"
Task: "T019 [US2] Write failing status-line hint refresh and disabled-visibility tests in tests/TuiVision.Controls.Tests/TStatusLineTests.cs"
```

## Parallel Example: User Story 3

```bash
# Write failing US3 tests in parallel:
Task: "T024 [US3] Write failing desktop child activation and focus fallback tests in tests/TuiVision.Controls.Tests/TDesktopTests.cs"
Task: "T025 [US3] Write failing zero-child startup and active-child removal integration tests in tests/TuiVision.Controls.Tests/TApplicationTests.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational
3. Complete Phase 3: User Story 1
4. **STOP and VALIDATE** the MVP shell using the US1 independent test criteria

### Incremental Delivery

1. Build shared shell primitives and red tests
2. Deliver US1 as the first usable shell
3. Add US2 command-routing and interaction behavior
4. Add US3 desktop child-management behavior
5. Finish with resize handling, documentation alignment, and repository-wide validation

### Suggested MVP Scope

- **MVP**: Phase 1 + Phase 2 + Phase 3 (through `T016`)
- This delivers the first usable `TApplication` shell with menu bar, desktop, status line, and controlled shutdown

---

## Notes

- All tasks follow the required checklist format: checkbox, ID, optional `[P]`, optional story label, and exact file path(s)
- Story phases are ordered by priority from `spec.md`
- TDD is mandatory for this feature; do not implement before the corresponding failing tests exist
- `SC-005` coverage work is explicitly included in the final validation phase
