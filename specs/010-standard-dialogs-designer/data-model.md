# Data Model: Standard Dialogs and Designer Readiness

## Overview

This model describes reusable dialog state and persisted dialog-description
state. It does not introduce database storage. Runtime state lives in memory;
history is active-session-only; persisted designer data is limited to a minimal
dialog-description roundtrip.

## StandardDialogFlow

**Purpose**: Common lifecycle for reusable dialog interactions.

**Fields**:
- `DialogKind`: file, directory, color, symbolic charset, display, or designer.
- `InteractionState`: `idle`, `active`, `validated`, `confirmed`, `canceled`,
  or `rejected`.
- `KeyboardReachability`: whether all required actions are reachable by
  keyboard.
- `ValidationMessages`: ordered text-first validation outcomes.
- `Decision`: optional result produced after confirmation.

**Validation rules**:
- Confirmed dialogs must have either a valid decision or an explicit no-value
  outcome.
- Canceled dialogs must not modify the previously committed value.
- Required actions must remain keyboard-reachable even if mouse input is absent.

## FileDialogState

**Purpose**: Synchronized state for file/directory standard dialogs.

**Fields**:
- `CurrentDirectory`
- `SelectionTargetKind`: file or directory.
- `ActiveFilter`
- `VisibleEntries`
- `SelectedEntry`
- `ManualPath`
- `FileInfo`
- `HistoryId`
- `SessionHistoryEntries`
- `ValidationState`
- `DecisionKind`: `open`, `select`, or `save-target`; `select` may target a
  file or directory according to `SelectionTargetKind`.
- `DecisionValue`

**Validation rules**:
- `SelectedEntry`, `ManualPath`, `FileInfo`, and `DecisionValue` must not
  contradict each other when a decision is confirmed.
- `SelectionTargetKind` must match the confirmed file or directory decision.
- Empty `VisibleEntries` must still allow manual path entry and cancellation.
- Unreadable or missing metadata must produce an explicit fallback state.
- Existing or non-writable save targets return validation/decision state only;
  no file content operation happens inside the dialog.
- `SessionHistoryEntries` are partitioned by `HistoryId` and do not survive a
  program restart.

**State transitions**:
`idle` -> `active` -> `validated` -> `confirmed`
`active` -> `canceled`
`active` -> `rejected` -> `active`

## FileDecisionResult

**Purpose**: Explicit outcome returned by a file- or directory-oriented dialog.

**Fields**:
- `Kind`: `open`, `select`, `save-target`, or `canceled`.
- `TargetKind`: `file`, `directory`, or `none` for canceled results.
- `Path`
- `Filter`
- `MetadataSnapshot`
- `RequiresCallerDecision`: true when the caller must handle overwrite or other
  content-level decisions.

**Validation rules**:
- A result never represents completed file I/O.
- `save-target` may point to an existing path, but only as caller-visible
  intent.
- Directory selection returns a directory target and does not imply recursive
  scanning or file content operations.

## ColorDisplaySelectionState

**Purpose**: Synchronized state for color, symbolic charset, and display choices.

**Fields**:
- `SelectionKind`: `color`, `mono`, `symbolic-charset`, or `display`.
- `Group`
- `SelectedValue`
- `PreviewValue`
- `CommittedValue`
- `SupportedOptions`
- `FallbackReason`

**Validation rules**:
- `SelectedValue` must be one of `SupportedOptions` unless an explicit fallback
  state is active.
- Cancel restores `CommittedValue`.
- If no supported option exists, the state must expose a text-first fallback
  reason and preserve the last committed value or return an explicit
  no-supported-option decision.
- Symbolic charset values do not change terminal rendering, fonts, buffers, or
  emulation behavior.

## DialogDescription

**Purpose**: Design-time model consumed by the `dlgdsn` flow.

**Fields**:
- `DescriptionId`
- `Version`
- `Title`
- `Controls`
- `NavigationOrder`
- `CommandBindings`
- `InitialValues`
- `ValidationRules`

**Relationships**:
- Contains many `DialogControlDescription` entries.
- Contains many `DialogCommandBinding` entries.
- Produces one runtime dialog only after validation succeeds.
- May produce one `PersistedDialogRepresentation`.

**Validation rules**:
- Control identifiers are unique within one dialog.
- Command bindings are unique within one dialog.
- Every required label is present.
- Navigation order references only known controls and does not contain
  duplicates.
- Unknown control roles are rejected.
- Unsupported initial or persisted values are rejected.

## DialogControlDescription

**Purpose**: One design-time control entry inside a dialog description.

**Fields**:
- `ControlId`
- `Role`
- `Label`
- `BoundsHint`
- `InitialValue`
- `CanFocus`
- `ValidationRules`

**Validation rules**:
- `ControlId` is required and unique inside the owning description.
- `Label` is required for learner-facing and user-facing controls.
- `Role` must be one of the supported standard dialog/designer roles planned
  for this feature.

## DialogCommandBinding

**Purpose**: Binds one command to one control or dialog-level action.

**Fields**:
- `CommandId`
- `TargetControlId`
- `Meaning`
- `KeyboardTrigger`

**Validation rules**:
- `CommandId` is unique inside one dialog description.
- `TargetControlId` must reference an existing control when present.
- Acceptance-critical commands must be reachable by keyboard.

## PersistedDialogRepresentation

**Purpose**: Minimal stored form of a validated dialog description.

**Fields**:
- `FormatVersion`
- `DescriptionPayload`
- `StoredControlIds`
- `StoredCommandIds`
- `RuntimeStateIncluded`: always false for this feature.

**Validation rules**:
- Malformed or truncated payloads are rejected.
- Unsupported format versions are rejected.
- Unsupported persisted values are rejected.
- Semantic validation runs after reading and before runtime dialog creation.
- Runtime-only state is not persisted.

**State transitions**:
`validated description` -> `persisted representation` -> `loaded description`
`persisted representation` -> `rejected input`

## Wave2ExampleConsumer

**Purpose**: Traceability classification for downstream examples.

**Fields**:
- `ExampleName`: `demo`, `dlgdsn`, `sdlg`, or `sdlg2`.
- `ReusableResponsibilities`
- `ExampleSpecificResponsibilities`
- `ThinConsumerRisk`

**Validation rules**:
- Each affected example must have a reviewable classification before it is
  treated as fully ported.
- Example-specific responsibilities must not duplicate file selection, color
  selection, symbolic charset/display selection, or dialog-description
  validation when the framework provides them.
- `demo` must classify general standard-dialog consumption, `sdlg` and `sdlg2`
  must classify color/charset/display consumption, and `dlgdsn` must classify
  dialog-description and persistence consumption.
