# Richtlinie Zugangssteuerung / Access Control Policy

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

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

## Identitäts-Lebenszyklus / Identity Lifecycle

- **Joiner:** Person oder Dienst erhält eine individuelle Identität, minimale Rolle, MFA soweit möglich und eine dokumentierte Freigabe.
- **Mover:** Bei Aufgaben- oder Rollenwechsel werden alte Rechte zuerst geprüft und nicht automatisch behalten.
- **Leaver:** Rechte, Tokens, SSH-Schlüssel, Sessions und Gruppenmitgliedschaften werden zeitnah entzogen.
- **Recertification:** Privilegierte und produktionsnahe Rechte werden regelmäßig durch Owner geprüft.

*Joiners receive individual identities, least privilege, MFA where possible, and approval. Movers do not keep old rights automatically. Leavers lose permissions, tokens, keys, sessions, and group memberships promptly. Privileged access is periodically recertified.*

## Technische Identitäten und Notfallzugriff / Service Identities and Emergency Access

Service Accounts haben einen Owner, einen engen Zweck, minimale Scopes, kurzlebige Credentials soweit möglich und Rotation. Break-glass-Zugriff ist getrennt geschützt, wird nur im Notfall verwendet und danach protokolliert, geprüft und rotiert. / Service accounts have an owner, narrow purpose, minimum scopes, short-lived credentials where possible, and rotation. Break-glass access is separately protected, used only in emergencies, then logged, reviewed, and rotated.

## Freigabe- und Entzugsnachweis / Grant and Revocation Evidence

Der Nachweis nennt Identität, Rolle, Ressource, Rechte, Zweck, Genehmigung, Start, Ablauf oder Prüftermin. Secret-Werte selbst gehören niemals in den Nachweis. / Evidence names identity, role, resource, permission, purpose, approval, start, expiry or review date. Secret values themselves never belong in evidence.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Zugangssteuerung legt fest, wer was tun darf. Sichere Entwicklung braucht kleine Rechte, klare Rollen und sauberen Umgang mit Secrets. Ein Token im Repository ist kein kleiner Fehler, sondern ein Sicherheitsvorfall.

**EN:** Access control defines who may do what. Secure development needs small permissions, clear roles, and clean handling of secrets. A token in the repository is not a small mistake; it is a security incident.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, wenn Benutzerkonten, Rollen, CI/CD-Secrets, API-Keys, Agenten-Sandboxen oder Deployment-Rechte betroffen sind.
- **EN:** `Applicable` when user accounts, roles, CI/CD secrets, API keys, agent sandboxes, or deployment permissions are involved.
- **DE:** Typische Evidenz: Berechtigungsmatrix, Secret-Scan, Sandbox-Konfiguration, CI/CD-Secret-Liste, Rotationsnotiz.
- **EN:** Typical evidence: permission matrix, secret scan, sandbox configuration, CI/CD secret list, rotation note.

## Preset-Bezug / Preset Alignment

- `security-governance`: Auth, Authorization, Secret Handling.
- `architecture-governance`: Trust Boundaries, Secure Configuration.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
