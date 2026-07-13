# Tasks: Core Runtime Conformance Hardening

**Input**: Accepted artifacts in `specs/025-core-runtime-conformance-hardening/`
**Tests**: Red-first MSTest proof is mandatory for every finding.
**Execution**: Tasks are sequential unless explicitly marked `[P]`; shared evidence, source, test, version, audit, documentation, agent, and remote-delivery files are single-writer surfaces.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Safe only when the named files and acceptance evidence are independent.
- **[Story]**: User story from `spec.md`.
- Every `dotnet test` invocation has its own immediately preceding build-counter task.

## Phase 1: Setup and Evidence Foundation

**Purpose**: Prove the authorized starting state and create the evidence ledger before implementation or test edits.

- [X] T001 Create `specs/025-core-runtime-conformance-hardening/pr-evidence.md` from `.specify/templates/autonomous-run-evidence-template.md` with delivery authority, scope firewall, convergence gates, Finding and governance tables
- [X] T002 Verify branch `025-core-runtime-conformance-hardening`, `.specify/feature.json`, clean historical/external source boundaries, and `HEAD` ancestry in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T003 Run `specify check`, `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks`, and the zero-incomplete-checklist scan; record results in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T004 Record deterministic validator triggers before edits, including Feature-024 JSON, public inventory, agent parity, Pflichtenheft markers, Lastenheft archive, versioning, documentation, coverage, and generated-output checks in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T005 Verify the pinned Free Vision commit and manifest hashes plus the reviewed `tv203s/` implementations/headers without modifying either source in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T006 Review compile surfaces before red tests: imports, complete public XML documentation, harness helpers, focus/ownership assertions, linked-source assembly identity, and Controls-to-Compatibility dependency direction; record the result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T007 Reconcile `F001` through `F009` against Feature-024 `Core025` findings and reserve exactly one evidence row per finding in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T008 Record the seven-preset governance checkpoint inventory with owner, reviewer, date, result, residual risk, follow-up, and re-evaluation trigger in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T009 Record the optional before-tasks/before-implement commit-hook deferral and the accepted plan/analyze checkpoint boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: Evidence exists, all validators are declared, source authority is reproducible, and no implementation file has changed.

---

## Phase 2: Foundational Event and Real-Ingress Contracts

**Purpose**: Complete the `F001` reference slice and `F008` canonical real keyboard ingress before later loop and interaction proofs.

### F001 reference slice

- [X] T010 Add explicit failing concrete-kind and composite/mask/unknown-kind tests for `F001` in `tests/TuiVision.Core.Tests/Test1.cs`
- [X] T011 Increment only the manual build counter in `Directory.Build.props` before the `F001` red `dotnet test`
- [X] T012 Run the filtered Release `F001` red test in `tests/TuiVision.Core.Tests/TuiVision.Core.Tests.csproj` and confirm failure at the current mask guard
- [X] T013 Record the exact `F001` red command, expected failure, actual boundary, and historical relation in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T014 Implement the exact concrete mouse-kind allow-list with reason-focused didactic comments and complete affected XML docs in `src/TuiVision.Core/TEvent.cs`
- [X] T015 Increment only the manual build counter in `Directory.Build.props` before the `F001` green `dotnet test`
- [X] T016 Run the filtered Release `F001` test in `tests/TuiVision.Core.Tests/TuiVision.Core.Tests.csproj` and prove concrete kinds pass while masks/composites/unknown kinds fail before dispatch
- [X] T017 Complete the `F001` Finding row as `Implemented` with real-boundary result and residual filter-mask boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

### F008 canonical real keyboard ingress

- [X] T018 Add failing raw-`ConsoleKeyInfo` production-ingress and modifier tests for `F008` in `tests/TuiVision.Controls.Tests/TProgramTests.cs` and `tests/TuiVision.Controls.Tests/TWindowTests.cs`
- [X] T019 Increment only the manual build counter in `Directory.Build.props` before the `F008` red `dotnet test`
- [X] T020 Run the filtered Release `F008` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm the production path bypasses canonical translation or uses the wrong Ctrl bit
- [X] T021 Record the `F008` red boundary and the verified no-cycle/no-packaging-conflict dependency result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T022 Add the bounded project reference from `src/TuiVision.Controls/TuiVision.Controls.csproj` to existing `src/TuiVision.Compatibility/TuiVision.Compatibility.csproj` without package changes
- [X] T023 Route the controlled and real `ConsoleKeyInfo` path through `TConsoleInputAdapter` with an overridable deterministic ingress seam and reason-focused XML/comments in `src/TuiVision.Controls/TProgram.cs`
- [X] T024 Replace the local window Ctrl-bit literal with the canonical modifier contract in `src/TuiVision.Controls/TWindow.cs`
- [X] T025 Increment only the manual build counter in `Directory.Build.props` before the `F008` green `dotnet test`
- [X] T026 Run the filtered Release `F008` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove letters, navigation, function keys, modifiers, Ctrl+W, Alt shortcuts, and unknown fallbacks through production ingress
- [X] T027 Complete the `F008` Finding row with macOS/Linux behavior and Windows/WSL CI proof boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: Concrete event construction and canonical production keyboard ingress are independently proven.

---

## Phase 3: User Story 1 - Events, Focus, and Hierarchy Stay Consistent (Priority: P1)

**Goal**: Close `F002` and `F003` with pre-mutation focus veto and state-specific hierarchy propagation.

**Independent Test**: A real Group transition accepts, rejects, or no-ops exactly once and leaves one coherent focus/state/announcement tree.

### F002 focus veto

- [X] T028 [US1] Add failing accepted/rejected/no-op, exactly-once veto, removal/disable, and announcement tests for `F002` in `tests/TuiVision.Controls.Tests/TGroupTests.cs`
- [X] T029 [US1] Increment only the manual build counter in `Directory.Build.props` before the `F002` red `dotnet test`
- [X] T030 [US1] Run the filtered Release `F002` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm focus currently mutates without a view-owned veto result
- [X] T031 [US1] Record the `F002` red proof and the Feature-026 validator-integration boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T032 [US1] Add the typed focus transition result and complete bilingual XML contract in `src/TuiVision.Controls/TFocusTransitionResult.cs`
- [X] T033 [US1] Add the additive overridable focus-release decision with didactic historical rationale in `src/TuiVision.Controls/TView.cs`
- [X] T034 [US1] Implement atomic `TrySetFocus` and preserve `SetFocus` compatibility in `src/TuiVision.Controls/TGroup.cs`
- [X] T035 [US1] Increment only the manual build counter in `Directory.Build.props` before the `F002` green `dotnet test`
- [X] T036 [US1] Run the filtered Release `F002` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove accepted/rejected/no-op outcomes plus unchanged rejected state
- [X] T037 [US1] Complete the `F002` Finding row with focus/data/announcement proof and residual InputLine boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

### F003 hierarchy state matrix

- [X] T038 [US1] Add failing `Active`, `Dragging`, `Focused`, `Exposed`, `Disabled`, insert, and removal matrix tests for `F003` in `tests/TuiVision.Controls.Tests/TGroupTests.cs`
- [X] T039 [US1] Increment only the manual build counter in `Directory.Build.props` before the `F003` red `dotnet test`
- [X] T040 [US1] Run the filtered Release `F003` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm uniform propagation violates the accepted matrix
- [X] T041 [US1] Record the `F003` red proof and direct-child responsibility boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T042 [US1] Implement the historical responsibility matrix for propagation and insertion with concise why-comments in `src/TuiVision.Controls/TGroup.cs`
- [X] T043 [US1] Increment only the manual build counter in `Directory.Build.props` before the `F003` green `dotnet test`
- [X] T044 [US1] Run the filtered Release `F003` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove one focused child plus owner-local disabled behavior
- [X] T045 [US1] Complete the `F003` Finding row with hierarchy matrix proof in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: User Story 1 is independently complete without implementing Feature-026 child validation.

---

## Phase 4: User Story 2 - Event Loop Idles Predictably (Priority: P1)

**Goal**: Close `F004` with one pending slot, pending-first ordering, one idle call per empty poll, and a bounded CPU-release seam.

**Independent Test**: The actual `TProgram.Run` loop proves pending, input, idle, repeated idle, shutdown, and no-queue-growth order.

- [X] T046 [US2] Add failing pending-slot, occupied rejection, idle ordering, repeated idle, CPU-release, and shutdown tests for `F004` in `tests/TuiVision.Controls.Tests/TProgramTests.cs`
- [X] T047 [US2] Increment only the manual build counter in `Directory.Build.props` before the `F004` red `dotnet test`
- [X] T048 [US2] Run the filtered Release `F004` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm blocking/busy-loop and missing pending behavior
- [X] T049 [US2] Record the `F004` red proof, one-slot choice, and no-thread/no-timer boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T050 [US2] Implement one pending event slot, pending-first polling, overridable `Idle`, and overridable CPU release with bilingual XML and reason-focused comments in `src/TuiVision.Controls/TProgram.cs`
- [X] T051 [US2] Increment only the manual build counter in `Directory.Build.props` before the `F004` green `dotnet test`
- [X] T052 [US2] Run the filtered Release `F004` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove exact order, bounded occupancy, repeatability, CPU release, and shutdown
- [X] T053 [US2] Complete the `F004` Finding row with actual `Run()` proof and performance boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: User Story 2 is independently complete and the loop no longer relies on helper-only normalized events.

---

## Phase 5: User Story 4 - Command Status and Real Keyboard Agree (Priority: P1)

**Goal**: Close `F007` on top of canonical `F008` ingress with one immutable command truth across active View, menu, StatusLine, and dispatch.

**Independent Test**: Focus/event/idle changes refresh all four surfaces, while manual disablement remains authoritative and dispatch rechecks immediately before execution.

- [X] T054 [US4] Add failing shared command-context, refresh-generation, manual-overlay, stale-dispatch, and editor-focus tests for `F007` in `tests/TuiVision.Controls.Tests/TProgramTests.cs`, `TMenuBarTests.cs`, and `TStatusLineTests.cs`
- [X] T055 [US4] Increment only the manual build counter in `Directory.Build.props` before the `F007` red `dotnet test`
- [X] T056 [US4] Run the filtered Release `F007` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm command truth is split across program/menu/status surfaces
- [X] T057 [US4] Record the `F007` red proof and no-global-registry boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T058 [US4] Add the immutable command snapshot, refresh trigger, and opt-in provider contracts with complete XML docs in `src/TuiVision.Controls/TCommandContext.cs` and `src/TuiVision.Controls/ICommandStateProvider.cs`
- [X] T059 [US4] Preserve manual disablement and add separate context overlays in `src/TuiVision.Controls/TMenuItem.cs`, `TMenuBar.cs`, `TStatusItem.cs`, and `TStatusLine.cs`
- [X] T060 [US4] Refresh and pre-dispatch the shared snapshot from `src/TuiVision.Controls/TProgram.cs` after focus, handled events, and idle
- [X] T061 [US4] Provide editor and edit-window command state through the opt-in contract in `src/TuiVision.Controls/TEditor.cs` and `src/TuiVision.Controls/TEditWindow.cs`
- [X] T062 [US4] Increment only the manual build counter in `Directory.Build.props` before the `F007` green `dotnet test`
- [X] T063 [US4] Run the filtered Release `F007` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove all four surfaces agree after every required trigger
- [X] T064 [US4] Complete the `F007` Finding row and reconcile the already completed `F008` keyboard matrix in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: User Story 4 is independently complete with production keyboard ingress and shared command availability.

---

## Phase 6: User Story 3 - Desktop and Modal Lifecycle Complete Visibly (Priority: P1)

**Goal**: Close `F005` and `F006` with deterministic Desktop stack/geometry plus visible safe close and owner-scoped modal completion.

**Independent Test**: Real application/Desktop loops prove geometry, Z-order, close veto/removal, modal isolation/result/cleanup, restored focus, and rendered cells.

### F005 Desktop stack and geometry

- [X] T065 [US3] Add failing empty/mixed Desktop insertion, top/next, tile, cascade, bounds, focus, and close-all tests for `F005` in `tests/TuiVision.Controls.Tests/TDesktopTests.cs`
- [X] T066 [US3] Increment only the manual build counter in `Directory.Build.props` before the `F005` red `dotnet test`
- [X] T067 [US3] Run the filtered Release `F005` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm the coherent Desktop operations are absent
- [X] T068 [US3] Record the `F005` red proof and application-window-type exclusion in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T069 [US3] Add any minimal owner-local reordering/snapshot primitive required by Desktop with XML docs in `src/TuiVision.Controls/TGroup.cs`
- [X] T070 [US3] Implement focused insertion, top/next selection, bounded tile/cascade, and safe close-all results in `src/TuiVision.Controls/TDesktop.cs`
- [X] T071 [US3] Increment only the manual build counter in `Directory.Build.props` before the `F005` green `dotnet test`
- [X] T072 [US3] Run the filtered Release `F005` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove deterministic bounds/focus/Z-order for empty and mixed participants
- [X] T073 [US3] Complete the `F005` Finding row with Desktop matrix proof in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

### F006 close and modal completion

- [X] T074 [US3] Add failing accepted/vetoed/non-closeable close, Ctrl+W/Escape, modal result/isolation/nesting/cleanup/shutdown, focus restoration, View-tree, and cell tests for `F006` in `tests/TuiVision.Controls.Tests/TWindowTests.cs`, `TDialogTests.cs`, and `TApplicationTests.cs`
- [X] T075 [US3] Increment only the manual build counter in `Directory.Build.props` before the `F006` red `dotnet test`
- [X] T076 [US3] Run the filtered Release `F006` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm close signals without completing lifecycle or owner-scoped modality
- [X] T077 [US3] Record the `F006` red proof and safe-close/non-discard boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T078 [US3] Add close decision/contract types with complete XML documentation in `src/TuiVision.Controls/ICloseableView.cs` and `src/TuiVision.Controls/TCloseResult.cs`
- [X] T079 [US3] Implement accepted/vetoed window lifecycle completion and modal-aware close behavior in `src/TuiVision.Controls/TWindow.cs`
- [X] T080 [US3] Implement owner-scoped modal execution, one direct modal child, temporary insertion, isolation, result, and `finally` cleanup/focus restoration in `src/TuiVision.Controls/TGroup.cs` and `src/TuiVision.Controls/TDialog.cs`
- [X] T081 [US3] Adapt existing framed safe-close hosts to the shared close contract without changing discard policy in `src/TuiVision.Controls/Internal/FramedHostView.cs`
- [X] T082 [US3] Increment only the manual build counter in `Directory.Build.props` before the `F006` green `dotnet test`
- [X] T083 [US3] Run the filtered Release `F006` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove visible removal, veto, result, isolation, cleanup, focus, View-tree, and cells
- [X] T084 [US3] Extend the integrated Desktop/close/modal matrix after implementation in `tests/TuiVision.Controls.Tests/TDesktopTests.cs`, `TWindowTests.cs`, and `TDialogTests.cs` without marking `F006` closed yet
- [X] T085 [US3] Increment only the manual build counter in `Directory.Build.props` before the integrated Desktop/close/modal `dotnet test`
- [X] T086 [US3] Run the filtered Release integrated Desktop/close/modal tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj`
- [X] T087 [US3] Complete the `F006` Finding row with real-loop visible proof and Feature-026 dialog-child-validation boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: User Story 3 is independently complete with no application-specific window registry.

---

## Phase 7: User Story 5 - Drag Works with Pointer and Keyboard (Priority: P2)

**Goal**: Close `F009` with one bounded generic drag session consumed by title dragging and Ctrl+F5.

**Independent Test**: The actual app loop uses the same state for pointer and keyboard, proves threshold/capture/bounds/target/drop/cancel/lifecycle loss, and renders the moved result.

- [X] T088 [US5] Add failing pointer/keyboard threshold, one-capture, bounds, target decision, Enter/drop, Escape, owner/capability/shutdown cancellation, and cell tests for `F009` in `tests/TuiVision.Controls.Tests/TWindowMouseDragTests.cs`
- [X] T089 [US5] Increment only the manual build counter in `Directory.Build.props` before the `F009` red `dotnet test`
- [X] T090 [US5] Run the filtered Release `F009` red tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and confirm title drag lacks the accepted generic shared contract
- [X] T091 [US5] Record the `F009` red proof, one-cell threshold, and no-full-desktop-protocol boundary in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T092 [US5] Add generic drag state/result/session/target contracts with complete XML docs in `src/TuiVision.Controls/TDragSession.cs`, `TDragResult.cs`, and `IDragTarget.cs`
- [X] T093 [US5] Implement one-capture pointer/keyboard transitions, bounds, target negotiation, terminal results, and unconditional capture release in `src/TuiVision.Controls/TDragSession.cs`
- [X] T094 [US5] Route window title drag and Ctrl+F5 through the common session without expanding other drag targets in `src/TuiVision.Controls/TWindow.cs`
- [X] T095 [US5] Increment only the manual build counter in `Directory.Build.props` before the `F009` green `dotnet test`
- [X] T096 [US5] Run the filtered Release `F009` tests in `tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj` and prove equivalent pointer/keyboard outcomes plus lifecycle cancellation
- [X] T097 [US5] Complete the `F009` Finding row with A11Y, focus, View-tree, cell, and platform proof in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: User Story 5 is independently complete and remains bounded to one reusable character-cell drag contract.

---

## Phase 8: User Story 6 - Findings Close Traceably (Priority: P2)

**Goal**: Preserve the Feature-024 baseline while appending proven Feature-025 closure metadata for all nine findings.

**Independent Test**: Machine checks reconcile one accepted finding, contract, red proof, implementation decision, real proof, and resolution for each `F001` through `F009`.

- [X] T098 [US6] Add failing resolution-schema, exact-nine closure, proof-field, and readable-gate tests in `tests/TuiVision.Drivers.Tests/ConformanceAuditEvidenceTests.cs`
- [X] T099 [US6] Increment only the manual build counter in `Directory.Build.props` before the Feature-024 resolution red `dotnet test`
- [X] T100 [US6] Run the filtered Release resolution tests in `tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj` and confirm Feature-024 has no proven Feature-025 resolution metadata yet
- [X] T101 [US6] Record the resolution validator red boundary and immutable-audit rule in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T102 [US6] Append machine-checkable `F001`-`F009` resolution metadata without rewriting original observations in `specs/024-tv203-freevision-conformance-audit/conformance-audit.json`
- [X] T103 [US6] Add readable Feature-025 closure state while keeping `F010`-`F013`, Feature 026, Feature 028, Wave 5, and Wave 6 gates intact in `specs/024-tv203-freevision-conformance-audit/findings.md` and `pre-wave5-gate.md`
- [X] T104 [US6] Update Feature-024 source/public inventory entries and reciprocal links only for actual new public/source files in `specs/024-tv203-freevision-conformance-audit/conformance-audit.json`
- [X] T105 [US6] Increment only the manual build counter in `Directory.Build.props` before the Feature-024 resolution green `dotnet test`
- [X] T106 [US6] Run the filtered Release resolution tests in `tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj` and prove exact closure/inventory/relationship consistency
- [X] T107 [US6] Reconcile all nine Finding rows with the resolution dataset and record zero documentation-only closures in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

**Checkpoint**: User Story 6 is independently complete and the audit preserves both historical findings and proven closure.

---

## Phase 9: Documentation, Governance, and Repository Context

**Purpose**: Publish the additive contracts accessibly and advance maintained project markers without releasing Wave 5 or Wave 6.

- [X] T108 Add the DE-first/EN-second CEFR-B2 learner guide, historical intent, modern deviations, real-path examples, keyboard paths, and text-first proof in `docs/guides/core-runtime-conformance-hardening.md`
- [X] T109 Add the guide to the existing DocFX navigation in `docs/toc.yml`
- [X] T110 Review every new/changed public member for complete `summary`, `param`, `returns`, and `exception` XML plus non-trivial didactic-comment value across `src/TuiVision.Core/` and `src/TuiVision.Controls/`
- [X] T111 Record NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC, iSAQB quality/risk, A11Y, cross-platform input, agent parity, and autonomous governance as `Applicable` with evidence in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T112 Record ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, DORA, Zero Trust, SAMM, BSI C3A/C5, and script parity as trigger-based `N/A` unless actual scope changed in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T113 Record S-ADR, arc42, and shared security-document applicability from the final API/dependency graph; create only materially triggered records under `docs/security/` and otherwise justify `N/A` in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T114 Synchronize completed 025 status and next 026 intake across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [X] T115 Update `Pflichtenheft.md` markers so Feature 026 is next while Wave 5 and Wave 6 remain blocked through Feature 028
- [X] T116 Update `docs/project-statistics.md` with the Feature-025 delta and preserve the final `## Gesamtstatistik` text-first block
- [X] T117 Compare Bash `--dry-run` with PowerShell `-WhatIf`, reject any error-channel/fatal-signature mismatch, then archive `Lastenheft_10_Core-Runtime-Conformance-Hardening.md` exactly once via Bash `--no-commit` for branch `025-core-runtime-conformance-hardening`
- [X] T118 Record archive outputs, script parity result, next-intake markers, and agent parity in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

---

## Phase 10: Final Local Validation

**Purpose**: Prove formatting, targeted runtime behavior, full regression safety, canonical coverage, docs/A11Y, platform evidence, and scope integrity.

- [X] T119 Run `git diff --check`, `dotnet format --verify-no-changes --no-restore`, Markdown fence/structure/UTF-8 checks, and record results in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T120 Increment only the manual build counter in `Directory.Build.props` before final targeted Core `dotnet test`
- [X] T121 Run `dotnet test tests/TuiVision.Core.Tests/TuiVision.Core.Tests.csproj --configuration Release` and record the result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T122 Increment only the manual build counter in `Directory.Build.props` before final targeted Compatibility `dotnet test`
- [X] T123 Run `dotnet test tests/TuiVision.Compatibility.Tests/TuiVision.Compatibility.Tests.csproj --configuration Release` and record the result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T124 Increment only the manual build counter in `Directory.Build.props` before final targeted Controls `dotnet test`
- [X] T125 Run `dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release` and record the result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T126 Increment only the manual build counter in `Directory.Build.props` before final targeted Drivers `dotnet test`
- [X] T127 Run `dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release` and record the result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T128 Increment only the manual build counter in `Directory.Build.props` before the full Release `dotnet test`
- [X] T129 Run the full repository `dotnet test --configuration Release` and record totals/failures in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T130 Validate `coverlet.runsettings` using `xmllint --noout coverlet.runsettings` and record the result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T131 Increment only the manual build counter in `Directory.Build.props` before the canonical Coverlet `dotnet test`
- [X] T132 Run `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`, calculate all five assembly percentages, and record the >=70 percent gate in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T133 Run `docfx docfx.json`, require zero warnings/errors, and record the public XML/guide result in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T134 Run `npm ci` and ensure Chromium availability under `tests/web-a11y/`; record dependency/install scope without tracking output in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T135 Run `npm run test:docfx` under `tests/web-a11y/` and record Playwright/Axe results in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T136 Review representative changed generated pages through UTF-8 Lynx/text output and record semantic reading order in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T137 Record Windows/WSL keyboard/modifier expectations and rely on final CI only for unavailable local platform proof in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T138 Verify no new package, generated `_site/`/`api/*.yml`, test output, cache, credential, Wave app, example, `TVDEMOS/`, `TVFM/`, `tv203s/`, or external Free Vision change exists; record scans in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [X] T139 Verify T001-T138, all nine Finding rows, governance rows, API/A11Y/platform decisions, scope gates, and quickstart steps are complete in `specs/025-core-runtime-conformance-hardening/tasks.md` and `pr-evidence.md`
- [X] T140 Prepare a concise PR description from `specs/025-core-runtime-conformance-hardening/pr-evidence.md` without embedding recursive post-merge facts

---

## Phase 11: Commit, PR, Review Convergence, Merge, and Sync

**Purpose**: Deliver the explicitly authorized `MergeAndSync` closeout while preserving reviewed-head integrity.

- [X] T141 Align `Version`, `AssemblyVersion`, and `FileVersion` to `1.25.<new-commit-count>.<current-build>` before the planning/implementation commit in `Directory.Build.props`
- [ ] T142 Commit the complete validated Feature-025 change with the repository co-author trailer, verify the commit externally, and preserve the non-recursive boundary already declared in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [ ] T143 Push the already version-aligned `025-core-runtime-conformance-hardening` commit and verify remote head parity without a post-commit version edit; use `specs/025-core-runtime-conformance-hardening/pr-evidence.md` as the declared closeout evidence path
- [ ] T144 Create a ready Feature-025 PR from the prepared description and verify base `main`, branch, labels, and no-empty diff; use `specs/025-core-runtime-conformance-hardening/pr-evidence.md` as the declared closeout evidence path
- [ ] T145 Identify pull-request-context required checks, record push-trigger duplicates as noise, and do not cancel runs without a safe concurrency contract; use `specs/025-core-runtime-conformance-hardening/pr-evidence.md` as the declared closeout evidence path
- [ ] T146 Wait for required PR-context checks and verify Linux/macOS plus Windows/WSL evidence are green on the final reviewed head; use `specs/025-core-runtime-conformance-hardening/pr-evidence.md` as the declared closeout evidence path
- [ ] T147 Query Copilot, Claude, and GraphQL review threads; address every actionable finding and repeat validation/review until zero actionable threads remain against `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [ ] T148 Record unavailable/quota-limited reviewers against `specs/025-core-runtime-conformance-hardening/pr-evidence.md` and use the approved narrow admin bypass only if green required checks plus zero actionable threads leave human approval as the sole rule
- [ ] T149 Merge the Feature-025 PR using a merge commit, delete the remote feature branch, and verify terminal facts externally from the boundary declared in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- [ ] T150 Switch locally to `main`, fetch/prune, fast-forward pull, and prove clean `HEAD == origin/main` externally from the boundary declared in `specs/025-core-runtime-conformance-hardening/pr-evidence.md`

Terminal PR URL, final reviewed-head result, merge commit, and synchronized-main facts are verified externally after the last repository commit so the evidence file does not invalidate its own reviewed head.

After T150 and the retrospective, exactly one causal evidence-closeout PR may
mark T142-T154 complete in the tracked task list. That closeout records the
Feature-025 PR facts, but never requires its own PR URL, reviewed-head result,
or merge commit in the same file; its own terminal facts remain external.

---

## Phase 12: Autonomous Retrospective

**Purpose**: Classify reusable learning after delivery without creating an empty PR or changing Feature-025 scope.

- [ ] T151 Evaluate task shape, red-proof completeness, validator triggers, evidence quality, CI duplication, build-counter use, review convergence, resume behavior, and remote closeout against `.agents/skills/speckit-autonomous-retrospective/SKILL.md`
- [ ] T152 Classify each observation in `docs/spec-kit-autonomous-retrospectives.md` as `FeatureSpecific`, `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `ValidationAutomation`, `PresetFollowUp`, or `NoPromotion`
- [ ] T153 Create a TuiVision retrospective branch/PR only for a concrete non-empty correction recorded in `docs/spec-kit-autonomous-retrospectives.md`; otherwise record `NoPromotion` there and keep clean synchronized `main`
- [ ] T154 Hand off any evidence-backed portable insight to `~/home-baseline-tmp/specs/autonomous-run-governance/workitems/025-core-runtime-conformance-hardening.md` only when it satisfies the promotion threshold; do not delay Feature-025 completion for speculative upstream work

---

## Dependencies and Execution Order

- Setup T001-T009 blocks every source/test edit.
- Foundational execution is strict: `F001` (T010-T017), `F008` (T018-T027).
- Runtime findings then follow: `F002` (T028-T037), `F003` (T038-T045), `F004` (T046-T053), `F007` (T054-T064), `F005` (T065-T073), `F006` (T074-T087), `F009` (T088-T097).
- Feature-024 resolution T098-T107 starts only after all nine real-path proofs pass.
- Documentation/governance T108-T118 starts after runtime and resolution closure.
- Final validation T119-T140 is serial because version, evidence, generated output, and shared test projects are single-writer surfaces.
- Remote delivery T141-T150 starts only after every local acceptance gate passes.
- Retrospective T151-T154 starts only after clean synchronized `main` is proven.

## Parallel Opportunities

No implementation tasks are marked `[P]`. The findings intentionally share event, Group, Program, Desktop, evidence, version, audit, and test files; sequential execution is the safe and reviewable strategy for this feature.

## Implementation Strategy

1. Establish evidence and validators before the first test edit.
2. Complete `F001` as the narrow vertical reference slice.
3. Repair real keyboard ingress before relying on keyboard acceptance elsewhere.
4. Close each remaining finding red-first in the accepted dependency order and finish its evidence row immediately.
5. Update Feature-024 resolution only after the actual runtime proofs pass.
6. Complete public documentation, full validation, reviewed-head convergence, merge, main synchronization, and then retrospective classification.
