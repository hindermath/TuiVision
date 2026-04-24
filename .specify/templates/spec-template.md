# Feature Specification: [FEATURE NAME]

**Feature Branch**: `[###-feature-name]`  
**Created**: [DATE]  
**Status**: Draft  
**Input**: User description: "$ARGUMENTS"

## User Scenarios & Testing *(mandatory)*

<!--
  IMPORTANT: User stories should be PRIORITIZED as user journeys ordered by importance.
  Each user story/journey must be INDEPENDENTLY TESTABLE - meaning if you implement just ONE of them,
  you should still have a viable MVP (Minimum Viable Product) that delivers value.
  
  Assign priorities (P1, P2, P3, etc.) to each story, where P1 is the most critical.
  Think of each story as a standalone slice of functionality that can be:
  - Developed independently
  - Tested independently
  - Deployed independently
  - Demonstrated to users independently
-->

### User Story 1 - [Brief Title] (Priority: P1)

[Describe this user journey in plain language]

**Why this priority**: [Explain the value and why it has this priority level]

**Independent Test**: [Describe how this can be tested independently - e.g., "Can be fully tested by [specific action] and delivers [specific value]"]

**Acceptance Scenarios**:

1. **Given** [initial state], **When** [action], **Then** [expected outcome]
2. **Given** [initial state], **When** [action], **Then** [expected outcome]

---

### User Story 2 - [Brief Title] (Priority: P2)

[Describe this user journey in plain language]

**Why this priority**: [Explain the value and why it has this priority level]

**Independent Test**: [Describe how this can be tested independently]

**Acceptance Scenarios**:

1. **Given** [initial state], **When** [action], **Then** [expected outcome]

---

### User Story 3 - [Brief Title] (Priority: P3)

[Describe this user journey in plain language]

**Why this priority**: [Explain the value and why it has this priority level]

**Independent Test**: [Describe how this can be tested independently]

**Acceptance Scenarios**:

1. **Given** [initial state], **When** [action], **Then** [expected outcome]

---

[Add more user stories as needed, each with an assigned priority]

### Edge Cases

<!--
  ACTION REQUIRED: The content in this section represents placeholders.
  Fill them out with the right edge cases.
-->

- What happens when [boundary condition]?
- How does system handle [error scenario]?

## Requirements *(mandatory)*

<!--
  ACTION REQUIRED: The content in this section represents placeholders.
  Fill them out with the right functional requirements.
-->

### Functional Requirements

- **FR-001**: System MUST [specific capability, e.g., "allow users to create accounts"]
- **FR-002**: System MUST [specific capability, e.g., "validate email addresses"]  
- **FR-003**: Users MUST be able to [key interaction, e.g., "reset their password"]
- **FR-004**: System MUST [data requirement, e.g., "persist user preferences"]
- **FR-005**: System MUST [behavior, e.g., "log all security events"]

*Example of marking unclear requirements:*

- **FR-006**: System MUST authenticate users via [NEEDS CLARIFICATION: auth method not specified - email/password, SSO, OAuth?]
- **FR-007**: System MUST retain user data for [NEEDS CLARIFICATION: retention period not specified]

### Constitution Requirements *(mandatory)*

- **CR-001**: If this feature targets a listed Level-2 project, the feature MUST
  use the matching Level-2 Project Environment Registry entry from
  `constitution.md` as binding project context.
- **CR-002**: User-facing artefacts MUST identify their A11Y review path
  (WCAG 2.2 Level AA where applicable, text-first fallback otherwise).
- **CR-003**: Learner-facing or shared guidance content MUST be DE-first,
  EN-second unless a synchronized `.EN.md` companion is explicitly chosen.
- **CR-004**: The feature MUST state whether statistics and AI-agent guidance
  files require synchronized updates.
- **CR-005**: The feature MUST name its primary implementation language and
  either confirm it is on the MSL allow-list (`constitution.md`, Principle XI)
  or cite the documented non-MSL justification from the Level-2
  `constitution.md`.
- **CR-006**: The feature MUST determine the applicable security standards from
  `constitution.md`, Principles XIV-XVIII, and mark non-applicable standards
  as `N/A` with justification. `NIST SSDF` and `CWE Top 25` are mandatory for
  all Level-2 work.
- **CR-007**: If the feature includes web/API/HTTP/auth-bearing services, it
  MUST declare the selected `OWASP ASVS` level and verification scope.
- **CR-008**: If the feature creates releasable or distributable artefacts, it
  MUST declare the intended `SBOM` / `VEX` evidence path and any required
  provenance / `SLSA` considerations.
- **CR-009**: If the feature changes trust boundaries, externally reachable
  flows, or distributed/service architecture, it MUST state how `CAPEC` and
  `Zero Trust` applicability will be handled.
- **CR-010**: The feature MUST state whether it uses the default evidence files
  in `docs/security/` (`asvs-verification.md`, `supply-chain-evidence.md`,
  `zero-trust-applicability.md`, `samm-assessment.md`) or an explicitly
  justified equivalent governance location.

### Key Entities *(include if feature involves data)*

- **[Entity 1]**: [What it represents, key attributes without implementation]
- **[Entity 2]**: [What it represents, relationships to other entities]

## Success Criteria *(mandatory)*

<!--
  ACTION REQUIRED: Define measurable success criteria.
  These must be technology-agnostic and measurable.
-->

### Measurable Outcomes

- **SC-001**: [Measurable metric, e.g., "Users can complete account creation in under 2 minutes"]
- **SC-002**: [Measurable metric, e.g., "System handles 1000 concurrent users without degradation"]
- **SC-003**: [User satisfaction metric, e.g., "90% of users successfully complete primary task on first attempt"]
- **SC-004**: [Business metric, e.g., "Reduce support tickets related to [X] by 50%"]

## Assumptions

<!--
  ACTION REQUIRED: The content in this section represents placeholders.
  Fill them out with the right assumptions based on reasonable defaults
  chosen when the feature description did not specify certain details.
-->

- [Assumption about target users, e.g., "Users have stable internet connectivity"]
- [Assumption about scope boundaries, e.g., "Mobile support is out of scope for v1"]
- [Assumption about data/environment, e.g., "Existing authentication system will be reused"]
- [Dependency on existing system/service, e.g., "Requires access to the existing user profile API"]
