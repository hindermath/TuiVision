# Pflichtenheft: TuiVision (Portierung Turbo Vision 2.0.3 nach C#/.NET 10, .NET Core)

## Arbeitsstatus und Leseschluessel
- [x] Nachweisbar erledigt anhand des aktuellen Repository-Stands
- [-] Begonnen oder teilweise nachgewiesen; Abnahmekriterium noch nicht vollstaendig erreicht
- [ ] Offen oder im Repository derzeit nicht belastbar nachweisbar

Hinweis:
Die Statusmarken in diesem Dokument basieren auf dem lokalen Repository-Stand vom 2026-03-23.
Externe GitHub-Einstellungen oder nicht versionierte Nachweisartefakte sind nur dann als erledigt markiert, wenn sie im Repository direkt belegbar sind.

## 1. Zweck und Geltungsbereich
Dieses Pflichtenheft beschreibt die technische Umsetzung der im Lastenheft definierten Anforderungen fuer das Projekt **TuiVision**.
Grundlage ist der Quellcode im Ordner `tv203s` (Turbo Vision 2.0.3, C/C++).

Ziel ist eine wartbare, testbare und dokumentierte Portierung nach **C#/.NET 10 (net10.0, .NET Core)** inklusive Beispielanwendungen.
TuiVision ist dabei ausdruecklich als **Beispielprojekt** zur Modernisierung mit **Agentic-AI/Agentic-UI** konzipiert.

## 2. Ausgangsbasis (Ist-Analyse)
- Lastenheft: `Lasten_Heft.md`
- Quellcodebasis: `tv203s/contrib/tvision`
- Herkunft des `tv203s/`-Bestands laut Lastenheft: Download von `https://sourceforge.net/projects/tvision/files/DOS_Win32/2.0.3/`
- Lokale Arbeitsumgebungen: macOS auf `MacBook Air M2` und `Mac mini M4 Pro`
- Zusaetzliche Kompatibilitaets- und Validierungsumgebungen: Windows und Linux; unter Windows insbesondere WSL mit aktuellem Ubuntu (derzeit bevorzugt `Ubuntu 24.04`)
- Auf beiden Systemen vorhanden und authentifiziert: `gh` (GitHub CLI), `codex`, `claude`, `copilot` und `gemini`; zusaetzlich ist GitHub Spec-Kit als verpflichtende Grundlage in allen vier KI-Agenten (`codex`, `claude`, `copilot`, `gemini`) installiert und nutzbar. Optional koennen `glab` (GitLab CLI) sowie die KI-Agenten `opencode` und JetBrains `junie` eingerichtet werden; falls `opencode` oder `junie` genutzt werden, ist GitHub Spec-Kit auch dort zu installieren und dokumentiert nutzbar zu halten
- Umfang der vorhandenen C/C++-Basis (Stand Analyse):
  - ca. 130 oeffentliche Header in `include/tv`
  - 105 Implementierungsdateien im Kernbereich `classes`
  - 9 plattformspezifische Treiberordner: `dos`, `linux`, `qnx4`, `qnxrtp`, `unix`, `win32`, `wingr`, `winnt`, `x11`
  - 25 Beispielprogramme in `examples`
- Das Git-Repository ist initialisiert; grundlegende C#-Projektstruktur (`TuiVision.sln`, Kernmodule in `src/`) ist bereits vorhanden.
- Portierung befindet sich in frueherer Phase; noch nicht alle Klassen und Beispiele sind portiert.

## 3. Zielprodukt
Bereitgestellt wird ein **reines .NET-Core-Framework** (Managed Code), das die zentralen Turbo-Vision-Konzepte fuer Text-UI in C# abbildet:
- Ereignisverarbeitung
- View-/Widget-Hierarchie
- Fenster, Dialoge, Menues, Statuszeile
- Text-/Editor-Komponenten
- Ressourcen/Streams
- Internationalisierung
- Beispielprogramme als Referenz und Regressionstest-Basis
- Keine nativen, betriebssystemspezifischen Bibliotheksabhaengigkeiten

## 4. Lieferumfang
Der Lieferumfang umfasst:

Statuscheckliste Lieferumfang:
- [x] C#-Portierung des Frameworks als Loesung mit klarer Projektstruktur
- [x] Unit-Tests mit MSTest fuer portierte Klassen/Methoden
- [x] API-Dokumentation mit docfx
- [-] Portierung der vorhandenen Beispielprogramme aus `tv203s/contrib/tvision/examples`
- [-] Build- und Qualitaetssicherungsprozesse
- [-] CI/CD-Workflow mit automatischem Build und Test auf GitHub Actions
- [ ] Nutzerdokumentation (Leitfaeden fuer Einstieg und Verwendung)
- [ ] Ausfuehrliche Dokumentation aller portierten Beispielprogramme
- [-] Ausreichende Quellcode-Dokumentation fuer Lern- und Wartungszwecke
- [-] Dokumentierter lokaler Workflow fuer `gh`, `codex`, `claude`, `copilot` und `gemini` auf beiden macOS-Systemen einschliesslich GitHub Spec-Kit in allen vier KI-Agenten; optionale Zusatzwerkzeuge `glab`, `opencode` und `junie` sind bei Nutzung ebenfalls dokumentiert

## 5. Anforderungen (MUSS)

Statuscheckliste Anforderungen:
- [x] `M-01` Lokales Git-Repository erstellen
- [x] `M-02` `.gitignore` fuer JetBrains, C#/.NET, VS Code, Visual Studio
- [x] `M-03` Remote auf GitHub unter `https://github.com/hindermath/TuiVision.git`
- [x] `M-04` Verwendung von C#/.NET 10 (.NET Core)
- [x] `M-05` Projektstruktur nach .NET-Best-Practices
- [-] `M-06` Portierung des Framework-Kerns aus `tv203s`
  Reihenfolgehinweis: Phasen 1 bis 6 sind sichtbar umgesetzt; Phase 7 und die Vollstaendigkeitsnachweise aus `M-07` fehlen noch.
- [ ] `M-07` Portierung der Implementierungsdateien aus `tv203s/contrib/tvision/classes`
  Reihenfolgehinweis: Vor dem Eingangstor aus Abschnitt 8.2 abschliessen und in `docs/porting-status.md` nachweisen.
- [x] `M-08` Unit-Tests fuer portierte Klassen/Methoden mit MSTest
- [x] `M-09` API-Dokumentation mit docfx
- [-] `M-10` Portierung der vorhandenen Beispiele
  Reihenfolgehinweis: Die 25 Originalbeispiele bleiben der MUSS-Umfang und werden erst nach bestandenem Eingangstor aus Abschnitt 8.2 in den Wellen 1 bis 4 abgearbeitet; die neu aufgenommenen TP7-Beispiele in `TVDEMOS/` und `TVFM/` sind danach als Anschlusswellen 5 und 6 vorgesehen.
- [-] `M-11` Qualitaetssicherung zusaetzlich zu Unit-Tests
- [x] `M-12` Keine nativen OS-Abhaengigkeiten
- [x] `M-13` Lizenz-Disclaimer fuer Beispielcharakter
- [-] `M-14` CI/CD mit GitHub Actions
  Reihenfolgehinweis: Build/Test ist vorhanden; das Dokumentations-Deployment aus `M-22` fehlt noch.
- [ ] `M-15` Nutzerdokumentation
- [-] `M-16` Vollstaendige XML-Kommentierung der oeffentlichen API
- [ ] `M-17` Einheitlicher didaktischer Dokumentationsstil
- [ ] `M-18` Ausfuehrliche Dokumentation der Beispielprogramme
- [-] `M-19` Ausreichende Quellcode-Dokumentation im gesamten TuiVision-Code
- [ ] `M-20` Messbarer Mindest-Testumfang (MUSS-Tests)
- [-] `M-21` Reproduzierbarer Multi-Mac-Entwicklungsworkflow
- [ ] `M-22` Automatische Veroeffentlichung der Dokumentation auf GitHub Pages

| ID | Anforderung | Technische Umsetzung | Abnahmekriterium |
|---|---|---|---|
| M-01 | Lokales Git-Repository erstellen | `git init`, sinnvolle Branch-/Commit-Strategie | Repository ist initialisiert, Historie nachvollziehbar |
| M-02 | `.gitignore` fuer JetBrains, C#/.NET, VS Code, Visual Studio | Kombination etablierter Ignore-Regeln | Keine IDE-/Build-Artefakte im Repo |
| M-03 | Remote auf GitHub unter `https://github.com/hindermath/TuiVision.git` | `origin` konfigurieren, Push faehig | Remote ist gesetzt, Push/Fetch funktionieren |
| M-04 | Verwendung von C#/.NET 10 (.NET Core) | SDK-Target `net10.0` in allen Projekten | `dotnet build` laeuft ohne Target-Konflikte |
| M-05 | Projektstruktur nach .NET-Best-Practices | Trennung in `src`, `tests`, `examples`, `docs` | Struktur ist konsistent, Build reproduzierbar |
| M-06 | Portierung des Framework-Kerns aus `tv203s` | API- und Verhaltensport in C# | Definierte Kernmodule sind funktional und testbar |
| M-07 | Portierung der Implementierungsdateien aus `tv203s/contrib/tvision/classes` | Alle `.cc`-Dateien aus dem Ordner `tv203s/contrib/tvision/classes` (z. B. `tview.cc`, `tgroup.cc`, `tapplica.cc`, `teditor.cc` u. v. m.) dienen als direkte C/C++-Vorlage fuer M-06; jede Datei wird gemaess Modulmapping (Abschnitt 7.2) einem Zielmodul (`TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` oder `TuiVision.Drivers.Console`) zugeordnet und portiert | Alle identifizierten `.cc`-Quelldateien aus `tv203s/contrib/tvision/classes` (einschliesslich der plattformspezifischen Unterordner fuer `TuiVision.Drivers.Console`) sind in den entsprechenden C#-Zielmodulen nachweisbar abgebildet und durch Unit-Tests abgesichert |
| M-08 | Unit-Tests fuer portierte Klassen/Methoden mit MSTest | Testprojekte pro Modul, CI-faehig | Tests laufen lokal und in CI stabil durch |
| M-09 | API-Dokumentation mit docfx | docfx-Konfiguration, API-Generierung und erneute Doku-Erstellung bei API-/XML-Kommentar-Aenderungen | Doku erzeugbar, verlinkt alle Kern-Namespaces und ist nach API-/XML-Aenderungen aktualisiert |
| M-10 | Portierung der vorhandenen Beispiele | Alle 25 Originalbeispiele aus `tv203s/contrib/tvision/examples` als .NET-Beispiele abbilden; zusaetzlich koennen die Anschlusswellen aus `TVDEMOS/` und `TVFM/` nachgelagert portiert werden | Die 25 Originalbeispiele bauen, bestehen die definierten Smoke-Tests und zeigen bei interaktiv gedachten Beispielen einen sichtbaren normalen Startpfad; die TP7-Anschlusswellen werden als Zusatzumfang separat verfolgt |
| M-11 | Qualitaetssicherung zusaetzlich zu Unit-Tests | Analyzer, Format- und Build-Gates | Qualitaets-Gates sind dokumentiert und aktiv |
| M-12 | Keine nativen OS-Abhaengigkeiten | Keine P/Invoke-/Native-Library-Pflicht, keine OS-spezifischen Zusatzpakete | Build/Tests laufen mit .NET 10 Runtime ohne native Zusatzinstallation |
| M-13 | Lizenz-Disclaimer fuer Beispielcharakter | Sichtbarer Hinweis in `LICENSE`/`README` | Hinweis beschreibt: Beispielprojekt, keine Konkurrenzabsicht, keine beabsichtigte Lizenzverletzung |
| M-14 | CI/CD mit GitHub Actions | Build-/Test-Workflow und Dokumentations-Deployment-Workflow unter `.github/workflows`; GitHub-Pages-Deployment gemaess M-22 ist Bestandteil der CI/CD-Pipeline; dabei werden die primaeren Entwicklungsumgebungen auf macOS und zusaetzliche Kompatibilitaetspruefungen fuer Linux und nach Moeglichkeit auch Windows/WSL beruecksichtigt | Automatischer Build und Testlauf pro Push/PR ist aktiv; GitHub-Pages-Deployment laeuft automatisch bei Doku-Aenderungen auf `main` (M-22); Kompatibilitaetsnachweise fuer Linux und Windows/WSL sind dokumentiert oder in der CI sichtbar |
| M-15 | Nutzerdokumentation | Pflichtguides unter `docs/guides/` gemaess Abschnitt 10.7; Quellenrangfolge und Adaptionspflicht gemaess Abschnitt 10.7 eingehalten; Sprache bilingual (Deutsch zuerst, Englisch) auf CEFR-B2-Niveau | Alle Pflichtguides unter `docs/guides/` sind vorhanden; Quellenrangfolge nachvollziehbar eingehalten; Sprache und Struktur entsprechen dem didaktischen Standard gemaess Abschnitt 10.3 und 10.7 |
| M-16 | Vollstaendige XML-Kommentierung der oeffentlichen API | Alle `public` Typen, Member, Parameter, Rueckgabewerte und Ausnahmen mit XML-Dokumentation | API ist durchgaengig und didaktisch ausfuehrlich kommentiert; docfx erzeugt daraus vollstaendige Referenzseiten |
| M-17 | Einheitlicher didaktischer Dokumentationsstil | Alle Dokumentationsartefakte folgen einem verbindlichen Lehr-/Beispielstandard fuer Fachinformatiker (Anwendungsentwicklung) | Struktur, Detailtiefe und Beispiele sind ueber alle Dokuarten konsistent und nachvollziehbar |
| M-18 | Ausfuehrliche Dokumentation der Beispielprogramme | Pro Beispielprogramm eigener Guide mit Lernziel, Voraussetzungen, Start, Bedienung, Architekturhinweisen und Uebungen | Alle portierten Beispiele sind didaktisch nachvollziehbar dokumentiert und reproduzierbar ausfuehrbar |
| M-19 | Ausreichende Quellcode-Dokumentation im gesamten TuiVision-Code | Nicht-triviale Logik, Architekturentscheidungen und interne Zusammenhaenge werden im Code nachvollziehbar kommentiert | Der Quellcode erfuellt die pruefbaren Kriterien aus Abschnitt 10.5 und ist fuer Fachinformatiker (Anwendungsentwicklung) lern- und wartbar |
| M-20 | Messbarer Mindest-Testumfang (MUSS-Tests) | Definierte Mindestabdeckung, Pflichttestfaelle und vollstaendige Smoke-Tests gemaess Abschnitt 9.4 | Die in Abschnitt 9.4 definierten Kennzahlen und Testumfaenge sind vollstaendig erreicht |
| M-21 | Reproduzierbarer Multi-Mac-Entwicklungsworkflow | Build-, Test-, GitHub- und KI-Agenten-Arbeitsablaeufe sind fuer `MacBook Air M2` und `Mac mini M4 Pro` mit `gh`, `codex`, `claude`, `copilot` und `gemini` dokumentiert; GitHub Spec-Kit ist in allen vier KI-Agenten verpflichtend installiert und in den Workflows beruecksichtigt. Optionale Zusatzwerkzeuge `glab`, `opencode` und `junie` werden bei Nutzung ebenfalls dokumentiert; fuer `opencode` und `junie` gilt dann ebenfalls die Spec-Kit-Pflicht. Windows und Linux, insbesondere WSL mit aktuellem Ubuntu, dienen ergaenzend als Kompatibilitaets- und Validierungsumgebungen | Die dokumentierten Schluesselablaeufe funktionieren auf beiden macOS-Systemen mit den dokumentierten Voraussetzungen und ggf. automatisierter Tool-Bereitstellung (z. B. DocFX als .NET-Tool); die Spec-Kit-gestuetzten Agentenablaeufe sind fuer `codex`, `claude`, `copilot` und `gemini` nachvollziehbar nachweisbar, bei Nutzung optional zusaetzlich auch fuer `opencode` und `junie`; Kompatibilitaetsnachweise fuer Linux und Windows/WSL sind reproduzierbar dokumentiert oder in der CI sichtbar |
| M-22 | Automatische Veroeffentlichung der Dokumentation auf GitHub Pages | Dedizierter Workflow `.github/workflows/docs-deploy.yml` gemaess Abschnitt 10.8: Trigger auf `push` nach `main` fuer Pfade `docs/**`, `src/**`, `docfx.json`; Schritte: checkout, dotnet-build (XML-Docs), docfx, `upload-pages-artifact`, `deploy-pages`; Repository-Setting auf Source „GitHub Actions"; Umgebung `github-pages` | Workflow existiert gemaess Abschnitt 10.8; docfx-Dokumentation ist ueber GitHub Pages erreichbar; Deployment laeuft automatisch nach jedem relevanten Merge auf `main`; Deployment-Status im Actions-Tab sichtbar |

## 6. Optionale Anforderungen (KANN / Pruefauftraege)

Statuscheckliste optionale Anforderungen:
- [ ] `O-01` NuGet-Paketierung

| ID | Option | Umsetzung | Ergebnisartefakt |
|---|---|---|---|
| O-01 | NuGet-Paketierung | Pack-/Versionierungsprozess | erzeugbares `.nupkg` |

## 7. Zielarchitektur und Projektstruktur

### 7.1 Loesungsstruktur (Soll)
```text
TuiVision.sln
src/
  TuiVision.Core/
  TuiVision.Controls/
  TuiVision.Drivers.Console/
  TuiVision.Serialization/
  TuiVision.Compatibility/
tests/
  TuiVision.Core.Tests/
  TuiVision.Controls.Tests/
  TuiVision.Drivers.Tests/
  TuiVision.Examples.SmokeTests/
examples/
  (portierte Beispielprogramme)
docs/
  docfx.json
  api/
  guides/
```

### 7.2 Modulmapping von C/C++ nach C#
| Quelle `tv203s` | Zielmodul |
|---|---|
| `include/tv/*.h`, `classes/t*.cc` | `TuiVision.Core`, `TuiVision.Controls` |
| `classes/*` (streams, resource, file/help/editor) | `TuiVision.Serialization`, `TuiVision.Core` |
| `classes/dos`, `classes/linux`, `classes/unix`, `classes/win32`, ... | Konsolidierung in `TuiVision.Drivers.Console` (managed, ohne native Bindings) |
| `compat/*` | `TuiVision.Compatibility` |
| `intl/*` | `TuiVision.Core` (I18N-Teil) |
| `examples/*` | `examples/*` in C# |

## 8. Portierungsstrategie

### 8.1 Reihenfolge (inkrementell)
- [x] **1. Basisinfrastruktur**: Loesung, Build, Tests, Doku-Pipeline
  Reihenfolgehinweis: abgeschlossen; Grundlage fuer alle Folgephasen.
- [x] **2. Kernobjekte**: `TObject`, Collections, Geometrie (`TPoint`, `TRect`), Events
  Reihenfolgehinweis: abgeschlossen; Voraussetzung fuer View- und Event-System.
- [x] **3. View-System**: `TView`, `TGroup`, Zeichenpuffer, Fokus/States
  Reihenfolgehinweis: abgeschlossen; Voraussetzung fuer App-Rahmen und Controls.
- [x] **4. Anwendungsrahmen**: `TProgram`, `TApplication`, Menues, Statuszeile, Desktop
  Reihenfolgehinweis: abgeschlossen; Voraussetzung fuer Welle 1 der Beispiele.
- [x] **5. Dialog-/Control-Schicht**: Eingabezeilen, Listen, Scrollbars, Buttons, usw.
  Reihenfolgehinweis: abgeschlossen; Voraussetzung fuer Welle 2 der Beispiele.
- [x] **6. Editor/Datei/Hilfe/Streams**: Editor, Resource-, Stream- und Help-Komponenten
  Reihenfolgehinweis: abgeschlossen; Voraussetzung fuer Welle 3 der Beispiele.
- [x] **7. Treiberkonsolidierung**: Managed Console-Treiber unter .NET Core ohne native OS-Bindings
  Reihenfolgehinweis: als naechstes fachliches Hauptpaket vorziehen; Voraussetzung fuer Welle 4 und fuer den Vollstaendigkeitsnachweis aus `M-07`.
- [ ] **8. Beispiele**: Portierung aller 25 MUSS-Beispiele in vier thematisch-technischen Wellen; anschliessend optionale Anschlusswellen 5 und 6 fuer die zusaetzlich im Repository abgelegten TP7-Beispiele aus `TVDEMOS/` und `TVFM/`; jede Welle wird erst begonnen, wenn die zugehoerigen Framework-Phasen abgeschlossen sind (Abhaengigkeitsprinzip); **vor Welle 1 ist das Eingangstor gemaess Abschnitt 8.2 vollstaendig zu bestehen**
  Reihenfolgehinweis: erst nach Phase 7 und bestandenem Eingangstor starten; TP7-Anschlusswellen erst nach den vier MUSS-Wellen.

### 8.2 Eingangstor Phase 8: Framework-Vollstaendigkeitsnachweis (verpflichtend vor Welle 1)

Bevor das erste Beispielprogramm portiert wird (Welle 1, Abschnitt 8.3), muessen alle nachfolgenden Kriterien nachweisbar erfuellt sein. Jedes offene Kriterium blockiert den Start von Phase 8 und ist als Issue im Repository zu erfassen und zu schliessen.

Statuscheckliste Eingangstor:
- [x] Kriterium 1 - M-07-Vollstaendigkeitsnachweis (Mapping-Tabelle)
  Reihenfolgehinweis: abgeschlossen; `docs/porting-status.md` bildet alle 151 historischen `.cc`-Dateien final ab.
- [x] Kriterium 2 - Keine portierten Luecken ohne Begruendung
  Reihenfolgehinweis: abgeschlossen; nicht portierte Faelle sind als `bewusst ausgelassen + Begruendung` dokumentiert.
- [x] Kriterium 3 - Build-Gate
- [x] Kriterium 4 - Test-Gate
  Reihenfolgehinweis: abgeschlossen; `dotnet test` ist gruen, und es gibt keine dokumentationspflichtigen Skip-/Ignored-Faelle.
- [x] Kriterium 5 - Coverage-Gate
  Reihenfolgehinweis: abgeschlossen; `TuiVision.Core` `89,11 %`, `TuiVision.Controls` `84,10 %`, `TuiVision.Serialization` `83,33 %`, `TuiVision.Compatibility` `80,95 %` und `TuiVision.Drivers.Console` `97,43 %` Line Coverage.
- [x] Kriterium 6 - API-Doku-Gate
  Reihenfolgehinweis: abgeschlossen; `docfx docfx.json` lief im 006-Closure-Paket fehlerfrei.
- [x] Nachweisdokument
  Reihenfolgehinweis: dedizierter Closure-Commit `docs: close phase-8 entrance gate for feature 006`.

#### Kriterium 1 – M-07-Vollstaendigkeitsnachweis (Mapping-Tabelle)

Die Datei `docs/porting-status.md` muss existieren und fuer jede `.cc`-Quelldatei aus `tv203s/contrib/tvision/classes` (einschliesslich plattformspezifischer Unterordner) einen Eintrag enthalten mit:
- Quelldatei (relativer Pfad in `tv203s/`)
- Zugeordnetes C#-Zielmodul (`TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` oder `TuiVision.Drivers.Console`)
- Zugehoerige C#-Zieldatei(en)
- Teststatus (`portiert + getestet` / `portiert + Test ausstehend` / `bewusst ausgelassen + Begruendung`)

Kein Eintrag darf den Status `ausstehend` oder `TODO` ohne zugehoerigen offenen Issue tragen.

#### Kriterium 2 – Keine portierten Luecken ohne Begruendung

Jede `.cc`-Datei, die nicht portiert wurde, muss in `docs/porting-status.md` mit einer nachvollziehbaren Begruendung als `bewusst ausgelassen` markiert sein (z. B. plattformspezifischer Treiber, der durch `TuiVision.Drivers.Console` ersetzt wird).

#### Kriterium 3 – Build-Gate

`dotnet build --configuration Release` laeuft ohne Fehler und ohne als Fehler konfigurierte Warnungen fuer alle Projekte in `src/`.

#### Kriterium 4 – Test-Gate

Alle Unit-Tests in `tests/` laufen gruen durch, lokal und in CI (M-08). Kein Test darf als `Ignored` oder `Skip` markiert sein, ohne einen zugehoerigen offenen Issue.

#### Kriterium 5 – Coverage-Gate

Line Coverage in `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` betraegt jeweils mindestens 70 % (gemaess §9.4 Nr. 1), gemessen mit Coverlet. Das Coverage-Ergebnis ist als CI-Artefakt oder lokaler Report nachweisbar.

#### Kriterium 6 – API-Doku-Gate

`docfx` laeuft ohne Fehler durch; keine oeffentliche API ist undokumentiert (M-09, M-16). Das erzeugte Ausgabeverzeichnis ist im Repository oder als CI-Artefakt vorhanden.

#### Nachweisdokument

Der Abschluss des Eingangstors wird durch den dedizierten Commit
`docs: close phase-8 entrance gate for feature 006` auf dem Feature-Branch
dokumentiert. Diese Commit-Nachricht verweist implizit auf die sechs erfuellten
Kriterien und auf `docs/porting-status.md` als kanonische Nachweisflaeche.

### 8.3 Beispielprogramme (MUSS-Umfang)
Zu portieren sind alle 25 vorhandenen Beispielordner, eingeteilt in vier Wellen nach technischer Abhaengigkeit.
Fuer jedes portierte Beispiel ist eine eigene didaktische Dokumentationsseite in `docs/guides/examples/` bereitzustellen.
Die nachfolgend aufgefuehrten Turbo-Pascal-Beispiele aus dem Repository (`TVDEMOS/`, `TVFM/`) erweitern diesen Umfang als Anschlusswellen, ersetzen aber nicht den MUSS-Nachweis fuer die 25 Originalbeispiele aus `tv203s/contrib/tvision/examples`.
Fuer groessere Beispielwellen ist ein zweistufiges Spec-Kit-Liefermuster zulaessig und bevorzugt, wenn Portierungs-/Nachweisarbeit und interaktive Runtime-Politur sonst zu gross fuer einen einzelnen Feature-Lauf wuerden: Stufe 1 liefert den funktionalen Portierungs- und Smoke-Test-Nachweis, Stufe 2 macht dieselben Funktionen ueber sichtbare Menues, Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und UI-Event-Smoke-Tests als echte Demo bedienbar. Eine Welle gilt fuer Lernende und manuelle Reviews erst nach der interaktiven Showcase-Stufe als vollstaendig reif, sofern der Scope nicht ausdruecklich nur einen minimalen nicht-interaktiven Nachweis verlangt.

Statuscheckliste Beispielwellen:
- [x] **Welle 1 - Grundlegende Anwendungsstruktur**
  Reihenfolgehinweis: abgeschlossen (Branch `007-port-wave1-examples`); 4 Beispiele portiert, 41 Smoke-Tests gruen, Guides geliefert.
- [x] **Welle 1 - Funktionaler Nachweisnachlauf**
  Reihenfolgehinweis: abgeschlossen (Branch `014-wave1-functional-hardening`); Desklogo, MsgCls, Tutorial und Videomode sind fachlich gegen die historischen Quellen, gehaertete Smokes, Helper-Klassifikation und `specs/014-wave1-functional-hardening/pr-evidence.md` nachgewiesen.
- [x] **Didaktische Inline-Code-Kommentar-Haertung**
  Reihenfolgehinweis: abgeschlossen (Branch `015-didactic-comment-hardening`); zentrale Framework-Flows und Smoke-Test-Helfer wurden selektiv didaktisch gehaertet und in `specs/015-didactic-comment-hardening/pr-evidence.md` nachgewiesen.
- [x] **Secure-Development-Hardening**
  Reihenfolgehinweis: abgeschlossen (Branch `016-secure-development-hardening`); die `docs/secure-development/`-Basis, Governance-Anwendbarkeit, 157 Kontrollen und projektweite Sicherheitsnachweise wurden geprueft und gehaertet.
- [x] **Welle 1 - Sichtbarer Komponenten-Nachweis**
  Reihenfolgehinweis: abgeschlossen (Branch `017-wave1-visual-component-remediation`); Welle 1 und Welle 2 besitzen denselben sichtbaren Drei-Schichten- und App-Loop-Qualitätsmaßstab.
- [x] **Welle 2 - Controls und Dialoge**
  Reihenfolgehinweis: Portierungs-/Smoke-Nachweis abgeschlossen (Branch `011-port-wave2-examples`); 11 Controls-/Dialog-Beispiele portiert, alle 15 gelieferten Beispiele per Smoke-Test abgedeckt, Guides geliefert. Die interaktive Showcase-Stufe wurde im Follow-up `012-interactive-wave2-demos` nachgezogen.
- [x] **Welle 2 - Interaktive Showcase-Stufe**
  Reihenfolgehinweis: abgeschlossen (Branch `012-interactive-wave2-demos`); die elf Wave-2-Beispiele zeigen beim normalen Start sichtbare Menue-/Command-Pfade mit text-first Rueckmeldung und besitzen app-loop-basierte Smoke-Nachweise.
- [x] **Welle 2 - Sichtbarer Komponenten-Nachweis**
  Reihenfolgehinweis: abgeschlossen (Branch `013-wave2-visual-component-remediation`); die elf Wave-2-Beispiele besitzen echte sichtbare Hauptkomponenten, echte `TStatusLine`-Rueckmeldung, `Help -> Description` und primaere App-Loop-Smokes mit View-Baum- plus Buffer-/Cell-Nachweis.
- [x] **Welle 3 - Editor-/Hilfe-/Ressourcen-Vorhaertung**
  Reihenfolgehinweis: abgeschlossen (Branch `018-editor-help-resources-hardening`); Editor-/Datei- und Runtime-Help-Flows sind zusammenhaengend nachgewiesen, der Help-Source-Compiler und sprachabhaengige Resource-Lookup sind wiederverwendbar gehaertet.
- [x] **Welle 3 - Visual Component Porting**
  Reihenfolgehinweis: abgeschlossen (Branch `019-wave3-visual-component-porting`); `tvedit`, `bhelp`, `helpdemo`, `tvhc` und `i18n` sind sichtbare Drei-Schichten-Demos mit App-Loop- und Buffer-/Cell-Proof.
- [x] **Maussupport und Interaktions-Härtung**
  Reihenfolgehinweis: abgeschlossen (Branch `020-mouse-support-interaction`); begrenzter SGR-1006-Ingress, Fokus/Aktivierung, Doppelklick, ein Titelzeilen-Drag und vollständige Tastaturfallbacks sind frameworkweit nachgewiesen.
- [x] **Welle 4 - Terminal-/Charset-/Plattform-Vorhaertung**
  Reihenfolgehinweis: abgeschlossen (Branch `021-terminal-charset-hardening`); kontrollierte Terminal-Session, begrenztes Emulations-Subset, KOI8-R-/Unicode-Mapping, rohe 8x16-Fixture, geschlossene Profile und App-Loop-/Cell-Proof sind frameworkweit nachgewiesen.
- [x] **Welle 4 - Visual Component Porting**
  Reihenfolgehinweis: abgeschlossen (Branch `022-wave4-visual-component-porting`); `terminal`, `cyrillic`, `fonts`, `eterm` und `xterm` sind sichtbare Drei-Schichten-Demos mit App-Loop-, Zustands-, View- und Buffer-/Cell-Proof sowie ehrlichen Host-Fallbacks.
- [ ] **Welle 5 - Turbo-Pascal-Demos aus TP7 (`TVDEMOS/`)**
  Reihenfolgehinweis: erst nach Abschluss der MUSS-Wellen 1 bis 4; bevorzugt nach stabilen Portierungen von `tvdemo`, `tvedit`, Hilfesystem und Dialogschicht.
- [ ] **Welle 6 - Turbo-Pascal-Dateimanager `TVFM/`**
  Reihenfolgehinweis: zuletzt; setzt die Erkenntnisse aus Welle 5, Datei-/Verzeichnisdialoge, Drag/Drop-Analoga und eine robuste Event-/Fensterintegration voraus.
- [ ] Beispiel-Guides unter `docs/guides/examples/`
  Reihenfolgehinweis: jedes portierte Beispiel im selben Arbeitsgang mit eigenem Guide dokumentieren.

**Welle 1 – Grundlegende Anwendungsstruktur** (nach Abschluss Phase 4: Anwendungsrahmen)

Benoetigt: `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, `TStatusLine`.
Keine Controls, keine Dialoge, kein Editor, kein Hilfesystem.

Checkliste Welle 1:
- [x] `desklogo` - Minimale App: statisches Logo auf dem Desktop
- [x] `msgcls` - Benutzerdefinierte Ereignisklassen und Nachrichtenverarbeitung
- [x] `tutorial` - Schrittweise Einfuehrung in die TuiVision-Grundkonzepte (16 Schritte, token-basiert)
- [x] `videomode` - Wechsel von Anzeigemodi (Pufferbreite/-hoehe)

**Welle 2 – Controls und Dialoge** (nach Abschluss Phase 5: Dialog-/Control-Schicht)

Benoetigt: Eingabezeilen, Listen, Scrollbars, Buttons, Checkboxes, RadioButtons, Dialoge.

> Hinweis: `sdlg`/`sdlg2` konsumieren das neue managed `TScrollGroup` (siehe `docs/architecture/adr/0001-tscrollgroup-foundation.md`). / Note: `sdlg`/`sdlg2` consume the new managed `TScrollGroup` (see `docs/architecture/adr/0001-tscrollgroup-foundation.md`).

Checkliste Welle 2:
- [x] `clipboard` - Zwischenablage-Integration in Controls
- [x] `demo` - Vollstaendige Turbo-Vision-Kerndemo (zeigt alle Basis-Controls)
- [x] `dlgdsn` - Dialog-Designer: dynamisch zusammengesetzte Dialoge
- [x] `dyntxt` - Dynamisch erzeugter Text in Views
- [x] `inplis` - Eingabelisten mit `TInputLine`
- [x] `listvi` - Listenansichten mit `TListViewer`
- [x] `progba` - Einfacher Fortschrittsbalken
- [x] `sdlg` - Scrollbarer Dialog mit `ScrollDialog`/`ScrollGroup` und vertikalem Scrollen
- [x] `sdlg2` - Erweiterter scrollbarer Dialog mit horizontalem und vertikalem Scrollen
- [x] `tcombo` - Kombinationsfelder (`TComboBox`)
- [x] `tprogb` - Erweiterter Fortschrittsbalken mit Abbruch

Abgrenzung: Die Welle-2-Checkliste dokumentiert den funktionalen Portierungs- und Smoke-Test-Nachweis aus `011-port-wave2-examples`. Die interaktive Demo-Reife fuer normale CLI-Starts wurde in `012-interactive-wave2-demos` als eigene Showcase-Stufe nachgezogen. Der sichtbare Komponenten-Nachweis aus `013-wave2-visual-component-remediation` schaerft die Abnahme: echte Hauptkomponente, echte `TStatusLine`, `Help -> Description` und gerenderter Buffer-/Cell-Nachweis sind nun dokumentiert.

**Welle 3 – Editor, Dateien, Hilfe und Streams** (nach Abschluss Phase 6: Editor/Datei/Hilfe/Streams)

Benoetigt: `TEditor`, `TFileDialog`, `THelpViewer`, Stream- und Ressourcen-Infrastruktur.

Die Vorhaertung aus
`Lastenheft_03_EditorHelpAndResourcesHardening.018-editor-help-resources-hardening.md`
ist abgeschlossen. `tvedit`, `bhelp`, `helpdemo`, `tvhc` und `i18n` koennen
deshalb als sichtbare Drei-Schichten-Demos auf den nachgewiesenen Editor-,
Help-, Compiler-, Resource- und i18n-Vertraegen aufbauen.

Checkliste Welle 3:
- [ ] `bhelp` - Grundlegendes Hilfesystem mit kontextsensitiven Themen
- [ ] `helpdemo` - Interaktive Demonstration des Hilfesystems
- [ ] `i18n` - Internationalisierung: mehrsprachige Texte und Ressourcen
- [ ] `tvedit` - Vollstaendiger Texteditor (Datei oeffnen, bearbeiten, speichern)
- [ ] `tvhc` - Hilfe-Compiler: Konvertierung von Quelltext in binaere Hilfedatei

**Welle 4 – Terminal-Emulation und erweiterte Zeichensaetze** (nach Abschluss Phase 7: Treiberkonsolidierung)

Benoetigt: Managed Console-Treiber, plattformunabhaengige Zeichensatz- und Terminalpufferverwaltung.

Vor der sichtbaren Beispielportierung ist die eigene Wave-4-Vorhaertung aus
`Lastenheft_05_TerminalCharsetAndEmulation.md` verpflichtend abzuarbeiten oder
im Spec-Kit-Plan als explizite vorgelagerte Task-Gruppe zu fuehren. Diese
Vorhaertung muss Terminal-Session-Vertrag, strukturierte Buffer-/Cell-Proofs,
Charset-/Font-Mapping, Resource-/Config-Fallbacks, Plattformgrenzen fuer
Multi-Mac, Linux und Windows/WSL sowie klare Sicherheitsgrenzen gegen
persistente Host-Terminal-Manipulationen festlegen. Erst danach kann
`Lastenheft_Wave4-Visual-Component-Porting.md` die sichtbaren Beispiele
portieren.

Checkliste Welle 4:
- [ ] `cyrillic` - Kyrillische Zeichensatz-Unterstuetzung im Textpuffer
- [ ] `eterm` - Erweiterter Terminal-Emulator
- [ ] `fonts` - Zeichensatz-Verwaltung und Darstellung alternativer Fonts
- [ ] `terminal` - Einfache Terminal-Integration
- [ ] `xterm` - XTerm-Protokoll-Emulation

**Welle 5 – Turbo-Pascal-Demos aus TP7 (`TVDEMOS/`)** (Anschlusswelle nach Abschluss der MUSS-Wellen 1 bis 4)

Portierungsmeinung:
Die Portierung von Turbo-Pascal-Beispielen nach C# ist grundsaetzlich gut machbar. Turbo Pascal und C# liegen fuer diesen Einsatzzweck semantisch naeher beieinander als C++ und C#: die Pascal-Turbo-Vision-Beispiele arbeiten meist klarer mit Units, Records/Objects, Menues, Dialogen und Ereignissen. Zusatzauswand entsteht vor allem bei DOS-/CRT-Annahmen, Dateisystemdetails, Ressourcenformaten und dem alten Pascal-Objektmodell.

Benoetigt: stabile Dialog-/Control-Schicht, Editor-/Hilfe-/Datei-Infrastruktur, saubere Abbildung von Pascal-Object/Unit-Strukturen auf C#-Klassen und Namespaces.

Checkliste Welle 5:
- [ ] `TVDEMO.PAS` - grosse Turbo-Vision-Gesamtdemo als Pascal-Gegenstueck zu `demo`
- [ ] `TVEDIT.PAS` - Pascal-Editorbeispiel als zusaetzlicher Validierer fuer den Editor-Stack
- [ ] `TVHC.PAS`, `HELPFILE.PAS`, `DEMOHELP.PAS` - Pascal-Hilfesystem und Hilfe-Compiler-Flows
- [ ] `CALC.PAS`, `CALENDAR.PAS`, `ASCIITAB.PAS`, `PUZZLE.PAS` - in sich geschlossene Fach-/Widget-Demos
- [ ] `GADGETS.PAS`, `MOUSEDLG.PAS`, `TVRDEMO.PAS`, `GENRDEMO.PAS` - Interaktions-, Maus- und Ressourcen-Demos

**Welle 6 – Turbo-Pascal-Dateimanager `TVFM/`** (Anschlusswelle nach Welle 5)

Benoetigt: robuste Datei-/Verzeichnisnavigation, View-/Window-Komposition, Ressourcenverwaltung, zusaetzliche Portierungsentscheidungen fuer Dateimanager-spezifische Arbeitsablaeufe.

Checkliste Welle 6:
- [ ] `TVFM.PAS` - Hauptanwendung des Dateimanagers
- [ ] `DIRVIEW.PAS`, `TREEWIN.PAS`, `FILEVIEW.PAS`, `VIEWTEXT.PAS`, `VIEWHEX.PAS`, `INFOVIEW.PAS` - Navigations- und Viewer-Komponenten
- [ ] `FILECOPY.PAS`, `FILEFIND.PAS`, `DRAGDROP.PAS`, `TRASH.PAS` - Dateioperationen und Interaktionsfluesse
- [ ] `COLORS.PAS`, `EDITPAL.PAS`, `GAUGES.PAS`, `TOOLS.PAS`, `GLOBALS.PAS`, `ASSOC.PAS`, `EQU.PAS` - Hilfs- und Konfigurationsmodule
- [ ] Ressourcen- und Build-Begleitdateien (`MAKERES.PAS`, `MAKETVFM.BAT`, `.PAL`, `.TVR`) dokumentiert sichten und als C#-kompatible Assets/Generatoren neu zuschneiden

## 9. Test- und Qualitaetskonzept

### 9.1 Testarten
- [x] **Unit-Tests (MSTest)** fuer alle portierten Klassen/Methoden
- [x] **Integrations-/Verhaltenstests** fuer Event-Loop, Fokus, Menue-/Dialogfluss
- [-] **Snapshot-/Golden-Tests** fuer Render-/Zeichenpuffer-Verhalten
- [ ] **Smoke-Tests** fuer alle portierten Beispiele
- [-] **Runtime-Kompatibilitaetstests** fuer .NET 10 (net10.0) ohne native Zusatzkomponenten auf macOS, Linux sowie ergaenzend Windows/WSL
- [ ] **MUSS-Tests**: Sammelbegriff fuer alle in Abschnitt 9.4 als verpflichtend markierten Tests und Kennzahlen
- [-] **Umgebungs-/Workflow-Checks** fuer lokale Arbeit auf `MacBook Air M2` und `Mac mini M4 Pro` sowie fuer ergaenzende Kompatibilitaetspruefungen auf Linux und Windows/WSL

### 9.2 Mindest-Qualitaetsgates
- [x] Build ohne Warnungsfehler fuer freigegebene Konfiguration
- [ ] Alle MUSS-Tests erfolgreich
- [x] API-Doku erfolgreich generierbar
- [-] Bei Aenderungen an oeffentlicher API oder XML-Kommentaren wird die docfx-Dokumentation im selben Arbeitsgang neu erzeugt
- [x] Einheitliche Formatierung/Analyzer-Regeln aktiv
- [ ] Fehlende XML-Kommentare fuer oeffentliche API werden als Qualitaetsverstoss behandelt (z. B. CS1591 nicht ignoriert)
- [ ] Dokumentations-Review gegen den didaktischen Standard gemaess Abschnitt 10.3 ist fuer Releases verpflichtend
- [ ] Beispielprogramme gelten erst als abgeschlossen, wenn die zugehoerigen Guides gemaess Abschnitt 10.4 vorliegen
- [ ] Quellcode-Review gegen den Standard gemaess Abschnitt 10.5 ist fuer Releases verpflichtend
- [ ] GitHub-Pages-Deployment der docfx-Dokumentation ist nach jedem Merge auf `main` mit Aenderungen an `docs/`, `src/` oder `docfx.json` automatisch erfolgreich abgeschlossen (M-22)

### 9.3 Rueckverfolgbarkeit
Jede MUSS-Anforderung (M-xx) wird mindestens einem Nachweisartefakt zugeordnet.
Zulaessige Nachweisartefakte sind Testfall, Smoke-Test, Build-/CI-Nachweis, Dokumentationsartefakt oder Checklisten-/Review-Nachweis.

### 9.4 Verbindlicher Mindest-Testumfang (MUSS-Tests)
Die folgenden Punkte sind verpflichtend und bilden die "MUSS-Tests" im Sinne dieses Pflichtenhefts:
- [-] Unit-Test-Abdeckung (Line Coverage) von mindestens 70% in `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console`.
  Reihenfolgehinweis: Fuer den Gesamtabschluss muessen die finalen Coverlet-Nachweise fuer alle fuenf Gate-Module gemeinsam vorliegen.
- [-] Fuer jede portierte Kernkomponente existieren mindestens ein Positivtest und ein Negativ-/Fehlerfalltest (sofern fachlich sinnvoll).
- [x] Integrations-/Verhaltenstests decken mindestens Event-Loop, Fokuswechsel, Menueausfuehrung und Dialoginteraktion ab.
- [ ] Smoke-Tests laufen fuer alle 25 portierten Beispielprogramme automatisiert in CI.
- [ ] Alle MUSS-Tests laufen in GitHub Actions auf jedem Pull Request und auf dem Hauptbranch.
- [-] Die dokumentierten lokalen Schluesselablaeufe mit `gh`, `codex`, `claude`, `copilot` und `gemini` sind auf `MacBook Air M2` und `Mac mini M4 Pro` nachvollziehbar und reproduzierbar; GitHub Spec-Kit ist dabei in allen vier KI-Agenten installiert und Bestandteil der nachgewiesenen Agentenablaeufe. Falls `glab`, `opencode` oder `junie` genutzt werden, sind auch diese Ablaeufe dokumentiert; fuer `opencode` und `junie` ebenfalls mit installiertem Spec-Kit. Linux- und Windows/WSL-Kompatibilitaetschecks sind als zusaetzliche Validierung nachgewiesen oder eingeplant.
- [x] Bei API-/XML-Kommentar-Aenderungen ist die erfolgreiche docfx-Erzeugung als Nachweisartefakt vorhanden.

## 10. Dokumentation
- [x] API-Dokumentation mit docfx aus dem Code
- [ ] Architektur- und Migrationsdokumentation in `docs/guides`
- [ ] Changelog fuer Portierungsfortschritt und Abweichungen vom Originalverhalten
- [ ] Saemtliche Projektdokumentation folgt einem einheitlichen didaktischen Lehr-/Beispielstandard
- [-] Der Quellcode selbst ist didaktisch und wartungsorientiert dokumentiert
- [-] Lokale Arbeitsablaeufe mit `gh`, `codex`, `claude`, `copilot` und `gemini` sind fuer beide macOS-Systeme dokumentiert; GitHub Spec-Kit ist als Pflichtbestandteil in allen vier KI-Agenten beruecksichtigt. Optionale Zusatzwerkzeuge `glab`, `opencode` und `junie` sind bei Nutzung ebenfalls dokumentiert; fuer `opencode` und `junie` mit Spec-Kit.

## 10.1 XML-Dokumentationsstandard fuer die API (verbindlich)
Fuer den Lehr- und Beispielcharakter des Projekts gelten folgende verbindliche Regeln:
1. Alle oeffentlichen Bestandteile der TuiVision-API sind mit XML-Kommentaren zu versehen.
2. Die Kommentare sind ausfuehrlich und didaktisch zu schreiben (Zweck, Verhalten, Randfaelle, Nebenwirkungen, typische Nutzung).
3. Pro oeffentlichem API-Element sind mindestens `summary` sowie bei Bedarf `remarks` zu verwenden.
4. Bei Methoden sind `param` fuer alle Parameter und `returns` fuer Rueckgabewerte verpflichtend.
5. Erwartete Fehlerfaelle sind mit `exception` zu dokumentieren.
6. Wo sinnvoll sind `example`-Abschnitte fuer Fachinformatiker (Anwendungsentwicklung) bereitzustellen.
7. Docfx muss diese XML-Kommentare ohne Luecken in der API-Referenz verarbeiten.
8. Wenn API-Signaturen oder XML-Kommentare geaendert werden, muss die docfx-Ausgabe im selben Arbeitsgang neu erstellt werden.

## 10.2 Lizenz- und Disclaimer-Text (verbindlich)
Im Projekt muss ein klarer Hinweis enthalten sein, der mindestens folgende Punkte abdeckt:
1. TuiVision ist ein Lern-/Beispielprojekt zur Modernisierung von Turbo-Vision-Konzepten mit C#/.NET 10 und Agentic-AI.
2. Das Projekt beabsichtigt keine Verletzung von Rechten oder Lizenzen des Originalprojekts Turbo Vision.
3. Das Projekt verfolgt keine Konkurrenzabsicht gegenueber Turbo Vision.
4. Es handelt sich nicht um eine offizielle Fortfuehrung oder ein offiziell verbundenes Produkt.
5. Fuer uebernommene Originalquellen gelten deren jeweilige Lizenz- und Rechtehinweise weiterhin.

## 10.3 Didaktischer Standard fuer gesamte Dokumentation (verbindlich)
Der folgende Standard gilt fuer alle Dokumentationsartefakte (API-Referenz, Guides, Architektur, README, Changelog):
1. Zielgruppe ist Fachinformatiker (Anwendungsentwicklung) mit Lern- und Praxisfokus.
2. Jede Dokumentationsart folgt einer passenden Mindeststruktur:
   Guides mit Zweck, Voraussetzungen, Schritten und Ergebnis;
   API-Referenz mit Signatur und Verhalten;
   Changelog mit Aenderung, Motivation und Auswirkung.
3. Fachbegriffe werden kontextbezogen erklaert; Abkuerzungen werden beim ersten Auftreten aufgeloest.
4. Inhalte enthalten dort, wo fachlich sinnvoll, nachvollziehbare Beispiele (Code, Ablauf oder Nutzungsszenario), nicht nur Theorie.
5. Entscheidungen und Trade-offs werden begruendet, damit technische Hintergruende lernbar sind.
6. Komplexe Themen werden schrittweise von Grundlagen zu Details aufgebaut.
7. Die Struktur ist ueber alle Dokumente konsistent (einheitliche Kapitel- und Benennungslogik).
8. Dokumentation wird bei API- oder Verhaltensaenderungen im selben Arbeitsgang aktualisiert.

## 10.4 Standard fuer Beispielprogramm-Dokumentation (verbindlich)
Fuer jedes portierte Beispielprogramm ist eine eigene Dokumentationsseite mit mindestens folgenden Inhalten verpflichtend. Dies gilt sowohl fuer die 25 MUSS-Beispiele aus `examples/` als auch fuer spaeter portierte TP7-Anschlussbeispiele aus `TVDEMOS/` und `TVFM/`:
1. Ziel und Lernnutzen des Beispiels (welche Konzepte werden vermittelt).
2. Voraussetzungen (SDK, Build-Schritte, ggf. Eingabedaten).
3. Startanleitung (Befehle, erwartete Konsolenausgabe/Verhalten).
4. Bedienung und typische Interaktionen im Beispiel.
5. Technische Einordnung (relevante Klassen, Events, Datenfluss).
6. Varianten oder Uebungen fuer Lernende (z. B. Erweiterungsaufgaben).
7. Typische Fehlerbilder und Troubleshooting-Hinweise.
8. Verweis auf Quellcode und zugehoerige Tests.

## 10.5 Standard fuer Quellcode-Dokumentation (verbindlich)
Fuer den Lehr- und Beispielcharakter gilt fuer den TuiVision-Quellcode:
1. Jede fachlich gepflegte Quelldatei in `src/` beginnt mit einem kurzen Datei-/Modulkommentar (1 bis 3 Zeilen, Deutsch zuerst, Englisch danach). Der Kommentar benennt mindestens: a) die Hauptverantwortung der Datei, b) ihren technischen oder fachlichen Kontext im TuiVision-System und c) bei Bedarf die wichtigste Abgrenzung zu anderen Modulen. Diese Pflicht gilt ausdruecklich auch fuer von KI-Agenten sowie in Spec-/SDD-Workflows erzeugte Quelldateien. Ausgenommen sind nur rein technische, vollautomatisch regenerierbare Artefakte. Triviale Kommentare ohne Zusatznutzen, insbesondere blosse Wiederholungen von Klassen-, Typ- oder Dateinamen, gelten nicht als Erfuellung dieses Kriteriums.
2. Methoden mit nicht-trivialer Logik (z. B. mehrere Verzweigungen, komplexe Zustandswechsel, nicht offensichtliche Algorithmen) erhalten erklaerende Kommentare.
3. Kommentare erklaeren das Warum (Entscheidung, Randbedingung, Trade-off), nicht nur das Was.
4. Didaktische Inline-Kommentare werden moderat eingesetzt: 1 bis 3 Zeilen fuer einen nicht-trivialen Block reichen im Regelfall. Mehrzeilige Kommentare sind komplexen Flows, historischen Abweichungen, Sicherheits-/A11Y-Randbedingungen oder Test-Proof-Pfaden vorbehalten.
5. Triviale Kommentare, die nur Namen, Operatoren, offensichtliche Zuweisungen oder den unmittelbar sichtbaren Code wiederholen, gelten nicht als Erfuellung des didaktischen Standards.
6. Zentrale Framework-Flows und Smoke-Test-Helfer, insbesondere Event-/Command-Dispatch, Fokuswechsel, StatusLine, Dialogzustand, Buffer-/Cell-Proofs, Rendering-/Fallbackpfade und historische Abweichungen, muessen bei Aenderungen auf didaktischen Kommentarbedarf geprueft werden.
7. Interne Invarianten, Vorbedingungen und Nachbedingungen werden bei komplexer Logik explizit dokumentiert.
8. Relevante Nebenwirkungen, Fehlerfaelle und Verhaltensgrenzen werden am Codepunkt beschrieben.
9. Historische Portierungsentscheidungen und bewusst abweichendes Verhalten zum Original werden am Codepunkt begruendet.
10. Kommentare werden bei Codeaenderungen im selben Arbeitsgang aktualisiert; veraltete Kommentare gelten als Qualitaetsmangel.

## 10.6 Standard fuer lokalen Multi-Mac-Workflow (verbindlich)
Fuer die Arbeitsumgebungen `MacBook Air M2` und `Mac mini M4 Pro` gilt:
1. Die Nutzung von `gh`, `codex`, `claude`, `copilot` und `gemini` fuer taegliche Entwicklungsschritte ist in `docs/guides` nachvollziehbar beschrieben. `codex`, `claude`, `copilot` und `gemini` sind auf beiden Systemen als Pflichtinstallation vorzuhalten. `glab`, `opencode` und JetBrains `junie` koennen optional ergaenzt werden und sind bei Nutzung ebenfalls nachvollziehbar zu dokumentieren.
2. Mindestens die Schluesselablaeufe Build, Test, Branch/PR-Workflow und Repository-Operationen sind dokumentiert.
3. Die Befehle sind so dokumentiert, dass sie auf beiden Systemen mit den angegebenen Voraussetzungen reproduzierbar funktionieren.
4. Voraussetzungen (authentifizierte CLI-Tools) und die Versionspruefung sind explizit dokumentiert.
5. GitHub Spec-Kit ist als Pflichtbestandteil in allen vier KI-Agenten (`codex`, `claude`, `copilot`, `gemini`) installiert; Installation, Versionspruefung und Grundnutzung sind fuer beide Systeme dokumentiert.
6. Fuer optional genutzte KI-Agenten `opencode` und JetBrains `junie` gilt ebenfalls: Wenn sie lokal eingerichtet sind, ist GitHub Spec-Kit auch dort zu installieren, mit Versionspruefung zu versehen und in der Workflow-Dokumentation nachweisbar zu nutzen.
7. Falls zusaetzliche Tools noetig sind (z. B. DocFX), ist die automatisierte Bereitstellung per dokumentiertem Befehl Bestandteil des Workflows.
8. Der Multi-Mac-Workflow ist der primaere Entwicklungs- und Testworkflow des Projekts. Zusaetzlich sind Windows und Linux als Kompatibilitaetsumgebungen zu beruecksichtigen; unter Windows bevorzugt WSL mit aktuellem Ubuntu (derzeit `Ubuntu 24.04`).
9. Soweit praktikabel, werden Linux- und Windows/WSL-Kompatibilitaetspruefungen auch in GitHub Actions oder aequivalenten automatisierten Nachweisen mitgefuehrt.

## 10.7 Standard fuer Nutzerdokumentation – Quellen, Struktur und Sprache (verbindlich)

Statuscheckliste Pflichtguides:
- [ ] `docs/guides/getting-started.md`
- [ ] `docs/guides/architecture.md`
- [ ] `docs/guides/concepts/event-loop.md`
- [ ] `docs/guides/concepts/view-hierarchy.md`
- [ ] `docs/guides/concepts/coordinate-system.md`
- [ ] `docs/guides/concepts/serialization.md`
- [ ] `docs/guides/tutorials/first-dialog.md`
- [ ] `docs/guides/examples/` mit einer Seite pro portiertem Beispielprogramm
- [-] Reihenfolgehinweis: zuerst die allgemeinen Guides (`getting-started`, `architecture`, `concepts/*`) aufbauen, danach Tutorials, danach die Beispiel-Guides parallel zu den Beispielportierungen pflegen.

### Pflichtstruktur unter `docs/guides/`

Die folgenden Guides sind verpflichtend bereitzustellen:

| Datei/Ordner | Mindestinhalt |
|---|---|
| `getting-started.md` | Installation, Build, erstes Beispielprogramm starten; Zielgruppe: Azubis ohne TV-Vorkenntnisse |
| `architecture.md` | Moduluebersicht, View-Hierarchie, Event-System, Koordinatensystem — konzeptuell mit Diagrammen oder Codeauszuegen |
| `concepts/event-loop.md` | Wie Ereignisse entstehen, weitergeleitet und verarbeitet werden (`HandleEvent`, `PutEvent`, Broadcast) |
| `concepts/view-hierarchy.md` | `TView`, `TGroup`, Fokus, Owner/Parent-Beziehungen, `Draw`-Zyklus |
| `concepts/coordinate-system.md` | Lokale vs. globale Koordinaten, `MakeLocal`/`MakeGlobal`, `TRect`-Semantik |
| `concepts/serialization.md` | Envelopenformat, `TRecordRegistry`, polymorphe De-/Serialisierung |
| `tutorials/first-dialog.md` | Vollstaendiger Schritt-fuer-Schritt-Guide: eigenen Dialog aufbauen und in eine App einbinden |
| `examples/` | Pro portiertem Beispielprogramm eine Seite gemaess M-18 und Abschnitt 10.4; fuer TP7-Anschlusswellen koennen parallele Unterordner oder klar benannte Zusatzseiten verwendet werden |

### Quellenrangfolge und Adaptionspflicht

Die Nutzerdokumentation speist sich aus folgenden Quellen in absteigender Prioritaet:

1. **Borland-Originaldokumentation (Tier 1 – Konzepte und Struktur)**
   - *Turbo Vision for C++ User's Guide* (Borland International, 1992; lokale Quelle: [TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.pdf](TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.pdf)) – Konzepte, Architektur, Tutorials, Event-Modell
   - *Turbo Vision for C++ Reference Guide* (Borland International, 1992) – vollstaendige Klassenreferenz mit Verhalten und Beispielen
   - Zusaetzlich verifiziertes oeffentlich zugaengliches Begleitdokument: *Turbo Vision Version 2.0 Programming Guide* (Borland, 1992; lokale Quelle: [TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.pdf](TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.pdf))
   - Das User's Guide und das zusaetzlich genannte Programming Guide liegen damit in diesem Repository als originale PDF-Quellen unter `TVDocs/`; fuer das separat benannte Reference Guide ist derzeit keine eigenstaendig verifizierte Einzeldatei hinterlegt.
   - **Adaptionspflicht**: Diese Werke dienen ausschliesslich als inhaltliche Vorlage und Inspirationsquelle. Jeder Text ist vollstaendig neu zu formulieren: C++ wird zu C#, DOS- und Borland-Terminologie wird durch TuiVision- und .NET-Terminologie ersetzt, das Sprachniveau wird auf CEFR B2 angepasst. Woertliche Textuebernahmen sind nicht zulaessig.

2. **Lokale Hilfsfassungen fuer Recherche und agentische Dokuarbeit (nicht normative Arbeitsmittel)**
   - OCR-Textfassungen und Markdown-Arbeitsfassungen: [TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.txt](TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.txt), [TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.md](TVDocs/Borland-Turbo-Vision-for-C-User-s-Guide.md), [TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.txt](TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.txt), [TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.md](TVDocs/Turbo_Vision_Version_2.0_Programming_Guide_1992.md)
   - Diese Fassungen dienen ausschliesslich der lokalen Suche, Navigation und agentischen Aufbereitung. Bei Konflikten oder OCR-Fehlern haben die PDF-Originalquellen aus Tier 1 Vorrang.

3. **tv203s C/C++-Quellcode (Tier 2 – Verhaltensreferenz)**
   - `tv203s/contrib/tvision/` als massgebliche Referenz fuer das Originalverhalten
   - Wird genutzt, um Verhalten zu erklaeren und bewusste Abweichungen der C#-Portierung zu begruenden

4. **C# Quellcode und XML-Kommentare (Tier 3 – API-Dokumentation)**
   - Primaerquelle fuer alle API-Referenzseiten; docfx erzeugt daraus automatisch die API-Doku
   - XML-Kommentare muessen vollstaendig und didaktisch gemaess Abschnitt 10.1 sein

5. **Portierte Beispielprogramme (Tier 4 – Nutzungsszenarien)**
   - `examples/` liefert konkrete Anwendungsszenarien fuer Tutorials und konzeptuelle Guides

### Sprache und Stil

- Alle Guides sind **bilingual** zu verfassen: Deutsch zuerst, Englisch als gleichwertiger zweiter Abschnitt oder parallel.
- Sprachniveau: **CEFR B2** – klare Satzstruktur, keine Umgangssprache, Fachbegriffe beim ersten Auftreten erklaert und mit Originalbegriff in Klammern angegeben.
- Zielgruppe: Auszubildende Fachinformatiker Anwendungsentwicklung mit grundlegenden C#-Kenntnissen, ohne Vorkenntnisse in TUI-Frameworks.
- Fuer grosse normative Dokumente wie `Pflichtenheft*.md` und `Lastenheft*.md` ist aus Uebersichtsgruenden eine synchron gepflegte englische Parallelfassung mit Suffix `.EN.md` zulaessig und empfohlen; die deutsche Fassung bleibt kanonisch, sofern nicht explizit anders markiert.
- Der didaktische Standard gemaess Abschnitt 10.3 gilt ergaenzend fuer alle Nutzerdokumentation.

## 10.8 Standard fuer GitHub-Pages-Deployment (verbindlich)

Statuscheckliste GitHub Pages:
- [ ] Repository-Einstellung `Settings -> Pages -> Source = GitHub Actions`
- [ ] Workflow-Datei `.github/workflows/docs-deploy.yml`
- [ ] Trigger auf `main` fuer `docs/**`, `src/**`, `docfx.json`
- [ ] Schritte `checkout`, `setup-dotnet`, `restore/build`, `docfx`, `upload-pages-artifact`, `deploy-pages`
- [ ] Berechtigungen `pages: write`, `id-token: write`, `contents: read`
- [ ] Erfolgreicher Deployment-Nachweis nach relevantem Merge auf `main`
- [ ] Veroeffentlichte GitHub-Pages-URL liefert die aktuelle docfx-Ausgabe aus

### Repository-Einstellung

Im GitHub-Repository `hindermath/TuiVision` muss unter *Settings → Pages → Source* die Option **GitHub Actions** aktiviert sein. Eine manuelle Branch-basierte Veroeffentlichung (z. B. `gh-pages`-Branch) ist nicht zulaessig, da sie keinen nachvollziehbaren Deployment-Status im CI-Workflow erzeugt.

### Workflow-Datei

Das Deployment wird in einer dedizierten Workflow-Datei `.github/workflows/docs-deploy.yml` konfiguriert, getrennt vom Build-/Test-Workflow. Die Trennung stellt sicher, dass ein fehlgeschlagenes Deployment den Build-/Test-Status nicht beeinflusst und umgekehrt.

### Trigger

Der Workflow wird ausgeloest durch einen `push` auf den Branch `main`, eingeschraenkt auf folgende Pfade:

```yaml
on:
  push:
    branches:
      - main
    paths:
      - 'docs/**'
      - 'src/**'
      - 'docfx.json'
```

Dadurch wird ein Deployment nur ausgeloest, wenn Dokumentationsinhalte, XML-Kommentare im Quellcode oder die docfx-Konfiguration geaendert wurden. Reine Code-Commits ohne Doku-Relevanz loesen kein Deployment aus.

### Erforderliche Actions und Berechtigungen

Der Workflow benoetigt folgende GitHub Actions in dieser Reihenfolge:

| Schritt | Action | Zweck |
|---|---|---|
| 1 | `actions/checkout` | Repository auschecken |
| 2 | `actions/setup-dotnet` | .NET 10 SDK bereitstellen (fuer `dotnet build` zur XML-Doc-Erzeugung) |
| 3 | `dotnet restore` + `dotnet build` | XML-Kommentardateien erzeugen, die docfx als API-Quelle benoetigt |
| 4 | `docfx docfx.json` | Statische Dokumentationsseiten generieren |
| 5 | `actions/upload-pages-artifact` | Erzeugtes Ausgabeverzeichnis als Pages-Artefakt hochladen |
| 6 | `actions/deploy-pages` | Artefakt auf GitHub Pages veroeffentlichen |

Der Job benoetigt folgende explizite Berechtigungen im Workflow:

```yaml
permissions:
  pages: write
  id-token: write
  contents: read
```

Der Deployment-Job muss der GitHub-Pages-Umgebung zugeordnet sein:

```yaml
environment:
  name: github-pages
  url: ${{ steps.deployment.outputs.page_url }}
```

### Abnahmekriterium fuer diesen Standard

- Der Workflow `.github/workflows/docs-deploy.yml` existiert und enthaelt Trigger, Berechtigungen, Umgebung und alle sechs Schritte gemaess diesem Abschnitt.
- Nach einem Merge auf `main` mit Aenderungen an den definierten Pfaden ist der Deployment-Job im Actions-Tab des Repositories als erfolgreich abgeschlossen sichtbar.
- Die veroeffentlichte GitHub-Pages-URL liefert die aktuelle docfx-Ausgabe aus.

## 11. Risiken und Randbedingungen

| Risiko | Bewertung | Gegenmassnahme |
|---|---|---|
| Lizenzlage der historischen Quellen (Borland/Inprise + spaetere GPL/BSD-Anteile) | mittel/hoch | Klarer Disclaimer (M-13), saubere Trennung eigener und uebernommener Inhalte, juristisch-technische Klaerung |
| API-/Verhaltensunterschiede zwischen Original und Port | mittel | Golden-Tests mit Beispielprogrammen, dokumentierte Abweichungen |
| Konsolidierung vieler historischer Treiber auf einen Managed-.NET-Treiber | mittel | Funktionale Priorisierung nach Kernfeatures, Regressionstests |
| Umfang der Beispielportierung | mittel | Inkrementelle Wellen, pro Beispiel Smoke-Test |
| Vermeidung nativer Abhaengigkeiten bei zugleich hoher Funktionsnahe | mittel | Architekturregeln (M-12) und Build-Checks ohne Native-Payload |

## 11.1 Priorisierte Restarbeiten bis zur Abnahme

Die verbleibenden Arbeiten sind in folgender Reihenfolge abzuarbeiten, damit die MUSS-Abnahme mit moeglichst geringem Ruecksprungrisiko erreicht wird:

Dieser Marker ist bei jeder wesentlichen Fortschreibung des Pflichtenhefts auf den dann hoechstprioren offenen Arbeitsschritt zu verschieben.

1. **Phase 7 Treiberkonsolidierung abschliessen** — ✓ ABGESCHLOSSEN (Branch `005-driver-consolidation-m07`)
   - Managed-Console-Treiber stabilisiert; `DriverCapabilityMap` mit 5 Faehigkeitsgruppen dokumentiert.
   - 151 historische `.cc`-Dateien in `docs/porting-status.md` abgebildet; alle mit Primaerziel, Statuswert, Nachweis und Begruendung.
   - 30 MSTests in `tests/TuiVision.Drivers.Tests/` — alle bestanden.

2. **M-07 vollstaendig schliessen und das Eingangstor fuer Phase 8 (Beginn der Beispielportierungen) nachweisbar schliessen** — ✓ ABGESCHLOSSEN (Branch `006-close-phase8-gate`)
   - `docs/porting-status.md` schliesst alle 151 historischen `.cc`-Zeilen final als `portiert + getestet` oder `bewusst ausgelassen + Begruendung`.
   - `dotnet build --configuration Release`, `dotnet test`, `dotnet format --verify-no-changes` und `docfx docfx.json` sind fuer das Closure-Paket vom `2026-03-27` erfolgreich.
   - Die harte 5x-70-%-Coverage-Huerde ist erfuellt: `TuiVision.Core` `89,11 %`, `TuiVision.Controls` `84,10 %`, `TuiVision.Serialization` `83,33 %`, `TuiVision.Compatibility` `80,95 %`, `TuiVision.Drivers.Console` `97,43 %`.
   - Closure-Commit-Referenz: `docs: close phase-8 entrance gate for feature 006`.

3. **MUSS-Beispielwellen 1 bis 4 portieren**
   - Das Eingangstor aus Abschnitt 8.2 ist bestanden; Welle 1 ist abgeschlossen (Branch `007-port-wave1-examples`, 2026-03-28).
   - Welle-1-Lieferumfang: `desklogo`, `msgcls`, `tutorial` (16 Schritte), `videomode`; 41 Smoke-Tests gruen; Guides unter `docs/guides/examples/` geliefert; Release-Build sauber, `dotnet format --verify-no-changes` bestanden.
   - Die 25 Originalbeispiele aus `tv203s/contrib/tvision/examples` bleiben bis zur Abnahme der einzige verpflichtende Beispielumfang.
   - Die vier Wellen sind als vier eigenstaendige Unterphasen `3.1` bis `3.4` zu behandeln; sie werden nacheinander abgearbeitet und jeweils separat geplant, portiert, getestet und dokumentiert.
   - `3.1` = Welle 1: Grundlegende Anwendungsstruktur — ✓ ABGESCHLOSSEN
   - `3.1a` = Welle 1: Funktionaler Nachweisnachlauf — ✓ ABGESCHLOSSEN (Branch `014-wave1-functional-hardening`)
   - `3.1b` = Didaktische Inline-Code-Kommentar-Haertung — ✓ ABGESCHLOSSEN (Branch `015-didactic-comment-hardening`)
   - `3.1c` = Secure-Development-Hardening — ✓ ABGESCHLOSSEN (Branch `016-secure-development-hardening`)
   - `3.1d` = Welle 1: Sichtbarer Komponenten-Nachweis — ✓ ABGESCHLOSSEN (Branch `017-wave1-visual-component-remediation`)
   - `3.2` = Welle 2: Controls und Dialoge — ✓ ABGESCHLOSSEN
   - `3.2a` = Welle 2: Interaktive Showcase-Stufe — ✓ ABGESCHLOSSEN (Branch `012-interactive-wave2-demos`)
   - `3.2b` = Welle 2: Sichtbarer Komponenten-Nachweis — ✓ ABGESCHLOSSEN (Branch `013-wave2-visual-component-remediation`)
   - `3.3a` = Welle 3: Editor-/Hilfe-/Ressourcen-Vorhaertung — ✓ ABGESCHLOSSEN (Branch `018-editor-help-resources-hardening`)
   - `3.3` = Welle 3: Editor, Dateien, Hilfe und Streams — ✓ ABGESCHLOSSEN (Branch `019-wave3-visual-component-porting`)
   - `3.3b` = Maussupport und Interaktions-Härtung — ✓ ABGESCHLOSSEN (Branch `020-mouse-support-interaction`)
   - `3.3c` = Terminal-/Charset-/Plattform-Vorhärtung — ✓ ABGESCHLOSSEN (Branch `021-terminal-charset-hardening`)
   - `3.4` = Welle 4: Terminal-Emulation und erweiterte Zeichensaetze — ✓ ABGESCHLOSSEN (Branch `022-wave4-visual-component-porting`)

>>> NAECHSTER SCHRITT <<< A11Y-Framework über `Lastenheft_06_A11Y_Framework.md` umsetzen. Wave 5 bleibt bis zu diesem Abschluss zurückgestellt.

4. **MUSS-Testumfang und Beispiel-Smoke-Tests schliessen**
   - Fuer alle 25 portierten Originalbeispiele automatisierte Smoke-Tests in CI bereitstellen.
   - Die MUSS-Tests gemaess Abschnitt 9.4 fuer Pull Requests und `main` vollstaendig nachweisbar machen.

5. **Pflichtdokumentation parallel zur Portierung fertigstellen**
   - Pflichtguides unter `docs/guides/`, Beispiel-Guides gemaess Abschnitt 10.4, XML-Dokumentation, Changelog und didaktische Vereinheitlichung nicht auf den Projektabschluss verschieben.
   - Dokumentation ist im selben Arbeitsgang wie API-, Verhaltens- und Beispielaenderungen nachzuziehen.

6. **Multi-Mac-Workflow und GitHub Pages abschliessen**
   - Die dokumentierten Schluesselablaeufe fuer `gh`, `codex`, `claude`, `copilot`, `gemini` und GitHub Spec-Kit auf beiden Macs nachvollziehbar absichern; optionale Werkzeuge `glab`, `opencode` und `junie` bei Nutzung einschliessen, bei den beiden optionalen KI-Agenten jeweils ebenfalls mit Spec-Kit.
   - `M-22` mit automatischem GitHub-Pages-Deployment der docfx-Dokumentation auf `main` schliessen.

7. **TP7-Anschlusswellen erst nach MUSS-Abnahme verfolgen**
   - `TVDEMOS/` und `TVFM/` bleiben wertvolle Anschlussarbeit, duerfen aber weder `M-10` noch die Abschlusskriterien fuer die 25 Originalbeispiele verwischen.

## 12. Abnahmekriterien
Die Abnahme gilt als bestanden, wenn:
- [ ] Alle MUSS-Anforderungen M-01 bis M-22 nachweisbar erfuellt sind.
- [-] Das Framework in C#/.NET 10 (net10.0) buildbar ist und die definierten Tests durchlaufen.
- [x] Die API-Dokumentation mit docfx erzeugt wird.
- [ ] Alle 25 identifizierten Original-Beispielprogramme in portierter Form vorliegen und mindestens per Smoke-Test validiert sind; TP7-Anschlussbeispiele werden zusaetzlich und getrennt nachverfolgt.
- [ ] Interaktiv gedachte Beispielprogramme zeigen beim normalen Start ueber `dotnet run --project examples/<Name>` einen sichtbaren Bedienpfad mit Rueckmeldung; reine Headless-Seams oder direkte Testmethoden reichen fuer die finale Lern- und Review-Reife nicht aus.
- [x] Das Projekt im GitHub-Repository `https://github.com/hindermath/TuiVision.git` nachvollziehbar versioniert ist.
- [x] Der verbindliche Lizenz-/Disclaimer-Hinweis gemaess Abschnitt 10.2 sichtbar enthalten ist.
- [ ] Die Gesamtdokumentation den didaktischen Standard gemaess Abschnitt 10.3 nachweisbar einhaelt.
- [ ] Alle lernrelevanten Dokumente, Guides sowie die aktiven `Pflichtenheft`-/`Lastenheft`-Artefakte liegen fuer Auszubildende nachvollziehbar in Deutsch und Englisch auf CEFR-B2-Niveau vor; grosse normative Dokumente duerfen dafuer als synchron gepflegte `.EN.md`-Parallelfassung statt als inline zweisprachiger Block bereitgestellt werden.
- [ ] Fuer alle 25 portierten Original-Beispielprogramme liegt eine Dokumentation gemaess Abschnitt 10.4 vor; fuer TP7-Anschlussbeispiele gilt derselbe Dokumentationsstandard, sobald sie portiert werden.
- [-] Der Quellcode erfuellt den Dokumentationsstandard gemaess Abschnitt 10.5 nachweisbar.
- [ ] Der Mindest-Testumfang gemaess Abschnitt 9.4 ist nachweisbar vollstaendig erfuellt.
- [-] Der lokale Workflow mit `gh`, `codex`, `claude`, `copilot` und `gemini` gemaess Abschnitt 10.6 ist auf beiden macOS-Systemen nachweisbar anwendbar; GitHub Spec-Kit ist in allen vier KI-Agenten installiert und in den dokumentierten Ablaeufen nutzbar. Optionale Werkzeuge `glab`, `opencode` und `junie` sind bei Nutzung ebenfalls erfasst; fuer `opencode` und `junie` mit installierter Spec-Kit-Unterstuetzung.
- [x] Bei API-/XML-Kommentar-Aenderungen ist die docfx-Dokumentation nachweisbar neu erzeugt worden.
- [ ] Fuer erzeugte HTML-Dokumentation und textorientierte Review-Pfade liegt ein nachvollziehbarer A11Y-Nachweis gemaess `Programmierung #include<everyone>` vor: WCAG 2.2 AA als Baseline, textorientierter Review nach DocFX-Neubau sowie Nutzbarkeit fuer Braille-Zeile, Screenreader und Textbrowser.
- [ ] Die docfx-Dokumentation ist ueber GitHub Pages des Repositories `hindermath/TuiVision` erreichbar und wird bei jedem Merge auf `main` mit Doku-relevanten Aenderungen automatisch aktualisiert (M-22).

## 13. Abgrenzung
Nicht Bestandteil der MUSS-Abnahme:
- Vollstaendige bitgenaue Replikation jeder historischen Plattformbesonderheit
- NuGet-Veroeffentlichung (optional)
- Native OS-Bindings oder betriebssystemspezifische Zusatzruntimes
