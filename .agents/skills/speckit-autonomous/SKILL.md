---
name: speckit-autonomous
description: Orchestrate a complete repository-local Spec-Kit feature from intake through specification, repeated clarification, planning, checklists, tasks, repeated analysis, implementation, validation, and an explicitly authorized GitHub delivery closeout. Use when the user delegates an entire autonomous Spec-Kit run or asks to continue one without manual prompts between normal phases.
---

# Autonomous Spec-Kit Run

Follow `docs/spec-kit-autonomous-runbook.md` as the binding repository workflow.
Use the existing `speckit-*` and `speckit-git-*` skills for their individual
stages; this skill only orchestrates them and enforces cross-stage gates.

## Start

1. Read `AGENTS.md`, `.specify/memory/constitution.md`, the selected intake,
   `.specify/feature.json`, and the current Git state.
2. Run `specify check`. Prefer the matching PowerShell 7 repository script on
   this project when both Bash and PowerShell variants exist.
3. Determine the delegated delivery mode from the user's request:
   `LocalImplementation`, `PublishPR`, or `MergeAndSync`. Never infer remote
   write or merge authority from general autonomy alone.
4. Confirm intake ordering, branch identity, clean ownership boundaries, and
   the six installed governance presets. Stop only for a material conflict,
   missing required authority, destructive ambiguity, or an unmet hard gate.
5. Create the numbered feature branch through `speckit-git-feature` when a new
   feature is required. Resume the existing branch and feature directory when
   the run already exists.

## Orchestrate

Execute these stages in order:

1. `speckit-specify`
2. `speckit-clarify` until clarification convergence
3. requirements and feature-specific `speckit-checklist` passes
4. `speckit-plan`
5. plan-review checklists, followed by applying every actionable instruction
6. `speckit-tasks`
7. `speckit-analyze` and remediation until analysis convergence
8. `speckit-implement`
9. local validation and evidence completion
10. authorized Git and GitHub delivery closeout

Create `specs/NNN-feature/pr-evidence.md` from
`.specify/templates/autonomous-run-evidence-template.md` before the first
implementation edit. Keep scope, decisions, commands, results, skipped
triggers, residual risks, review state, and follow-ups current during the run.

## Converge

- Clarification converges when no remaining question would materially change
  plan, task decomposition, validation, acceptance, or scope.
- Checklist convergence requires every item to pass or carry an explicit
  accepted disposition. Apply checklist instructions before rechecking.
- Analysis converges when no `CRITICAL` or `HIGH` finding remains and every
  `MEDIUM` finding is remediated or explicitly accepted with rationale and
  owner. Do not recycle accepted low-style findings.
- Implementation converges when every task is complete or has an allowed,
  evidenced conditional outcome and all triggered validation passes.
- Remote review converges when required checks pass and no actionable review
  thread remains. Record unavailable reviewers or quota failures as evidence;
  never represent them as successful reviews.

## Shape Work

- Establish evidence before implementation.
- Deliver one representative vertical slice with its test and proof before
  spreading a repeated pattern.
- Use test-first proof where a contract can fail observably.
- Before the first red batch, check the complete compile surface: imports,
  public XML docs, harness helpers, focus/ownership assertions, and linked-source
  assembly identity.
- Group independent negative cases into one bounded project-local red matrix
  only when every expected failure remains explicit and ownership stays local.
- Do not assume shared CLR type identity when one source file is linked into
  multiple assemblies; prove through public contracts or state delegates, or
  use one intentionally shared compiled assembly when the architecture allows.
- Group tasks by independently reviewable outcome. Do not create one task per
  evidence cell when one bounded task can update a complete matrix slice.
- Require every remote or delivery task to name the exact repository evidence
  path that records its acceptance result; an implicit "record evidence" is
  not sufficient for analysis or resume.
- Serialize writes to shared evidence, version, statistics, workflow, and
  agent-guidance files.
- Protect scope. Route discovered runtime, design, parity, or proof work outside
  the accepted feature to a named follow-up instead of silently expanding it.
- Treat historical sources as read-only intent evidence, never as a mechanical
  line-by-line port target.

## Validate

Always run repository-required static checks. Add targeted tests for touched
code, the full Release and coverage gates for shared executable behavior, the
DocFX plus web-A11Y path for triggered documentation surfaces, script parity
for scripts, and app-loop plus state/view/rendering proof for visual UI work.

Perform static review before test batches. Increment the manual build counter
before every `dotnet build` or `dotnet test`, but batch related tests so repeated
commands add evidence rather than administrative churn.

## Deliver

Use logical Git checkpoints only after their acceptance gates pass. On numbered
branches, align `Directory.Build.props` before commit or push as required by
repository policy. Never commit generated DocFX output, caches, credentials,
logs, or test results.

- `LocalImplementation`: stop after complete local evidence; do not push or
  create a PR.
- `PublishPR`: commit, push, create or update the PR, and process authorized
  review/CI fixes; do not merge.
- `MergeAndSync`: complete `PublishPR`, converge checks and reviews, merge only
  under the delegated policy, delete obsolete remote branches, switch to local
  `main`, pull/prune, and prove `HEAD == origin/main` with a clean tree.

Use a closeout PR only when post-merge facts cannot truthfully be recorded
before the main merge. Keep that closeout evidence-only and state why it was
causally necessary.

## Finish

Report artifact and task counts, changed files, validation results, skipped
conditional checks, review state, follow-ups, PR/merge identifiers when
applicable, and the exact local/remote synchronization state. Record one brief
run-retrospective entry in the feature evidence so this workflow can be refined
after future autonomous runs.
