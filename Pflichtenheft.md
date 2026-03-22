# Pflichtenheft: TuiVision (Portierung Turbo Vision 2.0.3 nach C#/.NET 10, .NET Core)

## 1. Zweck und Geltungsbereich
Dieses Pflichtenheft beschreibt die technische Umsetzung der im Lastenheft definierten Anforderungen fuer das Projekt **TuiVision**.
Grundlage ist der Quellcode im Ordner `tv203s` (Turbo Vision 2.0.3, C/C++).

Ziel ist eine wartbare, testbare und dokumentierte Portierung nach **C#/.NET 10 (net10.0, .NET Core)** inklusive Beispielanwendungen.
TuiVision ist dabei ausdruecklich als **Beispielprojekt** zur Modernisierung mit **Agentic-AI/Agentic-UI** konzipiert.

## 2. Ausgangsbasis (Ist-Analyse)
- Lastenheft: `Lasten_Heft.md`
- Quellcodebasis: `tv203s/contrib/tvision`
- Lokale Arbeitsumgebungen: macOS auf `MacBook Air M2` und `Mac mini M4 Pro`
- Auf beiden Systemen vorhanden und authentifiziert: `gh` (GitHub CLI) und `codex` (Codex CLI)
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
1. C#-Portierung des Frameworks als Loesung mit klarer Projektstruktur
2. Unit-Tests mit MSTest fuer portierte Klassen/Methoden
3. API-Dokumentation mit docfx
4. Portierung der vorhandenen Beispielprogramme aus `tv203s/contrib/tvision/examples`
5. Build- und Qualitaetssicherungsprozesse
6. CI/CD-Workflow mit automatischem Build und Test auf GitHub Actions
7. Nutzerdokumentation (Leitfaeden fuer Einstieg und Verwendung)
8. Ausfuehrliche Dokumentation aller portierten Beispielprogramme
9. Ausreichende Quellcode-Dokumentation fuer Lern- und Wartungszwecke
10. Dokumentierter lokaler Workflow fuer `gh` und `codex` auf beiden macOS-Systemen

## 5. Anforderungen (MUSS)

| ID | Anforderung | Technische Umsetzung | Abnahmekriterium |
|---|---|---|---|
| M-01 | Lokales Git-Repository erstellen | `git init`, sinnvolle Branch-/Commit-Strategie | Repository ist initialisiert, Historie nachvollziehbar |
| M-02 | `.gitignore` fuer JetBrains, C#/.NET, VS Code, Visual Studio | Kombination etablierter Ignore-Regeln | Keine IDE-/Build-Artefakte im Repo |
| M-03 | Remote auf GitHub unter `https://github.com/hindermath/TuiVision.git` | `origin` konfigurieren, Push faehig | Remote ist gesetzt, Push/Fetch funktionieren |
| M-04 | Verwendung von C#/.NET 10 (.NET Core) | SDK-Target `net10.0` in allen Projekten | `dotnet build` laeuft ohne Target-Konflikte |
| M-05 | Projektstruktur nach .NET-Best-Practices | Trennung in `src`, `tests`, `examples`, `docs` | Struktur ist konsistent, Build reproduzierbar |
| M-06 | Portierung des Framework-Kerns aus `tv203s` | API- und Verhaltensport in C# | Definierte Kernmodule sind funktional und testbar |
| M-07 | Portierung der Implementierungsdateien aus `tv203s/contrib/tvision/classes` | Alle `.cc`-Dateien aus dem Ordner `tv203s/contrib/tvision/classes` (z. B. `tview.cc`, `tgroup.cc`, `tapplica.cc`, `teditor.cc` u. v. m.) dienen als direkte C/C++-Vorlage fuer M-06; jede Datei wird gemaess Modulmapping (Abschnitt 7.2) einem Zielmodul (`TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization` oder `TuiVision.Drivers.Console`) zugeordnet und portiert | Alle identifizierten `.cc`-Quelldateien aus `tv203s/contrib/tvision/classes` (einschliesslich der plattformspezifischen Unterordner fuer `TuiVision.Drivers.Console`) sind in den entsprechenden C#-Zielmodulen nachweisbar abgebildet und durch Unit-Tests abgesichert |
| M-08 | Unit-Tests fuer portierte Klassen/Methoden mit MSTest | Testprojekte pro Modul, CI-faehig | Tests laufen lokal und in CI stabil durch |
| M-09 | API-Dokumentation mit docfx | docfx-Konfiguration, API-Generierung und erneute Doku-Erstellung bei API-/XML-Kommentar-Aenderungen | Doku erzeugbar, verlinkt alle Kern-Namespaces und ist nach API-/XML-Aenderungen aktualisiert |
| M-10 | Portierung der vorhandenen Beispiele | Alle 25 Beispiele als .NET-Beispiele abbilden | Beispiele bauen; definierte Smoke-Tests bestehen |
| M-11 | Qualitaetssicherung zusaetzlich zu Unit-Tests | Analyzer, Format- und Build-Gates | Qualitaets-Gates sind dokumentiert und aktiv |
| M-12 | Keine nativen OS-Abhaengigkeiten | Keine P/Invoke-/Native-Library-Pflicht, keine OS-spezifischen Zusatzpakete | Build/Tests laufen mit .NET 10 Runtime ohne native Zusatzinstallation |
| M-13 | Lizenz-Disclaimer fuer Beispielcharakter | Sichtbarer Hinweis in `LICENSE`/`README` | Hinweis beschreibt: Beispielprojekt, keine Konkurrenzabsicht, keine beabsichtigte Lizenzverletzung |
| M-14 | CI/CD mit GitHub Actions | Build-/Test-Workflow und Dokumentations-Deployment-Workflow unter `.github/workflows`; GitHub-Pages-Deployment gemaess M-22 ist Bestandteil der CI/CD-Pipeline | Automatischer Build und Testlauf pro Push/PR ist aktiv; GitHub-Pages-Deployment laeuft automatisch bei Doku-Aenderungen auf `main` (M-22) |
| M-15 | Nutzerdokumentation | Pflichtguides unter `docs/guides/` gemaess Abschnitt 10.7; Quellenrangfolge und Adaptionspflicht gemaess Abschnitt 10.7 eingehalten; Sprache bilingual (Deutsch zuerst, Englisch) auf CEFR-B2-Niveau | Alle Pflichtguides unter `docs/guides/` sind vorhanden; Quellenrangfolge nachvollziehbar eingehalten; Sprache und Struktur entsprechen dem didaktischen Standard gemaess Abschnitt 10.3 und 10.7 |
| M-16 | Vollstaendige XML-Kommentierung der oeffentlichen API | Alle `public` Typen, Member, Parameter, Rueckgabewerte und Ausnahmen mit XML-Dokumentation | API ist durchgaengig und didaktisch ausfuehrlich kommentiert; docfx erzeugt daraus vollstaendige Referenzseiten |
| M-17 | Einheitlicher didaktischer Dokumentationsstil | Alle Dokumentationsartefakte folgen einem verbindlichen Lehr-/Beispielstandard fuer Fachinformatiker (Anwendungsentwicklung) | Struktur, Detailtiefe und Beispiele sind ueber alle Dokuarten konsistent und nachvollziehbar |
| M-18 | Ausfuehrliche Dokumentation der Beispielprogramme | Pro Beispielprogramm eigener Guide mit Lernziel, Voraussetzungen, Start, Bedienung, Architekturhinweisen und Uebungen | Alle portierten Beispiele sind didaktisch nachvollziehbar dokumentiert und reproduzierbar ausfuehrbar |
| M-19 | Ausreichende Quellcode-Dokumentation im gesamten TuiVision-Code | Nicht-triviale Logik, Architekturentscheidungen und interne Zusammenhaenge werden im Code nachvollziehbar kommentiert | Der Quellcode erfuellt die pruefbaren Kriterien aus Abschnitt 10.5 und ist fuer Fachinformatiker (Anwendungsentwicklung) lern- und wartbar |
| M-20 | Messbarer Mindest-Testumfang (MUSS-Tests) | Definierte Mindestabdeckung, Pflichttestfaelle und vollstaendige Smoke-Tests gemaess Abschnitt 9.4 | Die in Abschnitt 9.4 definierten Kennzahlen und Testumfaenge sind vollstaendig erreicht |
| M-21 | Reproduzierbarer Multi-Mac-Entwicklungsworkflow | Build-, Test-, GitHub- und Codex-Arbeitsablaeufe sind fuer `MacBook Air M2` und `Mac mini M4 Pro` mit `gh` und `codex` dokumentiert | Die dokumentierten Schluesselablaeufe funktionieren auf beiden Systemen mit den dokumentierten Voraussetzungen und ggf. automatisierter Tool-Bereitstellung (z. B. DocFX als .NET-Tool) |
| M-22 | Automatische Veroeffentlichung der Dokumentation auf GitHub Pages | Dedizierter Workflow `.github/workflows/docs-deploy.yml` gemaess Abschnitt 10.8: Trigger auf `push` nach `main` fuer Pfade `docs/**`, `src/**`, `docfx.json`; Schritte: checkout, dotnet-build (XML-Docs), docfx, `upload-pages-artifact`, `deploy-pages`; Repository-Setting auf Source „GitHub Actions"; Umgebung `github-pages` | Workflow existiert gemaess Abschnitt 10.8; docfx-Dokumentation ist ueber GitHub Pages erreichbar; Deployment laeuft automatisch nach jedem relevanten Merge auf `main`; Deployment-Status im Actions-Tab sichtbar |

## 6. Optionale Anforderungen (KANN / Pruefauftraege)

| ID | Option | Umsetzung | Ergebnisartefakt |
|---|---|---|---|
| O-01 | NuGet-Paketierung | Pack-/Versionierungsprozess | erzeugbares `.nupkg` |
| O-02 | Free Vision als Vergleichsquelle nutzen | API-/Konzeptvergleich | Mapping-/Abweichungsdokument |

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
1. **Basisinfrastruktur**: Loesung, Build, Tests, Doku-Pipeline
2. **Kernobjekte**: `TObject`, Collections, Geometrie (`TPoint`, `TRect`), Events
3. **View-System**: `TView`, `TGroup`, Zeichenpuffer, Fokus/States
4. **Anwendungsrahmen**: `TProgram`, `TApplication`, Menues, Statuszeile, Desktop
5. **Dialog-/Control-Schicht**: Eingabezeilen, Listen, Scrollbars, Buttons, usw.
6. **Editor/Datei/Hilfe/Streams**: Editor, Resource-, Stream- und Help-Komponenten
7. **Treiberkonsolidierung**: Managed Console-Treiber unter .NET Core ohne native OS-Bindings
8. **Beispiele**: Portierung aller 25 Beispiele in vier thematisch-technischen Wellen; jede Welle wird erst begonnen, wenn die zugehoerigen Framework-Phasen abgeschlossen sind (Abhaengigkeitsprinzip); **vor Welle 1 ist das Eingangstor gemaess Abschnitt 8.3 vollstaendig zu bestehen**

### 8.3 Eingangstor Phase 8: Framework-Vollstaendigkeitsnachweis (verpflichtend vor Welle 1)

Bevor das erste Beispielprogramm portiert wird (Welle 1, Abschnitt 8.2), muessen alle nachfolgenden Kriterien nachweisbar erfuellt sein. Jedes offene Kriterium blockiert den Start von Phase 8 und ist als Issue im Repository zu erfassen und zu schliessen.

#### Kriterium 1 – M-07-Vollstaendigkeitsnachweis (Mapping-Tabelle)

Die Datei `docs/porting-status.md` muss existieren und fuer jede `.cc`-Quelldatei aus `tv203s/contrib/tvision/classes` (einschliesslich plattformspezifischer Unterordner) einen Eintrag enthalten mit:
- Quelldatei (relativer Pfad in `tv203s/`)
- Zugeordnetes C#-Zielmodul (`TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization` oder `TuiVision.Drivers.Console`)
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

Line Coverage in `TuiVision.Core`, `TuiVision.Controls` und `TuiVision.Serialization` betraegt jeweils mindestens 70 % (gemaess §9.4 Nr. 1), gemessen mit Coverlet. Das Coverage-Ergebnis ist als CI-Artefakt oder lokaler Report nachweisbar.

#### Kriterium 6 – API-Doku-Gate

`docfx` laeuft ohne Fehler durch; keine oeffentliche API ist undokumentiert (M-09, M-16). Das erzeugte Ausgabeverzeichnis ist im Repository oder als CI-Artefakt vorhanden.

#### Nachweisdokument

Der Abschluss des Eingangstors wird durch einen dedizierten Commit auf dem jeweiligen Feature-Branch dokumentiert, dessen Commit-Nachricht alle sechs Kriterien als erfuellt ausweist und auf `docs/porting-status.md` verweist.

### 8.2 Beispielprogramme (MUSS-Umfang)
Zu portieren sind alle 25 vorhandenen Beispielordner, eingeteilt in vier Wellen nach technischer Abhaengigkeit.
Fuer jedes portierte Beispiel ist eine eigene didaktische Dokumentationsseite in `docs/guides/examples/` bereitzustellen.

**Welle 1 – Grundlegende Anwendungsstruktur** (nach Abschluss Phase 4: Anwendungsrahmen)

Benoetigt: `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, `TStatusLine`.
Keine Controls, keine Dialoge, kein Editor, kein Hilfesystem.

| Beispiel | Inhalt |
|---|---|
| `desklogo` | Minimale App: statisches Logo auf dem Desktop |
| `msgcls` | Benutzerdefinierte Ereignisklassen und Nachrichtenverarbeitung |
| `tutorial` | Schrittweise Einfuehrung in die TuiVision-Grundkonzepte |
| `videomode` | Wechsel von Anzeigemodi (Pufferbreite/-hoehe) |

**Welle 2 – Controls und Dialoge** (nach Abschluss Phase 5: Dialog-/Control-Schicht)

Benoetigt: Eingabezeilen, Listen, Scrollbars, Buttons, Checkboxes, RadioButtons, Dialoge.

| Beispiel | Inhalt |
|---|---|
| `clipboard` | Zwischenablage-Integration in Controls |
| `demo` | Vollstaendige Turbo-Vision-Kerndemo (zeigt alle Basis-Controls) |
| `dlgdsn` | Dialog-Designer: dynamisch zusammengesetzte Dialoge |
| `dyntxt` | Dynamisch erzeugter Text in Views |
| `inplis` | Eingabelisten mit `TInputLine` |
| `listvi` | Listenansichten mit `TListViewer` |
| `progba` | Einfacher Fortschrittsbalken |
| `sdlg` | Standarddialoge (Datei-, Farb-, Zeichensatz-Auswahl) |
| `sdlg2` | Erweiterte Standarddialoge |
| `tcombo` | Kombinationsfelder (`TComboBox`) |
| `tprogb` | Erweiterter Fortschrittsbalken mit Abbruch |

**Welle 3 – Editor, Dateien, Hilfe und Streams** (nach Abschluss Phase 6: Editor/Datei/Hilfe/Streams)

Benoetigt: `TEditor`, `TFileDialog`, `THelpViewer`, Stream- und Ressourcen-Infrastruktur.

| Beispiel | Inhalt |
|---|---|
| `bhelp` | Grundlegendes Hilfesystem mit kontextsensitiven Themen |
| `helpdemo` | Interaktive Demonstration des Hilfesystems |
| `i18n` | Internationalisierung: mehrsprachige Texte und Ressourcen |
| `tvedit` | Vollstaendiger Texteditor (Datei oeffnen, bearbeiten, speichern) |
| `tvhc` | Hilfe-Compiler: Konvertierung von Quelltext in binaere Hilfedatei |

**Welle 4 – Terminal-Emulation und erweiterte Zeichensaetze** (nach Abschluss Phase 7: Treiberkonsolidierung)

Benoetigt: Managed Console-Treiber, plattformunabhaengige Zeichensatz- und Terminalpufferverwaltung.

| Beispiel | Inhalt |
|---|---|
| `cyrillic` | Kyrillische Zeichensatz-Unterstuetzung im Textpuffer |
| `eterm` | Erweiterter Terminal-Emulator |
| `fonts` | Zeichensatz-Verwaltung und Darstellung alternativer Fonts |
| `terminal` | Einfache Terminal-Integration |
| `xterm` | XTerm-Protokoll-Emulation |

## 9. Test- und Qualitaetskonzept

### 9.1 Testarten
1. **Unit-Tests (MSTest)** fuer alle portierten Klassen/Methoden
2. **Integrations-/Verhaltenstests** fuer Event-Loop, Fokus, Menue-/Dialogfluss
3. **Snapshot-/Golden-Tests** fuer Render-/Zeichenpuffer-Verhalten
4. **Smoke-Tests** fuer alle portierten Beispiele
5. **Runtime-Kompatibilitaetstests** fuer .NET 10 (net10.0) ohne native Zusatzkomponenten
6. **MUSS-Tests**: Sammelbegriff fuer alle in Abschnitt 9.4 als verpflichtend markierten Tests und Kennzahlen
7. **Umgebungs-/Workflow-Checks** fuer lokale Arbeit auf `MacBook Air M2` und `Mac mini M4 Pro`

### 9.2 Mindest-Qualitaetsgates
- Build ohne Warnungsfehler fuer freigegebene Konfiguration
- Alle MUSS-Tests erfolgreich
- API-Doku erfolgreich generierbar
- Bei Aenderungen an oeffentlicher API oder XML-Kommentaren wird die docfx-Dokumentation im selben Arbeitsgang neu erzeugt
- Einheitliche Formatierung/Analyzer-Regeln aktiv
- Fehlende XML-Kommentare fuer oeffentliche API werden als Qualitaetsverstoss behandelt (z. B. CS1591 nicht ignoriert)
- Dokumentations-Review gegen den didaktischen Standard gemaess Abschnitt 10.3 ist fuer Releases verpflichtend
- Beispielprogramme gelten erst als abgeschlossen, wenn die zugehoerigen Guides gemaess Abschnitt 10.4 vorliegen
- Quellcode-Review gegen den Standard gemaess Abschnitt 10.5 ist fuer Releases verpflichtend
- GitHub-Pages-Deployment der docfx-Dokumentation ist nach jedem Merge auf `main` mit Aenderungen an `docs/`, `src/` oder `docfx.json` automatisch erfolgreich abgeschlossen (M-22)

### 9.3 Rueckverfolgbarkeit
Jede MUSS-Anforderung (M-xx) wird mindestens einem Nachweisartefakt zugeordnet.
Zulaessige Nachweisartefakte sind Testfall, Smoke-Test, Build-/CI-Nachweis, Dokumentationsartefakt oder Checklisten-/Review-Nachweis.

### 9.4 Verbindlicher Mindest-Testumfang (MUSS-Tests)
Die folgenden Punkte sind verpflichtend und bilden die "MUSS-Tests" im Sinne dieses Pflichtenhefts:
1. Unit-Test-Abdeckung (Line Coverage) von mindestens 70% in `TuiVision.Core`, `TuiVision.Controls` und `TuiVision.Serialization`.
2. Fuer jede portierte Kernkomponente existieren mindestens ein Positivtest und ein Negativ-/Fehlerfalltest (sofern fachlich sinnvoll).
3. Integrations-/Verhaltenstests decken mindestens Event-Loop, Fokuswechsel, Menueausfuehrung und Dialoginteraktion ab.
4. Smoke-Tests laufen fuer alle 25 portierten Beispielprogramme automatisiert in CI.
5. Alle MUSS-Tests laufen in GitHub Actions auf jedem Pull Request und auf dem Hauptbranch.
6. Die dokumentierten lokalen Schluesselablaeufe mit `gh` und `codex` sind auf `MacBook Air M2` und `Mac mini M4 Pro` nachvollziehbar und reproduzierbar.
7. Bei API-/XML-Kommentar-Aenderungen ist die erfolgreiche docfx-Erzeugung als Nachweisartefakt vorhanden.

## 10. Dokumentation
- API-Dokumentation mit docfx aus dem Code
- Architektur- und Migrationsdokumentation in `docs/guides`
- Changelog fuer Portierungsfortschritt und Abweichungen vom Originalverhalten
- Saemtliche Projektdokumentation folgt einem einheitlichen didaktischen Lehr-/Beispielstandard
- Der Quellcode selbst ist didaktisch und wartungsorientiert dokumentiert
- Lokale Arbeitsablaeufe mit `gh` und `codex` sind fuer beide macOS-Systeme dokumentiert

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
Fuer jedes portierte Beispielprogramm ist eine eigene Dokumentationsseite mit mindestens folgenden Inhalten verpflichtend:
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
1. Jede nicht-generierte Quelldatei in `src/` enthaelt einen kurzen Modul-/Dateikommentar zu Verantwortung und Kontext.
2. Methoden mit nicht-trivialer Logik (z. B. mehrere Verzweigungen, komplexe Zustandswechsel, nicht offensichtliche Algorithmen) erhalten erklaerende Kommentare.
3. Kommentare erklaeren das Warum (Entscheidung, Randbedingung, Trade-off), nicht nur das Was.
4. Interne Invarianten, Vorbedingungen und Nachbedingungen werden bei komplexer Logik explizit dokumentiert.
5. Relevante Nebenwirkungen, Fehlerfaelle und Verhaltensgrenzen werden am Codepunkt beschrieben.
6. Historische Portierungsentscheidungen und bewusst abweichendes Verhalten zum Original werden am Codepunkt begruendet.
7. Kommentare werden bei Codeaenderungen im selben Arbeitsgang aktualisiert; veraltete Kommentare gelten als Qualitaetsmangel.

## 10.6 Standard fuer lokalen Multi-Mac-Workflow (verbindlich)
Fuer die Arbeitsumgebungen `MacBook Air M2` und `Mac mini M4 Pro` gilt:
1. Die Nutzung von `gh` und `codex` fuer taegliche Entwicklungsschritte ist in `docs/guides` nachvollziehbar beschrieben.
2. Mindestens die Schluesselablaeufe Build, Test, Branch/PR-Workflow und Repository-Operationen sind dokumentiert.
3. Die Befehle sind so dokumentiert, dass sie auf beiden Systemen mit den angegebenen Voraussetzungen reproduzierbar funktionieren.
4. Voraussetzungen (authentifizierte CLI-Tools) und die Versionspruefung sind explizit dokumentiert.
5. Falls zusaetzliche Tools noetig sind (z. B. DocFX), ist die automatisierte Bereitstellung per dokumentiertem Befehl Bestandteil des Workflows.

## 10.7 Standard fuer Nutzerdokumentation – Quellen, Struktur und Sprache (verbindlich)

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
| `examples/` | Pro portiertem Beispielprogramm eine Seite gemaess M-18 und Abschnitt 10.4 |

### Quellenrangfolge und Adaptionspflicht

Die Nutzerdokumentation speist sich aus folgenden Quellen in absteigender Prioritaet:

1. **Borland-Originaldokumentation (Tier 1 – Konzepte und Struktur)**
   - *Turbo Vision for C++ User's Guide* (Borland International, 1992) – Konzepte, Architektur, Tutorials, Event-Modell
   - *Turbo Vision for C++ Reference Guide* (Borland International, 1992) – vollstaendige Klassenreferenz mit Verhalten und Beispielen
   - Beide Werke sind auf archive.org oeffentlich zugaenglich
   - **Adaptionspflicht**: Diese Werke dienen ausschliesslich als inhaltliche Vorlage und Inspirationsquelle. Jeder Text ist vollstaendig neu zu formulieren: C++ wird zu C#, DOS- und Borland-Terminologie wird durch TuiVision- und .NET-Terminologie ersetzt, das Sprachniveau wird auf CEFR B2 angepasst. Woertliche Textuebernahmen sind nicht zulaessig.

2. **Free Vision Reference (Tier 1 – ergaenzende Konzepte)**
   - Free Pascal Projekt (freepascal.org) – konzeptionell verwandte Implementierung der gleichen TV-API
   - Gleiche Adaptionspflicht wie fuer Borland-Material

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
- Der didaktische Standard gemaess Abschnitt 10.3 gilt ergaenzend fuer alle Nutzerdokumentation.

## 10.8 Standard fuer GitHub-Pages-Deployment (verbindlich)

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

## 12. Abnahmekriterien
Die Abnahme gilt als bestanden, wenn:
1. Alle MUSS-Anforderungen M-01 bis M-21 nachweisbar erfuellt sind.
2. Das Framework in C#/.NET 10 (net10.0) buildbar ist und die definierten Tests durchlaufen.
3. Die API-Dokumentation mit docfx erzeugt wird.
4. Alle 25 identifizierten Beispielprogramme in portierter Form vorliegen und mindestens per Smoke-Test validiert sind.
5. Das Projekt im GitHub-Repository `https://github.com/hindermath/TuiVision.git` nachvollziehbar versioniert ist.
6. Der verbindliche Lizenz-/Disclaimer-Hinweis gemaess Abschnitt 10.2 sichtbar enthalten ist.
7. Die Gesamtdokumentation den didaktischen Standard gemaess Abschnitt 10.3 nachweisbar einhaelt.
8. Fuer alle 25 portierten Beispielprogramme liegt eine Dokumentation gemaess Abschnitt 10.4 vor.
9. Der Quellcode erfuellt den Dokumentationsstandard gemaess Abschnitt 10.5 nachweisbar.
10. Der Mindest-Testumfang gemaess Abschnitt 9.4 ist nachweisbar vollstaendig erfuellt.
11. Der lokale Workflow mit `gh` und `codex` gemaess Abschnitt 10.6 ist auf beiden macOS-Systemen nachweisbar anwendbar.
12. Bei API-/XML-Kommentar-Aenderungen ist die docfx-Dokumentation nachweisbar neu erzeugt worden.
13. Die docfx-Dokumentation ist ueber GitHub Pages des Repositories `hindermath/TuiVision` erreichbar und wird bei jedem Merge auf `main` mit Doku-relevanten Aenderungen automatisch aktualisiert (M-22).

## 13. Abgrenzung
Nicht Bestandteil der MUSS-Abnahme:
- Vollstaendige bitgenaue Replikation jeder historischen Plattformbesonderheit
- NuGet-Veroeffentlichung (optional)
- Native OS-Bindings oder betriebssystemspezifische Zusatzruntimes
