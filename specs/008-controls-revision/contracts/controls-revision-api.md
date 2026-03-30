# Contract: Controls Revision API

## Purpose

Define the behavioral contract for the public or externally consumable surface
introduced or revised by the Controls revision. The contract fixes
responsibilities and observable guarantees more strongly than final internal
signatures.

## Public Surface Contract

### `TMenuItem` and `TSubMenu`

- Represent declaration-time menu nodes for the top-level menu bar and its one
  direct submenu level.
- Support historical declaration ergonomics strongly enough that the
  `tvguid02`-style submenu-building example compiles without source changes.
- Distinguish actionable entries from disabled entries and separator-only
  entries.
- Do not promise deeper recursive submenu trees in this increment.

### `TMenuBar`

- Owns menu activation, top-level wrap-around navigation, submenu selection, and
  command dispatch for the supported one-level hierarchy.
- Keeps the currently focused submenu entry visually distinct.
- Recomputes top-level layout placement when its bounds change so visible menu
  titles stay aligned to the current width.
- Silently skips disabled or separator submenu entries during directional
  navigation.

### `TStatusDef`

- Defines one inclusive help-context range and one ordered status-action set.
- Participates in declaration-order routing where the first matching definition
  wins.

### `TStatusLine`

- Resolves status actions from configured `TStatusDef` entries against the
  active help context.
- Falls back to a neutral empty presentation when definitions exist but no
  definition matches.
- May consume direct focused-view hint chains only as a compatibility bridge
  when no explicit `TStatusDef` configuration was supplied.
- Keeps status actions visible as presentation of the active command context;
  command execution still routes through the normal application command path.

### `TView`

- Exposes a shell-readable help-context value that allows focused-shell surfaces
  such as `TStatusLine` to resolve the current context explicitly.

### `WindowFlags` and `TWindow`

- `WindowFlags` defines the enabled interactive window capabilities for this
  increment and includes only `Close` and `Move`.
- `TWindow` displays a visible close affordance when `Close` is enabled.
- `TWindow` supports close through `Ctrl+W` and through `Escape` only when no
  focused child consumed the key first.
- `TWindow` supports move mode through `Ctrl+F5`, directional preview movement,
  `Enter` commit, and `Escape` restore when `Move` is enabled.
- `TWindow` does not promise zoom, grow, or mouse-driven manipulation in this
  increment.

### `TDialog`

- Evaluates whether a closing command is valid before accepting the modal close.
- Keeps the dialog open when validation rejects the requested close command.
- Returns an explicit `ushort` modal result when validation accepts the close.
- Preserves the existing synchronous modal execution model.

## Behavioral Guarantees

1. **Directional menu guarantee**: Users can navigate the active menu bar and
   the open submenu with directional keys alone.
2. **Actionable-entry guarantee**: Directional submenu navigation never leaves
   focus on disabled or separator-only entries.
3. **Selection-visibility guarantee**: The currently focused submenu entry is
   always visually distinguishable from non-focused entries.
4. **Hierarchy guarantee**: This revision supports exactly one submenu level
   under top-level entries and does not promise deeper recursive submenu trees.
5. **Status-routing guarantee**: When multiple `TStatusDef` entries match, the
   first declared definition wins.
6. **Neutral-fallback guarantee**: When status definitions exist but none match
   the active help context, the status line shows no stale context actions.
7. **Compatibility-bridge guarantee**: Existing direct focused-view hint chains
   may continue to drive the status line only when no explicit status
   definitions were configured.
8. **Window-close guarantee**: Closable windows expose a visible affordance and
   accept `Ctrl+W` plus guarded `Escape` through the normal event pipeline.
9. **Window-move guarantee**: Movable windows use reversible move mode entered
   by `Ctrl+F5`, committed by `Enter`, and canceled by `Escape`.
10. **Dialog-validation guarantee**: Invalid close requests do not close the
    dialog; valid close requests return an explicit modal result.
11. **Resize-layout guarantee**: Menu layout remains consistent with current
    bounds after resize-driven relayout.
12. **Scope guarantee**: This revision does not require terminal-mouse support,
    streaming, palette customization, zoom/grow behavior, or new example-wave
    delivery.

## Test Obligations

- Each behavioral guarantee must be backed by failing MSTest coverage before
  production implementation is added.
- `tests/TuiVision.Controls.Tests/TMenuBarTests.cs` must cover wrap-around
  navigation, skip-over behavior, selection visibility, confirmation, and
  layout recomputation.
- `tests/TuiVision.Controls.Tests/TStatusLineTests.cs` must cover definition
  routing, first-match ordering, neutral fallback, and compatibility fallback.
- `tests/TuiVision.Controls.Tests/TWindowTests.cs` must cover close affordance,
  `Ctrl+W`, guarded `Escape`, move-mode entry, commit, and restore.
- `tests/TuiVision.Controls.Tests/TDialogTests.cs` must cover rejected and
  accepted close validation paths without regressing existing modal behavior.
- Integration-style shell coverage must continue to demonstrate event loop,
  focus transitions, menu execution, and dialog interaction as required by the
  constitution.
- Public API additions require bilingual XML documentation and docfx-compatible
  comments.
