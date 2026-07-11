# OWASP-SAMM-Kurzassessment / Lightweight SAMM Assessment

**Stand / Current as of**: 2026-07-11
**Scope**: TuiVision repository and delivery workflow

Dieses Assessment ist eine priorisierte Reifegradhilfe, keine Zertifizierung.

*This assessment is a prioritized maturity aid, not a certification.*

| Business Function | Aktuelle Praxis / Current practice | Ergebnis / Result | Nächster Schritt / Next step |
|---|---|---|---|
| Governance | Constitution, six presets, 157-control matrix, PR evidence | Established baseline | Human owners review `Open` rows periodically |
| Design | Threat model, arc42 concepts, quality scenarios, bounded S-ADR trigger | Established baseline | Update on new trust boundary |
| Implementation | MSL C#, secure-coding profiles, tests, didactic review | Established baseline | Continue per-change security review |
| Verification | MSTest, coverage gate, secret scans, package review, DocFX/A11Y | Established baseline | Add provider-backed scans only with owner approval |
| Operations | Public source/release workflow, disclosure policy | Partial | Formal response ownership and release provenance remain open/follow-up |

## Priorisierte Verbesserungen / Prioritized Improvements

1. Complete repository-local SBOM, immutable Actions, Dependabot, and rename
   contract hardening in feature 016.
2. Establish release provenance/reproducibility in a dedicated release feature.
3. Obtain human decisions for disclosure ownership, agent sandbox evidence, and
   CRA market-placement role before making operational or legal claims.

Review at least when a release pipeline, provider boundary, product service,
security incident, or material architecture changes.
