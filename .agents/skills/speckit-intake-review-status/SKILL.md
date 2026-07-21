---
name: speckit-intake-review-status
description: Inspect intake-review result freshness and gate status without changing
  files.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:intake-review-governance
---

# Speckit Intake Review Status Skill

## User Input

```text
$ARGUMENTS
```

This command is strictly read-only. Locate the selected result and policy,
invoke the installed validator with the repository root, and report:

- mode, review ID, outcome, policy, and review time;
- target paths, normalized hashes, optional Git blobs, and drift;
- findings, questions, accepted-risk ownership, and re-evaluation triggers;
- series and campaign coverage, including unique intake and worker counts;
- whether Preset 7 or Preset 8 may consume the result under current policy;
- the exact next command.

Do not rewrite timestamps, hashes, status, reports, target files, Git state, or
authority. A successful status check grants no implementation or remote right.
