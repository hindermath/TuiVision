# Generischer Leitfaden Sichere Entwicklung / Generic Secure Development Guide

**Stand / Date:** 2026-06-20
**Zielgruppe / Audience:** Fachinformatik-Auszubildende, Entwickler*innen, Reviewer und KI-Agenten in Level-2-Projekten / IT specialist apprentices, developers, reviewers, and AI agents in level-2 projects

## Zweck / Purpose

**DE:** Dieser Ordner stellt eine generische, auditfaehige Grundlage fuer sichere Softwareentwicklung bereit. Er wurde aus dem Projekt `RL_Sichere-Entwicklung` abgeleitet, aber bewusst von konkreten Organisations- und Dokumentenmanagement-Bezuegen geloest. Die fachliche Schaerfe der ISO/IEC-27001-/27002-orientierten sicheren Entwicklung bleibt erhalten.

**EN:** This folder provides a generic, audit-ready baseline for secure software development. It was derived from the `RL_Sichere-Entwicklung` project, but deliberately removes organization-specific and document-management bindings. The ISO/IEC 27001/27002-oriented secure-development rigor is preserved.

## Nutzung / Usage

- **DE:** Nutze die Richtlinie als verbindliche fachliche Orientierung fuer Level-2-Haertungen, Spec-Kit-Laeufe, Code-Reviews und Ausbildungsaufgaben. Nutze die Einzelchecklisten fuer gezielte Pruefungen und den Sammelband fuer eine vollstaendige Projektdurchsicht.
- **EN:** Use the guideline as the binding technical orientation for level-2 hardening, Spec Kit runs, code reviews, and training tasks. Use the individual checklists for focused reviews and the compendium for a full project review.
- **DE:** Jeder pruefbare Punkt wird mit Status, Begruendung, Evidenzpfad und naechster Massnahme dokumentiert. Nicht anwendbare Punkte werden nicht geloescht, sondern als `N/A` mit Begruendung festgehalten.
- **EN:** Every review item records status, rationale, evidence path, and next action. Non-applicable items are not removed; they are marked as `N/A` with a rationale.

## Dokumente / Documents

| Dokument / Document | Zweck / Purpose |
|---|---|
| [Richtlinie_Sichere-Entwicklung.md](Richtlinie_Sichere-Entwicklung.md) | Richtlinienaehnliche Grundlage fuer sichere Entwicklung / Policy-like secure-development baseline |
| [checklisten/](checklisten/) | Zwoelf Einzelchecklisten fuer fokussierte Pruefungen / Twelve individual checklists for focused reviews |
| [Checklistensammelband_Sichere-Entwicklung.md](Checklistensammelband_Sichere-Entwicklung.md) | Zusammengefuehrter Sammelband fuer vollstaendige Reviews / Combined compendium for full reviews |
| [mitgeltende-dokumente/](mitgeltende-dokumente/) | Mitgeltende Leitlinien, Richtlinien und externe Referenzen / Related guidelines, policies, and external references |
| [mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md) | Zentrale Mapping-Datei fuer Richtlinie, Checklisten, mitgeltende Dokumente und Governance-Presets / Central mapping file for guideline, checklists, related documents, and governance presets |

## Spec-Kit-Verzahnung / Spec Kit Alignment

**DE:** Bei einem Spec-Kit-Lauf zuerst die zentrale Verzahnungsdatei lesen. Sie zeigt, welche mitgeltenden Dokumente, Checklisten und Governance-Presets zu einem Pruefbereich gehoeren. Dadurch muessen Auszubildende und Reviewer die Zuordnung nicht neu erfinden. Das Ergebnis des Laufs bleibt trotzdem projektspezifisch: konkrete Evidenz liegt im jeweiligen Feature, Review, Test, Scan oder in `docs/security/`.

**EN:** At the start of a Spec Kit run, read the central alignment file first. It shows which related documents, checklists, and governance presets belong to a review area. Apprentices and reviewers do not need to invent the mapping again. The run result remains project-specific: concrete evidence lives in the feature, review, test, scan, or `docs/security/`.

**DE:** Swift ist in dieser Basis als Memory-Safe Language beruecksichtigt. Das folgt aus der zentralen MSL-Erlaubnisliste und der CISA-Unterlage zu Memory-Safe Roadmaps. Swift-Projekte brauchen trotzdem die Swift-spezifische Secure-Coding-Pruefung.

**EN:** Swift is considered a memory-safe language in this baseline. This follows from the central MSL allow-list and the CISA memory-safe roadmaps document. Swift projects still need Swift-specific secure-coding review.

## Grenzen / Boundaries

**DE:** Diese Dokumente ersetzen keine projektspezifischen Sicherheitsartefakte in `docs/security/`, sondern liefern die wiederverwendbare Pruefgrundlage. Projektspezifische Nachweise bleiben im jeweiligen Projektkontext zu fuehren.

**EN:** These documents do not replace project-specific security artefacts in `docs/security/`; they provide the reusable review baseline. Project-specific evidence remains part of the respective project context.
