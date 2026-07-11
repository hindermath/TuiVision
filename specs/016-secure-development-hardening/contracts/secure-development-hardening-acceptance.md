# Contract: Secure Development Hardening Acceptance

**Feature**: `016-secure-development-hardening`  
**Date**: 2026-07-11

## 1. Scope Contract

Feature 016 MAY change project security evidence, policy, CI, dependency automation, local security tooling, critical repository scripts, script tests, agent context, statistics, and bounded source/test code only when a concrete finding requires it.

Feature 016 MUST NOT autonomously change provider settings, credentials, legal/compliance status, commercial distribution decisions, broad architecture, public API without a critical finding, package versions without evidence, example scope, Wave-1 visuals, or `tv203s/`.

## 2. Control Inventory Contract

The assessment MUST contain exactly one row for every `#### CL-XX-NN` heading in:

- CL-01 Standards Applicability: 12 controls;
- CL-02 Secure Software Architecture: 13 controls;
- CL-03 Cryptographic Minimum Requirements: 15 controls;
- CL-04 Threat Modeling: 10 controls;
- CL-05 Supply Chain and Build Integrity: 13 controls;
- CL-06 Vulnerability Disclosure: 11 controls;
- CL-07 CRA Applicability: 12 controls;
- CL-08 Security Code Review: 13 controls;
- CL-09 AI Code Generation: 17 controls;
- CL-10 Secure Development Environment: 17 controls;
- CL-11 Data Protection Impact Assessment: 12 controls;
- CL-12 Agentic AI Sandbox: 12 controls.

The total baseline is 157 unique controls. A mechanical comparison MUST report no missing, duplicate, or unknown control ID.

## 3. Status Contract

Every control MUST use exactly one status:

- `Applicable`
- `AlreadySatisfied`
- `N/A`
- `Open`
- `FollowUp`

Every row MUST contain control ID/source, title, status, rationale, evidence path, owner, reviewer, review date, result, risk priority, residual risk, follow-up, re-evaluation trigger, and human-only flag.

`AlreadySatisfied` requires current direct evidence. `N/A` requires a factual rationale and trigger. `Open` requires owner, priority, risk, concrete action, trigger, and visible human-only status where applicable. `FollowUp` requires an explicit later boundary.

## 4. Finding and Remediation Contract

- Every finding has severity, affected paths, plausible impact, mapped controls, disposition, and acceptance condition.
- Critical/high findings are remediated and proven or block merge.
- Medium and implementation-relevant low findings are remediated or receive an accepted `Open`/`FollowUp` boundary.
- Bounded remediation is reversible, testable, repository-local, and compatible with existing behavior.
- Runtime, API, persistence, package, or architecture changes require explicit finding evidence.

## 5. Project Security Evidence Contract

The following project-wide evidence MUST be current and MUST NOT claim to be an unpopulated stub:

```text
docs/security/README.md
docs/security/control-assessment.md
docs/security/gsdb-self-assessment.md
docs/security/threat-model.md
docs/security/security-checklist.md
docs/security/arc42-security.md
docs/security/dependency-audit.md
docs/security/security-quality-scenarios.md
docs/security/asvs-verification.md
docs/security/supply-chain-evidence.md
docs/security/zero-trust-applicability.md
docs/security/samm-assessment.md
docs/security/cloud-autonomy-applicability.md
docs/security/cloud-compliance-assurance.md
docs/security/regulatory-applicability.md
docs/security/adr/README.md
```

Project evidence MUST distinguish policy, actual proof, generated-on-demand evidence, accepted residual risk, human-only decisions, and future work.

## 6. Supply-Chain Contract

- CycloneDX for .NET MUST be version-pinned in the local tool manifest.
- A clean checkout MUST be able to restore the tool and generate CycloneDX JSON from `TuiVision.sln`.
- Generated BOM files MUST remain untracked.
- The BOM MUST parse and contain non-empty component/dependency data.
- Direct and transitive packages MUST be checked for vulnerable and deprecated packages.
- Existing GitHub Action dependencies changed by this feature MUST use immutable full commit SHAs with readable version comments.
- NuGet, GitHub Actions, and npm update surfaces MUST have repository-controlled review automation or an explicit accepted follow-up.
- VEX, SLSA, Scorecard, and AI-SBOM statuses MUST include evidence and triggers without unsupported claims.

## 7. Vulnerability Disclosure Contract

Root `SECURITY.md` MUST be discoverable, German-first/English-second, text-first, and state:

- supported-version policy;
- private GitHub Security Advisory reporting path;
- information reporters should provide;
- acknowledgement and response expectations without an unapproved legal SLA;
- coordinated disclosure guidance;
- ownership/follow-up boundary.

Provider feature activation or organizational response ownership that cannot be proven in repository files remains human-only `Open`.

## 8. Rename Script Contract

Bash and PowerShell implementations MUST provide equivalent:

| Behavior | Bash | PowerShell |
|---|---|---|
| Help | `--help` | comment-based help / `Get-Help` |
| Commit-free rename | `--no-commit` | `-NoCommit` |
| Preview | `--dry-run` | `-WhatIf` |
| Explicit commit | default | default |
| Errors | non-zero, bilingual | non-zero, bilingual |

Both MUST:

- accept only a tracked `Lastenheft*.md` source;
- normalize `/` in a branch name to `-` and reject unsafe target segments;
- fail before mutation for missing, untracked, non-Markdown, or unsafe input;
- use Git-aware rename behavior;
- avoid staging or committing unrelated files;
- be idempotent when the target name is already present;
- produce equivalent filesystem, index, commit-count, exit-code, and meaning outcomes.

Contract tests MUST run in disposable temporary Git repositories.

## 9. Architecture and Regulatory Contract

- Threat evidence covers STRIDE, CIA impact, relevant CAPEC patterns, mitigations, and residual risks for local/runtime/tooling boundaries.
- arc42 and quality scenarios describe security-relevant context and runtime flows.
- S-ADR is required only for an architecturally significant security choice.
- ASVS and owned crypto are `N/A` unless their factual triggers enter scope.
- Zero Trust, BSI C3A, and BSI C5 are `N/A` while no cloud, distributed service, provider, remote identity, or deployment topology exists.
- CRA market-placement status is human-only `Open`; NIS2, DORA, EU AI Act product scope, and DPIA receive factual decisions and triggers.
- No row claims certification, legal conformity, or provider assurance.

## 10. Agent and Template Contract

If shared security/workflow guidance changes, these files MUST be reviewed and updated together:

```text
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
```

The plan context MUST be refreshed for Codex, Claude, Gemini, and Copilot. `.specify/templates/` remain unchanged unless a concrete repository-template defect is identified. Any divergence requires explicit evidence.

## 11. A11Y and Didactic Contract

- Learner-facing evidence is German-first/English-second at approximately CEFR-B2.
- Status and risk meaning does not rely on color, visual position, or pointer interaction.
- Tables have semantic headers and prose explains dense control sets.
- New or changed non-trivial logic is reviewed for didactic comment value; comments explain why, trade-off, constraint, history, or proof boundary.
- Changed `docs/security/` content triggers DocFX followed by web-A11Y validation and a text-first spot check.

## 12. Validation Contract

Final evidence MUST include:

```bash
git diff --check
dotnet format --verify-no-changes
dotnet list TuiVision.sln package --vulnerable --include-transitive
dotnet list TuiVision.sln package --deprecated --include-transitive
dotnet tool restore
# CycloneDX generation into a temporary path plus JSON validation
bash -n scripts/rename-lastenheft.sh
# PowerShell parser validation and isolated parity tests
dotnet test TuiVision.sln --configuration Release
dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
cd tests/web-a11y && npm run test:docfx
```

Repository secret scans and control-ID/evidence completeness checks MUST also pass. Before every build/test command, `Directory.Build.props` uses `1.16.<patch>.<incremented-build>`. Before commit/push it is aligned without incrementing unless another build/test ran.

## 13. Completion Contract

Completion requires:

- 157/157 control coverage and complete audit fields;
- no accepted security-document stub;
- no unresolved critical/high finding;
- no medium or implementation-relevant low gap without disposition;
- reproducible SBOM and package evidence;
- Bash/PowerShell script parity;
- all required validation green;
- all checklists complete;
- repeated Analyze actionable-clean;
- project statistics, progress marker, active context, Lastenheft archive, and PR evidence current;
- no generated, credential, cache, log, coverage, DocFX, SBOM, or historical-source output tracked.
