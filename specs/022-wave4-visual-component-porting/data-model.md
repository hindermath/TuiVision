# Data Model: Wave-4 Visual Component Porting

The feature adds no database. These in-process and evidence entities define
visible state, controlled assets, platform claims, proof, and governance.

## Wave4Example

| Field | Type | Rules |
|---|---|---|
| `ExampleId` | enum | Exactly `Cyrillic`, `ETerm`, `Fonts`, `Terminal`, or `XTerm` |
| `ProjectPath` | path | Unique executable project under `examples/` |
| `HistoricalSources` | list | Read-only implementation/header/config/resource/script/fixture paths |
| `MainSurface` | descriptor | Real visible view or stable visible fallback |
| `StatusSurface` | descriptor | Dynamic status line/equivalent |
| `DescriptionPath` | descriptor | Keyboard reachable |
| `PrimaryOperation` | descriptor | Real event/command/key route |
| `FrameworkDecision` | enum | Exactly one accepted value |
| `Assets` | list | Embedded or source-controlled/read-only only |
| `ProofRecords` | list | At least one passing primary proof |

## VisibleTerminalState

| Field | Type | Rules |
|---|---|---|
| `SessionId` | string | Stable controlled identity |
| `Lifecycle` | enum | Active/Closed/Disposed/CapabilityLost |
| `Cursor` | point | Inside visible buffer |
| `Attributes` | value | Current 16-color attributes |
| `VisibleCells` | snapshot | Independent deterministic cells |
| `ProfileId` / `CharsetId` / `FontId` | string | Requested/effective identity remains distinguishable |
| `LastOutcome` | enum | Accepted/Rejected/Unsupported/Fallback |
| `Status` | string | Text-first current state and next route |

## CharacterGridState

| Field | Type | Rules |
|---|---|---|
| `SourceCharset` | enum | Unicode/KOI8-R/Unsupported |
| `SourceValue` | integer | Exact displayed source value |
| `Glyph` | character | Mapped scalar or U+FFFD |
| `Outcome` | enum | Mapped/Replaced/Invalid/Unsupported |
| `GridPosition` | point | Stable proof location |
| `Reason` | string | Required for non-exact result |

## FontGridState

| Field | Type | Rules |
|---|---|---|
| `FixtureId` | string | Source-controlled identity |
| `Width` / `Height` | integer | Exactly 8/16 |
| `GlyphCount` | integer | Exactly 256 |
| `BytesPerGlyph` | integer | Exactly 16 |
| `TotalBytes` | integer | Exactly 4,096 |
| `SelectedGlyph` | integer | 0..255 and nonblank for primary proof |
| `RowBytes` | immutable bytes | Exactly 16 copied bytes |
| `VisiblePixels` | cell region | Recognizable and non-color-only |
| `ValidationOutcome` | enum | Valid/Invalid/Unsupported |

## ResourceManifestEntry

| Field | Type | Rules |
|---|---|---|
| `ManifestId` | enum | ETerm or XTerm |
| `Key` | string | Unique, exact, stable |
| `Value` | string | Controlled representative value |
| `Category` | enum | Menu/Theme/Presentation/Resource/Sequence/Capability |
| `HistoricalSource` | path | Read-only source identity |
| `SupportState` | enum | Demonstrated/Informational/Unsupported |
| `Deviation` | string | Required for native parser/resource omission |

## HostEvidenceRecord

| Field | Type | Rules |
|---|---|---|
| `HostFamily` | enum | macOS/Linux/Windows/WSL/Headless/Unknown |
| `Condition` | string | Interactive/redirected/capability detail |
| `EvidenceClass` | enum | DeterministicInProcess/RemoteCI/PhysicalObservation |
| `Capability` | enum | Enabled/Disabled/Unsupported/NotRun |
| `Result` | enum | Pass/Fail/NotRun |
| `ResidualRisk` | string | Required for NotRun/Unsupported |
| `ReevaluationTrigger` | string | Required unless fully closed |

## FrameworkUsageDecision

| Field | Type | Rules |
|---|---|---|
| `ExampleId` | enum | Exactly one row per example |
| `Decision` | enum | UseExistingFramework/SmallFrameworkFix/IntentionalDeviation/FollowUpHardening |
| `ExistingComponents` | list | Exact reused contracts |
| `LocalLogic` | string | Presentation/composition only unless justified |
| `Rationale` | string | Required |
| `EvidencePath` | path | Required |
| `ResidualRisk` | string | Explicit, including None |
| `FollowUp` | string | Required for FollowUpHardening |

## VisualProofRecord

| Field | Type | Rules |
|---|---|---|
| `ExampleId` / `ProofId` | string | Unique |
| `AppLoopRoute` | string | Real run/dispatch path |
| `Operation` | string | Injected event/command/key |
| `ConcreteState` | string | Domain assertion |
| `ViewIdentity` | string | Exact visible view type/role |
| `RenderedRegion` | rectangle | Stable in current buffer |
| `ExpectedCells` | string/list | At least one concrete visible assertion |
| `Status` / `Description` | string | Both required |
| `HelperRole` | enum | PrimaryProof/SupplementalProof/SetupOnly |
| `Result` | enum | Pass/Fail/Open |

## GovernanceCheckpoint

| Field | Type | Rules |
|---|---|---|
| `RunId` / `PresetName` / `PresetVersion` | string | Required |
| `Checkpoint` | string | Unique within preset/run |
| `Applicability` | enum | Applicable/N/A/Open |
| `Rationale` / `EvidencePath` | string/path | Required |
| `Owner` / `Reviewer` / `ReviewDate` | identity/date | Required |
| `Result` / `ResidualRisk` | string | Required |
| `FollowUp` / `ReevaluationTrigger` | string | Trigger required for N/A/Open |

## Relationships

```text
Wave4Example 1 --- * VisibleTerminalState | CharacterGridState | FontGridState | ResourceManifestEntry
Wave4Example 1 --- 1 FrameworkUsageDecision
Wave4Example 1 --- 1..* VisualProofRecord
Wave4Example * --- * HostEvidenceRecord
GovernanceCheckpoint * --- 1 FeatureEvidence
```
