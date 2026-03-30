# Data Model: Controls Widgets and Collections

## Overview

This feature modifies the in-memory widget interaction model of the Controls
layer. No database or persisted production storage is involved. The data model
focuses on list navigation state, session-scoped input support, editable
combo-box state, determinate progress state, and bounded parameterized text
rendering.

## Entities

### ListNavigationState

- **Purpose**: Represents the active navigation state for one list-driven
  control flow.
- **Key attributes**:
  - Active item index
  - Visible start index
  - Visible row capacity
  - Total item count
  - Linked scroll offset or range snapshot
- **Relationships**:
  - Owned by exactly one list-oriented control (`TListViewer`, `TListBox`, or a
    list-backed temporary combo drop-down)
  - May be synchronized with one `ScrollSyncState`
- **Validation rules**:
  - Active item must always be within the available item range when items exist
  - Empty collections must not retain stale item selection
  - Bounds shrink and growth must recompute visible capacity before the next
    draw

### ScrollSyncState

- **Purpose**: Captures the relationship between logical list range and visible
  scroll position.
- **Key attributes**:
  - Logical minimum
  - Logical maximum
  - Current offset
  - Page size
- **Relationships**:
  - Owned by one `TScrollBar` / `TScroller` pair or one list-driven host
  - May be read by one `ListNavigationState`
- **Validation rules**:
  - Offset must remain clamped to the valid logical range
  - Page-size changes must update scroll representation before the next draw

### HistoryBucketState

- **Purpose**: Represents one MRU recall bucket for a specific history
  identifier during the active application session.
- **Key attributes**:
  - History identifier
  - Ordered values (most recent first)
  - Duplicate-suppression behavior
- **Relationships**:
  - Owned by the in-memory `THistory` store
  - May be consumed by `TFileInputLine` or `TComboBox`
- **Validation rules**:
  - Empty or whitespace-only additions are ignored
  - Re-adding an existing value moves it to the front instead of duplicating it
  - Buckets are valid only for the active application session

### ManagedClipboardState

- **Purpose**: Represents the application-internal clipboard payload available
  to supported controls.
- **Key attributes**:
  - Current clipboard text payload
  - Source action metadata (`copy`, `cut`, or equivalent control-origin marker)
  - Empty/non-empty state
- **Relationships**:
  - Owned by the shared managed clipboard service
  - May be consumed by input-oriented controls
- **Validation rules**:
  - Empty clipboard state must be explicit and testable
  - Clipboard behavior must not require operating-system clipboard availability

### ComboBoxSession

- **Purpose**: Represents one editable combo-box interaction while its drop-down
  may be closed or temporarily open.
- **Key attributes**:
  - Current edited text
  - Ordered choice list
  - Open/closed drop-down state
  - Active choice index when open
  - Selected or committed value
- **Relationships**:
  - Owned by exactly one `TComboBox`
  - References one `ListNavigationState` while the drop-down is open
  - May consume one `HistoryBucketState`
- **Validation rules**:
  - Free text editing remains available even when no choice is currently
    selected
  - The open drop-down state must always point to a valid choice when choices
    exist
  - Closing the drop-down must leave one consistent resulting value

### ProgressState

- **Purpose**: Represents the determinate progress contract for one reusable
  progress display.
- **Key attributes**:
  - Minimum numeric value
  - Maximum numeric value
  - Current numeric value
  - Operational state (`running`, `completed`, `canceled`)
- **Relationships**:
  - Owned by exactly one `TProgressBar`
- **Validation rules**:
  - Current value remains within the configured numeric range
  - `completed` implies the current value reached the terminal range endpoint
  - `canceled` preserves a visible final state without pretending completion

### ParameterizedTextState

- **Purpose**: Represents one bounded formatting surface for runtime text
  values.
- **Key attributes**:
  - Text template or formatting pattern
  - Ordered runtime values
  - Current rendered output
  - Available width and height bounds
- **Relationships**:
  - Owned by exactly one `TParamText`
- **Validation rules**:
  - Rendered output must be reproducible from the template and current values
  - Output must be clipped to the current bounds before drawing
  - Refreshing values must replace stale rendered output on the next draw

## State Transitions

### List Navigation Lifecycle

`empty` / `ready` → `navigating` → `updated`

- `ready` to `navigating`: directional input changes the active item or visible
  range.
- `navigating` to `updated`: the control recomputes visible state and linked
  scroll position for the next draw.
- `updated` to `ready`: the current state becomes the new stable baseline until
  another navigation event arrives.

### History Bucket Lifecycle

`empty` → `populated` → `reordered`

- `empty` to `populated`: the first valid value is added.
- `populated` to `reordered`: an existing value is re-added and moved to the
  front, or a new value is inserted at the front.
- Session end clears all buckets without persistence.

### Managed Clipboard Lifecycle

`empty` → `filled` → `consumed` / `replaced`

- `empty` to `filled`: a supported control copies or cuts text into the managed
  clipboard.
- `filled` to `consumed`: a supported control pastes the current payload.
- `filled` to `replaced`: a later copy/cut action overwrites the payload.

### Combo Box Lifecycle

`editing` ↔ `drop-down-open` → `committed`

- `editing` to `drop-down-open`: the combo box opens its choice list.
- `drop-down-open` to `drop-down-open`: navigation changes the active choice.
- `drop-down-open` to `committed`: the selected choice is accepted and becomes
  the resulting value.
- `editing` to `committed`: the user keeps an edited text value without
  choosing a drop-down item.

### Progress Lifecycle

`running` → `completed` / `canceled`

- `running` to `running`: the current numeric value advances within range.
- `running` to `completed`: the current value reaches the terminal endpoint.
- `running` to `canceled`: the operation stops before completion and exposes a
  visible canceled state.

### Parameterized Text Lifecycle

`stale` → `formatted` → `clipped`

- `stale` to `formatted`: updated runtime values are formatted into output text.
- `formatted` to `clipped`: output is cropped to the current bounds for draw.
- Any value or bounds change returns the state to `stale` before the next draw.
