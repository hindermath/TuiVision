# BCM-/Notfallhandbuch / BCM and Emergency Handbook

**Stand / Date:** 2026-06-19

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

## Preset-Bezug / Preset Alignment

- `architecture-governance`: Resilienz, Cloud-Autonomie, C3A/C5, Zero Trust.
- `isaqb-architecture-governance`: Deployment View, Runtime View, Qualitaetsszenarien.
- `security-governance`: Supply Chain Evidence, SLSA, Dependency Audit.
