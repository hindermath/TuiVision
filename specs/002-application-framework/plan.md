# Implementation Plan: Application Framework Shell

**Branch**: `[002-application-framework]` | **Date**: 2026-03-20 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/002-application-framework/spec.md](./spec.md)
**Input**: Feature specification from `/specs/002-application-framework/spec.md`

**Note**: This plan covers the shell infrastructure increment only: `TProgram`, `TApplication`, menu bar, status line, desktop hosting, focus recovery, and global command routing. Concrete dialogs, control widgets, and specialized window classes remain out of scope by specification.

## Summary

Implement the first complete application shell on top of the existing `TView`/`TGroup` foundation. The design will add a default `TApplication` shell that composes a menu bar, desktop workspace, and status line, routes commands consistently across menu/status/keyboard entry points, and keeps focus valid during startup and desktop child changes. The implementation stays inside the existing module hierarchy, relies on the managed console driver abstraction for rendering, and is validated primarily through new MSTest coverage in `tests/TuiVision.Controls.Tests` plus a focused shell-level integration slice.

## Terminology & Operational Definitions

- **Controlled shutdown**: The shell accepts an exit action, stops accepting new application commands, releases shell-owned views in a documented order, and leaves no orphaned shell region references.
- **Valid focus target**: At every interactive point, focus belongs either to an eligible desktop child or to the desktop workspace itself when no eligible child exists.
- **Effectively immediate**: For this local single-user terminal increment, shell startup, focus changes, and global command dispatch are expected to complete within the same interaction cycle and without introducing extra user-visible setup steps or deferred confirmation phases.
- **Customize or replace shell regions**: This increment must expose at least one supported customization seam for menu bar, desktop, and status line composition, but it does not yet commit to a final API mechanism such as overridable methods versus injected builders.
- **Illustrative API sketch**: Example code in planning artifacts may show likely extension seams, but examples do not freeze exact public member names or signatures unless the contract states them explicitly.
- **Command ID**: An integer constant (`const int cmXxx`) that uniquely identifies an application command. Command IDs are the shared currency for routing, availability checks, menu binding, and status hint binding. Standard shell commands (e.g., `cmQuit`) are defined in `ShellCommandIds`.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)
**Primary Dependencies**: Existing project modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`; MSTest for tests; docfx for API documentation validation
**Storage**: N/A (in-memory UI state only)
**Testing**: MSTest unit and integration-style tests in `tests/TuiVision.Controls.Tests`; full repository validation via `dotnet build --configuration Release`, `dotnet test`, and `dotnet format --verify-no-changes`
**Target Platform**: Cross-platform terminal applications on macOS, Linux, and Windows using managed .NET APIs only
**Project Type**: Managed .NET library framework with example-oriented terminal UI abstractions
**Performance Goals**: First interactive shell frame renders on startup without extra setup steps; focus changes and global command dispatch remain deterministic and effectively immediate for single-user local terminal workflows
**Constraints**: No native dependencies; preserve current five-module architecture; shell increment only; `TApplication` must auto-create the default shell; unavailable global actions remain visible but disabled; public and non-public members require bilingual documentation updates; ≥70% line coverage gate on all new shell classes (SC-005)
**Scale/Scope**: New shell-layer types in `src/TuiVision.Controls` with supporting tests in `tests/TuiVision.Controls.Tests`; no new modules; no dialog/control implementations in this increment

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. All planned work remains in managed C# and consumes the existing `TConsoleDriver` abstraction rather than introducing native console bindings.
- **Test-First Development — TDD**: Pass with explicit workflow requirement. Tasks must start with failing MSTest coverage for shell startup, command routing, focus recovery, and disabled-action behavior before production code is added.
- **Didactic and Linguistic Clarity**: Pass. New and changed members will require bilingual German-first/English-second XML documentation at B2 level, including non-public types and helpers introduced for shell composition.
- **Modular Architecture**: Pass. New shell types belong in `TuiVision.Controls`; they may depend on `TuiVision.Core` and may collaborate with `TuiVision.Drivers.Console` only through existing abstractions already exposed by the repository structure.
- **Cross-Platform Portability**: Pass. No OS-specific logic is planned for `TuiVision.Core` or `TuiVision.Controls`; runtime-specific behavior stays behind the console driver abstraction.
- **License & Disclaimer Integrity**: Pass. No changes are planned under `tv203s/`; new source files must keep the project's MIT header convention.

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

### Planned Artifact Status

- `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, and `TStatusLine` are required shell deliverables for this increment because they map directly to the feature scope in the specification.
- `TMenuItem`, `TStatusItem`, and `ShellCommandIds` are required planning artifacts as lightweight internal design placeholders for shared command routing and visible action state; they are not automatically commitments to a large public API surface.
- `ShellCommandIds` defines `const int cmXxx` integer constants that are the shared currency for command routing, availability checks, menu binding, and status hint binding across the shell.
- Exact member signatures for customization seams remain intentionally open until implementation, but the presence of customization support itself is mandatory for this increment.

## Phase 0 Research Summary

See `research.md` for full detail. Key planning decisions:

1. Place shell orchestration in `TuiVision.Controls` instead of `TuiVision.Drivers.Console`.
2. Model the desktop as a `TGroup`-derived workspace so it reuses child ownership and focus behavior already present.
3. Keep menu and status command definitions as lightweight view-oriented models rather than introducing a general-purpose command bus module.
4. Identify commands by integer constants (`const int cmXxx` in `ShellCommandIds`) matching the original Turbo Vision model; routing and availability checks compare Command IDs by integer value.
5. Validate shell behavior with unit/integration-style MSTest coverage before implementation to satisfy the constitution's visible Red-Green-Refactor requirement.

## Phase 1 Design Overview

- `TProgram` becomes the shell coordinator responsible for startup, frame composition, global event dispatch, terminal resize handling, and shutdown flow.
- `TApplication` acts as the convenience subclass that auto-creates the default shell regions and exposes customization hooks for menu bar, desktop, and status line. Default layout: `TMenuBar` occupies 1 row at the top, `TStatusLine` occupies 1 row at the bottom, `TDesktop` fills all remaining rows.
- `TDesktop` hosts child views/windows and owns focus fallback rules when the active workspace item changes or closes.
- `TMenuBar` renders the application's menu hierarchy and handles keyboard activation (F10 or Alt to open, arrow keys to navigate, Enter/Escape to confirm or dismiss). Menus support nesting via a `TMenuBar → TMenu → TSubMenu` hierarchy matching the original Turbo Vision structure.
- `TStatusLine` automatically updates its displayed hints and shortcuts whenever focus changes; each view may declare its own status hints and `TStatusLine` reflects the focused view's hints without requiring manual application updates.
- Commands are identified by integer constants (`const int cmXxx`) defined in `ShellCommandIds`. Routing, availability checks, menu item binding, and status hint binding all use Command ID integer comparison.
- Disabled commands remain visible in both surfaces and are prevented from executing.
- Terminal resize events are detected at runtime and trigger a full re-layout of all shell regions to fit the new terminal dimensions.
- Contracts document behavioral expectations for the public shell API without overcommitting to premature internal signatures.

### Responsibility Boundaries

- `TProgram` owns shell lifecycle, root-level command routing, frame-level coordination, terminal resize event detection and re-layout, and the transition from initialized to interactive to shutting down.
- `TApplication` owns default shell creation and default wiring of menu bar, desktop workspace, and status line with the fixed 1-row/fill/1-row layout. It specializes convenience, not the core command-routing contract.
- `TDesktop` owns workspace hosting, active-child tracking, and focus fallback. When the active child closes, `TDesktop` must prefer the next eligible child and fall back to the desktop itself only when no eligible child remains.
- `TMenuBar` owns visible menu action presentation, nested submenu rendering, and F10/Alt keyboard activation with arrow-key navigation. It does not own business execution logic.
- `TStatusLine` owns visible shortcut/context presentation and disabled-state visibility. It automatically reads the focused view's status hints on each focus change and routes execution through the same shared Command ID path as the menu bar.
- `ShellCommandIds` owns the canonical set of integer command constants for standard shell commands (e.g., `cmQuit`). Application authors extend with additional constants in their own scope.

### Customization Boundary for This Increment

- This increment must support customization or replacement of menu bar, desktop, and status line regions after default shell creation.
- The contract requires the availability of customization seams, but the exact API form remains intentionally undecided during planning.
- Concrete dialogs, control widgets, and specialized window classes remain out of scope even if later customization points may host them.

## Implementation Strategy

1. Add failing tests for shell construction, default composition, disabled command presentation, command routing, focus recovery, status line focus-driven updates, and terminal resize re-layout.
2. Introduce minimal shell data types and command identifiers (`ShellCommandIds` with `const int cmXxx` constants) needed to make the tests compile.
3. Implement `TDesktop`, `TMenuBar` (with nested submenu support), and `TStatusLine` (with focus-driven hint updates) as focused `TView`/`TGroup`-based components.
4. Implement `TProgram` orchestration for startup, event dispatch, terminal resize handling, and controlled shutdown.
5. Implement `TApplication` default shell creation and customization extension points.
6. Refactor names, helper methods, and documentation once tests pass and behavior is stable.
7. Run build, test, format, and doc generation checks required by the constitution when public APIs change.

## Scenario & Edge-Case Coverage

### Scenario Matrix

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| Primary startup flow | User Story 1 | `plan.md` summary, `quickstart.md`, `contracts/application-shell-api.md` |
| Shared global action flow | User Story 2 | `research.md`, `data-model.md` command model, `contracts/application-shell-api.md`, `quickstart.md` |
| Desktop child activation flow | User Story 3 | `plan.md` design overview, `data-model.md`, `contracts/application-shell-api.md` |
| Empty desktop startup | Edge Cases | `data-model.md` desktop focus state, `quickstart.md` expected outcomes |
| Disabled command visibility | Edge Cases | `research.md`, `data-model.md`, `contracts/application-shell-api.md`, test strategy |
| Active-child closure recovery | Edge Cases | `data-model.md` desktop focus transitions, `contracts/application-shell-api.md`, shell integration tests |
| Terminal resize re-layout | Edge Cases / FR-012 | layout responsibility boundary in `TProgram`, resize handling in integration tests |
| Nested submenu navigation | FR-002 | `data-model.md` menu hierarchy, `TMenuBar` responsibility boundary, `TMenuBarTests.cs` |
| Status line focus-driven update | FR-003 | `data-model.md` status hint model, `TStatusLine` responsibility boundary, `TStatusLineTests.cs` |

### Reviewer Readiness Criteria

- Reviewers must be able to point to a written artifact for each of these shell behaviors before tasks are generated:
  - default shell creation
  - shared command routing (integer Command IDs)
  - disabled-but-visible global actions
  - focus fallback after active-child removal
  - terminal resize re-layout
  - status line auto-update on focus change
- If any of those behaviors are only implied and not explicitly described in at least one planning artifact plus one validation-oriented artifact, the plan is not review-ready.

## Testing Strategy

- **Unit tests**: Child insertion/removal, menu/status availability rules, command dispatch single-execution guarantee, focus fallback behavior, status line hint refresh on focus change, nested submenu expansion/collapse, and integer Command ID routing correctness.
- **Shell integration tests**: Default `TApplication` startup produces menu/desktop/status layout with correct 1-row geometry; global commands route identically from menu and status line; desktop child closure recovers focus; terminal resize triggers re-layout of all three regions.
- **Regression tests**: Preserve existing `TView` and `TGroup` behavior by extending rather than replacing current ownership and dispatch patterns.
- **Validation commands**: `dotnet build --configuration Release`, `dotnet test`, `dotnet format --verify-no-changes`, and `docfx docfx.json` if public API/XML docs change.
- **Coverage gate (SC-005)**: `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"` must report ≥70% line coverage for all new shell classes before merging to `main`.

### Success-Criteria Traceability

| Success criterion | Planning hook |
|---|---|
| `SC-001` usable default shell on first interactive screen | default shell creation in design overview, shell integration tests, quickstart expected outcomes |
| `SC-002` primary action from menu or status line within limited interactions | shared command routing model (integer Command IDs), menu/status command consistency tests, contract shared-command guarantee |
| `SC-003` usable state after desktop-management actions | desktop focus transitions in data model, focus recovery guarantee in contract, shell integration tests |
| `SC-004` startup-to-exit demonstration in under five minutes | quickstart workflow, validation commands, controlled-shutdown definition |
| `SC-005` ≥70% line coverage for all new shell classes | Coverlet measurement in testing strategy, coverage gate in validation commands |

## Non-Functional Operationalization

- **Portability**: All shell logic remains in `TuiVision.Controls`; no OS-specific behavior is introduced there.
- **Documentation completeness**: New public and non-public members introduced for shell composition require bilingual documentation in the same change as implementation.
- **TDD discipline**: Tasks must be ordered so failing tests appear first for shell creation, command routing, disabled visibility, focus recovery, status line focus-driven updates, and terminal resize.
- **Performance interpretation**: "First interactive shell frame" means the default shell reaches an interactable state during initial startup without additional user configuration, modal setup steps, or deferred region construction after launch.
- **No persistence**: The shell increment manages runtime UI state only; it must not add persistence, serialization duties, or file-backed shell preferences as part of this feature.

## Dependencies & Assumptions

- The plan depends on the current `TView`/`TGroup` ownership and event-dispatch model being extensible enough for desktop hosting, focus fallback, and terminal resize propagation.
- The relationship to `TuiVision.Drivers.Console` is intentionally indirect: shell types may rely on rendering abstractions already available to views, but must not absorb driver responsibilities.
- Lightweight menu/status action models and shared integer command constants are assumed sufficient for this increment; a broader command framework is intentionally deferred.
- If implementation discovers that `TGroup` semantics are insufficient for deterministic focus fallback, that gap must be recorded before expanding scope.
- Terminal resize event detection is assumed to be available through the existing `TConsoleDriver` abstraction or a thin managed wrapper; no native signal handling (SIGWINCH) is introduced directly in `TuiVision.Controls`.

## Traceability Matrix

| Spec reference | Planned coverage |
|---|---|
| `FR-001` | summary, design overview, contract `TProgram` and `TApplication` |
| `FR-001a` | terminology definitions, customization boundary, contract `TApplication`, fixed layout geometry in design overview |
| `FR-002` / `FR-003` | project structure, `TMenuBar` (nested submenu hierarchy) / `TStatusLine` (focus-driven hint update) responsibilities, data model action entities |
| `FR-004` | research decision 4, integer Command ID model (`ShellCommandIds`), command binding, contract shared-command guarantee |
| `FR-005` / `FR-006` | `TDesktop` responsibilities, desktop focus state transitions, integration-test strategy |
| `FR-007` | controlled-shutdown definition, shell lifecycle state model, `TProgram` contract |
| `FR-008` | scenario matrix empty-desktop startup, quickstart expected outcomes |
| `FR-009` / `FR-009a` | disabled command visibility in design overview, command availability state model, contract visibility guarantee |
| `FR-010` | customization boundary, quickstart illustrative customization seam |
| `FR-011` | note, scope boundary, contract scope guarantee, reviewer readiness criteria |
| `FR-012` | `TProgram` resize responsibility, scenario matrix terminal resize row, shell integration tests |
| `SC-005` | coverage gate in testing strategy, validation commands, Coverlet measurement |

## Risks & Mitigations

- **Risk**: Overcoupling shell orchestration to rendering details.
  **Mitigation**: Keep rendering concerns behind existing buffer/presenter abstractions and express shell behavior in terms of views and events.

- **Risk**: Focus rules become brittle when no desktop child remains.
  **Mitigation**: Define explicit desktop fallback behavior in tests before implementation and centralize focus recovery in desktop/program coordination.

- **Risk**: Menu and status actions diverge in behavior.
  **Mitigation**: Route both through shared integer Command IDs and assert identical outcomes in tests.

- **Risk**: Terminal resize handling couples shell layout to driver internals.
  **Mitigation**: Surface resize events through the existing `TConsoleDriver` abstraction; `TProgram` reacts to a resize event type rather than OS signals directly.

- **Risk**: Nested submenu rendering adds disproportionate complexity.
  **Mitigation**: Model the hierarchy as a recursive structure (`TMenu` containing items that may be `TSubMenu` nodes); keep rendering logic localized to `TMenuBar` and cover each level in unit tests.

- **Risk**: Public API shape grows too quickly before dialog/window types exist.
  **Mitigation**: Keep contracts narrow and scoped to shell infrastructure only, deferring specialized window/dialog abstractions to later increments.

## Complexity Tracking

No constitution violations or exceptional complexity justifications are currently required.
