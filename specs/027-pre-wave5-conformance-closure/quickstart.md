# Quickstart: Pre-Wave-5 Conformance Closure

## 1. Preflight

```bash
git branch --show-current
cat .specify/feature.json
specify check
pwsh -NoLogo -NoProfile -File .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
```

Expected branch is `027-pre-wave5-conformance-closure`; do not create 025 or
026 artifacts.

## 2. Revalidate the audit

Before the explicit test command, increment the manual build counter and align
the version to `1.27.<patch>.<build>`.

```bash
dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj \
  --configuration Release \
  --filter 'FullyQualifiedName~ConformanceAuditEvidenceTests'
```

Independently inspect exact JSON counts with `jq` and verify protected path
diffs against the Feature-024 product baseline.

## 3. Run full gates

Use one new build-counter increment for each explicit `dotnet test` command.

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
dotnet test --configuration Release --no-restore
xmllint --noout coverlet.runsettings
dotnet test --configuration Release --no-restore \
  --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
(cd tests/web-a11y && npm run test:docfx)
lynx -dump -assume_charset=UTF-8 -display_charset=UTF-8 \
  _site/docs/project-statistics.html
bash scripts/scan-agent-secrets.sh --fail-on-high
```

## 4. Decide the gate

Release Wave 5 only when all closure rows pass, decisions remain 13/34/1/0/0,
findings remain zero, protected diffs remain empty, and 025/026 remain absent.

## 5. Deliver

Align the final numbered-branch version, commit with the required Copilot
trailer, push, open the PR, converge PR-context checks and threads, merge under
explicit authority, synchronize `main`, and use one causal closeout for facts
that only become true after merge.
