# Richtlinie Secure Development Life Cycle / Secure Development Life Cycle Policy

**Stand / Date:** 2026-07-19
**Version / Version:** 1.2.0
**Baseline-Version / Baseline version:** 3.2.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

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
- **DE:** Die acht Governance-Presets werden als pruefbarer Rahmen verwendet, wenn sie installiert oder projektseitig verbindlich sind.
- **EN:** The eight governance presets are used as a review framework when installed or binding for the project.
- **DE:** Die Verzahnungsdatei ordnet SDLC-Prüfbereiche den mitgeltenden Dokumenten, Checklisten und Presets zu.
- **EN:** The alignment file maps SDLC review areas to related documents, checklists, and presets.

## Rollen und Gates / Roles and Gates

| Gate | Verantwortliche Rollen / Responsible Roles | Mindestentscheidung / Minimum Decision |
|---|---|---|
| Intake bereit / Intake ready | Auftraggebende oder Projektverantwortung, Lernbegleitung | Scope, Nicht-Ziele, Lernstufe, Standards und Risiken sind verständlich. / Scope, non-goals, learning stage, standards, and risks are understandable. |
| Design bereit / Design ready | Entwicklung und Architektur-/Security-Review | Trust Boundaries, Bedrohungen, Daten, Abhängigkeiten und Teststrategie sind behandelt. / Boundaries, threats, data, dependencies, and test strategy are addressed. |
| Umsetzung bereit / Implementation ready | Entwicklung und Reviewer | Aufgaben enthalten Secure Coding, Tests, A11Y und Evidenzpfade. / Tasks include secure coding, tests, accessibility, and evidence paths. |
| Freigabe bereit / Release ready | Projektverantwortung und unabhängige Review-Rolle | Befunde, Restrisiken, Ausnahmen und Folgeaufgaben sind entschieden. / Findings, residual risks, exceptions, and follow-ups are decided. |

Eine Person darf in kleinen Lernprojekten mehrere Rollen ausfüllen. Kritische Eigenfreigaben werden durch eine zweite Person geprüft. / One person may hold several roles in small training projects. Critical self-approvals receive a second-person review.

## Ausnahmen und Aufbewahrung / Exceptions and Retention

Eine Ausnahme nennt Regel, Grund, Risiko, Ersatzmaßnahme, Owner, Ablaufdatum und erneuten Prüfzeitpunkt. Nachweise bleiben mindestens für die Lebensdauer des bewerteten Releases oder entsprechend einer projektspezifischen Aufbewahrungsentscheidung erhalten. / An exception names the rule, reason, risk, compensating control, owner, expiry date, and review date. Evidence is retained at least for the lifetime of the assessed release or according to a project-specific retention decision.

## Nachweise / Evidence

**DE:** Geeignete Nachweise sind `spec.md`, `plan.md`, `tasks.md`, Checklisten, Testprotokolle, S-ADRs, Dependency-Audits, Threat Models und `docs/security/`-Artefakte.

**EN:** Suitable evidence includes `spec.md`, `plan.md`, `tasks.md`, checklists, test records, S-ADRs, dependency audits, threat models, and `docs/security/` artifacts.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** SDLC ist der Ablauf von der Idee bis zur Freigabe. Sicherer SDLC heißt: Sicherheit wird nicht erst am Ende geprüft, sondern in Ziel, Plan, Aufgaben, Umsetzung, Test und Freigabe sichtbar dokumentiert.

**EN:** SDLC is the path from idea to release. Secure SDLC means: security is not only checked at the end, but visibly documented in goal, plan, tasks, implementation, test, and release.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable` für jeden nicht-trivialen Spec-Kit-Lauf, Härtungslauf, Release oder sicherheitsrelevanten Review.
- **EN:** `Applicable` for every non-trivial Spec Kit run, hardening run, release, or security-relevant review.
- **DE:** `N/A` ist nur möglich, wenn es keine Entwicklungs-, Prüf- oder Freigabeaktivität gibt.
- **EN:** `N/A` is only possible when there is no development, review, or release activity.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
| 1.1.0 | 2026-07-17 | Spec-Kit-Rahmen auf das verbindliche Siebenerprofil einschließlich `autonomous-run-governance` erweitert; synchron mit sichere-Entwicklung-Basis 3.1.0. / Extended the Spec Kit framework to the binding seven-preset profile including `autonomous-run-governance`; synchronized with secure-development baseline 3.1.0. |
| 1.2.0 | 2026-07-19 | Spec-Kit-Rahmen auf das verbindliche Achterprofil einschließlich `parallel-autonomous-run-governance` erweitert; synchron mit sichere-Entwicklung-Basis 3.2.0. / Extended the Spec Kit framework to the binding eight-preset profile including `parallel-autonomous-run-governance`; synchronized with secure-development baseline 3.2.0. |
