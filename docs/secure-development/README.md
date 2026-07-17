# Generischer Leitfaden Sichere Entwicklung / Generic Secure Development Guide

**Stand / Date:** 2026-07-10
**Baseline-Version / Baseline version:** 3.0.0
**Zielgruppe / Audience:** Fachinformatik-Auszubildende, Entwickler*innen, Reviewer und KI-Agenten in Level-2-Projekten / IT specialist apprentices, developers, reviewers, and AI agents in level-2 projects

## Zweck / Purpose

**DE:** Dieser Ordner stellt eine generische, auditfaehige Grundlage fuer sichere Softwareentwicklung bereit. Er ist bewusst ohne konkrete Organisations-, Firmen-, System- oder Dokumentenmanagementsysteme als Voraussetzung formuliert. Die fachliche Schaerfe der ISO/IEC-27001-/27002-orientierten sicheren Entwicklung bleibt erhalten.

**EN:** This folder provides a generic, audit-ready baseline for secure software development. It deliberately avoids assuming a concrete organization, company, system, or document-management system. The ISO/IEC 27001/27002-oriented secure-development rigor is preserved.

## Nutzung / Usage

- **DE:** Nutze die Richtlinie als verbindliche fachliche Orientierung fuer Level-2-Haertungen, Spec-Kit-Laeufe, Code-Reviews und Ausbildungsaufgaben. Nutze die Einzelchecklisten fuer gezielte Pruefungen und den Sammelband fuer eine vollstaendige Projektdurchsicht.
- **EN:** Use the guideline as the binding technical orientation for level-2 hardening, Spec Kit runs, code reviews, and training tasks. Use the individual checklists for focused reviews and the compendium for a full project review.
- **DE:** Jeder Prüfpunkt erhält genau einen Wert je Statusachse: Anwendbarkeit (`Applicable`, `N/A`, `Open`) und Umsetzung (`Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed`). Dazu kommen Begründung, Evidenzpfad, Owner, Restrisiko, Neubewertungs-Trigger und nächste Maßnahme.
- **EN:** Every review item receives exactly one value per status axis: applicability and implementation. It also records rationale, evidence path, owner, residual risk, re-evaluation trigger, and next action.

## Neutralitaetsregel / Neutrality Rule

**DE:** Konkrete Firmen, private URLs, lokale Hostpfade, Provider-Portale, accountgebundene Annahmen, externe Dokumentenmanagement- oder Sicherheitsmanagementsysteme und Plattformregeln duerfen in wiederverwendbaren Dokumenten nicht als Pflichtvoraussetzung erscheinen. Wenn solche Bezuege in einem Projekt relevant sind, werden sie als Beispiel, Kontext, `N/A`, `Open` oder projektspezifische Evidenz klassifiziert.

**EN:** Concrete companies, private URLs, local host paths, provider portals, account-bound assumptions, external document-management or security-management systems, and platform rules must not appear as mandatory prerequisites in reusable documents. If such references are relevant in a project, they are classified as examples, context, `N/A`, `Open`, or project-specific evidence.

## Dokumente / Documents

| Dokument / Document | Zweck / Purpose |
|---|---|
| [Richtlinie_Sichere-Entwicklung.md](Richtlinie_Sichere-Entwicklung.md) | Richtlinienaehnliche Grundlage fuer sichere Entwicklung / Policy-like secure-development baseline |
| [checklisten/](checklisten/) | Zwoelf Einzelchecklisten fuer fokussierte Pruefungen / Twelve individual checklists for focused reviews |
| [Checklistensammelband_Sichere-Entwicklung.md](Checklistensammelband_Sichere-Entwicklung.md) | Zusammengefuehrter Sammelband fuer vollstaendige Reviews / Combined compendium for full reviews |
| [baseline-manifest.json](baseline-manifest.json) | Kanonische Dateiliste, Versionen, Statusmodell und erwartete 157 CL-IDs / Canonical file list, versions, status model, and expected 157 CL IDs |
| [Lernpfad_Sichere-Entwicklung_Lehrjahr-1-bis-3.md](Lernpfad_Sichere-Entwicklung_Lehrjahr-1-bis-3.md) | Sicherheit ab dem ersten Lehrjahr mit wachsender Selbstständigkeit / Security from training year one with increasing independence |
| [mitgeltende-dokumente/](mitgeltende-dokumente/) | Mitgeltende Leitlinien, Richtlinien und externe Referenzen / Related guidelines, policies, and external references |
| [mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](mitgeltende-dokumente/Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md) | Zentrale Mapping-Datei fuer Richtlinie, Checklisten, mitgeltende Dokumente und Governance-Presets / Central mapping file for guideline, checklists, related documents, and governance presets |

## Entwicklungs-Sandbox / Development Sandbox

**DE:** Die mitgeltende [Leitlinie Sichere Entwicklungs-Sandbox](mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md) beschreibt, wie MSL-basierte Level-2-Projekte mit KI-Agenten in einer freigegebenen Sandbox bearbeitet werden koennen. `absdd-image-sandbox` ist das vorgesehene oeffentlichkeitsfaehige Ausbildungsprofil; konkrete Projektnachweise bleiben im jeweiligen Level-2-Repository.

**EN:** The related [Secure Development Sandbox Guideline](mitgeltende-dokumente/Leitlinie_Sichere-Entwicklungs-Sandbox.md) describes how MSL-based level-2 projects can be worked on with AI agents in an approved sandbox. `absdd-image-sandbox` is the intended public-ready training profile; concrete project evidence remains in the respective level-2 repository.

## Spec-Kit-Verzahnung / Spec Kit Alignment

**DE:** Bei einem Spec-Kit-Lauf zuerst die zentrale Verzahnungsdatei lesen. Sie zeigt, welche mitgeltenden Dokumente, Checklisten und Governance-Presets zu einem Pruefbereich gehoeren. Dadurch muessen Auszubildende und Reviewer die Zuordnung nicht neu erfinden. Das Ergebnis des Laufs bleibt trotzdem projektspezifisch: konkrete Evidenz liegt im jeweiligen Feature, Review, Test, Scan oder in `docs/security/`.

**EN:** At the start of a Spec Kit run, read the central alignment file first. It shows which related documents, checklists, and governance presets belong to a review area. Apprentices and reviewers do not need to invent the mapping again. The run result remains project-specific: concrete evidence lives in the feature, review, test, scan, or `docs/security/`.

**DE:** Swift ist in dieser Basis als Memory-Safe Language beruecksichtigt. Das folgt aus der zentralen MSL-Erlaubnisliste und der CISA-Unterlage zu Memory-Safe Roadmaps. Swift-Projekte brauchen trotzdem die Swift-spezifische Secure-Coding-Pruefung.

**EN:** Swift is considered a memory-safe language in this baseline. This follows from the central MSL allow-list and the CISA memory-safe roadmaps document. Swift projects still need Swift-specific secure-coding review.

**DE:** Lastenhefte fuer spaetere Spec-Kit-Laeufe sollen selbst schon auditfaehig vorbereitet sein. Dazu gehoeren Zweck, Ausgangslage, Zielbild, Scope, Nicht-Ziele, Anforderungen, erwartete Artefakte, Akzeptanzkriterien und ein kopierbarer `/speckit-specify`-Prompt. Der Prompt verlangt bei Sicherheits- oder Governance-Punkten beide Statusachsen, Begründung, Evidenzpfad, Owner, Restrisiko und Neubewertungs-Trigger.

**EN:** Requirements documents for later Spec Kit runs should already be prepared in an audit-ready form. They include purpose, context, target state, scope, non-goals, requirements, expected artefacts, acceptance criteria, and a copyable `/speckit-specify` prompt. For security or governance items, the prompt requires both status axes, rationale, evidence path, owner, residual risk, and re-evaluation trigger.

## Quelle und Erzeugung / Source and Generation

**DE:** Die zwölf Dateien unter `checklisten/` sind die kanonische Quelle. Der Sammelband wird mit `bash scripts/build-secure-development-docs.sh` oder der funktional gleichen PowerShell-Variante erzeugt. `--check` beziehungsweise `-Check` prüft in CI, dass Manifest, Versionen, 157 stabile IDs und Sammelband übereinstimmen. Direkte Änderungen am Sammelband sind nicht zulässig.

**EN:** The twelve files under `checklisten/` are canonical. The compendium is generated with the Bash or functionally equivalent PowerShell generator. Check mode verifies that manifest, versions, 157 stable IDs, and compendium match. Direct compendium edits are not allowed.

## Projektnachweise / Project Evidence

**DE:** Diese Dateien sind Vorlagen. Ausgefüllte Nachweise liegen unter `docs/security/secure-development/<datum>-<scope>/`. Eine positive Aussage ist nur belastbar, wenn die genannte Evidenz existiert und zum geprüften Stand gehört. `N/A` ist eine begründete Anwendbarkeitsentscheidung, kein Ersatz für eine fehlende Umsetzung.

**EN:** These files are templates. Completed evidence lives under `docs/security/secure-development/<date>-<scope>/`. A positive statement is reliable only when the cited evidence exists and belongs to the assessed state. `N/A` is a justified applicability decision, not a substitute for missing implementation.

## Grenzen / Boundaries

**DE:** Diese Dokumente ersetzen keine projektspezifischen Sicherheitsartefakte in `docs/security/`, sondern liefern die wiederverwendbare Pruefgrundlage. Projektspezifische Nachweise bleiben im jeweiligen Projektkontext zu fuehren.

**EN:** These documents do not replace project-specific security artefacts in `docs/security/`; they provide the reusable review baseline. Project-specific evidence remains part of the respective project context.
