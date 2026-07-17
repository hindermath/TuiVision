# Retrospective: Feature 036 Wave-6 TVFM Showcase Remediation

## Decision

`NoPromotion`

Feature 036 completed with a successful autonomous `MergeAndSync` run and did
not expose a reproducible provider-neutral defect in the installed
`autonomous-run-governance` preset. No Home-Baseline branch, preset patch or
empty follow-up PR is justified by this run.

## What Worked

- The two-stage example-wave model kept the Feature-035 filesystem authority
  stable while Feature 036 added only the evidenced presentation delta.
- The exact-head gate file and both gate validators established a clear merge
  boundary after the Windows correction.
- Provider validation detected a real checkout-neutrality defect in test-only
  historical hash proof before merge.
- The task, evidence and run-state model separated feature-head facts from
  post-merge facts without changing runtime scope.

## Observations

| Observation | Classification | Action |
|---|---|---|
| Windows converted historical `.PAS` and `.BAT` text line endings, while the initial test-only validator expected raw LF bytes. | `FeatureSpecific` | Canonicalize only historical text bytes and keep `.PAL` and `.TVR` byte-exact; retain the explicit LF/CRLF test. |
| Push and pull-request events produced duplicate workflow activity during the correction cycle. | `RunbookClarification` | Continue using pull-request-context terminal gates and do not cancel runs without an explicit safe concurrency contract. |
| Copilot quota exhaustion produced no review result. | `NoPromotion` | Continue recording reviewer unavailability as missing review rather than Pass. |
| The merge required the authorized Human-Approval-only admin bypass after every technical and exact-head gate was green. | `NoPromotion` | Keep the bypass narrow and evidence-bound; no policy change is required. |

## Reusable Learning

Platform-neutral proof for historical text must distinguish normalized source
content from byte-exact binary resources. This is a repository test-design
lesson, not an autonomous orchestration defect.

The one causal evidence-only closeout remains sufficient for post-merge facts.
It must not recursively require its own PR or merge identity in the committed
file.

## Final Outcome

- Feature PR: <https://github.com/hindermath/TuiVision/pull/104>.
- Feature head: `a0d506297c101104fd0e15911a7d21e1c5a21caa`.
- Feature merge commit: `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d`.
- Tasks: `187/187`.
- Run state target: `Retrospective`, `Completed`, `nextExactAction: N/A`.
- Preset promotion: `NoPromotion`.
- Feature 037, independent Wave-6 closure and portfolio audit: not created or
  started.
