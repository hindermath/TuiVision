# PR Evidence: Interactive Wave 2 Demos

**Feature**: `012-interactive-wave2-demos`
**PR**: [#27](https://github.com/hindermath/TuiVision/pull/27)
**Status**: Planning evidence initialized; implementation has not started.

This file is the repository-local proof ledger required by the specification. It is intentionally present before implementation so later tasks can append evidence without inventing the proof surface mid-feature.

## Current Planning Evidence

| Evidence Area | Current State | Reference |
|---|---|---|
| Specification | Created and clarified | `spec.md` |
| Plan | Created and reviewed | `plan.md` |
| Task plan | Created with 89 executable tasks after analysis remediation | `tasks.md` |
| Plan-quality checklist | Completed with 36 checks | `checklists/plan-quality.md` |
| Requirements checklist | Review-cleaned wording for governance/validation details | `checklists/requirements.md` |

## Implementation Evidence Matrix

| Example | Historical Source Review | Visible Runtime Path | App-Loop Smoke | Guide Update | Notes |
|---|---|---|---|---|---|
| Clipboard | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Planned in `tasks.md` |
| Demo | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | MVP vertical slice |
| DlgDsn | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Read-only fixture proof required |
| DynTxt | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Short, long, constrained text states |
| InpLis | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Input, list, history, boundary states |
| ListVi | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Selection and boundary states |
| ProgBa | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Completion state |
| Sdlg | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Vertical scroll/focus state |
| Sdlg2 | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Horizontal and vertical scroll/focus state |
| TCombo | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Selection, value, boundary states |
| TProgB | Pending implementation task | Pending implementation task | Pending implementation task | Pending implementation task | Partial, abort, cancelled states |

## Validation Evidence

| Command | Status | Notes |
|---|---|---|
| `.specify/scripts/bash/check-prerequisites.sh --json --paths-only` | Passed for planning | Points to `specs/012-interactive-wave2-demos` |
| `git diff --check` | Passed for planning | Used for spec, plan, checklist, and task artifacts |
| `dotnet build --configuration Release` | Pending implementation | Required before final test evidence |
| `dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release` | Pending implementation | Required after interactive paths are wired |
| `dotnet test --configuration Release` | Pending implementation | Required before merge |
| `dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings` | Pending implementation | Required before merge |
| `dotnet format --verify-no-changes` | Pending implementation | Required before merge |
| `docfx docfx.json` | Pending implementation | Required when guide/docs updates are made |
| `npm run test:docfx` in `tests/web-a11y/` | Pending implementation | Required when generated DocFX pages are refreshed |

## Review Cleanup Notes

- 2026-05-10: Added this evidence ledger before implementation so all references to `pr-evidence.md` resolve in the PR.
- 2026-05-10: Replaced local absolute links in planning artifacts with repository-relative links.
- 2026-05-10: Reworded the requirements checklist to distinguish user-facing behavioural requirements from required governance and validation-evidence details.
- 2026-05-10: Remediated `/speckit-analyze` findings by adding explicit Release build/test evidence, pre-test version/build-counter ordering, PR-description completion evidence, and the scripted Lastenheft rename as the final Polish step.
