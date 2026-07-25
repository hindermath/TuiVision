---
name: speckit-intake-series-delete
description: Logically delete one intake series through archive and tombstone evidence.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:intake-sequencing-governance
---

# Speckit Intake Series Delete Skill

## User Input

```text
$ARGUMENTS
```

Logically delete exactly one named series with explicit current authority.

1. Validate current manifest and receipt.
2. State that intake documents remain unchanged.
3. Archive manifest and receipt byte-identically.
4. Create a tombstone with identity, authority, reason, UTC time, archive paths,
   hashes, and `intakeDocumentsDeleted: false`.
5. Publish deletion receipt and tombstone only after both validators pass.
6. Do not physically purge evidence or start another command.

Finish with archive and tombstone proof plus one exact safe next action.
