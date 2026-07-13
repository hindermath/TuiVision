# Requirements Checklist: Finding Contracts

**Purpose**: Review whether the nine accepted core findings are specified completely, consistently, and measurably before planning
**Created**: 2026-07-13
**Audience**: Feature author and pull-request reviewers
**Feature**: [spec.md](../spec.md)

## Finding Identity and Traceability

- [X] CHK001 Are all accepted Finding IDs `F001` through `F009` mapped one-to-one to their audit contract and Lastenheft requirement? [Completeness, Spec §Functional Requirements]
- [X] CHK002 Does each Finding retain its original observation and acceptance boundary without silent merging or reinterpretation? [Consistency, Spec §FR-001-FR-018]
- [X] CHK003 Are `Implemented`, `AlreadySatisfied`, `FollowUpHardening`, `ProductDecision`, and governance applicability kept as distinct vocabularies? [Clarity, Spec §Decision and Follow-up Model]
- [X] CHK004 Is it explicit that `FollowUpHardening` cannot close an accepted `F001`-`F009` Finding? [Boundary, Spec §FR-022]
- [X] CHK005 Is the stop condition for a breaking public-contract conflict objectively stated? [Coverage, Spec §FR-023]

## Event, Focus, and State Requirements

- [X] CHK006 Are concrete accepted event kinds and rejected mask/category classes exhaustively specified? [Completeness, Spec §FR-001-FR-002]
- [X] CHK007 Is the event rejection point defined before dispatch and independent of payload-channel interpretation? [Clarity, Spec §FR-001]
- [X] CHK008 Is focus veto ownership and ordering fixed before any focus, Current, data, or visible-state mutation? [Clarity, Spec §Clarifications]
- [X] CHK009 Are accepted, rejected, and no-op focus outcomes distinguishable and measurable? [Measurability, Spec §FR-003-FR-004]
- [X] CHK010 Is Feature-026 validator integration excluded without weakening the generic Feature-025 veto contract? [Consistency, Spec §FR-004]
- [X] CHK011 Are state-specific requirements present for `Focused`, `Disabled`, `Active`, `Dragging`, `Exposed`, and Current? [Completeness, Spec §FR-005-FR-006]
- [X] CHK012 Is the difference between Current ownership and propagated state bits explicit enough to prevent several focused children? [Clarity, Spec §Key Entities]

## Idle, Desktop, and Modal Requirements

- [X] CHK013 Is the single Pending-Event boundary specified together with its ordering before idle work? [Completeness, Spec §Clarifications]
- [X] CHK014 Are no-event, repeated-idle, pending-event, shutdown, and CPU-release scenarios covered? [Coverage, Spec §FR-007-FR-008]
- [X] CHK015 Is the no-background-thread and no-unbounded-queue constraint explicit? [Boundary, Spec §Clarifications]
- [X] CHK016 Are Insert, Top/Next, Tile, Cascade, and Close-All all named as one coherent desktop contract? [Completeness, Spec §FR-009-FR-010]
- [X] CHK017 Are empty-desktop, non-closeable, bounds, focus, Z-order, and lifecycle-loss outcomes specified? [Edge Case, Spec §FR-010]
- [X] CHK018 Do close requirements distinguish successful removal from a mere `cmClose` signal? [Clarity, Spec §FR-011-FR-012]
- [X] CHK019 Are safe-close veto, modal result, event isolation, bounded nesting, cancellation, shutdown, and focus restoration all required? [Coverage, Spec §FR-011-FR-012]
- [X] CHK020 Is the one-active-modal-child-per-owner rule compatible with nested sessions only below the active modal child? [Consistency, Spec §Clarifications]

## Command, Keyboard, and Drag Requirements

- [X] CHK021 Is one shared command context required for active View, menu, StatusLine, and keyboard? [Completeness, Spec §FR-013-FR-014]
- [X] CHK022 Are all context refresh triggers and the pre-execution recheck explicitly defined? [Clarity, Spec §Clarifications]
- [X] CHK023 Does the specification preserve local presentation state without allowing a second truth source? [Consistency, Spec §FR-014]
- [X] CHK024 Is the real `ConsoleKeyInfo` or equivalent adapter boundary mandatory rather than normalized-event-only proof? [Coverage, Spec §FR-015-FR-016]
- [X] CHK025 Are printable, navigation, function, Alt, Ctrl, Shift, Ctrl+W, Alt-shortcut, unknown, and fallback key classes all named? [Completeness, Spec §FR-016]
- [X] CHK026 Is the generic drag boundary explicit about threshold, capture, bounds, target decision, result, cancellation, lifecycle loss, and keyboard parity? [Completeness, Spec §FR-017-FR-018]
- [X] CHK027 Is one-cell threshold terminology measurable for a character-cell UI? [Measurability, Spec §Clarifications]
- [X] CHK028 Are pointer and keyboard completion paths required to use the same contract and end-state evidence? [Consistency, Spec §FR-017-FR-018]

## Acceptance Quality

- [X] CHK029 Does every Finding require a Red-Proof before behavior change and a real-path proof after it? [Traceability, Spec §FR-019-FR-021]
- [X] CHK030 Are state, View tree, focus, and Buffer/Cell evidence required for lifecycle-visible flows? [Acceptance Criteria, Spec §FR-020]
- [X] CHK031 Can all Finding-specific success criteria be measured without relying on comments or weaker replacement tests? [Measurability, Spec §SC-001-SC-008]
- [X] CHK032 Are the release, coverage, API, dependency, example, and historical-source completion boundaries explicit? [Completeness, Spec §SC-009-SC-012]

## Review Result

All finding-contract requirements are complete enough for implementation
planning. No checklist item requires a specification change.
