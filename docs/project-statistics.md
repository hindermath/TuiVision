Warning: truncated output (original token count: 72032)
Total output lines: 1454

# Projektstatistik TuiVision

Stand: 2026-07-17 (aktualisiert einschließlich Feature-036-Delivery und Preset v0.2.2)

Aktueller Zusatz: Feature 016 dokumentiert 157/157 Secure-Development-
Kontrollen, sechs behobene Medium-Funde, 498/498 grüne Release-Tests und eine
Coverage von mindestens 80,55 % in allen fünf Gate-Assemblies.

Feature 020 ergänzt einen atomar validierten Host-Mauseingang, Topmost-Fokus,
Exactly-once-Aktivierung, Doppelklick und genau einen begrenzten Titelzeilen-
Drag. Physische Host-Prüfung und deterministische CI-Evidence bleiben getrennt.

Feature 021 ergänzt eine rein in-process arbeitende Terminal-Session, ein
begrenztes C0-/CSI-Subset, hostunabhängiges KOI8-R-/Unicode-Mapping, eine rohe
8x16-Fixture, geschlossene Profile und ein sichtbares `TTerminalView`.

Feature 022 ergänzt `Terminal`, `Cyrillic`, `Fonts`, `ETerm` und `XTerm` als
sichtbare Drei-Schichten-Demos. Deterministische In-Process-Evidence bleibt von
physischer Hostbeobachtung getrennt; native Ressourcen erhalten ehrliche
textorientierte Fallbacks.

Der 022-Closeout und die anschließende Retrospektive härten den autonomen
Remote-Abschluss: ein Evidence-Closeout bleibt ohne Selbstreferenz ein Commit,
und doppelte Push-/PR-Workflow-Sätze werden als operatives Rauschen klassifiziert.

Feature 023 ergänzt die opt-in A11Y-Schicht. Die anschließende Retrospektive
bindet jede Build-Zählererhöhung an genau einen Build-/Testaufruf und lässt
Repository-Prüfhelfer bei fehlenden Abhängigkeiten fail-closed abbrechen.

Das aus den Feldläufen entstandene `autonomous-run-governance` ist jetzt als
v0.2.2 zusätzlich zu den sechs Standard-Presets aus dem öffentlichen Tag-ZIP
installiert. Maschinenlesbare Gate-Requirements und exakte HEAD-Evidence
schließen die False-Readiness-Grenze; Status, kooperativer Stopp, geschützte
Wiederaufnahme und validierter Laufzustand härten Unterbrechungen. Der
TuiVision-spezifische Codex-Skill bleibt als einzelner lokaler Override erhalten.

Der begrenzte Folgeabgleich führt Antigravity CLI mit `agy` als aktive
Google-Agentenoberfläche. Gemini CLI bleibt nur als historische oder ausdrücklich
benötigte Enterprise-/API-Kompatibilität dokumentiert; `GEMINI.md` und der
Herstellerpfad `~/.gemini/antigravity-cli/` bleiben erhalten.

Feature 024 bleibt als ursprünglicher Auditlauf abgeschlossen. Eine spätere
Verbraucherprüfung gegen `TVDEMOS/` und `TVFM/` hat die Zukunftsentscheidung als
Revision 2 mit 13 Findings superseded. Borland und `tv203s/` bleiben primär;
Free Vision bleibt die gepinnte sekundäre Gegenprüfung. Feature 025 schließt
seine neun Core-Findings durch reale Red-/Green-Pfade. Feature 026 schließt die
vier Component-/Data-Findings mit hierarchischer Validierung, sicheren Datei-
entscheidungen und allowlist-basierter UI-Persistenz. Feature 028 schließt alle
13 Findings durch sieben reale Integrations-Slices und bewertet 13 geschützte
Consumer-Gruppen: zwölfmal `UseExistingFramework`, einmal
`FollowUpHardening`. Der TV203-/Free-Vision-Gate ist
`ReadyForTerminalGuiAudit`. Feature 029 prüft danach alle 48 Verträge gegen 25
gepinnt-hashgebundene Terminal.GUI-v1.9.0-Quellen und übergibt 48 explizite
Beobachtungen. Feature 030 ergänzt 50 gepinnte magiblot/tvision-Quellen,
entscheidet alle 48 neuen `MB*`-Beobachtungen und dedupliziert 48 `TGO*` plus
48 `MB*` ohne kanonisches Finding. Daher entsteht kein Hardening-Lauf;
Feature 031 ist als unabhängiger gemeinsamer Konformitätsabschluss vollständig
geliefert. Der kausale Evidence-Closeout setzt Wave 5 auf `Eligible`, startet
die Wave aber nicht. Wave 6 bleibt `ConditionallyReady`, bis Wave 5 vollständig
geliefert und ihr tatsächlicher Delta erneut geprüft ist.

Feature 032 liefert die funktionale erste Wave-5-Stufe als zehn startbare
`Tp7*`-Beispiele. Die vollständige Evidence umfasst 15 Quellenrollen, sechs
Consumer-Entscheidungen, zehn primäre Proof-Zeilen und zehn konkrete
Showcase-Deltas. Feature 033 schließt diese Deltas mit zehn sichtbaren
Hauptkomponenten, echten Statuszeilen, per Tastatur erreichbaren
Beschreibungen, begrenzten Layout-Proofs und synchronisierten Guides.
Lastenheft 19 reserviert Feature 034 als read-only Combined-Delta-Closure über
die exakten Produktdateien der PRs #93 und #96. Wave 6 bleibt bis zum belegten
Abschluss blockiert.

Feature 035 liefert die funktionale erste Wave-6-Stufe aus dem archivierten
Lastenheft 20 als modernen, kontrollierten `Tp7FileManager` über PR #101 und
Merge `52f77fa`. Feature 036 schließt das belegte Stage-2-Delta über PR #104,
Head `a0d5062` und Merge `559bffb`:
persistentes Hauptfenster, sechs Menügruppen, fokussierbare sichere Dialoge,
StatusLine, F1-Description, vollständige Tastaturpfade, begrenzte
Mouse-Intent-Parität und `48x16`-Proof verwenden die unveränderten
Feature-035-Dateisystemverträge. Die exakte Evidence umfasst zehn
Showcase-Bereiche, einen Einstiegspunkt und 24 hashgeprüfte read-only
TVFM-Quellen. Der unabhängige Wave-6-Abschluss, Post-Wave-6-Audit und beliebige
Benutzerdaten bleiben getrennt; Feature 037 wurde nicht gestartet.

## Zweck und Pflege

Diese Datei ist das fortlaufende Statistik-Register fuer TuiVision. Sie wird
nach jeder abgeschlossenen Spec-Kit-Implementierungsphase, nach jeder
agentischen Aenderung am Repository und auf explizite Anforderung
fortgeschrieben.

## Methodik

- Quellen: Git-Historie, lokale Branches, aktueller Arbeitsbaum.
- Ausgeschlossen: `tv203s/`, `_site/`, `bin/`, `obj/` sowie sonstige generierte
  Artefakte.
- Produktionscode: `src/**/*.cs` und `examples/**/*.cs`
- Testcode: `tests/**/*.cs`
- Dokumentation: projektgepflegte Markdown-Dateien einschließlich Repository-
  Wurzel, `docs/`, `specs/`, `TVDocs/`, `.github/`, `.specify/`,
  `.agents/skills/` und Agent-Dateien; generierte und ausgeschlossene Pfade
  bleiben unberücksichtigt.
- Leitsatz fuer diese Datei und die Lernmaterialien:
  `Programmierung #include<everyone>`. Inhalte muessen fuer Braille-Zeile,
  Screenreader und Textbrowser lesbar bleiben; die ASCII-Diagramme sind
  deshalb bewusst textuell und nicht farbgetragen aufgebaut.
- Fuer erzeugte HTML-Dokumentation gilt WCAG 2.2 Konformitaetsstufe AA als
  konkrete Pruefbasis; besonders wichtig sind Seitensprache, Bypass-Mechanismen,
  Tastaturfokus, Non-Text-Contrast und semantische Landmarken.
- Git-Phasenwerte sind Aenderungsvolumen aus Commit-Historie
  (`added/deleted/net`). Snapshot-Werte beschreiben den aktuellen Dateistand.
- Die konservative Handarbeits-Basis in dieser Datei zaehlt Produktionscode,
  Testcode und Dokumentation gemeinsam als manuell zu erstellenden Umfang.
- Die konservative Handarbeits-Basis folgt dem Beitrag
  [Adapt or Disappear: How AI Turned a 2-Year Project into a 1-Week Sprint](https://www.holgerscode.com/blog/2026/02/23/adapt-or-disappear-how-ai-turned-a-2-year-project-into-a-1-week-sprint/#a-note-on-the-orm-29000-lines-you-never-have-to-write):
  maximal 80 manuell erstellte Zeilen pro Arbeitstag fuer einen erfahrenen
  Entwickler.
- Umrechnung in Zeitraeume:
  durchschnittlich 21.5 Arbeitstage pro Monat (Mittel aus 21-22 Arbeitstagen);
  unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr bis einschliesslich 2026 und
  31 Urlaubstagen pro Jahr ab 2027 (jeweils 5-Tage-Woche) ergeben sich
  `21.5 * 12 - 30 = 228` produktive Arbeitstage pro Jahr fuer Zeitraeume bis
  2026 bzw. `21.5 * 12 - 31 = 227` produktive Arbeitstage pro Jahr ab 2027.
- TVoeD-Stundenbasis in dieser Datei:
  `7.8 Stunden` bzw. `7 Stunden 48 Minuten` pro Arbeitstag fuer zusaetzliche
  Stundenumrechnungen.
- Abgeleitete Formeln in dieser Datei:
  Einzelentwickler `((Produktionscode + Testcode + Dokumentation) / 80)`;
  3er-Team `Einzelentwickler / 3 * 1.2` mit 20 % Koordinationsaufschlag.
- Zusatzannahmen fuer die erfahrungsadjustierte Thorsten-Referenz:
  - Allgemeiner Expertenaufschlag `* 1.25`, weil Thorsten seit Februar 1985
    mehr als 40 Jahre Softwareentwicklungspraxis einbringt und seit 2001 mit
    .NET/C# arbeitet.
  - Legacy-Portierungsaufschlag `* 1.25`, weil fuer Pascal-/Turbo-
    Vision-nahe Altcode-Portierungen zusaetzlich 10 bis 15 Jahre praktische
    Turbo-Pascal-/Turbo-Vision-Erfahrung vorliegen.
  - Daraus ergibt sich fuer TuiVision eine erfahrungsadjustierte Solo-Referenz
    von `80 * 1.25 * 1.25 = 125` manuell erstellten Zeilen pro Arbeitstag.
- Beschleunigungsfaktoren vergleichen Referenz-Arbeitstage mit sichtbaren
  `Git-Aktivtagen`. Sie sind bewusst als repo-weiter Output-zu-Aktivtag-
  Indikator formuliert und keine exakte Stoppuhrmessung.

## Erfahrungsprofil und Beschleunigungsmodell

- Referenzprofil fuer die erfahrungsadjustierte Zweitrechnung:
  - mehr als 40 Jahre Softwareentwicklung seit Februar 1985
  - langjaehrige .NET-/C#-Praxis seit 2001
  - 10 bis 15 Jahre Turbo-Pascal-/Turbo-Vision-Erfahrung
- Dadurch wird neben der konservativen 80-Zeilen-Referenz eine zweite,
  explizit benannte Thorsten-Solo-Referenz mit `125 Zeilen/Arbeitstag`
  ausgewiesen.
- Die ausgewiesenen Beschleunigungsfaktoren beantworten nicht die Frage
  "Wie viele Stunden wurden wirklich gearbeitet?", sondern die Frage
  "Wie stark verdichtet war der sichtbare Lieferumfang gegenueber einer
  klassischen, manuell dominierten Portierung?".

## Gesamtstand des Repositories

| Kennzahl | Wert |
|---|---:|
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-07-12 |
| Git-Commits gesamt | 432 |
| Autoren laut Git | 1 |
| Sichtbare Aktivtage inkl. aktuellem Working Tree | 69 |
| Produktionscode aktuell | 177 Dateien / 24127 Zeilen |
| Testcode aktuell | 113 Dateien / 16312 Zeilen |
| Dokumentation aktuell | 560 Dateien / 186597 Zeilen |
| Davon Spec-Kit-Artefakte | 318 Dateien / 45859 Zeilen |
| Davon zentrale Governance-/Agent-Dateien | 5 Dateien / 2535 Zeilen |
| Davon projektgebundene Agent-Skills | 15 Dateien / 2415 Zeilen |
| Gesamtbasis für Handschätzung (inkl. Dokumentation) | 227036 Zeilen |
| Erfahrener Entwickler, konservative Untergrenze | 2838.0 Arbeitstage |
| Erfahrener Entwickler, konservative Untergrenze in Stunden | 22136.0 Stunden (227036 / 80 * 7.8) |
| Erfahrener Entwickler, brutto | 132.0 Arbeitsmonate (21.5 Tage/Monat) |
| Erfahrener Entwickler, TVoeD-Annahme | 149.4 Kalendermonate bzw. 12.4 Jahre |
| Thorsten solo, erfahrungsadjustierte Untergrenze | 1816.3 Arbeitstage |
| Thorsten solo, erfahrungsadjustierte Untergrenze in Stunden | 14167.0 Stunden (227036 / 125 * 7.8) |
| Thorsten solo, brutto | 84.5 Arbeitsmonate (21.5 Tage/Monat) |
| Thorsten solo, TVoeD-Annahme | 95.6 Kalendermonate bzw. 8.0 Jahre |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 1135.2 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), TVoeD-Annahme | 59.8 Kalendermonate |
| Repo-weiter Beschleunigungsfaktor vs. konservative Referenz | 41.1x (2838.0 / 69 sichtbare Aktivtage) |
| Repo-weiter Beschleunigungsfaktor vs. Thorsten-Referenz | 26.3x (1816.3 / 69 sichtbare Aktivtage) |

## Phasen und Haupt-Branches

### 0. Projektbasis auf `main` (Bootstrap, Governance, Kernmodule)

- Status: abgeschlossen und in `main` integriert
- Beobachtbarer Zeitraum: 2026-02-08 bis 2026-03-08
- Commit-Bild: 20 Commits an 4 Git-Aktivtagen
- Grundlegende Arbeiten: Loesungs-Setup, erste Kernmodule, Tests, CI, Agent-
  Dateien, Constitution, Spec-Kit-Grundlagen
- Git-Aenderungsvolumen:
  - Produktionscode: +2346 / -41 / netto +2305
  - Testcode: +534 / -16 / netto +518
  - Dokumentation: +7528 / -140 / netto +7388
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 10211 Zeilen netto gesamt
  - 127.6 Arbeitstage fuer einen erfahrenen Entwickler
  - 5.9 Arbeitsmonate brutto bzw. 6.7 TVoeD-Kalendermonate
  - 51.1 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    2.7 TVoeD-Kalendermonaten

### 1. `001-view-system-tgroup`

- Status: abgeschlossen und in `main` integriert
- Beobachtbarer Zeitraum: 2026-03-16 bis 2026-03-20
- Commit-Bild: 36 Commits an 4 Git-Aktivtagen
- Grundlegende Arbeiten: `TGroup`, Fokus- und State-Verhalten, Draw-Integration,
  `TConsoleBuffer`/`TConsoleCell` in `TuiVision.Core`, didaktische Specs,
  Checklisten und TDD-Testpaket
- Git-Aenderungsvolumen:
  - Produktionscode: +1174 / -382 / netto +792
  - Testcode: +1384 / -2 / netto +1382
  - Dokumentation: +2398 / -219 / netto +2179
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 4353 Zeilen netto gesamt
  - 54.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 2.5 Arbeitsmonate brutto bzw. 2.9 TVoeD-Kalendermonate
  - 21.8 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    1.1 TVoeD-Kalendermonaten

### 2. `002-application-framework`

- Status: abgeschlossen und in `main` integriert
- Beobachtbarer Zeitraum: 2026-03-20 bis 2026-03-21
- Commit-Bild: 34 Commits an 2 Git-Aktivtagen
- Grundlegende Arbeiten: `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`,
  `TStatusLine`, Shell-Command-IDs, Shell-Tests, API-/Plan-/Quickstart-Artefakte
- Git-Aenderungsvolumen:
  - Produktionscode: +622 / -34 / netto +588
  - Testcode: +486 / -4 / netto +482
  - Dokumentation: +1303 / -99 / netto +1204
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 2274 Zeilen netto gesamt
  - 28.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 1.3 Arbeitsmonate brutto bzw. 1.5 TVoeD-Kalendermonate
  - 11.4 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    0.6 TVoeD-Kalendermonaten

### 3. `003-dialog-control-layer`

- Status: in Umsetzung auf Branch `003-dialog-control-layer`
- Beobachtbarer Zeitraum: 2026-03-21 bis 2026-03-21
- Commit-Bild: 18 Commits an 1 Git-Aktivtag fuer Planung und Aufgabenschnitt
- Grundlegende Arbeiten:
  - committed: Spezifikation, Research, Datenmodell, API-Vertrag, Checklisten und
    Task-Plan fuer 13 Controls/Dialogklassen
  - working tree: `TButton`, `TCheckBoxes`, `TCluster`, `TDialog`,
    `TInputLine`, `TLabel`, `TListBox`, `TListViewer`, `TRadioButtons`,
    `TScrollBar`, `TScroller`, `TStaticText`, `TStringList` sowie zugehoerige
    Control-Tests und Hilfsklassen
- Git-Aenderungsvolumen aus Commits:
  - Produktionscode: +0 / -0 / netto +0
  - Testcode: +0 / -0 / netto +0
  - Dokumentation: +4045 / -204 / netto +3841
- Aktueller Working-Tree-Zuwachs:
  - Produktionscode: +2025 / -0 / netto +2025
  - Testcode: +1626 / -127 / netto +1499
  - Dokumentation: +88 / -40 / netto +48
- Konservative Handarbeits-Basis fuer den aktuellen Implementierungsstand
  inklusive Dokumentation:
  - 3572 Zeilen netto gesamt
  - 44.7 Arbeitstage fuer einen erfahrenen Entwickler
  - 2.1 Arbeitsmonate brutto bzw. 2.3 TVoeD-Kalendermonate
  - 17.9 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    0.9 TVoeD-Kalendermonaten

### 4. `004-editor-file-help-streams`

- Status: Implementierung im Working Tree abgeschlossen; Editor-, Datei-, Hilfe-,
  Stream- und Ressourcenkomponenten stehen inklusive Validierung auf Branch
  `004-editor-file-help-streams`
- Beobachtbarer Zeitraum: 2026-03-21 bis 2026-03-22
- Commit-Bild: 0 Commits an 0 Git-Aktivtagen; aktueller Stand liegt vollstaendig
  im Working Tree
- Grundlegende Arbeiten:
  - Setup- und Foundation-Arbeiten: neues Testprojekt
    `TuiVision.Serialization.Tests`, gesplittete Archiv-/Registry-Klassen,
    Kompatibilitaets-Streams und nicht-modaler Host-Frame
  - User Story 1: `TEditor`, `TMemo`, `TIndicator`, `TEditWindow` samt
    Shell-Routing, Safe-Close, Undo, Suche/Ersetzen und Clipboard-Flows
  - User Story 2: `THistory`, `TFileInputLine`, `TFileList`, `TDirListBox`,
    `TFileEditor` und `TFileDialog` mit Datei-Metadaten-Synchronisation,
    Filterung, manueller Pfadeingabe und Konfliktbehandlung
  - User Story 3: `THelpTopic`, `THelpIndex`, `THelpFile`, `THelpViewer` und
    `THelpWindow` fuer kontextbasierte Hilfe mit Querverweisen und Fallback
  - User Story 4: `pstream`, `ipstream`, `opstream`, `fpstream`,
    `TResourceCollection` und `TResourceFile` fuer Shared References,
    Fehlersignale und case-sensitive Resource-Keys
  - Abschlussvalidierung: `dotnet build --configuration Release`,
    `dotnet test tests/TuiVision.Core.Tests/`,
    `dotnet test tests/TuiVision.Controls.Tests/`,
    `dotnet test tests/TuiVision.Serialization.Tests/`, `dotnet test`,
    `dotnet test --collect:"XPlat Code Coverage"`,
    `dotnet format --verify-no-changes` und `docfx docfx.json`
- Snapshot-Zuwachs gegen den letzten Statistikstand:
  - Produktionscode: +3167 / -0 / netto +3167
  - Testcode: +1425 / -0 / netto +1425
  - Dokumentation: +155 / -0 / netto +155
- Konservative Handarbeits-Basis fuer diesen Implementierungsschritt inklusive
  Dokumentation:
  - 4747 Zeilen netto gesamt
  - 59.3 Arbeitstage fuer einen erfahrenen Entwickler
  - 2.8 Arbeitsmonate brutto bzw. 3.1 TVoeD-Kalendermonate
  - 23.7 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    1.2 TVoeD-Kalendermonaten

### 5. `005-driver-consolidation-m07`

- Status: Phase-7-Implementierung im Working Tree abgeschlossen; alle 30 Treiber-
  und Nachweis-Tests bestehen, Formatierung geprueft, `docs/porting-status.md`
  vollstaendig
- Beobachtbarer Zeitraum: 2026-03-23 bis 2026-03-23
- Commit-Bild: 5 Commits an 1 Git-Aktivtag fuer Spezifikation, Planung und
  Aufgabenschnitt; Implementierung liegt vollstaendig im Working Tree
- Grundlegende Arbeiten:
  - Phase 1 – Setup: `TConsoleDriverBaselineTests.cs` (4 Baseline-Tests),
    `Phase7DriverTestContext.cs` (gemeinsame Testhelfer)
  - Phase 2 – Foundational: `PortingStatusLedgerTests.cs` (6 Schema-Tests),
    `TConsoleDriverConsolidationTests.cs` (8 Capability-Bucket-Tests)
  - Phase 3 – US1: `TConsoleDriverCompatibilityTests.cs` (7 Managed-Only-Tests),
    `DriverCapabilityMap.cs` mit 5 Capability-Buckets,
    Multi-Mac-Kompatibilitaetsnachweis in `docs/guides/multi-mac-workflow.md`
  - Phase 4 – US2: `PortingStatusCompletenessTests.cs` (4 Vollstaendigkeitstests),
    `docs/porting-status.md` vollstaendig mit allen 151 historischen `.cc`-Dateien
  - Phase 5 – US3: `checklists/phase-8-gate-review.md`, `quickstart.md`
    Abschlussvalidierung, `Pflichtenheft.md`-Marker, Agent-Dateien synchronisiert
  - Phase 6 – Polish: `dotnet build --configuration Release` fehlerfrei,
    alle 192 Tests (davon 30 neue Treibertests) bestehen,
    `dotnet format --verify-no-changes` fehlerfrei
- Snapshot-Zuwachs gegen den letzten Statistikstand:
  - Produktionscode: +1 Datei / +137 Zeilen netto (DriverCapabilityMap.cs)
  - Testcode: +6 Dateien / +839 Zeilen netto (6 neue Klassen, Test1.cs bereinigt)
  - Dokumentation: +51 Dateien / +8683 Zeilen netto (porting-status.md,
    Checkliste, Spec-Kit-Artefakte, Pflichtenheft-Erweiterungen, Agent-Dateien)
- Konservative Handarbeits-Basis fuer diesen Implementierungsschritt inklusive
  Dokumentation:
  - 9659 Zeilen netto gesamt
  - 120.7 Arbeitstage fuer einen erfahrenen Entwickler
  - 5.6 Arbeitsmonate brutto bzw. 6.4 TVoeD-Kalendermonate
  - 48.3 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    3.1 TVoeD-Kalendermonaten

### 6. `006-close-phase8-gate`

- Status: Eingangstor fuer Phase 8 im Closure-Paket geschlossen; Welle 1 der
  MUSS-Beispiele ist nach dem dedizierten Abschluss-Commit der naechste offene
  Hauptschritt
- Beobachtbarer Zeitraum: 2026-03-24 bis 2026-03-27
- Commit-Bild: Planungsstand bereits im Repository sichtbar; der finale
  Nachweis wird durch den dedizierten Closure-Commit
  `docs: close phase-8 entrance gate for feature 006` markiert
- Grundlegende Arbeiten:
  - Abschluss des restlichen `M-07`-Proofs mit finalem `docs/porting-status.md`
    ohne provisorische `.cc`-Zeilen
  - neues Coverage-/Compatibility-Hardening in `TuiVision.Core`,
    `TuiVision.Compatibility` und `TuiVision.Drivers.Console` samt
    Gate-spezifischen MSTests und Coverlet-Infrastruktur
  - repo-weite Eingangstor-Evidenz fuer `dotnet build --configuration Release`,
    `dotnet test`, `dotnet format --verify-no-changes`, `docfx docfx.json`
    und 5 assembly-scharfe Coverage-Laeufe
  - synchronisierte Gate-Artefakte in `Pflichtenheft.md`,
    `docs/porting-status.md`, `docs/guides/multi-mac-workflow.md`,
    `specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`,
    `specs/006-close-phase8-gate/quickstart.md`,
    `specs/006-close-phase8-gate/contracts/phase-8-gate-contract.md` und
    `specs/006-close-phase8-gate/tasks.md`
- Snapshot-Zuwachs gegen den letzten Statistikstand:
  - Produktionscode: +1578 Zeilen netto
  - Testcode: +2353 Zeilen netto
  - Dokumentation: +1865 Zeilen netto
- Konservative Handarbeits-Basis fuer diesen Abschlussstand inklusive
  Dokumentation:
  - 5796 Zeilen netto gesamt
  - 72.5 Arbeitstage fuer einen erfahrenen Entwickler
  - 565.1 Stunden (72.5 * 7.8)
  - 3.4 Arbeitsmonate brutto bzw. 3.8 TVoeD-Kalendermonate
  - 46.4 Arbeitstage bzw. 361.7 Stunden in der Thorsten-Solo-Referenz
  - 29.0 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    1.5 TVoeD-Kalendermonaten

### 7. Branch `007-spec-kit-versioning`

- Status: in Arbeit auf Feature-Branch `007-spec-kit-versioning`
- Beobachtbarer Zeitraum: 2026-03-27 bis 2026-03-27
- Commit-Bild: aktueller Working-Tree-Aenderungssatz vor dem ersten Branch-Commit
- Grundlegende Arbeiten: repo-weite Versionslogik ueber `Directory.Build.props`
  eingefuehrt und die gemeinsame Agent-/Constitution-Governance auf
  nummerierte Spec-Kit-Branches mit `Minor = Feature-/Branch-Nummer als
  kanonische PR-Nummer` synchronisiert
- Git-/Arbeitsbaum-Aenderungsvolumen fuer den aktuellen Aenderungssatz:
  - Produktionscode: 0 Zeilen
  - Testcode: 0 Zeilen
  - Dokumentation und Governance: 32 Zeilen netto
  - Build-/Versionsmetadaten: 3 Zeilen in `Directory.Build.props`
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 35 Zeilen netto gesamt
  - 0.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 3.1 Stunden auf TVoeD-Basis (`0.4 * 7.8`)
  - 0.0 Arbeitsmonate brutto bzw. 0.0 TVoeD-Kalendermonate
- Thorsten-Solo-Referenz:
  - 0.3 Arbeitstage
  - 2.3 Stunden auf TVoeD-Basis (`0.3 * 7.8`)
  - 0.0 Arbeitsmonate brutto bzw. 0.0 TVoeD-Kalendermonate
- Blended Repository Speedup gegen sichtbare 1 Git-Aktivtag fuer diesen
  Aenderungssatz:
  - 0.4x gegen die konservative 80-Zeilen-Referenz
  - 0.3x gegen die Thorsten-Solo-Referenz mit 125 Zeilen pro Arbeitstag

### 8. Branch `007-port-wave1-examples`

- Status: Wave 1 vollstaendig portiert (2026-03-28): `desklogo`, `msgcls`,
  `tutorial` (16 Schritte), `videomode`; 41 Smoke-Tests gruen; Release-Build
  und `dotnet format --verify-no-changes` fehlerfrei; 4 interaktive Korrekturen
  (Rendering-Pipeline, Lorem-Ipsum-Menüeintrag, Vollbild-Konsole, macOS-Quit);
  `>>> NAECHSTER SCHRITT <<<` zeigt auf Welle 2
- Beobachtbarer Zeitraum: 2026-03-27 bis 2026-03-28
- Commit-Bild: 15 Commits an 2 Git-Aktivtagen (Planung + Analyse,
  Wave-1-Portierung, Lernmaterial-Nacharbeit, 4 Korrekturs-Commits)
- Grundlegende Arbeiten:
  - Spezifikation, Requirements-Checkliste, Plan, Research, Datenmodell,
    Quickstart und Contract fuer die erste MUSS-Beispielwelle mit `desklogo`,
    `msgcls`, `tutorial` und `videomode`
  - zusaetzliche Plan-Review-Checkliste mit 28 Pruefpunkten und
    Durchfuehrungshinweisen
  - umsetzbare `tasks.md` mit 39 Aufgaben fuer Setup, gemeinsame Smoke-
    Infrastruktur, `desklogo`, `msgcls`, alle 16 `tutorial`-Schritte,
    `videomode` sowie Tracking-, Plattform- und Governance-Follow-through
  - Nachschaerfung der Planungsartefakte fuer Managed-Delivery-Begriff,
    In-Process-Smoke-Seam, Clean-Exit-Definition, Tracking-Baseline,
    Agent-Synchronisationsoberflaechen und Branch-Versionierung
  - finale Analyse-Remediation fuer Guide-Bilingualitaet, MUSS-vs.-Follow-on-
    Abgrenzung, Helper-/Asset-Audits, Small-Terminal-Hinweise, explizite
    Later-Wave-Dependency-Pruefung und konkrete Driver-Zieldateien
- Kumulierter Snapshot-Zuwachs fuer den gesamten Branch (alle 15 Commits):
  - Produktionscode (`src/`): +341 Zeilen netto (Wave-1-Impl.) + +400 Zeilen
    netto (4 Korrekturen) = gesamt `+741` Zeilen netto / 1 neue Datei
  - Beispielcode (`examples/`): `~2 227` Zeilen netto (Wave-1-Impl.) + `+797`
    Zeilen netto (Lernmaterialien) + `+138` Zeilen netto (4 Korrekturen) =
    gesamt `~3 162` Zeilen netto
  - Testcode (`tests/`): `~654` Zeilen netto (41 Smoke-Tests), keine weiteren
    Aenderungen
  - Dokumentation und Governance: `1447` Zeilen netto (Planung/Specs) +
    `~672` Zeilen netto (4 Guides + README) + `+102` Zeilen netto
    (Lernmaterial-Guides) = gesamt `~2 221` Zeilen netto
  - Build-/Versionsmetadaten: Zeilen in `Directory.Build.props` laufend
    fortgeschrieben
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - Gesamt-Netto ca. `6778` Zeilen (Prod + Tests + Doku kumuliert)
  - 84.7 Arbeitstage fuer einen erfahrenen Entwickler
  - 660.7 Stunden auf TVoeD-Basis (`84.7 * 7.8`)
  - 3.9 Arbeitsmonate brutto bzw. 4.5 TVoeD-Kalendermonate
- Thorsten-Solo-Referenz:
  - 54.2 Arbeitstage
  - 422.8 Stunden auf TVoeD-Basis (`54.2 * 7.8`)
  - 2.5 Arbeitsmonate brutto bzw. 2.9 TVoeD-Kalendermonate
- Blended Repository Speedup gegen sichtbare 2 Git-Aktivtage fuer diesen Branch:
  - 42.3x gegen die konservative 80-Zeilen-Referenz
  - 27.1x gegen die Thorsten-Solo-Referenz mit 125 Zeilen pro Arbeitstag

### 9. Branch `008-controls-revision`

- Status: Implementierung im Working Tree abgeschlossen; Controls-Revision (Menues, Statuszeile, Fenster, Dialog) als Voraussetzung fuer Welle 2 nachgewiesen; 338 Tests gruen, Release-Build und Format-Gate sauber
- Beobachtbarer Zeitraum: 2026-03-29 bis 2026-03-29
- Commit-Bild: 4 vorbereitende Commits (Planung, Spezifikation, Tasks, Analyse-Remediationen); Implementierung liegt vollstaendig im Working Tree
- Grundlegende Arbeiten:
  - Drei neue Deklarationstypen: `TSubMenu.cs`, `TStatusDef.cs`, `WindowFlags.cs`
  - `TMenuItem.cs`: IsSeparator, Mnemonic, SubMenuDef, Separator-Factory-Methode
  - `TMenuBar.cs`: vollstaendige Tastaturnavigation (Wrap-around, Skip-over, Layout-Slots, Auswahl-Highlight, Resize-Relayout per `OnBoundsChanged`)
  - `TStatusLine.cs`: TStatusDef-basiertes First-Match-Wins-Routing, neutraler Fallback, Kompatibilitaets-Bruecke fuer `GetStatusHints()`
  - `TView.cs`: explizite `HelpContext`-Property fuer Shell-lesbaren Hilfekontext
  - `TWindow.cs`: `WindowFlags` (Close, Move), Schliess-Affordanz (×), Ctrl+W, bewachtes Escape, reversibler Verschiebe-Modus per Ctrl+F5
  - `TDialog.cs`: `Valid(ushort command)`-Validierungshook vor Dialog-Schliessung
  - Neues Testpaket `TWindowTests.cs` mit 8 Testmethoden (317 Zeilen); Erweiterungen in 4 bestehenden Testdateien (je 40-135 neue Zeilen)
  - Proof-Updates: 8 Zeilen in `docs/porting-status.md`, `>>> NAECHSTER SCHRITT <<<` in `Pflichtenheft.md`
- Snapshot-Zuwachs:
  - Produktionscode (`src/TuiVision.Controls/`): ca. `+800` Zeilen netto (3 neue Dateien + 7 modifizierte)
  - Testcode (`tests/TuiVision.Controls.Tests/`): ca. `+700` Zeilen netto (1 neue Datei + 4 modifizierte)
  - Dokumentation und Governance: ca. `+50` Zeilen netto
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - Gesamt-Netto ca. `1550` Zeilen
  - 19.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 151.1 Stunden auf TVoeD-Basis (`19.4 * 7.8`)
  - 0.9 Arbeitsmonate brutto bzw. 1.0 TVoeD-Kalendermonat
- Thorsten-Solo-Referenz:
  - 12.4 Arbeitstage
  - 96.7 Stunden auf TVoeD-Basis (`12.4 * 7.8`)
  - 0.6 Arbeitsmonate brutto bzw. 0.7 TVoeD-Kalendermonate
- Coverage-Stand: `TuiVision.Controls` 84.02 % Line Coverage (Tor: 70 % bestanden)
- Blended Repository Speedup gegen sichtbaren 1 Git-Aktivtag fuer diesen Branch:
  - 19.4x gegen die konservative 80-Zeilen-Referenz
  - 12.4x gegen die Thorsten-Solo-Referenz mit 125 Zeilen pro Arbeitstag

## Zusatz-Branches

| Branch | Letzte sichtbare Aktivitaet | Rolle |
|---|---|---|
| `origin/codex/add-project-constitution` | 2026-03-01 | Governance-Bootstrap fuer die erste Constitution |
| `origin/hindermath-patch-1` | 2026-03-01 | CI-Anpassung fuer Branch-Trigger |

## Einordnung der KI-/Spec-Kit-Wirkung

- Die beobachtbare manuelle Gesamtbasis liegt bereits bei 70597 Zeilen
  (Produktionscode + Tests + Dokumentation).
- Selbst mit der fuer klassische Entwicklung guenstigen Obergrenze von
  80 manuell erstellten Zeilen pro Arbeitstag ergibt sich bereits eine
  Untergrenze von 882.5 Entwickler-Arbeitstagen.
- Unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr entspricht das fuer einen
  erfahrenen Entwickler ca. 46.4 Kalendermonaten bzw. 3.9 Arbeitsjahren; fuer
  ein 3er-Team mit 20 % Koordinationsaufschlag ca. 18.6 Kalendermonaten.
- Unter Einbezug von Thorstens Erfahrungsprofil sinkt die klassische
  Solo-Referenz fuer dieses Repository auf ca. 564.8 Arbeitstage bzw.
  29.7 TVoeD-Kalendermonate.
- Gegen die sichtbaren 21 Aktivtage inklusive aktuellem Working Tree ergibt
  sich damit ein repo-weiter Beschleunigungsfaktor von ca. 42.0x gegen die
  konservative Referenz und immer noch ca. 26.9x gegen die
  erfahrungsadjustierte Thorsten-Referenz.
- Die vorliegenden Git-Daten zeigen damit eine deutliche Verdichtung durch
  agentische KI und GitHub Spec-Kit: hoher Dokumentations- und Codeumfang in
  einem kurzen beobachtbaren Aktivfenster.
- Der Nachweis-Anteil (Spec-Kit-Artefakte, Governance, Beweis-Ledger) macht
  einen signifikanten Teil des Dokumentationsbestands aus; die kombinierte
  Code-Basis aus Produktions- und Testcode mit 30992 Zeilen bestaetigt dennoch
  erheblichen realen Entwicklungsumfang.

## Fortschreibungsprotokoll

| Datum | Ausloeser | Eintrag |
|---|---|---|
| 2026-03-27 | TVoeD-Urlaubsregel ab 2027 nachgezogen | Die Statistikmethodik wurde auf die neue Stichtagsregel umgestellt: 30 Urlaubstage pro Jahr gelten nur bis einschliesslich 2026, ab dem Kalenderjahr 2027 werden unter TVoeD-Annahme 31 Urlaubstage bei unveraenderter 5-Tage-Woche verwendet. |
| 2026-03-27 | Branch `007-spec-kit-versioning` | Repo-weite Versionslogik fuer nummerierte Spec-Kit-Branches eingefuehrt: `Directory.Build.props` traegt jetzt `Version`, `AssemblyVersion` und `FileVersion`; die gemeinsame Agent-Governance und die Constitution wurden auf `Minor = Spec-Kit-Feature-/Branch-Nummer als kanonische PR-Nummer` synchronisiert. |
| 2026-03-21 | Erstanlage | Basisstatistik fuer `main`, `001-view-system-tgroup`, `002-application-framework` und den aktuellen Stand von `003-dialog-control-layer` erzeugt; Constitution, Templates und Agent-Dateien auf Pflegepflicht synchronisiert. |
| 2026-03-21 | Branch `004-editor-file-help-streams` | Dokumentationsstand nach neuer Phase-6-Spezifikation, Requirements-Checklist und synchronisierten Agent-Dateien fortgeschrieben; kein Code- oder Testzuwachs in diesem Arbeitsschritt. |
| 2026-03-21 | `/speckit-plan` fuer `004-editor-file-help-streams` | Planungsstand mit `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/public-api.md` und synchronisierten Agent-Dateien fortgeschrieben; weiterhin kein Produktions- oder Testcodezuwachs in diesem Arbeitsschritt. |
| 2026-03-21 | `/speckit-checklist` fuer `004-editor-file-help-streams` | Zusaetzliche Plan-Review-Checkliste `checklists/planning.md` mit Durchfuehrungshinweisen aufgenommen und den Dokumentationsstand fuer Branch `004-editor-file-help-streams` erneut fortgeschrieben. |
| 2026-03-21 | Review der Plan-Checkliste | Planungsartefakte anhand von `checklists/planning.md` nachgeschaerft, Safe-Close- und Coverage-Klarstellungen eingepflegt und anschliessend alle 30 Review-Punkte in der Checkliste abgehakt. |
| 2026-03-21 | Nachpruefung `/speckit-plan` | Keine weitere fachliche Plananpassung erforderlich; versehentlich ueberschriebener Working-Tree-Plan wiederhergestellt und gemeinsame Agent-Dateien auf den Post-Review-Planstand synchronisiert. |
| 2026-03-21 | `/speckit-tasks` fuer `004-editor-file-help-streams` | Umsetzbare `tasks.md` mit 43 Aufgaben aus Plan, Datenmodell, Vertrag, Research und Quickstart erstellt; Branch damit vom Planungs- in den Ausfuehrungszustand ueberfuehrt. |
| 2026-03-22 | Analyse-Remediation fuer `004-editor-file-help-streams` | Spec-, Plan-, Datenmodell-, Vertrags-, Quickstart- und Task-Artefakte sowie die gemeinsamen Agent-Dateien nach Analysefunden geschaerft: explizite Insert/Overwrite- und Clipboard-Abdeckung, synchronisierte Datei-Metadaten, Shell-Menue-/Status-Routing und das volle Coverage-Gate fuer `TuiVision.Core`, `TuiVision.Controls` und `TuiVision.Serialization`. |
| 2026-03-22 | Zweite Analyse-Remediation fuer `004-editor-file-help-streams` | Die letzten offenen Analysepunkte ausgeraumt: Event-Loop-Verhalten, Fokuswechsel, Menueausfuehrung und explizite Dialoginteraktion nun direkt in `spec.md`, `plan.md`, `quickstart.md`, `tasks.md` und den synchronisierten Agent-Dateien benannt; Story-Tests in der Spezifikation an den nachgeschaerften FR-Stand angeglichen. |
| 2026-03-22 | `/speckit-implement` fuer `004-editor-file-help-streams` | Phase 6 im Working Tree implementiert: neue Editor-, Datei-, Hilfe-, Stream- und Resource-Typen samt Controls-/Serialization-Tests, Coverage-Sweeps, Validierung ueber Build/Test/Format/Coverage/`docfx` und Fortschreibung der Repository-Statistik. |
| 2026-03-22 | Pflege von `Pflichtenheft.md` | Das Pflichtenheft wurde um repo-basierte Statuschecklisten, Reihenfolgehinweise und eine offene/teilweise/erledigt-Markierung erweitert, damit der aktuelle Umsetzungsstand und die verbleibende Abarbeitungsreihenfolge im Dokument direkt sichtbar sind. |
| 2026-03-22 | Erweiterung von Abschnitt 8.3 in `Pflichtenheft.md` | Die vier Beispielwellen wurden in Abschnitt 8.3 von Tabellen auf 25 einzelne Checkbox-Eintraege umgestellt, damit jede Beispielportierung direkt im Pflichtenheft als Arbeitspunkt nachverfolgt und abgehakt werden kann. |
| 2026-03-22 | Methodik-Update fuer Handarbeits-Schaetzung | Die Statistik rechnet Handarbeit jetzt auf Basis von Produktionscode, Testcode und Dokumentation gemeinsam; zusaetzlich werden Monatswerte auf Basis von 21.5 Arbeitstagen pro Monat sowie TVoeD-Kalenderwerte mit 30 Urlaubstagen pro Jahr ausgewiesen. |
| 2026-03-22 | Wiederherstellung von `Pflichtenheft.md` | Die vollstaendige Fassung von `Pflichtenheft.md` wurde aus Commit `4537ea4ecb152e63ba901da15214ffcee79193fe` wiederhergestellt; dadurch stieg der Dokumentationsbestand im aktuellen Repository-Snapshot um netto 562 Zeilen. |
| 2026-03-22 | Quellenangabe fuer `tv203s/` in `Pflichtenheft.md` | Die im Lastenheft genannte SourceForge-Herkunft des Verzeichnisses `tv203s/` wurde in die Ausgangsbasis des Pflichtenhefts uebernommen; der Dokumentationsbestand stieg dadurch um eine weitere Zeile. |
| 2026-03-22 | URL-Quellen fuer Borland-Turbo-Vision-Dokumente | Die Tier-1-Quellen im Pflichtenheft wurden um konkrete Web-URLs ergaenzt: das User's Guide mit direkter Archive.org-Quelle und das oeffentlich verifizierbare begleitende Referenz-/Programmiermaterial ebenfalls mit Archive.org-Verweis. |
| 2026-03-22 | Praezisierung der Borland-Quellenlage im `Pflichtenheft.md` | Die Formulierung zu den historischen Borland-Dokumenten wurde geschaerft: direkte URL nur fuer das verifizierte User's Guide, eigenstaendige Nennung des zusaetzlich verifizierten Programming Guide und ausdruecklicher Hinweis, dass fuer das separat benannte Reference Guide derzeit keine eigenstaendig verifizierte Einzel-URL hinterlegt ist. |
| 2026-03-22 | Lokale PDF-Verweise unter `TVDocs/` | Die Tier-1-Quellen im Pflichtenheft verweisen jetzt per Markdown direkt auf die lokal abgelegten PDF-Dateien unter `TVDocs/`; fuer das separat benannte Reference Guide bleibt der Hinweis auf die derzeit fehlende eigenstaendige Einzeldatei bestehen. |
| 2026-03-22 | OCR-Textablage fuer `TVDocs/` | Aus den beiden lokal abgelegten Turbo-Vision-PDFs wurde per macOS-Vision-OCR Volltext in `TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.txt` und `TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.txt` extrahiert, um spaetere Dokumentationsschritte lokal durchsuchbar zu machen. |
| 2026-03-22 | Markdown-Aufbereitung der OCR-Texte in `TVDocs/` | Die beiden OCR-Textdateien wurden zusaetzlich als lokale Markdown-Arbeitsfassungen mit Seitenueberschriften unter `TVDocs/*.md` erzeugt; die Statistikzaehlung selbst erfasst diese Hilfsdateien bewusst nicht, da die Methodik nur definierte Markdown-Orte zaehlt. |
| 2026-03-22 | Markdown-Arbeitsfassungen um Snippet-Sektion erweitert | Die beiden `TVDocs/*.md`-Dateien wurden fuer spaetere Dokuarbeit nachgeschaerft: Metadatenlinks zur PDF- und TXT-Quelle, ein eigener Abschnitt mit automatisch erkannten Code-/Deklarationsschnipseln und ein davon getrennter roher OCR-Hauptteil mit Seitenueberschriften. |
| 2026-03-22 | TP7-Beispiele als Anschlusswellen im `Pflichtenheft.md` | Abschnitt 8.3 wurde um zwei weitere Portierungswellen fuer die neu aufgenommenen Turbo-Pascal-Beispiele aus `TVDEMOS/` und `TVFM/` erweitert; dabei bleibt klar festgehalten, dass der urspruengliche MUSS-Umfang der 25 C/C++-Originalbeispiele zuerst vollstaendig zu erledigen ist. |
| 2026-03-22 | M-10 und Beispielprogramm-Stellen nachgeschaerft | `M-10`, Abschnitt 8.3, die Dokumentationspflichten und die Abschlusskriterien wurden sprachlich auf den neuen Stand gebracht: 25 Originalbeispiele bleiben MUSS-Umfang, waehrend `TVDEMOS/` und `TVFM/` als getrennte TP7-Anschlusswellen gefuehrt werden. |
| 2026-03-22 | Datei-Kommentarregel fuer agentische Dateien praezisiert | Abschnitt 10.5 Nr. 1 wurde durch eine pruefbare Formulierung ersetzt: 1 bis 3 bilinguale Zeilen mit Verantwortung, Kontext und ggf. Abgrenzung; die Pflicht gilt ausdruecklich auch fuer von KI-Agenten sowie in Spec-/SDD-Workflows erzeugte fachlich gepflegte Quelldateien, ausgenommen nur rein technische, vollautomatisch regenerierbare Artefakte. |
| 2026-03-22 | Datei-/Modulkommentare in `src/` nachgezogen | Alle fachlich gepflegten `.cs`-Dateien unter `src/` wurden gegen Abschnitt 10.5 Nr. 1 geprueft; in 10 Dateien ohne Kopfkommentar wurden kurze bilinguale Datei-/Modulkommentare zu Verantwortung und Kontext nachgetragen, anschliessend per Skript mit `ALL_OK` verifiziert. |
| 2026-03-22 | Multi-Agenten-Workflow in Abschnitt 10.6 erweitert | Die Workflow-Anforderungen nennen jetzt nicht mehr nur `gh` und `codex`, sondern konsistent auch `claude`, `copilot` und `gemini` in Lieferumfang, M-21, Test-/Dokumentationskapiteln sowie den Abschlusskriterien. |
| 2026-03-22 | OCR- und Markdown-Hilfsfassungen als Tier-1-Quellen genannt | Im Abschnitt „Quellenrangfolge und Adaptionspflicht“ verweisen die Borland-Originalquellen jetzt zusaetzlich auf die lokalen `TVDocs/*.txt`- und `TVDocs/*.md`-Fassungen, damit Recherche und agentische Dokumentationserstellung direkt auf die aufbereiteten Hilfsdateien zugreifen koennen. |
| 2026-03-23 | Konsistenzreview von `Pflichtenheft.md` | Vier Review-Punkte direkt eingearbeitet: Statusdatum auf den aktuellen Repository-Stand 2026-03-23 angehoben, die Werkzeugbasis zwischen Ist-Analyse und Multi-Agenten-Workflow sprachlich entkoppelt, OCR-/Markdown-Hilfsfassungen aus der Tier-1-Originalquellenliste in eigene nicht normative Arbeitsmittel ueberfuehrt und die formale MUSS-Abnahme von `M-01` bis `M-22` geschlossen. |
| 2026-03-23 | Werkzeugbasis und Spec-Kit-Pflicht geschaerft | Die Werkzeugbasis im Pflichtenheft wurde auf verbindliche Pflichtinstallation fuer `gh`, `codex`, `claude`, `copilot` und `gemini` auf beiden Macs umgestellt; zusaetzlich ist GitHub Spec-Kit nun ausdruecklich als in allen vier KI-Agenten verpflichtend installierter und dokumentierter Workflow-Bestandteil in Ist-Analyse, Lieferumfang, M-21, Abschnitt 10.6, Testkonzept und Abnahmekriterien verankert. |
| 2026-03-23 | Priorisierte Restarbeiten in `Pflichtenheft.md` ergaenzt | Eine kurze, verbindliche Restarbeiten-Liste wurde direkt im Pflichtenheft aufgenommen und an die Abschlusslogik gekoppelt: zuerst `M-07` plus Eingangstor, dann Treiberkonsolidierung, anschliessend die vier MUSS-Beispielwellen, MUSS-Tests, Pflichtdokumentation, Multi-Mac-/Spec-Kit-/Pages-Nachweise und erst danach die TP7-Anschlusswellen. |
| 2026-03-23 | Free-Vision-Verweise aus `Pflichtenheft.md` entfernt | Der optionale Vergleichspfad ueber Free Vision wurde aus dem Pflichtenheft gestrichen, damit die Quellen- und Ausbildungslogik klar bei Borland-Originaldokumentation, lokalen Hilfsfassungen, `tv203s`, dem C#-Quellcode und den portierten Beispielprogrammen bleibt; die Nummerierung der Quellenrangfolge und der optionalen Anforderungen wurde entsprechend bereinigt. |
| 2026-03-23 | Constitution und Spec-Kit-Templates nachgezogen | Die Constitution wurde auf Version 1.6.0 angehoben: verbindliche Toolbasis jetzt `gh`, `codex`, `claude`, `copilot`, `gemini` plus GitHub Spec-Kit in allen vier KI-Agenten; direkte Pushes auf `main` fuer eng begrenzte, user-genehmigte Repo-Aenderungen realitaetsnah geregelt; die 25 Originalbeispiele wurden als verbindlicher MUSS-Umfang vor `TVDEMOS/`/`TVFM/` auf Governance-Ebene festgeschrieben. Die betroffenen Spec-Kit-Templates (`plan-template.md`, `spec-template.md`, `tasks-template.md`) wurden auf diese Regeln synchronisiert. |
| 2026-03-23 | Optionale Zusatzwerkzeuge in Pflichtenheft und Constitution ergaenzt | `glab` sowie die optionalen KI-Agenten `opencode` und JetBrains `junie` wurden in `Pflichtenheft.md` und der Constitution als dokumentierbare Zusatzwerkzeuge aufgenommen. Fuer `opencode` und `junie` wurde festgelegt, dass GitHub Spec-Kit bei lokaler Nutzung ebenfalls installiert, versionsgeprueft und im Workflow nachweisbar dokumentiert sein muss; die Constitution wurde dafuer auf Version 1.7.0 fortgeschrieben. |
| 2026-03-23 | Constitution-Review und Template-Nachschaerfung | Der `speckit-constitution`-Review hat eine verbleibende Inkonsistenz gefunden: `tasks-template.md` sprach in Story-Abschnitten noch von optionalen Tests trotz verfassungsseitig verpflichtendem Test-First-Ansatz. Das wurde bereinigt; zusaetzlich nennt `plan-template.md` optionale Zusatzwerkzeuge (`glab`, `opencode`, `junie`) jetzt explizit. Die Constitution wurde als Patch-Fortschreibung auf Version 1.7.1 aktualisiert und der Sync-Impact-Report entsprechend korrigiert. |
| 2026-03-23 | Pflichtenheft-Marker fuer den naechsten Schritt eingefuehrt | Im `Pflichtenheft.md` wurde ein auffaelliger Textmarker `>>> NAECHSTER SCHRITT <<<` auf den aktuell hoechstprioren offenen Restarbeitsschritt gesetzt. Zusaetzlich wurden `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` synchronisiert, damit dieser Marker kuenftig bei Fortschritt bewusst weitergeschoben und gemeinsam gepflegt wird. |
| 2026-03-23 | Restarbeiten-Formulierung zu Phase 8 praezisiert | Der priorisierte Restarbeitspunkt im `Pflichtenheft.md` wurde sprachlich geschaerft: Statt der kuerzeren Form `M-07 und Eingangstor Phase 8 schliessen` steht dort jetzt explizit, dass `M-07` abzuschliessen und das Eingangstor fuer Phase 8 als Beginn der Beispielportierungen nachweisbar zu schliessen ist. |
| 2026-03-23 | Restarbeiten-Reihenfolge auf technische Abarbeitung korrigiert | Die priorisierte Restarbeiten-Liste in `Pflichtenheft.md` wurde auf die fachlich stimmige Reihenfolge umgestellt: zuerst Phase 7 Treiberkonsolidierung, danach `M-07` plus Eingangstor fuer Phase 8 und erst anschliessend die MUSS-Beispielwellen 1 bis 4. Der Marker `>>> NAECHSTER SCHRITT <<<` wurde entsprechend auf Phase 7 verschoben. |
| 2026-03-23 | Beispielwellen als Unterphasen 3.1 bis 3.4 verdeutlicht | Der Restarbeiten-Punkt zu den MUSS-Beispielwellen wurde didaktisch nachgeschaerft: Welle 1 bis 4 sind jetzt ausdruecklich als vier eigenstaendige Unterphasen `3.1` bis `3.4` beschrieben, die nacheinander geplant, portiert, getestet und dokumentiert werden. Das soll fuer Auszubildende klarer machen, dass nicht alle 25 Beispiele in einem Schritt bearbeitet werden. |
| 2026-03-23 | Multi-Mac als Hauptworkflow und Linux/Windows/WSL als Kompatibilitaetsumgebungen verankert | `Pflichtenheft.md`, die Constitution, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` wurden so geschaerft, dass der Multi-Mac-Workflow auf `MacBook Air M2` und `Mac mini M4 Pro` der primaere Entwicklungs- und Testpfad ist, waehrend Linux und Windows als zusaetzliche Kompatibilitaets- und Validierungsumgebungen gelten; fuer Windows wurde WSL mit aktuellem Ubuntu, derzeit bevorzugt `Ubuntu 24.04`, hervorgehoben. Zusaetzlich wurde festgehalten, dass solche Kompatibilitaetschecks nach Moeglichkeit auch in GitHub Actions oder aequivalenten Nachweisen sichtbar sein sollen. |
| 2026-03-23 | `/speckit-specify` fuer `005-driver-consolidation-m07` | Die Spezifikation fuer den naechsten Pflichtenheft-Schritt wurde angelegt: Phase 7 Treiberkonsolidierung, `M-07`-relevante Treiberreste und das fehlende Nachweisartefakt `docs/porting-status.md` sind jetzt als eigenstaendiges Spec-Kit-Feature mit User Stories, funktionalen Anforderungen, Annahmen, Erfolgskennzahlen und validierter Requirements-Checkliste beschrieben. |
| 2026-03-23 | `/speckit-clarify` fuer `005-driver-consolidation-m07` | Zwei hochwirksame Klaerungen wurden direkt in die Spezifikation integriert: `docs/porting-status.md` verlangt bei gesplitteter Verantwortung je historischer Datei ein Pflicht-Primaarziel mit optionalen Sekundaerzielen, und Linux/Windows/WSL-Kompatibilitaetspruefungen muessen fuer Phase 7 nachweisbar erfolgen, duerfen aber noch manuell oder halbautomatisch statt als hartes CI-Gate belegt werden. |
| 2026-03-23 | `/speckit-plan` fuer `005-driver-consolidation-m07` | Die Planungsartefakte fuer Phase 7 wurden angelegt: `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und ein eigener Contract fuer Treiber- und Nachweisoberflaeche beschreiben jetzt die managed Treiberkonsolidierung, den formalen `.cc`-Scope von `M-07`, das Schema von `docs/porting-status.md`, die Rolle von Linux/Windows/WSL-Kompatibilitaetsnachweisen und die klare Trennung zwischen abgeschlossener Phase 7 und spaeterem Phase-8-Eingangstor. |
| 2026-03-23 | Phase-7-Plan um zugehoerige Header- und Support-Dateien geschaerft | Die Planungsartefakte fuer `005-driver-consolidation-m07` wurden nachgeschaerft: Historische `.h`- und `.c`-Dateien bleiben zwar ausserhalb der formalen `M-07`-Zeilenmenge, muessen aber als relevanter Abhaengigkeits- und Begruendungskontext bei den betroffenen `.cc`-Eintraegen in `docs/porting-status.md` sichtbar beruecksichtigt werden. |
| 2026-03-23 | `/speckit-checklist` fuer `005-driver-consolidation-m07` | Eine Plan-Review-Checkliste mit Review-Schwerpunkt wurde fuer `plan.md` und alle zugehoerigen Plan-Artefakte angelegt. Sie prueft insbesondere die Klarheit des Phase-7-Scope, die Konsistenz des `M-07`-Nachweisformats, die explizite Behandlung zugehoeriger `.h`-/`.c`-Dateien sowie die Readiness des gesamten Planpakets vor `/speckit-tasks`. |
| 2026-03-23 | Plan-Review-Checkliste fuer `005-driver-consolidation-m07` durchgearbeitet und abgeschlossen | Die neue `checklists/planning.md` wurde direkt gegen `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und den Contract geprueft. Dabei wurden noch fehlende Regeln fuer ueberlappende Capability-Buckets, gemeinsam genutzte Support-Dateien, Zero-State-Proof-Zeilen und die explizite Szenarioklassen-Zuordnung im Plan nachgeschaerft; anschliessend konnte die gesamte Checkliste mit 40 Punkten abgeschlossen werden. |
| 2026-03-23 | `/speckit-tasks` fuer `005-driver-consolidation-m07` | Umsetzbare `tasks.md` mit 29 Aufgaben aus `spec.md`, `plan.md`, `research.md`, `data-model.md`, `contracts/phase-7-proof-contract.md` und `quickstart.md` erstellt; die Aufgaben trennen Phase-7-Treiberkonsolidierung, den formalen `M-07`-Nachweis in `docs/porting-status.md` und die Vorbereitung des spaeteren Phase-8-Eingangstors. |
| 2026-03-23 | Analyse-Remediation fuer `005-driver-consolidation-m07` | Die Top-4-Funde aus `/speckit-analyze` wurden direkt eingearbeitet: Branch-Governance auf den real genutzten Spec-Kit-Workflow erweitert, die Phase-7-Planung von der unklaren `Class1.cs`-Catch-all-Formulierung weg auf purpose-named Support-Dateien geschaerft, die Pflicht-Kompatibilitaetsnachweise um explizite Evidence fuer `MacBook Air M2` und `Mac mini M4 Pro` erweitert und die TDD-Commit-Sequenz als sichtbar zu erhaltende Red-Green-Refactor-Regel in den Aufgaben verankert. |
| 2026-03-23 | `/speckit-implement` fuer `005-driver-consolidation-m07` | Phase 7 im Working Tree vollstaendig implementiert: `DriverCapabilityMap.cs` mit 5 Capability-Buckets, 6 neue Testklassen (30 Tests gesamt), `docs/porting-status.md` mit allen 151 historischen `.cc`-Dateien, Phase-8-Gate-Checkliste, Multi-Mac-Kompatibilitaetsnachweis, `Pflichtenheft.md`-Naechster-Schritt auf Phase-8-Eingangstor vorgeschoben; `dotnet build`, alle 192 Tests und `dotnet format --verify-no-changes` fehlerfrei. |
| 2026-03-24 | Multi-Mac-Workflow um Spec-Kit-Checks und Toolpflege erweitert | `docs/guides/multi-mac-workflow.md` wurde auf den agentenuebergreifenden Branch-Workflow, den Root-`docfx.json`-Pfad, `specify` als Pflichtwerkzeug, `specify check` fuer die Toolpruefung und die `uv`-Befehle fuer Installation bzw. Release-Upgrades der GitHub-Spec-Kit-CLI erweitert. Die gemeinsamen Agent-Dateien `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` wurden dazu auf denselben Multi-Mac-/Spec-Kit-Stand synchronisiert. |
| 2026-03-24 | `/speckit-specify` fuer `006-close-phase8-gate` | Neue Spezifikation und Requirements-Checklist fuer den finalen `M-07`-Abschluss und das nachweisbare Phase-8-Eingangstor angelegt; gemeinsame Agent-Dateien auf den neuen aktiven Feature-Kontext synchronisiert und die Statistik fuer diesen Branch fortgeschrieben. |
| 2026-03-24 | Coverage-Gate-Synchronisierung fuer `006-close-phase8-gate` | Die damals geklaerte harte Coverage-Regel wurde repo-weit auf den damaligen Drei-Modul-Stand (`TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization` jeweils `>= 70 %`) vereinheitlicht; dazu wurden `Pflichtenheft.md`, `docs/porting-status.md`, die Phase-8-Review-Checkliste sowie die gemeinsamen Agent-Dateien synchronisiert. Dieser Stand wurde am 2026-03-25 auf fuenf Module erweitert. |
| 2026-03-25 | `/speckit-plan` fuer `006-close-phase8-gate` | Planartefakte `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und `contracts/phase-8-gate-contract.md` fuer den finalen `M-07`- und Phase-8-Gate-Abschluss angelegt; der Plan fixiert den repository-weiten `dotnet test`-Lauf, die bedingte Linux/Windows/WSL-Evidenz und den dedizierten Gate-Closure-Commit als verbindlichen Arbeitsstand. Die Coverage-Huerde wurde am selben Tag anschliessend von drei auf fuenf Module erweitert. |
| 2026-03-25 | Coverage-Gate-Erweiterung fuer `006-close-phase8-gate` | Die Phase-8-Coverage-Regel wurde von drei auf fuenf Module erweitert: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` muessen nun jeweils `>= 70 %` Line Coverage erreichen; dafuer wurden Spezifikation, Planartefakte, `Pflichtenheft.md`, `docs/porting-status.md`, die Phase-8-Review-Unterlagen und die gemeinsamen Agent-Dateien nachgezogen. |
| 2026-03-25 | Plan-Refresh nach Coverage-Clarify fuer `006-close-phase8-gate` | Nach den letzten Spec-Klarstellungen wurde der 006-Plan auf assembly-scharfe Coverage-Nachweise fuer alle fuenf Gate-Module und auf das Verbot von Platzhalter-/No-op-Modulen als Schein-Nachweis nachgeschaerft; dazu wurden Planartefakte und die gemeinsamen Agent-Dateien synchronisiert. |
| 2026-03-25 | Gate-Dokumentations-Review fuer `006-close-phase8-gate` | Die neue Gate-Checkliste wurde gegen den 006-Dokumentensatz abgeglichen; dabei wurden Gate-Scope-Entfernungen im selben Artefaktpaket, Begriffschaerfungen fuer Platzhalter-/No-op-/triviale Tests sowie die Behandlung lokaler-vs.-CI-Coverage-Konflikte in Spezifikation, Plan, Datenmodell, Quickstart, Vertrag und Agent-Guidance nachgeschaerft. |
| 2026-03-25 | `/speckit-tasks` fuer `006-close-phase8-gate` | Eine umsetzbare `tasks.md` mit 34 Aufgaben aus `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und `contracts/phase-8-gate-contract.md` erstellt; die Aufgaben schneiden den Restaufwand in die drei 006-User-Stories, verankern die TDD-Reihenfolge, ein eigenes `TuiVision.Compatibility`-Testprojekt, die assembly-scharfen 5x-70-%-Coverage-Nachweise und den dedizierten Gate-Closure-Commit. |
| 2026-03-25 | Erfahrungsadjustierte Beschleunigungsrechnung erweitert | Die Statistik wurde um eine zweite Thorsten-Solo-Referenz erweitert: >40 Jahre Softwareentwicklung seit Februar 1985, .NET-/C#-Praxis seit 2001 und 10-15 Jahre Turbo-Pascal-/Turbo-Vision-Erfahrung werden nun als explizite Annahmen fuer die repo-weite Beschleunigungsrechnung gegenueber klassischer Portierung ausgewiesen; dieselbe Logik wurde in die gemeinsamen Agent-Dateien synchronisiert. |
| 2026-03-25 | TVoeD-Stundenbasis ergänzt | Die Statistik weist zusaetzlich Stundenwerte auf Basis von `7,8 Stunden` bzw. `7 Stunden 48 Minuten` pro Arbeitstag aus; dieselbe Umrechnungsregel wurde in die gemeinsamen Agent-Dateien aufgenommen. |
| 2026-03-25 | Analyse-Remediation fuer `006-close-phase8-gate` | Nach der Querpruefung von `tasks.md` gegen `spec.md` und `plan.md` wurden die letzten Dokumentdriften beseitigt: `gate-docs.md` ist jetzt expliziter Bestandteil des Plan-Artefaktsets, `tests/TuiVision.Compatibility.Tests/` ist als feste Compatibility-Fallback-Suite in Plan und Quickstart verankert, und Skip-/Ignore-Faelle duerfen das Gate nur noch mit dokumentierter Tracking-Issue-Referenz passieren. Die gemeinsamen Agent-Dateien wurden im selben Arbeitsschritt synchronisiert. |
| 2026-03-27 | Sortierung des Fortschreibungsprotokolls vereinheitlicht | Die Eintraege im Fortschreibungsprotokoll wurden auf strikt chronologische Reihenfolge gebracht: aeltester Eintrag oben, juengster und zuletzt eingetragener Eintrag unten. Dieselbe Regel wurde in der gemeinsamen Agent-Governance fuer dieses Repository festgeschrieben. |
| 2026-03-27 | `/speckit-implement` fuer `006-close-phase8-gate` | Das Phase-8-Eingangstor wurde im Closure-Paket abgeschlossen: restliche `M-07`-Proof-Luecken geschlossen, Gate-Artefakte synchronisiert, repo-weite Build-/Test-/Format-/`docfx`-Nachweise dokumentiert, 5 assembly-scharfe Coverlet-Ergebnisse (`89.11 %`, `84.10 %`, `83.33 %`, `80.95 %`, `97.43 %`) verankert und der dedizierte Closure-Commit fuer Feature `006` gesetzt. |
| 2026-03-27 | Branch `007-port-wave1-examples` | Spezifikations-, Planungs- und Review-Paket fuer die erste MUSS-Beispielwelle angelegt und nach der Plan-Checkliste nachgeschaerft: `spec.md`, `requirements.md`, `plan.md`, `research.md`, `data-model.md`, `contracts/wave-1-example-contract.md`, `quickstart.md` und `checklists/planning.md` definieren jetzt Scope, Smoke-Seams, Guides, Tracking-Baseline und Synchronisationspflichten fuer `desklogo`, `msgcls`, `tutorial` und `videomode`. |
| 2026-03-27 | `/speckit-tasks` fuer `007-port-wave1-examples` | Die Aufgabenzerlegung fuer die erste MUSS-Beispielwelle wurde mit 37 Tasks aus `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und `contracts/wave-1-example-contract.md` erzeugt. Die neue `tasks.md` schneidet Setup, gemeinsame Smoke-Infrastruktur, die vier Wave-1-Beispiele, Plattformnachweise sowie Tracking- und Governance-Follow-through in einen ausfuehrbaren, story-basierten Ablauf. |
| 2026-03-28 | Analyse-Remediation fuer `007-port-wave1-examples` | `plan.md` und `tasks.md` wurden nach mehreren Konsistenzlaeufen final bereinigt: Guide-Seiten sind jetzt explizit bilingual (`Deutsch zuerst`, `Englisch danach`, CEFR-B2), Wave-1-MUSS-Abgrenzung und Helper-/Asset-Entscheidungen sind sichtbar verankert, Small-Terminal- und Later-Wave-Abhaengigkeitspruefungen sind als Aufgaben enthalten, `T029` ist auf konkrete Driver-Dateien zugeschnitten und der Pfad `docs/guides/multi-mac-workflow.md` wurde im Plan korrigiert. |
| 2026-03-28 | Lastenheft-Branch-Suffix-Regel in Agent-Guidance verankert | Die gemeinsamen Agent-Dateien (`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`) wurden um die Governance-Regel erweitert, dass ein Lastenheft nach Umsetzung durch einen dedizierten Feature-Branch auf `Lastenheft_<Thema>.<feature-branch>.md` umzubenennen ist, damit die Rueckverfolgbarkeit im Repository erhalten bleibt; Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: 0 Produktionscode-Zeilen, 0 Testcode-Zeilen, +5 Dokumentationszeilen netto im Arbeitsbaum, konservative Handarbeits-Untergrenze 0.1 Arbeitstage bzw. 0.5 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.0 Arbeitstage bzw. 0.3 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat. |
| 2026-03-28 | Agenten-Governance-Sync auf Branch `007-port-wave1-examples` | Die gemeinsame Agent-Guidance wurde gegen `main` nachgezogen: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` enthalten jetzt wieder konsistent die Lastenheft-Traceability-Regel zur Umbenennung nach `Lastenheft_<Thema>.<feature-branch>.md`; `.github/agents/copilot-instructions.md` war bereits auf diesem Stand. |
| 2026-03-28 | Analyse-Follow-up fuer `007-port-wave1-examples` | Die Analysefunde aus dem read-only Cross-Artifact-Check wurden in den 007-Artefakten nachgezogen: `spec.md` grenzt den Small-Terminal-Edge-Case jetzt auf die relevanten Beispiele ein, `plan.md`, `tasks.md` und `quickstart.md` operationalisieren die reviewbare Red-Green-Refactor-Commitfolge, die 5-Minuten-Guide-Walkthrough-Pflicht und den Dokumentations-Scope fuer `examples/`, `tests/TuiVision.Examples.SmokeTests/` und beruehrte `src/`-Dateien. |
| 2026-03-28 | Constitution-Patch fuer TDD-Commitdisziplin | Die Constitution wurde von `1.10.0` auf `1.10.1` per PATCH-Schaerfung angehoben: Der bislang widerspruechliche Zusammenhang zwischen sichtbarer Red-Green-Refactor-Historie und der Pflicht auf gruene Commits ist jetzt eng geklaert. Bewusst rote TDD-Commits sind nur noch als schmale Feature-Branch-Ausnahme vor Green/Handover/Merge zulaessig; Templates und gemeinsame Agent-Dateien wurden auf Aenderungsbedarf geprueft, brauchten aber keine Textanpassung. |
| 2026-03-28 | `/speckit-implement` fuer `007-port-wave1-examples` | Welle 1 der MUSS-Beispiele wurde vollstaendig portiert: 4 Beispiele (`desklogo`, `msgcls`, `tutorial` mit 16 Schritten, `videomode`), 35 Produktionsdateien (`~2 227` Zeilen), 5 Smoke-Test-Klassen mit 41 Tests (`~654` Zeilen), 4 bilinguale Guides plus `examples/README.md` (`~672` Zeilen), gesamt `~3 553` neue Zeilen. Release-Build, alle 314 Tests (inkl. Framework-Suites) und `dotnet format --verify-no-changes` fehlerfrei; `>>> NAECHSTER SCHRITT <<<` auf Welle 2 vorgeschoben. Konservative Manualreferenz: 80 Zeilen/Tag = `44,4` Tage (ca. `346` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `28,4` Tage (ca. `222` Stunden); tatsaechliches Arbeitsfenster: 1 Agentensitzung am 2026-03-28. |
| 2026-03-28 | Nacharbeit Wave-1-Lernmaterialien auf Branch `007-port-wave1-examples` | Vier Folgearbeiten in einer Agentensitzung: (1) Voraussetzungen-Abschnitte in allen 4 Wave-1-Guides um .NET-SDK-URL, `git clone`-Snippet, Microsoft-Learn-Link und TuiVision-Event-System-Link erweitert; (2) kollabierbare `#region`/`#endregion`-Lösungsblöcke in alle Wave-1-Beispieldateien eingefügt (Schritte 01–03 bereits in `1035876`, Schritte 04–16 in `0cc7ac2`); (3) fehlende `**Übungen / Exercises**`-Abschnitte fuer Schritte 08–16 in `tutorial.md` ergänzt (`ddcf6a3`); (4) alle Übungsabschnitte (Schritte 01–16) auf konsequente CEFR-B2-Bilingualität korrigiert. Netto-Änderungsvolumen gegenüber dem Welle-1-Implementierungscommit `12c47a2`: Beispiel-CS-Dateien `+797` Zeilen / Guide-MD-Dateien `+102` Zeilen netto, gesamt `+899` Zeilen über 25 geänderte Dateien. Konservative Manualreferenz: 80 Zeilen/Tag = `11,2` Tage (ca. `87` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `7,2` Tage (ca. `56` Stunden); tatsaechliches Arbeitsfenster: 1 Agentensitzung am 2026-03-28. |
| 2026-03-28 | Wave-1-Korrekturen auf Branch `007-port-wave1-examples` | Vier Korrekturen in einer Agentensitzung nach dem Lernmaterial-Commit: (1) `fix(wave1)` – vollstaendige Rendering-Pipeline repariert: `TViewState.Exposed` jetzt propagiert, Child-TGroup-Puffer korrekt in Parent-Puffer kompositiert, `TGroup.GetDrawBuffer()` als `protected internal new`, neue `SystemConsolePresenter.cs`, `TDesktop.Draw()`, `TMenuBar.Draw()`/`HandleEvent()` mit `~X~`-Hotkey-Rendering, `TStatusLine.Draw()` und `TWindow.Draw()` mit Box-Drawing-Rahmen implementiert; (2) `feat(msgcls)` – Menüeintrag `~N~achricht posten / Post ~m~essage` löst `cmPostLoremIpsum` aus, `MsgClsApp.HandleEvent()` sendet `"Lorem Ipsum dolor sit amet."` via `PostMessage()`; (3) `feat(shell)` – alle vier Wave-1-`Program.cs` ermitteln die tatsaechliche Konsolengrösse bei Start, `TProgram.Run()` loescht den Bildschirm, setzt `CursorVisible = false` und ruft nach dem Quit `CleanupConsole()` auf (Prompt sichtbar); (4) `fix(quit)` – `Ctrl+Q` und `Ctrl+C` als universelle Quit-Tasten in `TProgram.GetEvent()` verankert (`Console.TreatControlCAsInput = true`), Statuszeile auf `^Q Beenden / Quit  Alt+X` umgestellt. Hintergrund: In macOS Terminal.app sendet die Option-Taste standardmaessig Unicode-Zeichen statt ESC-Sequenz, `ConsoleModifiers.Alt` wird nie gesetzt. Netto-Aenderungsvolumen: `src/` `+413 / -13` = `+400` Zeilen netto (1 neue Datei `SystemConsolePresenter.cs`, 6 veraenderte Dateien); `examples/` `+152 / -14` = `+138` Zeilen netto (9 veraenderte Dateien); gesamt `+538` Zeilen netto ueber 16 Dateien. Konservative Manualreferenz: 80 Zeilen/Tag = `6.7` Tage (ca. `52` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `4.3` Tage (ca. `34` Stunden); tatsaechliches Arbeitsfenster: 1 Agentensitzung am 2026-03-28. |
| 2026-03-29 | Untermenü-Popup-Fix und Controls-Revision-Lastenheft auf Branch `007-port-wave1-examples` | Zwei Arbeitspakete in einer Agentensitzung: (1) `feat(menubar)` – vollstaendige Untermenü-Popup-Implementierung: `TMenuBar` erhaelt `_openSubMenu`/`_openSubMenuX`-Felder, `HandleEvent()` oeffnet Untermenüs per Hotkey, schliesst per Escape, dispatcht Untermenü-Eintraege; `DrawSubMenuOverlay()` rendert ein Box-Drawing-Popup (`┌─┐ │ │ └─┘`) mit Hotkey-Hervorhebung direkt in den finalen Puffer; `TProgram.Draw()` ruft das Overlay nach `base.Draw()` auf, sodass der Desktop-Puffer es nicht ueberschreibt (Overlay-Pattern nach Desktop-Compositing). (2) `Lastenheft_ControlsRevision.md` – 300-Zeilen-Anforderungsdokument fuer Branch `008-controls-revision` erstellt: vergleichende Analyse von `tmenuvie.cc` (644 Zeilen) vs. `TMenuBar.cs`, `tstatusl.cc` (369 Zeilen) vs. `TStatusLine.cs` (80 Zeilen), `twindow.cc` vs. `TWindow.cs`; 7 priorisierte Anforderungen R-01 bis R-07 mit messbaren Akzeptanzkriterien; Branch `007` in `main` per PR #14 gemergt (13 Commits, admin-merge nach gruenen CI-Gates). Netto-Aenderungsvolumen: `src/` `+205 / -29` = `+176` Zeilen netto (2 veraenderte Dateien); `docs/` `+300` Zeilen netto (1 neue Datei `Lastenheft_ControlsRevision.md`); gesamt `+476` Zeilen netto ueber 3 Dateien. Konservative Manualreferenz: 80 Zeilen/Tag = `6.0` Tage (ca. `46.5` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `3.8` Tage (ca. `29.8` Stunden); tatsaechliches Arbeitsfenster: 1 Agentensitzung am 2026-03-29. |
| 2026-03-29 | Spec-/Plan-Paket und Plan-Review-Gate fuer `008-controls-revision` eingecheckt | Das vollstaendige Spec-Kit-Artefaktpaket fuer `008-controls-revision` wurde im Arbeitsbaum finalisiert: `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/controls-revision-api.md` sowie `checklists/requirements.md` und `checklists/planning.md`. Zusaetzlich wurden die Plan-Nachschaerfungen fuer Legacy-Bridge-Traceability (`TEditor`, `TEditWindow`), konkrete `docs/porting-status.md`-Zeilen inklusive `tmenubox.cc`, die Pflicht zum Mitziehen des `>>> NAECHSTER SCHRITT <<<`-Markers in `Pflichtenheft.md`, die beobachtbare Event-Loop-Performance-Formulierung sowie der Agenten-Sync fuer `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` eingearbeitet. Netto-Aenderungsvolumen vor dieser Ledger-Zeile: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+1 477` Dokumentationszeilen netto (`specs/008-controls-revision/` plus Agent-Guidance), zusaetzlich `+3` Repo-Metadaten-Zeilen in `Directory.Build.props` fuer die Branch-Version `1.8.226.9`. Konservative Manualreferenz: 80 Zeilen/Tag = `18,5` Tage (ca. `144,0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `11,8` Tage (ca. `92,2` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-29, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-29 | `/speckit-tasks` fuer `008-controls-revision` | Aus `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und `contracts/controls-revision-api.md` wurde eine umsetzbare `tasks.md` fuer den Controls-Revision-Branch erzeugt: 31 Tasks insgesamt, davon 6 fuer User Story 1 (Menüshell), 6 fuer User Story 2 (Statuszeilen-Kontexte), 6 fuer User Story 3 (Fenster/Dialoge) und 13 gemeinsame Setup-/Foundation-/Polish-Aufgaben. Die Aufgaben verankern die Red-Green-Refactor-Reihenfolge, die minimalen Compile-Time-Seams (`TSubMenu`, `TStatusDef`, `WindowFlags`, `HelpContext`), die Pflicht-Updates fuer `docs/porting-status.md`, `Pflichtenheft.md`, `docs/project-statistics.md`, die Lastenheft-Umbenennung sowie die finalen Multi-Mac-/Linux-/Windows-Validierungen. Netto-Aenderungsvolumen vor dieser Ledger-Zeile: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+236` Dokumentationszeilen netto in `specs/008-controls-revision/tasks.md`. Konservative Manualreferenz: 80 Zeilen/Tag = `3,0` Tage (ca. `23,0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,9` Tage (ca. `14,7` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-29, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-29 | Analyse-Remediation fuer `008-controls-revision` | Die Top-4-Funde aus dem Cross-Artifact-Check wurden direkt in `spec.md`, `plan.md`, `quickstart.md` und `tasks.md` nachgezogen: (1) Die TDD-Reihenfolge wurde in `tasks.md` repariert, indem Phase 2 jetzt nur noch testseitige Vorbereitungen enthaelt und keine Produktionsdateien unter `src/TuiVision.Controls/` mehr vor den ersten Red-Tests anfasst. (2) Die US3-Inkonsistenz zum Window-Close-Pfad wurde auf den dokumentierten Tastaturpfad (`Ctrl+W` / guarded `Escape`) geschaerft, statt still einen nicht operationalisierten Titelbereich-Trigger mitzuschleppen. (3) `SC-003` wurde in Plan, Quickstart und Tasks um eine reviewbare Repeated-Run-Matrix mit Versuchszahlen und First-Attempt-Pass-Rate operationalisiert. (4) Linux- und Windows/WSL-Portabilitaet verlangen nun nicht nur Laufzeitnotizen, sondern explizite Build-/Test-Nachweise im finalen Validierungsblock. Netto-Aenderungsvolumen vor dieser Ledger-Zeile: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+15 / -14` Dokumentationszeilen netto ueber `specs/008-controls-revision/spec.md`, `specs/008-controls-revision/plan.md`, `specs/008-controls-revision/quickstart.md`, `specs/008-controls-revision/tasks.md` und `docs/project-statistics.md`. Konservative Manualreferenz: 80 Zeilen/Tag = `0,2` Tage (ca. `1,0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,1` Tage (ca. `0,7` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-29, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-29 | Analyse-Remediation Runde 2 fuer `008-controls-revision` | Die verbleibenden Top-3-Funde aus dem erneuten Analyse-Durchlauf wurden direkt eingearbeitet: (1) `plan.md` und `tasks.md` erlauben jetzt explizit eine enge compile-only scaffolding Ausnahme fuer neue Typen/Mitglieder (`TSubMenu`, `TStatusDef`, `WindowFlags`, `HelpContext`), damit statisch typisierte Red-Tests in C# ueberhaupt kompilieren koennen, ohne bereits ausgelieferte Laufzeitlogik vorzuziehen. (2) Die Dokumentationsaufgabe in `tasks.md` wurde von einer Public-API-Bedingung auf die constitution-konforme Pruefung aller beruehrten projekt-eigenen Quelldateien unter `src/TuiVision.Controls/` erweitert. (3) Ein eigener Audit-Task gegen `FR-016` bis `FR-018` wurde hinzugefuegt, damit Streaming-, Mouse- und Palette-Scope nicht nur narrativ, sondern auch reviewbar abgesichert sind. Die Aufgabenliste wurde dabei konsistent von 31 auf 33 Tasks umnummeriert und die Parallel-/Beispielsektionen entsprechend nachgezogen. Netto-Aenderungsvolumen vor dieser Ledger-Zeile: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+20 / -16` Dokumentationszeilen netto ueber `specs/008-controls-revision/plan.md`, `specs/008-controls-revision/tasks.md` und `docs/project-statistics.md`. Konservative Manualreferenz: 80 Zeilen/Tag = `0,3` Tage (ca. `1,6` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,2` Tage (ca. `1,0` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-29, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-29 | Thematische Lastenhefte fuer Welle 2 bis 4 | Vier neue Anforderungsdokumente wurden als bewusst geschnittenes Vorbereitungs- und Roadmap-Paket angelegt: `Lastenheft_01_ControlsWidgetsAndCollections.md` und `Lastenheft_02_StandardDialogsAndDesigner.md` definieren die aus meiner Sicht echten Vor-Welle-2-Voraussetzungen jenseits der laufenden `008-controls-revision`; `Lastenheft_03_EditorHelpAndResourcesHardening.md` und `Lastenheft_04_TerminalCharsetAndEmulation.md` ziehen die spaeteren Wave-3-/Wave-4-Haertungen frueh sichtbar nach, ohne sie als Sofort-Blocker fuer Wave 2 zu deklarieren. Netto-Aenderungsvolumen dieser Runde: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+660` Dokumentationszeilen netto (`659` neue Lastenheft-Zeilen plus Fortschreibung in `docs/project-statistics.md`). Konservative Manualreferenz: 80 Zeilen/Tag = `8,3` Tage (ca. `64,4` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `5,3` Tage (ca. `41,2` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-29, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-29 | `/speckit-implement` fuer `008-controls-revision` | Controls-Revision vollstaendig implementiert: drei neue Deklarationstypen (`TSubMenu.cs`, `TStatusDef.cs`, `WindowFlags.cs`), erweiterte `TMenuItem.cs` (IsSeparator, Mnemonic, SubMenuDef, Separator-Factory), vollstaendige Tastaturnavigation in `TMenuBar.cs` (Wrap-around, Skip-over, Layout-Slots, Auswahl-Highlight, Resize-Relayout), TStatusDef-basiertes First-Match-Wins-Routing mit neutralem Fallback in `TStatusLine.cs`, HelpContext-Property in `TView.cs`, WindowFlags-Flags mit Schliess-Affordanz/Ctrl+W/bewachtem Escape/Ctrl+F5-Verschiebe-Modus in `TWindow.cs`, Valid(ushort)-Validierungshook in `TDialog.cs`. Neues Testpaket `TWindowTests.cs` (317 Zeilen) und Erweiterungen in `TMenuBarTests.cs`, `TStatusLineTests.cs`, `TDialogTests.cs`, `TProgramTests.cs`. `docs/porting-status.md` fuer alle 8 betroffenen `.cc`-Zeilen aktualisiert; `>>> NAECHSTER SCHRITT <<<` in `Pflichtenheft.md` auf Welle-2-Planung vorgeschoben. Validierung: `dotnet build --configuration Release` (0 Fehler, 0 Warnungen), `dotnet test` (338 Tests, 0 Fehler), `dotnet format --verify-no-changes` (gruen). TuiVision.Controls Coverage: 84,02 % Line Coverage (Tor: 70 %). Netto-Aenderungsvolumen: ca. `+800` Produktionscode-Zeilen netto (10 Dateien bearbeitet/neu), ca. `+700` Testcode-Zeilen netto (5 Dateien bearbeitet/neu), ca. `+50` Dokumentationszeilen netto. Konservative Manualreferenz: 80 Zeilen/Tag = `19,4` Tage (ca. `151,1` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `12,4` Tage (ca. `96,7` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-29, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | Hotkey-Korrektur fuer `MsgCls` auf `main` | Rendering-Regression im Menue behoben: `TMenuBar` markiert Hotkeys jetzt positionsgenau statt alle spaeteren Vorkommen desselben Zeichens; `examples/MsgCls/MsgClsApp.cs` wurde auf die bilinguale Mnemonik `~N~achricht posten / ~P~ost message` gezogen, zwei neue Controls-Regressionstests sichern Top-Level- und Untermenue-Rendering, `dotnet test tests/TuiVision.Controls.Tests/ --filter "FullyQualifiedName~TMenuBar"` lief mit 12/12 gruener Testsuite. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `src/` `+45 / -8` = `+37` Zeilen netto, `tests/` `+87 / -0` = `+87` Zeilen netto, `docs/` `+33 / -32` = `+1` Zeile netto in `docs/project-statistics.md`; zusaetzlich `examples/` `+1 / -1` = `0` und `Directory.Build.props` `+3 / -3` = `0` Repo-Metadaten-/Versionspflege. Konservative Manualreferenz fuer die repo-relevanten `125` Netto-Zeilen aus `src/` + `tests` + `docs`: `1,6` Tage (ca. `12,2` Stunden); Thorsten-Solo-Referenz: `1,0` Tage (ca. `7,8` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | Lastenheft-Reihe um Maussupport erweitert und neu nummeriert | Die thematische Lastenheft-Reihe wurde zwischen Wave-3-Haertung und Terminal-/Charset-Vorbereitung um ein eigenes Dokument fuer Laufzeit-Maussupport ergaenzt: neu `Lastenheft_04_MouseSupportAndInteraction.md`; das bisherige Terminal-Dokument wurde auf `Lastenheft_05_TerminalCharsetAndEmulation.md` verschoben. Die angrenzenden Lastenhefte `01`, `02`, `03` und `05` wurden in ihren Scope-Grenzen auf den neuen Block ausgerichtet. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+213` Dokumentationszeilen netto ueber `Lastenheft_01_ControlsWidgetsAndCollections.md`, `Lastenheft_02_StandardDialogsAndDesigner.md`, `Lastenheft_03_EditorHelpAndResourcesHardening.md`, `Lastenheft_04_MouseSupportAndInteraction.md` und `Lastenheft_05_TerminalCharsetAndEmulation.md`. Konservative Manualreferenz: 80 Zeilen/Tag = `2,7` Tage (ca. `20,8` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,7` Tage (ca. `13,3` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | Gesamtstatistik-Block und ASCII-Diagramme als Schlusssektion verankert | `docs/project-statistics.md` endet jetzt verbindlich mit einem finalen Top-Level-Block `## Gesamtstatistik`, in dem eine kompakte Gesamtauswertung und ASCII-only-Verlaufsdiagramme direkt am Dateiende mitgefuehrt werden. Dieselbe Pflegepflicht wurde in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md` und in einer globalen Codex-Merknote fuer die Repositories `TuiVision`, `TinyCalc`, `TinyPl0`, `InventarWorkerService` und `sysinfotool` verankert. Netto-Aenderungsvolumen dieses Arbeitsschritts wird als reine Dokumentations-/Governance-Aenderung im Folge-Refresh der Snapshot-Zaehler mitgefuehrt; die neue Schlusssektion selbst basiert auf den aktuell dokumentierten Snapshot- und Phasenwerten dieses Statistikdokuments. |
| 2026-03-30 | Erklaertexte fuer ASCII-Diagramme nachgezogen | Die ASCII-Diagramme im finalen Block `## Gesamtstatistik` wurden um kurze Erklaertexte in CEFR-B2-Sprache erweitert, damit Auszubildende die dargestellten Messgroessen und die beabsichtigte Aussage schneller verstehen. Dieselbe Pflicht wurde in die gemeinsamen Agent-Dateien und in die globale Codex-Merknote fuer die weiteren Repositories uebernommen. |
| 2026-03-30 | Zusaetzliche ASCII-X/Y-Diagramme fuer Verlaufsdaten | Die Schlusssektion `## Gesamtstatistik` enthaelt jetzt neben Balkenvergleichen auch einfache ASCII-X/Y-Diagramme fuer dokumentiertes Phasenvolumen, konservative Handarbeits-Referenzen und dokumentierte Beschleunigungsfaktoren. Die gemeinsame Guidance und die globale Codex-Merknote erlauben solche X/Y-Darstellungen kuenftig ausdruecklich, wenn Verlaufsdaten damit fuer Lernende besser lesbar werden. |
| 2026-03-30 | Inklusions-Leitsatz und DocFX-Textzugang verankert | Der Leitsatz `Programmierung #include<everyone>` wurde fuer dieses Repository explizit auf textuelle Barrierefreiheit erweitert: Guides, Statistik und erzeugte API-Dokumentation muessen fuer Braille-Zeile, Screenreader und Textbrowser nutzbar bleiben. `README.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `docs/project-statistics.md` wurden entsprechend nachgezogen; die globale Codex-Merknote fuer `TuiVision`, `TinyCalc`, `TinyPl0`, `InventarWorkerService` und `sysinfotool` wurde ebenfalls aktualisiert. Als reviewbarer Pruefpfad ist ein lokaler Playwright-Accessibility-Snapshot fuer repraesentative DocFX-Seiten (`/index.html`, `/api/TuiVision.Controls.TView.html`, `/docs/project-statistics.html`) festgehalten; der aktuelle Check zeigt sauber lesbare Ueberschriften, Listen, Tabellen und Artikelbereiche. Validierung: `docfx docfx.json` erfolgreich, danach Accessibility-Snapshot ueber den lokal gestarteten `_site/`-Server. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+280 / -1` Dokumentationszeilen netto ueber die genannten Repo-Dateien; die ausserhalb des Repositories gepflegte Codex-Merknote ist darin nicht mitgezaehlt. Konservative Manualreferenz: 80 Zeilen/Tag = `3,5` Tage (ca. `27,3` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `2,2` Tage (ca. `17,5` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | WCAG-2.2-AA-Baseline und DocFX-Template-Nachschaerfung | Die Barrierefreiheitsanforderung fuer erzeugte HTML-Dokumentation wurde auf WCAG 2.2 Konformitaetsstufe AA geschaerft und in die gemeinsame Agent-Guidance sowie in `README.md` aufgenommen. Technisch wurde `docfx.json` um ein eigenes Accessibility-Template erweitert, `index.md` fuer eine ruhigere Landing-Page ohne doppelte Hauptueberschrift nachgeschaerft und ein kleines DocFX-Override-Paket unter `docfx-templates/accessibility-modern/` eingefuehrt: `lang=\"de\"`, Skip-Link zum Hauptinhalt, klar sichtbare Fokusmarkierung, Landmark-Labels, geringeres Screenreader-Rauschen fuer Deko-Icons und textfreundlichere Layoutregeln. Validierung: `docfx docfx.json` erfolgreich; frischer Playwright-Check mit Cache-Busting bestaetigt auf `/index.html?wcag=1` `lang=\"de\"`, vorhandenes Accessibility-CSS/JS, genau eine `h1` auf der Landing-Page, `#main-content` als Sprungziel und den Skip-Link als ersten Tastaturfokus. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+558 / -4` Zeilen netto ueber Governance-, Konfigurations-, Landing-Page- und DocFX-Template-Dateien im Repository; die neu erzeugten `_site/`- und `api/`-Artefakte sind darin nicht mitgezaehlt. Konservative Manualreferenz: 80 Zeilen/Tag = `6,9` Tage (ca. `54,0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `4,4` Tage (ca. `34,6` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | Playwright-plus-axe-Smoke-Setup fuer DocFX-A11y pruefbar gemacht | Unter `tests/web-a11y/` wurde ein eigenstaendiger Node-basierter Pruefpfad fuer die erzeugte DocFX-Dokumentation eingerichtet: `package.json`, `playwright.config.ts`, eine erste repraesentative Spezifikation fuer Landing-Page, API-Seite und Statistikseite sowie eine kurze Bedienhilfe. Das Setup verbindet den vorhandenen DocFX-Build mit Playwright, `@axe-core/playwright`, einem lokalen `_site/`-Server und optionalen `lynx`-Sichtpruefungen. Die gemeinsame Agent-Guidance und `README.md` nennen den neuen Standardpfad jetzt explizit, damit kuenftige DocFX- oder API-Doku-Aenderungen den A11y-Smoke-Test mitziehen. Validierung: `npm install`, `npx playwright install chromium` und `npm run test:docfx` in `tests/web-a11y/`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `+92 / -0` Test-/Tooling-Zeilen in `tests/web-a11y/`, `+53 / -0` Dokumentations- und Guidance-Zeilen in den begleitenden Repo-Dateien; generierte `package-lock.json`-Metadaten sind darin nicht separat ausgewiesen. Konservative Manualreferenz: 80 Zeilen/Tag = `1,8` Tage (ca. `14,1` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,2` Tage (ca. `9,0` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | DocFX-A11y-Pflicht und Installationsvoraussetzungen nachgezogen | Der neue A11y-Pruefpfad wurde als dauerhafte Betriebsregel verschaerft: Jeder erfolgreiche `docfx`-Neubau muss im selben Arbeitsschritt durch den passenden Playwright-plus-axe-Smoke-Test unter `tests/web-a11y/` begleitet werden. Zusaetzlich wurden `Node 24 LTS`, `npm`, Playwright, `@axe-core/playwright` und `lynx` in den Installationsvoraussetzungen des Repositories sichtbar verankert, vor allem im Multi-Mac-Workflow und in der lokalen A11y-Bedienhilfe. Die gemeinsame Agent-Guidance sowie die globale Codex-Merknote fuer `TuiVision`, `TinyCalc`, `TinyPl0`, `InventarWorkerService` und `sysinfotool` wurden auf dieselbe Regel synchronisiert. Validierung: Regel- und Doku-Update im Repository; kein zusaetzlicher Build notwendig, weil der A11y-Pruefpfad bereits unmittelbar zuvor gruen gegen `npm run test:docfx` verifiziert wurde. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+88 / -0` Dokumentations- und Guidance-Zeilen netto ueber die betroffenen Repo-Dateien; die ausserhalb des Repositories gepflegte Codex-Merknote ist darin nicht mitgezaehlt. Konservative Manualreferenz: 80 Zeilen/Tag = `1,1` Tage (ca. `8,6` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,7` Tage (ca. `5,5` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | lynx-Workflow im Installationspfad praxisbereinigt | Die Installations- und Nutzungstexte fuer den textbasierten `lynx`-Gegencheck wurden noch einmal nachgeschaerft: `README.md`, `docs/guides/multi-mac-workflow.md` und `tests/web-a11y/README.md` beschreiben jetzt sauber, dass fuer den separaten `lynx`-Abruf ein lokaler Server wie `npm run serve:docfx` in einem zweiten Terminal laufen muss. Dadurch ist der textuelle Review-Pfad fuer sehbehinderte Auszubildende nicht nur benannt, sondern auch praktisch reproduzierbar dokumentiert. Validierung: reiner Doku-Nachtrag; der technische A11y-Pruefpfad war unmittelbar zuvor bereits gruen ueber `npm run test:docfx`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+20 / -2` Dokumentationszeilen netto ueber die drei genannten Repo-Dateien. Konservative Manualreferenz: 80 Zeilen/Tag = `0,3` Tage (ca. `1,8` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,2` Tage (ca. `1,2` Stunden); sichtbares Arbeitsfenster: 1 kurze Agenten-Nacharbeit am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | Bilinguale Abschlusspruefung in Pflichtenheft-Abnahme verankert | Nach Review des aktuellen Doku-Standes wurde klar nachgezogen, dass die grossen Anforderungsdokumente in `TuiVision` noch nicht durchgaengig in einem einzigen Inline-Format gehalten werden muessen. Deshalb nennt `Pflichtenheft.md` jetzt explizit zwei Abschlusspruefpunkte: erstens muessen lernrelevante Dokumente sowie aktive `Pflichtenheft`-/`Lastenheft`-Artefakte fuer Auszubildende in Deutsch und Englisch auf CEFR-B2-Niveau vorliegen, wobei grosse normative Dokumente als synchron gepflegte `.EN.md`-Parallelfassung ausgeliefert werden duerfen; zweitens ist die A11Y-Pruefung nach `Programmierung #include<everyone>` mit WCAG-2.2-AA-Baseline, DocFX-Folgereview und textorientierter Nutzbarkeit Teil der formalen Abnahme. Die gemeinsame Agent-Guidance (`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, beide Copilot-Instructions) wurde dazu synchron nachgezogen; dieselbe Leitentscheidung wurde fuer die weiteren Repositories in ihren verbindlichen Anforderungs- oder Planungsdokumenten vermerkt. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+24 / -1` Dokumentationszeilen netto in `Pflichtenheft.md`, den gemeinsamen Guidance-Dateien und dieser Statistikfortschreibung. Konservative Manualreferenz: 80 Zeilen/Tag = `0,3` Tage (ca. `2,3` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,2` Tage (ca. `1,5` Stunden); sichtbares Arbeitsfenster: 1 kurze Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-03-30 | Parent-Guidance bewusst auf repo-uebergreifende Regeln begrenzt | Nach dem Anlegen von `/Users/thorstenhindermann/RiderProjects/AGENTS.md` wurde in den lokalen TuiVision-Guidance-Dateien explizit vermerkt, dass die Parent-Datei nur gemeinsame Basisregeln fuer mehrere Repositories tragen soll. Repository-spezifische Build-, Test-, Workflow-, Architektur- und Feature-Vorgaben bleiben bewusst in `TuiVision` selbst und sind bei Konflikten die spezifischere Autoritaet. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+12 / -0` Dokumentationszeilen netto ueber die lokalen Guidance-Dateien und diese Statistikfortschreibung. Konservative Manualreferenz: 80 Zeilen/Tag = `0,2` Tage (ca. `1,2` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,1` Tage (ca. `0,7` Stunden); sichtbares Arbeitsfenster: 1 kurze Agentensitzung am 2026-03-30. |
| 2026-03-30 | `009-controls-widgets-and-collections` mit `main` synchronisiert und gegen 008/Wave 1 revalidiert | Der Branch `009-controls-widgets-and-collections` wurde lokal mit `main` zusammengefuehrt, die kollidierenden generierten Artefakte unter `tests/web-a11y/` wurden vorab bereinigt, und das 009-Planpaket wurde anschliessend gegen den inzwischen gelieferten Stand aus `006-close-phase8-gate`, `007-port-wave1-examples` und `008-controls-revision` nachgeschaerft. Konkret wurden die 009-Artefakte auf die aktuelle Lastenheft-Namensregel mit Punkt-Suffix umgestellt, `ManagedClipboard` und `TParamText` als bereits vorhandene Basis statt als rein neue 009-Dateien eingeordnet, der verpflichtende DocFX-plus-A11y-Folgeschritt im Quickstart und in den Aufgaben verankert und die gemeinsamen Agent-Kontexte via `.specify/scripts/bash/update-agent-context.sh` fuer `codex`, `claude`, `gemini` und `copilot` aktualisiert. Validierung auf dem synchronisierten Stand: `dotnet build --configuration Release` erfolgreich, danach `dotnet test` erfolgreich mit `44` Core-, `191` Controls-, `37` Drivers-, `18` Compatibility-, `13` Serialization- und `41` Example-Smoke-Tests; `Directory.Build.props` steht fuer diesen Arbeitsschritt auf der branchkonformen Version `1.9.182.2`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+287 / -56` Dokumentations- und Guidance-Zeilen netto ueber `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/agents/copilot-instructions.md` sowie das 009-Planpaket; zusaetzlich `Directory.Build.props` `+3 / -3` = `0` Repo-Metadaten-/Versionspflege. Konservative Manualreferenz fuer die dokumentationsrelevanten `231` Netto-Zeilen: `2,9` Tage (ca. `22,5` Stunden); Thorsten-Solo-Referenz: `1,8` Tage (ca. `14,4` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-02 | `/speckit-implement` fuer `010-standard-dialogs-designer` | Standarddialog- und Designer-Readiness frameworkseitig umgesetzt: explizite File-/Directory-Entscheidungen ohne Dateiinhalt-I/O, textorientierte Validierung und Fallbacks, Color-/Display-/symbolische Charset-Auswahl, validierte Dialogbeschreibung, Runtime-Erzeugungsgrenze und minimaler persistierter Dialogbeschreibungs-Roundtrip mit Ablehnung fehlerhafter Eingaben. Neue Nachweise liegen in Controls- und Serialization-Tests sowie in Security-/Supply-Chain-/Multi-Mac-Evidenz. Validierung: `dotnet build --configuration Release` erfolgreich, `dotnet test tests/TuiVision.Controls.Tests/` mit 281 Tests gruen, `dotnet test tests/TuiVision.Serialization.Tests/` mit 18 Tests gruen, `dotnet test` mit allen 439 Tests gruen, `dotnet test --collect:"XPlat Code Coverage"` mit gate-relevanten Cobertura-Dateien gruen (`Core` 89,11 %, `Controls` 82,85 %, `Serialization` 87,00 %, `Compatibility` 80,95 %, `Drivers.Console` 76,76 %), `dotnet format --verify-no-changes` nach Umstellung auf LF und Entfernung von UTF-8-BOM aus MSTest-Settings gruen, `docfx docfx.json` erfolgreich und `npm run test:docfx` mit 2/2 Playwright/Axe-Smoke-Tests gruen. Sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-05-02, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-03 | Analyse-Follow-up fuer `010-standard-dialogs-designer` | Die drei vor dem Commit priorisierten Cross-Check-Funde wurden geschlossen: ein positiver Runtime-Factory-Test fuer gueltige Dialogbeschreibungen, ein Adapter-Test fuer semantisch ungueltige persistierte Dialogbeschreibungen und die Fortschreibung des finalen Gesamtstatistikblocks inklusive 010-Balken- und X/Y-Diagrammen. Validierung: `dotnet format --verify-no-changes`, `dotnet build --configuration Release`, `dotnet test` mit 441/441 Tests, `docfx docfx.json` und `npm run test:docfx` mit 2/2 Playwright/Axe-Smoke-Tests gruen. Sichtbares Arbeitsfenster: 1 kurze Agenten-Nacharbeit am 2026-05-03, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-08 | `/speckit-implement` fuer `011-port-wave2-examples` | Welle 2 der MUSS-Beispiele wurde portiert: `clipboard`, `demo`, `dlgdsn`, `dyntxt`, `inplis`, `listvi`, `progba`, `sdlg`, `sdlg2`, `tcombo` und `tprogb`; zusaetzlich wurde `TScrollGroup` als minimale Controls-Basis fuer scrollbare Dialoge eingefuehrt. Validierung: `dotnet build --configuration Release` (0 Fehler, 0 Warnungen), `dotnet test tests/TuiVision.Examples.SmokeTests/` mit 58/58 Tests und allen 15 gelieferten Beispielen, `dotnet test` mit 463/463 Tests, `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings` mit gate-relevanten Cobertura-Werten (`Core` 89,74 %, `Controls` 83,82 %, `Serialization` 86,66 %, `Compatibility` 80,95 %, `Drivers.Console` 76,28 %), `dotnet format --verify-no-changes`, `docfx docfx.json`, `npm run test:docfx` mit 2/2 Playwright/Axe-Smoke-Tests und `git diff --check` gruen. Netto-Aenderungsvolumen ohne generierte DocFX-Ausgabe: `+1646` Produktions-/Beispielzeilen, `+710` Testzeilen, `+947` Dokumentations-/Nachweiszeilen; zusaetzlich `+5753` netto in generierten `api/`- und `_site/`-Artefakten sowie `+165` Loesungs-/Versionsmetadaten. Konservative Manualreferenz fuer die `3303` manuell zu pruefenden Netto-Zeilen: `41,3` Tage (ca. `322,0` Stunden); Thorsten-Solo-Referenz: `26,4` Tage (ca. `206,1` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-05-08, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | Lastenheft fuer interaktive Wave-2-Demos | Nach dem Merge von `011-port-wave2-examples` wurde `Lastenheft_Interactive-Wave2-Demos.md` als Eingabedatei fuer den naechsten Spec-Kit-Specify-Lauf angelegt. Das Dokument haelt die Review-Erkenntnis fest, dass die Wave-2-Beispiele zwar smoke-testbare Nachweisobjekte sind, beim normalen CLI-Start aber echte sichtbare Menue-/Tastaturpfade, Status-/Desktop-Rueckmeldungen und geskriptete UI-Event-Smokes benoetigen. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+288` Lastenheft-Dokumentationszeilen. Konservative Manualreferenz: 80 Zeilen/Tag = `3,6` Tage (ca. `28,1` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `2,3` Tage (ca. `18,0` Stunden); sichtbares Arbeitsfenster: kurze Agenten-Nacharbeit am 2026-05-09, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | Generierte DocFX-Ausgabe aus dem Git-Tracking genommen | `_site/` und die generierten Dateien unter `api/` wurden als DocFX-Output in `.gitignore` verankert und mit `git rm -r --cached _site api` nur aus dem Git-Index entfernt. Lokal bleiben die Artefakte vorhanden und regenerierbar; neue `api/index.md`-Handseiten bleiben ausdruecklich trackbar. Netto-Aenderungsvolumen fuer die fachliche Handarbeitsbasis: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen; die 750 vormals getrackten Generated-Output-Dateien sind bewusst kein fachlicher Lieferumfang. Validierung: `git check-ignore` bestaetigt `_site/` und generierte `api/*.yml`, `git ls-files _site api` ist leer. |
| 2026-05-09 | Zweistufiges Beispielwellen-Liefermuster dokumentiert | Die aus `011`/`012` abgeleitete Lieferentscheidung wurde als wiederverwendbares Muster festgehalten: groessere Beispielwellen duerfen zuerst funktionale Portierungs-/Nachweisfeatures mit deterministischen Smoke-Pfaden liefern und sollen danach in einem eigenen interaktiven Showcase-Feature Menues, Statuszeilen, Desktop-Controls, Tastaturpfade und UI-Event-Smoke-Tests nachziehen. Synchronisiert wurden `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, beide Copilot-Instructions und `Lastenheft_Interactive-Wave2-Demos.md`; zusaetzlich wurde eine externe Codex-Merknote angelegt. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+70` Dokumentations-/Guidance-Zeilen netto im Repository. Konservative Manualreferenz: 80 Zeilen/Tag = `0,9` Tage (ca. `6,8` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,6` Tage (ca. `4,4` Stunden); sichtbares Arbeitsfenster: kurze Agenten-Nacharbeit am 2026-05-09, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | Pflichtenheft-Abnahme fuer interaktive Beispielreife nachgezogen | Das zweistufige Beispielwellen-Muster ist nun auch in der formalen Abnahmebasis sichtbar: `M-10`, Abschnitt 8.3, der Welle-2-Status, der `>>> NAECHSTER SCHRITT <<<`-Marker und Abschnitt 12 unterscheiden zwischen funktionalem Portierungs-/Smoke-Nachweis und interaktiver Demo-Reife. `012-interactive-wave2-demos` ist damit als naechster bewusster Follow-up-Lauf fuer normale CLI-Starts mit Menues, Statuszeilen, Dialogen, Tastaturpfaden und UI-Event-Smoke-Tests markiert. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+7` Pflichtenheft-Dokumentationszeilen. Konservative Manualreferenz: 80 Zeilen/Tag = `0,1` Tage (ca. `0,7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0,1` Tage (ca. `0,4` Stunden); sichtbares Arbeitsfenster: kurze Agenten-Nacharbeit am 2026-05-09, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | 011-Review-Cleanup vor 012 | Die offenen Copilot-Review-Punkte aus PR #22 wurden vor dem Start von `012-interactive-wave2-demos` bereinigt: `DlgDsn` lehnt unsichere Fixture-Namen allow-list-basiert ab und meldet malformed-Deserialisierung diagnostisch, `Sdlg`/`Sdlg2` setzen echten `TScrollGroup`-Fokus, die zugehoerigen Smoke-Tests pruefen die neuen Fokus-Offsets und die 10 betroffenen Wave-2-Guides enthalten vollstaendige englische Proof-Bloecke. Validierung: gezielte DlgDsn/Sdlg/Sdlg2-Smoke-Tests `5/5` gruen, komplette Example-Smoke-Suite `59/59` gruen, `dotnet build --configuration Release` gruen, `dotnet test` gruen (`44` Core-, `288` Controls-, `37` Drivers-, `18` Compatibility-, `18` Serialization- und `59` Example-Smoke-Tests). Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `+44` Produktions-/Beispielcode-Zeilen, `+20` Testzeilen, `+109` Guide-Dokumentationszeilen; `Directory.Build.props` wurde nur als Build-Zaehler-Metadatum aktualisiert. Konservative Manualreferenz fuer `173` fachliche Netto-Zeilen: `2,2` Tage (ca. `16,9` Stunden); Thorsten-Solo-Referenz: `1,4` Tage (ca. `10,8` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-05-09, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | Lastenheft als Spec-Kit-Intake fuer 012 aufbereitet | `Lastenheft_Interactive-Wave2-Demos.md` wurde von einem Entwurfs-/Diskussionsdokument zu einer direkten Eingabedatei fuer `/speckit-specify` geschaerft: Intake-Zusammenfassung, PR-#24-Precondition, Beispiel-Matrix mit vorhandenen 011-Funktionen, priorisierte User Stories, zusaetzliche Requirements gegen Schein-Interaktion, erwartete Spec-Kit-Artefakte und ein aktualisierter kopierbarer Specify-Prompt fuer `012-interactive-wave2-demos`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+235` Lastenheft-Dokumentationszeilen. Konservative Manualreferenz: 80 Zeilen/Tag = `2,9` Tage (ca. `22,9` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,9` Tage (ca. `14,7` Stunden); sichtbares Arbeitsfenster: kurze Agenten-Nacharbeit am 2026-05-09, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | GitHub-Pages-Artefaktworkflow fuer DocFX eingerichtet | Die DocFX-Dokumentation wird nun ueber `.github/workflows/pages.yml` in GitHub Actions gebaut, mit dem vorhandenen `tests/web-a11y/` Playwright-plus-axe-Smoke geprueft, als Pages-Artefakt aus `_site/` hochgeladen und ausserhalb von Pull Requests in das `github-pages`-Environment deployed. Der bestehende CI-DocFX-Schritt zeigt jetzt auf root-`docfx.json`; README, A11Y-README und gemeinsame Agent-Guidance halten fest, dass `_site/` und generierte `api/*.yml`-Dateien nicht committet werden. Validierung: `npm install` mit lokalem Node-25-Engine-Hinweis gegen erlaubte Node-20/22/24-Linien, `npx playwright install chromium`, danach `npm run test:docfx` mit DocFX `0` Warnungen/`0` Fehlern und Playwright/Axe `2/2` gruen. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+126` Workflow-/Dokumentations-/Guidance-Zeilen inklusive dieser Statistikfortschreibung. Konservative Manualreferenz: 80 Zeilen/Tag = `1,6` Tage (ca. `12,3` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,0` Tage (ca. `7,9` Stunden); sichtbares Arbeitsfenster: kurze Agenten-Nacharbeit am 2026-05-09, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-09 | PR-#26-Review-Cleanup fuer DocFX-Pages | Die beiden Copilot-Review-Punkte in PR #26 wurden eingearbeitet: `configure-pages` und `upload-pages-artifact` laufen nur noch ausserhalb von Pull Requests, waehrend DocFX-Build und A11Y-Smoke auch im PR weiterlaufen; die README spricht nun praezise von generierten `api/*.yml`-Dateien und laesst handgeschriebene Dateien wie `api/index.md` ausdruecklich unangetastet. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+7` Workflow-/Dokumentationszeilen. |
| 2026-05-09 | `/speckit-specify` fuer `012-interactive-wave2-demos` | Aus `Lastenheft_Interactive-Wave2-Demos.md` wurde die Feature-Spezifikation fuer `012-interactive-wave2-demos` erzeugt und mit einer Requirements-Quality-Checklist validiert. Die Spec fixiert `Demo` als P1-Vertical-Slice, mindestens einen sichtbaren Bedienpfad pro Wave-2-Beispiel, UI-/Eventpfad-Smokes statt reiner Direktmethoden, DE-first/EN-second Guides, A11Y-Pfade, Scope-Grenzen gegen Welle 3/4 und Governance-Evidence fuer Sicherheit/Architektur. Validierung: `specify check`, Spec-Kit-Branch-Skript mit sequenzieller Nummer `012`, Platzhalter-/Clarification-Suche ohne offene Treffer und abgehakte `requirements.md`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+189` Spec-Kit-Dokumentationszeilen inklusive dieser Statistikfortschreibung. |
| 2026-05-09 | `/speckit-clarify` fuer `012-interactive-wave2-demos` | Die 012-Spezifikation wurde vor der Planung ges…22032 tokens truncated…fuehrbare Logik betroffen ist. |
| 2026-07-12 | `/speckit-autonomous` fuer `019-wave3-visual-component-porting` | Die fuenf historischen Wave-3-Beispiele `BHelp`, `HelpDemo`, `I18n`, `TvEdit` und `TvHc` sind als sichtbare Drei-Schichten-Anwendungen umgesetzt: reale Hauptkomponente, echte `TStatusLine` und tastaturerreichbares `Help -> Description`. Primaere Smokes laufen durch `app.Run()` und verbinden Zustand, View-Typ, Buffer-Zellen, Status und Beschreibung; die gemeinsame Matrix besteht lokal 14/14 Tests einschliesslich fuenf stabiler `48x16`-Ansichten. Vier Beispiele nutzen `UseExistingFramework`; BHelp dokumentiert `IntentionalDeviation`, weil kein proprietaerer ungepruefter Borland-`.tch`-Decoder portiert wird. Der finale Vor-Commit-Snapshot umfasst `+918` Produktions-/Beispielcodezeilen, `+369` Testzeilen, `+2056/-30` Dokumentations-/Evidence-/Guidance-Zeilen und `+83/-4` Metadatenzeilen, insgesamt `+3426/-34` beziehungsweise `3392` Nettozeilen bei Branch-Commitzahl `0`. Konservative Manualreferenz fuer 3426 hinzugefuegte oder aktualisierte Zeilen: `42,8` Tage beziehungsweise `334,0` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `27,4` Tage beziehungsweise `213,8` Stunden bei 125 Zeilen/Tag. Sichtbares Arbeitsfenster ist der autonome Lauf am 2026-07-12; `42,8x` und `27,4x` beschreiben Lieferdichte, keine Stoppuhrzeit. Lokale Abnahme: 14/14 Wave-3-Smokes, 550/550 Volltests, alle fuenf Coverage-Gates ueber 70 %, DocFX 0 Warnungen/0 Fehler, Playwright/axe 2/2 sowie saubere Secret- und Generated-Output-Scans. Das Lastenheft ist branch-suffigiert archiviert; Remote-Abschluss folgt in derselben Feature-Evidence, naechster Intake ist Mouse Support and Interaction. |
| 2026-07-12 | Post-Merge-Ledger-Abschluss fuer `019-wave3-visual-component-porting` | PR #45 bestand Ubuntu-/macOS-CI, macOS-/Linux-/Windows-Homogeneity, DocFX, Supply Chain, Gitleaks, Agent-Secret-Scan und Claude Review auf dem finalen Head. GraphQL meldete 0 Review-Threads und 0 Konversationskommentare; Copilot konnte wegen Nutzerquota keinen Review erzeugen und bleibt als fehlender Review dokumentiert. Ausschliesslich die Human-Approval-Regel blockierte den mergefaehigen PR, daher wurde der autorisierte Admin-Bypass eng auf diese Regel begrenzt. Merge-Commit ist `60f5951`; der Remote-Featurebranch wurde geloescht und lokales sauberes `main` entsprach danach exakt `origin/main`. Der nicht leere Closeout aktualisiert nur Taskstatus, Evidence und diese Statistikzeile. |
| 2026-07-12 | Lernende Nacharbeit nach autonomem Feature 019 | Die zweite Feldbeobachtung bestaetigt vollstaendige projektlokale Red-Matrizen als belastbare Effizienzregel. Runbook, `$speckit-autonomous`, Task-Template und alle fuenf Agentenflaechen verlangen nun vor dem ersten Red-Befehl einen Compile-Surface-Check fuer Imports, oeffentliche XML-Dokumentation, Harness-Helfer, Fokus-/Ownership-Assertionen und Linked-Source-Assembly-Identitaet. Negative Faelle duerfen nur bei expliziten Einzelgrenzen und gemeinsamer Ownership gebuendelt werden; mehrfach gelinkter Quellcode wird Cross-Projekt ueber oeffentliche Vertraege oder Zustandsdelegaten bewiesen. Aenderungsvolumen vor dieser Statistikzeile: `+89` reine Dokumentations-, Skill-, Template- und Guidance-Zeilen, kein Produktions- oder Testcode. Konservative Manualreferenz: `1,1` Tage beziehungsweise `8,7` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `0,7` Tage beziehungsweise `5,6` Stunden bei 125 Zeilen/Tag. Sichtbares Arbeitsfenster ist die lernende Nacharbeit am 2026-07-12; Validierung umfasst Skill-YAML, `specify check`, Agentenparitaet, Diff, Secrets sowie den DocFX-/A11Y-/lynx-Pfad. |
| 2026-07-12 | `/speckit-autonomous` für `020-mouse-support-interaction` | Feature 020 liefert einen begrenzten SGR-1006-Ingress in `TuiVision.Drivers.Console`, rekursive globale View-Koordinaten, Topmost-Hit/Fokus, Exactly-once-Control-Aktivierung, deterministische 500-ms-Doppelklickklassifikation und genau einen `TWindow`-Titelzeilen-Drag mit vollständigen Abbruch- und Tastaturfallbackpfaden. Die lokale Umgebung ist macOS, aber headless mit `TERM=dumb`; physische Host-Evidence bleibt deshalb ehrlich `NotRun`. Der Vorvalidierungs-Snapshot ohne diese Statistikzeile umfasst `+986/-12` Produktionscode, `+933` Tests, `+1681/-4` Dokumentation/Evidence/Guidance und `+4/-4` Metadaten, zusammen `+3604/-20` beziehungsweise `3584` Nettozeilen. Konservative Manualreferenz für 3604 hinzugefügte oder aktualisierte Zeilen: `45,1` Tage beziehungsweise `351,4` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `28,8` Tage beziehungsweise `224,9` Stunden bei 125 Zeilen/Tag. Sichtbares Arbeitsfenster ist der autonome Lauf am 2026-07-12; `45,1x` und `28,8x` beschreiben Lieferdichte, keine Stoppuhrzeit. Lokale Slice-Abnahme: Driver 54/54, Fokusregressionen 81/81, Drag 6/6 und App-Loop 5/5; voller Release-, Coverage-, DocFX-/A11Y- und Remote-Abschluss folgt in derselben Feature-Evidence. Nächster Intake ist `Lastenheft_05_TerminalCharsetAndEmulation.md`. |
| 2026-07-12 | Post-Merge-Ledger-Abschluss für `020-mouse-support-interaction` | PR #48 bestand Ubuntu-/macOS-CI, macOS-/Linux-/Windows-Homogeneity, DocFX, Supply Chain, Gitleaks, Agent-Secret-Scan und Claude Review auf dem finalen Head `6944719`. GraphQL meldete null Review-Threads und null Konversationskommentare; Copilot blieb wegen Nutzerquota ein fehlender Review. Nur Human Approval erforderte den autorisierten engen Admin-Bypass. Feature-Merge `b52d90f` und Evidence-Closeout #49 mit Merge `5aca8c1` löschten ihre Remote-Branches; lokales sauberes `main` entsprach danach jeweils `origin/main`. Feature 020 schließt mit 126/126 Tasks. |
| 2026-07-12 | Lernende Nacharbeit nach autonomem Feature 020 | Der dritte Feldlauf bestätigt Compile-Surface-Check und projektlokale Red-Matrix. Neu korrigiert wird eine Evidence-Schleife: Ein Commit mit aktuellem Check-/Thread-Stand ändert den geprüften Head und entwertet seine eigene Aussage. Runbook, `$speckit-autonomous`, Tasks-/Evidence-Template und alle fünf Agentenflächen prüfen die Gates deshalb weiter vor dem Merge, legen selbstinvalidierende Reviewed-Head- und echte Post-Merge-Fakten aber in genau einen benannten kausalen Closeout-Pfad. Änderungsvolumen vor dieser Statistikzeile: `+65/-14` reine Dokumentations-, Skill-, Template-, Guidance- und Feature-Evidence-Zeilen, kein Produktions- oder Testcode. Konservative Manualreferenz: `0,8` Tage beziehungsweise `6,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `0,5` Tage beziehungsweise `4,1` Stunden bei 125 Zeilen/Tag. Validierung: Skill-Schema, `specify check`, sechs Presets, 5/5 Agentenparität, 31 eindeutige numerische Template-Task-IDs, Diff- und Secret-Prüfung, DocFX zweimal mit 0 Warnungen/0 Fehlern, Playwright/axe 2/2 und UTF-8-`lynx`-Review bestanden. Kein .NET-Build, Test oder Coverage-Lauf wurde ausgelöst, weil keine ausführbare Logik betroffen ist. Ein stackneutraler Detektor ist noch nicht belegt; deshalb wird kein neues Skript eingeführt. |
| 2026-07-12 | `/speckit-autonomous` für `021-terminal-charset-hardening` | Feature 021 liefert `TerminalSession` mit begrenztem C0-/CSI-Subset, 4.096-Zellen-FIFO, atomarer Recovery und Lifecycle, eine feste KOI8-R-Tabelle mit U+FFFD, die exakte rohe 8x16/256/4.096-Byte-Fixture, geschlossene `System.Text.Json`-Profile und ein `TTerminalView` mit Profil-/Capability-Status, Cursor und App-Loop-/Cell-Proof. Der Vor-Commit-Snapshot ohne diese Statistikzeile umfasst `+1580/-6` Produktionscode, `+933/-6` Tests, `+2120/-4` Dokumentation/Evidence/Guidance und `+4/-4` Metadaten, zusammen `+4637/-20` beziehungsweise `4617` Nettozeilen. Konservative Manualreferenz für 4637 hinzugefügte oder aktualisierte Zeilen: `58,0` Tage beziehungsweise `452,1` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `37,1` Tage beziehungsweise `289,3` Stunden bei 125 Zeilen/Tag. Sichtbares Arbeitsfenster ist der autonome Lauf am 2026-07-12; `58,0x` und `37,1x` beschreiben Lieferdichte, keine Stoppuhrzeit. Lokale Abnahme: 51/51 Driver-, 314/314 Controls- und 18/18 Compatibility-Targeted-Tests, 640/640 Volltests, Coverage Core 89,78 %, Controls 83,42 %, Serialization 89,50 %, Compatibility 80,55 % und Drivers.Console 89,18 %, DocFX 0 Warnungen/0 Fehler, Playwright/axe 2/2 sowie UTF-8-`lynx`-Review. Die lokale Umgebung ist Darwin arm64 mit `TERM=dumb`; physische Host-Evidence bleibt deshalb ehrlich `NotRun`. Nächster Intake ist `Lastenheft_Wave4-Visual-Component-Porting.md`. |
| 2026-07-12 | `/speckit-autonomous` für `022-wave4-visual-component-porting` | Feature 022 liefert `Terminal`, `Cyrillic`, `Fonts`, `ETerm` und `XTerm` als sichtbare Hauptkomponente mit echter `TStatusLine`, `Help -> Description`, kontrollierter Operation, Fallback sowie Zustands-, View- und Buffer-/Cell-Proof. Der Vor-Statistik-Snapshot umfasst `+962/-0` Produktionscode, `+588/-0` Tests, `+2007/-4` Markdown/Evidence/Guidance und `+166/-6` Projekt-/Tooling-Metadaten, zusammen `+3723/-10` beziehungsweise `3713` Nettozeilen. Konservative Manualreferenz für 3723 hinzugefügte oder aktualisierte Zeilen: `46,5` Tage beziehungsweise `362,9` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `29,8` Tage beziehungsweise `232,3` Stunden bei 125 Zeilen/Tag. Sichtbares Arbeitsfenster ist der autonome Lauf am 2026-07-12; `46,5x` und `29,8x` beschreiben Lieferdichte, keine Stoppuhrzeit. Lokale Abnahme: 137/137 vollständige Beispiel-Smokes, 662/662 Volltests, Coverage Core 89,78 %, Controls 83,42 %, Serialization 89,50 %, Compatibility 80,55 % und Drivers.Console 89,18 %, DocFX 0 Warnungen/0 Fehler, Playwright/axe 2/2 sowie UTF-8-`lynx` für alle fünf Guides. Die lokale Umgebung ist Darwin arm64 mit `TERM=dumb`; physische macOS-/Linux-/Windows-/WSL-Evidence bleibt ehrlich `NotRun`. Nächster Intake ist `Lastenheft_06_A11Y_Framework.md`. |
| 2026-07-12 | Closeout und autonome Retrospektive nach Feature 022 | Der kausale Closeout-PR #54 blieb mit 75 Evidence-Zeilen genau ein Commit und schrieb seine eigene URL nicht zurück. Die anschließende Retro-Änderung umfasst vor Statistikpflege `+61/-3` Runbook-, Skill-, Template-, Agent- und Retrospektivzeilen, also 58 Nettozeilen. Zusammen sind dies `+136/-3` beziehungsweise 133 Nettozeilen ohne Runtime- oder Testcode. Konservative Manualreferenz: `1,7` Tage beziehungsweise `13,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo `1,1` Tage beziehungsweise `8,5` Stunden bei 125 Zeilen/Tag. Promoviert wurden der nicht rekursive Single-Commit-Closeout und die Klassifikation doppelter Push-/PR-Checks; Coverage-argv und Primary-Proof-Marker bleiben `ObserveAgain`. |
| 2026-07-12 | `/speckit-autonomous` für `023-a11y-framework` | Feature 023 liefert opt-in `IAccessibleWidget`-Texte, genau einen typisierten und bis zur Shell propagierten Fokus-Broadcast, unveränderliche Shortcut-Abfragen für Menü und Status, explizites `TColorScheme.HighContrast`, eine vollständige siebenzeilige Tastaturinventur sowie die sichtbare `A11yFramework`-Referenz-App mit App-Loop-/Zustands-/View-/Cell-Proof. Der finale Vor-Statistik-Snapshot umfasst `+813/-27` Produktions-/Beispielcode, `+521/-0` Tests, `+1030/-1` Dokumentation/Evidence/Guidance und `+39/-5` Metadaten, zusammen `+2403/-33` beziehungsweise 2370 Nettozeilen. Konservative Manualreferenz für 2403 hinzugefügte oder aktualisierte Zeilen: `30,0` Tage beziehungsweise `234,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `19,2` Tage beziehungsweise `149,9` Stunden bei 125 Zeilen/Tag. Sichtbares Arbeitsfenster ist der autonome Lauf am 2026-07-12; `30,0x` und `19,2x` beschreiben Lieferdichte, keine Stoppuhrzeit. Lokale Abnahme: 24/24 Feature-Targets, 140/140 Beispiel-Smokes, final 687/687 Volltests, Coverage Core 90,45 %, Controls 83,89 %, Serialization 89,50 %, Compatibility 80,55 % und Drivers.Console 89,18 %, DocFX 0 Warnungen/0 Fehler, Playwright/Axe 2/2 sowie UTF-8-Lynx-Review. Native Plattform-Brücken und Vollmigration bleiben ehrliche Follow-ups; Wave 5 ist der nächste fachliche Intake, Feature 024 wurde nicht begonnen. |
| 2026-07-12 | Closeout und autonome Retrospektive nach Feature 023 | Feature-PR #56 wurde als Merge `7f90fda` geliefert; der kausale Closeout-PR #57 blieb mit 72 Evidence-Zeilen genau ein Commit und wurde als `0437e3e` gemergt. Beide PRs bestanden Pflichtchecks und Claude; Copilot blieb wegen Nutzerquota ein fehlender Review, GraphQL meldete null Threads und nur Human Approval erforderte den eng autorisierten Admin-Bypass. Die Retrospektive ändert vor dieser Statistikzeile `+118/-6`, also 112 Nettozeilen in Runbook, Skill, Templates, Agentenflächen, Retrospektive und fail-closed Homogeneity-Wrappern. Konservative Manualreferenz: `1,5` Tage beziehungsweise `11,5` Stunden bei 80 hinzugefügten Zeilen/Tag; Thorsten-Solo: `0,9` Tage beziehungsweise `7,4` Stunden bei 125 Zeilen/Tag. Validierung: Skill-Schema, `specify check`, Preset-Matrix, Agentenparität, Diff und Secret-Scan grün; Bash- und PowerShell-Syntax bestanden, beide Wrapper liefern bei fehlenden Helpern erwartungsgemäß Exitcode 2 und laufen mit dem vollständigen Home-Baseline-Helfersatz ohne Abhängigkeitsfehler. DocFX bestand zweimal mit 0 Warnungen/0 Fehlern, Playwright/Axe 2/2 sowie drei UTF-8-Lynx-Seiten. Keine .NET-Ausführung wurde ausgelöst, weil keine C#-Runtime oder Tests betroffen sind. |
| 2026-07-12 | Öffentliches Preset in TuiVision adoptiert | `autonomous-run-governance` v0.1.0 wurde mit Priorität 70 aus dem veröffentlichten Tag-ZIP installiert und ergänzt die unveränderte Standard-Sechsermatrix. Der öffentliche Payload stimmt bytegenau mit der Installation überein; beide Commands sind auf Codex-, Claude-, Copilot- und OpenCode-Flächen jeweils eindeutig. Der vorhandene Codex-Orchestrierungs-Skill bleibt als einzelner lokaler Override erhalten, weil er zusätzliche TuiVision-Verträge für nummerierte Branches, Build-Zähler, DocFX/A11Y und historische Quellen trägt. Der Vor-Statistik-Snapshot umfasst `+1212/-0`, also 1212 Nettozeilen in Preset, Registry, generierten Agent-Flächen, Guidance, Template und Adoption-Evidence, ohne Runtime-, Test-, Paket-, Beispiel- oder `tv203s/`-Änderung. Konservative Manualreferenz: `15,2` Tage beziehungsweise `118,2` Stunden bei 80 hinzugefügten Zeilen/Tag; Thorsten-Solo: `9,7` Tage beziehungsweise `75,6` Stunden bei 125 Zeilen/Tag. `specify check`, siebenfaches Preset-Resolve, Payload-/Checksum-, Override-, Eindeutigkeits-, Agentenparitäts-, Diff-, Secret-, DocFX-, Playwright/Axe- und Lynx-Evidence bilden den Abschluss; `.NET`-Build, Tests und Coverage werden mangels ausführbarer Änderung nicht ausgelöst. |
| 2026-07-12 | Antigravity-Folgeabgleich nach Preset-Adoption | Die aktiven Betriebsflächen verwenden jetzt `agy` statt direkter Gemini-CLI-Aufrufe: Installationspflicht, Agent-Kontextpflege, Multi-Mac-Guide und `agy/**`-CI-Branchfilter sind ausgerichtet; `gemini/**` bleibt ausdrücklich legacy-kompatibel. Alle fünf Agent-Dateien tragen dieselbe Übergangsregel. `GEMINI.md`, `~/.gemini/antigravity-cli/`, historische Specs und Ledger-Einträge sowie Enterprise-/Cloud-/API-Kompatibilität bleiben erhalten. Die Adoption-Evidence trennt reales Antigravity-Dogfooding von der historisch wahren v0.1.0-Gemini-Kompatibilitätsprüfung. Der Vor-Statistik-Snapshot umfasst `+75/-24`, also 51 Nettozeilen, davon 50 Markdown- und eine Workflow-Zeile; Runtime, Tests, Pakete, Beispiele, Preset-Payload und `tv203s/` bleiben unverändert. Konservative Manualreferenz für 75 hinzugefügte oder aktualisierte Zeilen: `0,9` Tage beziehungsweise `7,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `0,6` Tage beziehungsweise `4,7` Stunden bei 125 Zeilen/Tag. |
| 2026-07-12 | Intake-Vorbereitung für `024-tv203-freevision-conformance-audit` | Das neue Lastenheft verankert ein reines Pre-Wave-5-Konformitätsaudit mit Borland/`tv203s` als Primärreferenz und dem gepinnten offiziellen Free-Vision-Commit nur als sekundärer Gegenprüfung. Die Reihenfolge wird auf 024, ausschließlich findings-basierte 025/026, das verpflichtende Closure 027 und danach Wave 5 ausgerichtet; veraltete aktive Verweise auf bereits archivierte 018–023-Lastenhefte werden korrigiert. Der Vor-Statistik-Snapshot umfasst `+402/-12`, also 390 Netto-Dokumentzeilen ohne Produktions-, Test-, Paket-, Beispiel-, Agent-, Preset- oder historische Source-Änderung. Konservative Manualreferenz für 402 hinzugefügte oder aktualisierte Zeilen: `5,0` Tage beziehungsweise `39,2` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `3,2` Tage beziehungsweise `25,1` Stunden bei 125 Zeilen/Tag. |
| 2026-07-12 | `/speckit-autonomous` für `024-tv203-freevision-conformance-audit` | Feature 024 inventarisiert 151 historische `.cc`-Dateien, 119 gepflegte Produktionsdateien, 176 exportierte öffentliche Typen und 48 Verträge in 16 Domänen. Die Primärentscheidungen sind 13 `Aligned`, 34 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 0 `BehavioralDrift` und 0 `EvidenceGap`; 15 gepinnte Free-Vision-Quellen bleiben unabhängige Sekundärevidence. Damit sind die Finding-Mengen für 025 und 026 leer, während 027 als verpflichtender Closure-Lauf offen bleibt. Der Vor-Statistik-Snapshot umfasst `+0/-0` Produktionscode, `+528/-0` Tests und Testprojekt, `+9352/-17` Dokumentation/Evidence/Guidance sowie `+4/-4` Metadaten, zusammen `+9884/-21` beziehungsweise 9863 Nettozeilen. Konservative Manualreferenz für 9884 hinzugefügte oder aktualisierte Zeilen: `123,6` Tage beziehungsweise `963,7` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `79,1` Tage beziehungsweise `616,8` Stunden bei 125 Zeilen/Tag. Lokale Abnahme: Audit-Tests 11/11, Volltests 698/698, Coverage Core 90,45 %, Controls 83,89 %, Serialization 89,50 %, Compatibility 80,55 % und Drivers.Console 89,18 %. Produktcode, öffentliche API, Pakete, Beispiele, historische Quellen und der externe Free-Vision-Worktree bleiben unverändert. |
| 2026-07-12 | Closeout und autonome Retrospektive nach Feature 024 | Feature-PR #62 wurde als `5c0a4d7` gemergt; der kausale Closeout-PR #63 blieb genau ein Evidence-Commit und wurde als `f3fd98f` gemergt. Beide PRs bestanden alle technischen Gates und Claude, Copilot blieb wegen Nutzerquota ein fehlender Review, GraphQL meldete null Threads und nur Human Approval erforderte den eng autorisierten Bypass. Der Closeout und diese Retrospektive umfassen einschließlich Statistikpflege `+113/-46`, also 67 Netto-Dokumentzeilen. Konservative Manualreferenz: `1,4` Tage beziehungsweise `11,0` Stunden bei 80 hinzugefügten Zeilen/Tag; Thorsten-Solo: `0,9` Tage beziehungsweise `7,1` Stunden bei 125 Zeilen/Tag. Home-Baseline-Commit `db2bd86` korrigiert den reproduzierten PowerShell-Error-Channel-Defekt; die portable Preset-Regel war bereits vorhanden. 132/132 Tasks sind abgeschlossen, 025/026 bleiben unterdrückt und 027 ist der nächste verpflichtende Intake. |
| 2026-07-12 | Intake-Vorbereitung für `027-pre-wave5-conformance-closure` | Das audit-abgeleitete Lastenheft definiert den verpflichtenden Revalidation-, Integrations- und Release-Gate-Abschluss bei unverändertem Produkt-Scope. Es bindet 16 Domänen, 48 Contracts, 151/119/176 Inventare, 15 Free-Vision-Records, 94 Proof-Referenzen und die Entscheidungsmengen 13/34/1/0/0. Jede neue Drift stoppt Closure und öffnet zuerst eine reviewte Audit-Revision; leere 025-/026-Features bleiben verboten. Der gesamte Branch-Diff einschließlich Statistikpflege umfasst `+262/-19`, also 243 Netto-Dokumentzeilen. Konservative Manualreferenz: `3,3` Tage beziehungsweise `25,5` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `2,1` Tage beziehungsweise `16,3` Stunden bei 125 Zeilen/Tag. Wave 5 bleibt bis zum gemergten Feature 027 blockiert. |
| 2026-07-12 | `/speckit-autonomous` für `027-pre-wave5-conformance-closure` | Feature 027 revalidiert den gemergten Auditstand exakt: 16 Domänen, 48 Contracts, Inventare 151/119/176, 15 gepinnte Free-Vision-Quellen, 94 Proof-Referenzen, Entscheidungen 13/34/1/0/0 und null Findings. 025/026 bleiben unterdrückt. Lokale Abnahme: Audit 11/11, Volltests 698/698, Coverage Core 90,45 %, Controls 83,89 %, Serialization 89,50 %, Compatibility 80,55 % und Drivers.Console 89,18 %, DocFX 0/0, Playwright/Axe 2/2, UTF-8-Lynx 4/4 und Secrets high 0. Der Vor-Statistik-Snapshot umfasst `+1155/-25`, also 1130 Nettozeilen ausschließlich in Spec-Kit-, Evidence-, Status-, Agent- und Versionsflächen. Konservative Manualreferenz für 1155 hinzugefügte oder aktualisierte Zeilen: `14,4` Tage beziehungsweise `112,6` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `9,2` Tage beziehungsweise `72,1` Stunden bei 125 Zeilen/Tag. Produktcode, API, Pakete, Beispiele und historische Quellen bleiben unverändert; der reviewte Merge ist die letzte Wave-5-Freigabegrenze. |
| 2026-07-12 | Closeout und autonome Retrospektive nach Feature 027 | Feature-PR #66 wurde nach vollständig grünen technischen Gates und null GraphQL-Threads als Merge `35414af` geliefert; Copilot blieb quota-bedingt nicht verfügbar und der eng autorisierte Bypass betraf nur Human Approval. Der nicht rekursive Closeout richtet Gate, Pflichtenheft, Intake-Reihenfolge und Agent-Kontexte auf Wave 5 aus. Die Retrospektive fand eine portable Trigger-Lücke: ein Audit-Test erwartete noch `Blocked`, nachdem der Closeout korrekt auf `Eligible` wechselte. CI stoppte, der Validator wurde mit 1/1 Tests korrigiert. Home-Baseline-PR #60 lieferte die Regel als öffentliches Preset v0.1.1; Tag-ZIP und Upstream-Issue #3479 wurden nachgewiesen. Der Vor-Statistik-Diff umfasst `+94/-63`, also 31 Nettozeilen. Konservative Manualreferenz: `1,2` Tage beziehungsweise `9,2` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `0,8` Tage beziehungsweise `5,9` Stunden bei 125 Zeilen/Tag. |
| 2026-07-12 | Audit-Revision 2 und Intake-Vorbereitung für 025, 026 und 028 | Eine kritische Verbraucherprüfung gegen die read-only Quellen `TVDEMOS/` und `TVFM/` korrigiert die Zukunftsentscheidung, ohne die historischen 024-/027-Läufe umzuschreiben. Die 48 Verträge stehen nun bei 7 `Aligned`, 27 `IntentionalModernization`, 1 `ConsciouslyOmitted`, 8 `BehavioralDrift` und 5 `EvidenceGap`; neun Findings gehören verbindlich zu `Core025`, vier zu `ComponentData026`. Der Validator prüft beidseitige Inventarrelationen, geschlossene Finding-Felder und existierende Source-Evidence. Drei neue Lastenhefte schreiben 025 -> 026 -> 028 fest; nächster Intake ist Lastenheft 10, heute wurde kein autonomer Lauf gestartet. Der Vor-Statistik-Snapshot umfasst `+0/-0` Produktionscode, `+138/-12` Testvalidator, `+1852/-277` Dokumentation/Evidence/Guidance und nur ausgleichende Versionsmetadaten, insgesamt `+1993/-292` beziehungsweise 1701 Nettozeilen. Konservative Manualreferenz für 1993 hinzugefügte oder aktualisierte Zeilen: `24,9` Tage beziehungsweise `194,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `15,9` Tage beziehungsweise `124,4` Stunden bei 125 Zeilen/Tag. Statische Integrität, Format und targeted Auditvalidator 11/11 sind grün; DocFX besteht mit 0 Warnungen/0 Fehlern und Playwright/Axe mit 2/2. |
| 2026-07-13 | `/speckit-autonomous` für `025-core-runtime-conformance-hardening` | Feature 025 schließt `F001` bis `F009` test-first: konkrete Event-Kinds, validator-eigenes Fokus-Veto, zustandsspezifische Group-Propagation, ein Pending-Slot mit Idle/CPU-Freigabe, Desktop-Stack-Operationen, sichtbares Close und owner-lokale Modalität, gemeinsamer Command-Kontext, kanonischer realer Keyboard-Ingress sowie eine begrenzte Pointer-/Tastatur-Drag-Session. Feature 024 enthält nun 127 moderne Source-Dateien, 192 öffentliche Typen und exakt neun nicht dokumentationsbasierte `Closed`-Resolutionen; `F010` bis `F013`, Feature 026/028 und beide Wave-Gates bleiben offen. Der Vor-Statistik-Snapshot umfasst `+1641/-150` Produktionscode, `+1237/-33` Tests, `+2598/-46` Dokumentation/Evidence/Guidance und `+5/-4` Metadaten, zusammen `+5481/-233` beziehungsweise 5248 Nettozeilen. Konservative Manualreferenz für 5481 hinzugefügte oder aktualisierte Zeilen: `68,5` Tage beziehungsweise `534,4` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `43,8` Tage beziehungsweise `342,0` Stunden bei 125 Zeilen/Tag. Das sichtbare Arbeitsfenster ist der autonome Lauf am 2026-07-13; `68,5x` und `43,8x` beschreiben überprüfbare Lieferdichte, keine Stoppuhrzeit. Lokale Abschlussgates und Remote-Evidence werden nach dieser Statistikzeile in derselben Feature-Evidence ergänzt. |
| 2026-07-13 | `autonomous-run-governance` v0.1.2 veröffentlicht und adoptiert | Die zwei Feature-025-Erkenntnisse zur exakten Staging-Kandidatenprüfung und zur Zuordnung jedes Acceptance-Gates zu wirklich ausgeführtem Workflow, Job, Runner beziehungsweise Plattform und Befehl sind portabel produktisiert. Home-Baseline-PR #61 und öffentlicher Preset-PR #2 wurden gemergt; Tag/Release v0.1.2 und das GitHub-ZIP mit SHA-256 `6e401289...e2d0` sind geprüft. TuiVision installiert die Version aus dem Tag-ZIP bei Priorität 70; alle sieben Presets bleiben aufgelöst, Codex/Antigravity, Claude, Copilot und OpenCode zeigen beide Commands je Oberfläche genau einmal, und der TuiVision-Codex-Override bleibt bytegleich. Der Vor-Statistik-Snapshot umfasst `+292/-29`, also 263 Nettozeilen in Preset, Registry, generierten Agent-Flächen, Guidance, Template und Evidence ohne Runtime-, Test-, Paket-, Beispiel- oder `tv203s/`-Änderung. Konservative Manualreferenz für 292 hinzugefügte oder aktualisierte Zeilen: `3,7` Tage beziehungsweise `28,5` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `2,3` Tage beziehungsweise `18,2` Stunden bei 125 Zeilen/Tag. `specify check`, Tag-ZIP-/Payload-/Resolve-/Eindeutigkeits-/Override- und Agentenparitätsprüfung sind grün; DocFX, Playwright/Axe, Lynx, Diff und Secrets schließen den Adoptions-PR ab. |
| 2026-07-13 | `/speckit-autonomous` für `026-component-data-conformance-hardening` | Feature 026 schließt `F010` bis `F013` test-first: Dialoge klassifizieren nur echte Abschlussbefehle und validieren hierarchisch; `TInputLine` erhält phasenbezogene, zustandserhaltende Validierung; Dateidialoge liefern mode-spezifische, atomare Entscheidungen ohne versteckte Datei-I/O; Menü-, StatusLine- und Dialogbeschreibungen werden ausschließlich über geschlossene primitive Records und allowlist-basierte Factories rekonstruiert. Feature 024 enthält nun 139 moderne Source-Dateien, 211 öffentliche Typen und exakt 13 eindeutige Resolutionen; Wave 5 und Wave 6 bleiben bis Feature 028 blockiert. Der Vor-Statistik-Snapshot umfasst `+1761/-97` Produktionscode, `+802/-26` Tests, `+2539/-60` Dokumentation/Evidence/Guidance und `+4/-4` Metadaten, zusammen `+5106/-187` beziehungsweise 4919 Nettozeilen. Konservative Manualreferenz für 5106 hinzugefügte oder aktualisierte Zeilen: `63,8` Tage beziehungsweise `497,8` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `40,8` Tage beziehungsweise `318,6` Stunden bei 125 Zeilen/Tag. Lokale Abnahme: 748/748 Release- und Coverage-Tests; Coverage Core 92,96 %, Controls 86,66 %, Serialization 90,01 %, Compatibility 80,55 % und Drivers.Console 89,18 %; DocFX, Playwright/Axe und UTF-8-Lynx werden auf dem finalen Dokumentstand nachgewiesen. Der nächste Intake ist ausschließlich `Lastenheft_12_Pre-Wave5-and-Wave6-Conformance-Closure.md`. |
| 2026-07-13 | Closeout und autonome Retrospektive nach Feature 026 | Feature-PR #74 wurde als Merge `f3586aa` geliefert. Alle PR-Kontext-Gates und Claude waren grün, GraphQL meldete null Threads, Copilot blieb quota-bedingt ein fehlender Review und der enge Admin-Bypass betraf nur Human Approval. Die Retrospektive erkannte danach, dass der grüne Windows-Homogeneity-Job keine Runtime ausführte, obwohl der Contract Windows/WSL-Proof verlangte. Der temporäre, nicht gemergte Run 29291308306 schloss die Lücke mit 748/748 Tests und DocFX 0/0 auf `windows-latest`. Der Vor-Statistik-Diff für kausalen Closeout und Retrospektive umfasst `+134/-0` Dokumentzeilen. Konservative Manualreferenz: `1,7` Tage beziehungsweise `13,1` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `1,1` Tage beziehungsweise `8,4` Stunden bei 125 Zeilen/Tag. Home-Baseline-Commit `046b65a` klassifiziert das zweite Auftreten als `PresetFollowUp` mit `Promote`: Eine spätere Paketversion muss Applicable Gate, exakten Head, Workflow, Job, Runner und ausgeführten Befehl maschinenprüfbar binden. Feature 028 bleibt der einzige nächste Intake. |
| 2026-07-14 | `autonomous-run-governance` v0.1.4 veröffentlicht und adoptiert | Die zwei unabhängigen False-Readiness-Feldfunde aus Feature 025 und 026 sind über v0.1.3/v0.1.4 als deklarierte Gate-Requirements, temporäre exakte HEAD-Evidence und read-only Bash-/PowerShell-Validatoren produktisiert. Home-Baseline-PRs #62/#63 und öffentliche Preset-PRs #3/#4 wurden gemergt; Release v0.1.4 und der GitHub-ZIP mit SHA-256 `da667e2f...967e0` sind geprüft. TuiVision installiert den exakten Tag-ZIP bei Priorität 70. Alle sieben Presets bleiben aufgelöst und beide autonomen Commands erscheinen je Agent-Oberfläche genau einmal. Der installierte Bash-Modus `0644` reproduziert die Paketgrenze; `bash <validator.sh>` und `pwsh -NoProfile -File <validator.ps1>` bestehen dieselbe Fixture. Der Vor-Statistik-Snapshot umfasst `+1086/-38`, also 1048 Nettozeilen in Preset, Validatoren, Agent-Flächen, Guidance, Template und Evidence ohne Runtime-, API-, Test-, Abhängigkeits-, Beispiel-, Projekt- oder `tv203s/`-Änderung. Konservative Manualreferenz: `13,6` Tage beziehungsweise `105,9` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `8,7` Tage beziehungsweise `67,8` Stunden bei 125 Zeilen/Tag. DocFX, Playwright/Axe, Lynx, Diff und Secrets schließen den Adoptions-PR ab; Feature 028 bleibt der einzige nächste Intake. |
| 2026-07-14 | Intake-Vorbereitung für 028 und `029-tv203-freevision-terminalgui-conformance-audit` | Lastenheft 12 schließt nach 028 nur das bestehende TV203-/Free-Vision-Gate; beide Waves bleiben bis zur zusätzlichen Terminal.GUI-v1.9.0-Prüfung, allen real findings-basierten Hardening-Läufen und einem neuen unabhängigen Closure blockiert. Lastenheft 13 pinnt das offizielle `tui-cs/Terminal.Gui`-Tag v1.9.0 mit Tag-Objekt und Commit, erweitert alle 48 Verträge um eine dritte Relation und verbietet Runtime-, API-, Dependency-, Beispiel- oder Fremdquellenänderungen. Nicht leere Folgefeatures werden dynamisch ab 030 nummeriert. Der Vor-Statistik-Snapshot umfasst `+471/-76`, also 395 Netto-Dokumentzeilen. Konservative Manualreferenz: `5,9` Tage beziehungsweise `45,9` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `3,8` Tage beziehungsweise `29,4` Stunden bei 125 Zeilen/Tag. |
| 2026-07-14 | `autonomous-run-governance` v0.2.0 veröffentlicht und adoptiert | Die unterbrochene 028-Vorbereitung wird als produktiver Feldfall genutzt, ohne den Feature-Lauf fortzusetzen: v0.2.0 ergänzt lesenden Status, kooperatives Stoppen, geschützte Wiederaufnahme und einen validierten feature-lokalen Laufzustand. Home-Baseline-PR #65 und öffentlicher Preset-PR #5 sind gemergt; Release v0.2.0 und das Tag-ZIP mit SHA-256 `7cde2b22...7237` sind geprüft. TuiVision installiert den bytegleichen Payload bei Priorität 70. Alle sieben Presets bleiben aufgelöst und fünf autonome Commands erscheinen auf Codex/Antigravity, Claude, beiden Copilot-Flächen und OpenCode jeweils genau einmal. Eine temporäre `PausedByUser`-Fixture für den unveränderten 028-Commit besteht Bash und PowerShell; die widersprüchliche `Interrupted`-Variante wird von beiden verworfen. Der Vor-Statistik-Snapshot umfasst `+1605/-63`, also 1542 Nettozeilen ausschließlich in Preset, Validatoren, Skills, Agent-Flächen, Guidance, Runbook und Adoption-Evidence. Runtime, API, Tests, Abhängigkeiten, Beispiele, Feature-028-Artefakte und `tv203s/` bleiben unverändert. Konservative Manualreferenz: `20,1` Tage beziehungsweise `156,5` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `12,8` Tage beziehungsweise `100,2` Stunden bei 125 Zeilen/Tag. Skill-/YAML-, Diff-, Format- und Secret-Prüfung (`high=0`), DocFX 0/0, Playwright/Axe 2/2 und UTF-8-`lynx` schließen die lokale Adoption ab. Ein realer Resume-Feldnachweis bleibt bis zur ausdrücklichen Freigabe von Feature 028 zurückgestellt. |
| 2026-07-15 | Echter Resume-Feldnachweis und autonome Umsetzung von Feature 028 | Der unterbrochene Lauf wurde aus dem validierten v0.2.0-Run-State wiederaufgenommen, mit `main` abgeglichen und ab T001 fortgesetzt. 13 Findings, sieben reale Integrations-Slices und 13 read-only Consumer-Gruppen sind vollständig geschlossen; zwölf Consumer nutzen das vorhandene Framework, `W6-007` bleibt als destruktive Produktpolitik `FollowUpHardening`. Sieben Governance-Presets und neun provider-neutrale Gates sind explizit belegt; Windows ergänzt die unveränderte CI-Runtime-Matrix. Der Vor-Statistik-Snapshot umfasst 25 Pfade: `+2086/-202` Dokumentation/Evidence/Guidance, `+538/-0` test-only Validatoren, eine Workflow-Zeile und ausgleichende Versionsmetadaten, insgesamt `+2628/-206` beziehungsweise 2422 Nettozeilen. Lokale Abnahme: 756/756 Release-Tests; Coverage Core 92,96 %, Controls 86,66 %, Serialization 90,01 %, Compatibility 80,55 %, Drivers.Console 89,18 %; DocFX 0/0, Playwright/Axe 2/2, UTF-8-Lynx, 0 vulnerable packages, CycloneDX 1.7 mit 21 Komponenten, Secrets high 0 und Gitleaks 0/456 Commits. Produktcode, API, Dependencies, Beispiele, `tv203s/`, `TVDEMOS/` und `TVFM/` bleiben unverändert. Feature 029 ist der einzige nächste Intake; beide Waves bleiben blockiert. |
| 2026-07-15 | `autonomous-run-governance` v0.2.1 veröffentlicht und adoptiert | Der echte 028-Resume-Feldfund wird als begrenzter Pflichtregel-Delta-Audit produktisiert: nach Preset- oder Governance-Drift werden neue zwingende Korrektheits-, Sicherheits-, Berechtigungs- und Evidenzregeln mit akzeptierten Plan-, Task- und Checklist-Artefakten abgeglichen; nur anwendbare fehlende Regeln werden in-place ergänzt und erneut analysiert, Effizienzpräferenzen bleiben retrospektiv. Home-Baseline-PR #67 und öffentlicher Preset-PR #6 sind gemergt; Release v0.2.1 und Tag-ZIP-SHA-256 `799cc189...cf75` sind geprüft. TuiVision installiert den bytegleichen Payload bei Priorität 70, erhält den projektgebundenen Codex-Override und fünf UI-YAML-Dateien und zeigt fünf Commands je gepflegter Oberfläche genau einmal. Der Vor-Statistik-Snapshot umfasst `+326/-97`, also 229 Nettozeilen in Preset, Skills, Agent-Flächen, Template, Runbook, Adoption-Evidence und Statistik ohne Runtime-, API-, Test-, Dependency-, Beispiel- oder Fremdquellenänderung. Konservative Manualreferenz: `4,1` Tage beziehungsweise `32,6` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `2,6` Tage beziehungsweise `20,9` Stunden bei 125 Zeilen/Tag. Format, Diff, Secrets high 0, DocFX 0/0, Playwright/Axe 2/2 und UTF-8-Lynx sind grün. Copilot Legacy-Agent/Prompt bleibt belegt; fehlende Custom-Preset-Commands im neuen Copilot-Skills-Modus und das `compatibility`-Schema-Mismatch des aktuellen Codex-Quick-Validators sind externe Toolgrenzen. Feature 029 bleibt ungestartet und beide Waves bleiben blockiert. |
| 2026-07-15 | Kausaler Closeout des echten Feature-028-Resume-Feldlaufs | Feature-PR #79 wurde nach einer auf zwei veraltete Evidence-Assertions begrenzten CI-Korrektur als `28f23cc` gemergt. Der finale Head `75889b8` bestand 756/756 Tests auf Ubuntu, macOS und Windows, DocFX/A11Y, Homogeneity, Supply Chain, PowerShell, Agent Secrets, Gitleaks und Claude; GraphQL meldete null Threads, Copilot blieb quota-bedingt ein fehlender Review und nur Human Approval erforderte den engen Bypass. Neun Primary-Gate-Zeilen sind an Requirements-Hash, Head, Workflow, Job, Plattform und Befehl gebunden; Bash und PowerShell akzeptieren die Evidence und lehnen die manipulierte Kopie ab. Home-Baseline-PRs #67/#69, öffentliches Preset-PR #6, Release v0.2.1 und TuiVision-Adoptions-PR #81 schließen den promovierten Pflichtregel-Delta-Audit ab. Der nicht rekursive Closeout umfasst fünf reine Evidence-/State-/Task-/Statistikpfade mit `+263/-33`, also 230 Nettozeilen; konservative Manualreferenz `3,3` Tage beziehungsweise `25,5` Stunden bei 80 Zeilen/Tag, Thorsten-Solo `2,1` Tage beziehungsweise `16,3` Stunden bei 125 Zeilen/Tag. State- und Gate-Validatoren, Diff, Secrets high 0, DocFX 0/0, Playwright/Axe 2/2 und UTF-8-Lynx sind grün; .NET wird mangels ausführbarer Closeout-Änderung nicht erneut ausgelöst. Der Run-State steht auf `Completed` mit 146/146 Aufgaben. Feature 029 bleibt einziger nächster Intake; beide Waves bleiben blockiert. |
| 2026-07-15 | Separaten `magiblot/tvision`-Evolutionsaudit als Feature 030 vorbereitet | Lastenheft 13 bleibt auf Terminal.GUI v1.9.0 begrenzt und liefert `TG*`-Candidate-Findings nur als maschinenlesbaren Handoff. Das neue Lastenheft 14 pinnt den direkten C++-Modernisierungszeugen auf Commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`, Tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6` und den mehrteiligen `COPYRIGHT`-Hash `66220bae...548`; es vergleicht beobachtbare Verantwortungen statt C++-Form oder Namensparität. Erst Feature 030 dedupliziert `TG*` und `MB*` zu kanonischen `CF*`-Findings; nicht leere Hardening-Läufe beginnen ab 031 und ein unabhängiger Closure folgt zuletzt. Der Vor-Statistik-Snapshot umfasst acht reine Intake-/Guidance-Pfade mit `+535/-70`, also 465 Nettozeilen. Konservative Manualreferenz: `6,7` Tage beziehungsweise `52,2` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `4,3` Tage beziehungsweise `33,4` Stunden bei 125 Zeilen/Tag. `specify check`, Pin-/Tree-/Lizenzhash, fünf Agentenflächen, Diff, Format, Bash-/PowerShell-Homogeneity 100 %, DocFX 0/0, Playwright/Axe 2/2, UTF-8-Lynx und Secrets high 0 sind grün. Runtime, API, Dependencies, Tests, Beispiele und historische oder externe Quellen bleiben unverändert; Feature 029 bleibt der einzige unmittelbare nächste Lauf und beide Waves bleiben blockiert. |
| 2026-07-16 | Echter Resume-Feldnachweis und autonome Umsetzung von Feature 029 | Der durch den Benutzer gestoppte Lauf wurde aus dem validierten v0.2.1-Run-State mit erneuter Authority-, Drift-, Checklisten- und Preset-Prüfung fortgesetzt. Das Audit bindet Terminal.GUI v1.9.0 an Tag-Objekt `4b812e4`, Commit `d5abc20`, MIT-Lizenz und 25 SHA-256-geprüfte Produktions-/UnitTest-Quellen. Alle 48 Verträge und 16 Domänen erhalten genau eine Relation: 17 `CorroboratesOriginal`, 4 `CorroboratesModernization`, 20 `AlternativeModernization` und 7 `NotApplicable`; 6 Wave-5- und 7 Wave-6-Consumer bleiben vollständig. 48 `TGO###`-Beobachtungen ergeben 21 `AlreadySatisfiedWithNewEvidence`, 20 `IntentionalDeviation`, 7 `RejectedComparison`, 0 `CandidateFinding` und 0 `ProductDecision`; Feature 030 erhält den vollständigen maschinenlesbaren Handoff, während Wave 5/6 gesperrt bleiben. Der Vor-Statistik-Snapshot umfasst `+7330/-60`, davon 528 Zeilen test-only Validator, 6608 Feature-Artefakte, 110 Guide-Zeilen sowie Status-, Agent-, Versions- und Ordnungsanpassungen. Konservative Manualreferenz für 7330 hinzugefügte oder aktualisierte Zeilen: `91,6` Tage beziehungsweise `714,7` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `58,6` Tage beziehungsweise `457,4` Stunden bei 125 Zeilen/Tag. Frühe lokale Abnahme: D02 1/1, vollständiger Validator 8/8, Consumer 1/1, Provenienz 1/1 und Handoff 1/1; vollständige Release-, Coverage-, DocFX-/A11Y- und Remote-Gates folgen auf dem finalen Kandidaten. Produktcode, APIs, Dependencies, Beispiele, historische Quellen, Consumer-Quellen und externe Quellen bleiben unverändert. |
| 2026-07-16 | `autonomous-run-governance` v0.2.2 veröffentlicht und adoptiert | Den Feature-029-Feldnachweis in ein ausführliches bilinguales Bedien- und Lernhandbuch sowie eine strikte Stage-Zuordnung produktisiert: `Deliver` bleibt lesbare Skill-Überschrift, maschinenlesbarer Remote-Closeout nutzt `Publish`, `Review` oder `MergeAndSync`. Home-Baseline-PR #70 und öffentlicher Preset-PR #7 sind gemergt; Release v0.2.2 und Tag-ZIP-SHA-256 `e9f1bea4...cf4f0d` sind geprüft. TuiVision installiert den inhaltlich identischen Payload bei Priorität 70, erhält den projektgebundenen Codex-Override und fünf UI-YAML-Dateien und zeigt fünf Commands je gepflegter Oberfläche genau einmal. Der Vor-Statistik-Snapshot umfasst `+605/-89`, also 516 Nettozeilen in Preset, Skills, Agent-Flächen, Template und Adoption-Evidence ohne Runtime-, API-, Test-, Dependency-, Beispiel- oder Fremdquellenänderung. Konservative Manualreferenz: `7,6` Tage beziehungsweise `59,0` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `4,8` Tage beziehungsweise `37,8` Stunden bei 125 Zeilen/Tag. Spec Kit 0.12.11, Sieben-Preset-Auflösung, Stage-Validatoren, Format, Diff, Homogeneity 100, Secrets high 0, DocFX 0/0, Playwright/Axe 2/2 und UTF-8-Lynx sind grün. Der erste DocFX-Aufruf scheiterte nur am eingeschränkten App-PATH und bestand mit expliziter Toolauflösung. Feature 030 bleibt der einzige nächste Intake und wird nicht gestartet; beide Waves bleiben blockiert. |
| 2026-07-16 | Kausaler Closeout des Feature-029-Resume-Feldlaufs | Feature-PR #84 wurde auf dem finalen Head `50b715e` als `e825b7d` gemergt. Zehn Primary-Gate-Zeilen waren an Requirements-Hash, Head, Workflow, Job, Plattform und Scope gebunden; Bash und PowerShell akzeptierten die Evidence. Linux, macOS, Windows, DocFX/A11Y, Homogeneity, Supply Chain, PowerShell, Agent Secrets, Gitleaks und Claude waren grün; GraphQL meldete null Threads, Copilot blieb quota-bedingt ein fehlender Review und nur Human Approval erforderte den engen Bypass. Die Retrospektive produktisierte das Bedienhandbuch und die strikte Stage-Zuordnung über Home-Baseline-PRs #70/#71, öffentliches Preset-PR #7, Release v0.2.2 und TuiVision-Adoptions-PR #85. Der Closeout ändert nur fünf Evidence-, State-, Task-, Statistik- und Retrospektivpfade; Runtime, API, Tests, Dependencies, Beispiele und historische oder externe Quellen bleiben unverändert. State-Validatoren, Diff, Secrets und DocFX/A11Y bilden die Abschlussgates; .NET wird mangels ausführbarer Änderung nicht erneut ausgelöst. Der Run-State steht auf `Completed` mit 130/130 Aufgaben. Feature 030 bleibt einziger nächster Intake und wird nicht gestartet; beide Waves bleiben blockiert. |
| 2026-07-16 | Post-Wave-6 Example-Portfolio-Audit als Lastenheft 15 vorgemerkt | Einen verbindlichen, aber noch unnummerierten Folgeaudit nach dem vollständigen Wave-6-Closeout vorbereitet. Das Lastenheft inventarisiert später alle 25 Originalbeispiele, alle tatsächlich gelieferten Wave-5-/Wave-6-Beispiele und `A11yFramework` als `SupplementalControl`; TV203 beziehungsweise TP7 bleiben historische Autorität, während Free Vision, Terminal.GUI v1.9.0 und das Feature-030-gepinnte `magiblot/tvision` nur zusätzliche Implementierungsmeinungen liefern. Pro Beispiel sind Portfolio-Rolle, Hauptentscheidung, Dimensionsstatus, Framework-Nutzung, sichtbare Bedienung, Real-App-Loop-/View-/Buffer-/Cell-Proof, Guide, A11Y und Plattformgrenzen festgelegt. Nur deduplizierte reale `EF*`-Findings dürfen nicht leere Owner-Lastenhefte und anschließend einen unabhängigen Portfolio-Closure erzeugen. Der Vor-Statistik-Snapshot umfasst `+522/-3`, also 519 Nettozeilen in Lastenheft, Reihenfolge, Pflichtenheft und fünf Agent-Oberflächen. Konservative Manualreferenz: `6,5` Tage beziehungsweise `52,2` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `4,2` Tage beziehungsweise `33,4` Stunden bei 125 Zeilen/Tag. Feature 030 bleibt der einzige nächste Intake; es wurde kein Feature-Verzeichnis angelegt und kein autonomer Lauf gestartet. |
| 2026-07-16 | Autonomer Feature-030-Evolutionsaudit mit echtem Hard-Abort-/Resume-Feldnachweis | Der read-only Lauf bindet `magiblot/tvision` an Commit `57b6f56b`, Tree `96dd0387` und den mehrteiligen COPYRIGHT-Hash. 50 ausgewählte Quellen unterstützen 48 Vertragsrelationen: 27 `CorroboratesOriginal`, 12 `CorroboratesModernization`, 6 `AlternativeModernization` und 3 `NotApplicable`. Die 48 `MB*`-Entscheidungen ergeben 39 `AlreadySatisfiedWithNewEvidence`, 6 `IntentionalDeviation` und 3 `RejectedComparison`; gemeinsam mit 48 `TGO*` entstehen 96 eindeutige `NonFinding`-Dispositionen, null `CF*`-Findings, null Ownergruppen und kein Hardening-Intake. `Lastenheft_16_Pre-Wave5-Wave6-Combined-Conformance-Closure.md` definiert Feature 031 als einzigen nächsten Lauf; beide Waves bleiben gesperrt. Der einmalige UI-Abbruch während Analyze-Remediation wurde durch read-only Status, verweigerte implizite Fortsetzung und expliziten Resume unter erneuerter `MergeAndSync`-Authority konsistent rekonstruiert; ein zweiter Abbruch ist ausgeschlossen. Der Vor-Statistik-Snapshot umfasst `+9172/-96`: 0 Produktionszeilen, 497 test-only Validatorzeilen, 8505 Feature-Artefaktzeilen sowie 170 weitere Evidence-, Intake-, Agent-, Status- und Metadatenzeilen. Konservative Manualreferenz für 9172 hinzugefügte oder aktualisierte Zeilen: `114,7` Tage beziehungsweise `894,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `73,4` Tage beziehungsweise `572,3` Stunden bei 125 Zeilen/Tag. Lokale Abnahme: gezielte Auditvalidatoren 37/37, vollständige Release-Suite 773/773, Coverage Core 92,96 %, Controls 86,66 %, Serialization 90,01 %, Compatibility 80,55 % und Drivers.Console 89,18 %, DocFX 0/0, Playwright/Axe 2/2, UTF-8-Lynx 3/3, Homogeneity 100 % und Secrets High 0. Produktcode, APIs, Dependencies, Beispiele, Consumer, historische und externe Quellen bleiben unverändert; Remote- und exakte Head-Nachweise folgen kausal im Feature-PR und nicht rekursiven Closeout. |
| 2026-07-16 | Kausaler Closeout des Feature-030-Hard-Abort-/Resume-Feldlaufs | Feature-PR #88 wurde auf dem finalen Head `28e17ff` als Merge `316d827` geliefert. Elf Primary-Gate-Zeilen waren an Requirements-Hash, Head, Workflow, Job, Plattform und Scope gebunden; der installierte Bash-Validator akzeptierte die ungetrackte Evidence. Ubuntu, macOS, Windows, DocFX/A11Y, Homogeneity, Supply Chain, PowerShell, Agent Secrets, Gitleaks und Claude waren grün; 22 Checks bestanden, der PR-unzutreffende Pages-Deploy wurde übersprungen. GraphQL meldete null Threads und Kommentare, Copilot blieb quota-bedingt ein fehlender Review und nur Human Approval erforderte den engen Bypass. Das verborgene Commitment wählte Index 8 für PR/Review; der frühere reale Abbruch in Analyze-Remediation supersedete diese Auswahl, ohne einen zweiten Abbruch auszulösen. Die Retrospektive klassifiziert v0.2.2 als `NoPromotion`: Status, Refusal, Authority-Revalidation und Resume funktionierten ohne portable Lücke, daher entstehen weder Home-Baseline-Branch noch Preset-PR. Der Vor-Statistik-Diff für Closeout, Retrospektive, terminale 165/165 Tasks und finalen Run-State umfasst vier Pfade mit `+265/-38`, also 227 Nettozeilen. Konservative Manualreferenz: `3,3` Tage beziehungsweise `25,8` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `2,1` Tage beziehungsweise `16,5` Stunden bei 125 Zeilen/Tag. State-/Gate-Validator, Diff, Secrets, DocFX 0/0, Playwright/Axe 2/2 und UTF-8-Lynx bilden die Abschlussgates; .NET wird mangels ausführbarer Closeout-Änderung nicht erneut ausgelöst. Feature 031 bleibt einziger nächster Intake; Wave 5 und Wave 6 bleiben blockiert. |
| 2026-07-16 | `/speckit-autonomous` für `031-combined-conformance-closure` | Der unabhängige evidence-only Abschluss bindet die gemergten Features 024, 025, 026, 028, 029 und 030 an sieben akzeptierte Vorgängerdateien und rekonstruiert 48 Verträge, 13 Consumer-Gruppen, 48 TGO- plus 48 MB-Beobachtungen, 96 `NonFinding`-Dispositionen, 13 geschlossene Findings sowie drei leere Ownergruppen. Free Vision 15/15, Terminal.GUI v1.9.0 25/25 und magiblot/tvision 50/50 wurden in sauberen detached `/tmp`-Checkouts gegen Commit-, Tag-, Tree-, Lizenz- und Source-Hashes verifiziert. Negative Tests lehnen fehlende oder doppelte IDs, unbekannte Dispositionen, neue Findings, Produktentscheidungen, Ownerbelegung, Kanten, Hardening-Intakes, Pin-/Quellenzahldrift, Governance-Lücken und verfrühte Wave-Marker fail-closed ab. Der Vorvalidierungs-Snapshot ohne diese Statistikzeile umfasst `+643/-0` test-only Validatorcode, `+8184/-40` Dokumentation/Evidence/Guidance und `+4/-4` Versions-/Feature-Metadaten, zusammen `+8831/-44` beziehungsweise 8787 Nettozeilen; Produktionscode, APIs, Dependencies, Pakete, Projekte, Beispiele, Consumer und historische Quellen bleiben unverändert. Konservative Manualreferenz für 8831 hinzugefügte oder aktualisierte Zeilen: `110,4` Tage beziehungsweise `861,1` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `70,6` Tage beziehungsweise `551,1` Stunden bei 125 Zeilen/Tag. Frühe Abnahme: repräsentativer Slice erwartungsgemäß Red bei Build 303 und 1/1 Green bei Build 304, externe Provenienz 90/90, Agent-Abschnitte 5/5 bytegleich und Home-Baseline-Homogeneity 100 %. Der reviewte Feature-Head hält Wave 5 und Wave 6 `BlockedPendingCausalClosure`; vollständige lokale, Remote-, Review- und Exact-Head-Gates sowie der kausale Closeout folgen im selben autonomen Lauf. |
| 2026-07-16 | Kausaler Closeout des gemeinsamen Feature-031-Konformitätsabschlusses | Feature-PR #90 wurde auf dem finalen Head `4e6a974` als Merge `3d64a36` geliefert. Zwölf Primary-Gate-Zeilen waren an Requirements-Hash, Head, Workflow, Job, Plattform und Befehl gebunden; der installierte Bash-Validator akzeptierte die ungetrackte Evidence. Der erste Windows-Run fand einen CRLF-abhängigen Hash im neuen test-only Validator; die begrenzte LF-Kanonisierung und ein direkter LF-/CRLF-Test bestanden lokal 9/9 und anschließend 782/782 auf Ubuntu, macOS und Windows. DocFX/A11Y, Homogeneity, Supply Chain, PowerShell, Agent Secrets, Gitleaks und Claude waren grün; 22 Checks bestanden, der PR-unzutreffende Pages-Deploy wurde übersprungen. GraphQL meldete null Threads und Kommentare, Copilot blieb quota-bedingt ein fehlender Review und nur Human Approval erforderte den engen Bypass. Der Closeout setzt Wave 5 auf `Eligible`, startet sie aber nicht; Wave 6 bleibt `ConditionallyReady`. Die Retrospektive ist `NoPromotion`, weil kein provider-neutraler Preset-Defekt reproduziert wurde. Der Vor-Statistik-Diff ohne Statistikdatei umfasst `+460/-250`, also 210 Nettozeilen in Evidence-, State-, Task-, Status-, Agent- und Retrospektivflächen ohne Runtime-, API-, Dependency-, Projekt-, Beispiel- oder Testlogikänderung. Konservative Manualreferenz: `5,8` Tage beziehungsweise `44,9` Stunden bei 80 hinzugefügten oder aktualisierten Zeilen/Tag; Thorsten-Solo `3,7` Tage beziehungsweise `28,7` Stunden bei 125 Zeilen/Tag. Der Run-State steht auf `Retrospective`, `Completed`, 172/172 und `nextExactAction: N/A`; Feature 032 wurde nicht angelegt. |
| 2026-07-16 | Wave-5-Intake und gebündelte Community-Preset-Submission vorbereitet | Den zurückgestellten Community-Katalogabgleich als einzelnen englischen Folgeissue [github/spec-kit#3569](https://github.com/github/spec-kit/issues/3569) für `autonomous-run-governance` v0.2.2 eingereicht: 13 Templates, fünf Commands, vier Bash-/PowerShell-Validatoren sowie synthetische und reale Hard-Abort-/Resume-, Exact-Head- und Closeout-Feldnachweise. Das repository-eigene Trigger-Label bleibt wegen externer Contributor-Berechtigung eine nicht blockierende Maintainer-Aktion; kein doppelter Issue und kein künstliches v0.2.3-Release entstehen. `Lastenheft_17_Wave5-TP7-Functional-Porting.032-wave5-tp7-functional-porting.md` reserviert anschließend Feature 032 als erste von zwei Wave-5-Stufen: 15 read-only Pascal-Quellen, sechs geschlossene Consumer-Gruppen, vorhandene Framework-Verträge, startbare moderne C#-Beispiele sowie reale App-Loop-, Zustands-, View-, Buffer-/Cell-, Keyboard-, A11Y-, Guide- und Evidence-Proofs. Die spätere Showcase-Stufe entsteht ausschließlich aus der gelieferten 032-Delta-Matrix; Wave 6 bleibt `ConditionallyReady`. Der Vor-Statistik-Snapshot umfasst `+608/-49`, also 559 Nettozeilen in Lastenheft, Reihenfolge, Pflichtenheft und vier synchronen Agentenflächen ohne Runtime-, API-, Dependency-, Projekt-, Beispiel-, Test- oder historische Source-Änderung. Konservative Manualreferenz: `7,6` Tage beziehungsweise `59,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo `4,9` Tage beziehungsweise `37,9` Stunden bei 125 Zeilen/Tag. |
| 2026-07-16 | `/speckit-autonomous` für `032-wave5-tp7-functional-porting` | Die funktionale erste Wave-5-Stufe ordnet alle 15 read-only `TVDEMOS/*.PAS`-Quellen genau einer Rolle zu und liefert sechs `UseExistingFramework`-Consumer über zehn startbare moderne C#-Beispiele. Calculator, Demo/Editor/Help, Resource-Generator/-Load, ASCII, Calendar, Puzzle und Mouse Capability/Fallback werden test-first beziehungsweise mit realen `app.Run()`-, Zustands-, View- und Cell-Proofs belegt. Datei-, Resource-, Help-, Generator- und Mausgrenzen bleiben kontrolliert; Hostzustand, beliebige Benutzerdaten, Dependencies, historische Quellen und breite Framework-Verträge bleiben unverändert. Die exakte 15/6/10/10-Matrix erzeugt `Lastenheft_18_Wave5-TP7-Showcase-Remediation.md` als nächsten Intake, ohne Feature 033 oder Wave 6 zu starten. Der Vor-Statistik-Snapshot ohne diese Statistikzeile umfasst `+1402/-0` Produktions-/Beispielcode, `+680/-0` Tests, `+3234/-81` Dokumentation/Evidence/Guidance und `+227/-4` Metadaten, zusammen `+5543/-85` beziehungsweise 5458 Nettozeilen. Konservative Manualreferenz für 5543 hinzugefügte oder aktualisierte Zeilen: `69,3` Tage beziehungsweise `540,4` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `44,3` Tage beziehungsweise `345,9` Stunden bei 125 Zeilen/Tag. Frühe Abnahme: Calculator 3/3, zentrale Apps 6/6, Resources 3/3, Domain/Mouse 6/6 und vollständige Wave-5-Matrix 3/3, zusammen 21/21. Vollständige Release-, Coverage-, DocFX-/A11Y-, Plattform-, Review- und Exact-Head-Gates folgen auf dem finalen Kandidaten. |
| 2026-07-16 | Kausaler Closeout der funktionalen Wave-5-Stufe Feature 032 | Feature-PR #93 wurde auf dem finalen Head `cf274c6` als Merge `e74c33d` geliefert. Elf Primary-Gate-Zeilen banden Requirements-Hash, Head, Workflow, Job, Plattform und Befehl; der installierte Bash-Validator akzeptierte die temporäre Evidence. Der erste Windows-Lauf fand eine CRLF-abhängige zusätzliche Leerzelle im neuen Markdown-Matrixparser. Die begrenzte Zeilenendenkanonisierung plus LF-/CRLF-Test bestand lokal 4/4, im vollständigen Wave-5-Filter 22/22 und anschließend als Teil von 804/804 Tests auf Ubuntu, macOS und Windows. Coverage blieb mit 92,96 %, 86,66 %, 90,01 %, 80,55 % und 89,18 % grün; DocFX/A11Y, Homogeneity, Supply Chain, PowerShell, Agent Secrets, Gitleaks und Claude bestanden. GraphQL meldete null Threads und Kommentare, Copilot blieb quota-bedingt ein fehlender Review und nur Human Approval erforderte den engen Bypass. Die Retrospektive ist `NoPromotion`, weil kein provider-neutraler Preset-Defekt reproduziert wurde. Der Closeout ändert ausschließlich Evidence-, State-, Task-, Statistik- und Retrospektivflächen, schließt 180/180 Aufgaben und lässt Feature 033 als nächsten Intake sowie Wave 6 blockiert. |
| 2026-07-17 | `/speckit-autonomous` für `033-wave5-tp7-showcase-remediation` | Die zweite Wave-5-Stufe schließt alle zehn aus Feature 032 abgeleiteten Showcase-Deltas mit vorhandenen Framework-Komponenten. Demo, Editor, Help, Resource Demo und Generator, ASCII-Tabelle, Calculator, Calendar, Puzzle und Mouse Dialog besitzen eine sichtbare Hauptkomponente, echte `TStatusLine`, per `F1` erreichbare zweisprachige Description, vollständige Tastaturpfade und reproduzierbare Normal-/Constrained-App-Loop-/View-/Cell-Proofs. Alle zehn Entscheidungen lauten `UseExistingFramework`; `TVDEMOS/` bleibt 15/15 blob-identisch, Hostzustand und beliebige Benutzerdaten bleiben geschützt, und es entstehen weder neue Dependency noch breiter Framework-Fix. Der Vorvalidierungs-Snapshot ohne diese Statistikzeile umfasst `+1067/-44` Produktions-/Beispielcode, `+768/-1` Tests, `+2326/-252` Dokumentation/Evidence/Guidance und `+3/-3` Metadaten, zusammen `+4164/-300` beziehungsweise 3864 Nettozeilen. Konservative Manualreferenz für 4164 hinzugefügte oder aktualisierte Zeilen: `52,1` Tage beziehungsweise `416,4` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `33,3` Tage beziehungsweise `266,5` Stunden bei 125 Zeilen/Tag. Frühe Abnahme: Calculator 6/6, zentrale Apps 10/10, Resources 4/4, Domain/Mouse 14/14 und exakte Showcase-Matrix 6/6. Vollständige Release-, Coverage-, DocFX-/A11Y-, Plattform-, Review- und Exact-Head-Gates folgen auf dem finalen Kandidaten. Wave 6 und Feature 034 bleiben bis zum Merge und zur Prüfung des tatsächlichen kombinierten Wave-5-Deltas blockiert. |
| 2026-07-17 | Kausaler Closeout der Wave-5-Showcase-Stufe Feature 033 | Feature-PR #96 wurde auf dem finalen Head `8921bd3` und Tree `5d4e676` als Merge `d476e63` geliefert. Zwölf Primary-Gate-Zeilen banden den Requirements-Hash `90cbcd1b`, Head, Workflow, Job, Plattform und ausgeführten Scope; Bash und PowerShell akzeptierten die temporäre Exact-Head-Evidence. 826/826 Tests bestanden auf Ubuntu, macOS und Windows; die lokale Coverage lag für alle fünf Pflichtassemblies über 70 %. DocFX 0/0, Playwright/Axe 2/2, Homogeneity, Supply Chain, PowerShell, Agent Secrets, Gitleaks und Claude waren grün. Insgesamt bestanden 22 Checks; nur der für Pull Requests nicht anwendbare Pages-Deploy wurde übersprungen. GraphQL meldete null Threads und Kommentare, Copilot blieb quota-bedingt ein fehlender Review und ausschließlich Human Approval erforderte den ausdrücklich autorisierten engen Bypass. Der Run-State endet mit `Retrospective`, `Completed`, 196/196 und `nextExactAction: N/A`; die Retrospektive ist `NoPromotion`. Der Vor-Statistik-Diff umfasst `+270/-96`, also 174 Nettozeilen ausschließlich in Evidence-, State-, Task-, Status- und Agentflächen. Konservative Manualreferenz: `3,4` Tage beziehungsweise `26,3` Stunden bei 80 hinzugefügten oder aktualisierten Zeilen/Tag; Thorsten-Solo: `2,2` Tage beziehungsweise `16,8` Stunden bei 125 Zeilen/Tag. Der Closeout löst mangels ausführbarer Änderung keinen erneuten .NET-Lauf aus. Wave 5 ist vollständig geliefert; Wave 6 und Feature 034 bleiben bis zur separaten Prüfung des tatsächlichen kombinierten Wave-5-Deltas blockiert. |
| 2026-07-17 | Wave-5 Combined-Delta-Closure als Feature 034 vorbereitet | `Lastenheft_19_Wave5-Combined-Delta-Closure.md` reserviert den nächsten autonomen Lauf als read-only Audit des tatsächlichen Produktdeltas aus Feature 032 PR #93 und Feature 033 PR #96. Der Vertrag trennt die kausalen Closeouts #94/#97 und die Prompt-Metadaten #95 vom Produktdelta, verlangt exakte 15/6/10/10/10-Cardinalities, genau eine kombinierte Disposition je Beispiel, eine explizite Prüfung gemeinsamer Wave-5-Sonderlogik sowie positive und negative test-only Closure-Validierung. Produkt-, API-, Dependency-, Projekt-, Beispiel-, Framework-, historische und externe Quellen bleiben im Audit unverändert. Bei null Findings darf Feature 034 einen späteren Wave-6-Intake für Feature 035 ableiten, ihn aber nicht starten. Der Intake umfasst rund 524 neue beziehungsweise aktualisierte Dokumentationszeilen; konservative Manualreferenz etwa `6,6` Tage beziehungsweise `51,1` Stunden bei 80 Zeilen/Tag, Thorsten-Solo etwa `4,2` Tage beziehungsweise `32,7` Stunden bei 125 Zeilen/Tag. |
| 2026-07-17 | `/speckit-autonomous` für `034-wave5-combined-delta-closure` | Der read-only Audit rekonstruiert fünf exakte PR-Dateimengen, bindet sieben Vorgängerartefakte über SHA-256 und bestätigt alle 15 historischen Quellen über feste Git-Blobs. Zehn Funktionsproofs, zehn Showcase-Abschlüsse, zehn Guide-/Startpfade und zehn kombinierte Beispielzeilen sind sechs Consumer-Gruppen vollständig zugeordnet. Alle zehn Entscheidungen lauten `AcceptedIntentionalDeviation`; es gibt null offene `Gap`-Dimensionen, null `CandidateFinding`, null `ProductDecision`, null Owner-Gruppen und null Hardening-Intakes. `Wave5Application`, `Wave5ConsoleHost`, `Wave5StatusLine` und `Wave5GridView` bleiben begrenzte Beispielkomposition über bestehenden TuiVision-Verträgen. Der test-only Validator besteht lokal 10/10 und verwirft Pin-, Hash-, Source-, Consumer-, Relation-, Proof-, Governance-, Finding-, Wave- und LF/CRLF-Mutationen fail-closed. Der Vorvalidierungs-Snapshot umfasst ungefähr `+4313/-60` Zeilen einschließlich Spezifikation, Evidence, JSON-Datensatz und 753 Zeilen test-only Validator; Produkt-, API-, Dependency-, Projekt-, Beispiel-, Framework- und historische Quellen bleiben unverändert. Konservative Manualreferenz für 4313 hinzugefügte oder aktualisierte Zeilen: `53,9` Tage beziehungsweise `431,3` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `34,5` Tage beziehungsweise `276,0` Stunden bei 125 Zeilen/Tag. Wave 5 und Wave 6 bleiben am Feature-Head bis Remote-, Review-, Merge- und kausalem Evidence-Abschluss gesperrt; Feature 035 wurde nicht gestartet. |
| 2026-07-17 | Kausaler Closeout des Wave-5 Combined Delta Feature 034 | Feature-PR #99 wurde auf dem finalen Head `016692d`, Tree `ae1218a2` und Version `1.34.4.358` als Merge `7fb52e2` geliefert. Zwei frühe Windows-Runs deckten nacheinander CRLF-abhängige Vorgänger-SHA-256- und historische Git-Blob-Rekonstruktion im test-only Validator auf. Die vollständige checkout-neutrale Korrektur bestand lokal 11/11 und 837/837 sowie anschließend auf Ubuntu, macOS und Windows. Dreizehn Exact-Head-Gate-Zeilen banden Requirements-Hash `f4810ce8`, Head, Workflow, Job, Plattform und Befehl; Bash und PowerShell akzeptierten sie. 22 Checks, DocFX 0/0, Playwright/Axe 2/2, Supply Chain, Secrets, Homogeneity und Claude waren grün; Copilot blieb quota-bedingt ein fehlender Review, GraphQL meldete null Threads und Kommentare, und nur Human Approval benötigte den autorisierten engen Bypass. Der Closeout setzt Wave 5 auf `Closed`, Wave 6 auf `EligibleForIntake` und erstellt `Lastenheft_20_Wave6-TVFM-Functional-Porting.md` mit kopierbaren Specify-/Autonomous-Prompts; Feature 035 wurde nicht gestartet. Die Retrospektive ist `NoPromotion`, weil die Zeilenendengrenze feature-eigene Testlogik und keinen providerneutralen Preset-Defekt betraf. Der Vor-Statistik-Diff ohne diese Zeile umfasst ungefähr `+824/-129`, also 695 Nettozeilen in Evidence-, State-, Task-, Status-, Agent- und Intake-Flächen. Konservative Manualreferenz: `10,3` Tage beziehungsweise `80,4` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `6,6` Tage beziehungsweise `51,4` Stunden bei 125 Zeilen/Tag. Der Evidence-only Closeout löst keine erneute .NET-Validierung aus. |
| 2026-07-17 | `/speckit-autonomous` für `035-wave6-tvfm-functional-porting` | Die funktionale Wave-6-Stage 1 liefert `Tp7FileManager` und die gemeinsame `TuiVision.Examples.Wave6`-Schicht. Alle 24 read-only `TVFM/`-Quellen sind einer modernen Rolle zugeordnet; zehn Funktionsbereiche decken Navigation, Liste, Filter, Sortierung, Tagging, Text-/Hexvorschau, Suche, interne Viewerwahl, Dateioperationen, Drag-/Drop-Intent und geschlossene Paletten ab. Der Workspace bindet alle Pfade an eine kanonische kopierte Lernwurzel, lehnt absolute Pfade, `..`, Links und Reparse-Segmente ab, begrenzt Vorschau auf 4 KiB/80 Zeilen sowie Suche auf Tiefe 8, 256 Dateien und 100 Treffer. Mutierende Pfade verwenden Einmal-Intents, explizite Bestätigung und Revalidierung; Maus bereitet nur dieselbe bestätigungspflichtige Absicht wie die Tastatur vor. Der aktuelle Kandidaten-Snapshot umfasst 42 Pfade mit etwa `+4382/-68`: Produktions-/Beispielcode `+1195`, Tests `+793`, Dokumentation/Evidence/Guidance `+2179` und Metadaten `+215/-4`. Konservative Manualreferenz für 4382 hinzugefügte oder aktualisierte Zeilen: `54,8` Tage beziehungsweise `427,2` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `35,1` Tage beziehungsweise `273,4` Stunden bei 125 Zeilen/Tag. Frühe lokale Abnahme: Wave-6-Zieltests 22/22 bei Version `1.35.0.367`. Vollständige Release-, Coverage-, DocFX-/A11Y-, Supply-Chain-, Secret-, Plattform-, Review- und Exact-Head-Gates folgen auf dem finalen Kandidaten. Feature 036, Stage-2-Showcase-Remediation und Post-Wave-6-Audit werden nicht gestartet. |
| 2026-07-17 | Wave-6 TVFM Showcase Remediation als Feature 036 vorbereitet | `Lastenheft_21_Wave6-TVFM-Showcase-Remediation.036-wave6-tvfm-showcase-remediation.md` leitet den nächsten Intake ausschließlich aus der einen akzeptierten Feature-035-`ShowcaseDelta`-Zeile ab. Genau ein Einstiegspunkt und zehn Showcase-Bereiche erhalten sichtbare Menü-/Control-Zugänge, sichere fokussierbare Dateioperationsdialoge, echte StatusLine, F1-Description, vollständige Tastaturpfade, begrenzte Mausparität, normale sowie constrained App-Loop-/View-/Cell-Proofs und ein geschlossenes Evidence-Modell. Die in Feature 035 bewiesenen Root-, Pfad-, Such-, Vorschau-, Viewer-, Intent- und Mutationsverträge bleiben unverändert; `TVFM/`, `TVDEMOS/` und `tv203s/` bleiben read-only. Das Lastenheft enthält kopierbare Specify- und Autonomous-Prompts, reserviert Branch `036-wave6-tvfm-showcase-remediation` und startet weder Feature 036 noch Feature 037, den unabhängigen Wave-6-Abschluss oder den Post-Wave-6-Audit. |
| 2026-07-17 | `/speckit-autonomous` für `036-wave6-tvfm-showcase-remediation` | Die sichtbare Wave-6-Stage 2 verwendet den bestehenden Feature-035-Workspace unverändert. Ein persistentes `TWindow` mit fokussierbarer `TListBox`, sechs Menügruppen, begrenzten Preview-/Such-/Palette-Pfaden, echten Copy-/Rename-/Delete-/Read-only-Dialogen, StatusLine, F1-Description, Tastatur und optionalem nicht mutierendem Mouse-Intent ist in normaler und `48x16`-Komposition belegt. Die exakte Evidence schließt zehn `W6S`-Zeilen und einen `ShowcaseComplete`-Einstiegspunkt; alle 24 read-only TVFM-Dateien stimmen per SHA-256. Frühe lokale Abnahme: 23/23 Showcase-/Funktions-Smokes und 4/4 Evidence-Validatoren bei Version `1.36.0.392`. Der vorläufige Kandidatenumfang umfasst etwa 694 hinzugefügte oder wesentlich aktualisierte Beispielcodezeilen, 996 Testzeilen und 2637 Dokumentations-/Evidence-/Guidance-Zeilen, zusammen etwa 4327 Zeilen. Konservative Manualreferenz: rund `54,1` Tage beziehungsweise `421,9` Stunden bei 80 Zeilen/Tag; Thorsten-Solo rund `34,6` Tage beziehungsweise `270,0` Stunden bei 125 Zeilen/Tag. Vollständige Release-, Coverage-, DocFX-/A11Y-, Plattform-, Review- und Exact-Head-Gates folgen. Feature 037, unabhängiger Wave-6-Abschluss und Post-Wave-6-Audit wurden nicht gestartet. |
| 2026-07-17 | Kausaler Closeout der Wave-6-Showcase-Stufe Feature 036 | Feature-PR #104 wurde nach der Windows-Korrektur auf dem finalen Head `a0d5062` als Merge `559bffb` geliefert. Der erste Windows-Lauf fand eine checkout-abhängige CRLF-Abweichung im test-only Hashnachweis historischer `.PAS`-/`.BAT`-Texte; die begrenzte Kanonisierung bewahrt `.PAL`-/`.TVR`-Ressourcen bytegenau und bestand anschließend 35/35 betroffene sowie 880/880 vollständige Release-Tests. Die lokale Coverage lag mit 92,96 %, 86,66 %, 90,01 %, 80,55 % und 89,18 % über allen fünf 70-%-Gates; DocFX 0/0 und Playwright/Axe 2/2 bestanden. Der exakte Head erreichte 22 erfolgreiche technische Checks bei einem erwarteten übersprungenen Pages-Deploy. Zwölf Primary-Gate-Zeilen wurden von Bash und PowerShell akzeptiert; Claude war grün, GraphQL meldete null Threads und Kommentare, Copilot blieb quota-bedingt ein fehlender Review, und ausschließlich Human Approval benötigte den autorisierten engen Bypass. Der Run-State endet mit `Retrospective`, `Completed`, 187/187 und `nextExactAction: N/A`; die Retrospektive ist `NoPromotion`. Der Evidence-only Closeout löst keine erneute .NET-Validierung aus. Feature 037, unabhängiger Wave-6-Abschluss und Post-Wave-6-Audit wurden nicht gestartet. |
| 2026-07-23 | Intake Authoring und Review | Fünf aktive Lastenhefte als hashgebundene Legacy-Intakes normalisiert; der frühere Post-Wave-6-Einzelreview bleibt als supersedierte Evidence erhalten, der Serienreview endet `Ready` ohne offene Findings. |
| 2026-08-02 | Runnerprofil und Windows-ripgrep gehärtet | Documentation Impact `GeneratedUpdate`: TuiVision bleibt als öffentliches Referenz-Repository vollständiger Linux-/macOS-/Windows-Canary der generisch verteilten Wartungsworkflows. Der Windows-Homogeneity-Check ersetzt den fehlertoleranten Chocolatey-Bezug durch das offizielle, auf Version 15.2.0 fixierte ripgrep-Archiv mit SHA-256-Prüfung und explizitem `rg --version`. Produktcode, öffentliche API, Bedienung und Produktdokumentation bleiben unverändert; die Projektstatistik wird aus Git-Historie und Konfiguration neu erzeugt. |
| 2026-08-09 | `/speckit.implement` für `038-example-portfolio-conformance-audit` | Der strikt read-only Produktaudit erfasst exakt 37 Beispielzeilen, 138 hashgebundene Quellen, 128 reziproke Evidence-Knoten, zehn Dimensionen je Beispiel und 24 vollständige Governance-/Standardsentscheidungen. Ein test-only C#-Validator verwirft exakt 46 kanonische Ein-Ursachen-Fixtures mit stabilen EPA-Codes; die eingefrorene Finding-Menge ist leer, alle vier Owner-Gruppen sind unterdrückt, und genau ein unnummerierter Closure-Intake besitzt ein beidseitig validiertes Schema-2.0-Receipt, ohne ein Folgefeature zu starten. Der Vor-Statistik-Snapshot umfasst 848 Testzeilen, 5041 Dokumentations-/Evidence-/Intake-Zeilen und vier ausgleichende Metadatenänderungen, zusammen 5893 hinzugefügte oder aktualisierte Zeilen bei 5889 Nettozeilen; Runtime-Routing-Ausgaben und generierte Test-/Coverage-/DocFX-Artefakte sind ausgeschlossen. Konservative Manualreferenz: `73,7` Tage beziehungsweise `574,6` Stunden bei 80 Zeilen/Tag; Thorsten-Solo: `47,1` Tage beziehungsweise `367,7` Stunden bei 125 Zeilen/Tag. Lokale Abnahme: Audit 52/52, Example Smokes 298/298, vollständige Regression 940/940, null Skips und Coverage Core 92,85 %, Controls 85,54 %, Serialization 87,74 %, Compatibility 80,95 % sowie Drivers.Console 88,78 %. Produktcode, öffentliche API, Dependencies, Projekte, Pakete, Beispiele und historische Quellen bleiben unverändert; Remote-Delivery, Merge und Retrospektive folgen getrennt. |

## Statistikprofil-1-Archiv / Statistics Profile 1 Archive
Basis dieses Schlussblocks ist der Repository-Snapshot vom 2026-07-15. Die
Werte schließen den aktuellen Working Tree ein und bleiben deshalb bis zum
nächsten Statistiklauf eine dokumentierte Momentaufnahme.

| Kennzahl | Verdichteter Gesamtblick |
|---|---:|
| Artefaktbasis gesamt | 266898 Zeilen |
| Produktions- und Testcode zusammen | 52118 Zeilen (19.5 %) |
| Dokumentationsanteil | 214310 Zeilen (80.4 %) |
| Spec-Kit-Anteil innerhalb der Doku | 59189 Zeilen (27.6 %) |
| Zentrale Governance-/Agent-Dateien | 3952 Zeilen (1.8 % der Doku) |
| Projektgebundene Agent-Skills | 2512 Zeilen (1.2 % der Doku) |
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-07-15 |
| Git-Commits / sichtbare Aktivtage | 524 / 72 |
| Gesamtzeilen pro sichtbarem Aktivtag | 3700.4 |
| Gesamtzeilen pro Commit | 508.4 |
| Konservative Einzelentwickler-Untergrenze | 3330.3 Arbeitstage / 25976.5 Stunden |
| Thorsten-Solo-Untergrenze | 2131.4 Arbeitstage / 16624.9 Stunden |
| Kleines 3er-Team mit Koordinationsaufschlag | 1332.1 Arbeitstage |
| Repo-Speedup gegen 80-Zeilen-Referenz | 46.3x |
| Repo-Speedup gegen Thorsten-Referenz | 29.6x |

Die hohe Dokumentationsquote enthält die umfangreiche Secure-Development-,
Governance- und Spec-Kit-Basis. Das autonome Runbook und der neue
Orchestrierungs-Skill machten den in 018 wiederholten Ablauf reproduzierbar;
die neuen Parser-, Persistenz- und Integrationsnachweise bleiben dabei eng am
Feature-Scope. Die Speedups vergleichen sichtbaren Umfang mit
Git-Aktivtagen und sind keine Messung persönlicher Arbeitszeit.

The high documentation share includes the extensive secure-development,
governance, and Spec-Kit baseline. The autonomous runbook and orchestration
skill made the workflow repeated in 018 through 023 reproducible, while Feature
023 keeps semantic text, keyboard proof and native-bridge boundaries explicit.
The public preset adoption proves the stackable package without removing the
still necessary TuiVision override. The feature-024 intake inserts a
source-governed conformance gate before Wave 5 without pre-creating remediation
scope. Speedups compare visible scope with Git active days and do not measure
personal time.

Feature 027 remains valid historical closure evidence. Consumer Review Revision
2 adds stronger real-path and consumer-completeness criteria, routes 13 findings
to Features 025 and 026, and requires Feature 028 before Wave 5 or Wave 6.
Feature 025 closes its nine core findings. Feature 026 closes the four
component/data findings. Feature 028 independently closes the existing
TV203/Free Vision gate as `ReadyForTerminalGuiAudit`; Feature 029 is now the sole
immediate intake and adds the pinned Terminal.GUI v1.9.0 comparison. Feature
030 then audits pinned magiblot/tvision as a direct-lineage modernization
witness, deduplicates both observation sets, and reserves finding-derived work
from Feature 031 before either example wave.

### ASCII-Diagramme

```text
Artefaktmix nach Snapshot (Zeilen)
Produktion     | ####                          |  30604 | 11.5 %
Tests          | ###                           |  21514 |  8.1 %
Dokumentation  | ##############################| 214780 | 80.5 %
```

Die Balken verwenden dieselbe Skala. Die Zahlen bleiben der genaue,
textorientierte Nachweis für Screenreader und Braille-Zeilen.

The bars use one shared scale. The numbers remain the exact text-first evidence
for screen readers and Braille displays.

```text
Branch-/Phasenvolumen nach dokumentierter Netto-Basis (Zeilen)
0 main  | #################### | 10211
1 001   | #########            |  4353
2 002   | #####                |  2274
3 003   | #######              |  3572
4 004   | #########            |  4747
5 005   | ###################  |  9659
6 006   | ###########          |  5796
7 007v  | #                    |    35
8 007ex | #############        |  6778
9 008   | ###                  |  1550
10 009  | #                    |   231
11 010  | #################### | 12169
12 011  | ######               |  3303
13 012p | ###                  |  1604
14 012i | ####                 |  2219
15 013i | ####                 |  2105
16 014i | #                    |   556
17 015i | #                    |   283
18 016i | ####                 |  2659
19 017i | #####                |  3493
20 auto | #                    |   595
21 018i | #####                |  2912
22 019i | #####                |  3392
23 020i | #####                |  3584
24 021i | #######              |  4617
25 022i | ######               |  3713
26 022c | #                    |    75
27 022r | #                    |    58
28 023i | ####                 |  2370
29 023c | #                    |    72
30 023r | #                    |   112
31 adopt| ##                   |  1212
32 agy  | #                    |    51
33 024p | #                    |   390
34 024i | ################     |  9863
35 024c | #                    |     8
36 024r | #                    |    59
37 027p | #                    |   243
38 027i | ##                   |  1130
39 027c | #                    |    31
40 024v2| ###                  |  1701
41 025i | ######               |  5248
42 026i | ######               |  4919
43 026c | #                    |   134
44 v014 | ##                   |  1048
45 intake| #                   |   395
46 v020 | ###                  |  1542
47 028i | ####                 |  2422
```

Feature 017 kombiniert einen kleinen beispielinternen Runtime-Anteil mit einer
vergleichbar großen Spec-Kit-, Evidence-, Test- und Guide-Fläche. Der
anschließende `auto`-Block standardisiert den dabei erprobten Ablauf ohne
Runtime- oder Testcodeänderung.

Feature 018 kombiniert einen begrenzten Framework-Fix mit test-first Parser-,
Persistenz- und Integrationsnachweisen sowie vollständiger Spec-Kit-Evidence.

Feature 017 combines a small example-internal runtime part with a similarly
large Spec-Kit, evidence, test, and guide surface. The following `auto` block
standardizes the proven workflow without runtime or test-code changes.

Feature 018 combines a bounded framework fix with test-first parser,
persistence, and integration proof plus complete Spec-Kit evidence.

Feature 019 combines five visible applications with app-loop, view, cell,
status, description, controlled-I/O, and constrained-viewport proof.

Feature 020 combines bounded host input, focus/activation, double-click and one
window drag with deterministic Driver, Controls, and app-loop proof.

Feature 021 combines bounded terminal state and parser recovery with KOI8-R,
raw-font metadata, closed profiles, and deterministic view/cell proof.

Feature 022 combines five visible terminal-adjacent demos with deterministic
state/view/cell proof, immutable resource manifests, and honest host fallbacks.

Feature 023 combines opt-in semantic text, nested focus announcements,
structured shortcuts, explicit high contrast, a complete keyboard inventory
and one visible reference application with state/view/cell proof.

Die Adoptionsphase installiert das öffentlich veröffentlichte Preset als
siebte Schicht und hält den projektspezifischen Codex-Override bewusst fest.

The adoption phase installs the publicly released preset as the seventh layer
and intentionally retains the project-specific Codex override.

Der Antigravity-Folgeabgleich richtet die aktiven Agenten- und CI-Flächen auf
`agy` aus, ohne historische Gemini-Kompatibilität zu löschen.

The Antigravity follow-up aligns active agent and CI surfaces with `agy`
without deleting historical Gemini compatibility.

Die 024-Intake-Vorbereitung setzt das historische Vergleichs- und Closure-Gate,
ohne bereits Remediation oder Wave-5-Verhalten zu implementieren.

The 024 intake preparation establishes the historical comparison and closure
gate without implementing remediation or Wave-5 behavior.

Feature 024 selbst liefert eine vollständige, maschinenprüfbare Vertragsmatrix
und trennt Primärentscheidung, Free-Vision-Gegenprüfung und Finding-Routing. Die
leeren Finding-Mengen verhindern bewusst inhaltslose Features 025 und 026.

Feature 024 itself delivers a complete machine-verifiable contract matrix and
keeps primary decisions, Free Vision corroboration, and finding routing
separate. Empty finding sets intentionally suppress content-free Features 025
and 026.

Revision 2 retains that historical run evidence but checks the real Wave-5 and
Wave-6 consumers. It produces 13 bounded findings and three ordered requirements
documents without starting a runtime implementation or example port.

Closeout und Retrospektive bestätigen die bestehenden Autonomiegrenzen. Der
einzige portable Defekt wurde in Home Baseline korrigiert; TuiVision benötigt
keine neue lokale Orchestrierungsregel.

Closeout and retrospective confirm the existing autonomy boundaries. The only
portable defect was corrected in Home Baseline; TuiVision needs no new local
orchestration rule.

Die 027-Intake-Vorbereitung macht aus dem Audit einen messbaren, verpflichtenden
Closure-Vertrag, ohne bereits Wave 5 oder eine Runtime-Änderung zu beginnen.

The 027 intake preparation turns the audit into a measurable mandatory closure
contract without starting Wave 5 or a runtime change.

Feature 025 combines bounded runtime contracts, real-path red/green tests,
machine-readable audit closure, public API documentation, and learner evidence
without starting either example wave.

Feature 026 combines hierarchical validation, state-preserving rejection,
mode-aware file decisions, allowlisted UI-description persistence, and
real-path red/green tests without starting either example wave.

The Feature-026 closeout records the corrected Windows runtime proof and the
reusable need for machine-checkable acceptance-gate execution mapping.

```text
Konservative Handarbeits-Referenz je dokumentierter Phase
0 main  | #################    | 127.6 d
1 001   | #######              |  54.4 d
2 002   | ####                 |  28.4 d
3 003   | ######               |  44.7 d
4 004   | ########             |  59.3 d
5 005   | ################     | 120.7 d
6 006   | ##########           |  72.5 d
7 007v  | #                    |   0.4 d
8 007ex | ###########          |  84.7 d
9 008   | ###                  |  19.4 d
10 009  | #                    |   2.9 d
11 010  | #################### | 152.1 d
12 011  | #####                |  41.3 d
13 012p | ###                  |  20.1 d
14 012i | ####                 |  27.7 d
15 013i | ###                  |  26.3 d
16 014i | #                    |   7.0 d
17 015i | #                    |   3.5 d
18 016i | ####                 |  33.2 d
19 017i | #####                |  43.7 d
20 auto | #                    |   7.4 d
21 018i | #####                |  36.4 d
22 019i | #####                |  42.4 d
23 020i | #####                |  44.8 d
24 021i | #######              |  57.7 d
25 022i | ######               |  46.4 d
26 022c | #                    |   0.9 d
27 022r | #                    |   0.7 d
28 023i | ####                 |  29.6 d
29 023c | #                    |   0.9 d
30 023r | #                    |   1.4 d
31 adopt| ##                   |  15.2 d
32 agy  | #                    |   0.9 d
33 024p | #                    |   4.9 d
34 024i | #################### | 123.3 d
35 024c | #                    |   0.1 d
36 024r | #                    |   0.7 d
37 027p | #                    |   3.0 d
38 027i | ##                   |  14.4 d
39 027c | #                    |   0.3 d
40 024v2| ###                  |  21.3 d
41 025i | #########            |  65.6 d
42 026i | ########             |  61.5 d
43 026c | #                    |   1.7 d
47 028i | ####                 |  30.3 d
```

Die Referenz rechnet den Netto-Phasenumfang mit 80 Zeilen pro Arbeitstag. Für
016 zeigt der Balken die kumulative Netto-Basis; für `auto` zeigt er die
Nettoänderung vor Statistikpflege. Der Ledger nennt zusätzlich die jeweils
hinzugefügten oder aktualisierten Zeilen.

The reference converts net phase scope at 80 lines per workday. For 016, the
bar shows the cumulative net baseline; for `auto`, it shows the net change
before statistics maintenance. The ledger also records added or updated lines.

```text
Dokumentierte Beschleunigungsfaktoren durch agentische KI + Spec-Kit/SDD
Repo 80 | ############################## | 46.3x
Repo125 | ###################            | 29.6x
014i    | #####                          |  7.0x
015i-80 | ####                           |  5.4x
015i125 | ###                            |  3.4x
016i-80 | ###################            | 24.2x
016i125 | ############                   | 15.5x
017i-80 | ############################## | 46.0x
017i125 | ###################            | 29.4x
AUTO-80 | #####                          |  7.8x
AUTO125 | ###                            |  5.0x
018i-80 | #########################      | 36.9x
018i125 | ################               | 23.6x
019i-80 | #############################  | 42.8x
019i125 | ###################            | 27.4x
020i-80 | ############################## | 45.1x
020i125 | ###################            | 28.8x
021i-80 | ############################## | 58.0x
021i125 | ###################            | 37.1x
022i-80 | ########################       | 46.5x
022i125 | ###############                | 29.8x
022c-80 | #                              |  0.9x
022c125 | #                              |  0.6x
022r-80 | #                              |  0.8x
022r125 | #                              |  0.5x
023i-80 | ####################           | 30.0x
023i125 | #############                  | 19.2x
023c-80 | #                              |  0.9x
023c125 | #                              |  0.6x
023r-80 | #                              |  1.5x
023r125 | #                              |  0.9x
ADOPT80 | ##########                     | 15.2x
ADOPT125| ######                         |  9.7x
AGY-80  | #                              |  0.9x
AGY125  | #                              |  0.6x
024P-80 | ####                           |  5.0x
024P125 | ###                            |  3.2x
024I-80 | ############################## | 123.6x
024I125 | ###################            | 79.1x
024C-80 | #                              |  0.1x
024C125 | #                              |  0.1x
024R-80 | #                              |  0.5x
024R125 | #                              |  0.3x
027P-80 | ##                             |  2.9x
027P125 | #                              |  1.9x
024V2-80| #################              | 24.9x
024V2125| ###########                    | 15.9x
025I-80 | ############################## | 68.5x
025I125 | ###################            | 43.8x
026I-80 | ############################   | 63.8x
026I125 | ##################             | 40.8x
026C-80 | #                              |  1.7x
026C125 | #                              |  1.1x
V14-80  | #########                      | 13.5x
V14125  | ######                         |  8.6x
028I-80 | ######################         | 33.0x
028I125 | ##############                 | 21.1x
```

Die 017-, AUTO-, 018-, 019-, 020-, 021-, 022-, 023-, ADOPT-, AGY- und
024P-, 024I-, 024C-, 024R-, 027P-, 024V2-, 025I-, 026I-, 026C-, V14- und
028I-Werte
beziehen sich jeweils auf einen sichtbaren autonomen Arbeitstag. Sie
beschreiben Lieferdichte, nicht die Dauer einzelner Denk-, Review-, CI- oder
Wartephasen.

The 017, AUTO, 018, 019, 020, 021, 022, 023, ADOPT, AGY, 024P, 024I, 024C,
024R, 027P, 024V2, 025I, 026I, 026C, V14, and 028I values each use one visible
autonomous workday. They describe delivery density, not the duration of
individual thinking, review, CI, or waiting phases.

```text
Vergleich Gesamtaufwand / sichtbares KI-Lieferfenster
Erfahren    | ############################## | 3330.3 d
Thorsten    | ###################            | 2131.4 d
KI sichtbar | #                              |   72.0 d
```

Der Direktvergleich stellt die konservative 80-Zeilen-Basis, die
Thorsten-Solo-Basis mit 125 Zeilen pro Tag und die sichtbaren Git-Aktivtage
nebeneinander.

The direct comparison places the conservative 80-line baseline, the
Thorsten-solo baseline at 125 lines per day, and visible Git active days next
to one another.

### X/Y-Verlauf

Die X-Achse hat feste Slots. Nicht belegte zukünftige Phasen bleiben leer; der
dritte Block hält bereits Platz bis Phase 47 frei.

The X-axis uses fixed slots. Unused future phases stay blank; the third block
already preserves space through phase 47.

```text
X/Y: Phasenvolumen 0..15 (Y ungefähr in Zeilen)
12000 |                      *         |
 9000 |*         *                     |
 6000 |            *   *               |
 3000 |  *   * *       *       *       |
    0 |    *     *   *   * * *   * * * |
      +--------------------------------+
       0 1 2 3 4 5 6 7 8 9 A B C D E F
```

```text
X/Y: Phasenvolumen 16..31 (Y ungefähr in Zeilen)
5000 |                *               |
3500 |      *   *     * * *           |
3000 |    *                           |
2300 |                        *       |
 1500 |                              * |
 600 |*       *                       |
 300 |  *                             |
   0 |                    * *   * *   |
     +--------------------------------+
      G H I J K L M N O P Q R S T U V
      16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
```

Die beiden Blöcke zeigen den Verlauf ohne eine zu breite Zeile. G bis J stehen
für die Phasen 16 bis 19, K für die autonome Standardisierung, L für Feature
018, M für Feature 019, N für Feature 020, O für Feature 021 und P für Feature
022, Q für dessen Closeout, R für dessen Retrospektive, S für Feature 023, T
für dessen Closeout, U für dessen Retrospektive und V für die öffentliche
Preset-Adoption.

The two blocks show progression without an overly wide line. G through J
represent phases 16 through 19, K represents autonomous standardization, L
represents Feature 018, M represents Feature 019, N represents Feature 020, O
represents Feature 021, P represents Feature 022, Q represents its closeout, R
represents its retrospective, S represents Feature 023, T represents its
closeout, U represents its retrospective, and V represents the public preset
adoption.

```text
X/Y: Phasenvolumen 32..47 (Y ungefähr in Zeilen)
10000 |    *                           |
 5000 |                  * *           |
 2000 |                *           *   |
 1000 |            *           *       |
  400 |  *                       *     |
  200 |          *                     |
   50 |*       *                       |
   10 |      *                         |
    0 |                                |
     +--------------------------------+
      W X Y Z a b c d e f g h i j k l
      32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47
```

W steht für den Antigravity-Folgeabgleich, X für die Intake-Vorbereitung, Y für
die Implementierung, Z für den Closeout, a für die Retrospektive des
Konformitätsaudits, b für die 027-Intake-Vorbereitung, c für die 027-Ausführung,
d für deren Closeout, e für Audit-Revision 2 samt 025-/026-/028-Intake-
Vorbereitung, f für Feature 025, g für Feature 026, h für dessen Closeout, i
für die v0.1.4-Preset-Adoption, j für die 028-/029-Terminal.GUI-Intake-
Vorbereitung, k für die v0.2.0-Preset-Adoption und l für den wiederaufgenommenen
Feature-028-Abschlusslauf.

W represents the Antigravity follow-up, X the conformance-audit intake, Y its
implementation, Z its closeout, a its retrospective, b the 027 intake
preparation, c the 027 run, d its closeout, e Audit Revision 2 plus the
025/026/028 intake preparation, f Feature 025, g Feature 026, h its closeout,
i the v0.1.4 preset adoption, j the 028/029 Terminal.GUI intake preparation, k
the v0.2.0 preset adoption, and l the resumed Feature 028 closure run.

## Gesamtstatistik / Overall Statistics

<!-- project-statistics-v2:begin -->

Profil 2 verwendet Git-getrackte Textdateien und sichtbare Git-Aktivitaet. Die Werte beschreiben Lieferdichte, keine persoenliche Arbeitszeit.

*Profile 2 uses Git-tracked text files and visible Git activity. The values describe delivery density, not personal working time.*

| Kennzahl / Metric | Wert / Value |
|---|---:|
| Textbasis / Text base | 624226 lines |
| Textdateien / Text files | 3177 |
| Beobachtbarer Zeitraum / Observable period | 2025-09-07..2026-08-30 |
| Aktivtage / Active days | 93 |
| Relevante Commits / Relevant commits | 565 |
| Zeilen je Aktivtag / Lines per active day | 6712.1 |
| Peak-Tag im Fenster / Peak day in window | 2026-03-22 / 321183 |
| Peak-Woche im Fenster / Peak week in window | 2026-03-22 / 373576 |
| Laengste Serie / Longest streak | 17 days |
| Speedup vs. 80 lines/day | 83.9x |
| Speedup vs. 125 lines/day | 53.7x |
| Methodik / Methodology | v2; source `f90d9d2a4511` |

### Artefaktmix / Artifact Mix

```text
Produktiv / Production          [####................]  22.4% | 139704
Tests                           [##..................]   8.0% | 50124
Dokumentation / Documentation   [#########...........]  43.4% | 270708
Skripte / Scripts               [#...................]   3.7% | 23193
Konfiguration / Configuration   [#...................]   6.5% | 40634
Daten und Medien / Data and media [#...................]   0.0% | 1
Sonstiger Text / Other text     [###.................]  16.0% | 99862
```

Die Balken teilen die aktuelle getrackte Textbasis in stabile Kategorien. Prozent und Zeilenwert sind die genaue, textorientierte Aussage.

*The bars split the current tracked text base into stable categories. Percentages and line counts provide the exact text-first result.*

### Tagesaktivitaet / Daily Activity

```text
Wochen / Weeks 01..26 | 2025-09-07..2026-03-07
So/Su  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 4 0 0 4
Mo/Mo  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
Di/Tu  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
Mi/We  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
Do/Th  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
Fr/Fr  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 4
Sa/Sa  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
```

```text
Wochen / Weeks 27..52 | 2026-03-08..2026-09-05
So/Su  4 0 4 4 0 4 0 0 4 4 0 0 4 0 4 0 0 0 4 4 4 1 4 4 0 4
Mo/Mo  0 4 4 4 0 1 4 0 4 0 0 0 1 0 2 0 3 1 4 4 0 0 0 2 0 -
Di/Tu  0 3 3 4 0 0 0 0 3 4 0 3 2 0 0 0 2 0 4 4 4 0 0 0 0 -
Mi/We  0 1 4 0 0 0 3 0 4 1 0 0 2 0 4 0 2 0 4 2 4 0 0 0 0 -
Do/Th  0 0 0 0 0 0 0 4 0 0 3 3 3 4 2 0 0 0 4 4 0 0 4 0 0 -
Fr/Fr  0 4 4 4 0 2 4 0 3 1 4 3 0 0 3 2 3 4 4 4 1 0 0 0 0 -
Sa/Sa  0 4 4 0 0 0 0 4 4 0 0 4 0 4 4 0 4 4 2 4 2 4 2 0 4 -
```

DE: 0 = keine Aenderung; 1 = 1..79; 2 = 80..399; 3 = 400..1599; 4 = 1600+ geaenderte Textzeilen; - = noch nicht abgelaufen.

*EN: 0 = no change; 1 = 1..79; 2 = 80..399; 3 = 400..1599; 4 = 1600+ changed text lines; - = not elapsed.*

### Wochenvolumen / Weekly Volume

```text
Wochen / Weeks 01..26 | 2025-09-07..2026-03-07
  cap 200000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      166667 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      133333 | . . . . . . . . . . . . . . . . . . . . . . # . . .
      100000 | . . . . . . . . . . . . . . . . . . . . . . # . . .
       66667 | . . . . . . . . . . . . . . . . . . . . . . # . . .
       33333 | . . . . . . . . . . . . . . . . . . . . . . # . . #
           0 +-----------------------------------------------------
```

```text
Wochen / Weeks 27..52 | 2026-03-08..2026-09-05
  cap 500000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      416667 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      333333 | . . # . . . . . . . . . . . . . . . . . . . . . . .
      250000 | . . # . . . . . # . . . . . . . . . . . . . . . . .
      166667 | . . # . . . . . # . . . . . . . . . . . . . . . . .
       83333 | . . # . . . . . # . . . . . . . . . # . . . . . . .
           0 +-----------------------------------------------------
```

Das Wochenvolumen zeigt Additionen plus Loeschungen. Es ist Aenderungsaktivitaet, nicht die aktuelle Groesse des Repositories.

*Weekly volume shows additions plus deletions. It represents change activity, not the current repository size.*

### Kumulative Entwicklung / Cumulative Development

```text
Wochen / Weeks 01..26 | 2025-09-07..2026-03-07
  cap 200000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      166667 | . . . . . . . . . . . . . . . . . . . . . . . . . #
      133333 | . . . . . . . . . . . . . . . . . . . . . . # # # #
      100000 | . . . . . . . . . . . . . . . . . . . . . . # # # #
       66667 | . . . . . . . . . . . . . . . . . . . . . . # # # #
       33333 | . . . . . . . . . . . . . . . . . . . . . . # # # #
           0 +-----------------------------------------------------
```

```text
Wochen / Weeks 27..52 | 2026-03-08..2026-09-05
 cap 2000000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
     1666667 | . . . . . . . . . . . . . . . . . . . . . . . . . .
     1333333 | . . . . . . . . . . . . . . . . . . . . . . . . . .
     1000000 | . . . . . . . . . . . . . . # # # # # # # # # # # #
      666667 | . . . # # # # # # # # # # # # # # # # # # # # # # #
      333333 | . . # # # # # # # # # # # # # # # # # # # # # # # #
           0 +-----------------------------------------------------
```

Die kumulative Kurve summiert nur das Brutto-Aenderungsvolumen im Fenster. Sie darf nicht als aktuelle Codebasis gelesen werden.

*The cumulative curve sums gross change volume within the window only. It must not be read as the current code base.*

### Phasenvolumen / Phase Volume

```text
Slots 0..15
   cap 20000 | . . . . . . . . . . . . . . . .
       16667 | . . . . . . . . . . . . . . . .
       13333 | . . . . . . . . . . . . . . . .
       10000 | # . . . . . . . . . . # . . . .
        6667 | # . . . . # . . # . . # . . . .
        3333 | # # . # # # # . # . . # . . . .
           0 +---------------------------------
             00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15
```

```text
Slots 16..31
    cap 5000 | . . . . . . . . . . . . . . . .
        4167 | . . . . . . . . # . . . . . . .
        3333 | . . . # . . # # # # . . . . . .
        2500 | . . # # . # # # # # . . . . . .
        1667 | . . # # . # # # # # . . # . . .
         833 | . . # # . # # # # # . . # . . #
           0 +---------------------------------
             16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31
```

```text
Slots 32..47
   cap 10000 | . . . . . . . . . . . . . . . .
        8333 | . . # . . . . . . . . . . . . .
        6667 | . . # . . . . . . . . . . . . .
        5000 | . . # . . . . . . # . . . . . .
        3333 | . . # . . . . . . # # . . . . .
        1667 | . . # . . . . . # # # . . . . #
           0 +---------------------------------
             32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47
```

| Slot | Phase | Nettozeilen / Net lines |
|---:|---|---:|
| 0 | main / main | 10211 |
| 1 | 001 / 001 | 4353 |
| 2 | 002 / 002 | 2274 |
| 3 | 003 / 003 | 3572 |
| 4 | 004 / 004 | 4747 |
| 5 | 005 / 005 | 9659 |
| 6 | 006 / 006 | 5796 |
| 7 | 007v / 007v | 35 |
| 8 | 007ex / 007ex | 6778 |
| 9 | 008 / 008 | 1550 |
| 10 | 009 / 009 | 231 |
| 11 | 010 / 010 | 12169 |
| 12 | 011 / 011 | 3303 |
| 13 | 012p / 012p | 1604 |
| 14 | 012i / 012i | 2219 |
| 15 | 013i / 013i | 2105 |
| 16 | 014i / 014i | 556 |
| 17 | 015i / 015i | 283 |
| 18 | 016i / 016i | 2659 |
| 19 | 017i / 017i | 3493 |
| 20 | auto / auto | 595 |
| 21 | 018i / 018i | 2912 |
| 22 | 019i / 019i | 3392 |
| 23 | 020i / 020i | 3584 |
| 24 | 021i / 021i | 4617 |
| 25 | 022i / 022i | 3713 |
| 26 | 022c / 022c | 75 |
| 27 | 022r / 022r | 58 |
| 28 | 023i / 023i | 2370 |
| 29 | 023c / 023c | 72 |
| 30 | 023r / 023r | 112 |
| 31 | adopt / adopt | 1212 |
| 32 | agy / agy | 51 |
| 33 | 024p / 024p | 390 |
| 34 | 024i / 024i | 9863 |
| 35 | 024c / 024c | 8 |
| 36 | 024r / 024r | 59 |
| 37 | 027p / 027p | 243 |
| 38 | 027i / 027i | 1130 |
| 39 | 027c / 027c | 31 |
| 40 | 024v2 / 024v2 | 1701 |
| 41 | 025i / 025i | 5248 |
| 42 | 026i / 026i | 4919 |
| 43 | 026c / 026c | 134 |
| 44 | v014 / v014 | 1048 |
| 45 | intake / intake | 395 |
| 46 | v020 / v020 | 1542 |
| 47 | 028i / 028i | 2422 |

Die festen Slots halten den Phasenvergleich auch bei fehlenden oder spaeter ergaenzten Werten stabil.

*Stable slots keep the phase comparison consistent when values are missing or added later.*

### Beschleunigungsfaktoren / Acceleration Factors

```text
Scale: 0..100x
80 lines/day       [#################...] 83.9x
125 lines/day      [###########.........] 53.7x
```

Die Faktoren vergleichen sichtbare Lieferdichte mit den dokumentierten manuellen Referenzen. Sie messen keine Arbeitszeit.

*The factors compare visible delivery density with documented manual references. They do not measure working time.*

### Durchsatzvergleich / Throughput Comparison

```text
Scale: 0..10000 lines/day
Experienced manual [#...................] 80
Thorsten solo      [#...................] 125
Visible repository [#############.......] 6712.1
```

Die gemeinsame Skala vergleicht Referenzen und sichtbare Lieferdichte. Sie schreibt die Git-Aktivitaet keiner Person oder KI pauschal zu.

*The common scale compares references with visible delivery density. It does not attribute Git activity to a person or AI by default.*

### Textalternative / Text Alternative

DE: Das Fenster beginnt am 2025-09-07 und endet am 2026-08-30. Es enthaelt 93 aktive und 265 inaktive vergangene Tage. Peak-Tag: 2026-03-22 / 321183. Peak-Woche: 2026-03-22 / 373576. Laengste Serie: 17 Tage (2026-07-10..2026-07-26).

*EN: The window starts on 2025-09-07 and ends on 2026-08-30. It contains 93 active and 265 inactive elapsed days. Peak day: 2026-03-22 / 321183. Peak week: 2026-03-22 / 373576. Longest streak: 17 days (2026-07-10..2026-07-26).*

| Monat / Month | Geaenderte Textzeilen / Changed text lines |
|---|---:|
| 2025-09 | 0 |
| 2025-10 | 0 |
| 2025-11 | 0 |
| 2025-12 | 0 |
| 2026-01 | 0 |
| 2026-02 | 139105 |
| 2026-03 | 532483 |
| 2026-04 | 18953 |
| 2026-05 | 273853 |
| 2026-06 | 42176 |
| 2026-07 | 205615 |
| 2026-08 | 37382 |

<!-- project-statistics-v2:end -->
