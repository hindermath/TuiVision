# Data Model: Controls Revision

## Overview

This feature modifies the in-memory interaction model of the Controls layer. No
database or persisted production storage is involved. The data model focuses on
menu-session state, context-driven status resolution, window move/close state,
and dialog close validation.

## Entities

### MenuBarSession

- **Purpose**: Represents one active or inactive menu-bar interaction flow.
- **Key attributes**:
  - Menu-active state (`inactive`, `top-level-active`, `submenu-open`)
  - Selected top-level entry
  - Optional open submenu reference
  - Optional selected submenu entry
  - Cached layout slots for visible top-level entries
  - Pending command dispatch or dismissal intent
- **Relationships**:
  - Owned by exactly one `TMenuBar`
  - References zero or one `SubmenuDefinition`
  - Owns one or more `MenuLayoutSlot` entries while a menu definition exists
- **Validation rules**:
  - When menu state is active, there is always a selected actionable top-level
    entry
  - When a submenu is open, the selected submenu entry must always be actionable
  - Resize-driven layout recalculation must rebuild layout slots before the next
    draw

### MenuLayoutSlot

- **Purpose**: Captures the computed visible placement of one top-level menu
  entry after the latest bounds calculation.
- **Key attributes**:
  - Owning top-level entry
  - Start column
  - Rendered width
  - Visible/clipped state
- **Relationships**:
  - Belongs to exactly one `MenuBarSession`
- **Validation rules**:
  - Slots are recomputed whenever the menu bar bounds change
  - A clipped slot may still define the entry's logical selection order even if
    part of the title is not visible

### SubmenuDefinition

- **Purpose**: Represents one reusable submenu branch under a top-level menu
  entry.
- **Key attributes**:
  - Display label
  - Ordered child entry chain
  - Mnemonic metadata
  - One-level nesting boundary marker
- **Relationships**:
  - Owned by exactly one top-level menu entry
  - Contains zero or more declaration items, each of which may be actionable,
    disabled, or separator-only
- **Validation rules**:
  - Submenus may appear only directly below top-level entries
  - Deeper recursive submenu nesting is invalid for this feature

### StatusContextDefinition

- **Purpose**: Defines how one inclusive help-context range maps to one visible
  status-action chain.
- **Key attributes**:
  - Inclusive minimum help context
  - Inclusive maximum help context
  - Ordered status-action chain
  - Declaration order
- **Relationships**:
  - Owned by exactly one `TStatusLine` configuration
- **Validation rules**:
  - The first declared matching definition wins when ranges overlap
  - No matching definition produces a neutral empty status line

### HelpContextSource

- **Purpose**: Represents the shell-readable help context exposed by the focused
  view.
- **Key attributes**:
  - Current help-context value
  - Focus ownership state
  - Optional legacy direct-hint chain for compatibility fallback
- **Relationships**:
  - Belongs to one focused `TView`
  - May be consumed by one `TStatusLine`
- **Validation rules**:
  - The help-context value must remain readable without requiring hint
    inspection
  - Legacy direct-hint fallback is only used when the status line has no
    explicit `StatusContextDefinition` configuration

### StatusResolutionState

- **Purpose**: Represents the current visible status-line outcome.
- **Key attributes**:
  - Resolution mode (`definitions`, `legacy-fallback`, `neutral-empty`)
  - Matched definition, if any
  - Visible action chain
- **Relationships**:
  - Owned by exactly one `TStatusLine`
  - May reference one `StatusContextDefinition`
  - May reference one focused `HelpContextSource`
- **Validation rules**:
  - A `definitions` result must reference the first matching definition only
  - A `neutral-empty` result must not keep stale actions from the previous focus

### WindowInteractionSession

- **Purpose**: Represents the interactive state of one window regarding close
  and move behavior.
- **Key attributes**:
  - Window flags (`Close`, `Move`)
  - Current bounds
  - Optional original bounds snapshot for move mode
  - Move-mode state (`idle`, `previewing`)
  - Close-affordance visibility
- **Relationships**:
  - Owned by exactly one `TWindow`
- **Validation rules**:
  - `Escape` closes the window only when no focused child consumed the key first
  - `Enter` commits the previewed bounds in move mode
  - `Escape` in move mode restores the original bounds snapshot

### DialogCloseRequest

- **Purpose**: Represents one attempt to close a dialog with a specific command.
- **Key attributes**:
  - Requested command
  - Validation result (`accepted`, `rejected`)
  - Result state (`open`, `closed`)
  - Returned modal result when accepted
- **Relationships**:
  - Owned by exactly one `TDialog`
- **Validation rules**:
  - Rejected requests keep the dialog open and preserve dialog state
  - Accepted requests return one explicit `ushort` command result

## State Transitions

### Menu Interaction Lifecycle

`inactive` → `top-level-active` → `submenu-open` → `command-dispatched` /
`dismissed`

- `inactive` to `top-level-active`: menu activation key opens the top-level
  interaction and selects one actionable top-level entry.
- `top-level-active` to `submenu-open`: the selected top-level entry opens its
  direct submenu.
- `submenu-open` to `submenu-open`: directional navigation wraps and skips
  non-actionable entries while changing the selected submenu entry.
- `submenu-open` to `command-dispatched`: `Enter` or the focused entry's
  mnemonic dispatches the selected command.
- `top-level-active` or `submenu-open` to `dismissed`: `Escape` or a toggle
  deactivates the menu interaction.

### Status Resolution Lifecycle

`focus-changed` → `definitions` / `legacy-fallback` / `neutral-empty`

- `focus-changed` to `definitions`: one or more `StatusContextDefinition`
  entries are configured and the first matching range is selected.
- `focus-changed` to `neutral-empty`: definitions are configured, but none match
  the active help context.
- `focus-changed` to `legacy-fallback`: no definitions are configured and the
  focused view exposes a direct hint chain.

### Window Move Lifecycle

`idle` → `previewing` → `committed` / `restored`

- `idle` to `previewing`: `Ctrl+F5` enters move mode and stores the original
  bounds snapshot.
- `previewing` to `previewing`: directional input adjusts the previewed window
  bounds.
- `previewing` to `committed`: `Enter` accepts the previewed bounds.
- `previewing` to `restored`: `Escape` restores the original bounds and exits
  move mode.

### Dialog Close Lifecycle

`open` → `close-requested` → `rejected` / `accepted`

- `open` to `close-requested`: a close-capable command path reaches the dialog.
- `close-requested` to `rejected`: `Valid(ushort command)` returns false and the
  dialog remains open.
- `close-requested` to `accepted`: validation passes and the dialog returns the
  requested modal result.
