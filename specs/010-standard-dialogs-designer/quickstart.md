# Quickstart: Standard Dialogs and Designer Readiness

## Purpose

Use this quickstart to validate the `010-standard-dialogs-designer` plan before
task generation and again after implementation. The feature is accepted through
framework-level Controls and Serialization evidence, not through full wave-2
example ports.

## Prerequisites

- Worktree on branch `010-standard-dialogs-designer`
- `.specify/feature.json` points to `specs/010-standard-dialogs-designer`
- .NET 10 SDK available
- Existing `TuiVision.Controls` and `TuiVision.Serialization` test projects
- Multi-Mac workflow remains the primary local validation path

## Planned Validation Flow

1. Confirm scope boundaries:
   - no full `demo`, `sdlg`, `sdlg2`, or `dlgdsn` port in this feature
   - no additional "comparable wave-2 selections" beyond the planned file,
     directory, color, symbolic charset, display, and dialog-description
     responsibility classes
   - no terminal rendering/font/emulation implementation
   - no runtime mouse requirement
   - no file content I/O inside standard dialogs
   - no cross-session history persistence

2. Run focused Controls validation after implementation:

   ```bash
   dotnet test tests/TuiVision.Controls.Tests/
   ```

   Expected proof:
   - file open/select/save-target decision flows
   - directory navigation and directory select decision flows
   - current directory, filter, selected/manual path, metadata, history, and
     result synchronization
   - empty filter/list, invalid path, unreadable metadata, stale entry, and
     save-target validation outcomes
   - color/display/symbolic-charset selection with confirm/cancel behavior
   - keyboard-only completion of all acceptance-critical flows
   - downstream consumer classification for `demo`, `sdlg`, `sdlg2`, and
     `dlgdsn`
   - planned test targets are named as planned evidence until `tasks.md`
     assigns concrete create/update tasks

3. Run focused Serialization validation after implementation:

   ```bash
   dotnet test tests/TuiVision.Serialization.Tests/
   ```

   Expected proof:
   - minimal persisted dialog-description roundtrip
   - runtime-only state excluded from persisted form
   - malformed/truncated persisted input rejection
   - unsupported version rejection
   - duplicate control ID and duplicate command-binding rejection
   - semantic invalid-description rejection before runtime dialog creation

4. Run repository validation:

   ```bash
   dotnet build --configuration Release
   dotnet test
   dotnet test --collect:"XPlat Code Coverage"
   dotnet format --verify-no-changes
   ```

5. If public APIs or XML comments change, regenerate and validate docs:

   ```bash
   docfx docfx.json
   cd tests/web-a11y
   npm run test:docfx
   ```

6. Record evidence:
   - update `docs/project-statistics.md`
   - update `docs/guides/multi-mac-workflow.md` if validation workflow changes
   - record Linux and Windows/WSL compatibility evidence when practical
   - rename `Lastenheft_02_StandardDialogsAndDesigner.md` to
     `Lastenheft_02_StandardDialogsAndDesigner.010-standard-dialogs-designer.md`
     after the implementation is complete

## Expected Outcomes

- Standard dialogs expose reusable decision flows for file, directory, color,
  symbolic charset, and display selection.
- Dialogs remain fully keyboard-operable.
- File dialogs return decisions and never perform file content I/O.
- History remains session-scoped.
- Dialog descriptions validate uniqueness and semantic rules before runtime
  creation.
- Persisted dialog descriptions roundtrip minimally and reject malformed input.
- Wave-2 examples are ready to consume shared framework behavior later without
  duplicating infrastructure.
