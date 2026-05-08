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
  reduced, including the affected behavior, rationale, acceptance impact,
  earliest follow-up point, and traceable reference.

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
  editor, help, stream, terminal, runtime-mouse, or real charset-effect
  acceptance.
- `dlgdsn`: creates or loads a structured dialog description, renders it,
  demonstrates one simple change, and visibly rejects malformed, incomplete,
  duplicate-control, or invalid-navigation descriptions.
- `dyntxt`: demonstrates dynamic text or parameter output that updates
  predictably inside constrained view bounds.
- `inplis`: demonstrates input-list behavior with `TInputLine`, synchronized
  selection/history/input state, keyboard navigation, and empty or minimal list
  contents.
- `listvi`: demonstrates `TListViewer`-style list navigation, visible
  selection movement, empty-list handling, first/last item boundary handling,
  and viewport-sized content.
- `progba`: demonstrates deterministic progress through completion.
- `sdlg`: demonstrates historical vertical `ScrollDialog`/`ScrollGroup`
  behavior, focus movement, bounds, and visible control state.
- `sdlg2`: demonstrates historical horizontal and vertical
  `ScrollDialog`/`ScrollGroup` behavior, focus movement, bounds, and visible
  control state.
- `tcombo`: demonstrates combo-box selection, input synchronization, and
  visible selected value, including empty and boundary-sized choice lists.
- `tprogb`: demonstrates progress plus an abort path with a visible canceled
  state.

## Standard-Dialog Contract

- Standard-dialog proof is supplied through `demo` and `dlgdsn`; no third
  example is admissible as a standard-dialog acceptance vehicle in wave 2.
- File and directory dialogs use real local metadata, filters, manual path
  entry, cancellation, and invalid-path handling.
- Standard-dialog acceptance does not include opening, reading, writing, or
  saving file contents, and it does not include deleting or overwriting files.
- Charset selection is documented as omitted or non-acceptance-relevant when no
  ported historical wave-2 flow directly represents it.

## Interaction-Family Contract

- Clipboard proof is owned by `clipboard`.
- List/input/history proof is owned by `inplis` and `listvi`.
- Combo-box proof is owned by `tcombo`.
- Progress proof is owned by `progba` and `tprogb`.
- Dynamic-text proof is owned by `dyntxt`.
- Scrollable-dialog proof is owned by `sdlg` and `sdlg2`.
- Standard-dialog proof is owned by `demo` and `dlgdsn`; no third example is
  admissible.
- Dynamic-dialog-design proof is owned by `dlgdsn`.
- Broad integration proof is owned by `demo`.

## Boundary And Failure Contract

- Dialog flows must expose success and non-success states: cancel, close,
  invalid selection, and failed validation where the flow supports them.
- File and directory dialog failures remain metadata/validation failures; they
  must not become file-content operations.
- Dynamic dialog descriptions reject malformed, incomplete, duplicate-control,
  or invalid-navigation descriptions visibly.
- List, combo, dynamic-text, and progress examples cover empty, very small, or
  boundary-sized content where the historical flow allows it.
- Clipboard examples expose unavailable or isolated clipboard state as a visible
  result instead of silently skipping the behavior.
- Host-sensitive historical behavior is either represented deterministically or
  recorded as an accepted limitation.

## Accepted Limitation Contract

- Each accepted limitation names the affected example and historical behavior.
- Each accepted limitation states why the behavior is reduced or omitted.
- Each accepted limitation states whether wave-2 acceptance is affected.
- Each accepted limitation states the earliest valid follow-up point.
- Each accepted limitation has a traceable reference in the guide,
  architecture-risk notes, Pflichtenheft note, or Historical Example Parity
  Cleanup record.

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
