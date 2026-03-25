# Data Model: M-07 Closure and Phase-8 Entrance Gate

## Overview

This feature combines framework completion work in `TuiVision.Core`,
`TuiVision.Controls`, `TuiVision.Serialization`, and
`TuiVision.Compatibility`, plus final driver-proof hardening in
`TuiVision.Drivers.Console`, with a repository-local gate-evidence model. No
database storage is involved. The data model focuses on historical source rows,
final proof states, quality-gate results, coverage evidence,
compatibility-validation evidence, and the dedicated gate-closure commit.

## Entities

### HistoricalImplementationFile

- **Purpose**: Represents one historical `.cc` source file from
  `tv203s/contrib/tvision/classes` that must end in a final `M-07` proof state.
- **Key attributes**:
  - Relative source path
  - Source family (`shared`, `dos`, `linux`, `qnx4`, `qnxrtp`, `unix`, `wingr`,
    `winnt`, `x11`)
  - Current target module area
  - Whether the row still points to a `geplant` target
- **Relationships**:
  - Maps to exactly one `PortingStatusEntry`
  - May be supported by zero to many `EvidenceRecord` values
- **Validation rules**:
  - Every historical `.cc` file in scope must appear exactly once in the ledger
  - Non-driver rows mapped to `geplant` targets cannot remain implementation-
    free at closure time unless they become a true replacement or obsolete case

### PortingStatusEntry

- **Purpose**: Represents one row in `docs/porting-status.md`.
- **Key attributes**:
  - Relative source path
  - Primary target
  - Optional secondary targets
  - Final proof state
  - Evidence reference
  - Rationale note
- **Relationships**:
  - Refers to exactly one `HistoricalImplementationFile`
  - May consume one or more `EvidenceRecord` values
- **Validation rules**:
  - Allowed end states are only `portiert + getestet` and
    `bewusst ausgelassen + Begruendung`
  - `portiert + Test ausstehend` is illegal at closure time
  - `bewusst ausgelassen + Begruendung` requires explicit rationale
  - `portiert + getestet` requires repository-visible automated test evidence

### EvidenceRecord

- **Purpose**: Represents one reviewable proof artifact supporting a ledger row
  or a gate criterion.
- **Key attributes**:
  - Evidence kind (`test`, `coverage-report`, `build-run`, `format-run`,
    `doc-run`, `compatibility-run`, `rationale-note`, `commit-reference`)
  - Evidence location
  - Summary observation
- **Relationships**:
  - May support one or more `PortingStatusEntry` rows
  - May belong to one `QualityGateResult`
  - May belong to one `CompatibilityValidationRun`
- **Validation rules**:
  - Evidence must be repository-visible or explicitly linked from repository
    artifacts
  - Memory-only or oral claims are not acceptable

### CoverageResult

- **Purpose**: Represents one measured line-coverage outcome for a required
  module in the Phase-8 entrance gate.
- **Key attributes**:
  - Module name (`TuiVision.Core`, `TuiVision.Controls`,
    `TuiVision.Serialization`, `TuiVision.Compatibility`,
    `TuiVision.Drivers.Console`)
  - Target assembly identity
  - Measurement method
  - Coverage percentage
  - Report location
  - Contributing test projects
  - Evidence source (`local`, `CI`, `mixed`)
  - Conflict state (`clear`, `conflicted`, `resolved`)
  - Authoritative-result marker
  - Pass/fail state
- **Relationships**:
  - Belongs to one `QualityGateResult`
- **Validation rules**:
  - Each of the five required modules must appear exactly once in the gate
    evidence set
  - Coverage percentage must be `>= 70 %` for a passing result
  - The evidence must be readable per target assembly, not only as an
    aggregated repository percentage
  - Conflicted local-versus-CI evidence cannot reach a passing final state
    until one authoritative result is identified

### GateScopedModule

- **Purpose**: Represents one module that is still counted toward the hard
  Phase-8 coverage gate.
- **Key attributes**:
  - Module name
  - Responsibility summary
  - Code status (`active`, `placeholder-only`, `restructured-out`)
  - Gate-inclusion state
- **Relationships**:
  - Owns exactly one `CoverageResult` while included in the hard gate
  - May be referenced by zero to many `HistoricalImplementationFile` rows
- **Validation rules**:
  - A gate-included module cannot remain `placeholder-only` at closure time
  - If a module has no real remaining responsibility, it must be marked
    `restructured-out` before gate closure is claimed
  - A restructured-out module must be removed from the gate-defining proof
    surfaces in the same closure effort

### QualityGateResult

- **Purpose**: Represents one formal entrance-gate criterion result.
- **Key attributes**:
  - Gate area (`build`, `tests`, `coverage`, `format`, `API-doc`, `proof-state`)
  - Current status (`pending`, `passing`, `blocked`)
  - Blocking reason when present
- **Relationships**:
  - Owns zero to many `EvidenceRecord` items
  - Coverage gate owns five `CoverageResult` items
- **Validation rules**:
  - All six gate areas must have an explicit current status
  - No undocumented blocker is allowed at closure time

### CompatibilityValidationRun

- **Purpose**: Represents one execution pass on a named environment relevant to
  the gate.
- **Key attributes**:
  - Environment (`MacBook Air M2`, `Mac mini M4 Pro`, `Linux`, `Windows/WSL`)
  - Trigger reason (`material-runtime-change`, `material-build-change`,
    `not-applicable`)
  - Result summary
  - Not-applicable rationale when needed
- **Relationships**:
  - May produce zero to many `EvidenceRecord` items
- **Validation rules**:
  - The two macOS environments remain the primary workflow evidence
  - Linux and Windows/WSL must be refreshed when changes materially affect
    runtime behavior, terminal behavior, portability, or build reliability
  - If not refreshed, a reviewable not-applicable rationale is required

### GateClosureCommit

- **Purpose**: Represents the dedicated git commit that records the closed
  Phase-8 entrance gate.
- **Key attributes**:
  - Commit hash
  - Commit message
  - Referenced proof artifacts
  - Closure timestamp
- **Relationships**:
  - Closes one `QualityGateResult` set
  - May reference many `EvidenceRecord` items
- **Validation rules**:
  - Exactly one dedicated gate-closure commit is required for the final claim
  - The commit must reference or point to the supporting proof artifacts

## State Transitions

### Porting Status Lifecycle

`inventoried` -> `implemented-or-replaced` -> `tested-or-rationalized` -> `finalized`

- `inventoried`: the historical row exists in `docs/porting-status.md`
- `implemented-or-replaced`: the modern handling is known
- `tested-or-rationalized`: the row has automated test proof or explicit
  replacement / obsolete rationale
- `finalized`: the row ends in one of the two allowed final proof states

### Coverage Lifecycle

`planned` -> `measured` -> `passing` / `blocked` / `conflicted`

- `planned`: the module is identified as part of the hard gate
- `measured`: a Coverlet-backed result exists and is attributable to one target
  assembly
- `passing`: the module is at least 70% line coverage
- `blocked`: the module is below threshold or missing evidence
- `conflicted`: multiple repository-visible measurements disagree and the
  authoritative result is not yet named

### Gate-Scoped Module Lifecycle

`identified` -> `confirmed-active` / `restructured-out` -> `eligible-for-closure`

- `identified`: the module is currently listed in the hard gate
- `confirmed-active`: the module still carries real remaining responsibility
- `restructured-out`: the module is explicitly removed from gate scope by
  restructuring or scope correction, and the proof surfaces are updated
- `eligible-for-closure`: the module is active and backed by valid coverage
  evidence

### Entrance-Gate Lifecycle

`open` -> `evidenced` -> `review-ready` -> `closed`

- `open`: one or more gate criteria are still missing
- `evidenced`: all criteria have explicit status and supporting artifacts
- `review-ready`: no undocumented blocker remains and all hard thresholds pass
- `closed`: the dedicated gate-closure commit exists
