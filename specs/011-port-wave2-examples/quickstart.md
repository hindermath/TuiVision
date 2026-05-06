# Quickstart: Port Wave 2 Examples

## Purpose

Use this quickstart to validate the `011-port-wave2-examples` plan before task
generation and again after implementation. The feature is accepted through
eleven managed example projects, deterministic smoke tests, didactic guides, and
repository-visible proof updates.

## Prerequisites

- Worktree on branch `011-port-wave2-examples`
- `.specify/feature.json` points to `specs/011-port-wave2-examples`
- .NET 10 SDK available
- Existing wave-1 examples and smoke tests are present
- Framework readiness from `008-controls-revision`,
  `009-controls-widgets-and-collections`, and
  `010-standard-dialogs-designer` is available

## Planned Validation Flow

1. Confirm scope boundaries:
   - exactly eleven wave-2 examples are added
   - `sdlg` and `sdlg2` are scrollable-dialog examples
   - `demo` and `dlgdsn` carry standard/dynamic dialog proof
   - no wave-3/4 example counts toward wave-2 completion
   - no editor/help/terminal-emulation/runtime-mouse/real charset effect scope
   - no file content I/O inside standard-dialog acceptance
   - broader `sdlg`/`sdlg2` parity cleanup is separated from wave-2 acceptance

2. Verify example project registration:

   ```bash
   dotnet sln TuiVision.sln list
   dotnet build --configuration Release
   ```

   Before `dotnet build`, increment the manual build counter in
   `Directory.Build.props` according to the repository versioning rule.

3. Run focused smoke validation after implementation:

   ```bash
   dotnet test tests/TuiVision.Examples.SmokeTests/
   ```

   Expected proof:
   - all existing wave-1 smoke tests still pass
   - every wave-2 smoke test triggers one deterministic example-specific
     interaction
   - visible state is asserted for clipboard, list/input/history, combo,
     progress, dynamic text, scrollable dialogs, standard dialogs, dynamic
     dialog design, and broad demo integration
   - `progba` reaches completion and `tprogb` reaches a visible canceled state
   - `sdlg` proves vertical scrollable-dialog behavior
   - `sdlg2` proves horizontal and vertical scrollable-dialog behavior

4. Run repository validation:

   Before each `dotnet build` and `dotnet test` command below, increment the
   manual build counter in `Directory.Build.props` according to the repository
   versioning rule.

   ```bash
   dotnet build --configuration Release
   dotnet test
   dotnet test --collect:"XPlat Code Coverage"
   dotnet format --verify-no-changes
   ```

5. If public APIs, XML comments, or generated docs change:

   ```bash
   docfx docfx.json
   cd tests/web-a11y
   npm run test:docfx
   ```

6. Record completion evidence:
   - update `examples/README.md`
   - add eleven DE-first/EN-second guides under `docs/guides/examples/`
   - update `Pflichtenheft.md` wave-2 checklist and next-step marker
   - update `docs/project-statistics.md`
   - add/update lightweight architecture evidence under `docs/architecture/`
   - review existing `docs/security/` applicability files
   - record A11Y review path for terminal examples and guides
   - refresh agent context for Codex, Claude, Gemini, and Copilot after plan
     generation and again if implementation changes active context

## Expected Outcomes

- `examples/` contains 15 delivered examples in total: 4 from wave 1 and 11
  from wave 2.
- `tests/TuiVision.Examples.SmokeTests/` covers all 15 delivered examples.
- Each wave-2 example has a dedicated guide.
- `sdlg` and `sdlg2` are complete for their historical ScrollDialog/ScrollGroup
  purpose in wave 2.
- Historical parity cleanup beyond acceptance is traceable and non-blocking.
- The next Pflichtenheft marker points to wave 3 only after wave-2 evidence is
  complete.
