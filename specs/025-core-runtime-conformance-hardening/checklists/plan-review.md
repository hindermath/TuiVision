# Executed Plan Review: Core Runtime Conformance Hardening

**Purpose**: Execute the cross-artifact checks that task generation and implementation will rely on.
**Reviewed**: 2026-07-13

## Requirements and traceability

- [X] **PR-001** Every user story has an independently reviewable implementation and proof path.
  **Execution**: Map Stories 1-6 to the ordered finding slices, per-slice tests, final matrices, and Feature-024 resolution evidence.
- [X] **PR-002** All nine findings have one contract, one implementation decision, and one final evidence row.
  **Execution**: Compare `F001`-`F009` in spec, plan, research, data model, acceptance contract, and the planned `pr-evidence.md` schema; reject duplicates or omissions.
- [X] **PR-003** Clarification decisions are encoded consistently across artifacts.
  **Execution**: Recheck focus outcomes, one pending slot, owner-scoped modality, command refresh triggers, and the one-cell shared drag threshold in every design artifact.
- [X] **PR-004** `FollowUpHardening` and `ProductDecision` cannot falsely close an accepted finding.
  **Execution**: Confirm both boundaries in the spec and contract and require `Implemented` or `AlreadySatisfied` plus real proof for each accepted finding.

## Source, architecture, and proof boundaries

- [X] **PR-005** Historical and Free Vision comparison is reproducible without source modification.
  **Execution**: Verify reviewed C++ implementation/header paths, external Free Vision commit and hashes, read-only status, and own-word rationale requirements.
- [X] **PR-006** Dependency direction for canonical keyboard translation is explicit and guarded.
  **Execution**: Confirm the bounded Controls-to-Compatibility reference is allowed only without a cycle or packaging conflict; otherwise stop as `ProductDecision` instead of copying code.
- [X] **PR-007** Real-path tests begin at the contract boundary named by each finding.
  **Execution**: Require concrete factory, raw console ingress, `Run()`/event loop, focus/group, Desktop, modal, close, command, and drag entry points rather than helper-only assertions.
- [X] **PR-008** Visible flows require state, View-tree, focus, and buffer/cell evidence.
  **Execution**: Trace this proof combination through Story 3, FR-020, SC-005, the acceptance contract, and final validation.

## Operational readiness

- [X] **PR-009** Deterministic validator triggers are identified before affected edits.
  **Execution**: Require `pr-evidence.md` first and list the Feature-024 JSON validator, XML/docs gates, agent-parity surfaces, coverage configuration, archive markers, and generated-output scans before changing them.
- [X] **PR-010** Shared files are single-writer and cannot be parallelized in tasks.
  **Execution**: Treat feature evidence, audit evidence, version, Pflichtenheft, five agent files, statistics, and Lastenheft archive as serialized task owners.
- [X] **PR-011** Validation scale matches shared runtime and public API changes.
  **Execution**: Require targeted red/green tests, full Release, canonical five-assembly coverage, formatting, DocFX, Playwright/Axe, Lynx, platform evidence, and scope scans.
- [X] **PR-012** Remote delivery and post-merge synchronization have objective convergence gates.
  **Execution**: Require green PR-context checks, zero actionable threads, honest unavailable-review evidence, narrow approval-only bypass, merge commit, branch deletion, and clean `main == origin/main`.
- [X] **PR-013** Agent context is synchronized across all five maintained surfaces.
  **Execution**: Compare the active 025 block in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and both Copilot instruction files; verify the generated technology additions where supported.
- [X] **PR-014** No placeholder or unresolved clarification survives into task generation.
  **Execution**: Scan all feature artifacts and distinguish normative text that names forbidden markers from actual markers or empty template fields.

## Review result

No Critical, High, or Medium plan defect remains. Task generation may proceed in the explicit `F001`, `F008`, `F002`, `F003`, `F004`, `F007`, `F005`, `F006`, `F009` order.
