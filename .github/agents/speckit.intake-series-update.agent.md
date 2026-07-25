---
description: Update one intake series with explicit authority and preserved lineage.
---


<!-- Source: intake-sequencing-governance -->
## User Input

```text
$ARGUMENTS
```

Update exactly one active series. Current explicit update authority is required.

1. Validate the accepted manifest and receipt before proposing changes.
2. Present exact target, order, root, edge, lifecycle, and evidence differences.
3. Refuse ambiguous facts and unrelated intake-content changes.
4. Archive the prior manifest and receipt byte-identically.
5. Prepare successor manifest, receipt, operation journal, and order document.
6. Bind the successor through `supersedes`, prior hashes, archive paths, and
   current authority evidence.
7. Run both validators before atomic publication.
8. Do not perform Intake Review or start downstream work.

Finish with changed cardinalities, archive evidence, validation results, and
exactly `$speckit-intake-series-status` as the next action.