# Acceptance Contract: magiblot/tvision Evolution Audit

## Purpose

Define the closed acceptance boundary for Feature 030 without changing
TuiVision product behavior or copying external source.

## Required Artifacts

- Specification, plan, research, data model, quickstart, tasks, checklists
- `pr-evidence.md`, run state, and gate requirements
- magiblot source manifest, JSON audit, contract and consumer matrices
- combined findings JSON and readable findings report
- pre-wave gate and exactly derived follow-up Lastenheft files
- test-only Feature-030 validator

## Dataset Acceptance

1. Exact magiblot repository, commit, tree, timestamp, subject, and COPYRIGHT
   hash.
2. Exactly one allowed relation and one MB observation per accepted contract.
3. Complete source, contract, consumer, observation, proof, and historical
   reciprocal links.
4. Exactly one combined disposition per Feature-029 TG and Feature-030 MB
   observation.
5. Every CF finding has one Primary Owner and an acyclic dependency graph.
6. Unknown, missing, duplicate, orphaned, contradictory, or cyclic data fails
   closed.

## Follow-up Acceptance

1. Empty owner groups create no hardening intake.
2. Non-empty groups create one intake each from Feature 031 in topological
   order.
3. Exactly one independent closure intake follows last.
4. Zero findings produce Feature 031 as closure only.
5. Wave 5 and Wave 6 remain blocked through that closure merge.

## Markdown Acceptance

- German-first/English-second CEFR-B2 where learner-facing
- semantic headings, lists, and text-first tables
- exact source and decision vocabulary
- no copied upstream source or unsupported license simplification
- no unresolved markers or open starter rows

## Scope Acceptance

The candidate contains no product runtime, public API, dependency, package,
example, consumer, historical, or external-source modification and no
generated DocFX/test output.

## Interruption Acceptance

Exactly one random recoverable phase is interrupted. Status is read-only, the
general command refuses implicit continuation, resume revalidates authority
and drift, uncertain operations become `NeedsRevalidation`, and completed
operations are not duplicated.

## Remote Acceptance

All required technical checks map to actual commands on the exact reviewed
head, no actionable thread remains, unavailable reviews are honest, and any
human-approval-only bypass is narrow and documented. One predeclared,
single-commit-capable evidence-only closeout persists post-merge task, state,
retrospective, and synchronization facts without requiring its own remote
identity inside the same file. The run then ends on clean synchronized `main`.
