# Requirements Checklist: Source Authority and Provenance

**Purpose**: Validate requirements for historical authority, Free Vision comparison, and no-copy boundaries
**Created**: 2026-07-12
**Audience**: Specification and PR reviewers

## Authority Completeness

- [x] CHK001 Are Borland documentation and `tv203s/` explicitly ordered above Free Vision for historical interpretation? [Completeness, Spec §FR-004]
- [x] CHK002 Is the relationship between accepted TuiVision contracts and historical intent defined without making either silently override the other? [Clarity, Spec §User Story 2]
- [x] CHK003 Are conflicting Borland documentation, historical source, and current public behavior routed to a visible decision boundary? [Coverage, Spec §Edge Cases]
- [x] CHK004 Are original TP7 sources constrained to explanatory Wave-5 context rather than included in feature-024 implementation scope? [Consistency, Spec §Scope Boundaries]

## Free Vision Clarity

- [x] CHK005 Is the approved Free Vision repository revision immutable and explicitly named? [Clarity, Spec §FR-005]
- [x] CHK006 Are repository, revision, retrieval date, and reviewed paths required as provenance fields? [Completeness, Spec §FR-007]
- [x] CHK007 Is every audit domain required to contain a comparison or justified `NotApplicable`? [Coverage, Spec §FR-020]
- [x] CHK008 Are the four Free Vision relations closed, distinct, and separated from primary contract decisions? [Consistency, Spec §FR-018]
- [x] CHK009 Is the behavior defined when the pinned revision is unavailable? [Edge Case, Spec §Edge Cases]
- [x] CHK010 Is Free Vision evolution distinguished from evidence about original behavior? [Clarity, Spec §User Story 3]

## Provenance and Copy Boundary

- [x] CHK011 Do requirements prohibit vendoring, substantial source excerpts, and mechanical translation? [Completeness, Spec §FR-006]
- [x] CHK012 Is later source-derived implementation explicitly placed behind a separate license/provenance decision? [Boundary, Spec §Out of Scope]
- [x] CHK013 Can the no-copy outcome be measured from tracked repository content? [Measurability, Spec §SC-007]
- [x] CHK014 Are source comparison and runtime modification kept separate throughout the requirements? [Consistency, Spec §FR-002]

## Result

All source-authority and provenance requirements are complete. No specification
change remains from this checklist.
