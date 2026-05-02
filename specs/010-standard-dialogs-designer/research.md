# Research: Standard Dialogs and Designer Readiness

## Decision 1: Harden existing file-dialog types in place

**Decision**: Use the existing `TFileDialog`, `TFileList`, `TDirListBox`,
`TFileInfo`, `TFileInputLine`, and `THistory` surfaces as the file-dialog
foundation.

**Rationale**: Phase 6 already introduced these reusable types, and `009`
strengthened their input/history context. Creating a second file-picker stack
would make wave-2 examples choose between competing framework surfaces.

**Alternatives considered**:
- Add example-local file picker helpers: rejected because `sdlg`, `sdlg2`, and
  `demo` would become infrastructure owners.
- Create a new dialog module: rejected because the repository keeps Controls
  and Serialization as the relevant existing module boundaries.

## Decision 2: Dialogs return file decisions but do not perform file content I/O

**Decision**: File-oriented standard dialogs cover open, select, and save-target
decisions while leaving file loading, writing, deletion, and overwrite operations
to their caller.

**Rationale**: This preserves the Phase 6 boundary where editor/file components
own content persistence and explicit overwrite decisions. Standard dialogs
remain reusable across examples and do not silently mutate files.

**Alternatives considered**:
- Only existing-file selection: rejected because `sdlg` and `demo` need
  save-target-style validation.
- Full file I/O inside dialogs: rejected because it would duplicate editor/file
  responsibility and increase overwrite-risk scope.

## Decision 3: Keep standard-dialog history session-scoped

**Decision**: Standard-dialog history follows the `009` session-only rule and is
partitioned by history identifier.

**Rationale**: Cross-session persistence would add hidden storage behavior and
contradict the recent widget/history clarification. Session-only recall is
enough for wave-2 dialog ergonomics and keeps privacy/security scope small.

**Alternatives considered**:
- Persist history across restarts: rejected as unnecessary scope and a privacy
  concern.
- Leave history scope to planning tasks: rejected because tests need a stable
  acceptance rule.

## Decision 4: Treat symbolic charset as dialog data only

**Decision**: `010` supports symbolic charset/display choices as returned dialog
values, but it does not change terminal rendering, fonts, buffers, or emulation.

**Rationale**: `sdlg` names charset selection in wave 2, while full charset and
terminal behavior belongs to later wave-4 hardening. The symbolic choice keeps
wave-2 API shape ready without pulling terminal work forward.

**Alternatives considered**:
- Defer all charset choice to wave 4: rejected because `sdlg` would lack a
  standard-dialog selection surface.
- Implement real terminal charset effects now: rejected as a scope violation.

## Decision 5: Reuse color dialog components as one composed flow

**Decision**: `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`,
and `TColorDisplay` are treated as one standard selection flow.

**Rationale**: The Lastenheft explicitly warns against stopping at a minimal
dialog or letting `sdlg`/`sdlg2` define local color widgets. A composed flow
gives tests one reviewable state contract.

**Alternatives considered**:
- Test selectors independently only: rejected because cancellation and preview
  synchronization are flow-level behavior.
- Implement new example-specific color widgets: rejected as duplicate behavior.

## Decision 6: Use a small validated dialog-description model for `dlgdsn`

**Decision**: Plan a compact dialog-description model with unique control IDs,
unique command bindings, labels, navigation order, initial values, and
validation constraints.

**Rationale**: `dlgdsn` needs a clear boundary between design-time description,
runtime dialog, and optional persisted representation. Unique IDs and command
bindings make roundtrip and validation failures testable.

**Alternatives considered**:
- Build runtime dialogs directly from ad-hoc example logic: rejected by the
  Lastenheft.
- Design a broad visual designer subsystem now: rejected because wave-2 only
  needs a bounded intermediate model.

## Decision 7: Persist only validated dialog descriptions

**Decision**: Provide a minimal persisted-description roundtrip using existing
Serialization/resource primitives, and reject malformed, truncated,
unsupported-version, and semantically invalid input before runtime dialog
creation.

**Rationale**: The spec requires persisted or dynamic dialog definitions to have
a testable intermediate representation, but not a broad new persistence
framework. Reusing existing serialization keeps the module boundary explicit.

**Alternatives considered**:
- No persistence proof: rejected because `dlgdsn` would still be able to drift
  into untested ad-hoc persistence.
- New JSON or external format dependency: rejected because no new dependency or
  second persistence stack is needed.

## Decision 8: Make keyboard-only operation acceptance-critical

**Decision**: Every required dialog and designer flow must be operable and
testable through keyboard interaction; mouse support is optional.

**Rationale**: TuiVision is a terminal UI framework with accessibility and
text-first governance. Runtime mouse support is explicitly a later feature, so
`010` must not depend on it.

**Alternatives considered**:
- Require mouse and keyboard equally: rejected because runtime mouse support is
  out of scope.
- Leave input method implicit: rejected because tests could miss keyboard-only
  accessibility.

## Decision 9: Keep acceptance framework-first

**Decision**: Primary acceptance belongs in Controls and Serialization tests.
`demo`, `sdlg`, `sdlg2`, and `dlgdsn` are downstream consumers and traceability
targets, not full deliverables of this feature.

**Rationale**: This matches the Lastenheft acceptance wording and the successful
pattern from `009`: framework readiness first, example ports later.

**Alternatives considered**:
- Add minimal example smoke slices now: rejected because it would mix framework
  hardening with example delivery.
- Fully port the four examples now: rejected as scope creep into wave-2 example
  implementation.

## Decision 10: Security and A11Y evidence remains proportional

**Decision**: Treat local file paths and persisted descriptions as the relevant
trust boundaries; require validation and text-first/keyboard proof, but do not
create new web/API/security-governance documents unless scope widens.

**Rationale**: The feature adds no network service, authentication, database, or
external dependency. Security risk is concentrated in file/path validation and
deserialization-style malformed input handling.

**Alternatives considered**:
- Full ASVS/Zero Trust evidence: rejected as not applicable to a local library
  increment.
- No security/A11Y treatment: rejected because Level-2 constitution rules still
  require explicit applicability and keyboard/text-first review.
