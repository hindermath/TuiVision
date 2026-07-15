# Quickstart: Pre-Wave-5 and Wave-6 Conformance Closure

## 1. Preflight

```bash
git branch --show-current
cat .specify/feature.json
specify check
pwsh -NoProfile -File .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
```

Expected branch is `028-pre-wave5-wave6-conformance-closure`, and the feature
directory must be `specs/028-pre-wave5-wave6-conformance-closure`.

## 2. Establish the test-first closure validator

Create `pr-evidence.md`, then add the validator before its JSON input. Increment
the manual build counter once and observe the missing-dataset failure:

```bash
dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj \
  --configuration Release \
  --no-restore \
  --filter 'FullyQualifiedName~ConformanceClosureEvidenceTests'
```

After `closure-evidence.json` contains all required rows, increment once more
and rerun the same command. The completed validator must reject malformed,
duplicate, unknown, incomplete, and non-reciprocal data.

## 3. Execute all real-path proof families

Increment the build counter once and run one batched solution-level Release
filter containing every proof reference recorded by the seven slices:

```bash
dotnet test TuiVision.sln \
  --configuration Release \
  --no-restore \
  --filter '<all R-028 proof method filters from closure-evidence.json>'
```

The evidence must record exact project totals and confirm that each named
method executed; a zero-test project or helper-only pass cannot close a slice.

## 4. Run full local gates

Use a new build-counter increment before each explicit `dotnet test` command.

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
xmllint --noout coverlet.runsettings
dotnet test TuiVision.sln --configuration Release --no-restore
dotnet test TuiVision.sln --configuration Release --no-restore \
  --collect:'XPlat Code Coverage' \
  --settings coverlet.runsettings
docfx docfx.json
(cd tests/web-a11y && npm run test:docfx)
lynx -dump -assume_charset=UTF-8 -display_charset=UTF-8 \
  _site/docs/project-statistics.html
bash scripts/scan-agent-secrets.sh --fail-on-high .
```

Read every generated Cobertura report outside Git and record the exact line
rate for the five mandatory assemblies. Remove or ignore all test and DocFX
output before staging.

## 5. Decide the gate

Closure passes only when all thirteen findings, seven slices, thirteen baseline
consumer rows, governance decisions, local gates, and protected-path scans pass.
Set only `ReadyForTerminalGuiAudit`; keep both Waves
`BlockedPendingTerminalGuiAudit` and name Feature 029.

## 6. Validate the exact reviewed head

After PR checks complete, construct provider-neutral evidence in a temporary
file and run both validators without committing it:

```bash
bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.sh \
  --requirements specs/028-pre-wave5-wave6-conformance-closure/autonomous-gate-requirements.json \
  --evidence /tmp/028-autonomous-gate-evidence.json \
  --head '<full-reviewed-head>'

pwsh -NoProfile -File \
  .specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.ps1 \
  -Requirements specs/028-pre-wave5-wave6-conformance-closure/autonomous-gate-requirements.json \
  -Evidence /tmp/028-autonomous-gate-evidence.json \
  -Head '<full-reviewed-head>'
```

## 7. Deliver and close out

Merge only after technical convergence and the authorized Human Approval
boundary. Delete the feature branch, synchronize local `main`, record terminal
facts once in `delivery-closeout.md`, run the retrospective, and publish a
preset patch only for a deterministic provider-neutral improvement.
