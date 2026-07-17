# Quickstart: Wave-6 TVFM Showcase Remediation

## Voraussetzungen / Prerequisites

```bash
git branch --show-current
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Expected branch: `036-wave6-tvfm-showcase-remediation`.

## Normaler Lernstart / Normal Learning Start

```bash
dotnet run --project examples/Tp7FileManager
```

Der normale Start verwendet nur eine frische kontrollierte Kopie der
veröffentlichten Fixtures. Menüs und fokussierbare Controls führen durch
Navigation, Vorschau, Suche und sichere Dateioperationsdialoge. `F1` öffnet
Description; `Ctrl+Q` beendet.

The normal launch uses only a fresh controlled copy of the published
fixtures. Menus and focusable controls expose navigation, preview, search,
and safe file-operation dialogs. `F1` opens Description; `Ctrl+Q` exits.

Mouse drag is optional. It prepares the same confirmation-required operation
as the keyboard and never mutates a file directly.

## Kontrollierter Smoke / Controlled Smoke

```bash
dotnet run --project examples/Tp7FileManager -- --smoke
```

The smoke path uses the real application loop and exits deterministically.

## Targeted showcase proof

Before each `dotnet build` or `dotnet test`, increment the manual build
counter once and align all version fields to `1.36.<patch>.<build>`.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj \
  --configuration Release \
  --filter "FullyQualifiedName~Wave6Showcase"
```

## Preserved filesystem proof

Use a separate build-counter increment:

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
dotnet test TuiVision.sln --configuration Release \
  --collect:"XPlat Code Coverage" \
  --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Also run controlled PTY, UTF-8/text-first, secret, supply-chain,
historical/protected-path, scope, agent-parity, state, and exact-head checks.
Do not track `_site/`, `api/*.yml`, `TestResults/`, logs, caches,
credentials, or temporary workspaces.

## Evidence review

Review:

- exactly one `Tp7FileManager` entry-point row;
- exactly ten `W6S-001` through `W6S-010` rows;
- every visible command and keyboard path;
- all four safe mutation-dialog paths;
- normal and `48x16` app-loop/view/focus/status/Description/cell proof;
- one closed framework decision per area;
- one closed final entry-point decision;
- seven preset applicability records;
- all command results, skipped triggers, residual risks, and follow-ups.

Feature 037, independent Wave-6 closure, and the post-Wave-6 portfolio audit
must not be started when Feature 036 completes.
