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

- Explanatory documentation must be bilingual (German first, English second) with CEFR-B2 readability.
- Public API changes must include complete XML documentation updates.
- Run `docfx docfx.json` when root config exists and API/XML docs changed.

### Reference Source

`tv203s/contrib/tvision/` contains the original Turbo Vision 2.0.3 C/C++ source. Consult it when porting new classes or understanding original behavior. Do not modify files in `tv203s/`.

## Branching Convention

Feature branches use the pattern `codex/<feature-description>`. CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `gemini/**`, `opencode/**`, and `copilot/**` branches.

## Active Technologies
- C# `latest` (C# 14) / .NET 10 (`net10.0`) + TuiVision.Core (TPoint, TRect, TEvent, TObject, TConsoleBuffer ← verschoben) (001-view-system-tgroup)
- C# `latest` on .NET 10 (`net10.0`) + Existing project modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`; MSTest for tests; docfx for API documentation validation (002-application-framework)
- N/A (in-memory UI state only) (002-application-framework)
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core` (TView, TGroup, TEvent, TObject, TPoint, TRect, (003-dialog-control-layer)
- N/A — in-memory UI state only; keine Persistenz in Phase 5 (003-dialog-control-layer)

## Recent Changes
- 001-view-system-tgroup: Added C# `latest` (C# 14) / .NET 10 (`net10.0`) + TuiVision.Core (TPoint, TRect, TEvent, TObject, TConsoleBuffer ← verschoben)

## Agent File Synchronization Policy

- When active feature context, implementation-plan guidance, or other shared AI-agent instructions change, update the following files together when affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
- Do not leave shared guidance synchronized in only one of these files.
- If an agent-specific file needs intentional divergence, document the reason in the same change.
