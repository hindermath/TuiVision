# Quickstart: M-07 Closure and Phase-8 Entrance Gate

## Goal

Validate the planned Phase-8 gate-closure increment by:

1. closing the remaining non-driver framework gaps behind `M-07`,
2. lifting all 151 historical `.cc` ledger rows into a final allowed proof
   state, and
3. assembling the full gate evidence package required before mandatory example
   wave 1 may begin.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `006-close-phase8-gate`
- Historical source inventory present under `tv203s/contrib/tvision/classes`
- Existing test projects available under `tests/`

## Planned Validation Flow

1. Identify every row in `docs/porting-status.md` that still ends in
   `portiert + Test ausstehend` or still points to a non-driver `geplant`
   target.

2. Write failing MSTest cases in the affected test projects:
   - `tests/TuiVision.Core.Tests/`
   - `tests/TuiVision.Controls.Tests/`
   - `tests/TuiVision.Serialization.Tests/`
   - `tests/TuiVision.Drivers.Tests/`
   - a dedicated Compatibility-focused suite if the existing repository tests
     do not exercise `TuiVision.Compatibility` deeply enough

3. Implement the minimum framework behavior needed to close those rows as
   `portiert + getestet`, or explicitly justify true replacements / obsolete
   special cases as `bewusst ausgelassen + Begruendung`.

4. Update `docs/porting-status.md` so all 151 rows end in a final allowed proof
   state and each tested row points to repository-visible automated evidence.

5. Confirm that every module still counted in the hard gate carries real
   remaining responsibility rather than placeholder-only or no-op-only code.
   If not, restructure it out of gate scope before claiming closure.
   Update the gate-defining proof surfaces in the same change:
   `spec.md`, `plan.md`, `quickstart.md`, `contracts/phase-8-gate-contract.md`,
   `Pflichtenheft.md`, and the Phase-8 review artifacts.

6. Run the mandatory quality gates before the closure commit:

```bash
dotnet build --configuration Release
dotnet test
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Core.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Serialization.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Drivers.Tests/ --collect:"XPlat Code Coverage"
```

The resulting evidence must separate the five target assemblies in the final
coverage package. A single aggregated repository percentage is not sufficient.
If local and CI coverage results diverge for any gate assembly, do not claim
closure until the discrepancy is explained and the authoritative
repository-visible result is identified in the proof package.

7. If public APIs or XML comments changed, run the conditional documentation
   gate:

```bash
docfx docfx.json
```

8. Refresh compatibility evidence:
   - always validate on `MacBook Air M2` and `Mac mini M4 Pro`
   - validate on Linux and Windows/WSL whenever the implemented changes
     materially affect runtime behavior, terminal behavior, portability, or
     build reliability
   - otherwise record an explicit not-applicable rationale

9. Reconcile the final proof package across:
   - `docs/porting-status.md`
   - `Pflichtenheft.md`
   - gate review artifacts
   - the final dedicated gate-closure commit

## Representative Command Flow

```bash
dotnet build --configuration Release
dotnet test
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Core.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Serialization.Tests/ --collect:"XPlat Code Coverage"
dotnet test tests/TuiVision.Drivers.Tests/ --collect:"XPlat Code Coverage"
docfx docfx.json   # only when public API or XML comments changed
```

## Expected Outcomes

- No row in `docs/porting-status.md` remains in `portiert + Test ausstehend`.
- Every tested row has repository-visible automated evidence.
- Core, Controls, Serialization, Compatibility, and Drivers.Console each meet
  or exceed 70% line coverage, reported separately per target assembly.
- No module remains inside the hard gate as placeholder-only or no-op-only
  code.
- No unresolved local-versus-CI conflict remains for any gate assembly.
- `dotnet test` passes for all repository test projects.
- Linux and Windows/WSL evidence is either refreshed or explicitly documented as
  not applicable.
- A dedicated git commit states that the Phase-8 entrance gate is closed.
- Mandatory example wave 1 becomes reviewably eligible to start only after that
  commit exists.
