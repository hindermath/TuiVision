# Research: TV203 and Free Vision Conformance Audit

## R1 - Historical authority

**Decision**: Use Borland documentation and `tv203s/` to determine historical
intent. Use current accepted TuiVision contracts to decide whether later change
is allowed. Keep these two questions distinct.

**Rationale**: Historical fidelity and product compatibility are both required;
silently allowing one to replace the other would turn an audit into an
unreviewed breaking change.

**Alternatives considered**: Treat current tests as the only authority; treat
historical source as mechanically normative. Both hide an essential decision.

## R2 - Free Vision snapshot

**Decision**: Read the official FPC repository at commit
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2` from an external sparse checkout.
Record repository, commit, retrieval date, reviewed paths, and file hashes.

**Rationale**: A moving branch would make audit conclusions irreproducible.
Keeping the checkout outside TuiVision protects provenance and scope.

**Alternatives considered**: Vendor `packages/fv/`; use daily generated docs
without source revision. Both weaken provenance or license separation.

## R3 - Free Vision role

**Decision**: Record one secondary relation per contract. Free Vision may
corroborate original intent, corroborate a modernization, diverge from the
original, or be not applicable.

**Rationale**: Free Vision is actively evolved Object Pascal software. Its
choices are useful evidence but cannot redefine Borland behavior.

**Alternatives considered**: A single combined decision field. Rejected because
it would confuse normative and comparative judgments.

## R4 - Contract granularity

**Decision**: One contract represents one independently reviewable observable
responsibility. It may span several files but must have one coherent intent,
decision, proof boundary, and risk statement.

**Rationale**: Method-level rows would create noise; module-level rows would
hide event ordering, focus, rejection, rendering, or persistence gaps.

**Alternatives considered**: One row per method, one row per file, or one row
per capability bucket. Each is either too granular or too coarse.

## R5 - Canonical evidence shape

**Decision**: Add `conformance-audit.json` as the canonical machine-checkable
dataset and keep the required Markdown artifacts as the human review surfaces.

**Rationale**: Structured parsing avoids brittle Markdown-only completeness
logic while preserving text-first evidence for maintainers and apprentices.

**Alternatives considered**: Markdown only; CSV/TSV; SQLite. Markdown-only
parsing is fragile, CSV cannot express relationships cleanly, and SQLite would
be an unreviewable generated/binary state surface.

## R6 - Validation owner

**Decision**: Add `ConformanceAuditEvidenceTests` to
`TuiVision.Drivers.Tests` and add test-only references to the other framework
assemblies.

**Rationale**: This project already owns the M-07 ledger and repository-root
proof. Reflection over the five referenced assemblies provides compiler-backed
public-contract inventory without a new project or package.

**Alternatives considered**: Bash/PowerShell script pair; source regex; a new
test project. The chosen path is more portable and has less maintenance surface.

## R7 - Inventory ownership

**Decision**: Historical rows and current source files each have one primary
domain. Public contracts are separate inventory items. Contracts may reference
several inventory items without changing their unique ownership.

**Rationale**: This prevents both omissions and duplicate counting when modern
types consolidate several historical implementations.

## R8 - Finding creation

**Decision**: Create findings only for `BehavioralDrift` and `EvidenceGap`.
Every finding has one severity and one owner/disposition. Public breaking-change
conflicts stop at `ProductDecision`.

**Rationale**: Intentional modernization and conscious omission need evidence,
not automatic remediation. One owner avoids duplicated cross-domain work.

## R9 - Validation scale

**Decision**: Run the focused audit suite first, then full Release and canonical
coverage because the test-only proof spans all five assemblies. Run DocFX,
Playwright/Axe, and Lynx because statistics and learner-facing evidence change.

**Rationale**: Runtime is unchanged, but a framework-wide completeness claim
needs broad regression and published-evidence validation.

## R10 - Governance and workflow learning

**Decision**: Keep framework findings separate from autonomous-workflow
observations. Promote correctness/evidence-integrity defects after one
reproducible occurrence; require two occurrences for efficiency preferences.

**Rationale**: TuiVision behavior must not leak into a provider-neutral Spec Kit
preset. The intake-preparation PowerShell helper error is therefore a
`PresetFollowUp`, not a conformance finding.
