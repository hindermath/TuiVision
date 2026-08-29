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
| Language and accessibility | German-first/English-second CEFR-B2 and text-first | In validation | All new learner documents are bilingual and semantic; renderer/Axe/Lynx pending |
| Multi-agent workflow | Operational `agy`; Gemini classified as legacy compatibility | Pass | `docs/guides/multi-mac-workflow.md`, maintained agent surfaces and registries already comply; no shared guidance update needed |
| Historical deviations | Discoverable guide or changelog path | Pass | `docs/guides/historical-deviations.md` links policy, porting ledger and existing feature evidence |
| Publishing | DocFX, Pages, CS1591, Playwright/Axe | Open | Local and exact-head proof pending |

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
| Homogeneity | Repository structure and statistics | Pending one expected refresh | Only `docs/project-statistics.md` drift remains; renderer requires a clean committed tree |
| GitHub exact-head checks and review | Remote delivery | Open | PreMerge evidence required |

## Decision

`InProgress`. Specify through Analyze converged without a critical or high
finding. Documentation implementation is complete at the 7/38/27 cardinality
boundary; local renderer, A11Y, governance and delivery validation remain.
