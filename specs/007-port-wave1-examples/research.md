# Research: Mandatory Example Wave 1 Ports

## Decision 1: Deliver wave 1 as four managed example projects, with one shared tutorial project

- **Decision**: Create dedicated managed example deliveries for `desklogo`,
  `msgcls`, and `videomode`, and keep `tutorial` as one shared managed project
  that exposes all 16 original steps through stable step tokens
  `tvguid01` through `tvguid16`.
- **Rationale**: This preserves the mandatory original-example scope while
  avoiding 16 extra solution projects for the tutorial family. The shared
  project keeps tutorial-wide helpers and documentation coherent, but still
  allows every original step to remain individually runnable and testable.
- **Alternatives considered**:
  - Create 19 separate example projects: rejected because project sprawl would
    grow faster than the actual behavioral differences justify.
  - Collapse all tutorial steps into one undifferentiated demo: rejected
    because the specification requires all 16 original steps to stay distinct
    and individually smoke-tested.

## Decision 2: Use the existing example smoke test project as the canonical validation home

- **Decision**: Replace the current module-oriented placeholder tests in
  `tests/TuiVision.Examples.SmokeTests/` with real example-focused MSTest smoke
  scenarios for the four wave-1 example scopes.
- **Rationale**: The repository already has a dedicated example-smoke test
  project. Reusing it keeps example validation discoverable, lets CI continue
  to target one stable project for smoke coverage, and avoids fragmenting
  example acceptance logic across multiple test projects.
- **Alternatives considered**:
  - Create one test project per example: rejected because wave 1 is one
    delivery unit and shared smoke helpers would be duplicated.
  - Keep the current module-smoke tests unchanged: rejected because they do not
    prove any real example behavior.

## Decision 3: Prefer in-process smoke seams over brittle interactive terminal automation

- **Decision**: Design each example with a deterministic in-process startup and
  validation seam that smoke tests can exercise directly, while still keeping a
  normal user-facing executable entry point.
- **Interpretation note**: "In-process seam" means a test-callable startup
  surface for the real example host. It does not permit replacing the example
  with a disconnected mock workflow that bypasses the documented launch and
  exit contract.
- **Rationale**: Interactive terminal automation is fragile across macOS,
  Linux, and Windows/WSL. In-process smoke seams keep CI stable, shorten the
  Red-Green loop, and still verify the defining behavior and clean shutdown of
  each example.
- **Alternatives considered**:
  - Drive every smoke scenario through spawned console processes only: rejected
    because timing and terminal capability differences would make the suite
    brittle.
  - Skip smoke tests and rely on manual example runs: rejected by the
    constitution and the feature specification.

## Decision 4: Treat historical helper utilities as optional support scope, not automatic mandatory ports

- **Decision**: Port only the primary example application by default. Include
  helper utilities, asset generators, or side tools from the original example
  folders only when they are required for visible behavior, assets, or
  repeatable smoke validation in the managed port.
- **Rationale**: This matches the specification clarification and keeps wave 1
  focused on user-visible example delivery rather than on historical build-time
  tooling that may no longer be needed in .NET.
- **Alternatives considered**:
  - Port every helper program unconditionally: rejected because it expands the
    wave without guaranteed user value.
  - Ignore helper provenance entirely: rejected because some helpers may still
    be needed for assets or reproducible test paths.

## Decision 5: Make `videomode` capability-driven with a visible fallback contract

- **Decision**: `videomode` attempts real terminal-supported size or mode
  transitions first and must present an explicit visible fallback when the
  runtime cannot reproduce the historical behavior.
- **Rationale**: This preserves the example's teaching intent without forcing
  unsupported low-level behavior on modern managed terminals. The fallback must
  be visible so that the example remains reviewable and educational rather than
  silently degraded.
- **Alternatives considered**:
  - Require exact historical mode switching everywhere: rejected because modern
    terminals do not expose uniform capabilities across platforms.
  - Simulate the behavior only: rejected because the specification requires
    real supported transitions where available.

## Decision 6: Deliver four guide surfaces, with one shared tutorial guide page

- **Decision**: Provide one guide page each for `desklogo`, `msgcls`, and
  `videomode`, plus one shared `tutorial` guide page with clearly separated
  sections for `tvguid01` through `tvguid16`.
- **Rationale**: The specification already fixes this documentation shape. One
  shared tutorial guide keeps the step sequence readable, while still letting
  reviewers find each step's learning goal, startup path, and exercise hints.
- **Alternatives considered**:
  - Create 16 tutorial guide pages: rejected because it spreads one learning
    sequence too thinly across the documentation tree.
  - Use one minimal tutorial overview with no step detail: rejected because it
    would underserve the didactic requirement.

## Decision 7: Reflect wave progress explicitly in project-tracking artifacts

- **Decision**: Treat `Pflichtenheft.md` and `docs/project-statistics.md` as
  mandatory follow-through artifacts for the implementation phase, not optional
  reporting extras.
- **Interpretation note**: Until implementation work lands, the current state
  of those files is baseline context only and must not be misread as partial
  delivery evidence for wave 1.
- **Rationale**: The Pflichtenheft next-step marker and the statistics ledger
  are both project-governance surfaces named in repository instructions. If
  wave 1 ships without synchronized tracking updates, reviewers cannot tell
  whether the project actually advanced its mandatory example obligations.
- **Alternatives considered**:
  - Update tracking later in a separate cleanup change: rejected because the
    repository rules require synchronized tracking when work lands.
  - Treat planning artifacts alone as sufficient progress evidence: rejected
    because they do not update the user-facing project status surfaces.

## Decision 8: Keep agent-guidance synchronization explicit when plan-derived context changes

- **Decision**: If wave-1 work changes shared agent guidance, active
  technologies, or project structure, synchronize
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md` in the same work item.
- **Rationale**: The repository already treats shared agent guidance as a
  synchronized surface. Example-wave work touches plan-derived technology and
  workflow context, so the follow-through rule must stay explicit instead of
  relying on oral convention.
- **Alternatives considered**:
  - Leave agent-guidance synchronization implicit in repo-wide instructions:
    rejected because reviewers could miss the coupling while focusing on the
    example artifacts only.
  - Update only the currently active agent file: rejected because it recreates
    the partial-synchronization risk the repository guidance already forbids.
