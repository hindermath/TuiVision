# Implementation Plan: Wave-1 Visual Component Remediation

**Branch**: `017-wave1-visual-component-remediation` | **Date**: 2026-07-11 | **Spec**: [spec.md](spec.md)  
**Input**: `Lastenheft_Wave1-Visual-Component-Remediation.md` and the accepted feature-014 baseline

## Summary

Remediate `Desklogo`, `MsgCls`, all 16 `Tutorial` steps, and `Videomode` so
normal runtime exposes the historical demo intent through a visible main
component, a real `TStatusLine`, and keyboard-reachable `Help -> Description`.
Primary smoke proof will drive real app-loop events and combine concrete state,
view-tree, and rendered buffer/cell assertions. `MsgCls` is the vertical slice;
the same bounded composition pattern then extends to Desklogo, Tutorial, and
Videomode. Historical sources remain read-only.

## Technical Context

**Language/Version**: C# 14 on .NET 10  
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Drivers.Console`; no new packages  
**Storage**: N/A; all example state is in-process and session-only  
**Testing**: MSTest 4, in-process example smoke tests, Coverlet, DocFX, Playwright with axe  
**Target Platform**: macOS and Linux terminals; Windows/WSL compatibility evidence for terminal capability paths  
**Project Type**: Multi-project terminal UI framework with executable learning examples  
**Performance Goals**: No new performance target; first visible state and deterministic smoke completion remain within existing test timeouts  
**Constraints**: Keyboard-first, text-first fallback, no new dependencies, no historical-source edits, no Wave-2/3/4 behavior, no broad framework redesign  
**Scale/Scope**: Four example areas, 16 Tutorial tokens, four smoke classes plus one Wave-1 visual matrix, four guides, one README, and feature/governance evidence

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: TuiVision's .NET 10, MSTest, DocFX/A11Y,
  statistics, and five-agent-surface addendum is binding.
- **Memory-safe language**: C#/.NET is on the MSL allow-list. Historical C/C++
  is read-only reference and is never generated or modified.
- **Secure code generation**: Changes avoid new I/O, serialization, network,
  authentication, secrets, and dependencies. Inputs such as Tutorial tokens
  and terminal capability results retain explicit fallback handling.
- **Secure architecture**: No trust boundary, privilege, deployment, or service
  boundary changes. Example composition remains separated from reusable
  framework behavior through the framework-usage gate.
- **Security documentation**: Existing `docs/security/` evidence is reviewed.
  New threat model, S-ADR, or arc42 security concept is `N/A` unless the actual
  implementation changes a security or trust boundary.
- **Security standards**: NIST SSDF and CWE Top 25 apply. ASVS, CAPEC, new
  SBOM/VEX/SLSA/OpenSSF evidence, Zero Trust, BSI C3A/C5, SAMM change, and new
  regulatory evidence are trigger-based `N/A` with re-evaluation conditions in
  `pr-evidence.md`.
- **AI-SBOM**: `N/A`; AI is development tooling only. Re-evaluate if runtime
  models, datasets, inference infrastructure, or AI services enter scope.
- **Supply chain**: Existing repository evidence remains authoritative. No
  package, lock, build-distribution, or provenance change is planned.
- **Default evidence files**: Review
  `docs/security/asvs-verification.md`, `supply-chain-evidence.md`,
  `zero-trust-applicability.md`, `samm-assessment.md`,
  `cloud-autonomy-applicability.md`, `cloud-compliance-assurance.md`, and
  `regulatory-applicability.md`; update only if a trigger changes.
- **Installed presets**: `security-governance` 0.6.0,
  `architecture-governance` 0.5.0, `isaqb-architecture-governance` 0.2.0,
  `a11y-governance` 0.4.0, `cross-platform-governance` 0.2.0, and
  `agent-parity-governance` 0.3.0 all apply proportionally.
- **Security-first**: No credential, agent state, log, cache, generated DocFX,
  or test-output tracking is planned.
- **Inclusion/A11Y**: Terminal main surfaces, status lines, description dialogs,
  guides, README, and DocFX are affected. Keyboard operation, text-first
  equivalence, semantic Markdown, WCAG 2.2 AA, and axe proof apply.
- **Bilingual delivery**: Learner-facing runtime descriptions and guides are
  German first, English second, approximately CEFR-B2.
- **Statistics**: `docs/project-statistics.md` is updated with the 80-line/day
  experienced-developer and 125-line/day Thorsten-Solo baselines plus 7.8-hour
  conversion.
- **Agent guidance parity**: Active feature context changes, so `AGENTS.md`,
  `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md` are reviewed and synchronized.
- **Versioning**: Before every build or test, increment only the manual build
  component. Before commit/push, align all three version fields to
  `1.17.<branch-commit-count>.<build>` without an extra build increment.

### Governance Checkpoint Matrix

| Domain | Planned applicability | Evidence boundary |
|---|---|---|
| NIST SSDF / CWE Top 25 | Applicable | Changed C# and test files plus `pr-evidence.md` secure-coding review |
| OWASP ASVS | N/A unless web/API/auth surface appears | `docs/security/asvs-verification.md` review row |
| SBOM / VEX / SLSA / OpenSSF | Existing baseline; no feature artefact unless dependency/release trigger changes | `docs/security/supply-chain-evidence.md` and feature evidence |
| AI-SBOM | N/A, development-tool-only AI | Feature evidence with runtime-AI re-evaluation trigger |
| NIS2 / CRA / EU AI Act / DORA | Screening only; local example UI does not trigger new evidence | `docs/security/regulatory-applicability.md` review |
| STRIDE / CIA / CAPEC | Existing architecture baseline; no new trust boundary | Feature evidence and threat-model review |
| S-ADR / arc42 / Zero Trust / SAMM | N/A for new artefacts unless architecture/security boundary changes | Existing architecture/security documents |
| BSI C3A / BSI C5 | N/A; no cloud provider, cloud service, deployment, or portability boundary changes | Cloud applicability documents plus feature trigger row |
| A11Y | Applicable | Runtime keyboard/text proof, guides, DocFX, Playwright/axe |
| Cross-platform | Applicable to terminal runtime and tests; script governance N/A unless scripts change | local/CI platform evidence |
| Agent parity | Applicable because active feature context changes | synchronized five agent surfaces |

## Project Structure

### Documentation (this feature)

```text
specs/017-wave1-visual-component-remediation/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── wave1-visual-component-acceptance.md
├── checklists/
├── tasks.md
└── pr-evidence.md
```

### Source Code (repository root)

```text
examples/
├── Shared/
│   └── Wave1Runtime.cs
├── Desklogo/
├── MsgCls/
├── Tutorial/
│   ├── Steps/
│   └── TutorialVisualFactory.cs
└── Videomode/

tests/TuiVision.Examples.SmokeTests/
├── ExampleTestBase.cs
├── InteractiveSmokeEventScript.cs
├── Wave1VisualSmokeMatrixTests.cs
├── DesklogoSmokeTests.cs
├── MsgClsSmokeTests.cs
├── TutorialSmokeTests.cs
└── VideomodeSmokeTests.cs

docs/guides/examples/
├── desklogo.md
├── msgcls.md
├── tutorial.md
└── videomode.md
```

**Structure Decision**: Keep example-specific composition in the four existing
projects. Share only status/help/region composition that is repeated by all four
through `examples/Shared/Wave1Runtime.cs`. Do not alter Wave-2 implementation or
framework projects unless a focused failing test proves a small framework gap.

## Phase 0: Research Decisions

1. Review feature-014 evidence and historical `.c`/`.cc`/`.cpp` plus required
   headers before coding.
2. Use `MsgCls` as the vertical slice because it has a concrete command,
   broadcast route, repeatable state, and visible result.
3. Reuse the established Wave-2 proof shape, not Wave-2 local behavior:
   app-loop event script, concrete state, view-tree kind, stable buffer region,
   status line, and `Help -> Description`.
4. Keep Tutorial metadata classes intact and build token-specific visible
   compositions through a factory. This avoids public contract churn while
   allowing 16 distinct component/state targets.
5. Treat terminal inability to resize as an accepted honest Videomode outcome,
   not a platform failure.
6. Prefer a shared example-composition helper over repeated local status/help
   code. Escalate only genuinely reusable framework behavior through the gate.

## Phase 1: Design

### Runtime Design

- `Wave1Runtime` supplies a drawable status line, consistent Help menu,
  bilingual status formatting, and stable screen-region conversion.
- Each app owns command identifiers and state transitions so tests exercise
  public application behavior instead of generic helper output.
- `MsgCls` first proves the complete pattern: menu/key command, broadcast,
  message-window update, status update, description dialog, repeated trigger.
- `Desklogo` keeps logo rendering as its main state and adds status,
  description, and quit paths without artificial logo mutation.
- `TutorialVisualFactory` maps every accepted token to a representative existing
  control/view composition and visible state. The application adds status,
  description, default-token, unknown-token, and clean-quit behavior.
- `Videomode` exposes a visible retry/probe command and maps coordinator results
  to the four canonical user-visible states without claiming unsupported
  terminal behavior.

### Tutorial Visual Target Map

| Token | Defining historical intent | Planned representative visible target |
|---|---|---|
| `tvguid01` | Minimal application shell | Desktop lesson panel with application lifecycle state |
| `tvguid02` | Status-line item | Real `TStatusLine` lesson state and key hint |
| `tvguid03` | Menu and command handling | Real menu command with visible result window |
| `tvguid04` | Window insertion | Visible `TWindow` inserted into the desktop |
| `tvguid05` | Drawing inside a window | Window containing a custom-drawn lesson view |
| `tvguid06` | Scrollable content introduction | Window with vertical scroll bar and clipped content |
| `tvguid07` | Improved two-axis content | Window with horizontal and vertical scroll bars |
| `tvguid08` | Scroller delta | Scrollable view with visible delta/offset feedback |
| `tvguid09` | Multiple panes | Window with two visible content panes |
| `tvguid10` | Resize constraints | Resizable window with visible size-limit state |
| `tvguid11` | Dialog introduction | Visible non-modal dialog composition |
| `tvguid12` | Modal dialog behavior | Visible modal-result state through the app loop |
| `tvguid13` | Dialog buttons | Dialog with two distinct command buttons |
| `tvguid14` | Labels, checks, and radio choices | Dialog with label, check boxes, and radio buttons |
| `tvguid15` | Input line | Dialog with visible input state |
| `tvguid16` | Dialog data transfer and validation | Dialog with save/restore state and visible validation/rejection |

Every row includes the token and goal in text-first form. Reused control families
are acceptable only when the representative state and defining result remain
distinct in the view tree and rendered buffer.

### Proof Design

- Add a Wave-1 matrix listing each primary method, visible target, status,
  description route, historical source, helper classification, and render proof.
- Extend current Wave-1 smoke classes test-first.
- Use `InteractiveSmokeEventScript` and existing buffer helpers; change shared
  smoke infrastructure only if the Wave-1 requirements cannot be expressed by
  existing generic methods.
- Keep current feature-014 functional tests as regression/supplemental proof.
- Record each example and Tutorial token in `pr-evidence.md`; no empty starter
  rows are accepted at completion.

### Documentation And Governance Design

- Update four guides and `examples/README.md` to match actual controls and keys.
- Update `Pflichtenheft.md` completion and next-step marker only after acceptance.
- Update all five agent surfaces because active feature context advances to 017.
- Review architecture and security evidence proportionally; unchanged documents
  receive explicit rationale in feature evidence.
- Archive the Lastenheft only after runtime, proof, docs, and governance gates pass.

## Phase 2: Implementation Order

1. Create evidence schema and baseline inventories.
2. Add failing matrix and vertical-slice smoke tests.
3. Implement shared Wave-1 composition and MsgCls slice.
4. Validate slice and record framework decision.
5. Add failing Desklogo tests, then implementation.
6. Add 16-token Tutorial matrix/tests, then visual factory and app integration.
7. Add Videomode operation/fallback tests, then implementation.
8. Update guides, README, governance, agent context, project statistics, and
   completion routing.
9. Run targeted, full, coverage, formatting, DocFX, A11Y, platform, and diff
   gates with version increments before every build/test.
10. Archive the Lastenheft and prepare PR evidence.

## Post-Design Constitution Re-check

Passed. The design introduces no dependency, storage, external service, trust
boundary, cloud topology, script, or runtime-AI change. A11Y, cross-platform,
historical-source, statistics, and agent-parity obligations are explicitly
planned. Any framework change remains conditional on a focused failing test and
must stay narrow enough for `SmallFrameworkFix`.

## Complexity Tracking

No Constitution violation or exceptional complexity is planned.
