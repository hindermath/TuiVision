# Research: Pre-Wave-5 and Wave-6 Conformance Closure

## Decision 1: Keep Feature 024 canonical and add a separate closure dataset

**Decision**: Preserve `conformance-audit.json` as the immutable audit model.
Create `closure-evidence.json` in Feature 028 with reciprocal references to its
findings, resolutions, contracts, sources, and proof methods.

**Rationale**: Rewriting accepted observations would erase the independent
review boundary. A separate closure layer can validate current outcomes without
changing what Revision 2 found.

**Alternatives rejected**: overwrite finding dispositions; copy the entire
audit JSON; keep closure only in prose.

## Decision 2: Validate the closure dataset through a test-only MSTest reader

**Decision**: Add `ConformanceClosureEvidenceTests.cs` to the existing Drivers
test project. Validate exact cardinalities, vocabularies, reciprocal IDs,
source paths, proof references, consumer paths, and malformed-data rejection.

**Rationale**: The existing test project already references all five framework
assemblies and owns conformance-data validation. Test-only code cannot become a
runtime substitute.

**Alternatives rejected**: a new production parser; ad-hoc shell string checks;
a new package or project.

## Decision 3: Reuse complete real-path tests instead of manufacturing duplicates

**Decision**: Map each R-028 slice to existing tests when their combined proof
covers every named production boundary. Add a new behavior test only if review
finds a missing accepted proof, and stop if that absence represents a product
defect rather than measurement weakness.

**Rationale**: Re-executing strong evidence is independent validation. Copying
tests only to create Feature-028 names adds maintenance cost without new proof.

**Alternatives rejected**: one monolithic synthetic app; helper-only closure;
automatic acceptance of each prior test without boundary review.

## Decision 4: Use exact finding and slice sets plus a stable consumer baseline

**Decision**: Require exactly thirteen finding closures, exactly seven
integration slices, and all thirteen Revision-2 consumer groups. Newly
discovered consumer rows may be added only for a real shared-framework
responsibility and must not replace a baseline row.

**Rationale**: Fixed baseline sets make omissions and duplicates observable
while preserving room for a genuine newly discovered shared flow.

## Decision 5: Permanently add Windows to the existing CI runtime matrix

**Decision**: Extend `.github/workflows/ci.yml` from Ubuntu/macOS to
Ubuntu/macOS/Windows. Do not create a one-off proof branch or infer runtime
acceptance from `Repository Tooling (windows-2022)`.

**Rationale**: Feature-026 evidence already proved the same CI body passes on
`windows-latest`. The recurring missing-command defect is now blocked by
autonomous-run-governance v0.1.4; the smallest project correction is to execute
the accepted command on the reviewed PR head.

**Alternatives rejected**: tooling-job inference; a supplemental head after
merge; a second Windows-only workflow; WSL claims without an available runner.

## Decision 6: Keep WSL as an honest evidence boundary

**Decision**: Windows runtime is mandatory. WSL-specific host behavior is
recorded as `N/A` or residual risk unless an actual WSL runner and command are
available; no Windows success is relabeled as WSL proof.

**Rationale**: The requirement asks for relevant Windows/WSL evidence, not an
unsupported equivalence claim.

## Decision 7: Keep coverage local plus exact-head runtime remote

**Decision**: Run the canonical per-project Coverlet collection locally after
all executable edits, record five assembly values, and rely on exact-head
Ubuntu/macOS/Windows CI for full runtime re-execution. Do not add a second
coverage workflow in this evidence-only feature.

**Rationale**: The local collector already yields separate reports and the
remote full-suite matrix catches platform drift. A new reporting pipeline is a
larger automation feature not required to close the accepted contracts.

**Re-evaluation trigger**: A future policy requires immutable remote coverage
artifacts or the local/remote product trees diverge.

## Decision 8: Separate closure truth from delivery truth

**Decision**: Commit finding, slice, consumer, governance, and local-validation
facts in the feature PR. Keep exact reviewed-head, review, merge, deletion, and
main-sync facts in one post-merge `delivery-closeout.md`.

**Rationale**: Committing current-head facts would create a new head and make
them false. The named closeout avoids recursive self-documentation.

## Decision 9: Close only the pre-Terminal.GUI gate

**Decision**: A full pass yields `ReadyForTerminalGuiAudit`; both Waves remain
`BlockedPendingTerminalGuiAudit` and Feature 029 is next.

**Rationale**: The user explicitly inserted a third-source audit after 028.
Earlier wording that released Wave 5 is superseded but remains historical.

## Decision 10: Promote preset learning only after deterministic reproduction

**Decision**: Run the retrospective after merge. A provider-neutral gap gets a
Home-Baseline patch release and exact tag-ZIP adoption before Feature 029;
otherwise record `NoPromotion` without publication churn.

**Rationale**: Patch-per-finding keeps field learning available to the next run
while the no-empty rule prevents cosmetic releases.
