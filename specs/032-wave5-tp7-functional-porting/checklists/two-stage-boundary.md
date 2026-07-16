# Two-Stage Boundary Requirements Checklist

**Purpose**: Prüft die Trennung zwischen funktionaler Feature-032-Lieferung,
späterer Showcase-Stufe und weiterhin blockierter Wave 6.
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Stage Boundary

- [x] CHK001 Ist Feature 032 eindeutig als funktionale erste Wave-5-Stufe definiert? [Clarity, Spec Scope Boundaries]
- [x] CHK002 Ist Mindest-Sichtbarkeit von vollständiger Showcase-Reife unterschieden? [Consistency, Spec FR-013, FR-035-FR-037]
- [x] CHK003 Ist pro Beispiel genau eine vollständige Delta-Zeile mit Visual-, Interaktions-, Layout- und A11Y-Dimension gefordert? [Completeness, Spec FR-035-FR-037]
- [x] CHK004 Sind `CompleteIn032` und `Stage2Required` als geschlossene Delta-Entscheidungen definiert? [Clarity, Spec Decision and Follow-up Model]

## Ordering and Stop Conditions

- [x] CHK005 Ist Feature 033 ausdrücklich nicht Bestandteil dieses Laufs? [Consistency, Spec FR-047]
- [x] CHK006 Bleibt Wave 6 bis zu beiden Wave-5-Stufen und tatsächlichem Delta-Review blockiert? [Completeness, Spec FR-048]
- [x] CHK007 Stoppt ein breiter oder closure-widersprechender Frameworkfund den betroffenen Slice statt Scope-Erweiterung auszulösen? [Coverage, Spec FR-008-FR-010]
- [x] CHK008 Ist der spätere Showcase-Intake ausschließlich aus realer Feature-032-Evidence abzuleiten? [Traceability, Spec User Story 5]

## Delivery Integrity

- [x] CHK009 Sind Archivierung, Statusflächen, Statistik und Agent-Parität als gemeinsamer Abschlusszustand gefordert? [Completeness, Spec FR-045-FR-050]
- [x] CHK010 Ist der externe Community-Issue ausdrücklich nicht blockierend und ohne neues Preset-Release behandelt? [Assumption, Spec Assumptions]
- [x] CHK011 Ist ein nicht leerer PR mit Review-Konvergenz, Merge und sauberem Main-Sync messbar vorgeschrieben? [Acceptance Criteria, Spec SC-013]
- [x] CHK012 Verhindert die Scope-Grenze Wave-6-Code, TVFM-Portierung und Post-Wave-6-Audit? [Consistency, Spec Out of Scope]

## Notes

- Zweistufige Liefergrenze und Reihenfolge sind eindeutig; keine weitere Klärung nötig.
