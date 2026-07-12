# Tasks: Wave-4 Visual Component Porting

**Input**: Accepted artifacts under `specs/022-wave4-visual-component-porting/`
**Delivery mode**: `MergeAndSync`
**Acceptance ledger**: `specs/022-wave4-visual-component-porting/pr-evidence.md`

Tasks are sequential because most slices share the smoke project, linked
runtime, solution, evidence, version, documentation, statistics, agent, or
delivery files. No task-level `[P]` marker is used where ownership could overlap.

## Phase 1: Preflight and Evidence Foundation

- [X] T001 Create `specs/022-wave4-visual-component-porting/pr-evidence.md` from the autonomous evidence template before runtime/test edits
- [X] T002 Verify branch, ancestry from synchronized `main`, `.specify/feature.json`, and intake order; record exact state in `pr-evidence.md`
- [X] T003 Run `specify check` and PowerShell prerequisite checks with tasks included; record results in `pr-evidence.md`
- [X] T004 Verify every 022 checklist has zero incomplete items and record counts in `pr-evidence.md`
- [X] T005 Read AGENTS, Constitution, binding Lastenheft, and all 022 artifacts; record material conflicts or `None`
- [X] T006 Verify the six installed preset names, versions, and priorities; record them in `pr-evidence.md`
- [X] T007 Add five-example, visible-state, historical, framework, host, asset, proof, and comment-decision tables to `pr-evidence.md`
- [X] T008 Add governance rows with owner, reviewer, date/result, residual risk, follow-up, and re-evaluation trigger
- [X] T009 Add requirement/SC coverage, validation, generated-output hygiene, remote delivery, causal closeout, and retrospective tables
- [X] T010 Pre-name `specs/022-wave4-visual-component-porting/closeout-evidence.md` for self-invalidating reviewed-head and post-merge facts
- [X] T011 Record `speckit-constitution` unchanged and `speckit-taskstoissues` N/A because one dependency-ordered feature PR is the delivery unit
- [X] T012 Record Specify, two Clarify passes, checklist convergence, Plan, Plan Review, Tasks, and pending Analyze gates
- [X] T013 Scan 022 artifacts for unresolved markers, temporary requirement IDs, duplicate tasks, and task gaps; record result
- [X] T014 Record hard scope boundaries, `MergeAndSync` authority, missing-review semantics, and narrow admin-bypass policy
- [X] T015 Prove `git diff -- tv203s/` contains no implementation change before historical review

## Phase 2: Compile Surface, Historical Intent, and Architecture Gate

- [X] T016 Review project graph, example references, solution entries, and absence of dependency cycles
- [X] T017 Review public types/imports/XML docs/nullability/lifecycle/naming before the first red command
- [X] T018 Review smoke harness, event queues, app-loop ownership, focus, status, description, view-tree, and buffer/cell helpers
- [X] T019 Review linked-source assembly identity and define public-state/delegate cross-example proof
- [X] T020 Review Feature-021 `TerminalSession`, mapper, fixture, profile, host, `TTerminalView`, and tests as binding reusable contracts
- [X] T021 Review historical `terminal.cc` and `terminal.h` read-only; record retained session/view intent and process deviations
- [X] T022 Review both Cyrillic `test.cc` files, README, ACM data, and setup scripts read-only; record mapping intent and prohibited host actions
- [X] T023 Review Fonts `test.cc`, `genraw.cc`, `font.016`, `ocr.sft`, `fontcoll.cc`, and `fontcoll.h` read-only; record fixture/generator boundaries
- [X] T024 Review ETerm `menus.cfg`, `theme.cfg`, docs and XTerm `Xterm.res` plus relevant xterm key/display/screen sources read-only
- [X] T025 Verify historical `font.016` shape and select a nonblank glyph without executing a generator
- [X] T026 Classify each example with one preliminary framework decision and exact reused components
- [X] T027 Record shared presentation-only ownership and reject reusable terminal/mapping/font/profile/host logic in `examples/`
- [X] T028 Record exact historical source inventory, retained purpose, intentional deviations, and non-executed scripts/generators
- [X] T029 Record compile-surface findings and any required pre-red correction in `pr-evidence.md`
- [X] T030 Re-prove `tv203s/` remains unchanged after read-only review

## Phase 3: User Story 1 - Failing Terminal Vertical Slice

- [X] T031 Add five Wave-4 project skeletons and references without domain implementation; align `TuiVision.sln` and `coverlet.runsettings` with the new example assemblies
- [X] T032 Link `examples/Shared/Wave4Runtime.cs` into all five projects and add smoke-project references without implementing behavior
- [X] T033 Add failing `TerminalSmokeTests.cs` construction and first-frame assertions
- [X] T034 Add failing controlled text input/output, cursor, attribute, session/profile/capability assertions
- [X] T035 Add failing app-loop exact `TTerminalView` identity and rendered-cell region proof
- [X] T036 Add failing status-line and keyboard-reachable description command proof
- [X] T037 Add failing accepted cursor/attribute action with exact before/after state
- [X] T038 Add failing rejected/unsupported action atomicity and next-input recovery proof
- [X] T039 Add failing reset and unavailable-capability visible fallback proof
- [X] T040 Add failing deterministic quit and post-run state proof
- [X] T041 Add failing narrow-viewport identity/status/fallback/clipping proof
- [X] T042 Review the grouped Terminal red matrix so every expected failure and ownership boundary is explicit
- [X] T043 Increment manual build counter and run focused Terminal matrix expecting the documented red compile/contract boundary
- [X] T044 Record red test names, state/view/cell expectations, version, and proof limits in `pr-evidence.md`
- [X] T045 Recheck imports, XML docs, harness helpers, ownership, and linked-source identity exposed by the red surface

## Phase 4: User Story 1 - Terminal and Shared Presentation Implementation

- [X] T046 Add bilingual XML-documented `Wave4StatusLine`, `Wave4Application`, and presentation helpers in `examples/Shared/Wave4Runtime.cs`
- [X] T047 Implement deterministic scripted event queue, quit fallback, dynamic status, stable main region, and screen-region conversion
- [X] T048 Implement keyboard-reachable bilingual description and text-first fallback presentation
- [X] T049 Add `TerminalApp` using one existing `TerminalSession` and `TTerminalView` without duplicate parser or host access
- [X] T050 Implement controlled first-frame welcome text, profile/charset/font/capability status, and exact visible view identity
- [X] T051 Implement accepted text/cursor/attribute command routes through real event dispatch
- [X] T052 Implement visible rejected/unsupported outcome, atomic recovery, reset, and next independent input
- [X] T053 Implement unavailable-capability fallback preserving description and quit
- [X] T054 Implement standard and narrow viewport composition without status/identity loss
- [X] T055 Review shared/Terminal code for concise DE-first/EN-second why/trade-off/A11Y/proof-boundary comments
- [X] T056 Increment manual build counter and run focused Terminal tests to green; record exact count/version/result
- [X] T057 Record Terminal framework decision, reused 021 contracts, app-loop/state/view/cell proof, and process boundary
- [X] T058 Confirm no process, shell, PTY, audio, font, codepage, locale, keyboard-map, or terminal-setting API enters Terminal/shared code
- [X] T059 Add Terminal normal-start guide draft and README matrix row only after the slice is green
- [X] T060 Re-prove the shared helper contains presentation/proof only and no duplicate 021 domain behavior

## Phase 5: User Story 2 - Cyrillic and Fonts Test-First Delivery

- [X] T061 Add failing `CyrillicSmokeTests.cs` first-frame labeled KOI8-R/Unicode grid proof
- [X] T062 Add failing direct, replacement, invalid, and unsupported mapping state/status/cell cases
- [X] T063 Add failing host-locale-independent and narrow-viewport fallback cases
- [X] T064 Add failing Cyrillic description, exact view identity, app-loop command, and quit proof
- [X] T065 Add exact source-controlled `examples/Fonts/Fixtures/font-8x16.bin` with documented historical origin
- [X] T066 Add failing `FontsSmokeTests.cs` exact metadata and nonblank selected-glyph proof
- [X] T067 Add failing recognizable 8x16 pixel-region state/view/cell proof
- [X] T068 Add failing wrong length, geometry, stride, format/source, and blank-selected-glyph fallback classes
- [X] T069 Add failing Fonts description, status, narrow-viewport, app-loop command, and quit proof
- [X] T070 Review grouped Cyrillic/Fonts red matrices for explicit expected failures and separate ownership
- [X] T071 Increment manual build counter and run Cyrillic/Fonts matrices expecting documented red boundaries
- [X] T072 Implement `CyrillicApp` using `TerminalCharsetMapper` and fixed controlled sample data
- [X] T073 Render labeled source bytes, glyphs, outcomes, reasons, status, and fallback without host locale/codepage
- [X] T074 Implement Cyrillic mapping-cycle command, description, narrow viewport, and quit route
- [X] T075 Implement `FontsApp` using `BitmapFontFixture` and only the project-owned copied fixture
- [X] T076 Render fixture metadata and one known nonblank glyph as a stable non-color-only 8x16 pixel matrix
- [X] T077 Implement invalid/unsupported fixture fallback, selected-glyph command, description, narrow viewport, and quit
- [X] T078 Review mapping/font/raster code for historical, replacement, host-independence, and proof-boundary comments
- [X] T079 Increment manual build counter and run Cyrillic/Fonts tests to green; record exact counts/version/results
- [X] T080 Record Cyrillic and Fonts framework decisions, historical deviations, asset hashes/shape, proof regions, and host boundaries
- [X] T081 Confirm no historical setup script/generator, host codec/font API, arbitrary path, or write operation was executed
- [X] T082 Add Cyrillic and Fonts guide drafts plus README matrix rows only after both slices are green

## Phase 6: User Story 3 - ETerm and XTerm Manifest Delivery

- [X] T083 Add failing `ETermSmokeTests.cs` first-frame proof for at least three menu/theme/presentation entries
- [X] T084 Add failing ETerm source identity, selection command, dynamic status, description, view, and cell proof
- [X] T085 Add failing ETerm absent/out-of-subset entry visible unsupported fallback and narrow-viewport cases
- [X] T086 Add failing assertion that ETerm never claims or invokes native legacy parser/host theme support
- [X] T087 Add failing `XTermSmokeTests.cs` first-frame proof for at least three resource/sequence/capability entries
- [X] T088 Add failing XTerm source identity, selection command, dynamic status, description, view, and cell proof
- [X] T089 Add failing XTerm out-of-subset/native-resource visible unsupported fallback and narrow-viewport cases
- [X] T090 Add failing assertion that existing Compatibility input and 021 session parser remain the only relevant reusable boundaries
- [X] T091 Review grouped ETerm/XTerm red matrices for explicit failure classes and immutable ownership
- [X] T092 Increment manual build counter and run ETerm/XTerm matrices expecting documented red boundaries
- [X] T093 Implement immutable typed ETerm manifest entries with exact historical source metadata
- [X] T094 Implement ETerm selection, main list, status, description, unsupported fallback, narrow viewport, and quit
- [X] T095 Implement immutable typed XTerm manifest entries plus accepted 021 subset metadata
- [X] T096 Implement XTerm selection, main list, status, description, unsupported native-resource fallback, narrow viewport, and quit
- [X] T097 Keep manifest lookup exact and bounded; reject missing/duplicate/out-of-subset entries before visible state publication
- [X] T098 Review manifest code for resource-only deviation, false-capability, and proof-boundary comments
- [X] T099 Increment manual build counter and run ETerm/XTerm tests to green; record exact counts/version/results
- [X] T100 Record ETerm/XTerm `IntentionalDeviation` decisions, representative entries, proof regions, and native-parser boundaries
- [X] T101 Confirm no arbitrary file parser, X resource database, terminfo, external command, host theme, or terminal mutation exists
- [X] T102 Add ETerm and XTerm guide drafts plus README matrix rows only after both slices are green

## Phase 7: Cross-Example Proof, Governance, and Documentation

- [X] T103 Add `Wave4VisualSmokeMatrixTests.cs` covering all five unique projects through public state/delegates
- [X] T104 Prove each project has main/status/description, app-loop operation, exact view, cells, host classification, framework decision, and fallback
- [X] T105 Prove linked `Wave4Runtime` source is not treated as one cross-assembly CLR type identity
- [X] T106 Increment the manual build counter, run existing Wave-1/2/3 smoke regressions with the five new projects, and record version plus no behavioral regression
- [X] T107 Complete all five comment-decision rows and confirm no trivial what-comments were added
- [X] T108 Populate NIST SSDF, CWE, input/resource/fixture bounds, fail-safe, least privilege, and STRIDE/CIA/CAPEC rows
- [X] T109 Populate trigger-based N/A rows for ASVS, new SBOM/VEX/SLSA/OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, and DORA
- [X] T110 Populate iSAQB/reuse rows and trigger-based N/A for S-ADR, arc42 change, Zero Trust, SAMM, BSI C3A, and BSI C5
- [X] T111 Populate A11Y, host cross-platform, script-governance N/A, agent parity, and `.specify/templates/` N/A rows
- [X] T112 Update architecture/security evidence only for actually triggered Wave-4 resource/host boundaries; record unchanged rationale elsewhere
- [X] T113 Complete all five guides with DE-first/EN-second startup, operation, main area, status, description, host evidence, fallback, historical source, and A11Y sections
- [X] T114 Update `examples/README.md`, root README if relevant, and `docs/toc.yml` for five examples
- [X] T115 Review changed Markdown for CEFR-B2, umlauts/ß, semantic headings/tables/lists, fenced language tags, and text-first accessibility
- [X] T116 Update active Feature-022 and next Feature-023 context in all five maintained agent surfaces
- [X] T117 Verify five agent blocks are synchronized and record hashes/results
- [X] T118 Update `Pflichtenheft.md` completion and next-intake marker to `Lastenheft_06_A11Y_Framework.md`
- [X] T119 Update `docs/project-statistics.md` with 022 scope, lines, work window, 80/125-line baselines, validation, and next intake
- [X] T120 Complete exact FR/CR/SC links, example/framework/host/governance rows, and residual-risk/follow-up fields in `pr-evidence.md`

## Phase 8: Validation, Archive, and Local Completion

- [X] T121 Run `git diff --check`, placeholder, TODO, scope, duplicate-domain, generated-output, `tv203s/`, dependency, and host-mutation scans; record results
- [X] T122 Run `dotnet format --verify-no-changes --no-restore` and record result
- [X] T123 Increment manual build counter and run all targeted Wave-4/example Release smokes; record per-suite counts/version
- [X] T124 Increment manual build counter and run the complete example-smoke suite; record count/version
- [X] T125 Increment manual build counter and run the full Release suite; record per-project and total counts
- [X] T126 Validate `coverlet.runsettings`, increment manual build counter, run canonical coverage, and record all five required assembly percentages
- [X] T127 Run `docfx docfx.json`, then Playwright/axe DocFX smoke; record models/warnings/errors/tests
- [X] T128 Review all five generated guides through UTF-8 `lynx` when available and record text-first result
- [X] T129 Run repository diff/tracked-secret scans and record results
- [X] T130 Remove generated DocFX/API/test artifacts and prove they are absent from Git
- [X] T131 Verify five CLI project paths build/start contractually through project/smoke proof without requiring interactive manual input
- [X] T132 Verify SC-003 through SC-008 exact matrices, five framework decisions, host rows, and no local blocker
- [X] T133 Verify final diff contains no process/shell/PTY, host mutation, legacy parser, dependency, Wave-5/023, user-data, generated, or `tv203s/` change
- [X] T134 Archive `Lastenheft_Wave4-Visual-Component-Porting.md` through PowerShell rename with suffix `022-wave4-visual-component-porting`
- [X] T135 Record final local task count, changed files, validation, conditional gates, follow-ups, and retrospective observations
- [X] T136 Re-run `specify check`, prerequisite/task checks, checklist counts, and final Analyze consistency after implementation evidence
- [X] T137 Mark all local tasks through T137 complete only after their acceptance results are present in `pr-evidence.md`

## Phase 9: Authorized GitHub Delivery

- [X] T138 Align `Directory.Build.props` to `1.22.<branch-commit-count>.<build>` without incrementing Build, stage intentional files, and record scope in `specs/022-wave4-visual-component-porting/pr-evidence.md`
- [ ] T139 Commit the complete 022 implementation after recording planned version/scope in `specs/022-wave4-visual-component-porting/pr-evidence.md`; defer observed hash to `specs/022-wave4-visual-component-porting/closeout-evidence.md`
- [ ] T140 Recalculate branch commit count, align version and `specs/022-wave4-visual-component-porting/pr-evidence.md`, and create a bounded follow-up commit only when required
- [ ] T141 Push `022-wave4-visual-component-porting` and record observed branch/head in `specs/022-wave4-visual-component-porting/closeout-evidence.md`
- [ ] T142 Create a ready feature PR from `specs/022-wave4-visual-component-porting/pr-evidence.md` and record its URL in `specs/022-wave4-visual-component-porting/closeout-evidence.md` without invalidating reviewed-head claims
- [ ] T143 Monitor required CI, Claude/Copilot availability, comments, and GraphQL threads to convergence; record current-head state in `specs/022-wave4-visual-component-porting/closeout-evidence.md`
- [ ] T144 Remediate every actionable remote finding with focused validation and record response/thread resolution in `specs/022-wave4-visual-component-porting/closeout-evidence.md`
- [ ] T145 Use authorized narrow admin bypass only after green checks, zero actionable threads, and sole human-approval block; record boundary in `specs/022-wave4-visual-component-porting/closeout-evidence.md`
- [ ] T146 Merge with merge commit, delete remote feature branch, switch to local `main`, fetch/prune/pull, and prove clean `HEAD == origin/main` in `specs/022-wave4-visual-component-porting/closeout-evidence.md`
- [ ] T147 Record post-merge facts through one non-empty evidence-only closeout PR using `specs/022-wave4-visual-component-porting/closeout-evidence.md` only when causally necessary
- [ ] T148 Converge closeout-PR checks/reviews under the same policy, record the result in `specs/022-wave4-visual-component-porting/closeout-evidence.md`, and merge/sync without recursive closeout evidence
- [ ] T149 Finish with synchronized clean `main` and complete Feature-022 evidence in `specs/022-wave4-visual-component-porting/closeout-evidence.md`, ready for separate retrospective and Home-Baseline handoff

## Dependencies and Execution Order

- T001-T030 gate all implementation.
- T031-T045 establish the Terminal red slice; T046-T060 make it green.
- T061-T082 deliver Cyrillic/Fonts; T083-T102 deliver ETerm/XTerm.
- T103-T120 complete shared proof, governance, docs, routing, and statistics.
- T121-T137 gate local completion; T138-T149 gate remote delivery.
- `pr-evidence.md`, `closeout-evidence.md`, `Wave4Runtime.cs`, smoke csproj,
  solution, version, docs navigation, statistics, and agent files remain single-writer.

## Requirement Coverage

| Requirement | Task coverage |
|---|---|
| FR-001 to FR-008 | T016-T020, T031-T060, T103-T106 |
| FR-009 to FR-010 | T033-T060 |
| FR-011 to FR-015 | T022-T025, T061-T082 |
| FR-016 to FR-019 | T024, T083-T102 |
| FR-020 to FR-024 | T021-T030, T058, T081, T101, T120-T133 |
| FR-025 to FR-028 | T026-T029, T057, T080, T100, T120 |
| FR-029 to FR-036 | T103-T137 |
| CR-001 to CR-003 | T005-T006, T016-T030, T108, T121-T126 |
| CR-004 to CR-007 | T108-T112 |
| CR-008 to CR-014 | T016-T030, T103-T120 |
| CR-015 to CR-016 | T009-T010, T138-T149 |
| SC-001 to SC-003 | T031-T120, T123-T125 |
| SC-004 to SC-008 | T033-T102, T120-T133 |
| SC-009 to SC-013 | T103-T149 |

## Independent Story Acceptance

- **US1**: Terminal proves controlled session, action, rejection, recovery,
  fallback, description, exact view/cells, and quit without host process.
- **US2**: Cyrillic and Fonts prove host-independent mappings plus one exact
  visible nonblank glyph with honest invalid/unsupported fallbacks.
- **US3**: ETerm and XTerm prove useful immutable resource manifests without
  claiming native parser or emulator compatibility.
- **US4**: All demos and guides expose keyboard/text-first operation, distinct
  host evidence classes, historical deviations, and complete validation.
