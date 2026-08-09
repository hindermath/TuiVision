---
description: Inspect a parallel autonomous campaign without changing it.
---


<!-- Source: parallel-autonomous-run-governance -->
Read the campaign manifest, state, runtime result files, Git worktrees, and
referenced autonomous states. Report queued, running, completed, failed,
blocked, interrupted, and ready workers; observed concurrency; missing or stale
evidence; stop state; selection state; runner profile and explicitly declared
stable routing role, non-secret model metadata, reasoning effort, and preflight
result; attempt counts; consolidation checkpoints;
post-merge actions; and the next exact action.

Status is read-only. A running marker without a trustworthy live process or
result is `Interrupted`, never success.

Support machine-readable JSON and accessible text. Never expose executable
arguments, environment values, credentials, or undeclared provider settings.

*DE: Status ist read-only und bietet JSON sowie barrierearmen Text. Fehlende
Pflichtbindungen werden als `Blocked`, nicht als stiller Standard angezeigt.*