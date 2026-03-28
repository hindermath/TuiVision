# Implementation Plan: Mandatory Example Wave 1 Ports

**Branch**: `007-port-wave1-examples` | **Date**: 2026-03-27 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/007-port-wave1-examples/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/007-port-wave1-examples/spec.md)
**Input**: Feature specification from `/specs/007-port-wave1-examples/spec.md`

## Summary

Deliver the first mandatory example wave by adding four managed example
deliverables for the original `desklogo`, `msgcls`, `tutorial`, and
`videomode` scopes under `examples/`, replacing the current module-smoke
placeholder coverage in `tests/TuiVision.Examples.SmokeTests/` with real
example-focused MSTest smoke scenarios, documenting the examples in
`docs/guides/examples/`, and updating the project-tracking surfaces so wave 1
progress is explicit without leaking into later mandatory waves. The plan keeps
the source architecture inside the existing five framework modules and treats
the examples as consumer applications that exercise that framework.

## Terminology & Operational Definitions

- **Wave-1 example**: One of the four mandatory original example scopes from
  `tv203s/contrib/tvision/examples`: `desklogo`, `msgcls`, `tutorial`, or
  `videomode`.
- **Primary example application**: The user-facing managed example delivery
  that corresponds to the historical example's main behavior. Historical helper
  utilities inside the same source folder are only in scope when they are
  necessary to reproduce visible behavior, assets, or smoke validation.
- **Managed example delivery**: A runnable .NET example project under
  `examples/` with one canonical entry point, one reviewable smoke-validation
  surface, and one guide surface that together define the acceptance target.
- **Tutorial step token**: The canonical selector for one original tutorial
  lesson, preserved as `tvguid01` through `tvguid16`.
- **Example smoke scenario**: One repeatable MSTest validation path that
  launches an example, verifies its defining behavior, and proves a clean exit.
- **In-process smoke seam**: A test-callable example host or startup surface
  that lets MSTest prove startup, defining behavior, and clean exit without
  depending on interactive terminal automation as the primary assertion method.
- **Clean exit**: A documented and assertable example shutdown path that leaves
  no forced process termination, hanging modal state, or ambiguous pending
  interaction behind.
- **Visible fallback**: A user-observable message or state shown when
  `videomode` cannot perform the requested real terminal-supported transition.
- **Wave-tracking surface**: One repository artifact that must reflect progress
  for this increment, such as `Pflichtenheft.md`, `docs/project-statistics.md`,
  or the example guides.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing modules `TuiVision.Core`,
`TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`,
`TuiVision.Compatibility`; MSTest in `tests/TuiVision.Examples.SmokeTests`;
repository validation via `dotnet build`, `dotnet test`, `dotnet format`, and
conditional `docfx docfx.json`; GitHub Actions for the existing CI path  
**Storage**: Source-controlled example projects under `examples/`,
source-controlled guides under `docs/guides/examples/`, source-controlled
tracking artifacts (`Pflichtenheft.md`, `docs/project-statistics.md`); no
database or external service storage  
**Testing**: MSTest-first example smoke coverage in
`tests/TuiVision.Examples.SmokeTests/`; existing module tests remain part of
the repository-wide gate; `dotnet build --configuration Release`,
`dotnet test`, `dotnet format --verify-no-changes`, and conditional
`docfx docfx.json` remain mandatory; smoke tests for all newly ported examples
must become CI-ready  
**Target Platform**: Managed cross-platform .NET TUI applications on macOS,
Linux, and Windows/WSL, with `MacBook Air M2` and `Mac mini M4 Pro` as the
primary development machines  
**Project Type**: Managed .NET framework-consumer increment with example
applications, smoke tests, and didactic documentation  
**Performance Goals**: Wave-1 examples must start and complete their smoke
paths without manual timing-sensitive interaction; the example smoke suite must
remain practical for routine local and CI validation on the documented .NET 10
workflow  
**Constraints**: No native bindings; no new source framework module; TDD with
visible Red-Green-Refactor history remains mandatory; only the four mandatory
wave-1 example scopes are in scope; `tutorial` covers all 16 original steps
with individual smoke coverage and one shared guide page; `videomode` must use
real supported transitions where possible and an explicit visible fallback
otherwise; numbered Spec-Kit branch version alignment in `Directory.Build.props`
must be maintained before `dotnet build` or `dotnet test`; explanatory
documentation and code comments remain bilingual at CEFR-B2  
**Scale/Scope**: Four managed example deliveries, sixteen individually
addressable tutorial steps, one wave-focused smoke test project update, four
example-guide deliverables (`desklogo`, `msgcls`, `tutorial`, `videomode`),
and the related wave-tracking updates in project documentation

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. The plan consumes the existing managed
  modules and does not introduce native terminal bindings or OS-specific
  package dependencies.
- **Test-First Development — TDD**: Pass with explicit workflow constraint.
  Each example port and each tutorial step must begin with failing MSTest smoke
  coverage before production code is added, and the resulting smoke tests join
  the repository-wide validation path.
- **Didactic and Linguistic Clarity**: Pass. This increment adds example
  applications, guides, and likely internal comments or XML docs; all
  explanatory material remains bilingual with German first and English second.
- **Modular Architecture**: Pass. No sixth framework module is introduced.
  Example executables live under `examples/` and consume the established five
  modules as clients.
- **Cross-Platform Portability**: Pass with explicit validation requirement.
  Example behavior affects runtime and terminal interaction, so Linux and
  Windows/WSL compatibility checks are required in addition to the primary
  Multi-Mac workflow.
- **License & Disclaimer Integrity**: Pass. Historical files under `tv203s/`
  remain read-only reference input; new example code and guides stay under the
  existing project-owned licensing and disclaimer rules.

**Post-Design Gate Review**: Phase-1 artifacts keep the feature inside the
existing five-module architecture, preserve managed-only runtime assumptions,
retain MSTest-first smoke validation, and maintain the distinction between the
four mandatory wave-1 examples and all later example waves. No constitution
exception is required.

- The feature advances mandatory original-example scope, not `TVDEMOS/` or
  `TVFM/` follow-on work.
- The feature affects runtime behavior and validation workflow, so Linux and
  Windows/WSL evidence must be planned in addition to the primary Multi-Mac
  path.
- Statistical-documentation impact identified; update
  `docs/project-statistics.md` when the implementation phase lands.

## Project Structure

### Documentation (this feature)

```text
specs/007-port-wave1-examples/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── wave-1-example-contract.md
├── checklists/
│   └── requirements.md
└── tasks.md
```

### Source Code (repository root)

```text
examples/
├── Desklogo/
├── MsgCls/
├── Tutorial/
│   ├── Tutorial.csproj
│   ├── TutorialApp.cs
│   └── Steps/
│       ├── TvGuid01Step.cs
│       ├── ...
│       └── TvGuid16Step.cs
└── Videomode/

src/
├── TuiVision.Core/
├── TuiVision.Controls/
├── TuiVision.Drivers.Console/
├── TuiVision.Serialization/
└── TuiVision.Compatibility/

tests/
└── TuiVision.Examples.SmokeTests/

docs/
├── guides/
│   ├── examples/
│   └── multi-mac-workflow.md
├── project-statistics.md

tv203s/
└── contrib/tvision/examples/
    ├── desklogo/
    ├── msgcls/
    ├── tutorial/
    └── videomode/
```

**Structure Decision**: Use one dedicated managed example project for
`Desklogo`, `MsgCls`, and `Videomode`, plus one shared `Tutorial` project that
keeps the 16 original tutorial steps in a single codebase while exposing each
step through a stable selector token. Keep smoke validation centralized in
`tests/TuiVision.Examples.SmokeTests/` rather than splitting example-specific
test projects, because the examples are one wave-scoped delivery unit and
benefit from shared runner utilities. No new framework module or external
storage layer is justified.

## Research Focus

Phase 0 resolves and locks the following planning decisions:

1. The four wave-1 example scopes map to four managed example deliverables,
   with `tutorial` covering all 16 original steps under one shared project.
2. `tutorial` steps remain individually runnable through canonical step tokens
   rather than through 16 separate solution projects.
3. Example smoke validation should run in-process where possible through shared
   application-host seams, avoiding brittle interactive terminal automation as
   the primary CI path.
4. Historical helper programs or asset generators are only included when they
   are necessary to reproduce visible behavior, assets, or repeatable smoke
   validation.
5. `videomode` must prefer real terminal-supported transitions and fall back to
   an explicit visible limitation when the runtime cannot perform them.
6. Example guides are delivered in the same work stream as code and tests, with
   one guide page per example and one shared guide page for `tutorial`.
7. Wave progress must be reflected in `Pflichtenheft.md` and
   `docs/project-statistics.md` when implementation lands.

## Design Decisions

### Example Packaging Boundary

- `Desklogo`, `MsgCls`, and `Videomode` are separate managed example projects
  under `examples/`.
- `Tutorial` is one managed project with 16 individually addressable steps,
  preserving the original `tvguid01` to `tvguid16` identity without creating
  16 extra projects.
- Example projects act as consumers of the framework modules and may add
  example-local helpers, runners, or view models as needed.

### Smoke Validation Boundary

- `tests/TuiVision.Examples.SmokeTests/` becomes the canonical home for wave-1
  smoke scenarios.
- Each example gets at least one defining smoke scenario; `tutorial` gets one
  smoke scenario per original step.
- Smoke validation should prefer deterministic in-process seams over fragile
  console automation, while still proving the real example startup and exit
  path.
- A qualifying in-process seam must still exercise the real example startup
  contract and a documented clean-exit path; it is not a substitute for the
  example itself.
- Negative or fallback scenarios are required where the example behavior makes
  them meaningful, especially for `videomode`.

### Documentation Boundary

- `docs/guides/examples/desklogo.md`, `msgcls.md`, and `videomode.md` each
  describe one example.
- `docs/guides/examples/tutorial.md` is one shared guide page with step-scoped
  sections for `tvguid01` through `tvguid16`.
- Guides remain part of the acceptance surface, not a follow-up cleanup task.

### Tracking and Delivery Boundary

- `Pflichtenheft.md` must show wave-1 progress clearly without collapsing later
  waves into the same status bucket.
- `docs/project-statistics.md` must be refreshed when the implementation phase
  completes.
- Until implementation lands, the current `Pflichtenheft.md` next-step marker
  and `docs/project-statistics.md` entries are treated as pre-wave baseline
  state rather than partial delivery evidence for feature 007.
- The existing example smoke test project replaces its current module-smoke
  placeholder role with actual example-application coverage.
- If wave-1 implementation changes shared agent guidance, active technologies,
  or project structure, the synchronized agent-guidance surfaces
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`,
  and `.github/agents/copilot-instructions.md` must be updated in the same
  work item.

## Complexity Tracking

No constitution violations or extra source-module justifications are required
for this feature.
