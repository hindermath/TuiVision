# Specification Quality Checklist: RL-SE-/Checklist-Selbstpruefung

**Purpose**: Validate specification completeness and quality before proceeding to clarification or planning
**Created**: 2026-08-30
**Feature**: [spec.md](../spec.md)

## Content Quality

- [x] No product implementation details, runtime design, APIs, dependencies, or hardening steps are prescribed
- [x] Focused on audit value, evidence integrity, learner understanding, and governance decisions
- [x] Written for maintainers, reviewers, auditors, and first-year apprentices
- [x] All mandatory sections are completed
- [x] German-first/English-second, CEFR-B2, and text-first expectations are explicit

## Requirement Completeness

- [x] No unresolved clarification markers remain
- [x] Requirements are testable and unambiguous
- [x] Success criteria are measurable
- [x] Success criteria are technology-agnostic except for binding repository evidence identities
- [x] All acceptance scenarios are defined
- [x] Edge cases are identified
- [x] Scope and non-goals are clearly bounded
- [x] Dependencies and assumptions are identified
- [x] Every functional requirement has a verifiable acceptance path
- [x] User scenarios cover control completeness, governance drift, authority boundaries, and inclusive review

## Control and Status Integrity

- [x] Exactly 157 controls and all twelve chapter counts are specified
- [x] The only result statuses are `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, and `FollowUp`
- [x] Rationale, evidence, owner, follow-up, priority, residual risk, and re-evaluation trigger are mandatory for every row
- [x] Positive claims require current direct evidence
- [x] `N/A`, `Open`, and `FollowUp` have status-specific acceptance rules
- [x] Human-only and external-only decisions cannot be claimed as completed by an agent

## Governance Coverage

- [x] Secure-development guideline, manifest, compendium, all twelve checklists, related documents, and mapping file are required inputs
- [x] Both constitution surfaces and their current version drift are in review scope
- [x] All twelve enabled presets and their versions are explicitly covered
- [x] MSL does not waive API, I/O, error, dependency, or supply-chain review
- [x] NIST SSDF, CWE Top 25, ASVS, SBOM, VEX, AI-SBOM, SLSA, SAMM, CAPEC, Zero Trust, and OpenSSF Scorecard are covered
- [x] CRA, NIS2, DORA, EU AI Act, BSI C3A, and BSI C5 are covered without invented legal or provider approval
- [x] A11Y, bilingual delivery, sandbox, secrets, toolchain, agent parity, routing, and autonomous boundaries are covered
- [x] Source-reference policy is explicitly `N/A` with a product-semantics trigger

## Scope and Authority Gates

- [x] Specify-phase writes are limited to `spec.md` and `checklists/requirements.md`
- [x] Automatic repository hardening and silent governance repair are excluded
- [x] Runtime, API, dependency, package, project, example, and product changes are excluded
- [x] Branch creation or switching and manual mutation of feature/run state are excluded
- [x] Remote, provider, secret, model, approval, and organization actions are excluded
- [x] Follow-up intakes, branches, issues, and features cannot be created automatically
- [x] Current repository discrepancies are auditable observations rather than scope expansion

## Feature Readiness

- [x] All functional requirements have clear acceptance criteria
- [x] User scenarios independently exercise the primary review outcomes
- [x] Measurable outcomes cover completeness, evidence quality, governance coverage, A11Y, and scope purity
- [x] No material clarification is required before the next phase
- [x] No placeholder or unfinished example text remains

## Notes

Quality review iteration 1 passed all items. The specification is ready for the
next autonomous phase. Baseline, constitution, preset, and evidence differences
are deliberately retained as review observations; they are not repaired in
Specify. Completion of this checklist does not assert that the later 157-row
self-review has already been performed.
