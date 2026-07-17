# Implementation Plan: Wave-6 TVFM Functional Porting

**Branch**: `035-wave6-tvfm-functional-porting` | **Date**: 2026-07-17 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/035-wave6-tvfm-functional-porting/spec.md`

## Summary

Feature 035 liefert die funktionale erste Wave-6-Stufe als eine moderne,
eigenständig startbare `Tp7FileManager`-Lernanwendung. Eine gemeinsame
Beispielassembly kapselt die kontrollierte Workspace-Fachlogik und die
TuiVision-App-Komposition. Sie verwendet bestehende Views, Commands,
StatusLine, Help/Description, Progress und Rendering-Verträge, ohne
allgemeine Dateisystem-APIs in das Framework zu verschieben.

Die Anwendung kopiert eine source-kontrollierte Fixture in einen
prozess-eigenen temporären Lernarbeitsbereich. Navigation, Liste, Filter,
Sortierung, Markierung, Text-/Hex-Vorschau, begrenzte Suche, interne
Zuordnung und explizit bestätigte Mutationen bleiben dort eingeschlossen.
Primäre Smokes laufen durch `app.Run()` und kombinieren Fachzustand,
View-Identität, Fokus, Status und gerenderte Zellen. Eine vollständige
24-Quellen-, zehn-Bereiche- und Stage-2-Matrix leitet spätere Arbeit nur aus
tatsächlichen Deltas ab.

## Technical Context

**Language/Version**: C# 14 / .NET 10
**Primary Dependencies**: Existing TuiVision.Core, TuiVision.Controls,
TuiVision.Serialization, TuiVision.Compatibility,
TuiVision.Drivers.Console, MSTest 4.0.1 and .NET BCL; no new package
**Storage**: Source-controlled UTF-8/binary fixtures copied to an
application- or test-owned temporary root; no database, service, host profile,
shell, process or network storage
**Testing**: MSTest Release unit and app-loop smokes, controlled PTY launch,
full solution tests, canonical Coverlet five-assembly gate, DocFX and
Playwright/Axe
**Target Platform**: macOS local development; GitHub-hosted Ubuntu, macOS and
Windows acceptance runners
**Project Type**: Multi-project C#/.NET terminal UI framework with one Wave-6
example executable and one shared example-support assembly
**Performance Goals**: Preview reads at most 4 KiB and 80 text lines; search
visits at most 256 files, depth 8, and returns at most 100 results; scripted
app loops terminate deterministically
**Constraints**: Explicit controlled root, no link/reparse traversal, no
silent overwrite, no permanent current-directory mutation, no arbitrary
user data, no external execution, no dependency, API break, broad framework
revision, Feature 036, or historical-source write
**Scale/Scope**: 24 source roles, ten functional areas, one managed
application, one primary proof matrix, one Stage-2 disposition, seven preset
records

## Constitution Check

*GATE: Passed before research; rechecked after design.*

| Gate | Decision and evidence |
|---|---|
| Level-2 environment | TuiVision remains the registered .NET 10, MSTest, DocFX/Axe Level-2 project |
| Memory-safe language | C# is on the MSL allow-list; Pascal and C/C++ remain read-only evidence |
| Secure generation | Canonical-root checks, closed enums, bounded reads/searches, explicit mutation decisions and fail-closed errors |
| Secure architecture | The controlled workspace is the trust boundary; UI composition cannot bypass it and external execution is absent |
| Security documentation | Feature-local `pr-evidence.md` contains proportional STRIDE/CIA/CAPEC and filesystem evidence; no product architecture document changes |
| NIST SSDF / CWE Top 25 | Applicable to path validation, symlink/reparse rejection, mutation authorization, input limits, evidence integrity and review |
| OWASP Proactive Controls / Cheat Sheets | Applicable as secure-input, path, error and least-privilege guidance |
| OWASP ASVS | `N/A`: no web, HTTP, authentication, session or service surface; re-evaluate if such a surface enters scope |
| SBOM / VEX / SLSA / OpenSSF | Existing supply-chain workflows remain gates; no package or new release component triggers a feature-owned artefact |
| AI-SBOM | `N/A`: AI is development tooling only; no model, dataset, AI service or AI runtime ships |
| STRIDE / CIA / CAPEC | Applicable to traversal, link escape, unauthorized mutation, stale intent, resource exhaustion and evidence tampering |
| S-ADR / arc42 security | `N/A`: no product architecture or security-concept change; re-evaluate for a reusable framework or trust-boundary change |
| Zero Trust / SAMM | `N/A`: no identity, network, deployment, service or maturity-program change |
| BSI C3A / BSI C5 | `N/A`: no cloud service, provider dependency, shared-responsibility model or cloud assurance scope |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: no new regulated role, runtime AI, financial ICT service or distributed product boundary |
| Presets | Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0, Cross-Platform 0.2.0, Agent Parity 0.3.0, Autonomous Run 0.2.2 |
| Cross-platform | Applicable to path comparison, separators, read-only attributes, links and terminal behavior; script parity is `N/A` because no script is planned |
| Security-first | Credentials, agent state, logs, caches, `_site/`, generated API YAML, TestResults and external checkouts stay untracked |
| Inclusion/A11Y | Keyboard parity, text-first state, real focus/status, F1 Description, constrained terminal and WCAG 2.2 AA guide path |
| Bilingual delivery | Learner guidance and explanatory blocks are DE-first/EN-second at CEFR-B2 |
| Statistics | `docs/project-statistics.md` is updated after the final candidate using existing 80/125 lines-per-workday references |
| Agent parity | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` and `.github/agents/copilot-instructions.md` are reviewed and synchronized together |

**Post-design recheck**: Passed. The Wave-6 assembly is example composition,
not a substitute framework. All actual host filesystem access is mediated by
one controlled-root boundary. No Constitution exception is required.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction to start and autonomously
deliver Feature 035
**Evidence path**: `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`
**Representative vertical slice**: Add a missing/failing controlled-workspace
contract and real app-loop smoke for root listing, subdirectory navigation and
bounded text preview; implement the smallest workspace, application window,
status, Description, cells, guide and first evidence rows before mutation
paths
**Convergence gates**: Clarify has no material question; all requirements and
domain checklists pass; Plan review has no actionable finding; Analyze has no
Critical/High and no unowned Medium; every task is complete; all local,
remote, review and exact-head gates pass
**Shared single-writer files**: `pr-evidence.md`, `tasks.md`,
`autonomous-run-state.json`, `autonomous-gate-requirements.json`,
`TuiVision.sln`, smoke-test project, `examples/README.md`,
`Directory.Build.props`, DocFX navigation, five agent surfaces,
Pflichtenheft, processing order, statistics and archived intake
**Validation triggers**: Static and scope checks always; targeted Wave-6
smokes; full Release and coverage because an executable, file operations and
shared proof code are added; DocFX/A11Y because a learner guide and navigation
change; platform gates because filesystem and runnable TUI behavior change;
script parity `N/A`
**Scope firewall**: Any broad, API-breaking, closure-contradicting or shared
runtime defect becomes `FollowUpHardening` and stops its slice; any unsafe
filesystem boundary or `ProductDecision` stops the run
**Remote closeout**: Commit and push the exact candidate, create a non-empty
feature PR, validate exact-head provider evidence, converge checks and review
threads, use the authorized Human-Approval-only bypass only if all technical
gates are green, merge, synchronize `main`, and use one evidence-only causal
closeout only for facts that cannot truthfully exist before merge

## Architecture and Evidence Strategy

### Controlled workspace boundary

`ControlledFileWorkspace` owns one canonical root. It resolves only relative
paths, verifies every existing path segment, rejects links/reparse points, and
rechecks source and target immediately before a mutation. UI code receives
relative records and operation results; it never combines arbitrary host
paths itself.

The default executable materializes source-controlled fixture content into a
new process-owned temporary directory. The path is visible to the user and
deleted on normal disposal. Tests own their own roots. This permits real
file-operation proof without exposing personal data or mutating repository
fixtures.

### Bounded read and search contracts

- Directory entries are ordinally stable after platform-aware path
  validation.
- Wildcard matching uses the BCL simple-expression matcher over entry names.
- Text and hex previews read at most 4 KiB; text publishes at most 80 lines.
- Search visits at most 256 files to depth 8 and returns at most 100 relative
  paths. Cancellation is checked before each directory and file.
- Internal associations select text, hex or fallback only.

### Mutation state machine

Every mutation follows:

`Requested -> Validated -> AwaitingDecision -> Confirmed -> Executing -> Completed`

Cancel transitions from `AwaitingDecision` to `Canceled` without a write.
Conflict or validation failure transitions to `Rejected`. Execution failure
transitions to `Failed` with an explicit recovery boundary. A stale intent is
revalidated before execution; silent overwrite is never allowed.

### TuiVision composition

`Tp7FileManagerApp` provides:

- one main window with tree/list/preview text-first regions;
- a real `TStatusLine`;
- closed commands for navigate, filter/sort/tag, text/hex/associated preview,
  search, operation preparation, confirm/cancel, keyboard drop intent,
  palette and Description;
- deterministic queued events for `--smoke`;
- visible proof properties for selected path, operation state, view identity,
  status and stable cell region.

The functional first stage may use a compact combined window rather than the
full historical multiwindow layout. That distinction is measured in the
Stage-2 disposition.

### Historical source roles

| Source group | Count | Primary role family | Modern target |
|---|---:|---|---|
| `TVFM.PAS`, `GLOBALS.PAS`, `EQU.PAS`, `TOOLS.PAS` | 4 | Entry point and application support | app shell, commands, state and formatting |
| `DIRVIEW.PAS`, `TREEWIN.PAS`, `FILEVIEW.PAS`, `INFOVIEW.PAS` | 4 | View or interaction | navigation, list, metadata and focus |
| `VIEWTEXT.PAS`, `VIEWHEX.PAS`, `FILEFIND.PAS` | 3 | View or interaction | bounded previews and search |
| `FILECOPY.PAS`, `DRAGDROP.PAS`, `TRASH.PAS` | 3 | File operation | explicit operation intents and keyboard fallback |
| `ASSOC.PAS` | 1 | View or interaction | internal association decision |
| `COLORS.PAS`, `EDITPAL.PAS`, `GAUGES.PAS` | 3 | View or interaction | palette, progress and recovery state |
| `MAKERES.PAS`, `TVFM.TVR`, `DEFAULT.PAL`, `CYAN.PAL`, `ROSE.PAL` | 5 | Resource or palette | closed managed resources and palette choices |
| `MAKETVFM.BAT` | 1 | Build intent | documented historical generation boundary |

The detailed matrix records exactly one row per path. No historical bytes are
copied into executable code.

## Implementation Phases

### Phase A - Evidence and reference slice

1. Create run state, gate requirements, PR evidence, exact 24-source matrix,
   ten-area framework matrix and one-row Stage-2 matrix.
2. Add the Wave-6 shared project, executable skeleton, solution and test
   references.
3. Add missing/failing contracts for root binding, navigation and bounded
   text preview.
4. Implement the smallest controlled workspace and real app-loop shell.
5. Complete the first source, functional-area and primary proof rows.

### Phase B - Read-only file-manager functions

1. Add stable directory snapshots, filter, sort, tag and metadata state.
2. Add bounded text and hex previews with invalid/truncated indicators.
3. Add bounded cancellable search and internal association fallback.
4. Add app-loop commands, focus/status/cell proof and negative tests.

### Phase C - Explicit mutations and recovery

1. Add operation-intent validation and stale-state revalidation.
2. Add cancel-first copy, rename, delete and read-only attribute paths.
3. Add target-conflict, traversal, link, missing source, cancellation,
   execution failure and recovery proof.
4. Add drag/drop intent with complete keyboard command parity and no
   pointer-exclusive action.

### Phase D - Resources, documentation and matrices

1. Add closed palette/config/resource choices and visible fallback.
2. Complete 24 source rows, ten framework decisions, primary proof rows and
   the one Stage-2 disposition.
3. Add the bilingual guide, README and DocFX navigation.
4. Review didactic comments, agent contexts, Pflichtenheft, processing order
   and statistics.
5. Archive Lastenheft 20 without creating Feature 036.

### Phase E - Validation and delivery

1. Run static, format, targeted, controlled PTY, full, coverage, DocFX/A11Y,
   text-first, secret, supply-chain, parity, protected-path and platform gates.
2. Validate exact source cardinalities, protected historical hashes and
   absence of arbitrary-root access.
3. Align version, stage exact candidate, commit, push, create PR and converge
   review plus exact-head gates.
4. Merge and synchronize local `main`; create a non-recursive evidence-only
   closeout only if post-merge facts require it.

## Validation Strategy

| Gate | Planned proof |
|---|---|
| Static candidate | `git diff --check`, no placeholders, generated/protected path and dependency/project diff scans |
| Historical inventory | Exact 24 rows and unchanged hashes for `TVFM/`; read-only scans for `TVDEMOS/` and `tv203s/` |
| Targeted workspace | Release tests for root, link, navigation, preview, search, mutation and recovery contracts |
| Targeted app | Release tests for real `app.Run()`, command, focus, status, Description and cell evidence |
| Normal / smoke | Controlled PTY normal start plus `dotnet run --project examples/Tp7FileManager -- --smoke` |
| Full regression | One full `dotnet test TuiVision.sln --configuration Release` invocation |
| Coverage | Canonical Coverlet invocation with five required assemblies at or above 70 % |
| Format | `dotnet format TuiVision.sln --verify-no-changes` |
| Documentation | `docfx docfx.json`, Playwright/Axe and UTF-8/text-first review |
| Security/scope | Secret scan, provider Gitleaks, supply chain, path ownership, no external execution and no historical write |
| Agent parity | Local homogeneity plus remote Bash/PowerShell parity jobs |
| Platform | PR-context Ubuntu, macOS and Windows jobs exercising controlled filesystem and app proof |
| Exact head | Temporary provider evidence validated against committed gate requirements and reviewed PR head |
| Review | GraphQL thread state, PR comments, reviewer outcomes and unavailable-review evidence |

Before every `dotnet build` or `dotnet test`, set all three version fields to
`1.35.<patch>.<build>` and increment the manual build counter exactly once.
Before commit or push, realign the fields without another increment unless a
later build or test ran.

## Project Structure

### Documentation and evidence

```text
specs/035-wave6-tvfm-functional-porting/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
├── contracts/wave6-functional-acceptance.md
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
├── Shared/TuiVision.Examples.Wave6/
│   ├── TuiVision.Examples.Wave6.csproj
│   ├── ControlledFileWorkspace.cs
│   ├── Wave6FileModels.cs
│   └── Tp7FileManagerApp.cs
└── Tp7FileManager/
    ├── Program.cs
    ├── Tp7FileManager.csproj
    └── Fixtures/

tests/TuiVision.Examples.SmokeTests/
├── Wave6ControlledWorkspaceTests.cs
├── Wave6FileOperationTests.cs
└── Wave6FunctionalSmokeMatrixTests.cs

docs/guides/tp7-file-manager.md
```

**Structure Decision**: One compiled Wave-6 example assembly prevents
duplicate linked-source type identities while keeping domain composition out
of product framework assemblies. One thin executable preserves an independent
normal launch path. Existing smoke tests host all test-only proof.

## Complexity Tracking

No Constitution violation or complexity exception is required.
