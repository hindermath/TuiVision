---
description: Validate and execute a permission-bounded parallel autonomous campaign.
---


<!-- Source: parallel-autonomous-run-governance -->
## User Input

```text
$ARGUMENTS
```

Require an accepted campaign manifest, local runner-profile binding, explicit
delivery authority, and `autonomous-run-governance >= 0.2.2` in every worker
repository.

Resolve the shared project learner and accessibility policy before scheduling.
Every worker and the final consolidation must preserve the declared audience,
language order, readability, first-use terminology, prior-knowledge boundary,
and text-first dependency/status/decision/next-action contract. Provider or
runner defaults cannot weaken that policy.

Campaign schemas `1.0`, `1.1`, and `1.2` are supported. Schema `1.2` may
declare `intakeReview.required`. When true, validate the result before any
worktree or worker process is created. Require one semantic review per unique
intake content, one applicability row per worker, agreement with the manifest
DAG, and separately owned operator exceptions with reason, date, and expiry.
Store the accepted result hash in campaign state. A missing, stale, blocking,
or incomplete result schedules zero workers.

1. Validate campaign identity, topology, worker IDs, UUIDs, concurrency, DAG,
   branches, repository state, campaign and optional worker runner profiles,
   stable routing roles, fallback policy, and consolidation policy. Under
   `fail-closed`, require an exact role match plus explicit model, reasoning
   effort, executable, and successful read-only preflight before a worker
   process starts.
2. Default ambiguous authority to `LocalImplementation`.
3. Create one isolated branch and worktree per worker without switching or
   resetting normal checkouts.
4. Start no more than `maxConcurrency` workers. Execute runner arguments
   directly without shell evaluation.
5. Preserve each worker's autonomous state as authoritative. Record only its
   path and hash in campaign state.
6. Continue unrelated workers after ordinary failure. Block pipeline
   descendants. Stop new scheduling on integrity, security, permission, or
   evidence failure.
7. Persist state after every scheduling and completion transition.
8. End local campaigns with validated worker results. End remote campaigns at
   the all-ready consolidation boundary; never infer merge authority.
9. Persist only declared non-secret runner metadata. Never guess a model or
   reasoning level from another agent's configuration.
10. Never replace a missing, ambiguous, or failed routed profile with another
    model. Mark the affected worker and campaign `Blocked`.

*DE: Worker-spezifische Runner-Profile duerfen das Kampagnenprofil
ueberschreiben. Die stabile Routing-Rolle muss passen; bei `fail-closed` sind
Modell, Reasoning und erfolgreicher Preflight verbindlich. Keine fremde
Agentenkonfiguration erraten.*