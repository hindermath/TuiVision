# Quickstart: Wave 1 Functional Hardening

**Feature**: `014-wave1-functional-hardening`
**Date**: 2026-05-31

## Deutsch

Dieser Quickstart beschreibt, wie die spaetere Umsetzung lokal vorbereitet,
ausgefuehrt und geprueft werden soll. Ziel ist eine belastbare funktionale
Beweisbasis fuer `Desklogo`, `MsgCls`, `Tutorial` und `Videomode`, nicht die
spaetere sichtbare Wave-1-Remediation.

### 1. Arbeitszweig und Spec-Kit-Werkzeuge pruefen

```bash
git checkout 014-wave1-functional-hardening
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Erwartung: Der ausgegebene Spec-Pfad zeigt auf
`specs/014-wave1-functional-hardening`.

### 2. Abhaengigkeiten wiederherstellen

```bash
dotnet restore
```

Erwartung: Es werden keine neuen Runtime-Abhaengigkeiten fuer diese Funktion
benoetigt.

### 3. Primaere Evidence-Matrix anlegen oder pruefen

Die spaetere Implementierung fuehrt die verbindliche Matrix hier:

```text
specs/014-wave1-functional-hardening/pr-evidence.md
```

Die Matrix muss fuer jedes Wave-1-Gebiet mindestens historische Quelle,
historische Kernfunktion, aktuelle C#-Abbildung, Proof-Methode,
Helper-Klassifikation, negative/Fallback-Nachweise, fehlende Kernfunktionen und
bewusste Abweichungen festhalten. Sie muss ausserdem Dokumentations-Trigger,
Validierungsnachweis oder Blocker und die jeweilige Evidence-Stelle nennen.

### 4. Historische Quellen pruefen

Vor Implementierung oder Abnahme eines Bereichs sind diese Dateien nur lesend
zu pruefen:

```text
tv203s/contrib/tvision/examples/desklogo/desklogo.cc
tv203s/contrib/tvision/examples/desklogo/set-logo.cc
tv203s/contrib/tvision/examples/desklogo/tv_logo.cc
tv203s/contrib/tvision/examples/msgcls/testdyn.cpp
tv203s/contrib/tvision/examples/msgcls/tlnmsg.cpp
tv203s/contrib/tvision/examples/msgcls/tlnmsg.h
tv203s/contrib/tvision/examples/tutorial/tvguid01.cc
...
tv203s/contrib/tvision/examples/tutorial/tvguid16.cc
tv203s/contrib/tvision/examples/videomode/test.cc
```

`set-logo.cc` und `tv_logo.cc` dienen nur zur Desklogo-Asset- und
Generator-Abgrenzung. `tv203s/` wird nicht veraendert.

### 5. Bestehende Wave-1-Beispiele starten

```bash
dotnet run --project examples/Desklogo
dotnet run --project examples/MsgCls
dotnet run --project examples/Tutorial -- tvguid01
dotnet run --project examples/Tutorial -- tvguid16
dotnet run --project examples/Videomode
```

Erwartung: Die Beispiele bleiben lauffaehig. Dieser Feature-Lauf bewertet aber
nicht nur Startbarkeit, sondern die historische Kernfunktion und den
fachlichen Smoke-Nachweis.

### 6. Smoke-Proof schaerfen

Primaere Smokes muessen reale Beispiel- oder App-Logik beweisen. Erlaubte
primaere Pfade sind oeffentliche Commands, Events, App-Methoden oder stabiler
oeffentlicher Zustand mit konkreten Assertions. Reine Setup-Helfer,
private Detailinspektion und Verhalten, das den geprueften Pfad umgeht, duerfen
nicht als `PrimaryProof` zaehlen.

Gezielte Beispiel-Smokes:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Desklogo"
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~MsgCls"
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Tutorial"
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Videomode"
```

Vor jedem Build- oder Testbefehl muss `Directory.Build.props` gemaess
Branch-Version `1.14.<patch>.<build>` ausgerichtet und der manuelle
Build-Zaehler nach Repository-Regel erhoeht werden.

### 7. Negative, Fallback- und Missing-Core-Pfade pruefen

Wenn ein negativer oder Fallback-Pfad deterministisch ausloesbar ist, wird er
per Smoke bewiesen. Wenn die Umgebung ihn nicht deterministisch ausloesen kann,
muss `pr-evidence.md` Ausloeser, erwartete Abweichung, beobachteten Fallback
und Nachweisgrenze festhalten.

Wenn historische Quellen eine fehlende Kernfunktion zeigen, gilt:

- Kleine notwendige Funktionsluecken fuer den bestehenden Wave-1-Zweck werden
  implementiert und per Smoke bewiesen.
- Luecken, die breite Framework-Arbeit, visuelle Remediation, neue
  Abhaengigkeiten oder anderen Scope benoetigen, werden als bewusste Abweichung
  oder Follow-up dokumentiert.

### 8. Dokumentation und A11Y pruefen

Guides oder `examples/README.md` muessen nur dann geaendert werden, wenn
Runtime-Verhalten, Bedienpfad, sichtbare Ausgabe, historische Abweichung oder
learner-facing Proof-Erklaerung betroffen sind.

Wenn Guides, DocFX-Inhalte, Navigationsdaten oder API-Dokumentation geaendert
werden:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Erwartung: DocFX baut lokal, Playwright/axe prueft repraesentative Seiten, und
generierte `_site/`- sowie `api/*.yml`-Dateien bleiben aus dem Commit heraus.

### 9. Vollstaendige Implementierungsvalidierung

Die spaetere Umsetzung soll diese Befehle ausfuehren und in `pr-evidence.md`
festhalten:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
git diff --check
```

DocFX und web-a11y kommen dazu, wenn Dokumentationsausgabe oder Navigation
betroffen sind.

### 10. Governance und Agent-Kontext

AI-SBOM bleibt fuer diese Funktion `N/A`, solange keine Runtime-/Produkt-KI,
Modelle, Datensaetze, AI-Infrastruktur oder ausgelieferten AI-Komponenten
eingefuehrt werden. `NIST SSDF` und `CWE Top 25` bleiben Basiskontext.
`OWASP ASVS`, `CAPEC` und `Zero Trust` bleiben `N/A`, solange keine Web-,
API-, Auth- oder neue Trust-Boundary-Flaeche entsteht.

Die Agent-Guidance-Dateien sind gemeinsam zu pruefen, wenn aktive
Feature-Kontexte, Technologien, Projektstruktur oder gemeinsame Workflow-Regeln
geaendert werden:

```text
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

## English

This quickstart describes how the later implementation should be prepared, run,
and validated locally. The goal is a reliable functional proof base for
`Desklogo`, `MsgCls`, `Tutorial`, and `Videomode`, not the later visible Wave-1
remediation.

### 1. Check branch and Spec-Kit tools

```bash
git checkout 014-wave1-functional-hardening
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Expected result: the reported spec path points to
`specs/014-wave1-functional-hardening`.

### 2. Restore dependencies

```bash
dotnet restore
```

No new runtime dependency should be needed for this feature.

### 3. Create or inspect the primary evidence matrix

The later implementation maintains the binding matrix here:

```text
specs/014-wave1-functional-hardening/pr-evidence.md
```

For each Wave-1 area, the matrix must record at least historical source,
historical core function, current C# mapping, proof method, helper
classification, negative/fallback proof, missing core functions, and intentional
deviations. It must also name documentation triggers, validation evidence or
blockers, and the matching evidence location.

### 4. Review historical sources

Before implementing or accepting an area, review these files as read-only
material:

```text
tv203s/contrib/tvision/examples/desklogo/desklogo.cc
tv203s/contrib/tvision/examples/desklogo/set-logo.cc
tv203s/contrib/tvision/examples/desklogo/tv_logo.cc
tv203s/contrib/tvision/examples/msgcls/testdyn.cpp
tv203s/contrib/tvision/examples/msgcls/tlnmsg.cpp
tv203s/contrib/tvision/examples/msgcls/tlnmsg.h
tv203s/contrib/tvision/examples/tutorial/tvguid01.cc
...
tv203s/contrib/tvision/examples/tutorial/tvguid16.cc
tv203s/contrib/tvision/examples/videomode/test.cc
```

`set-logo.cc` and `tv_logo.cc` are used only for Desklogo asset and generator
boundary decisions. `tv203s/` is not modified.

### 5. Run existing Wave-1 examples

```bash
dotnet run --project examples/Desklogo
dotnet run --project examples/MsgCls
dotnet run --project examples/Tutorial -- tvguid01
dotnet run --project examples/Tutorial -- tvguid16
dotnet run --project examples/Videomode
```

Expected result: the examples remain runnable. This feature does not accept
startup alone; it evaluates historical core function and meaningful smoke proof.

### 6. Harden smoke proof

Primary smokes must prove real example or application logic. Valid primary
paths are public commands, events, application methods, or stable public state
with concrete assertions. Setup-only helpers, private detail inspection, and
paths that bypass the reviewed behavior cannot count as `PrimaryProof`.

Targeted example smokes:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Desklogo"
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~MsgCls"
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Tutorial"
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release --filter "FullyQualifiedName~Videomode"
```

Before every build or test command, `Directory.Build.props` must be aligned to
branch version `1.14.<patch>.<build>` and the manual build counter must be
incremented according to repository rules.

### 7. Check negative, fallback, and missing-core paths

If a negative or fallback path can be triggered deterministically, prove it by
smoke test. If the environment cannot trigger it deterministically,
`pr-evidence.md` must record trigger, expected deviation, observed fallback, and
proof boundary.

If historical sources reveal a missing core function:

- Small necessary functional gaps for the existing Wave-1 purpose are
  implemented and smoke-proven.
- Gaps that require broad framework work, visual remediation, new dependencies,
  or other out-of-scope behavior are documented as intentional deviation or
  follow-up.

### 8. Validate documentation and A11Y

Guides or `examples/README.md` must be changed only when runtime behavior,
usage path, visible output, historical deviation, or learner-facing proof
explanation changes.

When guides, DocFX content, navigation data, or API documentation change:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Expected result: DocFX builds locally, Playwright/axe validates representative
pages, and generated `_site/` plus `api/*.yml` files stay out of the commit.

### 9. Run full implementation validation

The later implementation should run and record these commands in
`pr-evidence.md`:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
git diff --check
```

DocFX and web-a11y are added when documentation output or navigation is
affected.

### 10. Governance and agent context

AI-SBOM remains `N/A` for this feature as long as no runtime/product AI,
models, datasets, AI infrastructure, or delivered AI components are introduced.
`NIST SSDF` and `CWE Top 25` remain baseline context. `OWASP ASVS`, `CAPEC`,
and `Zero Trust` stay `N/A` while no web, API, auth, or new trust-boundary
surface appears.

Review the agent guidance files together when active feature context,
technologies, project structure, or shared workflow rules change:

```text
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```
