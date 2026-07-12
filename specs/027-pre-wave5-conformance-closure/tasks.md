# Tasks: Pre-Wave-5 Conformance Closure

**Input**: Feature-027 spec, plan, research, data model, quickstart, contract,
checklists, binding Lastenheft, and merged Feature-024 evidence.

**Scope**: Evidence-only closure. Any product, API, dependency, example, or
historical-source drift stops implementation and routes to a reviewed audit
revision.

## Phase 1: Setup and Evidence Foundation

- [X] T001 Verify clean branch `027-pre-wave5-conformance-closure`, `HEAD` ancestry from merged PR #65, and `.specify/feature.json` ownership; record in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`
- [X] T002 Run `specify check` and record tool availability in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`
- [X] T003 Run `.specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks` and record exact feature/task resolution in `specs/027-pre-wave5-conformance-closure/closure-evidence.md`
- [X] T004 Verify all four feature checklists have zero incomplete items and record that actual execution results are owned by `closure-evidence.md`
- [X] T005 Read AGENTS, Constitution, Lastenheft 09, all 027 artifacts, all 024 artifacts, and the archived Lastenheft 08; record the accepted source hierarchy
- [X] T006 Create `specs/027-pre-wave5-conformance-closure/closure-evidence.md` with identity, authority, baseline, revalidation, governance, validation, gate, delivery, resume, and retrospective sections
- [X] T007 Create `specs/027-pre-wave5-conformance-closure/pr-evidence.md` as the PR-facing summary that references `closure-evidence.md` without duplicating current-head facts
- [X] T008 Record explicit `MergeAndSync` authority, stop boundaries, no-empty 025/026 rule, causal closeout path, and narrow bypass boundary
- [X] T009 Record the seven resolved preset layers with exact versions and template composition results
- [X] T010 Record immutable protected-path baselines for `src/`, `examples/`, package/project metadata, `tv203s/`, `TVDEMOS/`, and `TVFM/`
- [X] T011 Record accepted post-audit evidence/status path classes and the Feature-024 product baseline SHA in `closure-evidence.md`
- [X] T012 Run first static `git diff --check`, checklist marker scan, and secret preflight; record results before validation commands

---

## Phase 2: User Story 1 - Revalidate Merged Audit Baseline

**Goal**: Prove exact audit identity, inventories, decisions, sources, proofs,
findings, and owner sets on the closure head.

**Independent Test**: Focused conformance evidence suite plus independent JSON,
path, reflection, and protected-diff checks.

- [X] T013 Record Feature-024 run ID, schema identity, 16 domain IDs, and 48 contract IDs from `conformance-audit.json`
- [X] T014 Verify and record 151 unique historical items against `docs/porting-status.md`
- [X] T015 Verify and record 119 maintained production C# files against live tracked `src/` paths
- [X] T016 Verify and record 176 exported public types through the existing reflection-backed validator
- [X] T017 Verify and record 15 external Free Vision source records and pinned commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2`
- [X] T018 Verify and record all 94 concrete `path::method` proof references
- [X] T019 Verify primary decisions are exactly 13 `Aligned`, 34 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 0 `BehavioralDrift`, and 0 `EvidenceGap`
- [X] T020 Verify Free Vision relations are exactly 22 `CorroboratesOriginal`, 10 `CorroboratesModernization`, 3 `DivergesFromOriginal`, and 13 `NotApplicable`
- [X] T021 Verify findings and `Core025`, `ComponentData026`, `AcceptedFollowUp`, and `ProductDecision` owner sets are exactly empty
- [X] T022 Verify no `specs/025-*`, `specs/026-*`, local/remote 025/026 branch, or open/merged 025/026 PR exists; record no-empty-work evidence
- [X] T023 Compare protected product and historical paths between Feature-024 product merge `5c0a4d7` and current head; classify every path or stop
- [X] T024 Verify post-audit changes are limited to evidence, closeout, retrospective, intake, specification, agent context, version, and statistics surfaces
- [X] T025 Revalidate the external Free Vision checkout hash when available; otherwise record the exact local provenance proof limit without fetching new source into Git
- [X] T026 Align `Directory.Build.props` to the current `1.27.<patch>.<build>` version and increment the manual build counter once before the focused test command
- [X] T027 Run focused Release `ConformanceAuditEvidenceTests` and record exact pass/fail/skip totals and version in `closure-evidence.md`
- [X] T028 Run independent `jq` cardinality queries and compare them with focused test results
- [X] T029 Mark baseline revalidation checks `Pass` only when all T013-T028 results agree; otherwise set Wave 5 `Blocked` and stop
- [X] T030 Mark the User Story 1 revalidation rows complete in `closure-evidence.md`

---

## Phase 3: User Story 2 - Complete Integration and Release Gates

**Goal**: Prove repository-wide release readiness on the closure head.

**Independent Test**: Full Release, canonical coverage, documentation/A11Y,
security, and protected-scope gates all pass.

- [X] T031 Run `git diff --check` after baseline evidence edits and record result
- [X] T032 Run `dotnet format --verify-no-changes --no-restore` and record result
- [X] T033 Align the numbered version and increment the manual build counter once before the full Release test command
- [X] T034 Run the full Release test suite in one explicit invocation and record project-level and total pass/fail/skip counts
- [X] T035 Validate `coverlet.runsettings` with `xmllint --noout` and record the gate assembly set
- [X] T036 Align the numbered version and increment the manual build counter once before the canonical coverage command
- [X] T037 Run the canonical Coverlet command in one explicit invocation and preserve only transient local result files
- [X] T038 Extract and record assembly-specific Core line coverage from its canonical report
- [X] T039 Extract and record assembly-specific Controls line coverage from its canonical report
- [X] T040 Extract and record assembly-specific Serialization line coverage from its canonical report
- [X] T041 Extract and record assembly-specific Compatibility line coverage from its canonical report
- [X] T042 Extract and record assembly-specific Drivers.Console line coverage from its canonical report
- [X] T043 Verify each required assembly is at least 70 percent and classify excluded example collector notices separately
- [X] T044 Run `docfx docfx.json` and record warning/error totals
- [X] T045 Run `tests/web-a11y` DocFX plus Playwright/Axe smoke and record page/test totals
- [X] T046 Run UTF-8 Lynx over landing, project statistics, retrospective, and representative API pages; record semantic text findings
- [X] T047 Run `scripts/scan-agent-secrets.sh --fail-on-high` and record high/medium/low boundaries
- [X] T048 Remove transient TestResults, DocFX, Playwright, and cache outputs; verify no generated output is tracked
- [X] T049 Re-run dependency, package, API, runtime, example, external-source, and historical-source diff scans and record exact empty protected sets
- [X] T050 Mark User Story 2 validation rows complete in `closure-evidence.md` only when T031-T049 pass

---

## Phase 4: User Story 3 - Formal Wave-5 Gate Decision

**Goal**: Atomically align project status on a passed Feature-027 closure.

**Independent Test**: All maintained status and agent surfaces agree, 025/026
remain absent, and Wave 5 is named as the next eligible intake.

- [X] T051 Reconcile all `CL-027-*` checks with one result and complete owner, reviewer, date, residual-risk, follow-up, and re-evaluation fields
- [X] T052 Record final local gate decision `Passed` or `Blocked`; continue release tasks only for `Passed`
- [X] T053 Update `specs/024-tv203-freevision-conformance-audit/pre-wave5-gate.md` with the reviewed Feature-027 local closure state without rewriting 024 decisions
- [X] T054 Mark the Pre-Wave-5 Framework audit and hardening item complete in `Pflichtenheft.md`
- [X] T055 Update `Lastenheft_Abarbeitungsreihenfolge.md` to mark 027 completed and Wave 5 as the next eligible intake
- [X] T056 Replace planning context with completed Feature-027 context in `AGENTS.md`
- [X] T057 Apply the identical completed Feature-027 context to `CLAUDE.md`
- [X] T058 Apply the identical completed Feature-027 context to `GEMINI.md`
- [X] T059 Apply the identical completed Feature-027 context to `.github/copilot-instructions.md`
- [X] T060 Apply the identical completed Feature-027 context to `.github/agents/copilot-instructions.md`
- [X] T061 Verify byte-identical Feature-027 blocks across all five maintained agent surfaces
- [X] T062 Update `docs/project-statistics.md` with chronological 027 implementation evidence, final totals, diagrams, and CEFR-B2 explanations while keeping `Gesamtstatistik` last
- [X] T063 Prepare the bilingual PR summary, exact closure counts, non-triggered governance checks, residual risk, and Wave-5 release boundary in `pr-evidence.md`
- [X] T064 Archive `Lastenheft_09_Pre-Wave5-Conformance-Closure.md` with `bash scripts/rename-lastenheft.sh --no-commit Lastenheft_09_Pre-Wave5-Conformance-Closure.md 027-pre-wave5-conformance-closure` after local closure passes
- [X] T065 Mark User Story 3 and all local closure rows complete in `closure-evidence.md`, leaving remote, retrospective, and handoff rows open

---

## Phase 5: User Story 4 - Delivery, Closeout, and Learning

**Goal**: Deliver under explicit authority and complete reusable learning.

- [X] T066 Re-run `docfx docfx.json` after final published status changes and record 0-error result
- [X] T067 Re-run Playwright/Axe after the final DocFX regeneration and record result
- [X] T068 Re-run UTF-8 Lynx on all changed published pages and record result
- [X] T069 Re-run final secret, generated-output, protected-scope, dependency, API, example, and historical-source scans
- [X] T070 Re-run `git diff --check`, `dotnet format --verify-no-changes --no-restore`, checklist completeness, marker, and task-count checks
- [X] T071 Align `Directory.Build.props` to final pre-commit `1.27.<patch>.<build>` without incrementing build unless another explicit build/test ran
- [X] T072 Commit the complete local closure with the Constitution-required Copilot co-author trailer
- [ ] T073 Push `027-pre-wave5-conformance-closure` and open the feature PR with `pr-evidence.md` summary
- [ ] T074 Monitor PR-context technical checks to completion; classify duplicate push runs as operational noise
- [ ] T075 Record Claude and Copilot availability honestly and inspect conversation comments
- [ ] T076 Query GraphQL review threads and remediate every actionable item within accepted scope
- [ ] T077 Re-run affected local gates after any review remediation and align version before additional commit/push
- [ ] T078 Verify all technical checks green, zero actionable threads, and exact reviewed head before merge
- [ ] T079 Merge with merge commit, using the narrow admin bypass only if Human Approval is the sole blocker
- [ ] T080 Delete remote feature branch, switch to `main`, fetch/prune/pull fast-forward, and prove clean `HEAD == origin/main`
- [ ] T081 Create one non-recursive evidence-only closeout branch because merge and synchronized-main facts became true post-merge
- [ ] T082 Record feature PR, checks, unavailable reviews, threads, bypass, merge SHA, branch deletion, and synchronized main in `closure-evidence.md`
- [ ] T083 Mark post-feature-merge delivery tasks T073-T082 and matching `closure-evidence.md` rows complete in the single closeout commit; leave later retrospective/handoff and self-referential closeout-provider facts to their actual boundaries
- [ ] T084 Push and open the non-empty closeout PR without writing its own PR URL or merge result into repository evidence
- [ ] T085 Converge closeout technical checks and threads, merge under the same narrow authority, delete branch, and synchronize main
- [ ] T086 Run `$speckit-autonomous-retrospective` and classify every observation; create no TuiVision branch if no non-empty local improvement exists
- [ ] T087 Re-run Home-Baseline PowerShell and Bash homogeneity JSON/error-channel proof from its current package branch and record result
- [ ] T088 Update the Home-Baseline Feature-027 workitem/field input only for a real reproducible portable observation; otherwise record `NoPromotion`
- [ ] T089 Update `github/spec-kit#3479` only if a published/revalidated preset improvement now exists; otherwise leave the issue unchanged with rationale
- [ ] T090 Finish with T001-T090 status, changed files, exact counts, validation, review, PR/merge identifiers, preset decision, and clean synchronized `main`; do not start Wave 5

## Dependencies and Execution Order

- T001-T012 establish evidence and must complete before any validation command.
- User Story 1 blocks every later story.
- User Story 2 blocks formal gate release.
- User Story 3 writes shared status surfaces serially after all local gates pass.
- User Story 4 begins only after the final local diff is validated.
- No task is parallel-marked because shared evidence, version, status,
  statistics, agent, or remote state is involved in every phase.

## Requirement and Success-Criteria Coverage

| Requirement set | Primary task coverage |
|---|---|
| FR-001 to FR-006: immutable audit, exact counts, decisions, findings, results, stop routing | T005-T011, T013-T030, T051-T052 |
| FR-007 to FR-011: focused, full, coverage, static, DocFX/A11Y/scope gates | T026-T050, T066-T070 |
| FR-012 to FR-014: read-only historical/external scope and no empty 025/026 | T010, T022-T025, T048-T049, T069 |
| FR-015 to FR-018: evidence, governance, helper integrity, serialized writers | T006-T012, T051, T056-T063, T087-T089 |
| FR-019: version and build-counter contract | T026, T033, T036, T071, T077 |
| FR-020 to FR-022: status, agent parity, statistics, Lastenheft archive | T053-T065 |
| FR-023 to FR-025: authority, review, closeout, retrospective, handoff | T073-T090 |
| FR-026: bilingual text-first accessibility | T044-T046, T062-T068 |
| SC-001 to SC-003: exact revalidation and empty remediation work | T013-T030 |
| SC-004 to SC-006: complete gates, protected diff, governance completeness | T031-T052, T066-T070 |
| SC-007: synchronized formal status | T053-T065 |
| SC-008: remote merge and clean synchronized main | T073-T085, T090 |
| SC-009: no markers/placeholders | T004, T012, T070 |
