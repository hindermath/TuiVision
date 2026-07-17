# Requirements Checklist: Provenance and Cardinality

**Purpose**: Test whether the audit requirements define an exact, reproducible Wave-5 product population
**Created**: 2026-07-17
**Feature**: [spec.md](../spec.md)

## Requirement Completeness

- [x] CHK001 Are both authoritative Feature PRs identified by base, reviewed head, merge, and role? [Completeness, Spec §FR-002–FR-004]
- [x] CHK002 Are closeout and prompt-only PRs explicitly separated from the product delta? [Consistency, Spec §FR-004–FR-005]
- [x] CHK003 Are exact counts defined for sources, consumers, examples, functional proofs, showcase closures, and guide/launch paths? [Measurability, Spec §FR-006]
- [x] CHK004 Are historical-source identity and no-drift requirements defined? [Coverage, Spec §FR-007]

## Requirement Clarity

- [x] CHK005 Is the authoritative file-population rule clearer than a broad baseline-to-main comparison? [Clarity, Spec §FR-002–FR-005]
- [x] CHK006 Are unique identity and relationship expectations defined for every matrix family? [Clarity, Spec §FR-006, §FR-015]
- [x] CHK007 Are commit, file-set, blob, and line-ending drift all named as fail-closed cases? [Edge Cases, Spec §FR-003, §FR-025–FR-026]

## Acceptance Criteria Quality

- [x] CHK008 Can the required 15/6/10/10/10/10 population be measured without subjective interpretation? [Measurability, Spec §SC-001–SC-003]
- [x] CHK009 Does the specification prevent supporting evidence from inflating product cardinality? [Consistency, Spec §FR-004–FR-005]
- [x] CHK010 Are missing, duplicate, unknown, contradictory, and drifted relations covered as exception scenarios? [Coverage, Spec §FR-025–FR-026]

## Notes

- All items pass. Provenance is exact enough for deterministic planning and negative validation.
