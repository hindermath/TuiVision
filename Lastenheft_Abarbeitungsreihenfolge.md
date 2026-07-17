# Lastenheft-Abarbeitungsreihenfolge / Requirements Processing Order

Diese Datei haelt die sichtbare Abarbeitungsreihenfolge der vorhandenen Lastenhefte fest. Sie ist eine Vorbereitung fuer spaetere Spec-Kit-Laeufe und startet selbst keinen Lauf.

*This file records the visible processing order of existing requirements documents. It prepares later Spec Kit runs and does not start a run by itself.*

## Spec-Kit-Intake-Regel / Spec Kit Intake Rule

- Diese Datei ist ein Ordnungsdokument und selbst kein Spec-Kit-Intake.
- Aktive Lastenhefte ohne Feature-Branch-Suffix koennen als Intake dienen, wenn sie Scope, Nicht-Ziele, Anforderungen, Akzeptanzkriterien sowie kopierbare `$speckit-specify`- und `$speckit-autonomous`-Prompts enthalten.
- Lastenhefte mit Feature-Branch-Suffix wie `.001-*` oder `.009-*` gelten als historisch oder abgeschlossen und werden nicht erneut gestartet.
- Vor jedem neuen Lauf wird zuerst der aktuelle Repository-Stand geprueft; erledigte Punkte werden als `AlreadySatisfied` oder `N/A` dokumentiert, nicht neu implementiert.

- This file is an ordering document and not itself a Spec Kit intake.
- Active Lastenhefte without a feature-branch suffix can be used as intake when they include scope, non-goals, requirements, acceptance criteria, and copyable `$speckit-specify` and `$speckit-autonomous` prompts.
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
| 25 | `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.029-tv203-freevision-terminalgui-conformance-audit.md` | Framework-Konformitätsaudit | archiviert oder abgeschlossen / archived or completed |
| 26 | `Lastenheft_14_TV203-Magiblot-Evolution-Audit.030-tv203-magiblot-evolution-audit.md` | Framework-Evolutionsaudit | archiviert oder abgeschlossen / archived or completed |
| 27 | `Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.031-combined-conformance-closure.md` | Gemeinsamer Pre-Wave-5-/Wave-6-Abschluss | archiviert oder abgeschlossen / archived or completed |
| 28 | `Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md` | Wave-5 TP7 Functional Porting | archiviert oder abgeschlossen / archived or completed |
| 29 | `Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md` | Wave-5 TP7 Showcase Remediation | archiviert oder abgeschlossen / archived or completed |
| 30 | `Lastenheft_19_Wave5-Combined-Delta-Closure.034-wave5-combined-delta-closure.md` | Wave-5 Combined Delta Closure | archiviert im aktiven Feature 034 / archived in active Feature 034 |
| 31 | `Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` | Beispielportfolio-Konformitätsaudit | verbindlich nach Wave-6-Closeout, Feature-Nummer später / binding after Wave-6 closeout, feature number deferred |
<!-- secure-development-hardening-order:end -->

## Fortsetzungsmarke / Resume Marker

> **AKTIVER FACHLICHER SCHRITT: Feature 034 prüft den tatsächlichen kombinierten Wave-5-Delta read-only; die lokale Matrix ist sauber, Wave 6 bleibt bis zum Remote-, Review-, Merge- und kausalen Abschluss blockiert.**
>
> **ACTIVE DOMAIN STEP: Feature 034 reviews the actual combined Wave-5 delta read-only; the local matrix is clean, and Wave 6 remains blocked until remote, review, merge, and causal closure complete.**

Features 025, 026, 028, 029, 030 und 031 sind abgeschlossen. Der unabhängige
Closure bestätigt 48 Verträge, 13 Consumer-Gruppen, 96 vollständige
Nicht-Finding-Dispositionen, null `CF*`-Findings und null Hardening-Intakes.
Wave-5-Stage 1 ist als Feature 032 vollständig geliefert. Feature 033 ergänzt
die zweite Stufe mit zehn sichtbaren Hauptkomponenten, echten Statuszeilen,
per Tastatur erreichbaren Beschreibungen, begrenzten Layout-Proofs und
synchronisierten Guides. Feature 033 ist vollständig gemergt. Der explizite
Auftrag führt Feature 034 als read-only Combined-Delta-Closure aus
Lastenheft 19 aus. Die lokale 15/6/10/10/10-Matrix enthält null Findings und
null Product Decisions. Wave 6 bleibt bis zum vollständigen belegten Abschluss
`ConditionallyReady` und blockiert.

*Features 025, 026, 028, 029, 030, and 031 are complete. The independent
closure confirms 48 contracts, 13 consumer groups, 96 complete non-finding
dispositions, zero CF findings, and zero hardening intakes. Wave 5 is
eligible. Feature 032 supplies Stage 1. Feature 033 adds the complete Stage 2
with ten visible main components, real status lines,
keyboard-reachable descriptions, constrained-layout proof, and synchronized
guides. Feature 033 is fully merged. The explicit instruction reserves
Feature 034 is active for the read-only combined-delta closure from Lastenheft
19. The local 15/6/10/10/10 matrix contains zero findings and zero product
decisions. Wave 6 remains conditionally ready and blocked until that closure
is fully evidenced.*

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
4. `029-tv203-freevision-terminalgui-conformance-audit` hat alle 48 Verträge
   gegen Terminal.GUI v1.9.0 geprüft und 48 Beobachtungen ohne neues
   Candidate Finding an Feature 030 übergeben.
5. `030-tv203-magiblot-evolution-audit` hat alle 48 Verträge gegen 50
   magiblot-Quellen geprüft und 96 TG-/MB-Beobachtungen ohne `CF*`-Finding
   dedupliziert. Daher entstehen keine Hardening-Läufe.
6. `031-combined-conformance-closure` mit Lastenheft 16 ist vollständig
   gemergt. Sein kausaler Closeout setzt Wave 5 auf `Eligible` und Wave 6 nur
   auf `ConditionallyReady`.
7. `Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md` ist als Feature 032
   funktional geliefert und archiviert. Seine zehn konkreten Delta-Zeilen
   erzeugen `Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md`.
8. Feature 033 liefert die zweite Wave-5-Stufe aus Lastenheft 18 und ist über
   PR #96 mit Merge `d476e63` vollständig abgeschlossen.
9. `Lastenheft_19_Wave5-Combined-Delta-Closure.md` reserviert Feature 034 als
   read-only Audit des tatsächlichen kombinierten Produktdeltas aus PR #93
   und PR #96. Der Lauf behebt keine Produktfindings und startet Wave 6 nicht.
10. Wave 6 folgt erst nach einem belegten Feature-034-Abschluss. Bei null
    Findings darf der Lauf einen späteren Feature-035-Intake ableiten; bei
    Findings entstehen nur nicht leere, ownerbezogene Hardening-Intakes.
11. Nach dem vollständig gemergten Wave-6-Closeout folgt das in Lastenheft 15
   vorgemerkte Example-Portfolio-Audit. Seine Feature-Nummer wird erst dann
   aus der nächsten freien Nummer gebildet; es erzeugt nur aus realen Findings
   nicht leere Remediation-Lastenhefte und danach einen unabhängigen Closure.

*Revision 2 routed nine findings to Feature 025 and four to Feature 026; both
are complete. Feature 028 closed that gate, Feature 029 completed the pinned
Terminal.GUI comparison, and Feature 030 completed the magiblot comparison
with zero canonical findings. Feature 031 is complete. Feature 032 delivers
the functional Wave-5 stage and its real-path proof. Feature 033 completes
and merges the evidence-derived showcase stage. Wave 6 remains blocked until
the read-only Feature-034 audit has evidenced the actual combined delta.*

## Verbindlicher Folgeaudit nach Wave 6 / Binding Post-Wave-6 Audit

`Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` ist bereits
als späterer Intake festgelegt, wird aber weder nummeriert noch gestartet,
solange Wave 5 und Wave 6 mit ihren Closeouts
nicht vollständig gemergt sind.

Der spätere Audit prüft alle 25 Originalbeispiele, alle tatsächlich gelieferten
Wave-5-/Wave-6-Beispiele und `A11yFramework` als projektspezifische
Vergleichskontrolle. Der nächste fachliche Schritt ist Feature 034 aus
Lastenheft 19. Der Post-Wave-6-Audit bleibt davon getrennt.

*Lastenheft 15 reserves one read-only portfolio audit after the complete
Wave-6 closeout. It does not assign a feature number or start a run. The
current next step is Feature 034 from Lastenheft 19. The post-Wave-6 audit
remains separate.*
