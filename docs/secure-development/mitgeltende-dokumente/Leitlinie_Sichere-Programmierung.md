# Leitlinie fuer sichere Programmierung / Secure Programming Guideline

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

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

**DE:** Swift ist eine MSL. Das ist besonders relevant für iOS-, watchOS- und macOS-Projekte. Swift-Code muss trotzdem auf Force-Unwraps bei nicht vertrauenswürdigen Daten, validierte Eingaben, sichere Dateizugriffe, Keychain/CryptoKit-Nutzung und Dependency-Risiken geprüft werden.

**EN:** Swift is an MSL. This is especially relevant for iOS, watchOS, and macOS projects. Swift code still needs review for force unwraps on untrusted data, validated input, safe file access, Keychain/CryptoKit use, and dependency risks.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Eine sichere Sprache hilft, aber sie entscheidet nicht alles. Auch in C#, Java, Python, Go, Swift oder Rust kann unsicherer Code entstehen, wenn Eingaben nicht geprüft, Fehler falsch behandelt oder Secrets falsch gespeichert werden.

**EN:** A safe language helps, but it does not decide everything. C#, Java, Python, Go, Swift, or Rust code can still be unsafe when input is not checked, errors are mishandled, or secrets are stored incorrectly.

## Sprachprofile / Language Profiles

| Sprache / Language | Mindestprüfung / Minimum Review |
|---|---|
| C# / .NET | Parametrisierte Queries, Output-Encoding, Anti-Forgery, sichere Deserialisierung, Cancellation und Secret Stores. |
| Rust | `unsafe` klein halten und begründen, untrusted Input ohne Panic behandeln, Deserialisierung begrenzen, `cargo audit` oder gleichwertig nutzen. |
| Go | Timeouts und `context` durchreichen, SSRF prüfen, `crypto/rand` nutzen, Fehler nicht verschlucken, `govulncheck` oder gleichwertig nutzen. |
| Swift | Keine Force-Unwraps für untrusted Input, `Codable`-Daten validieren, Keychain/CryptoKit und App Sandbox nutzen, File-/URL-Scope begrenzen, Concurrency prüfen. |
| Java / Kotlin | DTOs validieren, Persistence parametrisieren, Deserialisierung begrenzen, Auth/CSRF/CORS/Session-Defaults prüfen. |
| Python | Eingaben an Grenzen validieren, kein unsicheres `pickle`/`eval`, Prozesse und Pfade begrenzen, Dependencies auditieren. |
| TypeScript / JavaScript | Runtime-Schemata validieren, XSS, Prototype Pollution und SSRF prüfen, kein dynamisches `eval`, Lockfiles auditieren. |

*The same profiles apply in English: validate boundaries and runtime input, use parameterised data access, safe platform crypto and secret storage, constrained file/network/process access, maintained dependencies, and explicit error handling. Language-specific tools do not replace human review.*

## Nicht-MSL-Projekte / Non-MSL Projects

C, C++, klassisches Objective-C, Assembly, `cc65`-C89, Zig vor 1.0 und andere nicht freigegebene Primärsprachen benötigen eine dokumentierte Plattformbegründung, zusätzliche Speicher- und Grenztests sowie einen Migrations- oder Risikobehandlungsweg. / C, C++, classic Objective-C, assembly, `cc65` C89, pre-1.0 Zig, and other non-approved primary languages need a documented platform rationale, additional memory and boundary tests, and a migration or risk-treatment path.

## Review-Gate / Review Gate

Für jede nicht-triviale Änderung werden betroffene CL-IDs, Sprache/Framework, untrusted Inputs, privilegierte I/O-Pfade, neue Dependencies, Tests und Restrisiken genannt. `N/A` ist nur mit technischer Begründung zulässig. / For each non-trivial change, record affected CL IDs, language/framework, untrusted input, privileged I/O paths, new dependencies, tests, and residual risk. `N/A` requires a technical rationale.

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


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
