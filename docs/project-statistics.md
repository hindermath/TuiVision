# Projektstatistik TuiVision

Stand: 2026-03-21

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
- Die konservative Handarbeits-Basis folgt dem Beitrag
  [Adapt or Disappear: How AI Turned a 2-Year Project into a 1-Week Sprint](https://www.holgerscode.com/blog/2026/02/23/adapt-or-disappear-how-ai-turned-a-2-year-project-into-a-1-week-sprint/#a-note-on-the-orm-29000-lines-you-never-have-to-write):
  maximal 80 Codezeilen pro Tag fuer einen erfahrenen Entwickler.
- Abgeleitete Team-Schaetzung in dieser Datei:
  `((Produktionscode + Testcode) / 80) / 3 * 1.2`
  Das ist eine abgeleitete 3-Personen-Kalenderschaetzung mit 20 % Abstimmungs-
  aufschlag. Dokumentation ist darin noch nicht eingepreist; die klassische
  Handarbeit laege also real hoeher.

## Gesamtstand des Repositories

| Kennzahl | Wert |
|---|---:|
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-03-21 |
| Git-Commits gesamt | 125 |
| Autoren laut Git | 1 |
| Git-Aktivtage | 9 |
| Produktionscode aktuell | 34 Dateien / 5710 Zeilen |
| Testcode aktuell | 35 Dateien / 3881 Zeilen |
| Dokumentation aktuell | 72 Dateien / 10309 Zeilen |
| Davon Spec-Kit-Artefakte | 44 Dateien / 7305 Zeilen |
| Davon Governance/Agent-Dateien | 5 Dateien / 635 Zeilen |
| Codebasis fuer Handschaetzung | 9591 Zeilen |
| Erfahrener Entwickler, konservative Untergrenze | 119.9 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 48.0 Arbeitstage |

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
- Konservative Handarbeits-Basis fuer Code:
  - 2823 Codezeilen netto
  - 35.3 Arbeitstage fuer einen erfahrenen Entwickler
  - 14.1 Arbeitstage fuer ein 3er-Team (+20 % Koordination)

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
- Konservative Handarbeits-Basis fuer Code:
  - 2174 Codezeilen netto
  - 27.2 Arbeitstage fuer einen erfahrenen Entwickler
  - 10.9 Arbeitstage fuer ein 3er-Team (+20 % Koordination)

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
- Konservative Handarbeits-Basis fuer Code:
  - 1070 Codezeilen netto
  - 13.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 5.4 Arbeitstage fuer ein 3er-Team (+20 % Koordination)

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
- Konservative Handarbeits-Basis fuer den aktuellen Implementierungsstand:
  - 3524 Codezeilen netto
  - 44.1 Arbeitstage fuer einen erfahrenen Entwickler
  - 17.6 Arbeitstage fuer ein 3er-Team (+20 % Koordination)

### 4. `004-editor-file-help-streams`

- Status: Spezifikation, Checkliste und Planungsartefakte auf Branch
  `004-editor-file-help-streams` angelegt; `tasks.md` erstellt und bereit fuer
  den naechsten Implementierungsschritt
- Beobachtbarer Zeitraum: 2026-03-21 bis 2026-03-21
- Commit-Bild: 0 Commits an 0 Git-Aktivtagen; aktueller Stand liegt vollstaendig
  im Working Tree
- Grundlegende Arbeiten:
  - neue Phase-6-Spezifikation und Requirements-Checklist fuer Editor-, Datei-,
    Hilfe-, Stream- und Ressourcenkomponenten
  - Planungsartefakte `plan.md`, `research.md`, `data-model.md`,
    `quickstart.md` und `contracts/public-api.md`
  - zusaetzliche Plan-Review-Checkliste mit Durchfuehrungshinweisen
  - neue umsetzbare Aufgabenliste `tasks.md` mit Setup-, Foundation-,
    User-Story- und Polish-Phasen
  - nachgezogene Klarstellungen fuer Artefaktrollen, Safe-Close vs.
    Overwrite-Konflikt, Coverage-Gate-Interpretation, nicht-funktionale
    Abgrenzung und Szenarioabdeckung; Checkliste danach vollstaendig abgehakt
  - Synchronisation der gemeinsam genutzten Agent-Dateien auf den
    Post-Review-Planstand des aktiven Features
- Aktueller Working-Tree-Zuwachs:
  - Produktionscode: +0 / -0 / netto +0
  - Testcode: +0 / -0 / netto +0
  - Dokumentation: +262 / -0 / netto +262
- Konservative Handarbeits-Basis fuer Code:
  - 0 Codezeilen netto
  - 0.0 Arbeitstage fuer einen erfahrenen Entwickler
  - 0.0 Arbeitstage fuer ein 3er-Team (+20 % Koordination)

## Zusatz-Branches

| Branch | Letzte sichtbare Aktivitaet | Rolle |
|---|---|---|
| `origin/codex/add-project-constitution` | 2026-03-01 | Governance-Bootstrap fuer die erste Constitution |
| `origin/hindermath-patch-1` | 2026-03-01 | CI-Anpassung fuer Branch-Trigger |

## Einordnung der KI-/Spec-Kit-Wirkung

- Die beobachtbare Codebasis liegt bereits bei 9591 C#-Zeilen
  (Produktionscode + Tests), dazu kommen mehrere tausend Zeilen
  Spec-Kit-, Governance- und Betriebsdokumentation.
- Selbst mit der fuer klassische Entwicklung guenstigen Obergrenze von
  80 Codezeilen pro Tag ergibt sich bereits eine Untergrenze von
  119.9 Entwickler-Arbeitstagen nur fuer den Code.
- Da Spezifikationen, Plaene, Checklisten, Agent-Dateien, Constitution und
  sonstige Doku separat anfallen, liegt der klassische Gesamtaufwand real ueber
  dieser Untergrenze.
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
