# Tasks: Didactic Inline Code Comment Hardening

**Input**: Design documents from `specs/015-didactic-comment-hardening/`
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/didactic-comment-hardening-acceptance.md`, `checklists/requirements.md`, `checklists/plan-quality.md`, `checklists/plan-review.md`

**Tests**: This is a comment-hardening feature with no planned runtime behavior change. New behavior tests are not expected. Each story starts with review and validation guards, then uses existing targeted tests scaled to the touched source or test-helper files.

**Organization**: Tasks are grouped by independently reviewable user-story slices. Shared files such as `specs/015-didactic-comment-hardening/pr-evidence.md`, `docs/project-statistics.md`, `Directory.Build.props`, and maintained agent guidance surfaces are edited serially.

**Parallel policy**: No parallel task marker is used. The feature relies on a single evidence ledger and cross-file flow decisions, so parallel edits would add avoidable merge and review risk.

## Format: `[ID] [Story] Description`

- **[Story]**: `US1`, `US2`, `US3`, or `US4`; setup, foundation, governance, validation, and PR tasks are shared.
- Every reviewed file or named flow area must receive exactly one primary decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
- Didactic code-near comments explain why, trade-off, constraint, historical deviation, or proof boundary. They must not restate obvious code.

## Phase 1: Setup and Scope Guard

**Purpose**: Confirm the active feature, preserve the accepted scope, and prepare the shared evidence surface before reviewing code.

- [X] T001 Verify the branch and feature paths with `git status --short --branch -uall` and `.specify/scripts/bash/check-prerequisites.sh --json --paths-only`; confirm the active spec is `specs/015-didactic-comment-hardening/spec.md`.
- [X] T002 Read `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md`, `specs/015-didactic-comment-hardening/spec.md`, `specs/015-didactic-comment-hardening/plan.md`, and `specs/015-didactic-comment-hardening/contracts/didactic-comment-hardening-acceptance.md`; record in `specs/015-didactic-comment-hardening/pr-evidence.md` that the accepted scope is comment, evidence, and guidance hardening only.
- [X] T003 Confirm no new feature directory was created and no branch change occurred; record the check in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- [X] T004 Inspect `Directory.Build.props` and `git rev-list --count origin/main..HEAD`; before later build, test, commit, or push actions, align version fields to `1.15.<patch>.<build>` and increment the manual build counter only before build or test commands.
- [X] T005 Create `specs/015-didactic-comment-hardening/pr-evidence.md` with sections for scope guard, hotspot inventory, review decisions, smoke proof boundaries, a separate audit-ready governance matrix, agent guidance review, validation evidence, statistics decision, and final acceptance summary.
- [X] T006 Add two separate evidence tables to `specs/015-didactic-comment-hardening/pr-evidence.md`: the review-area table uses `AreaId`, `PathOrFlow`, `HotspotCategory`, `Decision`, `Rationale`, `CommentNeed`, `CommentState`, `ChangeSummary`, `ValidationOrProofBoundary`, `FollowUpBoundary`, and `GovernanceTrigger`; the governance table uses `RunId`, `PresetName`, `PresetVersion`, `Checkpoint`, `Applicability`, `Rationale`, `EvidencePath`, `Owner`, `Reviewer`, `ReviewDate`, `Result`, `ResidualRisk`, `FollowUp`, and `ReevaluationTrigger`.
- [X] T007 Add the allowed decision legend to `specs/015-didactic-comment-hardening/pr-evidence.md` and state that no other primary decision values are valid.
- [X] T008 Add the out-of-scope boundary to `specs/015-didactic-comment-hardening/pr-evidence.md`: no runtime behavior changes, no API changes, no new dependencies, no new example porting, no broad framework revision, and no Wave-1 visual remediation.
- [X] T009 Add the DocFX and A11Y trigger matrix to `specs/015-didactic-comment-hardening/pr-evidence.md`, distinguishing pure `//` or `/* */` hardening from XML/API/docs/navigation/guide changes.

**Checkpoint**: The feature evidence ledger exists, has complete columns, and protects the accepted scope before code review begins.

## Phase 2: Foundation and Hotspot Inventory

**Purpose**: Build the review map that all user stories use, including source areas, smoke helpers, historical context, validation scaling, and governance triggers.

- [X] T010 Build the initial hotspot inventory in `specs/015-didactic-comment-hardening/pr-evidence.md` for `EventCommandDispatch`, `FocusTransition`, `ViewHierarchy`, `StatusLine`, `HelpDescription`, `DialogState`, `ValidationRejection`, `BufferCellProof`, `RenderingSnapshot`, `TerminalFallback`, `HistoricalTurboVisionDeviation`, and `SmokeTestHelper`.
- [X] T011 Map central source candidates in `specs/015-didactic-comment-hardening/pr-evidence.md` from `src/TuiVision.Core/`, `src/TuiVision.Controls/`, `src/TuiVision.Drivers.Console/`, `src/TuiVision.Serialization/`, and `src/TuiVision.Compatibility/`.
- [X] T012 Map smoke-helper and proof candidates in `specs/015-didactic-comment-hardening/pr-evidence.md` from `tests/TuiVision.Examples.SmokeTests/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Core.Tests/`, `tests/TuiVision.Drivers.Tests/`, `tests/TuiVision.Serialization.Tests/`, and `tests/TuiVision.Compatibility.Tests/`.
- [X] T013 For each hotspot category, define the intended validation scale in `specs/015-didactic-comment-hardening/pr-evidence.md`: evidence-only review, targeted test project, full Release test, coverage gate, conditional DocFX, or conditional web-a11y.
- [X] T014 Create the historical-reference plan in `specs/015-didactic-comment-hardening/pr-evidence.md`; use `tv203s/` only as read-only context when it clarifies modern code or proof boundaries.
- [X] T015 Record the initial source-control cleanliness boundary in `specs/015-didactic-comment-hardening/pr-evidence.md`, including that generated `_site/`, generated `api/*.yml`, local caches, logs, credentials, and validation output are not planned for tracking.
- [X] T016 Record the dependency boundary in `specs/015-didactic-comment-hardening/pr-evidence.md`: no new runtime NuGet dependency is planned, and package updates remain out of scope unless a separate trigger is documented.
- [X] T017 Record the branch-versioning boundary in `specs/015-didactic-comment-hardening/pr-evidence.md`: `Directory.Build.props` must be aligned before build, test, commit, or push on branch `015-didactic-comment-hardening`.

**Checkpoint**: Every required hotspot category is mapped to candidate review areas or has an explicit no-current-area rationale.

## Phase 3: User Story 1 - Understand Central Framework Decisions (Priority: P1)

**Goal**: Apprentices and maintainers can understand central framework decisions around dispatch, focus, hierarchy, status, help, dialog, validation, rendering, serialization, compatibility, and terminal fallback without runtime changes.

**Independent Test**: A reviewer can pick any required framework hotspot in `specs/015-didactic-comment-hardening/pr-evidence.md`, find exactly one approved decision, and inspect matching code-near comments only where they add learning value.

### Review and Validation Guard for US1

- [X] T018 [US1] Record in `specs/015-didactic-comment-hardening/pr-evidence.md` that US1 introduces no new behavior tests unless review discovers a source or test-helper edit beyond comments; route any such discovery to `FollowUpHardening` or a separate approved change.
- [X] T019 [US1] Select the targeted validation projects for US1 based on the files that may receive comment-only edits: `tests/TuiVision.Core.Tests/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Drivers.Tests/`, `tests/TuiVision.Serialization.Tests/`, or `tests/TuiVision.Compatibility.Tests/`.

### Implementation for US1

- [X] T020 [US1] Review event, command, and dispatch flows in `src/TuiVision.Controls/TApplication.cs`, `src/TuiVision.Controls/TProgram.cs`, `src/TuiVision.Controls/TGroup.cs`, `src/TuiVision.Controls/TMenuBar.cs`, `src/TuiVision.Controls/TMenuItem.cs`, and `src/TuiVision.Controls/ShellCommandIds.cs`; record one primary decision per reviewed file or named flow in `pr-evidence.md`.
- [X] T021 [US1] Apply only `CommentNeeded` or `UpdateExistingComment` changes from T020, keeping comments concise, German-first/English-second where didactic, and focused on dispatch rationale or command-routing constraints.
- [X] T022 [US1] Review focus transitions in `src/TuiVision.Controls/TView.cs`, `src/TuiVision.Controls/TGroup.cs`, `src/TuiVision.Controls/TProgram.cs`, `src/TuiVision.Controls/TDialog.cs`, and `src/TuiVision.Controls/TWindow.cs`; record decisions and focus-boundary rationale in `pr-evidence.md`.
- [X] T023 [US1] Apply only needed focus-transition comment changes from T022, explaining selection transfer, activation, disabled-state, or keyboard-path consequences where the code is not self-explaining.
- [X] T024 [US1] Review view hierarchy and ownership flows in `src/TuiVision.Controls/TGroup.cs`, `src/TuiVision.Controls/TDesktop.cs`, `src/TuiVision.Controls/TWindow.cs`, `src/TuiVision.Controls/Internal/FramedHostView.cs`, `src/TuiVision.Controls/TScrollGroup.cs`, and `src/TuiVision.Controls/TScroller.cs`; record decisions in `pr-evidence.md`.
- [X] T025 [US1] Apply only needed view-hierarchy comments from T024, explaining parent/child traversal, insertion/removal, Z-order, visible composition, or buffered drawing boundaries where useful.
- [X] T026 [US1] Review status feedback flows in `src/TuiVision.Controls/TStatusLine.cs`, `src/TuiVision.Controls/TStatusDef.cs`, `src/TuiVision.Controls/TStatusItem.cs`, and `src/TuiVision.Controls/TProgram.cs`; record decisions in `pr-evidence.md`.
- [X] T027 [US1] Apply only needed StatusLine comments from T026, explaining command/status linkage, dynamic feedback, or user-visible feedback boundaries where non-obvious.
- [X] T028 [US1] Review help and description flows in `src/TuiVision.Controls/THelpViewer.cs`, `src/TuiVision.Controls/THelpWindow.cs`, `src/TuiVision.Controls/TDialogDescription.cs`, `src/TuiVision.Controls/TDialogDescriptionFactory.cs`, `src/TuiVision.Controls/TDialogDescriptionValidator.cs`, `src/TuiVision.Serialization/THelpFile.cs`, `src/TuiVision.Serialization/THelpIndex.cs`, and `src/TuiVision.Serialization/THelpTopic.cs`; record decisions in `pr-evidence.md`.
- [X] T029 [US1] Apply only needed Help/Description comments from T028, explaining fallback content, context lookup, cross-reference limits, or description reachability where names and XML docs are insufficient.
- [X] T030 [US1] Review dialog state flows in `src/TuiVision.Controls/TDialog.cs`, `src/TuiVision.Controls/TStandardDialogFlowState.cs`, `src/TuiVision.Controls/TColorDialog.cs`, `src/TuiVision.Controls/TFileDialog.cs`, `src/TuiVision.Controls/TEditWindow.cs`, and `src/TuiVision.Controls/TFileEditor.cs`; record decisions in `pr-evidence.md`.
- [X] T031 [US1] Apply only needed dialog-state comments from T030, explaining modal lifecycle, command result, restoration, safe-close, overwrite, or deferred interaction boundaries.
- [X] T032 [US1] Review validation and rejection flows in `src/TuiVision.Controls/TValidator.cs`, `src/TuiVision.Controls/TRangeValidator.cs`, `src/TuiVision.Controls/TFilterValidator.cs`, `src/TuiVision.Controls/TInputLine.cs`, `src/TuiVision.Controls/TDialogDescriptionValidator.cs`, and `src/TuiVision.Controls/TFileInputLine.cs`; record decisions in `pr-evidence.md`.
- [X] T033 [US1] Apply only needed validation/rejection comments from T032, explaining guard rationale, invalid-input handling, rejected commands, or safe failure paths.
- [X] T034 [US1] Review buffer, cell, and rendering primitives in `src/TuiVision.Core/TConsoleBuffer.cs`, `src/TuiVision.Core/TConsoleCell.cs`, `src/TuiVision.Core/TRect.cs`, `src/TuiVision.Controls/TGroup.cs`, `src/TuiVision.Controls/TProgram.cs`, and `src/TuiVision.Controls/TIndicator.cs`; record decisions in `pr-evidence.md`.
- [X] T035 [US1] Apply only needed buffer/cell or rendering comments from T034, explaining clipping, snapshot, buffer ownership, draw ordering, or proof limits without changing rendering behavior.
- [X] T036 [US1] Review serialization and resource behavior in `src/TuiVision.Serialization/TRecordSerializer.cs`, `src/TuiVision.Serialization/TRecordRegistry.cs`, `src/TuiVision.Serialization/TResourceFile.cs`, `src/TuiVision.Serialization/TResourceCollection.cs`, `src/TuiVision.Serialization/pstream.cs`, `src/TuiVision.Serialization/ipstream.cs`, and `src/TuiVision.Serialization/opstream.cs`; record decisions in `pr-evidence.md`.
- [X] T037 [US1] Apply only needed serialization/resource comments from T036, explaining malformed-input rejection, named lookup, shared reference, cycle, trailing-data, or proof-boundary rationale where non-trivial.
- [X] T038 [US1] Review terminal fallback and compatibility flows in `src/TuiVision.Drivers.Console/TConsoleDriver.cs`, `src/TuiVision.Drivers.Console/SystemConsolePresenter.cs`, `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs`, and `src/TuiVision.Compatibility/TConsoleInputAdapter.cs`; record decisions in `pr-evidence.md`.
- [X] T039 [US1] Apply only needed terminal-fallback comments from T038, explaining capability boundaries, unsupported terminal behavior, platform constraints, or managed-driver deviations.
- [X] T040 [US1] Review historical Turbo Vision references under `tv203s/` only where they clarify a modern US1 code path or proof boundary; record the read-only reference, modern area, deviation kind, and explanation need in `pr-evidence.md`.
- [X] T041 [US1] Apply only needed historical-deviation comments from T040; if a parity or design issue requires runtime work, record `FollowUpHardening` instead of changing behavior.

**Checkpoint**: US1 source review decisions are complete, needed comments are applied, unchanged adequate areas are evidenced, and no runtime behavior change was accepted.

## Phase 4: User Story 2 - Understand Smoke-Test Proof Paths (Priority: P1)

**Goal**: Maintainers, reviewers, and apprentices can see why smoke-test helpers are stable and where their proof boundaries stop.

**Independent Test**: A reviewer can inspect each reviewed smoke-helper area and determine proof purpose, stability reason, helper role, and proof limit without reverse-engineering helper internals.

### Review and Validation Guard for US2

- [X] T042 [US2] Record in `specs/015-didactic-comment-hardening/pr-evidence.md` that US2 edits are limited to comments and evidence unless a discovered test-design issue is routed to `FollowUpHardening`.
- [X] T043 [US2] Select targeted validation commands for any touched smoke helper files, including `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` and affected module test projects.

### Implementation for US2

- [X] T044 [US2] Review shared example smoke helpers in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs` for `DirectHelperUsage`, app-loop proof, rendered text extraction, view-tree proof, buffer/cell proof, region proof, and assertion helper boundaries; record decisions in `pr-evidence.md`.
- [X] T045 [US2] Apply only needed comments in `tests/TuiVision.Examples.SmokeTests/ExampleTestBase.cs`, explaining proof purpose, stability reason, helper role, or proof limit where method names and assertions are not enough.
- [X] T046 [US2] Review event-script proof in `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`; record decisions for command, key, focus, and dialog-state driving in `pr-evidence.md`.
- [X] T047 [US2] Apply only needed comments in `tests/TuiVision.Examples.SmokeTests/InteractiveSmokeEventScript.cs`, explaining event-loop proof boundaries without changing event order or assertions.
- [X] T048 [US2] Review Wave-1 and Wave-2 smoke proof files in `tests/TuiVision.Examples.SmokeTests/*SmokeTests.cs` and `tests/TuiVision.Examples.SmokeTests/Wave2InteractiveSmokeMatrixTests.cs`; record decisions for app-loop, command, status, Help/Description, dialog, rendered visibility, and fallback proof areas in `pr-evidence.md`.
- [X] T049 [US2] Apply only needed comments in reviewed example smoke files from T048, keeping setup-only and supplemental proof clearly separate from primary behavior proof.
- [X] T050 [US2] Review control smoke and proof helpers in `tests/TuiVision.Controls.Tests/ControlBufferAssert.cs`, `tests/TuiVision.Controls.Tests/ControlEventFactory.cs`, `tests/TuiVision.Controls.Tests/ControlTestContext.cs`, `tests/TuiVision.Controls.Tests/ControlsProofTests.cs`, `tests/TuiVision.Controls.Tests/ShellPresenterSpy.cs`, `tests/TuiVision.Controls.Tests/ShellTestSupport.cs`, and `tests/TuiVision.Controls.Tests/ControlsWidgetTestContext.cs`; record decisions in `pr-evidence.md`.
- [X] T051 [US2] Apply only needed comments in reviewed control test helpers from T050, explaining buffer/cell proof, presenter snapshots, shell-state limits, or deterministic setup where non-obvious.
- [X] T052 [US2] Review dialog and designer proof helpers in `tests/TuiVision.Controls.Tests/StandardDialogTestSupport.cs`, `tests/TuiVision.Controls.Tests/DialogDesignerFlowTests.cs`, `tests/TuiVision.Controls.Tests/TStandardDialogFlowTests.cs`, and `tests/TuiVision.Serialization.Tests/DialogDescriptionTestSupport.cs`; record decisions in `pr-evidence.md`.
- [X] T053 [US2] Apply only needed comments from T052, explaining dialog validation, rejection, persisted description, or state-proof boundaries.
- [X] T054 [US2] Review serialization and driver proof helpers in `tests/TuiVision.Serialization.Tests/SerializationTestSupport.cs`, `tests/TuiVision.Serialization.Tests/PStreamTests.cs`, `tests/TuiVision.Serialization.Tests/SerializationCoverageSweepTests.cs`, `tests/TuiVision.Drivers.Tests/Phase7DriverTestContext.cs`, `tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs`, and `tests/TuiVision.Drivers.Tests/TConsoleDriverConsolidationTests.cs`; record decisions in `pr-evidence.md`.
- [X] T055 [US2] Apply only needed comments from T054, explaining malformed payload proof, snapshot proof, terminal fallback, compatibility boundary, or helper stability limits.
- [X] T056 [US2] Verify that every reviewed smoke-helper entry in `pr-evidence.md` records proof purpose, stability reason, boundary, helper role, comment decision, and any validation command chosen for that touched file.

**Checkpoint**: US2 smoke-helper review decisions and proof boundaries are clear, stable, and not overstated.

## Phase 5: User Story 3 - Keep Comment Noise Under Control (Priority: P2)

**Goal**: The feature improves learning value without adding line-by-line prose duplicates or conflating unchanged comment states.

**Independent Test**: A reviewer can open `specs/015-didactic-comment-hardening/pr-evidence.md` and see one approved decision for every reviewed file or named flow area.

### Review and Validation Guard for US3

- [X] T057 [US3] Run a decision-value review over the review-area table in `specs/015-didactic-comment-hardening/pr-evidence.md` and changed comments; confirm only `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, and `FollowUpHardening` appear as primary comment decisions and that governance applicability values remain in the separate governance table.
- [X] T058 [US3] Record in `pr-evidence.md` that US3 is a review-quality pass and creates no new runtime tests unless comment review exposes a real code or proof issue outside scope.

### Implementation for US3

- [X] T059 [US3] Add separate `CommentAdequate` evidence entries for reviewed areas where existing comments already explain the needed why, trade-off, constraint, historical deviation, or proof boundary.
- [X] T060 [US3] Add separate `NoCommentNeeded` evidence entries for reviewed self-explaining areas where a comment would only repeat identifiers, operators, assignments, assertions, or simple control flow.
- [X] T061 [US3] Apply `UpdateExistingComment` changes for reviewed comments that are stale, misleading, too broad, or trivial; correct, replace, or remove the comment without changing executable code.
- [X] T062 [US3] Verify every `CommentNeeded` evidence row has a matching code-near comment change or a same-feature evidence correction that changes the row to another approved decision.
- [X] T063 [US3] Verify every `FollowUpHardening` evidence row names the real issue, explains why it is outside 015, and identifies the later work item or evidence boundary that should carry it.
- [X] T064 [US3] Review added or updated comments for the normal 1-to-3-line intensity target; record total new/updated didactic comment count, longer-comment count, the resulting percentage, and rationale for any longer comment in `pr-evidence.md`.
- [X] T065 [US3] Review didactic explanation blocks for German-first/English-second CEFR-B2 wording; keep technical license, generated-file, tool-owned, and marker lines unchanged.
- [X] T066 [US3] Review `git diff --word-diff` or an equivalent focused diff to confirm executable statements, public APIs, dependencies, project files, and example scope did not change as part of US3.

**Checkpoint**: Comment value, omitted comments, updated comments, and follow-up boundaries are separated cleanly.

## Phase 6: User Story 4 - Carry Comment Rules Into Future Work (Priority: P3)

**Goal**: Future contributors and agent-assisted maintainers preserve the same moderate didactic-comment rule when new or changed non-trivial logic needs learner-facing explanation.

**Independent Test**: A reviewer can inspect maintained guidance surfaces and the feature evidence to see whether shared guidance changed, stayed unchanged by rationale, or intentionally diverged with documentation.

### Review and Validation Guard for US4

- [X] T067 [US4] Review existing comment guidance in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`; record in `pr-evidence.md` whether the rule is already synchronized or needs updates.
- [X] T068 [US4] Review `.specify/templates/` impact; record `N/A` in `pr-evidence.md` unless repository-owned templates are intentionally changed by this implementation.

### Implementation for US4

- [X] T069 [US4] If project-wide comment guidance changes, update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` together with the same didactic inline-comment rule.
- [X] T070 [US4] If the guidance surfaces intentionally diverge for agent-specific reasons, document the exact divergence and rationale in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- [X] T071 [US4] If shared guidance changes active agent context, run `.specify/scripts/bash/update-agent-context.sh` for `codex`, `claude`, `gemini`, and `copilot`, then review generated agent-surface diffs for parity before keeping them.
- [X] T072 [US4] If shared guidance does not change, record the unchanged rationale in `pr-evidence.md`, including that feature-local comments and evidence did not require another agent-surface update.

**Checkpoint**: Agent guidance impact is explicit, synchronized when changed, and bounded when unchanged.

## Phase 7: Governance and Documentation Evidence

**Purpose**: Convert the six preset matrix into concrete feature evidence without expanding runtime scope.

- [X] T073 Record Security Governance v0.6.0, NIST SSDF, CWE Top 25, and C#/.NET secure-coding context as `Applicable` in the governance table in `specs/015-didactic-comment-hardening/pr-evidence.md`; fill all audit fields, including owner, reviewer, review date, evidence path, result, residual risk, and follow-up.
- [X] T074 Record ASVS, SBOM, VEX, SLSA, OpenSSF Scorecard, and AI-SBOM as trigger-based `N/A` governance rows in `pr-evidence.md` unless web/API/auth, dependency, release, provenance, public OSS posture, runtime AI, models, datasets, AI infrastructure, or delivered AI components enter scope; fill all audit fields, including rationale and re-evaluation trigger, for every row.
- [X] T075 Record NIS2, CRA, EU AI Act, and DORA as trigger-based `N/A` governance rows in `pr-evidence.md` unless market-placement, customer handover, vulnerability process, cloud operation, financial-sector ICT dependency, regulated customer flow, or runtime/product AI scope changes; fill all audit fields, including rationale and re-evaluation trigger, for every row.
- [X] T076 Record Architecture Governance v0.5.0 and iSAQB Architecture Governance v0.2.0 as `Applicable` governance context in `pr-evidence.md`; record STRIDE, CIA, CAPEC, S-ADR, arc42 security concepts, Zero Trust, SAMM, BSI C3A, and BSI C5 as `N/A` because no trust boundary, cloud service, provider dependency, distributed service flow, deployment topology, or architecture structure changes, with complete audit fields.
- [X] T077 Record A11Y Governance v0.4.0 as `Applicable` in `pr-evidence.md`: changed Markdown evidence and guidance remain text-first and didactic explanation blocks follow German-first/English-second CEFR-B2; record generated DocFX/WCAG proof as trigger-based `N/A` unless generated documentation or navigation changes, with complete audit fields.
- [X] T078 Record Cross-Platform Governance v0.2.0 as `Applicable` governance context in `pr-evidence.md`; record script parity, Bash/Pwsh pairs, man page, Cmdlet naming, `--dry-run`, and `-WhatIf` as `N/A` because no script-shaped tool is added or changed, with complete audit fields including rationale and re-evaluation trigger.
- [X] T079 Record Agent Parity Governance v0.3.0 as `Applicable` in `pr-evidence.md`: maintained surfaces are `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`; record `.specify/templates/` as `N/A` unless intentionally changed, and fill all audit fields, including owner, reviewer, review date, evidence path, residual risk, and follow-up.
- [X] T080 Run or explicitly review `dotnet list package --outdated` as dependency-currency evidence; do not update packages in this feature unless a separate dependency-scope decision is approved and recorded.
- [X] T081 If any governance trigger changes from `N/A` to `Applicable`, update the proper evidence home under `docs/security/`, `docs/architecture/`, or another documented project-local governance surface; if any decision is `Open`, record owner, concrete follow-up, and re-evaluation trigger before acceptance.
- [X] T082 Update `docs/project-statistics.md` after implementation completion with the 015 work window, touched artifact mix, validation evidence, and acceleration baselines, unless the final evidence records a justified deferment.

**Checkpoint**: Governance evidence is complete, trigger based, and proportional to a comment-only feature.

## Phase 8: Validation and Acceptance

**Purpose**: Prove the implementation stayed inside scope and did not reduce accepted behavior or proof coverage.

- [X] T083 Before any build or test command, align `Directory.Build.props` to the current `1.15.<patch>.<build>` branch version and increment the manual build counter once for that build/test run.
- [X] T084 Run `git diff --check` from the repository root and record the result in `specs/015-didactic-comment-hardening/pr-evidence.md`.
- [X] T085 Run `dotnet format --verify-no-changes` from the repository root and record the result in `pr-evidence.md`.
- [X] T086 Run targeted tests for every touched source or test-helper module, using the selected commands from T019 and T043; record command, scope, and result in `pr-evidence.md`.
- [X] T087 If shared logic or broad smoke-helper proof was materially touched, run `dotnet test --configuration Release` and record the result in `pr-evidence.md`.
- [X] T088 If shared logic or broad smoke-helper proof was materially touched, run `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` and record the coverage-gate result for the required assemblies in `pr-evidence.md`.
- [X] T089 If XML comments, public API signatures, generated API documentation, documentation navigation, or learner-facing guides changed, run `docfx docfx.json` and record the result in `pr-evidence.md`.
- [X] T090 If T089 was triggered, run `npm run test:docfx` from `tests/web-a11y/` after DocFX generation and record the result in `pr-evidence.md`.
- [X] T091 Review changed Markdown evidence, statistics, and guidance for text-first accessibility: German-first/English-second CEFR-B2 where user-facing, correct German umlauts and `ß`, language tags on fenced code blocks, semantic headings, readable tables/lists, no color-only meaning, no pointer-only instructions, and no generated output tracked.
- [X] T092 Scan `specs/015-didactic-comment-hardening/pr-evidence.md` and changed planning/evidence files for unresolved clarification markers, task-generation markers, empty review decisions, silently omitted governance checkpoints, or missing audit fields; fix any finding before acceptance.
- [X] T093 Verify all required hotspot categories have at least one evidence entry or explicit no-current-area rationale in `pr-evidence.md`.
- [X] T094 Verify every reviewed file or named flow area has exactly one primary decision and that `CommentState` distinguishes `Changed`, `Unchanged`, `Removed`, and `NotApplicable`.
- [X] T095 Verify all `CommentNeeded`, `UpdateExistingComment`, `CommentAdequate`, `NoCommentNeeded`, and `FollowUpHardening` rows satisfy their decision-specific evidence rules.
- [X] T096 Review the final diff to confirm no runtime behavior, public API, dependency, project structure, example scope, generated output, or `tv203s/` edit slipped into the feature.
- [X] T097 If implementation completed the Lastenheft scope and repository delivery rules require archiving, run the paired Lastenheft rename path for macOS/Linux and Windows documentation parity: `bash scripts/rename-lastenheft.sh Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md 015-didactic-comment-hardening` and the matching PowerShell path when Windows validation is available.

**Checkpoint**: Validation evidence is complete, proportional, and tied to the touched files.

## Phase 9: PR Preparation and Final Version Boundary

**Purpose**: Prepare a reviewable final change set with evidence, statistics, version alignment, and PR text.

- [X] T098 Add the final acceptance summary to `specs/015-didactic-comment-hardening/pr-evidence.md`, covering purpose, touched projects, changed files or flow areas, decision counts by review model value, line-budget counts for the 90% SC-004 threshold, validation commands, DocFX/A11Y trigger result, governance counts for `Applicable`/`N/A`/`Open`, reviewer/date and residual-risk completeness, open follow-ups, agent guidance result, statistics result, config/API impact, and follow-up boundaries.
- [X] T099 Before final commit or push, align `Directory.Build.props` to the current branch version `1.15.<patch>.<build>` without incrementing the build counter unless another build or test command is run.
- [X] T100 Run `git status --short --branch -uall` and review untracked files; keep only intended source, test, evidence, guidance, statistics, version, and Lastenheft archive changes.
- [X] T101 Prepare the PR description from `specs/015-didactic-comment-hardening/pr-evidence.md`, including scope, changed areas, validation evidence, governance applicability, and no-runtime-change statement.
- [X] T102 If the remote PR already exists, update it with the final evidence summary; if no PR exists and the user requests one, create a PR for branch `015-didactic-comment-hardening`.

**Checkpoint**: The branch is ready for commit, push, and PR review when the user asks for those actions.

## Dependencies and Execution Order

- Phase 1 blocks all later work because `pr-evidence.md` and scope guard are required before review decisions are recorded.
- Phase 2 blocks user-story implementation because every hotspot category must be mapped before comments are changed.
- US1 and US2 are both P1. They may be reviewed in either order after Phase 2, but writes to `pr-evidence.md` must remain serial.
- US3 depends on at least one completed US1 or US2 review slice and finishes the decision-model quality gate.
- US4 depends on the outcome of shared-guidance review and should run after it is clear whether project-wide rules changed.
- Phase 7 can begin after Phase 2 but must be finalized after US1 through US4 so trigger decisions reflect the actual touched files.
- Phase 8 depends on all intended file edits being complete.
- Phase 9 depends on final validation and evidence completion.

## Implementation Strategy

1. Complete setup, evidence columns, and hotspot inventory.
2. Deliver the P1 source-flow slice for US1 with targeted evidence and comments.
3. Deliver the P1 smoke-helper slice for US2 with proof-boundary evidence and comments.
4. Run the US3 noise-control pass to separate adequate, unnecessary, updated, new, and follow-up decisions.
5. Review US4 guidance synchronization only if shared rules changed or need an explicit unchanged rationale.
6. Finalize governance, validation, statistics, version alignment, and PR evidence.
