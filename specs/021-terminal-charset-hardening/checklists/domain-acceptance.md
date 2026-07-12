# Domain Requirements Checklist: Terminal and Charset Hardening

**Purpose**: Review requirement quality before planning the terminal, emulation,
charset/font, profile, host, and proof contracts
**Created**: 2026-07-12
**Audience**: Feature author and PR reviewer
**Depth**: Formal planning gate

## Requirement Completeness

- [X] CHK001 Are session input, output, cursor, attributes, history, status, lifecycle, and in-process boundaries all specified? [Completeness, Spec FR-001..FR-005]
- [X] CHK002 Is the supported control-sequence subset enumerated and is the unsupported remainder explicitly bounded? [Completeness, Spec FR-006..FR-009]
- [X] CHK003 Are Unicode, KOI8-R, replacement, host-independence, and unsupported-codepage requirements complete? [Completeness, Spec FR-010..FR-012]
- [X] CHK004 Are font fixture dimensions, glyph count, length, source ownership, and generator boundaries documented? [Completeness, Spec FR-013..FR-015]
- [X] CHK005 Are required/optional profile fields, defaults, rejection, fallback, and capability states specified? [Completeness, Spec FR-016..FR-017]
- [X] CHK006 Are deterministic, remote-CI, and physical-host evidence classes separated for every required host family? [Coverage, Spec FR-021..FR-022]

## Requirement Clarity and Consistency

- [X] CHK007 Are wrap, scroll, FIFO history, resize, reset, BEL, and cursor semantics precise enough to prevent conflicting plans? [Clarity, Clarifications]
- [X] CHK008 Are all numeric limits and their adjacent acceptance boundaries explicitly quantified? [Clarity, Spec FR-008, SC-002]
- [X] CHK009 Is `U+FFFD` the only replacement outcome throughout scenarios, requirements, entities, and success criteria? [Consistency, Spec FR-012, SC-004]
- [X] CHK010 Are safe profile defaults consistent with the only accepted 8x16 fixture contract and 16-color session scope? [Consistency, Spec FR-013..FR-017]
- [X] CHK011 Is the no-host-process/no-persistent-host-change boundary consistent across stories, requirements, assumptions, and exclusions? [Consistency, Spec FR-001, FR-003, FR-015]

## Acceptance and Scenario Quality

- [X] CHK012 Can every success criterion be measured through named state, boundary counts, decision rows, or evidence classes? [Measurability, Spec SC-001..SC-013]
- [X] CHK013 Are primary, alternate, rejection, recovery, resize, cleanup, headless, and unsupported scenarios represented? [Coverage, User Stories and Edge Cases]
- [X] CHK014 Is the later App-Loop/View proof prepared without requiring visible Wave-4 example porting in this feature? [Scope, Spec FR-019..FR-020, FR-030]
- [X] CHK015 Are framework reuse, historical intent, didactic comments, governance, and follow-up decisions traceable to exact requirement groups? [Traceability, Spec FR-023..FR-029]
- [X] CHK016 Does the causal closeout requirement preserve pre-merge gate verification without implying remote authority? [Security and Permission Clarity, Spec CR-015..CR-016]

## Review Result

- 16/16 requirement-quality items pass.
- No missing scenario class or unresolved planning ambiguity remains.
