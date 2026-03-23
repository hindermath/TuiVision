# Projektstatistik TuiVision

Stand: 2026-03-22

## Zweck und Pflege

Diese Datei ist das fortlaufende Statistik-Register fuer TuiVision. Sie wird
nach jeder abgeschlossenen Spec-Kit-Implementierungsphase, nach jeder
agentischen Aenderung am Repository und auf explizite Anforderung
fortgeschrieben.

## Methodik

- Quellen: Git-Historie, lokale Branches, aktueller Arbeitsbaum.
- Ausgeschlossen: `tv203s/`, `_site/`, `bin/`, `obj/` sowie sonstige generierte
  Artefakte.
- Produktionscode: `src/**/*.cs`
- Testcode: `tests/**/*.cs`
- Dokumentation: Markdown-Dateien in Repository-Wurzel, `docs/`, `specs/`,
  `.github/`, `.specify/` sowie den Agent-Dateien.
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
  unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr ergeben sich
  `21.5 * 12 - 30 = 228` produktive Arbeitstage pro Jahr bzw.
  durchschnittlich 19.0 produktive Tage pro Kalendermonat.
- Abgeleitete Formeln in dieser Datei:
  Einzelentwickler `((Produktionscode + Testcode + Dokumentation) / 80)`;
  3er-Team `Einzelentwickler / 3 * 1.2` mit 20 % Koordinationsaufschlag.

## Gesamtstand des Repositories

| Kennzahl | Wert |
|---|---:|
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-03-22 |
| Git-Commits gesamt | 125 |
| Autoren laut Git | 1 |
| Git-Aktivtage | 9 |
| Produktionscode aktuell | 61 Dateien / 8907 Zeilen |
| Testcode aktuell | 54 Dateien / 5306 Zeilen |
| Dokumentation aktuell | 70 Dateien / 10505 Zeilen |
| Davon Spec-Kit-Artefakte | 44 Dateien / 7305 Zeilen |
| Davon Governance/Agent-Dateien | 5 Dateien / 635 Zeilen |
| Gesamtbasis fuer Handschaetzung (inkl. Dokumentation) | 24718 Zeilen |
| Erfahrener Entwickler, konservative Untergrenze | 309.0 Arbeitstage |
| Erfahrener Entwickler, brutto | 14.4 Arbeitsmonate (21.5 Tage/Monat) |
| Erfahrener Entwickler, TVoeD-Annahme | 16.3 Kalendermonate bzw. 1.4 Jahre |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 123.6 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), TVoeD-Annahme | 6.6 Kalendermonate |

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

## Zusatz-Branches

| Branch | Letzte sichtbare Aktivitaet | Rolle |
|---|---|---|
| `origin/codex/add-project-constitution` | 2026-03-01 | Governance-Bootstrap fuer die erste Constitution |
| `origin/hindermath-patch-1` | 2026-03-01 | CI-Anpassung fuer Branch-Trigger |

## Einordnung der KI-/Spec-Kit-Wirkung

- Die beobachtbare manuelle Gesamtbasis liegt bereits bei 24718 Zeilen
  (Produktionscode + Tests + Dokumentation).
- Selbst mit der fuer klassische Entwicklung guenstigen Obergrenze von
  80 manuell erstellten Zeilen pro Arbeitstag ergibt sich bereits eine
  Untergrenze von 309.0 Entwickler-Arbeitstagen.
- Unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr entspricht das fuer einen
  erfahrenen Entwickler ca. 16.3 Kalendermonaten bzw. 1.4 Arbeitsjahren; fuer
  ein 3er-Team mit 20 % Koordinationsaufschlag ca. 6.6 Kalendermonaten.
- Die vorliegenden Git-Daten zeigen damit eine deutliche Verdichtung durch
  agentische KI und GitHub Spec-Kit: hoher Dokumentations- und Codeumfang in
  einem kurzen beobachtbaren Aktivfenster.

## Fortschreibungsprotokoll

| Datum | Ausloeser | Eintrag |
|---|---|---|
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
