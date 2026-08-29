# Tasks: Sandbox Secure Development Hardening

## Phase 1 - Preflight and Evidence Foundation

- [x] T001 Verify clean synchronized `main`, Feature 043 completion, and next feature number 044
- [x] T002 Validate the current Series intake review and exact sandbox-intake hash
- [x] T003 Verify the sandbox intake is the single `Eligible` series target
- [x] T004 Run `specify check` and resolve Codex model routing to `Aligned`
- [x] T005 Create branch `044-sandbox-secure-development-hardening` and align `.specify/feature.json`
- [x] T006 Create and validate `autonomous-run-state.json` before semantic implementation
- [x] T007 Create `pr-evidence.md` before the first implementation change
- [x] T008 Declare gate requirements and MergeAndSync/approval-only authority boundaries

## Phase 2 - Specify, Clarify, Checklists, and Plan

- [x] T009 Create `spec.md` from the binding intake with explicit CL-12 cardinality and hard scope boundaries
- [x] T010 Complete `checklists/requirements.md` with no clarification or placeholder marker
- [x] T011 Run a focused Clarify pass and record that no planning-material ambiguity remains
- [x] T012 Research the exact read-only sandbox commit, source hashes, controls, mounts, tools, and open approvals
- [x] T013 Create `research.md`, `data-model.md`, `quickstart.md`, and the acceptance contract
- [x] T014 Create `plan.md` with vertical slice, proof levels, shared-writer order, and trigger-based validation
- [x] T015 Complete `checklists/plan-quality.md` and remediate every planning note
- [x] T016 Generate dependency-ordered `tasks.md` with stable IDs and proportional validation
- [x] T017 Run Analyze across spec, plan, contract, tasks, governance, and constitution
- [x] T018 Remediate every Critical/High finding and disposition each Medium before implementation

## Phase 3 - Vertical Validator Slice

- [x] T019 [US3] Add a failing fixture with one missing CL-12 row
- [x] T020 [US3] Prove the missing-row fixture fails before the positive implementation exists
- [x] T021 [US3] Implement the structured read-only Python validator core
- [x] T022 [US3] Add the Bash entry point with strict mode, quoting, root handling, and help
- [x] T023 [US3] Add the PowerShell advanced-function entry point `Test-SandboxApplicability` with bilingual help
- [x] T024 [US3] Add the Unix man page and keep Bash/PowerShell exit behavior equivalent
- [x] T025 [US3] Add positive and negative unittest coverage for all acceptance-contract failures

## Phase 4 - US1 Mount and Write Boundaries

- [x] T026 [US1] Bind sandbox repository, commit, default branch, and accepted source hashes in `assessment.json`
- [x] T027 [US1] Record portable mount roles and restrict `TuiVisionCheckout` to the selected checkout
- [x] T028 [US1] Separate read-write project, read-only configuration, named volumes, and audit metadata
- [x] T029 [US1] Exclude home, desktop, downloads, unrelated projects, credentials, and private host paths
- [x] T030 [US1] Create the bilingual text-first `mount-policy.md`

## Phase 5 - US2 Execution and Proof Boundaries

- [x] T031 [US2] Map build, test, format, DocFX, A11Y, dependency/SBOM, secret, and agent-parity checks
- [x] T032 [US2] Assign every check one location and one proof level
- [x] T033 [US2] Distinguish static, practical, platform, provider, and human evidence
- [x] T034 [US2] Record non-permitted credential and broad-host operations
- [x] T035 [US2] Create the bilingual text-first `execution-matrix.md`

## Phase 6 - US3 CL-12 Assessment

- [x] T036 [US3] Record exactly one decision for each `CL-12-01` through `CL-12-12`
- [x] T037 [US3] Add rationale, evidence, owner, reviewer, date, risk, follow-up, and trigger to every row
- [x] T038 [US3] Reconcile CL-12 decisions with Feature 016 without rewriting its historical baseline
- [x] T039 [US3] Run both validator entry points against the canonical assessment

## Phase 7 - US4 Secrets and Agent State

- [x] T040 [US4] Document separate agent-state and build-cache volumes without local account data
- [x] T041 [US4] Document secret injection, redaction, prompt, log, screenshot, and Git boundaries
- [x] T042 [US4] Verify agent guidance and `.specify/templates/` need no shared-rule update
- [x] T043 [US4] Run repository and agent secret scans without exposing matched values

## Phase 8 - US5 Recommendation and Security Navigation

- [x] T044 [US5] Set the single final recommendation to `ConditionallyUsable` with exact boundaries
- [x] T045 [US5] Keep formal approval, data class, provider, egress, and missing platform proof `Open`
- [x] T046 [US5] Create the bilingual assessment `README.md` with one safe next action
- [x] T047 [US5] Link the assessment from `docs/security/README.md` and DocFX navigation where applicable
- [x] T048 [US5] Confirm no non-empty finding-derived follow-up intake is required and start no feature

## Phase 9 - Governance and Repository Evidence

- [x] T049 Record all twelve installed preset versions and applicability in `pr-evidence.md`
- [x] T050 Record NIST SSDF, CWE, ASVS, SBOM, VEX, SLSA, Scorecard, AI-SBOM, and regulatory trigger decisions
- [x] T051 Record STRIDE/CIA/CAPEC, S-ADR, arc42, Zero Trust, SAMM, BSI C3A, and BSI C5 decisions
- [x] T052 Record A11Y, script parity, agent parity, intake, routing, autonomous, parallel, and historical-source decisions
- [x] T053 Update `docs/project-statistics.md` after implementation completion
- [x] T054 Align `Directory.Build.props` to the numbered-branch version before commit or push without incrementing build counter unless a build/test runs

## Phase 10 - Local Validation

- [x] T055 Run Python positive and negative validator tests
- [x] T056 Run Bash and PowerShell validators on canonical Evidence and compare outcomes
- [x] T057 Run Bash syntax, PowerShell parser/PSScriptAnalyzer, help, and man-page checks
- [x] T058 Run read-only sandbox commit, hash, diff, and isolated Compose-config checks
- [x] T059 Run `git diff --check` and `dotnet format --verify-no-changes`
- [x] T060 Run homogeneity, maintained-agent parity, secret, dependency, and supply-chain checks
- [x] T061 Run DocFX, Playwright/Axe, and representative Lynx text review
- [x] T062 Confirm product build/test/coverage is not locally triggered by the final delivery set, or increment once per invocation if scope changes
- [x] T063 Validate the complete intended delivery set read-only and reconcile staged paths

## Phase 11 - MergeAndSync Delivery

- [ ] T064 Commit and push the non-empty Feature 044 delivery after version alignment
- [ ] T065 Create the feature PR from `pr-evidence.md` and monitor required checks and reviews
- [ ] T066 Map every acceptance gate to exact-head workflow, job, platform, and executed command
- [ ] T067 Resolve every actionable review thread; unavailable review remains missing, never Pass
- [ ] T068 Generate and validate temporary schema-2.0 PreMerge evidence for the reviewed head
- [ ] T069 Use admin bypass only if Human Approval is the sole open rule after all technical gates pass
- [ ] T070 Merge with a merge commit, delete the feature branch, and synchronize clean local `main`
- [ ] T071 Create a non-recursive evidence-only closeout only if post-merge facts require it

## Phase 12 - Lifecycle and Retrospective

- [ ] T072 Mark the sandbox intake `Completed` and advance the existing series without starting the next intake
- [ ] T073 Update Feature 044 state to terminal `Retrospective`, `Completed`, and `nextExactAction: N/A`
- [ ] T074 Complete `retrospective.md` with `NoPromotion` unless a reproducible provider-neutral preset defect was found
- [ ] T075 Verify final task count, clean `HEAD == origin/main`, no product/image/external-repository change, and no next feature

## Dependencies

- T001-T008 precede all semantic work.
- T009-T018 precede implementation.
- T019-T025 are the test-first vertical slice and precede T026-T048.
- T026-T048 may be reviewed by story but write the canonical assessment serially.
- T049-T063 precede delivery.
- T064-T071 are strictly serial remote operations.
- T072-T075 occur only after delivery facts are known.

No task is marked complete from intent alone. Conditional `N/A` outcomes are
recorded in `pr-evidence.md` before the task is checked.
