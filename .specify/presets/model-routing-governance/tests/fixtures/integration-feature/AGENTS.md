# Isolated model-routing integration

This repository is an explicitly authorised synthetic test fixture, not a real
product feature. Implement the accepted single task T001 in
specs/999-routing-integration/tasks.md. Spec, plan, research, data model,
quickstart, readiness checklist and constitution are already provided. No
additional intake or planning phase is needed. Read the documents and perform
the task; report an honest Blocked result only on a concrete unmet requirement.

Prerequisite checking is provided by the deliberately fixture-local command:
pwsh -NoProfile -File .specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks
Its Bash wrapper accepts --json --require-tasks --include-tasks. It validates
actual fixture files and reports their paths; it is not an upstream Spec Kit
installer or a claim that unrelated product gates have been satisfied.

All writes stay in this repo. Do not install packages or tools. No network for
application/package work. Do not commit, push, merge or publish. The normal
runner owns phase-result validation. Output path/protocol come from its prompt.
