# Governance and Readiness Requirements Checklist

**Purpose**: Review governance applicability, validation triggers, scope protection, and Wave-order requirements
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Governance Applicability

- [x] CHK001 Are all six baseline presets and autonomous-run-governance named at exact installed versions? [Completeness, Spec CR-014]
- [x] CHK002 Are NIST SSDF, CWE Top 25, STRIDE, CIA, CAPEC, A11Y, agent parity, and autonomous-run requirements explicitly applicable? [Coverage, Spec CR-003, CR-007, CR-011, CR-013, CR-015]
- [x] CHK003 Do ASVS, supply-chain, AI-SBOM, regulatory, cloud, BSI C3A, and BSI C5 exclusions carry scope-based re-evaluation triggers? [Completeness, Spec CR-004-CR-010]
- [x] CHK004 Is script-parity applicability conditional on an actual repository-script change? [Clarity, Spec CR-012]
- [x] CHK005 Is operational remote authority kept outside product requirements? [Consistency, Spec CR-016]

## Scope and Stop Boundaries

- [x] CHK006 Are runtime, API, dependency, package, example, Wave, and external-source changes explicitly excluded? [Completeness, Spec FR-028 and Out of Scope]
- [x] CHK007 Are product decisions, provenance failure, licensing conflict, incomplete consumer mapping, and irreparable audit integrity hard stops? [Coverage, Spec FR-022]
- [x] CHK008 Is magiblot/tvision analysis explicitly deferred to Feature 030? [Boundary, Spec FR-005 and FR-026]

## Validation and Evidence

- [x] CHK009 Are all validation families trigger-based and tied to feature evidence? [Completeness, Spec FR-031-FR-036]
- [x] CHK010 Are learner-facing documentation requirements measurable across bilingual, CEFR-B2, text-first, DocFX, Axe, and UTF-8 text-browser paths? [Measurability, Spec FR-032 and SC-010]
- [x] CHK011 Are maintained agent surfaces treated as one synchronized group? [Consistency, Spec FR-033 and CR-013]
- [x] CHK012 Are placeholder, incomplete-checklist, and contradictory-readiness outcomes explicitly rejected? [Acceptance Criteria, Spec FR-037 and SC-013]

## Ordering and Wave State

- [x] CHK013 Is Feature 030 the sole next intake across every maintained status surface? [Consistency, Spec FR-026 and SC-012]
- [x] CHK014 Are Wave 5 and Wave 6 required to remain blocked after Feature 029? [Completeness, Spec FR-027 and SC-012]

## Review Result

All governance, scope, validation, and ordering requirements are complete. No specification change is required.
