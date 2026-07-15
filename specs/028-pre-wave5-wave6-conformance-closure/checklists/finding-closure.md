# Finding Closure Requirements Checklist

**Purpose**: Review the quality and completeness of the thirteen-finding closure contract
**Created**: 2026-07-14
**Audience**: Spec author and PR reviewer
**Feature**: [spec.md](../spec.md)

## Cardinality and Identity

- [x] CHK001 Is the exact `F001`-`F013` cardinality stated without permitting sampled or aggregate-only closure? [Completeness, Spec §FR-002]
- [x] CHK002 Does the specification prohibit omitted, duplicated, or unknown finding identifiers? [Clarity, Spec §FR-002]
- [x] CHK003 Are original observations, contracts, ownership, historical intent, Free Vision relation, and consumer scope explicitly immutable? [Consistency, Spec §FR-001-FR-003]

## Decision Quality

- [x] CHK004 Is exactly one exhaustive closure-decision vocabulary defined for each finding? [Clarity, Spec §FR-004]
- [x] CHK005 Are `Closed` and `AlreadySatisfiedWithNewProof` distinguished by measurable evidence rather than intent? [Clarity, Spec §FR-005]
- [x] CHK006 Do reopened and product-decision outcomes explicitly block closure and identify their routing boundary? [Exception Coverage, Spec §FR-006]
- [x] CHK007 Does the specification prevent governance or consumer decisions from substituting for a finding decision? [Consistency, Spec §Decision and Follow-up Model]

## Proof Completeness

- [x] CHK008 Is a named real production-path proof required for every successful finding closure? [Completeness, Spec §FR-005]
- [x] CHK009 Are comments, implementation claims, broad suite passes, and pre-normalized input explicitly insufficient by themselves? [Proof Boundary, Spec §FR-005]
- [x] CHK010 Must every finding preserve red proof, merged change, source relation, impact, residual risk, and re-evaluation evidence? [Traceability, Spec §Key Entities]
- [x] CHK011 Are bidirectional finding-contract-source-proof-consumer-owner relations required to be machine-checkable? [Integrity, Spec §FR-007]
- [x] CHK012 Does SC-001 objectively measure complete and unique finding closure? [Measurability, Spec §SC-001]

## Scope and Failure Behavior

- [x] CHK013 Is runtime repair of a reproduced finding explicitly forbidden inside Feature 028? [Scope, Spec §FR-006, §FR-022]
- [x] CHK014 Are test-only or validator changes constrained to measurement of an already accepted contract? [Scope, Spec §FR-023]
- [x] CHK015 Does the specification stop rather than weaken acceptance for a real regression? [Failure Handling, Spec §US1]
- [x] CHK016 Are historical and external source roles read-only and non-mechanical? [Source Boundary, Spec §FR-024]

## Governance and Acceptance

- [x] CHK017 Are owner, reviewer, date, evidence, result, risk, follow-up, and trigger fields required for closure records? [Governance, Spec §FR-027]
- [x] CHK018 Is the existing-gate result separate from both Wave states? [Consistency, Spec §FR-019-FR-020]
- [x] CHK019 Does success retain the mandatory Feature-029 next intake rather than declaring Wave eligibility? [Ordering, Spec §FR-021]
- [x] CHK020 Are missing review and stale exact-head evidence represented as failures or explicit absence rather than approval? [Edge Coverage, Spec §Edge Cases]
