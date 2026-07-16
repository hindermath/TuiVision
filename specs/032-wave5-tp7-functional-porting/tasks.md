# Tasks: Wave-5 TP7 Functional Porting

**Input**: Accepted artifacts under
`specs/032-wave5-tp7-functional-porting/` and binding
`Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md`
**Delivery mode**: `MergeAndSync`
**Scope**: Functional Stage 1 only; no Feature 033 or Wave 6 start

## Phase 1: Setup and Evidence Foundation

**Purpose**: Lock identity, authority, scope, evidence and project structure
before executable changes.

- [X] T001 Verify branch, `.specify/feature.json`, baseline ancestry and dirty-path ownership in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T002 Record Feature-031 PR #90 and closeout PR #91 merge ancestry in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T003 Record Wave-5 intake PR #92 and merge commit `269c54f5f882c69e21f46f97d3e89a938bfb568f` in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T004 Run `specify check` and prerequisite checks and record exit/error-channel review in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T005 Verify the seven installed preset versions and priorities in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T006 Confirm all 97 feature checklist items are complete in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T007 Validate `specs/032-wave5-tp7-functional-porting/autonomous-run-state.json` with the installed Bash validator
- [X] T008 Record local PowerShell availability and Windows parity boundary in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T009 Validate `specs/032-wave5-tp7-functional-porting/autonomous-gate-requirements.json` as UTF-8 JSON and record SHA-256 in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T010 Record all hard scope exclusions and read-only roots in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T011 Record shared single-writer paths and serialization rules in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T012 Record Feature-032 version scheme and one-build-counter-increment-per-command rule in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T013 Record that no intentional interruption, Feature 033, Wave 6 or TVFM implementation is permitted in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T014 Record exact 15-source, six-consumer, ten-example, ten-proof and ten-delta cardinalities in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T015 Record the potential non-recursive closeout path `specs/032-wave5-tp7-functional-porting/delivery-closeout.md`
- [X] T016 Inventory all intended new project, test, guide, evidence and status paths in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T017 Confirm no generated output, external checkout, credential, cache, log or test result is intended for Git in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T018 Review `.gitignore` coverage for .NET, DocFX, Node and test output without broad unrelated edits
- [X] T019 Record the current Community preset issue `github/spec-kit#3569` as non-blocking external context in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T020 Update `specs/032-wave5-tp7-functional-porting/autonomous-run-state.json` to the validated Tasks checkpoint after task generation

---

## Phase 2: Foundational Project and Proof Infrastructure

**Purpose**: Establish the shared compiled Wave-5 assembly, ten launch
projects and complete compile/test surface before the first Red test.

- [X] T021 Create `examples/Shared/TuiVision.Examples.Wave5/TuiVision.Examples.Wave5.csproj` with existing framework references and no new package
- [X] T022 Create the common headless event/status/view shell in `examples/Shared/TuiVision.Examples.Wave5/Wave5Application.cs`
- [X] T023 Add German-first/English-second XML docs and why-focused comments to non-trivial shell logic in `examples/Shared/TuiVision.Examples.Wave5/Wave5Application.cs`
- [X] T024 Create deterministic calculator, ASCII, calendar and puzzle models in `examples/Shared/TuiVision.Examples.Wave5/Wave5Domain.cs`
- [X] T025 Add invariant numeric, fixed-date, fixed-board and bounded-state validation in `examples/Shared/TuiVision.Examples.Wave5/Wave5Domain.cs`
- [X] T026 Add public XML docs and didactic comments to non-trivial domain transitions in `examples/Shared/TuiVision.Examples.Wave5/Wave5Domain.cs`
- [X] T027 Create executable project and normal plus `--smoke` entry point under `examples/Tp7Demo/`
- [X] T028 Create executable project and normal plus `--smoke` entry point under `examples/Tp7Edit/`
- [X] T029 Create executable project and normal plus `--smoke` entry point under `examples/Tp7Help/`
- [X] T030 Create executable project and normal plus `--smoke` entry point under `examples/Tp7ResourceDemo/`
- [X] T031 Create executable project and normal plus `--smoke` entry point under `examples/Tp7ResourceGenerator/`
- [X] T032 Create executable project and normal plus `--smoke` entry point under `examples/Tp7AsciiTable/`
- [X] T033 Create executable project and normal plus `--smoke` entry point under `examples/Tp7Calculator/`
- [X] T034 Create executable project and normal plus `--smoke` entry point under `examples/Tp7Calendar/`
- [X] T035 Create executable project and normal plus `--smoke` entry point under `examples/Tp7Puzzle/`
- [X] T036 Create executable project and normal plus `--smoke` entry point under `examples/Tp7MouseDialog/`
- [X] T037 Add the shared assembly and ten executables to `TuiVision.sln`
- [X] T038 Add the shared Wave-5 assembly reference to `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj`
- [X] T039 Review the complete compile surface including imports, constructors, XML-doc warnings, test helpers and CLR type identity and record it in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T040 Confirm all ten project names are unique and all normal launch commands resolve in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T041 Confirm no framework project, dependency or package file changed while establishing the foundation

**Checkpoint**: The complete project surface exists; no functional example
contract is claimed complete.

---

## Phase 3: User Story 1 - Calculator Reference Slice (Priority: P1)

**Goal**: Deliver the first complete historical-to-modern app-loop proof.

**Independent Test**: Valid arithmetic and division rejection run through
`app.Run()` and prove state, `TWindow` identity and rendered cells.

- [X] T042 [US1] Add missing/failing valid-calculation app-loop test in `tests/TuiVision.Examples.SmokeTests/Tp7CalculatorSmokeTests.cs`
- [X] T043 [US1] Add missing/failing division-by-zero preservation test in `tests/TuiVision.Examples.SmokeTests/Tp7CalculatorSmokeTests.cs`
- [X] T044 [US1] Add missing/failing constrained-layout first-frame test in `tests/TuiVision.Examples.SmokeTests/Tp7CalculatorSmokeTests.cs`
- [X] T045 [US1] Increment `Directory.Build.props` manual build counter for the Calculator Red invocation
- [X] T046 [US1] Run the Calculator-only Release tests Red and accept only missing implementation failures
- [X] T047 [US1] Record Calculator Red command, version, exit/error review and failure boundary in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T048 [US1] Implement `Tp7CalculatorApp` command, display, status and visible window in `examples/Shared/TuiVision.Examples.Wave5/Tp7CalculatorApp.cs`
- [X] T049 [US1] Implement invariant digit, decimal, sign, operation, equals, clear and backspace paths in `examples/Shared/TuiVision.Examples.Wave5/Tp7CalculatorApp.cs`
- [X] T050 [US1] Preserve the last valid value and show text-first rejection on division by zero in `examples/Shared/TuiVision.Examples.Wave5/Tp7CalculatorApp.cs`
- [X] T051 [US1] Add XML docs and moderate historical/proof comments in `examples/Shared/TuiVision.Examples.Wave5/Tp7CalculatorApp.cs`
- [X] T052 [US1] Increment `Directory.Build.props` manual build counter for the Calculator Green invocation
- [X] T053 [US1] Run the Calculator-only Release tests Green
- [X] T054 [US1] Record Calculator state/view/cell and negative proof in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T055 [US1] Create `docs/guides/examples/tp7-calculator.md` with purpose, source, launch, keyboard path, modernization, A11Y and proof boundary
- [X] T056 [US1] Complete the `CALC.PAS` source row in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T057 [US1] Complete the calculator part of consumer W5-005 in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T058 [US1] Complete the `Tp7Calculator` primary proof row in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T059 [US1] Draft the concrete `Tp7Calculator` showcase-delta row in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`

**Checkpoint**: The reusable project, test, guide and evidence pattern is
proven before broader rollout.

---

## Phase 4: User Story 2 - Demo, Editor and Help (Priority: P2)

**Goal**: Deliver central application, file/editor and help flows.

**Independent Test**: Three separate apps run their real loops and prove
exactly-once commands, controlled file state and atomic Help behavior.

- [X] T060 [US2] Add failing/missing Demo menu, status, Help, exactly-once command, window and repeated bounded-idle/gadget tests in `tests/TuiVision.Examples.SmokeTests/Tp7ApplicationSmokeTests.cs`
- [X] T061 [US2] Add failing/missing Editor modify, safe-close, conflict, controlled-save and rejected-path tests in `tests/TuiVision.Examples.SmokeTests/Tp7ApplicationSmokeTests.cs`
- [X] T062 [US2] Add failing/missing Help compile, invalid-source, context and fallback tests in `tests/TuiVision.Examples.SmokeTests/Tp7ApplicationSmokeTests.cs`
- [X] T063 [US2] Increment `Directory.Build.props` manual build counter for the central-app Red invocation
- [X] T064 [US2] Run the three central-app Release test groups Red and accept only missing implementation failures
- [X] T065 [US2] Record central-app Red command and failure matrix in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T066 [US2] Implement Demo menu, StatusLine, Help context, typed commands, visible windows and exactly-once handling in `examples/Shared/TuiVision.Examples.Wave5/Tp7DemoApp.cs`
- [X] T067 [US2] Implement repeated bounded deterministic idle/gadget cycles without host heap semantics in `examples/Shared/TuiVision.Examples.Wave5/Tp7DemoApp.cs`
- [X] T068 [US2] Implement `Tp7EditApp` with `TFileEditor`, `TEditWindow`, modified state, safe-close and explicit overwrite/conflict decisions in `examples/Shared/TuiVision.Examples.Wave5/Tp7EditApp.cs`
- [X] T069 [US2] Implement controlled-root save acceptance, conflict result and traversal rejection in `examples/Shared/TuiVision.Examples.Wave5/Tp7EditApp.cs`
- [X] T070 [US2] Implement `Tp7HelpApp` valid/invalid compile and no-partial-model state in `examples/Shared/TuiVision.Examples.Wave5/Tp7HelpApp.cs`
- [X] T071 [US2] Implement known-context viewer and unknown-context fallback in `examples/Shared/TuiVision.Examples.Wave5/Tp7HelpApp.cs`
- [X] T072 [US2] Add complete public XML docs and why-focused comments to all three central app files
- [X] T073 [US2] Increment `Directory.Build.props` manual build counter for the central-app Green invocation
- [X] T074 [US2] Run the three central-app Release test groups Green
- [X] T075 [US2] Record Demo, Editor and Help state/view/cell proof in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T076 [US2] Create `docs/guides/examples/tp7-demo.md`
- [X] T077 [US2] Create `docs/guides/examples/tp7-edit.md`
- [X] T078 [US2] Create `docs/guides/examples/tp7-help.md`
- [X] T079 [US2] Complete source rows for `TVDEMO.PAS`, `DEMOCMDS.PAS`, `DEMOSTRS.PAS` and `GADGETS.PAS`
- [X] T080 [US2] Complete source row for `TVEDIT.PAS`
- [X] T081 [US2] Complete source rows for `TVHC.PAS`, `HELPFILE.PAS` and `DEMOHELP.PAS`
- [X] T082 [US2] Complete consumer decisions W5-001 and W5-002 in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T083 [US2] Complete `Tp7Demo`, `Tp7Edit` and `Tp7Help` primary proof rows
- [X] T084 [US2] Draft concrete showcase-delta rows for Demo, Editor and Help

---

## Phase 5: User Story 3 - Resource Demo and Generator (Priority: P3)

**Goal**: Deliver named resources and generation through the existing closed
serialization contract.

**Independent Test**: The generator writes only below a controlled root and
the demo loads exact keys; malformed or unallowed records are atomic failures.

- [X] T085 [US3] Add failing/missing generator controlled-output test in `tests/TuiVision.Examples.SmokeTests/Tp7ResourceSmokeTests.cs`
- [X] T086 [US3] Add failing/missing exact-key resource reconstruction test in `tests/TuiVision.Examples.SmokeTests/Tp7ResourceSmokeTests.cs`
- [X] T087 [US3] Add failing/missing duplicate, unknown, invalid-length and traversal rejection matrix in `tests/TuiVision.Examples.SmokeTests/Tp7ResourceSmokeTests.cs`
- [X] T088 [US3] Increment `Directory.Build.props` manual build counter for the Resource Red invocation
- [X] T089 [US3] Run Resource Release tests Red and accept only missing implementation failures
- [X] T090 [US3] Record Resource Red command and each expected failure boundary in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T091 [US3] Implement allowlisted resource generation in `examples/Shared/TuiVision.Examples.Wave5/Tp7ResourceApps.cs`
- [X] T092 [US3] Implement controlled-root output and traversal rejection in `examples/Shared/TuiVision.Examples.Wave5/Tp7ResourceApps.cs`
- [X] T093 [US3] Implement exact-key atomic resource load and visible reconstruction in `examples/Shared/TuiVision.Examples.Wave5/Tp7ResourceApps.cs`
- [X] T094 [US3] Add XML docs and security/proof-boundary comments in `examples/Shared/TuiVision.Examples.Wave5/Tp7ResourceApps.cs`
- [X] T095 [US3] Increment `Directory.Build.props` manual build counter for the Resource Green invocation
- [X] T096 [US3] Run Resource Release tests Green
- [X] T097 [US3] Create `docs/guides/examples/tp7-resource-demo.md`
- [X] T098 [US3] Create `docs/guides/examples/tp7-resource-generator.md`
- [X] T099 [US3] Complete source rows for `TVRDEMO.PAS` and `GENRDEMO.PAS`
- [X] T100 [US3] Complete consumer decisions W5-003 and W5-004
- [X] T101 [US3] Complete Resource Demo and Generator primary proof rows
- [X] T102 [US3] Draft concrete showcase-delta rows for both resource examples

---

## Phase 6: User Story 4 - ASCII, Calendar, Puzzle and Mouse (Priority: P4)

**Goal**: Deliver deterministic domain state plus bounded mouse capability
with complete keyboard parity.

**Independent Test**: Four domain apps and the mouse app run real loops with
fixed state, rejected boundaries and visible fallback.

- [X] T103 [US4] Add failing/missing ASCII navigation and boundary tests in `tests/TuiVision.Examples.SmokeTests/Tp7DomainSmokeTests.cs`
- [X] T104 [US4] Add failing/missing fixed-date Calendar rollover tests in `tests/TuiVision.Examples.SmokeTests/Tp7DomainSmokeTests.cs`
- [X] T105 [US4] Add failing/missing fixed-board Puzzle move and rejection tests in `tests/TuiVision.Examples.SmokeTests/Tp7DomainSmokeTests.cs`
- [X] T106 [US4] Add failing/missing Mouse supported, Unsupported, mid-interaction capability-loss and keyboard-parity tests in `tests/TuiVision.Examples.SmokeTests/Tp7DomainSmokeTests.cs`
- [X] T107 [US4] Increment `Directory.Build.props` manual build counter for the domain/mouse Red invocation
- [X] T108 [US4] Run domain/mouse Release tests Red and accept only missing implementation failures
- [X] T109 [US4] Record domain/mouse Red command and failure matrix in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T110 [US4] Implement `Tp7AsciiTableApp` navigation, direct selection and visible decimal/hex state in `examples/Shared/TuiVision.Examples.Wave5/Tp7DomainApps.cs`
- [X] T111 [US4] Implement `Tp7CalendarApp` fixed-date month navigation and year rollover in `examples/Shared/TuiVision.Examples.Wave5/Tp7DomainApps.cs`
- [X] T112 [US4] Implement `Tp7PuzzleApp` fixed board, adjacent move and invalid-move preservation in `examples/Shared/TuiVision.Examples.Wave5/Tp7DomainApps.cs`
- [X] T113 [US4] Implement `Tp7MouseDialogApp` local settings, capability state/loss, mouse activation cancellation and complete keyboard parity in `examples/Shared/TuiVision.Examples.Wave5/Tp7DomainApps.cs`
- [X] T114 [US4] Prove `HostMutationPerformed` remains false in all mouse paths
- [X] T115 [US4] Add public XML docs and deterministic/historical/proof comments to domain and mouse app logic
- [X] T116 [US4] Increment `Directory.Build.props` manual build counter for the domain/mouse Green invocation
- [X] T117 [US4] Run domain/mouse Release tests Green
- [X] T118 [US4] Create `docs/guides/examples/tp7-ascii-table.md`
- [X] T119 [US4] Create `docs/guides/examples/tp7-calendar.md`
- [X] T120 [US4] Create `docs/guides/examples/tp7-puzzle.md`
- [X] T121 [US4] Create `docs/guides/examples/tp7-mouse-dialog.md`
- [X] T122 [US4] Complete source rows for `ASCIITAB.PAS`, `CALENDAR.PAS`, `PUZZLE.PAS` and `MOUSEDLG.PAS`
- [X] T123 [US4] Complete remaining W5-005 and W5-006 consumer proof
- [X] T124 [US4] Complete ASCII, Calendar, Puzzle and Mouse primary proof and showcase-delta rows

---

## Phase 7: User Story 5 - Complete Traceability and Showcase Delta (Priority: P5)

**Goal**: Make all Stage-1 proof and Stage-2 remaining work exact and
reviewable.

**Independent Test**: One matrix test reconstructs exact 15/6/10/10 sets and
rejects duplicate, missing, unknown or empty rows.

- [X] T125 [US5] Add exact-cardinality source, consumer, example, proof and delta tests in `tests/TuiVision.Examples.SmokeTests/Wave5FunctionalSmokeMatrixTests.cs`
- [X] T126 [US5] Add missing, duplicate, unknown decision and empty-delta negative tests in `tests/TuiVision.Examples.SmokeTests/Wave5FunctionalSmokeMatrixTests.cs`
- [X] T127 [US5] Add launch-project, guide-path and proof-test existence checks in `tests/TuiVision.Examples.SmokeTests/Wave5FunctionalSmokeMatrixTests.cs`
- [X] T128 [US5] Complete all 15 source rows and verify exactly one role each in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T129 [US5] Complete all six consumer rows and verify exactly one decision each in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T130 [US5] Complete all ten primary proof rows in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T131 [US5] Complete all ten non-empty showcase-delta rows in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T132 [US5] Derive `Lastenheft_18_Wave5-TP7-Showcase-Remediation.md` only from the completed delta matrix
- [X] T133 [US5] Confirm no `specs/033-*`, Feature-033 branch or Wave-6 implementation exists
- [X] T134 [US5] Update `examples/README.md` with all ten Wave-5 Stage-1 launch paths and proof boundary
- [X] T135 [US5] Update DocFX navigation for all ten new guides in `docs/toc.yml`
- [X] T136 [US5] Review all ten guides for semantic Markdown, DE-first/EN-second CEFR-B2 and text-first accessibility
- [X] T137 [US5] Complete all seven governance preset rows and every Applicable/N/A trigger in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T138 [US5] Increment `Directory.Build.props` manual build counter for the complete targeted Wave-5 invocation
- [X] T139 [US5] Run all Tp7 and Wave5Functional Release tests and record exact counts in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`

---

## Phase 8: Cross-Cutting Status and Local Validation

**Purpose**: Synchronize repository surfaces and prove the exact local
candidate.

- [X] T140 Synchronize active/completed Feature-032 context and next showcase intake in `AGENTS.md`
- [X] T141 Synchronize the same context in `CLAUDE.md`
- [X] T142 Synchronize the same context in `GEMINI.md`
- [X] T143 Synchronize the same context in `.github/copilot-instructions.md`
- [X] T144 Synchronize the same context in `.github/agents/copilot-instructions.md`
- [X] T145 Run agent homogeneity/parity checks and record result in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [X] T146 Update `Pflichtenheft.md` to mark Wave-5 Stage 1 delivered and Stage 2 next while Wave 6 remains blocked
- [X] T147 Update `Lastenheft_Abarbeitungsreihenfolge.md` to make Lastenheft 18 the next intake without starting it
- [X] T148 Update `docs/project-statistics.md` for the complete Feature-032 candidate
- [X] T149 Archive Lastenheft 17 through `scripts/rename-lastenheft.sh --no-commit` and update all references
- [X] T150 Run `git diff --check`, placeholder scan, protected-root scan, secret scan and dependency/package/project-scope review
- [X] T151 Run `dotnet format TuiVision.sln --verify-no-changes`
- [X] T152 Increment `Directory.Build.props` manual build counter for the full Release invocation
- [X] T153 Run `dotnet test TuiVision.sln --configuration Release` and record exact result
- [X] T154 Validate `coverlet.runsettings` with `xmllint --noout` where available
- [X] T155 Increment `Directory.Build.props` manual build counter for the canonical coverage invocation
- [X] T156 Run canonical Coverlet coverage and record all five assembly percentages
- [X] T157 Run `docfx docfx.json` and record zero-warning/error result
- [X] T158 Run `tests/web-a11y` Playwright/Axe and record result
- [X] T159 Run all ten Release `dotnet run --no-build --project examples/Tp7* -- --smoke` entry-point checks plus UTF-8/text-first and local supply-chain review
- [X] T160 Validate final run state and refresh accepted artifact/task hashes

---

## Phase 9: Exact Candidate, PR, Review, Merge and Sync

**Purpose**: Deliver only the accepted reviewed candidate and finish on clean
synchronized `main`.

- [X] T161 Align `Directory.Build.props` to final `1.32.<patch>.<build>` without extra counter increment
- [X] T162 Stage only intended files and run `git diff --cached --check` plus staged/untracked/unstaged inventory; record in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [ ] T163 Commit the Feature-032 candidate and record commit identity externally and in mutable run evidence where non-self-invalidating
- [ ] T164 Push `032-wave5-tp7-functional-porting` and create a non-empty PR; record URL in `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
- [ ] T165 Monitor PR-context Linux, macOS, Windows, Release, coverage, docs/A11Y, supply-chain and parity checks
- [ ] T166 Map every declared gate to actual workflow, job, runner and executed command in temporary provider evidence
- [ ] T167 Validate temporary exact-head provider evidence with the installed autonomous gate validator
- [ ] T168 Inspect Claude, Copilot and other review results and GraphQL thread state; record unavailable reviews as missing
- [ ] T169 Resolve every actionable review thread with scoped fixes, rerun affected gates and refresh exact-head evidence
- [ ] T170 Use the authorized narrow admin bypass only if Human Approval is the sole open rule and all technical gates plus threads are green
- [ ] T171 Merge the Feature-032 PR with a merge commit and delete the remote feature branch
- [ ] T172 Switch locally to `main`, fetch/prune, fast-forward pull and prove clean `HEAD == origin/main`
- [ ] T173 Decide whether post-merge facts require `specs/032-wave5-tp7-functional-porting/delivery-closeout.md`; do not create an empty closeout
- [ ] T174 If required, create one evidence-only closeout PR that records merge, final Stage-1 status and terminal run facts without recursive self-reference
- [ ] T175 Converge and merge the closeout PR under the same technical-gate and Human-Approval-only policy
- [ ] T176 Return locally to clean synchronized `main` and verify no obsolete 032 branch remains
- [ ] T177 Complete `specs/032-wave5-tp7-functional-porting/retrospective.md` with `FeatureSpecific`, `PresetFollowUp` or `NoPromotion`
- [ ] T178 Promote no preset release unless a reproducible provider-neutral defect is proven; never create an empty retrospective PR
- [ ] T179 Set the final autonomous state to `Retrospective`, `Completed`, all tasks complete and `nextExactAction: N/A`
- [ ] T180 Record final task counts, source/consumer/proof/delta counts, validation, review, PR/merge IDs and main-sync proof in the completion report

## Dependencies and Execution Order

- Phase 1 blocks all later work.
- Phase 2 blocks every user story.
- User Story 1 is the mandatory reference slice and blocks repeated app
  rollout.
- User Stories 2, 3 and 4 are functionally independent after US1, but this run
  executes them sequentially because they share the Wave-5 project, smoke
  project and evidence.
- User Story 5 depends on all four implementation stories.
- Local validation depends on complete status, docs and evidence.
- Remote delivery depends on every local mandatory gate.
- A causal closeout depends on the feature merge and is created only when
  genuinely required.

## Parallel Execution

No task carries `[P]`. Although some app files are distinct, nearly every
slice also updates the shared project, smoke project, evidence, version or
documentation inventory. Serial execution is the safer and more resumable
shape for this feature.

## Implementation Strategy

1. Finish the Calculator Red/Green slice before spreading the pattern.
2. Reuse existing framework contracts; stop and route any broad defect.
3. Keep domain fixtures deterministic and every file write test-owned.
4. Complete exact traceability and delta matrices before final validation.
5. Validate and deliver the exact staged/reviewed head, then merge and sync.
