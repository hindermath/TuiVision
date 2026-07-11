# Research: Secure Development Hardening

**Feature**: `016-secure-development-hardening`  
**Date**: 2026-07-11

## R01 - Control Granularity

**Decision**: Treat every `#### CL-XX-NN` heading in the twelve secure-development checklists as one stable control. The baseline contains 157 unique IDs.

**Rationale**: This is the smallest mechanically complete unit that satisfies the Lastenheft without turning examples, glossary text, or explanatory bullets into duplicate controls.

**Alternatives considered**: One row per checklist was too coarse; one row per Markdown bullet was unstable and would classify explanatory text as normative controls.

## R02 - Assessment Status and Audit Fields

**Decision**: Use exactly `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp`. Every row carries control ID/source, rationale, evidence, owner, reviewer, date, result, residual risk, follow-up, re-evaluation trigger, and human-only flag.

**Rationale**: The vocabulary separates current evidence from required work, factual non-applicability, unresolved human decisions, and intentionally deferred technical scope.

**Alternatives considered**: `Pass/Fail` could not express trigger-based non-applicability or human-only decisions. Empty fields for `N/A` were rejected because they hide re-evaluation boundaries.

## R03 - Evidence Split

**Decision**: Store durable project control results in `docs/security/control-assessment.md`; store feature commands, task completion, failures, and PR proof in `specs/016-secure-development-hardening/pr-evidence.md`.

**Rationale**: Project evidence must outlive feature 016, while implementation logs should remain traceable to one branch and PR.

**Alternatives considered**: A single feature file would make future reviews depend on an old feature directory. Putting command transcripts in project documentation would create noisy and quickly stale evidence.

## R04 - Bounded Remediation

**Decision**: Implement reversible repository-local code, test, CI, script, documentation, and evidence changes of small or medium size. Use `Open` or `FollowUp` for provider settings, organization policy, credentials, formal legal decisions, commercial release status, broad architecture, or irreversible actions.

**Rationale**: This preserves autonomy without silently accepting material risk or expanding one hardening run into organizational change.

**Alternatives considered**: Assessment-only would leave known local weaknesses. Unlimited remediation would make scope and acceptance unpredictable.

## R05 - SBOM Format and Tool

**Decision**: Add CycloneDX for .NET 6.2.0 to a repository-local .NET tool manifest and generate CycloneDX JSON from `TuiVision.sln` into a temporary or ignored directory. Validate JSON structure and non-empty components; do not commit generated BOM files.

**Rationale**: CycloneDX for .NET supports .NET 10, aggregates solution dependencies, emits a standard machine-readable format, and can be restored reproducibly from a clean checkout. Version 6.2.0 is the current package version observed during planning.

**Alternatives considered**: A global unpinned tool is not reproducible. Hand-written JSON would not prove the resolved dependency graph. Committing generated BOMs would create stale output and conflict with repository policy.

## R06 - VEX, SLSA, and Scorecard

**Decision**: VEX is `N/A` while no known shipped vulnerability exists, with re-evaluation on any vulnerable-package finding. SLSA remains `FollowUp` until a release artifact pipeline produces provenance. OpenSSF Scorecard applies to the public repository; record current posture and use a human/provider follow-up for publication or settings not controlled by repository files.

**Rationale**: These statuses reflect real triggers without claiming a release artefact or external service state that feature 016 does not produce.

**Alternatives considered**: Fabricating VEX/provenance files would overstate evidence. Enabling provider-level publication autonomously would cross the agreed external-action boundary.

## R07 - GitHub Actions and Dependency Posture

**Decision**: Pin existing workflow actions to immutable commit SHAs with readable version comments, add Dependabot review for NuGet, GitHub Actions, and the DocFX npm surface, and add bounded repository-controlled dependency/SBOM checks where they do not require credentials.

**Rationale**: Mutable action tags and absent update automation are concrete supply-chain gaps. Repository-local configuration is reversible and reviewable.

**Alternatives considered**: Leaving tags mutable weakens build integrity. Enabling repository vulnerability alerts or changing rulesets is a provider-setting action and remains human-only `Open`.

## R08 - Vulnerability Disclosure

**Decision**: Add a bilingual root `SECURITY.md` with supported-version statement, private reporting route through GitHub Security Advisories, response expectations, no-public-disclosure guidance, and scope boundaries.

**Rationale**: The public repository currently has no discoverable disclosure policy. A source-controlled policy is bounded and directly satisfies the disclosure control family.

**Alternatives considered**: Publishing an email address would introduce personal-data and ownership maintenance. Enabling or configuring provider features beyond the documented route remains human-owned.

## R09 - Rename Script Contract

**Decision**: Preserve explicit commit as the default, add Bash `--no-commit`, `--dry-run`, and `--help`, and equivalent PowerShell `-NoCommit`, `-WhatIf`, and comment-based help. Restrict source input to a tracked `Lastenheft*.md`, normalize branch `/` to `-`, reject unsafe target segments, use `git mv`, and isolate explicit commits to the renamed paths.

**Rationale**: The current scripts force a commit and can include unrelated staged changes. Safe path handling, dry-run semantics, and commit isolation close concrete CWE-22/CWE-78-style script boundaries without changing application runtime.

**Alternatives considered**: Changing the default to no-commit would break existing automation. Plain `mv` would lose Git intent. Committing the whole index would retain the current unrelated-change risk.

## R10 - Script Tests and Documentation

**Decision**: Add isolated temporary-Git-repository contract tests covering help, missing/invalid input, dry run, no-commit, explicit commit, unrelated staged content, normalized branch names, idempotence, and Bash/PowerShell result parity. Add an actual bilingual man-page source.

**Rationale**: Script parity is behavioral, not textual. Temporary repositories prove index and commit behavior without touching the working repository.

**Alternatives considered**: Manual happy-path checks would miss staging and exit-code regressions. Pester-only tests would not prove Bash parity.

## R11 - Security Documentation Consolidation

**Decision**: Replace every accepted `Stub` marker in `docs/security/` with current project-wide content. Maintain threat model, arc42 concepts, quality scenarios, checklist/control matrix, dependency audit, supply chain, ASVS, Zero Trust, SAMM, cloud autonomy, cloud compliance, regulatory applicability, and S-ADR index.

**Rationale**: Existing feature-local notes are useful but do not constitute a project baseline while their files still claim to be unpopulated.

**Alternatives considered**: Keeping stubs and linking feature evidence violates SC-004. Copying templates unchanged would create non-evidence.

## R12 - Architecture Applicability

**Decision**: STRIDE/CIA/CAPEC, arc42, quality scenarios, risks, and SAMM apply. Create an S-ADR only for an architecturally significant security decision. ASVS, owned cryptography, Zero Trust, BSI C3A, and BSI C5 are `N/A` for the current local terminal framework, with explicit scope-change triggers.

**Rationale**: Local file, terminal, serialization, package, CI, and agent boundaries are real architecture concerns; cloud and service controls are not currently factual.

**Alternatives considered**: Blanket `N/A` would miss local trust boundaries. Blanket applicability would make unsupported cloud and authentication claims.

## R13 - Regulatory and Privacy Boundary

**Decision**: Record CRA market-placement as human-only `Open`; NIS2 and DORA as factual `N/A` for current operation; EU AI Act product scope and AI-SBOM as `N/A` while AI is development tooling; DPIA as `N/A` while no systematic personal-data processing is introduced.

**Rationale**: Technical readiness may be improved, but legal role, market placement, and regulated-entity classification require human authority.

**Alternatives considered**: A compliance assertion would exceed evidence. Omitting the controls would violate the governance matrix.

## R14 - Runtime and Historical Scope

**Decision**: Perform a security-oriented review of current C# boundaries, but plan no executable behavior change. `tv203s/` is `N/A` as an implementation reference because feature 016 does not port or alter historical behavior; it remains read-only if a finding requires context.

**Rationale**: This feature reviews security across framework surfaces, but existing tests and prior hardening already provide broad behavior proof. Changes require a concrete finding.

**Alternatives considered**: Broad proactive refactoring would exceed the bounded policy. Ignoring source entirely would make SSDF/CWE review superficial.

## R15 - A11Y and Documentation Validation

**Decision**: Use semantic Markdown, explicit text statuses, German-first/English-second CEFR-B2 explanations, and fenced language tags. Because `docs/security/**/*.md` is included by DocFX, run DocFX followed by the Playwright/axe web-A11Y suite and perform a text-first spot check.

**Rationale**: Security evidence is learner-facing and part of generated documentation. Accessibility cannot be inferred from Markdown source alone.

**Alternatives considered**: A source-only review would miss generated navigation and HTML regressions.

## R16 - Validation Scale

**Decision**: Run package vulnerability/deprecation checks, SBOM generation/validation, script contract tests, secret scans, formatting, full Release tests, canonical per-assembly coverage, DocFX, and web-A11Y. Increment the manual build counter before every build/test command.

**Rationale**: Feature 016 is project-wide and modifies CI/tooling/documentation, so narrow validation would not satisfy the accepted success criteria.

**Alternatives considered**: Targeted tests alone would not prove repository-wide baseline or coverage gates.
