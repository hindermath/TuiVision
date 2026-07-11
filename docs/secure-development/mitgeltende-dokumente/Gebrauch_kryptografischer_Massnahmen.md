# Gebrauch kryptografischer Massnahmen / Use of Cryptographic Measures

**Stand / Date:** 2026-07-10
**Version / Version:** 1.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Verantwortliche Rolle / Responsible role:** Projekt- oder Ausbildungsverantwortung mit Security-Review / Project or training owner with security review
**Review-Zyklus / Review cycle:** jährlich und bei wesentlichen Änderungen / annually and after material changes

## Zweck / Purpose

**DE:** Dieses Dokument legt Mindestregeln fuer den sicheren Gebrauch von Kryptografie in Ausbildungs- und Level-2-Projekten fest. Es ersetzt keine Detailberatung, verhindert aber typische Fehlentscheidungen.

**EN:** This document defines minimum rules for safe use of cryptography in training and level-2 projects. It does not replace expert advice, but it prevents common mistakes.

## Mindestregeln / Minimum Rules

| Thema / Topic | Vorgabe / Requirement |
|---|---|
| Algorithmen | Moderne, allgemein empfohlene Verfahren verwenden; keine Eigenentwicklung von Kryptografie. / Use modern, widely recommended algorithms; do not invent cryptography. |
| Hashing | SHA-256 oder staerker fuer Integritaet; Passwoerter nur mit Passwort-Hashing wie Argon2id, bcrypt oder PBKDF2. / SHA-256 or stronger for integrity; passwords only with password hashing such as Argon2id, bcrypt, or PBKDF2. |
| Verschluesselung | Authenticated Encryption bevorzugen, zum Beispiel AES-GCM oder ChaCha20-Poly1305. / Prefer authenticated encryption, for example AES-GCM or ChaCha20-Poly1305. |
| Zufall | Kryptografischen Zufall der Plattform verwenden, nie `Random` fuer Geheimnisse. / Use platform cryptographic random APIs, never `Random` for secrets. |
| TLS | TLS-Validierung nicht abschalten; Zertifikatsfehler nicht ignorieren. / Do not disable TLS validation; do not ignore certificate errors. |
| Secrets | Secrets nie in Git, Logs, Testdaten oder Screenshots speichern. / Never store secrets in Git, logs, test data, or screenshots. |

## Ausbildungsregeln / Training Rules

- **DE:** Auszubildende muessen erkennen koennen, wann Standardbibliotheken ausreichen und wann Fachreview noetig ist.
- **EN:** Apprentices must be able to tell when standard libraries are enough and when expert review is needed.
- **DE:** Jede neue kryptografische Nutzung wird im Review mit Zweck, Datenart, Algorithmus, Schluesselablage und Fehlermodus dokumentiert.
- **EN:** Each new cryptographic use is reviewed with purpose, data type, algorithm, key storage, and failure mode.

## Entscheidungsweg / Decision Path

1. Kläre das Schutzziel: Vertraulichkeit, Integrität, Authentizität oder Passwortschutz.
2. Nutze zuerst eine gepflegte Plattformfunktion oder ein etabliertes Protokoll, zum Beispiel TLS, .NET Cryptography, Java Cryptography Architecture, Go `crypto`, RustCrypto oder Apple CryptoKit/Keychain.
3. Dokumentiere Algorithmus, Modus, Schlüsselherkunft, Speicherung, Rotation, Widerruf und sicheren Fehlerfall.
4. Vermeide feste Schlüssel, wiederverwendete Nonces, deaktivierte Zertifikatsprüfung und eigene Protokolle.
5. Hole Fachreview ein, wenn Schlüssel geteilt werden, Signaturen rechtliche Wirkung haben, Langzeitschutz nötig ist oder Standards widersprüchlich wirken.

*Clarify the protection goal; prefer maintained platform APIs and established protocols; document algorithm, key lifecycle, rotation, revocation, and safe failure; avoid hard-coded keys, nonce reuse, disabled certificate checks, and custom protocols; request specialist review for high-impact or unclear use.*

## Schlüssel-Lebenszyklus / Key Lifecycle

Der Nachweis umfasst Erzeugung, Verteilung, Speicherung, Nutzung, Rotation, Sperrung, Wiederherstellung und sichere Löschung. Entwickler*innen dürfen keine geheimen Schlüssel in Beispielcode, Tickets oder Screenshots übernehmen. / Evidence covers generation, distribution, storage, use, rotation, revocation, recovery, and secure deletion. Developers must not place secret keys in sample code, tickets, or screenshots.

## Für Auszubildende kurz erklärt / Short Explanation for Apprentices

**DE:** Kryptografie ist ein Werkzeugkasten für Schutz von Vertraulichkeit, Integrität und Authentizität. Der wichtigste Grundsatz lautet: Keine eigene Kryptografie bauen. Nutze geprüfte Bibliotheken und sichere Standardkonfigurationen.

**EN:** Cryptography is a toolbox for confidentiality, integrity, and authenticity. The main rule is: do not build your own cryptography. Use reviewed libraries and safe defaults.

## Prüf- und Evidenzhinweise / Review and Evidence Notes

- **DE:** Prüfe immer Zweck, Datenart, Algorithmus, Schlüsselablage, Rotation und Fehlerverhalten.
- **EN:** Always review purpose, data type, algorithm, key storage, rotation, and error behavior.
- **DE:** Typische Evidenz: S-ADR, Code-Review, Threat Model, Konfigurationsdatei, Test für Fehlerpfade.
- **EN:** Typical evidence: S-ADR, code review, threat model, configuration file, test for error paths.

## Nachweise / Evidence

- **DE:** Geeignete Evidenzpfade sind Code-Review, Security-Checkliste, Dependency-Audit, Threat Model, S-ADR oder Testnachweis.
- **EN:** Suitable evidence paths include code review, security checklist, dependency audit, threat model, S-ADR, or test evidence.
- **DE:** Nichtanwendbarkeit wird als `N/A` dokumentiert, wenn das Projekt keine vertraulichen Daten, keine Integritaetsnachweise und keine Authentisierung verarbeitet.
- **EN:** Non-applicability is documented as `N/A` when the project handles no confidential data, no integrity evidence, and no authentication.

## Preset-Bezug / Preset Alignment

- `security-governance`: Secure Coding, ASVS, Dependency Audit, Supply Chain Evidence.
- `architecture-governance`: Trust Boundaries, Secure Configuration, S-ADR.


## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 1.0.0 | 2026-07-10 | Erstes kontrolliertes Release als mitgeltendes Dokument der sichere-Entwicklung-Basis 3.0.0. / First controlled release as a related document of secure-development baseline 3.0.0. |
