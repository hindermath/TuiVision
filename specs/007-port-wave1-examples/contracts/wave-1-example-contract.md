# Contract: Wave-1 Example Delivery Surface

## Purpose

Define the reviewable contract for the first mandatory example wave. The
contract fixes what reviewers, smoke tests, and guides must be able to observe
from the managed `desklogo`, `msgcls`, `tutorial`, and `videomode` deliveries
without freezing every internal helper name.

## Managed Example Delivery Contract

### Wave-1 Scope

- The contract covers exactly four mandatory original example scopes from
  `tv203s/contrib/tvision/examples`: `desklogo`, `msgcls`, `tutorial`, and
  `videomode`.
- Later mandatory waves and the follow-on `TVDEMOS/` and `TVFM/` scope remain
  outside this contract.

### Launch Surface

- `desklogo`, `msgcls`, and `videomode` each provide one canonical managed
  example entry point under `examples/`.
- `tutorial` provides one canonical managed entry point under `examples/` plus
  stable selector tokens for all 16 original lessons:
  `tvguid01`, `tvguid02`, `tvguid03`, `tvguid04`, `tvguid05`, `tvguid06`,
  `tvguid07`, `tvguid08`, `tvguid09`, `tvguid10`, `tvguid11`, `tvguid12`,
  `tvguid13`, `tvguid14`, `tvguid15`, and `tvguid16`.
- A reviewer must be able to launch each wave-1 example, and each tutorial step,
  through a documented and deterministic path.
- Smoke validation may use a test-callable in-process seam, but that seam must
  still exercise the real example host contract rather than a disconnected mock
  workflow.

### Behavioral Guarantees

1. **Scope guarantee**: Each managed example preserves the primary teaching
   purpose of its corresponding historical original example.
2. **Primary-application guarantee**: The mandatory delivery target is the
   primary example application. Historical helper tools or generators are only
   required when they are necessary for visible behavior, assets, or repeatable
   smoke validation.
3. **Tutorial-step guarantee**: All 16 tutorial steps remain individually
   addressable and smoke-testable even though they are delivered through one
   shared managed tutorial project.
4. **Smoke-validation guarantee**: Every wave-1 example has repository-visible
   MSTest smoke coverage; `tutorial` has one smoke scenario per original step.
5. **Guide guarantee**: `desklogo`, `msgcls`, and `videomode` each have their
   own guide page, while `tutorial` has one shared guide page with clearly
   separated sections for all 16 steps.
6. **Videomode guarantee**: `videomode` prefers a real terminal-supported
   change when the runtime allows it and otherwise presents an explicit visible
   fallback instead of silently degrading.
7. **Exit-path guarantee**: Every example smoke path proves a documented clean
   exit rather than leaving shutdown behavior implicit. "Clean exit" means no
   forced process kill and no hanging pending interaction remains.

## Test and Review Obligations

- Wave-1 implementation starts with failing MSTest smoke coverage before
  production code is added.
- `tests/TuiVision.Examples.SmokeTests/` is the canonical smoke-validation home
  for this increment.
- Reviewers must be able to trace each wave-1 delivery back to its historical
  source folder under `tv203s/contrib/tvision/examples`.
- Example guides under `docs/guides/examples/` are first-class acceptance
  artifacts, not optional documentation extras.
- `Pflichtenheft.md` and `docs/project-statistics.md` must be updated when the
  implementation lands so repository-level progress reflects the delivered wave.
- If plan-derived agent guidance changes while the wave lands, the synchronized
  agent-guidance files must be updated in the same work item rather than as a
  later cleanup.
