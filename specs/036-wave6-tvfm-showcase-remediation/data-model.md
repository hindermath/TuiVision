# Data Model: Wave-6 TVFM Showcase Remediation

## ShowcaseLayout

| Field | Type | Rule |
|---|---|---|
| `Viewport` | cell rectangle | Normal or constrained, minimum accepted `48x16` |
| `Mode` | enum | `Normal`, `Constrained` |
| `ListRegion` | cell rectangle | Non-empty, visible, focusable |
| `DetailRegion` | cell rectangle | Non-empty or summarized in constrained mode |
| `StatusRegion` | cell row | Real StatusLine, never overlapped |
| `DescriptionAvailable` | bool | Always true through F1 and Help menu |

**Invariant**: Purpose, selected path, next action, status, Description, and
quit remain visible or keyboard-reachable in both modes.

## VisibleFileSelection

| Field | Type | Rule |
|---|---|---|
| `Snapshot` | existing `Wave6DirectorySnapshot` | Controlled workspace result |
| `FocusedIndex` | int | `-1` for empty; otherwise within visible entries |
| `SelectedRelativePath` | string? | Must match focused entry |
| `DisplayRows` | ordered string list | One row per visible entry |
| `FocusText` | string | Text-first current control and selection |

**Identity**: current snapshot plus selected root-relative path.

## VisibleCommandAccess

| Field | Type | Rule |
|---|---|---|
| `CommandId` | ushort | Unique, typed, example-local |
| `Group` | enum | File, Navigate, View, Search, Options, Help |
| `Label` | bilingual text | Non-empty and mnemonic-aware |
| `KeyboardPath` | string | Required for every primary command |
| `Enabled` | bool | Derived from current state |
| `UnavailableReason` | string | Required when disabled |
| `StatusHint` | string | Text-first next-action explanation |

## ShowcaseViewState

| Field | Type | Rule |
|---|---|---|
| `Layout` | ShowcaseLayout | Current viewport composition |
| `Selection` | VisibleFileSelection | Current list/focus state |
| `Preview` | existing `Wave6PreviewResult`? | Bounded text/hex/fallback |
| `Search` | existing `Wave6SearchResult`? | Bounded result/cancel/limit |
| `Palette` | existing `Wave6Palette` | Closed values only |
| `PendingDialog` | OperationDialogState? | At most one |
| `PreparedDrag` | PreparedDragIntent? | At most one |
| `LastOperation` | existing `Wave6OperationResult`? | Terminal result only |
| `Status` | string | Current text-first feedback |
| `FocusedControlKind` | string | Stable concrete control identity |
| `DescriptionOpened` | bool | Set through event/command path |

## OperationDialogState

| Field | Type | Rule |
|---|---|---|
| `Kind` | existing `Wave6OperationKind` | Copy, Rename, Delete, Set/Clear ReadOnly |
| `SourceRelativePath` | string | Existing selected controlled file |
| `InputMode` | enum | `TargetPath`, `LeafName`, `None` |
| `InputValue` | string | Bounded; empty only when input mode is None |
| `NormalizedPreview` | string | Root-relative operation summary |
| `ValidationState` | enum | `Editing`, `Valid`, `Invalid` |
| `ValidationMessage` | string | Text-first and non-empty after validation |
| `Decision` | enum | `Pending`, `Confirmed`, `Canceled` |
| `FocusedControlKind` | string | Input or concrete button |
| `Intent` | existing `Wave6OperationIntent`? | Created only after valid Preview |
| `TerminalResult` | existing `Wave6OperationResult`? | Present after execution/rejection |

**Lifecycle**:

```text
Editing -> Invalid -> Editing
       \-> Valid -> Pending -> Confirmed -> Revalidated -> Completed/Rejected/Failed
                           \-> Canceled -> NoMutation
```

## PreparedDragIntent

| Field | Type | Rule |
|---|---|---|
| `SourceRelativePath` | string | Current selected controlled file |
| `TargetRelativePath` | string? | Root-relative visible target |
| `Phase` | enum | `Idle`, `Pressed`, `Dragging`, `Prepared`, `Canceled` |
| `Origin` | cell point | Within the selected list row |
| `Current` | cell point | Current bounded application coordinate |
| `CancelReason` | string | Required for Canceled |
| `OperationIntent` | existing `Wave6OperationIntent`? | Same shape as keyboard path |

**Invariant**: `Prepared` never implies execution. Confirmation and
workspace revalidation remain mandatory.

## ShowcaseAreaEvidence

| Field | Type | Rule |
|---|---|---|
| `AreaId` | string | Exactly `W6S-001` through `W6S-010` |
| `Scope` | string | Non-empty |
| `Feature035Proof` | test/evidence references | At least one |
| `VisibleAccess` | command/control/dialog reference | Non-empty |
| `NormalProof` | state/view/cell evidence | Non-empty |
| `ConstrainedProof` | state/view/cell evidence | Non-empty |
| `InteractionProof` | focus/status/Description/keyboard evidence | Non-empty |
| `FrameworkContracts` | existing types/flows | Non-empty |
| `LocalComposition` | explicit | `None` allowed only with rationale |
| `HistoricalAlignment` | intent/deviation | Non-empty |
| `Boundaries` | filesystem/A11Y/platform/security | Non-empty |
| `Decision` | enum | Exact four-term framework vocabulary |
| `ResidualRisk` | string | Explicit |
| `ReevaluationTrigger` | string | Explicit |

**Identity**: ordinal `AreaId`; each accepted ID appears exactly once.

## ShowcaseEntryDecision

| Field | Type | Rule |
|---|---|---|
| `EntryPoint` | string | Exactly `Tp7FileManager` |
| `StateProof` | reference | Non-empty |
| `ViewFocusProof` | reference | Non-empty |
| `CellStatusProof` | reference | Non-empty |
| `AppLoopProof` | reference | Non-empty |
| `Decision` | enum | Exact entry-point vocabulary |
| `ResidualRisk` | string | Explicit |
| `ReevaluationTrigger` | string | Explicit |

`ShowcaseComplete` and `IntentionalMinimalSurface` require no open `Gap` or
`ShowcaseDelta`. `FollowUpHardening` requires owner and evidence.
`ProductDecision` blocks delivery.

## AutonomousRunCheckpoint

The feature-local state follows autonomous-run-governance v0.2.2. Accepted
artifact hashes refresh at every logical stage. Tasks and Git remain
authoritative when a persisted index is stale, and any interruption requires
read-only Status followed by explicit Resume authority.
