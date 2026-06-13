# Contract: Didactic Inline Code Comment Hardening Acceptance

**Feature**: `015-didactic-comment-hardening`
**Date**: 2026-06-14

This contract defines the observable review, comment, evidence, guidance, governance, and validation obligations for the didactic inline-code-comment hardening run.

## 1. Common Scope Contract

The feature MAY touch:

- didactic inline, block, file, or module comments;
- reviewed source files under the scoped TuiVision modules;
- relevant smoke-test helper and proof-path files;
- `specs/015-didactic-comment-hardening/pr-evidence.md`;
- shared guidance surfaces only when project-wide comment guidance changes;
- learner-facing guides only when changed wording or API/docs triggers require them.

The feature MUST NOT:

- change runtime behavior, public behavior, API signatures, dependencies, example scope, or broad framework structure;
- start Wave-1 visual remediation, Wave 3, Wave 4, or new example porting;
- add scripts, services, databases, network flows, release artifacts, cloud dependencies, or runtime/product AI;
- modify `tv203s/`;
- treat every method or obvious line as requiring a comment.

## 2. Primary Evidence Contract

Implementation MUST create and maintain:

```text
specs/015-didactic-comment-hardening/pr-evidence.md
```

Each evidence entry MUST record:

- reviewed file or named flow area;
- hotspot category;
- primary decision;
- rationale;
- comment need;
- changed or unchanged comment state;
- change summary;
- validation or proof boundary;
- follow-up boundary where applicable;
- governance, DocFX/A11Y, statistics, or agent-guidance trigger where applicable.

Guides, code comments, and PR discussion may summarize evidence, but they do not replace the feature evidence ledger.

## 3. Hotspot Coverage Contract

The evidence ledger MUST cover or explicitly rationalize all required hotspot categories:

| Hotspot category | Required review focus |
|---|---|
| Event/command/dispatch | Non-trivial event routing, command handling, dispatch branching, or historical message-routing deviation |
| Focus transition | Focus changes, selection transfer, view activation, and keyboard path consequences |
| View hierarchy | Parent/child relationships, traversal, ownership, insertion/removal, or visible composition boundaries |
| StatusLine | Dynamic feedback, command/status linkage, or hidden state exposed to the user |
| Help/Description | Help topic, description reachability, fallback text, and learner explanation path |
| Dialog state | Dialog lifecycle, modal state, command result, or state restoration |
| Validation/Rejection | Invalid input, rejected command, guard clause, or safe failure path |
| Buffer/Cell proof | Rendered buffer/cell inspection and proof limits |
| Rendering snapshot | Snapshot stability, expected region, and non-overstated visual proof |
| Terminal fallback | Capability limits, unsupported terminal path, or platform fallback |
| Historical deviation | Turbo Vision difference that explains modern code or proof |
| Smoke-test helper | Helper purpose, stability reason, setup/supplemental role, and proof boundary |

## 4. Comment Decision Contract

Each reviewed area MUST receive exactly one primary decision:

- `CommentAdequate`
- `CommentNeeded`
- `NoCommentNeeded`
- `UpdateExistingComment`
- `FollowUpHardening`

No additional primary decision values are allowed.

`CommentNeeded` MUST produce a concise didactic comment unless a later same-feature review proves the existing wording or code shape is already adequate.

`UpdateExistingComment` MUST correct, replace, or remove stale, misleading, overly broad, or trivial comments.

`NoCommentNeeded` MUST record why a comment would only repeat clear code.

`FollowUpHardening` MUST record the real issue, why it is outside this feature, and which later work item or evidence boundary should carry it.

## 5. Didactic Comment Style Contract

New or changed didactic comments MUST:

- explain why, trade-off, constraint, historical deviation, or proof boundary;
- avoid restating adjacent identifiers, operators, assertions, assignments, or obvious control flow;
- normally stay within 1 to 3 lines;
- include an evidence rationale when longer wording is needed;
- be German-first/English-second and approximately CEFR-B2 when they are didactic explanation blocks;
- leave technical license, generated-file, tool-owned, and marker lines unchanged.

## 6. Smoke Helper and Proof Boundary Contract

Reviewed smoke helpers MUST make proof purpose, stability reason, and proof boundary understandable when the helper name and assertions are not enough.

Evidence or code-near comments MUST clarify non-obvious:

- app-loop, event, command, key, focus, or dialog proof;
- view-tree, buffer, cell, rendering snapshot, or terminal fallback proof;
- setup-only, supplemental, legacy, or temporary helper role;
- environment or capability limits that affect proof claims.

Helpers that only prepare state or support assertions MUST NOT be described as complete behavior proof.

## 7. Historical Deviation Contract

Historical Turbo Vision references are read-only context. The feature MUST review historical deviations only where they clarify a modern implementation, proof boundary, or intentional difference.

When a historical deviation is relevant, evidence MUST record:

- historical reference or behavior;
- modern TuiVision area;
- why the difference matters for comprehension;
- whether explanation belongs in a comment, evidence, guide, or follow-up.

Runtime parity fixes are out of scope.

## 8. DocFX, Documentation, and A11Y Contract

Pure `//` or `/* */` comment hardening that does not change XML comments, API signatures, generated documentation, navigation, or guides MUST NOT require DocFX.

If XML comments, public API signatures, generated API documentation, documentation navigation, or learner-facing guides change, completion evidence MUST include:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Changed Markdown evidence, guidance, and guides MUST remain text-first and usable in screen-reader, Braille, and text-browser contexts. Learner-facing updates MUST be German-first/English-second and around CEFR-B2.

## 9. Agent Guidance Contract

If project-wide comment guidance changes, implementation MUST update together:

- `AGENTS.md`
- `CLAUDE.md`
- `GEMINI.md`
- `.github/copilot-instructions.md`
- `.github/agents/copilot-instructions.md`

Any intentional divergence MUST be explicit in `pr-evidence.md`. `.specify/templates/` stay `N/A` unless repository-owned templates are intentionally changed.

If shared guidance is not changed, `pr-evidence.md` MUST record the unchanged rationale.

## 10. Governance Contract

Implementation MUST record:

- NIST SSDF and CWE Top 25 as Level-2 secure-development context;
- ASVS `N/A` unless web/API/HTTP/auth scope changes;
- SBOM/VEX/SLSA/OpenSSF Scorecard unchanged unless dependency, release, provenance, or public OSS risk posture changes;
- AI-SBOM `N/A` unless runtime/product AI, models, datasets, AI infrastructure, or delivered AI components enter scope;
- NIS2/CRA/EU AI Act/DORA `N/A` unless regulated scope triggers change;
- STRIDE/CIA/CAPEC, S-ADR, arc42 security concepts, Zero Trust, SAMM, BSI C3A, and BSI C5 `N/A` unless architecture, trust boundary, cloud, provider, or deployment topology changes;
- cross-platform script requirements `N/A` unless script-shaped tools are added or changed.

## 11. Validation Contract

Final implementation evidence MUST include at least:

```bash
git diff --check
dotnet format --verify-no-changes
```

When source or test-helper files are changed, evidence MUST include matching targeted tests. When shared logic or broad smoke-helper proof is changed, evidence MUST include:

```bash
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

Before build or test commands, commits, or pushes on the numbered branch, `Directory.Build.props` MUST be aligned to `1.15.<patch>.<build>`. The manual build counter MUST be incremented before build or test commands.

If a validation command cannot run locally, the reason and equivalent CI/manual evidence MUST be recorded in `pr-evidence.md`.

## Deutsch / English

Deutsch: Dieser Vertrag beschreibt, woran die spaetere Umsetzung gemessen wird: Hotspot-Abdeckung, genau eine Review-Entscheidung je Bereich, moderate didaktische Kommentare, klare Proof-Grenzen, Governance-Rationale und passende Validierung.

English: This contract describes how the later implementation will be judged: hotspot coverage, exactly one review decision per area, moderate didactic comments, clear proof boundaries, governance rationale, and matching validation.
