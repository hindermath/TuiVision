# Feature Specification: Controls Revision

**Feature Branch**: `008-controls-revision`  
**Created**: 2026-03-29  
**Status**: Ready for Planning  
**Input**: User description: "Erstelle aus der Datei Lastenheft_ControlsRevision.md eine Specifikation."

## Clarifications

### Session 2026-03-29

- Q: Which keyboard close path is mandatory for closable windows? → A: Both `Ctrl+W` and `Escape` are mandatory; `Escape` closes only when no focused child control consumes it.
- Q: How should overlapping status-context definitions be resolved? → A: If multiple status-context definitions match, the first declared match wins.
- Q: Should arrow-key menu navigation wrap at the ends? → A: Top-level and submenu arrow navigation wraps at the ends.
- Q: How should window move mode handle confirm versus cancel? → A: `Enter` commits the new position; `Escape` cancels and restores the original position.
- Q: What should the status line show if no status-context definition matches? → A: Show no context actions; use an empty or neutral status line.
- Q: How should submenu navigation handle non-actionable entries (separators, disabled items)? → A: Navigation silently skips non-actionable entries; focus always lands on the next/previous actionable entry.
- Q: Which keys constitute "standard confirmation input" for submenu entry activation (FR-005)? → A: `Enter` and the mnemonic letter of the focused entry.
- Q: How many nesting levels must the reusable submenu model support (FR-006)? → A: Exactly one level — submenus may only appear directly under top-level menu entries.
- Q: Which key or action triggers entry into window move mode (FR-011)? → A: `Ctrl+F5` — matching the Turbo Vision 2.0 historical baseline.

## User Scenarios & Testing *(mandatory)*

This feature does not advance one of the 25 mandatory example ports directly.
Instead, it closes framework gaps in the Controls layer that block or weaken the
next mandatory example wave and leaves follow-on example delivery out of scope.

### User Story 1 - Operate Menus Like a Real Turbo Vision Shell (Priority: P1)

As a TUI user, I want the menu bar and its open submenus to react to normal menu
navigation keys so that I can move through commands predictably without relying
only on mnemonic hotkeys.

**Why this priority**: Reliable top-level and submenu navigation is the most
visible gap in the current Controls shell and directly blocks the next example
wave from behaving like the historical baseline.

**Independent Test**: Activate the menu system, move across top-level entries
and within an open submenu using navigation keys alone, and confirm that the
currently focused entry is always obvious before activation.

**Acceptance Scenarios**:

1. **Given** an application with an active menu bar, **When** the user opens the
   menu system and presses left or right navigation keys, **Then** focus moves
   between top-level menu entries without closing the active menu context.
2. **Given** an open submenu with multiple entries, **When** the user presses up
   or down navigation keys, **Then** the focused submenu entry changes and the
   newly focused entry is visually highlighted.
3. **Given** a highlighted submenu entry, **When** the user confirms it or
   cancels menu interaction, **Then** the system either executes the selected
   command or dismisses the menu cleanly without leaving an ambiguous state.

---

### User Story 2 - See Context-Sensitive Actions in the Status Line (Priority: P2)

As a TUI user, I want the status line to change when focus moves between
different views so that I can immediately see the shortcuts and commands that
apply to my current context.

**Why this priority**: Context-driven status hints are a core usability contract
for dialogs, editors, and future examples, and the current static status line
does not provide that guidance.

**Independent Test**: Switch focus between two views with different help or
command contexts and confirm that the status line replaces its visible actions
with the matching set for the newly focused context.

**Acceptance Scenarios**:

1. **Given** an application defines different status actions for different help
   contexts, **When** focus moves from one context to another, **Then** the
   status line updates to the matching action set for the active context.
2. **Given** the status line shows actionable entries for the current context,
   **When** the user triggers one of those entries through its documented
   shortcut, **Then** the corresponding command is routed through the same
   application command flow as other shell actions.

---

### User Story 3 - Control Windows and Dialog Closure Safely (Priority: P3)

As a TUI user, I want windows and dialogs to expose familiar close and movement
behaviors so that framed interactions feel controllable and predictable instead
of static or abruptly dismissive.

**Why this priority**: Window controls and dialog close validation are expected
shell behaviors, but they are secondary to menu and status-line recovery
because they do not block as much near-term example scope.

**Independent Test**: Open a closable window and a validating dialog, confirm
that the window exposes a visible close affordance and move mode, and verify
that the dialog can block closure when its validation rule fails.

**Acceptance Scenarios**:

1. **Given** a window is marked as closable, **When** the user invokes a close
   action through `Ctrl+W` or `Escape` when no focused child control consumes
   that key, **Then** the window closes through a normal command result instead
   of remaining stuck onscreen.
2. **Given** a window is marked as movable, **When** the user enters move mode
   and uses navigation keys, **Then** the window position changes in visible
   single-step increments until the move interaction is confirmed with `Enter`
   or canceled with `Escape`, which restores the original position.
3. **Given** a dialog defines a rule that must pass before it closes, **When**
   the user attempts to close it with a command that requires validation,
   **Then** the dialog either returns its result when valid or remains open when
   the rule fails.

### Edge Cases

- When a submenu contains disabled or separator entries, directional navigation
  silently skips them and always lands on the nearest actionable entry in the
  requested direction (resolved by FR-003b).
- When the application area shrinks, menu presentation recalculates and clips
  visible top-level titles without introducing a new overflow-navigation model
  (resolved by FR-012 and Assumptions).
- When focus moves to a view with no matching status-context definition, the
  status line shows no context actions and falls back to a neutral empty
  presentation (resolved by FR-008a).
- When multiple status-context definitions match, the first declared definition
  wins (resolved by FR-007a).
- When the user cancels a window move interaction, `Escape` restores the
  original position from before move mode started (resolved by FR-011a).
- When a dialog's default close path is requested but validation rejects the
  current state, the dialog remains open and preserves state for correction
  (resolved by FR-013 and FR-014).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The Controls revision MUST remain limited to the reusable
  framework behavior of the Controls layer and MUST NOT be presented as delivery
  of a mandatory example wave by itself.
- **FR-002**: The menu system MUST support directional keyboard navigation
  across top-level entries while the menu is active.
- **FR-002a**: Top-level directional menu navigation MUST wrap from the last
  actionable entry to the first and from the first to the last.
- **FR-003**: The menu system MUST support directional keyboard navigation
  within an open submenu, including moving focus to the next and previous
  actionable entry.
- **FR-003a**: Submenu directional navigation MUST wrap from the last
  actionable entry to the first and from the first to the last.
- **FR-003b**: Submenu directional navigation MUST silently skip separator
  lines and disabled entries; focus MUST always land on the nearest actionable
  entry in the requested direction.
- **FR-004**: An open submenu MUST always make the currently focused entry
  visually distinguishable from non-focused entries through a high-contrast
  selection state.
- **FR-005**: The menu system MUST allow the currently focused submenu entry to
  be activated through `Enter` or through the entry's mnemonic letter, and MUST
  allow the menu interaction to be dismissed without triggering a command.
- **FR-006**: The Controls layer MUST provide a consistent submenu-building
  model for exactly one level of nesting (submenus directly under top-level
  entries only) so that application authors can declare and reuse submenu
  structures without relying on ad-hoc top-level-only behavior. Deeper
  recursive nesting is explicitly out of scope for this revision.
- **FR-007**: The status line MUST support multiple context-bound action sets
  and MUST select the visible set according to the active help context.
- **FR-007a**: If multiple status-context definitions match the active help
  context, the first declared matching definition MUST win.
- **FR-008**: When application focus changes, the status line MUST refresh its
  visible action set to match the newly active context without requiring manual
  user re-entry into the shell.
- **FR-008a**: If no status-context definition matches the active help context,
  the status line MUST show no context actions and fall back to an empty or
  neutral presentation instead of keeping stale actions visible.
- **FR-009**: Status-line actions shown for the active context MUST remain
  executable through the normal application command flow.
- **FR-010**: A window that is configured as closable MUST display a visible
  close affordance in its title area and MUST support closing through both
  `Ctrl+W` and `Escape`, with `Escape` closing the window only when no focused
  child control consumes that key first.
- **FR-011**: A window that is configured as movable MUST support an explicit
  move interaction entered via `Ctrl+F5`, in which directional input changes the
  window position until the move is confirmed or canceled.
- **FR-011a**: In window move mode, `Enter` MUST commit the new position and
  `Escape` MUST cancel the move and restore the original position from before
  move mode started.
- **FR-012**: If a window layout changes because the available application area
  changes, menu presentation MUST recalculate visible top-level placement so
  that remaining menu titles stay correctly aligned within the new width.
- **FR-013**: A dialog MUST be able to evaluate whether a requested close
  command is currently valid before it returns a modal result.
- **FR-014**: When dialog validation rejects a close request, the dialog MUST
  remain open and preserve enough state for the user to correct the issue.
- **FR-015**: When dialog validation accepts a close request, the dialog MUST
  return a command result that distinguishes the accepted outcome from a generic
  boolean success flag.
- **FR-016**: This feature MUST leave streaming and persisted serialization of
  menu, status-line, dialog, and window state out of scope.
- **FR-017**: This feature MUST leave terminal-mouse support out of scope until
  the runtime environment provides a supported mouse-input path.
- **FR-018**: This feature MUST leave configurable color themes and palette
  customization out of scope.
- **FR-019**: This feature MUST leave editor, memo, file-editor, and help-viewer
  feature expansion to their dedicated planning scope rather than reopening that
  work here.
- **FR-020**: Acceptance for this feature MUST include updated project proof
  surfaces so reviewers can see that the affected Controls-layer components now
  reflect their actual delivered behavior.

### Key Entities *(include if feature involves data)*

- **Menu Context**: The currently active top-level menu entry together with any
  open submenu path and the single submenu entry that is presently focused,
  with wrap-around navigation across both top-level and submenu bounds.
- **Submenu Definition**: A reusable declaration of one nested menu branch,
  including its label, contained entries, and any child submenu relationships.
- **Status Context Definition**: A rule that maps one help-context value or
  range to one visible set of status actions, with declaration order acting as
  the tie-breaker when multiple definitions match.
- **Status Action Set**: The ordered collection of commands and shortcut labels
  that the status line exposes for one active help context, or an empty or
  neutral set when no definition matches.
- **Window Interaction State**: The current control state of a window, including
  whether close behavior is available, whether move mode is active, what
  onscreen position is currently in effect, and what original position must be
  restored if move mode is canceled.
- **Dialog Close Result**: The accepted outcome that a dialog returns when a
  close request passes validation, distinct from rejected close attempts that
  leave the dialog open.

## Assumptions

- The existing Controls layer already provides the baseline shell structure,
  command routing, and drawing pipeline needed to extend menu, status-line,
  window, and dialog behavior without redefining the broader application model.
- The next mandatory example wave depends on these Controls-layer behavior gaps
  being closed first, so this revision is treated as an enabling framework step
  rather than optional polish.
- Directional keyboard navigation is the mandatory interaction path for this
  revision; mouse hover and click behavior remain excluded until supported by
  the runtime driver.
- A single visible close affordance and a single documented move interaction are
  sufficient for this feature; full maximize, zoom, grow, and palette feature
  parity is not required here.
- If no dedicated status definition matches the active help context, the status
  line must fall back to a neutral empty presentation rather than keeping stale
  actions or fabricating unrelated ones.
- Menu resizing acceptance focuses on correct recalculation and safe clipping of
  visible menu titles, not on inventing a new overflow-navigation model.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Reviewers can activate a menu, navigate across top-level items and
  through open submenus using directional keys alone, and reach the intended
  command in all defined acceptance scenarios without relying on mnemonic-only
  access paths.
- **SC-002**: In validation scenarios with at least two distinct help contexts,
  the status line updates to the correct action set on every context change and
  shows no stale actions from the previously focused context.
- **SC-003**: In validation scenarios for closable and movable windows, each
  requested close or move interaction produces the expected visible result on
  the first attempt in at least 95% of repeated runs across the supported test
  environments.
- **SC-004**: In validation scenarios for dialogs that apply close rules,
  rejected close attempts keep the dialog open and accepted close attempts
  return a distinct modal result in 100% of observed runs.
- **SC-005**: The feature remains within its stated bounds: no acceptance
  artifact for this revision depends on streaming, terminal-mouse support,
  palette customization, or delivery of a new example wave.
