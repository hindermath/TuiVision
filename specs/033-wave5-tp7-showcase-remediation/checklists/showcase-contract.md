# Requirements Quality Checklist: Showcase Contract

**Purpose**: Verify that the ten-example visual and interaction contract is
complete before task generation.

- [x] CHK001 Exactly ten accepted example IDs are stated and no substitute example is allowed.
- [x] CHK002 The main-component, real-status, and keyboard Description requirements are independently testable.
- [x] CHK003 The first-frame purpose, focus, selection, rejection, fallback, and keyboard-parity requirements are explicit.
- [x] CHK004 Every example has a concrete normal and constrained proof obligation.
- [x] CHK005 Primary proof requires app loop, domain state, view/focus identity, status, Description, and cells.
- [x] CHK006 Direct helper roles cannot satisfy primary acceptance.
- [x] CHK007 Exactly one framework decision per example is required.
- [x] CHK008 `SmallFrameworkFix` and `FollowUpHardening` boundaries are unambiguous.
- [x] CHK009 Evidence-validator failure cases include missing, duplicate, unknown, incomplete, and boundary-violating rows.
- [x] CHK010 Wave 6 and Feature 034 remain outside the accepted scope.
