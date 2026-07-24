---
description: Summarize one intake or approved series without changing files.
---


<!-- Source: intake-authoring-governance -->
## User Input

```text
$ARGUMENTS
```

This command is strictly read-only.

1. Resolve exactly one target, active receipt, tombstone, or series manifest.
   Stop on ambiguity and never infer a related intake merely because it exists.
2. Hash every inspected artifact before reading. Run the installed validators
   and classify provenance, target freshness, review freshness, prompt state,
   and deletion state.
3. Default to `Summary`: identity, purpose, scope, non-goals, requirement and
   acceptance counts, dependencies, risks, readiness, review status, source
   proof boundary, and exact next action.
4. Use `Detailed` only when explicitly requested. Add ordered source metadata,
   decisions, lineage, series coverage, DAG, archive/tombstone state, and hashes
   without copying source bodies wholesale.
5. Use `Json` only when explicitly requested. Emit the same structured fields
   as machine-readable JSON and no hidden reasoning, credentials, private
   paths, or unnecessary personal data.
6. Do not use network access by default. URL evidence is reported as the
   recorded snapshot. Explicit URL revalidation belongs to
   `speckit.intake-create-status`.
7. Hash every inspected artifact again and verify Git status. Any mutation
   invalidates the Read result.

Finish with view, identity, lifecycle state, validation result, proof boundary,
and exact next action. Never update, delete, review, specify, or start an
autonomous command implicitly.