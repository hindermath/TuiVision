# Contract: A11Y Framework Acceptance

## Public Contract

- `IAccessibleWidget` is public, opt-in and fully XML documented.
- Focus announcements are immutable snapshots carried by the existing
  `cmFocusChanged` broadcast and preserve compatibility for legacy consumers.
- Shortcut results are read-only values; querying cannot execute commands or
  mutate menu/status structures.
- `TColorScheme.HighContrast` is public, named, immutable and explicitly
  applied; default behavior is untouched.

## Behavioral Contract

1. Actual focus transition: one target, one announcement, optional truthful label.
2. Same-target focus request: no additional transition announcement.
3. Menu/status query: only enabled executable entries, with source identity.
4. Keyboard matrix: each selectable family has Proof or named N/A per key class.
5. High Contrast: visible semantic differences plus text status; no colour-only meaning.
6. Reference app: real loop, deterministic quit, standard/narrow viewport,
   state/view/cell proof and keyboard-reachable Description.

## Validation Contract

- `git diff --check` and `dotnet format --verify-no-changes` pass.
- Targeted Core, Controls and example smokes pass in Release.
- Full Release suite passes.
- Canonical Coverlet gate is at least 70 % for all five required assemblies.
- DocFX has zero warnings/errors and Playwright/Axe passes.
- Generated output, secrets and `tv203s/` changes are absent.
- Required remote checks are green and actionable review threads are zero.

## Scope Contract

No native accessibility bridge, full control migration, terminal-wide WCAG
claim, new dependency, Wave 1-4 remediation or Feature 024 implementation.
