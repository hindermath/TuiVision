# Kompetenzprofile und Schulungsplan fuer sichere Entwicklung / Competency Profiles and Training Plan for Secure Development

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

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

## Lernfortschritt nach Lehrjahr / Progress by Training Year

| Stufe / Stage | Lernziel / Learning Goal | Begleitungsgrad / Supervision | Geeigneter Nachweis / Evidence |
|---|---|---|---|
| Lehrjahr 1 | Grenzen erkennen, Eingaben prüfen, Secrets vermeiden, sichere Fehlerwege und einfache Tests erklären. / Recognise boundaries, validate input, avoid secrets, explain safe error paths and basic tests. | Schrittweise Anleitung und Vier-Augen-Review / Step-by-step guidance and four-eyes review | Kleine Änderung mit CL-ID, Test und Reflexion / Small change with CL ID, test, and reflection |
| Lehrjahr 2 | Threat Model, Rollen, Dependencies, Logging und Datenschutz in Planung und Review anwenden. / Apply threat modelling, roles, dependencies, logging, and privacy in planning and review. | Review an Risikopunkten / Review at risk points | S-ADR, Dependency-Audit oder ausgefüllte Checkliste / S-ADR, dependency audit, or completed checklist |
| Lehrjahr 3 | Sicherheitsumfang selbst planen, Evidenz bewerten, Restrisiken begründen und Reviews moderieren. / Plan security scope, assess evidence, justify residual risk, and moderate reviews. | Stichproben und Freigabe durch erfahrene Rolle / Sampling and approval by experienced role | Vollständiger evidenzbasierter Härtungslauf / Complete evidence-based hardening run |

Der ausführliche Ablauf steht im [Lernpfad Lehrjahr 1 bis 3](../Lernpfad_Sichere-Entwicklung_Lehrjahr-1-bis-3.md). Lernende dürfen einen Punkt als `Open` markieren. Sie dürfen fehlende Evidenz nicht als erfüllt darstellen. / The detailed progression is in the [learning path for years 1 to 3](../Lernpfad_Sichere-Entwicklung_Lehrjahr-1-bis-3.md). Learners may mark an item `Open`; they must not present missing evidence as fulfilled.

## Bewertung / Assessment

Bewertet werden Verständnis, sichere Ausführung, Nachweisqualität und Fähigkeit zur Eskalation. Ein Fehler in einer Übung ist Lernstoff; das Verschweigen eines Risikos oder das Erfinden von Evidenz ist nicht akzeptabel. / Assessment covers understanding, safe execution, evidence quality, and escalation ability. A mistake in an exercise is learning material; hiding a risk or inventing evidence is not acceptable.

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


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
