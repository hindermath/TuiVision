# Implementation Plan: Port Wave 2 Examples

**Branch**: `011-port-wave2-examples` | **Date**: 2026-05-06 | **Spec**: [spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/011-port-wave2-examples/spec.md)
**Input**: Feature specification from `/specs/011-port-wave2-examples/spec.md`

## Summary

Port the eleven mandatory wave-2 examples from
`tv203s/contrib/tvision/examples/` into managed C# example projects under
`examples/`, add deterministic in-process smoke coverage, and provide
DE-first/EN-second didactic guides for each example. The feature proves the
Controls/Dialog layer through real applications while preserving the clarified
scope: `sdlg` and `sdlg2` are completed in wave 2 for their historical
`ScrollDialog`/`ScrollGroup` purpose; broader historical parity is recorded as
separate cleanup after the prioritized mandatory example waves. The plan reuses
framework readiness from `008-controls-revision`, `009-controls-widgets-and-
collections`, and `010-standard-dialogs-designer`; missing framework behavior
is implemented only where it blocks a required wave-2 example.

## Terminology & Operational Definitions

- **Wave-2 example**: One of `clipboard`, `demo`, `dlgdsn`, `dyntxt`,
  `inplis`, `listvi`, `progba`, `sdlg`, `sdlg2`, `tcombo`, or `tprogb`.
- **Historical purpose**: The observable behavior represented by the original
  example source under `tv203s/contrib/tvision/examples/`, excluding build
  helper files and later-wave terminal, editor, help, stream, mouse, or charset
  effects.
- **Example-specific deterministic interaction**: A smoke-test path that sends
  or invokes at least one example-specific user-visible action and verifies the
  resulting visible state. Startup plus clean exit is insufficient.
- **Scrollable dialog flow**: The `sdlg`/`sdlg2` proof of vertical and combined
  horizontal/vertical dialog scrolling, focus movement, bounds, and visible
  control state.
- **Standard-dialog proof**: File, directory, color, display, validation,
  cancellation, invalid-path, and dynamic-dialog evidence supplied through
  `demo`, `dlgdsn`, or another historically justified wave-2 flow; it is not
  assigned to `sdlg`/`sdlg2`.
- **Historical Example Parity Cleanup**: A follow-up record for optional or
  expanded historical parity beyond wave-2 acceptance, scheduled no earlier
  than after mandatory waves 1-4 are complete.
- **Wave proof record**: The traceable completion evidence in `Pflichtenheft.md`,
  example guides, smoke tests, project statistics, architecture/security/A11Y
  evidence, and the next-step marker.

## Technical Context

- **Language/Version**: C# `latest` / C# 14 on .NET 10 (`net10.0`)
- **Primary Dependencies**: Existing `TuiVision.Core`,
  `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`,
  and `TuiVision.Drivers.Console`; existing Controls/Dialog/Serialization
  surfaces from features `008`, `009`, and `010`; MSTest 4.0.1; Coverlet
  collector for coverage; conditional DocFX and Playwright + axe documentation
  smoke tooling
- **Storage**: Runtime example state is in memory. Standard-dialog file flows
  use real local file-system metadata only. `dlgdsn` may use source-controlled
  fixtures for validated dialog descriptions. No database, no external service,
  and no user history persistence are planned.
- **Testing**: MSTest-based in-process smoke tests in
  `tests/TuiVision.Examples.SmokeTests/`, focused Controls/Serialization tests
  only if a missing framework behavior blocks an example, full repository
  `dotnet test`, Coverlet coverage evidence, `dotnet format --verify-no-changes`,
  and conditional DocFX + web A11Y smoke checks when API/docs output changes
- **Target Platform**: Managed terminal UI examples on macOS as the primary
  local workflow, with Linux and Windows/WSL compatibility evidence where
  practical
- **Project Type**: Multi-project .NET solution with reusable framework modules,
  console example applications, smoke-test project, guides, and proof documents
- **Performance Goals**: Example smoke interactions must be deterministic local
  event-loop flows with no unbounded background work. File-list examples may
  enumerate local metadata only for the selected directory/filter. Progress
  examples must advance predictably to completion or cancellation without
  timing-dependent assertions.
- **Constraints**: Preserve wave ordering; no wave-3/4 example counts toward
  wave-2 acceptance; no editor/help/stream/terminal-emulation/runtime-mouse/real
  charset effect scope; no file content I/O in standard-dialog acceptance;
  keyboard-first and text-first flows; learner-facing guides are
  DE-first/EN-second at CEFR-B2; before implementation-phase build/test
  commands, increment the build counter in `Directory.Build.props` according to
  repository versioning rules
- **Scale/Scope**: 11 new example projects, 11 new example guides, smoke
  coverage for all 15 delivered examples after completion, updates to
  `examples/README.md`, `TuiVision.sln`, `Pflichtenheft.md`,
  `docs/project-statistics.md`, and required architecture/security/A11Y evidence

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Branching and PR flow**: Pass. Work remains on numbered Spec-Kit branch
  `011-port-wave2-examples`; before commit/push, keep `Version`,
  `AssemblyVersion`, and `FileVersion` aligned to `1.11.<patch>.<build>` and
  follow the existing Draft-PR-before-implementation workflow if a PR is opened.
- **Level-2 environment**: Pass. Uses the `RiderProjects/TuiVision` registry
  context: .NET 10 / C# terminal UI framework, `dotnet restore/build/test`,
  MSTest, Coverlet, `dotnet format`, DocFX + Playwright/axe when docs output is
  regenerated, Multi-Mac primary workflow, statistics baselines of 80
  experienced-developer lines/day and 125 Thorsten-solo lines/day.
- **.NET 10 + C# 14.0 toolchain alignment**: Pass. New examples use existing
  `Directory.Build.props` defaults and do not pin divergent language/runtime
  settings in individual projects.
- **Memory-safe languages (MSL)**: Pass. Primary implementation language is C#,
  which is on the constitution MSL allow-list.
- **Secure code generation**: Pass with task-level verification. Generated code
  must validate file paths/dialog descriptions, avoid exposing internal state in
  user-facing errors, and avoid tracking secrets, logs, agent state, or local
  runtime history.
- **Secure software architecture**: Pass. Trust boundaries are local file-system
  metadata and persisted/dialog-description fixtures. No web/API/auth/service
  boundary is introduced.
- **Architecture/layer boundaries**: Pass. Example projects may consume
  `TuiVision.Controls`, `TuiVision.Serialization`, and driver abstractions, but
  they must not duplicate reusable framework responsibilities when an existing
  control/dialog/service surface exists.
- **General architecture governance**: Pass with required evidence. This feature
  affects runtime behavior, interfaces through examples, quality attributes,
  and technical debt for example readiness; create/update lightweight evidence
  under `docs/architecture/` for context/vision, runtime flows, quality
  scenarios, and architecture risks. ADRs are required only for new cross-
  cutting decisions discovered during implementation.
- **Architecture evidence**: Pass with planned artifacts. Expected evidence:
  `docs/architecture/architecture-vision.md`,
  `docs/architecture/runtime-view.md`,
  `docs/architecture/quality-scenarios.md`,
  `docs/architecture/architecture-risks.md`, plus
  `docs/architecture/adr/` only if a new decision is needed.
- **Bilingual CEFR-B2 documentation scope**: Pass. Each new guide under
  `docs/guides/examples/` is DE-first/EN-second and readable at CEFR-B2.
- **XML documentation + DocFX regeneration scope**: Pass. Public API additions
  or XML-comment changes require complete bilingual XML docs; DocFX is
  regenerated when API output, generated documentation, or DocFX navigation
  changes, followed by the matching web A11Y smoke check.
- **Red-Green-Refactor testing scope**: Pass. Tasks must start with failing or
  missing smoke/test evidence for each example family, then implement the
  minimal required behavior and refactor only within touched boundaries.
- **Coverage gate**: Pass with planned validation. Repository gate remains
  >=70% line coverage per required assembly with >=80% target. Example smoke
  tests are required proof but do not replace module coverage evidence.
- **NuGet dependency currency and pinning exceptions**: Pass. No new NuGet
  dependency is planned. If implementation discovers a dependency need, it must
  be justified, current, pinned through normal project conventions, and reflected
  in supply-chain evidence.
- **Serialization/data conventions**: Pass. `dlgdsn` uses existing
  `TuiVision.Serialization`/resource primitives for any persisted fixture;
  no new JSON/external format stack is planned unless justified by a later ADR.
- **Security documentation**: Pass with proportional evidence. Existing
  `docs/security/` evidence files are used. Update `supply-chain-evidence.md`
  if dependencies or release outputs change; update
  `zero-trust-applicability.md` only if trust boundaries widen. `NIST SSDF` and
  `CWE Top 25` remain applicable; `OWASP ASVS` is N/A because no web/API/auth
  service is introduced.
- **Release / supply-chain evidence**: Pass. No SBOM/VEX/SLSA feature-local
  artifact is required unless a releasable artifact/dependency change is
  introduced during implementation.
- **Inclusion/A11Y**: Pass. Terminal examples, smoke output, guides, and any
  generated docs require text-first/keyboard-first review; WCAG 2.2 AA applies
  to generated HTML documentation when changed.
- **Cross-platform governance**: Pass as N/A for script parity. This feature is
  not expected to add Bash/PowerShell tool scripts. If scripts are introduced,
  they must be planned as paired Bash + PowerShell variants with help/man/parity
  evidence.
- **Statistics**: Pass. `docs/project-statistics.md` must be updated after
  implementation using the documented baselines and newest ledger-entry order.
- **Agent guidance parity**: Pass. Active feature context changes require a
  multi-agent context refresh for Codex, Claude, Gemini, and Copilot after this
  plan run. Manual guidance-file edits are only required if the generated
  refresh or later implementation changes active guidance/technology context.

**Post-Design Gate Review**: Phase-1 artifacts keep work inside existing
solution structure, do not introduce external services or dependencies, keep
`sdlg`/`sdlg2` scoped to scrollable dialogs, and preserve the mandatory
architecture/security/A11Y evidence paths. No constitution exception is
required.

## Project Structure

### Documentation (this feature)

```text
specs/011-port-wave2-examples/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── wave2-example-acceptance.md
├── checklists/
│   ├── requirements.md
│   ├── plan-quality.md
│   └── plan-review.md
└── tasks.md                 # created later by /speckit-tasks
```

### Source Code (repository root)

```text
examples/
├── Clipboard/
├── Demo/
├── DlgDsn/
├── DynTxt/
├── InpLis/
├── ListVi/
├── ProgBa/
├── Sdlg/
├── Sdlg2/
├── TCombo/
└── TProgB/

tests/
└── TuiVision.Examples.SmokeTests/
    ├── ClipboardSmokeTests.cs
    ├── DemoSmokeTests.cs
    ├── DlgDsnSmokeTests.cs
    ├── DynTxtSmokeTests.cs
    ├── InpLisSmokeTests.cs
    ├── ListViSmokeTests.cs
    ├── ProgBaSmokeTests.cs
    ├── SdlgSmokeTests.cs
    ├── Sdlg2SmokeTests.cs
    ├── TComboSmokeTests.cs
    └── TProgBSmokeTests.cs

docs/
├── guides/examples/
│   ├── clipboard.md
│   ├── demo.md
│   ├── dlgdsn.md
│   ├── dyntxt.md
│   ├── inplis.md
│   ├── listvi.md
│   ├── progba.md
│   ├── sdlg.md
│   ├── sdlg2.md
│   ├── tcombo.md
│   └── tprogb.md
├── architecture/
│   ├── architecture-vision.md
│   ├── runtime-view.md
│   ├── quality-scenarios.md
│   ├── architecture-risks.md
│   └── adr/                 # only if new ADRs are needed
└── security/
    ├── asvs-verification.md
    ├── arc42-security.md
    ├── dependency-audit.md
    ├── samm-assessment.md
    ├── security-checklist.md
    ├── security-quality-scenarios.md
    ├── supply-chain-evidence.md
    ├── threat-model.md
    └── zero-trust-applicability.md
```

**Structure Decision**: Add one project per wave-2 example under `examples/`,
extend the existing in-process smoke-test project, and keep all reusable control
or serialization behavior in existing `src/` modules when framework work is
needed. Do not create a new example framework, new persistence stack, database,
or scripting subsystem.

## Phase 0 Research Summary

See [research.md](research.md). Key decisions:

1. Use one managed project per historical example for traceability.
2. Extend the existing in-process smoke-test style and require one
   example-specific deterministic interaction per new example.
3. Keep `sdlg` and `sdlg2` complete for historical scrollable-dialog behavior
   in wave 2; record anything broader as Historical Example Parity Cleanup.
4. Let `demo` and `dlgdsn` carry standard-dialog/dynamic-dialog proof.
5. Keep file-content I/O, editor/help, terminal emulation, real charset effects,
   stream behavior, and runtime mouse behavior out of wave-2 acceptance.
6. Reuse existing Serialization/resource primitives for `dlgdsn` fixtures.
7. Treat architecture, security, A11Y, statistics, and guide updates as part of
   completion, not follow-up cleanup.

## Phase 1 Design Overview

- Example projects expose a normal console entry point plus a headless or
  deterministic test surface aligned with wave-1 patterns.
- Smoke tests instantiate examples in process, trigger one example-specific
  interaction, verify visible state, and assert clean completion.
- `Demo` is broad but bounded: controls, dialogs, and gadget flows only.
- `DlgDsn` proves structured dialog description creation/load, render, one
  simple change, visible rejection for malformed, incomplete, duplicate-control,
  and invalid-navigation descriptions, and optional persisted fixture through
  existing serialization.
- `Sdlg`/`Sdlg2` prove scrollable dialog containers and do not own file/color/
  display/charset standard-dialog acceptance.
- Guides, `examples/README.md`, `Pflichtenheft.md`, `docs/project-statistics.md`,
  and architecture/security/A11Y evidence are updated in the same implementation
  phase.

## Wave-2 Checklist Traceability

| Pflichtenheft item | Project path | Smoke proof | Guide path | Primary acceptance proof |
|-------------------|--------------|-------------|------------|--------------------------|
| `clipboard` - Zwischenablage-Integration in Controls | `examples/Clipboard/` | `ClipboardSmokeTests.cs` | `docs/guides/examples/clipboard.md` | Copy/cut/paste, input-state update, unavailable or isolated clipboard state |
| `demo` - Vollstaendige Turbo-Vision-Kerndemo | `examples/Demo/` | `DemoSmokeTests.cs` | `docs/guides/examples/demo.md` | Broad wave-2 controls/dialogs/gadget integration with later-wave behavior documented as omitted |
| `dlgdsn` - Dialog-Designer | `examples/DlgDsn/` | `DlgDsnSmokeTests.cs` | `docs/guides/examples/dlgdsn.md` | Structured dialog description create/load, render, one simple change, invalid-description rejection |
| `dyntxt` - Dynamisch erzeugter Text in Views | `examples/DynTxt/` | `DynTxtSmokeTests.cs` | `docs/guides/examples/dyntxt.md` | Dynamic text update inside constrained view bounds |
| `inplis` - Eingabelisten mit `TInputLine` | `examples/InpLis/` | `InpLisSmokeTests.cs` | `docs/guides/examples/inplis.md` | Input/list/history synchronization with keyboard navigation |
| `listvi` - Listenansichten mit `TListViewer` | `examples/ListVi/` | `ListViSmokeTests.cs` | `docs/guides/examples/listvi.md` | Visible selection movement and boundary navigation |
| `progba` - Einfacher Fortschrittsbalken | `examples/ProgBa/` | `ProgBaSmokeTests.cs` | `docs/guides/examples/progba.md` | Deterministic progress through completion |
| `sdlg` - Scrollbarer Dialog | `examples/Sdlg/` | `SdlgSmokeTests.cs` | `docs/guides/examples/sdlg.md` | Historical vertical `ScrollDialog`/`ScrollGroup` behavior |
| `sdlg2` - Erweiterter scrollbarer Dialog | `examples/Sdlg2/` | `Sdlg2SmokeTests.cs` | `docs/guides/examples/sdlg2.md` | Historical horizontal and vertical `ScrollDialog`/`ScrollGroup` behavior |
| `tcombo` - Kombinationsfelder | `examples/TCombo/` | `TComboSmokeTests.cs` | `docs/guides/examples/tcombo.md` | Combo-box selection and input synchronization |
| `tprogb` - Erweiterter Fortschrittsbalken mit Abbruch | `examples/TProgB/` | `TProgBSmokeTests.cs` | `docs/guides/examples/tprogb.md` | Progress plus abort path with visible canceled state |

Every row must also appear in `examples/README.md`, the final wave-2 section of
`Pflichtenheft.md`, and the project statistics entry before wave 2 is accepted.

## Interaction-Family Mapping

| Interaction family | Planned examples | Required visible proof |
|--------------------|------------------|------------------------|
| Clipboard | `clipboard` | Copy, cut, paste, input state, and unavailable or isolated clipboard handling |
| List/input/history | `inplis`, `listvi` | Keyboard navigation, selection movement, synchronized input/history/list state, empty and boundary content |
| Combo box | `tcombo` | Keyboard selection, synchronized input value, visible selected value, empty and boundary choices |
| Progress | `progba`, `tprogb` | Deterministic completion for `progba`; abort and visible canceled state for `tprogb`, without wall-clock assertions |
| Dynamic text | `dyntxt` | Predictable text/parameter update inside constrained bounds, including short and long values |
| Scrollable dialogs | `sdlg`, `sdlg2` | Vertical scrolling for `sdlg`; horizontal and vertical scrolling for `sdlg2`; focus and visible state stay deterministic |
| Standard dialogs | `demo`, `dlgdsn`, or another historically justified wave-2 flow | Local metadata, filters, manual path entry, cancellation, invalid paths, color/display/validation where represented, no file-content I/O |
| Dynamic dialog design | `dlgdsn` | Structured description create/load, render, one simple change, visible rejection of malformed, incomplete, duplicate-control, and invalid-navigation descriptions |
| Broad integration | `demo` | A coherent wave-2 controls/dialogs/gadget flow with editor/help/stream/terminal/mouse/charset behavior excluded or documented |

## Testing Strategy

- Add smoke tests for all 11 new examples in
  `tests/TuiVision.Examples.SmokeTests/`.
- Update `TuiVision.Examples.SmokeTests.csproj` with project references for all
  wave-2 example projects.
- Preserve existing wave-1 smoke tests and ensure final smoke coverage includes
  15 delivered examples.
- Add focused Controls/Serialization tests only when missing framework behavior
  is required for a wave-2 example.
- Run validation after implementation:
  `dotnet build --configuration Release`, `dotnet test`,
  `dotnet test --collect:"XPlat Code Coverage"`,
  `dotnet format --verify-no-changes`, plus conditional `docfx docfx.json` and
  `tests/web-a11y` smoke tests when docs output changes.

## Success-Criteria Traceability

| Success Criterion | Plan Evidence |
|------------------|---------------|
| SC-001 | One runnable project and one deterministic smoke interaction per wave-2 example |
| SC-002 | Eleven new guide files under `docs/guides/examples/`, raising total guides from 4 to 15 |
| SC-003 | Smoke-test project references all 15 delivered examples |
| SC-004 | Contract and smoke tests cover clipboard, list/input/history, combo, progress, dynamic text, scrollable dialogs, standard dialogs, dynamic dialog design, and demo integration |
| SC-005 | `Pflichtenheft.md`, `examples/README.md`, guides, smoke tests, and project statistics cross-reference each wave-2 item |
| SC-006 | Next-step marker moves to wave 3 only after implementation proof is complete |
| SC-007 | Contract and plan exclude wave-3/4 examples from wave-2 completion |
| SC-008 | Guides and any generated HTML docs include text-first/WCAG path evidence |
| SC-009 | `sdlg`/`sdlg2` proof marks historical ScrollDialog/ScrollGroup completion and separates broader parity cleanup |

## Complexity Tracking

No constitution violations or complexity exceptions are planned.
