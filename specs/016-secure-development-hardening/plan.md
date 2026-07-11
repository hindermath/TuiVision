# Implementation Plan: Secure Development Hardening

**Branch**: `016-secure-development-hardening` | **Date**: 2026-07-11 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `./spec.md`

## Summary

Feature 016 turns TuiVision's secure-development guidance into an auditable project baseline. It assesses every stable `CL-XX-NN` control from all twelve checklists, replaces accepted security-document stubs with current project evidence, closes bounded repository-local gaps, and records larger or human-only decisions without making compliance claims.

The known implementation slices are: a 157-row control assessment, consolidated `docs/security/` evidence, a reproducible CycloneDX SBOM path, dependency and GitHub Actions supply-chain hardening, a discoverable vulnerability-reporting policy, and a safe cross-platform Lastenheft rename contract. Runtime behavior and public APIs remain unchanged unless a concrete critical or high security finding makes a bounded tested fix necessary.

## Baseline Decisions

- `Lastenheft_Secure-Development-Hardening.md` is binding and remains separate from the RL-SE self-review and GSDB intensive-review intakes.
- The review unit is each `#### CL-XX-NN` heading. The current baseline contains 157 unique controls across CL-01 through CL-12.
- Project-wide assessment evidence lives in `docs/security/control-assessment.md`; feature execution and command evidence lives in `specs/016-secure-development-hardening/pr-evidence.md`.
- The only assessment statuses are `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, and `FollowUp`.
- Small and medium reversible repository changes are in scope. Provider settings, formal compliance decisions, credentials, legal determinations, and broad architecture remain human-only or follow-up work.
- Existing runtime, public API, persistence, example, terminal, and historical Turbo Vision behavior is preserved unless a concrete security finding requires otherwise.
- Generated SBOM, scan, coverage, DocFX, and test outputs remain untracked.

## Technical Context

**Language/Version**: C# `latest` / C# 14 on .NET 10 (`net10.0`); Bash and PowerShell 7 for repository tooling

**Primary Dependencies**: Existing TuiVision projects, MSTest, Coverlet, DocFX, Playwright/axe, GitHub Actions, Gitleaks, and CycloneDX for .NET 6.2.0 as a repository-local tool. No new runtime package is planned.

**Storage**: Source-controlled Markdown, YAML, shell/PowerShell scripts, a local .NET tool manifest, and test fixtures. Generated evidence is written to temporary or ignored directories. No database, service, credential, runtime AI, or user-data store is introduced.

**Testing**: Script contract tests, Bash syntax checks, PowerShell parser/execution checks, package vulnerability/deprecation review, SBOM generation and JSON validation, `git diff --check`, `dotnet format --verify-no-changes`, full Release tests, canonical Coverlet gate, Gitleaks/agent-secret checks, and DocFX plus web-A11Y because `docs/security/` is included in DocFX.

**Target Platform**: Primary macOS development, Linux CI, PowerShell 7 parity, and Windows/WSL-compatible script semantics. Remote provider settings are evidence-only unless already repository-controlled.

**Project Type**: Multi-project .NET terminal UI framework with repository tooling, documentation, examples, tests, and GitHub Actions.

**Performance Goals**: No runtime performance change. Security checks must be deterministic and suitable for local or CI execution without retaining generated output in Git.

**Constraints**: No unsupported compliance claim, credential change, paid service, branch-protection mutation, broad architecture revision, package upgrade without a finding, new example, or edit under `tv203s/`.

**Scale/Scope**: 157 checklist controls; five runtime modules; six test projects; current workflows and critical scripts; all accepted project-wide security evidence files; five maintained agent surfaces.

## Constitution Check

*GATE: Passed before research and re-checked after design.*

### Core Gates

- **Level-2 environment**: PASS. The plan uses the `RiderProjects/TuiVision` Level-2 registry row, .NET 10 baseline, MSTest/Coverlet gates, DocFX/A11Y path, statistics rules, and maintained agent surfaces.
- **Memory-safe language**: PASS. C# is on the approved MSL list. Bash and PowerShell are repository tools and follow strict argument, error, and quoting rules.
- **Secure code generation**: PASS. NIST SSDF and CWE Top 25 guide code and script review. No query, authentication, crypto, or network implementation is planned.
- **Secure architecture**: PASS. The threat model covers terminal input, local files/resources, serialization, generated output, CI, package feeds, and agent/tool boundaries. No new trust boundary is introduced.
- **Security documents**: PASS with required updates. `docs/security/` stubs must become current project evidence; S-ADR is created only for an architecturally significant decision.
- **Standards applicability**: PASS. NIST SSDF, CWE Top 25, CAPEC, SAMM, SBOM, dependency review, and public OSS posture apply. ASVS, owned crypto, Zero Trust, BSI C3A/C5, and AI-SBOM are trigger-based `N/A`. CRA market-placement is human-only `Open`; NIS2, DORA, EU AI Act product scope, and DPIA are recorded without legal claims.
- **Supply chain**: PASS with remediation. Pin repository Actions to immutable SHAs, add dependency-update configuration, generate CycloneDX through a pinned local tool, document VEX/SLSA/Scorecard state, and keep generated outputs untracked.
- **Security-first**: PASS. Credentials, logs, local history, caches, generated scan data, and agent state are not tracked.
- **A11Y and inclusion**: PASS. Security evidence and script help are text-first, semantic, German-first/English-second at CEFR-B2 where learner-facing. DocFX and web-A11Y run because documentation changes.
- **Statistics**: PASS. `docs/project-statistics.md` is updated at milestones and completion using 80 and 125 lines/workday baselines.
- **Agent parity**: PASS. The multi-agent context refresh runs after planning. Shared policy changes require all five maintained surfaces together.
- **Cross-platform governance**: PASS with remediation. `rename-lastenheft` receives equivalent Bash/PowerShell options, help, validation, exit semantics, dry-run/WhatIf behavior, commit isolation, documentation, and tests.

### Preset Matrix

| Preset | Version | Plan application |
|---|---:|---|
| `security-governance` | 0.6.0 | SSDF, CWE, dependencies, SBOM, VEX, SLSA, Scorecard, AI-SBOM, regulatory screening |
| `architecture-governance` | 0.5.0 | STRIDE/CIA/CAPEC, arc42, S-ADR, SAMM, Zero Trust, BSI C3A/C5 |
| `isaqb-architecture-governance` | 0.2.0 | Context, runtime boundaries, quality scenarios, decisions, risks, debt |
| `a11y-governance` | 0.4.0 | Text-first evidence, CEFR-B2, DocFX/web-A11Y, didactic-comment review |
| `cross-platform-governance` | 0.2.0 | Bash/PowerShell parity, help, dry-run/WhatIf, exit behavior, tests, man page |
| `agent-parity-governance` | 0.3.0 | Five maintained guidance surfaces and `.specify/templates/` impact review |

### Post-Design Gate Review

PASS. Research and design resolve control granularity, evidence ownership, SBOM tooling, CI boundaries, script behavior, validation, and human-only decisions. No `NEEDS CLARIFICATION` remains and no constitution exception is needed.

## Project Structure

### Feature Artifacts

```text
specs/016-secure-development-hardening/
|-- spec.md
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
|-- pr-evidence.md                 # implementation evidence
|-- checklists/
|   |-- requirements.md
|   |-- plan-quality.md
|   |-- security-applicability.md
|   |-- implementation-readiness.md
|   `-- human-only-boundaries.md
|-- contracts/
|   `-- secure-development-hardening-acceptance.md
`-- tasks.md                       # generated by /speckit-tasks
```

### Repository Surfaces

```text
.config/dotnet-tools.json          # pinned CycloneDX local tool
.github/dependabot.yml             # NuGet, Actions, and npm update review
.github/workflows/                 # immutable action pins; bounded security checks
docs/security/                     # project-wide control and security evidence
docs/man/rename-lastenheft.1       # cross-platform CLI contract
scripts/rename-lastenheft.sh
scripts/rename-lastenheft.ps1
tests/scripts/                     # isolated Git-repository contract tests
src/                               # review; change only for concrete bounded finding
tests/TuiVision.*.Tests/           # existing runtime proof
SECURITY.md                        # disclosure path
Directory.Build.props
docs/project-statistics.md
Pflichtenheft.md
```

**Structure Decision**: Reuse existing project and evidence boundaries. Add no production project, service, database, runtime package, example, or historical-source edit.

## Phase 0: Research

[research.md](./research.md) fixes the control granularity, status semantics, SBOM format/tool, supply-chain remediation, script contract, architecture and regulatory applicability, A11Y path, historical-source boundary, and validation strategy.

## Phase 1: Design and Contracts

[data-model.md](./data-model.md) defines assessment, finding, evidence, follow-up, validation, and script-contract records. [contracts/secure-development-hardening-acceptance.md](./contracts/secure-development-hardening-acceptance.md) defines observable acceptance. [quickstart.md](./quickstart.md) provides the implementation and verification sequence.

## Phase 2: Task Planning Approach

1. Establish `pr-evidence.md`, control inventory, mandatory fields, ownership defaults, and generated-output boundaries.
2. Classify all 157 `CL-XX-NN` controls exactly once and validate coverage mechanically.
3. Review runtime, serialization, file/resource, terminal, event/command, documentation, CI, script, and agent trust boundaries against SSDF/CWE/STRIDE/CAPEC.
4. Replace project-wide security-document stubs with current evidence and create missing cloud, regulatory, disclosure, and control-matrix surfaces.
5. Implement the pinned CycloneDX local-tool path, dependency update configuration, immutable workflow pins, and bounded supply-chain checks.
6. Harden both Lastenheft rename scripts and execute isolated parity tests before archiving this feature's binding Lastenheft.
7. Route broad or human-only findings to complete `Open`/`FollowUp` rows; block on unresolved critical/high risk.
8. Complete agent context, progress markers, archive state, and final statistics before generated-document validation so DocFX/web-A11Y sees the accepted documentation state.
9. Run formatting, package, SBOM, script, secret, full Release, coverage, DocFX, web-A11Y, artifact, parity, and PR-evidence validation in dependency order; then run repeated Analyze until actionable-clean.

Shared files such as `control-assessment.md`, `pr-evidence.md`, workflows, agent guidance, statistics, version metadata, and progress markers must be edited serially.

## Complexity Tracking

No constitution violations are introduced.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|---|---|---|
| None | N/A | N/A |
