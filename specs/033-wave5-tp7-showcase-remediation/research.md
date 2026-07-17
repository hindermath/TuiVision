# Research: Wave-5 TP7 Showcase Remediation

## R-001 Presentation-only sharing

**Decision**: Extend `Wave5Application` only with shared Description, status,
focus, view-region, and bounded-layout presentation helpers.

**Rationale**: All ten apps need the same showcase shell, but domain and
framework behavior are already proven. This removes duplication without
creating an examples-only framework substitute.

**Alternatives considered**:

- Duplicate menu/status/Description code in ten apps: rejected as avoidable
  drift.
- Move showcase behavior into a framework project: rejected because it is
  Wave-5 presentation, not reusable framework semantics.

## R-002 Calculator reference composition

**Decision**: Use a real `TDialog` containing a visible display, focusable
calculator buttons, and keyboard commands; keep `Tp7CalculatorState`
unchanged.

**Rationale**: The calculator is compact enough for a complete `40x12`
red/green slice and proves the shared shell before broader rollout.

**Alternatives considered**:

- Keep a static text window: rejected because it cannot prove focus order or
  a real button grid.
- Add calculator logic to controls: rejected because Feature 032 already
  supplies the accepted domain state.

## R-003 Grid widgets

**Decision**: Use bounded focusable Wave-5 views for ASCII, calendar, and
puzzle grids. They render from existing typed state and translate keyboard
input into existing application commands.

**Rationale**: A small presentation view expresses cell selection and focus
without changing the domain model or adding general-purpose framework APIs.

**Alternatives considered**:

- Build hundreds of independent buttons: rejected because constrained
  layouts would become noisy and fragile.
- Add a reusable framework grid control: rejected as broad scope without a
  demonstrated cross-feature need.

## R-004 Desktop window operations

**Decision**: Compose the demo from real `TWindow` instances and existing
desktop/group operations. Commands expose open, Tile, Cascade, Next, and
Close, with visible status after each action.

**Rationale**: The historical learning purpose is multi-window interaction,
not a text report. Existing TuiVision ownership and focus contracts are the
correct proof surface.

**Alternatives considered**:

- Simulate window state in strings: rejected because it cannot prove view
  hierarchy or focus.
- Recreate the full TP7 demo suite: rejected as functional re-porting.

## R-005 Editor and Help reuse

**Decision**: Keep `TEditWindow`, `TFileEditor`, `THelpWindow`,
`THelpViewer`, `THelpSourceCompiler`, and their accepted state transitions.
Add only visible menus, diagnostics, navigation commands, and Description.

**Rationale**: Features 018 and 032 already hardened these contracts. Feature
033 must demonstrate them, not fork them.

**Alternatives considered**:

- Add custom example editors/viewers: rejected as a local framework copy.
- Decode historical proprietary binary Help: rejected by the existing
  intentional modernization boundary.

## R-006 Resource UI boundary

**Decision**: Build visible controls only after exact allowlisted Resource
records are fully loaded. Generator UI uses the existing controlled-root
request and reports target, progress, success, or rejection text.

**Rationale**: Presentation must not weaken atomic publication or path
ownership.

**Alternatives considered**:

- Bind partially decoded records as they arrive: rejected because malformed
  input could expose an inconsistent model.
- Add arbitrary type or path selection: rejected by the closed Feature-032
  contract.

## R-007 Mouse settings composition

**Decision**: Use real focusable settings controls for local double-click
step, button order, and activation. Capability remains honest and every action
has a keyboard equivalent.

**Rationale**: The historical purpose is understandable mouse configuration,
while modern portability forbids host mutation.

**Alternatives considered**:

- Native host settings: rejected as non-portable and outside scope.
- Pointer-only interaction: rejected by keyboard and A11Y requirements.

## R-008 Description contract

**Decision**: Every app supplies German-first/English-second CEFR-B2
Description content covering purpose, operation, modernization,
security/capability boundary, and proof boundary.

**Rationale**: One shared command ensures reachability; app-owned content
keeps explanations accurate.

**Alternatives considered**:

- Guide-only explanation: rejected because the required in-app path would be
  missing.
- Generic one-size text: rejected because boundaries differ per app.

## R-009 Constrained layouts

**Decision**: Define one stable small viewport per app, use size-derived bounds,
and verify required labels, focus text, and status do not overlap.

**Rationale**: Cell-based layouts need deterministic proof at their minimum
supported teaching size.

**Alternatives considered**:

- Screenshot-only review: rejected as unstable and not text-first.
- Viewport-independent fixed coordinates: rejected because they clip in
  constrained terminals.

## R-010 Validation depth

**Decision**: Run targeted showcase smokes, ten normal entry points, full
Release tests, canonical coverage, DocFX/Axe, all three remote platforms,
agent parity, secrets, supply chain, review convergence, and exact-head
evidence.

**Rationale**: Shared executable presentation code, public XML comments, ten
guides, and solution-wide examples have repository-wide blast radius.

**Alternatives considered**:

- Targeted tests only: rejected because solution, docs, and platform
  integration remain material.
- New validation scripts: rejected unless the existing MSTest/workflow paths
  prove insufficient.
