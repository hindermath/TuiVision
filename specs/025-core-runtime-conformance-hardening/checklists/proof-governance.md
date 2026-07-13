# Requirements Checklist: Proof and Governance Boundaries

**Purpose**: Review requirements quality for real-path evidence, historical provenance, A11Y, platform behavior, governance, and scope
**Created**: 2026-07-13
**Audience**: Feature author, maintainers, governance reviewers, and pull-request reviewers
**Feature**: [spec.md](../spec.md)

## Real-Path Evidence

- [X] CHK001 Are real-path entry points defined for event loop, keyboard ingress, focus, close, modal, stack, command, and drag flows? [Completeness, Spec §FR-019-FR-021]
- [X] CHK002 Is normalized direct-event injection explicitly supplemental for real keyboard proof? [Boundary, Spec §FR-016]
- [X] CHK003 Are positive, rejection, cancellation, empty-state, repeated-execution, and shutdown classes represented? [Coverage, Spec §Edge Cases]
- [X] CHK004 Is Finding Evidence defined with all fields needed to distinguish behavior, proof, history, API/A11Y impact, and residual boundary? [Completeness, Spec §Key Entities]
- [X] CHK005 Is one allowed closure decision per Finding objectively enforceable? [Measurability, Spec §SC-001]

## Historical and Secondary Sources

- [X] CHK006 Is Turbo Vision 2.0.3 identified as the primary intent source and Free Vision only as a secondary implementation opinion? [Clarity, Spec §FR-024-FR-025]
- [X] CHK007 Is the exact immutable Free Vision revision specified? [Traceability, Spec §FR-024]
- [X] CHK008 Are `tv203s/`, external Free Vision, `TVDEMOS/`, and `TVFM/` consistently read-only? [Consistency, Spec §Scope Boundaries]
- [X] CHK009 Are mechanical translation, vendoring, and normative adoption of Free Vision explicitly excluded? [Boundary, Spec §FR-025]
- [X] CHK010 Are material modern C# deviations required to state user- or API-visible impact? [Completeness, Spec §FR-025]

## Accessibility and Documentation

- [X] CHK011 Is a complete keyboard alternative required for every mouse or drag path? [Coverage, Spec §FR-028]
- [X] CHK012 Are focus and state changes required to remain text-first and announcement-compatible? [Completeness, Spec §FR-028]
- [X] CHK013 Are DE-first/EN-second CEFR-B2 requirements present for learner-facing explanations and public XML documentation? [Clarity, Spec §CR-002-CR-003]
- [X] CHK014 Is the didactic comment trigger limited to non-trivial logic and reason-focused explanations? [Boundary, Spec §FR-027]
- [X] CHK015 Are XML/API/guide/navigation changes tied to DocFX, Axe, and text-oriented review without forcing those gates for unrelated source-only edits? [Consistency, Spec §CR-003]

## Security and Platform Quality

- [X] CHK016 Are unknown event, ingress, and state-transition failures specified as fail-closed? [Security, Spec §FR-029]
- [X] CHK017 Are NIST SSDF, CWE Top 25, STRIDE, CIA, and CAPEC applicability tied to the actual changed boundaries? [Completeness, Spec §Governance Applicability]
- [X] CHK018 Do all `N/A` security, supply-chain, cloud, provider, AI, and regulatory decisions have explicit scope triggers? [Governance, Spec §Governance Applicability]
- [X] CHK019 Are macOS, Linux, and Windows/WSL proof expectations stated for affected keyboard, modifier, and terminal paths? [Cross-Platform, Spec §FR-030]
- [X] CHK020 Is an unavailable but required platform proof explicitly blocking rather than silently skipped? [Reliability, Spec §FR-030]
- [X] CHK021 Is script parity trigger-based and `N/A` only while no repository script changes? [Cross-Platform, Spec §Governance Applicability]

## Governance and Agent Parity

- [X] CHK022 Are all six base presets listed with exact accepted local versions? [Traceability, Spec §Governance Applicability]
- [X] CHK023 Is optional `autonomous-run-governance` v0.1.0 separated from the six-base-preset matrix? [Consistency, Spec §CR-012]
- [X] CHK024 Are governance decisions constrained to `Applicable`, `N/A`, or `Open` with complete ownership, review, evidence, residual-risk, and re-evaluation fields? [Completeness, Spec §FR-037]
- [X] CHK025 Is the deterministic-validator trigger captured before affected implementation edits? [Governance, Spec §Autonomous Run Governance]
- [X] CHK026 Are the five maintained agent surfaces updated together only when shared guidance changes? [Agent Parity, Spec §FR-034]
- [X] CHK027 Are `.specify/templates/` explicitly `N/A` unless intentionally changed? [Boundary, Spec §Agent Parity Governance]
- [X] CHK028 Is remote delivery authority kept out of product requirements and deferred to plan and run evidence? [Consistency, Spec §CR-013]

## Scope and Ordering

- [X] CHK029 Are `F010`-`F013`, Feature 026, Feature 028, Wave 5, and Wave 6 clearly separated from Feature 025 implementation? [Boundary, Spec §Scope Boundaries]
- [X] CHK030 Are new packages, broad rewrites, historic platform recreation, full desktop drag/drop, and pointer-only operation excluded? [Boundary, Spec §Out of Scope]
- [X] CHK031 Is the accepted order after Audit Revision 2, before 026 and 028, and before Waves 5/6 unambiguous? [Dependency, Spec §Dependencies and Ordering]
- [X] CHK032 Are statistics, Pflichtenheft, audit status, Lastenheft archive, and agent-context completion surfaces specified without creating application scope? [Completeness, Spec §FR-033-FR-035]

## Review Result

All proof, governance, accessibility, platform, and scope requirements are
complete enough for implementation planning. No checklist item requires a
specification change.
