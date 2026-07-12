# Tasks: Wave-3 Visual Component Porting

**Input**: `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md`,
`contracts/wave3-visual-component-acceptance.md`, and all feature checklists
**Delivery mode**: `MergeAndSync`
**Evidence ledger**: `specs/019-wave3-visual-component-porting/pr-evidence.md`

All tasks are serialized unless a later edit proves independent ownership.
Every checkbox is marked only after its acceptance condition is satisfied.

## Phase 1: Preflight and Evidence Foundation

- [X] T001 Verify clean branch `019-wave3-visual-component-porting`, `HEAD` ancestry from synchronized `main`, and `.specify/feature.json` target; record in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T002 Run `specify check` and `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks`; record exact results in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T003 Verify all feature checklists have zero incomplete items and record counts in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T004 Read `AGENTS.md`, `.specify/memory/constitution.md`, the binding Lastenheft, and all 019 artifacts; record material conflicts or `None` in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T005 Verify the six installed preset names, versions, and priorities; record them in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T006 Create `specs/019-wave3-visual-component-porting/pr-evidence.md` from the autonomous evidence template before runtime edits
- [X] T007 Add the five-example acceptance matrix and exact framework-decision column to `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T008 Add primary proof columns for app-loop route, concrete state, view-tree kind, rendered region, status, description, helper class, result, and proof limit to `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T009 Add controlled-artifact rows for `TvEdit` and `TvHc` ownership, access, cleanup, and proof boundaries to `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T010 Add a governance table with all CR-013 audit fields to `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T011 Add validation, remote-delivery, retrospective, SC coverage, and generated-output hygiene tables to `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T012 Review `specs/018-editor-help-resources-hardening/pr-evidence.md` and map accepted framework contracts into `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T013 Review every named historical Wave-3 `.cc`, header, PO/resource, README, and fixture file read-only and record retained intent/deviation in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T014 Prove `git diff -- tv203s/` is empty and record the historical-source boundary in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T015 Record `speckit-taskstoissues` as `N/A` in `specs/019-wave3-visual-component-porting/pr-evidence.md` because one dependency-ordered feature PR is the accepted delivery unit

## Phase 2: Project Skeleton and TvEdit Red Proof

- [X] T016 Add `examples/Shared/Wave3Runtime.cs` skeleton with presentation-only types and no domain behavior
- [X] T017 Add `examples/TvEdit/TvEdit.csproj` with existing framework references and linked `Wave3Runtime.cs`
- [X] T018 Add minimal `examples/TvEdit/Program.cs` CLI entrypoint with bounded console-size fallback
- [X] T019 Add `examples/BHelp/BHelp.csproj` and minimal `Program.cs` project skeleton
- [X] T020 Add `examples/HelpDemo/HelpDemo.csproj` and minimal `Program.cs` project skeleton
- [X] T021 Add `examples/I18n/I18n.csproj` and minimal `Program.cs` project skeleton
- [X] T022 Add `examples/TvHc/TvHc.csproj` and minimal `Program.cs` project skeleton
- [X] T023 Add all five Wave-3 project references to `tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj`
- [X] T024 Add `tests/TuiVision.Examples.SmokeTests/TvEditSmokeTests.cs` with failing app-loop first-frame, editor-view, and rendered-content proof
- [X] T025 Extend `TvEditSmokeTests.cs` with failing key-edit, modified-state, status, and buffer/cell proof
- [X] T026 Extend `TvEditSmokeTests.cs` with failing description-command proof
- [X] T027 Extend `TvEditSmokeTests.cs` with failing safe-close rejection/acceptance and controlled test-temp ownership proof
- [X] T028 Increment the manual build counter, run the focused `TvEdit` tests expecting the documented red boundary, and record failures/version in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 3: TvEdit Vertical Slice Implementation

- [X] T029 Implement drawable Wave-3 status, Help menu, description window, and stable region conversion in `examples/Shared/Wave3Runtime.cs`
- [X] T030 Implement `TvEditApp` and real `TFileEditor`/`TEditWindow` main composition in `examples/TvEdit/TvEditApp.cs`
- [X] T031 Implement app-loop edit dispatch and visible modified/status synchronization in `examples/TvEdit/TvEditApp.cs`
- [X] T032 Implement explicit safe-close decision flow without arbitrary user discovery in `examples/TvEdit/TvEditApp.cs`
- [X] T033 Implement fixture/test-temp open/save support only where needed for tests in `examples/TvEdit/TvEditApp.cs`
- [X] T034 Implement keyboard-reachable bilingual description in `examples/TvEdit/TvEditApp.cs`
- [X] T035 Review `Wave3Runtime.cs` and `TvEditApp.cs` for selective didactic comments and add only reason/trade-off/proof-boundary comments where needed
- [X] T036 Increment the manual build counter and run focused `TvEdit` Release tests; record count/version/result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T037 Complete the `TvEdit` framework, controlled-artifact, and primary-proof rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T038 Verify the vertical slice has app-loop, state, view, cell, status, description, negative, and I/O proof before any other demo implementation; record gate pass in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 4: Help Demos Red Proof and Implementation

- [X] T039 Add `tests/TuiVision.Examples.SmokeTests/BHelpSmokeTests.cs` with failing first-topic, navigation, view-tree, status, and rendered-cell proof
- [X] T040 Extend `BHelpSmokeTests.cs` with failing unknown-context/missing-target fallback and description proof
- [X] T041 Add `tests/TuiVision.Examples.SmokeTests/HelpDemoSmokeTests.cs` with failing focus/context/hint, help command, view-tree, and cell proof
- [X] T042 Extend `HelpDemoSmokeTests.cs` with failing unknown-context fallback and description proof
- [X] T043 Increment the manual build counter and run grouped focused Help tests expecting the documented red boundary; record failures/version in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T044 Implement controlled topics, cross-references, and fallback content in `examples/BHelp/BHelpApp.cs` using `THelpFile`
- [X] T045 Implement `THelpWindow` main composition and app-loop topic/context navigation in `examples/BHelp/BHelpApp.cs`
- [X] T046 Implement BHelp status and bilingual description, including the proprietary `.tch` deviation, in `examples/BHelp/BHelpApp.cs`
- [X] T047 Implement focusable context controls and current hint/status state in `examples/HelpDemo/HelpDemoApp.cs`
- [X] T048 Implement HelpDemo help command/topic/fallback dispatch in `examples/HelpDemo/HelpDemoApp.cs`
- [X] T049 Implement HelpDemo bilingual description and historical context explanation in `examples/HelpDemo/HelpDemoApp.cs`
- [X] T050 Review BHelp/HelpDemo non-trivial logic for selective didactic comments and add only reason/trade-off/proof-boundary comments where needed
- [X] T051 Increment the manual build counter and run grouped focused Help Release tests; record counts/version/result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T052 Complete BHelp historical, framework, primary-proof, fallback, and deviation rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T053 Complete HelpDemo historical, framework, primary-proof, focus/hint, and fallback rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 5: I18n and TvHc Red Proof and Implementation

- [X] T054 Add `tests/TuiVision.Examples.SmokeTests/I18nSmokeTests.cs` with failing neutral, Spanish, missing-language/key fallback, status, view, cell, and description proof
- [X] T055 Add `tests/TuiVision.Examples.SmokeTests/TvHcSmokeTests.cs` with failing valid compile, visible topic/result, view, cell, status, and description proof
- [X] T056 Extend `TvHcSmokeTests.cs` with grouped malformed/invalid UTF-8 or source diagnostics, no-partial-result, and test-temp-only output proof
- [X] T057 Increment the manual build counter and run grouped focused I18n/TvHc tests expecting the documented red boundary; record failures/version in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T058 Implement explicit resource dictionaries and deterministic lookup state in `examples/I18n/I18nApp.cs`
- [X] T059 Implement I18n language/fallback commands, visible main composition, status, and bilingual description in `examples/I18n/I18nApp.cs`
- [X] T060 Implement controlled compiler source/result composition in `examples/TvHc/TvHcApp.cs` using `THelpSourceCompiler`
- [X] T061 Implement TvHc compile command, stable diagnostics, visible compiled topic, status, and bilingual description in `examples/TvHc/TvHcApp.cs`
- [X] T062 Implement optional test-temp persistence proof with no partial accepted output in `examples/TvHc/TvHcApp.cs` only if required by the tests
- [X] T063 Review I18n/TvHc non-trivial logic for selective didactic comments and add only reason/trade-off/proof-boundary comments where needed
- [X] T064 Increment the manual build counter and run grouped focused I18n/TvHc Release tests; record counts/version/result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T065 Complete I18n historical, framework, primary-proof, lookup/fallback, and host-independence rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T066 Complete TvHc historical, framework, primary-proof, diagnostic, controlled-artifact, and output-boundary rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 6: Complete Proof Matrix and Documentation

- [X] T067 Add `tests/TuiVision.Examples.SmokeTests/Wave3VisualSmokeMatrixTests.cs` to assert five unique project/decision/proof records and no helper-only primary proof
- [X] T068 Add constrained-viewport and stable-layout assertions for all five examples to the Wave-3 smoke matrix
- [X] T069 Increment the manual build counter and run all Wave-3 smoke tests; record counts/version/result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T070 Verify every example has exactly one framework decision and no unclassified reusable local domain logic; record in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T071 Create `docs/guides/examples/tvedit.md` with DE-first/EN-second startup, keyboard, visible state, safe-close, controlled I/O, history, A11Y, and proof
- [X] T072 Create `docs/guides/examples/bhelp.md` with DE-first/EN-second topic, navigation, fallback, `.tch` deviation, A11Y, and proof
- [X] T073 Create `docs/guides/examples/helpdemo.md` with DE-first/EN-second focus/context/hint, commands, fallback, A11Y, and proof
- [X] T074 Create `docs/guides/examples/i18n.md` with DE-first/EN-second language, resource key, fallback, host-independence, A11Y, and proof
- [X] T075 Create `docs/guides/examples/tvhc.md` with DE-first/EN-second source, compile, diagnostics, controlled output, history, A11Y, and proof
- [X] T076 Add all five guides to `docs/toc.yml`
- [X] T077 Update `examples/README.md` with five start commands, main surfaces, keyboard routes, status/description, controlled I/O, and fallbacks
- [X] T078 Review all changed Markdown for German-first/English-second CEFR-B2, umlauts/ß, semantic headings/tables, fenced-code language tags, and text-first accessibility; record in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 7: Governance, Routing, and Statistics

- [X] T079 Populate applicable NIST SSDF, CWE Top 25, secure-coding, controlled-I/O, and STRIDE/CIA/CAPEC rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T080 Populate trigger-based `N/A` rows for ASVS, new SBOM/VEX/SLSA/OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, and DORA in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T081 Populate iSAQB/architecture reuse plus trigger-based `N/A` rows for S-ADR, arc42 security changes, Zero Trust, SAMM, BSI C3A, and BSI C5 in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T082 Populate A11Y, didactic-comment, cross-platform runtime, script-governance `N/A`, agent-parity, and `.specify/templates/` `N/A` rows in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T083 Review existing security and architecture evidence files and record unchanged rationale or triggered changes in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T084 Update the active 019 context and next Feature-020 intake in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [X] T085 Verify the five maintained agent context blocks are synchronized and record hashes/results in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T086 Update `Pflichtenheft.md` completion and next-intake marker to `Lastenheft_04_MouseSupportAndInteraction.md`
- [X] T087 Update `docs/project-statistics.md` with 019 scope, line counts, work window, 80/125-line baselines, validation, and next intake
- [X] T088 Verify SC-001 through SC-013 have explicit evidence links in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 8: Validation, Archive, and Local Completion

- [X] T089 Run `git diff --check`, placeholder/TODO scan, scope scan, generated-output scan, and `tv203s/` diff; record results in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T090 Run `dotnet format --verify-no-changes --no-restore` and record result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T091 Increment the manual build counter and run all targeted Wave-3 Release smoke tests, then use the built outputs for bounded `--no-build` CLI-start proof of all five documented projects; record version/count/start results in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T092 Increment the manual build counter and run the full Release test suite; record per-project and total counts in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T093 Validate `coverlet.runsettings`, increment the manual build counter, run the canonical coverage gate, and record all five assembly percentages in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T094 Run `docfx docfx.json`, then `tests/web-a11y` Playwright/axe, and record warnings/errors/tests in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T095 Review representative new guide output through UTF-8 `lynx` and record text-first result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T096 Run repository diff/tracked secret scans and record result in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T097 Remove generated DocFX/API/test artifacts and prove they are absent from Git in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T098 Archive `Lastenheft_Wave3-Visual-Component-Porting.md` through the repository rename workflow with suffix `019-wave3-visual-component-porting`
- [X] T099 Verify all 99 local tasks through T099, framework/proof/governance/SC tables, line counts, final scope, and no remaining local blocker in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Phase 9: Authorized GitHub Delivery

- [X] T100 Align `Directory.Build.props` to the required `1.19.<branch-commit-count>.<build>` value without incrementing build, stage only intentional files, and record scope in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T101 Commit the complete 019 implementation and capture its commit/version for the next evidence update in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [X] T102 Recalculate branch commit count, update `specs/019-wave3-visual-component-porting/pr-evidence.md` with the T101 commit/version, align and commit version/evidence only if required, and record the result
- [X] T103 Re-align `Directory.Build.props` without a build increment, push `019-wave3-visual-component-porting`, record the observed branch/commit in `specs/019-wave3-visual-component-porting/pr-evidence.md`, commit that evidence update with aligned version, and push it
- [X] T104 Create a ready feature PR from `specs/019-wave3-visual-component-porting/pr-evidence.md`, record the URL in that evidence file, re-align version, commit the PR-reference update, and push it
- [ ] T105 Monitor required CI, Claude/Copilot availability, review comments, and GraphQL threads to convergence; record each state in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [ ] T106 Remediate every actionable remote finding through focused tests/validation and record finding, response, and thread resolution in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [ ] T107 Use the authorized narrow admin bypass only after green required checks, zero actionable threads, and a sole human-approval block; record the exact boundary in `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [ ] T108 Merge with a merge commit, delete the remote feature branch, switch locally to `main`, fetch/prune/pull fast-forward, prove clean `HEAD == origin/main`, and capture those observed facts for the T109 update to `specs/019-wave3-visual-component-porting/pr-evidence.md`
- [ ] T109 Record post-merge facts through a non-empty evidence-only closeout PR only when they could not truthfully be recorded before merge; otherwise record why no closeout PR is needed in `specs/019-wave3-visual-component-porting/pr-evidence.md`

## Dependencies and Execution Order

- T001-T015 gate all implementation.
- T016-T028 establish the red vertical slice; T029-T038 must pass before T039.
- Help tasks T039-T053 precede I18n/TvHc tasks T054-T066.
- Matrix/docs T067-T078 precede governance/routing T079-T088.
- Validation/archive T089-T099 precede delivery T100-T109.
- Shared files (`pr-evidence.md`, `Directory.Build.props`, agent files,
  `docs/project-statistics.md`, `Pflichtenheft.md`) are always single-writer.
- No remote/delivery task may be accepted without its exact 019 evidence entry.

## Requirement Coverage

| Requirement | Task coverage |
|---|---|
| FR-001 to FR-004 | T016-T023, T029-T034, T044-T049, T058-T061, T067-T077 |
| FR-005 to FR-008 | T024-T028, T039-T043, T054-T057, T067-T070 |
| FR-009 to FR-010 | T024-T038, T071 |
| FR-011 to FR-013 | T039-T053, T072-T073 |
| FR-014 to FR-015 | T054, T058-T059, T065, T074 |
| FR-016 to FR-018 | T055-T066, T075 |
| FR-019 to FR-020 | T013-T014, T046, T049, T052-T053, T065-T066, T071-T075 |
| FR-021 to FR-024 | T007-T008, T037, T052-T053, T065-T070 |
| FR-025 to FR-027 | T071-T078, T094-T095 |
| FR-028 to FR-029 | T084-T087, T098 |
| FR-030 to FR-031 | T014, T089, T097, T099 |
| FR-032 | T035, T050, T063, T082 |
| CR-001 to CR-003 | T004-T005, T079, T089-T093 |
| CR-004 to CR-008 | T079-T083 |
| CR-009 to CR-012 | T035, T050, T063, T071-T078, T082, T084-T085, T094-T095 |
| CR-013 | T010, T079-T083 |
| CR-014 | T100-T109 |
| SC-001 to SC-003 | T016-T023, T024-T070, T091 |
| SC-004 | T024-T038, T071 |
| SC-005 | T039-T053, T072-T073 |
| SC-006 | T054, T058-T059, T065, T074 |
| SC-007 | T055-T066, T075 |
| SC-008 | T071-T078, T094-T095 |
| SC-009 | T089-T097, T105-T106 |
| SC-010 to SC-012 | T070, T079-T089, T097-T099 |
| SC-013 | T100-T109 |
