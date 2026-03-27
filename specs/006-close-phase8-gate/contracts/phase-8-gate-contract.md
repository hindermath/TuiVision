# Contract: Phase-8 Entrance Gate and M-07 Closure Surface

## Purpose

Define the externally reviewable contract for the final framework-completeness
increment: all historical `.cc` proof rows end in a final allowed state, the
hard quality gates are satisfied, and the repository contains enough evidence
to decide whether mandatory example wave 1 may begin.

## Repository Proof Surface

The closure claim is represented by these repository-owned surfaces:

- `docs/porting-status.md`
- `Pflichtenheft.md`
- MSTest suites under `tests/`
- coverage evidence for `TuiVision.Core`, `TuiVision.Controls`,
  `TuiVision.Serialization`, `TuiVision.Compatibility`, and
  `TuiVision.Drivers.Console`
- formatting/build/doc validation records
- compatibility-evidence records
- one dedicated gate-closure git commit

No external spreadsheet, hidden local note, or oral-only review context is part
of the accepted closure surface.

## Closure Record for Feature `006-close-phase8-gate`

The repository-visible closure record for `2026-03-27` is:

- `docs/porting-status.md` contains no provisional `portiert + Test ausstehend`
  ledger row.
- `dotnet build --configuration Release` passed.
- `dotnet test` passed across the repository test-project set.
- `dotnet format --verify-no-changes` passed.
- `docfx docfx.json` passed because public APIs and XML comments changed in the
  006 package.
- Coverage evidence is recorded separately for the five gate assemblies:
  - `TuiVision.Core`: `89.11 %`
  - `TuiVision.Controls`: `84.10 %`
  - `TuiVision.Serialization`: `83.33 %`
  - `TuiVision.Compatibility`: `80.95 %`
  - `TuiVision.Drivers.Console`: `97.43 %`
- No unresolved local-versus-CI coverage conflict remains because the
  repository proof surface currently contains no conflicting assembly-specific
  CI artifact.
- Dedicated closure-commit reference:
  `docs: close phase-8 entrance gate for feature 006`

## `docs/porting-status.md` Final-State Contract

### Required Row Outcome

- Every historical `.cc` file in the formal `M-07` scope must appear exactly
  once.
- Every row must end in exactly one of these states:
  - `portiert + getestet`
  - `bewusst ausgelassen + Begruendung`
- The provisional state `portiert + Test ausstehend` is not allowed in the
  finished gate-closure package.

### Proof Guarantees

1. **Tested-port guarantee**: Every `portiert + getestet` row points to
   repository-visible automated test evidence.
2. **Rationale guarantee**: Every `bewusst ausgelassen + Begruendung` row
   explains the true architecture replacement or obsolete special case.
3. **Implementation guarantee**: Non-driver rows still mapped to `geplant`
   targets in Core/Controls/Serialization/Compatibility are implementation work
   unless they are explicitly justified as a true replacement or obsolete
   special case.
4. **Reviewability guarantee**: Reviewers can inspect any row without relying on
   commit-history archaeology alone.

## Quality-Gate Contract

### Hard Pass Conditions

1. `dotnet build --configuration Release` succeeds.
2. `dotnet test` succeeds across all test projects in the repository.
3. `dotnet format --verify-no-changes` succeeds.
4. `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
   `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each reach
   `>= 70 %` line coverage.
5. Coverage evidence is reviewable separately for each of the five gate
   assemblies, not only as an aggregated percentage.
6. No gate-scoped module remains placeholder-only or no-op-only.
7. If local and CI coverage evidence diverges for a gate assembly, the
   discrepancy is resolved and the authoritative repository-visible result is
   identified before closure.
8. If public APIs or XML comments changed, `docfx docfx.json` succeeds.
9. No undocumented blocker remains in the gate review.

### Test and Coverage Guarantees

1. **Repository-wide test guarantee**: The gate is not satisfied by a subset of
   module tests; the full repository test-project set must pass.
2. **Five-module coverage guarantee**: Core, Controls, Serialization,
   Compatibility, and Drivers.Console each satisfy the same hard 70%
   threshold.
3. **Assembly-report guarantee**: The final proof package exposes coverage
   results separately for the five target assemblies, even where tests are
   shared across projects.
4. **Real-responsibility guarantee**: A gate-counted module must carry real
   remaining framework responsibility; placeholder-only or no-op-only code with
   trivial tests does not satisfy the contract.
5. **Gate-scope update guarantee**: If a module is restructured out of the hard
   gate, the gate-defining proof surfaces are updated in the same change.
6. **Conflict-resolution guarantee**: Divergent local and CI coverage results
   cannot leave the gate in a passing state until the governing result is
   explicitly named.
7. **Skip/ignore guarantee**: A skipped or ignored test without a tracked issue
   blocks closure.

## Compatibility-Evidence Contract

### Required Environments

- `MacBook Air M2`
- `Mac mini M4 Pro`
- Linux
- Windows/WSL

### Applicability Rule

- Linux and Windows/WSL execution evidence is mandatory when the implemented
  changes materially affect runtime behavior, terminal behavior, portability, or
  build reliability.
- If the closure work is limited to documentation or other non-runtime proof
  maintenance, the evidence package must record a reviewable not-applicable
  rationale instead of pretending execution happened.

## Closure-Commit Contract

- The final closure claim must be represented by a dedicated git commit.
- That commit must identify the proof artifacts or point directly to them.
- The commit boundary is part of the contract, not an optional documentation
  convenience.
- For feature `006-close-phase8-gate`, the dedicated closure-commit reference
  is `docs: close phase-8 entrance gate for feature 006`.

## Documentation Contract

- `Pflichtenheft.md`, `docs/porting-status.md`, and the Phase-8 review surfaces
  must use the same interpretation of the gate.
- Public API or XML-comment changes require `docfx docfx.json` in the same
  closure effort.
- Documentation remains bilingual and CEFR-B2 where the constitution requires
  explanatory or XML documentation updates.
