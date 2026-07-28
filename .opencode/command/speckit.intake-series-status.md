---
description: Validate current intake-series state without changing files.
---


<!-- Source: intake-sequencing-governance -->
## User Input

```text
$ARGUMENTS
```

Inspect one named series read-only.

1. Hash run state and tracked evidence before inspection.
2. Run Bash and PowerShell validators.
3. Report identity, status, targets, roots, dependencies, eligible targets,
   blockers, receipt lineage, archive/tombstone state, and drift.
4. When schema-2.0 requirements governance is configured, validate it first and
   resolve target paths from its portable roles and collection paths. Confirm
   that the human-readable order and manifest use the same resolved paths.
5. Classify ambiguity or drift fail-closed. Do not repair it.
6. Prove before/after hashes and Git status are unchanged.
7. Do not stage, commit, push, review, or execute a target.

Finish with one exact next action.