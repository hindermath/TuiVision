# Filesystem Safety Requirements Checklist

**Purpose**: Review controlled-root, mutation, recovery, and platform
requirements before planning
**Created**: 2026-07-17
**Feature**: [spec.md](../spec.md)

## Boundary Definition

- [x] CHK001 Is the controlled root explicitly required for every file operation? [Completeness, Spec §FR-011-FR-014]
- [x] CHK002 Are normal repository fixtures and test-owned temporary roots distinguished? [Clarity, Spec §FR-011, FR-012]
- [x] CHK003 Are canonical path, traversal, link, reparse, and casing boundaries addressed? [Coverage, Spec §FR-013, FR-014, Edge Cases]
- [x] CHK004 Is permanent process-current-directory mutation prohibited? [Boundary, Spec §FR-015]
- [x] CHK005 Are arbitrary user data, network devices, shell, process, PTY, and external viewers excluded? [Completeness, Spec §FR-020, FR-021, Scope Boundaries]

## Read Paths

- [x] CHK006 Are empty, missing, removed, and filtered directory states specified? [Coverage, Spec §FR-016, FR-017]
- [x] CHK007 Are preview byte and line boundaries plus invalid-text behavior required? [Clarity, Spec §FR-018]
- [x] CHK008 Are search root, depth, file, hit, cancellation, and relative-result limits required? [Completeness, Spec §FR-019]
- [x] CHK009 Are internal association decisions closed without an external-launch fallback? [Consistency, Spec §FR-020, FR-021]

## Mutation and Recovery

- [x] CHK010 Is explicit confirmation required before every mutating operation? [Coverage, Spec §FR-022]
- [x] CHK011 Is byte-preserving cancel or missing-decision behavior specified? [Measurability, Spec §FR-023]
- [x] CHK012 Are silent overwrite and source-equals-target cases excluded? [Edge Case, Spec §FR-024]
- [x] CHK013 Are progress, completion, failure, abort, and recovery evidence required? [Completeness, Spec §FR-025]
- [x] CHK014 Does drag/drop remain an intent with complete keyboard parity? [Consistency, Spec §FR-026, FR-031]
- [x] CHK015 Are unsupported attribute semantics required to fail honestly rather than claim false parity? [Coverage, Edge Cases]

## Acceptance Quality

- [x] CHK016 Can root ownership and zero external mutation be measured for every mutating test? [Measurability, Spec §SC-003]
- [x] CHK017 Are positive, negative, cancellation, and conflict paths all required? [Coverage, Spec §SC-004, SC-005]
- [x] CHK018 Does the scope stop when a filesystem boundary cannot be made safe? [Boundary, Spec §Decision and Follow-up Model]

## Notes

- Requirements review passed. Planning must choose concrete preview and search
  limits without weakening these boundaries.
