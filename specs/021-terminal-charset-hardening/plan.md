# Implementation Plan: Terminal and Charset Hardening

**Branch**: `021-terminal-charset-hardening` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_05_TerminalCharsetAndEmulation.md` and the accepted Feature-020 baseline

## Summary

Add one deterministic in-process terminal session to `TuiVision.Drivers.Console`
with a deliberately small control-sequence grammar, bounded history, atomic
rejection, Unicode and KOI8-R mapping, one raw 8x16 font-fixture contract, and a
closed JSON profile schema. Project the session through one `TTerminalView` in
`TuiVision.Controls` for real app-loop, view-tree, and buffer/cell proof. Host
processes, shells, PTYs, host font/codepage changes, full ANSI/VT/XTerm parity,
and visible Wave-4 example porting remain outside Feature 021.

## Technical Context

**Language/Version**: C# 14 on .NET 10
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Drivers.Console`, `TuiVision.Controls`, and reviewed `TuiVision.Compatibility` key translation; no new packages
**Storage**: In-memory session/history state plus source-controlled JSON and raw 8x16 fixtures
**Testing**: MSTest 4, deterministic parser/state matrices, real app-loop integration proof, Coverlet, DocFX, Playwright with axe
**Target Platform**: Deterministic macOS/Linux/Windows/WSL behavior; physical terminal observations remain a separate optional evidence class
**Project Type**: Multi-project terminal UI framework
**Performance Goals**: Bounded sequence parsing; O(visible cells) resize/reset; FIFO history capped at 4,096 cells
**Constraints**: No process/shell/PTY, no host mutation, no new dependency, no example-local reusable logic, no `tv203s/` edits, no Wave-4 porting
**Scale/Scope**: One Driver session/parser, one mapper, one font contract, one profile loader, one Controls view, focused tests, guides/evidence/governance

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: .NET 10, MSTest, coverage, DocFX/A11Y,
  statistics, versioning, and five maintained agent surfaces remain binding.
- **Memory-safe language**: C#/.NET is approved; historical C/C++ is read-only.
- **Secure coding**: Terminal observations, profile JSON, charset units, and font
  bytes are untrusted, bounded, fully validated, and atomically rejected.
- **Architecture**: Drivers.Console owns reusable terminal state and decoding;
  Controls owns only projection and event-loop integration; Compatibility keeps
  its existing key-translation ownership.
- **NIST SSDF / CWE Top 25**: Applicable to size/range validation, parser state,
  lifecycle cleanup, malformed-input recovery, tests, and review evidence.
- **ASVS**: `N/A`; no web/API/auth surface. Re-evaluate if one appears.
- **Supply chain**: Existing SBOM/VEX/SLSA/OpenSSF evidence remains authoritative;
  no package, distribution, lockfile, or provenance change is planned.
- **AI-SBOM / regulation**: `N/A`; no runtime/product AI or regulated operated service.
- **STRIDE/CIA/CAPEC**: Proportional review applies to spoofed control input,
  malformed profiles/fixtures, state corruption, denial through unbounded data,
  and misleading host capability claims.
- **S-ADR / arc42**: Existing component boundaries are extended without a new
  deployment or trust boundary; create no new artifact unless implementation deviates.
- **Zero Trust / SAMM / BSI C3A / BSI C5**: `N/A`; no cloud, provider,
  distributed service, or operations boundary changes.
- **iSAQB**: Ownership, quality goals, bounded grammar, and deliberate host
  limits are explicit; parallel example-local terminal implementations are prohibited.
- **A11Y**: Text-first status, keyboard operation, deterministic visible cells,
  bilingual guides, WCAG 2.2 AA review, and DocFX/axe proof are applicable.
- **Didactic comments**: New parser, state-machine, mapping, fallback, and proof
  logic is reviewed for concise why/trade-off/proof-boundary comments.
- **Cross-platform**: Host/capability classification is applicable; script
  governance is `N/A` because no script is planned.
- **Agent parity**: Active feature context changes; all five maintained agent
  surfaces are synchronized at completion.
- **Autonomous task rule**: Every remote task names
  `specs/021-terminal-charset-hardening/pr-evidence.md` as acceptance ledger.
- **Versioning**: Increment only the manual build component before each build
  or test; align all fields to `1.21.<branch-commit-count>.<build>` before commit/push.

### Governance Checkpoint Matrix

| Domain | Planned applicability | Evidence boundary |
|---|---|---|
| NIST SSDF / CWE Top 25 | Applicable | Parser, profile/font validation, lifecycle, tests, feature evidence |
| OWASP ASVS | N/A unless web/API/auth appears | Existing ASVS ledger plus trigger row |
| SBOM / VEX / SLSA / OpenSSF | Existing baseline; no new artifact | Supply-chain ledger plus feature row |
| AI-SBOM / NIS2 / CRA / EU AI Act / DORA | N/A for local non-AI training framework | Feature row with re-evaluation trigger |
| STRIDE / CIA / CAPEC | Applicable to untrusted input, resource bounds, state integrity, and host claims | Threat/evidence rows |
| S-ADR / arc42 / Zero Trust / SAMM | N/A for new artifacts absent architecture change | Existing architecture/security evidence |
| BSI C3A / BSI C5 | N/A; no cloud/provider boundary | Cloud applicability ledgers plus feature row |
| iSAQB architecture | Applicable | Plan, research, component decisions |
| A11Y | Applicable | Keyboard path, text status, view/cell proof, guides, DocFX/axe |
| Cross-platform | Host/fallback proof applicable; script governance N/A | Host matrix and CI evidence |
| Agent parity | Applicable | Five synchronized agent surfaces |

## Project Structure

### Documentation

```text
specs/021-terminal-charset-hardening/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── terminal-charset-acceptance.md
├── checklists/
├── tasks.md
└── pr-evidence.md
```

### Runtime and Tests

```text
src/TuiVision.Drivers.Console/
├── TerminalSession.cs
├── TerminalCharsetMapper.cs
├── BitmapFontFixture.cs
└── TerminalProfile.cs
src/TuiVision.Controls/
└── TTerminalView.cs
tests/TuiVision.Drivers.Tests/
├── TerminalSessionTests.cs
├── TerminalCharsetAndFontTests.cs
└── TerminalProfileTests.cs
tests/TuiVision.Controls.Tests/
└── TTerminalViewTests.cs
docs/guides/terminal-charset-hardening.md
```

**Structure Decision**: Drivers.Console owns the host-independent session,
bounded output grammar, charset mapping, font fixture validation, and profile
loading. `TTerminalView` consumes that public contract and renders through the
existing Controls/Core buffer model. Compatibility input translation remains
unchanged and is referenced only at its public boundary. No example project
receives reusable terminal logic.

## Phase 0: Research Decisions

1. Keep the terminal model in-process; never start a host process, shell, or PTY.
2. Support text; BEL, BS, TAB, CR, LF; CSI `A/B/C/D`, `H/f`, `J`, `K`, and `m`;
   plus full reset. Treat every other sequence as unsupported.
3. Limit one sequence to 64 characters, four numeric parameters, values
   0..9,999, and publish state only after full validation.
4. Cap history at 4,096 cells; preserve the top-left resize intersection and
   reset all visible/history/parser/fallback state deterministically.
5. Use Unicode as canonical display text, KOI8-R as the sole historical byte
   mapping, and U+FFFD for replacement.
6. Accept only raw 8x16, 256-glyph, 4,096-byte font fixtures; do not execute or
   port historical font generators or host installers.
7. Use a closed `System.Text.Json` profile schema with required identity and
   charset, safe optional defaults, and whole-profile rejection for unknown or
   duplicate properties.
8. Reuse the existing xterm key-compatibility boundary without adding a
   Drivers-to-Compatibility dependency.
9. Separate deterministic in-process, Remote-CI, and physical-host evidence;
   unavailable physical proof remains `NotRun`.
10. Add no script until a repeated deterministic need proves one; any later
    script requires Bash/PowerShell parity.

## Phase 1: Design

### Runtime Design

- `TerminalSession` owns dimensions, cursor, attributes, visible cells, FIFO
  history, lifecycle, notice count, status, and the bounded parser state.
- Each observation is classified and completely validated before mutation. A
  rejected or unsupported sequence preserves cells, cursor, attributes, and the
  next independent input boundary.
- Plain text and C0 controls use deterministic wrap, scroll, clipping, tab,
  backspace, CR/LF, and BEL rules. BEL changes only in-process notice/status state.
- CSI handlers clamp cursor operations, support documented erase modes and 16
  colors, and reject unsupported modes/codes without partial application.
- `TerminalCharsetMapper` maps Unicode and KOI8-R independently of host locale.
  Invalid or unmappable input returns U+FFFD plus an explicit outcome.
- `BitmapFontFixture` publishes row-byte metadata only after exact geometry and
  length checks; it never installs or generates a host font.
- `TerminalProfile` performs token-level duplicate/unknown-property checks before
  deserialization and exposes requested, effective, source, fallback, and
  capability state. Accepted presentation metadata is applied to the session so
  profile, charset, effective font identity/capability, and default colors are
  observable without installing raw raster bytes into the host renderer.
- `TTerminalView` renders the session snapshot, cursor, active presentation
  metadata, and status through the existing view buffer and forwards only
  controlled text/key input.

### Vertical Slice

1. Create evidence and complete the compile-surface review.
2. Add one failing Driver matrix for plain text, cursor/cell state, one accepted
   CSI action, one malformed sequence, and recovery.
3. Implement only enough session/parser behavior to make that matrix green.
4. Add one failing Controls app-loop proof for input, session state, view
   identity, rendered cells/status, and deterministic quit.
5. Implement the smallest `TTerminalView` projection and make the proof green.
6. Spread C0/CSI boundaries, history/resize/reset, charset/font/profile, host,
   historical, documentation, and governance coverage.

### Proof Design

- Session/emulation rows record observation, pre-state, command, outcome,
  cursor, affected cells, status, recovery, and result.
- Charset/font/profile rows record source, requested and effective values,
  validation outcome, fallback reason, host-independence, and evidence path.
- Controls proof runs `app.Run()` with controlled input and quit, then asserts
  state, active profile/charset/font metadata, concrete view identity, status
  text, and rendered buffer/cell positions.
- Host rows distinguish `Pass`, `Unsupported`, `NotRun`, and
  `FollowUpHardening`; deterministic tests never become physical-host proof.
- Framework rows use exactly one of `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, or `FollowUpHardening` for each accepted contract area.

### Documentation and Governance Design

- Add one bilingual terminal/charset guide and DocFX navigation entry.
- Update driver/runtime, security/architecture applicability, Pflichtenheft next
  marker, all five agent contexts, and project statistics.
- Archive the binding Lastenheft after all acceptance and validation gates pass.
- Keep generic autonomous-workflow refinement on a separate retrospective PR.

### Autonomous Delivery and Causal Closeout

- Delivery mode is `MergeAndSync` under the user's explicit authorization.
- Shared evidence, version, statistics, workflow, and agent files have one
  serialized writer at a time.
- Every remote acceptance task writes to the exact ledger
  `specs/021-terminal-charset-hardening/pr-evidence.md` or to the pre-named
  evidence-only closeout path
  `specs/021-terminal-charset-hardening/closeout-evidence.md` when the fact
  cannot exist before merge.
- Current-head checks and review-thread facts are verified before merge but are
  not committed back onto the same reviewed head when that commit would
  invalidate those claims.
- A closeout PR is used only for causally post-merge facts or self-invalidating
  reviewed-head evidence. It remains evidence-only and explains the necessity.
- Missing Copilot quota or unavailable reviewers are recorded as missing review,
  never as successful review. Admin bypass is limited to green required checks,
  zero actionable threads, and the sole remaining human-approval rule.

## Phase 2: Implementation Order

1. Evidence schema, historical inventory, framework/host/governance matrices.
2. Compile-surface review and failing Driver vertical-slice matrix.
3. Terminal session, bounded parser, C0/CSI behavior, atomic recovery, and targeted proof.
4. History, resize, reset, lifecycle, BEL, and boundary matrices.
5. KOI8-R/Unicode mapping, raw font contract, profile parsing/fallback, and negative proof.
6. Failing Controls integration test, minimal terminal view, real app-loop/view/cell proof.
7. Host matrix, historical deviations, comments, framework decisions, and threat review.
8. Guide, DocFX navigation, security/architecture evidence, Pflichtenheft, agents, statistics.
9. Static, targeted, full Release, coverage, DocFX/A11Y, secrets, and hygiene gates.
10. Archive, version, commit, push, PR, review convergence, merge, causal closeout if needed, cleanup, and main sync.

## Historical Source Review

Read-only intent evidence includes:

- `tv203s/contrib/tvision/examples/terminal/terminal.cc`
- `tv203s/contrib/tvision/include/tv/terminal.h`
- relevant Cyrillic KOI8 example sources and setup scripts
- fonts `font.016`, `genraw.cc`, `test.cc`, and `ocr.sft`
- `tv203s/contrib/tvision/classes/fontcoll.cc`
- `tv203s/contrib/tvision/include/tv/fontcoll.h`
- relevant Eterm configuration/documentation and XTerm resources/documentation
- relevant Unix xterm display, keyboard, and screen implementation files

These files establish historical purpose only. Host font installation, keyboard
map changes, shell execution, terminal-resource mutation, and mechanical C/C++
translation are prohibited.

## Validation Trigger Matrix

| Touched surface | Required validation |
|---|---|
| Any file | `git diff --check`, placeholder/generated/secret/historical scans |
| C# source/tests | `dotnet format --verify-no-changes`, targeted Release tests |
| Shared executable session/view logic | Full Release suite and canonical five-assembly Coverlet gate |
| Public XML, guide, navigation, architecture/security docs | `docfx docfx.json`, then Playwright/axe and text-first review |
| Bash/PowerShell scripts | N/A unless scope changes; then parity, syntax, help, and man-page proof |
| Visible TUI | Real app loop, concrete state, view identity, and buffer/cell proof |
| Historical behavior | Relevant `.c`/`.cc` and necessary header review under read-only `tv203s/` |

Before every `dotnet build` or `dotnet test`, increment the manual Build field.
Batch related validations so each command adds distinct evidence instead of
administrative counter churn.

## Post-Design Constitution Re-check

Passed. The design adds no dependency, network, cloud, authentication, script,
runtime AI, visible example, process, shell, PTY, or host mutation. All new raw
data is bounded before publication, text-first status remains available, and
platform evidence is not overstated. A material need for a host process, native
font installation, another charset, full terminal emulation, a new project, or
a Wave-4 visible example stops local implementation and becomes follow-up work.

## Complexity Tracking

No Constitution violation or exceptional complexity is planned.
