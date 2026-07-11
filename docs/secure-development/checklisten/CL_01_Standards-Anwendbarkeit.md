<!--
Quelle / Source: generische Ausbildungs- und Pruefgrundlage, bereinigt am 2026-06-17.
Dieses Dokument ist organisationsneutral und als generische Ausbildungs- und Pruefgrundlage formuliert.
Source: generic training and review baseline, generalized on 2026-06-17.
This document is organization-neutral and written as a generic training and review baseline.
-->

> **DE:** Diese Checkliste ist generisch und projektunabhaengig. Sie ist als Ausbildungs-, Review- und Haertungsgrundlage gedacht. Eine Nichtanwendbarkeit muss als `N/A` mit kurzer Begruendung dokumentiert werden.
>
> **EN:** This checklist is generic and project-independent. It is intended as a training, review, and hardening baseline. Non-applicability must be documented as `N/A` with a short rationale.

## Checkliste 01 – Standards-Anwendbarkeit / Standards Applicability

**Dokument-ID / Document ID:** CL-01
**Version / Version:** 2.0.0
**Baseline-Version / Baseline version:** 3.0.0
**Dokumenttyp / Document type:** Kanonische, wiederverwendbare Pruefvorlage / Canonical reusable review template

### Nachweisinstanz / Evidence Instance

Diese Datei ist eine wiederverwendbare Vorlage. Ausgefüllte Projektnachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person und Reviewer. / This file is a reusable template. Completed project evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, and reviewer.

### Zweck / Purpose

**DE:** Diese Checkliste hilft dem Projektteam, die anwendbaren Sicherheitsstandards
für ein Software- oder Service-Vorhaben zu bestimmen und die Auswahl
nachvollziehbar zu dokumentieren. Sie ist mitgeltend zur Richtlinie „Sichere
Entwicklung" und unterstützt die Audit-Vorbereitung für ISO/IEC 27001:2022.

**EN:** This checklist helps the project team identify the applicable security
standards for a software or service project and document the choice in a
traceable way. It supports the guideline "Secure Development" and helps prepare
audits for ISO/IEC 27001:2022.

### Geltungsbereich / Scope

**DE:** Gilt für alle neuen und bestehenden Entwicklungsvorhaben, die unter die
Richtlinie „Sichere Entwicklung" fallen. Anwendung zu Projektbeginn und bei
jeder größeren Änderung der Architektur oder des Liefermodells.

**EN:** Applies to all new and existing development projects covered by the
"Secure Development" guideline. Use at project start and after every major
change in architecture or delivery model.

### Mitgeltende Dokumente / Related Documents

- Richtlinie Sichere Entwicklung
- ISO/IEC 27001:2022 Annex A.5.8, A.8.25–A.8.33
- NIST SP 800-218 (SSDF)
- OWASP ASVS, OWASP SAMM
- NIST SP 800-207 (Zero Trust)
- Richtlinie (EU) 2022/2555 (NIS2)
- Verordnung (EU) 2022/2554 (DORA)
- Verordnung (EU) 2024/1689 (EU AI Act)

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
- **NIS2-Richtlinie:** [EUR-Lex Richtlinie (EU) 2022/2555 / EUR-Lex Directive (EU) 2022/2555](https://eur-lex.europa.eu/eli/dir/2022/2555/oj)
- **DORA:** [EUR-Lex Verordnung (EU) 2022/2554 / EUR-Lex Regulation (EU) 2022/2554](https://eur-lex.europa.eu/eli/reg/2022/2554/oj)
- **EU AI Act:** [EUR-Lex Verordnung (EU) 2024/1689 / EUR-Lex Regulation (EU) 2024/1689](https://eur-lex.europa.eu/eli/reg/2024/1689/oj)
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

**DE:** Ein internes Web-Portal mit Login wählt ASVS Level 2. SBOM ist erforderlich, weil ein Release-Artefakt gebaut wird. OpenSSF Scorecard ist „nicht anwendbar", wenn keine öffentliche OSS-Abhängigkeit bewertet wird; die Begründung nennt dann die betroffenen internen Repositories.

**EN:** An internal web portal with login selects ASVS Level 2. An SBOM is required because a release artefact is built. OpenSSF Scorecard is "not applicable" if no public OSS dependency is assessed; the explanation then names the affected internal repositories.

### A11Y-Hinweise / A11Y Notes

**DE:** Beim Ausfüllen dieser Checkliste müssen alle Nachweise auch textlich verständlich sein. Verweise sollen beschreibende Linktexte haben. Screenshots, Diagramme oder Scan-Auszüge brauchen eine kurze Textbeschreibung. Der Status darf nicht nur über Farbe erkennbar sein.

**EN:** When this checklist is filled in, all evidence must also be understandable as text. References should use descriptive link text. Screenshots, diagrams, or scan extracts need a short text description. The status must not be shown by color alone.

### Wichtige Begriffe / Key Terms

**DE:** Die folgenden Begriffe kommen in dieser Checkliste vor. Die Links springen zum Glossar dieses Kapitels, damit Auszubildende und Entwickler:innen ohne Sicherheits-Spezialwissen die Begriffe direkt nachlesen können.

**EN:** The following terms appear in this checklist. The links jump to this chapter's glossary so that apprentices and developers without specialist security knowledge can look them up directly.

- [ISO/IEC 27001](#cl-01-glossar-iso-27001)
- [NIST SSDF](#cl-01-glossar-nist-ssdf)
- [CWE Top 25](#cl-01-glossar-cwe-top-25)
- [OWASP ASVS](#cl-01-glossar-owasp-asvs)
- [OWASP SAMM](#cl-01-glossar-owasp-samm)
- [SBOM / Software Bill of Materials](#cl-01-glossar-sbom)
- [VEX / Vulnerability Exploitability eXchange](#cl-01-glossar-vex)
- [SLSA](#cl-01-glossar-slsa)
- [Zero Trust](#cl-01-glossar-zero-trust)
- [CAPEC](#cl-01-glossar-capec)
- [OpenSSF Scorecard](#cl-01-glossar-openssf-scorecard)
- [CRA / Cyber Resilience Act](#cl-01-glossar-cra)
- [NIS2](#cl-01-glossar-nis2)
- [DORA](#cl-01-glossar-dora)
- [EU AI Act](#cl-01-glossar-eu-ai-act)
- [SoA / Statement of Applicability / Anwendbarkeitserklärung](#cl-01-glossar-soa)
- [Nicht anwendbar / N/A](#cl-01-glossar-nicht-anwendbar)

### Checkliste / Checklist

#### CL-01-01: NIST SSDF und CWE Top 25 / NIST SSDF and CWE Top 25

- **DE:** Dieser Punkt legt die Sicherheitsbasis für jedes Projekt fest.
  NIST SSDF (NIST SP 800-218) beschreibt, wie sichere Software
  geplant, gebaut und gepflegt wird. Die CWE Top 25 sind eine Liste
  häufiger und gefährlicher Programmierfehler, zum Beispiel Cross-Site
  Scripting, SQL Injection, unsichere Speicherzugriffe und Path
  Traversal. Für das Projekt bedeutet das: Im Projektplan steht,
  welche SSDF-Praktiken genutzt werden. In der Code-Review-Vorlage,
  im Linter oder im Security-Scan steht, wie die CWE Top 25 geprüft
  werden.
- **EN:** This item defines the security baseline for every project.
  NIST SSDF (NIST SP 800-218) describes how secure software is planned,
  built, and maintained. The CWE Top 25 are a list of common and
  dangerous programming mistakes, for example cross-site scripting,
  SQL injection, unsafe memory access, and path traversal. For the
  project this means: the project plan states which SSDF practices are
  used. The code review template, linter, or security scan states how
  the CWE Top 25 are checked.
- **Mindestprüfung / Minimum check:** PO.1 dokumentiert
  Sicherheitsanforderungen im Plan. PS.1 schützt die Integrität des
  Quellcodes, zum Beispiel durch signierte Commits (`git commit -S`,
  GPG oder Sigstore). PW.4 verankert sichere Codierungsregeln im
  Style-Guide. PW.7 verlangt Code-Review mit Sicherheits-Fokus. PW.8
  nutzt statische Analyse, zum Beispiel Semgrep, SonarQube oder
  CodeQL. RV.1 verfolgt Schwachstellen in einem Issue-System mit
  Reaktionsfrist. / PO.1 documents security requirements in the plan.
  PS.1 protects source-code integrity, for example with signed commits
  (`git commit -S`, GPG, or Sigstore). PW.4 places secure coding rules
  in the style guide. PW.7 requires security-focused code review. PW.8
  uses static analysis, for example Semgrep, SonarQube, or CodeQL.
  RV.1 tracks vulnerabilities in an issue system with a response time.
- **Akzeptanz / Acceptance:** Der Projektplan nennt die genutzten
  SSDF-Praktiken mit ID und kurzer Alltagserklärung. Die Code-Review-
  Vorlage, der Linter-Regelsatz oder der Scan-Bericht zeigt, wie die
  CWE Top 25 geprüft werden. / The project plan lists the used SSDF
  practices with ID and a short plain-language explanation. The code
  review template, linter rule set, or scan report shows how the CWE
  Top 25 are checked.
- **Referenz / Reference:** ISO 27002 A.8.28; Verfassung XII, XIV.
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

#### CL-01-02: OWASP ASVS-Level / OWASP ASVS Level

- **DE:** Für Web-, API-, HTTP- oder authentifizierte Dienste den ASVS-Level
  (Application Security Verification Standard, derzeit stabile Version 5.0.0)
  festlegen. **L1** passt für einfache interne Hilfswerkzeuge,
  Read-Only-Dashboards und geringes Risiko. **L2** ist der Standard für
  regelmäßig betriebene Fachanwendungen mit personen- oder geschäftsbezogenen
  Daten. **L3** ist für besonders sensible Dienste vorgesehen, zum Beispiel
  Finanztransaktionen, Gesundheitsdaten oder kritische Infrastruktur.
  Die erfüllten und offenen Anforderungen werden mit versionierten ASVS-IDs
  dokumentiert, zum Beispiel `v5.0.0-1.2.5`. Werkzeuge: ASVS-Checkliste von
  OWASP, ASVS-Tracker in Jira oder Git, Mapping in Bedrohungsmodellen.
- **EN:** For web, API, HTTP, or authenticated services pick the ASVS level
  (Application Security Verification Standard, currently stable version 5.0.0).
  **L1** fits simple internal helpers, read-only dashboards, and low risk.
  **L2** is the default for regularly operated business applications with
  personal or business data. **L3** is for highly sensitive services such as
  financial transactions, healthcare data, or critical infrastructure.
  Fulfilled and open requirements are documented with versioned ASVS IDs,
  for example `v5.0.0-1.2.5`. Tooling: ASVS checklist from OWASP, ASVS tracker
  in Jira or Git, mapping in threat models.
- **Akzeptanz / Acceptance:** Level (L1/L2/L3) und Begründung in
  `docs/security/asvs-verification.md`; Mapping der erfüllten und offenen
  ASVS-IDs mit Versionspräfix vorhanden. / Level (L1/L2/L3) and justification
  in `docs/security/asvs-verification.md`; mapping of fulfilled and open ASVS
  IDs with version prefix available.
- **Referenz / Reference:** Verfassung XV.
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

#### CL-01-03: SBOM / Software Bill of Materials

- **DE:** Für jedes auslieferbare Artefakt eine SBOM (Software Bill of
  Materials) in CycloneDX (JSON `bom.json` oder XML, OWASP-Format) oder
  SPDX (JSON `*.spdx.json` oder Tag-Value, Linux-Foundation-Format)
  pflegen. SBOM bei jedem Release aktualisieren und mitveröffentlichen.
  Werkzeuge: `syft` (Anchore, sprachübergreifend), `cdxgen` (CycloneDX
  Generator, viele Sprachen), `cyclonedx-bom` (Python),
  `cyclonedx-dotnet` (.NET), `cyclonedx-maven-plugin` (Java),
  `@cyclonedx/cyclonedx-npm` (Node.js), `cyclonedx-gomod` (Go).
  Container-SBOM mit `docker sbom` (Docker Desktop), `syft` oder
  `trivy sbom`. Pflichtfelder: Komponenten-Name, Version, Lizenz,
  Hash (SHA-256), Lieferant, Beziehungs-Typ. Die SBOM wird im
  CI/CD-Pipeline-Build erzeugt, signiert (z. B. Cosign) und zusammen
  mit dem Artefakt veröffentlicht.
- **EN:** Maintain a CycloneDX (JSON `bom.json` or XML, OWASP format) or
  SPDX (JSON `*.spdx.json` or tag-value, Linux Foundation format) SBOM
  for every shipped artefact. Refresh and publish the SBOM at every
  release. Tooling: `syft` (Anchore, multi-language), `cdxgen`
  (CycloneDX Generator, many languages), `cyclonedx-bom` (Python),
  `cyclonedx-dotnet` (.NET), `cyclonedx-maven-plugin` (Java),
  `@cyclonedx/cyclonedx-npm` (Node.js), `cyclonedx-gomod` (Go).
  Container SBOM via `docker sbom` (Docker Desktop), `syft`, or
  `trivy sbom`. Required fields: component name, version, licence,
  hash (SHA-256), supplier, relationship type. The SBOM is generated
  in the CI/CD pipeline build, signed (e.g. Cosign), and published
  alongside the artefact.
- **Akzeptanz / Acceptance:** SBOM-Datei im Release-Artefakt; Format
  (CycloneDX oder SPDX), Erzeugungs-Werkzeug, Signaturpfad in
  `docs/security/supply-chain-evidence.md`. / SBOM file in the release
  artefact; format (CycloneDX or SPDX), generation tool, signature
  path in `docs/security/supply-chain-evidence.md`.
- **Referenz / Reference:** ISO 27002 A.8.30; Verfassung XVI.
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

#### CL-01-04: VEX / Vulnerability Exploitability eXchange

- **DE:** VEX (Vulnerability Exploitability eXchange) ist ein maschinen-
  lesbares Dokument, das je CVE/Komponente einen Status nennt:
  **not_affected** (Code wird nicht ausgeführt), **affected** (verwundbar),
  **fixed** (Patch eingespielt), **under_investigation**. Formate:
  CSAF 2.0 (OASIS, JSON-Schema), CycloneDX VEX (Inline in der SBOM oder
  separat), OpenVEX (Spezifikation der OpenSSF). Werkzeuge: `vexctl`
  (Chainguard) zum Erzeugen und Signieren; `csaf-tools` für CSAF;
  Dependency-Track als zentrale Plattform mit VEX-Aufnahme; Trivy und
  Grype können VEX zum Filtern unterdrückbarer Findings verwenden.
  Begründung im VEX nennt den `justification`-Code (z. B.
  `vulnerable_code_not_in_execute_path`,
  `vulnerable_code_cannot_be_controlled_by_adversary`,
  `inline_mitigations_already_exist`). Pflicht, sobald ein Scan eine
  CVE in einer Lieferkomponente meldet, die nicht unmittelbar gepatcht
  wird.
- **EN:** VEX (Vulnerability Exploitability eXchange) is a machine-
  readable document that states a status per CVE/component:
  **not_affected** (code not executed), **affected** (vulnerable),
  **fixed** (patch applied), **under_investigation**. Formats: CSAF 2.0
  (OASIS, JSON schema), CycloneDX VEX (inline in the SBOM or separate),
  OpenVEX (OpenSSF specification). Tooling: `vexctl` (Chainguard) to
  generate and sign; `csaf-tools` for CSAF; Dependency-Track as a
  central platform with VEX intake; Trivy and Grype can use VEX to
  filter suppressed findings. The justification cites a
  `justification` code (e.g.
  `vulnerable_code_not_in_execute_path`,
  `vulnerable_code_cannot_be_controlled_by_adversary`,
  `inline_mitigations_already_exist`). Required as soon as a scan
  reports a CVE in a shipped component that is not immediately patched.
- **Akzeptanz / Acceptance:** VEX-Datei im Release-Artefakt mit Format,
  Pfad, Justification-Codes, oder N/A mit kurzer Begründung. / VEX
  file in the release artefact with format, path, justification codes,
  or N/A with short justification.
- **Referenz / Reference:** Verfassung XVI.
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

#### CL-01-05: SLSA-Build-Level / SLSA Build Level

- **DE:** SLSA v1.2 trennt Anforderungen in Tracks. Für diese Richtlinie ist
  der Build-Track maßgeblich. **Build L1** bedeutet: Provenance ist vorhanden
  und beschreibt Artefakt, Quelle und Build. **Build L2** ergänzt einen
  gehosteten Build-Dienst und signierte, überprüfbare Provenance.
  **Build L3** verlangt eine gehärtete Build-Plattform mit stärkerer Isolation
  und nicht fälschbarer Provenance. Das Ziel-Level wird je Release festgelegt.
  Werkzeuge: `slsa-verifier`, Sigstore `cosign verify-attestation`,
  in-toto-Attestations und geeignete SLSA-Generatoren für die jeweilige CI.
  Die Attestation wird neben dem Artefakt veröffentlicht.
- **EN:** SLSA v1.2 separates requirements into tracks. This guideline uses
  the Build track. **Build L1** means that provenance exists and describes the
  artefact, source, and build. **Build L2** adds a hosted build service and
  signed, verifiable provenance. **Build L3** requires a hardened build
  platform with stronger isolation and non-forgeable provenance. The target
  level is selected per release. Tooling: `slsa-verifier`, Sigstore
  `cosign verify-attestation`, in-toto attestations, and suitable SLSA
  generators for the CI system. Publish the attestation alongside the artefact.
- **Akzeptanz / Acceptance:** Ziel-SLSA-Build-Level (Build L1, Build L2 oder
  Build L3), Builder-Werkzeug, Pfad zur Attestation und Verifikationsbefehl in
  `docs/security/supply-chain-evidence.md`. / Target SLSA Build level
  (Build L1, Build L2, or Build L3), builder tool, path to the attestation,
  and verification command in `docs/security/supply-chain-evidence.md`.
- **Referenz / Reference:** Verfassung XVI.
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

#### CL-01-06: Zero Trust / Zero Trust

- **DE:** Zero Trust nach NIST SP 800-207 ersetzt das implizite Vertrauen
  innerhalb eines Netzes durch explizite Prüfung jeder Zugriffs-
  Anforderung. Sieben Prinzipien: alle Datenquellen und Dienste sind
  Ressourcen; jede Kommunikation wird geschützt; Zugriff je Sitzung;
  dynamische Policy auf Basis von Identität, Gerätezustand und
  Verhalten; Asset-Integrität wird kontinuierlich gemessen;
  Authentifizierung und Autorisierung vor jedem Zugriff; ständiges
  Verbessern der Sicherheitslage. Konkrete Bausteine: Identitäts-
  Provider mit MFA und kurzlebigen Tokens (z. B. Azure AD/Entra,
  Okta, Keycloak); Service-Mesh mit mTLS (Istio, Linkerd, Consul);
  BeyondCorp- oder ZTNA-Gateway statt VPN (Cloudflare Access, Tailscale,
  Pomerium, Teleport); SPIFFE/SPIRE für maschinelle Identitäten; OPA/
  Rego für Policy-Engines; eBPF-basiertes Monitoring (Cilium Tetragon,
  Falco). Bei Mikroservices, Cloud, Remote-Verwaltung oder
  Hybrid-Setups ist Zero Trust ausdrücklich zu prüfen.
- **EN:** Zero Trust per NIST SP 800-207 replaces implicit trust within a
  network with explicit verification of every access request. Seven
  tenets: all data sources and services are resources; all
  communication is protected; access per session; dynamic policy
  based on identity, device posture, and behaviour; asset integrity
  is continuously measured; authentication and authorization before
  every access; continuous improvement of the security posture.
  Concrete building blocks: identity provider with MFA and short-lived
  tokens (e.g. Azure AD/Entra, Okta, Keycloak); service mesh with mTLS
  (Istio, Linkerd, Consul); BeyondCorp- or ZTNA-gateway instead of VPN
  (Cloudflare Access, Tailscale, Pomerium, Teleport); SPIFFE/SPIRE
  for machine identities; OPA/Rego for policy engines; eBPF-based
  monitoring (Cilium Tetragon, Falco). For microservices, cloud,
  remote management, or hybrid setups, Zero Trust must be explicitly
  evaluated.
- **Akzeptanz / Acceptance:** `docs/security/zero-trust-applicability.md`
  mit Bewertung pro Prinzip, gewählten Werkzeugen oder N/A mit
  Begründung. / `docs/security/zero-trust-applicability.md` with
  assessment per tenet, chosen tooling, or N/A with justification.
- **Referenz / Reference:** Verfassung XVIII.
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

#### CL-01-07: CAPEC im Bedrohungsmodell / CAPEC in the Threat Model

- **DE:** CAPEC (Common Attack Pattern Enumeration and Classification, MITRE)
  bietet einen Katalog von ca. 600 Angriffsmustern, hierarchisch von
  Meta-Mustern (z. B. CAPEC-1000 „Mechanism of Attack") bis zu detaillierten
  Mustern (z. B. CAPEC-66 SQL Injection, CAPEC-242 Code Injection,
  CAPEC-63 Cross-Site Scripting, CAPEC-94 Adversary in the Middle,
  CAPEC-115 Authentication Bypass, CAPEC-22 Exploiting Trust in Client,
  CAPEC-148 Content Spoofing). Im Bedrohungsmodell wird je STRIDE-Kategorie
  und je Trust-Boundary mindestens ein passendes CAPEC-Muster als
  Angriffsweg benannt; je Muster werden Voraussetzungen, Skill-Level
  des Angreifers, mögliche Auswirkungen und Gegenmaßnahmen dokumentiert.
  Werkzeuge: MITRE-CAPEC-Webseite (HTML- und XML-Export); ATT&CK-CAPEC-
  Mapping für TTP-Verbindung; Threat Modeling Tool (Microsoft) und
  pytm (Python) unterstützen CAPEC-Annotation.
- **EN:** CAPEC (Common Attack Pattern Enumeration and Classification,
  MITRE) is a catalogue of ~600 attack patterns, hierarchical from meta
  patterns (e.g. CAPEC-1000 "Mechanism of Attack") to detailed patterns
  (e.g. CAPEC-66 SQL Injection, CAPEC-242 Code Injection, CAPEC-63
  Cross-Site Scripting, CAPEC-94 Adversary in the Middle, CAPEC-115
  Authentication Bypass, CAPEC-22 Exploiting Trust in Client,
  CAPEC-148 Content Spoofing). In the threat model, at least one
  matching CAPEC pattern is named as an attack path per STRIDE
  category and per trust boundary; per pattern, preconditions,
  attacker skill level, possible impacts, and mitigations are
  documented. Tooling: MITRE CAPEC website (HTML and XML export);
  ATT&CK-CAPEC mapping for TTP linkage; Threat Modeling Tool
  (Microsoft) and pytm (Python) support CAPEC annotation.
- **Akzeptanz / Acceptance:** Bedrohungsmodell nennt mindestens drei
  CAPEC-IDs für die risikoreichsten Flows, jeweils mit Gegenmaßnahme. /
  Threat model names at least three CAPEC IDs for the highest-risk
  flows, each with a mitigation.
- **Referenz / Reference:** Verfassung XVII.
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

#### CL-01-08: OWASP SAMM / OWASP SAMM

- **DE:** OWASP SAMM (Software Assurance Maturity Model, derzeit Version 2.x)
  bewertet die Sicherheitsreife eines Projekts oder einer Organisation in
  fünf Geschäftsfunktionen: **Governance** (Strategy & Metrics, Policy &
  Compliance, Education & Guidance), **Design** (Threat Assessment,
  Security Requirements, Security Architecture), **Implementation**
  (Secure Build, Secure Deployment, Defect Management), **Verification**
  (Architecture Assessment, Requirements-driven Testing, Security
  Testing), **Operations** (Incident Management, Environment Management,
  Operational Management). Je Praktik gibt es drei Reifestufen
  (1 = ad-hoc, 2 = strukturiert, 3 = kontinuierlich verbessert).
  Werkzeuge: SAMM-Toolbox als Excel oder Web-App
  (`https://owaspsamm.org/assessment/`); SAMMY (interaktiv); GitHub-
  Repository `OWASP/samm` mit Modellen und Beispielen. Verbesserungsplan
  benennt Ist-Stufe je Praktik, Ziel-Stufe in 6–12 Monaten,
  Maßnahmen und Verantwortliche.
- **EN:** OWASP SAMM (Software Assurance Maturity Model, currently version
  2.x) assesses security maturity of a project or organisation across
  five business functions: **Governance** (Strategy & Metrics, Policy
  & Compliance, Education & Guidance), **Design** (Threat Assessment,
  Security Requirements, Security Architecture), **Implementation**
  (Secure Build, Secure Deployment, Defect Management), **Verification**
  (Architecture Assessment, Requirements-driven Testing, Security
  Testing), **Operations** (Incident Management, Environment
  Management, Operational Management). Each practice has three
  maturity levels (1 = ad-hoc, 2 = structured, 3 = continuously
  improved). Tooling: SAMM Toolbox as Excel or web app
  (`https://owaspsamm.org/assessment/`); SAMMY (interactive); GitHub
  repository `OWASP/samm` with models and examples. The improvement
  plan names current level per practice, target level in 6–12 months,
  actions, and owners.
- **Akzeptanz / Acceptance:** SAMM-Bewertung mit Datum, Ist- und Ziel-
  Reifegrad je Praktik und Folgeaufgabenliste in
  `docs/security/samm-assessment.md`. / SAMM assessment with date,
  current and target maturity per practice, and follow-up task list in
  `docs/security/samm-assessment.md`.
- **Referenz / Reference:** Verfassung XVIII.
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

#### CL-01-09: OpenSSF Scorecard / OpenSSF Scorecard

- **DE:** OpenSSF Scorecard prüft öffentliche Git-Repositories anhand von
  ca. 19 automatischen Checks und liefert eine Punktzahl 0–10. Wichtige
  Checks: **Branch-Protection** (Schutz von `main`/`master`), **Code-
  Review** (≥ 2 Reviewer für Merges), **Signed-Commits** (signierte
  Commits oder Tags), **Token-Permissions** (CI-Token mit minimalen
  Rechten), **Vulnerabilities** (offene CVEs), **SAST** (statische
  Analyse aktiv), **Pinned-Dependencies** (Abhängigkeiten mit Hash
  gepinnt), **Dangerous-Workflow** (unsichere CI-Patterns),
  **Maintained** (Aktivität in den letzten 90 Tagen). Werkzeuge:
  CLI `scorecard --repo=github.com/<org>/<repo>`; GitHub Action
  `ossf/scorecard-action`; Web-Frontend
  `https://scorecard.dev/`. Schwellwert: ≥ 7 für kritische
  Abhängigkeiten, ≥ 5 als Mindestmaß. Niedrige Werte sind ein Risiko-
  Signal, kein automatischer Ausschluss; sie führen zu einem
  Risikoeintrag und ggf. zu einem Fork mit Härtung.
- **EN:** OpenSSF Scorecard rates public Git repositories via ~19
  automated checks and yields a score 0–10. Key checks: **Branch
  Protection** (protect `main`/`master`), **Code Review** (≥ 2
  reviewers for merges), **Signed Commits** (signed commits or tags),
  **Token Permissions** (CI tokens with minimum rights),
  **Vulnerabilities** (open CVEs), **SAST** (static analysis active),
  **Pinned Dependencies** (deps pinned by hash), **Dangerous Workflow**
  (unsafe CI patterns), **Maintained** (activity in the last 90 days).
  Tooling: CLI `scorecard --repo=github.com/<org>/<repo>`; GitHub
  Action `ossf/scorecard-action`; web frontend
  `https://scorecard.dev/`. Threshold: ≥ 7 for critical dependencies,
  ≥ 5 as the minimum bar. Low scores are a risk signal, not an
  automatic exclusion; they drive a risk entry and possibly a hardened
  fork.
- **Akzeptanz / Acceptance:** Scorecard-Wert je kritische Abhängigkeit
  mit Datum und Schwellwert dokumentiert, oder N/A mit Begründung. /
  Scorecard score per critical dependency with date and threshold
  documented, or N/A with justification.
- **Referenz / Reference:** Verfassung XVI.
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

#### CL-01-10: OWASP Cheat Sheets und Proactive Controls / OWASP Cheat Sheets and Proactive Controls

- **DE:** **OWASP Cheat Sheet Series** liefert über 80 thematische
  Anleitungen mit konkreten Empfehlungen, z. B. „Authentication Cheat
  Sheet", „Cross-Site Scripting Prevention", „SQL Injection Prevention",
  „Password Storage", „Input Validation", „Secure Headers", „REST
  Security", „GraphQL Security", „File Upload", „Cryptographic
  Storage", „Session Management". **OWASP Proactive Controls** (aktuell
  v3.0) gibt zehn entwicklerorientierte Kontrollen vor: C1
  Implementierungsstandards, C2 Bibliotheken und Frameworks, C3
  Authentifizierung, C4 Sitzungsmanagement, C5 Zugriffskontrolle, C6
  Verschlüsselung, C7 Validierung & Sanitization, C8 Logging & Audit,
  C9 Sicherheitskonfigurationen, C10 Fehler- und Ausnahmebehandlung.
  Im Review-Leitfaden werden die thematisch relevanten Cheat Sheets
  pro Pull-Request-Typ (Auth-Änderung, neue Endpoint, Crypto-Code)
  als Pflichtlektüre verlinkt; Proactive Controls erscheinen als
  Definition-of-Done-Checkliste. PR-Templates verweisen direkt auf
  die jeweils anzuwendenden Cheat-Sheet-URLs.
- **EN:** **OWASP Cheat Sheet Series** offers over 80 topic guides with
  concrete recommendations, e.g. "Authentication Cheat Sheet",
  "Cross-Site Scripting Prevention", "SQL Injection Prevention",
  "Password Storage", "Input Validation", "Secure Headers", "REST
  Security", "GraphQL Security", "File Upload", "Cryptographic
  Storage", "Session Management". **OWASP Proactive Controls**
  (currently v3.0) defines ten developer-focused controls: C1
  implementation standards, C2 libraries and frameworks, C3
  authentication, C4 session management, C5 access control, C6
  encryption, C7 validation & sanitization, C8 logging & audit, C9
  security configuration, C10 error and exception handling. In the
  review guide, the topical Cheat Sheets per pull request type (auth
  change, new endpoint, crypto code) are linked as required reading;
  Proactive Controls appear as a Definition-of-Done checklist. PR
  templates link directly to the applicable Cheat Sheet URLs.
- **Akzeptanz / Acceptance:** Code-Review-Leitfaden bzw. PR-Template
  enthält direkte Links zu den thematisch relevanten Cheat Sheets
  und Proactive Controls. / Code review guide or PR template
  contains direct links to the topical Cheat Sheets and Proactive
  Controls.
- **Referenz / Reference:** Verfassung XII.
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

#### CL-01-11: Nichtanwendbarkeit dokumentieren / Document Non-Applicability

- **DE:** Jeder nicht angewandte Standard wird ausdrücklich als „N/A" mit
  ein bis zwei Sätzen Begründung dokumentiert. Beispiele: „ASVS = N/A,
  weil das Projekt eine reine CLI ohne HTTP- oder Auth-Komponenten ist";
  „SBOM = N/A, weil dieses Repository nur Konfigurationsdateien
  enthält und keine ausführbaren Artefakte erzeugt"; „Zero Trust = N/A,
  weil das System ein lokal laufendes Single-User-Tool ohne
  Netzwerk-Komponenten ist"; „Scorecard = N/A, weil keine externen
  OSS-Abhängigkeiten genutzt werden". Die N/A-Einträge stehen im
  selben Dokument wie die erfüllten Anforderungen
  (`docs/security/standards-applicability.md` oder die jeweilige
  Standard-Datei). Stille Auslassung würde im Audit als Lücke
  bewertet. Bei Unsicherheit lieber „nicht erfüllt" als „N/A" wählen
  und eine Folgeaufgabe anlegen.
- **EN:** Every standard that does not apply is explicitly recorded as
  "N/A" with a one- or two-sentence justification. Examples: "ASVS =
  N/A because this project is a pure CLI with no HTTP or auth
  components"; "SBOM = N/A because this repository only contains
  configuration files and produces no executable artefacts"; "Zero
  Trust = N/A because the system is a locally running single-user
  tool without network components"; "Scorecard = N/A because no
  external OSS dependencies are used". N/A entries live in the same
  document as the fulfilled requirements
  (`docs/security/standards-applicability.md` or the respective
  standard file). Silent omission would be treated as a gap during
  audit. When uncertain, prefer "not fulfilled" over "N/A" and
  create a follow-up task.
- **Akzeptanz / Acceptance:** Jeder Punkt der Anwendbarkeits-Checkliste
  hat einen Status; N/A-Einträge tragen eine kurze Begründung. /
  Every item of the applicability checklist has a status; N/A
  entries carry a short justification.
- **Referenz / Reference:** Verfassung XIV.
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

#### CL-01-12: Regulatorische Anwendbarkeit / Regulatory Applicability

- **DE:** Für jedes Projekt wird zusätzlich zur technischen Standardauswahl
  geprüft, ob NIS2, CRA, EU AI Act, DORA oder sektor- und
  kundenspezifische Regeln anwendbar sind. Die Entscheidung wird als
  `anwendbar`, `nicht anwendbar` oder `offen` dokumentiert. Für private
  Lern-, Referenz- oder interne Tooling-Projekte darf `nicht anwendbar`
  gewählt werden, wenn das Projekt nicht als regulierter Dienst betrieben
  wird, nicht als reguliertes Produkt in Verkehr gebracht wird, nicht an
  regulierte Kunden geliefert wird und nicht Teil einer regulierten
  Lieferkette ist. Diese Begründung wird ausdrücklich festgehalten.
- **EN:** Every project checks, in addition to the technical standards
  selection, whether NIS2, CRA, the EU AI Act, DORA, or sector- and
  customer-specific rules apply. The decision is documented as
  `applicable`, `not applicable`, or `open`. For private learning,
  reference, or internal tooling projects, `not applicable` may be used
  when the project is not operated as a regulated service, not placed on
  the market as a regulated product, not delivered to regulated customers,
  and not part of a regulated supply chain. This rationale is recorded
  explicitly.
- **Akzeptanz / Acceptance:** `docs/security/regulatory-applicability.md`
  oder ein gleichwertiges Spec-Kit-Artefakt enthält eine Matrix zu NIS2,
  CRA, EU AI Act, DORA und kundenspezifischen Regeln mit Entscheidung,
  Begründung, Evidenzpfad, verantwortlicher Person und Wiedervorlage. /
  `docs/security/regulatory-applicability.md` or an equivalent Spec Kit
  artefact contains a matrix for NIS2, CRA, EU AI Act, DORA, and
  customer-specific rules with decision, rationale, evidence path, owner,
  and re-review trigger.
- **Referenz / Reference:** Spec-Kit `security-governance`, Template
  `regulatory-applicability-template`.
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
- **Evidenz / Evidence:** _Regulatory-Matrix, Spec-Kit-Artefakt, Ticket oder N/A-Begründung nennen. Wenn der Nachweis nicht selbsterklärend ist, kurz beschreiben, was er zeigt. / Name regulatory matrix, Spec Kit artefact, ticket, or N/A rationale. If the evidence is not self-explanatory, briefly describe what it shows._
- **Restrisiko / Residual risk:** _Verbleibendes Risiko oder `None` mit kurzer Begründung dokumentieren. / Record remaining risk or `None` with a short rationale._
- **Neubewertungs-Trigger / Re-evaluation trigger:** _Aenderung, Termin oder Ereignis nennen, das eine erneute Prüfung auslöst. / Name the change, date, or event that triggers renewed review._
- **Nächste Maßnahme / Next action:** _Bei `Open`, `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` ausfüllen: konkrete Aufgabe, verantwortliche Person, Zieltermin und erwarteter Nachweis. / Fill for `Open`, `Partly Fulfilled`, `Not Fulfilled`, or `Not Assessed`: concrete task, owner, target date, and expected evidence._

### Akzeptanzkriterien gesamt / Overall Acceptance

**DE:** Die Checkliste ist erfüllt, wenn alle Punkte mit „erfüllt" oder „N/A
mit Begründung" abgeschlossen sind und die Evidenzpfade in
`docs/security/` aufrufbar sind. Regulatorische Entscheidungen sind
ausdrücklich dokumentiert und offene Fragen haben eine verantwortliche
Person und Wiedervorlage.

**EN:** The checklist is fulfilled when every item is closed as "done" or
"N/A with justification" and the evidence paths under `docs/security/` are
reachable. Regulatory decisions are documented explicitly, and open
questions have an owner and re-review trigger.

### Glossar / Glossary

**DE:** Dieses Glossar erklärt die wichtigsten Begriffe dieser Checkliste in Alltagssprache. Es ändert keine Anforderungen, sondern macht die vorhandenen Prüfpunkte leichter verständlich.

**EN:** This glossary explains the most important terms in this checklist in plain language. It does not change requirements; it makes the existing review items easier to understand.

<a id="cl-01-glossar-iso-27001"></a>

#### ISO/IEC 27001

- **DE:** ISO/IEC 27001 ist ein internationaler Standard für Sicherheitsnachweisprozesse. Er fordert gesteuerte Prozesse, Risikobehandlung, Nachweise und regelmäßige Verbesserung.
- **EN:** ISO/IEC 27001 is an international standard for security evidence processs. It requires controlled processes, risk treatment, evidence, and regular improvement.

<a id="cl-01-glossar-nist-ssdf"></a>

#### NIST SSDF

- **DE:** NIST SSDF ist ein Rahmenwerk für sichere Softwareentwicklung. Es beschreibt, wie Teams Software sicher planen, bauen, prüfen und nach Schwachstellen verbessern.
- **EN:** NIST SSDF is a framework for secure software development. It describes how teams plan, build, review, and improve software after vulnerabilities.

<a id="cl-01-glossar-cwe-top-25"></a>

#### CWE Top 25

- **DE:** Die CWE Top 25 ist eine Liste häufiger und gefährlicher Programmierfehler. Beispiele sind Cross-Site Scripting, SQL Injection, Path Traversal und unsichere Speicherzugriffe.
- **EN:** The CWE Top 25 is a list of common and dangerous programming mistakes. Examples are cross-site scripting, SQL injection, path traversal, and unsafe memory access.

<a id="cl-01-glossar-owasp-asvs"></a>

#### OWASP ASVS

- **DE:** OWASP ASVS ist ein Prüfkatalog für Anwendungssicherheit. Er hilft, passende Sicherheitsanforderungen für Web-Anwendungen und APIs festzulegen.
- **EN:** OWASP ASVS is an application security verification catalogue. It helps define suitable security requirements for web applications and APIs.

<a id="cl-01-glossar-owasp-samm"></a>

#### OWASP SAMM

- **DE:** OWASP SAMM ist ein Reifegradmodell für sichere Softwareentwicklung. Es zeigt, wie Teams ihre Sicherheitsprozesse schrittweise verbessern können.
- **EN:** OWASP SAMM is a maturity model for secure software development. It shows how teams can improve their security processes step by step.

<a id="cl-01-glossar-sbom"></a>

#### SBOM / Software Bill of Materials

- **DE:** Eine SBOM ist eine Stückliste für Software. Sie nennt Komponenten, Versionen und oft Lizenzen, damit Risiken und Schwachstellen schneller gefunden werden.
- **EN:** An SBOM is a bill of materials for software. It lists components, versions, and often licences so that risks and vulnerabilities can be found faster.

<a id="cl-01-glossar-vex"></a>

#### VEX / Vulnerability Exploitability eXchange

- **DE:** VEX erklärt, ob eine bekannte Schwachstelle in einem konkreten Produkt wirklich ausnutzbar ist. Das verhindert unnötige Alarmarbeit.
- **EN:** VEX explains whether a known vulnerability is actually exploitable in a specific product. This prevents unnecessary alert handling.

<a id="cl-01-glossar-slsa"></a>

#### SLSA

- **DE:** SLSA beschreibt Schutzstufen für Software-Lieferketten. Es geht vor allem um nachvollziehbare Builds, Herkunftsnachweise und Schutz gegen Manipulation.
- **EN:** SLSA describes protection levels for software supply chains. It mainly covers traceable builds, provenance evidence, and protection against tampering.

<a id="cl-01-glossar-zero-trust"></a>

#### Zero Trust

- **DE:** Zero Trust bedeutet: Nicht automatisch vertrauen, nur weil etwas im internen Netz ist. Zugriff wird geprüft, begrenzt und laufend kontrolliert.
- **EN:** Zero Trust means: do not trust something automatically just because it is inside the internal network. Access is checked, limited, and continuously controlled.

<a id="cl-01-glossar-capec"></a>

#### CAPEC

- **DE:** CAPEC ist ein Katalog bekannter Angriffsmuster. Er hilft beim Bedrohungsmodell, typische Angriffe systematisch zu erkennen.
- **EN:** CAPEC is a catalogue of known attack patterns. It helps threat models identify typical attacks systematically.

<a id="cl-01-glossar-openssf-scorecard"></a>

#### OpenSSF Scorecard

- **DE:** OpenSSF Scorecard ist ein Werkzeug, das öffentliche Repositories auf Sicherheitspraktiken prüft, zum Beispiel Branch-Schutz, Abhängigkeiten und CI-Konfiguration.
- **EN:** OpenSSF Scorecard is a tool that checks public repositories for security practices, for example branch protection, dependencies, and CI configuration.

<a id="cl-01-glossar-cra"></a>

#### CRA / Cyber Resilience Act

- **DE:** Der Cyber Resilience Act ist eine EU-Verordnung für Produkte mit digitalen Elementen. Er verlangt Sicherheitsanforderungen, Schwachstellenbehandlung und bestimmte Meldungen.
- **EN:** The Cyber Resilience Act is an EU regulation for products with digital elements. It requires security requirements, vulnerability handling, and certain reports.

<a id="cl-01-glossar-nis2"></a>

#### NIS2

- **DE:** NIS2 ist eine EU-Richtlinie für Cybersicherheit wichtiger und besonders wichtiger Einrichtungen. Sie kann Meldewege, Risikomanagement und Lieferkettenschutz beeinflussen.
- **EN:** NIS2 is an EU directive for cybersecurity of important and essential entities. It can affect reporting paths, risk management, and supply-chain protection.

<a id="cl-01-glossar-dora"></a>

#### DORA

- **DE:** DORA ist eine EU-Verordnung zur digitalen Betriebsstabilität im Finanzsektor. Sie kann Anforderungen an IT-Risiken, Dienstleister und Sicherheitsvorfälle auslösen.
- **EN:** DORA is an EU regulation on digital operational resilience in the financial sector. It can trigger requirements for IT risks, service providers, and security incidents.

<a id="cl-01-glossar-eu-ai-act"></a>

#### EU AI Act

- **DE:** Der EU AI Act ist eine EU-Verordnung für KI-Systeme. Er kann Pflichten zu Risiko, Dokumentation, Transparenz und menschlicher Aufsicht auslösen.
- **EN:** The EU AI Act is an EU regulation for AI systems. It can trigger duties for risk, documentation, transparency, and human oversight.

<a id="cl-01-glossar-soa"></a>

#### SoA / Statement of Applicability / Anwendbarkeitserklärung

- **DE:** Die SoA ist die zentrale Liste der ISO-27001-Controls. Sie zeigt, welche Controls gelten, welche nicht gelten, warum das so ist und wie der Umsetzungsstand ist.
- **EN:** The SoA is the central list of ISO 27001 controls. It shows which controls apply, which do not apply, why this is the case, and what the implementation status is.

<a id="cl-01-glossar-nicht-anwendbar"></a>

#### Nicht anwendbar / N/A

- **DE:** Nicht anwendbar bedeutet, dass ein Prüfpunkt sachlich nicht zum Projekt passt. Diese Entscheidung braucht immer eine kurze und nachvollziehbare Begründung.
- **EN:** Not applicable means that a review item does not fit the project. This decision always needs a short and traceable reason.

### Versionshistorie / Version History

- **Version 1.0 (2026-04-27):** Erstfassung / Initial version
- **Version 1.1 (2026-04-27):** Erweiterte Durchführungshinweise, Quellen-URLs, Statusfelder und Beispiele / Extended guidance, source URLs, status fields, and examples
- **Version 1.2 (2026-04-30):** ASVS 5.0.0 und SLSA v1.2 Build-Track aktualisiert / Updated ASVS 5.0.0 and SLSA v1.2 Build track
- **Version 1.3 (2026-06-15):** Prüfpunkt 12 zur regulatorischen Anwendbarkeit von NIS2, CRA, EU AI Act, DORA und kundenspezifischen Regeln ergänzt; synchron mit Richtlinie Sichere Entwicklung v2.9.0. / Added item 12 for regulatory applicability of NIS2, CRA, EU AI Act, DORA, and customer-specific rules; synchronized with Richtlinie Sichere Entwicklung v2.9.0.

- **Version 1.4 (2026-06-16):** Verständlichkeit der Durchführungshinweise, Begründungs-, Evidenz- und Maßnahmenfelder für Entwickler:innen und Auszubildende präzisiert; CEFR-B2- und WCAG-2.2-AA-konforme Ausfüllhilfe ergänzt. / Refined understandability of implementation guidance, rationale, evidence, and action fields for developers and apprentices; added CEFR B2 and WCAG 2.2 AA conformant completion help.

- **Version 1.5 (2026-06-17):** Glossar und Begriff-Links für Entwickler:innen und Fachinformatik-Auszubildende ergänzt; wichtige Abkürzungen und Technologien in CEFR-B2-Sprache erklärt. / Added glossary and term links for developers and IT specialist apprentices; explained important abbreviations and technologies in CEFR B2 language.

---

- **Version 2.0.0 (2026-07-10):** Einheitliches zweiachsiges Statusmodell, stabile CL-IDs, Lernstufe, Rollen-, Evidenz-, Restrisiko- und Neubewertungsfelder sowie klare Trennung zwischen Vorlage und Projektnachweis eingeführt; synchron mit sichere-Entwicklung-Basis 3.0.0. / Added the unified two-axis status model, stable CL IDs, learning-stage, role, evidence, residual-risk, and re-evaluation fields, plus clear separation between template and project evidence; synchronized with secure-development baseline 3.0.0.
