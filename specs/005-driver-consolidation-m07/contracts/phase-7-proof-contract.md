# Contract: Phase-7 Driver and M-07 Proof Surface

## Purpose

Define the externally reviewable contract for the Phase-7 increment: the
managed driver baseline in `TuiVision.Drivers.Console` and the proof ledger in
`docs/porting-status.md`. The contract fixes observable responsibilities and
review rules more strongly than final internal helper names.

## Managed Driver Baseline Contract

### `TuiVision.Drivers.Console`

- Acts as the single managed runtime target for the remaining historical driver
  responsibilities that still matter to the project.
- Consolidates historical platform-specific behavior by capability rather than
  by preserving one managed branch per old operating-system directory.
- Must remain free of native bindings, OS-specific packages, or runtime
  requirements beyond managed .NET 10.
- May add or refine internal or public supporting types where needed to express
  the consolidated behavior clearly and testably.

### Behavioral Guarantees

1. **Managed-only guarantee**: No Phase-7 driver behavior depends on native
   bindings or external terminal libraries outside the managed .NET runtime.
2. **Capability-consolidation guarantee**: Historical driver behavior is
   reviewed and implemented as capability buckets such as presentation, input,
   display adaptation, or terminal handling rather than as one-to-one platform
   clones.
3. **Regression guarantee**: Driver changes remain covered by explicit MSTest
   validation in `tests/TuiVision.Drivers.Tests` and by any impacted
   cross-module regression tests.
4. **Compatibility-evidence guarantee**: The feature records validation across
   the primary Multi-Mac workflow and additionally on Linux and Windows/WSL,
   even if the latter are still manual or semi-automated in this increment.

## `docs/porting-status.md` Proof Ledger Contract

### Required Row Scope

- The formal row set is every historical `.cc` implementation file under
  `tv203s/contrib/tvision/classes`, including the platform-specific driver
  subdirectories.
- Historical `.c` and `.h` support files are not mandatory ledger rows, but may
  be named inside rationale notes or explicit support-file references when they
  explain a conscious replacement, omission, or include-driven dependency.

### Required Row Fields

Every proof row must contain:

1. Relative source path
2. Capability bucket
3. One mandatory primary target module or file group
4. Optional secondary targets
5. One canonical status value
6. Evidence reference
7. Rationale note where required

Where relevant, a row must also contain:

8. Associated support-file references for materially relevant `.h`/`.c`
   dependencies

### Canonical Status Values

Allowed row states are:

- `portiert + getestet`
- `portiert + Test ausstehend`
- `bewusst ausgelassen + Begruendung`

No placeholder such as `TODO`, `später`, `offen`, or an undocumented blank
status is acceptable in the finished ledger.

### Mapping Guarantees

1. **Completeness guarantee**: Every historical `.cc` file appears exactly once
   in the ledger.
2. **Primary-target guarantee**: Every row has exactly one primary target.
3. **Secondary-target guarantee**: Additional targets are optional and may be
   listed only when they add real traceability.
4. **Replacement guarantee**: Any consciously replaced or omitted historical
   behavior carries an explicit rationale.
5. **Dependency-context guarantee**: If a historical `.cc` file depends on
   materially relevant `.h` or `.c` support files, that dependency context is
   made visible in the row instead of being left implicit.
6. **Reviewability guarantee**: A reviewer can inspect the ledger without
   reconstructing mappings from commit history or ad-hoc searches.

## Test and Review Obligations

- The driver implementation side must start with failing MSTest coverage before
  production changes are added.
- `tests/TuiVision.Drivers.Tests` is the primary test home for this increment.
- `docs/porting-status.md` must be reviewed as a first-class deliverable, not as
  an optional afterthought.
- If public or non-public API members change in `TuiVision.Drivers.Console`,
  bilingual XML documentation remains mandatory, and `docfx docfx.json` must run
  when the repository-level documentation gate applies.
