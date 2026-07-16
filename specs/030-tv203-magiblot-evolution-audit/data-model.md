# Data Model: TV203 and magiblot/tvision Evolution Audit

## MagiblotAuditRun

- `schemaVersion`
- `runId`
- canonical and closure feature IDs
- Feature-029 handoff identity and hash
- repository, commit, tree, timestamp, subject
- COPYRIGHT SHA-256 and multipart provenance summary
- accepted and new contract counts
- wave states and next intake

## MagiblotSourceRecord

- `sourceId` (`MBSR001+`)
- relative `path`
- `sha256`
- `sourceKind`
- comparison chapters and contract IDs
- short German and English behavior summaries
- pinned `permalink`
- provenance and no-copy boundary

## MagiblotContractRelation

- `contractId`, `domainId`, names
- `magiblotSourceIds`
- `magiblotRelation`
- German and English rationale
- `tuiVisionProof`
- historical source/intent
- `consumerIds`
- `sharedBiasRisk`
- `observationId`
- `deduplicationKey`
- `reevaluationTrigger`

Allowed relations are the five exact terms from `spec.md` FR-008.

## MagiblotConsumerReview

- `consumerId`, wave, source paths
- contract IDs and proof paths
- magiblot source IDs and relevance
- decision, risk, follow-up boundary
- owner, reviewer, review date, residual risk, trigger

## MagiblotObservation

- `observationId` (`MB001+`)
- contract/domain identity
- observation and reproduction
- historical, Free Vision, Terminal.GUI, and magiblot relations
- shared-bias risk and consumer scope
- TuiVision source and current proof
- missing proof/behavior, impacts, risk
- suggested owner and dependencies
- required red and real-path green proof
- exact decision
- evidence/review/residual-risk fields
- `deduplicationKey`

## CombinedObservationDisposition

- `sourceObservationId` (`TGO*` or `MB*`)
- source feature and contract
- canonical deduplication key
- disposition: `CanonicalFinding` or `NonFinding`
- `canonicalFindingId` or non-finding rationale
- reviewer, review date, residual risk, trigger

## CanonicalFinding

- `findingId` (`CF001+`)
- source observation IDs
- contract/domain/consumer scope
- one `primaryOwner`
- dependencies
- common reproduction
- required red proof and real-path green proof
- API, A11Y, platform, security impacts
- risk and governance effect
- evidence, reviewer, residual risk, trigger

Allowed Primary Owners are `CoreRuntimeDriver`,
`ComponentDataInteraction`, and `CrossCuttingA11YProof`.

## GeneratedIntake

- feature number and filename
- kind: `Hardening` or `Closure`
- owner or `IndependentClosure`
- source finding IDs
- dependencies
- wave-gate effect
- non-empty proof

## GovernanceDecision

- preset, version, checkpoint
- applicability
- rationale and evidence path
- owner, reviewer, date, result
- residual risk, follow-up, re-evaluation trigger

## ValidationEvidence

- gate ID
- command/scope
- build counter when applicable
- result and evidence boundary
- runner/platform for remote gates
- exact reviewed head

## Relationships

- Every accepted contract has exactly one relation and one MB observation.
- Every relation references existing source records and proof.
- Every accepted consumer references existing contracts and source paths.
- Every TG and MB observation has exactly one combined disposition.
- Every canonical finding has one Primary Owner and valid source observations.
- Finding dependencies are acyclic.
- Every hardening intake maps to one non-empty owner group.
- Exactly one closure intake follows all hardening intakes.

## State Transitions

```text
PinnedSource -> Manifested -> Related -> Observed
TG/MB Observation -> NonFinding | CanonicalFinding | ProductDecision(Blocked)
CanonicalFinding -> Owned -> DependencySorted -> HardeningIntake
All owner groups -> IndependentClosureIntake
Active -> Interrupted -> StatusOnly -> ResumeAudit -> Active
Active -> Retrospective -> Completed
```
