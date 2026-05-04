# Agent Parity Governance Preset

Version: `0.1.0`
Requires: `spec-kit >= 0.8.0` (uses the `wrap` and `append` composition
strategies introduced in 0.8.x).

Purpose:

- prevent silent process drift between AI-agent guidance files
- enforce that shared rules land atomically across the project's
  declared AI-agent guidance surfaces

Primary source chapter from `home-baseline` constitution:

- `IX. Agent Guidance Parity & Template Synchronization`

What it covers:

- project-declared list of maintained agent surfaces
- atomic-change discipline (one change → all surfaces)
- propagation into project templates and `.specify/memory/constitution.md`
- explicit documentation of intentional deviations
- parity-verification artefact (`agent-parity-checklist-template`)

Preset strategy:

- append parity governance to `constitution-template`, `spec-template`,
  `plan-template`, and `tasks-template`
- provide a standalone agent-guidance addendum template for projects that
  maintain agent instruction files
- wrap `speckit.specify`, `speckit.plan`, and `speckit.tasks` with a
  shared parity workflow
- provide a parity checklist starter

When to use:

- any project that maintains more than one AI-agent guidance file
- any team that wants atomic, auditable changes to shared agent
  instructions
- any project where AI-agent surfaces are part of the contributor
  contract

Common surfaces include `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
`.github/copilot-instructions.md`, `.cursorrules`, `.windsurfrules`,
`JUNIE.md`, or other project-specific instruction files. The preset does
not require a specific vendor or agent mix.

When not to use:

- projects with only one agent guidance file and no plans to add another
- one-off prototypes without long-term agent contributors

Recommended standalone install priority:

- `40`
