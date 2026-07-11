# Feature Evidence: Didactic Inline Code Comment Hardening

**Feature / Feature**: `015-didactic-comment-hardening`  
**Datum / Date**: 2026-07-11  
**Owner**: Thorsten Hindermann  
**Reviewer**: Codex  
**Binding input**: `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md`

## Umfangsgrenze / Scope Guard

Deutsch: Dieser Lauf härtet ausschließlich didaktische Inline-, Block-, Datei-
oder Modulkommentare sowie die zugehörige Evidence. Er ändert kein
Runtime-Verhalten, keine öffentliche API, keine Abhängigkeit, keine
Beispielportierung und keine Framework-Struktur.

English: This run hardens only didactic inline, block, file, or module comments
and their evidence. It changes no runtime behavior, public API, dependency,
example port, or framework structure.

| Prüfung / Check | Ergebnis / Result | Evidence |
|---|---|---|
| Branch | `015-didactic-comment-hardening` | `git status --short --branch -uall` |
| Governance baseline | Commit `bedb7fa` vorhanden / present | `git merge-base --is-ancestor bedb7fa HEAD` |
| Feature path | Unverändert / unchanged | `.specify/feature.json` |
| Reihenfolge / ordering | Nach 014, vor Wave-1 Visual / after 014, before Wave-1 Visual | `spec.md` FR-001 |
| Runtime/API/Dependencies | Keine Änderung geplant / no change planned | `spec.md` FR-002/FR-003 |
| Out of scope | Wave-1 Visual, Wave 3/4, neue Beispiele, breite Revision | `spec.md` FR-021 |
| Generated output | `_site/`, `api/*.yml`, Caches, Logs und Testergebnisse bleiben untracked | `.gitignore`, finaler Git-Status |

## Evidence-Schema / Evidence Schema

Die Kommentarentscheidung und die Governance-Anwendbarkeit sind getrennte
Modelle. `Applicable`, `N/A` und `Open` sind keine Kommentarentscheidungen.

The comment decision and governance applicability are separate models.
`Applicable`, `N/A`, and `Open` are not comment decisions.

### Review-Bereiche / Review Areas

| AreaId | PathOrFlow | HotspotCategory | Decision | Rationale | CommentNeed | CommentState | ChangeSummary | ValidationOrProofBoundary | FollowUpBoundary | GovernanceTrigger |
|---|---|---|---|---|---|---|---|---|---|---|
| RA-001 | `TApplication` shell layout | EventCommandDispatch | UpdateExistingComment | Randzeilen und initialer Fokus sind Shell-Randbedingungen, keine nummerierten Aufbauschritte. | Warum Arbeitsbereich und Fokus so gewählt sind. | Changed | Triviale Schrittlabels durch zwei Warum-Blöcke ersetzt. | Controls-Tests prüfen unveränderte Bounds und Shell-Ereignisse. | None | A11Y didactic comments |
| RA-002 | `TGroup.HandleEvent`, `TProgram`, `TMenuBar`, `TMenuItem`, `ShellCommandIds` | EventCommandDispatch | UpdateExistingComment | Die Abbruchsemantik eines geleerten Ereignisses erklärt die Dispatch-Reihenfolge besser als Phasenlabels. | Priorität und Verbrauchsgrenze. | Changed | Phasenlabels entfernt, Dispatch-Rationale ergänzt; übrige Dateien unverändert. | Controls-Tests prüfen Pre-, Fokus- und Post-Dispatch; keine vollständige Eingabegeräte-Parität. | None | NIST SSDF, CWE Top 25 |
| RA-003 | `TGroup.SetFocus` no-op label | FocusTransition | UpdateExistingComment | Die Bedingung ist durch Bezeichner und Rückgabe selbsterklärend; der Kommentar wiederholte nur den Code. | Kein didaktischer Mehrwert. | Removed | Trivialen No-op-Kommentar entfernt. | Controls-Fokustests bleiben Verhaltensevidence. | None | A11Y didactic comments |
| RA-004 | `TGroup.SelectNext` circular focus scan | FocusTransition | CommentNeeded | Die zirkuläre Liste macht die feste Iterationsgrenze zu einer Sicherheits- und Terminierungsrandbedingung. | Warum die Kindanzahl die Suche begrenzt. | Changed | Zweizeiligen Warum-Block ergänzt. | Controls-Tests prüfen vorwärts/rückwärts und ungeeignete Views. | None | CWE Top 25 |
| RA-005 | `TDialog.HandleEvent` with `TView`, `TProgram`, `TWindow` | FocusTransition | CommentNeeded | Der Basishandler darf das Ereignis leeren; lokale Kopien bewahren Dialogtasten und Außenklick-Prüfung. | Warum Originaldaten vor Dispatch gesichert werden. | Changed | Zweizeiligen Ereignisgrenzen-Block ergänzt. | Controls-Dialogtests prüfen Tab, Escape, Enter und Außenklick; kein neues Verhalten. | None | A11Y keyboard path |
| RA-006 | `TGroup`, `TDesktop`, `TWindow`, `FramedHostView`, `TScrollGroup`, `TScroller` hierarchy | ViewHierarchy | CommentAdequate | Vorhandene XML- und Inline-Kommentare erklären Ownership, zirkuläre Liste, Z-Reihenfolge und Buffer-Compositing. | Keine weitere Erklärung nötig. | Unchanged | Keine Änderung; Kommentarverdopplung vermieden. | Controls-Hierarchie- und Render-Tests; keine Pixelparität. | None | Architecture context |
| RA-007 | `TStatusLine`, `TStatusDef`, `TStatusItem`, `TProgram` status selection | StatusLine | CommentAdequate | Vorhandene Dokumentation erklärt First-Match-Auswahl, Fallback und Command-Verknüpfung. | Bestehende Erklärung ist ausreichend. | Unchanged | Keine Änderung. | Controls-StatusLine-Tests prüfen sichtbare Auswahl und Commands. | None | A11Y visible feedback |
| RA-008 | `THelpViewer`, `THelpWindow`, dialog description types, `THelpFile`, `THelpIndex`, `THelpTopic` | HelpDescription | CommentNeeded | History muss den sichtbaren Ausgangskontext vor möglicher Fallback-Auflösung sichern. | Warum Push vor Zielauflösung erfolgt. | Changed | Kommentar in `THelpViewer` ergänzt; übrige Help-/Description-Dokumentation ausreichend. | Controls- und Serialization-Tests prüfen Kontext, Fallback und Querverweise. | None | A11Y learner-facing flow |
| RA-009 | `TDialog`, `TStandardDialogFlowState`, `TColorDialog`, `TFileDialog`, `TEditWindow`, `TFileEditor` lifecycle | DialogState | CommentAdequate | Vorhandene XML- und Code-Kommentare grenzen Modalresultat, Wiederherstellung, Safe-Close und Overwrite klar ab. | Keine zusätzliche Inline-Prosa. | Unchanged | Keine Änderung außerhalb RA-005. | Controls-Tests prüfen Annahme, Ablehnung und Zustandsrestauration. | None | A11Y, CWE Top 25 |
| RA-010 | Validators, input lines and description validation | ValidationRejection | NoCommentNeeded | Guard-Namen, Fehlerrückgaben und vorhandene XML-Dokumentation machen die Rejection-Pfade direkt lesbar. | Zusätzliche Kommentare würden Assertions und Bedingungen wiederholen. | Unchanged | Bewusst keine Änderung. | Controls-Validierungs- und Dialogtests prüfen sichere Ablehnung. | None | CWE Top 25 |
| RA-011 | `TConsoleBuffer.WriteText`, `TConsoleCell`, `TRect`, group/program buffers | BufferCellProof | CommentNeeded | Linkes Clipping muss Quelle und Ziel gemeinsam verschieben, sonst wäre sichtbarer Text semantisch versetzt. | Warum beide Offsets gekoppelt sind. | Changed | Zweizeiligen Clipping-Block ergänzt. | Core- und Controls-Tests prüfen Zellinhalt und Grenzen; beweist keine Terminaldarstellung. | None | A11Y text-first proof |
| RA-012 | `TIndicator` and stable rendered regions | RenderingSnapshot | NoCommentNeeded | Zeichenpositionen und bestehende Render-Dokumentation sind selbsterklärend; die Proof-Grenze gehört in Test-Helfer und Evidence. | Kein Produktionskommentar nötig. | Unchanged | Bewusst keine Änderung. | Render-Snapshots beweisen stabile Zellen/Regionen, nicht vollständige visuelle Parität. | None | A11Y proof boundary |
| RA-013 | `TRecordSerializer`, registry, resources, `pstream`, `ipstream`, `opstream` | ValidationRejection | CommentNeeded | Aktive und abgeschlossene Referenzen unterscheiden Zyklus-Ablehnung von Shared-Reference-Erhaltung. | Warum dieselbe ID-Tabelle beide Fälle trennt. | Changed | Zweizeiligen Referenzgraph-Block in `opstream` ergänzt. | Serialization-Tests prüfen Truncation, Restdaten, unbekannte Typen, Shared References und Zyklen. | None | NIST SSDF, CWE Top 25 |
| RA-014 | `TConsoleDriver`, `SystemConsolePresenter`, `DriverCapabilityMap` | TerminalFallback | UpdateExistingComment | Das Auslassen der letzten Zelle ist eine Terminal-Randbedingung und kein allgemeiner Renderfehler. | Warum Geometrie vor Vollständigkeit der letzten Zelle priorisiert wird. | Changed | Knappe Autoscroll-Erklärung zweisprachig präzisiert. | Drivers-Tests prüfen Buffer-Snapshots; echte Terminalvarianten bleiben Plattform-Evidence. | None | Cross-platform context |
| RA-015 | `TConsoleInputAdapter` managed xterm subset | TerminalFallback | CommentAdequate | Die vorhandene Klassen- und Methodendokumentation benennt unterstützte Teilmenge und historischen Ersatz klar. | Keine zusätzliche Inline-Erklärung. | Unchanged | Keine Änderung. | Compatibility-Tests prüfen Übersetzung, nicht sämtliche Terminalprotokolle. | None | Cross-platform context |
| RA-016 | `tv203s` program/group/dialog/help/stream/validator sources versus modern flows | HistoricalTurboVisionDeviation | CommentNeeded | Historische Dispatch-, Help- und Stream-Absicht erklärt moderne managed Randbedingungen; die Portierung bleibt idiomatisch. | Moderne Abweichung statt mechanischer Zeilenparität erklären. | Changed | Relevante moderne Kommentare in RA-002, RA-008, RA-013 und RA-014; `tv203s/` unverändert. | Read-only Vergleich mit `.cc` und nötigen Headern; keine Paritätsbehauptung über nicht geprüfte Pfade. | None | Architecture and iSAQB context |
| RA-017 | `ExampleTestBase` rendered-region proof | SmokeTestHelper | CommentNeeded | Die Clipping-Regel stabilisiert kleine Terminals, begrenzt den Beweis aber auf den sichtbaren Schnittbereich. | Proof purpose, stability reason and proof limit. | Changed | Zweizeiligen Proof-Grenzen-Block ergänzt; helper role `PrimaryProof` assertion support. | `TuiVision.Examples.SmokeTests`; kein vollständiger Screenshot-Vergleich. | None | A11Y text-first proof |
| RA-018 | `InteractiveSmokeEventScript` command/key sequence | SmokeTestHelper | CommentAdequate | Vorhandene Dokumentation erklärt Queue-Eigentum und bewusst externen Quit-Pfad vollständig. | Bestehende Proof-Grenze ist ausreichend. | Unchanged | Keine Änderung; helper role `SetupOnly` event injection. | Example smokes prüfen Reihenfolge durch echten App-Loop; Script allein beweist kein Verhalten. | None | A11Y keyboard path |
| RA-019 | Wave-1/Wave-2 `*SmokeTests.cs` and interactive matrix | SmokeTestHelper | CommentAdequate | Testnamen, Klassifikation und App-Loop-/View-/Buffer-Assertions trennen primäre und ergänzende Evidence bereits klar. | Keine flächige Testkommentierung. | Unchanged | Keine Änderung; helper roles vary per explicit `DirectHelperUsage`. | Example-smoke-Projekt; einzelne Assertions beweisen nur ihren benannten Zustand. | None | A11Y proof boundary |
| RA-020 | Controls buffer, context, event, presenter, shell and widget helpers | SmokeTestHelper | CommentNeeded | Ein geklonter interner Puffer friert den Beweiszeitpunkt ein, ist aber kein vollständiger visueller Vergleich. | Purpose, stability and snapshot limit. | Changed | Kommentar in `ControlTestContext` ergänzt; übrige Helper-Dokumentation ausreichend; role `SupplementalProof`. | Controls-Tests prüfen Zellen, Regionen und Shell-Zustand. | None | A11Y proof boundary |
| RA-021 | Standard-dialog, designer-flow and persisted-description test support | SmokeTestHelper | NoCommentNeeded | Methodennamen und konkrete Assertions zeigen Validierung, Ablehnung und Persistenzgrenze ohne verborgene Helper-Logik. | Inline-Kommentar würde Testcode wiederholen. | Unchanged | Bewusst keine Änderung; roles `SetupOnly` and assertion support. | Controls- und Serialization-Tests; keine allgemeine Dateiformat-Kompatibilität. | None | NIST SSDF |
| RA-022 | Serialization graph support, `PStreamTests`, coverage sweep | SmokeTestHelper | CommentNeeded | Paar und Knoten müssen Shared Reference und Zyklus-Ablehnung getrennt beweisen. | Stable test shape and malformed-payload boundary. | Changed | Kommentar in `SerializationTestSupport` ergänzt; role `SetupOnly`. | Serialization-Tests prüfen explizite Fehler, nicht beliebige Objektgraphen. | None | CWE Top 25 |
| RA-023 | Phase-7 driver context, baseline and consolidation tests | SmokeTestHelper | UpdateExistingComment | Nur konkrete Treiberpfade sind als geplante Ziele prüfbar; andere Einträge benötigen eigene Begründung. | Ledger-Stabilität und historische Proof-Grenze. | Changed | Zwei triviale englische Zeilen durch zweisprachigen Warum-Block ersetzt; role `SupplementalProof`. | Drivers-Tests prüfen Ledger und Snapshots; Plattformbetrieb bleibt separate Evidence. | None | Cross-platform, architecture context |
| RA-024 | `rename-lastenheft.sh` and `rename-lastenheft.ps1` transaction boundary | SmokeTestHelper | FollowUpHardening | Beide Plattformskripte koppeln das Archivieren zwingend an `git commit`; das kollidiert mit einem ausdrücklich commit-freien Implement-Lauf. | Eine spätere Script-Härtung braucht einen Rename-only- oder No-Commit-Modus mit Bash/Pwsh-Parität. | NotApplicable | Keine Script-Änderung in 015; äquivalentes `git mv` ohne Commit ausgeführt. | Datei ist archiviert, aber die Skripte wurden nur gelesen und nicht ausgeführt, weil sie sonst die Remote-/Commit-Grenze verletzt hätten. | Workitem `Lastenheft_Secure-Development-Hardening.md`; Owner Thorsten Hindermann; prüfen, sobald Rename-Workflow oder Script-Governance bearbeitet wird. | Cross-platform re-evaluation trigger |

US1 und US2 ändern ausschließlich Kommentare und Evidence. Es wurden keine
ausführbaren Quell- oder Testzeilen geändert und keine neuen Verhaltenstests
angelegt. Ein künftig entdeckter Runtime-, Design-, Paritäts- oder Proof-Defekt
wird als `FollowUpHardening` aus 015 herausgeführt.

US1 and US2 change comments and evidence only. No executable source or test
lines were changed, and no new behaviour tests were added. Any later runtime,
design, parity, or proof defect is routed out of 015 as `FollowUpHardening`.

Ausgewählte gezielte Validierung / Selected targeted validation:
`TuiVision.Core.Tests`, `TuiVision.Controls.Tests`,
`TuiVision.Serialization.Tests`, `TuiVision.Drivers.Tests` und
`TuiVision.Examples.SmokeTests`. `TuiVision.Compatibility.Tests` bleibt
unverändert und wird deshalb nicht als touched-module Test ausgelöst.

### Governance-Evidence / Governance Evidence

| RunId | PresetName | PresetVersion | Checkpoint | Applicability | Rationale | EvidencePath | Owner | Reviewer | ReviewDate | Result | ResidualRisk | FollowUp | ReevaluationTrigger |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 015 | security-governance | 0.6.0 | Preset and C#/.NET secure-coding context | Applicable | C# remains the memory-safe implementation language; this run reviews comments near non-trivial logic without changing executable code. | `spec.md` CR-005, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Low: comments could misdescribe code; final diff and targeted tests bound this risk. | None | Re-evaluate if executable logic or language constraints change. |
| 015 | security-governance | 0.6.0 | NIST SSDF | Applicable | Level-2 secure-development context remains binding. | `spec.md` CR-006, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Low: no security control changes. | None | Re-evaluate if implementation changes security-relevant logic or release evidence. |
| 015 | security-governance | 0.6.0 | CWE Top 25 | Applicable | Review context applies, while comment-only edits introduce no weakness mitigation change. | `spec.md` CR-006, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Low: wording must not conceal existing guards. | None | Re-evaluate if input handling, authorization, file or network I/O changes. |
| 015 | security-governance | 0.6.0 | OWASP ASVS | N/A | No web, API, HTTP, authentication, or authorization-bearing service changes. | `spec.md` CR-007, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | None within feature scope. | None | Re-evaluate when web/API/auth scope enters the feature. |
| 015 | security-governance | 0.6.0 | SBOM | N/A | No dependency, distributable component, or release artifact changes. | `spec.md` CR-008, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing release SBOM posture remains unchanged. | None | Re-evaluate when dependencies or release artifacts change. |
| 015 | security-governance | 0.6.0 | VEX | N/A | No shipped vulnerability status or affected component changes. | `spec.md` CR-008, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing vulnerability handling remains unchanged. | None | Re-evaluate when a shipped component or known vulnerability is affected. |
| 015 | security-governance | 0.6.0 | SLSA | N/A | Build provenance and publication flow are unchanged. | `spec.md` CR-008, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing provenance posture remains unchanged. | None | Re-evaluate when CI, build provenance, or publication changes. |
| 015 | security-governance | 0.6.0 | OpenSSF Scorecard | N/A | Public OSS risk posture and repository controls are unchanged. | `spec.md` CR-008, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing repository posture remains unchanged. | None | Re-evaluate when public repository controls or release posture change. |
| 015 | security-governance | 0.6.0 | AI-SBOM | N/A | AI is development tooling only; no model, dataset, inference service, or product AI is delivered. | `spec.md` CR-009, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | None for delivered runtime. | None | Re-evaluate when runtime/product AI or delivered AI assets enter scope. |
| 015 | security-governance | 0.6.0 | NIS2 | N/A | No regulated operation, essential-service flow, or vulnerability process changes. | `spec.md` CR-010, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Regulatory posture unchanged. | None | Re-evaluate when regulated operations or customer obligations change. |
| 015 | security-governance | 0.6.0 | CRA | N/A | No market placement, customer handover, release, or conformity scope changes. | `spec.md` CR-010, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Regulatory posture unchanged. | None | Re-evaluate when distribution or EU market placement changes. |
| 015 | security-governance | 0.6.0 | EU AI Act | N/A | No runtime/product AI or regulated AI system is delivered. | `spec.md` CR-010, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | No AI product risk in this feature. | None | Re-evaluate when product AI enters scope. |
| 015 | security-governance | 0.6.0 | DORA | N/A | No financial-sector ICT dependency or regulated customer flow changes. | `spec.md` CR-010, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Regulatory posture unchanged. | None | Re-evaluate when financial-sector ICT scope changes. |
| 015 | architecture-governance | 0.5.0 | Preset context | Applicable | Architecture applicability is reviewed even though runtime boundaries stay unchanged. | `spec.md` CR-011, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Low: review may discover out-of-scope design debt. | Route genuine debt to `FollowUpHardening`. | Re-evaluate if architecture structure or trust boundaries change. |
| 015 | isaqb-architecture-governance | 0.2.0 | Preset context | Applicable | iSAQB/arc42 review context applies; no new architecture decision is introduced. | `plan.md` Constitution Check, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | No architecture artifact change expected. | None | Re-evaluate if a significant architecture decision is discovered. |
| 015 | architecture-governance | 0.5.0 | STRIDE/CIA/CAPEC | N/A | No trust boundary, data flow, attack path, or runtime surface changes. | `spec.md` CR-011, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing threat model remains unchanged. | None | Re-evaluate when trust boundaries or security-relevant flows change. |
| 015 | architecture-governance | 0.5.0 | S-ADR and arc42 security concepts | N/A | No architecturally significant or security cross-cutting decision changes. | `spec.md` CR-011, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing architecture records remain authoritative. | None | Re-evaluate when architecture/security decisions change. |
| 015 | architecture-governance | 0.5.0 | Zero Trust and SAMM | N/A | No distributed, cloud-near, identity, remote-access, or maturity posture changes. | `spec.md` CR-011, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing posture remains unchanged. | None | Re-evaluate when distributed identity or maturity scope changes. |
| 015 | architecture-governance | 0.5.0 | BSI C3A | N/A | No cloud-service selection, provider dependency, or cloud operation changes. | `spec.md` CR-012, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | No cloud-autonomy impact. | None | Re-evaluate when cloud or managed-service scope changes. |
| 015 | architecture-governance | 0.5.0 | BSI C5 | N/A | No cloud assurance, shared-responsibility, audit, or operational evidence changes. | `spec.md` CR-012, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | No cloud-compliance impact. | None | Re-evaluate when cloud assurance or provider scope changes. |
| 015 | a11y-governance | 0.4.0 | Preset and Markdown accessibility | Applicable | Evidence, statistics, and any guidance changes remain text-first and reviewable with assistive technology. | `spec.md` CR-002/CR-013, T091, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Low: wide tables require semantic headings and concise cells. | Final Markdown review in T091. | Re-evaluate when user-facing documents or UI change. |
| 015 | a11y-governance | 0.4.0 | Didactic inline comments | Applicable | This feature directly reviews learner-facing explanations near non-trivial logic. | `spec.md` FR-014/FR-017, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Risk of comment noise is controlled by the five-value review model. | Final line-budget and language review. | Re-evaluate when comment guidance changes. |
| 015 | a11y-governance | 0.4.0 | DocFX/WCAG generated HTML proof | N/A | Pure `//` and `/* */` changes do not alter generated documentation. | `spec.md` FR-019/FR-020, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | None while XML/API/docs/navigation remain unchanged. | None | Re-evaluate when XML comments, API docs, guides, or navigation change. |
| 015 | cross-platform-governance | 0.2.0 | Preset context | Applicable | Cross-platform applicability is reviewed for terminal fallback comments. | `spec.md` CR-015, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | OK | Comments must not overstate platform proof. | Bound terminal claims in evidence. | Re-evaluate when portability behavior changes. |
| 015 | cross-platform-governance | 0.2.0 | Script parity, man page, Cmdlet, dry-run | N/A | No script-shaped tool is added, changed, or removed. | `spec.md` CR-015, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | Existing scripts remain unchanged. | None | Re-evaluate when a script-shaped tool changes. |
| 015 | agent-parity-governance | 0.3.0 | Preset and maintained agent surfaces | Applicable | All five repository-declared agent surfaces retained the synchronized comment rule and received the same completed-feature/next-step context. | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md` | Thorsten Hindermann | Codex | 2026-07-11 | OK | Low: future drift remains possible and is governed by parity policy. | `update-agent-context.sh` completed for codex, claude, gemini, and copilot. | Re-evaluate when shared guidance or active feature context changes. |
| 015 | agent-parity-governance | 0.3.0 | `.specify/templates/` impact | N/A | This implementation applies existing repository guidance and does not change shared templates. | `spec.md` CR-014, this matrix | Thorsten Hindermann | Codex | 2026-07-11 | N/A | No template drift introduced. | None | Re-evaluate when shared guidance or repository templates change. |

Governance-Zählung / Governance count: `Applicable = 9`, `N/A = 18`,
`Open = 0`.

## Hotspot-Inventar / Hotspot Inventory

| HotspotCategory | CandidatePathOrFlow | LearnerValueAndRisk | PlannedValidation |
|---|---|---|---|
| EventCommandDispatch | `TApplication`, `TProgram`, `TGroup`, menu command routing | Routing order and command ownership are easy to misread. | Controls targeted tests |
| FocusTransition | `TView`, `TGroup`, `TProgram`, `TDialog`, `TWindow` | Selection, activation, and disabled state interact across views. | Controls targeted tests |
| ViewHierarchy | `TGroup`, `TDesktop`, `TWindow`, `FramedHostView`, scroll views | Ownership and Z-order determine visible composition. | Controls targeted tests |
| StatusLine | `TStatusLine`, `TStatusDef`, `TStatusItem`, `TProgram` | Status selection and command linkage affect visible feedback. | Controls targeted tests |
| HelpDescription | Help viewer/window, dialog description, help serialization | Context fallback and cross-reference limits cross modules. | Controls and Serialization targeted tests |
| DialogState | Dialog, standard flow, color/file/editor dialogs | Modal result, restoration, safe-close, and overwrite paths are non-trivial. | Controls targeted tests |
| ValidationRejection | Validators, input lines, description validation | Rejection must preserve safe state without hidden mutation. | Controls targeted tests |
| BufferCellProof | Console buffer/cell, geometry, group/program drawing | Clipping and buffer ownership define what rendered proof means. | Core and Controls targeted tests |
| RenderingSnapshot | Indicator rendering and test snapshot helpers | Stable regions prove visibility, not complete visual parity. | Controls and smoke targeted tests |
| TerminalFallback | Console driver, presenter, capability map, input adapter | Environment capability limits must not be overstated. | Drivers and Compatibility targeted tests |
| HistoricalTurboVisionDeviation | Relevant `tv203s/` source matched to modern flows | Historical intent explains deliberate managed modernization. | Read-only source comparison |
| SmokeTestHelper | Example, control, dialog, serialization, and driver helpers | Setup, supplemental, and primary proof roles must stay distinct. | Matching targeted test projects |

## Validierungsgrenze / Validation Boundary

| Befehl oder Prüfung / Command or check | Scope | Ergebnis / Result | Proof boundary |
|---|---|---|---|
| `git diff --check` | Working tree before tests | Pass, exit 0 | Whitespace only |
| `dotnet format --verify-no-changes` | Solution | Pass, exit 0 | Formatting and analyzers; no runtime assertion |
| `dotnet test tests/TuiVision.Core.Tests/ --configuration Release` | Core comment | Pass, 44/44 | Existing Core behaviour |
| `dotnet test tests/TuiVision.Controls.Tests/ --configuration Release` | Controls comments and helper | Pass, 288/288 | Existing Controls behaviour and helper assertions |
| `dotnet test tests/TuiVision.Serialization.Tests/ --configuration Release` | Serialization comments and helper | Pass, 18/18 | Existing archive/stream behaviour |
| `dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release` | Driver comment and helper | Pass, 37/37 | Managed driver and ledger proof; not every physical terminal |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` | Example proof-helper comment | Pass, 91/91 | Existing in-process example-smoke behaviour |
| Full Release suite | Conditional | `N/A` | No executable shared logic or broad smoke-proof behaviour changed |
| Coverlet coverage gate | Conditional | `N/A` | Same trigger as full Release suite; no coverage-bearing code changed |
| `docfx docfx.json` | Conditional | `N/A` | No XML comment, API signature, generated docs, navigation, or guide changed |
| `tests/web-a11y` | Conditional after DocFX | `N/A` | DocFX trigger did not fire |

Vor den fünf Testbefehlen wurde der manuelle Build-Zähler jeweils genau einmal
erhöht: `1.15.36.56` bis `1.15.36.60`. Der Paket-Check und `dotnet format`
waren keine Build-/Testbefehle und lösten keine Erhöhung aus.

Before each of the five test commands, the manual build counter was incremented
exactly once: `1.15.36.56` through `1.15.36.60`. The package review and
`dotnet format` were not build/test commands and did not trigger an increment.

## Agent-Guidance / Agent Guidance

Die Kommentarregel ist semantisch über alle fünf gepflegten Oberflächen
synchron geblieben. Der Abschlussstatus und der nächste Schritt änderten
jedoch den aktiven Feature-Kontext; deshalb wurden `AGENTS.md`, `CLAUDE.md`,
`GEMINI.md`, `.github/copilot-instructions.md` und
`.github/agents/copilot-instructions.md` gemeinsam aktualisiert. Anschließend
lief `update-agent-context.sh` erfolgreich für `codex`, `claude`, `gemini` und
`copilot`. Es gibt keine beabsichtigte Abweichung.

The comment rule stayed semantically synchronized across all five maintained
surfaces. Completion status and the next step changed active feature context,
so all five surfaces were updated together. Repository-owned
`.specify/templates/` remain unchanged and `N/A` because no shared template
rule changed.

## Kommentarqualität / Comment Quality

| Messwert / Measure | Ergebnis / Result |
|---|---:|
| Review-Bereiche / review areas | 24 |
| `CommentAdequate` | 6 |
| `CommentNeeded` | 9 |
| `NoCommentNeeded` | 3 |
| `UpdateExistingComment` | 5 |
| `FollowUpHardening` | 1 |
| Neue oder aktualisierte didaktische Blöcke / new or updated didactic blocks | 13 |
| Blöcke mit 1 bis 3 Zeilen / blocks with 1 to 3 lines | 13 |
| Längere Blöcke / longer blocks | 0 |
| SC-004-Erfüllung / SC-004 compliance | 100 % |

Alle neuen oder aktualisierten Blöcke bestehen aus einer deutschen und einer
englischen Zeile. Entfernte Kommentare waren reine Schritt-, Phasen- oder
No-op-Wiederholungen. Lizenz-, Generator-, Marker- und Tool-Zeilen blieben
unverändert. US3 ist damit ein Review-Qualitätslauf ohne neue Runtime-Tests.

All new or updated blocks contain one German and one English line. Removed
comments only repeated steps, phases, or no-op code. License, generator,
marker, and tool-owned lines stayed unchanged. US3 is therefore a review
quality pass without new runtime tests.

Der fokussierte C#-Diff enthält ausschließlich Kommentarzeilen. Es wurden
keine ausführbaren Statements, öffentlichen APIs, Projektdateien,
Abhängigkeiten oder Beispielumfänge geändert. Es entstand kein konkreter
Runtime- oder Design-Fund. Der einzige `FollowUpHardening`-Fund betrifft die
commit-gekoppelte Lastenheft-Rename-Automation und ist mit Workitem, Owner,
Out-of-scope-Begründung und Neubewertungstrigger dokumentiert.

## Abhängigkeitsprüfung / Dependency Review

`dotnet list package --outdated` lief am 2026-07-11 erfolgreich. Es meldete
MSTest `4.3.0` und Coverlet Collector `10.0.1` als neuere Versionen gegenüber
den aufgelösten Versionen `4.0.1` und `6.0.4`. Feature 015 übernimmt keine
Updates, weil Abhängigkeitsänderungen ausdrücklich außerhalb des Scopes liegen.
Produktionsprojekte meldeten keine verfügbaren Paketupdates.

`dotnet list package --outdated` completed successfully on 2026-07-11. It
reported MSTest `4.3.0` and Coverlet Collector `10.0.1` as newer than the
resolved versions `4.0.1` and `6.0.4`. Feature 015 accepts no updates because
dependency changes are explicitly out of scope. Production projects reported
no available package updates.

## Abschlussstatus / Final Status

### Abnahmeübersicht / Acceptance Summary

| Punkt / Item | Ergebnis / Result |
|---|---|
| Zweck / purpose | Selektive didaktische Kommentarhärtung ohne Runtime-Änderung abgeschlossen. |
| Betroffene Projekte / touched projects | `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Drivers.Console` sowie vier zugehörige Test-/Smoke-Helfer. |
| Working-Tree-Umfang / working-tree scope | 23 beabsichtigte Statuszeilen: Kommentarcode, Testkommentare, Evidence, Tasks, Version, Statistik, fünf Agent-Oberflächen, Pflichtenheft und Lastenheft-Archivierung. |
| Entscheidungen / decisions | 6 `CommentAdequate`, 9 `CommentNeeded`, 3 `NoCommentNeeded`, 5 `UpdateExistingComment`, 1 `FollowUpHardening`. |
| SC-004 | 13/13 Blöcke mit 1 bis 3 Zeilen, 0 längere Blöcke, 100 %. |
| Governance | 9 `Applicable`, 18 `N/A`, 0 `Open`; alle 27 Zeilen mit Owner, Reviewer, Datum, Evidence, Ergebnis, Restrisiko, Follow-up und Trigger. |
| Validierung / validation | Format und Diff sauber; gezielte Release-Tests 478/478 grün. |
| Conditional checks | Full Suite/Coverage `N/A`; DocFX/Web-A11Y `N/A`, weil kein XML/API-/Generated-Docs-/Navigation-/Guide-Trigger eintrat. |
| Agent parity | Kommentarregel unverändert; Abschlussstatus und nächster Schritt in allen fünf Oberflächen synchronisiert; vier Kontextskripte grün. |
| Statistik / statistics | Snapshot und Ledger auf 2026-07-11 aktualisiert; 015-Artefaktmix und beide Manualbaselines dokumentiert. |
| Konfiguration/API | Nur `Directory.Build.props` auf `1.15.37.60`; keine API-, Projekt-, Paket- oder Runtime-Konfiguration geändert. |
| Archivierung / archive | Lastenheft nach `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.015-didactic-comment-hardening.md` verschoben. |
| Follow-up | Rename-Skripte benötigen später einen commit-freien, Bash/Pwsh-paritätischen Modus; Workitem `Lastenheft_Secure-Development-Hardening.md`. |
| Remote boundary | T102 ist auf ausdrückliche Benutzeranweisung bedingt zurückgestellt: kein Commit, Push oder PR-Update in diesem Lauf. |

`Directory.Build.props` ist für diesen Commit auf `1.15.37.60` ausgerichtet:
Der Commit erhöht den Abstand zu `origin/main` von 36 auf 37, während der
manuelle Build-Zähler ohne weiteren Build oder Test unverändert bei 60 bleibt.

### PR-Beschreibung / PR Description

```markdown
## Umfang

Feature 015 härtet selektiv didaktische Inline-Kommentare in zentralen
Framework- und Smoke-Proof-Flows. Der Diff ändert kein Runtime-Verhalten, keine
öffentliche API, keine Abhängigkeit und keinen Beispielumfang.

## Änderungen

- 24 benannte Review-Bereiche mit genau einer Entscheidung dokumentiert.
- 13 kurze German-first/English-second Kommentarblöcke ergänzt oder verbessert.
- Triviale Schritt-, Phasen- und No-op-Kommentare entfernt.
- Governance-Evidence für alle sechs Presets sowie Agent-Parität aktualisiert.
- Lastenheft archiviert und nächsten Schritt auf Secure Development gesetzt.

## Validierung

- `git diff --check`: grün
- `dotnet format --verify-no-changes`: grün
- Core: 44/44, Controls: 288/288, Serialization: 18/18
- Drivers: 37/37, Example-Smokes: 91/91
- Full Suite/Coverage: nicht ausgelöst, da keine ausführbare Logik geändert wurde
- DocFX/Web-A11Y: nicht ausgelöst, da kein Dokumentations-Trigger eintrat

## Governance und Restrisiko

Governance: 9 `Applicable`, 18 `N/A`, 0 `Open`. Ein
`FollowUpHardening` bleibt für einen commit-freien Bash/Pwsh-Rename-Modus; es
liegt außerhalb des comment-only Scopes von 015.

English summary: Feature 015 adds selective code-near explanations and
audit-ready evidence without changing runtime behaviour, APIs, dependencies,
or example scope. All 478 targeted Release tests pass.
```
