# Autonomous Run Evidence: [FEATURE]

**Branch**: `[NNN-feature]`  
**Feature directory**: `specs/[NNN-feature]`  
**Binding intake**: `[path]`  
**Delivery mode**: `[LocalImplementation | PublishPR | MergeAndSync]`  
**Authority source**: `[current user instruction or N/A]`

## Scope

### Included

- [Accepted outcome]

### Excluded

- [Hard scope boundary]

## Run Gates

| Phase | Attempt | Result | Evidence | Remaining action |
|---|---:|---|---|---|
| Preflight | 1 | Open | [command or path] | [next action] |
| Specify | 1 | Open | `spec.md` | [next action] |
| Clarify | 1 | Open | `spec.md` | [next action] |
| Checklists | 1 | Open | `checklists/` | [next action] |
| Plan | 1 | Open | `plan.md` | [next action] |
| Tasks | 1 | Open | `tasks.md` | [next action] |
| Analyze | 1 | Open | [report summary] | [next action] |
| Implement | 1 | Open | [diff or task IDs] | [next action] |
| Validate | 1 | Open | [commands] | [next action] |
| Deliver | 1 | Open | [PR or N/A] | [next action] |

Allowed results are `Pass`, `Fail`, `Accepted`, `Deferred`, and `Open`.

## Decisions and Follow-ups

| Area | Decision | Rationale | Evidence | Residual risk | Owner | Follow-up or re-evaluation trigger |
|---|---|---|---|---|---|---|
| [area] | [feature decision term] | [why] | [path or command] | [risk or None] | [owner] | [boundary] |

## Historical Intent

| Modern area | Historical source | Intent retained | Intentional deviation | Proof or N/A rationale |
|---|---|---|---|---|
| [area] | `tv203s/[path]` | [intent] | [deviation or None] | [evidence] |

## Governance Applicability

| Preset | Version | Checkpoint | Applicability | Rationale | Evidence path | Owner | Reviewer | Result | Residual risk | Follow-up | Re-evaluation trigger |
|---|---|---|---|---|---|---|---|---|---|---|---|
| [preset] | [version] | [checkpoint] | [Applicable/N/A/Open] | [why] | [path] | [owner] | [reviewer] | [result] | [risk] | [follow-up] | [trigger] |

## Validation

| Command or review | Trigger | Result | Evidence or failure boundary |
|---|---|---|---|
| `git diff --check` | Always for tracked working-tree changes | Open | [output summary] |
| `git diff --cached --check` plus candidate-path inventory | Before `PublishPR`/`MergeAndSync` commit; equivalent new-file check for `LocalImplementation` | Open | [staged paths, untracked/unstaged boundary, and prior-index preservation] |
| [command] | [scope trigger] | [Pass/Fail/N/A] | [summary and path] |

For every repository validation helper, record the explicit repository root,
exit status, and error-channel review. A zero exit status with a PowerShell
error record, command-not-found message, or equivalent fatal signature is a
failure, not a pass.

For every explicit `dotnet build` or `dotnet test`, record the immediately
preceding manual build-counter value. One counter increment covers exactly one
invocation.

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | [Pass/N/A/Open] | [branch or rationale] |
| Pull request | [Pass/N/A/Open] | [URL or rationale] |
| Required checks | [Pass/N/A/Open] | [check summary] |
| Acceptance-gate mapping | [Pass/N/A/Open] | [required proof -> workflow/job/platform/executed command; missing scope blocks merge] |
| Review threads | [Pass/N/A/Open] | [thread count] |
| Unavailable reviews | [None or limitation] | [quota/provider evidence] |
| Reviewed head | [hash/N/A/Open] | [head whose checks and threads were verified] |
| Merge | [Pass/N/A/Open] | [commit or rationale] |
| Local `main` sync | [Pass/N/A/Open] | [`HEAD` and `origin/main`] |
| Causal closeout | [Required/N/A/Open] | [exact evidence path and why an earlier commit would be impossible or self-invalidating; omit the closeout's own URL/head/merge from that file and verify terminal facts externally] |
| Duplicate workflow events | [Observed/N/A] | [identify the pull-request-context gate; record equivalent push runs as noise unless a safe repository concurrency contract exists] |

## Retrospective

- **Effective**: [What accelerated delivery or improved proof]
- **Waste**: [Which repetition added no evidence]
- **Recurring blocker**: [Blocker or None]
- **Recommended refinement**: [Concrete runbook, skill, or template change]
