# Implementation Plan: Wave-3 Visual Component Porting

**Branch**: `019-wave3-visual-component-porting` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_Wave3-Visual-Component-Porting.md` and the accepted Feature-018 baseline

## Summary

Create five normal .NET terminal applications: `BHelp`, `HelpDemo`, `I18n`,
`TvEdit`, and `TvHc`. Each application uses existing TuiVision framework
components to expose a visible main composition, real status line, and
keyboard-reachable bilingual description. `TvEdit` is the test-first vertical
slice. Primary proof drives the real app loop and combines concrete state,
view-tree identity, and rendered buffer/cell assertions. Historical sources are
read-only; controlled fixtures and test-temp ownership protect file and compiler flows.

## Technical Context

**Language/Version**: C# 14 on .NET 10
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, and `TuiVision.Drivers.Console`; no new packages
**Storage**: Embedded/source-controlled learning content and test-owned temporary files only
**Testing**: MSTest 4, in-process app-loop smoke tests, Coverlet, DocFX, Playwright with axe
**Target Platform**: macOS/Linux terminals plus Windows/WSL compatibility through existing CI
**Project Type**: Multi-project terminal UI framework with executable learning examples
**Performance Goals**: No new throughput target; deterministic first frame and smoke completion within existing test timeouts
**Constraints**: Keyboard-first, controlled I/O, no new dependencies, no `tv203s/` edits, no mouse requirement, no Wave-4 or broad framework scope
**Scale/Scope**: Five projects, five primary smoke suites, one shared presentation helper, five guides, one README/index, feature/governance evidence

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: TuiVision's .NET 10, MSTest, coverage, DocFX/A11Y,
  statistics, versioning, and five agent surfaces are binding.
- **Memory-safe language**: C#/.NET is approved. Historical C/C++ is read-only.
- **Secure coding**: Controlled file paths, parser input, error states, and
  resource keys receive explicit validation and fail-safe handling.
- **Architecture**: Existing framework boundaries are reused. Repeated example
  presentation is isolated in `examples/Shared/Wave3Runtime.cs`; reusable domain
  behavior cannot be implemented there.
- **NIST SSDF / CWE Top 25**: Applicable to changed code, tests, parser/file
  calls, and review evidence.
- **ASVS**: `N/A`; no web/API/auth surface. Re-evaluate if such a boundary appears.
- **Supply chain**: Existing SBOM/VEX/SLSA/OpenSSF evidence remains authoritative;
  no package, lockfile, distribution, or provenance change is planned.
- **AI-SBOM / regulation**: `N/A`; AI remains development tooling and no
  runtime/product AI or regulated operated service enters scope.
- **STRIDE/CIA/CAPEC**: Applicable proportionally to controlled path, malformed
  source, help-data, and resource-key boundaries. No new external trust boundary.
- **S-ADR / arc42 / Zero Trust / SAMM / BSI C3A / BSI C5**: New artifacts are
  `N/A` unless architecture, cloud, provider, deployment, or distributed service
  boundaries change.
- **iSAQB**: Existing component boundaries and quality goals are documented;
  a material architecture deviation would require an S-ADR and stop the local fix.
- **A11Y**: Applicable to keyboard operation, status, description, semantic
  guides, text-first equivalence, WCAG 2.2 AA review, and DocFX/axe proof.
- **Didactic comments**: New non-trivial dispatch, controlled-I/O, and proof
  helpers receive selective why/trade-off/proof-boundary review.
- **Cross-platform**: Runtime/path behavior is cross-platform; script governance
  is `N/A` because no scripts are planned.
- **Agent parity**: Active feature context changes; all five maintained surfaces
  are synchronized after planning and again at completion if needed.
- **Autonomous task rule**: Every remote/delivery task names
  `specs/019-wave3-visual-component-porting/pr-evidence.md` as its acceptance ledger.
- **Versioning**: Before each `dotnet build` or `dotnet test`, increment only the
  manual build component. Before commit/push align all version fields to
  `1.19.<branch-commit-count>.<build>`.

### Governance Checkpoint Matrix

| Domain | Planned applicability | Evidence boundary |
|---|---|---|
| NIST SSDF / CWE Top 25 | Applicable | Changed C#, tests, controlled I/O, and feature evidence |
| OWASP ASVS | N/A unless web/API/auth appears | Existing ASVS ledger plus feature trigger row |
| SBOM / VEX / SLSA / OpenSSF | Existing baseline; no new feature artifact | Supply-chain ledger and feature evidence |
| AI-SBOM / NIS2 / CRA / EU AI Act / DORA | N/A for local non-AI training examples | Feature row with re-evaluation trigger |
| STRIDE / CIA / CAPEC | Proportional review of local file/parser/resource boundaries | Feature risk/evidence rows |
| S-ADR / arc42 security / Zero Trust / SAMM | N/A for new artifacts absent architecture change | Existing architecture/security documents |
| BSI C3A / BSI C5 | N/A; no cloud/provider/deployment boundary | Cloud applicability documents and feature row |
| iSAQB architecture | Applicable as reuse/quality-boundary review | Plan, research, framework decisions |
| A11Y | Applicable | Runtime keyboard/text proof, guides, DocFX, Playwright/axe |
| Cross-platform | Runtime/path proof applicable; script governance N/A | local and remote platform evidence |
| Agent parity | Applicable | five synchronized agent surfaces |

## Project Structure

### Documentation

```text
specs/019-wave3-visual-component-porting/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── wave3-visual-component-acceptance.md
├── checklists/
├── tasks.md
└── pr-evidence.md
```

### Runtime and Tests

```text
examples/
├── Shared/Wave3Runtime.cs
├── BHelp/
├── HelpDemo/
├── I18n/
├── TvEdit/
└── TvHc/

tests/TuiVision.Examples.SmokeTests/
├── Wave3VisualSmokeMatrixTests.cs
├── BHelpSmokeTests.cs
├── HelpDemoSmokeTests.cs
├── I18nSmokeTests.cs
├── TvEditSmokeTests.cs
└── TvHcSmokeTests.cs

docs/guides/examples/
├── bhelp.md
├── helpdemo.md
├── i18n.md
├── tvedit.md
└── tvhc.md
```

**Structure Decision**: Five independent executable projects own their domain
composition. `Wave3Runtime.cs` contains only repeated status/description/region
presentation. Existing serialization and control APIs carry editor, help,
localization, and compilation behavior. A failing focused contract is required
before any framework source change.

## Phase 0: Research Decisions

1. Treat Feature 018 as complete and reuse its public contracts.
2. Use `TvEdit` as the vertical slice because it exercises a real control,
   command dispatch, visible state, controlled I/O, rejection, and safe close.
3. Preserve `BHelp` viewer/search/navigation intent but intentionally omit the
   proprietary unsafe Borland `.tch` decoder.
4. Preserve `HelpDemo` context-aware focus/hint/command intent using current
   controls and help topics.
5. Preserve `I18n` visible language change through explicit deterministic lookup,
   not ambient process locale or gettext.
6. Preserve `TvHc` topic/source/cross-reference/compiler-result intent through
   `THelpSourceCompiler`; persistence proof is test-temp-only.
7. Reuse the Wave-1/Wave-2 app-loop proof shape and current smoke infrastructure.

## Phase 1: Design

### Runtime Design

- `Wave3Runtime` supplies a drawable `TStatusLine`, consistent Help menu,
  description window, and stable main/screen region conversion.
- `TvEditApp` hosts `TFileEditor` inside `TEditWindow`, exposes edit/safe-close/
  description commands, and reports buffer identity and modified state.
- `BHelpApp` builds controlled `THelpFile` topics, hosts `THelpWindow`, exposes
  next/unknown/search-style navigation, and reports current context/topic.
- `HelpDemoApp` composes focusable buttons with help contexts and shows current
  hint/topic through real focus/command dispatch.
- `I18nApp` renders selected localized values from explicit neutral/alternative
  dictionaries and reports requested/matched language and fallback reason.
- `TvHcApp` compiles embedded/source-controlled text, displays diagnostics or
  compiled topic content, and uses no implicit user path.

### Vertical Slice

1. Add Wave-3 project references and failing `TvEdit` app-loop tests.
2. Prove first frame, edit event, modified state, view tree, rendered cells,
   description route, and safe-close rejection.
3. Implement shared presentation and `TvEdit` only.
4. Run one targeted test batch and record the complete evidence row.
5. Spread the proven pattern to Help, i18n, and compiler demos.

### Proof Design

- One matrix row per example with historical source, framework decision,
  operation, state, view type, cell region, status, description, I/O boundary,
  helper classification, and result.
- Primary smokes drive `InteractiveSmokeEventScript` or equivalent injected
  events through `app.Run()`.
- Existing `ExampleTestBase` captures rendered buffers; direct methods may only
  prepare controlled content or add supplemental assertions.
- Negative cases are grouped per project before implementation where they share
  one contract, testing the 018 `ObserveAgain` efficiency hypothesis without
  obscuring individual expected failures.

### Documentation and Governance Design

- Add five bilingual guides and update `docs/toc.yml` plus `examples/README.md`.
- Update Pflichtenheft and agent context to route next to Feature 020.
- Populate governance rows with complete audit fields in `pr-evidence.md`.
- Archive the Lastenheft only after runtime, proof, docs, and validation pass.
- Retrospective changes to generic autonomous workflow occur only on a separate
  post-feature branch/PR after merge.

## Phase 2: Implementation Order

1. Create evidence schema, historical inventory, framework matrix, and governance rows.
2. Add project skeletons and complete failing `TvEdit` vertical-slice tests.
3. Implement `Wave3Runtime` and `TvEdit`; validate and record proof.
4. Add grouped failing `BHelp` and `HelpDemo` proof, then implementations.
5. Add grouped failing `I18n` and `TvHc` proof, then implementations.
6. Complete matrix tests, negative cases, comments, and framework decisions.
7. Update guides, README, DocFX navigation, Pflichtenheft, agents, and statistics.
8. Run static, targeted, full Release, coverage, DocFX/A11Y, secret, and hygiene gates.
9. Archive the Lastenheft, align version, commit, push, open PR, converge review,
   merge, clean branches, synchronize main, and record closeout evidence.

## Post-Design Constitution Re-check

Passed. The design adds no dependency, network, cloud, authentication, script,
runtime AI, proprietary decoder, or uncontrolled file discovery. Controlled
local I/O, A11Y, cross-platform paths, historical evidence, agent parity, and
remote evidence paths are explicit. A material framework gap stops local
implementation until classified through the framework gate.

## Complexity Tracking

No Constitution violation or exceptional complexity is planned.
