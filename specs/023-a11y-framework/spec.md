# Feature Specification: A11Y Framework

**Feature Branch**: `023-a11y-framework`  
**Created**: 2026-07-12  
**Status**: Accepted  
**Binding Input**: `Lastenheft_06_A11Y_Framework.md`

## Clarifications

### Session 2026-07-12

- Q: Wie wird ein Fokuswechsel ohne zweiten Event-Pfad gemeldet? A: Der bestehende `cmFocusChanged`-Broadcast bleibt kanonisch und trägt einen typisierten Text-Payload mit Ziel-View und optionalem AccessibleLabel.
- Q: Welche Controls müssen das neue Widget-Interface sofort implementieren? A: Das Interface bleibt opt-in; die Referenz-App und repräsentative fokussierbare Controls beweisen den Vertrag. Nicht migrierte Controls behalten ihr Verhalten.
- Q: Wie werden Shortcuts strukturiert? A: Menü- und Statuszeilenobjekte exponieren unveränderliche, programmatisch abfragbare Beschreibungen; es gibt keinen globalen veränderlichen Registry-Singleton.
- Q: Wie wird High Contrast aktiviert? A: Ein benanntes Framework-Farbschema wird explizit auf eine Anwendung beziehungsweise deren A11Y-fähige Views angewendet und bleibt ohne Aktivierung verhaltensneutral.
- Q: Muss ein zweiter DocFX-A11Y-Workflow entstehen? A: Nein. Der bestehende `pages.yml`-Pfad ist die kanonische CI-Integration; 023 beweist und dokumentiert ihn, ohne äquivalente Jobs zu duplizieren.

## User Scenarios & Testing

### User Story 1 - Fokus textbasiert erkennen (Priority: P1)

Als tastaturnutzende oder assistiv arbeitende Person erhalte ich bei jedem relevanten Fokuswechsel einen stabilen, maschinenlesbaren Text, damit das aktive Ziel ohne Farbe oder Maus erkennbar ist.

**Independent Test**: Eine Anwendung fokussiert zwei opt-in Widgets per Tab und Shift+Tab. Jeder echte Wechsel erzeugt genau einen `cmFocusChanged`-Broadcast mit Ziel und AccessibleLabel; ein No-op-Fokuswechsel erzeugt keinen zusätzlichen Wechsel.

**Acceptance Scenarios**:

1. **Given** ein Widget implementiert `IAccessibleWidget`, **When** es Fokus erhält, **Then** enthält der Broadcast sein nichtleeres Label und seine aktuelle Fokusfähigkeit.
2. **Given** eine nicht migrierte View erhält Fokus, **When** der Broadcast entsteht, **Then** bleibt das Ziel maschinenlesbar und das Label ist ausdrücklich nicht vorhanden.
3. **Given** dasselbe Ziel ist bereits fokussiert, **When** Fokus erneut gesetzt wird, **Then** entsteht kein doppeltes Fokusereignis.

### User Story 2 - Shortcuts entdecken und per Tastatur bedienen (Priority: P1)

Als Nutzerin oder automatisierter Prüfer kann ich verfügbare Menü- und Statuszeilen-Shortcuts strukturiert abfragen und dieselben Aktionen per Tastatur auslösen.

**Independent Test**: Die Referenz-App listet Shortcut-Beschreibungen aus Menüleiste und Statuszeile auf; F10, Pfeiltasten, Enter, Tab, Shift+Tab und ein direkter Shortcut führen durch den realen App-Loop zu den erwarteten Zuständen.

**Acceptance Scenarios**:

1. **Given** Menü- und Statusdefinitionen, **When** Shortcuts abgefragt werden, **Then** sind Taste, Text, Befehl und Quelle stabil verfügbar.
2. **Given** ein deaktivierter oder trennender Eintrag, **When** Shortcuts gesammelt werden, **Then** wird keine ausführbare Fähigkeit behauptet.
3. **Given** Tastaturbedienung, **When** die Aktion ausgelöst wird, **Then** entspricht der sichtbare Zustand dem strukturierten Vertrag.

### User Story 3 - Fokusfähige Controls vollständig per Tastatur prüfen (Priority: P1)

Als Maintainer erhalte ich eine explizite Inventur der fokussierbaren Controls und automatisierte Proof-Pfade für ihre jeweils anwendbaren Tab-, Shift+Tab-, Pfeil-, Enter- und Shortcut-Interaktionen.

**Independent Test**: Eine kontrollierte Testmatrix weist für jede inventarisierte `Selectable`-Control-Familie anwendbare Tasten oder ein begründetes `N/A` nach und prüft Fokus-, Zustands- und Rejection-Grenzen ohne Maus.

### User Story 4 - High Contrast sichtbar und text-first aktivieren (Priority: P2)

Als Person mit Kontrastbedarf kann ich ein vordefiniertes High-Contrast-Schema aktivieren. Die Referenz-App zeigt den aktiven Modus zusätzlich als Text; kein wesentlicher Zustand wird nur durch Farbe vermittelt.

**Independent Test**: Vor und nach Aktivierung werden Schemaidentität, konkrete Vorder-/Hintergrundzellen, lesbarer Text und Deaktivierungs-/Fallback-Verhalten geprüft.

### User Story 5 - Dokumentations-A11Y dauerhaft in CI beweisen (Priority: P2)

Als Maintainer sehe ich, dass DocFX-Seiten bei Main- und Pull-Request-Läufen über Playwright und Axe geprüft werden und generierte Ergebnisse nicht eingecheckt werden.

**Independent Test**: Workflow-Trigger, DocFX-Build, `npm ci`, Browsernachweis und Axe-Test sind statisch und lokal nachvollziehbar; der Remote-Lauf ist PR-Evidence.

## Edge Cases

- Leere oder nur aus Leerraum bestehende AccessibleLabels werden abgelehnt.
- Eine nicht migrierte View bleibt fokussierbar, ohne eine erfundene Bezeichnung zu liefern.
- Doppelte Shortcut-Schlüssel bleiben quellenbezogen sichtbar; sie werden nicht still überschrieben.
- Separatoren, deaktivierte Menüeinträge und nicht ausführbare Statushinweise werden nicht als ausführbare Shortcuts gemeldet.
- Eine deaktivierte oder unsichtbare View wird von der Fokusnavigation übersprungen.
- High Contrast auf einer View ohne Zeichenpuffer bleibt zustandsstabil und darf nicht werfen.
- Kleine Terminalgrößen behalten Fokus-, Status- und Modusidentität als Text.
- Axe- oder Browserausfall ist ein fehlgeschlagener Nachweis, kein bestanden markierter Skip.

## Requirements

### Functional Requirements

- **FR-001**: Das Core-Modul MUST ein öffentliches, opt-in `IAccessibleWidget` mit `AccessibleLabel`, `AccessibleDescription` und `CanReceiveFocus` bereitstellen.
- **FR-002**: Alle neuen öffentlichen A11Y-Verträge MUST vollständige DE-first/EN-second XML-Dokumentation besitzen.
- **FR-003**: Jeder echte Fokuswechsel im Anwendungs-Shell-Flow MUST über den bestehenden Fokus-Broadcast einen typisierten Text-Payload liefern.
- **FR-004**: Der Fokus-Payload MUST Ziel, optionales Label und aktuelle Fokusfähigkeit enthalten; nicht migrierte Views MUST ohne falsches Label unterstützt werden.
- **FR-005**: Wiederholtes Setzen desselben Fokusziels MUST ein No-op bleiben.
- **FR-006**: Das Framework MUST eine unveränderliche, programmgesteuert abfragbare Shortcut-Beschreibung mit Taste, Text, Befehl und Quelle bereitstellen.
- **FR-007**: `TMenuBar` und `TStatusLine` MUST ihre ausführbaren Shortcuts über den strukturierten Vertrag exponieren.
- **FR-008**: Separatoren, deaktivierte oder nicht ausführbare Einträge MUST von ausführbaren Shortcut-Ergebnissen ausgeschlossen sein.
- **FR-009**: Die Tastaturmatrix MUST alle aktuell inventarisierten fokussierbaren Controls abdecken und Tab, Shift+Tab, Pfeiltasten, Enter sowie direkte Shortcuts jeweils prüfen oder begründet als nicht anwendbar dokumentieren.
- **FR-010**: Fokus-, Shortcut- und Aktivierungs-Proofs MUST ohne Maus und ohne echte Terminalabhängigkeit deterministisch ausführbar sein.
- **FR-011**: Das Framework MUST ein benanntes High-Contrast-Schema mit einfacher expliziter Aktivierung anbieten.
- **FR-012**: High Contrast MUST mindestens Hintergrund, normalen Text, hervorgehobenen Text, Auswahl und Status sichtbar unterscheiden und zusätzlich textbasiert erkennbar sein.
- **FR-013**: Ohne Aktivierung MUST das bestehende Farb- und Laufzeitverhalten unverändert bleiben.
- **FR-014**: Eine kleine Referenz-App MUST Fokuslabel, strukturierte Shortcuts, Tastaturnavigation und High Contrast über den realen App-Loop sichtbar demonstrieren.
- **FR-015**: Der Referenz-App-Proof MUST konkreten Zustand, View-Hierarchie und gerenderte Buffer-/Cell-Ausgabe kombinieren.
- **FR-016**: Der bestehende DocFX-Workflow MUST auf `main`, Pull Requests und manuelle Ausführung reagieren und Axe-Fehler als Fehler behandeln.
- **FR-017**: Generierte DocFX-, Browser-, Test- und Coverage-Ausgaben MUST aus Git ausgeschlossen bleiben.
- **FR-018**: `Pflichtenheft.md` MUST PF-A11Y-Einträge für Widget, Fokus, Shortcut, Tastatur, Kontrast und Docs-CI erhalten.
- **FR-019**: Relevante Guides MUST DE-first/EN-second, CEFR-B2, semantisch und text-first sein.
- **FR-020**: Neue oder geänderte nicht-triviale Logik MUST auf didaktische Inline-Kommentare geprüft werden; Kommentare erklären Warum, Randbedingung oder Proof-Grenze.
- **FR-021**: Alle fünf Agent-Guidance-Oberflächen MUST bei gemeinsam geänderter A11Y-Guidance atomar synchronisiert werden.
- **FR-022**: Historische Quellen unter `tv203s/` MUST read-only bleiben; moderne A11Y-Verträge werden als bewusste Erweiterung ohne historisches Pendant dokumentiert.
- **FR-023**: Erkannte weitergehende native Assistive-Technik-, Vollmigration- oder Terminal-WCAG-Themen MUST als `FollowUpHardening` abgegrenzt werden.
- **FR-024**: Die sechs Governance-Presets MUST mit Owner, Reviewer, Datum, Ergebnis, Restrisiko und Re-Evaluation-Trigger bewertet werden.

### Governance Applicability

- **Security Governance v0.6.0**: Eingabevalidierung für Labels/Shortcuts, keine Geheimnisse, keine neue Lieferkette. ASVS, SBOM, VEX, SLSA, OpenSSF, AI-SBOM, NIS2, CRA, EU AI Act und DORA sind triggerbasiert `N/A`, solange keine Abhängigkeit, Distribution, Runtime-AI oder regulierte Dienstgrenze entsteht.
- **Architecture Governance v0.5.0**: STRIDE/CIA/CAPEC betrachtet Event-Payload, Registry und Farbschema. S-ADR, arc42-Strukturänderung, Zero Trust, SAMM, BSI C3A und BSI C5 sind `N/A`, solange keine Deployment-, Cloud-, Provider- oder verteilte Grenze geändert wird.
- **iSAQB Architecture Governance v0.2.0**: Kleine kohärente Verträge, bestehender Event-Flow und vorhandene Controls werden wiederverwendet; keine breite Framework-Revision.
- **A11Y Governance v0.4.0**: voll anwendbar für Tastatur, Fokus, Kontrast, Text-Alternativen, Docs-WCAG-2.2-AA, zweisprachige CEFR-B2-Inhalte und didaktische Kommentarprüfung.
- **Cross-Platform Governance v0.2.0**: Runtime- und CI-Verhalten auf macOS/Linux sowie Windows/WSL betrachten; neue Skripte sind nicht geplant und daher Script-Parität `N/A`.
- **Agent Parity Governance v0.3.0**: fünf TuiVision-Agentoberflächen gemeinsam prüfen; `.specify/templates/` sind `N/A`, sofern keine projektweite Vorlage bewusst geändert wird.

### Key Entities

- **Accessible Widget**: opt-in Textvertrag eines UI-Elements.
- **Focus Announcement**: unveränderlicher Snapshot eines echten Fokuswechsels.
- **Accessible Shortcut**: abfragbare Taste-Aktion-Quelle-Beschreibung.
- **Keyboard Coverage Entry**: Control-Familie, anwendbare Tasten, Proof und N/A-Begründung.
- **Color Scheme**: benannte semantische Farbrollen einschließlich High Contrast.
- **A11Y Reference State**: sichtbarer App-Zustand für Fokus, Shortcut und Kontrast.
- **Governance Decision**: `Applicable`, `N/A` oder `Open` mit vollständiger Evidence.

## Success Criteria

- **SC-001**: 100 % der echten Fokuswechsel im Referenzpfad liefern genau einen maschinenlesbaren Payload; No-op-Fokuswechsel liefern keinen zusätzlichen Payload.
- **SC-002**: 100 % der ausführbaren Menü-/Status-Shortcuts der Referenz-App sind strukturiert abfragbar und per Tastatur beweisbar.
- **SC-003**: 100 % der inventarisierten fokussierbaren Control-Familien besitzen für jede geforderte Tastenklasse einen bestandenen Proof oder ein überprüftes `N/A`.
- **SC-004**: High Contrast wird mit einem Aufruf aktiviert und ist durch Schemaidentität, Text und konkrete Zellen nachweisbar.
- **SC-005**: Die Referenz-App besteht App-Loop-, Zustand-, View- und Buffer-/Cell-Proof in Standard- und schmalem Viewport.
- **SC-006**: Alle betroffenen Release-Tests bestehen; alle fünf gate-relevanten Assemblies erreichen mindestens 70 % Zeilenabdeckung.
- **SC-007**: DocFX erzeugt null Warnungen/Fehler und Playwright/Axe besteht alle DocFX-Smokes lokal und im erforderlichen Remote-Workflow.
- **SC-008**: 100 % der neuen öffentlichen APIs haben vollständige zweisprachige XML-Dokumentation; Guide und Pflichtenheft bleiben text-first verständlich.
- **SC-009**: Alle Governance-Zeilen und Follow-ups besitzen vollständige Owner-, Review-, Risiko- und Re-Evaluation-Felder.

## Assumptions

- Das bestehende Eventmodell, `TGroup.SelectNext`, `TMenuBar`, `TStatusLine`, `TConsoleBuffer`, FakeDriver und `pages.yml` sind stabile Wiederverwendungspunkte.
- „CanFocus=true“ aus dem Lastenheft entspricht in diesem Framework einer sichtbaren, nicht deaktivierten View mit `TViewOptions.Selectable`; die Evidence hält diese Terminologieabbildung fest.
- Native Screenreader-Bridges und eine Vollmigration aller bestehenden Controls bleiben nachfolgende Features.

## Out of Scope

- Native AT-SPI-, NSAccessibility-, UI-Automation- oder Speech-Integration.
- Vollständige WCAG-Konformitätsbehauptung für Terminals oder alle historischen Anwendungen.
- Maus als primärer oder einziger Bedienpfad.
- Migration sämtlicher bestehender Controls auf `IAccessibleWidget`.
- Neue Runtime-Abhängigkeiten, Cloud-Dienste, Telemetrie oder persistente Benutzerprofile.
- Funktionale oder visuelle Änderungen an Wave 1 bis Wave 4.
- Feature 024 oder Wave-5-Implementierung.

## Dependencies and Ordering

- Läuft nach `022-wave4-visual-component-porting` und vor Wave 5.
- Nutzt die bestehenden Framework-, Smoke-, DocFX- und A11Y-Pfade ohne deren Verantwortungsgrenzen zu duplizieren.
