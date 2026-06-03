## Agent Parity Tasks

- Add an explicit task to update every maintained agent surface in the
  same change, based on the project's declared parity set.
- Add tasks to propagate shared rules into the relevant project
  templates under `.specify/templates/` and into
  `.specify/memory/constitution.md`.
- Add a parity-verification task using
  `agent-parity-checklist-template`.
- If model-routing guidance changes, add a task that confirms no
  provider-specific model names were written into `spec.md`, `plan.md`,
  `tasks.md`, or individual feature specs.
- Add a documentation task for any intentional deviation between
  surfaces.
- Add a final review task that confirms no maintained surface was
  silently skipped.
