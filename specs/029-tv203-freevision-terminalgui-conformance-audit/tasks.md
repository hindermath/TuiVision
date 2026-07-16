# Tasks: TV203, Free Vision, and Terminal.GUI Conformance Audit

**Input**: Accepted artifacts under `specs/029-tv203-freevision-terminalgui-conformance-audit/`
**Delivery**: `MergeAndSync` with evidence in `pr-evidence.md`

## Phase 1: Setup and Run Foundation

- [X] T001 Create `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md` from `.specify/templates/autonomous-run-evidence-template.md` with concrete Feature-029 scope, `MergeAndSync` authority, run gates, source, relation, consumer, observation, governance, validation, and remote-delivery tables
- [X] T002 Validate `specs/029-tv203-freevision-terminalgui-conformance-audit/autonomous-run-state.json` and record branch, feature metadata, accepted-artifact hashes, resume result, owned changes, and no-drift decision in `pr-evidence.md`
- [X] T003 Run `specify check` and `.specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks`; record exact paths, exit status, and error-channel review in `pr-evidence.md`
- [X] T004 Verify every checklist under `specs/029-tv203-freevision-terminalgui-conformance-audit/checklists/` has zero incomplete items and record counts in `pr-evidence.md`
- [X] T005 Create `specs/029-tv203-freevision-terminalgui-conformance-audit/autonomous-gate-requirements.json` from the installed v0.2.1 template with stable local, documentation, security, platform, review, and exact-head gates
- [X] T006 Validate the committed gate requirements shape and record its path and initial hash boundary in `pr-evidence.md`
- [X] T007 Record the complete seven-preset applicability inventory and all trigger-based `N/A` families in `pr-evidence.md`
- [X] T008 Record the intended candidate path inventory, protected source roots, generated-output exclusions, and external-checkout boundary in `pr-evidence.md`
- [X] T009 Record shared single-writer files and the no-parallel-write rule for dataset, handoff, evidence, version, status, agent, archive, and statistics paths in `pr-evidence.md`
- [X] T010 Record the build-counter and `1.29.<patch>.<build>` versioning contract before any build or test in `pr-evidence.md`
- [X] T011 Review complete compile surfaces for the new test validator: imports, MSTest APIs, System.Text.Json helpers, repository-root discovery, proof path parsing, didactic comments, and assembly references; record the result in `pr-evidence.md`
- [X] T012 Record the Feature-024, 025, 026, and 028 immutable input files and their current hashes in `pr-evidence.md`

## Phase 2: Foundational Source and Schema Baseline

- [X] T013 Verify Terminal.GUI repository, tag `v1.9.0`, annotated tag object, peeled commit, and MIT license in the external checkout; record immutable command evidence in `pr-evidence.md`
- [X] T014 Record the external checkout path as temporary/untracked and prove no path under it appears in Git status or tracked files in `pr-evidence.md`
- [X] T015 Inventory selected Terminal.GUI production and UnitTests files across all 16 domains and reserve stable `TGSR###` IDs in `terminalgui-source-manifest.md`
- [X] T016 Calculate SHA-256 values for every selected Terminal.GUI source and license record and record them in `terminalgui-source-manifest.md`
- [X] T017 Add own-word German-first/English-second behavior and no-copy summaries for all selected source records in `terminalgui-source-manifest.md`
- [X] T018 Add `tests/TuiVision.Drivers.Tests/TerminalGuiConformanceEvidenceTests.cs` with dataset existence and exact run/source pin tests before creating either accepted JSON dataset
- [X] T019 Align `Directory.Build.props` to the current `1.29.<patch>.<build>` branch version and increment the manual build counter once for the red targeted test
- [X] T020 Run the targeted Release filter for `TerminalGuiConformanceEvidenceTests`, require the missing-dataset red boundary, and record command, counter, failure, and proof purpose in `pr-evidence.md`
- [X] T021 Create the initial closed-shape root, run metadata, source array, relation array, consumer array, observation array, governance array, and validation array in `terminalgui-conformance-audit.json`
- [X] T022 Create the initial closed-shape Feature-030 metadata, contract IDs, observation IDs, owner groups, dependency edges, deduplication keys, false follow-up-document flags, blocked Waves, and next-intake fields in `feature030-handoff.json`
- [X] T023 Extend the test-only validator with closed vocabulary, duplicate ID, exact contract/domain set, reciprocal relation, path/hash, observation ownership, DAG, and handoff reconciliation helpers
- [X] T024 Add test cases that reject malformed JSON, unknown values, wrong pins, invalid hashes, missing paths, duplicate/orphan links, cycles, and handoff disagreement in `TerminalGuiConformanceEvidenceTests.cs`
- [X] T025 Review non-trivial validator blocks for concise German-first/English-second didactic comments explaining why relation, proof, or handoff boundaries fail closed

## Phase 3: User Story 1 - Existing Contract Review

**Goal**: Every existing contract has exactly one complete Terminal.GUI relation.

**Independent test**: The validator accepts exactly `C001`-`C048`, `D01`-`D16`, reciprocal source links, valid TuiVision proof, and one allowed relation per contract.

- [X] T026 [US1] Add D02 source records for Application, MainLoop, Responder, ConsoleDriver, FakeDriver, and relevant UnitTests to `terminalgui-conformance-audit.json`
- [X] T027 [US1] Add complete `C004`-`C006` Terminal.GUI relation rows with current TuiVision proof and consumer links to `terminalgui-conformance-audit.json`
- [X] T028 [US1] Add the D02 vertical-slice rows to `terminalgui-contract-matrix.md`
- [X] T029 [US1] Align `Directory.Build.props` and increment the build counter once for the D02 green targeted test
- [X] T030 [US1] Run only the D02-specific targeted Feature-029 test, require that slice green, and record that global 48-contract completeness remains deliberately pending until T050
- [X] T031 [US1] Review D01 contracts `C001`-`C003` against pinned Terminal.GUI base types, geometry/lifecycle evidence, and existing TuiVision proof; add complete relation rows to JSON and Markdown
- [X] T032 [US1] Review D03 contracts `C007`-`C009` against Responder/View/Toplevel focus and lifecycle evidence; add complete relation rows to JSON and Markdown
- [X] T033 [US1] Review D04 contracts `C010`-`C012` against Pos/Dim/View layout, clipping, and resize evidence; add complete relation rows to JSON and Markdown
- [X] T034 [US1] Review D05 contracts `C013`-`C015` against Application/MainLoop/Toplevel/Window/modal evidence; add complete relation rows to JSON and Markdown
- [X] T035 [US1] Review D06 contracts `C016`-`C018` against Menu, StatusBar, shortcut, and help-description evidence; add complete relation rows to JSON and Markdown
- [X] T036 [US1] Review D07 contracts `C019`-`C021` against Dialog, controls, and TextValidateField evidence; add complete relation rows to JSON and Markdown
- [X] T037 [US1] Review D08 contracts `C022`-`C024` against TextField/TextView, Clipboard, FileDialog, and editor/file evidence; add complete relation rows to JSON and Markdown
- [X] T038 [US1] Review D09 contracts `C025`-`C027` against resource, localization, and help-relevant evidence; add complete relation rows to JSON and Markdown
- [X] T039 [US1] Review D10 contracts `C028`-`C030` against persistence/registry applicability and record justified `NotApplicable` or alternative-modernization rows in JSON and Markdown
- [X] T040 [US1] Review D11 contracts `C031`-`C033` against ConsoleDriver/FakeDriver rendering, clipping, and cell proof evidence; add complete relation rows to JSON and Markdown
- [X] T041 [US1] Review D12 contracts `C034`-`C036` against key, mouse, capture, responder, and input evidence; add complete relation rows to JSON and Markdown
- [X] T042 [US1] Review D13 contracts `C037`-`C039` against ConsoleDriver charset, glyph, and terminal capability evidence; add complete relation rows to JSON and Markdown
- [X] T043 [US1] Review D14 contracts `C040`-`C042` against driver, platform, clipboard, and fallback evidence; add complete relation rows to JSON and Markdown
- [X] T044 [US1] Review D15 contracts `C043`-`C045` against focus, key, status/text, and accessibility responsibility evidence; add complete relation rows to JSON and Markdown
- [X] T045 [US1] Review D16 contracts `C046`-`C048` against UnitTests, FakeDriver, helper, and proof-boundary evidence; add complete relation rows to JSON and Markdown
- [X] T046 [US1] Reconcile exact `C001`-`C048` and `D01`-`D16` sets, one relation per contract, and reciprocal `TGSR###` links in `terminalgui-conformance-audit.json`
- [X] T047 [US1] Record `NotApplicable` rationale and re-evaluation trigger for every relation without a useful Terminal.GUI comparison surface
- [X] T048 [US1] Execute the five C049+ admission checks and record either complete new contract rows or an explicit zero-new-contract result in JSON, matrix, and `pr-evidence.md`
- [X] T049 [US1] Align `Directory.Build.props` and increment the build counter once for the complete contract validator
- [X] T050 [US1] Run the targeted Feature-029 validator and require all contract, domain, source, proof, relation, and C049+ gates green; record totals in `pr-evidence.md`

## Phase 4: User Story 2 - Consumer and Proof Review

**Goal**: Every accepted Wave-5/Wave-6 consumer group has current contract, proof, Terminal.GUI, risk, and decision evidence.

**Independent test**: All six Wave-5 and seven Wave-6 baseline rows are present with valid source paths, contracts, proofs, decisions, and no consumer-source edit.

- [X] T051 [US2] Re-read the six accepted Wave-5 consumer groups in `TVDEMOS/` and map current contracts, proof, and Terminal.GUI source relations in `terminalgui-consumer-review.md`
- [X] T052 [US2] Re-read the seven accepted Wave-6 consumer groups in `TVFM/` and map current contracts, proof, and Terminal.GUI source relations in `terminalgui-consumer-review.md`
- [X] T053 [US2] Add exact `W5-001`-`W5-006` and `W6-001`-`W6-007` consumer rows to `terminalgui-conformance-audit.json`
- [X] T054 [US2] Preserve the destructive `FILECOPY.PAS`/`TRASH.PAS` product-policy boundary without converting it into a framework finding
- [X] T055 [US2] Review Application/MainLoop/menu/status consumers against Terminal.GUI evidence and current TuiVision real-path proof
- [X] T056 [US2] Review focus/window/desktop/modal consumers against Terminal.GUI evidence and current TuiVision real-path proof
- [X] T057 [US2] Review dialog/validation/file/editor consumers against Terminal.GUI evidence and current TuiVision real-path proof
- [X] T058 [US2] Review resource/localization/help consumers against Terminal.GUI evidence and current TuiVision proof or justified comparison boundary
- [X] T059 [US2] Review rendering/input/mouse/driver/platform consumers against Terminal.GUI evidence and current TuiVision real-path proof
- [X] T060 [US2] Review A11Y and helper-proof consumers against Terminal.GUI UnitTests/FakeDriver evidence and current TuiVision proof boundaries
- [X] T061 [US2] Decide whether any new shared-framework consumer row exists; add only a uniquely identified material row or record zero additions
- [X] T062 [US2] Prove all consumer source paths exist and no path under `TVDEMOS/` or `TVFM/` changed; record hashes or tree-diff evidence in `pr-evidence.md`
- [X] T063 [US2] Align `Directory.Build.props` and increment the build counter once for the complete consumer validator
- [X] T064 [US2] Run the targeted Feature-029 validator and require exact baseline consumer cardinality, valid links, and protected-source boundaries green
- [X] T065 [US2] Record the complete consumer decision totals, residual risks, and follow-up boundaries in `pr-evidence.md`

## Phase 5: User Story 3 - Reproducible Terminal.GUI Evidence

**Goal**: Every used upstream observation is reproducibly pinned without copied source.

**Independent test**: Manifest paths and hashes match the external v1.9.0 checkout, license evidence is exact, and no external content is tracked.

- [X] T066 [US3] Reconcile every `TGSR###` JSON record with `terminalgui-source-manifest.md`
- [X] T067 [US3] Add pinned commit permalinks for selected production and UnitTests paths to `terminalgui-source-manifest.md`
- [X] T068 [US3] Verify each manifest SHA-256 against the external checkout and record zero mismatch in `pr-evidence.md`
- [X] T069 [US3] Verify the license SHA-256 and own-word no-copy summaries in JSON, manifest, and `pr-evidence.md`
- [X] T070 [US3] Scan tracked Feature-029 files for copied multi-line Terminal.GUI source or foreign fixtures and record the no-copy result
- [X] T071 [US3] Verify Terminal.GUI v2, later revisions, and magiblot/tvision references appear only as excluded-scope text, not evidence rows
- [X] T072 [US3] Verify the external checkout path, Git objects, and caches are absent from the staged-candidate inventory
- [X] T073 [US3] Align `Directory.Build.props` and increment the build counter once for the source/hash validator
- [X] T074 [US3] Run the targeted Feature-029 validator and require exact pin, license, path, hash, reciprocal-link, and no-copy metadata gates green
- [X] T075 [US3] Record provenance result, retrieval date, source count, and residual upstream-availability risk in `pr-evidence.md`

## Phase 6: User Story 4 - Findings and Feature-030 Handoff

**Goal**: Every observation is classified and the complete non-speculative handoff is ready for Feature 030.

**Independent test**: Observation decisions, ownership, dependencies, deduplication, contract links, and handoff totals reconcile with no premature Lastenheft.

- [X] T076 [US4] Review every `DivergesFromTuiVision` or risk-bearing relation for a reproducible TuiVision contract, consumer, security, A11Y, platform, or real-path proof gap
- [X] T077 [US4] Create `TG###` candidate findings only for reproduced gaps and populate every intake-defined field in `terminalgui-conformance-audit.json`
- [X] T078 [US4] Record every non-finding observation as `IntentionalDeviation`, `AlreadySatisfiedWithNewEvidence`, or `RejectedComparison` with complete rationale
- [X] T079 [US4] Stop and record `ProductDecision` if any destructive or breaking owner decision is required; otherwise record zero open ProductDecision
- [X] T080 [US4] Assign exactly one Primary Owner and bounded suggested work boundary to every observation
- [X] T081 [US4] Add observation dependencies and prove the graph is acyclic
- [X] T082 [US4] Assign stable unique deduplication keys to every finding and non-finding observation
- [X] T083 [US4] Create `terminalgui-findings.md` with bilingual finding/non-finding decisions, proof boundaries, owners, risks, and Feature-030 disposition
- [X] T084 [US4] Complete `feature030-handoff.json` from the final observation, owner, dependency, proof, and deduplication data
- [X] T085 [US4] Create `feature030-handoff.md` with readable totals, owner groups, dependency order, proof needs, and no-premature-follow-up statement
- [X] T086 [US4] Align `Directory.Build.props` and increment the build counter once for the observation and handoff validator
- [X] T087 [US4] Run the targeted Feature-029 validator and require observation, owner, DAG, deduplication, false Lastenheft flags, blocked Waves, and next-intake reconciliation green

## Phase 7: User Story 5 - Gate, Governance, and Status

**Goal**: Feature 029 closes consistently without releasing either Wave or starting Feature 030.

**Independent test**: Governance rows are complete, status surfaces agree, Feature 030 is next, both Waves are blocked, and no forbidden scope changed.

- [X] T088 [US5] Create `pre-wave-gate.md` with the Feature-029 local result, blocker model, Feature-030 next intake, and blocked Wave states
- [X] T089 [US5] Add complete governance rows for all seven installed presets to `terminalgui-conformance-audit.json`
- [X] T090 [US5] Record NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC, iSAQB quality/risk, A11Y, portable data/path, agent parity, and autonomous governance as `Applicable`
- [X] T091 [US5] Record ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, DORA, Zero Trust, SAMM, BSI C3A, BSI C5, S-ADR/arc42, and script parity as trigger-based `N/A` unless final scope changes
- [X] T092 [US5] Create `docs/guides/terminalgui-conformance-audit.md` with DE-first/EN-second CEFR-B2 method, sources, relations, findings, proof boundaries, no-copy rule, and next-intake explanation
- [X] T093 [US5] Update `Pflichtenheft.md` so Feature 029 is completed, Feature 030 is the sole next step, and Wave 5/Wave 6 remain blocked
- [X] T094 [US5] Update processing-order documentation to the same completed/next/blocked state
- [X] T095 [US5] Update the shared Feature-029 completion and Feature-030 next-intake context identically across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [X] T096 [US5] Verify `.specify/templates/` remains unchanged and record `N/A` unless a reusable template need is proven
- [X] T097 [US5] Update `docs/project-statistics.md` with the Feature-029 artifact and line totals while preserving the final `## Gesamtstatistik` section
- [X] T098 [US5] Run `bash scripts/rename-lastenheft.sh --dry-run Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md 029-tv203-freevision-terminalgui-conformance-audit` and `pwsh -NoProfile -File scripts/rename-lastenheft.ps1 -File Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md -BranchName 029-tv203-freevision-terminalgui-conformance-audit -WhatIf`, compare target paths and error channels, then run the same Bash command with `--no-commit` instead of `--dry-run` exactly once
- [X] T099 [US5] Record archive, status, agent parity, statistics, and next-intake results in `pr-evidence.md`
- [X] T100 [US5] Verify no Feature-030 directory/branch, no C049+ without admission, and no hardening/closure Lastenheft was created

## Phase 8: Validation and Delivery

- [X] T101 Run `git diff --check`, Markdown heading/fence/UTF-8 review, JSON parse checks, marker scans, and record results in `pr-evidence.md`
- [X] T102 Run `dotnet format --verify-no-changes --no-restore` and record the result in `pr-evidence.md`
- [X] T103 Align `Directory.Build.props` and increment the build counter once for the final targeted Drivers Release test
- [X] T104 Run targeted Drivers Release tests covering Feature-024, Feature-028, and Feature-029 evidence validators; record totals in `pr-evidence.md`
- [X] T105 Align `Directory.Build.props` and increment the build counter once for the full Release test
- [X] T106 Run `dotnet test TuiVision.sln --configuration Release --no-restore` and record totals in `pr-evidence.md`
- [X] T107 Validate `coverlet.runsettings` with `xmllint --noout` and record the result
- [X] T108 Align `Directory.Build.props` and increment the build counter once for the canonical coverage invocation
- [X] T109 Run canonical Coverlet coverage, calculate all five mandatory assembly percentages, and require each at least 70 percent
- [X] T110 Run `docfx docfx.json`, require zero warnings/errors, and record the result
- [X] T111 Ensure `tests/web-a11y` dependencies and Chromium are available without tracking generated dependencies
- [X] T112 Run `npm run test:docfx` under `tests/web-a11y` and record Playwright/Axe results
- [X] T113 Review representative Feature-029 generated pages through UTF-8 Lynx/text output and record semantic reading order
- [X] T114 Run repository secret scans and require zero High findings; record any known local-only medium boundary
- [X] T115 Review package/dependency manifests and supply-chain workflow input; require zero new dependency or package change
- [X] T116 Verify no generated `_site/`, `api/*.yml`, bin/obj, TestResults, logs, caches, credentials, or external checkout is tracked
- [X] T117 Verify zero diff under `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/`, package manifests, and external sources
- [X] T118 Verify five-agent guidance parity, Feature-030 next-intake parity, Wave-block parity, and zero duplicate autonomous skills
- [X] T119 Complete all validation and governance rows plus the retrospective section in `pr-evidence.md`
- [X] T120 Verify T001-T119, all 48 relations, all domains, consumers, observations, governance rows, handoff, SC metrics, and checklists are complete
- [X] T121 Prepare the concise PR title and body from `pr-evidence.md` without recursive post-merge claims
- [X] T122 Align `Directory.Build.props` to the pre-commit `1.29.<patch>.<build>` values without incrementing build and stage only intended Feature-029 files
- [X] T123 Run `git diff --cached --check`, compare staged inventory with repository status, and prove no intended untracked or unstaged file remains outside the candidate
- [X] T124 Commit the validated Feature-029 candidate with repository trailer policy and verify the commit externally
- [X] T125 Push the version-aligned `029-tv203-freevision-terminalgui-conformance-audit` commit and verify local/remote head parity
- [X] T126 Create a ready Feature-029 PR, verify base/head/no-empty diff, and use `pr-evidence.md` as the declared remote evidence path
- [X] T127 Wait for required PR-context checks, map each gate to actual workflow/job/platform/executed command, and validate temporary exact-head evidence with both installed validators
- [X] T128 Query Claude, Copilot, PR comments, and GraphQL review threads; remediate every actionable finding and repeat affected validation until convergence
- [X] T129 Merge with a merge commit using the narrow admin bypass only under the authorized conditions, delete the remote branch, switch to `main`, pull/prune, and prove clean `HEAD == origin/main`
- [X] T130 Run the autonomous retrospective, record the documentation-learning `PresetFollowUp`, and create a non-recursive closeout only if post-merge truth cannot otherwise be recorded

## Dependencies

- Phase 1 blocks every later phase.
- Phase 2 establishes the source IDs, schema, red proof, and validator used by all stories.
- US1 supplies contract relations required by US2 and US4.
- US2 can complete only after relevant US1 contract rows exist.
- US3 revalidates the source records used by US1 and US2.
- US4 consumes the final US1-US3 evidence and produces the Feature-030 handoff.
- US5 consumes the final handoff and may update status only after local audit acceptance.
- Phase 8 begins only after US1-US5 and every checklist pass.

## Parallel Execution

No task is marked `[P]`. Source review can be investigated concurrently in
principle, but accepted writes are serialized because the same JSON,
Markdown, evidence, status, version, or agent files are shared.

## Implementation Strategy

1. Establish evidence and gate requirements.
2. Prove the missing-dataset red boundary.
3. Complete D02/C004-C006 as the representative green slice.
4. Expand relation coverage domain by domain.
5. Add consumers, provenance closure, observations, and handoff.
6. Synchronize governance and status only after audit acceptance.
7. Validate the exact candidate, deliver through PR, merge, sync, and then
   promote the separate documentation patch before Feature 030.
