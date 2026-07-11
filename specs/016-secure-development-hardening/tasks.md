# Tasks: Secure Development Hardening

**Input**: Design documents from `specs/016-secure-development-hardening/`  
**Prerequisites**: `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/`, completed `checklists/`

**Tests**: Script contract tests are test-first. Full Release, coverage, SBOM, package, secret, DocFX, and web-A11Y validation are mandatory because this feature is project-wide.

**Organization**: Tasks are grouped by user story. No parallel markers are used because `pr-evidence.md`, `control-assessment.md`, workflows, scripts, agent guidance, statistics, version metadata, and delivery state require serialized writes.

## Format: `[ID] [Story] Description`

- **[Story]** maps implementation work to a user story from `spec.md`.
- Every task names its primary file or command surface.
- Conditional findings receive an explicit evidence outcome; they are never skipped silently.

## Phase 1: Setup and Preflight

**Purpose**: Prove the active feature, toolchain, artifacts, and clean execution boundary before implementation.

- [X] T001 Create `specs/016-secure-development-hardening/pr-evidence.md` with run metadata, scope guard, command ledger, finding/remediation ledger, governance table, validation table, human-only table, and final PR summary sections from `data-model.md` before running further preflight commands.
- [X] T002 Verify branch `016-secure-development-hardening`, HEAD ancestry from `e28ce6e`, and `.specify/feature.json` path; record the result directly in `pr-evidence.md`.
- [X] T003 Run `specify check` and record the concise tool/preset result directly in `pr-evidence.md`.
- [X] T004 Run `.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks` and record the resolved feature paths in `pr-evidence.md`.
- [X] T005 Verify every checklist in `specs/016-secure-development-hardening/checklists/` has zero incomplete items; record the result and stop on any open item.
- [X] T006 Read `AGENTS.md`, `.specify/memory/constitution.md`, `Lastenheft_Secure-Development-Hardening.md`, and every artifact under `specs/016-secure-development-hardening/`; record no material post-plan conflict in `pr-evidence.md`.
- [X] T007 Read `docs/secure-development/Richtlinie_Sichere-Entwicklung.md`, `Checklistensammelband_Sichere-Entwicklung.md`, all twelve files in `docs/secure-development/checklisten/`, and related documents; record the reviewed baseline paths.
- [X] T008 Record initial `git status --short --branch -uall`, current version fields, commit count, platform/tool versions, and generated-output retention boundary in `pr-evidence.md`.
- [X] T009 Record exactly `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, and `FollowUp` plus their status-specific mandatory fields in `pr-evidence.md`; reject aliases, combined states, and blank required fields.
- [X] T010 Record stop conditions for credentials, legal decisions, irreversible provider changes, scope impossibility, and unremediated critical risk in `pr-evidence.md`.

**Checkpoint**: Preflight is evidenced, all checklists are complete, and implementation may create durable assessment data.

## Phase 2: Foundational Evidence Model

**Purpose**: Establish complete inventories and schemas before any positive security claim or remediation.

- [X] T011 Extract all `#### CL-XX-NN` headings from `docs/secure-development/checklisten/CL_*.md`; record 157 unique source IDs, per-checklist counts, duplicate count, and extraction command in `pr-evidence.md`.
- [X] T012 Create `docs/security/control-assessment.md` with bilingual purpose, source baseline, complete field definitions, status rules, ownership defaults, and no empty starter rows.
- [X] T013 Add finding, remediation, evidence-artifact, validation-run, supply-chain, governance, and script-contract schemas to `pr-evidence.md` exactly as required by `data-model.md`.
- [X] T014 Inventory every file under `docs/security/`, classify current/stub/missing state, and record the consolidation target in `pr-evidence.md`.
- [X] T015 Inventory security-relevant runtime boundaries in `src/` and `tests/`: input/events, terminal, file/resource, serialization, error/output, generated data, and package/CI boundaries; record paths in `pr-evidence.md`.
- [X] T016 Inventory every `.github/workflows/*.yml` `uses:` dependency, mutable tag, permission block, trigger, secret use, and artifact retention path in `pr-evidence.md`.
- [X] T017 Inventory `scripts/rename-lastenheft.sh`, `scripts/rename-lastenheft.ps1`, existing script docs/tests, and current commit/index behavior in `pr-evidence.md`.
- [X] T018 Inventory the five maintained agent files and `.specify/templates/` impact surface; record current 016 plan references and any pre-existing divergence.
- [X] T019 Inventory ignored/generated paths for SBOM, DocFX, API YAML, test results, coverage, caches, logs, credentials, and temporary data; record any missing ignore boundary as a finding.
- [X] T020 Create a traceability table in `pr-evidence.md` mapping FR-001..FR-036, CR-001..CR-016, and SC-001..SC-014 to task IDs and evidence destinations.
- [X] T021 Record the bounded-remediation decision test in `pr-evidence.md`: repository-local, reversible, testable, architecture-compatible, and no unsupported external assertion.
- [X] T022 Record `tv203s/` as read-only and initially `N/A` because feature 016 reviews security rather than porting historical behavior; define the re-evaluation trigger.

**Checkpoint**: Inventories, schemas, scope guards, and requirement mappings are ready; no control has been silently classified.

## Phase 3: User Story 1 - Obtain an Auditable Security Baseline (Priority: P1) MVP

**Goal**: Every secure-development control and project security document has a current, traceable, non-misleading status.

**Independent Test**: A reviewer can trace all 157 source controls to exactly one complete assessment row and direct evidence or a bounded decision.

- [X] T023 [US1] Assess CL-01 controls `CL-01-01` through `CL-01-12` in `docs/security/control-assessment.md` with complete fields and standard applicability evidence.
- [X] T024 [US1] Assess CL-02 controls `CL-02-01` through `CL-02-13` in `docs/security/control-assessment.md` with architecture, quality, risk, and debt evidence.
- [X] T025 [US1] Assess CL-03 controls `CL-03-01` through `CL-03-15` in `docs/security/control-assessment.md`; use owned-crypto `N/A` only with factual trigger and evidence.
- [X] T026 [US1] Assess CL-04 controls `CL-04-01` through `CL-04-10` in `docs/security/control-assessment.md` with STRIDE/CIA/CAPEC evidence.
- [X] T027 [US1] Assess CL-05 controls `CL-05-01` through `CL-05-13` in `docs/security/control-assessment.md` with SBOM, VEX, SLSA, Scorecard, action-pin, and dependency evidence boundaries.
- [X] T028 [US1] Assess CL-06 controls `CL-06-01` through `CL-06-11` in `docs/security/control-assessment.md` with disclosure, response, ownership, and provider boundaries.
- [X] T029 [US1] Assess CL-07 controls `CL-07-01` through `CL-07-12` in `docs/security/control-assessment.md`; keep CRA market placement human-only and avoid conformity claims.
- [X] T030 [US1] Assess CL-08 controls `CL-08-01` through `CL-08-13` in `docs/security/control-assessment.md` with source-review and validation evidence.
- [X] T031 [US1] Assess CL-09 controls `CL-09-01` through `CL-09-17` in `docs/security/control-assessment.md` with AI-development-tool and secure-generation evidence.
- [X] T032 [US1] Assess CL-10 controls `CL-10-01` through `CL-10-17` in `docs/security/control-assessment.md` with workstation, CI, secret, dependency, and environment evidence.
- [X] T033 [US1] Assess CL-11 controls `CL-11-01` through `CL-11-12` in `docs/security/control-assessment.md`; use DPIA `N/A` only with factual personal-data trigger.
- [X] T034 [US1] Assess CL-12 controls `CL-12-01` through `CL-12-12` in `docs/security/control-assessment.md` with sandbox, permission, evidence, and agent-boundary results.
- [X] T035 [US1] Mechanically compare source and assessment IDs; prove 157 source IDs, 157 rows, zero missing, zero duplicate, and zero unknown IDs in `pr-evidence.md`.
- [X] T036 [US1] Update `docs/security/README.md` into a current bilingual index with status, owner/freshness, and links for every required security evidence file.
- [X] T037 [US1] Update `docs/security/gsdb-self-assessment.md` from preflight-only status to a current feature-016 summary that links the 157-control matrix without claiming formal approval.
- [X] T038 [US1] Replace the stub in `docs/security/threat-model.md` with project-wide assets, trust boundaries, STRIDE/CIA/CAPEC paths, mitigations, and residual risks.
- [X] T039 [US1] Replace the stub in `docs/security/arc42-security.md` with project-wide security context, runtime boundaries, principles, risks, and decision links.
- [X] T040 [US1] Replace the stub in `docs/security/security-quality-scenarios.md` with measurable local input, serialization, terminal, file, supply-chain, script, and disclosure scenarios.
- [X] T041 [US1] Replace the stub in `docs/security/security-checklist.md` with a project-level summary linked to `control-assessment.md`, findings, remediation, and validation.
- [X] T042 [US1] Replace the stub in `docs/security/asvs-verification.md` with current ASVS `N/A`, factual scope, evidence, residual risk, and re-evaluation trigger.
- [X] T043 [US1] Replace the stub in `docs/security/zero-trust-applicability.md` with current `N/A`, local trust-boundary distinction, residual risk, and distributed-service trigger.
- [X] T044 [US1] Replace the stub in `docs/security/samm-assessment.md` with a lightweight current maturity assessment and prioritized repository-local improvements/follow-ups.
- [X] T045 [US1] Create `docs/security/cloud-autonomy-applicability.md` with BSI C3A `N/A`, current provider/deployment facts, and cloud-selection trigger.
- [X] T046 [US1] Create `docs/security/cloud-compliance-assurance.md` with BSI C5 `N/A`, current shared-responsibility facts, and cloud-assurance trigger.
- [X] T047 [US1] Create `docs/security/regulatory-applicability.md` with NIS2, CRA, EU AI Act, DORA, and DPIA statuses, human-only ownership, evidence limits, and triggers.
- [X] T048 [US1] Update `docs/security/adr/README.md` with the feature-016 S-ADR applicability decision; create a numbered S-ADR only if an architecturally significant security choice was actually made.
- [X] T049 [US1] Add a text-first sample trace to `docs/security/control-assessment.md` from one source control through status, evidence, remediation/follow-up, and validation.
- [X] T050 [US1] Record US1 completion counts, status counts, direct-evidence checks, remaining `Open`/`FollowUp` rows, and no-stub status in `pr-evidence.md`.

**Checkpoint**: The project has an auditable baseline independent of later remediation and supply-chain implementation.

## Phase 4: User Story 2 - Close Bounded Security Gaps (Priority: P1)

**Goal**: Concrete repository-local findings are fixed with tests/evidence; broader or human-only findings remain explicit.

**Independent Test**: Every finding has severity and disposition; every bounded remediation has an acceptance condition and validation result.

- [X] T051 [US2] Review `src/TuiVision.Core/` against relevant CWE Top 25 and SSDF controls for input, buffer/cell, collection, event, error, and output boundaries; record findings or `AlreadySatisfied` evidence.
- [X] T052 [US2] Review `src/TuiVision.Controls/` for event/command dispatch, focus, dialog state, file/path handling, validation/rejection, help/status, and safe output; record findings or evidence.
- [X] T053 [US2] Review `src/TuiVision.Serialization/` for malformed/truncated/trailing/unknown/cyclic input, length/count bounds, type resolution, file/resource paths, and failure behavior; record findings or evidence.
- [X] T054 [US2] Review `src/TuiVision.Drivers.Console/` for terminal input, escape parsing, fallback behavior, environment data, output safety, and platform errors; record findings or evidence.
- [X] T055 [US2] Review `src/TuiVision.Compatibility/` for unsafe fallback, unsupported behavior, data conversion, and exception boundaries; record findings or evidence.
- [X] T056 [US2] Review `examples/` for arbitrary user-file access, unsafe persistence, secret/log output, and misleading secure-coding examples without starting new example work.
- [X] T057 [US2] Review all six test projects for negative security proof, deterministic fixtures, temporary-file cleanup, environment isolation, and proof overstatement; record findings or gaps.
- [X] T058 [US2] Review repository scripts and workflows beyond the known rename/action gaps for quoting, injection, path traversal, secret exposure, permissions, mutable dependencies, and failure handling.
- [X] T059 [US2] Assign stable finding IDs, severity, controls, impact boundary, disposition, owner, reviewer, and acceptance condition to every discovered issue in `pr-evidence.md`.
- [X] T060 [US2] Add or update focused tests before each bounded executable remediation in the affected `tests/TuiVision.*.Tests/` project; record the expected failing proof or justify why no executable change is needed.
- [X] T061 [US2] Implement only approved bounded source/test remediations in affected `src/` and `tests/` paths; do not alter public API or behavior without explicit critical/high finding evidence.
- [X] T062 [US2] Review every new/changed non-trivial code, test, CI, or script block for selective didactic inline-comment value under feature 015 guidance; record the decision in `pr-evidence.md`.
- [X] T063 [US2] Review executable diffs for runtime behavior, public API, persistence, terminal, dependency, example, and historical-intent impact; route broad impact to `FollowUp` rather than silently expanding scope.
- [X] T064 [US2] Replace any remaining accepted `Stub`, `to be populated`, or placeholder status under `docs/security/` with current evidence or an explicit non-accepted template boundary.
- [X] T065 [US2] Create root `SECURITY.md` with bilingual supported-version policy, private GitHub Security Advisory path, requested report data, response expectations, and coordinated-disclosure guidance.
- [X] T066 [US2] Record vulnerability-response organizational ownership and provider activation as human-only `Open` where repository evidence cannot prove them.
- [X] T067 [US2] Prove zero unresolved critical/high finding or stop the run; record remediation and validation links for every closed critical/high issue.
- [X] T068 [US2] Prove every medium and implementation-relevant low finding is remediated or has an accepted, owned `Open`/`FollowUp` boundary.
- [X] T069 [US2] Reconcile changed code, tests, dependencies, APIs, and package files against the bounded-remediation ledger; remove or reclassify any unlinked change.
- [X] T070 [US2] Record US2 finding counts by severity/disposition, changed paths, tests, comment-review outcomes, and residual risks in `pr-evidence.md`.

**Checkpoint**: Bounded repository findings are closed or explicitly bounded, and merge-blocking risk is zero.

## Phase 5: User Story 3 - Demonstrate Supply-Chain and Release Readiness (Priority: P2)

**Goal**: A clean checkout can reproduce package and SBOM evidence, while release/provenance/provider limits remain honest.

**Independent Test**: A reviewer can restore a pinned tool, generate/parse a non-empty CycloneDX BOM, inspect dependency results, and trace VEX/SLSA/Scorecard decisions.

- [X] T071 [US3] Create `.config/dotnet-tools.json` with CycloneDX 6.2.0 pinned as a local tool and no global-tool dependency.
- [X] T072 [US3] Restore the local tool and run a temporary CycloneDX JSON generation from `TuiVision.sln`; record actual command, filename, spec version, component count, and cleanup in `pr-evidence.md`.
- [X] T073 [US3] Add a reproducible SBOM generation/validation section to `docs/security/supply-chain-evidence.md` using temporary output and JSON assertions.
- [X] T074 [US3] Run direct/transitive vulnerable, deprecated, and outdated package reviews for `TuiVision.sln`; record all results and service/failure boundaries in `pr-evidence.md`.
- [X] T075 [US3] Classify every package result and justify any unchanged outdated package; update packages only for a concrete vulnerability, deprecation, compatibility, or approved maintenance finding.
- [X] T076 [US3] Resolve immutable full commit SHAs and readable release comments for every action currently used in `.github/workflows/*.yml`; record source tag-to-SHA evidence in `pr-evidence.md`.
- [X] T077 [US3] Replace mutable action tags with immutable full SHAs plus readable version comments across `.github/workflows/*.yml` without changing workflow semantics.
- [X] T078 [US3] Create `.github/dependabot.yml` for NuGet, GitHub Actions, and `tests/web-a11y` npm updates with bounded cadence and grouping.
- [X] T079 [US3] Add repository-controlled dependency vulnerability/deprecation and temporary SBOM validation to an appropriate `.github/workflows/` security workflow without publishing generated output or requiring new secrets.
- [X] T080 [US3] Validate all changed workflow/config YAML structurally and review least-privilege permissions, trigger scope, output retention, and immutable action dependencies.
- [X] T081 [US3] Record VEX as `N/A` only if package review finds no known shipped vulnerability; otherwise create a factual disposition or block on unresolved critical/high risk.
- [X] T082 [US3] Record SLSA/provenance as `FollowUp` with target and owner until the release pipeline produces attestable artifacts; do not fabricate provenance.
- [X] T083 [US3] Record OpenSSF Scorecard applicability, current observable repository posture, unavailable public API result if applicable, and human/provider boundary for publication/settings.
- [X] T084 [US3] Record AI-SBOM as `N/A` while AI remains development tooling only, including all runtime/product AI re-evaluation triggers.
- [X] T085 [US3] Replace the stub in `docs/security/dependency-audit.md` with current direct/transitive package inventory, commands, results, exceptions, and freshness boundary.
- [X] T086 [US3] Complete `docs/security/supply-chain-evidence.md` with SBOM, VEX, SLSA, Scorecard, action-pin, Dependabot, AI-SBOM, license, and release-boundary results.
- [X] T087 [US3] Remove temporary BOM/package output and prove no generated SBOM, package report, cache, credential, or scan output is tracked.
- [X] T088 [US3] Record US3 tool/version, component/package counts, workflow changes, applicability decisions, follow-ups, and residual risks in `pr-evidence.md`.

**Checkpoint**: Supply-chain evidence is reproducible, generated output is untracked, and external/release claims stay bounded.

## Phase 6: User Story 4 - Preserve Cross-Platform and Agent Governance (Priority: P2)

**Goal**: Critical rename tooling and shared agent context behave consistently across maintained surfaces.

**Independent Test**: Disposable Git-repository tests prove equivalent Bash/PowerShell outcomes for help, errors, preview, no-commit, explicit commit, staging isolation, normalization, and idempotence.

- [X] T089 [US4] Create failing Bash-driven contract cases in `tests/scripts/rename-lastenheft-tests.sh` for help, missing input, invalid/untracked/non-Lastenheft input, and unsafe branch/path input before script implementation.
- [X] T090 [US4] Extend `tests/scripts/rename-lastenheft-tests.sh` with failing dry-run/WhatIf, no-commit, explicit-commit, unrelated-staged-change, branch-normalization, and idempotence cases for both script implementations.
- [X] T091 [US4] Implement reusable disposable-Git-repository fixtures and semantic parity assertions in `tests/scripts/rename-lastenheft-tests.sh`; never run mutation cases in the project working tree.
- [X] T092 [US4] Implement safe parsing, bilingual help, `--dry-run`, `--no-commit`, tracked Lastenheft validation, branch normalization, idempotence, Git-aware rename, and path-isolated commit in `scripts/rename-lastenheft.sh`.
- [X] T093 [US4] Implement equivalent advanced-script behavior, comment-based help, `-WhatIf`, `-NoCommit`, validation, normalization, idempotence, Git-aware rename, and path-isolated commit in `scripts/rename-lastenheft.ps1`.
- [X] T094 [US4] Create bilingual `docs/man/rename-lastenheft.1` documenting both invocations, defaults, options, safety constraints, exit behavior, examples, and generated filename normalization.
- [X] T095 [US4] Run Bash syntax and PowerShell parser/help checks; record command, platform, result, and proof limit in `pr-evidence.md`.
- [X] T096 [US4] Align `Directory.Build.props` for the next script-test run, increment the manual build counter, run `tests/scripts/rename-lastenheft-tests.sh`, and record all case counts/results.
- [X] T097 [US4] Verify negative cases fail before filesystem/index/commit mutation in both implementations and record evidence.
- [X] T098 [US4] Verify no-commit mode performs only the intended Git rename and creates zero commits in both implementations.
- [X] T099 [US4] Verify explicit commit includes only old/new Lastenheft paths while unrelated staged content remains staged and absent from the commit.
- [X] T100 [US4] Verify branch slash normalization, unsafe-segment rejection, and already-archived idempotence are equivalent in both implementations.
- [X] T101 [US4] Integrate script contract checks into the existing homogeneity/security CI path with Linux/macOS and PowerShell coverage where repository-controlled runners support it.
- [X] T102 [US4] Review `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` together for shared feature-016 security/tooling guidance.
- [X] T103 [US4] Review `.specify/templates/` and local presets for implementation impact; record `N/A` unless a concrete repository-owned defect is found.
- [X] T104 [US4] Run `.specify/scripts/bash/update-agent-context.sh` for `codex`, `claude`, `gemini`, and `copilot` after any plan/technology context change; manually preserve explicit 016 plan markers and semantic parity.
- [X] T105 [US4] Record script-case counts, platform limits, agent-surface results, template impact, intentional divergences, and residual risks in `pr-evidence.md`.

**Checkpoint**: Critical scripts are behaviorally equivalent and agent context is synchronized without hidden template expansion.

## Phase 7: User Story 5 - Keep the Baseline Teachable and Accessible (Priority: P3)

**Goal**: Apprentices and assistive-technology users can trace security decisions without visual-only meaning or unexplained jargon.

**Independent Test**: A sampled control remains understandable from source through decision, evidence, remediation/follow-up, and validation in text-first output.

- [X] T106 [US5] Review all changed learner-facing Markdown and `SECURITY.md` for German-first/English-second CEFR-B2 structure and correct German umlauts/`ß`.
- [X] T107 [US5] Ensure status, severity, ownership, result, residual risk, and next action are expressed in text and never depend on color, icons, layout, or pointer interaction.
- [X] T108 [US5] Review headings, tables, lists, links, ASCII/text diagrams, and fenced code language tags in changed documentation for semantic and screen-reader-friendly structure.
- [X] T109 [US5] Ensure `docs/security/README.md` links all project evidence with meaningful link text and makes missing/human-only/follow-up states explicit.
- [X] T110 [US5] Review all new/changed non-trivial script, workflow, test, or source logic for concise why/trade-off/constraint/proof comments; avoid comments that restate obvious code.
- [X] T111 [US5] Record that changed `docs/security/` pages trigger DocFX, web-A11Y, and representative text-first review in `pr-evidence.md`.
- [X] T112 [US5] Re-run the sample control trace in `docs/security/control-assessment.md` as an apprentice-oriented acceptance walk and record any ambiguity remediation.
- [X] T113 [US5] Record changed-document language/A11Y review results, exceptions, and DocFX page sample paths in `pr-evidence.md`.
- [X] T114 [US5] Confirm no user-facing security statement claims certification, legal conformity, provider assurance, or vulnerability absence beyond the recorded evidence date/scope.

**Checkpoint**: Feature evidence is teachable, bilingual where learner-facing, semantic, and text-first.

## Phase 8: Governance, Validation, and Acceptance

**Purpose**: Complete all preset checkpoints and execute the project-wide proof path in versioned order.

- [X] T115 Record Security Governance v0.6.0 rows for SSDF, CWE Top 25, package/dependency review, SBOM, VEX, SLSA, Scorecard, AI-SBOM, and regulatory screening in `pr-evidence.md` with complete fields.
- [X] T116 Record Architecture Governance v0.5.0 rows for STRIDE/CIA/CAPEC, arc42, S-ADR, SAMM, Zero Trust, BSI C3A, and BSI C5 in `pr-evidence.md` with complete fields.
- [X] T117 Record iSAQB Architecture Governance v0.2.0 rows for goals, context/runtime views, quality scenarios, decisions, risks, and technical debt in `pr-evidence.md`.
- [X] T118 Record A11Y Governance v0.4.0 rows for bilingual/text-first evidence, WCAG 2.2 AA generated HTML, inclusive language, and didactic-comment review in `pr-evidence.md`.
- [X] T119 Record Cross-Platform Governance v0.2.0 rows for Bash/PowerShell contract, help, man page, dry-run/WhatIf, exit behavior, test parity, and platform limits in `pr-evidence.md`.
- [X] T120 Record Agent Parity Governance v0.3.0 rows for five maintained surfaces, four context refreshes, `.specify/templates/` impact, and any divergence in `pr-evidence.md`.
- [X] T121 Record explicit applicability rows for ASVS, owned crypto, Zero Trust, BSI C3A/C5, AI-SBOM, NIS2, CRA, EU AI Act, DORA, DPIA, VEX, SLSA, and Scorecard; complete every `N/A`, `Open`, or `FollowUp` field.
- [X] T122 Re-run the control-ID/schema validator and record 157/157 coverage, complete fields, status counts, evidence-link checks, and zero duplicate/missing/unknown IDs.
- [X] T123 Scan accepted security evidence for `Stub`, `to be populated`, placeholders, empty starter rows, stale feature-only status, and unsupported claims; remediate every actionable result.
- [X] T124 Scan Git tracking/status for `_site/`, generated API YAML, TestResults, coverage, SBOM, package reports, caches, logs, credentials, and `tv203s/` edits; remove all prohibited output.
- [X] T125 Align `Directory.Build.props` to `1.16.<next-patch>.<current-build>` before validation commits without incrementing the build counter.
- [X] T126 Run `dotnet format --verify-no-changes` and record the exact result in `pr-evidence.md`.
- [X] T127 Run `git diff --check`, Markdown marker/fence checks, YAML structural checks, Bash syntax, and PowerShell parser checks; record results.
- [X] T128 Re-run vulnerable/deprecated/outdated package checks and temporary CycloneDX generation/JSON validation; record final results and delete output.
- [X] T129 Increment the manual build counter in `Directory.Build.props` immediately before the full Release test command and keep all three version fields aligned.
- [X] T130 Run `dotnet test TuiVision.sln --configuration Release`; record per-project and total pass/fail/skip counts and failure boundary.
- [X] T131 Increment the manual build counter immediately before the canonical coverage command and keep all three version fields aligned.
- [X] T132 Validate `coverlet.runsettings`, run `dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`, and record per-required-assembly line coverage against 70%.
- [X] T133 Update active feature context across all affected maintained agent surfaces and move `Pflichtenheft.md` `>>> NAECHSTER SCHRITT <<<` only after implementation acceptance, pointing to the highest-priority remaining intake.
- [X] T134 Archive `Lastenheft_Secure-Development-Hardening.md` with the new commit-free rename mode for branch 016; verify no implicit commit and retain traceability.
- [X] T135 Update `docs/project-statistics.md` after T133-T134 with the complete feature-016 work window, production/test/documentation counts, validation, 80/125-lines-per-day baselines, and refreshed final summary/diagrams.
- [X] T136 Run `docfx docfx.json` after all DocFX-included Markdown changes; record warnings/errors, security/statistics page inclusion, generated-output cleanup requirement, and failure boundary.
- [X] T137 Increment the manual build counter immediately before `npm run test:docfx`, run the Playwright/axe suite from `tests/web-a11y/`, and record result plus representative text-first security/statistics page review.
- [X] T138 Run `scripts/scan-agent-secrets.sh`, the PowerShell parity path, and available Gitleaks validation; record local/CI proof boundaries and false-positive dispositions.
- [X] T139 Compare all five maintained agent surfaces after T133 for shared-policy parity and verify each explicit marker/context reference remains valid after generation.
- [X] T140 Complete `pr-evidence.md` with task counts, control/status counts, findings/remediations, validation, governance, human-only decisions, residual risks, final generated-output cleanup, changed files, and PR-ready summary.

**Checkpoint**: Local implementation is complete, all evidence is actionable-clean, and the branch is ready for remote delivery.

## Phase 9: Commit, PR, Review, Merge, and Local Main Sync

**Purpose**: Complete the autonomous experiment through reviewed remote delivery.

- [X] T141 Align `Directory.Build.props` to `1.16.<next-patch>.<current-build>` before commit/push without incrementing the build counter unless another build/test ran.
- [X] T142 Run final `git status --short --branch -uall`, `git diff --check`, tracked-generated-output scan, and diff scope review; update `pr-evidence.md` if the final boundary changed.
- [X] T143 Commit the complete implementation with an intentional Spec-Kit message; verify the commit contains only feature-016 scope and aligned version metadata.
- [X] T144 Push `016-secure-development-hardening` to `origin` and verify upstream/remote commit identity.
- [X] T145 Create a pull request from `pr-evidence.md` with scope, findings, security/governance decisions, validation, residual risks, and follow-ups.
- [X] T146 Wait for required CI and automated reviews; address every actionable comment or failure with bounded remediation, revalidation, aligned version, commit, push, thread response, and renewed convergence.
- [ ] T147 Merge the PR only when required checks pass, no actionable review remains, and no critical/high risk is unresolved; record the merge result.
- [ ] T148 Switch locally to `main`, run `git pull --ff-only origin main`, and verify clean status plus equality with `origin/main`.

**Checkpoint**: The feature is merged and the local workspace is clean on synchronized `main`.

## Dependencies and Execution Order

- Phase 1 blocks all later phases.
- Phase 2 blocks all user stories because evidence schemas and inventories must exist before classification or remediation.
- US1 establishes the auditable baseline and is the MVP.
- US2 depends on US1 classification so findings have control IDs and evidence destinations.
- US3 may begin after US1 but is serialized after US2 to keep workflows, evidence, and finding status coherent.
- US4 depends on the foundational script inventory and is serialized after supply-chain workflow edits.
- US5 depends on all learner-facing documentation and logic changes being known.
- Phase 8 depends on US1 through US5 and must be repeated after any remediation.
- Phase 9 depends on local acceptance and the repeated `/speckit-analyze` convergence run performed before `/speckit-implement` and after any material artifact remediation.

## User Story Task Counts

| Story | Tasks | Independent result |
|---|---:|---|
| US1 | T023-T050 (28) | 157-control auditable project baseline |
| US2 | T051-T070 (20) | Bounded findings remediated or explicitly owned |
| US3 | T071-T088 (18) | Reproducible SBOM and supply-chain posture |
| US4 | T089-T105 (17) | Bash/PowerShell and agent parity |
| US5 | T106-T114 (9) | Text-first, bilingual, teachable evidence |

## Requirement Coverage Map

| Requirement group | Primary task coverage |
|---|---|
| FR-001..FR-010 | T001-T011, T020-T035, T133-T140 |
| FR-011..FR-018 | T014-T022, T036-T070 |
| FR-019..FR-027 | T065-T088, T115-T138 |
| FR-028..FR-033 | T089-T114, T119-T120, T133, T139 |
| FR-034..FR-036 | T019, T087, T124, T133-T148 |
| CR-001..CR-009 | T002-T007, T020, T115-T120, T133-T140 |
| CR-010..CR-016 | T038-T048, T073-T088, T121-T138 |
| SC-001..SC-005 | T023-T070, T122-T123, T140 |
| SC-006..SC-009 | T071-T105, T128, T138-T139 |
| SC-010..SC-014 | T004, T106-T114, T122-T148 |

## Implementation Strategy

1. Build the evidence schema and complete the 157-control MVP.
2. Triage and close bounded findings before broad documentation claims.
3. Harden supply-chain configuration and prove a clean-checkout SBOM path.
4. Implement rename parity test-first and synchronize agent context.
5. Complete A11Y, governance, project-wide validation, and generated-output cleanup.
6. Run repeated Analyze to actionable-clean, then execute implementation and remote-delivery phases without skipping conditional outcomes.
