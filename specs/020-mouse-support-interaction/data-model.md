# Data Model: Mouse Support and Interaction Hardening

The feature adds no persistent business data. These in-process and evidence
entities define the bounded input, interaction, proof, and governance states.

## MouseCapability

| Field | Type | Rules |
|---|---|---|
| `State` | enum | `Enabled`, `Disabled`, or `Unsupported` |
| `HostFamily` | enum | `MacOS`, `Linux`, `Wsl`, `WindowsConsole`, `Headless`, `Unknown` |
| `Protocol` | enum | `Sgr1006` or `None` |
| `Reason` | string | Non-empty bilingual/text-first status source |
| `ReevaluationTrigger` | string | Required for `Unsupported` |

Transitions: `Disabled -> Enabled` only after host checks; any failure or
shutdown reaches `Disabled` or `Unsupported` and clears pressed/click state.

## HostMouseObservation

| Field | Type | Rules |
|---|---|---|
| `RawSequence` | bounded string | Complete SGR report within configured maximum |
| `TimestampMs` | long | Monotonic, non-negative |
| `BufferWidth/Height` | integer | Positive current dimensions |
| `TargetId` | optional string | Stable key returned by caller-supplied point resolver; no Controls object crosses into Driver |
| `Trust` | constant | Always `Untrusted` until validation completes |

## MouseIngressState

| Field | Type | Rules |
|---|---|---|
| `PressedButton` | enum | `None` or `Left` in Feature 020 |
| `LastPosition` | point | Valid only while pressed |
| `LastClickTimeMs` | optional long | Cleared on capability reset/clock regression |
| `LastClickPosition` | optional point | Exact cell equality required |
| `LastClickTargetId` | optional string | Exact target equality required |
| `SequenceState` | enum | `Idle` or complete-observation processing only |

## CanonicalMousePublication

| Field | Type | Rules |
|---|---|---|
| `Kind` | enum | `MouseDown`, `MouseMove`, or `MouseUp` |
| `Buttons` | enum | `Left` while pressed; release semantics documented |
| `Where` | point | Zero-based and inside current buffer |
| `DoubleClick` | bool | True only on qualifying second `MouseDown` |
| `PublicationCount` | integer | Exactly `0` or `1` per observation |

## DragSession

| Field | Type | Rules |
|---|---|---|
| `Target` | `TWindow` reference | Movable, visible, owned, not disabled |
| `StartPointer` | point | Press on title row |
| `StartBounds` | rectangle | Used for delta and cancellation restore |
| `CurrentBounds` | rectangle | Clamped fully inside owner extent |
| `State` | enum | `Idle`, `Dragging`, `Committed`, `Cancelled` |
| `EndReason` | enum | `Release`, `Escape`, `CapabilityLoss`, `Disabled`, `Removed`, `Shutdown` |

## InteractionProofRecord

| Field | Type | Rules |
|---|---|---|
| `ProofId` | string | Stable and unique |
| `Route` | string | Raw sequence and/or keyboard events through `app.Run()` |
| `TargetIdentity` | string | Concrete expected view/window |
| `FocusBefore/After` | string | Explicit ownership assertion |
| `CommandCount` | integer | Exactly-once where activation applies |
| `DragState/Bounds` | string | Required for drag cases |
| `KeyboardEquivalent` | string | Required for every mandatory operation |
| `VisibleStatus` | string | Text-first assertion |
| `RenderedRegion` | rectangle/text | Stable buffer/cell proof |
| `Result` | enum | `Pending`, `Pass`, `Fail`, `Deferred` |
| `ProofBoundary` | string | Distinguishes injected, app-loop, and physical-host proof |

## HostEvidenceRecord

| Field | Type | Rules |
|---|---|---|
| `Host` | enum | macOS, Linux, WSL, native Windows Console |
| `Terminal` | string | Named when known |
| `Capability` | enum | Capability state |
| `EvidenceClass` | enum | `DeterministicInjection`, `PhysicalSpotCheck`, `RemoteCI`, `NotRun` |
| `Result` | enum | `Pass`, `Unsupported`, `NotRun`, `FollowUpHardening` |
| `ResidualRisk` | string | Always explicit |
| `ReevaluationTrigger` | string | Required unless result is a physical pass |

## FrameworkUsageDecision

Allowed values are exactly `UseExistingFramework`, `SmallFrameworkFix`,
`IntentionalDeviation`, and `FollowUpHardening`. Each contract area receives
one value. Fixes require focused red and regression proof; deviations require
historical intent and learner impact; follow-ups require owner and trigger.

## GovernanceCheckpoint

| Field | Type | Rules |
|---|---|---|
| `RunId` | string | `020-mouse-support-interaction` |
| `PresetName/Version` | string/version | Exact installed preset |
| `Checkpoint` | string | Named governance concern |
| `Applicability` | enum | `Applicable`, `N/A`, or `Open` |
| `Rationale/EvidencePath` | string/path | Always required |
| `Owner/Reviewer/ReviewDate` | identity/date | Required before completion |
| `Result/ResidualRisk` | string | Explicit |
| `FollowUp/ReevaluationTrigger` | string | Required for `Open`; trigger required for `N/A` |

## Relationships

```text
MouseCapability 1 --- 1 MouseIngressState
HostMouseObservation 1 --- 0..1 CanonicalMousePublication
CanonicalMousePublication * --- 0..1 DragSession
InteractionProofRecord * --- 1 FeatureEvidence
HostEvidenceRecord * --- 1 FeatureEvidence
FrameworkUsageDecision * --- 1 FeatureEvidence
GovernanceCheckpoint * --- 1 FeatureEvidence
```
