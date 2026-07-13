# Plan Quality Checklist: Core Runtime Conformance Hardening

**Purpose**: Confirm that the design is complete enough for red-first task generation without widening Feature 025.
**Reviewed**: 2026-07-13

## Scope and finding ownership

- [X] **PQ-001** The plan covers exactly findings `F001` through `F009`.
  **Execution**: Reconcile the Finding Slices table with FR-001 through FR-036 and the Feature-024 `Core025` finding set; confirm that `F010` through `F013` remain owned by 026.
- [X] **PQ-002** Every finding has a bounded reusable framework contract and a red-first proof boundary.
  **Execution**: Trace each Finding Slices row into `research.md`, `data-model.md`, and the acceptance contract; verify that no row is closed by documentation alone.
- [X] **PQ-003** Wave 5, Wave 6, broad redesign, new packages, and external-source edits remain outside the implementation diff.
  **Execution**: Compare the Scope Firewall, Structure Decision, validation scans, and contract non-goals with the binding Lastenheft.

## Runtime design integrity

- [X] **PQ-004** Focus-veto ownership and the state-propagation matrix are deterministic.
  **Execution**: Verify the typed focus outcome, exactly-once pre-mutation veto, unchanged rejection state, and distinct `Active`, `Dragging`, `Focused`, `Exposed`, and `Disabled` rules.
- [X] **PQ-005** Pending-event and idle behavior is bounded and testable without production busy waiting.
  **Execution**: Confirm one pending slot, pending-first ordering, one idle call per empty poll, a replaceable CPU-release seam, and no queue/thread/timer scope.
- [X] **PQ-006** Desktop, close, and modal responsibilities preserve ownership and safe-close vetoes.
  **Execution**: Trace stack/geometry candidates, close results, one direct modal child per owner, temporary insertion, and `finally`-based focus restoration through plan and data model.
- [X] **PQ-007** Command availability has one snapshot source without overwriting static menu/status constraints.
  **Execution**: Verify active-view provider, legacy compatibility input, refresh triggers, pre-dispatch recheck, and separate manual/context disabled inputs.
- [X] **PQ-008** Real keyboard ingress reuses the canonical translator and drag remains keyboard-complete.
  **Execution**: Confirm raw `ConsoleKeyInfo` reaches Compatibility ownership, canonical modifier bits are used, and pointer/arrows/Enter/Escape share one bounded drag session.

## Evidence and governance

- [X] **PQ-009** Turbo Vision is primary, Free Vision is secondary, pinned, external, and read-only.
  **Execution**: Verify source precedence, exact commit, reviewed hashes, own-word comparison, and no vendoring or mechanical translation.
- [X] **PQ-010** Feature-024 resolution preserves the accepted audit baseline.
  **Execution**: Confirm that resolution metadata and a readable closure addendum are appended only after passing proofs; original observations and provenance remain intact.
- [X] **PQ-011** All seven installed presets have explicit proportional applicability.
  **Execution**: Compare the preset matrix with the Constitution Check, including C3A/C5, cross-platform input proof, agent parity, and autonomous validator triggers.
- [X] **PQ-012** Public API, documentation, coverage, version, and remote-closeout gates are complete.
  **Execution**: Verify XML documentation, DocFX/Axe/Lynx, full Release/coverage, one build-counter increment per build/test invocation, shared-file serialization, review convergence, and `MergeAndSync` cleanup.

## Review result

All plan-quality checks pass. No scope expansion, unresolved product decision, or task-generation blocker remains.
