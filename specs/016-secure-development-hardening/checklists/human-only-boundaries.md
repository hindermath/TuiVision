# Human-Only Boundary Checklist: Secure Development Hardening

**Purpose**: Validate that autonomous and human-only requirements are explicit, safe, and auditable.  
**Created**: 2026-07-11

## Decision Ownership

- [x] CHK001 Are human-only decisions explicitly represented rather than hidden in `N/A` or `FollowUp`? [Clarity, Spec FR-008/FR-014]
  - Durchführungshinweis: Review all legal, provider, credential, organizational, and release-role decisions for `Open` plus human-only flag.
- [x] CHK002 Does every human-only `Open` item require owner, priority, risk, action, and re-evaluation trigger? [Completeness, Spec FR-008, SC-013]
  - Durchführungshinweis: Compare mandatory fields across spec, data model, and contract.
- [x] CHK003 Are conservative defaults permitted only when they are reversible and do not assert external facts? [Clarity, Spec Clarifications]
  - Durchführungshinweis: Identify every autonomous default and verify its factual evidence and rollback boundary.
- [x] CHK004 Are unresolved credentials, legal decisions, irreversible external changes, scope impossibility, and critical risk explicit stop conditions? [Completeness, Spec Clarifications]
  - Durchführungshinweis: Confirm each stop class has no contradictory continue instruction elsewhere.

## Provider and Repository Boundaries

- [x] CHK005 Are repository-file CI improvements separated from provider-level settings such as alerts, rulesets, secrets, and publication? [Clarity, Research R06..R07]
  - Durchführungshinweis: Classify each planned GitHub action as repository-controlled or provider-controlled.
- [x] CHK006 Is OpenSSF Scorecard applicability distinguished from publishing results or changing provider settings? [Clarity, Spec FR-021, Research R06]
  - Durchführungshinweis: Ensure project posture can be documented while publication remains human-owned.
- [x] CHK007 Is vulnerability reporting documentation separated from organizational response ownership and provider activation? [Consistency, Spec FR-027, Contract §7]
  - Durchführungshinweis: Compare source-controlled policy obligations with human-only operational commitments.
- [x] CHK008 Are branch protection, vulnerability alerts, credentials, paid services, and formal audit approval outside autonomous execution? [Coverage, Plan Constraints]
  - Durchführungshinweis: Search plan, contract, and quickstart for any command that would mutate these surfaces.

## Legal and Regulatory Boundaries

- [x] CHK009 Is CRA market-placement applicability explicitly human-only `Open`? [Clarity, Spec CR-013]
  - Durchführungshinweis: Ensure no artifact converts technical readiness into a legal conformity claim.
- [x] CHK010 Are NIS2, DORA, EU AI Act, and DPIA statuses based on stated factual scope assumptions? [Completeness, Spec FR-025]
  - Durchführungshinweis: Match every status to current operation/data/AI facts and a scope-change trigger.
- [x] CHK011 Are certification, attestation, conformity, and regulated-entity claims expressly excluded? [Coverage, Contract §9]
  - Durchführungshinweis: Search for absolute compliance language and replace it with applicability or evidence language.

## Risk and Completion

- [x] CHK012 Are human-only items allowed at merge only when they do not represent unresolved critical/high technical risk? [Consistency, Spec FR-015, SC-005]
  - Durchführungshinweis: Compare status semantics with severity merge gates.
- [x] CHK013 Does every follow-up name a concrete future boundary rather than an indefinite deferral? [Measurability, Spec FR-009]
  - Durchführungshinweis: Require a named work item, provider decision, or re-evaluation event for each deferred item.
- [x] CHK014 Is residual risk stated for human-only and deferred decisions? [Completeness, Spec FR-005, SC-013]
  - Durchführungshinweis: Inspect the evidence schema and acceptance contract for mandatory residual-risk fields.
- [x] CHK015 Does remote delivery stop before any unsupported provider/legal action while still allowing PR review and merge? [Consistency, Quickstart §10]
  - Durchführungshinweis: Walk the delivery sequence and identify the authority boundary for every external action.
