# Projektweite Sicherheits-Checkliste / Project Security Checklist

**Stand / Current as of**: 2026-07-11
**Detailnachweis / Detailed evidence**: [control-assessment.md](control-assessment.md)

## Gate-Zusammenfassung / Gate Summary

| Gate | Status | Evidenz / Evidence | Neubewertung / Re-evaluation |
|---|---|---|---|
| 157 CL-Kontrollen vollständig / 157 CL controls complete | PASS | `control-assessment.md` | Checklist source changes |
| NIST SSDF und CWE Top 25 | Applicable | Source/test review and feature evidence | Runtime/tool boundary changes |
| Threat Model, arc42, Quality Scenarios | PASS | Files in `docs/security/` | Architecture/trust-boundary changes |
| Critical/High Findings | Required zero at merge | Feature finding ledger | Every security review |
| Input, File, Serialization, Terminal, Output | Applicable | Existing negative tests plus 016 review | New parser/input/I/O path |
| Owned crypto | `N/A` | No project-owned crypto or key management | Crypto/signing/TLS enters scope |
| Web/API/Auth/ASVS | `N/A` | No product web/API/auth surface | Such a surface enters scope |
| SBOM and dependency review | Applicable | CycloneDX and package evidence | Every release/dependency change |
| VEX | `N/A` while no known shipped vulnerability | Supply-chain evidence | Vulnerability finding |
| SLSA/reproducible release | `FollowUp` | Supply-chain evidence | Release pipeline scope |
| Zero Trust, BSI C3A/C5 | `N/A` | Local runtime; no cloud/service/provider product dependency | Cloud/distributed scope |
| AI-SBOM/EU AI Act product scope | `N/A` | AI is development tooling only | Runtime/product AI |
| CRA market placement | Human-only `Open` | Regulatory evidence | Human legal decision |
| NIS2, DORA, DPIA | Current `N/A` | No regulated operation/financial ICT/high-risk personal-data scope | Scope facts change |
| Disclosure policy | Applicable | Root `SECURITY.md` | Reporting ownership/channel changes |
| Agent/sandbox external controls | Human-only `Open` where not repository-provable | Agent files and feature ledger | Platform evidence supplied |
| Generated output and secrets | PASS required | `.gitignore`, secret scans, final status | Every delivery |

## Review-Regel / Review Rule

Ein positives Gate ersetzt nie den detaillierten Kontrollnachweis. `Open` ist
keine technische Erfüllung, und `N/A` gilt nur bis zum genannten Trigger.

*A positive gate never replaces detailed control evidence. `Open` is not
technical completion, and `N/A` lasts only until its trigger.*
