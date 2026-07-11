# Implementation Readiness Checklist: Secure Development Hardening

**Purpose**: Validate whether requirements and planning artifacts are sufficiently precise for dependency-ordered task generation.  
**Created**: 2026-07-11

## Evidence Foundation

- [x] CHK001 Are the two evidence homes and their distinct lifetimes explicitly defined? [Clarity, Research R03]
  - Durchführungshinweis: Compare project-wide control evidence with feature-local command/PR evidence.
- [x] CHK002 Is the control-row schema complete enough to generate deterministic assessment tasks? [Completeness, Data Model ControlAssessment]
  - Durchführungshinweis: Trace every mandatory field to status-specific validation rules.
- [x] CHK003 Is there a mechanical acceptance method for 157 source IDs and 157 assessment rows? [Measurability, Contract §2]
  - Durchführungshinweis: Confirm the source regex, unique-count rule, and missing/unknown comparison are documented.
- [x] CHK004 Are finding, remediation, evidence, validation, and follow-up relationships defined? [Completeness, Data Model Relationships]
  - Durchführungshinweis: Walk a sample control from source through acceptance and confirm no missing entity link.

## Implementable Slices

- [x] CHK005 Can security-document consolidation be decomposed without concurrent edits to shared evidence? [Dependency, Plan Phase 2]
  - Durchführungshinweis: Identify shared files and require serialized task ownership.
- [x] CHK006 Are SBOM tool-manifest, generation, validation, and retention requirements independently reviewable? [Clarity, Contract §6]
  - Durchführungshinweis: Separate tool definition, command proof, CI use, and generated-output cleanup.
- [x] CHK007 Are workflow pinning and Dependabot changes bounded from provider-setting changes? [Clarity, Research R07]
  - Durchführungshinweis: Mark repository-file tasks separately from human-only GitHub configuration items.
- [x] CHK008 Is the disclosure policy content precise enough to implement without inventing contact data or SLA? [Clarity, Contract §7]
  - Durchführungshinweis: Compare required sections with prohibited organizational commitments.
- [x] CHK009 Are Bash and PowerShell option mappings and state expectations complete enough for test-first implementation? [Completeness, Contract §8]
  - Durchführungshinweis: Convert every option and error case into a `ScriptContractCase` without adding new semantics.

## Validation Readiness

- [x] CHK010 Is each validation command tied to a trigger, expected result, retention rule, and failure boundary? [Completeness, Data Model ValidationRun]
  - Durchführungshinweis: Build a validation table from quickstart and flag commands without one of the four fields.
- [x] CHK011 Are version increments ordered before every build/test and alignment before commit/push? [Consistency, Quickstart §7]
  - Durchführungshinweis: Simulate the command sequence and check patch/build counter transitions.
- [x] CHK012 Are full Release, per-assembly coverage, DocFX, web-A11Y, package, SBOM, script, and secret gates all mandatory where triggered? [Coverage, Contract §12]
  - Durchführungshinweis: Compare SC-006 through SC-012 with the complete command list.
- [x] CHK013 Is Windows/WSL evidence bounded realistically while Bash/PowerShell semantic parity remains mandatory? [Clarity, Plan Target Platform]
  - Durchführungshinweis: Distinguish local/CI proof from unavailable provider runner claims.
- [x] CHK014 Are cleanup requirements explicit for generated documentation, API YAML, test, coverage, SBOM, cache, and logs? [Completeness, Spec FR-034]
  - Durchführungshinweis: Compare `.gitignore`, retention rules, and final tracked-file scan expectations.

## Final Delivery Readiness

- [x] CHK015 Are statistics, active context, next-step marker, Lastenheft archive, version, evidence, and PR description ordered after accepted implementation? [Dependency, Spec FR-036]
  - Durchführungshinweis: Ensure completion metadata cannot be marked before control and validation acceptance.
- [x] CHK016 Are Analyze convergence and automated PR-review remediation loops defined before merge? [Coverage, Spec SC-010, Quickstart §10]
  - Durchführungshinweis: Confirm actionable findings return to artifacts/tasks/implementation and revalidation.
- [x] CHK017 Are task parallelism limits explicit for shared evidence, workflows, scripts, agent files, statistics, and version metadata? [Dependency, Plan Phase 2]
  - Durchführungshinweis: List all shared files and disallow parallel markers for overlapping writes.
- [x] CHK018 Are no unresolved clarification, placeholder, TODO, contradictory status, or unmapped success criterion permitted before tasks? [Measurability, Spec Clarifications, Plan Post-Design Gate]
  - Durchführungshinweis: Run marker, vocabulary, ID, and requirement-to-plan mapping checks.
