<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 10 – Sichere Entwicklungsumgebung / Secure Development Environment

**Dokument-ID / Document ID:** CL-10
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste prüft, ob die Entwicklungs-, Build- und Test-Umgebungen
sicher konfiguriert sind. Sie deckt Arbeitsplatz, Quellcode-Verwaltung,
CI/CD-Pipelines und Geheimnisverwaltung ab.

**EN:** This checklist verifies that development, build, and test environments
are configured securely. It covers the developer workstation, source control,
CI/CD pipelines, and secret management.

### Geltungsbereich / Scope

**DE:** Pflicht für alle Projektteams, die unter die Richtlinie „Sichere
Entwicklung" fallen. Mindestens jährlich und nach jeder größeren Toolchain-
Änderung erneut prüfen.

**EN:** Mandatory for every project team covered by the "Secure Development"
guideline. Re-check at least once a year and after every major toolchain
change.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- ISO/IEC 27002:2022 A.8.31, A.8.32
- NIST SP 800-218 (SSDF)
- Verfassung XII, XIII

#### URL-/Ablageverweise / URLs and Storage Locations

**DE:** Diese Links helfen beim Review. Projekt- oder organisationsinterne Dokumente koennen als lokale Arbeitskopie oder als Verweis auf den festgelegten Ablageort ergaenzt werden.

**EN:** These links help during reviews. Project or organization-internal documents can be added as local working copies or references to the defined storage location.

- **Richtlinie Sichere Entwicklung / Secure Development Guideline:** [lokale Arbeitsfassung in diesem Repository / local working copy in this repository](../Richtlinie_Sichere-Entwicklung.md)
- **Verfassung / Constitution:** [lokale Arbeitskopie der Verfassung / local working copy of the constitution](../../../constitution.md), [Verfassung im GitHub-Repository home-baseline / constitution in the home-baseline GitHub repository](https://github.com/hindermath/home-baseline/blob/main/constitution.md)
- **Checklisten-Index / Checklist index:** [Übersicht aller Checklisten / overview of all checklists](../README.md)
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

**DE:** Jeder Prüfpunkt bekommt genau einen Wert je Statusachse. Schreibe die Begründung so, dass eine neue Kollegin oder ein neuer Kollege den Entscheid später ohne Rückfrage versteht.

**EN:** Each checklist item gets exactly one value per status axis. Write the explanation so that a new team member can understand the decision later without asking again.

- **Anwendbarkeit / Applicability:** `Applicable`, `N/A` oder `Open`.
- **Umsetzungsstatus / Implementation status:** `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed`.

**Pflichtfelder je Prüfpunkt / Required fields per item:** Anwendbarkeit, Umsetzungsstatus, Lernstufe, verantwortliche Rolle, Begründung, Evidenzpfad oder Link, Restrisiko, Neubewertungs-Trigger und nächste Maßnahme mit Zieltermin.

### Durchführungshinweise / Implementation Guidance

**DE:** Nutze diese Checkliste nicht als reine Ja/Nein-Liste. Sie ist ein Arbeits- und Auditdokument. Prüfe jeden Punkt gegen reale Artefakte: Code, Pull Request, Architekturdiagramm, Build-Log, Scan-Ergebnis, Ticket, Betriebsdokumentation oder Freigabeprotokoll. Wenn ein Nachweis noch fehlt, setze den Umsetzungsstatus auf `Not Assessed` oder `Not Fulfilled` und lege eine konkrete Folgeaufgabe an.

**EN:** Do not use this checklist as a simple yes/no list. It is a working and audit document. Check each item against real artefacts: code, pull request, architecture diagram, build log, scan result, ticket, operations document, or approval record. If evidence is missing, set the implementation status to `Not Assessed` or `Not Fulfilled` and create a concrete follow-up task.

**DE:** Schreibe kurze, klare Begründungen. Vermeide Abkürzungen ohne Erklärung. Wenn ein Punkt technisch schwierig ist, beschreibe den aktuellen Stand, das Risiko und den nächsten machbaren Schritt.

**EN:** Write short and clear explanations. Avoid unexplained abbreviations. If an item is technically difficult, describe the current state, the risk, and the next feasible step.

**DE:** Jeder Prüfpunkt muss deshalb drei Fragen beantworten: Was bedeutet die Anforderung im Projektalltag? Was ist konkret zu tun oder zu entscheiden? Welcher Nachweis zeigt das Ergebnis? Verwende Standard-IDs, Toolnamen und Abkürzungen nur zusammen mit einer kurzen Erklärung in Alltagssprache. Wenn ein Punkt für Auszubildende oder neue Teammitglieder nicht selbsterklärend ist, ergänze eine kurze Erklärung in der Begründung.

**EN:** Each item must therefore answer three questions: What does the requirement mean in daily project work? What exactly must be done or decided? Which evidence shows the result? Use standard IDs, tool names, and abbreviations only together with a short plain-language explanation. If an item is not self-explanatory for apprentices or new team members, add a short explanation in the rationale.

### Beispiel / Example

**DE:** Fuer ein Projekt wird nachgewiesen: Branch-Schutz ist aktiv, MFA ist erzwungen, Secret-Scanning blockiert Pushes, Runner haben minimale Rechte, und der letzte Wiederherstellungstest ist datiert.

**EN:** For a project, evidence shows: branch protection is active, MFA is enforced, secret scanning blocks pushes, runners have least privilege, and the latest recovery test has a date.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [Festplattenverschlüsselung / Full-Disk Encryption](#cl-10-glossar-full-disk-encryption)
- [MFA](#cl-10-glossar-mfa)
- [IDE](#cl-10-glossar-ide)
- [Branch-Schutz / Branch Protection](#cl-10-glossar-branch-protection)
- [Signierte Commits / Signed Commits](#cl-10-glossar-signed-commits)
- [GPG](#cl-10-glossar-gpg)
- [Sigstore / Cosign](#cl-10-glossar-sigstore-cosign)
- [Secret Scanning](#cl-10-glossar-secret-scanning)
- [Secret Store](#cl-10-glossar-secret-store)
- [CI/CD](#cl-10-glossar-ci-cd)
- [Umgebungstrennung / Environment Separation](#cl-10-glossar-environment-separation)
- [Testdaten / Test Data](#cl-10-glossar-test-data)
- [Audit-Spur / Audit Trail](#cl-10-glossar-audit-trail)
- [Zugriffsrezertifizierung / Access Recertification](#cl-10-glossar-access-recertification)
- [Endpoint-Schutz / Endpoint Protection](#cl-10-glossar-endpoint-protection)
- [Backup und Restore](#cl-10-glossar-backup-restore)
- [Cross-Platform-Skript](#cl-10-glossar-cross-platform-script)

### Checkliste / Checklist

#### CL-10-01: Festplattenverschlüsselung / Full-Disk Encryption

- **DE:** Entwickler-Notebooks und Build-Server haben aktive
  Festplattenverschlüsselung mit modernen Algorithmen (AES-XTS-256). Per
  Plattform: macOS FileVault 2 (`fdesetup status` zeigt `On`),
  Windows BitLocker (XTS-AES 256, `manage-bde -status` zeigt
  `Percentage Encrypted: 100,0%`, `Conversion Status: Fully Encrypted`),
  Linux LUKS2 (`cryptsetup luksDump /dev/sdaX`, AES-XTS-Plain64 mit
  PBKDF2/Argon2id-Schlüsselableitung). Recovery-Keys per Mobile Device
  Management (Microsoft Intune, Jamf Pro, Workspace ONE) zentral
  hinterlegt; Offline-Recovery-Code beim Geräteempfang dokumentiert.
  Pre-Boot-Authentifizierung Pflicht (TPM-PIN, Smartcard, FIDO2-Token);
  Ruhezustand und Hibernation-File ebenfalls verschlüsselt
  (`powercfg /h on` + BitLocker; macOS `pmset -a destroyfvkeyonstandby 1`).
  Entfernbare Datenträger nur mit BitLocker-To-Go oder LUKS verschlüsselt.
  Nachweis durch MDM-Reports oder `osquery`-Abfrage (`SELECT * FROM
  disk_encryption`). Referenzen: NIST SP 800-111, BSI IT-Grundschutz
  SYS.2.1.A8, ISO/IEC 27001 A.7.10.
- **EN:** Developer laptops and build servers have active full-disk
  encryption with modern algorithms (AES-XTS-256). Per platform: macOS
  FileVault 2 (`fdesetup status` shows `On`), Windows BitLocker
  (XTS-AES 256, `manage-bde -status` shows `Percentage Encrypted:
  100.0%`, `Conversion Status: Fully Encrypted`), Linux LUKS2
  (`cryptsetup luksDump /dev/sdaX`, AES-XTS-Plain64 with PBKDF2/Argon2id
  key derivation). Recovery keys centrally stored via Mobile Device
  Management (Microsoft Intune, Jamf Pro, Workspace ONE); offline
  recovery code documented at device handout. Pre-boot authentication
  mandatory (TPM PIN, smartcard, FIDO2 token); sleep and hibernation
  file also encrypted (`powercfg /h on` + BitLocker; macOS
  `pmset -a destroyfvkeyonstandby 1`). Removable drives only encrypted
  with BitLocker-To-Go or LUKS. Evidence via MDM reports or `osquery`
  query (`SELECT * FROM disk_encryption`). References: NIST SP 800-111,
  BSI IT-Grundschutz SYS.2.1.A8, ISO/IEC 27001 A.7.10.
- **Akzeptanz / Acceptance:** Status pro Gerät im MDM dokumentiert
  (`encryption_status: encrypted`, Algorithmus AES-XTS-256, Recovery-Key
  hinterlegt); jährlicher Audit-Bericht mit Liste aller verschlüsselten
  und ggf. Ausnahme-Geräten. / Status per device documented in MDM
  (`encryption_status: encrypted`, AES-XTS-256 algorithm, recovery key
  on file); annual audit report with list of all encrypted and any
  exempted devices.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-02: Bildschirmsperre und MFA / Screen Lock and MFA

- **DE:** Automatische Bildschirmsperre nach maximal 10 Minuten Inaktivität
  ist per MDM-Policy erzwungen (macOS `sudo defaults write
  com.apple.screensaver askForPassword 1; askForPasswordDelay 0`; Windows
  Group Policy `Computer Configuration > Administrative Templates > Control
  Panel > Personalization > Screen saver timeout = 600`; GNOME `gsettings
  set org.gnome.desktop.session idle-delay 600`). Konten für Quellcode-
  Plattform (GitHub, Git, Bitbucket), Build-System (Jenkins, GitHub
  Actions, Git CI, Azure DevOps), Cloud (AWS, Azure, GCP), Identity
  Provider (Entra ID, Okta, Keycloak), Paket-Registry (npm, PyPI,
  Maven Central, NuGet) und VPN nutzen Multi-Faktor-Authentifizierung
  (MFA). Phishing-resistente MFA bevorzugt: FIDO2/WebAuthn-Hardware-Keys
  (YubiKey 5, Google Titan, SoloKeys), Passkeys, Smartcard mit PKCS#11.
  TOTP (RFC 6238) als Fallback (Google Authenticator, Microsoft
  Authenticator, Authy); SMS-OTP nur als letzte Option und nicht für
  privilegierte Konten (SIM-Swapping-Risiko). Push-Notifications mit
  Number-Matching gegen MFA-Fatigue (Microsoft Authenticator,
  Duo Security). Conditional Access in Entra ID erzwingt MFA für alle
  riskanten Anmeldungen. Notfall-Konten (`break-glass`) ohne MFA-
  Sperrungsmöglichkeit aber mit Hardware-Key-Pflicht. Beispiel macOS-MDM-
  Profile: `com.apple.screensaver` mit `idleTime=600`,
  `askForPassword=1`. Referenzen: NIST SP 800-63B AAL2/AAL3, OWASP MFA
  Cheat Sheet, BSI TR-03107.
- **EN:** Automatic screen lock after max. 10 minutes of inactivity is
  enforced via MDM policy (macOS `sudo defaults write
  com.apple.screensaver askForPassword 1; askForPasswordDelay 0`;
  Windows Group Policy `Computer Configuration > Administrative
  Templates > Control Panel > Personalization > Screen saver timeout =
  600`; GNOME `gsettings set org.gnome.desktop.session idle-delay 600`).
  Accounts for source platform (GitHub, Git, Bitbucket), build system
  (Jenkins, GitHub Actions, Git CI, Azure DevOps), cloud (AWS, Azure,
  GCP), identity provider (Entra ID, Okta, Keycloak), package registry
  (npm, PyPI, Maven Central, NuGet), and VPN use multi-factor
  authentication (MFA). Phishing-resistant MFA preferred: FIDO2/WebAuthn
  hardware keys (YubiKey 5, Google Titan, SoloKeys), passkeys, smartcard
  with PKCS#11. TOTP (RFC 6238) as fallback (Google Authenticator,
  Microsoft Authenticator, Authy); SMS OTP only as last resort and not
  for privileged accounts (SIM-swapping risk). Push notifications with
  number matching against MFA fatigue (Microsoft Authenticator, Duo
  Security). Conditional Access in Entra ID enforces MFA for all risky
  sign-ins. Emergency accounts (`break-glass`) without MFA lockout but
  with hardware-key requirement. Example macOS MDM profile:
  `com.apple.screensaver` with `idleTime=600`, `askForPassword=1`.
  References: NIST SP 800-63B AAL2/AAL3, OWASP MFA Cheat Sheet, BSI
  TR-03107.
- **Akzeptanz / Acceptance:** MFA-Status je Plattform geprüft und in
  `docs/security/mfa-coverage.md` dokumentiert (Plattform, Account-Typ,
  MFA-Methode, Erzwingung); ≥ 95 % MFA-Coverage; phishing-resistente MFA
  für privilegierte Rollen Pflicht; MDM-Policy Bildschirmsperre ≤ 10 min
  aktiv. / MFA status checked per platform and documented in
  `docs/security/mfa-coverage.md` (platform, account type, MFA method,
  enforcement); ≥ 95 % MFA coverage; phishing-resistant MFA mandatory
  for privileged roles; MDM screen-lock policy ≤ 10 min active.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-03: IDE-Härtung / IDE Hardening

- **DE:** IDE-Plugins kommen aus geprüften Quellen (offizielle Marketplaces
  mit Verifizierten Publishern: VS Code Marketplace, JetBrains Plugin
  Repository, Visual Studio Marketplace, Eclipse Marketplace). Plugins aus
  unbekannten Quellen oder mit niedrigen Download-Zahlen sind verboten;
  jedes Plugin durchläuft Security-Review (Berechtigungen, Code-Signatur,
  Maintainer-Reputation, OpenSSF Scorecard ≥ 7.0). Sandboxing für KI-
  Plugins (Restricted Mode in VS Code: `security.workspace.trust.enabled =
  true`). Telemetrie und KI-Funktionen sind nach Vorgabe konfiguriert:
  GitHub Copilot Business/Enterprise mit deaktivierter Code-Suggestion-
  Telemetrie für sensitive Repositories (`github.copilot.advanced.
  excludeFiles`); JetBrains AI Assistant mit `Send code data` deaktiviert
  oder selbst-gehostetem LLM (Continue, OpenWebUI, LocalAI); VS Code
  Telemetrie reduziert (`telemetry.telemetryLevel = "off"` oder `crash`);
  Allgemeine IDE-Telemetrie per Enterprise-Policy abgeschaltet. Lokale
  Code-Caches sind bekannt und gesichert: Speicherorte dokumentiert
  (VS Code `~/.vscode/extensions/`, `~/Library/Application Support/Code/`;
  JetBrains `~/.cache/JetBrains/`, `~/Library/Caches/JetBrains/`); Caches
  liegen auf der verschlüsselten Festplatte; bei Geräte-Rückgabe oder
  Personalwechsel Cache und IDE-Workspace-State per `IDE Reset`-Skript
  gelöscht. SSH-Agent Forwarding nur restriktiv per
  `~/.ssh/config Match` und `IdentityAgent`. Standardprofil als
  `.editorconfig`, `.vscode/settings.json` (im Repository),
  `.idea/codeStyles/` versioniert. Pre-Commit-Hooks (`husky`,
  `pre-commit`) für Format/Lint vor jedem Commit. Referenzen: OWASP IDE
  Security, NIST SP 800-218 PW.6.
- **EN:** IDE plugins come from trusted sources (official marketplaces
  with verified publishers: VS Code Marketplace, JetBrains Plugin
  Repository, Visual Studio Marketplace, Eclipse Marketplace). Plugins
  from unknown sources or with low download counts are forbidden; every
  plugin undergoes security review (permissions, code signature,
  maintainer reputation, OpenSSF Scorecard ≥ 7.0). Sandboxing for AI
  plugins (Restricted Mode in VS Code:
  `security.workspace.trust.enabled = true`). Telemetry and AI features
  are configured per policy: GitHub Copilot Business/Enterprise with
  disabled code-suggestion telemetry for sensitive repositories
  (`github.copilot.advanced.excludeFiles`); JetBrains AI Assistant with
  `Send code data` disabled or self-hosted LLM (Continue, OpenWebUI,
  LocalAI); VS Code telemetry reduced (`telemetry.telemetryLevel = "off"`
  or `crash`); general IDE telemetry disabled via enterprise policy.
  Local code caches are known and protected: storage locations
  documented (VS Code `~/.vscode/extensions/`, `~/Library/Application
  Support/Code/`; JetBrains `~/.cache/JetBrains/`,
  `~/Library/Caches/JetBrains/`); caches reside on the encrypted disk; on
  device return or staff change, cache and IDE workspace state cleared
  via `IDE Reset` script. SSH agent forwarding restrictively only via
  `~/.ssh/config Match` and `IdentityAgent`. Standard profile versioned
  as `.editorconfig`, `.vscode/settings.json` (in repository),
  `.idea/codeStyles/`. Pre-commit hooks (`husky`, `pre-commit`) for
  format/lint before each commit. References: OWASP IDE Security, NIST
  SP 800-218 PW.6.
- **Akzeptanz / Acceptance:** IDE-Standardprofil im Repository
  (`.vscode/settings.json` oder `.idea/`); Plugin-Allowlist im MDM oder
  zentral verwalteter Marketplace; Telemetrie- und KI-Konfiguration
  dokumentiert; quartalsweiser Plugin-Audit. / IDE standard profile in
  repository (`.vscode/settings.json` or `.idea/`); plugin allowlist in
  MDM or centrally managed marketplace; telemetry and AI configuration
  documented; quarterly plugin audit.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-04: Branch-Schutz / Branch Protection

- **DE:** Schutzregeln für `main`/`master` (und Release-Branches) sind
  aktiv: (1) Pull-Request-Pflicht (kein Direkt-Push, auch nicht für
  Repository-Admins via `enforce_admins`); (2) mindestens 2 Code-Owner-
  Reviews bei kritischen Repositories, mindestens 1 Review sonst; (3)
  alle Status-Checks (CI, SAST, SCA, DAST, Tests) müssen grün sein
  (`requires_status_checks` mit `strict: true` für Up-to-date-Branch);
  (4) lineare Historie ohne Merge-Commits (`require_linear_history`);
  (5) signierte Commits Pflicht (`required_signatures`); (6) Force-Push
  und Branch-Löschung verboten (`allow_force_pushes: false`,
  `allow_deletions: false`); (7) Konversations-Resolution Pflicht
  (`required_conversation_resolution`); (8) Stale-Reviews werden bei
  neuen Commits zurückgesetzt (`dismiss_stale_reviews`); (9) Review von
  Nicht-Code-Owners zählt nicht (`require_code_owner_reviews`); (10)
  Bypass nur für definierte Bot-Accounts (`bypass_pull_request_allowances`).
  GitHub: Settings > Branches > Branch protection rules; per API
  `gh api -X PUT repos/{owner}/{repo}/branches/main/protection` mit JSON-
  Konfiguration. Git: Project > Settings > Repository > Protected
  branches, plus Push Rules (`reject_unsigned_commits: true`,
  `commit_message_regex`). Bitbucket: Repository settings > Branch
  permissions. CODEOWNERS-Datei in `.github/CODEOWNERS` oder
  `.git/CODEOWNERS` (z. B. `/security/ @security-team @ciso`).
  Repository-Rulesets (GitHub Enterprise) für organisationsweite
  Durchsetzung. Beispiel JSON für GitHub: `{ "required_pull_request_reviews":
  { "required_approving_review_count": 2,
  "require_code_owner_reviews": true,
  "dismiss_stale_reviews": true }, "enforce_admins": true,
  "required_linear_history": true, "required_signatures": true }`.
  Referenzen: SLSA Source Track Level 3, OpenSSF Branch-Protection-Check,
  NIST SP 800-218 PW.7.
- **EN:** Protection rules for `main`/`master` (and release branches)
  are active: (1) pull request required (no direct push, not even for
  repository admins via `enforce_admins`); (2) at least 2 code-owner
  reviews on critical repositories, at least 1 review otherwise; (3) all
  status checks (CI, SAST, SCA, DAST, tests) must be green
  (`requires_status_checks` with `strict: true` for up-to-date branch);
  (4) linear history without merge commits
  (`require_linear_history`); (5) signed commits mandatory
  (`required_signatures`); (6) force-push and branch deletion forbidden
  (`allow_force_pushes: false`, `allow_deletions: false`); (7)
  conversation resolution required (`required_conversation_resolution`);
  (8) stale reviews dismissed on new commits (`dismiss_stale_reviews`);
  (9) review from non-code-owners does not count
  (`require_code_owner_reviews`); (10) bypass only for defined bot
  accounts (`bypass_pull_request_allowances`). GitHub: Settings >
  Branches > Branch protection rules; via API `gh api -X PUT
  repos/{owner}/{repo}/branches/main/protection` with JSON
  configuration. Git: Project > Settings > Repository > Protected
  branches, plus push rules (`reject_unsigned_commits: true`,
  `commit_message_regex`). Bitbucket: Repository settings > Branch
  permissions. CODEOWNERS file in `.github/CODEOWNERS` or
  `.git/CODEOWNERS` (e.g. `/security/ @security-team @ciso`).
  Repository rulesets (GitHub Enterprise) for organisation-wide
  enforcement. Example JSON for GitHub: `{ "required_pull_request_reviews":
  { "required_approving_review_count": 2,
  "require_code_owner_reviews": true,
  "dismiss_stale_reviews": true }, "enforce_admins": true,
  "required_linear_history": true, "required_signatures": true }`.
  References: SLSA Source Track Level 3, OpenSSF Branch-Protection
  check, NIST SP 800-218 PW.7.
- **Akzeptanz / Acceptance:** Schutzregeln per Plattform-Screenshot
  oder API-Auszug (`gh api repos/.../branches/main/protection`) für
  jedes geschützte Repository nachweisbar; alle 10 Schutzregeln aktiv;
  CODEOWNERS-Datei vorhanden und aktuell. / Protection rules
  evidenced via platform screenshot or API extract (`gh api
  repos/.../branches/main/protection`) for every protected repository;
  all 10 protection rules active; CODEOWNERS file present and
  up to date.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-05: Signierte Commits / Signed Commits

- **DE:** Commits werden mit GPG, S/MIME oder SSH signiert. Verifizierte
  Commits sind als Branch-Schutz-Voraussetzung gesetzt
  (`required_signatures: true`). SSH-Signaturen seit Git 2.34 bevorzugt
  (einfacher als GPG, gleiche Hardware-Keys nutzbar). Konfiguration
  Beispiele: GPG mit `gpg --full-generate-key` (Ed25519 oder RSA-4096),
  `git config --global user.signingkey 0xKEYID`, `git config --global
  commit.gpgsign true`, `git config --global tag.gpgsign true`; Public
  Key auf GitHub/Git hochgeladen (Settings > SSH and GPG keys).
  S/MIME mit X.509-Zertifikat, `git config --global gpg.format x509`,
  `git config --global gpg.x509.program /usr/local/bin/smimesign`. SSH
  mit Ed25519-Schlüssel, `git config --global gpg.format ssh`,
  `git config --global user.signingkey ~/.ssh/id_ed25519.pub`,
  `git config --global gpg.ssh.allowedSignersFile ~/.config/git/allowed_signers`;
  Public Key in `allowed_signers`-Datei (`E-Mail ssh-ed25519 AAAA...`).
  Hardware-Token (YubiKey) als Schlüsselträger: GPG mit `gpg --card-edit`
  + `keytocard`; SSH mit `ssh-keygen -t ed25519-sk` (FIDO2-resident); per
  Tap signieren. Mit Sigstore `gitsign` ist passwortlose, kurzlebige
  OIDC-basierte Signatur möglich (keine lokalen Schlüssel) — empfohlen für
  Cloud-CI. Tag-Signing für Releases zwingend (`git tag -s v1.0.0`).
  Verifikation: `git log --show-signature`; GitHub-Badge `Verified` neben
  Commit; Git-Badge `Verified` und `Signed by`. CI prüft Signaturen
  zusätzlich (`git verify-commit HEAD`). Schlüssel-Rotation alle 24
  Monate; abgelaufene Schlüssel werden in `revocation-list.md`
  dokumentiert. Referenzen: SLSA Source Track Level 3, GitHub Commit
  Signature Verification, BSI TR-03109.
- **EN:** Commits are signed with GPG, S/MIME, or SSH. Verified commits
  are required by branch protection (`required_signatures: true`). SSH
  signatures preferred since Git 2.34 (simpler than GPG, can use same
  hardware keys). Configuration examples: GPG with `gpg
  --full-generate-key` (Ed25519 or RSA-4096), `git config --global
  user.signingkey 0xKEYID`, `git config --global commit.gpgsign true`,
  `git config --global tag.gpgsign true`; public key uploaded to
  GitHub/Git (Settings > SSH and GPG keys). S/MIME with X.509
  certificate, `git config --global gpg.format x509`, `git config
  --global gpg.x509.program /usr/local/bin/smimesign`. SSH with Ed25519
  key, `git config --global gpg.format ssh`, `git config --global
  user.signingkey ~/.ssh/id_ed25519.pub`, `git config --global
  gpg.ssh.allowedSignersFile ~/.config/git/allowed_signers`; public key
  in `allowed_signers` file (`email ssh-ed25519 AAAA...`). Hardware
  tokens (YubiKey) as key carrier: GPG with `gpg --card-edit` +
  `keytocard`; SSH with `ssh-keygen -t ed25519-sk` (FIDO2 resident);
  sign by tap. With Sigstore `gitsign`, passwordless short-lived
  OIDC-based signing is possible (no local keys) — recommended for
  cloud CI. Tag signing for releases mandatory (`git tag -s v1.0.0`).
  Verification: `git log --show-signature`; GitHub `Verified` badge
  next to commit; Git `Verified` and `Signed by` badges. CI
  additionally verifies signatures (`git verify-commit HEAD`). Key
  rotation every 24 months; expired keys documented in
  `revocation-list.md`. References: SLSA Source Track Level 3, GitHub
  Commit Signature Verification, BSI TR-03109.
- **Akzeptanz / Acceptance:** Signaturen für ≥ 95 % der Commits in
  geschützten Branches sichtbar (`Verified` Badge in der Plattform);
  Branch-Schutz-Regel `required_signatures` aktiv; Schlüssel- und
  Zertifikatsverwaltung dokumentiert. / Signatures visible for ≥ 95 %
  of commits in protected branches (`Verified` badge in platform);
  branch protection rule `required_signatures` active; key and
  certificate management documented.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-06: Secret-Scanning vor Push / Secret Scanning Before Push

- **DE:** Pre-Commit-, Pre-Push- und Server-Side-Hooks blockieren
  Geheimnisse vor Veröffentlichung. Lokale Pre-Commit-Hooks per
  `.pre-commit-config.yaml`: gitleaks, trufflehog, detect-secrets,
  ggshield (GitGuardian). Beispiel: `repos: - repo: https://github.com/
  gitleaks/gitleaks rev: v8.x hooks: - id: gitleaks - repo:
  https://github.com/Yelp/detect-secrets rev: v1.5.0 hooks: - id:
  detect-secrets args: ['--baseline', '.secrets.baseline']`. Server-Side:
  GitHub Advanced Security (GHAS) Secret Scanning + Push Protection
  (verhindert Push bekannter Secret-Patterns wie AWS Keys, GitHub PATs,
  Stripe Keys, Slack Tokens — über 200 Provider unterstützt); Git
  Secret Detection (Free) und Secret Push Protection (Ultimate);
  Bitbucket Secret Scanning; Azure DevOps Credential Scanner.
  Selbstgehostet: gitleaks im CI (`gitleaks detect --redact --report-format
  sarif --report-path leaks.sarif`); trufflehog (`trufflehog filesystem
  . --no-update --json`). Custom Patterns für interne Secrets
  (`.gitleaks.toml`): `[[rules]] id = "org-internal-token" regex =
  '''org_[a-z0-9]{32}'''`. Ausnahmen über `.gitleaksignore` mit Hash
  und Begründung. Bei Treffer: sofortige Rotation des Geheimnisses,
  History-Bereinigung mit `git filter-repo --replace-text` (nur nach
  Team-Abstimmung, da Force-Push), Audit-Log-Prüfung,
  Incident-Ticket. Pre-Push-Hook im Repository als ausführbare
  Datei `.git/hooks/pre-push` mit Pattern-Check (siehe `home-baseline`-
  Repository `scripts/hooks/pre-push`). Quartalsweiser Scan der
  gesamten Git-Historie mit `gitleaks detect --log-opts="--all"`.
  Referenzen: OWASP Secrets Management Cheat Sheet, NIST SP 800-218
  PW.6.
- **EN:** Pre-commit, pre-push, and server-side hooks block secrets
  before publication. Local pre-commit hooks via
  `.pre-commit-config.yaml`: gitleaks, trufflehog, detect-secrets,
  ggshield (GitGuardian). Example: `repos: - repo: https://github.com/
  gitleaks/gitleaks rev: v8.x hooks: - id: gitleaks - repo:
  https://github.com/Yelp/detect-secrets rev: v1.5.0 hooks: - id:
  detect-secrets args: ['--baseline', '.secrets.baseline']`. Server
  side: GitHub Advanced Security (GHAS) Secret Scanning + Push
  Protection (prevents push of known secret patterns like AWS keys,
  GitHub PATs, Stripe keys, Slack tokens — over 200 providers
  supported); Git Secret Detection (Free) and Secret Push Protection
  (Ultimate); Bitbucket Secret Scanning; Azure DevOps Credential
  Scanner. Self-hosted: gitleaks in CI (`gitleaks detect --redact
  --report-format sarif --report-path leaks.sarif`); trufflehog
  (`trufflehog filesystem . --no-update --json`). Custom patterns for
  internal secrets (`.gitleaks.toml`): `[[rules]] id =
  "org-internal-token" regex = '''org_[a-z0-9]{32}'''`. Exceptions
  via `.gitleaksignore` with hash and justification. On hit:
  immediate rotation of the secret, history cleanup with `git
  filter-repo --replace-text` (only after team coordination, as it is
  force-push), audit-log review, incident ticket. Pre-push hook in
  repository as executable file `.git/hooks/pre-push` with pattern
  check (see `home-baseline` repository `scripts/hooks/pre-push`).
  Quarterly scan of entire git history with `gitleaks detect
  --log-opts="--all"`. References: OWASP Secrets Management Cheat
  Sheet, NIST SP 800-218 PW.6.
- **Akzeptanz / Acceptance:** Hook-Skripte sichtbar im Repository
  (`.pre-commit-config.yaml`, `.git/hooks/pre-push`); aktuelle
  Scan-Logs (≤ 7 Tage); Server-Side Push Protection aktiv;
  Vollhistorien-Scan vierteljährlich. / Hook scripts visible in
  repository (`.pre-commit-config.yaml`, `.git/hooks/pre-push`);
  current scan logs (≤ 7 days); server-side push protection
  active; quarterly full-history scan.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-07: Geheimnis-Speicher / Secret Store

- **DE:** Lokal werden Geheimnisse im System-Schlüsselspeicher gehalten:
  macOS Keychain (`security add-generic-password -a $USER -s servicename
  -w`); Windows Credential Manager (`cmdkey /add:server /user:user
  /pass:secret`, PowerShell `Get-Credential` und `Export-Clixml`
  benutzerverschlüsselt); Linux GNOME Keyring oder KDE KWallet via
  `secret-tool` und `libsecret`; CLI-übergreifend `pass` (auf GPG-
  Verschlüsselung) oder `keepassxc-cli`. Niemals in `.env`-Dateien
  ungeschützt im Klartext (`.env` per `.gitignore` ausschließen, lokale
  Dateien per Festplattenverschlüsselung schützen). Im Build dienen
  Secret-Manager: HashiCorp Vault (Self-Hosted oder HCP) mit
  AppRole/Kubernetes/JWT-Auth, Dynamic Secrets, Secret Engines
  (KV-v2, PKI, Database, Transit); AWS Secrets Manager und AWS Systems
  Manager Parameter Store mit IAM-Rollen; Azure Key Vault mit Managed
  Identities; GCP Secret Manager mit Workload Identity Federation;
  Kubernetes Secrets mit Sealed Secrets (Bitnami) oder External Secrets
  Operator; SOPS (Mozilla) für GitOps mit AGE/PGP/KMS-Verschlüsselung
  am Ruheort. CI/CD-Plattform-Secrets: GitHub Actions Encrypted Secrets
  + Environment Secrets (mit Required Reviewers), GitHub OIDC für
  passwortlosen Cloud-Zugriff (`permissions: id-token: write`); Git
  CI/CD Variables (Masked + Protected + File-Type); Azure DevOps Variable
  Groups + Azure Key Vault Integration; Jenkins Credentials Plugin mit
  Vault-Plugin. Geheimnisse niemals in Logs (Maskierung erzwingen),
  niemals als CLI-Argumente (`ps`-Liste sichtbar), nur per ENV oder
  Pipe. Rotation: 90 Tage für statische Secrets, sofort bei
  Personalwechsel. Referenzen: OWASP Secrets Management Cheat Sheet,
  NIST SP 800-57, BSI TR-02102-2.
- **EN:** Locally, secrets live in the system keystore: macOS Keychain
  (`security add-generic-password -a $USER -s servicename -w`); Windows
  Credential Manager (`cmdkey /add:server /user:user /pass:secret`,
  PowerShell `Get-Credential` and `Export-Clixml` user-encrypted);
  Linux GNOME Keyring or KDE KWallet via `secret-tool` and
  `libsecret`; CLI-cross `pass` (on GPG encryption) or `keepassxc-cli`.
  Never in `.env` files unprotected in plaintext (exclude `.env` via
  `.gitignore`, protect local files via full-disk encryption). In CI/CD,
  use secret managers: HashiCorp Vault (self-hosted or HCP) with
  AppRole/Kubernetes/JWT auth, dynamic secrets, secret engines (KV-v2,
  PKI, Database, Transit); AWS Secrets Manager and AWS Systems Manager
  Parameter Store with IAM roles; Azure Key Vault with managed
  identities; GCP Secret Manager with Workload Identity Federation;
  Kubernetes Secrets with Sealed Secrets (Bitnami) or External Secrets
  Operator; SOPS (Mozilla) for GitOps with AGE/PGP/KMS encryption at
  rest. CI/CD platform secrets: GitHub Actions Encrypted Secrets +
  Environment Secrets (with required reviewers), GitHub OIDC for
  passwordless cloud access (`permissions: id-token: write`); Git
  CI/CD variables (masked + protected + file-type); Azure DevOps
  variable groups + Azure Key Vault integration; Jenkins Credentials
  Plugin with Vault plugin. Secrets never in logs (enforce masking),
  never as CLI arguments (visible in `ps` list), only via ENV or pipe.
  Rotation: 90 days for static secrets, immediately on staff change.
  References: OWASP Secrets Management Cheat Sheet, NIST SP 800-57,
  BSI TR-02102-2.
- **Akzeptanz / Acceptance:** Konfiguration je Umgebung in
  `docs/security/secrets-architecture.md` dokumentiert (lokal:
  Keychain/Credential Manager/Keyring; CI: Vault/KMS/Cloud Secret
  Manager); kein Geheimnis in `.env`-Datei oder Quellcode; Rotation
  alle 90 Tage. / Configuration per environment documented in
  `docs/security/secrets-architecture.md` (local: Keychain/Credential
  Manager/Keyring; CI: Vault/KMS/cloud secret manager); no secret in
  `.env` file or source code; rotation every 90 days.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-08: CI/CD-Pipeline-Härtung / CI/CD Pipeline Hardening

- **DE:** Pipelines laufen mit minimalen Rechten (Least Privilege) in
  isolierten Runnern. GitHub Actions: Job-Level `permissions:` Block mit
  `contents: read` als Default; nur explizit erweitern (z. B. `id-token:
  write` für OIDC, `packages: write` für GHCR-Push). Git: Job-Level
  `id_tokens:` und `secrets:` mit Vault-OIDC; Protected Branches und
  Protected Variables. Isolierte Runner: ephemerale Single-Use-Runner pro
  Job (GitHub Actions ARC mit Kubernetes, Git Auto-Scaling Runner mit
  Docker-Machine), keine Shared State zwischen Jobs; bevorzugt Container-
  Runner (Linux Container, Windows Container) statt Bare-Metal;
  Confidential Computing für hochsensitive Builds. Reusable Workflows /
  CI Components versioniert mit Tag oder Commit-SHA pinnen, niemals
  `@main` oder `@latest` (`uses: org/repo/.github/workflows/build.yml@
  abc123def`); GitHub Allowed Actions auf Allowlist beschränken (Settings >
  Actions > General > Allow specified actions). Netzwerk-Egress
  kontrolliert: Outbound-Firewall im Runner-VPC mit Allowlist (z. B.
  Maven Central, npm Registry, GitHub-API, Cloud-APIs); ausgehende
  Verbindungen zu unbekannten Hosts blockiert; Step Security Harden-
  Runner Action (`step-security/harden-runner@v2 with: egress-policy:
  audit` zum Lernen, dann `block`); StepSecurity oder Datadog für
  Detection. Pipeline-Definitionen sind versioniert: `.github/workflows/`,
  `.git-ci.yml`, `azure-pipelines.yml`, `Jenkinsfile` im Repository
  unter Branch-Schutz; Änderungen per PR mit Code-Owner-Review;
  CODEOWNERS für Pipeline-Dateien (`*.yml @devops-team`). Secrets-Zugriff
  per OIDC-Federation (kein Long-Lived Cloud-Credential): GitHub OIDC zu
  AWS IAM Role, Azure Federated Credentials, GCP Workload Identity. Build
  Provenance (SLSA Build L3): `slsa-framework/slsa-github-generator` für
  Attestations. Referenzen: SLSA Build Track L3, OpenSSF SCM Best
  Practices, NIST SP 800-218 PO.5.
- **EN:** Pipelines run with least privilege in isolated runners. GitHub
  Actions: job-level `permissions:` block with `contents: read` as
  default; only explicitly extend (e.g. `id-token: write` for OIDC,
  `packages: write` for GHCR push). Git: job-level `id_tokens:` and
  `secrets:` with Vault OIDC; protected branches and protected
  variables. Isolated runners: ephemeral single-use runners per job
  (GitHub Actions ARC with Kubernetes, Git auto-scaling runner with
  Docker Machine), no shared state between jobs; prefer container
  runners (Linux container, Windows container) over bare metal;
  Confidential Computing for highly sensitive builds. Reusable
  workflows / CI components versioned with tag or commit SHA pin,
  never `@main` or `@latest` (`uses:
  org/repo/.github/workflows/build.yml@abc123def`); GitHub Allowed
  Actions restricted to allowlist (Settings > Actions > General > Allow
  specified actions). Network egress controlled: outbound firewall in
  runner VPC with allowlist (e.g. Maven Central, npm Registry, GitHub
  API, cloud APIs); outbound connections to unknown hosts blocked; Step
  Security Harden-Runner action (`step-security/harden-runner@v2 with:
  egress-policy: audit` to learn, then `block`); StepSecurity or
  Datadog for detection. Pipeline definitions are versioned:
  `.github/workflows/`, `.git-ci.yml`, `azure-pipelines.yml`,
  `Jenkinsfile` in repository under branch protection; changes via PR
  with code-owner review; CODEOWNERS for pipeline files (`*.yml
  @devops-team`). Secret access via OIDC federation (no long-lived
  cloud credentials): GitHub OIDC to AWS IAM role, Azure federated
  credentials, GCP Workload Identity. Build provenance (SLSA Build
  L3): `slsa-framework/slsa-github-generator` for attestations.
  References: SLSA Build Track L3, OpenSSF SCM best practices, NIST SP
  800-218 PO.5.
- **Akzeptanz / Acceptance:** Runner-Profile (ephemeral, isolated) und
  Egress-Regeln (Allowlist) benannt; `permissions:` mit
  Least-Privilege in jedem Job; OIDC-Federation für Cloud-Zugriff;
  Pipeline-Dateien unter Branch-Schutz mit CODEOWNERS. / Runner
  profiles (ephemeral, isolated) and egress rules (allowlist) named;
  `permissions:` with least privilege in every job; OIDC federation
  for cloud access; pipeline files under branch protection with
  CODEOWNERS.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-09: Reproducible-CI / Reproducible CI

- **DE:** Build- und Testumgebungen werden aus Container-Images oder
  versionierten Konfigurationen aufgebaut. Container-Images mit Digest-
  Pinning (`FROM ubuntu:22.04@sha256:abc123...`); Multi-Stage-Build für
  schlanke Final-Images; Distroless-Images (`gcr.io/distroless/static-
  debian12`) oder Chainguard Images für minimalen Angriffsfläche; OCI-
  Image-Signatur mit cosign. Toolchain-Versionen versioniert in
  `.tool-versions` (asdf), `mise.toml`, `.python-version`, `.nvmrc`,
  `rust-toolchain.toml`, `go.mod` mit `go 1.22.x` Direktive. Devcontainer-
  Definition (`.devcontainer/devcontainer.json`) für reproduzierbare
  Entwicklungsumgebungen lokal und in Codespaces. Konfigurations-
  Management (Infrastructure as Code): Terraform/OpenTofu mit State-
  Locking, Pulumi, Ansible, Chef, Puppet — alle versioniert; `terraform
  fmt` und `terraform validate` in CI; `tfsec`, `checkov`, `kics`,
  `terrascan` als IaC-Security-Scanner. Drift wird erkannt: Terraform
  `terraform plan` täglich gegen Live-State (`drift detection` in
  Terraform Cloud, OpenTofu, Spacelift); AWS Config Rules; Azure Policy
  Compliance; GCP Asset Inventory; Open Policy Agent (OPA) mit Conftest
  für Policy-as-Code; HashiCorp Sentinel. Beispiel GitHub Action für
  Drift Detection: `name: Drift Detection on: schedule: - cron: '0 6 *
  * *'`. Reproducible Builds: deterministische Build-Ergebnisse durch
  fixierte Toolchain, `SOURCE_DATE_EPOCH`, Verifikation mit
  `diffoscope`. Referenzen: SLSA Build L3, NIST SP 800-218 PS.3, ISO/IEC
  27001 A.8.32.
- **EN:** Build and test environments come from container images or
  versioned configurations. Container images with digest pinning
  (`FROM ubuntu:22.04@sha256:abc123...`); multi-stage build for slim
  final images; distroless images (`gcr.io/distroless/static-
  debian12`) or Chainguard images for minimal attack surface; OCI
  image signature with cosign. Toolchain versions versioned in
  `.tool-versions` (asdf), `mise.toml`, `.python-version`, `.nvmrc`,
  `rust-toolchain.toml`, `go.mod` with `go 1.22.x` directive.
  Devcontainer definition (`.devcontainer/devcontainer.json`) for
  reproducible development environments locally and in Codespaces.
  Configuration management (Infrastructure as Code): Terraform/OpenTofu
  with state locking, Pulumi, Ansible, Chef, Puppet — all versioned;
  `terraform fmt` and `terraform validate` in CI; `tfsec`, `checkov`,
  `kics`, `terrascan` as IaC security scanners. Drift detected:
  Terraform `terraform plan` daily against live state (drift detection
  in Terraform Cloud, OpenTofu, Spacelift); AWS Config Rules; Azure
  Policy Compliance; GCP Asset Inventory; Open Policy Agent (OPA) with
  Conftest for policy as code; HashiCorp Sentinel. Example GitHub
  Action for drift detection: `name: Drift Detection on: schedule: -
  cron: '0 6 * * *'`. Reproducible builds: deterministic build
  results via fixed toolchain, `SOURCE_DATE_EPOCH`, verification with
  `diffoscope`. References: SLSA Build L3, NIST SP 800-218 PS.3,
  ISO/IEC 27001 A.8.32.
- **Akzeptanz / Acceptance:** Container-Image-Tags mit Digest gepinnt;
  IaC-Konfiguration unter Versionierung; tägliches Drift-Detection-
  Job mit Report; reproducible-build-fähige Toolchain-Pinning. /
  Container image tags pinned with digest; IaC configuration under
  version control; daily drift-detection job with report; reproducible-
  build capable toolchain pinning.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-10: Trennung der Umgebungen / Environment Separation

- **DE:** Entwicklung, Test, Vorabnahme und Produktion sind in allen
  Dimensionen getrennt: (1) Daten — separate Datenbanken, kein Prod-Daten
  in Dev/Test (siehe Item 11); (2) Berechtigungen — eigene Identity-
  Provider-Gruppen, RBAC-Rollen pro Stage, JIT-Access (Just-in-Time) für
  Production; (3) Netzwerke — getrennte VPCs/VNets/Projects mit Network
  Policies, keine direkte Konnektivität; (4) Cloud-Konten — separate AWS
  Accounts (AWS Organizations OU pro Stage), Azure Subscriptions, GCP
  Projects; (5) DNS und Endpoints — getrennte Domains
  (`dev.example.com`, `staging.example.com`, `example.com`); (6) Secrets
  — eigene Vault-Mounts oder Key Vaults pro Stage; (7) Build-Artefakte
  — getrennte Container-Registries oder Pfade (`registry.example.com/
  dev/`, `staging/`, `prod/`). Promotion-Prozess: Artefakte werden vom
  Test über Staging zu Production befördert (kein Re-Build), digital
  signiert und attestiert (cosign + SLSA Provenance). Manuelle Approval-
  Gates für Production-Deployment (GitHub Environments mit Required
  Reviewers; Git Manual Jobs; Argo CD Sync Policies). Production-
  Zugang nur per PIM (Privileged Identity Management) mit zeitlich
  begrenzter Eskalation und Begründung. Beispielarchitektur: Dev-VPC
  10.10.0.0/16, Staging-VPC 10.20.0.0/16, Prod-VPC 10.30.0.0/16; keine
  VPC-Peering zwischen Dev und Prod. Audit-Trail aller Stage-Übergänge.
  Referenzen: NIST SP 800-218 PO.5, ISO/IEC 27001 A.8.31, ENISA Cloud
  Security Guide.
- **EN:** Development, test, staging, and production are separated in
  all dimensions: (1) data — separate databases, no prod data in
  dev/test (see Item 11); (2) permissions — separate identity provider
  groups, RBAC roles per stage, JIT (Just-in-Time) access for
  production; (3) networks — separate VPCs/VNets/projects with network
  policies, no direct connectivity; (4) cloud accounts — separate AWS
  accounts (AWS Organizations OU per stage), Azure subscriptions, GCP
  projects; (5) DNS and endpoints — separate domains
  (`dev.example.com`, `staging.example.com`, `example.com`); (6)
  secrets — separate Vault mounts or Key Vaults per stage; (7) build
  artefacts — separate container registries or paths
  (`registry.example.com/dev/`, `staging/`, `prod/`). Promotion process:
  artefacts promoted from test through staging to production (no
  rebuild), digitally signed and attested (cosign + SLSA provenance).
  Manual approval gates for production deployment (GitHub Environments
  with required reviewers; Git manual jobs; Argo CD sync policies).
  Production access only via PIM (Privileged Identity Management) with
  time-limited elevation and justification. Example architecture: Dev
  VPC 10.10.0.0/16, Staging VPC 10.20.0.0/16, Prod VPC 10.30.0.0/16; no
  VPC peering between dev and prod. Audit trail of all stage
  transitions. References: NIST SP 800-218 PO.5, ISO/IEC 27001 A.8.31,
  ENISA Cloud Security Guide.
- **Akzeptanz / Acceptance:** Trennung im Architekturbild
  (`docs/architecture/environments.md`) beschrieben mit Diagramm
  (Mermaid oder C4); separate Cloud-Konten/VPCs/Identity-Gruppen pro
  Stage; Manual Approval Gate für Production-Deployment; Audit-Trail
  aller Promotions. / Separation described in architecture diagram
  (`docs/architecture/environments.md`) with diagram (Mermaid or C4);
  separate cloud accounts/VPCs/identity groups per stage; manual
  approval gate for production deployment; audit trail of all
  promotions.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-11: Schutz von Testdaten / Test Data Protection

- **DE:** Echtdaten (insbesondere personenbezogene Daten gemäß DSGVO Art.
  4 Nr. 1, Gesundheitsdaten, Finanzdaten) werden in Test- und
  Entwicklungsumgebungen nur in anonymisierter (DSGVO ErwGr. 26) oder
  pseudonymisierter Form verwendet. Anonymisierungstechniken:
  Generalisation (z. B. Geburtsjahr statt -datum), Suppression (Löschen
  einzelner Werte), k-Anonymität, l-Diversität, t-Closeness,
  Differential Privacy. Pseudonymisierung mit kryptografischem Hash
  (HMAC-SHA-256 mit Schlüssel im Vault), Tokenisierung, Format-
  Preserving Encryption (FPE-FF1/FF3 nach NIST SP 800-38G). Synthetische
  Testdaten bevorzugt — Tools: Faker (Python `faker`, JS `@faker-js/
  faker`, Java `datafaker`, .NET `Bogus`); Mockaroo (web); Synthea
  (Healthcare-Daten); GAN-basierte Synthese (Gretel.ai, MOSTLY AI,
  YData) für statistisch repräsentative Synthese; SDV (Synthetic Data
  Vault). Datenmaskierung beim Daten-Refresh aus Production: Delphix,
  Informatica TDM, IBM InfoSphere Optim, Open-Source `pgmasker` für
  PostgreSQL, `mysql_masking` für MariaDB. DSGVO-Compliance:
  Auftragsverarbeitungsvertrag (AV-Vertrag) bei externen Tools;
  Verarbeitungsverzeichnis nach Art. 30; Löschkonzept gemäß Art. 17;
  Datenschutz-Folgenabschätzung (DSFA) nach Art. 35 für hochriskante
  Verarbeitungen. Verfahren dokumentiert in
  `docs/security/test-data-policy.md` mit Datenklassifikation, Maskie-
  rungsregel pro Tabelle/Feld, Refresh-Frequenz, Verantwortlichen.
  Audit-Trail jedes Refreshs. Bei Verstoß sofortige Eskalation an DSB
  und Incident Response. Referenzen: DSGVO Art. 4, 25, 32, 35; ISO/IEC
  27018; ENISA Pseudonymisation Techniques and Best Practices; BSI TR-
  03161.
- **EN:** Real data (especially personal data per GDPR Art. 4(1),
  health data, financial data) is used in test and development
  environments only in anonymised (GDPR Recital 26) or pseudonymised
  form. Anonymisation techniques: generalisation (e.g. birth year
  instead of date), suppression (deletion of individual values),
  k-anonymity, l-diversity, t-closeness, differential privacy.
  Pseudonymisation with cryptographic hash (HMAC-SHA-256 with key in
  vault), tokenisation, format-preserving encryption (FPE-FF1/FF3 per
  NIST SP 800-38G). Synthetic test data preferred — tools: Faker
  (Python `faker`, JS `@faker-js/faker`, Java `datafaker`, .NET
  `Bogus`); Mockaroo (web); Synthea (healthcare data); GAN-based
  synthesis (Gretel.ai, MOSTLY AI, YData) for statistically
  representative synthesis; SDV (Synthetic Data Vault). Data masking on
  data refresh from production: Delphix, Informatica TDM, IBM
  InfoSphere Optim, open-source `pgmasker` for PostgreSQL,
  `mysql_masking` for MariaDB. GDPR compliance: data processing
  agreement (DPA) with external tools; processing register per Art. 30;
  deletion concept per Art. 17; data protection impact assessment
  (DPIA) per Art. 35 for high-risk processing. Procedure documented in
  `docs/security/test-data-policy.md` with data classification, masking
  rule per table/field, refresh frequency, owner. Audit trail of every
  refresh. On violation: immediate escalation to DPO and incident
  response. References: GDPR Art. 4, 25, 32, 35; ISO/IEC 27018; ENISA
  Pseudonymisation Techniques and Best Practices; BSI TR-03161.
- **Akzeptanz / Acceptance:** `docs/security/test-data-policy.md` mit
  Maskierungsregel pro Tabelle und Feld; synthetische oder
  anonymisierte Testdaten in Dev/Test; AV-Vertrag und DSFA bei
  Bedarf; jährliche Wirksamkeitsprüfung. /
  `docs/security/test-data-policy.md` with masking rule per table and
  field; synthetic or anonymised test data in dev/test; DPA and DPIA
  where applicable; annual effectiveness review.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-12: Audit-Logs der Plattform / Platform Audit Logs

- **DE:** Quellcode-Plattform, Build-System und Cloud erzeugen Audit-
  Logs für alle sicherheitsrelevanten Ereignisse: Anmeldungen, Repo-
  Zugriffe, Permission-Änderungen, Branch-Schutz-Bypass, Secret-Zugriffe,
  Pipeline-Ausführungen, Cloud-API-Calls, IAM-Änderungen.
  Plattform-Quellen: GitHub Audit Log (Org Settings > Audit Log; per API
  `gh api orgs/{org}/audit-log`); GitHub Enterprise Cloud `audit_log_api`
  Streaming nach Splunk/Datadog/Azure Monitor/AWS S3; Git Audit
  Events (`Admin > Monitoring > Audit Events`, API
  `/api/v4/audit_events`); Bitbucket Cloud Audit Logs; Azure DevOps
  Auditing (Project Settings > Auditing); AWS CloudTrail (Multi-Region
  Trail mit S3-Zielort, KMS-Verschlüsselung, Log File Validation aktiv);
  Azure Monitor / Log Analytics + Microsoft Sentinel; GCP Cloud Audit
  Logs (Admin Activity, Data Access, System Event, Policy Denied).
  Zentralisierung: SIEM-Integration (Splunk Enterprise Security,
  Microsoft Sentinel, IBM QRadar, Elastic Security, Sumo Logic) mit
  korrelations-Regeln für Anomalie-Erkennung. Schutz gegen Änderung:
  Write-Once-Read-Many (WORM) Storage — AWS S3 Object Lock im
  Compliance-Mode (kann auch Root nicht löschen) für ≥ 1 Jahr; Azure Blob
  Immutable Storage; GCP Bucket Lock; Hash-Chain mit täglichem Anchoring
  in TSA (Time-Stamping Authority); Kryptografische Signatur jedes Logs
  mit Vault Transit Engine. Aufbewahrung: ≥ 1 Jahr operativ, 10 Jahre
  für CRA-relevante Vorfälle (CRA Art. 14), 6 Jahre für DSGVO-relevante
  Verarbeitungen, 10 Jahre für SOX-Pflichten falls anwendbar. Zugriff
  per Need-to-Know; Audit-Log-Zugriff selbst geloggt
  (Meta-Audit). Beispiel CloudTrail-Konfiguration: `{ "IsMultiRegionTrail":
  true, "IncludeGlobalServiceEvents": true, "EnableLogFileValidation":
  true, "KmsKeyId": "..." }`. Referenzen: NIST SP 800-92, ISO/IEC 27001
  A.8.15, A.8.34, BSI IT-Grundschutz OPS.1.1.5, CRA Art. 14.
- **EN:** Source platform, build system, and cloud produce audit logs
  for all security-relevant events: logins, repo access, permission
  changes, branch protection bypass, secret access, pipeline
  executions, cloud API calls, IAM changes. Platform sources: GitHub
  Audit Log (Org Settings > Audit Log; via API `gh api
  orgs/{org}/audit-log`); GitHub Enterprise Cloud `audit_log_api`
  streaming to Splunk/Datadog/Azure Monitor/AWS S3; Git Audit Events
  (`Admin > Monitoring > Audit Events`, API `/api/v4/audit_events`);
  Bitbucket Cloud audit logs; Azure DevOps auditing (Project Settings
  > Auditing); AWS CloudTrail (multi-region trail with S3 target, KMS
  encryption, log file validation active); Azure Monitor / Log
  Analytics + Microsoft Sentinel; GCP Cloud Audit Logs (Admin Activity,
  Data Access, System Event, Policy Denied). Centralisation: SIEM
  integration (Splunk Enterprise Security, Microsoft Sentinel, IBM
  QRadar, Elastic Security, Sumo Logic) with correlation rules for
  anomaly detection. Protection against modification: Write-Once-Read-
  Many (WORM) storage — AWS S3 Object Lock in compliance mode (cannot
  even be deleted by root) for ≥ 1 year; Azure Blob Immutable Storage;
  GCP Bucket Lock; hash chain with daily anchoring in TSA (Time-
  Stamping Authority); cryptographic signature of every log with Vault
  Transit engine. Retention: ≥ 1 year operational, 10 years for
  CRA-relevant incidents (CRA Art. 14), 6 years for GDPR-relevant
  processing, 10 years for SOX duties if applicable. Access on
  need-to-know basis; access to audit log itself logged (meta-audit).
  Example CloudTrail configuration: `{ "IsMultiRegionTrail": true,
  "IncludeGlobalServiceEvents": true, "EnableLogFileValidation": true,
  "KmsKeyId": "..." }`. References: NIST SP 800-92, ISO/IEC 27001
  A.8.15, A.8.34, BSI IT-Grundschutz OPS.1.1.5, CRA Art. 14.
- **Akzeptanz / Acceptance:** Aufbewahrungsfrist (≥ 1 Jahr operativ,
  10 Jahre CRA) und Schutzmaßnahme (WORM, Object Lock, Hash-Chain)
  in `docs/security/audit-log-retention.md` benannt; SIEM-Integration
  aktiv; Zugriff geloggt; jährliche Integritätsprüfung. / Retention
  period (≥ 1 year operational, 10 years CRA) and protection measure
  (WORM, Object Lock, hash chain) named in
  `docs/security/audit-log-retention.md`; SIEM integration active;
  access logged; annual integrity check.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-13: Zugriffsrezertifizierung / Access Recertification

- **DE:** Mindestens halbjährlich werden Zugriffe auf Repositories,
  Build-Systeme, Secret-Stores, Cloud-Konten und Produktivumgebungen
  überprüft (Access Recertification, User Access Review nach ISO/IEC
  27001 A.5.18). Trigger für ad-hoc-Rezertifizierung: Personalwechsel,
  Rollenwechsel, Sicherheitsvorfall, M&A, Audit-Befund. Verfahren: (1)
  IAM-System exportiert je User: Repository-Berechtigung, Build-System-
  Rolle, Vault-Policy, Cloud-IAM-Rolle, Group-Mitgliedschaft; (2)
  Manager bestätigt oder widerruft je Zugriff (`grant`/`revoke`); (3)
  inaktive Konten (kein Login ≥ 90 Tage) automatisch deaktiviert; (4)
  verwaiste Konten (z. B. ausgeschiedener Mitarbeiter) sofort entfernt;
  (5) Service-Account-Credentials rotiert und Begründung dokumentiert;
  (6) Bericht an Security Lead mit Übersicht Granted/Revoked/Pending/Expired.
  Tools: Microsoft Entra ID Access Reviews (Quartalsweise konfigurierbar
  mit Self-Review oder Manager-Review); Okta Lifecycle Management +
  Access Certification; SailPoint IdentityNow; ServiceNow GRC; Saviynt;
  HashiCorp Vault `vault token list` + `vault list auth/`; AWS IAM
  Access Analyzer Last-Accessed; Azure RBAC Activity-Log-Analyse; GitHub
  Audit-Log für inaktive Mitglieder; Git Inactive User Cleanup.
  Automatische Deprovisionierung bei HR-Workflow-Trigger (Workday,
  SAP SuccessFactors → SCIM → Okta/Entra ID). Beispiel-Skript: `az ad
  user list --query "[?accountEnabled && signInActivity.lastSignInDateTime
  < '2025-12-01']"`. Privileged-Access-Reviews (Admins, Production-
  Deploy, Secret-Rotate) monatlich oder bei jeder PIM-Eskalation.
  Referenzen: ISO/IEC 27001 A.5.18, NIST SP 800-53 AC-2, BSI ORP.4,
  SOX Sec. 404.
- **EN:** At least every six months, access to repositories, build
  systems, secret stores, cloud accounts, and production environments
  is reviewed (access recertification, user access review per ISO/IEC
  27001 A.5.18). Triggers for ad-hoc recertification: staff change,
  role change, security incident, M&A, audit finding. Procedure: (1)
  IAM system exports per user: repository permission, build system
  role, Vault policy, cloud IAM role, group membership; (2) manager
  confirms or revokes each access (`grant`/`revoke`); (3) inactive
  accounts (no login ≥ 90 days) automatically disabled; (4) orphaned
  accounts (e.g. departed employee) removed immediately; (5)
  service-account credentials rotated and justification documented;
  (6) report to Security Lead with overview granted/revoked/pending/expired.
  Tools: Microsoft Entra ID Access Reviews (quarterly configurable
  with self-review or manager review); Okta Lifecycle Management +
  Access Certification; SailPoint IdentityNow; ServiceNow GRC;
  Saviynt; HashiCorp Vault `vault token list` + `vault list auth/`;
  AWS IAM Access Analyzer last-accessed; Azure RBAC activity log
  analysis; GitHub audit log for inactive members; Git Inactive
  User Cleanup. Automatic deprovisioning on HR workflow trigger
  (Workday, SAP SuccessFactors → SCIM → Okta/Entra ID). Example
  script: `az ad user list --query "[?accountEnabled &&
  signInActivity.lastSignInDateTime < '2025-12-01']"`. Privileged
  access reviews (admins, production deploy, secret rotate) monthly
  or on every PIM elevation. References: ISO/IEC 27001 A.5.18, NIST SP
  800-53 AC-2, BSI ORP.4, SOX Sec. 404.
- **Akzeptanz / Acceptance:** Letzter Rezertifizierungsbericht ≤ 6
  Monate alt, mit Übersicht aller Plattformen, Granted/Revoked-Anzahl
  und Security-Sign-off; automatische Deaktivierung inaktiver Konten;
  Privileged-Access monatlich. / Last recertification report ≤ 6
  months old, with overview of all platforms, granted/revoked count,
  and security sign-off; automatic deactivation of inactive accounts;
  privileged access monthly.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-14: Endpoint-Schutz / Endpoint Protection

- **DE:** Entwickler-Geräte haben aktuelle OS- und Anwendungs-Patches
  (kritische Patches innerhalb 7 Tagen, hohe innerhalb 30 Tagen
  installiert; Auto-Update aktiviert wo möglich), aktive Endpoint-
  Sicherheitslösungen (Endpoint Detection and Response, EDR — Microsoft
  Defender for Endpoint, CrowdStrike Falcon, SentinelOne, Sophos
  Intercept X, Trend Micro Apex One; mit Tamper-Protection,
  Echtzeit-Scanning, Cloud-Lookup); aktive Host-Firewall (macOS Application
  Firewall + pf, Windows Defender Firewall, Linux nftables/ufw); aktive
  DNS-Filter (Cisco Umbrella, Cloudflare for Teams, Quad9); USB-Storage-
  Restriktion per MDM. Mobile Device Management (MDM) mit Compliance-
  Policies: Microsoft Intune, Jamf Pro, Workspace ONE, Kandji, Mosyle.
  Konformitätsregeln: OS-Version min., Festplattenverschlüsselung,
  Bildschirmsperre, Patch-Level, EDR aktiv, kein Jailbreak/Root.
  Conditional Access (Entra ID): Geräte ohne Konformität dürfen nicht
  auf Source-Code-Plattform oder Cloud zugreifen. Zentrales Inventar
  per CMDB (Microsoft Configuration Manager, Jamf, ServiceNow CMDB,
  Snipe-IT, Ralph) mit Felder: Gerätetyp, Seriennummer, Hostname,
  Eigentümer, OS-Version, EDR-Status, Verschlüsselungsstatus, Patch-
  Datum, Garantieende. Asset-Inventar gemäß ISO/IEC 27001 A.5.9.
  Regelmäßige Schwachstellen-Scans (z. B. Tenable Nessus,
  Qualys VMDR, Microsoft Defender Vulnerability Management, Rapid7
  InsightVM) für Endpoints. Bei Geräteverlust/-diebstahl Remote Wipe
  per MDM (`Microsoft Endpoint Manager > Devices > Remote actions >
  Wipe`); Offboarding-Prozess mit Geräterückgabe und Wipe-Bestätigung.
  Patch-Reporting per WSUS, Microsoft Endpoint Manager, Jamf, AWS
  Systems Manager Patch Manager, Ansible. Referenzen: NIST SP 800-128,
  ISO/IEC 27001 A.5.9, A.8.7, A.8.8, BSI IT-Grundschutz SYS.1.1, SYS.2.1.
- **EN:** Developer devices have current OS and application patches
  (critical patches installed within 7 days, high within 30 days;
  auto-update enabled where possible), active endpoint security
  solutions (Endpoint Detection and Response, EDR — Microsoft Defender
  for Endpoint, CrowdStrike Falcon, SentinelOne, Sophos Intercept X,
  Trend Micro Apex One; with tamper protection, real-time scanning,
  cloud lookup); active host firewall (macOS Application Firewall + pf,
  Windows Defender Firewall, Linux nftables/ufw); active DNS filter
  (Cisco Umbrella, Cloudflare for Teams, Quad9); USB storage
  restriction via MDM. Mobile Device Management (MDM) with compliance
  policies: Microsoft Intune, Jamf Pro, Workspace ONE, Kandji, Mosyle.
  Compliance rules: minimum OS version, full-disk encryption, screen
  lock, patch level, EDR active, no jailbreak/root. Conditional Access
  (Entra ID): non-compliant devices may not access source platform or
  cloud. Central inventory via CMDB (Microsoft Configuration Manager,
  Jamf, ServiceNow CMDB, Snipe-IT, Ralph) with fields: device type,
  serial number, hostname, owner, OS version, EDR status, encryption
  status, patch date, warranty end. Asset inventory per ISO/IEC 27001
  A.5.9. Regular vulnerability scans (e.g. Tenable Nessus, Qualys
  VMDR, Microsoft Defender Vulnerability Management, Rapid7 InsightVM)
  for endpoints. On device loss/theft remote wipe via MDM (`Microsoft
  Endpoint Manager > Devices > Remote actions > Wipe`); offboarding
  process with device return and wipe confirmation. Patch reporting
  via WSUS, Microsoft Endpoint Manager, Jamf, AWS Systems Manager
  Patch Manager, Ansible. References: NIST SP 800-128, ISO/IEC 27001
  A.5.9, A.8.7, A.8.8, BSI IT-Grundschutz SYS.1.1, SYS.2.1.
- **Akzeptanz / Acceptance:** CMDB-Inventar-Auszug mit allen
  Entwickler-Geräten und Status-Feldern; ≥ 95 % Compliance-Quote;
  Patch-SLA: kritisch ≤ 7 Tage, hoch ≤ 30 Tage; EDR aktiv auf 100 %
  der Endpunkte. / CMDB inventory extract with all developer devices
  and status fields; ≥ 95 % compliance rate; patch SLA: critical
  ≤ 7 days, high ≤ 30 days; EDR active on 100 % of endpoints.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-15: Datensicherung und Wiederherstellung / Backup and Recovery

- **DE:** Quellcode, Konfiguration (IaC, Pipeline-Definitionen,
  Secret-Schemata), Build-Artefakte (Container-Images, Binaries),
  CI/CD-Konfiguration, Vault-Snapshots und Datenbank-Schemata werden
  regelmäßig gesichert nach 3-2-1-Regel (3 Kopien, 2 Medien, 1 offsite).
  Backup-Quellen: GitHub-Repos per `gh repo clone --bare` oder
  GitHub Backup (Backblaze, Rewind, BackHub); Git Backup
  (`git-backup create` oder Geo-Replikation für Self-Hosted);
  Bitbucket Backup; Azure DevOps mit Continuous Export; Container-
  Registries (ECR, GCR, ACR, GHCR) per `crane` oder `skopeo` Mirror in
  Cold-Storage; Vault per `vault operator raft snapshot save`; Cloud
  Resources per Terraform-State (verschlüsselt im Backend mit
  Versionierung) und Backup-Tools (AWS Backup, Azure Backup, Velero
  für Kubernetes). Backup-Storage: verschlüsselt am Ruheort
  (AES-256 mit eigenem KMS-Key), separater Cloud-Account/Region für
  Offsite; Object Lock / Immutable Storage gegen Ransomware; Versionierung
  und Versions-Aufbewahrung 90 Tage täglich, 12 Monate monatlich, 7
  Jahre jährlich. Wiederherstellungs-Ziele: RTO (Recovery Time
  Objective) Quellcode 4 Stunden, Build-System 8 Stunden, Cloud-
  Infrastruktur 24 Stunden; RPO (Recovery Point Objective) Quellcode
  1 Stunde, Build-Artefakte 24 Stunden. Wiederherstellungstests
  mindestens jährlich (besser quartalsweise) per Game-Day: vollständige
  Wiederherstellung in isolierter Umgebung, dokumentiert mit Start-/End-
  Zeit, Vollständigkeitsprüfung, Hash-Vergleich, RTO/RPO-Validierung.
  Beispiel-Skript: `gh repo list org --json name --jq '.[].name' |
  xargs -I {} gh repo clone org/{} --mirror`. Disaster-Recovery-Plan
  in `docs/security/disaster-recovery.md` mit Runbook, Verantwortlichen,
  Eskalationskette. Tests dokumentieren Lessons Learned. Referenzen:
  ISO/IEC 27001 A.8.13, NIST SP 800-34, BSI IT-Grundschutz CON.3
  (Datensicherungskonzept), CRA Anhang I Teil II 2.1 lit. b.
- **EN:** Source code, configuration (IaC, pipeline definitions,
  secret schemas), build artefacts (container images, binaries),
  CI/CD configuration, Vault snapshots, and database schemas are
  regularly backed up following the 3-2-1 rule (3 copies, 2 media,
  1 offsite). Backup sources: GitHub repos via `gh repo clone --bare`
  or GitHub Backup (Backblaze, Rewind, BackHub); Git Backup
  (`git-backup create` or Geo replication for self-hosted);
  Bitbucket Backup; Azure DevOps with continuous export; container
  registries (ECR, GCR, ACR, GHCR) via `crane` or `skopeo` mirror to
  cold storage; Vault via `vault operator raft snapshot save`; cloud
  resources via Terraform state (encrypted in backend with versioning)
  and backup tools (AWS Backup, Azure Backup, Velero for Kubernetes).
  Backup storage: encrypted at rest (AES-256 with own KMS key),
  separate cloud account/region for offsite; Object Lock / immutable
  storage against ransomware; versioning and version retention 90 days
  daily, 12 months monthly, 7 years yearly. Recovery objectives: RTO
  (Recovery Time Objective) source code 4 hours, build system 8 hours,
  cloud infrastructure 24 hours; RPO (Recovery Point Objective) source
  code 1 hour, build artefacts 24 hours. Recovery tests at least
  yearly (better quarterly) via game day: full restore in isolated
  environment, documented with start/end time, completeness check,
  hash comparison, RTO/RPO validation. Example script: `gh repo list
  org --json name --jq '.[].name' | xargs -I {} gh repo clone org/{}
  --mirror`. Disaster recovery plan in
  `docs/security/disaster-recovery.md` with runbook, owners, escalation
  chain. Tests document lessons learned. References: ISO/IEC 27001
  A.8.13, NIST SP 800-34, BSI IT-Grundschutz CON.3 (Backup concept),
  CRA Annex I Part II 2.1 lit. b.
- **Akzeptanz / Acceptance:** Letzter erfolgreicher
  Wiederherstellungstest ≤ 12 Monate alt, mit Datum, Umfang,
  Erfolgskriterien und Lessons Learned in
  `docs/security/disaster-recovery-tests.md`; 3-2-1-Backup-Strategie
  aktiv; RTO/RPO-Werte definiert. / Last successful recovery test
  ≤ 12 months old, with date, scope, success criteria, and lessons
  learned in `docs/security/disaster-recovery-tests.md`; 3-2-1 backup
  strategy active; RTO/RPO values defined.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-16: Schulung / Training

- **DE:** Alle Entwickler erhalten regelmäßig verpflichtende Schulungen
  zu sicherer Entwicklung und sicherem Umgang mit der Toolchain.
  Pflicht-Themen: Secure Coding (OWASP Top 10 2025, CWE Top 25,
  sprachspezifische Sicherheitspraktiken: Java/Spring Security,
  C#/.NET, Python/PyCA, Go, JavaScript/TypeScript), Threat Modeling
  (STRIDE-Methodik, Microsoft Threat Modeling Tool, OWASP Threat Dragon,
  IriusRisk), Datenschutz (DSGVO Art. 25 „Privacy by Design", Art. 32
  technische und organisatorische Maßnahmen, Datenschutz-Folgenabschätzung
  Art. 35), CRA-Bewusstsein (Verordnung (EU) 2024/2847, Hersteller-Pflichten
  Art. 13/14, Konformitätsbewertung Module A/B+C/H, Schwachstellenmeldung
  binnen 24 h Art. 14(2)), Geheimnis-Hygiene (Pre-Commit-Hooks gitleaks,
  GitHub Push Protection, Secret Manager Vault/AWS Secrets Manager/Azure
  Key Vault), Social Engineering und Phishing (Vishing, Smishing, Spear
  Phishing, Business Email Compromise, KnowBe4-Simulationen mindestens
  vierteljährlich), Supply-Chain-Sicherheit (typosquatting, dependency
  confusion, SLSA-Levels, SBOM CycloneDX/SPDX, signierte Commits, Sigstore
  cosign), IDE- und KI-Tool-Sicherheit (GitHub Copilot Business
  `excludeFiles`, JetBrains AI Telemetrie, Plugin-Risiken), sichere
  Konfiguration und Härtung. Schulungsformate: verpflichtendes
  Onboarding-Sicherheitstraining vor erstem Repository-Zugriff (4 h),
  jährliche Auffrischung (mindestens 2 h, dokumentiert), Just-in-Time
  Mikro-Trainings (5–15 min Videos zu aktuellen Vorfällen, neuen CVEs,
  Tool-Updates), Capture-the-Flag-Übungen (CTF, mindestens halbjährlich),
  Tabletop-Übungen mit Incident-Response-Team (Ransomware-, Lieferketten-,
  Insider-Bedrohungs-Szenarien). Schulungs-Plattformen: SecureFlag, Kontra
  Application Security, Avatao, Hack The Box, OWASP WebGoat, OWASP Juice
  Shop, PortSwigger Web Security Academy, eigene Inhalte in LMS (Moodle,
  TalentLMS, SAP SuccessFactors Learning, Cornerstone OnDemand, Workday
  Learning). Empfohlene Zertifizierungen je Rolle: ISC2 SSCP/CSSLP, OffSec
  OSCP/OSWE, GIAC GSSP-JAVA/.NET/PYTHON, iSAQB CPSA-A, CompTIA Security+/
  CySA+, ISC2 CISSP für Architekten, Certified Ethical Hacker (CEH),
  Microsoft SC-100 Cybersecurity Architect Expert. Schulungs-Matrix je
  Rolle: Junior Developer (Onboarding 4 h + jährlich 2 h Refresher + 4 CTF
  pro Jahr), Senior Developer (zusätzlich Threat-Modeling-Workshop 8 h
  jährlich, eine Zertifizierung im Jahr 1), Tech Lead/Architect
  (zusätzlich CSSLP oder iSAQB CPSA-A, Mentoring von Junioren),
  DevOps/Platform Engineer (zusätzlich Cloud-Sicherheit AWS Security
  Specialty/AZ-500/GCP Professional Cloud Security Engineer, Kubernetes
  CKS), Security Champion (zusätzlich 16 h Vertiefung pro Jahr,
  Bug-Bounty-Teilnahme, OffSec-Zertifizierung). Nachweisführung in
  HR-System (Workday, SAP SuccessFactors, Personio) oder LMS mit
  automatischer Eskalation bei Überschreitung der Frist (30 Tage
  Nachfrist, dann Sperrung des Repository-Zugriffs via SCIM-Deprovisioning
  in Entra ID/Okta). Jahresbericht an Security Lead mit Erfüllungsquote pro Team,
  Trend gegenüber Vorjahr, Lessons Learned aus CTF und Tabletops. Referenzen:
  NIST SP 800-50 (Building an Information Technology Security Awareness
  and Training Program), NIST SP 800-181 (NICE Workforce Framework),
  ISO/IEC 27001 A.6.3 (Informationssicherheitsbewusstsein, -ausbildung
  und -training), BSI IT-Grundschutz ORP.3 (Sensibilisierung und Schulung),
  CRA Anhang I Teil II 2.1 (Awareness empfohlen).
- **EN:** All developers receive regular mandatory training on secure
  development and safe use of the toolchain. Mandatory topics: Secure
  Coding (OWASP Top 10 2025, CWE Top 25, language-specific practices:
  Java/Spring Security, C#/.NET, Python/PyCA cryptography, Go,
  JavaScript/TypeScript), Threat Modeling (STRIDE methodology, Microsoft
  Threat Modeling Tool, OWASP Threat Dragon, IriusRisk), Data Protection
  (GDPR Art. 25 Privacy by Design, Art. 32 technical and organizational
  measures, Data Protection Impact Assessment Art. 35), CRA Awareness
  (Regulation (EU) 2024/2847, manufacturer obligations Art. 13/14,
  conformity assessment Modules A/B+C/H, vulnerability reporting within
  24 h Art. 14(2)), Secret Hygiene (pre-commit hooks gitleaks, GitHub
  Push Protection, secret managers Vault/AWS Secrets Manager/Azure Key
  Vault), Social Engineering and Phishing (vishing, smishing, spear
  phishing, business email compromise, KnowBe4 simulations at least
  quarterly), Supply Chain Security (typosquatting, dependency confusion,
  SLSA levels, SBOM CycloneDX/SPDX, signed commits, Sigstore cosign), IDE
  and AI Tool Security (GitHub Copilot Business `excludeFiles`, JetBrains
  AI telemetry, plugin risks), secure configuration and hardening.
  Training formats: mandatory onboarding security training before first
  repository access (4 h), annual refresher (at least 2 h, documented),
  just-in-time micro-trainings (5–15 min videos on current incidents, new
  CVEs, tool updates), capture-the-flag exercises (CTF, at least
  semi-annually), tabletop exercises with incident response team
  (ransomware, supply chain, insider threat scenarios). Training
  platforms: SecureFlag, Kontra Application Security, Avatao, Hack The
  Box, OWASP WebGoat, OWASP Juice Shop, PortSwigger Web Security Academy,
  custom content in LMS (Moodle, TalentLMS, SAP SuccessFactors Learning,
  Cornerstone OnDemand, Workday Learning). Recommended certifications by
  role: ISC2 SSCP/CSSLP, OffSec OSCP/OSWE, GIAC GSSP-JAVA/.NET/PYTHON,
  iSAQB CPSA-A, CompTIA Security+/CySA+, ISC2 CISSP for architects,
  Certified Ethical Hacker (CEH), Microsoft SC-100 Cybersecurity
  Architect Expert. Training matrix by role: Junior Developer (onboarding
  4 h + annual 2 h refresher + 4 CTFs per year), Senior Developer
  (additionally threat modeling workshop 8 h annually, one certification
  in year 1), Tech Lead/Architect (additionally CSSLP or iSAQB CPSA-A,
  mentoring of juniors), DevOps/Platform Engineer (additionally cloud
  security AWS Security Specialty/AZ-500/GCP Professional Cloud Security
  Engineer, Kubernetes CKS), Security Champion (additionally 16 h
  in-depth per year, bug bounty participation, OffSec certification).
  Proof in HR system (Workday, SAP SuccessFactors, Personio) or LMS with
  automatic escalation on deadline overrun (30-day grace period, then
  repository access suspension via SCIM deprovisioning in Entra ID/Okta).
  Annual report to Security Lead with completion rate per team, trend versus prior
  year, lessons learned from CTFs and tabletops. References: NIST SP
  800-50 (Building an Information Technology Security Awareness and
  Training Program), NIST SP 800-181 (NICE Workforce Framework), ISO/IEC
  27001 A.6.3 (Information security awareness, education and training),
  BSI IT-Grundschutz ORP.3 (Sensibilisierung und Schulung), CRA Annex I
  Part II 2.1 (awareness recommended).
- **Akzeptanz / Acceptance:** Schulungsmatrix in `docs/security/training-matrix.md`
  je Rolle, Erfüllungsquote ≥ 95 % je Quartal in HR-System/LMS abrufbar,
  Onboarding-Training vor Repository-Zugriff abgeschlossen, jährliche
  Refresher und CTF/Tabletop-Übungen dokumentiert, Jahresbericht an Security Lead
  mit Trend und Lessons Learned vorhanden.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

#### CL-10-17: Cross-Platform-Skriptparität / Cross-Platform Script Parity

- **DE:** Kritische Skript-Werkzeuge, die Entwicklungs-, Build-,
  Prüf-, SBOM-, Audit- oder Bootstrap-Aufgaben ausführen, werden für
  macOS/Linux und Windows gleichwertig bereitgestellt oder ausdrücklich
  als plattformspezifisch begründet. Standard ist ein Bash-Skript für
  macOS/Linux und ein PowerShell-7+-Skript für Windows. Beide Varianten
  haben gleiche Argumente, gleiche Defaults, gleiche Exitcodes, gleiches
  Ausgabeformat, gleiche Nebenwirkungen und gleichwertigen Trockenlauf
  (`--dry-run` in Bash, `-WhatIf` in PowerShell).
- **EN:** Critical script tools that perform development, build, review,
  SBOM, audit, or bootstrap tasks are provided equivalently for
  macOS/Linux and Windows or explicitly justified as platform-specific.
  The default is a Bash script for macOS/Linux and a PowerShell 7+ script
  for Windows. Both variants have the same arguments, same defaults, same
  exit codes, same output format, same side effects, and equivalent dry-run
  behaviour (`--dry-run` in Bash, `-WhatIf` in PowerShell).
- **Akzeptanz / Acceptance:** Für jedes kritische Skript gibt es eine
  Paritätsprüfung mit Dateipfaden, getesteten Plattformen, Help-Oberflächen
  und N/A-Begründung. Bash-Skripte haben `-h`/`--help` und bei dauerhafter
  Nutzung eine Unix-Manpage unter `docs/man/`. PowerShell-Skripte haben
  deutsch/englische comment-based help, `Get-Help`-Nachweis und bei Cmdlets
  ein genehmigtes `Verb-Noun`-Namensschema. / For every critical script
  there is a parity check with file paths, tested platforms, help surfaces,
  and N/A rationale. Bash scripts have `-h`/`--help` and, for permanent
  use, a Unix man page under `docs/man/`. PowerShell scripts have German/
  English comment-based help, `Get-Help` evidence, and for cmdlets an
  approved `Verb-Noun` naming scheme.
- **Referenz / Reference:** Spec-Kit `cross-platform-governance`,
  Template `script-parity-checklist-template`.
- **Lernstufe / Learning stage:** _Grundlage, Aufbau oder Vertiefung gemaess Lernpfad dokumentieren. / Record foundation, intermediate, or advanced according to the learning path._
- **Verantwortliche Rolle / Responsible role:** _Lernende Person, ausbildende Person, Projektverantwortung oder Security-Rolle benennen. / Name learner, instructor, project owner, or security role._
- **Anwendbarkeit / Applicability:**
  - [ ] `Applicable`
  - [ ] `N/A`
  - [ ] `Open`
- **Umsetzungsstatus / Implementation status:**
  - [ ] `Fulfilled`
  - [ ] `Partly Fulfilled`
  - [ ] `Not Fulfilled`
  - [ ] `Not Assessed`
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Skriptpfade, Paritätscheck, Manpage, Get-Help-Ausgabe, Testnachweis oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name script paths, parity check, man page, Get-Help output, test evidence, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind und die Belege im
Sicherheits- oder Qualitätsmanagement-System auffindbar sind. Kritische
Skripte sind plattformparitätisch geprüft oder begründet nicht anwendbar.

**EN:** Fulfilled when every item is closed and the evidence is findable
in the security or quality management system. Critical scripts have
platform-parity evidence or justified non-applicability.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-10-glossar-full-disk-encryption"></a>

#### Festplattenverschlüsselung / Full-Disk Encryption

- **DE:** Festplattenverschlüsselung schützt Daten auf einem Gerät, wenn es verloren geht oder gestohlen wird. Der Schutz wirkt vor allem bei ausgeschaltetem oder gesperrtem Gerät.
- **EN:** Full-disk encryption protects data on a device if it is lost or stolen. It mainly protects when the device is powered off or locked.

<a id="cl-10-glossar-mfa"></a>

#### MFA

- **DE:** MFA bedeutet Anmeldung mit mehreren Faktoren. Neben Passwort oder Schlüssel wird ein weiterer Nachweis verlangt, zum Beispiel App-Bestätigung oder Hardware-Token.
- **EN:** MFA means login with multiple factors. In addition to password or key, another proof is required, for example app approval or hardware token.

<a id="cl-10-glossar-ide"></a>

#### IDE

- **DE:** Eine IDE ist eine Entwicklungsumgebung, zum Beispiel VS Code oder IntelliJ. Sie sollte sicher konfiguriert und nicht mit unnötigen Erweiterungen belastet sein.
- **EN:** An IDE is a development environment, for example VS Code or IntelliJ. It should be securely configured and not overloaded with unnecessary extensions.

<a id="cl-10-glossar-branch-protection"></a>

#### Branch-Schutz / Branch Protection

- **DE:** Branch-Schutz verhindert unkontrollierte Änderungen an wichtigen Branches. Typische Regeln verlangen Reviews, erfolgreiche Checks und eingeschränkte Schreibrechte.
- **EN:** Branch protection prevents uncontrolled changes to important branches. Typical rules require reviews, passing checks, and restricted write access.

<a id="cl-10-glossar-signed-commits"></a>

#### Signierte Commits / Signed Commits

- **DE:** Signierte Commits enthalten einen kryptografischen Nachweis der Urheberschaft. Sie helfen, Manipulation und falsche Identitäten zu erkennen.
- **EN:** Signed commits contain cryptographic proof of authorship. They help detect tampering and false identities.

<a id="cl-10-glossar-gpg"></a>

#### GPG

- **DE:** GPG ist ein Werkzeug für Signaturen und Verschlüsselung. In Git kann es genutzt werden, um Commits oder Tags zu signieren.
- **EN:** GPG is a tool for signatures and encryption. In Git it can be used to sign commits or tags.

<a id="cl-10-glossar-sigstore-cosign"></a>

#### Sigstore / Cosign

- **DE:** Sigstore ist ein Werkzeug-Ökosystem für Signaturen und Nachweise. Cosign wird oft genutzt, um Container-Images oder Artefakte zu signieren.
- **EN:** Sigstore is a tool ecosystem for signatures and evidence. Cosign is often used to sign container images or artefacts.

<a id="cl-10-glossar-secret-scanning"></a>

#### Secret Scanning

- **DE:** Secret Scanning sucht versehentlich veröffentlichte Geheimnisse, zum Beispiel Tokens oder Passwörter. Es sollte vor Push und in der Plattform laufen.
- **EN:** Secret scanning searches for accidentally published secrets, for example tokens or passwords. It should run before push and in the platform.

<a id="cl-10-glossar-secret-store"></a>

#### Secret Store

- **DE:** Ein Secret Store speichert Geheimnisse wie Passwörter, API-Schlüssel oder Tokens geschützt. Geheimnisse sollen nicht im Code, in Logs oder in Tickets stehen.
- **EN:** A secret store protects secrets such as passwords, API keys, or tokens. Secrets should not be in code, logs, or tickets.

<a id="cl-10-glossar-ci-cd"></a>

#### CI/CD

- **DE:** CI/CD automatisiert Bauen, Testen und Ausliefern von Software. Weil diese Pipeline Artefakte erzeugt, muss sie besonders geschützt und nachvollziehbar sein.
- **EN:** CI/CD automates building, testing, and delivering software. Because this pipeline creates artefacts, it must be especially protected and traceable.

<a id="cl-10-glossar-environment-separation"></a>

#### Umgebungstrennung / Environment Separation

- **DE:** Umgebungstrennung hält Entwicklung, Test und Produktion getrennt. Dadurch gelangen Testdaten, Experimente oder unsichere Einstellungen nicht in Produktion.
- **EN:** Environment separation keeps development, test, and production apart. This prevents test data, experiments, or unsafe settings from reaching production.

<a id="cl-10-glossar-test-data"></a>

#### Testdaten / Test Data

- **DE:** Testdaten sind Daten für Entwicklung und Tests. Personenbezogene Echtdaten dürfen nur genutzt werden, wenn es erlaubt, begründet und geschützt ist.
- **EN:** Test data is data for development and testing. Real personal data may only be used when it is allowed, justified, and protected.

<a id="cl-10-glossar-audit-trail"></a>

#### Audit-Spur / Audit Trail

- **DE:** Eine Audit-Spur zeigt nachvollziehbar, wer was wann getan oder entschieden hat. Sie hilft bei Prüfung, Fehlersuche und Verantwortung.
- **EN:** An audit trail traceably shows who did or decided what and when. It helps with review, troubleshooting, and accountability.

<a id="cl-10-glossar-access-recertification"></a>

#### Zugriffsrezertifizierung / Access Recertification

- **DE:** Zugriffsrezertifizierung ist die regelmäßige Prüfung, ob Berechtigungen noch nötig sind. Nicht mehr benötigte Zugriffe werden entfernt.
- **EN:** Access recertification is the regular review of whether permissions are still needed. Unneeded access is removed.

<a id="cl-10-glossar-endpoint-protection"></a>

#### Endpoint-Schutz / Endpoint Protection

- **DE:** Endpoint-Schutz schützt Geräte wie Laptops oder Server. Beispiele sind Malware-Schutz, Härtung, Updates und Überwachung.
- **EN:** Endpoint protection protects devices such as laptops or servers. Examples are malware protection, hardening, updates, and monitoring.

<a id="cl-10-glossar-backup-restore"></a>

#### Backup und Restore

- **DE:** Backup sichert Daten. Restore ist die Wiederherstellung. Für Sicherheit zählt, dass Wiederherstellung regelmäßig getestet wird.
- **EN:** Backup saves data. Restore is recovery. For security, it matters that recovery is tested regularly.

<a id="cl-10-glossar-cross-platform-script"></a>

#### Cross-Platform-Skript

- **DE:** Ein Cross-Platform-Skript funktioniert auf mehreren Betriebssystemen oder hat gleichwertige Varianten. Unterschiede müssen dokumentiert und getestet werden.
- **EN:** A cross-platform script works on several operating systems or has equivalent variants. Differences must be documented and tested.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-06-15):** Prüfpunkt 17 zur Cross-Platform-Skriptparität ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Added item 17 for cross-platform script parity; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.3 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.4 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
