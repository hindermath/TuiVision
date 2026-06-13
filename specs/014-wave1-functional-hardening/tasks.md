# Tasks: Wave 1 Functional Hardening

**Input**: Design documents from `specs/014-wave1-functional-hardening/`
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/wave1-functional-hardening-acceptance.md`, `checklists/plan-quality.md`, `checklists/requirements.md`
**Feature Branch**: `014-wave1-functional-hardening`

**Tests**: Required. The specification requires executable smoke proof whenever managed runtime behavior exists, and evidence-only proof only for explicit no-runtime-target cases with proof boundary.

**Organization**: Tasks are grouped by user story. Setup and Foundation establish the evidence surface, source reviews, and smoke audit. US1 delivers the historical proof matrix MVP, US2 hardens functional smoke proof, US3 classifies helper/headless paths, and US4 completes learner-facing traceability.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel because it touches different files or independent evidence surfaces.
- **[Story]**: User story label for story phases only.
- Every task names the target file path or evidence file path.

## Phase 1: Setup (Shared Baseline)

**Purpose**: Confirm readiness, create the feature evidence surface, and record the current Wave-1 implementation baseline before hardening starts.

- [ ] T001 Create `specs/014-wave1-functional-hardening/pr-evidence.md` with sections for setup evidence, the primary proof matrix, historical source reviews, smoke proof, helper classification, negative/fallback proof, missing-core decisions, documentation triggers, governance, validation, and final PR evidence
- [ ] T002 Record `.specify/scripts/bash/check-prerequisites.sh --json`, current branch, `specify preset list`, `specify preset info agent-parity-governance`, and the 36/36 checked `specs/014-wave1-functional-hardening/checklists/plan-quality.md` readiness result in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T003 Record the current Wave-1 baseline inventory for `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`, `examples/README.md`, and `tests/TuiVision.Examples.SmokeTests/` in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T004 Record the no-new-runtime-dependency and no-network/database/service/runtime-AI baseline from `Directory.Build.props`, `examples/Desklogo/Desklogo.csproj`, `examples/MsgCls/MsgCls.csproj`, `examples/Tutorial/Tutorial.csproj`, `examples/Videomode/Videomode.csproj`, and `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj` in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T005 Record the out-of-scope guard for Wave-1 visual remediation, Wave 2/3/4, broad framework revision, mouse-only operation, arbitrary user-file proof, and persistent user history in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T006 Verify `specs/014-wave1-functional-hardening/checklists/requirements.md` and `specs/014-wave1-functional-hardening/checklists/plan-quality.md` remain checked and reference them from `specs/014-wave1-functional-hardening/pr-evidence.md`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Complete read-only historical review and proof-gap classification before any user-story implementation starts.

**CRITICAL**: No user-story implementation should start until the historical source review and current smoke-proof audit are recorded.

- [ ] T007 Record the `Desklogo` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/desklogo/desklogo.cc`, with `tv203s/contrib/tvision/examples/desklogo/set-logo.cc` and `tv203s/contrib/tvision/examples/desklogo/tv_logo.cc` only for asset/generator boundary rationale
- [ ] T008 Record the `MsgCls` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/msgcls/testdyn.cpp`, `tv203s/contrib/tvision/examples/msgcls/tlnmsg.cpp`, and `tv203s/contrib/tvision/examples/msgcls/tlnmsg.h`
- [ ] T009 Record the `Tutorial` `tvguid01` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc`
- [ ] T010 Record the `Tutorial` `tvguid02` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid02.cc`
- [ ] T011 Record the `Tutorial` `tvguid03` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid03.cc`
- [ ] T012 Record the `Tutorial` `tvguid04` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid04.cc`
- [ ] T013 Record the `Tutorial` `tvguid05` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid05.cc`
- [ ] T014 Record the `Tutorial` `tvguid06` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid06.cc`
- [ ] T015 Record the `Tutorial` `tvguid07` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid07.cc`
- [ ] T016 Record the `Tutorial` `tvguid08` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid08.cc`
- [ ] T017 Record the `Tutorial` `tvguid09` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid09.cc`
- [ ] T018 Record the `Tutorial` `tvguid10` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid10.cc`
- [ ] T019 Record the `Tutorial` `tvguid11` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid11.cc`
- [ ] T020 Record the `Tutorial` `tvguid12` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid12.cc`
- [ ] T021 Record the `Tutorial` `tvguid13` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid13.cc`
- [ ] T022 Record the `Tutorial` `tvguid14` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid14.cc`
- [ ] T023 Record the `Tutorial` `tvguid15` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid15.cc`
- [ ] T024 Record the `Tutorial` `tvguid16` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/tutorial/tvguid16.cc`
- [ ] T025 Record the `Videomode` read-only source review in `specs/014-wave1-functional-hardening/pr-evidence.md` using `tv203s/contrib/tvision/examples/videomode/test.cc`
- [ ] T026 Record any additional `tv203s/` header/declaration review needed for constants, macros, data layout, inheritance, or signatures in `specs/014-wave1-functional-hardening/pr-evidence.md`; record `N/A` there if no additional header is needed
- [ ] T027 Audit existing Wave-1 smoke proof in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`, then record startup-only, static-text-only, helper-only, and missing-proof gaps in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T028 Audit existing helper infrastructure in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` and `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`, then record whether current helper taxonomy covers `SetupOnly`, `PrimaryProof`, `SupplementalProof`, and `LegacyOrTemporary` in `specs/014-wave1-functional-hardening/pr-evidence.md`

**Checkpoint**: Historical reviews, source boundaries, smoke gaps, and helper-classification gaps are recorded.

---

## Phase 3: User Story 1 - Historical proof matrix for Wave 1 (Priority: P1) MVP

**Goal**: A reviewer can inspect `specs/014-wave1-functional-hardening/pr-evidence.md` and find complete historical proof records for `Desklogo`, `MsgCls`, `Videomode`, and each of the 16 `Tutorial` steps.

**Independent Test**: Review `specs/014-wave1-functional-hardening/pr-evidence.md` and confirm all required matrix fields exist for every scoped area and every tutorial token.

### Evidence for User Story 1

- [ ] T029 [US1] Complete the `Desklogo` `Wave1FunctionalReview` row in `specs/014-wave1-functional-hardening/pr-evidence.md` with historical source, historical core function, current C# behavior, proof method, helper classification, negative/fallback proof, missing-core decision, deviation, documentation trigger, validation placeholder, and evidence location
- [ ] T030 [US1] Complete the `MsgCls` `Wave1FunctionalReview` row in `specs/014-wave1-functional-hardening/pr-evidence.md` with historical source, historical core function, current C# behavior, proof method, helper classification, negative/fallback proof, missing-core decision, deviation, documentation trigger, validation placeholder, and evidence location
- [ ] T031 [US1] Complete 16 `TutorialStepReview` sub-records for `tvguid01` through `tvguid16` in `specs/014-wave1-functional-hardening/pr-evidence.md`, each with historical source, managed step path, learning target or behavior proof, sequence relationship, deviation decision, and evidence location
- [ ] T032 [US1] Complete the `Videomode` `Wave1FunctionalReview` row in `specs/014-wave1-functional-hardening/pr-evidence.md` with historical source, historical core function, current C# behavior, proof method, helper classification, negative/fallback proof, missing-core decision, deviation, documentation trigger, validation placeholder, and evidence location
- [ ] T033 [US1] Add a reviewer checklist section to `specs/014-wave1-functional-hardening/pr-evidence.md` proving SC-001 and SC-002: four of four Wave-1 areas plus 16 of 16 tutorial steps are represented without collapsing `Tutorial` into one generic row

**Checkpoint**: User Story 1 is independently testable: the primary proof matrix is complete enough to serve as the MVP.

---

## Phase 4: User Story 2 - Hardened functional smoke proof (Priority: P1)

**Goal**: Each Wave-1 area has meaningful executable smoke proof for managed runtime behavior, or explicit evidence-only proof where no direct runtime target exists.

**Independent Test**: Run the relevant `tests/TuiVision.Examples.SmokeTests/` filters and inspect `pr-evidence.md` for proof methods, smoke names, fallback boundaries, and missing-core decisions.

### Tests for User Story 2

- [ ] T034 [P] [US2] Add or sharpen failing `Desklogo` smoke assertions in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs` so proof covers logo/desktop intent, asset replacement rationale, and undersized-display fallback rather than only startup
- [ ] T035 [P] [US2] Add or sharpen failing `MsgCls` smoke assertions in `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs` so proof covers custom message triggering, routing, observable result, and repeated-trigger stability
- [ ] T036 [US2] Add or sharpen failing `Tutorial` smoke assertions for `tvguid01` through `tvguid08` in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs` so each token has step-specific learning-target or behavior proof beyond startup and proves selectable step identity through the managed tutorial catalog, CLI token path, or equivalent public step-selection path
- [ ] T037 [US2] Add or sharpen failing `Tutorial` smoke assertions for `tvguid09` through `tvguid16` in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs` so each token has step-specific learning-target or behavior proof beyond startup and proves selectable step identity through the managed tutorial catalog, CLI token path, or equivalent public step-selection path
- [ ] T038 [P] [US2] Add or sharpen failing `Videomode` smoke assertions in `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs` so proof covers real capability outcome or clear fallback, post-transition usability, and platform limitation

### Implementation for User Story 2

- [ ] T039 [P] [US2] Implement only the FR-027-compatible minimal `Desklogo` functional proof support required by T034 in `examples/Desklogo/DesklogoApp.cs` and `examples/Desklogo/DesklogoDesktop.cs`; otherwise record `IntentionalDeviation` or `FollowUp` in T045 without adding visual-remediation scope
- [ ] T040 [P] [US2] Implement only the FR-027-compatible minimal `MsgCls` functional proof support required by T035 in `examples/MsgCls/MsgClsApp.cs`, `examples/MsgCls/MsgClsWindow.cs`, and `examples/MsgCls/MsgClsEvents.cs`; otherwise record `IntentionalDeviation` or `FollowUp` in T045 without broad framework work
- [ ] T041 [P] [US2] Implement only the FR-027-compatible minimal `Tutorial` proof support required by T036 and T037 in `examples/Tutorial/TutorialApp.cs`, `examples/Tutorial/Steps/TutorialStepCatalog.cs`, and `examples/Tutorial/Steps/TvGuid01Step.cs` through `examples/Tutorial/Steps/TvGuid16Step.cs`, including public token/step-selection proof support if missing; otherwise record `IntentionalDeviation` or `FollowUp` in T045
- [ ] T042 [P] [US2] Implement only the FR-027-compatible minimal `Videomode` proof support required by T038 in `examples/Videomode/VideomodeApp.cs`, `examples/Videomode/VideomodeView.cs`, and `examples/Videomode/DisplayModeCoordinator.cs`; otherwise record `IntentionalDeviation` or `FollowUp` in T045 without broad framework, visual-remediation, or new-dependency scope
- [ ] T043 [US2] Record each accepted `Desklogo`, `MsgCls`, `Tutorial`, and `Videomode` smoke method name, proof method, concrete assertion, selected `Tutorial` token or step path where applicable, and executable/evidence-only status in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T044 [US2] Record every relevant negative/fallback proof or proof boundary for `Desklogo`, `MsgCls`, `Tutorial`, and `Videomode` in `specs/014-wave1-functional-hardening/pr-evidence.md`, including an explicit `N/A` rationale for any area where no acceptance-relevant negative/fallback path exists
- [ ] T045 [US2] Record every missing-core-function decision discovered during US2 in `specs/014-wave1-functional-hardening/pr-evidence.md`, marking each as `ImplementIn014`, `IntentionalDeviation`, or `FollowUp`
- [ ] T046 [US2] Run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Desklogo|FullyQualifiedName~MsgCls|FullyQualifiedName~Tutorial|FullyQualifiedName~Videomode"` after incrementing `Directory.Build.props`, then record command output or blocker in `specs/014-wave1-functional-hardening/pr-evidence.md`

**Checkpoint**: User Story 2 is independently testable: every scoped Wave-1 area has meaningful functional smoke proof or an explicit no-runtime-target boundary.

---

## Phase 5: User Story 3 - Helper and headless path classification (Priority: P2)

**Goal**: Every helper, headless, or direct proof path used by Wave-1 smokes is classified as `SetupOnly`, `PrimaryProof`, `SupplementalProof`, or `LegacyOrTemporary`.

**Independent Test**: Inspect `tests/TuiVision.Examples.SmokeTests/` and `specs/014-wave1-functional-hardening/pr-evidence.md` and confirm no helper-only path is counted as primary proof unless it executes real app logic through an allowed public surface with concrete assertions.

### Tests for User Story 3

- [ ] T047 [US3] Extend helper-classification support in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` so Wave-1 smokes can explicitly record `SetupOnly`, `PrimaryProof`, `SupplementalProof`, and `LegacyOrTemporary`
- [ ] T048 [P] [US3] Add or sharpen `Desklogo` and `MsgCls` helper-classification assertions in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs` and `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`
- [ ] T049 [P] [US3] Add or sharpen `Tutorial` helper-classification assertions in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`
- [ ] T050 [P] [US3] Add or sharpen `Videomode` helper-classification assertions in `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`

### Implementation for User Story 3

- [ ] T051 [US3] Record the final helper/headless/direct path classification inventory for all Wave-1 smokes in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T052 [US3] Record every `LegacyOrTemporary` classification and its later Wave-1 visual-remediation responsibility in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T053 [US3] Run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Desklogo|FullyQualifiedName~MsgCls|FullyQualifiedName~Tutorial|FullyQualifiedName~Videomode"` after incrementing `Directory.Build.props`, or explicitly reuse T046 output only if it includes the final helper-classification assertions, then record helper-classification validation output or blocker in `specs/014-wave1-functional-hardening/pr-evidence.md`

**Checkpoint**: User Story 3 is independently testable: all Wave-1 helper/headless/direct paths have accepted classifications.

---

## Phase 6: User Story 4 - Learner-facing traceability (Priority: P2)

**Goal**: Learners and text-first reviewers can understand historical intent, modern deviations, proof status, and next-step boundaries in German first and English second.

**Independent Test**: Read the affected guide, README, and `pr-evidence.md`; confirm CEFR-B2-oriented German-first/English-second traceability and text-first A11Y notes for each Wave-1 area.

### Documentation for User Story 4

- [ ] T054 [P] [US4] Update `docs/guides/examples/desklogo.md` with German-first/English-second CEFR-B2 historical intent, managed behavior, proof method, asset/generator boundary, fallback/deviation, and text-first A11Y notes if T029, T034, or T039 changes learner-facing facts
- [ ] T055 [P] [US4] Update `docs/guides/examples/msgcls.md` with German-first/English-second CEFR-B2 historical intent, managed routing behavior, proof method, helper classification, deviation, and text-first A11Y notes if T030, T035, or T040 changes learner-facing facts
- [ ] T056 [P] [US4] Update `docs/guides/examples/tutorial.md` with German-first/English-second CEFR-B2 traceability for `tvguid01` through `tvguid16`, proof boundaries, deviations, and text-first A11Y notes if T031, T036, T037, or T041 changes learner-facing facts
- [ ] T057 [P] [US4] Update `docs/guides/examples/videomode.md` with German-first/English-second CEFR-B2 historical intent, managed capability/fallback behavior, platform limitation, proof boundary, and text-first A11Y notes if T032, T038, or T042 changes learner-facing facts
- [ ] T058 [US4] Update `examples/README.md` with the Wave-1 functional-hardening status, `pr-evidence.md` pointer, historical-source boundary, and statement that Wave-1 visual remediation and Wave 3 remain separate if learner-facing summary facts changed
- [ ] T059 [US4] Record guide/README documentation triggers, updated artifact paths, or explicit review-only `N/A` decisions in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T060 [US4] Record German-first/English-second CEFR-B2 and text-first/A11Y review status for affected `docs/guides/examples/*.md`, `examples/README.md`, and `specs/014-wave1-functional-hardening/pr-evidence.md`

**Checkpoint**: User Story 4 is independently testable: learner-facing traceability is present where facts changed, and review-only details are confined to `pr-evidence.md`.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Complete governance evidence, versioning, final validation, statistics, Pflichtenheft state, agent parity review, and Lastenheft archiving.

- [ ] T061 Record architecture impact and unchanged-risk or changed-risk rationale in `specs/014-wave1-functional-hardening/pr-evidence.md`; update `docs/architecture/architecture-vision.md`, `docs/architecture/runtime-view.md`, `docs/architecture/quality-scenarios.md`, and `docs/architecture/architecture-risks.md` only if implementation changes architecture-facing facts
- [ ] T062 Record security governance evidence for C#/.NET secure coding, NIST SSDF, CWE Top 25, ASVS `N/A`, CAPEC `N/A`, Zero Trust `N/A`, SBOM/VEX/SLSA unchanged, and AI-SBOM `N/A` in `specs/014-wave1-functional-hardening/pr-evidence.md`; update `docs/security/security-checklist.md`, `docs/security/threat-model.md`, `docs/security/dependency-audit.md`, and `docs/security/supply-chain-evidence.md` only if implementation changes risk, dependency, release, or supply-chain facts
- [ ] T063 Record `security-governance` v0.4.0, `architecture-governance` v0.2.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.2.0, `cross-platform-governance` v0.1.0, and `agent-parity-governance` v0.2.0 applicability in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T064 Review agent guidance parity for `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`; update those files only if active feature context, technologies, project structure, or shared workflow rules changed, otherwise record unchanged rationale in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T065 Update `Pflichtenheft.md` only if implementation changes the prioritized next-step marker, Wave-1 hardening status, or evidence status; otherwise record unchanged rationale in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T066 Run `dotnet restore` and record output or blocker in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T067 Align `Directory.Build.props` to branch version `1.14.<patch>.<build>` and increment the manual build counter immediately before `dotnet build --configuration Release`, then record the version and build output in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T068 Align `Directory.Build.props` to branch version `1.14.<patch>.<build>` and increment the manual build counter immediately before `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release`, then record output in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T069 Align `Directory.Build.props` to branch version `1.14.<patch>.<build>` and increment the manual build counter immediately before `dotnet test --configuration Release`, then record output in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T070 Align `Directory.Build.props` to branch version `1.14.<patch>.<build>` and increment the manual build counter immediately before `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`, then record coverage output in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T071 Run `dotnet format --verify-no-changes` and record output or blocker in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T072 If T054-T058 changed guides, README, DocFX content, documentation navigation, or API documentation, run `docfx docfx.json` and record output plus generated-output hygiene for `_site/` and `api/*.yml` in `specs/014-wave1-functional-hardening/pr-evidence.md`; otherwise record DocFX `N/A` rationale there
- [ ] T073 If T072 ran DocFX, run `npm run test:docfx` from `tests/web-a11y/` and record Playwright/axe output in `specs/014-wave1-functional-hardening/pr-evidence.md`; otherwise record web-a11y `N/A` rationale there
- [ ] T074 Run `git diff --check` and record the clean result or blocker in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T075 Update final PR evidence in `specs/014-wave1-functional-hardening/pr-evidence.md` with changed examples, changed tests, documentation changes, validation commands, security-risk statement, AI-SBOM `N/A`, and confirmation that no visual remediation, Wave 2/3/4 behavior, new dependency, database, external service, network path, persistent history, or runtime/product AI was added
- [ ] T076 Update `docs/project-statistics.md` after implementation validation is complete, using the final 014 implementation scope, changed production/test/documentation line counts, validation evidence from `specs/014-wave1-functional-hardening/pr-evidence.md`, manual baselines, acceleration notes, and a refreshed final `## Gesamtstatistik` block with ASCII diagrams and adjacent CEFR-B2 explanations
- [ ] T077 Verify `Lastenheft_Wave1-Visual-Component-Remediation.md` remains an unrenamed follow-up intake and record that boundary in `specs/014-wave1-functional-hardening/pr-evidence.md`
- [ ] T078 Perform a final self-check that all task acceptance surfaces in `specs/014-wave1-functional-hardening/pr-evidence.md`, `tests/TuiVision.Examples.SmokeTests/`, affected guides, `examples/README.md`, `docs/project-statistics.md`, `Pflichtenheft.md`, and the ready-to-archive `Lastenheft_Wave1-Functional-Hardening.md` state are complete, then rerun `git diff --check` after T075 and T076 and record the final clean result or blocker in `specs/014-wave1-functional-hardening/pr-evidence.md`, with only the constitution-required Lastenheft rename remaining
- [ ] T079 Run `bash scripts/rename-lastenheft.sh Lastenheft_Wave1-Functional-Hardening.md 014-wave1-functional-hardening` or `pwsh scripts/rename-lastenheft.ps1 -File Lastenheft_Wave1-Functional-Hardening.md -BranchName 014-wave1-functional-hardening`, then record the resulting Lastenheft path in `specs/014-wave1-functional-hardening/pr-evidence.md` as the last Polish step

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies.
- **Foundational (Phase 2)**: Depends on Setup and blocks all user stories.
- **US1 Historical proof matrix (Phase 3)**: Depends on Foundational and is the MVP.
- **US2 Hardened functional smoke proof (Phase 4)**: Depends on Foundational and uses US1 evidence records as proof targets.
- **US3 Helper/headless classification (Phase 5)**: Depends on Foundation and can proceed after the current proof paths are known; it integrates with US2 smoke proof.
- **US4 Learner-facing traceability (Phase 6)**: Depends on the matching US1-US3 facts for each area.
- **Polish (Phase 7)**: Depends on all desired user stories.

### User Story Dependencies

- **US1 (P1)**: MVP after Foundation; no dependency on US2-US4.
- **US2 (P1)**: Can proceed after Foundation; updates US1 matrix rows with final smoke names and proof status.
- **US3 (P2)**: Can proceed after Foundation; final classification should be reconciled after US2 smoke changes.
- **US4 (P2)**: Proceeds after relevant US1-US3 facts are stable.

### Within Each User Story

- Add or sharpen smoke tasks before implementation tasks where executable proof is required.
- Record every accepted proof in `pr-evidence.md`.
- Keep `tv203s/` read-only.
- Keep runtime behavior narrow and avoid visual-remediation scope.

---

## Parallelization Notes

### Shared Evidence Work

```text
T007-T025 source reviews may be read independently, but evidence edits are serialized through pr-evidence.md and are intentionally not marked [P].
T061-T065 governance and status checks are serialized because they also update pr-evidence.md and may touch shared governance/status artifacts.
```

### US2 Smoke Proof Work

```text
T034 Desklogo smoke hardening in DesklogoSmokeTests.cs
T035 MsgCls smoke hardening in MsgClsSmokeTests.cs
T038 Videomode smoke hardening in VideomodeSmokeTests.cs
T036-T037 are serialized because both edit TutorialSmokeTests.cs.
```

### US4 Documentation Work

```text
T054 docs/guides/examples/desklogo.md
T055 docs/guides/examples/msgcls.md
T056 docs/guides/examples/tutorial.md
T057 docs/guides/examples/videomode.md
```

---

## Implementation Strategy

### MVP First (US1 Only)

1. Complete Phase 1 Setup.
2. Complete Phase 2 Foundation.
3. Complete Phase 3 US1.
4. Stop and validate `specs/014-wave1-functional-hardening/pr-evidence.md` for four of four Wave-1 areas and 16 of 16 tutorial steps.

### Incremental Delivery

1. Finish US1 historical proof matrix.
2. Add US2 hardened functional smokes and minimal required runtime support.
3. Add US3 helper/headless classification.
4. Add US4 learner-facing traceability only where facts changed.
5. Complete Polish validation and governance evidence.

### Parallel Team Strategy

1. One engineer owns `pr-evidence.md` structure and final consistency.
2. Independent reviewers can process historical sources per area after Setup.
3. Independent implementers can harden `[P]`-marked area-specific smoke paths after Foundation; shared `TutorialSmokeTests.cs` and `pr-evidence.md` edits must be serialized.
4. Documentation work starts only after the corresponding proof facts are stable.
