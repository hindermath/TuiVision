# Implementation Plan: Editor, File, Help, and Stream Components

**Branch**: `004-editor-file-help-streams` | **Date**: 2026-03-21 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/004-editor-file-help-streams/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/004-editor-file-help-streams/spec.md)
**Input**: Feature specification from `/specs/004-editor-file-help-streams/spec.md`

**Note**: This plan covers the phase-6 framework increment only: editor views and editor-host windows, file dialogs and history recall, runtime help consumption from a dedicated help file, compatibility stream primitives, and named resource persistence. Example ports such as `tvedit`, `bhelp`, and `helpdemo` remain out of scope for this planning step.

## Summary

Implement the phase-6 framework slice across `TuiVision.Controls` and `TuiVision.Serialization`. The design keeps user-facing editing, file, history, and help interaction inside `TuiVision.Controls`, while stream primitives, type registration, resource containers, and help-file persistence live in `TuiVision.Serialization`. The implementation intentionally preserves conceptual Turbo Vision behavior without requiring byte-compatible legacy file formats, reuses the current managed binary archive foundation where practical, and validates the increment through MSTest-first coverage split between Controls and Serialization test projects plus focused editor/file/help integration slices.

## Terminology & Operational Definitions

- **Document session**: One active editor workflow that combines buffer content, modification state, file metadata snapshot, and shell-visible command availability.
- **Explicit overwrite decision**: A save-path branch in which the editor has detected either an existing target replacement or an external file change and does not overwrite on-disk content until the user chooses to continue.
- **History bucket**: The ordered in-memory recall list associated with one history identifier. Two fields share history only when they use the same identifier.
- **Dedicated help file**: The persisted source loaded by `THelpFile` for runtime help lookup. It is distinct from the generic named resource container, even if both use shared stream infrastructure.
- **Concept compatibility**: The port preserves the original framework's observable responsibilities, lookup behavior, object-reference semantics, and navigation rules without promising byte-for-byte compatibility with Turbo Vision files.
- **Shared reference identity**: When the same logical object is persisted multiple times in one supported object graph, deserialization restores a shared instance rather than independent copies. Cyclic graphs remain out of acceptance scope.
- **Supporting host frame**: A narrow framed/titled hosting abstraction used to support `TEditWindow` and `THelpWindow` without broadening this increment into a full general-purpose windowing subsystem.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation validation  
**Storage**: Real local file system plus persisted binary help/resource files; no database storage  
**Testing**: MSTest unit and integration-style coverage in `tests/TuiVision.Controls.Tests` and a new `tests/TuiVision.Serialization.Tests`; full repository validation via `dotnet build --configuration Release`, `dotnet test`, and `dotnet format --verify-no-changes`  
**Target Platform**: Cross-platform terminal applications on macOS, Linux, and Windows using managed .NET APIs only  
**Project Type**: Managed .NET library framework with file-backed editing and persistence support  
**Performance Goals**: Editing, cursor movement, scrolling, history recall, and help-topic navigation remain single-interaction-cycle operations for local terminal workflows; file save conflict detection occurs before overwrite and without extra hidden background passes  
**Constraints**: No native dependencies; preserve current five-module source architecture; keep phase scope limited to reusable framework components; runtime help must load from a dedicated help file; loaded files preserve original line endings while new files default to `LF`; external file changes require explicit overwrite decisions; streams preserve shared references but do not support cyclic object graphs; resource keys are case-sensitive; public and non-public members require bilingual documentation updates; coverage gate remains ≥70% for `TuiVision.Controls` and `TuiVision.Serialization`  
**Scale/Scope**: New editor/file/help UI types in `src/TuiVision.Controls`, new stream/resource/help persistence types in `src/TuiVision.Serialization`, new Controls and Serialization tests, no new source modules, and no example-port delivery in this increment

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. All planned work remains inside the managed .NET runtime; file and stream access uses standard .NET stream and file abstractions with no native bindings.
- **Test-First Development — TDD**: Pass with explicit workflow requirement. Tasks must begin with failing MSTest coverage for editor behavior, file-dialog flows, help navigation, stream/resource edge cases, and serialization error handling before production code is added.
- **Didactic and Linguistic Clarity**: Pass. New and changed public and non-public members in Controls and Serialization require German-first/English-second XML documentation, plus design artifacts that explain feature boundaries and trade-offs.
- **Modular Architecture**: Pass. User-facing UI stays in `TuiVision.Controls`; persistence and stream infrastructure stays in `TuiVision.Serialization`; both depend only on allowed lower-level modules.
- **Cross-Platform Portability**: Pass. Real file-system access uses managed .NET file APIs, and no OS-specific code is planned for Controls or Serialization.
- **License & Disclaimer Integrity**: Pass. No changes are planned under `tv203s/`; new implementation files must keep the repository's MIT header convention.

**Post-Design Gate Review**: Phase 1 artifacts keep the feature inside existing modules, add no new source assembly, and avoid constitution exceptions. The only structural expansion is a dedicated `tests/TuiVision.Serialization.Tests` project, which mirrors an existing source module and does not violate the five-module source rule.

- Statistical-documentation impact identified; update `docs/project-statistics.md` after this planning change is written.

## Project Structure

### Documentation (this feature)

```text
specs/004-editor-file-help-streams/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── public-api.md
├── checklists/
│   └── requirements.md
└── tasks.md
```

### Source Code (repository root)

```text
src/
├── TuiVision.Core/
│   ├── TConsoleBuffer.cs
│   ├── TEvent.cs
│   ├── TObject.cs
│   ├── TPoint.cs
│   └── TRect.cs
├── TuiVision.Controls/
│   ├── TView.cs
│   ├── TGroup.cs
│   ├── TApplication.cs
│   ├── TDesktop.cs
│   ├── TDialog.cs
│   ├── TInputLine.cs
│   ├── TListBox.cs
│   ├── TScroller.cs
│   ├── TButton.cs
│   ├── ShellCommandIds.cs
│   ├── TEditor.cs               # planned
│   ├── TMemo.cs                 # planned
│   ├── TFileEditor.cs           # planned
│   ├── TEditWindow.cs           # planned
│   ├── TIndicator.cs            # planned support type
│   ├── TFileDialog.cs           # planned
│   ├── TFileInputLine.cs        # planned
│   ├── TFileList.cs             # planned
│   ├── TDirListBox.cs           # planned
│   ├── THistory.cs              # planned
│   ├── THelpViewer.cs           # planned
│   ├── THelpWindow.cs           # planned
│   └── Internal/
│       └── framed-host helper   # planned if required
├── TuiVision.Serialization/
│   ├── Class1.cs                # existing archive/registry foundation
│   ├── PStream*.cs              # planned compatibility stream layer
│   ├── TResourceFile.cs         # planned
│   ├── TResourceCollection.cs   # planned
│   ├── THelpTopic.cs            # planned
│   ├── THelpIndex.cs            # planned
│   └── THelpFile.cs             # planned
└── TuiVision.Compatibility/
    └── Class1.cs

tests/
├── TuiVision.Controls.Tests/
│   ├── TDialogTests.cs
│   ├── TInputLineTests.cs
│   ├── TListBoxTests.cs
│   ├── TButtonTests.cs
│   ├── TEditorTests.cs          # planned
│   ├── TMemoTests.cs            # planned
│   ├── TFileEditorTests.cs      # planned
│   ├── TEditWindowTests.cs      # planned
│   ├── TFileDialogTests.cs      # planned
│   ├── TDirListBoxTests.cs      # planned
│   ├── TFileListTests.cs        # planned
│   ├── THistoryTests.cs         # planned
│   ├── THelpViewerTests.cs      # planned
│   └── THelpWindowTests.cs      # planned
├── TuiVision.Serialization.Tests/   # planned new test project
│   ├── PStreamTests.cs              # planned
│   ├── TResourceFileTests.cs        # planned
│   ├── THelpFileTests.cs            # planned
│   └── TRecordCompatibilityTests.cs # planned
├── TuiVision.Core.Tests/
└── TuiVision.Examples.SmokeTests/
```

**Structure Decision**: Keep the feature inside the existing repository structure and existing source modules. `TuiVision.Controls` owns interactive editor/file/help UI, while `TuiVision.Serialization` owns stream, resource, and help-file persistence. The current `Class1.cs` scaffold in Serialization is too coarse for this increment, so the plan permits splitting that module into focused files without changing module boundaries.

### Planned Artifact Status

- `plan.md` is the integration artifact: it fixes scope boundaries, module ownership, quality gates, traceability expectations, and the review-ready implementation sequence.
- `research.md` is the decision artifact: it records which planning alternatives were chosen, why they were chosen, and which alternatives were rejected so later tasks do not reopen the same scope questions implicitly.
- `data-model.md` is the behavior-shaping artifact: it defines entities, validation rules, and state transitions for editor sessions, file dialogs, help navigation, streams, and resources.
- `contracts/public-api.md` is the observable-behavior artifact: it describes public responsibilities and guarantees without freezing final private helper names or internal member layouts.
- `quickstart.md` is the validation artifact: it defines the intended reviewer/implementer walkthrough, mandatory quality gates, and conditional documentation regeneration steps.
- `TEditor`, `TMemo`, `TFileEditor`, and `TEditWindow` are required editor deliverables because they realize the central user workflow from the specification.
- `TIndicator` is treated as a supporting editor artifact, not as a separate feature track.
- `TFileDialog`, `TFileInputLine`, `TFileList`, `TDirListBox`, and `THistory` are required file-flow artifacts because the specification requires synchronized browsing, manual entry, wildcard filtering, and scoped history recall.
- `THelpViewer` and `THelpWindow` are UI-facing help artifacts; `THelpTopic`, `THelpIndex`, and `THelpFile` are persistence/model artifacts.
- `pstream`, `ipstream`, `opstream`, `fpstream`, `TResourceFile`, and `TResourceCollection` are required compatibility and persistence artifacts for this increment because shared-reference streams and named resources are explicitly in scope.
- A narrow framed-host helper may be introduced if needed to avoid duplicating frame/title logic between `TEditWindow` and `THelpWindow`; this is an enabling implementation detail, not a commitment to a broad general window subsystem.

## Phase 0 Research Summary

See `research.md` for full detail. Key planning decisions:

1. Keep editor/file/help interaction in `TuiVision.Controls` and move stream/resource/help-file persistence into `TuiVision.Serialization`.
2. Implement compatibility stream primitives as a higher-level layer on top of the existing binary archive and registry foundation instead of creating a separate unmanaged-style serializer stack.
3. Preserve shared references inside supported persisted object graphs while rejecting cyclic graphs explicitly for this phase.
4. Use a dedicated help-file model in Serialization and let `THelpViewer`/`THelpWindow` consume it at runtime.
5. Scope history recall by history identifier through an internal bucketed store.
6. Detect external file changes through a persisted file snapshot and require an explicit overwrite decision before save.
7. Preserve loaded line endings and default new files to `LF`.
8. Use exact case-sensitive resource keys for lookup, replacement, removal, and enumeration.
9. Support framed editor/help hosts through a narrow reusable host abstraction instead of broadening the phase into a full windowing subsystem.

## Phase 1 Design Overview

- `TEditor` becomes the reusable multi-line editing surface responsible for buffer mutation, cursor motion, scrolling, selection, search/replace orchestration hooks, shell-visible command availability, and explicit safe-close decision routing when modified content would otherwise be discarded.
- `TMemo` specializes `TEditor` for in-memory text workflows that do not need file-system attachment.
- `TFileEditor` adds file loading/saving, line-ending tracking, file snapshot tracking, and explicit overwrite-decision hooks for target replacement and external modification conflicts.
- `TEditWindow` hosts a `TFileEditor` plus optional indicator/status elements in a framed, non-modal desktop-compatible shell view.
- `TFileDialog`, `TFileInputLine`, `TFileList`, `TDirListBox`, and `THistory` form the file-selection workflow. The dialog coordinates current directory, wildcard filter, selected entry, typed path, and history bucket recall.
- `THelpTopic`, `THelpIndex`, and `THelpFile` model dedicated runtime help persistence in `TuiVision.Serialization`; `THelpViewer` and `THelpWindow` consume that model to display topics, scroll content, and navigate cross-references.
- `pstream`, `ipstream`, `opstream`, and `fpstream` form a compatibility stream layer on top of the existing archive primitives and registry. This layer adds object-reference tracking, file-backed seek/tell behavior, and strict malformed-input rejection without promising legacy byte compatibility.
- `TResourceFile` and `TResourceCollection` provide case-sensitive named persistence built on the compatibility stream layer.
- The plan adds `tests/TuiVision.Serialization.Tests` so stream, help-file, and resource behavior can be validated independently from UI tests while preserving the constitution's per-module testing expectations.

### Responsibility Boundaries

- `TEditor` owns editing behavior and document-state transitions; it does not own real file I/O or persisted help/resource concerns.
- `TFileEditor` owns file attachment, line-ending preservation, and external-change conflict detection; it does not own directory browsing UX.
- `TFileDialog` owns the selection workflow and returns an explicit user decision, but it does not perform the actual document save/load itself.
- `THistory` owns recall interaction for one linked field; the shared history store owns bucket partitioning by history identifier.
- `THelpFile` owns dedicated help-file loading and topic lookup; it does not render UI.
- `THelpViewer` owns on-screen help navigation and cross-reference activation; it does not author or rewrite help files.
- `pstream`-family types own stream semantics such as reference tracking, seek/tell, and primitive/object reads and writes.
- `TRecordSerializer`, `TBinaryArchiveReader`, and `TBinaryArchiveWriter` remain the lower-level binary foundation; they are reused rather than discarded.
- `TResourceFile` owns named object persistence with exact case-sensitive key semantics.

### Customization Boundary for This Increment

- The increment must expose editor/file/help components as reusable framework types rather than a single monolithic demo app.
- Public API contracts describe responsibilities and observable guarantees, not final private helper names.
- A narrow framed-host helper is allowed if it remains internal or tightly scoped to editor/help windows.
- Porting example applications, generalized macro support, calculator integration, and OS shell invocation remain explicitly out of scope.

## Implementation Strategy

1. Add failing MSTest coverage for editor buffer behavior, file-dialog synchronization, history scoping, help navigation, stream malformed-input rejection, resource key semantics, and external file-change handling.
2. Expand `TuiVision.Serialization` with focused files for the compatibility stream layer, help-file model, and resource persistence while retaining the current archive/registry primitives as the binary foundation.
3. Implement editor and file-flow UI types in `TuiVision.Controls`, starting with core editor behavior and then layering file dialogs, history, and framed hosts.
4. Implement help persistence and help UI together so dedicated help files can be exercised end to end in runtime navigation tests.
5. Refactor helper boundaries, documentation, and naming only after the new tests pass and the line-ending/conflict/reference semantics are stable.
6. Run build, test, format, coverage, and doc generation checks required by the constitution when public APIs change.

## Scenario & Edge-Case Coverage

### Scenario Matrix

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| Editor typing/search/close flow | User Story 1 | `plan.md` summary, `data-model.md` document session, `contracts/public-api.md`, `quickstart.md` |
| File-dialog open/save flow | User Story 2 | `research.md`, `data-model.md` file dialog session, `contracts/public-api.md`, Controls integration tests |
| Help topic lookup and cross-reference navigation | User Story 3 | `research.md`, `data-model.md` help entities, `contracts/public-api.md`, help runtime tests |
| Stream/resource persistence flow | User Story 4 | `research.md`, `data-model.md` stream/resource entities, `contracts/public-api.md`, Serialization tests |
| Unsaved-close decision flow | User Story 1 + FR-002 | `data-model.md` document lifecycle, `contracts/public-api.md`, editor/file close tests |
| Manual path entry and wildcard-filter update flow | User Story 2 + FR-006 / FR-007 | `data-model.md` file dialog rules, `contracts/public-api.md`, dialog synchronization tests |
| Empty editor / long line / save target failure | Edge Cases | `data-model.md` document state rules, editor/file tests |
| Missing help context / no cross references | Edge Cases | `data-model.md` help state transitions, help tests |
| Case-sensitive resource keys | Edge Cases / FR-014a | `research.md`, `data-model.md`, Serialization tests |
| External file modification conflict | Clarification + Edge Cases | `data-model.md` file snapshot state, `contracts/public-api.md`, file-editor tests |
| Cyclic graph rejection / malformed input rejection | Edge Cases / FR-012 / FR-013 | `research.md`, `data-model.md`, Serialization negative tests |
| Recovery after declined overwrite | Recovery flow | `data-model.md` document lifecycle, `contracts/public-api.md`, file-editor recovery tests |
| Portability and coverage quality gates | Non-functional scope | `plan.md` technical context, testing strategy, `quickstart.md` |

### Reviewer Readiness Criteria

- Reviewers must be able to point to a written artifact for each of these behaviors before tasks are generated:
  - editor document mutation and command-state exposure
  - file-dialog synchronization and history bucket scoping
  - dedicated help-file loading and runtime topic navigation
  - shared-reference stream behavior and cyclic-graph exclusion
  - case-sensitive resource key behavior
  - line-ending preservation and external file-change conflict handling
- If any of those behaviors are only implied and not explicitly described in at least one design artifact plus one validation-oriented artifact, the plan is not review-ready.

## Testing Strategy

- **Controls unit tests**: Editor buffer mutation, cursor/viewport behavior, search and replace entry points, history recall scoping, file-dialog synchronization, help-view navigation, framed-host close behavior, and shell command-availability updates.
- **Serialization unit tests**: Primitive read/write behavior, reference-table behavior, malformed-input rejection, seek/tell semantics, help-file topic lookup, resource key exact matching, replacement/removal, and cyclic-graph rejection.
- **Integration-style tests**: `TFileEditor` load/save lifecycle, unsaved-close decision path, external file-change conflict path, line-ending preservation on save, help-file-to-help-view runtime navigation, and editor/dialog interaction under the shell.
- **Regression tests**: Preserve existing dialog, list, input, scroll, and shell behavior by reusing current controls rather than regressing phase-3 or phase-4 expectations.
- **Negative serialization cases**: Truncated payloads, trailing data, unknown type identifiers, duplicate/invalid registrations, and unsupported cycles must each have explicit failing tests rather than being grouped under one generic malformed-input check.
- **Mandatory validation commands before merge**:
  - `dotnet build --configuration Release`
  - `dotnet test tests/TuiVision.Controls.Tests/`
  - `dotnet test tests/TuiVision.Serialization.Tests/`
  - `dotnet test`
  - `dotnet format --verify-no-changes`
  - `dotnet test --collect:"XPlat Code Coverage"`
- **Conditional validation command**:
  - `docfx docfx.json` when public APIs or XML comments changed
- **Coverage gate interpretation**: Repository policy requires at least 70% line coverage for `TuiVision.Controls`. This feature plan keeps that merge gate and additionally sets the same 70% target for `TuiVision.Serialization` because this increment adds a new persistence subsystem whose acceptance would otherwise be weakly measurable.

### Success-Criteria Traceability

| Success criterion | Planning hook |
|---|---|
| `SC-001` create/open/edit/save-or-close in one editor session | document session state model, file-editor integration tests, quickstart workflow |
| `SC-002` open/save-target selection within limited interactions | file-dialog session model, history scoping decision, controls tests |
| `SC-003` help opens valid or invalid context responsively | help-file model, help-view contract, runtime help tests |
| `SC-004` named resources enumerate and reload with expected keys | resource catalog model, case-sensitive key rule, Serialization tests |
| `SC-005` malformed/incomplete data rejected explicitly | compatibility stream design, malformed-input/cyclic-graph negative tests |

## Non-Functional Operationalization

- **Portability**: File and stream logic use managed .NET APIs only; no OS-specific code is introduced in Controls or Serialization.
- **Documentation completeness**: New Controls and Serialization members require bilingual documentation in the same change as implementation.
- **TDD discipline**: Tasks must begin with failing tests for editor behavior, file conflict handling, help navigation, and persistence error cases before production code is added.
- **Performance interpretation**: "Single-interaction-cycle" means local editing/navigation actions complete within the same event loop or synchronous method flow without deferred background processing.
- **No hidden persistence expansion**: The feature may read and write local files plus dedicated help/resource files, but it must not introduce JSON, database, or network persistence.
- **Deferred non-functional areas**: Observability, security hardening, and formal numeric performance thresholds are intentionally deferred from this phase unless a later task set promotes them explicitly.

## Dependencies & Assumptions

- The current phase-3/4 controls (`TDialog`, `TInputLine`, `TListBox`, `TScroller`, `TButton`, `TDesktop`, `TProgram`) are assumed stable enough to host editor/file/help UI without revisiting their core contracts.
- `TuiVision.Serialization` already provides deterministic primitive archive helpers and a registry; the plan assumes these can serve as the low-level substrate for the higher-level compatibility stream layer.
- A new `tests/TuiVision.Serialization.Tests` project is assumed acceptable and necessary because the repository currently lacks dedicated serialization coverage.
- If implementation shows that a generic `TWindow` abstraction is unavoidable, that expansion must be documented explicitly before tasks are widened beyond the narrow framed-host helper described in this plan.
- Real file-system integration assumes the build/test environment can create and clean up temporary files safely using managed APIs.
- Cross-platform file-system behavior is assumed to be normalized through managed path APIs rather than hard-coded path separators, and conflict detection must tolerate timestamp granularity differences by checking both last-write metadata and file length.
- Temporary-file and metadata-based tests are assumed to run inside repository-safe or OS temp locations without requiring elevated privileges or long-lived file locks.

## Traceability Matrix

| Spec reference | Planned coverage |
|---|---|
| `FR-001` / `FR-002` | summary, design overview, document session model, `contracts/public-api.md` editor contract |
| `FR-003` | `TMemo` design boundary, data model document session without file attachment |
| `FR-004` / `FR-004a` / `FR-004b` | file snapshot model, line-ending rules, conflict-detection strategy, file-editor tests |
| `FR-005` | framed-host support, `TEditWindow` responsibility boundary |
| `FR-006` / `FR-007` | file dialog session model, synchronization rules, controls tests |
| `FR-008` | history bucket model, scoped recall strategy, contracts and tests |
| `FR-009` / `FR-010` / `FR-011` / `FR-011a` | dedicated help-file model, help-view runtime contract, help navigation tests |
| `FR-012` / `FR-013` | compatibility stream design, malformed-input and cyclic-graph rejection tests |
| `FR-014` / `FR-014a` | resource catalog model, exact key matching rule, Serialization tests |
| `FR-015` | scope notes, structure decision, reviewer readiness criteria |
| `SC-001`–`SC-005` | testing strategy and success-criteria traceability table |

## Risks & Mitigations

- **Risk**: Stream compatibility work duplicates the existing archive layer.
  **Mitigation**: Keep `TBinaryArchiveReader/Writer`, `TRecordRegistry`, and `TRecordSerializer` as the binary substrate; layer compatibility semantics on top instead of replacing them.

- **Risk**: Editor-host windows force a premature generic window subsystem.
  **Mitigation**: Start with a narrow framed-host helper scoped to `TEditWindow` and `THelpWindow`; escalate only if tests prove a broader abstraction is unavoidable.

- **Risk**: File conflict handling becomes platform-fragile.
  **Mitigation**: Base conflict detection on managed file metadata snapshots and verify behavior with temporary-file integration tests.

- **Risk**: Help persistence and generic resource persistence blur together.
  **Mitigation**: Keep dedicated help-file loading as a separate contract from generic resource containers, even if both reuse the same stream primitives.

- **Risk**: Shared-reference support introduces subtle serializer bugs.
  **Mitigation**: Constrain scope to acyclic graphs, add explicit negative tests for cycles and malformed payloads, and keep reference-table behavior localized to the compatibility stream layer.

- **Risk**: The feature sprawls into example-port work.
  **Mitigation**: Keep quickstart and contracts focused on reusable framework types only; example ports remain downstream consumers, not deliverables of this increment.

## Complexity Tracking

No constitution violations or exceptional complexity justifications are currently required.
