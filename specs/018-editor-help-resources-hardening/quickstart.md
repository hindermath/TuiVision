# Quickstart: Feature 018 Validation

## Preflight

```bash
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
git diff --check
```

Before every following `dotnet build` or `dotnet test`, increment only the
manual Build component in all three aligned version fields in
`Directory.Build.props`.

## Focused Proof

```bash
dotnet test tests/TuiVision.Serialization.Tests/TuiVision.Serialization.Tests.csproj --configuration Release
dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release
```

Required scenarios:

- Help source to model, persistence, runtime lookup, forward reference, and
  deterministic recompile.
- Duplicate/malformed/unresolved source rejection with no partial model.
- Exact language, ordered fallback, neutral, missing, empty value, and
  case-sensitive resource behavior.
- Coherent editor save/close/conflict/failure and help viewer navigation.

## Repository Validation

```bash
dotnet format --verify-no-changes
dotnet test --configuration Release
xmllint --noout coverlet.runsettings
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

The full Release and coverage commands are required because shared runtime code
changes. DocFX plus A11Y is required because public APIs and XML comments are
expected. Do not track generated `_site/`, `api/*.yml`, results, logs, or caches.

## Completion Review

- Every one of the six areas has exactly one framework decision.
- All malformed-input rows have explicit atomic rejection proof.
- No example, mouse, terminal, charset, dependency, or `tv203s/` change exists.
- Evidence, statistics, archived intake, completion marker, and next intake are
  aligned.
- Required remote checks pass and actionable review-thread count is zero before
  merge.
