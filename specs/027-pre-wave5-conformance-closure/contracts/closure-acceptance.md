# Acceptance Contract: Pre-Wave-5 Conformance Closure

## Input Contract

- Feature-024 dataset and evidence exist on the merged baseline.
- Exactly 16 domains and 48 contracts are present.
- Inventory counts are 151 historical, 119 modern source, and 176 public types.
- Decisions are 13/34/1/0/0 and findings are zero.
- Features 025 and 026 have no accepted non-empty owner set.

## Closure Contract

Closure passes only when:

1. all exact baseline counts, IDs, hashes, and 94 proof references revalidate;
2. focused audit, full Release, five coverage, format, DocFX, Axe, Lynx,
   secrets, protected-scope, and remote checks pass;
3. no product runtime, public API, dependency, package, example behavior,
   generated output, or historical source changes enter the final diff;
4. all governance rows have complete evidence and ownership;
5. the formal status surfaces agree on Feature 027 completion and Wave 5 as
   the next eligible intake.

## Failure Contract

Any new drift, gap, finding, owner-set entry, protected-path change, or failed
required gate sets the decision to `Blocked`. Feature 027 records owner,
reproduction, and next action but does not implement runtime remediation.

## Delivery Contract

- Explicit authority is `MergeAndSync`.
- Unavailable reviewers remain missing reviews.
- Actionable threads must be zero.
- A bypass is limited to the sole Human Approval rule after technical green.
- Post-merge facts use one non-recursive evidence-only closeout.
- Final local `main` must be clean and equal to `origin/main`.
