# Data Model: Application Framework Shell

## Overview

This feature has no persistent storage model. Its data model is an in-memory interaction model that describes shell regions, action descriptors, and focus ownership inside a running text application.

## Entities

### ApplicationShell

- **Purpose**: Represents the running top-level shell that coordinates startup, shutdown, layout regions, global command handling, and active focus ownership.
- **Key attributes**:
  - Shell lifecycle state (`created`, `initialized`, `interactive`, `shutting-down`, `terminated`)
  - Root bounds / current frame geometry
  - References to menu bar, desktop workspace, and status line
  - Current active command target
- **Relationships**:
  - Owns exactly one `DesktopWorkspace`
  - Owns zero or one `MenuBar`
  - Owns zero or one `StatusLine`
  - Routes commands to zero or one `ActiveWorkspaceItem`

### DesktopWorkspace

- **Purpose**: Hosts child views or window-like workspace items and provides focus fallback behavior.
- **Key attributes**:
  - Child collection inherited from `TGroup`
  - Current active workspace item
  - Workspace bounds
- **Relationships**:
  - Belongs to exactly one `ApplicationShell`
  - Contains zero to many `ActiveWorkspaceItem` candidates
- **Validation rules**:
  - Must remain valid even with zero children
  - Must always provide a valid focus target after child activation or removal
  - On active-child removal, fallback order is: next eligible child first, desktop workspace second

### MenuBar

- **Purpose**: Provides visible global navigation and command entry points.
- **Key attributes**:
  - Ordered collection of top-level menu items; each item may reference a nested `TMenu` or `TSubMenu`, forming a `TMenuBar → TMenu → TSubMenu` hierarchy
  - Visibility state
  - Current highlighted item (if applicable to interaction model)
- **Menu hierarchy and keyboard navigation**:
  - Left/right arrow keys move between top-level menu bar items
  - Down arrow (or Enter) on a top-level item opens its menu
  - Up/down arrow keys move between items within an open menu
  - Right arrow (or Enter) on a submenu item opens the next nesting level
  - Escape or left arrow closes the current submenu level and returns to the parent
  - Nesting depth is not bounded by this increment; practical usage follows the original Turbo Vision convention of 2–3 levels
- **Relationships**:
  - Belongs to exactly one `ApplicationShell`
  - References zero to many `MenuAction` entries, which may themselves contain nested `TMenu`/`TSubMenu` structures
- **Validation rules**:
  - Visible actions must preserve order at every nesting level
  - Disabled actions remain visible but non-executable at any nesting level

### StatusLine

- **Purpose**: Exposes shortcut-oriented global actions and context hints; automatically reflects the focused view's declared hints on each focus change.
- **Key attributes**:
  - Ordered collection of `TStatusItem`
  - Visibility state
  - Current context label or shortcut set
- **Focus-notification mechanism**: `TStatusLine` receives focus-change notifications through the shell's existing event dispatch path. When `TProgram` processes a focus-change event, it propagates a focus-updated notification to `TStatusLine`, which then re-reads the newly focused view's declared status hints and replaces its current shortcut set. No polling or separate observer registration is required; the notification travels through the same event routing that all shell views use for `TEvent` handling.
- **Status-hint declaration**: A `TView` declares its available status hints by implementing a virtual `GetStatusHints()` method that returns an ordered collection of `TStatusItem` descriptors. `TStatusLine` invokes this method on the currently focused view to determine its content.
- **Relationships**:
  - Belongs to exactly one `ApplicationShell`
  - References zero to many `TStatusItem` entries derived from the focused view's current hints
- **Validation rules**:
  - Disabled actions remain visible but non-executable
  - Shared commands must align with menu-defined behavior
  - Status hints must be refreshed synchronously with each focus change before the next interactive frame

### CommandBinding

- **Purpose**: Describes a shared shell command that may be surfaced through menu, status line, or keyboard interaction.
- **Key attributes**:
  - Command identifier: a `const int cmXxx` integer constant defined in `ShellCommandIds`; all routing, availability checks, and binding comparisons use integer equality
  - User-visible label
  - Availability state
  - Optional shortcut representation
  - Optional routing target / handler information
- **Relationships**:
  - May be referenced by one or many `TMenuItem`
  - May be referenced by one or many `TStatusItem`
  - Is executed through `ApplicationShell`
- **Validation rules**:
  - A single user invocation maps to one command execution
  - Disabled commands cannot execute
  - Menu, status line, and equivalent keyboard entry points resolve through the same conceptual command binding

### ActiveWorkspaceItem

- **Purpose**: Represents the currently focused child view hosted by the desktop workspace.
- **Key attributes**:
  - View reference
  - Activation state
  - Focus eligibility
- **Relationships**:
  - Belongs to `DesktopWorkspace`
  - May become the active command target
- **Validation rules**:
  - When removed or closed, focus must move to the next eligible item or fall back to the desktop

## State Transitions

### Shell Lifecycle

`created` → `initialized` → `interactive` → `shutting-down` → `terminated`

- `created` to `initialized`: shell regions are created and inserted.
- `initialized` to `interactive`: the shell has a valid focus target and accepts global commands without requiring additional user setup steps.
- `interactive` to `shutting-down`: an exit command is accepted.
- `shutting-down` to `terminated`: resources are released in controlled order, and shell-owned region references are cleared.

### Command Availability

`enabled` ↔ `disabled`

- Disabled commands remain visible in menu and status line.
- Transition to `disabled` blocks execution but not display.
- Transition to `enabled` restores normal execution without changing command identity.

### Desktop Focus

`no-active-child` ↔ `child-active`

- Startup may begin in `no-active-child`.
- Inserting and activating a child moves to `child-active`.
- Closing the active child first attempts to activate the next eligible child in workspace order.
- If no eligible child remains, the workspace returns to `no-active-child` and the desktop itself becomes the valid focus target.
