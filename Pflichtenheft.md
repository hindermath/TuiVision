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
- Im aktuellen Arbeitsverzeichnis ist noch kein initialisiertes Git-Repository vorhanden.
- Es sind noch keine C#-Quelldateien vorhanden.

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
| M-07 | Unit-Tests fuer portierte Klassen/Methoden mit MSTest | Testprojekte pro Modul, CI-faehig | Tests laufen lokal und in CI stabil durch |
| M-08 | API-Dokumentation mit docfx | docfx-Konfiguration, API-Generierung und erneute Doku-Erstellung bei API-/XML-Kommentar-Aenderungen | Doku erzeugbar, verlinkt alle Kern-Namespaces und ist nach API-/XML-Aenderungen aktualisiert |
| M-09 | Portierung der vorhandenen Beispiele | Alle 25 Beispiele als .NET-Beispiele abbilden | Beispiele bauen; definierte Smoke-Tests bestehen |
| M-10 | Qualitaetssicherung zusaetzlich zu Unit-Tests | Analyzer, Format- und Build-Gates | Qualitaets-Gates sind dokumentiert und aktiv |
| M-11 | Keine nativen OS-Abhaengigkeiten | Keine P/Invoke-/Native-Library-Pflicht, keine OS-spezifischen Zusatzpakete | Build/Tests laufen mit .NET 10 Runtime ohne native Zusatzinstallation |
| M-12 | Lizenz-Disclaimer fuer Beispielcharakter | Sichtbarer Hinweis in `LICENSE`/`README` | Hinweis beschreibt: Beispielprojekt, keine Konkurrenzabsicht, keine beabsichtigte Lizenzverletzung |
| M-13 | CI/CD mit GitHub Actions | Build-/Test-Workflow unter `.github/workflows` | Automatischer Build und Testlauf pro Push/PR ist aktiv |
| M-14 | Nutzerdokumentation | Leitfaeden, Einstieg und Nutzung unter `docs/guides` | Dokumentation ist vorhanden, nachvollziehbar und aktuell zum Stand der Portierung |
| M-15 | Vollstaendige XML-Kommentierung der oeffentlichen API | Alle `public` Typen, Member, Parameter, Rueckgabewerte und Ausnahmen mit XML-Dokumentation | API ist durchgaengig und didaktisch ausfuehrlich kommentiert; docfx erzeugt daraus vollstaendige Referenzseiten |
| M-16 | Einheitlicher didaktischer Dokumentationsstil | Alle Dokumentationsartefakte folgen einem verbindlichen Lehr-/Beispielstandard fuer Fachinformatiker (Anwendungsentwicklung) | Struktur, Detailtiefe und Beispiele sind ueber alle Dokuarten konsistent und nachvollziehbar |
| M-17 | Ausfuehrliche Dokumentation der Beispielprogramme | Pro Beispielprogramm eigener Guide mit Lernziel, Voraussetzungen, Start, Bedienung, Architekturhinweisen und Uebungen | Alle portierten Beispiele sind didaktisch nachvollziehbar dokumentiert und reproduzierbar ausfuehrbar |
| M-18 | Ausreichende Quellcode-Dokumentation im gesamten TuiVision-Code | Nicht-triviale Logik, Architekturentscheidungen und interne Zusammenhaenge werden im Code nachvollziehbar kommentiert | Der Quellcode erfuellt die pruefbaren Kriterien aus Abschnitt 10.5 und ist fuer Fachinformatiker (Anwendungsentwicklung) lern- und wartbar |
| M-19 | Messbarer Mindest-Testumfang (MUSS-Tests) | Definierte Mindestabdeckung, Pflichttestfaelle und vollstaendige Smoke-Tests gemaess Abschnitt 9.4 | Die in Abschnitt 9.4 definierten Kennzahlen und Testumfaenge sind vollstaendig erreicht |
| M-20 | Reproduzierbarer Multi-Mac-Entwicklungsworkflow | Build-, Test-, GitHub- und Codex-Arbeitsablaeufe sind fuer `MacBook Air M2` und `Mac mini M4 Pro` mit `gh` und `codex` dokumentiert | Die dokumentierten Schluesselablaeufe funktionieren auf beiden Systemen mit den dokumentierten Voraussetzungen und ggf. automatisierter Tool-Bereitstellung (z. B. DocFX als .NET-Tool) |

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
8. **Beispiele**: Portierung aller 25 Beispiele in Wellen

### 8.2 Beispielprogramme (MUSS-Umfang)
Zu portieren sind alle vorhandenen Beispielordner:
`bhelp`, `clipboard`, `cyrillic`, `demo`, `desklogo`, `dlgdsn`, `dyntxt`, `eterm`, `fonts`, `helpdemo`, `i18n`, `inplis`, `listvi`, `msgcls`, `progba`, `sdlg`, `sdlg2`, `tcombo`, `terminal`, `tprogb`, `tutorial`, `tvedit`, `tvhc`, `videomode`, `xterm`.
Fuer jedes portierte Beispiel ist eine eigene didaktische Dokumentationsseite in `docs/guides/examples/` bereitzustellen.

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

## 11. Risiken und Randbedingungen

| Risiko | Bewertung | Gegenmassnahme |
|---|---|---|
| Lizenzlage der historischen Quellen (Borland/Inprise + spaetere GPL/BSD-Anteile) | mittel/hoch | Klarer Disclaimer (M-12), saubere Trennung eigener und uebernommener Inhalte, juristisch-technische Klaerung |
| API-/Verhaltensunterschiede zwischen Original und Port | mittel | Golden-Tests mit Beispielprogrammen, dokumentierte Abweichungen |
| Konsolidierung vieler historischer Treiber auf einen Managed-.NET-Treiber | mittel | Funktionale Priorisierung nach Kernfeatures, Regressionstests |
| Umfang der Beispielportierung | mittel | Inkrementelle Wellen, pro Beispiel Smoke-Test |
| Vermeidung nativer Abhaengigkeiten bei zugleich hoher Funktionsnahe | mittel | Architekturregeln (M-11) und Build-Checks ohne Native-Payload |

## 12. Abnahmekriterien
Die Abnahme gilt als bestanden, wenn:
1. Alle MUSS-Anforderungen M-01 bis M-20 nachweisbar erfuellt sind.
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

## 13. Abgrenzung
Nicht Bestandteil der MUSS-Abnahme:
- Vollstaendige bitgenaue Replikation jeder historischen Plattformbesonderheit
- NuGet-Veroeffentlichung (optional)
- Native OS-Bindings oder betriebssystemspezifische Zusatzruntimes
