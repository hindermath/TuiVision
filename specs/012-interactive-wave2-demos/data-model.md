# Data Model: Interactive Wave 2 Demos

**Feature**: `012-interactive-wave2-demos`
**Date**: 2026-05-09

This feature has no database schema and no persisted user data model. The model below defines the runtime/proof entities that implementation and smoke tests must make explicit.

## Entity: InteractiveExample

Represents one Wave-2 example application that must become visibly operable.

**Fields**:
- `Name`: Stable example name, one of `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, `TProgB`.
- `ProjectPath`: Example project path under `examples/`.
- `AppClass`: Main application class used by both normal runtime and smoke tests.
- `PurposeText`: Text shown on the first screen so users understand what the example demonstrates.
- `OperationPaths`: One or more command, menu, or keyboard paths.
- `VisibleFeedbackStates`: States users and tests can observe after operations.
- `SmokeScenario`: Primary event-loop smoke path.
- `GuidePath`: Matching guide page under `docs/guides/examples/`.
- `HistoricalSourceReview`: Link or note for the relevant read-only `.c`/`.cc` reference files and any important matching headers under `tv203s/`.
- `OmissionRecords`: Explicit non-goals or historical behavior left out of scope.

**Validation rules**:
- `PurposeText` must be visible without a prior command.
- At least one `OperationPath` must be smoke-tested through the app loop.
- Every primary behavior claimed by the guide must have a visible feedback state.
- Direct helper methods may not be the sole proof for primary behavior.
- Historical source review must be completed before final acceptance, and user-visible deviations must be documented.

## Entity: OperationPath

Represents a user-operable route through the running application.

**Fields**:
- `CommandId`: Stable command identifier where command routing is used.
- `MenuLabel`: Human-readable menu label when available.
- `KeyboardShortcut`: Optional key path.
- `InjectedEvents`: Ordered events used by the smoke test.
- `Preconditions`: Required runtime state before triggering the operation.
- `ExpectedFeedbackState`: State that must be visible after completion.

**Validation rules**:
- Must be reachable through `app.Run()` or the equivalent runtime loop.
- Must not rely on mouse-only input.
- Must have deterministic completion and quit behavior in smoke tests.

## Entity: VisibleFeedbackState

Represents text-first UI evidence that an operation happened.

**Fields**:
- `StatusText`: Status-line or message text.
- `DesktopText`: Main desktop or view text.
- `DialogState`: Dialog title, result, selected value, validation message, or rendered field.
- `SelectionState`: Current list/combo/input selection or boundary marker.
- `ProgressState`: Current, completed, aborted, or cancelled progress value.
- `ErrorState`: Visible invalid/cancel/unavailable feedback where applicable.

**Validation rules**:
- Must be assertable by smoke tests without relying on color alone.
- Must remain readable in text-oriented documentation and snapshots.
- Must not require timing-dependent animation to pass.

## Entity: ScriptedEventPath

Represents the smoke-test input sequence for a runtime path.

**Fields**:
- `AppLoopRequired`: Always true for primary smoke proof.
- `Events`: Ordered `TEvent`, command, or key events.
- `SetupActions`: Optional setup that does not replace the runtime path.
- `Assertions`: Visible state checks after the event path.
- `QuitEvent`: Deterministic event that exits the app loop.
- `DirectHelperUse`: `None`, `SetupOnly`, or `SupplementalAssertion`.

**Validation rules**:
- Must not pass solely through direct method calls.
- Must include a quit path so tests cannot hang.
- Must avoid sleeps and external process timing where possible.

## Entity: ReadOnlyDemoFixture

Represents fixed data used by file/path and dialog-designer examples.

**Fields**:
- `FixtureKind`: `RepositoryPath`, `DialogDescription`, `TempDirectory`, or `InvalidPath`.
- `SourcePath`: Source-controlled or test-created path.
- `AccessMode`: Metadata-only, render-only, or validation-only.
- `CleanupRequired`: True only for test temp directories.

**Validation rules**:
- Must not read arbitrary user file contents as the proof mechanism.
- Must not persist user data during normal example operation.
- Invalid-path demonstrations must fail visibly and deterministically.

## Entity: GuideUpdate

Represents the documentation change for one example or a shared README.

**Fields**:
- `DocumentPath`: Markdown path.
- `GermanSection`: German-first CEFR-B2 content.
- `EnglishSection`: English synchronized content.
- `RuntimePathDescription`: How to run and operate the example.
- `SmokeEvidenceReference`: Test or evidence reference.
- `OmissionReference`: Known non-goals where relevant.

**Validation rules**:
- Must describe the actual visible runtime path.
- Must not claim behavior that lacks smoke evidence.
- Must preserve text-first, semantic Markdown structure.

## Entity: HistoricalSourceReview

Represents the per-example comparison with the original Turbo Vision source files.

**Fields**:
- `ExampleName`: Affected Wave-2 example.
- `SourceFiles`: Relevant read-only `.c`/`.cc` paths and any important matching header paths under `tv203s/`.
- `OriginalIntent`: Short summary of what the historical example demonstrates.
- `PlannedInteractivePath`: Runtime path planned or implemented in the C# example.
- `DeviationNotes`: Intentional differences caused by modern framework design, scope, portability, or safety.
- `EvidencePath`: Guide, `pr-evidence.md`, or task note that records the result.

**Validation rules**:
- Must be completed for all eleven Wave-2 examples.
- Must not modify files under `tv203s/`.
- Must document any user-visible deviation from the historical demo intent.

## Entity: ProofRecord

Represents the evidence needed for PR review and merge readiness.

**Fields**:
- `ExampleName`: Example or shared surface.
- `SmokeTest`: Test class/method proving the runtime path.
- `CommandsRun`: Validation commands and outcomes.
- `DocumentationUpdated`: Guide/README/evidence paths.
- `HistoricalSourceReviewed`: Source comparison completed and deviations recorded.
- `ValidationGateEvidence`: Example smoke, full test, coverage, formatting, and documentation/A11Y evidence or justified non-applicability.
- `ArchitectureSecurityNotes`: Paths updated or confirmed unchanged.
- `UnchangedEvidenceRationale`: Reason why an expected proof surface did not need content changes.
- `ResidualRisk`: Remaining limitation or `None`.

**Validation rules**:
- `pr-evidence.md` must summarize all eleven examples.
- Completion evidence must include example smokes and full `dotnet test`.
- Repository governance evidence must be recorded before merge.
- Unchanged governance or proof surfaces must carry an explicit rationale rather than being omitted silently.

## Entity: OmissionRecord

Represents intentionally excluded behavior.

**Fields**:
- `ExampleName`: Affected example.
- `OmittedBehavior`: Behavior not implemented in 012.
- `Reason`: Scope, later wave, framework prerequisite, or unsafe/non-deterministic path.
- `UserVisibleNote`: Where the omission is documented.

**Validation rules**:
- Must not hide behavior that the spec requires.
- Must be listed in guide/evidence when it affects user expectations.

## State Transitions

```text
Planned
  -> RuntimeWired
  -> EventScripted
  -> SmokeVerified
  -> Documented
  -> EvidenceRecorded
  -> Accepted
```

**Transition rules**:
- `RuntimeWired` requires visible first-screen purpose text and at least one command path.
- `EventScripted` requires the primary smoke sequence through the app loop.
- `SmokeVerified` requires assertions on visible feedback.
- `Documented` requires guide/README updates where behavior changed.
- `EvidenceRecorded` requires `pr-evidence.md` and governance notes.
- `Accepted` requires the validation gate defined in the contract and quickstart.
