# Tasks: Wave-5 Combined Delta Closure

**Input**: Akzeptierte Artefakte unter
`specs/034-wave5-combined-delta-closure/`, bindendes Lastenheft 19 sowie die
gemergten Feature-032-/033-Artefakte
**Delivery mode**: `MergeAndSync`
**Scope**: Read-only Produktdelta-Audit mit test-only Validierung; keine
Produkt-, Beispiel- oder Frameworkänderung

## Phase 1: Setup und Evidence-Grundlage

**Zweck**: Laufidentität, Authority, Scope und wiederaufnahmefähige Evidence
vor jeder Implementierungsänderung absichern.

- [X] T001 Verify branch `034-wave5-combined-delta-closure`, `.specify/feature.json`, baseline ancestry, and dirty-path ownership in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T002 Verify intake PR #98 and synchronized baseline commit `4dbfa39511f774af5b0c79ac6c5518dd058f664c` in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T003 Run `specify check` and repository prerequisites and record exit status plus error-channel review in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T004 Verify the seven installed preset names, versions, and priorities in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T005 Confirm every Feature-034 checklist item is complete in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T006 Validate `specs/034-wave5-combined-delta-closure/autonomous-run-state.json` with the installed Bash and PowerShell validators
- [X] T007 Validate `specs/034-wave5-combined-delta-closure/autonomous-gate-requirements.json` as UTF-8 JSON and record its SHA-256 in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T008 Record all binding scope exclusions and protected read-only roots in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T009 Record shared single-writer paths and serialization rules in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T010 Record the Feature-034 `1.34.<patch>.<build>` version scheme and one-build-counter-increment-per-explicit-command rule in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T011 Record that no intentional interruption, product remediation, Wave 6, or Feature 035 start is permitted in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T012 Record the non-recursive causal closeout boundary and exact path `specs/034-wave5-combined-delta-closure/delivery-closeout.md`
- [X] T013 Inventory all intended feature, test, status, archive, statistics, version, and agent paths in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T014 Confirm no generated DocFX output, external checkout, cache, log, credential, or test output is intended for Git in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [X] T015 Record accepted artifacts and SHA-256 values at the Tasks checkpoint in `specs/034-wave5-combined-delta-closure/autonomous-run-state.json`
- [X] T016 Update `specs/034-wave5-combined-delta-closure/autonomous-run-state.json` to the validated Analyze-ready checkpoint

---

## Phase 2: Foundational Provenienz und Test-Harness

**Zweck**: Bindende Eingaben und geschlossene Vokabulare festlegen, bevor der
erste test-first Slice ausgeführt wird.

- [ ] T017 Verify PR #93 base, head, merge, role, and exact changed-file set through Git history and GitHub, then record them in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T018 Verify PR #94 through Git history and GitHub as the Feature-032 causal closeout, then record it in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T019 Verify PR #95 through Git history and GitHub as non-product prompt metadata, then record it in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T020 Verify PR #96 base, head, merge, role, and exact changed-file set through Git history and GitHub, then record them in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T021 Verify PR #97 through Git history and GitHub as the Feature-033 causal closeout, then record it in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T022 Add run identity, baseline, blocked feature-head states, post-merge targets, review fields, and causal boundary to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T023 Record accepted Feature-032 input paths and SHA-256 values in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T024 Record accepted Feature-033 input paths and SHA-256 values in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T025 Add the closed source, consumer, example, dimension, decision, proof-role, governance, and validation vocabularies to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T026 Add all 15 historical source paths, roles, Feature-032 targets, stable blobs, and reciprocal IDs to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T027 Add all six consumer rows `W5-001` through `W5-006` with reciprocal source and example sets to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T028 Add the seven governance row skeletons without empty starter fields to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T029 Add local, remote, review, and exact-head validation row skeletons without inferred results to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T030 Review imports, MSTest 4 APIs, JSON DOM choices, repository-root helpers, public XML summaries, and didactic-comment needs before editing `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T031 Add the test class, repository-root loader, JSON parser, and result helper to `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T032 Add closed-set, ordinal-ID, SHA-256, Git-object, proof-path, and reciprocal-reference helpers to `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T033 Add bounded clone-and-mutate helpers for missing, duplicate, invalid, drift, gap, finding, product-decision, and state cases to `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T034 Add LF/CRLF normalization helpers that parse structurally rather than replacing data strings in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T035 Add German-first/English-second XML summaries and moderate why/proof comments to non-trivial validation blocks in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T036 Record compile-surface, comment, test-helper, and failure-channel boundaries in `specs/034-wave5-combined-delta-closure/pr-evidence.md`

**Checkpoint**: Provenienz und Harness sind definiert; kein Produktpfad wurde
verändert.

---

## Phase 3: User Story 1 - Tatsächlichen Wave-5-Delta nachvollziehen (Priority: P1)

**Ziel**: Exakte PR- und Source-Provenienz sowie der Calculator-Referenz-Slice
sind unabhängig beweisbar.

**Independent Test**: Der Calculator-Slice und seine Pins scheitern bei
fehlenden Restzeilen erwartungsgemäß; nach vollständigem Slice bestehen sie,
während PR-, Hash- und Duplicate-Mutationen weiterhin scheitern.

- [ ] T037 [US1] Add failing existence and root-schema proof for `wave5-combined-delta.json` in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T038 [US1] Add failing exact PR #93/#96 pin and role proof in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T039 [US1] Add failing exact 15-source, six-consumer, and Calculator reference proof in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T040 [US1] Add failing missing-example and unknown-decision mutation proofs in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T041 [US1] Increment the manual build counter in `Directory.Build.props` for the first targeted Red invocation
- [ ] T042 [US1] Run the Calculator/provenance-only Release test filter and accept only the planned incomplete-matrix failure
- [ ] T043 [US1] Record Red command, version, exit/error review, and accepted failure boundary in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T044 [US1] Complete the `Tp7Calculator` functional-proof row in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T045 [US1] Complete the `Tp7Calculator` showcase-closure row in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T046 [US1] Complete the `Tp7Calculator` guide/launch row in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T047 [US1] Complete the `Tp7Calculator` combined row with `W5-005`, all dimensions, Safety boundary, residual risk, trigger, and `AcceptedAsIs` in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T048 [US1] Add reciprocal Calculator references among source, consumer, proof, showcase, guide, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T049 [US1] Add complete PR role, expected-file, and excluded-closeout tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T050 [US1] Add exact accepted-input SHA-256 tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T051 [US1] Add all 15 source path, blob, role, target, and current-head equality tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T052 [US1] Add all six consumer ID, source, example, framework, proof, and reciprocal-set tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T053 [US1] Add negative PR pin, changed-file, input-hash, source-blob, missing-source, duplicate-source, and duplicate-consumer tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T054 [US1] Increment the manual build counter in `Directory.Build.props` for the reference-slice Green invocation
- [ ] T055 [US1] Run the Calculator/provenance Release filter Green and record exact count and error-channel review
- [ ] T056 [US1] Record exact PR, source, consumer, Calculator, and Red/Green evidence in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T057 [US1] Update `specs/034-wave5-combined-delta-closure/autonomous-run-state.json` and accepted hashes at the reference-slice checkpoint

**Checkpoint**: Die autoritative Produktmenge und ein vollständiger
Beispielslice sind unabhängig bewiesen.

---

## Phase 4: User Story 2 - Zehn Beispiele gemeinsam abnehmen (Priority: P1)

**Ziel**: Genau zehn kombinierte Beispielzeilen verbinden Funktion, Showcase,
Guide, Bedienung und Qualitätsdimensionen.

**Independent Test**: Die vollständige 10/10/10/10-Matrix besteht; fehlende,
doppelte, unvollständige oder widersprüchliche Zeilen scheitern.

- [ ] T058 [US2] Complete `Tp7Demo` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T059 [US2] Complete `Tp7Edit` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T060 [US2] Complete `Tp7Help` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T061 [US2] Complete `Tp7ResourceDemo` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T062 [US2] Complete `Tp7ResourceGenerator` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T063 [US2] Complete `Tp7AsciiTable` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T064 [US2] Complete `Tp7Calendar` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T065 [US2] Complete `Tp7Puzzle` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T066 [US2] Complete `Tp7MouseDialog` functional, showcase, guide/launch, and combined rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T067 [US2] Reconcile exactly ten functional proof IDs with Feature-032 test identities and evidence paths in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T068 [US2] Reconcile exactly ten showcase closure IDs with Feature-033 app-loop, focus, status, Description, layout, and cell evidence in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T069 [US2] Reconcile exactly ten guide/launch IDs with existing project and guide paths in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T070 [US2] Reconcile all ten combined rows with exact source and consumer reverse links in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T071 [US2] Complete normal entry point, first visible state, primary action, focus, status, F1/Description, and `Ctrl+Q` fields for all ten rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T072 [US2] Complete behavior, interaction, layout, proof, documentation, A11Y, platform, security, and framework-reuse dimensions for all ten rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T073 [US2] Assign exactly one primary decision and complete evidence, residual risk, and trigger for all ten rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T074 [US2] Add exact ten functional, ten showcase, ten guide/launch, and ten combined-row tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T075 [US2] Add complete per-row source, consumer, proof, launch, operation, and dimension tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T076 [US2] Add exact-one-primary-decision and accepted-row-without-Gap tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T077 [US2] Add negative missing, duplicate, unknown, orphaned, incomplete, and contradictory example relation tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T078 [US2] Add negative unknown dimension, unknown decision, accepted Gap, missing guide, missing launch, missing F1, missing quit, and missing cell proof tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T079 [US2] Add `AcceptedIntentionalDeviation` completeness and style-only-not-finding tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T080 [US2] Add LF and CRLF serialization-equivalence tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T081 [US2] Create the readable ten-row matrix and cardinality summary in `specs/034-wave5-combined-delta-closure/wave5-closure.md`
- [ ] T082 [US2] Reconcile every readable summary count and decision with `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T083 [US2] Increment the manual build counter in `Directory.Build.props` for the complete combined-matrix invocation
- [ ] T084 [US2] Run the complete Feature-034 closure test class and record exact count, version, result, and error-channel review
- [ ] T085 [US2] Record all exact cardinalities, decisions, intentional deviations, and no-open-Gap result in `specs/034-wave5-combined-delta-closure/pr-evidence.md`

**Checkpoint**: Alle zehn Beispiele sind als kombinierte, einzeln reviewbare
Einheiten abgenommen.

---

## Phase 5: User Story 3 - Framework-Nutzung und Proof-Qualität bewerten (Priority: P2)

**Ziel**: Beispielkomposition, Framework-Ownership, echte App-Loops und
kontrollierte Grenzen sind nachvollziehbar disponiert.

**Independent Test**: Shared Helper sind als reine Komposition oder Finding
klassifiziert; alle Primary-Proofs bleiben echte App-Loop-/View-/Cell-Pfade.

- [ ] T086 [US3] Review `Wave5Application` ownership, event-loop, status, Description, and composition responsibilities read-only and record the decision in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T087 [US3] Review `Wave5ConsoleHost` launch, smoke, PTY, fallback, and shutdown responsibilities read-only and record the decision in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T088 [US3] Review `Wave5StatusLine` text-first and framework-duplication boundaries read-only and record the decision in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T089 [US3] Review `Wave5GridView` focus, navigation, rendering, and reuse boundaries read-only and record the decision in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T090 [US3] Review all Wave-5 example state models for hidden framework, host, locale, clock, network, or file dependencies and record decisions in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T091 [US3] Review all Feature-032 functional tests for real `app.Run()`, state, view, and cell proof roles in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T092 [US3] Review all Feature-033 showcase tests for focus, status, Description, constrained layout, and controlled quit in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T093 [US3] Classify every relevant helper as `PrimaryProof`, `SupplementalProof`, or `SetupOnly` in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T094 [US3] Confirm editor, generator, Resource, Help, mouse, locale, clock, and host-state boundaries remain controlled in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T095 [US3] Add Framework helper inventory, allowed-local-composition, and reusable-duplication tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T096 [US3] Add PrimaryProof role and direct-helper-not-primary rejection tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T097 [US3] Add controlled path, capability honesty, no-host-mutation, deterministic date/puzzle, and no-hidden-dependency tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T098 [US3] Complete security-governance v0.6.0 applicability and N/A rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T099 [US3] Complete architecture-governance v0.5.0 including framework ownership and BSI C3A/C5 N/A rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T100 [US3] Complete isaqb-architecture-governance v0.2.0 quality, ownership, risk, and debt rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T101 [US3] Complete a11y-governance v0.4.0 keyboard, focus, status, Description, layout, text-first, and comment rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T102 [US3] Complete cross-platform-governance v0.2.0 platform and script-parity N/A rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T103 [US3] Complete agent-parity-governance v0.3.0 and `.specify/templates/` N/A rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T104 [US3] Complete autonomous-run-governance v0.2.2 state, authority, exact-head, review, closeout, and retrospective rows in `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T105 [US3] Add governance vocabulary, metadata, N/A-trigger, and Open-follow-up tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T106 [US3] Record the Framework ownership, proof-role, governance, and external-comparison N/A conclusions in `specs/034-wave5-combined-delta-closure/pr-evidence.md`

**Checkpoint**: Kein zweites examples-lokales Framework oder unehrlicher
Primary-Proof bleibt unbemerkt.

---

## Phase 6: User Story 4 - Wave-5-Abschluss und Wave-6-Folge festlegen (Priority: P3)

**Ziel**: Der Feature-Head bleibt kausal gesperrt; Findings oder ein sauberer
Abschluss führen zu genau der erlaubten Folge.

**Independent Test**: Premature final markers scheitern; vollständige
Candidate Findings werden akzeptiert, unvollständige Findings und Product
Decisions blockieren, und ein vollständiger Closeout erlaubt genau
`Closed`/`EligibleForIntake`.

- [ ] T107 [US4] Add explicit empty or populated Candidate Finding, Product Decision, owner-group, and hardening-intake sets to `specs/034-wave5-combined-delta-closure/wave5-combined-delta.json`
- [ ] T108 [US4] Add stable `W5D###`, reproducibility, evidence, owner, deduplication, and follow-up validation in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T109 [US4] Add ProductDecision stop and no-automatic-remediation validation in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T110 [US4] Add blocked feature-head and premature `Closed`/`EligibleForIntake` rejection tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T111 [US4] Add dual-state tests requiring complete `specs/034-wave5-combined-delta-closure/delivery-closeout.md` causal evidence
- [ ] T112 [US4] Add no-empty-intake, no-duplicate-owner, no-suppressed-finding, and no-Feature-035-start tests in `tests/TuiVision.Examples.SmokeTests/Wave5CombinedDeltaClosureTests.cs`
- [ ] T113 [US4] Search all Wave-5/Wave-6 and next-intake marker consumers in tests, specs, status documents, agent files, and statistics
- [ ] T114 [US4] Reconcile every marker consumer with the feature-head blocked and post-merge target contract
- [ ] T115 [US4] Update `Pflichtenheft.md` to keep Wave 6 blocked pending the causal Feature-034 closeout
- [ ] T116 [US4] Update `Lastenheft_Abarbeitungsreihenfolge.md` to keep Feature 034 active without starting Feature 035
- [ ] T117 [US4] Synchronize the active Feature-034 blocked-state context in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`
- [ ] T118 [US4] Verify all five maintained agent surfaces are semantically equivalent and `.specify/templates/` remains unchanged
- [ ] T119 [US4] Update `docs/project-statistics.md` with the Feature-034 pre-delivery snapshot while Wave 6 remains blocked
- [ ] T120 [US4] Archive `Lastenheft_19_Wave5-Combined-Delta-Closure.md` through the repository rename workflow only after every local audit requirement is satisfied
- [ ] T121 [US4] Record final finding, owner, intake, product-decision, Wave-target, and no-start counts in `specs/034-wave5-combined-delta-closure/pr-evidence.md`

**Checkpoint**: Der reviewbare Kandidat kann Wave 6 nicht selbst starten,
enthält aber die vorab reviewte kausale Abschlussregel.

---

## Phase 7: Lokale, interaktive und vollständige Validierung

**Zweck**: Den vollständigen test-only Audit und die tatsächlichen Wave-5-Pfade
auf dem beabsichtigten Kandidaten beweisen.

- [ ] T122 Run `git diff --check`, JSON parse, Markdown fence, UTF-8, placeholder, closed-vocabulary, and line-ending scans and record them in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T123 Run protected-source, generated-output, dependency, package, project, runtime, API, framework, example, and external-source diff scans and record them in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T124 Run `dotnet format TuiVision.sln --verify-no-changes` and record the result in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T125 Increment the manual build counter in `Directory.Build.props` for the complete targeted Feature-034 and Wave-5 smoke invocation
- [ ] T126 Run all Feature-034, `Tp7*`, `Wave5Functional`, and `Wave5Showcase` Release tests and record exact counts in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T127 Increment the manual build counter in `Directory.Build.props` for the explicit Release build used by entry-point validation
- [ ] T128 Run `dotnet build TuiVision.sln --configuration Release` and record exit/error-channel review in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T129 Run all ten `dotnet run --no-build --configuration Release --project examples/Tp7* -- --smoke` checks and record each result in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T130 Run all ten normal PTY paths through `dotnet run --no-build --configuration Release --project examples/Tp7*` with first frame, one primary action, focus/status, F1/Description, and `Ctrl+Q`, then record each result in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T131 Increment the manual build counter in `Directory.Build.props` for the full Release test invocation
- [ ] T132 Run `dotnet test TuiVision.sln --configuration Release` and record exact counts in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T133 Run `xmllint --noout coverlet.runsettings` before coverage and record the result
- [ ] T134 Increment the manual build counter in `Directory.Build.props` for the canonical coverage invocation
- [ ] T135 Run the canonical Coverlet command and record all five assembly percentages in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T136 Run `docfx docfx.json` and record warning/error counts in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T137 Run Playwright/Axe under `tests/web-a11y` and record exact results in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T138 Run UTF-8, semantic Markdown, German-first/English-second CEFR-B2, text-first, keyboard, and Lynx review for changed learner-facing files
- [ ] T139 Run the agent secret scan, high-confidence secret scan, supply-chain checks, and explicit-root homogeneity/parity helper; inspect exit and error channels
- [ ] T140 Re-run every Feature-034 checklist and confirm all items complete with no accepted open defect
- [ ] T141 Reconcile final evidence counts, validation rows, task progress, and accepted-artifact hashes in `pr-evidence.md`, `tasks.md`, and `autonomous-run-state.json`

---

## Phase 8: Exact Candidate, Publish, Review und Feature-Merge

**Zweck**: Den exakten reviewten Feature-Head liefern und kausale
Post-Merge-Fakten für einen benannten Closeout bewahren.

- [ ] T142 Align `Version`, `AssemblyVersion`, and `FileVersion` in `Directory.Build.props` to the current `1.34.<patch>.<build>` candidate without an extra build increment
- [ ] T143 Stage only intended Feature-034 paths and inventory them in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T144 Run `git diff --cached --check` and compare staged, unstaged, and untracked inventories in `specs/034-wave5-combined-delta-closure/pr-evidence.md`
- [ ] T145 Commit the exact feature candidate and reserve its hash for `specs/034-wave5-combined-delta-closure/delivery-closeout.md`
- [ ] T146 Push branch `034-wave5-combined-delta-closure`, verify the remote head, and reserve the result for `specs/034-wave5-combined-delta-closure/delivery-closeout.md`
- [ ] T147 Create the non-empty Feature-034 PR and reserve its stable identity for `specs/034-wave5-combined-delta-closure/delivery-closeout.md`
- [ ] T148 Identify pull-request-context checks as authoritative and record duplicate push runs as operational noise
- [ ] T149 Map every acceptance gate to the actual reviewed head, workflow, job, platform, and executed command in temporary exact-head evidence
- [ ] T150 Validate temporary exact-head evidence with the installed autonomous gate validator and reserve its result for `specs/034-wave5-combined-delta-closure/delivery-closeout.md`
- [ ] T151 Inspect Claude, Copilot, Human Approval, comments, and GraphQL threads; record unavailable reviewers as missing
- [ ] T152 Address every actionable review or CI finding, resolve its thread, re-run affected gates, and rebuild exact-head evidence
- [ ] T153 Merge the feature PR with a merge commit only after all technical gates pass, zero actionable threads remain, and any bypass affects Human Approval alone
- [ ] T154 Delete the feature branch, switch locally to `main`, fetch/prune, pull fast-forward only, and prove clean `HEAD == origin/main`

---

## Phase 9: Kausaler Closeout und Retrospektive

**Zweck**: Nicht im Feature-Head behauptbare Merge-Fakten abschließen, ohne
einen weiteren fachlichen Lauf zu starten.

- [ ] T155 Create `specs/034-wave5-combined-delta-closure/delivery-closeout.md` only if required, recording reviewed feature head, passing gates, feature merge, final decisions, and causal boundary
- [ ] T156 If zero Candidate Findings and zero Product Decisions are proven, set Wave 5 `Closed` and Wave 6 `EligibleForIntake` in status and evidence files without starting Wave 6
- [ ] T157 If clean, create `Lastenheft_20_Wave6-TVFM-Functional-Porting.md`, reserve Feature 035 in processing order, require any later Showcase remediation to be derived from the actual Feature-035 delta, and add copyable Specify/Autonomous prompts without creating a branch or feature directory
- [ ] T158 If findings exist, create only non-empty deduplicated owner-specific hardening intakes and keep Wave 6 blocked
- [ ] T159 If a Product Decision exists, record the stop boundary and create no Wave-6 intake
- [ ] T160 Complete `specs/034-wave5-combined-delta-closure/retrospective.md` with `NoPromotion` unless a reproducible provider-neutral preset defect exists
- [ ] T161 Update final task, run-state, statistics, agent, Pflichtenheft, and ordering surfaces as one serialized closeout candidate
- [ ] T162 Validate the closeout as evidence-only with no test, product, example, dependency, historical, or generated-output change
- [ ] T163 Create, review, and merge a non-empty causal closeout PR only when T155 is genuinely required
- [ ] T164 Finish with `Retrospective`, `Completed`, all tasks terminal, `nextExactAction: N/A`, clean local `main`, and `HEAD == origin/main`

---

## Abhängigkeiten und Reihenfolge / Dependencies and Order

- Phase 1 blockiert alle weiteren Phasen.
- Phase 2 blockiert jeden User-Story-Slice.
- US1 liefert das Provenienz- und Calculator-Referenzmuster.
- US2 hängt von US1 ab und vervollständigt die zehn Zeilen.
- US3 hängt von US2 ab, weil Framework- und Proof-Entscheidungen die
  vollständige Matrix benötigen.
- US4 hängt von US2 und US3 ab und setzt Finding-/Wave-Regeln.
- Lokale Validierung hängt von allen User Stories ab.
- Remote Delivery hängt vom vollständigen lokalen Kandidaten ab.
- Der kausale Closeout hängt vom tatsächlichen Feature-Merge ab.

Nahezu alle Tasks schreiben dieselben Evidence-, Test-, Status-, Versions-,
Agent- oder Statistikdateien. Daher enthält die Liste bewusst keine
`[P]`-Marker.

## Implementierungsstrategie / Implementation Strategy

1. Evidence-first und exakte Inputs.
2. Calculator als test-first Referenz-Slice.
3. Vollständige zehnzeilige Kombination.
4. Framework-/Proof-Audit und Finding-Regeln.
5. Lokale und interaktive Vollvalidierung.
6. Exact-Head-Delivery.
7. Kausaler Evidence-Closeout ohne automatischen Feature-035-Start.
