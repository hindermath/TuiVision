---
description: Request a cooperative stop at parallel campaign safe boundaries.
---


<!-- Source: parallel-autonomous-run-governance -->
Set the campaign stop request after validating identity. Start no new workers.
Do not kill agent, test, build, provider, or merge processes. Let active workers
reach their existing autonomous safe boundary, then persist `PausedByUser` or
`Interrupted` from observed evidence.

During consolidation, honor the same request between provider preflights,
merge commands, synchronization, post-merge actions, and final validation.
Persist the request both in the campaign state and in the durable
`<state-path>.stop-requested` sidecar so a concurrent checkpoint cannot lose
it. Only an accepted start or resume may clear the sidecar.

*DE: Stop gilt auch waehrend der Konsolidierung und wird zwischen sicheren
Merge- und Closeout-Schritten beachtet. Der dauerhafte
`<state-path>.stop-requested`-Marker verhindert, dass ein gleichzeitiger
Checkpoint die Anforderung verliert; erst ein akzeptierter Start oder Resume
entfernt ihn.*