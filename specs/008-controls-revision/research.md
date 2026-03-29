# Research: Controls Revision

## Decision 1: Keep the revision inside the existing Controls and Controls.Tests projects

- **Decision**: Implement the feature entirely in `src/TuiVision.Controls` and
  `tests/TuiVision.Controls.Tests`.
- **Rationale**: The missing behavior belongs to existing shell/control types.
  A new module would violate the constitution's architecture principle without
  adding a real behavioral boundary.
- **Alternatives considered**:
  - Introduce a separate shell/menu module: rejected because the repository is
    constrained to five framework modules.
  - Move parts into `TuiVision.Compatibility`: rejected because the behavior is
    UI interaction, not compatibility translation.

## Decision 2: Introduce `TSubMenu` as a standalone declaration node, not as a full runtime view hierarchy

- **Decision**: Add `TSubMenu` as a standalone declaration type in
  `src/TuiVision.Controls/TSubMenu.cs`, keep it compatible with the historical
  `tvguid02` menu-building style, and continue to let `TMenuBar` own runtime
  execution and popup rendering.
- **Rationale**: The specification requires a standalone submenu type and
  unchanged declaration ergonomics, but it does not require a full `TMenuView`
  or `TMenuBox` subsystem. A declaration-first type restores authoring
  compatibility without widening scope.
- **Alternatives considered**:
  - Keep only `TMenuItem.SubMenu`: rejected because it does not satisfy the
    explicit `TSubMenu` acceptance target.
  - Recreate the full original `TMenuView` + `TMenuBox` class graph now:
    rejected because mouse tracking and deeper hierarchy support are out of
    scope.

## Decision 3: Support exactly one submenu level and recompute top-level layout slots on every bounds change

- **Decision**: Model the menu bar as one top-level chain whose entries may each
  own at most one direct submenu, and recompute cached top-level layout slots
  whenever bounds change through `Locate()`/`Resize()`-driven hooks.
- **Rationale**: The clarified spec limits nesting to exactly one submenu level.
  Recomputable layout slots solve the resize requirement without inventing a new
  overflow-navigation model.
- **Alternatives considered**:
  - Allow deeper recursive nesting: rejected by clarification and test-scope
    risk.
  - Keep one-time column calculations from startup only: rejected because it
    fails the resize acceptance behavior.

## Decision 4: Make submenu navigation wrap and skip non-actionable entries

- **Decision**: Directional submenu navigation wraps at both ends and silently
  skips separators or disabled entries, always landing on the next actionable
  entry.
- **Rationale**: This matches the clarified interaction contract and avoids dead
  focus states.
- **Alternatives considered**:
  - Stop at the first/last actionable entry: rejected because wrap-around was
    explicitly clarified.
  - Allow focus to land on disabled/separator items: rejected because those
    entries cannot execute and would create ambiguous selection behavior.

## Decision 5: Use explicit `TStatusDef` range matching with inclusive bounds, first-match-wins ordering, and neutral empty fallback

- **Decision**: `TStatusDef` maps one inclusive help-context range to one
  status-action chain. `TStatusLine` resolves definitions in declaration order;
  the first matching definition wins, and no match yields a neutral empty
  status line.
- **Rationale**: Inclusive range matching aligns with the historical
  `TStatusDef` intent, while the clarified first-match and no-match rules make
  behavior deterministic and testable.
- **Alternatives considered**:
  - Narrowest-range-wins selection: rejected because it adds hidden precedence
    rules the spec did not choose.
  - Treat overlaps as invalid input only: rejected because the clarified
    contract explicitly chose first-match-wins instead.

## Decision 6: Add an explicit help-context surface to `TView` and keep direct hint fallback only as a compatibility bridge

- **Decision**: Add a shell-readable help-context surface to `TView` and let
  `TStatusLine` use that surface for `TStatusDef` matching. Preserve the older
  focused-view `GetStatusHints()` path only when a `TStatusLine` instance was
  created without any explicit status definitions.
- **Rationale**: The new status model needs explicit context routing. The
  compatibility bridge avoids breaking already-ported callers such as
  `TEditor` and `TEditWindow` while keeping the clarified `TStatusDef`
  behavior authoritative once definitions are configured.
- **Alternatives considered**:
  - Infer help context from existing hints: rejected because it keeps routing
    implicit and under-specified.
  - Replace all direct-hint callers immediately: rejected because it would force
    unrelated feature work into this revision.

## Decision 7: Introduce `[Flags] WindowFlags` with only `Close` and `Move` in scope

- **Decision**: Add a focused `WindowFlags` enum and implement only the `Close`
  and `Move` bits in this revision.
- **Rationale**: The spec requires closable and movable windows only. A flags
  enum keeps the API aligned with the historical model without reopening
  `Zoom`/`Grow` scope.
- **Alternatives considered**:
  - Add separate boolean constructor parameters or properties: rejected because
    the spec and historical model are flag-oriented.
  - Implement `Zoom` and `Grow` too: rejected as out of scope.

## Decision 8: Model move mode as a transient preview session entered via `Ctrl+F5`

- **Decision**: `Ctrl+F5` enters window move mode, arrow keys preview movement,
  `Enter` commits the new position, and `Escape` restores the original bounds.
- **Rationale**: This exactly matches the clarified interaction contract and is
  easy to test as a reversible state transition.
- **Alternatives considered**:
  - Let `Escape` end move mode while keeping the previewed position: rejected by
    clarification.
  - Use immediate move-on-arrow without an explicit mode: rejected because the
    historical contract and the spec require an explicit move mode.

## Decision 9: Extend `TDialog` with `Valid(ushort command)` and preserve the existing modal result flow

- **Decision**: Add a validation hook that runs before a close command is
  accepted, but keep `Run()` returning `ushort` and keep the existing modal loop
  shape.
- **Rationale**: The current dialog already returns command results. The missing
  behavior is validation before close, not a new modal execution model.
- **Alternatives considered**:
  - Redesign the dialog to return a richer result object: rejected because the
    spec only requires a distinct command result, which already exists.
  - Handle validation only in individual buttons: rejected because closure can
    also be requested through keyboard and command events.

## Decision 10: Update proof surfaces in the same change as behavior

- **Decision**: Treat `docs/porting-status.md`, `Pflichtenheft.md`, and
  `docs/project-statistics.md` as mandatory follow-through artifacts for the
  implementation phase, including moving the prominent
  `>>> NAECHSTER SCHRITT <<<` marker when the completed increment changes the
  highest-priority remaining work item.
- **Rationale**: The background problem is specifically that proof status
  overstated the delivered behavior. The feature is incomplete if the proof
  surfaces remain stale.
- **Alternatives considered**:
  - Update proof documents later: rejected because it repeats the current drift
    problem.
  - Limit updates to tests only: rejected because repository-visible proof is an
    explicit requirement.
