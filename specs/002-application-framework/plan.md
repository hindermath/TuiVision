# Implementation Plan: Application Framework Shell

**Branch**: `[002-application-framework]` | **Date**: 2026-03-20 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/002-application-framework/spec.md](./spec.md)
**Input**: Feature specification from `/specs/002-application-framework/spec.md`

**Note**: This plan covers the shell infrastructure increment only: `TProgram`, `TApplication`, menu bar, status line, desktop hosting, focus recovery, and global command routing. Concrete dialogs, control widgets, and specialized window classes remain out of scope by specification.

## Summary

Implement the first complete application shell on top of the existing `TView`/`TGroup` foundation. The design will add a default `TApplication` shell that composes a menu bar, desktop workspace, and status line, routes commands consistently across menu/status/keyboard entry points, and keeps focus valid during startup and desktop child changes. The implementation stays inside the existing module hierarchy, relies on the managed console driver abstraction for rendering, and is validated primarily through new MSTest coverage in `tests/TuiVision.Controls.Tests` plus a focused shell-level integration slice.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing project modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`; MSTest for tests; docfx for API documentation validation  
**Storage**: N/A (in-memory UI state only)  
**Testing**: MSTest unit and integration-style tests in `tests/TuiVision.Controls.Tests`; full repository validation via `dotnet build --configuration Release`, `dotnet test`, and `dotnet format --verify-no-changes`  
**Target Platform**: Cross-platform terminal applications on macOS, Linux, and Windows using managed .NET APIs only  
**Project Type**: Managed .NET library framework with example-oriented terminal UI abstractions  
**Performance Goals**: First interactive shell frame renders on startup without extra setup steps; focus changes and global command dispatch remain deterministic and effectively immediate for single-user local terminal workflows  
**Constraints**: No native dependencies; preserve current five-module architecture; shell increment only; `TApplication` must auto-create the default shell; unavailable global actions remain visible but disabled; public and non-public members require bilingual documentation updates  
**Scale/Scope**: New shell-layer types in `src/TuiVision.Controls` with supporting tests in `tests/TuiVision.Controls.Tests`; no new modules; no dialog/control implementations in this increment

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. All planned work remains in managed C# and consumes the existing `TConsoleDriver` abstraction rather than introducing native console bindings.
- **Test-First Development — TDD**: Pass with explicit workflow requirement. Tasks must start with failing MSTest coverage for shell startup, command routing, focus recovery, and disabled-action behavior before production code is added.
- **Didactic and Linguistic Clarity**: Pass. New and changed members will require bilingual German-first/English-second XML documentation at B2 level, including non-public types and helpers introduced for shell composition.
- **Modular Architecture**: Pass. New shell types belong in `TuiVision.Controls`; they may depend on `TuiVision.Core` and may collaborate with `TuiVision.Drivers.Console` only through existing abstractions already exposed by the repository structure.
- **Cross-Platform Portability**: Pass. No OS-specific logic is planned for `TuiVision.Core` or `TuiVision.Controls`; runtime-specific behavior stays behind the console driver abstraction.
- **License & Disclaimer Integrity**: Pass. No changes are planned under `tv203s/`; new source files must keep the project’s MIT header convention.

**Post-Design Gate Review**: Phase 1 artifacts keep the feature within existing module boundaries, define only library-surface contracts, and do not require constitution exceptions. No gate violations are currently expected.

## Project Structure

### Documentation (this feature)

```text
specs/002-application-framework/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── application-shell-api.md
└── tasks.md
```

### Source Code (repository root)

```text
src/
├── TuiVision.Core/
│   ├── TObject.cs
│   ├── TEvent.cs
│   ├── TPoint.cs
│   ├── TRect.cs
│   └── TConsoleBuffer.cs
├── TuiVision.Controls/
│   ├── TView.cs
│   ├── TGroup.cs
│   ├── TProgram.cs                # planned
│   ├── TApplication.cs            # planned
│   ├── TDesktop.cs                # planned
│   ├── TMenuBar.cs                # planned
│   ├── TStatusLine.cs             # planned
│   ├── TMenuItem.cs               # planned
│   ├── TStatusItem.cs             # planned
│   └── ShellCommandIds.cs         # planned
└── TuiVision.Drivers.Console/
    └── TConsoleDriver.cs

tests/
├── TuiVision.Controls.Tests/
│   ├── TGroupTests.cs
│   ├── TViewExtendedTests.cs
│   ├── TProgramTests.cs           # planned
│   ├── TApplicationTests.cs       # planned
│   ├── TDesktopTests.cs           # planned
│   ├── TMenuBarTests.cs           # planned
│   └── TStatusLineTests.cs        # planned
├── TuiVision.Core.Tests/
└── TuiVision.Examples.SmokeTests/
```

**Structure Decision**: Keep the feature entirely within the existing single-repository, multi-project library structure. Shell behavior belongs in `src/TuiVision.Controls` because it composes views, groups, focus, and event routing. No new assembly is justified under Constitution principle IV.

## Phase 0 Research Summary

See `research.md` for full detail. Key planning decisions:

1. Place shell orchestration in `TuiVision.Controls` instead of `TuiVision.Drivers.Console`.
2. Model the desktop as a `TGroup`-derived workspace so it reuses child ownership and focus behavior already present.
3. Keep menu and status command definitions as lightweight view-oriented models rather than introducing a general-purpose command bus module.
4. Validate shell behavior with unit/integration-style MSTest coverage before implementation to satisfy the constitution’s visible Red-Green-Refactor requirement.

## Phase 1 Design Overview

- `TProgram` becomes the shell coordinator responsible for startup, frame composition, global event dispatch, and shutdown flow.
- `TApplication` acts as the convenience subclass that auto-creates the default shell regions and exposes customization hooks for menu bar, desktop, and status line.
- `TDesktop` hosts child views/windows and owns focus fallback rules when the active workspace item changes or closes.
- `TMenuBar` and `TStatusLine` remain visible global surfaces that map user actions to shared command identifiers.
- Disabled commands remain visible in both surfaces and are prevented from executing.
- Contracts document behavioral expectations for the public shell API without overcommitting to premature internal signatures.

## Implementation Strategy

1. Add failing tests for shell construction, default composition, disabled command presentation, command routing, and focus recovery.
2. Introduce minimal shell data types and command identifiers needed to make the tests compile.
3. Implement `TDesktop`, `TMenuBar`, and `TStatusLine` as focused `TView`/`TGroup`-based components.
4. Implement `TProgram` orchestration for startup, event dispatch, and controlled shutdown.
5. Implement `TApplication` default shell creation and customization extension points.
6. Refactor names, helper methods, and documentation once tests pass and behavior is stable.
7. Run build, test, format, and doc generation checks required by the constitution when public APIs change.

## Testing Strategy

- **Unit tests**: Child insertion/removal, menu/status availability rules, command dispatch single-execution guarantee, and focus fallback behavior.
- **Shell integration tests**: Default `TApplication` startup produces menu/desktop/status layout, global commands route identically from menu and status line, and desktop child closure recovers focus.
- **Regression tests**: Preserve existing `TView` and `TGroup` behavior by extending rather than replacing current ownership and dispatch patterns.
- **Validation commands**: `dotnet build --configuration Release`, `dotnet test`, `dotnet format --verify-no-changes`, and `docfx docfx.json` if public API/XML docs change.

## Risks & Mitigations

- **Risk**: Overcoupling shell orchestration to rendering details.  
  **Mitigation**: Keep rendering concerns behind existing buffer/presenter abstractions and express shell behavior in terms of views and events.

- **Risk**: Focus rules become brittle when no desktop child remains.  
  **Mitigation**: Define explicit desktop fallback behavior in tests before implementation and centralize focus recovery in desktop/program coordination.

- **Risk**: Menu and status actions diverge in behavior.  
  **Mitigation**: Route both through shared command identifiers and assert identical outcomes in tests.

- **Risk**: Public API shape grows too quickly before dialog/window types exist.  
  **Mitigation**: Keep contracts narrow and scoped to shell infrastructure only, deferring specialized window/dialog abstractions to later increments.

## Complexity Tracking

No constitution violations or exceptional complexity justifications are currently required.
