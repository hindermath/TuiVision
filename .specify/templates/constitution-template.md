# [PROJECT_NAME] Constitution
<!-- Example: Spec Constitution, TaskFlow Constitution, etc. -->

## Core Principles

### [PRINCIPLE_1_NAME]
<!-- Example: I. Library-First -->
[PRINCIPLE_1_DESCRIPTION]
<!-- Example: Every feature starts as a standalone library; Libraries must be self-contained, independently testable, documented; Clear purpose required - no organizational-only libraries -->

### [PRINCIPLE_2_NAME]
<!-- Example: II. CLI Interface -->
[PRINCIPLE_2_DESCRIPTION]
<!-- Example: Every library exposes functionality via CLI; Text in/out protocol: stdin/args → stdout, errors → stderr; Support JSON + human-readable formats -->

### [PRINCIPLE_3_NAME]
<!-- Example: III. Test-First (NON-NEGOTIABLE) -->
[PRINCIPLE_3_DESCRIPTION]
<!-- Example: TDD mandatory: Tests written → User approved → Tests fail → Then implement; Red-Green-Refactor cycle strictly enforced -->

### [PRINCIPLE_4_NAME]
<!-- Example: IV. Integration Testing -->
[PRINCIPLE_4_DESCRIPTION]
<!-- Example: Focus areas requiring integration tests: New library contract tests, Contract changes, Inter-service communication, Shared schemas -->

### [PRINCIPLE_5_NAME]
<!-- Example: V. Observability, VI. Versioning & Breaking Changes, VII. Simplicity -->
[PRINCIPLE_5_DESCRIPTION]
<!-- Example: Text I/O ensures debuggability; Structured logging required; Or: MAJOR.MINOR.BUILD format; Or: Start simple, YAGNI principles -->

## [SECTION_2_NAME]
<!-- Example: Additional Constraints, Security Requirements, Performance Standards, etc. -->

[SECTION_2_CONTENT]
<!-- Example: Technology stack requirements, compliance standards, deployment policies, etc. -->

## [SECTION_3_NAME]
<!-- Example: Development Workflow, Review Process, Quality Gates, etc. -->

[SECTION_3_CONTENT]
<!-- Example: Code review requirements, testing gates, deployment approval process, etc. -->

## Spec-Kit Presets

<!--
  If this project uses Spec-Kit governance presets, list the installed presets,
  versions, priorities, and evidence paths here. The home-baseline default set is:
  security-governance v0.6.0 prio 10, architecture-governance v0.5.0 prio 20,
  isaqb-architecture-governance v0.2.0 prio 30, a11y-governance v0.4.0 prio 40,
  cross-platform-governance v0.2.0 prio 50, agent-parity-governance v0.3.0 prio 60.
  security-governance v0.6.0 keeps conditional AI-SBOM evidence, language-
  specific secure-coding profiles, and regulatory applicability screening for
  NIS2, CRA, EU AI Act, and DORA. Development-tool-only AI usage is N/A; AI
  runtime/product components require G7/BSI cluster evidence.
  architecture-governance v0.5.0 adds conditional BSI C3A cloud-autonomy and
  BSI C5 cloud-compliance assurance evidence for cloud-service selection and
  provider-dependent deployments.
  C#/.NET Level-2 projects default to all six unless a justified exception is documented.
-->

[SPEC_KIT_PRESET_POLICY]

## Governance
<!-- Example: Constitution supersedes all other practices; Amendments require documentation, approval, migration plan -->

[GOVERNANCE_RULES]
<!-- Example: All PRs/reviews must verify compliance; Complexity must be justified; Use [GUIDANCE_FILE] for runtime development guidance -->

**Version**: [CONSTITUTION_VERSION] | **Ratified**: [RATIFICATION_DATE] | **Last Amended**: [LAST_AMENDED_DATE]
<!-- Example: Version: 2.1.1 | Ratified: 2025-06-13 | Last Amended: 2025-07-16 -->

- All six governance presets include audit-ready Spec-Kit run evidence fields; do not treat starter templates as evidence until the current run records concrete decisions, rationale, evidence paths, reviewer, residual risk, and follow-up.
