# Data Model: Secure Development Hardening

**Feature**: `016-secure-development-hardening`  
**Date**: 2026-07-11

This feature introduces no runtime database or persisted user-data schema. The model defines audit, finding, remediation, validation, and script-contract records stored in Markdown or represented by repository files.

## Entity: ControlAssessment

Represents one stable `CL-XX-NN` secure-development control.

**Fields**:
- `ControlId`: Unique identifier such as `CL-05-01`.
- `SourcePath`: Checklist file and heading.
- `ControlTitle`: Bilingual or source-aligned short title.
- `Status`: One approved `AssessmentStatus`.
- `Rationale`: Why the status is factual for TuiVision.
- `EvidencePath`: Current repository path, command evidence, or explicit absence boundary.
- `Owner`: Responsible role or person.
- `Reviewer`: Reviewing role or person.
- `ReviewDate`: ISO date.
- `Result`: Concise observed outcome.
- `RiskPriority`: `Critical`, `High`, `Medium`, `Low`, or `None`.
- `ResidualRisk`: Risk remaining after decision or remediation.
- `FollowUp`: Concrete action, work item, or `None`.
- `ReevaluationTrigger`: Event that invalidates the current decision.
- `HumanOnly`: Boolean.

**Validation rules**:
- Exactly 157 unique rows must exist for the planning baseline.
- Every `CL-XX-NN` source heading appears exactly once.
- No mandatory field is blank.
- `Applicable` and `AlreadySatisfied` require direct current evidence.
- `N/A` requires rationale and re-evaluation trigger.
- `Open` requires owner, priority, risk, follow-up, and trigger.
- `FollowUp` requires a named scope boundary or future work item.

## Entity: AssessmentStatus

**Allowed values**:
- `Applicable`: Work or evidence is required and belongs to feature 016.
- `AlreadySatisfied`: Current evidence proves the control without new remediation.
- `N/A`: The control is factually not applicable under recorded conditions.
- `Open`: The control needs a human, provider, legal, credential, or unresolved decision.
- `FollowUp`: The control is relevant but remediation exceeds feature 016.

No aliases or combined statuses are allowed.

## Entity: SecurityFinding

Represents a concrete gap discovered by assessment or validation.

**Fields**:
- `FindingId`: Stable `F-###` identifier.
- `ControlIds`: One or more related controls.
- `AffectedPaths`: Repository paths or external boundary.
- `Description`: Factual problem statement.
- `Severity`: `Critical`, `High`, `Medium`, or `Low`.
- `ExploitOrFailureBoundary`: Preconditions and plausible impact.
- `Disposition`: `Remediate`, `AcceptedN/A`, `OpenHumanOnly`, or `FollowUp`.
- `Owner`, `Reviewer`, `ReviewDate`.
- `AcceptanceCondition`: Observable completion proof.
- `ResidualRisk`, `FollowUp`, `ReevaluationTrigger`.

**Validation rules**:
- Critical/high findings cannot remain unresolved at merge.
- Medium and implementation-relevant low findings require remediation or accepted out-of-scope disposition.
- Human-only findings cannot be auto-closed by repository evidence.
- False positives record why the reported pattern cannot reach the claimed impact.

## Entity: RemediationItem

Represents one bounded implementation change.

**Fields**:
- `RemediationId`: Stable `R-###` identifier.
- `FindingId` and `ControlIds`.
- `Paths`.
- `ChangeClass`: `Code`, `Test`, `Script`, `CI`, `Documentation`, `Evidence`, or `Configuration`.
- `ScopeReason`: Why it is small/medium, reversible, and feature-compatible.
- `BehaviorImpact`: Expected behavior change or `None`.
- `CommentReview`: Didactic-comment decision for changed non-trivial logic.
- `ValidationIds`: Required validation records.
- `Result` and `ResidualRisk`.

**Validation rules**:
- Every remediation maps to a finding and acceptance condition.
- Runtime/API/package changes require explicit finding rationale.
- Changed non-trivial logic receives selective didactic-comment review.

## Entity: EvidenceArtifact

Represents durable proof.

**Fields**:
- `EvidenceId`: Stable identifier.
- `PathOrCommand`: Source-controlled path or reproducible command.
- `EvidenceType`: `Policy`, `Assessment`, `ThreatModel`, `Architecture`, `Dependency`, `SBOMDefinition`, `Test`, `CI`, `Validation`, or `FollowUp`.
- `SupportsControls`: Control IDs.
- `FreshnessDate`: ISO date or validation run date.
- `Owner` and `Reviewer`.
- `Generated`: Boolean.
- `Retention`: `Tracked`, `CIArtifact`, `Temporary`, or `NotGenerated`.
- `Result` and `Limit`.

**Validation rules**:
- Positive control claims point to direct evidence.
- Generated outputs are never `Tracked` unless explicitly approved by the spec.
- Evidence limits prevent policy text or templates from being treated as implementation proof.

## Entity: ValidationRun

Represents one reproducible verification.

**Fields**:
- `ValidationId`: Stable `V-###` identifier.
- `CommandOrReview`.
- `Scope`.
- `Version`: `Directory.Build.props` value when build/test is involved.
- `Platform`.
- `StartedAt` and optional `CompletedAt`.
- `Result`: `Pass`, `Fail`, `Blocked`, or `N/A`.
- `Summary`: Counts or essential output.
- `OutputRetention`: `None`, `Temporary`, or `CIArtifact`.
- `FailureBoundary`.

**Validation rules**:
- Build/test records use a build-counter value incremented before the command.
- Failure is not converted to `N/A`.
- Conditional validation records the trigger and reason when `N/A`.

## Entity: ScriptContractCase

Represents equivalent Bash and PowerShell behavior for `rename-lastenheft`.

**Fields**:
- `CaseId`.
- `Scenario`: `Help`, `MissingInput`, `UnsafeInput`, `DryRun`, `NoCommit`, `ExplicitCommit`, `UnrelatedStagedChange`, `BranchNormalization`, or `Idempotent`.
- `BashInvocation` and `PowerShellInvocation`.
- `ExpectedExitCode`.
- `ExpectedFilesystemState`.
- `ExpectedIndexState`.
- `ExpectedCommitDelta`.
- `ExpectedOutputMeaning`.

**Validation rules**:
- Both implementations have the same outcome for every case.
- Dry-run/WhatIf changes neither files, index, nor commits.
- No-commit renames through Git but creates no commit.
- Explicit commit includes only the rename paths and leaves unrelated staged content untouched.
- Unsafe or untracked non-Lastenheft inputs fail before mutation.

## Entity: SupplyChainAssessment

**Fields**:
- `DependencyResult`: vulnerable, deprecated, and outdated review summary.
- `SbomTool`: package ID and pinned version.
- `SbomFormat`: CycloneDX JSON and schema/spec version.
- `SbomCommand`: clean-checkout generation path.
- `VexStatus` and trigger.
- `SlsaStatus`, target, and follow-up.
- `ScorecardStatus`, evidence, and provider boundary.
- `ActionPinStatus`.
- `UpdateAutomationStatus`.
- `AiSbomStatus` and trigger.

**Validation rules**:
- SBOM output parses as JSON and contains components.
- Known vulnerabilities cannot be silently omitted from VEX disposition.
- Provider-only controls remain human-only when repository files cannot prove them.

## Entity: GovernanceApplicability

Represents one preset or regulatory checkpoint.

**Fields**:
- `PresetName`, `PresetVersion`, `Checkpoint`.
- `AssessmentStatus`.
- `Rationale`, `EvidencePath`, `Owner`, `Reviewer`, `ReviewDate`.
- `Result`, `ResidualRisk`, `FollowUp`, `ReevaluationTrigger`, `HumanOnly`.

**Validation rules**:
- All six preset versions are represented.
- BSI C3A/C5, ASVS, Zero Trust, AI-SBOM, NIS2, CRA, EU AI Act, DORA, and DPIA are explicit rather than omitted.
- Legal or formal compliance conclusions remain human-only.

## Entity: AgentParityReview

**Fields**:
- `SharedGuidanceChanged`: Boolean.
- `SurfacesReviewed`: the five maintained files.
- `ContextRefreshCommands`: four update-agent-context invocations.
- `Synchronized`: Boolean.
- `IntentionalDivergence`: rationale or `None`.
- `TemplateImpact`: result for `.specify/templates/`.

**Validation rules**:
- Shared guidance changes update all affected surfaces in one serialized work item.
- Context refresh does not replace hand-maintained shared policy.
- Template impact is `N/A` unless a repository-owned template defect is found.

## Relationships

```text
ChecklistHeading 1 --- 1 ControlAssessment
ControlAssessment * --- * EvidenceArtifact
ControlAssessment * --- * SecurityFinding
SecurityFinding 1 --- 0..* RemediationItem
RemediationItem * --- * ValidationRun
ScriptContractCase * --- * ValidationRun
SupplyChainAssessment 1 --- * EvidenceArtifact
GovernanceApplicability * --- * EvidenceArtifact
```

## State Transitions

```text
Inventoried
  -> Classified
  -> EvidenceLinked
  -> FindingsTriaged
  -> BoundedRemediationComplete
  -> HumanAndFollowUpBoundariesRecorded
  -> ValidationComplete
  -> AnalyzeClean
  -> Accepted
```

`Accepted` requires 157/157 controls, no unresolved critical/high risk, complete mandatory fields, successful required validation, and no actionable Analyze finding.
