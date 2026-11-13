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
- for Series mode, request hash, root set, target order, edge count, and DAG validity;
- for schema-2.0 requirements collections, language, profile, portable roles,
  resolved paths, computed inventory, canonical index count, and eligible
  candidate;
- whether Preset 7 or Preset 8 may consume the result under current policy;
- the exact next command.

Do not rewrite timestamps, hashes, status, reports, target files, Git state, or
authority. A successful status check grants no implementation or remote right.

## Nutzungshilfe / Reader help

DE: Einen bestehenden Arbeitsauftrag fachlich prüfen, ohne ihn still zu reparieren. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/main/docs/user-guide.md).

EN: Assess an existing work request without silently repairing it. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-intake-review-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
