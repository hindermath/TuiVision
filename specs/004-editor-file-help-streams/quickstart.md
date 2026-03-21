# Quickstart: Editor, File, Help, and Stream Components

## Goal

Validate the planned phase-6 increment by exercising one complete editor workflow, one file-dialog/history workflow, one runtime help workflow, and one stream/resource persistence workflow.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `004-editor-file-help-streams`

## Planned Validation Flow

1. Write failing MSTest cases for:
   - editor document mutation and command-state behavior
   - safe-close decision handling for modified buffers versus save-conflict handling for overwrite cases
   - file-dialog synchronization and history bucket scoping
   - manual path entry and wildcard-filter refresh behavior inside file dialogs
   - line-ending preservation and external file-change conflict handling
   - dedicated help-file loading, fallback lookup, and cross-reference navigation
   - shared-reference stream behavior, truncated/trailing/unknown-type rejection, unsupported cycle rejection, and case-sensitive resource keys

2. Implement the minimal production types in `src/TuiVision.Controls` and `src/TuiVision.Serialization`.

3. Run the mandatory quality gates before merge:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Controls.Tests/
dotnet test tests/TuiVision.Serialization.Tests/
dotnet test
dotnet format --verify-no-changes
dotnet test --collect:"XPlat Code Coverage"
```

4. If public APIs or XML comments changed, run the conditional documentation gate:

```bash
docfx docfx.json
```

## Representative Usage Sketch

**Interpretation note**: The example below is illustrative only. It shows the kind of runtime flow the plan must support, but it does not freeze final member names or signatures unless the contract states them explicitly.

```csharp
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Serialization;

public sealed class DemoEditorWorkflow
{
    public void Run(TRect bounds, string helpPath)
    {
        var editor = new TFileEditor(bounds, fileName: "notes.txt");
        var helpFile = THelpFile.Load(helpPath);
        var helpWindow = new THelpWindow(helpFile, context: 1000);

        // Open editor, launch file dialog when needed, and show runtime help.
        // Save operations preserve loaded line endings and confirm overwrite
        // when the file changed externally.
    }
}
```

## Expected Outcomes

- A file-backed editor can load, edit, and save a real file without silently changing its original line endings.
- A modified document cannot be closed or replaced without an explicit discard or save decision.
- New files default to `LF`.
- If the file changed on disk during the session, save requires an explicit overwrite decision.
- File dialogs keep browsing state, wildcard filters, typed path, and history bucket recall synchronized.
- Runtime help loads from a dedicated help file, supports cross-reference navigation, and falls back safely for missing contexts.
- Named resources can be stored and reloaded with exact case-sensitive keys.
- Truncated, trailing, unknown-type, or cyclic persisted payloads fail explicitly.
