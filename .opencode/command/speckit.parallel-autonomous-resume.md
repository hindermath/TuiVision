---
description: Resume a stopped or interrupted campaign after complete revalidation.
---


<!-- Source: parallel-autonomous-run-governance -->
Revalidate campaign manifest hash, repositories, worktrees, branches, current
authority, runner profile, worker result contracts, autonomous states,
completed handoffs, and the last trustworthy operation. Reuse verified
completed work. Retry only an unproven or incomplete operation. Reconcile
current mandatory governance deltas before scheduling.

For consolidation, revalidate every PR against the exact expected head. Skip a
verified `Merged` worker, adopt an externally merged exact head, and classify
any drift as `NeedsRevalidation`. Retry only failed idempotent post-merge
actions from the reviewed manifest.

*DE: Bereits verifizierte Merges nicht wiederholen. Extern gemergte exakte
Heads duerfen uebernommen werden; Drift bleibt blockierend.*