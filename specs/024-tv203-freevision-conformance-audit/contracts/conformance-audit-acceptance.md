# Acceptance Contract: Framework Conformance Audit

## Purpose

This contract defines the durable acceptance surface shared by the JSON audit
dataset, human-readable evidence, MSTest validation, and the pre-Wave-5 gate.

## Required artifacts

| Artifact | Acceptance role |
|---|---|
| `conformance-audit.json` | Canonical structured data and machine validation source |
| `framework-inventory.md` | Human-readable historical, modern-source, and public-contract inventory summary |
| `framework-conformance-matrix.md` | Human-readable contract decisions and proof boundaries |
| `freevision-source-manifest.md` | Pinned external path, hash, behavior, and provenance evidence |
| `findings.md` | Complete actionable drift/gap ledger or explicit zero-finding statement |
| `consumer-readiness-review.md` | Read-only Wave-5/Wave-6 flow mapping and proof-strength review |
| `pre-wave5-gate.md` | Aggregate gate decision and downstream feature boundaries |
| `pr-evidence.md` | Commands, governance, validation, delivery, and retrospective truth |

## Dataset acceptance

1. JSON parses with `System.Text.Json` using a closed expected shape.
2. Exactly 16 domain IDs exist.
3. Historical paths equal the live `.cc` inventory and canonical ledger exactly.
4. Modern source paths equal the live maintained five-module `.cs` inventory exactly.
5. Public contract names equal exported reflection types from all five assemblies exactly.
6. Every inventory item has one domain and at least one valid contract; every
   item-to-contract and contract-to-item link is reciprocal.
7. Every contract has one allowed primary decision and one allowed Free Vision relation.
8. Every external source record uses the approved repository and commit and names a hash.
9. Drift/gap contracts have exactly one complete finding with consumer scope,
   reproduction, source evidence, acceptance boundary, and disposition; other
   contracts have none.
10. All IDs and paths are unique where the data model requires uniqueness.

## Markdown acceptance

- Every JSON domain and contract is discoverable by ID in the readable evidence.
- Counts and decision totals agree with JSON.
- German explanation precedes equivalent English explanation for learner-facing blocks.
- Tables use semantic headers and do not encode meaning only through color or layout.
- No copied Free Vision implementation text appears.

## Scope acceptance

The final feature diff contains no path under product source, examples,
historical trees, package manifests, or generated output. Permitted executable
changes are limited to test-only evidence validation and test-project references.

## Downstream acceptance

- `Core025` findings alone may define feature 025.
- `ComponentData026` findings alone may define feature 026.
- Empty owner sets create no branch or PR.
- The historical Feature-027 result remains traceable but is superseded for
  forward planning by Revision 2.
- Feature 028 remains mandatory after 025 and 026 and cannot pass with an
  unresolved Critical or High finding.
- `ProductDecision` blocks autonomous behavior change until the user resolves it.

## Remote acceptance

Required PR-context checks pass, unavailable reviews remain reported as
unavailable, GraphQL has no actionable unresolved thread, and the merge follows
the explicitly approved delivery/bypass boundary. Self-invalidating terminal
facts use at most one causal evidence-only closeout.
