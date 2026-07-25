---
description: Summarize one intake series without changing files.
---


<!-- Source: intake-sequencing-governance -->
## User Input

```text
$ARGUMENTS
```

Read one named series in Summary, Detailed, or JSON form.

1. Resolve only the named manifest and receipt.
2. Validate them before summarizing.
3. Report visible order, roots, typed dependencies, binding versus
   serialization-only edges, lifecycle states, eligible targets, blockers,
   evidence freshness, and residual risk.
4. Keep the explanation text-first, German-first/English-second where project
   policy requires it, and understandable without graph-theory knowledge.
5. Do not modify files, stage changes, or start another Spec Kit command.

Finish with the read-only hash proof and one exact safe next action.