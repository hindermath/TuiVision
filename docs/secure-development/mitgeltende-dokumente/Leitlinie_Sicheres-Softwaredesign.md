# Leitlinie fuer sicheres Softwaredesign / Secure Software Design Guideline

**Stand / Date:** 2026-06-19

## Zweck / Purpose

**DE:** Diese Leitlinie beschreibt sichere Design- und Architekturprinzipien fuer Ausbildungs- und Level-2-Projekte. Sie verbindet sichere Programmierung mit sicheren Architekturentscheidungen.

**EN:** This guideline describes secure design and architecture principles for training and level-2 projects. It connects secure programming with secure architecture decisions.

## Designprinzipien / Design Principles

| Prinzip / Principle | Bedeutung / Meaning |
|---|---|
| Trust Boundaries | Grenzen zwischen vertrauenswuerdigen und nicht vertrauenswuerdigen Bereichen benennen. / Name boundaries between trusted and untrusted areas. |
| Defense in Depth | Mehrere unabhaengige Schutzschichten fuer wichtige Werte nutzen. / Use multiple independent layers for important assets. |
| Least Privilege | Komponenten erhalten nur noetige Rechte. / Components receive only needed permissions. |
| Fail-Safe Defaults | Standard ist sicher; Zugriff wird explizit erlaubt. / Default is safe; access is allowed explicitly. |
| Separation of Concerns | Auth, Logging, Validierung und Fehlerbehandlung nicht ad hoc verstreuen. / Do not scatter auth, logging, validation, and error handling ad hoc. |
| Secure Configuration | Unsichere Defaults, Debug-Endpunkte und harte Secrets vermeiden. / Avoid unsafe defaults, debug endpoints, and hard-coded secrets. |

## Architekturentscheidungen / Architecture Decisions

- **DE:** Sicherheitsrelevante Designentscheidungen werden als ADR oder S-ADR dokumentiert.
- **EN:** Security-relevant design decisions are documented as ADR or S-ADR.
- **DE:** Jede Entscheidung nennt Kontext, Entscheidung, Alternativen, Folgen und Restrisiko.
- **EN:** Each decision names context, decision, alternatives, consequences, and residual risk.

## Preset-Bezug / Preset Alignment

- `architecture-governance`: Secure Architecture, Threat Model, S-ADR, Zero Trust, SAMM, C3A/C5.
- `isaqb-architecture-governance`: Architekturziele, Sichten, Qualitaetsszenarien, Risiken.
