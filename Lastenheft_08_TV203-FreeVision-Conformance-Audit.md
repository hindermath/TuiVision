# Lastenheft 08: TV203-/Free-Vision-Konformitätsaudit

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** `024-tv203-freevision-conformance-audit`

**Verbindlicher Zeitpunkt:** nach `023-a11y-framework`, vor Wave 5

**Lieferart:** reines Audit und Evidence-Härtung ohne Runtime-Verhaltensänderung

**Kanonische Sprache:** Deutsch; englische Erklärungen folgen jeweils als zweiter Block

**Free-Vision-Referenzstand:** offizielles FPC-Repository, Branch `main`, Commit
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2`; Abweichungen beim späteren Abruf
müssen sichtbar dokumentiert werden.

*This requirements document defines feature `024-tv203-freevision-conformance-audit`.
It runs after feature 023 and before Wave 5. The feature is evidence-only and must
not change runtime behavior. The official Free Pascal source repository at the
pinned commit above is the only accepted Free Vision comparison snapshot.*

---

## 1. Ausgangslage

TuiVision bildet alle 151 historischen `.cc`-Implementierungsdateien unter
`tv203s/contrib/tvision/classes/` im kanonischen M-07-Ledger ab. Der aktuelle
Ledger klassifiziert 112 Zeilen als `portiert + getestet` und 39 Zeilen als
`bewusst ausgelassen + Begruendung`. Neuere Features dokumentieren historische
Absicht, moderne Abweichung und Proof-Grenzen detaillierter als frühe
Portierungsschritte. Deshalb ist die Inventarabdeckung stark, die Tiefe des
Verhaltensnachweises über das gesamte Framework jedoch noch nicht einheitlich.

Vor der Portierung der Turbo-Pascal-Demos aus `TVDEMOS/` soll ein vollständiges
Vertragsaudit klären, wo TuiVision dem Original folgt, wo moderne C#- und
.NET-Entscheidungen absichtlich abweichen und wo echte Verhaltens- oder
Evidence-Lücken bestehen. Free Vision dient dabei als unabhängige zweite
Implementierungsmeinung aus der Object-Pascal-Welt, nicht als gleichrangige
Normquelle und nicht als Übersetzungsvorlage.

*TuiVision maps all 151 historical implementation files in the canonical M-07
ledger. Newer features provide richer intent, deviation, and proof evidence than
early porting work. Before Wave 5, this audit establishes consistent contract-level
evidence. Free Vision is a secondary independent implementation witness, never a
co-equal authority or a source for mechanical translation.*

## 2. Ziele

1. Vollständige Zuordnung aller historischen Ledger-Zeilen und aller produktiven
   TuiVision-Frameworkdateien zu prüfbaren Vertragsdomänen.
2. Einheitliche Bewertung der historischen Absicht, des aktuellen Verhaltens,
   der modernen C#-Entscheidung und der vorhandenen Proof-Tiefe.
3. Vergleich relevanter Verträge mit der gepinnten Free-Vision-Implementierung,
   ohne fremden Quellcode zu übernehmen.
4. Trennung echter Verhaltensabweichungen von beabsichtigter Modernisierung,
   bewusster Auslassung und bloßen Evidence-Lücken.
5. Erzeugung eines priorisierten, reproduzierbaren Finding-Ledgers als einzige
   fachliche Grundlage für mögliche Features 025 und 026.
6. Definition eines belastbaren Pre-Wave-5-Closure-Gates für Feature 027.

*Goals are complete inventory and contract coverage, consistent historical and
modernization decisions, a pinned secondary Free Vision comparison, and a
findings ledger that alone may define later remediation features.*

## 3. Verbindliche Quellenordnung

### 3.1 Historische Interpretation

1. Borland-Originaldokumentation unter `TVDocs/` für Konzepte, Architektur und
   dokumentierte Verträge.
2. `tv203s/contrib/tvision/` als primäre Implementierungs- und
   Verhaltensreferenz für Turbo Vision 2.0.3.
3. Originale Turbo-Pascal-Quellen unter `TVDEMOS/` nur dort, wo sie einen
   späteren Wave-5-Vertrag erklären; Feature 024 portiert sie nicht.
4. Free Vision aus `https://gitlab.com/freepascal.org/fpc/source.git`,
   `packages/fv/`, am gepinnten Commit als sekundäre Vergleichsquelle.

### 3.2 Änderungsautorität

Aktuelle Lasten- und Pflichtenhefte, akzeptierte Feature-Spezifikationen,
öffentliche TuiVision-Verträge und nachgewiesenes Nutzerverhalten bestimmen,
ob eine erkannte historische Abweichung später geändert werden darf. Das Audit
ändert keinen Vertrag automatisch.

### 3.3 Konfliktregel

- Borland und `tv203s/` bestimmen die historische Absicht.
- Free Vision kann die historische Lesart oder eine moderne Anpassung stützen,
  widerlegen oder als eigene Abweichung sichtbar machen.
- Ein Free-Vision-Unterschied überschreibt niemals Borland-Evidence.
- Ein Konflikt zwischen historischem Verhalten und akzeptierter öffentlicher
  TuiVision-API wird als Produktentscheidung gestoppt und nicht autonom gelöst.

*Borland documentation and `tv203s/` define historical intent. Accepted TuiVision
contracts govern whether later changes are allowed. Free Vision may corroborate
or challenge an interpretation but never overrides Borland evidence.*

## 4. Scope

### 4.1 Im Scope

- `src/TuiVision.Core/`
- `src/TuiVision.Controls/`
- `src/TuiVision.Serialization/`
- `src/TuiVision.Compatibility/`
- `src/TuiVision.Drivers.Console/`
- zugehörige Tests und bestehende Proof-Helfer unter `tests/`
- `docs/porting-status.md` und vorhandene Feature-/PR-Evidence
- relevante Borland-Dokumentation, historische `.c`-/`.cc`-Dateien und bei
  Bedarf zugehörige Header unter `tv203s/`
- relevante Free-Vision-Units, insbesondere `Views`, `App`, `Dialogs`,
  `Drivers`, `Menus`, `Editors`, `StdDlg`, `Validate`, `Resource`, `HistList`,
  `MsgBox`, `Gadgets` und ihre Unicode-Gegenstücke
- statische Inventar-, Mapping- und Vollständigkeitsnachweise

### 4.2 Nicht im Scope

- Runtime- oder öffentliches Verhalten ändern
- öffentliche API-Signaturen ändern
- neue Abhängigkeiten oder Pakete hinzufügen
- Free Vision oder andere externe Quellen in das Repository vendoren
- Quelltext kopieren, zeilenweise übersetzen oder Bezeichner nur zur Parität
  angleichen
- Wave-5-Beispiele portieren
- bestehende Beispiele visuell überarbeiten
- breite Framework-Revision oder Architektur-Neuzuschnitt
- historische Dateien unter `tv203s/`, `TVDEMOS/` oder `TVFM/` verändern
- Findings aus 025 oder 026 bereits in 024 beheben
- einen Community-Catalog-PR ohne Maintainer-Rückmeldung erstellen

*Feature 024 reviews source, behavior, tests, and evidence. It changes no runtime,
API, dependency, example, external source, or broad architecture.*

## 5. Prüfdomänen

Das Audit muss mindestens folgende Domänen vollständig abdecken:

1. Basistypen, Collections, Sortierung, Punkte und Rechtecke
2. Event-Erzeugung, Queue, Command, Broadcast und Dispatch-Reihenfolge
3. `TView`-, `TGroup`-, Owner-, Parent-, Fokus- und Lifecycle-Verträge
4. lokale/globale Koordinaten, Clipping, Wachstum, Resize und Exposure
5. Application, Program, Desktop, Modalität und Window-Stack
6. Menüs, StatusLine, Shortcuts, Command-Enablement und Help/Description
7. Dialoge, Controls, Validation, Rejection und Zustandsbewahrung
8. Editor, Clipboard, Datei-, Close-, Conflict-, Search- und Replace-Flows
9. Hilfe, Querverweise, Compiler, Ressourcen, History und Lokalisierung
10. Streams, Registries, Objektidentität, Zyklen, malformed Input und Versionen
11. DrawBuffer, ConsoleBuffer, Cells, Paletten, Cursor und Snapshots
12. Tastatur-, Maus-, Double-Click-, Drag- und Terminal-Ingress
13. Charset, Unicode, Fonts, Terminal-Subset und Plattform-Fallbacks
14. Compatibility-Schicht und bewusst ausgelassene native Plattformpfade
15. A11Y-Texte, Fokusereignisse, strukturierte Shortcuts und High Contrast
16. Smoke-, App-Loop-, View-Tree-, Buffer-/Cell- und Proof-Helfer

*The audit covers core types, events, views, focus, geometry, application shell,
menus, controls, editors, help, persistence, rendering, input, terminal,
compatibility, accessibility, and proof helpers.*

## 6. Inventar- und Abdeckungsvertrag

### R-CFA-001: Historisches Inventar

Jede der 151 `.cc`-Zeilen aus `docs/porting-status.md` muss genau einer
Audit-Domäne und mindestens einem Contract-Eintrag zugeordnet werden. Bewusst
ausgelassene Zeilen benötigen eine erneut geprüfte Begründung und einen
Reevaluierungs-Trigger.

### R-CFA-002: Modernes Inventar

Jede produktive C#-Datei der fünf Frameworkmodule muss genau einer primären
Audit-Domäne zugeordnet werden. Generierte `bin/`-/`obj/`-Dateien zählen nicht.
Öffentliche Verträge werden zusätzlich compiler- oder reflexionsgestützt
inventarisiert, damit mehrere Typen in einer Datei nicht verschwinden.

### R-CFA-003: Test- und Evidence-Inventar

Jeder Contract-Eintrag benennt einen konkreten Test, eine konkrete Evidence
oder eine sichtbare `EvidenceGap`. Ein bloßer Verweis auf ein ganzes
Testprojekt reicht nur, wenn ein deterministischer Filter oder eine benannte
Testsammlung den Vertrag eindeutig nachweist.

### R-CFA-004: Free-Vision-Abdeckung

Jede Audit-Domäne erhält einen Free-Vision-Vergleich oder ein begründetes
`NotApplicable`. Ein Unit-Name allein ist kein Vergleich; die relevante
Verhaltensaussage, der Quellpfad und der gepinnte Commit müssen dokumentiert
sein.

## 7. Entscheidungsmodell

Jeder Contract-Eintrag erhält genau eine primäre Entscheidung:

| Entscheidung | Bedeutung |
|---|---|
| `Aligned` | Historische Absicht und aktueller TuiVision-Vertrag stimmen ausreichend überein. |
| `IntentionalModernization` | TuiVision weicht bewusst und begründet für modernes C#/.NET, Unicode, Sicherheit, A11Y oder Plattformunabhängigkeit ab. |
| `BehavioralDrift` | Beobachtbares Verhalten weicht unbegründet oder schädlich vom akzeptierten historischen Vertrag ab. |
| `EvidenceGap` | Verhalten kann mit vorhandener Evidence nicht zuverlässig bewertet werden. |
| `ConsciouslyOmitted` | Historische Fähigkeit bleibt mit begründeter Scope-/Plattformgrenze ausgelassen. |

Die Free-Vision-Relation ist ein getrenntes Feld:

| Relation | Bedeutung |
|---|---|
| `CorroboratesOriginal` | Free Vision bestätigt die aus Borland/`tv203s` abgeleitete Absicht. |
| `CorroboratesModernization` | Free Vision zeigt eine vergleichbare, aber nicht normative Modernisierung. |
| `DivergesFromOriginal` | Free Vision besitzt eine eigene erkennbare Abweichung. |
| `NotApplicable` | Kein sinnvoller Free-Vision-Vergleich existiert; Begründung ist dokumentiert. |

`Applicable`, `N/A`, `Open`, Governance-Status oder Remediation-Status dürfen
nicht als Contract-Entscheidung verwendet werden.

## 8. Finding-Modell

Nur `BehavioralDrift` und `EvidenceGap` erzeugen Findings. Jedes Finding enthält:

- stabile `FindingId`
- betroffene `ContractId`
- Priorität `Critical`, `High`, `Medium` oder `Low`
- reproduzierbares beobachtetes Verhalten oder fehlende Proof-Grenze
- historische Referenz und Free-Vision-Relation
- Nutzer-, API-, Daten-, Sicherheits-, A11Y- und Plattformauswirkung
- empfohlene Disposition `Core025`, `ComponentData026`, `Closure027`,
  `AcceptedFollowUp` oder `ProductDecision`
- Owner, Akzeptanztest und Nicht-Ziele

`Critical` oder `High` darf nicht als still akzeptiertes Follow-up in Wave 5
verschoben werden. Ein öffentliches Breaking-Change-Risiko wird immer
`ProductDecision`.

*Only behavioral drift and evidence gaps become findings. Every finding has a
stable ID, severity, reproduction, impact, owner, proof target, and one bounded
downstream disposition.*

## 9. Evidence-Artefakte

Feature 024 muss mindestens erzeugen:

- `specs/024-tv203-freevision-conformance-audit/framework-inventory.md`
- `specs/024-tv203-freevision-conformance-audit/framework-conformance-matrix.md`
- `specs/024-tv203-freevision-conformance-audit/freevision-source-manifest.md`
- `specs/024-tv203-freevision-conformance-audit/findings.md`
- `specs/024-tv203-freevision-conformance-audit/pre-wave5-gate.md`
- `specs/024-tv203-freevision-conformance-audit/pr-evidence.md`

Die Konformitätsmatrix verwendet mindestens:

`ContractId`, `Domain`, `HistoricalLedgerRows`, `BorlandSources`,
`FreeVisionSources`, `FreeVisionCommit`, `TuiVisionPaths`, `HistoricalIntent`,
`ObservedTuiVisionBehavior`, `PrimaryDecision`, `FreeVisionRelation`,
`ModernCSharpRationale`, `Proof`, `Risk`, `FindingId`, `DownstreamDisposition`.

Der Audit-Lauf darf die Matrix in domänenbezogene Teiltabellen aufteilen,
solange eine maschinenprüfbare Indexdatei Vollständigkeit und Eindeutigkeit
belegt.

## 10. Provenienz- und Lizenzgrenzen

1. Externe Free-Vision-Quellen werden außerhalb des TuiVision-Repositorys oder
   direkt aus dem offiziellen Upstream gelesen.
2. Repository-URL, Commit, Abrufdatum und geprüfte Pfade werden festgehalten.
3. Keine Free-Vision-Datei, kein längerer Quelltextauszug und keine mechanische
   Übersetzung wird committed.
4. Evidence beschreibt Verhalten in eigenen Worten und verwendet kurze
   Identifikatoren oder Signaturnamen nur zur eindeutigen Zuordnung.
5. Eine spätere tatsächlich quelltextabgeleitete Übernahme erfordert eine
   eigene Lizenz-/Provenienzentscheidung und liegt außerhalb von 024 bis 027.

*Free Vision remains external, pinned, and cited. Evidence paraphrases behavior.
No source or mechanical translation enters the TuiVision repository.*

## 11. Folgefeature-Regel

### Feature 025

Ein Lastenheft für `025-core-runtime-conformance-hardening` wird erst nach dem
finalen Audit erstellt und enthält ausschließlich Findings mit Disposition
`Core025`. Ohne nicht leeren, akzeptierten Scope gibt es keinen Feature-Branch
und keinen leeren PR.

### Feature 026

Ein Lastenheft für `026-component-data-conformance-hardening` wird erst nach dem
finalen Audit erstellt und enthält ausschließlich Findings mit Disposition
`ComponentData026`. Ohne nicht leeren, akzeptierten Scope gibt es keinen
Feature-Branch und keinen leeren PR.

### Feature 027

`027-pre-wave5-conformance-closure` ist das verpflichtende Abschluss-Gate. Es
behebt keine neue breite Remediation, sondern prüft Matrix, Findings,
Regressionen, Governance und vollständige Qualitätsgates erneut. Neue
`Critical`-/`High`-Findings blockieren den Abschluss.

*Features 025 and 026 are created only from non-empty accepted audit findings.
Feature 027 is mandatory and verifies closure without absorbing new broad work.*

## 12. Governance und autonome Retrospektive

Der Lauf berücksichtigt die installierte Matrix aus sechs Basis-Presets plus
`autonomous-run-governance`. Cloud-, Supply-Chain-, Regulierungs-, Skript- und
AI-SBOM-Checkpoints erhalten triggerbasierte Entscheidungen; ein reines Audit
erzeugt keine erfundenen Runtime- oder Cloud-Nachweise.

Nach jedem Lauf 024 bis 027 wird `$speckit-autonomous-retrospective`
ausgeführt. TuiVision-spezifische Beobachtungen bleiben lokal. Wiederverwendbare
Findings werden als `PresetFollowUp` an die Home-Baseline-Arbeitsfläche
übergeben und erhalten einen reproduzierbaren synthetischen Test. Der offene
Upstream-Issue `github/spec-kit#3479` wird nur nach einer veröffentlichten und
erneut validierten Preset-Version einmalig aktualisiert.

## 13. Validierung

Feature 024 führt mindestens aus:

- `git diff --check`
- `dotnet format --verify-no-changes`, falls formatierbare C#- oder Projektdateien berührt werden
- deterministische Inventar- und Matrixvollständigkeitsprüfung
- vorhandene gezielte Tests nur, wenn bestehendes Verhalten zur Entscheidung
  reproduziert werden muss
- vollständige Release-Tests und Coverage nur, wenn gemeinsame ausführbare
  Proof-Logik geändert wird
- DocFX plus `tests/web-a11y`, wenn XML/API, Navigation oder learner-facing
  Guides geändert werden
- Secret-, Generated-Output-, Dependency- und historische Source-Diff-Prüfung

Vor jedem `dotnet build` oder `dotnet test` gilt der manuelle Build-Zählervertrag.
Reine externe Quellenlektüre löst keinen Build aus.

## 14. Akzeptanzkriterien

| ID | Kriterium |
|---|---|
| AK-CFA-001 | Alle 151 historischen `.cc`-Ledger-Zeilen sind genau einer Audit-Domäne und mindestens einem Contract zugeordnet. |
| AK-CFA-002 | Alle produktiven C#-Frameworkdateien und öffentlichen Verträge sind inventarisiert und eindeutig zugeordnet. |
| AK-CFA-003 | Jeder Contract besitzt genau eine erlaubte Primärentscheidung und genau eine Free-Vision-Relation. |
| AK-CFA-004 | Jede Domäne besitzt konkrete Borland-/`tv203s`-Evidence und Free-Vision-Evidence oder begründetes `NotApplicable`. |
| AK-CFA-005 | Jede `BehavioralDrift`- oder `EvidenceGap`-Entscheidung besitzt genau ein Finding mit Priorität und Disposition. |
| AK-CFA-006 | Kein Runtime-, API-, Paket-, Beispiel- oder historischer Source-Diff wurde erzeugt. |
| AK-CFA-007 | Free Vision ist gepinnt, extern geblieben und ohne kopierten oder mechanisch übersetzten Code ausgewertet. |
| AK-CFA-008 | 025 und 026 besitzen nur findings-basierten Scope; 027 besitzt ein messbares Closure-Gate. |
| AK-CFA-009 | Keine `[NEEDS CLARIFICATION]`-, TODO-, TBD- oder Platzhaltermarker verbleiben. |
| AK-CFA-010 | Alle ausgelösten lokalen und remoten Gates sind grün und die Evidence ist vollständig. |

## 15. Kopierbarer autonomer Startprompt

```text
$speckit-autonomous Execute the complete feature
`024-tv203-freevision-conformance-audit` from the binding intake
`Lastenheft_08_TV203-FreeVision-Conformance-Audit.md` in delivery mode
`MergeAndSync`.

Run the full repository-local autonomous workflow from Specify through repeated
Clarify, feature checklists, Plan, plan review, Tasks, repeated Analyze,
Implement, validation, PR review convergence, merge, local main synchronization,
and autonomous retrospective.

Keep feature 024 audit-only. Do not change runtime behavior, public APIs,
dependencies, examples, or historical sources. Pin and record the official Free
Vision source commit, keep it external, and use it only as a secondary comparison
below Borland documentation and tv203s. Produce complete inventory, conformance,
source-manifest, findings, pre-Wave-5 gate, and PR-evidence artifacts.

Do not create speculative 025 or 026 requirements. Create downstream intake only
from accepted non-empty findings after the 024 audit has merged. Do not update
github/spec-kit#3479 unless a reusable preset improvement has been implemented,
published, and revalidated.
```
