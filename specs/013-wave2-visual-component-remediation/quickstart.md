# Quickstart: Wave 2 Visual Component Remediation

**Feature**: `013-wave2-visual-component-remediation`
**Date**: 2026-05-22

## Deutsch

Dieser Quickstart beschreibt, wie die spaetere Umsetzung lokal vorbereitet, ausgefuehrt und geprueft werden soll. Ziel ist nicht nur ein Textstatus, sondern eine sichtbare TuiVision-Komposition mit Hauptkomponente, `TStatusLine` und `Help -> Description`.

### 1. Arbeitszweig und Spec-Kit-Werkzeuge pruefen

```bash
git checkout 013-wave2-visual-component-remediation
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Erwartung: Der ausgegebene Spec-Pfad zeigt auf `specs/013-wave2-visual-component-remediation`.

### 2. Abhaengigkeiten wiederherstellen

```bash
dotnet restore
```

Erwartung: Es werden keine neuen Runtime-Abhaengigkeiten fuer diese Funktion benoetigt.

### 3. Historische Quellen pro Beispiel pruefen

Vor Implementierung oder Abnahme eines Beispiels:

```bash
rg -n "Clipboard|Demo|DlgDsn|DynTxt|InpLis|ListVi|ProgBa|Sdlg|Sdlg2|TCombo|TProgB" tv203s
```

Die relevanten `.c`/`.cc`-Dateien und bei Bedarf passende Header unter `tv203s/` sind nur Referenzmaterial. Die Pruefung muss festhalten, welchen historischen visuellen Zweck das Beispiel hatte, welche sichtbare C#-Zielkomposition umgesetzt wird und welche Abweichungen bewusst bleiben.

### 4. Sichtbare Beispiele manuell starten

```bash
dotnet run --project examples/Demo
dotnet run --project examples/Clipboard
dotnet run --project examples/DlgDsn
```

Nach der `Demo`-Vertikalscheibe muessen alle elf Beispiele gleichartig pruefbar sein:

```text
Clipboard, Demo, DlgDsn, DynTxt, InpLis, ListVi, ProgBa, Sdlg, Sdlg2, TCombo, TProgB
```

Erwartung fuer jedes Beispiel:

- Der Hauptbereich zeigt eine echte sichtbare Komponente oder einen stabilen sichtbaren Runtime-Zustand.
- Kurze dynamische Rueckmeldung erscheint in einer echten `TStatusLine`, ausser eine dokumentierte Abweichung nutzt einen gleichwertigen Statusbereich.
- `Help -> Description` ist tastaturerreichbar und erklaert Hauptkomponente, Bedienpfad, historischen Zweck und A11Y-Pruefpfad.
- Der sichtbare Zustand passt zum jeweiligen historischen Beispielzweck oder die Abweichung ist dokumentiert.

### 5. Beispiel-Smokes ausfuehren

Vor Build- oder Testbefehlen muss `Directory.Build.props` gemaess Branch-Version `1.13.<patch>.<build>` ausgerichtet und der manuelle Build-Zaehler erhoeht werden.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
```

Erwartung: Primaere Smokes laufen ueber `app.Run()` oder den gleichwertigen App-Loop, injizieren `TEvent`-, Command- oder Tastaturereignisse und pruefen konkrete Control-, Dialog-, Fokus-, Auswahl-, Scroll-, Eingabe-, Progress-, Abbruch- oder Cancel-Zustaende. Jeder primaere Smoke enthaelt View-Baum-Nachweis plus Buffer-/Cell-Snapshot.

### 6. Vollstaendige Validierung ausfuehren

Vor jedem Build- oder Testbefehl muss der Branch-Versions- und Build-Zaehlerstand erneut korrekt sein.

```bash
dotnet build --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
```

Erwartung: Release-Build und Tests sind gruen, die Coverage-Grenze bleibt fuer die gate-relevanten Assemblies bei mindestens 70 Prozent, und die Formatpruefung meldet keine Aenderungen.

### 7. Dokumentation und A11Y pruefen

Wenn Guides, DocFX-Inhalte, Navigationsdaten oder API-Dokumentation geaendert werden:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Erwartung: DocFX baut lokal, die Playwright/axe-Smokes pruefen repraesentative Seiten, und generierte Dateien aus `_site/` sowie `api/*.yml` bleiben aus dem Commit heraus.

### 8. Evidenz aktualisieren

Vor Review oder PR muessen diese Flaechen aktualisiert oder mit begruendetem `N/A`/Unchanged-Vermerk versehen sein:

```text
specs/013-wave2-visual-component-remediation/
examples/README.md
docs/guides/examples/
docs/architecture/
docs/security/
docs/project-statistics.md
Pflichtenheft.md
```

AI-SBOM bleibt fuer diese Funktion `N/A`, solange keine Runtime-/Produkt-KI, Modelle, Datensaetze, AI-Infrastruktur oder ausgelieferten AI-Komponenten eingefuehrt werden. Die aktive Security-Governance-Basis ist `security-governance` v0.4.0. Deren neue Sprachprofile fuer Rust, Go, Swift, Java/Kotlin, Python und TypeScript/JavaScript erzeugen fuer diese C#/.NET-Umsetzung keine neue Pflicht; C#/.NET-Secure-Coding und die bestehenden TuiVision-Regeln gelten weiter.

## English

This quickstart describes how the later implementation should be prepared, run, and validated locally. The goal is not only a text status, but a visible TuiVision composition with main component, `TStatusLine`, and `Help -> Description`.

### 1. Check branch and Spec-Kit tools

```bash
git checkout 013-wave2-visual-component-remediation
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Expected result: the reported spec path points to `specs/013-wave2-visual-component-remediation`.

### 2. Restore dependencies

```bash
dotnet restore
```

No new runtime dependency should be needed for this feature.

### 3. Review historical sources per example

Before implementing or accepting an example:

```bash
rg -n "Clipboard|Demo|DlgDsn|DynTxt|InpLis|ListVi|ProgBa|Sdlg|Sdlg2|TCombo|TProgB" tv203s
```

The relevant `.c`/`.cc` files and, when needed, matching headers under `tv203s/` are read-only reference material. The review must record the historical visual purpose, the visible C# target composition, and any intentional deviations.

### 4. Run visible examples manually

```bash
dotnet run --project examples/Demo
dotnet run --project examples/Clipboard
dotnet run --project examples/DlgDsn
```

After the `Demo` vertical slice, all eleven examples must be testable in the same way:

```text
Clipboard, Demo, DlgDsn, DynTxt, InpLis, ListVi, ProgBa, Sdlg, Sdlg2, TCombo, TProgB
```

Expected result for each example:

- The main area shows a real visible component or stable visible runtime state.
- Short dynamic feedback appears in a real `TStatusLine`, unless a documented deviation uses an equivalent status area.
- `Help -> Description` is keyboard-reachable and explains the main component, operation path, historical purpose, and A11Y review path.
- The visible state matches the historical example purpose or the deviation is documented.

### 5. Run example smokes

Before build or test commands, `Directory.Build.props` must be aligned to branch version `1.13.<patch>.<build>` and the manual build counter must be incremented.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
```

Expected result: primary smokes run through `app.Run()` or the equivalent application loop, inject `TEvent`, command, or keyboard events, and verify concrete control, dialog, focus, selection, scroll, input, progress, abort, or cancel states. Every primary smoke includes view-tree proof plus buffer/cell snapshot proof.

### 6. Run full validation

Before every build or test command, the branch version and build counter must be correct again.

```bash
dotnet build --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
```

Expected result: the Release build and tests pass, coverage remains at least 70 percent for gate-relevant assemblies, and formatting is clean.

### 7. Validate documentation and A11Y

When guides, DocFX content, navigation data, or API documentation are changed:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Expected result: DocFX builds locally, Playwright/axe smokes validate representative pages, and generated `_site/` plus `api/*.yml` files stay out of the commit.

### 8. Update evidence

Before review or PR, these surfaces must be updated or marked with justified `N/A`/unchanged rationale:

```text
specs/013-wave2-visual-component-remediation/
examples/README.md
docs/guides/examples/
docs/architecture/
docs/security/
docs/project-statistics.md
Pflichtenheft.md
```

AI-SBOM remains `N/A` for this feature as long as no runtime/product AI, models, datasets, AI infrastructure, or delivered AI components are introduced. The active security-governance baseline is `security-governance` v0.4.0. Its new language profiles for Rust, Go, Swift, Java/Kotlin, Python, and TypeScript/JavaScript create no new duty for this C#/.NET implementation; C#/.NET secure coding and the existing TuiVision rules continue to apply.
