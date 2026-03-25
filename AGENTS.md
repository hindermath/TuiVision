# AGENTS.md

## Build/Lint/Test Commands

### Build and Test
```bash
# Restore, build, and test (full validation cycle)
dotnet restore
dotnet build --configuration Release
dotnet test

# Run tests for a specific project
dotnet test tests/TuiVision.Core.Tests/

# Run a single test method
dotnet test --filter "FullyQualifiedName~TestMethodName"

# Build a specific project
dotnet build src/TuiVision.Core
```

### Linting and Formatting
```bash
# Run code analysis
dotnet build --no-restore --verbosity quiet

# Check for style issues (if configured)
dotnet format --verify-no-changes

# Regenerate docs when API/XML comments change and root config exists
docfx docfx.json
```

### Coverage Gate (SC-003)
- **Minimum**: ≥ 70 % Line Coverage jeweils in `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` (Pflichtenheft §9.4 Nr. 1)
- **Measurement**: Coverlet (`coverlet.collector` package) via `dotnet test --collect:"XPlat Code Coverage"`
- Merging into `main` without passing the coverage gate is NOT permitted.

## Code Style Guidelines

### Imports and Using Statements
- Use `using` directives at the top of files, grouped logically
- Place `using` directives inside namespace declarations when needed
- Follow C# convention for ordering: System namespaces first, then third-party, then internal

### Formatting and Naming Conventions
- Use PascalCase for all identifiers (classes, methods, properties, etc.)
- Use camelCase for local variables and parameters
- Use `var` for implicit typing when the type is obvious
- Prefer explicit type declarations for readability where needed
- Use `readonly record struct` for immutable value types
- Use `sealed class` for classes not meant to be inherited
- Use `public` keyword explicitly when needed (C# default is internal)
- Use `private` keyword for private members when needed for clarity

### Types and Structs
- Use `readonly record struct` for immutable value types (like TPoint, TRect)
- Use records for value objects that carry data
- Use classes for reference types that need inheritance or mutability
- Use enums for sets of named constants
- Use `enum with Flags` for bitwise combinations

### Error Handling
- Use exceptions for exceptional conditions
- Don't catch exceptions unless you can handle them meaningfully
- Provide meaningful exception messages
- Use `ArgumentNullException` for null parameter validation
- Consider using `ArgumentNullException.ThrowIfNull()` in .NET 6+

### Code Organization
- Group related methods and properties within classes
- Use partial classes for large files when appropriate
- Keep methods small and focused on single responsibilities
- Use meaningful names that describe what the code does
- Avoid magic numbers - use constants or enums instead
- Prefer composition over inheritance where possible
- Use interfaces for contracts and abstractions
- Use `static` for utility methods that don't depend on instance state
- Use `sealed` classes when inheritance is not intended

### JSON Handling
- Use `System.Text.Json` for project-owned JSON parsing and serialization
- Introduce `Newtonsoft.Json` only with documented justification and explicit review approval

### Documentation Guidelines
- Explanatory documentation blocks must be bilingual: German first, English second
- German and English documentation should target CEFR-B2 readability
- Public APIs must include complete XML documentation (`summary`, `param`, `returns`, `exception` where needed)
- Update documentation in the same change when API signatures or XML comments change

## Testing Guidelines
- All test files should be in `tests/` directory with corresponding project structure
- Use MSTest attributes for test methods: `[TestMethod]` for tests
- Use descriptive test method names starting with `Test_`
- Organize tests into classes that match the naming of the tested class
- Use `[TestClass]` attribute for test classes
- Tests should be independent and not rely on test execution order
- Use `Assert.AreEqual()` for equality checks
- Use `Assert.IsTrue()` or `Assert.IsFalse()` for boolean assertions
- Use `Assert.ThrowsException<T>()` for expected exception testing
- Test edge cases including boundary conditions and null inputs

## CI/CD Configuration
- Repository uses GitHub Actions for CI
- Builds and tests on Ubuntu and macOS runners; Windows oder WSL-basierte Kompatibilitaetschecks sollen bei relevanten Aenderungen zusaetzlich beruecksichtigt werden
- Uses .NET 10 SDK
- Tests are run using `dotnet test` command with Release configuration
- Build and test validation is mandatory for all code changes

## Branching Convention
- Feature branches use either the agent-prefixed form `codex/<feature-description>` (or another supported agent prefix such as `claude/`, `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit form `NNN-short-description` when the Spec-Kit workflow creates the branch.
- CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `gemini/**`, `copilot/**`, and `opencode/**` branches.

## Active Feature Context

### 004-editor-file-help-streams
- Current implementation baseline: execute the phase-6 increment from `specs/004-editor-file-help-streams/spec.md` and `specs/004-editor-file-help-streams/plan.md`
- Scope is limited to reusable framework components in `src/TuiVision.Controls` and `src/TuiVision.Serialization`: `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`, file/dialog/history helpers, help topics/viewers/windows, stream primitives, and named resource containers
- Out of scope for this increment: example applications such as `tvedit`, `bhelp`, and `helpdemo`; driver consolidation; calculator/macros/OS-shell integrations; and unrelated specialized widgets
- Editor flows must cover text editing, insert/overwrite behavior, clipboard-oriented actions, search/replace, modified-state handling, explicit safe-close decisions before unsaved changes are discarded, and distinct overwrite decisions when save conflicts occur
- Integration coverage for this feature must explicitly include event-loop-aware shell interaction, focus transitions, menu execution, and dialog interaction rather than relying on those behaviors only implicitly
- File flows must keep directory navigation, file lists, current file-information metadata, wildcard filtering, manual path entry, and history recall synchronized inside reusable dialogs
- Help flows must support context-based topic lookup, cross-reference navigation, and fallback content for missing contexts
- Stream/resource flows must preserve named lookup semantics and reject malformed persisted input explicitly, including truncated, trailing, unknown-type, and cyclic payload failures
- Planning decisions now fixed for this feature: dedicated runtime help files, shared-reference preservation without cyclic-graph support, exact case-sensitive resource keys, `LF` default for new files, preserved line endings for loaded files, and explicit overwrite decisions after external file changes

### 005-driver-consolidation-m07
- Current planning baseline: execute the Phase-7 increment from `specs/005-driver-consolidation-m07/spec.md` and `specs/005-driver-consolidation-m07/plan.md`
- Scope is limited to the managed driver baseline in `src/TuiVision.Drivers.Console`, the supporting validation in `tests/TuiVision.Drivers.Tests`, and the proof ledger `docs/porting-status.md`
- Out of scope for this increment: mandatory example waves, full closure of the Phase-8 entrance gate, new source modules, native bindings, and any one-to-one recreation of the historical per-OS driver split
- The proof ledger must cover every historical `.cc` implementation file in `tv203s/contrib/tvision/classes` with one mandatory primary target, optional secondary targets, status, evidence, and rationale
- Linux and Windows/WSL compatibility checks are required as reviewable evidence for this phase, but may still be manual or semi-automated rather than mandatory CI gates
- Planning decisions now fixed for this feature: `.cc` files are the formal `M-07` ledger scope, ancillary `.c`/`.h` files may appear only as rationale support, capability buckets replace per-OS lineage as the review model, and Phase 7 remains distinct from the later full Phase-8 gate closure

### 006-close-phase8-gate
- Current planning baseline: execute the Phase-8 entrance-gate closure from `specs/006-close-phase8-gate/spec.md` and `specs/006-close-phase8-gate/plan.md`
- Scope is limited to closing the remaining `M-07` proof and gate evidence in `docs/porting-status.md`, `Pflichtenheft.md`, the existing module test projects plus any required Compatibility-focused validation additions, coverage evidence, formatting evidence, and API-documentation validation for `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`
- Out of scope for this increment: starting any of the 25 mandatory example waves, introducing substitute follow-on example scope from `TVDEMOS/` or `TVFM/`, reopening Phase-7 capability-bucket decisions except where pending proof rows require final status updates, and unrelated new framework features
- Every historical `.cc` row in `docs/porting-status.md` must end in a final proof state of `portiert + getestet` or `bewusst ausgelassen + Begruendung`; no `portiert + Test ausstehend` rows may remain once the gate is claimed closed
- Gate closure must provide explicit build, full-test, coverage, formatting, and conditional API-doc evidence, and must keep the mandatory example waves blocked until the closure is formally recorded
- Planning decisions now fixed for this feature: `docs/porting-status.md` remains the authoritative M-07 ledger; `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` must each satisfy the explicit 70 % line-coverage gate with assembly-specific evidence; placeholder-only or no-op-only modules cannot satisfy that gate; gate-scope removals must update the proof surfaces in the same change; unresolved local-versus-CI coverage conflicts block closure; and the final Phase-8 decision requires a dedicated closure marker or commit reference

## Agent File Synchronization Policy

- When active feature context, implementation plans, or project-wide agent guidance changes, the following AI-agent files MUST be reviewed and updated together in the same work item if they are affected:
  - `AGENTS.md`
  - `CLAUDE.md`
  - `GEMINI.md`
  - `.github/copilot-instructions.md`
- Partial synchronization is not acceptable when shared guidance has changed.
- If one file intentionally diverges for agent-specific reasons, that divergence MUST be explicit and documented in the same change.

## Project Statistics

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Each update must record the relevant branch/phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 code lines per day for an experienced developer, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal/Turbo-Vision-derived port.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Workflow Platforms

- The Multi-Mac setup on `MacBook Air M2` and `Mac mini M4 Pro` is the primary development and day-to-day test workflow.
- Keep `gh`, `specify`, `codex`, `claude`, `copilot`, and `gemini` installed on both Macs; before Spec-Kit work or Spec-Kit updates, run `specify check` to confirm the required toolchain is available.
- Linux and Windows are additional compatibility-validation environments; on Windows, prefer WSL with a current Ubuntu release, currently `Ubuntu 24.04`.
- When changes affect runtime behavior, build reliability, terminal behavior, or portability, include Linux and Windows/WSL compatibility checks where practical and reflect them in CI or equivalent validation evidence when feasible.

## Pflichtenheft Next-Step Marker

- Maintain a prominent `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md`.
- The marker MUST point to the currently highest-priority open work item in the prioritized rest-work section and MUST be moved whenever progress changes the effective next step.

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Real local file-system interaction plus persisted binary help/resource files; no database layer in this increment (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, existing MSTest suites plus any required Compatibility-focused validation additions, Coverlet coverage evidence, `dotnet format`, docfx, `Pflichtenheft.md`, and `docs/porting-status.md` for the formal Phase-8 entrance-gate proof (006-close-phase8-gate)
- Repository-visible proof artifacts only; no database layer, and no example-application delivery in this increment (006-close-phase8-gate)

## Recent Changes
- 004-editor-file-help-streams: Added the phase-6 specification and requirements checklist for editor, file, help, stream, and resource components.
- 004-editor-file-help-streams: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/public-api.md`; synchronized shared agent guidance to the post-plan baseline.
- 004-editor-file-help-streams: Applied plan-review clarifications for safe-close vs. overwrite handling, wildcard-filtered file dialogs, explicit malformed-stream cases, and non-functional scope boundaries.
- 004-editor-file-help-streams: Added explicit coverage of insert/overwrite plus clipboard editor actions, synchronized file-information state in dialogs, shell menu/status routing, and the full Core/Controls/Serialization coverage gate.
- 004-editor-file-help-streams: Tightened the remaining integration-test expectations so event-loop dispatch, focus transitions, menu execution, and explicit dialog interaction are named directly in the feature artifacts.
- 005-driver-consolidation-m07: Added the phase-7 specification and clarification set for managed driver consolidation, `M-07` proof coverage, primary versus secondary ledger targets, and required Linux/Windows/WSL compatibility evidence.
- 005-driver-consolidation-m07: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-7-proof-contract.md` to define the capability-based driver consolidation approach, the formal `.cc` ledger scope, and the review contract for `docs/porting-status.md`.
- 005-driver-consolidation-m07: Implemented Phase-7 consolidation: created `DriverCapabilityMap.cs` with 5 capability buckets, built `docs/porting-status.md` covering all 151 historical `.cc` files, added 5 new driver test files (30 tests passing), updated `docs/guides/multi-mac-workflow.md` with compatibility evidence, created `checklists/phase-8-gate-review.md`, updated `Pflichtenheft.md` marker to Phase-8 gate closure.
- 006-close-phase8-gate: Added the Phase-8 entrance-gate specification and requirements checklist for fully closing `M-07`, resolving every pending ledger row to a final proof state, and packaging the remaining build/test/coverage/format/API-doc evidence before mandatory example work may begin.
- 006-close-phase8-gate: Synchronized the hard coverage rule to require `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each to reach at least 70 % line coverage across the shared agent guidance and gate-tracking artifacts.
- 006-close-phase8-gate: Added `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/phase-8-gate-contract.md` to define the final `M-07` closure workflow, the 5x-70%-coverage gate, the full-suite validation package, and the dedicated closure-commit contract.
- 006-close-phase8-gate: Refined the planning baseline so the five-module coverage gate now requires assembly-specific evidence and forbids placeholder-only or no-op-only modules from satisfying Phase-8 closure.
