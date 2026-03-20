# Feature Specification: Application Framework Shell

**Feature Branch**: `[002-application-framework]`  
**Created**: 2026-03-20  
**Status**: Ready for Planning  
**Input**: User description: "Bitte setze aus der Datei Pflichtenheft.md in Abschnitt 8.1 Nr. 4 Anwendungsrahmen: `TProgram`, `TApplication`, Menues, Statuszeile, Desktop um."

## Clarifications

### Session 2026-03-20

- Q: Which shell initialization model should the specification require for `TApplication`? → A: `TApplication` creates a complete default shell with desktop, menu bar, and status line.
- Q: How should currently unavailable global actions be presented? → A: Unavailable menu and status-line actions remain visible but are shown as disabled.
- Q: What is the scope boundary for this feature? → A: This feature is limited to shell infrastructure; concrete dialogs, controls, and specialized window types are out of scope.
- Q: What is the class hierarchy relationship between `TProgram` and `TApplication`? → A: `TProgram` is the abstract base class (event loop, command routing, lifecycle); `TApplication` is the concrete subclass that auto-assembles menu bar, desktop, and status line.
- Q: How does `TDesktop` relate to the existing `TGroup` from feature 001? → A: `TDesktop` extends `TGroup`, reusing child-view management, focus traversal, and event delegation.
- Q: What keyboard activation model is required for menus? → A: Alt-key / F10 menu activation required; arrow-key navigation within the menu bar also required.
- Q: What does "controlled shutdown" mean for `TProgram`/`TApplication`? → A: Immediate shutdown on exit request; no confirmation dialog; all child views receive a close/teardown notification before the shell exits.
- Q: Must the shell handle terminal resize events at runtime? → A: Yes; terminal resize events must be detected and the shell must redraw and re-layout all regions (menu bar, desktop, status line) to fit the new dimensions.

### Session 2026-03-20 (continued)

- Q: Are nested submenus required in the menu system? → A: Nested submenus required; a menu item may open a submenu with its own items, matching original Turbo Vision behavior.
- Q: How does the status line update its content? → A: Status line updates automatically when focus changes; each view can declare its own status hints which the status line reads.
- Q: What are the layout heights for `TMenuBar` and `TStatusLine`? → A: `TMenuBar` is always 1 row at the top; `TStatusLine` is always 1 row at the bottom; desktop fills the remaining rows.

### Session 2026-03-20 (second continuation)

- Q: How are commands identified at runtime for routing, availability checks, and menu/status binding? → A: Integer command constants (`const int cmXxx`) in a shared class; command IDs are compared by value throughout routing and availability checks, matching the original Turbo Vision model.
- Q: Should the spec include an explicit code coverage SC? → A: Yes; add SC-005 requiring ≥70% line coverage for all new classes from this feature, measured with Coverlet, aligning with the project-level CLAUDE.md gate.

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
- A command is visible in the menu system or status line but is not currently available in the active context, and it must remain visible while being shown as disabled.
- Different entry points reference the same command, and the action must not execute twice for a single user invocation.
- The active desktop item closes while it owns focus, and the shell must recover to another valid focus target.
- The available screen area is constrained or changes at runtime (terminal resize); the shell must detect resize events and re-layout all regions (menu bar, desktop, status line) to fit the new dimensions while keeping them logically separated.
- The shell infrastructure must remain usable even when no concrete dialog, control, or specialized window type has been implemented yet.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST provide a top-level application shell via `TProgram` (abstract base: event loop, command routing, lifecycle) and its concrete subclass `TApplication` (auto-assembles menu bar, desktop area, and status line) that organizes the interface into those three regions.
- **FR-001a**: The system MUST define `TApplication` startup so that a usable default shell is created automatically, including desktop workspace, menu bar, and status line, while still allowing application authors to customize or replace those regions afterward. The default layout MUST place `TMenuBar` as a 1-row region at the top, `TStatusLine` as a 1-row region at the bottom, and `TDesktop` filling all remaining rows.
- **FR-002**: The system MUST allow application authors to define application-level menus that expose labeled actions to end users. Menus MUST support nesting: a menu item may open a submenu with its own list of items (cascading submenus), matching the original Turbo Vision `TMenuBar → TMenu → TSubMenu` hierarchy.
- **FR-003**: The system MUST provide a status line (`TStatusLine`) that automatically updates its displayed actions and shortcuts when focus changes. Each view MAY declare its own status hints; `TStatusLine` reads the focused view's hints and reflects them, making the status line context-sensitive without requiring manual application updates.
- **FR-004**: The system MUST route application-level commands consistently, regardless of whether the user invokes them from menus, status-line actions, or equivalent keyboard interactions. Commands MUST be identified by integer constants (`const int cmXxx`) defined in a shared constants class; routing and availability checks MUST compare commands by integer value. Keyboard activation MUST include: F10 or Alt to open the menu bar, arrow-key navigation between menu bar items and within open menus, and Enter/Escape to confirm or dismiss.
- **FR-005**: The system MUST preserve a dedicated desktop workspace (`TDesktop`, extending `TGroup`) where child windows or views can be opened, activated, deactivated, and closed without collapsing the outer application frame. `TDesktop` MUST reuse `TGroup`'s child-view management, focus traversal, and event delegation rather than reimplementing those behaviours.
- **FR-012**: The system MUST detect terminal resize events at runtime and re-layout all shell regions (menu bar, desktop workspace, status line) to fit the new terminal dimensions without requiring an application restart.
- **FR-006**: The system MUST maintain a valid active focus target within the application shell during startup, command execution, window activation changes, and window closure.
- **FR-007**: The system MUST allow the application shell to start, remain interactive until an exit action is requested, and then shut down in a controlled manner. Controlled shutdown means: no confirmation dialog is shown (dialog infrastructure is out of scope for this increment); all child views receive a close/teardown notification before the shell exits; and the process terminates cleanly.
- **FR-008**: The system MUST support applications that start with zero desktop children and still remain fully navigable through global shell actions.
- **FR-009**: The system MUST represent commands that are not currently available in a way that prevents accidental execution while preserving user orientation.
- **FR-009a**: The system MUST keep unavailable global actions visible in both the menu system and status line and present them as disabled rather than hiding them.
- **FR-010**: The system MUST allow framework consumers to customize the shell composition at startup, including menu content, desktop content, and status line content, without redefining the overall application frame model.
- **FR-011**: The system MUST keep this feature scoped to application-shell infrastructure, including global command routing and desktop hosting behavior, without requiring concrete dialog classes, control widgets, or specialized window types to be delivered as part of this increment.

### Key Entities *(include if feature involves data)*

- **TProgram**: Abstract base class providing the event loop, command routing, and application lifecycle. Framework consumers subclass `TProgram` when they need full control over shell composition.
- **TApplication**: Concrete subclass of `TProgram` that automatically assembles the default shell (menu bar, desktop workspace, status line) at startup. Framework consumers subclass `TApplication` for standard applications.
- **Application Shell**: The outer interactive frame (realized by `TProgram`/`TApplication`) that coordinates startup, shutdown, layout regions, global commands, and focus behavior.
- **Desktop Workspace / TDesktop**: The central area that hosts child windows or views and acts as the main working surface for the user. `TDesktop` extends `TGroup`, reusing child-view management, focus traversal, and event delegation from feature 001.
- **Command ID**: An integer constant (`const int cmXxx`) that uniquely identifies an application command. Command IDs are the shared currency for event routing, availability checks, menu item binding, and status hint binding. Standard shell commands (e.g., `cmQuit`) are defined in a shared constants class.
- **Menu Action**: A selectable application command shown in the menu system, identified by a Command ID, including its label, availability state, and user-visible intent.
- **TMenuBar**: The top-most shell region (fixed 1-row height) that renders the application's menu structure and handles menu activation via F10/Alt and arrow-key navigation.
- **TStatusLine**: The bottom-most shell region (fixed 1-row height) that automatically reads the focused view's declared status hints on each focus change and renders the current set of context-relevant shortcut/action labels.
- **Status Hint**: A shortcut-oriented command declaration that a view registers to describe its available actions. `TStatusLine` reads the active view's hints and renders them; hints change as focus moves.
- **Active Workspace Item**: The child window or view that currently owns focus inside the desktop workspace.

## Assumptions

- The feature targets single-user interactive text applications that run in a local terminal session.
- A standard application shell always includes menu bar, desktop, and status line, even if one of those regions contains only minimal content at startup.
- `TApplication` is the convenience entry point that creates the full default shell automatically rather than requiring authors to assemble the initial frame from scratch.
- Commands may be temporarily unavailable depending on application context, but users should still be able to understand which actions exist because unavailable actions remain visible as disabled entries.
- The desktop workspace may contain zero, one, or many child windows or views over the lifetime of the application.
- Concrete dialogs, control widgets, and specialized window types belong to later increments and are not required for acceptance of this feature.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: In 100% of acceptance validations for this feature, a representative application launches into a usable shell that shows menu bar, desktop, and status line on the first interactive screen.
- **SC-002**: In at least 95% of validated interaction scenarios, users can trigger a primary global action from either the menu system or the status line in no more than three interactions.
- **SC-003**: In 100% of validated desktop-management scenarios, opening, activating, and closing workspace items leaves the application in a usable interactive state with a valid focus target.
- **SC-004**: Maintainers can demonstrate the complete startup-to-exit flow, including at least one menu action and one desktop interaction, in under five minutes using a representative sample application.
- **SC-005**: All new classes delivered by this feature (`TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, `TStatusLine`, and supporting types) MUST achieve ≥70% line coverage as measured by Coverlet (`dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`). This gate must pass before merging to `main`.
