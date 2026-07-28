---
name: speckit-intake-series-next
description: List every currently eligible intake target or exact blockers without
  starting work.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:intake-sequencing-governance
---

# Speckit Intake Series Next Skill

## User Input

```text
$ARGUMENTS
```

Select candidates only from a valid, current named series.

1. Run the read-only status contract first.
2. A target is eligible only when its lifecycle permits selection and every
   binding predecessor is `Completed`.
3. Require exactly one `Eligible` target when the manifest declares a preferred
   next intake. More than one is invalid. An eligible result is ordering
   evidence only and grants no implementation, push, PR, merge, bypass, or
   provider authority.
4. Report all eligible targets in visible order. If none are eligible, report
   each exact blocker and evidence path. For a valid `Idle` series, report that
   no active intake exists and do not treat this as an error.
5. Distinguish preferred order and shared-writer serialization from binding
   functional dependencies.
6. Revalidate downstream review freshness and user authority only when a later
   command is separately invoked.
7. Never start Intake Review, Specify, Autonomous, or Parallel Autonomous.

Finish with a copy-ready suggested command only; do not execute it.
