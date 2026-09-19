---
name: speckit-statistics-init
description: Projektstatistik init / Project statistics init
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: project-statistics-governance:commands/speckit.statistics-init.md
---

# Statistik init / Statistics init

DE: Verwende das ausdruecklich benannte Repository und den Kontext.
Default: `docs/project-statistics/config.json`. Pilotkontexte sind getrennt.
Nur mit aktuellem ausdruecklichem Auftrag. Zuerst Vorschau; bestehende Quellen und manuelle Abschnitte erhalten.
Auf macOS/Linux:
`bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh init --repo . --config <config>`
Auf Windows:
`pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Init -Repo . -Config <config>`

EN: Use the explicitly named repository and configuration. Do not shorten the
installed script paths. Require current explicit write authority and preview first; preserve existing sources and authored sections.
Report reproducibility separately from freshness, source revision, cutoff,
coverage and exact exit code. Missing tools or sources block; never install
tools, fetch history, stash, commit, push, merge or grant human approvals.
Reference estimates remain opt-in and are not measured AI productivity.
Follow the resolved `project-statistics-contract`.