# Security Applicability Checklist: Secure Development Hardening

**Purpose**: Validate the quality and completeness of security-control applicability requirements.  
**Created**: 2026-07-11

## Control Model

- [x] CHK001 Is every CL-01 through CL-12 checklist explicitly selected and counted? [Completeness, Spec FR-010, Contract §2]
  - Durchführungshinweis: Compare the checklist directory, heading counts, and contract totals.
- [x] CHK002 Are the five status meanings mutually exclusive and sufficient for current, absent, human-only, and deferred controls? [Clarity, Spec FR-004, Data Model AssessmentStatus]
  - Durchführungshinweis: Test each status definition against representative control outcomes and reject overlap.
- [x] CHK003 Are positive claims barred from relying only on policy, templates, old feature notes, or stale command results? [Clarity, Spec FR-006, Contract §5]
  - Durchführungshinweis: Trace the definition of direct current evidence through spec, data model, and contract.
- [x] CHK004 Are critical/high merge blocks and medium/low disposition requirements explicit and consistent? [Consistency, Spec FR-015, Contract §4]
  - Durchführungshinweis: Compare severity handling in spec, finding model, contract, and completion criteria.

## Secure Coding and Architecture

- [x] CHK005 Are local input, file/resource, serialization, event/command, terminal, script, error, and output boundaries all included? [Coverage, Spec FR-016]
  - Durchführungshinweis: Map every named boundary to threat-model or source-review planning.
- [x] CHK006 Are threat-model requirements complete for assets, trust boundaries, STRIDE, CIA, CAPEC, mitigations, and residual risks? [Completeness, Spec FR-017, Contract §9]
  - Durchführungshinweis: Compare the planned threat-model fields with Constitution principles XIII and XVII.
- [x] CHK007 Is the S-ADR trigger limited to architecturally significant security decisions and otherwise explicit? [Clarity, Spec FR-018, Research R12]
  - Durchführungshinweis: Identify the decision threshold and ensure ordinary evidence updates do not fabricate ADRs.
- [x] CHK008 Are cryptography, ASVS, Zero Trust, BSI C3A, and BSI C5 `N/A` conditions factual and paired with triggers? [Completeness, Spec FR-023..FR-026]
  - Durchführungshinweis: Verify each non-applicability rationale names the missing system boundary and the event that changes it.

## Supply Chain and Disclosure

- [x] CHK009 Are direct/transitive vulnerability, deprecation, and package-change requirements distinguished? [Clarity, Spec FR-019, Contract §6]
  - Durchführungshinweis: Compare package review obligations with the no-unjustified-upgrade boundary.
- [x] CHK010 Is SBOM acceptance measurable without requiring generated output in Git? [Measurability, Spec FR-020/FR-034, SC-006]
  - Durchführungshinweis: Confirm tool restore, generation, parse, component count, and deletion/retention are all specified.
- [x] CHK011 Are VEX, SLSA, Scorecard, action pins, and update automation assigned evidence or bounded follow-up? [Completeness, Spec FR-021, Research R06..R07]
  - Durchführungshinweis: Reject any supply-chain topic that is mentioned but lacks a result owner or boundary.
- [x] CHK012 Is vulnerability disclosure specified without inventing an unapproved SLA, public contact, or provider state? [Clarity, Spec FR-027, Contract §7]
  - Durchführungshinweis: Check that discoverability and private reporting are required while organizational commitments remain bounded.

## AI, Privacy, and Regulation

- [x] CHK013 Is AI-SBOM `N/A` tied specifically to development-tool-only AI and a complete product-AI trigger? [Clarity, Spec FR-022]
  - Durchführungshinweis: Compare trigger elements with Constitution G7/BSI AI-SBOM clusters.
- [x] CHK014 Are NIS2, CRA, EU AI Act, DORA, and DPIA each explicit without a legal compliance claim? [Coverage, Spec FR-025, CR-013..CR-014]
  - Durchführungshinweis: Ensure each framework has factual status, owner where human-only, and re-evaluation event.
- [x] CHK015 Are provider settings, credentials, formal approval, and market placement clearly human-only? [Consistency, Spec FR-014, Assumptions]
  - Durchführungshinweis: Search all artifacts for autonomous verbs applied to those boundaries.

## Evidence Quality

- [x] CHK016 Are all accepted security-document stubs required to become current project evidence? [Completeness, Spec FR-011, SC-004]
  - Durchführungshinweis: Compare the current `docs/security/` inventory with the contract's required list.
- [x] CHK017 Are false-positive and unavailable-service cases given a reviewable evidence treatment? [Coverage, Spec Edge Cases]
  - Durchführungshinweis: Trace both cases to finding disposition, failure boundary, or temporary validation status.
- [x] CHK018 Are no secrets, generated scans, SBOMs, coverage, DocFX, caches, or logs planned for tracking? [Consistency, Spec FR-034, Plan Security-first]
  - Durchführungshinweis: Compare retention rules in spec, data model, contract, and quickstart.
