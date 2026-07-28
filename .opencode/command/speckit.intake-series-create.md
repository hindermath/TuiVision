---
description: Create one traceable intake series from explicitly named existing intakes.
---


<!-- Source: intake-sequencing-governance -->
## User Input

```text
$ARGUMENTS
```

Create exactly one new series. Do not change intake content and do not start a
review or feature workflow.

1. Read repository guidance, sequencing policy, and every explicitly named
   target as strict UTF-8.
2. Refuse an existing active series. Never infer an update authority.
3. Present the exact target order, roles, normalized hashes, roots, typed edges,
   binding flags, lifecycle states, and evidence paths before writing.
4. Ask material questions one at a time. Do not guess a dependency, root,
   completion fact, or delivery authority.
5. Prepare manifest, receipt, operation journal, and learner-readable order
   document under repository-owned paths.
   Preserve the project's declared audience, language order, readability,
   first-use terminology, and prior-knowledge boundary. The order document
   must state dependencies, blockers, status, decisions, and next actions as
   text even when it also contains a diagram.
6. Validate paths, hashes, graph, lifecycle, and receipts in Bash and
   PowerShell before publishing any active file.
7. Publish all prepared files atomically. A failure leaves an explicit
   incomplete operation and no partially active series.
8. Finish with series ID, cardinalities, validation results, eligible targets,
   blockers, and exactly `$speckit-intake-series-status` as the next action.