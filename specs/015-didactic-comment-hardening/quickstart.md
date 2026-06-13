# Quickstart: Didactic Inline Code Comment Hardening

**Feature**: `015-didactic-comment-hardening`
**Date**: 2026-06-14

## Deutsch

Dieser Quickstart beschreibt, wie die spaetere Umsetzung lokal vorbereitet,
ausgefuehrt und geprueft werden soll. Ziel ist ein selektiver
Kommentar-Haertungslauf fuer zentrale Framework-Flows und Smoke-Test-Helfer,
nicht eine Runtime-Aenderung.

### 1. Arbeitszweig und Spec-Kit-Werkzeuge pruefen

```bash
git checkout 015-didactic-comment-hardening
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Erwartung: Der ausgegebene Spec-Pfad zeigt auf
`specs/015-didactic-comment-hardening/spec.md`.

### 2. Evidence-Ledger anlegen oder pruefen

Die spaetere Implementierung fuehrt die verbindliche Review-Evidence hier:

```text
specs/015-didactic-comment-hardening/pr-evidence.md
```

Jeder Eintrag muss Review-Bereich, Hotspot-Kategorie, Entscheidung,
Begruendung, Kommentarbedarf, geaenderten oder ungeaenderten Kommentarzustand,
Aenderungszusammenfassung, Validierungs- oder Proof-Grenze und
Follow-up-Grenze festhalten.

### 3. Hotspot-Inventar erstellen

Pruefe mindestens diese Kategorien:

```text
Event-/Command-/Dispatch-Flows
Fokuswechsel und View-Hierarchie
StatusLine und Help/Description
Dialogzustand, Validation und Rejection
Buffer-/Cell-Proof und Rendering-Snapshots
Terminal-Fallbacks
historische Turbo-Vision-Abweichungen
Smoke-Test-Helfer
```

Wenn eine Kategorie aktuell keinen Kommentarbedarf hat, dokumentiert
`pr-evidence.md` die `NoCommentNeeded`- oder `CommentAdequate`-Begruendung.

### 4. Kommentarentscheidung je Bereich treffen

Erlaubt ist genau eine primaere Entscheidung:

```text
CommentAdequate
CommentNeeded
NoCommentNeeded
UpdateExistingComment
FollowUpHardening
```

`FollowUpHardening` beschreibt echte Framework-, Test-, Visual- oder
Proof-Probleme, die ausserhalb dieses Kommentar-Laufs bleiben.

### 5. Kommentare nur dort aendern, wo sie Lernwert haben

Neue oder geaenderte didaktische Kommentare:

- erklaeren Warum, Trade-off, Randbedingung, historische Abweichung oder
  Proof-Grenze;
- wiederholen nicht offensichtliche Identifier, Operatoren, Zuweisungen oder
  Assertions;
- bleiben normalerweise bei 1 bis 3 Zeilen;
- sind bei didaktischen Erklaerbloecken German-first/English-second und etwa
  CEFR-B2;
- lassen technische Lizenz-, Generator- und Markerzeilen unveraendert.

### 6. DocFX- und A11Y-Trigger pruefen

Pure `//`- oder `/* */`-Kommentarhaertung loest keinen DocFX-Zwang aus.

Wenn XML-Kommentare, API-Signaturen, generierte API-Dokumentation,
Dokumentationsnavigation oder learner-facing Guides geaendert werden:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Generierte `_site/`- und `api/*.yml`-Dateien bleiben aus dem Commit heraus.

### 7. Agent-Guidance pruefen

Wenn projektweite Kommentarregeln geaendert werden, sind diese Dateien zusammen
zu aktualisieren:

```text
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

Wenn nur feature-lokale Kommentare und Evidence betroffen sind, dokumentiert
`pr-evidence.md`, warum keine erneute Guidance-Aenderung noetig war.

### 8. Validierung ausfuehren

Vor jedem Build- oder Testbefehl muss `Directory.Build.props` gemaess
Branch-Version `1.15.<patch>.<build>` ausgerichtet und der manuelle
Build-Zaehler nach Repository-Regel erhoeht werden.

Vor jedem Commit oder Push auf dem nummerierten Branch muss
`Directory.Build.props` ebenfalls auf `1.15.<patch>.<build>` ausgerichtet
sein; der manuelle Build-Zaehler wird dabei nicht erhoeht.

Minimal fuer reine Kommentar-/Evidence-Aenderungen:

```bash
git diff --check
dotnet format --verify-no-changes
```

Wenn Source- oder Test-Helferdateien beruehrt werden, fuehre passende gezielte
Tests aus, zum Beispiel:

```bash
dotnet test tests/TuiVision.Core.Tests/ --configuration Release
dotnet test tests/TuiVision.Controls.Tests/ --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
dotnet test tests/TuiVision.Serialization.Tests/ --configuration Release
dotnet test tests/TuiVision.Compatibility.Tests/ --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
```

Wenn gemeinsame Logik oder breite Smoke-Helfer beruehrt werden:

```bash
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

### 9. Governance-Evidence abschliessen

`pr-evidence.md` muss festhalten:

- NIST SSDF und CWE Top 25 bleiben Level-2-Kontext;
- ASVS, SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM, NIS2, CRA, EU AI Act,
  DORA, STRIDE/CIA/CAPEC, S-ADR, Zero Trust, SAMM, BSI C3A/C5 und
  Cross-Platform-Script-Parity bleiben `N/A`, solange ihre Trigger nicht
  eintreten;
- geaenderte Evidence und Guidance bleiben text-first und barrierearm lesbar.

## English

This quickstart describes how the later implementation should be prepared,
run, and validated locally. The goal is a selective comment-hardening pass for
central framework flows and smoke-test helpers, not a runtime change.

### 1. Check branch and Spec-Kit tools

```bash
git checkout 015-didactic-comment-hardening
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Expected result: the reported spec path points to
`specs/015-didactic-comment-hardening/spec.md`.

### 2. Create or inspect the evidence ledger

The later implementation maintains the binding review evidence here:

```text
specs/015-didactic-comment-hardening/pr-evidence.md
```

Each entry records review area, hotspot category, decision, rationale, comment
need, changed or unchanged comment state, change summary, validation or proof
boundary, and follow-up boundary.

### 3. Create the hotspot inventory

Review at least these categories:

```text
Event/command/dispatch flows
Focus transitions and view hierarchy
StatusLine and Help/Description
Dialog state, validation, and rejection
Buffer/cell proof and rendering snapshots
Terminal fallbacks
historical Turbo Vision deviations
smoke-test helpers
```

If a category currently needs no comment, `pr-evidence.md` records the
`NoCommentNeeded` or `CommentAdequate` rationale.

### 4. Choose one decision per area

Exactly one primary decision is allowed:

```text
CommentAdequate
CommentNeeded
NoCommentNeeded
UpdateExistingComment
FollowUpHardening
```

`FollowUpHardening` describes real framework, test, visual, or proof problems
that remain outside this comment run.

### 5. Change comments only where they add learning value

New or changed didactic comments:

- explain why, trade-off, constraint, historical deviation, or proof boundary;
- do not repeat obvious identifiers, operators, assignments, or assertions;
- normally stay within 1 to 3 lines;
- are German-first/English-second and around CEFR-B2 for didactic explanation
  blocks;
- leave technical license, generator, and marker lines unchanged.

### 6. Check DocFX and A11Y triggers

Pure `//` or `/* */` comment hardening does not require DocFX.

If XML comments, API signatures, generated API documentation, documentation
navigation, or learner-facing guides change:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Generated `_site/` and `api/*.yml` files stay out of the commit.

### 7. Review agent guidance

If project-wide comment rules change, update these files together:

```text
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

If only feature-local comments and evidence change, `pr-evidence.md` records
why no further guidance change was needed.

### 8. Run validation

Before every build or test command, align `Directory.Build.props` to branch
version `1.15.<patch>.<build>` and increment the manual build counter according
to the repository rule.

Before every commit or push on the numbered branch, also align
`Directory.Build.props` to `1.15.<patch>.<build>`; do not increment the manual
build counter for commit-only or push-only work.

Minimum for pure comment/evidence changes:

```bash
git diff --check
dotnet format --verify-no-changes
```

When source or test helper files are touched, run the matching targeted tests.
When shared logic or broad smoke helpers are touched, run full Release tests
and the Coverlet coverage gate.

### 9. Finish governance evidence

`pr-evidence.md` records that NIST SSDF and CWE Top 25 remain Level-2 context,
and that the other governance standards remain `N/A` unless their triggers
change. Changed evidence and guidance stay text-first and accessible.
