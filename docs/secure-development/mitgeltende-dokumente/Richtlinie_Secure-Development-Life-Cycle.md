# Richtlinie Secure Development Life Cycle / Secure Development Life Cycle Policy

**Stand / Date:** 2026-06-19

## Zweck / Purpose

**DE:** Diese Richtlinie beschreibt den sicheren Entwicklungslebenszyklus fuer Ausbildungs- und Level-2-Projekte. Jeder Schritt soll planbar, pruefbar und auditfaehig dokumentiert sein.

**EN:** This policy describes the secure development life cycle for training and level-2 projects. Each step should be planned, reviewable, and audit-ready.

## SDLC-Phasen / SDLC Phases

| Phase | Mindestanforderung / Minimum Requirement |
|---|---|
| Intake | Ziel, Nicht-Ziele, Standards, `Applicable`/`N/A`-Logik und Evidenzpfade klaeren. / Clarify goal, non-goals, standards, `Applicable`/`N/A` logic, and evidence paths. |
| Spezifikation | Sicherheits-, A11Y-, Architektur- und Lieferkettenanforderungen sichtbar aufnehmen. / Include security, A11Y, architecture, and supply-chain requirements visibly. |
| Planung | Risiken, Trust Boundaries, Tests, Rollen und offene Fragen dokumentieren. / Document risks, trust boundaries, tests, roles, and open questions. |
| Umsetzung | Secure Coding, Reviews, Tests und Dependency-Pruefung anwenden. / Apply secure coding, reviews, tests, and dependency checks. |
| Freigabe | Restbefunde, Restrisiken, `N/A`-Begruendungen und Folgeaufgaben dokumentieren. / Document residual findings, residual risks, `N/A` rationales, and follow-up tasks. |
| Betrieb/Nachlauf | Schwachstellen, Updates und Lessons Learned nachhalten. / Track vulnerabilities, updates, and lessons learned. |

## Spec-Kit-Nutzung / Spec Kit Usage

- **DE:** Spec-Kit-Artefakte duerfen Sicherheitsstandards nicht stillschweigend auslassen.
- **EN:** Spec Kit artifacts must not silently omit security standards.
- **DE:** Die sechs Governance-Presets werden als pruefbarer Rahmen verwendet, wenn sie installiert oder projektseitig verbindlich sind.
- **EN:** The six governance presets are used as a review framework when installed or binding for the project.

## Nachweise / Evidence

**DE:** Geeignete Nachweise sind `spec.md`, `plan.md`, `tasks.md`, Checklisten, Testprotokolle, S-ADRs, Dependency-Audits, Threat Models und `docs/security/`-Artefakte.

**EN:** Suitable evidence includes `spec.md`, `plan.md`, `tasks.md`, checklists, test records, S-ADRs, dependency audits, threat models, and `docs/security/` artifacts.
