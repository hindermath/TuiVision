# Richtlinie Changemanagement / Change Management Policy

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

## Zweck / Purpose

**DE:** Diese Richtlinie beschreibt, wie Aenderungen sicher geplant, geprueft, umgesetzt und nachvollzogen werden. Sie gilt fuer Code, Konfiguration, Dokumentation, CI/CD und Sicherheitsnachweise.

**EN:** This policy describes how changes are planned, reviewed, implemented, and traced safely. It applies to code, configuration, documentation, CI/CD, and security evidence.

## Regeln / Rules

- **DE:** Jede fachliche Aenderung hat einen nachvollziehbaren Anlass, Scope und Akzeptanzkriterien.
- **EN:** Each functional change has a traceable reason, scope, and acceptance criteria.
- **DE:** Sicherheitsrelevante Aenderungen benoetigen Review mit passender Checkliste.
- **EN:** Security-relevant changes need review with the appropriate checklist.
- **DE:** Aenderungen an gemeinsamen Agenten-Regeln werden atomar in allen gepflegten Agenten-Dateien nachgezogen.
- **EN:** Changes to shared agent rules are applied atomically to all maintained agent files.
- **DE:** Produktive oder veroeffentlichte Artefakte werden nur nach erfolgreicher Pruefung freigegeben.
- **EN:** Production or published artifacts are released only after successful checks.

## Mindestnachweise / Minimum Evidence

| Nachweis / Evidence | Erwartung / Expectation |
|---|---|
| Commit | Aussagekraeftige Commit-Message und begrenzter Scope / Clear commit message and bounded scope |
| Review | Sicherheits-, A11Y- oder Architekturbezug sichtbar / Security, A11Y, or architecture relevance visible |
| Tests | Passende Tests oder begruendetes `N/A` / Suitable tests or justified `N/A` |
| Nachlauf | Offene Punkte und Restrisiken dokumentiert / Open items and residual risks documented |

## Änderungsklassen und Ablauf / Change Classes and Workflow

| Klasse / Class | Beispiel / Example | Mindestweg / Minimum Path |
|---|---|---|
| Standard | Dokumentation oder risikoarme, wiederholbare Wartung / Documentation or low-risk repeatable maintenance | Scope, automatisierte Prüfungen, Review bei betroffenen Regeln |
| Normal | Feature, Dependency, Konfiguration oder Pipeline / Feature, dependency, configuration, or pipeline | Risikoanalyse, Testplan, Peer Review, Rückfallweg |
| Kritisch | Auth, Crypto, Rechte, personenbezogene Daten, Build-/Release-Vertrauen / Auth, crypto, permissions, personal data, build/release trust | Security-Review, Vier-Augen-Freigabe, expliziter Rollback, Nachkontrolle |
| Notfall | Sofortige Begrenzung eines aktiven Risikos / Immediate containment of an active risk | Begründete Abkürzung, Protokoll, nachträglicher Review innerhalb festgelegter Frist |

Jede Änderung durchläuft Intake, Auswirkungs- und Risikoprüfung, Umsetzung, Test, Review, Freigabe und Nachkontrolle. Der Rückfallweg wird vor der Freigabe geprüft oder als `N/A` begründet. / Every change passes intake, impact and risk assessment, implementation, test, review, approval, and follow-up. The rollback path is checked before release or justified as `N/A`.

## Trennung und Schutz / Separation and Protection

Wer eine kritische Änderung erstellt, darf sie nicht allein als sicher freigeben. Branch-Schutz, signierte oder nachvollziehbare Commits, begrenzte CI-Rechte und unveränderte Audit-Logs unterstützen diese Trennung. / The author of a critical change must not approve it alone as secure. Branch protection, signed or traceable commits, limited CI permissions, and protected audit logs support separation.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Changemanagement bedeutet: Eine Änderung soll nicht zufällig passieren. Ziel, Risiko, Review, Test und Ergebnis müssen nachvollziehbar sein. Das schützt vor Fehlern und hilft später beim Audit.

**EN:** Change management means: a change should not happen by accident. Goal, risk, review, test, and result must be traceable. This prevents errors and helps later in audits.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, sobald Code, Konfiguration, Dokumentation, Pipeline, Agentenregeln oder Sicherheitsnachweise geändert werden.
- **EN:** `Applicable` when code, configuration, documentation, pipeline, agent rules, or security evidence changes.
- **DE:** Typische Evidenz: Commit, Pull Request, Review-Kommentar, Testausgabe, Freigabe- oder Folgeaufgaben-Notiz.
- **EN:** Typical evidence: commit, pull request, review comment, test output, approval or follow-up note.

## Preset-Bezug / Preset Alignment

- `agent-parity-governance`: gemeinsame Guidance synchron halten.
- `cross-platform-governance`: Skriptaenderungen fuer Bash und PowerShell gemeinsam pruefen.
- `security-governance`: Sicherheitsrelevanz und Evidenzpflicht klaeren.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
