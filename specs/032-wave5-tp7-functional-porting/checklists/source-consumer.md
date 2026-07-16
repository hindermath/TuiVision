# Quellen- und Consumer-Requirements / Source and Consumer Requirements

**Purpose**: Prüft Vollständigkeit, Eindeutigkeit und Traceability der
historischen Quellen- und Consumer-Anforderungen vor der Planung.
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Requirement Completeness

- [x] CHK001 Sind genau alle 15 bindenden Pascal-Quellen mit einer eindeutigen Rollenmenge beschrieben? [Completeness, Spec FR-002]
- [x] CHK002 Sind Zweck, moderne Zuordnung und Abweichungsgrenze für jede zulässige Quellenrolle definiert? [Completeness, Spec FR-002-FR-003]
- [x] CHK003 Sind genau die sechs Consumer-Gruppen W5-001 bis W5-006 und ihre Ausgangsentscheidung dokumentiert? [Completeness, Spec FR-005-FR-007]
- [x] CHK004 Ist der Help-Querschnitt zwischen Demo, Editor und vorhandenem Help-Stack ausdrücklich abgedeckt? [Coverage, Spec User Story 2]

## Requirement Clarity

- [x] CHK005 Ist eindeutig, dass Quellen genau eine primäre Rolle erhalten, aber mehrere moderne Beispiele fachlich unterstützen dürfen? [Clarity, Spec FR-002]
- [x] CHK006 Ist `IntentionalOmission` mit historischen Zweck, Alternative, Grund, sichtbarer Wirkung und Follow-up-Grenze messbar definiert? [Clarity, Spec FR-003]
- [x] CHK007 Ist die Grenze zwischen Beispielkomposition und wiederverwendbarem Frameworkverhalten eindeutig beschrieben? [Clarity, Spec FR-008-FR-010]
- [x] CHK008 Ist die gemeinsam kompilierte Beispielassembly als Identitätslösung erlaubt, ohne die zehn eigenständigen Projekte aufzuheben? [Clarity, Spec FR-040a]

## Consistency and Traceability

- [x] CHK009 Stimmen Source-, Consumer- und Beispielzahlen zwischen User Stories, Anforderungen und Erfolgskriterien überein? [Consistency, Spec SC-001-SC-003]
- [x] CHK010 Bleiben `TVDEMOS/`, `TVFM/`, `tv203s/` und externe Checkouts überall read-only? [Consistency, Spec FR-004, FR-044]
- [x] CHK011 Ist jede Abweichung von `UseExistingFramework` an reproduzierbare Evidence gebunden? [Traceability, Spec FR-006-FR-010]
- [x] CHK012 Ist die spätere Showcase-Stufe aus der tatsächlichen zehnzeiligen Delta-Matrix ableitbar, ohne Feature 033 vorwegzunehmen? [Traceability, Spec FR-035-FR-037, FR-047]

## Notes

- Erster und wiederholter Review-Pass: alle Punkte erfüllt.
- Keine weitere formale Klärung für Plan oder Tasks erforderlich.
