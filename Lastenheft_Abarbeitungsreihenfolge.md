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
| 3 | `Lastenheft_06_A11Y_Framework.023-a11y-framework.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 4 | `Lastenheft_ControlsRevision.008-controls-revision.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 5 | `Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 6 | `Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` | Kernlogik/Runtime | aktiv, aber nicht vor 024 priorisiert / active, not prioritized before 024 |
| 7 | `Lastenheft_Wave1-Functional-Hardening.014-wave1-functional-hardening.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 8 | `Lastenheft_Wave1-Visual-Component-Remediation.017-wave1-visual-component-remediation.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 9 | `Lastenheft_Wave2-Visual-Component-Remediation.013-wave2-visual-component-remediation.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 10 | `Lastenheft_Wave3-Visual-Component-Porting.019-wave3-visual-component-porting.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 11 | `Lastenheft_Wave4-Visual-Component-Porting.022-wave4-visual-component-porting.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 12 | `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.015-didactic-comment-hardening.md` | UI/A11Y/Dokumentation | archiviert oder abgeschlossen / archived or completed |
| 13 | `Lastenheft_RL-SE-Checklist-Selbstpruefung.md` | RL-SE-/Checklist-Selbstpruefung | aktiv / active |
| 14 | `Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` | GSDB-Spec-Kit-Intensivpruefung | aktiv / active |
| 15 | `Lastenheft_Secure-Development-Hardening.016-secure-development-hardening.md` | Secure-Development-Hardening | archiviert oder abgeschlossen / archived or completed |
| 16 | `Lastenheft_02_StandardDialogsAndDesigner.010-standard-dialogs-designer.md` | Weitere Anforderungen | archiviert oder abgeschlossen / archived or completed |
| 17 | `Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md` | Weitere Anforderungen | archiviert oder abgeschlossen / archived or completed |
| 18 | `Lastenheft_04_MouseSupportAndInteraction.020-mouse-support-interaction.md` | Weitere Anforderungen | archiviert oder abgeschlossen / archived or completed |
| 19 | `Lastenheft_05_TerminalCharsetAndEmulation.021-terminal-charset-hardening.md` | Weitere Anforderungen | archiviert oder abgeschlossen / archived or completed |
| 20 | `Lastenheft_08_TV203-FreeVision-Conformance-Audit.024-tv203-freevision-conformance-audit.md` | Framework-Konformitätsaudit | archiviert oder abgeschlossen / archived or completed |
| 21 | `Lastenheft_09_Pre-Wave5-Conformance-Closure.027-pre-wave5-conformance-closure.md` | Pre-Wave-5-Konformitätsabschluss | archiviert oder abgeschlossen / archived or completed |
| 22 | `Lastenheft_10_Core-Runtime-Conformance-Hardening.025-core-runtime-conformance-hardening.md` | Core-Runtime-Konformität | archiviert oder abgeschlossen / archived or completed |
| 23 | `Lastenheft_11_Component-Data-Conformance-Hardening.026-component-data-conformance-hardening.md` | Komponenten-/Daten-Konformität | archiviert oder abgeschlossen / archived or completed |
| 24 | `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.028-pre-wave5-wave6-conformance-closure.md` | Pre-Wave-5-/Wave-6-Abschluss | archiviert oder abgeschlossen / archived or completed |
| 25 | `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` | Framework-Konformitätsaudit | **nächster Intake / next intake** |
<!-- secure-development-hardening-order:end -->

## Fortsetzungsmarke / Resume Marker

> **NÄCHSTER SPEC-KIT-INTAKE: `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` -> `029-tv203-freevision-terminalgui-conformance-audit`.**
>
> **NEXT SPEC KIT INTAKE: `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` -> `029-tv203-freevision-terminalgui-conformance-audit`.**

Features 025, 026 und 028 sind abgeschlossen. Feature 028 setzt den bestehenden
Gate auf `ReadyForTerminalGuiAudit`. Feature 029 ist der naechste autonome Lauf;
beide Waves bleiben bis zu dessen findings-basiertem Closure blockiert.

*Features 025, 026, and 028 are complete. Feature 028 sets the existing gate to
`ReadyForTerminalGuiAudit`. Feature 029 is next; both Waves remain blocked
through its finding-derived closure.*

## Verbindliche Folge vor Wave 5 und Wave 6 / Binding Sequence Before Wave 5 and Wave 6

1. Feature 024 bleibt als ursprünglicher Auditlauf historisch abgeschlossen.
   Die kombinierte Consumer-Review-Revision 2 hat seine Zukunftsentscheidung
   mit 13 Findings superseded: neun `Core025`, vier `ComponentData026`.
2. `025-core-runtime-conformance-hardening` und
   `026-component-data-conformance-hardening` sind vollständig gemergt und
   schließen die 13 Revision-2-Findings mit realen Red-/Green-Proofs.
3. `028-pre-wave5-wave6-conformance-closure` hat alle 13 Findings, sieben
   Integrations-Slices, 13 Consumer-Gruppen und Pflichtgates unabhaengig
   geschlossen; keine Wave wurde gestartet oder freigegeben.
4. Als naechster Lauf folgt
   `029-tv203-freevision-terminalgui-conformance-audit` mit Lastenheft 13.
5. Feature 029 erzeugt nur aus realen Findings nicht leere Hardening-
   Lastenhefte ab Nummer 030 sowie danach immer einen unabhängigen Closure-
   Lauf.
6. Wave 5 bleibt bis zu diesem Closure-Merge blockiert. Wave 6 folgt erst nach
   Wave 5 und einer erneuten Prüfung der tatsächlichen Wave-5-Deltas.

*Revision 2 routed nine findings to Feature 025 and four to Feature 026; both
are complete. Feature 028 closed that gate, Feature 029 adds the pinned
Terminal.GUI v1.9.0 comparison, and only the final finding-derived closure may
release Wave 5. Wave 6 additionally requires a post-Wave-5 delta review.*
