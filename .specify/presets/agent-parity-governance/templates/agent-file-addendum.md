## Agent Parity Governance Agent Guidance

- Treat shared agent guidance as a single logical document with multiple
  physical surfaces. Use the project's declared parity set as the source
  of truth.
- Common surfaces include `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`, `.cursorrules`, `.windsurfrules`,
  `JUNIE.md`, or project-specific instruction files. Do not assume this
  list is complete.
- Never update one of these files in isolation when the change affects
  shared rules. If you find yourself editing only one, stop and update
  the others in the same change.
- When propagating a shared rule, also update the relevant project
  templates under `.specify/templates/` and `.specify/memory/constitution.md`.
- Document every intentional deviation: which surface deviates, what it
  deviates from, why, and the expected lifetime of the deviation.
- Document every `N/A` decision with rationale; never silently omit.
- Use `agent-parity-checklist-template` as the standard verification
  artefact for shared-guidance changes.

## Spec-Kit Model Routing

- Treat model choice as operational agent-routing guidance, not as a
  feature requirement.
- Do not pin model names in `spec.md`, `plan.md`, `tasks.md`, or
  individual feature specs. Those artifacts must remain reproducible even
  when model names change or a different AI agent is used.
- Each agent maps these recommendations to its currently available
  models. Do not derive a fixed vendor or model requirement from this
  preset.
- For Spec-Kit specification, clarification, planning, task generation,
  and analysis, prefer the strongest available frontier reasoning/coding
  model.
- For complete long-running `speckit.implement` or `/speckit-implement`
  runs, prefer the strongest available long-running agent model; use a
  frontier model when maximum judgment quality is more important than
  runtime stability.
- For focused review or CI fixes, prefer a coding-optimized model.
- For trivial cleanup, formatting, or low-risk mechanical edits, a fast
  small coding model is acceptable.
