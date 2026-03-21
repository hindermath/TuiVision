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
    *   Vollständige XML-Kommentare für alle öffentlichen APIs (`summary`, `param`, `returns`, `remarks`).
    *   Didaktischer Stil: Erklärt das *Warum* und bietet Beispiele für Lernende.
    *   Aktualisierung der Dokumentation erfolgt zeitgleich mit Codeänderungen.
4.  **Testing:**
    *   Mindestens 70% Testabdeckung für Kernmodule.
    *   Jedes Feature benötigt Unit-Tests (MSTest) und ggf. Smoke-Tests in den Beispielen.
5.  **Keine Nativen Abhängigkeiten:** Alle Treiber müssen rein in verwaltetem Code implementiert sein (kein P/Invoke, wo vermeidbar).
6.  **Lizenztreue:** Einhaltung der MIT-Lizenz für neuen Code; Respektierung der Original-Lizenzen im `tv203s` Ordner.

## 📚 Wichtige Dokumente
*   `README.md`: Allgemeine Einführung und CI-Status.
*   `Lasten_Heft.md`: Grobe Anforderungen und Ziele.
*   `Pflichtenheft.md`: Detaillierte technische Spezifikation (Referenz für MUSS-Anforderungen).
*   `docs/guides/multi-mac-workflow.md`: Anleitung für die verteilte Entwicklung.

## 🎯 Aktueller Feature-Fokus

### `002-application-framework`
*   Plan-Quelle: `specs/002-application-framework/plan.md`
*   Ziel dieses Inkrements: erster vollständiger Anwendungsrahmen auf Basis von `TView` und `TGroup`
*   Geplanter Umfang in `src/TuiVision.Controls`:
    *   `TProgram`
    *   `TApplication`
    *   `TDesktop`
    *   `TMenuBar`
    *   `TStatusLine`
    *   leichte Menü-/Status-Aktionsmodelle und gemeinsame Shell-Command-IDs
*   Verhalten:
    *   `TApplication` erzeugt standardmäßig Menüleiste, Desktop und Statuszeile
    *   globale Aktionen müssen über Menü, Statuszeile und Tastatur konsistent geroutet werden
    *   nicht verfügbare Aktionen bleiben sichtbar, werden aber deaktiviert dargestellt
    *   Fokus muss nach Start, Aktivierungswechsel und Schließen des aktiven Desktop-Kinds gültig bleiben
*   Explizit nicht Teil dieses Schritts:
    *   konkrete Dialoge
    *   Controls/Widgets
    *   spezialisierte Fenstertypen
*   Testfokus:
    *   neue MSTest-Abdeckung in `tests/TuiVision.Controls.Tests/`
    *   TDD in sichtbarer Red-Green-Refactor-Reihenfolge
    *   Validierung mit `dotnet build --configuration Release`, `dotnet test`, `dotnet format --verify-no-changes` und bei API-/XML-Änderungen zusätzlich `docfx docfx.json`

## 🔄 Synchronisationsregel für KI-Agenten-Dateien

*   Wenn sich aktiver Feature-Kontext, Planungsstand oder gemeinsam genutzte Agenten-Hinweise ändern, müssen diese Dateien gemeinsam geprüft und bei Bedarf im selben Arbeitsgang aktualisiert werden:
    *   `AGENTS.md`
    *   `CLAUDE.md`
    *   `GEMINI.md`
    *   `.github/copilot-instructions.md`
*   Eine nur teilweise Synchronisierung ist nicht zulässig, wenn sich gemeinsame Vorgaben geändert haben.
*   Falls eine Datei absichtlich agentenspezifisch abweicht, muss diese Abweichung im selben Change ausdrücklich dokumentiert werden.

---
*Hinweis: Dieses Dokument wurde automatisch von Gemini CLI generiert und dient als Instruktionsbasis.*

## Active Technologies
- C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core` (TView, TGroup, TEvent, TObject, TPoint, TRect, (003-dialog-control-layer)
- N/A — in-memory UI state only; keine Persistenz in Phase 5 (003-dialog-control-layer)

## Recent Changes
- 003-dialog-control-layer: Added C# latest (C# 14) / .NET 10 (`net10.0`) + `TuiVision.Core` (TView, TGroup, TEvent, TObject, TPoint, TRect,
