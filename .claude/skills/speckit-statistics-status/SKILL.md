---
name: speckit-statistics-status
description: Projektstatistik status / Project statistics status
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: project-statistics-governance:commands/speckit.statistics-status.md
---

# Statistik status / Statistics status

DE: Verwende das ausdruecklich benannte Repository und den Kontext.
Default: `docs/project-statistics/config.json`. Pilotkontexte sind getrennt.
Strikt read-only; keine Reparatur oder automatischen Folgeschritte.
Auf macOS/Linux:
`bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh status --repo . --config <config>`
Auf Windows:
`pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Status -Repo . -Config <config>`

EN: Use the explicitly named repository and configuration. Do not shorten the
installed script paths. Read-only status must not repair or start follow-up work.
Report reproducibility separately from freshness, source revision, cutoff,
coverage and exact exit code. Missing tools or sources block; never install
tools, fetch history, stash, commit, push, merge or grant human approvals.
Reference estimates remain opt-in and are not measured AI productivity.
Follow the resolved `project-statistics-contract`.