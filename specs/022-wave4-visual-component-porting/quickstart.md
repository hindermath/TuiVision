# Quickstart: Wave-4 Visual Component Porting

## Preconditions

```bash
git switch 022-wave4-visual-component-porting
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Confirm `.specify/feature.json` points to
`specs/022-wave4-visual-component-porting` and every checklist is complete.

## Start the Examples

```bash
dotnet run --project examples/Cyrillic
dotnet run --project examples/ETerm
dotnet run --project examples/Fonts
dotnet run --project examples/Terminal
dotnet run --project examples/XTerm
```

Each first frame must show a domain main surface, dynamic status, and
keyboard-reachable description. No demo may start a host process or mutate
terminal, locale, font, codepage, or keyboard state.

## Proof Order

1. Create `pr-evidence.md` and pre-name `closeout-evidence.md` before runtime edits.
2. Review historical sources and the full compile/linked-source surface.
3. Add the complete failing `Terminal` vertical-slice matrix.
4. Implement shared presentation plus `Terminal`; record the passing slice.
5. Add grouped failing Cyrillic/Fonts proof, then implementations.
6. Add grouped failing ETerm/XTerm proof, then immutable manifest demos.
7. Complete five-example, host, narrow-viewport, guide, governance, and validation matrices.

## Versioned Validation

Before every `dotnet build` or `dotnet test`, increment only the manual build
counter. Use `1.22.<branch-commit-count>.<build>`.

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
xmllint --noout coverlet.runsettings
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Record exact command, version, result, counts, coverage, skipped trigger, and
failure boundary. Never commit `_site/`, generated `api/`, test results, caches,
logs, credentials, or validation output.

## Completion

- Five framework decisions, primary proof rows, host rows, and governance rows are complete.
- Lastenheft is archived with `.022-wave4-visual-component-porting.md` suffix.
- Required checks pass, actionable threads are zero, and authorized merge/sync is recorded.
- Self-invalidating reviewed-head and post-merge facts use exactly one causal closeout path.
