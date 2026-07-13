# Quickstart: Core Runtime Conformance Hardening

## 1. Preflight

```bash
git branch --show-current
jq -r .feature_directory .specify/feature.json
specify check
.specify/scripts/bash/check-prerequisites.sh --json --require-tasks --include-tasks
```

Expected branch: `025-core-runtime-conformance-hardening`. All feature
checklists must contain zero incomplete items before implementation.

## 2. Establish Evidence First

Create `pr-evidence.md` from
`.specify/templates/autonomous-run-evidence-template.md`. Before editing each
runtime or test file, record:

- finding and contract ID;
- planned deterministic validator/test and expected red boundary;
- historical and Free Vision source paths;
- exact allowed source/test/evidence files;
- API, A11Y, platform and governance triggers.

## 3. Execute Finding Slices

Use the dependency order from `plan.md`: `F001`, `F008`, `F002`, `F003`,
`F004`, `F007`, `F005`, `F006`, `F009`.

For every slice:

1. Add the narrow real-path test and run it red.
2. Record the expected failure in `pr-evidence.md`.
3. Implement only the accepted finding boundary.
4. Re-run the narrow Release test green.
5. Add state/View-tree/focus/Buffer evidence where visible.
6. Complete the Finding row before starting the next shared lifecycle slice.

## 4. Version Before Build or Test

Before every explicit `dotnet build` or `dotnet test`, increment the manual
Build field exactly once and keep all three version fields aligned:

```xml
<Version>1.25.PATCH.BUILD</Version>
<AssemblyVersion>1.25.PATCH.BUILD</AssemblyVersion>
<FileVersion>1.25.PATCH.BUILD</FileVersion>
```

Do not increment Build for `dotnet format`, DocFX, npm, Lynx, Git or GitHub
commands. Before commit/push, align Patch to the feature-branch commit count
after the new commit without adding a build increment.

## 5. Historical and Secondary Review

Read matching sources under `tv203s/` and keep them unchanged. Verify the
external second opinion:

```bash
git -C /tmp/tuivision-fv-025-ffc03b34 rev-parse HEAD
```

Expected: `ffc03b34d8cafb85ddcf0686de1c5551601dacb2`. Never stage the external
worktree or copy implementation text into TuiVision.

## 6. Final Local Validation

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
xmllint --noout coverlet.runsettings
```

After a fresh build-counter increment for each invocation, run targeted Release
tests, the full Release suite, and:

```bash
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

Because public XML/API and the guide change, also run:

```bash
docfx docfx.json
cd tests/web-a11y
npm ci
npm run test:docfx
```

Review changed generated pages through the repository's UTF-8 text/Lynx path.
Do not stage `_site/`, generated `api/*.yml`, test results, caches or logs.

## 7. Scope and Completion Audit

Confirm:

- `F001`-`F009` each have one closure decision and complete evidence;
- `F010`-`F013` remain assigned to Feature 026;
- Feature 024 retains original findings and adds only proven resolution data;
- `TVDEMOS/`, `TVFM/`, `tv203s/`, examples and external Free Vision are unchanged;
- no new package or unresolved breaking change exists;
- all five agent surfaces, Pflichtenheft, statistics and archived Lastenheft agree
  that Feature 026 is next and Waves 5/6 remain blocked until Feature 028.

## 8. Remote Delivery

Align version, commit, push and open a ready PR. Wait for required PR-context
checks and inspect thread-level reviews. Merge with a merge commit only after
green required checks and zero actionable threads. Use the approved narrow
admin bypass only when human approval is the sole remaining rule. Delete the
feature branch and prove a clean local `main == origin/main`.
