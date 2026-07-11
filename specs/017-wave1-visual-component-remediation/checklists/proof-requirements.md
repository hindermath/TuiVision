# Smoke And Evidence Requirements Quality Checklist

**Purpose**: Review whether proof and traceability requirements are ready for planning  
**Created**: 2026-07-11  
**Audience**: Maintainers, test authors, and PR reviewers

## Primary Proof Definition

- [x] CHK001 Is the required app-loop event, command, or key path defined? [Completeness, Spec FR-011]
- [x] CHK002 Are concrete state, view-tree, and rendered buffer/cell proof layers all named? [Completeness, Spec FR-011]
- [x] CHK003 Is the exception rule for a technically impossible proof layer explicit and evidence-bound? [Edge Case, Spec FR-011]
- [x] CHK004 Are direct helpers excluded from replacing primary proof? [Clarity, Spec FR-012]
- [x] CHK005 Are startup-only, static-text-only, and private-inspection-only proofs rejected? [Consistency, Spec FR-005 and SC-005]

## Matrix And Traceability

- [x] CHK006 Is separate primary proof required for every example area? [Coverage, Spec SC-004]
- [x] CHK007 Is 16/16 Tutorial token coverage specified? [Coverage, Spec FR-013]
- [x] CHK008 Is accidental generic or duplicate Tutorial output addressed? [Edge Case, Spec FR-013]
- [x] CHK009 Are historical source, framework decision, runtime, smoke, render, docs, and validation linked? [Traceability, Spec FR-022]
- [x] CHK010 Does every framework contract area receive exactly one allowed decision? [Consistency, Spec FR-017]
- [x] CHK011 Are the four framework-decision terms exact and mutually exclusive? [Clarity, Spec FR-017]
- [x] CHK012 Are `FollowUpHardening` issue, scope reason, owner/boundary, and trigger fields specified? [Completeness, Spec FR-020]

## Validation And Completion

- [x] CHK013 Are targeted, full, coverage, formatting, and diff-hygiene outcomes measurable? [Acceptance Criteria, Spec SC-010]
- [x] CHK014 Is the five-assembly 70% coverage floor explicit? [Measurability, Spec SC-010]
- [x] CHK015 Is generated-output hygiene an explicit completion boundary? [Coverage, Spec FR-027]
- [x] CHK016 Is read-only historical-source integrity measurable as zero changes? [Measurability, Spec SC-008]
- [x] CHK017 Are no-scope-leak outcomes measurable at completion? [Acceptance Criteria, Spec SC-012]
- [x] CHK018 Is Lastenheft archival required through the repository workflow? [Dependency, Spec FR-025]

## Review Result

All items passed. Planning can derive an evidence schema and dependency-ordered
proof tasks without further clarification.
