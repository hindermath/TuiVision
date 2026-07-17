# Proof, A11Y, and Governance Requirements Checklist

**Purpose**: Review primary proof, learner accessibility, governance, and
delivery requirements before planning
**Created**: 2026-07-17
**Feature**: [spec.md](../spec.md)

## Primary Proof

- [x] CHK001 Are app loop, event, command, dialog, focus, view, status, Description, and cell proof explicit? [Completeness, Spec §FR-029]
- [x] CHK002 Are direct helpers limited to setup or supplemental proof? [Clarity, Spec §FR-030]
- [x] CHK003 Are normal and `48x16` proof scopes both measurable? [Coverage, Spec §SC-006]
- [x] CHK004 Are positive, rejection, abort, recovery, and unsupported paths required? [Coverage, Spec §FR-031]
- [x] CHK005 Are normal PTY and controlled smoke starts distinguished? [Clarity, Spec §FR-032]

## Accessibility and Learning

- [x] CHK006 Is full keyboard access required for every primary command and mouse path? [Coverage, Spec §FR-008, FR-023]
- [x] CHK007 Are focus, status, errors, capabilities, and fallbacks text-first? [Completeness, Spec §FR-026]
- [x] CHK008 Is High Contrast meaningful without color alone? [A11Y, Spec §FR-027]
- [x] CHK009 Does Description include purpose, operation, safety, deviation, and proof boundary? [Clarity, Spec §FR-028]
- [x] CHK010 Is the guide content complete, bilingual, CEFR-B2, semantic, and text-first? [Coverage, Spec §FR-040]
- [x] CHK011 Is didactic inline-comment review selective and limited to non-trivial logic? [Consistency, Spec §FR-041]

## Governance and Delivery

- [x] CHK012 Are all seven accepted presets named with exact versions? [Completeness, Spec §CR-012]
- [x] CHK013 Are NIST SSDF, CWE Top 25, STRIDE, CIA, and CAPEC applicable? [Coverage, Spec §CR-004, CR-005]
- [x] CHK014 Do ASVS, AI-SBOM, regulatory, cloud, and script-parity N/A decisions have triggers? [Clarity, Spec §CR-006-CR-011]
- [x] CHK015 Are A11Y, cross-platform runtime, agent parity, and autonomous state requirements applicable? [Coverage, Spec §CR-003, CR-011-CR-013]
- [x] CHK016 Are declared gates required before implementation and exact-head evidence before merge? [Traceability, Spec §FR-042-FR-044]
- [x] CHK017 Is remote authority absent from user-facing product requirements? [Boundary, Assumptions]

## Notes

- Requirements review passed. No additional formal clarification is needed
  before planning.
