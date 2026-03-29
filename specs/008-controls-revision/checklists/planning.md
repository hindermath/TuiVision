# Planning Checklist: Controls Revision

**Purpose**: Validate the quality, completeness, clarity, consistency, traceability, and repo-wide follow-through readiness of `plan.md` and its related planning artifacts before `/speckit.tasks` or implementation work begins.
**Created**: 2026-03-29
**Feature**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/008-controls-revision/spec.md](/Users/thorstenhindermann/RiderProjects/TuiVision/specs/008-controls-revision/spec.md)

**Note**: Diese Checkliste ist als formales Plan-Review-Gate fuer Autor und PR-Reviewer gedacht. Sie prueft die Qualitaet der Planungsartefakte selbst, nicht die korrekte Implementierung des Features.

## Requirement Completeness

- [x] CHK001 - Sind alle in der Spezifikation geforderten Kernliefergegenstaende fuer Menue, Statuszeile, Fenster und Dialog im Plansatz sichtbar vertreten? [Completeness, Spec §User Story 1-3, Spec §FR-002..FR-015, Plan §Summary, §Phase 1 Design Overview]
  Durchführungshinweis: [../spec.md](../spec.md), [../plan.md](../plan.md), [../research.md](../research.md), [../data-model.md](../data-model.md) und [../contracts/controls-revision-api.md](../contracts/controls-revision-api.md) nebeneinander lesen. Markiere den Punkt nur als erfuellt, wenn jede Verhaltensgruppe aus der Spec mindestens in einem Designartefakt normativ wieder auftaucht.

- [x] CHK002 - Sind die neu eingefuehrten Deklarationstypen `TSubMenu`, `TStatusDef` und `WindowFlags` im Planungsset nicht nur genannt, sondern auch in Rolle, Zweck und Grenzen ausreichend beschrieben? [Completeness, Spec §FR-006..FR-011, Plan §Project Structure, §Phase 1 Design Overview, Research §Decision 2/5/7]
  Durchführungshinweis: Pruefe, ob jeder der drei Typen in Strukturbaum, Research, Datenmodell oder Contract mehr als nur als Dateiname erscheint. Wenn ein Typ lediglich im Dateibaum steht, aber keine fachliche Rolle hat, bleibt der Punkt offen.

- [x] CHK003 - Sind alle angekuendigten Plan-Artefakte fuer dieses Feature vorhanden und gehoeren inhaltlich zur gleichen Planversion? [Completeness, Plan §Project Structure, Research, Data Model, Quickstart, Contract]
  Durchführungshinweis: Kontrolliere, ob [../plan.md](../plan.md), [../research.md](../research.md), [../data-model.md](../data-model.md), [../quickstart.md](../quickstart.md) und [../contracts/controls-revision-api.md](../contracts/controls-revision-api.md) vorhanden sind und dieselbe Begriffs- und Scopewelt verwenden.

- [x] CHK004 - Ist die Planungsabdeckung fuer die repo-weiten Pflichtflaechen vollstaendig, also fuer `docs/porting-status.md`, `Pflichtenheft.md` und `docs/project-statistics.md`? [Completeness, Spec §FR-020, Plan §Summary, §Dependencies & Assumptions, Research §Decision 10, Gap]
  Durchführungshinweis: Suche in allen Planartefakten gezielt nach diesen drei Pfadnamen. Der Punkt ist nur erfuellt, wenn fuer jede Pflichtflaeche klar wird, warum sie angepasst werden muss und dass sie Teil der Lieferdefinition ist.

## Requirement Clarity

- [x] CHK005 - Ist die Grenze „genau eine Submenu-Ebene“ praezise genug formuliert, sodass weder tiefere Rekursion noch unklare Sonderfaelle offen bleiben? [Clarity, Spec §FR-006, Plan §Terminology, §Design Boundaries, Research §Decision 3, Contract §Hierarchy guarantee]
  Durchführungshinweis: Lies alle Passagen zu Menühierarchie hintereinander. Wenn ein Reviewer daraus noch ableiten koennte, dass Untermenüs wiederum Untermenüs haben duerfen, ist die Formulierung nicht scharf genug.

- [x] CHK006 - Ist die Bedeutung von „neutraler leerer Statuszeile“ so klar beschrieben, dass kein Reviewer sie als versteckten globalen Default-Aktionssatz missverstehen muss? [Ambiguity, Spec §FR-008a, Plan §Terminology, Data Model §StatusResolutionState, Contract §TStatusLine]
  Durchführungshinweis: Pruefe, ob „neutral/empty“ an allen Stellen als bewusst fehlende Kontextaktionen beschrieben wird und nicht als stiller Ersatz durch alte oder globale Aktionen.

- [x] CHK007 - Ist die Compatibility-Bridge fuer bestehende `GetStatusHints()`-Aufrufer klar genug abgegrenzt, sodass sie nicht als dauerhafte Parallelarchitektur missverstanden wird? [Clarity, Plan §Research Focus 6, §Design Boundaries, Research §Decision 6, Contract §Compatibility-bridge guarantee]
  Durchführungshinweis: Suche nach Formulierungen wie „fallback“, „compatibility bridge“ und „only when“. Wenn nicht eindeutig wird, wann der alte Pfad endet und wann der neue Pfad gilt, ist der Punkt nicht erfuellt.

- [x] CHK008 - Ist die Fenster-Schliessen-Semantik fuer `Ctrl+W` und konsumiertes `Escape` klar genug formuliert, um Fehlinterpretationen zwischen Kind-Control und Fenster-Ebene zu vermeiden? [Clarity, Spec §FR-010, Plan §Technical Context, Data Model §WindowInteractionSession, Contract §Window-close guarantee]
  Durchführungshinweis: Lies Spec, Datenmodell und Contract auf dieselbe Guard-Bedingung hin. Fehlt die Einschraenkung „nur wenn kein fokussiertes Kind konsumiert“, ist die Regel noch zu unscharf.

- [x] CHK009 - Ist die Dialog-Validierungspassage klar genug, um zwischen „Close Request abgelehnt“ und „modaler Result-Code akzeptiert“ ohne Zwischeninterpretation zu unterscheiden? [Clarity, Spec §FR-013..FR-015, Research §Decision 9, Data Model §DialogCloseRequest]
  Durchführungshinweis: Pruefe, ob das Planset sichtbar trennt zwischen dem Pruefen einer Schliessanforderung und dem tatsaechlichen Rueckgabewert von `Run()`. Wenn beides sprachlich vermischt wird, bleibt der Punkt offen.

## Requirement Consistency

- [x] CHK010 - Stimmen Spec, Plan, Research, Datenmodell, Quickstart und Contract bei Wrap-Around-Navigation, Skip-Verhalten und Confirm-Verhalten des Menues inhaltlich ueberein? [Consistency, Spec §FR-002..FR-005, Plan §Scenario Matrix, Research §Decision 4, Data Model §Menu Interaction Lifecycle, Quickstart §Expected Outcomes]
  Durchführungshinweis: Suche nach Aussagen zu `wrap`, `skip`, `Enter`, Mnemonic und Dismiss. Schon eine abweichende Formulierung wie „stops at the ends“ in nur einem Artefakt ist ein Inkonsistenzsignal.

- [x] CHK011 - Ist die Statuskontext-Regel „first match wins“ in allen relevanten Artefakten konsistent und ohne konkurrierendes Alternativmodell beschrieben? [Consistency, Spec §FR-007a, Research §Decision 5, Data Model §StatusContextDefinition, Contract §Status-routing guarantee]
  Durchführungshinweis: Vergleiche alle Passagen zur Konfliktaufloesung bei ueberlappenden Bereichen. Wenn irgendwo „narrowest“ oder „invalid overlap“ implizit mitschwingt, ist der Plansatz nicht konsistent.

- [x] CHK012 - Bleibt die Aussage zur `HelpContext`-Einfuehrung ueber alle Artefakte hinweg konsistent, ohne dass daneben implizit wieder eine Hint-basierte Primärroute behauptet wird? [Consistency, Plan §Research Focus 7, Research §Decision 6, Data Model §HelpContextSource, Contract §TView]
  Durchführungshinweis: Lies alle Stellen zu `HelpContext` und `GetStatusHints()`. Wenn ein Dokument `HelpContext` als neue Primärroute beschreibt, ein anderes aber weiter nur Hint-Inspektion voraussetzt, ist der Punkt offen.

- [x] CHK013 - Sind die Scope-Grenzen fuer kein Maus-Support, kein Streaming, keine Zoom/Grow-Funktionen und keine neue Beispielwelle durchgaengig konsistent? [Consistency, Spec §FR-016..FR-019, Plan §Technical Context, §Design Boundaries, Contract §Scope guarantee]
  Durchführungshinweis: Fuehre eine Volltextsuche nach `mouse`, `stream`, `zoom`, `grow`, `example wave`, `out of scope` durch. Jede Scope-Grenze muss in mehr als einem Artefakt denselben Status haben.

## Acceptance Criteria Quality

- [x] CHK014 - Ist fuer jedes Success Criterion aus der Spec im Planungsset klar erkennbar, welcher Planbaustein seine spaetere Verifikation vorbereitet? [Traceability, Spec §SC-001..SC-005, Plan §Success-Criteria Traceability]
  Durchführungshinweis: Gehe `SC-001` bis `SC-005` einzeln durch und ordne jedem Kriterium mindestens einen Planabschnitt, ein Designartefakt und einen Test-/Review-Hook zu. Fehlt fuer ein Kriterium diese Zuordnung, ist die Traceability unzureichend.

- [x] CHK015 - Sind die Testpflichten fuer Menu, Status, Fenster und Dialog so geschrieben, dass daraus objektiv pruefbare Aufgaben ableitbar sind, ohne interne Implementierung vorwegzunehmen? [Measurability, Plan §Testing Strategy, Contract §Test Obligations, Quickstart §Planned Validation Flow]
  Durchführungshinweis: Pruefe, ob die Testpflichten konkrete beobachtbare Verhaltensklassen nennen, aber keine verbotene interne Loesung diktieren. Wenn aus einem Punkt nur „mach Tests fuer X“ hervorgeht, ohne Messbarkeit, oder umgekehrt schon interne Algorithmen vorgeschrieben werden, bleibt der Punkt offen.

- [x] CHK016 - Ist die Forderung nach Linux- und Windows/WSL-Evidenz stark genug operationalisiert, um spaetere „wir pruefen das irgendwann“‑Ausreden zu verhindern? [Acceptance Criteria, Non-Functional, Plan §Constitution Check, §Testing Strategy, Quickstart §Prerequisites]
  Durchführungshinweis: Suche nach den Plaetzen, an denen die Zusatzplattformen genannt werden. Wenn dort nur eine lose Empfehlung steht statt einer klaren Review-/Liefererwartung, ist die Anforderung nicht hart genug.

## Scenario Coverage

- [x] CHK017 - Deckt das Planset Primaer-, Alternativ-, Recovery- und Negative-Szenarien fuer alle vier Verhaltungsbloecke (Menue, Status, Fenster, Dialog) sichtbar ab? [Coverage, Plan §Scenario Matrix, Data Model §State Transitions, Quickstart §Planned Validation Flow]
  Durchführungshinweis: Erstelle eine kleine Matrix mit den vier Szenarioklassen und den vier Verhaltungsbloecken. Markiere pro Zelle, in welchem Artefakt der Fall vorkommt. Luecken bei Recovery oder Negative-Szenarien sind besonders kritisch.

- [x] CHK018 - Sind die Resize-Faelle des Menues nicht nur als technischer Randfall, sondern als eigene fachliche Review-Verpflichtung erkennbar? [Coverage, Edge Case, Spec §FR-012, Plan §Scenario Matrix, Data Model §MenuLayoutSlot]
  Durchführungshinweis: Pruefe, ob Resize nicht nur in einem Nebensatz auftaucht, sondern in mindestens einem Datenmodell- und einem Test-/Review-Abschnitt als eigenstaendiger Fall sichtbar ist.

- [x] CHK019 - Ist fuer die Statuszeile sowohl der Match-Fall als auch der No-Match-Fall samt Legacy-Fallback abgedeckt, statt nur den „Happy Path“ zu beschreiben? [Coverage, Spec §FR-007..FR-008a, Data Model §Status Resolution Lifecycle, Quickstart §Expected Outcomes]
  Durchführungshinweis: Lies Datenmodell, Quickstart und Contract mit Fokus auf die drei Ausgaenge `definitions`, `neutral-empty` und `legacy-fallback`. Wenn einer dieser Ausgaenge nur implizit bleibt, ist die Szenarioabdeckung unvollstaendig.

- [x] CHK020 - Sind fuer `TWindow` sowohl der Commit- als auch der Restore-Pfad des Move-Mode gleichwertig beschrieben, statt nur den erfolgreichen Verschiebefall hervorzuheben? [Coverage, Spec §FR-011..FR-011a, Data Model §Window Move Lifecycle, Contract §Window-move guarantee]
  Durchführungshinweis: Pruefe, ob `Enter`-Commit und `Escape`-Restore im Planset mit vergleichbarer Sichtbarkeit vorkommen. Wenn Restore nur als Nebensatz genannt wird, besteht Task-Ableitungsrisiko.

## Edge Case Coverage

- [x] CHK021 - Sind die nicht-aktionsfaehigen Menu-Eintraege (Separatoren, Disabled) im Planset als echte Edge Cases behandelt und nicht nur als beiläufige Nebenbemerkung? [Edge Case, Spec §Edge Cases, Research §Decision 4, Data Model §SubmenuDefinition, Contract §Actionable-entry guarantee]
  Durchführungshinweis: Pruefe, ob diese Eintragsarten in mindestens zwei Artefakten explizit benannt und mit Auswahl-/Fokusfolgen verknuepft sind.

- [x] CHK022 - Ist der Fall „Kind-Control konsumiert `Escape`, daher kein Fenster-Close“ als eigener Grenzfall ausreichend sichtbar? [Edge Case, Spec §FR-010, Data Model §WindowInteractionSession, Contract §Window-close guarantee]
  Durchführungshinweis: Suche gezielt nach der Konsumierungsbedingung und bewerte, ob daraus fuer Reviewer klar wird, dass dieser Fall separat abgesichert werden muss.

- [x] CHK023 - Ist der Dialog-Grenzfall „Close Request wird abgelehnt und Zustand bleibt erhalten“ im Planungsset staerker als nur eine Selbstverstaendlichkeit formuliert? [Edge Case, Spec §FR-014, Data Model §DialogCloseRequest, Quickstart §Expected Outcomes]
  Durchführungshinweis: Wenn die Zustandsbewahrung nur indirekt aus „dialog remains open“ folgt, ist der Punkt zu schwach. Es muss explizit erkennbar sein, dass keine stillen Zustandsverluste akzeptiert werden.

## Non-Functional Requirements

- [x] CHK024 - Uebersetzt das Planset die Constitution-Vorgaben fuer TDD, Coverage, Dokumentation und Plattformportabilitaet in konkret überprüfbare Plan-Constraints? [Non-Functional, Plan §Constitution Check, §Technical Context, §Testing Strategy, AGENTS.md, CLAUDE.md, GEMINI.md]
  Durchführungshinweis: Vergleiche Constitution/Agent-Guidance mit dem Plan. Der Punkt ist nur erfuellt, wenn die repo-weiten Regeln im Plan nicht nur vorausgesetzt, sondern als konkrete Planbedingungen erkennbar sind.

- [x] CHK025 - Ist die Performance-Aussage „innerhalb desselben Event-Loop-Zyklus und ohne user-visible lag“ hinreichend praezise, um als Designleitplanke zu taugen, oder bleibt sie subjektiv? [Ambiguity, Non-Functional, Plan §Technical Context, Gap]
  Durchführungshinweis: Frage dich als Reviewer, ob du aus dem Text erkennen kannst, wann diese Erwartung verletzt waere. Wenn nur Gefuehl statt beobachtbarer Schwelle bleibt, den Punkt offen lassen.

## Dependencies & Assumptions

- [x] CHK026 - Sind die Abhaengigkeiten auf bestehende `TView`/`TGroup`/`TProgram`-Semantik stark genug dokumentiert, sodass spaetere Tasks keine Architektur erfinden muessen? [Dependency, Plan §Dependencies & Assumptions, Research §Decision 6/9, Assumption]
  Durchführungshinweis: Pruefe, ob die Wiederverwendung bestehender Basisklassen mit konkreten Annahmen und Grenzen beschrieben ist. Reine Behauptungen ohne Folgenabschätzung reichen nicht.

- [x] CHK027 - Ist ausreichend dokumentiert, welche bestehenden Aufrufer oder Feature-004-Oberflächen von der `GetStatusHints()`-Kompatibilitaetsbruecke betroffen sein koennen? [Dependency, Research §Decision 6, Plan §Design Boundaries, Gap]
  Durchführungshinweis: Suche nach konkreten Hinweisen auf bereits portierte Hint-Produzenten oder angrenzende Editor-/Help-Oberflächen. Wenn die Bruecke nur abstrakt benannt ist, fehlt Traceability zu realen Abhaengern.

## Repo-Wide Synchronization & Proof Surfaces

- [x] CHK028 - Prueft das Planset klar genug, dass `docs/porting-status.md` fuer die betroffenen historischen `.cc`-Zeilen aktualisiert werden muss und nicht nur allgemein „Proof Surfaces“ genannt werden? [Traceability, Plan §Dependencies & Assumptions, Research §Decision 10, docs/porting-status.md, Gap]
  Durchführungshinweis: Vergleiche die im Plan genannten historischen Zuordnungen mit den aktuell betroffenen Zeilen in [docs/porting-status.md](/Users/thorstenhindermann/RiderProjects/TuiVision/docs/porting-status.md). Wenn der Reviewer nicht erkennen kann, welche Ledger-Zeilen spaeter betroffen sind, ist die Planung noch zu abstrakt.

- [x] CHK029 - Ist die Pflicht zum Verschieben des Markers `>>> NAECHSTER SCHRITT <<<` in `Pflichtenheft.md` ausreichend als Follow-through-Anforderung abgeleitet? [Completeness, Assumption, AGENTS.md, Pflichtenheft.md, Gap]
  Durchführungshinweis: Pruefe, ob der Plan nicht nur `Pflichtenheft.md` als Artefakt nennt, sondern die Funktion als priorisierte Next-Step-Flaeche implizit oder explizit mitdenkt. Wenn das Dokument nur als allgemeine Doku genannt wird, bleibt der Punkt offen.

- [x] CHK030 - Ist die Statistik-Update-Pflicht fuer `docs/project-statistics.md` im Planungsset hinreichend stark verankert, statt nur als lose Nachpflege erwähnt zu werden? [Completeness, Plan §Summary, §Constitution Check, Research §Decision 10, docs/project-statistics.md]
  Durchführungshinweis: Suche nach Formulierungen wie `must`, `required`, `mandatory delivery surface`. Eine reine Erwaehnung ohne Verbindlichkeit reicht nicht fuer ein formales Gate.

- [x] CHK031 - Sind die Agent-Kontext-Folgen fuer `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und Copilot-Dateien konsistent genug mit dem Planset, dass kein stiller Sync-Drift entsteht? [Consistency, Dependency, AGENTS.md, CLAUDE.md, GEMINI.md, .github/agents/copilot-instructions.md]
  Durchführungshinweis: Stichprobenartig pruefen, ob die neu eingetragenen `008-controls-revision`-Technologiezeilen in den Agent-Dateien inhaltlich zum Technical Context aus [../plan.md](../plan.md) passen und keine abgeschnittenen oder widerspruechlichen Informationen tragen.

## Ambiguities & Conflicts

- [x] CHK032 - Gibt es einen Konflikt zwischen dem engen Contract „genau eine Submenu-Ebene“ und der Quickstart-/Beispielsprache, die versehentlich wie eine allgemein rekursive Menue-API wirken koennte? [Conflict, Contract §Hierarchy guarantee, Quickstart §Representative Usage Sketch]
  Durchführungshinweis: Lies das Beispiel in [../quickstart.md](../quickstart.md) so, als waere es normativ. Wenn daraus ohne Zusatzwissen auch tiefere Rekursion ableitbar scheint, ist die Dokumentation noch konfliktanfaellig.

- [x] CHK033 - Ist die Grenze zwischen „Plan beschreibt Testpflichten“ und „Plan zwingt konkrete interne Umsetzung“ ueber alle Artefakte hinweg sauber eingehalten? [Ambiguity, Conflict, Plan §Testing Strategy, Quickstart §Planned Validation Flow, Contract §Test Obligations]
  Durchführungshinweis: Suche nach Passagen, die mehr als Beobachtbarkeit verlangen und schon internen Codeaufbau, private Hilfsklassen oder konkrete Datenstrukturen faktisch festschreiben. Solche Stellen als Konflikt zwischen Anforderungen und Implementierungsfreiheit markieren.

- [x] CHK034 - Ist die Traceability von Spec zu Planungsset stark genug, oder braucht dieses Feature vor `/speckit.tasks` noch eine zusaetzliche explizite ID-/Mapping-Schicht? [Traceability, Spec §FR-002..FR-020, Plan §Traceability Matrix, Gap]
  Durchführungshinweis: Gehe stichprobenartig mehrere `FR-*`-Punkte und mindestens zwei `SC-*`-Punkte aus [../spec.md](../spec.md) durch und versuche sie ohne Interpretationssprung in Plan, Research, Datenmodell, Quickstart oder Contract wiederzufinden. Wenn das nur mit viel Detektivarbeit gelingt, ist die vorhandene Traceability noch zu schwach.

## Notes

- Diese Checkliste ist als formales Review-Gate vor `/speckit.tasks` gedacht.
- Sie ergaenzt [requirements.md](./requirements.md) und fokussiert speziell die Qualitaet von `plan.md` und den dazugehoerigen Designartefakten.
- Der Schwerpunkt dieser Liste liegt bewusst auf Vollstaendigkeit, Traceability, Testbarkeit, Architektur- und Scope-Disziplin sowie repo-weiten Pflichtflaechen.
- Review-Durchgang 2026-03-29: Vor dem Abhaken wurden Plan-/Research-/Quickstart-Follow-through, Legacy-Bridge-Traceability, Performance-Formulierung und Agent-Datei-Synchronisierung nachgeschaerft.
