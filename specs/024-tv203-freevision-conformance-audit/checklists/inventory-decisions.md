# Requirements Checklist: Inventory and Decision Integrity

**Purpose**: Validate completeness, uniqueness, contract granularity, and decision vocabularies
**Created**: 2026-07-12
**Audience**: Planning and evidence reviewers

## Inventory Completeness

- [x] CHK001 Is the expected 151-row historical baseline explicit and measurable? [Clarity, Spec §FR-008]
- [x] CHK002 Is exactly one primary domain required for each historical row? [Uniqueness, Spec §FR-009]
- [x] CHK003 Are consciously omitted historical rows required to carry impact and re-evaluation criteria? [Completeness, Spec §FR-010]
- [x] CHK004 Are all maintained source files covered while generated output is excluded? [Coverage, Spec §FR-011]
- [x] CHK005 Are source-file ownership and public-contract inventory treated as separate completeness dimensions? [Consistency, Spec §FR-013]
- [x] CHK006 Does the spec address one file containing several contracts and one modern target consolidating several historical files? [Edge Cases, Spec §Edge Cases]
- [x] CHK007 Are all 16 required domains enumerated in the specification itself? [Completeness, Spec §Required Audit Domains]

## Contract Evidence

- [x] CHK008 Is every contract required to include intent, observed behavior, rationale, proof, risk, and disposition? [Completeness, Spec §FR-016]
- [x] CHK009 Is a broad test-project reference rejected unless a deterministic filter or named collection makes proof specific? [Clarity, Spec §FR-014]
- [x] CHK010 Is missing negative, rejection, recovery, or fallback proof allowed to remain visible as an evidence gap? [Coverage, Spec §Edge Cases]
- [x] CHK011 Is machine-checkable completeness required even when evidence is split by domain? [Measurability, Spec §FR-029]

## Decision Integrity

- [x] CHK012 Are the five primary decisions closed and mutually distinguishable? [Clarity, Spec §FR-017]
- [x] CHK013 Are the four Free Vision relations closed and independent from primary decisions? [Consistency, Spec §FR-018]
- [x] CHK014 Are governance statuses prohibited from substituting for either decision dimension? [Conflict Prevention, Spec §FR-019]
- [x] CHK015 Is exactly-one decision and relation coverage measurable for every contract? [Acceptance Criteria, Spec §SC-003]
- [x] CHK016 Does the specification permit a valid zero-finding audit without weakening inventory or proof requirements? [Edge Case, Spec §Assumptions]

## Result

The 16 domains and exact preset versions were normalized during this checklist
pass. All inventory and decision requirements now pass.
