# Quickstart: Wave-3 Visual Component Porting

## Preconditions

```bash
git switch 019-wave3-visual-component-porting
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Confirm `.specify/feature.json` points to
`specs/019-wave3-visual-component-porting` and all checklists are complete.

## Start the Examples

```bash
dotnet run --project examples/BHelp
dotnet run --project examples/HelpDemo
dotnet run --project examples/I18n
dotnet run --project examples/TvEdit
dotnet run --project examples/TvHc
```

Each first frame must show a domain main surface, real status line, and
keyboard-reachable Help/Description route. `TvEdit` and `TvHc` must not discover
or overwrite arbitrary user files.

## Proof Order

1. Create `pr-evidence.md` before runtime edits.
2. Add the complete failing `TvEdit` vertical-slice proof.
3. Implement shared presentation plus `TvEdit`; record the passing slice.
4. Add grouped failing Help proofs, then Help implementations.
5. Add grouped failing i18n/compiler proofs, then implementations.
6. Complete the five-example matrix, guides, governance, and validation.

## Versioned Validation

Before every `dotnet build` or `dotnet test`, increment only the manual build
counter in `Directory.Build.props`. Use `1.19.<branch-commit-count>.<build>`.

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Record exact command, version, result, counts, coverage, skipped trigger, and
failure boundary in `pr-evidence.md`. Never commit `_site/`, generated `api/*.yml`,
test results, caches, logs, or credentials.

## Completion

- Five framework decisions, five primary proof rows, and all governance rows are complete.
- Lastenheft is archived with `.019-wave3-visual-component-porting.md` suffix.
- Remote tasks name `specs/019-wave3-visual-component-porting/pr-evidence.md`.
- Required checks pass, actionable threads are zero, and authorized merge/sync is recorded.
