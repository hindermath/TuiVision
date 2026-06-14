# ASVS-Verifikation / ASVS Verification: [PROJECT]

## Spec-Kit-Laufnachweis / Spec-Kit Run Evidence

- Feature / Spec-ID:
- Spec-Kit-Phase / Spec-Kit phase: [specify / plan / tasks / implement / review / release]
- Branch / Commit / PR:
- Datum des Laufs / Run date:
- Evidenzverantwortliche*r / Evidence owner:
- Reviewende Person / Reviewer:
- Gepruefte Standards/Kriterien / Standards or criteria checked: OWASP ASVS, OWASP Cheat Sheet Series, NIST SSDF, CWE Top 25 where applicable
- Entscheidung / Decision: [Applicable / N/A / Open]
- Evidenzpfad / Evidence path:
- N/A-Begruendung, falls nicht anwendbar / N/A rationale, if not applicable:
- Offene Folgeaktion mit Owner und Ausloeser / Open follow-up owner and trigger:
- Ausloeser fuer Neubewertung / Re-evaluation trigger:
- Belastbarkeitsnotiz / Assurance note: Diese Datei dokumentiert den konkreten Spec-Kit-Lauf als interne Audit- und Zertifizierungsvorbereitung. Sie ersetzt keine externe Auditierung, Rechtsberatung oder formale Zertifizierung.

## Audit-Evidenzmatrix / Audit Evidence Matrix

| Pruefpunkt / Checkpoint | Anwendbarkeit / Applicability | Erzeugte oder verlinkte Evidenz / Evidence produced or linked | Ergebnis / Result | Restrisiko oder Begruendung / Residual risk or rationale |
|---|---|---|---|---|
| Spec-Kit-Lauf und Scope sind identifiziert / Spec-Kit run and scope are identified | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Standard- oder Kriterienbezug ist zugeordnet / Standard or criteria mapping is recorded | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Evidenzpfad ist dokumentiert / Evidence path is documented | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Nichtanwendbarkeit ist begruendet / Non-applicability is justified | [Applicable / N/A / Open] | | [OK / Open / N/A] | |
| Offene Punkte haben Owner und Ausloeser / Open findings have owner and trigger | [Applicable / N/A / Open] | | [OK / Open / N/A] | |

**Projekt / Project**: [Level-2 project name]
**Scope**: [System / feature / endpoint set]
**Reviewer**: [Name]
**Datum / Date**: [YYYY-MM-DD]
**Gewaehltes Level / Selected Level**: [ASVS L1 / L2 / L3 / N/A]
**Constitution-Referenz / Constitution Reference**: Principle XIV, Principle XV

<!--
  Dieses Dokument haelt fest, welches OWASP-ASVS-Level fuer das Projekt oder
  Feature gilt, welche Kapitel im Scope sind und welche Kontrollen bereits
  erfuellt, offen oder bewusst ausgenommen sind.

  This document records the OWASP ASVS level selected for the project or
  feature, which chapters are in scope, and which controls are already
  satisfied, still open, or explicitly out of scope.
-->

## 1. Geltungsbereich / Scope

- Projekttyp / Project type: [Web / API / MVC / SPA / Worker+HTTP / N/A]
- In Scope:
  - [ ]
- Out of Scope:
  - [ ]
- Begründung bei `N/A` / Rationale if `N/A`:
  - [ ]

## 2. Level-Wahl / Level Selection

| Option | Gilt / Applies | Begründung / Rationale |
|---|---|---|
| ASVS L1 | [Ja/Nein] | |
| ASVS L2 | [Ja/Nein] | |
| ASVS L3 | [Ja/Nein] | |
| N/A | [Ja/Nein] | |

## 3. Kapitel im Scope / Chapters in Scope

| ASVS-Kapitel / ASVS Chapter | Im Scope / In Scope | Evidenz / Evidence | Status |
|---|---|---|---|
| V1 Architecture, Design and Threat Modeling | [Ja/Nein] | [Link] | [OK/Open/N/A] |
| V2 Authentication | [Ja/Nein] | [Link] | |
| V3 Session Management | [Ja/Nein] | [Link] | |
| V4 Access Control | [Ja/Nein] | [Link] | |
| V5 Validation, Sanitization and Encoding | [Ja/Nein] | [Link] | |
| V6 Stored Cryptography | [Ja/Nein] | [Link] | |
| V7 Error Handling and Logging | [Ja/Nein] | [Link] | |
| V8 Data Protection | [Ja/Nein] | [Link] | |
| V9 Communications | [Ja/Nein] | [Link] | |
| V10 Malicious Code | [Ja/Nein] | [Link] | |
| V11 Business Logic | [Ja/Nein] | [Link] | |
| V12 Files and Resources | [Ja/Nein] | [Link] | |
| V13 API and Web Service | [Ja/Nein] | [Link] | |
| V14 Configuration | [Ja/Nein] | [Link] | |

## 4. Offene Punkte / Open Findings

| ID | ASVS-Referenz / ASVS Reference | Finding | Risiko / Risk | Aktion / Action | Owner | Ziel / Target |
|---|---|---|---|---|---|---|
| A001 | | | | | | |

## 5. Evidenz / Evidence

- Threat Model: [Link]
- Security Checklist: [Link]
- arc42 Security: [Link]
- S-ADR(s): [Link]
- Tests / Scans: [Link]

## Referenzen / References

- OWASP ASVS: [owasp.org/www-project-application-security-verification-standard](https://owasp.org/www-project-application-security-verification-standard/)
- Constitution Principle XIV: Secure Development Standards & Applicability Matrix
- Constitution Principle XV: Secure SDLC & Verification Standards

<!-- EN: asvs-verification-template.md
[DE-Zusammenfassung: Template fuer die Auswahl und Verifikation des passenden OWASP-ASVS-Levels mit Scope, Kapitelabdeckung, Findings und Evidenz.]
-->
