<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 07 – EU Cyber Resilience Act – Anwendbarkeit / EU Cyber Resilience Act – Applicability

**Dokument-ID / Document ID:** CL-07
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste hilft dabei, festzustellen, ob ein Produkt oder
Dienst unter den EU Cyber Resilience Act (CRA, Verordnung (EU) 2024/2847)
fällt, welche Pflichten gelten und bis wann sie umzusetzen sind.

**EN:** This checklist helps determine whether a product or service falls
under the EU Cyber Resilience Act (CRA, Regulation (EU) 2024/2847), which
duties apply, and the implementation deadline.

### Geltungsbereich / Scope

**DE:** Alle Produkte mit digitalen Elementen (Hardware, Software, Cloud-
Komponenten), die in der EU bereitgestellt werden. Auch interne Werkzeuge
sollten geprüft werden, falls sie an Externe abgegeben werden.

**EN:** All products with digital elements (hardware, software, cloud
components) made available in the EU. Internal tools should also be
checked if they are shared with external parties.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- Verordnung (EU) 2024/2847 (CRA)
- ISO/IEC 27001:2022, A.5.31, A.5.32
- ENISA-Leitfäden zur CRA-Umsetzung

### Wichtige Termine / Key Dates

- **DE:** In Kraft seit 10. Dezember 2024. Regeln zu
  Konformitätsbewertungsstellen gelten ab 11. Juni 2026. Die Pflicht zur
  Schwachstellen- und Vorfallmeldung gilt ab 11. September 2026. Die übrigen
  Pflichten gelten ab 11. Dezember 2027.
- **EN:** In force since 10 December 2024. Rules for conformity assessment
  bodies apply from 11 June 2026. Vulnerability and incident reporting duties
  apply from 11 September 2026. The remaining duties apply from
  11 December 2027.

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

**DE:** Eine intern betriebene reine SaaS-Anwendung kann für CRA „nicht direkt anwendbar" sein. Wird dieselbe Software aber als Produkt oder installierbares Paket an Dritte abgegeben, muss die CRA-Anwendbarkeit neu bewertet und begründet werden.

**EN:** An internally operated pure SaaS application may be "not directly applicable" for CRA. If the same software is delivered to third parties as a product or installable package, CRA applicability must be reassessed and explained.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [CRA / Cyber Resilience Act](#cl-07-glossar-cra)
- [Produkt mit digitalen Elementen / Product with Digital Elements](#cl-07-glossar-product-digital-elements)
- [Kritisches Produkt / Critical Product](#cl-07-glossar-critical-product)
- [Konformitätsbewertung / Conformity Assessment](#cl-07-glossar-conformity-assessment)
- [CE-Kennzeichnung / CE Marking](#cl-07-glossar-ce-marking)
- [Technische Dokumentation / Technical Documentation](#cl-07-glossar-technical-documentation)
- [SBOM / Software Bill of Materials](#cl-07-glossar-sbom)
- [Schwachstellenbehandlung / Vulnerability Handling](#cl-07-glossar-vulnerability-handling)
- [Nicht anwendbar / N/A](#cl-07-glossar-nicht-anwendbar)
- [Evidenz / Evidence](#cl-07-glossar-evidenz)

### Checkliste / Checklist

#### CL-07-01: Produktart bestimmen / Determine Product Class

- **DE:** Ist das Produkt ein „Produkt mit digitalen Elementen" gemäß CRA?
  Reine SaaS-Dienste fallen nicht direkt darunter, eingebettete Komponenten
  hingegen schon.
  Definition nach CRA Art. 3 Nr. 1:
  - „Produkt mit digitalen Elementen" = Software- oder Hardware-Produkt
    und seine Daten-Fernverarbeitungslösungen, das/die in Verkehr gebracht
    werden, mit direkter oder indirekter Daten- oder Funktionsverbindung
    zu Geräten oder Netzen.
  Typische CRA-relevante Kategorien:
  - **Hardware mit eingebetteter Software**: Router, IoT-Geräte, Smart-Home-
    Systeme, Industriesteuerungen, medizinische Geräte (im Kontext CRA).
  - **Software-Produkte**: Betriebssysteme, Browser, Anwendungssoftware,
    Mobile Apps, Kommando-Zeilen-Tools, Desktop-Anwendungen.
  - **Standalone-Komponenten**: Bibliotheken, die separat verkauft werden,
    SDKs, Firmware-Images.
  Ausnahmen (CRA gilt nicht oder anders):
  - **Reine SaaS** (Software as a Service ohne Endkunden-Installation)
    fällt unter NIS2 statt CRA.
  - **Open Source** ohne kommerzielle Tätigkeit (nicht-monetarisierte
    Beiträge), aber mit Sonderrolle „Open Source Software Steward".
  - **Medizinprodukte (MDR/IVDR)**, **Kfz-Typgenehmigung**, **Luftfahrt**,
    **Marine-Equipment** – haben eigene Regulatorien.
  - **Sicherheitskritische Verteidigungsprodukte** – nicht im CRA-Scope.
  Klassifikations-Workflow:
  1. Liefert die Organisation ein Produkt aus oder bietet es kommerziell an?
  2. Hat das Produkt digitale Elemente (Software, Firmware, Daten-
     verbindung)?
  3. Wird es im EU-Binnenmarkt in Verkehr gebracht?
  4. Falls ja: CRA gilt grundsätzlich → Klassifikation gemäß Anhang III/IV
     prüfen.
  Werkzeuge:
  - **CRA Self-Assessment Toolkit** der EU-Kommission (geplant 2026).
  - **CEN/CENELEC Harmonised Standards** als Konformitätsvermutung.
  - Interne Klassifikations-Matrix in `docs/security/cra-applicability.md`.
- **EN:** Is the product a "product with digital elements" per the CRA?
  Pure SaaS services are not directly covered, but embedded components are.
  Definition per CRA Art. 3(1):
  - "Product with digital elements" = software or hardware product and its
    remote data-processing solutions placed on the market, with direct or
    indirect data or functional connection to devices or networks.
  Typical CRA-relevant categories:
  - **Hardware with embedded software**: routers, IoT devices, smart-home
    systems, industrial controls, medical devices (in CRA context).
  - **Software products**: operating systems, browsers, application
    software, mobile apps, command-line tools, desktop applications.
  - **Standalone components**: libraries sold separately, SDKs, firmware
    images.
  Exceptions (CRA does not apply or applies differently):
  - **Pure SaaS** (no customer installation) falls under NIS2 instead of
    CRA.
  - **Open source** without commercial activity (non-monetised
    contributions), with special role "open-source software steward".
  - **Medical devices (MDR/IVDR)**, **vehicle type approval**, **aviation**,
    **marine equipment** – have their own regulations.
  - **Security-critical defence products** – outside CRA scope.
  Classification workflow:
  1. Does the organisation deliver or commercially offer a product?
  2. Does the product have digital elements (software, firmware, data
     connection)?
  3. Is it placed on the EU internal market?
  4. If yes: CRA applies in principle → check classification per Annex
     III/IV.
  Tools:
  - **CRA Self-Assessment Toolkit** by the EU Commission (planned 2026).
  - **CEN/CENELEC harmonised standards** as presumption of conformity.
  - Internal classification matrix in `docs/security/cra-applicability.md`.
- **Akzeptanz / Acceptance:** Klassifikation in `docs/security/cra-applicability.md`
  mit Begründung; Mapping zu Art. 3 Nr. 1 CRA und Ausnahmen-Prüfung;
  von Geschäftsleitung freigegeben (Datum + Unterschrift).
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

#### CL-07-02: Kritikalität nach Anhang III / Criticality per Annex III

- **DE:** Prüfen, ob das Produkt zu den „wichtigen" Klassen I oder II nach
  Anhang III gehört (z. B. Identitätsmanagement, VPN, EDR, Boot-Manager).
  Anhang III — Klasse I (wichtige Produkte mit höherem Risiko):
  - **Identitätsmanagement-Systeme** (IAM, SSO, MFA, Authentifizierungs-
    server).
  - **Privileged Access Management (PAM)**, Single-Sign-On.
  - **Standalone & Embedded Browser**.
  - **Kennwortmanager**.
  - **Antivirus-Software**.
  - **VPN-Produkte** (auch Site-to-Site, Client).
  - **Netzwerkmanagement-Systeme**.
  - **SIEM** (Security Information & Event Management).
  - **Boot-Manager**, Firmware-Loader.
  - **Public Key Infrastructure (PKI)**, digitale Zertifikatsdienste.
  - **Physikalische Netzwerk-Schnittstellen** (Router, Switches —
    konsumentennah).
  - **Operating Systems** für Unternehmens-Geräte.
  - **Microcontroller mit sicherheitsrelevanten Funktionen**.
  - **ASICs/FPGAs** für sicherheitsrelevante Verarbeitung.
  - **Industrielle Automatisierung & Steuerungssysteme** (PLCs, DCS, HMI).
  Anhang III — Klasse II (wichtige Produkte mit höherem Risiko):
  - **Hypervisoren / Container-Runtime-Systeme**.
  - **Firewalls, IPS, IDS**.
  - **Manipulationssichere Microprozessoren** (Tamper-resistant).
  - **Manipulationssichere Microcontroller**.
  Konsequenzen der Klassifikation:
  - **Klasse I**: Konformitätsbewertung über Modul A (interne
    Produktionskontrolle) oder mit harmonisierten Normen.
  - **Klasse II**: Modul B (EU-Baumusterprüfung) + Modul C oder Modul H
    (volle Qualitätssicherung) — externe Notified Body erforderlich.
  Werkzeuge:
  - Aktuelle Anhang-III-Listen aus Verordnung (EU) 2024/2847.
  - CEN/CENELEC harmonisierte Normen als Stand der Technik.
  - [NANDO-Datenbank für Notified Bodies](https://ec.europa.eu/growth/tools-databases/nando/).
- **EN:** Check whether the product belongs to "important" class I or II
  per Annex III (e.g. identity management, VPN, EDR, boot managers).
  Annex III — Class I (important products with higher risk):
  - **Identity-management systems** (IAM, SSO, MFA, authentication
    servers).
  - **Privileged Access Management (PAM)**, single sign-on.
  - **Standalone and embedded browsers**.
  - **Password managers**.
  - **Antivirus software**.
  - **VPN products** (including site-to-site, client).
  - **Network management systems**.
  - **SIEM** (security information & event management).
  - **Boot managers**, firmware loaders.
  - **Public-key infrastructure (PKI)**, digital certificate services.
  - **Physical network interfaces** (routers, switches — consumer-grade).
  - **Operating systems** for enterprise devices.
  - **Microcontrollers with security-relevant functions**.
  - **ASICs/FPGAs** for security-relevant processing.
  - **Industrial automation & control systems** (PLCs, DCS, HMI).
  Annex III — Class II (important products with higher risk):
  - **Hypervisors / container runtime systems**.
  - **Firewalls, IPS, IDS**.
  - **Tamper-resistant microprocessors**.
  - **Tamper-resistant microcontrollers**.
  Consequences of classification:
  - **Class I**: conformity assessment via Module A (internal production
    control) or with harmonised standards.
  - **Class II**: Module B (EU type examination) + Module C, or Module H
    (full quality assurance) — external notified body required.
  Tools:
  - Up-to-date Annex III lists from Regulation (EU) 2024/2847.
  - CEN/CENELEC harmonised standards as state of the art.
  - [NANDO database for notified bodies](https://ec.europa.eu/growth/tools-databases/nando/).
- **Akzeptanz / Acceptance:** Anhang-III-Zuordnung schriftlich in
  `docs/security/cra-applicability.md`; bei Klasse II: Notified Body
  benannt; harmonisierte Normen referenziert; Begründung der gewählten
  Konformitätsbewertungsmodul.
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

#### CL-07-03: Kritisch nach Anhang IV / Critical per Annex IV

- **DE:** Prüfen, ob das Produkt zu den „kritischen" Produkten nach
  Anhang IV gehört (z. B. Hardware-Sicherheitsmodule, Smart Meter Gateways).
  Diese Klasse erfordert eine externe Konformitätsbewertung.
  Anhang IV — kritische Produkte mit digitalen Elementen:
  - **Hardware-Sicherheitsmodule (HSM)** mit Schlüsselgenerierungs-Funktion.
  - **Smart-Card-IC** und sichere Elemente für Authentifizierung.
  - **Smart-Meter-Gateways** in intelligenten Messsystemen.
  - **Andere Produkte**, die per delegiertem Rechtsakt der Kommission als
    kritisch eingestuft werden (Liste kann sich erweitern).
  Anforderungen für kritische Produkte:
  - **Verpflichtende externe Konformitätsbewertung** durch Notified Body
    (Modul B/C oder H, nicht Selbstbewertung).
  - **Cybersecurity-Zertifikat** im Rahmen eines europäischen Zertifizierungs-
    schemas (CSA, gem. Verordnung (EU) 2019/881).
  - **Common Criteria EAL4+** oder gleichwertige Zertifizierung für HSMs.
  - **Schutzprofile (Protection Profiles)** wie z. B. PP-SSCD (Secure
    Signature Creation Device), PP-Smart Meter Gateway.
  - Höhere Anforderungen an Penetrationstests, Side-Channel-Analyse,
    Fault-Injection-Tests.
  Notified-Body-Auswahl:
  - **NIST-akkreditierte CC-Labs** für Common Criteria.
  - **DEKRA**, **TÜV Süd**, **TÜV Rheinland**, **Bureau Veritas** mit
    CRA-Notifikation.
  - **CCRA Mutual Recognition** beachten für internationale Anerkennung.
  Werkzeuge:
  - **CC Tools**: AIS-Liste BSI, BSI Schutzprofile,
    [Common Criteria Portal](https://www.commoncriteriaportal.org/).
  - **EUCC** (European Cybersecurity Certification scheme on Common
    Criteria) als CSA-Implementierung.
  Falls nicht zutreffend:
  - Explizit als „nicht in Anhang IV gelistet" begründen, mit Verweis auf
    Anhang-III- oder Standard-Klassifikation.
- **EN:** Check whether the product belongs to "critical" products per
  Annex IV (e.g. hardware security modules, smart meter gateways). This
  class requires an external conformity assessment.
  Annex IV — critical products with digital elements:
  - **Hardware security modules (HSM)** with key-generation function.
  - **Smart-card ICs** and secure elements for authentication.
  - **Smart-meter gateways** in smart metering systems.
  - **Other products** classified as critical via delegated act by the
    Commission (the list may expand).
  Requirements for critical products:
  - **Mandatory external conformity assessment** by a notified body
    (Module B/C or H, not self-assessment).
  - **Cybersecurity certificate** under a European certification scheme
    (CSA, per Regulation (EU) 2019/881).
  - **Common Criteria EAL4+** or equivalent for HSMs.
  - **Protection profiles** like PP-SSCD (Secure Signature Creation
    Device), PP-Smart Meter Gateway.
  - Higher requirements for penetration testing, side-channel analysis,
    fault-injection testing.
  Notified-body selection:
  - **NIST-accredited CC labs** for Common Criteria.
  - **DEKRA**, **TÜV Süd**, **TÜV Rheinland**, **Bureau Veritas** with
    CRA notification.
  - **CCRA mutual recognition** for international acceptance.
  Tools:
  - **CC tools**: BSI AIS list, BSI protection profiles,
    [Common Criteria Portal](https://www.commoncriteriaportal.org/).
  - **EUCC** (European Cybersecurity Certification scheme on Common
    Criteria) as CSA implementation.
  If not applicable:
  - Explicitly justify "not listed in Annex IV" referring to the Annex III
    or standard classification.
- **Akzeptanz / Acceptance:** Anhang-IV-Prüfung in
  `docs/security/cra-applicability.md` dokumentiert; bei Treffer: Notified
  Body, Schutzprofil, geplantes Zertifikat (z. B. EUCC EAL4+) benannt; bei
  Nicht-Treffer: explizite Begründung mit Verweis auf gewählte Klasse.
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

#### CL-07-04: Sicherheits-Anforderungen Anhang I Teil I / Annex I Part I Requirements

- **DE:** Die wesentlichen Sicherheitsanforderungen werden umgesetzt:
  Zugangskontrolle, Vertraulichkeit, Integrität, Verfügbarkeit, sichere
  Voreinstellungen, Update-Fähigkeit, Datensparsamkeit.
  CRA Anhang I, Teil I — wesentliche Cybersicherheitsanforderungen
  (Auswahl):
  - **Risikobasiertes Design** (Art. 1.1): Sicherheit auf Basis der
    Risikobewertung (Art. 13 Abs. 2).
  - **Keine bekannten Schwachstellen** beim Inverkehrbringen
    (Art. 1.2 lit. a).
  - **Sichere Standardkonfiguration** (Art. 1.2 lit. b): Default-Settings
    sicher, Reset auf sicheren Zustand möglich.
  - **Schutz vor unbefugtem Zugriff** (Art. 1.2 lit. c): Authentifizierung,
    Identitätsmanagement.
  - **Vertraulichkeitsschutz** (Art. 1.2 lit. d): Verschlüsselung in
    Ruhe und im Transit.
  - **Integritätsschutz** (Art. 1.2 lit. e): von Daten, Befehlen,
    Konfigurationen, Code.
  - **Datensparsamkeit** (Art. 1.2 lit. f): nur erforderliche personen-
    bezogene Daten verarbeiten.
  - **Verfügbarkeit** (Art. 1.2 lit. g): Schutz vor DoS-Angriffen.
  - **Reduzierung der Angriffsfläche** (Art. 1.2 lit. h): minimaler
    Funktionsumfang im Standard.
  - **Logging und Monitoring** (Art. 1.2 lit. i): Aufzeichnung
    sicherheitsrelevanter Ereignisse.
  - **Update-Mechanismus** (Art. 1.2 lit. j): sichere automatische oder
    manuelle Updates, signierte Updates.
  - **Sichere Stilllegung / Datenlöschung** (Art. 1.2 lit. k).
  Anforderungs-Mapping (Beispiel-Tabelle in `docs/security/cra-anhang-i-mapping.md`):
  | CRA Anf. | Umsetzung | Evidenz |
  |---|---|---|
  | 1.2.a Keine bekannten Schwachstellen | OSV-Scan vor Release | `release/v1.2.3/scan-report.json` |
  | 1.2.b Sichere Defaults | TLS 1.2+, MFA an, Telemetrie aus | `config/defaults.yaml` |
  | 1.2.c Zugangskontrolle | OAuth 2.1 + RBAC | `arc42/section-08-security.md` |
  | 1.2.d Vertraulichkeit | AES-GCM in Ruhe, TLS 1.3 in Transit | `security-quality-scenarios.md` |
  | 1.2.j Update-Mechanismus | Signed Updates via Cosign | `update-mechanism.md` |
  Harmonisierte Normen als Konformitätsvermutung:
  - **EN 18031** (Cybersecurity for connected radio equipment).
  - **ETSI EN 303 645** (IoT Consumer Cybersecurity).
  - **IEC 62443** (Industrial Security).
- **EN:** Essential cybersecurity requirements are implemented: access
  control, confidentiality, integrity, availability, secure defaults,
  updatability, data minimisation.
  CRA Annex I, Part I — essential cybersecurity requirements (selection):
  - **Risk-based design** (Art. 1.1): security based on the risk
    assessment (Art. 13(2)).
  - **No known vulnerabilities** when placed on the market
    (Art. 1.2(a)).
  - **Secure default configuration** (Art. 1.2(b)): default settings are
    secure, reset to secure state possible.
  - **Protection against unauthorised access** (Art. 1.2(c)): authentication,
    identity management.
  - **Confidentiality protection** (Art. 1.2(d)): encryption at rest and
    in transit.
  - **Integrity protection** (Art. 1.2(e)): of data, commands,
    configurations, code.
  - **Data minimisation** (Art. 1.2(f)): process only necessary personal
    data.
  - **Availability** (Art. 1.2(g)): protection against DoS attacks.
  - **Attack-surface reduction** (Art. 1.2(h)): minimal functionality by
    default.
  - **Logging and monitoring** (Art. 1.2(i)): recording of security-
    relevant events.
  - **Update mechanism** (Art. 1.2(j)): secure automatic or manual
    updates, signed updates.
  - **Secure decommissioning / data deletion** (Art. 1.2(k)).
  Requirement mapping (example table in `docs/security/cra-annex-i-mapping.md`):
  | CRA req. | Implementation | Evidence |
  |---|---|---|
  | 1.2.a No known vulnerabilities | OSV scan before release | `release/v1.2.3/scan-report.json` |
  | 1.2.b Secure defaults | TLS 1.2+, MFA on, telemetry off | `config/defaults.yaml` |
  | 1.2.c Access control | OAuth 2.1 + RBAC | `arc42/section-08-security.md` |
  | 1.2.d Confidentiality | AES-GCM at rest, TLS 1.3 in transit | `security-quality-scenarios.md` |
  | 1.2.j Update mechanism | Signed updates via Cosign | `update-mechanism.md` |
  Harmonised standards as presumption of conformity:
  - **EN 18031** (cybersecurity for connected radio equipment).
  - **ETSI EN 303 645** (IoT consumer cybersecurity).
  - **IEC 62443** (industrial security).
- **Akzeptanz / Acceptance:** Anforderungs-Mapping je Anhang-I-Punkt mit
  Umsetzung und Evidenz in `docs/security/cra-annex-i-mapping.md`;
  harmonisierte Normen referenziert; Risikobewertung in
  `docs/security/risk-assessment.md` verlinkt.
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

#### CL-07-05: Schwachstellenbehandlung Anhang I Teil II / Annex I Part II Vulnerability Handling

- **DE:** Es gibt eine SBOM, einen Meldeweg, einen Patch-Prozess, eine
  Sicherheitsrichtlinie und Tests vor Auslieferung.
  CRA Anhang I, Teil II — Schwachstellenbehandlungs-Anforderungen:
  - **Schwachstellen identifizieren und dokumentieren** (Art. 2.1 lit. a):
    SBOM mit allen Komponenten und Versionen.
  - **Schwachstellen unverzüglich beheben** (Art. 2.1 lit. b): Patch-
    Prozess mit definierten SLA.
  - **Sicherheitsmaßnahmen regelmäßig testen** (Art. 2.1 lit. c):
    Pentests, SAST, DAST, Fuzzing.
  - **Patches kostenlos und unverzüglich bereitstellen** (Art. 2.1 lit. d):
    keine Paywall für Sicherheits-Updates.
  - **Sicherheits-Advisory veröffentlichen** (Art. 2.1 lit. e): CSAF/CVE-
    Format.
  - **Coordinated Vulnerability Disclosure Policy** (Art. 2.1 lit. f):
    siehe Schwachstellenoffenlegung-Checkliste.
  - **Maßnahmen zur Erleichterung der Meldung** (Art. 2.1 lit. g):
    `security.txt`, `security@`-Postfach.
  - **Sichere Update-Verteilung** (Art. 2.1 lit. h): signierte Updates,
    Rollback-Möglichkeit.
  Erforderliche Artefakte:
  - **SBOM** (CycloneDX/SPDX) je Release — siehe Lieferkette-Checkliste.
  - **Coordinated Vulnerability Disclosure Policy** —
    `docs/security/cvd-policy.md` und `SECURITY.md`.
  - **Security.txt** unter `/.well-known/security.txt`.
  - **Patch-Prozess** in `docs/security/patch-management.md` mit SLA-
    Tabelle.
  - **Test-Evidenzen**: Pentest-Bericht (jährlich), SAST/DAST-Reports
    (CI), Fuzzing-Logs (kontinuierlich).
  - **Advisory-Vorlage** (CSAF 2.0).
  Werkzeuge:
  - **Dependency-Track** mit SBOM-Upload und CVE-Tracking.
  - **GitHub Security Advisories** + **Dependabot Alerts**.
  - **CSAF Provider Metadata** für maschinenlesbare Advisories.
  - **SARIF**-Format für SAST-Ergebnisse.
- **EN:** There is an SBOM, a reporting channel, a patch process, a
  security policy, and tests before delivery.
  CRA Annex I, Part II — vulnerability handling requirements:
  - **Identify and document vulnerabilities** (Art. 2.1(a)): SBOM with
    all components and versions.
  - **Address vulnerabilities without delay** (Art. 2.1(b)): patch
    process with defined SLAs.
  - **Test security measures regularly** (Art. 2.1(c)): pentests, SAST,
    DAST, fuzzing.
  - **Provide patches free of charge and without delay** (Art. 2.1(d)):
    no paywall for security updates.
  - **Publish security advisories** (Art. 2.1(e)): CSAF/CVE format.
  - **Coordinated vulnerability disclosure policy** (Art. 2.1(f)): see
    vulnerability disclosure checklist.
  - **Measures to facilitate reporting** (Art. 2.1(g)): `security.txt`,
    `security@` mailbox.
  - **Secure update distribution** (Art. 2.1(h)): signed updates,
    rollback capability.
  Required artefacts:
  - **SBOM** (CycloneDX/SPDX) per release — see supply-chain checklist.
  - **Coordinated vulnerability disclosure policy** —
    `docs/security/cvd-policy.md` and `SECURITY.md`.
  - **`security.txt`** at `/.well-known/security.txt`.
  - **Patch process** in `docs/security/patch-management.md` with SLA
    table.
  - **Test evidence**: pentest report (annual), SAST/DAST reports (CI),
    fuzzing logs (continuous).
  - **Advisory template** (CSAF 2.0).
  Tools:
  - **Dependency-Track** with SBOM upload and CVE tracking.
  - **GitHub Security Advisories** + **Dependabot alerts**.
  - **CSAF provider metadata** for machine-readable advisories.
  - **SARIF** format for SAST results.
- **Akzeptanz / Acceptance:** SBOM, security.txt, CVD-Policy, Patch-Prozess
  mit SLA, Test-Evidenzen, Advisory-Vorlage je Punkt mit Pfad zur Evidenz
  in `docs/security/cra-annex-ii-mapping.md` dokumentiert.
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

#### CL-07-06: Konformitätsbewertung / Conformity Assessment

- **DE:** Der passende Bewertungspfad ist gewählt: Selbstbewertung,
  EU-Baumusterprüfung oder vollständige Qualitätssicherung. Die Wahl ist
  begründet.
  Konformitätsbewertungsmodule (CRA Anhang VIII):
  - **Modul A — Interne Produktionskontrolle (Selbstbewertung)**:
    - Geeignet für Standard-Produkte (nicht Anhang III oder IV).
    - Hersteller dokumentiert Konformität intern.
    - Keine Notified Body erforderlich.
  - **Modul B — EU-Baumusterprüfung + Modul C — Konformität mit Baumuster**:
    - Pflicht für Anhang IV und Klasse II nach Anhang III ohne
      harmonisierte Normen.
    - Notified Body prüft Baumuster und Serienfertigung.
  - **Modul H — Vollständige Qualitätssicherung**:
    - Alternative für Klasse II und kritische Produkte.
    - Notified Body zertifiziert das gesamte Qualitätsmanagementsystem.
  - **Europäisches Cybersicherheitszertifikat (CSA, EUCC)**:
    - Für kritische Produkte nach Anhang IV mit höchsten Anforderungen.
  Auswahl-Workflow:
  1. Klasse bestimmen (Anhang III/IV/Standard).
  2. Harmonisierte Normen verfügbar?
     - Ja → Modul A (mit Konformitätsvermutung) für Standard und
       Klasse I möglich.
     - Nein → externe Bewertung (Modul B+C oder H).
  3. Klasse II oder Anhang IV → externe Bewertung verpflichtend.
  4. Bei kritischen Funktionen: zusätzliches CSA/EUCC-Zertifikat.
  Notified-Body-Auswahl:
  - [NANDO-Datenbank für Notified and Designated Organisations](https://ec.europa.eu/growth/tools-databases/nando/).
  - Bekannte CRA-notifizierte Bodies (Stand 2026): TÜV Rheinland,
    DEKRA, Bureau Veritas, SGS, BSI Group.
  - Auswahlkriterien: Akkreditierung, Sprache (DE), Branchenexpertise,
    Lieferzeit, Kosten.
  Begründungs-Dokument:
  - Welche Klasse?
  - Welches Modul?
  - Welcher Notified Body? (falls anwendbar)
  - Welche harmonisierten Normen?
  - Geplanter Zeitplan für Erst-Zertifizierung und Re-Zertifizierung.
- **EN:** The right assessment route is chosen: self-assessment, EU type-
  examination, or full quality assurance. The choice is justified.
  Conformity assessment modules (CRA Annex VIII):
  - **Module A — internal production control (self-assessment)**:
    - Suitable for standard products (not Annex III or IV).
    - Manufacturer documents conformity internally.
    - No notified body required.
  - **Module B — EU type-examination + Module C — conformity to type**:
    - Mandatory for Annex IV and Annex III Class II without harmonised
      standards.
    - Notified body assesses type and serial production.
  - **Module H — full quality assurance**:
    - Alternative for Class II and critical products.
    - Notified body certifies the complete quality management system.
  - **European Cybersecurity Certificate (CSA, EUCC)**:
    - For critical products per Annex IV with highest requirements.
  Selection workflow:
  1. Determine class (Annex III/IV/standard).
  2. Are harmonised standards available?
     - Yes → Module A (with presumption of conformity) for standard and
       Class I possible.
     - No → external assessment (Module B+C or H).
  3. Class II or Annex IV → external assessment mandatory.
  4. For critical functions: additional CSA/EUCC certificate.
  Notified-body selection:
  - [NANDO database for Notified and Designated Organisations](https://ec.europa.eu/growth/tools-databases/nando/).
  - Known CRA-notified bodies (status 2026): TÜV Rheinland, DEKRA,
    Bureau Veritas, SGS, BSI Group.
  - Selection criteria: accreditation, language (German), domain
    expertise, lead time, cost.
  Justification document:
  - Which class?
  - Which module?
  - Which notified body? (if applicable)
  - Which harmonised standards?
  - Planned schedule for initial and re-certification.
- **Akzeptanz / Acceptance:** Bewertungspfad und Notified Body (falls
  anwendbar) in `docs/security/cra-conformity-assessment.md` mit
  Begründung; harmonisierte Normen referenziert; Zeitplan für Zertifizierung
  enthalten.
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

#### CL-07-07: CE-Kennzeichnung / CE Marking

- **DE:** Vor Inverkehrbringen wird die CE-Kennzeichnung mit
  Konformitätserklärung vorbereitet (sofern anwendbar).
  CE-Kennzeichnungs-Anforderungen:
  - **Anbringung der CE-Kennzeichnung** (CRA Art. 32):
    - Auf dem Produkt selbst (sichtbar, lesbar, dauerhaft).
    - Bei Software: in der Software (Splash-Screen, Settings, Help) und
      auf der Verpackung / im Online-Shop.
    - Mindestgröße: 5 mm Höhe; bei kleineren Geräten zulässig kleiner.
  - **EU-Konformitätserklärung (DoC)** (CRA Art. 28, Anhang V):
    - Pflichtfelder:
      - Hersteller-Name und -Anschrift, ggf. Bevollmächtigter.
      - Produktname, Modell-/Typennummer, Seriennummer / Charge.
      - „Diese Konformitätserklärung wird unter alleiniger Verantwortung
        des Herstellers ausgestellt."
      - Verweis auf CRA: „Verordnung (EU) 2024/2847".
      - Liste der angewandten harmonisierten Normen.
      - Bei Notified Body: Name, Identifikationsnummer, Typ der
        Bescheinigung.
      - Ort, Datum, Unterschrift, Funktion.
    - Sprache: in der/den Sprache(n) der Mitgliedstaaten, in denen
      vermarktet wird.
  - **Begleitende Informationen** (CRA Art. 13, Anhang II):
    - Sicherheits-Hinweise, sichere Inbetriebnahme.
    - Update-Mechanismen, Lifetime, Support-Zeitraum.
    - Liste angeschlossener Dienste.
    - Reset-Anleitung auf sicheren Zustand.
    - Kontakt für Schwachstellenmeldungen.
  Vorlage einer DoC:
  ```text
  EU-Konformitätserklärung

  Hersteller: <Organisation>, <Adresse>, <PLZ Ort>, <Land>
  Produkt: WidgetService 3.2.4
  Erklärung: Wir erklären in alleiniger Verantwortung, dass das oben
  bezeichnete Produkt allen einschlägigen Bestimmungen der Verordnung
  (EU) 2024/2847 (Cyber Resilience Act) entspricht.
  Angewandte harmonisierte Normen: EN 18031:2024, ETSI EN 303 645
  Notified Body: TÜV Rheinland, ID 0197, Modul B+C
  Zertifikat-Nr.: TR-CRA-2026-XXXX
  Ort, Datum: <Ort>, <Datum>
  Unterschrift: ____________________ Geschäftsführer
  ```
- **EN:** Before placing on the market, CE marking with declaration of
  conformity is prepared (if applicable).
  CE marking requirements:
  - **Affixing CE marking** (CRA Art. 32):
    - On the product itself (visible, legible, durable).
    - For software: in the software (splash screen, settings, help) and
      on the packaging / online shop.
    - Minimum size: 5 mm height; smaller is permitted on small devices.
  - **EU Declaration of Conformity (DoC)** (CRA Art. 28, Annex V):
    - Mandatory fields:
      - Manufacturer name and address, possibly authorised representative.
      - Product name, model/type number, serial number / batch.
      - "This declaration of conformity is issued under the sole
        responsibility of the manufacturer."
      - Reference to CRA: "Regulation (EU) 2024/2847".
      - List of harmonised standards applied.
      - For notified body: name, identification number, type of
        certificate.
      - Place, date, signature, role.
    - Language: in the language(s) of the member states where marketed.
  - **Accompanying information** (CRA Art. 13, Annex II):
    - Safety advice, secure setup.
    - Update mechanisms, lifetime, support period.
    - List of connected services.
    - Reset instructions to a secure state.
    - Contact for vulnerability reports.
  DoC template:
  ```text
  EU Declaration of Conformity

  Manufacturer: <organization>, <address>, <postal code and city>, <country>
  Product: WidgetService 3.2.4
  Statement: We declare under our sole responsibility that the product
  identified above complies with all relevant provisions of Regulation
  (EU) 2024/2847 (Cyber Resilience Act).
  Applied harmonised standards: EN 18031:2024, ETSI EN 303 645
  Notified Body: TÜV Rheinland, ID 0197, Module B+C
  Certificate No.: TR-CRA-2026-XXXX
  Place, Date: <place>, <date>
  Signature: ____________________ Managing Director
  ```
- **Akzeptanz / Acceptance:** EU-DoC im Release-Paket (PDF + Markdown);
  CE-Kennzeichnung im Produkt sichtbar (Splash, Settings); Begleit-
  informationen nach Anhang II vollständig; Sprache in DE und EN
  (mindestens).
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

#### CL-07-08: Technische Dokumentation / Technical Documentation

- **DE:** Die technische Dokumentation umfasst Risikobewertung,
  Bedrohungsmodell, SBOM, Architekturbeschreibung, Test- und Patch-Belege.
  Pflichtinhalte (CRA Anhang VII):
  - **Allgemeine Beschreibung des Produkts**: Funktion, Komponenten, Schnitt-
    stellen, Lebenszyklus.
  - **Konzeption, Entwicklung, Herstellungsprozess**: Architekturdiagramm
    (arc42), Datenflussdiagramm, Komponentendiagramm.
  - **Risikobewertung** (Cybersecurity Risk Assessment) mit:
    - Bedrohungsmodell (STRIDE + CAPEC).
    - Schutzziele (CIA + Authentizität, Nicht-Abstreitbarkeit).
    - Restrisiken mit Akzeptanz und Begründung.
  - **Anwendung der wesentlichen Anforderungen** aus Anhang I:
    Mapping-Tabelle je Anforderung mit Umsetzung.
  - **Anwendbarkeit harmonisierter Normen**: Liste mit Versionen.
  - **Konformitätsbewertungsbericht**: Ergebnis je Modul, ggf. Notified-
    Body-Bescheinigung.
  - **Test-Evidenz**: SAST, DAST, Pentest, Fuzzing, Vulnerability-Scan-
    Reports.
  - **SBOM** (CycloneDX/SPDX) je Release-Version.
  - **Patch-Management-Plan** und Lifecycle-Tabelle.
  - **Coordinated Vulnerability Disclosure Policy**.
  - **Konformitätserklärung (DoC)** Kopie.
  Empfohlene Ordnerstruktur:
  ```text
  docs/
    cra-dossier/
      00-overview.md
      01-product-description.md
      02-architecture/
        arc42-section-08.md
        dfd.drawio
      03-risk-assessment/
        threat-model.md
        risk-register.md
      04-annex-i-mapping/
        annex-i-part-i.md
        annex-i-part-ii.md
      05-conformity/
        module-a-self-assessment.md
        notified-body-certificate.pdf
      06-test-evidence/
        sast-report-2026-04.sarif
        pentest-2026-q1.pdf
        sbom-v3.2.4.cdx.json
      07-lifecycle/
        patch-management.md
        eol-policy.md
      08-cvd-policy.md
      09-doc-2026-04-27.pdf
  ```
  Aufbewahrungsfristen:
  - **Mindestens 10 Jahre** nach Inverkehrbringen (CRA Art. 7).
  - **Audit-fest**: WORM-Storage, Cloud-Storage mit Object-Lock,
    qualifiziertes elektronisches Siegel.
- **EN:** Technical documentation includes risk assessment, threat model,
  SBOM, architecture description, test evidence, and patch evidence.
  Mandatory contents (CRA Annex VII):
  - **General description of the product**: function, components,
    interfaces, lifecycle.
  - **Design, development, manufacturing process**: architecture diagram
    (arc42), data-flow diagram, component diagram.
  - **Risk assessment** (cybersecurity risk assessment) with:
    - Threat model (STRIDE + CAPEC).
    - Security objectives (CIA + authenticity, non-repudiation).
    - Residual risks with acceptance and justification.
  - **Application of essential requirements** from Annex I: mapping
    table per requirement with implementation.
  - **Applicability of harmonised standards**: list with versions.
  - **Conformity assessment report**: result per module, possibly
    notified-body certificate.
  - **Test evidence**: SAST, DAST, pentest, fuzzing, vulnerability scan
    reports.
  - **SBOM** (CycloneDX/SPDX) per release version.
  - **Patch management plan** and lifecycle table.
  - **Coordinated vulnerability disclosure policy**.
  - **Declaration of Conformity (DoC)** copy.
  Recommended folder structure:
  ```text
  docs/
    cra-dossier/
      00-overview.md
      01-product-description.md
      02-architecture/
        arc42-section-08.md
        dfd.drawio
      03-risk-assessment/
        threat-model.md
        risk-register.md
      04-annex-i-mapping/
        annex-i-part-i.md
        annex-i-part-ii.md
      05-conformity/
        module-a-self-assessment.md
        notified-body-certificate.pdf
      06-test-evidence/
        sast-report-2026-04.sarif
        pentest-2026-q1.pdf
        sbom-v3.2.4.cdx.json
      07-lifecycle/
        patch-management.md
        eol-policy.md
      08-cvd-policy.md
      09-doc-2026-04-27.pdf
  ```
  Retention periods:
  - **At least 10 years** after placing on the market (CRA Art. 7).
  - **Audit-proof**: WORM storage, cloud storage with Object Lock,
    qualified electronic seal.
- **Akzeptanz / Acceptance:** CRA-Dossier vollständig in
  `docs/cra-dossier/` mit allen Anhang-VII-Inhalten; 10-Jahre-Retention
  konfiguriert (z. B. S3 Object Lock); audit-feste Ablage; Index-Dokument
  `00-overview.md` mit Verweisen.
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

#### CL-07-09: Meldepflichten 24 / 72 Stunden / 24- and 72-hour Reports

- **DE:** Verfahren für Frühwarnung (24 Stunden), Update-Meldung
  (72 Stunden) und Schlussbericht an CSIRT/ENISA sind eingerichtet.
  CRA Art. 14 — Meldepflichten:
  - **24 Stunden** nach Bekanntwerden einer aktiv ausgenutzten
    Schwachstelle: Frühwarnung (Early Warning) an ENISA + nationale
    CSIRT.
  - **72 Stunden**: Vulnerability Notification mit detaillierter
    Beschreibung, Schwere, betroffene Versionen, Mitigation.
  - **14 Tage** nach Patch-Bereitstellung: Schlussbericht zu aktiv
    ausgenutzten Schwachstellen mit Root Cause, getroffenen Maßnahmen und
    Lessons Learned.
  - **Ein Monat** nach der 72-Stunden-Meldung: Schlussbericht zu schweren
    Sicherheitsvorfällen.
  - **24 Stunden** bei schwerwiegendem Sicherheitsvorfall (Incident
    Notification) – separat von Vulnerability Notification.
  Erforderliche Verfahren:
  - **Krisen-Runbook** mit Eskalationskette: Engineer -> Lead -> Security Lead ->
    Geschäftsleitung.
  - **24/7-Bereitschaft** mit definiertem On-Call-Schedule (z. B.
    PagerDuty, Opsgenie).
  - **Kommunikationsmatrix**: wer benachrichtigt ENISA, BSI, Kunden,
    Pressestelle, Recht.
  - **Vorlagentexte** in DE und EN für ENISA- und BSI-Meldungen.
  - **Decision-Tree** zur Klassifikation: aktiv ausgenutzt / potenziell
    ausnutzbar / kein Risiko.
  - **Übungen** mindestens halbjährlich (siehe Schwachstellen-Checkliste
    Item 11).
  Meldekanäle:
  - **ENISA Single Reporting Platform** (geplant ab 2026).
  - **BSI CSIRT-Bund**: `cert@bsi.bund.de`,
    [BSI-Meldeformular](https://www.bsi.bund.de/DE/Service/Meldestelle).
  - **Nationale CSIRT** je Mitgliedstaat (CSIRT-Network).
  - **CSAF Document** für maschinenlesbare Meldungen.
  Pflichtfelder einer Frühwarnung:
  - Hersteller-Identifikation.
  - Produkt-Identifikation (Name, Version).
  - Kurzbeschreibung der Schwachstelle/des Vorfalls.
  - Hinweise auf aktive Ausnutzung (IOCs, Quelle).
  - Vorläufige CVSS-Bewertung.
  - Bekannte Mitigationen oder Workaround.
  - Ansprechperson + 24/7-Kontakt.
  Werkzeuge:
  - **TheHive / Cortex** für Incident-Response-Plattform.
  - **MISP** für Threat-Intelligence-Sharing.
  - **CSAF Provider Tools** für maschinenlesbare Veröffentlichung.
- **EN:** Procedures for early warning (24 hours), update notification
  (72 hours), and final report to CSIRT/ENISA are in place.
  CRA Art. 14 — reporting obligations:
  - **24 hours** after becoming aware of an actively exploited
    vulnerability: early warning to ENISA + national CSIRT.
  - **72 hours**: vulnerability notification with detailed description,
    severity, affected versions, mitigation.
  - **14 days** after patch availability: final report for actively exploited
    vulnerabilities with root cause, measures taken, and lessons learned.
  - **One month** after the 72-hour notification: final report for severe
    incidents.
  - **24 hours** for severe security incident (incident notification) –
    separate from vulnerability notification.
  Required procedures:
  - **Crisis runbook** with escalation chain: engineer -> lead -> Security Lead ->
    management.
  - **24/7 on-call** with defined schedule (e.g. PagerDuty, Opsgenie).
  - **Communication matrix**: who notifies ENISA, BSI, customers,
    press, legal.
  - **Template texts** in DE and EN for ENISA and BSI notifications.
  - **Decision tree** for classification: actively exploited / potentially
    exploitable / no risk.
  - **Exercises** at least semi-annually (see vulnerability checklist
    item 11).
  Reporting channels:
  - **ENISA Single Reporting Platform** (planned from 2026).
  - **BSI CSIRT-Bund**: `cert@bsi.bund.de`,
    [BSI reporting form](https://www.bsi.bund.de/DE/Service/Meldestelle).
  - **National CSIRTs** per member state (CSIRT Network).
  - **CSAF document** for machine-readable notifications.
  Mandatory fields of an early warning:
  - Manufacturer identification.
  - Product identification (name, version).
  - Short description of the vulnerability/incident.
  - Indicators of active exploitation (IOCs, source).
  - Preliminary CVSS score.
  - Known mitigations or workaround.
  - Contact person + 24/7 number.
  Tools:
  - **TheHive / Cortex** for incident-response platform.
  - **MISP** for threat-intelligence sharing.
  - **CSAF provider tools** for machine-readable publication.
- **Akzeptanz / Acceptance:** Krisen-Runbook in
  `docs/security/incident-response.md`; 24/7-On-Call-Schedule
  konfiguriert; Vorlagentexte DE/EN; Decision-Tree für 24/72-h-Klassifikation;
  letzte Übung ≤ 6 Monate.
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

#### CL-07-10: Lebenszyklus und Support / Lifecycle and Support

- **DE:** Die erwartete Nutzungsdauer (Expected Product Lifetime) und der
  Support-Zeitraum sind festgelegt und öffentlich kommuniziert. Gemäß CRA
  Art. 13(8) sind Sicherheits-Updates mindestens über den Support-Zeitraum
  oder mindestens 5 Jahre (je nachdem, was länger ist) bereitzustellen.
  Lifecycle-Modelle: Semantic Versioning (Major/Minor/Patch) mit klarer
  Deprecation-Policy; Long-Term-Support (LTS) für stabile Versionen mit
  definiertem Sicherheits-Patch-Zeitraum (z. B. Ubuntu LTS 5–10 Jahre, .NET
  LTS 3 Jahre); N/N-1-Support (aktuelle und vorherige Major-Version).
  Beispieltabelle: `v1.0` Released 2025-01, EOS (End of Sale) 2027-01, EOL
  (End of Life) 2030-01, EOSS (End of Security Support) 2030-01. Mindestens
  12 Monate vor EOL angekündigt (EOL-Notice). Kommunikationskanäle:
  Produkt-Website unter `Lifecycle Policy`, Release Notes, Customer Portal,
  E-Mail-Verteiler, Security Advisories. Migrationsleitfaden zur Nachfolge-
  Version mitgeliefert. Einstellung der Sicherheits-Updates dokumentiert
  in `docs/lifecycle-policy.md` mit Versionsmatrix.
- **EN:** Expected product lifetime and support period are defined and
  publicly communicated. Per CRA Art. 13(8), security updates must be
  provided for at least the support period or at least 5 years (whichever
  is longer). Lifecycle models: Semantic Versioning (Major/Minor/Patch)
  with clear deprecation policy; Long-Term Support (LTS) for stable
  versions with defined security patch window (e.g. Ubuntu LTS 5–10 years,
  .NET LTS 3 years); N/N-1 support (current and previous major version).
  Example table: `v1.0` Released 2025-01, EOS (End of Sale) 2027-01, EOL
  (End of Life) 2030-01, EOSS (End of Security Support) 2030-01. Announced
  at least 12 months before EOL (EOL notice). Communication channels:
  product website under `Lifecycle Policy`, release notes, customer
  portal, email distribution list, security advisories. Migration guide
  to successor version provided. End of security updates documented in
  `docs/lifecycle-policy.md` with version matrix.
- **Akzeptanz / Acceptance:** `docs/lifecycle-policy.md` mit Versions-
  Matrix (Released, EOS, EOL, EOSS); öffentliche Lifecycle-Seite mit
  Mindestsupport ≥ 5 Jahre gemäß CRA Art. 13(8); EOL-Notice ≥ 12 Monate
  vor Abschaltung; Migrationsleitfaden für Nachfolge-Version. /
  `docs/lifecycle-policy.md` with version matrix (Released, EOS, EOL,
  EOSS); public lifecycle page with minimum support ≥ 5 years per CRA
  Art. 13(8); EOL notice ≥ 12 months before shutdown; migration guide
  for successor version.
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

#### CL-07-11: Hersteller-Pflichten und -Verträge / Manufacturer Duties and Contracts

- **DE:** Verträge mit Zulieferern (Komponenten-Lieferanten, OSS-Wartungs-
  vertragspartner, Dienstleister) und mit dem Vertrieb (Distributoren,
  Importeure, Reseller) decken die CRA-Pflichten ab. Verbindliche
  Vertragsklauseln für Zulieferer: (1) Schwachstellenmeldung an Hersteller
  innerhalb von 24 h nach Kenntnis; (2) Bereitstellung von
  Sicherheits-Updates mindestens über den Produkt-Lifecycle des
  Endprodukts; (3) SBOM-Lieferung mit jeder Komponentenversion (CycloneDX
  oder SPDX); (4) Audit-Recht für CRA-Konformitätsprüfung; (5) Haftung und
  Gewährleistung für Sicherheitsmängel; (6) Coordinated Vulnerability
  Disclosure (CVD) Policy; (7) Right to Repair / Source-Code-Hinterlegung
  bei Insolvenz (Escrow). Klauseln für Distributoren/Importeure (CRA Art.
  19, 20): Pflicht zur Konformitätsprüfung, Aufbewahrung der Konformitäts-
  erklärung, Weiterleitung von Sicherheitsmeldungen, Nichtinverkehrbringen
  bei Bedenken. Vertragsvorlagen verfügbar als `vendor-security-clauses.md`
  und `distributor-cra-clauses.md`. Bestehende Verträge per Addendum oder
  Vertragsänderung an CRA-Pflichten angepasst. Lieferantenmanagement-
  Datenbank (z. B. SAP Ariba, Coupa) hält Compliance-Status pro Lieferant.
- **EN:** Contracts with suppliers (component suppliers, OSS maintenance
  partners, service providers) and with distributors (distributors,
  importers, resellers) cover the CRA duties. Mandatory contractual
  clauses for suppliers: (1) vulnerability notification to the manufacturer
  within 24 h of knowledge; (2) security updates for at least the
  end-product lifecycle; (3) SBOM delivery with every component version
  (CycloneDX or SPDX); (4) audit right for CRA conformity; (5) liability
  and warranty for security defects; (6) Coordinated Vulnerability
  Disclosure (CVD) policy; (7) right to repair / source-code escrow on
  insolvency. Clauses for distributors/importers (CRA Art. 19, 20):
  conformity check duty, retention of declaration of conformity,
  forwarding of security notifications, non-placement on concerns.
  Contract templates available as `vendor-security-clauses.md` and
  `distributor-cra-clauses.md`. Existing contracts adapted via addendum
  or amendment to CRA duties. Vendor management database (e.g. SAP Ariba,
  Coupa) holds compliance status per supplier.
- **Akzeptanz / Acceptance:** Vertragsklauseln in `vendor-security-clauses.md`
  und `distributor-cra-clauses.md`; alle bestehenden Verträge mit
  CRA-relevantem Bezug aktualisiert oder per Addendum erweitert;
  Lieferantenliste mit CRA-Compliance-Status gepflegt. /
  Contractual clauses in `vendor-security-clauses.md` and
  `distributor-cra-clauses.md`; all existing contracts with CRA-relevant
  scope updated or extended via addendum; supplier list with CRA
  compliance status maintained.
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

#### CL-07-12: Nichtanwendbarkeit dokumentieren / Document Non-Applicability

- **DE:** Wenn der CRA für ein Produkt oder eine Komponente nicht anwendbar
  ist, wird dies im Anwendbarkeitsbericht (`docs/cra-applicability.md`) mit
  ausführlicher Begründung festgehalten. Die Entscheidung verweist auf das
  regulatorische Screening nach CL_Standards-Anwendbarkeit. Pflichtfelder
  pro Eintrag: (1)
  Produktname und Version; (2) Begründung der Nichtanwendbarkeit mit
  Verweis auf konkrete CRA-Ausnahmeklausel (z. B. Art. 2 Abs. 2: SaaS-
  Dienste; Art. 2 Abs. 3: MDR/IVDR-Medizinprodukte; Art. 2 Abs. 4:
  Verteidigungsprodukte; Art. 2 Abs. 5: Produkte rein für nationale
  Sicherheit); (3) Datum der Bewertung; (4) Verantwortliche Person mit
  Name und Funktion; (5) Vier-Augen-Prüfung (zweite Person); (6) Datum der
  nächsten Überprüfung (mindestens jährlich oder bei Produktänderung); (7)
  Rechtsbeistand oder Compliance-Officer hinzugezogen (ja/nein). Beispiel-
  Eintrag: „Produkt: <interner oder externer Cloud-Storage-SaaS-Dienst> — Begründung: SaaS-Dienst
  ohne Bereitstellung als Produkt mit digitalen Elementen, fällt unter
  NIS2-Richtlinie (RL (EU) 2022/2555) Art. 21, nicht unter CRA Art. 1.
  Bewertet am <Datum> durch <Rolle 1>, gegengeprüft durch
  <Rolle 2>, nächste Überprüfung <Datum>.
  Rechtsbeistand oder Compliance-Rolle konsultiert: ja/nein, Aktenzeichen <Referenz>." Stillschwei-
  gende Auslassung (kein Eintrag) ist nicht zulässig.
- **EN:** If the CRA is not applicable to a product or component, this is
  recorded in the applicability report (`docs/cra-applicability.md`) with
  detailed justification. The decision references the regulatory screening
  under CL_Standards-Anwendbarkeit. Mandatory fields per entry: (1) product name
  and version; (2) justification with reference to the specific CRA
  exemption clause (e.g. Art. 2(2): SaaS services; Art. 2(3): MDR/IVDR
  medical devices; Art. 2(4): defence products; Art. 2(5): national
  security only); (3) date of assessment; (4) responsible person with
  name and function; (5) four-eyes review (second person); (6) date of
  next review (at least annually or on product change); (7) legal counsel
  or compliance officer consulted (yes/no). Example entry: „Product: <cloud
  storage SaaS service> — Justification: SaaS service without provision as a
  product with digital elements, falls under NIS2 Directive (Dir. (EU)
  2022/2555) Art. 21, not under CRA Art. 1. Assessed on <date> by
  <responsible role>, countersigned by <second reviewer role>, next review
  <date>. Legal department consulted: yes/no, reference <reference>." Silent
  omission (no entry) is not permitted.
- **Akzeptanz / Acceptance:** `docs/cra-applicability.md` enthält je
  ausgenommenes Produkt einen Eintrag mit allen sieben Pflichtfeldern
  (Produkt+Version, CRA-Ausnahmeklausel, Datum, verantwortliche Person,
  Vier-Augen-Prüfer, nächste Überprüfung, Rechtsbeistand-Konsultation);
  jährliche Wiedervorlage aktiv; CL_01-Regulatory-Screening ist verlinkt
  oder inhaltlich gleichwertig enthalten. / `docs/cra-applicability.md` contains
  one entry per exempted product with all seven mandatory fields
  (product+version, CRA exemption clause, date, responsible person,
  four-eyes reviewer, next review, legal counsel consultation); annual
  re-review active; CL_01 regulatory screening is linked or included in an
  equivalent way.
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

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Erfüllt, wenn alle Punkte abgeschlossen sind und das CRA-Dossier
revisionsfest abgelegt ist.

**EN:** Fulfilled when every item is closed and the CRA dossier is stored
in an audit-ready way.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-07-glossar-cra"></a>

#### CRA / Cyber Resilience Act

- **DE:** Der Cyber Resilience Act ist eine EU-Verordnung für Produkte mit digitalen Elementen. Er verlangt Sicherheitsanforderungen, Schwachstellenbehandlung und bestimmte Meldungen.
- **EN:** The Cyber Resilience Act is an EU regulation for products with digital elements. It requires security requirements, vulnerability handling, and certain reports.

<a id="cl-07-glossar-product-digital-elements"></a>

#### Produkt mit digitalen Elementen / Product with Digital Elements

- **DE:** Ein Produkt mit digitalen Elementen ist Software oder Hardware mit Softwarebezug, die unter den CRA fallen kann.
- **EN:** A product with digital elements is software or hardware with software relevance that may fall under the CRA.

<a id="cl-07-glossar-critical-product"></a>

#### Kritisches Produkt / Critical Product

- **DE:** Ein kritisches Produkt ist nach CRA besonders sicherheitsrelevant. Die Einstufung kann strengere Prüf- und Dokumentationspflichten auslösen.
- **EN:** A critical product is especially security-relevant under the CRA. The classification can trigger stricter review and documentation duties.

<a id="cl-07-glossar-conformity-assessment"></a>

#### Konformitätsbewertung / Conformity Assessment

- **DE:** Eine Konformitätsbewertung prüft, ob ein Produkt die geltenden Anforderungen erfüllt. Beim CRA kann sie Voraussetzung für die Bereitstellung sein.
- **EN:** A conformity assessment checks whether a product meets applicable requirements. Under the CRA it can be required before making the product available.

<a id="cl-07-glossar-ce-marking"></a>

#### CE-Kennzeichnung / CE Marking

- **DE:** Die CE-Kennzeichnung zeigt, dass ein Produkt nach Erklärung des Herstellers die einschlägigen EU-Anforderungen erfüllt.
- **EN:** CE marking shows that, according to the manufacturer’s declaration, a product meets the relevant EU requirements.

<a id="cl-07-glossar-technical-documentation"></a>

#### Technische Dokumentation / Technical Documentation

- **DE:** Technische Dokumentation beschreibt Produkt, Architektur, Risiken, Sicherheitsmaßnahmen, Tests und Nachweise so, dass Prüfung und Wartung möglich sind.
- **EN:** Technical documentation describes product, architecture, risks, security measures, tests, and evidence so that review and maintenance are possible.

<a id="cl-07-glossar-sbom"></a>

#### SBOM / Software Bill of Materials

- **DE:** Eine SBOM ist eine Stückliste für Software. Sie nennt Komponenten, Versionen und oft Lizenzen, damit Risiken und Schwachstellen schneller gefunden werden.
- **EN:** An SBOM is a bill of materials for software. It lists components, versions, and often licences so that risks and vulnerabilities can be found faster.

<a id="cl-07-glossar-vulnerability-handling"></a>

#### Schwachstellenbehandlung / Vulnerability Handling

- **DE:** Schwachstellenbehandlung umfasst Erkennen, Bewerten, Beheben und Kommunizieren von Schwachstellen. Sie braucht klare Zuständigkeiten und Fristen.
- **EN:** Vulnerability handling covers identifying, assessing, fixing, and communicating vulnerabilities. It needs clear responsibilities and deadlines.

<a id="cl-07-glossar-nicht-anwendbar"></a>

#### Nicht anwendbar / N/A

- **DE:** Nicht anwendbar bedeutet, dass ein Prüfpunkt sachlich nicht zum Projekt passt. Diese Entscheidung braucht immer eine kurze und nachvollziehbare Begründung.
- **EN:** Not applicable means that a review item does not fit the project. This decision always needs a short and traceable reason.

<a id="cl-07-glossar-evidenz"></a>

#### Evidenz / Evidence

- **DE:** Evidenz ist ein prüfbarer Nachweis. Das kann ein Link, Ticket, Scan-Bericht, Pull Request, Protokoll, Architekturdiagramm oder Dokument sein.
- **EN:** Evidence is verifiable proof. It can be a link, ticket, scan report, pull request, record, architecture diagram, or document.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-04-30):** CRA-Stichtage und Schlussbericht-Fristen konkretisiert / Clarified CRA key dates and final-report deadlines
- **Version 1.3 (2026-06-15):** Nichtanwendbarkeitsentscheidung mit CL_01-Regulatory-Screening verknüpft; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Linked non-applicability decisions to CL_01 regulatory screening; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.4 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.5 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
