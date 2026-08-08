# Research: Wave-6 Combined Delta Closure

## R1 - Authoritative product union

**Decision**: Use PR #101 and #104 as the only product deliveries. Treat #102
and #105 as causal evidence and #103 as prompt/intake metadata.

**Rationale**: A broad baseline-to-main diff would mix delivery closeout,
active-feature context and intake preparation into the product assessment.

## R2 - Audit cardinalities

**Decision**: Validate exactly 24 historical TVFM files, ten functional areas,
ten showcase areas, ten combined contracts and one `Tp7FileManager` entry.

**Rationale**: These are the closed sets independently accepted by Features
035 and 036. A new inferred consumer set would weaken predecessor traceability.

## R3 - Combined-area mapping

**Decision**: Join by behavior and proof responsibility, not by matching ordinal
alone. `W6C-001` and `W6C-009` therefore use the relevant shared shell/layout
showcase rows while preserving one unique primary decision per combined area.

## R4 - Historical source hashing

**Decision**: Preserve the Feature-036 rule: `.PAS` and `.BAT` are hashed after
CRLF-to-LF normalization; `.PAL` and `.TVR` remain byte-exact.

**Rationale**: Text checkout policy must not create false drift, while binary
or opaque resource bytes must not be normalized.

## R5 - Framework duplication threshold

**Decision**: Example-local code is acceptable when it only composes the one
TVFM learning example. It becomes a finding only if it replaces a TuiVision
contract or is demonstrably reusable by an independent consumer.

**Rejected alternative**: Treating every local view/helper as duplication.
That would confuse application composition with framework ownership.

## R6 - Finding threshold

**Decision**: Require reproducible user-, proof-, A11Y-, platform-, safety- or
reuse-relevant impact. Pascal/C# structure or naming differences alone are not
findings.

## R7 - Local causal boundary

**Decision**: LocalImplementation can produce a fully validated
`ReadyForDelivery` candidate. It cannot truthfully claim a merge-dependent
Wave-6 `Closed` or portfolio `Eligible` event.

**Rationale**: This was the accepted initial authority. The resumed run now has
explicit `MergeAndSync` authority, but the reviewed feature head still cannot
turn an intended post-merge transition into a historical fact.

## R8 - Provider gates

**Decision**: Current macOS checks run locally. Existing Linux and Windows
predecessor evidence is recorded as supplemental; current-head provider gates
remain not triggered until remote delivery is authorized.

**Authority update**: Remote delivery is now authorized. Ubuntu and Windows
therefore become mandatory exact-head gates before merge; predecessor evidence
remains supplemental and cannot substitute for the current head.

## R9 - Comparison frameworks

**Decision**: Do not repeat the Free Vision, Terminal.GUI or magiblot/tvision
audits. Consult them only if a new concrete Wave-6 question appears.

**Rationale**: Feature 037 closes the actual 035/036 delta, not the prior
cross-framework audit campaign.

## R10 - Preset learning

**Decision**: Promote only a reproducible provider-neutral autonomous-run
defect. Feature-specific evidence or local authority handling remains
`NoPromotion`.
