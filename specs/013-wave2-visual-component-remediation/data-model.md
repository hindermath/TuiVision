# Data Model: Wave 2 Visual Component Remediation

**Feature**: `013-wave2-visual-component-remediation`
**Date**: 2026-05-22

This feature has no database schema and no persisted user data model. The model below defines runtime, proof, and evidence entities that implementation and smoke tests must make explicit.

## Entity: Wave2Example

Represents one scoped Wave-2 example application.

**Fields**:
- `Name`: Stable example name, one of `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, `TProgB`.
- `ProjectPath`: Example project path under `examples/`.
- `HistoricalSourceReview`: Completed review record for relevant `tv203s/` source and header files.
- `VisibleMainComponent`: Primary visible component or stable visual state.
- `StatusFeedback`: `TStatusLine` feedback or documented equivalent status area.
- `DescriptionPath`: Canonical runtime path `Help -> Description`.
- `PrimarySmokeProof`: Main app-loop proof with concrete state and rendered visibility assertions.
- `GuidePath`: Matching guide page under `docs/guides/examples/`.
- `FeatureEvidencePath`: Feature or PR evidence record under `specs/013-wave2-visual-component-remediation/`, normally `pr-evidence.md` once implementation evidence is collected.
- `EvidenceRecords`: Feature, architecture, security, A11Y, supply-chain, AI-SBOM, and statistics evidence or N/A rationale.

**Validation rules**:
- `Name` must be one of the eleven scoped examples.
- Every example must have exactly one primary visible proof target, even if supplemental states exist.
- Status feedback may support but may not replace the primary visible proof.
- `Help -> Description` must be reachable by keyboard/command path and smoke-verified.
- Historical-source review must be completed before final acceptance.

## Entity: VisibleMainComponent

Represents the visual component or stable runtime state used as parity proof.

**Fields**:
- `ComponentKind`: `Control`, `Dialog`, `Window`, `ViewGroup`, `ScrollGroup`, `Progress`, `DynamicText`, `InputList`, `ComboInput`, or `StableRuntimeState`.
- `RuntimeContainer`: Desktop, dialog, window, group, or equivalent parent view.
- `ExpectedRegion`: Stable position or region used by rendered visibility proof.
- `ControlSpecificContent`: Text, value, selection, field label, progress mark, scroll content, or dialog title expected in the buffer.
- `InitialState`: State visible at normal startup.
- `InteractionStates`: State changes after commands, keys, or injected events.

**Validation rules**:
- Must be visible from normal startup or after a documented primary operation path.
- Must be assertable through both view-tree and buffer/cell snapshot proof.
- Must not rely on color alone for meaning.
- Must have documented deviation evidence if the historical visual target cannot be represented exactly.

## Entity: StatusFeedback

Represents short text-first runtime feedback.

**Fields**:
- `Surface`: `TStatusLine` by default, or `EquivalentStatusArea` only with deviation record.
- `Message`: Short status text.
- `Trigger`: Command, key, selection, focus, scroll, progress, validation, abort, cancel, or unavailable state.
- `TextFirstPurpose`: Explanation of why the message helps text-first review.

**Validation rules**:
- Must be visible and text-readable after relevant state changes.
- Must stay short and support the main component instead of replacing it.
- Equivalent status areas require explicit deviation evidence.

## Entity: DescriptionPath

Represents the canonical in-app learner explanation route.

**Fields**:
- `MenuPath`: Always `Help -> Description`.
- `Reachability`: Keyboard/menu/command path used by runtime and tests.
- `ContentGerman`: German-first explanation at roughly CEFR-B2.
- `ContentEnglish`: English explanation synchronized with the German content.
- `Explains`: Visible component, operation path, historical intent, status feedback, and A11Y review path.
- `SmokeEvidence`: Test assertion proving reachability and content.

**Validation rules**:
- Must exist in every scoped example.
- Must be reachable without mouse-only input.
- Must not be replaced by `About`; `About` may be supplemental only.
- Must explain visual behavior in text-first form.

## Entity: PrimarySmokeProof

Represents the main deterministic proof for an example.

**Fields**:
- `AppLoopPath`: `app.Run()` or equivalent real application loop route.
- `InjectedEvents`: Ordered `TEvent`, command, or key events.
- `ConcreteStateAssertions`: Focus, selection, scroll offset, progress value, dialog state, input value, history state, rejection state, or equivalent.
- `ViewTreeProof`: Assertion that the expected control/dialog/view exists in the runtime composition.
- `RenderedVisibilityProof`: Buffer/cell snapshot at expected position or region.
- `DirectHelperUse`: `None`, `SetupOnly`, or `SupplementalAssertion`.
- `QuitPath`: Deterministic event or command that exits the app loop.

**Validation rules**:
- Must exercise the real runtime path.
- Must include both view-tree proof and rendered buffer/cell proof.
- Must not pass by asserting only `VisibleText`, `VisibleHistory`, or direct helper output.
- Must avoid sleeps, external processes, and unbounded filesystem scans.

## Entity: HistoricalSourceReview

Represents the read-only historical intent review.

**Fields**:
- `ExampleName`: Affected Wave-2 example.
- `SourceFiles`: Relevant `.c`/`.cc` paths under `tv203s/`.
- `HeaderFiles`: Required `.h`, `.hpp`, or `.hh` files when declarations are needed.
- `OriginalVisualIntent`: What the historical example visually demonstrates.
- `TargetCSharpState`: The planned visible C# state.
- `DeviationNotes`: Intentional user-visible differences.
- `EvidencePath`: Guide, feature evidence, architecture note, security note, or task reference that records the result.

**Validation rules**:
- Must be completed for all eleven examples.
- Must not modify `tv203s/`.
- Must record intentional user-visible deviations.

## Entity: ControlledFixture

Represents allowed proof data for file/path, dialog-designer, and clipboard-adjacent behavior.

**Fields**:
- `FixtureKind`: `SourceControlledFile`, `DialogDescription`, `InvalidFixture`, `TempDirectory`, or `ClipboardTestDouble`.
- `PathOrScope`: Repository path or test temporary scope.
- `AccessMode`: Metadata-only, render-only, validation-only, unavailable-state, or cleanup-only.
- `CleanupRequired`: True only for test-created temporary resources.

**Validation rules**:
- Must not read arbitrary user file contents.
- Must not rely on external proof paths.
- Must not persist user history.
- Invalid fixtures must fail visibly and deterministically.

## Entity: EvidenceRecord

Represents a reviewable completion record.

**Fields**:
- `ExampleName`: Example or shared surface.
- `VisibleTarget`: Link to `VisibleMainComponent`.
- `SmokeTest`: Test class/method proving the target.
- `HistoricalSourceReviewed`: Source comparison completed and deviations recorded.
- `DocumentationUpdated`: Guide/README/description path content updated or N/A rationale.
- `FeatureEvidencePath`: Feature or PR evidence entry that traces the example from historical intent to visible runtime proof.
- `ValidationGateEvidence`: Build, tests, coverage, format, DocFX/A11Y as applicable.
- `GovernanceEvidence`: Security, architecture, supply-chain, AI-SBOM, A11Y, statistics, and Pflichtenheft evidence or unchanged rationale.
- `ResidualRisk`: Remaining limitation or `None`.

**Validation rules**:
- Every scoped example must have a traceable evidence record.
- Governance surfaces that stay unchanged need explicit rationale.
- Completion evidence must not omit failed or skipped validation commands.

## Per-Example Visual Targets

| Example | Required visible target |
|---|---|
| `Clipboard` | Visible text or input component before/after copy, cut, paste, and unavailable state |
| `Demo` | Three flow families: `Dialog/Control`, `File/Path metadata`, `Display/Color/Gadget` |
| `DlgDsn` | Visible dialog/control tree for valid descriptions and visible rejection for invalid fixtures |
| `DynTxt` | Dynamic text view with changed/clipped/aligned/narrow-width content |
| `InpLis` | Dialog composition with list, input, history or boundary behavior |
| `ListVi` | List viewer/list box with selected item plus boundary or empty-state feedback |
| `ProgBa` | Progress-bar state through completion |
| `Sdlg` | Scroll-dialog or scroll-group state with content outside initial viewport |
| `Sdlg2` | Two-axis scroll-dialog or scroll-group state |
| `TCombo` | Input-plus-combo or selection composition with displayed value and boundary/empty state |
| `TProgB` | Progress dialog/window with partial progress, abort, and cancelled states |

## State Transitions

```text
Specified
  -> HistoricalIntentReviewed
  -> VisibleTargetDesigned
  -> RuntimeComposed
  -> StatusAndDescriptionWired
  -> AppLoopSmokeScripted
  -> RenderedVisibilityVerified
  -> Documented
  -> EvidenceRecorded
  -> Accepted
```

**Transition rules**:
- `HistoricalIntentReviewed` requires read-only `tv203s/` source review.
- `RuntimeComposed` requires visible main component or stable runtime state.
- `StatusAndDescriptionWired` requires `TStatusLine` feedback and `Help -> Description`.
- `RenderedVisibilityVerified` requires view-tree plus buffer/cell proof.
- `Accepted` requires the validation gate defined in the contract and quickstart.

## Deutsch / English

Deutsch: Dieses Datenmodell beschreibt keine Datenbank. Es beschreibt, welche Laufzeit- und Nachweisobjekte fuer die sichtbare Wave-2-Remediation eindeutig nachweisbar sein muessen.

English: This data model does not describe a database. It describes the runtime and evidence objects that must be explicit for the visible Wave-2 remediation.
