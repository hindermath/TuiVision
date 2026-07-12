# Feature Specification: Terminal and Charset Hardening

**Feature Branch**: `021-terminal-charset-hardening`
**Created**: 2026-07-12
**Status**: Draft
**Input**: Binding intake `Lastenheft_05_TerminalCharsetAndEmulation.md`

## Clarifications

### Session 2026-07-12

- Q: Welches Emulations-Subset ist für 021 verbindlich? → A: Text plus BEL/BS/TAB/CR/LF, CSI `A/B/C/D`, `H/f`, `J`, `K`, `m` und vollständiger Reset; alles Weitere ist `Unsupported`.
- Q: Welche festen Größen- und Kapazitätsgrenzen gelten? → A: 4.096 Verlaufszellen, 64 Zeichen pro Steuerfolge, höchstens 4 numerische Parameter und Werte von 0 bis 9.999.
- Q: Welches Ersatzzeichen ist verbindlich? → A: Unicode `U+FFFD` für jede ungültige oder nicht abbildbare Zeicheneinheit.
- Q: Welches Font-Fixture-Format ist der erste Vertrag? → A: Rohes 8x16-Bitmap mit 256 Glyphen, 16 Bytes je Glyphe und exakt 4.096 Bytes; andere Formate bleiben `Unsupported`.
- Q: Welche Profilgrenze ist verbindlich? → A: Pflichtfelder `ProfileId` und `Charset`; optionale Font-/Vordergrund-/Hintergrundwerte nutzen sichere Defaults, unbekannte oder doppelte Keys lehnen das ganze Profil ab.
- Q: Darf die Sitzung einen Hostprozess, eine Shell oder ein PTY starten? → A: Nein; 021 ist ein reines In-Process-Transkript- und Präsentationsmodell mit kontrollierter Eingabe.
- Q: Wie verhalten sich Scroll und Verlauf? → A: Überlauf schiebt die sichtbaren Zeilen nach oben, hält den Cursor in der letzten Zeile und speichert verdrängte Zellen FIFO bis 4.096 Zellen.
- Q: Welche Resize-Semantik gilt? → A: Die links-oben liegende Schnittmenge bleibt erhalten, neue Zellen sind leer und der Cursor wird in die neuen Grenzen geklemmt.
- Q: Was umfasst ein vollständiger Sitzungsreset? → A: Sichtbarer Buffer und Verlauf leer, Cursor auf 0/0, Standardattribute aktiv, Parserstatus und Fallbackmeldung zurückgesetzt.
- Q: Welche Wirkung hat BEL? → A: Erhöht nur einen beobachtbaren In-Process-Hinweiszähler und setzt Textstatus; kein Hostton und kein visueller Host-Flash.

## User Scenarios & Testing

### User Story 1 - Kontrollierte Terminal-Sitzung / Controlled Terminal Session (Priority: P1)

Als Maintainerin oder Maintainer möchte ich eine vollständig beobachtbare
Terminal-Sitzung mit Ausgabe, Cursor, Attributen, Verlauf und Beendigung
verwenden. Wave-4-Beispiele sollen später keine direkten Host-Konsolenzugriffe
als Frameworkersatz benötigen.

As a maintainer, I want a fully observable terminal session with output,
cursor, attributes, history, and termination. Later Wave-4 examples must not
need direct host-console access as a replacement for framework behavior.

**Why this priority**: Der Sitzungszustand ist die gemeinsame Grundlage für
Emulation, Charset-Mapping, Buffer-Proof und ehrliche Host-Fallbacks.

**Independent Test**: Eine kontrollierte Sitzung verarbeitet Text, unterstützte
Steueraktionen und Quit, während Zustand, Cursor und sichtbare Zellen ohne
physisches Terminal vollständig geprüft werden.

**Acceptance Scenarios**:

1. **Given** eine neue Sitzung mit fester sichtbarer Größe, **When** Text und
   Zeilenwechsel eintreffen, **Then** sind Zeichen, Attribute, Cursor und
   Verlauf deterministisch beobachtbar.
2. **Given** Ausgabe erreicht eine sichtbare Grenze, **When** weitere Zeichen
   eintreffen, **Then** gelten dokumentierte Wrap-, Scroll- und
   Clipping-Regeln ohne Zugriff auf Hostzustand.
3. **Given** die Sitzung wird beendet oder verworfen, **When** Cleanup läuft,
   **Then** bleiben keine Eingabe, Sequenz oder Hoständerung aktiv.

---

### User Story 2 - Begrenzte Emulation mit sicherem Fallback / Bounded Emulation with Safe Fallback (Priority: P1)

Als Entwicklerin oder Entwickler eines späteren Terminalbeispiels möchte ich
einen kleinen, dokumentierten Satz an Steuersequenzen verwenden. Unbekannte,
unvollständige oder zu große Sequenzen sollen atomar und sichtbar begrenzt
werden.

As a developer of a later terminal example, I want a small documented set of
control sequences. Unknown, incomplete, or oversized sequences must be bounded
atomically and reported visibly.

**Why this priority**: Ein bewusst kleiner Vertrag verhindert eine
unbeabsichtigte vollständige ANSI-/XTerm-Emulation und behandelt
Terminaleingaben als nicht vertrauenswürdig.

**Independent Test**: Eine Matrix aus unterstützten und abgelehnten
Steuerfolgen prüft Ausgabe, Cursor, Farben, Löschaktionen, Recovery und
Unsupported-Status gegen denselben Sitzungszustand.

**Acceptance Scenarios**:

1. **Given** eine vollständig unterstützte Steuerfolge, **When** sie verarbeitet
   wird, **Then** ändert sie genau den dokumentierten Sitzungszustand.
2. **Given** eine unbekannte, abgeschnittene, überlange oder ungültige Folge,
   **When** sie verarbeitet wird, **Then** entsteht keine Teilaktion und die
   nächste unabhängige gültige Eingabe bleibt nutzbar.
3. **Given** eine Folge liegt außerhalb des akzeptierten Subsets, **When** ihr
   Ergebnis angezeigt wird, **Then** nennt ein textorientierter Status die
   Unsupported-Grenze ohne Hostmanipulation.

---

### User Story 3 - Deterministisches Charset- und Font-Mapping / Deterministic Charset and Font Mapping (Priority: P1)

Als Lernende oder Maintainer möchte ich historische KOI8-R-Zeichen und
kontrollierte Fontdaten deterministisch in moderne Zeichen und Proof-Zustände
abbilden. Nicht abbildbare Werte sollen ein festes Ersatzzeichen und einen
erklärbaren Status erhalten.

As a learner or maintainer, I want historical KOI8-R characters and controlled
font data to map deterministically to modern characters and proof state.
Unmappable values must receive one fixed replacement character and an
explainable status.

**Why this priority**: `cyrillic` und `fonts` benötigen einen
Frameworkvertrag; Host-Codepages, installierte Fonts oder Screenshots wären
nicht reproduzierbar.

**Independent Test**: Kontrollierte Zeichen- und Font-Fixtures beweisen
erfolgreiche Mappings, Ersatzzeichen, Glyphenmetadaten, ungültige Daten und
read-only Generatorgrenzen ohne Installation auf dem Host.

**Acceptance Scenarios**:

1. **Given** bekannte KOI8-R-Bytes, **When** sie abgebildet werden, **Then**
   entstehen die erwarteten Unicode-Zeichen und ein erfolgreicher Mappingstatus.
2. **Given** ein Wert ist im gewählten Mapping nicht darstellbar, **When** er
   verarbeitet wird, **Then** erscheint das feste Ersatzzeichen und der Status
   nennt die Ersetzung.
3. **Given** Font- oder Rasterdaten sind gültig, unvollständig oder außerhalb
   der Grenzen, **When** sie geladen werden, **Then** entstehen entweder stabile
   Glyphenmetadaten oder eine atomare lesbare Ablehnung ohne Host-Fontänderung.

---

### User Story 4 - Profile, Plattformen und ehrliche Evidence / Profiles, Platforms, and Honest Evidence (Priority: P2)

Als Benutzerin oder Benutzer auf macOS, Linux oder Windows/WSL möchte ich
erkennen, welche Terminal-, Charset- und Fontfähigkeiten aktiv, emuliert,
deaktiviert oder nicht unterstützt sind. Fehlende optionale Profilwerte und
nicht verfügbare angeforderte Capabilities sollen deterministisch auf sichere
Defaults fallen; ungültige Schemawerte werden atomar abgelehnt.

As a user on macOS, Linux, or Windows/WSL, I want to know which terminal,
charset, and font capabilities are active, emulated, disabled, or unsupported.
Missing optional profile values and unavailable requested capabilities must use
safe deterministic defaults; invalid schema values are rejected atomically.

**Why this priority**: Die spätere Wave-4-Darstellung darf Plattformunterschiede
nicht als universelle Unterstützung ausgeben oder Konfiguration im Beispiel
neu erfinden.

**Independent Test**: Eine Profil- und Hostmatrix prüft gültige, fehlende,
ungültige und nicht unterstützte Werte sowie getrennte deterministische,
Remote-CI- und physische Evidence-Klassen.

**Acceptance Scenarios**:

1. **Given** ein gültiges Terminalprofil, **When** es ausgewählt wird, **Then**
   sind verwendete Werte, Quelle und Capability-Zustand beobachtbar.
2. **Given** ein optionaler Key fehlt oder eine angeforderte Capability ist
   nicht verfügbar, **When** das Profil geladen wird, **Then** gilt ein
   dokumentierter sicherer Default und eine lesbare Fallback-Begründung.
3. **Given** ein Pflichtfeld, Schemawert oder Key ist ungültig, unbekannt oder
   doppelt, **When** das Profil geladen wird, **Then** wird das ganze Profil
   ohne teilweise veröffentlichte Werte abgelehnt.
4. **Given** eine physische Hostprüfung ist nicht möglich, **When** Evidence
   erfasst wird, **Then** bleibt sie `NotRun` und wird nicht mit
   deterministischem In-Process-Proof verwechselt.

### Edge Cases

- Leere Ausgabe, leere Eingabe und Ende mitten in einer Steuerfolge.
- Überlange Steuerfolge oder sehr große numerische Parameter.
- Cursorbewegung und Löschung außerhalb des sichtbaren Bereichs.
- Wrap, Scroll und Resize an Ein-Zeichen- oder Ein-Zeilen-Grenzen.
- Kombinierte CR/LF-, Backspace- und Tab-Folgen am Zeilenrand.
- Ungültige UTF-8-, isolierte Ersatzcodeeinheiten und nicht darstellbare
  historische Bytes.
- Leere, abgeschnittene, übergroße oder inkonsistente Font-/Raster-Fixtures.
- Doppelte, unbekannte, leere oder nicht unterstützte Profilwerte.
- Capability-Wechsel oder Sitzungsende während unvollständiger Eingabe.
- Nicht-interaktive, umgeleitete oder headless Ein-/Ausgabe.
- Host-Uhr-, Locale-, Codepage- oder Fontunterschiede, die keinen
  deterministischen Proof beeinflussen dürfen.

## Requirements

### Functional Requirements

- **FR-001**: Das System MUSS einen benannten, kontrollierten
  Terminal-Sitzungsvertrag für Eingabe, Ausgabe, Cursor, Attribute, Verlauf,
  Status und Beendigung bereitstellen. Der Vertrag ist vollständig in-process
  und DARF keinen Hostprozess, keine Shell und kein PTY starten.
- **FR-002**: Der Sitzungsvertrag MUSS ohne physisches Terminal deterministisch
  nutzbar und vollständig beobachtbar sein.
- **FR-003**: Direkte unstrukturierte Host-Konsolenzugriffe DÜRFEN weder der
  primäre Frameworkvertrag noch der primäre Akzeptanznachweis sein. Externe
  Prozessausführung und beliebige Host-Ein-/Ausgabe sind nicht Teil der Sitzung.
- **FR-004**: Textausgabe MUSS dokumentierte Regeln für Zeichen, Tab,
  Backspace, CR/LF, Wrap, Scroll, Clipping und Cursorfortschritt besitzen. Ein
  Überlauf schiebt sichtbare Zeilen nach oben, hält den Cursor in der letzten
  Zeile und übernimmt verdrängte Zellen in einen FIFO-Verlauf bis 4.096 Zellen.
  Bei Resize bleibt die links-oben liegende Schnittmenge erhalten, neue Zellen
  sind leer und der Cursor wird in die neuen Grenzen geklemmt.
- **FR-005**: Sitzungszustand MUSS bei Reset, Quit, Capability-Verlust und
  Dispose vollständig und idempotent bereinigt werden. Ein vollständiger Reset
  leert sichtbaren Buffer und Verlauf, setzt den Cursor auf 0/0, aktiviert
  Standardattribute und löscht Parser- sowie Fallbackstatus.
- **FR-006**: Das Emulations-Subset MUSS Text, BEL, BS, TAB, CR, LF, relative
  Cursorbewegung über CSI `A/B/C/D`, absolute Position über CSI `H/f`,
  Display-/Zeilenlöschung über CSI `J/K`, 16-Farben-Attribute und Reset über
  CSI `m` sowie vollständigen Sitzungsreset abdecken. BEL erhöht nur einen
  beobachtbaren In-Process-Hinweiszähler und setzt Textstatus; Hostton und
  visueller Host-Flash sind unzulässig. Andere Steuerfolgen sind `Unsupported`;
  vollständige ANSI-/VT-/XTerm-Parität ist unzulässig.
- **FR-007**: Unterstützte Steuerfolgen MÜSSEN vollständig vor Anwendung
  validiert und höchstens einmal veröffentlicht werden.
- **FR-008**: Unbekannte, unvollständige, überlange oder ungültige Steuerfolgen
  MÜSSEN atomar begrenzt werden, ohne Teilaktion oder Verlust der nächsten
  unabhängigen gültigen Eingabe. Der Verlauf MUSS auf 4.096 Zellen begrenzt
  sein. Eine
  Steuerfolge DARF höchstens 64 Zeichen und höchstens vier numerische Parameter
  mit Werten von 0 bis 9.999 enthalten; jede Überschreitung wird atomar
  abgelehnt.
- **FR-009**: Unsupported-, Rejected- und Fallback-Ergebnisse MÜSSEN als
  textorientierter Status unterscheidbar sein.
- **FR-010**: Der Lauf MUSS Unicode als moderne Darstellung und KOI8-R als
  expliziten historischen Mappingvertrag abdecken; weitere Codepages benötigen
  eine neue Entscheidung.
- **FR-011**: Charset-Mapping MUSS erfolgreiche, ersetzte, ungültige und nicht
  unterstützte Werte mit Zeichen, Status und Begründung unterscheiden.
- **FR-012**: Nicht abbildbare Eingabe MUSS ein einziges dokumentiertes
  Ersatzzeichen verwenden: Unicode `U+FFFD`. Host-Locale oder Host-Codepage
  DÜRFEN das Ergebnis nicht verändern.
- **FR-013**: Font-/Rasterdaten MÜSSEN nur aus source-controlled Fixtures oder
  kontrollierten Testpfaden gelesen werden. Der erste Vertrag ist ein rohes
  8x16-Bitmap mit 256 Glyphen, 16 Bytes je Glyphe und exakt 4.096 Bytes;
  Kompression und andere Formate sind `Unsupported`.
- **FR-014**: Font-Fixtures MÜSSEN Breite 8, Höhe 16, Glyphenzahl 256,
  16 Bytes je Glyphe und Gesamtlänge 4.096 vor Veröffentlichung vollständig
  validieren.
- **FR-015**: Historische Fontgeneratoren, Fontloader, Keyboardmaps und
  Host-Setup-Skripte bleiben read-only Intent- oder Fixture-Grenzen und DÜRFEN
  im Proof keine persistente Hoständerung ausführen.
- **FR-016**: Terminalprofile MÜSSEN gültige Werte, fehlende Keys, ungültige
  Werte und nicht unterstützte Capabilities deterministisch unterscheiden.
  `ProfileId` und `Charset` sind erforderlich. Font-ID, Vordergrund und
  Hintergrund sind optional und fallen auf eingebautes 8x16, Grau und Schwarz
  zurück. Unbekannte oder doppelte Keys lehnen das gesamte Profil atomar ab.
- **FR-017**: Jeder Profilfallback MUSS Default, Quelle, Begründung und
  Capability-Zustand beobachtbar machen. Ein angeforderter, aber nicht
  verfügbarer Font oder Hostvertrag verwendet den sicheren Default und meldet
  `Unsupported`, statt Hostzustand zu verändern.
- **FR-018**: Primäre Proofs MÜSSEN konkrete Session-, Cursor-, Mapping-,
  Profil- und Fontzustände mit gerenderten Buffer-/Cell-Regionen verbinden.
- **FR-019**: Direkte Helper DÜRFEN Setup oder Zusatzbeweis liefern, aber nicht
  die einzige Akzeptanzschicht für spätere App-Loop-/View-Pfade sein.
- **FR-020**: Der Lauf MUSS mindestens einen Controls-nahen Integrationsproof
  vorbereiten, der Eingabe, Sitzung, sichtbare Zellen, Status und Quit verbindet,
  ohne ein Wave-4-Beispiel zu portieren.
- **FR-021**: Für macOS, Linux und Windows/WSL MUSS je eine überprüfbare
  Host-/Capability-Evidence oder ein ehrlicher `Unsupported`-/`NotRun`-Nachweis
  mit Re-Evaluation-Trigger vorliegen.
- **FR-022**: Deterministische In-Process-, Remote-CI- und physische
  Host-Evidence MÜSSEN als getrennte Nachweisklassen behandelt werden.
- **FR-023**: Für jeden Terminal-, Emulations-, Charset-, Font-, Profil- und
  Plattformvertrag MUSS eine Entscheidung `UseExistingFramework`,
  `SmallFrameworkFix`, `IntentionalDeviation` oder `FollowUpHardening` mit
  Evidence-Pfad dokumentiert werden.
- **FR-024**: Wiederverwendbare Terminal-, Parser-, Mapping-, Font- oder
  Profil-Logik DARF nicht als lokale `examples/`-Sonderlösung entstehen.
- **FR-025**: Historisch abgeleitete Absicht MUSS anhand relevanter Quellen und
  Header unter `tv203s/` read-only geprüft werden; materielle Abweichungen
  MÜSSEN dokumentiert werden.
- **FR-026**: Neue oder geänderte nicht-triviale Parser-, Zustands-, Mapping-,
  Fallback- und Proof-Logik MUSS auf didaktischen Kommentarbedarf geprüft werden.
- **FR-027**: Benutzer- und Maintainer-Dokumentation MUSS Deutsch zuerst,
  Englisch danach, CEFR-B2 und text-first zugänglich sein.
- **FR-028**: Feature-Evidence MUSS Scope, Vertragsmatrizen,
  Frameworkentscheidungen, historische Absicht, Hostmatrix, Tests, Governance,
  Remote-State und Follow-ups vollständig erfassen.
- **FR-029**: Pflichtenheft, Agent-Kontexte und Projektstatistik MÜSSEN nach
  Abschluss auf `Lastenheft_Wave4-Visual-Component-Porting.md` weitergeführt
  werden.
- **FR-030**: Der Lauf DARF keine sichtbare Portierung von `cyrillic`, `eterm`,
  `fonts`, `terminal` oder `xterm`, keine Wave-3-Nacharbeit und keine breite
  native Emulatorrevision enthalten.

### Constitution Requirements

- **CR-001**: Das Feature MUSS die TuiVision-Level-2-Registry-Zeile und C# als
  Memory-Safe-Language-Kontext verwenden.
- **CR-002**: NIST SSDF, CWE Top 25, sichere Eingabevalidierung, Größenlimits
  und fail-safe Zustände sind anwendbar und MÜSSEN Evidence erhalten.
- **CR-003**: STRIDE/CIA/CAPEC MÜSSEN proportional für Steuersequenzen,
  Charset-/Fontdaten, Profile, Sitzungszustand und Hostgrenzen geprüft werden.
- **CR-004**: OWASP ASVS ist `N/A`, solange keine Web-/API-/Auth-Fläche
  hinzukommt; jede Scope-Änderung löst Neubewertung aus.
- **CR-005**: Neue SBOM-, VEX-, SLSA-, OpenSSF- oder AI-SBOM-Evidence ist
  `N/A`, solange keine Abhängigkeit, Distribution oder Produkt-AI hinzukommt.
- **CR-006**: NIS2, CRA, EU AI Act und DORA bleiben `N/A` für das lokale
  Trainingsframework; Distribution oder regulierter Betrieb löst Neubewertung
  aus.
- **CR-007**: S-ADR und arc42-Security-Updates sind bei neuer
  Architektur-/Trust-Boundary-Entscheidung erforderlich; andernfalls wird
  bestehende Evidence mit Begründung aktualisiert oder wiederverwendet.
- **CR-008**: Zero Trust, SAMM, BSI C3A und BSI C5 sind `N/A`, solange keine
  verteilte, Cloud-, Provider- oder Betriebsgrenze geändert wird.
- **CR-009**: WCAG 2.2 AA, Tastaturvollständigkeit, Textstatus und bilinguale
  CEFR-B2-Dokumentation sind für sichtbare Proof- und Guide-Flächen anwendbar.
- **CR-010**: Cross-Platform-Governance ist für Host-, Terminal-, Charset- und
  Fontunterschiede anwendbar; Skriptparität ist `N/A`, solange kein Skript
  geändert wird.
- **CR-011**: Agent-Parity ist bei Kontextänderung auf allen fünf gepflegten
  Agent-Dateien anwendbar; `.specify/templates/` bleiben `N/A`, sofern keine
  neue generische Workflow-Regel entsteht.
- **CR-012**: Das Feature MUSS die sechs installierten Presets in den
  akzeptierten Versionen und Prioritäten als Governance-Kontext verwenden.
- **CR-013**: Vor dem ersten roten Testbefehl MUSS ein Compile-Surface-Check
  Imports, öffentliche XML-Dokumentation, Harness-Helfer,
  Zustands-/Ownership-Assertionen und Shared-/Generated-Source-Identität prüfen.
- **CR-014**: Negative Fälle DÜRFEN nur als projektlokale Red-Matrix gebündelt
  werden, wenn Einzelgrenzen und Ownership explizit bleiben.
- **CR-015**: Remote-Gates MÜSSEN vor dem Merge geprüft werden. Aktuelle
  Reviewed-Head- und echte Post-Merge-Fakten MÜSSEN einen vorab benannten
  kausalen Closeout-Evidence-Pfad verwenden, wenn ihr Commit die eigene Aussage
  entwerten würde.
- **CR-016**: Operationales Commit-, Push-, PR- und Merge-Verhalten gehört in
  Plan, Tasks und Feature-Evidence und darf keine Benutzeranforderung oder
  implizite Remote-Autorität erzeugen.

### Key Entities

- **Terminal Session**: Sichtbare Größe, Cursor, aktuelles Attribut, Buffer,
  Verlauf, Status, Capability und Lebenszyklus.
- **Terminal Observation**: Kontrollierte Text- oder Steuerfolgeneingabe vor
  vollständiger Validierung und Zustandsänderung.
- **Emulation Result**: Akzeptierte, abgelehnte oder nicht unterstützte Aktion
  mit Zustandsdelta, Status und Recovery-Grenze.
- **Charset Mapping Result**: Eingabewert, Quellcharset, Unicode-Zeichen,
  Mappingstatus, Ersatzgrund und Hostunabhängigkeit.
- **Font Fixture**: Source-controlled Glyphenquelle mit Abmessungen,
  Glyphenzahl, Datenlänge, Prüfergebnis und Generatorgrenze.
- **Terminal Profile**: Benannte, validierte Werte für Darstellung,
  Charset-/Fontwahl und Capability-Fallbacks einschließlich Quelle und Defaults.
- **Host Evidence Record**: Host/Terminal, Evidence-Klasse, Capability,
  Ergebnis, Restrisiko und Re-Evaluation-Trigger.
- **Framework Decision Record**: Vertragsbereich, vorhandene Komponente,
  lokale Logik, Entscheidung, Evidence und Follow-up.

## Success Criteria

### Measurable Outcomes

- **SC-001**: 100 % der dokumentierten Sitzungsaktionen besitzen
  deterministische Assertions für Zustand, Cursor und sichtbare Zellen.
- **SC-002**: 100 % der unterstützten Steuerfolgen ändern genau den erwarteten
  Zustand; alle dokumentierten ungültigen Klassen erzeugen keine Teilaktion.
  Tests beweisen die Grenzen 4.095/4.096/4.097 Verlaufszellen,
  63/64/65 Sequenzzeichen, 4/5 Parameter und 9.999/10.000 Parameterwert ohne
  Teilzustand oder Verlust der nächsten gültigen Eingabe.
- **SC-003**: Die Emulationsmatrix deckt Text/Wrap/Scroll, BEL/BS/TAB/CR/LF,
  CSI `A/B/C/D`, `H/f`, `J`, `K`, `m` und vollständigen Reset jeweils mit
  Positiv-, Grenz- und Unsupported-Nachweis ab.
- **SC-004**: KOI8-R- und Unicode-Mappings bestehen eine Positiv-, Ersatz-,
  ungültige und Hostunabhängigkeitsmatrix mit ausschließlich `U+FFFD` als
  Ersatzzeichen.
- **SC-005**: Mindestens eine gültige 8x16/256/4.096-Byte-Fixture und vier
  negative Grenzen (Abmessung, Glyphenzahl, Trunkierung und Übergröße) sind
  atomar bewiesen; kein Test installiert einen Host-Font.
- **SC-006**: Gültige, fehlende, ungültige und nicht unterstützte Profilwerte
  liefern jeweils Default, Quelle, Status und Begründung. Tests decken fehlende
  Pflichtfelder, jedes optionale Default, unbekannte und doppelte Keys sowie
  einen nicht verfügbaren Font-/Hostvertrag ab.
- **SC-007**: Der Integrationsproof verbindet kontrollierte Eingabe,
  Sitzungszustand, Cursor, Status, View-Identität, gerenderte Cells und Quit.
- **SC-008**: macOS, Linux und Windows/WSL besitzen je mindestens einen
  überprüfbaren `Pass`, `Unsupported` oder `NotRun`-Eintrag mit
  Re-Evaluation-Trigger; Nachweisklassen werden nicht vermischt.
- **SC-009**: 0 Dateien unter `examples/` enthalten neue Terminalparser,
  Charsetmapper, Fontloader oder Profilfallbacks.
- **SC-010**: 100 % der sechs Vertragsbereiche haben genau eine zulässige
  Frameworkentscheidung und einen Evidence-Pfad.
- **SC-011**: Alle ausgelösten Format-, Test-, Coverage-, Dokumentations-,
  A11Y-, Secret-, Generated-Output- und Remote-Gates sind bestanden.
- **SC-012**: Alle neuen oder geänderten nicht-trivialen Flows besitzen eine
  dokumentierte Kommentarentscheidung ohne triviale Was-Kommentare.
- **SC-013**: Der Lauf endet mit archiviertem Lastenheft, aktualisiertem
  Folge-Intake, vollständiger Evidence und sauber synchronisiertem `main`.

## Assumptions

- Vorhandene Console-Cells, Buffer-Snapshots, Driver-Capability-Buckets und
  Controls-Rendering bleiben die primären wiederverwendeten Verträge.
- Das erste historische Charset ist KOI8-R, weil beide Cyrillic-Varianten und
  die Fontquellen darauf ausgerichtet sind; weitere Codepages bleiben Follow-up.
- Ein kleiner 16-Farben-Vertrag reicht für Wave-4-Grundlagen; True Color und
  vollständige Terminalattributparität sind nicht erforderlich.
- Sitzungs-, Parser-, Mapping- und Fontproofs laufen kontrolliert in-process;
  physische Hosts sind zusätzliche, getrennt klassifizierte Evidence.
- Vorhandene Mausunterstützung aus Feature 020 wird nicht erweitert. Ein
  Terminalprofil darf Capability-Status lesen, aber keine neue Maussemantik
  einführen.
- Runtime-/Produkt-AI, Datenbank, Netzwerkdienst, beliebige Nutzerdaten und neue
  Abhängigkeiten sind nicht Teil des Features.

## Scope Boundaries

### In Scope

- Kontrollierter Terminal-Sitzungs- und Lebenszyklusvertrag.
- Bewusst begrenztes Text-, Cursor-, Lösch- und 16-Farben-Subset.
- Unicode-/KOI8-R-Mapping mit festem Ersatzverhalten.
- Kontrollierte Font-/Raster-Fixtures und atomare Validierung.
- Deterministische Terminalprofile und Capability-Fallbacks.
- Buffer-/Cell-/View-, Host-, historische, Guide- und Governance-Evidence.

### Out of Scope

- Sichtbare Wave-4-Portierung der fünf Beispiele.
- Vollständige ANSI-, VT100-, XTerm-, Eterm-, Maus- oder Fontemulation.
- Persistente Host-Terminal-, Shell-, Font-, Keyboardmap-, Codepage- oder
  Profiländerungen.
- Wave-3-Editor-/Help-/Stream-Nacharbeit und TP7-Anschlusswellen.
- Breite Frameworkrevision oder lokale Beispiel-Ersatzabstraktionen.
- Neue Pakete, externe Dienste, Datenbanken, Netzwerk-Proof oder Produkt-AI.

### Decision and Follow-up Model

- `UseExistingFramework`: Bestehende Komponente erfüllt den Vertrag.
- `SmallFrameworkFix`: Kleiner Feature-bezogener Framework-Fix mit Tests.
- `IntentionalDeviation`: Bewusste, dokumentierte Abweichung.
- `FollowUpHardening`: Reales Problem außerhalb des akzeptierten 021-Scopes.
