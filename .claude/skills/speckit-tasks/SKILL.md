---
name: speckit-tasks
description: 'Spec-kit workflow command: speckit-tasks'
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: agent-parity-governance:commands/speckit.tasks.md
---

Before continuing, apply the Agent Parity Governance preset:

- add explicit tasks to update every maintained agent surface in the
  same change
- add tasks to propagate shared rules into project templates and the
  local constitution mirror
- add a parity-verification task using the agent-parity checklist

{CORE_TEMPLATE}

Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.
