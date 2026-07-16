# Security and A11Y Requirements Checklist

**Purpose**: Prüft Sicherheits-, Datei-, Parser-, Accessibility- und
Governance-Anforderungen auf Vollständigkeit.
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Security Boundaries

- [x] CHK001 Sind Resource-, Help-, Datei- und Generatorgrenzen geschlossen, bounded und atomar ablehnend beschrieben? [Completeness, Spec FR-020-FR-025]
- [x] CHK002 Sind neue Runtime-Pakete, Dienste, Datenbanken, Shells, Prozesse, PTYs und native Bridges ausdrücklich ausgeschlossen? [Consistency, Spec FR-039]
- [x] CHK003 Ist festgelegt, dass Maus-Einstellungen keinen Host- oder globalen Zustand verändern? [Clarity, Spec FR-028a]
- [x] CHK004 Sind Secret-, Supply-Chain-, Scope- und Exact-Head-Nachweise Teil der messbaren Delivery-Gates? [Coverage, Spec FR-041-FR-043]

## Accessibility and Learning

- [x] CHK005 Ist Tastaturvollständigkeit für alle zehn Beispiele und besonders den Mausdialog verlangt? [Completeness, Spec FR-028-FR-031, SC-004]
- [x] CHK006 Sind Fokus, Shortcuts, Status, Ablehnung und High Contrast text-first nachvollziehbar gefordert? [Coverage, Spec FR-030-FR-031]
- [x] CHK007 Sind DE-first/EN-second CEFR-B2-Guides mit Zweck, Start, Bedienung, Quelle, Abweichung, A11Y und Proof-Grenze vollständig definiert? [Completeness, Spec FR-034]
- [x] CHK008 Ist die didaktische Inline-Kommentarprüfung auf nicht triviale Logik begrenzt und warum-orientiert? [Clarity, Spec FR-032]

## Governance Applicability

- [x] CHK009 Sind alle sieben installierten Presets mit Version und anwendbaren Nachweisen genannt? [Completeness, Spec GR-001-GR-007]
- [x] CHK010 Sind ASVS, AI-SBOM, regulatorische, Cloud- und Zero-Trust-N/A-Entscheidungen triggerbasiert? [Clarity, Spec GR-001-GR-002]
- [x] CHK011 Ist Cross-Platform-Runtime-Proof anwendbar und neue Skriptparität ohne neues Skript begründet N/A? [Consistency, Spec GR-005]
- [x] CHK012 Sind alle fünf gepflegten Agent-Oberflächen gemeinsam zu prüfen und zu synchronisieren? [Coverage, Spec GR-006]

## Notes

- Alle Security-, A11Y- und Governance-Anforderungen sind vollständig und ohne offenen Trigger.
