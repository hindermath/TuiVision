# Implementation Plan: Wave-5 TP7 Functional Porting

**Branch**: `032-wave5-tp7-functional-porting` | **Date**: 2026-07-16 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/032-wave5-tp7-functional-porting/spec.md`

## Summary

Feature 032 liefert die funktionale erste Wave-5-Stufe für alle 15
read-only Pascal-Quellen unter `TVDEMOS/`. Zehn eigenständig startbare
C#-Beispiele verwenden eine gemeinsame kompilierte Beispielassembly für
Shell-, Zustands- und Proof-Komposition, während alle wiederverwendbaren
Runtime-Verträge aus den bestehenden TuiVision-Projekten kommen.

`Tp7Calculator` ist der test-first Referenz-Slice. Danach folgen Demo, Editor,
Help, Resource-Demo, Resource-Generator, ASCII-Tabelle, Kalender, Puzzle und
Mausdialog. Jeder primäre Smoke läuft durch `app.Run()` und verbindet
Fachzustand, View-Identität sowie gerenderte Zellen. Eine zehnzeilige
Showcase-Delta-Matrix erzeugt anschließend den verbindlichen Intake für die
zweite Wave-5-Stufe, ohne Feature 033 zu starten.

## Technical Context

**Language/Version**: C# 14 / .NET 10
**Primary Dependencies**: Existing TuiVision.Core, TuiVision.Controls, TuiVision.Serialization, TuiVision.Compatibility, TuiVision.Drivers.Console, MSTest 4.0.1 and BCL System.Text.Json; no new package
**Storage**: Embedded UTF-8 fixtures, controlled test-temporary files, existing THelpFile and TResourceFile; no database or service
**Testing**: MSTest Release smokes, full solution tests, canonical Coverlet
five-assembly gate, DocFX, Playwright/Axe
**Target Platform**: macOS local development; GitHub-hosted Ubuntu, macOS and
Windows acceptance runners
**Project Type**: Multi-project C#/.NET terminal UI framework with ten console example executables and one shared example-support assembly
**Performance Goals**: Every scripted headless app loop terminates
deterministically; resource/help inputs stay within existing bounded parsers;
idle work is constant and bounded per cycle
**Constraints**: No framework redesign, API break, package update, host
configuration mutation, arbitrary user-file access, process, shell, PTY,
native bridge, Wave-6 code or historical-source change
**Scale/Scope**: 15 source roles, six consumers, ten example projects, ten
primary app-loop proof rows, ten guides and ten showcase-delta rows

## Constitution Check

*GATE: Passed before research; rechecked after design.*

| Gate | Decision and evidence |
|---|---|
| Level-2 environment | TuiVision remains the registered .NET 10/MSTest/DocFX/Axe Level-2 project |
| Memory-safe language | C# is on the MSL allow-list; Pascal and C/C++ remain read-only evidence |
| Secure generation | Closed commands and records, ordinal keys, bounded parsers, controlled roots and no reflection-based type activation |
| Secure architecture | Example composition is separated from framework behavior; file, parser, mouse capability and delivery boundaries fail closed |
| Security documentation | Feature-local `pr-evidence.md` and governance rows are the proportional evidence location; no product trust boundary requires a new `docs/security/` document |
| NIST SSDF / CWE Top 25 | Applicable to generated code review, input rejection, file ownership, test validity, supply chain and exact-head delivery |
| OWASP ASVS | `N/A`: no web, HTTP, authentication, session or service surface |
| SBOM / VEX / SLSA / OpenSSF | Existing repository supply-chain workflows remain delivery gates; no new package or release component triggers a feature-owned artefact |
| AI-SBOM | `N/A`: AI is development tooling only and no model, dataset, inference service or AI runtime ships |
| STRIDE / CIA / CAPEC | Applicable to path traversal, malformed Help/Resource input, duplicate records, command misuse, capability claims and evidence tampering |
| S-ADR / arc42 security | `N/A`: no product architecture or security-concept change |
| Zero Trust / SAMM | `N/A`: no identity, network, deployment, service or maturity-program change |
| BSI C3A / BSI C5 | `N/A`: no cloud service, provider dependency, shared-responsibility model or cloud assurance scope |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: no new regulated role, distributed product boundary, runtime AI or financial ICT service |
| Presets | Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0, Cross-Platform 0.2.0, Agent Parity 0.3.0, Autonomous Run 0.2.2 |
| Security-first | No credentials, agent state, caches, logs, `_site/`, generated API YAML, test results or external checkouts enter Git |
| Inclusion/A11Y | Ten examples and guides require keyboard paths, text-first state, focus/status evidence, semantic Markdown and WCAG 2.2 AA review where applicable |
| Bilingual delivery | Learner-facing guides and explanatory blocks are German first, English second at CEFR-B2 |
| Statistics | `docs/project-statistics.md` is updated after the final candidate using the existing 80/125 lines-per-workday references |
| Agent parity | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` and `.github/agents/copilot-instructions.md` are reviewed and synchronized together |

**Post-design recheck**: Passed. The deliberately shared example assembly
prevents duplicated linked-source CLR identities and does not become a
replacement framework. No Constitution exception is required.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction to execute the ordered sequence
through autonomous Feature 032 delivery
**Evidence path**: `specs/032-wave5-tp7-functional-porting/pr-evidence.md`
**Representative vertical slice**: Create the shared shell plus
`Tp7Calculator`, first add a failing/missing real-path smoke for valid
calculation and division rejection, then implement the smallest app, guide and
source/consumer evidence row before spreading the pattern
**Convergence gates**: Clarify has no material question; 73/73 requirements
checklist items pass; Plan review has no actionable finding; Analyze has no
Critical/High and no unowned Medium; every task is complete; all local,
remote, review and exact-head gates pass
**Shared single-writer files**: `pr-evidence.md`, `tasks.md`,
`autonomous-run-state.json`, `autonomous-gate-requirements.json`,
`TuiVision.sln`, smoke-test project, `examples/README.md`,
`Directory.Build.props`, DocFX navigation, five agent surfaces,
Pflichtenheft, processing order, statistics, archived intake and later
showcase intake
**Validation triggers**: Static and scope checks always; targeted Wave-5
smokes; full Release and coverage because ten executable projects and shared
proof code are added; DocFX/A11Y because ten learner guides and navigation
change; platform gates because runnable TUI examples change; script parity
`N/A` because no script is planned
**Scope firewall**: Any broad, API-breaking, Feature-031-contradicting or
shared runtime defect is recorded as `FollowUpHardening` and stops its slice;
it is not hidden in example-local logic
**Remote closeout**: Commit and push the exact candidate, create the feature
PR, validate exact-head provider evidence, converge checks and review threads,
use the authorized Human-Approval-only bypass only if all technical gates are
green, merge, synchronize `main`, then use one evidence-only causal closeout
only for facts that cannot truthfully exist before merge

## Architecture and Evidence Strategy

### Shared compiled example assembly

`examples/Shared/TuiVision.Examples.Wave5/` contains:

- `Wave5Application`: headless event queue, controlled quit, visible status,
  current proof region and common text-view replacement.
- `Wave5Domain.cs`: deterministic calculator, calendar, ASCII and puzzle
  value/state models used only by Wave-5 examples.
- one application class per managed example.

The assembly references existing framework modules. It does not add reusable
product behavior, parser formats, driver capabilities or persistence
semantics. Ten executable projects contain only their project file and normal
console entry point, reference this one assembly and remain independently
startable.

### Consumer implementation

| Consumer | Modern example proof | Planned decision |
|---|---|---|
| W5-001 Demo/gadgets | `Tp7DemoApp` opens typed demo windows and advances one bounded deterministic gadget cycle | `UseExistingFramework` |
| W5-002 Editor | `Tp7EditApp` uses `TFileEditor`/`TEditWindow`, safe close and controlled save root | `UseExistingFramework` |
| W5-003 Runtime resources | `Tp7ResourceDemoApp` reconstructs named visible content from exact keys | `UseExistingFramework` |
| W5-004 Generator | `Tp7ResourceGeneratorApp` creates only the allowlisted existing record set inside a controlled root | `UseExistingFramework` |
| W5-005 Domain examples | Calculator, ASCII, calendar and puzzle use deterministic local value models and existing views/commands | `UseExistingFramework` |
| W5-006 Mouse dialog | `Tp7MouseDialogApp` uses existing mouse events/capability state plus complete keyboard commands | `UseExistingFramework` |

### Historical source roles

| Source | Primary role | Modern target |
|---|---|---|
| `TVDEMO.PAS` | `EntryPoint` | `Tp7Demo` |
| `DEMOCMDS.PAS` | `SupportUnit` | typed Wave-5 command constants |
| `DEMOSTRS.PAS` | `FixtureOrContent` | embedded UTF-8 display text |
| `GADGETS.PAS` | `SupportUnit` | bounded demo gadget state |
| `TVEDIT.PAS` | `EntryPoint` | `Tp7Edit` |
| `TVHC.PAS` | `EntryPoint` | `Tp7Help` compiler path |
| `HELPFILE.PAS` | `SupportUnit` | existing bounded Help model |
| `DEMOHELP.PAS` | `FixtureOrContent` | controlled Help topics |
| `TVRDEMO.PAS` | `EntryPoint` | `Tp7ResourceDemo` |
| `GENRDEMO.PAS` | `GeneratorIntent` | `Tp7ResourceGenerator` |
| `ASCIITAB.PAS` | `EntryPoint` | `Tp7AsciiTable` |
| `CALC.PAS` | `EntryPoint` | `Tp7Calculator` |
| `CALENDAR.PAS` | `EntryPoint` | `Tp7Calendar` |
| `PUZZLE.PAS` | `EntryPoint` | `Tp7Puzzle` |
| `MOUSEDLG.PAS` | `EntryPoint` | `Tp7MouseDialog` |

No source needs `IntentionalOmission`; proprietary binary or host-mutating
details are intentional deviations inside the relevant modern consumer, not
omission of the source's user purpose.

## Implementation Phases

### Phase A - Foundation and reference slice

1. Create evidence, gate requirements and the shared Wave-5 project.
2. Add the ten executable project skeletons and solution/test references.
3. Add the failing/missing `Tp7Calculator` real-path tests.
4. Implement calculator state, app shell, visible result and guide.
5. Complete the first source, consumer and showcase-delta rows.

### Phase B - Central application, editor and help

1. Add `Tp7Demo` command/window/gadget paths with exactly-once dispatch.
2. Add `Tp7Edit` modified, safe-close and controlled-save paths.
3. Add `Tp7Help` valid compile, invalid compile, context and fallback paths.
4. Add independent app-loop smokes and guides for all three.

### Phase C - Resources and deterministic domain examples

1. Add allowlisted generator and exact-key resource reconstruction.
2. Add ASCII navigation and selected decimal/hex state.
3. Add fixed-date calendar navigation across year boundaries.
4. Add fixed-board puzzle moves and invalid-move rejection.
5. Add bounded demo gadget and negative proof.

### Phase D - Mouse, matrices and documentation

1. Add local mouse settings/capability state, supported input and full keyboard
   fallback without host mutation.
2. Complete ten primary proof rows, 15 source rows, six consumer decisions and
   ten showcase-delta rows.
3. Create ten guides, update examples README and DocFX navigation.
4. Derive `Lastenheft_18_Wave5-TP7-Showcase-Remediation.md` only from the
   delivered delta; do not create Feature 033.

### Phase E - Validation and delivery

1. Run static, format, targeted, full, coverage, DocFX/A11Y, text-first,
   secret, supply-chain, parity, protected-path and platform gates.
2. Archive Lastenheft 17 and synchronize status/statistics/agent surfaces.
3. Align version, stage exact candidate, commit, push, create PR and converge
   review plus exact-head gates.
4. Merge and synchronize local `main`; create one non-recursive evidence-only
   closeout only when post-merge facts require it.

## Validation Strategy

| Gate | Planned proof |
|---|---|
| Static candidate | `git diff --check`, staged candidate inventory, no placeholders, protected/generated path scan |
| State | Installed Bash validator locally; matching PowerShell validator in Windows acceptance path |
| Targeted examples | Release test filtered to all `Tp7*` smoke and Wave-5 matrix tests |
| Full regression | One full `dotnet test TuiVision.sln --configuration Release` invocation |
| Coverage | Canonical Coverlet invocation with five required assemblies at or above 70% |
| Format | `dotnet format TuiVision.sln --verify-no-changes` |
| Documentation | `docfx docfx.json`, Playwright/Axe and text-first/UTF-8 review |
| Security/scope | secret scan, provider Gitleaks, supply-chain workflow, dependency/project diff and read-only-root scan |
| Agent parity | Local homogeneity plus remote Bash/PowerShell parity jobs |
| Platform | PR-context Ubuntu, macOS and Windows jobs executing the real Release/example proof |
| Exact head | Temporary provider evidence validated against committed gate requirements and the reviewed PR head |
| Review | GraphQL thread state, PR comments, reviewer outcomes and unavailable-review evidence |

Before every `dotnet build` or `dotnet test`, set all three version fields to
`1.32.<patch>.<build>` and increment the manual build counter exactly once.
Before commit or push, realign the three fields without an additional
increment unless another build or test was run.

## Project Structure

### Documentation and evidence

```text
specs/032-wave5-tp7-functional-porting/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
├── contracts/wave5-functional-acceptance.md
├── data-model.md
├── plan.md
├── pr-evidence.md
├── quickstart.md
├── research.md
├── retrospective.md
├── spec.md
└── tasks.md
```

### Source and tests

```text
examples/
├── Shared/TuiVision.Examples.Wave5/
│   ├── TuiVision.Examples.Wave5.csproj
│   ├── Wave5Application.cs
│   ├── Wave5Domain.cs
│   ├── Tp7DemoApp.cs
│   ├── Tp7EditApp.cs
│   ├── Tp7HelpApp.cs
│   ├── Tp7ResourceApps.cs
│   └── Tp7DomainApps.cs
├── Tp7Demo/
├── Tp7Edit/
├── Tp7Help/
├── Tp7ResourceDemo/
├── Tp7ResourceGenerator/
├── Tp7AsciiTable/
├── Tp7Calculator/
├── Tp7Calendar/
├── Tp7Puzzle/
└── Tp7MouseDialog/

tests/TuiVision.Examples.SmokeTests/
├── Tp7CalculatorSmokeTests.cs
├── Tp7ApplicationSmokeTests.cs
├── Tp7ResourceSmokeTests.cs
├── Tp7DomainSmokeTests.cs
└── Wave5FunctionalSmokeMatrixTests.cs

docs/guides/examples/tp7-*.md
```

**Structure Decision**: A single compiled example-support assembly avoids
linked-source identity problems and centralizes only Wave-5-specific
composition. Ten thin executable projects preserve independent launch paths.
No new framework project or package is introduced.

## Complexity Tracking

No Constitution violation requires a waiver.
