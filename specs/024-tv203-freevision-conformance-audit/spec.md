# Feature Specification: TV203 and Free Vision Conformance Audit

**Feature Branch**: `024-tv203-freevision-conformance-audit`
**Created**: 2026-07-12
**Status**: Draft
**Input**: Binding requirements from `Lastenheft_08_TV203-FreeVision-Conformance-Audit.md`

## User Scenarios & Testing

### User Story 1 - Framework contracts can be assessed consistently (Priority: P1)

As a TuiVision maintainer, I need every historical implementation area and
every current framework source area assigned to an explicit behavioral
contract so that I can distinguish strong alignment from unverified or
unintended drift before Wave 5 builds on the framework.

**Why this priority**: A complete and consistent contract inventory is the
foundation for every later comparison or remediation decision. Partial review
would allow high-impact event, focus, rendering, persistence, or input behavior
to remain invisible.

**Independent Test**: Verify that all historical ledger rows and all current
framework source files appear exactly once in the inventory and that each is
linked to at least one contract with one allowed primary decision.

**Acceptance Scenarios**:

1. **Given** the canonical historical ledger, **When** the audit inventory is
   evaluated, **Then** every one of its 151 implementation rows is assigned to
   exactly one primary domain and at least one contract.
2. **Given** the current production framework tree, **When** generated output
   is excluded, **Then** every maintained source file is assigned to exactly one
   primary domain and its public contracts are represented.
3. **Given** a reviewed contract, **When** its result is recorded, **Then** it
   has exactly one primary decision and concrete proof or a visible evidence
   gap.

---

### User Story 2 - Modernization remains explainable without losing historical intent (Priority: P1)

As a maintainer or apprentice, I need each meaningful difference between the
historical framework and TuiVision explained as deliberate modernization,
conscious omission, behavioral drift, or missing evidence so that modern C# and
.NET design does not become either an opaque rewrite or a mechanical copy.

**Why this priority**: TuiVision is intended to remain strongly aligned with
Turbo Vision while using managed memory, Unicode, safe rejection, accessibility,
and cross-platform .NET behavior. Those goals require explicit trade-offs.

**Independent Test**: Select one contract from each required domain and verify
that historical intent, observed current behavior, decision rationale, proof,
and residual risk can be understood without reading undocumented agent history.

**Acceptance Scenarios**:

1. **Given** a TuiVision behavior that differs from the original for a modern
   constraint, **When** it is reviewed, **Then** the rationale names the
   constraint and the proof protecting that behavior.
2. **Given** an unexplained observable difference, **When** it is reviewed,
   **Then** it is classified as behavioral drift or an evidence gap rather than
   silently accepted.
3. **Given** an intentionally omitted historical capability, **When** it is
   reviewed, **Then** its omission rationale, impact, and re-evaluation trigger
   are explicit.

---

### User Story 3 - Free Vision provides a bounded second opinion (Priority: P2)

As a reviewer, I need relevant Free Vision behavior compared at one immutable
official source revision so that Object Pascal can clarify or challenge an
interpretation without becoming a second normative source or introducing copied
third-party code.

**Why this priority**: Free Vision covers the same major framework domains and
can reveal alternate interpretations, but it has its own evolution, fixes,
platform assumptions, and provenance boundaries.

**Independent Test**: Verify that every audit domain has a concrete pinned Free
Vision comparison or a justified `NotApplicable`, and that no external source
file or translated implementation enters the repository.

**Acceptance Scenarios**:

1. **Given** the approved Free Vision revision, **When** a relevant domain is
   reviewed, **Then** the source path, revision, comparison statement, and one
   allowed relation are recorded.
2. **Given** Free Vision differs from Borland behavior, **When** the audit
   decides historical intent, **Then** Borland and `tv203s/` remain primary and
   the Free Vision difference is visible as its own relation.
3. **Given** no meaningful Free Vision equivalent exists, **When** the domain is
   closed, **Then** `NotApplicable` includes a concrete rationale.

---

### User Story 4 - Follow-up work is findings-driven and reviewable (Priority: P2)

As the project owner, I need every actionable audit result routed to one bounded
downstream disposition so that later autonomous runs implement confirmed work
instead of speculative framework revisions.

**Why this priority**: The audit must improve delivery confidence without
turning into a broad implementation feature or creating empty follow-up pull
requests.

**Independent Test**: Verify that every behavioral drift or evidence-gap
decision has exactly one stable finding, severity, owner, acceptance boundary,
and downstream disposition, while all other decisions create no finding.

**Acceptance Scenarios**:

1. **Given** an actionable core-runtime finding, **When** the audit closes,
   **Then** it is assigned to `Core025` with a reproducible acceptance boundary.
2. **Given** an actionable component or data finding, **When** the audit closes,
   **Then** it is assigned to `ComponentData026` with a reproducible acceptance
   boundary.
3. **Given** no accepted finding for one remediation feature, **When** follow-up
   intake is considered, **Then** no empty feature branch or pull request is
   created for that feature.
4. **Given** a potential breaking public-contract conflict, **When** it is
   identified, **Then** it is routed to `ProductDecision` and autonomous runtime
   modification stops.

### Edge Cases

- One historical implementation file supports several contracts. It still has
  one primary inventory domain while the matrix may reference it from several
  contracts without duplicate inventory ownership.
- One modern source file declares several public types. File ownership remains
  unique, while each public contract is inventoried separately.
- One modern type consolidates several historical platform implementations.
  Each historical ledger row remains accounted for and shares the modern target
  only with an explicit consolidation rationale.
- Existing tests prove a happy path but not ordering, rejection, recovery, or
  fallback semantics. The contract is not considered fully proved merely
  because a broad test project passes.
- Historical documentation and source appear inconsistent. The audit records
  the conflict and confidence boundary instead of selecting a convenient answer
  silently.
- The pinned Free Vision commit becomes unreachable. The audit stops source
  comparison until the exact revision is retrievable or an explicitly reviewed
  replacement pin is recorded.
- Free Vision contains its own bug fix or Unicode behavior absent from the
  original. The relation records that evolution without redefining historical
  intent.
- A finding spans both core and component domains. It receives one owning
  disposition and names dependent contracts instead of being duplicated.
- A review discovers a security, accessibility, or data-loss defect. The audit
  records severity and boundary but does not implement the defect inside 024.

## Requirements

### Required Audit Domains

1. Base types, collections, sorting, points, and rectangles
2. Event creation, queueing, commands, broadcasts, and dispatch order
3. View/group ownership, parent relationships, focus, and lifecycle
4. Local/global coordinates, clipping, growth, resizing, and exposure
5. Application, program, desktop, modality, and window stack
6. Menus, status line, shortcuts, command enablement, and Help/Description
7. Dialogs, controls, validation, rejection, and state preservation
8. Editor, clipboard, file, close, conflict, search, and replace flows
9. Help, references, compiler, resources, history, and localization
10. Streams, registries, object identity, cycles, malformed input, and versions
11. Draw buffer, console buffer, cells, palettes, cursor, and snapshots
12. Keyboard, mouse, double-click, drag, and terminal ingress
13. Charset, Unicode, fonts, terminal subset, and platform fallbacks
14. Compatibility layer and consciously omitted native platform paths
15. Accessibility text, focus events, structured shortcuts, and high contrast
16. Smoke, application-loop, view-tree, buffer/cell, and proof helpers

### Functional Requirements

- **FR-001**: The audit MUST treat the binding Lastenheft and the approved
  pre-Wave-5 ordering as fixed scope.
- **FR-002**: The audit MUST remain evidence-only and MUST NOT change runtime or
  public behavior.
- **FR-003**: The audit MUST NOT change public API signatures, dependencies,
  packages, examples, or historical sources.
- **FR-004**: The audit MUST preserve Borland documentation and `tv203s/` as the
  primary historical interpretation sources.
- **FR-005**: The audit MUST use the official Free Pascal source repository at
  commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` as the only approved Free
  Vision comparison snapshot unless an unavailable-revision stop condition is
  explicitly resolved.
- **FR-006**: The audit MUST keep Free Vision external and MUST NOT commit its
  files, substantial excerpts, or mechanically translated code.
- **FR-007**: The audit MUST record the external repository, immutable revision,
  retrieval date, reviewed paths, and any retrieval limitation.
- **FR-008**: The audit MUST account for all 151 historical `.cc` rows from the
  canonical porting ledger.
- **FR-009**: Every historical row MUST have exactly one primary audit domain
  and at least one contract reference.
- **FR-010**: Every consciously omitted historical row MUST have a reviewed
  rationale, impact statement, and re-evaluation trigger.
- **FR-011**: The audit MUST inventory every maintained production source file
  in the five framework modules while excluding generated build output.
- **FR-012**: Every maintained production source file MUST have exactly one
  primary audit domain.
- **FR-013**: Public framework contracts MUST be inventoried independently of
  physical file count so that files containing several declarations remain
  complete.
- **FR-014**: Every test or proof reference MUST identify a concrete test,
  filtered collection, evidence row, or explicit evidence gap; an unqualified
  project-directory reference is insufficient.
- **FR-015**: The audit MUST cover all 16 required domains listed in the binding
  Lastenheft.
- **FR-016**: Every contract MUST record historical intent, current observed
  behavior, modern rationale, proof boundary, risk, and follow-up disposition.
- **FR-017**: Every contract MUST receive exactly one primary decision from
  `Aligned`, `IntentionalModernization`, `BehavioralDrift`, `EvidenceGap`, or
  `ConsciouslyOmitted`.
- **FR-018**: Every contract MUST receive exactly one separate Free Vision
  relation from `CorroboratesOriginal`, `CorroboratesModernization`,
  `DivergesFromOriginal`, or `NotApplicable`.
- **FR-019**: Governance applicability terms MUST NOT be used as contract
  decisions or Free Vision relations.
- **FR-020**: Every audit domain MUST include a concrete Free Vision comparison
  or a justified `NotApplicable`.
- **FR-021**: Free Vision MUST NOT override a conflict with Borland or `tv203s`
  when determining historical intent.
- **FR-022**: Only `BehavioralDrift` and `EvidenceGap` decisions MUST create
  findings.
- **FR-023**: Every finding MUST have a stable identifier, contract reference,
  severity, reproduction or proof boundary, impact, owner, acceptance target,
  non-goals, and exactly one downstream disposition.
- **FR-024**: Finding severity MUST use `Critical`, `High`, `Medium`, or `Low`.
- **FR-025**: Finding disposition MUST use `Core025`, `ComponentData026`,
  `Closure027`, `AcceptedFollowUp`, or `ProductDecision`.
- **FR-026**: A `Critical` or `High` finding MUST block the pre-Wave-5 gate until
  resolved or explicitly converted into a human product decision.
- **FR-027**: A potential breaking public-contract conflict MUST be classified
  as `ProductDecision` and MUST stop autonomous behavioral modification.
- **FR-028**: The audit MUST produce the required inventory, conformance matrix,
  Free Vision source manifest, findings ledger, pre-Wave-5 gate, and run evidence
  artifacts.
- **FR-029**: The conformance data MUST remain machine-checkable even when split
  into domain-specific tables.
- **FR-030**: The audit MUST provide deterministic completeness and uniqueness
  evidence for historical rows, modern source files, contract decisions, Free
  Vision relations, and finding links.
- **FR-031**: The audit MUST NOT create speculative requirements for features
  025 or 026.
- **FR-032**: A 025 or 026 intake MUST be created only after 024 is final and only
  from a non-empty accepted finding set owned by that feature.
- **FR-033**: The audit MUST define feature 027 as a mandatory closure gate even
  when one or both remediation features are unnecessary.
- **FR-034**: The audit MUST record no-current-scope rationales for every
  conditional validation or governance checkpoint that is not triggered.
- **FR-035**: Any reusable autonomous-workflow observation MUST be classified
  separately from TuiVision framework findings.
- **FR-036**: An upstream issue update MUST occur only after a reusable preset
  improvement has been implemented, published, and independently revalidated.
- **FR-037**: The final diff MUST demonstrate that runtime, API, package,
  dependency, example, generated output, and historical-source scopes remain
  unchanged.

### Constitution Requirements

- **CR-001**: TuiVision is a registered Level-2 C#/.NET project and MUST use its
  repository Constitution, AGENTS guidance, and project evidence as binding
  context.
- **CR-002**: Learner-facing audit explanations MUST be text-first and must use
  German first and English second at CEFR-B2 readability.
- **CR-003**: Audit tables MUST remain understandable without color, layout-only
  meaning, screenshots, or pointer interaction.
- **CR-004**: `docs/project-statistics.md` MUST be updated at implementation
  completion; maintained agent files are updated together only if shared
  guidance changes.
- **CR-005**: No new implementation language is introduced. Existing C#/.NET is
  on the memory-safe-language allow-list; external Pascal and C/C++ are read-only
  evidence.
- **CR-006**: NIST SSDF and CWE Top 25 applicability MUST be recorded. Security
  evidence is proportional because the feature changes no executable behavior.
- **CR-007**: OWASP ASVS MUST be recorded as `N/A` unless the actual scope gains
  a web, API, HTTP, or authentication-bearing service.
- **CR-008**: SBOM, VEX, SLSA, OpenSSF Scorecard, supply-chain, and release
  provenance checkpoints MUST use trigger-based applicability; an evidence-only
  feature creates no new distributable artifact.
- **CR-009**: AI is development tooling only. AI-SBOM MUST be `N/A` unless a
  runtime model, AI service, dataset, inference infrastructure, or delivered AI
  component enters scope.
- **CR-010**: STRIDE, CIA, CAPEC, Zero Trust, S-ADR, arc42 security concepts,
  SAMM, BSI C3A, and BSI C5 MUST receive explicit applicability decisions; no
  runtime trust boundary, cloud service, deployment topology, or provider
  dependency is changed.
- **CR-011**: NIS2, CRA, EU AI Act, and DORA screening MUST be recorded with
  re-evaluation triggers where `N/A`.
- **CR-012**: The resolved preset matrix MUST distinguish the six base governance
  presets (`security-governance` v0.6.0, `architecture-governance` v0.5.0,
  `isaqb-architecture-governance` v0.2.0, `a11y-governance` v0.4.0,
  `cross-platform-governance` v0.2.0, and `agent-parity-governance` v0.3.0)
  from optional `autonomous-run-governance` v0.1.0 rather than rely on an
  ambiguous hard-coded count.
- **CR-013**: The feature MUST preserve permission boundaries, evidence-first
  execution, convergence, no-empty-PR behavior, and the distinction between
  project findings and portable preset follow-ups.

### Key Entities

- **AuditDomain**: One of the required framework responsibility areas. It owns
  historical rows and modern source files but may contain many contracts.
- **InventoryItem**: A historical ledger row, maintained modern source file, or
  public framework contract with unique ownership and traceable evidence.
- **FrameworkContract**: A reviewable behavioral responsibility with historical
  intent, current behavior, decision, secondary comparison, proof, and risk.
- **ExternalSourceRecord**: The immutable Free Vision repository revision,
  reviewed path, retrieval evidence, and provenance boundary.
- **AuditFinding**: An actionable behavioral drift or evidence gap with one
  severity and one downstream disposition.
- **PreWave5Gate**: The aggregate decision that blocks Wave 5 until inventory,
  findings, quality, and governance conditions are satisfied.
- **PresetObservation**: A provider-neutral autonomous-workflow learning item
  kept separate from framework conformance findings.

## Success Criteria

### Measurable Outcomes

- **SC-001**: 151 of 151 historical implementation rows have one primary domain
  and at least one framework-contract link, with zero duplicates or omissions.
- **SC-002**: 100% of maintained production framework source files and 100% of
  discovered public framework contracts are inventoried with unique ownership.
- **SC-003**: 100% of framework contracts have exactly one allowed primary
  decision and one allowed Free Vision relation.
- **SC-004**: All 16 required domains have Borland/`tv203s` evidence and a pinned
  Free Vision comparison or justified `NotApplicable`.
- **SC-005**: 100% of `BehavioralDrift` and `EvidenceGap` decisions map one-to-one
  to a complete finding, and no other decision creates a finding.
- **SC-006**: 100% of findings have one severity, one owner, one acceptance
  boundary, and one downstream disposition.
- **SC-007**: Zero external Free Vision source files, substantial excerpts, or
  mechanically translated implementations are tracked by TuiVision.
- **SC-008**: Zero runtime, public API, dependency, package, example, generated
  output, or historical source changes occur in feature 024.
- **SC-009**: No unresolved clarification, placeholder, TODO, or TBD marker
  remains in accepted feature artifacts.
- **SC-010**: All triggered local and remote quality gates pass, all unavailable
  reviews are reported honestly, and no actionable review thread remains.
- **SC-011**: Feature 025 and 026 intake creation is prevented for an empty
  finding set, while feature 027 always has a measurable closure contract.
- **SC-012**: A maintainer can trace any contract from historical source through
  current behavior, proof, decision, finding, and downstream boundary using the
  delivered evidence without undocumented session context.

## Assumptions

- The canonical M-07 ledger remains structurally complete at 151 historical
  `.cc` rows when the audit starts; any count drift is treated as an inventory
  conflict, not silently normalized.
- The production framework baseline consists of the five existing modules and
  excludes generated build output.
- The pinned Free Vision commit remains retrievable from the official Free
  Pascal repository throughout the audit.
- Existing accepted TuiVision public behavior is not automatically invalidated
  by historical differences.
- Behavioral contracts may group several files when one responsibility is
  genuinely shared, but inventory ownership remains unique.
- Existing test execution may be used to observe current behavior without
  changing tests or runtime code.
- A finding count of zero is acceptable if completeness, source comparison, and
  proof quality are fully demonstrated.
- Remote delivery uses `MergeAndSync` under the approved campaign authority and
  the repository's previously approved narrow human-approval bypass policy.

## Scope Boundaries

### In Scope

- Complete historical, modern, public-contract, test, and evidence inventory
- Contract-level comparison and decisions across all required domains
- Pinned external Free Vision comparison and provenance manifest
- Deterministic completeness validation
- Findings and pre-Wave-5 gate evidence
- Findings-driven downstream intake boundaries
- Autonomous retrospective and portable preset observation classification

### Out of Scope

- Runtime or public behavior changes
- Public API signature changes
- Dependency, package, or toolchain additions
- New or revised example applications
- Wave-5 porting or Wave-1-to-4 visual remediation
- Broad framework restructuring
- Editing `tv203s/`, `TVDEMOS/`, `TVFM/`, or external Free Vision sources
- Vendoring or translating third-party implementation code
- Implementing findings inside feature 024
- Creating empty 025/026 branches or pull requests
- Updating the upstream issue without a published and validated preset change

### Decision and Follow-up Model

- Primary contract decisions are exactly `Aligned`,
  `IntentionalModernization`, `BehavioralDrift`, `EvidenceGap`, and
  `ConsciouslyOmitted`.
- Free Vision relations are exactly `CorroboratesOriginal`,
  `CorroboratesModernization`, `DivergesFromOriginal`, and `NotApplicable`.
- Finding severities are exactly `Critical`, `High`, `Medium`, and `Low`.
- Finding dispositions are exactly `Core025`, `ComponentData026`, `Closure027`,
  `AcceptedFollowUp`, and `ProductDecision`.
- Governance applicability remains exactly `Applicable`, `N/A`, or `Open` and
  is never substituted for a contract decision.
