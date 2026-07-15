# Integration and Consumer Requirements Checklist

**Purpose**: Review real-path slice and consumer-readiness requirement quality
**Created**: 2026-07-14
**Audience**: Test, framework, and Wave readiness reviewers
**Feature**: [spec.md](../spec.md)

## Seven Integration Slices

- [x] CHK001 Are all seven slice identifiers present and mapped to distinct observable contracts? [Completeness, Spec §FR-008-FR-014]
- [x] CHK002 Does keyboard ingress begin at the real adapter and include kind, modifiers, shortcuts, fallback, dispatch, and consumption? [Coverage, Spec §FR-008]
- [x] CHK003 Does focus proof cover unique current child, propagation, veto, preserved state, and announcement? [Coverage, Spec §FR-009]
- [x] CHK004 Does idle proof specify pending-first order, bounded execution, command refresh, no displacement, CPU release, and shutdown? [Coverage, Spec §FR-010]
- [x] CHK005 Does desktop/modal proof include stack, geometry, close veto, input paths, isolation, cleanup, focus, view tree, and rendered cells? [Coverage, Spec §FR-011]
- [x] CHK006 Does drag proof define capture, threshold, bounds, target, commit, cancellation, lifecycle loss, and keyboard equivalence? [A11Y Coverage, Spec §FR-012]
- [x] CHK007 Does dialog proof define completion classification, real child validation, first rejection, preserved state, accessible evidence, and cancel? [Coverage, Spec §FR-013]
- [x] CHK008 Does file/resource proof cover all modes, safe paths, reconstruction, malformed input, bounds, and atomic rejection? [Security Coverage, Spec §FR-014]

## Proof Boundaries

- [x] CHK009 Must each slice name production entry, assertions, negative or fallback case, helper role, and proof limit? [Traceability, Spec §FR-015]
- [x] CHK010 Is helper-only or pre-normalized proof explicitly rejected when it bypasses a named real path? [Consistency, Spec §US2]
- [x] CHK011 Are visible, view-tree, and buffer/cell signals required only where they substantively apply? [Clarity, Spec §US2, §FR-011]
- [x] CHK012 Are Windows or WSL commands required for input, path, and terminal-facing acceptance instead of inferred OS labels? [Platform Evidence, Spec §FR-026]

## Consumer Matrix

- [x] CHK013 Are the six Wave-5 and seven Wave-6 Revision-2 groups explicitly enumerated? [Completeness, Spec §FR-016]
- [x] CHK014 Does each baseline and newly discovered shared flow require exactly one allowed readiness decision? [Clarity, Spec §FR-016]
- [x] CHK015 Are contract, proof, rationale, Wave relevance, risk, and follow-up required for every row? [Traceability, Spec §FR-017]
- [x] CHK016 Do `SmallFrameworkFix` and `ProductDecision` block closure while `FollowUpHardening` cannot conceal a shared gap? [Decision Boundary, Spec §FR-018]
- [x] CHK017 Is the destructive `FILECOPY.PAS`/`TRASH.PAS` group retained as a product-policy boundary rather than omitted? [Edge Coverage, Spec §FR-016]
- [x] CHK018 Are all consumer and historical trees explicitly read-only? [Scope, Spec §FR-017, §FR-024]

## Measurability

- [x] CHK019 Does SC-002 require all seven slices, not a representative subset? [Acceptance Criteria, Spec §SC-002]
- [x] CHK020 Does SC-003 require all thirteen baseline consumer groups plus any discovered shared responsibility? [Acceptance Criteria, Spec §SC-003]
