# Feature-028-Abschlussnachweis / Feature 028 Closure Evidence

## Zweck / Purpose

Dieses Dokument macht den maschinenpruefbaren Datensatz
`closure-evidence.json` fuer Lernende und Reviewer lesbar. Feature 028
revalidiert vorhandene Framework-Vertraege; es veraendert weder Runtime noch
API und gibt Wave 5 oder Wave 6 nicht frei.

*This document presents the machine-verifiable `closure-evidence.json` in a
learner- and reviewer-readable form. Feature 028 revalidates existing framework
contracts; it changes neither runtime nor API and does not release Wave 5 or
Wave 6.*

## Gate-Zustand / Gate State

| Gate | Zustand / State | Grenze / Boundary |
|---|---|---|
| TV203/Free Vision | `ReadyForTerminalGuiAudit` | 13 Findings, 7 Slices, 13 Consumer-Gruppen und alle lokalen Gates sind geschlossen; exact-head Remote-Evidence folgt bei Delivery. |
| Wave 5 | `BlockedPendingTerminalGuiAudit` | Feature 029 und dessen findings-basierter Abschluss bleiben Pflicht. |
| Wave 6 | `BlockedPendingTerminalGuiAudit` | Zusaetzlich bleibt die erneute Delta-Pruefung nach Wave 5 Pflicht. |
| Naechster Intake / Next intake | `029-tv203-freevision-terminalgui-conformance-audit` | Feature 029 wird in 028 weder angelegt noch gestartet. |

## Finding-Abschluss / Finding Closure

| Finding | Contract | Owner | Entscheidung / Decision | Primaerbeweis / Primary proof | Proof-Grenze / Proof limit |
|---|---|---|---|---|---|
| `F001` | `C004` | Feature 025 | `Closed` | `TEvent_CreateMouse_AcceptsConcreteKindsAndRejectsMasksOrComposites` | Beweist konkrete Event-Kinds, nicht native Terminalprotokolle. / Proves concrete event kinds, not native terminal protocols. |
| `F002` | `C008` | Feature 025 | `Closed` | `TGroup_TrySetFocus_VetoAndEligibilityAreAtomic` | Beweist atomaren Fokus-Veto, keine visuelle Neugestaltung. / Proves atomic focus veto, not visual redesign. |
| `F003` | `C009` | Feature 025 | `Closed` | `TGroup_SetState_*` und Insert-Vererbung / and insertion inheritance | Beweist den geschlossenen State-Satz; neue Flags loesen Revalidation aus. |
| `F004` | `C013` | Feature 025 | `Closed` | `TProgram_Run_IdleAndPendingOrdering_IsDeterministic` | Beweist bounded managed idle, keinen nativen Message-Pump. |
| `F005` | `C014` | Feature 025 | `Closed` | `TDesktop_F005_TileAndCascadeStayInsideBoundsForMixedChildren` | Beweist Framework-Stack/Geometrie, keine Wave-Fensterklassen. |
| `F006` | `C015` | Feature 025 | `Closed` | `TDialog_F006_*`; `TWindow_F006_*` | Beweist Close/Modal/Fokus durch reale Owner, keinen nativen Nested Loop. |
| `F007` | `C017` | Feature 025 | `Closed` | `TProgram_CommandContext_RefreshesAllTriggersAndRejectsStaleDispatch` | Beweist gemeinsamen Kontext, keinen App-Command-Katalog. |
| `F008` | `C034` | Feature 025 | `Closed` | `TProgram_GetEvent_UsesCanonicalTranslationForRawConsoleKeys` | Beweist `ConsoleKeyInfo`-Ingress; unbekannte Host-Sequenzen bleiben begrenzt. |
| `F009` | `C036` | Feature 025 | `Closed` | `TWindow_F009_ActualRunPointerAndKeyboardRenderEquivalentResults` | Beweist generische Session und Keyboard-Paritaet, keine destructive Drop-Policy. |
| `F010` | `C019` | Feature 026 | `Closed` | `TDialog_F010_*` | Beweist Completion, Reihenfolge, Veto und Cancel, keine App-Kommandoliste. |
| `F011` | `C021` | Feature 026 | `Closed` | `TInputLine_F011_*`; `TDialog_F011_*` | Beweist Edit/Fokus/Acceptance mit State-Erhalt, kein Pointer-Transfermodell. |
| `F012` | `C023` | Feature 026 | `Closed` | `TFileDialog_F012_*` | Beweist typed metadata decisions in test-owned paths, keine spaetere Datei-I/O. |
| `F013` | `C026` | Feature 026 | `Closed` | `TUiDescriptionRecord_F013_*`; `TResourceFile_F013_*` | Beweist Allowlist und atomare Ablehnung, keine historische Binaerparitaet. |

Alle 13 Beobachtungen, Contract-IDs, Owner, historischen Absichten,
Free-Vision-Relationen und Consumer-Scopes sind unveraendert aus Revision 2
uebernommen. Jede Zeile besitzt genau die Entscheidung `Closed`; eine spaetere
Abweichung setzt das Gate wieder auf `Blocked`.

*All 13 observations and source relations remain unchanged from Revision 2.
Every row has exactly one `Closed` decision; later drift blocks the gate again.*

## Integrations-Slices / Integration Slices

| Slice | Verantwortung / Responsibility | Rolle / Role | Ergebnis / Result | Grenze / Limit |
|---|---|---|---|---|
| `R-028-001` | Raw keyboard -> translator -> event -> command/dispatch | `PrimaryProof` | `Pass` 9/9 | Kein Anspruch auf native Protokollparitaet oder Wave-Portierung. / No native protocol parity or Wave porting claim. |
| `R-028-002` | Fokus -> Validation -> Hierarchie-State -> Broadcast | `PrimaryProof` | `Pass` 10/10 | Keine visuelle Neugestaltung oder Pointer-only-Fokuslogik. |
| `R-028-003` | Pending event -> input -> command refresh -> idle -> shutdown | `PrimaryProof` | `Pass` 5/5 | Kein Background-Thread oder nativer Message-Pump. |
| `R-028-004` | Application -> Desktop -> Close/Modal -> View/Cell/Fokus | `PrimaryProof` | `Pass` 12/12 | Keine Wave-spezifischen Fenster oder nativen Nested Loops. |
| `R-028-005` | Pointer/Keyboard -> DragSession -> Target -> Result/Render | `PrimaryProof` | `Pass` 11/11 | Keine destructive Drop-Policy oder volle Desktop-DnD-Paritaet. |
| `R-028-006` | Dialog command -> child validation -> InputLine -> focus/result | `PrimaryProof` | `Pass` 10/10 | Keine App-Kommandoliste oder historische Validator-1:1-Kopie. |
| `R-028-007` | File outcome / Resource record -> allowlisted factory | `PrimaryProof` | `Pass` 13/13 | Keine beliebige User-I/O, historische Binary-Dekodierung oder Runtime-Aktivierung. |

Die gezielten Aufrufe meldeten Zero-Test-Hinweise nur fuer nicht betroffene
Solution-Projekte; jeder benannte Proof lief mindestens einmal im zustaendigen
Testprojekt. Der separate Integrity-Validator bestand mit 1/1 und bleibt
`SupplementalProof` fuer die Evidence-Beziehungen.

*Zero-test messages applied only to unrelated solution projects; every named
proof executed in its owning test project. The separate integrity validator
passed 1/1 and remains `SupplementalProof` for evidence relationships.*

## Consumer-Bereitschaft / Consumer Readiness

| Consumer | Wave | Entscheidung / Decision | Begruendung / Rationale | Folgegrenze / Follow-up boundary |
|---|---|---|---|---|
| `W5-001` `TVDEMO.PAS` | 5 | `UseExistingFramework` | Der gemeinsame Input-/Command-Pfad ist wiederverwendbar; weitere Vertrage werden in getrennten 028-Zeilen geprueft. / The shared input and command path is reusable; other contracts are checked separately. | Feature 029 und sein Closure bleiben Pflicht. |
| `W5-002` `TVEDIT.PAS` | 5 | `UseExistingFramework` | Desktop-, Close-, Command-, Dialog-, Keyboard- und typed File-Outcome-Vertraege tragen die gemeinsame Editor-Shell. | Wave-spezifische Editor-Komposition und User-File-Policy bleiben offen. |
| `W5-003` `TVRDEMO.PAS` | 5 | `UseExistingFramework` | Exact named lookup und allowlisted UI-Rekonstruktion ersetzen historische Binary-Paritaet. | Nur source-controlled moderne Beschreibungen verwenden. |
| `W5-004` `GENRDEMO.PAS` | 5 | `UseExistingFramework` | Das geschlossene Description-Schema traegt generierte Menues, Status und Dialoge. | Generatoren duerfen Allowlist und Bounds nicht erweitern. |
| `W5-005` `GADGETS.PAS` und verwandte Demos | 5 | `UseExistingFramework` | Bounded Idle und gemeinsamer Command-State decken die Framework-Rolle. | Jeder Gadget-Slice braucht bounded work und sichtbaren Keyboard-Proof. |
| `W5-006` `MOUSEDLG.PAS` | 5 | `UseExistingFramework` | Konkrete Mouse-Events, Capability-Grenze und Keyboard-Paritaet sind vorhanden. | Native Timing-/Button-Policy und Feature 029 bleiben offen. |
| `W6-001` `TVFM.PAS` | 6 | `UseExistingFramework` | Lifecycle, Desktop, Commands, Keyboard und Resources tragen die Shell. | File-Manager-Produktkomposition und destructive policy bleiben offen. |
| `W6-002` `FILEVIEW.PAS` | 6 | `UseExistingFramework` | Bounded Idle, Command Refresh und Drag Negotiation sind wiederverwendbar. | Enumeration, Cancellation und Operation Policy gehoeren in Wave 6. |
| `W6-003` `DRAGDROP.PAS` | 6 | `UseExistingFramework` | Session, Threshold, Capture, Target, Result und Cancel sind shared mechanics. | Item- und destructive-operation policy bleiben Consumer-Verantwortung. |
| `W6-004` `TREEWIN.PAS` | 6 | `UseExistingFramework` | Fokus, Hierarchie, Desktop, Close und Fallback sind geschlossen. | Tree-Modell und Refresh duerfen Ownership nicht umgehen. |
| `W6-005` `GLOBALS.PAS` | 6 | `UseExistingFramework` | Dialog-, Validator- und typed File-Outcome-Vertraege tragen Rename/Attribute UI. | Mutation, Konflikt, Rollback und Autorisierung bleiben Produktpolitik. |
| `W6-006` `COLORS.PAS` | 6 | `UseExistingFramework` | Acceptance, Cancel, Rejection und State Preservation sind vorhanden. | Palette und High-Contrast brauchen eigene A11Y-Akzeptanz. |
| `W6-007` `FILECOPY.PAS` / `TRASH.PAS` | 6 | `FollowUpHardening` | Shared mechanics sind vorhanden; destructive policy darf nicht aus Pascal-Code geraten werden. | Vor Aktivierung muessen Confirmation, Conflict, Rollback und Recovery spezifiziert sein. |

*Twelve groups reuse the existing framework. W6-007 remains a deliberate
`FollowUpHardening` boundary because destructive operation policy is product
work, not a missing shared-framework contract.*

## Governance und Validierung / Governance and Validation

| Preset | Version | Applicability | Ergebnis / Result | Wesentliche Grenze / Material boundary |
|---|---:|---|---|---|
| Security | 0.6.0 | `Applicable` | `Pass` | Supply chain, agent secrets und Gitleaks bleiben getrennte Evidence; Produkt-/Regulatory-Trigger sind N/A. |
| Architecture | 0.5.0 | `Applicable` | `Pass` | Contract-Risiko wird geprueft; Cloud, C3A/C5 und Deployment sind N/A. |
| iSAQB Architecture | 0.2.0 | `Applicable` | `Pass` | Quality, Views, Risk und Debt sind traceable; keine neue ADR-Entscheidung. |
| A11Y | 0.4.0 | `Applicable` | `Pass` | Keyboard, Fokus, Rejection, text-first und bilingual; HTML-Gate folgt. |
| Cross-Platform | 0.2.0 | `Applicable` | `Pass` | Drei OS sind Pflicht; WSL und Script-Parity bleiben begruendete N/A. |
| Agent Parity | 0.3.0 | `Applicable` | `Pass` | Fuenf Oberflaechen spaeter gemeinsam; `.specify/templates/` N/A. |
| Autonomous Run | 0.2.0 | `Applicable` | `Pass` | Echter Resume-Nachweis; exact-head, merge und closeout bleiben terminal. |

| Gate | Pre-remote mapping | Status before exact-head delivery |
|---|---|---|
| Linux runtime | `CI` / `build-test` / `ubuntu-latest` / restore-build-test-DocFX | Mapped, exact-head pending |
| macOS runtime | `CI` / `build-test` / `macos-latest` / identical command body | Mapped, exact-head pending |
| Windows runtime | `CI` / `build-test` / `windows-latest` / identical Bash body | Mapped, exact-head pending |
| DocFX/A11Y | `DocFX Pages` / `build` / Ubuntu / DocFX, `npm ci`, `npm test` | Mapped, exact-head pending |
| Homogeneity | `Homogeneity Check` / three OS / secret and rename contracts | Mapped, exact-head pending |
| Supply chain | `Security Supply Chain` / package and SBOM evidence | Mapped, exact-head pending |
| Agent secrets | `Agent Secret Scan` / independent Ubuntu job | Mapped, exact-head pending |
| Gitleaks | `Gitleaks` / independent Ubuntu action | Mapped, exact-head pending |
| WSL | No accepted runner or reproducible command | `N/A`; re-evaluate when one exists |

Der lokale Gate-Evidence-Validator wurde vor Remote Delivery synthetisch positiv
und mit stale head negativ geprueft. Das ist nur Validator-Proof und ersetzt
keine Provider-Ausfuehrung. Der TV203/Free-Vision-Gate ist nach vollstaendiger
lokaler Abnahme `ReadyForTerminalGuiAudit`; der autonome Run bleibt bis zur
Delivery `Active`, beide Waves bleiben blockiert, und Feature 029 bleibt der
einzige naechste Intake.

*Synthetic positive and stale-head checks prove only validator behavior. They
do not replace provider execution. The TV203/Free Vision gate is locally ready
for the Terminal.GUI audit, while the autonomous run and both Waves remain
blocked until their respective delivery and audit boundaries complete.*
