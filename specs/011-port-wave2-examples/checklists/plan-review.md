# Plan-Review-Pruefliste: Port Wave 2 Examples

**Zweck**: Review von `plan.md` und den zugehoerigen Feature-Artefakten vor
`/speckit-tasks`. Jeder Pruefpunkt enthaelt einen konkreten
Durchfuehrungshinweis.
**Erstellt**: 2026-05-06
**Feature**: [spec.md](../spec.md), [plan.md](../plan.md)
**Zugehoerige Artefakte**: [research.md](../research.md),
[data-model.md](../data-model.md),
[contracts/wave2-example-acceptance.md](../contracts/wave2-example-acceptance.md),
[quickstart.md](../quickstart.md), [plan-quality.md](plan-quality.md)

**Hinweis**: Diese Pruefliste bewertet Planung, Nachvollziehbarkeit,
Vollstaendigkeit, Governance und Task-Readiness. Sie validiert noch keine
Implementierung.

## Umfang Und Nachvollziehbarkeit

- [x] CHK001 Sind alle elf Welle-2-Beispiele aus `FR-001` im Plan abgedeckt:
  `clipboard`, `demo`, `dlgdsn`, `dyntxt`, `inplis`, `listvi`, `progba`,
  `sdlg`, `sdlg2`, `tcombo` und `tprogb`? [Vollstaendigkeit, Spec FR-001,
  Plan Project Structure]
  - Durchfuehrungshinweis: Vergleiche die Beispielnamen in `spec.md`,
    `plan.md`, `data-model.md`, Contract und `quickstart.md`. Jeder Name muss
    identisch erscheinen; kein Welle-3/4-Beispiel darf als Welle-2-Nachweis
    zaehlen.

- [x] CHK002 Hat jedes Welle-2-Beispiel einen geplanten Projektpfad, eine
  Smoke-Test-Klasse und einen Guide-Pfad? [Vollstaendigkeit, Plan Project
  Structure, Data Model Wave2Example]
  - Durchfuehrungshinweis: Ordne jedes Beispiel genau einem
    `examples/<Name>/`-Eintrag, einer
    `tests/TuiVision.Examples.SmokeTests/*SmokeTests.cs`-Klasse und einer
    `docs/guides/examples/*.md`-Datei zu.

- [x] CHK003 Ist der Pfad vom Pflichtenheft-Eintrag zu Beispiel, Guide und
  Smoke-Szenario explizit geplant? [Nachvollziehbarkeit, Spec SC-005, Contract
  Proof-Surface Contract]
  - Durchfuehrungshinweis: Gehe jeden erwarteten Welle-2-Checklisteneintrag in
    `Pflichtenheft.md` gedanklich durch und notiere den passenden Projekt-,
    Guide- und Smoke-Nachweis. Markiere Eintraege, die nur indirekt ableitbar
    sind.

- [x] CHK004 Ist klar geregelt, dass Welle 3 erst nach vollstaendigem Welle-2-
  Nachweis gestartet oder angerechnet werden darf? [Umfang, Spec FR-014,
  Quickstart Expected Outcomes]
  - Durchfuehrungshinweis: Pruefe Plan, Contract und Quickstart darauf, ob der
    `>>> NAECHSTER SCHRITT <<<`-Marker erst nach allen Welle-2-
    Nachweisflaechen auf Welle 3 verschoben werden darf.

- [x] CHK005 Ist der Punkt "Historical Example Parity Cleanup" klar vom
  Welle-2-Acceptance-Umfang getrennt und fruehestens nach den Pflichtwellen 1-4
  eingeordnet? [Umfang, Spec FR-015, Research Decision 4]
  - Durchfuehrungshinweis: Suche alle Cleanup-Erwaehnungen und bestaetige, dass
    sie als nicht blockierender, nachvollziehbarer Folgepunkt formuliert sind,
    nicht als Aufgabe fuer diese Welle.

## Anforderungsklarheit

- [x] CHK006 Ist `sdlg` eindeutig als historisches vertikales
  `ScrollDialog`/`ScrollGroup`-Beispiel geplant? [Klarheit, Spec FR-005, Data
  Model ScrollableDialogFlow]
  - Durchfuehrungshinweis: Pruefe, ob fuer `sdlg` vertikales Scrollen,
    Fokusbewegung, Begrenzungen und sichtbarer Kontrollzustand gefordert sind
    und keine Standarddialog-Verantwortung bei `sdlg` liegt.

- [x] CHK007 Ist `sdlg2` eindeutig als historisches horizontal und vertikal
  scrollbares `ScrollDialog`/`ScrollGroup`-Beispiel geplant? [Klarheit, Spec
  FR-005, Contract Per-Example Contract]
  - Durchfuehrungshinweis: Pruefe, ob fuer `sdlg2` beide Scrollachsen,
    deterministische Fokusbewegung, Begrenzungen und sichtbarer Kontrollzustand
    gefordert sind und die Umsetzung nicht in eine spaetere Welle verschoben
    wird.

- [x] CHK008 Ist Standarddialog-Nachweis `demo`, `dlgdsn` oder einem anderen
  historisch begruendeten Welle-2-Fluss zugeordnet, nicht `sdlg`/`sdlg2`?
  [Konsistenz, Spec FR-005a, Research Decision 5]
  - Durchfuehrungshinweis: Vergleiche die Standarddialog-Abschnitte in
    `spec.md`, `plan.md`, `research.md` und Contract. Die Zustaendigkeit darf
    zwischen den Artefakten nicht wechseln.

- [x] CHK009 Sind Editor, Hilfe, Streams, Terminalemulation, Runtime-Maus,
  echte Charset-Effekte und Welle-3/4-Beispiele aus der Acceptance
  ausgeschlossen? [Umfang, Spec FR-003, Plan Constraints]
  - Durchfuehrungshinweis: Suche nach diesen Begriffen in den Artefakten und
    pruefe, ob sie nur als Ausschluss, dokumentierte Begrenzung oder nicht
    acceptance-relevantes historisches Verhalten vorkommen.

- [x] CHK010 Ist Dateiinhalt-I/O aus Standarddialog-Acceptance ausgeschlossen,
  waehrend echte lokale Dateisystem-Metadaten erforderlich bleiben? [Klarheit,
  Spec FR-005a, Research Decision 6]
  - Durchfuehrungshinweis: Bestaetige, dass Datei- und Verzeichnisdialoge
    Metadaten, Filter, manuelle Pfade, Abbruch und ungueltige Pfade behandeln,
    aber kein Oeffnen, Lesen, Schreiben, Speichern, Loeschen oder Ueberschreiben
    von Dateiinhalten verlangen.

- [x] CHK011 Ist "example-specific deterministic interaction" so definiert,
  dass daraus konkrete Tasks erzeugt werden koennen? [Klarheit, Plan
  Terminology, Contract Smoke-Test Contract]
  - Durchfuehrungshinweis: Pruefe, ob der Plan den Begriff operationalisiert
    und der Contract reine Start-und-Exit-Smoke-Tests ausschliesst.

- [x] CHK012 Ist `dlgdsn` auf strukturierte Dialogbeschreibung laden/erzeugen,
  rendern, eine einfache Aenderung und sichtbare Ablehnung ungueltiger
  Beschreibungen begrenzt? [Klarheit, Spec FR-006, Data Model
  StructuredDialogDescription]
  - Durchfuehrungshinweis: Pruefe, dass vollstaendige Designer-Paritaet,
    komplette Property-Editoren und Codegenerierung nicht als Welle-2-
    Acceptance beschrieben sind.

## Konsistenz Der Artefakte

- [x] CHK013 Verwenden Plan, Research, Datenmodell, Contract und Quickstart
  dieselbe Entscheidung "ein Projekt pro historischem Beispiel"? [Konsistenz,
  Research Decision 1, Plan Structure Decision]
  - Durchfuehrungshinweis: Vergleiche die Strukturabschnitte. Kein Artefakt
    sollte ein kombiniertes Welle-2-Projekt oder ein neues gemeinsames
    Beispiel-Framework nahelegen.

- [x] CHK014 Stimmen die Smoke-Test-Erwartungen in Spec, Plan, Datenmodell,
  Contract und Quickstart ueberein? [Konsistenz, Spec FR-002, Data Model
  SmokeScenario]
  - Durchfuehrungshinweis: Bestaetige ueberall die Kombination aus
    deterministischer Interaktion und sichtbarem oder oeffentlich
    beobachtbarem Zustand.

- [x] CHK015 Passen die Quickstart-Validierungskommandos zur Teststrategie und
  zur Repository-Versionierungsregel? [Konsistenz, Plan Testing, Quickstart
  Planned Validation Flow]
  - Durchfuehrungshinweis: Pruefe, ob Build- und Testbefehle vorhanden sind
    und ob vor `dotnet build` und `dotnet test` der Build-Counter in
    `Directory.Build.props` genannt wird.

- [x] CHK016 Sind Architektur-, Security-, A11Y-, Statistik- und
  Agent-Context-Nachweise sowohl im Plan als auch im Quickstart sichtbar?
  [Konsistenz, Plan Constitution Check, Quickstart Record Completion Evidence]
  - Durchfuehrungshinweis: Vergleiche die Completion-Evidence-Listen. Jede
    geforderte Nachweisflaeche sollte in beiden Artefakten vorkommen oder einen
    begruendeten N/A-Pfad haben.

- [x] CHK017 Bleibt die vorhandene `plan-quality.md` mit dieser tieferen
  Review-Pruefliste kompatibel? [Konsistenz, Existing Checklist]
  - Durchfuehrungshinweis: Lies `plan-quality.md`; wenn dort kein weiterer
    Klaerungsbedarf steht, muss diese Liste zusaetzliche Review-Tiefe liefern,
    ohne dem Ergebnis zu widersprechen.

## Acceptance-Kriterien

- [x] CHK018 Ist jedes messbare Erfolgskriterium `SC-001` bis `SC-009` auf ein
  konkretes Planartefakt oder eine Nachweisflaeche zurueckfuehrbar?
  [Messbarkeit, Spec Success Criteria]
  - Durchfuehrungshinweis: Ordne jedes `SC-*` gedanklich einem Projektpfad,
    Smoke-Test-Pfad, Guide-Pfad, Pflichtenheft-Update oder Proof-Dokument zu.
    Markiere Kriterien ohne Zielartefakt.

- [x] CHK019 Ist das Guide-Ziel messbar als vier vorhandene Guides plus elf
  neue Guides gleich fuenfzehn Guides? [Messbarkeit, Spec SC-002, Quickstart
  Expected Outcomes]
  - Durchfuehrungshinweis: Pruefe, ob der Plan elf neue Guide-Dateien nennt und
    der Quickstart nach Welle 2 insgesamt fuenfzehn gelieferte Beispiele
    erwartet.

- [x] CHK020 Ist Smoke-Coverage fuer alle fuenfzehn gelieferten Beispiele nach
  der Umsetzung messbar? [Messbarkeit, Spec SC-003, Quickstart Expected
  Outcomes]
  - Durchfuehrungshinweis: Pruefe, ob vorhandene Welle-1-Smoke-Tests erhalten
    bleiben und pro Welle-2-Beispiel eine neue Klasse im bestehenden
    Smoke-Test-Projekt geplant ist.

- [x] CHK021 Sind alle geforderten Interaktionsfamilien abgedeckt: Clipboard,
  Listen/Input/History, Combo Box, Progress, dynamischer Text, scrollbare
  Dialoge, Standarddialoge, dynamischer Dialogentwurf und breites
  Demo-Integrationsbeispiel? [Abdeckung, Spec SC-004, Contract Per-Example
  Contract]
  - Durchfuehrungshinweis: Weise jede Interaktionsfamilie mindestens einem
    geplanten Beispiel zu und markiere Familien, die nur vage statt explizit
    beschrieben sind.

- [x] CHK022 Sind Progress-Ergebnisse objektiv genug, um zeitabhaengige
  Acceptance zu vermeiden? [Messbarkeit, Spec FR-009, Data Model ProgressFlow]
  - Durchfuehrungshinweis: Pruefe, ob `progba` deterministisch bis Completion
    laeuft und `tprogb` einen sichtbaren Canceled-Zustand erreicht, ohne
    unkontrollierte Timer- oder Wall-Clock-Assertions.

## Randfaelle Und Fehlerpfade

- [x] CHK023 Sind Abbruch, Schliessen, ungueltige Auswahl und fehlgeschlagene
  Validierung fuer Dialogfluesse beruecksichtigt? [Randfall, Spec Edge Cases,
  Data Model DialogFlow]
  - Durchfuehrungshinweis: Pruefe Dialogfluss-Anforderungen auf Erfolgs- und
    Nicht-Erfolgs-Pfade. Standarddialoge und dynamische Dialoge sollten
    sichtbare Fehler- oder Abbruchzustaende haben.

- [x] CHK024 Sind fehlende, nicht lesbare, manuell eingegebene oder ungueltige
  Pfade ohne Dateiinhalt-I/O abgedeckt? [Randfall, Spec Edge Cases, Contract
  Standard-Dialog Contract]
  - Durchfuehrungshinweis: Bestaetige, dass diese Faelle als Metadaten- und
    Validierungsverhalten beschrieben sind und kein Artefakt Dateiinhalte
    oeffnen oder veraendern laesst.

- [x] CHK025 Werden fehlerhafte oder unvollstaendige dynamische
  Dialogbeschreibungen als sichtbare Ablehnung behandelt? [Randfall, Spec
  FR-006, Data Model StructuredDialogDescription]
  - Durchfuehrungshinweis: Pruefe, ob Plan und Contract ungueltige
    Beschreibungen sichtbar ablehnen muessen, bevor oder statt gerendert wird.

- [x] CHK026 Sind leere, sehr kleine oder randwertige Inhalte fuer Listen,
  Combo Boxes, dynamischen Text und Progress-Anzeigen abgedeckt? [Randfall,
  Spec Edge Cases, Data Model SmokeScenario]
  - Durchfuehrungshinweis: Pruefe, ob die Artefakte Randinhalte nennen oder
    genug Datenmodell-Regeln enthalten, damit Tasks gezielte Smoke- oder
    Unit-Abdeckung erzeugen koennen.

- [x] CHK027 Ist nicht verfuegbarer oder isolierter Clipboard-Zugriff als
  sichtbares Verhalten geplant statt als versteckter Test-Skip? [Randfall, Spec
  FR-007, Contract Per-Example Contract]
  - Durchfuehrungshinweis: Pruefe, ob Clipboard-Fallback im Contract vorkommt
    und in Headless-Smoke-Tests beobachtbar gemacht werden kann.

- [x] CHK028 Werden host-sensitive historische Verhaltensweisen ueber
  dokumentierte Begrenzungen behandelt statt still ausgelassen? [Randfall, Spec
  Edge Cases, Contract Common Example Contract]
  - Durchfuehrungshinweis: Identifiziere host-sensitive Verhaltensweisen in den
    Artefakten und pruefe, ob akzeptierte Begrenzungen Begruendung und
    Follow-up-Bezug verlangen, wenn sie Acceptance betreffen.

## Governance Und Nicht-Funktionale Anforderungen

- [x] CHK029 Nutzt der Plan die Level-2-TuiVision-Umgebung und .NET 10/C# 14
  ohne abweichende projektlokale Einstellungen? [Governance, Plan Constitution
  Check]
  - Durchfuehrungshinweis: Pruefe Technical Context und Constitution Check.
    Beispielprojekte sollen Repository-Defaults erben und keine eigenen
    Runtime- oder Language-Versionen pinnen.

- [x] CHK030 Ist C# als primaere speichersichere Sprache festgehalten, sodass
  keine Non-MSL-Ausnahme noetig ist? [Governance, Spec CR-005, Plan
  Constitution Check]
  - Durchfuehrungshinweis: Pruefe, ob die Artefakte C# als primaere
    Implementierungssprache nennen und den MSL-Allow-List-Pass begruenden.

- [x] CHK031 Bleiben NIST SSDF und CWE Top 25 anwendbar, waehrend OWASP ASVS
  wegen fehlendem Web/API/Auth-Service begruendet N/A ist? [Security, Spec
  GA-001, Plan Security Documentation]
  - Durchfuehrungshinweis: Vergleiche Governance Applicability und Security-
    Abschnitt des Plans. Die N/A-Begruendung muss explizit und angemessen sein.

- [x] CHK032 Sind Supply-Chain-, SBOM/VEX/SLSA- und Dependency-Aussagen zum
  Plan "keine neue Dependency" passend dosiert? [Security, Plan NuGet
  Dependency Currency, Research Decision 9]
  - Durchfuehrungshinweis: Bestaetige, dass keine neue NuGet-Abhaengigkeit
    geplant ist. Wenn ein Artefakt doch eine andeutet, muss ein
    Nachweis- und Begruendungspfad existieren.

- [x] CHK033 Ist Architektur-Evidence unter `docs/architecture/` mit konkreten
  Zieldateien geplant und sind ADRs nur bei neuen querschnittlichen
  Entscheidungen erforderlich? [Architecture, Spec GA-002/GA-003, Plan
  Architecture Evidence]
  - Durchfuehrungshinweis: Pruefe die geplanten Dateien
    `architecture-vision.md`, `runtime-view.md`, `quality-scenarios.md` und
    `architecture-risks.md`; ADRs sollen bedingt bleiben.

- [x] CHK034 Ist A11Y auf text-first und keyboard-first Terminalverhalten plus
  WCAG 2.2 AA fuer geaenderte generierte HTML-Dokumentation abgegrenzt?
  [A11Y, Spec CR-002, Plan Inclusion/A11Y]
  - Durchfuehrungshinweis: Pruefe, ob Guides, Terminalbeispiele, Smoke-Ausgabe
    und bedingte DocFX-Ausgabe je einen Review-Pfad oder eine begruendete
    N/A-Entscheidung haben.

- [x] CHK035 Sind DE-first, EN-second und CEFR-B2 an jeden lernorientierten
  Guide gebunden? [Dokumentation, Spec FR-011, Data Model ExampleGuide]
  - Durchfuehrungshinweis: Pruefe Guide-Modell und Plan darauf, dass alle elf
    neuen Guide-Dateien Deutsch zuerst und Englisch danach liefern.

- [x] CHK036 Ist `docs/project-statistics.md` als Completion-Artefakt geplant
  und nicht als optionale Nacharbeit? [Governance, Spec CR-004, Plan
  Statistics]
  - Durchfuehrungshinweis: Pruefe, ob Plan und Quickstart Statistik-Updates
    nach der Umsetzung verlangen und diese als Teil des Welle-2-Nachweises
    behandeln.

- [x] CHK037 Ist der Multi-Agent-Context-Refresh fuer Codex, Claude, Gemini und
  Copilot nach Planerzeugung und bei geaendertem Active Context geplant?
  [Governance, Spec GA-006, Quickstart Record Completion Evidence]
  - Durchfuehrungshinweis: Pruefe, ob Agent Guidance Parity im Plan steht und
    der Quickstart alle vier Agent-Kontexte nennt; bei geaenderten Technologien
    auch die aktuellen Guidance-Diffs ansehen.

## Readiness Fuer `/speckit-tasks`

- [x] CHK038 Werden die Voraussetzungen aus `008-controls-revision`,
  `009-controls-widgets-and-collections` und
  `010-standard-dialogs-designer` als Annahmen behandelt statt in diesem
  Feature neu entworfen? [Readiness, Plan Summary, Spec Assumptions]
  - Durchfuehrungshinweis: Suche nach Sprache, die ein breites Framework-
    Redesign andeutet. Fehlendes Framework-Verhalten darf nur blocking-only und
    auf erforderliche Welle-2-Beispiele begrenzt sein.

- [x] CHK039 Sind wiederverwendbare Framework-Aenderungen auf bestehende
  `src/`-Module begrenzt, falls ein Beispiel-Blocker auftaucht? [Architecture,
  Research Decision 3, Plan Structure Decision]
  - Durchfuehrungshinweis: Pruefe, ob der Plan beispiel-lokale Ersatz-
    Implementierungen fuer wiederverwendbare Controls oder Serialization-
    Verhalten vermeidet.

- [x] CHK040 Ist die Implementierungsphase fuer Red-Green-Refactor-Aufgaben
  vorbereitet, bei denen fehlende oder rote Smoke-/Test-Evidence pro
  Beispielfamilie zuerst kommt? [Testing, Plan Red-Green-Refactor Testing
  Scope]
  - Durchfuehrungshinweis: Lies Testing- und Constitution-Abschnitte und
    pruefe, ob daraus Tasks entstehen koennen, die erst Nachweis/Tests und dann
    Implementierung liefern.

- [x] CHK041 Sind die Validierungskommandos ausreichend, aber fuer die
  Planungsphase nicht ueberzogen? [Readiness, Quickstart Planned Validation
  Flow]
  - Durchfuehrungshinweis: Stelle sicher, dass Build, Smoke-Tests, Full Tests,
    Coverage, Format und bedingte DocFX/A11Y-Pruefung fuer die Umsetzung
    genannt sind, waehrend diese reine Checklisten-Erzeugung keine Full Suite
    erfordert.

- [x] CHK042 Sind die Feature-Artefakte frei von offenen Platzhalter-
  Markierungen und generischen Beispiel-Checklistenpunkten? [Qualitaet, alle
  Feature-Artefakte]
  - Durchfuehrungshinweis: Suche im Feature-Verzeichnis nach ueblichen
    Spec-Kit-Klaerungs- und Platzhaltermarkierungen. Pruefe ausserdem, dass
    alle Checklisteneintraege feature-spezifische Review-Punkte sind.

- [x] CHK043 Muessen zurueckgestellte oder bewusst reduzierte historische
  Verhaltensweisen Begruendung und nachvollziehbaren Follow-up-Bezug enthalten?
  [Readiness, Spec FR-013, Contract Common Example Contract]
  - Durchfuehrungshinweis: Pruefe jede akzeptierte Begrenzung darauf, was
    reduziert wird, warum, ob es Acceptance betrifft und wo Follow-up verfolgt
    wird.

- [x] CHK044 Ist der Plan ohne weitere Klaerungsrunde bereit fuer
  `/speckit-tasks`? [Readiness, Existing Checklist, alle zugehoerigen
  Artefakte]
  - Durchfuehrungshinweis: Bearbeite CHK001-CHK043 und sammle verbleibende
    Unklarheiten. Wenn keine offen bleiben, kann dieser Punkt abgehakt und die
    Task-Generierung gestartet werden.

## Hinweise

- Abgehakte Punkte als `[x]` markieren.
- Kurze Findings direkt unter dem betroffenen Punkt ergaenzen.
- Wenn ein Punkt einen Plan-/Spec-Konflikt zeigt, zuerst das Quellartefakt
  korrigieren und danach den Pruefpunkt erneut bewerten.

## Review Result 2026-05-06

- CHK003/CHK018/CHK021 fuehrten zu einer expliziten
  `Wave-2 Checklist Traceability`-Matrix und einer
  `Interaction-Family Mapping`-Tabelle in `plan.md`.
- CHK009 synchronisierte den `stream`-Ausschluss in `plan.md`,
  `quickstart.md` und `plan-quality.md`.
- CHK023-CHK028 fuehrten zu konkreteren Boundary-, Failure- und
  Limitation-Regeln in Contract, Datenmodell und Quickstart.
- CHK034 erweiterte den Quickstart-Nachweis auf Smoke-Ausgabe und begruendete
  N/A-Pfade fuer generierte HTML-Dokumentation.
- Nach den Korrekturen bleibt kein weiterer Klaerungsbedarf vor
  `/speckit-tasks`.
