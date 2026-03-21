# Data Model: Editor, File, Help, and Stream Components

## Overview

This feature combines in-memory interaction models in `TuiVision.Controls` with file-backed persistence models in `TuiVision.Serialization`. No database storage is involved. The data model focuses on editor sessions, file-dialog state, help-file lookup, compatibility stream context, and named resource persistence.

## Entities

### DocumentSession

- **Purpose**: Represents one active editing workflow, whether file-backed or memory-only.
- **Key attributes**:
  - Current text content
  - Cursor position
  - Visible viewport offset
  - Selection range
  - Modification state (`clean`, `modified`, `close-pending`, `conflict-pending`)
  - Line-ending mode (`preserved-crlf`, `preserved-lf`, `default-lf`)
  - Optional attached file path
  - Optional file snapshot
  - Pending close-decision intent
- **Relationships**:
  - Hosted by exactly one `EditorHost`
  - May be persisted through `FileAttachment`
  - Exposes command availability to the shell
- **Validation rules**:
  - New sessions start with `default-lf`
  - Loaded sessions retain detected line-ending mode
  - A modified session cannot be closed without an explicit keep-or-discard decision
  - A session with an external file conflict cannot silently overwrite the target

### FileAttachment

- **Purpose**: Captures the real local file-system binding for a `TFileEditor` session.
- **Key attributes**:
  - Absolute or resolved file path
  - Existence state (`new`, `existing`)
  - Last loaded snapshot
  - Current overwrite-decision requirement
- **Relationships**:
  - Belongs to zero or one `DocumentSession`
- **Validation rules**:
  - Existing files must refresh snapshot data after a successful save
  - New files do not carry a prior snapshot before first save

### FileSnapshot

- **Purpose**: Represents the managed metadata used to detect external modification between load and save.
- **Key attributes**:
  - Last-write timestamp
  - File length
- **Relationships**:
  - Belongs to exactly one `FileAttachment`
- **Validation rules**:
  - Snapshot comparison happens before overwrite of an existing file
  - A mismatch transitions the owning session to `conflict-pending`

### EditorHost

- **Purpose**: Represents a framed shell host for an editor workflow.
- **Key attributes**:
  - Frame/title metadata
  - Embedded `DocumentSession`
  - Optional indicator data
  - Close state
- **Relationships**:
  - Hosts exactly one `DocumentSession`
  - Lives inside the existing desktop workspace
- **Validation rules**:
  - Must remain usable as a non-modal shell child
  - Must surface document-state changes to shell command routing

### FileDialogSession

- **Purpose**: Represents one open/select/save interaction through file-oriented dialogs.
- **Key attributes**:
  - Current directory
  - Wildcard filter
  - Visible file entries
  - Visible directory entries
  - Typed path text
  - Pending action (`open`, `select`, `save-target`, `cancel`)
  - Linked history identifier
- **Relationships**:
  - May read from one `HistoryBucket`
  - May emit one resolved target path
- **Validation rules**:
  - Directory view, file list, and typed path must remain synchronized
  - Wildcard-filter changes refresh the visible file-entry set without breaking manual path entry
  - Empty result sets do not block cancellation or manual path entry
  - Manual path entry may resolve a target without requiring prior list selection

### HistoryBucket

- **Purpose**: Stores recalled text entries for one history identifier.
- **Key attributes**:
  - History identifier
  - Ordered entry list
  - Most-recent-first semantics
- **Relationships**:
  - May be used by zero to many linked fields
- **Validation rules**:
  - Buckets are isolated by identifier
  - Cross-session persistence is not required

### HelpFileModel

- **Purpose**: Represents the dedicated persisted help file loaded at runtime.
- **Key attributes**:
  - Topic index
  - Topic payload source
  - Modified flag (always false for phase-6 runtime usage)
- **Relationships**:
  - Owns one `HelpIndex`
  - Owns zero to many `HelpTopic`
- **Validation rules**:
  - Runtime loading must support topic lookup by numeric context
  - Writing or updating the help file is outside acceptance scope

### HelpIndex

- **Purpose**: Maps numeric help contexts to stored topic locations.
- **Key attributes**:
  - Context identifier
  - Topic position / lookup entry
- **Relationships**:
  - Belongs to exactly one `HelpFileModel`
- **Validation rules**:
  - Missing contexts must trigger fallback help behavior rather than empty UI

### HelpTopic

- **Purpose**: Represents one runtime help topic.
- **Key attributes**:
  - Numeric context identifier
  - Paragraph collection
  - Wrapped-line state
  - Cross-reference collection
- **Relationships**:
  - Belongs to one `HelpFileModel`
  - References zero to many other `HelpTopic` entries through `CrossReference`
- **Validation rules**:
  - A topic may have zero cross-references
  - Cross-reference targets must resolve through the help file or fall back safely

### CrossReference

- **Purpose**: Defines one navigable jump inside a help topic.
- **Key attributes**:
  - Target context identifier
  - Text offset / length
  - Visible selection state
- **Relationships**:
  - Belongs to exactly one `HelpTopic`
- **Validation rules**:
  - Activation must navigate within the same runtime help workflow

### StreamContext

- **Purpose**: Represents the state of one compatibility stream read or write operation.
- **Key attributes**:
  - Stream direction (`read`, `write`, `read-write`)
  - Underlying managed stream
  - Current position
  - Registered type metadata
  - Object-reference table
- **Relationships**:
  - May own zero to many `TypeRegistration` records
  - May own zero to many `ReferenceRecord` entries
- **Validation rules**:
  - Shared references must resolve consistently
  - Cyclic object graphs are rejected rather than reconstructed
  - Malformed, truncated, or trailing data produces explicit failure

### TypeRegistration

- **Purpose**: Defines how a persisted type identifier maps to a factory or builder.
- **Key attributes**:
  - Stable type identifier
  - Factory delegate
- **Relationships**:
  - Belongs to a `StreamContext` or registry
- **Validation rules**:
  - Type identifiers are unique within one registry

### ResourceCatalog

- **Purpose**: Represents the named resource container behind `TResourceFile`.
- **Key attributes**:
  - Ordered or searchable list of entries
  - Case-sensitive key comparer
  - Backing stream position metadata
- **Relationships**:
  - Owns zero to many `ResourceEntry`
- **Validation rules**:
  - Lookup, replacement, and removal use exact key matching
  - Keys differing only by case remain distinct

### ResourceEntry

- **Purpose**: Represents one named persisted object in the resource catalog.
- **Key attributes**:
  - Case-sensitive key
  - Payload position
  - Payload length
  - Registered type identity
- **Relationships**:
  - Belongs to exactly one `ResourceCatalog`
- **Validation rules**:
  - Replacing an existing key updates the active lookup target
  - Removing a key excludes it from future enumeration

## State Transitions

### Document Session Lifecycle

`new` → `clean` → `modified` → `close-pending` / `conflict-pending` → `clean` / `modified` / `closed`

- `new` to `clean`: document is created or loaded and ready for editing.
- `clean` to `modified`: user changes content.
- `modified` to `close-pending`: the user attempts to close or replace the session while unsaved content still exists.
- `modified` to `conflict-pending`: save detects an overwrite decision requirement or external file change.
- `close-pending` to `closed`: user explicitly discards unsaved changes.
- `close-pending` to `modified`: user cancels the close and returns to editing.
- `modified` to `clean`: save succeeds and snapshot/line-ending state is refreshed.
- `conflict-pending` to `clean`: user explicitly confirms overwrite and save succeeds.
- `conflict-pending` to `modified`: user declines overwrite or save fails.

### File Dialog Outcome

`browsing` → `resolved-open` / `resolved-save-target` / `cancelled`

- `browsing` persists while directory, file list, and typed path continue changing.
- `resolved-open` returns a chosen existing path.
- `resolved-save-target` returns a target path that may be new or replacing.
- `cancelled` exits without a path.

### Help Navigation

`topic-loaded` ↔ `topic-linked` → `fallback-loaded`

- `topic-loaded`: a context resolved to a topic.
- `topic-linked`: the viewer is focused on a selectable cross-reference inside the loaded topic.
- `fallback-loaded`: the requested context was missing and fallback help content is shown.

### Stream Read Lifecycle

`initialized` → `reading` → `completed` / `failed`

- `initialized` to `reading`: stream begins consuming bytes and building references.
- `reading` to `completed`: payload consumed fully with valid registrations and no trailing bytes.
- `reading` to `failed`: malformed input, unknown type, truncated payload, trailing data, invalid registration state, or cycle rejection occurs.

### Resource Entry Lifecycle

`absent` → `stored` → `replaced` / `removed`

- `absent` to `stored`: a new key is written.
- `stored` to `replaced`: the same exact key is written again and supersedes the old entry.
- `stored` to `removed`: the exact key is deleted from active lookup/enumeration.

## Validation Notes

- The model intentionally excludes database persistence, network collaboration, and help-authoring workflows.
- The help file and resource catalog may share stream primitives, but they remain distinct persistence domains.
- Observability, security hardening, and formal numeric performance thresholds are intentionally deferred unless promoted by a later planning revision.
- Any later need for a broader generic windowing model is outside the current data model and would require scope review.
