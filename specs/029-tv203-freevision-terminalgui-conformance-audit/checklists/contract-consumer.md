# Contract and Consumer Requirements Checklist

**Purpose**: Review completeness and consistency of the contract, domain, flow, and consumer requirements
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Contract Cardinality and Relations

- [x] CHK001 Is the exact existing contract range `C001` through `C048` stated without sampling language? [Completeness, Spec FR-008]
- [x] CHK002 Is exactly one Terminal.GUI relation required for every existing contract? [Clarity, Spec FR-009]
- [x] CHK003 Are all five relation values defined consistently in requirements and the decision model? [Consistency, Spec FR-009 and Decision Model]
- [x] CHK004 Are relation evidence fields complete enough to distinguish source, rationale, proof, consumer relevance, risk, and finding linkage? [Completeness, Spec FR-010]
- [x] CHK005 Does `NotApplicable` require both rationale and a re-evaluation trigger? [Edge Case, Spec FR-011]
- [x] CHK006 Is architectural difference clearly separated from a reproducible TuiVision contract gap? [Clarity, Spec FR-012-FR-013]

## New Contract Boundary

- [x] CHK007 Are all admission conditions for `C049+` specified and jointly required? [Completeness, Spec FR-014]
- [x] CHK008 Is duplicate coverage by an existing contract an explicit rejection boundary? [Coverage, Spec FR-014]
- [x] CHK009 Is the no-new-contract outcome measurable when no material uncovered responsibility exists? [Acceptance Criteria, Spec SC-004]

## Domain and Flow Coverage

- [x] CHK010 Are all sixteen binding audit domains required rather than treated as examples? [Completeness, Spec FR-015]
- [x] CHK011 Are the minimum Application, MainLoop, input, focus, lifecycle, layout, rendering, menu, validation, file, clipboard, driver, and proof flows named? [Coverage, Spec FR-016]
- [x] CHK012 Is helper-only proof distinguishable from a real framework-path proof? [Clarity, User Story 2 and Edge Cases]

## Consumer Coverage

- [x] CHK013 Are both `TVDEMOS/` and `TVFM/` required as complete read-only consumer families? [Completeness, Spec FR-017]
- [x] CHK014 Are consumer-row fields sufficient for contract, proof, Wave relevance, risk, decision, and follow-up traceability? [Completeness, Spec FR-018]
- [x] CHK015 Is omission because no immediate code change is expected explicitly rejected? [Edge Case, Spec SC-003]
- [x] CHK016 Are consumer and contract cardinality outcomes objectively measurable? [Measurability, Spec SC-001-SC-003]

## Review Result

All contract, domain, flow, and consumer requirements are complete. No specification change is required.
