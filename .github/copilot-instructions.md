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

CI runs on Ubuntu and macOS against .NET 10. The `tv203s/` directory is **excluded from all builds and tests**. CI triggers on pushes to `main`, `master`, and branches matching `codex/**`, `claude/**`, `gemini/**`, `copilot/**`, `opencode/**`.

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
- **Branch naming**: Feature branches follow `codex/<feature-description>`.
- **Porting guidance**: Consult `tv203s/contrib/tvision/` for original behavior when porting new classes. The C# port modernizes idioms — it does not translate line-for-line.

## Active Feature Context

### 003-dialog-control-layer
- Align active work with `specs/003-dialog-control-layer/plan.md`
- Implement 13 new classes in `src/TuiVision.Controls` in dependency order: `TStringList` → `TScrollBar` → `TScroller` → `TStaticText` → `TCluster` → `TCheckBoxes` → `TRadioButtons` → `TLabel` → `TListViewer` → `TListBox` → `TButton` → `TInputLine` → `TDialog`
- `TDialog.Run()` is synchronously blocking (inner event loop); it returns a `ushort` command ID
- Tab/Shift-Tab focus navigation in `TDialog` is wrap-around (circular child list inherited from `TGroup`)
- `TButton` supports `TButtonFlags.bfDefault` — activated by Enter when the focused control does not consume Enter; sets `TViewState.Default` (0x400)
- `TScrollBar` is optional (nullable) for `TListBox` and `TListViewer`
- `TCluster` is abstract; `TCheckBoxes` uses a bitmask `uint Value`; `TRadioButtons` uses an index `uint Value`
- All 13 classes require complete bilingual XML documentation (German first, English second, CEFR-B2) for every member including non-public ones
- Follow TDD Red-Green-Refactor with separate commits: test (Red) before implementation (Green)
- Coverage gate: `TuiVision.Controls` ≥ 70% line coverage after all 13 classes are added

### 002-application-framework
- Align active work with `specs/002-application-framework/plan.md`
- Implement the shell increment in `src/TuiVision.Controls`, centered on `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, `TStatusLine`, and shared shell command identifiers
- Preserve the existing module hierarchy; do **not** introduce a new shell assembly
- Reuse existing `TView`/`TGroup` semantics for ownership, focus, and event dispatch
- `TApplication` must create a default shell automatically: menu bar, desktop workspace, and status line
- Unavailable global actions must remain visible in both menu and status line, but be disabled
- Keep this increment scoped to shell infrastructure only; dialogs, controls, and specialized window classes belong to later steps
- Add or extend MSTest coverage in `tests/TuiVision.Controls.Tests/` before production code to preserve the repository's TDD-first workflow

## Agent File Synchronization Policy

- When active feature context, plan-derived implementation guidance, or other shared AI-agent instructions change, review and update these files together when affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
- Shared guidance must not be updated in only one of these files.
- Any intentional agent-specific divergence must be called out explicitly in the same change.
