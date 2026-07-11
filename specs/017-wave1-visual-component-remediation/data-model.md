# Data Model: Wave-1 Visual Component Remediation

The feature has no persistent data model. These in-process and evidence entities
define runtime state, proof traceability, and governance acceptance.

## VisualExampleArea

| Field | Type | Rules |
|---|---|---|
| `AreaId` | string | Unique: `Desklogo`, `MsgCls`, `Tutorial`, or `Videomode` |
| `HistoricalSources` | list of paths | Read-only paths; at least one primary source |
| `FunctionalBaseline` | evidence link | Must point to feature 014 |
| `MainSurface` | descriptor | Real visible component or stable state |
| `StatusSurface` | descriptor | `TStatusLine` by default; exception requires rationale |
| `DescriptionPath` | descriptor | Keyboard-reachable Help/Description/About |
| `PrimaryOperation` | descriptor | Event, command, key, startup selection, or quit route |
| `FrameworkDecision` | enum | Exactly one allowed decision |
| `ProofRecords` | list | At least one primary visual proof |
| `FollowUpBoundary` | optional | Required only for deferred issue |

## TutorialStepVisual

| Field | Type | Rules |
|---|---|---|
| `Token` | string | Unique `tvguid01` through `tvguid16` |
| `Sequence` | integer | 1 through 16 without gaps |
| `LearningGoalDe` | string | German-first CEFR-B2 text |
| `LearningGoalEn` | string | English CEFR-B2 companion |
| `RepresentativeKind` | string | Component/view/state family for the defining lesson |
| `VisibleResult` | string | Must differ meaningfully from other tokens |
| `StatusMessage` | string | Names step and next operation |
| `Description` | string | Explains source intent and modern boundary |
| `HistoricalSource` | path | Matching `tvguidNN.cc` |
| `PrimaryProof` | evidence link | App-loop, state, view, and buffer/cell proof |

### Tutorial Validation

- Exactly 16 records exist.
- Token and sequence are one-to-one.
- Default launch selects and names the documented default.
- Unknown token enters a visible fallback state and is not counted as a valid
  step record.
- Repeated representative kinds are allowed only when the visible state and
  defining learning result remain distinct.

## VisualProofRecord

| Field | Type | Rules |
|---|---|---|
| `ProofId` | string | Stable identifier |
| `AreaOrToken` | reference | One visual area or Tutorial token |
| `TestMethod` | string | Concrete primary smoke method |
| `AppLoopRoute` | string | Event/command/key sequence |
| `ConcreteState` | string | State assertion after dispatch |
| `ViewTreeKind` | string | Expected visible control/view kind |
| `RenderedRegion` | string | Stable region/cell assertion |
| `StatusProof` | string | Status line assertion |
| `DescriptionProof` | string | Keyboard description assertion |
| `DirectHelperUsage` | enum | `None`, `SetupOnly`, or `SupplementalProof` for primary rows |
| `ProofBoundary` | string | What the test proves and does not prove |
| `Result` | enum | `Pending`, `Pass`, `Fail`, `Deferred` |

### Proof State Transitions

```text
Pending -> Pass
Pending -> Fail -> Pending
Pending -> Deferred
```

`Deferred` is allowed only with a technically impossible proof layer, substitute
proof, owner, follow-up, and re-evaluation trigger. A feature cannot complete
with a deferred primary visible state for an entire example area.

## FrameworkUsageDecision

### Allowed States

- `UseExistingFramework`: Existing controls and framework behavior are enough.
- `SmallFrameworkFix`: A narrow shared framework defect is fixed with focused tests.
- `IntentionalDeviation`: Modern behavior intentionally differs and is documented.
- `FollowUpHardening`: The issue is real but outside feature 017.

### Rules

- One primary decision per example or shared contract area.
- `SmallFrameworkFix` requires failing proof, changed framework path, and focused
  regression test.
- `IntentionalDeviation` requires historical source, rationale, visible effect,
  and guide/evidence link.
- `FollowUpHardening` requires issue, scope reason, owner or tracked boundary,
  follow-up, residual risk, and re-evaluation trigger.

## GovernanceCheckpoint

| Field | Type | Rules |
|---|---|---|
| `RunId` | string | Feature/run identifier |
| `PresetName` | string | One installed preset |
| `PresetVersion` | version | Exact local version |
| `Checkpoint` | string | Standard or governance concern |
| `Applicability` | enum | `Applicable`, `N/A`, or `Open` |
| `Rationale` | string | Mandatory for all rows |
| `EvidencePath` | path | Existing or feature evidence |
| `Owner` | string | Required |
| `Reviewer` | string | Required |
| `ReviewDate` | date | Required before completion |
| `Result` | string | Outcome or bounded pending state |
| `ResidualRisk` | string | Explicit, including `None identified` |
| `FollowUp` | string | Required for `Open` |
| `ReevaluationTrigger` | string | Required for `N/A` and `Open` |

## ValidationRun

| Field | Type | Rules |
|---|---|---|
| `Command` | string | Exact executed command |
| `Version` | string | `1.17.patch.build` used for build/test |
| `Scope` | string | Targeted, full, coverage, docs, A11Y, or platform |
| `Result` | string | Pass/fail/skipped with reason |
| `Evidence` | string | Counts, coverage, warning/error totals, or proof boundary |
| `GeneratedOutputHygiene` | string | Required for DocFX/test artefacts |

## Relationships

```text
VisualExampleArea 1 --- * VisualProofRecord
VisualExampleArea 1 --- 1 FrameworkUsageDecision
VisualExampleArea Tutorial 1 --- 16 TutorialStepVisual
TutorialStepVisual 1 --- 1..* VisualProofRecord
GovernanceCheckpoint * --- 1 FeatureEvidence
ValidationRun * --- 1 FeatureEvidence
```
