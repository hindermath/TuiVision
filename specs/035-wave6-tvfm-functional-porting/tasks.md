# Tasks: Wave-6 TVFM Functional Porting

**Input**: Accepted artifacts under
`specs/035-wave6-tvfm-functional-porting/` and binding
`Lastenheft_20_Wave6-TVFM-Functional-Porting.035-wave6-tvfm-functional-porting.md`
**Delivery mode**: `MergeAndSync`
**Scope**: Functional Wave-6 Stage 1 only; no Feature 036 or portfolio audit

## Phase 1: Setup and Evidence Foundation

**Purpose**: Lock identity, authority, scope, matrices and delivery gates
before executable changes.

- [X] T001 Verify branch, `.specify/feature.json`, baseline ancestry and dirty-path ownership in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T002 Record Feature-034 PR #99 and causal closeout PR #100 ancestry in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T003 Run `specify check` and prerequisite checks and record exit/error-channel review in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T004 Verify every Feature-035 checklist is complete in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T005 Record all seven installed preset versions and priorities in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T006 Create and validate `specs/035-wave6-tvfm-functional-porting/autonomous-run-state.json`
- [X] T007 Create and validate `specs/035-wave6-tvfm-functional-porting/autonomous-gate-requirements.json`
- [X] T008 Record `MergeAndSync` authority and its remote/bypass boundaries in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T009 Record hard scope exclusions, read-only roots and stop boundaries in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T010 Record shared single-writer paths and serialization rules in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T011 Record Feature-035 version scheme and one-counter-increment-per-build/test rule in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T012 Record exact 24-source, ten-area, one-entry-point and one-Stage-2 cardinalities in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T013 Record the non-recursive closeout boundary `specs/035-wave6-tvfm-functional-porting/delivery-closeout.md`
- [X] T014 Inventory intended project, test, fixture, guide, evidence and status paths in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T015 Confirm no generated output, external checkout, credential, cache, log or test result is intended for Git
- [X] T016 Hash all 24 `TVFM/` files and initialize exactly one historical matrix row per file in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T017 Initialize exactly ten `W6-001` through `W6-010` framework rows in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T018 Initialize the `Tp7FileManager` primary-proof and Stage-2 rows in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T019 Record governance checkpoint rows for all seven presets in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T020 Refresh run-state accepted-artifact hashes at the Tasks checkpoint

---

## Phase 2: Foundational Project and Compile Surface

**Purpose**: Establish one compiled Wave-6 example assembly, one launch
project, fixtures and complete compile/test ownership before the Red slice.

- [X] T021 Create `examples/Shared/TuiVision.Examples.Wave6/TuiVision.Examples.Wave6.csproj` with existing framework references and no package
- [X] T022 Create controlled workspace and application model declarations in `examples/Shared/TuiVision.Examples.Wave6/Wave6FileModels.cs`
- [X] T023 Add complete German-first/English-second XML docs to public Wave-6 model declarations
- [X] T024 Create the `examples/Tp7FileManager/Tp7FileManager.csproj` executable project
- [X] T025 Create normal and `--smoke` entry-point skeleton in `examples/Tp7FileManager/Program.cs`
- [X] T026 Create source-controlled learning fixtures under `examples/Tp7FileManager/Fixtures/`
- [X] T027 Add the shared assembly and executable to `TuiVision.sln`
- [X] T028 Add the shared Wave-6 assembly reference to `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj`
- [X] T029 Review imports, XML-doc warnings, fixture copy rules, test helpers and shared CLR type identity in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T030 Confirm framework, package and dependency manifests are unchanged except intended project references
- [X] T031 Record the source-controlled-fixture to disposable-workspace ownership transition in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`

**Checkpoint**: The compile surface exists; no functional contract is yet
claimed.

---

## Phase 3: User Story 1 - Controlled Navigation Reference Slice (Priority: P1)

**Goal**: Deliver root binding, stable directory navigation and bounded text
preview through a real TuiVision application loop.

**Independent Test**: A test-owned tree is listed and navigated; traversal and
link escape fail; `app.Run()` proves path, focus, view, status and cells.

- [X] T032 [US1] Add missing/failing root, relative-path and disposed-workspace tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T033 [US1] Add missing/failing traversal, sibling-prefix and absolute-path rejection tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T034 [US1] Add missing/failing link/reparse segment rejection tests with platform-aware setup in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T035 [US1] Add missing/failing stable root-list and subdirectory-navigation tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T036 [US1] Add missing/failing 4-KiB/80-line text preview, invalid UTF-8 and truncation tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T037 [US1] Add missing/failing real app-loop navigation, F1 Description and cell proof in `tests/TuiVision.Examples.SmokeTests/Wave6FunctionalSmokeMatrixTests.cs`
- [X] T038 [US1] Increment `Directory.Build.props` manual build counter for the reference-slice Red invocation
- [X] T039 [US1] Run the Wave-6 reference-slice Release tests Red and accept only missing implementation failures
- [X] T040 [US1] Record Red command, version, exit/error review and each expected failure in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T041 [US1] Implement canonical root binding and relative path normalization in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T042 [US1] Implement segment-wise link/reparse rejection and disposal boundary in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T043 [US1] Implement stable directory snapshots and navigation in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T044 [US1] Implement bounded UTF-8 text preview and honest invalid/truncated state in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T045 [US1] Add moderate DE-first/EN-second why-comments to root, link and preview boundaries
- [X] T046 [US1] Implement the common real status line, event queue and visible proof tracking in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T047 [US1] Implement first-frame tree/list summary, navigation command, text preview, F1 Description and controlled quit in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T048 [US1] Materialize fixture content into a disposable root in `examples/Tp7FileManager/Program.cs`
- [X] T049 [US1] Implement deterministic `--smoke` events in `examples/Tp7FileManager/Program.cs`
- [X] T050 [US1] Increment `Directory.Build.props` manual build counter for the reference-slice Green invocation
- [X] T051 [US1] Run the Wave-6 reference-slice Release tests Green
- [X] T052 [US1] Record W6-001, W6-002 and first primary-proof results in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T053 [US1] Complete historical rows for `TVFM.PAS`, `GLOBALS.PAS`, `EQU.PAS`, `TOOLS.PAS`, `DIRVIEW.PAS` and `TREEWIN.PAS`

**Checkpoint**: A complete safe read-only vertical slice is proven before
broader reads or any mutation.

---

## Phase 4: User Story 2 - List, Tag, Text and Hex Preview (Priority: P2)

**Goal**: Deliver deterministic filtering, sorting, tagging, metadata and two
bounded internal viewers.

**Independent Test**: Controlled fixtures change list order/filter/tag state
and render bounded text/hex output through the real app loop.

- [X] T054 [US2] Add failing/missing filter, sort, empty-result and stable-selection tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T055 [US2] Add failing/missing tag and metadata snapshot tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T056 [US2] Add failing/missing 4-KiB hex offset, row and printable-character tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T057 [US2] Add failing/missing app-loop filter, tag, text/hex command and cell tests in `tests/TuiVision.Examples.SmokeTests/Wave6FunctionalSmokeMatrixTests.cs`
- [X] T058 [US2] Increment `Directory.Build.props` manual build counter for list/view Red invocation
- [X] T059 [US2] Run list/view Release tests Red and record expected missing behavior
- [X] T060 [US2] Implement simple wildcard filtering and deterministic sort modes in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T061 [US2] Implement application-local tag state and metadata snapshots in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T062 [US2] Implement bounded hex preview in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T063 [US2] Implement list filter, sort, tag, text and hex commands in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T064 [US2] Add XML docs and didactic comments for deterministic ordering and preview proof limits
- [X] T065 [US2] Increment `Directory.Build.props` manual build counter for list/view Green invocation
- [X] T066 [US2] Run list/view Release tests Green
- [X] T067 [US2] Record W6-003 and W6-004 framework and primary proof in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T068 [US2] Complete historical rows for `FILEVIEW.PAS`, `INFOVIEW.PAS`, `VIEWTEXT.PAS` and `VIEWHEX.PAS`

---

## Phase 5: User Story 3 - Bounded Search and Internal Associations (Priority: P3)

**Goal**: Deliver limited recursive search, cancellation and closed viewer
selection without external execution.

**Independent Test**: Search returns stable relative results, stops on token
or limits, and associations select only text, hex or fallback.

- [X] T069 [US3] Add failing/missing search match, stable-order, depth, file and result-limit tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T070 [US3] Add failing/missing cancellation and consistent-partial-result tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T071 [US3] Add failing/missing text, binary and unknown association tests in `tests/TuiVision.Examples.SmokeTests/Wave6ControlledWorkspaceTests.cs`
- [X] T072 [US3] Add failing/missing app-loop search, cancel, association and fallback cell tests in `tests/TuiVision.Examples.SmokeTests/Wave6FunctionalSmokeMatrixTests.cs`
- [X] T073 [US3] Increment `Directory.Build.props` manual build counter for search/association Red invocation
- [X] T074 [US3] Run search/association Release tests Red and record expected failures
- [X] T075 [US3] Implement depth-8, 256-file, 100-result cancellable search in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T076 [US3] Implement closed internal association decisions in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T077 [US3] Implement search, cancel and associated-preview commands in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T078 [US3] Add why-comments for resource ceilings, partial cancellation and no-external-viewer boundary
- [X] T079 [US3] Increment `Directory.Build.props` manual build counter for search/association Green invocation
- [X] T080 [US3] Run search/association Release tests Green
- [X] T081 [US3] Record W6-005 and W6-008 framework and proof rows in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T082 [US3] Complete historical rows for `FILEFIND.PAS` and `ASSOC.PAS`

---

## Phase 6: User Story 4 - Explicit Mutations, Progress and Recovery (Priority: P4)

**Goal**: Deliver safe file-level copy, rename, delete, read-only and
drag/drop-intent paths with explicit confirmation and recovery.

**Independent Test**: Preview/cancel stays byte-identical; confirm mutates only
inside the root; conflict, stale source, traversal, link and failure reject.

- [X] T083 [US4] Add failing/missing operation-intent validation and cancel-no-write tests in `tests/TuiVision.Examples.SmokeTests/Wave6FileOperationTests.cs`
- [X] T084 [US4] Add failing/missing copy and rename success plus source-equals-target/conflict tests in `tests/TuiVision.Examples.SmokeTests/Wave6FileOperationTests.cs`
- [X] T085 [US4] Add failing/missing delete and read-only toggle tests with platform capability assertions in `tests/TuiVision.Examples.SmokeTests/Wave6FileOperationTests.cs`
- [X] T086 [US4] Add failing/missing stale source, removed source, traversal and link revalidation tests in `tests/TuiVision.Examples.SmokeTests/Wave6FileOperationTests.cs`
- [X] T087 [US4] Add failing/missing execution-failure and recovery-boundary tests in `tests/TuiVision.Examples.SmokeTests/Wave6FileOperationTests.cs`
- [X] T088 [US4] Add failing/missing app-loop prepare, cancel, confirm, progress and keyboard-drop tests in `tests/TuiVision.Examples.SmokeTests/Wave6FunctionalSmokeMatrixTests.cs`
- [X] T089 [US4] Increment `Directory.Build.props` manual build counter for mutation Red invocation
- [X] T090 [US4] Run mutation Release tests Red and record every expected failure boundary
- [X] T091 [US4] Implement immutable one-operation intent creation in `examples/Shared/TuiVision.Examples.Wave6/ControlledFileWorkspace.cs`
- [X] T092 [US4] Implement confirmation, stale-state/root/link revalidation and one-shot authorization
- [X] T093 [US4] Implement no-overwrite file copy and rename with terminal result state
- [X] T094 [US4] Implement file delete and portable read-only toggle with honest unsupported/failure state
- [X] T095 [US4] Implement cancellation, progress and explicit recovery boundaries for all operations
- [X] T096 [US4] Implement app commands for prepare, cancel, confirm and keyboard drop-intent in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T097 [US4] Ensure mouse drag can only prepare the same intent and cannot bypass confirmation
- [X] T098 [US4] Add moderate DE-first/EN-second why-comments to TOCTOU, overwrite, link and recovery logic
- [X] T099 [US4] Increment `Directory.Build.props` manual build counter for mutation Green invocation
- [X] T100 [US4] Run mutation Release tests Green
- [X] T101 [US4] Record W6-006, W6-007 and W6-009 framework and proof rows in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
- [X] T102 [US4] Complete historical rows for `FILECOPY.PAS`, `DRAGDROP.PAS`, `TRASH.PAS` and `GAUGES.PAS`

---

## Phase 7: User Story 5 - Resources, Traceability and Stage-2 Decision (Priority: P5)

**Goal**: Complete closed palette/config/resources, historical traceability,
framework ownership and the actual later-showcase decision.

**Independent Test**: Exact 24/10/1/1 matrices reject missing, duplicate,
unknown or empty decisions, and no Feature 036 exists.

- [X] T103 [US5] Add failing/missing palette/config/resource fallback app-loop tests in `tests/TuiVision.Examples.SmokeTests/Wave6FunctionalSmokeMatrixTests.cs`
- [X] T104 [US5] Add exact 24-source, ten-area, one-entry and one-Stage-2 cardinality tests in `tests/TuiVision.Examples.SmokeTests/Wave6FunctionalSmokeMatrixTests.cs`
- [X] T105 [US5] Add missing, duplicate, unknown decision, unsafe deviation and empty-evidence negative matrix tests
- [X] T106 [US5] Implement closed palette/config/resource choices in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T107 [US5] Complete historical rows for `COLORS.PAS`, `EDITPAL.PAS`, `MAKERES.PAS`, `TVFM.TVR`, three `.PAL` files and `MAKETVFM.BAT`
- [X] T108 [US5] Verify all 24 source rows have one role, modern target, retained intent, deviation and proof
- [X] T109 [US5] Complete W6-010 and verify all ten framework rows have one decision and no hidden reusable defect
- [X] T110 [US5] Complete all read, search, mutation, recovery and app-loop primary proof rows
- [X] T111 [US5] Set exactly one evidence-backed Stage-2 disposition for `Tp7FileManager`
- [X] T112 [US5] Confirm no `specs/036-*`, Feature-036 branch or post-Wave-6 audit start exists
- [X] T113 [US5] Increment `Directory.Build.props` manual build counter for the complete targeted Wave-6 invocation
- [X] T114 [US5] Run all Wave6 Release tests and record exact counts in `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`

---

## Phase 8: Documentation, Governance and Repository Status

**Purpose**: Make the functional stage usable, accessible and traceable across
all maintained project surfaces.

- [X] T115 Create `docs/guides/examples/tp7-file-manager.md` with purpose, sources, launch, keyboard flow, safety, modernization, platform and proof boundaries
- [X] T116 Update `examples/README.md` with normal and `--smoke` Wave-6 launch paths
- [X] T117 Update `docs/toc.yml` with the Wave-6 guide
- [X] T118 Review changed Markdown for semantic headings, fenced-language tags, UTF-8, DE-first/EN-second CEFR-B2 and text-first access
- [X] T119 Review keyboard, focus, status, F1 Description, high-contrast and constrained-terminal acceptance
- [X] T120 Review all new non-trivial code for selective didactic comments and all public APIs for XML docs
- [X] T121 Complete NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC and secure-filesystem governance rows
- [X] T122 Complete ASVS, supply-chain, AI-SBOM, S-ADR/arc42, Zero Trust/SAMM, BSI C3A/C5 and regulatory N/A rows with triggers
- [X] T123 Complete A11Y, cross-platform and agent-parity governance rows
- [X] T124 Synchronize active Feature-035 context across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` and `.github/agents/copilot-instructions.md`
- [X] T125 Run agent homogeneity/parity checks and record the result
- [X] T126 Update `Pflichtenheft.md` with Wave-6 Stage-1 status and independent closure requirement
- [X] T127 Update `Lastenheft_Abarbeitungsreihenfolge.md` without creating or starting Feature 036
- [X] T128 Update `docs/project-statistics.md` for the complete Feature-035 candidate
- [X] T129 Archive Lastenheft 20 through `scripts/rename-lastenheft.sh --no-commit` and update references

---

## Phase 9: Local Validation and Exact Candidate

**Purpose**: Prove the exact local candidate under repository-wide quality
gates.

- [X] T130 Run `git diff --check`, placeholder, generated-path, historical-root, dependency/package/project-scope and secret scans
- [X] T131 Run `dotnet format TuiVision.sln --verify-no-changes`
- [X] T132 Run the controlled normal PTY start and all `--smoke` paths without an additional build
- [X] T133 Increment `Directory.Build.props` manual build counter for the full Release invocation
- [X] T134 Run `dotnet test TuiVision.sln --configuration Release` and record exact result
- [X] T135 Validate `coverlet.runsettings` with `xmllint --noout` where available
- [X] T136 Increment `Directory.Build.props` manual build counter for the canonical coverage invocation
- [X] T137 Run canonical Coverlet coverage and record all five assembly percentages
- [X] T138 Run `docfx docfx.json` and record zero-warning/error result
- [X] T139 Run `tests/web-a11y` Playwright/Axe and record result
- [X] T140 Run local supply-chain, agent parity, UTF-8/text-first and state validators with explicit repository root
- [X] T141 Rehash all 24 `TVFM/` files and prove `TVDEMOS/` and `tv203s/` have no diff
- [X] T142 Verify exact 24/10/1/1 evidence cardinalities, one-decision rules and SC outcomes
- [X] T143 Verify the final diff contains no uncontrolled filesystem, external execution, API, dependency, historical or Feature-036 change
- [X] T144 Refresh accepted artifact/task hashes and validate both autonomous state validators
- [X] T145 Align `Directory.Build.props` to final `1.35.<patch>.<build>` without extra counter increment
- [X] T146 Stage only intended files and run `git diff --cached --check` plus staged/untracked/unstaged inventory

---

## Phase 10: PR, Review, Merge, Sync and Retrospective

**Purpose**: Deliver only the reviewed candidate and finish on clean,
synchronized `main`.

- [ ] T147 Commit the exact Feature-035 candidate and record commit identity without self-invalidating evidence
- [ ] T148 Push `035-wave6-tvfm-functional-porting` and create a non-empty feature PR
- [ ] T149 Identify PR-context required checks and record duplicate push runs without unsafe cancellation
- [ ] T150 Monitor Ubuntu, macOS, Windows, docs/A11Y, supply-chain, parity and full-test gates to terminal state
- [ ] T151 Map every Applicable gate to actual workflow, job, platform and executed command
- [ ] T152 Validate temporary exact-head provider evidence against `autonomous-gate-requirements.json`
- [ ] T153 Inspect Copilot, Claude and GraphQL review threads; resolve every actionable finding
- [ ] T154 Re-run affected validation and refresh exact-head evidence after any review correction
- [ ] T155 Use narrow admin bypass only if all technical gates are green and Human Approval is the sole open rule
- [ ] T156 Merge the feature PR with a merge commit and delete the remote feature branch
- [ ] T157 Create one causal evidence-only closeout PR only if truthful post-merge facts require it
- [ ] T158 Switch locally to `main`, fetch/prune and fast-forward pull
- [ ] T159 Prove clean working tree and `HEAD == origin/main`
- [ ] T160 Complete `specs/035-wave6-tvfm-functional-porting/retrospective.md` with promotion classification
- [ ] T161 If a provider-neutral preset defect exists, hand off one bounded `PresetFollowUp`; otherwise record `NoPromotion` without an empty branch or PR
- [ ] T162 Set run state to `Retrospective`, `Completed`, `163/163`, `nextExactAction: N/A`
- [ ] T163 Report final matrices, validation, PR/merge, Stage-2 decision, follow-ups and main-sync proof without starting Feature 036

## Dependencies and Execution Order

1. Phase 1 precedes every implementation edit.
2. Phase 2 establishes project and compile ownership.
3. US1 is the mandatory vertical slice.
4. US2 and US3 build only on the proven read boundary.
5. US4 begins only after all read/search boundaries pass.
6. US5 closes matrices only after all behavior is proven.
7. Shared docs, evidence, agent, version and delivery files remain serialized.
8. No task creates Feature 036 or starts the portfolio audit.

## Parallel Opportunities

No `[P]` markers are used. Nearly every slice touches the shared workspace,
application, smoke-test project or single evidence ledger. Serial execution is
the safer and more reviewable plan.

## Implementation Strategy

1. Prove one safe read-only navigation and preview slice.
2. Add broader read/search behavior.
3. Add mutations only behind the accepted root and explicit decision model.
4. Close exact matrices and learner documentation.
5. Run repository-wide validation once the candidate stabilizes.
6. Deliver, review, merge and return to synchronized `main`.
