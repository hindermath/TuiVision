# Contract: Application Shell API

## Purpose

Define the behavioral contract for the new application-shell public surface introduced by this feature. This contract describes required responsibilities and observable guarantees, not final internal implementation details.

This contract intentionally fixes behavior and responsibility boundaries more strongly than exact signatures. Example code or planned file names may illustrate likely seams, but they do not automatically define final public member names unless that commitment is stated here.

## Public Surface Contract

### `TProgram`

- Acts as the root shell coordinator for an interactive text application.
- Owns the shell layout regions or references to them.
- Starts in a non-interactive state and transitions to an interactive state only after a valid focus target exists.
- Accepts global commands and routes them exactly once per user invocation.
- Shuts down the shell in a controlled order when an exit action is requested.
- Does not own menu-item rendering details or workspace-child hosting rules directly when those responsibilities belong to specialized shell views.

### `TApplication`

- Specializes `TProgram` as the convenience application entry point.
- Automatically creates a usable default shell that includes:
  - menu bar
  - desktop workspace
  - status line
- Allows consumers to customize or replace those shell regions without rebuilding the whole application frame model.
- The exact customization API shape remains open during planning; acceptable designs include overridable methods, builders, or other narrow extension seams as long as the default-shell and customization guarantees are preserved.

### `TDesktop`

- Hosts zero or more child views or window-like workspace items.
- Maintains or restores a valid focus target when children are inserted, activated, removed, or closed.
- Remains usable even when no child items are present.
- When the active child is removed, tries to activate the next eligible child first and falls back to the desktop itself only if no eligible child remains.

### `TMenuBar`

- Displays global actions to the user in a stable order.
- Can expose enabled and disabled actions.
- Keeps disabled actions visible while preventing their execution.
- Routes chosen actions through the shared shell command path rather than a separate execution path.
- Owns action presentation, not independent business execution.
- Supports nested submenus via a `TMenuBar → TMenu → TSubMenu` hierarchy. Keyboard navigation contract: F10 or Alt activates the menu bar; left/right arrow moves between top-level items; down arrow opens a menu; up/down arrow moves within an open menu; right arrow or Enter on a submenu item opens the next level; Escape or left arrow closes the current level and returns to the parent.

### `TStatusLine`

- Displays shortcut-oriented global actions and context hints.
- Mirrors shared shell command behavior with the menu bar.
- Keeps disabled actions visible while preventing their execution.
- Owns shortcut/context presentation, not an alternative execution path.
- Receives focus-change notifications through the shell's `TEvent` dispatch path; on each focus-change event, re-reads the newly focused view's declared status hints and updates its visible shortcut set synchronously before the next rendered frame. No polling or separate observer wiring is required.

## Behavioral Guarantees

1. **Default shell guarantee**: Creating a default `TApplication` yields a usable shell without extra assembly code.
2. **Shared command guarantee**: A command exposed through multiple shell surfaces produces the same outcome regardless of entry point.
3. **Single execution guarantee**: One user invocation results in at most one command execution.
4. **Visibility guarantee**: Unavailable global actions remain visible and are shown as disabled.
5. **Focus recovery guarantee**: Closing or removing the active desktop child never leaves the shell without a valid interactive focus target.
6. **Scope guarantee**: The API introduced in this increment does not require dialog widgets, form controls, or specialized window classes.

## Test Obligations

- Each contract guarantee must be backed by MSTest coverage before the corresponding production implementation is added.
- Public API additions require bilingual XML documentation and docfx-compatible comments.
