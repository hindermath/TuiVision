# Feature Specification: Component and Data Conformance Hardening

**Feature Branch**: `026-component-data-conformance-hardening`
**Created**: 2026-07-13
**Status**: Accepted
**Binding Input**: `Lastenheft_11_Component-Data-Conformance-Hardening.md`

## Clarifications

### Session 2026-07-13

- Q: Welche Commands bilden den kompatiblen Standard-Satz für Dialogabschluss? A: `cmOK`, `cmCancel`, `cmYes` und `cmNo` sind die zentralen Standard-Completion-Commands; ein abgeleiteter Dialog darf den Satz über einen expliziten Hook erweitern, ohne andere Commands pauschal als Abschluss zu behandeln.
- Q: In welcher Reihenfolge werden relevante Dialogkinder validiert? A: In stabiler Owner-/View-Reihenfolge vom ersten zum nächsten relevanten Kind; die Prüfung endet beim ersten ablehnenden Kind, das Fokus und Fehler-Evidence erhält.
- Q: Wie wird der File-Dialog-Ausgang gegenüber dem Aufrufer dargestellt? A: Ein geschlossener typisierter Ergebnisvertrag unterscheidet Navigation, Filter, Open, Save, Overwrite-Entscheidung, Rejection und Cancel; er führt selbst keine Dateioperation aus.
- Q: Wann darf eine geladene UI-Resource sichtbar werden? A: Erst nachdem Parse, Version, Grenzen, Typ-Allowlist, Commands und Graph vollständig validiert und rekonstruiert wurden; bei jedem Fehler bleibt der vorherige Zustand atomar unverändert.

## User Scenarios & Testing

### User Story 1 - Dialoge nur nach gültiger Entscheidung abschließen (Priority: P1)

Als tastaturnutzende Person kann ich einen Dialog bestätigen, abbrechen oder
weiter bedienen, ohne dass Navigation, Hilfe oder ein unverwandter Command den
Dialog versehentlich beendet. Bei ungültiger Eingabe bleibt der Dialog offen,
der relevante Fokus ist erkennbar und meine Eingabe bleibt erhalten.

*As a keyboard user, I can confirm, cancel, or continue using a dialog without
navigation, help, or unrelated commands closing it. Invalid input keeps the
dialog open, identifies the relevant focus, and preserves entered data.*

**Why this priority**: Der Dialogabschluss ist die gemeinsame Grenze für
Validation, Dateientscheidungen und spätere Wave-Anwendungen. Ein falscher
Abschluss kann ungültigen Zustand akzeptieren oder Nutzerarbeit verlieren.

**Independent Test**: Der reale Dialogpfad erhält bestätigende, abbrechende,
navigierende, Hilfe- und unbekannte Commands. Er prüft Abschlusszustand, Fokus,
Kindreihenfolge, Eingabedaten, View-Tree und text-first sichtbare Ablehnung.

**Acceptance Scenarios**:

1. **Given** ein gültiger Dialog, **When** ein zentral erlaubter bestätigender Completion-Command eintrifft, **Then** validiert der Dialog seine relevanten Kinder und schließt mit dem eindeutigen Ergebnis.
2. **Given** ein Dialog, **When** Cancel eintrifft, **Then** schließt er ohne Inhaltsvalidierung, sofern kein separater Safe-Close-Vertrag greift.
3. **Given** Navigation, Hilfe, ein Anwendungscommand oder ein unbekannter Command, **When** der Dialog ihn verarbeitet, **Then** bleibt er offen oder leitet ihn geordnet weiter.
4. **Given** ein ablehnendes Kind, **When** ein bestätigender Abschluss versucht wird, **Then** bleibt der Dialog offen und erhält Eingabe, Zustand, Fokus sowie verständliche Fehler-Evidence.

### User Story 2 - Eingaben über einen expliziten Validator absichern (Priority: P1)

Als Framework-Nutzerin kann ich einer Eingabezeile optional einen Validator
zuordnen. Editieren, Fokusverlust und bestätigender Abschluss verwenden einen
einheitlichen, typsicheren Vertrag, ohne Text, Auswahl oder Cursor bei einer
Ablehnung teilweise zu beschädigen.

*As a framework consumer, I can attach an optional validator to an input line.
Editing, focus loss, and affirmative acceptance use one type-safe contract
without partially corrupting text, selection, or cursor state on rejection.*

**Why this priority**: `F011` ist ein High-Finding und die Validator-Grenze ist
Voraussetzung für wiederverwendbare Dialog-, Datei- und Formulareingaben.

**Independent Test**: Eine reale `TInputLine` durchläuft Edit-/Syntaxprüfung,
Fokus-Veto sowie Dialog-Acceptance mit gültiger und ungültiger Eingabe. Der
Proof prüft Validator-Aufruf, Phase, Zustandserhalt, Fokus und Fehlertext.

**Acceptance Scenarios**:

1. **Given** eine Eingabezeile ohne Validator, **When** sie wie bisher verwendet wird, **Then** bleibt ihr kompatibles Verhalten erhalten.
2. **Given** eine Eingabezeile mit Validator, **When** Editier-, Fokus- oder Acceptance-Prüfung erforderlich ist, **Then** erhält der Validator die unterscheidbare Phase und ein eindeutiges Ergebnis.
3. **Given** eine Ablehnung, **When** der Produktionspfad sie verarbeitet, **Then** bleiben Text, Auswahl, Cursor und letzter gültiger Zustand konsistent und der Fokuswechsel beziehungsweise Abschluss wird abgelehnt.

### User Story 3 - Dateiresultate nach Operationsmodus unterscheiden (Priority: P2)

Als aufrufende Komponente erhalte ich aus einer Dateiauswahl einen typisierten,
erklärbaren Zustand für Navigation, Filter, Open, Save, Ablehnung oder Cancel.
Ein vorhandenes Save-Ziel wird nicht still überschrieben, sondern verlangt eine
separate ausdrückliche Entscheidung der aufrufenden Anwendung.

*As a calling component, I receive a typed, explainable file-selection state for
navigation, filtering, open, save, rejection, or cancel. An existing save target
is never overwritten implicitly and requires a separate caller decision.*

**Why this priority**: Die Grenze verhindert versteckte I/O-Entscheidungen und
bereitet Wave 5 und Wave 6 vor, ohne deren Anwendungen oder destruktive
Operationen vorwegzunehmen.

**Independent Test**: Kontrollierte temporäre Verzeichnisse prüfen alle
geforderten Modi und Ablehnungen über den normalen Dialogpfad. Außerhalb der
Fixtures werden keine Dateien gelesen oder verändert.

**Acceptance Scenarios**:

1. **Given** ein bestehendes Open-Ziel, **When** die Auswahl bestätigt wird, **Then** entsteht ein typisiertes akzeptiertes Open-Ergebnis.
2. **Given** ein fehlendes Open-Ziel oder einen ungültigen manuellen Pfad, **When** bestätigt wird, **Then** bleibt der Dialog mit erklärbarer Ablehnung offen.
3. **Given** ein neues oder bestehendes Save-Ziel, **When** bestätigt wird, **Then** unterscheidet das Ergebnis Neuanlage und notwendige Overwrite-Entscheidung, ohne selbst zu schreiben.
4. **Given** Verzeichnisnavigation, Wildcard-Eingabe oder Cancel, **When** der Pfad verarbeitet wird, **Then** wird er nicht als erfolgreiche Dateioperation missdeutet.

### User Story 4 - Named UI Resources sicher rekonstruieren (Priority: P2)

Als Framework-Nutzer kann ich benannte Menü-, StatusLine- und Dialogstrukturen
aus einem versionierten, geschlossenen Beschreibungsvertrag rekonstruieren. Nur
registrierte Typen und gültige Commands werden akzeptiert; fehlerhafte Daten
erzeugen keinen teilweise sichtbaren oder gespeicherten Objektgraphen.

*As a framework consumer, I can reconstruct named menu, status-line, and dialog
structures from a versioned closed description contract. Only registered types
and valid commands are accepted, and malformed data never creates a partial
visible or persisted object graph.*

**Why this priority**: Ohne gemeinsame Resource-Komposition würden Wave-
Anwendungen historische Service-Locator oder lokale hart codierte Strukturen
erneut erfinden.

**Independent Test**: Roundtrip- und Rejection-Proofs prüfen exakte Keys,
Identity, Version, erlaubte Typen, Ownership und alle geforderten fehlerhaften
Eingaben ohne Reflection-, Assembly-Scanning- oder Partial-State-Pfad.

**Acceptance Scenarios**:

1. **Given** eine gültige benannte Menü-, StatusLine- oder Dialogbeschreibung, **When** sie gespeichert und geladen wird, **Then** bleiben exakter Key, Struktur, Command-Zuordnung und Eigentumsgrenze erhalten.
2. **Given** ein unbekannter Typ, eine unbekannte Version, ungültiger Command, Duplicate Key oder unzulässiger Graph, **When** die Resource gelesen wird, **Then** wird sie vollständig und deterministisch abgelehnt.
3. **Given** abgeschnittene oder nachlaufende Daten, **When** der Ladevorgang fehlschlägt, **Then** bleibt der zuvor gültige Resource-Zustand unverändert.

### User Story 5 - Vier Audit-Findings nachvollziehbar schließen (Priority: P2)

Als Maintainer kann ich für `F010` bis `F013` nachvollziehen, welcher historische
Zweck übernommen, welche Free-Vision-Zweitmeinung berücksichtigt, welcher
moderne C#-Vertrag gewählt und wie der reale Produktionspfad bewiesen wurde.

*As a maintainer, I can trace the historical intent, Free Vision second opinion,
modern C# contract, and real production-path proof for findings F010 through
F013.*

**Why this priority**: Feature 028 darf Wave 5 und Wave 6 nur auf Grundlage
vollständiger, reproduzierbarer Evidence freigeben.

**Independent Test**: Eine Finding-Matrix ordnet jedem Finding genau eine
Abschlussentscheidung, Red-/Green-Proof, Quellenrelation, Auswirkung,
Governance-Entscheidung und Restgrenze zu.

**Acceptance Scenarios**:

1. **Given** ein Finding `F010` bis `F013`, **When** die Feature-Evidence geprüft wird, **Then** existiert genau eine vollständige Abschlusszeile mit realem Proof.
2. **Given** ein neu entdecktes Thema außerhalb 026, **When** es bewertet wird, **Then** wird es als `FollowUpHardening` dokumentiert und schließt keines der vier Findings.

## Edge Cases

- Mehrere ungültige Kinder werden deterministisch geprüft; das erste ablehnende
  relevante Control erhält Fokus, ohne spätere Kinder als erfolgreich geprüft
  zu behaupten.
- Ein bereits fokussiertes ablehnendes Control erzeugt keinen Fokuszyklus und
  verliert seinen Text-, Auswahl- oder Cursorzustand nicht.
- Ein Validator darf bei Editieren, Fokusverlust und Dialog-Acceptance
  unterschiedliche Entscheidungen treffen; unbekannte Phasen werden nicht als
  Erfolg interpretiert.
- Relative Pfade, Root-Pfade, ungültige Zeichen, nicht vorhandene Eltern,
  Wildcards und plattformspezifische Pfadformen werden innerhalb der
  dokumentierten Operationsgrenze deterministisch klassifiziert.
- Ein Race zwischen Prüfung und späterem Speichern bleibt eine explizite
  Caller-/Safe-Save-Verantwortung; das File-Dialog-Ergebnis behauptet keine
  atomare Dateioperation.
- Leere, sehr große, zyklische, mehrfach referenzierte oder tief verschachtelte
  Resource-Beschreibungen überschreiten keine dokumentierte Größen- oder
  Graphgrenze und erzeugen keinen Partial State.
- License-, Generator-, Marker- und tool-owned Zeilen bleiben unverändert.

## Requirements

### Functional Requirements

- **FR-001**: Nur der zentrale, testbare Standard-Satz `cmOK`, `cmCancel`, `cmYes` und `cmNo` sowie explizit über einen Dialog-Hook ergänzte Completion-Commands MUST einen Dialog beenden; Navigation, Hilfe, Anwendungscommands und unbekannte Commands MUST offen bleiben oder geordnet weitergeleitet werden (`F010`, `C019`, `R-026-001`).
- **FR-002**: Bestätigende Completion-Commands MUST relevante Dialogkinder in stabiler Owner-/View-Reihenfolge validieren und beim ersten ablehnenden Kind stoppen; Cancel MUST ohne Inhaltsvalidierung schließen dürfen, sofern kein separater Safe-Close-Vertrag greift.
- **FR-003**: Das erste ablehnende relevante Control MUST Fokus erhalten oder behalten; Dialog, Eingabe, Auswahl, Cursor, sichtbare Fehler-Evidence und zuvor gültiger Zustand MUST erhalten bleiben (`R-026-002`).
- **FR-004**: Dialogtests MUST den realen `HandleEvent`- und Completion-Pfad verwenden; ein Testtyp darf keine nicht virtuelle Methode verstecken, um einen vom Produkt nicht aufgerufenen Pfad zu beweisen.
- **FR-005**: `TInputLine` MUST einen optionalen, expliziten und typsicheren Validator-Vertrag anbieten, ohne bestehende Nutzung ohne Validator zu brechen (`F011`, `C021`, `R-026-003`).
- **FR-006**: Der Validator-Vertrag MUST Edit-/Syntaxprüfung, Fokusverlust und bestätigende Dialog-Acceptance unterscheidbar machen; Transfer oder Commit MUST nur dann eine eigene Phase bilden, wenn dies für den gewählten modernen Vertrag erforderlich ist.
- **FR-007**: Validator-Ablehnung MUST zustandserhaltend und text-first erklärbar sein und MUST den Fokus-Veto-Vertrag aus Feature 025 verwenden statt ihn lokal zu duplizieren.
- **FR-008**: File-Dialog-Flows MUST Verzeichnisnavigation, Wildcard-/Filtereingabe, bestehendes Open-Ziel, fehlendes Open-Ziel, neues Save-Ziel, bestehendes Save-Ziel mit separater Overwrite-Entscheidung, ungültigen manuellen Pfad und Cancel unterscheiden (`F012`, `C023`, `R-026-004`).
- **FR-009**: Die Dialoggrenze MUST einen geschlossenen typisierten Vertrag für Navigation, Filter, Open, Save, erforderliche Overwrite-Entscheidung, Rejection und Cancel liefern; sie MUST weder versteckte I/O-Entscheidungen treffen noch eine Datei öffnen, speichern, überschreiben, löschen oder verschieben.
- **FR-010**: File-Proofs MUST ausschließlich Source-Fixtures oder testverwaltete temporäre Verzeichnisse und betriebssystemneutrale Pfadbildung verwenden; beliebige Benutzerdaten und historische Quellverzeichnisse bleiben unberührt.
- **FR-011**: Named Resources MUST sichere rekonstruierbare Menü-, StatusLine- und Dialogkomposition über unveränderliche Beschreibungen, Builder oder registrierte Records ermöglichen (`F013`, `C026`, `R-026-005`).
- **FR-012**: Der Resource-Vertrag MUST exakte case-sensitive Keys, eine geschlossene Typ-Allowlist, Formatversion, eindeutige Ownership und deterministische Command-Referenzen besitzen.
- **FR-013**: Parse, Version, Grenzen, Typ-Allowlist, Commands und Graph MUST vollständig validiert und rekonstruiert sein, bevor eine Resource sichtbar wird; Unknown Type, unsupported version, truncation, trailing data, duplicate key, invalid command reference, unzulässiger Graph sowie Größen- oder Tiefengrenzverletzung MUST atomar ohne Partial State abgelehnt werden.
- **FR-014**: Ressourceninput MUST als untrusted data behandelt werden; beliebige Reflection-Aktivierung, Assembly-Scanning, polymorphe Deserialisierung oder Ausführung persistierter Typnamen ist verboten.
- **FR-015**: Für jedes Finding MUST vor der Produktionsänderung ein reproduzierbarer Red-Proof gegen den realen Pfad aufgezeichnet werden; bestehende Tests allein dürfen den Red-Zustand nicht ersetzen.
- **FR-016**: Jedes Finding `F010` bis `F013` MUST genau eine Abschlussentscheidung `Implemented` oder `AlreadySatisfied` mit Red-Proof, Änderung beziehungsweise Unverändert-Begründung, Green-Proof, historischem Intent, Free-Vision-Relation, Consumer-Grenze, API-/A11Y-Auswirkung und Restrisiko besitzen.
- **FR-017**: `AlreadySatisfied` MUST einen neuen Real-Path-Proof und eine ausdrückliche Unverändert-Begründung besitzen; `FollowUpHardening` darf nur neu entdeckte Themen außerhalb 026 aufnehmen und kein akzeptiertes Finding schließen.
- **FR-018**: Ein erforderliches Breaking Change, unklare Datenformatkompatibilität, beliebige Runtime-Typaktivierung oder destruktive Produktpolicy MUST als `ProductDecision` dokumentiert werden und die autonome Verhaltensänderung stoppen.
- **FR-019**: Relevante C/C++-Implementierungen und Header unter `tv203s/` MUST read-only geprüft werden; der für Feature 024 gepinnte Free-Vision-Commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` MUST über `FV006`, `FV007`, `FV010` und `FV012` als externe, unveränderte Zweitmeinung dienen.
- **FR-020**: `TVDEMOS/` und `TVFM/` MUST ausschließlich read-only Consumer-Evidence liefern; C++, Pascal und historische Formate dürfen nicht mechanisch übersetzt, vendort oder verändert werden.
- **FR-021**: Neue oder geänderte öffentliche APIs MUST additiv sein und vollständige DE-first/EN-second XML-Dokumentation erhalten; API-, XML-, Guide- oder Navigationsänderungen MUST DocFX, Playwright/Axe und text-first Review auslösen.
- **FR-022**: Validation und Rejection MUST vollständig per Tastatur erreichbar, mit eindeutigem Fokus und verständlicher text-first Rückmeldung beweisbar sein.
- **FR-023**: Neue oder geänderte nicht triviale Logik MUST auf didaktischen Kommentarwert geprüft werden; Kommentare erklären Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze statt triviales Was.
- **FR-024**: Targeted und full Release, kanonische Assembly-Coverage, Format und alle ausgelösten DocFX-, A11Y-, Security- und Plattform-Gates MUST bestehen; alle fünf gate-relevanten Assemblies MUST mindestens 70 Prozent Zeilenabdeckung erreichen.
- **FR-025**: Feature-024-Findingstatus MUST erst nach bestandenem Real-Path-Proof aktualisiert werden; Feature 028 bleibt danach der einzige nächste Intake und Wave 5 sowie Wave 6 bleiben bis zu dessen Freigabe blockiert.
- **FR-026**: `docs/project-statistics.md`, Pflichtenheft-Marker, Reihenfolge und Agent-Kontexte MUST am Abschluss konsistent sein; alle gepflegten Agent-Oberflächen werden nur bei tatsächlich geänderter gemeinsamer Guidance atomar aktualisiert.
- **FR-027**: `TVDEMOS/`, `TVFM/`, `tv203s/`, externe Free-Vision-Quellen, Wave-Anwendungen, generierte Ausgaben und neue Runtime-Abhängigkeiten MUST unverändert bleiben.
- **FR-028**: Alle Governance-Entscheidungen MUST genau `Applicable`, `N/A` oder `Open` verwenden und Owner, Reviewer, Datum, Ergebnis, Restrisiko, Evidence, Follow-up und Re-Evaluation-Trigger enthalten.

### Constitution Requirements

- **CR-001**: TuiVision ist ein registriertes Level-2-C#/.NET-Projekt und MUST Constitution, `AGENTS.md`, verbindliches Lastenheft und lokale Preset-Matrix als Projektkontext verwenden.
- **CR-002**: Lern- und nutzergerichtete Erklärungen MUST DE-first/EN-second auf CEFR-B2-Niveau und text-first für assistive Umgebungen sein.
- **CR-003**: Neue öffentliche Verträge, XML-Kommentare, Guides oder Navigation MUST den vorhandenen DocFX-, Playwright-/Axe- und textorientierten Reviewpfad auslösen.
- **CR-004**: Statistik und Agent-Guidance MUST auf Synchronisationsbedarf geprüft werden; `.specify/templates/` bleiben `N/A`, sofern keine projektweite Vorlage bewusst betroffen ist.
- **CR-005**: C#/.NET bleibt die einzige Implementierungssprache und steht auf der Memory-Safe-Language-Allowlist; C/C++ und Pascal sind ausschließlich read-only Evidence.
- **CR-006**: NIST SSDF und CWE Top 25 MUST für Eingabevalidierung, Zustandsintegrität, Persistenz- und Teständerungen angewendet werden; weitere Security-Standards erhalten triggerbasierte Entscheidungen.
- **CR-007**: OWASP ASVS MUST `N/A` bleiben, solange kein Web-, HTTP-, API-, Auth- oder Session-Service entsteht.
- **CR-008**: SBOM, VEX, SLSA, OpenSSF Scorecard und Release-Provenance MUST nur bei neuer Abhängigkeit, Distribution oder Lieferkettenänderung ausgelöst werden.
- **CR-009**: AI bleibt Entwicklungswerkzeug; AI-SBOM MUST `N/A` bleiben, solange kein Modell, Datensatz, AI-Service, Inference-Betrieb oder ausgelieferter AI-Bestandteil entsteht.
- **CR-010**: STRIDE, CIA und CAPEC MUST Dialogabschluss, Validation, Dateiresultat und untrusted Resource-Input betrachten; Zero Trust, Cloud- und Provider-Modelle bleiben ohne entsprechenden Trigger `N/A`.
- **CR-011**: Die Governance-Evidence MUST die vorhandenen `docs/security/`-Nachweise oder eine explizit begründete Feature-Evidence verwenden.
- **CR-012**: Die sechs Basis-Presets und das optionale `autonomous-run-governance` v0.1.2 MUST getrennt mit ihren lokalen Versionen bewertet werden.
- **CR-013**: Scope, Stop-Grenzen und Entscheidungsmodelle MUST fachliche Anforderungen von operativer Remote-Autorität trennen; `MergeAndSync` wird erst in Plan und Run-Evidence angewendet.

### Governance Applicability

- **Security Governance v0.6.0**: NIST SSDF, CWE Top 25, fail-closed Validation, untrusted Resource-Input und Evidence-Integrität sind `Applicable`. ASVS ist ohne Web/Auth `N/A`. SBOM, VEX, SLSA, OpenSSF, AI-SBOM sowie NIS2, CRA, EU AI Act und DORA bleiben mit Re-Evaluation-Trigger `N/A`, solange keine Abhängigkeit, Distribution, Runtime-AI oder regulierte Dienstgrenze entsteht.
- **Architecture Governance v0.5.0**: STRIDE, CIA und CAPEC sind für Completion-, Validation-, File-Result- und Resource-Trust-Grenzen `Applicable`. S-ADR und arc42 werden nur bei materieller öffentlicher Architekturentscheidung ausgelöst. Zero Trust, SAMM, BSI C3A und BSI C5 sind ohne verteilte, Cloud-, Provider- oder Deploymentgrenze `N/A`.
- **iSAQB Architecture Governance v0.2.0**: Qualitätsszenarien, kleine kohärente Verantwortungen, historische Intent-Zuordnung, Risiken und bewusste moderne Abweichungen sind `Applicable`; eine breite Neustrukturierung bleibt ausgeschlossen.
- **A11Y Governance v0.4.0**: Tastaturvollständigkeit, Fokus-Veto, text-first Rejection, zweisprachige öffentliche Dokumentation und didaktische Kommentarprüfung sind `Applicable`.
- **Cross-Platform Governance v0.2.0**: Betriebssystemneutrale Pfade und macOS-, Linux- sowie Windows/WSL-Proof sind `Applicable`. Neue Skripte sind nicht geplant; Bash-/PowerShell-Parität bleibt `N/A`, solange kein Script angelegt oder geändert wird.
- **Agent Parity Governance v0.3.0**: Die gepflegten Agent-Oberflächen werden gemeinsam bewertet und nur bei geteilter Guidance geändert. `.specify/templates/` bleiben `N/A`, sofern keine wiederverwendbare Projektvorlage absichtlich geändert wird.
- **Autonomous Run Governance v0.1.2**: Evidence-first-Ausführung, wiederholte Clarify-/Analyze-Konvergenz, exakte Staged-Candidate-Validierung, Zuordnung verpflichtender Gates zu tatsächlichen Workflow-/Job-/Runner-Semantiken, Resume-Fähigkeit, No-empty-PR und explizit autorisiertes `MergeAndSync` sind `Applicable`.

### Key Entities

- **Dialog Completion Decision**: Command, Completion-Klasse, Validationsbedarf, Ergebnis und Abschlusszustand.
- **Validation Phase**: Edit-/Syntaxprüfung, Fokusverlust, Acceptance sowie optionaler Transfer-/Commit-Punkt mit eindeutigem Ergebnis.
- **Validation Rejection**: Fehlertext, relevantes Control, Fokusziel und erhaltener Text-, Auswahl-, Cursor- und gültiger Zustand.
- **File Dialog Result**: Operationsmodus, normalisierter Kandidat, Zielklassifikation, Acceptance- oder Rejection-Grund und erforderliche Folgeentscheidung.
- **UI Resource Description**: Versionierte unveränderliche Beschreibung für Menü, StatusLine oder Dialog mit allowlisted Typen, Commands und Ownership.
- **Resource Load Transaction**: Exakter Key, Formatversion, Grenzen, Ergebnis und atomar erhaltener vorheriger Zustand.
- **Finding Evidence**: Finding-ID, Red-Proof, Änderung, Real-Path-Proof, historische Absicht, Free-Vision-Relation, Consumer-Grenze, Auswirkungen und Restrisiko.
- **Governance Decision**: `Applicable`, `N/A` oder `Open` mit vollständiger Review- und Re-Evaluation-Evidence.

## Success Criteria

### Measurable Outcomes

- **SC-001**: Alle vier Findings `F010` bis `F013` besitzen genau eine erlaubte Abschlussentscheidung und vollständige, eindeutig zuordenbare Red-/Green-Evidence; es gibt null doppelte, ausgelassene oder nur durch Kommentar geschlossene Findings.
- **SC-002**: 100 Prozent der zentral erlaubten getesteten Completion-Commands schließen im passenden Zustand; 100 Prozent der getesteten Navigation-, Hilfe-, Anwendungs- und unbekannten Commands schließen nicht.
- **SC-003**: 100 Prozent der getesteten ablehnenden Kind- und Validatorpfade lassen Dialog, Eingabe, Auswahl, Cursor, Fokus und zuletzt gültigen Zustand konsistent.
- **SC-004**: Die Validator-Matrix beweist unterscheidbare Edit-, Fokusverlust- und Acceptance-Phasen sowie kompatibles Verhalten ohne Validator über den realen Produktionspfad.
- **SC-005**: Die File-Dialog-Matrix besteht für alle acht geforderten Modi und Ablehnungen; null Proof-Schritte lesen beliebige Benutzerdaten oder führen destruktive Dateioperationen aus.
- **SC-006**: Gültige Menü-, StatusLine- und Dialogbeschreibungen bestehen Roundtrip und Rekonstruktion; 100 Prozent der geforderten malformed-, version-, type-, key-, command-, graph-, size- und depth-Fälle werden ohne Partial State abgelehnt.
- **SC-007**: Targeted und full Release bestehen; Core, Controls, Serialization, Compatibility und Drivers.Console erreichen jeweils mindestens 70 Prozent Zeilenabdeckung.
- **SC-008**: 100 Prozent neuer oder geänderter öffentlicher APIs besitzen vollständige zweisprachige XML-Dokumentation, und alle dadurch ausgelösten DocFX-/A11Y-Prüfungen bestehen.
- **SC-009**: Der finale Diff enthält null Wave-5-/Wave-6-Anwendungslogik, null Änderungen an `TVDEMOS/`, `TVFM/`, `tv203s/` oder Free Vision, null neue Abhängigkeiten und null unentschiedene Breaking Changes.
- **SC-010**: Alle Governance- und Plattformentscheidungen besitzen Evidence, Owner, Review, Restrisiko und Re-Evaluation-Trigger; es verbleiben keine Clarification-, TODO-, TBD- oder Platzhaltermarker.

## Assumptions

- Feature 025 ist vollständig gemergt und `main` war vor Branch-Erstellung synchron; damit ist die im Lastenheft formulierte zeitliche Startgrenze erfüllt. Die aktuelle ausdrückliche autonome Freigabe ersetzt das überholte Wort „heute“, nicht die fachliche Reihenfolge.
- Die vier Findings und Contract-IDs aus Audit Revision 2 sind akzeptiert und werden nicht neu priorisiert oder zusammengelegt.
- Bestehende öffentliche Semantik bleibt kompatibel; additive APIs sind zulässig, wenn ein Finding anders nicht als wiederverwendbarer Frameworkvertrag geschlossen werden kann.
- Save-Overwrite bleibt eine separate explizite Caller-Entscheidung und keine Aktion des File-Dialog-Resultats.
- Der Resource-Vertrag ist ein moderner, geschlossener Beschreibungsvertrag und keine binäre 1:1-Reproduktion historischer Objektgraphen.
- Der gepinnte Free-Vision-Stand bleibt extern verfügbar; Nichtverfügbarkeit blockiert die betroffene Zweitmeinung statt eine andere Revision still zu verwenden.
- Operative Remote-Autorität ist für diesen Lauf als `MergeAndSync` erteilt, gehört jedoch nicht zum Produktvertrag.

## Scope Boundaries

### In Scope

- Findings `F010` bis `F013` und ihre Dialog-, Validator-, Datei- und Resource-Verträge
- Kleine additive öffentliche APIs, wenn ein Finding anders nicht als wiederverwendbarer Frameworkvertrag geschlossen werden kann
- Zugehörige Controls-, Serialization- und erforderliche Integrationstests
- Test-first Real-Path-, Fokus-, View-Tree-, Buffer-/Cell-, Roundtrip- und Rejection-Proofs
- Historischer `tv203s`-Intent, gepinnte Free-Vision-Zweitmeinung und read-only Consumer-Evidence
- XML-, Guide-, Audit-, Governance-, Agent-, Statistik- und PR-Evidence, soweit durch den Feature-Diff ausgelöst

### Out of Scope

- Feature 028, Wave-5- oder Wave-6-Beispiele und deren Anwendungslogik
- Änderungen an `TVDEMOS/`, `TVFM/`, `tv203s/` oder externen Free-Vision-Quellen
- File Manager, Copy, Move, Delete, Trash, Provider-Integration oder andere destruktive Dateioperationen
- Mechanische C++-/Pascal-Übersetzung oder binäre historische Resource-Kompatibilität
- Beliebige Reflection-/Typaktivierung, Assembly-Scanning oder polymorphe Deserialisierung aus untrusted data
- Breite Serialization- oder Framework-Neuarchitektur, visuelle Neugestaltung oder neue Runtime-Abhängigkeiten
- Autonome Entscheidung eines Breaking Changes, unklarer Formatkompatibilität oder destruktiver Produktsemantik

### Decision and Follow-up Model

- Finding-Abschlüsse sind genau `Implemented` oder `AlreadySatisfied`.
- `AlreadySatisfied` verlangt einen neuen Real-Path-Proof und eine ausdrückliche Unverändert-Begründung.
- `FollowUpHardening` ist nur für neu entdeckte Themen außerhalb 026 zulässig und schließt kein akzeptiertes Finding.
- `ProductDecision` kennzeichnet ein Breaking Change, unklare Formatkompatibilität, beliebige Runtime-Typaktivierung oder destruktive Produktpolicy und stoppt die autonome Verhaltensänderung.
- Governance-Anwendbarkeit ist genau `Applicable`, `N/A` oder `Open` und ersetzt niemals eine Finding-Entscheidung.

## Dependencies and Ordering

- Feature 026 läuft nach vollständig gemergtem Feature 025 auf dessen Fokus-, Lifecycle-, Modal- und Command-Verträgen.
- Feature 028 ist der einzige nächste Intake und das gemeinsame Pre-Wave-5-Closure-Gate nach 025 und 026.
- Wave 5 und Wave 6 bleiben bis zur erfolgreichen Feature-028-Freigabe blockiert.
