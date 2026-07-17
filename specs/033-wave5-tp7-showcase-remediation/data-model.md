# Data Model: Wave-5 TP7 Showcase Remediation

## ShowcaseRow

| Field | Type | Rule |
|---|---|---|
| `ExampleId` | enum/string | Exactly one of the ten accepted `Tp7*` examples |
| `FrameworkDecision` | enum | `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening` |
| `MainComponent` | type plus evidence | Concrete visible and focusable view identity |
| `StatusProof` | text plus test | Real `TStatusLine` contains current state |
| `DescriptionProof` | text plus test | Keyboard-reachable purpose/boundary explanation |
| `KeyboardInventory` | command list | Complete core path; no pointer-only action |
| `NormalProof` | test identity | Real app loop, domain state, view identity, cells |
| `ConstrainedProof` | viewport plus test | Stable required text and no overlap |
| `HistoricalIntent` | source list plus summary | Read-only purpose and user flow |
| `IntentionalDeviation` | string | Non-empty modern deviation or `None` |
| `FollowUpBoundary` | owner plus trigger | Required only for follow-up |

**Identity and cardinality**: ordinal `ExampleId`; exactly ten unique rows and
one primary decision per row.

## ShowcaseComposition

| Field | Type | Rule |
|---|---|---|
| `MainView` | `TView` | Concrete visible app-specific component |
| `StatusLine` | `TStatusLine` | Real status view with current text |
| `DescriptionView` | `TWindow` or `TDialog` | Opened by a keyboard-reachable Help command |
| `FocusedView` | `TView` | Non-null for interactive composition |
| `VisibleRegion` | `TRect` | Positive dimensions within desktop bounds |
| `Viewport` | `TRect` | Normal or accepted constrained size |

**Lifecycle**:
`Constructed -> MainVisible -> Focused -> Interacted -> DescriptionVisible -> Quit`.
A rejected domain action remains in `Interacted` with the previous valid
domain state preserved.

## ShortcutEntry

| Field | Type | Rule |
|---|---|---|
| `ExampleId` | example ID | Resolves to one ShowcaseRow |
| `Action` | string | User-visible action name |
| `Key` | string | Non-empty keyboard shortcut |
| `Command` | ushort/name | Existing or Wave-5 presentation command |
| `VisibleOutcome` | string | Status, selection, dialog, or content change |
| `PointerEquivalent` | optional string | Never the only path |

The inventory contains every accepted core action for all ten examples.

## ConstrainedLayoutProof

| Field | Type | Rule |
|---|---|---|
| `ExampleId` | example ID | Exactly one per example |
| `Width` / `Height` | positive int | Declared stable viewport |
| `RequiredTexts` | string list | Purpose, selection/focus, and status |
| `MainRegion` | `TRect` | Positive and within the viewport |
| `StatusRegion` | `TRect` | Does not overlap main required content |
| `DescriptionRegion` | `TRect` | Fits when opened |
| `Result` | enum | `Pass` only with cell evidence |

## DescriptionContent

| Field | Type | Rule |
|---|---|---|
| `PurposeDe` / `PurposeEn` | string | German first, English second, CEFR-B2 |
| `OperationDe` / `OperationEn` | string | Complete keyboard operation |
| `ModernizationDe` / `ModernizationEn` | string | Historical intent and modern deviation |
| `BoundaryDe` / `BoundaryEn` | string | Security, file, parser, or capability boundary |
| `ProofDe` / `ProofEn` | string | What the primary smoke proves and does not prove |

## ControlledBoundaryState

The existing Feature-032 entities remain authoritative:

- calculator rejection preserves the last valid value;
- editor and generator paths stay below the test-owned root;
- Help and Resource rejection publish no partial model;
- calendar and puzzle use deterministic fixtures;
- mouse capability remains `Enabled`, `Disabled`, or `Unsupported`;
- `HostMutationPerformed` remains false.

Showcase composition may display these values but must not redefine them.

## AutonomousRunCheckpoint

The feature-local state follows `schemaVersion: 1.0`. Accepted artifact hashes
are refreshed at logical phase boundaries. `tasks.md`, Git ownership, provider
evidence, and actual process/check state remain authoritative when an index is
stale.
