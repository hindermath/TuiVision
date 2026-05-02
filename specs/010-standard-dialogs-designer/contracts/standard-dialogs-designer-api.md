# Contract: Standard Dialogs and Designer Readiness

## Purpose

This contract records observable framework responsibilities for `010`. It does
not freeze final private helper names. Public API changes must keep German-first
and English-second XML documentation complete.

## File And Directory Dialog Contract

- Provides reusable open, select, and save-target decision flows.
- Covers both file targets and directory targets. A returned select decision
  must identify whether the selected path is a file or directory.
- Synchronizes current directory, active filter, visible entries, selected
  entry, manual path, metadata, session history, validation state, and returned
  result.
- Keeps history session-scoped and partitioned by history identifier.
- Returns explicit caller-visible decisions.
- Does not perform file loading, writing, deletion, or overwrite operations.
- Provides text-first validation outcomes for invalid manual paths, empty
  filtered lists, unreadable metadata, stale entries, existing save targets, and
  non-writable save targets.
- Supports full keyboard operation for all acceptance-critical actions.

## Color, Charset, And Display Dialog Contract

- Provides one coherent selection flow for color/display-related choices.
- Supports symbolic charset choice as a returned value for wave-2 dialog
  traceability.
- Does not change terminal rendering, fonts, buffers, or emulation behavior.
- Keeps selected value, preview/display value, cancellation, and confirmation
  synchronized.
- Provides a bounded fallback when no supported option exists: the flow must
  expose a text-first fallback reason, preserve the committed value or return an
  explicit no-supported-option decision, and avoid any example-local
  reinterpretation of unsupported state.
- Supports full keyboard operation for all acceptance-critical actions.

## Dialog Description Contract

- Provides a design-time description model for `dlgdsn`-style dynamic dialogs.
- Separates dialog description, validated runtime dialog, and persisted
  representation.
- Requires unique control identifiers within one dialog.
- Requires unique command bindings within one dialog.
- Validates labels, navigation order, command targets, supported control roles,
  initial values, and persisted values before runtime creation.
- Rejects invalid descriptions without producing a partial runtime dialog.
- Supports full keyboard operation for designer flows.

## Persisted Description Contract

- Provides a minimal roundtrip for validated dialog descriptions.
- Uses project-owned serialization/resource boundaries.
- Excludes runtime-only state from persisted payloads.
- Rejects malformed, truncated, unsupported-version, and semantically invalid
  persisted input before runtime dialog creation.
- Does not introduce a broad new persistence framework, database, or external
  dependency.

## Wave-2 Consumer Contract

- `demo`, `sdlg`, `sdlg2`, and `dlgdsn` are downstream consumers for this
  feature.
- Each affected example must have a reviewable classification of reusable
  framework responsibilities versus intentionally example-specific behavior.
- Full example porting is outside `010`.
- Later example code must not duplicate framework-provided file selection,
  color/display/charset selection, or dialog-description validation.

## Test Obligations

- Controls tests must prove file decisions, color/display/charset decisions,
  directory decisions, keyboard operation, cancellation/confirmation,
  validation, and consumer classification.
- Serialization tests must prove persisted-description roundtrip and malformed
  input rejection.
- Tests must demonstrate that wave-2 example smoke coverage is not the primary
  acceptance vehicle for this feature.
