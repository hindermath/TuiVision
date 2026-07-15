# Implementation Plan: Pre-Wave-5 and Wave-6 Conformance Closure

**Branch**: `028-pre-wave5-wave6-conformance-closure` | **Date**: 2026-07-14 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md` and final Feature-024/025/026 evidence

## Summary

Independently revalidate all thirteen accepted findings, prove seven
consumer-shaped integration slices, and reassess the thirteen Revision-2
`TVDEMOS/` and `TVFM/` groups without changing product behavior. Add one
feature-local machine-readable closure dataset plus a test-only validator,
reuse existing real-path tests where they cover each named boundary, and add
Windows to the existing CI runtime matrix so exact-head platform evidence is no
longer inferred from a tooling-only job. A successful run closes only the
existing TV203/Free Vision gate as `ReadyForTerminalGuiAudit`; both Waves remain
`BlockedPendingTerminalGuiAudit`, and Feature 029 is the sole next intake.

## Technical Context

**Language/Version**: C# / .NET 10 for test-only validation; JSON, Markdown, YAML, Bash and PowerShell evidence tooling
**Primary Dependencies**: existing MSTest 4.0.1, Coverlet 6.0.4, DocFX, Playwright/Axe, Lynx, jq, xmllint, Git and GitHub Actions
**Storage**: repository-owned JSON and Markdown only; historical, consumer, and Free Vision sources remain read-only
**Testing**: new closure-integrity MSTest, existing Core/Controls/Serialization/Drivers tests, full Release, canonical coverage, docs/A11Y, scope and platform gates
**Target Platform**: local macOS; exact-head GitHub Ubuntu, macOS, and Windows runtime; relevant terminal and WSL boundary documented honestly
**Project Type**: evidence and test-infrastructure closure for a cross-platform C# TUI framework
**Performance Goals**: no runtime performance change; closure validation remains bounded to 13 findings, 7 slices, 13 required baseline consumer groups, and only genuinely discovered additional shared-flow rows
**Constraints**: no runtime/API/dependency/example/external-source change; one build-counter increment per explicit build/test command; no self-invalidating exact-head evidence in Git
**Scale/Scope**: 48 contracts in 16 domains, 13 findings and resolutions, 7 integrated proof slices, 13 consumer groups, 5 coverage-gate assemblies, 7 installed governance presets

## Constitution Check

| Gate | Result and planned evidence |
|---|---|
| Level-2 project context | Pass: TuiVision C#/.NET registry, Constitution, `AGENTS.md`, binding Lastenheft, and all seven local presets apply |
| Memory-safe language | Pass: C#/.NET is allow-listed; C++, Pascal, and external C# are read-only evidence |
| Secure generation | Pass: test-only JSON parsing is closed-schema, fail-closed, bounded, and does not activate runtime types |
| Secure architecture | Pass: trust boundaries are evidence input, console ingress, focus/lifecycle state, files/resources, and remote proof; no deployment boundary changes |
| Historical-source policy | Pass: matching `tv203s/`, `TVDEMOS/`, `TVFM/`, and pinned Free Vision files are read-only and cited, never copied mechanically |
| NIST SSDF / CWE Top 25 | Applicable to evidence integrity, malformed data, test validity, scope scans, and fail-closed gate decisions |
| ASVS | `N/A`: no web, HTTP, authentication, session, or service boundary; re-evaluate if such a surface enters scope |
| SBOM / VEX / SLSA / OpenSSF | Existing supply-chain check remains applicable as validation; new artifacts are `N/A` because no package, dependency, or distribution changes |
| AI-SBOM | `N/A`: AI is development tooling only; re-evaluate for released models, datasets, AI services, inference, or runtime AI |
| STRIDE / CIA / CAPEC | Applicable to ingress, focus, lifecycle, validation, file/resource, closure-data, and proof-integrity boundaries |
| S-ADR / arc42 | `N/A` unless implementation reveals a material architecture decision; the planned test/evidence layer does not alter product architecture |
| Zero Trust / SAMM / BSI C3A / BSI C5 | `N/A`: no cloud, provider, deployment, organization-wide maturity, or distributed-service boundary changes |
| NIS2 / CRA / EU AI Act / DORA | `N/A` for this test/evidence-only feature; re-evaluate for regulated operation, distribution, product AI, or financial-service scope |
| A11Y | Applicable to keyboard ingress, focus, keyboard-equivalent drag, rejection, modal restoration, bilingual docs, and text-first proof |
| Cross-platform | Applicable: actual Ubuntu/macOS/Windows runtime commands, path behavior, and terminal fallbacks are required; no new paired script is planned |
| Agent parity | Applicable: five maintained guidance files move together only for the shared Feature-028 completion state |
| Autonomous governance | Applicable: v0.2.0 gate requirements, validated run state, protected resume, exact staged candidate, exact-head provider evidence, permissions, closeout, and retrospective |
| Statistics | Applicable: update `docs/project-statistics.md` after the final diff and validation counts are stable |
| Security-first tracking | Pass: no credentials, agent state, logs, histories, SQLite, generated docs, test output, or temporary exact-head JSON will be committed |

No Constitution exception is required. The existing `docs/security/` evidence
remains the project-wide source; feature-specific applicability and results are
recorded in `pr-evidence.md` rather than duplicating unchanged governance files.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: The user approved the full proposed autonomous plan,
including feature PR, merge, clean main synchronization, and the previously
defined narrow admin bypass when only Human Approval remains after technical
convergence.

**Evidence path**: `specs/028-pre-wave5-wave6-conformance-closure/pr-evidence.md`

**Representative vertical slice**: `F001` + `R-028-001` +
`TVDEMOS/TVDEMO.PAS`. First create the closure validator against a missing
dataset and record the observable red failure. Then add the first finding,
keyboard-ingress slice, and consumer row with real proof references before
expanding the same closed schema to all required baseline rows and any
genuinely discovered additional shared-flow rows.

**Convergence gates**:

- Clarify: no remaining question changes scope, evidence schema, tests,
  platform proof, acceptance, or ordering.
- Checklists: every requirement and plan item passes or has an explicit
  accepted disposition.
- Analyze: zero Critical/High; every Medium is remediated or accepted with
  owner and boundary.
- Implement: every task is complete or conditionally evidenced, all 13
  findings, 7 slices, 13 baseline consumer rows, and any discovered additional
  shared-flow rows reconcile, and no forbidden product diff exists.
- Remote review: all required pull-request-context checks pass, exact-head gate
  evidence validates, and actionable threads are zero.

**Shared single-writer files**: `pr-evidence.md`, `closure-evidence.json`,
`closure-evidence.md`, `autonomous-gate-requirements.json`,
`Directory.Build.props`, `.github/workflows/ci.yml`, Feature-024 gate/status
documents, Pflichtenheft, processing order, five agent files, Lastenheft names,
and `docs/project-statistics.md`.

**Validation triggers**:

- Always: candidate inventory, diff, formatting, JSON, marker, secret,
  protected-source, dependency, and generated-output checks.
- Test-only validator and workflow change: targeted Drivers/Core/Controls/
  Serialization proof, full Release, and canonical coverage.
- Learner-facing and status Markdown: DocFX, Playwright/Axe, and UTF-8 Lynx.
- Workflow: YAML parse, action-pin review, local semantic inspection, and remote
  Ubuntu/macOS/Windows execution.
- Historical behavior: read matching C/C++ and Pascal as intent and consumer
  evidence without changing those trees.

**Scope firewall**: A reproduced product defect becomes `Reopened025`,
`Reopened026`, or `ProductDecision` and stops closure. Application-specific
work becomes `FollowUpHardening` only when it does not hide a shared gap.
Terminal.GUI analysis remains Feature 029.

**Remote closeout**: Push the final candidate, create the feature PR, use
pull-request-context checks as primary evidence, leave duplicate push events as
noise, validate temporary exact-head JSON with both installed validators,
converge Claude/Copilot and GraphQL threads, merge only under the authorized
policy, delete the branch, and synchronize local `main`. Post-merge facts are
recorded once in
`specs/028-pre-wave5-wave6-conformance-closure/delivery-closeout.md`; that
closeout does not claim its own PR URL, head, or merge commit.

## Project Structure

### Feature artifacts

```text
specs/028-pre-wave5-wave6-conformance-closure/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── tasks.md
├── pr-evidence.md
├── closure-evidence.json
├── closure-evidence.md
├── autonomous-gate-requirements.json
├── delivery-closeout.md              # created only after feature merge
├── contracts/
│   └── conformance-closure-acceptance.md
└── checklists/
    ├── requirements.md
    ├── finding-closure.md
    ├── integration-consumer.md
    ├── governance-delivery.md
    ├── plan-quality.md
    ├── plan-review.md
    └── closure-readiness.md
```

### Existing and changed executable proof surfaces

```text
tests/
├── TuiVision.Core.Tests/Test1.cs
├── TuiVision.Controls.Tests/
│   ├── TProgramTests.cs
│   ├── TGroupTests.cs
│   ├── TDesktopTests.cs
│   ├── TApplicationTests.cs
│   ├── TWindowTests.cs
│   ├── TWindowMouseDragTests.cs
│   ├── TDialogTests.cs
│   ├── TInputLineTests.cs
│   ├── TFileDialogTests.cs
│   ├── MenuDescriptionTests.cs
│   └── StatusLineDescriptionTests.cs
├── TuiVision.Serialization.Tests/
│   ├── TResourceFileTests.cs
│   └── TUiDescriptionRecordTests.cs
└── TuiVision.Drivers.Tests/
    ├── ConformanceAuditEvidenceTests.cs
    └── ConformanceClosureEvidenceTests.cs   # new test-only validator

.github/workflows/ci.yml                     # add windows-latest to runtime matrix
```

**Structure Decision**: Keep the Feature-024 audit dataset canonical and
unchanged except for its human-readable gate/status surfaces. Feature 028 owns
one separate closure dataset that links back to canonical finding, resolution,
contract, source, consumer, and proof IDs. Product assemblies remain untouched.

## Phase 0: Research and Baseline Freeze

1. Freeze Feature-024 Revision 2 plus final 025/026 resolutions as immutable
   closure input.
2. Reconcile all 13 finding IDs, 48 contracts, current 139 maintained source
   files, 211 exported public types, and the existing source/proof relations.
3. Review the thirteen consumer groups and relevant historical/Free Vision
   sources read-only.
4. Inspect existing real-path tests against every R-028 slice; add no new
   behavior test when complete proof already exists.
5. Confirm the permanent CI gap: Ubuntu/macOS run the product suite while
   Windows currently runs only repository tooling.
6. Freeze eight applicable remote acceptance gates plus one explicit WSL
   `N/A` boundary before implementation. Keep independent workflow runs as
   independent gates so every Primary row maps to one immutable run.

## Phase 1: Evidence Model and Acceptance Contract

1. Model `ClosureRun`, `FindingClosure`, `IntegrationSlice`,
   `ConsumerReadiness`, `GovernanceDecision`, and `ValidationEvidence` in
   `data-model.md` and `closure-evidence.json`.
2. Require exact finding and slice sets, the exact thirteen-row consumer
   baseline, and closed vocabularies. Permit only uniquely identified
   additional consumer rows when the read-only review discovers a genuinely
   new shared-framework responsibility.
3. Require each proof reference to use `path::method` and exist on disk.
4. Require reciprocal links to canonical finding, resolution, and contract IDs.
5. Keep delivery facts and temporary exact-head evidence outside the closure
   dataset so they cannot invalidate themselves.

## Phase 2: Evidence Foundation and Test-First Validator

1. Create `pr-evidence.md` from the autonomous template before test or status
   edits.
2. Add `ConformanceClosureEvidenceTests.cs` first; observe the missing-dataset
   red proof after one build-counter increment.
3. Add the representative F001/R-028-001/TVDEMO slice, then expand the dataset
   to all required rows without changing product code.
4. Prove malformed JSON, duplicate IDs, unknown decisions, missing reciprocal
   links, missing source/proof paths, wrong cardinalities, and a ready state
   paired with any reopened finding or blocking consumer decision fail closed.
5. Review non-trivial validator logic for concise German-first/English-second
   didactic comments that explain relation or proof boundaries rather than
   restating code.
6. Run the completed closure validator and existing Feature-024 validator.

## Phase 3: Seven Real-Path Slices

| Slice | Existing primary proof family | Required closure signal |
|---|---|---|
| `R-028-001` | Core `TEvent` plus Controls `TProgram` and `TWindow` tests | raw adapter, concrete kind/modifiers, shortcut/fallback, dispatch and consumption |
| `R-028-002` | `TGroupTests` plus validator/focus tests | unique current focus, state rules, veto, preserved state, announcement |
| `R-028-003` | `TProgramTests` | pending-first, idle, command refresh, CPU release, shutdown |
| `R-028-004` | `TDesktopTests`, `TWindowTests`, `TDialogTests`, `TApplicationTests` | stack/geometry, safe close, modality, isolation, focus, view/cells |
| `R-028-005` | `TWindowMouseDragTests` and app-loop mouse integration | capture, threshold, bounds, target, commit/cancel, lifecycle, keyboard parity |
| `R-028-006` | `TDialogTests` and `TInputLineTests` | completion set, ordered real validation, focus/state preservation, accessible rejection |
| `R-028-007` | file-dialog, resource, and UI-description tests | typed modes, safe fixtures, strict reconstruction, malformed atomic rejection |

Execute each family through targeted Release filters and record exact totals.
A missing named boundary reopens the relevant owner; it is not replaced by a
new product fix inside 028.

## Phase 4: Consumer and Gate Decision

1. Re-read all six Wave-5 and seven Wave-6 baseline groups and retain source
   hashes or protected-tree diff evidence.
2. Assign exactly one consumer decision and complete traceability per row.
3. Confirm all thirteen findings remain `Closed` or
   `AlreadySatisfiedWithNewProof`; otherwise stop.
4. Update Feature-024 human-readable gate and consumer status to
   `ReadyForTerminalGuiAudit` only after all local gates pass.
5. Keep both Waves `BlockedPendingTerminalGuiAudit` and name Feature 029.
6. Archive Lastenhefte 10, 11, and 12 only at final completion.

## Phase 5: Full Validation and Delivery

1. Add `windows-latest` to the existing CI matrix and validate YAML plus the
   previously proven Bash-on-Windows command contract.
2. Run static checks, targeted closure/slice tests, full Release, canonical
   coverage, DocFX, Axe, Lynx, secrets, dependencies, protected-source and
   generated-output checks.
3. Update status, five agent surfaces, statistics, final evidence, and version
   serially.
4. Stage only the intended candidate and verify no untracked or unstaged
   remainder.
5. Publish the PR, map every declared gate to actual PR-head workflow/job/
   platform/command evidence, and validate the temporary JSON with Bash and
   PowerShell validators.
6. Merge, synchronize main, create the single causal closeout, and run the
   autonomous retrospective.
7. Promote a provider-neutral preset improvement only if reproducible; publish
   a patch release and install its exact tag ZIP before Feature 029. Otherwise
   record `NoPromotion` and create no empty branch, PR, or release.

## Validation Strategy

| Trigger | Command or proof | Acceptance |
|---|---|---|
| Every candidate | `git diff --check`, staged candidate inventory, marker and protected-path scans | zero defects and no missing intended file |
| C# test-only validator | `dotnet format --verify-no-changes --no-restore` and targeted Drivers test | format clean; closure validator passes |
| Finding/slice proof | batched Core, Controls, Serialization, Drivers Release filters | all named proof methods execute and pass |
| Repository integration | `dotnet test TuiVision.sln --configuration Release --no-restore` | full suite passes with explicit totals |
| Coverage | `xmllint --noout coverlet.runsettings` plus canonical Coverlet invocation | each mandatory assembly >= 70 percent |
| Documentation/status | `docfx docfx.json`, `npm run test:docfx`, UTF-8 Lynx | 0 DocFX errors/warnings, 2/2 Axe, semantic readable text |
| Workflow | YAML parse, immutable action pins, PR-head CI matrix | Ubuntu/macOS/Windows execute restore, build, test, and DocFX |
| Security/supply chain | repository secret scan, Gitleaks, dependency and SBOM workflow | zero High secrets and no unresolved dependency finding |
| Remote review | required PR-context checks, Claude/Copilot state, GraphQL threads | technical green; unavailable review honest; actionable threads 0 |
| Exact-head gates | temporary provider-neutral evidence plus both v0.2.0 gate-evidence validators | requirements hash/head/tokens/one-primary all pass |
| Resume state | both v0.2.0 run-state validators at logical phase boundaries | branch, feature, hashes, task counts, authority, stage, and next action agree |

## Version and Build-Counter Strategy

- Branch version is `1.28.<patch>.<build>` with all three fields aligned.
- Before every explicit `dotnet build` or `dotnet test`, increment only the
  manual build counter and record the value in `pr-evidence.md`.
- One increment authorizes one command. Related filters are batched in that
  command rather than creating administrative repeats.
- Before each commit or push, align the patch to the resulting branch commit
  count without incrementing build unless another build/test executes.

## Complexity Tracking

No Constitution violation or product complexity exception is required. The
new closure validator is test-only, the dataset is feature-local, and the CI
change extends an already proven matrix by one runner rather than introducing a
second workflow.
