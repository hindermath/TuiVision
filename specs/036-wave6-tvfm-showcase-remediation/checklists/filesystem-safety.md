# Filesystem Safety Requirements Checklist

**Purpose**: Review dialog, intent, mutation, and recovery safety requirements
before planning
**Created**: 2026-07-17
**Feature**: [spec.md](../spec.md)

## Preserved Authority

- [x] CHK001 Is reuse of Feature-035 controlled-root behavior mandatory? [Completeness, Spec §FR-003]
- [x] CHK002 Are arbitrary user, network, device, drive, process, shell, PTY, and host paths excluded? [Boundary, Scope Boundaries]
- [x] CHK003 Are all historical and comparison roots read-only? [Coverage, Spec §FR-004]
- [x] CHK004 Does the spec prohibit widening path, preview, search, viewer, intent, or mutation contracts? [Consistency, Spec §FR-003]

## Dialog Decisions

- [x] CHK005 Are copy, rename, delete, and read-only dialogs all covered? [Completeness, Spec §FR-015]
- [x] CHK006 Are source, target/name, normalized preview, safety boundary, Confirm, and Cancel required? [Clarity, Spec §FR-016]
- [x] CHK007 Is immediate pre-execution intent revalidation mandatory? [Security, Spec §FR-017]
- [x] CHK008 Are Cancel, Escape, missing confirmation, and invalid input guaranteed non-mutating? [Measurability, Spec §FR-018]
- [x] CHK009 Are recursive delete and silent overwrite explicitly prohibited? [Boundary, Spec §FR-019]
- [x] CHK010 Are result, rejection, and recovery boundaries visible? [Completeness, Spec §FR-020]

## Mouse Boundary

- [x] CHK011 Is mouse drag limited to the keyboard-equivalent prepared intent? [Consistency, Spec §FR-021]
- [x] CHK012 Is direct pointer-triggered mutation explicitly prohibited? [Security, Spec §FR-022]
- [x] CHK013 Are invalid target, Escape, capability loss, view removal, and shutdown covered? [Edge Case, Spec §FR-023]
- [x] CHK014 Is full keyboard fallback required for every mouse path? [A11Y, Spec §FR-008, SC-005]

## Acceptance Quality

- [x] CHK015 Do all four mutation types require positive and negative/abort/recovery proof? [Coverage, Spec §SC-004]
- [x] CHK016 Can zero pre-confirmation mouse mutation be measured? [Measurability, Spec §SC-005]
- [x] CHK017 Does any unsafe authority requirement stop the autonomous run? [Boundary, Decision and Follow-up Model]

## Notes

- Requirements review passed. Planning must preserve Feature-035 services as
  the only mutation authority behind the visible dialogs.
