# Contract: Widgets and Collections API

## Purpose

Define the behavioral contract for the public or externally consumable surface
introduced or revised by the Controls widgets and collections feature. The
contract fixes responsibilities and observable guarantees more strongly than
final internal signatures.

## Public Surface Contract

### `TListViewer`, `TListBox`, `TScrollBar`, and `TScroller`

- Remain the canonical reusable list-navigation and scroll-synchronization
  stack for the Controls layer.
- Keep active item, visible range, and scroll position coherent during keyboard
  navigation and redraws.
- Remain stable for empty collections, single-item collections, and bounds
  smaller than a normal viewport.

### `THistory` and `TFileInputLine`

- `THistory` defines session-scoped MRU buckets keyed by history identifier.
- Recall remains most-recent-first and suppresses duplicate values by moving an
  existing value to the front.
- `TFileInputLine` may consume the same session-scoped history contract without
  adding a second local history implementation.
- Persistence across application restarts is not part of this contract.

### `ManagedClipboard`

- Provides the required application-internal clipboard semantics for supported
  controls.
- Allows copy/cut/paste style widget flows to work without depending on host
  operating-system clipboard support.
- Host clipboard integration may exist later, but it is not part of this
  feature's acceptance contract.

### `TComboBox`

- Represents an editable input plus a visible drop-down choice surface.
- Supports both typed text and explicit item selection.
- Closes its temporary drop-down state predictably and leaves one resulting
  value after acceptance.
- Does not promise multiple distinct combo-box families in this increment.

### `TProgressBar`

- Represents a determinate progress surface with a numeric range.
- Exposes running, completed, and canceled states.
- Does not require indeterminate progress support in this increment.

### `TParamText`

- Represents a non-interactive bounded formatting view for runtime values.
- Refreshes output when values change and clips output to the available bounds.
- Keeps formatting behavior inside the Controls layer rather than pushing it
  into consuming examples.

## Behavioral Guarantees

1. **List coherence guarantee**: List-driven controls keep focus, visible
   range, and scroll state synchronized under normal navigation.
2. **List stability guarantee**: Empty, single-item, and undersized list
   surfaces do not retain stale selection state or overflow their bounds.
3. **Session-history guarantee**: History recall is valid for the active
   application session only and does not require persistence across restarts.
4. **Managed-clipboard guarantee**: Widget clipboard flows work with
   application-internal clipboard semantics and do not require host clipboard
   access for acceptance.
5. **Editable-combo guarantee**: The combo-box surface supports free text
   editing and visible drop-down selection in the same reusable control.
6. **Drop-down-session guarantee**: An open combo drop-down always resolves to
   one consistent resulting value when it closes.
7. **Determinate-progress guarantee**: The generic progress surface supports a
   numeric range and visible running/completed/canceled outcomes.
8. **Parameterized-text guarantee**: Dynamic text refresh and clipping happen in
   the shared framework surface, not as example-local helper behavior.
9. **Framework-first acceptance guarantee**: The mandatory acceptance slice for
   this feature lives primarily in `tests/TuiVision.Controls.Tests`.
10. **Scope guarantee**: This feature does not itself deliver consuming
    example ports, host clipboard requirements, restart-persistent history, or
    mandatory indeterminate progress.

## Test Obligations

- Each behavioral guarantee must be backed by failing MSTest coverage before
  production implementation is added.
- `tests/TuiVision.Controls.Tests/TListViewerTests.cs`,
  `TListBoxTests.cs`, `TScrollBarTests.cs`, and `TScrollerTests.cs` must cover
  list coherence and edge-state behavior.
- Planned `THistoryTests.cs` and `TManagedClipboardTests.cs` must cover
  session-only history and managed clipboard semantics.
- Planned `TComboBoxTests.cs` must cover typed input, drop-down opening,
  navigation, selection, and closure semantics.
- Planned `TProgressBarTests.cs` must cover determinate numeric range updates
  plus running/completed/canceled state transitions.
- Planned `TParamTextTests.cs` must cover refresh and clipping behavior.
- Integration-style widget coverage must continue to run through
  `tests/TuiVision.Controls.Tests`; example smoke expansion is deferred to
  later wave-2 delivery features.
- Public API additions require bilingual XML documentation and docfx-compatible
  comments.
