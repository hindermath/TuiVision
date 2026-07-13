# Tasks: Component and Data Conformance Hardening

**Input**: Design documents from `specs/026-component-data-conformance-hardening/`
**Prerequisites**: `spec.md`, `plan.md`, `research.md`, `data-model.md`,
`contracts/component-data-conformance-acceptance.md`, completed checklists

**Tests**: Test-first Red/Green proof is mandatory for every finding.
**Parallel policy**: No task is marked `[P]`. The slices share evidence,
Controls sources, Serialization registry/files, audit artifacts, agent context,
versioning, or remote state and therefore execute serially.

## Phase 1: Setup and Evidence Foundation

**Purpose**: Prove the starting state and create durable evidence before implementation edits.

- [X] T001 Create `specs/026-component-data-conformance-hardening/pr-evidence.md` from `.specify/templates/autonomous-run-evidence-template.md` with concrete scope, exclusions, `MergeAndSync` authority, gates, and no placeholder row
- [X] T002 Verify branch `026-component-data-conformance-hardening`, clean ownership boundary, `.specify/feature.json`, `specify check`, and `git status` in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T003 Run `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks` and record absolute feature/task paths in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T004 Verify every checklist under `specs/026-component-data-conformance-hardening/checklists/` has zero incomplete items and record counts in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T005 Add the F010–F013 finding table and Dialog, Validator, File Outcome, Resource, Historical, Governance, Validation, and Follow-up matrices to `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T006 Record protected read-only path baselines for `tv203s/`, `TVDEMOS/`, and `TVFM/` using tree hashes or path inventories in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T007 Review `tv203s/contrib/tvision/classes/tdialog.cc`, `tinputli.cc`, `tfiledia.cc` and relevant headers under `tv203s/contrib/tvision/include/tv/` and record intent/deviation boundaries in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T008 Verify pinned Free Vision commit and SHA-256 for `FV006`, `FV007`, `FV010`, and `FV012` in `/tmp/tuivision-freevision-026/` and record untracked provenance in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T009 Review only relevant consumer flows in `TVDEMOS/TVDEMO.PAS`, `TVEDIT.PAS`, `TVRDEMO.PAS`, `GENRDEMO.PAS`, `TVFM/GLOBALS.PAS`, `COLORS.PAS`, and `TVFM.PAS` and record framework/application boundaries in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T010 Inventory current public API, XML comments, tests, harness helpers, focus/ownership assertions, project references, and linked-source identity for all planned Controls/Serialization files in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T011 Record Specify, two Clarify passes, three requirements checklists, Plan, and two plan-review checklist convergence results in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T012 Run `git diff --check` for planning artifacts and record the result in `specs/026-component-data-conformance-hardening/pr-evidence.md`

---

## Phase 2: Foundational Contracts

**Purpose**: Fix cross-slice public shapes and security limits before Red tests.

**Critical**: No production implementation edit starts before this phase and T004 are complete.

- [X] T013 Reconcile all FR-001–FR-028 and SC-001–SC-010 mappings to task IDs in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T014 Freeze the dialog completion set, cancel exception, ordered child walk, derived classifier hook, and first-rejection focus contract in `specs/026-component-data-conformance-hardening/contracts/component-data-conformance-acceptance.md`
- [X] T015 Freeze validator `Edit`, `FocusLoss`, and `Acceptance` defaults plus text/cursor/viewport/insert/selection preservation in `specs/026-component-data-conformance-hardening/contracts/component-data-conformance-acceptance.md`
- [X] T016 Freeze `TFileDialogOutcome`, additive `FileDecisionKind.Rejected`, compatibility projection, TOCTOU, and no-I/O rules in `specs/026-component-data-conformance-hardening/contracts/component-data-conformance-acceptance.md`
- [X] T017 Freeze menu/status/dialog record allowlist, 4,096-entry/item, 4-MiB payload, depth-16, full-consumption, and atomic-publication rules in `specs/026-component-data-conformance-hardening/contracts/component-data-conformance-acceptance.md`
- [X] T018 Record NIST SSDF, CWE Top 25, OWASP input-validation guidance, STRIDE/CIA/CAPEC, and A11Y applicability in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T019 Record ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, DORA, Zero Trust, SAMM, BSI C3A, and BSI C5 as trigger-based `N/A` with owner/reviewer/risk/evidence/follow-up/re-evaluation fields in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T020 Record iSAQB, cross-platform path behavior, agent parity, `.specify/templates/`, and Autonomous Run Governance v0.1.2 applicability in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T021 Verify the six baseline presets plus `autonomous-run-governance` v0.1.2 with `specify preset list`, `info`, and relevant `resolve` commands and record exact results in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T022 Verify the four agent-context script outputs and four maintained `SPECKIT` markers point to Feature 026, and record the separate root Copilot surface in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T023 Capture the unchanged production baseline for F010–F013 test names and current behavior in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T024 Re-run `git diff --check` and confirm no implementation file changed before the representative Red slice in `specs/026-component-data-conformance-hardening/pr-evidence.md`

**Checkpoint**: Evidence, contracts, governance, and test surface are ready.

---

## Phase 3: User Story 1 - Dialog Completion and Child Validation (Priority: P1) MVP

**Goal**: Close `F010` through explicit completion and ordered state-preserving child validation.

**Independent Test**: Real `TDialog.HandleEvent` commands prove OK/Cancel/Yes/No, unrelated commands, derived completion, ordered rejection, focus, state, view tree, and text/cell evidence.

### Red Proof

- [X] T025 [US1] Add F010 Red tests for unrelated/help/application/unknown commands remaining open in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T026 [US1] Add F010 Red tests for OK/Yes/No ordered child validation, first rejection, focus target, preserved state, and Cancel bypass in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T027 [US1] Add F010 Red tests for a derived explicit completion classifier without event-loop override in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T028 [US1] Increment only the manual build counter in `Directory.Build.props` immediately before the F010 Red test invocation
- [X] T029 [US1] Run `dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release --filter "FullyQualifiedName~TDialog"` and record the expected F010 failure boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

### Implementation and Green Proof

- [X] T030 [US1] Add fully XML-documented `TValidationPhase` in `src/TuiVision.Controls/TValidationPhase.cs`
- [X] T031 [US1] Add fully XML-documented immutable `TValidationResult` with accepted/rejected factories, phase, message, and target in `src/TuiVision.Controls/TValidationResult.cs`
- [X] T032 [US1] Add default-accepting view validation to `src/TuiVision.Controls/TView.cs` with selective didactic rationale for the compatibility boundary
- [X] T033 [US1] Add stable child-snapshot, ordered recursive validation, and first-rejection propagation to `src/TuiVision.Controls/TGroup.cs`
- [X] T034 [US1] Add the minimal descendant-focus helper that uses existing `TrySetFocus` veto semantics in `src/TuiVision.Controls/TGroup.cs`
- [X] T035 [US1] Add protected virtual default completion classification for `cmOK`, `cmCancel`, `cmYes`, and `cmNo` in `src/TuiVision.Controls/TDialog.cs`
- [X] T036 [US1] Restrict command consumption and modal completion to classified commands in `src/TuiVision.Controls/TDialog.cs`
- [X] T037 [US1] Route non-Cancel completion through ordered child `Acceptance` validation and focus/text rejection evidence in `src/TuiVision.Controls/TDialog.cs`
- [X] T038 [US1] Review all F010 production changes for DE-first/EN-second XML and selective reason-focused inline comments in `src/TuiVision.Controls/`
- [X] T039 [US1] Complete the F010 command/validation/view-tree/buffer-cell Green assertions in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T040 [US1] Increment only the manual build counter in `Directory.Build.props` immediately before the F010 Green test invocation
- [X] T041 [US1] Run the F010 targeted Release test command from T029 and record pass count/result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T042 [US1] Mark F010 `Implemented` with Red/Green proof, historical intent, Free Vision relation, API/A11Y effect, and residual boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

**Checkpoint**: The representative slice is independently complete.

---

## Phase 4: User Story 2 - Phase-Aware TInputLine Validation (Priority: P1)

**Goal**: Close `F011` through optional validators on edit, focus loss, and acceptance.

**Independent Test**: A real input line and dialog preserve state on rejected candidate edit, focus transition, and dialog acceptance while unvalidated input remains compatible.

### Red Proof

- [X] T043 [US2] Add F011 Red tests for optional validator attachment and compatible no-validator behavior in `tests/TuiVision.Controls.Tests/TInputLineTests.cs`
- [X] T044 [US2] Add F011 Red matrix for permissive intermediate range edit, custom syntax rejection, focus veto, dialog acceptance, and text-first result in `tests/TuiVision.Controls.Tests/TInputLineTests.cs`
- [X] T045 [US2] Add F011 state-preservation tests for text, cursor, viewport, insert mode, explicit non-empty and collapsed selection ranges, paste, cut, delete, backspace, and overwrite in `tests/TuiVision.Controls.Tests/TInputLineTests.cs`
- [X] T046 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the F011 Red test invocation
- [X] T047 [US2] Run `dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release --filter "FullyQualifiedName~TInputLine"` and record the expected F011 failure boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

### Implementation and Green Proof

- [X] T048 [US2] Extend `TValidator` with a virtual phase-aware result method while preserving abstract `IsValid(string)` in `src/TuiVision.Controls/TValidator.cs`
- [X] T049 [US2] Add an optional XML-documented validator and observable last result to `src/TuiVision.Controls/TInputLine.cs`
- [X] T050 [US2] Add bounded read-only selection start/end, an explicit range setter, and snapshot/candidate helpers for data, cursor, viewport, insert mode, and selection in `src/TuiVision.Controls/TInputLine.cs`
- [X] T051 [US2] Validate prospective selected-range replacement and character insert/overwrite before committing mutation in `src/TuiVision.Controls/TInputLine.cs`
- [X] T052 [US2] Validate prospective paste, cut, delete, and backspace edits before committing mutation in `src/TuiVision.Controls/TInputLine.cs`
- [X] T053 [US2] Override focus release with exactly one `FocusLoss` validation and no pre-veto mutation in `src/TuiVision.Controls/TInputLine.cs`
- [X] T054 [US2] Override hierarchical acceptance validation with target/message propagation in `src/TuiVision.Controls/TInputLine.cs`
- [X] T055 [US2] Review `TRangeValidator` and `TFilterValidator` compatibility and add only required phase-specific behavior in `src/TuiVision.Controls/TRangeValidator.cs` and `src/TuiVision.Controls/TFilterValidator.cs`
- [X] T056 [US2] Add real dialog integration and A11Y focus/text proof for F011 in `tests/TuiVision.Controls.Tests/TDialogTests.cs`
- [X] T057 [US2] Review all F011 changes for complete XML, state-preserving security, and selective didactic comments in `src/TuiVision.Controls/`
- [X] T058 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the F011 Green test invocation
- [X] T059 [US2] Run the F011 targeted Release test command from T047 and record pass count/result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T060 [US2] Mark F011 `Implemented` with Red/Green proof, historical intent, Free Vision relation, API/A11Y effect, and residual boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

---

## Phase 5: User Story 3 - Mode-Aware File Dialog Outcomes (Priority: P2)

**Goal**: Close `F012` with typed outcomes and no hidden file operation.

**Independent Test**: Test-owned temporary directories prove navigation, filter, Open, Save, overwrite decision, mismatch, invalid path, and Cancel through the normal dialog path.

### Red Proof

- [X] T061 [US3] Add F012 Red tests for navigation, wildcard/filter, existing/missing Open, new/existing Save, directory selection, invalid manual path, and Cancel in `tests/TuiVision.Controls.Tests/TFileDialogTests.cs`
- [X] T062 [US3] Add F012 Red tests for stale-result prevention, no history commit on rejection, no close on rejection, and no file content mutation in `tests/TuiVision.Controls.Tests/TFileDialogTests.cs`
- [X] T063 [US3] Add platform-neutral exception/rejection tests for invalid path, invalid wildcard, missing parent, and file/directory mismatch in `tests/TuiVision.Controls.Tests/TFileDialogTests.cs`
- [X] T064 [US3] Increment only the manual build counter in `Directory.Build.props` immediately before the F012 Red test invocation
- [X] T065 [US3] Run `dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release --filter "FullyQualifiedName~TFileDialog"` and record the expected F012 failure boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

### Implementation and Green Proof

- [X] T066 [US3] Add fully XML-documented `TFileDialogOutcomeKind` and immutable `TFileDialogOutcome` in `src/TuiVision.Controls/TFileDialogOutcome.cs`
- [X] T067 [US3] Add the compatibility `FileDecisionKind.Rejected` projection without changing the positional record shape in `src/TuiVision.Controls/TFileDecisionResult.cs`
- [X] T068 [US3] Add one mode/path classifier for all accepted and rejected outcomes in `src/TuiVision.Controls/TFileDialog.cs`
- [X] T069 [US3] Convert invalid path, wildcard, metadata, mode mismatch, and missing-parent failures to typed text-first rejection in `src/TuiVision.Controls/TFileDialog.cs`, `TFileList.cs`, and `TFileInputLine.cs`
- [X] T070 [US3] Make navigation and filter outcomes observable without classifying them as file completion in `src/TuiVision.Controls/TFileDialog.cs`
- [X] T071 [US3] Make confirmation commit history and close only after an accepted/overwrite outcome, and project rejection without stale success in `src/TuiVision.Controls/TFileDialog.cs`
- [X] T072 [US3] Complete the F012 temp-fixture, TOCTOU-boundary, view-tree, focus, and text/cell Green matrix in `tests/TuiVision.Controls.Tests/TFileDialogTests.cs`
- [X] T073 [US3] Review all F012 public APIs/XML, cross-platform path handling, privacy boundary, and selective didactic comments in `src/TuiVision.Controls/`
- [X] T074 [US3] Increment only the manual build counter in `Directory.Build.props` immediately before the F012 Green test invocation
- [X] T075 [US3] Run the F012 targeted Release test command from T065 and record pass count/result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T076 [US3] Mark F012 `Implemented` with Red/Green proof, historical intent, Free Vision relation, API/A11Y effect, and residual TOCTOU boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

---

## Phase 6: User Story 4 - Safe Named UI Resource Composition (Priority: P2)

**Goal**: Close `F013` with allowlisted, versioned, bounded, atomic dialog/menu/status resource reconstruction.

**Independent Test**: Controls factories reconstruct valid runtime structures; Serialization roundtrips exact keys and rejects every required malformed case before publication.

### Red Proof

- [X] T077 [US4] Add F013 Red Controls tests for valid menu/status descriptions and runtime reconstruction in `tests/TuiVision.Controls.Tests/MenuDescriptionTests.cs` and `StatusLineDescriptionTests.cs`
- [X] T078 [US4] Add F013 Red Controls graph/range tests for duplicate IDs, unknown parent, cycle, depth, order, invalid labels, context ranges, and commands in `tests/TuiVision.Controls.Tests/MenuDescriptionTests.cs` and `StatusLineDescriptionTests.cs`
- [X] T079 [US4] Add F013 Red Serialization tests for dialog/menu/status built-in registration and exact-key roundtrip in `tests/TuiVision.Serialization.Tests/TUiDescriptionRecordTests.cs`
- [X] T080 [US4] Add F013 Red malformed matrix for version, type, truncation, trailing data, duplicate key, invalid command/reference, entry/item/payload/depth limit, and no-partial-state in `tests/TuiVision.Serialization.Tests/TResourceFileTests.cs` and `TUiDescriptionRecordTests.cs`
- [X] T081 [US4] Increment only the manual build counter in `Directory.Build.props` immediately before the F013 Controls Red test invocation
- [X] T082 [US4] Run `dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release --filter "FullyQualifiedName~Description"` and record the expected Controls failure boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T083 [US4] Increment only the manual build counter in `Directory.Build.props` immediately before the F013 Serialization Red test invocation
- [X] T084 [US4] Run `dotnet test tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj --configuration Release --filter "FullyQualifiedName~Resource|FullyQualifiedName~Description"` and record the expected Serialization failure boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

### Serialization Implementation

- [X] T085 [US4] Add versioned immutable menu primitive records with bounded load/save, full ID/parent/command/cycle/depth validation, and explicit registration in `src/TuiVision.Serialization/TMenuDescriptionRecord.cs`
- [X] T086 [US4] Add versioned immutable status primitive records with bounded load/save, full order/range/command validation, and explicit registration in `src/TuiVision.Serialization/TStatusLineDescriptionRecord.cs`
- [X] T087 [US4] Register dialog, menu, and status records as explicit built-ins in `src/TuiVision.Serialization/TResourceFile.cs`
- [X] T088 [US4] Enforce maximum 4,096 resource entries and 4-MiB payloads on save/load in `src/TuiVision.Serialization/TResourceFile.cs`
- [X] T089 [US4] Preserve full candidate isolation, exact duplicate checks, registered-type rejection, payload consumption, and stream consumption in `src/TuiVision.Serialization/TResourceFile.cs`

### Controls Implementation

- [X] T090 [US4] Add immutable XML-documented menu description entities in `src/TuiVision.Controls/MenuDescription.cs`
- [X] T091 [US4] Add in-memory menu version/ID/parent/order/label/command/cycle/depth/item validation matching persisted-record invariants in `src/TuiVision.Controls/MenuDescriptionValidator.cs`
- [X] T092 [US4] Add deterministic existing-control reconstruction in `src/TuiVision.Controls/MenuDescriptionFactory.cs`
- [X] T093 [US4] Add immutable XML-documented status-line description entities in `src/TuiVision.Controls/StatusLineDescription.cs`
- [X] T094 [US4] Add in-memory status version/order/range/label/command/item validation matching persisted-record invariants in `src/TuiVision.Controls/StatusLineDescriptionValidator.cs`
- [X] T095 [US4] Add deterministic existing-control reconstruction in `src/TuiVision.Controls/StatusLineDescriptionFactory.cs`
- [X] T096 [US4] Add validated menu/status record conversion and preserve existing dialog conversion in `src/TuiVision.Controls/UiDescriptionPersistenceAdapter.cs` and `TDialogDescriptionPersistenceAdapter.cs`
- [X] T097 [US4] Complete positive menu/status runtime identity, order, command, shortcut, context, view-tree, and buffer/cell proof in `tests/TuiVision.Controls.Tests/MenuDescriptionTests.cs` and `StatusLineDescriptionTests.cs`
- [X] T098 [US4] Complete Controls negative graph/range/command, persisted-record adapter parity, and no-runtime-object proof in `tests/TuiVision.Controls.Tests/MenuDescriptionTests.cs` and `StatusLineDescriptionTests.cs`
- [X] T099 [US4] Complete dependency-free Serialization exact-key, built-in allowlist, roundtrip, unknown-type, version, truncation, trailing, duplicate, and limit proof in `tests/TuiVision.Serialization.Tests/TResourceFileTests.cs` and `TUiDescriptionRecordTests.cs`
- [X] T100 [US4] Verify no persisted field contains CLR type names, runtime owners, delegates, pointers, or reflection metadata and record the review in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T101 [US4] Review all F013 public XML, untrusted-input handling, atomicity, and selective didactic comments in `src/TuiVision.Controls/` and `src/TuiVision.Serialization/`
- [X] T102 [US4] Increment only the manual build counter in `Directory.Build.props` immediately before the F013 Controls Green test invocation
- [X] T103 [US4] Run the F013 Controls targeted Release test command from T082 and record pass count/result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T104 [US4] Increment only the manual build counter in `Directory.Build.props` immediately before the F013 Serialization Green test invocation
- [X] T105 [US4] Run the F013 Serialization targeted Release test command from T084 and record pass count/result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T106 [US4] Mark F013 `Implemented` with Red/Green proof, historical intent, Free Vision relation, API/A11Y effect, and residual boundary in `specs/026-component-data-conformance-hardening/pr-evidence.md`

---

## Phase 7: User Story 5 - Finding Closure and Learner Evidence (Priority: P2)

**Goal**: Make all four finding decisions, sources, deviations, matrices, and limits reviewable for Feature 028.

**Independent Test**: Every finding and required matrix row is complete, unique, reproducible, and consistent with actual tests/diff.

- [X] T107 [US5] Reconcile the dialog completion/validation matrix against F010 tests and source in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T108 [US5] Reconcile the validator lifecycle/rejection matrix against F011 tests and source in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T109 [US5] Reconcile the file outcome/temp-fixture matrix against F012 tests and source in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T110 [US5] Reconcile the resource type/version/malformed matrix against F013 tests and source in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T111 [US5] Verify every finding has exactly one `Implemented` or `AlreadySatisfied` decision and no `FollowUpHardening` row closes it in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T112 [US5] Record every discovered out-of-scope runtime, design, parity, format, application, or proof issue as bounded `FollowUpHardening` or blocking `ProductDecision` in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T113 [US5] Create DE-first/EN-second learner guide `docs/guides/component-data-conformance-hardening.md` covering modern C# contracts, historical intent, deliberate deviations, safe examples, and text-first diagrams
- [X] T114 [US5] Add the new guide to `toc.yml` and verify semantic heading/link structure without generated output
- [X] T115 [US5] Review all changed Markdown for CEFR-B2, umlauts/`ß`, fenced-code languages, semantic tables/headings, text-first accessibility, and `Programmierung #include<everyone>` in affected docs
- [X] T116 [US5] Update the Feature-024 finding rows and proof references for F010–F013 in `specs/024-tv203-freevision-conformance-audit/findings.md` only after all four Green proofs
- [X] T117 [US5] Update F010–F013 proof/risk/closure fields consistently in `specs/024-tv203-freevision-conformance-audit/conformance-audit.json` and validate structured JSON parsing
- [X] T118 [US5] Update `specs/024-tv203-freevision-conformance-audit/pre-wave5-gate.md` and `consumer-readiness-review.md` to route final combined verification to Feature 028 without unblocking either wave

---

## Phase 8: Cross-Cutting Validation and Repository Closeout

**Purpose**: Complete documentation, governance, full validation, and exact candidate integrity.

- [X] T119 Run `git diff --check` and record exit status/output summary in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T120 Run `dotnet format --verify-no-changes --no-restore` and record exit status/error-channel review in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T121 Increment only the manual build counter in `Directory.Build.props` immediately before the full Release test invocation
- [X] T122 Run `dotnet test TuiVision.sln --configuration Release --no-restore` and record total pass/skip/failure counts in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T123 Validate `coverlet.runsettings` with `xmllint --noout coverlet.runsettings` where available and record result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T124 Increment only the manual build counter in `Directory.Build.props` immediately before the canonical coverage test invocation
- [X] T125 Run `dotnet test TuiVision.sln --configuration Release --no-restore --collect:"XPlat Code Coverage" --settings coverlet.runsettings` and record raw result paths/counts in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T126 Calculate and verify at least 70 percent line coverage for Core, Controls, Serialization, Compatibility, and Drivers.Console in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T127 Run `docfx docfx.json`, keep `_site/` and generated `api/*.yml` untracked, and record warning/error counts in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T128 Run `npm run test:docfx` from `tests/web-a11y/` and record Playwright/Axe results in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T129 Run the repository text-first/Lynx documentation review path and record UTF-8 content evidence in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T130 Run repository agent-parity validation across AGENTS, Claude, Antigravity/GEMINI, and Copilot surfaces and record exact command/result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T131 Run preset payload/command/skill uniqueness checks for all seven presets and record no duplicate autonomous command display in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T132 Run the repository secret scan with explicit repository root and inspect stdout/stderr for fatal signatures in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T133 Verify `tv203s/`, `TVDEMOS/`, `TVFM/`, external Free Vision, examples, dependencies, workflows, generated output, caches, logs, and credentials remain unchanged in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T134 Re-evaluate every governance `N/A` trigger against the actual diff and finalize owner/reviewer/date/result/risk/follow-up fields in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T135 Update `Pflichtenheft.md` to mark Feature 026 complete, keep Wave 5/6 blocked, and set `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md` as the sole next step
- [X] T136 Update all maintained agent-context `SPECKIT` markers to the Feature-028 intake and change shared guidance only if the completed implementation requires it in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md`
- [X] T137 Review `.github/agents/copilot-instructions.md`, `.specify/templates/`, and agent-generated skills for actual shared-guidance impact; record unchanged or synchronized result in `specs/026-component-data-conformance-hardening/pr-evidence.md`
- [X] T138 Update `docs/project-statistics.md` with final Feature-026 code/test/doc counts and retain the final `## Gesamtstatistik` block
- [X] T139 Archive `Lastenheft_11_Component-Data-Conformance-Hardening.md` through both-script parity review as `Lastenheft_11_Component-Data-Conformance-Hardening.026-component-data-conformance-hardening.md`
- [X] T140 Complete the PR description, validation matrix, decision counts, remaining follow-ups, and retrospective in `specs/026-component-data-conformance-hardening/pr-evidence.md`

---

## Phase 9: Exact Candidate, PR, Merge, and Main Sync

**Purpose**: Deliver the validated candidate under the authorized `MergeAndSync` policy.

Tasks T143–T153 describe terminal delivery work that cannot truthfully mark its
own checkbox on the already reviewed feature head. Their final dispositions are
recorded in `delivery-closeout.md`; unchecked boxes on that historical head do
not mean the terminal task was skipped.

- [X] T141 Align `Version`, `AssemblyVersion`, and `FileVersion` to `1.26.<post-commit-count>.<current-build>` without incrementing the build counter in `Directory.Build.props`
- [X] T142 Verify every task T001–T141 acceptance condition and mark it complete only with evidence in `specs/026-component-data-conformance-hardening/tasks.md`
- [ ] T143 Stage only intended Feature-026 files, validate through the staged set or a temporary index, write the pre-final result to `specs/026-component-data-conformance-hardening/pr-evidence.md`, re-stage it, then rerun non-writing `git diff --cached --check` plus staged/untracked/unstaged reconciliation; retain the final staged tree/hash externally for closeout
- [ ] T144 Commit the exact staged Feature-026 candidate, push `026-component-data-conformance-hardening`, create the feature PR, and retain terminal identifiers outside self-invalidating feature evidence
- [ ] T145 Identify the pull-request-context gate, map every required proof to actual workflow/job/runner/platform/executed command, and record duplicate push/PR runs as non-cancelled noise in the designated external closeout record
- [ ] T146 Monitor all required checks and Claude/Copilot/GraphQL review surfaces; address every actionable finding with new local validation and candidate alignment before push
- [ ] T147 Verify the final reviewed head has all technical gates green and zero actionable threads; record unavailable/quota-limited reviewers as missing rather than passed
- [ ] T148 Use the expressly authorized narrow admin bypass only if the sole remaining rule is Human Approval, then merge with a merge commit and delete the remote feature branch
- [ ] T149 Switch locally to `main`, fetch/prune, fast-forward pull, and prove clean `HEAD == origin/main` after the feature merge
- [ ] T150 Run `speckit-autonomous-retrospective` for Feature 026, classify each learning, and create no TuiVision or Home-Baseline branch when there is no evidence-backed non-empty improvement
- [ ] T151 If a reusable preset learning exists, add and push the Feature-026 `PresetFollowUp` workitem on `~/home-baseline-tmp` branch `codex/autonomous-run-governance-package`; otherwise classify `NoPromotion`
- [ ] T152 Create the required non-empty causal record `specs/026-component-data-conformance-hardening/delivery-closeout.md` on `codex/026-component-data-closeout` with final dispositions for T143–T151, feature PR/head/gate/review/merge facts, retrospective, and preset handoff; validate, PR, review, merge, and keep the closeout PR's own terminal URL/head/merge externally verified
- [ ] T153 Prove `/Users/thorstenhindermann/RiderProjects/TuiVision` is clean synchronized `main`, prove Home-Baseline is clean synchronized or on its documented package branch, and report Feature 028 as the only next autonomous intake

---

## Dependencies and Execution Order

### Phase Dependencies

- Phase 1 has no dependency beyond the completed planning artifacts.
- Phase 2 depends on Phase 1 and blocks every implementation slice.
- US1 is the mandatory representative vertical slice and must complete before US2–US4.
- US2 depends on the shared validation contract from US1.
- US3 depends on US1 dialog rejection/focus behavior but not on US2 validator internals.
- US4 depends only on Foundation, but executes after US3 because shared evidence and version files are single-writer.
- US5 depends on all four Green finding proofs.
- Cross-cutting validation depends on all stories and audit reconciliation.
- Remote delivery depends on exact-candidate validation and every local hard gate.

### User Story Dependency Graph

```text
Setup -> Foundation -> US1/F010 -> US2/F011 -> US3/F012 -> US4/F013
                                                   \             /
                                                    -> US5/closure
                                                          |
                                                Validate -> Deliver
```

### Implementation Strategy

1. Complete F010 as the full MVP slice including Red/Green/evidence.
2. Reuse its validation result and focus boundary for F011 and F012.
3. Keep F013 Records and Controls separated by the existing project direction.
4. Reconcile audit status only after all four slices pass independently.
5. Batch full validation after implementation while preserving one build-counter increment per explicit .NET build/test invocation.
6. Validate and deliver the exact staged candidate, then record terminal facts through one causal closeout only when necessary.
