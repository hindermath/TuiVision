# Quickstart: Application Framework Shell

## Goal

Validate the planned implementation of the application shell increment by building a minimal application that starts with a default shell, exposes a shared global command through menu and status line, and keeps a valid focus target even when no desktop child is open.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `002-application-framework`

## Planned Validation Flow

1. Write failing MSTest cases for:
   - default `TApplication` shell creation
   - menu/status command equivalence
   - disabled command visibility
   - desktop focus fallback after child removal

2. Implement the minimal shell types in `src/TuiVision.Controls`.

3. Run the core validation commands:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Controls.Tests/
dotnet test
dotnet format --verify-no-changes
```

4. If public APIs or XML comments changed, regenerate documentation:

```bash
docfx docfx.json
```

## Representative Usage Sketch

**Interpretation note**: The example below is illustrative only. It demonstrates the kind of customization seam the plan must support, but it does not lock the final public API to these exact method names or signatures.

```csharp
using TuiVision.Controls;
using TuiVision.Core;

public sealed class DemoApplication : TApplication
{
    public DemoApplication(TRect bounds) : base(bounds)
    {
    }

    protected override void BuildMenuBar()
    {
        // Define shell-level menu actions here.
    }

    protected override void BuildStatusLine()
    {
        // Define matching status actions here.
    }
}
```

## Expected Outcomes

- A default `TApplication` instance creates menu bar, desktop, and status line automatically.
- The same global command behaves identically from menu and status line.
- Disabled actions remain visible and cannot execute.
- Removing the active desktop child leaves the shell in a valid interactive state.
- If no eligible desktop child remains, focus falls back to the desktop workspace itself.
