# Quickstart: Terminal and Charset Hardening

## 1. Preflight

```bash
git branch --show-current
git status --short
specify check
specify preset list
pwsh -NoProfile .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
```

Expected branch is `021-terminal-charset-hardening`; all feature checklists must
be complete before implementation.

## 2. Evidence first

Create `specs/021-terminal-charset-hardening/pr-evidence.md` before changing
source. Record exact rows for session/emulation, charset, font, profile,
Controls integration, host evidence, framework decisions, governance, comments,
validation, and remote closeout.

## 3. Test-first vertical slice

Before the first red command, review imports, public XML documentation, test
harness, state/ownership assertions, and shared/generated-source identity.
Then add one Driver-owned red matrix for plain text, cursor/cell state, one
accepted CSI action, one atomic rejection, and recovery. Implement only enough
session/parser behavior to make that slice green before spreading the pattern.

## 4. Build-counter rule

Before every `dotnet build` or `dotnet test`, increment only the manual Build
field in `Directory.Build.props`. Before commit/push on the numbered branch,
align all version fields to `1.21.<branch-commit-count>.<build>` without another
Build increment unless another build/test runs.

## 5. Focused and full validation

```bash
git diff --check
dotnet format --verify-no-changes --no-restore
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release
dotnet test tests/TuiVision.Controls.Tests/ --configuration Release
xmllint --noout coverlet.runsettings
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
docfx docfx.json
npm --prefix tests/web-a11y run test:docfx
pwsh -NoProfile scripts/scan-agent-secrets.ps1
```

Run only commands whose trigger applies, but record every skipped trigger and
reason. Remove `_site/`, generated `api/*.yml`, coverage/TestResults, Playwright
output, caches, and logs after extracting evidence.

## 6. Scope checks

- No new Wave-4 example directory or project.
- No shell/PTY/process spawning or arbitrary host I/O.
- No host font, keyboard map, codepage, terminal profile, or audio mutation.
- No package/dependency update or `tv203s/` edit.
- No example-local terminal parser, charset mapper, font loader, or profile
  fallback.

## 7. Delivery closeout

Verify checks and GraphQL threads on the final feature head before merge. Do not
commit those current-head facts onto that same branch when the commit would
invalidate the evidence. Record them with merge, branch cleanup, and final
`main == origin/main` in the pre-named causal closeout path. Remote authority
comes only from the current user delegation.
