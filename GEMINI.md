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
    *   GitHub Pages wird aus `.github/workflows/pages.yml` veroeffentlicht: root-`docfx.json` bauen, `tests/web-a11y/` mit Playwright plus axe ausfuehren, `_site/` als Pages-Artefakt hochladen und `_site/` sowie generierte `api/*.yml`-Dateien aus Git heraushalten.
    *   Wichtige Aussagen duerfen nicht nur ueber Farbe, Layout oder Mauszeiger-Hinweise transportiert werden; bevorzugt werden semantische Ueberschriften, Listen, Tabellen und ASCII-/Textdiagramme.
    *   Bilinguale CEFR-B2-Lieferung und der dokumentierte A11Y-Nachweis gehoeren zur formalen Abschlusspruefung fuer lernrelevante Doku und aktive Anforderungsartefakte.
    *   Vollständige XML-Kommentare für alle öffentlichen APIs (`summary`, `param`, `returns`, `remarks`).
    *   Didaktischer Stil: Erklärt das *Warum* und bietet Beispiele für Lernende.
    *   Neue oder geaenderte nicht-triviale Logik muss auf didaktischen Inline-Kommentarbedarf geprueft werden, wenn Lernverstaendnis oder Wartbarkeit betroffen sind, besonders bei zentralen Framework-Flows und Smoke-Test-Helfern.
    *   Inline-Kommentare erklaeren Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze; sie wiederholen nicht den offensichtlichen Code.
    *   Die Kommentarintensitaet bleibt moderat: normalerweise 1 bis 3 Zeilen vor einem nicht-trivialen Block, bei didaktischen Erklaerbloecken Deutsch zuerst und Englisch danach auf CEFR-B2-Niveau.
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

## Example Wave Delivery Pattern

*   Groessere verpflichtende Beispielwellen SOLLEN als bewusstes zweistufiges Spec-Kit-Liefermuster geplant werden, wenn Portierungslogik, Framework-Luecken und interaktive Runtime-Politur sonst in einem Feature vermischt wuerden.
*   Stufe 1 ist der funktionale Portierungs- und Nachweis-Feature-Lauf: historische Beispielablaeufe portieren, Framework-Voraussetzungen schliessen, deterministische Headless- oder In-Process-Smoke-Pfade bereitstellen, Guides/Evidence ergaenzen und interaktive Runtime-Politur explizit als Follow-up markieren, wenn `dotnet run --project examples/<Name>` noch nicht die finale Demo zeigt.
*   Stufe 2 ist der interaktive Showcase-Feature-Lauf: die bewiesenen Funktionen ueber sichtbare Menues, Statuszeilen, Desktop-Controls, Dialoge, Tastaturpfade und skriptbare UI-Event-Smoke-Tests erreichbar machen.
*   Eine Beispielwelle gilt erst nach Stufe 2 als vollstaendig lern- und reviewtauglich, sofern der Scope nicht ausdruecklich nur einen minimalen nicht-interaktiven Nachweis verlangt.
*   Kuenftige Lastenhefte fuer Beispielwellen oder beispielnahe Framework-Vorhaertungen MUESSEN ein Framework-Usage- und Remediation-Gate enthalten. Der spaetere Spec-Kit-Lauf muss pro Beispiel oder Vertragsbereich dokumentieren, welche bestehende TuiVision-Framework-Komponente genutzt wird, ob lokale Sonderlogik existiert und welche Entscheidung gilt: `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation` oder `FollowUpHardening`.
*   Wiederverwendbare Logik darf nicht dauerhaft als lokale `examples/`-Sonderloesung verbleiben. Wenn dieselbe lokale Logik in mehreren Beispielen nuetzlich waere oder Framework-Verhalten ersetzt, gehoert sie in einen kleinen Framework-Fix oder in ein eigenes Follow-up-Hardening.

## Historical Source Reference Policy

*   Fuer jede Spec-Kit-Feature-Implementierung, die historisch abgeleitetes Turbo-Vision-Verhalten portiert, erweitert, testet, dokumentiert oder korrigiert, muessen die relevanten historischen Implementierungsdateien unter `tv203s/` als Read-only-Referenz geprueft werden. Das betrifft mindestens passende `.c`- und `.cc`-Dateien.
*   Falls Typen, Konstanten, Makros, Datenlayout, Vererbung, Funktionssignaturen oder Kontext nur ueber Deklarationen klar werden, muessen auch die relevanten C/C++-Header wie `.h`, `.hpp` oder `.hh` geprueft werden.
*   `tv203s/` wird nicht veraendert. Die C#-Portierung bleibt eine moderne, idiomatische Umsetzung der historischen Absicht und keine mechanische 1:1-Uebersetzung.
*   `spec.md`, `plan.md`, `tasks.md`, Guides, PR-Evidence oder Architektur-/Security-Nachweise muessen bei relevanten Features festhalten, welcher historische Zweck uebernommen wird und welche wesentlichen nutzer- oder API-sichtbaren Abweichungen bewusst sind.
*   Wenn ein Feature keinen historischen `tv203s`-Bezug hat, genuegt ein kurzes `N/A` mit Begruendung in Plan, Aufgaben oder Evidence.

*   For every Spec-Kit feature implementation that ports, extends, tests, documents, or fixes historically derived Turbo Vision behavior, review the relevant historical implementation files under `tv203s/` as read-only reference. This includes at least the matching `.c` and `.cc` files.
*   When types, constants, macros, data layout, inheritance, function signatures, or context are only clear from declarations, also review the relevant C/C++ headers such as `.h`, `.hpp`, or `.hh`.
*   Do not modify `tv203s/`. The C# port remains a modern idiomatic implementation of the historical intent, not a mechanical line-by-line translation.
*   For relevant features, `spec.md`, `plan.md`, `tasks.md`, guides, PR evidence, or architecture/security evidence must record which historical intent is followed and which material user-visible or API-visible deviations are intentional.
*   If a feature has no historical `tv203s` relevance, a short `N/A` rationale in plan, tasks, or evidence is sufficient.

## Active Feature Context

### 012-interactive-wave2-demos
*   Current implementation status: interactive Wave-2 demo polish is implemented on branch `012-interactive-wave2-demos`; final validation evidence is tracked in `specs/012-interactive-wave2-demos/pr-evidence.md`.
*   Delivered scope is limited to making the eleven Wave-2 examples visibly operable at normal runtime: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`; matching event-loop smoke tests in `tests/TuiVision.Examples.SmokeTests/`; guides, README, `pr-evidence.md`, and proportional architecture/security/A11Y/statistics evidence.
*   Before wiring or accepting each example, review the relevant historical `.c`/`.cc` source and any important matching headers under `tv203s/` as read-only reference, capture the original demo intent, and document intentional user-visible deviations in guide or PR evidence.
*   Every example must show first-screen purpose text, expose primary behavior through menu, keyboard, status, or command paths, and update visible text-first feedback after each demonstrated operation.
*   Primary smoke proof must run `app.Run()` or the equivalent real application loop with injected `TEvent`, command, or key events. Direct helper methods may support setup or supplemental assertions only.
*   `examples/Demo` is the P1 vertical slice and must prove at least three visible behaviors before the pattern is spread across the rest of the examples.
*   File/path and dialog-designer flows stay read-only toward user data: use source-controlled fixtures, fixed repository paths, or test temporary directories; do not read arbitrary user file contents or persist user history as proof.
*   Wave 3 and Wave 4 examples, mandatory mouse-only operation, broad framework redesign, new runtime dependencies, databases, external services, and DocFX publishing-model changes are out of scope.
*   Next open mandatory example scope after this feature is Wave 3: editor, file, help, and stream demos such as `tvedit`, `bhelp`, and `helpdemo`.

### 013-wave2-visual-component-remediation
*   Current implementation status: visual component remediation is implemented on branch `013-wave2-visual-component-remediation`; final validation evidence is tracked in `specs/013-wave2-visual-component-remediation/pr-evidence.md`.
*   Scope is limited to the eleven Wave-2 examples: `Clipboard`, `Demo`, `DlgDsn`, `DynTxt`, `InpLis`, `ListVi`, `ProgBa`, `Sdlg`, `Sdlg2`, `TCombo`, and `TProgB`.
*   Shared runtime support for the remediated examples lives in `examples/Shared/Wave2Runtime.cs` and is linked into the eleven scoped example projects.
*   The primary parity proof is the visible UI composition itself: controls, dialogs, windows, view groups, scroll groups, progress displays, input/list/combo composition, or another stable visible runtime state.
*   Each example must use the three-layer model: visible main component, real `TStatusLine` feedback, and keyboard-reachable `Help -> Description`.
*   Primary smokes must drive the real app loop and combine concrete state assertions with view-tree proof plus buffer/cell rendered visibility proof at expected positions or regions.
*   Historical C/C++ sources under `tv203s/` remain read-only intent references; intentional user-visible deviations must be documented in plan, tasks, guides, feature evidence, or PR evidence.
*   AI-SBOM is `N/A` for this feature while AI is only development/agent tooling; re-evaluate if runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope.
*   Wave 3/4 functionality, broad framework redesign, mandatory mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, and new runtime dependencies are out of scope.

### 014-wave1-functional-hardening
*   Current implementation status: Wave-1 functional hardening is implemented on branch `014-wave1-functional-hardening`; final validation evidence is tracked in `specs/014-wave1-functional-hardening/pr-evidence.md`.
*   Scope is limited to `Desklogo`, `MsgCls`, `Tutorial` steps `tvguid01` through `tvguid16`, and `Videomode`.
*   The primary proof surface is `specs/014-wave1-functional-hardening/pr-evidence.md`, which records historical source, C# behavior, proof method, helper classification, negative/fallback proof, missing-core decisions, intentional deviations, validation, and the archived Lastenheft path.
*   Managed runtime behavior is proven by executable smoke tests; evidence-only proof remains allowed only for explicitly documented no-runtime-target deviations.
*   Helper or headless paths may be `PrimaryProof` only when they execute real example or application logic through public commands, events, application methods, or stable public state with concrete assertions; 014 added `PrimaryProof`, `SupplementalProof`, `SetupOnly`, and `LegacyOrTemporary` helper taxonomy.
*   Historical C/C++ sources under `tv203s/` remain read-only intent references. `set-logo.cc` and `tv_logo.cc` are Desklogo asset/generator boundary context only.
*   Validation baseline: targeted Wave-1 smokes 38/38 passed, full example smokes 91/91 passed, full Release tests 496/496 passed, coverage gate exceeded 70% for all required assemblies, `docfx docfx.json` passed with 0 warnings/errors, and Playwright/axe DocFX smoke passed 2/2 via explicit local-server workaround when sandboxed webserver startup is blocked.
*   Feature 015 completed the didactic comment pass; the next open cross-cutting step is `Lastenheft_Secure-Development-Hardening.md` before Wave-1 visual remediation.
*   Wave-1 visual remediation, Wave 2/3/4 behavior, broad framework redesign, mouse-only operation, arbitrary user-file proof, external proof paths, persistent user history, databases, external services, new runtime dependencies, and runtime/product AI are out of scope.

### 015-didactic-comment-hardening
*   Current implementation status: the selective didactic inline-code-comment hardening is implemented; final evidence is in `specs/015-didactic-comment-hardening/pr-evidence.md`.
*   Scope is limited to selective didactic inline, block, file, or module comments, feature evidence in `specs/015-didactic-comment-hardening/pr-evidence.md`, and affected guidance/evidence surfaces.
*   Review must cover central framework flows and relevant smoke-test helpers: event/command/dispatch, focus transitions, view hierarchy, StatusLine, Help/Description, dialog state, validation/rejection, buffer/cell proof, rendering snapshots, terminal fallbacks, historical Turbo Vision deviations, and proof helpers.
*   Each reviewed area must receive exactly one decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.
*   Comments remain moderate and reason-focused: explain why, trade-off, constraint, historical deviation, or proof boundary; avoid restating obvious code; use German-first/English-second CEFR-B2 text for didactic explanation blocks.
*   XML comments remain the API/DocFX surface; pure `//` or `/* */` hardening does not trigger DocFX. XML/API/generated docs/navigation/guides changes trigger the normal DocFX plus A11Y path.
*   No runtime behavior change, API change, new dependency, new example porting, broad framework revision, Wave-1 visual remediation, or runtime/product AI is in scope.

### 016-secure-development-hardening
*   Current implementation status: the project-wide secure-development hardening is implemented; final evidence is in `specs/016-secure-development-hardening/pr-evidence.md`.
*   The durable control matrix covers all 157 `CL-XX-NN` controls with `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` and complete ownership, risk, evidence, and re-evaluation fields.
*   Delivered remediation is bounded to explicit malformed persistence rejection, immutable workflow dependencies, supply-chain automation, root disclosure guidance, and safe Bash/PowerShell archive-script parity.
*   Local acceptance is 498/498 Release tests plus line coverage above 70% for Core, Controls, Serialization, Compatibility, and Drivers.Console; DocFX/axe and remote OS/CI proof remain delivery gates.
*   Human legal, provider, organization, and agent-platform decisions remain `Open`; release provenance, reproducible-build/lock maturity, and RFC 9116 remain named follow-ups rather than implicit claims.
*   The next open prioritized intake is `Lastenheft_03_EditorHelpAndResourcesHardening.md` before Wave-3 visual porting.

### 017-wave1-visual-component-remediation
*   Current implementation status: Wave-1 visual component remediation is implemented; final evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.
*   `Desklogo`, `MsgCls`, all 16 Tutorial tokens, and `Videomode` now use the three-layer model: visible main state, real `TStatusLine`, and keyboard-reachable `Help -> Description`.
*   Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, and rendered buffer/cell evidence. The acceptance matrix contains four app rows plus 16 unique Tutorial rows; direct helpers are not primary proof.
*   Shared composition in `examples/Shared/Wave1Runtime.cs` uses existing framework controls. Desklogo and MsgCls are `UseExistingFramework`; Tutorial and Videomode are bounded `IntentionalDeviation` decisions.
*   Historical sources under `tv203s/` remain read-only. No functional re-port, Wave-2/3/4 behavior, broad framework redesign, new dependency, persistence, external service, or runtime/product AI entered scope.
*   The complete example-smoke suite passes 101/101 locally; final repository, coverage, DocFX, A11Y, and remote checks remain delivery gates until recorded in feature evidence.
*   Feature 018 closes the editor/help/resources intake; the next prioritized intake is `Lastenheft_Wave3-Visual-Component-Porting.md`.

### 018-editor-help-resources-hardening
*   Current implementation status: Wave-3 editor/help/resources hardening is implemented; final evidence is in `specs/018-editor-help-resources-hardening/pr-evidence.md`.
*   Existing `TEditor`, `TFileEditor`, `TEditWindow`, `THelpViewer`, and `THelpWindow` flows are retained and proven as coherent open/edit/search/replace/save, safe-close/conflict, persisted navigation, back, and fallback paths.
*   `THelpSourceCompiler` provides a bounded `.topic` and `{text[:alias]}` source contract with strict UTF-8, deterministic contexts, forward-reference resolution, stable diagnostics, and atomic no-partial-model failure.
*   `TLocalizedResourceLookup` uses explicit exact-language, caller-ordered fallback, then neutral keys over case-sensitive `TResourceFile` storage without ambient locale, gettext, codepage, or new dependency scope.
*   Persisted resources reject duplicate keys and negative payload lengths; persisted help rejects negative counts and unresolved or invalid reference ranges before presentation.
*   Framework decisions are `UseExistingFramework` for editor/file, and bounded `SmallFrameworkFix` for help graph validation, compiler, resources, and i18n. Historical sources remain read-only.
*   Wave-3 examples, mouse, terminal/charset/font work, TP7, broad redesign, services, and dependencies remain outside 018.
*   The next prioritized intake is `Lastenheft_Wave3-Visual-Component-Porting.md` for `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc`.

### 019-wave3-visual-component-porting
- Current implementation status: Wave-3 visual component porting is implemented locally; final evidence is in `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- `BHelp`, `HelpDemo`, `I18n`, `TvEdit`, and `TvHc` use visible main components, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
- Primary proof runs through `app.Run()` and combines concrete state, view-tree identity, rendered buffer/cell evidence, status, and description. The Wave-3 matrix passes 14/14 locally, including five constrained `48x16` layouts.
- Framework decisions are `UseExistingFramework` for TvEdit, HelpDemo, I18n, and TvHc, and bounded `IntentionalDeviation` for BHelp because the proprietary unchecked Borland `.tch` decoder is omitted.
- Embedded/source-controlled learning content and test-owned temporary paths are the only data boundaries. Historical sources remain read-only.
- Mouse interaction, terminal/charset/font work, Wave 4, broad redesign, services, new dependencies, and runtime/product AI remain outside 019.
- The next prioritized intake is `Lastenheft_04_MouseSupportAndInteraction.md`.

### 020-mouse-support-interaction
- Current implementation status: bounded mouse support and interaction hardening is implemented locally; final evidence is in `specs/020-mouse-support-interaction/pr-evidence.md`.
- `ConsoleMouseIngress` accepts only complete bounded SGR 1006 left press, pressed move, and release reports and publishes zero or one existing `TEvent`; malformed syntax, range, button, capability, and phase input is rejected atomically.
- `TGroup` routes mouse down to one topmost visible target, transfers focus only to selectable targets, and preserves existing exactly-once control commands. Nested mouse coordinates traverse the full owner chain.
- The only mouse drag contract is moving a `TWindow` from its title row. Owner bounds, release, Escape, capability loss, disable, removal, shutdown, and the existing `Ctrl+F5` keyboard fallback are proven.
- Interactive macOS/Linux terminals and WSL use the SGR capability contract; native Windows Console and redirected/headless I/O remain honest `Unsupported` boundaries. Wheel, hover, touch, extra buttons, full protocol parity, and additional drag targets remain out of scope.
- Primary proof runs through `TProgram.GetEvent` and `app.Run()` and combines concrete focus/command/drag state, target identity, visible text, and rendered buffer/cell assertions. Historical sources remain read-only.
- The next prioritized intake is `Lastenheft_05_TerminalCharsetAndEmulation.md`.


### Autonomous Red-Proof Completeness
- Before the first red test batch, review imports, public XML docs, harness helpers, focus/ownership assertions, and linked-source assembly identity.
- Group independent negative cases only as a bounded project-local red matrix with explicit failure boundaries and shared ownership.
- When source is linked into multiple assemblies, cross-project proof uses public contracts or state delegates and does not assume one CLR type identity.

## 🔄 Synchronisationsregel für KI-Agenten-Dateien

*   Wenn sich aktiver Feature-Kontext, Planungsstand oder gemeinsam genutzte Agenten-Hinweise ändern, müssen diese Dateien gemeinsam geprüft und bei Bedarf im selben Arbeitsgang aktualisiert werden:
    *   `AGENTS.md`
    *   `CLAUDE.md`
    *   `GEMINI.md`
    *   `.github/copilot-instructions.md`
    *   `.github/agents/copilot-instructions.md`
*   Eine nur teilweise Synchronisierung ist nicht zulässig, wenn sich gemeinsame Vorgaben geändert haben.
*   Falls eine Datei absichtlich agentenspezifisch abweicht, muss diese Abweichung im selben Change ausdrücklich dokumentiert werden.

## Agentische Skriptausfuehrung / Agentic Script Execution

- Vor jeder Automationsaufgabe zuerst das Betriebssystem pruefen. Wenn ein passendes PowerShell-7-Skript oder Cmdlet vorhanden ist und `pwsh` verfuegbar ist, diese Variante bevorzugen. Fuer strukturierte lokale Automationen ist C# ueber `.NET` oder `mono` ein zulaessiger zweiter Weg, wenn Typisierung, Dateiformate oder Wiederverwendbarkeit dadurch klar besser werden. Erst wenn PowerShell oder C# nicht sinnvoll passen, die OS-nahe vorhandene Repo-Variante nutzen, auf macOS/Linux typischerweise Bash. Keine neue Sprache nur aus Bequemlichkeit einfuehren, wenn ein bestehendes Repo-Skript denselben Zweck erfuellt.
- Detect the operating system before each automation task. If a matching PowerShell 7 script or cmdlet exists and `pwsh` is available, prefer that variant. For structured local automation, C# via `.NET` or `mono` is an acceptable second option when type safety, file formats, or reuse clearly benefit from it. Only when PowerShell or C# is not a good fit, use the existing OS-native repository variant, typically Bash on macOS/Linux. Do not introduce a new language merely for convenience when an existing repository script already solves the task.

## 📊 Projektstatistik

*   `docs/project-statistics.md` ist das fortlaufende Statistik-Register des Repositories.
*   Die Datei muss nach jeder abgeschlossenen Spec-Kit-Implementierungsphase, nach jeder agentischen Änderung am Repository und auf explizite Anforderung aktualisiert werden.
*   Im `## Fortschreibungsprotokoll` muessen die Tabelleneintraege strikt chronologisch stehen: der aelteste Eintrag oben, der juengste und zuletzt eingetragene Eintrag unten; Eintraege mit demselben Datum behalten ihre Eintragungsreihenfolge.
*   Als letzter Top-Level-Block der Datei muss immer ein `## Gesamtstatistik`-Abschnitt stehen; danach darf kein weiterer Top-Level-Abschnitt folgen.
*   Innerhalb dieses finalen `## Gesamtstatistik`-Abschnitts muessen kompakte ASCII-only-Diagramme direkt unter der textlichen Gesamtauswertung mitgefuehrt werden; sie sollen mindestens Artefaktmix, die dokumentierten Branch-/Phasenverlaeufe, die dokumentierten Beschleunigungsfaktoren durch agentische KI plus Spec-Kit/SDD und einen direkten Vergleich zwischen erfahrener Entwickler-Referenz, Thorsten-Solo-Referenz und sichtbarem KI-Lieferfenster zeigen und bei jeder Statistikpflege mitaktualisiert werden.
*   Jeder kurze Erklaertext in CEFR-B2-Sprache muss direkt bei seiner ASCII-Diagrammgruppe stehen, idealerweise unmittelbar davor oder danach, damit Auszubildende nicht zwischen Erklaerung und Diagramm scrollen muessen.
*   Wenn Daten entlang einer X-Achse als Verlauf besser lesbar werden, sollen zusaetzlich einfache ASCII-X/Y-Diagramme eingefuegt werden. Diese muessen bewusst grob, in reinem Markdown lesbar und ebenfalls in CEFR-B2 erklaert bleiben.
*   ASCII-X/Y-Diagramme muessen feste X-Slots verwenden: jede dokumentierte Phase behaelt ihren Slot, fehlende Werte bleiben leer, und zu breite kuenftige Reihen werden in beschriftete Bloecke wie `0..15`, `16..31` und `32..47` geteilt, jeweils mit eigener Achsenlinie und eigenen X-Labels.
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
*Hinweis: Dieses Dokument wurde automatisch von Antigravity CLI generiert und dient als Instruktionsbasis.*


## Gemeinsame Governance-Ergaenzung / Shared Governance Addendum

- Alle nutzerseitigen Artefakte muessen barrierefrei gedacht und geprueft werden: CLI-Ausgaben, Dokumentation, HTML, UI und generierte Templates; WCAG 2.2 Level AA ist die Standard-Basis, sobald die Kriterien auf das Artefakt anwendbar sind.
- All user-facing artefacts must be designed and reviewed for accessibility: CLI output, documentation, HTML, UI, and generated templates; WCAG 2.2 Level AA is the default baseline wherever the criteria apply.

- Fuer C#/.NET-Repositories gilt standardmaessig eine Thorsten-Solo-Basis von `125` Zeilen/Arbeitstag, sofern das Repo keinen abweichenden, begruendeten Wert dokumentiert.
- The default Thorsten-solo baseline for C#/.NET repositories is `125` lines/workday unless the repository documents a justified deviation.

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, MSTest, Coverlet, docfx (004-editor-file-help-streams)
- Lokales Dateisystem sowie persistente binaere Help-/Ressourcen-Streams; keine Datenbank in diesem Inkrement (004-editor-file-help-streams)
- C# `latest` on .NET 10 (`net10.0`) + Existing modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; docfx for API documentation validation; GitHub Actions for existing CI (005-driver-consolidation-m07)
- Versionskontrollierter Markdown-Nachweis in `docs/porting-status.md`; keine Datenbank; Kompatibilitaetsnachweise duerfen als Repo-Notizen oder dokumentierte Kommandoausgaben vorliegen (005-driver-consolidation-m07)
- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`, bestehende MSTest-Suiten plus ggf. notwendige Compatibility-spezifische Validierung, Coverlet-Nachweise, `dotnet format`, docfx, `Pflichtenheft.md` und `docs/porting-status.md` fuer den formalen Phase-8-Gate-Nachweis (006-close-phase8-gate)
- Reine Repository-Nachweisartefakte; keine Datenbank und keine Beispielanwendungs-Auslieferung in diesem Inkrement (006-close-phase8-gate)
- Source-controlled example projects under `examples/`; wave-1 examples (`desklogo`, `msgcls`, `tutorial`, `videomode`) delivered; 41 smoke tests green; next: Wave 2 Controls and Dialogs (007-port-wave1-examples)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Bestehendes `TuiVision.Core`-Geometrie-/Event-/Buffer-Fundament; bestehende `TuiVision.Controls`-Shell- und Widget-Basis aus `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`, `TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, `TParamText`, editor-orientiertes `TIndicator` nur als Kontrastfall); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; optional `docfx docfx.json`; GitHub Actions plus vorhandene Example-Smoke-Infrastruktur fuer die nachgelagerte Wave-2-Readiness (009-controls-widgets-and-collections)
- In-Memory-UI-Zustand in Produktion; versionskontrollierte Planungs-, Test-, Nachweis- und bereits gelieferte Example-Artefakte in `specs/`, `tests/`, `docs/` und `examples/`; keine Datenbank oder externer Persistenzdienst (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing framework modules `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; new wave-2 example projects under `examples/`; existing `tests/TuiVision.Examples.SmokeTests/`; MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (011-port-wave2-examples)
- Runtime example state is in memory; standard-dialog file flows use real local file-system metadata only; `dlgdsn` may use source-controlled dialog-description fixtures through existing Serialization/resource primitives; no database, external service, persisted user history, or new dependency planned (011-port-wave2-examples)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet test stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (012-interactive-wave2-demos)
- Runtime example state is in memory. Dialog-designer and file/path demonstrations use source-controlled fixtures, fixed repository paths, or test temporary directories. The examples must not persist user history, write user data as part of normal demonstration, read arbitrary user file contents as proof, or add a database/external service. (012-interactive-wave2-demos)
- Runtime example state remains in memory. Controlled examples may use source-controlled fixtures, fixed repository paths, or test temporary directories for metadata, rendering, validation, or rejection proof. The feature must not add a database, external service, network dependency, persistent user history, or arbitrary user-file content reads. (013-wave2-visual-component-remediation)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest and Coverlet stack; existing DocFX plus Playwright/axe web A11Y tooling. No new runtime NuGet dependency is planned. (014-wave1-functional-hardening)
- Runtime example state remains in memory. Proof data is limited to existing source-controlled files, controlled example fixtures if needed, or test temporary directories. No database, external service, network dependency, persistent user history, arbitrary user-file content reads, or runtime/product AI storage is planned. (014-wave1-functional-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + Existing TuiVision modules only: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility`, `TuiVision.Drivers.Console`; existing MSTest/Coverlet validation; existing DocFX plus Playwright/axe web A11Y tooling when documentation triggers apply. No new runtime NuGet dependency is planned. (015-didactic-comment-hardening)
- Source-controlled Markdown evidence and guidance files only. Production code state and tests keep their current storage model. No database, external service, network dependency, persistent user history, runtime/product AI storage, or arbitrary user-file proof path is planned. (015-didactic-comment-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling + Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned. (016-secure-development-hardening)
- Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced. (016-secure-development-hardening)
- C# `latest` / C# 14 on .NET 10 (`net10.0`) + existing TuiVision modules, shared Wave-1 example composition, MSTest/Coverlet, DocFX, and Playwright/axe; no new package (017-wave1-visual-component-remediation)
- Runtime example state remains in-process and session-only; proof and governance use source-controlled Markdown, with no database, external service, persistent user history, arbitrary user-file proof, or runtime/product AI (017-wave1-visual-component-remediation)
- C# 14 on .NET 10 + Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages (019-wave3-visual-component-porting)
- Embedded/source-controlled learning content and test-owned temporary files only (019-wave3-visual-component-porting)

### 007-port-wave1-examples
- Current status: Wave 1 delivered (2026-03-28). `desklogo`, `msgcls`, `tutorial` (16 steps), `videomode` are ported, smoke-tested, and guide-documented.
- Wave 1 scope: `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`; shared smoke-test infrastructure in `tests/TuiVision.Examples.SmokeTests/`; guides in `docs/guides/examples/`.
- Next open scope: Wave 2 – Controls and Dialogs (requires Controls/Dialog layer as prerequisite before planning starts).
- Planning decisions now fixed: headless smoke seam via `bool headless` constructor parameter + `GetEvent()` override; in-process MSTest execution without external process spawning; bilingual German-first/English-second XML docs and comments at CEFR-B2; `DisplayModeCoordinator.ProbeResizeSupport()` cross-platform probe with CA1416 suppressed.

## Recent Changes
- 014-wave1-functional-hardening: Planartefakte fuer Wave-1 Functional Hardening ergaenzt, inklusive historischer Proof-Matrix, Smoke-Proof, Helper-Klassifikation, Fallback-, Missing-Core-, Dokumentations- und Governance-Planung.
- 013-wave2-visual-component-remediation: Sichtbare Hauptkomponenten oder stabile visuelle Runtime-Zustaende, echte `TStatusLine`, `Help -> Description`, gemeinsames `examples/Shared/Wave2Runtime.cs`, strengere app-loop-basierte Render-Smokes, Guides, README, Architektur-/Security-Evidence, Statistik und PR-Evidence fuer alle elf Wave-2-Beispiele umgesetzt.
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
- 012-interactive-wave2-demos: Interaktive Showcase-Stufe fuer Welle 2 implementiert: alle elf Wave-2-Beispiele besitzen sichtbare normale Runtime-Pfade, app-loop-basierte Smoke-Nachweise, aktualisierte Guides, README-, Architektur-/Security-/A11Y- und PR-Evidence.

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

- KI-generierter und menschlich geschriebener Code MUSS den etablierten Secure-Coding-Best-Practices der Zielsprache und des Frameworks folgen. LLMs erzeugen nicht zuverlässig sicheren Code; explizite Durchsetzung ist erforderlich.
- Verbindliche Regeln und sprachspezifische Anforderungen: siehe `constitution.md`, Prinzip XII.
- Sprachspezifische Kurzregeln (Detailprofil: `.specify/templates/secure-coding-language-rules-template.md`):
  - **C / C89**: Bounds-Checking, kein `gets()`, kein ungeprueftes `sprintf()`/`strcpy()`, CERT C.
  - **C# / .NET**: parametrisierte Queries, Output-Encoding gegen XSS, Anti-Forgery-Tokens, sichere Deserialisierung, Microsoft Secure Coding Guidelines.
  - **Rust**: `unsafe` isolieren und begruenden, keine Panic-Pfade aus nicht vertrauenswuerdigem Input, Deserialisierung validieren, `cargo audit` oder gleichwertig verwenden.
  - **Go**: HTTP-/Client-Timeouts setzen, `context` propagieren, SSRF pruefen, `crypto/rand` nutzen, `govulncheck` oder gleichwertig verwenden.
  - **Swift**: keine Force-Unwraps auf nicht vertrauenswuerdigen Daten, dekodierte Eingaben validieren, Keychain/CryptoKit/TLS-Defaults nutzen, Datei-URLs einschraenken.
  - **Java / Kotlin**: DTOs validieren, Persistence-Zugriffe parametrisieren, Deserialisierung beschraenken, Auth/CSRF/CORS/Session-Defaults pruefen.
  - **Python**: Boundary-Input validieren, keine unsichere Deserialisierung oder dynamische Ausfuehrung, `subprocess`/Dateipfade einschraenken, Dependency-Audit nutzen.
  - **TypeScript / JavaScript**: Runtime-Input validieren, XSS/Prototype-Pollution/SSRF pruefen, keine dynamische Code-Ausfuehrung, Lockfiles auditieren.
  - **SQL**: nur parametrisierte Statements, kein dynamisches SQL aus nicht vertrauenswuerdigem Input.
  - **Bash**: Variable in Anfuehrungszeichen (`"$var"`), kein `eval` auf nicht vertrauenswuerdigem Input, `--` End-of-Options.
  - **PowerShell**: `Set-StrictMode -Version Latest`, validierte Parameter, kein `Invoke-Expression` auf nicht vertrauenswuerdigem Input.
- Kryptografie: aktuelle Algorithmen (AES-256, RSA >= 3072, SHA-256+, Ed25519); veraltete (MD5, SHA-1 für Signaturen, DES, RC4) nur mit expliziter Risikobegründung.
- Fehlerbehandlung darf keine internen Zustände, Stack-Traces oder Verbindungszeichenketten an Endbenutzer preisgeben.
- Hinzugefügte Abhängigkeiten müssen aktiv gepflegt sein und dürfen keine bekannten kritischen CVEs aufweisen.
- Code-Reviews MÜSSEN eine Sicherheitsperspektive für Eingabeverarbeitung, Authentifizierung, Autorisierung, Kryptografie und Datei-/Netzwerk-I/O enthalten.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated and human-written code MUST follow the secure-coding best practices of the target language and framework. Authoritative rules: `constitution.md`, Principle XII, and `.specify/templates/secure-coding-language-rules-template.md`. Language-specific short rules cover C/C89, C#/.NET, Rust, Go, Swift, Java/Kotlin, Python, TypeScript/JavaScript, SQL, Bash, and PowerShell. MSL status does not replace secure API, I/O, auth, SQL, crypto, logging, or dependency review. Cryptography: use current algorithms (AES-256, SHA-256+, Ed25519); deprecated (MD5, SHA-1 for signatures, DES, RC4) only with explicit risk acknowledgement. Error handling must not expose internals. Dependencies must have no known critical CVEs. Code reviews must include a security perspective for input handling, auth, crypto, and I/O. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
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
  - **Supply-Chain-Evidenz / Supply Chain Evidence** (`supply-chain-evidence-template.md`) — SBOM, AI-SBOM, VEX, SLSA, OpenSSF Scorecard (Prinzip XVI, releasefähige Projekte MUST; AI-SBOM nur bei KI-Runtime-/Produktkomponenten)
  - **Zero-Trust-Anwendbarkeit / Zero Trust Applicability** (`zero-trust-applicability-template.md`) — NIST SP 800-207-Bewertung (Prinzip XVIII, verteilte Systeme SHOULD)
  - **SAMM-Bewertung / SAMM Assessment** (`samm-assessment-template.md`) — OWASP SAMM Reifegrad und Verbesserungsplan (Prinzip XVIII, langlebige Projekte SHOULD)
- Projektspezifische Instanzen werden in `docs/security/` gepflegt; S-ADRs als einzelne Dateien in `docs/security/adr/`.

*Every Level-2 project MUST maintain security documents based on templates in `.specify/templates/`: threat model (STRIDE+CAPEC), S-ADRs, arc42 Section 8 security concepts, security checklist, dependency audit, security quality scenarios (SHOULD), ASVS verification (web/API MUST), supply-chain evidence (release-capable MUST; AI-SBOM when AI runtime/product components apply), Zero Trust applicability note (distributed systems SHOULD), and SAMM assessment (long-lived projects SHOULD). Project-specific instances live in `docs/security/`; S-ADRs in `docs/security/adr/`. See `constitution.md`, Principles XII–XVIII for authoritative requirements.*

## Sicherheitsstandards & Anwendbarkeit / Security Standards & Applicability

- Vor jeder Level-2-Aufgabe die anwendbaren Sicherheitsstandards aus `constitution.md`, Prinzipien XIV-XVIII bestimmen und explizit benennen.
- `NIST SSDF` und `CWE Top 25` gelten immer für Level-2-Arbeit.
- `OWASP ASVS` gilt für Web-, API-, HTTP- und authentifizierte Dienste; der gewählte ASVS-Level muss benannt werden.
- `SBOM` gilt für releasefähige oder verteilbare Artefakte; `VEX`, wenn bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten bewertet werden müssen.
- `AI-SBOM` gilt projektartabhängig bei KI-Modellen, KI-Diensten, Trainings-/Embedding-Daten, Inferenz-Infrastruktur oder KI-Runtime-Komponenten im ausgelieferten oder betriebenen System; reine Entwicklungswerkzeug-Nutzung wird als `N/A` mit Toolchain-Begründung dokumentiert.
- `SLSA` gilt als Soll-Vorgabe für CI/CD- oder veröffentlichte Artefakte; `Zero Trust` ist für verteilte, servicebasierte, cloudnahe oder remote-verwaltete Systeme explizit zu prüfen.
- `CAPEC` soll in Bedrohungsmodellen für die risikoreichsten Angriffswege verwendet werden; `OWASP SAMM` soll für langlebige Projekte/Workspaces in Verbesserungspläne einfließen.
- `OWASP Cheat Sheet Series`, `OWASP Proactive Controls` und bei öffentlichen OSS-Repositories oder kritischen Abhängigkeiten `OpenSSF Scorecard` sind als ergänzende Referenzen zu berücksichtigen.
- Nichtanwendbarkeit immer als `N/A` mit kurzer Begründung dokumentieren; keine stillschweigende Auslassung.

*At the start of every Level-2 task, determine and name the applicable security standards from `constitution.md`, Principles XIV-XVIII. `NIST SSDF` and `CWE Top 25` always apply. `OWASP ASVS` applies to web/API/HTTP/auth-bearing services; `SBOM` applies to releasable or distributable artefacts; `AI-SBOM` applies when AI models, AI services, datasets, inference infrastructure, or AI runtime components are part of the released or operated system; `VEX` applies when known vulnerabilities in shipped/evaluated components need a disposition statement. `SLSA` is the target model for CI/CD and published artefacts; `Zero Trust` must be explicitly evaluated for distributed, service-based, cloud, or remotely managed systems. `CAPEC`, `OWASP SAMM`, `OWASP Cheat Sheet Series`, `OWASP Proactive Controls`, and `OpenSSF Scorecard` are supporting references where relevant. Record non-applicability as `N/A` with justification rather than omitting it silently.*

## Agentischer Security-Workflow / Agentic Security Workflow

- In `spec.md`, `plan.md` und `tasks.md` die anwendbaren Standards samt Evidenzpfad festhalten.
- Bei Bedrohungsmodellen `STRIDE` als Basis und bei risikoreichen Flows zusätzlich relevante `CAPEC`-Patterns verwenden.
- Bei Web/API-Features den `ASVS`-Level und den Verifikationsumfang in `docs/security/` oder gleichwertiger Projektdokumentation ablegen.
- KI-Nutzung explizit klassifizieren: Entwicklungswerkzeug, keine KI im ausgelieferten/betriebenen System, oder KI-Runtime-/Produktkomponente; `AI-SBOM` entsprechend als `N/A` begründen oder in der Supply-Chain-Evidenz dokumentieren.
- Bei Release-/Artefakt-Arbeit `SBOM`, `AI-SBOM`, `VEX`, Provenance/SLSA-Nachweise und gegebenenfalls `OpenSSF Scorecard` in Release- oder Sicherheitsdokumentation einplanen.
- Bei Architekturänderungen `Zero Trust`-Anwendbarkeit und bei langlebigen Projekten `SAMM`-Folgeaktionen prüfen.
- Default-Evidenzpfad: `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, `docs/security/samm-assessment.md`; Abweichungen nur mit lokal dokumentierter Begründung.

*Capture the applicable standards and the evidence path in `spec.md`, `plan.md`, and `tasks.md`. Use `STRIDE` as the base for threat modeling and add relevant `CAPEC` patterns for the highest-risk flows. For web/API work, record the chosen `ASVS` level and verification scope in `docs/security/` or equivalent project documentation. Classify AI usage as development tooling, absent from the released/operated system, or AI runtime/product component; document `AI-SBOM` as `N/A` or as supply-chain evidence accordingly. For release and artefact work, plan `SBOM`, `AI-SBOM`, `VEX`, provenance/SLSA evidence, and `OpenSSF Scorecard` review where applicable. For architectural changes, evaluate `Zero Trust`; for long-lived projects, consider `OWASP SAMM` follow-up actions. The default evidence path is `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`, unless the repository documents a justified equivalent location.*

## Zentrale Verzeichnisse / Key Directories

- `~/scripts/`: Zentrale Automatisierungsskripte (Bootstrap, Secret-Scan, Hook-Installer).
- `~/`: Weitere Workspace-Verzeichnisse werden per `bootstrap-workspace` angelegt und hier eingetragen.
- `~/.gemini/`: Globale Gemini-Konfiguration und persistente Erinnerungen.

## Entwicklungskonventionen / Development Conventions

- **Plattformunabhängigkeit & Dokumentation:** Alle kritischen Skripte müssen sowohl als `.sh` (Bash) als auch als `.ps1` (PowerShell Core) vorliegen. Jedes Skript erfordert eine Unix man-Page (`.sh`, in `docs/man/`), eine vollständige PowerShell-Hilfe (`.ps1`) und muss zusätzlich als PowerShell Cmdlet (Advanced Function) im `Verb-Noun` Format verfügbar sein.
- **Sicherheits-Standard:** Jedes Projekt muss über einen `pre-push` Hook verfügen, der Secret-Scanning in Agenten-Verzeichnissen durchführt.
- **Git-Strategie:** Keine Submodules; stattdessen werden Sub-Repos durch die Baseline-Skripte in der `.gitignore` des übergeordneten Workspaces erfasst.

## Projektstatus / Repository Status

- **Sichtbarkeit:** Öffentliches **Template-Repo** — über „Use this template" nutzbar; kein Fork, keine History-Übertragung
- **Lizenz:** MIT
- **Branch-Schutz:** PR-Pflicht auf `main`; Admin (Eigentümer) kann direkt pushen (`enforce_admins: false`)
- **CI:** ✅ Ubuntu 22.04 · macOS 14 · Windows 2022
- **Compliance-Score:** 100 % (25/25 Checks)

## Bekannte Fallstricke / Known Pitfalls

### `gh auth login --web` bleibt hängen / `gh auth login --web` Hangs
Browser-Callback kommt in Hintergrundprozessen nicht an.
In **interaktivem Terminal** ausführen.

### `glab auth login --web` bleibt hängen / `glab auth login --web` Hangs
Browser-Callback kommt in Hintergrundprozessen nicht an.
In **interaktivem Terminal** ausführen.

### `gh`-Keyring ungültig (Windows) / `gh` Keyring Invalid (Windows)
Windows Credential Store korrupt.
`gh auth logout` + neu anmelden; danach `gh auth setup-git`.

### `ssh-agent` startet nicht (Windows) / `ssh-agent` Does Not Start (Windows)
Service deaktiviert, Admin nötig.
HTTPS + `gh auth setup-git` verwenden.

### `CursorPosition`-Fehler in PS-Subprocess / `CursorPosition` Error in PowerShell Subprocess
PowerShell-Profil (Oh-My-Posh) lädt im Subprozess.
`-NoProfile` zu `pwsh -File`-Aufrufen hinzufügen.

### `migrate-workspace.*` läuft parallel in Timeouts / `migrate-workspace.*` Times Out in Parallel
Jeder Migrationslauf startet `init-stats.*` und aktualisiert die Level-0/1/2-Statistiken global.
Mehrere parallele Läufe können sich gegenseitig ausbremsen. Erst Vorschau (`-WhatIf`/`--dry-run`),
dann echte Migrationen seriell pro Workspace mit längerem Timeout ausführen.

### `git pull` meldet divergierende Branches (Linux) / `git pull` Reports Divergent Branches (Linux)
Kein globales Rebase-Setup.
`git config --global pull.rebase true`.

### Push rejected: `fetch first` / Push Rejected: `fetch first`
Remote ist neuer als lokal.
`git pull --rebase --autostash && git push`.

### Test-Skript blockiert Pull / Test Script Blocks Pull
Output-Datei wird vor `pull` geschrieben.
`git pull --rebase --autostash origin main`.

### Lastenheft nach Feature-Abschluss nicht umbenannt / Lastenheft Not Renamed After Feature Completion
`tasks.md` enthielt keinen Rename-Schritt (seit constitution v1.1.1 behoben).
`bash scripts/rename-lastenheft.sh <LH-Datei> <branch-name>` oder `pwsh scripts/rename-lastenheft.ps1 -File <LH-Datei> -BranchName <branch-name>`.

### Windows: `$env:HOME` ist leer, nicht `$null` / Windows: `$env:HOME` Is Empty, Not `$null`
```powershell
# Falsch (??-Operator fängt '' nicht ab):
$home = $env:HOME ?? $env:USERPROFILE
# Richtig:
$home = if ($env:HOME) { $env:HOME } else { $env:USERPROFILE }
```

### CI: Scanner-Verzeichnis / CI: Scanner Directory
```bash
# Falsch (CWD = Repo-Root, Dateien nicht gefunden):
bash scripts/check-homogeneity.sh home-baseline
# Richtig (aus dem Parent heraus):
cd "$(dirname "$GITHUB_WORKSPACE")"
bash "$(basename "$GITHUB_WORKSPACE")/scripts/check-homogeneity.sh" "$(basename "$GITHUB_WORKSPACE")"
```

### `.gitignore`-Whitelist / `.gitignore` Whitelist
Jede neue Datei muss explizit als `!DATEINAME` in `.gitignore` eingetragen werden, sonst wird `git add` lautlos ignoriert (z. B. `LICENSE`).

### `bootstrap-workspace`: GitHub-Username / `bootstrap-workspace`: GitHub Username
Früher hardcodiert. Jetzt dynamisch:
```bash
GH_USER=$(gh api user --jq '.login')
```

### Doppelte Überschriften in TOC / Duplicate heading anchors
Gleiche Heading-Texte → GitHub hängt `-1`, `-2` an. TOC-Links für zweite Vorkommen müssen den Suffix enthalten.

### Pflicht für bilinguale Headings / Bilingual Heading Requirement
Format: `## DE / EN` — immer. Nur-Deutsch verletzt WCAG 2.4.6 und bilinguales Konsistenzgebot.
Ausnahme: Eigennamen wie `### Homogeneity Guardian` oder `### Compliance-Check`.

### Code-Blöcke immer mit Sprach-Tag (WCAG 4.1.1) / Code Blocks Must Always Have a Language Tag (WCAG 4.1.1)
Bare ` ``` ` ohne Sprache ist ein A11Y-Fehler. Für ASCII/Dialog/Verzeichnisse: ` ```text `.

### CHANGELOG.md hinzugefügt / CHANGELOG.md Added
Dokumentiert Versionen v0.1.0–v0.4.0. Muss in `.gitignore`-Whitelist (`!CHANGELOG.md`) eingetragen sein.

### ASCII-Box-Drawing-Tabellen: Zeilenbreite / ASCII Box-Drawing Tables: Line Width
Alle Zeilen einer `text`-Code-Block-Tabelle müssen exakt gleich breit sein. Ein überzähliges Leerzeichen vor dem schließenden `│` macht die Zeile 1 Zeichen zu lang.
Prüfen: PowerShell `$line.Length` oder `wc -m` (Bash) für jede Rahmen-Zeile.

### Spec-Kit-Verzeichnis initialisieren / Initialize the Spec-Kit Directory
Nie manuell aus `~/home-baseline-tmp/` kopieren. Stattdessen:
`specify init --here --force --integration {agent}` je Agent für `agy`, `opencode`, `claude`, `copilot` und `codex` ausführen.

### Spec-Kit-Updates repo-weit / Repository-Wide Spec-Kit Updates
Fuer Level 0, Level 1 und Level 2 nicht mehr per Hand in jedem Repo nachziehen.
Stattdessen zuerst `bash scripts/update-spec-kit.sh --dry-run` bzw.
`pwsh scripts/update-spec-kit.ps1 -WhatIf` ausfuehren, danach bei Bedarf
`--commit --push` / `-Commit -Push`.

Das Skript erkennt neue Repos dynamisch ueber `.git` plus `.specify/`, sichert
`.specify/memory/constitution.md`, legt die lokalen Governance-Templates wieder
auf und nimmt `RiderProjects/TuiVision` normal mit. OpenCode wird nur ueber
`.opencode/command/*.md` getrackt; `.opencode`-Caches, Sessions, Logs,
Credentials und lokale Abhaengigkeiten bleiben ausgeschlossen.

Die Standard-Template-Quelle ist das oeffentliche `home-baseline`-Repo, aus dem
das Skript laeuft. Private Repos wie `RiderProjects/TuiVision` duerfen nur
bewusst mit `--template-source` / `-TemplateSource` als Override genutzt werden.

### GitHub-Housekeeping: Archivierung, Sichtbarkeit, Forks und Stars / GitHub Housekeeping: Archiving, Visibility, Forks, and Stars
`archived` bedeutet bei GitHub nur read-only, nicht unsichtbar. Public archived Repos bleiben ohne Anmeldung sichtbar.
Archivierte Repos sind API-seitig read-only; Sichtbarkeit ändern geht deshalb nur über:
`archived=false` → `private=true` → `archived=true`.

Öffentliche Forks lassen sich nicht einfach auf private setzen. Optionen: öffentlich archiviert lassen, löschen, oder als private Mirror-Repos neu anlegen. Vor Löschungen die Repo-Liste eng festlegen; `gh repo delete` benötigt ggf. `gh auth refresh -h github.com -s delete_repo`.

Für Aktivitätsbewertungen `pushedAt` statt `updatedAt` verwenden, weil `updatedAt` durch Metadatenänderungen springt. Stars sind kontogebundene Metadaten und können über `DELETE /user/starred/{owner}/{repo}` entfernt werden; danach `user/starred` gegenprüfen.

## GitHub/GitLab CLI First / GitHub/GitLab CLI zuerst

Für GitHub-Repositories zuerst die authentifizierte `gh` CLI für mögliche Schreibaktionen und Live-Repository-Operationen verwenden, einschließlich PR-/Issue-Kommentaren, PR-Statusprüfungen, Review-Follow-up, Workflow-Prüfung und Merge-/Statusabfragen. GitHub-Connector-Tools hauptsächlich für strukturierte Read-only-Inspektion oder Fälle nutzen, in denen die CLI nicht geeignet ist.

Für GitLab-Repositories die authentifizierte `glab` CLI zuerst für gleichwertige Aktionen verwenden. Bekanntermaßen fehlschlagende Connector-Schreibwege nicht wiederholt versuchen, wenn `gh`/`glab` die Aufgabe direkt erledigen kann.

For GitHub repositories, use the authenticated `gh` CLI first for feasible write actions and live repository operations, including PR/issue comments, PR status checks, review follow-up, workflow inspection, and merge/status queries. Use GitHub connector tools mainly for structured read-only inspection or when the CLI is not suitable.

For GitLab repositories, use the authenticated `glab` CLI first for equivalent actions. Do not repeatedly try connector write paths that are known to fail when `gh`/`glab` can perform the task directly.


## Spec-Kit-Modell-Routing / Spec Kit Model Routing

- Modellwahl ist operative Agenten-Routing-Guidance, keine Feature-Anforderung. Modellnamen nicht in `spec.md`, `plan.md`, `tasks.md` oder einzelne Feature-Specs schreiben; diese Artefakte muessen reproduzierbar bleiben, auch wenn Modellnamen wechseln oder ein anderer KI-Agent verwendet wird.
- Der jeweilige Agent soll diese Empfehlungen auf seine aktuell verfuegbaren Modelle abbilden; keine feste Anbieter- oder Modellbindung ableiten.
- Fuer Spec-Kit-Spezifikation, Klaerung, Planung, Tasks und Analyse (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; je nach Agent auch `/speckit.specify` usw.) das staerkste verfuegbare Frontier-Reasoning-/Coding-Modell bevorzugen.
- Fuer vollstaendige, lang laufende `/speckit-implement`-Laeufe das staerkste verfuegbare Long-Running-Agent-Modell bevorzugen; das Frontier-Modell nutzen, wenn maximale Urteilsguete wichtiger ist als Laufzeitstabilitaet.
- Fuer fokussierte Reviews oder CI-Fixes ein coding-optimiertes Modell bevorzugen.
- Fuer triviale Bereinigung, Formatierung oder risikoarme mechanische Edits ist ein schnelles kleines Coding-Modell akzeptabel.

*Model choice is operational agent-routing guidance, not a feature requirement. Do not pin model names in `spec.md`, `plan.md`, `tasks.md`, or individual feature specs; those artifacts must stay reproducible even when model names change or another AI agent is used. Each agent should map these recommendations to its currently available models; do not derive a fixed vendor or model requirement. For Spec-Kit specification, clarification, planning, task generation, and analysis (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; or `/speckit.specify` etc. depending on the agent surface), prefer the strongest available frontier reasoning/coding model. For complete long-running `/speckit-implement` runs, prefer the strongest available long-running agent model; use the frontier model when maximum judgment quality is more important than runtime stability. For focused review or CI fixes, prefer a coding-optimized model. For trivial cleanup, formatting, or low-risk mechanical edits, a fast small coding model is acceptable.*

## Autonome Spec-Kit-Läufe / Autonomous Spec-Kit Runs

- Vollständig delegierte Spec-Kit-Läufe folgen `docs/spec-kit-autonomous-runbook.md` und verwenden den projektgebundenen Skill `$speckit-autonomous`.
- Vor dem Start muss der Delivery-Modus `LocalImplementation`, `PublishPR` oder `MergeAndSync` aus dem aktuellen Benutzerauftrag bestimmt werden. Allgemeine Autonomie erteilt keine stillschweigende Remote-Schreib- oder Merge-Berechtigung.
- Evidence wird vor der ersten Implementierungsänderung angelegt. Clarify, Checklists, Analyze, Implement und Remote Review werden bis zu den im Runbook definierten Konvergenzkriterien ausgeführt, nicht nach einer festen Wiederholungszahl.
- Ein repräsentativer vertikaler Slice mit Test und Proof kommt vor der breiten Wiederholung. Gemeinsame Evidence-, Versions-, Statistik-, Workflow- und Agent-Dateien bleiben Single-writer-Flächen.
- Jede Remote- oder Delivery-Task nennt den konkreten Repository-Evidence-Pfad für ihr Abnahmeergebnis; implizite Evidence-Verweise reichen für Analyze und Resume nicht aus.
- Jeder Lauf schützt den akzeptierten Scope, verwendet triggerbasierte Validierung und dokumentiert eine kurze Retrospektive für spätere Runbook-Verfeinerungen.

*Fully delegated Spec-Kit runs follow `docs/spec-kit-autonomous-runbook.md` and use the repository-local `$speckit-autonomous` skill. Determine `LocalImplementation`, `PublishPR`, or `MergeAndSync` from the current user request; general autonomy does not silently grant remote write or merge authority. Create evidence before implementation, iterate optional stages to their defined convergence criteria, prove one representative vertical slice before broad rollout, serialize shared writers, require each remote or delivery task to name its exact repository evidence path, protect accepted scope, use trigger-based validation, and record a short retrospective for later workflow refinement.*

## Spec-Kit-Preset-Pflege / Spec Kit Preset Maintenance

- Standard-Preset-Set: `security-governance` v0.6.0 prio 10, `architecture-governance` v0.5.0 prio 20, `isaqb-architecture-governance` v0.2.0 prio 30, `a11y-governance` v0.4.0 prio 40, `cross-platform-governance` v0.2.0 prio 50, `agent-parity-governance` v0.3.0 prio 60.
- `a11y-governance` v0.4.0 ergaenzt didaktische Inline-Code-Kommentar-Governance fuer neue oder geaenderte nicht-triviale Logik.
- `security-governance` v0.6.0 fuehrt `AI-SBOM` weiter als bedingt anwendbare Supply-Chain-Evidenz, ergaenzt sprachspezifische Secure-Coding-Profile und ergaenzt regulatorische Anwendbarkeit fuer NIS2, CRA, EU AI Act und DORA. Reine Entwicklungswerkzeug-Nutzung bleibt `N/A`; KI-Runtime-/Produktkomponenten benoetigen Evidenz nach G7/BSI AI-SBOM-Clustern; private Ausbildungsprojekte dokumentieren regulatorische Nichtanwendbarkeit mit kurzer Begruendung.
- `architecture-governance` v0.5.0 ergaenzt `BSI C3A` als bedingte Cloud-Autonomie-Evidenz und `BSI C5` als bedingte Cloud-Compliance-Assurance-Evidenz fuer Cloud-Service-Auswahl, Provider-Abhaengigkeiten, Audit-/Nachweisstand, Shared Responsibility und Betriebsnachweise.
- Alle sechs Presets enthalten ab diesem Release-Block audit-ready Spec-Kit-Run-Evidenz: `Applicable` / `N/A` / `Open`, Begruendung, Evidenzpfad, Reviewer, Restrisiko und Follow-up muessen im aktuellen Spec-Kit-Lauf dokumentiert werden.
- Alle sechs Presets sind seit 2026-05-04 im `github/spec-kit` Community-Katalog enthalten und liegen zusätzlich als veröffentlichte Repos unter `https://github.com/hindermath/spec-kit-preset-*`.
- Neue Level-2-Projekte SOLLEN bei der Spec-Kit-Initialisierung die passende Preset-Teilmenge installieren; C#/.NET-Level-2-Projekte verwenden standardmäßig alle sechs Presets, sofern keine begründete Ausnahme dokumentiert ist.
- Referenz-Rollout für alle sechs Presets: `RiderProjects/TinyPl0`, `RiderProjects/TinyCalc`, `RiderProjects/TuiVision`, `RiderProjects/InventarWorkerService`.
- Installation bevorzugt über den Community-Katalog, wenn `specify` das unterstützt; für reproduzierbare Pins die versionierten GitHub-ZIP-URLs aus `constitution.md`/`README.md` verwenden.
- `.specify/presets/` und generierte Agenten-/Command-Dateien committen, wenn Presets Projekt-Policy sind; `.specify/presets/.cache/` nie committen.
- Nach Installation oder Update prüfen: `specify preset list`, mindestens ein `specify preset info <id>`, bei Template-Fragen zusätzlich `specify preset resolve <template>`.
- Die lokale Arbeitskopie der veröffentlichten Preset-Repos liegt unter `~/SpecKitPresetProjects/`; kanonische Scaffolds in diesem Repo liegen unter `specs/spec-kit-presets/` und `specs/spec-kit-preset-repos/`.
- Verbesserungen an Presets zuerst im `home-baseline`-Scaffold einarbeiten, dann in die passenden Repos unter `~/SpecKitPresetProjects/` übertragen, committen, pushen und mit GitHub-ZIP-URL smoke-testen.
- Bei Änderungen an Preset-Regeln immer prüfen, ob `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `scripts/templates/*` ebenfalls aktualisiert werden müssen.
- Bei jeder Preset-Version oder Prioritätsänderung die kompakte Preset-Tabelle und ZIP-Installationsbefehle in `README.md`, die Matrix in `constitution.md`/`.specify/memory/constitution.md`, die vier Agenten-Dateien, `scripts/templates/speckit-workflow-section.md` und die Agenten-Templates gemeinsam aktualisieren.
- Community-/Katalog-Abstimmung läuft über `github/spec-kit#2362`.

*Standard preset set: `security-governance` v0.6.0 prio 10, `architecture-governance` v0.5.0 prio 20, `isaqb-architecture-governance` v0.2.0 prio 30, `a11y-governance` v0.4.0 prio 40, `cross-platform-governance` v0.2.0 prio 50, and `agent-parity-governance` v0.3.0 prio 60. `a11y-governance` v0.4.0 adds didactic inline-code-comment governance for new or changed non-trivial logic. `architecture-governance` v0.5.0 adds conditional `BSI C3A` cloud-autonomy evidence and `BSI C5` cloud-compliance assurance evidence for cloud-service selection, provider dependencies, audit/assurance status, shared responsibility, and operational evidence. `security-governance` v0.6.0 keeps conditional `AI-SBOM` evidence, language-specific secure-coding profiles, and regulatory applicability screening for NIS2, CRA, EU AI Act, and DORA: development-tool-only AI usage is `N/A`, AI runtime/product components require G7/BSI AI-SBOM cluster evidence, and private training projects record regulatory `N/A` when no regulated scope exists. All six presets now include audit-ready Spec-Kit run evidence: `Applicable` / `N/A` / `Open`, rationale, evidence path, reviewer, residual risk, and follow-up must be documented for the current Spec-Kit run. All six presets are in the `github/spec-kit` community catalog as of 2026-05-04 and are also published under `https://github.com/hindermath/spec-kit-preset-*`. New Level-2 projects should install the applicable subset; C#/.NET Level-2 projects default to all six unless a justified exception is documented. Commit `.specify/presets/` and generated agent command updates when presets are project policy, but never commit `.specify/presets/.cache/`. Verify installs with `specify preset list`, `specify preset info`, and where relevant `specify preset resolve`. Improve presets in the home-baseline scaffold first, propagate to standalone preset repos, then commit, push, and smoke-test via GitHub ZIP URL. Preset-rule changes and preset version/priority changes require reviewing constitution, README tables/install snippets, all agent guidance files, and relevant templates together. Community/catalog coordination happens in `github/spec-kit#2362`.*

<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read
`specs/018-editor-help-resources-hardening/plan.md`.
<!-- SPECKIT END -->

## Hinweise / Notes

- Diese Datei bleibt bewusst kompakt und ergänzt die projektspezifische Dokumentation.
- This file intentionally stays compact and complements the project-specific documentation.
