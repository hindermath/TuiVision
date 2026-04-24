# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/plan-template.md` for the execution workflow.

## Summary

[Extract from feature spec: primary requirement + technical approach from research]

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: [e.g., Python 3.11, Swift 5.9, Rust 1.75 or NEEDS CLARIFICATION]  
**Primary Dependencies**: [e.g., FastAPI, UIKit, LLVM or NEEDS CLARIFICATION]  
**Storage**: [if applicable, e.g., PostgreSQL, CoreData, files or N/A]  
**Testing**: [e.g., pytest, XCTest, cargo test or NEEDS CLARIFICATION]  
**Target Platform**: [e.g., Linux server, iOS 15+, WASM or NEEDS CLARIFICATION]
**Project Type**: [e.g., library/cli/web-service/mobile-app/compiler/desktop-app or NEEDS CLARIFICATION]  
**Performance Goals**: [domain-specific, e.g., 1000 req/s, 10k lines/sec, 60 fps or NEEDS CLARIFICATION]  
**Constraints**: [domain-specific, e.g., <200ms p95, <100MB memory, offline-capable or NEEDS CLARIFICATION]  
**Scale/Scope**: [domain-specific, e.g., 10k users, 1M LOC, 50 screens or NEEDS CLARIFICATION]

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

[Gates determined based on constitution file]

- **Level-2 environment**: If this plan targets a listed Level-2 project, it
  MUST cite the matching row from the Level-2 Project Environment Registry in
  `constitution.md` and use its runtime, build/test, docs/A11Y, statistics, and
  agent-surface baselines.
- **Memory-safe languages (MSL)**: State the primary implementation language
  and confirm it is on the MSL allow-list in `constitution.md`, Principle XI.
  If the primary language is not an MSL (e.g. C, C++, Assembly, `cc65`), cite
  the justification recorded in the Level-2 `constitution.md`.
- **Secure code generation**: Confirm that AI-generated code will follow the
  language-specific secure-coding rules in `constitution.md`, Principle XII
  (OWASP Top 10 avoidance, parameterised queries, output encoding, quoted
  variables, current cryptographic algorithms, no internal state exposure).
- **Secure software architecture**: Confirm the architecture follows the
  secure-architecture principles in `constitution.md`, Principle XIII (trust
  boundaries, defense in depth, least privilege, fail-safe defaults, attack
  surface reduction, separation of concerns, secure configuration, supply-chain
  security). State how trust boundaries and layered security apply.
- **Security documentation**: Identify which mandatory security documents apply
  (threat model, S-ADRs, arc42 Section 8 security concepts, security checklist,
  dependency audit, security quality scenarios). State whether `docs/security/`
  needs new or updated documents. Templates: `.specify/templates/`.
- **Security standards applicability**: Determine which standards from
  `constitution.md`, Principles XIV-XVIII apply. `NIST SSDF` and `CWE Top 25`
  always apply to Level-2; add `OWASP ASVS`, `SBOM`, `VEX`, `SLSA`, `CAPEC`,
  `NIST Zero Trust`, `OWASP SAMM`, `OWASP Cheat Sheet Series` /
  `OWASP Proactive Controls`, and `OpenSSF Scorecard` where relevant. Mark
  non-applicable standards as `N/A` with justification.
- **Release / supply-chain evidence**: State whether the feature requires
  ASVS verification notes, SBOM/VEX artefacts, provenance/SLSA evidence,
  CAPEC references, Zero-Trust applicability notes, or SAMM follow-up items,
  and where that evidence will live.
- **Security-first**: Confirm no credential files, agent state, logs, history,
  or SQLite state are planned for tracking.
- **Inclusion/A11Y**: Identify affected user-facing artefacts and the WCAG 2.2
  Level AA or text-first review path.
- **Bilingual delivery**: State how DE-first/EN-second requirements apply.
- **Statistics**: State whether `docs/project-statistics.md` needs an update
  and which manual/Thorsten-Solo baseline applies.
- **Agent guidance parity**: State whether `AGENTS.md`, `CLAUDE.md`,
  `GEMINI.md`, and `.github/copilot-instructions.md` are affected together.

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
