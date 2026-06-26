# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Datei haelt die sichtbare Abarbeitungsreihenfolge der vorhandenen Lastenhefte fest. Sie ist eine Vorbereitung fuer spaetere Spec-Kit-Laeufe und startet selbst keinen Lauf.

*This file records the visible processing order of existing requirements documents. It prepares later Spec Kit runs and does not start a run by itself.*

## Spec-Kit-Intake-Regel / Spec Kit Intake Rule

- Diese Datei ist ein Ordnungsdokument und selbst kein Spec-Kit-Intake.
- Aktive Lastenhefte ohne Feature-Branch-Suffix koennen als Intake dienen, wenn sie Scope, Nicht-Ziele, Anforderungen, Akzeptanzkriterien und einen kopierbaren `/speckit-specify`-Prompt enthalten.
- Lastenhefte mit Feature-Branch-Suffix wie `.001-*` oder `.009-*` gelten als historisch oder abgeschlossen und werden nicht erneut gestartet.
- Vor jedem neuen Lauf wird zuerst der aktuelle Repository-Stand geprueft; erledigte Punkte werden als `AlreadySatisfied` oder `N/A` dokumentiert, nicht neu implementiert.

- This file is an ordering document and not itself a Spec Kit intake.
- Active Lastenhefte without a feature-branch suffix can be used as intake when they include scope, non-goals, requirements, acceptance criteria, and a copyable `/speckit-specify` prompt.
- Lastenhefte with a feature-branch suffix such as `.001-*` or `.009-*` are historical or completed and are not started again.
- Before every new run, first check the current repository state; completed items are documented as `AlreadySatisfied` or `N/A`, not reimplemented.


<!-- secure-development-hardening-order:start -->
## Automatisch ermittelte Lastenheft-Reihenfolge / Automatically Detected Requirements Order

Diese Tabelle wird aus `Lastenheft*.md` im Repository-Root erzeugt. Sie ist eine Vorbereitung fuer spaetere Spec-Kit-Laeufe und startet selbst keinen Lauf. Manuelle Projektentscheidungen ausserhalb dieses markierten Abschnitts bleiben erhalten.

*This table is generated from `Lastenheft*.md` in the repository root. It prepares later Spec Kit runs and does not start a run. Manual project decisions outside this marked section remain preserved.*

| Rang | Lastenheft | Gruppe | Status |
|---:|---|---|---|
| 1 | `Lastenheft_Constitution_Change.md` | Governance/Baseline | aktiv / active |
| 2 | `Lastenheft_01_ControlsWidgetsAndCollections.009-controls-widgets-and-collections.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 3 | `Lastenheft_06_A11Y_Framework.md` | Kernlogik/Runtime | aktiv / active |
| 4 | `Lastenheft_ControlsRevision.008-controls-revision.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 5 | `Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 6 | `Lastenheft_Wave1-Functional-Hardening.014-wave1-functional-hardening.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 7 | `Lastenheft_Wave1-Visual-Component-Remediation.md` | Kernlogik/Runtime | aktiv / active |
| 8 | `Lastenheft_Wave2-Visual-Component-Remediation.013-wave2-visual-component-remediation.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 9 | `Lastenheft_Wave3-Visual-Component-Porting.md` | Kernlogik/Runtime | aktiv / active |
| 10 | `Lastenheft_Wave4-Visual-Component-Porting.md` | Kernlogik/Runtime | aktiv / active |
| 11 | `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md` | UI/A11Y/Dokumentation | aktiv / active |
| 12 | `Lastenheft_Secure-Development-Hardening.md` | Secure-Development-Hardening | aktiv / active |
| 13 | `Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` | Secure-Development-Hardening | aktiv / active |
| 14 | `Lastenheft_02_StandardDialogsAndDesigner.010-standard-dialogs-designer.md` | Weitere Anforderungen | archiviert oder abgeschlossen / archived or completed |
| 15 | `Lastenheft_03_EditorHelpAndResourcesHardening.md` | Weitere Anforderungen | aktiv / active |
| 16 | `Lastenheft_04_MouseSupportAndInteraction.md` | Weitere Anforderungen | aktiv / active |
| 17 | `Lastenheft_05_TerminalCharsetAndEmulation.md` | Weitere Anforderungen | aktiv / active |
<!-- secure-development-hardening-order:end -->
