# Tasks: Editor, Help, and Resources Hardening

**Input**: [spec.md](spec.md), [plan.md](plan.md), [research.md](research.md),
[data-model.md](data-model.md), [quickstart.md](quickstart.md), and
[contracts/hardening-contracts.md](contracts/hardening-contracts.md)

**Execution policy**: Complete tasks in ID order. Mark `[X]` only after the
stated file/result and its evidence exist. Shared evidence, version, statistics,
intake, workflow, and agent files are single-writer surfaces. Before every
`dotnet build` or `dotnet test`, increment the manual Build component in
`Directory.Build.props` and record it in `pr-evidence.md`.

## Phase 1: Setup and Evidence Foundation

- [X] T001 Verify branch `018-editor-help-resources-hardening`, `.specify/feature.json`, `specify check`, preset versions, clean baseline, binding intake, AGENTS, and Constitution in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T002 Create the autonomous run header, `MergeAndSync` authority, scope firewall, convergence ledger, and validation ledger in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T003 Create the six-area framework decision matrix with editor, file, help, compiler, resources, and i18n rows in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T004 Create malformed-state rows for truncation, trailing data, unknown type, cycles, invalid counts, duplicate keys, invalid references, malformed source, and missing resources in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T005 Create the governance table for all six preset versions and every required applicability/re-evaluation field in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T006 Record Feature 004 baseline evidence and classify already-proven contracts without claiming new proof in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T007 Review historical editor files `tv203s/contrib/tvision/classes/teditor.cc`, `teditorf.cc`, `teditwin.cc`, and `include/tv/editors.h` read-only and record intent in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T008 Review historical help/compiler files `tv203s/contrib/tvision/classes/help.cc`, `helpbase.cc`, `include/tv/help.h`, `include/tv/helpbase.h`, `examples/tvhc/tvhc.cc`, `tvhc.h`, and `demohelp.txt` read-only and record intent in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T009 Review historical resource/i18n files `tv203s/contrib/tvision/include/tv/resource.h`, `doc/I18n.txt`, and `examples/i18n/` read-only and record adopted intent and exclusions in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T010 Verify all feature checklists contain zero incomplete items and record Specify/Clarify/Checklist/Plan convergence in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 2: Foundational Contracts and Limits

- [X] T011 Reconcile public names, grammar limits, diagnostic codes, and lookup convention between `specs/018-editor-help-resources-hardening/data-model.md` and `contracts/hardening-contracts.md`
- [X] T012 Define test fixtures and input limits for the compiler in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs` without implementing production behavior
- [X] T013 Define test fixtures for exact/fallback resource selection in `tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs` without implementing production behavior
- [X] T014 Define coherent temporary-file editor and persisted-help fixture boundaries in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs` without implementing new behavior
- [X] T015 Record the expected initial missing/failing proof for T012-T014 without running a build/test in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 3: User Story 3 - Shared Help Compiler Vertical Slice (Priority: P2, Reference Slice)

**Goal**: Compile two topics with a forward reference into the runtime model and
prove persistence plus navigation before broader hardening.

**Independent test**: One source compiles deterministically, round-trips through
the existing resource/stream path, opens in the runtime help viewer, follows the
reference, and returns.

- [X] T016 [US3] Add a failing two-topic forward-reference compilation test in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T017 [US3] Add a failing deterministic symbol/context/result-shape test in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T018 [US3] Add a failing persisted resource round-trip test for compiled help in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T019 [US3] Increment the manual build counter and run the focused Serialization Release tests to capture the red/missing vertical-slice proof in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T020 [US3] Implement documented compiler diagnostic and result value types in `src/TuiVision.Serialization/THelpSourceCompiler.cs`
- [X] T021 [US3] Implement `.topic` declaration parsing, explicit/sequential contexts, and symbol collection in `src/TuiVision.Serialization/THelpSourceCompiler.cs`
- [X] T022 [US3] Implement paragraph/preformatted body collection and inline `{text[:alias]}` reference parsing in `src/TuiVision.Serialization/THelpSourceCompiler.cs`
- [X] T023 [US3] Implement forward-reference resolution and complete `THelpFile` publication only on success in `src/TuiVision.Serialization/THelpSourceCompiler.cs`
- [X] T024 [US3] Add selective German-first/English-second didactic comments for parser state, deferred resolution, and atomic publication in `src/TuiVision.Serialization/THelpSourceCompiler.cs`
- [X] T025 [US3] Increment the manual build counter and run focused Serialization Release tests for the green compiler slice, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T026 [US3] Add runtime viewer navigation over the compiled/round-tripped model in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T027 [US3] Increment the manual build counter and run focused Controls Release tests for compiled-help navigation, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T028 [US3] Complete compiler decision, historical deviation, positive proof, and residual-risk rows in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 4: User Story 3 - Help Compiler Hardening (Priority: P2)

- [X] T029 [US3] Add duplicate symbol and duplicate context rejection tests in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T030 [US3] Add malformed topic, invalid/overflow context, missing symbol, and body-before-topic tests in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T031 [US3] Add malformed reference, empty visible text/target, and unresolved-reference tests in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T032 [US3] Add null, empty, strict invalid UTF-8 stream, CRLF/LF, no-final-newline, deterministic first-symbol title, and configured input-limit tests in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T033 [US3] Add atomic failure tests proving no help model or partial symbol map on every compiler error in `tests/TuiVision.Serialization.Tests/THelpSourceCompilerTests.cs`
- [X] T034 [US3] Complete compiler validation, stable diagnostic ordering/codes, and bounded input handling in `src/TuiVision.Serialization/THelpSourceCompiler.cs`
- [X] T035 [US3] Increment the manual build counter and run focused Serialization Release tests for all compiler paths, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T036 [US3] Update the compiler contract with any implementation-proven limit while preserving accepted scope in `specs/018-editor-help-resources-hardening/contracts/hardening-contracts.md`

## Phase 5: User Story 4 - Language-Aware Resource Lookup (Priority: P2)

**Goal**: Select exact, ordered fallback, neutral, or missing resources without
ambient locale state or loss of exact-key semantics.

**Independent test**: A persisted catalog proves exact language, ordered
fallback, neutral, empty valid value, missing result, and case distinction.

- [X] T037 [US4] Add failing exact-language and matched-key tests in `tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs`
- [X] T038 [US4] Add failing ordered explicit fallback and duplicate-candidate suppression tests in `tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs`
- [X] T039 [US4] Add failing neutral, missing-versus-empty, and attempted-key tests in `tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs`
- [X] T040 [US4] Add failing case-sensitive, invalid base-key, invalid language-tag, and wrong-type tests in `tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs`
- [X] T041 [US4] Add failing save/reload selection proof using existing registrations in `tests/TuiVision.Serialization.Tests/TLocalizedResourceLookupTests.cs`
- [X] T042 [US4] Increment the manual build counter and run focused Serialization Release tests to capture the missing lookup proof in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T043 [US4] Implement documented request/result contracts and deterministic candidate construction in `src/TuiVision.Serialization/TLocalizedResourceLookup.cs`
- [X] T044 [US4] Implement typed exact-key lookup over existing `TResourceFile` without ambient locale or catalog mutation in `src/TuiVision.Serialization/TLocalizedResourceLookup.cs`
- [X] T045 [US4] Add selective German-first/English-second didactic comments for explicit fallback policy and missing/empty separation in `src/TuiVision.Serialization/TLocalizedResourceLookup.cs`
- [X] T046 [US4] Increment the manual build counter and run focused Serialization Release tests for all localized lookup paths, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T047 [US4] Complete resources/i18n decisions, historical gettext boundary, proof, and residual-risk rows in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 6: User Story 1 - Safe Editor Application Path (Priority: P1)

**Goal**: Prove the Feature 004 editor/file components as one coherent safe
application path and fix only demonstrated narrow gaps.

**Independent test**: Temporary documents complete open/edit/search/replace/save,
safe-close cancellation, external conflict, and failed-save recovery.

- [X] T048 [US1] Add coherent open-edit-search-replace-save assertions in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T049 [US1] Add title, path, line-ending, modified-state, and command-state coherence assertions in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T050 [US1] Add save/discard/cancel close paths and continued-editing proof in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T051 [US1] Add external-change overwrite rejection/acceptance proof in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T052 [US1] Add unwritable/failed-save proof that preserves content and modified state in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T053 [US1] Increment the manual build counter and run focused Controls Release tests for the complete editor path, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T054 [US1] Apply only test-demonstrated narrow editor/file corrections, if any, in `src/TuiVision.Controls/TFileEditor.cs` and `src/TuiVision.Controls/TEditWindow.cs`
- [X] T055 [US1] If T054 changes runtime code, increment the manual build counter and rerun focused Controls Release tests; otherwise record `UseExistingFramework` in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T056 [US1] Complete editor and file decisions, positive/negative proof, and residual-risk rows in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 7: User Story 2 - Navigable Runtime Help (Priority: P1)

**Goal**: Prove persisted lookup, event-facing navigation, backtracking, fallback,
and invalid-reference boundaries through existing runtime controls.

- [X] T057 [US2] Add persisted known-context and viewer/window presentation proof in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T058 [US2] Add selected-reference activation and back-navigation proof in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T059 [US2] Add missing-context fallback and continued-usability proof in `tests/TuiVision.Controls.Tests/EditorHelpEndToEndTests.cs`
- [X] T060 [US2] Add invalid persisted reference rejection/presentation-boundary proof in `tests/TuiVision.Serialization.Tests/SerializationHardeningEndToEndTests.cs`
- [X] T061 [US2] Increment the manual build counter and run focused Controls and Serialization Release tests for runtime help, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T062 [US2] Apply only test-demonstrated narrow help corrections, if any, in `src/TuiVision.Controls/THelpViewer.cs`, `src/TuiVision.Controls/THelpWindow.cs`, or `src/TuiVision.Serialization/THelpFile.cs`
- [X] T063 [US2] If T062 changes runtime code, increment the manual build counter and rerun affected focused Release tests; otherwise record `UseExistingFramework` in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T064 [US2] Complete help decision, navigation/fallback proof, and residual-risk rows in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 8: User Story 5 - Hard Persistence Failures (Priority: P2)

**Goal**: Demonstrate explicit atomic rejection across all required malformed
stream, resource, help, compiler, and lookup classes.

- [X] T065 [US5] Map existing truncation, trailing-data, unknown-type, cycle, and invalid-count tests to requirements in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T066 [US5] Add only missing duplicate persisted-key or invalid help-reference atomicity tests in `tests/TuiVision.Serialization.Tests/SerializationHardeningEndToEndTests.cs`
- [X] T067 [US5] Add an end-to-end malformed resource/help load test proving no partial accepted graph in `tests/TuiVision.Serialization.Tests/SerializationHardeningEndToEndTests.cs`
- [X] T068 [US5] Increment the manual build counter and run focused Serialization Release tests for the complete malformed-state matrix, recording result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T069 [US5] Apply only test-demonstrated narrow persistence corrections, if any, in the affected file under `src/TuiVision.Serialization/`
- [X] T070 [US5] If T069 changes runtime code, increment the manual build counter and rerun affected focused Release tests; otherwise record existing proof reuse in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T071 [US5] Complete every malformed-state row with command, assertion boundary, result, atomicity proof, and follow-up in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 9: Cross-Cutting Documentation and Governance

- [X] T072 Review all new/changed non-trivial logic for Feature 015 didactic-comment adequacy and record decisions in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T073 Verify complete public XML documentation and no API beyond the accepted compiler/lookup contracts in `src/TuiVision.Serialization/THelpSourceCompiler.cs` and `src/TuiVision.Serialization/TLocalizedResourceLookup.cs`
- [X] T074 Complete NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC, malformed-input, path, resource-exhaustion, and atomic-output evidence in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T075 Complete ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, and DORA applicability with triggers in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T076 Complete S-ADR, arc42, Zero Trust, SAMM, BSI C3A, BSI C5, cross-platform script, and `.specify/templates/` applicability with triggers in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T077 Complete A11Y, bilingual CEFR-B2, text-first, keyboard-help, XML/DocFX, and agent-parity evidence in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T078 Update relevant editor/help/resource readiness and next-Wave-3 guidance in `Pflichtenheft.md` and any affected existing guide without porting examples
- [X] T079 Update active/completed feature context and next intake consistently in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [X] T080 Archive `Lastenheft_03_EditorHelpAndResourcesHardening.md` as `Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md`
- [X] T081 Update the implementation row, work window, line counts, packages, baselines, trend data, and final `Gesamtstatistik` position in `docs/project-statistics.md`

## Phase 10: Validation

- [X] T082 Run `git diff --check`, placeholder scan, secret scan, generated-output scan, `tv203s/` scope scan, dependency diff scan, and executable-scope review; record results in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T083 Run `dotnet format --verify-no-changes` and record result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T084 Increment the manual build counter and run final focused Serialization and Controls Release tests, recording version, counts, duration, and result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T085 Increment the manual build counter and run full `dotnet test --configuration Release`, recording count, duration, and result in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T086 Run `xmllint --noout coverlet.runsettings`, increment the manual build counter, run the canonical Coverlet command, and record per-assembly line coverage in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T087 Run `docfx docfx.json`, remove generated output from the worktree, and record warnings/errors in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T088 Run the matching `tests/web-a11y` Playwright/axe DocFX smoke and text-oriented representative-page review, recording results in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T089 Re-run every feature checklist instruction and mark any implementation-readiness checklist added during the run complete in `specs/018-editor-help-resources-hardening/checklists/`
- [X] T090 Verify SC-001 through SC-010, all six decisions, all malformed-state rows, governance rows, follow-ups, and no forbidden scope in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T091 Record the retrospective observations, classification candidates, resume state, and Home-Baseline handoff recommendation in `specs/018-editor-help-resources-hardening/pr-evidence.md`

## Phase 11: Authorized Delivery and Sync

- [X] T092 Align `Directory.Build.props` to `1.18.<post-commit-patch>.<current-build>` without incrementing Build and verify all three fields match before commit
- [X] T093 Stage the intentional feature diff, verify no forbidden files, commit the complete 018 implementation with a Spec-Kit message, and record the commit in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [X] T094 Recalculate the post-commit patch, align `Directory.Build.props` if required, commit version alignment only when the prior commit count changed the required value, and verify clean staged scope
- [ ] T095 Push `018-editor-help-resources-hardening` and create a ready feature PR using `pr-evidence.md` as the description source
- [ ] T096 Monitor required CI, Claude/Copilot review availability, review comments, and GraphQL thread state to convergence; remediate actionable findings and record state in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [ ] T097 Use the explicitly authorized narrow admin bypass only if all required checks are green, actionable threads are zero, and the sole remaining block is the human-approval rule; record the exact reason in `specs/018-editor-help-resources-hardening/pr-evidence.md`
- [ ] T098 Merge with a merge commit, delete the remote feature branch, switch locally to `main`, fetch/prune/pull fast-forward, and prove clean `HEAD == origin/main`
- [ ] T099 Record truthful post-merge facts in `specs/018-editor-help-resources-hardening/pr-evidence.md` through a non-empty closeout PR only if they could not be recorded before merge; otherwise explicitly record no closeout PR needed

## Dependencies and Story Order

- Setup and foundation (T001-T015) block all implementation.
- US3 reference slice (T016-T028) proves the compiler/runtime architecture.
- US3 hardening (T029-T036) completes compiler failure contracts.
- US4 (T037-T047) is independent in behavior but follows US3 to serialize
  shared Serialization tests, evidence, and version writes.
- US1 (T048-T056) and US2 (T057-T064) reuse existing Controls components and
  follow the reference model proof.
- US5 (T065-T071) consolidates all negative boundaries after preceding slices.
- Cross-cutting, validation, and delivery (T072-T099) require all stories.

## Requirement Coverage

| Requirement group | Primary task IDs |
|---|---|
| FR-001-FR-003 foundation/decisions | T001-T011, T028, T047, T056, T064 |
| FR-004-FR-006 editor/file | T048-T056 |
| FR-007 help runtime | T026-T028, T057-T064 |
| FR-008-FR-010 compiler | T016-T036 |
| FR-011-FR-012 i18n/resources | T037-T047 |
| FR-013-FR-014 malformed atomicity | T029-T035, T065-T071 |
| FR-015-FR-018 remediation model | T003, T028, T047, T056, T064, T090 |
| FR-019 historical evidence | T007-T009 |
| FR-020 evidence trace | T002-T006, T071, T090 |
| FR-021-FR-022 comments/A11Y/docs | T024, T045, T072-T078, T087-T088 |
| FR-023-FR-024 shared completion | T079-T081 |
| FR-025-FR-026 scope firewall | T082, T090 |
| CR-001-CR-013 governance | T001, T005, T073-T077, T082-T090 |
| SC-001-SC-010 acceptance | T084-T091 |

## Parallel Execution

No implementation task is marked `[P]`. Although some source files differ,
every slice writes shared `pr-evidence.md` and `Directory.Build.props`, and all
Serialization slices share test/runtime contracts. Serial execution gives a
clear build-counter and proof history. Read-only review commands may be batched
internally when they do not mutate shared state.

## Implementation Strategy

1. Establish evidence and current baseline.
2. Complete one test-first compiler-to-runtime vertical slice.
3. Harden compiler errors, then language resource lookup.
4. Prove editor/help integration using existing components.
5. Consolidate malformed-state proof and apply only narrow demonstrated fixes.
6. Complete governance/docs, full validation, and authorized MergeAndSync.
