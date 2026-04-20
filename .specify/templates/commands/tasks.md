# Command Template: `/speckit.tasks`

Use this command to generate an executable task list from `plan.md` and `spec.md`.

## Required Actions

1. Organize tasks by user story for independent delivery.
2. Include Red-Green-Refactor test tasks before implementation tasks.
3. Include documentation tasks:
   - bilingual updates (German block first, then English)
   - XML documentation completeness
   - `docfx docfx.json` run when API/XML docs changed
4. Include coverage and dependency tasks:
   - coverage evidence for `>=70%` minimum and `>=80%` target tracking
   - `dotnet list package --outdated` review and update tasks
5. Include PR preparation task (purpose, touched projects, test evidence, config/API impact).

## Validation Checklist

- Every code change has corresponding tests.
- Documentation and governance tasks are present.
- Task ordering supports incremental, verifiable delivery.
- Coverage and dependency currency tasks are explicitly scheduled.
