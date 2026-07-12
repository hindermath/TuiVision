# Implementation Plan: Editor, Help, and Resources Hardening

**Branch**: `018-editor-help-resources-hardening` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_03_EditorHelpAndResourcesHardening.md` and Feature 004 artifacts

## Summary

Harden the existing Feature 004 editor, help, stream, and resource foundation
for Wave-3 readiness. Existing editor and viewer behavior receives coherent
integration and negative proof. Two bounded reusable gaps are closed in
`TuiVision.Serialization`: a deterministic historical-intent help-source
compiler and exact language-aware resource lookup. No Wave-3 example is ported.

## Technical Context

**Language/Version**: C# 14 on .NET 10
**Primary Dependencies**: Existing BCL, `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`; no new package
**Storage**: Local temporary files, managed streams, existing TuiVision binary archive/resource format
**Testing**: MSTest 4, focused Release tests, full Release suite, canonical Coverlet gate
**Target Platform**: Existing managed Linux, macOS, Windows/WSL targets
**Project Type**: Managed framework libraries plus deterministic tests and documentation
**Performance Goals**: Linear parsing and lookup over bounded help/resource input; no unbounded retry or recursion
**Constraints**: Feature 004 compatibility; public XML docs; atomic failure; `tv203s/` read-only; no new dependency
**Scale/Scope**: Six contract areas, one bounded line-oriented help grammar, focused Controls/Serialization proof

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: TuiVision's .NET 10/MSTest/DocFX/Playwright entry
  in Constitution v1.14.0 and repository `AGENTS.md` are binding.
- **MSL and secure coding**: C#/.NET is allowed. Parsing validates line,
  symbol, context, reference, count, and output boundaries; exceptions do not
  expose partial committed models.
- **Secure architecture**: Existing library separation remains. Text input is
  an untrusted parser boundary; managed runtime objects are validated before
  publication. File proof uses isolated temporary directories.
- **Security evidence**: NIST SSDF and CWE Top 25 are applicable. STRIDE/CIA/
  CAPEC review covers parser, path, persistence, resource-exhaustion, and
  atomic-output boundaries in feature evidence. No broad threat-model rewrite
  is necessary unless design boundaries change.
- **ASVS**: `N/A`; no web/API/HTTP/auth surface. Re-evaluate if one appears.
- **Supply chain**: Existing repository SBOM/VEX/SLSA/OpenSSF evidence remains
  applicable. Feature-specific new artifacts are `N/A` because no package,
  dependency, packaging, or provenance boundary changes.
- **AI-SBOM**: `N/A`; AI is development tooling only. Re-evaluate for delivered
  models, datasets, services, inference infrastructure, or AI components.
- **Regulation**: NIS2, CRA, EU AI Act, and DORA receive explicit screening in
  `pr-evidence.md`; no new operated service, AI runtime, financial service, or
  regulated deployment boundary is introduced.
- **Architecture governance**: A bounded runtime/parsing quality scenario and
  current architecture are sufficient. S-ADR, arc42 updates, Zero Trust, SAMM,
  BSI C3A, and BSI C5 are `N/A` unless trust, cloud, provider, deployment, or
  distributed-service boundaries change.
- **Installed presets**: `security-governance` v0.6.0,
  `architecture-governance` v0.5.0, `isaqb-architecture-governance` v0.2.0,
  `a11y-governance` v0.4.0, `cross-platform-governance` v0.2.0, and
  `agent-parity-governance` v0.3.0 all apply proportionally.
- **Cross-platform scripts**: `N/A` unless a repository script changes. No
  script is planned, so Bash/PowerShell parity is not triggered.
- **A11Y and bilingual delivery**: Keyboard help navigation, diagnostics,
  guides, contracts, evidence, and didactic comments use text-first paths and
  German-first/English-second CEFR-B2 where learner-facing.
- **DocFX trigger**: New public contracts require complete XML comments,
  `docfx docfx.json`, then `tests/web-a11y` and a text-oriented page review.
- **Statistics**: `docs/project-statistics.md` is updated using the repository
  baselines and strict chronological/final-section rules.
- **Agent parity**: Active feature/next-intake context affects all five
  maintained TuiVision surfaces. Shared guidance changes are synchronized;
  `.specify/templates/` remain unchanged unless a proven workflow correction
  emerges.
- **Security-first**: No secrets, logs, caches, generated `_site/`, generated
  API YAML, test output, or agent state will be tracked.

## Autonomous Execution Contract

**Delivery mode**: `MergeAndSync`
**Authority source**: User's explicit six-feature autonomous delivery instruction
**Evidence path**: `specs/018-editor-help-resources-hardening/pr-evidence.md`
**Representative vertical slice**: Test-first compile of two topics with a
forward reference, runtime reload, and viewer navigation using the shared model
**Convergence gates**: No material Clarify question; all checklists complete;
no Critical/High Analyze finding; Medium fixed or bounded; all tasks/evidence
complete; required remote checks green and zero actionable review threads
**Shared single-writer files**: `pr-evidence.md`, `Directory.Build.props`,
`docs/project-statistics.md`, Pflichtenheft/intake files, five agent files
**Validation triggers**: Always diff/format; focused Controls/Serialization;
full Release and coverage for shared runtime code; DocFX/A11Y for public XML/API;
no script or visible-example trigger expected
**Scope firewall**: Example, mouse, terminal, charset, broad serialization, or
dependency work becomes `FollowUpHardening` with owner and re-evaluation trigger
**Remote closeout**: Commit, push, PR, required checks, Claude/Copilot state,
GraphQL threads, authorized bounded bypass only for a sole human-approval rule,
merge commit, branch deletion, and clean synchronized local `main`

Create `pr-evidence.md` before implementation edits. Increment the manual build
counter before every `dotnet build` or `dotnet test`; align all version fields
to `1.18.<patch>.<build>` before commit and push.

## Design Decisions

### D1: Keep Feature 004 contracts authoritative

Do not refactor existing editor/file/help/resource APIs merely for consistency.
Add integration proof around them and change production code only for an
observable missing or unsafe accepted contract.

### D2: Bounded help-source grammar

Support historical-intent `.topic Symbol[=number][, ...]` declarations,
paragraph/preformatted lines, and `{visible text[:TargetSymbol]}` references.
Forward references are resolved before publication. Unlike historical warning-
and-output behavior, unresolved references are rejected to satisfy the intake's
explicit failure contract. The first declared symbol becomes the deterministic
runtime topic title. A stream overload decodes strict UTF-8 and reports invalid
byte sequences as source diagnostics rather than replacement characters.

### D3: Atomic compiler result

Compilation returns a result containing either a complete `THelpFile`, symbol
map, and no errors, or diagnostics and no help model. File persistence remains a
separate existing stream/resource operation, preventing a failed parse from
publishing partial output.

### D4: Exact language resource naming

Use `<baseKey>.<languageTag>` for language-specific resources and `<baseKey>`
for neutral fallback. Candidate order is exact tag, explicit ordered fallback
tags, then neutral. Validate tags/keys, preserve ordinal case sensitivity, and
return matched key plus value through a non-throwing lookup result.

The default compiler limits are 1 MiB of decoded source, 16,384 characters per
line, and 10,000 topics. Public options may lower or raise positive limits for a
known application; exceeding a limit is a normal compilation diagnostic.

### D5: Proof depth

Serialization tests establish compiler, lookup, persistence, and malformed-
input contracts. Controls integration tests drive editor window and help viewer
through public application-facing operations. Full coverage is required because
shared library behavior changes.

## Project Structure

### Documentation

```text
specs/018-editor-help-resources-hardening/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── hardening-contracts.md
├── checklists/
├── tasks.md
└── pr-evidence.md
```

### Source and Tests

```text
src/TuiVision.Serialization/
├── THelpSourceCompiler.cs       # bounded source-to-runtime model
├── TLocalizedResourceLookup.cs  # exact/fallback lookup result and service
├── THelpFile.cs                 # only if validation integration is required
└── TResourceFile.cs             # existing exact-key storage reused

src/TuiVision.Controls/
└── existing editor/help files   # change only if test-first proof finds a gap

tests/TuiVision.Serialization.Tests/
├── THelpSourceCompilerTests.cs
├── TLocalizedResourceLookupTests.cs
└── existing malformed-persistence suites

tests/TuiVision.Controls.Tests/
└── EditorHelpEndToEndTests.cs
```

**Structure Decision**: Keep reusable grammar and language-resource logic in
Serialization because both produce/consume persisted framework models. Keep
viewer/editor interaction proof in Controls. No new project or dependency.

## Implementation Phases

### Phase A: Evidence and baseline

Create evidence and six-area decision matrix, record historical files and
governance checkpoints, then run static inspection without a build/test.

### Phase B: Vertical compiler/runtime slice

Add failing tests for two topics, forward reference, persisted round-trip, and
viewer navigation. Implement the smallest complete compiler result and grammar.
Record divergence from historical unresolved-reference warnings.

### Phase C: Compiler hardening

Add duplicate context/symbol, malformed directive/reference, invalid number,
empty topic, encoding/string boundary, and atomic failure tests. Complete
diagnostics and validation without adding a CLI or example.

### Phase D: Resource/i18n hardening

Add exact language, ordered fallback, neutral, missing, empty value, invalid
tag/key, and case-separation tests. Implement lookup over `TResourceFile` using
existing exact keys and prove save/reload behavior.

### Phase E: Editor/help integration hardening

Build coherent temporary-file editor and persisted-help viewer scenarios from
existing components. Apply only test-demonstrated narrow fixes; otherwise mark
`UseExistingFramework`.

### Phase F: Documentation, governance, and closeout

Complete XML docs, feature evidence, docs/security applicability references,
statistics, archive/next-intake markers, and five agent contexts. Run all
triggered validation and perform authorized remote closeout.

## Post-Design Constitution Re-check

Passed. The design adds two small framework-local contracts, no dependencies,
no service/deployment boundary, and no historical-source changes. Public API
documentation and full shared-library validation are explicitly planned. No
Constitution violation requires a complexity exception.

## Complexity Tracking

No justified Constitution violations.
