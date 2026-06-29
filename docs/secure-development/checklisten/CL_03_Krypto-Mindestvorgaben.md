<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 03 – Kryptografische Mindestvorgaben / Cryptographic Minimum Requirements

### Zweck / Purpose

**DE:** Diese Checkliste benennt die einheitlichen Mindestalgorithmen und
Schlüssellängen für Verschlüsselung, Signatur, Hashing und Schlüsselverwaltung.
Sie ergänzt die Richtlinie „Sichere Entwicklung" und das mitgeltende Dokument
„Gebrauch kryptografischer Maßnahmen".

**EN:** This checklist lists the uniform minimum algorithms and key sizes
for encryption, signing, hashing, and key management. It complements the
"Secure Development" guideline and the related document "Gebrauch
kryptografischer Maßnahmen".

### Geltungsbereich / Scope

**DE:** Pflicht für alle Projekte, die Kryptografie einsetzen — auch indirekt
über Bibliotheken, Frameworks oder externe Dienste.

**EN:** Mandatory for all projects that use cryptography — including
indirectly via libraries, frameworks, or external services.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- Gebrauch kryptografischer Maßnahmen (interne Vorgabe)
- BSI TR-02102-1 (Kryptografische Verfahren: Empfehlungen und Schlüssellängen)
- NIST SP 800-131A Rev. 2 (Transitioning the Use of Cryptographic Algorithms)
- NIST SP 800-175B Rev. 1 (Guideline for Using Cryptographic Standards)
- ISO/IEC 27002:2022 A.8.24

#### URL-/Ablageverweise / URLs and Storage Locations

**DE:** Diese Links helfen beim Review. Projekt- oder organisationsinterne Dokumente koennen als lokale Arbeitskopie oder als Verweis auf den festgelegten Ablageort ergaenzt werden.

**EN:** These links help during reviews. Project or organization-internal documents can be added as local working copies or references to the defined storage location.

- **Richtlinie Sichere Entwicklung / Secure Development Guideline:** [lokale Arbeitsfassung in diesem Repository / local working copy in this repository](../Richtlinie_Sichere-Entwicklung.md)
- **Verfassung / Constitution:** [lokale Arbeitskopie der Verfassung / local working copy of the constitution](../constitution.md), [Verfassung im GitHub-Repository home-baseline / constitution in the home-baseline GitHub repository](https://github.com/hindermath/home-baseline/blob/main/constitution.md)
- **Checklisten-Index / Checklist index:** [Übersicht aller Checklisten / overview of all checklists](README.md)
- **Leitlinie fuer sichere Programmierung / Secure coding guideline:** dieser Leitfaden oder eine projektspezifische gleichwertige Leitlinie / this guide or an equivalent project-specific guideline
- **Secure coding guideline:** this guide or an equivalent project-specific guideline
- **CISA Memory Safe Roadmaps:** [lokale PDF-Kopie des CISA-Dokuments / local PDF copy of the CISA document](../mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf), [EN-Markdown](../mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.EN.md), [DE-Lernfassung](../mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.DE.md), [CISA-Webseite zum Dokument / CISA webpage for the document](https://www.cisa.gov/resources-tools/resources/case-memory-safe-roadmaps)
- **ISO/IEC 27001:2022:** [offizielle ISO-Webseite zur ISO/IEC 27001:2022 / official ISO webpage for ISO/IEC 27001:2022](https://www.iso.org/standard/27001)
- **ISO/IEC 27002:2022:** [offizielle ISO-Webseite zur ISO/IEC 27002:2022 / official ISO webpage for ISO/IEC 27002:2022](https://www.iso.org/standard/75652.html)
- **NIST SSDF SP 800-218:** [NIST-Veröffentlichung SP 800-218 Secure Software Development Framework / NIST publication SP 800-218 Secure Software Development Framework](https://csrc.nist.gov/publications/detail/sp/800-218/final)
- **NIST Zero Trust SP 800-207:** [NIST-Veröffentlichung SP 800-207 Zero Trust Architecture / NIST publication SP 800-207 Zero Trust Architecture](https://csrc.nist.gov/publications/detail/sp/800-207/final)
- **OWASP ASVS:** [OWASP-Projektseite Application Security Verification Standard / OWASP project page Application Security Verification Standard](https://owasp.org/www-project-application-security-verification-standard/)
- **OWASP Cheat Sheet Series:** [OWASP Cheat Sheet Series Projektseite / OWASP Cheat Sheet Series project page](https://cheatsheetseries.owasp.org/)
- **OWASP Proactive Controls:** [OWASP Proactive Controls Projektseite / OWASP Proactive Controls project page](https://owasp.org/www-project-proactive-controls/)
- **OWASP SAMM:** [OWASP SAMM Projektseite / OWASP SAMM project page](https://owaspsamm.org/)
- **OWASP Top 10 for LLM Applications:** [OWASP Top 10 for LLM Applications Projektseite / OWASP Top 10 for LLM Applications project page](https://owasp.org/www-project-top-10-for-large-language-model-applications/)
- **CWE Top 25:** [MITRE CWE Top 25 Übersicht / MITRE CWE Top 25 overview](https://cwe.mitre.org/top25/)
- **CAPEC:** [MITRE CAPEC Katalog / MITRE CAPEC catalogue](https://capec.mitre.org/)
- **CycloneDX:** [CycloneDX SBOM-Standard Projektseite / CycloneDX SBOM standard project page](https://cyclonedx.org/)
- **SPDX:** [SPDX SBOM-Standard Projektseite / SPDX SBOM standard project page](https://spdx.dev/)
- **CSAF/VEX:** [OASIS CSAF und VEX Dokumentation / OASIS CSAF and VEX documentation](https://oasis-open.github.io/csaf-documentation/)
- **SLSA:** [SLSA Supply-chain Levels for Software Artifacts Projektseite / SLSA project page](https://slsa.dev/)
- **OpenSSF Scorecard:** [OpenSSF Scorecard Projektseite / OpenSSF Scorecard project page](https://scorecard.dev/)
- **RFC 9116 security.txt:** [RFC 9116 zu security.txt / RFC 9116 for security.txt](https://www.rfc-editor.org/rfc/rfc9116)
- **NIST AI Risk Management Framework:** [NIST AI Risk Management Framework Webseite / NIST AI Risk Management Framework webpage](https://www.nist.gov/itl/ai-risk-management-framework)
- **EU Cyber Resilience Act:** [EU-Amtsblatt zum Cyber Resilience Act / EU Official Journal for the Cyber Resilience Act](https://eur-lex.europa.eu/eli/reg/2024/2847/oj)
- **BSI TR-02102:** [BSI-Webseite zur Technischen Richtlinie TR-02102 / BSI webpage for Technical Guideline TR-02102](https://www.bsi.bund.de/DE/Themen/Unternehmen-und-Organisationen/Standards-und-Zertifizierung/Technische-Richtlinien/TR-nach-Thema-sortiert/tr02102/tr-02102.html)

### Bewertung und Dokumentation / Assessment and Documentation

**DE:** Jeder Prüfpunkt bekommt genau einen Status. Schreibe die Begründung so, dass eine neue Kollegin oder ein neuer Kollege den Entscheid später ohne Rückfrage versteht.

**EN:** Each checklist item gets exactly one status. Write the explanation so that a new team member can understand the decision later without asking again.

- **Erfüllt / Fulfilled:** Die Anforderung ist umgesetzt und es gibt einen prüfbaren Nachweis.
- **Nicht erfüllt / Not fulfilled:** Die Anforderung ist noch nicht umgesetzt oder der Nachweis fehlt. Es muss eine Aufgabe mit Verantwortlicher Person und Termin geben.
- **Nicht anwendbar / Not applicable:** Die Anforderung passt nicht zum Projekt. Das ist erlaubt, aber nur mit kurzer Begründung.

**Pflichtfelder je Prüfpunkt / Required fields per item:** Status, Begründung, Evidenzpfad oder Link, nächste Maßnahme mit Verantwortlicher Person und Zieltermin.

### Durchführungshinweise / Implementation Guidance

**DE:** Nutze diese Checkliste nicht als reine Ja/Nein-Liste. Sie ist ein Arbeits- und Auditdokument. Prüfe jeden Punkt gegen reale Artefakte: Code, Pull Request, Architekturdiagramm, Build-Log, Scan-Ergebnis, Ticket, Betriebsdokumentation oder Freigabeprotokoll. Wenn ein Nachweis noch fehlt, markiere den Punkt als „nicht erfüllt" und lege eine konkrete Folgeaufgabe an.

**EN:** Do not use this checklist as a simple yes/no list. It is a working and audit document. Check each item against real artefacts: code, pull request, architecture diagram, build log, scan result, ticket, operations document, or approval record. If evidence is missing, mark the item as "not fulfilled" and create a concrete follow-up task.

**DE:** Schreibe kurze, klare Begründungen. Vermeide Abkürzungen ohne Erklärung. Wenn ein Punkt technisch schwierig ist, beschreibe den aktuellen Stand, das Risiko und den nächsten machbaren Schritt.

**EN:** Write short and clear explanations. Avoid unexplained abbreviations. If an item is technically difficult, describe the current state, the risk, and the next feasible step.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Ein Dienst speichert API-Tokens verschlüsselt. Die Evidenz nennt AES-256-GCM, den Secret Store, den Rotationsplan und den letzten TLS-Scan. Wenn ein Altsystem noch SHA-1 braucht, ist das „nicht erfüllt" oder nur mit befristeter Risikoakzeptanz zulässig.

**EN:** A service stores API tokens encrypted. The evidence names AES-256-GCM, the secret store, the rotation plan, and the latest TLS scan. If a legacy system still needs SHA-1, this is "not fulfilled" or only allowed with temporary risk acceptance.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [BSI TR-02102](#cl-03-glossar-bsi-tr-02102)
- [AES-GCM](#cl-03-glossar-aes-gcm)
- [AEAD](#cl-03-glossar-aead)
- [RSA](#cl-03-glossar-rsa)
- [ECDSA / EdDSA](#cl-03-glossar-ecdsa-eddsa)
- [SHA-256](#cl-03-glossar-sha-256)
- [HMAC](#cl-03-glossar-hmac)
- [Argon2 / bcrypt / scrypt](#cl-03-glossar-password-hashing)
- [TLS](#cl-03-glossar-tls)
- [CSPRNG](#cl-03-glossar-csprng)
- [KMS](#cl-03-glossar-kms)
- [HSM](#cl-03-glossar-hsm)
- [MD5](#cl-03-glossar-md5)
- [Post-Quanten-Krypto / PQC](#cl-03-glossar-pqc)
- [Evidenz / Evidence](#cl-03-glossar-evidenz)

### Checkliste / Checklist

#### 1. Symmetrische Verschlüsselung / Symmetric Encryption

- **DE:** Eingesetzt wird AES-256 in einem authentisierten Modus (AEAD), vorzugsweise
  AES-GCM oder AES-GCM-SIV; alternativ ChaCha20-Poly1305 (z. B. auf Plattformen ohne
  AES-NI). Konkret: in Java `Cipher.getInstance("AES/GCM/NoPadding")`, in .NET
  `System.Security.Cryptography.AesGcm`, in Python `cryptography.hazmat.primitives.ciphers.aead.AESGCM`,
  in Go `crypto/cipher.NewGCM`, in libsodium `crypto_aead_chacha20poly1305_ietf_encrypt`.
  Eine Nonce wird je Schlüssel niemals wiederverwendet (12 Byte Zufalls-Nonce für
  AES-GCM oder ein streng monoton steigender Zähler). Modi ohne Authentisierung
  (AES-CBC, AES-CTR ohne separaten MAC) sind verboten.
- **EN:** Use AES-256 in an authenticated mode (AEAD), preferably AES-GCM or
  AES-GCM-SIV; ChaCha20-Poly1305 is the alternative (e.g. on platforms without
  AES-NI). Concretely: Java `Cipher.getInstance("AES/GCM/NoPadding")`, .NET
  `System.Security.Cryptography.AesGcm`, Python `cryptography.hazmat.primitives.ciphers.aead.AESGCM`,
  Go `crypto/cipher.NewGCM`, libsodium `crypto_aead_chacha20poly1305_ietf_encrypt`.
  A nonce is never reused per key (12-byte random nonce for AES-GCM or a strictly
  monotonic counter). Non-authenticated modes (AES-CBC or AES-CTR without a
  separate MAC) are forbidden.
- **Akzeptanz / Acceptance:** Algorithmus, Modus, Nonce-Strategie und Bibliothek
  je Modul dokumentiert. / Algorithm, mode, nonce strategy, and library
  documented per module.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 2. Asymmetrische Verschlüsselung und Signatur / Asymmetric Encryption and Signature

- **DE:** RSA mit mindestens 3072 Bit, ECDSA mit Kurve P-256 (secp256r1) oder höher,
  oder Ed25519 (bevorzugt für neue Signaturen). Für Schlüsseltausch: ECDH (P-256
  oder höher) oder X25519. RSA-Verschlüsselung verwendet RSA-OAEP mit SHA-256
  (`Cipher.getInstance("RSA/ECB/OAEPWithSHA-256AndMGF1Padding")` in Java,
  `RSA.encrypt(..., padding=OAEP(SHA256()))` in Python `cryptography`,
  `RSAEncryptionPadding.OaepSHA256` in .NET); RSA-PKCS#1 v1.5 ist für neue
  Anwendungen verboten. ECDSA-Signaturen sollen RFC 6979 (deterministisch)
  oder einen geprüften CSPRNG verwenden, um Nonce-Kollisionen zu vermeiden.
  Verbotene Kurven: P-192 (secp192r1), brainpoolP160 und kürzere.
- **EN:** RSA with at least 3072 bit, ECDSA with curve P-256 (secp256r1) or
  higher, or Ed25519 (preferred for new signatures). For key exchange: ECDH
  (P-256 or higher) or X25519. RSA encryption uses RSA-OAEP with SHA-256
  (`Cipher.getInstance("RSA/ECB/OAEPWithSHA-256AndMGF1Padding")` in Java,
  `RSA.encrypt(..., padding=OAEP(SHA256()))` in Python `cryptography`,
  `RSAEncryptionPadding.OaepSHA256` in .NET); RSA-PKCS#1 v1.5 is forbidden
  for new applications. ECDSA signatures should use RFC 6979 (deterministic)
  or a vetted CSPRNG to avoid nonce collisions. Forbidden curves: P-192
  (secp192r1), brainpoolP160, and shorter.
- **Akzeptanz / Acceptance:** Algorithmus, Kurve, Padding und Bibliothek je
  Modul dokumentiert. / Algorithm, curve, padding, and library documented
  per module.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 3. Hash-Funktionen / Hash Functions

- **DE:** SHA-256 oder höher (SHA-384, SHA-512, SHA-3-Familie wie SHA3-256,
  SHA3-512, oder BLAKE2/BLAKE3). Konkret: in Python `hashlib.sha256()`, in
  Java `MessageDigest.getInstance("SHA-256")`, in .NET `SHA256.HashData(..)`,
  in Go `crypto/sha256.Sum256(..)`, in Node.js `crypto.createHash("sha256")`.
  MD5 und SHA-1 sind für Sicherheitszwecke (Signaturen, Integritätsprüfung
  von vertraulichen Daten, Passwort-Ableitung, HMAC-Schlüsselableitung)
  verboten. Nicht-sicherheitsrelevante Anwendungen wie Cache-Schlüssel,
  Datei-Deduplizierung oder ETags sind erlaubt, müssen aber als „nicht
  sicherheitsrelevant" gekennzeichnet sein.
- **EN:** SHA-256 or higher (SHA-384, SHA-512, SHA-3 family such as SHA3-256
  or SHA3-512, or BLAKE2/BLAKE3). Concretely: Python `hashlib.sha256()`,
  Java `MessageDigest.getInstance("SHA-256")`, .NET `SHA256.HashData(..)`,
  Go `crypto/sha256.Sum256(..)`, Node.js `crypto.createHash("sha256")`.
  MD5 and SHA-1 are forbidden for security purposes (signatures, integrity
  checks of confidential data, password derivation, HMAC key derivation).
  Non-security uses like cache keys, file deduplication, or ETags are
  allowed but must be flagged as "not security-relevant".
- **Akzeptanz / Acceptance:** Hash-Algorithmus, Bibliothek und Anwendungsfall
  („sicherheitsrelevant" oder „nicht sicherheitsrelevant") je Modul
  dokumentiert. / Hash algorithm, library, and use case ("security-relevant"
  or "not security-relevant") documented per module.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 4. Passwort-Hashing / Password Hashing

- **DE:** Argon2id (bevorzugt) mit Mindest-Parametern m = 64 MiB Speicher,
  t = 3 Iterationen, p = 1 (oder vergleichbare Profile nach OWASP Password
  Storage Cheat Sheet). Alternativ scrypt mit N ≥ 2^17, r = 8, p = 1, oder
  bcrypt mit Cost-Faktor ≥ 12. Salz mit mindestens 16 Byte aus einem CSPRNG.
  Konkret: in Python `argon2.PasswordHasher` (Paket `argon2-cffi`) oder
  `passlib.hash.argon2`, in Java `org.bouncycastle.crypto.generators.Argon2BytesGenerator`
  oder `de.mkammerer:argon2-jvm`, in .NET `Konscious.Security.Cryptography.Argon2id`,
  in Node.js das Paket `argon2`. Plaintext-Speicherung von Passwörtern und
  einfache Hashes ohne Salz (z. B. `SHA-256(password)`) sind verboten. Alte
  schnelle Hash-Funktionen (MD5, SHA-1, SHA-256) sind als Passwort-Hash
  verboten, weil sie GPU- und ASIC-Angriffe nicht ausreichend bremsen.
- **EN:** Argon2id (preferred) with minimum parameters m = 64 MiB memory,
  t = 3 iterations, p = 1 (or comparable profiles from the OWASP Password
  Storage Cheat Sheet). Alternative: scrypt with N ≥ 2^17, r = 8, p = 1,
  or bcrypt with cost factor ≥ 12. Salt of at least 16 bytes from a CSPRNG.
  Concretely: Python `argon2.PasswordHasher` (package `argon2-cffi`) or
  `passlib.hash.argon2`, Java `org.bouncycastle.crypto.generators.Argon2BytesGenerator`
  or `de.mkammerer:argon2-jvm`, .NET `Konscious.Security.Cryptography.Argon2id`,
  Node.js the `argon2` package. Plaintext password storage and plain hashes
  without salt (e.g. `SHA-256(password)`) are forbidden. Old fast hash
  functions (MD5, SHA-1, SHA-256) are forbidden as password hashes because
  they do not slow down GPU and ASIC attacks enough.
- **Akzeptanz / Acceptance:** Algorithmus, Parameter (m, t, p oder cost),
  Bibliothek und Salz-Quelle je Anwendung dokumentiert. / Algorithm,
  parameters (m, t, p or cost), library, and salt source documented per
  application.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 5. Message Authentication Code (MAC) / MAC

- **DE:** HMAC mit SHA-256 oder höher (HMAC-SHA-256, HMAC-SHA-384,
  HMAC-SHA-512). Für SHA-3-Familien zusätzlich KMAC. Konkret: in Python
  `hmac.new(key, msg, hashlib.sha256)` oder `cryptography.hazmat.primitives.hmac.HMAC`,
  in Java `Mac.getInstance("HmacSHA256")`, in .NET `HMACSHA256`, in Go
  `crypto/hmac.New(sha256.New, key)`, in Node.js `crypto.createHmac("sha256", key)`.
  Schlüssel mindestens so lang wie der Hash-Output (32 Byte für SHA-256).
  HMAC-MD5 und HMAC-SHA1 sind für neue Anwendungen verboten. Vergleiche
  müssen zeit-konstant sein (`hmac.compare_digest`, `MessageDigest.isEqual`,
  `CryptographicOperations.FixedTimeEquals`); einfaches `==` öffnet ein
  Timing-Seitenkanal-Risiko. Für AEAD-Verfahren (AES-GCM, ChaCha20-Poly1305)
  entfällt ein separater MAC, da die Authentisierung integriert ist.
- **EN:** HMAC with SHA-256 or higher (HMAC-SHA-256, HMAC-SHA-384,
  HMAC-SHA-512). For SHA-3 families also KMAC. Concretely: Python
  `hmac.new(key, msg, hashlib.sha256)` or
  `cryptography.hazmat.primitives.hmac.HMAC`, Java
  `Mac.getInstance("HmacSHA256")`, .NET `HMACSHA256`, Go
  `crypto/hmac.New(sha256.New, key)`, Node.js
  `crypto.createHmac("sha256", key)`. Keys at least as long as the hash
  output (32 bytes for SHA-256). HMAC-MD5 and HMAC-SHA1 are forbidden for
  new applications. Comparisons must be constant-time (`hmac.compare_digest`,
  `MessageDigest.isEqual`, `CryptographicOperations.FixedTimeEquals`);
  plain `==` opens a timing side-channel. AEAD modes (AES-GCM,
  ChaCha20-Poly1305) do not need a separate MAC because authentication is
  integrated.
- **Akzeptanz / Acceptance:** MAC-Verfahren, Bibliothek und
  Vergleichsfunktion je Schnittstelle dokumentiert. / MAC scheme, library,
  and comparison function documented per interface.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 6. Verbotene oder eingeschränkte Verfahren / Forbidden or Restricted Algorithms

- **DE:** Verboten sind: MD5 und SHA-1 für jede Sicherheitsfunktion (Signaturen,
  Zertifikate, HMAC-Schlüssel-Ableitung); DES und 3DES (Triple-DES) als
  Block-Cipher; RC4 als Stromchiffre; RSA-PKCS#1 v1.5 (Padding) für
  Verschlüsselung in neuen Anwendungen; AES im ECB-Modus
  (`Cipher.getInstance("AES/ECB/...")`, `AES.new(key, AES.MODE_ECB)`); statische
  IVs in CBC-/CTR-Modi; selbstgebaute Hash-Konstruktionen (`H(secret || data)`
  ohne HMAC); kürzere elliptische Kurven als P-256 (P-192, brainpoolP160).
  Beispiel-Anti-Patterns: `MessageDigest.getInstance("MD5")` für
  Passwort-Hash, `Cipher.getInstance("DES")`, `Cipher.getInstance("AES/ECB/PKCS5Padding")`,
  `crypto.createHash("sha1")` für Token-Validierung. Ausnahmen brauchen
  eine dokumentierte Risikoakzeptanz mit Ablaufdatum, Verantwortlicher und
  Migrationsplan im Risikoregister.
- **EN:** Forbidden: MD5 and SHA-1 for any security function (signatures,
  certificates, HMAC key derivation); DES and 3DES (Triple-DES) as block
  ciphers; RC4 as a stream cipher; RSA-PKCS#1 v1.5 (padding) for encryption
  in new applications; AES in ECB mode (`Cipher.getInstance("AES/ECB/...")`,
  `AES.new(key, AES.MODE_ECB)`); static IVs in CBC/CTR modes; self-built
  hash constructions (`H(secret || data)` without HMAC); elliptic curves
  shorter than P-256 (P-192, brainpoolP160). Example anti-patterns:
  `MessageDigest.getInstance("MD5")` for password hashing,
  `Cipher.getInstance("DES")`, `Cipher.getInstance("AES/ECB/PKCS5Padding")`,
  `crypto.createHash("sha1")` for token validation. Exceptions need a
  documented risk acceptance with expiry date, owner, and migration plan in
  the risk register.
- **Akzeptanz / Acceptance:** Code-Scan oder Linter-Bericht ohne Treffer; bei
  Ausnahme: Risikoregister-Eintrag mit Migrationspfad. / Code scan or
  linter report with no findings; for exceptions: risk register entry with
  migration path.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 7. TLS-Konfiguration / TLS Configuration

- **DE:** TLS 1.2 ist das Mindestmaß, TLS 1.3 ist bevorzugt. SSLv2, SSLv3,
  TLS 1.0 und TLS 1.1 sind deaktiviert. Empfohlene TLS 1.3 Cipher-Suites:
  `TLS_AES_256_GCM_SHA384`, `TLS_CHACHA20_POLY1305_SHA256`,
  `TLS_AES_128_GCM_SHA256`. Empfohlene TLS 1.2 Suites mit Forward Secrecy:
  `ECDHE-ECDSA-AES256-GCM-SHA384`, `ECDHE-RSA-AES256-GCM-SHA384`,
  `ECDHE-ECDSA-CHACHA20-POLY1305`, `ECDHE-RSA-CHACHA20-POLY1305`.
  Konfigurationsbeispiele: in nginx `ssl_protocols TLSv1.2 TLSv1.3;
  ssl_ciphers ECDHE+AESGCM:ECDHE+CHACHA20; ssl_prefer_server_ciphers on;`,
  in Apache `SSLProtocol -all +TLSv1.2 +TLSv1.3`, in Spring Boot
  `server.ssl.enabled-protocols=TLSv1.2,TLSv1.3`, in Kestrel
  `SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13`. HSTS
  (`Strict-Transport-Security: max-age=31536000; includeSubDomains`) ist
  für öffentliche Endpunkte gesetzt. Zertifikate aus einer CA, die im
  CT-Log (Certificate Transparency) erscheint; selbstsignierte Zertifikate
  nur in geschlossenen Test-/Entwicklungsumgebungen.
- **EN:** TLS 1.2 is the minimum, TLS 1.3 is preferred. SSLv2, SSLv3,
  TLS 1.0, and TLS 1.1 are disabled. Recommended TLS 1.3 cipher suites:
  `TLS_AES_256_GCM_SHA384`, `TLS_CHACHA20_POLY1305_SHA256`,
  `TLS_AES_128_GCM_SHA256`. Recommended TLS 1.2 suites with forward
  secrecy: `ECDHE-ECDSA-AES256-GCM-SHA384`, `ECDHE-RSA-AES256-GCM-SHA384`,
  `ECDHE-ECDSA-CHACHA20-POLY1305`, `ECDHE-RSA-CHACHA20-POLY1305`.
  Configuration examples: nginx `ssl_protocols TLSv1.2 TLSv1.3;
  ssl_ciphers ECDHE+AESGCM:ECDHE+CHACHA20; ssl_prefer_server_ciphers on;`,
  Apache `SSLProtocol -all +TLSv1.2 +TLSv1.3`, Spring Boot
  `server.ssl.enabled-protocols=TLSv1.2,TLSv1.3`, Kestrel
  `SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13`. HSTS
  (`Strict-Transport-Security: max-age=31536000; includeSubDomains`) is
  set for public endpoints. Certificates from a CA that appears in CT
  logs (Certificate Transparency); self-signed certificates only in closed
  test or development environments.
- **Akzeptanz / Acceptance:** TLS-Scan-Bericht pro Endpunkt (z. B.
  `testssl.sh`, Qualys SSL Labs, `sslyze`) mit Mindest-Note A oder
  vergleichbar. / TLS scan report per endpoint (e.g. `testssl.sh`,
  Qualys SSL Labs, `sslyze`) with at least an "A" rating or equivalent.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 8. Zufallszahlen / Random Numbers

- **DE:** Sicherheitsrelevante Zufallszahlen kommen aus kryptografisch
  sicheren Quellen (`/dev/urandom`, `getrandom()`, `CryptGenRandom`,
  Bibliotheks-CSPRNG). Vorhersagbare Quellen wie `Math.random` oder
  `rand()` werden nicht verwendet.
- **EN:** Security-relevant random numbers come from cryptographically secure
  sources (`/dev/urandom`, `getrandom()`, `CryptGenRandom`, library CSPRNG).
  Predictable sources like `Math.random` or `rand()` are not used.
- **Akzeptanz / Acceptance:** Quelle je Modul benannt.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 9. Schlüsselverwaltung / Key Management

- **DE:** Schlüssel liegen in einem geeigneten Secret Store: HashiCorp Vault,
  AWS KMS, Azure Key Vault, Google Cloud KMS, hardware-basiertes HSM (z. B.
  Thales Luna, AWS CloudHSM, YubiHSM), OS-Keychain (macOS Keychain,
  Windows DPAPI/Credential Manager, libsecret unter Linux), oder Kubernetes
  Secrets mit aktivierter Encryption at Rest. Schlüssel liegen niemals im
  Quellcode, in `.env`-Dateien, in Git-versionierten Konfigurationen, in
  Container-Images, in Build-Logs oder in CI-Variablen ohne Maskierung.
  Zugriff folgt dem Least-Privilege-Prinzip: getrennte Rollen für lesen,
  schreiben, rotieren, löschen; Audit-Log aller Zugriffe. Pre-Commit-Hooks
  oder Scanner wie `gitleaks`, `trufflehog`, `detect-secrets` blockieren
  versehentliche Commits. Bei Geheimnis-Kompromittierung wird sofort
  rotiert und der Vorfall im Sicherheits-Logbuch dokumentiert.
- **EN:** Keys live in a suitable secret store: HashiCorp Vault, AWS KMS,
  Azure Key Vault, Google Cloud KMS, hardware HSM (e.g. Thales Luna, AWS
  CloudHSM, YubiHSM), OS keychain (macOS Keychain, Windows DPAPI/Credential
  Manager, libsecret on Linux), or Kubernetes Secrets with encryption at
  rest enabled. Keys are never in source code, `.env` files, Git-tracked
  configuration, container images, build logs, or unmasked CI variables.
  Access follows least privilege: separate roles for read, write, rotate,
  delete; audit log of all access. Pre-commit hooks or scanners like
  `gitleaks`, `trufflehog`, `detect-secrets` block accidental commits. On
  secret compromise, immediate rotation and incident logging in the
  security record.
- **Akzeptanz / Acceptance:** Secret-Store-Name, Zugriffsmodell (Rollen,
  Berechtigungen), Audit-Log-Pfad und Scanner-Konfiguration je Projekt
  dokumentiert. / Secret store name, access model (roles, permissions),
  audit log path, and scanner configuration documented per project.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 10. Schlüsselrotation / Key Rotation

- **DE:** Festgelegte Rotationsfristen je Schlüsseltyp: TLS-Server-Zertifikate
  ≤ 397 Tage (Browser-Limit ab 2020), idealerweise per ACME (Let's Encrypt,
  ZeroSSL) alle 60–90 Tage automatisiert; API- und Service-Tokens 90 Tage;
  Datenbank-Verschlüsselungsschlüssel (DEK) jährlich oder häufiger;
  Master-/Wrapping-Keys (KEK) im KMS jährlich mit Versionierung;
  Signaturschlüssel projektabhängig (oft mehrere Jahre, aber mit
  Migrationspfad). Rotation funktioniert mit überlappender Gültigkeit
  (Old-Key + New-Key gleichzeitig akzeptieren), bis alle Verbraucher
  umgestellt sind. Werkzeuge: AWS KMS Automatic Key Rotation, Azure Key
  Vault Auto-Rotation, HashiCorp Vault `vault rotate`, cert-manager unter
  Kubernetes. Bei Verdacht auf Kompromittierung wird sofort rotiert,
  betroffene Sitzungen werden invalidiert, und der Vorfall geht in
  Incident Response.
- **EN:** Defined rotation periods per key type: TLS server certificates
  ≤ 397 days (browser limit since 2020), ideally automated via ACME
  (Let's Encrypt, ZeroSSL) every 60–90 days; API and service tokens 90
  days; database encryption keys (DEK) yearly or more often; master /
  wrapping keys (KEK) in the KMS yearly with versioning; signing keys
  project-dependent (often multi-year, but with a migration path).
  Rotation uses overlapping validity (old + new key both accepted) until
  all consumers have switched. Tools: AWS KMS Automatic Key Rotation,
  Azure Key Vault Auto-Rotation, HashiCorp Vault `vault rotate`,
  cert-manager on Kubernetes. On suspected compromise, immediate
  rotation, invalidate affected sessions, escalate to incident response.
- **Akzeptanz / Acceptance:** Rotationsplan mit Frist je Schlüsseltyp,
  Werkzeug, letztem Rotations-Datum und Verantwortlicher Person
  dokumentiert. / Rotation plan with period per key type, tooling, last
  rotation date, and owner documented.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 11. Keine eigene Krypto / No Custom Crypto

- **DE:** Eingesetzt werden geprüfte, aktiv gepflegte Bibliotheken: in Java
  JCA/JCE der Standard-JDK plus optional BouncyCastle (`org.bouncycastle`)
  oder Tink (`com.google.crypto.tink`); in .NET der Namensraum
  `System.Security.Cryptography` (z. B. `AesGcm`, `RSA`, `ECDsa`); in Python
  `cryptography` (Paket `cryptography` von der PyCA), `pynacl` oder Tink;
  in Go die Standardpakete `crypto/*` plus `golang.org/x/crypto`; in
  Node.js das eingebaute `crypto`-Modul oder `libsodium-wrappers`; in C/C++
  libsodium oder OpenSSL/BoringSSL via API-Wrapper. Eigenimplementierungen
  von Block-Ciphern, Hash-Funktionen, MACs, Schlüsselableitung, Padding,
  Konstanzvergleich oder Zufallszahlengenerator sind nicht zulässig — auch
  nicht „nur als Lernprojekt" im Produktivcode. Veraltete oder nicht
  gepflegte Bibliotheken (z. B. JCE-Provider ohne Sicherheitsupdates seit
  > 12 Monaten) müssen ersetzt werden.
- **EN:** Use vetted, actively maintained libraries: Java JCA/JCE in the
  JDK plus optional BouncyCastle (`org.bouncycastle`) or Tink
  (`com.google.crypto.tink`); .NET namespace `System.Security.Cryptography`
  (e.g. `AesGcm`, `RSA`, `ECDsa`); Python `cryptography` (PyCA package),
  `pynacl`, or Tink; Go standard `crypto/*` plus `golang.org/x/crypto`;
  Node.js built-in `crypto` or `libsodium-wrappers`; C/C++ libsodium or
  OpenSSL/BoringSSL via API wrappers. Custom implementations of block
  ciphers, hash functions, MACs, key derivation, padding, constant-time
  comparison, or random number generators are not allowed — not even "as
  a learning project" in production code. Outdated or unmaintained
  libraries (e.g. JCE providers without security updates for > 12 months)
  must be replaced.
- **Akzeptanz / Acceptance:** Bibliothek mit Versionsnummer je Sprache und
  Modul; SBOM (CycloneDX/SPDX) listet sie. / Library with version per
  language and module; SBOM (CycloneDX/SPDX) lists them.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 12. Hardware-Unterstützung / Hardware Support

- **DE:** Wo möglich werden Hardware-gestützte Schlüsselspeicher genutzt:
  TPM 2.0 (Trusted Platform Module) auf Servern und Notebooks für lokale
  Geräteschlüssel; HSM (Hardware Security Module) wie Thales Luna,
  Utimaco, AWS CloudHSM, YubiHSM 2 für hochwertige Master- oder
  Signaturschlüssel; Cloud-KMS (AWS KMS, Azure Key Vault Premium, Google
  Cloud KMS HSM-Tier) als verwaltete HSM-Variante; Smartcards oder
  USB-Token (YubiKey, NitroKey, FIDO2/WebAuthn) für persönliche
  Authentifizierung und Code-Signing. Schlüssel werden auf der Hardware
  erzeugt und verlassen sie niemals im Klartext (Schlüssel als
  „non-exportable" markiert). Beispiele: Code-Signing-Schlüssel auf einem
  HSM mit FIPS 140-2 Level 3 oder höher; SSH-Schlüssel auf einem YubiKey
  mit Resident-Key-Funktion; TLS-Schlüssel im Cloud-KMS mit
  Auto-Rotation. Bei nicht-realisierbarer Hardware-Bindung wird die
  Begründung (z. B. Container-Plattform ohne TPM-Passthrough) im S-ADR
  dokumentiert.
- **EN:** Where feasible, hardware-backed key stores are used: TPM 2.0
  (Trusted Platform Module) on servers and notebooks for local device
  keys; HSM (Hardware Security Module) such as Thales Luna, Utimaco,
  AWS CloudHSM, YubiHSM 2 for high-value master or signing keys; cloud
  KMS (AWS KMS, Azure Key Vault Premium, Google Cloud KMS HSM tier) as
  managed HSM variant; smartcards or USB tokens (YubiKey, NitroKey,
  FIDO2/WebAuthn) for personal authentication and code signing. Keys are
  generated on the hardware and never leave it in cleartext (keys flagged
  "non-exportable"). Examples: code-signing key on an HSM with FIPS
  140-2 Level 3 or higher; SSH key on a YubiKey with resident-key
  feature; TLS key in cloud KMS with auto-rotation. If hardware binding
  is not feasible, the justification (e.g. container platform without
  TPM passthrough) is documented in an S-ADR.
- **Akzeptanz / Acceptance:** Hardware-Typ, Sicherheitsstufe (z. B. FIPS
  140-2/3 Level), Konfiguration und Begründung je Schlüssel
  dokumentiert. / Hardware type, security level (e.g. FIPS 140-2/3
  level), configuration, and justification documented per key.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 13. Krypto-Agilität / Crypto Agility

- **DE:** Algorithmen, Schlüssellängen, Cipher-Suites, Hash-Funktionen und
  KDF-Parameter sind in der Konfiguration änderbar, ohne den Code zu
  ändern. So lässt sich auf neue BSI-/NIST-Empfehlungen oder akute
  Schwächen reagieren. Konkret: Cipher-Suites in `application.yaml`,
  `appsettings.json`, `nginx.conf` oder zentralem Config-Service; Argon2-
  Parameter (m, t, p) als Konfigurationswert mit Versions-Tag
  („argon2id-v1") im Hash-Präfix für Multi-Version-Support; in
  Verschlüsselungs-Bibliotheken Versionsfeld pro Geheimnis (z. B.
  `{"v": 2, "alg": "AES-256-GCM", "kid": "...", "ct": "..."}`), sodass
  alte und neue Verfahren parallel entschlüsselt werden können. Pro
  Algorithmus existiert ein Migrationspfad (alt akzeptieren, neu
  schreiben, später alt löschen). Vermeide hartkodierte
  `Cipher.getInstance("...")`-Strings über das ganze Projekt verteilt;
  zentralisiere die Auswahl in einer Krypto-Service-Schicht.
- **EN:** Algorithms, key sizes, cipher suites, hash functions, and KDF
  parameters are configurable without code changes, so the system can
  react to new BSI/NIST guidance or acute weaknesses. Concretely: cipher
  suites in `application.yaml`, `appsettings.json`, `nginx.conf`, or a
  central config service; Argon2 parameters (m, t, p) as configuration
  with a version tag ("argon2id-v1") in the hash prefix for multi-version
  support; in encryption libraries a version field per ciphertext (e.g.
  `{"v": 2, "alg": "AES-256-GCM", "kid": "...", "ct": "..."}`) so old
  and new schemes can be decrypted side by side. Each algorithm has a
  migration path (accept old, write new, later remove old). Avoid
  hard-coded `Cipher.getInstance("...")` strings scattered across the
  project; centralise the choice in a crypto service layer.
- **Akzeptanz / Acceptance:** Konfigurationspunkt(e) und
  Versionsfeld-Format dokumentiert; Test zeigt Wechsel ohne Code-
  Deployment. / Configuration point(s) and version-field format
  documented; test demonstrates switch without code deployment.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 14. Vorbereitung auf Post-Quanten-Krypto / Preparing for Post-Quantum Crypto

- **DE:** Für langlebige Daten (Aufbewahrung > 5 Jahre) und langlebige
  Signaturen wird die Migration auf quantensichere Verfahren beobachtet:
  NIST PQC-Standards FIPS 203 (ML-KEM, vormals Kyber, für Schlüsseltausch),
  FIPS 204 (ML-DSA, vormals Dilithium, für Signaturen) und FIPS 205
  (SLH-DSA, vormals SPHINCS+, hash-basierte Signaturen). Verfügbare
  Bibliotheken: liboqs (Open Quantum Safe) mit Bindings für C, Python,
  Java und Go; BouncyCastle ab Version 1.78; AWS-LC und Cloudflare
  CIRCL. Hybrid-Modi (klassisch + PQC, z. B. X25519+ML-KEM) werden für
  Übergangsphase bevorzugt. Ein Übergangsplan benennt: betroffene
  Datenklassen (z. B. archivierte Verträge, signierte Software-Releases),
  Zeitfenster („harvest now, decrypt later"-Risiko), bevorzugte PQC-
  Verfahren je Anwendungsfall, Pilotprojekt und Roll-out-Zeitplan.
- **EN:** For long-lived data (retention > 5 years) and long-lived
  signatures, the migration to quantum-safe schemes is tracked: NIST
  PQC standards FIPS 203 (ML-KEM, formerly Kyber, for key exchange),
  FIPS 204 (ML-DSA, formerly Dilithium, for signatures), and FIPS 205
  (SLH-DSA, formerly SPHINCS+, hash-based signatures). Available
  libraries: liboqs (Open Quantum Safe) with bindings for C, Python,
  Java, and Go; BouncyCastle from version 1.78; AWS-LC and Cloudflare
  CIRCL. Hybrid modes (classical + PQC, e.g. X25519+ML-KEM) are
  preferred during the transition. A transition plan names: affected
  data classes (e.g. archived contracts, signed software releases),
  exposure window ("harvest now, decrypt later" risk), preferred PQC
  schemes per use case, pilot project, and roll-out schedule.
- **Akzeptanz / Acceptance:** S-ADR oder Notiz mit Migrationspfad,
  Datenklassifikation und Zeitplan vorhanden. / S-ADR or note with
  migration path, data classification, and schedule available.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 15. Audit der Krypto-Nutzung / Crypto Usage Audit

- **DE:** Mindestens jährlich werden Algorithmen, Schlüssellängen, TLS-
  Konfigurationen, Cipher-Suites, Bibliotheksversionen und Schlüssel-
  Lebenszyklen gegen aktuelle BSI TR-02102, NIST SP 800-131A und
  CWE Top 25 geprüft. Werkzeuge: TLS-Scan mit `testssl.sh`, `sslyze`
  oder Qualys SSL Labs; Abhängigkeits-Scan mit `pip-audit`,
  `npm audit`, `dotnet list package --vulnerable`, OWASP Dependency-
  Check, Trivy oder Snyk; statische Code-Analyse für Krypto-Anti-
  Patterns mit Semgrep-Regelsatz `r/security`, SonarQube-Krypto-
  Regeln oder CodeQL; SBOM-Vergleich mit CycloneDX-Tool und VEX-
  Bewertung neuer CVEs. Im Audit-Bericht: Datum, geprüfte Komponenten,
  Abweichungen von Vorgaben, Risikobewertung, Folgeaufgaben mit
  Verantwortlicher Person und Termin. Bei kritischen Funden (z. B.
  bekannte CVE in Crypto-Bibliothek mit CVSS ≥ 7.0) erfolgt ein
  außerordentlicher Audit innerhalb von 30 Tagen.
- **EN:** At least once a year, algorithms, key sizes, TLS
  configurations, cipher suites, library versions, and key lifecycles
  are checked against current BSI TR-02102, NIST SP 800-131A, and
  CWE Top 25. Tooling: TLS scan with `testssl.sh`, `sslyze`, or
  Qualys SSL Labs; dependency scan with `pip-audit`, `npm audit`,
  `dotnet list package --vulnerable`, OWASP Dependency-Check, Trivy,
  or Snyk; static code analysis for crypto anti-patterns with the
  Semgrep rule set `r/security`, SonarQube crypto rules, or CodeQL;
  SBOM comparison via CycloneDX tooling and VEX assessment of new
  CVEs. The audit report contains: date, components reviewed,
  deviations from requirements, risk rating, follow-up tasks with
  owner and target date. On critical findings (e.g. known CVE in a
  crypto library with CVSS ≥ 7.0), an out-of-cycle audit happens
  within 30 days.
- **Akzeptanz / Acceptance:** Audit-Bericht mit Datum, geprüften
  Werkzeugen, Befunden und Folgeaufgaben im Sicherheitsdokumenten-
  bestand. / Audit report with date, tooling used, findings, and
  follow-up tasks in the security evidence set.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind, das mitgeltende Dokument
„Gebrauch kryptografischer Maßnahmen" verlinkt ist und der jüngste Audit-
Bericht im Sicherheitsdokumentenbestand abgelegt ist.

**EN:** Fulfilled when every item is closed, the related document "Gebrauch
kryptografischer Maßnahmen" is linked, and the latest audit report is
stored in the security evidence set.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-03-glossar-bsi-tr-02102"></a>

#### BSI TR-02102

- **DE:** BSI TR-02102 ist eine technische Richtlinie zu kryptografischen Verfahren. Sie hilft zu entscheiden, welche Algorithmen und Schlüssellängen aktuell geeignet sind.
- **EN:** BSI TR-02102 is a technical guideline for cryptographic methods. It helps decide which algorithms and key lengths are currently suitable.

<a id="cl-03-glossar-aes-gcm"></a>

#### AES-GCM

- **DE:** AES-GCM ist ein modernes Verfahren für symmetrische Verschlüsselung mit Integritätsschutz. Es schützt Inhalt und erkennt Manipulation.
- **EN:** AES-GCM is a modern method for symmetric encryption with integrity protection. It protects content and detects tampering.

<a id="cl-03-glossar-aead"></a>

#### AEAD

- **DE:** AEAD bezeichnet Verschlüsselungsverfahren, die Vertraulichkeit und Manipulationsschutz zusammen liefern. AES-GCM ist ein häufiges Beispiel.
- **EN:** AEAD means encryption methods that provide confidentiality and tamper protection together. AES-GCM is a common example.

<a id="cl-03-glossar-rsa"></a>

#### RSA

- **DE:** RSA ist ein asymmetrisches Kryptoverfahren. Es ist nur mit passenden Parametern und sicheren Padding-Verfahren zulässig.
- **EN:** RSA is an asymmetric cryptographic method. It is only acceptable with suitable parameters and secure padding methods.

<a id="cl-03-glossar-ecdsa-eddsa"></a>

#### ECDSA / EdDSA

- **DE:** ECDSA und EdDSA sind Signaturverfahren auf Basis elliptischer Kurven. Sie werden genutzt, um Herkunft und Integrität digital zu prüfen.
- **EN:** ECDSA and EdDSA are signature methods based on elliptic curves. They are used to verify digital origin and integrity.

<a id="cl-03-glossar-sha-256"></a>

#### SHA-256

- **DE:** SHA-256 ist eine kryptografische Hashfunktion. Sie erzeugt einen festen Prüfwert und wird zum Beispiel für Integritätsprüfungen genutzt.
- **EN:** SHA-256 is a cryptographic hash function. It creates a fixed digest and is used for integrity checks, for example.

<a id="cl-03-glossar-hmac"></a>

#### HMAC

- **DE:** HMAC ist ein Verfahren zur Integritätsprüfung mit geheimem Schlüssel. Es zeigt, ob eine Nachricht verändert wurde und vom erwarteten Absender stammt.
- **EN:** HMAC is a method for integrity checking with a secret key. It shows whether a message was changed and came from the expected sender.

<a id="cl-03-glossar-password-hashing"></a>

#### Argon2 / bcrypt / scrypt

- **DE:** Argon2, bcrypt und scrypt sind Verfahren zum sicheren Speichern von Passwörtern. Sie sind absichtlich langsam, damit Passwort-Raten erschwert wird.
- **EN:** Argon2, bcrypt, and scrypt are methods for storing passwords securely. They are intentionally slow to make password guessing harder.

<a id="cl-03-glossar-tls"></a>

#### TLS

- **DE:** TLS schützt Netzwerkverbindungen durch Verschlüsselung und Identitätsprüfung. Es wird zum Beispiel für HTTPS und viele API-Verbindungen genutzt.
- **EN:** TLS protects network connections through encryption and identity checks. It is used for HTTPS and many API connections, for example.

<a id="cl-03-glossar-csprng"></a>

#### CSPRNG

- **DE:** Ein CSPRNG ist ein kryptografisch sicherer Zufallszahlengenerator. Er wird für Schlüssel, Tokens, Nonces und andere Sicherheitswerte benötigt.
- **EN:** A CSPRNG is a cryptographically secure random number generator. It is needed for keys, tokens, nonces, and other security values.

<a id="cl-03-glossar-kms"></a>

#### KMS

- **DE:** Ein KMS ist ein Dienst zur Verwaltung kryptografischer Schlüssel. Es schützt Schlüssel, steuert Zugriff und protokolliert Nutzung.
- **EN:** A KMS is a service for managing cryptographic keys. It protects keys, controls access, and logs usage.

<a id="cl-03-glossar-hsm"></a>

#### HSM

- **DE:** Ein HSM ist spezielle Hardware zum Schutz kryptografischer Schlüssel. Es wird genutzt, wenn Schlüssel besonders stark geschützt werden müssen.
- **EN:** An HSM is special hardware for protecting cryptographic keys. It is used when keys need especially strong protection.

<a id="cl-03-glossar-md5"></a>

#### MD5

- **DE:** MD5 ist eine veraltete Hashfunktion. Sie ist für Sicherheitszwecke nicht mehr geeignet und darf nicht für Integrität oder Signaturen verwendet werden.
- **EN:** MD5 is an outdated hash function. It is no longer suitable for security purposes and must not be used for integrity or signatures.

<a id="cl-03-glossar-pqc"></a>

#### Post-Quanten-Krypto / PQC

- **DE:** Post-Quanten-Krypto bezeichnet Verfahren, die auch gegen zukünftige Quantencomputer widerstandsfähig sein sollen. Heute geht es meist um Vorbereitung und Migrationsfähigkeit.
- **EN:** Post-quantum cryptography means methods intended to resist future quantum computers. Today it mostly concerns preparation and migration ability.

<a id="cl-03-glossar-evidenz"></a>

#### Evidenz / Evidence

- **DE:** Evidenz ist ein prüfbarer Nachweis. Das kann ein Link, Ticket, Scan-Bericht, Pull Request, Protokoll, Architekturdiagramm oder Dokument sein.
- **EN:** Evidence is verifiable proof. It can be a link, ticket, scan report, pull request, record, architecture diagram, or document.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples

- **Version 1.2 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.3 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---
