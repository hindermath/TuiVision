# Research: Didactic Inline Code Comment Hardening

**Feature**: `015-didactic-comment-hardening`
**Spec**: [spec.md](./spec.md)
**Date**: 2026-06-14

## Decision 1: Use selective hotspot review, not a comment quota

**Decision**: Review the hotspot categories required by the spec and record the decision for each reviewed area. Do not attempt a repository-wide "comment every method" pass.

**Rationale**: The feature value is targeted learner and maintainer comprehension. A quota would add noise and conflict with the explicit out-of-scope boundary.

**Alternatives considered**:
- Comment every public or non-empty method. Rejected because XML docs are the API surface and trivial inline prose reduces readability.
- Review only files already touched by prior waves. Rejected because central framework and smoke-helper hotspots may sit outside recent diff history.

## Decision 2: Maintain `pr-evidence.md` as the primary review ledger

**Decision**: Create and maintain `specs/015-didactic-comment-hardening/pr-evidence.md` as the authoritative evidence ledger for reviewed files and named flow areas.

**Rationale**: Evidence must explain why a comment was added, updated, left alone, omitted, or deferred. A single ledger keeps reviewer decisions visible without forcing noisy comments into clear code.

**Alternatives considered**:
- Store decisions only in code comments. Rejected because `NoCommentNeeded` and `FollowUpHardening` need evidence without code churn.
- Store decisions only in PR discussion. Rejected because later Spec-Kit runs need source-controlled traceability.

## Decision 3: Use exactly the approved five decision values

**Decision**: Every reviewed area receives exactly one primary decision: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, or `FollowUpHardening`.

**Rationale**: The fixed vocabulary prevents ambiguous review outcomes and supports later task generation.

**Alternatives considered**:
- Add severity labels. Rejected because task-level prioritization can handle ordering without changing the review model.
- Allow multiple primary decisions per row. Rejected because one area must have one acceptance state; related follow-up details belong in separate fields.

## Decision 4: Treat smoke-test proof boundaries as first-class comment candidates

**Decision**: Review smoke-test helpers and proof paths for non-obvious stability reasons and proof limits, especially app-loop driving, view-tree inspection, buffer/cell proof, rendered snapshots, terminal fallback, setup-only helpers, and supplemental helpers.

**Rationale**: Recent example waves rely on proof helpers that can look like hidden test magic. The implementation must make proof value and proof limits understandable without overstating what a helper proves.

**Alternatives considered**:
- Comment only production framework code. Rejected because smoke helpers are a major acceptance surface for this repository.
- Move all proof explanation into evidence only. Rejected because non-obvious helper logic benefits from local explanation at the code point.

## Decision 5: Keep didactic comment style moderate and bilingual where explanatory

**Decision**: New or updated didactic comments normally stay within 1 to 3 lines and explain why, trade-off, constraint, historical deviation, or proof boundary. Didactic explanation blocks are German-first/English-second at approximately CEFR-B2.

**Rationale**: The project is learner-oriented and bilingual, but code must remain scannable. Short, reason-focused comments preserve both goals.

**Alternatives considered**:
- English-only inline comments. Rejected because learner-facing didactic explanations follow the project DE-first/EN-second rule.
- Long tutorial-style comments in code. Rejected because guides, XML docs, and evidence are better surfaces for extended explanation.

## Decision 6: Keep XML/API/DocFX triggers conditional

**Decision**: Pure `//` or `/* */` comment hardening does not require DocFX regeneration. XML comments, public API signatures, generated API docs, documentation navigation, or learner-facing guides trigger the normal DocFX/A11Y validation path.

**Rationale**: This feature should not make documentation builds mandatory for code-near comments that do not affect generated docs, while still preserving the existing documentation quality gate when generated or learner-facing docs change.

**Alternatives considered**:
- Always run DocFX for any comment change. Rejected because inline/block comments do not feed DocFX and the requirement would create unnecessary validation work.
- Never run DocFX in this feature. Rejected because XML/API/guide changes remain possible triggers.

## Decision 7: Record historical deviations only where they affect comprehension

**Decision**: Review historical Turbo Vision deviations when they explain a modern implementation or proof boundary; do not reopen historical porting or broad framework parity.

**Rationale**: Historical context is useful for learner comprehension, but this feature is comment hardening, not a new port or remediation wave.

**Alternatives considered**:
- Ignore `tv203s/` entirely. Rejected because historical deviations are explicitly in scope when they explain current design.
- Re-port historical behavior discovered during review. Rejected because runtime behavior change is out of scope.

## Decision 8: Use `FollowUpHardening` for real issues outside comment scope

**Decision**: If review finds a real framework, visual remediation, test design, or proof coverage problem, record `FollowUpHardening` with issue, scope boundary, and follow-up destination instead of changing runtime behavior.

**Rationale**: Comment review can uncover implementation gaps. Capturing them prevents loss of information without turning the feature into a hidden framework revision.

**Alternatives considered**:
- Fix discovered problems immediately. Rejected because this would violate no-runtime-change scope.
- Ignore discovered problems. Rejected because future remediation needs a traceable follow-up boundary.

## Decision 9: Synchronize agent guidance only when shared rules change

**Decision**: If project-wide comment guidance changes, update all maintained agent guidance surfaces together. If implementation only adds feature-local evidence and comments, record why shared guidance did not need another update.

**Rationale**: Agent-parity governance prevents process drift, but unconditional agent-file churn is not useful when the shared rule already exists.

**Alternatives considered**:
- Always touch all agent files. Rejected because unchanged shared rules should not create noisy metadata churn.
- Update only `AGENTS.md`. Rejected because the repository requires parity across maintained agent surfaces.

## Decision 10: Keep governance evidence proportional and trigger based

**Decision**: Record NIST SSDF and CWE Top 25 as Level-2 context. Mark ASVS, SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM, NIS2, CRA, EU AI Act, DORA, STRIDE/CIA/CAPEC, S-ADR, arc42 security concepts, Zero Trust, SAMM, BSI C3A, BSI C5, and cross-platform script requirements as `N/A` unless their trigger conditions change. Keep governance applicability separate from comment decisions and record every checkpoint as `Applicable`, `N/A`, or `Open` with preset version, rationale, evidence path, owner, reviewer, review date, result, residual risk, follow-up, and re-evaluation trigger.

**Rationale**: The feature changes comments, evidence, and possible guidance. It does not change dependencies, release artifacts, cloud/provider posture, service boundaries, scripts, or runtime AI. Audit-ready fields make that proportional decision reviewable without creating unrelated security or architecture work.

**Alternatives considered**:
- Skip governance evidence. Rejected because Level-2 Spec-Kit features require explicit applicability decisions.
- Produce new security/architecture documents for a comment-only feature. Rejected because no security or architecture surface changes are planned.

## Decision 11: Validate no behavior reduction with scaled checks

**Decision**: Planning uses artifact validation only. Implementation must run `git diff --check`, targeted tests for touched code/test-helper areas, broader Release tests/coverage when shared behavior or proof helpers are touched, format validation, and conditional DocFX/web-a11y only when documentation triggers apply.

**Rationale**: Comment-only changes should not require unrelated validation, but accepted behavior and proof coverage must not regress.

**Alternatives considered**:
- Run full validation for every one-line comment. Rejected because it is disproportionate.
- Rely only on review. Rejected because comments near code and proof helpers still need evidence that accepted behavior did not regress.

## Entscheidung / Decision Summary

Deutsch: Der Plan macht 015 zu einem gezielten Kommentar-Härtungslauf: zentrale Hotspots werden geprüft, jede Entscheidung wird belegt, notwendige Kommentare erklären Gründe statt Code zu wiederholen, und Scope-Ausweitungen werden als Follow-up dokumentiert.

English: The plan makes 015 a targeted comment-hardening run: central hotspots are reviewed, every decision is evidenced, needed comments explain reasons instead of repeating code, and scope expansions are documented as follow-up.
