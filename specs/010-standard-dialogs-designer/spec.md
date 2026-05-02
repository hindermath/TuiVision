# Feature Specification: Standard Dialogs and Designer Readiness

**Feature Branch**: `010-standard-dialogs-designer`  
**Created**: 2026-04-30  
**Status**: Draft  
**Input**: User description: "Erstelle eine Spezifikation aus dem Inhalt der Datei Lastenheft_02_StandardDialogsAndDesigner.md"

## Clarifications

### Session 2026-04-30

- Q: Soll `010` die in `sdlg` erwartete Zeichensatz-Auswahl behandeln, obwohl Terminal-/Charset-Haertung spaeter geplant ist? → A: `010` umfasst eine dialognahe symbolische Charset-/Display-Auswahl fuer Wave 2, aber keine Terminal-Rendering-, Font- oder Emulationslogik.

### Session 2026-05-01

- Q: Wie weit muss die optionale Persistenz fuer `dlgdsn` in `010` gehen? → A: `010` fordert einen minimalen Persistenz-Roundtrip fuer Dialogbeschreibungen inklusive Validierung fehlerhafter gespeicherter Eingaben.
- Q: Welche Datei-Entscheidungen muessen Standarddialoge abdecken? → A: Standarddialoge decken Open-/Select-/Save-Target-Entscheidungen ab, fuehren aber keine Datei-I/O-Operationen selbst aus.
- Q: Wo liegt die primaere Akzeptanz fuer `010`? → A: Akzeptanz liegt primaer in Framework-/Controls-/Serialization-Tests; Wave-2-Beispiele werden nur als spaetere Konsumenten klassifiziert.

### Session 2026-05-02

- Q: Welche Eingabemethode ist fuer `010` akzeptanzkritisch? → A: `010` verlangt vollstaendige Tastaturbedienung fuer Standarddialoge und Designer-Flows; Maus ist optional und nicht akzeptanzkritisch.
- Q: Welchen Scope hat History in Standarddialogen? → A: History in Standarddialogen ist session-scoped; keine Persistenz ueber Programmstarts hinweg.
- Q: Welche Eindeutigkeitsregeln gelten fuer Dialogbeschreibungen? → A: Dialogbeschreibungen muessen innerhalb eines Dialogs eindeutige Control-IDs und eindeutige Command-Bindings besitzen.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Complete Standard Dialog Flows (Priority: P1)

As a wave-2 example porter, I need reusable standard dialog flows for file,
directory, and similar selections so that `demo`, `sdlg`, and `sdlg2` can rely
on shared framework behavior instead of rebuilding dialog infrastructure in
example code.

**Why this priority**: These flows are the highest-risk prerequisite for wave
2 because they combine navigation, validation, manual entry, metadata display,
history recall, and return values.

**Independent Test**: The story is independently testable when a reviewer can
drive file and directory dialog scenarios and observe synchronized path,
filter, selection, metadata, history, validation, and result state without
using a wave-2 example as the primary proof. The complete flow must be usable
through keyboard interaction; mouse support is optional and not required for
acceptance.

**Traceability**: FR-001, FR-002, FR-002a, FR-003, FR-003a, FR-010,
FR-010a, FR-013; SC-001, SC-007, SC-008.

**Acceptance Scenarios**:

1. **Given** a user opens a standard file selection flow, **When** the user
   changes directory, applies a filter, selects a file, and confirms the dialog,
   **Then** the visible list, selected path, file metadata, validation state,
   and returned result all describe the same file.
2. **Given** a user manually enters a path that differs from the current list
   selection, **When** the dialog validates and accepts that input, **Then** the
   current selection, history entry, metadata display, and returned value are
   synchronized with the manual path.
3. **Given** a user enters an invalid, inaccessible, or non-matching path,
   **When** the dialog validates the input, **Then** the user receives a clear
   non-destructive validation outcome and the previous valid selection remains
   recoverable.
4. **Given** a user chooses a target path for saving, **When** the dialog
   validates and confirms that target, **Then** it returns an explicit
   save-target decision without loading, writing, deleting, or overwriting file
   content itself.

---

### User Story 2 - Reusable Color, Charset, and Display Selection (Priority: P2)

As a wave-2 example porter, I need color, symbolic charset, and display
selection to behave as one coherent reusable flow so that `sdlg` and `sdlg2` do
not introduce local color or display-choice widgets or divergent state rules.

**Why this priority**: Color selection is less broad than file selection, but
it is a visible standard-dialog capability and can easily drift into
example-local implementations if not specified first.

**Independent Test**: The story is independently testable when a reviewer can
change color, symbolic charset, and display choices through the reusable
standard flow and verify that selection, preview, validation, cancellation, and
confirmation stay consistent without requiring terminal rendering, font, or
emulation effects, mouse interaction, or a ported wave-2 example.

**Traceability**: FR-001, FR-004, FR-005, FR-010, FR-010a, FR-012, FR-013;
SC-002, SC-005, SC-006, SC-007, SC-008.

**Acceptance Scenarios**:

1. **Given** a user opens a color selection flow, **When** the user changes a
   group, foreground, background, or monochrome/display choice, **Then** the
   selected value and preview state update together.
2. **Given** a user opens a symbolic charset or display-choice flow, **When**
   the user selects an available option, **Then** the returned choice is
   synchronized with the visible selection without changing terminal rendering,
   fonts, or emulation behavior.
3. **Given** a user cancels after changing a color, charset, or display choice, **When**
   the dialog closes, **Then** the previous committed value is preserved.
4. **Given** `sdlg` or `sdlg2` needs color, charset, or display selection,
   **When** the example is reviewed, **Then** its main flow can consume the
   shared standard flow rather than defining a competing local selector.

---

### User Story 3 - Dialog Designer Description Boundary (Priority: P3)

As a maintainer of the `dlgdsn` example, I need a clear intermediate dialog
description model so that dynamic dialogs are described, validated, and
optionally persisted without ad-hoc runtime construction logic.

**Why this priority**: The designer is a specialized wave-2 proof. It can come
after the core standard flows, but it must be defined before `dlgdsn` is ported
so the example does not become the only place where designer semantics exist.

**Independent Test**: The story is independently testable when a reviewer can
describe a dialog, validate the description, create an equivalent runtime
dialog from it, complete a minimal persisted-description roundtrip, and
identify exactly which parts are persistable and which are runtime-only.
This evidence is framework-first; `dlgdsn` remains a later consumer rather than
the primary acceptance vehicle for this feature. Designer flows must be
operable through keyboard interaction.

**Traceability**: FR-006, FR-006a, FR-007, FR-008, FR-008a, FR-009, FR-010,
FR-010a, FR-011, FR-013; SC-003, SC-003a, SC-004, SC-005, SC-007, SC-008.

**Acceptance Scenarios**:

1. **Given** a valid dialog description, **When** it is validated and consumed,
   **Then** the produced runtime dialog has the expected controls, labels,
   navigation order, commands, and initial values.
2. **Given** an invalid dialog description, **When** it is validated, **Then**
   the validation result names the problem without producing a partial or
   misleading runtime dialog.
3. **Given** a dialog description contains duplicate control identifiers or
   duplicate command bindings inside the same dialog, **When** it is validated,
   **Then** validation rejects the description before runtime creation or
   persistence.
4. **Given** a dialog description is stored or loaded through project-owned
   resources, **When** reviewers inspect the flow, **Then** the boundary between
   dialog description, runtime object, and persisted representation is explicit.
5. **Given** a stored dialog description is malformed, truncated, unsupported,
   or semantically invalid, **When** it is loaded for the designer flow,
   **Then** validation rejects it clearly before any runtime dialog is produced.

---

### Edge Cases

- A directory changes while a dialog is open, so stale file entries and stale
  metadata must not silently become the accepted result.
- A save-target path points to an existing file or non-writable location, so the
  dialog must return an explicit decision or validation outcome without
  overwriting or creating file content itself.
- A wildcard or filter produces an empty list, so manual entry, validation, and
  cancellation must still be usable.
- A selected file has missing or unreadable metadata, so the dialog must show a
  clear fallback instead of desynchronizing the selected path.
- A color group, symbolic charset choice, or display mode has no supported
  option for the active environment, so the flow must return a text-first
  no-supported-option or preserved-committed-value fallback and no local example
  widget should reinterpret the state.
- A dialog description references an unknown control role, duplicate command,
  duplicate control identifier, invalid tab order, missing label, or
  unsupported persisted value.
- A resource-backed dialog description is truncated, malformed, or uses a
  version that is not supported by this feature.
- A persisted dialog description roundtrip omits runtime-only state, so the
  loaded description must still validate without implying that transient runtime
  objects were persisted.
- A host does not provide usable mouse input, so every required standard-dialog
  and designer action must remain reachable and verifiable through keyboard
  interaction.
- A new application session starts after prior file-dialog use, so previous
  standard-dialog history entries must not be required to reappear.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST define reusable standard dialog flows for file,
  directory, color, symbolic charset, display, and comparable wave-2 selections
  as complete user flows, including visible choices, validation, cancellation,
  confirmation, and return values. "Comparable wave-2 selections" is limited
  to selection points in `demo`, `sdlg`, `sdlg2`, or `dlgdsn` that use the same
  file, directory, color, symbolic charset, display, or dialog-description
  responsibility classes already named here; it does not add new dialog
  categories to this feature.
- **FR-002**: File and directory selection flows MUST keep current directory,
  active filter, visible entries, selected item, manual path entry, file
  metadata, history recall, validation state, and returned result synchronized.
- **FR-002a**: Standard-dialog history MUST be scoped to the active application
  session and MUST NOT require persistence across program restarts.
- **FR-003**: File and directory selection flows MUST handle empty lists,
  invalid manual paths, unreadable metadata, and changing directories without
  accepting stale or contradictory state.
- **FR-003a**: File and directory standard dialogs MUST cover open, select, and
  save-target decisions, but MUST NOT perform file loading, file writing,
  deletion, or overwrite operations themselves.
- **FR-004**: Color, symbolic charset, and display selection flows MUST keep
  selected group, selected value, preview/display state, cancellation, and
  confirmation synchronized.
- **FR-005**: `sdlg` and `sdlg2` MUST be able to consume the shared color,
  symbolic charset, and display selection behavior without introducing a
  competing local selection model.
- **FR-006**: The feature MUST define a dialog description model for `dlgdsn`
  that separates design-time description, validated runtime dialog, and
  optional persisted representation.
- **FR-006a**: Dialog descriptions MUST use control identifiers that are unique
  within one dialog and command bindings that are unique within one dialog.
- **FR-007**: The dialog description model MUST validate unknown control roles,
  missing labels, invalid navigation order, duplicate control identifiers,
  duplicate command bindings, malformed or truncated persisted input,
  unsupported persisted versions, and unsupported persisted values before a
  runtime dialog is created.
- **FR-008**: Any resource or serialization use in standard dialog or designer
  flows MUST have an explicit ownership boundary and must not occur as a hidden
  side effect of ordinary control interaction.
- **FR-008a**: The designer flow MUST provide a minimal persisted-description
  roundtrip for dialog descriptions and MUST keep runtime-only state outside
  that persisted representation.
- **FR-009**: The `demo` example MUST have a reviewable classification of which
  dialog behavior is supplied by the reusable framework surface and which
  behavior intentionally remains demo-specific.
- **FR-010**: The feature MUST provide green acceptance evidence for file,
  color, and designer flows before `demo`, `sdlg`, `sdlg2`, or `dlgdsn` are
  treated as fully ported.
- **FR-010a**: The primary acceptance surface MUST be framework-level Controls
  and, where persisted dialog descriptions are involved, Serialization
  validation; wave-2 examples are classified as downstream consumers and are
  not required to be fully ported by this feature.
- **FR-011**: The feature MUST keep wave-2 example code as a thin consumer of
  shared dialog behavior wherever a reusable standard flow exists.
- **FR-012**: The feature MUST NOT reopen menu/status/window baseline work,
  general widget work from `009-controls-widgets-and-collections`, runtime mouse
  support, editor/help functionality, or terminal/charset rendering, font, or
  emulation behavior.
- **FR-013**: Every acceptance-critical standard-dialog and designer flow MUST
  be fully operable through keyboard interaction; mouse interaction MAY be
  supported but MUST NOT be required for acceptance.

### Constitution Requirements *(mandatory)*

- **CR-001**: This feature targets the TuiVision Level-2 project and MUST use
  the matching Level-2 Project Environment Registry entry from `constitution.md`
  as binding context.
- **CR-002**: User-facing evidence, documentation, and generated examples MUST
  identify an A11Y review path using WCAG 2.2 Level AA where applicable and a
  text-first fallback otherwise.
- **CR-003**: Learner-facing or shared guidance produced by this feature MUST
  be German-first and English-second, unless a synchronized `.EN.md` companion
  is explicitly chosen.
- **CR-004**: The feature MUST state during planning whether
  `docs/project-statistics.md` and shared AI-agent guidance files require
  synchronized updates.
- **CR-005**: The feature MUST name its primary implementation language during
  planning and confirm that it is on the MSL allow-list in `constitution.md`,
  Principle XI.
- **CR-006**: The feature MUST determine applicable security standards from
  `constitution.md`, Principles XIV-XVIII, with `NIST SSDF` and `CWE Top 25`
  mandatory for Level-2 work and non-applicable standards marked with
  justification.
- **CR-007**: If the feature introduces web/API/HTTP/auth-bearing behavior, it
  MUST declare the selected `OWASP ASVS` level and verification scope.
- **CR-008**: If the feature creates releasable or distributable artefacts, it
  MUST declare the intended `SBOM` / `VEX` evidence path and any required
  provenance / `SLSA` considerations.
- **CR-009**: If the feature changes trust boundaries, externally reachable
  flows, or distributed/service architecture, it MUST state how `CAPEC` and
  `Zero Trust` applicability will be handled.
- **CR-010**: The feature MUST state whether it uses the default evidence files
  in `docs/security/` or an explicitly justified equivalent governance
  location.

### Key Entities *(include if feature involves data)*

- **Standard Dialog Flow**: A complete reusable selection journey with visible
  choices, validation, cancellation, confirmation, and a returned result.
- **File Dialog State**: The synchronized state for current directory, active
  filter, visible file entries, selected file, manual path entry, metadata,
  session-scoped history, validation outcome, and returned value.
- **File Decision Result**: The explicit outcome of a file-oriented dialog,
  covering open, select, or save-target intent without performing file content
  I/O.
- **Color and Display Selection State**: The synchronized state for
  color/display group, symbolic charset choice, selected value,
  preview/display outcome, cancellation, and confirmation.
- **Dialog Description**: A design-time representation of a dialog, including
  controls, labels, navigation order, command bindings, initial values, and
  validation constraints. Control identifiers and command bindings are unique
  within one dialog.
- **Persisted Dialog Representation**: The optional stored form of a dialog
  description, distinct from both the design-time model and the runtime dialog.
  It supports a minimal roundtrip for validated dialog descriptions and rejects
  malformed, truncated, unsupported-version, or semantically invalid input.
- **Wave-2 Example Consumer**: One of `demo`, `dlgdsn`, `sdlg`, or `sdlg2`
  consuming shared dialog behavior while keeping example-specific behavior
  clearly bounded. These examples provide traceability for this feature, but
  their full porting is a later wave-2 delivery step.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Reviewers can validate at least five representative file and
  directory selection scenarios, including directory navigation, filtering,
  manual entry, session-scoped history recall, metadata display, invalid input,
  and save-target selection, without relying on a fully ported wave-2 example.
- **SC-002**: Reviewers can validate at least four representative color,
  symbolic charset, or display selection scenarios, including confirm and
  cancel behavior, through a shared standard flow.
- **SC-003**: Reviewers can validate at least two dialog designer descriptions:
  one accepted description that produces a matching runtime dialog and one
  rejected description that reports clear validation problems, including
  duplicate control identifier or duplicate command-binding rejection.
- **SC-003a**: Reviewers can validate one minimal persisted-description
  roundtrip and at least two rejected persisted inputs, including one malformed
  or truncated input and one semantically invalid description.
- **SC-004**: For each of `demo`, `dlgdsn`, `sdlg`, and `sdlg2`, reviewers can
  identify which major dialog responsibilities are reusable framework behavior
  and which responsibilities remain intentionally example-specific.
- **SC-005**: No reviewed wave-2 example needs a second local implementation of
  file selection, color selection, or dialog-description validation when the
  shared feature provides that behavior.
- **SC-006**: All user-facing proof notes or guide additions created for this
  feature remain usable in a text-first review path and do not encode essential
  meaning only through color, layout, or pointer-only interaction.
- **SC-007**: The feature can be accepted through framework-level validation
  without requiring a fully ported `demo`, `sdlg`, `sdlg2`, or `dlgdsn`
  example in the same delivery.
- **SC-008**: Reviewers can complete every acceptance-critical file, color,
  symbolic charset, display, and designer scenario using keyboard interaction
  only.

## Assumptions

- `010-standard-dialogs-designer` is a framework-readiness feature for wave 2;
  it prepares reusable dialog behavior before the affected examples are
  considered fully ported.
- `Lastenheft_02_StandardDialogsAndDesigner.md` is the canonical input for
  this specification.
- General widget behavior from `009-controls-widgets-and-collections` and
  menu/status/window behavior from `008-controls-revision` are treated as
  prerequisites, not as work reopened by this feature.
- Runtime mouse support, editor/help behavior, terminal/charset behavior, and
  wave-3 or wave-4 example scope remain out of scope, except for symbolic
  charset choice inside wave-2 standard dialogs without terminal rendering,
  font, or emulation effects.
- Persistence for designer descriptions is allowed only when planning defines a
  clear resource or serialization boundary; this feature requires only a
  minimal dialog-description roundtrip, not a broad new persistence framework.
- Full porting of `demo`, `sdlg`, `sdlg2`, and `dlgdsn` is a downstream wave-2
  delivery activity after this framework-readiness feature.
- Standard-dialog history follows the session-only history scope established by
  `009-controls-widgets-and-collections`; cross-session history persistence is
  out of scope.
