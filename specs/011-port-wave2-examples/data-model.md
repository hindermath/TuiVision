# Data Model: Port Wave 2 Examples

## Overview

This model describes planning entities for example ports, smoke proof, guides,
dialog flows, and completion evidence. It introduces no database storage.
Runtime example state remains in memory; source-controlled proof artifacts live
in examples, tests, guides, and docs.

## Wave2Example

**Purpose**: One required managed example for the controls-and-dialogs wave.

**Fields**:
- `Name`: one of `clipboard`, `demo`, `dlgdsn`, `dyntxt`, `inplis`, `listvi`,
  `progba`, `sdlg`, `sdlg2`, `tcombo`, or `tprogb`.
- `ManagedProjectPath`
- `OriginalSourcePath`
- `HistoricalPurpose`
- `AcceptanceScope`
- `GuidePath`
- `SmokeTestClass`
- `Status`: `planned`, `implemented`, `smoke-tested`, `documented`, or
  `accepted`.
- `AcceptedLimitations`

**Validation rules**:
- Every wave-2 checklist item maps to exactly one managed example project.
- Every managed example has one guide and at least one deterministic smoke
  scenario.
- Wave-3 and wave-4 examples cannot satisfy wave-2 status.

## ExampleProjectRegistration

**Purpose**: Solution and test-project registration for one example.

**Fields**:
- `ProjectPath`
- `AssemblyName`
- `RootNamespace`
- `SolutionEntry`
- `SmokeTestProjectReference`
- `FrameworkReferences`

**Validation rules**:
- Example projects use the repository-wide .NET 10/C# defaults.
- Example projects reference existing framework modules instead of duplicating
  shared behavior.
- The smoke-test project references every delivered example project.

## SmokeScenario

**Purpose**: Deterministic in-process proof for one example behavior.

**Fields**:
- `ExampleName`
- `StartupState`
- `Interaction`
- `VisibleResult`
- `FailureMode`
- `HeadlessModeRequired`
- `CrossPlatformNotes`
- `BoundaryInputs`
- `A11YReviewPath`

**Validation rules**:
- Startup plus clean exit is not sufficient.
- Each scenario triggers at least one example-specific deterministic
  interaction.
- Assertions verify visible state or observable public example state, not
  private test-only implementation details.
- Progress scenarios avoid wall-clock timing dependence.
- Boundary inputs cover empty, minimal, first/last, constrained-width, or
  maximum-value states where the example family supports them.
- Smoke output remains text-first and keyboard-first.

## ExampleGuide

**Purpose**: Learner-facing documentation for one example.

**Fields**:
- `Path`
- `GermanSection`
- `EnglishSection`
- `HistoricalSourceReference`
- `ExpectedInteractionPath`
- `A11YNotes`
- `ValidationCommand`

**Validation rules**:
- German content appears before English content.
- Language targets CEFR-B2 readability.
- The guide remains useful in text-first assistive setups.
- The guide names the smoke or run path a reviewer can use.

## DialogFlow

**Purpose**: User-visible dialog journey demonstrated by `demo`, `dlgdsn`,
`sdlg`, or `sdlg2`.

**Fields**:
- `FlowKind`: `standard-dialog`, `dynamic-dialog`, or `scrollable-dialog`.
- `InitialState`
- `Interaction`
- `ResultState`
- `CancelOrRejectState`
- `VisibleText`

**Validation rules**:
- Standard-dialog flows demonstrate decisions and validation, not file content
  operations.
- Standard-dialog ownership is fixed to `demo` and `dlgdsn`; no third example
  is permitted as a standard-dialog acceptance vehicle in wave 2.
- Dynamic-dialog flows validate structured descriptions before rendering.
- Scrollable-dialog flows prove scrolling, focus movement, bounds, and visible
  control state.
- Dialog flows define visible success and non-success states, including cancel,
  close, invalid selection, or failed validation where applicable.

## ScrollableDialogFlow

**Purpose**: Acceptance model for `sdlg` and `sdlg2`.

**Fields**:
- `ExampleName`: `sdlg` or `sdlg2`.
- `ScrollAxes`: vertical for `sdlg`; horizontal and vertical for `sdlg2`.
- `FocusableControls`
- `Bounds`
- `CurrentScrollPosition`
- `VisibleControlState`

**Validation rules**:
- `sdlg` must prove vertical scrolling.
- `sdlg2` must prove combined horizontal and vertical scrolling.
- Focus movement remains deterministic and keyboard-reachable.
- The flow is complete in wave 2 and not deferred.

## StructuredDialogDescription

**Purpose**: `dlgdsn` description used to create or load a dynamic dialog.

**Fields**:
- `DescriptionId`
- `Title`
- `Controls`
- `NavigationOrder`
- `CommandBindings`
- `InitialValues`
- `ValidationRules`
- `PersistedRepresentation`

**Validation rules**:
- Control IDs are unique within one description.
- Required labels are present.
- Navigation references known controls.
- Invalid or incomplete descriptions are visibly rejected.
- One simple modification is demonstrable before or after rendering.

## ProgressFlow

**Purpose**: Progress behavior for `progba` and `tprogb`.

**Fields**:
- `ExampleName`
- `CurrentValue`
- `MaximumValue`
- `CompletionState`
- `CancellationState`
- `VisibleText`

**Validation rules**:
- `progba` reaches deterministic completion.
- `tprogb` demonstrates progress plus abort/canceled state.
- Tests must not depend on uncontrolled timers or wall-clock delay.

## InteractionFamilyCoverage

**Purpose**: Acceptance mapping from required interaction families to examples
and proof paths.

**Fields**:
- `FamilyName`: `clipboard`, `list-input-history`, `combo`, `progress`,
  `dynamic-text`, `scrollable-dialog`, `standard-dialog`, `dynamic-dialog`, or
  `broad-demo`.
- `OwningExamples`
- `SmokeScenarios`
- `GuidePaths`
- `VisibleProof`
- `BoundaryOrFailureCases`

**Validation rules**:
- Every family named in `SC-004` maps to at least one owning wave-2 example.
- No family is satisfied only by startup or clean exit.
- Standard-dialog proof cannot be assigned to `sdlg` or `sdlg2`; it is fixed to
  `demo` and `dlgdsn` and may not be reassigned to a third example.
- Broad demo proof cannot count editor, help, stream, terminal, mouse, or real
  charset behavior toward wave-2 acceptance.

## AcceptedLimitation

**Purpose**: Traceable record for intentionally reduced historical behavior.

**Fields**:
- `ExampleName`
- `HistoricalBehavior`
- `Reduction`
- `Rationale`
- `AcceptanceImpact`
- `EarliestFollowUpPoint`
- `TraceableReference`

**Validation rules**:
- Limitations cannot silently remove acceptance-critical behavior.
- Limitations that affect later parity are referenced from the example guide or
  architecture-risk notes.
- `sdlg`/`sdlg2` limitations beyond their historical ScrollDialog/ScrollGroup
  purpose are tracked as Historical Example Parity Cleanup and cannot block
  wave-2 acceptance.

## HistoricalExampleParityCleanup

**Purpose**: Follow-up record for optional parity beyond wave-2 acceptance.

**Fields**:
- `AffectedExample`
- `DeferredBehavior`
- `Rationale`
- `EarliestSchedulingPoint`
- `TraceableReference`

**Validation rules**:
- Cleanup cannot block wave-2 acceptance.
- Cleanup for `sdlg`/`sdlg2` is scheduled no earlier than after mandatory waves
  1-4.
- Cleanup records distinguish optional parity from required historical
  ScrollDialog/ScrollGroup completion.

## WaveProofRecord

**Purpose**: Repository-visible completion evidence for wave 2.

**Fields**:
- `PflichtenheftChecklistState`
- `NextStepMarker`
- `ExamplesReadmeState`
- `GuideIndexState`
- `SmokeEvidence`
- `ArchitectureEvidence`
- `SecurityEvidence`
- `A11YEvidence`
- `ProjectStatisticsEntry`

**Validation rules**:
- Wave 2 is accepted only when all eleven examples, guides, smoke scenarios, and
  proof surfaces are updated.
- The next-step marker moves to wave 3 only after wave-2 proof is complete.
