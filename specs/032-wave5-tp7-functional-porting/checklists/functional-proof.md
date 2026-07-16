# Functional-Proof Requirements Checklist

**Purpose**: Prüft, ob die Anforderungen für echte App-Loop-, Zustands-, View-
und Cell-Nachweise vollständig und messbar sind.
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Primary and Alternate Flows

- [x] CHK001 Ist pro Beispiel mindestens ein tastaturerreichbarer Kernpfad mit normalem Start beschrieben? [Completeness, Spec FR-012-FR-015]
- [x] CHK002 Ist `Tp7Calculator` als test-first Referenz-Slice mit Projekt, Test, Guide und Evidence festgelegt? [Completeness, Spec FR-017]
- [x] CHK003 Sind Demo-, Editor-, Help-, Resource-, Generator-, Fach- und Mauspfade getrennt akzeptierbar beschrieben? [Coverage, Spec User Stories 1-4]
- [x] CHK004 Ist Command-Verarbeitung genau einmal sowie Fokus-, Aktivierungs- und Fensterverhalten an vorhandene Frameworkpfade gebunden? [Clarity, Spec FR-018-FR-019]

## Determinism and Negative Flows

- [x] CHK005 Sind Division durch null, ungültige Help-Quelle, unbekannter Kontext, unbekannter Resource-Typ, doppelter Schlüssel und ungültige Länge als Ablehnungsfälle erfasst? [Coverage, Spec Edge Cases]
- [x] CHK006 Sind Datei- und Generatorpfade ausschließlich auf Fixtures und Test-Temp-Ziele begrenzt? [Clarity, Spec FR-021, FR-025]
- [x] CHK007 Sind feste Kalender-Fixture, fester Puzzle-Startzustand und feste Zugfolge vorgeschrieben? [Measurability, Spec FR-026a]
- [x] CHK008 Ist begrenzte Idle-Arbeit ohne Host-Speichersemantik definiert? [Clarity, Spec FR-027]

## Proof Quality

- [x] CHK009 Verlangt der primäre Proof einen echten App-Loop oder gleichwertigen Dispatch-Pfad statt direkter Helfer? [Consistency, Spec FR-014-FR-016]
- [x] CHK010 Sind konkreter Zustand, relevante View-Identität und Buffer-/Cell-Evidence gemeinsam erforderlich? [Measurability, Spec FR-015, SC-005]
- [x] CHK011 Ist für eingeschränkte Terminalgrößen und Capability-Fallback ein beobachtbarer textlicher Zustand gefordert? [Coverage, Spec Edge Cases, FR-028-FR-031]
- [x] CHK012 Sind vollständige Release-, Coverage-, DocFX/A11Y- und Plattformnachweise als messbare Abschlussgates festgelegt? [Acceptance Criteria, Spec FR-041-FR-042, SC-008-SC-010]

## Notes

- Alle Primär-, Alternativ-, Ausnahme- und Proof-Anforderungen sind planungsreif.
