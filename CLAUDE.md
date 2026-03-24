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

### Testing

Tests use MSTest. Test projects mirror source projects (e.g., `TuiVision.Core.Tests` → `TuiVision.Core`). `TuiVision.Examples.SmokeTests` is for integration-level tests of ported example programs.

**Coverage Gate (SC-003)**: `TuiVision.Controls` must achieve ≥ 70 % Line Coverage (Pflichtenheft §9.4 Nr. 1). Measured with Coverlet (`coverlet.collector`): `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`. Do not merge to `main` without passing this gate.

### Documentation

- Use `System.Text.Json` for project-owned JSON parsing and serialization.
- Introduce `Newtonsoft.Json` only with documented justification and explicit
  reviewer approval.
- Explanatory documentation must be bilingual (German first, English second) with CEFR-B2 readability.
- Public API changes must include complete XML documentation updates.
- Run `docfx docfx.json` when root config exists and API/XML docs changed.

### Reference Source

`tv203s/contrib/tvision/` contains the original Turbo Vision 2.0.3 C/C++ source. Consult it when porting new classes or understanding original behavior. Do not modify files in `tv203s/`.

## Branching Convention

Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch. CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `gemini/**`, `opencode/**`, and `copilot/**` branches.

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

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Real local file-system interaction plus persisted binary help/resource files; no database layer in this increment (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references (005-driver-consolidation-m07)

## Recent Changes
- 004-editor-file-help-streams: Added the phase-6 specification and requirements checklist for editor, file, help, stream, and resource components.
- 004-editor-file-help-streams: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/public-api.md`; synchronized shared agent guidance to the post-plan baseline.
- 004-editor-file-help-streams: Applied plan-review clarifications for safe-close vs. overwrite handling, wildcard-filtered file dialogs, explicit malformed-stream cases, and non-functional scope boundaries.
- 004-editor-file-help-streams: Added explicit coverage of insert/overwrite plus clipboard editor actions, synchronized file-information state in dialogs, shell menu/status routing, and the full Core/Controls/Serialization coverage gate.
- 004-editor-file-help-streams: Tightened the remaining integration-test expectations so event-loop dispatch, focus transitions, menu execution, and explicit dialog interaction are named directly in the feature artifacts.
- 005-driver-consolidation-m07: Added the phase-7 specification and clarification set for managed driver consolidation, `M-07` proof coverage, primary versus secondary ledger targets, and required Linux/Windows/WSL compatibility evidence.
- 005-driver-consolidation-m07: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-7-proof-contract.md` to define the capability-based driver consolidation approach, the formal `.cc` ledger scope, and the review contract for `docs/porting-status.md`.
- 005-driver-consolidation-m07: Implemented Phase-7 consolidation: created `DriverCapabilityMap.cs` with 5 capability buckets, built `docs/porting-status.md` covering all 151 historical `.cc` files, added 5 new driver test files (30 tests passing), updated `docs/guides/multi-mac-workflow.md` with compatibility evidence, created `checklists/phase-8-gate-review.md`, updated `Pflichtenheft.md` marker to Phase-8 gate closure.

## Agent File Synchronization Policy

- When active feature context, implementation-plan guidance, or other shared AI-agent instructions change, update the following files together when affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
- Do not leave shared guidance synchronized in only one of these files.
- If an agent-specific file needs intentional divergence, document the reason in the same change.

## Project Statistics

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Each update must capture branch/phase, observable work window, production/test/documentation line counts, main work packages, and the conservative manual baseline of 80 code lines per day for an experienced developer.

## Workflow Platforms

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `gemini` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Pflichtenheft Next-Step Marker

- Maintain a prominent `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md`.
- The marker MUST point to the currently highest-priority open work item in the prioritized rest-work section and MUST be moved whenever progress changes the effective next step.
