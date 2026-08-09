# Feature Specification: Beispielportfolio-Konformitätsaudit / Example Portfolio Conformance Audit

**Feature Branch**: `038-example-portfolio-conformance-audit`
**Created**: 2026-08-09
**Status**: Clarified
**Binding Intake**: `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md`
**Delivery Mode**: `MergeAndSync` (current resumed-run authority; the accepted read-only product scope is unchanged)
**Portfolio Baseline**: 37 entries: 25 original Wave-1-to-Wave-4 examples, 10 delivered Wave-5 examples, 1 delivered Wave-6 example, and `A11yFramework` as `SupplementalControl`

## Klärungen / Clarifications

### Session 2026-08-09

- Keine formale Rückfrage ist erforderlich. Der hashgleiche bindende Intake,
  die `Ready`-Review-Evidence, der `Eligible`-Serienstatus, der Abschluss von
  Feature 037, die Requirements-Checklist und die Verfassung legen alle
  planungs-, task-, audit-, akzeptanz- und stopwirksamen Entscheidungen fest.
- Die Grundmenge bleibt exakt bei 37 benannten Einträgen und gegen Drift
  gesperrt. `N/A` ist auch für die historische Relation von `A11yFramework`
  der kanonische Nicht-anwendbar-Status; ein zusätzlicher Alias
  `NotApplicable` wird nicht eingeführt.
- Findings bleiben lückenlos `EF001+`, ursachenbezogen dedupliziert und genau
  einem Primary Owner zugeordnet. Nur nicht leere Owner-Gruppen erzeugen
  dependency-geordnete, unnummerierte Remediation-Lastenhefte; danach folgt
  genau ein unabhängiger Closure.
- Quellenhierarchie und read-only Scope bleiben unverändert. Die damalige
  `LocalImplementation`-Autorität galt für diesen Clarify-Pass; die aktuelle
  Resume-Autorität wurde später ausdrücklich auf `MergeAndSync` erweitert.
  Weder Autorität startet ein Folgefeature.

*No formal question is required. The accepted, hash-bound evidence fixes every
decision that could materially affect planning, task decomposition, audit
evidence, acceptance, or stop boundaries. The exact 37-entry baseline,
canonical status and disposition vocabularies, finding ownership and
deduplication, non-empty follow-up rule, independent closure, source hierarchy,
and read-only scope remain unchanged. The clarification pass used
LocalImplementation; the resumed run now has explicit MergeAndSync authority.*

## Zielgruppe und Lernkontext / Audience and Learning Context

Der Audit richtet sich an Auszubildende ab dem ersten Ausbildungsjahr,
Maintainer, Reviewer und künftige Remediation-Verantwortliche. Ein Finding ist
eine reproduzierbare Lücke; eine Disposition ist die eindeutige Hauptentscheidung
für eine Portfoliozeile. Begriffe, Zustände, Abhängigkeiten, Entscheidungen und
nächste Schritte müssen bei der ersten Verwendung erklärt werden. Deutsch ist
die Primärsprache; Englisch folgt direkt. Vorwissen zu Spec Kit wird nicht
vorausgesetzt.

*The audit serves first-year apprentices, maintainers, reviewers, and future
remediation owners. A finding is a reproducible gap; a disposition is the one
primary decision for a portfolio row. Terms, states, dependencies, decisions,
and next actions must be explained on first use. German is primary and English
follows directly. Prior Spec Kit knowledge is not assumed.*

## Bindende Quellenhierarchie / Binding Source Hierarchy

1. Borland-Dokumentation und die read-only Quellen unter `tv203s/` bestimmen
   die historische Absicht der 25 Originalbeispiele.
2. Die read-only Turbo-Pascal-Quellen unter `TVDEMOS/` und `TVFM/` bestimmen
   die historische Absicht der tatsächlich gelieferten Waves 5 und 6.
3. Akzeptierte TuiVision-Verträge, Public API, idiomatisches modernes C# und
   nachgewiesenes Nutzerverhalten bestimmen die aktuelle Produktsemantik.
4. Das in Feature 024 gepinnte Free Vision ist eine unabhängige, Pascal-nahe
   Architektur- und Implementierungsmeinung.
5. Terminal.GUI v1.9.0 wird ausschließlich über die akzeptierte
   Feature-029-Evidence als alternative moderne C#-Meinung verwendet.
6. Das in Feature 030 gepinnte `magiblot/tvision` ist ein direkter
   C++-Modernisierungszeuge derselben Abstammungslinie.

Keine nachgeordnete Quelle überschreibt historische Absicht oder akzeptierte
TuiVision-Produktsemantik. Übereinstimmung mit einer Vergleichsquelle beweist
keine Richtigkeit. Abweichung ist ohne reproduzierbare TuiVision-Lücke kein
Finding. Die akzeptierten Pins aus Features 024, 029 und 030 werden unverändert
wiederverwendet; bewegliche Upstream-Branches und neuere Releases sind
ausgeschlossen.

*Lower-ranked sources never override historical intent or accepted TuiVision
product semantics. Agreement with a comparison source is not proof of
correctness, and divergence is not a finding without a reproducible TuiVision
gap. Accepted pins are reused unchanged; moving upstream revisions are out of
scope.*

## Verbindliche Portfolio-Grundmenge / Binding Portfolio Baseline

| Gruppe | Anzahl | Eindeutige Namen |
|---|---:|---|
| Originalbeispiele, Waves 1-4 | 25 | `BHelp`, `Clipboard`, `Cyrillic`, `Demo`, `Desklogo`, `DlgDsn`, `DynTxt`, `ETerm`, `Fonts`, `HelpDemo`, `I18n`, `InpLis`, `ListVi`, `MsgCls`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, `TProgB`, `Terminal`, `Tutorial`, `TvEdit`, `TvHc`, `Videomode`, `XTerm` |
| Gelieferte Wave-5-Beispiele | 10 | `Tp7AsciiTable`, `Tp7Calculator`, `Tp7Calendar`, `Tp7Demo`, `Tp7Edit`, `Tp7Help`, `Tp7MouseDialog`, `Tp7Puzzle`, `Tp7ResourceDemo`, `Tp7ResourceGenerator` |
| Geliefertes Wave-6-Beispiel | 1 | `Tp7FileManager` |
| Projektspezifische Kontrolle | 1 | `A11yFramework` als `SupplementalControl` |

Diese Namen bestimmen die aktuelle Audit-Grundmenge. Eine spätere Änderung
der gelieferten Beispielprojekte blockiert als Portfolio-Drift und wird nicht
stillschweigend in dieselbe Baseline aufgenommen.

*These names define the current audit population. A later change to delivered
example projects is blocking portfolio drift and is not silently absorbed into
the same baseline.*

## User Scenarios & Testing / Nutzungsszenarien und Prüfung

### User Story 1 - Portfolio vollständig und einheitlich inventarisieren (Priority: P1)

Als Maintainer möchte ich jedes gelieferte Beispiel genau einmal in derselben
Portfolio-Matrix sehen, damit Umfang, Lernzweck, Framework-Nutzung, Bedienung,
Proof, Dokumentation, Accessibility und Plattformgrenzen vollständig
vergleichbar sind.

*As a maintainer, I want every delivered example represented exactly once in
one consistent portfolio matrix so its scope, learning intent, framework use,
interaction, proof, documentation, accessibility, and platform boundaries can
be compared completely.*

**Why this priority**: Ohne vollständige und eindeutige Grundmenge sind weder
Konformitätsentscheidungen noch Finding-Deduplizierung belastbar.

**Independent Test**: Ein Integritätsreview bestätigt genau 37 eindeutige
Einträge: 25 Originalbeispiele, 10 Wave-5-Beispiele, 1 Wave-6-Beispiel und
`A11yFramework` genau einmal als `SupplementalControl`.

**Acceptance Scenarios**:

1. **Given** das auf Feature 037 folgende gelieferte Beispielportfolio,
   **When** das Inventar geprüft wird, **Then** ist jedes erwartete Beispiel
   genau einmal enthalten und kein unbekanntes Beispiel wird als historisches
   Portfolioelement gewertet.
2. **Given** eine historische Portfoliozeile, **When** ihre Pflichtfelder
   geprüft werden, **Then** besitzt sie genau eine `PortfolioRole`, eine
   `PrimaryDisposition` und je Dimension genau einen erlaubten Status.
3. **Given** `A11yFramework`, **When** die historische Relation bewertet wird,
   **Then** ist seine Rolle `SupplementalControl`, seine historische Relation
   `N/A`, und eine begründete Learning-, A11Y- oder Proof-Lücke darf
   dennoch ein Finding auslösen.

---

### User Story 2 - Historischen Zweck und moderne Produktsemantik bewerten (Priority: P1)

Als Reviewer möchte ich den ursprünglichen Demonstrations- und Lernzweck jedes
Beispiels gegen sein heutiges sichtbares Verhalten und die akzeptierte
TuiVision-Semantik prüfen, damit moderne idiomatische Entscheidungen erhalten
bleiben und reale Zweckverluste sichtbar werden.

*As a reviewer, I want each example's original demonstration and learning
purpose assessed against its current visible behavior and accepted TuiVision
semantics so idiomatic modernization remains intact while real intent loss is
identified.*

**Why this priority**: Der Audit prüft beobachtbare Verantwortung, nicht
Quelltext-, Typ-, Layout- oder Vererbungsidentität.

**Independent Test**: Jede Zeile enthält eine in eigenen Worten formulierte
historische Absicht, ein nachvollziehbares Lernziel, konkrete Source- und
Evidence-Relationen sowie eine begründete Hauptentscheidung.

**Acceptance Scenarios**:

1. **Given** eine historische Quelle und das moderne Beispiel, **When** der
   Kernzweck erhalten und belegt ist, **Then** ist `AcceptedAsIs` zulässig.
2. **Given** eine materielle moderne Abweichung, **When** sie dokumentiert und
   durch beobachtbares Verhalten oder Proof begründet ist, **Then** ist
   `AcceptedIntentionalDeviation` zulässig.
3. **Given** nur einen Unterschied in Aussehen, Benennung, API-Form,
   Vererbung, Speicherlayout oder Quelltext, **When** keine reproduzierbare
   Nutzer-, Lern-, A11Y-, Plattform-, Framework- oder Proof-Lücke besteht,
   **Then** entsteht kein Finding.
4. **Given** Free Vision, Terminal.GUI oder magiblot/tvision, **When** keine
   fachlich vergleichbare Verantwortung besteht, **Then** lautet die Relation
   `N/A` mit Begründung und Re-Evaluationsauslöser.

---

### User Story 3 - Framework-Nutzung, Bedienung und Real-Path-Proof prüfen (Priority: P1)

Als Framework-Maintainer möchte ich erkennen, ob ein Beispiel vorhandene
TuiVision-Komponenten nutzt oder wiederverwendbare Verantwortung lokal
nachbildet, und ob seine Hauptfunktionen sichtbar über den realen
Application-Loop erreichbar und beweisbar sind.

*As a framework maintainer, I want to know whether an example uses existing
TuiVision components or locally recreates reusable responsibility, and whether
its primary behavior is visibly reachable and proven through the real
application loop.*

**Why this priority**: Lokale Sonderlogik und Helper-only-Proofs können eine
scheinbar funktionierende Demo liefern, ohne den Framework- oder Nutzervertrag
zu erfüllen.

**Independent Test**: Jede Zeile erhält genau eine Framework-Entscheidung und
vollständige Interaktions-, Zustands-, View-Baum-, Buffer-/Cell-, Negativ- und
Fallback-Evidence entsprechend ihrem Risiko.

**Acceptance Scenarios**:

1. **Given** ein interaktiv gedachtes Beispiel, **When** es normal startet,
   **Then** sind Zweck, Hauptfunktion und sichtbare Rückmeldung über Menü,
   StatusLine, Tastatur, Command oder einen begründeten Mauspfad erreichbar.
2. **Given** ein relevanter Mauspfad, **When** die Bedienbarkeit geprüft wird,
   **Then** besitzt er einen dokumentierten Tastaturfallback.
3. **Given** ein Primary Proof, **When** seine Beweisgrenze geprüft wird,
   **Then** läuft er durch `app.Run()` oder den äquivalenten realen
   Application-Loop und kombiniert konkreten Zustand, View-Identität und
   sichtbaren Buffer-/Cell-Nachweis.
4. **Given** Editor-, Hilfe-, Ressourcen-, Datei-, Terminal-, Maus-,
   Drag-/Drop-, Persistenz- oder Mehrfensterverantwortung, **When** die
   Risikotiefe festgelegt wird, **Then** werden passende Negativ-, Rejection-,
   Safe-Close-, Fallback- und Small-Terminal-Pfade zusätzlich geprüft.

---

### User Story 4 - Findings reproduzierbar deduplizieren und disponieren (Priority: P1)

Als Produktverantwortlicher möchte ich ausschließlich reale, reproduzierbare
Lücken als deduplizierte `EF001+`-Findings mit genau einem Primary Owner sehen,
damit die spätere Remediation eindeutig, klein und dependency-geordnet bleibt.

*As the product owner, I want only real, reproducible gaps recorded as
deduplicated `EF001+` findings with exactly one primary owner so later
remediation stays clear, bounded, and dependency-ordered.*

**Why this priority**: Ein falsches oder doppeltes Finding erzeugt unnötige
Features; ein unklarer Product Decision darf nicht autonom entschieden werden.

**Independent Test**: Jede Gap-Dimension verweist auf ein vollständiges
Finding oder einen expliziten `ProductDecision`-Stop; gleiche Ursachen teilen
einen `DeduplicationKey` und genau einen Primary Owner.

**Acceptance Scenarios**:

1. **Given** dieselbe Ursache in mehreren Beispielen, **When** Findings
   dedupliziert werden, **Then** entsteht genau ein Finding mit allen
   betroffenen `ExampleIds`.
2. **Given** ein Finding, **When** sein Vertrag geprüft wird, **Then** besitzt
   es genau einen Owner aus `FrameworkReuse`, `BehaviorInteraction`,
   `ProofPlatform` oder `LearningA11Y` sowie Red- und Real-Path-Green-Proof.
3. **Given** eine notwendige Breaking-, destruktive oder fachlich nicht
   autonome Entscheidung, **When** sie erkannt wird, **Then** erhält die
   betroffene Zeile `ProductDecision` und der Lauf stoppt.
4. **Given** eine Beobachtung ohne reproduzierbare TuiVision-Lücke, **When**
   sie geprüft wird, **Then** wird sie begründet verworfen und nicht als
   Finding oder Follow-up weitergegeben.

---

### User Story 5 - Nur notwendige Folgearbeit und einen Closure ableiten (Priority: P1)

Als Projektverantwortlicher möchte ich nur aus nicht leeren Owner-Gruppen
spätere Remediation-Lastenhefte und danach genau einen unabhängigen
Portfolio-Closure erhalten, damit keine leeren Features entstehen und nur der
Closure die endgültige Portfolio-Konformität erklären darf.

*As the project owner, I want later remediation intakes generated only for
non-empty owner groups, followed by exactly one independent portfolio closure,
so no empty features are created and only the closure may declare final
portfolio conformance.*

**Why this priority**: Der Audit dokumentiert und ordnet Lücken, behebt sie
aber nicht selbst.

**Independent Test**: Der Handoff enthält genau eine dependency-geordnete
Zeile je nicht leerer Owner-Gruppe, unterdrückt jede leere Gruppe und benennt
anschließend genau einen unabhängigen Closure-Intake ohne vorweggenommene
Feature-Nummern oder gestartete Features.

**Acceptance Scenarios**:

1. **Given** eine leere Owner-Gruppe, **When** der Handoff erzeugt wird,
   **Then** entstehen dafür weder Lastenheft noch Branch, PR oder Feature.
2. **Given** eine oder mehrere nicht leere Owner-Gruppen, **When** ihre
   Abhängigkeiten geordnet werden, **Then** erhält jede Gruppe genau ein
   Remediation-Lastenheft in topologischer Reihenfolge.
3. **Given** alle nicht leeren Owner-Gruppen, **When** der Handoff vollständig
   ist, **Then** folgt zuletzt genau ein unabhängiger
   Example-Portfolio-Closure.
4. **Given** das Ende dieses `MergeAndSync`-Laufs, **When** der nächste Schritt
   bestimmt wird, **Then** darf Feature 038 geliefert und synchronisiert sein,
   aber kein Remediation-, Closure- oder sonstiges Folgefeature automatisch
   gestartet werden.

### Edge Cases / Randfälle

- Ein Beispielprojekt fehlt, erscheint doppelt oder wird der falschen Welle
  beziehungsweise Portfolio-Rolle zugeordnet.
- Ein Wave-5- oder Wave-6-Beispiel ist geliefert, aber in einem älteren
  statischen Portfolio-Snapshot nicht enthalten.
- `A11yFramework` wird fälschlich historisch gewertet oder wegen `N/A`
  vollständig aus der Lern-/A11Y-Prüfung ausgeschlossen.
- Eine Matrixzeile besitzt mehrere Hauptentscheidungen, mehrere Statuswerte
  pro Dimension oder ein unbegründetes `N/A`.
- Eine Vergleichsquelle wird als Produktnorm oder als alleiniger
  Richtigkeitsbeweis verwendet.
- Historischer Zweck ist nur aus zusammengehörigen Implementierungs- und
  Headerdateien verständlich; eine notwendige Source-Relation fehlt.
- Ein Helper wird als Primary Proof eingestuft, obwohl er den realen
  Application-Loop oder die sichtbare Ausgabe umgeht.
- Eine lokale Sonderlogik ersetzt eine wiederverwendbare Framework-Aufgabe,
  wird aber ohne `SmallFrameworkFix` oder `FollowUpHardening` akzeptiert.
- Ein `Gap` besitzt weder Finding noch `ProductDecision`-Stop.
- Mehrere Beispiele haben dieselbe Ursache, erzeugen aber verschiedene
  `DeduplicationKey`-Werte oder mehrere Primary Owner.
- Ein Finding ist nicht reproduzierbar oder besitzt keine kontrollierte
  Red-/Green-Proof-Grenze.
- Eine leere Owner-Gruppe erzeugt Folgearbeit oder ein Lastenheft nimmt eine
  noch nicht zugewiesene Feature-Nummer vorweg.
- Eine Datei- oder Persistenzprüfung liest beliebige Nutzerdaten statt
  kontrollierter Fixtures oder test-eigener temporärer Verzeichnisse.
- Small-Terminal-, Unicode-, Charset-, Farb- oder Plattformfähigkeit wird
  pauschal behauptet statt mit Evidence oder ehrlichem Fallback belegt.
- Ein Reviewer fehlt; dies wird als fehlender Review und nicht als
  erfolgreicher Review erfasst.
- Ein Audit-Diff berührt Runtime, Public API, Dependencies, Beispiele,
  historische oder externe Quellen.
- Der Autonomous-State verliert Routing-Metadaten oder behauptet Remote-
  Authority, Merge oder einen gestarteten Folge-Run.

## Requirements / Anforderungen

### Functional Requirements / Funktionale Anforderungen

- **FR-001**: Das Feature MUSS den bindenden Intake und die im
  `autonomous-run-state.json` hashgebunden akzeptierten Review-, Serien- und
  Receipt-Artefakte als unveränderliche Eingabe behandeln.
- **FR-002**: Das Feature MUSS den aus Feature 037 belegten Zustand Wave 6
  `Closed`, Portfolioaudit `Eligible`, null Candidate Findings und null Product
  Decisions als Eintrittsgrenze verwenden.
- **FR-003**: Der Audit MUSS genau 37 Portfolioeinträge inventarisieren: alle
  25 Originalbeispiele der Waves 1 bis 4, alle 10 tatsächlich gelieferten
  Wave-5-Beispiele, das tatsächlich gelieferte Wave-6-Beispiel und
  `A11yFramework`.
- **FR-004**: Jedes historische Beispiel MUSS genau die Rolle
  `HistoricalExample` erhalten; `A11yFramework` MUSS genau die Rolle
  `SupplementalControl` erhalten.
- **FR-005**: Jede Portfoliozeile MUSS mindestens folgende Felder enthalten:
  `ExampleId`, `Name`, `PortfolioRole`, `Wave`, `HistoricalSourceIds`,
  `ModernizationSourceIds`, `OriginalIntent`, `LearningGoal`,
  `CurrentEntryPoint`, `VisibleFirstScreen`, `PrimaryInteractionPaths`,
  `FrameworkComponents`, `LocalSpecialLogic`, `FrameworkDecision`,
  `BehaviorStatus`, `InteractionStatus`, `ProofStatus`,
  `DocumentationStatus`, `A11YStatus`, `PlatformStatus`,
  `HistoricalRelation`, `FreeVisionRelation`, `TerminalGuiRelation`,
  `MagiblotRelation`, `PrimaryDisposition`, `FindingIds`, `EvidencePath`,
  `Owner`, `Reviewer`, `ReviewDate`, `ResidualRisk` und
  `ReevaluationTrigger`.
- **FR-006**: Jede Prüfdimension MUSS genau einen Wert aus `Pass`,
  `IntentionalDeviation`, `Gap` oder `N/A` besitzen; `N/A` MUSS Begründung und
  Re-Evaluationsauslöser enthalten.
- **FR-007**: Jede Portfoliozeile MUSS genau eine `PrimaryDisposition` aus
  `AcceptedAsIs`, `AcceptedIntentionalDeviation`, `CandidateFinding` oder
  `ProductDecision` besitzen.
- **FR-008**: Die Hauptentscheidung DARF die einzelnen Dimensionsentscheidungen
  NICHT ersetzen oder verdecken.
- **FR-009**: Der Audit MUSS die bindende sechs Stufen umfassende
  Quellenhierarchie unverändert anwenden und die akzeptierten Pins aus
  Features 024, 029 und 030 wiederverwenden.
- **FR-010**: Bewegliche Upstream-Branches, neuere Vergleichsreleases oder
  ungepinnte externe Quellen DÜRFEN NICHT in den Audit einfließen.
- **FR-011**: Historische Zweck- und Lernzusammenfassungen MÜSSEN in eigenen
  Worten formuliert werden; externe Quelltexte DÜRFEN NICHT kopiert,
  mechanisch übersetzt, vendoriziert oder verändert werden.
- **FR-012**: Free Vision, Terminal.GUI und magiblot/tvision DÜRFEN nur als
  sekundäre Meinung bei fachlich vergleichbarer Verantwortung verwendet
  werden.
- **FR-013**: Übereinstimmung oder Abweichung zu einer Vergleichsquelle DARF
  ohne zusätzliche reproduzierbare TuiVision-Evidence KEIN Finding begründen.
- **FR-014**: Jedes Beispiel MUSS genau eine Framework-Entscheidung aus
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening` besitzen. `UseExistingFramework` bedeutet, dass die
  vorhandene wiederverwendbare TuiVision-Verantwortung ausreicht;
  `SmallFrameworkFix` bezeichnet eine kleine reproduzierbare Framework-Lücke;
  `IntentionalDeviation` akzeptiert eine begründete moderne Abweichung ohne
  wiederverwendbare Framework-Lücke; `FollowUpHardening` bezeichnet eine
  größere oder gesondert zu planende Framework-Härtung.
- **FR-015**: `SmallFrameworkFix` und `FollowUpHardening` MÜSSEN im Audit
  Finding-Entscheidungen bleiben und DÜRFEN NICHT in diesem Feature umgesetzt
  werden.
- **FR-016**: Wiederverwendbare Verantwortung DARF NICHT als dauerhaft
  akzeptierte lokale Beispielsonderlösung verbleiben.
- **FR-017**: Interaktiv gedachte Beispiele MÜSSEN Zweck, Hauptbedienung und
  sichtbare Rückmeldung über ihre tatsächlichen Bedienpfade nachweisen.
- **FR-018**: `Help -> Description` oder eine gleichwertige text-first
  Erklärung MUSS tastaturerreichbar sein; relevante Mausoperationen MÜSSEN
  einen Tastaturfallback besitzen.
- **FR-019**: Primary Proof MUSS `app.Run()` oder den äquivalenten realen
  Application-Loop verwenden und konkreten Zustand, View-Identität sowie
  sichtbaren Buffer-/Cell-Nachweis kombinieren.
- **FR-020**: Direkte Helper MÜSSEN als `SupplementalProof`, `SetupOnly` oder
  nach dem bestehenden Helper-Vertrag begründet als `PrimaryProof`
  klassifiziert werden.
- **FR-021**: Negative, Rejection-, Safe-Close-, Fallback- und
  Small-Terminal-Pfade MÜSSEN risikoproportional geprüft werden, wenn sie zur
  Verantwortung des Beispiels gehören.
- **FR-022**: Jedes Beispiel MUSS einen Guide nach Pflichtenheft Abschnitt
  10.4 besitzen; Guide, sichtbare Anwendung, Lernziel, Fehlerbilder, Übungen,
  Quellverweise und Testverweise MÜSSEN übereinstimmen.
- **FR-023**: Lernende und reviewende Inhalte MÜSSEN German-first/
  English-second, ungefähr CEFR-B2, text-first und für das erste
  Ausbildungsjahr verständlich sein.
- **FR-024**: Wesentliche Bedeutung, Status, Abhängigkeiten, Entscheidungen
  und nächste Schritte MÜSSEN für Tastatur-, Screenreader-, Braille- und
  Textbrowserpfade ohne reine Farb-, Diagramm-, Layout- oder Positionsaussage
  verfügbar bleiben.
- **FR-025**: Fokus, Shortcuts, High Contrast, textbasierte Rückmeldung,
  kleine Terminals und ehrliche Plattformfallbacks MÜSSEN entsprechend der
  tatsächlichen Control- und Plattformnutzung bewertet werden.
- **FR-026**: Datei- und persistente Beispiele DÜRFEN für Proof nur
  kontrollierte Fixtures oder test-eigene temporäre Verzeichnisse verwenden.
- **FR-027**: Findings MÜSSEN lückenlos bei `EF001` beginnen und mindestens
  enthalten: `FindingId`, `ExampleIds`, `Dimension`, `Observation`,
  `Reproduction`, `HistoricalIntent`, `CurrentBehavior`, `SourceRelations`,
  `MissingBehaviorOrProof`, `Risk`, `PrimaryOwner`, `Dependencies`,
  `RequiredRedProof`, `RequiredRealPathGreenProof`, `APIImpact`, `A11YImpact`,
  `PlatformImpact`, `EvidencePath`, `Owner`, `Reviewer`, `ReviewDate`,
  `ResidualRisk`, `ReevaluationTrigger` und `DeduplicationKey`.
- **FR-028**: Jedes Finding MUSS genau einen Primary Owner aus
  `FrameworkReuse`, `BehaviorInteraction`, `ProofPlatform` oder
  `LearningA11Y` besitzen; Querschnittswirkungen MÜSSEN als sekundäre
  Auswirkungen dokumentiert werden. `FrameworkReuse` verantwortet fehlende
  oder umgangene wiederverwendbare Framework-Funktion, `BehaviorInteraction`
  Beispielverhalten, Bedienung, Fokus, Commands oder sichtbare Rückmeldung,
  `ProofPlatform` Real-Path-Proof, Fallback, Terminal- oder Plattformnachweis
  und `LearningA11Y` Guide, Lernwert, text-first Accessibility oder
  didaktische Konsistenz. Das separate Feld `Owner` benennt die für Bearbeitung
  oder Review verantwortliche Rolle oder Person und ändert den Primary Owner
  nicht.
- **FR-029**: Gleiche Ursachen über mehrere Beispiele MÜSSEN über
  `DeduplicationKey` zu genau einem Finding zusammengeführt werden.
- **FR-030**: Jede `Gap`-Entscheidung MUSS auf ein Finding oder einen
  expliziten `ProductDecision`-Stop verweisen.
- **FR-031**: Persönliche Stilpräferenz sowie Unterschiede in Aussehen,
  Layout, Typen, Methoden, Dateien, API-Form, Vererbung, Speicherlayout,
  Sprachsyntax oder Quelltext DÜRFEN allein KEIN Finding erzeugen.
- **FR-032**: Ein `ProductDecision`, ein nicht reproduzierbares Finding, ein
  unklares Portfolio, eine unklare Owner-Zuordnung oder nicht behebbare
  Evidence-, Security- oder Validierungsintegrität MUSS den Lauf blockieren.
- **FR-033**: Eine Owner-Gruppe gilt genau dann als nicht leer, wenn ihr
  mindestens ein finales, dedupliziertes Finding über dessen `PrimaryOwner`
  zugeordnet ist. Jede nicht leere Owner-Gruppe MUSS genau ein späteres
  Remediation-Lastenheft mit allen zugeordneten Finding-IDs, Abhängigkeiten und
  geforderten Red-/Real-Path-Green-Proofs erhalten; leere Owner-Gruppen MÜSSEN
  vollständig unterdrückt werden.
- **FR-034**: Remediation-Lastenhefte MÜSSEN dependency-geordnet sein und
  DÜRFEN keine künftigen Feature-Nummern vorwegnehmen.
- **FR-035**: Nach allen nicht leeren Remediation-Gruppen MUSS genau ein
  unabhängiger Example-Portfolio-Closure vorgesehen werden; nur dieser Closure
  darf das Portfolio später als vollständig konform und lernreif markieren.
- **FR-036**: Dieses Feature DARF kein Remediation- oder Closure-Feature
  starten, keinen Branch dafür anlegen und keine Findings beheben.
- **FR-037**: Der Audit MUSS mindestens folgende Evidence-Familien vorsehen:
  `example-portfolio-source-manifest`, `example-portfolio-inventory`,
  `example-conformance-matrix`, `example-framework-usage-review`,
  `example-proof-and-platform-review`, `example-learning-a11y-review`,
  `example-portfolio-findings`, `example-remediation-handoff`,
  `example-portfolio-gate` und `pr-evidence`.
- **FR-038**: Maschinenlesbare Relationen und Integritätsprüfung MÜSSEN JSON
  verwenden; Markdown MUSS eine vollständige reviewbare text-first Darstellung
  liefern; Source- und Evidence-Pfade MÜSSEN bidirektional prüfbar sein.
- **FR-039**: Teständerungen DÜRFEN ausschließlich Audit-Integrität für
  Inventar, Relationen, Entscheidungen, Deduplizierung, Reihenfolge und
  Folge-Lastenhefte validieren.
- **FR-040**: Der finale Audit-Diff MUSS null Änderungen unter `src/`,
  `examples/`, `tv203s/`, `TVDEMOS/` und `TVFM/` sowie null Public-API- oder
  Dependency-Änderungen enthalten.
- **FR-041**: Die Validierung MUSS `specify check`, vollständige Checklists,
  Clarify-/Plan-/Tasks-/Analyze-Konvergenz ohne offene hohe Findings,
  Portfolio-Mengenprüfung, Relations- und Deduplizierungsintegrität,
  `git diff --check`, Formatprüfung, targeted Integritätsvalidatoren,
  vollständige Release-Tests und das kanonische Coverage-Gate umfassen.
- **FR-042**: Wenn Dokumentation oder generierte API-Oberflächen betroffen
  sind, MÜSSEN DocFX, Playwright/Axe und UTF-8-Lynx nachgewiesen werden.
- **FR-043**: Secret-, Dependency-, Agent-Paritäts- und Generated-Output-Scans
  MÜSSEN aktuelle Ergebnisse oder einen begründeten nicht ausgelösten Status
  besitzen.
- **FR-044**: Vor jedem Build oder Test MUSS die repo-weite
  Build-Counter-Regel eingehalten werden.
- **FR-045**: Remote-, PR-, Merge-, Bypass-, Push-, Provider- und
  Upstream-Kommunikation DÜRFEN ausschließlich aus aktueller ausdrücklicher
  Autorität abgeleitet werden. Für den fortgesetzten Lauf ist
  `MergeAndSync` autorisiert; der eng begrenzte Admin-Bypass ist nur zulässig,
  wenn alle technischen Pflichtgates grün sind, null umsetzbare Review-Threads
  offen sind und ausschließlich die Human-Approval-Regel blockiert.
- **FR-046**: `autonomous-run-state.json` MUSS an Phasengrenzen konsistent
  bleiben, Routing-Metadaten erhalten und bei absichtlichem Stop,
  unerwarteter Unterbrechung oder Drift fail-closed auf explizite
  Revalidierung beziehungsweise Resume verweisen.
- **FR-047**: Nach Audit und späteren Finding-Läufen MUSS eine autonome
  Retrospektive nur reproduzierbare providerneutrale Erkenntnisse als
  `PresetFollowUp` klassifizieren; `NoPromotion` DARF keinen Branch, PR oder
  Release erzeugen.
- **FR-048**: Das Feature MUSS den formellen Portfolio-Gate-Status,
  Projektstatistik und die Agent-Paritätsentscheidung konsistent halten, ohne
  eine vollständige Konformität vor dem unabhängigen Closure zu behaupten.
- **FR-049**: Das bindende Merge-Akzeptanzkriterium verlangt nach
  autorisiertem Merge einen sauberen und identischen Zustand von lokalem
  `main` und `origin/main`. Der aktuelle `MergeAndSync`-Lauf MUSS dieses
  Kriterium ausführen und mit exakter Post-Merge-Evidence belegen.

### Constitution Requirements / Verfassungsanforderungen

- **CR-001**: Die TuiVision-Zeile im Level-2-Projektumgebungsregister ist
  bindend: .NET 10/C#, MSTest, Coverlet, DocFX, Playwright/Axe, text-first
  A11Y, Statistikprofil und gepflegte Agentenflächen.
- **CR-002**: C# ist die primäre, laut Verfassung memory-safe Sprache. Der
  Audit ändert keinen Produktcode; spätere test-only Validatoren müssen
  dennoch die sicheren C#/.NET-Regeln und explizite Eingabevalidierung
  anwenden.
- **CR-003**: NIST SSDF und CWE Top 25 sind für diesen Level-2-Lauf
  `Applicable`; sie schützen Scope, Eingaben, Evidence-Integrität,
  Deduplizierung und fail-closed Validierung.
- **CR-004**: OWASP ASVS ist `N/A`, weil keine Web-, HTTP-, API-, Auth- oder
  Autorisierungsfläche geändert wird. Re-Evaluation: Eine solche Fläche tritt
  in den tatsächlichen Diff ein.
- **CR-005**: SBOM, VEX und SLSA sind `N/A`, weil der Audit keine
  releasbaren Produktartefakte, Dependencies oder Build-Pipeline ändert.
  Re-Evaluation: Ein distributables Artefakt, eine Dependency- oder
  Pipelineänderung tritt in Scope.
- **CR-006**: AI-SBOM ist `N/A`, weil AI ausschließlich als Entwicklungs- und
  Agentenwerkzeug verwendet wird und kein Modell, Dienst, Datensatz,
  Inferenzsystem oder AI-Runtime-Baustein ausgeliefert oder betrieben wird.
  Re-Evaluation: Runtime- oder Produkt-AI tritt in Scope.
- **CR-007**: OpenSSF Scorecard ist `N/A`, weil keine neue oder aktualisierte
  externe Abhängigkeit und kein Upstream-Adoptionsentscheid getroffen wird.
  Re-Evaluation: Abhängigkeitsadoption oder Releasebewertung tritt in Scope.
- **CR-008**: NIS2, CRA, EU AI Act und DORA sind `N/A`, weil der reine Audit
  keine neue Markt-, Betreiber-, Cloud-, AI- oder Finanz-ICT-Rolle schafft.
  Re-Evaluation: Produktverteilung, regulierter Betrieb oder entsprechende
  Lieferkette ändert sich.
- **CR-009**: Neue oder geänderte STRIDE-/CIA-/CAPEC-Threat-Modelle, S-ADR,
  arc42-Sicherheitskonzepte, Zero Trust, BSI C3A und BSI C5 sind `N/A`, weil
  keine Trust Boundary, Produktarchitektur, Cloud-, Deployment- oder verteilte
  Servicegrenze geändert wird. Re-Evaluation: Eine solche Grenze tritt in den
  Diff ein.
- **CR-010**: OWASP SAMM erhält in diesem Feature keine neue
  Produkt-Evidence (`N/A`), weil weder Reifegradprogramm noch
  Entwicklungsprozess geändert wird. Re-Evaluation: Der Audit findet eine
  reproduzierbare prozessweite Governance-Lücke.
- **CR-011**: Neue Evidence unter `docs/security/` ist `N/A`; die
  featurebezogene Standards- und Security-Entscheidung wird in `spec.md`, den
  Audit-Evidence-Artefakten und `pr-evidence` geführt. Re-Evaluation: Ein
  Security-, Dependency-, Trust-Boundary- oder Release-Trigger tritt ein.
- **CR-012**: iSAQB-Architekturgovernance ist `Applicable` für
  Framework-Nutzung, wiederverwendbare Verantwortungen, Qualitätsgrenzen,
  Risiken und Technical-Debt-Handoffs. Neue allgemeine ADRs oder zusätzliche
  Dateien unter `docs/architecture/` sind `N/A`, solange der Audit keine
  Architekturentscheidung trifft; Re-Evaluation bei `ProductDecision` oder
  architektonisch signifikanter Remediation.
- **CR-013**: WCAG 2.2 AA ist für Audit-Markdown, spätere HTML-Dokumentation
  und Portfolio-A11Y-Bewertungen `Applicable`, besonders textuelle Struktur,
  nicht farbgebundene Bedeutung, verständliche Überschriften, Sprachwechsel,
  Tastaturpfade und textbasierte Statusrückmeldung.
- **CR-014**: Neue nicht triviale Produktlogik und didaktische
  Produktcode-Kommentare sind `N/A`; test-only Integritätsvalidatoren müssen
  in der Planphase erneut auf didaktischen Kommentarwert geprüft werden.
- **CR-015**: `docs/accessibility/` wird nur aktualisiert, wenn der Audit eine
  portfolioübergreifende A11Y-Evidence oder Lücke feststellt; die
  featurebezogene Prüfung bleibt mindestens in
  `example-learning-a11y-review` und `pr-evidence` nachweisbar.
- **CR-016**: Neue oder geänderte script-shaped Tools sind `N/A`; daher sind
  Bash-/PowerShell-Paar, Manpage, bilinguale PowerShell-Hilfe, Cmdlet
  `Verb-Noun`, `--dry-run` und `-WhatIf` nicht ausgelöst. Re-Evaluation: Ein
  Skript tritt in den Plan oder Diff ein.
- **CR-017**: Gemeinsame Agent-Guidance, `.specify/templates/` und
  `.specify/memory/constitution.md` bleiben `NoUpdateRequired`, solange keine
  neue portable Regel entsteht. Bei einer gemeinsamen Status- oder
  Guidance-Änderung müssen `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`
  und betroffene Spec-Kit-Flächen gemeinsam bewertet und aktualisiert werden.
- **CR-018**: `docs/project-statistics.md` ist zum abgeschlossenen
  Implementierungsmeilenstein zu aktualisieren; die Methodik selbst und die
  Agent-Guidance bleiben unverändert.
- **CR-019**: Jeder Governance-Checkpoint MUSS Applicability (`Applicable`,
  `N/A` oder `Open`) und Umsetzung (`Fulfilled`, `Partly Fulfilled`,
  `Not Fulfilled` oder `Not Assessed`) getrennt mit Begründung, Evidence,
  Owner, Reviewer, Restrisiko, Re-Evaluationsauslöser und Follow-up führen.

## Aktuelle Preset-Anwendbarkeit / Current Preset Applicability

Die lokal installierte und aktivierte Matrix ist für diesen Lauf verbindlich.
Versionsangaben stammen aus dem installierten Preset-Registry-Stand am
2026-08-09.

| Preset | Version | Priority | Applicability | Specify-Evidence und Grenze |
|---|---:|---:|---|---|
| `security-governance` | 0.6.2 | 10 | Applicable | SSDF/CWE und Evidence-Integrität; produktbezogene Security-Trigger proportional `N/A` |
| `architecture-governance` | 0.5.2 | 20 | Applicable | Trust-/Cloud-/Serviceänderungen `N/A`; Scope- und Integritätsgrenzen bleiben prüfbar |
| `isaqb-architecture-governance` | 0.2.2 | 30 | Applicable | Framework-Nutzung, Qualitätsrisiken und Follow-up-Ownership |
| `a11y-governance` | 0.4.3 | 40 | Applicable | Lern-, Dokumentations-, Tastatur-, text-first- und WCAG-Prüfung |
| `cross-platform-governance` | 0.2.2 | 50 | Applicable | Plattform-/Terminalaudit; Script-Parität `N/A` solange kein Skript geändert wird |
| `agent-parity-governance` | 0.4.2 | 60 | Applicable | Gemeinsame Status-/Guidance-Entscheidung; keine einseitige Änderung |
| `model-routing-governance` | 0.1.4 | 61 | Applicable | Vorhandene providerneutrale Routing-Metadaten erhalten; keine Modellnamen in Anforderungen |
| `intake-authoring-governance` | 0.3.1 | 64 | Applicable | Bindender aktiver Intake und spätere nicht leere Lastenheft-Grenzen |
| `intake-review-governance` | 0.2.1 | 65 | Applicable | Hashgleicher `Ready`-Review ohne Findings, Fragen oder akzeptierte Risiken |
| `intake-sequencing-governance` | 0.2.3 | 66 | Applicable | Serienstatus `Eligible`, HardCompletionGate erfüllt, spätere Owner-DAG |
| `autonomous-run-governance` | 0.3.6 | 70 | Applicable | `MergeAndSync`, resumierbarer State, Authority-Revalidierung und fail-closed Phasengrenzen |
| `parallel-autonomous-run-governance` | 0.2.6 | 80 | N/A | Kein paralleler Campaign-Run autorisiert; Re-Evaluation nur bei ausdrücklicher Kampagnenautorität |

## Autonomous-Run Applicability / Anwendbarkeit des autonomen Laufs

- **Authority source**: Der Nutzer hat den bestehenden Lauf für Feature 038
  ausdrücklich im Modus `MergeAndSync` fortgesetzt. Commit, Push, Feature-PR,
  Review-Konvergenz, Merge, Branchbereinigung und Synchronisierung von `main`
  sind autorisiert. Ein eng begrenzter Admin-Bypass ist nur an der in FR-045
  definierten Grenze erlaubt. Provider-Administration, Upstream-Kommunikation
  und Start eines Folgefeatures bleiben unautorisiert.
- **Feature identity**: Branch und Feature-Pfad sind
  `038-example-portfolio-conformance-audit`; akzeptierte Inputs sind die vier
  hashgebundenen Artefakte aus dem Feature-State.
- **Scope boundary**: Autonomie darf den read-only Audit nicht auf Runtime,
  Public API, Dependencies, Beispiele, historische/externe Quellen oder
  Finding-Behebung ausweiten.
- **Evidence triggers**: Portfolio-, historische, sichtbare, operative,
  Security-, A11Y-, Plattform-, Lern- und Proof-Evidence sind proportional
  anwendbar; ein `ProductDecision` blockiert.
- **Causal closeout**: Die Delivery-Evidence für Feature 038 ist anwendbar.
  Ein einzelner evidence-only Closeout-PR entsteht nur, wenn Post-Merge-Fakten
  nicht wahrheitsgemäß auf dem geprüften Feature-Head stehen können. Der
  spätere unabhängige Portfolio-Closure bleibt ein eigener Intake und darf
  weder rekursiv gestartet noch durch den Delivery-Closeout ersetzt werden.
- **Mutable validation tokens**: Build-Counter und ein späterer lokaler
  Reviewed-HEAD sind bei Build/Test beziehungsweise finaler lokaler Evidence
  veränderlich und müssen dann frisch erfasst werden. Remote Runner-,
  Provider-Check- und Reviewed-Head-Token müssen vor dem Merge frisch aus den
  tatsächlichen Workflowdefinitionen beziehungsweise Joblogs abgeleitet werden.
- **Run state**: `specs/038-example-portfolio-conformance-audit/autonomous-run-state.json`
  bleibt die feature-lokale Zustandsquelle. `PausedByUser` benötigt explizites
  Resume; unerwartete Unterbrechung oder unbekanntes Operationsergebnis setzt
  `NeedsRevalidation`; Scope-, Intake-, Governance- oder Routing-Drift blockiert
  bis zur Revalidierung.
- **Retrospective boundary**: Nur reproduzierbares, providerneutrales Lernen
  darf als `PresetFollowUp` benannt werden. Keine Promotion wird in diesem
  Feature ausgeführt.

### Acceptance Gates / Abnahme-Gates

| Gate | Applicability | Required scope and stable command token | Runner/platform | Rationale and re-evaluation trigger |
|---|---|---|---|---|
| `GATE-038-01` Intake lineage | Applicable | Hashes, `Ready`, `Eligible`, Feature-037 closure; `specify check` | Local | Blockiert bei Intake-, Review- oder Serien-Drift |
| `GATE-038-02` Portfolio completeness | Applicable | Genau 37 eindeutige Zeilen und Rollen; portfolio integrity validator | Local, platform-neutral | Blockiert bei fehlender, doppelter oder unbekannter Zeile |
| `GATE-038-03` Matrix relations | Applicable | Alle Pflichtfelder, Statuswerte und bidirektionalen Relationen; matrix validator | Local, platform-neutral | Blockiert bei unvollständiger Relation oder unbegründetem `N/A` |
| `GATE-038-04` Finding integrity | Applicable | `EF001+`, Deduplizierung, genau ein Owner, Red-/Green-Proof; finding validator | Local, platform-neutral | Blockiert bei nicht reproduzierbarem Finding oder Product Decision |
| `GATE-038-05` Follow-up handoff | Applicable | Nur nicht leere Owner-Gruppen, azyklische Reihenfolge, genau ein Closure; handoff validator | Local, platform-neutral | Blockiert bei leerem Follow-up oder vorweggenommener Nummer |
| `GATE-038-06` Read-only scope | Applicable | Null Diff in verbotenen Wurzeln und null API/Dependency-Delta; `git diff --check` plus path audit | Local | Blockiert bei jeder Scope-Verletzung |
| `GATE-038-07` Repository validation | Applicable | Format, targeted Auditvalidatoren, vollständige Release-Tests, kanonisches Coverage-Gate | Local macOS; plattformneutrale Daten | Build-/Test-Trigger aktiviert; Build-Counter vorher aktualisieren |
| `GATE-038-08` Documentation/A11Y | Applicable | Bilinguale text-first Evidence; DocFX, Playwright/Axe und UTF-8-Lynx bei Dokumentations-/HTML-Trigger | Local | Blockiert bei nicht zugänglicher oder einsprachig unvollständiger Lern-Evidence |
| `GATE-038-09` Governance scans | Applicable | Secret-, Dependency-, Agent-Paritäts- und Generated-Output-Status | Local | Nicht ausgelöste Teilprüfungen benötigen Begründung und Trigger |
| `GATE-038-10` Remote exact-head delivery | Applicable | Push, PR, Pflichtchecks, null umsetzbare Threads und validierte Exact-Head-Provider-Evidence | GitHub Ubuntu/macOS/Windows und Provider | Blockiert bei fehlender, veralteter oder widersprüchlicher Exact-Head-Evidence |
| `GATE-038-11` Merge and causal closeout | Applicable | Merge-Commit, Branchbereinigung, `main == origin/main`; höchstens ein notwendiger evidence-only Closeout | GitHub und lokales `main` | Blockiert bei unvollständigem Merge-/Sync-Nachweis oder rekursivem Closeout |

## Documentation Impact / Dokumentationsauswirkung

**Decision**: `UpdateRequired`

- **Audiences**: Auszubildende ab dem ersten Ausbildungsjahr, Maintainer,
  Reviewer und Folgefeature-Verantwortliche.
- **Documentation families**: feature-lokale Spec-Kit- und Audit-Evidence,
  Beispielportfolio-Lern-/A11Y-Review, Portfolio-Gate, Handoff,
  `pr-evidence` und Projektstatistik; bestehende Beispiel-Guides nur bei
  festgestellter Dokumentationslücke als Finding, nicht als Sofortkorrektur.
- **Reader paths**: Markdown und JSON sind kanonisch text-first; generierte
  HTML-Pfade werden bei Trigger über DocFX, Tastatur, Axe und UTF-8-Lynx
  geprüft.
- **Canonical source and owner**: Der bindende Intake und die
  feature-lokalen Audit-Artefakte sind kanonisch; Owner ist der
  Example-Portfolio-Audit, spätere Remediation folgt dem Primary Owner.
- **Navigation impact**: Neue Audit-Evidence muss vom Feature-Evidence-Index
  und Portfolio-Handoff auffindbar sein; eine globale Navigationsänderung ist
  nur bei tatsächlichem Dokumentations-Trigger erforderlich.
- **Document class**: Governance-, Lern-, Audit- und Nachweisdokumentation;
  kein Runtime- oder API-Handbuch-Delta.
- **Language strategy and partner**: German-first/English-second in derselben
  Datei; ein `.EN.md`-Sidecar ist nur bei begründet unlesbarer Größe zulässig
  und muss synchron bleiben.
- **Platform/example proof**: Jede der 37 Zeilen trägt konkrete Guide-, Smoke-,
  Interaktions-, A11Y- und Plattformrelationen oder begründetes `N/A`.
- **Distribution class**: Repositoryinterne, sicher veröffentlichbare
  Audit-Evidence; keine neue Produktdistribution.
- **Home-sync need**: `NoUpdateRequired`, solange kein reproduzierbarer
  providerneutraler Preset- oder gemeinsamer Guidance-Fund entsteht.
- **Evidence**: Die in FR-037 benannten Evidence-Familien, Requirements-
  Checklist und späteres `pr-evidence`.
- **Re-evaluation trigger**: Ein Finding ändert einen Guide, eine gemeinsame
  Regel, Navigation, öffentliche API-Dokumentation oder Preset-Governance.

## Key Entities / Schlüsselentitäten

- **Portfolio Entry / Portfolioeintrag**: Eine eindeutige Zeile für genau ein
  geliefertes Beispiel mit Rolle, Welle, Zweck, Lernziel, Source- und
  Modernisierungsrelationen, sichtbaren Bedienpfaden, Framework-Nutzung,
  Dimensionsstatus, Hauptentscheidung, Evidence, Review und Restrisiko.
- **Source Manifest Entry / Quellenmanifest-Eintrag**: Eine read-only Quelle
  oder akzeptierte Evidence mit stabiler ID, Herkunft, Pin oder Hash,
  relativer Verantwortung, Lizenz-/No-Copy-Grenze und den referenzierenden
  Portfoliozeilen.
- **Conformance Finding / Konformitätsfinding**: Eine deduplizierte,
  reproduzierbare `EF001+`-Lücke mit betroffenen Beispielen, genau einem
  Primary Owner, Abhängigkeiten, Red-/Real-Path-Green-Proof und vollständiger
  Impact-/Review-Evidence.
- **Owner DAG / Owner-Abhängigkeitsgraph**: Der azyklische Graph der nicht
  leeren Owner-Gruppen, der die spätere Remediation-Reihenfolge bestimmt.
- **Remediation Handoff / Remediation-Übergabe**: Die Zuordnung jeder nicht
  leeren Owner-Gruppe zu genau einem unnummerierten späteren Lastenheft sowie
  genau einem abschließenden unabhängigen Portfolio-Closure.
- **Portfolio Gate / Portfolio-Gate**: Der wahrheitsgemäße Abschlussstatus des
  Audits. Er darf Findings und Folgearbeit vollständig ausweisen, aber vor dem
  unabhängigen Closure keine vollständige Konformität behaupten.

## Success Criteria / Erfolgskriterien

- **SC-001**: 100 Prozent der 37 erwarteten Beispiele sind genau einmal
  inventarisiert: 25 Original-, 10 Wave-5-, 1 Wave-6-Eintrag und
  `A11yFramework` als genau ein `SupplementalControl`.
- **SC-002**: 100 Prozent der Portfoliozeilen besitzen alle Pflichtfelder,
  genau eine Rolle, genau eine Hauptentscheidung und je Dimension genau einen
  gültigen Status.
- **SC-003**: 100 Prozent der historischen und Modernisierungsrelationen sind
  auf stabile Source- oder Evidence-IDs zurückführbar und bidirektional
  konsistent.
- **SC-004**: 100 Prozent der Beispiele besitzen eine nachvollziehbare
  Framework-, Bedien-, Proof-, Dokumentations-, A11Y- und Plattformentscheidung
  mit Evidence oder begründetem `N/A`.
- **SC-005**: 100 Prozent der Gap-Dimensionen verweisen auf genau ein
  dedupliziertes Finding oder einen blockierenden `ProductDecision`.
- **SC-006**: Jedes Finding besitzt genau einen Primary Owner, einen eindeutigen
  `DeduplicationKey`, vollständige Reproduktion und Red-/Real-Path-Green-Proof.
- **SC-007**: Es entstehen null leere Remediation-Lastenhefte; jede nicht leere
  Owner-Gruppe erzeugt genau einen dependency-geordneten Handoff und danach
  folgt genau ein unabhängiger Closure-Intake.
- **SC-008**: Der Audit-Diff enthält null Runtime-, Public-API-, Dependency-,
  Beispiel-, `tv203s/`-, `TVDEMOS/`-, `TVFM/`- oder externe Source-Änderungen.
- **SC-009**: 100 Prozent der lernenden Evidence ist German-first/
  English-second, CEFR-B2, text-first und ohne farb-, layout- oder
  diagrammabhängige Kernaussage verständlich.
- **SC-010**: Alle anwendbaren lokalen Gates, Checklists und Validatoren
  bestehen; kein Critical-, High- oder undisponierter Medium-Fund bleibt.
- **SC-011**: Jede Governance- und Standardsentscheidung besitzt vollständige
  Applicability-, Evidence-, Owner-, Reviewer-, Restrisiko- und
  Re-Evaluationsdaten; es gibt keine stille Auslassung.
- **SC-012**: Der Feature-State erreicht jede Phasengrenze ohne Verlust der
  Routing-Metadaten und ohne behauptete Remote-, Merge- oder
  Folgefeature-Autorität.
- **SC-013**: Das finale Portfolio-Gate unterscheidet wahrheitsgemäß zwischen
  abgeschlossenem Audit, offener Remediation und erst durch den späteren
  unabhängigen Closure erklärbarer vollständiger Konformität.

## Assumptions / Annahmen

- Feature 037 ist vollständig gemergt; sein akzeptiertes Artefakt weist Wave 6
  als `Closed` und den Portfolioaudit als `Eligible` aus.
- Das aktuelle Portfolio enthält 37 Projekte: 25 Originalbeispiele, 10
  Wave-5-Beispiele, 1 Wave-6-Beispiel und `A11yFramework`.
- Die akzeptierten Pins und Evidence aus Features 024, 029 und 030 bleiben für
  diesen Audit unverändert und lokal verfügbar.
- Moderne idiomatische C#-Entscheidungen bleiben bestehen, sofern keine
  reproduzierbare Vertrags-, Lern-, A11Y-, Plattform-, Framework- oder
  Proof-Lücke vorliegt.
- Ein fehlender Reviewer, ein nicht reproduzierbares Finding oder eine
  notwendige Produktentscheidung wird nicht autonom erraten.
- Dieser Specify-Lauf erstellt nur Spezifikation, Requirements-Checklist,
  Feature-Zuordnung und die notwendige Specify-Phasenmarkierung im vorhandenen
  Autonomous-State.

## Dependencies / Abhängigkeiten

- Hashgleicher bindender Intake, `Ready`-Intake-Review, aktives
  Serienmanifest und Receipt aus dem Autonomous-State.
- Gemergte und akzeptierte Features 024, 029, 030 sowie der Wave-5-/Wave-6-
  Liefer- und Closure-Stand bis Feature 037.
- Read-only Quellen unter `tv203s/`, `TVDEMOS/` und `TVFM/`.
- Aktuell installierte zwölf Presets und Spec Kit 0.12.11.

## Out of Scope / Nicht im Scope

- Runtime-, Public-API-, Dependency-, Paket- oder Produktänderungen
- Änderungen unter `examples/`, `src/`, `tv203s/`, `TVDEMOS/` oder `TVFM/`
- Sofortige Korrektur oder Implementierung eines Findings
- Erneute pauschale Portierung einer Beispielwelle oder neue Beispiele
- Optische, strukturelle, API-, Vererbungs-, Speicherlayout- oder
  Quelltextidentität mit historischen oder modernen Vergleichsprojekten
- Mechanische Übersetzung, Kopie, Fork, Vendorisierung oder Aktualisierung
  externer Quellen
- Bewegliche Upstream-Vergleiche und neue Runtime-Abhängigkeiten
- Breite Framework-Neustrukturierung
- Commit, Push, PR, Review-Anforderung, Merge, Bypass, Branchwechsel,
  Brancherstellung oder Start eines weiteren Features
