# Wave and Delivery Requirements Checklist: Feature 031

**Purpose**: Prüft die Anforderungsqualität für kausale Wave-Freigabe,
Exact-Head-Evidence, Reviews und MergeAndSync.
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Kausale Wave-Zustände / Causal Wave States

- [x] CHK001 Ist Wave 5 bis zu grünen lokalen, remoten, Review- und
  Exact-Head-Gates ausdrücklich blockiert? [Completeness, Spec §FR-024]
- [x] CHK002 Ist `Eligible` an den wahrheitsgemäß nachgewiesenen Feature-Merge
  und nicht nur an lokale Tests gebunden? [Clarity, Spec §FR-025]
- [x] CHK003 Ist Wave 6 nach Feature 031 auf höchstens `ConditionallyReady`
  begrenzt und zusätzlich an Wave 5 plus Delta-Review gebunden? [Consistency,
  Spec §FR-026]
- [x] CHK004 Ist der Unterschied zwischen reviewtem Feature-Head und kausalem
  Post-Merge-Closeout eindeutig beschrieben? [Clarity, Spec §Clarifications,
  FR-028]
- [x] CHK005 Verlangen die Erfolgskriterien denselben finalen Wave-Zustand wie
  die funktionalen Anforderungen? [Consistency, Spec §SC-010]

## Acceptance-Gates und Reviews / Acceptance Gates and Reviews

- [x] CHK006 Sind gezielte Validatoren, Full Release, Coverage, Format, DocFX,
  A11Y, Text-First, Security, Scope, Supply Chain, Agent-Parität und Plattformen
  vollständig gefordert? [Completeness, Spec §FR-020]
- [x] CHK007 Ist die Coverage-Grenze für alle fünf benannten Assemblies mit
  mindestens 70 Prozent messbar? [Measurability, Spec §FR-021]
- [x] CHK008 Muss Remote-Evidence Head, Workflow, Job, Plattform und
  ausgeführten Command enthalten? [Traceability, Spec §FR-022]
- [x] CHK009 Ist ein grüner Jobname ohne Acceptance-Command ausdrücklich
  unzureichend? [Edge Case, Spec §User Story 4, FR-022]
- [x] CHK010 Sind fehlende oder quota-begrenzte Reviewer als fehlend statt als
  Pass definiert? [Clarity, Spec §FR-023]
- [x] CHK011 Ist der enge Admin-Bypass auf ausschließlich offene
  Human-Approval-Policy nach grünen technischen Gates begrenzt?
  [Permission Boundary, Spec §Assumptions]

## Synchronisierung und Abschluss / Synchronization and Completion

- [x] CHK012 Müssen Wave-, Pflichtenheft-, Reihenfolge-, Agent-, Statistik-,
  Archiv- und Feature-Evidence-Oberflächen denselben Zustand nennen?
  [Consistency, Spec §FR-027]
- [x] CHK013 Ist der Lastenheft-Rename erst nach fachlicher Acceptance
  zulässig? [Ordering, Spec §FR-031]
- [x] CHK014 Ist der autonome Endzustand mit vollständigem Task-Zähler,
  `Completed`, `Retrospective`, `N/A` und sauberem `main` messbar?
  [Acceptance Criteria, Spec §SC-012]
- [x] CHK015 Ist ausdrücklich festgelegt, dass der Lauf Wave 5 nicht
  automatisch startet? [Boundary, Spec §FR-036]

## Notes

- Durchführung: Vergleiche jede Zeile mit Lastenheft, Delivery-Auftrag,
  Autonomous-Runbook und den Success Criteria. Ein Widerspruch blockiert Plan.
- Ergebnis 2026-07-16: 15/15 erfüllt. Der Human-Approval-only Bypass ist an
  grüne technische Gates und null umsetzbare Threads gebunden.
