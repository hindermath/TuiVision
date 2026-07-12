# Acceptance Contract: Mouse Support and Interaction

## Ingress Contract

- Accept only complete, bounded SGR-1006 left press, pressed move, and release reports.
- Validate syntax, numeric size, one-based coordinates, buffer bounds, capability,
  and phase order before creating any `TEvent`.
- Publish exactly zero or one canonical event per observation.
- Reject malformed or unsupported input atomically while preserving the next
  independent valid observation.
- Disable reporting and clear transient state on capability loss and shutdown.

## Interaction Matrix

| Area | Required route | Required outcome | Negative/fallback proof |
|---|---|---|---|
| Click focus | left press through app loop | one topmost eligible target becomes group focus | covered, hidden, disabled, or non-selectable target is not focused |
| Activation | focused control receives normal mouse event | existing command fires exactly once | outside/invalid/duplicate input fires none |
| Double click | second qualifying left press | one event carries `DoubleClick=true` | time >500 ms, other cell/button/target stay single |
| Window drag | title press, moves, release | one movable window stays within owner bounds | Escape/capability loss/removal/shutdown clears drag |
| Keyboard fallback | existing keys/commands | same mandatory task remains operable | disabled/unsupported mouse does not consume keys |

## Host Contract

- macOS/Linux interactive SGR terminals and WSL are the supported family.
- Native Windows Console, redirected/headless sessions, and unknown terminals are
  explicit `Unsupported` unless current evidence proves otherwise.
- Deterministic injection proves parser and state contracts, not physical host I/O.
- Each host row records terminal, capability, evidence class, result, risk, and trigger.

## Primary Proof Contract

At least one integration test runs the real app loop and combines:

1. queued controlled host observations,
2. two focusable controls and one movable window,
3. concrete focus, activation-count, double-click, and drag assertions,
4. concrete view-tree identity,
5. visible status plus stable buffer/cell assertions,
6. keyboard-equivalent operation with mouse disabled or unsupported.

Direct parser calls and helper methods are setup or supplemental proof only.

## Framework Usage Contract

Each area receives exactly one decision: `UseExistingFramework`,
`SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening`. No parser,
event model, or reusable interaction logic may live in an example project.

## Security and A11Y Contract

Raw input is untrusted and fail-safe. Essential state is text-visible and never
depends only on pointer, color, or layout. Keyboard paths remain complete.
Learner-facing docs are German-first/English-second CEFR-B2 and text-first.

## Delivery Contract

Every push, PR, check, review, merge, cleanup, closeout, and local-sync task names
`specs/020-mouse-support-interaction/pr-evidence.md`. Missing reviewer capacity
is missing review, never success. A bypass requires explicit authority, green
required checks, zero actionable GraphQL threads, and only the named human rule.
