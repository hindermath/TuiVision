---
description: Logically delete active intake artifacts through archive and tombstone
  evidence.
---


<!-- Source: intake-authoring-governance -->
## User Input

```text
$ARGUMENTS
```

Delete is logical and reversible from versioned evidence. v0.2.0 has no purge.

1. Resolve exactly one active intake or one complete series. Require the exact
   identity, a deletion reason, and explicit current delete authority.
2. Validate current target, receipt, series references, review linkage, and Git
   state. Stop on ambiguity, drift, missing evidence, an incomplete operation,
   or an already deleted identity.
3. Present the complete deletion plan before writing: active paths, archive
   paths, tombstones, affected series members, invalidated reviews, and next
   action.
4. Refuse deletion of one referenced series member. Require an approved series
   migration or explicit whole-series deletion.
5. Copy target and active receipt byte-for-byte to
   `specs/intake-authoring-archive/<intakeId>/<operationId>/`, verify the copied
   hashes, and only then remove the active files.
6. Write one tombstone under
   `specs/intake-authoring-tombstones/<intakeId>.json` with original paths and
   hashes, archive paths and hashes, operation ID, reason, authority evidence,
   timestamp, series impact, and reactivation boundary.
7. Never delete history, archives, tombstones, Git evidence, related reviews,
   or source files. Never interpret broad cleanup language as purge authority.
8. Prepare a multi-member deletion transactionally and publish it only after
   every archive and tombstone validates in Bash and PowerShell.

Finish with deleted identities, archive and tombstone paths, validation,
invalidated references, and exact next action. Never recreate, review, specify,
or start an autonomous command implicitly.