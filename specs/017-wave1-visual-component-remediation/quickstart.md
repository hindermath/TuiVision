# Quickstart: Wave-1 Visual Component Remediation

## 1. Preflight

```bash
git switch 017-wave1-visual-component-remediation
specify check
./.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
git status --short --branch
```

Confirm `.specify/feature.json` points to
`specs/017-wave1-visual-component-remediation` and all checklists are complete.

## 2. Read Before Editing

Read feature 014 evidence, this feature's spec/plan/research/data model/contract,
and the required historical sources:

```text
tv203s/contrib/tvision/examples/desklogo/
tv203s/contrib/tvision/examples/msgcls/
tv203s/contrib/tvision/examples/tutorial/tvguid01.cc .. tvguid16.cc
tv203s/contrib/tvision/examples/videomode/test.cc
```

Historical files are read-only.

## 3. Evidence First

Create `pr-evidence.md` before runtime edits. Add complete starter schemas for:

- example and Tutorial visual/proof rows;
- framework usage decisions;
- historical deviations;
- governance checkpoints;
- validation runs and conditional checks.

Starter rows may be pending during implementation but may not remain empty at
completion.

## 4. Test-First Vertical Slice

Add failing `MsgCls` app-loop tests for visible command routing, real status,
description, repeated trigger, view-tree, and rendered buffer/cells. Then add the
small shared Wave-1 composition helper and implement the slice.

Continue test-first with Desklogo, all 16 Tutorial tokens, and Videomode.

## 5. Version Before Every Build Or Test

For branch 017, align all three fields in `Directory.Build.props` to:

```text
1.17.<current-branch-commit-count>.<manual-build-counter>
```

Increment only the final component immediately before every `dotnet build` or
`dotnet test`, including test reruns and coverage commands.

## 6. Fast Validation

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release \
  --filter "FullyQualifiedName~Desklogo|FullyQualifiedName~MsgCls|FullyQualifiedName~Tutorial|FullyQualifiedName~Videomode|FullyQualifiedName~Wave1Visual"
git diff --check
```

Record exact command, version, pass count, and proof boundary in `pr-evidence.md`.

## 7. Full Validation

Increment the build counter separately before each build/test command:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
xmllint --noout coverlet.runsettings
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
git diff --check
```

Because guides and README change, also run:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Inspect representative generated pages through a text-oriented accessibility
snapshot. Remove generated `_site/`, generated API YAML, caches, and test output
from the worktree before delivery.

## 8. Completion

1. Complete all evidence and governance rows.
2. Update guides, README, `Pflichtenheft.md`, agent surfaces, and statistics.
3. Confirm no Wave-2/3/4, dependency, broad framework, or historical-source diff.
4. Archive the Lastenheft with the PowerShell rename workflow.
5. Align the version for commit without incrementing the build counter unless a
   later build/test runs.
6. Commit, push, create the PR, address reviews and CI, merge, switch to `main`,
   pull, and only then record any causal delivery-closeout task.
