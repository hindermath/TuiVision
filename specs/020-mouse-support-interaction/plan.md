# Implementation Plan: Mouse Support and Interaction Hardening

**Branch**: `020-mouse-support-interaction` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)
**Input**: `Lastenheft_04_MouseSupportAndInteraction.md` and the accepted Feature-019 baseline

## Summary

Add one bounded SGR-1006 host ingress to the existing console driver and route
validated events through the existing `TEvent` and app-loop contracts. Harden
click focus and exactly-once activation, deterministic double-click detection,
and exactly one drag interaction: moving a movable `TWindow` by its title row.
All required operations retain keyboard routes and visible text status. Native
Windows Console, wheel, hover, touch, arbitrary buttons, and complete terminal
protocol parity remain explicit follow-ups.

## Technical Context

**Language/Version**: C# 14 on .NET 10
**Primary Dependencies**: Existing `TuiVision.Core`, `TuiVision.Controls`, and `TuiVision.Drivers.Console`; no new packages
**Storage**: In-memory event, click, drag, and evidence state only
**Testing**: MSTest 4, deterministic parser/state tests, real app-loop integration proof, Coverlet, DocFX, Playwright with axe
**Target Platform**: SGR-1006 terminals on macOS/Linux and WSL; native Windows Console is an honest unsupported boundary
**Project Type**: Multi-project terminal UI framework
**Performance Goals**: One canonical event per accepted observation; bounded sequence size and no unbounded parser wait
**Constraints**: Keyboard-first, no new dependencies, no example-local parser, no `tv203s/` edits, exactly one drag target, no Wave-4 scope
**Scale/Scope**: Core event validation, console ingress/capability, group focus routing, window drag, one integration harness, guides/evidence/governance

## Constitution Check

*GATE: Passed before research and re-checked after design.*

- **Level-2 environment**: .NET 10, MSTest, coverage, DocFX/A11Y,
  statistics, versioning, and five agent surfaces remain binding.
- **Memory-safe language**: C#/.NET is approved; historical C/C++ is read-only.
- **Secure coding**: Raw terminal input is untrusted, size-bounded, fully
  validated before publication, and atomically rejected on malformed state.
- **Architecture**: Driver owns host protocol and click classification, Core
  owns canonical events, Controls own focus/activation/drag semantics.
- **NIST SSDF / CWE Top 25**: Applicable to parser boundaries, integer ranges,
  state transitions, fail-safe deactivation, tests, and review evidence.
- **ASVS**: `N/A`; no web/API/auth surface. Re-evaluate if one appears.
- **Supply chain**: Existing SBOM/VEX/SLSA/OpenSSF evidence remains authoritative;
  no package, distribution, lockfile, or provenance change is planned.
- **AI-SBOM / regulation**: `N/A`; no runtime/product AI or regulated operated service.
- **STRIDE/CIA/CAPEC**: Proportional review applies to terminal spoofing,
  malformed input, duplicate dispatch, stale state, and availability limits.
- **S-ADR / arc42**: Existing driver boundary is extended without a new
  deployment or trust boundary; create no new artifact unless implementation deviates.
- **Zero Trust / SAMM / BSI C3A / BSI C5**: `N/A`; no cloud, provider,
  distributed service, or operations boundary changes.
- **iSAQB**: Component ownership, quality goals, and deliberate protocol limits
  are explicit; a parallel UI mouse abstraction is prohibited.
- **A11Y**: Keyboard completeness, visible status, text-first guides, WCAG 2.2
  AA review, and DocFX/axe proof are applicable.
- **Didactic comments**: New parser, state-machine, dispatch, and proof logic is
  reviewed for concise why/trade-off/proof-boundary comments.
- **Cross-platform**: Host classification and fallbacks are applicable; script
  governance is `N/A` because no scripts are planned.
- **Agent parity**: Active feature context changes; all five maintained agent
  surfaces are synchronized at completion.
- **Autonomous task rule**: Every remote task names
  `specs/020-mouse-support-interaction/pr-evidence.md` as acceptance ledger.
- **Versioning**: Increment only the manual build component before each build
  or test; align all fields to `1.20.<branch-commit-count>.<build>` before commit/push.

### Governance Checkpoint Matrix

| Domain | Planned applicability | Evidence boundary |
|---|---|---|
| NIST SSDF / CWE Top 25 | Applicable | Parser, state, dispatch, tests, feature evidence |
| OWASP ASVS | N/A unless web/API/auth appears | Existing ASVS ledger plus trigger row |
| SBOM / VEX / SLSA / OpenSSF | Existing baseline; no new artifact | Supply-chain ledger plus feature row |
| AI-SBOM / NIS2 / CRA / EU AI Act / DORA | N/A for local non-AI training framework | Feature row with re-evaluation trigger |
| STRIDE / CIA / CAPEC | Applicable to untrusted host input and state/availability | Threat/evidence rows |
| S-ADR / arc42 / Zero Trust / SAMM | N/A for new artifacts absent architecture change | Existing architecture/security evidence |
| BSI C3A / BSI C5 | N/A; no cloud/provider boundary | Cloud applicability ledgers plus feature row |
| iSAQB architecture | Applicable | Plan, research, component decisions |
| A11Y | Applicable | Keyboard fallback, status, guides, DocFX/axe |
| Cross-platform | Host/fallback proof applicable; script governance N/A | Host matrix and CI evidence |
| Agent parity | Applicable | Five synchronized agent surfaces |

## Project Structure

### Documentation

```text
specs/020-mouse-support-interaction/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── mouse-interaction-acceptance.md
├── checklists/
├── tasks.md
└── pr-evidence.md
```

### Runtime and Tests

```text
src/TuiVision.Core/TEvent.cs
src/TuiVision.Drivers.Console/
├── ConsoleMouseIngress.cs
├── TConsoleDriver.cs
└── DriverCapabilityMap.cs
src/TuiVision.Controls/
├── TProgram.cs
├── TGroup.cs
├── TView.cs
└── TWindow.cs
tests/TuiVision.Core.Tests/
tests/TuiVision.Drivers.Tests/
tests/TuiVision.Controls.Tests/
docs/guides/mouse-support.md
```

**Structure Decision**: The console driver owns SGR framing, capability, and
click classification. It emits only existing `TEvent` values. `TProgram` owns
terminal lifecycle integration; `TGroup` owns hit/focus routing; `TWindow` owns
the single title-drag session. No example project receives reusable mouse logic.

## Phase 0: Research Decisions

1. Use complete SGR-1006 sequences only; reject partial, oversized, unknown,
   wheel, and invalid transition input atomically.
2. Support interactive macOS/Linux terminals and WSL when SGR mode is available;
   keep native Windows Console and redirected/headless input unsupported.
3. Preserve `TEventKind` and `TMouseEvent` as the sole UI contract.
4. Detect double-click in the ingress using injected monotonic milliseconds,
   same left button, same cell, same target identity, and `<= 500 ms`.
5. Route mouse down to the topmost visible eligible hit target and transfer
   focus through `TGroup.SetFocus` before normal activation.
6. Use movable `TWindow` title-row dragging as the only drag contract; clamp to
   owner bounds and preserve `Ctrl+F5` plus arrows as keyboard fallback.
7. Cancel drag on release, Escape, disable/capability loss, target removal, and shutdown.
8. Use deterministic injected sequences as CI proof; label physical host checks
   accurately and never infer hardware validation from parser tests.

## Phase 1: Design

### Runtime Design

- `ConsoleMouseIngress` holds capability state, bounded SGR parsing, pressed
  button state, and last-click state. It publishes no event until the complete
  sequence and transition are valid.
- `TConsoleDriver` owns the ingress and a controlled input queue used by the
  real `TProgram.GetEvent` route in tests and by host sequence collection. A
  caller-supplied point-to-target-key delegate lets the Driver compare click
  targets without referencing Controls or exposing view objects.
- `TProgram.Run` enables SGR reporting only for supported interactive hosts and
  always disables it during cleanup. `GetEvent` checks mouse input before normal
  keyboard translation without changing keyboard semantics.
- `TView` global/local coordinate conversion includes its owner chain so nested
  hit testing uses actual screen coordinates.
- `TGroup` selects one topmost visible hit target. Selectable targets receive
  group focus before the same event continues through their existing handler.
- `TWindow` starts drag only on left `MouseDown` in the top title row, updates
  on valid pressed movement, clamps inside its owner, and commits on release.

### Vertical Slice

1. Create evidence and complete the compile-surface review.
2. Add one failing Driver matrix for valid press/move/release, malformed input,
   and exact double-click boundaries.
3. Implement the bounded ingress and prove one event per accepted observation.
4. Add an app-loop harness with two controls and one movable window.
5. Prove click focus/activation, title drag, visible state/cells, and keyboard fallback.
6. Spread negative, host, cancellation, documentation, and governance proof.

### Proof Design

- Driver matrix records raw sequence, capability, timestamp, previous state,
  result, event payload, rejection reason, and remaining-stream boundary.
- Interaction matrix records target identity, focus before/after, command count,
  drag state/bounds, keyboard equivalent, status text, view identity, cells, and result.
- Host matrix distinguishes `Pass`, `Unsupported`, `NotRun`, and
  `FollowUpHardening`; deterministic injection never becomes physical-host proof.
- Primary integration proof runs `app.Run()` with queued host observations and
  keyboard events, then asserts state, view tree, status, and rendered cells.

### Documentation and Governance Design

- Add one bilingual mouse-support guide and DocFX navigation entry.
- Update driver capability evidence, security/architecture applicability,
  Pflichtenheft next marker, all five agent contexts, and project statistics.
- Archive the binding Lastenheft after all acceptance and validation gates pass.
- Keep any generic autonomous-workflow refinement on a separate retrospective PR.

## Phase 2: Implementation Order

1. Evidence schema, historical inventory, framework/host/governance matrices.
2. Compile-surface review and failing Driver vertical-slice matrix.
3. SGR ingress, capability lifecycle, exactly-once mapping, and targeted proof.
4. Failing Core/Controls focus and coordinate tests, then smallest framework fixes.
5. Failing window-drag/cancellation tests, then the single drag implementation.
6. Real app-loop integration, visible status, view/cell proof, and keyboard fallback.
7. Host matrix, negative/fuzz-style bounded cases, comments, and framework decisions.
8. Guide, DocFX navigation, security/architecture evidence, Pflichtenheft, agents, statistics.
9. Static, targeted, full Release, coverage, DocFX/A11Y, secrets, and hygiene gates.
10. Archive, version, commit, push, PR, review convergence, merge, cleanup, and main sync.

## Post-Design Constitution Re-check

Passed. The design adds no dependency, network, cloud, authentication, script,
runtime AI, new example, or broad terminal-emulation scope. The only new raw
input is bounded before publication. Keyboard and unsupported-host behavior are
first-class. A material need for native Windows input, a second drag target, or
a parallel event model stops local implementation and becomes follow-up work.

## Complexity Tracking

No Constitution violation or exceptional complexity is planned.
