# Datenschutzleitlinie / Privacy Guideline

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

## Zweck / Purpose

**DE:** Diese Leitlinie beschreibt, wie Datenschutz in Ausbildungs- und Level-2-Projekten beruecksichtigt wird. Sie ist generisch und ersetzt keine Rechtsberatung.

**EN:** This guideline describes how privacy is considered in training and level-2 projects. It is generic and does not replace legal advice.

## Grundregeln / Core Rules

- **DE:** Personenbezogene Daten nur verarbeiten, wenn Zweck, Datenarten und Aufbewahrung klar sind.
- **EN:** Process personal data only when purpose, data types, and retention are clear.
- **DE:** Testdaten anonymisieren oder synthetisch erzeugen, wenn echte Daten nicht erforderlich sind.
- **EN:** Anonymize test data or create synthetic data when real data is not required.
- **DE:** Logs duerfen keine sensiblen Daten, Tokens oder vollstaendige personenbezogene Datensaetze enthalten.
- **EN:** Logs must not contain sensitive data, tokens, or full personal data records.
- **DE:** Datenschutz-Folgenabschaetzung pruefen, wenn hohe Risiken fuer Personen entstehen koennen.
- **EN:** Check for a data protection impact assessment when high risks for people may arise.

## Nachweise / Evidence

**DE:** Geeignete Nachweise sind Datenflussnotiz, DPIA-Checkliste, Testdatenkonzept, Logging-Review oder `N/A`-Begruendung.

**EN:** Suitable evidence includes data-flow notes, DPIA checklist, test-data concept, logging review, or `N/A` rationale.

## Praktischer Ablauf / Practical Workflow

1. Erstelle ein kleines Dateninventar: Datenart, betroffene Person, Zweck, Quelle, Empfänger und Aufbewahrung.
2. Prüfe Datenminimierung, Zugriff, Löschung und sichere Voreinstellungen bereits in Spezifikation und Design.
3. Verwende synthetische Testdaten. Reale Daten benötigen eine dokumentierte Ausnahme und zusätzlichen Schutz.
4. Prüfe Logs, Exporte, Backups, Telemetrie und KI-Prompts auf personenbezogene Daten.
5. Nutze `CL-11` für die Schwellenwertprüfung einer Datenschutz-Folgenabschätzung. Rechtliche Unsicherheit wird an eine zuständige Datenschutzrolle eskaliert.

*Create a small data inventory; check minimisation, access, deletion, and privacy-friendly defaults during specification and design; use synthetic test data; review logs, exports, backups, telemetry, and AI prompts; and use `CL-11` for the DPIA threshold review. Escalate legal uncertainty to a responsible privacy role.*

## Betroffenenrechte und Aufbewahrung / Data Subject Rights and Retention

Wenn das Projekt personenbezogene Daten speichert, muss es erklären, wie Auskunft, Berichtigung, Löschung und Export technisch unterstützt oder an eine zuständige Stelle übergeben werden. Aufbewahrungsfristen haben einen Zweck und einen Löschweg; „für immer“ ist kein sicherer Standard. / If the project stores personal data, it must explain how access, correction, deletion, and export are technically supported or handed to a responsible role. Retention periods need a purpose and a deletion path; “forever” is not a safe default.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Datenschutz bedeutet nicht nur „keine Daten verlieren". Es bedeutet auch: Nur nötige personenbezogene Daten verarbeiten, Testdaten bewusst wählen, Logs sauber halten und Betroffene nicht unnötig gefährden.

**EN:** Privacy does not only mean "do not lose data". It also means: process only needed personal data, choose test data carefully, keep logs clean, and avoid unnecessary risk for people.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, sobald personenbezogene Daten, Benutzerkonten, Logs mit Personenbezug oder produktionsnahe Testdaten vorkommen.
- **EN:** `Applicable` as soon as personal data, user accounts, person-related logs, or production-like test data are involved.
- **DE:** `N/A` braucht eine kurze Aussage, warum keine personenbezogenen Daten verarbeitet werden.
- **EN:** `N/A` needs a short statement why no personal data is processed.

## Preset-Bezug / Preset Alignment

- `security-governance`: Standards Applicability, ASVS, Regulatory Applicability.
- `architecture-governance`: Trust Boundaries, Threat Model.
- `a11y-governance`: Verstaendliche, barrierearme Datenschutzhinweise.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
