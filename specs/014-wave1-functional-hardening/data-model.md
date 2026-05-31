# Data Model: Wave 1 Functional Hardening

**Feature**: `014-wave1-functional-hardening`
**Date**: 2026-05-31

This feature has no database schema and no persisted user data model. The model below defines evidence, runtime-proof, and review entities that implementation and smoke tests must make explicit.

## Entity: Wave1FunctionalReview

Represents one primary evidence record for a covered Wave-1 example area.

**Fields**:
- `ExampleArea`: One of `Desklogo`, `MsgCls`, `Tutorial`, or `Videomode`.
- `ProjectPath`: Example project path under `examples/`.
- `HistoricalSourceReferences`: Required read-only historical source records.
- `HistoricalCoreFunction`: Behavior, learning goal, routing pattern, logo/desktop intent, or mode capability under review.
- `CurrentManagedBehavior`: Current C# behavior and its public runtime path.
- `ProofMethod`: Executable smoke proof, evidence-only proof with boundary, or documented follow-up.
- `HelperClassification`: Classification for each helper/headless/direct path used by the proof.
- `NegativeFallbackProof`: Optional proof record for unsupported, invalid, undersized, or platform-limited paths.
- `MissingCoreFunctionDecision`: Optional decision when historical behavior is not fully present.
- `IntentionalDeviationRecords`: Intentional differences from historical behavior.
- `DocumentationTrigger`: Whether a guide or `examples/README.md` update is required.
- `EvidencePath`: Primary path `specs/014-wave1-functional-hardening/pr-evidence.md`.

**Validation rules**:
- `ExampleArea` must be one of the four scoped Wave-1 areas.
- Every scoped area must have at least one `Wave1FunctionalReview`.
- Each review must identify historical source, current managed behavior, proof method, helper classification, and deviation or omission decision.
- Acceptance uses `pr-evidence.md` as the primary matrix even when guides summarize the result.

## Entity: TutorialStepReview

Represents one review record for a tutorial step token.

**Fields**:
- `StepToken`: One of `tvguid01` through `tvguid16`.
- `HistoricalSourceReference`: Matching historical file under `tv203s/contrib/tvision/examples/tutorial/`.
- `LearningTarget`: Historical learning point or defining behavior for the step.
- `ManagedStepPath`: Managed C# step class, launcher argument, or selection path.
- `ProofMethod`: Executable smoke proof or evidence-only proof with no-runtime-target rationale.
- `SequenceRelationship`: How the step relates to earlier/later tutorial steps.
- `IntentionalDeviationRecords`: Didactic or runtime differences.

**Validation rules**:
- Exactly 16 step tokens must be individually traceable.
- No tutorial proof may collapse all steps into one generic statement.
- Every step must have a step-specific learning target or behavior proof.

## Entity: HistoricalSourceReference

Represents read-only historical source material used to justify proof or deviation decisions.

**Fields**:
- `SourcePath`: Path under `tv203s/contrib/tvision/examples/`.
- `ReferenceRole`: `PrimaryImplementation`, `HeaderContext`, `AssetBoundary`, `GeneratorBoundary`, or `ContextOnly`.
- `ReviewedPurpose`: Historical behavior, class role, message flow, step goal, asset source, or mode behavior.
- `ManagedMapping`: C# type, method, example path, smoke proof, or explicit no-runtime target.
- `DeviationImpact`: `None`, `UserVisible`, `LearnerVisible`, `ProofOnly`, or `FollowUp`.

**Validation rules**:
- `tv203s/` paths are read-only and must not be modified.
- Headers are reviewed when declarations, constants, data layout, inheritance, macros, or signatures are required to understand behavior.
- `set-logo.cc` and `tv_logo.cc` may justify only Desklogo asset/generator boundaries unless implementation discovers a narrower documented need.

## Entity: SmokeProofClassification

Represents the proof type for a historical core function.

**Fields**:
- `ProofKind`: `ExecutableSmoke`, `EvidenceOnlyNoRuntimeTarget`, `DocumentedDeviation`, or `FollowUp`.
- `RuntimePath`: Public command, event, application method, stable public state, launcher argument, or app-loop path used by the proof.
- `ConcreteAssertions`: State, result, sequence, routing, repeated-trigger stability, fallback, or post-transition usability assertions.
- `SmokeTestName`: Test class and method when executable proof exists.
- `ProofBoundary`: Required explanation when executable proof does not exist.

**Validation rules**:
- Managed runtime behavior requires `ExecutableSmoke`.
- Evidence-only proof requires an explicit no-runtime-target rationale.
- Startup success, static text presence, and project existence alone are not sufficient.

## Entity: HelperClassification

Represents the role of helper, headless, or direct proof paths used by Wave-1 smokes.

**Fields**:
- `HelperName`: Helper method, test seam, headless path, direct example method, or public state accessor.
- `Classification`: `SetupOnly`, `PrimaryProof`, `SupplementalProof`, or `LegacyOrTemporary`.
- `Reason`: Why the classification is correct.
- `RuntimeLogicExecuted`: Whether real example or application logic is executed.
- `PublicSurfaceUsed`: Public command, event, application method, or stable public state used.
- `ReplacementResponsibility`: Later visual-remediation responsibility when classification is `LegacyOrTemporary`.

**Validation rules**:
- `PrimaryProof` is valid only when the path executes real example or application logic through public commands, events, application methods, or stable public state and contains concrete assertions.
- Paths that only prepare state, inspect private implementation details, or bypass behavior cannot be `PrimaryProof`.
- `LegacyOrTemporary` must identify the later remediation responsibility it prepares.

## Entity: IntentionalDeviationRecord

Represents a documented difference between historical behavior and managed behavior.

**Fields**:
- `DeviationId`: Stable local identifier in `pr-evidence.md`.
- `HistoricalBehavior`: What the historical source did or taught.
- `ManagedBehavior`: What the current C# example does instead.
- `Reason`: Modernization, platform limitation, didactic simplification, unavailable runtime target, follow-up visual scope, or out-of-scope boundary.
- `LearnerExplanationRequired`: Whether guide or README text must explain the deviation.
- `EvidenceLocation`: `pr-evidence.md`, guide path, README path, or security/architecture note.

**Validation rules**:
- All intentional deviations found during this feature must be documented.
- Learner-visible deviations require German-first/English-second CEFR-B2 explanation in the affected learner-facing artifact.
- Review-only classification details may remain in `pr-evidence.md`.

## Entity: NegativeFallbackProof

Represents proof for unsupported, invalid, undersized, or platform-limited behavior.

**Fields**:
- `PathKind`: `UnsupportedDisplay`, `InvalidInput`, `UndersizedDisplay`, `PlatformLimitation`, `UnavailableCapability`, or `RejectedFixture`.
- `Trigger`: Condition or input that causes the negative or fallback path.
- `ExpectedDeviation`: Historical or ideal behavior that cannot be followed exactly.
- `ObservedFallback`: Managed fallback behavior.
- `ProofMethod`: Deterministic smoke proof or evidence-only proof boundary.
- `ProofBoundary`: Why deterministic triggering is unavailable when evidence-only proof is used.

**Validation rules**:
- Relevant negative/fallback paths affecting acceptance must be proven by smoke or documented with proof boundary.
- Platform-dependent fallback records must state whether local, CI, Linux, macOS, or Windows/WSL behavior limits proof.

## Entity: MissingCoreFunctionDecision

Represents a decision for a historical core function missing or incomplete in the managed example.

**Fields**:
- `HistoricalCoreFunction`: Missing function, routing behavior, step behavior, logo/desktop intent, or display-mode capability.
- `Necessity`: `RequiredForExistingFunctionalPurpose`, `DidacticOnly`, `VisualRemediationFollowUp`, or `OutOfScope`.
- `Feasibility`: `SmallFunctionalChange`, `RequiresBroadFrameworkWork`, `RequiresVisualRemediation`, `RequiresNewDependency`, or `OutsideWave1Scope`.
- `Decision`: `ImplementIn014`, `IntentionalDeviation`, or `FollowUp`.
- `ProofStatus`: Smoke method name, evidence-only boundary, or follow-up reference.
- `Rationale`: Short reason recorded in `pr-evidence.md`.

**Validation rules**:
- Decisions marked `ImplementIn014` require matching smoke proof.
- Decisions outside this feature must name the boundary that blocks implementation.
- New runtime dependencies are not allowed for this feature.

## Entity: DocumentationUpdateTrigger

Represents the condition that determines whether learner-facing documentation must change.

**Fields**:
- `AffectedArtifact`: Guide path, `examples/README.md`, or `None`.
- `TriggerKind`: `RuntimeBehavior`, `UsagePath`, `VisibleOutput`, `HistoricalDeviation`, `LearnerProofExplanation`, or `ReviewOnlyClassification`.
- `UpdateRequired`: Boolean decision.
- `LanguageRequirement`: German-first/English-second CEFR-B2 when updated.
- `A11YReviewPath`: Text-first review or DocFX/WCAG 2.2 AA path when generated documentation output changes.

**Validation rules**:
- `ReviewOnlyClassification` may remain only in `pr-evidence.md`.
- Any learner-visible trigger requires affected guide or README update.
- DocFX/web-a11y is required when generated documentation output or navigation is affected.

## Scoped Review Matrix

| Area | Required historical sources | Primary proof focus |
|---|---|---|
| `Desklogo` | `desklogo/desklogo.cc`; `set-logo.cc` and `tv_logo.cc` only for asset/generator boundary | Logo or desktop intent, asset source/replacement rationale, undersized/unsupported fallback |
| `MsgCls` | `msgcls/testdyn.cpp`, `msgcls/tlnmsg.cpp`, `msgcls/tlnmsg.h` | Custom message triggering, routing, observable result, repeated-trigger stability |
| `Tutorial` | `tutorial/tvguid01.cc` through `tutorial/tvguid16.cc` | Individual step learning target or behavior, token identity, sequence relationship |
| `Videomode` | `videomode/test.cc` | Real display capability outcome or honest fallback with post-transition usability |

## State Transitions

```text
Specified
  -> Clarified
  -> Planned
  -> EvidenceMatrixCreated
  -> HistoricalSourcesReviewed
  -> ProofPathsClassified
  -> SmokeGapsIdentified
  -> RuntimeProofImplementedOrDeviationRecorded
  -> NegativeFallbackProofRecorded
  -> LearnerDocsUpdatedWhenTriggered
  -> ValidationRecorded
  -> Accepted
```

**Transition rules**:
- `HistoricalSourcesReviewed` requires read-only `tv203s/` review.
- `ProofPathsClassified` requires all helper/headless/direct proof paths to have one accepted label.
- `RuntimeProofImplementedOrDeviationRecorded` requires executable smoke proof when managed runtime behavior exists.
- `Accepted` requires all scoped evidence records and validation output in `pr-evidence.md`.

## Deutsch / English

Deutsch: Dieses Datenmodell beschreibt keine Datenbank. Es beschreibt, welche Nachweis- und Review-Objekte fuer die Wave-1-Funktionshaertung eindeutig gepflegt werden muessen.

English: This data model does not describe a database. It describes the evidence and review objects that must be maintained for Wave-1 functional hardening.
