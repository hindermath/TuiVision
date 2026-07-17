# Quickstart: Wave-6 TVFM Functional Porting

## Voraussetzungen / Prerequisites

```bash
git branch --show-current
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Expected branch: `035-wave6-tvfm-functional-porting`.

## Normaler Lernstart / Normal Learning Start

```bash
dotnet run --project examples/Tp7FileManager
```

Der Start kopiert veröffentlichte Fixtures in einen neuen temporären
Lernarbeitsbereich. Der sichtbare Root-Pfad gehört nur diesem Lauf. Die
Anwendung öffnet weder das aktuelle Verzeichnis noch persönliche Dateien.

The launch copies published fixtures into a new temporary learning workspace.
The visible root belongs only to that run. The application opens neither the
current directory nor personal files.

Use the keyboard to navigate, filter, tag, preview, search, prepare an
operation, confirm or cancel it, open `F1` Description, and exit with
`Ctrl+Q`.

## Kontrollierter Smoke / Controlled Smoke

```bash
dotnet run --project examples/Tp7FileManager -- --smoke
```

The smoke path uses the real application loop and exits deterministically.

## Targeted proof

Before each invocation, increment the manual build counter and align all
version fields to `1.35.<patch>.<build>`.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj \
  --configuration Release \
  --filter "FullyQualifiedName~Wave6"
```

## Full validation

Each explicit `dotnet test` command below requires its own preceding
build-counter increment.

```bash
git diff --check
dotnet format TuiVision.sln --verify-no-changes
dotnet test TuiVision.sln --configuration Release
dotnet test --configuration Release \
  --collect:"XPlat Code Coverage" \
  --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Also run controlled PTY, UTF-8/text-first, secret, supply-chain, protected
historical path, scope and agent-parity checks. Do not add `_site/`,
`api/*.yml`, `TestResults/`, logs, caches, credentials or temporary
workspaces to Git.

## Evidence review

Review:

- 24 historical source rows;
- ten functional-area decisions;
- all primary read, search, mutation and recovery proof rows;
- one Stage-2 disposition;
- seven preset applicability rows;
- all command results, skipped triggers and residual risks.

Feature 036 and the post-Wave-6 portfolio audit must not exist when Feature
035 completes.
