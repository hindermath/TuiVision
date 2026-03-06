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
- **XML documentation**: All public APIs require `<summary>`, `<param>`, and `<returns>` XML comments. Explanatory documentation blocks must be **bilingual: German first, English second**, both at CEFR-B2 readability. Update docs in the same commit as the API change.
- **Test naming**: `ClassName_MethodName_Behavior` (e.g., `TRect_Contains_UsesTopLeftInclusiveBottomRightExclusive`).
- **Branch naming**: Feature branches follow `codex/<feature-description>`.
- **Porting guidance**: Consult `tv203s/contrib/tvision/` for original behavior when porting new classes. The C# port modernizes idioms — it does not translate line-for-line.
