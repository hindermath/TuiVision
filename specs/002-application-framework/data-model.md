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
  - Ordered collection of `MenuAction`
  - Visibility state
  - Current highlighted item (if applicable to interaction model)
- **Relationships**:
  - Belongs to exactly one `ApplicationShell`
  - References zero to many `MenuAction` entries
- **Validation rules**:
  - Visible actions must preserve order
  - Disabled actions remain visible but non-executable

### StatusLine

- **Purpose**: Exposes shortcut-oriented global actions and current context hints.
- **Key attributes**:
  - Ordered collection of `StatusAction`
  - Visibility state
  - Current context label or shortcut set
- **Relationships**:
  - Belongs to exactly one `ApplicationShell`
  - References zero to many `StatusAction` entries
- **Validation rules**:
  - Disabled actions remain visible but non-executable
  - Shared commands must align with menu-defined behavior

### CommandBinding

- **Purpose**: Describes a shared shell command that may be surfaced through menu, status line, or keyboard interaction.
- **Key attributes**:
  - Command identifier
  - User-visible label
  - Availability state
  - Optional shortcut representation
  - Optional routing target / handler information
- **Relationships**:
  - May be referenced by one or many `MenuAction`
  - May be referenced by one or many `StatusAction`
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
