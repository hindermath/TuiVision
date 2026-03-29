# Implementation Plan: Controls Revision

**Branch**: `008-controls-revision` | **Date**: 2026-03-29 | **Spec**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/008-controls-revision/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/008-controls-revision/spec.md)
**Input**: Feature specification from `/specs/008-controls-revision/spec.md`

## Summary

Close the current Controls-layer behavior gap between the accepted shell baseline
and the next example wave by expanding `TMenuBar`, `TStatusLine`, `TWindow`,
and `TDialog` in place, adding the minimum missing declaration types
(`TSubMenu`, `TStatusDef`, `WindowFlags`), making help-context-driven status
resolution explicit, and restoring the missing keyboard-driven window and menu
interactions. The feature stays inside `TuiVision.Controls` and
`tests/TuiVision.Controls.Tests`, reuses the managed shell/event pipeline, and
requires proof-surface updates in `docs/porting-status.md`,
`docs/project-statistics.md`, and `Pflichtenheft.md` when the implementation
lands, including moving the prominent `>>> NAECHSTER SCHRITT <<<` marker if the
completed Controls revision changes the highest-priority remaining work item.

## Terminology & Operational Definitions

- **Actionable menu entry**: A menu entry that is neither disabled nor a visual
  separator and may therefore receive focus and dispatch a command.
- **One-level submenu hierarchy**: A top-level menu entry may own one direct
  `TSubMenu`; entries inside that submenu may not open deeper nested submenus in
  this revision.
- **Menu layout slot**: The cached visible placement for one top-level menu
  entry, including its start column, rendered width, and clipped/hidden state
  after the last bounds computation.
- **Status context definition**: A `TStatusDef` rule that maps one inclusive
  help-context range to one ordered status-action set.
- **Neutral status line**: A visible status-line surface that shows no
  context-specific actions because no configured definition matched the active
  help context.
- **Move session**: The transient state entered through `Ctrl+F5` in which a
  movable window previews positional changes until `Enter` commits or `Escape`
  restores the original bounds.
- **Dialog close validation**: The rule evaluation performed before a dialog
  accepts a closing command and returns its modal result.
- **Proof surface**: A repository-visible tracking artifact that must be kept in
  sync with the delivered Controls behavior, especially `docs/porting-status.md`
  and `Pflichtenheft.md`.

## Technical Context

**Language/Version**: C# `latest` on .NET 10 (`net10.0`)  
**Primary Dependencies**: Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation  
**Storage**: In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage  
**Testing**: MSTest-first coverage in `tests/TuiVision.Controls.Tests`,
including unit-style and integration-style shell behavior checks; full
repository validation via `dotnet build --configuration Release`, `dotnet test`,
`dotnet format --verify-no-changes`, and conditional `docfx docfx.json`; the
feature must preserve the `TuiVision.Controls` 70% line-coverage gate and the
constitution-mandated event-loop/focus/menu/dialog integration coverage  
**Target Platform**: Managed cross-platform terminal UI on macOS, Linux, and
Windows/WSL, with the Multi-Mac workflow (`MacBook Air M2`, `Mac mini M4 Pro`)
as the primary development path  
**Project Type**: Managed .NET library increment in `TuiVision.Controls` with companion tests and repository-visible documentation/proof updates  
**Performance Goals**: Menu navigation, status-line refresh, window movement,
and dialog close validation must settle in the same interactive event-loop
cycle that processes the triggering input, without requiring a deferred follow-up
pass; reviewers should be able to observe the updated menu/status/window/dialog
state on the first redraw after the relevant event during normal local terminal
usage  
**Constraints**: Managed-only runtime; no terminal-mouse support; no streaming
or persisted serialization work; no new framework module; exactly one submenu
level; first-match-wins status resolution; empty/neutral fallback on no status
match; `Ctrl+W` and guarded `Escape` for closable windows; `Ctrl+F5` move mode;
German-first/English-second CEFR-B2 documentation; numbered-branch version
governance in `Directory.Build.props` remains mandatory before build/test
commands tied to implementation commits  
**Scale/Scope**: Extend existing controls (`TMenuBar`, `TMenuItem`,
`TStatusLine`, `TStatusItem`, `TWindow`, `TDialog`, `TView`, and possibly
`TProgram` integration points), add three focused declaration/helper files
(`TSubMenu.cs`, `TStatusDef.cs`, `WindowFlags.cs`), add or expand Controls test
coverage, and update the relevant proof/documentation surfaces

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Managed-Only Runtime**: Pass. The feature remains entirely inside the
  managed Controls/Core surface and does not introduce native mouse, terminal,
  or window bindings.
- **Test-First Development — TDD**: Pass with explicit workflow constraint. The
  revision must start with failing MSTest coverage for menu navigation,
  context-driven status resolution, closable/movable window behavior, and dialog
  validation before implementation changes are added.
- **Didactic and Linguistic Clarity**: Pass. New types and changed members in
  `src/TuiVision.Controls` and `tests/TuiVision.Controls.Tests` must keep full
  bilingual XML documentation and explanatory comments where they add learning
  value.
- **Modular Architecture**: Pass. No sixth source module is introduced; the
  new declaration types stay in `TuiVision.Controls`, and the feature continues
  to depend only on `TuiVision.Core`.
- **Cross-Platform Portability**: Pass with explicit validation requirement.
  The feature affects runtime shell behavior, so Linux and Windows/WSL evidence
  are required in addition to the primary Multi-Mac workflow.
- **License & Disclaimer Integrity**: Pass. Historical reference files remain
  untouched under `tv203s/`; new project-owned code stays under the existing MIT
  licensing/disclaimer rules.

**Post-Design Gate Review**: Phase-1 artifacts keep the feature inside the
existing five-module architecture, retain MSTest-first validation, and resolve
the missing Controls behavior without expanding into terminal-mouse, streaming,
or later example-wave scope. No constitution exception is required.

- The feature affects runtime behavior and validation workflow, so Linux and
  Windows/WSL evidence must be planned alongside the primary Multi-Mac path.
- The feature does not itself advance one of the 25 mandatory example ports; it
  is an enabling framework increment for the next mandatory wave.
- Statistical-documentation impact identified; update
  `docs/project-statistics.md` when the implementation phase lands.
- `Pflichtenheft.md` remains a mandatory follow-through surface; if the
  delivered Controls revision changes the effective next step, the prominent
  `>>> NAECHSTER SCHRITT <<<` marker must move in the same implementation
  change.

## Project Structure

### Documentation (this feature)

```text
specs/008-controls-revision/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── controls-revision-api.md
├── checklists/
│   └── requirements.md
└── tasks.md
```

### Source Code (repository root)

```text
src/
└── TuiVision.Controls/
    ├── TApplication.cs
    ├── TDialog.cs
    ├── TGroup.cs
    ├── TMenuBar.cs
    ├── TMenuItem.cs
    ├── TProgram.cs
    ├── TStatusItem.cs
    ├── TStatusLine.cs
    ├── TSubMenu.cs              # planned
    ├── TStatusDef.cs            # planned
    ├── TView.cs
    ├── TWindow.cs
    └── WindowFlags.cs           # planned

tests/
└── TuiVision.Controls.Tests/
    ├── ControlsProofTests.cs
    ├── TDialogTests.cs
    ├── TMenuBarTests.cs
    ├── TProgramTests.cs
    ├── TStatusLineTests.cs
    └── TWindowTests.cs          # planned

docs/
├── porting-status.md
├── project-statistics.md
└── guides/

Pflichtenheft.md
Lastenheft_ControlsRevision.md
```

**Structure Decision**: Keep the feature entirely inside the existing
`TuiVision.Controls` library and `tests/TuiVision.Controls.Tests`. Add only the
minimal missing declaration types (`TSubMenu`, `TStatusDef`, `WindowFlags`) and
extend existing shell classes in place rather than introducing a sixth module
or a standalone menu/window subsystem assembly.

## Research Focus

Phase 0 resolves and locks the following planning decisions:

1. The feature remains a focused Controls-layer correction and does not justify
   a new module.
2. `TSubMenu` must exist as a standalone declaration type compatible with the
   historical menu-builder style, but rendering/execution remains coordinated by
   `TMenuBar`.
3. The menu system supports exactly one submenu level and recomputes top-level
   layout slots whenever bounds change.
4. Menu navigation wraps at both levels and skips disabled/separator entries.
5. `TStatusDef` uses inclusive help-context ranges with first-match-wins
   semantics and an empty/neutral fallback when no definition matches.
6. The status-line design must not break existing focused-view hint producers,
   so a compatibility bridge is needed when no explicit `TStatusDef`
   configuration is supplied.
7. A dedicated `HelpContext` surface is needed on `TView` so status resolution
   no longer depends on implicit hint inference.
8. `WindowFlags` should expose only `Close` and `Move` in this increment, with
   `Ctrl+W`, guarded `Escape`, and `Ctrl+F5` aligned to the clarified UX
   contract.
9. Dialog close validation should extend the existing modal `ushort` result
   flow instead of replacing it.
10. The porting ledger and project tracking documents must be updated in the
    same implementation change as the behavior they describe.

## Phase 0 Research Summary

See [research.md](research.md) for full detail. Key planning decisions:

1. Keep the revision inside the existing Controls/test projects and do not
   introduce a broader `TMenuView`/`TMenuBox` framework split.
2. Make `TSubMenu` a standalone declaration node and preserve historical
   declaration ergonomics, including unchanged `tvguid02`-style menu-building
   syntax as an acceptance target.
3. Keep actual menu execution and popup drawing in `TMenuBar`, but formalize
   one-level submenu state and cached top-level layout slots.
4. Resolve status contexts through explicit `TStatusDef` definitions using an
   inclusive range model, first-match-wins ordering, and neutral empty fallback.
5. Add explicit help-context surfacing on `TView` and keep focused-view
   `GetStatusHints()` only as a compatibility path when no explicit status
   definitions are configured.
6. Represent closable/movable window behavior through a small `WindowFlags`
   surface plus a move-session snapshot, not through a full zoom/grow system.
7. Add a `Valid(ushort command)` dialog hook and gate close acceptance through
   that hook while preserving the existing `ushort` modal-result contract.
8. Treat `docs/porting-status.md`, `Pflichtenheft.md`, and
   `docs/project-statistics.md` as mandatory delivery surfaces for the finished
   increment.

## Phase 1 Design Overview

- `TMenuBar` remains the interactive coordinator for menu activation, top-level
  selection, submenu visibility, command dispatch, and popup rendering.
- `TSubMenu` becomes the standalone declaration type for submenu branches and
  keeps menu hierarchy authoring separate from render-time state.
- `TMenuItem` evolves from a minimal linked node into a declaration-friendly
  menu element that can participate in historical menu-building syntax while
  still representing disabled and separator entries.
- `TStatusDef` defines the context-routing contract for `TStatusLine`,
  associating one inclusive help-context range with one status-action chain.
- `TStatusLine` resolves the active help context, selects the first matching
  `TStatusDef`, shows an empty/neutral line when no definition matches, and may
  fall back to a focused view's direct hint chain only when no explicit
  definitions were supplied at construction time.
- `TView` gains an explicit help-context surface that can be read by the shell
  and specialized by focused views or hosts.
- `TWindow` grows an explicit flags model, close affordance, guarded close-key
  handling, and move-mode state management, but not full zoom/grow behavior.
- `TDialog` keeps the existing modal run loop and command result return type,
  but adds a close-validation hook so dialog closure can be rejected without
  losing dialog state.
- `TProgram` may need narrow integration updates for resize propagation or
  focus-driven shell refresh, but the feature does not redesign the overall
  shell lifecycle.

### Responsibility Boundaries

- `TMenuBar` owns top-level activation, wrap-around navigation, submenu
  selection state, skip-over behavior for non-actionable entries, and visible
  popup placement after resize.
- `TSubMenu` owns declaration-time submenu composition only; it does not run its
  own modal event loop and does not handle mouse behavior in this increment.
- `TStatusLine` owns context-definition selection, neutral fallback rendering,
  and compatibility bridging from older direct-hint callers when no definitions
  are configured.
- `TView` owns exposing help context as a stable shell-readable value.
- `TWindow` owns close affordance visibility, move-session lifecycle, and the
  boundary between child-consumed `Escape` and window-level close handling.
- `TDialog` owns deciding whether a close request is valid before modal result
  acceptance and preserving state when validation rejects the request.

### Design Boundaries for This Increment

- Exactly one submenu level is in scope. Recursive submenu trees remain
  explicitly out of scope.
- Terminal-mouse tracking, palette customization, zoom/grow behavior, and
  streaming/persistence all remain out of scope.
- The feature may touch `TProgram` or existing editor/help hosts only as needed
  to integrate the revised Controls behavior; it does not reopen the broader
  editor/help scope from feature 004.
- Existing focused-view `GetStatusHints()` behavior may remain as a transition
  path only where no explicit `TStatusDef` configuration exists, primarily for
  already-ported callers such as `TEditor` and `TEditWindow`.

## Implementation Strategy

1. Add failing MSTest coverage for menu wrap-around, submenu skip behavior,
   selection highlighting, resize-driven layout recomputation, and historical
   `TSubMenu` declaration compatibility.
2. Add failing MSTest coverage for `TStatusDef`-based routing, first-match
   ordering, neutral fallback, and any required compatibility bridge for
   existing focused-view hint callers.
3. Add failing MSTest coverage for window close affordance rendering, guarded
   `Ctrl+W`/`Escape` close behavior, move-mode entry via `Ctrl+F5`, move commit,
   and move cancel/restore semantics.
4. Add failing MSTest coverage for dialog close validation, including rejected
   close requests that keep the dialog open and accepted requests that return
   the expected modal result.
5. Introduce minimal declaration/helper types (`TSubMenu`, `TStatusDef`,
   `WindowFlags`) and expand existing models (`TMenuItem`, `TView`, `TWindow`,
   `TDialog`) only enough to make the tests compile.
6. Implement `TMenuBar`, `TStatusLine`, `TWindow`, and `TDialog` behaviors in
   the smallest vertical slices that satisfy the red tests.
7. Update proof/documentation surfaces and bilingual XML docs once the behavior
   is stable and passing.
8. Run build, test, coverage, formatting, and conditional docfx validation
   before the feature is considered ready for tasks/implementation review.

## Scenario & Edge-Case Coverage

### Scenario Matrix

| Scenario class | Covered in spec | Planned artifact coverage |
|---|---|---|
| Top-level menu activation and wrap navigation | User Story 1, FR-002, FR-002a | `research.md`, `data-model.md`, `contracts/controls-revision-api.md`, expanded `TMenuBarTests.cs` |
| Submenu focus, selection highlight, and command confirmation | User Story 1, FR-003 to FR-005 | `data-model.md`, contract menu guarantees, `TMenuBarTests.cs` |
| Submenu skip-over for separators/disabled entries | Edge Cases, FR-003b | `research.md`, `data-model.md`, `TMenuBarTests.cs` |
| Top-level layout recomputation after resize | Edge Cases, FR-012 | `research.md`, menu layout entity in `data-model.md`, `TProgramTests.cs` plus `TMenuBarTests.cs` |
| Context-bound status resolution | User Story 2, FR-007 to FR-009 | `research.md`, `data-model.md`, contract status guarantees, `TStatusLineTests.cs` |
| Overlapping status-definition resolution | Edge Cases, FR-007a | `research.md`, `data-model.md`, `TStatusLineTests.cs` |
| No-match status fallback | Edge Cases, FR-008a | `data-model.md`, contract fallback guarantee, `TStatusLineTests.cs` |
| Closable window behavior | User Story 3, FR-010 | `research.md`, `data-model.md`, contract window guarantees, planned `TWindowTests.cs` |
| Movable window behavior | User Story 3, FR-011, FR-011a | `research.md`, window state transitions, planned `TWindowTests.cs` |
| Dialog close validation | User Story 3, FR-013 to FR-015 | `research.md`, dialog close request entity, contract dialog guarantee, expanded `TDialogTests.cs` |

### Reviewer Readiness Criteria

- Reviewers must be able to point to a written artifact for each of these
  behaviors before `/speckit.tasks`:
  - one-level submenu declaration and navigation model
  - resize-safe top-level menu layout recomputation
  - first-match-wins status-definition routing
  - neutral status-line fallback on no match
  - guarded `Escape` semantics for closable windows
  - move-session commit versus restore behavior
  - dialog close validation versus accepted modal result
- If any of those behaviors are only implied and not explicitly described in at
  least one design artifact plus one validation-oriented artifact, the plan is
  not review-ready.

## Testing Strategy

- **Unit-style Controls tests**: `TMenuBarTests.cs` covers activation, wrap
  behavior, skip-over behavior, mnemonic/Enter confirmation, highlight state,
  submenu declaration compatibility, and resize-driven layout recomputation.
- **Status-line tests**: `TStatusLineTests.cs` covers inclusive-range matching,
  first-match ordering, no-match neutral fallback, and compatibility behavior
  when direct focused-view hints are still used without configured definitions.
- **Window tests**: planned `TWindowTests.cs` covers close affordance rendering,
  guarded `Ctrl+W` and `Escape`, move-mode entry, move commit, and move cancel
  restore. `ControlsProofTests.cs` continues to serve as proof coverage for the
  `twindow.cc` lineage row.
- **Dialog tests**: `TDialogTests.cs` expands to cover `Valid(ushort command)`
  acceptance/rejection paths without regressing existing default-button and
  Escape behavior.
- **Integration-style shell coverage**: `TProgramTests.cs` remains part of the
  integration slice for event-loop-aware behavior, focus transitions, menu
  execution, and dialog interaction, as required by the constitution.
- **Validation commands**:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Controls.Tests/
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"
dotnet test
dotnet format --verify-no-changes
```

- **Conditional documentation gate**:

```bash
docfx docfx.json
```

- **Compatibility evidence**: Because runtime shell behavior changes, the
  implementation phase must record Linux and Windows/WSL compatibility evidence
  alongside the primary Multi-Mac validation path.

## Success-Criteria Traceability

| Success criterion | Planning hook |
|---|---|
| `SC-001` menu navigation reaches intended command without mnemonic-only access | Menu interaction model, `TMenuBarTests.cs`, contract menu guarantees |
| `SC-002` status line updates correctly on context changes | `TStatusDef` data model, status contract, `TStatusLineTests.cs` |
| `SC-003` closable/movable windows behave predictably in repeated runs | window state model, `TWindowTests.cs`, contract window guarantees |
| `SC-004` dialog validation cleanly rejects or accepts close requests | dialog close request model, `TDialogTests.cs`, contract dialog guarantee |
| `SC-005` feature stays within stated bounds | constitution/scope sections, risk boundaries, proof-surface update plan |

## Non-Functional Operationalization

- **Portability**: All new runtime logic remains in `TuiVision.Controls`; no
  OS-specific logic is introduced there.
- **Coverage discipline**: New or expanded Controls behavior must keep the
  assembly-level 70% line-coverage gate intact for `TuiVision.Controls`.
- **Documentation completeness**: Changed production surfaces require bilingual
  XML docs in the same implementation change; if public API/XML docs change and
  `docfx.json` exists, docfx becomes part of the gate.
- **TDD visibility**: Tasks must be ordered so red tests for menu, status,
  window, and dialog behavior appear before the corresponding implementation
  slices.
- **Scope discipline**: One submenu level, no mouse, no streaming, no zoom/grow
  work, and no unrelated example delivery remain hard planning boundaries.

## Dependencies & Assumptions

- The existing `TView`/`TGroup` event and focus model is extensible enough to
  host the revised menu, status-line, window, and dialog behavior without a
  shell rewrite.
- Existing `TProgram` resize and focus propagation hooks are sufficient or can
  be extended narrowly without broad lifecycle changes.
- A small declaration-type expansion (`TSubMenu`, `TStatusDef`, `WindowFlags`)
  is enough to express the missing behavior without introducing a general menu
  framework or window manager.
- Existing focused-view hint producers `TEditor` and `TEditWindow` are the
  primary already-ported callers expected to rely on the temporary
  `GetStatusHints()` compatibility bridge until explicit `HelpContext` /
  `TStatusDef` adoption reaches them.
- `docs/porting-status.md` rows for `tmenubar.cc`, `tmenubox.cc`,
  `tmenuvie.cc`, `tsubmenu.cc`, `tstatusd.cc`, `tstatusl.cc`, `tdialog.cc`,
  and `twindow.cc` must be updated once the implementation reflects the revised
  behavior.
- `Pflichtenheft.md` must update the `>>> NAECHSTER SCHRITT <<<` marker in the
  same implementation change if this feature alters the highest-priority
  remaining work item, and `docs/project-statistics.md` must receive the
  corresponding statistics-ledger entry in that implementation phase.

## Traceability Matrix

| Spec reference | Planned coverage |
|---|---|
| `FR-002` to `FR-006` | menu design overview, `TSubMenu` contract, `TMenuBarTests.cs`, menu entities in `data-model.md` |
| `FR-007` to `FR-009` | status-routing decisions, `TStatusDef` entity, status contract, `TStatusLineTests.cs` |
| `FR-010` to `FR-012` | window flags/move-session design, `TWindowTests.cs`, resize/layout traceability |
| `FR-013` to `FR-015` | dialog validation decision, dialog close request model, `TDialogTests.cs` |
| `FR-016` to `FR-019` | explicit design boundaries, constitution check, risk/scope sections |
| `FR-020` | proof-surface update boundary and dependency notes |
| `SC-001` to `SC-005` | testing strategy, success traceability, proof/documentation update expectations |

## Risks & Mitigations

- **Risk**: Menu declaration compatibility forces a larger rewrite of
  `TMenuItem` than expected.
  **Mitigation**: Keep declaration changes narrow, document the one-level
  hierarchy contract, and confine runtime execution state to `TMenuBar`.

- **Risk**: The new status-definition model breaks existing direct-hint callers.
  **Mitigation**: Preserve a clear compatibility bridge when no explicit
  `TStatusDef` configuration exists, and cover it with tests.

- **Risk**: Window-level `Escape` handling conflicts with child control event
  consumption.
  **Mitigation**: Define guarded `Escape` semantics explicitly and test child
  consumption before window-level close.

- **Risk**: Dialog validation widens into a broader modal-loop redesign.
  **Mitigation**: Keep `Run()` and modal result handling intact; add only the
  validation gate needed by the spec.

- **Risk**: Resize recomputation becomes coupled to fragile hard-coded columns.
  **Mitigation**: Model top-level layout as recomputable slots derived from the
  current bounds rather than from one-time coordinates.

- **Risk**: Proof surfaces drift from actual implementation state again.
  **Mitigation**: Treat `docs/porting-status.md`, `Pflichtenheft.md`, and
  `docs/project-statistics.md` as required deliverables in the same change.

## Complexity Tracking

No constitution violations or exceptional complexity justifications are
currently required.
