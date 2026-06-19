# Richtlinie Changemanagement / Change Management Policy

**Stand / Date:** 2026-06-19

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

## Preset-Bezug / Preset Alignment

- `agent-parity-governance`: gemeinsame Guidance synchron halten.
- `cross-platform-governance`: Skriptaenderungen fuer Bash und PowerShell gemeinsam pruefen.
- `security-governance`: Sicherheitsrelevanz und Evidenzpflicht klaeren.
