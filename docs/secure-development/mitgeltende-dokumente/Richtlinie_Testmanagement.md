# Richtlinie Testmanagement / Test Management Policy

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

## Zweck / Purpose

**DE:** Diese Richtlinie beschreibt Mindestanforderungen an Tests in sicheren Entwicklungsprojekten. Tests dienen nicht nur der Funktion, sondern auch der Sicherheit, Nachvollziehbarkeit und Barrierefreiheit.

**EN:** This policy describes minimum requirements for tests in secure development projects. Tests cover not only function, but also security, traceability, and accessibility.

## Testarten / Test Types

| Testart / Test Type | Erwartung / Expectation |
|---|---|
| Unit Tests | Kernlogik, Grenzen und Fehlerfaelle pruefen. / Check core logic, boundaries, and error cases. |
| Integration Tests | Schnittstellen, Datenfluss und Konfiguration pruefen. / Check interfaces, data flow, and configuration. |
| Security Tests | Eingaben, Auth, Rechte, Crypto, Logs und Dependencies pruefen. / Check input, auth, permissions, crypto, logs, and dependencies. |
| A11Y Tests | CLI, UI, Dokumentation und Templates auf Nutzbarkeit pruefen. / Check CLI, UI, documentation, and templates for usability. |
| Regression Tests | Bereits behobene Fehler gegen Rueckfall schuetzen. / Protect fixed defects against regression. |

## Dokumentationsregeln / Documentation Rules

- **DE:** Fehlende Tests brauchen eine kurze technische Begruendung.
- **EN:** Missing tests need a short technical rationale.
- **DE:** Kritische Pfade brauchen mindestens eine nachvollziehbare Pruefung oder eine dokumentierte Folgeaufgabe.
- **EN:** Critical paths need at least one traceable check or a documented follow-up task.
- **DE:** Testausgaben sollen textorientiert nutzbar sein.
- **EN:** Test output should remain usable in text-oriented environments.

## Testplanung / Test Planning

Jede nicht-triviale Änderung ordnet Anforderungen und Risiken mindestens einem Test, Review oder begründeten `N/A` zu. Der Testplan nennt Umgebung, Testdaten, Vorbedingungen, erwartetes Ergebnis, Owner und Evidenzpfad. / Each non-trivial change maps requirements and risks to at least one test, review, or justified `N/A`. The test plan names environment, test data, preconditions, expected result, owner, and evidence path.

## Sicherheits- und Freigabegates / Security and Release Gates

- Kritische Eingabe-, Auth-, Rechte-, Crypto-, Datei- und Netzwerkpfade enthalten Negativ- und Grenztests.
- Dependency-, Secret- und statische Scans sind versioniert und reproduzierbar oder als `N/A` begründet.
- Fehlgeschlagene Pflichtprüfungen blockieren die Freigabe. Eine Ausnahme braucht Owner, Ablaufdatum, Ersatzmaßnahme und Restrisiko.
- Coverage ist ein Hinweis, kein Sicherheitsbeweis. Kritische Pfade werden anhand des Risikos bewertet.
- Testdaten enthalten standardmäßig keine produktiven personenbezogenen Daten oder echten Secrets.

*Critical paths need negative and boundary tests. Required dependency, secret, and static checks are reproducible. Failed gates block release unless a time-limited, owned exception with compensating control is approved. Coverage is an indicator, not proof. Test data excludes real personal data and secrets by default.*

## Defekte und Regression / Defects and Regression

Sicherheitsbefunde erhalten Schweregrad, Reproduktionsweg, Owner, Zieltermin und Freigabeentscheidung. Behobene kritische Fehler erhalten einen Regressionstest, wenn dieser technisch sinnvoll ist. / Security findings receive severity, reproduction steps, owner, due date, and release decision. Fixed critical defects receive a regression test when technically useful.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Tests zeigen nicht nur, dass etwas funktioniert. Sie zeigen auch, dass typische Fehler, Grenzfälle und Sicherheitsrisiken bedacht wurden. Ein fehlender Test ist erlaubt, aber nur mit guter Begründung.

**EN:** Tests do not only show that something works. They also show that typical errors, boundaries, and security risks were considered. A missing test is allowed, but only with a good rationale.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** `Applicable`, wenn Logik, Schnittstellen, UI, CLI, Build, Security-Verhalten oder Dokumentationsausgabe geändert werden.
- **EN:** `Applicable` when logic, interfaces, UI, CLI, build, security behavior, or documentation output changes.
- **DE:** Typische Evidenz: Testliste, CI-Lauf, Coverage-Bericht, Security-Test, A11Y-Smoke-Test, begründetes `N/A`.
- **EN:** Typical evidence: test list, CI run, coverage report, security test, A11Y smoke test, justified `N/A`.

## Preset-Bezug / Preset Alignment

- `security-governance`: Security Checklist, ASVS, Dependency Audit.
- `a11y-governance`: WCAG 2.2 AA, CLI-A11Y, CEFR B2.
- `cross-platform-governance`: Testparitaet fuer Plattformen und Skriptvarianten.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
