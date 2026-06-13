# Feature Specification: Didactic Inline Code Comment Hardening

**Feature Branch**: `015-didactic-comment-hardening`
**Created**: 2026-06-13
**Status**: Draft
**Input**: User description: "Use `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md` as the binding input. Create a feature specification for a didactic inline-code-comment hardening run after `014-wave1-functional-hardening` and before `Lastenheft_Wave1-Visual-Component-Remediation.md`. Central TuiVision framework flows and relevant smoke-test helpers must become easier for apprentices and maintainers to understand. XML comments remain the primary API and DocFX explanation surface; this feature adds code-near didactic comments only for non-trivial logic."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Understand central framework decisions (Priority: P1)

As an apprentice or new maintainer, I want short code-near explanations at
central framework decision points, so that I can understand why event,
command, focus, rendering, dialog, help, status, or fallback flows are shaped
as they are.

**Why this priority**: This is the core learning value. Without focused
comments, readers can see that a flow works but must reconstruct the reason
from many files, historical sources, and tests.

**Independent Test**: A reviewer can inspect the feature evidence, choose any
required hotspot category, and find a documented review decision plus a short
comment change where the code was not already self-explaining.

**Acceptance Scenarios**:

1. **Given** a reviewed framework flow contains non-trivial dispatch, focus,
   rendering, dialog, help, status, validation, rejection, or terminal fallback
   behavior, **When** the reviewer checks the code and evidence, **Then** the
   decision behind that behavior is either explained by a concise didactic
   comment or marked as already adequate.
2. **Given** a reviewed framework flow is self-explaining, **When** the
   reviewer checks the evidence, **Then** the evidence records
   `NoCommentNeeded` instead of adding noisy prose to obvious code.
3. **Given** a reviewed flow intentionally differs from historical
   Turbo Vision behavior, **When** the reviewer checks the code and evidence,
   **Then** the intentional deviation or follow-up boundary is explicit.

---

### User Story 2 - Understand smoke-test proof paths (Priority: P1)

As a maintainer, reviewer, or apprentice, I want smoke-test helpers and proof
paths to explain why their checks are stable and what their limits are, so
that app-loop, view-tree, buffer/cell, rendering, and fallback proof does not
look like hidden test magic.

**Why this priority**: Recent example waves rely on event-loop and rendered
visibility proof. Those paths are valuable only if future reviewers can see
why they are accepted and where they stop proving behavior.

**Independent Test**: A reviewer can inspect each reviewed smoke-test helper
area and determine the proof purpose, stability reason, and proof boundary
without reverse-engineering the helper implementation.

**Acceptance Scenarios**:

1. **Given** a reviewed smoke helper drives the application loop, events,
   commands, keys, focus, or dialog state, **When** the reviewer checks the
   helper, **Then** a concise comment explains the proof path when the intent
   is not obvious from the helper name and assertion shape.
2. **Given** a reviewed helper reads view-tree, buffer, cell, or rendering
   state, **When** the reviewer checks the helper, **Then** the proof boundary
   is documented where a future reader could otherwise overstate the result.
3. **Given** a helper is only setup or supplemental proof, **When** the
   reviewer checks the evidence, **Then** it is not presented as a complete
   behavior proof.

---

### User Story 3 - Keep comment noise under control (Priority: P2)

As a reviewer, I want a visible review model for comment decisions, so that
the feature improves learning value without turning the codebase into a
line-by-line prose duplicate.

**Why this priority**: The user explicitly rejects a global "comment every
method" pass. The feature succeeds only when important comments become easier
to find because trivial comments are avoided.

**Independent Test**: A reviewer can open the feature evidence and see one of
the approved decisions for every reviewed file or flow area:
`CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`,
`UpdateExistingComment`, or `FollowUpHardening`.

**Acceptance Scenarios**:

1. **Given** an existing comment is accurate and useful, **When** the area is
   reviewed, **Then** the evidence records `CommentAdequate` and the comment
   is left alone.
2. **Given** an existing comment is stale, misleading, or too vague, **When**
   the area is reviewed, **Then** the evidence records
   `UpdateExistingComment` and the comment is corrected or removed.
3. **Given** review finds a real framework or test-design problem, **When**
   the problem is outside this comment-hardening scope, **Then** the evidence
   records `FollowUpHardening` instead of changing runtime behavior.

---

### User Story 4 - Carry comment rules into future work (Priority: P3)

As a future contributor or agent-assisted maintainer, I want the project
guidance to preserve the same didactic-comment rule, so that new or changed
non-trivial logic follows the same moderate standard after this feature ends.

**Why this priority**: The feature is not only a one-time cleanup. Its value
depends on future changes keeping the same balance between useful explanation
and comment noise.

**Independent Test**: A reviewer can inspect the maintained guidance surfaces
and see that any changed shared rule is synchronized across the declared agent
surfaces, with no silent divergence.

**Acceptance Scenarios**:

1. **Given** this feature changes a project-wide comment rule, **When** the
   repository guidance is reviewed, **Then** all maintained agent surfaces
   contain the same shared rule or an explicit documented deviation.
2. **Given** only feature-local evidence is added, **When** the agent guidance
   is reviewed, **Then** the evidence explains why no shared guidance update
   was required.
3. **Given** future non-trivial logic is added or changed, **When** the
   contributor follows the guidance, **Then** the expected comment style is
   German-first, English-second, CEFR-B2, concise, and focused on why.

### Edge Cases

- A reviewed file already has a useful comment near the non-trivial flow.
- A reviewed file has no comment because the code is clear and extra prose
  would reduce readability.
- A reviewed comment repeats only names, operators, assignments, or obvious
  control flow.
- A reviewed comment is stale because later example hardening or framework
  work changed the proof path.
- A historical Turbo Vision deviation is important for understanding but does
  not require a runtime behavior change.
- A review uncovers a real framework defect, weak test design, or visual
  remediation need that belongs in a later feature.
- A comment change would require XML documentation, public API wording, or an
  API signature change to stay truthful.
- A generated, license, marker, or tool-owned line is encountered while
  reviewing a file.
- A terminal fallback or rendering proof depends on environment capability and
  cannot be explained only by the assertion name.
- A future reviewer tries to count this feature as a framework revision,
  example porting wave, or Wave-1 visual remediation.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST use
  `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md` as the binding
  input and MUST run after `014-wave1-functional-hardening` and before
  `Lastenheft_Wave1-Visual-Component-Remediation.md`.
- **FR-002**: The feature MUST be limited to didactic inline, block, file, or
  module comments, review evidence, and affected guidance surfaces.
- **FR-003**: The feature MUST NOT change runtime behavior, public behavior,
  API signatures, dependencies, example scope, ported functionality, or broad
  framework structure as part of comment hardening.
- **FR-004**: The feature MUST review central framework-flow areas in the
  maintained source modules when they carry learner value or maintenance risk:
  core geometry/event/buffer behavior, controls and shell behavior, managed
  console-driver behavior, serialization/resource behavior, and compatibility
  behavior.
- **FR-005**: The feature MUST review relevant smoke-test helper and proof
  areas, especially app-loop proof, event and command dispatch, focus
  transitions, status feedback, help or description reachability, dialog
  state, validation and rejection, view-tree proof, buffer/cell proof,
  rendering snapshots, and terminal fallbacks.
- **FR-006**: The feature MUST review historical Turbo Vision deviations when
  the modern code or proof path would otherwise be hard to understand.
- **FR-007**: The feature MUST maintain
  `specs/015-didactic-comment-hardening/pr-evidence.md` as the feature
  evidence surface.
- **FR-008**: Each evidence entry MUST identify the reviewed file or flow
  area, hotspot category, decision, rationale, comment need, change summary,
  validation or proof boundary, and follow-up boundary where applicable.
- **FR-009**: Each reviewed file or flow area MUST receive exactly one primary
  review decision from this set: `CommentAdequate`, `CommentNeeded`,
  `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
- **FR-010**: `CommentNeeded` decisions MUST result in concise didactic
  comments unless a later review within the same feature proves that existing
  wording or code shape already explains the reason.
- **FR-011**: `UpdateExistingComment` decisions MUST correct, replace, or
  remove stale, misleading, overly broad, or trivial comments in the reviewed
  area.
- **FR-012**: `NoCommentNeeded` decisions MUST be recorded when adding a
  comment would only repeat clear code, names, operators, assignments, or
  straightforward control flow.
- **FR-013**: `FollowUpHardening` decisions MUST record the real issue, why it
  is outside this feature, and which later work item or evidence boundary
  should carry it.
- **FR-014**: New or changed didactic comments MUST explain why a decision
  exists, which trade-off or constraint applies, which historical deviation is
  intentional, or which proof boundary matters.
- **FR-015**: New or changed didactic comments MUST NOT merely restate what
  the adjacent statement, identifier, operator, or assertion already says.
- **FR-016**: Normal file/module or non-trivial block comments SHOULD stay
  within 1 to 3 lines. Longer comments are acceptable only for complex flows,
  historical deviations, security or accessibility constraints, or test-proof
  boundaries that cannot be made clear in a shorter form.
- **FR-017**: Didactic explanation blocks MUST be German-first and
  English-second at approximately CEFR-B2 readability.
- **FR-018**: Technical license headers, generated-file markers, tool-owned
  markers, and similar non-didactic lines MUST remain unchanged unless a
  separate applicable rule requires the change.
- **FR-019**: If XML comments, public API signatures, generated API
  documentation, documentation navigation, or learner-facing guides are
  changed, the feature MUST use the normal documentation and accessibility
  proof path for those changed artefacts.
- **FR-020**: Pure `//` or `/* */` comment hardening that does not change XML
  comments, API signatures, generated documentation, navigation, or guides
  MUST NOT require DocFX regeneration as acceptance evidence.
- **FR-021**: The feature evidence MUST explicitly state that Wave-1 visual
  remediation, Wave-3/Wave-4 implementation, new example porting, and broad
  framework revision remain outside this feature.
- **FR-022**: The feature MUST preserve text-first accessibility for all
  changed evidence or guidance documents and MUST avoid meaning conveyed only
  through color, layout, or pointer-only affordances.
- **FR-023**: If project-wide comment guidance is clarified or changed, the
  feature MUST update the maintained agent guidance surfaces together:
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md`.
- **FR-024**: Any intentional divergence between maintained agent guidance
  surfaces MUST be explicit in the same feature evidence.
- **FR-025**: The feature MUST record validation evidence showing that comment
  changes did not reduce existing accepted behavior or proof coverage.

### Constitution Requirements *(mandatory)*

- **CR-001**: This feature targets the Level-2 project
  `RiderProjects/TuiVision`; the matching Level-2 Project Environment Registry
  row in `.specify/memory/constitution.md` is binding project context.
- **CR-002**: A11Y review applies to changed user-facing artefacts such as
  feature evidence, guides, generated documentation, and shared guidance.
  Pure code comments use the text-first review path; generated HTML
  documentation uses WCAG 2.2 Level AA proof when changed.
- **CR-003**: Learner-facing or didactic explanation content MUST be
  German-first and English-second at approximately CEFR-B2 readability unless
  an explicitly synchronized companion file is chosen.
- **CR-004**: The feature MUST state in evidence whether
  `docs/project-statistics.md` and shared AI-agent guidance were updated,
  unchanged by rationale, or deferred to implementation completion.
- **CR-005**: Security Governance v0.5.0 applies. The primary implementation
  language remains C# on .NET for TuiVision. C# is on the memory-safe-language
  allow-list, so no non-MSL justification is required; normal C#/.NET secure
  coding discipline remains applicable when comments touch non-trivial logic.
- **CR-006**: `NIST SSDF` and `CWE Top 25` apply as Level-2 secure-development
  review context. No new feature-specific security checklist is required for
  pure comment and evidence hardening unless implementation changes
  security-relevant logic, input handling, dependency state, distribution
  artefacts, or vulnerability-handling evidence.
- **CR-007**: `OWASP ASVS` verification scope is `N/A` for the planned feature
  because no web, API, HTTP, authentication, or authorization-bearing service
  is added or changed.
- **CR-008**: `SBOM`, `VEX`, `SLSA`, and `OpenSSF Scorecard` remain governed by
  the existing release, dependency, CI, and public-repository posture. This
  feature does not need new feature-specific supply-chain artefacts unless the
  plan later changes dependencies, release output, build provenance, or public
  OSS risk posture.
- **CR-009**: `AI-SBOM` is `N/A` because AI is used only as development or
  agent tooling. No runtime AI, model, dataset, inference infrastructure, AI
  service, or product AI component is delivered. If any of those enter scope,
  the AI-SBOM decision MUST be reopened and documented.
- **CR-010**: Regulatory screening for `NIS2`, `CRA`, `EU AI Act`, and `DORA`
  is `N/A` for this comment-only feature because it does not change market
  placement, customer handover, vulnerability-handling process, cloud
  operation, financial-sector ICT dependency, regulated customer flow, or
  runtime/product AI. If implementation changes any of those triggers, the
  plan MUST add regulatory applicability evidence.
- **CR-011**: Architecture Governance v0.4.0 applies as an applicability gate.
  Trust-boundary changes, data flows across trust boundaries, distributed
  service architecture, `STRIDE`/`CIA`/`CAPEC` threat-model entries, S-ADRs,
  arc42 security concepts, Zero Trust, SAMM, and iSAQB security quality
  scenarios are `N/A` because the feature changes comments, evidence, and
  guidance rather than runtime behavior, service boundaries, or deployment
  topology.
- **CR-012**: `BSI C3A` cloud autonomy and `BSI C5` cloud compliance assurance
  are `N/A` because this feature does not select, change, or operate cloud
  services, SaaS/PaaS/IaaS, managed services, container or artifact hosting,
  provider-dependent deployments, cloud assurance reviews, or related audit
  evidence.
- **CR-013**: A11Y Governance v0.3.0 applies. The affected artefacts are
  code-near didactic comments, feature evidence, checklists, and shared
  guidance. Didactic inline-code comments are required only where new or
  changed non-trivial logic affects learner understanding or maintainability.
  Generated HTML, DocFX output, or navigation evidence is required only if
  XML comments, public API documentation, guides, or generated documentation
  are changed.
- **CR-014**: Agent Parity Governance v0.2.0 applies when shared comment
  guidance changes. The maintained agent surfaces are `AGENTS.md`,
  `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md`; any intentional divergence MUST
  be recorded in feature evidence. Project templates under
  `.specify/templates/` are `N/A` for this feature unless the later plan
  explicitly changes repository-owned templates.
- **CR-015**: Cross-Platform Governance v0.1.0 script-specific requirements
  are `N/A` because this feature does not add, change, or remove
  script-shaped tools; therefore no Bash/Pwsh pair, man page, Cmdlet
  `Verb-Noun` name, `--dry-run`, or `-WhatIf` parity is planned.
- **CR-016**: All six installed governance presets apply by default:
  `security-governance` v0.5.0, `architecture-governance` v0.4.0,
  `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.3.0,
  `cross-platform-governance` v0.1.0, and `agent-parity-governance` v0.2.0.

### Key Entities *(include if feature involves data)*

- **Review Area**: A source file, test helper file, or named flow category
  inspected for didactic-comment adequacy. Key attributes include path or flow
  name, hotspot category, learner value, maintenance risk, and historical
  context where relevant.
- **Comment Decision**: The primary result for one review area. Allowed values
  are `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`,
  `UpdateExistingComment`, and `FollowUpHardening`.
- **Didactic Comment**: A concise code-near explanation that states why a
  decision, trade-off, constraint, historical deviation, or proof boundary
  exists. It is not a replacement for XML API documentation.
- **Feature Evidence Entry**: The review record for one area. It links the
  area, decision, rationale, changed or unchanged comment state, validation
  result, and follow-up boundary.
- **Follow-up Boundary**: A documented issue or improvement discovered during
  review that is real but outside this comment-hardening scope.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of required hotspot categories have at least one evidence
  entry or an explicit evidence rationale explaining why no current file or
  flow in that category required change.
- **SC-002**: 100% of reviewed files or flow areas have exactly one approved
  review decision and a short rationale in the feature evidence.
- **SC-003**: 100% of newly added or updated didactic comments explain why,
  trade-off, constraint, historical deviation, or proof boundary rather than
  repeating obvious code.
- **SC-004**: At least 90% of new or updated didactic comments stay within the
  normal 1 to 3 line target; every longer comment has an evidence rationale.
- **SC-005**: 100% of reviewed smoke-test helper areas with non-obvious proof
  paths document the proof purpose and boundary either in code-near comments
  or in feature evidence.
- **SC-006**: 0 accepted runtime behavior changes are introduced by this
  feature; validation evidence confirms existing accepted behavior remains
  intact.
- **SC-007**: A reviewer can determine within 5 minutes for any reviewed area
  whether the comment was adequate, needed, intentionally absent, updated, or
  deferred as follow-up.
- **SC-008**: All changed shared guidance surfaces are synchronized, or the
  feature evidence records an explicit intentional divergence.

## Assumptions

- `014-wave1-functional-hardening` is the current accepted baseline for this
  feature.
- The feature is a selective hardening pass, not a repository-wide comment
  quota.
- The planned evidence path is
  `specs/015-didactic-comment-hardening/pr-evidence.md`.
- XML API documentation and public API signatures are not expected to change.
  If planning or implementation proves otherwise, the normal DocFX and A11Y
  validation path becomes part of acceptance for those changes.
- AI assistance is development tooling only and is not delivered as runtime or
  product functionality.
- No new script-shaped tool is expected; cross-platform script parity remains
  inapplicable unless the later plan introduces scripts.
- The exact reviewed file list will be finalized during planning and task
  generation using the hotspot categories and evidence model in this
  specification.
