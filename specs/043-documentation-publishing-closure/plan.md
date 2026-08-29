# Implementation Plan: Documentation and Publishing Closure

## Technischer Kontext / Technical Context

- Dokumentations-only Feature auf .NET 10; keine Runtime-, API-, Dependency-,
  Projekt- oder Beispieländerung.
- Kanonische Quellen sind Markdown, `docs/toc.yml`, bestehende Evidence und
  die unveränderte Pflichtenheft-Abstimmung.
- Veröffentlichung über den vorhandenen DocFX-/Pages-Pfad; A11Y-Nachweis über
  Playwright/Axe und textorientierten Gegencheck.
- Delivery: `MergeAndSync`; Admin-Bypass ausschließlich für eine allein
  verbleibende Human-Approval-Regel nach grünen technischen Gates.

## Dokumentationsarchitektur / Documentation Architecture

1. `docs/guides/getting-started.md` bildet den ersten sicheren Leserpfad.
2. `docs/guides/architecture.md` erklärt Schichten, Verantwortung und
   Modernisierungsgrenzen und verlinkt die tieferen Architekturartefakte.
3. `docs/guides/concepts/` erklärt Event-Loop, View-Hierarchie,
   Koordinatensystem und Serialisierung getrennt und aufgabenorientiert.
4. `docs/guides/tutorials/first-dialog.md` führt den kleinsten Dialog über den
   bestehenden Tutorial-Pfad aus, ohne neuen Beispielcode einzuführen.
5. `docs/guides/example-learning-paths.md` ist die vollständige 38-Zeilen-
   Lernvertragsmatrix und verweist auf die bestehenden Detail-Guides.
6. `docs/guides/historical-deviations.md` macht vorhandene, bewusste
   Abweichungen auffindbar, ohne eine neue Konformitätsentscheidung zu treffen.
7. `docs/documentation-closure.md` ordnet genau 27
   `DocumentationAndPublishing`-IDs einem Abschluss oder einer akzeptierten
   Grenze zu.
8. `docs/toc.yml` und `README.md` machen den neuen Leserpfad sichtbar.

## Durchführung / Execution

1. Die sieben allgemeinen Guides mit Zweck, Voraussetzungen, Ablauf,
   text-first Architekturhinweis, Übung und nächstem sicheren Schritt anlegen.
2. Alle 38 Beispielprojekte exakt einem Guide und sechs Lernfeldern zuordnen.
3. Die 27 Abstimmungseinträge dedupliziert schließen; Feature 015 und aktuelle
   Agent-/Workflow-Evidence als bereits bestehende Grenzen wiederverwenden.
4. Vorhandenen Multi-Mac-Guide und alle gepflegten Agent-Oberflächen gemeinsam
   prüfen; nur bei tatsächlicher Abweichung ändern.
5. Navigation, README, Evidence, Statistik und Intake-Status synchronisieren.

## Governance

- A11Y ist vollständig anwendbar: semantische Überschriften, beschreibende
  Links, Texttabellen, Tastaturpfade, kein ausschließlich visuelles Wissen.
- Security beschränkt sich auf Links, Secrets, sichere Befehle und ehrliche
  Supply-Chain-`N/A`-Entscheidungen.
- Architektur/iSAQB sind als Erklärqualität anwendbar; neue Runtime- oder
  Deployment-Entscheidungen entstehen nicht.
- Cross-Platform gilt für dokumentierte Befehle und CI; neue Skriptparität wird
  nicht ausgelöst.
- Agent-Parität wird geprüft. Ohne neue gemeinsame Regel bleiben Agent-Dateien
  und `.specify/templates/` unverändert.

## Validierungsfolge / Validation Sequence

1. Pfad-, Cardinality-, Überschriften-, Sprach- und Linkprüfung der neuen
   Dokumente.
2. `git diff --check` und `dotnet format --verify-no-changes`.
3. Ein Release-Build für CS1591; kein redundanter lokaler Volltest, weil kein
   ausführbarer Produkt- oder Testcode geändert wird.
4. `docfx docfx.json`, danach `tests/web-a11y` mit Playwright/Axe und ein
   repräsentativer Lynx-Dump.
5. Governance-, Homogeneity-, Agent-Parity-, Secret- und Scope-Prüfungen.
6. Exact-Head-Remote-Gates auf Ubuntu, macOS und Windows, Review-Konvergenz,
   PreMerge-Evidence, Merge und PostMerge-Synchronisierung.

Vor jedem `dotnet build` oder `dotnet test` wird der manuelle Build-Zähler
erhöht. Eine reine DocFX-, npm-, Format- oder Skriptprüfung erhöht ihn nicht.
