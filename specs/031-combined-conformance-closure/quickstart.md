# Quickstart: Gemeinsamer Konformitätsabschluss

## Zweck / Purpose

Dieser Quickstart beschreibt die reproduzierbare Review- und
Validierungsreihenfolge für Feature 031. Er startet weder Wave 5 noch Wave 6.

*This quickstart describes the reproducible review and validation order for
Feature 031. It starts neither Wave 5 nor Wave 6.*

## 1. Ausgangszustand / Starting State

```bash
git switch 031-combined-conformance-closure
git status --short --branch
jq . .specify/feature.json
bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.sh \
  --state specs/031-combined-conformance-closure/autonomous-run-state.json
```

Expected feature path:

```text
specs/031-combined-conformance-closure
```

## 2. Strukturierte Eingaben / Structured Inputs

Review these files as immutable input:

```text
specs/024-tv203-freevision-conformance-audit/conformance-audit.json
specs/028-pre-wave5-wave6-conformance-closure/closure-evidence.json
specs/029-tv203-freevision-terminalgui-conformance-audit/terminalgui-conformance-audit.json
specs/030-tv203-magiblot-evolution-audit/magiblot-evolution-audit.json
specs/030-tv203-magiblot-evolution-audit/combined-conformance-findings.json
```

Use Features 025 and 026 evidence for the final `F001`-`F013` ownership and
Red/Green boundaries.

## 3. Externe Provenance / External Provenance

Use detached or otherwise read-only checkouts outside TuiVision:

```text
/tmp/tuivision-fv-024-ffc03b34
/tmp/terminal-gui-v1.9.0-*
/tmp/magiblot-tvision-030-57b6f56
```

Confirm the exact commits, tag object/tree, license hashes, and all accepted
source hashes. Never add these directories to Git.

## 4. Targeted Closure Proof

Before each command beginning with `dotnet build` or `dotnet test`, align
`Directory.Build.props` to `1.31.<patch>.<build>` and increment exactly the
manual build counter.

Before a later commit or push, align `Version`, `AssemblyVersion`, and
`FileVersion` to the current `1.31.<patch>.<build>` value without incrementing
the build counter unless another build or test is run.

The targeted Release invocation covers:

```text
CombinedConformanceClosureEvidenceTests
ConformanceAuditEvidenceTests
ConformanceClosureEvidenceTests
TerminalGuiConformanceEvidenceTests
MagiblotEvolutionAuditEvidenceTests
```

It must prove:

- 48 contracts;
- 13 consumers;
- 48 TGO plus 48 MB observations;
- 96 dispositions;
- 13 closed prior findings;
- three empty owner groups;
- zero canonical findings, product decisions, dependency edges, and hardening
  intakes;
- fail-closed malformed and contradiction cases.

## 5. Full Gates

Run the repository-required sequence:

```text
git diff --check
dotnet format --verify-no-changes
targeted Release tests
full Release tests
canonical Coverlet coverage
docfx docfx.json
tests/web-a11y Playwright/Axe
UTF-8 text-first review
secret and scope scans
agent homogeneity
```

Record every command, build counter, result, metric, and failure boundary in
`pr-evidence.md`.

## 6. Feature-head Gate

The feature head may become:

```text
ReadyForMerge
Wave 5: BlockedPendingCausalClosure
Wave 6: BlockedPendingCausalClosure
```

It may not claim its own future merge.

## 7. Remote Delivery

For the exact reviewed head:

1. map each gate to actual workflow, job, platform, and command;
2. validate temporary exact-head evidence;
3. confirm required checks are green;
4. confirm zero actionable review threads;
5. record unavailable reviewers honestly;
6. merge only under the delegated narrow policy;
7. delete the feature branch and synchronize local `main`.

## 8. Causal Closeout

After the feature merge, one evidence-only closeout may record:

```text
Wave 5: Eligible
Wave 6: ConditionallyReady
Stage: Retrospective
Status: Completed
nextExactAction: N/A
```

The closeout does not start either Wave. It also does not write its own remote
identity back into the same file.

Before creating it, search all marker consumers:

```bash
rg -n \
  "BlockedPendingCombinedConformanceClosure|BlockedPendingCausalClosure|Eligible|ConditionallyReady|Feature 031|Lastenheft_16" \
  tests specs Pflichtenheft.md Lastenheft_Abarbeitungsreihenfolge.md \
  AGENTS.md CLAUDE.md GEMINI.md \
  .github/copilot-instructions.md .github/agents/copilot-instructions.md
```

No closeout test or executable file may be needed: the feature-branch
validator must already enforce both causal states.
