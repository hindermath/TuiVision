# Lastenheft 13: TV203-, Free-Vision- und Terminal.GUI-Konformitätsaudit

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:**
`029-tv203-freevision-terminalgui-conformance-audit`

**Verbindliche Reihenfolge:** nach dem vollständig gemergten Feature
`028-pre-wave5-wave6-conformance-closure`, vor Wave 5 und Wave 6

**Lieferart:** reines Framework-, Consumer- und Proof-Audit ohne Runtime-,
API-, Dependency- oder Beispieländerung

**Primärquelle:** Borland Turbo Vision 2.0.3 und die read-only Quellen unter
`tv203s/`

**Unabhängige Implementierungsmeinungen:** gepinntes Free Vision sowie
Terminal.GUI v1.9.0

*Feature 029 runs after the merged Feature 028 and before Wave 5 or Wave 6. It
is a read-only framework, consumer, and proof audit. Borland Turbo Vision 2.0.3
remains authoritative; pinned Free Vision and Terminal.GUI v1.9.0 are
independent implementation opinions.*

---

## 1. Ausgangslage

Feature 024 und seine Consumer-Review-Revision 2 haben 48 Frameworkverträge
in 16 Domänen erfasst. Features 025 und 026 haben die 13 Findings `F001` bis
`F013` gehärtet. Feature 028 prüft diese Schließungen unabhängig, gibt Wave
5 und Wave 6 aber noch nicht frei.

Terminal.GUI ist eine eigenständige moderne C#-TUI-Implementierung. Die
Version 1.9.0 ist die höchste Veröffentlichung der bewusst gewählten
1.9.x-Linie. Ihre Application-, MainLoop-, View-, Focus-, Layout-, Driver-,
Window-, Dialog-, Menu-, Status-, FileDialog-, Clipboard- und Test-Fake-
Verträge können bestätigen, dass eine TuiVision-Modernisierung fachlich
tragfähig ist, oder eine bislang übersehene Consumer- oder Proof-Lücke
sichtbar machen.

Terminal.GUI ist keine neue normative Quelle. Eine Abweichung ist kein Finding,
solange TuiVision die historische Absicht, moderne C#-Sicherheit, A11Y,
Plattformgrenzen und die realen `TVDEMOS/`-/`TVFM/`-Verbraucher nachvollziehbar
trägt.

*Terminal.GUI provides a modern C# comparison, not a replacement design. A
difference is not a defect unless it exposes a reproducible TuiVision contract,
consumer, safety, accessibility, platform, or real-path proof gap.*

## 2. Verbindliche Eingaben

1. alle finalen Feature-024-Artefakte einschließlich Revision 2
2. alle finalen Feature-025-, Feature-026- und Feature-028-Artefakte und
   PR-Evidence
3. `tv203s/`, `TVDEMOS/` und `TVFM/` ausschließlich read-only
4. Free Vision am bereits akzeptierten Commit
   `ffc03b34d8cafb85ddcf0686de1c5551601dacb2`
5. Terminal.GUI aus `https://github.com/tui-cs/Terminal.Gui`:
   - Release und Tag `v1.9.0`
   - annotiertes Tag-Objekt
     `4b812e44798f2c7567afec50ba9a9293b6beb6de`
   - aufgelöster Commit
     `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`
   - MIT-Lizenz
6. aktueller TuiVision-Produkt-, Test-, Dokumentations-, Governance- und
   Agent-Kontext auf dem nach Feature 028 synchronisierten `main`

Terminal.GUI v2, spätere v1-Linien und nicht gepinnte Branch-Stände sind
nicht Teil der Vergleichsbasis.

## 3. Ziele

1. Alle bestehenden Verträge `C001` bis `C048` gegen die dritte
   Quellenperspektive prüfen.
2. Für jeden Vertrag genau eine Terminal.GUI-Relation mit konkreter Quelle
   oder begründetem `NotApplicable` erfassen.
3. Consumer-nahe Relevanz für Wave 5 (`TVDEMOS/`) und Wave 6 (`TVFM/`)
   erneut lesen, ohne Beispiele zu portieren.
4. TuiVision-Verhalten, Tests und Proof-Grenzen kritisch gegen Source und Tests
   von Terminal.GUI prüfen, nicht gegen Klassennamen oder Architekturform.
5. Neue Verträge `C049+` nur für eine materielle, consumer-relevante und noch
   nicht abgedeckte Frameworkverantwortung anlegen.
6. Reproduzierbare neue Findings als `TG001+` erfassen und genau einem
   späteren Eigentümerlauf zuordnen.
7. Ausschließlich aus realen Findings nicht leere Hardening-Lastenhefte sowie
   immer ein unabhängiges Closure-Lastenheft erzeugen.
8. Wave 5 und Wave 6 bis zum Merge dieses Closure-Laufs blockiert halten.

## 4. Scope

### 4.1 Im Scope

- Audit-Spezifikation, Plan, Tasks, Checklists und PR-Evidence
- kanonische maschinenlesbare Auditdaten und bidirektionale Validatoren
- Terminal.GUI-Quellmanifest mit Provenance, Pin, Lizenz, Pfad, SHA-256,
  Verhaltenszusammenfassung und No-Copy-Grenze
- erweiterte Framework-Konformitätsmatrix
- Consumer-Readiness-Review für `TVDEMOS/` und `TVFM/`
- Findings-, Ownership-, Abhängigkeits- und Folge-Lastenheft-Matrix
- formelles Pre-Wave-Gate, Reihenfolge, Agent-Parität und Projektstatistik
- Teständerungen nur für die Integrität der Auditdaten und Relationen

### 4.2 Nicht im Scope

- Runtime- oder Public-API-Änderungen
- neue oder aktualisierte NuGet-Abhängigkeiten
- Einbau, Fork oder Portierung von Terminal.GUI
- mechanische Übersetzung oder Nachbau seiner Klassenhierarchie
- Veränderungen an `tv203s/`, `TVDEMOS/`, `TVFM/`, Free Vision oder
  Terminal.GUI
- Wave-5- oder Wave-6-Portierung
- sofortige Reparatur eines gefundenen Produkt- oder Proof-Problems
- breite Framework-Neuschreibung oder visuelle Remediation
- Terminal.GUI-v2- oder Latest-Branch-Empfehlungen

## 5. Quellen- und Kopiergrenze

Terminal.GUI wird in einem externen, nicht getrackten Arbeitsverzeichnis aus
dem exakten Commit ausgecheckt. Das Repository speichert nur:

- offizielle URL, Tag-Objekt, Commit, Abrufdatum und Lizenz
- geprüfte relative Pfade und SHA-256-Prüfsummen
- eigene kurze Verhaltenszusammenfassungen
- konkrete Testnamen als Navigationsnachweis
- GitHub-Permalinks auf den gepinnten Commit, wo sinnvoll

Es werden keine Terminal.GUI-Dateien, längeren Quelltextauszüge, generierten
Artefakte oder fremden Testdaten eingecheckt. Kurze API- und Typnamen dienen nur
der eindeutigen Quellenzuordnung.

## 6. Verbindliche Prüfdomänen

Die 16 bestehenden Feature-024-Domänen bleiben erhalten:

1. Basistypen und Collections
2. Events, Commands und Dispatch
3. View-Hierarchie, Fokus und Lebenszyklus
4. Koordinaten, Clipping, Layout und Resize
5. Application, MainLoop, Desktop und Modalität
6. Menüs, StatusLine und Hilfe
7. Dialoge, Controls und Validation
8. Editor, Clipboard und Dateien
9. Hilfe, Ressourcen und Lokalisierung
10. Streams, Registry und Persistenz
11. Buffer, Cells und Rendering
12. Keyboard, Mouse, Capture und Input
13. Charset, Fonts und Terminal
14. Treiber, Plattformfähigkeiten und bewusste Auslassungen
15. Accessibility und Tastaturgleichwertigkeit
16. Smoke-, Fake-Driver- und Proof-Helfer

Mindestens zu prüfende Terminal.GUI-Flows sind `Application`, `MainLoop`,
`Responder`, `View`, `Toplevel`, `Window`, `Dialog`, `ConsoleDriver`,
`FakeDriver`, Key-/Command-/Shortcut-Verarbeitung, Fokuswechsel, Layout mit
`Pos`/`Dim`, Rendering/Clipping, Menu, StatusBar, TextValidateField,
FileDialog, Clipboard und Mausereignisse.

## 7. Vertragsrelationen

Jeder Vertrag besitzt genau einen Wert `terminalGuiRelation`:

| Relation | Bedeutung |
|---|---|
| `CorroboratesOriginal` | Terminal.GUI bestätigt dieselbe beobachtbare historische Verantwortung |
| `CorroboratesModernization` | Terminal.GUI stützt eine bewusste moderne TuiVision-Entscheidung |
| `AlternativeModernization` | Beide modernen Designs sind vertretbar; Architekturparität ist nicht erforderlich |
| `DivergesFromTuiVision` | Das Verhalten unterscheidet sich materiell; ein Finding entsteht nur mit zusätzlicher reproduzierbarer Vertragslücke |
| `NotApplicable` | Terminal.GUI besitzt für diesen Vertrag keine sinnvolle Vergleichsoberfläche; Begründung und Trigger sind Pflicht |

Jede Zeile enthält mindestens `terminalGuiSourceIds`, Relation, Begründung,
TuiVision-Proof, Consumer-Relevanz, Risiko und gegebenenfalls Finding-ID.

Neue `C049+`-Verträge benötigen:

1. einen realen `TVDEMOS/`- oder `TVFM/`-Verbraucher,
2. historische oder begründet moderne Frameworkverantwortung,
3. TuiVision-Source- und Proof-Prüfung,
4. Terminal.GUI-Quellenbezug und
5. Review, dass kein bestehender Vertrag dieselbe Grenze abdeckt.

## 8. Finding-Vertrag

Neue Findings beginnen bei `TG001` und enthalten:

`FindingId`, `ContractId`, `DomainId`, `Observation`, `Reproduction`,
`HistoricalIntent`, `FreeVisionRelation`, `TerminalGuiRelation`,
`ConsumerScope`, `TuiVisionSource`, `CurrentProof`, `MissingProofOrBehavior`,
`Risk`, `PrimaryOwner`, `Dependencies`, `RequiredRedProof`,
`RequiredRealPathGreenProof`, `APIImpact`, `A11YImpact`, `PlatformImpact`,
`SuggestedBoundary`, `Decision`, `EvidencePath`, `Owner`, `Reviewer`,
`ReviewDate`, `ResidualRisk` und `ReevaluationTrigger`.

Erlaubte Entscheidungen:

| Decision | Bedeutung |
|---|---|
| `HardeningRequired` | reproduzierbare Produkt- oder Proof-Lücke mit späterem Eigentümerlauf |
| `IntentionalDeviation` | bewusste Abweichung mit Consumer-, Nutzer- und Proof-Begründung |
| `AlreadySatisfiedWithNewEvidence` | kein Produktfix, aber stärkere Evidence schließt die Beobachtung |
| `ProductDecision` | breaking oder destruktive Owner-Entscheidung erforderlich; autonome Folge stoppt |
| `RejectedComparison` | Terminal.GUI-Verhalten ist für den TuiVision-Vertrag nicht maßgeblich |

Ein Finding darf nicht allein aus anderem Typnamen, anderer Vererbung,
statischer versus instanzbasierter API, anderem Renderingmodell oder einer
zusätzlichen Terminal.GUI-Funktion entstehen.

## 9. Folge-Lastenhefte und Nummerierung

Feature 029 erzeugt nach Review der Findings:

1. für jede nicht leere Eigentümergruppe genau ein Hardening-Lastenheft,
2. die Gruppen in topologisch sortierter Abhängigkeitsreihenfolge und
3. danach immer genau ein unabhängiges Closure-Lastenheft.

Die Nummerierung beginnt mit der nächsten freien Nummer 030:

- bei null Findings ist 030 der Closure-Lauf;
- bei einer Hardening-Gruppe ist 030 Hardening und 031 Closure;
- bei zwei Gruppen sind 030 und 031 Hardening, 032 ist Closure.

Mögliche Eigentümergrenzen sind:

- `CoreRuntimeDriver`: MainLoop, Events, Dispatch, Fokus, View/Window,
  Layout, Rendering, Input, Driver und Plattform
- `ComponentDataInteraction`: Controls, Menüs, Status, Dialoge, Validation,
  Editor, Datei, Hilfe, Ressourcen, Clipboard und Persistenz
- `CrossCuttingA11YProof`: nur für eigenständige A11Y-, Dokumentations- oder
  Proof-Verträge, die nicht sicher in eine der beiden Hauptgrenzen passen

Leere Gruppen, spekulative Lastenhefte und leere PRs sind verboten. Ein Finding
erhält genau einen Primary Owner; weitere Abhängigkeiten werden nur als DAG-
Kanten erfasst.

## 10. Wave-Gate

Feature 029 und alle Hardening-Läufe halten beide Waves `Blocked`.

Der spätere Closure-Lauf darf Wave 5 nur `Eligible` setzen, wenn:

- alle alten Findings `F001` bis `F013` geschlossen bleiben,
- alle neuen `TG*`-Findings geschlossen oder akzeptiert abweichend sind,
- alle Verträge und Consumer-Zeilen vollständig sind,
- kein ProductDecision offen ist und
- alle lokalen, Plattform-, Remote-, Security-, A11Y- und Review-Gates passen.

Wave 6 wird höchstens `ConditionallyReady` und bleibt bis nach Wave 5 sowie
einer erneuten Delta-Prüfung blockiert.

## 11. Preset-Lernzyklus

Nach Feature 029 und jedem daraus entstehenden Lauf wird die autonome
Retrospektive ausgeführt. Reproduzierbare providerneutrale Verbesserungen
werden in Home Baseline als `PresetFollowUp` implementiert, als neue
Patch-Version validiert und veröffentlicht und vor dem nächsten Lauf aus der
exakten Tag-ZIP in TuiVision installiert.

`NoPromotion` erzeugt keinen Branch, PR, Release oder Installationswechsel.
Upstream-relevante Erkenntnisse werden gesammelt. Erst unmittelbar vor Wave 5
wird ein einzelner freundlicher englischer Follow-up-Issue für
`github/spec-kit` erstellt; `@mnriem` wird dort genau einmal angesprochen.

## 12. Validierung

Feature 029 muss mindestens nachweisen:

1. `specify check`, Voraussetzungen und vollständige Feature-Checklists
2. Clarify-, Plan-, Task- und Analyze-Konvergenz ohne offene hohe Findings
3. `git diff --check` und `dotnet format --verify-no-changes`
4. maschinenlesbare JSON-/Schema-/Relationstests
5. genau eine Terminal.GUI-Relation je Vertrag
6. bidirektionale Quellen-, Contract-, Finding- und Consumer-Relationen
7. exakte Tag-/Commit-/Hash-Prüfung des externen Manifests
8. kein getracktes externes, generiertes oder geschütztes Source-Artefakt
9. targeted Auditvalidator-Tests und vollständige Release-Tests
10. kanonisches Coverage-Gate, sofern Auditvalidator oder gemeinsame
    Testinfrastruktur geändert werden
11. DocFX, Playwright/Axe und UTF-8-Lynx für die umfangreiche
    learner-facing Dokumentation
12. Secret-, Dependency-, Agent-Paritäts- und Generated-Output-Scans
13. exakte Reviewed-HEAD-Evidence vor PR-Merge

Vor jedem Build oder Test gilt die Build-Counter-Regel.

## 13. Akzeptanzkriterien

1. Terminal.GUI v1.9.0 ist mit Tag-Objekt, Commit, Lizenz und Hashmanifest
   reproduzierbar gepinnt.
2. Alle bestehenden `C001` bis `C048` besitzen genau eine vollständige
   Terminal.GUI-Relation.
3. `C049+` existiert nur mit nachgewiesener neuer Consumer-Verantwortung.
4. Jede neue Beobachtung ist entweder kein Finding oder genau ein vollständig
   beschriebenes `TG*`-Finding.
5. Kein Finding fordert mechanische Terminal.GUI-Parität.
6. Die Consumer-Matrix für Wave 5 und Wave 6 ist vollständig.
7. Aus Findings entstehen nur nicht leere, dependency-geordnete Lastenhefte.
8. Ein unabhängiges Closure-Lastenheft existiert immer.
9. Keine Runtime-, API-, Paket-, Beispiel- oder externe Source-Änderung ist im
   Audit-Diff enthalten.
10. Wave 5 und Wave 6 bleiben nach Feature 029 blockiert.
11. Nach Merge sind lokales `main` und `origin/main` sauber und identisch.

## 14. Stop-Grenzen

Der Lauf stoppt bei notwendigem Sofort-Fix, Breaking/ProductDecision,
unverifizierbarem Terminal.GUI-Pin, Lizenz- oder Provenance-Konflikt,
veränderter externer Quelle, unvollständiger Consumer-Zuordnung,
nicht behebbarer Auditintegrität oder dem Versuch, Wave 5 im selben Feature zu
starten.

## 15. Kopierbarer autonomer Intake-Prompt

```text
$speckit-autonomous Use
`Lastenheft_13_TV203-FreeVision-TerminalGUI-Conformance-Audit.md` as the binding
intake for Feature `029-tv203-freevision-terminalgui-conformance-audit` in
`MergeAndSync` mode.

Start only from clean synchronized main after Feature 028 is merged. Keep
Borland Turbo Vision 2.0.3 and `tv203s/` authoritative, retain pinned Free
Vision as an independent implementation opinion, and add only Terminal.GUI
v1.9.0 from `tui-cs/Terminal.Gui`, annotated tag object
`4b812e44798f2c7567afec50ba9a9293b6beb6de` and peeled commit
`d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`, as the additional modern C#
comparison.

This is a read-only audit. Do not change runtime behavior, public APIs,
dependencies, examples, external sources, `tv203s/`, `TVDEMOS/`, `TVFM/`, or
copy Terminal.GUI source. Review every existing contract C001-C048, add C049+
only for a material uncovered consumer responsibility, and create TG001+
findings only for reproducible TuiVision contract, consumer, safety, A11Y,
platform, or real-path proof gaps. Architectural difference alone is not a
finding.

Run the complete autonomous Spec Kit lifecycle and all useful optional passes
to convergence. Produce the pinned source manifest, machine-readable relation
matrix, consumer review, findings and dependency DAG. Create only non-empty
finding-derived hardening Lastenhefte starting at Feature 030, followed by one
mandatory independent closure Lastenheft. Keep Wave 5 and Wave 6 blocked.

Complete local and remote validation, exact reviewed-head evidence, PR review,
merge, branch cleanup, main synchronization, and the autonomous retrospective.
Promote a reusable preset improvement only through the documented Home
Baseline patch-release and exact tag-ZIP adoption cycle before the next run.
Do not open or update a github/spec-kit preset issue during this feature.
```
