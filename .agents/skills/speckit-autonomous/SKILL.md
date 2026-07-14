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
4. Confirm intake ordering, branch identity, clean ownership boundaries, the
   six baseline governance presets, and optional `autonomous-run-governance`
   v0.1.4. Stop only for a material conflict, missing required authority,
   destructive ambiguity, or an unmet hard gate.
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

Before implementation, resolve
`autonomous-run-gate-requirements-template` and create the reviewed
`specs/NNN-feature/autonomous-gate-requirements.json`. Give every acceptance
gate a stable ID, required scope, command tokens, optional runner/platform
tokens, and either `Applicable` or a justified `N/A` with re-evaluation trigger.

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
- Before merge, derive every acceptance-specific gate's command and runner from
  workflow definitions or job logs, never from a green aggregate, workflow,
  job, or OS-shaped name. Create temporary provider-neutral exact-head evidence
  from `autonomous-run-gate-evidence-template` and run the installed Bash or
  PowerShell validator. Missing, stale, contradictory, empty, duplicate-Primary,
  or token-mismatched evidence blocks merge; bypass supplies no proof.

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

Validate the exact delivery candidate, not only tracked working-tree changes.
Before a commit, stage only the intended files, run `git diff --cached --check`,
and compare the staged path inventory with repository status so no intended
untracked or unstaged file sits outside the candidate. Preserve unrelated user
changes. In `LocalImplementation`, use an equivalent per-file or temporary-index
check and restore the original index state.

Perform static review before test batches. Increment the manual build counter
before every `dotnet build` or `dotnet test`. One increment authorizes exactly
one explicit build or test invocation; do not chain multiple invocations behind
one counter update. Batch related tests inside that invocation so repeated
commands add evidence rather than administrative churn.

Invoke repository validation helpers with an explicit repository root. Accept a
helper result only when its exit status has the required value and its error
channel contains no PowerShell error record, shell command-not-found message, or
equivalent fatal signature. A nominal zero exit status does not override such an
error; record the helper as failed and route the defect to bounded remediation.

For `PublishPR` or `MergeAndSync`, validate the final reviewed head with
`bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.sh`
on macOS/Linux or `pwsh -NoProfile -File` with the matching `.ps1` on Windows;
installed mode bits are not a portable contract. The validator is read-only and
grants no remote authority. Keep the provider evidence snapshot temporary before
merge; committing it creates a new head and self-invalidates its reviewed-head claim.

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
before the main merge or when committing current review/check facts would
change the reviewed head and immediately invalidate those facts. Verify every
gate before merge, name one exact closeout evidence path in advance, keep the
closeout evidence-only, and state why it was causally necessary. Keep it
single-commit-capable by omitting any requirement to write its own PR URL,
reviewed-head result, or merge commit back into the same repository file;
verify those terminal facts externally without a recursive closeout.

If push and pull-request events start equivalent workflow sets, use the
pull-request-context checks as the delivery gate and record the duplicate runs
as operational noise. Do not cancel them unless the repository has an explicit
safe workflow or concurrency contract.

## Finish

Report artifact and task counts, changed files, validation results, skipped
conditional checks, review state, follow-ups, PR/merge identifiers when
applicable, and the exact local/remote synchronization state. Record one brief
run-retrospective entry in the feature evidence so this workflow can be refined
after future autonomous runs.
