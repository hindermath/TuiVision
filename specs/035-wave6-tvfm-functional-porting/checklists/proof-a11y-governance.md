# Proof, A11Y, and Governance Requirements Checklist

**Purpose**: Review primary proof, learner-facing accessibility, and
governance requirements before planning
**Created**: 2026-07-17
**Feature**: [spec.md](../spec.md)

## Primary Proof

- [x] CHK001 Are real app-loop, event, command, focus, view, status, and cell requirements all explicit? [Completeness, Spec §FR-028]
- [x] CHK002 Are direct helpers limited to supplemental or setup evidence? [Clarity, Spec §FR-029]
- [x] CHK003 Are normal and controlled starts both required to terminate predictably? [Coverage, Spec §FR-010, SC-006]
- [x] CHK004 Are positive, negative, abort, recovery, and platform proof classes covered? [Coverage, Spec §SC-004]
- [x] CHK005 Are primary proof outcomes objectively measurable per functional flow? [Measurability, Spec §SC-007]

## Accessibility and Learning

- [x] CHK006 Are status, focus, selection, success, failure, and Description states text-first? [Completeness, Spec §FR-030]
- [x] CHK007 Is keyboard access required for every primary function? [Coverage, Spec §FR-031]
- [x] CHK008 Are constrained terminal and high-contrast boundaries addressed? [Coverage, Spec §FR-032]
- [x] CHK009 Is the bilingual guide content defined with learning and safety topics? [Clarity, Spec §FR-033]
- [x] CHK010 Is didactic inline-comment review required only for non-trivial logic? [Consistency, Spec §FR-034]
- [x] CHK011 Is the existing WCAG/DocFX/Axe path tied to changed learner surfaces? [Traceability, Spec §CR-002, CR-003]

## Governance

- [x] CHK012 Are all seven accepted presets named with exact versions? [Completeness, Spec §CR-012]
- [x] CHK013 Are NIST SSDF, CWE Top 25, STRIDE, CIA, and CAPEC applicability explicit? [Coverage, Spec §CR-004, CR-005]
- [x] CHK014 Do all trigger-based `N/A` decisions include a scope-change re-evaluation boundary? [Clarity, Spec §CR-006-CR-010]
- [x] CHK015 Is cross-platform file and attribute behavior distinguished from script parity? [Consistency, Spec §CR-011]
- [x] CHK016 Are agent guidance, statistics, Pflichtenheft, and processing-order surfaces reviewed together? [Completeness, Spec §CR-013]
- [x] CHK017 Is exact-head evidence required before merge without embedding remote authority in the spec? [Boundary, Spec §FR-040, Assumptions]

## Notes

- Requirements review passed. No additional formal clarification is needed
  before planning.
