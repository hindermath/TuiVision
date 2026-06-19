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

## Preset-Bezug / Preset Alignment

- `security-governance`: Security Checklist, ASVS, Dependency Audit.
- `a11y-governance`: WCAG 2.2 AA, CLI-A11Y, CEFR B2.
- `cross-platform-governance`: Testparitaet fuer Plattformen und Skriptvarianten.
