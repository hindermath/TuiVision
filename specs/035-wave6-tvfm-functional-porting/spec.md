# Feature Specification: Wave-6 TVFM Functional Porting

**Feature Branch**: `035-wave6-tvfm-functional-porting`
**Created**: 2026-07-17
**Status**: Draft
**Binding Input**: `Lastenheft_20_Wave6-TVFM-Functional-Porting.035-wave6-tvfm-functional-porting.md`

## Klärungen / Clarifications

### Session 2026-07-17

- Keine formale Rückfrage ist erforderlich. Lastenheft 20 und die gemergte
  Wave-5-Closure legen Scope, Quelleninventar, Sicherheitsgrenzen,
  Entscheidungsvokabulare und Liefermodus vollständig fest.
- Feature 035 ist ausschließlich die funktionale erste Wave-6-Stufe. Feature
  036 und der Post-Wave-6-Portfolio-Audit werden weder angelegt noch gestartet.
- Die 24 historischen Dateien unter `TVFM/` bleiben read-only und erhalten
  jeweils genau eine Inventarrolle. Zusammengehörige Pascal-Units und
  Ressourcen dürfen in einer modernen Anwendung gebündelt werden.
- Alle Dateioperationen verwenden ausschließlich eine explizit kontrollierte
  Fixture- oder Testwurzel. Beliebige Benutzerdaten, externe Programme,
  Netzlaufwerke und globale Host-Einstellungen bleiben ausgeschlossen.
- Der normale Programmeinstieg verwendet einen dokumentierten,
  repository-eigenen Lernarbeitsbereich. Tests verwenden ausschließlich
  test-eigene temporäre Wurzeln. In beiden Fällen ist die Wurzel vor jeder
  Operation explizit gebunden und sichtbar.
- Die wiederholte Klärungsprüfung findet keine weitere planungswirksame Frage.
  Verzeichnis-Links und andere Reparse-Ziele werden nicht durchlaufen; ein
  Pfad, dessen kanonische Auflösung die kontrollierte Wurzel verlässt, wird
  atomar abgelehnt.

*No formal question is required. Feature 035 is the functional first Wave-6
stage, uses exactly 24 read-only historical source roles, and binds every file
operation to an explicit controlled root. It neither creates Feature 036 nor
starts the post-Wave-6 audit.*

## Nutzungsszenarien und Prüfung / User Scenarios and Testing

### User Story 1 - Kontrollierten Dateibaum erkunden (Priority: P1)

Als Lernender möchte ich einen kleinen, sichtbaren Dateibaum mit Verzeichnissen
und Dateien per Tastatur erkunden, damit ich die historische TVFM-Idee über
moderne TuiVision-Views nachvollziehen kann, ohne meine persönlichen Dateien
freizugeben.

*As a learner, I want to explore a small visible file tree by keyboard so I
can understand the historical TVFM intent through modern TuiVision views
without exposing my personal files.*

**Why this priority**: Navigation, Fokus, Dateiliste und kontrollierte Wurzel
bilden die Grundlage aller weiteren Dateioperationen.

**Independent Test**: Die Anwendung startet mit einer kontrollierten Fixture,
wechselt per realem Eventpfad in ein Unterverzeichnis und weist Auswahl,
Status, View-Identität und gerenderte Zellen nach.

**Acceptance Scenarios**:

1. **Given** eine kontrollierte Wurzel mit Unterverzeichnis und Dateien,
   **When** der Lernende per Tastatur navigiert, **Then** werden aktueller
   relativer Pfad, sortierte Einträge, Fokus und Status sichtbar aktualisiert.
2. **Given** einen leeren oder nicht mehr verfügbaren Ordner, **When** er
   gewählt wird, **Then** bleibt die Anwendung bedienbar und zeigt einen
   textlichen Leer- oder Fehlerzustand.
3. **Given** einen Pfad außerhalb der kontrollierten Wurzel, **When** eine
   Navigation versucht wird, **Then** wird sie ohne Zustandsübernahme
   abgelehnt.

---

### User Story 2 - Dateien filtern, markieren und sicher ansehen (Priority: P2)

Als Lernender möchte ich Dateien sortieren, filtern, markieren und als
begrenzte Text- oder Hex-Vorschau betrachten, damit Metadaten und
Darstellungsentscheidungen verständlich werden.

*As a learner, I want to sort, filter, tag, and inspect files through bounded
text or hexadecimal previews so metadata and presentation decisions are
understandable.*

**Why this priority**: Diese lesenden Pfade vermitteln den Kernnutzen eines
Dateimanagers mit geringerem Risiko als mutierende Operationen.

**Independent Test**: Eine Fixture mit Text- und Binärdatei wird gefiltert,
sortiert, markiert und über beide Viewer dargestellt; Inhalt und Grenze sind
in Zustand und Zellen nachweisbar.

**Acceptance Scenarios**:

1. **Given** mehrere kontrollierte Dateien, **When** Filter, Sortierung oder
   Markierung geändert werden, **Then** bleiben Auswahl und sichtbare
   Ergebnisreihenfolge deterministisch.
2. **Given** eine Textdatei, **When** die Textansicht gewählt wird, **Then**
   erscheint eine begrenzte UTF-8-Vorschau mit ehrlicher Ersatzdarstellung für
   ungültige Daten.
3. **Given** eine Binärdatei, **When** die Hexansicht gewählt wird, **Then**
   erscheinen Offset, Hexwerte und druckbare Zeichen innerhalb einer
   dokumentierten Größenbegrenzung.

---

### User Story 3 - Kontrolliert suchen und zuordnen (Priority: P3)

Als Anwendungsentwickler möchte ich eine begrenzte Suche sowie interne
Text-/Hex-Zuordnungen nachvollziehen, damit rekursive Arbeit, Abbruch und der
Verzicht auf externe Viewer klar erkennbar sind.

*As an application developer, I want to inspect bounded search and internal
text/hex associations so recursion, cancellation, and the exclusion of
external viewers are explicit.*

**Why this priority**: Suche und Zuordnungen verbinden mehrere
Framework-Flows und benötigen klare Ressourcen- und Sicherheitsgrenzen.

**Independent Test**: Eine deterministische Suche liefert passende relative
Pfade, respektiert Abbruch und Treffergrenze; eine Dateiendung wählt nur einen
internen Viewer oder einen sichtbaren Fallback.

**Acceptance Scenarios**:

1. **Given** eine kontrollierte Verzeichnisstruktur, **When** eine Suche
   gestartet wird, **Then** werden ausschließlich Treffer innerhalb der
   Wurzel in stabiler Reihenfolge angezeigt.
2. **Given** eine laufende Suche, **When** sie abgebrochen wird, **Then** bleibt
   ein konsistenter Teilstand mit sichtbarem Abbruchstatus erhalten.
3. **Given** eine unbekannte Dateiendung, **When** die zugeordnete Ansicht
   gewählt wird, **Then** startet kein externes Programm und ein interner
   Fallback wird erklärt.

---

### User Story 4 - Mutationen bewusst entscheiden und rückmelden (Priority: P4)

Als Maintainer möchte ich Kopieren, Umbenennen, Löschen und
Schreibschutzänderungen nur nach einer expliziten Entscheidung innerhalb
einer Testwurzel ausführen, damit Ablehnung, Konflikt, Fortschritt und
Recovery überprüfbar bleiben.

*As a maintainer, I want copy, rename, delete, and read-only changes to execute
only after an explicit decision inside a test root so rejection, conflict,
progress, and recovery remain reviewable.*

**Why this priority**: Mutierende Pfade sind der höchste Risikobereich und
dürfen erst auf der bewiesenen Navigations- und Pfadgrenze aufbauen.

**Independent Test**: Test-eigene Dateien durchlaufen Preview, Cancel,
Confirm, Zielkonflikt, erfolgreichen Abschluss und fehlgeschlagene Recovery,
ohne einen Pfad außerhalb der Wurzel zu verändern.

**Acceptance Scenarios**:

1. **Given** eine gültige Mutation, **When** sie nur vorbereitet oder
   abgebrochen wird, **Then** bleibt das Dateisystem bytegleich.
2. **Given** eine bestätigte Mutation ohne Konflikt, **When** sie ausgeführt
   wird, **Then** stimmen Dateisystemzustand, Fortschritt und sichtbarer Status
   überein.
3. **Given** ein existierendes Ziel, einen Link, Traversal oder Capability-
   Fehler, **When** eine Mutation versucht wird, **Then** wird sie atomar
   abgelehnt oder mit eindeutigem Recovery-Zustand beendet.
4. **Given** eine Drag/Drop-Absicht, **When** kein Mauspfad verfügbar ist,
   **Then** ist dieselbe vorbereitete Dateiaktion vollständig per Tastatur
   erreichbar.

---

### User Story 5 - Funktionale Vollständigkeit und Folgedelta bewerten (Priority: P5)

Als Projektverantwortlicher möchte ich pro Funktionsbereich die verwendete
Framework-Komponente, lokale Fachlogik und verbleibende Showcase-Arbeit
kennen, damit die zweite Wave-6-Stufe nur aus realen Deltas entsteht.

*As the project owner, I want the framework component, local domain logic, and
remaining showcase work for each functional area so a second Wave-6 stage is
derived only from real deltas.*

**Why this priority**: Die Matrix verhindert sowohl lokale
Framework-Duplikation als auch eine spekulative weitere Portierungswelle.

**Independent Test**: Genau zehn Funktionsbereiche und jeder gelieferte
Einstiegspunkt besitzen vollständige Evidence sowie genau eine zulässige
Framework- und Stage-2-Entscheidung.

**Acceptance Scenarios**:

1. **Given** die 24 historischen Dateien, **When** die Inventur geprüft wird,
   **Then** besitzt jede genau eine Rolle und eine moderne Zielbeziehung.
2. **Given** die zehn Funktionsbereiche, **When** Framework-Nutzung geprüft
   wird, **Then** besitzt jeder genau eine Hauptentscheidung mit Evidence.
3. **Given** den gelieferten Einstiegspunkt, **When** seine sichtbare
   Vollständigkeit bewertet wird, **Then** erhält er genau eine Stage-2-
   Entscheidung ohne automatische Feature-036-Erstellung.

### Randfälle / Edge Cases

- Ein Pfad ist absolut, enthält `..`, verwendet eine alternative
  Verzeichnistrennung oder unterscheidet sich nur durch Plattform-Casing.
- Ein Verzeichnis oder eine Datei wird zwischen Prüfung und Zugriff entfernt.
- Ein Link oder Reparse-Ziel zeigt innerhalb oder außerhalb der Wurzel.
- Ein Filter ist leer, ungültig oder liefert keine Treffer.
- Eine Datei ist größer als die Vorschau- oder Suchgrenze.
- UTF-8-Inhalt ist unvollständig oder ungültig.
- Ein Ziel existiert bereits oder entspricht der Quelle.
- Eine Datei ist schreibgeschützt oder die Plattform kann das angeforderte
  Attribut nicht zuverlässig abbilden.
- Eine Suche erreicht Treffer-, Tiefen- oder Dateigrenze.
- Eine Operation wird vor Start, während Fortschritt oder nach einem
  Teilfehler abgebrochen.
- Der Terminal ist kleiner als die bevorzugte Layoutgröße.
- Mausunterstützung fehlt oder fällt während einer Drag-Absicht aus.
- Eine Resource-, Paletten- oder Konfigurationsangabe ist unbekannt.
- Ein gemeinsamer Frameworkdefekt tritt trotz Feature-031-Closure auf.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Feature 035 MUSS Lastenheft 20 und die gemergte
  Feature-034-Closure als bindende Eingaben behandeln.
- **FR-002**: Der Lauf MUSS genau die 24 Dateien im flachen `TVFM/`-Inventar
  prüfen; jede MUSS genau eine primäre Rolle aus `EntryPoint`,
  `ApplicationSupport`, `ViewOrInteraction`, `FileOperation`,
  `ResourceOrPalette`, `BuildIntent` oder `IntentionalOmission` erhalten.
- **FR-003**: Jede historische Rolle MUSS Zweck, moderne Zielbeziehung,
  übernommene Absicht und wesentliche Abweichung dokumentieren.
- **FR-004**: `TVFM/`, `TVDEMOS/`, `tv203s/` und externe
  Vergleichscheckouts MÜSSEN read-only bleiben.
- **FR-005**: Das Feature MUSS genau die zehn Funktionsbereiche aus
  Lastenheft 20 abdecken.
- **FR-006**: Jeder Funktionsbereich MUSS genau eine Frameworkentscheidung
  aus `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`
  oder `FollowUpHardening` erhalten.
- **FR-007**: Ein gemeinsamer Defekt DARF NICHT durch lokale
  Beispielsonderlogik verdeckt werden.
- **FR-008**: `SmallFrameworkFix` MUSS reproduzierbare Red-Evidence, eine
  begrenzte wiederverwendbare Änderung und Regressionsevidence besitzen.
- **FR-009**: Ein breiter, API-brechender oder closure-widersprechender Fund
  MUSS als `FollowUpHardening` den betroffenen Slice stoppen.
- **FR-010**: Das Feature MUSS mindestens einen eigenständig normal
  startbaren TVFM-Lerneinstieg und einen kontrollierten `--smoke`-Einstieg
  liefern.
- **FR-011**: Der normale Einstieg MUSS einen dokumentierten
  repository-eigenen Lernarbeitsbereich verwenden und DARF NICHT automatisch
  beliebige Host-Verzeichnisse öffnen.
- **FR-012**: Tests MÜSSEN ausschließlich source-kontrollierte Fixtures oder
  test-eigene temporäre Wurzeln verwenden.
- **FR-013**: Jede Dateioperation MUSS vor Zugriff einen kanonischen Pfad
  innerhalb der aktiven kontrollierten Wurzel nachweisen.
- **FR-014**: Links, Reparse-Ziele, Traversal und andere Wurzelausbrüche
  MÜSSEN vor Inhaltsoffenlegung oder Mutation atomar abgelehnt werden.
- **FR-015**: Keine Operation DARF das prozessweite Arbeitsverzeichnis
  dauerhaft verändern.
- **FR-016**: Die Anwendung MUSS Verzeichnisnavigation, relative
  Pfadanzeige, leere Verzeichnisse und nicht verfügbare Einträge sichtbar
  behandeln.
- **FR-017**: Die Dateiliste MUSS stabile Sortierung, begrenzte Filterung,
  Markierung und textorientierte Metadaten bereitstellen.
- **FR-018**: Text- und Hexansicht MÜSSEN byte- und zeilenbegrenzt sein und
  abgeschnittene oder ungültige Inhalte ehrlich kennzeichnen.
- **FR-019**: Suche MUSS Wurzel, Tiefe, geprüfte Dateien, Treffer und
  Abbruch begrenzen und ausschließlich relative Treffer publizieren.
- **FR-020**: Dateizuordnungen DÜRFEN ausschließlich interne Text-, Hex- oder
  sichtbare Fallback-Entscheidungen liefern.
- **FR-021**: Shells, Prozesse, PTYs, externe Viewer und Host-Befehle DÜRFEN
  NICHT gestartet werden.
- **FR-022**: Kopieren, Umbenennen, Löschen und Attributänderung MÜSSEN vor
  Mutation eine explizite Confirm-Entscheidung verlangen.
- **FR-023**: Cancel, fehlende Entscheidung oder ungültige Eingabe MÜSSEN das
  Dateisystem unverändert lassen.
- **FR-024**: Existierende Ziele DÜRFEN NICHT still überschrieben werden.
- **FR-025**: Mutationen MÜSSEN Fortschritt, Abschluss, Fehler und
  Recovery-Grenze textlich und im Anwendungszustand veröffentlichen.
- **FR-026**: Drag/Drop DARF nur eine sichtbare Dateiaktionsabsicht
  vorbereiten und MUSS einen vollständigen Tastaturfallback besitzen.
- **FR-027**: Palette, Konfiguration und Resources MÜSSEN aus einem
  geschlossenen, deterministischen Satz stammen und unbekannte Werte
  ablehnen oder sichtbar zurückfallen.
- **FR-028**: Der primäre Proof MUSS den realen `app.Run()`-Pfad sowie
  Ereignis, Command, Fokus, View-Identität, Status und Buffer-/Cell-Evidence
  verwenden.
- **FR-029**: Direkte Fachhelfer DÜRFEN nur ergänzende oder Setup-Evidence
  liefern.
- **FR-030**: Die Anwendung MUSS eine reale `TStatusLine`, sichtbare Fokus-,
  Auswahl-, Erfolgs- und Fehlerzustände sowie `F1` beziehungsweise
  `Help -> Description` bereitstellen.
- **FR-031**: Alle primären Funktionen MÜSSEN per Tastatur erreichbar sein;
  Mauspfade DÜRFEN keinen exklusiven Funktionszugang bilden.
- **FR-032**: Kleine Terminalgrößen und High-Contrast-Nutzung MÜSSEN einen
  nachvollziehbaren, textorientierten Zustand behalten.
- **FR-033**: Ein zweisprachiger DE-first/EN-second CEFR-B2-Guide MUSS
  Lernziel, Start, Bedienung, Sicherheitsgrenze, historische Absicht,
  Abweichungen und Tests erklären.
- **FR-034**: Neue oder geänderte nicht triviale Logik MUSS auf
  didaktischen Inline-Kommentarwert geprüft werden.
- **FR-035**: Jeder gelieferte Einstiegspunkt MUSS genau eine Stage-2-
  Entscheidung aus `ShowcaseComplete`, `ShowcaseDelta`,
  `IntentionalMinimalSurface` oder `ProductDecision` erhalten.
- **FR-036**: `ShowcaseDelta` MUSS konkrete sichtbare oder bedienbare
  Restarbeit nennen; `ProductDecision` MUSS den Lauf stoppen.
- **FR-037**: Das Feature DARF Feature 036 und den Post-Wave-6-Portfolio-
  Audit weder anlegen noch starten.
- **FR-038**: Vor Implementierungsänderungen MÜSSEN Run-State,
  Gate-Anforderungen, PR-Evidence, historische Matrix und Framework-/Delta-
  Matrix vorhanden sein.
- **FR-039**: Evidence MUSS Sicherheits-, A11Y-, Plattform-, Agent-Paritäts-
  und Governance-Entscheidungen der sieben installierten Presets enthalten.
- **FR-040**: Das Feature MUSS alle lokalen, Remote-, Review- und
  Exact-Head-Gates vor Merge nachvollziehbar abschließen.

### Constitution Requirements

- **CR-001**: Das .NET-10-Level-2-Projekt MUSS den registrierten
  Projektkontext und C# als MSL-erlaubte Implementierungssprache verwenden.
- **CR-002**: Lern- und nutzerorientierte Artefakte MÜSSEN DE-first,
  EN-second auf CEFR-B2-Niveau und text-first zugänglich sein.
- **CR-003**: WCAG 2.2 AA, Tastaturbedienung und der vorhandene DocFX/Axe-
  Nachweispfad MÜSSEN für geänderte Guides und Navigation gelten.
- **CR-004**: NIST SSDF und CWE Top 25 MÜSSEN auf Pfadvalidierung,
  Dateioperationen, Evidence-Integrität und Lieferkette angewandt werden.
- **CR-005**: STRIDE, CIA und CAPEC MÜSSEN für Traversal, Link-Ausbruch,
  unbefugte Mutation, Ressourcenerschöpfung und Evidence-Manipulation geprüft
  werden.
- **CR-006**: OWASP ASVS ist `N/A`, weil keine Web-, HTTP-, Auth- oder
  Servicefläche entsteht; bei Scope-Änderung ist dies neu zu bewerten.
- **CR-007**: Feature-eigene SBOM-, VEX-, SLSA- oder OpenSSF-Artefakte sind
  `N/A`, solange keine Dependency oder neue Release-Komponente entsteht; die
  bestehenden Supply-Chain-Gates bleiben anwendbar.
- **CR-008**: AI-SBOM ist `N/A`, weil KI ausschließlich Entwicklungswerkzeug
  ist und keine Modelle, Daten oder AI-Runtime ausgeliefert werden.
- **CR-009**: S-ADR, arc42, Zero Trust, SAMM, BSI C3A und BSI C5 sind
  `N/A`, solange keine Architektur-, Cloud-, Provider-, Identitäts- oder
  Deploymentgrenze geändert wird.
- **CR-010**: NIS2, CRA, EU AI Act und DORA sind `N/A`, solange keine neue
  regulierte Rolle, AI-Komponente oder verteilte Produktgrenze entsteht.
- **CR-011**: Cross-Platform Governance ist für Datei- und
  Attributsemantik anwendbar; Script-Parität ist `N/A`, sofern kein Script
  geändert wird.
- **CR-012**: Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0,
  Cross-Platform 0.2.0, Agent Parity 0.3.0 und Autonomous Run 0.2.2 MÜSSEN
  in der Feature-Evidence dokumentiert werden.
- **CR-013**: Projektstatistik, Pflichtenheft, Abarbeitungsreihenfolge,
  Guides und alle gepflegten Agent-Kontexte MÜSSEN gemeinsam auf
  Änderungsbedarf geprüft werden.

### Schlüsseldaten / Key Entities

- **ControlledWorkspace**: Explizit gebundene, kanonische Wurzel mit
  Plattformvergleich, Link-Regel und sichtbarem relativem Pfad.
- **DirectorySnapshot**: Stabil sortierte Verzeichnis- und Dateieinträge mit
  relativer Identität, Metadaten, Auswahl und Markierungszustand.
- **PreviewResult**: Begrenzte Text- oder Hexdarstellung mit Format,
  Bytegrenze, Abschneide- und Fehlerstatus.
- **SearchRequest / SearchResult**: Muster, Startpfad, Ressourcenlimits,
  Abbruchstatus und relative Treffer.
- **FileOperationIntent**: Quell- und Zielpfad, Operationsart, Konflikt,
  explizite Entscheidung und Vorzustand.
- **FileOperationResult**: Status, Fortschritt, betroffene relative Pfade,
  Fehlergrenze und Recovery-Aussage.
- **ViewerAssociation**: Dateiendung und ausschließlich interne
  Text-/Hex-/Fallback-Entscheidung.
- **HistoricalSourceRole**: Eine der 24 Quellen mit Rolle, Absicht,
  modernem Ziel und Abweichung.
- **FunctionalAreaDecision**: Einer der zehn Bereiche mit Framework-
  Entscheidung, Evidence und Follow-up-Grenze.
- **Stage2Disposition**: Einstiegspunktbezogene Entscheidung über tatsächliche
  Showcase-Restarbeit.

## Erfolgskriterien / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: Genau 24 historische Dateien besitzen je eine eindeutige
  Inventarrolle; es gibt keine fehlende oder doppelte Datei.
- **SC-002**: Genau zehn Funktionsbereiche besitzen je eine eindeutige
  Frameworkentscheidung und vollständige Evidence.
- **SC-003**: 100 % der mutierenden Tests verwenden eine test-eigene Wurzel;
  kein Test liest oder verändert einen Pfad außerhalb seiner Wurzel.
- **SC-004**: Alle erlaubten Navigations-, Listen-, Vorschau-, Such- und
  Mutationspfade besitzen mindestens einen positiven und einen relevanten
  negativen oder Abbruchnachweis.
- **SC-005**: Traversal-, Link-, Zielkonflikt- und fehlende
  Bestätigungsversuche werden zu 100 % vor unzulässiger Mutation abgelehnt.
- **SC-006**: Der normale und der kontrollierte Einstieg beenden sich
  deterministisch und zeigen Zweck, Pfad, Fokus, Status und Description.
- **SC-007**: Jeder Primärflow besitzt konkreten App-Loop-, View-,
  Zustands- und Buffer-/Cell-Nachweis.
- **SC-008**: Alle gelieferten Funktionen sind vollständig per Tastatur
  erreichbar; keine Funktion ist maus-exklusiv.
- **SC-009**: Der zweisprachige Guide und alle geänderten Lernflächen bestehen
  die text-first- und A11Y-Prüfung ohne kritischen Befund.
- **SC-010**: Vollständige Release-Tests und das kanonische Coverage-Gate
  bestehen; alle fünf Framework-Assemblies bleiben bei mindestens 70 %
  Zeilenabdeckung.
- **SC-011**: Jeder Einstiegspunkt besitzt genau eine Stage-2-Entscheidung;
  Feature 036 wird nicht automatisch erzeugt.
- **SC-012**: Der finale Diff enthält keine Änderung an historischen Quellen,
  keine neue Dependency und keinen unkontrollierten Host-Dateizugriff.

## Annahmen / Assumptions

- Feature 034 und sein kausaler Closeout sind die verbindliche Freigabe für
  den Wave-6-Intake.
- Eine einzelne moderne TVFM-Lernanwendung kann die zusammengehörigen
  historischen Units besser vermitteln als viele künstlich getrennte
  Executables.
- Bestehende TuiVision-Views, Dialog-, Status-, Help-, Progress- und
  Eventverträge reichen grundsätzlich aus; neue Dateisystem-Fachlogik bleibt
  beispielbezogene, kontrollierte Komposition.
- Der repository-eigene normale Lernarbeitsbereich enthält ausschließlich
  bewusst veröffentlichte Fixtures und darf bei jedem Start neu hergestellt
  oder read-only verwendet werden.
- Plattformunterschiede bei Schreibschutz und Pfadvergleich werden ehrlich
  als Capability oder IntentionalDeviation dokumentiert.
- Remote-Autorität stammt ausschließlich aus der aktuellen
  `MergeAndSync`-Anweisung und nicht aus dieser Spezifikation.

## Scope-Grenzen / Scope Boundaries

### In Scope

- Eine moderne, idiomatische C#-Interpretation der funktionalen TVFM-
  Lernabsicht.
- Kontrollierte Navigation, Liste, Filter, Sortierung, Markierung, Vorschau,
  Suche, interne Zuordnung und explizit bestätigte Dateioperationen.
- Echte TuiVision-App-Loop-, View-, Status-, Help- und Cell-Proofs.
- Historische, Framework-, Sicherheits-, A11Y-, Plattform- und Stage-2-
  Evidence.

### Out of Scope

- Allgemeiner Host-Dateimanager oder Zugriff auf beliebige Benutzerdaten.
- Mechanische Pascal-Übersetzung und Änderungen unter historischen Quellen.
- Shell, Prozess, PTY, externe Viewer, Netzwerk-, Cloud- oder Gerätezugriff.
- Neue Dependencies, breite Framework-Revision oder API-Bruch.
- Spekulative Showcase-Remediation, Feature 036 oder Portfolio-Audit.

### Decision and Follow-up Model

- Framework: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, `FollowUpHardening`.
- Stage 2: `ShowcaseComplete`, `ShowcaseDelta`,
  `IntentionalMinimalSurface`, `ProductDecision`.
- Ein `FollowUpHardening`, `ProductDecision` oder nicht sicher begrenzbarer
  Dateipfad stoppt den betroffenen autonomen Laufabschnitt.
