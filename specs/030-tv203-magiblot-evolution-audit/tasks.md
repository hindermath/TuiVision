# Tasks: TV203 and magiblot/tvision Evolution Audit

**Input**: All accepted artifacts under `specs/030-tv203-magiblot-evolution-audit/`
**Delivery mode**: `MergeAndSync`
**Scope**: Read-only audit plus test-only evidence validation

## Phase 1: Setup and Evidence Foundation

- [X] T001 Verify branch, feature metadata, clean ownership, and synchronized base in `pr-evidence.md`
- [X] T002 Verify Spec Kit and the seven-preset matrix in `pr-evidence.md`
- [X] T003 Validate all feature checklists have zero incomplete items
- [X] T004 Create and validate `autonomous-run-state.json`
- [X] T005 Create `autonomous-gate-requirements.json` with stable Feature-030 gate IDs
- [X] T006 Validate the gate-requirements JSON shape and record its hash
- [X] T007 Record binding inputs and immutable Feature-024/025/026/028/029 hashes in `pr-evidence.md`
- [X] T008 Record protected write roots and generated-output exclusions in `pr-evidence.md`
- [X] T009 Record shared single-writer paths and candidate inventory rules in `pr-evidence.md`
- [X] T010 Record branch version and one-counter-per-command rules in `pr-evidence.md`
- [X] T011 Persist the random interruption phase commitment in ignored local evidence only
- [X] T012 Validate the initial state with the installed Bash validator
- [X] T013 Record local PowerShell validator availability honestly

## Phase 2: External Source Freeze

- [X] T014 Create or reuse an external detached magiblot checkout outside TuiVision
- [X] T015 Verify upstream repository URL and exact commit
- [X] T016 Verify exact tree object
- [X] T017 Verify commit timestamp and subject
- [X] T018 Verify multipart `COPYRIGHT` SHA-256 and provenance summary
- [X] T019 Verify the external checkout is clean and outside tracked paths
- [X] T020 Inventory required public headers for all comparison chapters
- [X] T021 Inventory required core application/view/event/rendering sources
- [X] T022 Inventory required dialog/menu/editor/help/resource sources
- [X] T023 Inventory required platform/input/width/terminal sources
- [X] T024 Inventory relevant upstream tests and real examples
- [X] T025 Create `magiblot-source-manifest.md` with stable `MBSR001+` IDs
- [X] T026 Record SHA-256 and pinned permalinks for every selected source
- [X] T027 Confirm no external source, fixture, binary, cache, or build output is tracked

## Phase 3: Test-First Validator and D02 Slice

- [X] T028 Review complete compile surface for the new test-only validator
- [X] T029 Add `MagiblotEvolutionAuditEvidenceTests.cs` with exact pin tests
- [X] T030 Add closed relation, observation, disposition, owner, and governance vocabularies
- [X] T031 Add reciprocal source/contract/proof/consumer validation
- [X] T032 Add TG/MB disposition and canonical-finding validation
- [X] T033 Add Primary-Owner and acyclic dependency validation
- [X] T034 Add deterministic follow-up numbering validation
- [X] T035 Add malformed, unknown, duplicate, orphan, and cycle rejection tests
- [X] T036 Increment the manual build counter for the first targeted red invocation
- [X] T037 Run the targeted Feature-030 tests and accept only missing-dataset red failures
- [X] T038 Record red proof and compile boundary in `pr-evidence.md`
- [X] T039 Create the initial `magiblot-evolution-audit.json` run/source skeleton
- [X] T040 Create the initial `combined-conformance-findings.json` skeleton
- [X] T041 Complete D02 source records for C004-C006
- [X] T042 Complete D02 magiblot relations for C004-C006
- [X] T043 Complete MB observations for C004-C006
- [X] T044 Complete TG/MB dispositions for the D02 slice
- [X] T045 Link D02 consumers, historical intent, and current real-path proof
- [X] T046 Increment the manual build counter for the isolated D02 green invocation
- [X] T047 Run the isolated D02 validator test green
- [X] T048 Record the vertical-slice result in `pr-evidence.md`

## Phase 4: Complete Source, Contract, and Consumer Review

- [X] T049 Complete application, lifecycle, event-loop, shutdown, and dispatch chapter
- [X] T050 Complete view ownership, focus, modality, window, and dialog chapter
- [X] T051 Complete coordinates, layout, clipping, resize, and desktop chapter
- [X] T052 Complete DrawBuffer, screen flush, cell model, and rendering chapter
- [X] T053 Complete UTF-8, width, combining, color, and palette chapter
- [X] T054 Complete keyboard, mouse, capture, clipboard, and input protocol chapter
- [X] T055 Complete driver, terminal state, signal, capability, and fallback chapter
- [X] T056 Complete controls, menus, StatusLine, dialog, and validation chapter
- [X] T057 Complete editor, file, help, resource, and persistence chapter
- [X] T058 Complete testability, fake/headless, and real-path proof chapter
- [X] T059 Complete Wave-5 and Wave-6 consumer relevance chapter
- [X] T060 Complete intentional-deviation, shared-bias, finding, owner, and closure chapter
- [X] T061 Ensure every accepted contract has exactly one relation
- [X] T062 Ensure every accepted contract has exactly one `MB001+` observation
- [X] T063 Ensure every relation references valid source IDs and TuiVision proof
- [X] T064 Ensure every `NotApplicable` row has rationale and trigger
- [X] T065 Ensure every accepted consumer row is complete and read-only
- [X] T066 Decide whether any real uncovered consumer requires `C049+`
- [X] T067 Record the new-contract decision and duplicate-boundary review
- [X] T068 Create `magiblot-contract-matrix.md`
- [X] T069 Create `magiblot-consumer-review.md`

## Phase 5: Combined TG/MB Deduplication

- [X] T070 Load and verify the complete Feature-029 handoff
- [X] T071 Verify all 48 `TGO*` observations reconcile with Feature-029 audit data
- [X] T072 Assign one combined disposition to every `TGO*` observation
- [X] T073 Assign one combined disposition to every `MB*` observation
- [X] T074 Deduplicate observations only by reproducible TuiVision gap boundaries
- [X] T075 Create `CF001+` findings only for reproduced gaps
- [X] T076 Give every CF finding exactly one Primary Owner
- [X] T077 Define common reproduction, red proof, and real-path green proof per finding
- [X] T078 Merge API, A11Y, platform, security, and risk impacts per finding
- [X] T079 Validate finding dependencies are acyclic
- [X] T080 Topologically sort non-empty owner groups
- [X] T081 Record non-finding rationale for every observation without a CF finding
- [X] T082 Create `combined-findings.md`
- [X] T083 Verify no `ProductDecision` remains open

## Phase 6: Follow-up Intakes and Wave Gate

- [X] T084 Compute the next feature number from the final finding set
- [X] T085 Generate one Hardening Lastenheft per non-empty Primary-Owner group
- [X] T086 Verify no empty owner group generated a file
- [X] T087 Generate exactly one independent Closure Lastenheft last
- [X] T088 Validate generated intake dependencies and numbering
- [X] T089 Create `pre-wave-gate.md`
- [X] T090 Keep Wave 5 `BlockedPendingCombinedConformanceClosure`
- [X] T091 Keep Wave 6 `BlockedPendingCombinedConformanceClosure`
- [X] T092 Update `Pflichtenheft.md` with the computed next intake
- [X] T093 Update `Lastenheft_Abarbeitungsreihenfolge.md` with the computed sequence
- [X] T094 Update all maintained agent surfaces as one synchronized group
- [X] T095 Verify `.specify/templates/` remains N/A unless a portable rule changed

## Phase 7: Governance and Readable Evidence

- [X] T096 Complete all governance rows in `magiblot-evolution-audit.json`
- [X] T097 Complete security applicability and N/A trigger evidence
- [X] T098 Complete architecture, iSAQB, BSI C3A, and BSI C5 evidence
- [X] T099 Complete A11Y, bilingual, text-first, and didactic-comment evidence
- [X] T100 Complete cross-platform and script-parity evidence
- [X] T101 Complete agent-parity and template-applicability evidence
- [X] T102 Complete autonomous-state, authority, resume, and delivery evidence
- [X] T103 Complete all validation rows without empty starter values
- [X] T104 Reconcile readable matrices with accepted JSON cardinalities
- [X] T105 Update `pr-evidence.md` with decision and governance counts

## Phase 8: Implementation Convergence

- [X] T106 Increment the manual build counter for the complete targeted validator invocation
- [X] T107 Run Feature-024/028/029/030 targeted Release validators
- [X] T108 Fix only audit-integrity defects revealed by targeted tests
- [X] T109 Re-run the targeted validator with a new build counter if required
- [X] T110 Create and execute the implementation-readiness checklist
- [X] T111 Run repeated Analyze and remediate all Critical/High findings
- [X] T112 Dispose every Medium finding with fix or accepted owner/rationale
- [X] T113 Verify all task requirements and success criteria are mapped
- [X] T114 Mark implementation tasks complete only after their evidence exists

## Phase 9: Final Local Validation

- [X] T115 Run `git diff --check`, JSON parse, Markdown fence, UTF-8, and placeholder scans
- [X] T116 Run protected-source, generated-output, dependency, and package diff scans
- [X] T117 Run `dotnet format --verify-no-changes`
- [X] T118 Increment the manual build counter for full Release tests
- [X] T119 Run the full Release test suite
- [X] T120 Validate `coverlet.runsettings`
- [X] T121 Increment the manual build counter for canonical coverage
- [X] T122 Run the canonical five-assembly coverage gate
- [X] T123 Run `docfx docfx.json`
- [X] T124 Run Playwright/Axe DocFX smoke
- [X] T125 Run UTF-8 Lynx review for learner-facing Feature-030 pages
- [X] T126 Run Bash and available PowerShell secret scans with High 0
- [X] T127 Run agent homogeneity and maintained-surface parity checks
- [X] T128 Re-run all feature checklists and confirm zero incomplete items
- [X] T129 Update `docs/project-statistics.md`
- [X] T130 Archive the binding Lastenheft through the repository rename workflow
- [X] T131 Reconcile final scope and prove zero forbidden executable or external-source changes
- [X] T132 Complete local validation evidence and final task counts

## Phase 10: Candidate, Publish, Review, and MergeAndSync

- [X] T133 Align `Directory.Build.props` to the current Feature-030 branch version
- [X] T134 Stage only the intended candidate and inventory every path
- [X] T135 Run `git diff --cached --check` and compare staged/unstaged/untracked status
- [X] T136 Commit the reviewed local candidate, capture its hash in temporary operation evidence, and reserve terminal proof for `delivery-closeout.md`
- [X] T137 Push the exact Feature-030 branch, verify the remote head, and reserve terminal proof for `delivery-closeout.md`
- [X] T138 Create the Feature-030 PR, capture its stable identity in temporary operation evidence, and reserve terminal proof for `delivery-closeout.md`
- [ ] T139 Reconcile stable PR identity into `pr-evidence.md`, align version, stage/check, commit, and push the final review candidate
- [ ] T140 Poll required PR checks, map actual workflow/job/platform/commands, and reserve terminal proof for `delivery-closeout.md`
- [ ] T141 Inspect Claude, Copilot, and GraphQL review state and reserve terminal proof for `delivery-closeout.md`
- [ ] T142 Address every actionable review finding, resolve its thread, and record remediation in `pr-evidence.md`
- [ ] T143 Revalidate the final reviewed head after remediation and reserve terminal proof for `delivery-closeout.md`
- [ ] T144 Create temporary exact-head gate evidence, run the installed validator, and reserve its terminal result for `delivery-closeout.md`
- [ ] T145 Confirm zero actionable review threads, document unavailable reviews, and reserve terminal proof for `delivery-closeout.md`
- [ ] T146 Apply the narrow human-approval-only bypass only if it is the sole blocker and record it in `delivery-closeout.md`
- [ ] T147 Merge the feature PR with the authorized repository policy and record the merge in `delivery-closeout.md`
- [ ] T148 Delete obsolete feature branch refs, fetch/prune, and record cleanup in `delivery-closeout.md`
- [ ] T149 Switch locally to `main`, pull fast-forward, and record synchronization in `delivery-closeout.md`
- [ ] T150 Prove clean tree and `HEAD == origin/main` after the feature merge in `delivery-closeout.md`

## Phase 11: Retrospective and Closeout

- [ ] T151 Reveal and verify the original random-selection commitment as superseded by the actual user-timed abort
- [ ] T152 Record interruption timing, stale state, operation reconstruction, and invocation counts
- [ ] T153 Confirm no second intentional interruption is scheduled
- [ ] T154 Classify the run learning as `NoPromotion` or a concrete `PresetFollowUp`
- [ ] T155 Create no Home-Baseline branch or PR for `NoPromotion`
- [ ] T156 If promotion is required, complete the documented patch-release and exact ZIP adoption cycle before closeout
- [ ] T157 Create `delivery-closeout.md` with feature-merge and first main-sync facts but no recursive closeout identity fields
- [ ] T158 Complete feature retrospective, all task dispositions, and final run state with `nextExactAction: N/A`
- [ ] T159 Create a single-commit evidence-only closeout branch and candidate
- [ ] T160 Run closeout-proportional state, diff, secret, documentation, and A11Y gates
- [ ] T161 Commit, push, and create the causal closeout PR whose acceptance is defined by `delivery-closeout.md`
- [ ] T162 Converge closeout checks and review threads against `delivery-closeout.md` without writing the closeout's own remote facts into it
- [ ] T163 Merge the closeout PR under the authorized policy and verify the terminal fact externally against `delivery-closeout.md`
- [ ] T164 Delete closeout branch refs, switch to `main`, pull, and prune while preserving `delivery-closeout.md` as the repository record
- [ ] T165 Verify TuiVision ends clean with `HEAD == origin/main`; compare externally with the completed state and `delivery-closeout.md`

## Dependencies

- T001-T013 precede all external and implementation work.
- T014-T027 precede relation decisions.
- T028-T048 form the test-first vertical slice.
- T049-T069 precede combined deduplication.
- T070-T083 precede generated follow-up intakes.
- T084-T105 precede implementation convergence.
- T106-T114 precede final local validation.
- T115-T132 precede candidate staging and remote delivery.
- The already completed UI interruption between task generation and Analyze
  remediation is the single hard-abort field proof; no second trigger is
  permitted.
- T133-T150 complete authorized feature-PR delivery.
- T151-T165 complete retrospective and the required non-recursive causal closeout.

No task may modify product runtime, public APIs, dependencies, examples,
consumer sources, historical sources, or the external magiblot checkout.
