# Tasks: Gemeinsamer Konformitätsabschluss

**Input**: Alle akzeptierten Artefakte unter
`specs/031-combined-conformance-closure/` sowie die bindenden
Vorgängerartefakte der Features 024, 025, 026, 028, 029 und 030
**Delivery mode**: `MergeAndSync`
**Scope**: Evidence-only Closure mit test-only Validierung

## Phase 1: Setup und Evidence-Grundlage

**Purpose**: Laufidentität, Autorität, Scope und wiederaufnahmefähige Evidence
vor jeder Implementierungsänderung absichern.

- [X] T001 Verify branch `031-combined-conformance-closure`, `.specify/feature.json`, clean ownership, and synchronized base in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T002 Verify Feature-030 PRs #88 and #89, completed 165/165 run state, and merge ancestry in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T003 Run `specify check` and repository prerequisites and record exit status plus error-channel review in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T004 Verify the seven installed preset names, versions, and priorities in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T005 Confirm 115/115 completed checklist items and zero incomplete items under `specs/031-combined-conformance-closure/checklists/`
- [X] T006 Validate `specs/031-combined-conformance-closure/autonomous-run-state.json` with the installed Bash state validator
- [X] T007 Record honest local PowerShell availability and the remote PowerShell proof boundary in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T008 Validate `specs/031-combined-conformance-closure/autonomous-gate-requirements.json` as UTF-8 JSON and record its SHA-256 in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T009 Record all binding scope exclusions and protected read-only roots in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T010 Record shared single-writer paths and serialization rules in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T011 Record the one-build-counter-increment-per-command rule and Feature-031 version scheme in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T012 Record that no intentional interruption, Feature 032, Wave 5, or Wave 6 start is permitted in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T013 Record the non-recursive causal closeout boundary and exact evidence path `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T014 Inventory current intended and untracked feature paths in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T015 Confirm no external checkout, generated DocFX output, cache, log, credential, or test output is tracked in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T016 Update `specs/031-combined-conformance-closure/autonomous-run-state.json` to the validated Tasks checkpoint after task generation

---

## Phase 2: Foundational Closure Inputs

**Purpose**: Bind every accepted predecessor and define the closed validation
surface before user-story implementation.

- [X] T017 Record the binding Lastenheft SHA-256 in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T018 Record accepted Feature-024 artifact paths and SHA-256 values in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T019 Record accepted Feature-025 artifact paths and SHA-256 values in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T020 Record accepted Feature-026 artifact paths and SHA-256 values in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T021 Record accepted Feature-028 artifact paths and SHA-256 values in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T022 Record accepted Feature-029 artifact paths and SHA-256 values in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T023 Record accepted Feature-030 artifact paths and SHA-256 values in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T024 Add run identity, baseline commit, owner, reviewer, review date, result, risk, follow-up, and re-evaluation trigger to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T025 Add exact closed vocabularies and expected ID sets to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T026 Add the three external-source baseline skeletons to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T027 Add blocked feature-head and target post-merge Wave-state skeletons to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T028 Add seven governance row skeletons without empty starter values to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T029 Add local, remote, review, and exact-head validation row skeletons without inferred results to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T030 Review imports, MSTest 4 APIs, repository-root helpers, linked-source identity, public XML summaries, and didactic-comment needs before editing `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T031 Add the test class and repository-root loading helpers to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T032 Add closed vocabulary, exact-set, SHA-256, reciprocal-reference, and proof-path helpers to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T033 Add bounded malformed JSON, duplicate, missing, unknown, orphan, and premature-state mutation helpers to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T034 Add German-first/English-second XML summaries and moderate why-focused comments to non-trivial validation blocks in `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T035 Record the compile-surface and test-helper proof boundary in `specs/031-combined-conformance-closure/pr-evidence.md`

**Checkpoint**: The accepted inputs and test harness are defined; no product
source has changed.

---

## Phase 3: User Story 1 - Kombinierte Evidence unabhängig bestätigen (Priority: P1)

**Goal**: Exactly 48 contracts, 13 consumers, 96 observations, 13 prior
findings, and all reciprocal relationships are independently reconstructable.

**Independent Test**: The representative slice fails before its rows exist,
then passes; the complete dataset later reports the exact closed sets.

- [X] T036 [US1] Add failing `Test_RepresentativeSliceIsComplete` existence and slice proof for `C001`, `W5-001`, `TGO001`, `MB001`, and `F001` in `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T037 [US1] Increment the manual build counter in `Directory.Build.props` for the first targeted red test invocation
- [X] T038 [US1] Run `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --filter "FullyQualifiedName~CombinedConformanceClosureEvidenceTests.Test_RepresentativeSliceIsComplete"` and accept only missing or incomplete closure-dataset failures
- [X] T039 [US1] Record the red command, version, exit status, error-channel review, and failure boundary in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T040 [US1] Complete the representative contract row `C001` in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T041 [US1] Complete the representative consumer row `W5-001` in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T042 [US1] Complete the representative observation rows `TGO001` and `MB001` in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T043 [US1] Complete the representative prior-finding row `F001` in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T044 [US1] Add reciprocal proof, source, contract, consumer, and disposition links for the representative slice in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T045 [US1] Increment the manual build counter in `Directory.Build.props` for the representative green test invocation
- [X] T046 [US1] Run `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --filter "FullyQualifiedName~CombinedConformanceClosureEvidenceTests.Test_RepresentativeSliceIsComplete"` green
- [X] T047 [US1] Record the green command, version, result, and proof boundary in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T048 [US1] Complete contract rows `C002`-`C008` for lifecycle, event, command, dispatch, focus, and view ownership in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T049 [US1] Complete contract rows `C009`-`C016` for modality, desktop, windows, coordinates, layout, clipping, and resize in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T050 [US1] Complete contract rows `C017`-`C024` for DrawBuffer, cells, rendering, Unicode, width, combining, color, and palettes in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T051 [US1] Complete contract rows `C025`-`C032` for keyboard, mouse, capture, clipboard, input, drivers, terminal state, and signals in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T052 [US1] Complete contract rows `C033`-`C040` for capabilities, fallbacks, controls, menus, StatusLine, dialogs, validation, and editor flows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T053 [US1] Complete contract rows `C041`-`C048` for files, help, resources, persistence, testability, headless proof, historical intent, and closure boundaries in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T054 [US1] Complete consumer rows `W5-002`-`W5-006` in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T055 [US1] Complete consumer rows `W6-001`-`W6-007` in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T056 [US1] Complete all 48 `TGO001`-`TGO048` observation closure rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T057 [US1] Complete all 48 `MB001`-`MB048` observation closure rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T058 [US1] Reconcile all 96 observation dispositions with `specs/030-tv203-magiblot-evolution-audit/combined-conformance-findings.json`
- [X] T059 [US1] Complete prior-finding rows `F002`-`F009` with Feature-025 and Feature-028 proof links in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T060 [US1] Complete prior-finding rows `F010`-`F013` with Feature-026 and Feature-028 proof links in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T061 [US1] Add complete-set tests for contracts, consumers, observations, dispositions, and prior findings in `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T062 [US1] Add reciprocal relation and proof-path existence tests in `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T063 [US1] Add predecessor SHA-256 binding tests in `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T064 [US1] Add negative tests for missing, duplicate, unknown, orphaned, out-of-order, and contradictory rows in `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T065 [US1] Create the readable exact-cardinality and relationship report in `specs/031-combined-conformance-closure/combined-closure.md`
- [X] T066 [US1] Reconcile readable report counts with `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T067 [US1] Record US1 exact counts, decisions, and independent test result in `specs/031-combined-conformance-closure/pr-evidence.md`

**Checkpoint**: The combined accepted evidence is complete and independently
testable without relying on copied summary claims.

---

## Phase 4: User Story 2 - Externe Quellenidentitäten reproduzierbar prüfen (Priority: P2)

**Goal**: All accepted Free Vision, Terminal.GUI, and magiblot identities and
90 source hashes are reproduced outside the repository.

**Independent Test**: Exact Git objects, license hashes, manifest identities,
and source hashes match; no checkout content enters the candidate.

- [X] T068 [US2] Create or reuse a detached read-only Free Vision checkout outside TuiVision and record its path only in local operation evidence
- [X] T069 [US2] Verify Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` and all 15 accepted source hashes against `specs/024-tv203-freevision-conformance-audit/freevision-source-manifest.md`
- [X] T070 [US2] Record the Free Vision pin, manifest count, source-hash result, owner, reviewer, date, risk, and trigger in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T071 [US2] Create or reuse a detached read-only Terminal.GUI checkout outside TuiVision and record its path only in local operation evidence
- [X] T072 [US2] Verify Terminal.GUI tag object `4b812e44798f2c7567afec50ba9a9293b6beb6de` and commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`
- [X] T073 [US2] Verify Terminal.GUI MIT license SHA-256 `2a7331c273b7c121f5e1f6f10e13d279a739ac310c49b56f2fb251d0490988d0` and all 25 accepted source hashes against `specs/029-tv203-freevision-terminalgui-conformance-audit/terminalgui-source-manifest.md`
- [X] T074 [US2] Record the Terminal.GUI identities, counts, results, risk, and trigger in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T075 [US2] Create or reuse a detached read-only magiblot/tvision checkout outside TuiVision and record its path only in local operation evidence
- [X] T076 [US2] Verify magiblot commit `57b6f56b38e0ee75240a80a10ee0e11470c24693` and tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`
- [X] T077 [US2] Verify magiblot COPYRIGHT SHA-256 `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548` and all 50 accepted source hashes against `specs/030-tv203-magiblot-evolution-audit/magiblot-source-manifest.md`
- [X] T078 [US2] Record the magiblot identities, counts, results, risk, and trigger in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T079 [US2] Add exact source-baseline and manifest-cardinality tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T080 [US2] Add negative source-count, pin, tree, license, and source-hash mutation tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T081 [US2] Confirm all three external checkouts are clean, detached, outside repository roots, and absent from Git inventory
- [X] T082 [US2] Record command, checkout, network, availability, and reproducibility boundaries in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T083 [US2] Reconcile all 90 source identities with the contract rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T084 [US2] Record US2 independent provenance result in `specs/031-combined-conformance-closure/pr-evidence.md`

**Checkpoint**: Every external evidence source is exact, reproducible, and
still only a read-only comparison source.

---

## Phase 5: User Story 3 - Null-Finding-Ergebnis kritisch bestätigen (Priority: P3)

**Goal**: Prove that zero canonical findings and zero hardening intakes result
from complete evidence rather than suppression.

**Independent Test**: Injected findings, product decisions, owner assignments,
dependency edges, or intakes fail closed; the accepted dataset remains empty.

- [X] T085 [US3] Add exactly three known owner-schema rows with empty finding sets to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T086 [US3] Add explicit empty canonical-finding, product-decision, dependency-edge, and hardening-intake sets to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T087 [US3] Reconcile each `TGO###` disposition with its contract, reproduction, proof, consumer, and no-finding rationale in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T088 [US3] Reconcile each `MB###` disposition with its contract, reproduction, proof, consumer, and no-finding rationale in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T089 [US3] Confirm every prior `F001`-`F013` resolution still points to a real test or evidence proof
- [X] T090 [US3] Confirm no previously closed finding reproduces against the current merged baseline
- [X] T091 [US3] Confirm no observation is missing, multiply disposed, or classified by aggregate count alone
- [X] T092 [US3] Confirm every empty owner row generates no intake and has no dependency edge
- [X] T093 [US3] Confirm no required hardening Lastenheft was suppressed by an absent, duplicate, or misclassified relation
- [X] T094 [US3] Add no-suppression and empty-owner tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T095 [US3] Add injected canonical-finding and product-decision rejection tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T096 [US3] Add injected non-empty owner, dependency-edge, and hardening-intake rejection tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T097 [US3] Add reopened prior-finding and suppressed-intake rejection tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T098 [US3] Complete security-governance v0.6.0 applicability, N/A rationale, residual risk, and re-evaluation rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T099 [US3] Complete architecture-governance v0.5.0 including STRIDE/CIA/CAPEC and BSI C3A/C5 N/A rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T100 [US3] Complete isaqb-architecture-governance v0.2.0 quality, debt, risk, and intentional-modernization rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T101 [US3] Complete a11y-governance v0.4.0 bilingual, text-first, semantic, and didactic-comment rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T102 [US3] Complete cross-platform-governance v0.2.0 platform rows and script-parity N/A trigger in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T103 [US3] Complete agent-parity-governance v0.3.0 five-surface and `.specify/templates/` applicability rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T104 [US3] Complete autonomous-run-governance v0.2.2 state, authority, exact-head, review, closeout, and retrospective rows in `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T105 [US3] Add governance vocabulary and mandatory metadata tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T106 [US3] Add governance N/A and Open trigger rejection tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T107 [US3] Record US3 zero-counts, governance counts, and no-suppression conclusion in `specs/031-combined-conformance-closure/pr-evidence.md`

**Checkpoint**: The empty remediation portfolio is a validated result, not an
implicit assumption.

---

## Phase 6: User Story 4 - Wave-Freigabe kausal setzen (Priority: P4)

**Goal**: Keep the reviewed feature head blocked and allow Wave 5 eligibility
only through complete post-merge causal evidence.

**Independent Test**: Feature-head markers reject `Eligible`; an evidence-only
closeout with exact reviewed head, passed gates, and merge proof permits Wave 5
`Eligible` and Wave 6 only `ConditionallyReady`.

- [X] T108 [US4] Create feature-head blocked and post-merge target status text in `specs/031-combined-conformance-closure/pre-wave-gate.md`
- [X] T109 [US4] Add the complete causal transition object to `specs/031-combined-conformance-closure/closure-evidence.json`
- [X] T110 [US4] Add feature-head blocked-state tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T111 [US4] Add premature Wave-5 `Eligible` and excessive Wave-6 state rejection tests to `tests/TuiVision.Drivers.Tests/CombinedConformanceClosureEvidenceTests.cs`
- [X] T112 [US4] Add dual-state marker tests requiring complete `specs/031-combined-conformance-closure/delivery-closeout.md` causal evidence
- [X] T113 [US4] Search all Wave-marker consumers in tests, feature datasets, `Pflichtenheft.md`, `Lastenheft_Abarbeitungsreihenfolge.md`, agent files, gate files, and `docs/project-statistics.md`
- [X] T114 [US4] Reconcile every marker consumer with the feature-head blocked and post-merge conditional contract
- [X] T115 [US4] Update `Pflichtenheft.md` to keep both Waves blocked pending the causal Feature-031 closeout
- [X] T116 [US4] Update `Lastenheft_Abarbeitungsreihenfolge.md` to keep Feature 031 as the final pre-Wave-5 intake without starting Feature 032
- [X] T117 [US4] Synchronize the active Feature-031 blocked-state context in `AGENTS.md`
- [X] T118 [US4] Synchronize the active Feature-031 blocked-state context in `CLAUDE.md`
- [X] T119 [US4] Synchronize the active Feature-031 blocked-state context in `GEMINI.md`
- [X] T120 [US4] Synchronize the active Feature-031 blocked-state context in `.github/copilot-instructions.md`
- [X] T121 [US4] Synchronize the active Feature-031 blocked-state context in `.github/agents/copilot-instructions.md`
- [X] T122 [US4] Verify all five maintained agent surfaces are semantically equivalent and `.specify/templates/` remains unchanged
- [X] T123 [US4] Record the intended archived Lastenheft path in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T124 [US4] Validate the repository rename workflow and reserve `Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` as the post-validation archive target
- [X] T125 [US4] Update `docs/project-statistics.md` with the Feature-031 pre-delivery snapshot while both Waves remain blocked
- [X] T126 [US4] Record the causal closeout acceptance contract and non-recursive terminal-fact boundary in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T127 [US4] Confirm no Wave-5, Wave-6, or Feature-032 branch, directory, implementation task, or runtime change exists
- [X] T128 [US4] As the final polish step before candidate validation, run `bash scripts/rename-lastenheft.sh --no-commit Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.md 031-combined-conformance-closure`, update the accepted input path in `specs/031-combined-conformance-closure/closure-evidence.json`, and record US4 feature-head plus post-merge target states in `specs/031-combined-conformance-closure/pr-evidence.md`

**Checkpoint**: The reviewable feature candidate cannot release Wave 5 by
itself, but already contains the validator for the later causal transition.

---

## Phase 7: Implementation und lokale Validierung

**Purpose**: Converge the complete test-only closure and run all mandatory
local gates on the intended candidate.

- [X] T129 Increment the manual build counter in `Directory.Build.props` for the complete targeted closure-validator invocation
- [X] T130 Run `/Users/thorstenhindermann/.dotnet/dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --filter "FullyQualifiedName~ConformanceAuditEvidenceTests|FullyQualifiedName~ConformanceClosureEvidenceTests|FullyQualifiedName~TerminalGuiConformanceEvidenceTests|FullyQualifiedName~MagiblotEvolutionAuditEvidenceTests|FullyQualifiedName~CombinedConformanceClosureEvidenceTests"`
- [X] T131 Fix only Feature-031 evidence or test-integrity defects revealed by targeted tests
- [X] T132 If targeted tests are rerun, increment the manual build counter again in `Directory.Build.props` before the single invocation
- [X] T133 Record targeted commands, versions, results, errors, and exact counts in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T134 Run `git diff --check`, JSON parsing, Markdown fence, UTF-8, placeholder, and closed-vocabulary scans
- [X] T135 Run protected-source, external-checkout, generated-output, dependency, package, project, runtime, API, example, and consumer diff scans
- [X] T136 Run `/Users/thorstenhindermann/.dotnet/dotnet format --verify-no-changes` and record the result in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T137 Increment the manual build counter in `Directory.Build.props` for the full Release test invocation
- [X] T138 Run `/Users/thorstenhindermann/.dotnet/dotnet test TuiVision.sln --configuration Release` and record the result in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T139 Run `/usr/bin/xmllint --noout coverlet.runsettings` before coverage
- [X] T140 Increment the manual build counter in `Directory.Build.props` for the canonical coverage invocation
- [X] T141 Run `/Users/thorstenhindermann/.dotnet/dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` and record each required assembly percentage in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T142 Run `/Users/thorstenhindermann/.dotnet/tools/docfx docfx.json` and record warning/error counts in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T143 Run `/opt/homebrew/bin/npm run test:docfx` from `tests/web-a11y/` and record Playwright/Axe results in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T144 Run the `tests/web-a11y/package.json` Lynx scripts with `/opt/homebrew/bin/npm` and `/opt/homebrew/bin/lynx`, plus UTF-8, semantic structure, DE-first/EN-second CEFR-B2, and text-first review for changed learner-facing Markdown
- [X] T145 Run `bash scripts/scan-agent-secrets.sh --fail-on-high "$PWD"` and `/Users/thorstenhindermann/home-baseline-tmp/scripts/check-homogeneity.sh --json --dry-run --no-patch "$PWD"`, then complete scope and supply-chain scans with explicit repository roots and inspect every error channel; record the repository-local wrapper's missing-helper failure without treating it as a pass
- [X] T146 Re-run every Feature-031 checklist and confirm 115/115 completed items with no accepted open defect
- [X] T147 Reconcile exact evidence counts, final archived-input hashes, validation rows, task progress, and accepted-artifact hashes in `specs/031-combined-conformance-closure/pr-evidence.md`, `specs/031-combined-conformance-closure/tasks.md`, and `specs/031-combined-conformance-closure/autonomous-run-state.json`

---

## Phase 8: Candidate, Publish, Review und Feature-Merge

**Purpose**: Deliver the exact reviewed feature head and preserve post-merge
facts for the named closeout evidence.

- [X] T148 Align `Version`, `AssemblyVersion`, and `FileVersion` in `Directory.Build.props` to the current `1.31.<patch>.<build>` candidate without an extra build increment
- [X] T149 Stage only intended Feature-031 paths and inventory them in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T150 Run `git diff --cached --check` and compare staged, unstaged, and untracked inventories in `specs/031-combined-conformance-closure/pr-evidence.md`
- [X] T151 Commit the exact feature candidate and reserve the commit hash for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T152 Push branch `031-combined-conformance-closure`, verify the remote head, and reserve the result for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T153 Create the non-empty Feature-031 pull request and reserve its stable identity for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T154 If stable PR identity must enter `pr-evidence.md`, align version, stage/check, commit, and push one final review candidate before gate mapping
- [X] T155 Identify pull-request-context checks as authoritative, record duplicate push runs as operational noise, and preserve evidence for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T156 Map every applicable gate to the actual reviewed head, workflow, job, platform, and executed command in temporary provider-neutral evidence and reserve the mapping summary for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T157 Validate temporary exact-head evidence with the installed autonomous gate validator and preserve only its terminal result for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T158 Inspect Claude, Copilot, Human Approval, comments, and GraphQL review threads; record unavailable reviewers as missing and reserve results for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T159 Address every actionable review or CI finding, resolve its thread, re-run affected gates, rebuild exact-head evidence, and reserve remediation results for `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T160 Merge the feature PR with a merge commit only after all technical gates pass, zero actionable threads remain, and any narrow bypass affects Human Approval alone; then delete the feature branch, synchronize clean local `main`, and reserve all terminal facts for `specs/031-combined-conformance-closure/delivery-closeout.md`

---

## Phase 9: Kausaler Closeout und Retrospektive

**Purpose**: Record facts that could not truthfully exist on the reviewed
feature head, release Wave 5 without recursion, and end on synchronized `main`.

- [X] T161 Create `specs/031-combined-conformance-closure/delivery-closeout.md` with feature PR, reviewed head, gate, review, merge, first main-sync, and causal Wave-transition facts but no self-referential closeout identity
- [X] T162 Update `specs/031-combined-conformance-closure/closure-evidence.json`, `specs/031-combined-conformance-closure/combined-closure.md`, `specs/031-combined-conformance-closure/pre-wave-gate.md`, `specs/031-combined-conformance-closure/pr-evidence.md`, and `Pflichtenheft.md` so Wave 5 is `Eligible` and Wave 6 is only `ConditionallyReady`
- [X] T163 Synchronize the final causal Wave state in `Lastenheft_Abarbeitungsreihenfolge.md`, all five agent surfaces, and `docs/project-statistics.md`
- [X] T164 Create `specs/031-combined-conformance-closure/retrospective.md` and classify every reusable learning as `FeatureSpecific`, `RunbookClarification`, `SkillCorrection`, `TemplateCorrection`, `AgentPolicyCorrection`, `ValidationAutomation`, `PresetFollowUp`, or `NoPromotion`
- [X] T165 For `NoPromotion`, create no Home-Baseline branch, preset release, or empty PR; for a reproduced provider-neutral defect, complete the documented PresetFollowUp cycle before Wave 5
- [X] T166 Prepare the terminal `172/172`, `Retrospective`, `Completed`, and `nextExactAction: N/A` state plus immutable causal dispositions for T167-T172 in `specs/031-combined-conformance-closure/delivery-closeout.md`; make them effective only when the closeout commit reaches `main`
- [X] T167 Create one evidence-only closeout branch and verify that it changes no runtime, API, dependency, project, test logic, example, consumer, historical, or external source
- [X] T168 Run closeout-proportional state, diff, JSON, secret, agent-parity, DocFX, A11Y, and UTF-8 checks and record them in `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T169 Commit, push, and create the non-empty causal closeout PR defined by `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T170 Converge closeout checks and actionable review threads without writing the closeout PR's own head or merge identity back into `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T171 Merge the closeout PR under the authorized policy, delete obsolete closeout branches, switch to `main`, fetch/prune, pull fast-forward only, and verify the terminal fact externally against `specs/031-combined-conformance-closure/delivery-closeout.md`
- [X] T172 Prove clean `HEAD == origin/main`, externally verify the completed causal contract in `specs/031-combined-conformance-closure/delivery-closeout.md`, confirm Wave 5 `Eligible`, Wave 6 `ConditionallyReady`, and do not start either Wave or Feature 032

## Dependencies

- T001-T016 establish the autonomous and evidence foundation.
- T017-T035 bind accepted inputs and establish the test-only validation surface.
- T036-T067 complete the test-first combined-evidence story.
- T068-T084 complete source provenance after accepted source manifests are bound.
- T085-T107 complete no-suppression and governance proof after all observations exist.
- T108-T128 complete the dual-state Wave contract before final validation.
- T129-T147 complete local convergence and all applicable repository gates.
- T148-T160 deliver and merge the reviewed feature head.
- T161-T172 perform the single non-recursive causal closeout and final synchronization.

All shared evidence, version, statistics, workflow, marker, and agent-guidance
files are single-writer surfaces. No task may modify product runtime, public
APIs, dependencies, packages, projects, examples, consumer sources,
`tv203s/`, `TVDEMOS/`, `TVFM/`, or external comparison checkouts.

## Implementation Strategy

The MVP is User Story 1 through T047: one complete representative slice and
its red/green proof. The remaining rows extend the same closed contracts
without changing architecture. Source provenance and no-suppression are
independently reviewable after the combined dataset exists. Wave eligibility
is intentionally deferred to the causal post-merge closeout.
