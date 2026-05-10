# Tasks: Interactive Wave 2 Demos

**Input**: Design documents from `specs/012-interactive-wave2-demos/`
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, `contracts/interactive-wave2-demo-acceptance.md`, `quickstart.md`
**Feature Branch**: `012-interactive-wave2-demos`

**Tests**: Required. The feature specification requires deterministic smoke scenarios that drive `app.Run()` or the real application loop with injected `TEvent`, command, or key events for every Wave-2 example.

**Organization**: Tasks are grouped by user story so the broad `Demo` vertical slice can be delivered first, the remaining runtime examples can follow by family, the smoke proof can be hardened as its own increment, and learner-facing documentation can be completed independently once behavior is stable.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel because it touches different files or only appends independent evidence.
- **[Story]**: User story label for story phases only.
- Every task names the target file path or evidence file path.

## Phase 1: Setup (Shared Baseline)

**Purpose**: Confirm the 012 workspace, capture the existing 011 proof baseline, and create the evidence surface before implementation starts.

- [ ] T001 Record `.specify/scripts/bash/check-prerequisites.sh --json --paths-only` output and current branch in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T002 Record `dotnet restore` result and the no-new-runtime-dependency baseline in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T003 [P] Review the 011 proof baseline in `specs/011-port-wave2-examples/pr-evidence.md` and list reusable direct proof helpers in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T004 [P] Review existing Wave-2 smoke classes under `tests/TuiVision.Examples.SmokeTests/` and record which methods currently rely on direct helpers in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T005 Create the initial per-example evidence matrix in `specs/012-interactive-wave2-demos/pr-evidence.md` with rows for `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Complete the read-only historical source review, shared smoke-event support, command-id plan, and safety checks that all user stories depend on.

**CRITICAL**: No user-story implementation should start until this phase records the historical source review and smoke-event foundation.

- [ ] T006 [P] Record the `Clipboard` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/clipboard/test.cc` and `tv203s/contrib/tvision/include/tv/osclipboard.h`
- [ ] T007 [P] Record the `Demo` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/demo/tvdemo1.cc`, `tv203s/contrib/tvision/examples/demo/tvdemo2.cc`, `tv203s/contrib/tvision/examples/demo/tvdemo3.cc`, `tv203s/contrib/tvision/examples/demo/tvdemo.h`, `tv203s/contrib/tvision/examples/demo/tvcmds.h`, `tv203s/contrib/tvision/examples/demo/gadgets.cc`, `tv203s/contrib/tvision/examples/demo/fileview.cc`, `tv203s/contrib/tvision/examples/demo/ascii.cc`, and `tv203s/contrib/tvision/examples/demo/calendar.cc`
- [ ] T008 [P] Record the `DlgDsn` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/dlgdsn/freedsgn.cc`, `tv203s/contrib/tvision/examples/dlgdsn/dsgobjs.cc`, `tv203s/contrib/tvision/examples/dlgdsn/propdlgs.cc`, `tv203s/contrib/tvision/examples/dlgdsn/propedit.cc`, `tv203s/contrib/tvision/examples/dlgdsn/strmoper.cc`, `tv203s/contrib/tvision/examples/dlgdsn/dsgdata.h`, and `tv203s/contrib/tvision/examples/dlgdsn/dsgobjs.h`
- [ ] T009 [P] Record the `DynTxt` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/dyntxt/dyntext.cpp`, `tv203s/contrib/tvision/examples/dyntxt/testdyn.cpp`, and `tv203s/contrib/tvision/examples/dyntxt/dyntext.h`
- [ ] T010 [P] Record the `InpLis` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/inplis/inplist.cpp`, `tv203s/contrib/tvision/examples/inplis/test.cpp`, and `tv203s/contrib/tvision/examples/inplis/inplist.h`
- [ ] T011 [P] Record the `ListVi` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/listvi/lst_view.cpp`, `tv203s/contrib/tvision/examples/listvi/listbox2.cpp`, `tv203s/contrib/tvision/examples/listvi/lst_view.h`, and `tv203s/contrib/tvision/classes/tlistvie.cc`
- [ ] T012 [P] Record the `ProgBa` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/progba/example.cpp`, `tv203s/contrib/tvision/examples/progba/tprogbar.cpp`, `tv203s/contrib/tvision/examples/progba/tprogbar.h`, `tv203s/contrib/tvision/examples/progba/makerez.cpp`, and `tv203s/contrib/tvision/examples/progba/readrez.cpp`
- [ ] T013 [P] Record the `Sdlg` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/sdlg/main.cpp`, `tv203s/contrib/tvision/examples/sdlg/scrldlg.cpp`, `tv203s/contrib/tvision/examples/sdlg/scrlgrp.cpp`, and `tv203s/contrib/tvision/examples/sdlg/dlg.h`
- [ ] T014 [P] Record the `Sdlg2` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/sdlg2/main.cpp`, `tv203s/contrib/tvision/examples/sdlg2/scrldlg.cpp`, `tv203s/contrib/tvision/examples/sdlg2/scrlgrp.cpp`, and `tv203s/contrib/tvision/examples/sdlg2/dlg.h`
- [ ] T015 [P] Record the `TCombo` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/tcombo/test.cpp`, `tv203s/contrib/tvision/examples/tcombo/tcombobx.cpp`, `tv203s/contrib/tvision/examples/tcombo/tcombobx.h`, `tv203s/contrib/tvision/examples/tcombo/tcmbovwr.cpp`, `tv203s/contrib/tvision/examples/tcombo/tcmbowin.cpp`, `tv203s/contrib/tvision/examples/tcombo/tsinputl.cpp`, and `tv203s/contrib/tvision/examples/tcombo/tsinputl.h`
- [ ] T016 [P] Record the `TProgB` historical source review in `specs/012-interactive-wave2-demos/pr-evidence.md` using `tv203s/contrib/tvision/examples/tprogb/calc.cpp`, `tv203s/contrib/tvision/examples/tprogb/tprogbar.cpp`, and `tv203s/contrib/tvision/examples/tprogb/tprogbar.h`
- [ ] T017 Add a shared scripted event helper for queued command/key events and deterministic quit handling in `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`
- [ ] T018 Update shared smoke assertions to verify visible app-loop feedback and direct-helper usage classification in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs`
- [ ] T019 Define reserved example-local command IDs and command labels for the eleven examples in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T020 Audit read-only fixture and file/path proof boundaries for `examples/Demo/DemoApp.cs`, `examples/DlgDsn/DlgDsnApp.cs`, and `examples/DlgDsn/Fixtures/valid.tvdialog` in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T021 Create the initial validation evidence section for smoke, full test, coverage, format, DocFX, and A11Y commands in `specs/012-interactive-wave2-demos/pr-evidence.md`

**Checkpoint**: Historical source review, smoke-event helper, command plan, fixture safety baseline, and evidence matrix are ready.

---

## Phase 3: User Story 1 - Demo as the visible vertical slice (Priority: P1) MVP

**Goal**: `examples/Demo` starts as a meaningful interactive application, exposes at least three visible commands, and proves each command path through the app loop.

**Independent Test**: Run `dotnet run --project examples/Demo`, trigger at least three visible commands, and run `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Demo"` to verify the scripted app-loop path.

### Tests for User Story 1

- [ ] T022 [US1] Add failing app-loop smoke coverage for Demo menu/command discovery and three visible result states in `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs`
- [ ] T023 [US1] Add failing app-loop smoke coverage for Demo file/path metadata, cancel, invalid path, color/display, and omission feedback in `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs`

### Implementation for User Story 1

- [ ] T024 [US1] Add visible first-screen purpose text, menu commands, status-line command hints, and deterministic command dispatch to `examples/Demo/DemoApp.cs`
- [ ] T025 [US1] Route Demo broad controls/dialog/gadget, file/path metadata, cancel/invalid, color/display, and omission flows through visible command handlers in `examples/Demo/DemoApp.cs`
- [ ] T026 [US1] Add deterministic injected-event sequencing for Demo headless mode without bypassing the runtime dispatch route in `examples/Demo/DemoApp.cs`
- [ ] T027 [US1] Ensure normal startup remains usable through `dotnet run --project examples/Demo` by reviewing `examples/Demo/Program.cs`
- [ ] T028 [US1] Record Demo command paths, visible feedback, historical-source deviations, and smoke results in `specs/012-interactive-wave2-demos/pr-evidence.md`

**Checkpoint**: Demo is a complete, independently testable vertical slice.

---

## Phase 4: User Story 2 - Every Wave-2 example has a real operation path (Priority: P1)

**Goal**: All eleven Wave-2 examples start with a meaningful first screen and expose at least one visible menu, key, status, or command path with text-first feedback.

**Independent Test**: Start each example with `dotnet run --project examples/<Example>` and verify purpose text, an operation path, a visible feedback state, and a deterministic quit path.

### Tests for User Story 2

- [ ] T029 [P] [US2] Add failing app-loop smoke coverage for Clipboard copy, cut, paste, and unavailable feedback in `tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs`
- [ ] T030 [P] [US2] Add failing app-loop smoke coverage for DlgDsn load, render, visible change, malformed rejection, and invalid-description rejection in `tests/TuiVision.Examples.SmokeTests/DlgDsnSmokeTests.cs`
- [ ] T031 [P] [US2] Add failing app-loop smoke coverage for DynTxt short, long, and constrained-width feedback in `tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs`
- [ ] T032 [P] [US2] Add failing app-loop smoke coverage for InpLis input, selection, history/recall, boundary, and empty feedback in `tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs`
- [ ] T033 [P] [US2] Add failing app-loop smoke coverage for ListVi selection, first/last boundary, and empty feedback in `tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs`
- [ ] T034 [P] [US2] Add failing app-loop smoke coverage for ProgBa visible progress completion in `tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs`
- [ ] T035 [P] [US2] Add failing app-loop smoke coverage for Sdlg vertical scroll/focus movement outside the initial viewport in `tests/TuiVision.Examples.SmokeTests/SdlgSmokeTests.cs`
- [ ] T036 [P] [US2] Add failing app-loop smoke coverage for Sdlg2 horizontal and vertical scroll/focus movement outside the initial viewport in `tests/TuiVision.Examples.SmokeTests/Sdlg2SmokeTests.cs`
- [ ] T037 [P] [US2] Add failing app-loop smoke coverage for TCombo selection, visible value change, boundary, and empty feedback in `tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs`
- [ ] T038 [P] [US2] Add failing app-loop smoke coverage for TProgB partial progress, abort, and cancelled feedback in `tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs`

### Implementation for User Story 2

- [ ] T039 [P] [US2] Add visible first-screen purpose text, menu/status command paths, and command dispatch for copy, cut, paste, and unavailable feedback in `examples/Clipboard/ClipboardApp.cs`
- [ ] T040 [P] [US2] Add visible first-screen purpose text, dialog-description command paths, render/change/reject feedback, and read-only fixture handling in `examples/DlgDsn/DlgDsnApp.cs`
- [ ] T041 [P] [US2] Add visible first-screen purpose text, short/long/constrained command paths, and text-first feedback in `examples/DynTxt/DynTxtApp.cs`
- [ ] T042 [P] [US2] Add visible first-screen purpose text, input/list/history command paths, boundary feedback, and empty feedback in `examples/InpLis/InpLisApp.cs`
- [ ] T043 [P] [US2] Add visible first-screen purpose text, list selection command paths, boundary feedback, and empty feedback in `examples/ListVi/ListViApp.cs`
- [ ] T044 [P] [US2] Add visible first-screen purpose text, progress-start command path, completion feedback, and deterministic quit path in `examples/ProgBa/ProgBaApp.cs`
- [ ] T045 [P] [US2] Add visible first-screen purpose text, vertical scroll/focus command paths, boundary feedback, and deterministic quit path in `examples/Sdlg/SdlgApp.cs`
- [ ] T046 [P] [US2] Add visible first-screen purpose text, horizontal/vertical scroll/focus command paths, boundary feedback, and deterministic quit path in `examples/Sdlg2/Sdlg2App.cs`
- [ ] T047 [P] [US2] Add visible first-screen purpose text, combo selection/value command paths, boundary feedback, and empty feedback in `examples/TCombo/TComboApp.cs`
- [ ] T048 [P] [US2] Add visible first-screen purpose text, partial progress, abort, cancelled command paths, and separate visible result states in `examples/TProgB/TProgBApp.cs`
- [ ] T049 [US2] Review normal startup and terminal bounds handling for all Wave-2 `Program.cs` files under `examples/Clipboard/`, `examples/DlgDsn/`, `examples/DynTxt/`, `examples/InpLis/`, `examples/ListVi/`, `examples/ProgBa/`, `examples/Sdlg/`, `examples/Sdlg2/`, `examples/TCombo/`, and `examples/TProgB/`
- [ ] T050 [US2] Record per-example operation path, visible feedback state, and accepted historical deviation notes in `specs/012-interactive-wave2-demos/pr-evidence.md`

**Checkpoint**: All Wave-2 examples are visibly operable from normal startup.

---

## Phase 5: User Story 3 - Smoke tests verify visible application paths (Priority: P2)

**Goal**: The smoke suite proves the same visible operation paths that manual reviewers use and no longer counts direct helper calls as primary interaction proof.

**Independent Test**: Run `dotnet test tests/TuiVision.Examples.SmokeTests/` and confirm the evidence matrix maps every Wave-2 example to one primary app-loop smoke scenario.

### Tests for User Story 3

- [ ] T051 [US3] Add a Wave-2 interactive smoke matrix test that lists all eleven primary app-loop scenarios in `tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs`
- [ ] T052 [US3] Add direct-helper classification assertions for setup-only and supplemental-only helper usage in `tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs`

### Implementation for User Story 3

- [ ] T053 [US3] Convert `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs` so primary assertions follow the injected event path before using supplemental direct-helper assertions
- [ ] T054 [P] [US3] Convert `tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs` and `tests/TuiVision.Examples.SmokeTests/DlgDsnSmokeTests.cs` so primary assertions follow injected event paths before supplemental direct-helper assertions
- [ ] T055 [P] [US3] Convert `tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs` so primary assertions follow injected event paths before supplemental direct-helper assertions
- [ ] T056 [P] [US3] Convert `tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs` and `tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs` so primary assertions follow injected event paths before supplemental direct-helper assertions
- [ ] T057 [P] [US3] Convert `tests/TuiVision.Examples.SmokeTests/SdlgSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/Sdlg2SmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs` so primary assertions follow injected event paths before supplemental direct-helper assertions
- [ ] T058 [US3] Align `Directory.Build.props` build counter for this test run, run the fast example smoke suite with `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release`, and record output in `specs/012-interactive-wave2-demos/pr-evidence.md`

**Checkpoint**: Smoke proof is aligned with the visible runtime path for all eleven examples.

---

## Phase 6: User Story 4 - Guides describe the real operation paths (Priority: P2)

**Goal**: Learners and reviewers can follow the actual startup, command, feedback, and accessibility paths from German-first/English-second guides without reading internal test helpers.

**Independent Test**: Read each affected guide, follow the documented `dotnet run --project examples/<Example>` command path, and trace it to smoke/evidence entries in `pr-evidence.md`.

### Documentation for User Story 4

- [ ] T059 [P] [US4] Update Clipboard runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/clipboard.md`
- [ ] T060 [P] [US4] Update Demo runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/demo.md`
- [ ] T061 [P] [US4] Update DlgDsn runtime path, expected feedback, read-only fixture notes, A11Y notes, and historical-source outcome in `docs/guides/examples/dlgdsn.md`
- [ ] T062 [P] [US4] Update DynTxt runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/dyntxt.md`
- [ ] T063 [P] [US4] Update InpLis runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/inplis.md`
- [ ] T064 [P] [US4] Update ListVi runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/listvi.md`
- [ ] T065 [P] [US4] Update ProgBa runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/progba.md`
- [ ] T066 [P] [US4] Update Sdlg runtime path, expected feedback, scroll/focus A11Y notes, and historical-source outcome in `docs/guides/examples/sdlg.md`
- [ ] T067 [P] [US4] Update Sdlg2 runtime path, expected feedback, two-axis scroll/focus A11Y notes, and historical-source outcome in `docs/guides/examples/sdlg2.md`
- [ ] T068 [P] [US4] Update TCombo runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/tcombo.md`
- [ ] T069 [P] [US4] Update TProgB runtime path, expected feedback, A11Y notes, and historical-source outcome in `docs/guides/examples/tprogb.md`
- [ ] T070 [US4] Update Wave-2 overview, run commands, and learner-ready status in `examples/README.md`
- [ ] T071 [US4] Update the guide review notes for cross-example text-first and accessibility evidence in `docs/guides/examples/wave2-guide-review-notes.md`
- [ ] T072 [US4] Record guide-to-operation-to-smoke traceability for all eleven examples in `specs/012-interactive-wave2-demos/pr-evidence.md`

**Checkpoint**: Documentation matches the real interactive runtime paths.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Complete governance evidence, statistics, final validation, versioning, and delivery preparation.

- [ ] T073 [P] Verify architecture impact and update or record unchanged rationale for `docs/architecture/architecture-vision.md`, `docs/architecture/runtime-view.md`, `docs/architecture/quality-scenarios.md`, and `docs/architecture/architecture-risks.md`
- [ ] T074 [P] Verify NIST SSDF/CWE posture and update or record unchanged rationale for `docs/security/security-checklist.md`, `docs/security/threat-model.md`, `docs/security/dependency-audit.md`, `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`
- [ ] T075 [P] Update the 012 implementation completion status, next-step marker, and interactive Wave-2 readiness in `Pflichtenheft.md`
- [ ] T076 [P] Update project statistics for implementation scope, validation evidence, manual baseline, and acceleration notes in `docs/project-statistics.md`
- [ ] T077 [P] Review agent guidance parity and update or record unchanged rationale for `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [ ] T078 Align `Directory.Build.props` to the next branch version and record that the manual build counter must be incremented in `Directory.Build.props` before each final `dotnet build` or `dotnet test` command
- [ ] T079 Run `dotnet build --configuration Release` and record command output in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T080 Run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` and record command output in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T081 Run full `dotnet test --configuration Release` and record command output in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T082 Run coverage gate `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` and record per-assembly coverage evidence in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T083 Run `dotnet format --verify-no-changes` and record command output in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T084 Run `docfx docfx.json` and confirm generated `_site/` and generated `api/*.yml` remain uncommitted in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T085 Run `npm run test:docfx` from `tests/web-a11y/` and record Playwright/axe evidence in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T086 Run one manual startup check for each of the eleven examples, including the primary operation path, and record the `dotnet run --project examples/<Example>` results in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T087 Update the PR description with affected scripts/docs, final validation commands, sample console output for changed user-visible runtime output, and an explicit security-risk statement or unchanged-risk rationale
- [ ] T088 Run `git diff --check` and record the clean diff-check result in `specs/012-interactive-wave2-demos/pr-evidence.md`
- [ ] T089 Run `bash scripts/rename-lastenheft.sh Lastenheft_Interactive-Wave2-Demos.md 012-interactive-wave2-demos` or `pwsh scripts/rename-lastenheft.ps1 -File Lastenheft_Interactive-Wave2-Demos.md -BranchName 012-interactive-wave2-demos` as the final Polish step, and record the resulting `Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md` path in `specs/012-interactive-wave2-demos/pr-evidence.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies.
- **Foundational (Phase 2)**: Depends on Setup and blocks all user stories.
- **US1 Demo vertical slice (Phase 3)**: Depends on Foundational and should be completed first as MVP.
- **US2 Remaining runtime paths (Phase 4)**: Depends on Foundational; can start after US1 proves the shared pattern.
- **US3 Smoke proof hardening (Phase 5)**: Depends on US1 and US2 runtime command paths.
- **US4 Guides (Phase 6)**: Depends on the matching runtime path for each example; individual guide tasks can start as soon as that example is stable.
- **Polish (Phase 7)**: Depends on completed runtime, smoke, and documentation work.

### User Story Dependencies

- **US1 (P1)**: MVP. No dependency on other stories after Foundation.
- **US2 (P1)**: Depends on Foundation and should reuse the US1 interaction pattern.
- **US3 (P2)**: Depends on US1 and US2 because it verifies the completed visible paths.
- **US4 (P2)**: Depends on each example's stable behavior; can proceed per example after US1/US2 tasks land.

### Within Each User Story

- Write or update smoke tests before implementation where the task phase includes tests.
- Add visible first-screen purpose text before adding command-specific behavior.
- Route operations through menu, keyboard, status, or command dispatch before using direct helpers for supplemental assertions.
- Record each completed example in `pr-evidence.md` before moving to final validation.

## Parallel Opportunities

- T006-T016 can run in parallel because each historical source review writes an independent evidence row.
- T029-T038 can be prepared in parallel because they touch separate smoke-test files.
- T039-T048 can be implemented in parallel by example after the Demo pattern is proven.
- T059-T069 can be updated in parallel because each guide is a separate file.
- T073-T077 can run in parallel because each governance surface is independent.

## Parallel Example: User Story 2

```text
Task: "T039 [P] [US2] Add visible first-screen purpose text, menu/status command paths, and command dispatch for copy, cut, paste, and unavailable feedback in examples/Clipboard/ClipboardApp.cs"
Task: "T041 [P] [US2] Add visible first-screen purpose text, short/long/constrained command paths, and text-first feedback in examples/DynTxt/DynTxtApp.cs"
Task: "T047 [P] [US2] Add visible first-screen purpose text, combo selection/value command paths, boundary feedback, and empty feedback in examples/TCombo/TComboApp.cs"
```

## Parallel Example: User Story 4

```text
Task: "T059 [P] [US4] Update Clipboard runtime path, expected feedback, A11Y notes, and historical-source outcome in docs/guides/examples/clipboard.md"
Task: "T061 [P] [US4] Update DlgDsn runtime path, expected feedback, read-only fixture notes, A11Y notes, and historical-source outcome in docs/guides/examples/dlgdsn.md"
Task: "T069 [P] [US4] Update TProgB runtime path, expected feedback, A11Y notes, and historical-source outcome in docs/guides/examples/tprogb.md"
```

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 and Phase 2.
2. Complete Phase 3 for `examples/Demo`.
3. Validate Demo manually with `dotnet run --project examples/Demo`.
4. Validate Demo smoke proof with `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Demo"`.
5. Stop and review the pattern before applying it to the remaining examples.

### Incremental Delivery

1. Deliver `Demo` as the visible vertical slice.
2. Deliver remaining examples by family: Clipboard/text, dialog-designer, list/input/combo, progress, scroll dialogs.
3. Harden smoke coverage so every primary scenario uses injected runtime events.
4. Update guides and proof surfaces.
5. Run full validation and record evidence.

### Final Validation Commands

```bash
# Before each build/test command, align Directory.Build.props to the branch version
# and increment the manual build counter.
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
git diff --check
bash scripts/rename-lastenheft.sh Lastenheft_Interactive-Wave2-Demos.md 012-interactive-wave2-demos
```
