# Implementation Plan: Core Runtime Conformance Hardening

**Branch**: `025-core-runtime-conformance-hardening` | **Date**: 2026-07-13 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_10_Core-Runtime-Conformance-Hardening.md` and Feature-024 Audit Revision 2

## Summary

Feature 025 schließt die neun akzeptierten Core-Findings `F001` bis `F009`
durch kleine, additive Frameworkverträge. Der Lauf härtet Event-Erzeugung,
Fokus-Veto, zustandsspezifische View-Hierarchie, Pending-/Idle-Lifecycle,
Desktop-Stack, Close/Modalität, gemeinsame Command-Verfügbarkeit, realen
Keyboard-Ingress und eine begrenzte Drag-Session. Jeder Slice beginnt mit einem
Red-Proof und endet mit einem Real-Path-Proof über echte Schleife, Zustand,
View-Tree, Fokus und bei sichtbaren Flows Buffer-/Cell-Evidence.

*Feature 025 closes the nine accepted core findings through small additive
framework contracts. Every slice starts with a red proof and ends with real-path
evidence; no Wave application or historical source is modified.*

## Technical Context

**Language/Version**: C# / .NET 10
**Primary Dependencies**: existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, MSTest; no new package
**Storage**: in-process state only; audit JSON and Markdown evidence are repository-owned documentation
**Testing**: MSTest, in-process application loops, fake/controlled console ingress, Coverlet, DocFX, Playwright/Axe, Lynx/text review
**Target Platform**: managed macOS/Linux runtime plus Windows/WSL compatibility and CI evidence
**Project Type**: reusable terminal UI framework with examples and generated API documentation
**Performance Goals**: one bounded idle invocation per empty poll; no busy loop, unbounded pending queue, background thread, or repeated dispatch
**Constraints**: additive public API only, no new external runtime dependency, `tv203s/` read-only, pinned Free Vision external/untracked, keyboard-complete interaction
**Scale/Scope**: nine findings across four implementation modules, their existing tests, Feature-024 resolution evidence, guides, statistics, and five agent surfaces

## Constitution Check

*GATE: PASS before research; rechecked after design with no exception.*

| Gate | Decision and Evidence Boundary |
|---|---|
| Level-2 environment | PASS: TuiVision's .NET 10, Release-test, coverage, DocFX/A11Y, statistics, agent and branch-version baselines are binding. |
| Memory-safe language | PASS: C#/.NET is on the Constitution MSL allow-list; C/C++ and Pascal remain read-only evidence. |
| Secure code generation | PASS: malformed event kinds, invalid states, unknown input and lifecycle loss fail closed; no secrets, SQL, crypto or external data flow enters scope. |
| Secure architecture | PASS: state ownership, one-event channels, one pending slot, one modal child per owner, command truth and drag capture are explicit boundaries. |
| Security documentation | PASS: feature-local governance evidence is the proportional equivalent; shared `docs/security/` files change only if a trigger actually changes. |
| Security standards | PASS: NIST SSDF and CWE Top 25 apply. STRIDE/CIA/CAPEC apply to event/state/input/lifecycle boundaries. ASVS, supply-chain, AI, cloud and regulatory controls remain trigger-based `N/A`. |
| Public API/XML | PASS: additive contracts receive complete DE-first/EN-second XML comments and therefore trigger DocFX plus web A11Y validation. |
| Tests and coverage | PASS: red-first targeted tests, full Release, and at least 70 percent line coverage in all five canonical assemblies are mandatory. |
| Historical source | PASS: matching `.cc` and headers are reviewed read-only; pinned Free Vision commit and checksums are verified externally. |
| Inclusion/A11Y | PASS: every drag/mouse flow has keyboard parity; focus and visible state remain text-first and announcement-compatible. |
| Cross-platform | PASS: touched keyboard/modifier/terminal behavior requires macOS/Linux and Windows/WSL evidence; no script edit is planned. |
| Agent parity | PASS: five maintained surfaces are synchronized because active feature context changes; templates remain `N/A`. |
| Statistics | PASS: `docs/project-statistics.md` receives the completed Feature-025 delta and retains the final `Gesamtstatistik`. |
| Versioning | PASS: numbered branch version is `1.25.<patch>.<build>`; only the manual build counter increments before each `dotnet build` or `dotnet test`. |

### Preset Applicability Matrix

| Preset | Version | Applicability |
|---|---:|---|
| `security-governance` | 0.6.0 | `Applicable` for SSDF, CWE, fail-closed input/state and evidence integrity; trigger-based `N/A` for ASVS, supply chain, AI and regulation |
| `architecture-governance` | 0.5.0 | `Applicable` for STRIDE/CIA/CAPEC and bounded responsibility design; S-ADR/arc42 only if the final API graph becomes materially broader; cloud/Zero-Trust/C3A/C5 remain `N/A` |
| `isaqb-architecture-governance` | 0.2.0 | `Applicable` for quality scenarios, risks, responsibility allocation and deliberate modernization |
| `a11y-governance` | 0.4.0 | `Applicable` for keyboard parity, focus, text-first evidence, XML/docs and didactic comments |
| `cross-platform-governance` | 0.2.0 | `Applicable` for keyboard/terminal proof; script parity `N/A` unless a script changes |
| `agent-parity-governance` | 0.3.0 | `Applicable` for all five active-context surfaces; repository templates `N/A` |
| `autonomous-run-governance` | 0.1.0 | `Applicable` for evidence-first convergence and `MergeAndSync`; validator triggers are recorded before affected edits |

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: current user instruction to complete PR #68 and execute Feature 025 autonomously
**Evidence path**: `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
**Representative vertical slice**: `F001` strict event kind, with red Core test, minimal factory guard, targeted proof, Finding row, historical intent and Free Vision relation
**Convergence gates**: no material Clarify question; every checklist passed/dispositioned; no Analyze Critical/High and every Medium dispositioned; all tasks complete or conditionally evidenced; required checks green and zero actionable review threads
**Shared single-writer files**: `pr-evidence.md`, Feature-024 audit evidence, `Directory.Build.props`, `Pflichtenheft.md`, five agent files, `docs/project-statistics.md`, Lastenheft archive
**Validation triggers**: diff/format/secret/generated-output always; targeted tests per slice; full Release/coverage for shared runtime; DocFX/Axe/Lynx for XML/API/guide changes; no script validator unless script scope appears
**Scope firewall**: newly discovered F010-F013, application logic, breaking semantics, broad redesign or full drag/drop become `FollowUpHardening` or blocking `ProductDecision`
**Remote closeout**: commit and push aligned version, ready PR, required PR-context checks, Copilot/Claude/thread review convergence, merge commit, branch deletion, fetch/prune, clean local `main == origin/main`; one evidence-only closeout PR only if post-merge repository evidence is causally required

`pr-evidence.md` is created from the autonomous evidence template before the
first implementation or test edit. Every deterministic validator affected by a
planned edit is listed there before that edit.

## Architecture and Design

### A. Event and keyboard boundaries

- `TEvent.CreateMouse` accepts only `MouseDown`, `MouseUp`, `MouseMove`, or
  `MouseAuto`; flag categories and combinations are rejected before payload
  construction.
- `TProgram` routes actual `ConsoleKeyInfo` values through the existing
  `TConsoleInputAdapter`/`TKeyCodeTranslator` contract. A bounded existing-
  project reference may connect Controls to Compatibility; no external package
  or copied scan-code table is introduced.
- A protected real-ingress seam allows deterministic tests to supply
  `ConsoleKeyInfo` without weakening the production path.

### B. Focus and hierarchy

- `TView` exposes an additive, overridable focus-release decision.
- `TGroup.TrySetFocus` returns a typed `Accepted`, `Rejected`, or `NoOp`
  outcome; existing `SetFocus` delegates to it for source compatibility.
- The old current view is asked exactly once before mutation. Rejection leaves
  current/focus/data/announcement state unchanged.
- State propagation follows the historical responsibility matrix: `Active` and
  `Dragging` reach all direct children, `Focused` reaches only Current,
  `Exposed` reaches only visible children, and `Disabled` remains an owner
  dispatch boundary instead of overwriting each child's local state.

### C. Pending events and idle

- `TProgram` owns one pending event slot, matching the historical bounded
  contract. Publishing while occupied is rejected rather than overwriting.
- The loop drains the pending event before polling mouse/keyboard. A no-event
  result invokes `Idle` once and then a replaceable CPU-release wait.
- No background thread, timer service, unbounded queue or platform-specific
  message pump is introduced.

### D. Desktop, close, and modal lifecycle

- `TDesktop` gains coherent insertion/focus, top/next, tile, cascade, and
  close-all operations over visible eligible children.
- A small closeable-view contract lets `TWindow` and existing framed hosts
  report accepted/vetoed close without teaching the Desktop application types.
- `TWindow` completes `cmClose`, Ctrl+W and guarded Escape by removing itself
  when allowed; modal close returns a result rather than destroying unrelated
  ownership.
- A group-owned modal executor permits one active direct modal child, inserts
  unattached dialogs temporarily, isolates their loop, and restores the prior
  still-eligible focus in `finally` paths.

### E. Shared command context

- An immutable command snapshot is the single context result for active View,
  menu, StatusLine and keyboard dispatch.
- Opt-in active views provide command decisions; legacy
  `TProgram.IsCommandDisabled` remains a compatibility input.
- Menu/status manual disabled flags remain authoritative static constraints;
  a separate context overlay prevents accidental re-enabling.
- Refresh runs after accepted focus changes, after each handled event, and in
  idle; command dispatch re-evaluates the same source immediately before use.

### F. Generic bounded drag

- A source-owned drag session records source, payload, start/current cell,
  threshold, bounds, current target, state and final result.
- Exactly one session captures movement. An opt-in target accepts or rejects
  drop; owner or capability loss cancels and clears capture.
- Pointer movement and keyboard arrows update the same session. Enter commits;
  Escape restores/cancels. Existing title drag and Ctrl+F5 become consumers of
  this common contract without adding a full desktop protocol.

## Project Structure

```text
src/TuiVision.Core/
  TEvent.cs
src/TuiVision.Compatibility/
  Class1.cs
  TConsoleInputAdapter.cs
src/TuiVision.Controls/
  TView.cs
  TGroup.cs
  TProgram.cs
  TApplication.cs
  TDesktop.cs
  TDialog.cs
  TWindow.cs
  TMenuBar.cs
  TMenuItem.cs
  TStatusLine.cs
  TStatusItem.cs
  TEditor.cs
  TEditWindow.cs
  [small focus/command/close/drag contract files as justified by final design]
tests/TuiVision.Core.Tests/
tests/TuiVision.Compatibility.Tests/
tests/TuiVision.Controls.Tests/
tests/TuiVision.Drivers.Tests/
docs/guides/core-runtime-conformance-hardening.md
specs/024-tv203-freevision-conformance-audit/
specs/025-core-runtime-conformance-hardening/
```

**Structure Decision**: Existing projects and ownership boundaries are reused.
Small public value/interface files are allowed where they prevent unrelated
runtime classes from becoming dumping grounds. No new project, package,
example, service, persistence layer or generated source is added.

## Finding Slices and Dependencies

| Order | Finding | Slice | Dependency |
|---:|---|---|---|
| 1 | `F001` | strict concrete event construction | none; reference slice |
| 2 | `F008` | canonical real keyboard ingress and modifier repair | F001 event invariant |
| 3 | `F002` | typed focus transition and veto | event invariant |
| 4 | `F003` | state-specific hierarchy propagation | focus outcome |
| 5 | `F004` | pending event, idle and CPU release | event and ingress boundaries |
| 6 | `F007` | shared command snapshot and refresh | focus plus idle triggers |
| 7 | `F005` | desktop stack and geometry | focus/state/command context |
| 8 | `F006` | close and modal completion | desktop stack and focus veto |
| 9 | `F009` | generic drag plus keyboard parity | event/focus/lifecycle/desktop |

The order deliberately brings `F008` forward so all later keyboard acceptance
uses the repaired real ingress rather than normalized test-only events.

## Evidence Model

The Finding table contains exactly one row per `F001`-`F009` with:
`FindingId`, `ContractId`, `Decision`, `RedProof`, `Change`, `RealPathProof`,
`HistoricalIntent`, `FreeVisionRelation`, `ModernRationale`, `ApiImpact`,
`A11YImpact`, `PlatformBoundary`, `ResidualBoundary`, and `Result`.

The governance table contains:
`RunId`, `Preset`, `Version`, `Checkpoint`, `Applicability`, `Rationale`,
`Evidence`, `Owner`, `Reviewer`, `ReviewDate`, `Result`, `ResidualRisk`,
`FollowUp`, and `ReevaluationTrigger`.

Feature-024 remains the immutable audit baseline. After all real-path proofs
pass, its findings/readiness surfaces receive resolution metadata and a readable
Feature-025 closure addendum; original observations and source provenance are
not rewritten as if they never existed.

## Validation Strategy

### Always

- `specify check` and prerequisite/checklist completion
- placeholder scan and Spec-Kit Analyze convergence
- `git diff --check`
- `dotnet format --verify-no-changes --no-restore`
- secret/generated-output/dependency/historical-source scope scans
- Feature-024 JSON relationship and resolution validator

### Per Slice

- increment manual build counter immediately before each `dotnet test`
- run the narrowest Release test project/filter proving the red and green path
- record exact command, expected red reason, actual result and proof boundary

### Final Local Gate

- targeted Core, Compatibility, Controls and Drivers tests in Release
- full repository Release test suite
- `xmllint --noout coverlet.runsettings`
- canonical Coverlet collection and assembly-specific line coverage >= 70 percent
- `docfx docfx.json`
- `tests/web-a11y`: `npm ci`, Chromium availability, `npm run test:docfx`
- UTF-8 Lynx/text-first review of changed generated documentation
- no tracked `_site/`, `api/*.yml`, test output, cache, logs or external source

### Remote Gate

- required PR-context checks on the final reviewed head
- thread-level review query with zero actionable threads
- honest recording of unavailable reviewers or quota failures
- narrow admin bypass only if green required checks and zero actionable threads
  leave the approved human-approval rule as the sole blocker

## Delivery Phases

1. Evidence scaffold, validator-trigger inventory, exact source/provenance check.
2. `F001` reference slice: red test, implementation, proof, evidence.
3. `F008`, then `F002`/`F003` with targeted regression matrices.
4. `F004` and `F007` lifecycle/context integration.
5. `F005` and `F006` desktop/modal/close integration.
6. `F009` generic drag and keyboard-equivalent integration.
7. Feature-024 resolution evidence, guide, XML/API, agent parity, Pflichtenheft,
   statistics and Lastenheft archive.
8. Static, targeted, full Release, coverage, DocFX/Axe/Lynx and scope gates.
9. Commit, push, PR, review convergence, merge commit, branch deletion and
   synchronized clean `main`.
10. Autonomous retrospective; local corrections or portable preset follow-up
    only when supported by evidence, never an empty PR.

## Complexity Tracking

No Constitution violation is accepted. Reusing the existing Compatibility
assembly from Controls is a bounded internal repository dependency required to
eliminate the duplicated keyboard mapping; it adds no package, service or
external runtime component. If the actual dependency graph creates a cycle or
public packaging conflict, implementation stops as `ProductDecision` instead
of copying the translator.
