# Quickstart: Wave-6 Combined Delta Closure

## Purpose

This guide executes and reviews the read-only product audit. Git and GitHub
delivery authority exists only through the explicit resumed `MergeAndSync`
request and does not expand product scope.

## Preflight

```bash
git status --short --branch
git rev-parse HEAD
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Confirm branch `037-wave6-combined-delta-closure`, Feature-037 metadata, zero
incomplete checklist items and no protected-path changes.

## Focused closure validator

Before each `dotnet test`, increment the manual build counter once and align
all three version fields to `1.37.0.<build>`.

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj \
  --configuration Release \
  --filter "FullyQualifiedName~Wave6CombinedDeltaClosureTests"
```

## Targeted Wave-6 proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj \
  --configuration Release \
  --filter "FullyQualifiedName~Wave6"

dotnet run --project examples/Tp7FileManager --configuration Release \
  --no-build -- --smoke
```

The bounded normal PTY check starts the same entry point, drives a controlled
primary action, F1 and `Ctrl+Q`, and records only test-owned output.

## Full repository proof

```bash
dotnet test TuiVision.sln --configuration Release --no-restore
xmllint --noout coverlet.runsettings
dotnet test --configuration Release --no-restore \
  --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Every explicit `dotnet test` or `dotnet build` needs its own preceding build
counter increment. `dotnet run --no-build` does not increment it.

## Static and scope proof

```bash
git diff --check
git diff --name-only
git status --short
```

The diff may contain only feature metadata/evidence, the test-only validator
and version alignment. It must not contain `src/`, `examples/`, project or
package changes, historical-source changes or generated output.

## State validation

```bash
bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.sh \
  --state specs/037-wave6-combined-delta-closure/autonomous-run-state.json

pwsh -NoProfile -File \
  .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.ps1 \
  -State specs/037-wave6-combined-delta-closure/autonomous-run-state.json
```

Both validators must accept the same stage, status and task counts.

## Delivery completion

The feature head may be committed only when the dataset contains 24/10/10/10/1
records, zero findings, zero product decisions and all applicable local gates
pass. Merge requires exact-head provider evidence and review convergence.
Actual Wave-6 closure and portfolio eligibility are recorded only in the
non-recursive causal closeout. Completion requires `Retrospective`,
`Completed`, `nextExactAction: N/A`, clean synchronized `main`, and no start of
Feature 038.
