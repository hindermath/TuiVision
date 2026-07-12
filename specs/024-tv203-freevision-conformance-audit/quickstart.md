# Quickstart: Run and Review the Conformance Audit

## 1. Preflight

```bash
git status --short --branch
specify check
pwsh -NoProfile -File .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
```

The branch must be `024-tv203-freevision-conformance-audit`, and
`.specify/feature.json` must reference this feature directory.

## 2. Verify the external snapshot

```bash
git -C /tmp/tuivision-fv-024-ffc03b34 rev-parse HEAD
git ls-remote https://gitlab.com/freepascal.org/fpc/source.git refs/heads/main
```

The review worktree must remain outside TuiVision. The checked-out HEAD must be
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2`; later movement of upstream `main`
does not change the accepted snapshot.

## 3. Review order

1. Read `framework-inventory.md` and confirm historical/source/public counts.
2. Read `framework-conformance-matrix.md` by domain and contract ID.
3. Check the matching paths and hashes in `freevision-source-manifest.md`.
4. Follow every drift/gap contract into `findings.md`.
5. Read `pre-wave5-gate.md` for blocking and downstream decisions.
6. Use `pr-evidence.md` for command, governance, and delivery truth.

## 4. Validate the dataset

Before each explicit `dotnet test`, increment the manual build counter once and
record that exact version in `pr-evidence.md`.

```bash
dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release --filter FullyQualifiedName~ConformanceAuditEvidenceTests
```

Then run the full Release and canonical coverage gates because the proof spans
all five framework assemblies.

## 5. Validate published evidence

```bash
git diff --check
dotnet format --verify-no-changes
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Review representative generated pages with UTF-8 `lynx`. Do not track `_site/`,
generated API YAML, logs, test results, or the external Free Vision checkout.

## 6. Interpret results

- `Aligned`, `IntentionalModernization`, and `ConsciouslyOmitted` need evidence
  but no finding.
- `BehavioralDrift` and `EvidenceGap` need exactly one finding.
- `Critical` or `High` blocks closure.
- `ProductDecision` stops autonomous behavior change.
- Empty `Core025` or `ComponentData026` sets create no feature or PR.

## 7. Completion

The audit is complete only when all inventories, decisions, relations, findings,
governance rows, validations, reviews, and scope checks pass. Feature 027 remains
the mandatory closure even when no 025 or 026 work is needed.
