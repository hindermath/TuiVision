# Quickstart: Wave-5 TP7 Showcase Remediation

## Voraussetzungen / Prerequisites

```bash
git branch --show-current
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Expected branch: `033-wave5-tp7-showcase-remediation`.

## Referenz-Slice / Reference Slice

```bash
dotnet run --project examples/Tp7Calculator
```

Use the visible calculator controls or keyboard shortcuts. The display, button
grid, focus text, and status must remain visible in the accepted `40x12`
layout. Open `Help -> Description` from the keyboard. Division by zero must
preserve the last valid display value and show a text rejection.

## Alle Showcase-Pfade / All Showcase Paths

```bash
dotnet run --project examples/Tp7Demo
dotnet run --project examples/Tp7Edit
dotnet run --project examples/Tp7Help
dotnet run --project examples/Tp7ResourceDemo
dotnet run --project examples/Tp7ResourceGenerator
dotnet run --project examples/Tp7AsciiTable
dotnet run --project examples/Tp7Calculator
dotnet run --project examples/Tp7Calendar
dotnet run --project examples/Tp7Puzzle
dotnet run --project examples/Tp7MouseDialog
```

Each path must show a concrete main component, real status, and a
keyboard-reachable Description. The guides under `docs/guides/examples/`
contain the complete shortcuts and controlled boundaries.

## Gezielter Proof / Targeted Proof

Before the invocation, increment the manual build counter and align the three
version fields to `1.33.<patch>.<build>`.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj \
  --configuration Release \
  --filter "FullyQualifiedName~Tp7|FullyQualifiedName~Wave5Showcase"
```

The ten primary rows must execute the app loop and combine domain state,
concrete view/focus identity, status, Description, and rendered-cell proof.

## Normaler Entry-Point-Smoke / Normal Entry-Point Smoke

After one explicit versioned Release build, run each executable without
another implicit build:

```bash
dotnet run --no-build --configuration Release --project examples/Tp7Demo -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7Edit -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7Help -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7ResourceDemo -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7ResourceGenerator -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7AsciiTable -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7Calculator -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7Calendar -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7Puzzle -- --smoke
dotnet run --no-build --configuration Release --project examples/Tp7MouseDialog -- --smoke
```

## Vollständige Validierung / Full Validation

Each explicit `dotnet build` or `dotnet test` requires its own preceding
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

Also run scope, protected/generated-path, secret, supply-chain, agent-parity,
platform, review, and exact-head gates. Do not track `_site/`, generated
`api/*.yml`, TestResults, logs, caches, or external checkouts.

## Evidence Review

Review exactly:

- ten showcase rows and ten framework decisions;
- ten main/status/Description proofs;
- ten keyboard inventories and normal launch paths;
- ten constrained-layout proofs;
- all controlled-boundary and historical-deviation rows;
- seven governance preset rows;
- all local, remote, review, and exact-head results.

Wave 6 remains blocked pending a separate review of the actual Feature-033
delta. Do not create Feature 034.
