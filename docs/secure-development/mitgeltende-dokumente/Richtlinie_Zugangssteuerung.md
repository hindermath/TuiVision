# Richtlinie Zugangssteuerung / Access Control Policy

**Stand / Date:** 2026-06-19

## Zweck / Purpose

**DE:** Diese Richtlinie beschreibt Mindestregeln fuer Identitaeten, Rollen, Berechtigungen und Geheimnisse in Entwicklungsprojekten.

**EN:** This policy describes minimum rules for identities, roles, permissions, and secrets in development projects.

## Grundregeln / Core Rules

- **DE:** Least Privilege gilt fuer Menschen, Dienste, CI/CD, Agenten und lokale Tools.
- **EN:** Least privilege applies to humans, services, CI/CD, agents, and local tools.
- **DE:** Gemeinsame Konten werden vermieden oder mit klarer Begruendung dokumentiert.
- **EN:** Shared accounts are avoided or documented with a clear rationale.
- **DE:** Rechte werden bei Rollenwechsel, Projektende oder Verdacht auf Missbrauch entzogen.
- **EN:** Permissions are removed after role changes, project end, or suspected misuse.
- **DE:** Secrets werden rotiert, wenn sie offengelegt oder unsicher behandelt wurden.
- **EN:** Secrets are rotated when exposed or handled unsafely.

## Review-Fragen / Review Questions

| Frage / Question | Erwartung / Expectation |
|---|---|
| Wer braucht Zugriff? / Who needs access? | Rolle und Zweck benennen / Name role and purpose |
| Welche Rechte sind minimal noetig? / Which rights are minimally needed? | Least-Privilege-Begruendung / Least-privilege rationale |
| Wo liegen Secrets? / Where are secrets stored? | Secret Store oder `N/A` / Secret store or `N/A` |
| Wie wird Zugriff protokolliert? / How is access logged? | Audit-Log oder Begruendung / Audit log or rationale |

## Preset-Bezug / Preset Alignment

- `security-governance`: Auth, Authorization, Secret Handling.
- `architecture-governance`: Trust Boundaries, Secure Configuration.
