---
name: speckit-intake-create-status
description: Inspect an intake-authoring receipt, source freshness, target hash, and
  prompt state without writes.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: intake-authoring-governance:commands/speckit.intake-create-status.md
---

## User Input

```text
$ARGUMENTS
```

This command is strictly read-only.

1. Resolve the named receipt or the receipt associated with the named intake.
   Do not infer a different intake when more than one candidate exists.
2. Hash the receipt before inspection. Run the installed Bash or PowerShell
   validator with the repository root passed explicitly.
3. Recheck every repository-local source, target hash, status, decisions,
   authority, prompt markers, rendered invocations, and supersession fields.
   Inline and external sources are snapshot-only; report that limit without
   treating an unverifiable path as current evidence.
4. Hash the receipt again and verify Git status for the inspected paths. Any
   mutation makes the status check fail.
5. Classify the result as `Current`, `SourceDrift`, `TargetDrift`,
   `NeedsClarification`, `InvalidReceipt`, or `Missing`.

Finish with classification, target, receipt, source freshness, prompt state,
delivery authority, validation exit, and exact next action. Never repair,
review, run Specify, or start an autonomous command implicitly.