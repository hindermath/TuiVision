# Quickstart: Interactive Wave 2 Demos

**Feature**: `012-interactive-wave2-demos`
**Date**: 2026-05-09

## Deutsch

Dieser Quickstart beschreibt, wie die spaetere Implementierung lokal vorbereitet, ausgefuehrt und geprueft werden soll. Die Beispiele muessen beim normalen Start sichtbar bedienbar sein; die Smoke-Tests muessen denselben Laufzeitpfad ueber das App-Event-System pruefen.

### 1. Arbeitszweig pruefen

```bash
git checkout 012-interactive-wave2-demos
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Erwartung: Der ausgegebene Spec-Pfad zeigt auf `specs/012-interactive-wave2-demos`.

### 2. Abhaengigkeiten wiederherstellen

```bash
dotnet restore
```

Es sollen keine neuen Runtime-Abhaengigkeiten fuer diese Funktion noetig sein.

### 3. Interaktive Beispiele manuell starten

Vor der Implementierung oder Abnahme eines Beispiels:

```bash
rg -n "relevanter Beispielname" tv203s
```

Die relevanten `.c`/`.cc`-Dateien und bei Bedarf wichtige passende Header unter `tv203s/` sind nur Referenzmaterial. Das Ergebnis der Pruefung muss festhalten, welchen historischen Demo-Zweck die C#-Interaktion abbildet und welche Abweichungen bewusst dokumentiert werden.

```bash
dotnet run --project examples/Demo
dotnet run --project examples/Clipboard
dotnet run --project examples/DlgDsn
```

Erwartung fuer jedes Beispiel:

- Der erste Bildschirm erklaert sichtbar den Zweck des Beispiels.
- Mindestens ein Menue-, Tastatur-, Status- oder Befehlspfad loest eine Funktion aus.
- Nach dem Befehl erscheint sichtbares Feedback, zum Beispiel Statuszeile, Auswahl, Fortschritt, Dialogzustand oder Fehlermeldung.
- Es gibt einen klaren Beenden-Pfad.

Nach der Demo-Vertikalscheibe muessen alle elf Beispiele gleichartig pruefbar sein:

```text
Clipboard, Demo, DlgDsn, DynTxt, InpLis, ListVi, ProgBa, Sdlg, Sdlg2, TCombo, TProgB
```

### 4. Beispiel-Smokes ausfuehren

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/
```

Erwartung: Die primaeren Smoke-Tests starten die jeweilige App-Laufzeit und injizieren `TEvent`-, Command- oder Tastaturereignisse. Direkte Hilfsmethoden duerfen nur vorbereiten oder ergaenzend pruefen.

### 5. Vollstaendige Validierung ausfuehren

```bash
dotnet test
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
```

Erwartung: Die Tests sind gruen, die Coverage-Grenze bleibt fuer die gate-relevanten Assemblies bei mindestens 70 Prozent, und die Formatpruefung meldet keine Aenderungen.

### 6. Dokumentation und A11Y pruefen

Wenn Guides, DocFX-Inhalte oder Navigationsflaechen geaendert wurden:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Erwartung: DocFX baut lokal, die Playwright/axe-Smokes pruefen repraesentative Seiten, und generierte Dateien aus `_site/` sowie `api/*.yml` bleiben aus dem Commit heraus.

### 7. Evidenz aktualisieren

Vor einem PR-Review muessen diese Artefakte die neue interaktive Laufzeit beschreiben:

```text
specs/012-interactive-wave2-demos/pr-evidence.md
examples/README.md
docs/guides/examples/
docs/architecture/
docs/security/
docs/project-statistics.md
Pflichtenheft.md
```

Falls ein Pfad bewusst unveraendert bleibt, muss die Begruendung kurz in der PR-Evidenz oder im passenden Governance-Dokument stehen.

## English

This quickstart describes how the later implementation should be prepared, run, and validated locally. The examples must be visibly operable during a normal start; the smoke tests must prove the same runtime path through the application event system.

### 1. Check the working branch

```bash
git checkout 012-interactive-wave2-demos
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Expected result: the reported spec path points to `specs/012-interactive-wave2-demos`.

### 2. Restore dependencies

```bash
dotnet restore
```

No new runtime dependency should be needed for this feature.

### 3. Run interactive examples manually

Before implementing or accepting an example:

```bash
rg -n "relevant example name" tv203s
```

The relevant `.c`/`.cc` files and, when needed, important matching headers under `tv203s/` are read-only reference material. The review result must record which historical demo purpose the C# interaction represents and which deviations are intentional.

```bash
dotnet run --project examples/Demo
dotnet run --project examples/Clipboard
dotnet run --project examples/DlgDsn
```

Expected result for each example:

- The first screen visibly explains the example purpose.
- At least one menu, keyboard, status, or command path triggers behavior.
- The command produces visible feedback, such as status text, selection, progress, dialog state, or an error message.
- A clear quit path exists.

After the Demo vertical slice, all eleven examples must be testable in the same way:

```text
Clipboard, Demo, DlgDsn, DynTxt, InpLis, ListVi, ProgBa, Sdlg, Sdlg2, TCombo, TProgB
```

### 4. Run example smokes

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/
```

Expected result: primary smoke tests run the application loop and inject `TEvent`, command, or keyboard events. Direct helper methods may only set up or supplement assertions.

### 5. Run full validation

```bash
dotnet test
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
```

Expected result: tests pass, coverage remains at least 70 percent for gate-relevant assemblies, and formatting is clean.

### 6. Validate documentation and A11Y

When guides, DocFX content, or navigation surfaces changed:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Expected result: DocFX builds locally, Playwright/axe smokes validate representative pages, and generated `_site/` plus `api/*.yml` files stay out of the commit.

### 7. Update evidence

Before PR review, these artifacts must describe the new interactive runtime:

```text
specs/012-interactive-wave2-demos/pr-evidence.md
examples/README.md
docs/guides/examples/
docs/architecture/
docs/security/
docs/project-statistics.md
Pflichtenheft.md
```

If a path intentionally stays unchanged, record the reason briefly in PR evidence or the matching governance document.
