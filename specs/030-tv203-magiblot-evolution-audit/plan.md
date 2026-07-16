# Implementation Plan: TV203 and magiblot/tvision Evolution Audit

**Branch**: `030-tv203-magiblot-evolution-audit`
**Date**: 2026-07-16
**Specification**: [spec.md](spec.md)

## Summary

Feature 030 performs a read-only semantic audit of every accepted TuiVision
contract and Wave-5/Wave-6 consumer against pinned `magiblot/tvision`. It
records reproducible provenance, one relation and one `MB*` observation per
contract, then deduplicates all Feature-029 `TG*` and Feature-030 `MB*`
observations into canonical `CF*` findings or justified non-findings. Only
non-empty Primary-Owner groups create dependency-ordered hardening intakes;
exactly one independent closure intake follows last.

The implementation changes feature evidence, one test-only validator, status
and guidance surfaces, version metadata, and the archived intake. It changes
no runtime, public API, dependency, example, consumer, historical, or external
source.

## Technical Context

**Runtime**: .NET 10, C# test-only validation, Markdown and JSON evidence
**Storage**: Source-controlled closed JSON and bilingual Markdown
**Testing**: MSTest in `TuiVision.Drivers.Tests`, full Release suite, Coverlet
**Documentation**: DocFX, Playwright/Axe, UTF-8 Lynx
**External source**: Detached checkout outside the repository at exact commit
**Delivery**: `MergeAndSync`, including exact-head provider evidence
**Version**: `1.30.<patch>.<manual-build-counter>`

## Constitution Check

| Principle | Decision | Evidence |
|---|---|---|
| Security-first | Applicable to pin, hashes, closed data, scope and resume integrity | Source manifest, negative validator matrix, scans |
| Cross-platform | JSON/path tests run on repository CI platforms; no script change | Targeted and full workflows |
| A11Y and bilingual delivery | Applicable to guide, matrices, findings, and status | DocFX, Axe, Lynx |
| Agent parity | Shared completion, next intake, and wave gate updated atomically | Maintained agent surfaces and homogeneity |
| Historical policy | `tv203s/` remains read-only authority | Contract relations and evidence |
| Build counter | Increment before every explicit build or test invocation | `Directory.Build.props`, validation log |
| Coverage | Required because shared audit test infrastructure changes | Canonical Coverlet gate |
| Supply chain/cloud/regulation | Trigger-based N/A because no package, runtime distribution, cloud, service, or regulated role changes | Governance ledger |
| Autonomous governance | Applicable to state, random interruption, resume, exact gates, review, merge, and retrospective | State, gate requirements, provider evidence |

No constitution violation is accepted. Discovery of a product architecture or
breaking decision routes to `ProductDecision` and blocks the run.

## Autonomous Execution Contract

1. Maintain `pr-evidence.md` and validator-accepted
   `autonomous-run-state.json` at phase boundaries.
2. Keep accepted artifact hashes in state; tasks and evidence override a stale
   index after interruption.
3. Select one interruption phase once, store only a commitment in ignored
   local evidence, and reveal it only in the completed retrospective.
4. At the selected recoverable checkpoint, capture Git, task, state, artifact,
   process, and provider facts before emitting the agreed standalone trigger.
5. After the UI abort, require read-only status, refusal by the general
   autonomous command, and explicit resume with renewed `MergeAndSync`
   authority.
6. Mark any operation without a trustworthy terminal result
   `NeedsRevalidation`; reconstruct commit, push, PR, review, merge, and sync
   by stable identifiers before repeating.
7. Permit exactly one intentional interruption.
8. Keep writes to shared evidence, JSON datasets, status, version, statistics,
   workflow, and agent files serialized.
9. Use the predeclared `delivery-closeout.md` evidence path and one causal
   evidence-only closeout PR for post-merge state, task, retrospective, and
   main-sync facts that cannot be true before the feature merge. The closeout
   file omits its own PR URL, reviewed head, and merge commit so it remains
   single-commit-capable and non-recursive.

## Project Structure

### Feature artifacts

```text
specs/030-tv203-magiblot-evolution-audit/
├── autonomous-gate-requirements.json
├── autonomous-run-state.json
├── checklists/
├── combined-conformance-findings.json
├── combined-findings.md
├── contracts/magiblot-evolution-acceptance.md
├── data-model.md
├── delivery-closeout.md
├── magiblot-consumer-review.md
├── magiblot-contract-matrix.md
├── magiblot-evolution-audit.json
├── magiblot-source-manifest.md
├── plan.md
├── pre-wave-gate.md
├── pr-evidence.md
├── quickstart.md
├── research.md
├── spec.md
└── tasks.md
```

### Executable and maintained surfaces

```text
tests/TuiVision.Drivers.Tests/MagiblotEvolutionAuditEvidenceTests.cs
Directory.Build.props
Pflichtenheft.md
Lastenheft_Abarbeitungsreihenfolge.md
AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md
docs/project-statistics.md
```

The final archive uses the repository rename workflow. External checkout,
`tv203s/`, `TVDEMOS/`, `TVFM/`, `src/`, `examples/`, package manifests, and
generated output are protected write boundaries.

## Phase 0: Research and Source Freeze

- Reconcile Feature-029 handoff cardinalities and hashes.
- Create an external detached magiblot checkout and prove repository, commit,
  tree, timestamp, subject, and multipart COPYRIGHT hash.
- Select the minimum relevant source, test, platform, and example paths needed
  for all fourteen comparison chapters.
- Record path, SHA-256, short original behavior summary, and pinned permalink.
- Confirm that building magiblot is unnecessary and no external dependency is
  installed.

## Phase 1: Evidence Model and Acceptance Contract

- Define closed entities for source records, contract relations, consumer
  reviews, MB observations, combined dispositions, canonical CF findings,
  owner dependencies, generated intakes, governance, and validation.
- Define exact allowed vocabularies and reciprocal links.
- Define the zero-finding and non-zero-finding numbering algorithm.
- Create the acceptance contract and quickstart.
- Create reviewed exact-head gate requirements before implementation.

## Phase 2: Evidence Foundation and Test-First Slice

- Add the test-only evidence validator before accepted datasets exist.
- Review the complete compile surface and reuse the existing repository-root
  helper and `System.Text.Json` patterns.
- Establish a red proof for missing Feature-030 datasets only.
- Complete D02 (`C004`-`C006`) as the vertical slice with source records,
  relations, MB observations, combined dispositions, and consumer/proof links.
- Run the isolated slice test green before expanding the matrix.

## Phase 3: Full Contract and Consumer Review

- Populate exactly one relation and one MB observation for every accepted
  contract.
- Review all fourteen comparison chapters and accepted consumers.
- Add no new contract unless a real uncovered consumer responsibility passes
  all five intake gates.
- Validate source, contract, observation, consumer, proof, and historical links
  bidirectionally.
- Record direct-lineage shared-bias risk explicitly.

## Phase 4: Combined Dedupllication and Follow-up Generation

- Consume all 48 Feature-029 `TGO*` observations.
- Give every TG and MB observation one combined disposition.
- Create canonical CF findings only for reproduced TuiVision gaps.
- Assign exactly one Primary Owner and validate the dependency DAG.
- Generate only non-empty owner intakes from Feature 031 in topological order.
- Generate exactly one independent closure intake last.
- Keep both waves blocked and update readable matrices and gate evidence.

## Phase 5: Full Validation and Delivery

- Complete all task and checklist acceptance.
- Run static, targeted, full, coverage, documentation, A11Y, scope, secret,
  dependency, generated-output, protected-source, agent-parity, and platform
  gates according to actual triggers.
- Archive the intake and align all maintained status surfaces.
- Stage the exact candidate, run cached diff/inventory checks, align version,
  commit, push, create the PR, and converge reviews.
- Route the stable PR identity into one final pre-review metadata commit after
  PR creation. Keep exact current-head check and review facts temporary until
  merge so the evidence does not invalidate itself.
- Map each required gate to the actual workflow, job, runner/platform, command,
  and exact reviewed head; validate provider evidence.
- Merge under authorized policy, delete the branch, synchronize local `main`,
  and run the autonomous retrospective.
- Persist terminal feature-merge, first main-sync, task, retrospective, and
  state facts through the predeclared non-recursive evidence-only closeout.

## Validation Strategy

| Scope | Required proof |
|---|---|
| Every candidate | Diff, JSON, Markdown, placeholder, protected and generated path checks |
| Audit validator | Targeted Release tests including prior audit/closure validators |
| Shared test infrastructure | Full Release tests and canonical five-assembly coverage |
| Learner-facing docs | DocFX, Playwright/Axe, UTF-8 Lynx |
| External source | Exact Git object and SHA-256 proof; clean external checkout |
| Agent/status updates | Maintained-surface parity and homogeneity |
| Delivery | Required platform checks, secrets, exact-head gate evidence, zero actionable threads |
| Interruption | Read-only status, general-command refusal, authority revalidation, idempotent resume |

## Version and Build-Counter Strategy

- The branch version is `1.30.<patch>.<build>`.
- Before each explicit `dotnet build` or `dotnet test`, increment only the
  manual build counter. One increment authorizes exactly one invocation.
- Before commit or push, align `Version`, `AssemblyVersion`, and `FileVersion`
  without another increment unless another build or test ran.
- Record every counter-command pair in `pr-evidence.md`.

## Complexity Tracking

| Complexity | Justification | Containment |
|---|---|---|
| Large audit matrix | 48 contracts, 48 TG observations, 48+ MB observations, 13 consumers | Closed JSON plus generated-readable summaries |
| Direct-lineage bias | Agreement is not independent confirmation | Explicit shared-bias field and source hierarchy |
| Combined finding graph | Must prevent duplicate remediation and owner cycles | One canonical CF set and DAG validator |
| Random interruption | May occur in a remote operation | Stable operation IDs, NeedsRevalidation, idempotent reconstruction |

No product abstraction is added.
