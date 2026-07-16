# Implementation Readiness Checklist: Terminal.GUI Conformance Audit

**Purpose**: Confirm that specification, plan, tasks, evidence boundaries, and delivery steps are executable after Analyze remediation
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Artifact Convergence

- [x] IRD001 Specification has no unresolved clarification or placeholder marker.
- [x] IRD002 All relation, consumer, finding, governance, and gate vocabularies are closed and consistent.
- [x] IRD003 Plan contains no unresolved technical decision or Constitution exception.
- [x] IRD004 Research verifies the exact Terminal.GUI tag object, commit, license, and no-copy boundary.
- [x] IRD005 Data model and acceptance contract agree on cardinalities and relationships.
- [x] IRD006 All prior requirements and plan checklists have zero incomplete items.

## Task Quality

- [x] IRD007 Tasks are exactly T001 through T130 with no duplicate or missing strict task ID.
- [x] IRD008 Every user-story task carries the correct story label.
- [x] IRD009 No task is marked parallel because accepted writes share evidence or dataset files.
- [x] IRD010 Evidence and gate requirements precede the first implementation edit.
- [x] IRD011 The test-only validator and red test precede both accepted JSON datasets.
- [x] IRD012 The D02-specific green slice precedes full 48-contract expansion.
- [x] IRD013 Every build/test task has an immediately preceding version/build-counter task.
- [x] IRD014 Archive commands name exact scripts, source file, branch, and no-commit boundary.
- [x] IRD015 Every remote task names `pr-evidence.md` or the declared exact-head evidence boundary.

## Scope and Delivery

- [x] IRD016 Product source, API, packages, dependencies, examples, historical trees, consumers, and external source are protected.
- [x] IRD017 Feature 030, magiblot review, Wave 5, Wave 6, hardening Lastenhefte, and closure Lastenheft remain outside Feature 029.
- [x] IRD018 Full Release, coverage, DocFX/A11Y, security, scope, and agent-parity validation are task-covered.
- [x] IRD019 Merge authority and narrow bypass conditions match the current user instruction.
- [x] IRD020 Retrospective and the separate v0.2.2 documentation promotion occur only after Feature-029 merge and main sync.

## Analyze Result

- Initial findings: one High test-first ordering conflict, one Medium consumer-vocabulary gap, and one Low archive-command specificity gap.
- Remediation: all three corrected in place.
- Repeated analysis: zero Critical, High, or Medium findings; no unmapped task and full requirement/success-criterion coverage.
