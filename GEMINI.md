# GEMINI.md - TuiVision Project Context

Dieses Dokument dient als zentraler Kontext für die Arbeit an **TuiVision**, einer Portierung von Turbo Vision 2.0.3 nach C#/.NET 10.

## 🚀 Projektübersicht

*   **Zweck:** Portierung des Turbo Vision Frameworks (C/C++) in ein modernes, rein verwaltetes (managed) .NET 10 Framework.
*   **Zielgruppe:** Fachinformatiker (Anwendungsentwicklung), dient als Lern- und Referenzprojekt für Agentic-AI-Workflows.
*   **Technologiestack:** C# 14, .NET 10 (`net10.0`), MSTest, docfx.
*   **Leitprinzipien:** Didaktisch wertvolle Dokumentation, keine nativen Abhängigkeiten, plattformübergreifend (macOS, Linux, Windows).

## 🏗️ Architektur & Projektstruktur

Das Projekt folgt einer modularen Struktur gemäß .NET Best Practices:

*   **`src/TuiVision.Core`**: Basisobjekte (`TObject`), Geometrie (`TPoint`, `TRect`), Ereignissystem (`TEvent`).
*   **`src/TuiVision.Controls`**: UI-Komponenten (`TView`, `TGroup`, Fenster, Dialoge).
*   **`src/TuiVision.Drivers.Console`**: Plattformübergreifender Konsolentreiber (rein managed).
*   **`src/TuiVision.Serialization`**: Streams, Ressourcen und Persistenz.
*   **`src/TuiVision.Compatibility`**: Hilfsklassen zur Erleichterung der Portierung von historischem Code.
*   **`tests/`**: Parallele Testprojekte für jedes Modul unter Verwendung von MSTest.
*   **`examples/`**: Portierte Beispielprogramme aus der Turbo Vision Distribution.
*   **`tv203s/`**: Historischer C++ Quellcode von Turbo Vision 2.0.3 als Referenzbasis.

## 🛠️ Build & Development

### Zentrale Befehle
*   **Bauen:** `dotnet build`
*   **Testen:** `dotnet test`
*   **Dokumentation:** `docfx docfx.json` (wenn im Projektwurzelverzeichnis vorhanden)
*   **Formatierung:** `dotnet format`
*   **DocFX-A11y-Smoke-Test:** `cd tests/web-a11y && npm install && npx playwright install chromium && npm run test:docfx`
*   **Regel fuer Doku-Neubau:** Nach jedem erfolgreichen `docfx docfx.json` den passenden A11y-Smoke-Test unter `tests/web-a11y/` direkt im selben Arbeitsschritt ausfuehren.

### Arbeitsumgebung
*   Optimiert für **Multi-Mac Workflow** (MacBook Air M2 & Mac mini M4 Pro).
*   Nutzung von `gh` (GitHub CLI) und `codex` für Agentic-Workflows.
*   Haupt-IDE: **JetBrains Rider**.

## 📝 Entwicklungskonventionen

1.  **Code-Stil:** Modernes C# (File-scoped Namespaces, Expression-bodied Members, Nullable Reference Types).
2.  **JSON-Verarbeitung:** Für projektinternes JSON-Parsing und JSON-Serialisierung ist `System.Text.Json` zu verwenden. `Newtonsoft.Json` darf nur mit dokumentierter Begründung und expliziter Freigabe im Review eingeführt werden.
3.  **Dokumentation (MUSS):**
    *   Dokumentationsblöcke zweisprachig: erst Deutsch, dann Englisch.
    *   Beide Sprachfassungen auf CEFR-B2-Niveau.
    *   Grosse normative Dokumente wie `Pflichtenheft*.md` und `Lastenheft*.md` duerfen statt eines uebergrossen Inline-Zweisprachblocks als synchron gepflegte `.EN.md`-Parallelfassung ausgeliefert werden; die deutsche Fassung bleibt kanonisch, sofern nichts anderes markiert ist.
    *   Programmierung #include<everyone> — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI) mit Deutsch und Englisch als Arbeitssprachen sowie an sehbehinderte Lernende, die mit Braille-Displays, Screen-Readern oder Textbrowsern arbeiten. Barrierefreiheit ist kein Nice-to-have, sondern Pflichtanforderung.
    *   Erzeugte HTML-Dokumentation soll mindestens WCAG 2.2 Konformitaetsstufe AA als Barrierefreiheits-Basis anstreben.
    *   Die Smoke-Tests unter `tests/web-a11y/` mit Playwright und `@axe-core/playwright` muessen bei DocFX-Struktur- oder API-Doku-Aenderungen mitgezogen werden; `lynx` dient als zusaetzlicher Textbrowser-Gegencheck.
    *   Jeder DocFX-Neubau gilt erst dann als abgeschlossen, wenn der zugehoerige A11y-Smoke-Test ebenfalls erfolgreich war.
    *   Wichtige Aussagen duerfen nicht nur ueber Farbe, Layout oder Mauszeiger-Hinweise transportiert werden; bevorzugt werden semantische Ueberschriften, Listen, Tabellen und ASCII-/Textdiagramme.
    *   Bilinguale CEFR-B2-Lieferung und der dokumentierte A11Y-Nachweis gehoeren zur formalen Abschlusspruefung fuer lernrelevante Doku und aktive Anforderungsartefakte.
    *   Vollständige XML-Kommentare für alle öffentlichen APIs (`summary`, `param`, `returns`, `remarks`).
    *   Didaktischer Stil: Erklärt das *Warum* und bietet Beispiele für Lernende.
    *   Aktualisierung der Dokumentation erfolgt zeitgleich mit Codeänderungen.
4.  **Testing:**
    *   Mindestens 70% Line Coverage jeweils fuer `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console`.
    *   Die verbindliche Coverage-Messung erfolgt aus dem Repository-Root mit `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`.
    *   `coverlet.runsettings` ist die kanonische TuiVision-Coverage-Gate-Konfiguration und MUSS gepflegt werden, wenn gate-relevante Assemblies, Beispiel-Assemblies oder Testprojekte hinzukommen, umbenannt oder entfernt werden. Vor Nutzung des Gates nach Moeglichkeit mit `xmllint --noout coverlet.runsettings` validieren.
    *   Jedes Feature benötigt Unit-Tests (MSTest) und ggf. Smoke-Tests in den Beispielen.
5.  **Keine Nativen Abhängigkeiten:** Alle Treiber müssen rein in verwaltetem Code implementiert sein (kein P/Invoke, wo vermeidbar).
6.  **Lizenztreue:** Einhaltung der MIT-Lizenz für neuen Code; Respektierung der Original-Lizenzen im `tv203s` Ordner.

## 🌿 Branch-Konvention

*   Feature-Branches verwenden entweder die agentenpraefixierte Form `codex/<feature-description>` (oder ein anderes unterstuetztes Praefix wie `claude/`, `gemini/`, `copilot/`, `opencode/`) oder die nummerierte Spec-Kit-Form `NNN-short-description`, wenn der Spec-Kit-Workflow diesen Branch-Typ erzeugt.
*   CI reagiert auf Pushes nach `main`, `master`, `codex/**`, `claude/**`, `gemini/**`, `copilot/**` und `opencode/**`.
*   Die repo-weite Versionslogik liegt in `Directory.Build.props`: `Version`, `AssemblyVersion` und `FileVersion` folgen `Major.Minor.Patch.Build`, wobei `Minor` die numerisch interpretierte Spec-Kit-Feature-/Branch-Nummer als kanonische PR-Nummer verwendet (`007` -> `7`), `Patch` der Commit-Anzahl im Feature-/PR-Branch nach dem aktuellen Commit entspricht und `Build` nur vor `dotnet build` oder `dotnet test` manuell erhoeht wird.
*   Wenn ein dedizierter Feature-Branch die Anforderungen eines Lastenhefts umgesetzt hat, wird die Datei in `Lastenheft_<Thema>.<feature-branch>.md` umbenannt, damit der gelieferte Umfang im Repository nachvollziehbar bleibt.

## 📚 Wichtige Dokumente
*   `README.md`: Allgemeine Einführung und CI-Status.
*   `Lasten_Heft.md`: Grobe Anforderungen und Ziele.
*   `Pflichtenheft.md`: Detaillierte technische Spezifikation (Referenz für MUSS-Anforderungen).
*   `docs/guides/multi-mac-workflow.md`: Anleitung für die verteilte Entwicklung.

## 🎯 Aktueller Feature-Fokus

### `004-editor-file-help-streams`
*   Spezifikationsquelle: `specs/004-editor-file-help-streams/spec.md`
*   Umsetzungsgrundlage dieses Inkrements: `specs/004-editor-file-help-streams/spec.md` und `specs/004-editor-file-help-streams/plan.md`
*   Geplanter Umfang in `src/TuiVision.Controls` und `src/TuiVision.Serialization`:
    *   `TEditor`, `TMemo`, `TFileEditor`, `TEditWindow`
    *   Datei-/Verzeichnisdialoge, Pfad-History und zugehörige Hilfskomponenten
    *   `THelpTopic`, `THelpFile`, `THelpViewer`, `THelpWindow`
    *   Stream-Primitiven und benannte Ressourcencontainer
*   Verhalten:
    *   Editor-Flows müssen Bearbeitung, Insert/Overwrite-Modus, Clipboard-Aktionen, Suche/Ersetzen, Modified-State, explizite Safe-Close-Entscheidungen und getrennte Overwrite-Entscheidungen bei Save-Konflikten abdecken
    *   Datei-Flows müssen Dateiliste, Verzeichnisnavigation, aktuelle Datei-Metadaten, Wildcard-Filter, manuelle Pfadeingabe und History-Rückruf synchron halten
    *   Hilfe-Flows müssen kontextbezogene Topics, Querverweise und Fallback-Inhalte für fehlende Kontexte unterstützen
    *   Stream-/Ressourcen-Flows müssen benannte Ablage und explizite Fehlerbehandlung bei gekuerzten, ueberhaengenden, unbekannten oder zyklischen Persistenzdaten abdecken
    *   Integrationsabdeckung muss Event-Loop-Verhalten, Fokuswechsel, Menueausfuehrung und explizite Dialoginteraktion fuer dieses Feature direkt benennen
*   Festgezogene Planungsentscheidungen:
    *   Runtime-Hilfe kommt aus dedizierten Help-Dateien
    *   Shared References bleiben erhalten, zyklische Objektgraphen sind nicht Teil der Abnahme
    *   Resource-Keys sind exakt case-sensitive
    *   Neue Dateien verwenden `LF`; geladene Dateien behalten ihr Zeilenendformat
    *   Externe Dateiaenderungen erfordern vor dem Ueberschreiben eine explizite Entscheidung
*   Explizit nicht Teil dieses Schritts:
    *   Beispielprogramme wie `tvedit`, `bhelp` und `helpdemo`
    *   Treiberkonsolidierung
    *   Rechner-/Makro-/OS-Shell-Integrationen
    *   sonstige fachfremde Spezial-Widgets

### `005-driver-consolidation-m07`
*   Spezifikationsquelle: `specs/005-driver-consolidation-m07/spec.md`
*   Planungsgrundlage dieses Inkrements: `specs/005-driver-consolidation-m07/spec.md` und `specs/005-driver-consolidation-m07/plan.md`
*   Geplanter Umfang:
    *   der verwaltete Konsolentreiber in `src/TuiVision.Drivers.Console`
    *   begleitende Validierung in `tests/TuiVision.Drivers.Tests`
    *   das M-07-Beweis-Ledger `docs/porting-status.md`
*   Festgezogene Planungsentscheidungen:
    *   `.cc`-Dateien sind der formale `M-07`-Ledger-Scope
    *   zugehoerige `.c`-/`.h`-Dateien duerfen nur als Begruendungs- oder Referenzmaterial auftreten
    *   Capability-Buckets ersetzen eine Eins-zu-eins-Abbildung pro historischem OS-Treiber
    *   Phase 7 bleibt bewusst von der spaeteren vollstaendigen Phase-8-Gateschliessung getrennt
*   Explizit nicht Teil dieses Schritts:
    *   die 25 verpflichtenden Beispielwellen
    *   eine vollstaendige Phase-8-Gateschliessung
    *   neue Quellmodule oder native Bindings

### `006-close-phase8-gate`
*   Spezifikationsquelle: `specs/006-close-phase8-gate/spec.md`
*   Planungsgrundlage dieses Inkrements: `specs/006-close-phase8-gate/spec.md` und `specs/006-close-phase8-gate/plan.md`
*   Geplanter Umfang:
    *   `docs/porting-status.md` als autoritatives M-07-Ledger
    *   Gate-Nachweise fuer Build, Volltests, Coverage, Formatierung und API-Dokumentation
    *   Fortschreibung der Pflichtenheft- und Review-Artefakte fuer die Phase-8-Entscheidung
*   Festgezogene Planungsentscheidungen:
    *   keine `portiert + Test ausstehend`-Zeilen mehr nach behauptetem Gate-Abschluss
    *   `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` muessen jeweils die explizite 70-%-Line-Coverage-Huerde mit getrenntem Assembly-Nachweis erreichen
    *   `tests/TuiVision.Compatibility.Tests/` ist die geplante dedizierte Compatibility-Fallback-Suite, falls geteilte Testprojekte fuer einen ehrlichen Nachweis nicht ausreichen
    *   Platzhalter- oder No-op-Module duerfen das Eingangstor nicht nur formal ueber triviale Tests bestehen
    *   Gate-Scope-Entfernungen muessen die Nachweisartefakte im selben Aenderungspaket nachziehen; Skip-/Ignore-Faelle brauchen dokumentierte Tracking-Issue-Referenzen; offene lokale-vs.-CI-Coverage-Konflikte blockieren den Abschluss
    *   die 25 MUSS-Beispiele bleiben bis zum formalen Gate-Abschluss blockiert
*   Explizit nicht Teil dieses Schritts:
    *   Beginn der Beispielportierungen
    *   Ersatzumfang aus `TVDEMOS/` oder `TVFM/`
    *   fachfremde neue Framework-Features

## 🔄 Synchronisationsregel für KI-Agenten-Dateien

*   Wenn sich aktiver Feature-Kontext, Planungsstand oder gemeinsam genutzte Agenten-Hinweise ändern, müssen diese Dateien gemeinsam geprüft und bei Bedarf im selben Arbeitsgang aktualisiert werden:
    *   `AGENTS.md`
    *   `CLAUDE.md`
    *   `GEMINI.md`
    *   `.github/copilot-instructions.md`
    *   `.github/agents/copilot-instructions.md`
*   Eine nur teilweise Synchronisierung ist nicht zulässig, wenn sich gemeinsame Vorgaben geändert haben.
*   Falls eine Datei absichtlich agentenspezifisch abweicht, muss diese Abweichung im selben Change ausdrücklich dokumentiert werden.

## 📊 Projektstatistik

*   `docs/project-statistics.md` ist das fortlaufende Statistik-Register des Repositories.
*   Die Datei muss nach jeder abgeschlossenen Spec-Kit-Implementierungsphase, nach jeder agentischen Änderung am Repository und auf explizite Anforderung aktualisiert werden.
*   Im `## Fortschreibungsprotokoll` muessen die Tabelleneintraege strikt chronologisch stehen: der aelteste Eintrag oben, der juengste und zuletzt eingetragene Eintrag unten; Eintraege mit demselben Datum behalten ihre Eintragungsreihenfolge.
*   Als letzter Top-Level-Block der Datei muss immer ein `## Gesamtstatistik`-Abschnitt stehen; danach darf kein weiterer Top-Level-Abschnitt folgen.
*   Innerhalb dieses finalen `## Gesamtstatistik`-Abschnitts muessen kompakte ASCII-only-Diagramme direkt unter der textlichen Gesamtauswertung mitgefuehrt werden; sie sollen mindestens Artefaktmix, die dokumentierten Branch-/Phasenverlaeufe, die dokumentierten Beschleunigungsfaktoren durch agentische KI plus Spec-Kit/SDD und einen direkten Vergleich zwischen erfahrener Entwickler-Referenz, Thorsten-Solo-Referenz und sichtbarem KI-Lieferfenster zeigen und bei jeder Statistikpflege mitaktualisiert werden.
*   Jeder kurze Erklaertext in CEFR-B2-Sprache muss direkt bei seiner ASCII-Diagrammgruppe stehen, idealerweise unmittelbar davor oder danach, damit Auszubildende nicht zwischen Erklaerung und Diagramm scrollen muessen.
*   Wenn Daten entlang einer X-Achse als Verlauf besser lesbar werden, sollen zusaetzlich einfache ASCII-X/Y-Diagramme eingefuegt werden. Diese muessen bewusst grob, in reinem Markdown lesbar und ebenfalls in CEFR-B2 erklaert bleiben.
*   Der Statistikblock muss fuer Braille-Zeile, Screenreader und Textbrowser plain-text-freundlich bleiben; ASCII-Diagramme und Erklaerungen duerfen keine Schluesselaussage nur ueber visuelles Layout transportieren.
*   Wenn sich DocFX-Inhalte, Navigationsstruktur oder API-Praesentation aendern, sind repraesentative `_site/`-Seiten ueber einen textorientierten Pruefpfad zu kontrollieren, bevorzugt mit einem lokalen Playwright-Accessibility-Snapshot.
*   Fuer erzeugte HTML-Dokumentation gilt WCAG 2.2 AA als konkrete Pruefbasis, besonders fuer Seitensprache, Bypass-Mechanismen, sichtbaren Tastaturfokus, Non-Text-Contrast und verstaendliche Landmark-Struktur.
*   Jeder Eintrag muss Branch oder Phase, beobachtbares Arbeitsfenster, Produktions-, Test- und Doku-Zeilen, die wesentlichen Arbeitspakete, die konservative Handarbeits-Basis von 80 Codezeilen pro Tag fuer einen erfahrenen Entwickler sowie die repo-spezifische Thorsten-Solo-Vergleichsbasis von 125 Zeilen pro Arbeitstag fuer diese Pascal-/Turbo-Vision-Portierung enthalten.
*   Beschleunigungsangaben muessen beide Referenzen gegen sichtbare Git-Aktivtage stellen und ausdruecklich als repo-weiten Verdichtungsfaktor statt als Stoppuhrmessung kennzeichnen.
*   Wenn Stundenwerte ausgewiesen werden, sind die Tageswerte mit der TVoeD-Arbeitszeit von `7,8 Stunden` bzw. `7 Stunden 48 Minuten` pro Arbeitstag umzurechnen.

## 🖥 Workflow-Plattformen

*   Der Multi-Mac-Aufbau auf `MacBook Air M2` und `Mac mini M4 Pro` ist der primaere Entwicklungs- und Alltagstest-Workflow.
*   Auf beiden Macs muessen `gh`, `specify`, `codex`, `claude`, `copilot` und `gemini` installiert sein; vor Spec-Kit-Arbeiten oder Spec-Kit-Updates ist `specify check` auszufuehren, damit die benoetigte Werkzeugkette bestaetigt ist.
*   Nach jedem `/speckit-plan`-Lauf oder einer gleichwertigen Plan-Aktualisierung, die aktive Technologien, Projektstruktur oder Agent-Kontext aendert, ist `.specify/scripts/bash/update-agent-context.sh` standardmaessig fuer `codex`, `claude`, `gemini` und `copilot` im selben Arbeitsgang auszufuehren. Diese Multi-Agenten-Kontextaktualisierung gilt in diesem Repository als vorab freigegebene Wartungsroutine und braucht keine gesonderte Rueckfrage.
*   Linux und Windows dienen zusaetzlich als Kompatibilitaets- und Validierungsumgebungen; unter Windows ist WSL mit einer aktuellen Ubuntu-Version, derzeit bevorzugt `Ubuntu 24.04`, der empfohlene Weg.
*   Wenn Aenderungen Laufzeitverhalten, Build-Stabilitaet, Terminalverhalten oder Portabilitaet betreffen, sollen Linux- und Windows/WSL-Kompatibilitaetschecks nach Moeglichkeit mitgefuehrt und in CI oder gleichwertigen Nachweisen sichtbar gemacht werden.

## ▶ Pflichtenheft-Marker fuer den naechsten Schritt

*   In `Pflichtenheft.md` ist ein gut sichtbarer Marker `>>> NAECHSTER SCHRITT <<<` zu pflegen.
*   Der Marker muss immer auf den aktuell hoechstprioren offenen Arbeitsschritt im Abschnitt der priorisierten Restarbeiten zeigen und bei Fortschritt entsprechend weitergeschoben werden.

---
*Hinweis: Dieses Dokument wurde automatisch von Gemini CLI generiert und dient als Instruktionsbasis.*


## Gemeinsame Governance-Ergaenzung / Shared Governance Addendum

- Alle nutzerseitigen Artefakte muessen barrierefrei gedacht und geprueft werden: CLI-Ausgaben, Dokumentation, HTML, UI und generierte Templates; WCAG 2.2 Level AA ist die Standard-Basis, sobald die Kriterien auf das Artefakt anwendbar sind.
- All user-facing artefacts must be designed and reviewed for accessibility: CLI output, documentation, HTML, UI, and generated templates; WCAG 2.2 Level AA is the default baseline wherever the criteria apply.

- Fuer C#/.NET-Repositories gilt standardmaessig eine Thorsten-Solo-Basis von `125` Zeilen/Arbeitstag, sofern das Repo keinen abweichenden, begruendeten Wert dokumentiert.
- The default Thorsten-solo baseline for C#/.NET repositories is `125` lines/workday unless the repository documents a justified deviation.

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Lokales Dateisystem sowie persistente binaere Help-/Ressourcen-Streams; keine Datenbank in diesem Inkrement (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Versionskontrollierter Markdown-Nachweis in `docs/porting-status.md`; keine Datenbank; Kompatibilitaetsnachweise duerfen als Repo-Notizen oder dokumentierte Kommandoausgaben vorliegen (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, bestehende MSTest-Suiten plus ggf. notwendige Compatibility-spezifische Validierung, Coverlet-Nachweise, `dotnet format`, docfx, `Pflichtenheft.md` und `docs/porting-status.md` fuer den formalen Phase-8-Gate-Nachweis (006-close-phase8-gate)
- Reine Repository-Nachweisartefakte; keine Datenbank und keine Beispielanwendungs-Auslieferung in diesem Inkrement (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Bestehendes `TuiVision.Core`-Geometrie-/Event-/Buffer-Fundament; bestehende `TuiVision.Controls`-Shell- und Widget-Basis aus `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-orientiertes `TIndicator` nur als Kontrastfall); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; optional `docfx docfx.json`; GitHub Actions plus vorhandene Example-Smoke-Infrastruktur fuer die nachgelagerte Wave-2-Readiness (009-controls-widgets-and-collections)
- In-Memory-UI-Zustand in Produktion; versionskontrollierte Planungs-, Test-, Nachweis- und bereits gelieferte Example-Artefakte in `specs/`, `tests/`, `docs/` und `examples/`; keine Datenbank oder externer Persistenzdienst (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes
- 004-editor-file-help-streams: Spezifikation und Requirements-Checklist fuer Phase 6 (Editor/Datei/Hilfe/Streams) angelegt.
- 004-editor-file-help-streams: Planartefakte (`plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/public-api.md`) erstellt und gemeinsame Agent-Hinweise auf den Post-Plan-Stand synchronisiert.
- 004-editor-file-help-streams: Plan-Review-Klarstellungen fuer Safe-Close vs. Overwrite, Wildcard-Filter in Dateidialogen, explizite Stream-Fehlerfaelle und nicht-funktionale Abgrenzungen eingearbeitet.
- 004-editor-file-help-streams: Editor-Clipboard/Overwrite, Datei-Metadaten-Synchronisation, Shell-Menue-/Status-Routing und das volle Coverage-Gate fuer Core/Controls/Serialization explizit nachgeschaerft.
- 004-editor-file-help-streams: Verbleibende Integrationsanforderungen fuer Event-Loop, Fokuswechsel, Menueausfuehrung und explizite Dialoginteraktion direkt in den Feature-Artefakten benannt.
- 005-driver-consolidation-m07: Spezifikation und Klarstellungen fuer Phase 7 angelegt, inklusive `M-07`-Nachweis ueber `docs/porting-status.md`, Pflicht-Primaarziel pro historischer Datei und reviewbarer Linux/Windows/WSL-Kompatibilitaetsnachweise.
- 005-driver-consolidation-m07: Planartefakte (`plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/phase-7-proof-contract.md`) erstellt und den Phase-7-Ansatz als capability-basierte Treiberkonsolidierung mit formalem `.cc`-Ledger-Scope beschrieben.
- 005-driver-consolidation-m07: Phase-7-Implementierung abgeschlossen: `DriverCapabilityMap.cs` mit 5 Faehigkeitsgruppen erstellt, `docs/porting-status.md` mit allen 151 historischen `.cc`-Dateien aufgebaut, 5 neue Treiber-Testdateien (30 Tests bestanden), `docs/guides/multi-mac-workflow.md` um Kompatibilitaetsnachweis erweitert, `checklists/phase-8-gate-review.md` angelegt, `Pflichtenheft.md`-Marker auf Phase-8-Gate-Abschluss verschoben.
- 006-close-phase8-gate: Spezifikation und Requirements-Checklist fuer den finalen `M-07`-Abschluss und die nachweisbare Phase-8-Gateschliessung angelegt; gemeinsamer Agent-Kontext auf den neuen Prioritaetsschritt synchronisiert.
- 006-close-phase8-gate: Die harte Coverage-Regel auf `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` jeweils `>= 70 %` erweitert und in die gemeinsamen Gate-Artefakte synchronisiert.
- 006-close-phase8-gate: Planartefakte (`plan.md`, `research.md`, `data-model.md`, `quickstart.md`, `contracts/phase-8-gate-contract.md`) erstellt und den finalen Framework-Vollbeweis mit 5x-70%-Coverage, Repository-Volltestlauf und dediziertem Gate-Closure-Commit als verbindlichen Planstand beschrieben.
- 006-close-phase8-gate: Plan nachgeschaerft: Coverage gilt assembly-scharf pro Gate-Modul, und Platzhalter-/No-op-Code zaehlt nicht als gueltiger Phase-8-Abschluss.
- 006-close-phase8-gate: Analyse-Remediation eingearbeitet: `gate-docs.md` ist nun expliziter Bestandteil des Plan-Artefaktsets, `tests/TuiVision.Compatibility.Tests/` ist als feste Fallback-Suite benannt, und Skip-/Ignore-Faelle muessen in den Gate-Nachweisen auf ein dokumentiertes Tracking-Issue verweisen.
- 007-port-wave1-examples: Welle 1 portiert: `desklogo`, `msgcls`, `tutorial` (16 token-basierte Schritte), `videomode`. 41 Smoke-Tests gruen, Release-Build sauber, `dotnet format --verify-no-changes` bestanden. `Pflichtenheft.md` Welle-1-Checkliste abgehakt, Marker auf Welle 2 vorgeschoben.
- 008-controls-revision: Controls-Revision implementiert: `TSubMenu`, `TStatusDef`, `WindowFlags` neu; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` erweitert; 338 Tests gruen, 84,02 % Abdeckung, Format-Gate bestanden; Nachweissurfaces aktualisiert, Lastenheft umbenannt.
- 010-standard-dialogs-designer: Framework-Readiness fuer Standarddialoge und Dialog-Designer implementiert: Datei-/Verzeichnisentscheidungen ohne Dateiinhalt-I/O, Color-/Display-/symbolische Charset-Auswahl, validierte Dialogbeschreibung, persistierter Roundtrip und fehlerhafte Persistenzdaten-Ablehnung; fokussierte Controls- und Serialization-Tests gruen.
- 011-port-wave2-examples: Planartefakte fuer Welle 2 erstellt: 11 neue Beispielprojekte, dedizierte Example-Smoke-Tests, DE-first/EN-second Guides, Architektur-/Security-/A11Y-Nachweise und klarer `sdlg`/`sdlg2`-Scope als historische ScrollDialog/ScrollGroup-Beispiele.

## Shared Parent Guidance

*   Die gemeinsamen Dateien `/Users/thorstenhindermann/RiderProjects/AGENTS.md` und `/Users/thorstenhindermann/RiderProjects/GEMINI.md` speichern die repo-uebergreifenden Basisregeln.
*   Diese Projekt-Datei ist die spezifischere Autoritaet fuer projektspezifische Build-Befehle, Workflows, Architektur und Features.

---

## Level-2-Umgebungsregister / Level-2 Environment Registry

- Die zentrale `constitution.md` enthält das verbindliche Level-2 Project Environment Registry.
- Spec-Kit-Pläne und Gemini-Arbeit in Level-2-Projekten müssen die passende Registry-Zeile als verbindlichen Kontext für Runtime, Build/Test, A11Y, Statistik und Agentenflächen verwenden.
- Änderungen an einer Level-2-Runtime, Toolchain oder Statistik-Basis müssen `constitution.md`, `.specify/memory/constitution.md` und betroffene KI-Agenten-Dateien gemeinsam prüfen.

*The central `constitution.md` contains the binding Level-2 Project Environment Registry. Spec-Kit plans and Gemini work in Level-2 projects must use the matching registry row as binding context for runtime, build/test, A11Y, statistics, and agent surfaces. Changes to Level-2 runtime, toolchain, or statistics baselines require a joint review of `constitution.md`, `.specify/memory/constitution.md`, and affected AI-agent files.*
## Memory-Safe Languages (MSL) / Speichersichere Sprachen

- Level-2-Projekte SOLLEN eine speichersichere Sprache (Memory-Safe Language, MSL) als primäre Laufzeit verwenden, wenn die Zielplattform es erlaubt.
- Verbindliche MSL-Erlaubnisliste, Regeln und Begründungspflicht: siehe `constitution.md`, Prinzip XI.
- MSL-Kurzliste: Rust, Swift, C#, F#, Java, Kotlin, Scala, Go, Dart, Python, Ruby, JavaScript, TypeScript, Haskell, OCaml, Erlang, Elixir, Ada, SPARK.
- **Nicht** MSL (Begründung im Level-2-`constitution.md` erforderlich): C, C++, klassisches Objective-C, Assembly, `cc65`-C89, Zig (pre-1.0), Nim (manual), D ohne GC.
- In Nicht-MSL-Repositories (z. B. `C64Projects/cc65`) die im Level-2-`constitution.md` hinterlegte Begründung im Plan- und Task-Kontext erwähnen.
- `speckit.constitution` und `speckit.specify` SOLLEN bei Nicht-MSL-Primärsprache einen **nicht blockierenden** Hinweis ausgeben (Tooling-Aufgabe, separate Umsetzung).
- Änderungen an dieser Empfehlung erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*Level-2 projects SHOULD use a memory-safe language (MSL) as their primary runtime when the target platform allows. Authoritative rules: `constitution.md`, Principle XI. MSL short list: Rust, Swift, C#/F#, Java/Kotlin/Scala, Go, Dart, Python, Ruby, JavaScript/TypeScript, Haskell, OCaml, Erlang/Elixir, Ada/SPARK. Non-MSL languages (C, C++, Assembly, `cc65`, Zig pre-1.0, …) require a documented justification in the Level-2 `constitution.md`. In non-MSL repositories (e.g. `C64Projects/cc65`), surface the documented justification in plans and tasks. `speckit.constitution` and `speckit.specify` SHOULD emit a non-blocking advisory warning when the primary language is not an MSL — tracked as a separate tooling task. Changes to this recommendation require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sichere Code-Erzeugung / Secure Code Generation (ISO 27001/27002 A.8.28)

- KI-generierter Code MUSS den etablierten Secure-Coding-Best-Practices der Zielsprache und des Frameworks folgen. LLMs erzeugen nicht zuverlässig sicheren Code; explizite Durchsetzung ist erforderlich.
- Verbindliche Regeln und sprachspezifische Anforderungen: siehe `constitution.md`, Prinzip XII.
- Sprachspezifische Kurzregeln:
  - **C / C89**: Bounds-Checking, kein `gets()`, kein ungeprüftes `sprintf()`/`strcpy()`, CERT C.
  - **C# / .NET**: parametrisierte Queries, Output-Encoding gegen XSS, Anti-Forgery-Tokens, sichere Deserialisierung, Microsoft Secure Coding Guidelines.
  - **SQL**: nur parametrisierte Statements, kein dynamisches SQL aus nicht vertrauenswürdigem Input.
  - **Bash**: Variable in Anführungszeichen (`"$var"`), kein `eval` auf nicht vertrauenswürdigem Input, `--` End-of-Options.
  - **PowerShell**: `Set-StrictMode -Version Latest`, validierte Parameter, kein `Invoke-Expression` auf nicht vertrauenswürdigem Input.
- Kryptografie: aktuelle Algorithmen (AES-256, RSA >= 3072, SHA-256+, Ed25519); veraltete (MD5, SHA-1 für Signaturen, DES, RC4) nur mit expliziter Risikobegründung.
- Fehlerbehandlung darf keine internen Zustände, Stack-Traces oder Verbindungszeichenketten an Endbenutzer preisgeben.
- Hinzugefügte Abhängigkeiten müssen aktiv gepflegt sein und dürfen keine bekannten kritischen CVEs aufweisen.
- Code-Reviews MÜSSEN eine Sicherheitsperspektive für Eingabeverarbeitung, Authentifizierung, Autorisierung, Kryptografie und Datei-/Netzwerk-I/O enthalten.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated code MUST follow the secure-coding best practices of the target language and framework. Authoritative rules: `constitution.md`, Principle XII. Language-specific short rules: C/C89 — bounds checking, no `gets()`, CERT C; C#/.NET — parameterised queries, output encoding, anti-forgery tokens, Microsoft Secure Coding Guidelines; SQL — parameterised statements only; Bash — quoted variables, no `eval` on untrusted input, `--` sentinel; PowerShell — `Set-StrictMode`, no `Invoke-Expression` on untrusted input. Cryptography: use current algorithms (AES-256, SHA-256+, Ed25519); deprecated (MD5, SHA-1 for signatures, DES, RC4) only with explicit risk acknowledgement. Error handling must not expose internals. Dependencies must have no known critical CVEs. Code reviews must include a security perspective for input handling, auth, crypto, and I/O. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sichere Software-Architektur / Secure Software Architecture (ISO 27001/27002 A.8.27)

- KI-generierte und menschlich geschriebene Software-Architektur MUSS etablierten sicheren Architekturprinzipien folgen. Sicherer Code (Prinzip XII) ohne sichere Architektur reicht nicht aus — beide Ebenen müssen zusammenwirken.
- Verbindliche Regeln und sprachspezifische Architekturvorgaben: siehe `constitution.md`, Prinzip XIII.
- Verbindliche Architekturprinzipien:
  - **Trust Boundaries**: Explizite Vertrauensgrenzen definieren; alle Eingaben an Vertrauensgrenzen validieren und bereinigen.
  - **Defense in Depth**: Mindestens zwei unabhängige Sicherheitsschichten für kritische Assets.
  - **Least Privilege**: Jede Komponente, jeder Dienst und Prozess arbeitet mit minimalen Berechtigungen.
  - **Fail-Safe Defaults**: Zugriff standardmäßig verweigern, explizit gewähren; Fehlerpfade fallen in sicheren Zustand zurück.
  - **Angriffsfläche reduzieren**: Ungenutzte Endpunkte, Dienste und Debug-Funktionen deaktivieren oder entfernen.
  - **Separation of Concerns**: Authentifizierung, Autorisierung, Logging und Eingabevalidierung als Cross-Cutting Concerns implementieren, nicht ad-hoc verstreuen.
  - **Sichere Konfiguration**: Secrets in plattformgeeigneten Secret-Stores (z. B. Azure Key Vault, macOS Keychain), nie im Quellcode oder in Git-tracked Config-Dateien.
  - **Supply-Chain-Sicherheit**: Abhängigkeiten aus verifizierten Registries; Lock-Files committen; verwundbare Abhängigkeiten vor Release ersetzen.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated and human-written software architecture MUST follow secure-architecture principles. Authoritative rules: `constitution.md`, Principle XIII. Core principles: trust boundaries (validate all input at system boundaries), defense in depth (at least two independent security layers), least privilege (minimum required permissions), fail-safe defaults (deny by default), attack surface reduction (disable unused features), separation of concerns (auth/logging/validation as cross-cutting concerns), secure configuration (secrets in secret stores, never in code or Git), supply-chain security (verified registries, lock files, no known-vulnerable dependencies). Principles XII + XIII together form the complete secure-development approach: XII = tactical code-level security, XIII = strategic architecture-level security. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Allgemeine Architektur-Governance / General Architecture Governance (iSAQB / arc42)

- Architektur MUSS als explizites Arbeitsergebnis behandelt werden, wenn Struktur, Schnittstellen, Qualitätsattribute, Laufzeitverhalten, Deployment, Wartbarkeit oder technische Schulden betroffen sind.
- Verbindliche Regeln: siehe `constitution.md`, Prinzip XX; Architektur-Evidenz liegt standardmaessig unter `docs/architecture/`.
- Relevante Artefakte: Architecture Vision, Context View, Building-Block View, Runtime View, Deployment View, Quality Scenarios, Architecture Decision Records und Architecture Risks/Technical Debt.
- In `spec.md`, `plan.md` und `tasks.md` immer festhalten, ob Architektur-Evidenz erforderlich ist; `N/A` nur mit kurzer Begruendung.
- Bei sicherheitsrelevanter Architektur zusaetzlich die Secure-Architecture- und Security-Evidenz aus `docs/security/` anwenden.

*Architecture MUST be treated as an explicit work product when structure, interfaces, quality attributes, runtime behavior, deployment, maintainability, or technical debt are affected. Authoritative rules: `constitution.md`, Principle XX. Default evidence lives under `docs/architecture/`: architecture vision, context, building blocks, runtime, deployment, quality scenarios, ADRs, and architecture risks/technical debt. `spec.md`, `plan.md`, and `tasks.md` must state whether this evidence applies; `N/A` requires rationale. Security-relevant architecture also uses the secure-architecture evidence under `docs/security/`.*
## Sicherheitsdokumentation / Security Documentation (XII–XVIII Extensions)

- Jedes Level-2-Projekt MUSS die folgenden Sicherheitsdokumente pflegen, basierend auf den Templates in `.specify/templates/`:
  - **Bedrohungsmodell / Threat Model** (`threat-model-template.md`) — STRIDE-Methodik, Trust Boundaries, Risikobewertung, CAPEC-Referenzen (Prinzip XIII + XVII)
  - **Security Architecture Decision Records (S-ADR)** (`adr-template.md`) — architektonische Sicherheitsentscheidungen mit Compliance-Nachweis (Prinzip XIII)
  - **arc42 Section 8 Sicherheits-Querschnittskonzepte** (`arc42-security-template.md`) — Authentifizierung, Autorisierung, Verschlüsselung, Eingabevalidierung, Fehlerbehandlung, Logging, Abhängigkeiten, Deployment (Prinzip XIII)
  - **Sicherheits-Checkliste / Security Checklist** (`security-checklist-template.md`) — sprachspezifische Code-Review-Checkliste (Prinzip XII)
  - **Abhängigkeits-Audit / Dependency Audit** (`dependency-audit-template.md`) — CVE-Tracking, Lizenz-Compliance, Supply-Chain-Sicherheit (Prinzip XII)
  - **Sicherheits-Qualitätsszenarien / Security Quality Scenarios** (`security-quality-scenarios-template.md`) — iSAQB CPSA-F Qualitätsszenario-Methodik (Prinzip XII + XIII, SHOULD)
  - **ASVS-Verifikation / ASVS Verification** (`asvs-verification-template.md`) — OWASP ASVS Level, Scope und Evidenz (Prinzip XV, Web-/API-Projekte MUST)
  - **Supply-Chain-Evidenz / Supply Chain Evidence** (`supply-chain-evidence-template.md`) — SBOM, VEX, SLSA, OpenSSF Scorecard (Prinzip XVI, releasefähige Projekte MUST)
  - **Zero-Trust-Anwendbarkeit / Zero Trust Applicability** (`zero-trust-applicability-template.md`) — NIST SP 800-207-Bewertung (Prinzip XVIII, verteilte Systeme SHOULD)
  - **SAMM-Bewertung / SAMM Assessment** (`samm-assessment-template.md`) — OWASP SAMM Reifegrad und Verbesserungsplan (Prinzip XVIII, langlebige Projekte SHOULD)
- Projektspezifische Instanzen werden in `docs/security/` gepflegt; S-ADRs als einzelne Dateien in `docs/security/adr/`.

*Every Level-2 project MUST maintain security documents based on templates in `.specify/templates/`: threat model (STRIDE+CAPEC), S-ADRs, arc42 Section 8 security concepts, security checklist, dependency audit, security quality scenarios (SHOULD), ASVS verification (web/API MUST), supply-chain evidence (release-capable MUST), Zero Trust applicability note (distributed systems SHOULD), and SAMM assessment (long-lived projects SHOULD). Project-specific instances live in `docs/security/`; S-ADRs in `docs/security/adr/`. See `constitution.md`, Principles XII–XVIII for authoritative requirements.*
## Sicherheitsstandards & Anwendbarkeit / Security Standards & Applicability

- Vor jeder Level-2-Aufgabe die anwendbaren Sicherheitsstandards aus `constitution.md`, Prinzipien XIV-XVIII bestimmen und explizit benennen.
- `NIST SSDF` und `CWE Top 25` gelten immer für Level-2-Arbeit.
- `OWASP ASVS` gilt für Web-, API-, HTTP- und authentifizierte Dienste; der gewählte ASVS-Level muss benannt werden.
- `SBOM` gilt für releasefähige oder verteilbare Artefakte; `VEX`, wenn bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten bewertet werden müssen.
- `SLSA` gilt als Soll-Vorgabe für CI/CD- oder veröffentlichte Artefakte; `Zero Trust` ist für verteilte, servicebasierte, cloudnahe oder remote-verwaltete Systeme explizit zu prüfen.
- `CAPEC` soll in Bedrohungsmodellen für die risikoreichsten Angriffswege verwendet werden; `OWASP SAMM` soll für langlebige Projekte/Workspaces in Verbesserungspläne einfließen.
- `OWASP Cheat Sheet Series`, `OWASP Proactive Controls` und bei öffentlichen OSS-Repositories oder kritischen Abhängigkeiten `OpenSSF Scorecard` sind als ergänzende Referenzen zu berücksichtigen.
- Nichtanwendbarkeit immer als `N/A` mit kurzer Begründung dokumentieren; keine stillschweigende Auslassung.

*At the start of every Level-2 task, determine and name the applicable security standards from `constitution.md`, Principles XIV-XVIII. `NIST SSDF` and `CWE Top 25` always apply. `OWASP ASVS` applies to web/API/HTTP/auth-bearing services; `SBOM` applies to releasable or distributable artefacts; `VEX` applies when known vulnerabilities in shipped/evaluated components need a disposition statement. `SLSA` is the target model for CI/CD and published artefacts; `Zero Trust` must be explicitly evaluated for distributed, service-based, cloud, or remotely managed systems. `CAPEC`, `OWASP SAMM`, `OWASP Cheat Sheet Series`, `OWASP Proactive Controls`, and `OpenSSF Scorecard` are supporting references where relevant. Record non-applicability as `N/A` with justification rather than omitting it silently.*

## Agentischer Security-Workflow / Agentic Security Workflow

- In `spec.md`, `plan.md` und `tasks.md` die anwendbaren Standards samt Evidenzpfad festhalten.
- Bei Bedrohungsmodellen `STRIDE` als Basis und bei risikoreichen Flows zusätzlich relevante `CAPEC`-Patterns verwenden.
- Bei Web/API-Features den `ASVS`-Level und den Verifikationsumfang in `docs/security/` oder gleichwertiger Projektdokumentation ablegen.
- Bei Release-/Artefakt-Arbeit `SBOM`, `VEX`, Provenance/SLSA-Nachweise und gegebenenfalls `OpenSSF Scorecard` in Release- oder Sicherheitsdokumentation einplanen.
- Bei Architekturänderungen `Zero Trust`-Anwendbarkeit und bei langlebigen Projekten `SAMM`-Folgeaktionen prüfen.
- Default-Evidenzpfad: `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, `docs/security/samm-assessment.md`; Abweichungen nur mit lokal dokumentierter Begründung.

*Capture the applicable standards and the evidence path in `spec.md`, `plan.md`, and `tasks.md`. Use `STRIDE` as the base for threat modeling and add relevant `CAPEC` patterns for the highest-risk flows. For web/API work, record the chosen `ASVS` level and verification scope in `docs/security/` or equivalent project documentation. For release and artefact work, plan `SBOM`, `VEX`, provenance/SLSA evidence, and `OpenSSF Scorecard` review where applicable. For architectural changes, evaluate `Zero Trust`; for long-lived projects, consider `OWASP SAMM` follow-up actions. The default evidence path is `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`, unless the repository documents a justified equivalent location.*

## Spec-Kit-Preset-Pflege / Spec Kit Preset Maintenance

- Standard-Preset-Set: `security-governance` v0.2.0 prio 10, `architecture-governance` v0.2.0 prio 20, `isaqb-architecture-governance` v0.1.0 prio 30, `a11y-governance` v0.2.0 prio 40, `cross-platform-governance` v0.1.0 prio 50, `agent-parity-governance` v0.1.0 prio 60.
- Alle sechs Presets sind seit 2026-05-04 im `github/spec-kit` Community-Katalog enthalten und liegen zusätzlich als veröffentlichte Repos unter `https://github.com/hindermath/spec-kit-preset-*`.
- Neue Level-2-Projekte SOLLEN bei der Spec-Kit-Initialisierung die passende Preset-Teilmenge installieren; C#/.NET-Level-2-Projekte verwenden standardmäßig alle sechs Presets, sofern keine begründete Ausnahme dokumentiert ist.
- Referenz-Rollout für alle sechs Presets: `RiderProjects/TinyPl0`, `RiderProjects/TinyCalc`, `RiderProjects/TuiVision`, `RiderProjects/InventarWorkerService`.
- Installation bevorzugt über den Community-Katalog, wenn `specify` das unterstützt; für reproduzierbare Pins die versionierten GitHub-ZIP-URLs aus `constitution.md`/`README.md` verwenden.
- `.specify/presets/` und generierte Agenten-/Command-Dateien committen, wenn Presets Projekt-Policy sind; `.specify/presets/.cache/` nie committen.
- Nach Installation oder Update prüfen: `specify preset list`, mindestens ein `specify preset info <id>`, bei Template-Fragen zusätzlich `specify preset resolve <template>`.
- Die lokale Arbeitskopie der veröffentlichten Preset-Repos liegt unter `~/SpecKitPresetProjects/`; kanonische Scaffolds liegen im Level-0-Repo unter `specs/spec-kit-presets/` und `specs/spec-kit-preset-repos/`.
- Verbesserungen an Presets zuerst im Level-0-Scaffold einarbeiten, dann in die passenden Repos unter `~/SpecKitPresetProjects/` übertragen, committen, pushen und mit GitHub-ZIP-URL smoke-testen.
- Bei Änderungen an Preset-Regeln immer prüfen, ob `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und relevante Templates ebenfalls aktualisiert werden müssen.
- Community-/Katalog-Abstimmung läuft über `github/spec-kit#2362`.

*Standard preset set: `security-governance` v0.2.0 prio 10, `architecture-governance` v0.2.0 prio 20, `isaqb-architecture-governance` v0.1.0 prio 30, `a11y-governance` v0.2.0 prio 40, `cross-platform-governance` v0.1.0 prio 50, and `agent-parity-governance` v0.1.0 prio 60. All six presets are in the `github/spec-kit` community catalog as of 2026-05-04 and are also published under `https://github.com/hindermath/spec-kit-preset-*`. New Level-2 projects should install the applicable subset; C#/.NET Level-2 projects default to all six unless a justified exception is documented. Commit `.specify/presets/` and generated agent command updates when presets are project policy, but never commit `.specify/presets/.cache/`. Verify installs with `specify preset list`, `specify preset info`, and where relevant `specify preset resolve`. Preset-rule changes require reviewing constitution, all agent guidance files, and relevant templates. Community/catalog coordination happens in `github/spec-kit#2362`.*

## Hinweise / Notes

- Diese Datei bleibt bewusst kompakt und ergänzt die projektspezifische Dokumentation.
- This file intentionally stays compact and complements the project-specific documentation.

<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->
