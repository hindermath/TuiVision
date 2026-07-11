# Quickstart: Didactic Inline Code Comment Hardening

**Feature**: `015-didactic-comment-hardening`
**Date**: 2026-06-14

## Deutsch

Dieser Quickstart beschreibt, wie die spätere Umsetzung lokal vorbereitet,
ausgeführt und geprüft werden soll. Ziel ist ein selektiver
Kommentar-Härtungslauf für zentrale Framework-Flows und Smoke-Test-Helfer,
nicht eine Runtime-Änderung.

### 1. Arbeitszweig und Spec-Kit-Werkzeuge prüfen

```bash
git checkout 015-didactic-comment-hardening
specify check
.specify/scripts/bash/check-prerequisites.sh --json --paths-only
```

Erwartung: Der ausgegebene Spec-Pfad zeigt auf
`specs/015-didactic-comment-hardening/spec.md`.

### 2. Evidence-Ledger anlegen oder prüfen

Die spätere Implementierung führt die verbindliche Review-Evidence hier:

```text
specs/015-didactic-comment-hardening/pr-evidence.md
```

Jeder Eintrag muss Review-Bereich, Hotspot-Kategorie, Entscheidung,
Begründung, Kommentarbedarf, geänderten oder ungeänderten Kommentarzustand,
Änderungszusammenfassung, Validierungs- oder Proof-Grenze und
Follow-up-Grenze festhalten.

### 3. Hotspot-Inventar erstellen

Prüfe mindestens diese Kategorien:

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
`pr-evidence.md` die `NoCommentNeeded`- oder `CommentAdequate`-Begründung.

### 4. Kommentarentscheidung je Bereich treffen

Erlaubt ist genau eine primäre Entscheidung:

```text
CommentAdequate
CommentNeeded
NoCommentNeeded
UpdateExistingComment
FollowUpHardening
```

`FollowUpHardening` beschreibt echte Framework-, Test-, Visual- oder
Proof-Probleme, die außerhalb dieses Kommentar-Laufs bleiben.

### 5. Kommentare nur dort ändern, wo sie Lernwert haben

Neue oder geänderte didaktische Kommentare:

- erklären Warum, Trade-off, Randbedingung, historische Abweichung oder
  Proof-Grenze;
- wiederholen nicht offensichtliche Identifier, Operatoren, Zuweisungen oder
  Assertions;
- bleiben normalerweise bei 1 bis 3 Zeilen;
- sind bei didaktischen Erklärblöcken German-first/English-second und etwa
  CEFR-B2;
- lassen technische Lizenz-, Generator- und Markerzeilen unverändert.

### 6. DocFX- und A11Y-Trigger prüfen

Pure `//`- oder `/* */`-Kommentarhärtung löst keinen DocFX-Zwang aus.

Wenn XML-Kommentare, API-Signaturen, generierte API-Dokumentation,
Dokumentationsnavigation oder learner-facing Guides geändert werden:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Generierte `_site/`- und `api/*.yml`-Dateien bleiben aus dem Commit heraus.

### 7. Agent-Guidance prüfen

Wenn projektweite Kommentarregeln geändert werden, sind diese Dateien zusammen
zu aktualisieren:

```text
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

Wenn nur feature-lokale Kommentare und Evidence betroffen sind, dokumentiert
`pr-evidence.md`, warum keine erneute Guidance-Änderung nötig war.

### 8. Validierung ausführen

Vor jedem Build- oder Testbefehl muss `Directory.Build.props` gemäß
Branch-Version `1.15.<patch>.<build>` ausgerichtet und der manuelle
Build-Zähler nach Repository-Regel erhöht werden.

Vor jedem Commit oder Push auf dem nummerierten Branch muss
`Directory.Build.props` ebenfalls auf `1.15.<patch>.<build>` ausgerichtet
sein; der manuelle Build-Zähler wird dabei nicht erhöht.

Minimal für reine Kommentar-/Evidence-Änderungen:

```bash
git diff --check
dotnet format --verify-no-changes
```

Wenn Source- oder Test-Helferdateien berührt werden, führe passende gezielte
Tests aus, zum Beispiel:

```bash
dotnet test tests/TuiVision.Core.Tests/ --configuration Release
dotnet test tests/TuiVision.Controls.Tests/ --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
dotnet test tests/TuiVision.Serialization.Tests/ --configuration Release
dotnet test tests/TuiVision.Compatibility.Tests/ --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
```

Wenn gemeinsame Logik oder breite Smoke-Helfer berührt werden:

```bash
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

### 9. Governance-Evidence abschließen

`pr-evidence.md` muss festhalten:

- alle sechs Presets mit der aktuellen Version und jeden relevanten Prüfpunkt
  als `Applicable`, `N/A` oder `Open`;
- Run-ID, Begründung, Evidence-Pfad, Owner, Reviewer, Review-Datum, Ergebnis,
  Restrisiko, Follow-up und Neubewertungstrigger für die Governance-Zeilen;
- NIST SSDF und CWE Top 25 bleiben Level-2-Kontext;
- ASVS, SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM, NIS2, CRA, EU AI Act,
  DORA, STRIDE/CIA/CAPEC, S-ADR, Zero Trust, SAMM, BSI C3A/C5 und
  Cross-Platform-Script-Parity bleiben `N/A`, solange ihre Trigger nicht
  eintreten;
- geänderte Evidence und Guidance bleiben text-first und barrierearm lesbar.

`N/A` benötigt eine Begründung und einen Neubewertungstrigger. `Open` benötigt
zusätzlich einen Owner und ein konkretes Follow-up. Leere Starter-Zeilen oder
still ausgelassene Prüfpunkte gelten nicht als Evidence.

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

`pr-evidence.md` records all six presets with their current versions and every
relevant checkpoint as `Applicable`, `N/A`, or `Open`. Each governance row
contains the run ID, rationale, evidence path, owner, reviewer, review date,
result, residual risk, follow-up, and re-evaluation trigger. NIST SSDF and CWE
Top 25 remain Level-2 context; the other governance standards remain `N/A`
unless their triggers change. `N/A` requires rationale and a re-evaluation
trigger. `Open` additionally requires an owner and concrete follow-up. Empty
starter rows or silently omitted checkpoints are not evidence. Changed
evidence and guidance stay text-first and accessible.
