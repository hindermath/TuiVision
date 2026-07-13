# Research: Core Runtime Conformance Hardening

## R1 - Source authority and provenance

**Decision**: Turbo Vision 2.0.3 under `tv203s/` remains the primary intent
source. Free Vision commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` is a
secondary implementation opinion from an external untracked worktree.

**Rationale**: The binding audit already defines this precedence. The Feature-025
worktree at `/tmp/tuivision-fv-025-ffc03b34` was fetched detached and the relevant
manifest hashes for `views.inc`, `app.inc`, `menus.inc`, `statuses.inc`,
`dialogs.inc`, and `drivers.inc` match Feature 024.

**Rejected**: vendoring, substantial excerpts, mechanical C++/Pascal
translation, or allowing Free Vision to redefine Borland intent.

## R2 - Concrete event kind (`F001`)

**Decision**: `CreateMouse` uses an exact allow-list of four concrete mouse
kinds rather than the current nonzero-mask test.

**Rationale**: Both historical event unions and Free Vision retain one active
event kind and payload channel. An allow-list rejects category aliases, mixed
channels, unknown bits, and combinations without changing the enum used for
filtering.

**Rejected**: removing `[Flags]`, because masks remain valid for event filters.

## R3 - Focus veto contract (`F002`)

**Decision**: Add a view-owned release hook plus typed `TrySetFocus` result;
retain the existing void `SetFocus` as a compatibility wrapper.

**Rationale**: The original `TGroup::setCurrent` detects when the current view
refuses to clear focus; Free Vision also asks the current view to validate
released focus. A pre-mutation managed decision is clearer than relying on a
derived `SetState` implementation to secretly reassert a flag.

**Rejected**: integrating `TInputLine` validators in 025, which belongs to F011
and Feature 026; exceptions as ordinary veto results; a second focus event.

## R4 - State propagation matrix (`F003`)

**Decision**: `Active` and `Dragging` propagate to all direct children;
`Focused` only to Current; `Exposed` only to visible children; `Disabled`
remains on the group as a dispatch boundary. Insert applies the same matrix.

**Rationale**: This matches the responsibility in `TGroup::setState` and the
corresponding Free Vision implementation while preserving managed ownership.
It prevents several focused or locally disabled children from being fabricated.

**Rejected**: uniform propagation and introducing a new view-tree abstraction.

## R5 - Pending event and idle lifecycle (`F004`)

**Decision**: One pending event slot is drained before physical input. An empty
poll calls overridable `Idle` once and then an overridable CPU-release wait.

**Rationale**: Turbo Vision has one pending slot and performs idle work only
after mouse and keyboard report no event. Free Vision preserves `PutEvent`,
`GetEvent`, and `Idle`. A single slot is bounded and deterministic; a replaceable
wait keeps tests fast without a production busy loop.

**Rejected**: unbounded queue, background worker, timer service, and blocking
`ReadKey` before idle can run.

## R6 - Keyboard translation ownership (`F008`)

**Decision**: Real `TProgram` ingress calls the existing
`TConsoleInputAdapter.CreateKeyDownEvent` after any mouse-sequence boundary.
Global quit chords are recognized from the same raw key after canonical
translation. `TWindow` uses the canonical Ctrl bit.

**Rationale**: Compatibility already owns the tested scan-code and modifier
mapping. The audit explicitly identifies the duplicated `TProgram` mapping and
wrong window Ctrl bit. An existing-project reference is smaller and safer than
copying tables.

**Rejected**: a second mapping in Controls, moving the public translator, or a
platform-specific scan table outside Compatibility.

## R7 - Desktop stack operations (`F005`)

**Decision**: Add bounded Desktop methods for focused insertion, top/next
selection, tile, cascade, and safe close-all. Only visible `Tileable` children
participate in geometry operations.

**Rationale**: Turbo Vision and Free Vision both keep these responsibilities on
the Desktop. Existing `TGroup` ownership remains sufficient; application-local
window registries would duplicate framework state.

**Rejected**: global desktop singleton, application-specific window subclasses,
or exact static-variable translation of the C++ tile implementation.

## R8 - Close and modal lifecycle (`F006`)

**Decision**: Use a small closeable-view request contract and a group-owned
modal executor. One direct modal child per owner is allowed; nested modality is
only possible below the active modal child. Temporary insertion and focus
restoration are protected by `finally`.

**Rationale**: Historical `close`, `execView`, and Free Vision `ExecView`/
`ExecuteDialog` couple validation, result, modal state, insertion and focus
restoration. The managed contract must prove visible removal rather than a
signal alone.

**Rejected**: native nested message loops, silent modified-data discard, and
making Desktop know editor or file-window types.

## R9 - Shared command context (`F007`)

**Decision**: Build an immutable per-refresh snapshot from an opt-in active-view
provider and the legacy program override. Menu/status manual disablement stays
separate from context disablement. Refresh after focus, each handled event, and
idle; recheck before dispatch.

**Rationale**: Turbo Vision and Free Vision use one command set and broadcast
changes. A snapshot avoids hidden mutable global state while keeping all four
surfaces consistent.

**Rejected**: a process-global command registry, an application command catalog,
or directly overwriting manual `Disabled` flags.

## R10 - Generic drag session (`F009`)

**Decision**: Add a source-owned session with a one-cell threshold, one capture,
optional payload, bounds, opt-in target negotiation, explicit result, and common
pointer/keyboard updates. Existing window title drag and Ctrl+F5 use it first.

**Rationale**: Historical `dragView` and Free Vision share tracked mouse and
keyboard move/grow responsibility. A state object is idiomatic C# and allows
deterministic lifecycle cancellation without raw nested pointer loops.

**Rejected**: full desktop drag-and-drop protocol, pointer-only behavior,
background tracking, or Wave-6 file semantics.

## R11 - Audit resolution strategy

**Decision**: Preserve Feature-024 Revision 2 as the historical audit baseline
and append machine-checkable Feature-025 resolution metadata after proof. Update
readable findings/readiness tables and their validators without deleting the
original findings.

**Rationale**: Rewriting the audit as if drift never existed would destroy
traceability. Resolution metadata shows both the accepted observation and the
proven correction.

**Rejected**: removing F001-F009, changing their historical observation, or
marking them closed before the final real-path gates pass.

## R12 - Governance and release triggers

**Decision**: Public additive APIs and their XML comments make DocFX/Axe/Lynx
mandatory. Shared runtime changes make full Release and canonical coverage
mandatory. No package, script, cloud, AI, web/auth, regulated service, or new
release artifact is planned.

**Rationale**: This is proportional to the touched surfaces and preserves every
constitution gate without creating unrelated compliance documents.

**Rejected**: treating runtime hardening as documentation-only, or generating
SBOM/ASVS/cloud artifacts without a trigger.
