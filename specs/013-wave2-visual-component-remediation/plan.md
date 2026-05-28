# Implementation Plan: Wave 2 Visual Component Remediation

**Branch**: `013-wave2-visual-component-remediation` | **Date**: 2026-05-22 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `./spec.md`

**Note**: This document follows the Spec-Kit plan template and records the implementation baseline for `/speckit-plan`. It stops before task generation.

## Summary

Remediate the eleven Wave-2 examples so that their primary parity proof is a real visible TuiVision composition: controls, dialogs, windows, scroll groups, progress displays, input/list/combo compositions, or another stable visual runtime state that matches the historical example intent. The existing `012-interactive-wave2-demos` app-loop menus and text-first feedback remain the starting point, but they are supporting runtime infrastructure rather than sufficient primary proof.

Each example will follow the clarified three-layer model: a visible main component, a real `TStatusLine` for short dynamic feedback, and a keyboard-reachable `Help -> Description` path that explains what the learner sees. Primary smoke tests must drive the real application loop and prove concrete control/dialog/focus/selection/scroll/progress state. They must also include a stable rendered visibility proof that combines view-tree evidence with a buffer/cell snapshot showing control-specific content at the expected position or region.

The feature stays narrow: all eleven Wave-2 examples are in scope, Wave 3 and Wave 4 are out of scope, and only the smallest required shared control/status/test seams may be added. Historical C/C++ sources under `tv203s/` are read-only intent references for each example, including matching headers where declarations are needed. Any intentional user-visible deviation must be documented in planning, guide, evidence, or PR material.

## Baseline Assumptions

- The implementation starts from the merged 011 Wave-2 port and the 012 interactive showcase baseline on `main`.
- 012 app-loop command paths, menus, keyboard routing, and text-first status messages should be reused where they help, but `VisibleText`, `VisibleHistory`, or direct helpers do not count as primary parity proof.
- The short 012 status sentences remain valuable and move into `TStatusLine` or a documented equivalent status area.
- The canonical learner explanation path is `Help -> Description` for all eleven examples. `About` may exist only as supplemental context.
- File/path, dialog-designer, and clipboard-adjacent proof uses source-controlled fixtures or test temporary directories only. Arbitrary user files, persistent user history, and external proof paths are not valid proof sources.
- The plan uses the propagated governance baseline from constitution v1.14.0 and `security-governance` v0.4.0. `AI-SBOM` is `N/A` because no runtime/product AI is delivered; this decision must be re-evaluated if delivered AI components enter scope.

## Terminology

**Visible Main Component**: The primary runtime control, dialog, window, view group, scroll group, progress display, dynamic text view, combo/input composition, or stable visual state used as the parity proof.

**Three-Layer Runtime Model**: The required composition of visible main component, `TStatusLine` feedback, and `Help -> Description` description path.

**Stable Rendered Visibility Proof**: Smoke evidence that combines view-tree proof with a buffer/cell snapshot containing control-specific content at the expected position or region.

**Primary Smoke Proof**: A deterministic test that drives the real application loop, command, key, or event path and verifies concrete visible state. Direct helpers may set up or supplement only.

**Historical Source Review**: The per-example read-only review of relevant `.c`/`.cc` files and important matching headers under `tv203s/`, recording original visual intent, target C# visual state, and intentional deviations.

## Technical Context

**Language/Version**: C# `latest` / C# 14 on .NET 10 (`net10.0`)

**Primary Dependencies**: Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned.

**Storage**: Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads.

**Testing**: Primary validation is `dotnet build --configuration Release`, `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release`, full `dotnet test --configuration Release`, and the repository coverage gate via `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`. `dotnet format --verify-no-changes` remains the style gate. DocFX plus `tests/web-a11y` validation is required when guides, DocFX content, navigation, or API documentation are changed. `Directory.Build.props` must be aligned to branch version `1.13.<patch>.<build>` and the manual build counter must be incremented before build/test commands.

**Target Platform**: Terminal UI examples on the primary Multi-Mac workflow (`MacBook Air M2` and `Mac mini M4 Pro`), with Linux and Windows/WSL compatibility considered through existing CI or practical validation where runtime behavior is affected.

**Project Type**: Multi-project .NET solution with example applications under `examples/`, MSTest smoke coverage under `tests/TuiVision.Examples.SmokeTests/`, and source-controlled Markdown proof/doc artifacts.

**Performance Goals**: Smoke paths must be deterministic, fast, and in-process. They must avoid wall-clock sleeps, network calls, unbounded filesystem scans, and timing-sensitive animation. Progress examples may simulate progress, but completion, abort, and cancel states must be bounded and assertable.

**Constraints**: No Wave-3 or Wave-4 functionality. No mandatory mouse-only path. No broad framework redesign. No new runtime dependency unless separately justified. Historical files under `tv203s/` are read-only. Larger missing framework capabilities are documented as intentional deviations instead of being solved inside 013.

**Scale/Scope**: Eleven existing Wave-2 example projects: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; their smoke tests; affected guides and README material; feature evidence; proportional architecture, security, A11Y, supply-chain, AI-SBOM, and statistics evidence.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Core Constitutional Gates

- **Level-2 environment**: PASS. The feature targets `RiderProjects/TuiVision`, the Level-2 .NET 10 / C# terminal UI framework and Turbo Vision port listed in `.specify/memory/constitution.md`.
- **Memory-safe languages (MSL)**: PASS. The primary language is C#, which is on the MSL allow-list for all `RiderProjects/*` C#/.NET 9-10 entries.
- **Secure code generation**: PASS. Implementation stays inside C#/.NET and existing TuiVision modules. The existing C#/.NET secure-coding rules and TuiVision project rules continue to apply. `security-governance` v0.4.0 adds language-specific secure-coding profiles for Rust, Go, Swift, Java/Kotlin, Python, and TypeScript/JavaScript; those profiles do not create new implementation obligations for this C#/.NET feature.
- **Secure software architecture**: PASS. Trust boundaries do not expand: no web/API/auth surface, network flow, external service, database, arbitrary user-file proof, or persistent user data is introduced. Least-privilege fixture/temp-directory use is planned.
- **Security documentation**: PASS. `docs/security/` needs updates only if implementation changes existing risk evidence. Otherwise feature evidence records the unchanged NIST SSDF/CWE posture and N/A decisions.
- **Security standards applicability**: PASS. `NIST SSDF` and `CWE Top 25` apply as Level-2 baselines. `OWASP ASVS`, `CAPEC`, and `Zero Trust` are `N/A` unless implementation introduces web/API/auth or changed trust boundaries. SBOM, VEX, and SLSA stay on the normal repository evidence path unless a new dependency, releasable artifact, or supply-chain change is added. `security-governance` v0.4.0 keeps the conditional `AI-SBOM` model: development-tool-only AI usage is `AI-SBOM: N/A`, while runtime/product AI, models, datasets, AI infrastructure, or delivered AI components require re-evaluation and evidence planning.
- **Release / supply-chain evidence**: PASS. No new dependency or release artifact is planned. Evidence belongs in feature evidence and, if changed, `docs/security/supply-chain-evidence.md`.
- **Spec-Kit presets**: PASS. The C#/.NET Level-2 default applies: `security-governance` v0.4.0, `architecture-governance` v0.2.0, `isaqb-architecture-governance` v0.1.0, `a11y-governance` v0.2.0, `cross-platform-governance` v0.1.0, and `agent-parity-governance` v0.1.0.
- **Security-first**: PASS. No credential files, logs, local histories, SQLite agent state, generated `_site/`, generated `api/*.yml`, or other transient artifacts are planned for tracking.
- **Inclusion/A11Y**: PASS. Terminal UI remains keyboard-first and text-first; `Help -> Description` and `TStatusLine` keep visible behavior understandable for text-first review. Generated HTML docs use WCAG 2.2 AA where changed.
- **Bilingual delivery**: PASS. Learner-facing guide and README changes must be German-first, English-second, and roughly CEFR-B2. This plan keeps the proof requirement explicit.
- **Statistics**: PASS. `docs/project-statistics.md` is updated for this planning phase and must be updated again after implementation completion. Manual baseline is 80 lines/workday; Thorsten-solo C#/.NET baseline is 125 lines/workday.
- **Agent guidance parity**: PASS. Because `/speckit-plan` changes active feature planning context, agent context refresh is part of this work item for `codex`, `claude`, `gemini`, and `copilot`. The maintained agent guidance files are reviewed together.

### Level-2 Environment Registry

- **Registry Row**: `RiderProjects/TuiVision` - .NET 10 / C# terminal UI framework and Turbo Vision port.
- **Runtime Baseline**: Existing TuiVision libraries, console driver, controls, compatibility, serialization, and examples.
- **Build/Test Baseline**: `dotnet restore`, Release build/test, MSTest suites, Coverlet coverage gates for gate-relevant assemblies, `dotnet format`, DocFX plus Playwright/axe and text-oriented A11Y review where documentation changes.
- **Statistics Baseline**: Experienced developer baseline 80 lines/workday; Thorsten-solo C#/.NET baseline 125 lines/workday.
- **Agent Surfaces**: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`, and Spec-Kit surfaces.

### Post-Design Gate Review

PASS. Phase 0 and Phase 1 design artifacts keep the feature within the constitution and the propagated `security-governance` v0.4.0 baseline: no unresolved clarification remains, no new dependency is planned, no changed trust boundary is planned, no generated output is committed, AI-SBOM is explicitly `N/A`, the new non-C# language-specific secure-coding profiles create no new obligation for this C#/.NET feature, A11Y and bilingual proof paths are defined, and historical-source review remains mandatory for all eleven examples.

## Project Structure

### Documentation (this feature)

```text
specs/013-wave2-visual-component-remediation/
|-- spec.md
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
|-- checklists/
|   `-- requirements.md
|-- contracts/
|   `-- wave2-visual-component-acceptance.md
`-- tasks.md                 # later /speckit-tasks output, not created by /speckit-plan
```

### Source Code (repository root)

```text
examples/
|-- Clipboard/
|-- Demo/
|-- DlgDsn/
|-- DynTxt/
|-- InpLis/
|-- ListVi/
|-- ProgBa/
|-- Sdlg/
|-- Sdlg2/
|-- TCombo/
|-- TProgB/
|-- README.md
`-- Shared/                  # use only if small shared runtime glue is needed

tests/
`-- TuiVision.Examples.SmokeTests/
    |-- *SmokeTests.cs
    `-- ExampleTestBase.cs   # may gain bounded visibility snapshot helpers

docs/
|-- guides/examples/
|-- architecture/
|-- security/
`-- project-statistics.md

Pflichtenheft.md
Directory.Build.props
```

**Structure Decision**: Keep production behavior in the existing example projects. Add only small source-level helper code for repeated visible-component/status/description/smoke evidence if duplication becomes material. Do not create a new runtime package or broad framework abstraction unless implementation proves it unavoidable and records the architecture decision.

## Phase 0: Research

Research is captured in [research.md](./research.md). It resolves the main design choices: visible proof model, three-layer runtime model, `Demo` vertical slice, per-example visual obligations, historical-source review, fixture boundaries, validation gates, and governance evidence.

## Phase 1: Design and Contracts

Design entities are captured in [data-model.md](./data-model.md). Runtime and proof obligations are captured in [contracts/wave2-visual-component-acceptance.md](./contracts/wave2-visual-component-acceptance.md). Implementation and validation entry points are captured in [quickstart.md](./quickstart.md).

## Phase 2: Task Planning Approach

The later `/speckit-tasks` run should produce tasks in this order:

1. Review the relevant historical `.c`/`.cc` files and required headers under `tv203s/` for each of the eleven examples; record original visual intent, target C# visual state, and planned deviations.
2. Establish the shared stable rendered visibility proof approach: view-tree proof plus buffer/cell snapshot at expected region, with direct helpers limited to setup or supplemental assertions.
3. Implement `Demo` as the P1 vertical slice proving `Dialog/Control`, `File/Path metadata`, and `Display/Color/Gadget` visible flow families.
4. Apply the three-layer model and smoke proof to `Clipboard`, including copy, cut, paste, and unavailable-clipboard visual states.
5. Apply the model to `DynTxt`, `InpLis`, `ListVi`, and `TCombo` for dynamic text, input, list, history, selection, boundary, empty, and focus states.
6. Apply the model to `ProgBa` and `TProgB` for completion, partial progress, abort, and cancelled states.
7. Apply the model to `DlgDsn`, `Sdlg`, and `Sdlg2` for dialog rendering/rejection, one-axis scroll/focus, and two-axis scroll/focus states.
8. Add or adjust `Help -> Description` in every app and verify reachability plus content through primary or supplemental smokes.
9. Update affected guides, `examples/README.md`, feature evidence, architecture/security/A11Y/supply-chain/AI-SBOM rationale, `docs/project-statistics.md`, and `Pflichtenheft.md` markers where applicable. Security evidence must name the `security-governance` v0.4.0 baseline and confirm that the added Rust/Go/Swift/Java/Kotlin/Python/TypeScript/JavaScript secure-coding profiles are not applicable to the C#/.NET implementation.
10. Run and record the formal validation gate: Release build, fast Example-Smoke suite, full Release test run, Coverlet coverage gate, `dotnet format --verify-no-changes`, plus DocFX/web-a11y when documentation output or navigation changes.

## Complexity Tracking

No constitution violations are introduced by this plan.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|--------------------------------------|
| None | N/A | N/A |
