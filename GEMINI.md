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
*   **Dokumentation:** `docfx docs/docfx.json` (sofern konfiguriert)
*   **Formatierung:** `dotnet format`

### Arbeitsumgebung
*   Optimiert für **Multi-Mac Workflow** (MacBook Air M2 & Mac mini M4 Pro).
*   Nutzung von `gh` (GitHub CLI) und `codex` für Agentic-Workflows.
*   Haupt-IDE: **JetBrains Rider**.

## 📝 Entwicklungskonventionen

1.  **Code-Stil:** Modernes C# (File-scoped Namespaces, Expression-bodied Members, Nullable Reference Types).
2.  **Dokumentation (MUSS):**
    *   Vollständige XML-Kommentare für alle öffentlichen APIs (`summary`, `param`, `returns`, `remarks`).
    *   Didaktischer Stil: Erklärt das *Warum* und bietet Beispiele für Lernende.
    *   Aktualisierung der Dokumentation erfolgt zeitgleich mit Codeänderungen.
3.  **Testing:**
    *   Mindestens 70% Testabdeckung für Kernmodule.
    *   Jedes Feature benötigt Unit-Tests (MSTest) und ggf. Smoke-Tests in den Beispielen.
4.  **Keine Nativen Abhängigkeiten:** Alle Treiber müssen rein in verwaltetem Code implementiert sein (kein P/Invoke, wo vermeidbar).
5.  **Lizenztreue:** Einhaltung der MIT-Lizenz für neuen Code; Respektierung der Original-Lizenzen im `tv203s` Ordner.

## 📚 Wichtige Dokumente
*   `README.md`: Allgemeine Einführung und CI-Status.
*   `Lasten_Heft.md`: Grobe Anforderungen und Ziele.
*   `Pflichtenheft.md`: Detaillierte technische Spezifikation (Referenz für MUSS-Anforderungen).
*   `docs/guides/multi-mac-workflow.md`: Anleitung für die verteilte Entwicklung.

---
*Hinweis: Dieses Dokument wurde automatisch von Gemini CLI generiert und dient als Instruktionsbasis.*
