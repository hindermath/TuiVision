# Domain Acceptance Checklist: Wave-4 Visual Component Porting

**Purpose**: Verify that the accepted Wave-4 visual, host, historical, safety,
and proof contract is complete before planning.
**Created**: 2026-07-12
**Feature**: [spec.md](../spec.md)

## Example and Visual Contract

- [X] The scope names exactly `Cyrillic`, `ETerm`, `Fonts`, `Terminal`, and `XTerm`.
- [X] Feature 021 is the binding reusable session/mapping/font/profile/view baseline.
- [X] Every example requires main composition, dynamic status, and keyboard-reachable description.
- [X] Every primary smoke requires app-loop, concrete state, exact view, and rendered-cell proof.

## Domain Boundaries

- [X] `Terminal` forbids process, shell, PTY, and host mutation while proving accepted/rejected recovery.
- [X] `Cyrillic` covers direct, replacement, invalid, and unsupported host-independent mapping.
- [X] `Fonts` requires exact fixture validation, recognizable 8x16 raster, and invalid fallback.
- [X] `ETerm` and `XTerm` use immutable representative manifests and explicitly reject general legacy-parser claims.

## Evidence and Safety

- [X] Deterministic, remote, and physical host evidence classes remain separate; unavailable physical proof is `NotRun`.
- [X] Relevant implementations, headers, configs, resources, scripts, and fixtures under `tv203s/` are read-only evidence.
- [X] Every example receives exactly one accepted framework decision with follow-up boundaries.
- [X] Assets are controlled/read-only and arbitrary user data plus persistent writes are excluded.

## A11Y, Governance, and Delivery

- [X] Keyboard access, text-first status/fallback, non-color-only state, and bilingual CEFR-B2 guidance are acceptance criteria.
- [X] All six preset applicability paths and complete governance-row fields are explicit.
- [X] Statistics, Pflichtenheft, five agent surfaces, guides, indexes, archive, and generated-output hygiene are included.
- [X] Remote tasks require exact evidence paths and one pre-named causal closeout path.

## Notes

- Review pass 1: 16/16 complete.
- No domain ambiguity remains that would materially change planning or validation.
