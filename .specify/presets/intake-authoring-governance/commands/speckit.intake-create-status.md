---
description: Inspect an intake-authoring receipt, source freshness, target hash, and prompt state without writes.
---

## User Input

```text
$ARGUMENTS
```

This command is strictly read-only.

1. Resolve the named receipt or the receipt associated with the named intake.
   Do not infer a different intake when more than one candidate exists.
2. Hash the receipt before inspection. Run the installed Bash or PowerShell
   validator with the repository root passed explicitly.
3. Recheck every repository-local source, target hash, status, decisions,
   authority, prompt markers, rendered invocations, and provenance fields.
   Inline and external sources are snapshot-only; report that limit without
   treating an unverifiable path as current evidence.
4. For schema 1.1, verify `New`, `Supersession`, or `LegacyAdoption`.
   A legacy Git blob must resolve to the recorded normalized prior-target hash;
   snapshot-only adoption reports its explicit proof limit.
5. For schema 2.0, verify stable intake identity, operation identity and type,
   lineage, optional series membership, and URL evidence. Schemas 1.0 and 1.1
   remain valid without migration.
6. URL evidence is snapshot-only by default. Re-fetch only when the user
   explicitly requests URL revalidation. Apply the same HTTPS, redirect,
   address, media, and size policy without writing files, then compare the raw
   response hash.
7. Hash the receipt again and verify Git status for the inspected paths. Any
   mutation makes the status check fail.
8. Classify the result as `Current`, `SourceDrift`, `TargetDrift`, `UrlDrift`,
   `NeedsClarification`, `InvalidReceipt`, or `Missing`.

Finish with classification, target, receipt, source freshness, prompt state,
delivery authority, validation exit, and exact next action. Never repair,
review, run Specify, or start an autonomous command implicitly.
