# Copilot Instructions for TuiVision

TuiVision is a C#/.NET 10 port of the Turbo Vision 2.0.3 TUI framework (originally C/C++). It is a managed, modernized interpretation — not a line-for-line translation.

## Build, Test, and Lint

```bash
# Full validation cycle
dotnet restore
dotnet build --configuration Release
dotnet test

# Test a specific project
dotnet test tests/TuiVision.Core.Tests/

# Run a single test method
dotnet test --filter "FullyQualifiedName~MethodName"

# Check formatting
dotnet format --verify-no-changes
```

Coverage Gate (SC-003): `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization` must each achieve at least 70% line coverage. Measure with Coverlet via `dotnet test --collect:"XPlat Code Coverage"`.

CI runs on Ubuntu and macOS against .NET 10. Linux and Windows/WSL compatibility checks should be added or expanded where changes affect runtime behavior, terminal handling, or portability. The `tv203s/` directory is **excluded from all builds and tests**. CI triggers on pushes to `main`, `master`, and branches matching `codex/**`, `claude/**`, `gemini/**`, `copilot/**`, `opencode/**`.

## Architecture

All projects target `net10.0` with `Nullable: enable`, `ImplicitUsings: enable`, and `LangVersion: latest` via `Directory.Build.props`.

| Module | Purpose |
|---|---|
| `src/TuiVision.Core` | Foundation: `TObject`, `TPoint`, `TRect`, `TEvent` |
| `src/TuiVision.Controls` | UI components: `TView` (base for all visual elements) |
| `src/TuiVision.Drivers.Console` | Console rendering: `TConsoleCell`, `TConsoleBuffer`, `TConsoleDriver`, `IConsolePresenter` |
| `src/TuiVision.Serialization` | Binary archive system: `TBinaryArchiveWriter/Reader`, `TRecordRegistry`, `TRecordSerializer` |
| `src/TuiVision.Compatibility` | Key code translation: `TKeyCodeTranslator`, `TShiftState` |
| `tests/` | MSTest projects mirroring each `src/` module |
| `examples/` | Ported example programs; `TuiVision.Examples.SmokeTests` covers integration-level tests |
| `tv203s/contrib/tvision/` | Original C/C++ source — **read-only reference, never modify** |

> **Note:** `src/TuiVision.Drivers.Console`, `src/TuiVision.Serialization`, and `src/TuiVision.Compatibility` currently store all their code in `Class1.cs` — a scaffold artifact. New types in those modules go in that file until it is split.

### Key design patterns

- **Event system**: `TEvent` is created only via static factory methods (`TEvent.CreateKeyDown(...)`, `TEvent.CreateCommand(...)`, etc.). Events are consumed via `TView.HandleEvent()`. Call `event.Clear()` to mark an event as handled.
- **Coordinate system**: `TRect` uses **inclusive top-left (`A`), exclusive bottom-right (`B`)**. `TView` maintains local coordinates; use `MakeLocal`/`MakeGlobal` to convert.
- **Lifecycle**: Override `TObject.ShutDown()` for logical teardown. Use `TObject.Destroy(instance)` (not `new` + GC) to shut down and dispose an object together.
- **Console rendering**: Presenter pattern — `TConsoleDriver` manages a back-buffer (`TConsoleBuffer`) and publishes immutable snapshots via `IConsolePresenter`. Use `TrySetCell` for bounds-safe writes; `WriteText` accepts `ReadOnlySpan<char>` and clips automatically.
- **Serialization**: Envelope-based binary format — `TRecordSerializer` writes `(typeId string, payloadLength int32, payload bytes)`. New serializable types implement `ITStreamSerializable` and register a factory with `TRecordRegistry.Register<T>(typeId, reader => ...)`.
- **Key translation**: `TKeyCodeTranslator.ComposeKeyCode` encodes Turbo Vision key codes as `(scanCode << 8) | charCode`. Named constants (e.g., `KeyEnter = 0x1C0D`) are on `TKeyCodeTranslator`.

## Conventions

- **No native dependencies**: No P/Invoke or native interop. All drivers must be pure managed code.
- **Value types**: Use `readonly record struct` for immutable payloads (e.g., `TMouseEvent`, `TKeyDownEvent`). Use `struct` (mutable) for geometry types like `TPoint` and `TRect` that match original Turbo Vision mutation semantics.
- **Flags enums**: Use `[Flags]` enums (e.g., `TEventKind`, `TViewState`, `TViewOptions`) matching original Turbo Vision bitmask values.
- **JSON handling**: Use `System.Text.Json` for project-owned JSON parsing and serialization. Introduce `Newtonsoft.Json` only with documented justification and explicit reviewer approval.
- **XML documentation**: All public APIs require `<summary>`, `<param>`, and `<returns>` XML comments. Explanatory documentation blocks must be **bilingual: German first, English second**, both at CEFR-B2 readability. Update docs in the same commit as the API change.
- **Test naming**: `ClassName_MethodName_Behavior` (e.g., `TRect_Contains_UsesTopLeftInclusiveBottomRightExclusive`).
- **Branch naming**: Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch.
- **Porting guidance**: Consult `tv203s/contrib/tvision/` for original behavior when porting new classes. The C# port modernizes idioms — it does not translate line-for-line.

## Active Feature Context

### 004-editor-file-help-streams
- Align active work with `specs/004-editor-file-help-streams/spec.md` and the planning artifacts in `specs/004-editor-file-help-streams/`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes
- Keep this increment scoped to reusable framework components only; example applications such as `tvedit`, `bhelp`, and `helpdemo`, as well as driver consolidation and calculator/macros/OS-shell integrations, are out of scope

### 005-driver-consolidation-m07
- Align active work with `specs/005-driver-consolidation-m07/spec.md` and the planning artifacts in `specs/005-driver-consolidation-m07/`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure
- Keep this increment scoped to driver consolidation and proof preparation only; mandatory example waves and complete Phase-8 gate closure remain out of scope
- Phase-7 implementation is complete: `DriverCapabilityMap.cs` with 5 capability buckets, `docs/porting-status.md` covering all 151 `.cc` files, 30 driver tests passing, compatibility evidence in `docs/guides/multi-mac-workflow.md`, gate checklist in `checklists/phase-8-gate-review.md`; next priority is Phase-8 gate closure (Core/Controls/Serialization coverage each ≥ 70 %, full dotnet test suite)

### 006-close-phase8-gate
- Align active work with `specs/006-close-phase8-gate/spec.md` and the planning artifacts in `specs/006-close-phase8-gate/`
- Scope is limited to final `M-07` proof closure plus the remaining Phase-8 entrance evidence across `docs/porting-status.md`, `Pflichtenheft.md`, the existing Core/Controls/Serialization test suites, coverage evidence, formatting evidence, and API-documentation validation
- Every historical `.cc` ledger row must finish in `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no `portiert + Test ausstehend` row may remain after closure is claimed
- Gate closure must include explicit build, full-test, coverage, formatting, and conditional API-doc proof, and must keep the 25 mandatory example waves blocked until the closure is formally recorded
- `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Serialization` must each satisfy the hard 70 % line-coverage gate before Phase 8 is declared open
- The planning baseline now also fixes repository-wide `dotnet test`, conditional Linux/Windows/WSL evidence, and a dedicated gate-closure commit as hard closure criteria
- Keep this increment scoped to gate closure only; mandatory example waves, substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, and unrelated new framework features remain out of scope

## Agent File Synchronization Policy

- When active feature context, plan-derived implementation guidance, or other shared AI-agent instructions change, review and update these files together when affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
- Shared guidance must not be updated in only one of these files.
- Any intentional agent-specific divergence must be called out explicitly in the same change.

## Project Statistics

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Each update must capture branch/phase, observable work window, production/test/documentation line counts, main work packages, and the conservative manual baseline of 80 code lines per day for an experienced developer.

## Workflow Platforms

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `gemini` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Pflichtenheft Next-Step Marker

- Maintain a prominent `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md`.
- The marker MUST point to the currently highest-priority open work item in the prioritized rest-work section and MUST be moved whenever progress changes the effective next step.
