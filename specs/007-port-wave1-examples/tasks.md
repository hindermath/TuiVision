# Tasks: Mandatory Example Wave 1 Ports

**Input**: Design documents from `/Users/thorstenhindermann/RiderProjects/TuiVision/specs/007-port-wave1-examples/`
**Prerequisites**: `plan.md` (required), `spec.md` (required for user stories), `research.md`, `data-model.md`, `contracts/`

**Tests**: Test tasks are mandatory for this behavior-changing example wave. Follow Red-Green-Refactor: add failing MSTest smoke coverage first, implement the minimum managed example behavior to turn the tests green, then re-run the relevant validation before advancing guides or tracking artifacts.

**Organization**: Tasks are grouped by user story so that each wave-1 example can be implemented, smoke-tested, documented, and reviewed as an independent increment.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on incomplete work)
- **[Story]**: Which user story this task belongs to (`[US1]`, `[US2]`, `[US3]`, `[US4]`)
- Every task names the concrete file or directory path that must be touched or reviewed

## Path Conventions

- Example applications live in `examples/`
- Framework modules remain in `src/`
- Example smoke tests live in `tests/TuiVision.Examples.SmokeTests/`
- Guides and tracking artifacts live in `docs/`, `Pflichtenheft.md`, and `specs/`

## Phase 1: Setup (Wave-1 Delivery Skeleton)

**Purpose**: Establish the example-project layout, branch-governance prerequisites, and source-traceability surfaces before behavior work starts.

- [ ] T001 Align numbered-branch version fields in `Directory.Build.props` for feature `007-port-wave1-examples` before any `dotnet build` or `dotnet test` run for this implementation slice.
- [ ] T002 Create the managed example project skeletons `examples/Desklogo/Desklogo.csproj`, `examples/MsgCls/MsgCls.csproj`, `examples/Tutorial/Tutorial.csproj`, and `examples/Videomode/Videomode.csproj`, and add them to `TuiVision.sln`.
- [ ] T003 [P] Replace the placeholder-only wave description in `examples/README.md` with source-traceable mappings for `desklogo`, `msgcls`, `tutorial`, and `videomode`, including the original `tv203s/contrib/tvision/examples/` folders and the intended managed launch identities.

---

## Phase 2: Foundational (Blocking Smoke and Launch Infrastructure)

**Purpose**: Install the shared smoke-test and launch infrastructure that every wave-1 example depends on.

**⚠️ CRITICAL**: No user-story implementation should start before these tasks are complete.

- [ ] T004 Replace the placeholder test in `tests/TuiVision.Examples.SmokeTests/Test1.cs` with shared wave-1 smoke-test infrastructure and create reusable helpers under `tests/TuiVision.Examples.SmokeTests/` for launch, behavior assertions, and clean-exit assertions.
- [ ] T005 [P] Configure `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj` to reference the new example projects under `examples/` and keep MSTest smoke execution CI-ready for the wave-1 suite.
- [ ] T006 [P] Add canonical user-facing entry-point files under `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, and `examples/Videomode/` so each example exposes a real launch surface plus a test-callable in-process seam that still exercises the real host contract.
- [ ] T007 [P] Create the documentation home `docs/guides/examples/` and establish the wave-1 guide file set `docs/guides/examples/desklogo.md`, `docs/guides/examples/msgcls.md`, `docs/guides/examples/tutorial.md`, and `docs/guides/examples/videomode.md`.

**Checkpoint**: The repository now has runnable example project skeletons, a shared smoke-test home, canonical launch seams, and the guide surface required by every story.

---

## Phase 3: User Story 1 - Launch a Minimal Desktop Example (Priority: P1) 🎯 MVP

**Goal**: Deliver `desklogo` as the smallest complete managed wave-1 example with startup, defining desktop behavior, smoke coverage, and guide support.

**Independent Test**: Run `dotnet run --project examples/Desklogo`, observe the desktop logo, then run `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Desklogo"` and confirm startup, defining behavior, and clean exit all pass.

### Tests for User Story 1 (MANDATORY for behavior changes) ⚠️

> **NOTE**: Write these tests first, verify they fail, and only then implement the managed example behavior.

- [ ] T008 [P] [US1] Add failing `desklogo` smoke scenarios in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs` that assert startup, static desktop-logo rendering, and documented clean exit.

### Implementation for User Story 1

- [ ] T009 [P] [US1] Implement the `desklogo` entry point and application host in `examples/Desklogo/Program.cs` and `examples/Desklogo/DesklogoApp.cs`.
- [ ] T010 [P] [US1] Implement the minimal desktop-logo rendering surface in `examples/Desklogo/DesklogoDesktop.cs` and any required example-local support files under `examples/Desklogo/`.
- [ ] T011 [US1] Write the didactic guide `docs/guides/examples/desklogo.md` with learning goal, prerequisites, startup flow, architecture hints, exercises, and explicit traceability back to `tv203s/contrib/tvision/examples/desklogo/`.
- [ ] T012 [US1] Re-run the focused `desklogo` smoke path in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs` and keep `examples/Desklogo/` green before moving to the next example.

**Checkpoint**: `desklogo` is independently runnable, smoke-covered, guide-covered, and traceable to its historical source.

---

## Phase 4: User Story 2 - Demonstrate Custom Message Handling (Priority: P2)

**Goal**: Deliver `msgcls` as a managed example that proves custom message routing on top of the accepted application shell.

**Independent Test**: Run `dotnet run --project examples/MsgCls`, perform the documented trigger action, and then run `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~MsgCls"` to confirm the observable custom-message outcome and clean shutdown.

### Tests for User Story 2 (MANDATORY for behavior changes) ⚠️

- [ ] T013 [P] [US2] Add failing `msgcls` smoke scenarios in `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs` that assert startup, the documented custom-message trigger, the visible routed outcome, repeated-trigger stability, and clean exit.

### Implementation for User Story 2

- [ ] T014 [P] [US2] Implement the `msgcls` entry point and host in `examples/MsgCls/Program.cs` and `examples/MsgCls/MsgClsApp.cs`.
- [ ] T015 [P] [US2] Implement the custom event/message classes and routed behavior in `examples/MsgCls/MsgClsWindow.cs`, `examples/MsgCls/MsgClsEvents.cs`, and related example-local files under `examples/MsgCls/`.
- [ ] T016 [US2] Write the didactic guide `docs/guides/examples/msgcls.md` with startup instructions, trigger flow, message-routing explanation, exercises, and traceability to `tv203s/contrib/tvision/examples/msgcls/`.
- [ ] T017 [US2] Re-run the focused `msgcls` smoke path in `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs` and keep `examples/MsgCls/` green before starting tutorial work.

**Checkpoint**: `msgcls` demonstrates user-defined message handling through a documented interaction and passes its standalone smoke flow.

---

## Phase 5: User Story 3 - Learn the Core Concepts Step by Step (Priority: P3)

**Goal**: Deliver the full 16-step `tutorial` family as one shared managed project with stable step tokens, individual smoke coverage, and one shared guide page with 16 sections.

**Independent Test**: Run `dotnet run --project examples/Tutorial -- tvguid01` and `dotnet run --project examples/Tutorial -- tvguid16`, then run `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Tutorial"` and confirm each of the 16 step-specific smoke scenarios passes independently.

### Tests for User Story 3 (MANDATORY for behavior changes) ⚠️

- [ ] T018 [P] [US3] Add failing smoke scenarios for `tvguid01` through `tvguid08` in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, asserting startup, the step-specific defining behavior, ordered discoverability, and clean exit for each token.
- [ ] T019 [P] [US3] Add failing smoke scenarios for `tvguid09` through `tvguid16` in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, asserting startup, the step-specific defining behavior, ordered discoverability, and clean exit for each token.

### Implementation for User Story 3

- [ ] T020 [P] [US3] Implement the shared tutorial entry point and token-selection host in `examples/Tutorial/Program.cs` and `examples/Tutorial/TutorialApp.cs`.
- [ ] T021 [P] [US3] Implement the shared tutorial-step contract and sequencing support in `examples/Tutorial/Steps/ITutorialStep.cs`, `examples/Tutorial/Steps/TutorialStepCatalog.cs`, and related files under `examples/Tutorial/Steps/`.
- [ ] T022 [P] [US3] Implement `tvguid01` through `tvguid08` in `examples/Tutorial/Steps/TvGuid01Step.cs`, `examples/Tutorial/Steps/TvGuid02Step.cs`, `examples/Tutorial/Steps/TvGuid03Step.cs`, `examples/Tutorial/Steps/TvGuid04Step.cs`, `examples/Tutorial/Steps/TvGuid05Step.cs`, `examples/Tutorial/Steps/TvGuid06Step.cs`, `examples/Tutorial/Steps/TvGuid07Step.cs`, and `examples/Tutorial/Steps/TvGuid08Step.cs`.
- [ ] T023 [P] [US3] Implement `tvguid09` through `tvguid16` in `examples/Tutorial/Steps/TvGuid09Step.cs`, `examples/Tutorial/Steps/TvGuid10Step.cs`, `examples/Tutorial/Steps/TvGuid11Step.cs`, `examples/Tutorial/Steps/TvGuid12Step.cs`, `examples/Tutorial/Steps/TvGuid13Step.cs`, `examples/Tutorial/Steps/TvGuid14Step.cs`, `examples/Tutorial/Steps/TvGuid15Step.cs`, and `examples/Tutorial/Steps/TvGuid16Step.cs`.
- [ ] T024 [US3] Write the shared guide `docs/guides/examples/tutorial.md` with one clearly separated section for each token from `tvguid01` through `tvguid16`, including learning goal, startup path, expected outcome, architecture hints, exercises, and sequence context.
- [ ] T025 [US3] Re-run the full tutorial smoke suite in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs` until all 16 step-specific scenarios are green and the ordered sequence remains reviewable.

**Checkpoint**: `tutorial` exposes all 16 original steps through stable tokens, each step is independently smoke-covered, and the shared guide preserves the learning sequence.

---

## Phase 6: User Story 4 - Validate Display Mode Behavior Safely (Priority: P4)

**Goal**: Deliver `videomode` as a managed example that performs a real supported terminal transition where possible and an explicit visible fallback where not.

**Independent Test**: Run `dotnet run --project examples/Videomode`, invoke the documented transition action, and then run `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Videomode"` to confirm both the real-transition path and the visible-fallback path are covered.

### Tests for User Story 4 (MANDATORY for behavior changes) ⚠️

- [ ] T026 [P] [US4] Add failing `videomode` smoke scenarios in `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs` for the supported-transition path, the explicit visible-fallback path, post-transition usability, and clean exit.

### Implementation for User Story 4

- [ ] T027 [P] [US4] Implement the `videomode` entry point and host in `examples/Videomode/Program.cs` and `examples/Videomode/VideomodeApp.cs`.
- [ ] T028 [P] [US4] Implement terminal-capability detection, real-transition handling, and visible fallback behavior in `examples/Videomode/DisplayModeCoordinator.cs`, `examples/Videomode/VideomodeView.cs`, and related files under `examples/Videomode/`.
- [ ] T029 [US4] If `videomode` needs framework-visible capability plumbing, add only the minimum supporting changes in `src/TuiVision.Drivers.Console/` or `src/TuiVision.Compatibility/` required to keep the example managed, cross-platform, and non-simulated.
- [ ] T030 [US4] Write the didactic guide `docs/guides/examples/videomode.md` with runtime-capability notes, supported transition flow, explicit fallback explanation, exercises, and traceability to `tv203s/contrib/tvision/examples/videomode/`.
- [ ] T031 [US4] Re-run the focused `videomode` smoke path in `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs` until the real-transition and visible-fallback assertions are both green.

**Checkpoint**: `videomode` proves a real supported change where available, exposes a visible fallback otherwise, and remains usable after either outcome.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Finish the wave-level validation, tracking, and governance follow-through after all four example scopes are implemented.

- [ ] T032 [P] Run the wave-level validation commands `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Examples.SmokeTests/`, `dotnet test`, and `dotnet format --verify-no-changes`, and fix any remaining issues in `examples/`, `tests/TuiVision.Examples.SmokeTests/`, and touched `src/` files before updating tracking artifacts.
- [ ] T033 [P] If public APIs or XML comments changed in touched framework files under `src/`, run `docfx docfx.json` and update the affected XML-comment or documentation surfaces in `src/` and `docs/`.
- [ ] T034 [P] Record the delivered wave-1 status in `Pflichtenheft.md`, keeping the `>>> NAECHSTER SCHRITT <<<` marker and wave-1 checklist state explicit without leaking into later mandatory waves.
- [ ] T035 [P] Refresh `docs/project-statistics.md` with the 007 implementation window, example/smoke/doc deltas, and the conservative manual-effort baseline after the wave-1 implementation lands.
- [ ] T036 [P] Capture runtime validation evidence for `MacBook Air M2`, `Mac mini M4 Pro`, Linux, and Windows/WSL in `docs/guides/multi-mac-workflow.md`, covering the wave-1 example launch and `videomode` capability behavior.
- [ ] T037 [P] If wave-1 implementation changes shared agent guidance, active technologies, or project structure, synchronize `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` in the same work item.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies; starts immediately.
- **Foundational (Phase 2)**: Depends on Setup and blocks all user-story work.
- **User Story 1 (Phase 3)**: Depends on Foundational and delivers the MVP wave entry point.
- **User Story 2 (Phase 4)**: Depends on Foundational and should follow User Story 1 for the simplest visible rollout.
- **User Story 3 (Phase 5)**: Depends on Foundational and the shared smoke/launch contracts; it is safest after User Stories 1 and 2 establish the basic example pattern.
- **User Story 4 (Phase 6)**: Depends on Foundational and may depend on any minimum framework-visible runtime-capability support identified during implementation.
- **Polish (Phase 7)**: Depends on the desired user stories being complete.

### User Story Dependencies

- **User Story 1 (P1)**: First deliverable and the MVP. It proves the minimal managed example shape for wave 1.
- **User Story 2 (P2)**: Depends on the shared wave infrastructure and can proceed after User Story 1 establishes the example shell pattern.
- **User Story 3 (P3)**: Depends on the shared wave infrastructure and the stable tutorial token/host contract.
- **User Story 4 (P4)**: Depends on the shared wave infrastructure and any minimum console-capability support needed for real transitions.

### Within Each User Story

- Smoke tests must be written and failing before production code is added.
- Canonical entry point and host first, example-local behavior second, guide third, focused re-validation last.
- `tutorial` step implementation should preserve token identity and sequence clarity while keeping each step independently runnable.
- `videomode` must prove both supported-transition and explicit-fallback behavior before the story is considered done.

### Parallel Opportunities

- `T003`, `T005`, `T006`, and `T007` can proceed in parallel once the project skeleton exists.
- In User Story 3, `T018` and `T019` can run in parallel, and the step-implementation batches `T022` and `T023` can also run in parallel.
- In User Story 4, `T027` and `T028` can proceed in parallel once the failing smoke tests exist.
- In Polish, `T034`, `T035`, `T036`, and `T037` can run in parallel after the mandatory validation commands are green.

---

## Parallel Example: User Story 3

```bash
# Launch the failing tutorial smoke batches together:
Task: "T018 Add failing smoke scenarios for tvguid01 through tvguid08 in tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs"
Task: "T019 Add failing smoke scenarios for tvguid09 through tvguid16 in tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs"

# Then split the tutorial implementation by step batch:
Task: "T022 Implement tvguid01 through tvguid08 in examples/Tutorial/Steps/"
Task: "T023 Implement tvguid09 through tvguid16 in examples/Tutorial/Steps/"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational smoke and launch infrastructure.
3. Complete Phase 3: User Story 1 (`desklogo`).
4. Stop and validate that `examples/Desklogo/`, `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`, and `docs/guides/examples/desklogo.md` together satisfy the MVP wave pattern.

### Incremental Delivery

1. Establish the wave skeleton and shared smoke infrastructure.
2. Deliver `desklogo` as the reference pattern.
3. Add `msgcls` as the message-routing example.
4. Add the full 16-step `tutorial` sequence with one shared guide.
5. Add `videomode` with real-transition and fallback behavior.
6. Finish with wave-level validation, tracking, compatibility evidence, and conditional governance/doc follow-through.

### Parallel Team Strategy

With multiple developers:

1. Complete Setup and Foundational together.
2. Split the story work after the shared infrastructure is ready:
   - Developer A: `examples/Desklogo/`, `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`, and `docs/guides/examples/desklogo.md`
   - Developer B: `examples/MsgCls/`, `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`, and `docs/guides/examples/msgcls.md`
   - Developer C: `examples/Tutorial/`, `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, and `docs/guides/examples/tutorial.md`
   - Developer D: `examples/Videomode/`, optional supporting changes in `src/TuiVision.Drivers.Console/` or `src/TuiVision.Compatibility/`, `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`, and `docs/guides/examples/videomode.md`
3. Rejoin for the wave-level validation and tracking updates.

---

## Notes

- `[P]` means the task can be worked independently in different files after its dependencies are satisfied.
- The task order keeps wave 1 inside the existing five-module framework boundary and avoids hidden later-wave dependencies.
- `tests/TuiVision.Examples.SmokeTests/` is treated as the canonical smoke-test home for this feature and should no longer remain a placeholder-only module-smoke surface.
- `tutorial` acceptance is not satisfied until all 16 step tokens are independently runnable, smoke-covered, and documented on the shared guide page.
- `Pflichtenheft.md` and `docs/project-statistics.md` remain pre-wave baseline surfaces until the implementation tasks in this file land.
