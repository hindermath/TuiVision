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

## Shared Parent Guidance

*   Die gemeinsamen Dateien `/Users/thorstenhindermann/RiderProjects/AGENTS.md` und `/Users/thorstenhindermann/RiderProjects/GEMINI.md` speichern die repo-uebergreifenden Basisregeln.
*   Diese Projekt-Datei ist die spezifischere Autoritaet fuer projektspezifische Build-Befehle, Workflows, Architektur und Features.

---

## Hinweise / Notes

- Diese Datei bleibt bewusst kompakt und ergänzt die projektspezifische Dokumentation.
- This file intentionally stays compact and complements the project-specific documentation.

<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->
