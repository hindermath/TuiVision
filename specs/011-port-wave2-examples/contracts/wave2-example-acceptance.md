# Contract: Wave 2 Example Acceptance

## Purpose

This contract records observable acceptance obligations for the wave-2 example
ports. It does not freeze private helper names. Public API changes must keep
German-first and English-second XML documentation complete.

## Common Example Contract

- Provides a runnable example project under `examples/`.
- Uses repository-wide .NET 10/C# defaults.
- Has a normal launch path suitable for manual review.
- Has a deterministic in-process smoke-test path.
- Has one DE-first/EN-second guide under `docs/guides/examples/`.
- Avoids example-local duplication of reusable framework behavior when an
  existing `TuiVision.Controls` or `TuiVision.Serialization` surface exists.
- Records accepted limitations when historical behavior is intentionally
  reduced.

## Smoke-Test Contract

- Each wave-2 example has at least one smoke test.
- Each smoke test triggers one example-specific deterministic interaction.
- Each smoke test verifies visible state or observable public example state.
- Startup plus clean exit alone is not sufficient.
- Smoke tests must not rely on uncontrolled wall-clock timing.
- Smoke tests remain text-first and keyboard-first.

## Per-Example Contract

- `clipboard`: demonstrates copy, cut, paste, input state, and unavailable or
  isolated clipboard behavior.
- `demo`: demonstrates broad controls/dialogs/gadget integration without
  editor, help, terminal, runtime-mouse, or real charset-effect acceptance.
- `dlgdsn`: creates or loads a structured dialog description, renders it,
  demonstrates one simple change, and visibly rejects invalid descriptions.
- `dyntxt`: demonstrates dynamic text or parameter output that updates
  predictably inside constrained view bounds.
- `inplis`: demonstrates input-list behavior with `TInputLine`, synchronized
  selection/history/input state, and keyboard navigation.
- `listvi`: demonstrates `TListViewer`-style list navigation, visible
  selection movement, and boundary handling.
- `progba`: demonstrates deterministic progress through completion.
- `sdlg`: demonstrates historical vertical `ScrollDialog`/`ScrollGroup`
  behavior, focus movement, bounds, and visible control state.
- `sdlg2`: demonstrates historical horizontal and vertical
  `ScrollDialog`/`ScrollGroup` behavior, focus movement, bounds, and visible
  control state.
- `tcombo`: demonstrates combo-box selection, input synchronization, and
  visible selected value.
- `tprogb`: demonstrates progress plus an abort path with a visible canceled
  state.

## Standard-Dialog Contract

- Standard-dialog proof is supplied through `demo`, `dlgdsn`, or another
  historically justified wave-2 flow.
- File and directory dialogs use real local metadata, filters, manual path
  entry, cancellation, and invalid-path handling.
- Standard-dialog acceptance does not include opening, reading, writing, or
  saving file contents.
- Charset selection is documented as omitted or non-acceptance-relevant when no
  ported historical wave-2 flow directly represents it.

## Historical Parity Cleanup Contract

- `sdlg` and `sdlg2` must be accepted in wave 2 for their historical
  ScrollDialog/ScrollGroup behavior.
- Work beyond that purpose is recorded as Historical Example Parity Cleanup.
- Cleanup does not block wave-2 acceptance.
- Cleanup is scheduled no earlier than after mandatory waves 1-4 are complete.

## Proof-Surface Contract

- `Pflichtenheft.md` wave-2 checklist reflects delivered examples.
- The Pflichtenheft next-step marker moves to wave 3 only after all wave-2
  proof is complete.
- `examples/README.md` lists wave-2 examples, original source folders, launch
  commands, and required support assets.
- `docs/project-statistics.md` records the completed implementation phase.
- Architecture evidence under `docs/architecture/` records runtime/example
  readiness, quality scenarios, and risks.
- Security/A11Y evidence records applicable review paths and justified N/A
  decisions.
