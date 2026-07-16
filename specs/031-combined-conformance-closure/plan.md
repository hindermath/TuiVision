# Implementation Plan: Gemeinsamer Konformitätsabschluss

**Branch**: `031-combined-conformance-closure` | **Date**: 2026-07-16 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/031-combined-conformance-closure/spec.md`

## Summary

Feature 031 liefert eine unabhängige, evidence-only Closure auf dem gemergten
Stand der Features 024, 025, 026, 028, 029 und 030. Ein neuer geschlossener
Datensatz bindet Eingabeartefakte, externe Pins, 48 Verträge, 13 Consumer,
48 TGO- und 48 MB-Beobachtungen, 96 Dispositionen, 13 frühere Findings, leere
Ownergruppen, Governance und Validierung. Ein einzelner test-only
MSTest-Validator rekonstruiert diese Mengen direkt aus den akzeptierten
Vorgängerdateien und lehnt Drift, Duplikate, unbekannte IDs, unterdrückte
Findings und verfrühte Wave-Zustände fail-closed ab.

Der reviewte Feature-Head hält Wave 5 und Wave 6 gesperrt. Nach grünen lokalen,
plattformbezogenen, remoten und Exact-Head-Gates sowie dem Feature-Merge setzt
ein einzelner nicht rekursiver Evidence-Closeout Wave 5 auf `Eligible` und
Wave 6 höchstens auf `ConditionallyReady`. Kein Produkt-, API-, Dependency-,
Beispiel-, Consumer- oder historischer Source-Pfad wird verändert.

## Technical Context

**Language/Version**: C# 14 / .NET 10 for test-only validation; JSON and Markdown for evidence
**Primary Dependencies**: Existing BCL `System.Text.Json`, MSTest 4.0.1, existing repository scripts and workflows; no new package
**Storage**: Source-controlled closed JSON and Markdown evidence; temporary external checkouts under `/tmp` only
**Testing**: MSTest in `tests/TuiVision.Drivers.Tests`, full solution Release tests, Coverlet collector, DocFX, Playwright/Axe
**Target Platform**: macOS local development plus GitHub-hosted Ubuntu, macOS, and Windows runners
**Project Type**: Multi-project C#/.NET terminal UI framework with evidence-only closure feature
**Performance Goals**: Validation remains bounded to six predecessor features, 48 contracts, 13 consumers, 96 observations, 13 prior findings, 90 external source hashes, and seven presets
**Constraints**: No runtime/API/dependency/project/example/consumer changes; protected and external sources read-only; one build-counter increment per `dotnet build` or `dotnet test` invocation
**Scale/Scope**: One closure JSON dataset, one test-only validator class, one readable closure report, one pre-wave gate, planning/evidence/checklists, synchronized status surfaces, and an optional causal closeout

## Constitution Check

*GATE: Passed before research; rechecked after design.*

| Gate | Decision and evidence |
|---|---|
| Level-2 environment | TuiVision uses the registered C#/.NET 10, MSTest, DocFX/Axe, statistics, and five-agent surface baseline |
| Memory-safe language | C# is on the Constitution MSL allow-list; C++, Pascal, and external C# remain read-only evidence |
| Secure generation | Test-only parser uses `System.Text.Json`, closed field expectations, bounded exact sets, ordinal comparison, no reflection-based activation, and fail-closed errors |
| Secure architecture | No runtime trust boundary changes. Evidence integrity uses immutable pins, SHA-256, reciprocal links, separation of accepted input from closure result, and least-authority delivery |
| Security documentation | Feature-local evidence is the justified equivalent governance location. Existing `docs/security/` files are unchanged because no product architecture or distribution boundary changes |
| NIST SSDF / CWE Top 25 | Applicable to evidence integrity, malformed data, test validity, scope control, review, and exact-head delivery |
| OWASP ASVS | `N/A`: no web, HTTP, authentication, session, or service surface |
| SBOM / VEX / SLSA / OpenSSF | Existing supply-chain workflow remains applicable as a delivery gate; no new feature-owned artefact is required because packages and distribution are unchanged |
| AI-SBOM | `N/A`: AI is development tooling only; no model, dataset, inference service, infrastructure, or runtime AI ships |
| STRIDE / CIA / CAPEC | Applicable to evidence tampering, omission, duplicate relations, false gate claims, and unavailable provenance |
| S-ADR / arc42 security | `N/A`: no architecture decision or product security concept changes |
| Zero Trust / SAMM | `N/A`: no identity, network, deployment, maturity-program, or service boundary changes |
| BSI C3A / BSI C5 | `N/A`: no cloud service, provider dependency, shared-responsibility model, assurance scope, or operational cloud evidence changes |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: private training framework; no new regulated operator, product distribution, runtime AI, or financial ICT role |
| Presets | Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0, Cross-Platform 0.2.0, Agent Parity 0.3.0, Autonomous Run 0.2.2 |
| Security-first | No credentials, agent state, logs, histories, databases, caches, external checkouts, test output, `_site/`, or generated API YAML are tracked |
| Inclusion/A11Y | Feature artifacts, status text, closure report, statistics, and generated documentation receive semantic, DE-first/EN-second, CEFR-B2, text-first, DocFX, Playwright/Axe, and Lynx review |
| Statistics | `docs/project-statistics.md` is updated after the final feature candidate; reference baselines remain 80 and 125 lines per workday |
| Agent parity | The five maintained surfaces change together for active 031 planning and later causal Wave state; `.specify/templates/` remain unchanged |

**Post-design recheck**: Passed. The design adds only closed evidence and
test-only validation. No Constitution exception or complexity waiver is needed.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction
**Evidence path**: `specs/031-combined-conformance-closure/pr-evidence.md`
**Representative vertical slice**: Bind the six accepted input datasets and
complete `C001`, `W5-001`, `TGO001`, `MB001`, and `F001` with exact hashes,
reciprocal references, one closed decision, fail-closed duplicate mutation,
and targeted Red/Green proof before filling the remaining rows.
**Convergence gates**: Clarify has no material question; all checklists pass;
Plan review has no actionable note; Analyze has no Critical/High and no
undisposed Medium; all tasks complete; all local and remote gates pass; no
actionable review thread remains.
**Shared single-writer files**: `pr-evidence.md`, `closure-evidence.json`,
`pre-wave-gate.md`, `Directory.Build.props`, `docs/project-statistics.md`,
`Pflichtenheft.md`, `Lastenheft_Abarbeitungsreihenfolge.md`, five agent
surfaces, run state, task file, archived Lastenheft, and causal closeout.
**Validation triggers**: Static and scope checks always; targeted test for the
new validator; full Release and canonical coverage because shared audit proof
and repository release readiness are binding; DocFX/A11Y because learner-facing
status documentation changes; no visual app-loop gate because no TUI changes;
script parity `N/A` because no script is planned.
**Scope firewall**: Any product, API, architecture, dependency, example,
consumer, historical, or external-source defect blocks 031 and becomes a
separate reviewed intake. It is never repaired inside this feature.
**Remote closeout**: Commit and push the exact candidate, create the feature
PR, map every gate to actual workflow/job/platform/command evidence, converge
reviews, use the narrow Human-Approval-only bypass only under the authorized
conditions, merge, delete the branch, synchronize `main`, then use one
evidence-only closeout PR for post-merge Wave state and terminal run facts.

## Evidence and Data Architecture

### Binding Inputs

| Feature | Primary structured input | Closure responsibility |
|---|---|---|
| 024 | `conformance-audit.json` | `C001`-`C048`, `F001`-`F013`, resolutions, Free Vision pin and source records |
| 025 | `pr-evidence.md` plus Feature-024 resolutions | `F001`-`F009` real Red/Green ownership |
| 026 | `pr-evidence.md` plus Feature-024 resolutions | `F010`-`F013` real Red/Green ownership |
| 028 | `closure-evidence.json` | 13 finding closures, seven slices, 13 consumer groups |
| 029 | `terminalgui-conformance-audit.json` | Terminal.GUI pin, 25 sources, 48 relations, 13 consumers, 48 TGO observations |
| 030 | `magiblot-evolution-audit.json`, `combined-conformance-findings.json` | magiblot pin, 50 sources, 48 relations, 13 consumers, 48 MB observations, 96 dispositions, zero findings |

Every binding input receives a repository-relative path and SHA-256 in
`closure-evidence.json`. The new validator recalculates each hash before using
the file so a copied summary cannot hide predecessor drift.

### Closure Dataset

`closure-evidence.json` contains:

1. run identity, baseline commit, blocked feature-head Wave states, target
   post-merge states, owner, reviewer, date, result, risk, and trigger;
2. accepted input paths and hashes;
3. three external source baselines with exact Git/license identities, manifest
   paths, accepted source counts, and source-ID/hash references;
4. 48 contract closure rows, each linking Feature-024 decision and proof,
   Free Vision relation, Terminal.GUI relation/TGO, magiblot relation/MB, final
   disposition, consumers, metadata, and result;
5. 13 consumer closure rows reconciled across Features 028, 029, and 030;
6. 96 observation closure rows copied by identity and compared back to the
   Feature-030 combined dataset;
7. 13 prior finding closure rows reconciled with Feature 024 and Feature 028;
8. three allowed empty owner groups, zero findings, zero product decisions,
   zero dependency edges, and zero hardening intakes;
9. seven-preset governance rows and local/remote validation rows;
10. one explicit causal Wave-transition contract.

### Test-only Validator

`CombinedConformanceClosureEvidenceTests.cs` uses only BCL JSON APIs and
existing `Phase7DriverTestContext.FindRepoRoot()`. It provides:

- existence and root-schema proof;
- exact accepted-input SHA-256 proof;
- exact external pin and manifest/source-count proof;
- reference-slice proof for `C001`, `W5-001`, `TGO001`, `MB001`, `F001`;
- complete contract, consumer, observation, finding, owner, and intake proof;
- reciprocal link and proof-path existence checks;
- governance metadata and vocabulary proof;
- feature-head Wave-state and causal transition proof, including a dual-state
  marker contract that accepts post-merge eligibility only when complete
  `delivery-closeout.md` evidence exists;
- malformed JSON, missing row, duplicate ID, unknown decision, non-empty owner,
  injected finding, suppressed intake, and premature `Eligible` negative tests.

The validator does not duplicate the complete Feature-024/028/029/030
validators. It composes their accepted contracts and checks the new cross-file
closure relationships.

Before the first Red invocation, review the full compile surface: required
imports, MSTest 4 APIs, repository-root helpers, every public test method's
German-first/English-second XML summary, and moderate didactic comments around
non-trivial cross-file or causal-state validation.

## Implementation Phases

### Phase A - Foundation and Red Proof

1. Freeze exact predecessor paths and SHA-256 values in evidence.
2. Create a minimal closure dataset with run, input, source, and Wave contract.
3. Add the new test class with existence and representative-slice tests.
4. Run the targeted Red test before the full row sets exist.

### Phase B - Complete Independent Closure

1. Fill all 48 contract rows from accepted structured inputs.
2. Fill all 13 consumer rows and reconcile source paths and proof references.
3. Fill all 96 observation rows and reconcile each disposition.
4. Fill all 13 prior finding closures.
5. Add empty owner groups and prove zero finding/intake/product/edge sets.
6. Complete malformed and contradiction matrices.

### Phase C - Governance, Markers, and Local Gates

1. Complete seven-preset governance and validation rows.
2. Create `combined-closure.md` and `pre-wave-gate.md` with feature-head blocked
   states and post-merge targets.
3. Synchronize active Feature-031 context across all five agent surfaces.
4. Update Pflichtenheft, processing order, archived Lastenheft marker, and
   project statistics without releasing Wave 5 on the feature head.
5. Search every marker consumer in tests, feature datasets, Pflichtenheft,
   processing order, all five agent surfaces, gate/evidence files, and
   statistics. Prove that every assertion uses the dual-state causal contract
   before the closeout is attempted.
6. Run static, targeted, full, coverage, documentation, A11Y, text-first,
   security, scope, and parity validation.

### Phase D - Exact-head Delivery and Causal Closeout

1. Align version, stage exact intended files, and validate the candidate.
2. Commit, push, create PR, and map exact-head gates.
3. Resolve review findings and revalidate the changed head.
4. Merge after convergence, clean branches, and synchronize `main`.
5. Create one evidence-only closeout that records the feature merge and
   provider evidence, marks Wave 5 `Eligible`, Wave 6 `ConditionallyReady`,
   completes tasks/state/retrospective/statistics, and does not name its own
   remote identity inside the closeout file.
6. Keep the closeout free of test and executable logic. The feature-branch
   validator must already accept the post-merge state only under complete
   causal evidence.
7. Merge the closeout and finish on clean synchronized `main`.

## Validation Strategy

| Gate | Planned proof |
|---|---|
| Static candidate | `git diff --check`, staged candidate inventory, no placeholders, protected/generated path scan |
| State | Bash validator locally; PowerShell validator through available remote PowerShell jobs when local `pwsh` is unavailable |
| Targeted closure | Release test filtered to `CombinedConformanceClosureEvidenceTests` and accepted predecessor audit validators |
| Full regression | One full `dotnet test TuiVision.sln --configuration Release` invocation |
| Coverage | Canonical `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings` invocation and five-assembly extraction |
| Format | `dotnet format --verify-no-changes` |
| Documentation | `docfx docfx.json`, then `tests/web-a11y` Playwright/Axe and UTF-8 Lynx review |
| Security/scope | agent secret scan, Gitleaks provider job, supply-chain job, dependency/project diff, protected-path and generated-output checks |
| Agent parity | Bash homogeneity locally and Bash/PowerShell remote matrix |
| Platform | PR-context Ubuntu, macOS, and Windows CI jobs executing the real Release body |
| Exact head | Temporary gate evidence validated against committed requirements and reviewed PR head |
| Review | GraphQL thread state, PR comments, reviewer results, unavailable-provider evidence |

Before every `dotnet build` or `dotnet test`, calculate the numbered-branch
version, set all three version fields to `1.31.<patch>.<build>`, and increment
only the manual build counter. Before commit or push, align the three fields
again without incrementing unless another build or test ran.

## Project Structure

### Documentation and Evidence

```text
specs/031-combined-conformance-closure/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
│   ├── closure-evidence.md
│   ├── plan-quality.md
│   ├── plan-review.md
│   ├── requirements.md
│   ├── stop-and-scope.md
│   └── wave-delivery.md
├── closure-evidence.json
├── combined-closure.md
├── contracts/
│   └── combined-conformance-closure-acceptance.md
├── data-model.md
├── delivery-closeout.md
├── plan.md
├── pre-wave-gate.md
├── pr-evidence.md
├── quickstart.md
├── research.md
├── retrospective.md
├── spec.md
└── tasks.md
```

### Test and Shared Status Surfaces

```text
tests/TuiVision.Drivers.Tests/
└── CombinedConformanceClosureEvidenceTests.cs

Directory.Build.props
Pflichtenheft.md
Lastenheft_Abarbeitungsreihenfolge.md
Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md
docs/project-statistics.md
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

**Structure Decision**: Keep all new behavior inside one existing test project
and feature-local evidence. Shared repository files change only for versioning,
status, ordering, statistics, and required agent parity.

## Complexity Tracking

No Constitution violation or additional project/dependency is planned.
