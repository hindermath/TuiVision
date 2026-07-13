# Feature Specification: Core Runtime Conformance Hardening

**Feature Branch**: `025-core-runtime-conformance-hardening`
**Created**: 2026-07-13
**Status**: Accepted
**Binding Input**: `Lastenheft_10_Core-Runtime-Conformance-Hardening.md`

## Clarifications

### Session 2026-07-13

- Q: Wer darf einen Fokuswechsel ablehnen und wann wird die Entscheidung ausgewertet? A: Das bisher aktuelle View besitzt den Veto-Hook; er wird genau einmal vor jeder Fokus- oder Current-Mutation ausgewertet. Der additive Try-Pfad unterscheidet `Accepted`, `Rejected` und `NoOp`, während der bestehende `SetFocus`-Aufruf kompatibel bleibt.
- Q: Welche Pending- und Idle-Grenze verhindert Queue-Wachstum und Busy Loops? A: Feature 025 verwendet den historischen einzelnen Pending-Event-Slot. Ein leerer Poll ruft Idle höchstens einmal auf und gibt danach CPU-Zeit frei; es entsteht weder Hintergrundthread noch unbeschränkte Queue.
- Q: Welche Modalverschachtelung ist erlaubt? A: Pro Owner darf genau ein direktes modales Kind aktiv sein. Eine weitere modale Session ist nur unterhalb des aktiven modalen Kindes erlaubt; Abschluss, Abbruch und Shutdown stellen den vorherigen noch berechtigten Fokus wieder her.
- Q: Wann wird die gemeinsame Command-Verfügbarkeit aktualisiert? A: Nach akzeptiertem Fokuswechsel, nach jedem verarbeiteten Ereignis und im Idle wird ein gemeinsamer Snapshot erzeugt; vor Ausführung wird derselbe Kontext erneut geprüft. Menü und StatusLine leiten ihre Darstellung daraus ab.
- Q: Wo liegt die minimale generische Drag-Grenze? A: Eine View beginnt nach einer Zellenbewegung genau eine Capture-Session. Ein opt-in Ziel entscheidet über Drop oder Rejection; Pfeiltasten bewegen, Enter bestätigt und Escape oder Lifecycle-Verlust bricht denselben Vertrag ab.

## User Scenarios & Testing

### User Story 1 - Ereignisse, Fokus und Zustand verlässlich verarbeiten (Priority: P1)

Als Framework-Nutzerin kann ich mich darauf verlassen, dass Ereignisse am
Erzeugungsrand eindeutig sind, Fokuswechsel vor ihrer Ausführung geprüft
werden können und hierarchische View-Zustände nur die fachlich betroffenen
Kinder erreichen.

*As a framework consumer, I can rely on concrete event kinds, veto-capable
focus transitions, and state propagation that affects only the intended views.*

**Why this priority**: Diese drei Verträge liegen unter Eingabe, Navigation,
Validierung und Darstellung. Fehler an dieser Stelle vervielfachen sich in
allen späteren Anwendungen.

**Independent Test**: Reale Erzeugungs-, Fokus- und Gruppenpfade prüfen
konkrete sowie zusammengesetzte Events, angenommene und abgelehnte
Fokuswechsel und die Zustandsmatrix eines Groups mit aktuellem und nicht
aktuellem Kind.

**Acceptance Scenarios**:

1. **Given** eine konkrete bekannte Eventart, **When** sie am kanonischen Rand erzeugt wird, **Then** erreicht genau diese Art den Dispatch.
2. **Given** eine Kategorie, Composite-Maske oder unbekannte Eventart, **When** sie als konkretes Event verwendet wird, **Then** wird sie vor dem Dispatch deterministisch abgelehnt.
3. **Given** ein aktuelles fokussiertes View und ein neues Ziel, **When** der Fokuswechsel abgelehnt wird, **Then** bleiben Fokus, Eingabedaten und sichtbare Rückmeldung konsistent.
4. **Given** ein Group mit mehreren Kindern, **When** fokus- oder zustandsabhängige Bits weitergegeben werden, **Then** gelten die dokumentierten Regeln und nur das aktuelle Kind erhält `Focused`.

### User Story 2 - Leerlauf und wartende Ereignisse deterministisch ordnen (Priority: P1)

Als Anwendungsentwickler kann ich begrenzte Leerlaufarbeit ausführen, ohne
echte Eingaben zu verdrängen, einen Busy Loop zu erzeugen oder Shutdown zu
verzögern.

*As an application developer, I can run bounded idle work without starving real
input, creating a busy loop, or delaying shutdown.*

**Why this priority**: Desktop-, Command- und Statusaktualisierungen benötigen
einen gemeinsamen Lifecycle-Punkt, bevor Verbraucher-Features darauf aufbauen.

**Independent Test**: Die reale Anwendungsschleife verarbeitet wartende
Ereignisse vor Leerlaufarbeit, führt Idle nur ohne Event aus, kann wiederholt
aktualisieren und beendet sich deterministisch.

**Acceptance Scenarios**:

1. **Given** ein wartendes Ereignis, **When** die Anwendungsschleife läuft, **Then** wird das Ereignis vor jeder Idle-Aktualisierung verarbeitet.
2. **Given** kein wartendes Ereignis, **When** ein begrenzter Schleifendurchlauf stattfindet, **Then** wird der Idle-Hook genau innerhalb seines dokumentierten Budgets aufgerufen.
3. **Given** Shutdown während Event- oder Idle-Verarbeitung, **When** die Schleife den Zustand beobachtet, **Then** endet sie ohne weitere unkontrollierte Arbeit.

### User Story 3 - Desktop-, Modal- und Close-Lifecycle sichtbar abschließen (Priority: P1)

Als Nutzer kann ich Fenster geordnet aktivieren, anordnen und schließen sowie
modale Interaktionen mit einem eindeutigen Ergebnis verlassen. Fokus, Z-Order
und sichtbare Hierarchie werden danach wiederhergestellt.

*As a user, I can activate, arrange, close, and run modal windows through a
coherent lifecycle with restored focus, ordering, and visible hierarchy.*

**Why this priority**: Wave 5 und Wave 6 benötigen wiederverwendbare
Frameworkverantwortung statt anwendungslokaler Fensterverwaltung.

**Independent Test**: Reale App-Loop-Proofs führen Insert, Top/Next, Tile,
Cascade, Close-All, `cmClose`, Ctrl+W, Escape, modales Ergebnis, Verschachtelung,
Abbruch und Shutdown aus und prüfen Zustand, View-Tree, Fokus und Zellen.

**Acceptance Scenarios**:

1. **Given** mehrere schließbare und nicht schließbare Fenster, **When** Stack- und Anordnungsoperationen ausgeführt werden, **Then** bleiben Fokus, Bounds und Z-Order deterministisch.
2. **Given** ein Fenster ohne Safe-Close-Veto, **When** `cmClose`, Ctrl+W oder der anwendbare Escape-Pfad ausgelöst wird, **Then** ist es sichtbar aus seinem Owner entfernt.
3. **Given** eine modale Session, **When** sie mit Ergebnis, Abbruch oder Shutdown endet, **Then** sind Events isoliert, das Ergebnis eindeutig und der vorherige Fokus soweit möglich wiederhergestellt.

### User Story 4 - Command-Status und reale Tastatur stimmen überein (Priority: P1)

Als tastaturnutzende Person sehe und erhalte ich dieselbe Command-Verfügbarkeit
in aktivem View, Menü und StatusLine. Reale Terminaltasten werden genau einmal
kanonisch übersetzt und führen denselben Command aus wie strukturierte Tests.

*As a keyboard user, I receive one coherent command state across views, menus,
and the status line, while real terminal keys use one canonical translation.*

**Why this priority**: Widersprüchliche Command-Zustände und ein abweichender
realer Keyboard-Pfad machen automatisierte Proofs trotz scheinbar grüner
Adaptertests unzuverlässig.

**Independent Test**: Eine reale Anwendung wechselt Fokus, Auswahl und Fenster,
fragt danach alle Command-Oberflächen ab und speist eine Matrix ab
`ConsoleKeyInfo` oder dem echten Adapterrand ein.

**Acceptance Scenarios**:

1. **Given** ein Kontextwechsel, **When** Command-Verfügbarkeit neu bestimmt wird, **Then** stimmen aktives View, Menü, StatusLine und Tastatur überein.
2. **Given** druckbare Zeichen, Navigation, Funktionstasten oder Modifier-Kombinationen, **When** sie am realen Ingress eintreffen, **Then** entstehen dieselben kanonischen Tasten- und Modifierwerte wie im Compatibility-Vertrag.
3. **Given** eine unbekannte oder nicht darstellbare Eingabe, **When** sie übersetzt wird, **Then** bleibt der Fallback deterministisch und behauptet keine ausführbare Aktion.

### User Story 5 - Begrenzte Drag-Interaktion mit Tastaturalternative (Priority: P2)

Als Nutzer kann ich eine View innerhalb klarer Grenzen ziehen, abbrechen oder
dieselbe Bewegung vollständig per Tastatur ausführen. Lifecycle-Verlust führt
nicht zu einem hängenden Capture-Zustand.

*As a user, I can drag a view within explicit bounds, cancel the operation, or
perform the same move by keyboard without leaving stale capture state.*

**Why this priority**: Der Vertrag ist für kommende Desktop-Verbraucher
wichtig, baut aber auf den vorher geschlossenen Event-, Fokus- und
Lifecycle-Grenzen auf.

**Independent Test**: Titelzeilen-Drag und ein generischer View-Drag laufen
durch die reale Schleife; Startschwelle, Capture, Bounds, Zielprüfung, Drop,
Escape, Owner-Verlust und Tastaturäquivalent werden sichtbar bewiesen.

**Acceptance Scenarios**:

1. **Given** eine drag-fähige View, **When** die Startschwelle erreicht und die Bewegung gültig ist, **Then** wird genau eine begrenzte Session mit eindeutigem Ergebnis abgeschlossen.
2. **Given** Escape, Owner-Verlust oder nicht mehr verfügbare Eingabefähigkeit, **When** die Session aktiv ist, **Then** wird sie atomar abgebrochen und Capture freigegeben.
3. **Given** keine Zeigerbedienung, **When** der dokumentierte Tastaturpfad verwendet wird, **Then** entsteht derselbe begrenzte Endzustand.

### User Story 6 - Findings nachvollziehbar schließen (Priority: P2)

Als Maintainer kann ich jedes Finding `F001` bis `F009` vom historischen Zweck
über die moderne C#-Entscheidung bis zum Red-Proof und bestandenen Real-Path-
Proof nachvollziehen.

*As a maintainer, I can trace each accepted finding from historical intent and
secondary comparison to the modern decision and executable proof.*

**Why this priority**: Die Basisfreigabe für Feature 026, Feature 028 und die
späteren Waves darf nicht auf implizitem Sitzungswissen beruhen.

**Independent Test**: Eine Vollständigkeitsprüfung weist für alle neun
Finding-IDs genau eine Abschlussentscheidung und alle Pflichtfelder nach, ohne
verwaiste oder doppelte Zeilen.

## Edge Cases

- Eine Event-Maske enthält eine konkrete Art zusammen mit einem Kategoriebit.
- Ein Fokus-Veto tritt ein, während das bisherige Ziel entfernt oder deaktiviert wird.
- Das angeforderte Fokusziel ist bereits aktuell, unsichtbar, nicht auswählbar oder außerhalb derselben Hierarchie.
- Ein Group besitzt kein aktuelles Kind oder das aktuelle Kind wird während einer Zustandsänderung entfernt.
- Idle-Arbeit stellt selbst ein Ereignis bereit, fordert Shutdown an oder wird wiederholt ohne neue Eingabe erreicht.
- Der Desktop ist leer, enthält nur nicht schließbare Fenster oder verliert das aktive Fenster während Tile/Cascade.
- Eine modale Session wird verschachtelt, ihr Owner entfernt oder die Anwendung während der Session beendet.
- Ein Command wird zwischen sichtbarer Abfrage und Ausführung durch einen Kontextwechsel deaktiviert.
- Das Terminal liefert unbekannte Tasten, widersprüchliche Modifier oder einen plattformspezifisch nicht darstellbaren Wert.
- Eine Drag-Session verlässt Owner-Bounds, verliert Ziel oder Owner, erhält doppelte Releases oder wird vor Erreichen der Schwelle abgebrochen.
- Eine historische oder Free-Vision-Variante widerspricht einer akzeptierten öffentlichen TuiVision-Semantik.

## Requirements

### Functional Requirements

- **FR-001**: Event-Factories MUST genau eine konkrete bekannte Eventart akzeptieren; Kategorien, Composite-Masken, gemischte Kanäle und unbekannte Werte MUST vor Dispatch deterministisch abgelehnt werden (`F001`, `C004`, `R-025-001`).
- **FR-002**: Bestehende konkrete None-, Key-, Mouse-, Command- und Broadcast-Pfade MUST kompatibel bleiben.
- **FR-003**: Der generische Fokusvertrag MUST vor einem Fokuswechsel einen geordneten Veto-Punkt anbieten und Annahme, Ablehnung und No-op eindeutig unterscheiden (`F002`, `C008`, `R-025-002`).
- **FR-004**: Bei abgelehntem Fokuswechsel MUST der bisherige Fokus samt Eingabedaten, sichtbarer Rückmeldung und hierarchischem Zustand konsistent bleiben; die konkrete InputLine-/Validator-Integration bleibt Feature 026.
- **FR-005**: `TGroup` MUST `Focused` nur dem aktuellen Kind zuordnen und für jeden weiteren propagierten Zustand eine explizite, historisch verglichene Regel besitzen (`F003`, `C009`, `R-025-003`).
- **FR-006**: Schutztests, die bekannte uniforme Fehlpropagierung erwarten, MUST test-first auf den akzeptierten Vertrag umgestellt werden und dürfen den alten Fehler nicht konservieren.
- **FR-007**: Die reale Anwendungsschleife MUST einen deterministischen, begrenzten Idle-Hook ausführen, wenn kein Ereignis ansteht, ohne echte Eingabe zu verdrängen, Nebenläufigkeit einzuführen oder einen Busy Loop zu erzeugen (`F004`, `C013`, `R-025-004`).
- **FR-008**: Idle-Proofs MUST Ereignisreihenfolge, wiederholte Leerlaufaktualisierung, bereitgestellte Ereignisse, Shutdown und die Pending-Event-Grenze prüfen.
- **FR-009**: Das Framework MUST eine kleine gemeinsame Desktop-Grenze für Window-Insertion, Top-/Next-Window, Tile, Cascade und sicheres Close-All bereitstellen (`F005`, `C014`, `R-025-005`).
- **FR-010**: Desktop-Operationen MUST leeren Desktop, Bounds, Fokus, Z-Order, nicht schließbare Fenster und Lifecycle-Verlust deterministisch behandeln; anwendungsspezifische Fenstertypen bleiben außerhalb des Frameworks.
- **FR-011**: Der Standardpfad für `cmClose`, Ctrl+W und den anwendbaren Escape-Pfad MUST ein Fenster sichtbar aus seinem Owner entfernen, sofern kein Safe-Close-Veto greift (`F006`, `C015`, `R-025-006`).
- **FR-012**: Modale Ausführung MUST Ergebnis, Event-Isolation, begrenzte Verschachtelung, Abbruch, Shutdown und Wiederherstellung des vorherigen Fokus beweisen; ein nur gesendetes Signal ist kein Abschlussnachweis.
- **FR-013**: Command-Verfügbarkeit MUST aus einer gemeinsamen, testbaren Kontextquelle ableitbar sein und nach Fokus-, Auswahl- oder Window-Wechsel für aktives View, Menü, StatusLine und Tastatur übereinstimmen (`F007`, `C017`, `R-025-007`).
- **FR-014**: Lokale Darstellungszustände dürfen erhalten bleiben, MUST aber aus der gemeinsamen Command-Quelle entstehen und dürfen keine zweite Wahrheitsquelle bilden.
- **FR-015**: Der reale `ConsoleKeyInfo`- oder gleichwertige Adapterrand MUST dieselbe kanonische Übersetzung und dieselben Modifier-Bits wie Compatibility-Tests und weitere Adapter verwenden (`F008`, `C034`, `R-025-008`).
- **FR-016**: Real-Ingress-Proofs MUST druckbare Zeichen, Navigation, Funktionstasten, Alt, Ctrl, Shift, Ctrl+W, Alt-Shortcuts, unbekannte Eingaben und Terminal-Fallbacks abdecken; bereits normalisierte `TEvent`-Einspeisung ist nur ergänzend.
- **FR-017**: Das Framework MUST eine kleine allgemeine Drag-Session für Views mit Startschwelle, Capture, Bewegung, Grenzen, Zielprüfung, Drop-Ergebnis, Escape-Abbruch, Owner-/Lifecycle-Verlust und Tastaturäquivalent bereitstellen (`F009`, `C036`, `R-025-009`).
- **FR-018**: Titelzeilen-Drag MUST ein konkreter Nutzer des gemeinsamen Vertrags bleiben; ein vollständiges Desktop-Drag-and-Drop-Protokoll ist nicht erforderlich.
- **FR-019**: Für jedes Finding MUST vor der Verhaltensänderung ein reproduzierbarer Red-Proof gegen den realen Pfad vorliegen.
- **FR-020**: Event-loop-Proofs MUST `Run()` oder die gleichwertige reale Schleife ausführen; Close-, Modal-, Stack- und Drag-Proofs MUST konkreten Zustand, View-Tree, Fokus und Buffer-/Cell-Ausgabe prüfen.
- **FR-021**: Jedes Finding `F001` bis `F009` MUST genau eine Abschlussentscheidung `Implemented` oder `AlreadySatisfied` mit Red-Proof, Änderung beziehungsweise Unverändert-Begründung, Real-Path-Proof, historischem Intent, Free-Vision-Relation, API-/A11Y-Auswirkung und Restgrenze besitzen.
- **FR-022**: `FollowUpHardening` MUST für neu entdeckte, außerhalb 025 liegende Themen verwendet werden und darf kein Finding `F001` bis `F009` als geschlossen markieren.
- **FR-023**: Ein notwendiges Breaking Change oder ein Konflikt mit akzeptierter öffentlicher Semantik MUST als `ProductDecision` dokumentiert werden und die autonome Verhaltensänderung stoppen.
- **FR-024**: Turbo Vision 2.0.3 und passende Header unter `tv203s/` MUST als read-only Primärquelle geprüft werden; der gepinnte Free-Vision-Commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` MUST als externe, untracked Zweitmeinung verwendet werden.
- **FR-025**: C++- und Pascal-Code MUST NICHT mechanisch übersetzt, vendort oder verändert werden; moderne C#-Abweichungen MUST mit Nutzer-/API-Auswirkung begründet werden.
- **FR-026**: Neue oder geänderte öffentliche APIs MUST additiv sein und vollständige DE-first/EN-second XML-Dokumentation besitzen.
- **FR-027**: Neue oder geänderte nicht-triviale Logik MUST auf didaktischen Kommentarwert geprüft werden; Kommentare erklären Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze statt triviales Was.
- **FR-028**: Jeder Mouse- oder Drag-Pfad MUST eine vollständige Tastaturalternative besitzen; Fokuswechsel und wesentliche Zustandsänderungen MUST text-first und für `TFocusAnnouncement` nachvollziehbar bleiben.
- **FR-029**: Unbekannte Events, ungültige Zustandsübergänge und fehlerhafte Ingress-Werte MUST fail-closed behandelt werden.
- **FR-030**: Plattformproofs MUST macOS, Linux und Windows/WSL für berührte Keyboard-, Terminal- und Modifierpfade berücksichtigen; eine erforderliche nicht verfügbare Plattform bleibt als blockierender Proof offen.
- **FR-031**: Targeted Release, full Release, kanonische Assembly-Coverage, Format und alle ausgelösten DocFX-, A11Y- und Plattform-Gates MUST bestehen.
- **FR-032**: Alle fünf gate-relevanten Assemblies MUST mindestens 70 Prozent Zeilenabdeckung erreichen.
- **FR-033**: Feature 024 MUST erst nach bestandenem Proof aktualisiert werden; Feature 026 bleibt der nächste Intake und Wave 5 sowie Wave 6 bleiben bis Feature 028 blockiert.
- **FR-034**: `docs/project-statistics.md`, Pflichtenheft-Marker, Reihenfolge und Agent-Kontexte MUST am Abschluss konsistent sein; alle fünf Agent-Guidance-Oberflächen werden nur bei gemeinsam geänderter Guidance atomar aktualisiert.
- **FR-035**: `TVDEMOS/`, `TVFM/`, `tv203s/`, externe Free-Vision-Quellen, Wave-Anwendungen und generierte Ausgaben MUST unverändert bleiben.
- **FR-036**: Neue Runtime-Abhängigkeiten, breite Framework-Neuschreibung, Findings `F010` bis `F013` und pointer-only Interaktion MUST außerhalb des Feature-Diffs bleiben.
- **FR-037**: Alle Governance-Entscheidungen MUST `Applicable`, `N/A` oder `Open` verwenden und Owner, Reviewer, Datum, Ergebnis, Restrisiko, Evidence sowie Re-Evaluation-Trigger enthalten.

### Constitution Requirements

- **CR-001**: TuiVision ist ein registriertes Level-2-C#/.NET-Projekt und MUST Constitution, `AGENTS.md`, verbindliches Lastenheft und lokale Preset-Matrix als Projektkontext verwenden.
- **CR-002**: Lern- und nutzergerichtete Erklärungen MUST DE-first/EN-second auf CEFR-B2-Niveau und text-first für assistive Umgebungen sein.
- **CR-003**: Neue öffentliche Verträge, Guides oder Navigation MUST den vorhandenen DocFX-, Playwright-/Axe- und textorientierten Reviewpfad auslösen.
- **CR-004**: Statistik und Agent-Guidance MUST auf Synchronisationsbedarf geprüft werden; `.specify/templates/` bleiben `N/A`, sofern keine projektweite Vorlage bewusst betroffen ist.
- **CR-005**: C#/.NET bleibt die einzige Implementierungssprache und steht auf der Memory-Safe-Language-Allowlist; C/C++ und Pascal sind ausschließlich read-only Evidence.
- **CR-006**: NIST SSDF und CWE Top 25 MUST für die Verhaltens-, Eingabe- und Teständerungen angewendet werden; weitere Security-Standards erhalten triggerbasierte Entscheidungen.
- **CR-007**: OWASP ASVS MUST `N/A` bleiben, solange kein Web-, HTTP-, API-, Auth- oder Session-Service entsteht.
- **CR-008**: SBOM, VEX, SLSA, OpenSSF Scorecard und Release-Provenance MUST nur bei neuer Abhängigkeit, Distribution oder Lieferkettenänderung ausgelöst werden.
- **CR-009**: AI bleibt Entwicklungswerkzeug; AI-SBOM MUST `N/A` bleiben, solange kein Modell, Datensatz, AI-Service, Inference-Betrieb oder ausgelieferter AI-Bestandteil entsteht.
- **CR-010**: STRIDE, CIA und CAPEC MUST Event-, Fokus-, Lifecycle-, Command-, Ingress- und Drag-Grenzen betrachten; Zero Trust, Cloud- und Provider-Modelle bleiben ohne entsprechenden Trigger `N/A`.
- **CR-011**: Die Governance-Evidence MUST die vorhandenen `docs/security/`-Nachweise oder eine explizit begründete Feature-Evidence verwenden.
- **CR-012**: Die sechs Basis-Presets und das optionale `autonomous-run-governance` v0.1.0 MUST getrennt mit ihren lokalen Versionen bewertet werden.
- **CR-013**: Scope, Stop-Grenzen und Entscheidungsmodelle MUST fachliche Anforderungen von operativer Remote-Autorität trennen; `MergeAndSync` wird erst in Plan und Run-Evidence angewendet.

### Governance Applicability

- **Security Governance v0.6.0**: NIST SSDF, CWE Top 25, robuste Eingabevalidierung, fail-closed Zustandsgrenzen und Evidence-Integrität sind `Applicable`. ASVS ist ohne Web/Auth `N/A`. SBOM, VEX, SLSA, OpenSSF, AI-SBOM sowie NIS2, CRA, EU AI Act und DORA bleiben mit Re-Evaluation-Trigger `N/A`, solange keine Abhängigkeit, Distribution, Runtime-AI oder regulierte Dienstgrenze entsteht.
- **Architecture Governance v0.5.0**: STRIDE, CIA und CAPEC sind für Event-, State-, Lifecycle-, Command-, Keyboard- und Drag-Grenzen `Applicable`. S-ADR und arc42 werden nur bei materieller öffentlicher Architekturentscheidung ausgelöst. Zero Trust, SAMM, BSI C3A und BSI C5 sind ohne verteilte, Cloud-, Provider- oder Deploymentgrenze `N/A`.
- **iSAQB Architecture Governance v0.2.0**: Qualitätsszenarien, kleine koharente Verantwortungen, historische Intent-Zuordnung, Risiken und bewusste moderne Abweichungen sind `Applicable`; eine breite Neustrukturierung bleibt ausgeschlossen.
- **A11Y Governance v0.4.0**: Tastaturalternative, Fokus- und Zustandsankündigung, text-first Buffer-/Cell-Proof, zweisprachige Dokumentation und didaktische Kommentarprüfung sind `Applicable`.
- **Cross-Platform Governance v0.2.0**: macOS-, Linux- und Windows/WSL-Verhalten berührter Keyboard-, Modifier- und Terminalpfade ist `Applicable`. Neue Skripte sind nicht geplant; Bash-/PowerShell-Parität bleibt `N/A`, solange kein Script angelegt oder geändert wird.
- **Agent Parity Governance v0.3.0**: Die fünf gepflegten Agent-Oberflächen werden gemeinsam bewertet und nur bei geteilter Guidance geändert. `.specify/templates/` bleiben `N/A`, sofern keine wiederverwendbare Projektvorlage absichtlich geändert wird.
- **Autonomous Run Governance v0.1.0**: Evidence-first-Ausführung, wiederholte Clarify-/Analyze-Konvergenz, Resume-Fähigkeit, No-empty-PR und die explizit autorisierte `MergeAndSync`-Lieferung sind `Applicable`. Jeder neu betroffene deterministische Validator muss vor dem Edit als Trigger erfasst werden.

### Key Entities

- **Event Boundary**: Erzeugungsrand mit konkreter Art, Kanal, Payload und Ablehnungsgrund.
- **Focus Transition Decision**: Angeforderter Wechsel, bisheriges und neues Ziel sowie Ergebnis `Accepted`, `Rejected` oder `NoOp`.
- **View State Rule**: Zustand, Propagationsrichtung, betroffene Kinder und historische beziehungsweise moderne Begründung.
- **Idle Cycle**: Schleifendurchlauf mit Pending-Event-Status, ausgeführter Arbeit und Shutdown-Ergebnis.
- **Desktop Stack Operation**: Insert-, Top-, Next-, Tile-, Cascade- oder Close-All-Aktion mit Z-Order-, Fokus- und Bounds-Ergebnis.
- **Modal Session**: Owner, vorheriger Fokus, Verschachtelung, Ergebnis, Abbruch- und Wiederherstellungszustand.
- **Command Context**: Gemeinsame Momentaufnahme der aktiven View-, Auswahl- und Window-Bedingungen sowie daraus abgeleiteter Commands.
- **Keyboard Ingress Translation**: Rohes Terminalereignis, kanonische Taste, Modifier, Fallback und Plattformgrenze.
- **Drag Session**: Quelle, Startpunkt, Capture, Bounds, Ziel, Tastaturmodus und Abschluss- oder Abbruchergebnis.
- **Finding Evidence**: Finding-ID, Red-Proof, Änderung, Real-Path-Proof, historische Absicht, Free-Vision-Relation, Auswirkungen und Restgrenze.
- **Governance Decision**: `Applicable`, `N/A` oder `Open` mit vollständiger Review- und Re-Evaluation-Evidence.

## Success Criteria

### Measurable Outcomes

- **SC-001**: Alle neun Findings `F001` bis `F009` besitzen genau eine erlaubte Abschlussentscheidung und vollständige, eindeutig zuordenbare Evidence; es gibt null doppelte, ausgelassene oder nur durch Kommentar geschlossene Findings.
- **SC-002**: 100 Prozent konkreter bekannter Eventarten bleiben kompatibel; 100 Prozent getesteter Kategorien, Composite-Masken, gemischter Kanäle und unbekannter Werte werden vor Dispatch abgelehnt.
- **SC-003**: Alle getesteten angenommenen, abgelehnten und No-op-Fokuswechsel sowie alle dokumentierten View-State-Regeln enden mit konsistentem Fokus, Zustand, Daten- und Sichtbarkeitsproof.
- **SC-004**: Event-loop-Proofs weisen echte-Eingabe-vor-Idle, wiederholtes Idle, pending Events und Shutdown deterministisch nach, ohne beobachteten Busy Loop oder verdrängte Eingabe.
- **SC-005**: Desktop-, Stack-, Close- und Modal-Matrizen bestehen für normalen, leeren, Veto-, Abbruch-, Verschachtelungs- und Shutdown-Pfad mit Zustand, View-Tree, Fokus und Buffer-/Cell-Proof.
- **SC-006**: Nach jedem geprüften Fokus-, Auswahl- oder Window-Wechsel stimmen 100 Prozent der abgefragten Command-Zustände von aktivem View, Menü, StatusLine und Tastatur überein.
- **SC-007**: Die reale Keyboard-Ingress-Matrix besteht für alle geforderten Tastenklassen, Modifier, Ctrl+W, Alt-Shortcuts, unbekannte Eingaben und anwendbare Plattform-Fallbacks.
- **SC-008**: Die Drag-Matrix besteht für Maus- und Tastaturpfad, Startschwelle, Bounds, Ziel, Drop, Escape, Owner-/Lifecycle-Verlust und Capture-Freigabe.
- **SC-009**: Targeted und full Release bestehen; Core, Controls, Serialization, Compatibility und Drivers.Console erreichen jeweils mindestens 70 Prozent Zeilenabdeckung.
- **SC-010**: 100 Prozent neuer oder geänderter öffentlicher APIs besitzen vollständige zweisprachige XML-Dokumentation, und alle dadurch ausgelösten DocFX-/A11Y-Prüfungen bestehen.
- **SC-011**: Der finale Diff enthält null Wave-5-/Wave-6-Anwendungslogik, null Änderungen an `TVDEMOS/`, `TVFM/`, `tv203s/` oder Free Vision, null neue Abhängigkeiten und null unentschiedene Breaking Changes.
- **SC-012**: Alle Governance- und Plattformentscheidungen besitzen Evidence, Owner, Review, Restrisiko und Re-Evaluation-Trigger; es verbleiben keine Clarification-, TODO-, TBD- oder Platzhaltermarker.

## Assumptions

- Die neun Findings und ihre Contract-IDs aus Audit Revision 2 sind akzeptiert und werden nicht neu priorisiert oder zusammengelegt.
- Bestehende TuiVision-Tests und öffentliche Semantik bleiben kompatibel, sofern ein Finding keine additive Erweiterung verlangt.
- Ein Safe-Close-Veto kann den Abschluss verhindern; ohne Veto muss der sichtbare Lifecycle abgeschlossen werden.
- Plattformproofs dürfen vorhandene CI-Runner und dokumentierte manuelle Windows-/WSL-Evidence kombinieren, sofern keine erforderliche Plattform still als bestanden markiert wird.
- Der gepinnte Free-Vision-Stand bleibt extern abrufbar; eine Nichtverfügbarkeit blockiert die betroffene Zweitmeinung statt eine andere Revision still zu verwenden.
- Operative Remote-Autorität ist für diesen Lauf als `MergeAndSync` erteilt, gehört jedoch nicht zum Produktvertrag.

## Scope Boundaries

### In Scope

- Findings `F001` bis `F009` und ihre neun Runtime-/Proof-Verträge
- Kleine additive öffentliche APIs, falls ein Finding anders nicht als wiederverwendbarer Frameworkvertrag geschlossen werden kann
- Zugehörige Core-, Controls-, Compatibility- und notwendige Console-Driver-Änderungen
- Test-first Real-Path-, App-Loop-, View-Tree-, Fokus- und Buffer-/Cell-Proofs
- Historischer `tv203s`-Intent und externe gepinnte Free-Vision-Zweitmeinung
- XML-, Guide-, Audit-, Governance-, Agent-, Statistik- und PR-Evidence, soweit durch den Feature-Diff ausgelöst

### Out of Scope

- Findings `F010` bis `F013`, Dialog-/InputLine-/Datei-/Ressourcenhärtung aus Feature 026
- Feature 028, Wave-5- oder Wave-6-Beispiele und deren Anwendungslogik
- Änderungen an `TVDEMOS/`, `TVFM/`, `tv203s/` oder externen Free-Vision-Quellen
- Mechanische C++-/Pascal-Übersetzung, binäre Turbo-Vision-Kompatibilität, Pointer- oder DOS-Speichermodelle
- Neue Runtime-Abhängigkeiten, breite Framework-Neuschreibung oder neue Architektur-Schicht ohne Finding-Bezug
- Vollständiges Desktop-Drag-and-Drop-Protokoll oder pointer-only Bedienung
- Autonome Entscheidung eines Breaking Changes oder einer destruktiven Produktsemantik

### Decision and Follow-up Model

- Finding-Abschlüsse sind genau `Implemented` oder `AlreadySatisfied`.
- `AlreadySatisfied` verlangt einen neuen Real-Path-Proof und eine ausdrückliche Unverändert-Begründung.
- `FollowUpHardening` ist nur für neu entdeckte Themen außerhalb 025 zulässig und schließt kein akzeptiertes Finding.
- `ProductDecision` kennzeichnet ein Breaking Change oder einen Konflikt mit akzeptierter öffentlicher Semantik und stoppt die autonome Verhaltensänderung.
- Governance-Anwendbarkeit ist genau `Applicable`, `N/A` oder `Open` und ersetzt niemals eine Finding-Entscheidung.

## Dependencies and Ordering

- Feature 025 läuft nach Feature-024 Audit Revision 2.
- Feature 026 folgt nach erfolgreichem Abschluss von Feature 025.
- Feature 028 ist das gemeinsame Pre-Wave-5-Closure-Gate nach 025 und 026.
- Wave 5 und Wave 6 bleiben bis zur erfolgreichen Feature-028-Freigabe blockiert.
