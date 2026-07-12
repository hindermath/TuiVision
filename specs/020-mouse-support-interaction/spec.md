# Feature Specification: Mouse Support and Interaction Hardening

**Feature Branch**: `020-mouse-support-interaction`
**Created**: 2026-07-12
**Status**: Draft
**Input**: Binding intake `Lastenheft_04_MouseSupportAndInteraction.md`

## Clarifications

### Session 2026-07-12

- Q: Welcher erste Host-Protokollumfang ist verbindlich? -> A: SGR 1006 für interaktive macOS-/Linux-Terminals und WSL; native Windows Console bleibt ehrlich `Unsupported`.
- Q: Welche Grenze gilt für einen Doppelklick? -> A: Zwei linke Press-Aktionen auf derselben Zelle und demselben Ziel innerhalb von 500 ms monotonic time.
- Q: Welcher einzelne Drag-Pfad wird geliefert? -> A: Verschieben eines beweglichen `TWindow` am oberen Titelrahmen; `Ctrl+F5` plus Pfeile bleibt der Tastaturfallback.
- Q: Wie werden Host- und CI-Nachweise getrennt? -> A: Parser, Zustandsfolgen und Fallbacks sind deterministisch injizierbar; physische Host-Evidence wird nur als verfügbarer manueller Spot-Check behauptet.

## User Scenarios & Testing

### User Story 1 - Kontrollierter Maus-Ingress / Controlled Mouse Ingress (Priority: P1)

Als Benutzerin oder Benutzer eines unterstützten Terminals möchte ich, dass
reale Mausaktionen als dieselben Framework-Ereignisse ankommen, die Controls
bereits verstehen. Beispiele sollen keine eigenen Escape-Sequenzen oder
Mausbefehle auswerten müssen.

As a user of a supported terminal, I want real mouse actions to arrive as the
same framework events already understood by controls. Examples must not parse
their own escape sequences or mouse commands.

**Why this priority**: Ohne einen zentralen Ingress bleiben alle weiteren
Interaktionen nur Test-Hilfen oder beispielspezifische Sonderlogik.

**Independent Test**: Eine kontrollierte Host-Eingabe wird durch den echten
Ereignispfad geführt und erscheint einmal mit Position, Button, Phase und
Capability-Zustand im kanonischen Mausereignis.

**Acceptance Scenarios**:

1. **Given** Maussupport ist aktiv und der Host meldet eine gültige Aktion,
   **When** die Aktion den Runtime-Ingress erreicht, **Then** wird genau ein
   kanonisches Framework-Ereignis mit begrenzten Koordinaten erzeugt.
2. **Given** eine Eingabe ist unvollständig, unzulässig oder außerhalb des
   darstellbaren Bereichs, **When** sie ausgewertet wird, **Then** entsteht kein
   teilgültiges Mausereignis und der Eingabestrom bleibt nutzbar.
3. **Given** ein Beispiel nutzt Mausinteraktion, **When** seine Quellen geprüft
   werden, **Then** enthält es keinen eigenen Raw-Mausparser und keine lokale
   Ersatzabstraktion.

---

### User Story 2 - Fokus, Aktivierung und Doppelklick / Focus, Activation, and Double Click (Priority: P1)

Als Tastatur- und Mausbenutzer möchte ich Controls per Klick fokussieren und
aktivieren sowie eine klar begrenzte Doppelklickaktion auslösen können, ohne
dass Fokus oder Befehle doppelt zugestellt werden.

As a keyboard and mouse user, I want to focus and activate controls by click
and trigger one clearly bounded double-click action without duplicate focus or
command delivery.

**Why this priority**: Fokus und Aktivierung sind der kleinste sichtbare Nutzen
des Ingress-Vertrags und müssen vor breiterer Gestenlogik stabil sein.

**Independent Test**: Ein echter App-Loop mit mindestens zwei fokussierbaren
Views erhält Klick- und Doppelklickereignisse und beweist Fokus, Aktivierung,
Status und unveränderte Tastaturbedienung.

**Acceptance Scenarios**:

1. **Given** zwei fokussierbare Views sind sichtbar, **When** die zweite View
   einmal geklickt wird, **Then** erhält nur sie den Fokus und der sichtbare
   Textstatus nennt den Fokuswechsel.
2. **Given** ein aktivierbares Control ist fokussiert, **When** ein vollständiger
   Klick ausgeführt wird, **Then** wird seine normale Aktion genau einmal über
   den bestehenden Befehlsweg ausgelöst.
3. **Given** zwei passende Klicks liegen innerhalb der dokumentierten Zeit- und
   Positionsgrenze, **When** der zweite Klick abgeschlossen wird, **Then** wird
   genau eine Doppelklickbedeutung gemeldet; andere Folgen bleiben Einzelklicks.

---

### User Story 3 - Begrenzter Drag-Pfad / Bounded Drag Path (Priority: P2)

Als Benutzerin oder Benutzer möchte ich genau einen dokumentierten einfachen
Drag-Pfad verwenden können, der vorhandene Fenster- oder Scrollinteraktion
nutzt und bei Abbruch in einem konsistenten Zustand endet.

As a user, I want to use one documented simple drag path that reuses existing
window or scrolling interaction and ends in a consistent state when cancelled.

**Why this priority**: Ein realer Drag beweist Press-Move-Release-Zustand, ohne
Hover, Wheel oder vollständige Terminalemulation in den Lauf zu ziehen.

**Independent Test**: Ein App-Loop führt Press, begrenzte Bewegung und Release
aus und prüft die sichtbare Endposition sowie Abbruch und Tastaturfallback.

**Acceptance Scenarios**:

1. **Given** der dokumentierte Drag-Handle ist sichtbar, **When** Press, Move und
   Release innerhalb gültiger Grenzen eintreffen, **Then** ändert sich nur der
   erlaubte Zielzustand und bleibt vollständig im Desktopbereich.
2. **Given** ein Drag verliert Support oder wird abgebrochen, **When** kein
   gültiger Release folgt, **Then** bleibt kein hängender Drag-Zustand zurück.
3. **Given** Drag ist nicht verfügbar, **When** dieselbe Aufgabe per Tastatur
   ausgeführt wird, **Then** bleibt sie vollständig erreichbar und der Status
   erklärt den Fallback.

---

### User Story 4 - Ehrlicher Host- und Tastaturfallback / Honest Host and Keyboard Fallback (Priority: P1)

Als Benutzerin oder Benutzer auf einem nicht unterstützten oder deaktivierten
Host möchte ich weiterhin alle Pflichtaufgaben per Tastatur erledigen können
und den Capability-Zustand als Text sehen.

As a user on an unsupported or disabled host, I want to complete all required
tasks by keyboard and see the capability state as text.

**Why this priority**: Maus darf Barrierefreiheit und Plattformportabilität
nicht zu einer stillen Teilfunktion verschlechtern.

**Independent Test**: Derselbe Integrationspfad läuft mit aktivem, deaktiviertem
und nicht unterstütztem Capability-Zustand; Tastaturaktionen bleiben identisch
wirksam und der Status unterscheidet die drei Zustände.

**Acceptance Scenarios**:

1. **Given** der Host bietet keinen verlässlichen Maus-Ingress, **When** die App
   startet, **Then** bleibt Maus deaktiviert und alle Pflichtpfade funktionieren
   per Tastatur.
2. **Given** Maussupport wurde bewusst deaktiviert, **When** eine potenzielle
   Mausquelle Daten liefert, **Then** werden sie nicht als UI-Aktion zugestellt.
3. **Given** Support ist verfügbar, deaktiviert oder nicht verfügbar, **When**
   Hilfe oder Status angezeigt wird, **Then** ist der aktuelle Zustand
   textorientiert und ohne Farbe oder Pointer verständlich.

### Edge Cases

- Leere, abgeschnittene, überlange oder nicht numerische Host-Eingaben.
- Negative, extrem große oder außerhalb des aktuellen Buffers liegende
  Koordinaten.
- Unbekannte Buttons, ungültige Phasenfolgen und Bewegung ohne vorheriges Press.
- Release nach Fokuswechsel, Fensterentfernung oder Capability-Deaktivierung.
- Zwei Klicks mit zu großer Zeit- oder Positionsabweichung.
- Doppelklickzustand nach Host-Uhrsprung oder monotonic-time-Grenze.
- Drag über Desktopgrenzen, auf nicht ziehbare Views oder nach App-Shutdown.
- Terminal-Resize zwischen Host-Eingang und View-Dispatch.
- Nicht-interaktive, umgeleitete oder headless Ein-/Ausgabe.
- Mehrere Eingaben in einem Read-Block ohne Verlust nach einer fehlerhaften
  Sequenz.

## Requirements

### Functional Requirements

- **FR-001**: Das System MUSS einen benannten Runtime-Ingress für reale oder
  kontrolliert injizierte Host-Mausbeobachtungen bereitstellen.
- **FR-002**: Der Ingress MUSS auf das bestehende kanonische Mausereignismodell
  abbilden und DARF keine konkurrierende UI-Mausabstraktion einführen.
- **FR-003**: Position, Button, Press/Move/Release-Phase und Doppelklickstatus
  MÜSSEN eindeutig und höchstens einmal pro akzeptierter Aktion zugestellt
  werden.
- **FR-004**: Host-Eingaben MÜSSEN als nicht vertrauenswürdig gelten und vor
  Veröffentlichung vollständig auf Syntax, Größe, Wertebereich und Zustand
  geprüft werden.
- **FR-005**: Ungültige oder unvollständige Eingaben MÜSSEN atomar abgelehnt
  werden, ohne Teilereignis oder Verlust nachfolgender gültiger Eingaben.
- **FR-006**: Maussupport MUSS explizit aktiviert, deaktiviert oder als nicht
  unterstützt klassifiziert sein; ein halbaktiver Zustand ist unzulässig.
- **FR-007**: Unterstützte Hostfamilien und erforderliche Terminalbedingungen
  MÜSSEN dokumentiert werden. SGR 1006 ist der erste unterstützte Vertrag für
  interaktive macOS-/Linux-Terminals und WSL; native Windows Console bleibt
  `Unsupported`, bis ein eigener belegter Backend-Vertrag vorliegt.
- **FR-008**: Click-to-focus MUSS genau eine geeignete sichtbare View fokussieren
  und nicht fokussierbare oder verdeckte Views unverändert lassen.
- **FR-009**: Click-to-activate MUSS die bestehende Control-/Command-Semantik
  genau einmal verwenden und DARF keine lokale Beispielübersetzung benötigen.
- **FR-010**: Doppelklick MUSS eine dokumentierte Zeit-, Button- und
  Positions- und Zielgrenze besitzen. Nur zwei linke Press-Aktionen auf
  derselben Zelle und demselben Ziel innerhalb von 500 ms monotonic time gelten
  als Doppelklick; nicht passende Folgen bleiben Einzelklicks.
- **FR-011**: Der Lauf MUSS genau einen einfachen Drag-Vertrag für eine
  bestehende Fensterinteraktion liefern: Ein bewegliches `TWindow` wird am
  oberen Titelrahmen gezogen. Desktopgrenzen, Release, Abbruch und der
  bestehende `Ctrl+F5`-Pfeiltastenfallback MÜSSEN dokumentiert sein.
- **FR-012**: Drag-Zustand MUSS bei Release, Abbruch, Capability-Verlust,
  Zielentfernung und Shutdown vollständig beendet werden.
- **FR-013**: Hover, Wheel, Touch, Mehrfinger, beliebige Buttons und vollständige
  XTerm-/Raw-Protokollparität DÜRFEN nicht stillschweigend in Scope gelangen.
- **FR-014**: Alle Pflichtinteraktionen MÜSSEN einen vollständigen
  Tastaturfallback behalten.
- **FR-015**: Capability-, Fokus-, Aktivierungs-, Doppelklick-, Drag- und
  Fallbackzustände MÜSSEN als sichtbarer Text nachweisbar sein.
- **FR-016**: Mindestens ein Integrationspfad MUSS den echten App-Loop, zwei
  fokussierbare Views, Aktivierung, Doppelklick, den begrenzten Drag und
  Tastaturfallback nachweisen.
- **FR-017**: Primäre UI-Nachweise MÜSSEN konkreten Zustand, View-Identität und
  gerenderte Buffer-/Cell-Regionen verbinden; direkte Helper sind nur Setup
  oder ergänzender Proof.
- **FR-018**: Core-, Controls- und Driver-Grenzen MÜSSEN fokussierte positive,
  negative und Zustandsfolge-Tests besitzen.
- **FR-019**: Für macOS, Linux und Windows/WSL MUSS jeweils reviewbare
  Host-Evidence oder ein ehrlicher nicht ausgelöster/unsupported Nachweis
  vorliegen. Deterministische CI-Injektion beweist Parser und Zustandsvertrag,
  ersetzt aber keinen nicht ausgeführten physischen Host-Spot-Check.
- **FR-020**: Beispiele DÜRFEN keine eigenen Raw-Mausparser, konkurrierenden
  Mausmodelle oder wiederverwendbare lokale Ersatzlogik enthalten.
- **FR-021**: Für jeden Interaktionsvertrag MUSS eine Entscheidung
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening` mit Evidence-Pfad dokumentiert werden.
- **FR-022**: Historisch abgeleitete Maussemantik MUSS anhand relevanter
  `tv203s/`-Implementierungen und Header read-only geprüft werden; bewusste
  Abweichungen werden dokumentiert.
- **FR-023**: Neue oder geänderte nicht-triviale Ingress-, Dispatch-, Fokus-,
  Doppelklick-, Drag- und Proof-Logik MUSS auf didaktischen Kommentarbedarf
  geprüft werden.
- **FR-024**: Benutzer- und Maintainer-Dokumentation MUSS Deutsch zuerst,
  Englisch danach, CEFR-B2 und text-first zugänglich sein.
- **FR-025**: Feature-Evidence MUSS Scope, Hostmatrix, Framework-Entscheidungen,
  Parser-/Zustandsgrenzen, Tests, Governance, Remote-State und Follow-ups
  vollständig erfassen.
- **FR-026**: Pflichtenheft, Agent-Kontexte und Projektstatistik MÜSSEN nach
  Abschluss auf `Lastenheft_05_TerminalCharsetAndEmulation.md` weitergeführt
  werden.

### Constitution Requirements

- **CR-001**: Das Feature MUSS die TuiVision-Level-2-Registry-Zeile und C# als
  Memory-Safe-Language-Kontext verwenden.
- **CR-002**: NIST SSDF, CWE Top 25, sichere Eingabevalidierung und
  fail-safe Zustände sind anwendbar und MÜSSEN Evidence erhalten.
- **CR-003**: STRIDE/CIA/CAPEC MÜSSEN proportional für Host-Eingang,
  Zustandsmaschine, Dispatch und Capability-Grenzen geprüft werden.
- **CR-004**: OWASP ASVS ist `N/A`, solange keine Web-/API-/Auth-Fläche
  hinzukommt; jede Scope-Änderung löst Neubewertung aus.
- **CR-005**: Neue SBOM-, VEX-, SLSA-, OpenSSF- oder AI-SBOM-Evidence ist
  `N/A`, solange keine Abhängigkeit, Distribution oder Produkt-AI hinzukommt.
- **CR-006**: NIS2, CRA, EU AI Act und DORA bleiben `N/A` für das lokale
  Trainingsframework; Distribution oder regulierter Betrieb löst Neubewertung
  aus.
- **CR-007**: S-ADR und arc42-Security-Updates sind nur bei neuer
  Architektur-/Trust-Boundary-Entscheidung erforderlich; andernfalls ist die
  bestehende Evidence mit Begründung wiederzuverwenden.
- **CR-008**: Zero Trust, SAMM, BSI C3A und BSI C5 sind `N/A`, solange keine
  verteilte, Cloud-, Provider- oder Betriebsgrenze geändert wird.
- **CR-009**: WCAG 2.2 AA, Tastaturvollständigkeit, Textstatus und bilinguale
  CEFR-B2-Dokumentation sind für sichtbare Laufzeit- und Guide-Flächen
  anwendbar.
- **CR-010**: Cross-Platform-Governance ist für Host- und Terminalunterschiede
  anwendbar; Skriptparität ist `N/A`, solange kein Skript geändert wird.
- **CR-011**: Agent-Parity ist bei Kontextänderung auf allen fünf gepflegten
  Agent-Dateien anwendbar; `.specify/templates/` bleiben `N/A`, sofern keine
  neue generische Workflow-Regel entsteht.
- **CR-012**: Das Feature MUSS die sechs installierten Presets in den
  akzeptierten Versionen und Prioritäten als Governance-Kontext verwenden.
- **CR-013**: Vor dem ersten roten Testbefehl MUSS der Compile-Surface-Check
  Imports, öffentliche XML-Dokumentation, Harness-Helfer,
  Fokus-/Ownership-Assertionen und Linked-Source-Identität bewerten.
- **CR-014**: Negative Fälle DÜRFEN nur als projektlokale Red-Matrix gebündelt
  werden, wenn Einzelgrenzen und Ownership explizit bleiben.
- **CR-015**: Operationales Commit-, Push-, PR- und Merge-Verhalten gehört in
  Plan, Tasks und Feature-Evidence und darf keine Benutzeranforderung oder
  implizite Remote-Autorität erzeugen.

### Key Entities

- **Mouse Capability State**: Aktiviert, deaktiviert oder nicht unterstützt,
  einschließlich Hostfamilie, Bedingung und textorientierter Begründung.
- **Host Mouse Observation**: Unveröffentlichte, nicht vertrauenswürdige
  Eingabe mit Rohgrenze, Position, Button und Phase.
- **Canonical Mouse Event**: Vollständig validiertes Framework-Ereignis mit
  Position, Button, Phase und Doppelklickstatus.
- **Click Sequence**: Begrenzter Zustand für Button, Position, Zeit und
  Zielidentität zur Einzel-/Doppelklickentscheidung.
- **Drag Session**: Begrenzter Press-Move-Release-Zustand mit Ziel, Handle,
  Start, aktueller Position, End- oder Abbruchgrund.
- **Host Evidence Record**: Host/Terminal, Capability, Aktivierung, Proof,
  Ergebnis, Restrisiko und Neubewertungstrigger.
- **Framework Decision Record**: Vertrag, bestehende Komponente, lokale Logik,
  Entscheidung, Evidence und Follow-up.

## Success Criteria

### Measurable Outcomes

- **SC-001**: 100 % der akzeptierten Host-Mausbeobachtungen erzeugen genau ein
  kanonisches Ereignis; alle dokumentierten ungültigen Klassen erzeugen keines.
- **SC-002**: Click-to-focus und Click-to-activate bestehen jeweils positive,
  negative und Exactly-once-Nachweise im echten App-Loop.
- **SC-003**: Mindestens drei Doppelklickgrenzen (Zeit, Position, Button/Ziel)
  sind deterministisch bei exakt 500 ms, derselben Zelle und demselben linken
  Button/Ziel getestet; nur die passende Folge wird Doppelklick.
- **SC-004**: Genau ein begründeter Drag-Pfad beweist Press, mehrere Moves,
  Release, Grenzen und mindestens zwei Abbruchpfade ohne hängenden Zustand.
- **SC-005**: Alle Pflichtaufgaben bleiben bei deaktiviertem und nicht
  unterstütztem Maussupport vollständig per Tastatur ausführbar.
- **SC-006**: Der Integrationsproof verbindet für jede primäre Interaktion
  App-Loop, konkreten Zustand, View-Identität, Status und gerenderte Cells.
- **SC-007**: Core-, Controls- und Driver-Testgruppen bestehen vollständig;
  der volle Repository-Testlauf bleibt grün.
- **SC-008**: Jede der drei Hostfamilien macOS, Linux und Windows/WSL besitzt
  einen überprüfbaren Pass-, Unsupported- oder nicht ausgelösten Nachweis mit
  Neubewertungstrigger.
- **SC-009**: 0 Beispieldateien enthalten einen lokalen Raw-Mausparser oder eine
  konkurrierende Mausabstraktion.
- **SC-010**: 100 % der Interaktionsverträge haben genau eine zulässige
  Framework-Entscheidung und einen Evidence-Pfad.
- **SC-011**: Alle ausgelösten Format-, Test-, Coverage-, Dokumentations-,
  A11Y-, Secret-, Generated-Output- und Remote-Gates sind bestanden.
- **SC-012**: Alle neuen oder geänderten nicht-trivialen Flows besitzen eine
  dokumentierte Kommentarentscheidung ohne triviale Was-Kommentare.
- **SC-013**: Der Lauf endet mit archiviertem Lastenheft, aktualisiertem
  Folge-Intake, vollständiger Evidence und sauber synchronisiertem `main`.

## Assumptions

- Das vorhandene Framework-Mausmodell bleibt der kanonische UI-Vertrag.
- Der erste Host-Ingress unterstützt nur vollständige SGR-1006-Sequenzen für
  linkes Press, Move und Release; weitere Buttons, Wheel und Protokolle folgen
  später.
- Doppelklick verwendet eine monotone Zeitquelle oder kontrolliert injizierte
  Zeit, nicht die veränderliche Wanduhr.
- Das Verschieben eines beweglichen `TWindow` am Titelrahmen ist der einzige
  Drag-Vertikalslice; zusätzliche Drag-Ziele benötigen eine neue Entscheidung.
- Host-CI kann Capability-, Parser- und Fallbackverhalten deterministisch
  beweisen, auch wenn kein physisches Zeigegerät verfügbar ist.
- Runtime-/Produkt-AI, Datenbank, Netzwerkdienst und neue Abhängigkeiten sind
  nicht Teil des Features.

## Scope Boundaries

### In Scope

- Benannter, begrenzter Host-Maus-Ingress und Capability-Zustand.
- Validierung und Abbildung auf das bestehende Mausereignismodell.
- Click-to-focus, Click-to-activate und begrenzter Doppelklick.
- Genau ein begründeter einfacher Drag-Pfad.
- Sichtbarer Tastaturfallback und Hostmatrix.
- Framework-, Integrations-, Host-, Guide- und Governance-Evidence.

### Out of Scope

- Vollständige XTerm-, Raw- oder plattformspezifische Protokollparität.
- Hover, Wheel, Touch, Mehrfinger und beliebige zusätzliche Buttons.
- TP7-`MOUSEDLG.PAS` oder neue Beispielportierung.
- Wave-4-Terminal-/Charset-/Font-Implementierung.
- Breite Framework-Revision oder konkurrierendes Mausmodell.
- Neue Pakete, Dienste, Datenbanken oder Runtime-/Produkt-AI.
- Beispiel-lokale Parser oder wiederverwendbare Mouse-Helper als Frameworkersatz.

### Decision and Follow-up Model

- `UseExistingFramework`: Bestehende Komponente erfüllt den Vertrag.
- `SmallFrameworkFix`: Kleiner Feature-bezogener Framework-Fix mit Tests.
- `IntentionalDeviation`: Bewusste, dokumentierte Abweichung.
- `FollowUpHardening`: Reales Problem außerhalb des akzeptierten 020-Scopes.
