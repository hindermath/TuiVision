---
name: speckit-intake-review
description: Review one intake, an ordered series, or a campaign before Spec Kit execution.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: intake-review-governance:commands/speckit.intake-review.md
---

## User Input

```text
$ARGUMENTS
```

Review targets are read-only. Determine `Single`, `Series`, or `Campaign` from
explicit input; do not infer a broader scope merely because related files
exist. Use the installed policy and checklist.

1. Reject missing, binary, or non-UTF-8 targets. Compute normalized SHA-256 by
   removing one UTF-8 BOM, converting CRLF and CR to LF, and changing nothing
   else. Record an optional Git blob when available.
2. Review identity, audience, goal, scope, non-goals, atomic requirements,
   measurable acceptance, dependencies, order, security/privacy/A11Y/platform,
   evidence, delivery authority, risks, references, prompt alignment, and
   secret or unnecessary-personal-data exposure.
3. For a series, additionally review IDs, DAG, gaps, overlap, terminology,
   invariants, handoffs, future scope, and authority. Use schema 1.1, declare
   every root, order every target exactly once, and bind the request path and
   normalized hash in the result.
4. For a campaign, prove every `featureInput`, one semantic review per unique
   intake, every worker applicability row, repository/language/stack fit,
   manifest DAG/base/handoff agreement, and separately governed operator
   exceptions.
5. In interactive mode ask at most five material questions per pass, exactly
   one at a time. In batch mode record blockers and do not guess.
6. Assign stable finding IDs, severity, category, owner, evidence, disposition,
   and re-evaluation trigger. Outcomes are exactly `Ready`,
   `ReadyWithAcceptedRisks`, `NeedsClarification`, `NeedsRemediation`, or
   `Rejected`.
7. Critical or High findings block. `ReadyWithAcceptedRisks` permits only
   Medium/Low risks with human owner, rationale, acceptance date, evidence, and
   re-evaluation trigger. An autonomous agent never accepts risk.
8. Write the machine-readable result and readable report outside the reviewed
   target files, then validate the result with the installed Bash or PowerShell
   validator. Supersede older results explicitly; never silently overwrite
   their audit meaning.

Finish with outcome, target and worker counts, findings by severity, accepted
risks, open questions, result path, and exact next action. Do not start Specify
or an autonomous run implicitly.
