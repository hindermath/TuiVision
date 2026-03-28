# Feature Specification: Mandatory Example Wave 1 Ports

**Feature Branch**: `007-port-wave1-examples`  
**Created**: 2026-03-27  
**Status**: Ready for Planning  
**Input**: User description: "$speckit-specify Erstelle eine Spezifikatiion aus der Datei Pflichtenheft.md  am Marker `>>> NAECHSTER SCHRITT <<< 3. MUSS-Beispielwellen 1 bis 4 portieren`, beginnend mit der Welle 1."

## Clarifications

### Session 2026-03-27

- Q: Soll `tutorial` in Welle 1 alle 16 Originalschritte (`tvguid01` bis `tvguid16`) umfassen oder nur einen Teil? → A: `tutorial` umfasst alle 16 Originalschritte (`tvguid01` bis `tvguid16`) in Welle 1.
- Q: Muessen bei Welle-1-Beispielen saemtliche Hilfsprogramme und Generatoren aus dem Originalordner mitportiert werden? → A: Pro Welle-1-Beispiel ist die primaere Beispielanwendung Pflicht; Hilfsprogramme und Generatoren nur bei funktionaler Notwendigkeit.
- Q: Muss die Smoke-Validierung fuer `tutorial` alle 16 Schritte einzeln abdecken? → A: Ja, die Smoke-Validierung fuer `tutorial` muss alle 16 Schritte (`tvguid01` bis `tvguid16`) einzeln abdecken.
- Q: Soll `tutorial` als eine gemeinsame Guide-Seite oder als viele Einzelseiten dokumentiert werden? → A: `tutorial` bekommt eine gemeinsame Guide-Seite mit klar getrennten Abschnitten fuer alle 16 Schritte.
- Q: Welche Form des managed Ersatzverhaltens ist fuer `videomode` verpflichtend? → A: `videomode` muss reale, im aktuellen Terminal zulaessige Groessen- oder Moduswechsel nutzen; wenn das nicht geht, ist ein expliziter, sichtbarer Fallback Pflicht.

## User Scenarios & Testing *(mandatory)*

This feature advances four of the 25 mandatory original examples from
`tv203s/contrib/tvision/examples`: `desklogo`, `msgcls`, `tutorial`, and
`videomode`. Within wave 1, `tutorial` covers all 16 original tutorial steps
from `tvguid01` through `tvguid16`. This feature does not cover the later
mandatory waves 2 to 4, and it does not count any follow-on scope from
`TVDEMOS/` or `TVFM/` as a substitute for the mandatory original examples.

### User Story 1 - Launch a Minimal Desktop Example (Priority: P1)

As a reviewer, I want a runnable `desklogo` port so that I can verify the
managed application shell, desktop drawing, and clean shutdown on the simplest
possible mandatory example.

**Why this priority**: `desklogo` is the lowest-complexity entry point for wave
1 and proves that example work can start on the already accepted application
framework baseline without pulling in later-wave dependencies.

**Independent Test**: Start `desklogo`, observe the static desktop logo in the
application workspace, and confirm that the example exits cleanly through its
documented path.

**Acceptance Scenarios**:

1. **Given** a clean checkout with the wave-1 feature applied, **When** the
   reviewer launches `desklogo`, **Then** the example opens as a minimal
   desktop application and displays its static desktop logo without requiring
   dialog, editor, or help features.
2. **Given** the reviewer follows the documented usage steps, **When** the
   reviewer exits `desklogo`, **Then** the application closes cleanly and
   leaves no ambiguous shutdown state.

---

### User Story 2 - Demonstrate Custom Message Handling (Priority: P2)

As a maintainer, I want a runnable `msgcls` port so that I can validate custom
event classes and message routing on top of the wave-1 application shell.

**Why this priority**: `msgcls` exercises a foundational behavior that is more
interesting than a static view, while still remaining inside the phase-4
application baseline.

**Independent Test**: Start `msgcls`, trigger the documented action that emits
or routes a custom message, and observe the expected visible response.

**Acceptance Scenarios**:

1. **Given** `msgcls` is running, **When** the user performs the documented
   trigger action, **Then** the example shows an observable response that
   proves custom message handling is active.
2. **Given** a reviewer compares the delivered example with the original
   `msgcls` purpose, **When** the example is inspected, **Then** its primary
   value remains centered on user-defined events and message processing rather
   than unrelated later-wave features.

---

### User Story 3 - Learn the Core Concepts Step by Step (Priority: P3)

As a learner, I want the `tutorial` example family ported as the full ordered
set of 16 original wave-1 lessons so that I can understand the TuiVision core
concepts from simple startup behavior through broader application-structure
patterns.

**Why this priority**: The tutorial delivers the clearest didactic value in the
mandatory example set and helps validate that the wave-1 baseline is not only
functional but also teachable.

**Independent Test**: Launch any delivered tutorial step from `tvguid01`
through `tvguid16`, complete its documented primary flow, and confirm that the
step stands on its own while also pointing to the broader progression.

**Acceptance Scenarios**:

1. **Given** a learner starts the first available tutorial step, **When** the
   learner follows the guide, **Then** the step explains one wave-1 concept
   clearly and reaches a visible outcome without requiring knowledge of later
   waves.
2. **Given** the learner moves between delivered tutorial steps, **When** the
   learner reviews the sequence, **Then** the progression is ordered, each step
   remains independently runnable, and the next step is discoverable.

---

### User Story 4 - Validate Display Mode Behavior Safely (Priority: P4)

As a cross-platform reviewer, I want a runnable `videomode` port so that I can
verify how TuiVision examples handle real terminal-supported display-mode or
buffer-dimension changes, and how they present an explicit fallback when the
runtime cannot perform the historical behavior.

**Why this priority**: `videomode` completes the wave-1 checklist and surfaces
one of the main environment-sensitive behaviors that must be handled explicitly
before the project moves on to later example waves.

**Independent Test**: Start `videomode`, invoke the documented mode-change or
size-change action, and confirm either a real terminal-supported effect or the
documented visible fallback outcome.

**Acceptance Scenarios**:

1. **Given** the runtime environment supports the requested display-mode or
   buffer-size transition, **When** the user invokes the change, **Then** the
   example performs a real supported change, remains usable, and shows the
   documented outcome.
2. **Given** the runtime environment does not support the requested transition,
   **When** the user attempts the same action, **Then** the example reports a
   deterministic fallback or limitation instead of failing silently.

### Edge Cases

- What happens when a runtime environment cannot change display mode or buffer
  geometry in the same way as the historical example expected and only a
  visible fallback can be shown?
- How is acceptance handled if one tutorial step is runnable but the learning
  sequence as a whole is no longer ordered or discoverable?
- What happens when a wave-1 example launches correctly but lacks a dedicated
  guide or smoke-validation path?
- How does `msgcls` behave if a custom message arrives in an unexpected order
  or a trigger action is repeated rapidly?
- What happens when a wave-1 example with spatial presentation constraints,
  especially `desklogo` or `videomode`, is run in a terminal that is too small
  for its intended default presentation?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: This feature MUST remain limited to mandatory example wave 1 from
  `Pflichtenheft.md`: `desklogo`, `msgcls`, `tutorial`, and `videomode`.
  Mandatory waves 2 to 4 and follow-on waves 5 to 6 MUST remain out of scope.
- **FR-002**: Each delivered wave-1 example MUST be explicitly traceable to its
  corresponding original example in `tv203s/contrib/tvision/examples` and MUST
  preserve that example's primary teaching purpose.
- **FR-002a**: For each wave-1 example, the primary runnable example
  application is mandatory. Auxiliary tools, generators, or support programs
  found in the same original example directory are required only when they are
  necessary to reproduce the example's visible behavior, assets, or
  repeatable validation path.
- **FR-003**: The feature MUST provide `desklogo` as a minimal desktop
  application that demonstrates application startup, desktop drawing, and clean
  shutdown without requiring controls, dialogs, editor, or help-system
  functionality.
- **FR-004**: The feature MUST provide `msgcls` as an example centered on
  user-defined event classes and message routing, with at least one documented
  interaction that produces an observable custom-message outcome.
- **FR-005**: The feature MUST provide `tutorial` as the full original
  16-step introduction from `tvguid01` through `tvguid16`, covering the
  foundational TuiVision concepts available after the completed
  application-framework phase with a clear progression from simpler to broader
  concepts.
- **FR-006**: Each delivered tutorial step MUST be understandable and runnable
  on its own while still indicating its place in the overall learning sequence.
- **FR-007**: The feature MUST provide `videomode` as an example for
  display-mode or buffer-dimension changes using real transitions that are
  supported by the current managed runtime environment.
- **FR-008**: If the runtime cannot reproduce a requested `videomode` behavior
  exactly, the example MUST surface a deterministic fallback or limitation
  explicitly instead of silently omitting the behavior.
- **FR-008a**: `videomode` MUST prefer a real supported size or mode change
  over simulation-only behavior whenever the current terminal allows it.
- **FR-009**: All four wave-1 examples MUST be independently launchable, must
  show their defining behavior, and must close through a documented clean exit
  path.
- **FR-010**: The feature MUST provide repository-visible automated smoke
  validation for all four wave-1 examples, covering startup, defining behavior,
  and clean shutdown.
- **FR-010a**: The smoke validation for `tutorial` MUST cover each of the 16
  original tutorial steps from `tvguid01` through `tvguid16` individually,
  rather than validating only a shared shell or a representative subset.
- **FR-011**: Each wave-1 example MUST receive its own didactic guide in
  `docs/guides/examples/` that covers learning goal, prerequisites, startup,
  usage flow, architecture hints, and exercises.
- **FR-011a**: `tutorial` MUST be documented as one shared guide page with
  clearly separated sections for all 16 original steps from `tvguid01` through
  `tvguid16`, rather than as 16 unrelated guide pages.
- **FR-012**: The guides for the wave-1 examples MUST state clearly that they
  belong to the mandatory original-example scope and not to the optional
  follow-on waves from `TVDEMOS/` or `TVFM/`.
- **FR-013**: Wave 1 MUST stay within the already accepted application-framework
  baseline. Any dependency on controls, dialogs, editor, help-system features,
  or later-wave terminal-emulation scope MUST be deferred rather than added as
  a hidden prerequisite.
- **FR-014**: The feature MUST update the relevant progress-tracking artifacts
  so reviewers can see that wave 1 has started or completed without confusing
  it with later mandatory waves or optional follow-on waves.
- **FR-015**: Wave-1 acceptance MUST fail if any of the four mandatory examples
  is missing its runnable example delivery, its smoke-validation coverage, or
  its dedicated guide.

### Key Entities *(include if feature involves data)*

- **Wave-1 Example Port**: One delivered managed example that corresponds to a
  single mandatory original example from the wave-1 checklist, centered on the
  primary runnable example application rather than every historical support
  utility in the same folder unless such utilities are functionally necessary.
- **Tutorial Step**: One independently runnable lesson within the delivered
  `tutorial` example family. Wave 1 includes all 16 original steps from
  `tvguid01` through `tvguid16`, ordered as part of the broader learning
  sequence.
- **Example Guide**: One didactic documentation page that explains how to start,
  use, understand, and extend a specific wave-1 example. For `tutorial`, this
  is one shared guide page with clearly separated sections for all 16 original
  steps.
- **Smoke Validation Scenario**: One repeatable acceptance path that proves an
  example starts, demonstrates its main behavior, and exits cleanly. For
  `tutorial`, there is one such scenario per original step from `tvguid01`
  through `tvguid16`.
- **Display Mode Outcome**: The visible result of either a real supported mode
  or size change, or a documented visible fallback, produced when `videomode`
  attempts a transition in a given runtime environment.

## Assumptions

- The Phase-8 entrance gate from `Pflichtenheft.md` section 8.2 is already
  closed, so wave 1 may start immediately without additional gate work.
- The wave-1 checklist in `Pflichtenheft.md` is authoritative for scope and
  includes exactly `desklogo`, `msgcls`, `tutorial`, and `videomode`, with
  `tutorial` interpreted as all 16 original steps from `tvguid01` through
  `tvguid16`.
- The original teaching intent of each example remains more important than a
  one-to-one recreation of every historical visual detail.
- Historical helper programs or asset generators inside an example directory do
  not expand scope by themselves; they are only included when the managed port
  needs them for visible behavior, assets, or reproducible validation.
- `tutorial` is delivered as a family of ordered lessons covering all 16
  original steps, and each delivered lesson remains independently runnable.
- Runtime behavior may differ across macOS, Linux, and Windows/WSL for
  `videomode`; explicit visible fallback behavior is acceptable when the
  runtime cannot perform the historical behavior or any equivalent supported
  transition.
- Each ported wave-1 example is documented in the same work stream as its code
  and validation, rather than leaving guides for a later cleanup phase.
- `tutorial` uses one shared guide page with step-specific sections, while the
  other wave-1 examples each keep their own dedicated guide page.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All 4 mandatory wave-1 examples are delivered, independently
  launchable, and traceable to their original folders in
  `tv203s/contrib/tvision/examples`, with `tutorial` covering `tvguid01`
  through `tvguid16`.
- **SC-002**: Automated validation passes for 100% of the wave-1 examples and
  proves startup, defining behavior, and clean shutdown for each one. For
  `tutorial`, this means 16 of 16 original steps are individually covered.
- **SC-003**: 100% of the delivered wave-1 examples have a dedicated guide, and
  a reviewer can reach each example's primary documented outcome in 5 minutes
  or less by following that guide. For `tutorial`, this requirement is met by
  one shared guide page that clearly covers all 16 original steps.
- **SC-004**: Scope review finds 0 undocumented dependencies on later-wave
  capabilities such as controls, dialogs, editor flows, help-system flows, or
  advanced terminal-emulation scope.
- **SC-005**: 100% of unsupported runtime-specific `videomode` behaviors are
  surfaced through explicit documented fallbacks or limitations rather than
  silent failure, while supported environments perform a real visible change.
