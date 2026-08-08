# Autonomous Run Evidence: Wave-6 Combined Delta Closure

**Branch**: `037-wave6-combined-delta-closure`
**Feature directory**: `specs/037-wave6-combined-delta-closure`
**Binding intake**: `requirements/intakes/active/Lastenheft_22_Wave6-Combined-Delta-Closure.md`
**Delivery mode**: `MergeAndSync` after explicit resumed authority
**Authority source**: Current user instruction, including a narrow Human-Approval-only admin-bypass boundary
**Owner**: Thorsten Hindermann
**Reviewer**: Codex
**Review date**: 2026-08-08

## Scope and Preflight

- Baseline: clean synchronized `main` at
  `faa42883d4a1cbe1b7b07e4e0b7f2b07e34e4c76`.
- Identity: branch `037-wave6-combined-delta-closure` and matching
  `.specify/feature.json`.
- Intake: Lastenheft 22, current authoring receipt and `Ready` series review;
  requirements-intake alignment passed for seven active targets.
- Tooling: `specify check` passed with Spec Kit 0.12.11.
- Authority: the initial local scope was superseded after the T128 hard gate by
  explicit `MergeAndSync` authority. Commit, push, PR, provider/review
  convergence, merge and one causal closeout are permitted. Feature 038 remains
  prohibited. Admin bypass is permitted only after every technical gate is
  green, no actionable thread remains and Human Approval alone is open.
- Protected roots: `src/`, `examples/`, project/package files, `TVFM/`,
  `TVDEMOS/`, `tv203s/` and generated output. Requirement state remains
  unchanged on the feature head and may move only in the causal closeout.
- Allowed writes: Feature-037 artifacts, `.specify/feature.json`, one test-only
  closure validator, `Directory.Build.props` version alignment, and the narrow
  alignment-validator correction with its fixtures.

## Preset Matrix

| Preset | Version | Priority |
|---|---:|---:|
| security-governance | 0.6.1 | 10 |
| architecture-governance | 0.5.1 | 20 |
| isaqb-architecture-governance | 0.2.1 | 30 |
| a11y-governance | 0.4.2 | 40 |
| cross-platform-governance | 0.2.1 | 50 |
| agent-parity-governance | 0.4.1 | 60 |
| intake-authoring-governance | 0.3.0 | 64 |
| intake-review-governance | 0.2.0 | 65 |
| intake-sequencing-governance | 0.2.2 | 66 |
| autonomous-run-governance | 0.3.3 | 70 |
| parallel-autonomous-governance | 0.2.4 | 80 |

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Pass | clean main, intake/review/receipt, branch, `specify check`, presets and authority | None |
| Specify | 1 | Pass | clarified `spec.md` and requirements checklist | None |
| Clarify | 2 | Pass | exact PR roles, 24/10/10/10/1 sets and local causal boundary fixed | None |
| Checklists | 1 | Pass | five domain checklists, 50/50 items complete | None |
| Plan | 2 | Pass | plan and design artifacts; independent plan review complete | None |
| Tasks | 2 | Pass | 169 dependency-ordered sequential tasks after delivery-authority delta | None |
| Analyze | 1 | Remediated | duplicate showcase-area ownership in two combined rows | Replaced with a ten-row bijection |
| Analyze | 2 | Pass | 33 requirements and 147 tasks covered; no blocking issue | Implement evidence and validator |
| Analyze | 3 | Remediated | authority delta exposed stale 24/10/1, eight-dimension and non-bijective data-model wording | Normalize to 24/10/10/10/1, nine dimensions and one showcase per row |
| Analyze | 4 | Pass | 33 functional/governance requirements, 12 success criteria and 169 tasks are consistent; no Critical, High or Medium issue | Publish exact candidate |
| Implement | 1 | Pass | complete 24/10/10/10/1 data and deterministic validator | None |
| Resume | 1 | Pass | stale Feature-036 guard corrected; two positive and ten negative fixtures pass through both wrappers | Final candidate validation |
| Validate | 1 | Active | all initial local gates pass; final closure rerun remains | Run final focused validator |
| Retrospective | 0 | Pending | no result preclaimed | Complete after validation |

## Product-Delta Provenance

| PR | Role | Base | Head | Merge | Files | File-set SHA-256 |
|---:|---|---|---|---|---:|---|
| #101 | FunctionalProduct | `4b32762dfc60e18655de35d816ff1d4ede0185eb` | `207e807ee8835779b9b8641f91868a6a5e80f938` | `52f77facc518e3084f897148b44ec19e62b3dde6` | 44 | `d9cf20ce76e07e7e4d8c3064589fea670fc049fe083e293091ea86df9a82e902` |
| #102 | FunctionalCloseout | `52f77facc518e3084f897148b44ec19e62b3dde6` | `e6d5b07ef91ac8770ab03a1c4b9830a17bf334ad` | `b0d99052b66f3f575f8343fa291761ec3f65779d` | 14 | `cc60e928453e596aa86afd30d1918d51b44a532fa7dfb4f8ebfa9d5a97eb9841` |
| #103 | PromptMetadata | `b0d99052b66f3f575f8343fa291761ec3f65779d` | `3d1ee66b6eb7c54e8663430a57945d47a8d63845` | `42a842fb63a0695a618a0f87ffec543e9bc3b6c8` | 9 | `d38454d7882446d97839e60a38f4977b02b474a9d4929f0b854cf5e718f80848` |
| #104 | ShowcaseProduct | `42a842fb63a0695a618a0f87ffec543e9bc3b6c8` | `a0d506297c101104fd0e15911a7d21e1c5a21caa` | `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d` | 35 | `2130ffa53ec34bbb23f9d64b040d3922a89238c2bcd7d36af44f9329bdc8a6aa` |
| #105 | ShowcaseCloseout | `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d` | `50a8f4dfab64de6a042555f8e304f01ac6b8596f` | `371af97ff1741313ab808c87c2827655073cff2c` | 14 | `5fea3e2b0e8f988a7f6307ed98045ffa0d071ec29ab70567faebace961e72503` |

The authoritative product union is the declared executable/test project-path
subset of #101 plus #104. Documentation is reviewed through the accepted
inputs and guide record. Closeout and prompt metadata do not become product
behavior.

## Accepted Predecessor Inputs

| Path | SHA-256 |
|---|---|
| `specs/035-wave6-tvfm-functional-porting/pr-evidence.md` | `0a44474af9e700a9e4000c1f7141e32eb3097e43450d9953964e1a1d6939e29c` |
| `specs/035-wave6-tvfm-functional-porting/delivery-closeout.md` | `55e1663260d6248b67ce3cb057d252b3adecc5d2c6fe5c8d88bc447969b120e9` |
| `specs/035-wave6-tvfm-functional-porting/retrospective.md` | `22fdeda63fce7b5e11c2fe220804748b4536bdf7ecc258455df1700fd82819e6` |
| `specs/035-wave6-tvfm-functional-porting/plan.md` | `fb3da3a015451ec2e22d496d58550ce29919f7808f3763eb1802b447decbca3b` |
| `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md` | `e2ba63f09efe707be79294538bcbaa98b733264f9bf40dc78021f40013069d9d` |
| `specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md` | `3daed913d02a496da4bd9f9063de8a84149c39ccffbe5956204bbde377988432` |
| `specs/036-wave6-tvfm-showcase-remediation/retrospective.md` | `bb3dbdf1cd4759d98b7b508b6efb29a6823879b222f849f8b76f576f3d201e21` |
| `specs/036-wave6-tvfm-showcase-remediation/plan.md` | `d3bf0f482d8628010e4635529d56c60e80040b58b821fc3ff9d2cde78ce4db11` |

## Historical Context Boundary

All 24 `TVFM/` paths match the hashes accepted by both predecessor features.
Relevant application, desktop, event, group, view, list, dialog, status and
window implementations under `tv203s/contrib/tvision/classes/` were reviewed
as read-only intent context. They do not add normative rows and no historical
bytes are modified.

## Governance Applicability

| Preset family | Checkpoint | Applicability | Rationale | Result | Re-evaluation trigger |
|---|---|---|---|---|---|
| Security | NIST SSDF, CWE Top 25, evidence integrity and secrets | Applicable | Hash-bound evidence and fail-closed validation are the feature | Pass | Any evidence or validator change |
| Security | ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act, DORA | N/A | No web, release, package, runtime AI or regulated-service change | Accepted | Any matching scope trigger |
| Architecture | STRIDE/CIA/CAPEC and evidence trust boundary | Applicable | Provenance and controlled filesystem proof must remain truthful | Pass | Any trust or product boundary change |
| Architecture | S-ADR, arc42, Zero Trust, SAMM, BSI C3A/C5 | N/A | No architecture, identity, network, cloud or maturity-program change | Accepted | Any matching scope trigger |
| iSAQB | Quality, decision and proof views | Applicable | Combined ownership and quality evidence are central | Pass | Any mapping or ownership change |
| A11Y | Text-first, keyboard, focus, Description and bilingual evidence | Applicable | The audit validates learner-facing predecessor proof | Pass | Any proof or documentation change |
| Cross-platform | Text hashing and platform evidence honesty | Applicable | CRLF and filesystem behavior are platform-sensitive | Pass | Any platform or hashing change |
| Cross-platform | Shared Node alignment logic through Bash/PowerShell wrappers | Applicable | The T128 remediation changes a cross-platform repository script | Pass | Alignment script, fixture or wrapper change |
| Agent parity | Maintained and generated guidance surfaces | Applicable | Existing parity must remain intact | Pass | Any guidance or template change |
| Intake governance | Binding intake, receipt, series and eligibility | Applicable | Feature 037 originates from the governed series | Pass | Intake or series drift |
| Autonomous | State, authority, gates, tasks and evidence | Applicable | The resumed run has explicit MergeAndSync authority | Pass | Authority, state, artifact or provider drift |
| Parallel autonomous | Campaign orchestration | N/A | This is one serial feature run | Accepted | Any campaign or worker scope |

## Documentation Impact

| Surface | Decision | Rationale |
|---|---|---|
| Feature-037 Markdown/JSON | Update | Required audit artifacts and evidence |
| Public API/XML comments | NoChange | Read-only audit has no API surface |
| Guides/DocFX navigation | NoChange | Existing Wave-6 guide is validated, not edited |
| Agent guidance/templates | NoChange | No new shared rule is introduced |

## Validation Ledger

| Command or review | Version | Result | Evidence or failure boundary |
|---|---|---|---|
| `specify check` | N/A | Pass | Spec Kit 0.12.11; repository configuration valid |
| requirements-intake alignment | N/A | Pass | seven active targets and one binding edge valid |
| prerequisite discovery before Plan | N/A | Expected precondition | Reported missing `plan.md`; plan was the documented next stage |
| prerequisite discovery after Plan/Tasks | N/A | Pass | Feature 037 and design artifacts resolved |
| Analyze pass 1 | N/A | Remediated | Two rows shared showcase ownership; replaced with a bijection |
| Analyze pass 2 | N/A | Pass | 33 requirements, 147 tasks, zero blocking issue |
| Bash/PowerShell state validation at Analyze | N/A | Pass | Both report `Analyze`, `Active`, 55/147 |
| focused closure compile attempt | `1.37.0.400` | Failed invocation | Public test methods lacked repository-required XML documentation; no Red result accepted |
| focused incomplete closure dataset | `1.37.0.401` | Expected fail | Compiled successfully and failed only because the functional proof set was incomplete |
| first `W6C-001` vertical slice | `1.37.0.402` | Pass | 1/1 passed with function, showcase, app-loop, view, focus, status, Description, cell and decision fields |
| complete structural closure attempt | `1.37.0.403` | Expected test-design fail | 6/7 passed; `TOOLS.PAS` exposed invalid use of text decoding for legacy bytes, while raw predecessor hash still matched |
| complete structural closure after byte-level normalization | `1.37.0.404` | Pass | 7/7 passed; exact PR sets, eight predecessor hashes, 24 sources, 10/10/10/1 relations and negative mutations accepted |
| final focused closure after authority delta | `1.37.0.409` | Pass | 8/8 passed with `MergeAndSync` metadata, 24/10/10/10/1 cardinality, 90 dimensions and fail-closed mutations |
| targeted predecessor Wave-6 tests | `1.37.0.405` | Pass | 43/43 functional and showcase tests passed |
| controlled `Tp7FileManager --smoke` | `1.37.0.405` binaries | Pass | Controlled navigation and preview completed with exit 0 |
| bounded normal PTY path | `1.37.0.405` binaries | Pass | First frame, F1 bilingual Description and `Ctrl+Q` exit were visible; exit 0 |
| first full Release solution run | `1.37.0.406` | Corrected sequencing | 245/246 example tests exposed a test-only premature terminal-state expectation; no product failure |
| full Release solution rerun | `1.37.0.407` | Pass | 888/888 passed: Core 52, Compatibility 18, Serialization 48, Controls 373, Drivers 151 and Examples 246 |
| `xmllint --noout coverlet.runsettings` | N/A | Pass | Canonical coverage configuration is well-formed XML |
| canonical Coverlet collection | `1.37.0.408` | Pass | Core 92.96%, Controls 86.74%, Serialization 90.01%, Compatibility 80.55% and Drivers.Console 89.18% |
| `git diff --check` | N/A | Pass | No whitespace finding |
| `dotnet format --verify-no-changes` | N/A | Pass | Exit 0 |
| `docfx docfx.json` | N/A | Pass with baseline warnings | Exit 0 and zero errors; two existing warnings remain in unchanged `docs/secure-development/README.md` |
| Playwright/Axe DocFX smoke | N/A | Pass | Built-in Python server timed out before tests; documented explicit Ruby server fallback passed 2/2 |
| UTF-8, semantic Markdown and fenced-language review | N/A | Pass | Feature files are ASCII or UTF-8, semantic and text-first; no unlabeled opening fence |
| `scan-agent-secrets.sh --fail-on-high` | N/A | Pass | Zero high findings; one pre-existing medium advisory for `.claude/settings.local.json` is outside the diff |
| vulnerable package scan | N/A | Pass | All solution projects report no vulnerable direct or transitive package |
| Bash and PowerShell homogeneity dry runs | N/A | Scoped pass | No agent-guidance finding; both wrappers report only a pre-existing `docs/project-statistics.md` profile drift outside Feature 037 |
| script-parity trigger review | N/A | N/A | No `.sh` or `.ps1` file changed |
| generated-output and protected-root inventory | N/A | Pass | No generated output, runtime, API, dependency, project, example, `TVFM/`, `TVDEMOS/` or `tv203s/` path is changed |
| requirements-intake alignment rerun | N/A | **Blocked** | Structural governance passed for seven targets and one binding edge, then the validator rejected `.specify/feature.json` because it still hard-codes `specs/036-wave6-tvfm-showcase-remediation` |
| alignment correction fixtures | N/A | Pass | Accepted predecessor 036 and authorized 037 pass; ten malformed or unauthorized cases fail closed |
| Bash and PowerShell alignment wrappers after correction | N/A | Pass | Seven current receipts, series manifest/receipt and `Ready` review all pass on both wrapper paths |
| `specify check` rerun | N/A | Pass | Spec Kit remains operational after the correction |
| feature checklist completeness | N/A | Pass | Seven checklists contain zero incomplete items |
| final artifact consistency | N/A | Pass | 24/10/10/10/1 rows, 90 dimensions, 169 unique sequential task IDs and zero drafting markers |

## Decision Summary

The complete structural dataset contains 24 historical sources, ten functional
proofs, ten showcase proofs, ten combined areas and one entry point. It has
zero `CandidateFinding` and zero `ProductDecision` records. Review of
`Tp7FileManagerApp`, `Wave6ShowcaseWindow`, `ControlledFileWorkspace` and the
Wave-6 state models found bounded application composition and domain logic, not
a replacement for a reusable framework contract.

Feature 037 stopped fail-closed at T128 because the migration-era validator
still required Feature 036. The explicitly authorized correction now accepts
only the completed predecessor 036 or the selected closure 037 and rejects an
unauthorized Feature 038. Both wrappers and all intake hashes pass unchanged.

The reviewed feature head still keeps Wave 6 `BlockedPendingDelivery` and the
portfolio audit `BlockedPendingWave6Closure`. Under the renewed
`MergeAndSync` authority, current Ubuntu/Windows provider checks, review
convergence and feature merge must occur before one causal closeout may record
`Closed` and portfolio `Eligible`. Feature 038 is not created or started.
