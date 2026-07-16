# Data Model: Wave-5 TP7 Functional Porting

## HistoricalSourceRole

| Field | Type | Rule |
|---|---|---|
| `SourcePath` | string | One of exactly 15 `TVDEMOS/*.PAS` paths |
| `HistoricalPurpose` | string | Non-empty intent summary |
| `PrimaryRole` | enum | `EntryPoint`, `SupportUnit`, `FixtureOrContent`, `GeneratorIntent`, `IntentionalOmission` |
| `ManagedTarget` | string | Existing Feature-032 example or shared support area |
| `IntentionalDeviation` | string | Non-empty when historical mechanism is not retained |
| `ProofPath` | repository-relative path | Must exist in final candidate |

**Identity**: ordinal `SourcePath`; exactly one row per accepted source.

## ConsumerDecision

| Field | Type | Rule |
|---|---|---|
| `ConsumerId` | string | Exactly `W5-001` through `W5-006` |
| `Sources` | source ID list | Non-empty and resolves to source rows |
| `ManagedExamples` | example ID list | Non-empty |
| `Decision` | enum | `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, `FollowUpHardening` |
| `FrameworkContracts` | string list | Existing TuiVision types/flows |
| `RealPathProof` | path plus test name | Must execute app-loop or equivalent dispatch |
| `ResidualRisk` | string | Explicit, `None` allowed |
| `FollowUpBoundary` | string | Required for non-default decisions |

**Identity**: `ConsumerId`; exactly one primary decision per row.

## ManagedExample

| Field | Type | Rule |
|---|---|---|
| `ExampleId` | enum/string | One of exactly ten `Tp7*` names |
| `ProjectPath` | path | Independent executable `.csproj` |
| `ApplicationType` | string | Public app type in shared Wave-5 assembly |
| `PurposeText` | string | Visible at normal start |
| `StateText` | string | Current deterministic state |
| `KeyboardCommand` | ushort | At least one core path |
| `MousePath` | optional event | Only `Tp7MouseDialog` requires it |
| `GuidePath` | path | DE-first/EN-second guide |
| `PrimaryProof` | test identity | `app.Run()` plus state/view/cell |

**Lifecycle**: `Created -> Visible -> Interacted -> Exited`. A rejected action
remains in `Visible` or `Interacted` with previous valid state preserved.

## CalculatorState

| Field | Type | Rule |
|---|---|---|
| `Display` | decimal | Last valid value |
| `PendingOperand` | decimal? | Left side of pending operation |
| `PendingOperation` | enum? | Add, Subtract, Multiply, Divide |
| `EntryText` | string | Invariant decimal text |
| `Mode` | enum | `Valid`, `Error` |
| `Status` | string | Visible operation/rejection summary |

Division by zero transitions to `Error` without replacing `Display`.
Clear transitions to `Valid` with zero.

## CalendarState

| Field | Type | Rule |
|---|---|---|
| `SelectedMonth` | `DateOnly` | Always day 1; fixed initial fixture |
| `Grid` | immutable cell list | Complete displayed month |
| `Status` | string | Invariant `yyyy-MM` plus navigation result |

Month navigation normalizes year rollover. Host date, locale and timezone are
not inputs.

## AsciiTableState

| Field | Type | Rule |
|---|---|---|
| `SelectedCode` | int | Inclusive 0..255 |
| `Character` | char/string | Printable representation or control label |
| `DecimalText` | string | Invariant decimal |
| `HexText` | string | Two-digit uppercase hex |
| `Status` | string | Visible selection |

Navigation clamps at boundaries; invalid direct codes are rejected.

## PuzzleState

| Field | Type | Rule |
|---|---|---|
| `Tiles` | immutable 16-value sequence | Values 0..15 exactly once; 0 is blank |
| `BlankIndex` | int | 0..15 and matches tile 0 |
| `MoveCount` | int | Non-negative |
| `Status` | string | Accepted or rejected move |

Only a tile sharing one orthogonal edge with the blank may move. The initial
fixture and scripted move sequence are fixed.

## HelpProofState

| Field | Type | Rule |
|---|---|---|
| `CompilationSucceeded` | bool | True only with complete `THelpFile` |
| `DiagnosticCode` | string? | Stable first diagnostic on rejection |
| `CurrentContext` | int | Known context or explicit unknown fixture |
| `CurrentTitle` | string | Topic title or fallback text |
| `NoPartialModel` | bool | Required true on invalid compilation |

## ResourceProofState

| Field | Type | Rule |
|---|---|---|
| `Keys` | ordinal string set | Exact, unique, case-sensitive |
| `AllowedRecordKinds` | closed enum | Existing registered built-in records only |
| `GeneratedPath` | string? | Must remain below test-owned root |
| `LoadSucceeded` | bool | True only after complete atomic load |
| `RejectionReason` | string? | Visible on unknown/duplicate/invalid input |

## MouseDialogState

| Field | Type | Rule |
|---|---|---|
| `Capability` | enum/string | Supported or honest Unsupported |
| `DoubleClickDelayStep` | int | Local bounded example value |
| `ButtonsReversed` | bool | Local example value only |
| `LastActivation` | string | Keyboard, mouse or fallback |
| `HostMutationPerformed` | bool | Must remain false |

## ShowcaseDelta

| Field | Type | Rule |
|---|---|---|
| `ExampleId` | managed example ID | Exactly one row per example |
| `Disposition` | enum | `CompleteIn032` or `Stage2Required` |
| `DeliveredFunction` | string | Non-empty |
| `VisualDelta` | string | Concrete or `None` with evidence |
| `InteractionDelta` | string | Concrete or `None` with evidence |
| `LayoutDelta` | string | Concrete or `None` with evidence |
| `A11yDelta` | string | Concrete or `None` with evidence |
| `Priority` | enum | P1, P2 or P3 when Stage 2 is required |
| `EvidencePath` | path | Existing proof |

## AutonomousRunCheckpoint

The feature-local state follows the installed schema. Accepted artifact hashes
are refreshed at logical phase boundaries. Tasks and Git remain authoritative
if a state index becomes stale.
