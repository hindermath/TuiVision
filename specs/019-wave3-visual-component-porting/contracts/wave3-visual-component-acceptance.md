# Acceptance Contract: Wave-3 Visual Components

## Example Matrix

| Example | Required main surface | Required operation | Required negative/fallback proof | I/O boundary |
|---|---|---|---|---|
| `TvEdit` | `TEditWindow` with real editor content | App-loop edit plus safe close | Unsaved-change rejection or decision | Fixture read; test-temp write only |
| `BHelp` | `THelpWindow` with visible topic | Context/topic navigation | Unknown context or missing target | Embedded/source-controlled help only |
| `HelpDemo` | Focusable help-context composition | Focus/help command dispatch | Unknown context fallback | In-process only |
| `I18n` | Visible localized resource composition | Explicit language command | Missing language/key fallback | In-process dictionaries only |
| `TvHc` | Compiler/source/result composition | Compile command | Invalid source diagnostic | Controlled source; test-temp write only |

## Three-Layer Contract

Every row passes only when all are true:

1. The main surface is a real visible domain component or stable result.
2. A real status line identifies current identity/result and next operation.
3. A keyboard-reachable description explains purpose, operation, historical
   intent, modern deviation, and A11Y boundary in German first and English second.

## Primary Proof Contract

Every example has at least one test that:

- runs the real application loop or equivalent dispatch path,
- injects the documented event, command, or key,
- asserts concrete post-dispatch domain state,
- finds the expected concrete view in the view tree,
- verifies visible text/cells in a stable rendered region,
- verifies status and description,
- classifies any direct helper as setup or supplemental,
- records its proof limit.

Startup-only, status-only, screenshot-only, and direct-helper-only tests fail this contract.

## Framework Usage Contract

Each example has exactly one primary decision:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

No other value is accepted. Reusable domain behavior cannot remain in local
example helpers. Historical files remain read-only.

## Controlled Artifact Contract

- Normal startup uses embedded or source-controlled learning content.
- Tests own every writable destination and clean it after proof.
- No home-directory, recent-file, shell, network, or arbitrary current-directory discovery.
- Rejected compiler input produces no accepted partial output.
- Unsaved editor state requires an explicit close decision.

## Documentation and A11Y Contract

Five guides and the example index describe startup, keyboard route, main
surface, status, description, fallback, controlled I/O, historical source,
framework decision, and proof. Essential meaning does not depend on color,
layout, mouse, screenshot, or animation.

## Delivery Contract

Every push, PR, check, review, merge, branch-cleanup, closeout, and local-sync
task names `specs/019-wave3-visual-component-porting/pr-evidence.md` as its
acceptance ledger. Missing reviewer capacity is recorded as missing review,
never success. A bypass requires current explicit authority, green required
checks, zero actionable GraphQL threads, and exactly one named residual rule.
