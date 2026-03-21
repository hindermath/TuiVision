# Contract: Editor, File, Help, and Persistence API

## Purpose

Define the behavioral contract for the public or externally consumable surface introduced by the phase-6 editor/file/help/stream increment. The contract fixes responsibilities and observable guarantees more strongly than final internal signatures.

## Public Surface Contract

### `TEditor`

- Acts as the reusable multi-line editing surface.
- Owns text mutation, cursor movement, viewport tracking, selection, and document modification state.
- Exposes command availability and document state so the shell can keep menu/status actions aligned with the active editor session.
- Requires an explicit safe-close decision path before modified content is discarded.
- Does not perform real file-system persistence by itself.

### `TMemo`

- Specializes `TEditor` for memory-only editing workflows.
- Supports the same editing interaction model without requiring file attachment.

### `TFileEditor`

- Specializes `TEditor` with real local file-system loading and saving.
- Preserves the line-ending format of loaded files.
- Uses `LF` for newly created files.
- Detects when the underlying file changed externally after load and requires an explicit overwrite decision before replacing the on-disk file.
- Does not silently discard modified content.

### `TEditWindow`

- Hosts one editor session inside a framed, desktop-compatible shell view.
- Surfaces document title and supporting editor indicators without requiring modal dialog semantics.
- Remains scoped to editor hosting, not a full general-purpose window framework.

### `TFileDialog`

- Coordinates directory browsing, file listing, manual path entry, and action resolution for open/select/save-target flows.
- Keeps wildcard filters, typed paths, and visible list content synchronized throughout one dialog session.
- Keeps related controls synchronized throughout one dialog session.
- Returns explicit user intent rather than performing document persistence directly.

### `THistory`

- Provides in-application recall of previous entries for a linked field.
- Shares entries only with fields that use the same history identifier.
- Does not require cross-session persistence in this increment.

### `THelpFile`

- Loads runtime help from a dedicated persisted help file.
- Resolves topics by numeric help context.
- Provides fallback behavior when a requested context is missing.
- Does not need to support help-file writing or authoring in this increment.

### `THelpViewer` and `THelpWindow`

- Display help content from `THelpFile`.
- Support scrolling, visible cross-reference selection, and in-session navigation to linked topics.
- Keep runtime help navigation inside one help workflow.

### `pstream`, `ipstream`, `opstream`, `fpstream`

- Provide compatibility stream semantics for primitive values, object registration, shared-reference reconstruction, and file-backed seek/tell behavior.
- Preserve shared references within supported graphs.
- Reject malformed input and unsupported cyclic graphs explicitly.
- Do not promise byte-for-byte compatibility with original Turbo Vision stream files.

### `TResourceFile`

- Stores, retrieves, replaces, removes, and enumerates named persisted objects.
- Uses exact case-sensitive key semantics for lookup and updates.
- Keeps replacement and removal behavior deterministic for repeated keys.

## Behavioral Guarantees

1. **Editor-session guarantee**: A user can create or load a document, edit it, and reach either a successful save or an explicit safe-close decision in one shell session.
2. **Discard-safety guarantee**: Modified content is never discarded through close or replacement flows without an explicit user decision.
3. **Line-ending guarantee**: Loaded files keep their original line endings on save; new files default to `LF`.
4. **Conflict guarantee**: External file modification or explicit target replacement never results in silent overwrite.
5. **History-scoping guarantee**: Recall data is shared only within the same history identifier.
6. **Help-source guarantee**: Runtime help comes from a dedicated help file, not from an unspecified persistence source.
7. **Help-navigation guarantee**: Help context lookup, cross-reference navigation, and missing-context fallback stay inside one responsive runtime help workflow.
8. **Shared-reference guarantee**: Supported persisted object graphs preserve shared-reference identity on read-back.
9. **Cycle-rejection guarantee**: Unsupported cyclic graphs fail explicitly instead of yielding partial or corrupted reconstruction.
10. **Resource-key guarantee**: Resource identifiers are case-sensitive and require exact matches for lookup, replacement, removal, and enumeration.
11. **Scope guarantee**: This increment does not require example-port delivery, macro systems, calculator integration, OS shell execution, or general-purpose help authoring.

## Test Obligations

- Each guarantee must be backed by failing MSTest coverage before production implementation is added.
- `tests/TuiVision.Controls.Tests` must cover editor, file-dialog, history, and help UI behavior.
- `tests/TuiVision.Serialization.Tests` must cover stream primitives, help-file persistence, resource behavior, and explicit malformed-input cases including truncated payloads, trailing data, unknown types, and unsupported cycles.
- Public API additions require bilingual XML documentation and docfx-compatible comments.
