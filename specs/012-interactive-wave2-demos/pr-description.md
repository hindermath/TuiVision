# Interactive Wave 2 Demos

## Summary

- Makes all eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
- Adds queued app-loop smoke proof through `InteractiveSmokeEventScript`, `DirectHelperUsage`, and one primary app-loop scenario per Wave-2 example.
- Updates German-first/English-second example guides, `examples/README.md`, architecture/security evidence, project statistics, agent guidance, `Pflichtenheft.md`, and `pr-evidence.md`.

## User-Visible Runtime Snippets

```text
Demo: Wave-2 controls/dialogs showcase
Commands: broad, metadata, manual path, cancel, invalid path, color/display
Use the Demo menu or scripted command events. Ctrl+Q quits.
```

```text
ProgBa: deterministic progress completion
Command completes the progress bar. Ctrl+Q quits.
```

```text
DlgDsn: load, render, change, and reject dialog descriptions
Commands use source-controlled fixtures only. Ctrl+Q quits.
```

## Validation

- `dotnet build --configuration Release`
- `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` -> 73/73 passed
- `dotnet test --configuration Release` -> 478/478 passed
- `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings`
- Coverage gate: `Core` 89.78 %, `Controls` 84.84 %, `Serialization` 87.95 %, `Compatibility` 80.55 %, `Drivers.Console` 81.70 %
- `dotnet format --verify-no-changes`
- `docfx docfx.json`
- `npm run test:docfx` -> 2/2 Playwright/Axe tests passed
- Manual startup checks for all eleven Wave-2 examples with `dotnet run --project examples/<Name> --configuration Release --no-build`
- `git diff --check`

## Security And Scope

No new runtime dependency, database, external service, user-data write path, or persisted user history was added. File/path and dialog-designer proof remains read-only: Demo uses metadata only, DlgDsn uses source-controlled allow-listed fixtures, and InpLis history remains in memory.

No Wave-3 or Wave-4 example, mouse-only path, terminal/charset work, or broad framework redesign was added.
