# Research: Controls Widgets and Collections

## Decision 1: Keep the feature inside the existing Controls and Controls.Tests projects

- **Decision**: Implement the feature entirely in `src/TuiVision.Controls` and
  `tests/TuiVision.Controls.Tests`.
- **Rationale**: The missing behavior belongs to reusable control primitives.
  A new module would violate the constitution's architecture principle without
  creating a meaningful new boundary.
- **Alternatives considered**:
  - Introduce a separate widget/collections module: rejected because the
    repository is constrained to five framework modules.
  - Move some logic into examples immediately: rejected because the spec
    explicitly treats this feature as framework preparation before wave-2
    example delivery.
  - Pull runtime mouse-support into the same feature: rejected because the
    updated requirements keep terminal-side mouse capture as a separate later
    interaction/driver block instead of mixing it into widget preparation.

## Decision 2: Strengthen list, list-box, scroll-bar, and scroller behavior in place

- **Decision**: Keep `TListViewer`, `TListBox`, `TScrollBar`, and `TScroller`
  as the canonical list-navigation stack and harden them in place instead of
  introducing a parallel collection-view subsystem.
- **Rationale**: These types already form the core reusable list contract. The
  current gap is behavioral thinness, not missing ownership.
- **Alternatives considered**:
  - Add a second list subsystem for examples only: rejected because it would
    create the exact duplication this feature is supposed to prevent.
  - Treat `TListBox` as sufficient without `TListViewer` or `TScroller`
    changes: rejected because the spec calls out synchronized range and scroll
    behavior across the whole list stack.

## Decision 3: Keep history and clipboard semantics session-local and in-memory

- **Decision**: Preserve `THistory` as an in-memory MRU bucket system and
  implement the required clipboard contract as application-internal managed
  clipboard semantics for the active application session only.
- **Rationale**: The clarifications explicitly chose session-only history and
  non-required host clipboard integration. This keeps the feature inside its
  widget scope and avoids premature persistence or OS integration work.
- **Alternatives considered**:
  - Persist history across restarts: rejected because the clarification made
    restart persistence out of scope.
  - Require host clipboard access: rejected because the feature must remain
    cross-platform and testable without OS-specific dependencies.

## Decision 4: Model `TComboBox` as editable input plus visible drop-down list

- **Decision**: Introduce `TComboBox` as a reusable control that combines free
  text editing with a temporary visible drop-down list backed by shared list
  primitives.
- **Rationale**: The spec clarification explicitly chose an editable combo with
  a visible drop-down as the minimum required behavior. This is the smallest
  shape that satisfies both authoring needs and later wave-2 example reuse.
- **Alternatives considered**:
  - Selection-only combo box: rejected by clarification.
  - History-only input without a real drop-down list: rejected by
    clarification.
  - Deliver both editable and selection-only variants immediately: rejected as
    unnecessary scope growth for this feature.

## Decision 5: Introduce a dedicated determinate `TProgressBar`

- **Decision**: Add a generic `TProgressBar` control for determinate numeric
  progress and keep the existing `TIndicator` editor-specific.
- **Rationale**: `TIndicator` already has a clear editor-status role. Reusing it
  as a generic progress surface would blur responsibilities and make later
  example reuse harder to understand.
- **Alternatives considered**:
  - Reuse `TIndicator` for progress display: rejected because it is tightly
    coupled to `TEditor`.
  - Require indeterminate progress in this feature too: rejected because the
    clarification chose determinate numeric progress only as the mandatory
    acceptance path.

## Decision 6: Introduce `TParamText` as a bounded formatting view

- **Decision**: Add `TParamText` as a standalone non-interactive view that
  formats runtime values into text on redraw and clips the rendered output to
  the declared bounds.
- **Rationale**: The spec requires dynamic parameterized text behavior for
  `dyntxt` and later demo usage, but it does not require a separate template
  engine or persisted formatting language.
- **Alternatives considered**:
  - Render dynamic text through ad-hoc helper methods inside examples:
    rejected because it would bypass the shared framework surface.
  - Fold parameterized text into `TStaticText`: rejected because runtime
    refresh semantics would become implicit and harder to test.

## Decision 7: Keep the acceptance surface primarily in Controls.Tests

- **Decision**: Treat `tests/TuiVision.Controls.Tests` as the primary mandatory
  acceptance surface for this feature and defer consuming example smoke tests to
  later wave-2 delivery branches.
- **Rationale**: The clarification explicitly selected a framework-first test
  strategy. This cleanly separates core widget hardening from the later example
  ports that consume those widgets.
- **Alternatives considered**:
  - Put the primary acceptance burden on `tests/TuiVision.Examples.SmokeTests`:
    rejected because it would tie framework acceptance to not-yet-delivered
    examples.
  - Create a separate feature-only test project: rejected because the existing
    Controls test project already owns the correct behavioral boundary.

## Decision 8: Keep proof surfaces and Lastenheft traceability in the same delivery flow

- **Decision**: Treat `docs/project-statistics.md` as a mandatory follow-through
  surface for this planning/implementation work and rename
  `Lastenheft_01_ControlsWidgetsAndCollections.md` to
  `Lastenheft_01_ControlsWidgetsAndCollections_009-controls-widgets-and-collections.md`
  when the feature implementation lands.
- **Rationale**: The repository governance explicitly requires both statistics
  maintenance and branch-traceable Lastenheft naming.
- **Alternatives considered**:
  - Leave statistics and Lastenheft rename to a later cleanup step: rejected
    because the governance rules require these surfaces to remain synchronized
    with the actual work item.
