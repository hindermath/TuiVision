# Research: Application Framework Shell

## Decision 1: Place shell orchestration in `TuiVision.Controls`

- **Decision**: Implement `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, and `TStatusLine` in `src/TuiVision.Controls`.
- **Rationale**: The existing architecture already places all view, group, focus, and event behavior in `TuiVision.Controls`. The new shell types are visual/application composition concerns, not platform driver concerns.
- **Alternatives considered**:
  - Put orchestration into `TuiVision.Drivers.Console`: rejected because it would mix platform rendering with application semantics.
  - Create a new shell module: rejected because Constitution principle IV forbids new modules without strong justification.

## Decision 2: Reuse `TGroup` as the desktop hosting base

- **Decision**: Model the desktop workspace as a specialized `TGroup` derivative.
- **Rationale**: `TGroup` already provides child ownership, circular child storage, event dispatch phases, and focus management hooks. Reusing it reduces risk and keeps the shell aligned with current view semantics.
- **Alternatives considered**:
  - Create an unrelated desktop container from scratch: rejected because it would duplicate group behavior and increase maintenance cost.
  - Use plain `TView` for desktop: rejected because desktop must host and coordinate child views.

## Decision 3: Use shared command identifiers across menu, status line, and keyboard routes

- **Decision**: Define shared shell command identifiers that are consumed by menu items, status items, and keyboard-triggered actions.
- **Rationale**: The specification requires the same action to behave identically regardless of entry point and to execute only once per user invocation. A shared command model is the simplest way to guarantee equivalence.
- **Alternatives considered**:
  - Give each surface its own callback logic: rejected because it invites divergence and duplicate execution paths.
  - Introduce a separate command-bus module: rejected as unnecessary complexity for this increment.

## Decision 4: `TApplication` auto-creates the default shell

- **Decision**: `TApplication` will be the convenience entry point that creates menu bar, desktop, and status line automatically at startup.
- **Rationale**: This was clarified explicitly in the feature specification and makes the shell usable from the first interactive frame with minimal ceremony for framework consumers.
- **Alternatives considered**:
  - Require callers to assemble all shell regions manually: rejected because it contradicts the clarification and increases integration friction.
  - Auto-create only the desktop: rejected because it weakens the notion of a complete application shell.

## Decision 5: Unavailable actions stay visible but disabled

- **Decision**: Both menu and status line keep unavailable global actions visible and render them as disabled.
- **Rationale**: The specification explicitly chose orientation over hiding. This improves learnability and keeps action discovery stable in a didactic framework.
- **Alternatives considered**:
  - Hide unavailable actions entirely: rejected because it removes user orientation.
  - Show them on one surface but hide them on the other: rejected because it creates inconsistent UX.

## Decision 6: Validate shell lifecycle through tests before code

- **Decision**: Start implementation with failing MSTest coverage for default shell composition, command routing, disabled-action behavior, and focus fallback.
- **Rationale**: The constitution requires a visible TDD sequence and specific integration coverage for event loop, focus transitions, and menu execution.
- **Alternatives considered**:
  - Implement shell classes first and add tests afterward: rejected by Constitution principle II.

## Decision 7: Keep public API contracts behavioral and narrow

- **Decision**: Document API contracts around shell responsibilities and interaction guarantees without locking the plan to unnecessary internal signatures.
- **Rationale**: The repository is still in an incremental porting phase. Behavioral contracts support planning and tasks while leaving room for idiomatic C# API design during implementation.
- **Alternatives considered**:
  - Fully specify every method signature now: rejected because it overcommits before tests shape the design.
  - Skip contracts entirely: rejected because the feature exposes a new library surface to consumers.
