# Implementation Plan: TV203, Free Vision, and Terminal.GUI Conformance Audit

**Branch**: `029-tv203-freevision-terminalgui-conformance-audit` | **Date**: 2026-07-16 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` and final Feature-024/025/026/028 evidence

## Summary

Review all 48 accepted framework contracts and the complete Wave-5/Wave-6
consumer baseline against pinned Terminal.GUI v1.9.0 without changing product
behavior. Preserve Feature 024 as the canonical TV203/Free Vision audit and add
one separate Feature-029 JSON dataset containing the Terminal.GUI source
manifest, contract relations, consumer review, observations, governance
decisions, and Feature-030 handoff. Add one test-only MSTest validator in the
existing Drivers test project, produce bilingual readable evidence, and leave
Wave 5 and Wave 6 blocked with Feature 030 as the only next intake.

## Technical Context

**Language/Version**: C# / .NET 10 for test-only validation; JSON and Markdown for audit evidence
**Primary Dependencies**: existing MSTest 4.0.1, System.Text.Json, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, Git and GitHub Actions
**Storage**: repository-owned Feature-029 JSON and Markdown; external Terminal.GUI checkout remains untracked under a temporary directory
**Testing**: new `TerminalGuiConformanceEvidenceTests`, existing Feature-024 and Feature-028 evidence validators, targeted Drivers tests, full Release, canonical coverage when the test validator changes
**Target Platform**: local macOS plus existing exact-head Ubuntu, macOS, and Windows CI; Terminal.GUI is source evidence only
**Project Type**: evidence and test-infrastructure audit for a cross-platform C# TUI framework
**Performance Goals**: no runtime performance change; validation remains bounded to 48 contracts, 16 domains, the accepted consumer baseline, and the selected pinned source inventory
**Constraints**: no runtime/API/dependency/example/external-source change; no Terminal.GUI source copy; no magiblot analysis; one build-counter increment per explicit build/test invocation
**Scale/Scope**: 48 existing contracts, 16 domains, 13 existing findings/resolutions, 13 consumer groups, selected Terminal.GUI source/test records, 7 installed governance presets

## Constitution Check

| Gate | Result and planned evidence |
|---|---|
| Level-2 project context | Pass: TuiVision C#/.NET registry, Constitution v1.14.0, agent guidance, Lastenheft 13, and all seven local presets apply |
| Memory-safe language | Pass: C#/.NET is allow-listed; C++, Pascal, Free Vision Pascal, and external Terminal.GUI C# are read-only evidence |
| Secure generation | Pass: test-only JSON parsing is closed-shape, fail-closed, bounded, and never activates runtime types |
| Secure architecture | Pass: source provenance, relation integrity, handoff integrity, and delivery evidence are explicit trust boundaries; product architecture is unchanged |
| Historical-source policy | Pass: relevant `tv203s/`, `TVDEMOS/`, `TVFM/`, and accepted Free Vision evidence remain read-only |
| NIST SSDF / CWE Top 25 | Applicable to provenance, malformed audit data, validator integrity, scope protection, and fail-closed decisions |
| ASVS | `N/A`: no web, HTTP, authentication, session, or service boundary; re-evaluate if such scope enters the diff |
| SBOM / VEX / SLSA / OpenSSF | Existing repository checks remain validation evidence; new artefacts are `N/A` because no dependency, package, or runtime distribution changes |
| AI-SBOM | `N/A`: AI is development tooling only; re-evaluate for released models, datasets, AI services, inference, or runtime AI |
| STRIDE / CIA / CAPEC | Applicable to source identity, manifest hashes, audit relations, findings, handoff, and exact-head evidence |
| S-ADR / arc42 | `N/A` unless a material product-architecture decision is discovered; the planned evidence layer does not change product architecture |
| Zero Trust / SAMM / BSI C3A / BSI C5 | `N/A`: no cloud, provider, deployment, organization-wide maturity, or distributed-service boundary changes |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: no regulated product, operation, AI runtime, financial ICT, or new distribution boundary |
| A11Y | Applicable to bilingual learner-facing audit guidance, text-first tables, consumer A11Y relations, and didactic comments in non-trivial validator logic |
| Cross-platform | Applicable to portable JSON paths and existing Ubuntu/macOS/Windows validation; no paired script change is planned |
| Agent parity | Applicable: five maintained guidance surfaces move together only for shared Feature-029 completion and next-intake state |
| Autonomous governance | Applicable: v0.2.1 run state, gate requirements, exact candidate checks, exact-head proof, permission boundaries, stop/resume, closeout, and retrospective |
| Statistics | Applicable: update `docs/project-statistics.md` after the final candidate and validation counts are stable |
| Security-first tracking | Pass: no credentials, agent state, logs, caches, external checkout, generated docs, or temporary provider evidence will be committed |

No Constitution exception is required. Existing repository security and
architecture documents remain authoritative; feature-local applicability and
results are recorded in `pr-evidence.md`.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: The user approved the complete proposed plan, including
Feature-029 resume, commits, push, PR, review remediation, merge, cleanup,
local main synchronization, Home-Baseline documentation publication, public
preset release, and TuiVision adoption. The narrow admin bypass is permitted
only when all technical gates pass, actionable threads are zero, and human
approval is the sole remaining rule.

**Evidence path**: `specs/029-tv203-freevision-terminalgui-conformance-audit/pr-evidence.md`

**Representative vertical slice**: Domain `D02`, contracts `C004`-`C006`.
Create the test-only validator before the dataset and record the missing-file
red proof. Then add pinned Application, MainLoop, Responder, ConsoleDriver, and
relevant UnitTests source records plus one relation row per contract. Prove
exact relation cardinality, reciprocal source links, existing TuiVision proof,
consumer relevance, and no finding caused by architecture alone before
expanding to the remaining domains.

**Convergence gates**:

- Clarify: no remaining question changes scope, schema, source pin, task shape,
  validation, acceptance, or ordering.
- Checklists: every requirements and plan item passes or has an explicit
  accepted disposition.
- Analyze: zero Critical/High; every Medium is remediated or accepted with
  owner and boundary.
- Implement: all 48 contracts, 16 domains, consumer rows, observations,
  governance rows, and Feature-030 handoff reconcile; all triggered validation
  passes and the forbidden-scope diff is empty.
- Remote review: required PR-context checks pass on the reviewed head, exact
  gate evidence validates, and actionable review threads are zero.

**Shared single-writer files**: `pr-evidence.md`,
`terminalgui-conformance-audit.json`, `feature030-handoff.json`,
`autonomous-gate-requirements.json`, `autonomous-run-state.json`,
`Directory.Build.props`, Pflichtenheft, processing order, five agent files,
Lastenheft archive, and `docs/project-statistics.md`.

**Validation triggers**:

- Always: JSON shape, relation cardinality, path/hash inventory, diff,
  formatting, markers, secrets, dependencies, protected sources, and generated
  output.
- Test-only validator: targeted Drivers Release tests, full Release, and
  canonical coverage because shared test infrastructure is extended.
- Learner-facing Markdown and status: DocFX, Playwright/Axe, and UTF-8 Lynx.
- External source: exact tag object, peeled commit, license hash, path hashes,
  and untracked-checkout boundary.
- Agent/status changes: five-surface parity and next-intake consistency.

**Scope firewall**: A reproducible product or proof defect becomes a complete
`TG*` observation and is handed to Feature 030; `ProductDecision` blocks the
run. No runtime correction, API change, example port, magiblot review, or
follow-up Lastenheft is implemented in Feature 029.

**Remote closeout**: Commit and push the final candidate, create a ready PR,
use pull-request-context checks as primary delivery evidence, treat duplicate
push runs as noise, validate temporary exact-head evidence with the installed
Bash and PowerShell validators, converge reviews and GraphQL threads, merge
under the authorized policy, delete the feature branch, and synchronize local
`main`. True post-merge facts use one non-recursive
`delivery-closeout.md` only when required.

## Project Structure

### Feature artifacts

```text
specs/029-tv203-freevision-terminalgui-conformance-audit/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── tasks.md
├── pr-evidence.md
├── terminalgui-conformance-audit.json
├── terminalgui-source-manifest.md
├── terminalgui-contract-matrix.md
├── terminalgui-consumer-review.md
├── terminalgui-findings.md
├── feature030-handoff.json
├── feature030-handoff.md
├── pre-wave-gate.md
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── delivery-closeout.md              # only if post-merge truth requires it
├── contracts/
│   └── terminalgui-conformance-acceptance.md
└── checklists/
    ├── requirements.md
    ├── source-provenance.md
    ├── contract-consumer.md
    ├── finding-handoff.md
    ├── governance-readiness.md
    ├── plan-quality.md
    ├── plan-review.md
    └── implementation-readiness.md
```text

### Executable and maintained surfaces

```text
tests/TuiVision.Drivers.Tests/
└── TerminalGuiConformanceEvidenceTests.cs   # new test-only validator

docs/guides/
└── terminalgui-conformance-audit.md

Pflichtenheft.md
docs/project-statistics.md
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```text

**Structure Decision**: Keep Feature 024 immutable as the canonical TV203/Free
Vision audit. Feature 029 owns a separate relation and handoff dataset linked
to the existing contract, finding, resolution, source, proof, and consumer IDs.
Only test infrastructure and evidence/status/documentation surfaces change.

## Phase 0: Research and Source Freeze

1. Freeze Feature-024 Revision 2, final 025/026 resolutions, and Feature-028
   closure as immutable inputs.
2. Verify Terminal.GUI URL, `v1.9.0`, tag object
   `4b812e44798f2c7567afec50ba9a9293b6beb6de`, peeled commit
   `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`, MIT license, and retrieval date.
3. Review selected v1.9.0 production and UnitTests files for the 16 domains;
   store only path, SHA-256, short behavior summary, and optional commit
   permalink.
4. Re-read current TuiVision proof, historical intent, Free Vision relation,
   and consumer relevance for all 48 contracts.
5. Confirm no existing contract needs `C049+` unless a real consumer
   responsibility is both material and uncovered.

## Phase 1: Evidence Model and Acceptance Contract

1. Model `TerminalGuiAuditRun`, `TerminalGuiSourceRecord`,
   `TerminalGuiContractRelation`, `TerminalGuiConsumerReview`,
   `TerminalGuiObservation`, `Feature030Handoff`, `GovernanceDecision`, and
   `ValidationEvidence`.
2. Require exactly `C001`-`C048`, `D01`-`D16`, one relation per contract,
   reciprocal source links, complete existing proof references, and closed
   vocabularies.
3. Require every observation to be either a complete finding or a complete
   non-finding; prohibit architecture-only findings.
4. Keep delivery facts and temporary exact-head provider evidence outside the
   audit dataset.

## Phase 2: Evidence Foundation and Test-First Slice

1. Create `pr-evidence.md` from the autonomous template and create committed
   gate requirements before the first test or evidence implementation edit.
2. Add `TerminalGuiConformanceEvidenceTests.cs` first and observe the
   missing-dataset red result after one build-counter increment.
3. Add the D02/C004-C006 source and relation slice, then prove exact
   cardinality, source hashes, reciprocal IDs, TuiVision proof paths, consumer
   links, and allowed relation values.
4. Expand malformed-data tests for invalid JSON, duplicate IDs, unknown
   relation/decision, missing source/proof path, wrong pin/hash, orphan links,
   cycles, and handoff disagreement.
5. Add concise German-first/English-second comments only where validator
   relation or proof-boundary logic is non-trivial.

## Phase 3: Full Contract and Consumer Review

1. Expand the relation dataset across all 16 domains and 48 contracts.
2. Use `NotApplicable` only with rationale and re-evaluation trigger.
3. Review all six Wave-5 and seven Wave-6 baseline groups from the accepted
   consumer review; add no consumer ID unless a genuinely new shared
   responsibility exists.
4. Record `C049+` only after all five admission checks; otherwise record the
   explicit zero-new-contract conclusion.
5. Classify each new observation through the five allowed decisions and
   assign one Primary Owner plus an acyclic dependency set.

## Phase 4: Handoff, Gate, and Readable Evidence

1. Generate the machine-readable and readable Feature-030 handoff with all
   findings, non-findings, owner proposals, dependencies, proof needs, and
   deduplication keys.
2. Create no hardening or closure Lastenheft.
3. Keep Wave 5 and Wave 6 blocked and name Feature 030/Lastenheft 14 as the
   sole next intake.
4. Add bilingual source manifest, contract matrix, consumer review, findings,
   gate, and learner guide.
5. Synchronize Pflichtenheft, processing order, five agent surfaces,
   project statistics, and Lastenheft archive only after audit acceptance.

## Phase 5: Full Validation and Delivery

1. Run static and candidate checks before test batches.
2. Run targeted Drivers tests for the new validator and existing Feature-024/
   Feature-028 validators, then full Release and canonical coverage.
3. Run DocFX, Axe, UTF-8 Lynx, secret, dependency, protected-source,
   generated-output, and agent-parity checks.
4. Align `Directory.Build.props` before every build/test and before
   commit/push under `1.29.<patch>.<build>`.
5. Stage only intended files, validate the staged candidate, commit, push,
   open the PR, converge checks/reviews, validate exact-head gate evidence,
   merge, delete the branch, and synchronize `main`.
6. Run the autonomous retrospective. Record documentation learning for the
   planned `autonomous-run-governance` v0.2.2 package, but do not start Feature
   030 until that package is published and adopted.

## Validation Strategy

| Trigger | Command or proof | Acceptance |
|---|---|---|
| Every candidate | `git diff --check`, staged inventory, marker and protected-path scans | zero defects and no missing intended file |
| JSON and Markdown | strict JSON parse, closed-schema MSTest, heading/fence/link review | exact cardinalities, no placeholders, readable DE/EN |
| Test-only validator | `dotnet format --verify-no-changes --no-restore` and targeted Drivers Release tests | formatter clean; all evidence validators pass |
| Repository integration | `dotnet test TuiVision.sln --configuration Release --no-restore` | full suite passes with explicit totals |
| Coverage | `xmllint --noout coverlet.runsettings` plus canonical Coverlet invocation | each mandatory assembly remains at least 70 percent |
| Documentation | `docfx docfx.json`, `npm run test:docfx`, UTF-8 Lynx | 0 DocFX warnings/errors, Axe pass, semantic text order |
| External provenance | `git ls-remote`, tag/commit inspection, SHA-256 inventory | exact pins and hashes; checkout absent from Git |
| Security/scope | secret scan, dependency review, generated/protected path diff | zero High secrets and zero forbidden changes |
| Agent/status | five-surface parity and marker scan | one consistent Feature-030 next-intake state |
| Remote review | required PR-context checks, Claude/Copilot state, GraphQL threads | technical green, unavailable review honest, actionable threads 0 |
| Exact-head gates | temporary provider evidence plus both validators | requirements hash, head, tokens, and one Primary row all pass |

## Version and Build-Counter Strategy

- Branch version is `1.29.<patch>.<build>` with `Version`,
  `AssemblyVersion`, and `FileVersion` aligned.
- Before each explicit `dotnet build` or `dotnet test`, increment only the
  manual build counter and record the value in `pr-evidence.md`.
- One increment authorizes one command; related filters are batched.
- Before commit or push, align patch to the resulting branch commit count
  without incrementing build unless another build/test runs.

## Complexity Tracking

No Constitution violation or product-complexity exception is required. The
validator is test-only, the datasets are feature-local, and the product
assemblies remain unchanged.
