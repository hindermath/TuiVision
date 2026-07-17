# Tasks: Wave-6 TVFM Showcase Remediation

**Input**: Accepted artifacts under
`specs/036-wave6-tvfm-showcase-remediation/` and binding
`Lastenheft_21_Wave6-TVFM-Showcase-Remediation.036-wave6-tvfm-showcase-remediation.md`
**Delivery mode**: `MergeAndSync`
**Scope**: Visible Wave-6 Stage 2 for the existing `Tp7FileManager`; no
functional re-port, Feature 037, independent closure, or portfolio audit

## Phase 1: Setup and Evidence Foundation

**Purpose**: Lock identity, authority, scope, cardinalities, governance, and
delivery evidence before executable changes.

- [X] T001 Verify branch, `.specify/feature.json`, baseline ancestry, and dirty-path ownership in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T002 Record Feature-035 PR #101, closeout PR #102, and intake PR #103 ancestry in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T003 Run `specify check` and prerequisite discovery and record exit/error-channel review in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T004 Verify every Feature-036 checklist has zero incomplete items in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T005 Record all seven installed preset versions and priorities in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T006 Create and validate `specs/036-wave6-tvfm-showcase-remediation/autonomous-run-state.json`
- [X] T007 Create and validate `specs/036-wave6-tvfm-showcase-remediation/autonomous-gate-requirements.json`
- [X] T008 Record `MergeAndSync` authority and its remote, review, and bypass limits in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T009 Record hard exclusions, read-only roots, stop boundaries, and no-intentional-interruption decision in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T010 Record shared single-writer paths and serialization rules in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T011 Record the `1.36.<patch>.<build>` version scheme and one-counter-increment-per-build/test rule in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T012 Record exact one-entry-point and ten-`W6S` cardinalities in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T013 Record the non-recursive closeout boundary `specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md`
- [X] T014 Inventory intended source, test, guide, evidence, status, and archive paths in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T015 Confirm no generated output, external checkout, credential, cache, log, test result, or arbitrary user data is intended for Git
- [X] T016 Rehash all 24 accepted `TVFM/` sources and record the Feature-035 baseline in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T017 Initialize exactly ten `W6S-001` through `W6S-010` rows in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T018 Initialize exactly one `Tp7FileManager` entry-point decision row in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T019 Record governance checkpoint rows for all seven presets in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T020 Refresh accepted artifact hashes and run-state checkpoint before task execution

---

## Phase 2: Foundational UI and Proof Ownership

**Purpose**: Review the existing functional authority and establish exact
view, event, dialog, mouse, and test ownership before the Red slice.

- [X] T021 Review Feature-035 workspace, models, application, tests, guide, and evidence boundaries and record reuse decisions in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T022 Review existing `TWindow`, `TListBox`, `TStringList`, `TStaticText`, and focus contracts in `src/TuiVision.Controls/`
- [X] T023 Review existing `TDialog`, `TInputLine`, `TButton`, default-button, Enter, Escape, Tab, and modal dispatch contracts in `src/TuiVision.Controls/`
- [X] T024 Review existing menu, command, StatusLine, Description, event queue, and `app.Run()` proof patterns in `examples/Shared/` and `tests/TuiVision.Examples.SmokeTests/`
- [X] T025 Review existing mouse routing, coordinate, capability-loss, removal, and shutdown contracts in `src/TuiVision.Core/` and `src/TuiVision.Controls/`
- [X] T026 Confirm `ControlledFileWorkspace.cs` and `Wave6FileModels.cs` remain the only functional and filesystem authority
- [X] T027 Record planned example-local showcase view and dialog-state ownership in `specs/036-wave6-tvfm-showcase-remediation/plan.md` without creating source before the Red slice
- [X] T028 Define primary, supplemental, and setup-only proof boundaries for `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T029 Confirm no new project, dependency, public framework API, second entry point, or reusable local framework replacement is required
- [X] T030 Update `specs/036-wave6-tvfm-showcase-remediation/autonomous-run-state.json` to the Analyze-ready Tasks checkpoint

**Checkpoint**: Existing contracts and ownership are understood; no showcase
behavior is accepted yet.

---

## Phase 3: User Story 1 - Persistent Navigation Reference Slice (Priority: P1)

**Goal**: Replace transient summaries with a persistent first-frame
composition that proves navigation, focus, status, Description, and normal
plus `48x16` rendering.

**Independent Test**: The real application loop shows the controlled path and
list, moves focus and selection, opens Description, renders concrete cells in
both viewports, and exits through the normal command path.

- [X] T031 [US1] Add failing first-frame purpose, root-path, list, selection, and primary-hint assertions in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T032 [US1] Add failing persistent view-tree identity and focused-list assertions in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T033 [US1] Add failing real app-loop navigation, selection, and status transition assertions in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T034 [US1] Add failing F1 and `Help -> Description` content assertions in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T035 [US1] Add failing normal-layout cell-region assertions in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T036 [US1] Add failing `48x16` no-overlap, focus, status, Description, and quit-path assertions in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T037 [US1] Increment `Directory.Build.props` for the reference-slice Red test invocation
- [X] T038 [US1] Run the reference-slice Release tests Red and accept only missing showcase behavior
- [X] T039 [US1] Record the Red command, version, exit/error review, and expected failures in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T040 [US1] Implement the persistent Wave-6 main window and stable child-view identities in `examples/Shared/TuiVision.Examples.Wave6/Wave6ShowcaseViews.cs`
- [X] T041 [US1] Implement the focusable controlled snapshot list using existing `TListBox` and `TStringList` contracts
- [X] T042 [US1] Implement text-first path, selection, metadata, mode, and safety regions using existing controls
- [X] T043 [US1] Compose the persistent main showcase in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T044 [US1] Synchronize list selection and navigation exclusively through existing `ControlledFileWorkspace` snapshots
- [X] T045 [US1] Implement focus-aware StatusLine text and stable primary keyboard hints
- [X] T046 [US1] Implement DE-first/EN-second F1 Description with purpose, safety, modernization, platform, and proof boundaries
- [X] T047 [US1] Implement normal and `48x16` layout selection without overlapping essential state
- [X] T048 [US1] Add concise bilingual why-comments only around non-trivial layout, focus, and snapshot synchronization
- [X] T049 [US1] Increment `Directory.Build.props` for the reference-slice Green test invocation
- [X] T050 [US1] Run the reference-slice Release tests Green
- [X] T051 [US1] Complete `W6S-001`, `W6S-009`, and initial `W6S-010` evidence in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T052 [US1] Verify unchanged Feature-035 navigation and preview tests remain green within the same targeted invocation
- [X] T053 [US1] Refresh task progress and accepted hashes in `specs/036-wave6-tvfm-showcase-remediation/autonomous-run-state.json`

**Checkpoint**: A complete visible and keyboard-operable reference slice is
proven before broader commands or mutations.

---

## Phase 4: User Story 2 - Visible Read-Only Commands (Priority: P2)

**Goal**: Expose preview, filter, sort, tags, search, association, palette,
resources, and fallbacks through visible menus, controls, status, and cells.

**Independent Test**: Real menu/command dispatch changes visible typed state
and rendered output without external execution or filesystem mutation.

- [X] T054 [US2] Add failing File/Navigate/View/Search/Options/Help menu-group and command-identity tests in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T055 [US2] Add failing keyboard reachability and honest command-availability tests for all Feature-035 core commands
- [X] T056 [US2] Add failing text-preview, truncation, invalid-UTF-8, and status/cell tests
- [X] T057 [US2] Add failing hex-preview, offset, printable-region, and status/cell tests
- [X] T058 [US2] Add failing filter, sort, tag, selection-preservation, and empty-result tests
- [X] T059 [US2] Add failing bounded search match, cancellation, limit, and partial-result tests
- [X] T060 [US2] Add failing internal association, text/hex viewer, and unsupported-fallback tests
- [X] T061 [US2] Add failing closed palette, resource choice, high-contrast, and unknown-value fallback tests
- [X] T062 [US2] Increment `Directory.Build.props` for the read-surface Red test invocation
- [X] T063 [US2] Run read-surface Release tests Red and accept only missing showcase behavior
- [X] T064 [US2] Record the Red command and expected read-surface boundaries in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T065 [US2] Implement closed File/Navigate/View/Search/Options/Help menus in `examples/Shared/TuiVision.Examples.Wave6/Tp7FileManagerApp.cs`
- [X] T066 [US2] Bind visible command labels, shortcuts, enablement rules, and status hints to stable example-local IDs
- [X] T067 [US2] Render existing bounded text and hex preview results in the persistent detail region
- [X] T068 [US2] Render filter, sort, tag, and selection state without duplicating Feature-035 logic
- [X] T069 [US2] Render existing search result, cancellation, and resource-limit states
- [X] T070 [US2] Render only existing internal viewer and honest fallback decisions
- [X] T071 [US2] Implement the closed normal/high-contrast palette and deterministic resource labels
- [X] T072 [US2] Add concise bilingual why-comments around bounded rendering and no-external-viewer decisions
- [X] T073 [US2] Increment `Directory.Build.props` for the read-surface Green test invocation
- [X] T074 [US2] Run read-surface Release tests Green
- [X] T075 [US2] Complete `W6S-002`, `W6S-003`, `W6S-004`, `W6S-005`, and `W6S-008` evidence in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T076 [US2] Refresh task progress and accepted hashes in `specs/036-wave6-tvfm-showcase-remediation/autonomous-run-state.json`

---

## Phase 5: User Story 3 - Safe Mutation Dialogs (Priority: P3)

**Goal**: Present copy, rename, delete, and read-only operations through real
focusable dialogs that preserve explicit intent, revalidation, and
non-overwrite boundaries.

**Independent Test**: Every operation proves Preview, Confirm, Cancel,
Enter/Escape, focus order, revalidation, terminal result, and a relevant
negative or recovery path through the real application loop.

- [X] T077 [US3] Add failing copy-dialog source, target, preview, validation, focus-order, Confirm, and Cancel tests in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T078 [US3] Add failing rename-dialog leaf-name, conflict, Enter, Escape, and no-overwrite tests
- [X] T079 [US3] Add failing delete-dialog non-recursive, explicit-confirmation, and Cancel-no-write tests
- [X] T080 [US3] Add failing read-only-dialog capability, target-state, confirmation, and unsupported-platform tests
- [X] T081 [US3] Add failing empty, absolute, traversal, link, outside-root, and over-limit input tests
- [X] T082 [US3] Add failing stale source, changed target, removed source, and one-shot revalidation tests
- [X] T083 [US3] Add failing operation-result, rejection, recovery-boundary, StatusLine, and cell tests
- [X] T084 [US3] Add failing modal Tab, Shift+Tab, default Enter, Escape, and F1 dispatch tests
- [X] T085 [US3] Increment `Directory.Build.props` for the dialog Red test invocation
- [X] T086 [US3] Run mutation-dialog Release tests Red and accept only missing presentation behavior
- [X] T087 [US3] Record the Red command and every expected dialog failure boundary in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T088 [US3] Implement immutable operation-dialog state in `examples/Shared/TuiVision.Examples.Wave6/Wave6ShowcaseViews.cs`
- [X] T089 [US3] Compose copy and rename dialogs from existing `TDialog`, `TInputLine`, `TStaticText`, and `TButton`
- [X] T090 [US3] Compose delete and read-only dialogs without fabricated target input
- [X] T091 [US3] Implement stable insertion/focus order, default Confirm, Cancel, Escape, and contextual Description behavior
- [X] T092 [US3] Validate only bounded root-relative target/name input and show normalized Preview plus safety boundary
- [X] T093 [US3] Prepare existing Feature-035 intents only after visible validation and explicit user decision
- [X] T094 [US3] Revalidate immediately before execution through `ControlledFileWorkspace` and render typed terminal results
- [X] T095 [US3] Preserve non-recursive delete, no-overwrite copy/rename, one-shot authorization, and safe unsupported results
- [X] T096 [US3] Add concise bilingual why-comments around explicit confirmation, TOCTOU revalidation, and modal focus
- [X] T097 [US3] Increment `Directory.Build.props` for the dialog Green test invocation
- [X] T098 [US3] Run mutation-dialog and preserved Feature-035 operation tests Green
- [X] T099 [US3] Verify all Cancel, Escape, invalid, stale, conflict, and unsupported fixtures remain byte-consistent where required
- [X] T100 [US3] Complete `W6S-006` evidence in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T101 [US3] Refresh task progress and accepted hashes in `specs/036-wave6-tvfm-showcase-remediation/autonomous-run-state.json`

---

## Phase 6: User Story 4 - Optional Mouse Intent Parity (Priority: P4)

**Goal**: Let a bounded drag gesture prepare the same confirmed operation as
the keyboard path without direct mutation or lost fallback.

**Independent Test**: Mouse and keyboard prepare equivalent typed intent;
invalid target, Escape, capability loss, view removal, and shutdown produce
`NoMutation`.

- [X] T102 [US4] Add failing valid mouse-down/move/release intent-preparation parity tests in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T103 [US4] Add failing proof that release cannot call workspace execution or mutate a fixture
- [X] T104 [US4] Add failing invalid-source, invalid-target, outside-region, and unsupported-button tests
- [X] T105 [US4] Add failing Escape, capability-loss, view-removal, and shutdown cancellation tests
- [X] T106 [US4] Add failing complete keyboard fallback and equivalent confirmation-dialog tests
- [X] T107 [US4] Add failing constrained-layout mouse-target and non-overlap tests
- [X] T108 [US4] Increment `Directory.Build.props` for the mouse-parity Red test invocation
- [X] T109 [US4] Run mouse-parity Release tests Red and accept only missing bounded presentation behavior
- [X] T110 [US4] Record the Red command and no-mutation boundaries in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T111 [US4] Implement prepared drag state with selected source, visible target, capability, and cancellation reason
- [X] T112 [US4] Route mouse events through existing group/application dispatch and visible coordinate ownership
- [X] T113 [US4] Convert valid release into the same prepared confirmation request as the keyboard path
- [X] T114 [US4] Clear drag state on invalid target, Escape, capability loss, view removal, and shutdown
- [X] T115 [US4] Prevent direct workspace execution from every mouse event path
- [X] T116 [US4] Add concise bilingual why-comments around mouse authority and cancellation boundaries
- [X] T117 [US4] Increment `Directory.Build.props` for the mouse-parity Green test invocation
- [X] T118 [US4] Run mouse-parity and preserved mouse interaction tests Green
- [X] T119 [US4] Complete `W6S-007` and remaining `W6S-010` evidence in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`

---

## Phase 7: User Story 5 - Exact Showcase Closure (Priority: P5)

**Goal**: Close one entry point and ten showcase areas with deterministic
framework, proof, deviation, boundary, risk, and re-evaluation evidence.

**Independent Test**: The feature validator accepts exactly 1/10 complete
rows and rejects missing, duplicate, unknown, open, planned, or inconsistent
evidence.

- [X] T120 [US5] Add exact one-entry and ten-area positive parser tests in `tests/TuiVision.Examples.SmokeTests/Wave6ShowcaseSmokeMatrixTests.cs`
- [X] T121 [US5] Add missing, duplicate, unknown, out-of-order, and malformed area negative fixtures
- [X] T122 [US5] Add invalid framework-decision and invalid entry-decision negative fixtures
- [X] T123 [US5] Add `Planned`, `Open`, empty-cell, missing-proof, missing-risk, and missing-re-evaluation negative fixtures
- [X] T124 [US5] Add inconsistent accepted-row, open-`ShowcaseDelta`, and unowned-follow-up negative fixtures
- [X] T125 [US5] Add exact 24-source-hash and protected-root drift rejection tests
- [X] T126 [US5] Increment `Directory.Build.props` for the evidence-validator test invocation
- [X] T127 [US5] Run evidence-validator Release tests and record positive and negative results
- [X] T128 [US5] Complete all ten framework decisions in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [X] T129 [US5] Complete exactly one `Tp7FileManager` entry-point decision with no open `ShowcaseDelta`
- [X] T130 [US5] Verify every Feature-035 core command has visible keyboard-reachable access
- [X] T131 [US5] Verify all four mutation types have Preview, Confirm, Cancel, revalidation, result, and negative/recovery proof
- [X] T132 [US5] Verify all tested mouse paths remain non-mutating before confirmation and retain keyboard fallback
- [X] T133 [US5] Verify normal and `48x16` app-loop, focus, view, status, Description, and cell matrices
- [X] T134 [US5] Rehash all 24 `TVFM/` sources and prove `TVDEMOS/` and `tv203s/` unchanged
- [X] T135 [US5] Permit `SmallFrameworkFix` only after explicit Red/Green reusable proof; stop on `ProductDecision`, unsafe authority, or broad framework gap; otherwise record owned `FollowUpHardening` with evidence and re-evaluation trigger
- [X] T136 [US5] Refresh task progress and accepted hashes in `specs/036-wave6-tvfm-showcase-remediation/autonomous-run-state.json`

---

## Phase 8: Documentation, Governance, and Repository Integration

**Purpose**: Make the Stage-2 showcase learnable, accessible, auditable, and
traceable across all maintained repository surfaces.

- [X] T137 Update `docs/guides/examples/tp7-file-manager.md` with first frame, menus, keyboard, dialogs, mouse fallback, safety, constrained layout, platform, modernization, and proof
- [X] T138 Update `examples/README.md` with normal, `--smoke`, primary-action, F1, and `Ctrl+Q` launch paths
- [X] T139 Update `docs/toc.yml` only if navigation does not already expose the Wave-6 guide
- [X] T140 Review changed Markdown for semantic headings, fenced-language tags, UTF-8, DE-first/EN-second CEFR-B2, and text-first access
- [X] T141 Review keyboard inventory, focus visibility, StatusLine, Description, High Contrast, and no-color-only meaning
- [X] T142 Review new non-trivial logic for selective didactic comments and public surfaces for XML documentation triggers
- [X] T143 Complete NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC, secure-filesystem, and evidence-integrity governance rows
- [X] T144 Complete ASVS, supply-chain artefact, AI-SBOM, S-ADR/arc42, Zero Trust/SAMM, BSI C3A/C5, and regulatory N/A rows with triggers
- [X] T145 Complete iSAQB, A11Y, cross-platform, agent-parity, and autonomous-run governance rows
- [X] T146 Synchronize active Feature-036 context across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [X] T147 Review generated Antigravity context in `.agent/rules/specify-rules.md` against the accepted plan
- [X] T148 Run agent homogeneity/parity checks and record every intentional structural deviation
- [X] T149 Update `Pflichtenheft.md` and `Lastenheft_Abarbeitungsreihenfolge.md` to show Stage-2 delivery without starting Feature 037
- [X] T150 Update `docs/project-statistics.md` for the complete Feature-036 candidate
- [X] T151 Archive Lastenheft 21 through the repository rename workflow and update references without creating a later intake

---

## Phase 9: Local Validation and Exact Candidate

**Purpose**: Prove the exact candidate under repository-wide static,
behavioral, documentation, security, and state gates.

- [X] T152 Run `git diff --check`, placeholder, generated-path, protected-root, dependency/package/project-scope, and secret scans
- [X] T153 Run `dotnet format TuiVision.sln --verify-no-changes`
- [X] T154 Increment `Directory.Build.props` for the complete targeted Wave-6 showcase and safety test invocation
- [X] T155 Run all targeted Wave-6 showcase, workspace, operation, and functional Release tests and record exact counts
- [X] T156 Run the controlled normal PTY start with primary action, F1, and `Ctrl+Q` using existing build output
- [X] T157 Run the controlled `--smoke` path and prove deterministic termination using existing build output
- [X] T158 Increment `Directory.Build.props` for the full Release solution test invocation
- [X] T159 Run `dotnet test TuiVision.sln --configuration Release` and record exact results
- [X] T160 Validate `coverlet.runsettings` with `xmllint --noout` where available
- [X] T161 Increment `Directory.Build.props` for the canonical coverage invocation
- [X] T162 Run canonical Coverlet coverage and record all five assembly percentages
- [X] T163 Run `docfx docfx.json` and record warning/error results
- [X] T164 Run `tests/web-a11y` Playwright/Axe and record the accessibility result
- [X] T165 Run local supply-chain, agent parity, UTF-8/text-first, and both autonomous state validators with explicit repository root
- [X] T166 Verify exact 1/10 evidence cardinalities, decision consistency, SC outcomes, and all non-triggered conditional gates
- [X] T167 Verify the final diff has no functional-authority, public API, dependency, project, historical, arbitrary-user-data, Feature-037, or portfolio-audit expansion
- [X] T168 Refresh accepted artifact/task hashes and validate both autonomous state validators
- [X] T169 Align `Directory.Build.props` to final `1.36.<patch>.<build>` without an extra counter increment
- [X] T170 Stage only intended files and run `git diff --cached --check` plus staged/untracked/unstaged inventory

---

## Phase 10: PR, Review, Merge, Sync, and Retrospective

**Purpose**: Deliver only the reviewed candidate and finish on clean,
synchronized `main`.

- [ ] T171 Commit the exact Feature-036 candidate and record commit identity without self-invalidating evidence in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T172 Push `036-wave6-tvfm-showcase-remediation`, create a non-empty feature PR, and record both identities in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T173 Identify PR-context required checks and record duplicate push runs without unsafe cancellation in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T174 Monitor Ubuntu, macOS, Windows, docs/A11Y, supply-chain, parity, and full-test gates to terminal state and record results in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T175 Map every Applicable gate to the actual workflow, job, platform, and executed command in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T176 Validate temporary exact-head provider evidence against `specs/036-wave6-tvfm-showcase-remediation/autonomous-gate-requirements.json` and record the result in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T177 Inspect Copilot, Claude, PR comments, and GraphQL review threads, resolve every actionable finding, and record review state in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T178 Re-run affected validation after any review correction and record refreshed exact-head evidence in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T179 Use the narrow admin bypass only if all technical gates are green and Human Approval is the sole open rule, recording the decision in `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
- [ ] T180 Merge the feature PR with a merge commit, delete the remote feature branch, and record externally verified merge facts for `specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md`
- [ ] T181 If truthful post-merge facts require it, create one causal evidence-only closeout PR containing `delivery-closeout.md`, completed `retrospective.md`, final task/state facts, and no recursive self-claim
- [ ] T182 Switch locally to `main`, fetch/prune, fast-forward pull, and record the external synchronization proof in `specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md`
- [ ] T183 Prove a clean working tree and `HEAD == origin/main` against the merged feature or closeout head
- [ ] T184 Verify the merged `specs/036-wave6-tvfm-showcase-remediation/retrospective.md` contains the established promotion classification
- [ ] T185 Verify a reproducible provider-neutral defect has one bounded `PresetFollowUp`; otherwise verify `NoPromotion` without an empty branch or PR
- [ ] T186 Verify the merged run state is `Retrospective`, `Completed`, `187/187`, and `nextExactAction: N/A`
- [ ] T187 Report final 1/10 decisions, validation, PR/merge, follow-ups, and main-sync proof without starting Feature 037

## Dependencies and Execution Order

1. Phase 1 precedes every implementation edit.
2. Phase 2 establishes UI, dialog, mouse, and proof ownership.
3. US1 is the mandatory vertical reference slice.
4. US2 exposes read-only commands only after US1 is green.
5. US3 adds mutation dialogs only after visible read paths are stable.
6. US4 may prepare only the US3 confirmation path and never executes directly.
7. US5 closes evidence only after all behavior and boundaries are proven.
8. Shared evidence, application, tests, guidance, version, statistics, and
   delivery files remain serialized.
9. No task creates Feature 037, an independent closure, or the portfolio audit.

## Parallel Opportunities

No `[P]` markers are used. Nearly every slice touches the shared
`Tp7FileManagerApp`, showcase test matrix, evidence ledger, run state, or
version file. Serial execution is the safer and more reviewable plan.

## Implementation Strategy

1. Prove a persistent navigation, focus, status, Description, and layout slice.
2. Expose existing read-only functions without duplicating domain logic.
3. Add mutations only through visible existing intent and revalidation rules.
4. Add optional mouse preparation only after keyboard confirmation is proven.
5. Close exact evidence and learner documentation.
6. Run repository-wide validation, deliver, review, merge, and return to
   synchronized `main`.
