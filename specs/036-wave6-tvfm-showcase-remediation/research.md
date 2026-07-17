# Research: Wave-6 TVFM Showcase Remediation

## R-001 Reuse the Stage-1 functional authority

**Decision**: Keep `ControlledFileWorkspace` and all Feature-035 functional
models as the only authority for navigation, preview, search, operation
preparation, revalidation, and execution.

**Rationale**: Feature 036 is a showcase layer. Duplicating or wrapping the
domain contract would create drift and could weaken the controlled-root
boundary.

**Alternatives considered**:

- Re-port TVFM behavior into the UI: rejected as explicit scope violation.
- Add a second filesystem service for dialogs: rejected because it creates
  competing authority.
- Move the workspace into a framework assembly: rejected absent a reusable
  framework finding.

## R-002 Persistent main composition

**Decision**: Replace the transient all-text window with one persistent
`TWindow` containing a `TListBox`, text-first detail/preview regions, and
existing menu/status/help infrastructure.

**Rationale**: A focusable list and stable visible regions provide genuine
view, focus, keyboard, and cell proof while reusing existing controls.

**Alternatives considered**:

- Keep recreating `TStaticText` windows: rejected because it does not expose
  stable list focus or a file-manager interaction model.
- Build a new tree widget: rejected because a reusable tree control is not
  required for this one controlled teaching fixture.
- Copy the Pascal window hierarchy exactly: rejected in favor of modern,
  compact, idiomatic C# composition.

## R-003 Closed menu and command topology

**Decision**: Use closed File, Navigate, View, Search, Options, and Help groups
with typed example-local commands and explicit enablement/status rules.

**Rationale**: Every Feature-035 capability becomes discoverable without
copying obsolete DOS menu structure or exposing hidden test commands.

**Alternatives considered**:

- One flat menu: rejected as hard to scan and teach.
- Historical menu replication: rejected because unavailable host and drive
  behavior would be misleading.
- Command palette or dependency: rejected as unnecessary new interaction and
  package scope.

## R-004 Existing controls for operation dialogs

**Decision**: Compose bounded dialogs with existing `TDialog`, `TInputLine`,
`TStaticText`, and `TButton`; retain typed feature-local dialog state for
proof and route accepted requests into the existing one-shot intent flow.

**Rationale**: Existing controls already provide focus, Tab, Enter, Escape,
validation, and command dispatch. Feature-local state makes Preview,
decision, revalidation, and result testable without modifying framework APIs.

**Alternatives considered**:

- Direct command execution: rejected because Preview and confirmation vanish.
- `TFileDialog`: rejected because it is a general host-path dialog and does
  not own this controlled-root policy.
- New public dialog framework: rejected absent a reusable gap.

## R-005 Dialog target semantics

**Decision**: Copy accepts a bounded root-relative target, rename accepts a
bounded leaf name in the current directory, and delete/read-only dialogs show
source plus decision without target input.

**Rationale**: Operation-specific input reduces accidental authority and maps
directly to Feature-035 typed intents. All values are revalidated by the
workspace after visible Preview.

**Alternatives considered**:

- Absolute paths: rejected as authority expansion.
- One generic free-form operation dialog: rejected because irrelevant fields
  obscure safety and validation.
- Silent default targets: rejected because the user must see the intended
  mutation.

## R-006 Bounded drag intent

**Decision**: Track only selected source, visible target region, and drag
phase. Mouse release may prepare the keyboard-equivalent intent and open the
same confirmation path; it never executes.

**Rationale**: This delivers useful mouse parity without a second mutation
path or general desktop drag/drop framework.

**Alternatives considered**:

- Execute on drop: rejected as unsafe and explicitly prohibited.
- General multi-view drag/drop API: rejected as broad framework scope.
- Omit mouse entirely: rejected because the accepted ShowcaseDelta names a
  bounded improvement and existing mouse events support it.

## R-007 Constrained layout

**Decision**: Normal layout shows list and details together. `48x16` retains
the focusable list, selected path, concise current-state summary, StatusLine,
Description, and quit path; secondary content is summarized.

**Rationale**: Stable minimum behavior is more honest than scaling text into
overlap or hiding controls. Text-first summaries remain usable in assistive
and narrow terminal contexts.

**Alternatives considered**:

- Horizontal scrolling for the whole app: rejected because essential context
  becomes hidden.
- Shrink fonts: not meaningful in a terminal-cell UI.
- Reject narrow terminals: rejected because the intake requires a constrained
  proof.

## R-008 Evidence validator shape

**Decision**: Add one deterministic test-only matrix parser for exact
`W6S-001` through `W6S-010` rows and one `Tp7FileManager` row, including
closed decision vocabularies and required evidence cells.

**Rationale**: Exact cardinality and fail-closed malformed fixtures prevent a
visually plausible but incomplete completion claim.

**Alternatives considered**:

- Manual PR review only: rejected as non-deterministic.
- Introduce a new schema/package: rejected as unnecessary dependency and
  project expansion.
- Reuse the Feature-035 Stage-2 parser unchanged: rejected because the new
  row shape and final decision vocabulary differ.

## R-009 A11Y and teaching contract

**Decision**: Couple focused-control identity, status, Description, keyboard
inventory, High Contrast, cell evidence, and bilingual guide updates to each
primary area.

**Rationale**: A visible showcase is complete only if its state and operation
are understandable without color or pointer-only input.

**Alternatives considered**:

- Rely on screenshots: rejected as non-text-first and weak for terminal state.
- Document shortcuts without behavioral proof: rejected because inventory and
  executable behavior could drift.

## R-010 Validation and delivery depth

**Decision**: Run targeted showcase plus preserved filesystem tests, normal
PTY and smoke, full Release, coverage, DocFX/Axe, three platforms,
supply-chain, agent parity, review, and exact-head gates.

**Rationale**: Shared executable UI, real dialogs, terminal input/layout,
filesystem-facing choices, and learner documentation have repository-wide
and cross-platform impact.

**Alternatives considered**:

- Targeted tests only: rejected because input, rendering, docs, and platform
  integration are material.
- New preset release automatically: rejected unless a reproducible
  provider-neutral defect is found.
