# Implementation Plan: Example Portfolio Closure

## Summary / Zusammenfassung

Feature 039 ist ein evidence-only Abschlusslauf. Er verwendet den vorhandenen
Feature-038-Validator unverändert, bindet dessen gelieferten Datensatz und
Projektionen per Hash und erzeugt eine neue unabhängige Closure-Evidence.

## Technical Context / Technischer Kontext

- **Runtime**: .NET 10, MSTest, bestehender test-only Portfolio-Validator
- **Input**: `specs/038-example-portfolio-conformance-audit/`
- **Output**: ausschließlich `specs/039-example-portfolio-closure/` sowie
  planungs- und governancebezogene Metadaten
- **Product changes**: keine
- **Historical source review**: `N/A`; es wird kein historisch abgeleitetes
  Verhalten portiert, erweitert, getestet oder korrigiert

## Constitution Check / Verfassungsprüfung

- Read-only Produktgrenze: erfüllt durch Scope-Diff gegen Ausgangs-HEAD.
- Historische Quellen: unverändert; Feature-038-Provenienz wird nur revalidiert.
- Bilingual/A11Y: deutsch zuerst, englische Zusammenfassung direkt danach;
  geordnete text-first Tabellen und Listen.
- Qualität: bestehende Pflichtleiter wird vollständig neu ausgeführt.
- Versionierung: Branch-Minus `39`, Patch aus Commitzahl, Build vor jedem
  `dotnet build` oder `dotnet test` erhöhen.

## Phases / Phasen

1. Intake, Review und Feature-038-Artefakte per Hash binden.
2. Spezifikation, Plan, Aufgaben und Evidence vor der Validierung anlegen.
3. Vorhandenen Integritätstest gezielt und anschließend die vollständige lokale
   Gate-Leiter ausführen.
4. Geschützte Roots gegen den Ausgangs-HEAD prüfen.
5. Closure nur bei vollständig grünen anwendbaren Gates finalisieren.

## Validation / Validierung

- `ExamplePortfolioAuditIntegrityTests`
- vollständige Release-Tests
- fünf assembly-spezifische Coverage-Schwellen
- `dotnet format --verify-no-changes`
- DocFX, Playwright/Axe und Lynx/Textpfad
- vorhandene Bash-/PowerShell-Governancevalidatoren
- Git-Scope-Diff für geschützte Roots

## Risks / Risiken

Drift in Feature 038, ein neuer Finding-Satz oder ein geschütztes Root-Delta
blockiert den Abschluss. Lokale Evidence wird nicht als Remote-Exact-Head-
Evidence bezeichnet.
