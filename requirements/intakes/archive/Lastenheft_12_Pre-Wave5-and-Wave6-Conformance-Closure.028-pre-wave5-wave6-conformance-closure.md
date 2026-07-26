# Lastenheft 12: Pre-Wave-5 and Wave-6 Conformance Closure

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** `028-pre-wave5-wave6-conformance-closure`

**Verbindliche Reihenfolge:** nach vollständig gemergten Features 025 und 026,
vor Feature `029-tv203-freevision-terminalgui-conformance-audit`, Wave 5 und
Wave 6

**Lieferart:** unabhängiger Evidence-, Integrations- und Release-Gate-Lauf;
keine neue fachliche Runtime-Implementierung

**Folgegrenze:** Ein vollständiger Pass schließt das bisherige TV203-/Free-
Vision-Gate, gibt Wave 5 oder Wave 6 aber noch nicht frei. Der nächste
verbindliche Intake ist Lastenheft 13 für Feature 029.

*Feature 028 runs only after Features 025 and 026 are merged. It is an
independent evidence, integration, and release-gate run without new product
behavior. A pass closes the existing TV203/Free Vision gate but keeps both
waves blocked through the mandatory Terminal.GUI audit and its follow-ups.*

---

## 1. Ausgangslage

Feature 027 hat den ursprünglichen Null-Finding-Auditstand vollständig und
korrekt geschlossen. Die spätere Consumer-Review-Revision 2 hat diese
Zukunftsentscheidung mit 13 Findings superseded, ohne die historische
Ausführung umzuschreiben. Features 025 und 026 müssen diese Findings
implementieren und belegen. Feature 028 prüft anschließend unabhängig, ob die
gemeinsame Frameworkbasis tatsächlich für Wave 5 (`TVDEMOS/`) und Wave 6
(`TVFM/`) bereit ist.

Der Abschluss darf keine Finding-Beobachtung still ändern, keinen schwächeren
Test akzeptieren und keinen Restfehler innerhalb eines Evidence-Features
reparieren. Ein offenes oder regressiertes Finding wird an den verantwortlichen
Implementierungsscope zurückgegeben.

*Feature 027 correctly closed the original zero-finding dataset. Consumer Review
Revision 2 later superseded its forward decision with 13 findings. Feature 028
independently verifies the merged 025 and 026 results and determines whether the
shared framework is ready for Wave 5 and conditionally ready for Wave 6.*

## 2. Verbindliche Eingaben

1. Feature-024 Revision-2 Datensatz, Findings, Matrix, Consumer Review und Gate
2. vollständige gemergte Feature-025-Artefakte, Tests und PR-Evidence
3. vollständige gemergte Feature-026-Artefakte, Tests und PR-Evidence
4. historischer Feature-027-Abschluss als Ausführungs- und
   Non-Regression-Evidence, nicht als aktuelle Gate-Entscheidung
5. aktuelle Produktquellen, Tests, Guides, Agent-Kontexte, Constitution,
   Pflichtenheft, Reihenfolge und Projektstatistik
6. relevante `tv203s/`, `TVDEMOS/`, `TVFM/` und gepinnte Free-Vision-Quellen
   ausschließlich read-only

## 3. Ziele

1. Alle 13 Findings auf dem gemergten Main erneut reproduzieren und ihren
   Abschlussstatus unabhängig bestätigen.
2. Die maschinenprüfbare Auditintegrität einschließlich beidseitiger Relationen
   und exakter Finding-Kardinalität schließen.
3. Die wichtigsten Verträge durch Consumer-nahe, aber nicht portierende
   Integrationsslices prüfen.
4. Vollständige lokale und remote Release-Gates ausführen.
5. Das bisherige TV203-/Free-Vision-Gate bei vollständigem Pass als
   `ReadyForTerminalGuiAudit` schließen.
6. Wave 5 und Wave 6 unabhängig vom Pass weiterhin
   `BlockedPendingTerminalGuiAudit` halten.
7. Lastenheft 13 als eindeutigen nächsten Intake benennen, ohne Feature 029 zu
   starten.

## 4. Scope

### 4.1 Im Scope

- strukturierte Audit- und Closure-Evidence
- Test- und Proof-Härtung nur dann, wenn ein vorhandener Test den bereits
  implementierten Vertrag falsch oder unvollständig misst
- kleine Korrekturen am Auditvalidator, wenn sie ausschließlich Evidence-
  Integrität betreffen
- vollständige Release-, Coverage-, Dokumentations-, A11Y-, Security-,
  Plattform- und Remote-Nachweise
- formelle Wave-5-/Wave-6-Gate-Marker
- Agent-Parität, Lastenheft-Archivierung, Reihenfolge und Projektstatistik

### 4.2 Nicht im Scope

- ein offenes Runtime-, API-, Component-, Data- oder Resource-Finding in 028
  implementieren
- Wave-5- oder Wave-6-Beispiele portieren, erzeugen oder visuell remediieren
- historische oder externe Quellen ändern
- neue Abhängigkeiten oder öffentliche Produktverträge hinzufügen
- Akzeptanzkriterien nachträglich abschwächen
- ein Finding ohne real-path Proof als geschlossen markieren
- Feature 027 löschen oder dessen historische Evidence umschreiben
- einen späteren Wave-Lauf im selben Feature starten
- Terminal.GUI analysieren oder Feature-029-Findings vorwegnehmen

## 5. Finding-Closure-Vertrag

Für jedes Finding `F001` bis `F013` enthält die Closure-Tabelle:

`FindingId`, `ContractId`, `OwnerFeature`, `OriginalObservation`,
`MergedChange`, `RedProof`, `RealPathGreenProof`, `HistoricalIntent`,
`FreeVisionRelation`, `ConsumerScope`, `APIImpact`, `A11YImpact`,
`PlatformImpact`, `ResidualRisk`, `ClosureDecision`, `EvidencePath`,
`Owner`, `Reviewer`, `ReviewDate`, `ReevaluationTrigger`.

Erlaubte `ClosureDecision`-Werte sind:

| Decision | Meaning |
|---|---|
| `Closed` | Original observation no longer reproduces and the real path passes |
| `AlreadySatisfiedWithNewProof` | No product change was needed, but stronger evidence now proves the full boundary |
| `Reopened025` | Core finding remains or regressed; Wave gates fail |
| `Reopened026` | Component/data finding remains or regressed; Wave gates fail |
| `ProductDecision` | Breaking or destructive owner decision is required; autonomous closure stops |

Exactly one decision is required per finding. `Closed` and
`AlreadySatisfiedWithNewProof` require a named real-path test. A comment,
implementation statement, broad suite pass, or injected normalized event alone
is insufficient.

## 6. Verbindliche Integrationsslices

### R-028-001: Event und realer Keyboard-Ingress

Beginne am realen Console-Adapterrand und beweise konkrete Eventart,
Modifier-Semantik, Function Keys, Ctrl+W, Alt-Shortcut, unbekannte Eingabe,
Dispatch und Consumption bis zum sichtbaren Ziel.

### R-028-002: Fokus, Group-State und Validation-Veto

Beweise genau ein fokussiertes aktuelles Kind, zustandsabhängige Propagation,
geordneten Fokuswechsel, ablehnendes Control, erhaltene Eingabe und
Fokusannouncement.

### R-028-003: Idle, Command-State und Shutdown

Beweise, dass Idle nur ohne pending event läuft, sichtbare
Consumer-ähnliche Zustände aktualisieren kann, gemeinsame Command-Freigabe
refreshen lässt, keine Eingabe verdrängt und sauber beendet wird.

### R-028-004: Desktop, Close und Modalität

Beweise Insert, Top/Next, Tile, Cascade, Close-All, Safe-Close-Veto,
Ctrl+W/Escape, modales Ergebnis, Event-Isolation und Fokusrestauration über die
reale Anwendungsschleife sowie View-Tree und Buffer/Cell.

### R-028-005: Generic Drag und Tastaturalternative

Beweise Capture, Schwelle, Bounds, Drop, Cancel, Owner-Loss und eine
gleichwertige Tastaturaktion. Der Slice verwendet eine kleine Test-View und
portiert keine TVFM-Anwendung.

### R-028-006: Dialog, Validator und Rejection

Beweise, dass nur Completion-Commands schließen, Kindvalidierung den
Produktionspfad erreicht, Fokus und Daten bei Ablehnung erhalten bleiben und
Cancel seine dokumentierte Grenze einhält.

### R-028-007: Datei- und Resource-Grenze

Beweise alle File-Dialog-Modi mit Temp-Fixtures sowie UI-Resource-Roundtrip,
Keys, Versionen, unbekannte Typen, truncation, trailing data und
kein-partial-state Ablehnung.

## 7. Wave-5-/Wave-6-Readiness-Matrix

Jede relevante Consumer-Datei oder benannte Flow-Gruppe erhält genau eine
Entscheidung:

| Decision | Meaning |
|---|---|
| `UseExistingFramework` | Der Consumer kann den gemergten Frameworkvertrag direkt nutzen |
| `SmallFrameworkFix` | Ein kleiner unbeabsichtigter Rest wäre nötig; Gate bleibt blockiert und wird an 025/026 zurückgegeben |
| `IntentionalDeviation` | Anwendung bleibt bewusst anders, mit Nutzer- und Proof-Begründung |
| `FollowUpHardening` | Nicht gate-kritische spätere Arbeit mit klarer Grenze |
| `ProductDecision` | Destruktive oder breaking Entscheidung benötigt Owner; Gate blockiert |

Für Wave 5 müssen alle shared-framework-relevanten Zeilen
`UseExistingFramework` oder akzeptierte `IntentionalDeviation` sein. Wave-6-
Dateioperationen dürfen `FollowUpHardening` oder `ProductDecision` bleiben,
sofern sie keine gemeinsame Framework-Lücke verdecken.

## 8. Vollständige Validierung

Feature 028 muss mindestens ausführen und protokollieren:

1. `specify check`, Voraussetzungen, vollständige Checklists und wiederholtes
   Analyze bis zur Konvergenz
2. `git diff --check` und `dotnet format --verify-no-changes`
3. alle targeted Tests für die 13 Finding-Verträge
4. vollständige Release-Tests
5. kanonisches Coverlet-Gate mit mindestens 70 % je verpflichtender Assembly
6. `xmllint` für `coverlet.runsettings`, soweit verfügbar
7. DocFX, Playwright/Axe und text-first Lynx-Review, weil die Features 025 und
   026 voraussichtlich XML/API/Guides berühren
8. Secret-, Dependency-, generated-output- und protected-source Scans
9. macOS-, Linux- und relevante Windows/WSL-Remote-Gates für Input, Pfade und
   Terminalverhalten
10. GraphQL-Review-Threads, Claude/Copilot-Verfügbarkeit und Pflichtchecks bis
    zur Konvergenz

Vor jedem Build oder Test gilt die Repository-Build-Counter-Regel. Coverage,
DocFX oder A11Y werden nicht aufgrund eines früheren Features übernommen,
sondern auf dem finalen 028-Stand erneut ausgeführt.

## 9. Governance und Evidence

- Jede aktuelle Preset-Schicht wird mit Version, Applicability, Rationale,
  Evidence, Owner, Reviewer, Datum, Result, Residual Risk, Follow-up und
  Reevaluation Trigger erfasst.
- Security- und Architecture-`N/A` bleiben nur bestehen, wenn der reale Diff
  keine neue Trust-, Cloud-, Auth-, Supply-Chain-, Produkt-AI-, Daten- oder
  Deployment-Grenze auslöst.
- A11Y ist für Fokus, Tastatur, Drag-Fallback, Rejection, öffentliche Docs und
  text-first Evidence `Applicable`.
- Agent-Parität umfasst alle fünf gepflegten Guidance-Flächen.
- `.specify/templates/` bleiben `N/A`, sofern das Feature sie nicht bewusst
  ändert.
- Externe Consumer-, historische und Free-Vision-Quellen bleiben unverändert.

## 10. Gate-Entscheidung

### Bestehendes TV203-/Free-Vision-Gate

`ReadyForTerminalGuiAudit` ist nur zulässig, wenn alle 13 Findings geschlossen,
alle Integrationsslices grün, keine unerlaubte lokale Consumer-Sonderlogik
nötig und alle Pflichtgates bestanden sind. Andernfalls bleibt die Entscheidung
`Blocked` und wird an 025, 026 oder einen Product Owner zurückgegeben.

### Wave 5 und Wave 6

Beide Waves bleiben auch bei `ReadyForTerminalGuiAudit` auf
`BlockedPendingTerminalGuiAudit`. Feature 029 prüft anschließend die
Frameworkbasis zusätzlich gegen Terminal.GUI v1.9.0. Nur der nach allen daraus
entstehenden Hardening-Läufen ausgeführte neue Closure-Lauf darf Wave 5
`Eligible` setzen. Wave 6 bleibt danach höchstens `ConditionallyReady` und
benötigt weiterhin die Post-Wave-5-Delta-Prüfung.

## 11. Abschluss- und Archivregeln

Nach vollständigem Pass:

1. Lastenhefte 10, 11 und 12 nach dem Repository-Rename-Workflow archivieren.
2. Feature-024-Gate, Pflichtenheft, Abarbeitungsreihenfolge, Agent-Kontexte und
   Projektstatistik auf den finalen Zustand aktualisieren.
3. `Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` als
   nächsten Schritt benennen, aber Feature 029 nicht in Feature 028 starten.
4. PR-Checks und Review-Threads konvergieren, mergen, Branch löschen und lokalen
   `main` synchronisieren.
5. Autonome Retrospektive ausführen; wiederverwendbare Preset-Erkenntnisse nur
   mit reproduzierbarer Evidence promoten.

## 12. Akzeptanzkriterien

1. Genau 13 Finding-Zeilen und keine unzugeordnete Drift/Gaps existieren.
2. Jede Zeile hat genau eine erlaubte Closure-Entscheidung und vollständige
   Evidence.
3. Alle sieben Integrationsslices bestehen über reale Produktionspfade.
4. Auditrelationen sind beidseitig und maschinenprüfbar.
5. Wave-5- und Wave-6-Consumer-Matrix ist vollständig und read-only entstanden.
6. Full Release, Coverage, Format, DocFX/A11Y, Security und Remote-Gates sind
   auf dem finalen Stand grün.
7. Kein Runtime-, API-, Dependency-, Example- oder Historical-Source-Fix wird
   innerhalb 028 versteckt.
8. Das bestehende Gate ist entweder nachvollziehbar
   `ReadyForTerminalGuiAudit` oder bleibt mit konkreter Reopen-Grenze
   `Blocked`.
9. Wave 5 und Wave 6 sind in jedem Fall
   `BlockedPendingTerminalGuiAudit`; der nächste Intake ist Feature 029.
10. Nach Merge sind Working Tree und lokaler `main` sauber und identisch zu
    `origin/main`; der nächste Intake ist sichtbar, aber nicht gestartet.

## 13. Stop-Grenzen

Der Lauf stoppt bei einem offenen/reproduzierbaren Finding, notwendiger Runtime-
Reparatur, Breaking/ProductDecision, verändertem Historical-/Consumer-Source,
nicht behebbaren Pflichtcheck, unvollständiger Plattform-Evidence oder einer
Versuchung, Wave 5 im selben Feature zu beginnen.

## 14. Kopierbarer autonomer Intake-Prompt

```text
$speckit-autonomous Use
`Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md` as the binding intake
for Feature `028-pre-wave5-wave6-conformance-closure`.

Start only from clean synchronized main after Features 025 and 026 are merged.
Read Feature-024 Revision 2, all final 025/026 artifacts and evidence, and the
historical Feature-027 closure. Preserve all 13 finding IDs and independently
revalidate them; do not silently rewrite observations or acceptance boundaries.

This is evidence and integration closure, not product implementation. Do not
fix runtime/API/component/data findings in 028, port Wave 5 or Wave 6, modify
TVDEMOS/TVFM/tv203s/external Free Vision, add dependencies, or weaken tests.
Reopen 025/026 or stop for ProductDecision when required.

Run the complete Spec Kit lifecycle, all useful optional clarification,
checklist, plan-review, task-review, and repeated Analyze passes to convergence.
Execute the seven required real-path integration slices, complete Release and
coverage gates, conditional documentation/A11Y, security, platform and remote
review gates, exact evidence, version/build-counter rules, merge/main sync, and
retrospective. On full pass, close only the existing TV203/Free Vision gate as
ReadyForTerminalGuiAudit. Keep Wave 5 and Wave 6
BlockedPendingTerminalGuiAudit. Name
`Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` as the next
intake, but do not start Feature 029 in this run.
```
