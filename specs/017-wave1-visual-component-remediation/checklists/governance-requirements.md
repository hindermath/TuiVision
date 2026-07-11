# Governance And Accessibility Requirements Quality Checklist

**Purpose**: Review six-preset applicability, A11Y, parity, and cross-platform requirement quality  
**Created**: 2026-07-11  
**Audience**: Governance and PR reviewers

## Preset Coverage

- [x] CHK001 Are all six installed presets named with their current versions? [Completeness, Spec Governance Applicability]
- [x] CHK002 Are NIST SSDF and CWE Top 25 mandatory for the Level-2 change? [Security, Spec CR-003]
- [x] CHK003 Is ASVS applicability tied to a concrete web/API/auth trigger? [Clarity, Spec CR-004]
- [x] CHK004 Are SBOM, VEX, SLSA, OpenSSF Scorecard, and supply-chain triggers bounded? [Completeness, Spec CR-005]
- [x] CHK005 Is AI-SBOM `N/A` rationale and re-evaluation trigger defined? [Completeness, Spec CR-006]
- [x] CHK006 Are NIS2, CRA, EU AI Act, and DORA screening requirements explicit? [Regulatory, Spec CR-007]
- [x] CHK007 Are STRIDE, CIA, CAPEC, S-ADR, arc42, Zero Trust, SAMM, BSI C3A, and BSI C5 screened? [Architecture, Spec CR-008]

## Accessibility And Language

- [x] CHK008 Are keyboard-only, screen-reader, Braille, and text-browser needs specified? [A11Y, Spec FR-023]
- [x] CHK009 Are German-first/English-second CEFR-B2 guide requirements explicit? [Localization, Spec FR-021]
- [x] CHK010 Are WCAG 2.2 AA, DocFX, and axe triggers defined? [A11Y, Spec CR-009]
- [x] CHK011 Is essential meaning prohibited from relying only on color, layout, or pointer input? [A11Y, Spec FR-023]

## Platform And Agent Parity

- [x] CHK012 Are macOS, Linux, and Windows/WSL runtime capability differences in scope? [Cross-Platform, Spec CR-010]
- [x] CHK013 Is script-pair/man-page governance correctly trigger-based rather than unconditional? [Scope, Spec CR-010]
- [x] CHK014 Are all five maintained agent guidance surfaces named? [Agent Parity, Spec CR-011]
- [x] CHK015 Is `.specify/templates/` applicability explicitly bounded? [Agent Parity, Spec CR-012]
- [x] CHK016 Are statistics, completion routing, and active-context updates required when affected? [Governance, Spec FR-024]
- [x] CHK017 Must every `N/A` include rationale and re-evaluation trigger? [Acceptance Criteria, Spec SC-011]
- [x] CHK018 Are cloud/provider/distributed-system triggers separated from local terminal UI work? [Architecture, Spec CR-008]

## Review Result

All items passed. No governance clarification or specification remediation
remains before the plan phase.
