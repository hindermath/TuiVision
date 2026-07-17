# Feature Specification: Wave-5 TP7 Showcase Remediation

**Feature Branch**: `033-wave5-tp7-showcase-remediation`
**Created**: 2026-07-17
**Status**: Draft
**Binding Input**: `Lastenheft_18_Wave5-TP7-Showcase-Remediation.033-wave5-tp7-showcase-remediation.md`

## Klärungen / Clarifications

### Session 2026-07-17

- Keine formale Rückfrage ist erforderlich. Lastenheft 18, die vollständige
  Feature-032-Delta-Matrix und der aktuelle `MergeAndSync`-Auftrag legen
  Feature-Identität, zehn Beispiele, Lieferumfang, Proof-Modell und
  Stop-Grenzen vollständig fest.
- Feature 033 ist ausschließlich die sichtbare zweite Wave-5-Stufe. Die
  funktionale C#-Logik aus Feature 032 bleibt bindend und wird nicht erneut
  portiert.
- Jedes Beispiel erhält eine reale Hauptkomponente, eine echte Statuszeile und
  einen tastaturerreichbaren `Help -> Description`-Pfad. Maus bleibt
  ergänzend.
- Ein kleiner Frameworkdefekt darf nur als test-first `SmallFrameworkFix`
  bearbeitet werden. Breitere oder API-brechende Befunde werden
  `FollowUpHardening`.
- Wave 6 und der Post-Wave-6-Portfolio-Audit werden nicht gestartet. Nach
  Feature 033 ist eine separate Prüfung des tatsächlichen Deltas erforderlich.

*No formal question is required. The binding intake, Feature-032 delta matrix,
and current delivery authority completely define the ten-example showcase
scope. Feature 033 preserves the proven C# behavior and does not start Wave 6.*

## Nutzungsszenarien und Prüfung / User Scenarios and Testing

### User Story 1 - Rechner als vollständiges Showcase bedienen (Priority: P1)

Als Lernender möchte ich den Rechner als sichtbares, fokussierbares
TuiVision-Widget mit Display, Tasten, Shortcuts, Status und Beschreibung
bedienen, damit Fachlogik und Frameworkpfad gemeinsam verständlich sind.

*As a learner, I want to operate the calculator as a visible, focusable
TuiVision widget with display, keys, shortcuts, status, and description.*

**Why this priority**: Der Rechner ist der kleinste repräsentative Slice für
die wiederholbare Showcase-Komposition.

**Independent Test**: Eine gültige und eine abgelehnte Rechnerfolge laufen
durch den echten App-Loop in einer stabilen `40x12`-Ansicht und belegen
Zustand, konkrete View-Identität, Fokus, Status, Beschreibung und Zellen.

**Acceptance Scenarios**:

1. **Given** den normalen Start, **When** eine Rechneraktion per Tastatur
   ausgelöst wird, **Then** sind Display, Tastenraster, Ergebnis und Status
   gleichzeitig sichtbar.
2. **Given** eine Division durch null, **When** sie ausgeführt wird, **Then**
   bleibt der letzte gültige Wert erhalten und die Ablehnung ist textlich
   erkennbar.
3. **Given** `Help -> Description`, **When** der Pfad per Tastatur geöffnet
   wird, **Then** erklärt er Zweck, Bedienung, moderne Abweichung und
   Proof-Grenze.

---

### User Story 2 - Demo, Editor und Hilfe vollständig erleben (Priority: P1)

Als Anwendungsentwickler möchte ich Demo, Editor und Hilfe über reale Menüs,
Fenster, Controls und Dialogpfade bedienen, damit zentrale TuiVision-Flows
nicht nur als Textzusammenfassung sichtbar sind.

*As an application developer, I want to operate demo, editor, and help through
real menus, windows, controls, and dialogs.*

**Why this priority**: Diese drei Anwendungen tragen die breitesten
Application-, Desktop-, Fokus-, Datei- und Help-Verträge.

**Independent Test**: Jede Anwendung besitzt einen realen App-Loop-Smoke für
ihren Kernpfad sowie einen constrained Layout- und Description-Proof.

**Acceptance Scenarios**:

1. **Given** `Tp7Demo`, **When** Tile, Cascade, Next oder Close ausgelöst wird,
   **Then** sind Fensterfamilie, Fokus und Status nachvollziehbar.
2. **Given** `Tp7Edit`, **When** Datei-, Edit- oder Search-Pfade bedient
   werden, **Then** bleiben Editor, Modified-State, Konflikt und Safe-Close
   sichtbar und kontrolliert.
3. **Given** `Tp7Help`, **When** ein Topic, Cross-Reference, Back,
   Compilerfehler oder unbekannter Kontext gewählt wird, **Then** ist der
   entsprechende Viewer- oder Diagnosezustand sichtbar.

---

### User Story 3 - Ressourcen sichtbar und kontrolliert verwenden (Priority: P1)

Als Maintainer möchte ich Resource-Demo und Generator über reale Dialog-,
Menü-, Eingabe-, Fortschritts- und Fehleroberflächen bedienen, ohne die
geschlossenen Daten- und Pfadgrenzen zu verlassen.

*As a maintainer, I want to operate the resource demo and generator through
real UI composition without leaving their closed data and path boundaries.*

**Why this priority**: Sichtbare Bedienung darf die in Feature 032 bewiesene
atomare und allowlist-basierte Verarbeitung nicht aufweichen.

**Independent Test**: Gültige und ungültige Resource-/Generatorpfade laufen
durch reale Commands und zeigen konkrete Dialog-, Status- und Cell-Evidence.

**Acceptance Scenarios**:

1. **Given** gültige benannte Records, **When** sie geladen werden, **Then**
   erscheinen rekonstruierter Dialog, Menü und Status sichtbar.
2. **Given** einen kontrollierten Zielnamen, **When** Generate ausgelöst wird,
   **Then** sind Ziel, Fortschritt und Ergebnis sichtbar.
3. **Given** Traversal oder ungültige Resource-Daten, **When** sie verarbeitet
   werden, **Then** bleibt die Ablehnung atomar und textorientiert.

---

### User Story 4 - Tabellen-, Kalender-, Puzzle- und Mauswidgets nutzen (Priority: P2)

Als Lernender möchte ich die vier kleineren Fachbeispiele über konkrete
fokussierbare Widgets und vollständige Tastaturpfade bedienen.

*As a learner, I want to operate the smaller domain examples through concrete
focusable widgets and complete keyboard paths.*

**Why this priority**: Diese Beispiele vervollständigen die sichtbare
Komponentenbreite und die text-first A11Y-Evidence.

**Independent Test**: Jedes Beispiel besitzt einen normalen und constrained
App-Loop-Proof mit Zustand, View, Fokus, Status und gerenderten Zellen.

**Acceptance Scenarios**:

1. **Given** die ASCII-Tabelle, **When** Pfeile, Paging oder direkte Auswahl
   verwendet werden, **Then** sind 16x16-Raster und fokussierter Bytewert
   sichtbar.
2. **Given** Kalender oder Puzzle, **When** Navigation erfolgt, **Then** sind
   Tages- oder Kachelfokus sowie Ablehnung textlich erkennbar.
3. **Given** den Mausdialog, **When** Capability, Doppelklickstufe,
   Button-Reihenfolge oder Aktivierung bedient werden, **Then** bleibt ein
   gleichwertiger Tastaturpfad verfügbar und Host-Mutation bleibt ausgeschlossen.

### Randfälle / Edge Cases

- Ein constrained Viewport bietet weniger Platz als die bevorzugte Komposition.
- Ein Pflichttext würde abgeschnitten oder ein Control mit Status/Menu
  überlappen.
- Fokus wechselt nach Entfernen oder Schließen eines Fensters.
- Ein Shortcut erreicht denselben Command wie Menü oder sichtbarer Button.
- Eine Datei- oder Generatoranforderung verlässt das kontrollierte Test-Root.
- Resource- oder Help-Eingabe ist unvollständig, doppelt oder unbekannt.
- Maus-Capability fällt während lokaler Interaktion aus.
- Ein Pointer-Pfad ist nicht verfügbar, der Tastaturpfad aber vollständig.
- Ein entdeckter Defekt erfordert eine breite Framework- oder API-Änderung.
- Ein Remote-Job ist grün, führt aber nicht den behaupteten Showcase-Proof aus.
- Ein Reviewer ist wegen Quote oder Providergrenze nicht verfügbar.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Feature 033 MUSS Lastenheft 18, Feature 032 und dessen
  vollständige Showcase-Delta-Matrix als bindende Basis behandeln.
- **FR-002**: Der Lauf MUSS genau `Tp7Demo`, `Tp7Edit`, `Tp7Help`,
  `Tp7ResourceDemo`, `Tp7ResourceGenerator`, `Tp7AsciiTable`,
  `Tp7Calculator`, `Tp7Calendar`, `Tp7Puzzle` und `Tp7MouseDialog` abdecken.
- **FR-003**: Jedes Beispiel MUSS genau eine Frameworkentscheidung aus
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder
  `FollowUpHardening` erhalten.
- **FR-004**: Jedes Beispiel MUSS eine reale sichtbare Hauptkomponente, eine
  echte `TStatusLine` und einen tastaturerreichbaren
  `Help -> Description`-Pfad besitzen.
- **FR-005**: Der normale Start MUSS Zweck und primäre Bedienoberfläche im
  ersten Frame zeigen.
- **FR-006**: Jeder Kernpfad MUSS vollständig per Tastatur erreichbar sein;
  Maus DARF NIE der einzige Pfad sein.
- **FR-007**: Fokus, Auswahl, Ablehnung, Capability und Fallback MÜSSEN
  textorientiert erkennbar sein.
- **FR-008**: Primäre Proofs MÜSSEN den echten App-Loop ausführen und
  Fachzustand, konkrete View-Identität sowie gerenderte Buffer-/Cell-Evidence
  verbinden.
- **FR-009**: Direkte Helfer DÜRFEN nur `SetupOnly` oder
  `SupplementalProof` sein.
- **FR-010**: Jedes Beispiel MUSS mindestens einen stabilen constrained
  Viewport ohne Überlappung oder abgeschnittenen Pflichttext besitzen.
- **FR-011**: `Tp7Calculator` MUSS zuerst als test-first Referenz-Slice mit
  Display, Tastenraster, Shortcuts, `40x12`-Layout und Description-Proof
  geliefert werden.
- **FR-012**: `Tp7Demo` MUSS eine sichtbare Fensterfamilie und die Commands
  Tile, Cascade, Next, Close sowie Help über bestehende Frameworkpfade zeigen.
- **FR-013**: `Tp7Edit` MUSS reale Editor-Chrome und tastaturerreichbare File-,
  Edit- und Search-Pfade zeigen, ohne Datei- oder Konfliktgrenzen zu ändern.
- **FR-014**: `Tp7Help` MUSS Compilerdiagnosen, Topic-Viewer,
  Cross-Reference, Back, unbekannten Kontext und Fallback sichtbar machen.
- **FR-015**: `Tp7ResourceDemo` MUSS geladene Dialog-, Menü- und Statusdaten
  als reale sichtbare Komposition zeigen.
- **FR-016**: `Tp7ResourceGenerator` MUSS Zielwahl, Generate-Command,
  Fortschritt, Ergebnis und Ablehnung innerhalb des kontrollierten Roots
  sichtbar machen.
- **FR-017**: `Tp7AsciiTable` MUSS ein sichtbares 16x16-Raster, Pfeil- und
  Paging-Navigation sowie den fokussierten Bytewert als Text zeigen.
- **FR-018**: `Tp7Calendar` MUSS Monats- und Tagesraster sowie deterministische
  Tages- und Monatsnavigation zeigen.
- **FR-019**: `Tp7Puzzle` MUSS ein stabiles 4x4-Raster, Leerfeld,
  Kachelfokus, gültige Bewegung und sichtbare Ablehnung zeigen.
- **FR-020**: `Tp7MouseDialog` MUSS reale fokussierbare Settings-Controls,
  Aktivierungsziel, Capability-Text und vollständige Shortcuts zeigen.
- **FR-021**: Die funktionalen Fach-, Datei-, Resource-, Help-, Capability-
  und Sicherheitsverträge aus Feature 032 DÜRFEN NICHT erweitert oder
  abgeschwächt werden.
- **FR-022**: Datei- und Generator-Proofs DÜRFEN nur source-controlled
  Fixtures oder test-eigene temporäre Verzeichnisse verwenden.
- **FR-023**: Ungültige Resource- oder Help-Eingabe DARF kein Teilmodell
  veröffentlichen.
- **FR-024**: `HostMutationPerformed` MUSS in allen Maus-Proofs `false`
  bleiben; Capability-Verlust MUSS lokale Interaktion beenden.
- **FR-025**: Historische `TVDEMOS/`-Quellen MÜSSEN read-only bleiben und
  definieren Lernzweck und Nutzerfluss, nicht C#-Quellform.
- **FR-026**: Free Vision, Terminal.GUI v1.9.x und `magiblot/tvision` DÜRFEN
  nur als nicht normative Vorgängerevidence dienen.
- **FR-027**: Gemeinsame reine Showcase-Komposition DARF in der bestehenden
  Wave-5-Assembly liegen; eine zweite examples-lokale Frameworkschicht ist
  unzulässig.
- **FR-028**: `SmallFrameworkFix` MUSS einen beobachtbaren Red-Test, einen
  begrenzten Fix und Regressionsevidence besitzen.
- **FR-029**: Breite, API-brechende oder neue fachliche Arbeit MUSS als
  `FollowUpHardening` mit Owner und Re-Evaluation-Trigger abgegrenzt werden.
- **FR-030**: Learner-facing Text MUSS German-first/English-second,
  CEFR-B2, semantisch und text-first sein.
- **FR-031**: Jede Description MUSS Zweck, Bedienung, moderne Abweichung,
  Sicherheits-/Capability-Grenze und Proof-Grenze erklären.
- **FR-032**: Neue oder geänderte nicht triviale Logik MUSS auf selektiven
  didaktischen Inline-Kommentarwert geprüft werden.
- **FR-033**: Geänderte öffentliche APIs MÜSSEN vollständige XML-Dokumentation
  besitzen und den DocFX-/A11Y-Pfad auslösen.
- **FR-034**: Die Feature-Evidence MUSS genau zehn Showcase-Zeilen, zehn
  Frameworkentscheidungen, zehn Main-/Status-/Description-Proofs, zehn
  normale Startpfade und zehn constrained Layout-Proofs enthalten.
- **FR-035**: Das Shortcut-Inventar MUSS für jedes Beispiel vollständig sein.
- **FR-036**: Negative Evidence-Validatoren MÜSSEN fehlende, doppelte,
  unbekannte, keyboard-unvollständige, layout-leere oder
  grenzverletzende Zeilen ablehnen.
- **FR-037**: Der Lauf MUSS gezielte Showcase-Smokes, zehn normale
  Entry-Point-Smokes, vollständige Release-Tests, Coverage, Formatierung,
  DocFX/Axe, Plattform-, Paritäts-, Secret-, Supply-Chain-, Review- und
  Exact-Head-Gates ausführen.
- **FR-038**: Vor jedem einzelnen `dotnet build` oder `dotnet test` MUSS der
  manuelle Build-Zähler genau einmal erhöht werden.
- **FR-039**: Fehlende Reviewer oder Quota-Ausfälle MÜSSEN als fehlender
  Review und nicht als Pass dokumentiert werden.
- **FR-040**: Der finale Diff MUSS null Änderungen unter `TVDEMOS/`, `TVFM/`,
  `tv203s/` oder externen Vergleichsquellen enthalten.
- **FR-041**: Der Lauf DARF keine neue Dependency, erneute Pascal-Portierung,
  breite Frameworkrevision, Host-Mutation oder Wave-6-Implementierung
  einführen.
- **FR-042**: Lastenheft 18 MUSS nach erfolgreicher Abnahme über den
  Repository-Rename-Workflow archiviert werden.
- **FR-043**: Pflichtenheft, Reihenfolge, Guides, Agent-Kontexte und
  Projektstatistik MÜSSEN denselben finalen Wave-5- und Wave-6-Zustand nennen.
- **FR-044**: Der autonome Lauf MUSS Run-State, Artefakthashes, Tasks,
  Authority, letzte Operation und nächste exakte Aktion pflegen.
- **FR-045**: Commit, Push, PR, Review-Konvergenz, Merge, Branch-Bereinigung
  und sauberer lokaler `HEAD == origin/main`-Sync MÜSSEN nachgewiesen werden.
- **FR-046**: Wave 6 MUSS bis zur separaten Prüfung des tatsächlichen
  Feature-033-Deltas blockiert bleiben.
- **FR-047**: Feature 034 und der Post-Wave-6-Audit DÜRFEN NICHT gestartet
  werden.

### Governance-Anforderungen / Governance Requirements

- **GR-001**: `security-governance` v0.6.0 MUSS NIST SSDF, CWE Top 25,
  kontrollierte Datei-/Resource-/Help-Eingaben, Secrets und Supply Chain als
  `Applicable` behandeln. ASVS, AI-SBOM und regulatorische Trigger bleiben
  ohne Scope-Änderung begründet `N/A`.
- **GR-002**: `architecture-governance` v0.5.0 MUSS STRIDE/CIA/CAPEC für
  Eingabe-, Datei-, Resource-, Help-, Command- und Capability-Grenzen prüfen.
  Zero Trust, BSI C3A und BSI C5 bleiben ohne Cloud-/Serviceänderung `N/A`.
- **GR-003**: `isaqb-architecture-governance` v0.2.0 MUSS Qualitätsziele,
  Laufzeitsichten, Komponentenverantwortung, Risiken und Technical Debt
  nachvollziehbar halten.
- **GR-004**: `a11y-governance` v0.4.0 MUSS Keyboard-Vollständigkeit, Fokus,
  Shortcuts, text-first Status/Fallback, constrained Layout,
  German-first/English-second CEFR-B2 und didaktische Kommentare anwenden.
- **GR-005**: `cross-platform-governance` v0.2.0 MUSS Linux-, macOS- und
  Windows-Laufzeitproof prüfen. Skriptparität bleibt `N/A`, solange kein
  Skript geändert wird.
- **GR-006**: `agent-parity-governance` v0.3.0 MUSS alle gepflegten
  Agent-Kontexte gemeinsam prüfen.
- **GR-007**: `autonomous-run-governance` v0.2.2 MUSS Evidence-first,
  Konvergenz, State-/Authority-Prüfung, Exact-Head-Gates, Review, Closeout und
  Retrospektive anwenden.

### Schlüsselentitäten / Key Entities

- **Showcase Row**: Genau eine vollständige Evidence-Zeile je Beispiel mit
  Frameworkentscheidung, Main-, Status-, Description-, Keyboard-, Layout- und
  Proof-Feldern.
- **Showcase Composition**: Reale Hauptkomponente, Statuszeile,
  Description-Pfad und fokussierbare Controls eines Beispiels.
- **Shortcut Inventory**: Vollständige tastaturerreichbare Commands und ihre
  sichtbaren Texte pro Beispiel.
- **Constrained Layout Proof**: Stabiler kleiner Viewport mit Pflichttexten,
  Fokus und null Überlappung.
- **Controlled Boundary**: Bestehende Datei-, Resource-, Help- oder
  Capability-Grenze aus Feature 032.

## Erfolgsmaße / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: Genau zehn von zehn Beispielen erfüllen das Drei-Schichten-Modell.
- **SC-002**: Genau zehn von zehn Kernpfaden sind vollständig per Tastatur
  erreichbar; kein Beispiel ist maus-exklusiv.
- **SC-003**: Genau zehn primäre Smokes verbinden App-Loop, Fachzustand,
  konkrete View-Identität und Buffer-/Cell-Evidence.
- **SC-004**: Genau zehn constrained Layout-Proofs bestehen ohne Überlappung
  oder abgeschnittenen Pflichttext.
- **SC-005**: Genau zehn Frameworkentscheidungen und zehn vollständige
  Showcase-Zeilen sind eindeutig vorhanden.
- **SC-006**: Datei-, Resource-, Help- und Maus-Negativfälle bewahren ihre
  atomaren beziehungsweise host-neutralen Grenzen.
- **SC-007**: Alle zehn normalen Release-Entry-Points starten und beenden den
  kontrollierten Smoke-Pfad erfolgreich.
- **SC-008**: Alle gezielten und vollständigen Release-Tests bestehen; die
  fünf Framework-Assemblies behalten mindestens 70 Prozent Line Coverage.
- **SC-009**: DocFX baut mit null Warnungen und Fehlern; Playwright/Axe besteht.
- **SC-010**: Linux, macOS und Windows führen die anwendbaren Gates auf dem
  final reviewten Head erfolgreich aus.
- **SC-011**: Der finale Diff enthält null historische Quellen-, Dependency-,
  Host-Mutations- oder Wave-6-Änderungen.
- **SC-012**: Der Lauf endet mit vollständigen Tasks, null umsetzbaren
  Review-Threads, gemergtem PR und sauberem lokalem `main == origin/main`.

## Annahmen / Assumptions

- Feature 032 ist die bindende funktionale Baseline.
- Die bestehende gemeinsam kompilierte Wave-5-Assembly ist der richtige Ort
  für reine gemeinsame Showcase-Komposition.
- Vorhandene Controls tragen die zehn Deltas ohne breites Framework-Redesign.
- AI ist ausschließlich Entwicklungswerkzeug und keine Runtime-Komponente.
- Das externe Community-Preset-Verfahren blockiert diesen Featurelauf nicht.

## Scope-Grenzen / Scope Boundaries

### In Scope

- sichtbare interaktive Showcase-Komposition für genau zehn TP7-Beispiele
- vollständige Tastatur-, Fokus-, Status-, Description- und Layout-Proofs
- Guides, Evidence, Validatoren und notwendige Status-/Statistikpflege
- kleine test-first Frameworkfixes nur unter dem definierten Gate

### Out of Scope

- erneute Fachportierung oder mechanische Pascal-Übersetzung
- neue Dependencies, Dienste, Prozesse, Shells, PTYs oder native Bridges
- breite Framework- oder API-Revision
- beliebige Benutzerdateien oder Host-Konfigurationsänderungen
- Wave 6, Feature 034 und Post-Wave-6-Audit

### Decision and Follow-up Model

- Framework: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, `FollowUpHardening`
- Governance: `Applicable`, `N/A`, `Open`
- Proof role: `PrimaryProof`, `SupplementalProof`, `SetupOnly`
