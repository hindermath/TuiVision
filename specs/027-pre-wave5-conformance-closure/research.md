# Research: Pre-Wave-5 Conformance Closure

## Decision 1: Reuse the Feature-024 validator unchanged by default

**Decision**: Treat `ConformanceAuditEvidenceTests.cs` and
`conformance-audit.json` as the canonical machine-verifiable closure input.

**Rationale**: A second model could disagree with the accepted audit. Running
the same strict validator on the merged closure head proves continued validity.

**Alternatives rejected**: copy the JSON into 027; rewrite the validator; rely
only on Markdown counts.

## Decision 2: Compare protected paths, not every post-audit evidence commit

**Decision**: Product drift covers `src/`, `examples/`, project/package/API
surfaces, `tv203s/`, `TVDEMOS/`, and `TVFM/`. Evidence, closeout,
retrospective, intake, agent context, statistics, and version changes are
allowed only when they do not alter those protected contracts.

**Rationale**: Features 024 closeout and retrospective necessarily changed
evidence after the product audit without changing audited behavior.

## Decision 3: Any new finding blocks 027

**Decision**: Even Low or Medium drift/gap stops Wave-5 release and requires a
reviewed audit revision before remediation ownership is reconsidered.

**Rationale**: The accepted 025/026 suppression depends on an exactly empty
finding set; closure cannot silently absorb new work.

## Decision 4: Full gates are mandatory despite no product diff

**Decision**: Run focused, full Release, coverage, DocFX/A11Y, scope, and remote
gates.

**Rationale**: Feature 027 exists specifically to prove integrated readiness on
the merged baseline rather than trust the earlier audit run.

## Decision 5: Home-Baseline helper validation is external evidence

**Decision**: Re-run the fixed PowerShell and Bash homogeneity paths in
`~/home-baseline-tmp` read-only from 027. Do not edit or merge that repository
inside the closure implementation.

**Rationale**: The helper defect was a portable follow-up from 024. Revalidation
tests the autonomous workflow; ownership remains Home Baseline.

## Decision 6: Preset contribution remains evidence-driven

**Decision**: Do not bump or republish the preset merely because 027 runs. Add a
package or upstream update only if current preset commands, templates, or
checklists demonstrably lack a provider-neutral requirement.

**Rationale**: The clean-error-channel rule already exists in v0.1.0; duplicate
wording without a gap is not an improvement.
