---
name: speckit-intake-repair
description: Repair explicitly authorized intake findings and perform a complete re-review.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:intake-review-governance
---

# Speckit Intake Repair Skill

## User Input

```text
$ARGUMENTS
```

Require a current intake-review result and explicit authority to modify the
named targets. Preserve unrelated work.

1. Revalidate target hashes before editing. Stop on drift.
2. Automatically repair only mechanical defects such as broken local links,
   duplicate numbering, malformed fenced blocks, inconsistent copied prompt
   paths, or deterministic ordering errors when the intended meaning is
   unambiguous.
3. Require an explicit user answer before changing goals, scope, non-goals,
   requirements, acceptance thresholds, delivery authority, security/privacy/
   A11Y decisions, risk acceptance, dependency ownership, or campaign
   exceptions.
4. Record each changed finding and authorization. Do not broaden the intake or
   create implementation artifacts.
5. Run a complete intake review again, produce a new result with `supersedes`,
   and validate all current hashes. A repaired file is not accepted merely
   because the edit succeeded.

Finish with changed targets, resolved and remaining findings, new outcome, and
the next exact action.
