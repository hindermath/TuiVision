# Delivery Closeout: Feature 036 Wave-6 TVFM Showcase Remediation

## Purpose

This closeout records facts that could not truthfully exist on the reviewed
Feature-036 head before PR merge: PR identity, merge commit, branch cleanup,
main synchronization and retrospective disposition.

It is evidence-only. It does not change runtime behavior, public API,
dependencies, examples beyond already merged Feature 036, generated output,
historical sources, Feature 037 or the independent Wave-6 closure.

## Feature Delivery

| Field | Value |
|---|---|
| Feature branch | `036-wave6-tvfm-showcase-remediation` |
| Feature head | `a0d506297c101104fd0e15911a7d21e1c5a21caa` |
| Feature PR | <https://github.com/hindermath/TuiVision/pull/104> |
| Merge method | Merge commit |
| Merge commit | `559bffbfbb94699a33cfe1ad8b01d5ac9b86641d` |
| Merged at | `2026-07-17T19:16:21Z` |
| Remote feature branch | Deleted and locally pruned |
| Delivery mode | `MergeAndSync` |

## Gate Summary

All PR-context technical gates for PR #104 reached terminal success:

- CI `Build and Test` on Ubuntu, macOS and Windows.
- DocFX Pages build. The deploy job was skipped for the PR event, as expected.
- Security Supply Chain package/SBOM evidence.
- Gitleaks and Agent Secret Scan.
- Homogeneity Check on Ubuntu, macOS and Windows.
- PowerShell Static Analysis on Ubuntu, macOS and Windows.
- Claude Code Review.

The final PR head had 22 successful checks and one expected skipped Pages
deployment. The first Windows run found a checkout-line-ending dependency in
the test-only historical source-hash validator. Commit `a0d5062` corrected
text hashing for `.PAS` and `.BAT` while preserving byte-exact `.PAL` and
`.TVR` hashing; the refreshed provider matrix passed.

Copilot returned a quota-limit comment on both reviewed heads. This was treated
as a missing review signal, not as a positive review. GitHub issue comments,
PR review comments and GraphQL review-thread inspection found no actionable
item before merge.

## Exact-Head Evidence

Temporary exact-head provider evidence was built for
`a0d506297c101104fd0e15911a7d21e1c5a21caa` and validated against
`specs/036-wave6-tvfm-showcase-remediation/autonomous-gate-requirements.json`.
Both Bash and PowerShell validators accepted all 12 primary gate requirements
before merge.

## Admin Bypass Boundary

The narrow admin bypass was used only after all technical gates were green,
exact-head evidence was valid, review-thread inspection found no actionable
work and Human Approval/review remained the sole open protection rule.

## Final Domain State

Feature 036 closes the one Feature-035 `ShowcaseDelta` through ten exact
`W6S` decisions, one `ShowcaseComplete` entry-point decision and all 24
accepted read-only `TVFM/` source hashes. `Tp7FileManager` now exposes the
already proven functional contracts through persistent menus and controls,
safe focusable dialogs, keyboard-complete interaction, bounded non-mutating
mouse intent, StatusLine, F1 Description and normal plus `48x16`
app-loop/view/cell proof.

The controlled-root, path, preview, search, viewer, one-shot intent and
revalidation boundaries remain unchanged. Feature 037, an independent
Wave-6 closure and the post-Wave-6 portfolio audit were not created or
started.

## Non-Recursive Closeout

This closeout PR records the Feature-036 delivery facts and the final local
state. It intentionally does not require another closeout for its own PR URL or
merge commit. The final assistant report after this closeout merge provides the
local `main` synchronization proof.
