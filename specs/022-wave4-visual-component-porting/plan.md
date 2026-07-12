# Implementation Plan: Wave-4 Visual Component Porting

**Branch**: `022-wave4-visual-component-porting` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_Wave4-Visual-Component-Porting.md` plus accepted Features 019-021

## Summary

Create five normal .NET terminal applications: `Cyrillic`, `ETerm`, `Fonts`,
`Terminal`, and `XTerm`. Each application uses existing Feature-021 contracts
and current TuiVision controls to show a visible main composition, dynamic
status line, and keyboard-reachable bilingual description. `Terminal` is the
test-first vertical slice. Primary proof drives the real app loop and combines
concrete domain state, exact view identity, and rendered buffer/cell regions.
Historical sources remain read-only; immutable manifests and one controlled
font fixture preserve intent without host mutation or general legacy parsers.

## Technical Context

**Language/Version**: C# 14 on .NET 10
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Drivers.Console`; no new packages
**Storage**: Embedded/source-controlled read-only manifests and one exact raw font fixture; no user persistence
**Testing**: MSTest 4, in-process app-loop smokes, Coverlet, DocFX, Playwright with axe
**Target Platform**: macOS/Linux terminals plus Windows/WSL compatibility through current CI
**Project Type**: Multi-project terminal UI framework with executable learning examples
**Performance Goals**: Deterministic first frame and smoke completion within existing repository timeouts
**Constraints**: Keyboard-first, no process/shell/PTY, no host mutation, no new dependency, no `tv203s/` edit, no full emulator, no Feature-023 scope
**Scale/Scope**: Five projects, one shared presentation helper, five primary smoke suites, five guides, one README/index, feature/governance evidence

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: .NET 10, MSTest, coverage, DocFX/A11Y,
  project statistics, versioning, and five agent surfaces are binding.
- **Memory-safe language**: C#/.NET is approved; historical C/C++ and scripts
  are read-only evidence.
- **Secure coding**: Terminal observations, profile/resource identities,
  fixture shape, sequence bounds, and fallback states are validated fail-safe.
- **Architecture**: Driver/Core/Controls ownership from 021 remains unchanged.
  `examples/Shared/Wave4Runtime.cs` contains presentation and proof helpers only.
- **NIST SSDF / CWE Top 25**: Applicable to changed code, tests, bounded
  resources, fixture handling, status publication, and evidence.
- **ASVS**: `N/A`; no web/API/auth surface.
- **Supply chain**: Existing SBOM/VEX/SLSA/OpenSSF evidence remains authoritative;
  no package, lockfile, distribution, or provenance scope is planned.
- **AI-SBOM / regulation**: `N/A`; AI is development tooling and no runtime AI,
  regulated service, market product, or regulated supply-chain role enters.
- **STRIDE/CIA/CAPEC**: Applicable proportionally to terminal input, resource
  identity, state integrity, bounded memory, and capability claims.
- **S-ADR / arc42 security / Zero Trust / SAMM / BSI C3A / BSI C5**: New
  artifacts are `N/A` unless architecture/cloud/provider/deployment boundaries change.
- **iSAQB**: Existing runtime/component views and quality goals are reused; a
  material ownership change requires a separate architecture decision.
- **A11Y**: Keyboard operation, text status/fallback, non-color-only proof,
  bilingual guides, WCAG 2.2 AA review, DocFX/axe, and text-browser proof apply.
- **Didactic comments**: Non-trivial app-loop, raster, manifest, and proof
  helpers receive selective why/trade-off/proof-boundary review.
- **Cross-platform**: Host and asset/path proof applies. Script parity is `N/A`
  because no script is planned or changed.
- **Agent parity**: Active feature context changes; all five maintained surfaces
  are synchronized together.
- **Autonomous evidence**: Every remote task names
  `specs/022-wave4-visual-component-porting/pr-evidence.md` or the pre-named
  `specs/022-wave4-visual-component-porting/closeout-evidence.md`.
- **Versioning**: Increment only the manual build component before each
  `dotnet build` or `dotnet test`; align commits/pushes to
  `1.22.<branch-commit-count>.<build>`.

### Governance Checkpoint Matrix

| Domain | Planned applicability | Evidence boundary |
|---|---|---|
| NIST SSDF / CWE Top 25 | Applicable | Changed C#, tests, resources, fixtures, and feature evidence |
| OWASP ASVS | N/A unless web/API/auth appears | Existing ASVS ledger plus feature trigger row |
| SBOM / VEX / SLSA / OpenSSF | Existing baseline; no new feature artifact | Supply-chain ledger and feature evidence |
| AI-SBOM / NIS2 / CRA / EU AI Act / DORA | N/A for local non-AI training demos | Feature row with re-evaluation trigger |
| STRIDE / CIA / CAPEC | Terminal/resource/fixture/fallback review | Feature risk/evidence rows and threat-model update if triggered |
| S-ADR / arc42 security / Zero Trust / SAMM | N/A absent architecture change | Existing architecture/security evidence |
| BSI C3A / BSI C5 | N/A; no cloud/provider/deployment boundary | Existing cloud applicability documents |
| iSAQB architecture | Applicable as reuse/quality-boundary review | Plan, research, framework decisions, runtime view |
| A11Y | Applicable | App-loop keyboard/text/cell proof, guides, DocFX, axe, lynx |
| Cross-platform | Host proof applicable; script governance N/A | deterministic/remote/physical host rows |
| Agent parity | Applicable | five synchronized agent surfaces |

## Project Structure

### Documentation

```text
specs/022-wave4-visual-component-porting/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── wave4-visual-component-acceptance.md
├── checklists/
├── tasks.md
├── pr-evidence.md
└── closeout-evidence.md
```

### Runtime and Tests

```text
examples/
├── Shared/Wave4Runtime.cs
├── Cyrillic/
├── ETerm/
├── Fonts/
│   └── Fixtures/font-8x16.bin
├── Terminal/
└── XTerm/

tests/TuiVision.Examples.SmokeTests/
├── Wave4VisualSmokeMatrixTests.cs
├── CyrillicSmokeTests.cs
├── ETermSmokeTests.cs
├── FontsSmokeTests.cs
├── TerminalSmokeTests.cs
└── XTermSmokeTests.cs

docs/guides/examples/
├── cyrillic.md
├── eterm.md
├── fonts.md
├── terminal.md
└── xterm.md
```

**Structure Decision**: Five independent executable projects own their domain
composition. `Wave4Runtime.cs` contains only repeated status, description,
region, manifest-display, and smoke-event presentation. Session parsing,
charset mapping, fixture validation, profiles, host classification, and terminal
rendering remain in existing 021 framework types. Because the helper is linked
into five assemblies, cross-project tests use public state/contract delegates,
not shared CLR type identity.

## Phase 0: Research Decisions

1. Treat Features 019-021 as complete; do not reopen their accepted behavior.
2. Use `Terminal` as the vertical slice because it exercises the complete
   Driver-to-Controls-to-app-loop chain and visible recovery/fallback proof.
3. Reuse `TerminalSession`, `TTerminalView`, `TerminalCharsetMapper`,
   `BitmapFontFixture`, `TerminalProfile`, and deterministic host classification.
4. Preserve Cyrillic KOI8-R learning intent without locale, codepage, setup
   script, root access, or host keyboard-map mutation.
5. Preserve Fonts raster intent through an exact project-owned fixture and
   visible 8x16 glyph, never generator execution or host installation.
6. Preserve ETerm/XTerm resource intent through immutable representative
   manifests with explicit `IntentionalDeviation`, not general parser claims.
7. Reuse the Wave-1 through Wave-3 app-loop/state/view/cell proof shape.
8. Keep deterministic, remote, and physical host evidence separate; physical
   gaps remain `NotRun` with a trigger.

## Phase 1: Design

### Runtime Design

- `Wave4Runtime` supplies a drawable status line, Help/Description menu,
  stable bounded regions, text-first fallback, and read-only manifest display.
- `TerminalApp` owns one 021 session and `TTerminalView`, queues controlled
  observations, exposes reset/rejection/description commands, and reports
  cursor/profile/capability state.
- `CyrillicApp` maps a fixed sample through `TerminalCharsetMapper`, renders a
  labeled character grid, and exposes direct/replaced/invalid/unsupported states.
- `FontsApp` loads only its copied source-controlled fixture, validates exact
  metadata through `BitmapFontFixture`, renders one nonblank glyph as text-first
  pixels, and shows invalid-fixture fallback in tests.
- `ETermApp` and `XTermApp` expose immutable typed manifest entries with source
  identity, representative values, accepted subset, and visible unsupported state.

### Vertical Slice

1. Add five project references and a complete failing `Terminal` matrix.
2. Prove first frame, controlled text, cursor/attribute action, rejection,
   recovery, fallback, description, view identity, cell region, and quit.
3. Implement shared presentation plus `Terminal` only.
4. Run one focused project-local green batch and record its evidence row.
5. Spread the established composition/proof pattern to Cyrillic/Fonts and manifests.

### Proof Design

- One acceptance row per example records historical sources, framework decision,
  operation, state, exact view type, cell region, status, description, host class,
  fallback, helper role, and result.
- Primary smokes drive queued events through `app.Run()` and capture the real
  owner buffer. Direct methods only create deterministic inputs or add supplemental proof.
- Project-local red matrices group related negative cases while preserving every
  expected boundary and repair owner.
- The solution-level smoke matrix accesses linked example assemblies through
  public contracts or state delegates and never assumes linked helper type identity.
- Standard and narrow viewport cases prove that identity/status/fallback remain
  visible even when a list or raster is clipped.

### Assets and Safety Design

- The font fixture is copied once into `examples/Fonts/Fixtures/` with source
  origin documented and `CopyToOutputDirectory` for controlled project access.
- ETerm/XTerm values are typed immutable data in their projects; historical
  files are not executed, parsed at arbitrary paths, or mutated.
- Examples perform no writes and no user-path discovery.
- Static review rejects process, shell, PTY, external-command, locale mutation,
  font install, codepage, keyboard-map, terminal-setting, and audio APIs.

### Documentation and Governance Design

- Add five bilingual guides and update `docs/toc.yml` plus `examples/README.md`.
- Update Pflichtenheft and all five agent contexts to route next to Feature 023.
- Populate complete governance/host/framework/proof rows in `pr-evidence.md`.
- Archive the Lastenheft after runtime, proof, docs, and all local gates pass.
- Generic autonomous-workflow changes remain a separate post-feature
  retrospective PR; 022 implementation does not silently change templates.

## Phase 2: Implementation Order

1. Create evidence schema, historical inventory, framework matrix, host matrix,
   governance rows, and pre-name the causal closeout path.
2. Review full compile surface, project references, linked-source identity,
   fixture copying, harness helpers, and public XML documentation.
3. Add project skeletons and complete failing `Terminal` vertical-slice tests.
4. Implement shared presentation and `Terminal`; validate and record proof.
5. Add grouped failing Cyrillic/Fonts matrices, then their implementations.
6. Add grouped failing ETerm/XTerm matrices, then immutable manifest demos.
7. Complete cross-example matrix, narrow viewport, fallback, comments, and decisions.
8. Update guides, README, navigation, architecture/security evidence if triggered,
   Pflichtenheft, agents, statistics, and archive.
9. Run static, targeted, full Release, coverage, DocFX/A11Y/lynx, secret, scope,
   and generated-output gates.
10. Align version, commit, push, open PR, converge checks/reviews, merge, clean
    branches, sync main, and use exactly one causal closeout PR when required.

## Validation Strategy

| Change | Required proof |
|---|---|
| Every change | `git diff --check`, placeholder/scope/generated/secret scans |
| Five examples/shared helper | Targeted Release example-smoke project |
| Shared executable/proof behavior | Full Release suite and canonical coverage gate |
| XML/guides/navigation | DocFX then Playwright/axe and UTF-8 lynx review |
| Visible TUI | App-loop, state, exact view, status, description, and cell-region proof |
| Historical behavior | Relevant implementations, headers, resources, configs, scripts, and fixture metadata reviewed read-only |
| Host claims | Separate deterministic, remote, and physical evidence rows |

## Post-Design Constitution Re-check

Passed. The design adds no package, network, cloud, authentication, script,
runtime AI, host process, host mutation, legacy parser, arbitrary user path, or
new architecture boundary. Controlled assets, A11Y, platform evidence,
historical intent, agent parity, versioning, and causal remote evidence are explicit.

## Complexity Tracking

No Constitution violation or exceptional complexity is planned. The linked
presentation helper is a deliberate example-only reuse boundary; reusable
terminal, mapping, font, profile, and host behavior remains in framework code.
