# Implementation Plan: Didactic Inline Code Comment Hardening

**Branch**: `015-didactic-comment-hardening` | **Date**: 2026-06-14 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `./spec.md`

**Note**: This document follows the Spec-Kit plan template and records the implementation baseline for `/speckit-plan`. It stops before task generation.

## Summary

Run a selective didactic inline-code-comment hardening pass across central TuiVision framework flows and relevant smoke-test helper/proof paths. The implementation will review the required hotspot categories, decide whether code-near explanation is useful, update only non-trivial `//` or `/* */` comments where needed, and maintain `specs/015-didactic-comment-hardening/pr-evidence.md` as the primary review and acceptance surface.

The feature is explicitly not a runtime hardening, API, framework revision, visual remediation, or example-porting feature. XML comments remain the primary API and DocFX explanation surface. Pure inline/block comment hardening does not trigger DocFX; XML/API/docs/navigation or learner-facing guide changes do.

The plan applies the current local preset matrix: `security-governance` v0.5.0, `architecture-governance` v0.4.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.3.0, `cross-platform-governance` v0.1.0, and `agent-parity-governance` v0.2.0.

## Baseline Assumptions

- `014-wave1-functional-hardening` is the accepted baseline; this feature runs before `Lastenheft_Wave1-Visual-Component-Remediation.md`.
- Comment updates must be moderate: normally 1 to 3 lines for file/module or non-trivial block explanation.
- New or updated didactic comments explain why, trade-off, constraint, historical deviation, or proof boundary; they do not restate obvious code.
- German-first/English-second CEFR-B2 applies to didactic explanation blocks. Technical license, generator, marker, and tool-owned lines remain unchanged.
- `pr-evidence.md` is the binding feature evidence file. It records reviewed areas, hotspot category, decision, rationale, comment need, changed/unchanged comment state, change summary, validation/proof boundary, and follow-up boundary.
- The exact file inventory is finalized during tasks and implementation, but hotspot category coverage is already fixed by the spec.
- No generated `_site/`, `api/*.yml`, test output, local caches, logs, credentials, agent state, or history are planned for tracking.

## Terminology

**Review Area**: A source file, test helper file, or named flow area reviewed for didactic-comment value.

**Hotspot Category**: A required review category from the spec, such as event/command dispatch, focus transitions, view hierarchy, status/help paths, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallback, historical deviation, or smoke helper proof.

**Comment Decision**: Exactly one primary evidence decision for a review area: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.

**Didactic Comment**: A concise code-near explanation of why a decision, trade-off, constraint, historical deviation, or proof boundary exists. It does not replace XML API documentation.

**Proof Boundary**: The point where a test helper, rendered snapshot, terminal fallback, or evidence-only rationale stops proving behavior and must not be overstated.

## Technical Context

**Language/Version**: C# `latest` / C# 14 on .NET 10 (`net10.0`)

**Primary Dependencies**: Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned.

**Storage**: Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned.

**Testing**: Planning artifacts are validated with placeholder scans, consistency checks, and `git diff --check`. Later implementation must run proportional validation for no-runtime-change comment hardening: targeted tests for touched modules or smoke helpers, full `dotnet test --configuration Release` when shared behavior or broad test helpers are touched, coverage gate when code/test files are changed, `dotnet format --verify-no-changes`, and conditional DocFX plus `tests/web-a11y` when XML/API/generated documentation/navigation/guides change. Before any build/test command, commit, or push on the numbered branch, `Directory.Build.props` must be aligned to branch version `1.15.<patch>.<build>`; the manual build counter is incremented only before build/test commands.

**Target Platform**: TuiVision terminal framework and tests on the primary Multi-Mac workflow, with Linux and Windows/WSL compatibility considered where comments touch terminal fallback, driver behavior, scripts, or portability evidence. Pure comment-only changes do not create new runtime platform obligations.

**Project Type**: Multi-project .NET terminal UI framework and Turbo Vision port with source modules under `src/`, tests under `tests/`, examples under `examples/`, historical references under `tv203s/`, and source-controlled Spec-Kit/evidence/governance artifacts.

**Performance Goals**: No runtime performance goals are introduced. Review and validation should avoid slow or flaky proof paths; test-helper comments should make boundedness and determinism clear when relevant.

**Constraints**: No runtime behavior change, public behavior change, API signature change, dependency change, broad framework restructuring, new example porting, or Wave-1 visual remediation is planned. Historical `tv203s/` files remain read-only references.

**Scale/Scope**: Central framework-flow categories across `src/TuiVision.Core`, `src/TuiVision.Controls`, `src/TuiVision.Drivers.Console`, `src/TuiVision.Serialization`, and `src/TuiVision.Compatibility`; relevant smoke-test helper/proof paths under `tests/`; optional affected guides/evidence/agent guidance only when triggered.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Core Constitutional Gates

- **Level-2 environment**: PASS. The feature targets `RiderProjects/TuiVision`, the Level-2 `.NET 10 / C# terminal UI framework and Turbo Vision port` listed in `.specify/memory/constitution.md`.
- **Memory-safe languages (MSL)**: PASS. C# is on the MSL allow-list. No non-MSL implementation work is planned.
- **Secure code generation**: PASS. The feature may touch comments near non-trivial logic, so existing C#/.NET secure-coding discipline remains applicable. No code generation, input-handling, cryptography, query, or output-encoding implementation change is planned.
- **Secure software architecture**: PASS. No trust boundary, authentication, authorization, data flow, persistence, service boundary, deployment topology, or runtime surface changes.
- **Security documentation**: PASS. `docs/security/` is not expected to change. `pr-evidence.md` records NIST SSDF/CWE context and trigger-based `N/A` decisions unless implementation changes security-relevant logic, dependency state, vulnerability handling, distribution artifacts, or release evidence.
- **Security standards applicability**: PASS. `NIST SSDF` and `CWE Top 25` apply as Level-2 secure-development context. `OWASP ASVS` is `N/A` because no web/API/HTTP/auth service changes. `SBOM`, `VEX`, `SLSA`, and `OpenSSF Scorecard` remain on the existing release, dependency, CI, and repository posture unless a dependency, release artifact, provenance, or public OSS risk posture changes. `AI-SBOM` is `N/A` while AI remains development tooling only.
- **Regulatory screening**: PASS. `NIS2`, `CRA`, `EU AI Act`, and `DORA` are `N/A` for this comment-only feature because it does not change market placement, customer handover, vulnerability process, cloud operation, financial-sector ICT dependency, regulated customer flow, or runtime/product AI.
- **Architecture governance**: PASS. STRIDE/CIA/CAPEC, S-ADR, arc42 Section 8, Zero Trust, SAMM, BSI C3A, and BSI C5 are `N/A` for new feature evidence because no cloud service, provider dependency, distributed service flow, deployment topology, trust boundary, or architectural structure changes. Existing architecture/security documents remain referenced but not updated unless implementation discovers a genuine architecture issue recorded as `FollowUpHardening`.
- **Release / supply-chain evidence**: PASS. No new dependency or release artifact is planned. Supply-chain evidence remains unchanged unless implementation changes dependency or release posture.
- **Default evidence files**: PASS. Feature-specific review proof lives in `specs/015-didactic-comment-hardening/pr-evidence.md`; default `docs/security/` files remain the governance home if a trigger changes security evidence.
- **Spec-Kit presets**: PASS. All six installed governance presets apply with the versions named in the spec.
- **Security-first**: PASS. No credentials, logs, histories, local caches, SQLite agent state, generated DocFX output, or transient validation artifacts are planned for tracking.
- **Inclusion/A11Y**: PASS. Changed evidence and guidance must remain text-first and usable in screen-reader, Braille, and text-browser contexts. WCAG 2.2 AA DocFX proof applies only when generated HTML documentation or navigation changes.
- **Bilingual delivery**: PASS. Didactic comment blocks and learner-facing updates are German-first/English-second and approximately CEFR-B2. Feature-internal evidence may remain concise but must preserve the bilingual rule where it documents learner-facing explanation style.
- **Statistics**: PASS. `docs/project-statistics.md` must be updated after implementation completion and may be updated during planning if repository practice records the planning phase separately. The baseline is manual 80 lines/workday and C#/.NET Thorsten-solo 125 lines/workday.
- **Agent guidance parity**: PASS. If this feature changes project-wide comment guidance, update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and `.github/agents/copilot-instructions.md` together and record any intentional divergence. `.specify/templates/` are `N/A` unless the later plan or implementation explicitly changes repository-owned templates.
- **Cross-platform governance**: PASS. No script-shaped tool is added or changed. Bash/PowerShell parity, man pages, Cmdlet naming, `--dry-run`, and `-WhatIf` requirements are `N/A`.

### Level-2 Environment Registry

- **Registry Row**: `RiderProjects/TuiVision` - .NET 10 / C# terminal UI framework and Turbo Vision port.
- **Runtime Baseline**: Existing framework libraries, controls, managed console driver, compatibility, serialization, examples, and smoke-test infrastructure.
- **Build/Test Baseline**: `dotnet restore`, Release build/test, MSTest suites, Coverlet coverage gate for core assemblies, `dotnet format`, and conditional DocFX plus Playwright/axe web A11Y validation.
- **Docs/A11Y Baseline**: DocFX regeneration requires matching web-a11y smoke; Markdown evidence and guidance remain text-first.
- **Statistics Baseline**: Experienced developer baseline 80 lines/workday; Thorsten-solo C#/.NET baseline 125 lines/workday.
- **Agent Surfaces**: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`, and Spec-Kit surfaces.

### Post-Design Gate Review

PASS. Phase 0 and Phase 1 artifacts keep 015 within the constitution: no unresolved clarification remains, no runtime/API/dependency change is planned, no trust boundary or cloud/provider dependency changes, no generated output is tracked, AI-SBOM and regulatory scopes are explicitly `N/A`, DocFX/A11Y triggers are conditional, and the comment-review evidence model is bounded and testable.

## Project Structure

### Documentation (this feature)

```text
specs/015-didactic-comment-hardening/
|-- spec.md
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
|-- pr-evidence.md          # later implementation/PR evidence, created when implementation starts
|-- checklists/
|   |-- requirements.md
|   |-- plan-quality.md
|   `-- plan-review.md
|-- contracts/
|   `-- didactic-comment-hardening-acceptance.md
`-- tasks.md                 # later /speckit-tasks output, not created by /speckit-plan
```

### Source Code (repository root)

```text
src/
|-- TuiVision.Core/
|-- TuiVision.Controls/
|-- TuiVision.Drivers.Console/
|-- TuiVision.Serialization/
`-- TuiVision.Compatibility/

tests/
|-- TuiVision.Core.Tests/
|-- TuiVision.Controls.Tests/
|-- TuiVision.Drivers.Tests/
|-- TuiVision.Serialization.Tests/
|-- TuiVision.Compatibility.Tests/
`-- TuiVision.Examples.SmokeTests/

docs/
|-- architecture/
|-- security/
|-- guides/
`-- project-statistics.md

tv203s/                         # read-only historical reference

AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
Directory.Build.props
```

**Structure Decision**: Keep all review and proof work inside the existing source, test, documentation, and agent-guidance surfaces. Do not create a new runtime package, service, script tool, dependency, or broad framework abstraction.

## Phase 0: Research

Research is captured in [research.md](./research.md). It resolves the selective review strategy, hotspot coverage, evidence model, comment-decision semantics, style/intensity rules, smoke-helper proof-boundary treatment, DocFX/A11Y trigger boundaries, agent-guidance parity, governance applicability, and validation scope.

## Phase 1: Design and Contracts

Evidence and review entities are captured in [data-model.md](./data-model.md). Acceptance obligations are captured in [contracts/didactic-comment-hardening-acceptance.md](./contracts/didactic-comment-hardening-acceptance.md). Local execution and validation entry points are captured in [quickstart.md](./quickstart.md).

## Phase 2: Task Planning Approach

The later `/speckit-tasks` run should produce tasks in this order:

1. Create or update `specs/015-didactic-comment-hardening/pr-evidence.md` with required columns for review area, hotspot category, decision, rationale, comment need, changed/unchanged comment state, change summary, proof/validation boundary, and follow-up boundary.
2. Build the initial review inventory across the required hotspot categories, mapping candidate files or named flow areas in `src/` and relevant smoke-test helper areas in `tests/`.
3. Review historical Turbo Vision deviations only where they explain a modern code path or proof boundary; keep `tv203s/` read-only.
4. Review central framework flows first: event/command dispatch, focus transitions, view hierarchy, status/help/description paths, dialog state, validation/rejection, serialization/resource behavior, console-driver fallback, and compatibility boundaries.
5. Review smoke-test helper and proof areas: app-loop proof, command/event/key driving, view-tree checks, buffer/cell proof, rendered snapshots, terminal fallback, setup-only helpers, supplemental helpers, and proof-boundary wording.
6. For each reviewed area, record exactly one decision from `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
7. Apply only the needed comment changes. Keep comments concise, German-first/English-second for didactic explanation blocks, and focused on why/trade-off/constraint/history/proof boundary.
8. Record `CommentAdequate` for useful existing comments and `NoCommentNeeded` for self-explaining areas instead of adding noise. Record `FollowUpHardening` for real framework/test/design issues outside this feature without changing runtime behavior.
9. Review shared guidance surfaces if project-wide comment guidance changes; update all maintained agent files together or record unchanged/intentional divergence rationale in `pr-evidence.md`.
10. Record governance, DocFX/A11Y, architecture/security, statistics, and validation evidence with explicit trigger-based `N/A` decisions where applicable.
11. Serialize tasks that edit shared evidence or shared agent guidance, especially `pr-evidence.md` and the maintained agent guidance files, so later task execution does not create conflicting parallel edits.
12. Run final validation scaled to touched files: `git diff --check`, targeted tests for changed source/test-helper files, full Release tests and coverage gate if shared code/test proof is touched broadly, `dotnet format --verify-no-changes`, and conditional `docfx docfx.json` plus `tests/web-a11y` when XML/API/generated docs/navigation/guides changed.

## Complexity Tracking

No constitution violations are introduced by this plan.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|--------------------------------------|
| None | N/A | N/A |
