---
name: speckit-parallel-autonomous-status
description: Inspect a parallel autonomous campaign without changing it.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: parallel-autonomous-run-governance:commands/speckit.parallel-autonomous-status.md
---

Read the campaign manifest, state, runtime result files, Git worktrees, and
referenced autonomous states. Report queued, running, completed, failed,
blocked, interrupted, and ready workers; observed concurrency; missing or stale
evidence; stop state; selection state; runner profile and explicitly declared
non-secret model metadata; attempt counts; consolidation checkpoints;
post-merge actions; and the next exact action.

Status is read-only. A running marker without a trustworthy live process or
result is `Interrupted`, never success.

Support machine-readable JSON and accessible text. Never expose executable
arguments, environment values, credentials, or undeclared provider settings.

*DE: Status ist read-only und bietet JSON sowie barrierearmen Text. Nicht
deklarierte Modelle werden als `Agent-Standard/nicht deklariert` angezeigt.*