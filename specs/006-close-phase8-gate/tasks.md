# Tasks: M-07 Closure and Phase-8 Entrance Gate

**Input**: Design documents from `/Users/thorstenhindermann/RiderProjects/TuiVision/specs/006-close-phase8-gate/`
**Prerequisites**: `plan.md` (required), `spec.md` (required for user stories), `research.md`, `data-model.md`, `contracts/`

**Tests**: Test tasks are mandatory for the behavior-changing closure work in this repository. Follow Red-Green-Refactor: add failing tests first, implement the missing behavior, then re-run the relevant validation before advancing the ledger or gate claim.

**Organization**: Tasks are grouped by user story so that the remaining `M-07` proof, the Phase-8 evidence package, and the explicit gate-decision artifacts can be completed and reviewed in dependency order.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete work)
- **[Story]**: Which user story this task belongs to (`[US1]`, `[US2]`, `[US3]`)
- Every task names the concrete file or directory path that must be touched or reviewed

## Path Conventions

- Runtime code lives in `src/`
- Tests live in `tests/`
- Proof and governance artifacts live in `docs/`, `Pflichtenheft.md`, and `specs/`

## Phase 1: Setup (Shared Inventory)

**Purpose**: Freeze the real 006 closure scope before code changes begin.

- [ ] T001 Audit `docs/porting-status.md` and freeze the list of all remaining `portiert + Test ausstehend` rows plus non-driver `geplant` targets that 006 must close.
- [ ] T002 Audit gate-scoped responsibilities in `src/TuiVision.Core/`, `src/TuiVision.Controls/`, `src/TuiVision.Serialization/`, `src/TuiVision.Compatibility/Class1.cs`, `src/TuiVision.Drivers.Console/Class1.cs`, and `src/TuiVision.Drivers.Console/TConsoleDriver.cs` to confirm whether any module must be restructured out of the hard gate.
- [ ] T003 [P] Audit the authoritative proof surfaces in `Pflichtenheft.md`, `docs/porting-status.md`, `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`, `specs/006-close-phase8-gate/quickstart.md`, and `specs/006-close-phase8-gate/contracts/phase-8-gate-contract.md` so later closure updates stay synchronized.

---

## Phase 2: Foundational (Blocking Proof Infrastructure)

**Purpose**: Install the failing guardrails and shared test infrastructure that block all story work until they exist.

**⚠️ CRITICAL**: No user-story implementation should start before these tasks are complete.

- [ ] T004 Strengthen `tests/TuiVision.Drivers.Tests/PortingStatusLedgerTests.cs` and `tests/TuiVision.Drivers.Tests/PortingStatusCompletenessTests.cs` so provisional `portiert + Test ausstehend` rows and undocumented non-driver `geplant` closures fail fast.
- [ ] T005 [P] Add gate-scope integrity assertions in `tests/TuiVision.Examples.SmokeTests/Test1.cs` and `tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs` that reject placeholder-only or no-op-only closure evidence for `TuiVision.Compatibility` and `TuiVision.Drivers.Console`.
- [ ] T006 [P] Create `tests/TuiVision.Compatibility.Tests/TuiVision.Compatibility.Tests.csproj`, add it to `TuiVision.sln`, and scaffold first coverage-focused tests under `tests/TuiVision.Compatibility.Tests/`.
- [ ] T007 Add shared ledger/evidence parsing helpers to `tests/TuiVision.Drivers.Tests/Phase7DriverTestContext.cs` so row-state, evidence-link, and final-proof checks can be reused across the 006 proof tests.

**Checkpoint**: The repository now has failing infrastructure for final-state proof, gate-scope integrity, and dedicated Compatibility coverage work.

---

## Phase 3: User Story 1 - Resolve the remaining M-07 proof gap (Priority: P1) 🎯 MVP

**Goal**: Turn every remaining historical `.cc` row into a final proof state backed by real implementation or an explicit justified omission.

**Independent Test**: Run `dotnet test tests/TuiVision.Core.Tests/`, `dotnet test tests/TuiVision.Controls.Tests/`, `dotnet test tests/TuiVision.Serialization.Tests/`, `dotnet test tests/TuiVision.Compatibility.Tests/`, and `dotnet test tests/TuiVision.Drivers.Tests/`, then inspect `docs/porting-status.md` and confirm that no row remains in `portiert + Test ausstehend`.

### Tests for User Story 1 (MANDATORY for behavior changes) ⚠️

> **NOTE**: Write these tests first, verify they fail, and only then implement the missing framework behavior.

- [ ] T008 [P] [US1] Add failing Core proof tests under `tests/TuiVision.Core.Tests/` for the ledger mappings from `tcollect.cc`, `tnscolle.cc`, `tnssorte.cc`, `tsortedc.cc`, `tsortedl.cc`, `tstrinde.cc`, `tstringc.cc`, and `tstrlist.cc`.
- [ ] T009 [P] [US1] Add failing Controls proof tests under `tests/TuiVision.Controls.Tests/` for the still-planned config, window, validator, file-info, colour, text, selection, and clipboard responsibilities.
- [ ] T010 [P] [US1] Add failing Serialization proof tests in `tests/TuiVision.Serialization.Tests/PStreamTests.cs`, `tests/TuiVision.Serialization.Tests/THelpFileTests.cs`, `tests/TuiVision.Serialization.Tests/TResourceFileTests.cs`, and `tests/TuiVision.Serialization.Tests/SerializationCoverageSweepTests.cs` for the remaining stream/help/resource rows.
- [ ] T011 [P] [US1] Add failing Compatibility and driver-input proof tests under `tests/TuiVision.Compatibility.Tests/` and `tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs` for `tgkey.cc`, `tvintl.cc`, codepage, keyboard, and mouse mappings.

### Implementation for User Story 1

- [ ] T012 [P] [US1] Implement the planned collection and string family under `src/TuiVision.Core/` for the `tcollect`, `tnscolle`, `tnssorte`, `tsortedc`, `tsortedl`, `tstrinde`, `tstringc`, and `tstrlist` ledger mappings.
- [ ] T013 [P] [US1] Implement config, initialization, window, and file-info types under `src/TuiVision.Controls/` for `configfile.cc`, `tprogini.cc`, `twindow.cc`, and `tfileinf.cc`.
- [ ] T014 [P] [US1] Implement validator types under `src/TuiVision.Controls/` for `tvalidat.cc`, `tfilterv.cc`, and `trangeva.cc`.
- [ ] T015 [P] [US1] Implement colour, palette, and selector types under `src/TuiVision.Controls/` for `tclrdisp.cc`, `tcolordi.cc`, `tcolorgr.cc`, `tcolorit.cc`, `tcolorse.cc`, `tpalette.cc`, and `tmonosel.cc`.
- [ ] T016 [P] [US1] Implement managed text and clipboard replacements in `src/TuiVision.Controls/`, `src/TuiVision.Controls/TEditor.cs`, and related Controls files for `osclipboard.cc`, `win32/win32clip.cc`, `tparamte.cc`, `tvtext1.cc`, and `tvtext2.cc`.
- [ ] T017 [P] [US1] Complete stream, help, and resource behavior in `src/TuiVision.Serialization/fpstream.cs`, `src/TuiVision.Serialization/ipstream.cs`, `src/TuiVision.Serialization/opstream.cs`, `src/TuiVision.Serialization/pstream.cs`, `src/TuiVision.Serialization/THelpFile.cs`, `src/TuiVision.Serialization/THelpIndex.cs`, `src/TuiVision.Serialization/TResourceCollection.cs`, `src/TuiVision.Serialization/TResourceFile.cs`, `src/TuiVision.Serialization/TRecordRegistry.cs`, and `src/TuiVision.Serialization/TRecordSerializer.cs`.
- [ ] T018 [P] [US1] Expand `src/TuiVision.Compatibility/Class1.cs` and add concrete support files under `src/TuiVision.Compatibility/` for global key tables, UI strings, internationalization, and xterm-key compatibility behavior.
- [ ] T019 [US1] Extend `src/TuiVision.Drivers.Console/TConsoleDriver.cs`, `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs`, and `src/TuiVision.Drivers.Console/Class1.cs` to close the remaining codepage, keyboard, and mouse proof rows without placeholder-only scaffolding.
- [ ] T020 [US1] Reconcile every affected row in `docs/porting-status.md` to `portiert + getestet` or `bewusst ausgelassen + Begruendung`, with direct evidence or rationale references for all 151 historical `.cc` files.
- [ ] T021 [US1] Run the targeted module proof suite via `tests/TuiVision.Core.Tests/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Serialization.Tests/`, `tests/TuiVision.Compatibility.Tests/`, and `tests/TuiVision.Drivers.Tests/` and fix the remaining proof-gap failures before declaring `M-07` closed.

**Checkpoint**: User Story 1 is complete when the ledger has no provisional rows left and every historical mapping is backed by implementation or explicit rationale.

---

## Phase 4: User Story 2 - Close the Phase-8 entrance evidence package (Priority: P2)

**Goal**: Produce one repository-visible proof package for build, test, coverage, formatting, documentation, and platform evidence.

**Independent Test**: Review `Pflichtenheft.md`, `docs/porting-status.md`, `docs/guides/multi-mac-workflow.md`, and `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md` after the validation runs and confirm that each of the six gate criteria has current supporting evidence.

### Validation and Coverage Tasks for User Story 2

- [ ] T022 [P] [US2] Add or extend coverage-sweep tests under `tests/TuiVision.Core.Tests/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Serialization.Tests/`, `tests/TuiVision.Compatibility.Tests/`, and `tests/TuiVision.Drivers.Tests/` until each gate assembly can reach `>= 70 %` line coverage.
- [ ] T023 [US2] Run `dotnet build --configuration Release` and `dotnet test`, then record build/test status plus skip-or-ignore outcomes in `Pflichtenheft.md` and `docs/porting-status.md`.
- [ ] T024 [P] [US2] Run assembly-specific Coverlet collection for `tests/TuiVision.Core.Tests/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Serialization.Tests/`, `tests/TuiVision.Compatibility.Tests/`, and `tests/TuiVision.Drivers.Tests/`, then publish the separated five-assembly results in `docs/porting-status.md` and `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`.
- [ ] T025 [US2] Compare local coverage outputs with repository-visible CI evidence and record the authoritative result plus any resolved discrepancy in `Pflichtenheft.md`, `docs/porting-status.md`, and `specs/006-close-phase8-gate/quickstart.md`.
- [ ] T026 [P] [US2] Run `dotnet format --verify-no-changes` and conditional `docfx docfx.json`, then record PASS or explicit N/A in `Pflichtenheft.md` and `docs/porting-status.md`.
- [ ] T027 [P] [US2] Refresh compatibility evidence in `docs/guides/multi-mac-workflow.md` for `MacBook Air M2`, `Mac mini M4 Pro`, and, when materially required, Linux and Windows/WSL.
- [ ] T028 [US2] Sync the final six-criteria evidence package across `Pflichtenheft.md`, `docs/porting-status.md`, and `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md` so reviewers see one consistent gate state.

**Checkpoint**: User Story 2 is complete when build, test, format, coverage, API-doc, and platform evidence are all reviewable from repository files without oral handover.

---

## Phase 5: User Story 3 - Keep Phase 8 blocked until closure is explicit (Priority: P3)

**Goal**: Make the repository say clearly whether example wave 1 may start, and tie that decision to one dedicated closure commit.

**Independent Test**: Read `Pflichtenheft.md`, `docs/porting-status.md`, `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`, and the 006 quickstart/contract files and verify that wave-1 readiness is obvious in one pass.

### Implementation for User Story 3

- [ ] T029 [US3] Update `Pflichtenheft.md` so the six entrance-gate criteria, the `>>> NAECHSTER SCHRITT <<<` marker, and the example-wave block/unblock wording match the finished Phase-8 gate state.
- [ ] T030 [US3] Record the final gate decision and dedicated closure-commit reference in `Pflichtenheft.md`, `docs/porting-status.md`, and `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`.
- [ ] T031 [US3] Add a final closure summary in `specs/006-close-phase8-gate/quickstart.md` and `specs/006-close-phase8-gate/contracts/phase-8-gate-contract.md` that points reviewers to the authoritative gate-closure proof surfaces.

**Checkpoint**: User Story 3 is complete when the repository explicitly states whether Phase 8 may start and names the commit that closed the gate.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Finish the cross-story cleanup and readiness checks around the closed gate.

- [ ] T032 [P] Refresh `docs/project-statistics.md` with the 006 implementation window, code/test/doc deltas, and conservative manual-effort baseline after the gate work lands.
- [ ] T033 [P] Re-run the review flow in `specs/006-close-phase8-gate/checklists/gate-docs.md` and `specs/006-close-phase8-gate/quickstart.md` against the final repository state.
- [ ] T034 Remove temporary coverage/debug scaffolding from `tests/` and `docs/` while preserving only the repository-visible evidence required for the gate-closure claim.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; starts immediately.
- **Foundational (Phase 2)**: Depends on Setup and blocks all user-story work.
- **User Story 1 (Phase 3)**: Depends on Foundational; establishes the final `M-07` proof state.
- **User Story 2 (Phase 4)**: Depends on User Story 1 because the evidence package cannot be finalized while provisional ledger rows remain.
- **User Story 3 (Phase 5)**: Depends on User Story 2 because the explicit gate decision must point to the finished evidence package.
- **Polish (Phase 6)**: Depends on the desired user stories being complete.

### User Story Dependencies

- **User Story 1 (P1)**: First deliverable and the MVP. It closes the remaining `M-07` proof gap but does not yet open Phase 8 by itself.
- **User Story 2 (P2)**: Depends on User Story 1. It packages the final build/test/coverage/format/doc/platform evidence.
- **User Story 3 (P3)**: Depends on User Story 2. It turns the proof package into an explicit allow/block decision for example wave 1.

### Within Each User Story

- Failing tests first, implementation second, ledger/evidence updates third.
- `docs/porting-status.md` must not be finalized before the backing tests or rationale exist.
- Coverage publication happens only after the underlying module tests are green.
- The dedicated gate-closure commit is the last functional step of User Story 3.

### Parallel Opportunities

- `T005`, `T006`, and `T007` can proceed in parallel after `T004`.
- In User Story 1, `T008` to `T011` can run in parallel, and the implementation clusters `T012` to `T018` can be split across multiple developers once the tests exist.
- In User Story 2, `T024`, `T026`, and `T027` can run in parallel after `T023` stabilizes the repository.
- In Polish, `T032` and `T033` can run in parallel.

---

## Parallel Example: User Story 1

```bash
# Launch the failing proof tests in parallel:
Task: "T008 Add failing Core proof tests under tests/TuiVision.Core.Tests/"
Task: "T009 Add failing Controls proof tests under tests/TuiVision.Controls.Tests/"
Task: "T010 Add failing Serialization proof tests in tests/TuiVision.Serialization.Tests/"
Task: "T011 Add failing Compatibility and driver-input proof tests under tests/TuiVision.Compatibility.Tests/ and tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs"

# Once the tests exist, split the implementation clusters:
Task: "T012 Implement the planned collection and string family under src/TuiVision.Core/"
Task: "T013 Implement config, initialization, window, and file-info types under src/TuiVision.Controls/"
Task: "T017 Complete stream, help, and resource behavior in src/TuiVision.Serialization/"
Task: "T018 Expand src/TuiVision.Compatibility/ with concrete compatibility support files"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational guardrails.
3. Complete Phase 3: User Story 1.
4. Stop and validate that `docs/porting-status.md` has no provisional rows and that the targeted module proof suites pass.

### Incremental Delivery

1. Setup + Foundational: establish the hard failing guardrails.
2. User Story 1: close `M-07` row-by-row and module-by-module.
3. User Story 2: package the six gate criteria with authoritative evidence.
4. User Story 3: publish the explicit allow/block decision and the dedicated closure commit.
5. Polish: clean up temporary scaffolding and refresh the statistics/review pass.

### Parallel Team Strategy

With multiple developers:

1. Complete Setup and Foundational together.
2. Split User Story 1 by module cluster:
   - Developer A: `src/TuiVision.Core/` plus `tests/TuiVision.Core.Tests/`
   - Developer B: `src/TuiVision.Controls/` plus `tests/TuiVision.Controls.Tests/`
   - Developer C: `src/TuiVision.Serialization/`, `src/TuiVision.Compatibility/`, `tests/TuiVision.Serialization.Tests/`, and `tests/TuiVision.Compatibility.Tests/`
   - Developer D: `src/TuiVision.Drivers.Console/`, `tests/TuiVision.Drivers.Tests/`, and `docs/porting-status.md`
3. Rejoin for the evidence-package and explicit gate-closure phases.

---

## Notes

- `[P]` means the task can be worked independently in different files after its dependencies are satisfied.
- The task order intentionally mirrors the clarified gate rules: no placeholder-only gate module, no aggregated-only coverage claim, and no unresolved local-vs-CI conflict at closure time.
- If `TuiVision.Compatibility` still cannot reach 70% through shared suites, keep `tests/TuiVision.Compatibility.Tests/` as a permanent dedicated test project instead of forcing artificial coverage through unrelated tests.
- The 25 mandatory original examples remain blocked until `T030` is complete and the closure commit exists.
