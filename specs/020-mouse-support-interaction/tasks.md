# Tasks: Mouse Support and Interaction Hardening

**Input**: All accepted artifacts under `specs/020-mouse-support-interaction/`
**Delivery mode**: `MergeAndSync`
**Acceptance ledger**: `specs/020-mouse-support-interaction/pr-evidence.md`

Tasks are sequential because most slices share Driver, Controls, evidence,
version, documentation, statistics, agent, or delivery files. No `[P]` markers
are used where ownership could overlap.

## Phase 1: Preflight and Evidence Foundation

- [X] T001 Create `specs/020-mouse-support-interaction/pr-evidence.md` from the autonomous evidence template before any later task records or runtime edits
- [X] T002 Verify branch `020-mouse-support-interaction`, clean ancestry from synchronized `main`, and `.specify/feature.json`; record in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T003 Run `specify check` and the PowerShell prerequisites check with tasks included; record exact results in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T004 Verify all 020 checklists have zero incomplete items and record counts in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T005 Read `AGENTS.md`, Constitution, binding Lastenheft, and every 020 artifact; record material conflicts or `None` in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T006 Verify the six installed preset names, versions, and priorities; record them in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T007 Add ingress-observation, interaction, host, framework-decision, and comment-decision matrices to `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T008 Add governance rows with complete owner, reviewer, date, result, residual-risk, follow-up, and trigger fields to `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T009 Add requirements/SC coverage, validation, generated-output hygiene, remote delivery, and retrospective tables to `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T010 Record `speckit-constitution` as unchanged and `speckit-taskstoissues` as `N/A` because no constitutional conflict exists and one dependency-ordered PR is the delivery unit
- [X] T011 Record Specify, four accepted Clarify decisions, second-pass convergence, checklist convergence, Plan, Plan Review, Tasks, and pending Analyze gates in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T012 Verify no placeholders or clarification markers remain outside normative checklist text and record the scan in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T013 Record hard scope boundaries and the explicit native-Windows/wheel/hover/touch/protocol follow-up boundary in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T014 Record the `MergeAndSync` authority source and narrow admin-bypass rule in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T015 Prove `git diff -- tv203s/` is empty before implementation and record the boundary in `specs/020-mouse-support-interaction/pr-evidence.md`

## Phase 2: Compile Surface, Historical Intent, and Architecture Gate

- [X] T016 Review project references for Driver -> Core and Controls -> Driver and record absence of dependency cycles in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T017 Review imports and planned public XML documentation for new Driver/Core/Controls APIs before the first red test
- [X] T018 Review test-harness helpers, app-loop injection points, focus/owner assertions, and linked-source assembly identity before the first red test
- [X] T019 Review `tv203s/contrib/tvision/classes/tevent.cc` and `tmouse.cc` plus `include/tv/event.h` read-only; record retained event/double-click intent
- [X] T020 Review `tv203s/contrib/tvision/classes/unix/xtermmouse.cc` plus `include/tv/unix/xtmouse.h` and `mouse.h` read-only; record retained host intent and protocol deviation
- [X] T021 Review `tv203s/contrib/tvision/classes/tview.cc`, `twindow.cc` plus `include/tv/view.h` and `window.h` read-only; record hit/focus/drag intent
- [X] T022 Classify ingress, focus, activation, double-click, title drag, fallback, and host support with exactly one framework decision each in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T023 Record the component ownership gate: Driver protocol/state, Core canonical event, Controls interaction, no example-local reusable logic
- [X] T024 Record compile-surface findings and any required pre-red corrections in `specs/020-mouse-support-interaction/pr-evidence.md`
- [X] T025 Re-prove `tv203s/` remains unchanged after historical review

## Phase 3: User Story 1 - Failing Driver Vertical Slice

- [X] T026 Add `tests/TuiVision.Drivers.Tests/ConsoleMouseIngressTests.cs` with failing complete SGR left press/move/release mapping tests
- [X] T027 Extend the Driver matrix with failing one-based-to-zero-based coordinate and current-buffer boundary tests
- [X] T028 Extend the Driver matrix with explicit malformed, truncated, oversized, non-numeric, unknown-button, wheel, and trailing-input rejection cases
- [X] T029 Extend the Driver matrix with invalid move-before-press, duplicate press, release-without-press, capability-disabled, and capability-unsupported cases
- [X] T030 Add failing stream-recovery proof showing a rejected observation does not consume the next independent valid observation
- [X] T031 Add failing double-click proof for same left button, exact cell, exact target, and `<= 500 ms`
- [X] T032 Add failing non-double proof for `501 ms`, different cell, different target, clock regression, and capability reset
- [X] T033 Increment the manual build counter, run the focused Driver matrix expecting the documented red compile/contract boundary, and record version/failures
- [X] T034 Verify every grouped red case has an explicit expected failure and local file ownership; record the red matrix result

## Phase 4: User Story 1 - Driver Ingress Implementation

- [X] T035 Add fully XML-documented capability, host-family, protocol, and rejection enums/records in `src/TuiVision.Drivers.Console/ConsoleMouseIngress.cs`
- [X] T036 Implement bounded complete SGR-1006 framing and numeric parsing in `ConsoleMouseIngress.cs`
- [X] T037 Implement syntax, size, coordinate, button, capability, and phase validation before publication
- [X] T038 Implement zero-or-one mapping to existing `TEvent` with no parallel UI event abstraction
- [X] T039 Implement pressed-button and position state with atomic reset on capability loss or shutdown
- [X] T040 Implement injected monotonic double-click classification with the exact 500 ms/cell/target contract using a point-to-target-key delegate and no Driver-to-Controls reference
- [X] T041 Add controlled observation queue and ingress ownership to `src/TuiVision.Drivers.Console/TConsoleDriver.cs`
- [X] T042 Implement supported/disabled/unsupported host classification for interactive macOS/Linux, WSL, native Windows Console, and headless input
- [X] T043 Update `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs` to describe the delivered managed mouse ingress and honest unsupported boundaries
- [X] T044 Review Driver changes for didactic comment value and add only concise why/trade-off/proof-boundary comments
- [X] T045 Increment the manual build counter, run the focused Driver matrix to green, and record exact count/version/result

## Phase 5: User Story 2 - Focus, Activation, and Coordinates

- [X] T046 Add failing recursive global/local coordinate tests for nested owner trees in `tests/TuiVision.Controls.Tests/TViewMouseInteractionTests.cs`
- [X] T047 Add failing `TGroup` tests for topmost visible hit selection and focus transfer before handling
- [X] T048 Add failing negative focus tests for covered, hidden, disabled, non-selectable, and outside targets
- [X] T049 Add failing exactly-once activation tests for a focused button through group dispatch
- [X] T050 Add failing no-activation tests for rejected/duplicate/outside observations
- [X] T051 Increment the manual build counter, run focused Controls mouse tests expecting the documented red boundary, and record it
- [X] T052 Update `TView.MakeGlobal` and `TView.MakeLocal` to traverse the owner chain while preserving root behavior
- [X] T053 Update `TGroup.HandleEvent` to identify one topmost visible hit target for mouse events
- [X] T054 Transfer focus through `TGroup.SetFocus` only for eligible selectable mouse-down targets before normal handling
- [X] T055 Ensure one target receives the mouse event and existing control activation remains the only command path
- [X] T056 Preserve keyboard/command pre-process, focused, and post-process behavior unchanged
- [X] T057 Review coordinate/focus/dispatch changes for didactic comments and historical deviations
- [X] T058 Increment the manual build counter, run focused Core/Controls interaction tests to green, and record result
- [X] T059 Run existing button, dialog, list, group, and program mouse/focus regression tests in the same targeted Release batch where possible
- [X] T060 Record `SmallFrameworkFix` evidence for coordinate and focus routing with red/green test names
- [X] T061 Confirm no example file contains a raw parser or competing mouse abstraction and record the scan

## Phase 6: User Story 3 - Single Window Title Drag

- [X] T062 Add failing `TWindow` tests for left press on a movable title row, multiple moves, and release commit
- [X] T063 Add failing owner-boundary clamp tests on all four desktop edges
- [X] T064 Add failing non-drag tests for body press, non-movable window, right/unknown button, and move without press
- [X] T065 Add failing cancellation tests for Escape, capability loss, disabled target, removal, and shutdown
- [X] T066 Add failing keyboard regression proof for `Ctrl+F5`, arrows, Enter commit, and Escape restore
- [X] T067 Increment the manual build counter, run focused window tests expecting the documented red boundary, and record it
- [X] T068 Add bounded title-drag session state to `src/TuiVision.Controls/TWindow.cs`
- [X] T069 Start drag only for valid left press on the top title row of a movable, visible, owned window
- [X] T070 Apply pointer delta and clamp the complete window bounds inside the owner extent
- [X] T071 Commit on valid release and clear all transient drag state
- [X] T072 Cancel and clear drag on Escape, capability loss, disable/removal, and shutdown without a hanging state
- [X] T073 Preserve existing keyboard move-mode behavior as the complete fallback
- [X] T074 Review drag logic for concise historical/trade-off/cancellation comments
- [X] T075 Increment the manual build counter, run focused window and keyboard regression tests to green, and record result

## Phase 7: User Stories 1, 2, and 4 - Runtime and App-Loop Proof

- [X] T076 Add failing `TProgram` tests for controlled Driver observations reaching the real `GetEvent` and `HandleEvent` route
- [X] T077 Add failing runtime lifecycle tests for supported enable, disabled, unsupported, and cleanup/reset states
- [X] T078 Add a bounded app-loop harness with two focusable controls, one movable window, status text, and rendered-cell capture
- [X] T079 Add failing primary proof for click focus and exactly-once activation through `app.Run()`
- [X] T080 Add failing primary proof for qualifying/non-qualifying double click through `app.Run()`
- [X] T081 Add failing primary proof for title drag, clamped bounds, release, and cancellation through `app.Run()`
- [X] T082 Add failing primary proof that keyboard focus, activation, and move remain complete when mouse is disabled/unsupported
- [X] T083 Implement `TProgram` SGR enable/read/disable lifecycle, point-to-target-key resolution, and queue-before-keyboard event retrieval without changing existing key semantics
- [X] T084 Implement visible bilingual capability/focus/activation/double-click/drag/fallback status in the integration harness
- [X] T085 Increment the manual build counter, run the complete app-loop matrix to green, and record state/view/status/cell proof plus limits

## Phase 8: Host, Security, Documentation, and Routing

- [X] T086 Complete macOS, Linux, WSL, native Windows Console, and headless host rows with evidence class, result, risk, and re-evaluation trigger
- [X] T087 Run any locally available physical macOS terminal spot-check without overstating non-executed Linux/WSL/Windows evidence
- [X] T088 Complete parser/interaction comment-decision rows and confirm no trivial what-comments were added
- [X] T089 Populate NIST SSDF, CWE Top 25, secure-input, fail-safe, and STRIDE/CIA/CAPEC governance rows
- [X] T090 Populate trigger-based `N/A` rows for ASVS, new SBOM/VEX/SLSA/OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, and DORA
- [X] T091 Populate iSAQB/component-boundary rows and trigger-based `N/A` for S-ADR, arc42 changes, Zero Trust, SAMM, BSI C3A, and BSI C5
- [X] T092 Populate A11Y, didactic-comment, cross-platform host, script-governance `N/A`, agent-parity, and `.specify/templates/` `N/A` rows
- [X] T093 Review existing security and architecture evidence and update only triggered host-input/threat rows; record unchanged rationale elsewhere
- [X] T094 Create `docs/guides/mouse-support.md` with DE-first/EN-second protocol, hosts, capability, interactions, keyboard fallback, security, A11Y, and proof boundaries
- [X] T095 Add the mouse-support guide to `docs/toc.yml` and update relevant README/index links
- [X] T096 Review changed Markdown for CEFR-B2, umlauts/ß, semantic structure, fenced-language tags, and text-first accessibility
- [X] T097 Update active Feature-020 and next Feature-021 context in all five maintained agent files
- [X] T098 Verify the five agent context blocks are synchronized and record hashes/results
- [X] T099 Update `Pflichtenheft.md` completion and next-intake marker to `Lastenheft_05_TerminalCharsetAndEmulation.md`
- [X] T100 Update `docs/project-statistics.md` with 020 scope, lines, work window, 80/125-line baselines, validation, and next intake

## Phase 9: Validation, Archive, and Local Completion

- [X] T101 Run `git diff --check`, placeholder/TODO, scope, local-parser, generated-output, and `tv203s/` scans; record results
- [X] T102 Run `dotnet format --verify-no-changes --no-restore` and record result
- [X] T103 Increment the manual build counter and run all targeted Driver/Core/Controls Release tests; record per-project counts and version
- [X] T104 Increment the manual build counter and run the full Release suite; record per-project and total counts
- [X] T105 Validate `coverlet.runsettings`, increment the manual build counter, run canonical coverage, and record all five required assembly percentages
- [X] T106 Run `docfx docfx.json`, then Playwright/axe DocFX smoke; record warnings/errors/tests
- [X] T107 Review the generated mouse guide through UTF-8 `lynx` when available and record the text-first result
- [X] T108 Run repository diff/tracked-secret scans and record results
- [X] T109 Remove generated DocFX/API/test artifacts and prove they are absent from Git
- [X] T110 Verify every FR, CR, and SC has an exact evidence link and every framework/host/governance row is complete
- [X] T111 Verify exactly one drag target, no executable out-of-scope change, and no remaining local blocker
- [X] T112 Archive `Lastenheft_04_MouseSupportAndInteraction.md` through the PowerShell rename workflow with suffix `020-mouse-support-interaction`
- [X] T113 Record final local task count, changed files, validation, conditional gates, follow-ups, and retrospective observations
- [X] T114 Re-run `specify check`, prerequisite/task checks, checklist counts, and final Analyze consistency after implementation evidence updates
- [X] T115 Mark all local tasks through T115 complete only after their acceptance results are present in `pr-evidence.md`

## Phase 10: Authorized GitHub Delivery

- [X] T116 Align `Directory.Build.props` to `1.20.<branch-commit-count>.<build>` without incrementing build, stage intentional files, and record scope
- [X] T117 Commit the complete 020 implementation and capture commit/version in `pr-evidence.md`
- [X] T118 Recalculate branch commit count, align version/evidence, and create a bounded follow-up commit only if required
- [X] T119 Push `020-mouse-support-interaction`, record observed branch/commit, align and commit truthful evidence if needed, and push again
- [X] T120 Create a ready feature PR from `pr-evidence.md`, record its URL, and push the bounded PR-reference evidence update
- [X] T121 Monitor required CI, Claude/Copilot availability, review comments, and GraphQL threads to convergence; record each state
- [X] T122 Remediate every actionable remote finding with focused validation and record response/thread resolution
- [ ] T123 Use the authorized narrow admin bypass only after green required checks, zero actionable threads, and a sole human-approval block; record exact boundary
- [ ] T124 Merge with a merge commit, delete the remote feature branch, switch to local `main`, fetch/prune/pull fast-forward, and prove clean `HEAD == origin/main`
- [ ] T125 Record post-merge facts through a non-empty evidence-only closeout PR only when causally impossible before merge; otherwise document why none is needed
- [ ] T126 Finish with synchronized clean `main` and a complete Feature-020 evidence record, ready for the separate retrospective decision

## Dependencies and Execution Order

- T001-T025 gate all implementation.
- T026-T034 establish the Driver red slice; T035-T045 must pass before Controls work.
- T046-T061 gate T062-T075; both gate app-loop tasks T076-T085.
- Host/docs/governance T086-T100 precede validation/archive T101-T115.
- Delivery T116-T126 starts only after every local acceptance gate passes.
- `pr-evidence.md`, `Directory.Build.props`, `TProgram.cs`, `TGroup.cs`,
  `TWindow.cs`, documentation, statistics, and agent files remain single-writer.
- No delivery task is accepted without its exact 020 evidence entry.

## Requirement Coverage

| Requirement | Task coverage |
|---|---|
| FR-001 to FR-007 | T016-T045, T076-T087 |
| FR-008 to FR-010 | T046-T061, T076-T085 |
| FR-011 to FR-013 | T062-T075, T081-T085 |
| FR-014 to FR-017 | T073, T078-T085, T094-T096 |
| FR-018 to FR-020 | T026-T085, T061, T086-T087 |
| FR-021 to FR-023 | T019-T025, T044, T057, T074, T088 |
| FR-024 to FR-026 | T094-T100, T112-T113 |
| CR-001 to CR-003 | T004-T005, T016-T024, T089, T101-T105 |
| CR-004 to CR-008 | T089-T093 |
| CR-009 to CR-012 | T086-T100, T106-T107 |
| CR-013 to CR-015 | T017-T018, T024, T026-T034, T116-T126 |
| SC-001 to SC-004 | T026-T075, T079-T085 |
| SC-005 to SC-006 | T078-T085, T094-T096 |
| SC-007 to SC-009 | T061, T086-T087, T101-T105 |
| SC-010 to SC-012 | T022, T088-T093, T110-T113 |
| SC-013 | T097-T100, T112-T126 |
