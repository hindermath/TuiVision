# Implementation Plan: Wave 1 Functional Hardening

**Branch**: `014-wave1-functional-hardening` | **Date**: 2026-05-31 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `./spec.md`

**Note**: This document follows the Spec-Kit plan template and records the implementation baseline for `/speckit-plan`. It stops before task generation.

## Summary

Harden the already delivered Wave-1 examples against their historical Turbo Vision sources without starting the later Wave-1 visual remediation. The implementation will review `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode` against read-only sources under `tv203s/contrib/tvision/examples/`, then create a primary feature evidence matrix in `specs/014-wave1-functional-hardening/pr-evidence.md`.

The plan strengthens acceptance proof where startup checks, static text presence, or direct helper paths prove too much. Managed runtime behavior must receive executable smoke proof. Evidence-only proof is allowed only when the historical point is didactic, historical-only, or otherwise has no direct managed runtime target, and the proof boundary is explicitly recorded. Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions.

The feature stays narrow: no Wave 2, Wave 3, Wave 4, broad framework redesign, visual-remediation finish, new runtime dependency, database, network dependency, external service, mouse-only operation, runtime/product AI, or arbitrary user-file proof is planned.

## Baseline Assumptions

- The existing Wave-1 delivery remains accepted; this feature improves functional proof quality and traceability.
- Historical files under `tv203s/` are read-only intent references and are never modified.
- `specs/014-wave1-functional-hardening/pr-evidence.md` is the primary proof matrix for historical source, C# behavior, smoke proof, helper classification, missing-core decisions, negative/fallback proof, and intentional deviations.
- Existing guides and `examples/README.md` remain learner-facing summaries. They are updated only when runtime behavior, usage path, visible output, historical deviation, or learner-facing proof explanation changes.
- `Lastenheft_Wave1-Visual-Component-Remediation.md` is follow-up context only. The current Pflichtenheft next-step marker for Wave 3 is not completed or replaced by this feature.
- No generated DocFX output, generated `api/*.yml`, local test output, agent state, credential material, or local history is planned for tracking.

## Terminology

**Wave-1 Functional Hardening**: A quality pass that strengthens historical intent, functional smoke proof, helper classification, and learner traceability for delivered Wave-1 examples.

**Primary Proof Matrix**: The feature-local evidence file `specs/014-wave1-functional-hardening/pr-evidence.md`, used as the authoritative acceptance surface for historical proof and deviations.

**Executable Smoke Proof**: A deterministic smoke test that exercises managed runtime behavior through public example or application logic and makes concrete assertions about the relevant state or result.

**Evidence-Only Proof**: A documented proof record used only when no direct managed runtime target exists; it must name the proof boundary and the intentional deviation or omission.

**Helper Classification**: One of `SetupOnly`, `PrimaryProof`, `SupplementalProof`, or `LegacyOrTemporary`, assigned to every helper, headless, or direct proof path used by Wave-1 smokes.

**Missing Core Function Decision**: A primary evidence decision for a historical core function that is not fully present in the managed example, either implemented and smoke-proven in this feature or documented as an intentional deviation or follow-up.

## Technical Context

**Language/Version**: C# `latest` / C# 14 on .NET 10 (`net10.0`)

**Primary Dependencies**: Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned.

**Storage**: Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage is planned.

**Testing**: Primary implementation validation should include targeted `tests/TuiVision.Examples.SmokeTests/` coverage for `Desklogo`, `MsgCls`, `Tutorial`, and `Videomode`; full Release build and tests; Coverlet coverage gate via `coverlet.runsettings`; `dotnet format --verify-no-changes`; and DocFX plus `tests/web-a11y` only when generated documentation output or navigation is affected. `Directory.Build.props` must be aligned to branch version `1.14.<patch>.<build>` and the manual build counter must be incremented before build/test commands.

**Target Platform**: TuiVision terminal example applications on the primary Multi-Mac workflow (`MacBook Air M2` and `Mac mini M4 Pro`), with Linux and Windows/WSL compatibility considered through existing CI or practical validation where runtime behavior or portability is affected.

**Project Type**: Multi-project .NET terminal UI framework and Turbo Vision port with example applications under `examples/`, MSTest smoke coverage under `tests/TuiVision.Examples.SmokeTests/`, and Markdown planning/evidence/governance artifacts.

**Performance Goals**: Smoke proof must be deterministic, bounded, and in-process where practical. Tests must avoid wall-clock sleeps, network calls, unbounded filesystem scans, arbitrary user paths, and timing-sensitive terminal behavior.

**Constraints**: Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` `tvguid01` through `tvguid16`, and `Videomode`. Broad framework work, visual-remediation completion, Wave 2/3/4 behavior, mouse-only requirements, new runtime dependencies, external services, databases, persistent history, and runtime/product AI are out of scope.

**Scale/Scope**: Four Wave-1 example areas, including 16 individually traceable tutorial steps; their existing example code and smoke tests; affected learner guides/README only when triggers apply; feature evidence; proportional architecture, security, A11Y, supply-chain, AI-SBOM, agent-context, Pflichtenheft, and statistics evidence.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Core Constitutional Gates

- **Level-2 environment**: PASS. The feature targets `RiderProjects/TuiVision`, the Level-2 `.NET 10 / C# terminal UI framework and Turbo Vision port` listed in `.specify/memory/constitution.md`.
- **Memory-safe languages (MSL)**: PASS. The primary language is C#, which is on the MSL allow-list.
- **Secure code generation**: PASS. Implementation remains in C#/.NET and existing TuiVision modules. Existing C# secure-coding and project rules apply. The `security-governance` v0.4.0 non-C# language profiles do not create new obligations for this feature.
- **Secure software architecture**: PASS. No web/API/auth surface, external service, network flow, database, persistent user data, runtime/product AI, or arbitrary user-file proof is introduced. Historical sources are read-only.
- **Security documentation**: PASS. `docs/security/` is updated only if implementation changes risk, dependency, supply-chain, or release evidence. Otherwise `pr-evidence.md` records unchanged-risk and `N/A` rationale.
- **Security standards applicability**: PASS. `NIST SSDF` and `CWE Top 25` apply as Level-2 baselines. `OWASP ASVS`, `CAPEC`, and `Zero Trust` are `N/A` unless implementation introduces web/API/auth or changed trust boundaries. SBOM/VEX/SLSA evidence remains unchanged unless a new dependency, release artifact, or supply-chain change is added. `AI-SBOM` is `N/A` while AI is development tooling only and no runtime/product AI is delivered.
- **Release / supply-chain evidence**: PASS. No new dependency or release artifact is planned. Evidence belongs in `pr-evidence.md` and, if materially changed, existing files under `docs/security/`.
- **Default evidence files**: PASS. The default `docs/security/` locations remain preferred. Feature-specific proof is kept in `specs/014-wave1-functional-hardening/pr-evidence.md`.
- **Spec-Kit presets**: PASS. The C#/.NET Level-2 default applies: `security-governance` v0.4.0, `architecture-governance` v0.2.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.2.0, `cross-platform-governance` v0.1.0, and `agent-parity-governance` v0.1.0.
- **Security-first**: PASS. No credential files, logs, local histories, SQLite agent state, generated `_site/`, generated `api/*.yml`, or transient caches are planned for tracking.
- **Inclusion/A11Y**: PASS. Learner-facing Markdown remains text-first. Generated HTML documentation uses WCAG 2.2 AA review if changed. Terminal examples remain keyboard/command oriented; no mouse-only proof is planned.
- **Bilingual delivery**: PASS. Learner-facing guide, README, and evidence text is German-first and English-second at roughly CEFR-B2 where updated.
- **Statistics**: PASS. `docs/project-statistics.md` is updated for this planning phase and again after implementation completion. Manual baseline is 80 lines/workday; Thorsten-solo C#/.NET baseline is 125 lines/workday.
- **Agent guidance parity**: PASS. Because `/speckit-plan` changes active feature planning context, agent context refresh is part of this work item for `codex`, `claude`, `gemini`, and `copilot`, including the repository-specific Copilot surfaces.

### Level-2 Environment Registry

- **Registry Row**: `RiderProjects/TuiVision` - .NET 10 / C# terminal UI framework and Turbo Vision port.
- **Runtime Baseline**: Existing TuiVision libraries, console driver, controls, compatibility, serialization, and examples.
- **Build/Test Baseline**: `dotnet restore`, Release build/test, MSTest suites, Coverlet coverage gates for gate-relevant assemblies, `dotnet format`, DocFX plus Playwright/axe and text-oriented A11Y review where documentation changes.
- **Docs/A11Y Baseline**: DocFX regeneration requires Playwright + axe and lynx-oriented A11Y smoke review for generated documentation.
- **Statistics Baseline**: Experienced developer baseline 80 lines/workday; Thorsten-solo C#/.NET baseline 125 lines/workday.
- **Agent Surfaces**: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`, and Spec-Kit surfaces.

### Post-Design Gate Review

PASS. Phase 0 and Phase 1 artifacts keep 014 within the constitution: no unresolved clarification remains, no new dependency is planned, no changed trust boundary is planned, no generated output is committed, AI-SBOM is explicitly `N/A`, A11Y and bilingual proof paths are defined, the primary evidence matrix is feature-local, and read-only historical source review is mandatory before accepting each hardened proof claim.

## Project Structure

### Documentation (this feature)

```text
specs/014-wave1-functional-hardening/
|-- spec.md
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
|-- pr-evidence.md          # later implementation/PR evidence, created when implementation starts
|-- checklists/
|   `-- requirements.md
|-- contracts/
|   `-- wave1-functional-hardening-acceptance.md
`-- tasks.md                 # later /speckit-tasks output, not created by /speckit-plan
```

### Source Code (repository root)

```text
examples/
|-- Desklogo/
|-- MsgCls/
|-- Tutorial/
|-- Videomode/
`-- README.md

tests/
`-- TuiVision.Examples.SmokeTests/
    |-- *SmokeTests.cs
    `-- ExampleTestBase.cs

docs/
|-- guides/examples/
|-- architecture/
|-- security/
`-- project-statistics.md

tv203s/contrib/tvision/examples/   # read-only historical reference
|-- desklogo/
|-- msgcls/
|-- tutorial/
`-- videomode/

Pflichtenheft.md
Directory.Build.props
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

**Structure Decision**: Keep runtime behavior in the existing Wave-1 example projects and smoke proof in the existing example-smoke test project. Add only tightly scoped example or test code if historical core behavior requires it. Do not create a new runtime package, storage layer, service, or broad framework abstraction.

## Phase 0: Research

Research is captured in [research.md](./research.md). It resolves historical-source review, primary evidence shape, smoke-proof classification, negative/fallback proof, missing-core-function decisions, tutorial step traceability, learner documentation triggers, fixture boundaries, and governance applicability.

## Phase 1: Design and Contracts

Design entities are captured in [data-model.md](./data-model.md). Acceptance obligations are captured in [contracts/wave1-functional-hardening-acceptance.md](./contracts/wave1-functional-hardening-acceptance.md). Planning-consumer validation and review steps are captured in [quickstart.md](./quickstart.md).

## Phase 2: Task Planning Approach

The later `/speckit-tasks` run should produce tasks in this order:

1. Create `specs/014-wave1-functional-hardening/pr-evidence.md` with the required matrix headings and setup evidence.
2. Review historical sources for `Desklogo`, `MsgCls`, `Tutorial` `tvguid01` through `tvguid16`, and `Videomode` as read-only material; record intent, current C# behavior, proof target, missing-core decisions, and deviations.
3. Audit existing Wave-1 smoke tests and helper/headless paths; classify every used proof path as `SetupOnly`, `PrimaryProof`, `SupplementalProof`, or `LegacyOrTemporary`.
4. Add or sharpen failing smoke proof first where managed runtime behavior exists but current proof is only startup, static text, or helper-only.
5. Implement small missing historical core behavior only when it is necessary for the existing Wave-1 functional purpose and feasible without broad framework work, visual remediation, or new dependencies.
6. Record negative and fallback path proof through deterministic smokes or evidence records with trigger, expected deviation, observed fallback, and proof boundary.
7. Update affected guides or `examples/README.md` only when runtime behavior, usage path, visible output, historical deviation, or learner-facing proof explanation changes.
8. Update proportional architecture, security, A11Y, supply-chain, AI-SBOM, Pflichtenheft, agent-context, and statistics evidence with unchanged or `N/A` rationale where applicable.
9. Run and record formal validation during implementation: `dotnet restore`, Release build/test, targeted example smokes, full Release tests, Coverlet coverage, `dotnet format --verify-no-changes`, `git diff --check`, plus DocFX/web-a11y when documentation output or navigation is affected.

## Complexity Tracking

No constitution violations are introduced by this plan.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|--------------------------------------|
| None | N/A | N/A |
