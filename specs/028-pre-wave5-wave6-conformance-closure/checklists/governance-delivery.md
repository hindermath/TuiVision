# Governance and Delivery Requirements Checklist

**Purpose**: Review governance, validation, ordering, and delivery-boundary requirements
**Created**: 2026-07-14
**Audience**: Governance and release reviewers
**Feature**: [spec.md](../spec.md)

## Governance Applicability

- [x] CHK001 Are all six baseline presets and autonomous-run-governance v0.2.0 evaluated separately, including validated stop/resume state? [Completeness, Spec §CR-012]
- [x] CHK002 Are NIST SSDF, CWE Top 25, evidence integrity, secrets, dependencies, and exact-head proof explicitly applicable? [Security, Spec §Governance Applicability]
- [x] CHK003 Do ASVS, supply-chain, AI-SBOM, and regulatory `N/A` decisions include concrete scope-change triggers? [Governance, Spec §CR-007-CR-009]
- [x] CHK004 Are STRIDE, CIA, CAPEC, quality, and consumer traceability distinguished from trigger-based cloud and provider controls? [Architecture, Spec §CR-010]
- [x] CHK005 Are keyboard, focus, drag fallback, rejection, modal restoration, bilingual text, and didactic-comment review included in A11Y scope? [A11Y, Spec §Governance Applicability]
- [x] CHK006 Are macOS, Linux, Windows or WSL, path, and terminal boundaries explicit without requiring new script work when none changes? [Cross-Platform, Spec §Governance Applicability]
- [x] CHK007 Are the five maintained agent surfaces one synchronized decision set and repository templates trigger-based `N/A`? [Agent Parity, Spec §FR-030]

## Validation and Evidence

- [x] CHK008 Does the specification require targeted, full Release, coverage, format, docs, A11Y, text, security, dependency, scope, and platform proof? [Completeness, Spec §FR-025]
- [x] CHK009 Is the 70 percent line-coverage threshold explicit for every mandatory assembly? [Measurability, Spec §FR-027]
- [x] CHK010 Must remote evidence identify actual command and platform rather than infer acceptance from a green job name? [Evidence Integrity, Spec §SC-007]
- [x] CHK011 Are every governance and validation row's ownership, review, evidence, risk, follow-up, and trigger fields mandatory? [Completeness, Spec §FR-028-FR-029]
- [x] CHK012 Are temporary exact-head snapshots and generated outputs explicitly excluded from Git? [Scope, Spec §FR-033]

## Gate and Ordering

- [x] CHK013 Is `ReadyForTerminalGuiAudit` the strongest successful Feature-028 gate result? [Clarity, Spec §FR-019]
- [x] CHK014 Do both Waves remain `BlockedPendingTerminalGuiAudit` in every outcome? [Consistency, Spec §FR-020, §SC-009]
- [x] CHK015 Is Feature 029 the sole next intake without being created during this run? [Ordering, Spec §FR-021, §SC-010]
- [x] CHK016 Are Lastenhefte 10, 11, and 12 archived only after all accepted closure requirements pass? [Lifecycle, Spec §FR-031]

## Scope and Authority

- [x] CHK017 Are runtime, API, dependency, example, external, and historical changes excluded from the final diff? [Scope, Spec §FR-022, §SC-004]
- [x] CHK018 Is operational remote authority deliberately left to plan and run evidence rather than product requirements? [Authority Boundary, Spec §CR-013]
- [x] CHK019 Does the specification prohibit empty follow-up work and premature Terminal.GUI analysis? [Scope, Spec §Out of Scope]
- [x] CHK020 Are no-placeholder, clean-candidate, and zero-actionable-thread outcomes objectively measurable? [Acceptance Criteria, Spec §SC-011-SC-012]
