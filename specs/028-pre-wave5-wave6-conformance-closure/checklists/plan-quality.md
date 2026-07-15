# Plan Quality Checklist: Pre-Wave-5 and Wave-6 Conformance Closure

**Purpose**: Validate that the implementation design is bounded, traceable,
test-first, and executable before task generation.
**Created**: 2026-07-14

## Scope and Architecture

- [x] PQ001 Does the plan preserve Feature 024 as the canonical audit dataset and add only feature-local closure evidence? [Plan - Summary; Research - Decision 1]
  - Durchfuehrungshinweis: Compare every planned write with the Feature-024 ownership boundary; reject any duplicate or rewritten canonical finding.
- [x] PQ002 Are all product, API, dependency, example, and historical-source changes excluded? [Plan - Summary; Contract - Input Contract]
  - Durchfuehrungshinweis: Inventory every planned changed path and classify it as evidence, test infrastructure, CI, guidance, statistics, version, or archive metadata.
- [x] PQ003 Does the gate advance only to `ReadyForTerminalGuiAudit` while both waves remain blocked? [Data Model - ClosureRun; Contract - Gate Contract]
  - Durchfuehrungshinweis: Search every artifact for gate-state terms and compare the exact state transition and next intake.
- [x] PQ004 Is Feature 029 the sole next intake and is Terminal.GUI analysis excluded from Feature 028? [Plan - Phase 4; Research - Decision 9]
  - Durchfuehrungshinweis: Search for any Terminal.GUI source-fetch, contract-analysis, or finding-generation task in the Feature-028 design.

## Evidence Cardinality and Traceability

- [x] PQ005 Are the exact 13 FindingClosure and 7 IntegrationSlice sets plus the required 13-row ConsumerReadiness baseline and extension rule consistent? [Research - Decision 4; Data Model]
  - Durchfuehrungshinweis: Count all stated baseline cardinalities and identifier ranges in spec, plan, data model, contract, and quickstart; confirm that a genuinely new shared flow can be added without hiding or replacing a baseline row.
- [x] PQ006 Does every finding preserve its canonical contract, owner, resolution paths, and real-path proof relation? [Data Model - FindingClosure]
  - Durchfuehrungshinweis: Compare the proposed schema with the existing Feature-024 finding and resolution fields and identify any lost relation.
- [x] PQ007 Do seven slices jointly cover all accepted real-path boundaries without helper-only closure? [Plan - Phase 3; Contract - Integration Contract]
  - Durchfuehrungshinweis: Map each slice to named production entries, negative boundaries, and existing test methods.
- [x] PQ008 Do all six Wave-5 and seven Wave-6 baseline consumer groups remain independently represented? [Data Model - ConsumerReadiness]
  - Durchfuehrungshinweis: Compare the planned baseline with the existing Feature-024 consumer-readiness review and verify no group is merged away.

## Test-First and Validation

- [x] PQ009 Is the closure validator introduced before its dataset and required to fail for a missing dataset? [Plan - Phase 2; Quickstart - Section 2]
  - Durchfuehrungshinweis: Verify red command, failure boundary, green input, and malformed-data rejection are ordered as one test-first chain.
- [x] PQ010 Are existing real-path tests reused only when their combined assertions cover each named slice? [Research - Decision 3]
  - Durchfuehrungshinweis: Inspect representative existing test methods for R-028-001 through R-028-007 and record any proof gap requiring a new test.
- [x] PQ011 Are local full tests, canonical coverage, format, DocFX, A11Y, Lynx, secret, dependency, and protected-path checks explicit? [Plan - Validation Strategy]
  - Durchfuehrungshinweis: Compare the validation list with AGENTS.md, the Constitution, and Feature-024 closure obligations.
- [x] PQ012 Does every explicit `dotnet test` or `dotnet build` boundary require exactly one manual build-counter increment? [Plan - Version and Build-Counter Strategy]
  - Durchfuehrungshinweis: Enumerate planned build/test commands and ensure each has a preceding counter task while non-build commands do not.

## Cross-Platform and Remote Gates

- [x] PQ013 Does Windows execute the same runtime CI body as Linux and macOS rather than a tooling-only substitute? [Research - Decision 5; Gate requirements]
  - Durchfuehrungshinweis: Compare the current CI matrix/body with all three runtime-gate token sets and the prior Windows proof boundary.
- [x] PQ014 Is WSL recorded as an honest `N/A` with a concrete re-evaluation trigger? [Research - Decision 6]
  - Durchfuehrungshinweis: Verify that no Windows result is relabeled as WSL and that empty command/runner tokens are intentional.
- [x] PQ015 Are committed requirements frozen before remote execution and temporary evidence exact-head, provider-neutral, and untracked? [Data Model - AutonomousGateRequirement; TemporaryGateEvidence]
  - Durchfuehrungshinweis: Compare the JSON schema to both installed validator scripts and verify one Primary row per gate.
- [x] PQ016 Is remote closeout non-recursive and limited to the authorized Human Approval bypass? [Contract - Delivery Contract]
  - Durchfuehrungshinweis: Trace reviewed head, technical checks, thread convergence, merge, closeout, and main sync without requiring a self-invalidating evidence commit.

## Governance and Maintainability

- [x] PQ017 Are all seven installed presets covered with complete applicability metadata? [Plan - Constitution Check]
  - Durchfuehrungshinweis: Compare installed preset versions and every required governance field against the data model.
- [x] PQ018 Are shared evidence, version, status, workflow, agent, archive, and statistics writers serialized? [Plan - Autonomous Execution Contract]
  - Durchfuehrungshinweis: Build the shared-writer path list and prohibit parallel task markers for every overlapping writer.
- [x] PQ019 Is preset promotion conditional on a deterministic provider-neutral reproduction rather than ordinary project findings? [Research - Decision 10]
  - Durchfuehrungshinweis: Verify retrospective outcomes include `NoPromotion` and forbid empty Home-Baseline PRs or releases.

## Review Result

- [x] PQ020 Every instruction was executed and no Constitution exception or unresolved clarification remains. The review separated independent secret gates, preserved consumer extensions, completed FR-028 metadata, added didactic-comment review, and synchronized agent context. [Readiness]
