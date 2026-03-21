# Feature Specification: Editor, File, Help, and Stream Components

**Feature Branch**: `004-editor-file-help-streams`  
**Created**: 2026-03-21  
**Status**: Ready for Planning  
**Input**: User description: "Erstelle die Spezifikation aus Pflichtenheft.md Abschnitt 8.1 Nr. 6 'Editor/Datei/Hilfe/Streams: Editor, Resource-, Stream- und Help-Komponenten'."

## Clarifications

### Session 2026-03-21

- Q: Which persistence-compatibility target should this increment require for streams and resource files? → A: Behavioral and conceptual compatibility is required; binary-format compatibility with original Turbo Vision files is not required.
- Q: What object-graph support must the stream subsystem provide? → A: Shared references must be preserved, but cyclic object graphs are not part of acceptance for this increment.
- Q: What file-system target must Phase 6 support for editor and file dialogs? → A: Editor and file dialogs must work against the real local file system.
- Q: Must Phase 6 support writing help files, or only reading and navigating help content at runtime? → A: Help content must be readable and navigable at runtime; writing or updating help files is not part of acceptance.
- Q: What persisted source must runtime help use for acceptance? → A: Runtime help must be loadable from a dedicated help file.
- Q: How must history entries be scoped? → A: History entries are partitioned by history identifier; only fields with the same history ID share entries.
- Q: How must `TFileEditor` handle line endings when saving? → A: Loaded files preserve their existing line-ending format; newly created files use a defined default format.
- Q: What default line-ending format must newly created files use? → A: Newly created files use LF as the default line-ending format.
- Q: How are resource keys compared? → A: Resource keys are case-sensitive and require exact matches for lookup, replacement, and removal.
- Q: How must `TFileEditor` behave if the file changed externally during the editing session? → A: It must detect the external change and require an explicit overwrite decision before saving.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Edit and manage document content (Priority: P1)

As an application developer, I want to host a reusable text editor inside the TuiVision shell so that end users can create, inspect, modify, search, and replace document content within the terminal workspace.

**Why this priority**: The editor is the center of this increment. File handling, help integration, and several example applications only become useful once the framework can offer a credible in-application editing experience.

**Independent Test**: Open a document window with a text buffer, type and delete text, move the cursor across lines, select text, run search and replace, and close the editor while preserving a valid application state.

**Acceptance Scenarios**:

1. **Given** an editor window is open with a visible text buffer, **When** the user types text, inserts line breaks, and deletes characters, **Then** the buffer content changes accordingly and the visible viewport continues tracking the active cursor position.
2. **Given** the editor contains searchable text, **When** the user runs find or replace, **Then** the editor moves to the matching content and applies the confirmed replacement without leaving the editing workflow.
3. **Given** the editor has unsaved changes, **When** the user attempts to close or replace the current document, **Then** the framework requires an explicit decision before the changes are discarded.

---

### User Story 2 - Open, choose, and save files through reusable dialogs (Priority: P2)

As an end user, I want file-oriented dialogs and history helpers that operate on the real local file system so that I can choose existing files, navigate directories, enter a target path manually, and save work without leaving the application shell.

**Why this priority**: File interaction is the practical bridge between the editor and real application data. Without reusable file and directory components, the editor remains limited to temporary buffers and later example programs cannot be ported cleanly.

**Independent Test**: Launch a file dialog from the shell, browse through directories, select an item from a filtered file list, recall a previously used path from history for the same history identifier, and complete either an open or save flow.

**Acceptance Scenarios**:

1. **Given** a file dialog opens with a wildcard filter, **When** the user navigates directories or selects a listed item, **Then** the file list, current directory, and path input stay synchronized.
2. **Given** the user wants to save to a different location, **When** the user enters or selects a new target path, **Then** the dialog returns that target for the save flow without forcing the user to restart the interaction.
3. **Given** a destructive file action would replace an existing target, **When** the user confirms the action, **Then** the framework completes the action only after the replacement decision is made explicitly.
4. **Given** an existing file is loaded with a known line-ending format, **When** the user saves changes without converting the document, **Then** the saved file keeps that line-ending format.
5. **Given** the loaded file changed on disk during the editor session, **When** the user saves, **Then** the editor detects that external change and requires an explicit overwrite decision before replacing the file.

---

### User Story 3 - Read context-sensitive help and follow linked topics (Priority: P3)

As an end user, I want a help system that opens the relevant topic from a dedicated persisted help file for the current context and lets me follow cross-references so that I can learn the application without leaving the shell.

**Why this priority**: Help is part of the original framework experience and is required for the `bhelp` and `helpdemo` example families. It also provides a meaningful integration target for streams and resources because help topics are persisted content, not only runtime UI.

**Independent Test**: Open help from a known context, verify the matching topic is shown, move through linked references with keyboard and mouse, and confirm that an unknown context still yields meaningful fallback information.

**Acceptance Scenarios**:

1. **Given** the application requests help for a valid context, **When** the help window opens, **Then** the matching topic is displayed in a scrollable view.
2. **Given** the visible help topic contains cross-references, **When** the user activates one of them, **Then** the viewer navigates to the referenced topic inside the same help workflow.
3. **Given** a help request references a missing context, **When** the request is handled, **Then** the user receives fallback help content instead of an empty or broken screen.

---

### User Story 4 - Persist and reload named resources and streamable objects (Priority: P4)

As a framework maintainer, I want reusable stream and resource components so that application data, help content, and other ported objects can be stored, loaded, and exchanged with stable naming and object reconstruction rules.

**Why this priority**: Streams and resources are the persistence backbone for this increment. They are prerequisites for file-backed help content, reusable application assets, and future example ports that rely on stored objects rather than purely transient UI state.

**Independent Test**: Write a representative set of named streamable objects to a persisted resource container, reload them in a fresh session, and verify that lookup, replacement, and removal behave consistently.

**Acceptance Scenarios**:

1. **Given** a streamable object graph is registered for persistence, **When** it is written and read back through the stream subsystem, **Then** the reconstructed objects preserve their declared type identity and logical data relationships.
2. **Given** a named resource already exists, **When** the maintainer stores a replacement under the same key, **Then** the newer version becomes the retrievable one without leaving duplicate active entries behind.
3. **Given** the persisted input is incomplete or invalid, **When** the stream or resource subsystem reads it, **Then** the operation fails explicitly and does not present partial data as a valid object.

### Edge Cases

- The editor starts with an empty buffer and must still allow navigation, insertion, and save-related flows.
- A document line is longer than the visible editor width and must remain editable through horizontal scrolling without corrupting adjacent content.
- A file dialog finds no matching items for the active filter and must still allow cancellation or manual path entry.
- The selected save target is invalid, missing, or not writable, and the failure must be surfaced without destroying the current editor buffer.
- A newly created document is saved for the first time, and the system must apply the defined default line-ending format consistently.
- A loaded file changes on disk during the editing session, and the editor must not silently overwrite that external change.
- A help topic contains no cross-references and must remain readable without broken navigation state.
- A help context is missing from the persisted help content and must resolve to fallback information rather than a blank or crashed viewer.
- A resource file already contains a key that is being updated or removed, and enumeration must reflect the latest visible state.
- Two resource keys differ only by letter casing, and they must remain distinct entries rather than collapsing into one another.
- A stream contains unknown type information, truncated payload data, or trailing unread data, and the read operation must stop with an explicit failure result.
- A stream contains cyclic object references, and the system must reject or explicitly not support that payload rather than silently corrupting reconstruction.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST provide a reusable multi-line editor component (`TEditor`) that supports text insertion, overwrite mode, deletion, line breaks, cursor movement, scrolling, selection, undo, clipboard-oriented editing actions, and search/replace within the active document buffer.
- **FR-002**: The system MUST expose editor command availability and document state so that the surrounding application shell can keep menus, status-line actions, and close/save flows aligned with the active editor context.
- **FR-003**: The system MUST provide an in-memory document editor (`TMemo`) for scenarios where application authors need editor behavior without immediate file-system attachment.
- **FR-004**: The system MUST provide a file-backed editor (`TFileEditor`) that can load an existing document from the real local file system, create a new untitled document, save to the current target, save to a new target, and require an explicit decision before unsaved changes are discarded.
- **FR-004a**: When `TFileEditor` saves a document that was loaded from an existing file, it MUST preserve that file's existing line-ending format. Newly created files MUST use `LF` as the default line-ending format consistently.
- **FR-004b**: When `TFileEditor` detects that the underlying file changed externally after loading, it MUST require an explicit overwrite decision before replacing the on-disk file.
- **FR-005**: The system MUST provide an editor host window (`TEditWindow`) that presents a document title, visible edit surface, and supporting editor indicators while remaining compatible with the existing desktop workspace and window-management model from earlier increments.
- **FR-006**: The system MUST provide reusable file-dialog components that let users browse matching files on the real local file system, type a path manually, choose directories, confirm open/select/save flows, and cancel safely without destabilizing the application shell.
- **FR-007**: The system MUST keep file-related controls synchronized so that directory navigation, file listing, file information, and manual path entry continue to describe the same current selection or target path throughout a dialog session.
- **FR-008**: The system MUST provide reusable history support (`THistory` and related interactions) so that previously used paths or text inputs can be recalled and reapplied without full re-entry. History entries MUST be partitioned by history identifier; fields share recall data only when they use the same history ID.
- **FR-009**: The system MUST provide a help-content model (`THelpTopic`, `THelpIndex`, `THelpFile`) that stores help topics by numeric context, supports wrapped paragraph content, and maintains cross-reference links between topics for runtime reading and navigation from a dedicated help file.
- **FR-010**: The system MUST provide a help viewer and help window (`THelpViewer`, `THelpWindow`) that can open the topic for a requested context, scroll through content, highlight selectable cross-references, and navigate to linked topics through keyboard and mouse interaction.
- **FR-011**: The help subsystem MUST return meaningful fallback content when a requested help context cannot be resolved.
- **FR-011a**: This increment MUST support runtime reading and navigation of help content, but creating, editing, or rewriting help files is not required for acceptance.
- **FR-012**: The system MUST provide reusable stream primitives (`pstream`, `ipstream`, `opstream`, `fpstream`) that can read and write primitive values, strings, byte sequences, and registered object graphs while preserving shared-reference identity rules during serialization and deserialization. Cyclic object-graph support is not required for acceptance in this increment. The acceptance target is behavioral and conceptual compatibility with the original framework, not byte-for-byte binary compatibility with original Turbo Vision stream files.
- **FR-013**: The stream subsystem MUST detect invalid, truncated, unknown, or trailing serialized input and report failure explicitly instead of treating partially read data as valid.
- **FR-014**: The system MUST provide a named resource container (`TResourceFile` and supporting collections) that supports storing, retrieving, replacing, removing, and enumerating streamable objects by stable resource keys. The acceptance target is reusable named persistence behavior for TuiVision applications, not mandatory interoperability with original Turbo Vision resource-file bytes.
- **FR-014a**: Resource keys MUST be case-sensitive. Lookup, replacement, removal, and enumeration semantics MUST use exact key matching.
- **FR-015**: The system MUST keep this increment scoped to reusable framework components for editing, file interaction, help content, and persistence. Porting example applications such as `tvedit`, `bhelp`, and `helpdemo`, as well as driver consolidation and OS-shell integrations, MUST remain outside this feature's acceptance scope.

### Key Entities *(include if feature involves data)*

- **Document Buffer**: The editable text content managed by `TEditor`, including cursor location, selection range, modification state, and undoable changes.
- **Editor Session**: The visible editing workflow hosted by `TEditWindow`, combining the document buffer with shell routing, indicators, and save/close decisions.
- **File Selection Session**: The temporary dialog state that coordinates current directory, wildcard filter, visible file entries, typed path text, and the pending user action such as open or save.
- **History Entry Set**: The reusable record of previously entered paths or values that can be recalled into a linked input field and is partitioned by history identifier.
- **Help Topic**: A context-addressable help entry containing wrapped paragraphs and zero or more cross-references to other topics.
- **Help File**: The dedicated persisted source that stores help topics and their index for runtime lookup by help context.
- **Cross-Reference**: A navigable link from one help topic to another, including its visible range within the topic text and its target help context.
- **Streamable Type Registration**: The mapping that tells the stream subsystem how to reconstruct persisted object instances when reading object graphs.
- **Shared Reference Identity**: The rule that multiple references to the same logical object remain shared after deserialization instead of being duplicated into unrelated copies.
- **Resource Entry**: A named persisted object stored in a resource container and retrievable by a stable key.
- **Resource Key**: The exact case-sensitive identifier used to store, retrieve, replace, remove, and enumerate a resource entry.

## Assumptions

- This increment builds on the already ported shell and control layers, so editor, file, and help components can rely on an existing desktop workspace, dialog behavior, scrolling controls, and command routing infrastructure.
- Text editing is terminal-oriented and single-user; collaborative editing, concurrent file locking strategies, and network-based documents are not required in this increment.
- Real local file-system access is part of the acceptance target for the editor and file-dialog flows in this increment.
- File dialogs may be used for both open and save-style workflows, but they do not need to provide full operating-system file-manager behavior.
- Newly created files use `LF` as the single TuiVision-defined default line-ending format, while loaded files keep their original line endings on save.
- Externally modified files are treated as a conflict that requires an explicit overwrite decision rather than silent replacement.
- The help subsystem in this increment is a runtime consumption feature based on a dedicated help file; help-authoring workflows belong to a later step if needed.
- History support is scoped to reusable in-application recall of previously entered values; cross-session persistence of history is not required unless an application chooses to persist it separately, and sharing occurs only within the same history identifier.
- The stream and resource subsystem serves framework persistence needs first; JSON import/export, database storage, and external service integration are outside this increment.
- The stream and resource subsystem ports the original framework concepts and behaviors, but it does not need to reproduce the original Turbo Vision binary file format exactly for acceptance of this feature.
- Shared references inside persisted object graphs are in scope; cyclic object graphs are explicitly outside the acceptance target for this increment.
- Resource identifiers use exact case-sensitive matching semantics throughout the resource container.
- Example applications named in the Pflichtenheft remain important validation targets later, but porting those examples is not part of this specification itself.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: In 100% of acceptance validations for this feature, a reviewer can create or open a document, make visible edits, and complete either a save or cancel-safe close flow inside one editor session without leaving the shell.
- **SC-002**: In at least 90% of scripted file-interaction validations, users complete an open or save-target selection in no more than five interactions after the dialog appears.
- **SC-003**: In 100% of help-system validations, opening help for either a valid or invalid context produces readable content within two interaction steps and keeps the application responsive.
- **SC-004**: In 100% of persistence validations, named resources written during the test session can be enumerated and reloaded with the expected keys and logical content in a subsequent read step.
- **SC-005**: In 100% of invalid-input validations, malformed or incomplete persisted data is rejected explicitly and does not yield a silently accepted partial object.
