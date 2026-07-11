# Plan Quality Checklist: Secure Development Hardening

**Purpose**: Validate whether planning requirements are complete, clear, consistent, measurable, and ready for task generation.  
**Created**: 2026-07-11

## Scope and Traceability

- [x] CHK001 Are the binding intake, ordering, separate future intakes, and hard exclusions stated consistently across spec and plan? [Consistency, Spec FR-001..FR-002, Plan Baseline Decisions]
  - Durchführungshinweis: Compare named files, ordering, and exclusions verbatim; flag any broader plan surface.
- [x] CHK002 Is every functional and constitution requirement represented by a planning decision, phase, contract clause, or validation obligation? [Completeness, Spec FR-001..FR-036, CR-001..CR-016]
  - Durchführungshinweis: Build an ID-to-artifact mapping and identify any requirement with no downstream execution path.
- [x] CHK003 Are the five user stories independently deliverable and traceable to plan slices? [Traceability, Spec User Stories, Plan Phase 2]
  - Durchführungshinweis: Map each story to at least one distinct task-planning slice and acceptance result.
- [x] CHK004 Is the 157-control review unit defined consistently and protected against missing, duplicate, or unknown IDs? [Clarity, Plan Baseline Decisions, Contract §2]
  - Durchführungshinweis: Count source headings and compare the count and uniqueness rule in all planning artifacts.

## Technical Decisions

- [x] CHK005 Is the CycloneDX tool choice, version, input, output format, retention, and clean-checkout path sufficiently specified? [Completeness, Spec FR-020/FR-034, Research R05]
  - Durchführungshinweis: Trace each SBOM property from requirement through research, contract, and quickstart.
- [x] CHK006 Are dependency, action-pinning, update-automation, VEX, SLSA, and Scorecard boundaries mutually consistent? [Consistency, Spec FR-019..FR-021, Research R06..R07]
  - Durchführungshinweis: Compare repository-controlled remediation with human/provider follow-ups and reject unsupported completion claims.
- [x] CHK007 Is the rename-script contract precise for default commit, no-commit, preview, path safety, unrelated staged content, errors, and parity? [Clarity, Spec FR-028..FR-029, Contract §8]
  - Durchführungshinweis: Enumerate every state transition for files, index, commits, output, and exit code.
- [x] CHK008 Are runtime, API, dependency, example, historical-source, provider, and legal change boundaries unambiguous? [Clarity, Spec FR-013..FR-015, Plan Constraints]
  - Durchführungshinweis: Search for verbs that could authorize broad changes and compare them with the bounded-remediation policy.

## Architecture and Governance

- [x] CHK009 Are all six preset versions and their applicable checkpoints explicit in plan and contract? [Completeness, Spec CR-004..CR-009, Plan Preset Matrix]
  - Durchführungshinweis: Compare local preset registry output with every named plan version and checkpoint family.
- [x] CHK010 Are SSDF/CWE, STRIDE/CIA/CAPEC, arc42, S-ADR, SAMM, ASVS, Zero Trust, BSI C3A/C5, AI-SBOM, and regulatory decisions explicit? [Coverage, Spec CR-003..CR-015]
  - Durchführungshinweis: Build a standards list from Constitution and verify each receives evidence, `N/A`, `Open`, or `FollowUp` treatment.
- [x] CHK011 Are evidence ownership, reviewer, freshness, residual-risk, and re-evaluation requirements defined for all status types? [Completeness, Spec FR-005..FR-009, Data Model ControlAssessment]
  - Durchführungshinweis: Validate mandatory fields and status-specific fields against every entity and contract rule.
- [x] CHK012 Are agent parity, `.specify/templates/` impact, and multi-agent context refresh requirements consistent? [Consistency, Spec FR-030..FR-031, Contract §10]
  - Durchführungshinweis: Compare the five maintained surfaces and four context-refresh commands with repository policy.

## Validation and Completion

- [x] CHK013 Are build-counter, full-test, coverage, DocFX/A11Y, script, package, SBOM, secret, and generated-output gates dependency ordered? [Completeness, Spec SC-006..SC-012, Quickstart §§7-9]
  - Durchführungshinweis: Walk the quickstart as a state machine and flag commands that lack prerequisite or retention rules.
- [x] CHK014 Are measurable completion thresholds defined for controls, findings, stubs, parity, tests, coverage, Analyze, and human-only items? [Measurability, Spec SC-001..SC-014]
  - Durchführungshinweis: Confirm every success criterion has a numeric, binary, or directly inspectable result.
- [x] CHK015 Are failure, false-positive, unavailable-service, generated-output, and newly introduced finding scenarios covered? [Coverage, Spec Edge Cases]
  - Durchführungshinweis: Map each edge case to a status, stop rule, recovery path, or evidence obligation.
- [x] CHK016 Does the plan contain no unresolved placeholder, contradiction, or constitution exception? [Ambiguity, Plan Post-Design Gate]
  - Durchführungshinweis: Run marker scans, semantic cross-checks, and Constitution comparison before marking complete.
