# Feature Specification: Wave-6 TVFM Showcase Remediation

**Feature Branch**: `036-wave6-tvfm-showcase-remediation`
**Created**: 2026-07-17
**Status**: Draft
**Binding Input**: `Lastenheft_21_Wave6-TVFM-Showcase-Remediation.036-wave6-tvfm-showcase-remediation.md`

## Klärungen / Clarifications

### Session 2026-07-17

- Keine formale Rückfrage ist erforderlich. Lastenheft 21, Feature 035 und
  dessen kausaler Closeout legen Einstiegspunkt, zehn Showcase-Bereiche,
  Sicherheitsgrenzen, Entscheidungsbegriffe und Liefermodus vollständig fest.
- Feature 036 ist ausschließlich die sichtbare und interaktive zweite
  Wave-6-Stufe für `Tp7FileManager`. Die funktionalen Datei-Verträge werden
  wiederverwendet und nicht erneut portiert.
- Der Mauspfad darf nur denselben bestätigungspflichtigen Intent wie der
  Tastaturpfad vorbereiten. Er darf niemals direkt eine Datei verändern.
- Das Feature erzeugt weder Feature 037 noch einen unabhängigen Wave-6-
  Abschluss oder den Post-Wave-6-Portfolio-Audit.
- Eine zweite fokussierte Clarify-Prüfung findet ebenfalls keine verbleibende
  planungswirksame Unklarheit. Die nächste zulässige Stufe ist die technische
  Planung, sobald alle Requirements-Checklists bestanden sind.

*No formal question is required. The binding intake fixes the single entry
point, ten showcase areas, safety boundaries, decision vocabularies, and
MergeAndSync delivery. Feature 036 exposes existing behavior rather than
re-porting it.*

## Nutzungsszenarien und Prüfung / User Scenarios and Testing

### User Story 1 - Sicher navigieren und Zustand erkennen (Priority: P1)

Als Lernender möchte ich nach dem normalen Start den kontrollierten
Dateibaum, die Auswahl, den aktuellen Pfad und die wichtigsten Bedienwege
sofort erkennen und vollständig per Tastatur bedienen können.

*As a learner, I want the controlled file tree, selection, current path, and
primary controls to be understandable from the first frame and fully
keyboard-accessible.*

**Why this priority**: Navigation, Fokus, Status und Description sind die
sichtbare Grundlage für alle weiteren Showcase-Pfade.

**Independent Test**: Eine echte Anwendungsschleife navigiert durch die
kontrollierte Fixture und weist Fokus, View-Hierarchie, StatusLine,
Description und gerenderte Zellen in normaler und enger Ansicht nach.

**Acceptance Scenarios**:

1. **Given** die kontrollierte Lernwurzel, **When** die Anwendung normal
   startet, **Then** sind Zweck, Pfad, Dateiliste, Auswahl und primäre
   Bedienwege im ersten sichtbaren Zustand erkennbar.
2. **Given** mehrere fokussierbare Bereiche, **When** der Nutzer per Tastatur
   wechselt, **Then** sind Fokus, Auswahl und verfügbarer nächster Schritt
   textlich nachvollziehbar.
3. **Given** die enge Ansicht `48x16`, **When** Navigation und Description
   verwendet werden, **Then** bleiben Zweck, Auswahl, Status und Beenden
   erreichbar, ohne unlesbare Überlagerung.

---

### User Story 2 - Lesende Funktionen sichtbar nutzen (Priority: P2)

Als Lernender möchte ich Text-/Hexvorschau, Filter, Sortierung, Tags, Suche
und interne Viewerwahl über sichtbare Menüs oder Controls erreichen, damit
die funktionalen Feature-035-Verträge verständlich werden.

*As a learner, I want visible access to preview, filtering, sorting, tags,
search, and internal viewer selection so the proven functional contracts are
understandable.*

**Why this priority**: Die lesenden Pfade vermitteln den Hauptnutzen des
Dateimanagers ohne Mutationsrisiko.

**Independent Test**: Reale Commands wechseln die sichtbare Vorschau,
verändern Filter-/Sortier-/Tagzustand, führen eine begrenzte Suche aus und
zeigen ausschließlich interne Viewerentscheidungen.

**Acceptance Scenarios**:

1. **Given** kontrollierte Text- und Binärdateien, **When** Text- oder
   Hexansicht gewählt wird, **Then** zeigt die Hauptkomposition den begrenzten
   Inhalt und eine ehrliche Abschneide- oder Fallback-Aussage.
2. **Given** mehrere Einträge, **When** Filter, Sortierung oder Tags geändert
   werden, **Then** bleiben aktueller Modus, Auswahl und Ergebnis sichtbar.
3. **Given** eine begrenzte Suche, **When** Treffer, Abbruch oder Limit
   eintritt, **Then** zeigt die Oberfläche einen konsistenten textlichen
   Zustand ohne externen Prozess.

---

### User Story 3 - Dateioperationen bewusst entscheiden (Priority: P3)

Als Maintainer möchte ich Kopieren, Umbenennen, Löschen und Schreibschutz
über fokussierbare Dialoge vorbereiten, prüfen, bestätigen oder abbrechen,
damit keine Mutation unbeabsichtigt erfolgt.

*As a maintainer, I want focusable dialogs for copy, rename, delete, and
read-only changes so every mutation is previewed, confirmed, or cancelled
explicitly.*

**Why this priority**: Mutationen sind der höchste Sicherheitsbereich und
müssen die bereits bewiesenen Einmal-Intents sichtbar, aber unverändert
verwenden.

**Independent Test**: Jede Operation durchläuft über die echte
Anwendungsschleife Preview, Confirm, Cancel, Revalidierung, Ergebnis und
mindestens einen relevanten Ablehnungs- oder Recovery-Pfad.

**Acceptance Scenarios**:

1. **Given** eine gültige Quelle, **When** ein Operationsdialog geöffnet
   wird, **Then** sind Operation, Quelle, Ziel, Sicherheitsgrenze und
   Confirm-/Cancel-Entscheidung sichtbar und per Tastatur erreichbar.
2. **Given** Cancel, Escape oder ungültige Eingabe, **When** der Dialog endet,
   **Then** bleibt die Fixture unverändert und der Grund wird textlich
   angezeigt.
3. **Given** eine bestätigte Operation, **When** der Intent unmittelbar vor
   Ausführung revalidiert wird, **Then** stimmen Ergebnis, Dateisystemzustand
   und StatusLine überein oder die Operation wird sicher abgelehnt.

---

### User Story 4 - Maus nur als optionale Vorbereitung nutzen (Priority: P4)

Als Tastatur- oder Mausnutzer möchte ich denselben Dateioperations-Intent
vorbereiten können, ohne dass Drag-and-Drop die Bestätigung oder
Sicherheitsprüfung umgeht.

*As a keyboard or mouse user, I want both paths to prepare the same file
operation intent without bypassing confirmation or safety checks.*

**Why this priority**: Mauspolitur ergänzt die Bedienung, darf aber keine
zweite oder privilegierte Mutationslogik schaffen.

**Independent Test**: Maus und Tastatur erzeugen denselben vorbereiteten
Intent; ungültiges Ziel, Escape, Capability-Verlust, View-Entfernung und
Shutdown brechen ohne Mutation ab.

**Acceptance Scenarios**:

1. **Given** einen ausgewählten Fixture-Eintrag, **When** ein gültiger
   Drag-Pfad endet, **Then** wird nur der gleiche bestätigungspflichtige
   Intent wie per Tastatur vorbereitet.
2. **Given** ein ungültiges Ziel oder einen Abbruch, **When** der Mauspfad
   endet, **Then** entsteht keine Dateiänderung und ein verständlicher Status.
3. **Given** keine Mausfähigkeit, **When** dieselbe Aktion benötigt wird,
   **Then** bleibt der vollständige Tastaturpfad verfügbar.

---

### User Story 5 - Showcase-Abschluss nachvollziehbar bewerten (Priority: P5)

Als Projektverantwortlicher möchte ich für alle zehn Bereiche Framework-
Wiederverwendung, sichtbaren Proof, Abweichungen und Restrisiken kennen,
damit Wave 6 ohne verdeckte Restarbeit bewertet werden kann.

*As the project owner, I want traceable framework usage, visible proof,
deviations, and residual risk for all ten areas so Wave 6 can be evaluated
without hidden work.*

**Why this priority**: Die 1/10-Matrix verhindert pauschale
Abschlussbehauptungen und lokale Framework-Duplikation.

**Independent Test**: Die Evidence enthält genau eine Einstiegspunktzeile und
genau zehn eindeutige Bereichszeilen mit zulässigen Entscheidungen und
vollständigen Proof-Grenzen.

**Acceptance Scenarios**:

1. **Given** `W6S-001` bis `W6S-010`, **When** die Matrix geprüft wird,
   **Then** besitzt jeder Bereich genau eine Frameworkentscheidung.
2. **Given** den Einstiegspunkt `Tp7FileManager`, **When** der Abschluss
   bewertet wird, **Then** liegt genau eine zulässige Abschlussentscheidung
   ohne offenes `ShowcaseDelta` vor.
3. **Given** eine breite Lücke oder Produktentscheidung, **When** sie
   entdeckt wird, **Then** stoppt der Lauf oder dokumentiert ein begrenztes
   Follow-up, statt den Scope still zu erweitern.

### Randfälle / Edge Cases

- Die normale oder enge Ansicht hat zu wenig Platz für alle Detailbereiche.
- Die Auswahl verschwindet durch Filter, Suche, Navigation oder Dateimutationen.
- Ein Dialog erhält leere, ungültige, außerhalb der Wurzel liegende oder
  inzwischen veraltete Eingaben.
- Das Ziel existiert bereits, ist ein Link oder wird zwischen Preview und
  Bestätigung verändert.
- Enter, Escape, F1, Tab oder Shift+Tab treffen auf einen modalen Dialog.
- Eine Suche erreicht Treffer-, Tiefen-, Datei- oder Zeitgrenzen oder wird
  abgebrochen.
- Vorschauinhalt ist ungültiges UTF-8, binär oder größer als das Limit.
- Mausunterstützung fehlt oder fällt während eines vorbereiteten Drag-Pfads aus.
- Ein Drag endet außerhalb eines gültigen Ziels oder die beteiligte View wird
  entfernt.
- Eine Palette, Resource oder Viewerzuordnung ist unbekannt.
- Ein historischer Ablauf widerspricht einem akzeptierten modernen
  Sicherheitsvertrag.
- Eine wiederverwendbare Framework-Lücke wäre breiter als ein kleiner,
  test-first belegter Fix.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Feature 036 MUSS Lastenheft 21, Feature-035-PR #101 und
  Closeout-PR #102 als verbindliche Basis behandeln.
- **FR-002**: Der Lauf MUSS genau einen Einstiegspunkt `Tp7FileManager` und
  genau die Bereiche `W6S-001` bis `W6S-010` abdecken.
- **FR-003**: Feature-035-Domänenlogik und kontrollierte
  Dateisystemverträge MÜSSEN wiederverwendet und DÜRFEN NICHT erneut portiert
  oder erweitert werden.
- **FR-004**: `TVFM/`, `TVDEMOS/`, `tv203s/` und externe
  Vergleichscheckouts MÜSSEN read-only bleiben.
- **FR-005**: Der normale erste Frame MUSS Zweck, kontrollierte Lernwurzel,
  Dateiliste, aktuelle Auswahl und primäre Bedienwege sichtbar machen.
- **FR-006**: `Tp7FileManager` MUSS eine reale sichtbare Hauptkomposition,
  eine echte `TStatusLine` und einen tastaturerreichbaren
  `Help -> Description`-Pfad besitzen.
- **FR-007**: Jeder bewiesene Feature-035-Kerncommand MUSS einen sichtbaren
  Menü-, Control-, Dialog- oder Statuszugang besitzen.
- **FR-008**: Jeder primäre Command MUSS vollständig per Tastatur erreichbar
  sein; Mausinteraktion DARF NICHT der einzige Zugang sein.
- **FR-009**: Menüs MÜSSEN Navigation, Filter, Sortierung, Tags, Vorschau,
  Viewerwahl, Suche, Dateioperationen, Palette/Resources, Hilfe und Beenden
  fachlich verständlich gruppieren.
- **FR-010**: Nicht verfügbare Aktionen MÜSSEN ehrlich deaktiviert oder
  textlich erklärt werden.
- **FR-011**: Text- und Hexvorschau MÜSSEN den bestehenden begrenzten Inhalt,
  Modus und Abschneide-/Fallback-Zustand sichtbar darstellen.
- **FR-012**: Filter-, Sortier- und Tagzustand MÜSSEN gemeinsam mit Auswahl
  und Ergebnis textlich erkennbar bleiben.
- **FR-013**: Suche MUSS Eingabe, Treffer, Abbruch und Ressourcenlimit über
  einen sichtbaren, begrenzten Pfad darstellen.
- **FR-014**: Viewerzuordnung DARF ausschließlich bestehende interne Text-,
  Hex- oder sichtbare Fallback-Entscheidungen anbieten.
- **FR-015**: Kopieren, Umbenennen, Löschen und Schreibschutz MÜSSEN über
  fokussierbare vorhandene Controls mit stabiler Tab-Reihenfolge,
  Enter-/Escape-Verhalten und sichtbarer Validierung laufen.
- **FR-016**: Jeder Mutationsdialog MUSS Quelle, Ziel oder neuen Namen,
  normalisierte Preview, Sicherheitsgrenze und Confirm-/Cancel-Entscheidung
  anzeigen.
- **FR-017**: Jeder bestätigte Intent MUSS unmittelbar vor Ausführung
  revalidiert werden.
- **FR-018**: Cancel, Escape, fehlende Bestätigung oder ungültige Eingabe
  MÜSSEN die Fixture unverändert lassen.
- **FR-019**: Löschung MUSS nicht rekursiv bleiben; Copy/Rename DARF
  vorhandene Ziele NICHT still überschreiben.
- **FR-020**: Ergebnis, Ablehnung und Recovery-Grenze MÜSSEN textlich und in
  der StatusLine sichtbar sein.
- **FR-021**: Drag-and-Drop DARF nur für einen ausgewählten Fixture-Eintrag
  denselben bestätigungspflichtigen Intent wie der Tastaturpfad vorbereiten.
- **FR-022**: Drag-and-Drop DARF eine Mutation NICHT direkt ausführen.
- **FR-023**: Ungültiges Ziel, Escape, Capability-Verlust, View-Entfernung
  und Shutdown MÜSSEN einen vorbereiteten Mauspfad ohne Mutation abbrechen.
- **FR-024**: Palette und Resources MÜSSEN aus einem geschlossenen,
  deterministischen Satz stammen; unbekannte Werte MÜSSEN abgelehnt oder
  sichtbar zurückgesetzt werden.
- **FR-025**: Die normale Ansicht und mindestens eine `48x16`-Ansicht MÜSSEN
  Zweck, Auswahl, nächsten Schritt, Status und Beendigungspfad erkennbar
  halten.
- **FR-026**: Fokus, Auswahl, Bestätigung, Ablehnung, Abbruch, Fehler und
  Plattformfallback MÜSSEN text-first verständlich sein.
- **FR-027**: High-Contrast-Nutzung DARF keine Information ausschließlich
  durch Farbe kodieren.
- **FR-028**: `F1` beziehungsweise `Help -> Description` MUSS Zweck,
  Bedienung, Sicherheitsgrenze, moderne Abweichung und Proof-Grenze erklären.
- **FR-029**: Primäre Proofs MÜSSEN die reale Anwendungsschleife, Event-,
  Command- und Dialogdispatch, Fokus, View-Hierarchie, Status, Description
  und Buffer-/Cell-Evidence verwenden.
- **FR-030**: Direkte Helfer DÜRFEN nur `SetupOnly` oder
  `SupplementalProof` sein.
- **FR-031**: Positive, abgelehnte, abgebrochene, Recovery- und
  nicht-unterstützte Pfade MÜSSEN mit kontrollierten Fixtures belegt werden.
- **FR-032**: Ein normaler PTY-Pfad MUSS Start, primäre Aktion, F1 und
  `Ctrl+Q` nachweisen; ein kontrollierter `--smoke`-Pfad MUSS deterministisch
  beenden.
- **FR-033**: Jeder der zehn Bereiche MUSS genau eine Entscheidung aus
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening` erhalten.
- **FR-034**: `SmallFrameworkFix` MUSS eine kleine reproduzierbare Lücke,
  Red-/Green-Proof und wiederverwendbare Regressionsevidence besitzen.
- **FR-035**: Breite Runtime-, API-, Architektur-, Dateisystem- oder
  Sicherheitslücken MÜSSEN als `FollowUpHardening` begrenzt werden.
- **FR-036**: Die Einstiegspunktzeile MUSS genau eine Entscheidung aus
  `ShowcaseComplete`, `IntentionalMinimalSurface`, `FollowUpHardening` oder
  `ProductDecision` erhalten.
- **FR-037**: Ein `ProductDecision` MUSS den autonomen Lauf stoppen.
- **FR-038**: Die Evidence MUSS genau eine Einstiegspunktzeile und genau zehn
  eindeutige Bereichszeilen enthalten.
- **FR-039**: Jede Evidence-Zeile MUSS Funktionsproof, sichtbaren Zugang,
  normale/enge Layout-Evidence, Fokus, Status, Description, Tastatur,
  Framework-Nutzung, historische Absicht, Abweichung, Grenzen, Restrisiko
  und Wiederbewertung dokumentieren.
- **FR-040**: Der Guide MUSS DE-first/EN-second auf CEFR-B2-Niveau Lernziel,
  Start, Menüs, Tastatur, Mausfallback, Dateioperationen, Sicherheitsgrenze,
  constrained layout, Plattformgrenzen, Abweichungen und Proof erklären.
- **FR-041**: Neue oder geänderte nicht triviale Logik MUSS auf
  didaktischen Inline-Kommentarwert geprüft werden.
- **FR-042**: Vor der ersten Implementierungsänderung MÜSSEN Run-State,
  Gate-Anforderungen und PR-Evidence vorhanden sein.
- **FR-043**: Die Evidence MUSS Sicherheits-, Architektur-, A11Y-,
  Plattform-, Agent-Paritäts- und autonome Governance der sieben Presets
  dokumentieren.
- **FR-044**: Alle lokalen, Remote-, Review- und Exact-Head-Gates MÜSSEN vor
  Merge konvergieren.
- **FR-045**: Feature 036 DARF Feature 037, einen unabhängigen Wave-6-
  Abschluss oder den Post-Wave-6-Portfolio-Audit NICHT starten.

### Constitution Requirements

- **CR-001**: Das .NET-10-Level-2-Projekt MUSS C# als MSL-erlaubte
  Implementierungssprache und den registrierten Projektkontext verwenden.
- **CR-002**: Lern- und Nutzerartefakte MÜSSEN DE-first, EN-second auf
  CEFR-B2-Niveau und text-first zugänglich sein.
- **CR-003**: WCAG 2.2 AA, Tastaturbedienung, High Contrast und der
  DocFX-/Playwright-/Axe-Pfad sind für die geänderte sichtbare Anwendung und
  den Guide anwendbar.
- **CR-004**: NIST SSDF und CWE Top 25 sind auf kontrollierte Pfade,
  Dateioperationen, Dialogentscheidungen, Evidence-Integrität und Lieferung
  anzuwenden.
- **CR-005**: STRIDE, CIA und CAPEC sind für Traversal, Link-Ausbruch,
  unbefugte Mutation, Ressourcenerschöpfung und UI-verdeckte Bestätigung
  anwendbar.
- **CR-006**: OWASP ASVS ist `N/A`, weil keine Web-, HTTP-, Auth- oder
  Servicefläche entsteht; bei Scope-Änderung ist neu zu bewerten.
- **CR-007**: Neue SBOM-, VEX-, SLSA- oder OpenSSF-Artefakte sind `N/A`,
  solange keine Dependency oder Release-Komponente entsteht; vorhandene
  Supply-Chain-Gates bleiben anwendbar.
- **CR-008**: AI-SBOM ist `N/A`, weil KI ausschließlich
  Entwicklungswerkzeug ist und keine AI-Runtime ausgeliefert wird.
- **CR-009**: S-ADR, neue arc42-Artefakte, Zero Trust, SAMM, BSI C3A und
  BSI C5 sind `N/A`, solange keine Architektur-, Cloud-, Provider-,
  Identitäts- oder Deploymentgrenze geändert wird.
- **CR-010**: NIS2, CRA, EU AI Act und DORA sind `N/A`, solange keine neue
  regulierte Rolle, AI-Komponente oder verteilte Produktgrenze entsteht.
- **CR-011**: Cross-Platform Governance ist für Pfad-, Dateiattribut-,
  Terminal- und Eingabesemantik anwendbar; Script-Parität ist `N/A`, sofern
  kein Script geändert wird.
- **CR-012**: Security 0.6.0, Architecture 0.5.0, iSAQB 0.2.0, A11Y 0.4.0,
  Cross-Platform 0.2.0, Agent Parity 0.3.0 und Autonomous Run 0.2.2 MÜSSEN
  in der Feature-Evidence erscheinen.
- **CR-013**: Projektstatistik, Pflichtenheft, Abarbeitungsreihenfolge,
  Guide, Dokumentationsnavigation und fünf dauerhaft gepflegte
  Agent-Kontexte MÜSSEN gemeinsam auf Änderungsbedarf geprüft werden; der
  generierte Antigravity-Kontext MUSS aus demselben Plan aktualisiert werden.

### Schlüsseldaten / Key Entities

- **ShowcaseAreaEvidence**: Eine der zehn `W6S`-Zeilen mit sichtbarem Zugang,
  Proof, Frameworkentscheidung, Grenzen und Restrisiko.
- **ShowcaseEntryDecision**: Genau eine Abschlusszeile für
  `Tp7FileManager`.
- **VisibleCommandAccess**: Fachlicher Command, Menü-/Control-Zugang,
  Shortcut, Aktivierungsbedingung und Statusrückmeldung.
- **OperationDialogState**: Operation, Quelle, Ziel, Preview, Validierung,
  Entscheidung, Revalidierung und Ergebnis.
- **PreparedDragIntent**: Auswahl, sichtbares Ziel, Abbruchgrund und
  unveränderter bestätigungspflichtiger Feature-035-Intent.
- **LayoutProof**: Viewport, Fokusreihenfolge, View-Identität, sichtbare
  Texte, Status und relevante Cell-Regionen.
- **GuidanceSurface**: Guide, Shortcut-Inventar, Description,
  Dokumentationsnavigation und Agent-Kontext.

## Erfolgskriterien / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: Genau eine Einstiegspunktzeile und genau zehn eindeutige
  `W6S-001`-bis-`W6S-010`-Zeilen besitzen vollständige Evidence.
- **SC-002**: 100 % der zehn Bereiche besitzen genau eine zulässige
  Frameworkentscheidung.
- **SC-003**: 100 % der Feature-035-Kerncommands besitzen einen sichtbaren
  und vollständig tastaturerreichbaren Zugang.
- **SC-004**: Alle vier Mutationsarten besitzen Preview-, Confirm-, Cancel-,
  Revalidierungs-, Ergebnis- und relevanten Negativ- oder Recovery-Proof.
- **SC-005**: 100 % der getesteten Mauspfade führen vor Bestätigung zu null
  Dateiänderungen und besitzen einen vollständigen Tastaturfallback.
- **SC-006**: Normale und `48x16`-Ansicht besitzen konkrete App-Loop-, Fokus-,
  View-, Status-, Description- und Buffer-/Cell-Evidence.
- **SC-007**: Normaler PTY-Start mit primärer Aktion, F1 und `Ctrl+Q` sowie
  kontrollierter `--smoke`-Start beenden erfolgreich und deterministisch.
- **SC-008**: Der zweisprachige Guide und alle geänderten Lernflächen bestehen
  text-first-, UTF-8- und WCAG-2.2-AA-orientierte Prüfungen ohne kritischen
  Befund.
- **SC-009**: Vollständige Release-Tests und das kanonische Coverage-Gate
  bestehen; alle fünf Framework-Assemblies bleiben bei mindestens 70 %
  Zeilenabdeckung.
- **SC-010**: Die Einstiegspunktzeile besitzt genau eine zulässige
  Abschlussentscheidung und kein offenes `ShowcaseDelta`.
- **SC-011**: Der finale Diff enthält keine Änderung an historischen Quellen,
  keine neue Dependency, keinen zweiten Einstiegspunkt und keine Erweiterung
  der kontrollierten Dateisystemautorität.
- **SC-012**: Alle lokalen, Ubuntu-, macOS-, Windows-, Review- und
  Exact-Head-Gates sind nachvollziehbar abgeschlossen, bevor gemergt wird.

## Annahmen / Assumptions

- Feature 035 und sein Closeout bilden eine vollständige und unveränderte
  funktionale Basis.
- Die vorhandenen TuiVision-Menü-, Dialog-, Fokus-, Status-, Help-, Maus- und
  Renderingverträge reichen grundsätzlich für die Showcase-Stufe aus.
- Gemeinsame reine Showcase-Komposition darf in
  `TuiVision.Examples.Wave6` bleiben; wiederverwendbares Frameworkverhalten
  darf dort nicht verborgen werden.
- Mutierende Nachweise verwenden ausschließlich test-eigene temporäre
  Wurzeln; der normale Lernstart bleibt auf repository-eigene Fixtures
  begrenzt.
- Sekundäre Vergleichsquellen werden nur bei einer neuen konkreten,
  reproduzierbaren Showcase-Frage konsultiert.
- Remote-Autorität stammt ausschließlich aus der aktuellen
  `MergeAndSync`-Anweisung.

## Scope-Grenzen / Scope Boundaries

### In Scope

- Sichtbare, interaktive und didaktische zweite Stufe für `Tp7FileManager`.
- Menüs, fokussierbare Controls und begrenzte Dialoge für bewiesene Commands.
- StatusLine, Description, Tastatur, optionale Mausparität und normales sowie
  constrained layout.
- Reale App-Loop-, View-, Fokus-, Dialog- und Buffer-/Cell-Proofs.
- Zweisprachiger Guide, Shortcut-Inventar und vollständige 1/10-Evidence.

### Out of Scope

- Erneute Pascal-Portierung oder Erweiterung der Feature-035-Fachverträge.
- Beliebige Benutzer-, Netzwerk-, Geräte-, Laufwerks- oder Hostpfade.
- Shell, Prozess, PTY, externer Viewer oder Host-Dateimanager.
- Neue Dependency, neues Projekt, zweiter Einstiegspunkt oder breite
  Framework-/API-Revision.
- Änderung unter `TVFM/`, `TVDEMOS/`, `tv203s/` oder externen Checkouts.
- Feature 037, unabhängiger Wave-6-Abschluss und Post-Wave-6-Portfolio-Audit.

### Decision and Follow-up Model

- Framework: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, `FollowUpHardening`.
- Einstiegspunkt: `ShowcaseComplete`, `IntentionalMinimalSurface`,
  `FollowUpHardening`, `ProductDecision`.
- Ein `ProductDecision`, eine unsichere Dateisystemgrenze oder eine breite
  nicht eindeutig verantwortete Framework-Lücke stoppt den autonomen Lauf.
