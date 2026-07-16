# Deduplication and Follow-up Checklist: Feature 030

**Purpose**: Verify complete TG/MB decisions and deterministic follow-up intake generation.

- [x] Do new observations start at `MB001`?
- [x] Are all five allowed MB decision terms exact and closed?
- [x] Must every TG and MB observation receive exactly one deduplication outcome?
- [x] Can one TuiVision gap produce at most one canonical CF finding?
- [x] Does every CF finding have exactly one Primary Owner?
- [x] Are reproduction, red proof, real-path green proof, impacts, risk, and dependencies mandatory?
- [x] Must the dependency graph be acyclic and topologically sortable?
- [x] Does `ProductDecision` block autonomous delivery?
- [x] Are empty owner groups forbidden from creating hardening intakes?
- [x] Are hardening intakes numbered from 031 and ordered by owner dependencies?
- [x] Does exactly one independent closure intake follow last?
- [x] Is a zero-finding result defined as Feature 031 closure only?

## Durchführungshinweis / Review Instruction

Build a complete observation-to-outcome map, group only actual CF findings by
one Primary Owner, validate the DAG, then generate non-empty Lastenhefte and
one final closure file from the computed result rather than from assumptions.
