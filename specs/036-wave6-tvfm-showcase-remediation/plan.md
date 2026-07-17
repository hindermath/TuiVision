# Implementation Plan: Wave-6 TVFM Showcase Remediation

**Branch**: `036-wave6-tvfm-showcase-remediation` | **Date**: 2026-07-17 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/036-wave6-tvfm-showcase-remediation/spec.md`

## Summary

Feature 036 erweitert ausschließlich die Präsentations- und
Interaktionsschicht des vorhandenen `Tp7FileManager`. Die bestehende
`ControlledFileWorkspace`-Fachlogik bleibt die einzige Autorität für
Navigation, Preview, Suche und Dateioperationen. `Tp7FileManagerApp`
komponiert vorhandene TuiVision-Menüs, fokussierbare Listen und Eingaben,
statische Preview-Flächen, Buttons, Dialoge, StatusLine und Description zu
einem sichtbaren Dateimanager-Showcase.

Der Referenz-Slice ersetzt zuerst die kompakte Textfenster-Darstellung durch
eine stabile Hauptkomposition für Navigation, Dateiliste, Auswahl, Status und
Description. Darauf folgen lesende Menüs, sichere Mutationsdialoge, ein
bestätigungspflichtiger Drag-Intent und die `48x16`-Darstellung. Neue
app-loop-basierte Smokes kombinieren Zustand, Fokus, View-Hierarchie,
StatusLine und gerenderte Zellen. Die Feature-Evidence enthält exakt eine
Einstiegspunktzeile und zehn `W6S`-Zeilen.

## Technical Context

**Language/Version**: C# 14 / .NET 10
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest 4.0.1 and .NET BCL; no new package
**Storage**: Existing source-controlled fixtures copied into process- or test-owned temporary controlled roots; no database, service, host profile, shell, process, PTY, network, or arbitrary-user storage
**Testing**: MSTest Release app-loop and filesystem smokes, controlled PTY launch, full solution tests, canonical Coverlet five-assembly gate, DocFX, Playwright/Axe, local and provider validation
**Target Platform**: macOS local development; GitHub-hosted Ubuntu, macOS,
and Windows acceptance runners
**Project Type**: Multi-project C#/.NET terminal UI framework with one
existing Wave-6 example executable and one existing shared example assembly
**Performance Goals**: Preserve Feature-035 preview/search bounds; scripted
dialogs and app loops terminate deterministically; normal and `48x16`
layouts expose the required primary state without unbounded work
**Constraints**: No domain re-port, no wider root/path/search/preview/viewer/
intent/mutation authority, no direct mouse mutation, no arbitrary user data,
no dependency, project, public API, broad framework revision, historical
write, Feature 037, closure, or portfolio-audit start
**Scale/Scope**: One existing entry point, ten showcase areas, four mutation
dialogs, one keyboard-equivalent drag intent, one guide, one exact evidence
matrix, seven preset records

## Constitution Check

*GATE: Passed before research; rechecked after design.*

| Gate | Decision and evidence |
|---|---|
| Level-2 environment | TuiVision remains the registered .NET 10, MSTest, DocFX/Axe Level-2 project |
| Memory-safe language | C# is on the MSL allow-list; Pascal and C/C++ remain read-only evidence |
| Secure generation | Closed commands and enums, bounded inputs, explicit dialog decisions, fail-closed validation, no internal-error disclosure |
| Secure architecture | `ControlledFileWorkspace` remains the trust boundary; UI can only request existing typed operations and cannot construct external authority |
| Security documentation | Feature-local `pr-evidence.md` records proportional STRIDE/CIA/CAPEC and dialog/filesystem evidence; no product security document change is triggered |
| NIST SSDF / CWE Top 25 | Applicable to path-safe UI, intent authorization, input limits, error states, evidence integrity, review, and delivery |
| OWASP Proactive Controls / Cheat Sheets | Applicable as secure-input, path, error, least-privilege, and explicit-confirmation guidance |
| OWASP ASVS | `N/A`: no web, HTTP, authentication, session, or service surface; re-evaluate on such a scope change |
| SBOM / VEX / SLSA / OpenSSF | Existing supply-chain workflows remain gates; no dependency or release component triggers a feature-owned artefact |
| AI-SBOM | `N/A`: AI is development tooling only; no model, dataset, AI service, or AI runtime ships |
| STRIDE / CIA / CAPEC | Applicable to traversal, link escape, unauthorized mutation, stale intent, UI-hidden confirmation, exhaustion, and evidence tampering |
| S-ADR / new arc42 security | `N/A`: no public framework architecture or trust-boundary change; re-evaluate if a reusable framework fix becomes necessary |
| Zero Trust / SAMM | `N/A`: no identity, network, deployment, service, or maturity-program change |
| BSI C3A / BSI C5 | `N/A`: no cloud service, provider dependency, shared responsibility, or cloud assurance scope |
| NIS2 / CRA / EU AI Act / DORA | `N/A`: no new regulated role, runtime AI, financial ICT service, or distributed product boundary |
| Presets | Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0, Cross-Platform 0.2.0, Agent Parity 0.3.0, Autonomous Run 0.2.2 |
| Cross-platform | Applicable to paths, attributes, console input, mouse fallback, focus, and terminal layout; script parity is `N/A` unless scripts change |
| Security-first | Credentials, agent state, logs, caches, `_site/`, generated API YAML, TestResults, and temporary workspaces stay untracked |
| Inclusion/A11Y | Keyboard-complete controls, stable focus, text-first state, F1 Description, High Contrast, constrained layout, and WCAG 2.2 AA guide path |
| Bilingual delivery | Learner guidance, Description, and non-trivial didactic comments are DE-first/EN-second at CEFR-B2 |
| Statistics | `docs/project-statistics.md` is updated after the final candidate using the existing 80/125 lines-per-workday references |
| Agent parity | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` are reviewed and synchronized together; the generated Antigravity integration `.agent/rules/specify-rules.md` is refreshed from the same plan |

**Post-design recheck**: Passed. The design changes only an example
presentation layer and its proof. `ControlledFileWorkspace` and the
Feature-035 models remain the only filesystem contract. No Constitution
exception is required.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: Current user instruction to autonomously deliver
Feature 036 after merging the intake PR
**Evidence path**: `specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md`
**Representative vertical slice**: Add failing app-loop tests for a persistent
navigation/list composition with concrete focused control, real StatusLine,
F1 Description, and normal plus `48x16` cell proof; implement that slice and
complete `W6S-001`, `W6S-009`, and the navigation/layout portion of
`W6S-010` before expanding commands or dialogs
**Convergence gates**: Repeated Clarify has no material question; six
requirements checklists and two plan checklists pass; Analyze has no
Critical/High and no unowned Medium; every task is complete; all local,
provider, review, and exact-head gates pass
**Shared single-writer files**: `pr-evidence.md`, `tasks.md`,
`autonomous-run-state.json`, `autonomous-gate-requirements.json`,
`Tp7FileManagerApp.cs`, showcase test matrix, guide, README/DocFX navigation,
`Directory.Build.props`, five maintained agent surfaces, generated Antigravity
context, Pflichtenheft, processing order, statistics, and archived intake
**Validation triggers**: Static and scope checks always; targeted
`Wave6Showcase` and preserved `Wave6` safety smokes; full Release and coverage
because shared executable UI/proof changes; DocFX/A11Y because guide and
navigation change; platform gates because terminal input/layout and
filesystem-facing dialogs change; script parity only if a script changes
**Scope firewall**: Any unsafe authority, broad/API-breaking framework change,
unowned reusable gap, or `ProductDecision` stops the run; bounded residual
work becomes an owned `FollowUpHardening` without implementation here
**Remote closeout**: Commit and push the exact candidate, create a non-empty
feature PR, validate exact-head provider evidence, converge checks and review
threads, use the authorized Human-Approval-only bypass only after every
technical gate is green, merge, synchronize `main`, and use one non-recursive
evidence-only closeout only for post-merge facts

Evidence, run state, and gate requirements already exist before product edits.
One explicit build-counter increment is reserved for each individual
`dotnet build` or `dotnet test` invocation.

## Architecture and Evidence Strategy

### Existing functional boundary

`ControlledFileWorkspace`, `Wave6DirectorySnapshot`, `Wave6PreviewResult`,
`Wave6SearchResult`, `Wave6OperationIntent`, and `Wave6OperationResult` are
reused unchanged unless a test proves a narrowly scoped showcase blocker.
The UI supplies only root-relative values to the workspace and renders its
typed results. It never resolves host paths, launches external commands, or
performs a mutation itself.

### Persistent showcase composition

`Tp7FileManagerApp` owns one persistent main `TWindow` containing:

- a focusable `TListBox` for the current controlled snapshot;
- text-first path, metadata, mode, and safety labels;
- a bounded preview/result `TStaticText` region;
- focused controls or menu commands for the accepted Feature-035 operations;
- a real `Wave6StatusLine` whose message reflects focus, selection, command
  availability, result, and keyboard hints.

The composition is recalculated from the current typed state after each
accepted command. Normal layout shows list and detail regions together.
`48x16` keeps the list, selected path, next action, StatusLine, Description,
and quit path, while secondary detail is summarized rather than overlapped.

### Closed command and menu map

Menus remain a closed modern interpretation rather than a Pascal copy:

| Group | Visible paths |
|---|---|
| File | Copy, Rename, Delete, Set/Clear Read-Only, Quit |
| Navigate | Enter first/selected directory, root/refresh |
| View | Text, Hex, Associated viewer, filter, sort, tag |
| Search | Search text, continue/display result, cancel |
| Options | Closed palette/resource choice |
| Help | Description |

Each command has one typed ID, visible text, keyboard path, deterministic
availability rule, and StatusLine explanation. Existing command IDs are
retained where possible; new IDs remain example-local.

### Safe operation dialogs

The application uses existing `TDialog`, `TInputLine`, `TStaticText`, and
`TButton` controls. A feature-local dialog-state model records operation,
source, target/name, normalized preview, validation message, focused control,
and decision. The flow is:

```text
Select -> Open dialog -> Validate input -> Prepare existing intent
       -> Preview -> Confirm or Cancel -> Revalidate in workspace
       -> Execute or reject -> Render terminal result
```

Delete and read-only dialogs do not fabricate target input. Copy and rename
accept a bounded relative target/name only. Enter activates the default
decision, Escape cancels, Tab/Shift+Tab follow the inserted control order.
The application records proof properties but does not bypass modal/event
dispatch with a direct execution helper.

### Drag intent boundary

Mouse down, move, and release may identify a selected fixture entry and a
valid visible target region. Release creates the same prepared operation
request as the keyboard command and opens the same confirmation path. Mouse
events never call `ControlledFileWorkspace.Execute`. Invalid target, Escape,
capability loss, removed view, or shutdown clears the prepared drag state and
reports `NoMutation`.

### Help, A11Y, and teaching value

Description explains purpose, menu/keyboard workflow, controlled root,
confirmation/revalidation, optional mouse path, platform limitations,
historical intent, modern deviation, and proof boundary. Focus and status are
visible in text. High Contrast uses the existing closed palette and no
essential meaning relies on color. New non-trivial composition, dialog, or
mouse-state logic receives concise reason-focused bilingual comments only
where it improves learning or maintenance.

### Evidence and validator

The feature adds a deterministic test-only parser for:

- exactly `W6S-001` through `W6S-010`, once each;
- exactly one `Tp7FileManager` entry row;
- exact decision vocabularies;
- complete visible access, layout, focus/status/Description/keyboard, proof,
  boundary, risk, and re-evaluation cells;
- rejection of `Planned`, `Open`, missing, duplicate, unknown, incomplete, or
  internally inconsistent accepted rows.

The 24 Feature-035 source hashes are rechecked, not duplicated as a new
normative inventory.

## Implementation Phases

### Phase A - Evidence and vertical reference slice

1. Refresh accepted artifact hashes and finalize plan/checklist evidence.
2. Add failing `Wave6Showcase` app-loop tests for persistent list/focus,
   StatusLine, Description, normal layout, and `48x16`.
3. Implement the smallest persistent main composition using existing controls.
4. Complete initial evidence for `W6S-001`, `W6S-009`, and relevant
   `W6S-010` proof.

### Phase B - Visible read-only command surfaces

1. Complete File/Navigate/View/Search/Options/Help menu groups and keyboard
   paths.
2. Render filter, sort, tag, text/hex preview, association, search result,
   cancellation/limit, palette, and resource state.
3. Prove focus, status, command availability, normal/constrained cells, and
   unsupported fallbacks through `app.Run()`.
4. Complete `W6S-002` through `W6S-005` and `W6S-008`.

### Phase C - Safe mutation dialogs

1. Add failing tests for copy, rename, delete, and read-only dialog flows.
2. Compose operation-specific dialogs from existing controls.
3. Prove input validation, stable focus order, Enter/Escape, preview,
   Confirm/Cancel, stale revalidation, terminal result, and recovery.
4. Re-run all Feature-035 filesystem safety tests unchanged.
5. Complete `W6S-006`.

### Phase D - Optional mouse intent and layout closure

1. Add failing tests for keyboard/mouse intent parity and every abort boundary.
2. Implement only bounded drag-state preparation through existing event flow.
3. Prove no direct mutation and full keyboard fallback.
4. Close normal and `48x16` layout, focus, StatusLine, and cell matrices.
5. Complete `W6S-007` and remaining `W6S-010` evidence.

### Phase E - Documentation, matrices, and repository integration

1. Complete ten framework decisions and one entry-point decision.
2. Update the bilingual guide, shortcut inventory, README, and DocFX
   navigation.
3. Review didactic comments, five maintained agent contexts, the generated
   Antigravity context, Pflichtenheft, processing order, statistics, and
   later-intake markers.
4. Archive Lastenheft 21 without creating Feature 037 or a later audit.

### Phase F - Validation and delivery

1. Run static, format, targeted showcase/safety, controlled PTY, `--smoke`,
   full Release, coverage, DocFX/A11Y, text-first, secret, supply-chain,
   parity, protected-path, state, and platform gates.
2. Validate exact 1/10 cardinality, decision consistency, protected historical
   hashes, and absence of authority/dependency/project expansion.
3. Align version, stage the exact candidate, run cached-diff checks, commit,
   push, create the feature PR, and converge review plus exact-head gates.
4. Merge and synchronize local `main`; create a single non-recursive
   evidence-only closeout only if required for post-merge truth.
5. Record the retrospective and do not start the next feature.

## Validation Strategy

| Gate | Planned proof |
|---|---|
| Static candidate | `git diff --check`, `git diff --cached --check`, exact path inventory, no placeholders/generated/protected/Feature-037 paths |
| Historical baseline | Unchanged `TVFM/`, `TVDEMOS/`, `tv203s/`; all 24 accepted Feature-035 hashes still match |
| Reference slice | Release `Wave6Showcase` tests for list, focus, status, Description, normal and `48x16` cells |
| Read surfaces | Real app-loop command/menu proof for preview, filter, sort, tag, search, viewer, palette, and resources |
| Dialog safety | Real dialog/event proof for copy, rename, delete, read-only, validation, Confirm, Cancel, stale intent, result, and recovery |
| Mouse parity | Mouse only prepares keyboard-equivalent intent; invalid/released/capability/view/shutdown paths remain non-mutating |
| Preserved filesystem | Existing Release `Wave6` workspace and operation tests stay green |
| Normal / smoke | Controlled PTY first frame, primary action, F1, Ctrl+Q plus deterministic `--smoke` |
| Full regression | One full `dotnet test TuiVision.sln --configuration Release` invocation |
| Coverage | Canonical Coverlet invocation with five required assemblies at or above 70 % |
| Format | `dotnet format TuiVision.sln --verify-no-changes` |
| Documentation | `docfx docfx.json`, Playwright/Axe, UTF-8, semantic/text-first guide review |
| Security/scope | Secret scan, Gitleaks, supply chain, path ownership, no external execution, no historical write, no dependency/project expansion |
| Agent parity | Five maintained guidance surfaces, generated Antigravity context, and available local and provider parity checks |
| Platform | PR-context Ubuntu, macOS, and Windows jobs executing actual Wave-6 tests |
| Exact head | Temporary provider evidence validated against committed gate requirements and reviewed PR head |
| Review | GraphQL thread state, PR comments, reviewer outcomes, missing-review classification, narrow bypass boundary |

Before every individual `dotnet build` or `dotnet test`, set all three
version fields to `1.36.<patch>.<build>` and increment the manual build
counter exactly once. Before commit or push, realign the fields without
another increment unless another build or test ran.

## Project Structure

### Documentation and evidence

```text
specs/036-wave6-tvfm-showcase-remediation/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
├── contracts/wave6-showcase-acceptance.md
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
examples/Shared/TuiVision.Examples.Wave6/
├── ControlledFileWorkspace.cs       # Existing functional authority
├── Wave6FileModels.cs               # Existing functional models
├── Tp7FileManagerApp.cs             # Showcase shell and command coordination
└── Wave6ShowcaseViews.cs            # New bounded example-local view/dialog composition

examples/Tp7FileManager/
├── Program.cs
└── Fixtures/

tests/TuiVision.Examples.SmokeTests/
├── Wave6ControlledWorkspaceTests.cs # Preserved safety regression
├── Wave6FileOperationTests.cs       # Preserved mutation regression
├── Wave6FunctionalSmokeMatrixTests.cs
└── Wave6ShowcaseSmokeMatrixTests.cs # New real UI and evidence proof

docs/guides/examples/tp7-file-manager.md
```

**Structure Decision**: The existing Wave-6 example assembly remains the
only shared application-composition boundary. One new source file isolates
view/dialog composition from the existing functional workspace without
creating a framework, project, dependency, or second entry point.

## Complexity Tracking

No Constitution violation or complexity exception is required.
