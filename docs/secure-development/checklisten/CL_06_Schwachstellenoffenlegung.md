<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 06 – Schwachstellenoffenlegung / Vulnerability Disclosure

### Zweck / Purpose

**DE:** Diese Checkliste sichert ein einheitliches Verfahren zur Aufnahme,
Bewertung und Behebung gemeldeter Schwachstellen, einschließlich der
Pflichten aus dem EU Cyber Resilience Act (CRA).

**EN:** This checklist ensures a uniform process to receive, assess, and fix
reported vulnerabilities, including the obligations of the EU Cyber
Resilience Act (CRA).

### Geltungsbereich / Scope

**DE:** Pflicht für alle nach außen sichtbaren Dienste und für alle
auslieferbaren Produkte. Empfehlung für interne Werkzeuge.

**EN:** Mandatory for all externally visible services and for all shipped
products. Recommended for internal tooling.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- ISO/IEC 27002:2022 A.5.7, A.5.25–A.5.28, A.8.8
- RFC 9116 (security.txt)
- Coordinated Vulnerability Disclosure (CVD)
- Verordnung (EU) 2024/2847 (CRA)

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

**DE:** Eine externe Person meldet eine kritische Schwachstelle. Das Team bestaetigt den Eingang innerhalb der definierten Frist, bewertet mit CVSS, legt ein Ticket an, plant den Fix und dokumentiert nach Abschluss eine Lessons-Learned-Notiz.

**EN:** An external person reports a critical vulnerability. The team confirms receipt within the defined time, rates it with CVSS, creates a ticket, plans the fix, and documents a lessons-learned note after closure.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [CVD / Coordinated Vulnerability Disclosure](#cl-06-glossar-cvd)
- [security.txt](#cl-06-glossar-security-txt)
- [RFC](#cl-06-glossar-rfc)
- [CVE](#cl-06-glossar-cve)
- [CVSS](#cl-06-glossar-cvss)
- [EPSS](#cl-06-glossar-epss)
- [KEV](#cl-06-glossar-kev)
- [SLA](#cl-06-glossar-sla)
- [CRA / Cyber Resilience Act](#cl-06-glossar-cra)
- [DORA](#cl-06-glossar-dora)
- [CSAF](#cl-06-glossar-csaf)
- [VEX / Vulnerability Exploitability eXchange](#cl-06-glossar-vex)
- [Advisory / Sicherheitshinweis](#cl-06-glossar-advisory)
- [Patch](#cl-06-glossar-patch)
- [Evidenz / Evidence](#cl-06-glossar-evidenz)

### Checkliste / Checklist

#### 1. Veröffentlichte CVD-Richtlinie / Published CVD Policy

- **DE:** Eine kurze, öffentliche Richtlinie beschreibt, wie Schwachstellen
  gemeldet werden, was Melder erwarten können und welche Fristen gelten.
  Pflichtinhalte einer CVD-Richtlinie (Coordinated Vulnerability Disclosure):
  - **Scope (in/out)**: welche Produkte, Domains, Repositories sind im Umfang.
  - **Out-of-Scope**: explizit (z. B. Marketing-Website, Drittanbieter-Hosting).
  - **Meldekanal**: E-Mail, Formular, HackerOne/Bugcrowd, signed `security@`.
  - **Erwartete Inhalte einer Meldung**: Beschreibung, PoC, Auswirkung,
    betroffene Version, Reproduktionsschritte.
  - **Bestätigungsfrist** (z. B. 3 Werktage).
  - **Triage- und Reaktionszeiten** je Schweregrad.
  - **Safe-Harbor-Klausel**: rechtlicher Schutz für Melder bei gutgläubiger
    Forschung.
  - **Anerkennung**: Hall of Fame, optionale Veröffentlichung des Namens.
  - **Sprache**: Englisch + Deutsch oder weitere Sprachen.
  Etablierte Vorlagen und Standards:
  - [disclose.io Open Source Templates](https://disclose.io/).
  - **GitHub Security Policy** (`SECURITY.md`) – wird auf Repo-Tab automatisch
    angezeigt.
  - **ISO/IEC 29147:2018** „Vulnerability Disclosure" als Referenz.
  - **ISO/IEC 30111:2019** „Vulnerability Handling Processes" als Referenz.
  Veröffentlichungsorte:
  - Repository: `SECURITY.md` im Root.
  - Website: `https://example.org/security` (stabile URL).
  - `security.txt` verweist per `Policy:`-Feld auf die URL.
  - In Release-Notes oder README verlinken.
- **EN:** A short, public policy describes how to report vulnerabilities,
  what reporters can expect, and which timelines apply.
  Mandatory contents of a CVD policy (coordinated vulnerability disclosure):
  - **Scope (in/out)**: which products, domains, repositories are in scope.
  - **Out of scope**: explicit (e.g. marketing site, third-party hosting).
  - **Reporting channel**: e-mail, form, HackerOne/Bugcrowd, signed
    `security@`.
  - **Expected report contents**: description, PoC, impact, affected
    version, reproduction steps.
  - **Acknowledgement deadline** (e.g. 3 business days).
  - **Triage and response times** per severity.
  - **Safe-harbour clause**: legal protection for good-faith researchers.
  - **Acknowledgement**: hall of fame, optional name publication.
  - **Language**: English + German or further languages.
  Established templates and standards:
  - [disclose.io open-source templates](https://disclose.io/).
  - **GitHub Security Policy** (`SECURITY.md`) – shown automatically on
    the security tab.
  - **ISO/IEC 29147:2018** "Vulnerability Disclosure" as reference.
  - **ISO/IEC 30111:2019** "Vulnerability Handling Processes" as reference.
  Publication locations:
  - Repository: `SECURITY.md` at the root.
  - Website: `https://example.org/security` (stable URL).
  - `security.txt` references the URL via `Policy:`.
  - Linked from release notes or README.
- **Akzeptanz / Acceptance:** Richtlinie unter einem stabilen Pfad
  (z. B. `/security`, `SECURITY.md`) öffentlich erreichbar; alle Pflicht-
  inhalte vorhanden; Safe-Harbor-Klausel; mindestens DE und EN; verlinkt aus
  `security.txt` und Repository-Tab.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 2. security.txt nach RFC 9116 / security.txt per RFC 9116

- **DE:** Webdienste stellen `/.well-known/security.txt` bereit. Die Datei
  enthält Kontakt, Richtlinien-Link, Sprachen und Ablaufdatum.
  Pflichtfelder nach RFC 9116:
  - `Contact:` (mindestens eines, mehrfach erlaubt) – `mailto:`,
    `https://`, `tel:`.
  - `Expires:` (Pflicht, ISO 8601 Datum, max. ein Jahr in der Zukunft).
  - `Encryption:` – Link zum öffentlichen PGP-Schlüssel.
  - `Acknowledgments:` – Hall-of-Fame oder Danksagungen.
  - `Preferred-Languages:` – z. B. `de, en`.
  - `Canonical:` – kanonische URL der security.txt-Datei.
  - `Policy:` – URL zur ausführlichen CVD-Policy.
  - `Hiring:` – optionaler Link zu Stellenangeboten Security.
  - `CSAF:` – URL zur CSAF-Provider-Metadata-Datei.
  Beispiel `security.txt`:
  ```text
  Contact: mailto:security@example.org
  Contact: https://example.org/security/report
  Expires: 2027-04-27T00:00:00.000Z
  Encryption: https://example.org/.well-known/pgp-key.txt
  Preferred-Languages: de, en
  Canonical: https://example.org/.well-known/security.txt
  Policy: https://example.org/security/policy
  Acknowledgments: https://example.org/security/hall-of-fame
  ```
  Bereitstellung:
  - Pfad: **muss** `/.well-known/security.txt` sein (RFC 9116).
  - Über HTTPS, gültiges Zertifikat, kein Redirect auf eine andere Domain.
  - Optional: digitale Signatur (`security.txt.asc`) per GPG.
  Validierung:
  - Online-Validator: [securitytxt.org](https://securitytxt.org/).
  - CLI: `curl https://example.org/.well-known/security.txt`.
  - Monitoring: Alarm 60 Tage vor `Expires` (z. B. via Cron, GitHub Action
    `securitytxt-validator`).
- **EN:** Web services provide `/.well-known/security.txt`. The file lists
  contact, policy link, languages, and an expiry date.
  Mandatory fields per RFC 9116:
  - `Contact:` (at least one, multiple allowed) – `mailto:`, `https://`,
    `tel:`.
  - `Expires:` (mandatory, ISO 8601 date, max. one year in the future).
  - `Encryption:` – link to the public PGP key.
  - `Acknowledgments:` – hall of fame or thanks.
  - `Preferred-Languages:` – e.g. `de, en`.
  - `Canonical:` – canonical URL of the security.txt file.
  - `Policy:` – URL of the detailed CVD policy.
  - `Hiring:` – optional link to security job openings.
  - `CSAF:` – URL of the CSAF provider metadata file.
  Example `security.txt`:
  ```text
  Contact: mailto:security@example.org
  Contact: https://example.org/security/report
  Expires: 2027-04-27T00:00:00.000Z
  Encryption: https://example.org/.well-known/pgp-key.txt
  Preferred-Languages: de, en
  Canonical: https://example.org/.well-known/security.txt
  Policy: https://example.org/security/policy
  Acknowledgments: https://example.org/security/hall-of-fame
  ```
  Deployment:
  - Path: **must** be `/.well-known/security.txt` (RFC 9116).
  - Over HTTPS, valid certificate, no redirect to a different domain.
  - Optional: digital signature (`security.txt.asc`) via GPG.
  Validation:
  - Online validator: [securitytxt.org](https://securitytxt.org/).
  - CLI: `curl https://example.org/.well-known/security.txt`.
  - Monitoring: alert 60 days before `Expires` (e.g. via cron, GitHub
    Action `securitytxt-validator`).
- **Akzeptanz / Acceptance:** `security.txt` unter `/.well-known/security.txt`
  erreichbar mit allen Pflichtfeldern; `Expires` ≤ 1 Jahr; Erinnerungs-Job
  60 Tage vor Ablauf konfiguriert; PGP-Schlüssel für `Encryption:` gültig.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 3. Eingangskanal und Postfach / Reporting Channel and Mailbox

- **DE:** Es gibt eine überwachte Kontaktadresse oder ein Formular. Eingehende
  Meldungen werden innerhalb einer festgelegten Frist bestätigt.
  Empfohlene Eingangskanäle:
  - **Funktionspostfach** `security@<domain>` – mehrere Personen lesen
    mit, kein Single Point of Failure.
  - **Verschlüsselter Kanal**: PGP-Schlüssel auf `/.well-known/pgp-key.txt`
    veröffentlicht, Fingerprint in `security.txt`.
  - **HackerOne / Bugcrowd / Intigriti** – kommerzielle Plattformen mit
    Triage-Service.
  - **GitHub Private Vulnerability Reporting** (GitHub-Repos) – aktivieren
    in Settings → Code security.
  - **Git Confidential Issues** – mit Label `security`.
  - **Webformular** mit reCAPTCHA, Verschlüsselung serverseitig.
  Mailbox-Anforderungen:
  - Erreichbarkeit Mo–Fr 09:00–17:00 (mindestens), 24/7 für `critical`.
  - Eingang automatisiert in Ticket-System (z. B. OTRS, Jira Service Desk,
    Zammad) verlinkt.
  - Auto-Responder mit Bestätigung in DE/EN.
  - Eskalation an Bereitschaft bei `critical`-Schlüsselwörtern im Betreff.
  Bestätigungsfrist (Beispiele):
  - **3 Werktage** – Standard für Initial-ACK.
  - **24 h** – bei „aktiv ausgenutzt" / „critical".
  - **5 Werktage** – bei niedriger Komplexität, klarer Backlog.
  Vorlage Auto-Responder:
  ```text
  Vielen Dank für Ihre Sicherheitsmeldung. Wir haben Ihre Nachricht
  erhalten und melden uns innerhalb von 3 Werktagen mit einer
  qualifizierten Antwort. Bitte verschlüsseln Sie weitere Informationen
  mit unserem PGP-Schlüssel: https://example.org/.well-known/pgp-key.txt
  ```
- **EN:** A monitored contact address or form exists. Incoming reports are
  acknowledged within a defined deadline.
  Recommended reporting channels:
  - **Functional mailbox** `security@<domain>` – multiple people read,
    no single point of failure.
  - **Encrypted channel**: PGP key published at `/.well-known/pgp-key.txt`,
    fingerprint in `security.txt`.
  - **HackerOne / Bugcrowd / Intigriti** – commercial platforms with triage
    service.
  - **GitHub Private Vulnerability Reporting** (GitHub repos) – enable in
    Settings → Code security.
  - **Git Confidential Issues** – with label `security`.
  - **Web form** with reCAPTCHA, server-side encryption.
  Mailbox requirements:
  - Availability Mon–Fri 09:00–17:00 (at minimum), 24/7 for `critical`.
  - Inbound automatically linked into a ticket system (e.g. OTRS, Jira
    Service Desk, Zammad).
  - Auto-responder with acknowledgement in DE/EN.
  - Escalation to on-call for `critical` keywords in the subject.
  Acknowledgement deadline (examples):
  - **3 business days** – standard for initial ACK.
  - **24 h** – for "actively exploited" / "critical".
  - **5 business days** – for low-complexity issues with a clear backlog.
  Auto-responder template:
  ```text
  Thank you for your security report. We have received your message and
  will respond with a qualified reply within 3 business days. Please
  encrypt further information with our PGP key:
  https://example.org/.well-known/pgp-key.txt
  ```
- **Akzeptanz / Acceptance:** Kontakt (Funktionspostfach + Plattform-Channel)
  benannt; Bestätigungsfrist je Schweregrad in der CVD-Policy festgelegt
  (z. B. 24 h für critical, 3 Werktage Standard); Auto-Responder DE/EN
  aktiv; Eingang in Ticket-System verlinkt.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 4. Triage und Schweregrad / Triage and Severity

- **DE:** Jede Meldung wird klassifiziert (z. B. CVSS-Wert, intern definierte
  Stufen). Die Klassifikation steuert Reaktion und Frist.
  Bewertungsmethoden:
  - **CVSS v4.0** (Common Vulnerability Scoring System) – verbindliche
    Zielmethode für neue Bewertungen. CVSS v3.1 darf übergangsweise genutzt
    werden, wenn externe Quellen noch keinen v4.0-Score liefern.
    Tools: [FIRST CVSS Calculator](https://www.first.org/cvss/calculator/),
    `cvss-rs`.
    Beispiel-Vector: `CVSS:4.0/AV:N/AC:L/AT:N/PR:N/UI:N/VC:H/VI:H/VA:H/SC:H/SI:H/SA:H = 10.0 critical`.
  - **EPSS** (Exploit Prediction Scoring System) – Wahrscheinlichkeit
    der Ausnutzung in 30 Tagen, FIRST-Standard.
  - **CISA KEV** (Known Exploited Vulnerabilities) – sofortige Eskalation,
    wenn ein gemeldetes CVE in der KEV-Liste auftaucht.
  - **OWASP Risk Rating** – Likelihood × Impact (technisch + business).
  - **DREAD** als interner Ergänzungswert für Triage-Geschwindigkeit.
  Schwellwerte (Beispiel-Skala):
  - **Critical (CVSS 9.0–10.0)** oder CISA-KEV: SLA Bestätigung 24 h, Fix 7 Tage.
  - **High (CVSS 7.0–8.9)**: SLA Bestätigung 48 h, Fix 30 Tage.
  - **Medium (CVSS 4.0–6.9)**: SLA Bestätigung 3 Werktage, Fix 90 Tage.
  - **Low (CVSS < 4.0)**: SLA Bestätigung 5 Werktage, Fix nächster Release.
  Triage-Workflow:
  - Erstreview: ist die Meldung im Scope, reproduzierbar, eindeutig?
  - Duplikatscheck gegen offene Tickets.
  - CVSS-Score und CVSS-Vektor berechnen, in Ticket dokumentieren.
  - Bei Hochrisiko: Sofortbenachrichtigung Stakeholder + Bereitschaft.
  - Klassifikation in Ticket-Feld (z. B. `Severity: critical`, `CVSS: 9.8`).
  Werkzeuge:
  - Jira mit Custom-Feldern für CVSS-Vector, Severity, EPSS-Score.
  - GitHub Issues mit Labels `severity:critical` … `severity:low`.
  - Dependency-Track integriert CVSS automatisch aus NVD.
- **EN:** Every report is classified (e.g. CVSS score, internally defined
  levels). The classification drives response and deadline.
  Scoring methods:
  - **CVSS v4.0** (Common Vulnerability Scoring System) – mandatory target
    method for new ratings. CVSS v3.1 may be used during transition if
    external sources do not yet provide a v4.0 score.
    Tools: [FIRST CVSS Calculator](https://www.first.org/cvss/calculator/),
    `cvss-rs`.
    Example vector: `CVSS:4.0/AV:N/AC:L/AT:N/PR:N/UI:N/VC:H/VI:H/VA:H/SC:H/SI:H/SA:H = 10.0 critical`.
  - **EPSS** (Exploit Prediction Scoring System) – probability of
    exploitation in 30 days, FIRST standard.
  - **CISA KEV** (Known Exploited Vulnerabilities) – immediate escalation
    when a reported CVE appears on the KEV list.
  - **OWASP Risk Rating** – likelihood × impact (technical + business).
  - **DREAD** as internal supplementary value for triage speed.
  Thresholds (example scale):
  - **Critical (CVSS 9.0–10.0)** or CISA KEV: SLA ack 24 h, fix 7 days.
  - **High (CVSS 7.0–8.9)**: SLA ack 48 h, fix 30 days.
  - **Medium (CVSS 4.0–6.9)**: SLA ack 3 business days, fix 90 days.
  - **Low (CVSS < 4.0)**: SLA ack 5 business days, fix next release.
  Triage workflow:
  - First review: is the report in scope, reproducible, clear?
  - Duplicate check against open tickets.
  - Calculate CVSS score and CVSS vector, document both in the ticket.
  - For high risk: immediate stakeholder + on-call notification.
  - Classification in ticket field (e.g. `Severity: critical`, `CVSS: 9.8`).
  Tools:
  - Jira with custom fields for CVSS vector, severity, EPSS score.
  - GitHub Issues with labels `severity:critical` … `severity:low`.
  - Dependency-Track integrates CVSS automatically from NVD.
- **Akzeptanz / Acceptance:** Schwellwerte und Reaktionsfristen je Stufe
  schriftlich in der CVD-Policy oder Runbook; CVSS v4.0 als verbindliche
  Zielmethode; Score und Vektor im Ticket; Triage-Workflow im Ticket-System
  abgebildet; CISA-KEV-Watchlist aktiv.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 5. Reaktionsfristen / Response SLAs

- **DE:** Pro Schweregrad sind Maximalzeiten für Bestätigung, Fix und
  Veröffentlichung festgelegt (z. B. kritisch in 7 Tagen, hoch in 30 Tagen).
  Beispiel-SLA-Tabelle:
  | Stufe | Bestätigung | Triage | Fix | Disclosure |
  |---|---|---|---|---|
  | Critical (CVSS 9.0+) | 24 h | 48 h | 7 Tage | 14 Tage |
  | High (CVSS 7.0–8.9) | 48 h | 5 Werktage | 30 Tage | 60 Tage |
  | Medium (CVSS 4.0–6.9) | 3 Werktage | 10 Werktage | 90 Tage | 120 Tage |
  | Low (CVSS < 4.0) | 5 Werktage | 15 Werktage | nächster Release | 180 Tage |
  Externe Referenzen für SLA-Setzung:
  - **Project Zero** (Google) – 90-Tage-Regel + 30 Tage Grace.
  - **CERT/CC** – 45-Tage-Regel als Standard.
  - **ZDI** (Zero Day Initiative) – 120-Tage-Regel.
  - **Industriestandard**: 90 Tage Disclosure-Frist als verbreitete Norm.
  SLA-Überwachung:
  - Ticket-System mit SLA-Tracking (Jira SLA, ServiceNow SLA Engine).
  - Eskalations-Trigger bei 75 % der SLA-Frist.
  - Wöchentlicher Review offener Tickets durch Security-Team.
  - Quartalsweiser SLA-Compliance-Bericht für verantwortliche Leitung.
  Ausnahmen:
  - SLA-Verlängerung nur mit schriftlicher Begründung (z. B. Hersteller-
    Abhängigkeit, Komplexität, Restrisiko).
  - Zustimmung des Melders vor Fristverlängerung einholen.
  - Bei `affected, no fix possible`: VEX-Statement + Kommunikation an
    Anwender.
- **EN:** Per severity level, maximum times for acknowledgement, fix, and
  publication are set (e.g. critical in 7 days, high in 30 days).
  Example SLA table:
  | Severity | Ack | Triage | Fix | Disclosure |
  |---|---|---|---|---|
  | Critical (CVSS 9.0+) | 24 h | 48 h | 7 days | 14 days |
  | High (CVSS 7.0–8.9) | 48 h | 5 business days | 30 days | 60 days |
  | Medium (CVSS 4.0–6.9) | 3 business days | 10 business days | 90 days | 120 days |
  | Low (CVSS < 4.0) | 5 business days | 15 business days | next release | 180 days |
  External references for SLA setting:
  - **Project Zero** (Google) – 90-day rule + 30-day grace.
  - **CERT/CC** – 45-day rule as default.
  - **ZDI** (Zero Day Initiative) – 120-day rule.
  - **Industry standard**: 90-day disclosure deadline as widespread norm.
  SLA monitoring:
  - Ticket system with SLA tracking (Jira SLA, ServiceNow SLA Engine).
  - Escalation trigger at 75 % of SLA window.
  - Weekly review of open tickets by the security team.
  - Quarterly SLA compliance report to management.
  Exceptions:
  - SLA extension only with written justification (e.g. vendor dependency,
    complexity, residual risk).
  - Obtain reporter consent before extending the deadline.
  - On `affected, no fix possible`: VEX statement + user communication.
- **Akzeptanz / Acceptance:** SLA-Tabelle in der CVD-Policy und im Runbook;
  Ticket-System mit automatischer SLA-Verfolgung; Eskalation bei 75 %
  Frist; quartalsweiser Compliance-Bericht.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 6. Koordinierte Veröffentlichung / Coordinated Disclosure

- **DE:** Vor der Veröffentlichung einer Schwachstelle wird der Melder
  einbezogen. Embargos sind erlaubt; sie haben ein Enddatum.
  Coordinated Disclosure Workflow:
  1. **Bestätigung** des Eingangs (siehe Item 3).
  2. **Triage** und CVSS-Berechnung (siehe Item 4).
  3. **Embargo-Vereinbarung**: Endtermin, kommunizierte Inhalte, Sprecher.
  4. **Fix-Entwicklung** im privaten Branch / Security Advisory Draft.
  5. **CVE-Reservierung** über CNA (CVE Numbering Authority): MITRE,
     GitHub, NVD, oder eigener CNA-Status.
  6. **Pre-Notification** an wichtige Anwender (Beta, ENISA, BSI CERT-Bund).
  7. **Patch-Release** und gleichzeitige Advisory-Veröffentlichung.
  8. **Public Disclosure** mit Anerkennung des Melders (sofern gewünscht).
  Embargo-Regeln:
  - Standard-Embargo: 90 Tage ab Erstkontakt (verhandelbar).
  - Verlängerung nur mit Zustimmung des Melders.
  - Notfall-Veröffentlichung bei aktiver Ausnutzung (CISA-KEV-Eintrag).
  - Bei Nichteinhaltung durch Hersteller: Melder kann nach Embargo-Ende
    selbst veröffentlichen.
  Werkzeuge und Plattformen:
  - **GitHub Security Advisories** – privater Draft, CVE-Anforderung,
    automatische Disclosure-Veröffentlichung.
  - **Git Security Advisories** – Private Issues mit Confidentiality.
  - **CVD-Tracker**: HackerOne Reports, eigene Confluence-Seite.
  - **CSAF Document** für maschinenlesbare Advisories.
  - **OSV.dev** für ökosystemspezifische Veröffentlichung.
  Inhalt einer Advisory:
  - CVE-Nummer, CVSS-Vector, Schwere.
  - Betroffene Versionen, Fixed-Version.
  - Beschreibung in laienverständlicher Sprache.
  - Workaround / Mitigation.
  - Patch-Link, Release-Notes-Verweis.
  - Melder-Anerkennung.
  - Zeitleiste (Eingang, Bestätigung, Fix, Disclosure).
- **EN:** Before publishing a vulnerability, the reporter is involved.
  Embargoes are allowed; they have an end date.
  Coordinated disclosure workflow:
  1. **Acknowledge** receipt (see item 3).
  2. **Triage** and CVSS scoring (see item 4).
  3. **Embargo agreement**: end date, communicated content, spokesperson.
  4. **Fix development** in a private branch / security advisory draft.
  5. **CVE reservation** through CNA (CVE Numbering Authority): MITRE,
     GitHub, NVD, or own CNA status.
  6. **Pre-notification** to key consumers (beta, ENISA, BSI CERT-Bund).
  7. **Patch release** and simultaneous advisory publication.
  8. **Public disclosure** with reporter acknowledgement (if desired).
  Embargo rules:
  - Standard embargo: 90 days from first contact (negotiable).
  - Extension only with reporter consent.
  - Emergency disclosure on active exploitation (CISA KEV entry).
  - On vendor non-compliance: reporter may publish after embargo end.
  Tools and platforms:
  - **GitHub Security Advisories** – private draft, CVE request,
    automatic disclosure publication.
  - **Git Security Advisories** – private issues with confidentiality.
  - **CVD tracker**: HackerOne reports, own Confluence page.
  - **CSAF document** for machine-readable advisories.
  - **OSV.dev** for ecosystem-specific publication.
  Advisory content:
  - CVE number, CVSS vector, severity.
  - Affected versions, fixed version.
  - Description in plain language.
  - Workaround / mitigation.
  - Patch link, release-notes reference.
  - Reporter acknowledgement.
  - Timeline (ingress, ack, fix, disclosure).
- **Akzeptanz / Acceptance:** Veröffentlichungsprozess in Runbook
  dokumentiert; CVE-CNA-Pfad benannt; GitHub/Git Security Advisories
  konfiguriert; Embargo-Regeln und Pre-Notification-Liste vorhanden;
  Advisory-Vorlage mit allen Pflichtfeldern.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 7. CRA-Pflichtmeldung 24 Stunden / CRA 24-Hour Notification

- **DE:** Bei aktiv ausgenutzten Schwachstellen oder schweren Sicherheitsvorfällen
  wird die zuständige CSIRT/ENISA innerhalb von 24 Stunden vorab informiert
  (Frühwarnung), gefolgt von 72-Stunden-Meldung und Schlussbericht.
  Cyber Resilience Act (CRA) Pflichtmeldungen (Art. 14 Verordnung (EU) 2024/2847):
  - **Frühwarnung (24 h)**: Mitteilung an ENISA und nationale CSIRT, dass
    eine aktiv ausgenutzte Schwachstelle bekannt ist.
  - **Vulnerability Notification (72 h)**: detaillierte Beschreibung,
    Schweregrad, betroffene Produkte, Mitigation, ggf. PoC.
  - **Schlussbericht für aktiv ausgenutzte Schwachstellen**: spätestens
    14 Tage nach Bereitstellung des Patches, mit Ursachenanalyse,
    Auswirkungen, getroffenen Maßnahmen und Lessons Learned.
  - **Schlussbericht für schwere Sicherheitsvorfälle**: spätestens einen
    Monat nach der 72-Stunden-Meldung.
  Meldewege:
  - **ENISA** – Single Reporting Platform (geplant ab 2026).
  - **CSIRT-Bund (BSI)** – `cert@bsi.bund.de`,
    [BSI-Meldeformular](https://www.bsi.bund.de/DE/Service/Meldestelle).
  - **Nationale CSIRT** je EU-Mitgliedstaat (CSIRT-Network).
  - **CIRCL** (Luxemburg), **CERT-FR** (Frankreich), **CERT.at** (Österreich).
  Pflichtinhalte der Meldung:
  - Hersteller-Identifikation (Name, Anschrift, Kontakt).
  - Produkt-Identifikation (Name, Version, betroffene Versionen).
  - Schwachstellen-Beschreibung (Typ, CWE, CVSS, CVE, falls vergeben).
  - Aktiv-Ausnutzungs-Indikatoren (IOCs, Quelle, Zeitpunkt).
  - Geografische Reichweite, Anzahl betroffener Anwender.
  - Mitigation und Patch-Verfügbarkeit.
  - Ansprechperson für Rückfragen (Name, E-Mail, Telefon, ggf. PGP).
  Krisen-Runbook:
  - 24/7 Bereitschaft mit Eskalationskette: Engineer -> Lead -> Security Lead -> GF.
  - Vorlagen-Texte für ENISA-Meldung in DE und EN bereit.
  - Decision-Tree: aktiv ausgenutzt oder schwerer Sicherheitsvorfall?
    Dann 24-h-Frühwarnung, 72-h-Meldung und Schlussbericht.
  - Tabletop-Übung mindestens jährlich (siehe Item 11).
  Synergien mit anderen Pflichten:
  - **NIS2-Richtlinie** Art. 23: 24-h-Frühwarnung, 72-h-Lagebericht,
    1 Monat Schlussbericht.
  - **DORA**: für Finanzunternehmen und relevante IKT-Drittparteien
    können zusätzliche Klassifizierungs-, Melde- und Testpflichten gelten.
  - **DSGVO Art. 33**: 72-h-Meldung an Datenschutzbehörde bei
    Datenpanne mit personenbezogenen Daten.
  - **TKG/IT-Sicherheitsgesetz** für regulierte Branchen.
- **EN:** For actively exploited vulnerabilities or severe incidents, the
  responsible CSIRT/ENISA receives an early warning within 24 hours, followed
  by a 72-hour report and a final report.
  Cyber Resilience Act (CRA) mandatory notifications (Art. 14 Regulation (EU)
  2024/2847):
  - **Early warning (24 h)**: notify ENISA and national CSIRT that an
    actively exploited vulnerability is known.
  - **Vulnerability notification (72 h)**: detailed description, severity,
    affected products, mitigation, possibly PoC.
  - **Final report for actively exploited vulnerabilities**: no later than
    14 days after patch availability, with root cause analysis, impact,
    measures taken, and lessons learned.
  - **Final report for severe incidents**: no later than one month after
    the 72-hour notification.
  Reporting channels:
  - **ENISA** – Single Reporting Platform (planned from 2026).
  - **CSIRT-Bund (BSI)** – `cert@bsi.bund.de`,
    [BSI reporting form](https://www.bsi.bund.de/DE/Service/Meldestelle).
  - **National CSIRTs** per EU member state (CSIRT Network).
  - **CIRCL** (Luxembourg), **CERT-FR** (France), **CERT.at** (Austria).
  Mandatory report contents:
  - Manufacturer identification (name, address, contact).
  - Product identification (name, version, affected versions).
  - Vulnerability description (type, CWE, CVSS, CVE if assigned).
  - Active-exploitation indicators (IOCs, source, time).
  - Geographic scope, number of affected users.
  - Mitigation and patch availability.
  - Contact person for follow-up (name, email, phone, PGP if available).
  Crisis runbook:
  - 24/7 on-call with escalation chain: engineer -> lead -> Security Lead ->
    management.
  - Template texts for ENISA notification ready in DE and EN.
  - Decision tree: actively exploited or severe incident? Then 24-hour
    early warning, 72-hour notification, and final report.
  - Tabletop drill at least annually (see item 11).
  Synergies with other obligations:
  - **NIS2 Directive** Art. 23: 24-h early warning, 72-h status report,
    1-month final report.
  - **DORA**: for financial entities and relevant ICT third-party
    providers, additional classification, reporting, and testing duties may
    apply.
  - **GDPR Art. 33**: 72-h notification to data protection authority for
    breaches involving personal data.
  - **TKG/IT-Sicherheitsgesetz** for regulated industries.
- **Akzeptanz / Acceptance:** Meldewege (ENISA, BSI CSIRT-Bund) in der
  Krisenanleitung benannt; 24/72/14-Tage-Workflow als Decision-Tree;
  Vorlagentext DE/EN; 24/7-Bereitschaft mit Eskalationskette; Tabletop-
  Übung jährlich nachgewiesen; CL_01-Regulatory-Screening zeigt, ob NIS2,
  DORA, DSGVO oder kundenspezifische Meldewege zusätzlich anwendbar sind. /
  Reporting channels (ENISA, BSI CSIRT-Bund) are named in the crisis
  procedure; 24/72/14-day workflow as a decision tree; DE/EN template text;
  24/7 on-call with escalation chain; annual tabletop drill evidenced;
  CL_01 regulatory screening shows whether NIS2, DORA, GDPR, or customer-
  specific reporting paths additionally apply.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 8. Anwendererklärung und Hinweise / User Notification and Advisories

- **DE:** Betroffene Anwender erhalten verständliche Hinweise mit
  Lösungsschritten. Wo nötig wird ein VEX-Dokument aktualisiert.
  Inhaltsstruktur einer Anwender-Advisory:
  - **Titel** (z. B. „CVE-2026-XXXX: SQL-Injection in WidgetService").
  - **Schweregrad** (CVSS, optional EPSS).
  - **Betroffene Produkte/Versionen**.
  - **Beschreibung in Laiensprache** (was passieren kann, ohne
    PoC-Details vor Patch).
  - **Empfohlene Maßnahmen** (Update auf Version X, Workaround,
    Konfigurationsänderung).
  - **Zeitleiste** (Eingang, Bestätigung, Patch, Disclosure).
  - **Anerkennung** des Melders.
  - **Referenzen** (CVE, NVD, GitHub Advisory, Hersteller-Hinweis).
  - **Kontakt** für Rückfragen.
  Verteilkanäle:
  - **GitHub Security Advisories** – Email-Notification an Watcher.
  - **Mailing-Liste** für Sicherheits-Advisories (`security-announce@`).
  - **RSS/Atom-Feed** unter `/security/advisories.xml`.
  - **CSAF-Provider-Metadata** unter `/.well-known/csaf/provider-metadata.json`.
  - **Customer-Portal**, Newsletter, Banner in Software bei Login.
  - **CERT-Bund WID-Meldungen** für DE-weite Verteilung.
  Vorlagen:
  - **CSAF 2.0 Document** für maschinenlesbare Veröffentlichung.
  - **GitHub Security Advisory** mit YAML-Frontmatter.
  - **Markdown-Vorlage** für Webseite/Blog.
  - **Email-Vorlage** mit PGP-Signatur des Sender-Postfachs.
  Verbindung zu VEX:
  - VEX-Dokument bei jeder neuen Version aktualisieren.
  - VEX in Release-Asset und CSAF-Document referenzieren.
  - Anwender-Werkzeuge (Trivy, Dependency-Track) lesen VEX automatisch.
  Mehrsprachigkeit und Barrierefreiheit:
  - Mindestens DE und EN.
  - HTML mit semantischem Markup (h1, h2, lists), kein PDF-Only.
  - WCAG 2.2 AA-konform; Farbe nicht alleinige Information für Schwere.
- **EN:** Affected users receive understandable advisories with mitigation
  steps. Where needed, a VEX document is updated.
  User advisory structure:
  - **Title** (e.g. "CVE-2026-XXXX: SQL injection in WidgetService").
  - **Severity** (CVSS, optionally EPSS).
  - **Affected products/versions**.
  - **Plain-language description** (what can happen, without PoC details
    before patch).
  - **Recommended actions** (update to version X, workaround, configuration
    change).
  - **Timeline** (ingress, ack, patch, disclosure).
  - **Reporter acknowledgement**.
  - **References** (CVE, NVD, GitHub advisory, vendor advisory).
  - **Contact** for follow-up.
  Distribution channels:
  - **GitHub Security Advisories** – email notifications to watchers.
  - **Mailing list** for security advisories (`security-announce@`).
  - **RSS/Atom feed** at `/security/advisories.xml`.
  - **CSAF provider metadata** at `/.well-known/csaf/provider-metadata.json`.
  - **Customer portal**, newsletter, in-app banner on login.
  - **CERT-Bund WID alerts** for Germany-wide distribution.
  Templates:
  - **CSAF 2.0 document** for machine-readable publication.
  - **GitHub Security Advisory** with YAML frontmatter.
  - **Markdown template** for website/blog.
  - **Email template** with PGP signature of the sender mailbox.
  Connection to VEX:
  - Update the VEX document with every new version.
  - Reference VEX in the release asset and CSAF document.
  - User tools (Trivy, Dependency-Track) read VEX automatically.
  Multilingualism and accessibility:
  - At least DE and EN.
  - HTML with semantic markup (h1, h2, lists), no PDF-only.
  - WCAG 2.2 AA conformant; colour is not the sole indicator of severity.
- **Akzeptanz / Acceptance:** Vorlage in DE/EN vorhanden (Markdown, CSAF,
  Email, GitHub Advisory); Mailing-Liste und CSAF-Provider-Metadata
  konfiguriert; VEX-Dokument synchron aktualisiert; WCAG 2.2 AA-konforme
  Webdarstellung der Advisory.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 9. Patch-Bereitstellung / Patch Availability

- **DE:** Sicherheits-Patches werden für die unterstützten Versionen
  bereitgestellt. Der unterstützte Lebenszyklus ist veröffentlicht.
  Lifecycle-Modelle:
  - **Semantic Versioning** (`MAJOR.MINOR.PATCH`) mit klaren Support-Fenstern.
  - **LTS-Modell** (Long Term Support) – z. B. Ubuntu LTS 5+5 Jahre,
    Node.js Active LTS 18 Monate + Maintenance LTS 12 Monate.
  - **N/N-1-Modell**: aktuelle Major + vorletzte Major werden gepflegt.
  - **Release-Train**: festgelegte Release-Kadenz (z. B. quartalsweise).
  Lifecycle-Tabelle (Beispiel):
  | Version | Erstrelease | Aktiv-Support bis | Security-Support bis | EOL |
  |---|---|---|---|---|
  | 3.x | 2025-10-01 | 2027-04-01 | 2028-10-01 | 2028-10-01 |
  | 2.x | 2024-04-01 | 2025-10-01 | 2027-04-01 | 2027-04-01 |
  | 1.x | 2023-01-01 | 2024-04-01 | 2025-10-01 | 2025-10-01 |
  Patch-Bereitstellung:
  - Patches als Backport in alle aktiv und security-supporteten Branches.
  - Versionierung: Patch-Release `3.2.4` für Security-Fix in 3.x.
  - Container-Images mit semantischen Tags (`3.2`, `3.2.4`, `3.2.4-202604`)
    plus `latest`.
  - Hash-pinned Releases (SHA-256) und Cosign-Signaturen.
  Kommunikation:
  - Lifecycle-Seite öffentlich: `/lifecycle` oder `SECURITY.md`.
  - Deprecation-Hinweis in Software bei EOL-Approach (z. B. 6 Monate vorher).
  - Migration-Guide für Major-Upgrades.
  - LTS-Verlängerungen kommerziell anbieten, falls anwendbar.
  CRA-Anforderungen:
  - Mindestens 5 Jahre Security-Support nach dem Release oder über die
    erwartete Produktlebenszeit (CRA Art. 13).
  - Bei vorzeitigem EOL: schriftliche Begründung und Migrationspfad.
- **EN:** Security patches are provided for supported versions. The
  supported lifecycle is published.
  Lifecycle models:
  - **Semantic Versioning** (`MAJOR.MINOR.PATCH`) with clear support
    windows.
  - **LTS model** (long-term support) – e.g. Ubuntu LTS 5+5 years,
    Node.js Active LTS 18 months + Maintenance LTS 12 months.
  - **N/N-1 model**: current major + previous major are maintained.
  - **Release train**: defined release cadence (e.g. quarterly).
  Lifecycle table (example):
  | Version | First release | Active support until | Security support until | EOL |
  |---|---|---|---|---|
  | 3.x | 2025-10-01 | 2027-04-01 | 2028-10-01 | 2028-10-01 |
  | 2.x | 2024-04-01 | 2025-10-01 | 2027-04-01 | 2027-04-01 |
  | 1.x | 2023-01-01 | 2024-04-01 | 2025-10-01 | 2025-10-01 |
  Patch delivery:
  - Patches as backports in all actively and security-supported branches.
  - Versioning: patch release `3.2.4` for a security fix in 3.x.
  - Container images with semantic tags (`3.2`, `3.2.4`, `3.2.4-202604`)
    plus `latest`.
  - Hash-pinned releases (SHA-256) and Cosign signatures.
  Communication:
  - Public lifecycle page: `/lifecycle` or `SECURITY.md`.
  - Deprecation notice in software approaching EOL (e.g. 6 months prior).
  - Migration guide for major upgrades.
  - Offer commercial LTS extensions where applicable.
  CRA requirements:
  - At least 5 years of security support after release or for the expected
    product lifetime (CRA Art. 13).
  - On early EOL: written justification and migration path.
- **Akzeptanz / Acceptance:** Lifecycle-Tabelle in `SECURITY.md` oder
  `/lifecycle`-Seite; Patch-Plan mit Backport-Strategie dokumentiert;
  Container-Tags + Cosign-Signatur; CRA-konformer Mindest-Support-Zeitraum
  (≥ 5 Jahre) eingehalten oder begründet abweichend.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 10. Nachverfolgung und Lessons Learned / Tracking and Lessons Learned

- **DE:** Jeder Fall wird in einem Tracker geführt. Nach Abschluss wird eine
  kurze Lessons-Learned-Notiz erstellt; wiederkehrende Muster fließen in das
  Bedrohungsmodell zurück.
  Tracker-Optionen:
  - **Jira Service Management** mit Custom-Workflow (New → Triage →
    Validated → Fix-in-Progress → Embargo → Disclosed → Closed).
  - **GitHub Issues** mit Labels `vuln/critical`, `vuln/high`,
    `cvd/embargo`, `cvd/disclosed`.
  - **Git Issues** mit Confidentiality-Flag und Custom-Status.
  - **HackerOne / Bugcrowd** als externe Plattform.
  - **OTRS / Zammad** für E-Mail-basiertes Tracking.
  Pflichtfelder pro Fall:
  - Eindeutige ID (z. B. `VULN-2026-001`).
  - Eingangsdatum, Quelle, Reporter (mit Einverständnis).
  - CVSS-Vector, Severity, EPSS, ggf. CISA-KEV-Status.
  - Status, SLA-Frist, Eskalation.
  - Zuständige Person (Engineer + Owner).
  - Affected Versions, Fixed-in-Version, CVE-ID.
  - Verlinkung zu PR, Test, Advisory, Release.
  - Embargo-Endtermin.
  Lessons-Learned-Format:
  - Kurzbeschreibung (2–4 Sätze).
  - Root Cause Analysis (5-Whys oder Fishbone).
  - Was hat funktioniert? Was hat nicht funktioniert?
  - Verbesserungsmaßnahmen mit Owner und Termin.
  - Übertragbare Muster (z. B. „Input-Validierung in Search-Endpoints").
  Rückfluss in Bedrohungsmodell:
  - Bei wiederkehrenden Mustern: neue STRIDE-Kategorie oder CAPEC-Pattern
    ergänzen.
  - Linter-Regel oder Test ergänzen, falls Pattern automatisch erkennbar
    (z. B. Semgrep-Rule für die spezifische Schwachstelle).
  - Threat-Model-Update mit Verweis auf Lessons-Learned-Dokument.
  Reporting:
  - Quartalsweiser CVD-Bericht: Anzahl Meldungen, Durchschnitts-Triage-Zeit,
    Fix-Zeit, SLA-Compliance, Top-Schwachstellentypen.
  - Trend-Analyse über 4 Quartale für verantwortliche Leitung.
- **EN:** Every case is tracked. After closure, a short lessons-learned
  note is written; recurring patterns flow back into the threat model.
  Tracker options:
  - **Jira Service Management** with custom workflow (new → triage →
    validated → fix in progress → embargo → disclosed → closed).
  - **GitHub Issues** with labels `vuln/critical`, `vuln/high`,
    `cvd/embargo`, `cvd/disclosed`.
  - **Git Issues** with confidentiality flag and custom status.
  - **HackerOne / Bugcrowd** as external platforms.
  - **OTRS / Zammad** for email-based tracking.
  Mandatory fields per case:
  - Unique ID (e.g. `VULN-2026-001`).
  - Ingress date, source, reporter (with consent).
  - CVSS vector, severity, EPSS, possibly CISA KEV status.
  - Status, SLA deadline, escalation.
  - Responsible person (engineer + owner).
  - Affected versions, fixed-in version, CVE ID.
  - Links to PR, test, advisory, release.
  - Embargo end date.
  Lessons-learned format:
  - Short description (2–4 sentences).
  - Root cause analysis (5 whys or fishbone).
  - What worked? What did not work?
  - Improvement actions with owner and date.
  - Transferable patterns (e.g. "input validation in search endpoints").
  Feedback into the threat model:
  - For recurring patterns: add a new STRIDE category or CAPEC pattern.
  - Add a linter rule or test if the pattern is automatically detectable
    (e.g. Semgrep rule for the specific issue).
  - Threat model update with reference to the lessons-learned document.
  Reporting:
  - Quarterly CVD report: number of reports, average triage time, fix
    time, SLA compliance, top vulnerability types.
  - Trend analysis over 4 quarters for management.
- **Akzeptanz / Acceptance:** Tracker (Jira/GitHub/Git) mit Pflichtfeldern
  und Custom-Workflow konfiguriert; Lessons-Learned-Vorlage vorhanden;
  jeder geschlossene Fall hat verlinkte Lessons-Learned-Notiz; Quartalsbericht
  in `docs/security/cvd-quarterly-report-YYYY-Q.md`.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

#### 11. Übungen und Tests / Drills and Tests

- **DE:** Mindestens einmal im Jahr wird der Meldeprozess geübt
  (Tabletop-Übung oder echter Testbericht).
  Übungstypen:
  - **Tabletop-Übung**: Discussion-based, 2–4 h, Szenario auf Papier.
    Teilnehmer: Security-Team, Entwicklung, Kommunikation, Recht, Geschäfts-
    leitung.
  - **Functional Drill**: aktive Simulation mit Test-Meldung über `security@`,
    Verfolgung durch Triage-Workflow.
  - **Red Team / Penetration Test**: externer Test mit anschließender
    CVD-Meldung, prüft End-to-End-Prozess.
  - **Capture the Flag (CTF)** für interne Awareness.
  - **CRA-spezifische Übung**: 24-h-Meldung an ENISA-Test-Endpoint.
  Beispiel-Szenarien:
  - **Szenario A**: aktiv ausgenutzte SQL-Injection wird intern entdeckt,
    24-h-Frühwarnung an BSI durchspielen.
  - **Szenario B**: externer Forscher meldet kritische RCE während des
    Wochenendes, Bereitschaftskette aktivieren.
  - **Szenario C**: Embargo-Verletzung durch Wettbewerber, Notfall-
    Disclosure planen.
  - **Szenario D**: Reporter unterhält ungültige Identität, Prüfung
    Verifikation.
  Übungs-Werkzeuge:
  - **CISA Tabletop Exercise Packages (CTEPs)** – kostenlose Vorlagen.
  - **ENISA Cybersecurity Exercise Material**.
  - **MITRE Caldera** für automatisierte Adversary Emulation.
  - **TheHive / Cortex** für Incident-Response-Plattform-Übung.
  Übungsprotokoll:
  - Datum, Teilnehmende, Szenario, Dauer.
  - Beobachtungen je Phase (Eingang, Triage, Patch, Disclosure).
  - Identifizierte Lücken (z. B. „PGP-Schlüssel abgelaufen", „kein 24/7-
    Kontakt für ENISA").
  - Maßnahmen mit Owner und Termin.
  - Nächste Übung geplant für ...
  Mindest-Frequenz:
  - Tabletop: jährlich.
  - Functional Drill: alle 2 Jahre.
  - Penetrationstest: jährlich oder bei Major-Release.
  - CRA-Frühwarn-Übung: alle 6 Monate (Empfehlung).
- **EN:** At least once a year, the reporting process is exercised
  (tabletop drill or real test report).
  Drill types:
  - **Tabletop drill**: discussion-based, 2–4 h, scenario on paper.
    Participants: security team, engineering, communications, legal,
    management.
  - **Functional drill**: active simulation with a test report to
    `security@`, tracked through the triage workflow.
  - **Red team / penetration test**: external test followed by a CVD
    submission; tests the end-to-end process.
  - **Capture the Flag (CTF)** for internal awareness.
  - **CRA-specific exercise**: 24-h notification to an ENISA test
    endpoint.
  Example scenarios:
  - **Scenario A**: actively exploited SQL injection discovered internally,
    rehearse a 24-h early warning to the BSI.
  - **Scenario B**: external researcher reports a critical RCE on the
    weekend, activate the on-call chain.
  - **Scenario C**: embargo break by a competitor, plan emergency
    disclosure.
  - **Scenario D**: reporter has an invalid identity, test verification
    procedure.
  Exercise tools:
  - **CISA Tabletop Exercise Packages (CTEPs)** – free templates.
  - **ENISA cybersecurity exercise material**.
  - **MITRE Caldera** for automated adversary emulation.
  - **TheHive / Cortex** for incident response platform drills.
  Exercise log:
  - Date, participants, scenario, duration.
  - Observations per phase (ingress, triage, patch, disclosure).
  - Identified gaps (e.g. "PGP key expired", "no 24/7 contact for ENISA").
  - Actions with owner and deadline.
  - Next exercise planned for ...
  Minimum frequency:
  - Tabletop: annually.
  - Functional drill: every 2 years.
  - Penetration test: annually or on major release.
  - CRA early-warning drill: every 6 months (recommended).
- **Akzeptanz / Acceptance:** Übungsprotokoll mit Datum, Szenario,
  Teilnehmenden, Beobachtungen und Folgemaßnahmen; Tabletop jährlich,
  Functional Drill alle 2 Jahre, Pentest jährlich oder bei Major-Release;
  identifizierte Lücken in Folge-Tickets überführt.
- **Bewertung / Assessment:**
  - [ ] erfüllt / fulfilled
  - [ ] nicht erfüllt / not fulfilled
  - [ ] nicht anwendbar / not applicable
- **Begründung / Explanation:** _Kurz erklären: Was wurde geprüft? Warum passt der Status? Welche Annahme gilt? Schreibe so, dass eine neue Entwicklerin, ein neuer Entwickler oder eine auszubildende Person den Entscheid ohne Spezialwissen versteht. / Briefly explain: What was checked? Why does the status fit? Which assumption applies? Write so that a new developer or apprentice can understand the decision without specialist security knowledge._
- **Evidenz / Evidence:** _Konkreten Nachweis nennen: Pfad, Link, Ticket, Pull Request, Scan-Report, Konfigurationsdatei, Spec-Kit-Artefakt oder anderes Dokument. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name concrete evidence: path, link, ticket, pull request, scan report, configuration file, Spec Kit artefact, or other document. If the evidence is not self-explanatory, briefly describe what it shows._
- **Nächste Maßnahme / Next action:** _Nur bei „nicht erfüllt" ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill only for "not fulfilled": concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind, security.txt erreichbar
ist, SLAs definiert sind und der CRA-Meldeweg trainiert wurde.

**EN:** Fulfilled when every item is closed, security.txt is reachable,
SLAs are defined, and the CRA reporting path has been rehearsed.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-06-glossar-cvd"></a>

#### CVD / Coordinated Vulnerability Disclosure

- **DE:** CVD ist ein koordinierter Prozess zur Meldung und Behebung von Schwachstellen. Ziel ist, Betroffene zu schützen und Informationen verantwortungsvoll zu veröffentlichen.
- **EN:** CVD is a coordinated process for reporting and fixing vulnerabilities. The goal is to protect affected parties and publish information responsibly.

<a id="cl-06-glossar-security-txt"></a>

#### security.txt

- **DE:** security.txt ist eine standardisierte Datei auf einer Website. Sie nennt, wie Sicherheitsforschende Schwachstellen melden können.
- **EN:** security.txt is a standardised file on a website. It states how security researchers can report vulnerabilities.

<a id="cl-06-glossar-rfc"></a>

#### RFC

- **DE:** Ein RFC ist ein technisches Dokument, das Internet-Standards oder Verfahren beschreibt. Viele Web- und Sicherheitsstandards sind als RFC veröffentlicht.
- **EN:** An RFC is a technical document describing internet standards or methods. Many web and security standards are published as RFCs.

<a id="cl-06-glossar-cve"></a>

#### CVE

- **DE:** Eine CVE ist eine weltweit eindeutige Kennung für eine bekannte Schwachstelle. Sie hilft, dieselbe Schwachstelle in Tools, Tickets und Meldungen eindeutig zu benennen.
- **EN:** A CVE is a globally unique identifier for a known vulnerability. It helps name the same vulnerability clearly in tools, tickets, and advisories.

<a id="cl-06-glossar-cvss"></a>

#### CVSS

- **DE:** CVSS ist ein Bewertungssystem für die Schwere von Schwachstellen. Es beschreibt technische Eigenschaften und liefert einen Zahlenwert.
- **EN:** CVSS is a rating system for vulnerability severity. It describes technical properties and provides a numeric score.

<a id="cl-06-glossar-epss"></a>

#### EPSS

- **DE:** EPSS schätzt, wie wahrscheinlich eine Schwachstelle bald ausgenutzt wird. Es ergänzt CVSS, weil hohe Schwere nicht immer hohe Ausnutzungswahrscheinlichkeit bedeutet.
- **EN:** EPSS estimates how likely a vulnerability is to be exploited soon. It complements CVSS because high severity does not always mean high exploit likelihood.

<a id="cl-06-glossar-kev"></a>

#### KEV

- **DE:** KEV steht für Known Exploited Vulnerabilities. Der Katalog enthält Schwachstellen, die nachweislich bereits ausgenutzt wurden.
- **EN:** KEV means Known Exploited Vulnerabilities. The catalogue contains vulnerabilities that have already been exploited in practice.

<a id="cl-06-glossar-sla"></a>

#### SLA

- **DE:** Ein SLA beschreibt verbindliche Service- oder Reaktionszeiten. Bei Schwachstellen legt es fest, wie schnell bewertet oder reagiert werden muss.
- **EN:** An SLA describes binding service or response times. For vulnerabilities it defines how quickly assessment or response must happen.

<a id="cl-06-glossar-cra"></a>

#### CRA / Cyber Resilience Act

- **DE:** Der Cyber Resilience Act ist eine EU-Verordnung für Produkte mit digitalen Elementen. Er verlangt Sicherheitsanforderungen, Schwachstellenbehandlung und bestimmte Meldungen.
- **EN:** The Cyber Resilience Act is an EU regulation for products with digital elements. It requires security requirements, vulnerability handling, and certain reports.

<a id="cl-06-glossar-dora"></a>

#### DORA

- **DE:** DORA ist eine EU-Verordnung zur digitalen Betriebsstabilität im Finanzsektor. Sie kann Anforderungen an IT-Risiken, Dienstleister und Sicherheitsvorfälle auslösen.
- **EN:** DORA is an EU regulation on digital operational resilience in the financial sector. It can trigger requirements for IT risks, service providers, and security incidents.

<a id="cl-06-glossar-csaf"></a>

#### CSAF

- **DE:** CSAF ist ein maschinenlesbares Format für Sicherheitsmeldungen. Es kann Hinweise zu Produkten, Schwachstellen, Bewertungen und Maßnahmen enthalten.
- **EN:** CSAF is a machine-readable format for security advisories. It can contain information on products, vulnerabilities, ratings, and actions.

<a id="cl-06-glossar-vex"></a>

#### VEX / Vulnerability Exploitability eXchange

- **DE:** VEX erklärt, ob eine bekannte Schwachstelle in einem konkreten Produkt wirklich ausnutzbar ist. Das verhindert unnötige Alarmarbeit.
- **EN:** VEX explains whether a known vulnerability is actually exploitable in a specific product. This prevents unnecessary alert handling.

<a id="cl-06-glossar-advisory"></a>

#### Advisory / Sicherheitshinweis

- **DE:** Ein Advisory ist ein Sicherheitshinweis an Nutzende oder Kundinnen und Kunden. Es erklärt Risiko, betroffene Versionen und empfohlene Maßnahmen.
- **EN:** An advisory is a security notice to users or customers. It explains risk, affected versions, and recommended actions.

<a id="cl-06-glossar-patch"></a>

#### Patch

- **DE:** Ein Patch ist eine Änderung, die einen Fehler oder eine Schwachstelle behebt. Für Audits zählt meist auch der Nachweis, wann der Patch bereitgestellt wurde.
- **EN:** A patch is a change that fixes a bug or vulnerability. For audits, evidence of when the patch was provided usually also matters.

<a id="cl-06-glossar-evidenz"></a>

#### Evidenz / Evidence

- **DE:** Evidenz ist ein prüfbarer Nachweis. Das kann ein Link, Ticket, Scan-Bericht, Pull Request, Protokoll, Architekturdiagramm oder Dokument sein.
- **EN:** Evidence is verifiable proof. It can be a link, ticket, scan report, pull request, record, architecture diagram, or document.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-04-30):** CVSS-v4.0-Zielmethode und CRA-Schlussberichte präzisiert / Clarified CVSS v4.0 target method and CRA final reports
- **Version 1.3 (2026-06-15):** Prüfpunkt 7 um DORA-Screening und CL_01-Verknüpfung zu zusätzlichen Meldewegen ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Extended item 7 with DORA screening and CL_01 linkage to additional reporting paths; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.4 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.5 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---
