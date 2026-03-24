# Data Model: Driver Consolidation and M-07 Porting Proof

## Overview

This feature combines one managed runtime target in `TuiVision.Drivers.Console`
with a documentation-backed proof model in `docs/porting-status.md`. No
database storage is involved. The data model focuses on historical source-file
inventory, capability-oriented consolidation, proof-ledger rows, and
compatibility-validation evidence.

## Entities

### HistoricalImplementationFile

- **Purpose**: Represents one historical `.cc` source file that must appear in
  the formal `M-07` proof set.
- **Key attributes**:
  - Relative source path under `tv203s/contrib/tvision/classes`
  - Source family (`shared`, `dos`, `linux`, `qnx4`, `qnxrtp`, `unix`, `wingr`,
    `winnt`, `x11`)
  - Capability bucket
  - Ancillary native-support references when relevant
- **Relationships**:
  - Maps to exactly one `PortingStatusEntry`
  - May reference zero to many `AncillarySupportFile` notes
- **Validation rules**:
  - Every historical `.cc` file in scope must exist exactly once in the proof
    ledger
  - Source-family naming must remain stable and repository-relative

### AncillarySupportFile

- **Purpose**: Represents a non-`.cc` historical support file that may explain a
  replacement or omission decision or provide include-driven dependency context
  for a mapped `.cc` file.
- **Key attributes**:
  - Relative source path
  - File kind (`.c`, `.h`, other support asset)
  - Explanation note
- **Relationships**:
  - May be referenced by zero to many `HistoricalImplementationFile` entries
- **Validation rules**:
  - Ancillary support files do not replace the formal `.cc` row set
  - If a header or helper C file materially affects the interpretation of an
    in-scope `.cc` file, the linked proof entry must mention that dependency
  - One support file may legitimately be referenced by multiple `.cc` rows
    without becoming a duplicate proof entry itself
  - Notes must remain explanatory rather than becoming undocumented side scope

### DriverCapabilityBucket

- **Purpose**: Represents the review-oriented responsibility group used to
  consolidate historical driver behavior.
- **Key attributes**:
  - Capability name
  - Scope description
  - Expected managed replacement behavior
- **Relationships**:
  - May group zero to many `HistoricalImplementationFile` entries
  - May map to one or more `ManagedTargetArea` entries
- **Validation rules**:
  - Every platform-specific historical driver row must belong to one bucket
  - Overlapping or duplicate bucket intent must be merged or explicitly
    clarified before the proof ledger is considered review-ready
  - Bucket names should stay stable across the ledger for readability

### ManagedTargetArea

- **Purpose**: Represents one current repository-owned destination area that
  receives behavior from the historical implementation inventory.
- **Key attributes**:
  - Target module
  - Target file or file group
  - Role (`primary`, `secondary`)
- **Relationships**:
  - Belongs to exactly one `PortingStatusEntry`
- **Validation rules**:
  - Every proof row has exactly one primary target
  - Secondary targets are optional, but when present they must not duplicate the
    primary target

### PortingStatusEntry

- **Purpose**: Represents one row in `docs/porting-status.md`.
- **Key attributes**:
  - Source path
  - Capability bucket
  - Primary target
  - Optional secondary targets
  - Optional associated support-file references
  - Status (`portiert + getestet`, `portiert + Test ausstehend`,
    `bewusst ausgelassen + Begruendung`)
  - Evidence reference
  - Rationale note
- **Relationships**:
  - Refers to exactly one `HistoricalImplementationFile`
  - Owns one or more `ManagedTargetArea` values
  - May refer to zero or more `EvidenceRecord` items
- **Validation rules**:
  - No entry may remain undocumented or carry a vague placeholder status
  - `bewusst ausgelassen + Begruendung` requires an explicit rationale
  - Split mappings must still preserve exactly one primary target
  - Materially relevant associated `.h`/`.c` files must be visible through the
    row rationale or support-file references
  - Rows with no secondary targets or no support-file references are valid when
    the rationale makes clear that no additional context is required

### EvidenceRecord

- **Purpose**: Represents one proof reference supporting a ledger entry or a
  compatibility claim.
- **Key attributes**:
  - Evidence kind (`test`, `manual-run`, `semi-automated-run`, `code-review`,
    `documented-rationale`)
  - Evidence location
  - Observation summary
- **Relationships**:
  - May support one or more `PortingStatusEntry` rows
  - May belong to one `CompatibilityValidationRun`
- **Validation rules**:
  - Evidence must point to something reviewable in the repository or in the
    documented validation notes for the feature
  - Pure memory-based claims are not acceptable evidence

### CompatibilityValidationRun

- **Purpose**: Represents one explicit validation pass of the managed driver
  baseline on a named environment.
- **Key attributes**:
  - Environment name (`MacBook Air M2`, `Mac mini M4 Pro`, `Linux`, `Windows/WSL`)
  - Validation mode (`local`, `CI`, `manual`, `semi-automated`)
  - Result summary
  - Outstanding caveats
- **Relationships**:
  - May produce zero to many `EvidenceRecord` items
- **Validation rules**:
  - The two macOS machines remain the primary development workflow
  - Linux and Windows/WSL must appear as reviewable validation environments in
    this phase, even if they are not yet hard CI gates

### Phase8GateFollowUp

- **Purpose**: Represents a still-open proof item that belongs to the final
  Phase-8 entrance gate rather than to Phase-7 consolidation itself.
- **Key attributes**:
  - Gate area (`build`, `tests`, `coverage`, `API documentation`, `proof commit`)
  - Current status
  - Next required action
- **Relationships**:
  - May be referenced by zero to many `EvidenceRecord` items
- **Validation rules**:
  - These items must stay explicitly separated from completed Phase-7 work
  - Open follow-up items must remain visible after the feature finishes

## State Transitions

### Porting Status Lifecycle

`inventoried` → `mapped` → `implemented-or-replaced` → `verified`

- `inventoried`: the historical `.cc` file is known and added to the proof set.
- `mapped`: the file has a capability bucket and at least one current target.
- `implemented-or-replaced`: the modern handling is described either as an
  actual port target or as a conscious replacement/omission.
- `verified`: the row has reviewable evidence and no unresolved ambiguity.

### Compatibility Evidence Lifecycle

`planned` → `executed` → `recorded` → `reviewed`

- `planned`: the environment and validation intent are identified.
- `executed`: the managed driver baseline has been exercised on that environment.
- `recorded`: evidence or notes have been captured for repository review.
- `reviewed`: the validation outcome is accepted as part of the feature proof.

### Phase-8 Gate Follow-Up

`identified` → `still-open` / `closed`

- `identified`: the item is known as part of the remaining gate work.
- `still-open`: the item remains outside the current feature scope.
- `closed`: later work finishes the follow-up item and removes it from the open
  gate list.
