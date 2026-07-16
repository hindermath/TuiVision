# Acceptance Contract: Terminal.GUI Conformance Audit

## Purpose

This contract defines the durable acceptance surface shared by the Feature-029
JSON datasets, readable evidence, MSTest validation, Feature-030 handoff, and
the pre-Wave gate.

## Required Artifacts

| Artifact | Acceptance role |
|---|---|
| `terminalgui-conformance-audit.json` | Canonical Feature-029 machine-readable audit |
| `terminalgui-source-manifest.md` | Pinned source, hash, behavior, license, and no-copy evidence |
| `terminalgui-contract-matrix.md` | Human-readable 48-contract relation matrix |
| `terminalgui-consumer-review.md` | Complete Wave-5/Wave-6 consumer relation review |
| `terminalgui-findings.md` | Findings and non-findings with complete decisions |
| `feature030-handoff.json` | Machine-readable complete successor handoff |
| `feature030-handoff.md` | Human-readable handoff and dependency summary |
| `pre-wave-gate.md` | Feature-029 result, blocked Waves, and next intake |
| `pr-evidence.md` | Governance, validation, delivery, and retrospective truth |

## Dataset Acceptance

1. Both JSON documents parse with `System.Text.Json`.
2. The source identity equals the exact v1.9.0 tag object, peeled commit, MIT
   license, and license SHA-256.
3. Exactly 16 canonical domain IDs and exactly `C001` through `C048` exist.
4. Every existing contract has exactly one allowed Terminal.GUI relation.
5. Every non-`NotApplicable` relation references at least one valid source
   record; every source link is reciprocal.
6. Every relation names an existing TuiVision proof and complete consumer
   relevance or an explicit empty rationale.
7. Every `NotApplicable` relation has rationale and re-evaluation trigger.
8. `C049+` exists only when all admission conditions are represented.
9. Every observation has one allowed decision, one Primary Owner, complete
   review data, and a unique deduplication key.
10. Observation dependencies are acyclic.
11. The Feature-030 handoff contains every observation and agrees with
    contract, owner, dependency, and deduplication data.
12. Both follow-up-document flags are false and Feature 030 is the sole next
    intake.

## Markdown Acceptance

- Every source ID, contract ID, consumer ID, and observation ID from JSON is
  discoverable in the readable evidence.
- Counts and decision totals agree with JSON.
- German explanations precede equivalent English explanations.
- Tables use semantic headers and text-first meaning.
- No copied Terminal.GUI implementation text appears.
- Source permalinks use the pinned commit.

## Scope Acceptance

The final diff contains no product source, public API, package, dependency,
example, historical tree, consumer tree, external source, or generated output
change. Executable changes are limited to test-only evidence validation.

## Downstream Acceptance

- Feature 029 creates no hardening or closure Lastenheft.
- Feature 030 receives every finding and non-finding.
- Feature 030 alone may deduplicate `TG*` and later `MB*` observations into
  canonical findings and owner groups.
- Wave 5 and Wave 6 remain blocked.
- `ProductDecision` blocks autonomous delivery.

## Remote Acceptance

Required PR-context checks pass on the reviewed head, exact-head gate evidence
validates, unavailable reviews remain honestly reported, GraphQL has no
actionable unresolved thread, and merge follows the authorized policy.
Self-invalidating terminal facts use at most one causal evidence-only closeout.
