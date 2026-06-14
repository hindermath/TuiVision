# Zero-Trust-Anwendbarkeit / Zero Trust Applicability: [PROJECT]

## Spec-Kit-Laufnachweis / Spec-Kit Run Evidence

- Feature / Spec-ID:
- Spec-Kit-Phase / Spec-Kit phase: [specify / plan / tasks / implement / review / release]
- Branch / Commit / PR:
- Datum des Laufs / Run date:
- Evidenzverantwortliche*r / Evidence owner:
- Reviewende Person / Reviewer:
- Gepruefte Standards/Kriterien / Standards or criteria checked: NIST SP 800-207 Zero Trust, ISO 27001/27002 A.8.27, distributed-system trust boundaries
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
**Scope**: [System / feature / deployment topology]
**Reviewer**: [Name]
**Datum / Date**: [YYYY-MM-DD]
**Constitution-Referenz / Constitution Reference**: Principle XIV, Principle XVIII

<!--
  Dieses Dokument bewertet, ob und wie NIST SP 800-207 fuer das Projekt
  anwendbar ist.

  This document evaluates whether and how NIST SP 800-207 applies to the
  project.
-->

## 1. Projektform / System Shape

| Merkmal / Characteristic | Ja/Nein | Anmerkungen / Notes |
|---|---|---|
| Verteilt / Distributed | [Ja/Nein] | |
| Service-basiert / Service-based | [Ja/Nein] | |
| Cloud-nah / Cloud-near | [Ja/Nein] | |
| Remote-verwaltet / Remotely managed | [Ja/Nein] | |
| Identitaetsfoederation / Federated identity | [Ja/Nein] | |
| Mehrgeraete-Zugriff / Multi-device access | [Ja/Nein] | |

## 2. Applicability Decision

- Ergebnis / Result: [Applicable / Partially Applicable / N/A]
- Begründung / Rationale:
  - [ ]

## 3. Relevante Zero-Trust-Kontrollen / Relevant Zero Trust Controls

| Bereich / Area | Kontrolle / Control | Evidenz / Evidence | Status |
|---|---|---|---|
| Identity | [AuthN/AuthZ decision before access] | [Link] | [OK/Open/N/A] |
| Policy | [Central policy decision/enforcement] | [Link] | |
| Device / Workload | [Context-aware checks] | [Link] | |
| Logging / Audit | [Observable decisions] | [Link] | |
| Network | [No implicit trust by location] | [Link] | |

## 4. Offene Punkte / Open Findings

| ID | Finding | Risiko / Risk | Aktion / Action | Owner | Ziel / Target |
|---|---|---|---|---|---|
| ZT001 | | | | | |

## Referenzen / References

- NIST SP 800-207: Zero Trust Architecture
- Constitution Principle XIV: Secure Development Standards & Applicability Matrix
- Constitution Principle XVIII: Zero Trust Applicability & Security Program Maturity

<!-- EN: zero-trust-applicability-template.md
[DE-Zusammenfassung: Template fuer die Bewertung, ob und wie Zero-Trust-Prinzipien auf ein Projekt anwendbar sind.]
-->
