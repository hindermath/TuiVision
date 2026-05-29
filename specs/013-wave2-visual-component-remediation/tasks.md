# Tasks: Wave 2 Visual Component Remediation

**Input**: Design documents from `specs/013-wave2-visual-component-remediation/`
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/wave2-visual-component-acceptance.md`, `checklists/plan-quality.md`
**Feature Branch**: `013-wave2-visual-component-remediation`

**Tests**: Required. The specification requires primary app-loop smoke tests that prove concrete visible control/dialog/focus/selection/scroll/progress/rejection states with view-tree proof plus buffer/cell rendered visibility proof.

**Organization**: Tasks are grouped by user story. Setup and Foundation create evidence, source-review, fixture, and shared smoke infrastructure. US1 delivers visible main components, US2 completes the three-layer runtime model, US3 hardens the smoke proof, and US4 completes learner-ready documentation and governance evidence.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel because it touches different files or independent evidence surfaces.
- **[Story]**: User story label for story phases only.
- Every task names the target file path or evidence file path.

## Phase 1: Setup (Shared Baseline)

**Purpose**: Confirm the checked 013 planning baseline, create the feature evidence surface, and capture the current 012 implementation state before remediation starts.

- [ ] T001 Record `.specify/scripts/bash/check-prerequisites.sh --json`, current branch, `specify preset list`, and the 40/40 checked `checklists/plan-quality.md` readiness result in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T002 Create the 013 evidence matrix in `specs/013-wave2-visual-component-remediation/pr-evidence.md` with rows for `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`
- [ ] T003 Record the starting 012 baseline and current direct-helper/Text-only proof gaps by scanning `examples/*/*App.cs` and `tests/TuiVision.Examples.SmokeTests/*SmokeTests.cs`, then summarize the gaps in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T004 Record `dotnet restore` result and the no-new-runtime-dependency baseline in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T005 Review existing shared smoke helpers in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` and `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`, then record required visibility-proof helper changes in `specs/013-wave2-visual-component-remediation/pr-evidence.md`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Complete historical intent review, controlled-fixture boundaries, and shared rendered-visibility proof support before user-story implementation starts.

**CRITICAL**: No user-story implementation should start until the historical source review and shared smoke-proof foundation are ready.

- [ ] T006 Record the `Clipboard` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/clipboard/test.cc` and `tv203s/contrib/tvision/include/tv/osclipboard.h`
- [ ] T007 Record the `Demo` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/demo/tvdemo1.cc`, `tv203s/contrib/tvision/examples/demo/tvdemo2.cc`, `tv203s/contrib/tvision/examples/demo/tvdemo3.cc`, `tv203s/contrib/tvision/examples/demo/tvdemo.h`, `tv203s/contrib/tvision/examples/demo/tvcmds.h`, `tv203s/contrib/tvision/examples/demo/gadgets.cc`, `tv203s/contrib/tvision/examples/demo/fileview.cc`, `tv203s/contrib/tvision/examples/demo/ascii.cc`, and `tv203s/contrib/tvision/examples/demo/calendar.cc`
- [ ] T008 Record the `DlgDsn` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/dlgdsn/freedsgn.cc`, `tv203s/contrib/tvision/examples/dlgdsn/dsgobjs.cc`, `tv203s/contrib/tvision/examples/dlgdsn/propdlgs.cc`, `tv203s/contrib/tvision/examples/dlgdsn/propedit.cc`, `tv203s/contrib/tvision/examples/dlgdsn/strmoper.cc`, `tv203s/contrib/tvision/examples/dlgdsn/dsgdata.h`, and `tv203s/contrib/tvision/examples/dlgdsn/dsgobjs.h`
- [ ] T009 Record the `DynTxt` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/dyntxt/dyntext.cpp`, `tv203s/contrib/tvision/examples/dyntxt/testdyn.cpp`, and `tv203s/contrib/tvision/examples/dyntxt/dyntext.h`
- [ ] T010 Record the `InpLis` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/inplis/inplist.cpp`, `tv203s/contrib/tvision/examples/inplis/test.cpp`, and `tv203s/contrib/tvision/examples/inplis/inplist.h`
- [ ] T011 Record the `ListVi` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/listvi/lst_view.cpp`, `tv203s/contrib/tvision/examples/listvi/listbox2.cpp`, `tv203s/contrib/tvision/examples/listvi/lst_view.h`, and `tv203s/contrib/tvision/classes/tlistvie.cc`
- [ ] T012 Record the `ProgBa` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/progba/example.cpp`, `tv203s/contrib/tvision/examples/progba/tprogbar.cpp`, `tv203s/contrib/tvision/examples/progba/tprogbar.h`, `tv203s/contrib/tvision/examples/progba/makerez.cpp`, and `tv203s/contrib/tvision/examples/progba/readrez.cpp`
- [ ] T013 Record the `Sdlg` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/sdlg/main.cpp`, `tv203s/contrib/tvision/examples/sdlg/scrldlg.cpp`, `tv203s/contrib/tvision/examples/sdlg/scrlgrp.cpp`, and `tv203s/contrib/tvision/examples/sdlg/dlg.h`
- [ ] T014 Record the `Sdlg2` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/sdlg2/main.cpp`, `tv203s/contrib/tvision/examples/sdlg2/scrldlg.cpp`, `tv203s/contrib/tvision/examples/sdlg2/scrlgrp.cpp`, and `tv203s/contrib/tvision/examples/sdlg2/dlg.h`
- [ ] T015 Record the `TCombo` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/tcombo/test.cpp`, `tv203s/contrib/tvision/examples/tcombo/tcombobx.cpp`, `tv203s/contrib/tvision/examples/tcombo/tcombobx.h`, `tv203s/contrib/tvision/examples/tcombo/tcmbovwr.cpp`, `tv203s/contrib/tvision/examples/tcombo/tcmbowin.cpp`, `tv203s/contrib/tvision/examples/tcombo/tsinputl.cpp`, and `tv203s/contrib/tvision/examples/tcombo/tsinputl.h`
- [ ] T016 Record the `TProgB` historical source review in `specs/013-wave2-visual-component-remediation/pr-evidence.md` using `tv203s/contrib/tvision/examples/tprogb/calc.cpp`, `tv203s/contrib/tvision/examples/tprogb/tprogbar.cpp`, and `tv203s/contrib/tvision/examples/tprogb/tprogbar.h`
- [ ] T017 Record controlled fixture and temp-directory boundaries for `examples/DlgDsn/Fixtures/*.tvdialog`, clipboard test doubles, and file/path metadata proof in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T018 Add or extend shared view-tree and buffer/cell rendered-visibility assertion helpers in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs`
- [ ] T019 Add or extend deterministic app-loop event sequencing support for rendered-visibility smokes in `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`
- [ ] T020 Add baseline matrix fields for visible target, status feedback, description path, rendered proof, direct-helper classification, and evidence link in `tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs`

**Checkpoint**: Historical source review, fixture safety, and shared rendered-visibility proof support are ready.

---

## Phase 3: User Story 1 - Visible component parity per example (Priority: P1) MVP

**Goal**: Every scoped Wave-2 example shows a real visible main component or stable visual runtime state that represents the historical visual idea; `Demo` is the first vertical slice.

**Independent Test**: Start `examples/Clipboard`, `examples/Demo`, `examples/DlgDsn`, `examples/DynTxt`, `examples/InpLis`, `examples/ListVi`, `examples/ProgBa`, `examples/Sdlg`, `examples/Sdlg2`, `examples/TCombo`, and `examples/TProgB`, then run the matching example-specific smoke filter to verify visible target state through app-loop proof.

### Tests for User Story 1

- [ ] T021 [US1] Add failing app-loop rendered-visibility smokes for `Demo` `Dialog/Control`, `File/Path metadata`, and `Display/Color/Gadget` visible families in `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs`
- [ ] T022 [P] [US1] Add failing app-loop rendered-visibility smokes for `Clipboard` copy, cut, paste, and unavailable visible states in `tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs`
- [ ] T023 [P] [US1] Add failing app-loop rendered-visibility smokes for `DynTxt`, `InpLis`, `ListVi`, and `TCombo` visible dynamic text/input/list/combo states in `tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs`
- [ ] T024 [P] [US1] Add failing app-loop rendered-visibility smokes for `ProgBa` and `TProgB` progress completion, partial, abort, and cancel visible states in `tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs` and `tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs`
- [ ] T025 [P] [US1] Add failing app-loop rendered-visibility smokes for `DlgDsn`, `Sdlg`, and `Sdlg2` dialog/rejection, one-axis scroll, and two-axis scroll visible states in `tests/TuiVision.Examples.SmokeTests/DlgDsnSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/SdlgSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/Sdlg2SmokeTests.cs`

### Implementation for User Story 1

- [ ] T026 [US1] Replace `Demo` text-only primary output with real visible `Dialog/Control`, `File/Path metadata`, and `Display/Color/Gadget` view compositions in `examples/Demo/DemoApp.cs`
- [ ] T027 [P] [US1] Replace `Clipboard` text-only primary proof with a visible text/input component that preserves copy, cut, paste, and unavailable states in `examples/Clipboard/ClipboardApp.cs`
- [ ] T028 [P] [US1] Replace `DynTxt` text-only primary proof with a dynamic text view that demonstrates changed, clipped, aligned, or narrow-width content in `examples/DynTxt/DynTxtApp.cs`
- [ ] T029 [P] [US1] Replace `InpLis` text-only primary proof with a visible dialog composition containing list, input, history or boundary behavior in `examples/InpLis/InpLisApp.cs`
- [ ] T030 [P] [US1] Replace `ListVi` text-only primary proof with a visible list viewer/list box with selection and boundary or empty-state feedback in `examples/ListVi/ListViApp.cs`
- [ ] T031 [P] [US1] Replace `TCombo` text-only primary proof with a visible input-plus-combo or selection composition with value and boundary/empty behavior in `examples/TCombo/TComboApp.cs`
- [ ] T032 [P] [US1] Replace `ProgBa` text-only primary proof with a visible progress-bar state through completion in `examples/ProgBa/ProgBaApp.cs`
- [ ] T033 [P] [US1] Replace `TProgB` text-only primary proof with a visible progress dialog/window covering partial progress, abort, and cancelled states in `examples/TProgB/TProgBApp.cs`
- [ ] T034 [P] [US1] Replace `DlgDsn` text-only primary proof with a visible dialog/control tree for valid descriptions and visible rejection state for invalid fixtures in `examples/DlgDsn/DlgDsnApp.cs`
- [ ] T035 [P] [US1] Replace `Sdlg` text-only primary proof with a visible scroll-dialog or scroll-group state containing content outside the initial viewport in `examples/Sdlg/SdlgApp.cs`
- [ ] T036 [P] [US1] Replace `Sdlg2` text-only primary proof with visible horizontal and vertical scroll-dialog/scroll-group behavior in `examples/Sdlg2/Sdlg2App.cs`
- [ ] T037 [US1] Record each example's visible target state, historical-source match, and intentional deviation status in `specs/013-wave2-visual-component-remediation/pr-evidence.md`

**Checkpoint**: User Story 1 is independently testable: all eleven examples have visible main components or stable visible runtime states.

---

## Phase 4: User Story 2 - Three-layer runtime experience (Priority: P1)

**Goal**: Every example follows the same three-layer model: visible main component, real `TStatusLine` feedback or documented equivalent, and keyboard-reachable `Help -> Description`.

**Independent Test**: For each example, identify the visible main component, trigger a short status update, and reach `Help -> Description` without reading source code.

### Tests for User Story 2

- [ ] T038 [US2] Add failing smoke coverage for `Demo` `TStatusLine` feedback and `Help -> Description` reachability/content in `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs`
- [ ] T039 [P] [US2] Add failing smoke coverage for `Clipboard`, `DynTxt`, `InpLis`, `ListVi`, and `TCombo` `TStatusLine` feedback and `Help -> Description` reachability/content in `tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs`
- [ ] T040 [P] [US2] Add failing smoke coverage for `ProgBa` and `TProgB` `TStatusLine` feedback and `Help -> Description` reachability/content in `tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs` and `tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs`
- [ ] T041 [P] [US2] Add failing smoke coverage for `DlgDsn`, `Sdlg`, and `Sdlg2` `TStatusLine` feedback and `Help -> Description` reachability/content in `tests/TuiVision.Examples.SmokeTests/DlgDsnSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/SdlgSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/Sdlg2SmokeTests.cs`

### Implementation for User Story 2

- [ ] T042 [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `Demo` in `examples/Demo/DemoApp.cs`
- [ ] T043 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `Clipboard` in `examples/Clipboard/ClipboardApp.cs`
- [ ] T044 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `DynTxt` in `examples/DynTxt/DynTxtApp.cs`
- [ ] T045 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `InpLis` in `examples/InpLis/InpLisApp.cs`
- [ ] T046 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `ListVi` in `examples/ListVi/ListViApp.cs`
- [ ] T047 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `TCombo` in `examples/TCombo/TComboApp.cs`
- [ ] T048 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `ProgBa` in `examples/ProgBa/ProgBaApp.cs`
- [ ] T049 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `TProgB` in `examples/TProgB/TProgBApp.cs`
- [ ] T050 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `DlgDsn` in `examples/DlgDsn/DlgDsnApp.cs`
- [ ] T051 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `Sdlg` in `examples/Sdlg/SdlgApp.cs`
- [ ] T052 [P] [US2] Wire real `TStatusLine` feedback and keyboard-reachable `Help -> Description` for `Sdlg2` in `examples/Sdlg2/Sdlg2App.cs`
- [ ] T053 [US2] Record any equivalent-status-area deviation or `Help -> Description` deviation as a blocking finding in `specs/013-wave2-visual-component-remediation/pr-evidence.md`

**Checkpoint**: User Story 2 is independently testable: all eleven examples expose the three-layer runtime model.

---

## Phase 5: User Story 3 - Smoke tests prove visible controls and dialogs (Priority: P2)

**Goal**: Primary smoke tests prove real visible UI composition through the app loop, concrete state assertions, view-tree proof, and buffer/cell snapshots. Direct helpers remain setup or supplemental only.

**Independent Test**: Run the Wave-2 smoke filter and confirm every scoped example appears in the matrix with app-loop state proof plus rendered visibility proof.

### Tests for User Story 3

- [ ] T054 [US3] Add or update the Wave-2 visual remediation matrix so all eleven primary scenarios are listed with rendered-visibility proof status in `tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs`
- [ ] T055 [US3] Add guard assertions that primary Wave-2 smokes do not pass using only `VisibleText`, `VisibleHistory`, or direct helper output in `tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs`
- [ ] T056 [P] [US3] Convert `Demo`, `Clipboard`, and `DlgDsn` legacy direct-helper-only proof to setup/supplemental classification or remove it from primary proof in `tests/TuiVision.Examples.SmokeTests/DemoSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/DlgDsnSmokeTests.cs`
- [ ] T057 [P] [US3] Convert `DynTxt`, `InpLis`, `ListVi`, and `TCombo` legacy direct-helper-only proof to setup/supplemental classification or remove it from primary proof in `tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs`
- [ ] T058 [P] [US3] Convert `ProgBa`, `TProgB`, `Sdlg`, and `Sdlg2` legacy direct-helper-only proof to setup/supplemental classification or remove it from primary proof in `tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs`, `tests/TuiVision.Examples.SmokeTests/SdlgSmokeTests.cs`, and `tests/TuiVision.Examples.SmokeTests/Sdlg2SmokeTests.cs`

### Implementation for User Story 3

- [ ] T059 [US3] Align `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` rendered-visibility assertions with all primary Wave-2 smoke classes and record helper usage policy in XML comments
- [ ] T060 [US3] Align `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs` with deterministic app-loop event and quit-path requirements for all primary Wave-2 smokes
- [ ] T061 [US3] Align `Directory.Build.props` build counter, run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release`, and record the fast smoke output in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T062 [US3] Record per-example primary-smoke method names, direct-helper classification, concrete state assertions, view-tree proof, and buffer/cell snapshot proof in `specs/013-wave2-visual-component-remediation/pr-evidence.md`

**Checkpoint**: User Story 3 is independently testable: every primary smoke proves visible composition, not only text status.

---

## Phase 6: User Story 4 - Learner-ready documentation and evidence (Priority: P2)

**Goal**: Guides and evidence explain the visible behavior in German first and English second, with text-first/A11Y review paths and traceability from historical source to smoke proof.

**Independent Test**: Read each updated guide and `pr-evidence.md`; trace historical source, visible target, status feedback, `Help -> Description`, smoke proof, A11Y notes, and deviations for each example.

### Documentation for User Story 4

- [ ] T063 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/clipboard.md`
- [ ] T064 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/demo.md`
- [ ] T065 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, fixture boundaries, and deviation notes in `docs/guides/examples/dlgdsn.md`
- [ ] T066 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/dyntxt.md`
- [ ] T067 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/inplis.md`
- [ ] T068 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/listvi.md`
- [ ] T069 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/progba.md`
- [ ] T070 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, scroll/focus explanation, and deviation notes in `docs/guides/examples/sdlg.md`
- [ ] T071 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, two-axis scroll/focus explanation, and deviation notes in `docs/guides/examples/sdlg2.md`
- [ ] T072 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/tcombo.md`
- [ ] T073 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, `Help -> Description`, A11Y notes, historical-source relationship, and deviation notes in `docs/guides/examples/tprogb.md`
- [ ] T074 [US4] Update German-first/English-second CEFR-B2 Wave-2 overview and learner-ready visual-remediation status in `examples/README.md`
- [ ] T075 [US4] Update cross-example German-first/English-second CEFR-B2 text-first/A11Y and guide-review notes in `docs/guides/examples/wave2-guide-review-notes.md`
- [ ] T076 [US4] Record guide-to-runtime-to-smoke traceability, German-first/English-second CEFR-B2 evidence, and A11Y review status for all eleven examples in `specs/013-wave2-visual-component-remediation/pr-evidence.md`

**Checkpoint**: User Story 4 is independently testable: documentation and evidence explain the visible runtime behavior.

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Complete governance evidence, final validation, versioning, statistics, Lastenheft archiving, and delivery preparation.

- [ ] T077 [P] Verify architecture impact and update or record unchanged rationale for `docs/architecture/architecture-vision.md`, `docs/architecture/runtime-view.md`, `docs/architecture/quality-scenarios.md`, and `docs/architecture/architecture-risks.md`
- [ ] T078 [P] Verify NIST SSDF/CWE posture, `security-governance` v0.4.0, and unchanged-risk rationale in `docs/security/security-checklist.md`, `docs/security/threat-model.md`, `docs/security/dependency-audit.md`, and `docs/security/asvs-verification.md`
- [ ] T079 [P] Verify supply-chain, SBOM/VEX/SLSA, AI-SBOM `N/A`, non-applicable non-C# secure-coding profiles, and Zero Trust/SAMM applicability in `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`
- [ ] T080 [P] Update implementation completion status, Wave-2 visual remediation status, and next-step marker in `Pflichtenheft.md`
- [ ] T081 [P] Update project statistics for 013 implementation scope, validation evidence, manual baselines, and acceleration notes in `docs/project-statistics.md`
- [ ] T082 [P] Review agent guidance parity and update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` only if implementation changed active feature context, technologies, project structure, or shared workflow rules; otherwise record unchanged rationale in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T083 Align `Directory.Build.props` to branch version `1.13.<patch>.<build>` and increment the manual build counter before final build/test commands in `Directory.Build.props`
- [ ] T084 Run `dotnet build --configuration Release` and record command output in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T085 Run `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` and record command output in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T086 Run full `dotnet test --configuration Release` and record command output in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T087 Run coverage gate `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` and record per-assembly coverage evidence in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T088 Run `dotnet format --verify-no-changes` and record command output in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T089 Because US4 changes guide/README documentation, run `docfx docfx.json` unless implementation evidence explicitly proves DocFX output/navigation is unaffected, and record generated-output hygiene for `_site/` and `api/*.yml` in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T090 If T089 runs, run `npm run test:docfx` from `tests/web-a11y/` and record Playwright/axe evidence in `specs/013-wave2-visual-component-remediation/pr-evidence.md`; if T089 is explicitly N/A, record the N/A rationale there instead
- [ ] T091 Run one manual startup check with `dotnet run --project examples/Clipboard --configuration Release --no-build`, `dotnet run --project examples/Demo --configuration Release --no-build`, `dotnet run --project examples/DlgDsn --configuration Release --no-build`, `dotnet run --project examples/DynTxt --configuration Release --no-build`, `dotnet run --project examples/InpLis --configuration Release --no-build`, `dotnet run --project examples/ListVi --configuration Release --no-build`, `dotnet run --project examples/ProgBa --configuration Release --no-build`, `dotnet run --project examples/Sdlg --configuration Release --no-build`, `dotnet run --project examples/Sdlg2 --configuration Release --no-build`, `dotnet run --project examples/TCombo --configuration Release --no-build`, and `dotnet run --project examples/TProgB --configuration Release --no-build`, including primary visible operation path and `Help -> Description`, then record results in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T092 Run `git diff --check` and record the clean diff-check result in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T093 Update PR description or final feature evidence with changed examples, validation commands, representative visual snippets, security-risk statement, AI-SBOM `N/A`, and explicit confirmation that no Wave-3/Wave-4 behavior, mouse-only path, broad framework redesign, database, external service, network dependency, or persistent user history was added in `specs/013-wave2-visual-component-remediation/pr-evidence.md`
- [ ] T094 Run `bash scripts/rename-lastenheft.sh Lastenheft_Wave2-Visual-Component-Remediation.md 013-wave2-visual-component-remediation` or `pwsh scripts/rename-lastenheft.ps1 -File Lastenheft_Wave2-Visual-Component-Remediation.md -BranchName 013-wave2-visual-component-remediation`, then record the resulting Lastenheft path in `specs/013-wave2-visual-component-remediation/pr-evidence.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies.
- **Foundational (Phase 2)**: Depends on Setup and blocks all user stories.
- **US1 Visible component parity (Phase 3)**: Depends on Foundational and is the MVP.
- **US2 Three-layer runtime model (Phase 4)**: Depends on US1 visible components for the same examples.
- **US3 Smoke proof hardening (Phase 5)**: Depends on US1 and US2 runtime paths.
- **US4 Documentation and evidence (Phase 6)**: Depends on the matching example behavior from US1/US2 and smoke proof from US3.
- **Polish (Phase 7)**: Depends on all desired user stories.

### User Story Dependencies

- **US1 (P1)**: MVP after Foundation; `Demo` must be completed before spreading the pattern.
- **US2 (P1)**: Builds on US1 visible components and can proceed per example after that example has a main component.
- **US3 (P2)**: Requires the runtime behavior from US1/US2.
- **US4 (P2)**: Requires stable runtime and smoke behavior for each documented example.

### Within Each User Story

- Add or update smoke tasks before implementation tasks where the story requires smoke proof.
- Keep direct helpers as setup or supplemental evidence only.
- Record each completed example in `pr-evidence.md` before final validation.
- Serialize writes to shared Markdown evidence files to avoid conflicting edits.

## Parallel Opportunities

- T021-T025 can be prepared in parallel by smoke-test file after Foundation.
- T027-T036 can be implemented in parallel after the `Demo` pattern in T026 is accepted.
- T038-T041 can be prepared in parallel by smoke-test family.
- T043-T052 can be implemented in parallel by example after the related US1 task is complete.
- T063-T073 can be updated in parallel because each guide is a separate file.
- T077-T082 can run in parallel because each governance surface is independent.

## Parallel Example: User Story 1

```text
Task: "T022 [P] [US1] Add failing app-loop rendered-visibility smokes for Clipboard copy, cut, paste, and unavailable visible states in tests/TuiVision.Examples.SmokeTests/ClipboardSmokeTests.cs"
Task: "T023 [P] [US1] Add failing app-loop rendered-visibility smokes for DynTxt, InpLis, ListVi, and TCombo visible dynamic text/input/list/combo states in tests/TuiVision.Examples.SmokeTests/DynTxtSmokeTests.cs, tests/TuiVision.Examples.SmokeTests/InpLisSmokeTests.cs, tests/TuiVision.Examples.SmokeTests/ListViSmokeTests.cs, and tests/TuiVision.Examples.SmokeTests/TComboSmokeTests.cs"
Task: "T024 [P] [US1] Add failing app-loop rendered-visibility smokes for ProgBa and TProgB progress completion, partial, abort, and cancel visible states in tests/TuiVision.Examples.SmokeTests/ProgBaSmokeTests.cs and tests/TuiVision.Examples.SmokeTests/TProgBSmokeTests.cs"
```

## Parallel Example: User Story 4

```text
Task: "T063 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, Help -> Description, A11Y notes, historical-source relationship, and deviation notes in docs/guides/examples/clipboard.md"
Task: "T065 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, Help -> Description, A11Y notes, historical-source relationship, fixture boundaries, and deviation notes in docs/guides/examples/dlgdsn.md"
Task: "T073 [P] [US4] Update German-first/English-second CEFR-B2 guide content for visible main component, operation path, status feedback, Help -> Description, A11Y notes, historical-source relationship, and deviation notes in docs/guides/examples/tprogb.md"
```

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 and Phase 2.
2. Complete `Demo` tasks T021 and T026 as the vertical slice.
3. Validate `Demo` manually with `dotnet run --project examples/Demo`.
4. Validate `Demo` smoke proof with `dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Demo"`.
5. Stop and review the visible composition pattern before applying it to remaining examples.

### Incremental Delivery

1. Deliver `Demo` as the visible vertical slice.
2. Deliver remaining examples by families: `Clipboard`; `DynTxt`/`InpLis`/`ListVi`/`TCombo`; `ProgBa`/`TProgB`; `DlgDsn`/`Sdlg`/`Sdlg2`.
3. Add `TStatusLine` and `Help -> Description` per example.
4. Harden primary smokes and direct-helper classification.
5. Update guides, evidence, governance surfaces, statistics, and final validation.
