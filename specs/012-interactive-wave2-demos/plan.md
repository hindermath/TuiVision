# Implementation Plan: Interactive Wave 2 Demos

**Branch**: `012-interactive-wave2-demos` | **Date**: 2026-05-09 | **Spec**: [spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/spec.md)
**Input**: Feature specification from `/specs/012-interactive-wave2-demos/spec.md`

**Note**: This document follows the Spec-Kit plan template and records the implementation baseline for `/speckit-plan`. It does not execute implementation tasks.

## Summary

Convert the eleven Wave-2 example applications delivered by `011-port-wave2-examples` from function-proof examples into visibly operable terminal demos. Each example must start with a meaningful first screen, expose its primary behavior through menu, keyboard, or command paths, update a visible status/desktop/dialog/control state after every demonstrated operation, and prove the path through deterministic smoke tests that drive the real application loop (`app.Run()` or the equivalent runtime loop) with injected `TEvent`, command, or key input. Before wiring each example, the matching historical `.c`/`.cc` source and any important matching headers under `tv203s/` must be reviewed as read-only reference so the planned interaction reflects the original demo purpose or documents intentional deviations.

The implementation will use `examples/Demo` as the first P1 vertical slice because it combines broad controls, standard dialogs, file/path metadata, invalid/cancel handling, color/display choices, and visible status feedback. After that slice proves the shared runtime pattern, the remaining examples are implemented in small behavior families: clipboard operations, dynamic text, input/list/combo selection, progress behavior, dialog-designer rendering, and scrollable dialogs.

The existing direct proof methods from 011 remain useful as setup or supplemental assertions, but they no longer count as the primary runtime proof. Guide, README, PR evidence, architecture, security, A11Y, and statistics surfaces must be updated only for the new interactive behavior and validation evidence. Generated DocFX `_site/` output and generated `api/*.yml` files remain uncommitted build artifacts.

## Terminology

**Interactive Example**: A Wave-2 example that exposes the historical/demo behavior through a visible runtime surface instead of only through direct helper methods.

**Operation Path**: A menu command, keyboard shortcut, status command, or scripted command event that a user or smoke test can trigger through the application loop.

**Visible Feedback State**: Text or UI state visible in the terminal application after an operation, such as a status line message, desktop text, selected row, progress value, dialog result, or validation message.

**Primary Smoke Scenario**: The deterministic smoke path that starts the example runtime and injects events through the same dispatch route used by the interactive application.

**Read-Only Fixture Path**: A source-controlled fixture path or test temporary path used to demonstrate metadata, validation, loading, rendering, or rejection without reading arbitrary user file contents and without persisting user state.

**Historical Source Review**: A per-example comparison against the relevant read-only `.c`/`.cc` files and any important matching headers under `tv203s/`, used to confirm the original demo intent, identify essential interaction paths, and record intentional deviations.

**Proof Surface**: The repository artifact that records completion evidence, especially smoke tests, `pr-evidence.md`, guides, `examples/README.md`, architecture/security notes, and project statistics.

## Technical Context

**Language/Version**: C# `latest` / C# 14 on .NET 10 (`net10.0`)

**Primary Dependencies**: Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned.

**Storage**: Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service.

**Testing**: Primary validation is `dotnet test tests/TuiVision.Examples.SmokeTests/`, full `dotnet test`, and the repository coverage gate via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings` before merge. `dotnet format --verify-no-changes` remains the style gate. Because guide and documentation content will change, DocFX generation and `tests/web-a11y` smoke validation are planned evidence, with generated output excluded from commits.

**Target Platform**: Terminal UI examples on the primary Multi-Mac workflow (`MacBook Air M2` and `Mac mini M4 Pro`), with Linux and Windows/WSL compatibility considered through existing CI or equivalent practical validation when runtime behavior is affected.

**Project Type**: Multi-project .NET solution with example applications under `examples/`, MSTest smoke coverage under `tests/TuiVision.Examples.SmokeTests/`, and source-controlled Markdown proof/doc artifacts.

**Performance Goals**: Smoke paths must be deterministic, fast, and in-process. Event scripts should avoid wall-clock sleeps, network calls, or unbounded filesystem scans. Progress examples may simulate increments but must expose bounded completion/abort/cancel states.

**Constraints**: Do not start Wave 3 or Wave 4 example work. Do not require mouse interaction as the only path. Do not redesign the framework broadly. Do not introduce unrelated documentation-platform changes. Every example must show a meaningful first screen and a visible operation result. Primary smoke proof must use the real runtime dispatch route, not only direct methods. Historical files under `tv203s/` are read-only reference material and must not be edited.

**Scale/Scope**: Eleven existing Wave-2 example projects (`Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, `TProgB`), their smoke tests, their guides, shared README/evidence surfaces, and proportional governance documentation.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Core Constitutional Gates

- **I. Preserve User Work**: PASS. The plan builds on the current `012-interactive-wave2-demos` branch and does not require reverting unrelated changes.
- **II. Branch and Review Discipline**: PASS. Work stays on numbered Spec-Kit branch `012-interactive-wave2-demos`. Any future commit before push must align `Directory.Build.props` to branch version `1.12.<patch>.<build>`.
- **III. Test-First and Evidence-First Delivery**: PASS. The plan requires event-loop smoke tests for visible runtime paths plus full suite, coverage, format, and documentation validation evidence before merge.
- **IV. Accessibility and Text-First Proof**: PASS. Keyboard/menu/command paths are mandatory; visible feedback must be readable as text; guides remain German-first/English-second; DocFX/A11Y smoke proof is planned for documentation changes.
- **V. Documentation and Statistics Coupling**: PASS. Example guides, `examples/README.md`, `pr-evidence.md`, architecture/security notes, and `docs/project-statistics.md` are part of the planned proof surface where affected.
- **VI. Multi-Agent Guidance Parity**: PASS. After plan generation, `.specify/scripts/bash/update-agent-context.sh` will be run for `codex`, `claude`, `gemini`, and `copilot` as pre-approved repository maintenance.
- **VII. Generated Output Hygiene**: PASS. Generated `_site/`, generated `api/*.yml`, and other DocFX build artifacts remain ignored/uncommitted.
- **VIII. Coverage Gate**: PASS. The repository merge gate remains at least 70% line coverage for `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`. Example assemblies are not the coverage target, but the full configured gate must still pass before merge.

### Governance Preset Gates

- **Security Governance**: PASS. NIST SSDF and CWE Top 25 remain default review lenses. ASVS is not applicable unless web/API/auth surfaces are introduced. VEX/SBOM evidence follows normal release-artifact policy; no new dependency is planned.
- **Architecture Governance**: PASS. Runtime behavior changes require focused updates or confirmation in `docs/architecture/architecture-vision.md`, `docs/architecture/runtime-view.md`, `docs/architecture/quality-scenarios.md`, and `docs/architecture/architecture-risks.md`. An ADR is only required if implementation introduces a new reusable framework abstraction or cross-cutting runtime contract beyond example/test helpers.
- **iSAQB Architecture Governance**: PASS. The plan names runtime views, quality scenarios, constraints, and risks explicitly; no new architectural style is introduced.
- **A11Y Governance**: PASS. Keyboard-operable examples, text-readable feedback, CEFR-B2 guide language, DocFX build, and Playwright/axe smoke validation are planned.
- **Cross-Platform Governance**: PASS. No OS-specific dependency is planned. Runtime behavior changes should remain compatible with macOS, Linux, and Windows/WSL terminal workflows.
- **Agent-Parity Governance**: PASS. Agent context refresh is part of this planning work item; manual guidance files are updated only if the plan changes shared guidance.

### Level-2 Environment Registry

- **Registry Row**: TuiVision Level-2 project, `.NET 10 / C# terminal UI framework and Turbo Vision port`.
- **Runtime Baseline**: Existing .NET 10 solution and examples.
- **Build/Test Baseline**: `dotnet restore`, `dotnet build --configuration Release`, `dotnet test`, coverage via Coverlet, `dotnet format`, DocFX plus Playwright/axe where docs change.
- **Statistics Baseline**: Experienced-developer baseline 80 lines/workday; Thorsten-solo C#/.NET baseline 125 lines/workday; AI-assisted visible work window recorded in `docs/project-statistics.md` after implementation.

### Post-Design Gate Review

PASS. Phase 0 and Phase 1 design artifacts keep the implementation within the constitution: no new dependencies, no storage boundary, no unrelated framework redesign, no generated output commits, no unreviewed security boundary, and no unresolved spec clarification remains before task generation.

## Project Structure

### Documentation (this feature)

```text
specs/012-interactive-wave2-demos/
|-- spec.md
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
|-- pr-evidence.md
`-- contracts/
    `-- interactive-wave2-demo-acceptance.md
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
`-- Shared/                  # add only if it reduces repeated example runtime glue

tests/
`-- TuiVision.Examples.SmokeTests/
    |-- *SmokeTests.cs
    `-- ExampleTestBase.cs   # may gain event-script helpers or delegate to a focused helper file

docs/
|-- guides/examples/
|-- architecture/
|-- security/
`-- project-statistics.md

Pflichtenheft.md
Directory.Build.props
```

**Structure Decision**: Keep production behavior inside the existing example projects. Add only small source-level helper code for repeated interactive command/status/event wiring if duplication becomes material; do not create a new runtime package or framework abstraction unless implementation proves it necessary and records the architecture decision.

## Phase 0: Research

Research is captured in [research.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/research.md). All open implementation questions from the spec are resolved there: vertical-slice order, event-loop smoke design, shared helper boundaries, read-only file/fixture policy, documentation/A11Y evidence, and governance proof scope.

## Phase 1: Design and Contracts

Design entities are captured in [data-model.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/data-model.md). Runtime and proof obligations are captured in [contracts/interactive-wave2-demo-acceptance.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/contracts/interactive-wave2-demo-acceptance.md). Implementation and validation entry points are captured in [quickstart.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/012-interactive-wave2-demos/quickstart.md).

## Phase 2: Task Planning Approach

The later `/speckit-tasks` run should produce tasks in this order:

1. For each of the eleven examples, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference and record the original interaction intent plus any planned deviation.
2. Establish the shared smoke-event and visible-feedback test pattern on `examples/Demo`.
3. Implement the `Demo` P1 vertical slice and its runtime smoke tests.
4. Apply the proven pattern to clipboard and text examples.
5. Apply the proven pattern to list, input, combo, and boundary-state examples.
6. Apply the proven pattern to progress and abort/cancel examples.
7. Apply the proven pattern to dialog-designer and scroll-dialog examples.
8. Update guides, README, evidence, architecture/security/A11Y/statistics surfaces, including the historical-source comparison outcome where user-visible behavior differs.
9. Run the validation gate and record evidence.

## Complexity Tracking

No constitution violations are introduced by this plan.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|--------------------------------------|
| None | N/A | N/A |
