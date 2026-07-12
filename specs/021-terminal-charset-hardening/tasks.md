# Tasks: Terminal and Charset Hardening

**Input**: All accepted artifacts under `specs/021-terminal-charset-hardening/`
**Delivery mode**: `MergeAndSync`
**Acceptance ledger**: `specs/021-terminal-charset-hardening/pr-evidence.md`

Tasks are sequential because most slices share Driver, Controls, evidence,
version, documentation, statistics, agent, or delivery files. No `[P]` markers
are used where ownership could overlap.

## Phase 1: Preflight and Evidence Foundation

- [X] T001 Create `specs/021-terminal-charset-hardening/pr-evidence.md` from `.specify/templates/autonomous-run-evidence-template.md` before any runtime or test edit
- [X] T002 Verify branch `021-terminal-charset-hardening`, ancestry from synchronized `main`, and `.specify/feature.json`; record exact state in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T003 Run `specify check` and the PowerShell prerequisite check with tasks included; record results in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T004 Verify every 021 checklist has zero incomplete items and record counts in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T005 Read `AGENTS.md`, Constitution, binding Lastenheft, and every 021 artifact; record material conflicts or `None` in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T006 Verify the six installed preset names, versions, and priorities; record them in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T007 Add session/emulation, charset, font, profile, Controls, host, framework-decision, and comment-decision matrices to `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T008 Add governance rows with owner, reviewer, date, result, residual risk, follow-up, and re-evaluation trigger to `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T009 Add requirement/SC coverage, validation, generated-output hygiene, remote delivery, causal closeout, and retrospective tables to `specs/021-terminal-charset-hardening/pr-evidence.md`; pre-name `specs/021-terminal-charset-hardening/closeout-evidence.md` for self-invalidating reviewed-head and post-merge facts
- [X] T010 Record `speckit-constitution` as unchanged and `speckit-taskstoissues` as `N/A` because one dependency-ordered feature PR is the delivery unit
- [X] T011 Record Specify, ten accepted Clarify decisions, clarification convergence, checklist convergence, Plan, Plan Review, Tasks, and pending Analyze gates in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T012 Scan 021 artifacts for placeholders, temporary requirement IDs, or unresolved markers and record the clean result in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T013 Record hard scope boundaries and the explicit full-emulation/host-process/host-mutation/Wave-4 follow-up boundary in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T014 Record `MergeAndSync` authority, missing-review semantics, causal closeout rule, and narrow admin-bypass policy in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T015 Prove `git diff -- tv203s/` and `git diff -- examples/` contain no implementation changes before runtime work and record both boundaries

## Phase 2: Compile Surface, Historical Intent, and Architecture Gate

- [X] T016 Review project references for Drivers.Console -> Core, Controls -> Drivers.Console/Core, and Compatibility -> Core; record absence of dependency cycles
- [X] T017 Review planned public types, namespace imports, complete XML documentation, nullability, lifecycle ownership, and enum/result naming before the first red command
- [X] T018 Review `TConsoleCell`, `TConsoleBuffer`, Driver resize/presentation, Controls draw-buffer APIs, and test harness helpers; record reusable boundaries
- [X] T019 Review app-loop injection, view identity, status, cursor, buffer/cell assertions, and cross-assembly type identity before the first red command
- [X] T020 Review `tv203s/contrib/tvision/examples/terminal/terminal.cc` and `include/tv/terminal.h` read-only; record retained terminal intent and host-process deviations
- [X] T021 Review both Cyrillic `test.cc` files, Linux KOI8 setup scripts/readme, and related fixtures read-only; record retained mapping intent and prohibited host changes
- [X] T022 Review fonts `test.cc`, `genraw.cc`, `font.016`, `ocr.sft`, `classes/fontcoll.cc`, and `include/tv/fontcoll.h` read-only; record retained fixture intent and generator boundary
- [X] T023 Review Eterm configuration/docs plus XTerm resources and relevant Unix xterm display/key/screen sources read-only; record profile/input intent and deliberate subset limits
- [X] T024 Classify session/emulation, buffer/cells, charset, font, profile, and Controls projection with exactly one framework decision each in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T025 Record the ownership gate: Driver session/parser/mapping/font/profile, Core cells/buffer, Controls projection, unchanged Compatibility key boundary, no example-local logic
- [X] T026 Record compile-surface findings and any required pre-red correction in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T027 Record exact historical source inventory, retained purposes, intentional deviations, and non-executed setup/generator boundaries
- [X] T028 Re-prove `tv203s/` and `examples/` remain unchanged after historical and architecture review

## Phase 3: User Story 1 - Failing Session Vertical Slice

- [X] T029 [US1] Add `tests/TuiVision.Drivers.Tests/TerminalSessionTests.cs` with failing construction, plain-text, cursor, and visible-cell assertions
- [X] T030 [US1] Add failing wrap, single-cell, single-row, and deterministic clipping cases in `tests/TuiVision.Drivers.Tests/TerminalSessionTests.cs`
- [X] T031 [US1] Add one failing accepted CSI relative-cursor case and exact before/after state assertion
- [X] T032 [US1] Add one failing malformed CSI rejection case proving no cell, cursor, or attribute partial action
- [X] T033 [US1] Add failing recovery proof showing the next independent valid text remains usable after rejection
- [X] T034 [US1] Add failing close, dispose, repeated dispose, reset, and input-after-close lifecycle cases
- [X] T035 [US1] Add failing resize proof for top-left intersection, empty new cells, and cursor clamping
- [X] T036 [US1] Add failing scroll proof for visible-row shift and FIFO history append
- [X] T037 [US1] Add failing 4,095/4,096/4,097 history-cell boundary cases
- [X] T038 [US1] Review the grouped Driver red matrix so every expected failure and ownership boundary remains explicit
- [X] T039 [US1] Increment the manual build counter, run the focused Driver matrix expecting the documented red compile/contract boundary, and record version/failures
- [X] T040 [US1] Record red test names, state expectations, and proof limits in `specs/021-terminal-charset-hardening/pr-evidence.md`
- [X] T041 [US1] Recheck public XML documentation and imports exposed by the red compile surface before implementation

## Phase 4: User Story 1 - Session and Lifecycle Implementation

- [X] T042 [US1] Add fully XML-documented session dimensions, cursor, lifecycle, capability, status, attribute, and result types in `src/TuiVision.Drivers.Console/TerminalSession.cs`
- [X] T043 [US1] Implement positive-dimension construction and a Driver-owned `TConsoleBuffer` visible state in `src/TuiVision.Drivers.Console/TerminalSession.cs`
- [X] T044 [US1] Implement plain-text output with deterministic cursor advance, clipping, wrap, and scroll
- [X] T045 [US1] Implement a 4,096-cell FIFO history with exact oldest-cell eviction
- [X] T046 [US1] Implement resize preserving the top-left intersection, empty new cells, and clamped cursor
- [X] T047 [US1] Implement active, closed, disposed, and capability-lost lifecycle transitions with idempotent cleanup
- [X] T048 [US1] Implement full reset clearing visible cells, history, cursor, attributes, parser, notice, and fallback state
- [X] T049 [US1] Expose immutable/snapshot-oriented visible buffer, cursor, history, status, and lifecycle observations
- [X] T050 [US1] Implement no-op-safe repeated close/reset/dispose semantics without stale input state
- [X] T051 [US1] Review session and lifecycle logic for concise DE-first/EN-second why/trade-off/proof-boundary comments
- [X] T052 [US1] Increment the manual build counter, run focused session/lifecycle tests to green, and record exact count/version/result
- [X] T053 [US1] Record `SmallFrameworkFix` for session and `UseExistingFramework` for cells/buffer with red/green evidence paths
- [X] T054 [US1] Confirm no host process, shell, PTY, audio, font, codepage, or terminal setting is accessed by the session

## Phase 5: User Story 2 - Bounded Emulation and Atomic Recovery

- [X] T055 [US2] Extend `TerminalSessionTests.cs` with failing BEL, BS, TAB, CR, LF positive and boundary cases
- [X] T056 [US2] Add failing BEL proof for in-process notice/status only and no host effect
- [X] T057 [US2] Add failing CSI `A/B/C/D` default, positive, clamp, zero, and 9,999 cases
- [X] T058 [US2] Add failing CSI `H/f` default, row/column, clamp, zero, and 9,999 cases
- [X] T059 [US2] Add failing CSI `J/K` accepted modes plus unsupported-mode atomicity cases
- [X] T060 [US2] Add failing CSI `m` reset and all 16 foreground/background color cases
- [X] T061 [US2] Add failing unsupported SGR code and multi-parameter atomicity cases
- [X] T062 [US2] Add failing truncated, unknown, non-numeric, empty, trailing, and interrupted-sequence recovery cases
- [X] T063 [US2] Add failing 63/64/65 sequence-character boundary cases
- [X] T064 [US2] Add failing 4/5 parameter and 9,999/10,000 value boundary cases
- [X] T065 [US2] Increment the manual build counter, run the expanded emulation matrix expecting red, and record exact failures
- [X] T066 [US2] Implement bounded text/C0/ESC/CSI observation classification in `src/TuiVision.Drivers.Console/TerminalSession.cs`
- [X] T067 [US2] Validate full syntax, command, parameter count, ranges, lifecycle, and capability before any state mutation
- [X] T068 [US2] Implement BEL, BS, TAB, CR, and LF with deterministic boundary semantics
- [X] T069 [US2] Implement CSI relative and absolute cursor movement with documented defaults and clamping
- [X] T070 [US2] Implement CSI display/line erase for the documented modes only
- [X] T071 [US2] Implement CSI `m` reset and 16-color foreground/background attributes
- [X] T072 [US2] Publish accepted, rejected, and unsupported outcomes with stable text-readable status and recovery boundary
- [X] T073 [US2] Preserve cells, cursor, attributes, history, and next independent observation for every rejected/unsupported sequence
- [X] T074 [US2] Review parser/command/fallback logic for moderate didactic comments that explain validation-before-publication and subset trade-offs
- [X] T075 [US2] Increment the manual build counter, run the complete emulation matrix to green, and record counts/version/results
- [X] T076 [US2] Record every supported action and negative class in the session/emulation evidence matrix with exact test paths
- [X] T077 [US2] Run a bounded deterministic malformed-input sweep without adding a broad fuzzing dependency and record its proof limit

## Phase 6: User Story 3 - Charset and Font Contracts

- [X] T078 [US3] Add `tests/TuiVision.Drivers.Tests/TerminalCharsetAndFontTests.cs` with failing Unicode identity, KOI8-R mapping, and host-locale-independence cases
- [X] T079 [US3] Add failing invalid Unicode, unmappable unit, unsupported codepage, and U+FFFD-only replacement cases
- [X] T080 [US3] Add a source-controlled 4,096-byte raw 8x16 test fixture under `tests/TuiVision.Drivers.Tests/Fixtures/terminal-font-8x16.bin`
- [X] T081 [US3] Add failing valid fixture metadata and representative glyph-row access tests
- [X] T082 [US3] Add failing wrong-width, wrong-height, wrong-glyph-count, wrong-stride, truncated, oversized, unsupported-format, and arbitrary-path cases
- [X] T083 [US3] Increment the manual build counter, run charset/font tests expecting the documented red boundary, and record failures
- [X] T084 [US3] Add fully XML-documented mapping outcomes/results and `TerminalCharsetMapper` in `src/TuiVision.Drivers.Console/TerminalCharsetMapper.cs`
- [X] T085 [US3] Implement host-independent Unicode validation and KOI8-R byte-to-Unicode mapping
- [X] T086 [US3] Implement U+FFFD replacement with explicit mapped/replaced/rejected/unsupported outcomes
- [X] T087 [US3] Add fully XML-documented `BitmapFontFixture` metadata/result types in `src/TuiVision.Drivers.Console/BitmapFontFixture.cs`
- [X] T088 [US3] Implement exact raw 8x16/256/16/4,096 validation before fixture publication
- [X] T089 [US3] Expose deterministic glyph-row bytes without host font installation, generator execution, or arbitrary file loading
- [X] T090 [US3] Review mapping/font logic for concise historical, replacement, and proof-boundary comments
- [X] T091 [US3] Increment the manual build counter, run charset/font tests to green, and record exact counts/version/result
- [X] T092 [US3] Record charset and font `SmallFrameworkFix` decisions, mappings, fixture limits, host independence, and historical deviations
- [X] T093 [US3] Re-prove no host setup script, font generator, compressed historical asset, locale, or codepage was executed or modified

## Phase 7: User Story 4 - Profiles, Host Evidence, and Fallbacks

- [X] T094 [US4] Add `tests/TuiVision.Drivers.Tests/TerminalProfileTests.cs` with failing valid minimal/full profile and source/effective value assertions
- [X] T095 [US4] Add failing default cases for missing optional FontId, Foreground, and Background
- [X] T096 [US4] Add failing missing/empty required ProfileId and Charset cases
- [X] T097 [US4] Add failing malformed JSON, unknown property, duplicate property, invalid charset/color/font, and trailing-content cases
- [X] T098 [US4] Add failing unavailable font/host capability fallback with requested/effective/default/source/status/reason evidence
- [X] T099 [US4] Add failing macOS/Linux/Windows/WSL/headless capability classification cases independent of physical-host proof
- [X] T100 [US4] Increment the manual build counter, run profile/host tests expecting the documented red boundary, and record failures
- [X] T101 [US4] Add fully XML-documented profile, capability, fallback, and parse-result types in `src/TuiVision.Drivers.Console/TerminalProfile.cs`
- [X] T102 [US4] Implement token-level duplicate/unknown-property validation and closed `System.Text.Json` parsing
- [X] T103 [US4] Implement required-field validation, optional safe defaults, and whole-profile atomic rejection
- [X] T104 [US4] Implement unavailable font/host capability fallback with requested and effective values preserved and apply accepted profile/charset/font/default-color metadata to the session
- [X] T105 [US4] Implement deterministic host-family evidence classification without claiming physical observation
- [X] T106 [US4] Review profile/fallback/capability logic for concise schema, default, and honest-evidence comments
- [X] T107 [US4] Increment the manual build counter, run profile/host tests to green, and record exact counts/version/result
- [X] T108 [US4] Complete macOS, Linux, Windows/WSL, and headless host rows with evidence class, result, residual risk, and re-evaluation trigger
- [X] T109 [US4] Run any locally available physical macOS terminal observation without mutating host state and record other physical hosts as `NotRun`
- [X] T110 [US4] Record profile `SmallFrameworkFix` and host fallback decisions with exact evidence paths

## Phase 8: User Stories 1 and 4 - Controls Projection and App-Loop Proof

- [X] T111 [US1] Add `tests/TuiVision.Controls.Tests/TTerminalViewTests.cs` with failing draw proof for session text, attributes, cursor marker, and text status
- [X] T112 [US1] Add failing controlled key/text input proof updating the Driver-owned session through the view
- [X] T113 [US1] Add failing view resize/session resize synchronization and clipping proof
- [X] T114 [US4] Add a bounded `TApplication` harness proving concrete `TTerminalView` identity, effective profile/charset/font metadata, status line, visible cells, and deterministic quit through `app.Run()`
- [X] T115 [US4] Add failing disabled/unsupported capability proof preserving keyboard quit and visible fallback status
- [X] T116 [US4] Add failing proof that existing Compatibility xterm key translation remains unchanged and no duplicate translation exists
- [X] T117 [US1] Increment the manual build counter, run Controls terminal tests expecting the documented red boundary, and record failures
- [X] T118 [US1] Add fully XML-documented `TTerminalView` in `src/TuiVision.Controls/TTerminalView.cs` using the public session contract
- [X] T119 [US1] Render the session snapshot, current attributes, cursor, and status into the existing owner buffer with deterministic clipping
- [X] T120 [US1] Forward only controlled printable/key input and preserve existing event clearing/dispatch semantics
- [X] T121 [US4] Integrate supported, disabled, unsupported, and quit states into the app-loop harness without creating a Wave-4 example
- [X] T122 [US4] Review view/app-loop/proof code for concise ownership, cursor/status, A11Y, and proof-boundary comments
- [X] T123 [US4] Increment the manual build counter, run complete Controls app-loop matrix to green, and record state/view/status/cell proof
- [X] T124 [US4] Run existing Driver presentation, Compatibility input, Controls draw/event, and app-loop regression tests in one targeted Release batch where possible
- [X] T125 [US4] Record Controls projection `SmallFrameworkFix`, Compatibility `UseExistingFramework`, and primary proof limits
- [X] T126 [US4] Confirm `examples/` contains no new parser, mapper, font loader, profile fallback, or visible Wave-4 port

## Phase 9: Security, Documentation, Governance, and Routing

- [X] T127 Complete all parser/state/mapping/font/profile/view comment-decision rows and confirm no trivial what-comments were added
- [X] T128 Populate NIST SSDF, CWE Top 25, secure-input, resource-limit, fail-safe, and STRIDE/CIA/CAPEC governance rows
- [X] T129 Populate trigger-based `N/A` rows for ASVS, new SBOM/VEX/SLSA/OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, and DORA
- [X] T130 Populate iSAQB/component-boundary rows and trigger-based `N/A` for S-ADR, arc42 changes, Zero Trust, SAMM, BSI C3A, and BSI C5
- [X] T131 Populate A11Y, didactic-comment, cross-platform host, script-governance `N/A`, agent-parity, and `.specify/templates/` `N/A` rows
- [X] T132 Review existing security and architecture evidence and update only triggered terminal-input/resource-bound rows; record unchanged rationale elsewhere
- [X] T133 Update `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs` with the managed session, charset, font, profile, and unsupported host boundaries
- [X] T134 Create `docs/guides/terminal-charset-hardening.md` with DE-first/EN-second session, subset, charset, font, profile, host, security, A11Y, and proof guidance
- [X] T135 Add the terminal/charset guide to `docs/toc.yml` and relevant README/index links
- [X] T136 Review changed Markdown for CEFR-B2, umlauts/ß, semantic structure, fenced-language tags, and text-first accessibility
- [X] T137 Update active Feature-021 and next Feature-022 context in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [X] T138 Verify the five maintained agent context blocks are synchronized and record hashes/results
- [X] T139 Update `Pflichtenheft.md` completion and next-intake marker to `Lastenheft_Wave4-Visual-Component-Porting.md`
- [X] T140 Update `docs/project-statistics.md` with 021 scope, lines, work window, 80/125-line baselines, validation, and next intake

## Phase 10: Validation, Archive, and Local Completion

- [X] T141 Run `git diff --check`, placeholder/TODO, scope, local-parser, generated-output, `examples/`, and `tv203s/` scans; record results
- [X] T142 Run `dotnet format --verify-no-changes --no-restore` and record result
- [X] T143 Increment the manual build counter and run all targeted Drivers/Controls/Compatibility Release tests; record per-project counts and version
- [X] T144 Increment the manual build counter and run the full Release suite; record per-project and total counts
- [X] T145 Validate `coverlet.runsettings`, increment the manual build counter, run canonical coverage, and record all five required assembly percentages
- [X] T146 Run `docfx docfx.json`, then Playwright/axe DocFX smoke; record warnings/errors/tests
- [X] T147 Review the generated guide through UTF-8 `lynx` when available and record the text-first result
- [X] T148 Run repository diff/tracked-secret scans and record results
- [X] T149 Remove generated DocFX/API/test artifacts and prove they are absent from Git
- [X] T150 Verify every FR, CR, and SC has an exact evidence link and every framework/host/governance row is complete
- [X] T151 Verify exact SC-002/SC-003 boundary coverage, six framework decisions, no executable out-of-scope change, and no local blocker
- [X] T152 Archive `Lastenheft_05_TerminalCharsetAndEmulation.md` through the PowerShell rename workflow with suffix `021-terminal-charset-hardening`
- [X] T153 Record final local task count, changed files, validation, conditional gates, follow-ups, and retrospective observations
- [X] T154 Re-run `specify check`, prerequisite/task checks, checklist counts, and final Analyze consistency after implementation evidence updates
- [X] T155 Mark all local tasks through T155 complete only after their acceptance results are present in `specs/021-terminal-charset-hardening/pr-evidence.md`

## Phase 11: Authorized GitHub Delivery

- [X] T156 Align `Directory.Build.props` to `1.21.<branch-commit-count>.<build>` without incrementing Build, stage intentional files, and record scope
- [X] T157 Commit the complete 021 implementation after recording the planned version and staged-tree scope in `specs/021-terminal-charset-hardening/pr-evidence.md`; defer the observed commit hash to `specs/021-terminal-charset-hardening/closeout-evidence.md`
- [X] T158 Recalculate branch commit count, align version/evidence, and create a bounded follow-up commit only when required
- [X] T159 Push `021-terminal-charset-hardening` and record the observed branch/head in `specs/021-terminal-charset-hardening/closeout-evidence.md` without committing it onto the reviewed feature head
- [X] T160 Create a ready feature PR from `specs/021-terminal-charset-hardening/pr-evidence.md` and record its URL in `specs/021-terminal-charset-hardening/closeout-evidence.md` without invalidating reviewed-head claims
- [X] T161 Monitor required CI, Claude/Copilot availability, review comments, and GraphQL threads to convergence; record current-head state in `specs/021-terminal-charset-hardening/closeout-evidence.md`
- [X] T162 Remediate every actionable remote finding with focused validation and record response/thread resolution
- [X] T163 Use the authorized narrow admin bypass only after green required checks, zero actionable threads, and a sole human-approval block; record exact boundary
- [X] T164 Merge with a merge commit, delete the remote feature branch, switch to local `main`, fetch/prune/pull fast-forward, and prove clean `HEAD == origin/main`
- [ ] T165 Record post-merge facts through a non-empty evidence-only closeout PR using `specs/021-terminal-charset-hardening/closeout-evidence.md` only when causally necessary; otherwise document in the pre-merge ledger why none is needed
- [ ] T166 Finish with synchronized clean `main` and a complete Feature-021 evidence record, ready for the separate retrospective and Home-Baseline handoff

## Dependencies and Execution Order

- T001-T028 gate all implementation.
- T029-T041 establish the Driver red slice; T042-T054 make the session foundation green.
- T055-T077 expand bounded emulation before charset/font tasks T078-T093.
- Profile/host tasks T094-T110 and session foundation gate Controls tasks T111-T126.
- Documentation/governance T127-T140 precedes validation/archive T141-T155.
- Delivery T156-T166 starts only after every local acceptance gate passes.
- `pr-evidence.md`, `Directory.Build.props`, Driver session files, `TTerminalView.cs`,
  documentation, statistics, and agent files remain single-writer.
- No delivery task is accepted without its exact 021 evidence entry or pre-named causal closeout path.

## Requirement Coverage

| Requirement | Task coverage |
|---|---|
| FR-001 to FR-005 | T016-T019, T029-T054, T111-T125 |
| FR-006 to FR-009 | T031-T033, T055-T077 |
| FR-010 to FR-015 | T021-T023, T078-T093 |
| FR-016 to FR-017 | T094-T110 |
| FR-018 to FR-020 | T111-T126 |
| FR-021 to FR-025 | T020-T028, T108-T110, T124-T126 |
| FR-026 to FR-030 | T127-T140, T152-T153 |
| CR-001 to CR-003 | T005-T006, T016-T028, T128, T141-T145 |
| CR-004 to CR-008 | T128-T132 |
| CR-009 to CR-012 | T108-T140, T146-T147 |
| CR-013 to CR-014 | T017-T019, T026, T029-T041, T055-T065, T078-T083, T094-T100, T111-T117 |
| CR-015 to CR-016 | T014, T156-T166 |
| SC-001 to SC-003 | T029-T077, T111-T125 |
| SC-004 to SC-006 | T078-T110 |
| SC-007 to SC-010 | T020-T028, T108-T126 |
| SC-011 to SC-013 | T127-T166 |

## Independent Story Acceptance

- **US1**: A controlled session proves text, cursor, visible cells, history,
  resize, reset, lifecycle, and a minimal Controls projection without a host process.
- **US2**: The complete documented C0/CSI subset passes positive and exact
  boundary matrices; every malformed or unsupported sequence is atomic and recoverable.
- **US3**: Unicode/KOI8-R mapping and one raw 8x16 fixture pass positive,
  replacement, invalid, host-independent, and exact-size proof without host mutation.
- **US4**: Closed profiles, safe defaults, honest host evidence, and a real
  app-loop/view/status/cell proof pass while unavailable physical hosts stay `NotRun`.
