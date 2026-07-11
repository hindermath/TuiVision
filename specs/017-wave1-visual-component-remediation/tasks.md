# Tasks: Wave-1 Visual Component Remediation

**Input**: Design documents from `specs/017-wave1-visual-component-remediation/`  
**Prerequisites**: `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, contract, and completed checklists  
**Tests**: Test-first smoke and contract proof is mandatory.  
**Execution model**: All tasks are intentionally sequential because slices share evidence, project files, runtime helpers, agent guidance, statistics, or version state.

## Format: `[ID] [Story] Description`

- Every task uses a stable ID and exact path.
- User-story tasks carry `[US1]` through `[US4]`.
- Mark a task `[X]` only after its acceptance condition is satisfied.

## Phase 1: Setup And Evidence Foundation

**Purpose**: Establish a verified branch, complete inputs, and auditable evidence before runtime edits.

- [X] T001 Read `AGENTS.md` and `.specify/memory/constitution.md`, run `specify check`, verify branch `017-wave1-visual-component-remediation`, and verify `.specify/feature.json` points to `specs/017-wave1-visual-component-remediation`.
- [X] T002 Run `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks` and record resolved paths in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T003 Verify every file in `specs/017-wave1-visual-component-remediation/checklists/` has zero incomplete items and record the count in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T004 Create `specs/017-wave1-visual-component-remediation/pr-evidence.md` with feature summary, scope boundaries, review-area table, Tutorial matrix, framework-decision table, governance table, validation table, documentation/A11Y table, and PR description section.
- [X] T005 Define review-area columns `AreaId`, `PathOrFlow`, `HistoricalSource`, `MainSurface`, `StatusSurface`, `DescriptionPath`, `Operation`, `PrimarySmoke`, `ConcreteState`, `ViewTreeProof`, `RenderedProof`, `HelperUsage`, `FrameworkDecision`, `Deviation`, `ProofBoundary`, and `FollowUp` in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T006 Define governance columns `RunId`, `PresetName`, `PresetVersion`, `Checkpoint`, `Applicability`, `Rationale`, `EvidencePath`, `Owner`, `Reviewer`, `ReviewDate`, `Result`, `ResidualRisk`, `FollowUp`, and `ReevaluationTrigger` in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T007 Inventory the feature-014 functional baseline for Desklogo, MsgCls, Tutorial, and Videomode from `specs/014-wave1-functional-hardening/pr-evidence.md` into `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T008 Record the pre-change `tv203s/` path inventory and clean-diff boundary for all required historical sources in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T009 Review `examples/Shared/Wave2Runtime.cs`, `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs`, and `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`; record reuse and non-reuse decisions in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T010 Add pending framework-decision rows for Desklogo, MsgCls, Tutorial, Videomode, shared Wave-1 composition, and shared smoke infrastructure in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T011 Add all six preset/version rows and every named security, architecture, cloud, regulatory, A11Y, cross-platform, and agent-parity checkpoint to `specs/017-wave1-visual-component-remediation/pr-evidence.md` without empty starter fields.
- [X] T012 Record initial scope-diff and version baseline from `Directory.Build.props` in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

**Checkpoint**: Evidence schemas and baseline rows exist before any source/test implementation edit.

---

## Phase 2: Foundational Historical And Design Decisions

**Purpose**: Resolve shared decisions that block all user-story work.

- [X] T013 Review `tv203s/contrib/tvision/examples/desklogo/desklogo.cc`, `set-logo.cc`, and `tv_logo.cc` and record logo/desktop, generator, clipping, description, and quit intent in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T014 Review `tv203s/contrib/tvision/examples/msgcls/testdyn.cpp`, `tlnmsg.cpp`, and `tlnmsg.h` and record command, broadcast, window, repeat, and historical deviation intent in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T015 Review `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc` through `tvguid16.cc` and record exactly 16 token/intent/target rows in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T016 Review `tv203s/contrib/tvision/examples/videomode/test.cc` and record capability, command, fallback, rejection, unchanged, and post-operation intent in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T017 Reconcile current Tutorial titles and guide claims with historical source comments and record any corrected intent or `IntentionalDeviation` in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T018 Record the exact 16-row Tutorial visual-target map from `specs/017-wave1-visual-component-remediation/plan.md` in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T019 Decide `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening` for shared status/help/region composition before creating `examples/Shared/Wave1Runtime.cs`; prove reusable behavior is not duplicated across examples and record rationale in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T020 Verify the four Wave-1 project files can link one shared composition source without package or target-framework changes in `examples/Desklogo/Desklogo.csproj`, `examples/MsgCls/MsgCls.csproj`, `examples/Tutorial/Tutorial.csproj`, and `examples/Videomode/Videomode.csproj`.
- [X] T021 Define command IDs, status text, description content, stable visible regions, and quit paths for all four apps in `specs/017-wave1-visual-component-remediation/pr-evidence.md` before coding.
- [X] T022 Review all planned non-trivial logic and smoke helpers for didactic inline-comment value under feature-015 guidance and record comment-needed decisions in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T023 Run `git diff --check` and verify no path under `tv203s/` changed; record the foundational checkpoint in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

**Checkpoint**: Historical intent, visual target, framework ownership, and proof boundaries are complete.

---

## Phase 3: User Story 1 - Sichtbare Wave-1-Demos / Visible Wave-1 Demos (Priority: P1)

**Goal**: Every example and Tutorial token presents a historically relevant visible main state. `MsgCls` proves the complete vertical-slice pattern first.

**Independent Test**: Launch each scoped path and verify its visible main state; the Tutorial matrix reports 16/16 distinct valid tokens.

### Test-First Vertical Slice And Main Surfaces

- [X] T024 [US1] Add failing MsgCls app-loop tests for visible command routing, message-window state, repeat stability, real status, `Help -> Description`, view-tree identity, and rendered regions in `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`.
- [X] T025 [US1] Increment the manual build counter and align `Version`, `AssemblyVersion`, and `FileVersion` to `1.17.<patch>.<build>` in `Directory.Build.props` before the expected-failing MsgCls test.
- [X] T026 [US1] Run the targeted expected-failing MsgCls Release test and record the failure boundary in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T027 [US1] Add linked compile entries for `examples/Shared/Wave1Runtime.cs` to `examples/Desklogo/Desklogo.csproj`, `examples/MsgCls/MsgCls.csproj`, `examples/Tutorial/Tutorial.csproj`, and `examples/Videomode/Videomode.csproj` without changing dependencies.
- [X] T028 [US1] Implement drawable Wave-1 status, Help menu, status formatting, and stable screen-region helpers in `examples/Shared/Wave1Runtime.cs` with selective bilingual didactic comments for non-trivial proof boundaries.
- [X] T029 [US1] Implement the MsgCls menu/key command, visible routed-message state, repeated-trigger feedback, real status line, description window, and stable quit behavior in `examples/MsgCls/MsgClsApp.cs` and `examples/MsgCls/MsgClsWindow.cs`.
- [X] T030 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the MsgCls passing test.
- [X] T031 [US1] Run targeted MsgCls Release tests and record method names, count, state/view/render proof, and helper classification in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T032 [US1] Finalize the MsgCls and shared-composition framework decisions in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T033 [US1] Add failing Desklogo tests for logo/desktop view-tree identity, stable rendered logo region, undersized clipping, status, description, and quit in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`.
- [X] T034 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the expected-failing Desklogo test.
- [X] T035 [US1] Run the targeted expected-failing Desklogo Release test and record its failure boundary in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T036 [US1] Integrate the visible logo/desktop state, real status, description, and quit path without artificial logo mutation in `examples/Desklogo/DesklogoApp.cs` and `examples/Desklogo/DesklogoDesktop.cs`.
- [X] T037 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the Desklogo passing test.
- [X] T038 [US1] Run targeted Desklogo Release tests and record full-size/clipped render proof plus asset/generator boundary in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T039 [US1] Finalize the Desklogo framework decision and historical deviation row in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T040 [US1] Add failing Tutorial tests for 16 unique tokens, default launch, unknown-token fallback, representative view kinds, distinct rendered states, status, description, and quit in `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`.
- [X] T041 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the expected-failing Tutorial test.
- [X] T042 [US1] Run the targeted expected-failing Tutorial Release test and record the failure boundary in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T043 [US1] Implement the internal 16-token representative component/state factory in `examples/Tutorial/TutorialVisualFactory.cs` using existing controls and the target map from `plan.md`.
- [X] T044 [US1] Integrate token selection, default and unknown-token states, visible target insertion, status, description, operation routing, and quit in `examples/Tutorial/TutorialApp.cs` and `examples/Tutorial/Program.cs` without changing the accepted `ITutorialStep` metadata contract.
- [X] T045 [US1] Correct only historically inaccurate Tutorial metadata in `examples/Tutorial/Steps/` when T017 evidence proves the correction; otherwise record unchanged rationale in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T046 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the Tutorial passing test.
- [X] T047 [US1] Run targeted Tutorial Release tests and record 16/16 token/state/view/render proof plus default and fallback outcomes in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T048 [US1] Finalize the Tutorial framework decision and all 16 historical-intent rows in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T049 [US1] Add failing Videomode app-loop tests for visible probe/retry, canonical result state, status, description, view/render proof, and post-operation usability in `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`.
- [X] T050 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the expected-failing Videomode test.
- [X] T051 [US1] Run the targeted expected-failing Videomode Release test and record the failure boundary in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T052 [US1] Implement visible probe/retry command routing and exact `supported`, `fallback`, `rejected`, or `unchanged` presentation in `examples/Videomode/VideomodeApp.cs`, `examples/Videomode/DisplayModeCoordinator.cs`, and `examples/Videomode/VideomodeView.cs` without overstating terminal capability.
- [X] T053 [US1] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the Videomode passing test.
- [X] T054 [US1] Run targeted Videomode Release tests and record platform-independent state/view/render and post-operation proof in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T055 [US1] Finalize the Videomode framework decision and capability/fallback boundary in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

**Checkpoint**: All four example areas and all 16 Tutorial tokens have a visible primary state.

---

## Phase 4: User Story 2 - Bedienbare Drei-Schichten-Erfahrung / Operable Three-Layer Experience (Priority: P1)

**Goal**: Ensure main state, real status line, operation path, and keyboard description form one coherent experience in every app.

**Independent Test**: A keyboard-driven app-loop scenario changes or reveals an accepted visible state, updates status, opens description, and exits cleanly for every example area.

- [X] T056 [US2] Add cross-app regression assertions for real `TStatusLine`, `Help -> Description`, keyboard access, close/quit behavior, and rendered status/description text in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`.
- [X] T057 [US2] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the three-layer diagnostic test.
- [X] T058 [US2] Run the combined Wave-1 three-layer Release tests and record pass/fail for every layer; any failing layer becomes the required remediation boundary for T059-T064 in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T059 [US2] Remediate only failing Desklogo keyboard description, status hint, clipping-safe layout, or clean-exit layers in `examples/Desklogo/DesklogoApp.cs`; otherwise record unchanged rationale.
- [X] T060 [US2] Remediate only failing MsgCls command repetition, focused/unfocused routing visibility, status update, description, or clean-exit layers in `examples/MsgCls/MsgClsApp.cs` and `examples/MsgCls/MsgClsWindow.cs`; otherwise record unchanged rationale.
- [X] T061 [US2] Remediate only failing Tutorial step-specific status/navigation, operation/description routing, fallback explanation, or clean-exit layers in `examples/Tutorial/TutorialApp.cs` and `examples/Tutorial/TutorialVisualFactory.cs`; otherwise record unchanged rationale.
- [X] T062 [US2] Remediate only failing Videomode retry/result status, honest description, or post-operation layers in `examples/Videomode/VideomodeApp.cs` and `examples/Videomode/VideomodeView.cs`; otherwise record unchanged rationale.
- [X] T063 [US2] Ensure all four status lines draw into stable terminal rows and expose current message state through `examples/Shared/Wave1Runtime.cs` without adding a framework API.
- [X] T064 [US2] Ensure descriptions in `examples/Desklogo/DesklogoApp.cs`, `examples/MsgCls/MsgClsApp.cs`, `examples/Tutorial/TutorialApp.cs`, and `examples/Videomode/VideomodeApp.cs` use German-first/English-second CEFR-B2 text and explain visual state, operation, historical intent, and status.
- [X] T065 [US2] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the combined passing three-layer test.
- [X] T066 [US2] Run the combined Wave-1 three-layer Release tests and record per-app status/description/view/render results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T067 [US2] Review `InteractiveSmokeEventScript` usage for key, command, description, repeated action, and quit sequences; change `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs` only if a generic proof gap is demonstrated.
- [X] T068 [US2] Record explicit unchanged rationale for any three-layer source file not modified after its tests already pass in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T069 [US2] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the full four-app operation-path test.
- [X] T070 [US2] Run all Desklogo, MsgCls, Tutorial, and Videomode Release tests and record the combined count in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T071 [US2] Verify every example has exactly one final framework decision and every `FollowUpHardening` row has issue, boundary, owner, follow-up, residual risk, and trigger in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T072 [US2] Run `git diff --check`, verify zero `tv203s/` changes, and record the user-story checkpoint in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

**Checkpoint**: The three-layer keyboard experience is complete for every scoped path.

---

## Phase 5: User Story 3 - Sichtbarer App-Loop-Nachweis / Visible App-Loop Proof (Priority: P2)

**Goal**: Make visible runtime maturity regression-safe through one authoritative matrix and concrete proof layers.

**Independent Test**: The matrix rejects missing app-loop, state, view, buffer/cell, status, description, and helper-boundary evidence.

- [X] T073 [US3] Add a failing Wave-1 visual acceptance matrix with four example rows and 16 Tutorial token rows in `tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`.
- [X] T074 [US3] Add matrix assertions for exact example/token counts, unique IDs, historical links, primary method names, and one allowed framework decision in `tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`.
- [X] T075 [US3] Add matrix assertions that every primary row names app-loop route, concrete state, view-tree proof, buffer/cell proof, status, description, and evidence path in `tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`.
- [X] T076 [US3] Add matrix guards rejecting startup-only, `VisibleText`-only, history-only, private-inspection-only, `PrimaryProof` helper-only, generic Tutorial, and pending render proof in `tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`.
- [X] T077 [US3] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the expected-failing Wave-1 matrix test.
- [X] T078 [US3] Run the expected-failing Wave-1 matrix Release test and record missing proof fields in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T079 [US3] Populate the Wave-1 matrix with final method names, visible targets, status/description routes, historical sources, helper classifications, render proof, and evidence links in `tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`.
- [X] T080 [US3] Extend `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` only where a generic assertion is needed for exact glyph regions, unique Tutorial states, or proof-layer completeness; add moderate bilingual didactic comments for non-trivial proof limits.
- [X] T081 [US3] Align `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs` with matrix method names and classify every direct helper as `None`, `SetupOnly`, or `SupplementalProof`.
- [X] T082 [US3] Add explicit proof-boundary assertions for clipped Desklogo, repeated MsgCls routing, Tutorial uniqueness, and platform-dependent Videomode outcome in `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`.
- [X] T083 [US3] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the passing Wave-1 matrix test.
- [X] T084 [US3] Run the Wave-1 matrix and targeted app tests in Release and record method/count/proof results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T085 [US3] Reconcile every test matrix row with exactly one review-area row in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T086 [US3] Verify all 16 Tutorial tokens have non-generic representative kinds or distinct states and no duplicate proof identifier in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T087 [US3] Increment the manual build counter and align the three version fields in `Directory.Build.props` before the complete example-smoke suite.
- [X] T088 [US3] Run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` and record total passed/failed/skipped counts in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T089 [US3] Confirm existing feature-014 functional smoke methods still pass and remain supplemental regression evidence in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T090 [US3] Run `git diff --check`, verify no generated/test output or `tv203s/` change, and record the proof checkpoint in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

**Checkpoint**: The authoritative matrix proves all visible runtime paths and rejects weak proof.

---

## Phase 6: User Story 4 - Text-First Learning And Review Path (Priority: P2)

**Goal**: Align learner-facing text, historical traceability, accessibility, and reviewer evidence with the delivered runtime.

**Independent Test**: A text-first reader can follow startup, main state, operation, status, description, source, deviation, and expected result for every scoped path.

- [X] T091 [US4] Update German-first/English-second CEFR-B2 Desklogo startup, main state, status, description, clipping, A11Y, source, and deviation guidance in `docs/guides/examples/desklogo.md`.
- [X] T092 [US4] Update German-first/English-second CEFR-B2 MsgCls trigger, routing result, repeat, status, description, A11Y, source, and deviation guidance in `docs/guides/examples/msgcls.md`.
- [X] T093 [US4] Update German-first/English-second CEFR-B2 Tutorial default/token launch, 16 visual targets, operation, status, description, fallback, A11Y, and historical progression in `docs/guides/examples/tutorial.md`.
- [X] T094 [US4] Update German-first/English-second CEFR-B2 Videomode probe/retry, four outcomes, status, description, platform boundary, A11Y, and source guidance in `docs/guides/examples/videomode.md`.
- [X] T095 [US4] Update Wave-1 visual-remediation completion, three-layer model, actual commands, and proof matrix link in `examples/README.md` without changing Wave-2/3/4 claims.
- [X] T096 [US4] Record guide-to-runtime-to-smoke-to-history traceability for all four apps and 16 Tutorial tokens in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T097 [US4] Review changed Markdown for German-first/English-second order, CEFR-B2 readability, correct umlauts and `ß`, fenced-code language tags, semantic headings/tables, and text-first meaning; record results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T098 [US4] Verify keyboard-only routes and status/description content remain understandable for screen readers, Braille displays, and text browsers and do not depend only on color, pointer input, or layout; record runtime A11Y results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T099 [US4] Review `docfx.json`, `toc.yml`, and guide links for navigation impact and update only affected repository-owned navigation files if required.
- [X] T100 [US4] Record all user-facing documentation files, DocFX trigger, axe trigger, and generated-output hygiene expectations in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T101 [US4] Run `git diff --check` and a Markdown link/reference review for `docs/guides/examples/`, `examples/README.md`, and `specs/017-wave1-visual-component-remediation/`.
- [X] T102 [US4] Confirm every new or updated learner-facing explanation has German first and English second or a documented non-user-facing exception in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T103 [US4] Record the US4 completion checkpoint and any bounded documentation follow-up in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

**Checkpoint**: Runtime, guides, README, and evidence tell the same accessible story.

---

## Phase 7: Governance, Context, And Project Records

**Purpose**: Close architecture, security, A11Y, platform, parity, statistics, and routing obligations.

- [X] T104 Verify the TuiVision Level-2 registry and Constitution gates in `.specify/memory/constitution.md`, run `specify preset list`, and review architecture impact against `docs/architecture/architecture-vision.md`, `docs/architecture/runtime-view.md`, `docs/architecture/quality-scenarios.md`, and `docs/architecture/architecture-risks.md`; update changed facts or record unchanged rationale in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T105 Review NIST SSDF, CWE Top 25, C# secure coding, STRIDE, CIA, CAPEC, S-ADR, and arc42 triggers against `docs/security/security-checklist.md`, `threat-model.md`, `dependency-audit.md`, and `asvs-verification.md`.
- [X] T106 Review SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM, and dependency/release triggers against `docs/security/supply-chain-evidence.md` and record existing-baseline or `N/A` outcomes in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T107 Review Zero Trust and SAMM triggers against `docs/security/zero-trust-applicability.md` and `docs/security/samm-assessment.md`; record `N/A` rationale and re-evaluation triggers when unchanged.
- [X] T108 Review BSI C3A and BSI C5 triggers against `docs/security/cloud-autonomy-applicability.md` and `docs/security/cloud-compliance-assurance.md`; record no-cloud/provider/topology `N/A` rationale and triggers.
- [X] T109 Review NIS2, CRA, EU AI Act, and DORA against `docs/security/regulatory-applicability.md`; record private-training/local-example `N/A` rationale and market/customer/AI/financial re-evaluation triggers.
- [X] T110 Review A11Y governance 0.4.0 for runtime controls, focus, keyboard, status, description, text-first fallback, bilingual content, and didactic comments; record results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T111 Review cross-platform governance 0.2.0 for macOS/Linux/Windows terminal outcomes and record script-shaped tooling as `N/A` unless the implementation actually changed scripts.
- [X] T112 Synchronize final active 017 implementation context and next-step wording across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`.
- [X] T113 Review `.specify/templates/` and record `N/A` unless implementation intentionally changed repository-owned templates in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T114 Update feature completion state and next-step marker to Wave 3 in `Pflichtenheft.md` only after all runtime and proof acceptance requirements pass.
- [X] T115 Update `docs/project-statistics.md` with 017 scope, observable work window, production/test/documentation line counts, commit count, 80/125 lines-per-day baselines, 7.8-hour conversion, chronological ledger row, and final ASCII trend diagrams.
- [X] T116 Complete every governance row with owner, reviewer, date, result, residual risk, follow-up, and re-evaluation trigger in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T117 Review every new/changed non-trivial source and smoke-helper block for moderate German-first/English-second didactic comments and record `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening` in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T118 Verify no API signature, package, runtime service, persistence, arbitrary user-file, Wave-2/3/4, broad framework, generated output, or `tv203s/` scope entered the diff; record results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T119 Run `git diff --check` and record the governance/context checkpoint in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

---

## Phase 8: Final Local Validation

**Purpose**: Execute every local acceptance gate with correct branch versioning and evidence.

- [X] T120 Increment the manual build counter and align the three version fields in `Directory.Build.props` before `dotnet build --configuration Release`.
- [X] T121 Run `dotnet build --configuration Release` and record warning/error totals and version in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T122 Increment the manual build counter and align the three version fields in `Directory.Build.props` before targeted Wave-1 Release tests.
- [X] T123 Run targeted Desklogo, MsgCls, Tutorial, Videomode, and Wave1Visual Release tests and record totals in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T124 Increment the manual build counter and align the three version fields in `Directory.Build.props` before the complete example-smoke suite.
- [X] T125 Run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` and record totals in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T126 Increment the manual build counter and align the three version fields in `Directory.Build.props` before full Release tests.
- [X] T127 Run `dotnet test --configuration Release` and record totals in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T128 Run `xmllint --noout coverlet.runsettings`, increment the manual build counter, and align the three version fields in `Directory.Build.props` before coverage.
- [X] T129 Run `dotnet test --configuration Release --collect:'XPlat Code Coverage' --settings coverlet.runsettings` and record per-assembly line coverage for all five gated assemblies in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T130 Run `dotnet format --verify-no-changes` and record the result in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T131 Run `docfx docfx.json`, record warnings/errors, and verify generated `_site/` and `api/*.yml` output remains ignored/untracked in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T132 Run the `tests/web-a11y` DocFX Playwright/axe path, use the documented explicit loopback-server workaround if required, and record page/violation counts in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T133 Perform normal-start smoke checks with `--configuration Release --no-build` for Desklogo, MsgCls, Tutorial, Videomode, and representative Tutorial `tvguid01`/`tvguid16` paths, including keyboard operation, description, and quit; record bounded results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T134 Run final `git diff --check`, inspect `git status --short`, verify zero prohibited/generated/historical changes, and record local acceptance in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

---

## Phase 9: Archive, Commit, PR, Review, Merge, And Main Sync

**Purpose**: Deliver the completed feature remotely and record closure only after each action actually succeeds.

- [X] T135 Audit SC-001 through SC-012, all four example rows, all 16 Tutorial rows, all framework decisions, all governance checkpoints, all validation runs, and all follow-ups in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T136 Archive `Lastenheft_Wave1-Visual-Component-Remediation.md` through `scripts/rename-lastenheft.ps1` as `Lastenheft_Wave1-Visual-Component-Remediation.017-wave1-visual-component-remediation.md` and record the path in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [X] T137 Refresh `docs/project-statistics.md` after archival and final line counts, keeping `## Gesamtstatistik` as the last top-level section.
- [X] T138 Align `Version`, `AssemblyVersion`, and `FileVersion` in `Directory.Build.props` to `1.17.<current-feature-commit-count-after-this-commit>.<current-build>` without incrementing Build unless another build/test ran.
- [X] T139 Run final secret/credential filename review, `bash scripts/scan-agent-secrets.sh --fail-on-high .`, `git diff --check`, and the repository pre-push validation path; record results in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
- [ ] T140 Commit all accepted feature changes with a Spec-Kit implementation commit, verify commit scope, and record the commit SHA in `specs/017-wave1-visual-component-remediation/pr-evidence.md` through a follow-up commit only if causally necessary.
- [ ] T141 Push branch `017-wave1-visual-component-remediation`, create the PR from the evidence description, and record the PR number without claiming merge completion.
- [ ] T142 Monitor all required GitHub checks and Copilot/Claude review surfaces; address actionable findings, rerun affected local gates with version increments, realign `Directory.Build.props` before every remediation commit/push, reply to and resolve review threads, and repeat until no actionable finding or failing required check remains.
- [ ] T143 Merge the green PR, delete the remote feature branch where permitted, switch locally to `main`, pull `origin/main`, and verify local `main` equals `origin/main`.
- [ ] T144 Record T141-T143 causal outcomes only after they occur; if the merged commit cannot contain those facts, create and merge a minimal closeout PR, then verify `tasks.md` is 144/144 complete and local `main` is clean and synchronized.

---

## Dependencies & Execution Order

### Phase Dependencies

- Phase 1 establishes evidence and blocks all source edits.
- Phase 2 completes historical/design decisions and blocks user stories.
- Phase 3 delivers all visible main states, with MsgCls first.
- Phase 4 completes the three-layer keyboard experience.
- Phase 5 hardens the authoritative visual proof matrix.
- Phase 6 aligns learner-facing documentation and A11Y.
- Phase 7 closes governance and shared project records.
- Phase 8 completes local validation.
- Phase 9 archives and performs causal remote delivery.

### User Story Dependencies

- **US1** depends on Phases 1-2 and supplies runtime states used by all later stories.
- **US2** depends on US1 because operation/status/description proof targets the delivered main states.
- **US3** depends on US1-US2 because the final matrix names real methods and proof regions.
- **US4** depends on delivered runtime and proof so guides cannot document planned-but-undelivered behavior.

### Within Each Slice

1. Add the failing test or matrix requirement.
2. Increment Build and run the expected-failing test.
3. Implement only the bounded behavior.
4. Increment Build and run the passing test.
5. Complete evidence and one framework decision.

## Parallel Opportunities

No task is marked `[P]`. Although historical reads and guide files are distinct,
this autonomous run shares `pr-evidence.md`, project links, agent context,
statistics, and `Directory.Build.props`. Sequential execution prevents stale
evidence and invalid build versions.

## Implementation Strategy

### Vertical Slice First

1. Complete evidence and historical foundation.
2. Deliver MsgCls end-to-end through visible main state, status, description,
   real app-loop proof, and framework decision.
3. Reuse the proven pattern for Desklogo, Tutorial, and Videomode.

### Acceptance Discipline

- Never mark an expected-failing test task complete without recording the
  observed failure boundary.
- Never mark implementation complete before its passing test and evidence row.
- Never mark a governance `N/A` without rationale and re-evaluation trigger.
- Never mark PR, merge, or main-sync tasks complete before the remote action.
