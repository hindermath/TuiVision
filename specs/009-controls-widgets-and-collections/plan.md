# Implementation Plan: Controls Widgets and Collections

**Branch**: `009-controls-widgets-and-collections` | **Date**: 2026-03-29 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision-009-controls-widgets-and-collections/specs/009-controls-widgets-and-collections/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision-009-controls-widgets-and-collections/specs/009-controls-widgets-and-collections/spec.md)
**Input**: Feature specification from `/specs/009-controls-widgets-and-collections/spec.md`

## Summary

Harden the reusable widget and collection surface that the next mandatory
example wave depends on by strengthening the existing list, scroller, input,
history, and clipboard contracts inside `TuiVision.Controls`, and by adding the
missing reusable controls for editable combo-box selection, determinate
progress display, and parameterized text output. The feature stays inside
`src/TuiVision.Controls` and `tests/TuiVision.Controls.Tests`, keeps history and
clipboard semantics in-memory for the active application session, and defers
consuming example smoke coverage until later wave-2 delivery branches. When the
implementation lands, it must update `docs/project-statistics.md` and rename
`Lastenheft_01_ControlsWidgetsAndCollections.md` to
`Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md`
to satisfy the branch-traceability rule. Runtime mouse-support remains
explicitly out of scope for this feature and is handled by a separate later
requirements block.

## Terminology & Operational Definitions

- **List navigation contract**: The combined guarantee for focus, visible
  range, and scroll position across `TListViewer`, `TListBox`, `TScrollBar`,
  and `TScroller`.
- **Session-scoped history**: Input recall data that remains valid only for the
  active application session and is not required to survive a program restart.
- **Managed clipboard semantics**: Application-internal copy/cut/paste state
  that does not depend on operating-system clipboard availability.
- **Editable combo box**: A reusable control that combines free text editing
  with a visible drop-down list of selectable choices.
- **Combo drop-down session**: The temporary open/closed selection state owned
  by the combo box while the user navigates available choices.
- **Determinate progress model**: A progress surface with an explicit numeric
  range and visible state transitions for running, completed, and canceled.
- **Parameterized text surface**: A non-interactive view that formats runtime
  values into a bounded display region and clips output to the available area.
- **Framework acceptance slice**: The mandatory widget-validation coverage that
  lives primarily in `tests/TuiVision.Controls.Tests` before later example
  smoke tests are introduced.
- **Consuming examples**: The later mandatory wave-2 examples `clipboard`,
  `dyntxt`, `inplis`, `listvi`, `progba`, `tcombo`, and `tprogb`, which must
  consume the shared controls delivered by this feature instead of re-defining
  them locally.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream context  
**Storage**: In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage  
**Testing**: MSTest-first validation primarily in `tests/TuiVision.Controls.Tests`, including unit-style widget checks plus integration-style coverage for event-loop dispatch, focus transitions, menu execution, dialog interaction, and framework-first acceptance scenarios; full repository validation via `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Controls.Tests/`, `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`, `dotnet test`, `dotnet format --verify-no-changes`, and conditional `docfx docfx.json` followed by `cd tests/web-a11y && npm run test:docfx`; existing example smoke infrastructure remains downstream context rather than this feature's primary acceptance surface  
**Target Platform**: Managed cross-platform terminal UI on macOS, Linux, and Windows/WSL, with the Multi-Mac workflow (`MacBook Air M2`, `Mac mini M4 Pro`) as the primary development path  
**Project Type**: Managed .NET library increment in `TuiVision.Controls` with companion test expansion and repository-visible planning/proof updates  
**Performance Goals**: List navigation, combo-box drop-down updates, determinate progress redraws, and parameterized text refreshes must become visible on the first redraw cycle after the triggering event during normal terminal interaction; focused widget-test slices should remain suitable for repeated local red-green TDD cycles on the primary Macs  
**Constraints**: Managed-only runtime; no new framework module; acceptance surface primarily in `tests/TuiVision.Controls.Tests`; session-only history; application-internal clipboard is required and host clipboard integration remains optional; editable combo box with visible drop-down is mandatory; determinate numeric progress is mandatory while indeterminate progress remains optional; runtime mouse-support is out of scope; no mandatory example-port delivery in this feature; German-first/English-second CEFR-B2 documentation; numbered-branch version governance in `Directory.Build.props` remains mandatory before any implementation-phase build/test commits  
**Scale/Scope**: Strengthen existing controls (`TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TInputLine`, `TFileInputLine`, `THistory`, `TStringList`, `ManagedClipboard`, and the current `TParamText` baseline) in place; add the missing reusable controls (`TComboBox` and `TProgressBar`) and evolve `TParamText` into the required bounded display surface; expand Controls test coverage with new or broadened test classes; update `docs/project-statistics.md` and rename `Lastenheft_01_ControlsWidgetsAndCollections.md` to `Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md` when the feature is implemented

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. The feature stays entirely within the managed
  `TuiVision.Controls` / `TuiVision.Core` surface. Clipboard support is
  application-internal by requirement, so no native host integration is needed
  for acceptance.
- **Test-First Development — TDD**: Pass with explicit workflow constraint. The
  implementation must begin with failing MSTest coverage for list/scroller
  hardening, session-scoped history and clipboard behavior, editable combo-box
  flows, determinate progress behavior, and parameterized text clipping before
  production changes are added.
- **Didactic and Linguistic Clarity**: Pass. New controls, test helpers, and
  changed members must keep full bilingual XML documentation and explanatory
  comments where they carry learning value.
- **Modular Architecture**: Pass. No sixth module is introduced. All new types
  remain in `TuiVision.Controls`, with tests in `TuiVision.Controls.Tests`.
- **Cross-Platform Portability**: Pass with explicit validation requirement.
  The feature changes runtime widget behavior, so Multi-Mac validation remains
  primary and Linux / Windows/WSL evidence must be planned before feature
  closure.
- **License & Disclaimer Integrity**: Pass. Historical reference files remain
  untouched under `tv203s/`; new project-owned code follows the existing MIT
  licensing and disclaimer rules.

**Post-Design Gate Review**: Phase-1 artifacts keep the feature inside the
existing module layout, preserve MSTest-first validation, and resolve the
widget/collections scope without widening into menu/status/window revision,
runtime mouse-support, editor/help systems, terminal emulation, or the actual
wave-2 example ports.
No constitution exception is required.

- The feature affects runtime behavior and validation workflow, so Linux and
  Windows/WSL evidence must be planned alongside the primary Multi-Mac path.
- The feature does not itself advance one of the 25 mandatory example ports; it
  prepares the framework surface that those mandatory wave-2 examples consume
  later.
- Runtime mouse-support remains intentionally excluded so this branch does not
  reopen the separate interaction/driver work planned after the wave-3 base is
  stable.
- Statistical-documentation impact identified; update
  `docs/project-statistics.md` when this planning or implementation work lands.
- The delivered requirements file must be renamed from
  `Lastenheft_01_ControlsWidgetsAndCollections.md` to
  `Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md`
  once the dedicated feature branch has implemented it.

## Project Structure

### Documentation (this feature)

```text
specs/009-controls-widgets-and-collections/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── widgets-collections-api.md
└── tasks.md
```

### Source Code (repository root)

```text
src/
└── TuiVision.Controls/
    ├── ManagedClipboard.cs          # existing baseline; strengthened in this feature
    ├── TComboBox.cs                 # planned new control
    ├── TFileInputLine.cs
    ├── THistory.cs
    ├── TInputLine.cs
    ├── TIndicator.cs                # existing editor-only indicator; not the generic progress surface
    ├── TListBox.cs
    ├── TListViewer.cs
    ├── TParamText.cs                # existing baseline; extended to bounded view behavior
    ├── TProgressBar.cs              # planned new control
    ├── TScrollBar.cs
    ├── TScroller.cs
    └── TStringList.cs

tests/
└── TuiVision.Controls.Tests/
    ├── ControlsWidgetTestContext.cs    # planned shared test helper
    ├── ControlCoverageSweepTests.cs
    ├── TComboBoxTests.cs            # planned
    ├── TFileInputLineTests.cs       # planned
    ├── TInputLineTests.cs
    ├── THistoryTests.cs             # planned
    ├── TListBoxTests.cs
    ├── TListViewerTests.cs
    ├── TManagedClipboardTests.cs    # planned
    ├── TParamTextTests.cs           # planned
    ├── TProgressBarTests.cs         # planned
    ├── TScrollBarTests.cs
    ├── TScrollerTests.cs
    ├── WidgetAcceptanceScenarioTests.cs # planned
    └── TStringListTests.cs

docs/
├── guides/
│   └── multi-mac-workflow.md
└── project-statistics.md

Lastenheft_01_ControlsWidgetsAndCollections.md   # implementation follow-through renames this file to Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md
```

**Structure Decision**: Keep the feature entirely inside the existing
`TuiVision.Controls` library and `tests/TuiVision.Controls.Tests`. Strengthen
the existing list/input primitives in place and add only the missing reusable
widget types needed by the spec (`TComboBox`, `TProgressBar`, and the required
bounded-view evolution of `TParamText` plus strengthened `ManagedClipboard`) rather than
introducing a dedicated widget subsystem or example-local helper assemblies.

## Research Focus

Phase 0 resolves and locks the following planning decisions:

1. Keep the feature inside the existing Controls/test projects and do not
   introduce a new module.
2. Strengthen list, list-box, scroll-bar, and scroller behavior in place
   instead of creating a second collections subsystem.
3. Treat history and clipboard semantics as in-memory, active-session-only
   framework services.
4. Introduce `TComboBox` as an editable input plus visible drop-down list
   rather than as a selection-only or history-only surface.
5. Introduce a dedicated generic determinate progress surface rather than
   repurposing the editor-specific `TIndicator`.
6. Extend the existing `TParamText` baseline into a standalone bounded display
   surface with explicit refresh and clipping semantics.
7. Keep the mandatory acceptance slice in `tests/TuiVision.Controls.Tests` and
   defer example smoke coverage to later wave-2 delivery branches.
8. Treat `docs/project-statistics.md` and the rename from
   `Lastenheft_01_ControlsWidgetsAndCollections.md` to
   `Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md`
   as mandatory follow-through surfaces when the feature implementation lands.

## Phase 0 Research Summary

See [research.md](research.md) for full detail. Key planning decisions:

1. Keep the feature entirely in `src/TuiVision.Controls` and
   `tests/TuiVision.Controls.Tests`.
2. Preserve and extend the existing list/scroller/input types in place instead
   of introducing a parallel collection/widget stack.
3. Keep `THistory` and the required clipboard contract session-local and
   in-memory; host clipboard integration remains optional.
4. Model `TComboBox` as an editable input with a visible drop-down session
   backed by shared list primitives.
5. Introduce a new generic `TProgressBar` control for determinate progress and
   leave `TIndicator` editor-specific.
6. Extend the existing `TParamText` baseline into a bounded formatting view
   that refreshes and clips full output on redraw.
7. Put the required acceptance coverage primarily in
   `tests/TuiVision.Controls.Tests` and defer example-smoke proof to later
   example-delivery features.
8. Treat `docs/project-statistics.md` and the rename from
   `Lastenheft_01_ControlsWidgetsAndCollections.md` to
   `Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md`
   as mandatory repository follow-through.

## Phase 1 Design Overview

- `TListViewer`, `TListBox`, `TScrollBar`, and `TScroller` remain the reusable
  backbone for list-oriented navigation, visible range handling, and scroll
  synchronization.
- `THistory` remains the owner of MRU-style session recall buckets, while
  `TFileInputLine` and later `TComboBox` consume those buckets through explicit
  input contracts.
- `ManagedClipboard` remains the required application-internal clipboard
  surface and is strengthened for widget flows without depending on host
  operating-system clipboard access.
- `TComboBox` becomes the reusable composition point for editable input plus a
  visible drop-down list of choices.
- `TProgressBar` becomes the reusable determinate progress surface with a
  numeric range and state transitions for running, completed, and canceled.
- `TParamText` becomes the bounded non-interactive dynamic text surface for
  formatting runtime values and clipping them to bounds.
- Controls-level test coverage owns acceptance for all of the above before any
  later consuming example branch adds smoke tests.

### Responsibility Boundaries

- `TListViewer` and `TListBox` own item focus, visible range, and selection
  semantics for list-based controls.
- `TScrollBar` and `TScroller` own synchronized viewport movement and the
  translation between logical range and visible offset.
- `THistory` owns in-memory MRU buckets keyed by history identifier during the
  active application session only.
- `ManagedClipboard` owns application-internal clipboard contents and transfer
  operations for supported controls; it does not promise host clipboard access.
- `TComboBox` owns editable text state, temporary drop-down visibility, and the
  resulting selected or typed value.
- `TProgressBar` owns numeric progress range, current value, and visible state
  transitions between running, completed, and canceled.
- `TParamText` owns formatting runtime values into a bounded display region and
  clipping output to the available area.
- `tests/TuiVision.Controls.Tests` owns the mandatory acceptance slice for this
  feature; `tests/TuiVision.Examples.SmokeTests` is not expanded in this
  feature.

### Design Boundaries for This Increment

- The feature does not reopen menu/status/window/dialog behavior from
  `008-controls-revision`.
- The feature does not introduce runtime mouse input capture or terminal-side
  mouse event parsing.
- The feature does not deliver or smoke-test the mandatory wave-2 example
  applications themselves.
- Host operating-system clipboard integration is optional and cannot be
  required by acceptance tests.
- History persistence across program restarts is explicitly out of scope.
- Indeterminate progress is optional and cannot be required by acceptance
  tests.
- Editor/help/terminal behavior remains outside this increment, even if some
  affected example names historically touched adjacent concepts.

## Implementation Strategy

1. Add failing MSTest coverage for list, list-box, scroll-bar, and scroller
   behaviors that are still too thin for later wave-2 consumers, especially
   empty/one-item bounds, focus visibility, and scroll synchronization.
2. Add failing MSTest coverage for session-scoped history recall and managed
   clipboard semantics, including empty-state and duplicate-handling paths.
3. Extend test-side fixtures first so that red coverage can be authored for the
   planned widget surfaces without silently turning compile scaffolding into
   early production behavior.
4. Implement the minimal production behavior for list/input/history/clipboard
   hardening first, keeping the acceptance slice in
   `tests/TuiVision.Controls.Tests`.
5. Implement `TComboBox` as an editable input plus visible drop-down list using
   shared list primitives rather than a second ad-hoc choice widget.
6. Implement the determinate `TProgressBar` surface and the bounded
   `TParamText` display behavior with direct focused tests, including the
   first-redraw visibility expectation after state changes.
7. Add explicit framework-first acceptance scenarios for event-loop dispatch,
   focus transitions, menu execution, and dialog interaction before example
   smoke work is allowed to claim the proof surface.
8. Refresh coverage-sweep tests, bilingual XML documentation, and repository
   proof surfaces once the behavioral slice is green.
9. Run build, test, coverage, formatting, and conditional docfx validation
   before the feature is considered ready for tasks/implementation review.

## Scenario & Edge-Case Coverage

### Scenario Matrix

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| List focus, visible range, and scroll synchronization | User Story 1, FR-001, FR-002 | `research.md`, `data-model.md`, `contracts/widgets-collections-api.md`, `TListViewerTests.cs`, `TListBoxTests.cs`, `TScrollBarTests.cs`, `TScrollerTests.cs` |
| Session-scoped history and managed clipboard flows | User Story 1, FR-003, FR-004, FR-004a, FR-004b | `research.md`, `data-model.md`, contract input guarantees, planned `THistoryTests.cs`, planned `TManagedClipboardTests.cs`, `TInputLineTests.cs` |
| Editable combo-box drop-down interaction | User Story 2, FR-005, FR-006 | `research.md`, `data-model.md`, contract combo guarantees, planned `TComboBoxTests.cs` |
| Determinate numeric progress updates | User Story 2, FR-007, FR-008, FR-008a | `research.md`, `data-model.md`, contract progress guarantees, planned `TProgressBarTests.cs` |
| Parameterized text refresh and clipping | User Story 2, FR-009 | `data-model.md`, contract parameter-text guarantees, planned `TParamTextTests.cs` |
| Framework-first acceptance before example smoke tests | User Story 3, FR-010, FR-010a, FR-011 | `plan.md`, `quickstart.md`, `WidgetAcceptanceScenarioTests.cs`, contract test obligations, tasks for follow-through |

### Edge-Case Mapping

- Empty list, single-item list, and undersized bounds are covered by the list
  navigation state model, focused list/scroller tests, and the quickstart
  validation flow.
- Empty clipboard/history actions are covered by the input interaction contract,
  planned history/clipboard tests, and the explicit in-memory semantics.
- Oversized combo choice sets are covered by the combo drop-down session model
  and planned combo-focused tests.
- Immediate completion, cancellation, or stalled progress paths are covered by
  the determinate progress state model and planned progress tests.
- Overlong formatted text and repeated refreshes are covered by the
  parameterized text state model and planned parameter-text tests.
