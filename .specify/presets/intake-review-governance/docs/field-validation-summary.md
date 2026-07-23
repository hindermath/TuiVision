# Field Validation Summary: v0.1.1

Date: 2026-07-23

## Synthetic validation

- Bash and PowerShell validators accepted the same strict UTF-8 review.
- LF and CRLF representations produced the same normalized SHA-256.
- Content drift failed in both validators with exit code 2.
- Agent-authored risk acceptance failed in both validators.
- Missing worker coverage, result drift, expired exceptions, Critical/High
  findings, and unanswered material questions are blocking cases.

## Series request graph

A three-target schema-1.1 fixture binds its repository-relative request with
the same strict UTF-8 normalization used for intake targets. Bash and
PowerShell accepted one root and two ordered predecessor edges with identical
exit behavior. The fixture suite rejects missing request evidence, request
hash drift, identity or role mismatch, target-set drift, duplicate or missing
order entries, unknown and self edges, duplicate edges, order contradictions,
cycles, and root sets that differ from the graph's zero-indegree targets.

The field observation from `SecureServiceHarvester-CSharp` contained 66
targets and 64 predecessor edges. An independent graph audit found an
intermediate request error that v0.1.0 could not reject. Version 0.1.1 makes
that class of accepted Series evidence impossible without a current,
structurally valid request binding.

## Package composition and agent parity

A temporary Spec Kit 0.12.11 project accepted all ten presets at priorities
10 through 80. `add`, `list`, `info`, `resolve`, `disable`, `enable`, `remove`,
and reinstall completed successfully with Intake Review v0.1.1 at priority 65.

Separate temporary Codex, Claude, Copilot Skills, and Antigravity projects each
contained exactly one generated surface for `speckit.intake-review`,
`speckit.intake-review-status`, and `speckit.intake-repair`. Codex and
Antigravity were intentionally validated in separate projects because both
integrations own the `.agents/skills` layout.

## TuiVision intake

`Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md` was reviewed
without modifying the target. Its scope, non-goals, evidence model, stop
boundaries, and copyable prompts are substantial and testable. The review
found one deterministic Medium documentation drift: section 12 still says
"Sieben-Preset-Matrix" although TuiVision uses eight standard presets. The
correct outcome before repair is `NeedsRemediation`, demonstrating that review
detects stale governance text before Specify.

## Secure CaseTracker campaign

Immutable evidence from campaign `91c2c1a0-1526-479a-b8a3-e36a7d15d2b1`
was replayed read-only:

- four unique intake contents;
- six language pipelines: C#, Go, Java, Python, Rust, and Swift;
- 24 worker applicability rows;
- maximum declared concurrency 3;
- 18 dependency edges.

The first schema-1.2 preflight correctly rejected the campaign because Unit 00
requires Container-First execution while the native macOS override existed
only in operator prose. The accepted fixture added one explicit human-owned,
dated, expiring exception covering the six Unit-00 workers. The second
preflight and standalone result validator passed. Removing one applicability
row failed before worker scheduling.

## Disposition

The tested behavior is provider-neutral and supports publication as
`intake-review-governance` v0.1.1, `autonomous-run-governance` v0.3.2, and
`parallel-autonomous-run-governance` v0.2.3. The standard eight-preset profile
remains unchanged; the optional authoring-and-review profile contains ten
presets.
