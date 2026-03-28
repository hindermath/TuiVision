# Quickstart: Mandatory Example Wave 1 Ports

## Goal

Validate the planned first mandatory example wave by porting `desklogo`,
`msgcls`, all 16 `tutorial` steps, and `videomode`, then proving the result
through example-focused smoke tests, guide pages, and synchronized project
tracking artifacts.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `007-port-wave1-examples`
- Historical reference sources available under `tv203s/contrib/tvision/examples`
- Existing framework baseline from phases 1 to 7 available in `src/`

## Planned Validation Flow

1. Write failing MSTest smoke scenarios in
   `tests/TuiVision.Examples.SmokeTests/` for:
   - `desklogo`
   - `msgcls`
   - `videomode`
   - each tutorial step `tvguid01` through `tvguid16`

2. Commit the red state separately for each story: the failing smoke tests must
   compile, fail for the intended reason, and land in a reviewable commit
   before the first production implementation for that story is committed.

3. Add the managed example projects under `examples/`:
   - `examples/Desklogo/`
   - `examples/MsgCls/`
   - `examples/Tutorial/`
   - `examples/Videomode/`

4. Implement the minimal example behavior needed to turn the new smoke tests
   green while reusing the existing framework modules instead of duplicating
   framework logic in the examples, then commit that green state separately
   before any follow-up refactor commit.

5. Add the guide surfaces in `docs/guides/examples/`:
   - `desklogo.md`
   - `msgcls.md`
   - `tutorial.md`
   - `videomode.md`

6. Walk each delivered guide from a clean checkout and confirm that a reviewer
   can reach the primary documented outcome in 5 minutes or less without hidden
   prerequisites; if not, revise the guide or the example before wave sign-off.

7. Update the project-tracking artifacts once the wave is delivered:
   - `Pflichtenheft.md`
   - `docs/project-statistics.md`

8. Before any `dotnet build` or `dotnet test` run on this numbered Spec-Kit
   branch, align `Version`, `AssemblyVersion`, and `FileVersion` in
   `Directory.Build.props` to the repository rule `1.7.Patch.Build`, with
   `Patch` equal to the feature-branch commit count after the pending commit
   and `Build` incremented manually before each build or test invocation.

9. If implementation changes shared agent guidance, active technologies, or
   project structure, update the synchronized agent-guidance surfaces in the
   same work item:
   - `AGENTS.md`
   - `CLAUDE.md`
   - `GEMINI.md`
   - `.github/copilot-instructions.md`
   - `.github/agents/copilot-instructions.md`

10. Run the mandatory validation commands before merge:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/
dotnet test
dotnet format --verify-no-changes
```

11. If public APIs or XML comments changed in the framework modules, run the
   conditional documentation gate:

```bash
docfx docfx.json
```

12. Capture runtime evidence on the primary and compatibility platforms:
   - validate on `MacBook Air M2`
   - validate on `Mac mini M4 Pro`
   - validate on Linux
   - validate on Windows/WSL with current Ubuntu, preferably `24.04`

## Representative Launch Sketch

**Interpretation note**: The commands below fix the intended launch surface for
planning purposes. Internal helper names may still change during
implementation, but the user-facing example identity must remain equivalent.

```bash
dotnet run --project examples/Desklogo
dotnet run --project examples/MsgCls
dotnet run --project examples/Tutorial -- tvguid01
dotnet run --project examples/Tutorial -- tvguid16
dotnet run --project examples/Videomode
```

## Expected Outcomes

- Four managed wave-1 example deliveries exist under `examples/`.
- `tutorial` exposes all 16 original steps through stable selector tokens and
  has individual smoke coverage for each step.
- `videomode` performs a real supported change where possible and otherwise
  presents an explicit visible fallback.
- `tests/TuiVision.Examples.SmokeTests/` proves startup, defining behavior, and
  clean exit for the delivered wave-1 examples.
- `docs/guides/examples/` contains one guide page per example scope, with one
  shared guide page for the full tutorial sequence.
- Each guide can be followed from a clean checkout to the documented primary
  outcome in 5 minutes or less, without relying on hidden setup knowledge.
- `Pflichtenheft.md` and `docs/project-statistics.md` reflect the delivered
  wave-1 progress instead of leaving the repository status ambiguous.
- Until those implementation updates land, the current `Pflichtenheft.md` and
  `docs/project-statistics.md` states are treated as pre-wave baseline context,
  not partial delivery evidence for feature 007.
