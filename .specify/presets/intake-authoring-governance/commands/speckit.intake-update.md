---
description: Explicitly update one active intake or migrate one approved intake series.
---

## User Input

```text
$ARGUMENTS
```

Update is the only normal command that changes an active intake.

1. Resolve the exact active target, receipt, or series manifest and require
   explicit current update authority. General write, autonomous, delivery, or
   historical authority is insufficient.
2. Validate the current target, receipt, sources, review linkage, and Git state
   before planning. Stop on unexplained drift, missing evidence, a tombstone,
   or an incomplete operation.
3. Treat the previous intake as the first binding source, followed by the
   explicitly ordered change sources. Preserve accepted scope unless current
   authority explicitly changes it.
4. Ask at most five material questions per pass, exactly one at a time. Never
   guess scope, deletion, split/merge identity, security, acceptance, or
   delivery authority.
5. An ordinary update retains `intakeId`, creates a new receipt and operation
   ID, archives the prior target and receipt byte-for-byte, and records their
   hashes and paths in `supersedes`.
6. Updating a schema-1.0 or schema-1.1 receipt upgrades only that intake to
   schema 2.0. Do not mass-migrate unrelated receipts.
7. A series split or merge requires a proposal naming every old and new
   member, predecessor/successor relation, coverage, overlap, order, root,
   edge, target path, and review handoff. New semantic units receive new IDs;
   ordinary one-to-one members retain their IDs.
8. Apply the same strict UTF-8, source order, secret, personal-data, HTTPS,
   crawl, temporary-source, prompt-safety, and context-limit rules as Create.
9. Prepare and validate all outputs transactionally before publishing active
   changes. A failure leaves prior active artifacts unchanged.
10. Supersede any old Intake Review result explicitly. Never report an old
    review as current after target or series drift.
11. Run both installed validators before completion.

Finish with updated identities, old/new hashes, lineage, archive paths,
validation, review invalidation, and exactly one Intake Review next action.
Never start the review or a downstream command.
