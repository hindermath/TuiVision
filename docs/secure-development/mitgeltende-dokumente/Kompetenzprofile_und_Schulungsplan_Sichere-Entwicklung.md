# Kompetenzprofile und Schulungsplan fuer sichere Entwicklung / Competency Profiles and Training Plan for Secure Development

**Stand / Date:** 2026-06-19

## Zweck / Purpose

**DE:** Dieses Dokument beschreibt, welche Sicherheitskompetenzen Fachinformatiker*innen in Ausbildungsprojekten aufbauen sollen. Es verbindet Richtlinie, Checklisten und Spec-Kit-Presets mit konkreten Lernzielen.

**EN:** This document describes the security competencies that IT specialist apprentices should build in training projects. It connects the guideline, checklists, and Spec Kit presets with concrete learning goals.

## Kompetenzprofile / Competency Profiles

| Rolle / Role | Erwartete Kompetenz / Expected Competence |
|---|---|
| Auszubildende*r / Apprentice | Eingaben validieren, sichere APIs nutzen, Tests schreiben, `N/A` begruenden, Risiken melden. / Validate input, use safe APIs, write tests, justify `N/A`, report risks. |
| Entwickler*in / Developer | Secure Coding anwenden, Abhaengigkeiten pruefen, Reviews vorbereiten, Evidenzpfade pflegen. / Apply secure coding, check dependencies, prepare reviews, maintain evidence paths. |
| Reviewer | Sicherheitsrelevante Aenderungen erkennen, Checklisten anwenden, offene Risiken klar dokumentieren. / Detect security-relevant changes, apply checklists, document open risks clearly. |
| Projektverantwortliche*r / Project Lead | Scope, Akzeptanzkriterien, Restrisiken und Folgeaufgaben steuern. / Manage scope, acceptance criteria, residual risks, and follow-up work. |

## Schulungsplan / Training Plan

| Modul / Module | Inhalt / Content | Nachweis / Evidence |
|---|---|---|
| Grundlagen | CIA, Threat Boundaries, sichere Defaults, `N/A`-Begruendung. / CIA, trust boundaries, secure defaults, `N/A` rationale. | Kurznotiz oder Review-Protokoll / Short note or review record |
| Secure Coding | Sprachprofile, Fehlerbehandlung, I/O, Auth, Crypto, Logging. / Language profiles, error handling, I/O, auth, crypto, logging. | Code-Review mit Checkliste / Code review with checklist |
| SDLC | Spezifikation, Plan, Tasks, Tests, Freigabe, Evidenz. / Specification, plan, tasks, tests, release, evidence. | Spec-Kit-Artefakte / Spec Kit artifacts |
| Supply Chain | Dependencies, SBOM, VEX, SLSA, Scorecard. / Dependencies, SBOM, VEX, SLSA, Scorecard. | Dependency-Audit / Dependency audit |
| A11Y und Dokumentation | DE/EN, CEFR B2, WCAG 2.2 AA, klare Markdown-Struktur. / DE/EN, CEFR B2, WCAG 2.2 AA, clear Markdown structure. | Dokumentationsreview / Documentation review |

## Mindestanforderung / Minimum Requirement

- **DE:** Jeder sicherheitsrelevante Spec-Kit-Lauf benennt, welche Lernziele beruehrt wurden.
- **EN:** Each security-relevant Spec Kit run states which learning goals were touched.
- **DE:** Offene Lernpunkte werden nicht versteckt, sondern als Folgeaufgabe dokumentiert.
- **EN:** Open learning points are not hidden; they are recorded as follow-up tasks.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Dieses Dokument zeigt, was gelernt und geübt werden soll. Ein Review ist nicht nur Kontrolle. Es ist auch Lernnachweis: Welche Sicherheitsentscheidung wurde verstanden, angewendet und dokumentiert?

**EN:** This document shows what should be learned and practiced. A review is not only a control. It is also learning evidence: which security decision was understood, applied, and documented?

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, wenn ein Spec-Kit-Lauf, Code-Review, KI-Agenten-Lauf oder Härtungslauf Lernwirkung hat.
- **EN:** `Applicable` when a Spec Kit run, code review, AI-agent run, or hardening run has learning value.
- **DE:** Typische Evidenz: Lernzielnotiz, Review-Kommentar, Aufgabenliste, kurze Reflexion, Schulungs- oder Pairing-Nachweis.
- **EN:** Typical evidence: learning-goal note, review comment, task list, short reflection, training or pairing evidence.

## Preset-Bezug / Preset Alignment

- `a11y-governance`: Lesbarkeit, DE/EN, WCAG.
- `security-governance`: Secure Coding und Sicherheitsnachweise.
- `agent-parity-governance`: gleiche Regeln fuer alle Agentenflaechen.
