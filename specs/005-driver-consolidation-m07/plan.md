# Implementation Plan: Driver Consolidation and M-07 Porting Proof

**Branch**: `005-driver-consolidation-m07` | **Date**: 2026-03-23 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/005-driver-consolidation-m07/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/005-driver-consolidation-m07/spec.md)
**Input**: Feature specification from `/specs/005-driver-consolidation-m07/spec.md`

**Note**: This plan covers the Phase-7 framework step only: consolidating the remaining historical driver responsibilities into `TuiVision.Drivers.Console`, expanding driver-focused validation, and creating the proof ledger `docs/porting-status.md` required for `M-07`. Mandatory example waves remain outside this planning step.

## Summary

Stabilize the managed console-driver baseline in `src/TuiVision.Drivers.Console`, expand `tests/TuiVision.Drivers.Tests` so remaining driver behavior is validated with MSTest-first coverage, and build `docs/porting-status.md` as the canonical proof ledger for every historical `.cc` implementation file in `tv203s/contrib/tvision/classes`. The design treats the historical per-OS drivers as capability inputs rather than one-to-one targets, keeps the project inside the existing five-module architecture, and separates three outcomes cleanly: driver consolidation, M-07 evidence, and the still-following Phase-8 entrance-gate closure. Associated historical `.h` and `.c` files are not promoted to standalone `M-07` ledger rows, but they must be reviewed and referenced wherever they explain include dependencies, data structures, register models, or conscious replacement decisions behind a `.cc` mapping.

## Terminology & Operational Definitions

- **Historical implementation file**: One `.cc` source file from `tv203s/contrib/tvision/classes`, including platform-specific subdirectories, that must appear in the M-07 proof ledger.
- **Associated support file**: A historical `.h`, `.c`, or similar non-`.cc` file that is technically tied to one or more `.cc` implementation files and may provide declarations, constants, data layouts, or helper logic that must be understood for correct mapping.
- **Ancillary native-support file**: A non-`.cc` support file such as the DOS-side `.c` or `.h` assets. These are not formal `M-07` row items on their own, but they must be reviewable as dependency or rationale context for the affected `.cc` entries.
- **Driver capability bucket**: A review-oriented grouping such as screen presentation, keyboard translation, mouse handling, display capability handling, or terminal-mode adaptation that explains how historical drivers collapse into the managed baseline.
- **Managed driver baseline**: The current runtime surface in `TuiVision.Drivers.Console` that provides terminal interaction without native bindings.
- **Primary target**: The one review-leading target module or file group assigned to a historical implementation file in `docs/porting-status.md`.
- **Secondary target**: An additional target module or file listed when a historical source contributes to more than one maintained area.
- **Proof ledger**: The human-readable `docs/porting-status.md` document that maps each historical `.cc` file to current targets, status, evidence, rationale, and where needed the associated non-`.cc` dependency context.
- **Compatibility evidence**: Reviewable proof that the managed driver baseline was exercised on the primary Multi-Mac workflow and additionally on Linux and Windows/WSL, even if the latter are still manual or semi-automated in this phase.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation validation; GitHub Actions for existing CI  
**Storage**: Source-controlled Markdown evidence in `docs/porting-status.md`; no database storage; compatibility evidence may include repository notes and command output references  
**Testing**: MSTest-first coverage in `tests/TuiVision.Drivers.Tests` plus impacted repository-wide suites; validation via `dotnet build --configuration Release`, targeted `dotnet test` runs, repository `dotnet test`, `dotnet format --verify-no-changes`, and coverage collection; Linux and Windows/WSL compatibility evidence may still be manual or semi-automated in this phase  
**Target Platform**: Managed cross-platform terminal runtime on macOS, Linux, and Windows; Windows validation preferably through WSL with current Ubuntu 24.04 in addition to native compatibility checks where practical  
**Project Type**: Managed .NET library framework increment with a documentation-backed completeness proof artifact  
**Performance Goals**: Managed driver operations such as resize, presentation, and core terminal-state transitions remain interactive single-cycle actions for local terminal workflows; the M-07 proof ledger remains reviewable in one pass without ad-hoc repository archaeology  
**Constraints**: No native bindings; no new source modules; the mandatory 25 original examples remain out of scope; `docs/porting-status.md` must cover the formal `.cc` implementation set with one required primary target per row while also recording associated `.h`/`.c` dependency context where relevant; Linux and Windows/WSL evidence is required but does not yet have to be a mandatory CI gate; documentation and XML comments must remain bilingual and CEFR-B2; quality gates remain aligned with the constitution  
**Scale/Scope**: One source module (`src/TuiVision.Drivers.Console`), one existing driver test project (`tests/TuiVision.Drivers.Tests`), one new proof ledger in `docs/`, and a full mapping pass across the historical `classes/` implementation inventory including the platform-specific driver subdirectories and their associated non-`.cc` support files as dependency context

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. The plan keeps all runtime work inside `TuiVision.Drivers.Console` using managed .NET APIs only and treats historical native or OS-specific behavior as conceptual input, not as a native dependency to reproduce.
- **Test-First Development — TDD**: Pass with explicit workflow requirement. Driver-consolidation work must begin with failing MSTest coverage in `tests/TuiVision.Drivers.Tests` and any impacted integration suites before implementation or refactoring proceeds.
- **Didactic and Linguistic Clarity**: Pass. The feature adds review-oriented design artifacts, a proof ledger, and likely driver-surface documentation updates; all changed API and non-public members remain subject to bilingual XML documentation.
- **Modular Architecture**: Pass. Runtime behavior remains inside the existing `TuiVision.Drivers.Console` module and its allowed dependencies; no additional source assembly is introduced.
- **Cross-Platform Portability**: Pass. The managed driver baseline continues to target macOS, Linux, and Windows without native installations; Linux and Windows/WSL compatibility checks are captured as explicit evidence requirements in this plan.
- **License & Disclaimer Integrity**: Pass. Historical files under `tv203s/` remain unmodified and are used only as analysis/proof input; new repository-owned documentation and code remain under the existing project conventions.

**Post-Design Gate Review**: Phase-1 design artifacts keep the feature within the existing five-module architecture, do not introduce a new runtime storage layer, preserve the primary Multi-Mac workflow, and treat Linux/Windows/WSL validation as explicit compatibility evidence rather than a deferred assumption. No constitution exception is required.

- The feature changes validation expectations and proof artifacts; Linux and Windows/WSL compatibility evidence is therefore explicitly required in addition to the primary Multi-Mac workflow.
- The feature does not advance any of the 25 mandatory original examples from `tv203s/contrib/tvision/examples`; it prepares the framework baseline and proof required before those waves may begin.
- Statistical-documentation impact identified; update `docs/project-statistics.md` after these planning artifacts and any synchronized agent context changes are written.

## Project Structure

### Documentation (this feature)

```text
specs/005-driver-consolidation-m07/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── phase-7-proof-contract.md
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
│   ├── TConsoleDriver.cs
│   ├── Class1.cs
│   └── additional purpose-named driver-support types (planned)
├── TuiVision.Serialization/
└── TuiVision.Compatibility/

tests/
├── TuiVision.Drivers.Tests/
│   ├── Test1.cs
│   └── additional driver-consolidation tests (planned)
├── TuiVision.Core.Tests/
├── TuiVision.Controls.Tests/
├── TuiVision.Serialization.Tests/
└── TuiVision.Examples.SmokeTests/

docs/
├── project-statistics.md
├── guides/
│   └── multi-mac-workflow.md
└── porting-status.md              # planned proof ledger

tv203s/
└── contrib/tvision/classes/
    ├── *.cc
    ├── dos/
    │   ├── *.cc
    │   ├── *.c
    │   └── *.h
    ├── linux/
    ├── qnx4/
    ├── qnxrtp/
    ├── unix/
    ├── wingr/
    ├── winnt/
    └── x11/
```

**Structure Decision**: Keep the feature inside the existing `TuiVision.Drivers.Console` module and `tests/TuiVision.Drivers.Tests`, and introduce `docs/porting-status.md` as the canonical human-readable proof artifact. No new source module or separate proof-tooling executable is needed.

## Phase 0 Research

Phase-0 research resolves the planning choices that would otherwise remain implicit:

1. How the historical per-OS drivers should be collapsed into capability-oriented managed behavior instead of copied one-for-one.
2. What the formal scope of the `M-07` ledger is, especially where historical directories also contain associated `.c` and `.h` support files.
3. Which ledger schema makes `docs/porting-status.md` simultaneously complete, reviewable, and easy to maintain.
4. How far compatibility evidence on Linux and Windows/WSL must go in this increment before it becomes a future CI concern.
5. Whether driver validation should stay in the existing driver test project or be pushed into other suites.

## Phase 1 Design Overview

- The managed console-driver baseline remains centered in `TuiVision.Drivers.Console`, but the feature is expected to expand beyond the current minimal `TConsoleDriver` by adding or refactoring purpose-named supporting types for capability mapping, input/output handling, or terminal-state behavior where the historical inventory proves that the current surface is incomplete. The placeholder file `Class1.cs` must not become a long-term catch-all container.
- Historical OS-specific source files are analyzed by capability bucket rather than by platform lineage. The plan treats screen presentation, keyboard handling, mouse handling, display adaptation, and terminal capability handling as the main review buckets for consolidation.
- `docs/porting-status.md` becomes the single canonical proof ledger for `M-07`. Each historical `.cc` file receives one row with source path, capability bucket, mandatory primary target, optional secondary targets, status, evidence reference, rationale, and where needed explicit references to associated `.h`/`.c` support files that shaped the mapping decision.
- The formal acceptance row set for the ledger is the `.cc` implementation inventory named in the Pflichtenheft. Associated `.h` and `.c` files found in historical subdirectories must still be reviewed for includes, constants, structs, register layouts, or helper logic and must be referenced from the affected `.cc` rows whenever they materially influence the managed replacement or omission rationale.
- The feature will likely touch documentation and proofs more heavily than public surface area. If public or non-public API changes occur in `TuiVision.Drivers.Console`, XML documentation and conditional `docfx` regeneration remain part of the gate.
- Driver validation stays anchored in `tests/TuiVision.Drivers.Tests`, which will grow from the current narrow `TConsoleDriver` coverage into a broader phase-7 safety net. Impacted cross-module tests may be added or updated only where behavior leaks into shell integration.
- Linux and Windows/WSL compatibility evidence is planned as a reviewable artifact in this increment. The evidence may come from documented command runs, notes, or semi-automated scripts; converting that evidence into a mandatory CI gate is explicitly deferred beyond this plan unless implementation naturally enables it. The primary Multi-Mac evidence set must name both `MacBook Air M2` and `Mac mini M4 Pro` explicitly instead of treating macOS validation as an unnamed aggregate.

### Responsibility Boundaries

- `TuiVision.Drivers.Console` owns the managed runtime behavior that replaces or consolidates the historical platform-specific driver code.
- `docs/porting-status.md` owns the human-readable M-07 completeness proof; it does not replace executable tests but complements them.
- `tests/TuiVision.Drivers.Tests` owns failing-first and regression validation for managed driver behavior; it does not serve as the only M-07 proof because mapping and conscious omissions must remain human-reviewable.
- Associated `.h`/`.c` support files do not become independent proof rows, but the plan treats them as mandatory dependency context for the linked `.cc` rows whenever they explain what a historical implementation file actually depended on.
- `TuiVision.Controls`, `TuiVision.Core`, `TuiVision.Serialization`, and `TuiVision.Compatibility` may appear as target modules in the ledger, but this feature does not broaden their phase scope beyond what the mapping or minimal bug-fix fallout requires.
- Mandatory example waves remain outside the implementation scope of this plan; the feature prepares, but does not itself close, the full Phase-8 entrance gate.

### Customization Boundary for This Increment

- The increment may refine the shape of the managed driver surface, but it must not introduce native adapters, new source assemblies, or platform-specific runtime forks.
- The proof ledger must stay review-oriented and repository-local; no external spreadsheet or hidden local-only artifact is acceptable.
- Internal helper types may be introduced inside `TuiVision.Drivers.Console` if they clarify managed driver responsibilities, but broad architectural expansion outside the driver scope is not justified in this increment.
- If implementation reveals historical behavior that belongs more naturally to another existing module, the ledger may assign that file a non-driver primary target or a driver primary target with secondary targets, but the rationale must make the split explicit.
- If an associated `.h` or `.c` file materially changes the interpretation of a `.cc` row, that dependency must be made visible in the row rationale instead of being left implicit.

## Implementation Strategy

1. Add failing MSTest coverage in `tests/TuiVision.Drivers.Tests` for the remaining managed driver behaviors that are required to replace the unresolved historical driver responsibilities.
2. Audit the historical `tv203s/contrib/tvision/classes` inventory, focusing first on the platform-specific subdirectories and driver-adjacent files that still influence Phase 7.
3. For every in-scope `.cc` file, inspect the associated `.h`/`.c` context where present so include-driven constants, structs, hardware models, or helper routines are reflected in the mapping rationale.
4. Expand or refactor `src/TuiVision.Drivers.Console` until the managed driver baseline covers or consciously replaces the identified capability buckets without native dependencies.
5. Create `docs/porting-status.md` as the canonical proof ledger and fill it across the entire `.cc` inventory with target mapping, status, evidence, rationale, and references to associated `.h`/`.c` files where they materially influenced the conclusion.
6. Capture explicit Linux and Windows/WSL compatibility evidence alongside the primary Multi-Mac validation results.
7. Re-run the required build, test, format, coverage, and conditional documentation gates.
8. Review the resulting artifacts against the remaining Phase-8 gate items so the next planning step can distinguish completed proof from still-open gate work.

## Scenario & Edge-Case Coverage

### Scenario Matrix

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| Managed driver replaces unresolved historical screen behavior | User Story 1 | `research.md`, `data-model.md`, `contracts/phase-7-proof-contract.md`, driver tests |
| Managed driver replaces unresolved keyboard or mouse behavior | User Story 1 | `research.md`, `data-model.md`, driver tests, plan testing strategy |
| Historical driver file maps to one primary and multiple secondary targets | Clarification + User Story 2 | `data-model.md`, `contracts/phase-7-proof-contract.md`, `docs/porting-status.md` implementation tasks |
| Historical file is consciously replaced rather than ported one-to-one | User Story 2 | `research.md`, `contracts/phase-7-proof-contract.md`, proof-ledger rules |
| Historical `.cc` inventory is complete and leaves no undocumented gaps | User Story 2 + SC-001/SC-004 | `contracts/phase-7-proof-contract.md`, `quickstart.md`, eventual proof-ledger review |
| Historical associated `.c` or `.h` files materially change the reading of a `.cc` file | Edge Cases | `research.md`, `data-model.md`, ledger rationale rules |
| One associated `.h` or `.c` file is shared by multiple `.cc` rows | Edge Cases | `data-model.md`, `contracts/phase-7-proof-contract.md`, ledger support-file references |
| A proof row legitimately has no secondary target and no support-file reference | Alternate / Zero-state | `contracts/phase-7-proof-contract.md`, `quickstart.md`, ledger row rules |
| Linux or Windows/WSL shows a compatibility caveat not seen on macOS | Clarification + Edge Cases | `data-model.md`, `quickstart.md`, compatibility evidence records |
| Phase 7 closes while Phase-8 gate still has other open proof items | User Story 3 + FR-010 | `plan.md`, `data-model.md`, quickstart review steps |

### Scenario Class Coverage Interpretation

- **Primary scenarios**: driver capability consolidation, complete `.cc` ledger coverage, and proof-ledger traceability.
- **Alternate scenarios**: split mappings with primary/secondary targets and proof rows that legitimately have no secondary targets or support-file references.
- **Exception scenarios**: consciously replaced historical files, platform-specific caveats, and rows whose rationale depends on associated `.h`/`.c` context.
- **Recovery scenarios**: compatibility validation or mapping review finds a caveat and records it without collapsing the entire Phase-7 outcome into an undocumented gap.
- **Non-functional scenarios**: managed-only runtime compliance, reviewability of the proof ledger, and compatibility evidence across Multi-Mac plus Linux/Windows/WSL.

### Reviewer Readiness Criteria

- Reviewers must be able to point to a written artifact for each of these concerns before tasks are generated:
  - capability-oriented consolidation of historical driver folders
  - exact scope and row schema of `docs/porting-status.md`
  - handling of split mappings with one primary and optional secondary targets
  - treatment of consciously replaced or omitted historical driver files
  - treatment of associated non-`.cc` support files such as headers and helper C sources
  - required compatibility evidence on Linux and Windows/WSL
  - the distinction between Phase-7 completion and full Phase-8 gate completion
- If any of those items are only implied instead of explicitly described in a design artifact, the plan is not review-ready.

## Testing Strategy

- **Driver unit tests**: Expand `tests/TuiVision.Drivers.Tests` beyond the current resize/presenter basics to cover the managed behaviors that replace unresolved historical driver responsibilities.
- **Cross-module regression tests**: Update `tests/TuiVision.Controls.Tests` or other suites only where driver changes materially affect shell/runtime behavior.
- **Proof-ledger review**: Treat `docs/porting-status.md` as a mandatory review artifact with a completeness pass against the historical `.cc` inventory and an explicit dependency-context pass for associated `.h`/`.c` files.
- **Compatibility evidence**: Capture reviewable Linux and Windows/WSL validation evidence in addition to the primary Multi-Mac workflow; in this phase that evidence may still be manual or semi-automated.
- **Negative proof cases**: Verify that no ledger row remains undocumented, no historical `.cc` file lacks a status, no associated support file materially influencing a row is left unmentioned, and no consciously replaced behavior is left without rationale.
- **Mandatory validation commands before merge**:
  - `dotnet build --configuration Release`
  - `dotnet test tests/TuiVision.Drivers.Tests/`
  - `dotnet test`
  - `dotnet format --verify-no-changes`
  - `dotnet test --collect:"XPlat Code Coverage"`
- **Conditional validation command**:
  - `docfx docfx.json` when public APIs or XML comments changed
- **Coverage gate interpretation**: The current project gate requires at least 70% line coverage in `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`. This feature adds driver-focused tests and may use coverage collection for local evidence, but it does not weaken the existing repository-wide gate model.

### Success-Criteria Traceability

| Success criterion | Planning hook |
|---|---|
| `SC-001` Complete ledger coverage for all historical `.cc` files | `contracts/phase-7-proof-contract.md`, `quickstart.md`, implementation strategy step 5 |
| `SC-002` Every platform-specific driver entry states covered/merged/replaced | `research.md`, `data-model.md`, contract status rules |
| `SC-003` Reviewers can explain remaining gap between Phase 7 and Phase 8 | plan summary, FR-010 handling, quickstart review step |
| `SC-004` Zero undocumented historical files or unexplained omissions | proof-ledger schema, negative proof cases, reviewer readiness criteria |

## Complexity Tracking

No constitution violations or justified exceptions are required for this plan.
