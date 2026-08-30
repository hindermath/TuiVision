# Sicherheitsdokumentation / Security Documentation

**Repository**: TuiVision (Level 2)
**Stand / Current as of**: 2026-08-30
**Owner**: TuiVision Maintainers
**Review**: Features `016-secure-development-hardening` and `044-sandbox-secure-development-hardening`

## Zweck / Purpose

Dieses Verzeichnis enthält die langlebige projektspezifische
Sicherheitsevidenz. Richtlinie und generische Checklisten liegen unter
`docs/secure-development/`; Feature-Befehle und Laufdetails liegen im
jeweiligen `specs/<feature>/pr-evidence.md`.

*This directory contains durable project-specific security evidence. Policy
and generic checklists live under `docs/secure-development/`; feature commands
and run details live in the matching `specs/<feature>/pr-evidence.md`.*

## Dokumente / Documents

| Dokument / Document | Aktueller Zweck / Current purpose | Status |
|---|---|---|
| [control-assessment.md](control-assessment.md) | 157 Kontrollen aus CL-01 bis CL-12 / 157 controls from CL-01 to CL-12 | Current |
| [gsdb-self-assessment.md](gsdb-self-assessment.md) | GSDB- und Preset-Abdeckung / GSDB and preset coverage | Current |
| [threat-model.md](threat-model.md) | STRIDE/CIA/CAPEC und Trust Boundaries | Current |
| [arc42-security.md](arc42-security.md) | Sicherheits-Querschnittskonzepte / Security cross-cutting concepts | Current |
| [security-quality-scenarios.md](security-quality-scenarios.md) | Messbare Sicherheitsszenarien / Measurable security scenarios | Current |
| [security-checklist.md](security-checklist.md) | Verdichtetes Gate / Consolidated gate | Current |
| [dependency-audit.md](dependency-audit.md) | Paket- und CVE-Nachweis / Package and CVE evidence | Current |
| [supply-chain-evidence.md](supply-chain-evidence.md) | SBOM, VEX, SLSA, Scorecard, Actions | Current |
| [asvs-verification.md](asvs-verification.md) | ASVS-Anwendbarkeit / ASVS applicability | Current `N/A` |
| [zero-trust-applicability.md](zero-trust-applicability.md) | Zero-Trust-Anwendbarkeit / Zero Trust applicability | Current `N/A` |
| [samm-assessment.md](samm-assessment.md) | Leichtes Reifegradbild / Lightweight maturity view | Current |
| [cloud-autonomy-applicability.md](cloud-autonomy-applicability.md) | BSI C3A | Current `N/A` |
| [cloud-compliance-assurance.md](cloud-compliance-assurance.md) | BSI C5 | Current `N/A` |
| [regulatory-applicability.md](regulatory-applicability.md) | NIS2, CRA, EU AI Act, DORA, DPIA | Current |
| [adr/README.md](adr/README.md) | Security-ADR-Index und Trigger / Security ADR index and trigger | Current |
| [Sandbox-Anwendbarkeit](secure-development/2026-08-29-sandbox-applicability/README.md) | TuiVision-Mounts, CL-12, Toolchain und Proof-Grenzen / TuiVision mounts, CL-12, toolchain, and proof boundaries | `ConditionallyUsable` |
| [RL-SE- und Checklist-Selbstpruefung](secure-development/2026-08-30-rl-se-checklist-self-review/README.md) | Nicht zertifizierender 157-Kontrollen-Audit mit Preset-, Governance- und Human-Grenzen / Non-certifying 157-control audit with preset, governance, and human boundaries | Validation pending |

## Evidenzregeln / Evidence Rules

- `Applicable` und `AlreadySatisfied` benötigen aktuelle direkte Evidenz.
- `N/A` benötigt Fakten und einen Neubewertungstrigger.
- `Open` ist Human-only und benötigt Owner, Risiko und Aktion.
- `FollowUp` benennt einen konkreten späteren Scope.
- Generierte SBOM-, Scan-, DocFX-, Test- und Coverage-Ausgaben bleiben außerhalb
  von Git.

*Positive states need current direct evidence. Non-applicability needs facts and
a trigger. Open decisions remain human-owned. Follow-ups name a later scope.
Generated security and validation output stays outside Git.*
