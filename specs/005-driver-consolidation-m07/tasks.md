# Tasks: Driver Consolidation and M-07 Porting Proof

**Input**: Design documents from `/specs/005-driver-consolidation-m07/`
**Prerequisites**: [plan.md](plan.md), [spec.md](spec.md), [research.md](research.md), [data-model.md](data-model.md), [contracts/phase-7-proof-contract.md](contracts/phase-7-proof-contract.md), [quickstart.md](quickstart.md)

**Tests**: Tests are mandatory for behavior-changing work in this feature. Driver-consolidation tasks must follow Red-Green-Refactor with failing MSTest coverage first. Documentation-ledger work must still include executable or reviewable validation where the proof contract can be checked objectively.

**Organization**: Tasks are grouped by user story so that Phase 7 driver consolidation, the `M-07` proof ledger, and the Phase-8 entrance-gate packaging remain independently understandable and reviewable.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete work)
- **[Story]**: Maps the task to one user story (`[US1]`, `[US2]`, `[US3]`)
- Every task includes an exact repository file path

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Prepare the driver test and proof-ledger scaffolding used by all later stories.

- [X] T001 Split the current baseline driver tests out of `tests/TuiVision.Drivers.Tests/Test1.cs` into a dedicated `tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs`
- [X] T002 [P] Add shared Phase-7 validation helpers for driver and ledger work in `tests/TuiVision.Drivers.Tests/Phase7DriverTestContext.cs`
- [X] T003 [P] Create the initial proof-ledger document skeleton in `docs/porting-status.md`

**Checkpoint**: Shared driver-test structure and the repository-local proof-ledger file exist.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish the inventory and validation foundations that block all user stories.

**CRITICAL**: No user-story work should begin until this phase is complete.

- [X] T004 [P] Add failing inventory and schema validation tests for `docs/porting-status.md` in `tests/TuiVision.Drivers.Tests/PortingStatusLedgerTests.cs`
- [X] T005 [P] Add failing capability-bucket and managed-replacement regression tests in `tests/TuiVision.Drivers.Tests/TConsoleDriverConsolidationTests.cs`
- [X] T006 Build the complete historical `.cc` inventory baseline, including associated `.h`/`.c` context notes, in `docs/porting-status.md`
- [X] T007 Define the canonical Phase-7 capability buckets and their managed target areas in `docs/porting-status.md`

**Checkpoint**: The formal proof scope, row schema, and capability vocabulary are fixed and reviewable.

---

## Phase 3: User Story 1 - Close the driver-consolidation gap (Priority: P1) 🎯 MVP

**Goal**: Consolidate the remaining historical driver responsibilities into one managed console-driver baseline without native dependencies.

**Independent Test**: Run the driver-focused MSTest suite and verify that the managed baseline now covers or consciously replaces the remaining historical screen, keyboard, mouse, and display-adaptation responsibilities without undocumented gaps.

### Tests for User Story 1

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [X] T008 [P] [US1] Add failing resize, presentation, and state-transition coverage in `tests/TuiVision.Drivers.Tests/TConsoleDriverConsolidationTests.cs`
- [X] T009 [P] [US1] Add failing compatibility-caveat and managed-replacement coverage in `tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs`

### Implementation for User Story 1

- [X] T010 [US1] Expand the managed driver baseline and supporting documentation in `src/TuiVision.Drivers.Console/TConsoleDriver.cs`
- [X] T011 [P] [US1] Retire the placeholder-only role of `src/TuiVision.Drivers.Console/Class1.cs` and move any real support type into a purpose-named file such as `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs`
- [X] T012 [US1] Record the driver-consolidation decisions for the historical platform families in `docs/porting-status.md`
- [X] T013 [US1] Capture the required compatibility-evidence workflow for `MacBook Air M2`, `Mac mini M4 Pro`, Linux, and Windows/WSL in `docs/guides/multi-mac-workflow.md`

**Checkpoint**: User Story 1 is independently reviewable as the Phase-7 MVP.

---

## Phase 4: User Story 2 - Prove M-07 completeness with a mapping ledger (Priority: P2)

**Goal**: Deliver `docs/porting-status.md` as the canonical, complete, and objectively reviewable `M-07` proof ledger.

**Independent Test**: Open `docs/porting-status.md`, sample shared and platform-specific rows, and confirm via automated checks plus review that every historical `.cc` file appears exactly once with one primary target, allowed status, evidence, rationale, and materially relevant `.h`/`.c` references where needed.

### Tests for User Story 2

- [X] T014 [P] [US2] Extend the ledger validation tests for primary/secondary target rules and canonical statuses in `tests/TuiVision.Drivers.Tests/PortingStatusLedgerTests.cs`
- [X] T015 [P] [US2] Add failing coverage for associated support-file references and zero-gap completeness checks in `tests/TuiVision.Drivers.Tests/PortingStatusCompletenessTests.cs`

### Implementation for User Story 2

- [X] T016 [US2] Populate all shared non-driver framework rows with target mapping, status, evidence, and rationale in `docs/porting-status.md`
- [X] T017 [US2] Populate all platform-specific driver rows for `dos/`, `linux/`, `unix/`, `qnx4/`, `qnxrtp/`, `wingr/`, `winnt/`, and `x11/` in `docs/porting-status.md`
- [X] T018 [US2] Add the materially relevant ancillary-support references for `tv203s/contrib/tvision/classes/dos/vgastate.h`, `tv203s/contrib/tvision/classes/dos/vgaregs.h`, `tv203s/contrib/tvision/classes/dos/vgastate.c`, and `tv203s/contrib/tvision/classes/dos/vgaregs.c` in `docs/porting-status.md`
- [X] T019 [US2] Record explicit evidence references for driver tests, manual compatibility runs, and documented replacement decisions in `docs/porting-status.md`

**Checkpoint**: User Story 2 closes the formal `M-07` proof artifact without relying on repository archaeology.

---

## Phase 5: User Story 3 - Prepare the Phase-8 entrance gate (Priority: P3)

**Goal**: Package the finished Phase-7 consolidation and `M-07` proof so the remaining Phase-8 gate work is explicit, bounded, and trainee-readable.

**Independent Test**: Compare the resulting artifacts against the Pflichtenheft gate language and verify that Phase 7 is closed as a framework step while all still-open build, test, coverage, and API-documentation gates are named explicitly rather than implied.

### Validation for User Story 3

- [X] T020 [P] [US3] Add a review checklist for Phase-7-vs.-Phase-8 gate separation in `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`
- [X] T021 [P] [US3] Add a quickstart validation pass for the finished proof package in `specs/005-driver-consolidation-m07/quickstart.md`

### Implementation for User Story 3

- [X] T022 [US3] Document the explicit remaining Phase-8 entrance-gate follow-up items in `docs/porting-status.md`
- [X] T023 [US3] Align the Phase-7 completion wording and `>>> NAECHSTER SCHRITT <<<` context in `Pflichtenheft.md`
- [X] T024 [US3] Sync the feature outcome and execution guidance in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md` if the active-work guidance changed

**Checkpoint**: User Story 3 leaves a clean hand-off from framework completion to later example-port gating.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Run the mandatory quality gates, close documentation updates, and refresh repository statistics.

- [X] T025 [P] Run and record `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Drivers.Tests/`, and `dotnet test` using `specs/005-driver-consolidation-m07/quickstart.md`
- [X] T026 [P] Run and record `dotnet format --verify-no-changes` and `dotnet test --collect:"XPlat Code Coverage"` using `specs/005-driver-consolidation-m07/quickstart.md`
- [X] T027 Run and record compatibility evidence for `MacBook Air M2`, `Mac mini M4 Pro`, Linux, and Windows/WSL in `docs/guides/multi-mac-workflow.md` and `docs/porting-status.md`
- [X] T028 [P] Run `docfx docfx.json` if `src/TuiVision.Drivers.Console/` public APIs or XML comments changed, and record the result in `specs/005-driver-consolidation-m07/quickstart.md`
- [X] T029 [P] Update `docs/project-statistics.md` with the branch/phase scope, documentation growth, observable work window, and manual-effort baseline for `005-driver-consolidation-m07`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; can start immediately.
- **Foundational (Phase 2)**: Depends on Setup; blocks all user-story work.
- **User Story 1 (Phase 3)**: Depends on Foundational; serves as the Phase-7 MVP.
- **User Story 2 (Phase 4)**: Depends on Foundational and reuses the consolidation decisions from US1.
- **User Story 3 (Phase 5)**: Depends on US1 and US2 because it packages the finished proof and remaining gate follow-up.
- **Polish (Phase 6)**: Depends on all desired user stories being complete.

### User Story Dependencies

- **US1 (P1)**: Starts after Foundational; no dependency on later stories.
- **US2 (P2)**: Starts after Foundational but should follow the capability decisions settled in US1.
- **US3 (P3)**: Starts only after the Phase-7 implementation and the `M-07` ledger are materially complete.

### Within Each User Story

- Tests and validation tasks MUST be written first and fail or identify a real gap before implementation proceeds.
- The required Red -> Green -> Refactor sequence must remain visible in commit history; do not collapse failing tests, implementation, and refactor into one combined commit.
- `docs/porting-status.md` is a first-class deliverable, not a polish-only afterthought.
- Associated `.h`/`.c` support files remain context-only, but materially relevant ones must be visible from the affected `.cc` rows.
- Do not treat example-porting work as part of this feature; it remains blocked behind the later Phase-8 entrance decision.

### Parallel Opportunities

- In Setup, `T002` and `T003` can run in parallel after `T001`.
- In Foundational, `T004` and `T005` can run in parallel; `T006` and `T007` can then proceed together on the shared ledger file.
- In US1, `T008` and `T009` can run in parallel before implementation; `T012` and `T013` can proceed in parallel once the core driver decisions are stable.
- In US2, `T014` and `T015` can run in parallel; `T016` and `T017` can be split by shared vs. platform-specific inventories.
- In US3, `T020` and `T021` can run in parallel; `T023` and `T024` can run in parallel if shared agent guidance is affected.
- In Polish, `T025`, `T026`, `T028`, and `T029` can run in parallel once implementation is complete; `T027` depends on the actual compatibility runs.

---

## Parallel Example: User Story 1

```bash
Task: "Add failing resize, presentation, and state-transition coverage in tests/TuiVision.Drivers.Tests/TConsoleDriverConsolidationTests.cs"
Task: "Add failing compatibility-caveat and managed-replacement coverage in tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs"
```

## Parallel Example: User Story 2

```bash
Task: "Extend the ledger validation tests for primary/secondary target rules and canonical statuses in tests/TuiVision.Drivers.Tests/PortingStatusLedgerTests.cs"
Task: "Add failing coverage for associated support-file references and zero-gap completeness checks in tests/TuiVision.Drivers.Tests/PortingStatusCompletenessTests.cs"
Task: "Populate all shared non-driver framework rows with target mapping, status, evidence, and rationale in docs/porting-status.md"
Task: "Populate all platform-specific driver rows for dos/, linux/, unix/, qnx4/, qnxrtp/, wingr/, winnt/, and x11/ in docs/porting-status.md"
```

## Parallel Example: User Story 3

```bash
Task: "Add a review checklist for Phase-7-vs.-Phase-8 gate separation in specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md"
Task: "Add a quickstart validation pass for the finished proof package in specs/005-driver-consolidation-m07/quickstart.md"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational.
3. Complete Phase 3: User Story 1.
4. **STOP and VALIDATE**: Run the driver-focused MSTest suite and review the consolidation decisions.
5. Use the result as the framework-completion MVP for Phase 7 before filling the full proof ledger.

### Incremental Delivery

1. Finish Setup + Foundational once.
2. Add US1 and validate the managed driver consolidation baseline.
3. Add US2 and validate the formal `M-07` ledger against the historical inventory.
4. Add US3 and validate the Phase-8 entrance-gate packaging.
5. Finish with Phase 6 quality gates, compatibility evidence, and repository statistics.

### Parallel Team Strategy

1. One developer sets up the shared test and ledger scaffolding.
2. After Foundational:
   - Developer A: US1 managed driver consolidation
   - Developer B: US2 ledger validation and inventory population
   - Developer C: US3 gate-packaging artifacts once US1 and US2 stabilize
3. Finish with shared validation, compatibility evidence capture, and documentation sync.

---

## Notes

- The original Turbo Vision sources under `tv203s/` remain reference-only and must not be modified.
- `docs/porting-status.md` is both a documentation file and a formal acceptance artifact for `M-07`.
- Linux and Windows/WSL evidence is mandatory for this feature, even if it is still manual or semi-automated rather than a hard CI gate.
- The primary Multi-Mac evidence set must mention `MacBook Air M2` and `Mac mini M4 Pro` explicitly.
