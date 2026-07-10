# BCM-/Notfallhandbuch / BCM and Emergency Handbook

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

## Zweck / Purpose

**DE:** Dieses Dokument beschreibt, wie Entwicklungsprojekte auf Ausfaelle, Verlust von Diensten und Wiederanlauf vorbereitet werden. Es ist eine kompakte Ausbildungsgrundlage fuer Business Continuity Management (BCM).

**EN:** This document describes how development projects prepare for outages, service loss, and recovery. It is a compact training baseline for business continuity management (BCM).

## Kritische Abhaengigkeiten / Critical Dependencies

| Abhaengigkeit / Dependency | Prueffrage / Review Question |
|---|---|
| Quellcode-Repository | Gibt es Remote, Backup und Wiederherstellungsweg? / Is there a remote, backup, and recovery path? |
| CI/CD | Kann nach Ausfall reproduzierbar gebaut werden? / Can builds be reproduced after an outage? |
| Paketregistries | Gibt es Lockfiles, Mirrors oder Cache-Strategien? / Are there lockfiles, mirrors, or cache strategies? |
| Secrets | Koennen Secrets rotiert und wiederhergestellt werden? / Can secrets be rotated and restored? |
| Dokumentation | Sind Richtlinien, Runbooks und Nachweise versioniert? / Are policies, runbooks, and evidence versioned? |

## Mindestmassnahmen / Minimum Measures

- **DE:** Wichtige Artefakte werden versioniert oder wiederherstellbar gehalten.
- **EN:** Important artifacts are versioned or recoverable.
- **DE:** Wiederanlaufwege werden bei kritischen Projekten kurz dokumentiert.
- **EN:** Recovery paths are briefly documented for critical projects.
- **DE:** Ausfaelle von Cloud-, Provider- oder Tool-Abhaengigkeiten werden als Risiko bewertet.
- **EN:** Outages of cloud, provider, or tool dependencies are assessed as risk.

## Vorbereitung und Wiederanlauf / Preparation and Recovery

1. **Schutzbedarf bestimmen / Determine criticality:** Benenne Dienste, Daten und Werkzeuge, deren Ausfall Lernen, Entwicklung oder Betrieb stoppt.
2. **Ziele festlegen / Set objectives:** Dokumentiere eine begründete Wiederanlaufzeit (RTO) und einen höchstens tolerierbaren Datenverlust (RPO).
3. **Verantwortung klären / Assign responsibility:** Benenne Alarmierung, technische Wiederherstellung, Kommunikation und Freigabe.
4. **Reihenfolge dokumentieren / Document sequence:** Stelle zuerst Identitäten und Secrets, dann Repository, Build, Tests und Verteilung wieder her.
5. **Üben / Exercise:** Prüfe mindestens jährlich und nach wesentlichen Änderungen einen Restore oder einen nachvollziehbaren Tabletop-Test.

## Notfallprotokoll / Incident Record

Ein Ereignisprotokoll nennt Startzeit, Auslöser, betroffene Werte, Entscheidungen, Wiederanlaufschritte, RTO-/RPO-Ergebnis, Restrisiken und Lessons Learned. Lernprojekte dürfen vereinfachte Werte verwenden, müssen diese aber begründen. / An incident record names start time, trigger, affected assets, decisions, recovery steps, RTO/RPO result, residual risks, and lessons learned. Training projects may use simplified values, but must justify them.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** BCM bedeutet: Ein Projekt soll nach einem Ausfall wieder arbeitsfähig werden. Dazu muss klar sein, wo Code, Build-Anleitung, Tests, Secrets und wichtige Dokumente liegen und wie sie wiederhergestellt werden.

**EN:** BCM means: a project should become usable again after an outage. The team must know where code, build instructions, tests, secrets, and important documents are stored and how to restore them.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, wenn das Projekt gebaut, verteilt, betrieben oder als Lernbasis dauerhaft genutzt wird.
- **EN:** `Applicable` when the project is built, distributed, operated, or used as a long-lived learning baseline.
- **DE:** Typische Evidenz: Repository-Remote, CI-Konfiguration, Backup-/Restore-Notiz, Runbook, Dependency-Lockfile, Provider-Risiko.
- **EN:** Typical evidence: repository remote, CI configuration, backup/restore note, runbook, dependency lockfile, provider risk.
- **DE:** `N/A` ist nur plausibel bei sehr kleinen Wegwerf-Experimenten ohne dauerhafte Nutzung.
- **EN:** `N/A` is only plausible for very small throw-away experiments without long-term use.

## Preset-Bezug / Preset Alignment

- `architecture-governance`: Resilienz, Cloud-Autonomie, C3A/C5, Zero Trust.
- `isaqb-architecture-governance`: Deployment View, Runtime View, Qualitaetsszenarien.
- `security-governance`: Supply Chain Evidence, SLSA, Dependency Audit.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
