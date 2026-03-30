# Quickstart: Controls Widgets and Collections

## Goal

Validate the planned widget and collections feature by exercising one complete
list/scroller flow, one session-scoped history/clipboard flow, one editable
combo-box flow, one determinate progress flow, and one bounded parameter-text
flow inside the Controls test suite.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `009-controls-widgets-and-collections`
- Primary validation available on the Multi-Mac workflow; Linux and
  Windows/WSL access available or scheduled because runtime-behavior evidence is
  required before feature closure

## Planned Validation Flow

1. Write failing MSTest coverage for:
   - list-viewer, list-box, scroll-bar, and scroller synchronization,
     especially empty collections, single-item collections, and undersized
     bounds
   - session-scoped `THistory` behavior, duplicate suppression, and recall
     order
   - application-internal managed clipboard behavior for empty, filled, and
     replaced clipboard states
   - editable combo-box behavior with visible drop-down opening, navigation,
     selection, and retained typed text
   - determinate numeric progress updates for running, completed, and canceled
     states
   - parameterized text refresh and clipping under changing values and shrinking
     bounds

2. Implement the minimal production changes in `src/TuiVision.Controls`.

3. Before the quality gates, align `Directory.Build.props` to the current
   numbered-branch version/build-counter state required for `009`.

4. Run the mandatory quality gates before merge:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Controls.Tests/
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test
dotnet format --verify-no-changes
```

5. If public APIs or XML comments changed, run the conditional documentation
   gate:

```bash
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

6. Capture the required runtime-behavior evidence on the primary Multi-Mac path
   and the supplemental Linux / Windows/WSL path before closing the feature,
   including explicit build/test outcomes on those paths and a repeated-run
   result matrix for representative widget scenarios.

7. Keep explicit traceability from the framework acceptance slice to the later
   consuming examples `clipboard`, `dyntxt`, `inplis`, `listvi`, `progba`,
   `tcombo`, and `tprogb` reviewable in the acceptance artifacts.

8. Complete the required repository follow-through before closing the feature:
   update `docs/project-statistics.md` and rename
   `Lastenheft_01_ControlsWidgetsAndCollections.md` to
   `Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md`.

## Representative Usage Sketch

**Interpretation note**: The example below is illustrative only. It shows the
kind of runtime flow the plan must support, but it does not freeze final member
names or signatures unless the contract states them explicitly.

```csharp
using TuiVision.Controls;
using TuiVision.Core;

TStringList choices = new(["Alpha", "Beta", "Gamma"]);
TComboBox combo = new(new TRect(1, 1, 21, 2), 20, choices, "combo-history");

combo.Text = "Al";
combo.OpenDropDown();
combo.SelectIndex(0);

TProgressBar progress = new(new TRect(1, 3, 31, 4), 0, 100);
progress.SetValue(25);

TParamText paramText = new(new TRect(1, 5, 31, 7), "Value: {0}");
paramText.SetValues(progress.Value);
```

## Expected Outcomes

- List-driven controls keep active item, visible range, and linked scroll state
  synchronized across redraws.
- Session-scoped history recall remains MRU-ordered and does not require
  persistence across program restarts.
- Managed clipboard behavior works inside the application even when no
  operating-system clipboard integration exists.
- The combo box supports both typed text and visible drop-down selection.
- The progress surface exposes determinate numeric running, completed, and
  canceled states.
- Parameterized text refreshes and clips output to bounds without leaking stale
  content.
- The primary acceptance proof for these guarantees lives in
  `tests/TuiVision.Controls.Tests`; later example smoke coverage remains deferred
  to wave-2 delivery features.
- No acceptance artifact in this feature requires runtime mouse input or
  terminal-side mouse-event capture.
- Traceability remains reviewable for the downstream consumers `clipboard`,
  `dyntxt`, `inplis`, `listvi`, `progba`, `tcombo`, and `tprogb`.
