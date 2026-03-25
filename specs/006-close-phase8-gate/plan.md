# Implementation Plan: M-07 Closure and Phase-8 Entrance Gate

**Branch**: `006-close-phase8-gate` | **Date**: 2026-03-25 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/006-close-phase8-gate/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/006-close-phase8-gate/spec.md)
**Input**: Feature specification from `/specs/006-close-phase8-gate/spec.md`

**Note**: This plan covers the final framework-completeness step before
mandatory example waves may begin: closing all remaining `M-07` proof gaps,
lifting every historical `.cc` ledger row into a final allowed state, and
closing the full Phase-8 entrance gate with repository-visible quality
evidence.

## Summary

Finish the still-open framework proof by implementing and testing the remaining
non-driver Phase-8-scope framework types in `TuiVision.Core`,
`TuiVision.Controls`, `TuiVision.Serialization`, and
`TuiVision.Compatibility`, update `docs/porting-status.md` so all 151
historical `.cc` rows end in `portiert + getestet` or
`bewusst ausgelassen + Begruendung`, and assemble the full gate evidence
package: `dotnet build --configuration Release`, `dotnet test` across all test
projects, `dotnet format --verify-no-changes`, Coverlet-backed line coverage of
at least 70% in Core/Controls/Serialization/Compatibility/Drivers.Console with
assembly-specific reporting for each gate assembly, conditional
`docfx docfx.json`, and platform-evidence records for Multi-Mac plus
Linux/Windows/WSL where the implemented changes materially affect runtime,
terminal, portability, or build behavior. Placeholder-only or no-op-only
modules are not valid closure evidence; every module counted in the hard gate
must still carry real remaining responsibility or be restructured out of gate
scope before closure is claimed.

## Terminology & Operational Definitions

- **Historical implementation file**: One `.cc` file from
  `tv203s/contrib/tvision/classes`, including platform-specific subdirectories,
  that must end in a final `M-07` proof state.
- **Final proof state**: One of exactly two allowed terminal states in
  `docs/porting-status.md`: `portiert + getestet` or
  `bewusst ausgelassen + Begruendung`.
- **Planned non-driver framework entry**: A ledger row currently mapped to a
  non-driver target marked `geplant` in `TuiVision.Core`, `TuiVision.Controls`,
  `TuiVision.Serialization`, or `TuiVision.Compatibility`; this plan treats
  such rows as implementation work unless they are upgraded to a true
  architecture replacement or obsolete special case with explicit rationale.
- **Validation evidence package**: The combined review set of build, test,
  coverage, format, documentation, compatibility, and ledger evidence used to
  judge whether Phase 8 may begin.
- **Assembly-specific coverage result**: One line-coverage outcome for a single
  gate assembly that remains reviewable on its own even if the exercising tests
  come from shared or cross-module test projects.
- **Gate-scoped module**: One module currently counted toward the hard Phase-8
  coverage gate; it must either carry real remaining responsibility or be
  explicitly restructured out of the gate before closure is claimed.
- **Placeholder-only module**: A gate-listed assembly whose remaining code is
  only scaffold, stub, pass-through, or no-op behavior and therefore does not
  represent substantive remaining framework responsibility.
- **Trivial test**: A test that only exercises placeholder, no-op, or
  scaffolding behavior without asserting meaningful gate-relevant framework
  outcomes.
- **Gate-scope restructuring package**: The coordinated update to spec, plan,
  quickstart, contract, and gate-review proof surfaces that removes a module
  from the hard gate because it no longer carries real remaining responsibility.
- **Gate-closure commit**: The dedicated git commit that records the finished
  proof package and states that the Phase-8 entrance gate is closed.
- **Materially platform-relevant change**: A change that affects runtime
  behavior, terminal behavior, portability, or build reliability strongly
  enough that Linux and Windows/WSL execution evidence must be refreshed.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing modules `TuiVision.Core`,
`TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`,
`TuiVision.Compatibility`; MSTest; Coverlet via
`dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation
validation; GitHub Actions for existing CI  
**Storage**: Source-controlled C# code plus Markdown evidence in
`docs/porting-status.md`, `Pflichtenheft.md`, and the 006 planning artifacts;
no database or external spreadsheet storage  
**Testing**: MSTest-first coverage in `tests/TuiVision.Core.Tests`,
`tests/TuiVision.Controls.Tests`, `tests/TuiVision.Serialization.Tests`,
`tests/TuiVision.Drivers.Tests`, and any added Compatibility-focused suite if
cross-module coverage is insufficient; repository-wide `dotnet test` is a hard
gate; Core/Controls/Serialization/Compatibility/Drivers.Console each require
`>= 70 %` line coverage with separate assembly-specific evidence even when test
execution is distributed across multiple repository test projects  
**Target Platform**: Managed cross-platform .NET library workflow on macOS,
Linux, and Windows/WSL, with `MacBook Air M2` and `Mac mini M4 Pro` as the
primary development environments  
**Project Type**: Managed .NET framework increment with repository-local proof
artifacts and gate-closing quality evidence  
**Performance Goals**: Gate validation remains practical for local execution on
the primary Multi-Mac workflow; reviewers can determine Phase-8 readiness in
one pass through the evidence package without ad-hoc repository archaeology  
**Constraints**: No native bindings; no example-porting work in this increment;
all 151 ledger rows must end in a final proof state; full `dotnet test` across
all repository test projects must pass;
Core/Controls/Serialization/Compatibility/Drivers.Console each must reach at
least 70% line coverage; aggregated coverage alone is insufficient because each
gate assembly must be reported separately; placeholder-only or no-op-only code
paired with trivial tests cannot satisfy the gate; skipped or ignored tests
without a tracked issue block closure; unresolved local-versus-CI evidence
conflicts block closure until one authoritative result is named; documentation
and XML comments remain bilingual at CEFR-B2; the closure requires a dedicated
git commit  
**Scale/Scope**: One documentation-backed closure pass across the remaining
Core/Controls/Serialization/Compatibility framework gaps, the still-open final
driver proof rows, all affected tests, the canonical ledger
`docs/porting-status.md`, the prioritized rest-work in `Pflichtenheft.md`, and
the Phase-8 review artifacts

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. The plan stays within the existing managed
  .NET architecture and does not introduce native bindings or platform-specific
  runtime forks.
- **Test-First Development — TDD**: Pass with explicit workflow constraint.
  Every remaining framework gap must start with failing MSTest coverage before
  implementation, and the final gate still requires repository-wide
  `dotnet test` plus assembly-specific five-module coverage evidence.
- **Didactic and Linguistic Clarity**: Pass. The feature extends repository
  proof and planning artifacts; any changed APIs or XML comments remain subject
  to bilingual CEFR-B2 documentation rules.
- **Modular Architecture**: Pass. Runtime work remains inside the existing five
  modules: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`; no new source
  assembly is introduced.
- **Cross-Platform Portability**: Pass. Multi-Mac remains the primary workflow,
  and Linux/Windows/WSL evidence is explicitly planned whenever the implemented
  changes materially affect runtime, terminal behavior, portability, or build
  reliability.
- **License & Disclaimer Integrity**: Pass. Historical files under `tv203s/`
  remain read-only inputs; new code and documentation stay under existing
  project ownership and disclaimer rules.

**Post-Design Gate Review**: Phase-1 artifacts keep the feature inside the
existing module hierarchy, preserve the managed-only runtime, retain the
mandatory full-test and stricter five-module assembly-specific coverage gates,
and maintain the distinction between this final framework-proof increment and
the later example-port waves. No constitution exception is required.

- The feature affects runtime validation, build/test workflow, and proof
  artifacts; Linux and Windows/WSL compatibility evidence must therefore be
  explicitly planned where changes materially affect platform-relevant
  behavior.
- The feature does not advance any of the 25 mandatory original examples from
  `tv203s/contrib/tvision/examples`; it closes the entrance gate that must pass
  before those waves may start.
- Statistical-documentation impact identified; update
  `docs/project-statistics.md` after the planning artifacts and synchronized
  agent context are written.

## Project Structure

### Documentation (this feature)

```text
specs/006-close-phase8-gate/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── phase-8-gate-contract.md
├── checklists/
│   └── requirements.md
└── tasks.md
```

### Source Code (repository root)

```text
src/
├── TuiVision.Core/
├── TuiVision.Controls/
├── TuiVision.Drivers.Console/
├── TuiVision.Serialization/
└── TuiVision.Compatibility/

tests/
├── TuiVision.Core.Tests/
├── TuiVision.Controls.Tests/
├── TuiVision.Drivers.Tests/
├── TuiVision.Serialization.Tests/
└── TuiVision.Examples.SmokeTests/

docs/
├── porting-status.md
├── project-statistics.md
└── guides/
    └── multi-mac-workflow.md

tv203s/
└── contrib/tvision/classes/
    ├── *.cc
    ├── dos/
    ├── linux/
    ├── qnx4/
    ├── qnxrtp/
    ├── unix/
    ├── wingr/
    ├── winnt/
    └── x11/
```

**Structure Decision**: Keep the feature inside the existing
Core/Controls/Serialization/Compatibility/Drivers.Console modules and the
paired or cross-module test projects that exercise them, use
`docs/porting-status.md` and `Pflichtenheft.md` as the primary proof surfaces,
and store all design artifacts under `specs/006-close-phase8-gate/`. No new
source module, external service, or alternate storage layer is justified.

## Research Focus

Phase 0 resolves and locks the following planning decisions:

1. The coverage gate applies independently to `TuiVision.Core`,
   `TuiVision.Controls`, `TuiVision.Serialization`,
   `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`, each at
   `>= 70 %`.
2. Coverage evidence must be reviewable per target assembly, not only as one
   aggregated repository or test-suite percentage.
3. Non-driver ledger rows still mapped to `geplant` targets are implementation
   work, not merely narrative reclassification work, unless a true replacement
   or obsolete special case is documented.
4. Full-suite validation means `dotnet test` across all repository test
   projects.
5. A gate-scoped module cannot remain placeholder-only or no-op-only at
   closure time; if it has no real remaining responsibility, it must be
   restructured out of the hard gate before closure is claimed.
6. Local and CI coverage conflicts must be resolved before closure, and the
   final evidence package must name the authoritative result if multiple
   repository-visible measurements existed during the work.
7. The Phase-8 closure requires a dedicated git commit.
8. Linux and Windows/WSL evidence is mandatory for materially
   platform-relevant changes and otherwise requires an explicit
   not-applicable rationale.

## Design Decisions

### Proof and Implementation Boundary

- `docs/porting-status.md` remains the single canonical `M-07` ledger.
- The ledger may no longer end with `portiert + Test ausstehend` rows once this
  feature is complete.
- Remaining framework gaps are closed by implementation and tests inside
  `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and
  `TuiVision.Compatibility`, while still-open driver rows must be backed by
  final `TuiVision.Drivers.Console` proof. Ledger text alone is not enough.
- A gate-scoped module must either retain real remaining framework
  responsibility or be explicitly restructured out of gate scope before closure
  is claimed.
- If a module is restructured out of gate scope, the same change must update
  the gate-defining proof surfaces that still name it.

### Quality-Gate Boundary

- The hard closure package consists of:
  - `dotnet build --configuration Release`
  - `dotnet test` across all test projects
  - `dotnet format --verify-no-changes`
  - per-module coverage evidence showing `>= 70 %` in
    Core/Controls/Serialization/Compatibility/Drivers.Console
  - assembly-specific coverage reporting for each of the five gate assemblies,
    even when the exercising tests come from shared or cross-module suites
  - conditional `docfx docfx.json` when public APIs or XML comments changed
  - Multi-Mac plus conditional Linux/Windows/WSL evidence
- No skipped or ignored test without a tracked issue is acceptable in the gate.
- Placeholder-only or no-op-only gate modules with trivial tests are not
  acceptable proof of Phase-8 readiness.
- Unexplained divergence between local and CI coverage evidence is a closure
  blocker until the authoritative repository-visible result is identified.

### Documentation and Review Boundary

- `Pflichtenheft.md`, `docs/porting-status.md`, and the Phase-8 review
  artifacts must all agree on the same gate interpretation.
- The dedicated gate-closure git commit is part of the functional output and is
  not an optional release ritual.
- Example-porting remains blocked until that commit exists and the evidence
  package is reviewable.

## Implementation Strategy

1. Inventory all remaining non-driver `geplant` targets and all ledger rows
   that still end in `portiert + Test ausstehend`.
2. Inventory every gate-scoped assembly to confirm it still carries real
   remaining responsibility rather than placeholder-only or no-op-only code; if
   not, restructure or explicitly remove it from gate scope before closure is
   claimed.
   If a module is removed from the hard gate, update the gate-scope
   restructuring package in the same change.
3. Add failing MSTest coverage in the relevant Core/Controls/Serialization/
   Drivers test projects, plus Compatibility-focused tests where existing
   suites are insufficient, for those still-open behaviors.
4. Implement the minimum framework code needed to turn those rows into
   `portiert + getestet`, or explicitly document true replacements / obsolete
   cases where implementation is not warranted.
5. Reconcile `docs/porting-status.md` so every one of the 151 rows ends in an
   allowed final proof state and references reviewable evidence.
6. Run build, full test, format, per-module coverage, and conditional docfx
   validation.
7. Ensure the final coverage evidence reports the five gate assemblies
   separately, even where the same test project contributes to multiple
   assemblies.
   Resolve any local-versus-CI divergence before closure and name the
   authoritative repository-visible result in the evidence package.
8. Refresh compatibility evidence on the two primary Macs and, when materially
   required by the implemented changes, on Linux and Windows/WSL.
9. Update `Pflichtenheft.md` and any remaining gate-review documents so they
   reflect the same closed/open state without contradiction.
10. Create the dedicated gate-closure git commit once all gate conditions are
    satisfied.

## Scenario & Edge-Case Coverage

### Scenario Matrix

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| Remaining Core/Controls/Serialization/Compatibility gaps require real code and tests | Clarifications + User Story 1 | `research.md`, `data-model.md`, contract guarantees, implementation strategy |
| Ledger row moves from `portiert + Test ausstehend` to `portiert + getestet` | User Story 1 | `data-model.md`, contract status rules, quickstart validation flow |
| Ledger row ends as `bewusst ausgelassen + Begruendung` because of true replacement or obsolescence | User Story 1 + FR-005/FR-006 | `research.md`, `data-model.md`, contract guarantees |
| Full gate package must prove Phase-8 readiness in one pass | User Story 2 + SC-003/SC-005 | `plan.md`, `quickstart.md`, contract review surface |
| A test is skipped or ignored without a tracked issue | Edge Cases + FR-008 | `data-model.md`, testing strategy, quickstart review step |
| A coverage report only shows one aggregated percentage instead of five gate assemblies separately | Clarification + FR-009 | `research.md`, `data-model.md`, quickstart coverage flow, contract guarantees |
| A gate-scoped module is still placeholder-only or no-op-only | Clarification + FR-009a | `research.md`, `data-model.md`, contract guarantees, implementation strategy |
| Local and CI coverage disagree for one gate assembly | Clarification + FR-009b | `quickstart.md`, contract guarantees, testing strategy |
| A very small gate-scoped module would satisfy coverage only through placeholder-oriented tests | Clarification + FR-009a | `plan.md`, `research.md`, `data-model.md`, contract guarantees |
| Public API or XML comments change during closure work | User Story 2 + FR-011 | `quickstart.md`, testing strategy, contract documentation rule |
| Linux or Windows/WSL evidence is required because runtime or terminal behavior changed | Clarification + FR-015 | `research.md`, `data-model.md`, quickstart validation flow |
| Linux or Windows/WSL evidence is not applicable because the change is documentation-only | Clarification + SC-006 | `data-model.md`, contract guarantees, quickstart review step |
| Gate is functionally complete but not yet represented by a dedicated commit | Clarification + FR-013 | contract closure rule, implementation strategy step 10 |

### Scenario Coverage Interpretation

- **Primary scenarios**: remaining framework implementation, final proof-state
  reconciliation, and full gate closure.
- **Alternate scenarios**: conscious replacement or obsolete special-case rows
  that do not require code but do require explicit rationale.
- **Exception scenarios**: skipped tests, conflicting evidence, aggregated-only
  coverage reports, or platform caveats discovered during validation.
- **Recovery scenarios**: a gate module is restructured out of scope or a
  local-versus-CI evidence conflict must be resolved before closure.
- **Non-functional scenarios**: 5x-70%-coverage compliance, repository-wide
  test pass, assembly-specific evidence, conditional docfx pass, and
  conditional platform evidence.

## Testing Strategy

- **Framework unit tests**: Expand `tests/TuiVision.Core.Tests`,
  `tests/TuiVision.Controls.Tests`, `tests/TuiVision.Serialization.Tests`, and
  Compatibility-focused coverage first, before implementation, for every
  still-open ledger-backed behavior.
- **Driver regression tests**: Retain and extend `tests/TuiVision.Drivers.Tests`
  because the gate interpretation now requires both full-suite pass status and
  `TuiVision.Drivers.Console` coverage at or above the hard threshold.
- **Example smoke tests**: Include `TuiVision.Examples.SmokeTests` in the
  repository-wide `dotnet test` pass; no new example ports are added here, but
  existing smoke tests still participate in the global gate.
- **Coverage validation**: Collect per-module Coverlet evidence until
  `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, and `TuiVision.Drivers.Console` each reach
  `>= 70 %` line coverage. The evidence package must present those results
  assembly-by-assembly rather than only as one aggregate.
- **Gate-scope integrity review**: Confirm that each counted gate module still
  contains real remaining responsibility. Placeholder-only or no-op-only code
  with trivial tests is not an acceptable closure strategy.
- **Coverage conflict resolution**: If local and CI measurements diverge for a
  gate assembly, closure stays blocked until the discrepancy is explained and
  the authoritative repository-visible result is named in the evidence package.
- **Proof-ledger review**: Treat `docs/porting-status.md` as a first-class
  deliverable and verify that no row remains provisional.
- **Compatibility evidence**: Refresh Linux and Windows/WSL evidence when the
  implemented changes materially affect runtime behavior, terminal behavior,
  portability, or build reliability; otherwise record a reviewable
  not-applicable rationale.
- **Mandatory validation commands before closure**:
  - `dotnet build --configuration Release`
  - `dotnet test`
  - `dotnet format --verify-no-changes`
  - `dotnet test tests/TuiVision.Core.Tests/ --collect:"XPlat Code Coverage"`
  - `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`
  - `dotnet test tests/TuiVision.Serialization.Tests/ --collect:"XPlat Code Coverage"`
  - `dotnet test tests/TuiVision.Drivers.Tests/ --collect:"XPlat Code Coverage"`
- **Compatibility coverage note**: If the existing suites do not exercise
  `TuiVision.Compatibility` deeply enough to clear 70%, add a dedicated
  Compatibility-focused MSTest suite and include it in the same coverage run.
  Shared suites remain acceptable as long as the final report still separates
  the five target assemblies.
- **Conditional validation command**:
  - `docfx docfx.json` when public APIs or XML comments changed

## Success-Criteria Traceability

| Success criterion | Planning hook |
|---|---|
| `SC-001` All 151 rows reach a final proof state | `data-model.md`, contract status rules, implementation strategy steps 1-5 |
| `SC-002` Every `portiert + getestet` row has repository-visible automated evidence | testing strategy, contract review surface, quickstart validation flow |
| `SC-003` All six entrance-gate criteria have explicit status with no undocumented blockers | plan summary, quickstart gate checklist, contract guarantees |
| `SC-004` Core/Controls/Serialization/Compatibility/Drivers.Console each reach `>= 70 %` line coverage with separate assembly-specific evidence and no placeholder-only gate modules | research decisions, testing strategy, quickstart coverage flow |
| `SC-005` Reviewers can decide in one pass whether example wave 1 may start | plan summary, quickstart expected outcomes, dedicated closure commit rule |
| `SC-006` Platform-evidence package includes Linux/Windows/WSL results or a valid N/A rationale | research decisions, data-model validation rules, contract platform evidence rule |

## Complexity Tracking

No constitution violations or justified exceptions are required for this plan.
