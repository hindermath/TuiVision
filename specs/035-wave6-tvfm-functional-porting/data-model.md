# Data Model: Wave-6 TVFM Functional Porting

## ControlledWorkspace

| Field | Type | Rule |
|---|---|---|
| `RootPath` | absolute path | Canonical, existing, application- or test-owned |
| `Comparison` | enum | Ordinal on case-sensitive platforms; ordinal-ignore-case on Windows |
| `CurrentRelativePath` | relative path | Empty for root; never absolute or traversing |
| `OwnsLifetime` | bool | True only for process-created disposable workspace |
| `Disposed` | bool | No operation accepted after disposal |

**Identity**: canonical root path plus workspace instance.

**Invariant**: Every accepted path resolves below `RootPath`, and every
existing segment from root to target is non-link/non-reparse.

## DirectoryEntry

| Field | Type | Rule |
|---|---|---|
| `RelativePath` | string | Root-relative and normalized |
| `Name` | string | Non-empty leaf name |
| `Kind` | enum | `Directory`, `File` |
| `Size` | long? | Non-negative for files |
| `LastWriteUtc` | DateTime | Metadata snapshot only |
| `ReadOnly` | bool | Observable platform attribute |
| `Tagged` | bool | Application-local selection state |

**Identity**: normalized `RelativePath` within one workspace.

## DirectorySnapshot

| Field | Type | Rule |
|---|---|---|
| `RelativeDirectory` | string | Accepted workspace directory |
| `Entries` | ordered list | Unique paths; directories then selected sort |
| `Filter` | string | Bounded simple wildcard, default `*` |
| `Sort` | enum | `Name`, `Size`, `Modified` |
| `Descending` | bool | Explicit |
| `SelectedRelativePath` | string? | Null for empty snapshot |
| `Status` | string | Visible result or rejection |

## PreviewResult

| Field | Type | Rule |
|---|---|---|
| `RelativePath` | string | Existing controlled file |
| `Kind` | enum | `Text`, `Hex`, `Fallback` |
| `Content` | string | Text-first bounded representation |
| `BytesRead` | int | 0..4096 |
| `Truncated` | bool | True when more bytes or text lines exist |
| `InvalidUtf8` | bool | True when replacement fallback was required |
| `Status` | string | Visible format and bound summary |

## SearchRequest

| Field | Type | Rule |
|---|---|---|
| `StartRelativePath` | string | Controlled directory |
| `Pattern` | string | Non-empty bounded simple wildcard |
| `MaxDepth` | int | Fixed at 8 or lower |
| `MaxFiles` | int | Fixed at 256 or lower |
| `MaxResults` | int | Fixed at 100 or lower |
| `CancellationToken` | token | Checked before each directory and file |

## SearchResult

| Field | Type | Rule |
|---|---|---|
| `Matches` | ordered relative paths | Unique, ordinally stable, at most 100 |
| `VisitedFiles` | int | 0..256 |
| `Canceled` | bool | Explicit partial-result state |
| `LimitReached` | bool | Explicit resource-bound state |
| `Status` | string | Visible completion/cancel/limit summary |

## ViewerAssociation

| Field | Type | Rule |
|---|---|---|
| `Extension` | normalized string | Ordinal, includes leading dot |
| `Decision` | enum | `Text`, `Hex`, `Fallback` |
| `ExternalCommand` | N/A | Must not exist |

## FileOperationIntent

| Field | Type | Rule |
|---|---|---|
| `OperationId` | GUID | One execution authorization |
| `Kind` | enum | `Copy`, `Rename`, `Delete`, `SetReadOnly`, `ClearReadOnly` |
| `SourceRelativePath` | string | Existing controlled file |
| `TargetRelativePath` | string? | Required for copy/rename |
| `SourceLength` | long | Captured validation snapshot |
| `SourceLastWriteUtc` | DateTime | Captured validation snapshot |
| `TargetExists` | bool | Must be false for executable copy/rename |
| `State` | enum | `AwaitingDecision`, `Confirmed`, `Canceled`, `Rejected`, `Executing`, `Completed`, `Failed` |
| `DecisionReason` | string | Visible review/cancel/rejection reason |

**Lifecycle**:

```text
Requested -> Validated -> AwaitingDecision -> Confirmed -> Executing -> Completed
                     \-> Rejected          \-> Canceled              \-> Failed
```

Der Intent beschreibt genau eine Dateiaktion. Vor `Executing` werden Wurzel,
Links, Source-Metadaten und Zielkonflikt erneut geprüft.

The intent describes exactly one file operation. Root, links, source metadata,
and target conflict are revalidated immediately before `Executing`.

## FileOperationResult

| Field | Type | Rule |
|---|---|---|
| `OperationId` | GUID | Resolves to one intent |
| `State` | enum | Terminal intent state |
| `AffectedRelativePaths` | string list | Root-relative only |
| `ProgressPercent` | int | 0..100 |
| `ErrorCode` | string? | Stable feature-local category |
| `RecoveryBoundary` | string | Explicit, including `NoMutation` or resulting state |

## Tp7FileManagerState

| Field | Type | Rule |
|---|---|---|
| `CurrentDirectory` | string | Root-relative |
| `Snapshot` | DirectorySnapshot | Current visible list |
| `SelectedPath` | string? | Current entry |
| `Preview` | PreviewResult? | Last bounded preview |
| `Search` | SearchResult? | Last bounded search |
| `PendingOperation` | FileOperationIntent? | At most one |
| `LastOperation` | FileOperationResult? | Last terminal result |
| `Palette` | enum | Closed `Default`, `Cyan`, `Rose`, `HighContrast` |
| `Status` | string | Text-first current result |
| `DescriptionOpened` | bool | Set only through help event/command |

## HistoricalSourceRole

| Field | Type | Rule |
|---|---|---|
| `SourcePath` | string | One of exactly 24 flat `TVFM/` files |
| `HistoricalPurpose` | string | Non-empty intent summary |
| `PrimaryRole` | enum | Closed seven-term vocabulary from spec |
| `ManagedTarget` | string | Feature-035 app, model, evidence or documented omission |
| `RetainedIntent` | string | Non-empty |
| `IntentionalDeviation` | string | Explicit; `None` allowed |
| `ProofPath` | repository-relative path | Must exist in final candidate |

**Identity**: ordinal `SourcePath`; exactly one row per accepted file.

## FunctionalAreaDecision

| Field | Type | Rule |
|---|---|---|
| `AreaId` | string | Exactly `W6-001` through `W6-010` |
| `Sources` | source path list | Non-empty and resolvable |
| `Decision` | enum | Exact four-term Framework vocabulary |
| `FrameworkContracts` | string list | Existing TuiVision types/flows |
| `LocalDomainLogic` | string | Explicit, `None` allowed |
| `PrimaryProof` | test identity | Real app path or named bounded domain proof |
| `ResidualRisk` | string | Explicit |
| `FollowUpBoundary` | string | Required for non-default decisions |

## Stage2Disposition

| Field | Type | Rule |
|---|---|---|
| `EntryPoint` | string | Exactly `Tp7FileManager` |
| `Disposition` | enum | Exact Stage-2 vocabulary |
| `DeliveredFunction` | string | Non-empty |
| `VisualDelta` | string | Concrete or `None` with proof |
| `InteractionDelta` | string | Concrete or `None` with proof |
| `LayoutDelta` | string | Concrete or `None` with proof |
| `A11yDelta` | string | Concrete or `None` with proof |
| `EvidencePath` | path | Existing feature evidence |

## AutonomousRunCheckpoint

The feature-local state follows autonomous-run-governance v0.2.2. Accepted
artifact hashes refresh at logical boundaries. Tasks and Git remain
authoritative when a persisted index is stale.
