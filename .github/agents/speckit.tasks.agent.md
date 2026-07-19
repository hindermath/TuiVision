

<!-- Source: security-governance -->
Before continuing, apply the Security Governance preset:

- convert MSL applicability and justification needs into explicit tasks
- convert security obligations into explicit tasks
- include evidence-production tasks under `docs/security/`
- avoid leaving secure-development work as undocumented assumptions

Before continuing, apply the Architecture Governance preset:

- convert architecture obligations into explicit tasks
- include `docs/security/` evidence updates
- add BSI C3A cloud autonomy applicability tasks when cloud services or
  provider-dependent deployments are in scope
- do not leave threat-modeling or ADR work implicit

Before continuing, apply the iSAQB Architecture Governance preset:

- convert architecture goals, quality scenarios, views, ADRs, risks, and
  technical debt into explicit tasks
- include concrete evidence-production tasks under `docs/architecture/`
- add architecture-review tasks for significant structure, interface,
  runtime, or deployment changes
- if security-relevant architecture is affected, include the corresponding
  secure-architecture tasks from `architecture-governance`

Before continuing, apply the A11Y Governance preset:

- convert accessibility expectations into explicit tasks
- convert bilingual delivery work into explicit tasks
- do not leave A11Y or language review implicit

Before continuing, apply the Cross-Platform Governance preset:

- add explicit tasks for both `*.sh` and `*.ps1` variants in the same
  change
- add tasks for the Unix man-page and the bilingual PowerShell help
  block
- add a task to expose the PowerShell variant as a Cmdlet with an
  approved `Verb-Noun` name
- add a parity-verification task using the script-parity checklist

Before continuing, apply the Agent Parity Governance preset:

- add explicit tasks to update every maintained agent surface in the
  same change
- add tasks to propagate shared rules into project templates and the
  local constitution mirror
- add a parity-verification task using the agent-parity checklist

# Command Template: `/speckit.tasks`

Use this command to generate an executable task list from `plan.md` and `spec.md`.

## Required Actions

1. Organize tasks by user story for independent delivery.
2. Include Red-Green-Refactor test tasks before implementation tasks.
3. Include documentation tasks:
   - bilingual updates (German block first, then English)
   - XML documentation completeness
   - `docfx docfx.json` run when API/XML docs changed
   - architecture evidence under `docs/architecture/` when structure, interfaces,
     quality attributes, runtime behavior, deployment, or technical debt changes
4. Include coverage and dependency tasks:
   - coverage evidence for `>=70%` minimum and `>=80%` target tracking
   - `dotnet list package --outdated` review and update tasks
5. Include PR preparation task (purpose, touched projects, test evidence, config/API impact).
6. Create evidence before implementation, schedule one representative vertical
   slice first, and use test-first proof for observable contracts.
7. Group tasks by reviewable outcomes instead of individual evidence cells;
   serialize all shared evidence, version, statistics, workflow, and agent-file
   writes.
8. Include trigger-based validation and only the remote closeout tasks allowed
   by the plan's delivery mode.

## Validation Checklist

- Every code change has corresponding tests.
- Documentation and governance tasks are present.
- Task ordering supports incremental, verifiable delivery.
- Coverage and dependency currency tasks are explicitly scheduled.
- Architecture evidence and `N/A` rationale tasks are explicitly scheduled.
- Task count is proportional to reviewable outcomes, and every iterative or remote phase has a clear completion gate.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.


Audit-ready evidence requirement:

- Ensure this tasks wrapper requires concrete Markdown evidence/checklist updates for every applicable checkpoint.
- If a checkpoint does not apply in the current Spec-Kit run, require `N/A` with a short rationale instead of omitting it.
- If a checkpoint is undecided, require `Open` with owner, follow-up, and re-evaluation trigger.
