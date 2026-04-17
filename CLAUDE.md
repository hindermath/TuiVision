# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TuiVision is a C#/.NET 10 port of the Turbo Vision 2.0.3 TUI framework (originally C/C++). The original source lives in `tv203s/` as the conceptual reference; the C# port is a managed, modernized interpretation — not a line-for-line translation.

## Commands

```bash
# Restore, build, and test (full validation cycle)
dotnet restore
dotnet build --configuration Release
dotnet test

# Run tests for a specific project
dotnet test tests/TuiVision.Core.Tests/

# Run a single test method
dotnet test --filter "FullyQualifiedName~TestMethodName"

# Build generated docs plus Playwright + axe accessibility smoke tests
cd tests/web-a11y
npm install
npx playwright install chromium
npm run test:docfx

# After every DocFX regeneration, rerun the matching A11y smoke check
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

## Architecture

### Module Structure

| Module | Purpose |
|---|---|
| `src/TuiVision.Core` | Foundation types: `TPoint`, `TRect`, `TEvent`, `TObject` |
| `src/TuiVision.Controls` | UI components: `TView` (base for all visual elements) |
| `src/TuiVision.Drivers.Console` | Console rendering: `TConsoleBuffer`, `TConsoleDriver`, `IConsolePresenter` |
| `src/TuiVision.Serialization` | Binary archive system with polymorphic `TRecordRegistry` |
| `src/TuiVision.Compatibility` | Key code translation from .NET keys to Turbo Vision scan codes |

### Key Design Patterns

- **Event system**: `TEvent` uses static factory methods (`TEvent.CreateKeyDown(...)`) and `readonly record struct` payloads. Events are dispatched via `TView.HandleEvent()`.
- **Coordinate system**: `TRect` uses inclusive top-left, exclusive bottom-right bounds. Views maintain local coordinates; `MakeLocal`/`MakeGlobal` convert between coordinate spaces.
- **Console rendering**: Presenter pattern — `TConsoleDriver` manages a back-buffer and publishes snapshots via `IConsolePresenter`, keeping rendering backends swappable.
- **Serialization**: Envelope-based binary format (type ID + payload). Types register themselves with `TRecordRegistry` for polymorphic deserialization.
- **Lifecycle**: `TObject.ShutDown()` for logical teardown + `IDisposable` for resource cleanup.

### Global Build Settings (`Directory.Build.props`)

All projects share: `net10.0`, `LangVersion: latest`, `Nullable: enable`, `ImplicitUsings: enable`.

**Repo Version Scheme** — `Directory.Build.props` carries the repo-wide `Version`, `AssemblyVersion`, and `FileVersion` values for all projects and follows `Major.Minor.Patch.Build`:
- `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`007` -> `7`) and used immediately even before a GitHub PR exists
- `Patch` = current commit count in that feature/PR branch (after committing the current change)
- `Build` = manual build counter incremented before every `dotnet build` or `dotnet test`

Align the three version fields in `Directory.Build.props` whenever a commit is created or the branch is updated on a numbered Spec-Kit branch, before pushing.

### Testing

Tests use MSTest. Test projects mirror source projects (e.g., `TuiVision.Core.Tests` → `TuiVision.Core`). `TuiVision.Examples.SmokeTests` is for integration-level tests of ported example programs.

**Coverage Gate (SC-003)**: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each achieve ≥ 70 % Line Coverage (Pflichtenheft §9.4 Nr. 1). Measured with Coverlet (`coverlet.collector`): `dotnet test --collect:"XPlat Code Coverage"`. Do not merge to `main` without passing this gate.

### Documentation

- Use `System.Text.Json` for project-owned JSON parsing and serialization.
- Introduce `Newtonsoft.Json` only with documented justification and explicit
  reviewer approval.
- Explanatory documentation must be bilingual (German first, English second) with CEFR-B2 readability.
- Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise.
- **`Programmierung #include<everyone>`** — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI) mit Deutsch und Englisch als Arbeitssprachen sowie an sehbehinderte Lernende, die mit Braille-Displays, Screen-Readern oder Textbrowsern arbeiten. Barrierefreiheit ist kein Nice-to-have, sondern Pflichtanforderung. Guides, statistics, examples, and generated API documentation must remain usable in text-first assistive setups such as Braille displays, screen readers, and text browsers.
- Generated HTML documentation should target WCAG 2.2 conformance level AA as the accessibility baseline.
- Prefer semantic headings, lists, tables, and ASCII/text-first diagrams; do not encode essential meaning only through color, layout, or pointer-only affordances.
- Treat bilingual CEFR-B2 delivery and the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts.
- Public API changes must include complete XML documentation updates.
- Run `docfx docfx.json` when root config exists and API/XML docs changed.
- Keep the Playwright + `@axe-core/playwright` smoke tests in `tests/web-a11y/` aligned with the current DocFX structure and representative pages; use `lynx` as an additional text-browser spot check when available.
- Treat every successful `docfx docfx.json` regeneration as incomplete until the matching `tests/web-a11y/` A11y smoke check has also passed in the same work item.

### Reference Source

`tv203s/contrib/tvision/` contains the original Turbo Vision 2.0.3 C/C++ source. Consult it when porting new classes or understanding original behavior. Do not modify files in `tv203s/`.

## Branching Convention

Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch. CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `gemini/**`, `opencode/**`, and `copilot/**` branches.
When a dedicated feature branch has implemented the requirements of a Lastenheft, rename that file to `Lastenheft_<topic>.<feature-branch>.md` so the delivered scope stays traceable.

## Active Feature Context

### 004-editor-file-help-streams
- Current implementation baseline: execute the phase-6 increment from `specs/004-editor-file-help-streams/spec.md` and `specs/004-editor-file-help-streams/plan.md`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Out of scope for this increment: example applications such as `tvedit`, `bhelp`, and `helpdemo`; driver consolidation; calculator/macros/OS-shell integrations; and unrelated specialized widgets
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes

### 005-driver-consolidation-m07
- Current planning baseline: execute the Phase-7 increment from `specs/005-driver-consolidation-m07/spec.md` and `specs/005-driver-consolidation-m07/plan.md`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- Out of scope for this increment: mandatory example waves, full closure of the Phase-8 entrance gate, new source modules, native bindings, and any one-to-one recreation of the historical per-OS driver split
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure

### 006-close-phase8-gate
- Current planning baseline: execute the Phase-8 entrance-gate closure from `specs/006-close-phase8-gate/spec.md` and `specs/006-close-phase8-gate/plan.md`
- Scope is limited to final `M-07` proof closure plus the remaining gate evidence across `docs/porting-status.md`, `Pflichtenheft.md`, the existing module test projects plus any required Compatibility-focused validation additions, coverage evidence, formatting evidence, and API-documentation validation
- Out of scope for this increment: any of the 25 mandatory example waves, substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, unrelated new framework features, and Phase-7 redesign beyond finalizing still-pending proof rows
- Every historical `.cc` ledger row must end in `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no provisional `portiert + Test ausstehend` state may remain after closure is claimed
- Gate closure must package explicit build, full-test, coverage, formatting, and conditional API-doc evidence and keep example waves blocked until the closure is formally recorded
- Planning decisions now fixed for this feature: `docs/porting-status.md` stays the authoritative M-07 ledger; `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each satisfy the explicit 70 % line-coverage gate with assembly-specific evidence; `tests/TuiVision.Compatibility.Tests/` is the planned dedicated Compatibility fallback suite when shared tests are insufficient; placeholder-only or no-op-only modules cannot satisfy that gate; gate-scope removals must update the proof surfaces in the same change; skipped or ignored gate-scoped tests require recorded tracked-issue references; unresolved local-versus-CI coverage conflicts block closure; and the closure needs a dedicated gate-marker or commit reference

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Real local file-system interaction plus persisted binary help/resource files; no database layer in this increment (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, existing MSTest suites plus any required Compatibility-focused validation additions, Coverlet coverage evidence, `dotnet format`, docfx, `Pflichtenheft.md`, and `docs/porting-status.md` for the formal Phase-8 entrance-gate proof (006-close-phase8-gate)
- Repository-visible proof artifacts only; no database layer and no example-application delivery in this increment (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell and widget foundation from `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions plus existing example-smoke infrastructure for downstream wave-2 readiness (009-controls-widgets-and-collections)
- In-memory UI state only in production; source-controlled planning, tests, proof artifacts, and already delivered downstream examples in `specs/`, `tests/`, `docs/`, and `examples/`; no database or external service storage (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes
- 004-editor-file-help-streams: Added the phase-6 specification and requirements checklist for editor, file, help, stream, and resource components.
- 004-editor-file-help-streams: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/public-api.md`; synchronized shared agent guidance to the post-plan baseline.
- 004-editor-file-help-streams: Applied plan-review clarifications for safe-close vs. overwrite handling, wildcard-filtered file dialogs, explicit malformed-stream cases, and non-functional scope boundaries.
- 004-editor-file-help-streams: Added explicit coverage of insert/overwrite plus clipboard editor actions, synchronized file-information state in dialogs, shell menu/status routing, and the full Core/Controls/Serialization coverage gate.
- 004-editor-file-help-streams: Tightened the remaining integration-test expectations so event-loop dispatch, focus transitions, menu execution, and explicit dialog interaction are named directly in the feature artifacts.
- 005-driver-consolidation-m07: Added the phase-7 specification and clarification set for managed driver consolidation, `M-07` proof coverage, primary versus secondary ledger targets, and required Linux/Windows/WSL compatibility evidence.
- 005-driver-consolidation-m07: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-7-proof-contract.md` to define the capability-based driver consolidation approach, the formal `.cc` ledger scope, and the review contract for `docs/porting-status.md`.
- 005-driver-consolidation-m07: Implemented Phase-7 consolidation: created `DriverCapabilityMap.cs` with 5 capability buckets, built `docs/porting-status.md` covering all 151 historical `.cc` files, added 5 new driver test files (30 tests passing), updated `docs/guides/multi-mac-workflow.md` with compatibility evidence, created `checklists/phase-8-gate-review.md`, updated `Pflichtenheft.md` marker to Phase-8 gate closure.
- 006-close-phase8-gate: Added the Phase-8 entrance-gate specification and requirements checklist for final `M-07` closure, removal of provisional ledger states, and explicit build/test/coverage/format/API-doc proof before example waves may start.
- 006-close-phase8-gate: Synchronized the hard coverage rule to require `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each to reach at least 70 % line coverage across the shared agent guidance and gate-tracking artifacts.
- 006-close-phase8-gate: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-8-gate-contract.md` to define the final `M-07` closure workflow, the 5x-70%-coverage gate, the full-suite validation package, and the dedicated closure-commit contract.
- 006-close-phase8-gate: Refined the plan after clarification so the five-module coverage gate now requires assembly-specific reporting and rejects placeholder-only or no-op-only closure modules.
- 006-close-phase8-gate: Analyse-Remediation nach der Task-Pruefung nachgezogen: `gate-docs.md` als Planartefakt explizit benannt, `tests/TuiVision.Compatibility.Tests/` als feste Plan-Baseline aufgenommen und Skip/Ignore-Faelle nur noch mit dokumentierter Tracking-Issue-Referenz als gate-konform zugelassen.
- 007-port-wave1-examples: Wave 1 portiert: `desklogo` (minimale Desktop-App), `msgcls` (Broadcast-Nachrichtenrouting), `tutorial` (16 token-basierte Schritte), `videomode` (Faehigkeitserkennung + Fallback). 41 Smoke-Tests gruen, Release-Build sauber, `dotnet format --verify-no-changes` bestanden. `Pflichtenheft.md` Welle-1-Checkliste abgehakt, `>>> NAECHSTER SCHRITT <<<` auf Welle 2 vorgeschoben.
- 008-controls-revision: Controls-Revision implementiert: `TSubMenu`, `TStatusDef`, `WindowFlags` neu hinzugefuegt; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` erweitert; 338 Tests gruen, `TuiVision.Controls`-Abdeckung 84,02 %, Format-Gate bestanden; `docs/porting-status.md`, `Pflichtenheft.md` und `docs/project-statistics.md` nachgezogen; `Lastenheft_ControlsRevision.md` umbenannt.

## Agent File Synchronization Policy

- When active feature context, implementation-plan guidance, or other shared AI-agent instructions change, update the following files together when affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
  - `.github/agents/copilot-instructions.md`
- Do not leave shared guidance synchronized in only one of these files.
- If an agent-specific file needs intentional divergence, document the reason in the same change.

## Project Statistics

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; no later top-level section should follow it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that show at least the current artifact mix, the documented branch/phase curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window; refresh them whenever the statistics ledger changes.
- Keep each short CEFR-B2 explanation directly adjacent to its matching ASCII diagram group, ideally immediately before or after it, so apprentices do not need to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- Each update must capture branch/phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 code lines per day for an experienced developer, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal/Turbo-Vision-derived port.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Workflow Platforms

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `gemini` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- After every `/speckit-plan` run or equivalent plan refresh that changes active technologies, project structure, or agent context, run `.specify/scripts/bash/update-agent-context.sh` for `codex`, `claude`, `gemini`, and `copilot` in the same work item by default. Treat this multi-agent context refresh as routine maintenance that does not need a separate user prompt.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Pflichtenheft Next-Step Marker

- Maintain a prominent `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md`.
- The marker MUST point to the currently highest-priority open work item in the prioritized rest-work section and MUST be moved whenever progress changes the effective next step.

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
<!-- claude-init-done -->

## Hinweise / Notes

- Diese Datei ergaenzt die projektspezifische Dokumentation mit agentischen Arbeitsregeln.
- This file complements the project-specific documentation with agent-oriented working rules.
