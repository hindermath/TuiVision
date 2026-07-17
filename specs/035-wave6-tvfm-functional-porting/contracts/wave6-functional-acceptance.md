# Wave-6 TVFM Functional Acceptance Contract

## 1. Closed historical scope

The accepted source set is exactly the 24 files directly under `TVFM/` at
Feature-035 start. Each path has exactly one role. `TVFM/`, `TVDEMOS/`,
`tv203s/` and external comparison sources remain byte-for-byte unchanged.

The accepted functional-area set is exactly:

1. `W6-001` application, desktop, menu, commands, status and help
2. `W6-002` directory tree, root and navigation
3. `W6-003` file list, sort, filter, tag and information
4. `W6-004` text and hex viewing
5. `W6-005` controlled search
6. `W6-006` safe copy, rename, delete and attribute decisions
7. `W6-007` drag/drop intent and keyboard fallback
8. `W6-008` internal association and viewer decisions
9. `W6-009` progress, abort, error and recovery
10. `W6-010` palette, configuration and resources

No additional row can replace a missing member.

## 2. Controlled workspace contract

- Every operation receives a workspace-relative path.
- Canonical resolution must remain below the bound root.
- Existing path segments must not be symbolic links or reparse points.
- The process current directory is not used as implicit authority and is not
  permanently changed.
- Normal execution uses a disposable copy of source-controlled fixtures.
- Tests use test-owned temporary roots.
- No arbitrary user, network, device or external-checkout content is read.

## 3. Runnable application contract

`Tp7FileManager`:

- has one independent executable project;
- starts normally and with `--smoke`;
- shows purpose, controlled root, current relative path and deterministic state;
- exposes navigation, list, preview, search and prepared mutation paths;
- provides a real status line and F1/Description;
- provides complete keyboard access and controlled quit;
- launches no process, shell, PTY or external viewer.

## 4. Primary proof contract

Primary application proof:

1. constructs the real application with deterministic buffer/driver state;
2. queues events or commands through the application event path;
3. calls `app.Run()` or a proven equivalent real dispatch loop;
4. asserts concrete workspace/application state;
5. asserts concrete view and focus identity;
6. asserts visible status and buffer/cell content;
7. asserts controlled exit.

Direct workspace calls are primary only for filesystem security conditions
that cannot be proven safely through UI alone. They remain paired with at
least one real app-loop proof for the corresponding user flow.

## 5. Read and search contract

- Directory snapshots are stable and root-relative.
- Filtering, sorting and tagging preserve deterministic selection semantics.
- Text and hex previews read no more than 4 KiB and disclose truncation.
- Search depth is at most 8, visits at most 256 files and returns at most 100
  results.
- Cancellation returns a consistent partial result.
- Association decisions are only text, hex or visible fallback.

## 6. Mutation contract

- Copy, rename, delete and read-only changes require a prepared intent and
  explicit confirmation.
- Cancel and missing confirmation perform no mutation.
- Source metadata, target conflict, root and link boundaries are revalidated
  before execution.
- Existing targets are never silently overwritten.
- Recursive directory mutation is not supported.
- Every terminal result states progress, affected relative paths, error and
  recovery boundary.
- Drag/drop prepares the same operation intent as its complete keyboard
  fallback; it does not bypass confirmation.

## 7. Framework decision contract

Each `W6-###` area has exactly one:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

`SmallFrameworkFix` requires prior red evidence. `FollowUpHardening` stops the
affected slice and records owner, scope and later intake boundary. Unsafe
filesystem behavior cannot be accepted as an intentional deviation.

## 8. Stage-2 contract

The final matrix has exactly one `Tp7FileManager` row with one:

- `ShowcaseComplete`
- `ShowcaseDelta`
- `IntentionalMinimalSurface`
- `ProductDecision`

A later showcase intake may be derived from a concrete `ShowcaseDelta`, but
Feature 035 does not create or start Feature 036. `ProductDecision` stops
delivery.

## 9. Documentation and A11Y contract

The learner guide is semantic DE-first/EN-second CEFR-B2 content covering
purpose, source, launch, keyboard operation, safety boundary, modernization,
platform fallback, tests and Stage-2 disposition. Application state is
text-first and does not rely only on color or pointer input. DocFX and
Playwright/Axe must pass.

## 10. Delivery contract

The exact reviewed head must pass all declared local and provider gates.
Missing reviewers remain missing, not passed. Merge requires zero actionable
threads. The narrow authorized bypass applies only when Human Approval is the
sole open rule and every technical gate is green.

After merge, the repository returns to clean synchronized `main`. A causal
closeout is allowed only for post-merge facts that cannot truthfully exist on
the reviewed feature head.
