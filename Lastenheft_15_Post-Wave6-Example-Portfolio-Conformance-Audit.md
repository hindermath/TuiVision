<!-- intake-authoring:begin -->
# Lastenheft 15: Post-Wave-6 Example Portfolio Conformance Audit

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** wird erst nach dem vollständig gemergten
Wave-6-Closeout aus der dann nächsten freien Feature-Nummer gebildet

**Verbindliche Reihenfolge:** nach Feature 030, allen daraus abgeleiteten
nicht leeren Hardening-Läufen, dem unabhängigen Pre-Wave-5-Closure, Wave 5
einschließlich Closeout sowie Wave 6 einschließlich Closeout

**Lieferart:** reiner Beispielportfolio-, Lern-, Framework-Nutzungs- und
Proof-Audit ohne Runtime-, Public-API-, Dependency- oder Beispieländerung

**Dokumentnummer und Feature-Nummer:** Die Dokumentnummer `15` ist fest. Die
Feature-Nummer bleibt absichtlich offen, weil Feature 030 die Anzahl der
Hardening-Läufe und damit die späteren Nummern erst aus realen Findings
ableitet.

*This is a deferred, binding post-Wave-6 intake. Its document number is fixed,
but its Spec Kit feature number is assigned only after Wave 6 because Feature
030 and its finding-derived work determine the intervening sequence.*

---

## 1. Ausgangslage und Zweck

Die 25 ursprünglichen Turbo-Vision-Beispiele werden in den Waves 1 bis 4 als
moderne C#-Beispiele bereitgestellt. Wave 5 ergänzt die Turbo-Pascal-Demos aus
`TVDEMOS/`; Wave 6 ergänzt den Turbo-Pascal-Dateimanager aus `TVFM/`.

Die bisherigen Features prüfen jedes Beispiel innerhalb seiner jeweiligen
Welle. Nach Wave 6 fehlt jedoch noch eine portfolioweite Gegenprüfung:

- Vermittelt jedes Beispiel weiterhin seinen historischen Lern- und
  Demonstrationszweck?
- Nutzt es das inzwischen gehärtete TuiVision-Framework konsequent?
- Sind lokale Sonderlösungen noch gerechtfertigt?
- Sind sichtbare Bedienung, Proof, Dokumentation, Accessibility und
  Plattformgrenzen über alle Wellen konsistent?
- Bestätigen oder relativieren Free Vision, Terminal.GUI und
  `magiblot/tvision` die getroffenen Modernisierungsentscheidungen?

Der Audit soll keine erneute pauschale Portierungswelle auslösen. Er erfasst
reproduzierbare Lücken, akzeptiert begründete moderne Abweichungen und erzeugt
nur für nicht leere Finding-Gruppen spätere Remediation-Lastenhefte.

*The goal is stronger behavioral and architectural alignment with Turbo
Vision while preserving idiomatic modern C#. The audit does not seek visual,
API, inheritance, memory-layout, or source-text identity.*

## 2. Quellenhierarchie

Die folgende Reihenfolge ist verbindlich:

1. Borland-Dokumentation und die read-only Quellen unter `tv203s/` bestimmen
   die historische Absicht der 25 Originalbeispiele.
2. Die read-only Turbo-Pascal-Quellen unter `TVDEMOS/` und `TVFM/` bestimmen
   die historische Absicht der Waves 5 und 6.
3. Akzeptierte TuiVision-Verträge, Public API, modernes C# und nachgewiesenes
   Nutzerverhalten bestimmen die aktuelle Produktsemantik.
4. Das in Feature 024 gepinnte Free Vision dient als unabhängige,
   Pascal-nahe Architektur- und Implementierungsmeinung.
5. Terminal.GUI v1.9.0 dient über die gemergte Feature-029-Evidence als
   alternative moderne C#-Meinung zu Bedienbarkeit, Plattformgrenzen,
   Accessibility und Proof-Strategien.
6. Das in Feature 030 gepinnte `magiblot/tvision` dient als direkter
   C++-Modernisierungszeuge derselben Abstammungslinie.

Keine nachgeordnete Quelle überschreibt die historische Absicht oder die
akzeptierte TuiVision-Produktsemantik. Übereinstimmung mit einer
Vergleichsquelle beweist keine Richtigkeit; Abweichung erzeugt ohne
reproduzierbare TuiVision-Lücke kein Finding.

Die akzeptierten Pins aus Features 024, 029 und 030 werden wiederverwendet.
Bewegliche Upstream-Branches oder neuere Releases sind nicht Teil dieses
Audits. Eine spätere Versionsrevision benötigt ein eigenes Lastenheft.

## 3. Verbindliches Portfolio

Der Audit inventarisiert vollständig:

1. alle 25 historisch abgeleiteten Originalbeispiele unter `examples/`, die in
   den Waves 1 bis 4 geliefert wurden;
2. alle tatsächlich gelieferten Wave-5-Beispiele aus `TVDEMOS/`;
3. alle tatsächlich gelieferten Wave-6-Beispiele aus `TVFM/`;
4. `A11yFramework` als `SupplementalControl`.

`A11yFramework` wird auf Portfolio-, Lern-, Bedien- und Proof-Konsistenz
geprüft. Seine historische Relation ist `NotApplicable`, weil es kein
portiertes Turbo-Vision-Beispiel ist. Es darf trotzdem ein
portfolioübergreifendes Learning-, A11Y- oder Proof-Finding begründen.

Jedes Beispiel erhält dieselbe vollständige Baseline-Prüfung. Die Prüftiefe
wird zusätzlich nach Risiko erhöht, insbesondere bei Editor-, Hilfe-,
Ressourcen-, Datei-, Terminal-, Maus-, Drag-/Drop-, Persistenz- und
mehrfenstrigen Anwendungen.

## 4. Ziele

1. Eine vollständige, maschinenlesbare Inventarliste des gelieferten
   Beispielportfolios erstellen.
2. Den historischen Zweck und Lernnutzen jedes Beispiels nachvollziehbar
   festhalten.
3. Framework-Nutzung, lokale Sonderlogik und bewusste Abweichungen prüfen.
4. Sichtbare Bedienung und reale Application-Loop-Pfade bewerten.
5. Zustands-, View-Baum-, Buffer-/Cell-, Negativ- und Fallback-Proofs prüfen.
6. Guides, Übungen, Troubleshooting, Quell- und Testverweise gegen den
   verbindlichen Dokumentationsstandard prüfen.
7. Tastatur, relevante Mauspfade, text-first Accessibility, High Contrast,
   kleine Terminals und Plattformfallbacks bewerten.
8. Free-Vision-, Terminal.GUI- und magiblot-Evidence nur dort als zweite
   Meinung verwenden, wo eine fachlich vergleichbare Verantwortung besteht.
9. Reale Lücken als deduplizierte `EF001+`-Findings erfassen.
10. Nur aus nicht leeren Owner-Gruppen spätere Remediation-Lastenhefte
    erzeugen und danach genau einen unabhängigen Portfolio-Closure anlegen.

## 5. Scope

### 5.1 Im Scope

- Spec-Kit-Spezifikation, Plan, Research, Datenmodell, Contract, Checklists,
  Tasks, Analyze- und PR-Evidence
- Beispiel-, Guide-, Smoke-, Quellen- und Framework-Nutzungsinventar
- historische Zweck- und Intent-Zusammenfassungen in eigenen Worten
- read-only Review der relevanten TV203-, TP7-, Free-Vision-, Terminal.GUI-
  und magiblot-Evidence
- Beispielmatrix, Source-Manifest, Finding-Ledger, Owner-DAG und Handoff
- test-only Integritätsvalidatoren für Inventar, Relationen, Entscheidungen,
  Deduplizierung, Reihenfolge und Folge-Lastenhefte
- Projektstatistik, Agent-Parität und formeller Portfolio-Gate-Status

### 5.2 Nicht im Scope

- Runtime- oder Public-API-Änderungen
- neue oder aktualisierte Abhängigkeiten
- Änderungen unter `examples/`, `src/`, `tv203s/`, `TVDEMOS/` oder `TVFM/`
- sofortige Korrektur eines Findings
- erneute vollständige Portierung einer Beispielwelle
- optische oder strukturelle Identität mit TV203, Free Vision,
  Terminal.GUI oder magiblot
- mechanische Übersetzung von C++, Pascal oder C#
- neue Beispielprogramme
- bewegliche Upstream-Vergleiche
- breite Framework-Neustrukturierung

## 6. Portfolio-Rolle und Hauptentscheidung

Jede Zeile besitzt genau eine `PortfolioRole`:

| Rolle | Bedeutung |
|---|---|
| `HistoricalExample` | Historisch abgeleitetes TV203-, TVDEMOS- oder TVFM-Beispiel |
| `SupplementalControl` | Projektspezifisches Kontrollbeispiel ohne historische Konformitätswertung |

Jedes Beispiel besitzt genau eine `PrimaryDisposition`:

| Entscheidung | Bedeutung |
|---|---|
| `AcceptedAsIs` | Zweck, Framework-Nutzung, Bedienung, Proof und Lernwert sind ausreichend. |
| `AcceptedIntentionalDeviation` | Eine materielle Abweichung ist modern, dokumentiert und durch Proof begründet. |
| `CandidateFinding` | Mindestens eine reproduzierbare Lücke benötigt Deduplizierung und spätere Remediation. |
| `ProductDecision` | Breaking, destruktive oder fachlich nicht autonom entscheidbare Änderung wäre nötig; der Lauf stoppt. |

Die Hauptentscheidung ersetzt nicht die Dimensionsprüfung. Ein Beispiel kann
mehrere Gap-Dimensionen besitzen, aber nur eine Hauptentscheidung.

## 7. Verbindliche Prüfmatrix

Jedes Beispiel enthält mindestens:

`ExampleId`, `Name`, `PortfolioRole`, `Wave`, `HistoricalSourceIds`,
`ModernizationSourceIds`, `OriginalIntent`, `LearningGoal`,
`CurrentEntryPoint`, `VisibleFirstScreen`, `PrimaryInteractionPaths`,
`FrameworkComponents`, `LocalSpecialLogic`, `FrameworkDecision`,
`BehaviorStatus`, `InteractionStatus`, `ProofStatus`, `DocumentationStatus`,
`A11YStatus`, `PlatformStatus`, `HistoricalRelation`, `FreeVisionRelation`,
`TerminalGuiRelation`, `MagiblotRelation`, `PrimaryDisposition`,
`FindingIds`, `EvidencePath`, `Owner`, `Reviewer`, `ReviewDate`,
`ResidualRisk` und `ReevaluationTrigger`.

Die folgenden Dimensionen verwenden jeweils genau einen Wert:

| Status | Bedeutung |
|---|---|
| `Pass` | Der Vertrag ist vollständig und nachvollziehbar erfüllt. |
| `IntentionalDeviation` | Die Abweichung ist bewusst, modern und ausreichend belegt. |
| `Gap` | Eine reproduzierbare Lücke oder fehlende Evidence besteht. |
| `N/A` | Die Dimension ist nicht anwendbar; Begründung und Trigger sind Pflicht. |

## 8. Prüfschwerpunkte

### 8.1 Historischer Zweck und Lernwert

- Der ursprüngliche Demonstrationszweck wird aus den verbindlichen Quellen
  rekonstruiert und vollständig neu formuliert.
- Das moderne Beispiel vermittelt denselben Kernzweck oder dokumentiert eine
  begründete moderne Verschiebung.
- Die Demo zeigt nicht nur Technik, sondern einen nachvollziehbaren
  Lernfortschritt für Auszubildende.

### 8.2 Framework-Nutzung

Jedes Beispiel erhält genau eine Entscheidung:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

Wiederverwendbare Logik darf nicht dauerhaft als lokale Beispielsonderlösung
bestehen. `SmallFrameworkFix` und `FollowUpHardening` sind im Audit nur
Finding-Entscheidungen; sie werden nicht sofort implementiert.

### 8.3 Sichtbare Bedienung

- Ein interaktiv gedachtes Beispiel zeigt beim normalen Start seinen Zweck.
- Hauptfunktionen sind über Menü, StatusLine, Tastatur, Command oder
  begründete Mauspfade erreichbar.
- `Help -> Description` oder eine gleichwertige text-first Erklärung ist
  tastaturerreichbar.
- Rückmeldung ist sichtbar und nicht nur über internen Zustand prüfbar.
- Relevante Mausoperationen besitzen einen Tastaturfallback.

### 8.4 Proof

- Primary Proof läuft durch `app.Run()` oder den äquivalenten realen
  Application Loop.
- Konkreter Zustand, View-Identität und sichtbarer Buffer-/Cell-Nachweis
  werden kombiniert.
- Direkte Helper sind nur `SupplementalProof`, `SetupOnly` oder begründet
  `PrimaryProof` nach dem bestehenden Helper-Vertrag.
- Negative, Rejection-, Safe-Close-, Fallback- und Small-Terminal-Pfade sind
  dort belegt, wo sie zum Zweck des Beispiels gehören.

### 8.5 Dokumentation und Accessibility

- Jedes Beispiel besitzt einen Guide gemäß Pflichtenheft Abschnitt 10.4.
- Guide und sichtbare Anwendung stimmen bei Start, Bedienung, Lernziel,
  Fehlerbildern, Übungen, Quellcode und Tests überein.
- Deutsch-first/Englisch-second und CEFR-B2 gelten für lernende Inhalte.
- Wesentliche Bedeutung bleibt in Screenreader-, Braille- und Textbrowser-
  Pfaden erhalten.
- Fokus, Shortcuts, High Contrast und textbasierte Zustandsrückmeldung werden
  entsprechend der tatsächlichen Control-Nutzung geprüft.

### 8.6 Plattform- und Terminalgrenzen

- Kleine Terminals führen zu stabiler Anpassung oder ehrlichem Fallback.
- Unicode, Charset, Breite, Farbe und Terminalfähigkeiten werden nicht
  pauschal behauptet.
- Windows, macOS, Linux und begründete WSL-Grenzen werden korrekt benannt.
- Datei- und persistente Beispiele verwenden kontrollierte Fixtures oder
  test-eigene temporäre Verzeichnisse.

## 9. Finding-Vertrag

Findings beginnen bei `EF001`. Jedes Finding enthält mindestens:

`FindingId`, `ExampleIds`, `Dimension`, `Observation`, `Reproduction`,
`HistoricalIntent`, `CurrentBehavior`, `SourceRelations`, `MissingBehaviorOrProof`,
`Risk`, `PrimaryOwner`, `Dependencies`, `RequiredRedProof`,
`RequiredRealPathGreenProof`, `APIImpact`, `A11YImpact`, `PlatformImpact`,
`EvidencePath`, `Owner`, `Reviewer`, `ReviewDate`, `ResidualRisk`,
`ReevaluationTrigger` und `DeduplicationKey`.

Erlaubte Primary Owner:

| Owner | Grenze |
|---|---|
| `FrameworkReuse` | Fehlende oder umgangene wiederverwendbare Framework-Verantwortung |
| `BehaviorInteraction` | Beispielverhalten, Bedienung, Fokus, Command oder sichtbare Rückmeldung |
| `ProofPlatform` | Real-Path-Proof, Fallback, Terminal- oder Plattformnachweis |
| `LearningA11Y` | Guide, Lernwert, text-first Accessibility oder didaktische Konsistenz |

Ein Finding besitzt genau einen Primary Owner. Querschnittswirkungen werden
als sekundäre Auswirkungen dokumentiert. Gleiche Ursachen über mehrere
Beispiele werden über `DeduplicationKey` zu einem Finding zusammengeführt.

Kein Finding entsteht allein aus:

- anderem Aussehen oder Layout;
- anderer Typ-, Methoden- oder Dateibenennung;
- fehlender C++-, Pascal- oder Terminal.GUI-Strukturparität;
- zusätzlicher Funktion einer Vergleichsquelle;
- moderner C#-Syntax, Records, verwaltetem Speicher oder anderen
  idiomatischen Sprachentscheidungen;
- persönlicher Stilpräferenz ohne beobachtbare Nutzer-, Lern- oder
  Proof-Auswirkung.

## 10. Folge-Lastenhefte

Nach vollständiger Auditentscheidung:

1. erhält jede nicht leere Owner-Gruppe genau ein Remediation-Lastenheft;
2. werden die Owner-Gruppen dependency-geordnet;
3. werden leere Gruppen vollständig unterdrückt;
4. folgt danach genau ein unabhängiger Example-Portfolio-Closure.

Die Nummerierung beginnt mit der nach dem Audit nächsten freien
Spec-Kit-Feature-Nummer. Das Lastenheft darf keine Nummern vorwegnehmen.

Der Closure prüft alle Findings, Beispielzeilen, Guides, Smokes, Plattformen,
Accessibility und Portfolio-Gates erneut. Erst der Closure darf das
Beispielportfolio als vollständig konform und lernreif markieren.

## 11. Evidence-Artefakte

Der spätere Feature-Lauf erzeugt mindestens:

- `example-portfolio-source-manifest`
- `example-portfolio-inventory`
- `example-conformance-matrix`
- `example-framework-usage-review`
- `example-proof-and-platform-review`
- `example-learning-a11y-review`
- `example-portfolio-findings`
- `example-remediation-handoff`
- `example-portfolio-gate`
- `pr-evidence`

JSON ist für maschinenlesbare Relationen und Integritätsprüfung verbindlich;
Markdown liefert die reviewbare, text-first Darstellung. Source- und
Evidence-Pfade werden bidirektional geprüft.

## 12. Governance und Preset-Lernzyklus

Der Audit bewertet die installierte Acht-Preset-Standardmatrix und das aktive
optionale Intake-Review-Preset proportional.
Runtime-, Dependency-, Cloud-, verteilte System- und Supply-Chain-Trigger
bleiben `N/A`, solange der tatsächliche Diff rein auditbezogen bleibt.

Nach dem Audit und jedem Finding-Lauf wird die autonome Retrospektive
ausgeführt. Nur reproduzierbare providerneutrale Erkenntnisse werden als
`PresetFollowUp` nach Home Baseline übergeben. `NoPromotion` erzeugt keinen
Branch, PR oder Release.

Remote-Rechte, Merge, Bypass und Upstream-Kommunikation werden niemals aus
diesem Lastenheft abgeleitet. Sie benötigen aktuelle ausdrückliche Autorität.

## 13. Validierung

Der spätere Audit muss mindestens nachweisen:

1. `specify check`, Voraussetzungen und vollständige Checklists
2. Clarify-, Plan-, Tasks- und Analyze-Konvergenz ohne offene hohe Findings
3. vollständige Portfolio-Mengenprüfung
4. genau eine `PortfolioRole` und `PrimaryDisposition` je Beispiel
5. genau einen Status je Prüfdimension
6. vollständige Source-, Guide-, Smoke-, Framework- und Proof-Relationen
7. genau einen Primary Owner je Finding
8. vollständige Deduplizierung und dependency-geordnete Folge-Lastenhefte
9. `git diff --check` und `dotnet format --verify-no-changes`
10. targeted Integritätsvalidatoren und vollständige Release-Tests
11. kanonisches Coverage-Gate bei gemeinsamer Testinfrastruktur
12. DocFX, Playwright/Axe und UTF-8-Lynx
13. Secret-, Dependency-, Agent-Paritäts- und Generated-Output-Scans
14. null Diff unter Runtime-, Beispiel- und historischen Source-Wurzeln
15. exakte Reviewed-HEAD-Evidence vor Merge

Vor jedem Build oder Test gilt die Build-Counter-Regel.

## 14. Akzeptanzkriterien

1. Alle 25 Originalbeispiele sind genau einmal inventarisiert.
2. Alle tatsächlich gelieferten Wave-5- und Wave-6-Beispiele sind genau einmal
   inventarisiert.
3. `A11yFramework` ist genau einmal als `SupplementalControl` enthalten.
4. Jedes Beispiel besitzt eine vollständige Matrixzeile und genau eine
   Hauptentscheidung.
5. Jede Gap-Entscheidung verweist auf ein Finding oder einen expliziten
   `ProductDecision`-Stop.
6. Alle Findings sind dedupliziert und besitzen genau einen Primary Owner.
7. Nur nicht leere Owner-Gruppen erzeugen Remediation-Lastenhefte.
8. Genau ein unabhängiger Portfolio-Closure folgt zuletzt.
9. Der Audit-Diff enthält keine Runtime-, API-, Dependency-, Beispiel- oder
   historische Quellenänderung.
10. Moderne idiomatische C#-Entscheidungen bleiben erhalten, sofern keine
    reproduzierbare Vertrags-, Lern-, A11Y-, Plattform- oder Proof-Lücke
    besteht.
11. Nach Merge sind lokales `main` und `origin/main` sauber und identisch.

## 15. Stop-Grenzen

Der Lauf stoppt bei:

- unvollständigem Wave-5- oder Wave-6-Closeout;
- nicht eindeutigem Portfolio-Inventar;
- notwendiger Breaking- oder destruktiver Produktentscheidung;
- nicht reproduzierbarem Finding;
- unklarer Primary-Owner-Zuordnung;
- Versuch, Findings im Audit selbst zu implementieren;
- Änderung oder Kopie historischer oder externer Quellen;
- Versuch, visuelle oder Quelltextparität als Abnahmekriterium zu erzwingen;
- nicht behebbarer Evidence-, Security- oder Validierungsintegrität.

## 16. Kopierbarer `/speckit-specify`-Prompt

```text
Ersetzter Alt-Prompt: speckit-specify Create the feature specification for the binding intake
`Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md`.

Start only after Wave 6 and its independent closeout are fully merged and
local main is clean and synchronized. Assign the next free Spec Kit feature
number at that time; do not reuse the Lastenheft number as a feature number.

This is a read-only example portfolio, learning, framework-usage, and proof
audit. Preserve modern idiomatic C#. Do not change runtime behavior, public
APIs, dependencies, examples, tests except audit-integrity validators, or any
historical or external source.

Inventory all 25 original Wave-1-to-Wave-4 examples, every actually delivered
Wave-5 and Wave-6 example, and A11yFramework as a SupplementalControl without
historical conformance scoring. Review historical purpose, learning value,
framework reuse, visible interaction, real app-loop proof, view-tree and
buffer/cell proof, documentation, A11Y, terminal limits, and platform
fallbacks.

Use TV203 and the matching TP7 sources as historical authority. Use accepted
TuiVision behavior as product semantics. Use the pinned Free Vision,
Terminal.GUI v1.9.0, and magiblot/tvision evidence only as secondary
implementation opinions. Structural, visual, API, inheritance, or source-text
difference alone is not a finding.

Give every example exactly one PortfolioRole and PrimaryDisposition. Use
Pass, IntentionalDeviation, Gap, or N/A for every required dimension. Create
deduplicated EF001+ findings only for reproducible TuiVision behavior,
framework-reuse, proof/platform, learning, or A11Y gaps. ProductDecision stops
the run.

Generate only non-empty, dependency-ordered remediation Lastenhefte grouped
by FrameworkReuse, BehaviorInteraction, ProofPlatform, or LearningA11Y,
followed by exactly one independent portfolio closure.
```

## 17. Kopierbarer autonomer Intake-Prompt

```text
Ersetzter Alt-Prompt: speckit-autonomous Use
`Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` as the
binding intake in MergeAndSync mode.

Start only from clean synchronized main after Feature 030, all combined
finding-derived hardening, the independent pre-Wave-5 closure, Wave 5 and its
closeout, and Wave 6 and its closeout are fully merged. Determine and use the
next free Spec Kit feature number. Do not infer it from Lastenheft number 15.

Execute the complete autonomous Spec Kit lifecycle and all useful optional
passes to convergence. Keep the accepted scope read-only toward runtime,
public APIs, dependencies, examples, and historical or external sources.
Audit-integrity test validators are allowed; product or example fixes are not.

Inventory and review every delivered historical example plus A11yFramework as
a SupplementalControl. Apply the binding source hierarchy and complete every
matrix field, dimension status, primary disposition, evidence relation, and
governance decision. Compare observable purpose, behavior, framework use,
learning value, A11Y, platform boundaries, and proof quality; never require
visual, API, inheritance, layout, or source-text parity.

Deduplicate all real gaps into EF001+ findings with exactly one Primary Owner.
Stop on ProductDecision. Generate no empty feature, branch, Lastenheft, or PR.
Create dependency-ordered remediation Lastenhefte only for non-empty owner
groups, followed by exactly one independent portfolio closure.

Complete local validation, exact reviewed-head evidence, PR review, merge,
branch cleanup, main synchronization, and the autonomous retrospective under
current explicit remote authority. Promote reusable preset learning only
through the documented Home Baseline release and exact tag-ZIP adoption cycle.
```
<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

Die folgenden Alternativen starten keinen Lauf automatisch. Der autonome
Prompt ist auf `LocalImplementation` begrenzt und erteilt keine Remote-,
PR-, Merge-, Bypass-, Secret- oder Provider-Berechtigung.

*The alternatives below do not start a run automatically. The autonomous
prompt is limited to `LocalImplementation` and grants no remote,
pull-request, merge, bypass, secret, or provider authority.*

### Specify

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md as the binding intake. Preserve its scope, non-goals, ordering, governance, evidence, and acceptance criteria. Create or update only the matching feature specification. Do not implement, commit, push, create a pull request, merge, or start another feature.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run using Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md as the binding intake. Delivery mode: LocalImplementation. Preserve all scope, ordering, security, accessibility, evidence, and acceptance boundaries. Do not push, create or merge a pull request, use bypass authority, expose secrets, or start a follow-up feature.
```
<!-- intake-authoring:end -->
