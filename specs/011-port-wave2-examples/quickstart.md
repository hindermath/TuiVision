# Quickstart: Port Wave 2 Examples

## Purpose

Use this quickstart to validate the `011-port-wave2-examples` plan before task
generation where applicable and again after implementation. Command-based checks
assume their matching setup or implementation tasks have created the required
projects, references, and proof files. The feature is accepted through eleven
managed example projects, deterministic smoke tests, didactic guides, and
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
   - exactly eleven wave-2 examples are added: `clipboard`, `demo`, `dlgdsn`,
     `dyntxt`, `inplis`, `listvi`, `progba`, `sdlg`, `sdlg2`, `tcombo`, and
     `tprogb`
   - `sdlg` and `sdlg2` are scrollable-dialog examples
   - `demo` and `dlgdsn` carry standard/dynamic dialog proof, and no third
     example is admissible as a standard-dialog acceptance vehicle
   - no wave-3/4 example counts toward wave-2 completion
   - no editor/help/stream/terminal-emulation/runtime-mouse/real charset effect
     scope
   - no file content I/O inside standard-dialog acceptance
   - broader `sdlg`/`sdlg2` parity cleanup is separated from wave-2 acceptance

2. Review traceability before task generation:
   - every Pflichtenheft wave-2 item maps to one planned project, smoke test,
     guide, and proof path
   - every `SC-001` through `SC-009` outcome maps to a concrete plan evidence
     surface
   - every interaction family from `SC-004` has an owning example and visible
     proof path
   - every accepted limitation has rationale, acceptance impact, earliest
     follow-up point, and traceable reference

3. After T015 and T016, verify example project registration:

   ```bash
   dotnet sln TuiVision.sln list
   dotnet build --configuration Release
   ```

   Apply the canonical repository versioning rule from
   `tasks.md` -> "Repository versioning rule" anchor before any `dotnet build`
   or `dotnet test` command in this quickstart. The rule is intentionally NOT
   duplicated here.

4. Run focused smoke validation after implementation:

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
   - boundary content is represented for lists, combo boxes, dynamic text,
     progress, and scrollable-dialog bounds where the historical flow supports
     it
   - dialog smoke paths include success plus applicable cancel, close, invalid,
     or rejected states
   - unavailable or isolated clipboard behavior is visible rather than skipped
   - `progba` reaches completion and `tprogb` reaches a visible canceled state
   - `sdlg` proves vertical scrollable-dialog behavior
   - `sdlg2` proves horizontal and vertical scrollable-dialog behavior

5. Run repository validation. Apply the canonical repository versioning rule
   from the `tasks.md` preamble before each `dotnet build` and `dotnet test`
   command below; the rule is intentionally NOT duplicated here. Run **from
   the repository root** so that `--settings coverlet.runsettings` resolves
   correctly; running from a sub-folder silently disables the
   Include/Exclude filter and invalidates the coverage gate.

   ```bash
   dotnet build --configuration Release
   dotnet test
   dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
   dotnet format --verify-no-changes
   ```

6. If public APIs, XML comments, generated docs, or DocFX navigation change:

   Before generating documentation, keep new example/helper types internal when
   no public API is intended. Public types that remain public need complete
   German-first and English-second XML documentation.

   ```bash
   docfx docfx.json
   cd tests/web-a11y
   npm run test:docfx
   ```

7. Record completion evidence:
   - update `examples/README.md`
   - add eleven DE-first/EN-second guides under `docs/guides/examples/`
   - update `Pflichtenheft.md` wave-2 checklist and next-step marker
   - update `docs/project-statistics.md`
   - add/update lightweight architecture evidence under `docs/architecture/`
   - review existing `docs/security/` applicability files
   - record A11Y review path for terminal examples, smoke output, guides, and
     generated HTML docs when changed; otherwise record a justified N/A
   - record platform evidence for current macOS validation plus Linux and
     Windows/WSL where practical, or record an explicit N/A and follow-up path
     for each missing environment
   - refresh agent context for Codex, Claude, Gemini, and Copilot after plan
     generation and again if implementation changes active context
   - verify whether a matching `Lastenheft_*.md` exists; if yes, rename it with
     `bash scripts/rename-lastenheft.sh <LH-file> 011-port-wave2-examples`,
     otherwise record the explicit N/A rationale because this wave is driven
     directly from `Pflichtenheft.md`

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
