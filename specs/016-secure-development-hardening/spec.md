# Feature Specification: Secure Development Hardening

**Feature Branch**: `016-secure-development-hardening`  
**Created**: 2026-07-11  
**Status**: Draft  
**Input**: `Lastenheft_Secure-Development-Hardening.md` is the binding requirements document.

## Clarifications

### Session 2026-07-11

- Q: How far may this feature remediate findings? → A: Implement small and medium, clearly evidenced code, test, CI, script, and evidence fixes; route broad architecture, provider, organizational, legal, or irreversible changes to follow-up work.
- Q: How are non-repository decisions handled? → A: Use conservative documented defaults; record human-only decisions as `Open` with owner, risk, follow-up, and re-evaluation trigger. Stop only for credentials, legal decisions, irreversible external changes, scope impossibility, or an unremediated critical risk.
- Q: When are iterative Spec-Kit quality loops complete? → A: When no open clarification, incomplete checklist, critical/high/medium issue, mapping gap, or implementation-relevant low issue remains. Pure style observations may be accepted with rationale.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Obtain an Auditable Security Baseline (Priority: P1)

As a maintainer or reviewer, I need every relevant secure-development control to have an explicit applicability decision and evidence path so that no security requirement is silently omitted or overstated.

**Why this priority**: The baseline determines which remediation is required and prevents unsupported security or compliance claims.

**Independent Test**: A reviewer can start from the feature evidence, trace every selected control from all twelve secure-development checklists to one approved status and evidence path, and find complete audit fields without reading implementation history.

**Acceptance Scenarios**:

1. **Given** the secure-development guideline, all twelve checklists, the constitution, and six presets, **When** the applicability review completes, **Then** every relevant control is recorded as `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` with the required rationale and audit fields.
2. **Given** a positive security statement, **When** a reviewer follows its evidence path, **Then** the referenced repository artifact or validation result directly supports that statement.
3. **Given** a non-applicable or human-only control, **When** it is reviewed, **Then** its rationale, owner where needed, residual risk, follow-up, and re-evaluation trigger are visible.

---

### User Story 2 - Close Bounded Security Gaps (Priority: P1)

As a developer, I need small and medium security gaps that are safe to fix within this feature to be remediated with tests and evidence, while larger changes remain explicit follow-ups.

**Why this priority**: An assessment-only result would leave actionable repository-local weaknesses unresolved even when they can be closed safely.

**Independent Test**: Each `Applicable` remediation can be reviewed as a bounded change with a linked finding, acceptance condition, validation result, and residual-risk decision.

**Acceptance Scenarios**:

1. **Given** an applicable repository-local finding, **When** it can be fixed without broad architecture, provider, organizational, legal, or irreversible external change, **Then** the feature implements and validates the fix.
2. **Given** a finding beyond the bounded remediation policy, **When** it is classified, **Then** it becomes `FollowUp` or human-only `Open` rather than being silently ignored or expanded into this feature.
3. **Given** a critical or high finding, **When** implementation reaches completion review, **Then** it is either remediated and proven or it blocks merge.

---

### User Story 3 - Demonstrate Supply-Chain and Release Readiness (Priority: P2)

As a release reviewer, I need current dependency, SBOM, vulnerability, provenance, and repository-posture evidence so that distributable TuiVision artifacts are transparent and reviewable.

**Why this priority**: TuiVision is a public, release-capable framework and therefore triggers the constitution's supply-chain requirements.

**Independent Test**: A reviewer can locate a machine-readable component inventory, current dependency/vulnerability results, VEX and AI-SBOM applicability decisions, provenance status, and public-repository posture evidence from the documented release review path.

**Acceptance Scenarios**:

1. **Given** the release-capable repository, **When** supply-chain evidence is generated, **Then** the produced component inventory is machine-readable, excludes secrets and generated noise, and is linked from repository security evidence.
2. **Given** no known vulnerable dependency, **When** VEX applicability is reviewed, **Then** the evidence records why no VEX statement is currently required and which trigger would change that decision.
3. **Given** AI is development tooling only, **When** AI-SBOM applicability is reviewed, **Then** it is `N/A` for the delivered runtime with a product-AI re-evaluation trigger.

---

### User Story 4 - Preserve Cross-Platform and Agent Governance (Priority: P2)

As a contributor using macOS, Linux, Windows, or an AI coding agent, I need security scripts and guidance to remain behaviorally equivalent and safely usable across maintained environments.

**Why this priority**: Security automation that works on only one platform or one agent surface creates an unreviewed bypass path.

**Independent Test**: A reviewer can compare paired scripts, help surfaces, agent instructions, and platform validation evidence and find either parity or an explicit bounded follow-up.

**Acceptance Scenarios**:

1. **Given** a changed critical script, **When** the feature completes, **Then** Bash and PowerShell variants expose equivalent behavior, safe argument handling, help, dry-run semantics where relevant, and matching tests or validation evidence.
2. **Given** the Lastenheft archive workflow, **When** it is used from a commit-free implementation phase, **Then** it can rename without forcing a commit and can still preserve the existing explicit-commit workflow.
3. **Given** shared security guidance changes, **When** agent context is refreshed, **Then** all maintained agent surfaces remain semantically synchronized or document an intentional divergence.

---

### User Story 5 - Keep the Baseline Teachable and Accessible (Priority: P3)

As an apprentice or assistive-technology user, I need the security assessment and remediation evidence to be understandable in text-first German and English without relying on visual layout or unexplained compliance jargon.

**Why this priority**: Security evidence is part of the learning and maintenance surface, not only an audit artifact.

**Independent Test**: Representative evidence remains navigable by headings and tables, explains decisions at CEFR-B2 level, and has an explicit A11Y review result.

**Acceptance Scenarios**:

1. **Given** a learner-facing evidence artifact, **When** it is reviewed in a text-oriented reader, **Then** status, rationale, risk, and next action remain understandable without color or pointer-only interaction.
2. **Given** new or changed non-trivial logic, **When** its review completes, **Then** didactic inline-comment value is explicitly assessed without adding comments that restate obvious code.

### Edge Cases

- A checklist item is generic, duplicated across checklists, or not meaningful for a local terminal UI framework.
- Existing evidence is marked complete but only contains a template stub or feature-local note.
- A control is technically applicable but depends on a human legal, provider, organizational, or release decision.
- Dependency services are temporarily unavailable or disagree with the locally resolved dependency graph.
- A security scan reports a false positive in documentation, examples, generated output, or historical sources.
- A proposed remediation would change public behavior, persistence compatibility, terminal semantics, or historical Turbo Vision intent.
- A script behaves equivalently on happy paths but differs in error handling, argument injection protection, dry-run behavior, or exit codes.
- Generated SBOM, DocFX, test, coverage, or scan output could accidentally enter Git.
- A new review finding appears after a previously clean Analyze or pull-request review pass.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST use `Lastenheft_Secure-Development-Hardening.md` as binding input and remain ordered after feature 015 and before Wave-1 visual remediation.
- **FR-002**: The RL-SE self-assessment and GSDB intensive-review Lastenhefte MAY provide context but MUST remain separate future intakes rather than being silently combined with feature 016.
- **FR-003**: The feature MUST review the secure-development guideline, checklist collection, all twelve individual checklists, related documents, constitution, installed presets, repository security evidence, tests, CI, scripts, and agent surfaces.
- **FR-004**: Every reviewed control MUST receive exactly one status: `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp`.
- **FR-005**: Every control row MUST record a stable identifier, source control, status, rationale, evidence path, owner, reviewer, review date, result, residual risk, follow-up, re-evaluation trigger, and human-only flag.
- **FR-006**: `AlreadySatisfied` MUST require current repository evidence and MUST NOT be inferred from a template, old feature note, or policy statement alone.
- **FR-007**: `N/A` MUST include a technical or factual rationale and a concrete re-evaluation trigger.
- **FR-008**: `Open` MUST include owner, priority, risk, concrete follow-up, and re-evaluation trigger; human-only decisions MUST remain visibly human-only.
- **FR-009**: `FollowUp` MUST identify why remediation exceeds feature 016 and name the later work item or evidence boundary.
- **FR-010**: The feature MUST select and justify the applicable portions of CL-01 through CL-12; no checklist may be silently omitted.
- **FR-011**: The feature MUST consolidate project-wide evidence under `docs/security/` or an explicitly justified equivalent location and remove misleading stub status from evidence accepted as complete.
- **FR-012**: The feature MUST provide a traceable mapping from applicable controls to bounded remediation tasks and validation results.
- **FR-013**: Small and medium repository-local findings MUST be remediated when the change is bounded, testable, reversible, and compatible with accepted architecture and behavior.
- **FR-014**: Broad architecture, provider, organizational, legal, commercial-distribution, credential, or irreversible external changes MUST NOT be performed autonomously and MUST be recorded as `Open` or `FollowUp`.
- **FR-015**: Unremediated critical or high risks MUST block feature merge; medium and implementation-relevant low findings MUST be remediated or explicitly moved outside scope with accepted rationale.
- **FR-016**: The feature MUST evaluate input validation, file/resource parsing, serialization, event/command boundaries, terminal input, scripts, error handling, and output safety using NIST SSDF and relevant CWE Top 25 categories.
- **FR-017**: The feature MUST maintain a repository-level threat model with assets, trust boundaries, STRIDE categories, relevant CAPEC references, mitigations, and residual risks.
- **FR-018**: The feature MUST maintain project-wide secure-architecture concepts, quality scenarios, risk/debt entries, and S-ADR decisions where an architecturally significant security choice exists.
- **FR-019**: The feature MUST produce current dependency and vulnerability evidence for direct and transitive packages without changing package versions unless a concrete vulnerability, deprecation, compatibility need, or separately justified maintenance decision requires it.
- **FR-020**: Because TuiVision is release-capable, the feature MUST provide a machine-readable SBOM generation and verification path and link its result from supply-chain evidence.
- **FR-021**: The feature MUST record VEX, SLSA/provenance, and OpenSSF Scorecard status, including bounded improvements or explicit follow-up boundaries.
- **FR-022**: AI-SBOM MUST remain `N/A` while AI is development tooling only; runtime/product AI, models, datasets, inference infrastructure, or delivered AI assets MUST trigger re-evaluation.
- **FR-023**: OWASP ASVS MUST be `N/A` unless web, API, HTTP, authentication, or authorization-bearing service scope enters the feature.
- **FR-024**: Zero Trust, BSI C3A, and BSI C5 MUST be `N/A` unless distributed service, cloud provider, remote identity, deployment topology, shared-responsibility, or cloud-assurance scope enters the feature.
- **FR-025**: NIS2, CRA, EU AI Act, DORA, and DPIA applicability MUST be recorded without making a legal compliance claim; unresolved legal or market-placement decisions MUST remain human-only `Open`.
- **FR-026**: The feature MUST review cryptographic requirements and record `N/A` unless project-owned cryptographic processing, key management, signing, encryption, or trust-anchor behavior is present.
- **FR-027**: The feature MUST review vulnerability disclosure and response readiness, including discoverability, reporting path, ownership, and response follow-up.
- **FR-028**: Critical scripts changed by this feature MUST preserve Bash/PowerShell parity, safe argument handling, equivalent exit behavior, bilingual help, and repository-required documentation.
- **FR-029**: The Lastenheft archive scripts MUST gain a commit-free mode while preserving the existing explicit commit behavior; the mode MUST not stage or commit unrelated changes.
- **FR-030**: Shared security or workflow guidance changes MUST be synchronized across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md`.
- **FR-031**: Repository-owned Spec-Kit templates and preset installations MUST be reviewed for impact and changed only when a project-wide rule or template defect is actually identified.
- **FR-032**: User-facing security evidence MUST be German-first/English-second at CEFR-B2, semantic, text-first, and reviewable without color-only or pointer-only meaning.
- **FR-033**: New or changed non-trivial logic MUST be reviewed for selective didactic inline-comment value under the feature-015 rule.
- **FR-034**: Generated SBOM, VEX, provenance, DocFX, test, coverage, scan, cache, log, credential, and temporary outputs MUST remain outside Git unless a specific source-controlled evidence format is intentionally approved.
- **FR-035**: Feature evidence MUST record every command, result, skipped trigger, failure boundary, accepted residual risk, and follow-up needed for final review.
- **FR-036**: The feature MUST update project statistics, progress markers, active feature context, and archive the completed Lastenheft only after accepted implementation work is complete.

### Constitution Requirements *(mandatory)*

- **CR-001**: The TuiVision Level-2 registry entry is binding: .NET 10/C# terminal UI modules, MSTest, Coverlet, DocFX/A11Y, statistics baselines, and five maintained agent surfaces.
- **CR-002**: C# is the primary implementation language and is on the constitution's memory-safe-language allow-list.
- **CR-003**: NIST SSDF and CWE Top 25 are mandatory and MUST have project-level evidence.
- **CR-004**: Security Governance v0.6.0 applies, including secure-coding context, dependency review, SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM, and regulatory screening.
- **CR-005**: Architecture Governance v0.5.0 applies, including STRIDE/CIA/CAPEC, S-ADR, arc42 security concepts, Zero Trust, SAMM, BSI C3A, and BSI C5 applicability.
- **CR-006**: iSAQB Architecture Governance v0.2.0 applies to architecture goals, context/runtime views, quality scenarios, decisions, risks, and technical debt.
- **CR-007**: A11Y Governance v0.4.0 applies to evidence, documentation, generated HTML triggers, inclusive language, and didactic comment review.
- **CR-008**: Cross-Platform Governance v0.2.0 applies to every script changed by this feature.
- **CR-009**: Agent Parity Governance v0.3.0 applies to all maintained agent surfaces and repository-owned template impact decisions.
- **CR-010**: OWASP ASVS is initially `N/A` because TuiVision has no web/API/auth service scope; re-evaluate if that scope changes.
- **CR-011**: Zero Trust, BSI C3A, and BSI C5 are initially `N/A` because no cloud, distributed service, provider, remote identity, or deployment topology change is planned.
- **CR-012**: AI-SBOM and EU AI Act product obligations are initially `N/A` because AI is development tooling only.
- **CR-013**: CRA market-placement applicability is human-only `Open`; technical readiness evidence may be improved without claiming legal compliance.
- **CR-014**: NIS2 and DORA are initially `N/A` because no essential-service operation, regulated customer flow, or financial-sector ICT service is identified.
- **CR-015**: The default evidence home is `docs/security/`; missing cloud, regulatory, and supply-chain evidence files MUST be created when their applicability decision requires a durable repository record.
- **CR-016**: Public API or XML-comment changes trigger complete documentation plus DocFX and web-A11Y validation; pure internal, script, CI, or evidence changes follow their proportional validation paths.

### Key Entities

- **Control Assessment**: One secure-development control with source, stable identifier, applicability status, rationale, evidence, ownership, result, residual risk, follow-up, re-evaluation trigger, and human-only flag.
- **Evidence Artifact**: A source-controlled document, test, workflow, script, generated-on-demand security artifact definition, or validation result that directly supports one or more assessments.
- **Remediation Item**: A bounded finding linked to affected controls, implementation tasks, acceptance conditions, validation, and residual-risk disposition.
- **Follow-up Item**: A finding outside feature 016 with owner, priority, risk, target work item, and re-evaluation trigger.
- **Validation Run**: A reproducible command with scope, version boundary, result, output-retention rule, and failure boundary.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of the twelve secure-development checklists have an explicit selection rationale and mapped project-level control rows.
- **SC-002**: 100% of reviewed controls use exactly one approved status and contain every mandatory audit field; no empty starter or placeholder row is accepted.
- **SC-003**: 100% of `Applicable` and `AlreadySatisfied` statements reference current evidence that directly supports the result.
- **SC-004**: Zero accepted project-wide security evidence files remain labelled as unpopulated stubs.
- **SC-005**: Zero critical or high findings remain unresolved at merge; zero medium or implementation-relevant low findings remain without remediation or an accepted out-of-scope disposition.
- **SC-006**: A machine-readable SBOM can be generated and validated from a clean checkout using a documented repository command, without committing generated output.
- **SC-007**: Dependency and vulnerability review reports zero unassessed direct or transitive package findings.
- **SC-008**: Every changed critical script has equivalent Bash and PowerShell behavior for success, invalid input, help, commit-free operation, and exit status.
- **SC-009**: All maintained agent surfaces carry semantically equivalent shared security guidance after context refresh.
- **SC-010**: All feature checklists are complete and repeated Analyze reports 100% buildable-requirement task coverage, zero unmapped tasks, and no actionable finding under the agreed convergence rule.
- **SC-011**: Full Release tests pass and every gate-required assembly meets the repository's 70% line-coverage threshold.
- **SC-012**: Formatting, secret scanning, security-script validation, and conditional DocFX/web-A11Y validation complete without an unaccepted failure.
- **SC-013**: Every human-only or deferred item has owner, priority, risk, follow-up, and re-evaluation trigger and makes no unsupported legal or compliance claim.
- **SC-014**: Apprentices can trace a sampled control from source checklist through decision, evidence, remediation or follow-up, and validation using text-first artifacts without relying on color or layout.

## Assumptions

- The manual `Pflichtenheft.md` next-step marker is authoritative; the generated Lastenheft ordering table is inventory, not execution priority.
- TuiVision remains a local terminal UI framework without web/API authentication, cloud deployment, database service, or runtime/product AI in feature 016.
- Public OSS and release capability make repository posture and SBOM evidence applicable even if no package is published during this feature.
- Current package review reports no known vulnerable or deprecated direct/transitive package; this is revalidated during implementation rather than treated as permanent evidence.
- Legal market-placement, CRA role, formal audit approval, vulnerability-reporting organizational ownership, and external provider controls remain human decisions.
- Existing runtime behavior, public APIs, persistence compatibility, examples, and historical Turbo Vision intent remain unchanged unless a concrete security finding justifies a bounded, tested change.
- The experiment may create milestone commits and a final pull request, but it will not perform credential rotation, paid service enrollment, branch-protection changes, or formal compliance attestation.
