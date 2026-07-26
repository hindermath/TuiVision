# Lastenheft 14: TV203- und magiblot/tvision-Evolutionsaudit

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:**
`030-tv203-magiblot-evolution-audit`

**Verbindliche Reihenfolge:** nach dem vollständig gemergten Feature
`029-tv203-freevision-terminalgui-conformance-audit`, vor allen daraus
abgeleiteten Hardening-Läufen, dem unabhängigen Closure-Lauf, Wave 5 und Wave 6

**Lieferart:** reiner API-, Architektur-, Consumer- und Proof-Audit ohne
Runtime-, Public-API-, Dependency-, Beispiel- oder Fremdquellenänderung

**Primärquelle:** Borland Turbo Vision 2.0.3, die read-only Quellen unter
`tv203s/` und die bereits akzeptierten öffentlichen TuiVision-Verträge

**Zusätzlicher Modernisierungszeuge:** `magiblot/tvision` am gepinnten Commit
`57b6f56b38e0ee75240a80a10ee0e11470c24693`

*Feature 030 runs after the merged Terminal.GUI audit and before any derived
hardening, independent closure, Wave 5, or Wave 6. It compares observable API
and architecture responsibilities with a pinned modern evolution of the
original C++ lineage. magiblot/tvision is a modernization witness, not a new
normative source.*

---

## 1. Ausgangslage und Zweck

Feature 024 hat 48 Frameworkverträge in 16 Domänen erfasst. Features 025 und
026 haben die daraus entstandenen Findings gehärtet, Feature 028 hat deren
Schließung unabhängig revalidiert und Feature 029 ergänzt eine moderne,
eigenständige C#-Architekturmeinung aus Terminal.GUI v1.9.0.

`magiblot/tvision` beantwortet eine andere Frage. Das Projekt entwickelt die
ursprüngliche C++-Codebasis behutsam weiter, nennt möglichst hohe
Quelltextkompatibilität als Ziel und integriert unter anderem Unicode,
plattformübergreifende Terminaltreiber, erweitertes Input, Clipboard,
Farben und bounds-sicherere Draw-Buffer-Verträge. Es kann deshalb zeigen, wie
historische Turbo-Vision-Verantwortungen innerhalb derselben Abstammungslinie
modernisiert wurden.

Diese Nähe ist hilfreich, aber nicht unabhängig. Übereinstimmung beweist weder
automatisch die Richtigkeit von TuiVision noch verlangt eine Abweichung eine
C#-Änderung. Maßgeblich bleiben historische Absicht, akzeptierte TuiVision-
Semantik, moderne C#-Sicherheit, Accessibility, Plattformgrenzen und reale
Verbraucher unter `TVDEMOS/` und `TVFM/`.

*The direct lineage makes magiblot/tvision useful for studying evolutionary
modernization, but also creates shared-bias risk. The audit therefore compares
behavioral responsibilities and proof boundaries, never implementation shape,
inheritance, memory layout, or line-by-line code.*

## 2. Verbindliche Quellenhierarchie

Die Review-Reihenfolge ist verbindlich:

1. Borland-Dokumentation und `tv203s/` bestimmen die historische Absicht.
2. Akzeptierte TuiVision-Contracts, Public API und Nutzerverhalten bestimmen
   die aktuelle Produktsemantik.
3. Das gepinnte Free Vision bleibt unabhängige Pascal-Implementierungsmeinung.
4. Terminal.GUI v1.9.0 bleibt die in Feature 029 geprüfte alternative moderne
   C#-Architekturmeinung; Feature 030 verwendet nur dessen gemergte Evidence.
5. `magiblot/tvision` ist ein direkter C++-Modernisierungszeuge.
6. `TVDEMOS/` und `TVFM/` sind read-only Consumer-Evidence für Wave 5 und 6.

Keine nachgeordnete Quelle überschreibt eine vorherige. Eine einzelne externe
Implementierung, ein gleicher Klassenname oder eine zusätzliche Funktion
erzeugt kein Finding.

## 3. Reproduzierbarer magiblot-Pin

Feature 030 verwendet ausschließlich:

| Feld | Verbindlicher Wert |
|---|---|
| Repository | `https://github.com/magiblot/tvision.git` |
| Referenzbranch | `master` nur als Herkunftshinweis, niemals als bewegliche Auditbasis |
| Commit | `57b6f56b38e0ee75240a80a10ee0e11470c24693` |
| Tree | `96dd03873955689ff0a79f6c8107a8148fe1ebd6` |
| Commit-Zeitpunkt | `2026-05-12T18:22:58+02:00` |
| Commit-Betreff | `Also restore terminal state on SIGBUS and SIGPIPE` |
| Release-/Tag-Status | kein Release oder Tag als Auditbasis; der Commit ist der Pin |
| `COPYRIGHT` SHA-256 | `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548` |

Das `COPYRIGHT` enthält Borlands ursprünglichen Disclaimer, eine MIT-Lizenz für
die von magiblot und weiteren Contributors erstellten Änderungen sowie
Hinweise zu Drittkomponenten. Der Audit darf diese mehrteilige Herkunft nicht
verkürzt als pauschale MIT-Lizenz darstellen.

Der Commit wird in einem externen, nicht getrackten Arbeitsverzeichnis
ausgecheckt. Das Repository speichert nur URL, Commit, Tree, Zeitpunkt,
Lizenz-/Provenance-Zusammenfassung, geprüfte relative Pfade, SHA-256-Werte,
kurze eigene Verhaltenszusammenfassungen und bei Bedarf Permalinks auf den
gepinnten Commit.

## 4. Ziele

1. Alle nach Feature 029 akzeptierten Verträge gegen den gepinnten
   Modernisierungszeugen prüfen.
2. Für jeden Vertrag genau eine `magiblotRelation` mit konkreter Source- oder
   begründeter `NotApplicable`-Evidence erfassen.
3. API-Nähe, Architekturverantwortung und beobachtbares Verhalten getrennt
   bewerten.
4. Wave-5- und Wave-6-Consumer erneut read-only auf materielle Relevanz prüfen.
5. Neue Verträge nur für reale, bislang ungedeckte Consumer-Verantwortungen
   anlegen.
6. Reproduzierbare magiblot-Beobachtungen als `MB001+` erfassen.
7. Alle `TG*`- und `MB*`-Beobachtungen in kanonische `CF001+`-Findings
   deduplizieren oder als Nicht-Finding begründet schließen.
8. Nur aus nicht leeren `CF*`-Ownergruppen dependency-geordnete
   Hardening-Lastenhefte ab Feature 031 erzeugen.
9. Danach immer genau ein unabhängiges Closure-Lastenheft erzeugen.
10. Wave 5 und Wave 6 bis zum Merge dieses Closure-Laufs blockiert halten.

## 5. Scope

### 5.1 Im Scope

- Spec-Kit-Spezifikation, Plan, Research, Datenmodell, Contract, Checklists,
  Tasks, Analyze-Evidence und PR-Evidence
- reproduzierbares magiblot-Quellmanifest mit No-Copy- und Lizenzgrenze
- semantische API- und Architekturmatrix je bestehendem Vertrag
- aktualisierte Consumer-Readiness-Matrix für `TVDEMOS/` und `TVFM/`
- `MB*`-Beobachtungen und vollständige TG-/MB-Deduplizierung
- kanonische `CF*`-Findings, Owner-DAG und Folge-Lastenhefte
- formelles Pre-Wave-Gate, Agent-Parität und Projektstatistik
- Teständerungen ausschließlich für Auditdaten-, Manifest-, Relations-,
  Deduplizierungs- und Reihenfolgeintegrität

### 5.2 Nicht im Scope

- Runtime- oder Public-API-Änderungen an TuiVision
- neue oder aktualisierte NuGet-, npm- oder Systemabhängigkeiten
- Fork, Vendorisierung, Submoduleinbau, Portierung oder Übersetzung von
  `magiblot/tvision`
- mechanische Übernahme seiner C++-API, Vererbung, Speicherstruktur oder
  Plattformabstraktion
- Veränderungen an `tv203s/`, `TVDEMOS/`, `TVFM/`, Free Vision,
  Terminal.GUI oder `magiblot/tvision`
- sofortige Reparatur eines Findings
- Wave-5- oder Wave-6-Portierung
- breite Framework-Neuschreibung oder visuelle Remediation
- Vergleich mit einem späteren beweglichen `master`-Stand
- Installation externer Build-Abhängigkeiten nur für diesen Quellenvergleich

## 6. Verbindliche Vergleichskapitel

Die Auswertung muss mindestens diese Kapitel besitzen:

1. Quellenhierarchie, Provenance, Lizenz und No-Copy-Grenze
2. semantische Public-API- und Vertragszuordnung
3. Application-, Event-Loop-, Shutdown- und Dispatch-Modell
4. View-Ownership, Fokus, Modalität und Fensterlebenszyklus
5. Koordinaten, Layout, Clipping, Resize und Desktop-Komposition
6. DrawBuffer, Screen-Flush, Cell-Modell und Rendering
7. UTF-8, Zeichenbreite, Combining Characters, Farben und Paletten
8. Keyboard, Mouse, Capture, Clipboard und Input-Protokolle
9. Treiber, Terminalzustand, Signale, Plattformfähigkeit und Fallbacks
10. Dialoge, Controls, Menüs, StatusLine und Validation
11. Editor, Dateien, Hilfe, Ressourcen und Persistenz
12. Testbarkeit, Fake-/Headless-Pfade und Real-Path-Proof
13. Wave-5-/Wave-6-Consumer-Relevanz
14. bewusste Abweichungen, Findings, Owner und Closure-Grenzen

## 7. Mindestquellen unter magiblot/tvision

Der Audit prüft je nach Vertragsrelevanz mindestens:

- `include/tvision/app.h`, `views.h`, `drawbuf.h`, `dialogs.h`, `editors.h`,
  `menus.h`, `help.h`, `tkeys.h`
- `include/tvision/internal/events.h` und `internal/winwidth.h`
- `source/tvision/tapplica.cpp`, `tevent.cpp`, `tgroup.cpp`, `tview.cpp`,
  `twindow.cpp`, `tdialog.cpp`, `tscreen.cpp`, `tmouse.cpp`
- relevante Menu-, StatusLine-, Editor-, Hilfe-, Datei- und Resource-Quellen
  unter `source/tvision/`
- relevante Input-, Zeichenbreiten- und Plattformquellen unter
  `source/platform/`
- zugehörige Tests unter `test/tvision/` und `test/platform/`
- reale Consumer unter `examples/tvdemo/`, `examples/tvedit/`,
  `examples/tvdir/` und `examples/tvhc/`

Header werden geprüft, wenn Contracts, Datentypen, Ownership oder API-Grenzen
nur dort sichtbar sind. Generierte Dateien, Binärartefakte und fremde Fixtures
werden nicht übernommen.

## 8. Vertragsrelationen

Jeder akzeptierte Vertrag besitzt genau einen Wert `magiblotRelation`:

| Relation | Bedeutung |
|---|---|
| `CorroboratesOriginal` | Die direkte Evolution bestätigt dieselbe beobachtbare historische Verantwortung. |
| `CorroboratesModernization` | Sie stützt eine bewusste moderne TuiVision-Entscheidung. |
| `AlternativeModernization` | Beide Modernisierungen sind fachlich tragfähig; Form- oder API-Parität ist nicht nötig. |
| `DivergesFromTuiVision` | Das Verhalten unterscheidet sich materiell; ein Finding benötigt zusätzliche reproduzierbare TuiVision-Evidence. |
| `NotApplicable` | Es gibt keine sinnvolle Vergleichsoberfläche; Begründung und Re-Evaluation-Trigger sind Pflicht. |

Jede Zeile enthält mindestens `magiblotSourceIds`, Relation, Begründung,
TuiVision-Proof, historischen Bezug, Consumer-Relevanz, Shared-Bias-Risiko,
gegebenenfalls `MB*` und den Deduplizierungsschlüssel.

Ein neuer Vertrag nach dem aktuellen Höchstwert benötigt:

1. einen realen `TVDEMOS/`- oder `TVFM/`-Verbraucher,
2. historische oder begründet moderne Frameworkverantwortung,
3. TuiVision-Source und TuiVision-Proof,
4. magiblot-Quellenbezug und
5. Review, dass kein bereits akzeptierter Vertrag dieselbe Grenze abdeckt.

## 9. Beobachtungs- und Finding-Vertrag

Neue magiblot-Beobachtungen beginnen bei `MB001`. Sie verwenden mindestens:

`ObservationId`, `ContractId`, `DomainId`, `Observation`, `Reproduction`,
`HistoricalIntent`, `FreeVisionRelation`, `TerminalGuiRelation`,
`MagiblotRelation`, `SharedBiasRisk`, `ConsumerScope`, `TuiVisionSource`,
`CurrentProof`, `MissingProofOrBehavior`, `Risk`, `SuggestedOwner`,
`Dependencies`, `RequiredRedProof`, `RequiredRealPathGreenProof`, `APIImpact`,
`A11YImpact`, `PlatformImpact`, `Decision`, `EvidencePath`, `Owner`, `Reviewer`,
`ReviewDate`, `ResidualRisk`, `ReevaluationTrigger` und `DeduplicationKey`.

Erlaubte `MB*`-Entscheidungen sind:

| Decision | Bedeutung |
|---|---|
| `CandidateFinding` | Reproduzierbare TuiVision-Lücke; vor Hardening mit TG-Evidence deduplizieren. |
| `IntentionalDeviation` | Bewusste moderne Abweichung mit Consumer- und Proof-Begründung. |
| `AlreadySatisfiedWithNewEvidence` | Kein Produktfix; stärkere Evidence schließt die Beobachtung. |
| `ProductDecision` | Breaking oder destruktive Entscheidung erforderlich; autonomer Lauf stoppt. |
| `RejectedComparison` | Die magiblot-Lösung ist für den TuiVision-Vertrag nicht maßgeblich. |

Nach Review werden alle offenen `TG*`- und `MB*`-Kandidaten genau einem
kanonischen `CF001+`-Finding oder einer begründeten Nicht-Finding-Entscheidung
zugeordnet. Ein `CF*`-Finding enthält alle Quellbeobachtungen, genau einen
Primary Owner, Abhängigkeiten, eine gemeinsame Reproduktion, erforderlichen
Red-Proof, Real-Path-Green-Proof und die strengste zutreffende Risiko- und
Governancewirkung. Eine TuiVision-Lücke wird dadurch nur einmal umgesetzt.

## 10. Finding-Grenzen

Kein Finding entsteht allein aus:

- gleichem oder anderem Typ-, Methoden- oder Feldnamen
- C++-Vererbung, Pointer-/Ownership-Form oder Speicherlayout
- statischer gegenüber instanzbasierter API
- anderer Datei-, Namespace- oder Plattformaufteilung
- zusätzlicher magiblot-Funktion ohne TuiVision-Consumer
- Quelltextkompatibilität als Selbstzweck
- fehlender mechanischer Parität zu Terminal.GUI oder magiblot/tvision

Ein `CF*`-Finding benötigt eine reproduzierbare TuiVision-Vertrags-, Consumer-,
Safety-, A11Y-, Plattform- oder Real-Path-Proof-Lücke.

## 11. Folge-Lastenhefte und Nummerierung

Feature 030 erzeugt erst nach der vollständigen kombinierten Review:

1. für jede nicht leere Primary-Owner-Gruppe genau ein Hardening-Lastenheft,
2. diese Gruppen in topologisch sortierter Abhängigkeitsreihenfolge und
3. danach genau ein unabhängiges Closure-Lastenheft.

Die Nummerierung beginnt mit Feature 031:

- null `CF*`-Findings: Feature 031 ist der Closure-Lauf;
- eine nicht leere Owner-Gruppe: 031 Hardening, 032 Closure;
- zwei Gruppen: 031 und 032 Hardening, 033 Closure;
- weitere Gruppen folgen demselben deterministischen Muster.

Mögliche Owner-Grenzen sind `CoreRuntimeDriver`,
`ComponentDataInteraction` und `CrossCuttingA11YProof`. Ein Finding besitzt
genau einen Primary Owner; leere Gruppen, spekulative Lastenhefte, doppelte
Findings und leere PRs sind verboten.

## 12. Wave-Gate

Feature 030 und alle daraus entstehenden Hardening-Läufe halten Wave 5 und
Wave 6 `BlockedPendingCombinedConformanceClosure`.

Der unabhängige Closure-Lauf darf Wave 5 nur `Eligible` setzen, wenn:

- `F001` bis `F013` geschlossen bleiben,
- jede `TG*`- und `MB*`-Beobachtung vollständig entschieden ist,
- jedes `CF*`-Finding geschlossen oder nachvollziehbar als bewusste Abweichung
  akzeptiert ist,
- alle Verträge und Consumer-Zeilen vollständig sind,
- kein `ProductDecision` offen ist und
- lokale, Plattform-, Remote-, Security-, A11Y- und Review-Gates passen.

Wave 6 wird höchstens `ConditionallyReady` und bleibt bis nach Wave 5 sowie
einer erneuten Delta-Prüfung blockiert.

## 13. Preset-Lernzyklus

Nach Feature 030 und jedem daraus entstehenden Lauf wird die autonome
Retrospektive ausgeführt. Reproduzierbare providerneutrale Verbesserungen
werden als `PresetFollowUp` in Home Baseline implementiert, als Patch-Version
validiert und veröffentlicht und vor dem nächsten Lauf aus der exakten Tag-ZIP
in TuiVision installiert.

`NoPromotion` erzeugt keinen Branch, PR, Release oder Installationswechsel.
Upstream-relevante Erkenntnisse werden weiter gesammelt. Erst unmittelbar vor
Wave 5 wird ein einzelner gebündelter, freundlicher englischer Follow-up-Issue
für `github/spec-kit` erstellt; `@mnriem` wird dort genau einmal angesprochen.

## 14. Validierung

Feature 030 muss mindestens nachweisen:

1. `specify check`, Voraussetzungen und vollständige Feature-Checklists
2. Clarify-, Plan-, Task- und Analyze-Konvergenz ohne offene hohe Findings
3. exakte Commit-, Tree-, Zeitpunkt-, Lizenz- und SHA-256-Prüfung
4. `git diff --check` und `dotnet format --verify-no-changes`
5. genau eine magiblot-Relation je akzeptiertem Vertrag
6. bidirektionale Source-, Contract-, Observation-, Finding- und Consumer-
   Relationen
7. vollständige TG-/MB-Deduplizierung mit genau einer Entscheidung je
   Beobachtung
8. genau eine Primary-Owner-Zuordnung je `CF*`-Finding
9. kein getracktes externes, generiertes oder geschütztes Source-Artefakt
10. targeted Auditvalidator-Tests und vollständige Release-Tests
11. kanonisches Coverage-Gate, sofern gemeinsame Validator- oder
    Testinfrastruktur geändert wird
12. DocFX, Playwright/Axe und UTF-8-Lynx für learner-facing Dokumentation
13. Secret-, Dependency-, Agent-Paritäts- und Generated-Output-Scans
14. deterministisch erzeugte nicht leere Folge-Lastenhefte und Closure-Reihenfolge
15. exakte Reviewed-HEAD-Evidence vor PR-Merge

Vor jedem Build oder Test gilt die Build-Counter-Regel. Ein externer
magiblot-Build ist kein TuiVision-Abnahmegate und darf keine zusätzlichen
Systempakete erzwingen; Source, Tests und Manifest bleiben der Vergleichspfad.

## 15. Akzeptanzkriterien

1. Der magiblot-Commit, Tree und mehrteilige Lizenzkontext sind reproduzierbar
   und unveränderlich nachgewiesen.
2. Jeder akzeptierte Vertrag besitzt genau eine vollständige
   `magiblotRelation`.
3. Neue Verträge existieren nur für materielle, bislang ungedeckte Consumer-
   Verantwortungen.
4. Jede magiblot-Beobachtung besitzt genau eine erlaubte Entscheidung.
5. Jede offene `TG*`- und `MB*`-Beobachtung ist genau einem `CF*`-Finding oder
   einer begründeten Nicht-Finding-Entscheidung zugeordnet.
6. Kein Finding verlangt C++-Form-, Quelltext- oder Architekturparität.
7. Die Wave-5-/Wave-6-Consumer-Matrix ist vollständig.
8. Nur nicht leere Ownergruppen erzeugen Hardening-Lastenhefte ab 031.
9. Genau ein unabhängiges Closure-Lastenheft folgt zuletzt.
10. Der Audit-Diff enthält keine Runtime-, API-, Dependency-, Beispiel- oder
    Fremdquellenänderung.
11. Wave 5 und Wave 6 bleiben nach Feature 030 blockiert.
12. Nach Merge sind lokales `main` und `origin/main` sauber und identisch.

## 16. Stop-Grenzen

Der Lauf stoppt bei unverifizierbarem Commit oder Tree, veränderter oder
falsch vereinfachter Lizenz-Provenance, notwendigem Sofort-Fix,
Breaking-/`ProductDecision`, unvollständigem Feature-029-Handoff,
nicht deterministisch deduplizierbaren Findings, unklarer Owner-Zuordnung,
nicht behebbarer Auditintegrität, Fremdquellenkopie oder dem Versuch, Wave 5,
Wave 6 oder ein Hardening im selben Feature zu implementieren.

## 17. Kopierbarer autonomer Intake-Prompt

```text
$speckit-autonomous Use
`Lastenheft_14_TV203-Magiblot-Evolution-Audit.md` as the binding intake for
Feature `030-tv203-magiblot-evolution-audit` in `MergeAndSync` mode.

Start only from clean synchronized main after Feature 029 is merged. Keep
Borland Turbo Vision 2.0.3, `tv203s/`, and accepted TuiVision contracts
authoritative. Consume the merged Free Vision and Terminal.GUI evidence, and
add `magiblot/tvision` only as a direct-lineage modernization witness at commit
`57b6f56b38e0ee75240a80a10ee0e11470c24693`, tree
`96dd03873955689ff0a79f6c8107a8148fe1ebd6`, with the complete multi-part
COPYRIGHT and provenance boundary.

This is a read-only API, architecture, consumer, and proof audit. Do not change
runtime behavior, public APIs, dependencies, examples, external sources,
`tv203s/`, `TVDEMOS/`, `TVFM/`, Free Vision, Terminal.GUI, or magiblot/tvision.
Do not copy or mechanically translate C++ source and do not require structural,
inheritance, naming, or source compatibility. Compare observable contracts,
modernization decisions, consumer needs, safety, A11Y, platform boundaries,
and real-path proof.

Review every accepted contract, assign exactly one magiblot relation, create
new contracts only for material uncovered consumer responsibilities, and
record MB001+ observations. Consume the complete TG* handoff from Feature 029,
deduplicate TG* and MB* candidates into canonical CF001+ findings, and give
each CF finding exactly one Primary Owner and proof contract. Architectural
difference or direct-lineage agreement alone is not a finding.

Run the complete autonomous Spec Kit lifecycle and all useful optional passes
to convergence. Generate only non-empty dependency-ordered hardening
Lastenhefte starting at Feature 031, followed by exactly one independent
closure Lastenheft. Keep Wave 5 and Wave 6 blocked through that closure.

Complete local and remote validation, exact reviewed-head evidence, PR review,
merge, branch cleanup, main synchronization, and the autonomous retrospective.
Promote reusable preset learning only through the documented Home Baseline
patch-release and exact tag-ZIP adoption cycle before the next run. Do not open
or update a github/spec-kit preset issue during this feature.
```
