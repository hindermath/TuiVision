# Acceptance Contract: Wave-1 Visual Component Remediation

## Contract Purpose

This contract defines the reviewable runtime, proof, historical, documentation,
and governance conditions for feature 017. It does not add a public network API.

## C1 - Scope Contract

1. Covered areas are exactly Desklogo, MsgCls, Tutorial `tvguid01` through
   `tvguid16`, and Videomode.
2. Feature 014 remains the functional baseline.
3. Wave-2/3/4 behavior, mouse requirements, dependencies, broad framework work,
   external services, persistence, runtime AI, and historical-source edits are
   excluded.

## C2 - Three-Layer Runtime Contract

Each covered example area provides:

1. A real visible main component or stable visible runtime state.
2. A real `TStatusLine`; an equivalent area is accepted only with documented
   historical or framework rationale.
3. A keyboard-reachable `Help -> Description` path explaining visible intent,
   operation, status, and historical boundary in German first and English second.

Static text alone cannot replace the main component.

## C3 - Per-Example Contract

| Area | Required visible state | Required operation | Required boundary |
|---|---|---|---|
| Desklogo | Logo/desktop or honest clipped/fallback state | Description and stable quit | No artificial logo mutation; generator files remain asset context |
| MsgCls | Message window and current routing result | Menu/key command through broadcast; repeatable | Modern message-window deviation documented |
| Tutorial | 16 token-specific goals and representative components/states | Token selection, step operation/description, quit | No full historical re-port; default and unknown token explicit |
| Videomode | `supported`, `fallback`, `rejected`, or `unchanged` | Visible probe/retry route | No unsupported capability claim; post-operation usability |

## C4 - Primary Smoke Contract

A primary smoke row is accepted only when it records and passes:

1. Real application-loop execution.
2. Event, command, or key route matching the documented operation.
3. Concrete runtime state assertion.
4. View-tree type or identity assertion.
5. Rendered buffer/cell assertion in a stable region.
6. Status-line proof.
7. Description-path proof where the scenario covers description.
8. Direct-helper classification that is not primary-helper-only.

If one render layer is technically impossible, the row must record the reason,
substitute proof, limitation, owner/follow-up, and re-evaluation trigger. Startup,
`VisibleText`, history, private state, or helper output alone never satisfies C4.

## C5 - Tutorial Matrix Contract

- Exactly 16 valid token rows exist.
- Every row has one matching historical source, sequence, goal, representative
  kind, visible result, status, description, smoke method, and render proof.
- Token/sequence pairs are unique and contiguous.
- Generic duplicated results fail acceptance.
- Default launch and unknown-token fallback have separate proof.

## C6 - Framework Usage Contract

Every example and shared contract area records exactly one decision:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

Reusable behavior may not remain duplicated across example projects.
`SmallFrameworkFix` needs focused tests. `FollowUpHardening` may not be used to
hide a required primary visual state.

## C7 - Historical Contract

- Required `.c`, `.cc`, `.cpp`, and relevant headers under `tv203s/` are reviewed.
- `tv203s/` has zero changed files.
- Every newly visible deviation records source, modern behavior, rationale, and
  learner-visible effect.
- Asset/build helper files are considered only when they explain source,
  startup, generator, or intent boundaries.

## C8 - Documentation And A11Y Contract

- Four guides and `examples/README.md` match the delivered runtime and keys.
- German-first/English-second CEFR-B2 order is preserved.
- Status and description remain text-first and keyboard reachable.
- Essential meaning does not depend only on color, layout, or pointer input.
- DocFX completes with zero errors; warnings are resolved or explicitly bounded.
- Playwright/axe passes representative generated pages under WCAG 2.2 AA rules.
- Generated `_site/`, API YAML, caches, and test output remain untracked.

## C9 - Governance Contract

The feature evidence contains complete rows for all six preset versions and all
named security, architecture, cloud, regulatory, A11Y, cross-platform, and
agent-parity checkpoints. Every `N/A` has rationale and re-evaluation trigger;
every `Open` has owner and concrete follow-up. Empty starter rows fail acceptance.

## C10 - Validation Contract

Before each build/test command, increment the manual build counter and keep
`Version`, `AssemblyVersion`, and `FileVersion` aligned to `1.17.patch.build`.
Acceptance requires:

- `git diff --check`
- `dotnet format --verify-no-changes`
- targeted Wave-1 Release smokes
- full Release tests
- canonical Coverlet gate with all five assemblies at or above 70% line coverage
- `docfx docfx.json`
- `tests/web-a11y` Playwright/axe path
- remote macOS/Linux/Windows evidence where workflows provide it

## C11 - Delivery Contract

- `docs/project-statistics.md`, completion routing, and all affected agent
  surfaces are synchronized.
- The Lastenheft is archived with `.017-wave1-visual-component-remediation.md`.
- PR evidence names changed files, decisions, validations, conditional `N/A`
  checks, residual risks, and follow-ups.
- Merge and local `main` synchronization are recorded only after they actually occur.
