# Implementation Plan: TV203 and Free Vision Conformance Audit

**Branch**: `024-tv203-freevision-conformance-audit` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_08_TV203-FreeVision-Conformance-Audit.md` and the accepted feature specification

## Summary

Feature 024 delivers a complete, evidence-only framework conformance audit
before Wave 5. Borland documentation and `tv203s/` remain the historical
authority. Free Vision is read from one pinned official FPC commit outside the
repository and provides a separate secondary relation. A canonical structured
audit dataset drives bilingual Markdown evidence and one test-only completeness
suite. No product behavior, public API, dependency, example, or historical
source changes.

## Technical Context

**Language/Version**: C# / .NET 10 for durable test-only validation; Markdown and JSON for evidence
**Primary Dependencies**: existing framework assemblies, MSTest, `System.Text.Json`, Git, official pinned FPC source checkout
**Storage**: repository-owned JSON and Markdown evidence; external Free Vision worktree under `/tmp`, never tracked
**Testing**: new audit-evidence MSTests plus existing Release, coverage, DocFX, Playwright/Axe, and text-browser gates as triggered
**Target Platform**: deterministic managed validation on macOS, Linux, and Windows/WSL
**Project Type**: C#/.NET library repository with evidence-only audit feature
**Performance Goals**: complete validation within the normal focused test invocation; no runtime performance target
**Constraints**: zero product-runtime/API/dependency/example change; 151 historical rows; current maintained source inventory; pinned Free Vision commit; DE-first/EN-second CEFR-B2 evidence
**Scale/Scope**: 16 domains, 151 historical rows, 119 current production source files at planning baseline, five framework assemblies, all exported public types discovered at validation time

## Constitution Check

*GATE: Passed before research and rechecked after design.*

| Gate | Decision and evidence plan |
|---|---|
| Level-2 environment | PASS: TuiVision C#/.NET 10 registry context, repository build/test, DocFX/A11Y, statistics, and agent surfaces remain binding. |
| Memory-safe language | PASS: C# is allow-listed; C/C++ and Object Pascal are read-only historical evidence. |
| Secure code generation | PASS: the only planned executable change is bounded test-only JSON/evidence validation with explicit malformed-input rejection. |
| Secure architecture | PASS: no trust boundary, deployment, service, identity, network, cloud, or runtime surface changes. |
| Security documentation | PASS: governance applicability lives in `pr-evidence.md`; no new threat model, S-ADR, or arc42 security concept is triggered. |
| NIST SSDF / CWE Top 25 | Applicable proportionally: source provenance, scope firewall, review, and durable validation evidence are recorded. |
| OWASP ASVS | `N/A`: no web/API/HTTP/authentication-bearing service; re-evaluate if that scope appears. |
| SBOM / VEX / SLSA / OpenSSF | `N/A` for new evidence: no package or distributable artifact; existing repository supply-chain checks still run remotely. |
| AI-SBOM | `N/A`: AI remains development tooling; no model, dataset, AI service, inference infrastructure, or product AI. |
| STRIDE / CIA / CAPEC / Zero Trust | `N/A` for new threat evidence: no runtime data flow or trust boundary changes; re-evaluate on executable or external-flow scope. |
| S-ADR / arc42 / SAMM | `N/A` for new documents: no architecture decision or maturity-boundary change; audit findings may recommend later evidence. |
| BSI C3A / BSI C5 | `N/A`: no cloud service, provider dependency, cloud autonomy, or cloud assurance boundary. |
| NIS2 / CRA / EU AI Act / DORA | Screened `N/A` for feature-specific evidence; no operated regulated service, product AI, financial service, or new product-distribution boundary. |
| A11Y and inclusion | Applicable: human-readable evidence is bilingual, semantic, text-first, and DocFX/Axe/Lynx reviewed where published. |
| Cross-platform governance | `N/A` for script parity because no script is added; the MSTest proof is platform-neutral. |
| Agent parity | Applicable when active feature context changes; all five maintained surfaces are updated together. |
| Preset matrix | Six base presets at versions in the spec plus optional `autonomous-run-governance` v0.1.0; resolved matrix, not hard-coded count, is evidence. |
| Secret and generated output | PASS: no external checkout, credentials, `_site/`, API YAML, logs, or test output is tracked. |
| Versioning | PASS: `1.24.<patch>.<build>`; one manual build increment per explicit build/test invocation. |
| Statistics | Applicable: chronological Feature-024 entry and final `Gesamtstatistik` refresh at implementation completion. |

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: user approval of the complete 024–027 autonomous campaign and its listed plan
**Evidence path**: `specs/024-tv203-freevision-conformance-audit/pr-evidence.md`
**Representative vertical slice**: domain 2 event/command/dispatch, covering historical rows, one current-source group, exported contracts, pinned Free Vision comparison, one decision, and test-enforced dataset completeness before all other domains
**Convergence gates**: no material Clarify question; all requirements and plan-review checklists disposed; no Critical/High Analyze finding; all Medium findings fixed or accepted; all tasks and triggered validation complete; all required remote checks green and no actionable thread
**Shared single-writer files**: `conformance-audit.json`, human evidence tables, `pr-evidence.md`, `Directory.Build.props`, `docs/project-statistics.md`, Pflichtenheft/order markers, Lastenheft archive, and five agent surfaces
**Validation triggers**: always static/diff/secret checks; targeted test-only audit suite; full Release and coverage because the proof spans all five assemblies; DocFX/A11Y/Lynx for published docs/statistics; no script or visual-UI gate
**Scope firewall**: product defects become findings with one downstream disposition; they are never implemented in 024
**Remote closeout**: PR-context gates, Claude/Copilot truth, GraphQL threads, approved narrow human-approval bypass only when all actionable gates are green, merge commit, branch deletion, local `main` sync; causal evidence-only closeout only if a terminal fact cannot be recorded without self-invalidation

## Audit Architecture

### Canonical data and readable views

`conformance-audit.json` is the structured source for deterministic validation.
The required Markdown inventory, conformance matrix, source manifest, findings,
and gate documents remain the review surfaces. IDs and counts must agree across
both representations. Markdown may summarize repeated rows, but the JSON data
cannot omit them.

### Durable validation

`ConformanceAuditEvidenceTests` extends the existing Drivers test project because
that project already owns M-07 ledger completeness and repository-root
resolution. Test-only project references to Core, Controls, Serialization,
Compatibility, and Drivers allow reflection over exported types without a new
package or project. The suite validates JSON structure, current filesystem
inventory, historical ledger coverage, assembly exports, decision vocabularies,
Free Vision relations, and one-to-one finding links.

### Comparison model

Each contract is one independently reviewable behavioral responsibility, not
one method and not one entire module. Historical and modern inventory ownership
is unique; several inventory items may support one contract and one inventory
item may be referenced by several contracts without duplicating ownership.

### External-source boundary

The comparison worktree is `/tmp/tuivision-fv-024-ffc03b34` at commit
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2`. Only source paths, short identifiers,
behavioral summaries, commit metadata, and checksums enter evidence. No external
source content enters Git.

## Project Structure

### Feature documentation and data

```text
specs/024-tv203-freevision-conformance-audit/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── conformance-audit.json
├── framework-inventory.md
├── framework-conformance-matrix.md
├── freevision-source-manifest.md
├── findings.md
├── pre-wave5-gate.md
├── pr-evidence.md
├── contracts/
│   └── conformance-audit-acceptance.md
└── checklists/
```

### Test-only validation surface

```text
tests/TuiVision.Drivers.Tests/
├── TuiVision.Drivers.Tests.csproj
├── Phase7DriverTestContext.cs
└── ConformanceAuditEvidenceTests.cs
```

### Read-only comparison surfaces

```text
TVDocs/
tv203s/
TVDEMOS/
/tmp/tuivision-fv-024-ffc03b34/packages/fv/
```

**Structure Decision**: Keep all delivered audit state inside the feature
directory and reuse the existing M-07 evidence-test owner. No production module,
new project, or third-party source directory is added.

## Delivery Phases

1. Establish `pr-evidence.md`, external source manifest, baseline hashes, and the canonical empty-but-schema-complete dataset.
2. Add a red test-only completeness slice for event/command/dispatch, then fill that domain end to end.
3. Inventory all 151 historical rows, all maintained source files, and all exported public types with unique ownership.
4. Review domains 1–8 against Borland/`tv203s`, current TuiVision behavior, tests, and pinned Free Vision.
5. Review domains 9–16 with the same contract and proof standard.
6. Classify all decisions, create only required findings, and assign exactly one downstream disposition.
7. Reconcile JSON and all Markdown evidence, governance applicability, scope diffs, and the pre-Wave-5 gate.
8. Update active context, statistics, Pflichtenheft/order state, and archive the Lastenheft after accepted audit completion.
9. Run static, targeted, full Release, coverage, DocFX/A11Y/Lynx, secret, generated-output, and remote validation.
10. Merge and synchronize, then run the autonomous retrospective and create findings-driven downstream intake only from final evidence.

## Test Strategy

- The first audit test fails on an absent dataset and then passes for one full
  event/dispatch slice before broad population.
- Filesystem enumeration proves the historical and modern source baselines
  dynamically, so later additions cannot disappear from the audit silently.
- Reflection proves exported public types from all five assemblies; the test
  compares stable full names rather than relying on source regex alone.
- Closed vocabularies and JSON parsing fail explicitly on unknown, duplicate,
  missing, or malformed values.
- Finding tests require one finding for each drift/gap contract and forbid
  findings for aligned, modernized, or consciously omitted contracts.
- The targeted audit test invocation comes first. Because the proof project now
  references all five assemblies and claims framework-wide completeness, full
  Release and canonical coverage follow.
- No existing behavior test is weakened or rewritten to make the audit pass.

## Governance and Evidence

The feature-owned `pr-evidence.md` records every run gate, exact command,
decision count, domain count, inventory count, finding count, governance
checkpoint, skipped trigger, review state, and residual risk. Existing generic
security documents are referenced rather than changed unless the actual audit
discovers a documentation inconsistency. The PowerShell homogeneity helper
`PropertyNotFoundException` observed during intake preparation is a separate
portable preset observation and never a TuiVision conformance finding.

## Post-Design Constitution Re-check

All pre-research gates still pass. The structured dataset and test-only project
references add durable proof without production behavior, new packages, scripts,
or external source. DocFX/A11Y and full test/coverage triggers are explicitly
planned. No constitution exception is required.

## Complexity Tracking

No constitution violation is accepted. Supplemental JSON and one test file are
the smallest durable solution that can prove all inventory and relationship
constraints across platforms without introducing a new parser dependency,
project, or Bash/PowerShell parity surface.
