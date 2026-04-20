# Command Template: `/speckit.constitution`

Use this command when governance or project rules change.

## Required Actions

1. Update `.specify/memory/constitution.md` with concrete, testable MUST-rules.
2. Add or update the Sync Impact Report at the top of the constitution.
3. Reconcile the following templates with the updated principles:
   - `.specify/templates/plan-template.md`
   - `.specify/templates/spec-template.md`
   - `.specify/templates/tasks-template.md`
4. Reconcile runtime guidance:
   - `AGENTS.md`
   - `CLAUDE.md`
   - `GEMINI.md`
   - `.github/copilot-instructions.md`
5. Ensure no unresolved placeholders remain in the constitution.
6. Verify toolchain, coverage, and dependency governance alignment:
   - `.NET 10` + `C# 14.0`
   - coverage gate `>=70%` with target `>=80%`
   - NuGet packages tracked against latest stable versions

## Validation Checklist

- Version line and Sync Impact Report are consistent.
- Dates are ISO `YYYY-MM-DD`.
- Principles are declarative and auditable.
- `main` protection workflow is respected (new branch + PR).
- Toolchain/coverage/dependency rules are reflected in templates and guidance files.
