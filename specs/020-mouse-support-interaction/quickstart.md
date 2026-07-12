# Quickstart: Mouse Support and Interaction Hardening

## Preconditions

```bash
git switch 020-mouse-support-interaction
specify check
pwsh -NoProfile -File .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
```

Confirm `.specify/feature.json` points to
`specs/020-mouse-support-interaction` and all feature checklists are complete.

## Proof Order

1. Create `pr-evidence.md` before runtime edits.
2. Complete compile-surface and historical-source reviews.
3. Add the bounded failing Driver parser/state matrix.
4. Implement and validate the SGR-1006 vertical slice.
5. Add failing focus, activation, coordinate, and title-drag proof.
6. Implement the smallest Core/Controls corrections.
7. Run the real app-loop matrix with enabled, disabled, and unsupported states.
8. Complete host, governance, guide, and delivery evidence.

## Runtime Boundary

Supported sessions use SGR-1006 on interactive macOS/Linux terminals or WSL.
Native Windows Console and redirected/headless sessions remain keyboard-only.
The implementation does not claim wheel, hover, touch, extra-button, X10, or
full emulator support.

## Versioned Validation

Before every `dotnet build` or `dotnet test`, increment only the manual build
counter in `Directory.Build.props`. Use `1.20.<branch-commit-count>.<build>`.

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
dotnet test tests/TuiVision.Core.Tests/ --configuration Release
dotnet test tests/TuiVision.Controls.Tests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Record exact command, version, result, counts, coverage, trigger, and proof
boundary in `pr-evidence.md`. Never commit `_site/`, generated `api/*.yml`, test
results, caches, logs, terminal captures containing secrets, or credentials.

## Completion

- Ingress, focus, activation, double-click, drag, fallback, and host rows are complete.
- Exactly one drag target is implemented and every required task has a keyboard route.
- Lastenheft is archived with `.020-mouse-support-interaction.md` suffix.
- Required checks pass, actionable threads are zero, and merge/main sync is recorded.
