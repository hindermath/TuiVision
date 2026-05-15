# Projektstatistik TuiVision

Stand: 2026-05-15 (aktualisiert inklusive Branch `011-port-wave2-examples`, Wave-2-Beispielen, Lastenheft fuer interaktive Wave-2-Demos, DocFX-Generated-Output-Cleanup, zweistufigem Beispielwellen-Liefermuster, Pflichtenheft-Abnahme fuer interaktive Beispielreife, 011-Review-Cleanup vor 012, Spec-Kit-Intake-Aufbereitung fuer 012, GitHub-Pages-Artefaktworkflow fuer DocFX, PR-#26-Review-Cleanup, `/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-checklist`, `/speckit-tasks`, PR-#27-Review-Cleanup, `/speckit-analyze`-Remediation fuer 012, Wave-1-Folge-Lastenhefte, Wave-1-Visual-Component-Remediation-Umbenennung, Wave-3-/Wave-4-Visual-Component-Porting-Intakes, Lastenheft-Specify-Readiness-Harmonisierung, C#-Dev-Kit-Language-Service-Cache-Ignore, Claude-Code-Review-Remediation fuer 012-Tasks, `/speckit-implement` fuer `012-interactive-wave2-demos`, Lastenheft fuer Wave-2-Visual-Component-Remediation sowie allgemeiner historischer `tv203s`-Quellenreferenzregel)

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
- Dokumentation: Markdown-Dateien in Repository-Wurzel, `docs/`, `specs/`,
  `.github/`, `.specify/` sowie den Agent-Dateien.
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
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-05-10 |
| Git-Commits gesamt | 314 |
| Autoren laut Git | 1 |
| Sichtbare Aktivtage inkl. aktuellem Working Tree | 21 |
| Produktionscode aktuell | 159 Dateien / 18347 Zeilen |
| Testcode aktuell | 96 Dateien / 12645 Zeilen |
| Dokumentation aktuell | 320 Dateien / 39605 Zeilen |
| Davon Spec-Kit-Artefakte | 114 Dateien / 20419 Zeilen |
| Davon Governance/Agent-Dateien | 5 Dateien / 1689 Zeilen |
| Gesamtbasis fuer Handschaetzung (inkl. Dokumentation) | 70597 Zeilen |
| Erfahrener Entwickler, konservative Untergrenze | 882.5 Arbeitstage |
| Erfahrener Entwickler, konservative Untergrenze in Stunden | 6883.2 Stunden (70597 / 80 * 7.8) |
| Erfahrener Entwickler, brutto | 41.0 Arbeitsmonate (21.5 Tage/Monat) |
| Erfahrener Entwickler, TVoeD-Annahme | 46.4 Kalendermonate bzw. 3.9 Jahre |
| Thorsten solo, erfahrungsadjustierte Untergrenze | 564.8 Arbeitstage |
| Thorsten solo, erfahrungsadjustierte Untergrenze in Stunden | 4405.3 Stunden (70597 / 125 * 7.8) |
| Thorsten solo, brutto | 26.3 Arbeitsmonate (21.5 Tage/Monat) |
| Thorsten solo, TVoeD-Annahme | 29.7 Kalendermonate bzw. 2.5 Jahre |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 353.0 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), TVoeD-Annahme | 18.6 Kalendermonate |
| Repo-weiter Beschleunigungsfaktor vs. konservative Referenz | 42.0x (882.5 / 21 sichtbare Aktivtage) |
| Repo-weiter Beschleunigungsfaktor vs. Thorsten-Referenz | 26.9x (564.8 / 21 sichtbare Aktivtage) |

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
| 2026-05-09 | `/speckit-clarify` fuer `012-interactive-wave2-demos` | Die 012-Spezifikation wurde vor der Planung geschaerft: primaere Smoke-Nachweise muessen durch `app.Run()` oder die echte App-Schleife mit injizierten Events laufen, Datei-/Pfad- und Dialog-Designer-Pfade bleiben read-only gegenueber Nutzerdaten, `pr-evidence.md` und `examples/README.md` sind explizite Abschlussnachweise, und Example-Smoke-Suite plus voller `dotnet test`-Lauf sind formale Completion-Evidence. Zusaetzlich wurde `.specify/feature.json` von 011 auf `specs/012-interactive-wave2-demos` korrigiert, damit `check-prerequisites.sh --json --paths-only` fuer Plan/Tasks auf die richtige Feature-Spec zeigt. Validierung: korrigierter Prerequisite-Pfad, keine offenen Clarification-/TODO-Marker und `git diff --check` sauber. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+15` Spec-Kit-Dokumentationszeilen; `.specify/feature.json` und `Directory.Build.props` sind reine Pfad-/Versionsmetadaten. |
| 2026-05-10 | `/speckit-plan` fuer `012-interactive-wave2-demos` und historische Quellenreferenzregel | Der 012-Plan wurde aus der geklaerten Spezifikation erzeugt: `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und der Acceptance-Contract beschreiben Demo-Vertical-Slice, sichtbare Bedienpfade, Event-Loop-Smokes, Read-only-Fixture-Grenzen, Governance-Evidence und die Pflicht zur historischen Quellenpruefung. Die Quellenpruefung wurde zugleich als allgemeine Spec-Kit-Regel verankert: Bei historisch abgeleitetem Turbo-Vision-Verhalten sind relevante `.c`-/`.cc`-Dateien und bei Bedarf C/C++-Header unter `tv203s/` read-only zu pruefen; bewusste Abweichungen werden in Spec, Plan, Tasks, Guide, PR-Evidence oder Architektur-/Security-Nachweis dokumentiert. Validierung: `specify check`, korrigierter 012-Prerequisite-Pfad und `git diff --check`; keine Build-/Testausfuehrung, weil nur Planungs- und Guidance-Dokumente geaendert wurden. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+905` Dokumentations-/Guidance-Zeilen inklusive Statistikfortschreibung; `Directory.Build.props` steht fuer den dritten Branch-Commit auf `1.12.3.27`. Konservative Manualreferenz: 80 Zeilen/Tag = `11,3` Tage (ca. `88,2` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `7,2` Tage (ca. `56,5` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-05-10, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-10 | `/speckit-checklist` fuer `012-interactive-wave2-demos` | Fuer den 012-Plan und die zugehoerigen Artefakte wurde `specs/012-interactive-wave2-demos/checklists/plan-quality.md` erstellt. Die neue Pruefliste enthaelt 36 Anforderungen-Qualitaetschecks mit ausfuehrlichem Durchfuehrungshinweis je Punkt und deckt Vollstaendigkeit, Klarheit, Konsistenz, Akzeptanzkriterien, Szenario-/Edge-Case-Abdeckung, Non-Functional Requirements, Abhaengigkeiten, Annahmen und Task-Readiness ab. Validierung: Spec-Kit-Prerequisite-Check zeigt auf 012, Checklist-Template und Planartefakte wurden gelesen, `git diff --check` sauber; keine Build-/Testausfuehrung, weil nur Planungsdokumentation geaendert wurde. `Directory.Build.props` steht fuer den vierten 012-Branch-Commit auf `1.12.4.27`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+141` Spec-Kit-Checklistenzeilen plus diese Statistikfortschreibung. Konservative Manualreferenz fuer die neue Checkliste: 80 Zeilen/Tag = `1,8` Tage (ca. `13,7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,1` Tage (ca. `8,8` Stunden); sichtbares Arbeitsfenster: kurze Agentennacharbeit am 2026-05-10, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-10 | `/speckit-tasks` fuer `012-interactive-wave2-demos` | Aus `spec.md`, `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und `contracts/interactive-wave2-demo-acceptance.md` wurde eine umsetzbare `tasks.md` fuer interaktive Wave-2-Demos erzeugt: 87 Tasks insgesamt, davon 5 Setup-, 16 Foundation-, 7 US1-Demo-Vertical-Slice-, 22 US2-Runtime-Pfad-, 8 US3-Smoke-Proof-, 14 US4-Dokumentations- und 15 Polish-/Governance-Aufgaben. Die Aufgaben verankern historische `.c`-/`.cc`-/Header-Reviews je Beispiel, app-loop-basierte Smoke-Pfade, sichtbare Menue-/Status-/Command-Interaktion, Guide-Updates, PR-Evidence, DocFX/A11Y, Coverage, Format und finale Versionspflege. Validierung: 012-Prerequisite-Check, Planartefakte gelesen, 87/87 Aufgaben im Spec-Kit-Checkboxformat, keine Build-/Testausfuehrung, weil nur Aufgaben- und Statistikdokumentation geaendert wurde. `Directory.Build.props` steht fuer den fuenften 012-Branch-Commit auf `1.12.5.27`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+268` Spec-Kit-Aufgabenzeilen plus diese Statistikfortschreibung. Konservative Manualreferenz fuer die neue Aufgabenliste: 80 Zeilen/Tag = `3,4` Tage (ca. `26,1` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `2,1` Tage (ca. `16,7` Stunden); sichtbares Arbeitsfenster: eine Agentensitzung am 2026-05-10, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-10 | PR-#27-Review-Cleanup fuer `012-interactive-wave2-demos` | Die Copilot-Review-Hinweise im Spec-PR wurden bereinigt: absolute lokale Plan-/Research-Links wurden durch repo-relative Links ersetzt, die Requirements-Checkliste erlaubt nun ausdruecklich notwendige Constitution-, Governance- und Validation-Evidence-Details, `specs/012-interactive-wave2-demos/pr-evidence.md` wurde als Nachweis-Ledger angelegt, und die PR-Beschreibung wurde mit dem damaligen Plan-/Task-Scope synchronisiert. Der aktuelle Branch-Versionsstand wird in der jeweils neuesten 012-Ledger-Zeile gefuehrt, damit der Statistiktext nicht mit spaeteren Folgecommits kollidiert. Validierung: Review-Hinweise gelesen, `git diff --check` sauber; keine Build-/Testausfuehrung, weil nur Planungs-, Review- und Statistikdokumentation geaendert wurde. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, rund `+53` Spec-Kit-/Nachweiszeilen plus diese Statistikfortschreibung. |
| 2026-05-10 | `/speckit-analyze`-Remediation fuer `012-interactive-wave2-demos` | Die vier vor `/speckit-implement` relevanten Analysefunde wurden in Plan-/Task-Artefakten bereinigt: Constitution-konformer Lastenheft-Rename ist jetzt per `scripts/rename-lastenheft.*` als letzter Polish-Schritt formuliert, die PR-Beschreibung ist eigener Abschlussnachweis mit betroffenen Artefakten, Validierung, Sample-Output und Sicherheitsrisiko-Aussage, Release-Build und Release-Testbefehle sind explizite Merge-Evidence, und `Directory.Build.props`/Build-Counter-Ausrichtung steht vor Build-/Testlaeufen. Die Aufgabenliste umfasst danach 89 Tasks. Validierung: erneute Aufgabenzaehlung `89`, `git diff --check` sauber; keine Build-/Testausfuehrung, weil nur Planungs- und Nachweisdokumente geaendert wurden. |
| 2026-05-10 | Wave-1-Folge-Lastenhefte fuer zweistufiges Beispielmuster | Zwei neue Spec-Kit-Intake-Dateien wurden als Folgefeatures nach `012-interactive-wave2-demos` angelegt: `Lastenheft_Wave1-Functional-Hardening.md` prueft und haertet Desklogo, MsgCls, Tutorial und Videomode fachlich gegen die historischen `.cc`-/`.cpp`-/Header-Quellen unter `tv203s/`; das inzwischen umbenannte und geschaerfte `Lastenheft_Wave1-Visual-Component-Remediation.md` baut danach auf den gehaerteten Funktionen sichtbare normale CLI-Demos mit Menue-/Status-/Tastatur-/Command-Pfaden und app-loop-basierten Smoke-Tests. Damit bleibt 012 im Scope geschlossen, waehrend das aus 011/012 gewonnene zweistufige Muster fuer Wave 1 unmittelbar als Specify-Eingabe bereitsteht. Damaliges Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+674` Lastenheft-Dokumentationszeilen. Konservative Manualreferenz: 80 Zeilen/Tag = `8,4` Tage (ca. `65,7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `5,4` Tage (ca. `42,1` Stunden); sichtbares Arbeitsfenster: kurze Agentennacharbeit am 2026-05-10, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-10 | PR-#27-Versionserzaehlung und Wave-1-Lastenheft-Commit vorbereitet | Die zwei offenen Copilot-Threads zu PR #27 wurden lokal korrigiert: `Directory.Build.props` steht fuer den naechsten 012-Branch-Commit konsistent auf `1.12.8.27`, die fruehere Statistikzeile behauptet keine veraltete aktuelle Version mehr, und die PR-Beschreibung wird nach dem Push auf denselben Versionsstand aktualisiert. Die beiden neuen Wave-1-Lastenhefte bleiben im selben Commit enthalten, damit die Folgefeature-Intakes gemeinsam mit der Review-Korrektur versioniert werden. Validierung: `git diff --check`; keine Build-/Testausfuehrung, weil nur Dokumentation und Versionsmetadaten geaendert wurden. |
| 2026-05-10 | Claude-Code-Review-Remediation fuer `012-interactive-wave2-demos`-Tasks | Die von Claude Code reviewten 012-Aufgaben wurden vor dem Implementierungsstart weiter geschaerft: `DirectHelperUsage` ist jetzt als explizite T018-API vor den app-loop-Smokes verankert, legacy direct-helper-only Tests muessen geloescht oder als Setup/Supplemental klassifiziert werden, historische Quellenreviews und Evidence-Matrix-Updates schreiben nicht mehr parallel in dieselben Markdown-Zeilen, Demo-Omissionen werden als PR-Evidence-Abweichung statt als Runtime-UI-State behandelt, und die finale Versionierung ist fuer den neunten 012-Branch-Commit auf `1.12.9.27` ausgerichtet. Validierung: `/speckit-analyze` Read-only-Gegenpruefung mit 100 % Requirements-Abdeckung, `git diff --check`; keine Build-/Testausfuehrung, weil nur Aufgaben- und Statistikdokumentation plus Versionsmetadaten geaendert wurden. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+3` Task-Dokumentationszeilen netto plus diese Statistikfortschreibung; `Directory.Build.props` ist reine Versionsmetadatenpflege. |
| 2026-05-10 | `/speckit-implement` fuer `012-interactive-wave2-demos` | Die interaktive Showcase-Stufe fuer Welle 2 wurde umgesetzt: alle elf Wave-2-Beispiele besitzen sichtbare normale CLI-Startbildschirme, Menue-/Command-Pfade, text-first Rueckmeldungen und app-loop-basierte Smoke-Nachweise; direkte Helfer sind nur noch Setup- oder Supplemental-Proof. Aktualisiert wurden Beispielcode, Example-Smoke-Tests, Guides, README, `Pflichtenheft.md`, Architektur-/Security-Nachweise, Agent-Guidance und das umbenannte Lastenheft `Lastenheft_Interactive-Wave2-Demos.012-interactive-wave2-demos.md`. Validierung: `dotnet build --configuration Release` gruen, Example-Smoke `73/73`, voller Release-Testlauf `478/478`, Coverage-Gate (`Core` `89,78 %`, `Controls` `84,84 %`, `Serialization` `87,95 %`, `Compatibility` `80,55 %`, `Drivers.Console` `81,70 %`), `dotnet format --verify-no-changes`, `docfx docfx.json`, `npm run test:docfx` mit `2/2` Playwright/Axe-Smokes, elf manuelle `dotnet run --project examples/<Name> --configuration Release --no-build`-Startupchecks und `git diff --check`; nach der Commit-Freigabe wurde zusaetzlich der finale Versionierungs-Build auf `1.12.10.35` mit 0 Warnungen und 0 Fehlern ausgefuehrt. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: ca. `+1384` Produktions-/Beispielcode-Zeilen, `+505` Testzeilen inklusive zwei neuer Smoke-Hilfsdateien, ca. `+330` Dokumentations-/Nachweis-/Guidance-Zeilen plus diese Statistikfortschreibung; `Directory.Build.props` steht nach der branchkonformen Commit-Ausrichtung auf `1.12.10.35`. Konservative Manualreferenz fuer rund `2219` fachliche Netto-Zeilen: `27,7` Tage (ca. `216,4` Stunden); Thorsten-Solo-Referenz: `17,8` Tage (ca. `138,5` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-05-10, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-11 | Lastenheft fuer Wave-2-Visual-Component-Remediation | `Lastenheft_Wave2-Visual-Component-Remediation.md` wurde als direkte Spec-Kit-Eingabedatei fuer `013-wave2-visual-component-remediation` angelegt. Das Dokument haelt die manuelle Review-Erkenntnis fest, dass `012-interactive-wave2-demos` zwar App-Loop-Menues und text-first Rueckmeldungen liefert, aber die historischen sichtbaren Controls, Dialoge, Fenster und View-Gruppen in mehreren Wave-2-Beispielen noch nicht als primaere UI-Komposition zeigt. Es definiert pro Beispiel historische Quellen, sichtbare Hauptidee, aktuelle Luecke, Zielzustand, Acceptance-Kriterien und einen kopierbaren `/speckit-specify`-Prompt; die bisherige `TStaticText`-Rueckmeldung wird dabei als Statuszeilen-/Statusbereichsfeedback erhalten und durch ein Drei-Schichten-Modell aus Hauptkomponente, Statuszeile und Beschreibungspfad eingeordnet. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+539` Lastenheft-Dokumentationszeilen plus diese Statistikfortschreibung. Konservative Manualreferenz: 80 Zeilen/Tag = `6,7` Tage (ca. `52,6` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `4,3` Tage (ca. `33,6` Stunden); sichtbares Arbeitsfenster: kurze Agentennacharbeit am 2026-05-11, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-11 | Wave-1-Visual-Component-Remediation-Lastenheft abgenommen | Das fruehere Interactive-Wave1-Lastenheft wurde per `git mv` zu `Lastenheft_Wave1-Visual-Component-Remediation.md` umbenannt und auf den abgenommenen Remediation-Zuschnitt gebracht. Das Dokument spiegelt jetzt die historischen Wave-1-Beispiele `Desklogo`, `MsgCls`, `Tutorial` und `Videomode`, fordert sichtbare Hauptflaechen-, Statuszeilen- und Beschreibungspfad-Nachweise und uebernimmt dieselbe Prueflogik wie das aktuelle Wave-2-Visual-Remediation-Lastenheft. Die vorherige 356-Zeilen-Fassung wurde durch eine 497-Zeilen-Fassung ersetzt; Netto-Aenderungsvolumen gegenueber der Vorfassung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+141` Lastenheft-Dokumentationszeilen plus Referenz- und Statistikpflege. Konservative Manualreferenz fuer die Netto-Dokumentationsaenderung: 80 Zeilen/Tag = `1,8` Tage (ca. `13,7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1,1` Tage (ca. `8,8` Stunden); sichtbares Arbeitsfenster: kurze Agentennacharbeit am 2026-05-11, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-11 | Wave-3-/Wave-4-Visual-Component-Porting-Intakes | Zwei neue Specify-ready Lastenhefte wurden fuer die noch offenen MUSS-Beispielwellen angelegt: `Lastenheft_Wave3-Visual-Component-Porting.md` fuer `bhelp`, `helpdemo`, `i18n`, `tvedit` und `tvhc`; `Lastenheft_Wave4-Visual-Component-Porting.md` fuer `cyrillic`, `eterm`, `fonts`, `terminal` und `xterm`. Beide Dateien uebernehmen das Drei-Schichten-Modell aus Hauptflaeche, Statuszeile/Statusbereich und Beschreibungspfad, machen sichtbare Kompositionen oder stabile Runtime-Zustaende zum primaeren Smoke-Nachweis und enthalten kopierbare `/speckit-specify`-Prompts. Damit wird die bei Welle 1/2 nachgeschaerfte Prueflogik fuer Welle 3/4 direkt vor der Portierung gesetzt. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+995` Lastenheft-Dokumentationszeilen plus diese Statistikfortschreibung. Konservative Manualreferenz: 80 Zeilen/Tag = `12,4` Tage (ca. `97,0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `8,0` Tage (ca. `62,1` Stunden); sichtbares Arbeitsfenster: kurze Agentennacharbeit am 2026-05-11, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-11 | Lastenheft-Specify-Readiness-Harmonisierung | Alle 14 `Lastenheft*.md`-Dateien wurden gegen denselben Mindeststandard geprueft: `Spec-Kit-Eingabedatei`-Status, direkt kopierbarer `/speckit-specify`-Prompt sowie sichtbare CEFR-B2-, DE/EN- und text-first-A11Y-Hinweise. Fehlende Readiness-Addenda und Prompts wurden in den aelteren Lastenheften fuer Controls-Revision, Wave-2-Voraussetzungen, Wave-3-/Wave-4-Hardening, Maussupport, A11Y-Framework und Constitution-Change nachgetragen; `Lastenheft_Wave1-Functional-Hardening.md` und das branchgebundene 012-Lastenheft erhielten direkt kopierbare Command-Prompts. Die abschliessende Audit-Ausgabe meldete fuer 14/14 Dateien `status=OK`, `prompt=OK`, `cefr_a11y=OK`. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+356` Lastenheft-Dokumentationszeilen plus diese Statistikfortschreibung. Konservative Manualreferenz: 80 Zeilen/Tag = `4,5` Tage (ca. `34,7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `2,8` Tage (ca. `22,2` Stunden); sichtbares Arbeitsfenster: kurze Agentennacharbeit am 2026-05-11, als blended repository speedup und nicht als Stopwatch-Messung zu lesen. |
| 2026-05-15 | C#-Dev-Kit-Language-Service-Cache ignoriert | `.gitignore` wurde um `*.lscache` ergaenzt, damit lokal von C# Dev Kit erzeugte Language-Service-Cache-Dateien neben `.csproj`-Dateien nicht mehr als untracked Repo-Aenderungen erscheinen. Die Dateien bleiben lokal regenerierbar und werden nicht geloescht oder versioniert. Netto-Aenderungsvolumen vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+2` Gitignore-Konfigurationszeilen plus diese Statistikfortschreibung. |

## Gesamtstatistik

Basis dieses Schlussblocks sind die aktuell dokumentierten Snapshot- und
Phasenwerte aus den Abschnitten `## Gesamtstand des Repositories` und
`## Phasen und Haupt-Branches` weiter oben.

| Kennzahl | Verdichteter Gesamtblick |
|---|---:|
| Artefaktbasis gesamt | `70597` Zeilen |
| Produktions- und Testcode zusammen | `30992` Zeilen (`43.9 %`) |
| Dokumentationsanteil | `39605` Zeilen (`56.1 %`) |
| Spec-Kit-Anteil innerhalb der Doku | `20419` Zeilen (`51.6 %`) |
| Governance-/Agent-Anteil innerhalb der Doku | `1689` Zeilen (`4.3 %`) |
| Beobachtbarer Projektzeitraum | `2026-02-08` bis `2026-05-10` |
| Git-Commits pro sichtbarem Aktivtag | `15.0` (`314 / 21`) |
| Dokumentierte Gesamtzeilen pro sichtbarem Aktivtag | `3361.8` (`70597 / 21`) |
| Dokumentierte Gesamtzeilen pro Commit | `224.8` (`70597 / 314`) |
| Konservative Einzelentwickler-Untergrenze | `882.5` Arbeitstage / `6883.2` Stunden |
| Thorsten-Solo-Untergrenze | `564.8` Arbeitstage / `4405.3` Stunden |
| Kleines 3er-Team mit Koordinationsaufschlag | `353.0` Arbeitstage |
| Repo-weiter Speedup gg. 80-Zeilen-Referenz | `42.0x` |
| Repo-weiter Speedup gg. Thorsten-Referenz | `26.9x` |

Kurzfazit:
Die aktuell dokumentierte Basis zeigt weiter ein starkes Verhaeltnis zwischen
Code (`43.9 %`) und Dokumentation (`56.1 %`). Welle 2 erhoeht den sichtbaren
Beispiel- und Smoke-Test-Anteil deutlich; das neue zweistufige Liefermuster
haelt den interaktiven Follow-up-Weg jetzt auch in der formalen Abnahmebasis sichtbar. Der aktuelle Arbeitsbaum enthaelt
jetzt auch `011-port-wave2-examples`, das Lastenheft fuer interaktive
Wave-2-Demos, den DocFX-Generated-Output-Cleanup, den 011-Review-Cleanup
vor 012, die Spec-Kit-Intake-Aufbereitung fuer 012, den GitHub-Pages-
Artefaktworkflow, die 012-Spezifikation, die 012-Clarifications, den 012-Plan
und die allgemeine historische `tv203s`-Quellenreferenzregel, die
012-Planqualitaetscheckliste, die 012-Aufgabenliste, den PR-#27-Review-
Cleanup, zwei Wave-1-Folge-Lastenhefte und die implementierte interaktive
012-Showcase-Stufe im Snapshot. Die dokumentierten
Beschleunigungsfaktoren zeigen dabei nicht eine Stoppuhr, sondern die sichtbare
Verdichtung durch agentische KI plus Spec-Kit-/SDD-gestuetzte Artefakt- und
Liefergeschwindigkeit.

### ASCII-Diagramme

```text
Artefaktmix nach aktuell dokumentiertem Snapshot (Zeilen)
Produktion     | ##############                 | 18347 | 26.1 %
Tests          | ##########                     | 12645 | 18.0 %
Dokumentation  | ############################## | 39605 | 56.1 %
```

Der Artefaktmix zeigt, wie sich der aktuelle Repository-Snapshot auf
Produktionscode, Testcode und Dokumentation verteilt. Lange Balken bedeuten
mehr Zeilen innerhalb derselben Vergleichsgruppe. So sieht man schnell, ob der
aktuelle Stand eher von Code oder eher von Dokumentation gepraegt ist.

The artifact mix shows how the current repository snapshot is split between
production code, test code, and documentation. Longer bars mean more lines
inside the same comparison group. This helps readers see quickly whether the
current state is driven more by code or by documentation.

```text
Branch-/Phasenvolumen nach dokumentierter Netto-Basis (Zeilen)
0 main  | ######################## | 10211
1 001   | ##########               |  4353
2 002   | #####                    |  2274
3 003   | ########                 |  3572
4 004   | ###########              |  4747
5 005   | #######################  |  9659
6 006   | ##############           |  5796
7 007v  | #                        |    35
8 007ex | ################         |  6778
9 008   | ####                     |  1550
10 009  | #                        |   231
11 010  | ######################## | 12169
12 011  | #######                  |  3303
13 012p | ###                      |  1604
14 012i | #####                    |  2219
```

Dieses Diagramm zeigt die grob sichtbare Netto-Basis der dokumentierten
Branch- und Phasenpakete. Es beantwortet die Frage, welches Paket im
Repository besonders viel sichtbaren Umfang erzeugt hat.

This chart shows the visible net size of the documented branch and phase
packages. It answers the question which package created especially large visible
scope in the repository.

```text
Konservative Handarbeits-Referenz je dokumentierter Phase (Arbeitstage)
0 main  | ######################## | 127.6 d
1 001   | ##########               |  54.4 d
2 002   | #####                    |  28.4 d
3 003   | ########                 |  44.7 d
4 004   | ###########              |  59.3 d
5 005   | #######################  | 120.7 d
6 006   | ##############           |  72.5 d
7 007v  | #                        |   0.4 d
8 007ex | ################         |  84.7 d
9 008   | ####                     |  19.4 d
10 009  | #                        |   2.9 d
11 010  | ######################## | 152.1 d
12 011  | #######                  |  41.3 d
13 012p | ###                      |  20.1 d
14 012i | #####                    |  27.7 d
```

Dieses Diagramm zeigt dieselben Pakete noch einmal, jetzt aber als
konservative manuelle Referenz in Arbeitstagen. Damit wird sichtbar, wie gross
derselbe Lieferumfang ohne agentische Unterstuetzung aus klassischer Sicht
waere.

This chart shows the same packages again, now as a conservative manual
reference in workdays. It makes visible how large the same delivery would look
from a traditional non-agentic perspective.

```text
Dokumentierte Beschleunigungsfaktoren durch agentische KI + Spec-Kit/SDD
Repo 80 | #######                  |  42.0x
Repo125 | #####                    |  26.9x
0 main  | ######                   |  31.9x
1 001   | ###                      |  13.6x
2 002   | ###                      |  14.2x
3 003   | #########                |  44.7x
4 004   | n/a                      |  keine belastbare Git-Aktivtag-Basis
5 005   | ######################## | 120.7x
6 006   | n/a                      |  kein expliziter Branch-Speedup dokumentiert
7 007v  | #                        |   0.4x
8 007ex | ########                 |  42.3x
9 008   | ####                     |  19.4x
10 009  | #                        |   2.9x
11 010  | ######################## | 152.1x
12 011  | #######                  |  41.3x
13 012p | ###                      |  20.1x
14 012i | #####                    |  27.7x
```

Dieses Diagramm zeigt die dokumentierten Beschleunigungsfaktoren. Hier wird
nicht echte Uhrzeit gemessen. Stattdessen wird die sichtbare Verdichtung des
Lieferumfangs gegen die gewaehlten manuellen Referenzen verglichen. Hohe Werte
bedeuten also: viel sichtbarer Output in wenigen dokumentierten Aktivtagen.

This chart shows the documented acceleration factors. It does not measure real
clock time. Instead, it compares visible delivery density against the chosen
manual references. Higher values therefore mean a lot of visible output within
only a few documented active days.

```text
Vergleich dokumentierter Gesamtaufwand / sichtbares KI-Lieferfenster
Erfahren   | ############################## | 882.5 d
Thorsten   | ###################            | 564.8 d
KI sichtbar| #                              |  21.0 d
```

Dieses Diagramm vergleicht die drei Gesamtperspektiven direkt: `Erfahren`
steht fuer die konservative 80-Zeilen-Referenz eines erfahrenen Entwicklers,
`Thorsten` fuer die erfahrungsadjustierte Solo-Referenz, und `KI sichtbar` fuer
das im Repository belegte Aktivfenster. Der Abstand zwischen den Balken macht
die dokumentierte Verdichtung durch agentische KI und Spec-Kit/SDD besonders
leicht lesbar.

This chart compares the three overall perspectives directly: `Erfahren` stands
for the conservative 80-lines-per-day reference of an experienced developer,
`Thorsten` for the experience-adjusted solo reference, and `KI sichtbar` for
the repository-visible activity window. The distance between the bars makes the
documented compression through agentic AI and Spec-Kit/SDD easier to
understand.

Wenn man einen Verlauf ueber die X-Achse sehen will, helfen X/Y-Diagramme
zusaetzlich. Hier steht die X-Achse fuer die dokumentierten Phasen oder
Branches (`0` bis `14`). Die Y-Achse zeigt je nach Diagramm Zeilen, Arbeitstage
oder Beschleunigungsfaktoren. Die Sternpunkte sind bewusst grob gesetzt: Sie
sollen Trends sichtbar machen, nicht mathematische Genauigkeit auf Plotter-
Niveau liefern.

If readers want to see progression across the X-axis, X/Y charts help as a
second view. Here the X-axis stands for the documented phases or branches (`0`
to `14`). Depending on the chart, the Y-axis shows lines, workdays, or
acceleration factors. The star markers are intentionally approximate: they are
meant to reveal trends, not to deliver plotter-level mathematical precision.

```text
X/Y-Diagramm: dokumentiertes Phasenvolumen (X = Phase/Branch, Y = Zeilen)
12169.0 |                      *
10647.9 |*         *
 9126.8 |
 7605.6 |                *
 6084.5 |            *
 4563.4 |  *   * *
 3042.3 |    *                 * *
 1521.1 |                  *
    0.0 |              *     * *
        +-------------------------
         0 1 2 3 4 5 6 7 8 9 10 11 12 13 14
```

Dieses X/Y-Diagramm zeigt denselben Verlauf wie der Volumen-Balkenblock, aber
als Punktkurve ueber die Phasenachse. Damit sieht man leichter, wo groessere
Spruenge oder Einbrueche zwischen direkt benachbarten Paketen liegen.

This X/Y chart shows the same progression as the volume bar block, but as a
point curve across the phase axis. It helps readers spot bigger jumps or drops
between directly neighboring packages.

```text
X/Y-Diagramm: konservative Handarbeits-Referenz (X = Phase/Branch, Y = Arbeitstage)
  152.1 |                      *
  133.1 |*         *
  114.1 |
   95.1 |                *
   76.1 |            *
   57.0 |  *   * *
   38.0 |    *                 *
   19.0 |                  *   *
    0.0 |              *     * *
        +-------------------------
         0 1 2 3 4 5 6 7 8 9 10 11 12 13 14
```

Dieses X/Y-Diagramm zeigt, wie sich die konservative manuelle Referenz ueber
die dokumentierten Pakete verteilt. Der Nutzen fuer Auszubildende liegt hier
vor allem im Vergleich: Ein hoher Zeilenwert und ein hoher Arbeitstagewert
laufen oft parallel, aber nicht jede kleine Aenderung hat automatisch denselben
didaktischen oder organisatorischen Aufwand.

This X/Y chart shows how the conservative manual reference is distributed across
the documented packages. Its value for apprentices is mainly comparative: a
high line count and a high workday count often move together, but not every
small code change implies the same teaching or coordination effort.

```text
X/Y-Diagramm: dokumentierte Beschleunigungsfaktoren (X = Phase/Branch, Y = Faktor)
  152.1 |                    *
  133.1 |
  114.1 |        *
   95.1 |
   76.1 |
   57.0 |      *     *       *
   38.0 |*
   19.0 |  * *           * * *
    0.0 |          * *     *
        +-------------------------
         0 1 2 3 5 7 8 9 10 11 12 13 14
```

Dieses X/Y-Diagramm zeigt die dokumentierten Beschleunigungsfaktoren nur fuer
die Pakete, bei denen im Dokument eine belastbare Aktivtag-Basis oder ein
expliziter Faktor genannt ist. Fehlende Punkte bedeuten also nicht automatisch
schlechte Leistung, sondern nur: Hier liegt im aktuellen Statistikstand keine
saubere Basis fuer denselben Vergleich vor.

This X/Y chart shows the documented acceleration factors only for packages
where the document already provides a reliable active-day basis or an explicit
factor. Missing points therefore do not automatically mean poor performance;
they only mean that the current statistics state does not provide a clean base
for the same comparison.
