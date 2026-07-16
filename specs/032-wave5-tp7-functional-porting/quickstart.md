# Quickstart: Wave-5 TP7 Functional Porting

## Voraussetzungen / Prerequisites

```bash
git branch --show-current
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Expected branch: `032-wave5-tp7-functional-porting`.

## Reference slice

```bash
dotnet run --project examples/Tp7Calculator
```

Use the keyboard command path to enter or execute a calculation. The visible
state must show the result. Division by zero must show a rejection while the
last valid value remains intact.

## All managed launch paths

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

## Targeted proof

Before the invocation, increment the manual build counter and align all version
fields to `1.32.<patch>.<build>`.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj \
  --configuration Release \
  --filter "FullyQualifiedName~Tp7|FullyQualifiedName~Wave5Functional"
```

The primary rows must all execute the app loop and combine state, view and
rendered-cell proof.

## Full validation

Each explicit `dotnet test` below requires its own preceding build-counter
increment.

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

Also run the repository secret, supply-chain, scope, protected-path and agent
parity checks. Do not add generated `_site/`, `api/*.yml`, `TestResults/`,
logs, caches or external checkouts to Git.

## Evidence review

Review:

- 15 source rows;
- six consumer rows;
- ten primary proof rows;
- ten showcase-delta rows;
- seven preset applicability rows;
- all command results and skipped triggers.

Wave 6 remains blocked. The later showcase intake may be present as a
Lastenheft derived from the delta, but Feature 033 must not exist.
