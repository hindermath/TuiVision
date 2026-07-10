# Leitlinie fuer sicheres Softwaredesign / Secure Software Design Guideline

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

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
| Attack Surface Reduction | Nicht benötigte Endpunkte, Dienste und Rechte entfernen oder abschalten. / Remove or disable unused endpoints, services, and permissions. |
| Supply-Chain Security | Komponenten, Images und Registries verifizieren; Lockfiles und Herkunftsnachweise pflegen. / Verify components, images, and registries; maintain lockfiles and provenance evidence. |

## Architekturentscheidungen / Architecture Decisions

- **DE:** Sicherheitsrelevante Designentscheidungen werden als ADR oder S-ADR dokumentiert.
- **EN:** Security-relevant design decisions are documented as ADR or S-ADR.
- **DE:** Jede Entscheidung nennt Kontext, Entscheidung, Alternativen, Folgen und Restrisiko.
- **EN:** Each decision names context, decision, alternatives, consequences, and residual risk.

## Design-Ablauf / Design Workflow

1. Benenne Werte, Akteure, Datenflüsse und Vertrauensgrenzen.
2. Erstelle ein STRIDE-basiertes Bedrohungsmodell und priorisiere Risiken.
3. Leite Schutzmaßnahmen und Security Quality Scenarios ab.
4. Dokumentiere wichtige Entscheidungen als S-ADR und die Querschnittskonzepte in arc42 Abschnitt 8.
5. Prüfe Deployment, Provider, Datenstandort, Recovery und Lieferkette.
6. Aktualisiere das Modell bei neuen Schnittstellen, Rollen, Datenarten oder Betriebsformen.

*Name assets, actors, flows, and trust boundaries; build a STRIDE threat model; derive controls and security quality scenarios; record decisions in S-ADRs and arc42 section 8; review deployment, providers, recovery, and supply chain; update after material changes.*

## Einfache Lernübung / Starter Exercise

Lernende zeichnen zunächst ein textbasiertes Datenflussbild mit mindestens einem Benutzer, einem Prozess, einem Datenspeicher und einer Vertrauensgrenze. Danach erklären sie eine Bedrohung und zwei unabhängige Schutzmaßnahmen. / Learners first draw a text-based data-flow view with at least one user, process, data store, and trust boundary. They then explain one threat and two independent controls.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Sicheres Design bedeutet: Sicherheit wird vor dem Code mitgedacht. Ein Projekt muss wissen, welche Daten wichtig sind, wo Grenzen liegen, wer Zugriff hat und was bei Fehlern sicher passieren soll.

**EN:** Secure design means: security is considered before code is written. A project must know which data matters, where boundaries are, who has access, and what should safely happen on errors.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, wenn Architektur, Datenfluss, Deployment, Rollen, externe Dienste oder Trust Boundaries geändert werden.
- **EN:** `Applicable` when architecture, data flow, deployment, roles, external services, or trust boundaries change.
- **DE:** Typische Evidenz: S-ADR, Threat Model, arc42-Abschnitt, Architekturdiagramm in Textform, Qualitäts- oder Risikoszenario.
- **EN:** Typical evidence: S-ADR, threat model, arc42 section, text-based architecture diagram, quality or risk scenario.

## Preset-Bezug / Preset Alignment

- `architecture-governance`: Secure Architecture, Threat Model, S-ADR, Zero Trust, SAMM, C3A/C5.
- `isaqb-architecture-governance`: Architekturziele, Sichten, Qualitaetsszenarien, Risiken.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
