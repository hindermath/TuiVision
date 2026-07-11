# Data Model: Didactic Inline Code Comment Hardening

**Feature**: `015-didactic-comment-hardening`
**Date**: 2026-06-14

This feature has no database schema and no persisted user data model. The model below defines the review, evidence, and validation entities that implementation must keep explicit.

## Entity: ReviewArea

Represents one source file, test helper file, or named flow area reviewed for didactic-comment adequacy.

**Fields**:
- `AreaId`: Stable local identifier in `pr-evidence.md`.
- `PathOrFlow`: Repository path or named flow area.
- `ModuleGroup`: `Core`, `Controls`, `Drivers.Console`, `Serialization`, `Compatibility`, `SmokeTests`, `Evidence`, or `Guidance`.
- `HotspotCategories`: One or more required hotspot categories.
- `LearnerValue`: Why an apprentice or maintainer benefits from reviewing this area.
- `MaintenanceRisk`: Why misunderstanding this area would matter.
- `HistoricalContext`: Optional Turbo Vision reference or `N/A` rationale.
- `PrimaryDecision`: One approved `CommentDecision`.

**Validation rules**:
- Every reviewed area must have exactly one `PrimaryDecision`.
- Every required hotspot category must appear in at least one review area or have an explicit no-current-area rationale.
- `tv203s/` references are read-only context and cannot be review targets for editing.

## Entity: HotspotCategory

Represents a required review dimension from the specification.

**Allowed values**:
- `EventCommandDispatch`
- `FocusTransition`
- `ViewHierarchy`
- `StatusLine`
- `HelpDescription`
- `DialogState`
- `ValidationRejection`
- `BufferCellProof`
- `RenderingSnapshot`
- `TerminalFallback`
- `HistoricalTurboVisionDeviation`
- `SmokeTestHelper`

**Validation rules**:
- Coverage must be recorded in `pr-evidence.md`.
- A category may be satisfied by a named flow area instead of a single file when the flow crosses files.
- If no current file or flow needs a comment for a category, the evidence must explain why.

## Entity: CommentDecision

Represents the primary result for one review area.

**Allowed values**:
- `CommentAdequate`
- `CommentNeeded`
- `NoCommentNeeded`
- `UpdateExistingComment`
- `FollowUpHardening`

**Validation rules**:
- No other decision values are allowed.
- `CommentNeeded` requires a matching comment change unless a later same-feature review proves the existing code/comment is already adequate.
- `UpdateExistingComment` requires correction, replacement, or removal of stale, misleading, broad, or trivial comment text.
- `NoCommentNeeded` must state why a comment would repeat obvious code.
- `FollowUpHardening` must name the real issue, out-of-scope reason, and follow-up boundary.

## Entity: DidacticCommentChange

Represents a new, updated, or removed code-near comment.

**Fields**:
- `Path`: Source or test file path.
- `LocationHint`: Type, method, helper, or flow name sufficient for review.
- `ChangeKind`: `Added`, `Updated`, `Removed`, or `LeftUnchanged`.
- `ExplanationKind`: `Why`, `TradeOff`, `Constraint`, `HistoricalDeviation`, `ProofBoundary`, or `NotApplicable`.
- `LineBudget`: `Within1To3Lines`, `LongerWithRationale`, or `NotApplicable`.
- `LanguageForm`: `GermanFirstEnglishSecond`, `TechnicalMarkerUnchanged`, or `NotApplicable`.
- `DocFxTrigger`: `None`, `XmlOrApiChanged`, `GuideOrNavigationChanged`, or `GeneratedDocsChanged`.

**Validation rules**:
- Added or updated didactic comments must explain reason, trade-off, constraint, historical deviation, or proof boundary.
- Added or updated didactic comments must not restate adjacent identifiers, operators, assertions, assignments, or obvious control flow.
- Longer comments require evidence rationale.
- `DocFxTrigger != None` requires the normal DocFX/A11Y validation path.

## Entity: FeatureEvidenceEntry

Represents the acceptance record for one review area.

**Fields**:
- `AreaId`
- `PathOrFlow`
- `HotspotCategory`
- `Decision`
- `Rationale`
- `CommentNeed`
- `CommentState`: `Changed`, `Unchanged`, `Removed`, or `NotApplicable`.
- `ChangeSummary`
- `ValidationOrProofBoundary`
- `FollowUpBoundary`
- `GovernanceTrigger`: Security, Architecture, A11Y, AgentParity, CrossPlatform, Statistics, or `None`.

**Validation rules**:
- Required evidence path is `specs/015-didactic-comment-hardening/pr-evidence.md`.
- Every reviewed area has one evidence entry or a clearly linked sub-entry.
- `CommentState` is required so unchanged adequate comments, intentionally absent comments, removed trivial comments, and real changes are distinguishable.
- `ValidationOrProofBoundary` is required for smoke-test helpers, rendering snapshots, buffer/cell proof, and terminal fallbacks.
- `FollowUpBoundary` is required when `Decision == FollowUpHardening`.

## Entity: GovernanceEvidenceEntry

Represents the audit-ready record for one checkpoint from an installed
governance preset. Governance applicability is deliberately separate from the
five-value comment decision model.

**Fields**:
- `RunId`: `015-didactic-comment-hardening`.
- `PresetName`
- `PresetVersion`
- `Checkpoint`
- `Applicability`: `Applicable`, `N/A`, or `Open`.
- `Rationale`
- `EvidencePath`
- `Owner`
- `Reviewer`
- `ReviewDate`
- `Result`: `OK`, `N/A`, or `Open`.
- `ResidualRisk`
- `FollowUp`
- `ReevaluationTrigger`

**Validation rules**:
- Every relevant checkpoint from all six installed presets has one entry; no
  checkpoint may be silently omitted.
- Every entry records preset/version, rationale, evidence path, owner, reviewer,
  review date, result, and residual risk.
- `Applicability == N/A` requires a short rationale and a re-evaluation
  trigger.
- `Applicability == Open` requires an owner, concrete follow-up, and a
  re-evaluation trigger.
- `Applicability == Applicable` requires a concrete evidence path and a result.
- `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`,
  `UpdateExistingComment`, and `FollowUpHardening` are not valid governance
  applicability values.

## Entity: SmokeProofBoundary

Represents the proof purpose and limit for a smoke-test helper or proof path.

**Fields**:
- `HelperOrProofPath`: Helper method, app-loop path, command/event/key path, view-tree inspection, rendered snapshot, or buffer/cell assertion.
- `ProofPurpose`: What behavior or state the proof supports.
- `StabilityReason`: Why the proof is deterministic enough for acceptance.
- `Boundary`: What the proof does not prove.
- `HelperRole`: `PrimaryProof`, `SupplementalProof`, `SetupOnly`, `LegacyOrTemporary`, or `N/A`.
- `CommentDecision`: Approved decision for local comment treatment.

**Validation rules**:
- Non-obvious proof paths require either code-near explanation or evidence explaining why local comments are unnecessary.
- Setup-only and supplemental helpers must not be described as complete behavior proof.
- Terminal and rendering proof must state environment/capability boundaries when relevant.

## Entity: HistoricalDeviationRecord

Represents a modern TuiVision difference from historical Turbo Vision behavior that matters for comprehension.

**Fields**:
- `HistoricalReference`: `tv203s/` source path or named historical behavior.
- `ModernArea`: C# path or flow.
- `DeviationKind`: `IntentionalModernization`, `PlatformConstraint`, `ProofBoundary`, `UnavailableHistoricalFeature`, or `DeferredHardening`.
- `ExplanationNeed`: `Comment`, `EvidenceOnly`, `GuideOrDoc`, or `None`.
- `Rationale`

**Validation rules**:
- Historical references are read-only.
- Only comprehension-relevant deviations need records.
- Runtime parity fixes are out of scope; real issues use `FollowUpHardening` as the comment-review decision.

## Entity: AgentGuidanceReview

Represents review of shared comment guidance surfaces.

**Fields**:
- `GuidanceChanged`: Boolean.
- `SurfacesReviewed`: Required agent surfaces.
- `Synchronized`: Boolean.
- `IntentionalDivergence`: Optional rationale.
- `TemplateImpact`: `.specify/templates/` impact or `N/A`.

**Validation rules**:
- If shared guidance changes, all maintained agent surfaces must be updated together.
- If shared guidance does not change, evidence records unchanged rationale.
- `.specify/templates/` remain `N/A` unless repository-owned templates are explicitly changed.

## Entity: ValidationRecord

Represents final validation evidence for the comment-hardening implementation.

**Fields**:
- `CommandOrReview`: Command, manual review, or CI evidence.
- `Trigger`: Why this validation was required.
- `Result`: Pass, Fail, Blocked, N/A.
- `EvidenceLocation`: `pr-evidence.md`, command output summary, CI link, or review note.

**Validation rules**:
- `git diff --check` is required for final implementation evidence.
- Targeted tests are required when source/test helper files are changed.
- Full Release tests and coverage gate are required when shared code or broad proof helpers are materially touched.
- DocFX plus web-a11y are required only when XML/API/generated docs/navigation/guides change.

## State Transitions

```text
Specified
  -> Clarified
  -> Planned
  -> EvidenceLedgerCreated
  -> HotspotInventoryCompleted
  -> ReviewAreasClassified
  -> CommentsAddedUpdatedRemovedOrOmitted
  -> FollowUpsRecorded
  -> GuidanceReviewed
  -> GovernanceEvidenceCompleted
  -> ValidationRecorded
  -> Accepted
```

**Transition rules**:
- `HotspotInventoryCompleted` requires every required hotspot category to be mapped or explicitly rationalized.
- `ReviewAreasClassified` requires exactly one approved decision per reviewed area.
- `CommentsAddedUpdatedRemovedOrOmitted` requires comment style and DocFX trigger checks.
- `GovernanceEvidenceCompleted` requires one complete audit-ready entry per
  relevant checkpoint and no unowned `Open` decision.
- `Accepted` requires final validation evidence, complete governance evidence,
  and no unbounded follow-up claims.

## Deutsch / English

Deutsch: Dieses Datenmodell beschreibt keine Datenbank. Es beschreibt die Review- und Evidence-Objekte, die fuer die didaktische Kommentarhaertung eindeutig gepflegt werden muessen.

English: This data model does not describe a database. It describes the review and evidence objects that must be maintained for didactic comment hardening.
