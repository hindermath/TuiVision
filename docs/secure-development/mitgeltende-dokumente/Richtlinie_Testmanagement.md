# Richtlinie Testmanagement / Test Management Policy

**Stand / Date:** 2026-06-19

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
