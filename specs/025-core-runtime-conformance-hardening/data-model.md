# Data Model: Core Runtime Conformance Hardening

The entities below are in-process framework contracts or feature evidence. None
is a persisted user-data model.

## EventBoundary (`F001`)

| Field | Type | Rule |
|---|---|---|
| Kind | `TEventKind` | exactly one supported concrete kind |
| Channel | None, Mouse, Keyboard, Message | exactly one channel implied by Kind |
| Payload | typed event payload | valid only for Channel |
| Rejection | exception category and reason | set before dispatch for invalid Kind |

Transition: `RawArguments -> Validated -> Created`; invalid input goes directly
to `Rejected` and never enters dispatch.

## FocusTransitionDecision (`F002`)

| Field | Type | Rule |
|---|---|---|
| Previous | `TView?` | current direct child before request |
| Requested | `TView` | non-null direct member and eligible target |
| Result | `Accepted`, `Rejected`, `NoOp` | exactly one |
| Reason | bounded text/enum | membership, eligibility, same target, or veto |
| Announcement | optional focus snapshot | created only for `Accepted` |

Lifecycle: validate request -> detect NoOp -> ask Previous exactly once ->
atomically mutate Current/Focused -> announce. `Rejected` changes no state.

## ViewStateRule (`F003`)

| State | Direct-child rule | Insert rule |
|---|---|---|
| `Focused` | Current only | only when inserted child is Current |
| `Active` | all direct children | inherit when owner active |
| `Dragging` | all direct children | inherit when owner dragging |
| `Exposed` | visible direct children | inherit only when child visible |
| `Disabled` | no propagation; owner blocks dispatch | no inherited local disable |
| Current | one direct child or null | insertion does not silently replace Current |

## PendingEventSlot (`F004`)

| Field | Type | Rule |
|---|---|---|
| Event | `TEvent?` | zero or one pending event |
| Occupied | bool | derived from Event |
| Publisher | owner/source reference | evidence only, no global registry |

Transitions: `Empty -> Pending -> Drained -> Empty`. Publishing in `Pending`
is rejected; shutdown clears the slot.

## IdleCycle (`F004`)

| Field | Type | Rule |
|---|---|---|
| PendingObserved | bool | checked before physical input |
| PhysicalInputObserved | bool | mouse then keyboard boundary |
| IdleInvoked | bool | true only when neither input source produced an event |
| CpuReleased | bool | true after an idle invocation in production loop |
| ShutdownRequested | bool | prevents further uncontrolled work |

## DesktopStackOperation (`F005`)

| Field | Type | Rule |
|---|---|---|
| Kind | Insert, Top, Next, Tile, Cascade, CloseAll | closed vocabulary |
| Candidates | ordered View snapshot | owner-local and stable for operation |
| Participating | View list | visible/eligible; Tile/Cascade also `Tileable` |
| PreviousFocus | `TView?` | restored or advanced if still eligible |
| Result | immutable operation result | counts, selected view, skipped/vetoed rows |

Geometry never escapes the Desktop extent. Empty and no-participant operations
are successful no-ops with explicit counts.

## CloseRequest (`F006`)

| Field | Type | Rule |
|---|---|---|
| Target | closeable View | direct owner member at request time |
| Trigger | Command, CtrlW, Escape, CloseAll | named source |
| Decision | Closed, Vetoed, NotCloseable, AlreadyDetached | exactly one |
| OwnerAfter | `TGroup?` | null only for completed non-modal close |

## ModalSession (`F006`)

| Field | Type | Rule |
|---|---|---|
| Owner | `TGroup` | exactly one direct modal session at a time |
| Child | modal `TDialog` | direct child during execution |
| InsertedTemporarily | bool | controls removal in cleanup |
| PreviousFocus | `TView?` | restore only if still eligible/member |
| Result | command ID | deterministic completion result |
| State | Created, Running, Completed, Cancelled, Shutdown | closed lifecycle |

Same-owner concurrent/reentrant execution is rejected. Nested sessions are
possible only when the active modal child owns the next modal child.

## CommandContext (`F007`)

| Field | Type | Rule |
|---|---|---|
| Generation | monotonic run-local integer | changes once per refresh |
| ActiveView | deepest focused View or null | snapshot source |
| CommandStates | read-only command-to-bool map | one answer per discovered command |
| Trigger | Focus, EventHandled, Idle, PreDispatch | named refresh reason |

Manual menu/status disablement and context disablement are separate inputs;
effective availability is their conjunction.

## KeyboardIngressTranslation (`F008`)

| Field | Type | Rule |
|---|---|---|
| Raw | `ConsoleKeyInfo` | production or controlled real-boundary input |
| CharCode | char | preserved, including control character |
| ScanCode | byte | canonical Compatibility mapping |
| KeyCode | ushort | canonical composed value |
| ShiftState | flags | Shift `0x0001`, Ctrl `0x0002`, Alt `0x0004` |
| Result | KeyDown, GlobalCommand, MouseSequence, NoEvent | exactly one |

## DragSession (`F009`)

| Field | Type | Rule |
|---|---|---|
| Source | `TView` | owner-attached at start |
| Payload | object? | run-local, never persisted |
| Start / Current | `TPoint` | global character-cell coordinates |
| Threshold | one cell | crossing either axis starts capture |
| Bounds | `TRect` | accepted movement/drop boundary |
| Target | optional target View | must opt in and remain eligible |
| Mode | Pointer or Keyboard | same state machine |
| State | Pending, Captured, Dropped, Rejected, Cancelled | exactly one current state |
| Result | immutable result | final target, point and reason |

Transitions: `Pending -> Captured -> Dropped|Rejected|Cancelled`. Owner removal,
disable, capability loss, shutdown or Escape ends in `Cancelled` and releases
capture. Enter attempts drop in keyboard mode.

## FindingEvidence

| Field | Rule |
|---|---|
| FindingId / ContractId | exactly one of F001-F009 and matching contract |
| Decision | `Implemented` or `AlreadySatisfied` |
| RedProof | command/test and expected failure before change |
| Change | exact files and bounded behavior |
| RealPathProof | test/command beginning at required real boundary |
| HistoricalIntent | own-word Turbo Vision summary and paths |
| FreeVisionRelation | own-word secondary relation and source IDs |
| ModernRationale | why managed C# differs or aligns |
| ApiImpact / A11YImpact / PlatformBoundary | explicit result or justified N/A |
| ResidualBoundary / Result | remaining non-goal and Pass/Fail |

## GovernanceDecision

`Applicable`, `N/A`, or `Open`, plus run ID, preset/version, checkpoint,
rationale, evidence path, owner, reviewer, review date, result, residual risk,
follow-up, and re-evaluation trigger. `N/A` requires rationale and trigger;
`Open` additionally requires a concrete owner and follow-up.
