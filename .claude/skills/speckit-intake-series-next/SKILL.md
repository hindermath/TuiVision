---
name: speckit-intake-series-next
description: List every currently eligible intake target or exact blockers without
  starting work.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: intake-sequencing-governance:commands/speckit.intake-series-next.md
---

## User Input

```text
$ARGUMENTS
```

Select candidates only from a valid, current named series.

1. Run the read-only status contract first.
2. A target is eligible only when its lifecycle permits selection and every
   binding predecessor is `Completed`.
3. Report all eligible targets in visible order. If none are eligible, report
   each exact blocker and evidence path.
4. Distinguish preferred order and shared-writer serialization from binding
   functional dependencies.
5. Revalidate downstream review freshness and user authority only when a later
   command is separately invoked.
6. Never start Intake Review, Specify, Autonomous, or Parallel Autonomous.

Finish with a copy-ready suggested command only; do not execute it.