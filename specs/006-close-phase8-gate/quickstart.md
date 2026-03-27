# Quickstart: M-07 Closure and Phase-8 Entrance Gate

## Goal

This quickstart now records the completed Phase-8 gate-closure increment:

1. the remaining non-driver framework gaps behind `M-07` are closed,
2. all 151 historical `.cc` ledger rows now end in a final allowed proof
   state, and
3. the full gate evidence package required before mandatory example wave 1 is
   repository-visible.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `006-close-phase8-gate`
- Historical source inventory present under `tv203s/contrib/tvision/classes`
- Existing test projects available under `tests/`

## Executed Validation Flow (2026-03-27)

1. All provisional `portiert + Test ausstehend` rows were eliminated from
   `docs/porting-status.md`.

2. Coverage-focused MSTest additions were completed in:
   - `tests/TuiVision.Core.Tests/`
   - `tests/TuiVision.Controls.Tests/`
   - `tests/TuiVision.Serialization.Tests/`
   - `tests/TuiVision.Compatibility.Tests/`
   - `tests/TuiVision.Drivers.Tests/`

3. The gate-scoped modules were rechecked for real remaining responsibility;
   no placeholder-only or no-op-only gate module remained in scope.

4. The mandatory quality gates were executed before the closure commit:

```bash
dotnet build --configuration Release
dotnet test
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Core.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Serialization.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Compatibility.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Drivers.Tests/ --collect:"XPlat Code Coverage"
```

5. The final coverage package was separated by gate assembly. A single
   aggregated repository percentage was not used as closure evidence.

6. The local and repository-visible CI review showed no conflicting
   assembly-specific coverage artifact, so the local `2026-03-27` Coverlet
   package remains the authoritative result for the gate proof.

7. No gate-scoped test was skipped or ignored, so no tracked-issue exception
   path was required.

8. Because public APIs and XML comments changed in the 006 package, the
   conditional documentation gate was executed:

```bash
docfx docfx.json
```

9. Compatibility evidence was reconciled against the existing Multi-Mac and
   Linux/WSL proof surfaces:
   - `MacBook Air M2` and `Mac mini M4 Pro` remain the primary workflow
     baselines
   - Linux and Windows/WSL received an explicit not-applicable refresh note for
     tasks `T022` to `T034`, because that closure slice only finalized proof
     artifacts after the runtime-facing `M-07` consolidation evidence already
     existed

10. The final proof package was synchronized across:
    - `docs/porting-status.md`
    - `Pflichtenheft.md`
    - gate review artifacts
    - the dedicated gate-closure commit

## Representative Command Flow

```bash
dotnet build --configuration Release
dotnet test
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Core.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Serialization.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Compatibility.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Drivers.Tests/ --collect:"XPlat Code Coverage"
docfx docfx.json   # only when public API or XML comments changed
```

## Closure Summary

- No row in `docs/porting-status.md` remains in `portiert + Test ausstehend`.
- Every tested row now points to repository-visible automated evidence.
- `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` reached
  `89.11 %`, `84.10 %`, `83.33 %`, `80.95 %`, and `97.43 %` line coverage.
- `dotnet build --configuration Release`, `dotnet test`,
  `dotnet format --verify-no-changes`, and `docfx docfx.json` all passed on
  `2026-03-27`.
- No unresolved local-versus-CI conflict remains for any gate assembly because
  no conflicting assembly-specific CI artifact exists in the repository proof
  surface.
- Linux and Windows/WSL carry an explicit not-applicable refresh note for the
  pure gate-documentation closeout tasks, while the earlier runtime-facing
  manual compatibility evidence remains reviewable in
  `docs/guides/multi-mac-workflow.md`.
- The authoritative closure surfaces are:
  - `docs/porting-status.md`
  - `Pflichtenheft.md`
  - `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`
  - this quickstart
  - `contracts/phase-8-gate-contract.md`
- Dedicated closure commit reference:
  `docs: close phase-8 entrance gate for feature 006`
- Mandatory example wave 1 becomes eligible to start with that dedicated
  closure commit.
