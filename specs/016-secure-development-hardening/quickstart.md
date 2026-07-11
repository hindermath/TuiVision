# Quickstart: Secure Development Hardening

**Feature**: `016-secure-development-hardening`  
**Branch**: `016-secure-development-hardening`

This quickstart is the execution order for implementation. Commands that build or test require the repository version to be aligned first.

## 1. Preflight

```bash
git branch --show-current
cat .specify/feature.json
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
git status --short
```

Expected branch is `016-secure-development-hardening`; feature directory is `specs/016-secure-development-hardening`.

Read the binding Lastenheft, Constitution, `AGENTS.md`, all feature artifacts, the secure-development guideline, the checklist collection, and all twelve checklists before classification.

## 2. Establish Evidence

Create `pr-evidence.md` before implementation changes. Record:

- preflight and tool versions;
- the 157-control source inventory;
- finding/remediation ledger;
- governance applicability;
- validation runs and retained-output rules;
- human-only and follow-up boundaries.

Create `docs/security/control-assessment.md` with one complete row for every source ID.

Mechanical source inventory:

```bash
rg -o '^#### (CL-[0-9]{2}-[0-9]{2})' \
  docs/secure-development/checklisten/CL_*.md
```

Acceptance: 157 unique IDs, no duplicate, missing, or unknown assessment row.

## 3. Assess and Triage

Review in this order:

1. CL-01 standards applicability;
2. CL-02 architecture and CL-04 threat modeling;
3. CL-08 code review across input, event, terminal, serialization, file/resource, and output boundaries;
4. CL-03 cryptography and CL-11 privacy applicability;
5. CL-05 supply chain and CL-06 disclosure;
6. CL-07 regulatory applicability;
7. CL-09 AI code generation, CL-10 development environment, and CL-12 agent sandbox.

For every finding, record severity and disposition before implementation. Stop for credentials, legal decisions, irreversible provider action, scope impossibility, or an unremediated critical risk.

## 4. Consolidate Security Evidence

Replace accepted stubs with project-wide evidence. Keep each document German-first/English-second, semantic, and explicit about proof limits.

Required durable surfaces are listed in the acceptance contract. Use an S-ADR only when the review identifies an architecturally significant security decision.

## 5. Supply-Chain Baseline

Restore and review packages:

```bash
dotnet restore TuiVision.sln
dotnet list TuiVision.sln package --vulnerable --include-transitive
dotnet list TuiVision.sln package --deprecated --include-transitive
dotnet list TuiVision.sln package --outdated --include-transitive
dotnet tool restore
```

Generate the BOM into a temporary directory:

```bash
sbom_dir="$(mktemp -d)"
dotnet tool run dotnet-CycloneDX TuiVision.sln \
  --output "$sbom_dir" \
  --json \
  --spec-version 1.7
jq -e '.bomFormat == "CycloneDX" and (.components | length > 0)' \
  "$sbom_dir/bom.json"
rm -rf "$sbom_dir"
```

Use the exact filename emitted by the pinned tool if it differs. Record package counts, tool version, format, result, and output deletion in `pr-evidence.md`.

Review all workflow `uses:` entries. Pin changed action dependencies to full commit SHAs and retain a version comment. Do not change repository rulesets, vulnerability-alert settings, secrets, or Scorecard publication without a human-owned decision.

## 6. Rename Script Hardening

Required behavior:

```bash
bash scripts/rename-lastenheft.sh --help
bash scripts/rename-lastenheft.sh --dry-run <file> <branch>
bash scripts/rename-lastenheft.sh --no-commit <file> <branch>
```

```powershell
Get-Help ./scripts/rename-lastenheft.ps1 -Full
./scripts/rename-lastenheft.ps1 -File <file> -BranchName <branch> -WhatIf
./scripts/rename-lastenheft.ps1 -File <file> -BranchName <branch> -NoCommit
```

Run only against disposable repositories until contract tests pass. Verify unrelated staged content remains staged and absent from the rename commit.

## 7. Versioning

Before each build or test command:

1. compute the expected feature commit patch;
2. set `Version`, `AssemblyVersion`, and `FileVersion` to `1.16.<patch>.<build>`;
3. increment only the manual build counter;
4. keep all three fields identical.

Before commit or push, align the patch to the next feature commit without increasing build unless another build/test ran.

## 8. Validation

Run non-build checks first:

```bash
git diff --check
bash -n scripts/rename-lastenheft.sh
pwsh -NoLogo -NoProfile -Command \
  '[void][System.Management.Automation.Language.Parser]::ParseFile("scripts/rename-lastenheft.ps1", [ref]$null, [ref]$null)'
bash tests/scripts/rename-lastenheft-tests.sh
bash scripts/scan-agent-secrets.sh
dotnet format --verify-no-changes
```

Run full Release tests after a version increment:

```bash
dotnet test TuiVision.sln --configuration Release
```

Run the canonical coverage gate after another version increment:

```bash
xmllint --noout coverlet.runsettings
dotnet test TuiVision.sln --configuration Release \
  --collect:"XPlat Code Coverage" \
  --settings coverlet.runsettings
```

Verify each required assembly reaches at least 70% line coverage using the repository's canonical aggregation method.

Because `docs/security/` changes, run after another version increment if the DocFX command builds projects:

```bash
docfx docfx.json
cd tests/web-a11y
npm install
npx playwright install chromium
npm run test:docfx
```

Perform a representative text-first review of generated security pages. Remove `_site/`, generated API YAML, test results, coverage, SBOM output, and caches from the working tree.

## 9. Final Audit

Verify:

```bash
git diff --check
git status --short
git diff --name-only -- tv203s/
git ls-files | rg '(^|/)(_site|TestResults|coverage|artifacts|sbom)(/|$)|bom\.json$'
```

Confirm:

- 157/157 controls and complete fields;
- zero accepted stubs;
- zero unresolved critical/high findings;
- explicit medium/low dispositions;
- all six presets and all trigger-based standards represented;
- five agent surfaces synchronized or unchanged with evidence;
- `.specify/templates/` impact recorded;
- statistics, next-step marker, active context, and archived Lastenheft current;
- repeated Analyze has no actionable finding.

## 10. Remote Delivery

After local implementation and Analyze convergence:

1. align version for the final commit;
2. commit and push the feature branch;
3. create the PR from `pr-evidence.md`;
4. wait for CI and automated reviews;
5. address actionable findings and repeat validation/Analyze as needed;
6. merge only when required checks pass and no critical/high risk remains;
7. switch to `main`, pull with fast-forward only, and verify a clean synchronized worktree.

Provider settings and formal legal/compliance approvals remain outside this remote delivery sequence.
