# Feature Specification: M-07 Closure and Phase-8 Entrance Gate

**Feature Branch**: `006-close-phase8-gate`  
**Created**: 2026-03-24  
**Status**: Ready for Planning  
**Input**: User description: "Bitte >>> NAECHSTER SCHRITT <<< 2. **M-07 vollstaendig schliessen und das Eingangstor fuer Phase 8 nachweisbar schliessen** in Pflichtenheft.md ausfuehren."

## Clarifications

### Session 2026-03-24

- Q: Soll das Phase-8-Gate fuer die Coverage nur `TuiVision.Controls` mit 70 % erzwingen oder weitere Module verbindlich einschliessen? → A: Das Eingangstor verlangt fuer `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` jeweils mindestens 70 % Line Coverage.
- Q: Duerfen heute noch als `geplant` markierte nicht-treiberspezifische Framework-Zeilen im M-07-Ledger ohne neue Implementierung nur narrativ abgeschlossen werden? → A: Nein. Nicht-treiberspezifische `geplant`-Zeilen muessen in diesem Feature real implementiert und getestet werden; nur echte Architektur-Ersetzungen oder obsolete Spezialfaelle duerfen als `bewusst ausgelassen + Begruendung` enden.
- Q: Reicht fuer den Phase-8-Gate-Abschluss ein alternatives Review-Marker-Artefakt oder ist ein eigener Git-Commit Pflicht? → A: Der Phase-8-Gate-Abschluss braucht zwingend einen eigenen dedizierten Git-Commit.
- Q: Bedeutet `full-suite validation evidence` im Gate einen erfolgreichen Lauf ueber alle Testprojekte im Repository oder nur ueber die Gate-nahen Module? → A: `Full-suite validation` bedeutet `dotnet test` fuer alle Testprojekte im Repository.
- Q: Sind Linux- und Windows/WSL-Kompatibilitaetsnachweise fuer dieses Gate immer Pflichtblocker oder nur bei materiell plattformrelevanten Aenderungen? → A: Linux- und Windows/WSL-Nachweise sind Pflicht, wenn die Aenderungen Laufzeit-, Terminal-, Portabilitaets- oder Build-Verhalten materiell betreffen; sonst reicht eine begruendete Nicht-Anwendbarkeit.

### Session 2026-03-25

- Q: Gilt das erweiterte Coverage-Gate pro Ziel-Assembly oder nur als aggregierter Report? → A: Die 70-%-Huerde gilt pro Ziel-Assembly; die Tests duerfen aus beliebigen Repository-Testprojekten kommen, solange der finale Coverage-Report `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` getrennt ausweist.
- Q: Duerfen Gate-Module ihre 70 % auch nur mit Platzhalter- oder No-op-Code erreichen? → A: Nein. Ein Gate-Modul darf nicht nur durch Platzhalter-/No-op-Code und triviale Tests die 70 % erreichen; wenn es im Gate bleibt, braucht es reale verbleibende Verantwortung, sonst muss es vor Gate-Schluss sauber entfernt oder umgeordnet werden.

## User Scenarios & Testing *(mandatory)*

This feature does not start any of the 25 mandatory original examples from
`tv203s/contrib/tvision/examples`. It closes the remaining proof and quality
gates that must be satisfied before example waves 1 to 4 may begin. `TVDEMOS/`
and `TVFM/` remain follow-on scope and do not replace the mandatory examples.

### User Story 1 - Resolve the remaining M-07 proof gap (Priority: P1)

As a reviewer, I want every historical `.cc` implementation file in the M-07
ledger to end in a final, reviewable proof state so that M-07 can be marked
complete without unresolved "pending test" placeholders.

**Why this priority**: The Pflichtenheft names M-07 closure as the immediate
next step before Phase 8. As long as rows remain in a provisional status,
mandatory example work is still formally blocked.

**Independent Test**: Inspect `docs/porting-status.md`, sample entries from
`TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
`TuiVision.Compatibility`, and driver areas, and confirm that each row is
either explicitly verified or consciously excluded with a rationale.

**Acceptance Scenarios**:

1. **Given** a ledger row currently states `portiert + Test ausstehend`,
   **When** this feature is complete, **Then** the row is updated to a final
   allowed proof state with traceable evidence.
2. **Given** a historical responsibility is intentionally not reproduced
   one-to-one, **When** the reviewer inspects the ledger, **Then** the entry
   explains the managed replacement or conscious omission instead of leaving an
   unresolved gap.

---

### User Story 2 - Close the Phase-8 entrance evidence package (Priority: P2)

As a maintainer, I want the remaining build, test, coverage, formatting, and
API-documentation gate evidence packaged explicitly so that the Phase-8 start
decision is based on repository-visible proof rather than assumptions.

**Why this priority**: The current Phase-7 work separated the remaining
entrance-gate items deliberately. Those items now need one focused closure step
before mandatory example waves may begin.

**Independent Test**: Review the resulting evidence package against the six
entrance-gate criteria in the Pflichtenheft and confirm that each criterion has
an explicit pass/fail status with supporting artifacts.

**Acceptance Scenarios**:

1. **Given** a reviewer checks the entrance-gate criteria in `Pflichtenheft.md`,
   **When** this feature is complete, **Then** each criterion has current
   evidence and no undocumented blocker remains.
2. **Given** scope-relevant public API or XML-comment changes were made during
   gate closure, **When** the evidence package is reviewed, **Then** refreshed
   API-documentation validation is part of the proof.

---

### User Story 3 - Keep Phase 8 blocked until closure is explicit (Priority: P3)

As a project lead, I want the repository artifacts to state clearly whether
Phase 8 may start so that the team does not begin mandatory example porting on
an incompletely evidenced framework baseline.

**Why this priority**: The project already has a defined ordering in the
Pflichtenheft. A clean, explicit gate decision reduces rework and keeps later
example planning aligned.

**Independent Test**: Compare the updated Pflichtenheft state, the M-07 ledger,
and the gate evidence package, and confirm that a reviewer can determine in one
pass whether example wave 1 is allowed to start.

**Acceptance Scenarios**:

1. **Given** the team reviews the prioritized rest-work section,
   **When** this feature is complete, **Then** the repository shows either a
   closed Phase-8 gate with evidence or explicitly documented remaining blockers.
2. **Given** example waves 1 to 4 remain out of scope for this feature,
   **When** a maintainer reviews the resulting artifacts, **Then** the phase
   boundary remains explicit and the examples are still blocked until the gate
   is formally closed.

### Edge Cases

- How is a ledger row handled when one automated test covers multiple historical
  `.cc` sources instead of one row mapping to exactly one test?
- What happens when a row still points to a planned or merged target area rather
  than to a one-class-to-one-file historical equivalent?
- How is the gate handled when the build and tests succeed, but a skipped or
  ignored test is discovered without a matching tracked issue?
- What happens if a Phase-8 gate module still contains only placeholder or
  no-op code while the coverage threshold is being measured?
- What happens if local and CI coverage results disagree for one gate
  assembly?
- What happens when no public API or XML-comment change occurs while the gate is
  being closed?
- How is the evidence package interpreted when macOS, Linux, and Windows/WSL
  validation expose different environment-specific observations?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: This feature MUST remain limited to closing the formal `M-07`
  proof and the documented Phase-8 entrance gate. Mandatory example waves 1 to
  4 MUST remain out of scope until the gate is explicitly closed.
- **FR-002**: The feature MUST resolve every historical `.cc` entry in
  `docs/porting-status.md` to one of the final allowed proof states
  `portiert + getestet` or `bewusst ausgelassen + Begruendung`.
- **FR-003**: The feature MUST leave zero ledger rows in the provisional state
  `portiert + Test ausstehend` after gate closure is claimed.
- **FR-004**: Every row concluded as `portiert + getestet` MUST have traceable
  automated test evidence in the repository.
- **FR-005**: Every row concluded as `bewusst ausgelassen + Begruendung` MUST
  explain why the historical responsibility is consciously omitted, replaced, or
  merged in the managed architecture.
- **FR-006**: Every non-driver framework entry that is still mapped to a
  `geplant` target area at the start of this feature MUST be implemented and
  covered by automated tests before M-07 closure is claimed, unless the entry
  is reclassified as a true architecture replacement or obsolete special case
  with explicit rationale.
- **FR-007**: The feature MUST provide repository-visible full-suite validation
  evidence by running `dotnet test` successfully for all test projects in the
  repository, including the gate scope across `TuiVision.Core`,
  `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, and the already consolidated driver baseline.
- **FR-008**: The feature MUST record whether any test is skipped or ignored
  within the gate scope. A skipped or ignored test without a corresponding
  tracked issue MUST prevent the gate from being reported as closed.
- **FR-009**: The feature MUST provide current coverage evidence for
  `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`. Each of the five
  modules MUST reach at least 70% line coverage before Phase 8 may start.
  Coverage is evaluated per target assembly, and the final evidence package
  MUST report the five assemblies separately even when the exercising tests are
  distributed across multiple repository test projects.
- **FR-009a**: A module counted toward the Phase-8 coverage gate MUST represent
  real remaining framework responsibility. Placeholder-only or no-op-only code
  paired with trivial tests MUST NOT be used to satisfy the gate. If such a
  module has no real remaining responsibility, it MUST be removed from the gate
  scope by explicit restructuring or scope correction before closure is claimed.
  The same change MUST update the gate-defining proof surfaces that still name
  the module, including the specification, planning artifacts, and the final
  gate-review documents.
- **FR-009b**: If local and CI coverage results diverge for any gate assembly,
  the gate MUST remain open until the discrepancy is explained and the final
  repository-visible evidence package identifies which result is authoritative.
- **FR-010**: The feature MUST provide current formatting and build compliance
  evidence for the gate scope as part of the same entrance-gate proof package.
- **FR-011**: If public API signatures or XML comments change within this gate
  closure scope, the feature MUST refresh the API-documentation validation and
  include its outcome in the evidence package. If no such changes occur, the
  evidence package MUST state that no refresh was required.
- **FR-012**: The feature MUST update the Pflichtenheft-facing gate status so a
  reviewer can determine the current state of all six entrance-gate criteria
  without reconstructing context from commit history or oral handover.
- **FR-013**: Once all gate criteria are satisfied, the feature MUST create and
  identify a dedicated gate-closure git commit that documents the closure and
  references the relevant proof artifacts.
- **FR-014**: The feature MUST preserve the project rule that the 25 mandatory
  original examples stay blocked until the gate is formally closed and recorded.
- **FR-015**: The feature MUST keep the primary execution workflow on the two
  documented macOS machines while preserving reviewable Linux and Windows/WSL
  compatibility evidence whenever the implemented changes materially affect
  runtime behavior, terminal behavior, portability, or build reliability. If
  the closure work is limited to documentation or other non-runtime proof
  maintenance, the evidence package MUST state why additional Linux and
  Windows/WSL execution was not applicable.

### Key Entities *(include if feature involves data)*

- **Porting Status Entry**: One row in `docs/porting-status.md` that links a
  historical `.cc` file to a current proof state, evidence, and rationale.
- **Final Proof State**: One of the two allowed end states for M-07 closure:
  `portiert + getestet` or `bewusst ausgelassen + Begruendung`.
- **Entrance-Gate Criterion**: One of the six formal checks in the Phase-8 gate
  that must be evidenced before mandatory example work may begin.
- **Validation Evidence Package**: The repository-visible collection of proof
  artifacts that shows current build, test, coverage, formatting, API-doc, and
  compatibility status for the gate scope.
- **Coverage Result**: One assembly-specific line-coverage result for
  `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, or `TuiVision.Drivers.Console`, measured and
  reported separately inside the final evidence package.
- **Gate-Scoped Module**: One module still counted toward the hard Phase-8
  coverage gate because it retains real remaining framework responsibility. A
  module with no such remaining responsibility must be explicitly restructured
  out of the gate and removed from the gate-defining proof surfaces.
- **Gate-Closure Commit**: The dedicated git commit that states the gate is
  closed and points to the supporting evidence.
- **Mandatory Example Wave**: One of the required example-porting waves that
  remains blocked until this feature's proof obligations are satisfied.

## Assumptions

- The Phase-7 driver consolidation feature remains the accepted baseline and is
  not reopened except where its still-pending proof rows require final status
  updates.
- `docs/porting-status.md` remains the primary human-readable proof ledger for
  M-07 and is the authoritative place to confirm row-level closure.
- The entrance-gate closure will likely require additional tests and possibly
  documentation updates in `TuiVision.Core`, `TuiVision.Controls`,
  `TuiVision.Serialization`, `TuiVision.Compatibility`, and
  `TuiVision.Drivers.Console`, but it does not start example porting itself.
- Any module retained inside the hard coverage gate is assumed to carry real
  remaining runtime or framework responsibility rather than placeholder-only
  content.
- If local and CI evidence diverge for a gate assembly, closure is deferred
  until the discrepancy is resolved and one authoritative repository-visible
  result is named.
- Linux and Windows/WSL compatibility evidence may remain manual or
  semi-automated unless the same feature explicitly upgrades them into a harder
  gate.
- The Pflichtenheft-defined numeric coverage threshold is mandatory where
  explicitly stated; this feature does not invent extra numeric quotas beyond
  those existing rules.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of the 151 historical `.cc` ledger rows end in a final proof
  state, and 0 rows remain in `portiert + Test ausstehend`.
- **SC-002**: Reviewers can trace 100% of rows marked `portiert + getestet` to
  repository-visible automated test evidence without ad-hoc archaeology.
- **SC-003**: The Phase-8 entrance-gate review records an explicit status for
  all six criteria and leaves 0 undocumented blockers.
- **SC-004**: The repository contains current coverage evidence for
  `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`,
  `TuiVision.Compatibility`, and `TuiVision.Drivers.Console`, and each of the
  five modules meets or exceeds 70% line coverage before Phase 8 is declared
  open; reviewers can read that result assembly-by-assembly rather than only as
  one aggregated coverage number, and no unresolved local-versus-CI conflict
  remains for any gate assembly.
- **SC-005**: A project lead can determine in one review pass whether mandatory
  example wave 1 may begin, using only repository artifacts updated by this
  feature.
- **SC-006**: For every closure change that materially affects runtime behavior,
  terminal behavior, portability, or build reliability, the evidence package
  either includes Linux and Windows/WSL validation results or records a
  reviewable not-applicable rationale when no such execution was required.
