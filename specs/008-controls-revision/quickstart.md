# Quickstart: Controls Revision

## Goal

Validate the planned Controls revision by exercising one complete menu
navigation flow, one context-driven status-line flow, one closable/movable
window flow, and one dialog close-validation flow.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `008-controls-revision`
- Primary validation available on the Multi-Mac workflow; Linux and
  Windows/WSL access available or scheduled because runtime-behavior evidence is
  required before feature closure

## Planned Validation Flow

1. Write failing MSTest coverage for:
   - top-level menu activation, wrap-around navigation, submenu opening, and
     selection highlighting
   - skip-over behavior for disabled or separator submenu entries
   - `TSubMenu` declaration compatibility for the historical `tvguid02`-style
     menu-building syntax
   - menu layout recomputation after `Locate()` / resize changes
   - `TStatusDef`-based routing with inclusive ranges, first-match-wins
     behavior, and neutral empty fallback on no match
   - compatibility fallback from focused-view direct hints when no explicit
     status definitions were configured, including current editor-driven callers
     such as `TEditor` and `TEditWindow`
   - closable-window affordance rendering plus guarded `Ctrl+W` / `Escape`
     handling
   - move-mode entry via `Ctrl+F5`, preview movement, `Enter` commit, and
     `Escape` restore
   - dialog close validation for both rejected and accepted close commands

2. Implement the minimal production changes in `src/TuiVision.Controls`.

3. Run the mandatory quality gates before merge:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Controls.Tests/
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test
dotnet format --verify-no-changes
```

4. If public APIs or XML comments changed, run the conditional documentation
   gate:

```bash
docfx docfx.json
```

5. Capture the required runtime-behavior evidence on the primary Multi-Mac path
   and the supplemental Linux / Windows/WSL path before closing the feature,
   including explicit build/test outcomes on those paths and a repeated-run
   result matrix for the `SC-003` window close/move scenarios.

## Representative Usage Sketch

**Interpretation note**: The example below is illustrative only. It shows the
kind of runtime flow the plan must support, but it does not freeze final member
names or signatures unless the contract states them explicitly.

```csharp
using TuiVision.Controls;
using TuiVision.Core;

TMenuItem fileItems =
    new TMenuItem("~O~pen", ShellCommandIds.cmOpen) +
    new TMenuItem("E~x~it", ShellCommandIds.cmQuit);

TSubMenu fileMenu = new("~F~ile", 0, fileItems);

TStatusDef[] defs =
{
    new TStatusDef(0, 0, new TStatusItem("^Q Quit", ShellCommandIds.cmQuit)),
    new TStatusDef(100, 199, new TStatusItem("~F2~ Save", ShellCommandIds.cmSave))
};

TMenuBar menuBar = new(new TRect(0, 0, 80, 1)) { Menu = fileMenu };
TStatusLine statusLine = new(new TRect(0, 24, 80, 25), defs);
TWindow window = new("Demo", 5, 3, 30, 10, WindowFlags.Close | WindowFlags.Move);
```

## Expected Outcomes

- Users can activate menus, wrap through top-level entries and submenu entries,
  skip non-actionable submenu rows, and confirm the selected command without
  relying on mnemonic-only access.
- The status line resolves the first matching `TStatusDef` for the active help
  context and becomes neutral/empty when no definition matches.
- Existing focused-view hint producers can still function when no explicit
  `TStatusDef` configuration is supplied.
- Closable windows expose a visible close affordance and respond to `Ctrl+W`
  and guarded `Escape`.
- Movable windows enter move mode via `Ctrl+F5`, commit position changes with
  `Enter`, and restore the original position with `Escape`.
- Dialogs can reject invalid close requests without leaving modal execution or
  losing in-dialog state.
- The Controls proof surfaces accurately reflect the revised implementation
  state once the feature is implemented.
