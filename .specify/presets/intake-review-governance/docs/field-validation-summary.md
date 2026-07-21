# Field Validation Summary: v0.1.0

Date: 2026-07-21

## Synthetic validation

- Bash and PowerShell validators accepted the same strict UTF-8 review.
- LF and CRLF representations produced the same normalized SHA-256.
- Content drift failed in both validators with exit code 2.
- Agent-authored risk acceptance failed in both validators.
- Missing worker coverage, result drift, expired exceptions, Critical/High
  findings, and unanswered material questions are blocking cases.

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
`intake-review-governance` v0.1.0, `autonomous-run-governance` v0.3.2, and
`parallel-autonomous-run-governance` v0.2.3. The standard eight-preset profile
remains unchanged; the nine-preset profile is explicit and optional.
