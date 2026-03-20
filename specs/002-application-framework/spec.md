# Feature Specification: Application Framework Shell

**Feature Branch**: `[002-application-framework]`  
**Created**: 2026-03-20  
**Status**: Ready for Planning  
**Input**: User description: "Bitte setze aus der Datei Pflichtenheft.md in Abschnitt 8.1 Nr. 4 Anwendungsrahmen: `TProgram`, `TApplication`, Menues, Statuszeile, Desktop um."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Launch a complete application shell (Priority: P1)

As a framework consumer, I want to start a text-based application that immediately provides a top-level application frame with menu bar, desktop area, and status line so that the application is usable from the first screen.

**Why this priority**: This is the foundation of the application framework. Without a usable shell, later features such as dialogs, editors, and examples cannot run in a coherent application context.

**Independent Test**: Create a representative application with a basic menu, an empty desktop, and a status line. Launching it must present all three regions in a usable layout and accept focus and commands without additional setup steps after startup.

**Acceptance Scenarios**:

1. **Given** an application defines a menu bar, desktop, and status line, **When** the application starts, **Then** the first usable screen shows those regions in a stable frame layout.
2. **Given** the application has no child windows open, **When** the shell becomes interactive, **Then** the desktop remains available as the active workspace and the shell continues accepting global commands.

---

### User Story 2 - Trigger global actions from menus and status line (Priority: P2)

As an end user, I want to trigger application-level actions from either the menu system or the status line so that navigation and common commands remain discoverable and consistent.

**Why this priority**: Menus and the status line are the main affordances for orientation and command discovery in a Turbo Vision style application. They make the shell practical for real applications and learning examples.

**Independent Test**: Configure at least one shared command that appears in the menu system and on the status line. The command must produce the same observable result no matter which entry point the user chooses.

**Acceptance Scenarios**:

1. **Given** a command is available in the menu system, **When** the user selects it, **Then** the corresponding application action is executed once and the shell remains interactive.
2. **Given** the same command is also exposed through the status line, **When** the user invokes it from the status line, **Then** the resulting behavior matches the menu-triggered behavior.

---

### User Story 3 - Work within the desktop area (Priority: P3)

As an end user, I want the desktop area to host and manage application content so that windows and work surfaces can coexist inside a stable outer frame.

**Why this priority**: The desktop is the center of the application workspace. It enables the next layers of the framework, including dialogs, editors, and example programs that rely on child views inside the application shell.

**Independent Test**: Open, activate, and close child content within the desktop area while the menu bar and status line remain available. The workspace must preserve a usable focus target throughout the flow.

**Acceptance Scenarios**:

1. **Given** multiple child windows or views exist on the desktop, **When** the user activates a different one, **Then** focus moves to the selected workspace item without disrupting the application frame.
2. **Given** the active desktop item is closed, **When** the close action completes, **Then** focus moves to the next eligible workspace item or to the desktop itself.

### Edge Cases

- The application starts with an empty desktop and must still provide a usable shell.
- A command is visible in the menu system or status line but is not currently available in the active context.
- Different entry points reference the same command, and the action must not execute twice for a single user invocation.
- The active desktop item closes while it owns focus, and the shell must recover to another valid focus target.
- The available screen area is constrained, and the shell must still keep the menu, desktop, and status line logically separated.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST provide a top-level application shell for `TProgram` and `TApplication` that organizes the interface into menu bar, desktop area, and status line.
- **FR-002**: The system MUST allow application authors to define application-level menus that expose labeled actions to end users.
- **FR-003**: The system MUST provide a status line that can expose context-relevant actions and shortcuts to end users.
- **FR-004**: The system MUST route application-level commands consistently, regardless of whether the user invokes them from menus, status-line actions, or equivalent keyboard interactions.
- **FR-005**: The system MUST preserve a dedicated desktop workspace where child windows or views can be opened, activated, deactivated, and closed without collapsing the outer application frame.
- **FR-006**: The system MUST maintain a valid active focus target within the application shell during startup, command execution, window activation changes, and window closure.
- **FR-007**: The system MUST allow the application shell to start, remain interactive until an exit action is requested, and then shut down in a controlled manner.
- **FR-008**: The system MUST support applications that start with zero desktop children and still remain fully navigable through global shell actions.
- **FR-009**: The system MUST represent commands that are not currently available in a way that prevents accidental execution while preserving user orientation.
- **FR-010**: The system MUST allow framework consumers to customize the shell composition at startup, including menu content, desktop content, and status line content, without redefining the overall application frame model.

### Key Entities *(include if feature involves data)*

- **Application Shell**: The outer interactive frame that coordinates startup, shutdown, layout regions, global commands, and focus behavior.
- **Desktop Workspace**: The central area that hosts child windows or views and acts as the main working surface for the user.
- **Menu Action**: A selectable application command shown in the menu system, including its label, availability state, and user-visible intent.
- **Status Action**: A shortcut-oriented command representation shown in the status line, aligned with the current context and linked to an application action.
- **Active Workspace Item**: The child window or view that currently owns focus inside the desktop workspace.

## Assumptions

- The feature targets single-user interactive text applications that run in a local terminal session.
- A standard application shell always includes menu bar, desktop, and status line, even if one of those regions contains only minimal content at startup.
- Commands may be temporarily unavailable depending on application context, but users should still be able to understand which actions exist.
- The desktop workspace may contain zero, one, or many child windows or views over the lifetime of the application.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: In 100% of acceptance validations for this feature, a representative application launches into a usable shell that shows menu bar, desktop, and status line on the first interactive screen.
- **SC-002**: In at least 95% of validated interaction scenarios, users can trigger a primary global action from either the menu system or the status line in no more than three interactions.
- **SC-003**: In 100% of validated desktop-management scenarios, opening, activating, and closing workspace items leaves the application in a usable interactive state with a valid focus target.
- **SC-004**: Maintainers can demonstrate the complete startup-to-exit flow, including at least one menu action and one desktop interaction, in under five minutes using a representative sample application.
