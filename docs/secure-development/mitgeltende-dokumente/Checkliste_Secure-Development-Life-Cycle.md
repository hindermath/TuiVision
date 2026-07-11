# Checkliste Secure Development Life Cycle / Secure Development Life Cycle Checklist

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

## Nutzung / Usage

**DE:** Diese kompakte Checkliste ergaenzt die zwoelf Hauptchecklisten. Sie ersetzt sie nicht. Sie hilft, einen Spec-Kit-Lauf oder ein Review schnell auf SDLC-Abdeckung zu pruefen.

**EN:** This compact checklist complements the twelve main checklists. It does not replace them. It helps to quickly review SDLC coverage in a Spec Kit run or review.

**Projekt / Project:**
**Scope und Zeitraum / Scope and period:**
**Verantwortliche Person / Owner:**
**Reviewer:**
**Evidenzablage / Evidence folder:** `docs/security/secure-development/<date>-<scope>/`

| ID | Pruefpunkt / Check | Anwendbarkeit / Applicability | Umsetzung / Implementation | Evidenz / Evidence |
|---|---|---|---|---|
| SDLC-01 | Ziel, Nicht-Ziele und Scope sind dokumentiert. / Goal, non-goals, and scope are documented. | `Open` | `Not Assessed` | |
| SDLC-02 | Anwendbare Standards und `N/A`-Begruendungen sind sichtbar. / Applicable standards and `N/A` rationales are visible. | `Open` | `Not Assessed` | |
| SDLC-03 | Trust Boundaries und wichtige Datenfluesse sind benannt. / Trust boundaries and key data flows are named. | `Open` | `Not Assessed` | |
| SDLC-04 | Secure-Coding-Regeln der Zielsprache wurden geprueft. / Target-language secure-coding rules were checked. | `Open` | `Not Assessed` | |
| SDLC-05 | Tests decken Normal-, Fehler- und Grenzfaelle ab. / Tests cover normal, error, and boundary cases. | `Open` | `Not Assessed` | |
| SDLC-06 | Dependencies, Lizenzen und bekannte CVEs wurden bewertet. / Dependencies, licenses, and known CVEs were assessed. | `Open` | `Not Assessed` | |
| SDLC-07 | Secrets, Logs und Konfiguration wurden sicher behandelt. / Secrets, logs, and configuration were handled safely. | `Open` | `Not Assessed` | |
| SDLC-08 | A11Y, DE/EN und CEFR-B2-Anforderungen wurden geprueft. / A11Y, DE/EN, and CEFR-B2 requirements were checked. | `Open` | `Not Assessed` | |
| SDLC-09 | Offene Risiken und Folgeaufgaben sind dokumentiert. / Open risks and follow-up tasks are documented. | `Open` | `Not Assessed` | |
| SDLC-10 | Freigabeentscheidung oder Abbruchgrund ist nachvollziehbar. / Release decision or stop reason is traceable. | `Open` | `Not Assessed` | |

## Statuswerte / Status Values

- `Applicable`: gilt und ist mit Evidenz zu bearbeiten / applies and must be handled with evidence
- `N/A`: gilt nicht und ist begruendet / does not apply and has a rationale
- `Open`: gilt oder ist unklar und braucht Folgearbeit / applies or is unclear and needs follow-up

Die Umsetzungsachse verwendet `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed`. Bei `N/A` bleibt die Umsetzung `Not Assessed`; die Begründung ist trotzdem Pflicht. / The implementation axis uses `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`. For `N/A`, implementation remains `Not Assessed`; a rationale is still mandatory.

## Abschluss / Completion

- **Restrisiken / Residual risks:**
- **Folgeaufgaben mit Owner und Termin / Follow-up actions with owner and due date:**
- **Neubewertungs-Trigger / Re-evaluation trigger:**
- **Entscheidung / Decision:** `Freigabe / Release` | `Freigabe mit Auflagen / Conditional release` | `Stop`

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Diese Checkliste ist der schnelle SDLC-Überblick. Sie zeigt, ob ein Entwicklungsdurchlauf vom Ziel bis zur Freigabe nachvollziehbar ist. Sie ersetzt die zwölf Hauptchecklisten nicht, sondern hilft beim Einstieg.

**EN:** This checklist is the quick SDLC overview. It shows whether a development run is traceable from goal to release. It does not replace the twelve main checklists; it helps with the first pass.

## Bezug zur Verzahnungsdatei / Relation to the Alignment File

**DE:** Wenn ein Punkt unklar ist, nutze [Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md). Dort steht, welche Richtlinie, welche Checkliste und welches Preset die Detailprüfung steuert.

**EN:** If an item is unclear, use [Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md](Verzahnung_Richtlinie_Checklisten_Spec-Kit-Presets.md). It shows which guideline, checklist, and preset controls the detailed review.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
