# Research: Port Wave 2 Examples

## Decision 1: Use one managed project per required historical example

**Decision**: Create one example project under `examples/` for each wave-2
example: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`,
`Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.

**Rationale**: The Pflichtenheft requires traceability from each historical
example to one delivered managed example, guide, and smoke scenario. One
project per example matches the wave-1 pattern and keeps review simple.

**Alternatives considered**:
- One combined wave-2 demo project: rejected because it would hide per-example
  proof and make guides/smoke tests less traceable.
- Example-only source files inside a shared project: rejected because it would
  weaken parity with the existing wave-1 layout.

## Decision 2: Extend in-process smoke tests instead of process spawning

**Decision**: Extend `tests/TuiVision.Examples.SmokeTests/` with one smoke-test
class per new example and headless/deterministic test hooks in the example
applications.

**Rationale**: Wave 1 already established in-process MSTest smoke coverage. It
is faster, deterministic, and easier to inspect than shelling out to `dotnet
run`.

**Alternatives considered**:
- Spawn each example as a child process: rejected because terminal I/O and
  process timing would make smoke tests less deterministic.
- Only run startup/exit smoke tests: rejected by the clarified spec because
  every smoke test must trigger an example-specific visible interaction.

## Decision 3: Keep framework hardening incidental and blocking-only

**Decision**: Implement new reusable framework behavior only when a wave-2
example cannot meet acceptance without it.

**Rationale**: Features `008`, `009`, and `010` already prepared controls,
widgets, standard dialogs, and dialog-description readiness. This feature is an
example-port/proof feature, not a broad framework redesign.

**Alternatives considered**:
- Reopen the full Controls/Dialog architecture: rejected because it would
  expand beyond the next-step marker.
- Implement example-local substitutes for missing controls: rejected because it
  would duplicate framework responsibilities and reduce example value.

## Decision 4: Treat `sdlg` and `sdlg2` as scrollable-dialog examples

**Decision**: Complete `sdlg` and `sdlg2` in wave 2 for their historical
`ScrollDialog`/`ScrollGroup` purpose. Broader parity work is recorded as
Historical Example Parity Cleanup after mandatory waves 1-4.

**Rationale**: The original `sdlg` and `sdlg2` sources demonstrate scrollable
dialog containers, not file/color/charset standard dialogs. Keeping their real
purpose in wave 2 prevents incorrect scope from leaking into planning.

**Alternatives considered**:
- Keep them as standard-dialog examples: rejected because it contradicts the
  historical sources.
- Defer their completion: rejected because they are explicit wave-2 checklist
  items.

## Decision 5: Assign standard-dialog proof to `demo` and `dlgdsn`

**Decision**: Standard-dialog proof is demonstrated through `demo` and
`dlgdsn`, not through `sdlg`/`sdlg2` and not through any unnamed third flow.

**Rationale**: `demo` contains broad control/dialog workflows, and `dlgdsn`
contains dynamic dialog and file-dialog usage. This aligns proof with actual
historical responsibilities. Removing the open-ended "or another historically
justified wave-2 flow" clause prevents dead optionality across the artifacts
because no third example was ever named.

**Alternatives considered**:
- Force standard dialogs into `sdlg`/`sdlg2`: rejected as artificial behavior.
- Drop standard-dialog proof from wave 2: rejected because the wave is
  "Controls and Dialogs" and feature `010` prepared that surface.
- Keep the open-ended "or another historically justified wave-2 flow" clause:
  rejected because no third example exists, and the open clause invited
  inconsistency between artifacts.

## Decision 6: Keep file-content I/O out of standard-dialog acceptance

**Decision**: File and directory flows inspect metadata, filters, manual paths,
cancellation, and invalid paths; they do not open, read, write, save, delete, or
overwrite file contents.

**Rationale**: File-content behavior belongs to editor/file features and later
examples. Dialog examples should return visible decisions and validation state.

**Alternatives considered**:
- Read sample files for demonstration: rejected because it would pull editor/file
  content responsibility into wave 2.
- Use fake file-system data only: rejected because the spec requires real local
  metadata for standard-dialog proof.

## Decision 7: Use existing Serialization/resource primitives for `dlgdsn`

**Decision**: `dlgdsn` uses existing project-owned Serialization/resource
primitives for any persisted structured dialog-description fixture.

**Rationale**: Feature `010` prepared a bounded dialog-description model and
malformed-input rejection. A new file format or dependency would create a
second persistence path without acceptance value.

**Alternatives considered**:
- Add JSON for designer fixtures: rejected because no new dependency or external
  format is needed.
- Skip persisted/dynamic proof: rejected because `dlgdsn` acceptance requires
  structured description load/create, render, modification, and invalid
  rejection.

## Decision 8: Make text-first and keyboard-first proof mandatory

**Decision**: All example interactions and guides must be usable without mouse
support and without relying on color or layout alone.

**Rationale**: The project constitution and example guide rules require
accessibility-first terminal behavior and learner-friendly documentation.
Runtime mouse support is outside wave-2 acceptance.

**Alternatives considered**:
- Use mouse interactions in smoke tests: rejected because runtime mouse is not a
  wave-2 acceptance requirement.
- Treat accessibility only as documentation cleanup: rejected because A11Y is a
  formal completion criterion.

## Decision 9: Keep architecture/security evidence lightweight but explicit

**Decision**: Plan lightweight architecture evidence under `docs/architecture/`
and proportional security applicability review through existing
`docs/security/` files.

**Rationale**: Wave 2 changes runtime example behavior, proof surfaces, and
technical debt visibility, but it does not create a web/API/auth service or new
external dependency.

**Alternatives considered**:
- No architecture/security evidence: rejected by the updated constitution.
- Full service-grade ASVS/Zero-Trust package: rejected as not applicable to
  local terminal examples.

## Decision 10: Update proof surfaces in the implementation phase

**Decision**: Treat `Pflichtenheft.md`, `examples/README.md`, guides,
`docs/project-statistics.md`, architecture/security/A11Y evidence, and agent
context as part of feature completion.

**Rationale**: The repository treats traceability and learner-facing proof as
formal deliverables. Leaving them to a later cleanup would make wave completion
ambiguous.

**Alternatives considered**:
- Ship code/tests first and update docs later: rejected because wave completion
  depends on proof and guides.
- Keep statistics outside feature scope: rejected by repository governance.

## Decision 11: Provide `TScrollGroup` as reusable framework foundation for `sdlg`/`sdlg2`

**Decision**: Add a managed `TScrollGroup` (and a thin `TScrollableDialog`
where needed) under `src/TuiVision.Controls/` so that `sdlg` and `sdlg2`
compose scrollable dialog containers from one reusable framework surface
instead of re-implementing scroll behavior in example projects. The decision is
recorded as ADR `docs/architecture/adr/0001-tscrollgroup-foundation.md`.

**Rationale**: The original `tv203s/contrib/tvision/examples/sdlg/scrlgrp.cpp`
and `sdlg2/scrlgrp.cpp` implement custom `ScrollGroup`/`ScrollDialog` classes;
the managed C# framework currently exposes only `TScrollBar` and `TScroller`.
The Common Example Contract forbids example-local duplication of reusable
framework behavior, so the historical `sdlg`/`sdlg2` design implies a missing
framework surface rather than an implementation detail. Pre-deciding this
surface keeps `sdlg`/`sdlg2` honest, prevents three different example-local
seams across parallelized teams, and lets later waves reuse the same surface.

**Alternatives considered**:

- Leave the surface contingent on T042/T043 ("only if a blocker is found"):
  rejected because the historical sources make a custom scrollable container
  the structurally certain, not speculative, blocker.
- Implement scrollable behavior example-locally in `examples/Sdlg/` and
  `examples/Sdlg2/`: rejected because it duplicates framework responsibilities
  and weakens later wave reuse.
- Reopen the full Controls/Dialog architecture: rejected as out of scope; only
  the minimal `TScrollGroup` (plus optional `TScrollableDialog`) surface is
  added.
