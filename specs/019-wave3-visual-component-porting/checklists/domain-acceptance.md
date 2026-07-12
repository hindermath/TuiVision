# Domain Acceptance Checklist: Wave-3 Visual Component Porting

**Purpose**: Challenge the accepted scope before planning and implementation
**Created**: 2026-07-12
**Feature**: [spec.md](../spec.md)

## Scope and Baseline

- [X] **DA-001 - Exact example set**: Confirm only `BHelp`, `HelpDemo`, `I18n`,
  `TvEdit`, and `TvHc` are in implementation scope.
  **Durchführungshinweis**: Compare the final project list, task paths, and diff
  against FR-001 and reject Wave-4, TP7, or mouse-only additions.
- [X] **DA-002 - Feature 018 reuse**: Treat Feature 018 as a prerequisite, not
  a backlog to reimplement.
  **Durchführungshinweis**: Map every editor/help/resource/compiler need to an
  existing 018 type before accepting local example logic.
- [X] **DA-003 - Framework usage gate**: Every example receives exactly one of
  the four accepted framework decisions.
  **Durchführungshinweis**: Maintain one evidence row per example and fail the
  matrix for duplicates, empty decisions, or unapproved terms.

## Visible Runtime Proof

- [X] **DA-004 - Three-layer model**: Main area, status line, and description
  route are required for all five examples.
  **Durchführungshinweis**: Inspect the first rendered frame and trigger the
  description through a key or command; status text alone cannot pass.
- [X] **DA-005 - Real dispatch**: Primary proof uses `app.Run()` or the real
  event/command/key dispatch path.
  **Durchführungshinweis**: Trace the injected event to application handling and
  classify direct helpers as setup or supplemental only.
- [X] **DA-006 - Three proof layers**: State, view tree, and rendered cells are
  all required.
  **Durchführungshinweis**: Assert a domain value, find the concrete view type,
  and inspect expected buffer text or cells in a stable region after dispatch.
- [X] **DA-007 - Small viewport**: Constrained terminals fail honestly.
  **Durchführungshinweis**: Render at the repository's supported small test size
  and verify clipping/fallback is stable, text-first, and does not overlap.

## Controlled I/O and Failure Paths

- [X] **DA-008 - TvEdit ownership**: Editor proof cannot read or overwrite
  arbitrary user data.
  **Durchführungshinweis**: Use a source fixture for read proof and a unique
  test-temp directory for writes; assert safe-close decisions before cleanup.
- [X] **DA-009 - TvHc ownership**: Compiler proof uses controlled input and
  test-temp-only output.
  **Durchführungshinweis**: Assert the resolved destination is inside the
  test-owned directory and prove invalid input leaves no accepted output.
- [X] **DA-010 - Stable rejection**: Unknown contexts, missing keys, malformed
  sources, and truncated persisted data stay visible.
  **Durchführungshinweis**: Drive one negative case per affected demo and assert
  diagnostic class, visible status, unchanged safe state, and proof limit.
- [X] **DA-011 - Host-independent i18n**: Locale proof is deterministic.
  **Durchführungshinweis**: Request neutral, alternative, and unavailable
  languages explicitly; assert attempted order and matched key without changing
  process locale.

## Historical, Learning, and A11Y Evidence

- [X] **DA-012 - Historical read-only review**: Relevant `.cc`, headers,
  resources, PO files, and fixtures are cited.
  **Durchführungshinweis**: Record source path, retained intent, modern behavior,
  and intentional deviation; verify `git diff -- tv203s/` is empty.
- [X] **DA-013 - Proprietary BHelp boundary**: No implicit `.tch` decoder scope
  remains.
  **Durchführungshinweis**: Evidence must classify the modern help-model viewer
  as `IntentionalDeviation` and explain why the unsafe proprietary decoder is
  not needed for learner-visible intent.
- [X] **DA-014 - Text-first guides**: Five guides and the example index explain
  all proof layers without color or pointer dependence.
  **Durchführungshinweis**: Review German first, English second, CEFR-B2,
  keyboard path, status, fallback, historical source, and controlled I/O.
- [X] **DA-015 - Didactic comments**: Non-trivial new logic receives selective
  reason-focused comment review.
  **Durchführungshinweis**: Record whether each shared dispatch/proof block needs
  a why/trade-off/boundary comment; reject comments that restate code.

## Governance and Delivery

- [X] **DA-016 - Six-preset matrix**: Every applicable or `N/A` checkpoint has
  complete audit fields.
  **Durchführungshinweis**: Validate preset/version, owner, reviewer, result,
  residual risk, follow-up, evidence path, and re-evaluation trigger row by row.
- [X] **DA-017 - Exact delivery evidence paths**: Every remote task names its
  acceptance ledger.
  **Durchführungshinweis**: Search all push/PR/check/review/merge/sync tasks and
  require `specs/019-wave3-visual-component-porting/pr-evidence.md` or another
  exact repository path in each row.
- [X] **DA-018 - Triggered validation**: Test and documentation gates match the
  touched scope.
  **Durchführungshinweis**: Require targeted Wave-3 smokes, full Release tests,
  coverage, format, DocFX/A11Y for guides/navigation, secret scan, and remote CI.
- [X] **DA-019 - Merge authority boundary**: Remote actions never infer wider
  permission.
  **Durchführungshinweis**: Record `MergeAndSync` authority; permit bypass only
  after green required checks, zero actionable GraphQL threads, and a sole
  named human-approval block.

## Result

All checklist items are specified and actionable. No checklist finding requires
a scope clarification before planning.
