# Tasks: Pre-Wave-5 and Wave-6 Conformance Closure

**Input**: Design documents from
`specs/028-pre-wave5-wave6-conformance-closure/`
**Prerequisites**: `spec.md`, `plan.md`, `research.md`, `data-model.md`,
`quickstart.md`, `contracts/conformance-closure-acceptance.md`, and all feature
checklists

**Tests**: Test-first proof is required. The missing closure dataset is observed
before it is created, and every explicit `dotnet test` command has a separate
immediately preceding manual build-counter task.

**Organization**: Tasks follow the four accepted user stories. No task has a
`[P]` marker because the slices share closure evidence, run evidence, version,
workflow, status, or agent files and must remain serialized.

## Format: `[ID] [Story] Description`

- **[Story]**: `US1` finding closure, `US2` integrated paths, `US3` consumer
  readiness, or `US4` final gate and next intake
- Every task names the file that owns its durable result.
- Product assemblies, examples, `tv203s/`, `TVDEMOS/`, `TVFM/`, pinned Free
  Vision, and Terminal.GUI sources remain unchanged.

## Phase 1: Setup and Evidence Foundation

**Purpose**: Prove the starting state and create durable evidence before test or
status changes.

- [ ] T001 Verify branch `028-pre-wave5-wave6-conformance-closure`, `.specify/feature.json`, `specify check`, prerequisite output, and zero incomplete feature checklist items against `specs/028-pre-wave5-wave6-conformance-closure/tasks.md`
- [ ] T002 Create `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md` from `.specify/presets/autonomous-run-governance/templates/autonomous-run-evidence-template.md` before any test, workflow, gate, or status edit
- [ ] T003 Record `MergeAndSync` authority, scope firewall, convergence gates, stop conditions, narrow Human Approval bypass, and Feature-029 boundary in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T004 Verify all seven installed preset names, versions, priorities, command uniqueness, and completed checklists and record the result in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T005 Hash the committed `specs/028-pre-wave5-wave6-conformance-closure/autonomous-gate-requirements.json`, verify nine unique gates with eight `Applicable` and one WSL `N/A`, and freeze it against implementation-time provider output in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T006 Freeze the canonical counts and reciprocal relation baseline of 48 contracts, 13 findings, and 13 final resolutions from `specs/024-tv203-freevision-conformance-audit/conformance-audit.json` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T007 Record current protected-tree hashes/status for `tv203s/`, `TVDEMOS/`, `TVFM/`, and the pinned external Free Vision evidence boundary in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T008 Assign stable baseline IDs `W5-001` through `W5-006` and `W6-001` through `W6-007` to the unchanged row order in `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md` and record the mapping in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T009 Record the serialized shared-writer list and `1.28.<patch>.<build>` command boundaries in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T010 Review the planned test compile surface, `System.Text.Json` helpers, project references, proof-path lookup, XML-doc non-trigger, and selective bilingual didactic-comment need for `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Checkpoint**: The immutable input, gate requirements, authority, and evidence
writer exist before test-first implementation starts.

---

## Phase 2: Foundational Test-First Closure Validator

**Purpose**: Establish one complete F001/R-028-001/W5-001 reference slice and
the fail-closed validator pattern before bulk rows are added.

- [ ] T011 Add only `Test_ClosureDatasetExists` and repository-root path resolution to `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs` while `closure-evidence.json` is still absent
- [ ] T012 Increment only the manual build counter in `Directory.Build.props` immediately before the missing-dataset Red test command
- [ ] T013 Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --filter "FullyQualifiedName~ConformanceClosureEvidenceTests.Test_ClosureDatasetExists"` and record the expected missing-file failure in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T014 Create the closed-schema representative run, F001, R-028-001, and W5-001 rows with complete review metadata in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T015 Add `Test_RepresentativeSliceIsComplete` plus required string/array/path/method helpers to `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T016 Increment only the manual build counter in `Directory.Build.props` immediately before the representative-slice Green test command
- [ ] T017 Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --no-restore --filter "FullyQualifiedName~ConformanceClosureEvidenceTests.Test_ClosureDatasetExists|FullyQualifiedName~ConformanceClosureEvidenceTests.Test_RepresentativeSliceIsComplete"` and record exact pass counts in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T018 Add validator tests for exact F001-F013 and R-028-001-R-028-007 sets, the complete 13-row consumer baseline, unique additions, closed vocabularies, and all required metadata in `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T019 Add mutation tests for malformed JSON, duplicates, unknown IDs/decisions, missing reciprocal links, missing source/proof paths or methods, and missing metadata in `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T020 Add fail-closed tests for `ReadyForTerminalGuiAudit` paired with a reopened finding, blocking consumer decision, failed slice, incomplete governance, or failed validation in `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T021 Increment only the manual build counter in `Directory.Build.props` immediately before the incomplete-cardinality Red test command
- [ ] T022 Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --no-restore --filter "FullyQualifiedName~ConformanceClosureEvidenceTests.Test_CompleteDatasetRelationshipsAndGateRules"` and record the expected partial-dataset failure in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T023 Create the German-first/English-second reference tables and proof-boundary structure in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.md`
- [ ] T024 Record Red/Green sequence, build counters, helper roles, proof limits, and didactic-comment decision in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T025 Verify `git diff --name-only` contains no product, API, dependency, example, historical, consumer, Free Vision, or Terminal.GUI source path and record the foundation result in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Checkpoint**: One reference slice passes, while the complete validator remains
red for the intentionally incomplete row sets.

---

## Phase 3: User Story 1 - Revalidate All Accepted Findings (Priority: P1)

**Goal**: Reconcile every F001-F013 row with immutable audit data, final 025/026
resolution, real-path proof, complete review metadata, and one closure decision.

**Independent Test**: Finding-specific closure tests and the existing canonical
resolution test pass with exactly thirteen non-documentation-only rows.

- [ ] T026 [US1] Reconcile F001 observation, C004 relation, Core025 ownership, merged change, Red/Green proof, historical/Free Vision intent, consumer scope, and residual boundary in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T027 [US1] Add complete F002-F004 focus/state/lifecycle closure rows from canonical and merged evidence to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T028 [US1] Add complete F005-F006 desktop/close/modal closure rows from canonical and merged evidence to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T029 [US1] Add complete F007-F009 command/keyboard/drag closure rows from canonical and merged evidence to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T030 [US1] Add complete F010-F011 dialog/validator closure rows from canonical and merged evidence to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T031 [US1] Add complete F012-F013 file/resource closure rows from canonical and merged evidence to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T032 [US1] Verify all thirteen original observations, contract IDs, owner features, historical intent, Free Vision relations, and consumer scopes are preserved exactly against `specs/024-tv203-freevision-conformance-audit/conformance-audit.json`
- [ ] T033 [US1] Verify every merged change path and canonical evidence path exists and remains assigned to the matching resolution in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T034 [US1] Replace every path-only proof with one or more existing `path::method` references that reach the accepted production boundary in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T035 [US1] Complete API, A11Y, platform, owner, reviewer, date, evidence, result, residual-risk, follow-up, and re-evaluation fields for all findings in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T036 [US1] Assign exactly one allowed closure decision per finding and stop with `Blocked` rather than modifying product code if any row is `Reopened025`, `Reopened026`, or `ProductDecision` in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T037 [US1] Add all thirteen readable closure rows and deliberate proof limits to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.md`
- [ ] T038 [US1] Extend finding-specific reciprocal, cardinality, path/method, and metadata assertions in `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T039 [US1] Record finding decisions, canonical reconciliation, decision counts, and any blocker in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T040 [US1] Increment only the manual build counter in `Directory.Build.props` immediately before the finding-closure test command
- [ ] T041 [US1] Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --no-restore --filter "FullyQualifiedName~ConformanceClosureEvidenceTests.Test_Finding|FullyQualifiedName~ConformanceAuditEvidenceTests.Test_FindingResolutions"` and record exact pass counts in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T042 [US1] Recheck the product/protected-source scope firewall and mark US1 complete only when all thirteen rows are evidence-backed in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Checkpoint**: All thirteen accepted findings close independently, or the run
stops honestly without a Feature-028 product fix.

---

## Phase 4: User Story 2 - Prove Integrated Framework Paths (Priority: P2)

**Goal**: Execute all seven consumer-shaped slices through real production
entries with explicit negative/fallback, helper-role, and proof-limit evidence.

**Independent Test**: Every named proof method executes under its targeted
Release filter, and slice-integrity tests pass for exactly R-028-001-R-028-007.

- [ ] T043 [US2] Finalize R-028-001 proof references for raw translation, function/unknown keys, concrete event kinds, modifier separation, Ctrl+W, Alt shortcut, dispatch, consumption, and visible target state in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T044 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-001 targeted test command
- [ ] T045 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~FromConsoleKey_FunctionKeys_ProduceExpectedScanCodes|FullyQualifiedName~FromConsoleKey_UnknownKey_ProducesScanCodeZero|FullyQualifiedName~TEvent_CreateMouse_AcceptsConcreteKindsAndRejectsMasksOrComposites|FullyQualifiedName~TProgram_GetEvent_UsesCanonicalTranslationForRawConsoleKeys|FullyQualifiedName~TWindow_CanonicalCtrlBit_IsDistinctFromAlt|FullyQualifiedName~TWindow_F006_CommandCtrlWAndEscapeCompleteTheSameLifecycle|FullyQualifiedName~TMenuBar_Activation_WorksWithAlt|FullyQualifiedName~TProgram_CommandRouting_ExecutesExactlyOnce|FullyQualifiedName~TGroup_HandleEvent_PostProcess_Skipped_WhenEventConsumedInFocusedPhase"` and record exact R-028-001 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T046 [US2] Add R-028-002 focus/state/validation references including atomic veto, state-specific propagation, preserved invalid input, and accessible focus announcement to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T047 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-002 targeted test command
- [ ] T048 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~TGroup_TrySetFocus_VetoAndEligibilityAreAtomic|FullyQualifiedName~TGroup_SetState_|FullyQualifiedName~TGroup_Insert_AppliesStateSpecificInheritance|FullyQualifiedName~TInputLine_F011_FocusAndDialogAcceptancePreserveInvalidInput|FullyQualifiedName~FocusTransition_DesktopDescendant_ReachesProgramBroadcast"` and record exact R-028-002 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T049 [US2] Add R-028-003 pending-event, idle, command-refresh, CPU-release, and shutdown references to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T050 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-003 targeted test command
- [ ] T051 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~TProgram_PendingEvent_IsBoundedAndDrainedBeforeInput|FullyQualifiedName~TProgram_Run_IdleAndPendingOrdering_IsDeterministic|FullyQualifiedName~TProgram_Run_InputPrecedesIdleAndIdleShutdownStopsWork|FullyQualifiedName~TProgram_CommandContext_RefreshesAllTriggersAndRejectsStaleDispatch|FullyQualifiedName~TProgram_Run_ShutsDownCleanly"` and record exact R-028-003 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T052 [US2] Add R-028-004 desktop stack/geometry, close/veto, modal isolation/result/cleanup/focus, application lifecycle, view-tree, and cell references to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T053 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-004 targeted test command
- [ ] T054 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~TDesktop_F005|FullyQualifiedName~TDesktop_FocusFallback|FullyQualifiedName~TWindow_F006|FullyQualifiedName~TWindow_CloseAffordance_VisibleWhenClosable|FullyQualifiedName~TDialog_F006|FullyQualifiedName~TApplication_F006_RunCompletesWindowCloseBeforeShutdown"` and record exact R-028-004 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T055 [US2] Add R-028-005 capture/threshold/bounds/target/drop/cancel/lifecycle and keyboard-parity references to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T056 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-005 targeted test command
- [ ] T057 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~F009|FullyQualifiedName~AppLoop_TitleDrag_ChangesWindowAndRenderedRegion|FullyQualifiedName~AppLoop_DisabledOrUnsupportedMouse_PreservesKeyboardFallback"` and record exact R-028-005 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T058 [US2] Add R-028-006 completion classification, ordered validation, first-rejection focus, preserved state, accessible rejection, and cancel-boundary references to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T059 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-006 targeted test command
- [ ] T060 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~TDialog_F010|FullyQualifiedName~TDialog_F011|FullyQualifiedName~TInputLine_F011"` and record exact R-028-006 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T061 [US2] Add R-028-007 typed file modes, test-owned paths, exact resource keys, versions, bounds, graph/reference rejection, and atomic no-publication references to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T062 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the R-028-007 targeted test command
- [ ] T063 [US2] Run `dotnet test TuiVision.sln --configuration Release --no-restore --filter "FullyQualifiedName~TFileDialog_F012|FullyQualifiedName~MenuDescription_F013|FullyQualifiedName~StatusLineDescription_F013|FullyQualifiedName~TUiDescriptionRecord_F013|FullyQualifiedName~TResourceFile_F013"` and record exact R-028-007 results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T064 [US2] Complete assertions, negative/fallback boundary, `PrimaryProof`/`SupplementalProof` rationale, proof limit, A11Y/platform scope, owner/reviewer/date/evidence/result/risk/follow-up/trigger for all seven rows in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T065 [US2] Add the seven German-first/English-second slice rows and proof-boundary explanations to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.md`
- [ ] T066 [US2] Complete slice-set, method-existence, negative-boundary, helper-role, and metadata validation in `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T067 [US2] Increment only the manual build counter in `Directory.Build.props` immediately before the slice-integrity validator test command
- [ ] T068 [US2] Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --no-restore --filter "FullyQualifiedName~ConformanceClosureEvidenceTests.Test_IntegrationSlices"` and record exact pass counts in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T069 [US2] Reconcile every executed method, zero-test boundary, helper role, and scope diff and mark US2 complete in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Checkpoint**: All seven integrated paths are proven through named production
behavior rather than aggregate suite success or helper-only claims.

---

## Phase 5: User Story 3 - Reassess Both Consumer Families (Priority: P3)

**Goal**: Re-read every immutable Wave-5 and Wave-6 consumer group after
remediation and assign one traceable readiness decision without porting it.

**Independent Test**: The consumer validator finds all 13 baseline IDs, accepts
only unique justified additions, and rejects missing/replaced rows or blocking
decisions under a ready gate.

- [ ] T070 [US3] Reassess W5-001 `TVDEMO.PAS` and W5-002 `TVEDIT.PAS` against current shared contracts and real proofs in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T071 [US3] Reassess W5-003/W5-004 resource consumers and W5-005/W5-006 idle/command/mouse consumers in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T072 [US3] Reassess W6-001 `TVFM.PAS` and W6-002 `FILEVIEW.PAS` against lifecycle, command, resource, and drag contracts in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T073 [US3] Reassess W6-003 `DRAGDROP.PAS` and W6-004 `TREEWIN.PAS` against generic drag, focus, desktop, and close contracts in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T074 [US3] Reassess W6-005/W6-006 dialog consumers and W6-007 destructive-operation product-policy boundary in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T075 [US3] Recompute protected consumer hashes/status and prove no `TVDEMOS/` or `TVFM/` source changed in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T076 [US3] Search the read-only consumers for any additional relevant shared-framework responsibility; add one stable continuation ID per real addition or record zero additions with rationale in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T077 [US3] Assign exactly one `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, `FollowUpHardening`, or `ProductDecision` per consumer row and stop closure for any blocking shared decision in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T078 [US3] Complete contract/finding/proof, rationale, wave, owner/reviewer/date/evidence, residual risk, follow-up, and re-evaluation fields for every consumer row in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T079 [US3] Add all baseline and additional consumer decisions with explicit future-Wave proof limits to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.md`
- [ ] T080 [US3] Complete consumer baseline/addition/decision/metadata and protected-source assertions in `tests/TuiVision.Drivers.Tests/ConformanceClosureEvidenceTests.cs`
- [ ] T081 [US3] Increment only the manual build counter in `Directory.Build.props` immediately before the consumer-readiness validator test command
- [ ] T082 [US3] Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --no-restore --filter "FullyQualifiedName~ConformanceClosureEvidenceTests.Test_ConsumerReadiness"` and record exact pass counts in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T083 [US3] Reconcile consumer decisions, additions, hashes, residual Wave limits, and the no-porting diff and mark US3 complete in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Checkpoint**: Every baseline and newly found shared consumer flow has one
honest decision; neither Wave has started or been released.

---

## Phase 6: User Story 4 - Record the Gate and Next Intake (Priority: P4)

**Goal**: Make local and remote acceptance requirements explicit, retain both
Wave blocks, and name only Feature 029 as next.

**Independent Test**: Workflow semantics and both gate validators agree with
the frozen nine-gate requirements, while closure remains blocked until all
local and exact-head evidence is complete.

- [ ] T084 [US4] Add `windows-latest` to the existing runtime matrix without changing its command body in `.github/workflows/ci.yml`
- [ ] T085 [US4] Parse `.github/workflows/ci.yml`, verify immutable action pins and Bash-on-Windows compatibility, and compare the unchanged command body with prior successful Windows proof `e55b075` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T086 [US4] Map Linux, macOS, and Windows runtime requirement tokens to the actual `CI` workflow/job/runner/commands in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T087 [US4] Map DocFX/A11Y requirement tokens to the actual `DocFX Pages` workflow/job/runner/commands in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T088 [US4] Map the three-platform Lastenheft/agent homogeneity requirement to `.github/workflows/homogeneity-check.yml` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T089 [US4] Map dependency and temporary CycloneDX requirements to `.github/workflows/security-supply-chain.yml` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T090 [US4] Map agent-surface secret requirements independently to `.github/workflows/agent-secret-scan.yml` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T091 [US4] Map Gitleaks requirements independently to `.github/workflows/gitleaks.yml` in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T092 [US4] Record WSL as `N/A` without relabeling Windows proof and retain its exact re-evaluation trigger in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T093 [US4] Complete Security Governance v0.6.0 applicability, evidence, owner/reviewer/date/result/risk/follow-up/trigger rows in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T094 [US4] Complete Architecture Governance v0.5.0 STRIDE/CIA/CAPEC and S-ADR/arc42/Zero Trust/SAMM/BSI C3A/C5 decisions in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T095 [US4] Complete iSAQB Architecture Governance v0.2.0 quality/view/ADR/risk/debt decisions in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T096 [US4] Complete A11Y Governance v0.4.0 keyboard/focus/rejection/text-first/bilingual/didactic-comment decisions in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T097 [US4] Complete Cross-Platform Governance v0.2.0 OS/path/terminal evidence and script-parity `N/A` decision in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T098 [US4] Complete Agent Parity Governance v0.3.0 five-surface synchronization and `.specify/templates/` `N/A` decisions in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T099 [US4] Complete Autonomous Run Governance v0.1.4 permission, staged-candidate, exact-head, resume, closeout, and retrospective decisions in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T100 [US4] Generate temporary synthetic positive and stale-head evidence for all nine frozen gates, run both v0.1.4 validators, delete the temporary files, and record results in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T101 [US4] Keep the local run state blocked pending complete validation and record Feature 029 as the sole next intake in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json`
- [ ] T102 [US4] Add the complete governance and pre-remote gate tables to `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.md`
- [ ] T103 [US4] Record all nine gate mappings, separate Primary boundaries, governance counts, and current block reason in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T104 [US4] Recheck workflow-only scope, action pins, gate-requirement immutability, and no Terminal.GUI work and mark the pre-validation US4 slice complete in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Checkpoint**: Gate requirements are real-command mappings, not job-name
inferences, and the feature remains blocked until final validation and remote
exact-head convergence.

---

## Phase 7: Cross-Cutting Validation and Repository Closeout

**Purpose**: Validate the complete candidate, set the local gate result, update
all status surfaces, and prepare one exact staged delivery candidate.

- [ ] T105 Run `git diff --check` and record exit status and output boundary in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T106 Run `dotnet format --verify-no-changes --no-restore`, inspect stdout/stderr for fatal signatures, and record the result in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T107 Increment only the manual build counter in `Directory.Build.props` immediately before the full Release test command
- [ ] T108 Run `dotnet test TuiVision.sln --configuration Release --no-restore` and record per-project and total pass/skip/failure counts in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T109 Run `xmllint --noout coverlet.runsettings` where available and record the canonical collector configuration result in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T110 Increment only the manual build counter in `Directory.Build.props` immediately before the canonical coverage test command
- [ ] T111 Run `dotnet test TuiVision.sln --configuration Release --no-restore --collect:"XPlat Code Coverage" --settings coverlet.runsettings` and record raw result paths and totals in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T112 Calculate and verify at least 70 percent line coverage for Core, Controls, Serialization, Compatibility, and Drivers.Console in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T113 Run `docfx docfx.json`, retain `_site/` and generated `api/*.yml` outside Git, and record warning/error counts in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T114 Run `npm run test:docfx` from `tests/web-a11y/` and record Playwright/Axe totals in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T115 Run the UTF-8 Lynx/text-first review against changed learner/status pages and record semantic readability in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T116 Run vulnerable/deprecated package review and temporary CycloneDX 1.7 generation outside Git, inspect results, delete output, and record the supply-chain boundary in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T117 Run `scripts/scan-agent-secrets.sh --fail-on-high` with the explicit repository root, inspect both channels, and record the result in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T118 Run local Gitleaks against the candidate/history boundary and record zero unresolved findings in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T119 Reparse changed workflows, recheck immutable action pins, and verify every frozen command token remains executable in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T120 Verify protected roots, product/API/dependency/example scope, generated output, caches, logs, credentials, and temporary provider evidence remain absent from the tracked diff in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T121 Run structured JSON checks and reconcile complete validator/full-suite results for all findings, slices, consumers, governance, and validation records in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T122 Set the existing gate to `ReadyForTerminalGuiAudit` only if every local criterion passed; otherwise retain `Blocked` with owner/reproduction in `specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json` and `closure-evidence.md`
- [ ] T123 Update `specs/024-tv203-freevision-conformance-audit/pre-wave5-gate.md` and `consumer-readiness-review.md` to the Feature-028 decision while keeping Wave 5 and Wave 6 `BlockedPendingTerminalGuiAudit`
- [ ] T124 Update `Pflichtenheft.md` and `Lastenheft_Abarbeitungsreihenfolge.md` to mark Feature 028 complete and Feature 029 as the sole next intake without creating Feature 029
- [ ] T125 Synchronize Feature-028 final state and the Feature-029 next-intake marker across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [ ] T126 Verify `.specify/templates/`, generated skills/commands, and unrelated agent guidance remain unchanged or document the exact shared reason for any synchronized change in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`
- [ ] T127 Run `bash scripts/rename-lastenheft.sh --no-commit Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md 028-pre-wave5-wave6-conformance-closure`, verify Lastenhefte 10/11 are already correctly archived, then run `bash tests/scripts/rename-lastenheft-tests.sh` to prove Bash/PowerShell contract parity
- [ ] T128 Update `docs/project-statistics.md` with final Feature-028 evidence/test/workflow/documentation counts while retaining the final `## Gesamtstatistik` block and text-first diagrams
- [ ] T129 Finalize PR description, validation/governance/decision counts, conditional `N/A` results, follow-ups, exact final diff, and `1.28.<post-commit-count>.<current-build>` alignment in `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md` and `Directory.Build.props`
- [ ] T130 Verify T001-T129 acceptance, mark only genuinely complete tasks in `specs/028-pre-wave5-wave6-conformance-closure/tasks.md`, stage only intended files, run `git diff --cached --check`, staged secret/status reconciliation, and retain the exact staged tree/hash externally for delivery closeout

**Checkpoint**: The exact local candidate is complete, version-aligned, scoped,
and staged without an unstaged or untracked remainder.

---

## Phase 8: Exact Candidate, PR, Merge, Retrospective, and Main Sync

**Purpose**: Deliver under authorized `MergeAndSync`, validate every exact-head
gate, and record terminal facts without invalidating the reviewed head.

Tasks T131-T146 cannot truthfully mark their own checkboxes on the already
reviewed feature head. Their final dispositions belong in
`specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`; unchecked
boxes on the historical feature commit do not mean those terminal tasks were
skipped.

- [ ] T131 Commit the exact staged Feature-028 candidate, push `028-pre-wave5-wave6-conformance-closure`, create the feature PR, and retain PR/head identifiers outside self-invalidating `pr-evidence.md`
- [ ] T132 Identify pull-request-context gates, retain duplicate push/PR runs as uncancelled noise unless safely deduplicated, and record the authoritative run set in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T133 Verify exact-head Linux, macOS, and Windows runtime runs execute restore, Release build, full tests, and DocFX and record immutable run/job URLs in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T134 Verify the exact-head DocFX Pages Playwright/Axe gate and record immutable run/job evidence in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T135 Verify exact-head Ubuntu/macOS/Windows homogeneity jobs execute agent-secret and Lastenheft rename checks and record immutable evidence in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T136 Verify exact-head vulnerable/deprecated package and CycloneDX supply-chain workflow and record immutable evidence in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T137 Verify exact-head Agent Secret Scan and Gitleaks as independent Primary gates and record both immutable runs in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T138 Generate untracked provider-neutral evidence with eight applicable Primary rows plus the WSL `N/A` Primary row, exact requirements hash, and exact reviewed head at `/tmp/028-autonomous-gate-evidence.json`
- [ ] T139 Run both installed v0.1.4 evidence validators against `/tmp/028-autonomous-gate-evidence.json`, prove a tampered head fails, and record results without committing the temporary file in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T140 Monitor all required checks plus Claude, Copilot, and GraphQL review surfaces; record missing/quota-limited review as missing rather than passed in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T141 Address every actionable review or CI finding through local edits, one build-counter increment before each new build/test command, complete revalidation, version realignment, commit, push, and a new exact-head evidence cycle recorded in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T142 Verify the final reviewed head has every technical gate green, both evidence validators passing, zero actionable threads, no scope violation, and no unavailable reviewer mislabeled as approval in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T143 Use the expressly authorized narrow admin bypass only if Human Approval is the sole remaining rule, merge with a merge commit, and delete the remote feature branch with the external terminal result destined for `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T144 Switch locally to `main`, fetch/prune, fast-forward pull, and prove a clean `HEAD == origin/main` for `/Users/thorstenhindermann/RiderProjects/TuiVision`
- [ ] T145 Run `speckit-autonomous-retrospective`; for a deterministic provider-neutral defect publish the non-empty Home-Baseline patch release and install its exact tag ZIP in TuiVision before Feature 029, otherwise record `NoPromotion` and create no empty branch/PR/release in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`
- [ ] T146 Create the single non-recursive causal closeout containing T131-T145 dispositions on a non-empty closeout branch, validate/review/merge it, return TuiVision and any touched Home-Baseline repository to clean synchronized `main`, and report Feature 029 as the sole next autonomous intake in `specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`

---

## Dependencies and Execution Order

### Phase Dependencies

- Phase 1 has no dependency beyond the committed planning artifacts.
- Phase 2 depends on Phase 1 and blocks every user story.
- US1 depends on the representative slice and completes all finding rows.
- US2 depends on US1 relations and completes all seven real-path slices.
- US3 depends on US1 and US2 because consumer decisions cite their final proof.
- US4 depends on the frozen gate requirements and consumer decision boundary.
- Cross-cutting validation depends on all four stories; final ready state is set
  only after every local gate passes.
- Remote delivery depends on the exact staged candidate and complete local
  evidence. Retrospective and preset promotion happen only after merge/main
  synchronization.

### User Story Dependency Graph

```text
Setup -> Foundation -> US1 Findings -> US2 Integrated Paths
                                      -> US3 Consumers -> US4 Gate
                                                           |
                                                Validate -> Deliver
```

### Build-Counter Boundaries

The explicit test commands are T013, T017, T022, T041, T045, T048, T051,
T054, T057, T060, T063, T068, T082, T108, and T111. Their immediate prior
tasks T012, T016, T021, T040, T044, T047, T050, T053, T056, T059, T062,
T067, T081, T107, and T110 each increment only the manual build counter once.
No other task may reuse one of those increments for another build/test command.

### Implementation Strategy

1. Prove the missing dataset, then complete one F001/R-028-001/W5-001 slice.
2. Reconcile all findings before using them as integrated-slice evidence.
3. Execute every slice through existing real paths; a missing boundary reopens
   its owner and stops Feature 028 rather than causing a product fix here.
4. Reassess consumers only after finding and slice proof is stable.
5. Keep the gate blocked until full local validation, then keep both Waves
   blocked through Feature 029 regardless of Feature-028 success.
6. Deliver one exact reviewed head and record terminal facts through one causal
   closeout so evidence never invalidates itself.
