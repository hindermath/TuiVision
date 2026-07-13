# Quickstart: Implement and Prove Feature 026

## 1. Preflight

```bash
git branch --show-current
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
rg -n '^\- \[ \]' specs/026-component-data-conformance-hardening/checklists
```

Expected branch: `026-component-data-conformance-hardening`. Do not begin an
implementation edit until `pr-evidence.md` exists and every checklist is
complete.

## 2. Read-only intent review

Review the relevant `tv203s/` implementation and headers, the pinned external
Free Vision files `FV006`, `FV007`, `FV010`, `FV012`, and consumer evidence in
`TVDEMOS/` and `TVFM/`. Never modify or vendor these sources.

## 3. Test-first slices

1. Add the complete `F010` Red matrix for unrelated commands and child rejection.
2. Implement completion classification and hierarchical validation; run the focused Green matrix.
3. Add and close the `F011` edit/focus/acceptance matrix.
4. Add and close the `F012` file-mode matrix using only test-owned temporary directories.
5. Add and close the `F013` roundtrip/reconstruction and malformed-input matrices.
6. Update finding evidence immediately after each Green slice.

Before each explicit `dotnet build` or `dotnet test`, increment only the manual
build counter in `Directory.Build.props`. Prefer one targeted project invocation
per counter increment.

## 4. Candidate and validation

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
```

Run targeted and full Release tests plus the canonical Coverlet gate after the
required build-counter increments. Because public API/XML changes are expected,
also run:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Stage the intended candidate, run `git diff --cached --check`, reconcile staged
and unstaged status, and use a temporary index for any candidate validator that
requires a worktree. Do not validate a different unstaged tree.

## 5. Closeout

- Reconcile `F010`–`F013` in Feature-024 artifacts only after Green proof.
- Update `Pflichtenheft.md`, agent context if shared guidance changed,
  `docs/project-statistics.md`, and all feature evidence.
- Archive the Lastenheft with the numbered feature suffix.
- Align `Version`, `AssemblyVersion`, and `FileVersion` to
  `1.26.<patch>.<build>` before commit/push.
- Map required GitHub checks to actual workflow/job/runner/platform semantics,
  resolve all actionable review threads, merge under the authorized policy,
  delete the feature branch, and prove clean synchronized local `main`.
- Leave Feature 028 as the only next intake; Wave 5 and Wave 6 remain blocked.
