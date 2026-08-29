# PR Evidence: Documentation and Publishing Closure

## Delivery boundary

Feature 043 is a documentation-only closure run. It may add or improve guides,
navigation, evidence, deterministic documentation validators, statistics, and
maintained workflow guidance. Runtime, public APIs, dependencies, projects,
examples, example behavior, historical sources, and generated DocFX output are
outside the delivery set.

The current user turn grants `MergeAndSync` authority and a narrow admin bypass.
The bypass is locked unless every technical and exact-head gate is green, no
actionable review thread remains, and Human Approval is the sole open rule.

## Binding lineage

- Intake: `requirements/intakes/active/Lastenheft_23_Documentation-Publishing-Closure.md`
- Intake SHA-256: `df5f1876c781632c13594e9ec60c3cc8f9408c586ac03aeb48aa87442987878c`
- Series review: `6b74e8e5-c605-48c5-b450-1a018b5dd7eb`
- Review-result file SHA-256: `4ab92b2d7499b37b68c3d90899144d89fd33c445e7dfda94872243f3f701b770`
- Starting main: `9dcf78e6d48e447ed20c7efd8fe98be66a5c9af9`
- Model routing: `Aligned`, policy `balanced-v1`, credential-free local refresh

## Closure ledger

| Area | Required outcome | Status | Evidence or boundary |
|---|---|---|---|
| General guides | Seven coherent onboarding and concept topics | Pass | Seven new files under `docs/guides/`, README and `docs/toc.yml` |
| Example guides | Six learning-contract fields per guide | Pass | `docs/guides/example-learning-paths.md`: 38 unique projects, 5 `GuideAdequate`, 33 `MatrixCompletesContract` |
| Requirements reconciliation | One closure per DocumentationAndPublishing statement | Pass | `docs/documentation-closure.md`: 22 `Closed`, 5 `AcceptedBoundary`, 0 open/duplicate IDs |
| Language and accessibility | German-first/English-second CEFR-B2 and text-first | Pass | All new learner documents are bilingual and semantic; renderer, Axe and Lynx passed |
| Multi-agent workflow | Operational `agy`; Gemini classified as legacy compatibility | Pass | `docs/guides/multi-mac-workflow.md`, maintained agent surfaces and registries already comply; no shared guidance update needed |
| Historical deviations | Discoverable guide or changelog path | Pass | `docs/guides/historical-deviations.md` links policy, porting ledger and existing feature evidence |
| Publishing | DocFX, Pages, CS1591, Playwright/Axe | Pass | Local proof and PR #157 exact-head jobs passed |

## Governance ledger

| Preset or policy | Applicability | Current result | Re-evaluation trigger |
|---|---|---|---|
| Security Governance v0.6.2 | Documentation integrity, secrets and supply-chain applicability | Pass; product supply-chain checkpoints `N/A` | Product, dependency, distribution or AI scope changes |
| Architecture Governance v0.5.2 | Architecture explanation only | Pass | Runtime or deployment boundary changes |
| iSAQB Governance v0.2.2 | Conceptual architecture quality | Pass | Runtime architecture decision changes |
| A11Y Governance v0.4.3 | Fully applicable | Pass | Any learner-facing or generated documentation change |
| Cross-Platform Governance v0.2.2 | Commands and remote gates; no new script | `N/A` for script parity; remote gates pending | Script or platform behavior changes |
| Agent Parity Governance v0.4.2 | Applicable to maintained workflow surfaces | `NoUpdateRequired`; parity 3/3 | Agent, command, skill or template changes |
| Historical source policy | No new audit; existing deviations made discoverable | Pass | New historical claim or behavior comparison |

## Validation ledger

| Command or gate | Scope | Result | Boundary |
|---|---|---|---|
| `specify check` | Spec Kit installation | Pass | Preflight on starting main |
| Intake review and series validators | Eligible target and current lineage | Pass | Read-only; hashes unchanged |
| Model-routing status | Harness-local routing | Pass | Refreshed outside repository; no credentials stored |
| Documentation closure validation | Guide, example, reconciliation cardinalities | Pass | 7 guides, 38 unique project/guide rows, 27 unique closure rows (22/5) |
| `git diff --check` | Delivery diff | Pass | No whitespace error |
| `dotnet format --verify-no-changes` | Repository formatting | Pass | 0 of 583 files changed |
| Release CS1591 | Public documentation completeness | Pass | Version `1.43.794.457`, 0 warnings, 0 errors |
| `docfx docfx.json` | Generated documentation | Pass | First run exposed 13 invalid repository links; corrected run finished 0 warnings/0 errors; output untracked |
| `npm run test:docfx` | Playwright/Axe | Pass | DocFX 0/0 and Playwright/Axe 2/2 |
| `lynx -display_charset=utf-8 -dump` | New reader paths | Pass | Getting Started, example matrix and closure remain ordered and bilingual as text |
| Source policy | Read-only external and historical evidence | Pass | Bash and PowerShell: 13 surfaces, exact pin |
| Agent parity | Maintained generated command surfaces | Pass | Python suite 3/3 |
| Documentation-impact fixtures | Trigger and evidence contract | Pass | Bash and PowerShell: 10/10 each |
| Secret scan | Current diff and agent directories | Pass | 0 high; `.claude` medium is expected local permissions metadata and untracked |
| Full local tests and coverage | Executable regression | Not triggered | No source, test, project, package, API, XML or example change; exact-head CI remains mandatory |
| Project statistics | Canonical generated profile | Pass | Final feature head uses stable source `c8a3fd5d25fa`, 621552 text lines and 92 active days |
| Homogeneity | Repository structure and statistics | Pass | 29/29, 100 percent after canonical statistics refresh |
| GitHub exact-head checks and review | Remote delivery | Pass | 28 successful checks, one expected Pages deploy skip, zero reviews, comments or threads |

The initial remote publication pushed checkpoint `c24550a` after both delivery-
set validators accepted every changed path, zero unrelated untracked paths and
an unchanged index/worktree during validation. The pre-push secret hook passed.
No bypass was used for publication.

## Remote convergence

PR [#157](https://github.com/hindermath/TuiVision/pull/157) converged on
exact head `4c701d80ec4b72c9d1cba27fb24dd95fb0090e9a`. All 28 technical and
review jobs passed; the Pages deploy job was the only expected skip. GitHub
GraphQL reported no review threads or submitted reviews, and the PR had no
conversation comments. Copilot produced no review and is therefore recorded
as unavailable rather than as approval.

The first publication checkpoint exposed stale generated statistics in the
three-platform Homogeneity gate. The canonical renderer and its documented
exclusion of `Directory.Build.props` produced one stable statistics closeout;
both local implementations then passed 29/29 and the final remote matrix was
green. No technical failure was bypassed.

The temporary schema-2.0 PreMerge snapshot passed Bash and PowerShell with
normalized SHA-256
`a2377f9647e6aa5fa600e451b869de074fa3fc465576c69e728cbdeb4fc84889`.
Only `REVIEW_REQUIRED` remained, so the explicitly authorized narrow admin
bypass created merge commit
`1f5890767063dcebbe363fb8087e4fb89a880af1` and deleted the feature branch.
The linked PostMerge snapshot passed both validators with normalized SHA-256
`f930d13fd41ba9acf12b391b5ab12ae630afc26519d8fac8c15ef23267288ba0`.
Temporary lifecycle evidence remains outside Git.

The causal closeout marks the Documentation Publishing Closure `Completed`,
archives the predecessor series manifest and receipt byte-identically, and
makes the independent sandbox security intake the sole declared `Eligible`
successor. It does not start that intake or another feature.

## Decision

`DeliveredAndSynchronized`. The documentation-only closure is complete at the
7/38/27 cardinality boundary. Local and exact-head documentation, A11Y,
governance, security and platform gates passed; PR #157 is merged and the
post-merge series transition is bounded to this causal closeout.
