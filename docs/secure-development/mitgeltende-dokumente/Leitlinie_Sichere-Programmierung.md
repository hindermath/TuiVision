# Leitlinie fuer sichere Programmierung / Secure Programming Guideline

**Stand / Date:** 2026-06-19

## Zweck / Purpose

**DE:** Diese Leitlinie fasst sichere Programmierregeln zusammen, die unabhaengig von einer konkreten Programmiersprache gelten. Sprachspezifische Details stehen in den Secure-Coding-Profilen des Security-Governance-Presets.

**EN:** This guideline summarizes secure programming rules that apply across languages. Language-specific details live in the secure-coding profiles of the Security Governance preset.

## Grundregeln / Core Rules

- **DE:** Eingaben an Systemgrenzen validieren und nicht vertrauenswuerdige Daten klar markieren.
- **EN:** Validate input at system boundaries and clearly mark untrusted data.
- **DE:** Fehler kontrolliert behandeln und keine internen Details an Endnutzer*innen ausgeben.
- **EN:** Handle errors in a controlled way and do not expose internals to end users.
- **DE:** Datenbankzugriffe parametrisieren; kein dynamisches SQL aus Benutzereingaben.
- **EN:** Parameterize database access; no dynamic SQL from user input.
- **DE:** Pfade, Prozesse, Netzwerkziele und Dateien nur nach expliziter Pruefung verwenden.
- **EN:** Use paths, processes, network targets, and files only after explicit checks.
- **DE:** Geheimnisse in Secret Stores halten, nicht im Quellcode.
- **EN:** Keep secrets in secret stores, not in source code.
- **DE:** Abhaengigkeiten aktiv pflegen und kritische Schwachstellen vor Freigaben behandeln.
- **EN:** Keep dependencies maintained and address critical vulnerabilities before releases.

## Memory-Safe Languages / Memory-Safe Languages

**DE:** Memory-Safe Languages reduzieren bestimmte Fehlerklassen, ersetzen aber keine sichere API-, I/O-, Auth-, Crypto-, Logging- oder Dependency-Pruefung. Bei Nicht-MSL-Projekten ist eine technische Begruendung erforderlich.

**EN:** Memory-safe languages reduce some classes of errors, but they do not replace secure API, I/O, auth, crypto, logging, or dependency review. Non-MSL projects need a technical rationale.

## Review-Fragen / Review Questions

| Frage / Question | Erwartung / Expectation |
|---|---|
| Welche Eingaben sind nicht vertrauenswuerdig? / Which inputs are untrusted? | Grenze und Validierung benennen / Name boundary and validation |
| Welche Fehlerpfade gibt es? / Which error paths exist? | Sicherer Zustand und Log-Level benennen / Name safe state and log level |
| Welche Abhaengigkeiten wurden neu eingefuehrt? / Which dependencies were added? | Pflegezustand und CVE-Lage dokumentieren / Document maintenance and CVE state |
| Gibt es Auth-, Rollen- oder Datenschutzbezug? / Is auth, role, or privacy scope involved? | Evidenzpfad oder `N/A` begruenden / Evidence path or `N/A` rationale |

## Preset-Bezug / Preset Alignment

- `security-governance`: Secure Coding, MSL, ASVS, Dependency Audit.
- `a11y-governance`: Nutzerseitige Fehlermeldungen muessen verstaendlich und barrierearm sein.
