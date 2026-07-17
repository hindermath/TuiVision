# Implementation Plan: Wave-5 TP7 Showcase Remediation

**Branch**: `033-wave5-tp7-showcase-remediation` | **Date**: 2026-07-17 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/033-wave5-tp7-showcase-remediation/spec.md`

## Summary

Feature 033 completes the visible Wave-5 showcase stage for the ten TP7
examples delivered functionally by Feature 032. The existing domain, file,
Help, Resource, capability, and security contracts remain unchanged. Each
application receives a concrete focusable main composition, a real
`TStatusLine`, a keyboard-reachable `Help -> Description` path, complete
keyboard commands, and normal plus constrained app-loop/state/view/cell proof.

The implementation extends only the existing compiled Wave-5 example assembly
and its smoke tests. `Tp7Calculator` is the test-first vertical slice. Shared
showcase helpers remain presentation-only; reusable framework behavior stays
in the existing TuiVision projects. Any broader defect is recorded as
`FollowUpHardening` and is not repaired in this feature.

## Technical Context

**Language/Version**: C# 14 / .NET 10
**Primary Dependencies**: Existing TuiVision.Core, TuiVision.Controls, TuiVision.Serialization, TuiVision.Compatibility, TuiVision.Drivers.Console, MSTest 4.0.1, and BCL APIs; no new package
**Storage**: Existing controlled test-temporary editor/resource files and embedded Help/Resource fixtures; no database, service, or arbitrary user file
**Testing**: MSTest Release app-loop smokes, evidence-validator negative tests,
full solution tests, canonical Coverlet five-assembly gate, DocFX, and
Playwright/Axe
**Target Platform**: macOS local development; GitHub-hosted Ubuntu, macOS, and
Windows acceptance runners
**Project Type**: Multi-project C#/.NET terminal UI framework with ten console examples and one shared Wave-5 example-support assembly
**Performance Goals**: Deterministic headless runs terminate without
unbounded idle work; each normal and constrained composition renders within
its declared viewport
**Constraints**: No functional re-port, framework redesign, API break, package
update, host mutation, arbitrary user-file access, process, shell, PTY,
historical-source edit, Wave-6 code, or Feature 034
**Scale/Scope**: Ten example compositions, ten framework decisions, ten
primary proofs, ten constrained-layout proofs, ten guides, and one exact
showcase evidence matrix

## Constitution Check

*GATE: Passed before research; rechecked after design.*

| Gate | Decision and evidence |
|---|---|
| Level-2 environment | TuiVision remains the registered .NET 10, MSTest, DocFX, and Axe Level-2 project |
| Memory-safe language | C# is on the MSL allow-list; Pascal and C/C++ remain read-only evidence |
| Secure generation | Existing closed commands, exact Resource keys, bounded Help compilation, controlled roots, and fail-closed rejection remain binding |
| Secure architecture | Showcase composition is separated from framework and domain behavior; file, parser, capability, evidence, and delivery boundaries remain explicit |
| Security documentation | Feature-local `pr-evidence.md` is proportional evidence; no product trust boundary requires a new `docs/security/` document |
| NIST SSDF / CWE Top 25 | Applicable to generated code review, controlled input preservation, test validity, supply chain, and exact-head delivery |
| OWASP ASVS | `N/A`: no web, authentication, session, HTTP, or service surface |
| SBOM / VEX / SLSA / OpenSSF | Existing workflows remain delivery gates; no package or release component changes, so no feature-owned artefact is triggered |
| AI-SBOM | `N/A`: AI is development tooling only; no model, dataset, inference service, or AI runtime ships |
| STRIDE / CIA / CAPEC | Applicable to command misuse, focus/dispatch, path traversal, malformed Help/Resource input, capability claims, and evidence tampering |
| S-ADR / arc42 security | `N/A`: no product architecture or security concept changes |
| Zero Trust / SAMM | `N/A`: no identity, network, deployment, service, or maturity-program change |
| BSI C3A / BSI C5 | `N/A`: no cloud service, provider dependency, shared responsibility, or cloud assurance scope |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: no new regulated role, product boundary, runtime AI, or financial ICT service |
| Presets | Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0, Cross-Platform 0.2.0, Agent Parity 0.3.0, Autonomous Run 0.2.2 |
| Security-first | Credentials, agent state, logs, caches, `_site/`, generated API YAML, TestResults, and external checkouts remain untracked |
| Inclusion/A11Y | Ten examples and guides require keyboard parity, focus/status text, semantic Markdown, constrained layouts, and WCAG 2.2 AA evidence |
| Bilingual delivery | Learner-facing guides and Description blocks are German first, English second at CEFR-B2 |
| Didactic comments | New non-trivial composition and proof logic is reviewed selectively for why, constraint, historical deviation, and proof-boundary comments |
| Statistics | `docs/project-statistics.md` is updated with the existing Thorsten-solo productivity baseline and final `## Gesamtstatistik` section |
| Agent parity | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` are reviewed together |

**Post-design recheck**: Passed. All new code remains example presentation or
proof code in existing projects. No Constitution exception or architecture
waiver is required.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction to implement the approved plan
through autonomous Feature-033 delivery
**Evidence path**: `specs/033-wave5-tp7-showcase-remediation/pr-evidence.md`
**Representative vertical slice**: Add missing showcase assertions for
`Tp7Calculator`, observe the red proof, implement its display/button
composition, shared Description/status path, `40x12` layout, guide, and
evidence row, then spread the proven composition model
**Convergence gates**: No material Clarify question; every requirements and
plan-review checklist item passes; Analyze has no Critical/High and no
unowned Medium; all tasks complete; local, remote, review, and exact-head
gates pass
**Shared single-writer files**: `Wave5Application.cs`, smoke-test project,
`pr-evidence.md`, `tasks.md`, `autonomous-run-state.json`,
`autonomous-gate-requirements.json`, `Directory.Build.props`,
`examples/README.md`, DocFX navigation, five agent surfaces, Pflichtenheft,
processing order, statistics, and archived intake
**Validation triggers**: Static and scope checks always; targeted showcase
smokes; full Release and coverage because shared executable example code
changes; DocFX/A11Y because ten learner guides change; platform gates because
runnable TUI examples change; script parity `N/A` unless a script changes
**Scope firewall**: Broad, API-breaking, behavior-expanding, Wave-6, or
Feature-032-contract defects become owned `FollowUpHardening` rows; no hidden
example-local framework substitute is allowed
**Remote closeout**: Commit and push the exact candidate, open the feature PR,
validate exact-head provider evidence, converge checks and review threads,
use the authorized Human-Approval-only bypass only when all technical gates
are green, merge, synchronize `main`, and use one evidence-only causal
closeout only for terminal facts that cannot exist on the reviewed head

## Architecture and Evidence Strategy

### Shared showcase shell

`Wave5Application` owns only presentation-level composition:

- deterministic event queue and controlled quit for smoke proof;
- a shared keyboard-reachable Description command;
- status text that always names the example and current state;
- stable main-view identity and screen-region tracking;
- helpers for bounded normal and constrained layout.

It does not own calculator, editor, Help, Resource, calendar, puzzle, or mouse
semantics. Those remain in the accepted Feature-032 application/domain types
and existing framework controls.

### Example composition plan

| Example | Main composition | Core keyboard proof | Decision |
|---|---|---|---|
| `Tp7Calculator` | Dialog with display and button grid | Digit/operator/equal, rejection, Description | `UseExistingFramework` |
| `Tp7Demo` | Desktop window family with menu commands | Open, Tile, Cascade, Next, Close, Description | `UseExistingFramework` |
| `Tp7Edit` | Existing `TEditWindow` with File/Edit/Search menu | Edit, save boundary, safe close, Description | `UseExistingFramework` |
| `Tp7Help` | Compiler diagnostics and `THelpWindow` | Topic, cross-reference, Back, fallback, Description | `UseExistingFramework` |
| `Tp7ResourceDemo` | Reconstructed dialog/menu/status composition | Load, choose, reject, Description | `UseExistingFramework` |
| `Tp7ResourceGenerator` | Target, Generate, progress/result dialog | Target, Generate, reject, Description | `UseExistingFramework` |
| `Tp7AsciiTable` | Focusable 16x16 grid view | Arrows, paging, direct select, Description | `UseExistingFramework` |
| `Tp7Calendar` | Focusable month/day grid view | Day/month navigation, Description | `UseExistingFramework` |
| `Tp7Puzzle` | Focusable 4x4 grid view | Arrows/direct tile, rejection, Description | `UseExistingFramework` |
| `Tp7MouseDialog` | Settings dialog with focusable controls | Delay, order, activation, Description | `UseExistingFramework` |

The planned decisions may change only when the evidence records the reason.
`SmallFrameworkFix` requires an observed red test and bounded regression
proof. `FollowUpHardening` stops the affected slice.

### Historical intent

The 15 accepted `TVDEMOS/*.PAS` files reviewed by Feature 032 are read-only.
Feature 033 rechecks the visible component family, user flow, shortcut intent,
and command meaning. Pascal object layout, DOS runtime, globals, overlays,
binary activation, host mutation, and source shape remain intentional modern
deviations.

## Implementation Phases

### Phase A - Evidence foundation and calculator slice

1. Finalize gate requirements, evidence schema, and historical review rows.
2. Add failing calculator showcase, Description, focus, and `40x12` tests.
3. Implement the shared Description/status path and calculator composition.
4. Complete the first exact showcase row and guide.

### Phase B - Demo, editor, and help

1. Add demo window-family and desktop command proofs.
2. Add editor menu/chrome and controlled dialog proofs without changing file
   semantics.
3. Add Help compiler/viewer/navigation/fallback composition.
4. Complete each guide, shortcut inventory, constrained proof, and decision.

### Phase C - Resource compositions

1. Add Resource-demo dialog/menu/status reconstruction and atomic rejection.
2. Add generator target/progress/result controls within the owned root.
3. Preserve exact keys, allowlist records, and no-partial-model behavior.

### Phase D - Grid and mouse compositions

1. Add ASCII, calendar, and puzzle focusable grids with visible selection.
2. Add mouse settings controls, honest capability, keyboard parity, and no
   host mutation.
3. Complete normal/constrained proofs and Description paths.

### Phase E - Evidence, documentation, and delivery

1. Validate exactly ten complete showcase rows and malformed negative cases.
2. Update ten guides, examples overview, DocFX navigation if required,
   status/order/agent/statistics surfaces, and archive Lastenheft 18.
3. Run all local gates with version-counter discipline.
4. Commit, push, review exact head, merge, synchronize `main`, and record the
   retrospective without starting Wave 6 or Feature 034.

## Validation Strategy

| Gate | Planned proof |
|---|---|
| Static candidate | `git diff --check`, staged inventory, placeholder scan, generated/protected path scan |
| Run state | Installed Bash and PowerShell validators at every logical checkpoint |
| Targeted examples | Release filter for `Tp7*Showcase`, existing `Tp7*`, and Wave-5 evidence tests |
| Normal entry points | Ten Release `dotnet run --project ... -- --smoke` invocations after one explicit build, with no further implicit build |
| Full regression | One full `dotnet test TuiVision.sln --configuration Release` invocation |
| Coverage | Canonical Coverlet invocation with five required assemblies at or above 70 percent |
| Format | `dotnet format TuiVision.sln --verify-no-changes` |
| Documentation | `docfx docfx.json`, Playwright/Axe, UTF-8 and text-first review |
| Security/scope | Secret scan, supply-chain workflow, dependency/project diff, and read-only-root scan |
| Agent parity | Local maintained-surface parity plus remote Bash/PowerShell parity jobs |
| Platform | Ubuntu, macOS, and Windows jobs that execute the real Release/example proof |
| Exact head | Temporary provider evidence validated against committed gate requirements and reviewed PR head |
| Review | GraphQL thread state, PR comments, reviewer outcomes, and unavailable-review evidence |

Before every explicit `dotnet build` or `dotnet test`, align all version fields
to `1.33.<patch>.<build>` and increment the manual build counter exactly once.
Before commit or push, realign the patch without another build increment unless
another build or test ran.

## Project Structure

```text
specs/033-wave5-tp7-showcase-remediation/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
├── contracts/wave5-showcase-acceptance.md
├── data-model.md
├── plan.md
├── pr-evidence.md
├── quickstart.md
├── research.md
├── retrospective.md
├── spec.md
└── tasks.md

examples/Shared/TuiVision.Examples.Wave5/
├── Wave5Application.cs
├── Tp7CalculatorApp.cs
├── Tp7DemoApp.cs
├── Tp7EditApp.cs
├── Tp7HelpApp.cs
├── Tp7ResourceApps.cs
└── Tp7DomainApps.cs

tests/TuiVision.Examples.SmokeTests/
├── Tp7CalculatorSmokeTests.cs
├── Tp7ApplicationSmokeTests.cs
├── Tp7ResourceSmokeTests.cs
├── Tp7DomainSmokeTests.cs
└── Wave5ShowcaseSmokeMatrixTests.cs

docs/guides/examples/tp7-*.md
```

**Structure Decision**: The existing compiled Wave-5 assembly remains the only
shared example composition location. Ten launch projects stay thin and
independent. No framework project, package, external service, or second local
framework layer is introduced.

## Complexity Tracking

No Constitution violation requires a waiver.
