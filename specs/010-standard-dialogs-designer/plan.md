# Implementation Plan: Standard Dialogs and Designer Readiness

**Branch**: `010-standard-dialogs-designer` | **Date**: 2026-05-02 | **Spec**: [spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/010-standard-dialogs-designer/spec.md)
**Input**: Feature specification from `/specs/010-standard-dialogs-designer/spec.md`

## Summary

Harden the reusable standard-dialog and dialog-designer surface required before
the wave-2 examples `demo`, `sdlg`, `sdlg2`, and `dlgdsn` are ported. The
feature strengthens existing file/directory dialog state synchronization,
session-scoped history, color/charset/display selection, and keyboard-only
operation in `TuiVision.Controls`, and adds a small validated dialog-description
roundtrip with malformed-input rejection through the existing
`TuiVision.Serialization` boundary. It does not port the wave-2 examples, does
not perform file content I/O inside dialogs, and does not reopen terminal,
font, mouse, editor, or help scope.

## Terminology & Operational Definitions

- **Standard dialog flow**: A reusable dialog journey that owns visible choices,
  validation, cancellation, confirmation, and a returned decision.
- **File decision result**: The explicit open/select/save-target outcome from a
  file-oriented dialog. It never performs file loading, writing, deletion, or
  overwrite operations.
- **Session-scoped dialog history**: History entries available only during the
  active application session, aligned with `009-controls-widgets-and-collections`.
- **Symbolic charset choice**: A dialog-level choice value for `sdlg`/`sdlg2`
  traceability. It does not alter terminal rendering, fonts, buffers, or
  emulation behavior.
- **Dialog description**: The design-time representation for `dlgdsn`, including
  controls, labels, navigation order, command bindings, initial values, and
  validation constraints.
- **Minimal persisted-description roundtrip**: Store and reload one validated
  dialog description through the existing serialization/resource boundary,
  while excluding runtime-only state and rejecting malformed persisted input.
- **Framework-first acceptance**: Required proof lives primarily in
  `tests/TuiVision.Controls.Tests` plus `tests/TuiVision.Serialization.Tests`
  for persisted dialog descriptions; wave-2 examples remain downstream
  consumers.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling  
**Storage**: In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage  
**Testing**: MSTest-first validation in `tests/TuiVision.Controls.Tests` and `tests/TuiVision.Serialization.Tests`; focused acceptance tests for keyboard-only dialog flows, synchronized file state, color/charset/display selection, dialog-description validation, malformed persisted input, and framework/example-consumer classification; full repository validation via `dotnet build --configuration Release`, targeted tests, `dotnet test`, coverage collection, `dotnet format --verify-no-changes`, and conditional DocFX + web A11Y smoke check  
**Target Platform**: Managed cross-platform terminal UI on macOS, Linux, and Windows/WSL, with Multi-Mac development (`MacBook Air M2`, `Mac mini M4 Pro`) as primary workflow  
**Project Type**: Managed .NET library/framework increment in existing Controls and Serialization modules, with test and proof-artifact expansion  
**Performance Goals**: Standard-dialog navigation, validation, selection updates, and cancel/confirm outcomes must complete within a normal single event-loop interaction for local terminal use; persisted dialog-description validation must fail before runtime dialog creation for invalid input  
**Constraints**: No new source module; no example-port delivery in this feature; full keyboard operation is acceptance-critical; mouse remains optional; file dialogs return decisions only and do not perform file content I/O; history is active-session-only; symbolic charset choices do not affect terminal rendering/font/emulation; minimal persisted dialog-description roundtrip only; German-first/English-second documentation rules; numbered-branch version governance before implementation-phase builds/tests  
**Scale/Scope**: Harden existing `TuiVision.Controls` and `TuiVision.Serialization` surfaces in place; add focused tests and proof notes; update `docs/project-statistics.md` and rename `Lastenheft_02_StandardDialogsAndDesigner.md` to `Lastenheft_02_StandardDialogsAndDesigner.010-standard-dialogs-designer.md` when the implementation is complete

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Level-2 environment**: Pass. This plan cites the `RiderProjects/TuiVision`
  Level-2 row: .NET 10 / C# terminal UI framework; `dotnet restore/build/test`,
  MSTest, Coverlet, `dotnet format`; DocFX changes require Playwright + axe and
  text-browser-oriented review; statistics use manual `80` lines/workday and
  C#/.NET Thorsten-Solo `125`; synchronized agent surfaces include `AGENTS.md`,
  `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`,
  `.github/agents/copilot-instructions.md`, and Spec-Kit surfaces.
- **Memory-safe languages (MSL)**: Pass. Primary implementation language is C#,
  which is on the constitution MSL allow-list.
- **Secure code generation**: Pass with task-level validation. Any generated C#
  code must follow Microsoft/.NET secure-coding guidance, safe deserialization
  defaults, explicit validation for file paths and persisted input, and no
  leakage of stack traces or internal state through user-facing dialog errors.
- **Secure software architecture**: Pass. Trust boundaries are local file-system
  metadata/path input and persisted dialog-description input. Both are validated
  before state acceptance or runtime dialog creation; dialogs do not perform
  file content I/O.
- **Security documentation**: Pass with no new mandatory document expected for
  this planning phase. Existing Level-2 requirements still apply; tasks should
  record security checklist review for file-path validation and persisted-input
  parsing. New S-ADRs or threat-model updates are only required if planning or
  implementation widens trust boundaries beyond local files/resource input.
- **Security standards applicability**: Pass. `NIST SSDF` and `CWE Top 25`
  apply. `OWASP ASVS` is N/A: no web/API/auth-bearing service. `SBOM` and VEX
  remain release-process obligations, not feature-local deliverables unless a
  release artefact is produced. CAPEC/Zero Trust are N/A for this local library
  increment unless external/service boundaries are introduced. OWASP cheat
  sheets remain developer guidance.
- **Release / supply-chain evidence**: Pass. No new dependencies are planned.
  SBOM/VEX/provenance evidence remains with release workflow if the repository
  ships artefacts after this change.
- **Default evidence files**: Pass. Use `docs/security/` if feature work later
  requires security evidence; no alternate governance location is introduced.
- **Security-first**: Pass. No credential files, agent state, logs, history, or
  SQLite state are planned for tracking. `.opencode/command/` remains the
  already-approved Spec-Kit command surface, not agent runtime state.
- **Inclusion/A11Y**: Pass. Keyboard-only operation is acceptance-critical, and
  proof notes must remain text-first; generated DocFX changes require the
  existing WCAG 2.2 AA-oriented smoke path.
- **Bilingual delivery**: Pass. Public APIs and learner-facing docs require
  German-first/English-second CEFR-B2 documentation where changed.
- **Statistics**: Pass. `docs/project-statistics.md` must be updated when this
  planning/implementation phase is completed.
- **Agent guidance parity**: Pass. This plan refresh affects active feature
  context and therefore requires agent-context refresh for Codex, Claude,
  Gemini, and Copilot.

**Post-Design Gate Review**: Phase-1 artifacts keep work inside existing
Controls and Serialization modules, preserve framework-first acceptance, avoid
new external dependencies, and do not widen into example ports, terminal
emulation, runtime mouse support, editor/help behavior, or file content I/O.
No constitution exception is required.

## Project Structure

### Documentation (this feature)

```text
specs/010-standard-dialogs-designer/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── standard-dialogs-designer-api.md
├── checklists/
│   └── requirements.md
└── tasks.md
```

### Source Code (repository root)

```text
src/
├── TuiVision.Controls/
│   ├── TFileDialog.cs          # existing; harden decision/state contract
│   ├── TFileList.cs            # existing; harden visible entry/filter sync
│   ├── TDirListBox.cs          # existing; harden directory navigation sync
│   ├── TFileInfo.cs            # existing; harden metadata/fallback behavior
│   ├── TFileInputLine.cs       # existing; harden manual path/history sync
│   ├── THistory.cs             # existing; confirm session-only dialog history
│   ├── TColorDialog.cs         # existing; harden composed selection flow
│   ├── TColorSelector.cs       # existing
│   ├── TMonoSelector.cs        # existing
│   ├── TColorGroup.cs          # existing
│   ├── TColorDisplay.cs        # existing
│   └── TDialog.cs / TWindow.cs # existing shell/dialog foundation
├── TuiVision.Serialization/
│   ├── TResourceFile.cs
│   ├── TResourceCollection.cs
│   ├── TRecordRegistry.cs
│   ├── TRecordSerializer.cs
│   ├── TBinaryArchiveReader.cs
│   ├── TBinaryArchiveWriter.cs
│   └── pstream.cs / ipstream.cs / opstream.cs / fpstream.cs
└── TuiVision.Core/
    └── existing event, geometry, and buffer primitives

tests/
├── TuiVision.Controls.Tests/
│   ├── TFileDialogTests.cs
│   ├── TFileListTests.cs
│   ├── TDirListBoxTests.cs
│   ├── TFileInputLineTests.cs
│   ├── THistoryTests.cs
│   ├── TDialogCompositeTests.cs
│   ├── TColorDialogTests.cs        # planned if not already present
│   ├── TStandardDialogFlowTests.cs # planned acceptance slice
│   └── DialogDesignerFlowTests.cs  # planned runtime-description slice
├── TuiVision.Serialization.Tests/
│   ├── TResourceFileTests.cs
│   ├── TRecordCompatibilityTests.cs
│   └── DialogDescriptionPersistenceTests.cs # planned
└── TuiVision.Examples.SmokeTests/
    └── downstream context only; no required new example smoke in this feature
```

**Structure Decision**: Keep the feature inside existing source modules and
test projects. Use Controls for user-facing dialog behavior and Serialization
only for the minimal persisted dialog-description roundtrip. Do not introduce a
new designer assembly, external file format package, database, or example-local
helper stack.

## Research Focus

Phase 0 resolves and locks the following decisions:

1. File and directory dialogs return explicit decisions and never perform file
   content I/O.
2. Session-scoped history from `009` is reused for standard-dialog history.
3. Symbolic charset selection is a dialog value only; terminal rendering, fonts,
   and emulation stay out of scope.
4. Color/display selection is hardened around existing `TColor*` surfaces rather
   than implemented as example-local widgets.
5. Dialog designer support is a validated description model plus a minimal
   persisted-description roundtrip, not a full visual designer subsystem.
6. Persisted dialog descriptions use project-owned serialization/resource
   primitives with explicit malformed-input rejection.
7. Keyboard-only operation is mandatory for acceptance; mouse support remains
   optional.
8. Framework-level Controls/Serialization tests own acceptance; wave-2 examples
   are classified as downstream consumers.

## Phase 0 Research Summary

See [research.md](research.md) for full detail. Key planning decisions:

1. Reuse and harden the existing `TFileDialog` family; do not create a parallel
   file-picker subsystem.
2. Treat file dialog outcomes as decision objects for open/select/save-target
   intent, with no file content I/O inside the dialog.
3. Keep history session-scoped and partitioned by history identifier.
4. Reuse existing `TColorDialog`, selectors, groups, and display types for
   color/display; add symbolic charset choice at the dialog contract level only.
5. Model dialog designer data as validated dialog descriptions with unique
   control identifiers and command bindings.
6. Persist only validated dialog descriptions through the existing
   Serialization/resource boundary, with explicit rejection for malformed,
   truncated, unsupported-version, and semantic errors.
7. Make keyboard-only operation the required interaction proof.
8. Keep example smoke expansion and full wave-2 example ports out of this
   feature.

## Phase 1 Design Overview

- `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, and
  `THistory` form the file decision flow. The dialog coordinates current
  directory, active filter, visible entries, selected/manual path, metadata,
  session-scoped history, validation, and open/select/save-target result.
- `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, and
  `TColorDisplay` form the color/display selection flow. Symbolic charset is a
  returned dialog choice value, not a terminal rendering operation.
- The dialog-designer surface introduces a small `DialogDescription`-style model
  inside the appropriate existing module boundary, with controls, labels,
  navigation order, command bindings, initial values, validation rules, unique
  control IDs, and unique command bindings.
- Serialization persists only the dialog description representation and rejects
  malformed, truncated, unsupported-version, or semantically invalid input
  before runtime dialog creation.
- Tests are framework-first: Controls tests prove interaction, keyboard
  operation, state synchronization, and downstream example classification;
  Serialization tests prove persisted-description roundtrip and rejection cases.

### Responsibility Boundaries

- `TFileDialog` owns dialog state synchronization and returns user intent; it
  does not load, write, delete, or overwrite file content.
- `TFileList` and `TDirListBox` own visible file/directory entries and selection
  movement; they do not own save/open decisions by themselves.
- `TFileInfo` owns displayable metadata and fallback state for missing or
  unreadable metadata.
- `TFileInputLine` owns manual path entry and validation handoff.
- `THistory` owns session-only recall buckets partitioned by history ID.
- `TColorDialog` owns composed color/display/symbolic-charset decisions and
  cancellation/confirmation semantics.
- Dialog-description validation owns uniqueness and semantic checks before any
  runtime dialog is produced.
- `TuiVision.Serialization` owns persisted dialog-description roundtrip and
  malformed input rejection only; it does not own Controls runtime interaction.
- `tests/TuiVision.Examples.SmokeTests` remains downstream context only.

## Scenario & Edge-Case Coverage

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| File open/select decision | User Story 1, FR-001..FR-003a | `data-model.md`, API contract, `TFileDialogTests` |
| Save-target decision without I/O | Clarification, FR-003a | `data-model.md`, API contract, negative Controls tests |
| Empty filter/list and invalid manual path | Edge Cases, FR-003 | Controls dialog tests and quickstart |
| Metadata fallback | Edge Cases, FR-002/FR-003 | `data-model.md`, `TFileInfo` tests |
| Session-scoped history | Clarification, FR-002a | `data-model.md`, `THistoryTests` |
| Color/display selection | User Story 2, FR-004/FR-005 | Controls tests and API contract |
| Symbolic charset choice | Clarification, FR-001/FR-004/FR-012 | Controls tests; terminal work explicitly excluded |
| Keyboard-only operation | Clarification, FR-013, SC-008 | Controls acceptance tests and quickstart |
| Dialog description validation | User Story 3, FR-006/FR-007 | `data-model.md`, `DialogDesignerFlowTests` |
| Persisted-description roundtrip | Clarification, FR-008a, SC-003a | Serialization tests and contract |
| Example consumer classification | FR-009..FR-011, SC-004/SC-007 | proof notes in quickstart and tests |

## Testing Strategy

- **Controls tests**: File dialog synchronization, manual entry, empty filter
  handling, metadata fallback, save-target decisions, session-only history,
  color/display/symbolic-charset selection, cancellation/confirmation, keyboard
  navigation, and framework/example consumer classification.
- **Serialization tests**: Minimal dialog-description roundtrip, runtime-only
  state exclusion, unsupported version, malformed/truncated input, duplicate
  control IDs, duplicate command bindings, unknown control role, invalid
  navigation order, missing label, and unsupported persisted value.
- **Regression tests**: Preserve existing dialog, input, list, scroll, color,
  editor/file phase-6, and 009 widget behavior.
- **Mandatory validation before merge**:
  - `dotnet build --configuration Release`
  - `dotnet test tests/TuiVision.Controls.Tests/`
  - `dotnet test tests/TuiVision.Serialization.Tests/`
  - `dotnet test`
  - `dotnet test --collect:"XPlat Code Coverage"`
  - `dotnet format --verify-no-changes`
- **Conditional validation**:
  - `docfx docfx.json` when public APIs or XML comments change
  - `cd tests/web-a11y && npm run test:docfx` after any DocFX regeneration

## Success-Criteria Traceability

| Success criterion | Planning hook |
|---|---|
| SC-001 | File decision model, `TFileDialog` contract, Controls tests |
| SC-002 | Color/display/charset state model, Controls tests |
| SC-003 | Dialog-description model and validation tests |
| SC-003a | Persisted-description model, Serialization tests |
| SC-004 | Wave-2 consumer classification in quickstart/proof notes |
| SC-005 | Thin-consumer guard checks and downstream classification |
| SC-006 | Text-first proof notes and A11Y-oriented quickstart review |
| SC-007 | Framework-first acceptance strategy |
| SC-008 | Keyboard-only acceptance tests |

## Complexity Tracking

No constitution violations or unjustified complexity are planned.
