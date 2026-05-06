# Tasks: Port Wave 2 Examples

**Input**: Design documents from `/specs/011-port-wave2-examples/`
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`,
`contracts/wave2-example-acceptance.md`, `quickstart.md`
**Branch**: `011-port-wave2-examples`

**Tests**: Required. The specification requires deterministic in-process smoke
tests for every wave-2 example. Test tasks appear before implementation tasks.
Before every `dotnet build` or `dotnet test` command in any task, increment the
manual build counter in `Directory.Build.props` and keep `Version`,
`AssemblyVersion`, and `FileVersion` aligned with the numbered-branch scheme.

**Organization**: Tasks are grouped by setup/foundation, then by user story so
each story can be implemented and validated independently.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel because it touches different files or independent
  artifacts.
- **[Story]**: `US1`, `US2`, or `US3` from `spec.md`.
- Paths are repository-root relative.

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Establish the wave-2 project skeleton, registration points, and
source-review baseline shared by all stories.

- [ ] T001 Confirm the worktree is on branch `011-port-wave2-examples` and that
  `git status --short --branch` is clean before implementation begins.
- [ ] T002 Verify the installed Spec-Kit governance presets with
  `specify preset list`; confirm the all-six C#/.NET default or document an
  exception in `specs/011-port-wave2-examples/quickstart.md`. Also verify that
  the `RiderProjects/TuiVision` row in the Level-2 Project Environment Registry
  remains the binding runtime, build/test, A11Y, statistics, and agent-surface
  context for this feature.
- [ ] T003 Review original source files under
  `tv203s/contrib/tvision/examples/clipboard/`, `demo/`, `dlgdsn/`, `dyntxt/`,
  `inplis/`, `listvi/`, `progba/`, `sdlg/`, `sdlg2/`, `tcombo/`, and `tprogb/`;
  record any accepted historical limitations for later guide and risk tasks.
- [ ] T004 [P] Create project directory and skeleton files
  `examples/Clipboard/Clipboard.csproj`, `examples/Clipboard/Program.cs`, and
  `examples/Clipboard/ClipboardApp.cs`.
- [ ] T005 [P] Create project directory and skeleton files
  `examples/Demo/Demo.csproj`, `examples/Demo/Program.cs`, and
  `examples/Demo/DemoApp.cs`.
- [ ] T006 [P] Create project directory and skeleton files
  `examples/DlgDsn/DlgDsn.csproj`, `examples/DlgDsn/Program.cs`, and
  `examples/DlgDsn/DlgDsnApp.cs`.
- [ ] T007 [P] Create project directory and skeleton files
  `examples/DynTxt/DynTxt.csproj`, `examples/DynTxt/Program.cs`, and
  `examples/DynTxt/DynTxtApp.cs`.
- [ ] T008 [P] Create project directory and skeleton files
  `examples/InpLis/InpLis.csproj`, `examples/InpLis/Program.cs`, and
  `examples/InpLis/InpLisApp.cs`.
- [ ] T009 [P] Create project directory and skeleton files
  `examples/ListVi/ListVi.csproj`, `examples/ListVi/Program.cs`, and
  `examples/ListVi/ListViApp.cs`.
- [ ] T010 [P] Create project directory and skeleton files
  `examples/ProgBa/ProgBa.csproj`, `examples/ProgBa/Program.cs`, and
  `examples/ProgBa/ProgBaApp.cs`.
- [ ] T011 [P] Create project directory and skeleton files
  `examples/Sdlg/Sdlg.csproj`, `examples/Sdlg/Program.cs`, and
  `examples/Sdlg/SdlgApp.cs`.
- [ ] T012 [P] Create project directory and skeleton files
  `examples/Sdlg2/Sdlg2.csproj`, `examples/Sdlg2/Program.cs`, and
  `examples/Sdlg2/Sdlg2App.cs`.
- [ ] T013 [P] Create project directory and skeleton files
  `examples/TCombo/TCombo.csproj`, `examples/TCombo/Program.cs`, and
  `examples/TCombo/TComboApp.cs`.
- [ ] T014 [P] Create project directory and skeleton files
  `examples/TProgB/TProgB.csproj`, `examples/TProgB/Program.cs`, and
  `examples/TProgB/TProgBApp.cs`.
- [ ] T015 Add all eleven wave-2 projects to `TuiVision.sln`.
- [ ] T016 Add project references for all eleven wave-2 examples to
  `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj`.

**Checkpoint**: All wave-2 projects exist, are registered, and can be referenced
by the smoke-test project.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Shared headless smoke infrastructure and reusable evidence surfaces
that must exist before any user-story implementation is completed.

**Critical**: No user story can be accepted until this phase is complete.

- [ ] T017 Update `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` so
  its XML docs and helper names describe wave-1 and wave-2 examples, not only
  wave 1.
- [ ] T018 Add shared smoke helpers in
  `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` for visible-state
  assertions, boundary-input assertions, and text-first output assertions.
- [ ] T019 Create `docs/architecture/architecture-vision.md` with the wave-2
  example-readiness context, DE-first and EN-second if learner-facing content is
  included.
- [ ] T020 Create `docs/architecture/runtime-view.md` describing in-process
  headless smoke flows and normal console launch flows for wave-2 examples.
- [ ] T021 Create `docs/architecture/quality-scenarios.md` with quality
  scenarios for deterministic smoke tests, text-first operation, guide
  completeness, and no file-content I/O in standard dialogs.
- [ ] T022 Create `docs/architecture/architecture-risks.md` with accepted
  limitation and Historical Example Parity Cleanup records discovered in T003.
- [ ] T023 Review whether any new cross-cutting architecture decision needs an
  ADR under `docs/architecture/adr/`; create the ADR only if a new decision is
  introduced during implementation.
- [ ] T024 Update `docs/security/supply-chain-evidence.md` to record that no
  new NuGet dependency is planned, or document any justified dependency found
  during implementation; include the SBOM, VEX, SLSA/provenance, and releasable
  example-artifact applicability decision for the new executable examples.
- [ ] T025 Update `docs/security/zero-trust-applicability.md` to record that
  wave-2 local terminal examples do not introduce web/API/auth or remote service
  trust boundaries.
- [ ] T026 Update or reference `docs/security/asvs-verification.md` with the
  justified `OWASP ASVS` N/A decision for this feature.
- [ ] T027 Apply `NIST SSDF`, `CWE Top 25`, STRIDE, and CAPEC review notes for
  generated code and local terminal-example trust boundaries in
  `docs/security/security-checklist.md` and `docs/security/threat-model.md`.
- [ ] T028 Run `dotnet list package --outdated` and record dependency-currency
  evidence or N/A rationale in `docs/security/dependency-audit.md`; also update
  or explicitly mark N/A for `docs/security/arc42-security.md`,
  `docs/security/security-quality-scenarios.md`, and
  `docs/security/samm-assessment.md`.

**Checkpoint**: Shared smoke helpers and governance evidence are ready; story
work can proceed in parallel.

---

## Phase 3: User Story 1 - Run the core controls and dialogs demo set (Priority: P1) MVP

**Goal**: Deliver the broad controls/dialogs demo, dynamic dialog designer
proof, and historical scrollable dialog examples.

**Independent Test**: Run
`dotnet test tests/TuiVision.Examples.SmokeTests/ --filter
"FullyQualifiedName~Demo|FullyQualifiedName~DlgDsn|FullyQualifiedName~Sdlg|FullyQualifiedName~Sdlg2"`
and confirm visible example-specific behavior for `demo`, `dlgdsn`, `sdlg`,
and `sdlg2`.

### Tests for User Story 1

> Write these tests first and ensure they fail before implementation.

- [ ] T029 [P] [US1] Add `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs`
  covering startup, a broad controls/dialogs/gadget interaction, visible state,
  standard-dialog file/directory metadata without file-content I/O, wildcard
  filtering, manual path entry, cancel and invalid-path decisions, color/display
  selection, and documented omission of editor/help/stream/terminal/mouse/
  charset behavior.
- [ ] T030 [P] [US1] Add
  `tests/TuiVision.Examples.SmokeTests/DlgDsnSmokeTests.cs` covering structured
  dialog description create/load, render, one simple change, and visible
  rejection for malformed, incomplete, duplicate-control, and invalid-navigation
  descriptions.
- [ ] T031 [P] [US1] Add
  `tests/TuiVision.Examples.SmokeTests/SdlgSmokeTests.cs` covering vertical
  scrollable dialog behavior, deterministic focus movement, bounds, and visible
  control state.
- [ ] T032 [P] [US1] Add
  `tests/TuiVision.Examples.SmokeTests/Sdlg2SmokeTests.cs` covering horizontal
  and vertical scrollable dialog behavior, deterministic focus movement, bounds,
  and visible control state.

### Implementation for User Story 1

- [ ] T033 [P] [US1] Implement `examples/Demo/DemoApp.cs` with wave-2-capable
  controls, standard dialogs, color/display selection, and gadget flows only;
  keep file-dialog proof limited to local metadata, wildcard/manual-path state,
  cancel/invalid decisions, and no file-content reads or writes.
- [ ] T034 [US1] Implement `examples/Demo/Program.cs` and
  `examples/Demo/Demo.csproj` so `dotnet run --project examples/Demo` works and
  the app uses repository-wide .NET defaults.
- [ ] T035 [P] [US1] Implement `examples/DlgDsn/DlgDsnApp.cs` with a structured
  dialog description model, render flow, simple modification flow, validated
  symbolic dialog values, and visible rejection for malformed, incomplete,
  duplicate-control, and invalid-navigation descriptions.
- [ ] T036 [US1] Add source-controlled `dlgdsn` fixtures under
  `examples/DlgDsn/Fixtures/` for one valid dialog description plus malformed,
  incomplete, duplicate-control, and invalid-navigation rejection examples, using
  existing `TuiVision.Serialization`/resource primitives.
- [ ] T037 [US1] Implement `examples/DlgDsn/Program.cs` and
  `examples/DlgDsn/DlgDsn.csproj` so `dotnet run --project examples/DlgDsn`
  works and no new JSON/external format stack is introduced.
- [ ] T038 [P] [US1] Implement `examples/Sdlg/SdlgApp.cs` with historical
  vertical `ScrollDialog`/`ScrollGroup` behavior.
- [ ] T039 [US1] Implement `examples/Sdlg/Program.cs` and
  `examples/Sdlg/Sdlg.csproj` so `dotnet run --project examples/Sdlg` works.
- [ ] T040 [P] [US1] Implement `examples/Sdlg2/Sdlg2App.cs` with historical
  horizontal and vertical `ScrollDialog`/`ScrollGroup` behavior.
- [ ] T041 [US1] Implement `examples/Sdlg2/Program.cs` and
  `examples/Sdlg2/Sdlg2.csproj` so `dotnet run --project examples/Sdlg2` works.
- [ ] T042 [US1] If any reusable control/dialog behavior blocks US1, add
  focused failing tests in the affected `tests/TuiVision.Controls.Tests/` or
  `tests/TuiVision.Serialization.Tests/` file before changing `src/`.
- [ ] T043 [US1] If T042 is needed, implement the minimal reusable framework
  behavior in the existing `src/TuiVision.Controls/` or
  `src/TuiVision.Serialization/` modules without example-local substitutes.

**Checkpoint**: US1 examples run independently and their smoke tests prove
visible behavior beyond startup/exit.

---

## Phase 4: User Story 2 - Validate focused widget examples (Priority: P2)

**Goal**: Deliver focused examples for clipboard, input/list/history,
combo boxes, progress, and dynamic text.

**Independent Test**: Run
`dotnet test tests/TuiVision.Examples.SmokeTests/ --filter
"FullyQualifiedName~Clipboard|FullyQualifiedName~DynTxt|FullyQualifiedName~InpLis|FullyQualifiedName~ListVi|FullyQualifiedName~ProgBa|FullyQualifiedName~TCombo|FullyQualifiedName~TProgB"`
and confirm each example-specific visible result.

### Tests for User Story 2

> Write these tests first and ensure they fail before implementation.

- [ ] T044 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs` covering copy,
  cut, paste, input state, and unavailable or isolated clipboard behavior.
- [ ] T045 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs` covering dynamic
  text updates with short, long, and constrained-width values.
- [ ] T046 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs` covering input-list
  keyboard navigation, synchronized input/history/list state, and empty or
  minimal list contents.
- [ ] T047 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs` covering visible
  selection movement, empty-list behavior, first/last boundary handling, and
  viewport-sized content.
- [ ] T048 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs` covering
  deterministic progress through completion without wall-clock assertions.
- [ ] T049 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs` covering combo-box
  selection, synchronized input value, visible selected value, empty choices,
  and boundary-sized choice lists.
- [ ] T050 [P] [US2] Add
  `tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs` covering progress,
  abort, and visible canceled state without wall-clock assertions.

### Implementation for User Story 2

- [ ] T051 [P] [US2] Implement `examples/Clipboard/ClipboardApp.cs` with
  copy/cut/paste, visible input-state changes, and isolated clipboard fallback.
- [ ] T052 [US2] Implement `examples/Clipboard/Program.cs` and
  `examples/Clipboard/Clipboard.csproj` so
  `dotnet run --project examples/Clipboard` works.
- [ ] T053 [P] [US2] Implement `examples/DynTxt/DynTxtApp.cs` with predictable
  dynamic text or parameter updates inside constrained view bounds.
- [ ] T054 [US2] Implement `examples/DynTxt/Program.cs` and
  `examples/DynTxt/DynTxt.csproj` so `dotnet run --project examples/DynTxt`
  works.
- [ ] T055 [P] [US2] Implement `examples/InpLis/InpLisApp.cs` with
  `TInputLine`-oriented input-list/history synchronization and keyboard
  navigation.
- [ ] T056 [US2] Implement `examples/InpLis/Program.cs` and
  `examples/InpLis/InpLis.csproj` so `dotnet run --project examples/InpLis`
  works.
- [ ] T057 [P] [US2] Implement `examples/ListVi/ListViApp.cs` with
  `TListViewer`-style navigation, boundary handling, and visible selection
  state.
- [ ] T058 [US2] Implement `examples/ListVi/Program.cs` and
  `examples/ListVi/ListVi.csproj` so `dotnet run --project examples/ListVi`
  works.
- [ ] T059 [P] [US2] Implement `examples/ProgBa/ProgBaApp.cs` with
  deterministic progress through completion.
- [ ] T060 [US2] Implement `examples/ProgBa/Program.cs` and
  `examples/ProgBa/ProgBa.csproj` so `dotnet run --project examples/ProgBa`
  works.
- [ ] T061 [P] [US2] Implement `examples/TCombo/TComboApp.cs` with combo-box
  selection, input synchronization, and visible selected value.
- [ ] T062 [US2] Implement `examples/TCombo/Program.cs` and
  `examples/TCombo/TCombo.csproj` so `dotnet run --project examples/TCombo`
  works.
- [ ] T063 [P] [US2] Implement `examples/TProgB/TProgBApp.cs` with progress,
  abort, and visible canceled state.
- [ ] T064 [US2] Implement `examples/TProgB/Program.cs` and
  `examples/TProgB/TProgB.csproj` so `dotnet run --project examples/TProgB`
  works.
- [ ] T065 [US2] If any reusable control/widget behavior blocks US2, add
  focused failing tests in `tests/TuiVision.Controls.Tests/` before changing
  `src/TuiVision.Controls/`.
- [ ] T066 [US2] If T065 is needed, implement the minimal reusable control or
  widget behavior in `src/TuiVision.Controls/` without example-local
  substitutes.

**Checkpoint**: US2 examples run independently and their smoke tests prove each
focused interaction family.

---

## Phase 5: User Story 3 - Preserve documentation and proof for the example wave (Priority: P3)

**Goal**: Deliver guides, proof surfaces, statistics, architecture/security/A11Y
evidence, and final traceability from `Pflichtenheft.md` to examples.

**Independent Test**: Review the feature artifacts and confirm every wave-2
example has a guide, smoke evidence, and completion record.

### Tests and Review Tasks for User Story 3

- [ ] T067 [P] [US3] Define the text-first guide review notes that T069-T079
  must include in each new guide file, including expected interaction path and
  smoke or run command.
- [ ] T068 [US3] Review generated or user-facing documentation impact; if
  DocFX output changes, plan `docfx docfx.json` and
  `cd tests/web-a11y && npm run test:docfx`; otherwise record N/A rationale in
  `docs/architecture/quality-scenarios.md` or a feature proof note.

### Implementation for User Story 3

- [ ] T069 [P] [US3] Create `docs/guides/examples/clipboard.md` with German
  section first, English section second, CEFR-B2 wording, expected interaction
  path, accessibility notes, and validation command.
- [ ] T070 [P] [US3] Create `docs/guides/examples/demo.md` with German section
  first, English section second, accepted omissions for editor/help/stream/
  terminal/mouse/charset behavior, and validation command.
- [ ] T071 [P] [US3] Create `docs/guides/examples/dlgdsn.md` with German
  section first, English section second, valid/invalid dialog description
  workflow, malformed/incomplete/duplicate-control/invalid-navigation rejection
  notes, accepted limitations, and validation command.
- [ ] T072 [P] [US3] Create `docs/guides/examples/dyntxt.md` with German
  section first, English section second, dynamic text workflow, boundary notes,
  and validation command.
- [ ] T073 [P] [US3] Create `docs/guides/examples/inplis.md` with German
  section first, English section second, input/list/history workflow, boundary
  notes, and validation command.
- [ ] T074 [P] [US3] Create `docs/guides/examples/listvi.md` with German
  section first, English section second, list navigation workflow, boundary
  notes, and validation command.
- [ ] T075 [P] [US3] Create `docs/guides/examples/progba.md` with German
  section first, English section second, deterministic completion workflow, and
  validation command.
- [ ] T076 [P] [US3] Create `docs/guides/examples/sdlg.md` with German section
  first, English section second, historical vertical `ScrollDialog`/`ScrollGroup`
  scope, and validation command.
- [ ] T077 [P] [US3] Create `docs/guides/examples/sdlg2.md` with German section
  first, English section second, historical horizontal/vertical
  `ScrollDialog`/`ScrollGroup` scope, and validation command.
- [ ] T078 [P] [US3] Create `docs/guides/examples/tcombo.md` with German
  section first, English section second, combo-box workflow, boundary notes, and
  validation command.
- [ ] T079 [P] [US3] Create `docs/guides/examples/tprogb.md` with German
  section first, English section second, progress abort/canceled workflow, and
  validation command.
- [ ] T080 [US3] Update `examples/README.md` with wave-2 example rows,
  original source folders, launch commands, and required support assets.
- [ ] T081 [US3] Update `docs/toc.yml` or the relevant DocFX navigation/index
  surface so the new `docs/guides/examples/*.md` files are discoverable; if the
  guides are intentionally indexed only through `examples/README.md`, record the
  rationale in `docs/architecture/quality-scenarios.md`.
- [ ] T082 [US3] Record final accepted limitations and Historical Example
  Parity Cleanup references in `docs/architecture/architecture-risks.md`.
- [ ] T083 [US3] Refresh agent context for `codex`, `claude`, `gemini`, and
  `copilot` with `.specify/scripts/bash/update-agent-context.sh` if active
  technologies, proof surfaces, next-step marker, or workflow guidance changed.
- [ ] T084 [US3] If T083 changes shared guidance, synchronize affected
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md` together.

**Checkpoint**: US3 proof surfaces establish traceability from examples to tests,
guides, architecture, security, and A11Y evidence. Final Pflichtenheft marker,
statistics, and PR evidence are completed only after Phase 6 validation passes.

---

## Phase 6: Final Validation And PR Preparation

**Purpose**: Repository-level validation, coverage, formatting, dependency,
documentation, final proof updates, and PR evidence.

- [ ] T085 Increment the manual build counter in `Directory.Build.props` before
  each final-validation `dotnet build` or `dotnet test` command below, and keep
  `Version`, `AssemblyVersion`, and `FileVersion` aligned before the command
  runs.
- [ ] T086 Run `dotnet build --configuration Release` and record the result in
  the PR evidence.
- [ ] T087 Run `dotnet test tests/TuiVision.Examples.SmokeTests/` and verify
  all 15 delivered examples are covered.
- [ ] T088 Run `dotnet test` and record full-suite evidence.
- [ ] T089 Run `dotnet test --collect:"XPlat Code Coverage"` and record
  assembly-specific evidence for the required `>=70%` coverage gate and the
  `>=80%` target tracking.
- [ ] T090 Run `dotnet format --verify-no-changes` and record formatting
  evidence.
- [ ] T091 If public APIs, XML comments, generated docs, or DocFX navigation
  changed, confirm new public types are either intentionally internalized or have
  complete DE-first/EN-second XML docs, then run `docfx docfx.json`.
- [ ] T092 If T091 ran, run `cd tests/web-a11y && npm run test:docfx` and record
  WCAG/text-first smoke evidence.
- [ ] T093 Record platform evidence for the wave-2 examples: at minimum the
  current macOS validation plus Linux and Windows/WSL command evidence where
  practical; if Linux or Windows/WSL cannot be checked in this work item,
  document the reason and follow-up path for each missing environment in the PR
  evidence.
- [ ] T094 Run `git diff --check` and resolve whitespace or conflict-marker
  issues.
- [ ] T095 Review `.gitignore` and staged files to ensure no secrets, logs,
  agent state, local history, generated cache, or `.specify/presets/.cache/`
  content is tracked.
- [ ] T096 Update `Pflichtenheft.md` to check off the wave-2 checklist only
  after T086 through T095 pass or have a documented, accepted N/A rationale.
- [ ] T097 Move the `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md` to
  wave 3 only after T096 is complete.
- [ ] T098 Update `docs/project-statistics.md` with the completed
  `011-port-wave2-examples` phase, observable work window, production/test/docs
  line counts, evidence summary, and 80/125 lines-per-day comparison.
- [ ] T099 Prepare the PR summary with purpose, touched projects, tests run,
  coverage evidence, documentation/A11Y evidence, security/governance evidence,
  config/API impact, and accepted limitations.
- [ ] T100 Confirm `Version`, `AssemblyVersion`, and `FileVersion` in
  `Directory.Build.props` match the numbered-branch scheme before commit/push.
- [ ] T101 As the last Polish task, verify whether a corresponding
  `Lastenheft_*.md` exists for this feature; if yes, rename it with
  `bash scripts/rename-lastenheft.sh <LH-file> 011-port-wave2-examples` and
  include that rename before the final commit, otherwise document the explicit
  N/A rationale because this wave is driven directly from `Pflichtenheft.md`.

---

## Dependencies And Execution Order

### Phase Dependencies

- Phase 1 setup has no dependencies.
- Phase 2 foundation depends on Phase 1 and blocks story acceptance.
- Phase 3 US1 depends on Phase 2 and is the MVP.
- Phase 4 US2 depends on Phase 2 and may run in parallel with US1 after shared
  infrastructure is ready.
- Phase 5 US3 depends on implemented examples and smoke evidence for guide,
  architecture, security, and A11Y proof, but guide drafts can start in parallel
  once example scope is stable.
- Phase 6 final validation depends on the desired story set being complete and
  owns the final Pflichtenheft marker, statistics ledger, PR evidence, and
  Lastenheft rename or N/A decision.

### User Story Dependencies

- US1 can be validated independently after Phase 2.
- US2 can be validated independently after Phase 2.
- US3 final proof depends on US1 and US2 completion because it records delivered
  wave status.

### Red-Green-Refactor Rules

- Smoke tests in T029-T032 and T044-T050 must be written and observed failing
  before their matching implementation tasks are completed.
- Focused framework tests in T042/T065 must be written before framework changes
  when a blocker is found.
- Refactor only after the relevant smoke and focused tests are green.

### Parallel Opportunities

- T004-T014 can run in parallel after T003.
- T019-T028 can run in parallel where files do not overlap.
- T029-T032 can run in parallel.
- T033/T035/T038/T040 can run in parallel after their tests exist.
- T044-T050 can run in parallel.
- T051/T053/T055/T057/T059/T061/T063 can run in parallel after their tests
  exist.
- T069-T081 can run in parallel after example scope is stable.

---

## Implementation Strategy

### MVP First

1. Complete Phase 1 and Phase 2.
2. Complete US1 tests and implementation.
3. Validate `Demo`, `DlgDsn`, `Sdlg`, and `Sdlg2` smoke tests.
4. Review the MVP before continuing to focused widget examples.

### Incremental Delivery

1. Deliver core dialog/demo set (US1).
2. Deliver focused widget set (US2).
3. Deliver documentation and proof package (US3).
4. Run final validation, then update Pflichtenheft/statistics/PR evidence.

### Team Parallelization

After Phase 2:

- Developer A can own `Demo`/`DlgDsn`.
- Developer B can own `Sdlg`/`Sdlg2`.
- Developer C can own focused widget examples.
- Documentation and governance tasks can run in parallel once scope and accepted
  limitations are stable.
