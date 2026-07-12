# Data Model: Wave-3 Visual Component Porting

The feature adds no business database. These in-process and evidence entities
define visible behavior, controlled ownership, proof, and governance.

## Wave3Example

| Field | Type | Rules |
|---|---|---|
| `ExampleId` | enum | Exactly `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, or `TvHc` |
| `ProjectPath` | path | Unique executable project under `examples/` |
| `HistoricalSources` | list | Read-only `.cc`/header/resource/PO/fixture paths |
| `MainSurface` | descriptor | Real visible view or stable state |
| `StatusSurface` | descriptor | Real `TStatusLine` unless justified equivalent |
| `DescriptionPath` | descriptor | Keyboard-reachable command |
| `PrimaryOperation` | descriptor | Real event/command/key route |
| `FrameworkDecision` | enum | Exactly one accepted value |
| `ControlledArtifacts` | list | Empty or explicitly owned paths/content |
| `ProofRecords` | list | At least one passing primary proof |

## VisibleState

| Field | Type | Rules |
|---|---|---|
| `StateId` | string | Stable per example and operation |
| `MainText` | string | Domain result, not only instruction text |
| `StatusText` | string | Current identity/result/next route |
| `FocusedView` | view reference | Required where focus changes meaning |
| `RenderedRegion` | rectangle | Stable, inside current buffer |
| `FallbackClass` | optional enum | `None`, `Unavailable`, `Rejected`, `Invalid`, `Clipped` |
| `Operation` | string | Event/command/key that produced state |

## ControlledArtifact

| Field | Type | Rules |
|---|---|---|
| `ArtifactId` | string | Unique in evidence |
| `Ownership` | enum | `Embedded`, `SourceControlled`, `TestTemp` |
| `Path` | optional path | Must resolve under allowed root |
| `Access` | enum | `ReadOnly`, `WriteNew`, `OverwriteAfterDecision` |
| `Cleanup` | string | Required for test-temp artifacts |
| `ProofBoundary` | string | States what is and is not accessed |

## HelpDemoState

| Field | Type | Rules |
|---|---|---|
| `Context` | integer | Known topic or explicit unknown value |
| `TopicTitle` | string | Visible title or fallback title |
| `CrossReferenceTarget` | optional integer | Must resolve or show fallback |
| `Hint` | string | Derived from current context/focus |
| `NavigationResult` | enum | `Topic`, `Next`, `Reference`, `Fallback` |

## LocalizationState

| Field | Type | Rules |
|---|---|---|
| `RequestedLanguage` | string | Explicit, never ambient-only |
| `AttemptedLanguages` | ordered list | Stable request/fallback order |
| `ResourceKey` | string | Exact case-sensitive key |
| `MatchedLanguage` | optional string | Empty for missing result |
| `VisibleValue` | string | Translation or honest fallback |
| `FallbackReason` | string | Visible when request is not exact |

## CompilerDemoState

| Field | Type | Rules |
|---|---|---|
| `SourceIdentity` | string | Embedded or source-controlled identity |
| `SourceText` | string | Bounded compiler input |
| `Result` | enum | `Success` or `Rejected` |
| `Diagnostics` | list | Stable code, line, message |
| `TopicCount` | integer | Non-negative; zero on rejected compile |
| `VisibleTopic` | optional string | Must come from compiled help model |
| `OutputArtifact` | optional reference | Test-temp only |

## FrameworkUsageDecision

Allowed values: `UseExistingFramework`, `SmallFrameworkFix`,
`IntentionalDeviation`, `FollowUpHardening`.

- Exactly one primary value per example.
- `SmallFrameworkFix` requires a focused failing test and regression proof.
- `IntentionalDeviation` requires historical source, retained intent, modern
  behavior, rationale, learner effect, and guide/evidence path.
- `FollowUpHardening` requires owner, scope reason, residual risk, follow-up,
  and re-evaluation trigger.

## VisualProofRecord

| Field | Type | Rules |
|---|---|---|
| `ProofId` | string | Stable and unique |
| `ExampleId` | reference | Exactly one Wave3Example |
| `TestMethod` | string | Concrete primary test |
| `AppLoopRoute` | string | Injected event/command/key sequence |
| `ConcreteState` | string | Domain assertion after dispatch |
| `ViewTreeKind` | string | Expected concrete view |
| `RenderedRegion` | string | Buffer/cell assertion |
| `StatusProof` | string | Status assertion |
| `DescriptionProof` | string | Keyboard description assertion |
| `HelperUsage` | enum | `None`, `SetupOnly`, `SupplementalProof` |
| `ProofBoundary` | string | Explicit limit |
| `Result` | enum | `Pending`, `Pass`, `Fail`, `Deferred` |

## GovernanceCheckpoint

| Field | Type | Rules |
|---|---|---|
| `RunId` | string | `019-wave3-visual-component-porting` |
| `PresetName` / `PresetVersion` | string/version | Exact installed preset |
| `Checkpoint` | string | Standard or governance concern |
| `Applicability` | enum | `Applicable`, `N/A`, `Open` |
| `Rationale` / `EvidencePath` | string/path | Always required |
| `Owner` / `Reviewer` / `ReviewDate` | identity/date | Required before completion |
| `Result` / `ResidualRisk` | string | Explicit outcome and risk |
| `FollowUp` / `ReevaluationTrigger` | string | Required for `Open`; trigger required for `N/A` |

## Relationships

```text
Wave3Example 1 --- * VisibleState
Wave3Example 1 --- * ControlledArtifact
Wave3Example 1 --- 1 FrameworkUsageDecision
Wave3Example 1 --- 1..* VisualProofRecord
BHelp/HelpDemo 1 --- * HelpDemoState
I18n 1 --- * LocalizationState
TvHc 1 --- * CompilerDemoState
GovernanceCheckpoint * --- 1 FeatureEvidence
```
