# Implementation Plan: Pre-Wave-5 Conformance Closure

**Branch**: `027-pre-wave5-conformance-closure` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)  
**Input**: `Lastenheft_09_Pre-Wave5-Conformance-Closure.md` and merged Feature-024 artifacts

## Summary

Revalidate the complete merged Feature-024 audit, execute all repository and
remote release gates, and release Wave 5 only when every closure boundary
passes. Reuse the existing machine-verifiable dataset and MSTest validator;
create Feature-027 evidence and update status surfaces without product runtime,
public API, package, example, or historical-source changes.

## Technical Context

**Language/Version**: C# / .NET 10 for existing tests; Markdown and JSON evidence  
**Primary Dependencies**: existing MSTest, Coverlet, DocFX, Playwright/Axe, Lynx, Git/GitHub tooling  
**Storage**: repository files only; external Free Vision checkout remains read-only outside Git  
**Testing**: focused 024 evidence tests, full Release suite, canonical coverage, documentation/A11Y, static and remote gates  
**Target Platform**: local macOS plus GitHub Ubuntu/macOS and Homogeneity macOS/Linux/Windows evidence  
**Project Type**: evidence-only framework closure  
**Performance Goals**: not applicable; no runtime path changes  
**Constraints**: zero product/API/dependency/example/historical changes; no empty 025/026 work; one build-counter increment per explicit build/test command  
**Scale/Scope**: 16 domains, 48 contracts, 151/119/176 inventories, 15 external records, 94 proof references, 132 completed source-audit tasks as input

## Constitution Check

| Gate | Result |
|---|---|
| Binding Lastenheft and numbered branch | Pass: Feature 027 and exact intake are fixed |
| Historical source policy | Pass: Borland/`tv203s/` primary and read-only; Free Vision secondary and external |
| No runtime/API/dependency scope | Pass: any new drift stops closure and routes to audit revision |
| Test and coverage policy | Pass: focused, full Release, and five assembly gates are mandatory |
| Documentation/A11Y policy | Pass: status docs trigger DocFX, Axe, and Lynx |
| Versioning | Pass: `1.27.<patch>.<build>` and one increment per explicit build/test invocation |
| Agent parity | Pass: five surfaces are serialized and synchronized |
| Autonomous authority | Pass: explicit `MergeAndSync`; narrow bypass only for sole Human Approval blocker |
| No-empty-work | Pass: 025/026 remain suppressed and retrospective/closeout PRs require a real diff |

No constitution exception is required.

## Project Structure

### Documentation for this feature

```text
specs/027-pre-wave5-conformance-closure/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── tasks.md
├── closure-evidence.md
├── pr-evidence.md
├── contracts/
│   └── closure-acceptance.md
└── checklists/
    ├── requirements.md
    ├── plan-quality.md
    ├── plan-review.md
    └── closure-readiness.md
```

### Existing proof surfaces

```text
specs/024-tv203-freevision-conformance-audit/
├── conformance-audit.json
├── framework-inventory.md
├── framework-conformance-matrix.md
├── freevision-source-manifest.md
├── findings.md
├── pre-wave5-gate.md
└── pr-evidence.md

tests/TuiVision.Drivers.Tests/
└── ConformanceAuditEvidenceTests.cs
```

**Structure Decision**: Add only Feature-027 evidence and status updates. The
024 dataset and validator remain canonical and are not duplicated. Test-only
changes are allowed only when a closure proof boundary is missing, not to alter
accepted decisions.

## Phase 0: Research and Baseline Freeze

1. Freeze the merged 024 product baseline and current closure head.
2. Reconcile 024 PR/merge/closeout/retrospective evidence and exact counts.
3. Define allowed post-audit paths: evidence, closeout, retrospective, intake,
   specification, agent context, version, and statistics only.
4. Revalidate Home-Baseline PowerShell helper fix using explicit root, JSON,
   exit, and error channel without modifying that repository from 027.
5. Record all seven preset versions and trigger boundaries.

## Phase 1: Evidence Model and Contracts

1. Create `closure-evidence.md` before closure implementation edits.
2. Record each revalidation check with stable ID, result, command/proof,
   baseline, owner, residual risk, and re-evaluation trigger.
3. Record one aggregate Wave-5 decision with explicit blocker semantics.
4. Keep PR delivery evidence separate from the closure decision to avoid
   self-invalidating current-head claims.

## Phase 2: User Story 1 - Baseline Revalidation

1. Run the focused audit evidence suite after one build-counter increment.
2. Independently query JSON counts and live path/public-type inventories.
3. Compare all protected product and historical paths with the 024 product
   baseline.
4. Stop if any cardinality, decision, finding, proof, or protected path drifts.

## Phase 3: User Story 2 - Full Gates

1. Run static diff and format checks.
2. Increment once and run the complete Release suite.
3. Validate Coverlet configuration, increment once, and run canonical coverage.
4. Build DocFX, run Playwright/Axe, and inspect representative pages with Lynx.
5. Run secrets, generated-output, dependency, API, runtime, example, and
   historical-source scans.

## Phase 4: User Story 3 - Formal Gate Decision

1. Confirm all closure check rows pass and no accepted finding owner set opened.
2. Mark the Pre-Wave-5 Pflichtenheft item complete.
3. Mark 027 complete and Wave 5 as the next intake in ordering.
4. Synchronize the completed 027 context across all five agent surfaces.
5. Update project statistics and archive the Lastenheft.

## Phase 5: User Story 4 - Delivery and Learning

1. Align version, commit, push, and open the feature PR.
2. Converge PR-context checks, review availability, comments, and GraphQL
   threads; remediate only within accepted scope.
3. Merge with the approved strategy and narrow bypass boundary, delete the
   branch, and synchronize local `main`.
4. Use one causal evidence closeout for post-merge facts.
5. Run retrospective and update Home Baseline or upstream only for a real,
   reproduced portable gap; otherwise record `NoPromotion`.

## Validation Strategy

| Trigger | Validation | Acceptance |
|---|---|---|
| Existing audit contract | focused `ConformanceAuditEvidenceTests` | exact counts and all negative boundaries pass |
| Repository integration | full Release suite | all projects pass with explicit totals |
| Coverage constitution | canonical Coverlet | each of five assemblies >= 70% |
| C# or project metadata | `dotnet format --verify-no-changes` | exit 0 |
| Published status documentation | DocFX, Playwright/Axe, Lynx | 0 errors, 2/2 smoke, readable text path |
| Security and scope | secret and path/diff scans | high secrets 0; forbidden diffs 0 |
| Remote delivery | required PR-context checks and GraphQL | all technical gates green; actionable threads 0 |

## Complexity Tracking

No complexity exception. Reusing the 024 dataset and validator avoids a second
audit model, while separate 027 evidence preserves independent closure review.
